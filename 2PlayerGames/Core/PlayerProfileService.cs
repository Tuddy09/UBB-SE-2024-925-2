using TwoPlayerGames;

namespace _2PlayerGames.Core
{
    public class PlayerProfileService
    {
        public PlayerProfileService()
        {
        }

        public List<string> GetProfileStats(Player player)
        {
            List<string> profileStats = new List<string> { "diamond", "512", "Mata" };
            return profileStats;
        }


        public List<string> GetGameHistory(Player player)
        {
            return new List<string>();
        }
    }
}
