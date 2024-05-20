using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_games.Model.Interfaces
{
    public interface ITile
    {
        int GetTileId();
        float GetCenterXPosition();
        float GetCenterYPosition();
    }
}
