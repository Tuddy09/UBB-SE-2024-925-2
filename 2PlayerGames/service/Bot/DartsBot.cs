using System;
using System.Collections.Generic;
using TwoPlayerGames.Repo;
using TwoPlayerGames.Service;

namespace TwoPlayerGames.Domain.Bot
{
    public class DartsBot
    {
        private readonly IGameService gameService;

        private readonly string difficulty;

        private readonly Player player;

        public DartsBot(string difficulty, Guid gameStateId, Player player)
        {
            gameService = new DartsService(gameStateId, player, new Player(Guid.NewGuid(), "DartsBot", "Nacho", 89), new DartsRepository());
            this.difficulty = difficulty;
            this.player = player;
        }

        public override bool Equals(object? obj)
        {
            return obj is DartsBot bot &&
                   EqualityComparer<Player>.Default.Equals(player, bot.player);
        }
    }
}
