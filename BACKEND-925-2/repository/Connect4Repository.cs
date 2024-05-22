using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Repository;

namespace TwoPlayerGames.Repo
{
    public class Connect4Repository : BaseGameRepository, IGameRepo
    {
        public Connect4Repository() : base()
        {
        }

        public override Dictionary<Guid, IGame> GetGames()
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            Dictionary<Guid, IGame> games = new Dictionary<Guid, IGame>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM getAllConnect4Games", sqlConnection))
            {
                sqlConnection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid gameStateId = reader.GetGuid(0);
                    Player player1 = PlayerRepository.GetPlayerById(reader.GetGuid(1));
                    Player player2 = PlayerRepository.GetPlayerById(reader.GetGuid(2));
                    Games gameType = GameStore.GetGameById(reader.GetGuid(3));
                    int turn = reader.GetInt32(4);
                    int timePlayed = reader.GetInt32(5);
                    Player? winner = reader[6] == System.DBNull.Value ? null : (player1.Id == reader.GetGuid(6) ? player1 : player2);
                    string jsonString = reader.GetString(7);
                    GameState gameState = new GameState(gameStateId, player1, player2, gameType, turn, timePlayed, winner, jsonString);
                    IGame connect4Game = LoadGameFromUnfinishedState(gameState);
                    games.Add(gameStateId, connect4Game);
                }
                reader.Close();
                sqlConnection.Close();
            }
            return games;
        }
    }
}
