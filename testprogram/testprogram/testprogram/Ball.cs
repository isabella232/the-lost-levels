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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ball : Microsoft.Xna.Framework.GameComponent
    {
        Game thisGame;
        public Ball(Game game,Vector2 p,Vector2 v)
            : base(game)
        {
            // TODO: Construct any child components here
            Position = p;
            Velocity = v;
            thisGame = game;


        }
        public Vector2 Position{get; set;}
        public Vector2 Velocity { get; set; }
        
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            
            base.Initialize();
        }


        public void Draw(GameTime gameTime)
        {
            
            
    

        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            Position = Position + Velocity * (gameTime.ElapsedGameTime.Milliseconds);
            base.Update(gameTime);
        }
    }
}
