using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using New2DRPG.CoreComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


    class StartScreen : GameScreen
    {
        ButtonMenu buttonMenu;
        SpriteFont spriteFont;
        Texture2D background;
        Texture2D buttonImage;

        public StartScreen(Game game) 
            : base(game)
        {
            LoadContent();
            Components.Add(new BackgroundComponent(game, background, true));

            string[] items = { "START", 
                                 "STOP", 
                                 "HELP",  
                                 "QUIT" };

            buttonMenu = new ButtonMenu(
                game,
                spriteFont,
                buttonImage);

            buttonMenu.SetMenuItems(items);
            Components.Add(buttonMenu);
        }

        public int SelectedIndex
        {
            get { return buttonMenu.SelectedIndex; }
        }

        protected override void LoadContent()
        {
            background = Content.Load<Texture2D>();//insert file path
            buttonImage = Content.Load<Texture2D>(); //image 
            spriteFont = Content.Load<SpriteFont>();//font
            base.LoadContent();
        }
        public override void Show()
        {
            buttonMenu.Position = new Vector2((Game.Window.ClientBounds.Width -
                                        buttonMenu.Width) / 2, 450);
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
