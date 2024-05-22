using TwoPlayerGames;

namespace BACKEND_925_2.Service
{
    public interface IGameService
    {
        IGame Play(int nrParameters, object[] parameters);

        IGame GetGame();

        void SetGame(IGame game);
    }
}
