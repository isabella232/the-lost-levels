using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheLostLevels
{
    public class Level :DrawableGameComponent
    {
        GraphicsDeviceManager graphics;

        int screenHeight = 600;
        int screenWidth = 800;

        Texture2D texture; //The texture that has ground plan
        List<BasicEffect> Effects; //The list of basic effects that shades objects in the game



        TileMap myMap;

        //declare here all the different models
        
        
              
        

        public Game1 TheLostLevelsGame;

        private List<CustomModel> TheModels; //models placed on the map

        
        public enum GameEffect
        { GROUND_PLANE };
        public Level(Game1 thisGame)
            : base(thisGame)
        {
            myMap = new TileMap("", thisGame);
            
            TheLostLevelsGame = thisGame;
        }

        public override void Initialize()
        {
            Effects = new List<BasicEffect>();
            TheModels = new List<CustomModel>();
            LoadContent();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = TheLostLevelsGame.Content.Load<Texture2D>("grass_plain");

            Effects.Add(new BasicEffect(TheLostLevelsGame.GraphicsDevice));

        
            Effects[(int)GameEffect.GROUND_PLANE].World = Matrix.Identity;
            Effects[(int)GameEffect.GROUND_PLANE].Projection = TheLostLevelsGame.gameCamera.Projection;
            Effects[(int)GameEffect.GROUND_PLANE].TextureEnabled = true;
            Effects[(int)GameEffect.GROUND_PLANE].Texture = texture;

        }

        public override void Update(GameTime gameTime)
        {
            Effects[(int)GameEffect.GROUND_PLANE].World = TheLostLevelsGame.gameCamera.ViewMatrix;
            MouseState st = Mouse.GetState();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            TheLostLevelsGame.GraphicsDevice.Clear(Color.Black);

            myMap.DrawTileMap(Effects[(int)GameEffect.GROUND_PLANE]
                , TheLostLevelsGame.GraphicsDevice);

            
            base.Draw(gameTime);
        }


    }
}
