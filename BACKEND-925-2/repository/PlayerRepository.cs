using BACKEND_925_2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Repo;

namespace TwoPlayerGames.Repository
{
    public class PlayerRepository
    {
        private readonly GamesDbContext _context;
        public PlayerRepository(GamesDbContext gamesDbContext)
        {
            _context = gamesDbContext;
            Players = GetAllPlayers();
            DrawPlayer = GetPlayerById(Guid.Empty);
            AIPlayer = GetPlayerById(Guid.Parse("00000000-0000-0000-0000-000000000001"));
        }
        public static Dictionary<Guid, Player> Players { get; set; }
        public static Player DrawPlayer { get; set; }
        public static Player AIPlayer { get; set; }

        public static Player? GetPlayerById(Guid id)
        {
            return Players.TryGetValue(id, out Player? value) ? value : null;
        }

        public Dictionary<Guid, Player> GetAllPlayers()
        {
            //add from GamesDbContext to players
            Dictionary<Guid, Player> players = new Dictionary<Guid, Player>();
            foreach (var player in _context.Players)
            {
                players.Add(player.Id, player);
            }
            return players;
        }

        public bool AddPlayer(Player player)
        {
            _context.Players.Add(player);
            return _context.SaveChanges() == 1;
        }

        public bool RemoveAddressById(Guid id)
        {
            Player player = GetPlayerById(id);
            if (player == null)
            {
                return false;
            }
            _context.Players.Remove(player);
            return _context.SaveChanges() == 1;
        }

        public  bool UpdatePlayer(Player player)
        {
            Player? playerToUpdate = GetPlayerById(player.Id);
            if (playerToUpdate == null)
            {
                return false;
            }
            playerToUpdate = player;
            return _context.SaveChanges() == 1;
        }

        public static bool RemovePlayerWhereNameEqualsTestOrUpdated()
        {
            Player? player = Players.Values.FirstOrDefault(x => x.Name == "Test" || x.Name == "Updated");
            if (player == null)
            {
                return false;
            }
            Players.Remove(player.Id);
            return true;
        }
    }
}
