using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsometricGame
{
    class MapRow
    {
        
        public List<MapCell> Columns;

        public MapRow()
        {
            Columns = new List<MapCell>();
        }
        
    }
}
