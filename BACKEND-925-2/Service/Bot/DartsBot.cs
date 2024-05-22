using BACKEND_925_2.Models;
using BACKEND_925_2.Service;
using System;
using System.Collections.Generic;
using TwoPlayerGames;
using TwoPlayerGames.Repo;

namespace BACKEND_925_2.Service.Bot
{
    public class DartsBot
    {
        private readonly IGameService gameService;

        private readonly string difficulty;

        private readonly Player2PlayerGame player;

        public DartsBot(string difficulty, Guid gameStateId, Player2PlayerGame player, GamesDbContext gameDbContext)
        {
            gameService = new DartsService(gameStateId, player, new Player2PlayerGame(Guid.NewGuid(), "DartsBot", "Nacho", 89), new DartsRepository(gameDbContext));
            this.difficulty = difficulty;
            this.player = player;
        }

        public override bool Equals(object? obj)
        {
            return obj is DartsBot bot &&
                   EqualityComparer<Player2PlayerGame>.Default.Equals(player, bot.player);
        }
    }
}
