using BACKEND_925_2.Models;
using System;
using System.Collections.Generic;
using TwoPlayerGames;
using TwoPlayerGames.Domain.DatabaseObjects;
using TwoPlayerGames.Repository;

namespace BACKEND_925_2.Service
{
    public class PlayerProfileService
    {
        StatsRepository StatsRepository;
        public PlayerProfileService(GamesDbContext gamesDbContext)
        {
            StatsRepository = new StatsRepository(gamesDbContext);
        }

        public List<string> GetProfileStats(Player2PlayerGame player)
        {
            List<string> profileStats = new List<string> { "diamond", "512", "Mata" };
            return profileStats;
        }

        public List<string> GetGameStats(Player2PlayerGame player, string gameType)
        {
            GameStats gameStats = StatsRepository.GetGameStatsForPlayer(player, GameStore.Games[gameType]);
            List<string> stats =
            [
                gameStats.EloRating.ToString(),
                gameStats.HighestElo.ToString(),
                gameStats.TotalMatches.ToString(),
                gameStats.TotalWins.ToString(),
                gameStats.TotalDraws.ToString(),
                gameStats.TotalPlayTime.ToString(),
                gameStats.TotalNumberOfTurn.ToString(),
            ];
            return stats;
        }

        public List<string> GetGameHistory(Player2PlayerGame player)
        {
            return new List<string>();
        }

        public void UpdateStats(Player2PlayerGame player, Games gameType, int newElo)
        {
            StatsRepository.UpdateStatsForPlayer(player, gameType, newElo);
        }
    }
}
