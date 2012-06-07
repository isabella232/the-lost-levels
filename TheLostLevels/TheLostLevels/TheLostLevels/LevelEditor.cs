#region File Description
//-----------------------------------------------------------------------------
// Game1.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RPGLibrary;

namespace TheLostLevels
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    public class LevelEditor : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int screenHeight = 600;
        int screenWidth = 800;

        LevelGUI form;

        Texture2D texture; //The texture that has ground plan
        List<BasicEffect> Effects; //The list of basic effects that shades objects in the game



        TileMap myMap;

        ////ADD MODELS HERE - declare the models we will load into and give to customModel constructor
        //instead of adding individually declaring the models here we make a list of models to load. 
        //if we keep the name of the model in modelproperties the same as the fbx model name we can load the model directly by placing the modelname in the txt file.
        //that way we dont need declarations like this
        //Model guy;
        //Model tree;
        //Model enemy;
        //Model well;
        ////Model castle;
        List<Model> AvailableModels;


        //declare list to hold a model of each type
        private List<Model> modellist;

        //
        private List<string> modellistnames;

        //dictionary to hold the properties




        public Vector3 pointToput = Vector3.Zero;  //point mouse is over

        public Camera gameCamera;

        private MouseState prevmousestate = Mouse.GetState();    //keeps track of last mouse state

        private KeyboardState prevkeystate = Keyboard.GetState();   //keeps track of last keyboard state

        private CustomModelEditor selectedmodel = null;

        private List<CustomModelEditor> themodels; //models placed on the map

        public enum GameEffect
        { GROUND_PLANE };

        public LevelEditor()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferMultiSampling = true;
            this.IsMouseVisible = true;

            myMap = new TileMap("", this);

            gameCamera = new Camera();



        }

        protected override void Initialize()
        {
            Effects = new List<BasicEffect>();

            themodels = new List<CustomModelEditor>();


            modellist = new List<Model>();

            modellistnames = new List<string>();

            ModelProperties.Initialize(@"Attributes\ModelProperties.txt");
            form = new LevelGUI();
            form.Show();


            //load the properties text file

            base.Initialize();
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("grass_plain");


            Effects.Add(new BasicEffect(graphics.GraphicsDevice));

            //ADD MODELS HERE - this goes through each model listed in the 
            //modelproperties and adds them to the list of models

            foreach (String p in ModelProperties.Properties.Keys)
            {
                modellist.Add(Content.Load<Model>(p));
                modellistnames.Add(p);
            }


            //Setting the world settings
            Effects[(int)GameEffect.GROUND_PLANE].World = Matrix.Identity;
            Effects[(int)GameEffect.GROUND_PLANE].Projection = gameCamera.Projection;
            Effects[(int)GameEffect.GROUND_PLANE].TextureEnabled = true;
            Effects[(int)GameEffect.GROUND_PLANE].Texture = texture;


        }

        protected override void Update(GameTime gameTime)
        {
            Effects[(int)GameEffect.GROUND_PLANE].World = gameCamera.ViewMatrix;



            MouseState st = Mouse.GetState();


            Vector3 dir;

#if WINDOWS
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            //finds out the intersecting point of your left click. for picking

            Vector3 pt1 = new Vector3(st.X, st.Y, 1);
            Vector3 pt2 = new Vector3(st.X, st.Y, 500);
            Vector3 minPointSource = GraphicsDevice.Viewport.Unproject(pt1
                , gameCamera.Projection
                , gameCamera.ViewMatrix, Matrix.Identity);
            Vector3 maxPointsource = GraphicsDevice.Viewport.Unproject(pt2
                , gameCamera.Projection, gameCamera.ViewMatrix, Matrix.Identity);
            dir = maxPointsource - minPointSource;
            dir.Normalize();

            float t = -maxPointsource.Y / dir.Y;
            pointToput = new Vector3(maxPointsource.X + t * dir.X, 0.0f, maxPointsource.Z + t * dir.Z);
            //}


            if ((Keyboard.GetState().IsKeyDown(Keys.D1)) && prevkeystate.IsKeyUp(Keys.D1))//if key 1 has just been pressed
            {
                int s = form.model_index;
                if (selectedmodel != null)//a model has been selected
                {
                    if (selectedmodel.dropped == false) //model has not been dropped
                    {
                        selectedmodel = new CustomModelEditor(this, new Vector3(1, 2, 1), modellist[s], ModelProperties.Properties[modellistnames[s]], modellistnames[s]);
                    }
                    else    //model has been dropped
                    {
                        //add the old selected model to the models on map list
                        themodels.Add(selectedmodel);

                        //make new model and set it as selected
                        selectedmodel = new CustomModelEditor(this, new Vector3(1, 2, 1), modellist[s], ModelProperties.Properties[modellistnames[s]], modellistnames[s]);

                    }
                }
                else    //a model hasn't been selected
                {
                    //make new model and set it as selected
                    selectedmodel = new CustomModelEditor(this, new Vector3(1, 2, 1), modellist[s], ModelProperties.Properties[modellistnames[s]], modellistnames[s]);
                }

            }

            if ((Keyboard.GetState().IsKeyDown(Keys.D2)) && prevkeystate.IsKeyUp(Keys.D2))//if key 2 has just been pressed
            {
                int count = 0;
                foreach (CustomModelEditor c in themodels)
                {
                    Point temptileClicked = TileMap.GetTileIndex(pointToput);

                    if (temptileClicked.Equals(c.tileClicked))
                    //the tile we clicked on is the same tile that the model is on
                    {
                        //delete the model
                        themodels.RemoveAt(count);
                        break;
                    }

                    count++;
                }
            }

            if ((st.LeftButton == ButtonState.Pressed) && (prevmousestate.LeftButton == ButtonState.Released))
            {
                if (selectedmodel != null)
                {
                    if (selectedmodel.dropped == false) //it is not dropped on the ground. i.e. on our cursor
                    {
                        selectedmodel.dropped = true;

                        selectedmodel.tileClicked = TileMap.GetTileIndex(pointToput);

                        Rectangle r =
                            Tile.GetSourceRectangle(new Vector2(selectedmodel.tileClicked.X, selectedmodel.tileClicked.Y));

                        //will be eventually read in from properties file as height the object will be drawn at in y axis (i.e. up)
                        float heightabove = 2;

                        selectedmodel.Position = new Vector3(r.Center.X, heightabove, r.Center.Y);

                    }
                    else //it is dropped on the ground already
                    {
                        Point temptileClicked = TileMap.GetTileIndex(pointToput);

                        if (temptileClicked.Equals(selectedmodel.tileClicked))
                        //the tile we clicked on is the same tile that the model is on
                        {
                            selectedmodel.dropped = false;

                        }
                    }
                }
            }

            prevmousestate = Mouse.GetState();


            //export file
            if (form.export == true)
            {
                form.export = false;

                TextWriter writer = new StreamWriter("level1.txt");

                foreach (CustomModelEditor c in themodels)
                {
                    string exportname = c.modelname;
                    string tposx = c.Position.X.ToString(CultureInfo.InvariantCulture);
                    string tposz = c.Position.Z.ToString(CultureInfo.InvariantCulture);

                    string totalline = exportname + ' ' + tposx + ' ' + tposz;

                    writer.WriteLine(totalline);
                }


                writer.Close();
                //System.IO.File.WriteAllText("level1.txt", totaltext);
            }
            //

#endif



            gameCamera.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (CustomModelEditor c in themodels)
            {
                c.Draw(gameTime, graphics);
            }
            if (selectedmodel != null)
            {
                selectedmodel.Draw(gameTime, graphics);
            }
            //plane.Draw(gameTime, Effects[0],graphics);
            myMap.DrawTileMap(Effects[(int)GameEffect.GROUND_PLANE], graphics.GraphicsDevice);


            base.Draw(gameTime);
        }








    }
}
