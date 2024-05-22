using TwoPlayerGames;

namespace _2PlayerGames.Core
{
    public interface IGameService
    {
        IGame Play(int nrParameters, object[] parameters);

        IGame GetGame();

        void SetGame(IGame game);
    }
}
