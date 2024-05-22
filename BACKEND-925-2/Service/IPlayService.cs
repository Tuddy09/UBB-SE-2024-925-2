using System;
using System.Collections.Generic;
using TwoPlayerGames;

namespace BACKEND_925_2.Service
{
    public interface IPlayService
    {
        void Play(int numberOfParameters, object[] parameters);
        bool IsGameOver();

        List<IPiece> GetBoard();

        Guid GetTurn();
        void PlayOther();
        Guid? GetWinner();

        Guid StartPlayer();
    }
}
