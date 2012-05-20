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


namespace TexturedQuadWindows
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CustomModel : DrawableGameComponent
    {
        Model themodel;

        public bool dropped = false;

        public Point tileClicked;

        //will be eventually read in from properties file as height the object will be drawn at in y axis (i.e. up)
        public float heightabove = 2;
        private Game1 thisGame;
        //constructor will eventually need file name of mesh model to be given as one of the inputs
        public CustomModel(Game1 g, Vector3 position, Model ourmodel)   
            : base(g)
        {
            Position = position;
            thisGame = g;
            themodel = ourmodel;
           
        }

        public Vector3 Position { get; set; }

        public void Draw(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            var _game = Game as Game1;

            Matrix[] transforms = new Matrix[themodel.Bones.Count];
            float aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            themodel.CopyAbsoluteBoneTransformsTo(transforms);
            Matrix projection = _game.gameCamera.Projection;
            Matrix view = _game.gameCamera.ViewMatrix;

            Vector3 tpos;//position to translate to

            if (dropped == false)
            {
                tpos = _game.pointToput;
            }
            else
            {
                tpos = Position;
            }



            foreach (ModelMesh mesh in themodel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.View = view;
                    effect.Projection = projection;
                    effect.World = transforms[mesh.ParentBone.Index]
                        * Matrix.CreateScale(0.5f, 0.5f, 0.5f)
                        * Matrix.CreateRotationX(MathHelper.ToRadians(-90))
                        * Matrix.CreateTranslation(tpos)
                        ;
                }
                mesh.Draw();
            }


            base.Draw(gameTime);
        }


        protected override void LoadContent()
        {
            // model file name needs to be read in to be given as input here. 

            //
            base.LoadContent();
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


            base.Update(gameTime);
        }
    }
}
