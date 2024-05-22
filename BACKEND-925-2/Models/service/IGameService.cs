namespace TwoPlayerGames.Service
{
    public interface IGameService
    {
        IGame Play(int nrParameters, object[] parameters);

        IGame GetGame();

        void SetGame(IGame game);
    }
}
