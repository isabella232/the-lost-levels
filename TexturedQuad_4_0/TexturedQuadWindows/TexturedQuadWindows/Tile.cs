﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGLibrary;

namespace TexturedQuadWindows
{
    class Tile
    {
        //static public Texture2D TileSetTexture;
        static public int TileWidth = 2;
        static public int TileHeight = 2;

        public int TileID;
        public Quad TileQuad;
        
        static public Rectangle GetSourceRectangle(Vector2 tilePoint)
        {
            int tileX = (int)tilePoint.X * 2;
            int tileY = (int)tilePoint.Y * 2;

            return new Rectangle(tileX, tileY, TileWidth, TileHeight);
        }
        public Tile(Vector3 origin, Vector3 normal, Vector3 up)
        {
            TileQuad = new Quad(origin, normal, up, TileWidth, TileHeight);
        }
        
    }
    
}
