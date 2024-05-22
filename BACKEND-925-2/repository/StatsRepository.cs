using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Domain.Auxiliary;
using TwoPlayerGames.Domain.DatabaseObjects;
using TwoPlayerGames.Repo;

namespace TwoPlayerGames.Repository
{
    public class StatsRepository
    {
        public static bool AddStats(GameStats gameStats)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("addGameStats", sqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@playerId", gameStats.Player.Id);
            command.Parameters.AddWithValue("@gameId", gameStats.Game.Id);
            command.Parameters.AddWithValue("@eloRating", gameStats.EloRating);
            command.Parameters.AddWithValue("@highestEloRating", gameStats.HighestElo);
            command.Parameters.AddWithValue("@totalPlayTime", gameStats.TotalPlayTime);
            command.Parameters.AddWithValue("@totalGamesPlayed", gameStats.TotalMatches);
            command.Parameters.AddWithValue("@totalWins", gameStats.TotalWins);
            command.Parameters.AddWithValue("@totalDraws", gameStats.TotalDraws);
            command.Parameters.AddWithValue("@numberTurns", gameStats.TotalNumberOfTurn);
            sqlConnection.Open();

            int result = command.ExecuteNonQuery();

            sqlConnection.Close();

            return result == 1;
        }

        public static bool UpdateStatsForPlayer(Player player, Games gameType, int newEloRating)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("updateGameStatsAuto", sqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@playerId", player.Id);
            command.Parameters.AddWithValue("@gameId", gameType.Id);
            command.Parameters.AddWithValue("@eloRating", newEloRating);
            sqlConnection.Open();

            int result = command.ExecuteNonQuery();

            sqlConnection.Close();

            return result == 1;
        }

        public static GameStats GetGameStatsForPlayer(Player player, Games gameType)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("SELECT * FROM getGameStats(@playerId, @gameId)", sqlConnection);
            command.Parameters.AddWithValue("@playerId", player.Id);
            command.Parameters.AddWithValue("@gameId", gameType.Id);
            sqlConnection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                GameStats newGameStats = new GameStats(player, gameType, reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4),
                    reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8));
                reader.Close();
                sqlConnection.Close();
                return newGameStats;
            }
            else
            {
                reader.Close();
                sqlConnection.Close();
                return new GameStats(player, gameType);
            }
        }

        public static List<GameHistory> GetGameHistoryForPlayer(Player player)
        {
            List<GameHistory> gameHistories = new ();
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new ("SELECT * FROM getGameHistory(@playerId)", sqlConnection))
            {
                command.Parameters.AddWithValue("@playerId", player.Id);
                sqlConnection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Player player1 = PlayerRepository.GetPlayerById((Guid)reader["player1"]);
                    Player player2 = PlayerRepository.GetPlayerById((Guid)reader["player2"]);
                    gameHistories.Add(new GameHistory(
                        player1,
                        player2,
                        GameStore.GetGameById((Guid)reader["gameId"]),
                        player1.Id != (Guid)reader["winner"] ? player2 : player1));
                }
                reader.Close();
            }
            sqlConnection.Close();
            return gameHistories;
        }

        public static PlayerStats GetProfileStatsForPlayer(Player player)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("SELECT * FROM getPlayerStatsGood(@playerId)", sqlConnection);
            command.Parameters.AddWithValue("@playerId", player.Id);
            sqlConnection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int trophies = (int)reader["trophies"];
                int eloRating = (int)reader["averageElo"];
                Games game = GameStore.GetGameById((Guid)reader["mostPlayedGame"]);
                string rank = RankDeterminer.DetermineRank(eloRating);
                PlayerStats playerStats = new (player, trophies, eloRating, rank, game);
                reader.Close();
                sqlConnection.Close();
                return playerStats;
            }
            else
            {
                reader.Close();
                sqlConnection.Close();
                return new PlayerStats(player);
            }
        }
    }
}
