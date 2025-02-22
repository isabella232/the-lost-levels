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

    public class TheLostLevelsGame : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;

        private Model testModel;

        StartScreen startScreen;
        SpriteFont font1;
        Texture2D background;
        SpriteBatch beginGame;

        public Camera gameCamera;

        public Player Player;
        public Effect celShader;
        public int screenHeight = 600;
        public int screenWidth = 800;
        Level CurrentLevel;

        public bool exitgame = false;

        public TheLostLevelsGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";



            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferMultiSampling = true;
            this.IsMouseVisible = true;
            ModelProperties.Initialize("ModelProperties");
            gameCamera = new Camera();
            CurrentLevel = new Level(this, "tutorial level");

            Player = new Player(this, new Vector2(4, 4), CurrentLevel);



        }

        protected override void Initialize()
        {
            CurrentLevel.Initialize();
            Player.Initialize();

            base.Initialize();
        }


        //TiffImporter tiff;
        protected override void LoadContent()
        {
            beginGame = new SpriteBatch(GraphicsDevice);


            font1 = Content.Load<SpriteFont>("SpriteFont1");
            background = Content.Load<Texture2D>("grass");
            celShader = Content.Load<Effect>("Shader Effects/Toon");
            startScreen = new StartScreen(this, font1, background, beginGame);
            Components.Add(startScreen);


            // startScreen.Show();
            testModel = Content.Load<Model>("wolf");
        }

        protected override void Update(GameTime gameTime)
        {
            gameCamera.Update();
            CurrentLevel.Update(gameTime);
            Player.Update(gameTime);

            bool playerfound = false;


            Vector3 playerpos = new Vector3(0, 0, 0);



            foreach (var x in CurrentLevel.TheModels)
            {
                if (x.ModelName == "wolf")
                {
                    Vector3 tempVector = new Vector3(Player.Position.X, 0, Player.Position.Y);
                    if (x.Position == tempVector)
                    {
                        exitgame = true;

                        this.Exit();
                    }
                }

            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            CurrentLevel.Draw(gameTime);
            Player.Draw(gameTime, this.GraphicsDevice, gameCamera);


            base.Draw(gameTime);

        }


    }
}
