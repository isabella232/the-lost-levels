using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheLostLevels
{
   
    
    class TileMap 
    {
        public Tile[,] tiles;
        static public int MapWidth = 50;
        static public int MapHeight = 50;
        
        Game thisGame;
        public TileMap(string MapFilePath, Game g)
        {
            thisGame = g;
            
            Vector3 startPoint = new Vector3(0.5f, 0, 0.5f);

            tiles = new Tile[MapHeight,MapWidth];
            
            Vector3 xoffset = new Vector3(0,0,Tile.TileWidth);
            Vector3 yoffset = new Vector3(Tile.TileHeight,0,0);

            for(int i=0;i<MapWidth;i++)
            {
                for(int j=0;j<MapHeight;j++)
                {
                    tiles[i,j] = new Tile(startPoint + i*xoffset + j*yoffset
                                          ,Vector3.Up
                                          ,-1*Vector3.UnitZ);
                    //TODO: change this part to read from the file
                    tiles[i,j].TileID = 0;
                }
            }
        }

        public static Microsoft.Xna.Framework.Point GetTileIndex(Vector3 point)
        {
            return new Microsoft.Xna.Framework.Point((int)(Math.Floor(point.X)/Tile.TileHeight), (int)(Math.Floor(point.Z)/Tile.TileWidth));
        }

        public void DrawTileMap(BasicEffect effect, GraphicsDevice graphics)
        {
            for (int i = 0; i < MapHeight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {

                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();

                        graphics.DrawUserIndexedPrimitives
                            <VertexPositionNormalTexture>(
                            PrimitiveType.TriangleList,
                            tiles[i, j].TileQuad.Vertices, 0, 4,
                            tiles[i, j].TileQuad.Indexes, 0, 2);
                    }
                }
            }
        }
        
    }
}
