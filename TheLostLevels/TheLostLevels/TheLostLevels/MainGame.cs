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
using ScreenManager;

namespace TheLostLevels
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        
        MenuComponent menuComponent;
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        int screenHeight = 600;
        int screenWidth = 800;
        public int currentSelectedItem = -1;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferMultiSampling = true;
            this.IsMouseVisible = true;
            
        }
        
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        protected override void Initialize()
        {
            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            string[] menuItems = { "Start Game", "Level Editor", "End Game" };
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            menuComponent = new MenuComponent(this, spriteBatch, Content.Load<SpriteFont>("SpriteFont1"), menuItems);
            Components.Add(menuComponent);
        }

        protected override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
            
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
