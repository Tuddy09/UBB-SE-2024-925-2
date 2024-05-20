namespace BoardGames.Model.CommonEntities;
    using Board_games.Model.Interfaces;

    public class Pawn : IPawn
    {
        private int id;
        private ITile occupiedTile;
        private Player associatedPlayer;

        public Pawn(int pawnId, Tile occupiedTile)
        {
            id = pawnId;
            this.occupiedTile = occupiedTile;
        }

        public Pawn(int pawnId, ITile occupiedTile, Player associatedPlayer)
        {
            id = pawnId;
            this.occupiedTile = occupiedTile;
            this.associatedPlayer = associatedPlayer;
        }

        public void ChangeTile(Tile tileToChangeTo)
        {
            occupiedTile = tileToChangeTo;
        }
        public ITile GetOccupiedTile()
        {
            return occupiedTile;
        }
        public int GetPawnId()
        {
            return id;
        }
        public Player GetPlayer()
        {
            return associatedPlayer;
        }

        public void SetAssociatedPlayer(Player associatedPlayer)
        {
            this.associatedPlayer = associatedPlayer;
        }

        public Player GetAssociatedPlayer()
        {
            return associatedPlayer;
        }
    }