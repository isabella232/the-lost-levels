using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace RPGLibrary
{
    
  

    public class QuadPlane : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Quad[,] quad;
        int span = 100;
        Game whichGame;
        BoundingBox box;

        public QuadPlane(Game game,int height, int width) :base(game)
        {
            whichGame = game;
            // TODO: Construct any child components here
            quad = new Quad[span, span];
            Vector3 xoffset = new Vector3(height, 0, 0);
            Vector3 zoffset = new Vector3(0, 0, width);

            for (int j = 0; j < span; j++)
            {
                for (int i = 0; i < span; i++)
                {
                    quad[i, j] = new Quad(
                        Vector3.Zero + i * xoffset + j * zoffset,
                        Vector3.Up,
                        -1*Vector3.UnitZ,
                        height, width);
                }
            }

            

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }


        public void Draw(GameTime gameTime, BasicEffect planeEffect,GraphicsDeviceManager graphics)
        {
            for (int i = 0; i < span; i++)
            {
                for (int j = 0; j < span; j++)
                {


                    foreach (EffectPass pass in planeEffect.CurrentTechnique.Passes)
                    {
                        pass.Apply();

                        graphics.GraphicsDevice.DrawUserIndexedPrimitives
                            <VertexPositionNormalTexture>(
                            PrimitiveType.TriangleList,
                            quad[i, j].Vertices, 0, 4,
                            quad[i, j].Indexes, 0, 2);

                    }

                }

            }
            base.Draw(gameTime);
        }
    }
}
