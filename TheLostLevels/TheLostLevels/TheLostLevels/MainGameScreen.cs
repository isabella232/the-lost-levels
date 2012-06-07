using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;
using TheLostLevels.ScreenManager;

namespace TheLostLevels.GameScreens
{

    public class MainGameScreen : GameScreen
    {
        public override bool isPaused
        {
            get { return paused; }
            set { paused = value; }
        }
        private bool paused;

        GraphicsDevice graphicsDevice;
        KeyboardState keyboardState;

        private Level level;
        private Player player;

        public override void HandleInput(InputState input)
        {
            if (input.IsPause() && isPaused == false)
            {
                //ScreenManager.AddScreen(new PauseScreen(this));
                isPaused = true;
            }

        }

        public void UnpauseEvent(object sender, EventArgs e)
        {
            isPaused = false;
        }

        public override void LoadContent()
        {
            
            graphicsDevice = this.ScreenManager.GraphicsDevice;

            // TODO: use this.Content to load your game content here
            LoadLevel();

           // player = new Player();
            keyboardState = Keyboard.GetState();
        }

        private void LoadLevel()
        {
            // Unloads the content for the current level before loading the next one.
            if (level != null)
                level.Dispose();

            // Load the level.
            //level = new Level();
           // level.LoadContent();
        }

        public override void Update(GameTime gameTime, bool hasFocus, bool isCovered)
        {
            if (isPaused == false)
            {
               // level.Update();
              //  player.Update();
            }

        }

        public override void Draw(GameTime gameTime)
        {
            level.Draw(gameTime);

            player.Draw(gameTime);

            base.Draw(gameTime);
        }

        
        public override void UnloadContent()
        {
            base.UnloadContent();
            ScreenManager.Game.Content.Unload();
        }
    }
}
