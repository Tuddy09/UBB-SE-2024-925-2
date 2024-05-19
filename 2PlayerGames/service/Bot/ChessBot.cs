using System;

namespace TwoPlayerGames.Domain.Bot
{
    public class ChessBot : Player
    {
        public ChessBot()
        {
        }

        public new (int nrParameters, object[] parameters) Play(IGame game)
        {
            throw new NotImplementedException();
        }
    }
}
