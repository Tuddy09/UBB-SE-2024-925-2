using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGames.Model.CommonEntities;

namespace Board_games.Model.Interfaces
{
    public interface IPawn
    {
        void ChangeTile(Tile tileToChangeTo);
        ITile GetOccupiedTile();
        int GetPawnId();
        Player GetPlayer();
        void SetAssociatedPlayer(Player associatedPlayer);
        Player GetAssociatedPlayer();
    }
}
