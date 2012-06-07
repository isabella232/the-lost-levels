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


        
        public Camera gameCamera;

        public Player Player;
        MainGame thisGame;
        int screenHeight = 600;
        int screenWidth = 800;
        Level CurrentLevel;

        Effect celShader;
        Effect postProcessEffect;

        
        RenderTarget2D sceneRenderTarget;
        RenderTarget2D normalRenderTarget;

        public TheLostLevelsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ModelProperties.Initialize("ModelProperties");
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferMultiSampling = true;
            this.IsMouseVisible = true;
            
            gameCamera = new Camera();
            CurrentLevel = new Level(this,"tutorial level");
            Player = new Player(this, new Vector2(1, 1), CurrentLevel);
           
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
        
        }


        protected override void Update(GameTime gameTime)
        {
            gameCamera.Update();
            CurrentLevel.Update(gameTime);
            Player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            CurrentLevel.Draw(gameTime);
            Player.Draw(gameTime, this.GraphicsDevice, gameCamera);
            base.Draw(gameTime);
        }

        void DrawModel(Matrix world, Matrix view, Matrix projection,

string effectTechniqueName, Model model)
        {

            RenderState renderState = graphics.GraphicsDevice.RenderState;

            renderState.AlphaBlendEnable = false;

            renderState.AlphaTestEnable = false;

            renderState.DepthBufferEnable = true;

            Matrix[] transforms = new Matrix[model.Bones.Count];

            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (Effect effect in mesh.Effects)
                {

                    effect.CurrentTechnique = effect.Techniques[effectTechniqueName];

                    Matrix localWorld = transforms[mesh.ParentBone.Index] * world * Matrix.CreateScale(40.0f);

                    effect.Parameters["world"].SetValue(localWorld);

                    effect.Parameters["view"].SetValue(view);

                    effect.Parameters["projection"].SetValue(projection);

                }

                mesh.Draw();

            }

        }

        void ApplyPostProcess(string effectTechniqueName)
        {

            EffectParameterCollection parameters = postProcessEffect.Parameters;

            Vector2 resolution = new Vector2(sceneRenderTarget.Width,

            sceneRenderTarget.Height);

            Texture2D normalDepthTexture = normalRenderTarget.GetTexture();

            parameters["edgeWidth"].SetValue(1);

            parameters["edgeIntensity"].SetValue(1);

            parameters["screenResolution"].SetValue(resolution);

            parameters["normalDepthTexture"].SetValue(normalDepthTexture);

            postProcessEffect.CurrentTechnique = postProcessEffect.Techniques[effectTechniqueName];

            spriteBatch.Begin(SpriteBlendMode.None,

            SpriteSortMode.Immediate,

            SaveStateMode.None);

            postProcessEffect.Begin();

            postProcessEffect.CurrentTechnique.Passes[0].Begin();

            spriteBatch.Draw(sceneRenderTarget.GetTexture(), Vector2.Zero, Color.White);

            spriteBatch.End();

            postProcessEffect.CurrentTechnique.Passes[0].End();

            postProcessEffect.End();

        }

       
    }
}
