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

namespace testprogram
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ball ball;
        int screenHeight = 480;
        int screenWidth = 640;
        Texture2D ballsprite;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            Content.RootDirectory = "Content";
            ball = new Ball(this, new Vector2(screenWidth/2,screenHeight/2), new Vector2(2,2));
            oldState = Keyboard.GetState();

        }
        KeyboardState oldState;
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ballsprite = Content.Load<Texture2D>("ball");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            if ((ball.Position.X - 16) < 0 || (ball.Position.X + ballsprite.Width / 2 > screenWidth)) 
            {
                float velocityx = ball.Velocity.X;
                ball.Position = new Vector2(ball.Position.X + ballsprite.Width / 2, ball.Position.Y);
                ball.Velocity = new Vector2(-velocityx,ball.Velocity.Y);
            }


            checkFire(gameTime);
            
            if ((ball.Position.Y - ballsprite.Width / 2) < 0 || (ball.Position.Y + ballsprite.Width / 2) > screenHeight)
            {
                float velocityY = ball.Velocity.Y;
                ball.Velocity = new Vector2(ball.Velocity.X, -velocityY);
            }



            ball.Update(gameTime);
            base.Update(gameTime);
        }
        private void checkFire(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown( Keys.Space )&&!oldState.IsKeyDown(Keys.Space))
            {
                Fire();
            }


            oldState = newState;
        }
        private void Fire()
        {
               int i=0;
               System.Console.WriteLine("Pressed");
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

                
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(ballsprite, ball.Position,null, Color.White,0.0f,new Vector2(ballsprite.Width/2,ballsprite.Height/2),new Vector2(1.0f,1.0f),SpriteEffects.None,0.0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private Vector2 ConvertToScreenCoordinates(Vector2 p)
        {
            return new Vector2((int)((screenWidth / 14) * p.X + screenWidth), (int) (screenHeight - (screenHeight * p.Y / 7)));
        }

    }
}
