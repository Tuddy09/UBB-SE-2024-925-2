using Board_games.Model.Interfaces;

namespace BoardGames.Model.CommonEntities
{
    public class Tile : ITile
    {
        private int id;
        private float centerPositionX;
        private float centerPositionY;

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
