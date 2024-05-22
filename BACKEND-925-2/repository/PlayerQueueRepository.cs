using BACKEND_925_2.Models;
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
        private readonly GamesDbContext _context;

        public PlayerQueueRepository(GamesDbContext gamesDbContext)
        {
            _context = gamesDbContext;
        }

        public static PlayerQueueRepository GetInstance(GamesDbContext gamesDbContext)
        {
            if (instance == null)
            {
                instance = new PlayerQueueRepository(gamesDbContext);
            }
            return instance;
        }

        public bool AddPlayer(PlayerQueue player)
        {
            _context.PlayerQueue.Add(player);
            _context.SaveChanges();
            return true;
        }

        public PlayerQueue? GetPlayerByPlayerId(Guid playerId)
        {
            return _context.PlayerQueue.Find(playerId);
        }

        public List<PlayerQueue> GetPlayers()
        {
            List<PlayerQueue> players = new();
            foreach (var player in _context.PlayerQueue)
            {
                players.Add(player);
            }
            return players;
        }

        public void RemovePlayer(PlayerQueue player)
        {
            _context.PlayerQueue.Remove(player);
            _context.SaveChanges();
        }
    }
}
