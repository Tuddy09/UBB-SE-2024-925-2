using System;
using System.Collections.Generic;
using TwoPlayerGames;

namespace _2PlayerGames.Core
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
