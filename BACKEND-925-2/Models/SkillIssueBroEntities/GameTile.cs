using BoardGames.Model.CommonEntities;

namespace BoardGames.Model.SkillIssueBroEntities
{
    public class GameTile : Tile
    {
        public int tileId { get; set; }
        public int gridRowIndex { get; set; }
        public int gridColumnIndex { get; set; }

        // someone find a solution
        public GameTile(int tileId, int gridRowIndex, int gridColumnIndex) : base(tileId, gridColumnIndex, gridRowIndex)
        {
            this.tileId = tileId;
            this.gridRowIndex = gridRowIndex;
            this.gridColumnIndex = gridColumnIndex;
        }

        public int GetTileId()
        {
            return tileId;
        }

        public int GetGridRowIndex()
        {
            return gridRowIndex;
        }

        public int GetGridColumnInded()
        {
            return gridColumnIndex;
        }
    }
}
