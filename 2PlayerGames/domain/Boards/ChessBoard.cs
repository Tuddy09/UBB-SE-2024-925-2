using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TwoPlayerGames;
using TwoPlayerGames.Domain.DatabaseObjects;
using TwoPlayerGames.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwoPlayerGames
{
    public class ChessBoard : IBoard
    {
        private List<IPiece> chessPieces;
        private int boardWidth;
        private int boardHeight;
        private int[] timers;

        [JsonIgnore]
        public List<IPiece> Board { get => chessPieces; set => chessPieces = value; }

        [JsonProperty]
        private List<ChessPiece> JsonBoard
        {
            get
            {
                List<ChessPiece> chessPieces = new List<ChessPiece>();
                foreach (IPiece piece in this.chessPieces)
                {
                    chessPieces.Add((ChessPiece)piece);
                }
                return chessPieces;
            }
        }
        public int GetWidth { get => boardWidth; set => boardWidth = value; }
        public int GetHeight { get => boardHeight; set => boardHeight = value; }

        public int[] Timers { get => timers; set => timers = value; }

        public ChessBoard(Player[] players, ChessModes mode)
        {
            this.chessPieces = ChessBoard.InitializeBoard(players);
            this.boardWidth = 8;
            this.boardHeight = 8;
            this.timers = new int[2];
            switch (mode)
            {
                case ChessModes.RAPID:
                    this.timers[0] = 600;
                    this.timers[1] = 600;
                    break;
                case ChessModes.BLITZ:
                    this.timers[0] = 300;
                    this.timers[1] = 300;
                    break;
                case ChessModes.BULLET:
                    this.timers[0] = 60;
                    this.timers[1] = 60;
                    break;
            }
        }

        public ChessBoard()
        {
            this.chessPieces = new List<IPiece>();
            this.boardWidth = 8;
            this.boardHeight = 8;
            this.timers = new int[2];
        }

        private static List<IPiece> InitializeBoard(Player[] players)
        {
            List<IPiece> chessPieces = new List<IPiece>();
            chessPieces.Add(new RookPiece(0, 0, players[0]));
            chessPieces.Add(new KnightPiece(1, 0, players[0]));
            chessPieces.Add(new BishopPiece(2, 0, players[0]));
            chessPieces.Add(new QueenPiece(3, 0, players[0]));
            chessPieces.Add(new KingPiece(4, 0, players[0]));
            chessPieces.Add(new BishopPiece(5, 0, players[0]));
            chessPieces.Add(new KnightPiece(6, 0, players[0]));
            chessPieces.Add(new RookPiece(7, 0, players[0]));

            for (int i = 0; i < 8; i++)
            {
                chessPieces.Add(new PawnPiece(i, 1, players[0]));
            }

            chessPieces.Add(new RookPiece(0, 7, players[1]));
            chessPieces.Add(new KnightPiece(1, 7, players[1]));
            chessPieces.Add(new BishopPiece(2, 7, players[1]));
            chessPieces.Add(new QueenPiece(3, 7, players[1]));
            chessPieces.Add(new KingPiece(4, 7, players[1]));
            chessPieces.Add(new BishopPiece(5, 7, players[1]));
            chessPieces.Add(new KnightPiece(6, 7, players[1]));
            chessPieces.Add(new RookPiece(7, 7, players[1]));

            for (int i = 0; i < 8; i++)
            {
                chessPieces.Add(new PawnPiece(i, 6, players[1]));
            }

            return chessPieces;
        }

        public IPiece? GetPiece(int xPosition, int yPosition)
        {
            return chessPieces.FirstOrDefault(piece => piece.XPosition == xPosition && piece.YPosition == yPosition);
        }

        public void AddPiece(IPiece piece)
        {
            this.chessPieces.Add(piece);
        }

        public bool RemovePiece(int xPosition, int yPosition)
        {
            int numberOfPiecesRemoved = chessPieces.RemoveAll(piece => piece.XPosition == xPosition && piece.YPosition == yPosition);
            return numberOfPiecesRemoved > 0;
        }

        public void GetFromJToken(JToken jsonObject)
        {
            List<IPiece> chessPieces = new List<IPiece>();
            foreach (JToken token in jsonObject["JsonBoard"])
            {
                string type = token["PieceType"].ToString();
                switch (type)
                {
                    case "Pawn":
                        chessPieces.Add(token.ToObject<PawnPiece>());
                        break;
                    case "Rook":
                        chessPieces.Add(token.ToObject<RookPiece>());
                        break;
                    case "Knight":
                        chessPieces.Add(token.ToObject<KnightPiece>());
                        break;
                    case "Bishop":
                        chessPieces.Add(token.ToObject<BishopPiece>());
                        break;
                    case "Queen":
                        chessPieces.Add(token.ToObject<QueenPiece>());
                        break;
                    case "King":
                        chessPieces.Add(token.ToObject<KingPiece>());
                        break;
                }
            }
            this.Board = chessPieces;
            this.Timers = jsonObject["Timers"].ToObject<int[]>();
        }
    }
}