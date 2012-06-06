using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using New2DRPG.CoreComponents;


    class QuitGameScreen : GameScreen
    {
        ButtonMenu menu;
        Texture2D image;
        Texture2D buttonImage;
        SpriteFont spriteFont;
        Vector2 imagePosition;

        public QuitGameScreen(Game game)
            : base(game)
        {
            LoadContent();

            Components.Add(new BackgroundComponent(game, image, false));

            imagePosition = new Vector2();
            imagePosition.X = (game.Window.ClientBounds.Width - image.Width) / 2;
            imagePosition.Y = (game.Window.ClientBounds.Height - image.Height) / 2;

            string[] items = { "QUIT", "CANCEL" };
            menu = new ButtonMenu(game, spriteFont, buttonImage);
            menu.SetMenuItems(items);
            Components.Add(menu);
        }

        public int SelectedIndex
        {
            get { return menu.SelectedIndex; }
        }

        protected override void LoadContent()
        {
            image = Content.Load<Texture2D>();//background image
            buttonImage = Content.Load<Texture2D>();//button image
            spriteFont = Content.Load<SpriteFont>();//font
            base.LoadContent();
        }

        public override void Show()
        {
            base.Show();
            menu.Position = new Vector2((image.Width -
                              menu.Width) / 2 + imagePosition.X,
                              image.Height - menu.Height - 10 + imagePosition.Y);
        }
    }

