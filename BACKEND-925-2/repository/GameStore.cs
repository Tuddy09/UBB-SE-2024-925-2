using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TwoPlayerGames.Repo;

namespace TwoPlayerGames.Repository
{
    public class GameStore
    {
        public static Dictionary<string, Games> Games { get; } = InitializeGamesFromDatabase();

        private static Dictionary<string, Games> InitializeGamesFromDatabase()
        {
            Dictionary<string, Games> dictionaryOfGames = new Dictionary<string, Games>();
            SqlConnection sqlConnection = Configurator.SqlConnection;
            using (SqlCommand command = new SqlCommand("SELECT * FROM Game", sqlConnection))
            {
                sqlConnection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dictionaryOfGames.Add(reader.GetString(1), new Games(reader.GetGuid(0), reader.GetString(1), reader.GetString(2)));
                    }
                    reader.Close();
                }

                sqlConnection.Close();
            }

            return dictionaryOfGames;
        }

        public static Games GetGameById(Guid id)
        {
            return Games.Values.FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
