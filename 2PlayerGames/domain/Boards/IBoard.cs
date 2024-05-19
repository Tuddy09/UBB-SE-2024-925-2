using System;
using System.Collections.Generic;
using TwoPlayerGames;
using Newtonsoft.Json.Linq;

namespace TwoPlayerGames
{
    public interface IBoard
    {
        List<IPiece> Board { get; set; }
        int GetWidth { get; set; }
        int GetHeight { get; set; }

        IPiece? GetPiece(int xPosition, int yPosition);
        void AddPiece(IPiece piece);

        void GetFromJToken(JToken jsonObject);
    }
}

