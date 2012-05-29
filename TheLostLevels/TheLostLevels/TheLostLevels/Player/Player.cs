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


namespace TheLostLevels
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : DrawableGameComponent
    {
        CustomModel Guy;

        public Point tileClicked;
        public Level CurrentLevel;
        //will be eventually read in from properties file as height the object will be drawn at in y axis (i.e. up)
        public float heightabove = 2;
        private Game1 TheLostLevelsGame;
        public Player(Game1 g, Vector2 position,Level level)
            : base(g)
        {
            Position = position;
            TheLostLevelsGame = g;
            CurrentLevel = level;
            
        }

        
        public Vector2 Position { get; set; }

        public void Draw(GameTime gameTime,GraphicsDevice graphics,Camera gameCamera)
        {
            Guy.Draw(gameTime, graphics, gameCamera);

            base.Draw(gameTime);
        }


        protected override void LoadContent()
        {
            float[] prop = new float[9];
            ModelProperties.Properties.TryGetValue("guy",out prop);
            Rectangle PlayerInitialRectangle
                =Tile.GetSourceRectangle(Position);
            Microsoft.Xna.Framework.Point center = PlayerInitialRectangle.Center;
            
            Guy = new CustomModel(CurrentLevel
                  , new Vector3(center.X,0,center.Y)
                , TheLostLevelsGame.Content.Load<Model>("guy"), prop,"guy");
                
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            LoadContent();
            base.Initialize();
        }

             

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }
    }
}
