using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsometricGame
{
    class MapCell
    {
        public int TileID
        {
            get { return BaseTiles.Count > 0 ? BaseTiles[0] : 0; }
            set
            {
                if (BaseTiles.Count > 0)
                    BaseTiles[0] = value;
                else
                    AddBaseTile(value);
            }
        }

        public List<int> BaseTiles;
        public List<int> HeightTiles;


        public MapCell(int tileID)
        {
            BaseTiles = new List<int>();
            HeightTiles = new List<int>();
            TileID = tileID;
            TopperTiles= new List<int>();
        }

        public void AddBaseTile(int tileID)
        {
            BaseTiles.Add(tileID);
        }
        public void AddHeightTile(int tileID)
        {
            HeightTiles.Add(tileID);
        }

        public List<int> TopperTiles;

        public void AddTopperTile(int tileID)
        {
            TopperTiles.Add(tileID);
        }
    }
}
