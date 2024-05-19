using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Domain.DatabaseObjects;
using TwoPlayerGames.Domain.Enums;
using TwoPlayerGames.Repo;

namespace TwoPlayerGames.Repository
{
    public class PlayerQueueRepository
    {
        private static PlayerQueueRepository? instance;

        private PlayerQueueRepository()
        {
        }

        public static PlayerQueueRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new PlayerQueueRepository();
            }
            return instance;
        }

        public static bool AddPlayer(PlayerQueue player)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("addToQueue", sqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@playerId", player.Player.Id);
            command.Parameters.AddWithValue("@gameId", player.GameType.Id);
            command.Parameters.AddWithValue("@eloRating", player.EloRating);
            command.Parameters.AddWithValue("@chessGameMode", player.ChessMode is null ? DBNull.Value : (int)player.ChessMode);
            command.Parameters.AddWithValue("@obstructionWidth", player.ObstractionWidth is null ? DBNull.Value : player.ObstractionWidth);
            command.Parameters.AddWithValue("@obstructionHeight", player.ObstractionHeight is null ? DBNull.Value : player.ObstractionHeight);
            sqlConnection.Open();

            bool result = command.ExecuteNonQuery() == 1;
            sqlConnection.Close();
            return result;
        }

        public static PlayerQueue? GetPlayerByPlayerId(Guid playerId)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("SELECT * FROM getPlayerQueue(@playerId)", sqlConnection);
            command.Parameters.AddWithValue("@playerId", playerId);
            sqlConnection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                PlayerQueue queue = new (
                    player: PlayerRepository.GetPlayerById(reader.GetGuid(0)),
                    gameType: GameStore.GetGameById(reader.GetGuid(1)),
                    elo: reader.GetInt32(2),
                    chessMode: reader[3] == DBNull.Value ? null : (ChessModes)reader.GetInt32(5),
                    obstructionWidth: reader[4] == DBNull.Value ? null : reader.GetInt32(4),
                    obstructionHeigth: reader[5] == DBNull.Value ? null : reader.GetInt32(5));
                reader.Close();
                sqlConnection.Close();

                return queue;
            }
            reader.Close();
            sqlConnection.Close();
            return null;
        }

        public static List<PlayerQueue> GetPlayers()
        {
            List<PlayerQueue> playerQueues = new ();
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new ("SELECT * FROM PlayerQueue", sqlConnection))
            {
                sqlConnection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    playerQueues.Add(new PlayerQueue(
                       player: PlayerRepository.GetPlayerById(reader.GetGuid(0)),
                       gameType: GameStore.GetGameById(reader.GetGuid(1)),
                       elo: reader.GetInt32(2),
                       chessMode: reader[3] == DBNull.Value ? null : (ChessModes)reader.GetInt32(3),
                       obstructionWidth: reader[4] == DBNull.Value ? null : reader.GetInt32(4),
                       obstructionHeigth: reader[5] == DBNull.Value ? null : reader.GetInt32(5)));
                }
                reader.Close();
            }
            sqlConnection.Close();
            return playerQueues;
        }

        public static void RemovePlayer(PlayerQueue player)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("removeFromQueue", sqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@playerId", player.Player.Id);
            sqlConnection.Open();

            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static List<PlayerQueue> GetAvailableChessPlayers(PlayerQueue player)
        {
            List<PlayerQueue> playerQueues = new ();
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new ("SELECT * FORM getAvailableChessGames(@playerId, @eloRating, @chessGameMode))", sqlConnection))
            {
                command.Parameters.AddWithValue("@playerId", player.Player.Id);
                command.Parameters.AddWithValue("@eloRating", player.EloRating);
                command.Parameters.AddWithValue("@chessGameMode", (int)player.ChessMode);
                sqlConnection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    playerQueues.Add(new PlayerQueue(
                       player: PlayerRepository.GetPlayerById((Guid)reader["PlayerId"]),
                       gameType: GameStore.GetGameById((Guid)reader["GameId"]),
                       elo: (int)reader["Elo"],
                       chessMode: reader[3] == DBNull.Value ? null : (ChessModes)reader.GetInt32(3),
                       obstructionWidth: null,
                       obstructionHeigth: null));
                }
                reader.Close();
            }
            sqlConnection.Close();
            return playerQueues;
        }

        public static List<PlayerQueue> GetAvailableDartsPlayers(PlayerQueue player)
        {
            List<PlayerQueue> playerQueues = new ();
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new ("SELECT * FORM getAvailableDartsGames(@playerId, @eloRating))", sqlConnection))
            {
                command.Parameters.AddWithValue("@playerId", player.Player.Id);
                command.Parameters.AddWithValue("@eloRating", player.EloRating);
                sqlConnection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    playerQueues.Add(new PlayerQueue(
                       player: PlayerRepository.GetPlayerById((Guid)reader["PlayerId"]),
                       gameType: GameStore.Games["Darts"],
                       elo: (int)reader["Elo"],
                       chessMode: null,
                       obstructionWidth: null,
                       obstructionHeigth: null));
                }
                reader.Close();
            }
            sqlConnection.Close();
            return playerQueues;
        }

        public static List<PlayerQueue> GetAvailableConnect4Players(PlayerQueue player)
        {
            List<PlayerQueue> playerQueues = new ();
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new ("SELECT * FORM getAvailableConnect4Games(@playerId, @eloRating))", sqlConnection))
            {
                command.Parameters.AddWithValue("@playerId", player.Player.Id);
                command.Parameters.AddWithValue("@eloRating", player.EloRating);
                sqlConnection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    playerQueues.Add(new PlayerQueue(
                       player: PlayerRepository.GetPlayerById((Guid)reader["PlayerId"]),
                       gameType: GameStore.Games["Connect4"],
                       elo: (int)reader["Elo"],
                       chessMode: null,
                       obstructionWidth: null,
                       obstructionHeigth: null));
                }
                reader.Close();
            }
            sqlConnection.Close();
            return playerQueues;
        }

        public static List<PlayerQueue> GetAvailableObstructionPlayers(PlayerQueue player)
        {
            List<PlayerQueue> playerQueues = new ();
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new ("SELECT * FORM getAvailableObstructionGames(@playerId, @eloRating, @obstructionWidth, @obstructionHeight))",
                sqlConnection))
            {
                command.Parameters.AddWithValue("@playerId", player.Player.Id);
                command.Parameters.AddWithValue("@eloRating", player.EloRating);
                command.Parameters.AddWithValue("@obstructionWidth", player.ObstractionWidth);
                command.Parameters.AddWithValue("@obstructionHeight", player.ObstractionHeight);
                sqlConnection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    playerQueues.Add(new PlayerQueue(
                       player: PlayerRepository.GetPlayerById((Guid)reader["PlayerId"]),
                       gameType: GameStore.Games["Obstruction"],
                       elo: (int)reader["Elo"],
                       chessMode: null,
                       obstructionWidth: (int)reader["ObsWidth"],
                       obstructionHeigth: (int)reader["ObsHeight"]));
                }
                reader.Close();
            }
            sqlConnection.Close();
            return playerQueues;
        }
    }
}
