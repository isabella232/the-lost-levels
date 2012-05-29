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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;



namespace TheLostLevels
{
    class PlayerComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        PlayerCharacter playerCharacter;
        Game game;
        bool inCombat = false;

        public PlayerComponent(Game game, AnimatedSprite sprite, PlayerCharacter playerCharacter)
            : base(game)
        {
            this.playerCharacter = playerCharacter;
            this.game = game;
        }

        public Vector2 Position
        {
            get { return sprite.Position; }
            set { sprite.Position = value; }
        }

        public AnimationKey Animation
        {
            get { return sprite.CurrentAnimation; }
            set { sprite.CurrentAnimation = value; }
        }


        public bool InCombat
        {
            get { return inCombat; }
            set { inCombat = value; }
        }

        public int SpriteWidth
        {
            get { return sprite.Width; }
        }

        public int SpriteHeight
        {
            get { return sprite.Height; }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            playerCharacter.Update(gameTime);
        }

        public void Show()
        {
            Enabled = true;
            Visible = true;
        }

        public void Hide()
        {
            Enabled = false;
            Visible = false;
        }
    }
}

