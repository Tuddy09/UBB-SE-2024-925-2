using System;
using System.Data.SqlClient;

namespace TwoPlayerGames.Repo
{
    public class Configurator
    {
        public static SqlConnection SqlConnection { get; } = new SqlConnection("Data Source=TUDDYDESKTOP\\SQLEXPRESS;Initial Catalog=GamesDatabase;Integrated Security=True; TrustServerCertificate = True;");

        public static string SoundStorePath { get; } = "C:\\soundEffects\\";
    }
}
