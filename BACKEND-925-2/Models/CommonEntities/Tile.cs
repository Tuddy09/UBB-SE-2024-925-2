using Board_games.Model.Interfaces;

namespace BoardGames.Model.CommonEntities
{
    public class Tile : ITile
    {
        public int id { get; set; }
        public float centerPositionX { get; set; }
        public float centerPositionY { get; set; }

        public Tile(int tileId, float centerXPosition, float centerYPosition)
        {
            id = tileId;
            centerPositionX = centerXPosition;
            centerPositionY = centerYPosition;
        }
        public float GetCenterXPosition()
        {
            return centerPositionX;
        }
        public float GetCenterYPosition()
        {
            return centerPositionY;
        }
        public int GetTileId()
        {
            return id;
        }
    }
}
