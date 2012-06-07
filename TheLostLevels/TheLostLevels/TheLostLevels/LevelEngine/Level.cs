using System;
using System.Collections.Generic;
using System.IO;
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
       
        Texture2D texture; //The texture that has ground plan
        List<BasicEffect> Effects; //The list of basic effects that shades objects in the game



        TileMap myMap;

        //declare here all the different models
        
        
              
        

        public TheLostLevelsGame TheLostLevelsGame;

        private List<CustomModel> TheModels; //models placed on the map

        
        public enum GameEffect
        { GROUND_PLANE };
        public Level(TheLostLevelsGame thisGame,string LevelFile)
            : base(thisGame)
        {
            myMap = new TileMap("", thisGame);
               
            Effects = new List<BasicEffect>();
            TheModels = new List<CustomModel>();
                
            TheLostLevelsGame = thisGame;
            
        }
        private void LoadLevelFile()
        {
            TextReader reader  = new StreamReader(@"Attributes\level1.txt");
            char[] delimiterChars = {' ','\t'};
            while (reader.Peek() != -1)
            {
                String fileContents = reader.ReadLine();

                if (fileContents != null)
                {
                    String[] words = fileContents.Split(delimiterChars);
                    var onlynumbers = new int[2];
                    int indexnum = -1;

                    foreach (string s in words)
                    {
                        if (indexnum >= 0)
                        {
                            onlynumbers[indexnum] = Convert.ToInt16(s);

                        }
                        indexnum++;
                    }
                    String modelname = words[0];
                    float[] prop;
                    ModelProperties.Properties.TryGetValue(modelname, out prop);
                    Vector2 toPut = new Vector2(onlynumbers[0], onlynumbers[1]);
                    Rectangle srcRectangle = Tile.GetSourceRectangle(toPut);
                    Microsoft.Xna.Framework.Point center = srcRectangle.Center;
                    TheModels.Add(new CustomModel(this
                        , new Vector3(center.X,0,center.Y)
                        , TheLostLevelsGame.Content.Load<Model>(modelname)
                        , prop
                        , modelname));
                }
            }
            reader.Close();
            
        }

        public override void Initialize()
        {
            LoadContent();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadLevelFile(); 
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
            foreach (CustomModel c in TheModels)
            {
                c.Draw(gameTime
                    , TheLostLevelsGame.GraphicsDevice
                    , TheLostLevelsGame.gameCamera,TheLostLevelsGame.celShader);
            }
            
            base.Draw(gameTime);
        }


    }
}
