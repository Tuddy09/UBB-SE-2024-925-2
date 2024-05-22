using BACKEND_925_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TwoPlayerGames.Domain.Auxiliary;
using TwoPlayerGames.Domain.DatabaseObjects;
using TwoPlayerGames.Repo;

namespace TwoPlayerGames.Repository
{
    public class StatsRepository
    {
        private readonly GamesDbContext _context;

        public StatsRepository(GamesDbContext context)
        {
            _context = context;
        }

        public bool AddStats(GameStats gameStats)
        {
            try
            {
                _context.GameStats.Add(gameStats);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateStatsForPlayer(Player2PlayerGame player, Games gameType, int newEloRating)
        {
            try
            {
                var gameStats = _context.GameStats.FirstOrDefault(gs => gs.Player == player && gs.Game == gameType);
                if (gameStats != null)
                {
                    gameStats.EloRating = newEloRating;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public GameStats GetGameStatsForPlayer(Player2PlayerGame player, Games gameType)
        {
            var gameStats = _context.GameStats.FirstOrDefault(gs => gs.Player == player && gs.Game == gameType);
            return gameStats ?? new GameStats(player, gameType);
        }

        public List<GameHistory> GetGameHistoryForPlayer(Player2PlayerGame player)
        {
            var gameHistories = _context.GameHistories
                .Where(gh => gh.Players[0] == player || gh.Players[1] == player)
                .ToList();

            foreach (var history in gameHistories)
            {
                history.Players[0] = _context.Players.Find(history.Players[0].Id);
                history.Players[1] = _context.Players.Find(history.Players[1].Id);
                history.GameType = _context.Games.Find(history.GameType);
                history.Winner = _context.Players.Find(history.Winner);
            }

            return gameHistories;
        }

        public PlayerStats GetProfileStatsForPlayer(Player2PlayerGame player)
        {
            var playerStatsQuery = (from gs in _context.GameStats
                                    where gs.Player == player
                                    group gs by gs.Player into g
                                    select new
                                    {
                                        Trophies = g.Sum(gs => gs.TotalWins),
                                        AverageElo = g.Average(gs => gs.EloRating),
                                        MostPlayedGame = g.OrderByDescending(gs => gs.Game).FirstOrDefault().Game
                                    }).FirstOrDefault();

            if (playerStatsQuery != null)
            {
                var rank = RankDeterminer.DetermineRank((int)playerStatsQuery.AverageElo);
                return new PlayerStats(player, playerStatsQuery.Trophies, (int)playerStatsQuery.AverageElo, rank, playerStatsQuery.MostPlayedGame);
            }
            else
            {
                return new PlayerStats(player);
            }
        }
    }
}
