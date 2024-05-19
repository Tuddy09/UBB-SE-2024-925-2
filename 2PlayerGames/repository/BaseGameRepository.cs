using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Domain.GameObjects;
using TwoPlayerGames.exceptions;
using TwoPlayerGames.Repository;

namespace TwoPlayerGames.Repo
{
    public abstract class BaseGameRepository : IGameRepo
    {
        private Dictionary<Guid, IGame> games;

        public BaseGameRepository()
        {
            games = GetGames();
        }

        public void UpdateRepo()
        {
            games = GetGames();
        }
        public abstract Dictionary<Guid, IGame> GetGames();

        public IGame? GetGameById(Guid id)
        {
            UpdateRepo();
            if (games != null)
            {
                try
                {
                    return games[id];
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void AddGame(IGame game)
        {
            games.Add(game.GameState.Id, game);
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new SqlCommand("addGameState", sqlConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@gameStateId", game.GameState.Id);
                command.Parameters.AddWithValue("@player1Id", game.GameState.Players[0].Id);
                command.Parameters.AddWithValue("@player2Id", game.GameState.Players[1].Id);
                command.Parameters.AddWithValue("@gameId", game.GameState.GameType.Id);
                command.Parameters.AddWithValue("@turn", game.GameState.Turn);
                command.Parameters.AddWithValue("@timePlayed", game.GameState.TimePlayed);
                command.Parameters.AddWithValue("@winnerPlayer", game.GameState.Winner is null ? DBNull.Value : game.GameState.Winner.Id);
                command.Parameters.AddWithValue("@gameState", game.GameState.StateJson);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void UpdateGame(IGame game)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new SqlCommand("updateGameState", sqlConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@gameStateId", game.GameState.Id);
                command.Parameters.AddWithValue("@turn", game.GameState.Turn);
                command.Parameters.AddWithValue("@timePlayed", game.GameState.TimePlayed);
                command.Parameters.AddWithValue("@winnerPlayer", game.GameState.Winner is null ? DBNull.Value : game.GameState.Winner.Id);
                command.Parameters.AddWithValue("@gameState", game.GameState.StateJson);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void RemoveGame(Guid id)
        {
            games.Remove(id);
        }

        public IGame LoadGameFromUnfinishedState(GameState unifinishedGameState)
        {
            IGame game = unifinishedGameState.GameType.Name switch
            {
                "Darts" => new DartsGame(unifinishedGameState),
                "Obstruction" => new ObstructionGame(unifinishedGameState),
                "Connect4" => new Connect4Game(unifinishedGameState),
                "Chess" => new ChessGame(unifinishedGameState),
                _ => throw new GameTypeNotFoundException("Game type not found")
            };
            return game;
        }

        public IGame GetGameFromDatabase(Guid id)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new SqlCommand("SELECT * FROM GameState WHERE gameStateId = @id", sqlConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                sqlConnection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Player player1 = PlayerRepository.GetPlayerById(reader.GetGuid(1));
                        Player player2 = PlayerRepository.GetPlayerById(reader.GetGuid(2));
                        GameState game = new GameState(
                            gameStateId: reader.GetGuid(0),
                            player1,
                            player2,
                            GameStore.GetGameById(reader.GetGuid(3)),
                            reader.GetInt32(4),
                            reader.GetInt32(5),
                            reader.IsDBNull(6) ? null : (player1.Id == reader.GetGuid(6)) ? player1 : player2,
                            jsonString: reader.GetString(7));
                        IGame gameObject = game.GameType.Name switch
                        {
                            "Darts" => new DartsGame(game),
                            "Obstruction" => new ObstructionGame(game),
                            "Connect4" => new Connect4Game(game),
                            "Chess" => new ChessGame(game),
                            _ => throw new GameTypeNotFoundException("Game type not found")
                        };
                        reader.Close();
                        sqlConnection.Close();
                        return gameObject;
                    }
                    else
                    {
                        reader.Close();
                        sqlConnection.Close();
                        return null;
                    }
                }
            }
        }
    }
}
