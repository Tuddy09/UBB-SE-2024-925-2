using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Repo;

namespace TwoPlayerGames.Repository
{
    public class SoundStore
    {
        public static Dictionary<string, Sounds> Sounds { get; } = InitializeSounds();

        private static Dictionary<string, Sounds> InitializeSounds()
        {
            SqlConnection sqlConnection = Configurator.SqlConnection;
            Dictionary<string, Sounds> dictionaryOfSounds = new ();
            using (SqlCommand command = new ("SELECT * FROM SoundEffects", sqlConnection))
            {
                sqlConnection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dictionaryOfSounds.Add(reader.GetString(1), new Sounds(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), Configurator.SoundStorePath + reader.GetString(3)));
                }
                reader.Close();
            }
            sqlConnection.Close();
            return dictionaryOfSounds;
        }
    }
}
