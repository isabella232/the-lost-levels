//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using New2DRPG.CoreComponents;
//using New2DRPG.SpriteClasses;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace New2DRPG
//{
//    class CombatScreen : GameScreen
//    {
//        PlayerComponent player;
//        Monster monster;
//        BackgroundComponent background;
//        Vector2 oldPosition;
//        AnimationKey oldAnimation;
//        int screenHeight;
//        int screenWidth;

//        public CombatScreen(Game game)
//            : base(game)
//        {
//            screenHeight = Game.Window.ClientBounds.Height;
//            screenWidth = Game.Window.ClientBounds.Width;
//        }

//        public void Begin() //if need to pass player/ monster add it in.
//        {
//            this.player = player;
//            //this.monster = monster;

//            oldPosition = player.Position;
//            oldAnimation = player.Animation;

//            player.Position = //players starting position

//            monster.CurrentAnimation = AnimationKey.Left;
//            monster.IsAnimating = false;
//            monster.InCombat = true;

//            player.Animation = AnimationKey.Right;
//            player.InCombat = true;
//            player.IsAnimating = false;
//        }

//        public void End()
//        {
//            childComponents.Remove(background);
//            player.InCombat = false;
//            player.Position = oldPosition;
//            player.Animation = oldAnimation;
//        }

//        public override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);
//            player.Update(gameTime);
//            monster.Update(gameTime);
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            base.Draw(gameTime);
//            player.Draw(gameTime);
//            monster.Draw(gameTime);
//        }
//    }
//}
