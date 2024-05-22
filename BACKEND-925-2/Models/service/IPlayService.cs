using System;
using System.Collections.Generic;

namespace TwoPlayerGames.Service
{
    internal interface IPlayService
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
