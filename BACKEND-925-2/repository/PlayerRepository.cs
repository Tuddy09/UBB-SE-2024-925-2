using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Repo;

namespace TwoPlayerGames.Repository
{
    public class PlayerRepository
    {
        public static Dictionary<Guid, Player> Players { get; } = PlayerRepository.GetAllPlayers();
        public static Player DrawPlayer { get; } = GetPlayerById(Guid.Empty);
        public static Player AIPlayer { get; } = GetPlayerById(Guid.Parse("00000000-0000-0000-0000-000000000001"));

        public static Player? GetPlayerById(Guid id)
        {
            return Players.TryGetValue(id, out Player? value) ? value : null;
        }

        public static Dictionary<Guid, Player> GetAllPlayers()
        {
            SqlConnection connection = Configurator.SqlConnection;
            connection.Open();
            Dictionary<Guid, Player> players = new ();
            using (SqlCommand command = new ("SELECT * FROM Player", connection))
            {
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    players.Add(reader.GetGuid(0), new Player(reader.GetGuid(0), reader.GetString(1),
                        reader.IsDBNull(2) ? null : reader.GetString(2),
                        reader.IsDBNull(3) ? null : reader.GetInt32(3)));
                }
                connection.Close();
                reader.Close();
            }
            connection.Close();
            return players;
        }

        public static bool AddPlayer(Player player)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("addPlayer", sqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@playerId", player.Id);
            command.Parameters.AddWithValue("@playerName", player.Name);
            command.Parameters.AddWithValue("@playerPcIp", player.Ip);
            command.Parameters.AddWithValue("@playerPcPort", player.Port);

            sqlConnection.Open();
            int result = command.ExecuteNonQuery();

            sqlConnection.Close();

            return result == 1;
        }

        public static bool RemoveAddressById(Guid id)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("removePlayerAddress", sqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@playerId", id);
            sqlConnection.Open();

            int result = command.ExecuteNonQuery();

            sqlConnection.Close();

            return result == 1;
        }

        public static bool UpdatePlayer(Player player)
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("updatePlayer", sqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@playerId", player.Id);
            command.Parameters.AddWithValue("@playerName", player.Name);
            command.Parameters.AddWithValue("@playerPcIp", player.Ip);
            command.Parameters.AddWithValue("@playerPcPort", player.Port);
            sqlConnection.Open();

            int result = command.ExecuteNonQuery();

            sqlConnection.Close();

            return result == 1;
        }

        public static bool RemovePlayerWhereNameEqualsTestOrUpdated()
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using SqlCommand command = new ("delete from Player where playerName = 'Test' or playerName = 'Updated'", sqlConnection);
            sqlConnection.Open();
            int result = command.ExecuteNonQuery();
            sqlConnection.Close();
            return result == 1;
        }
    }
}
