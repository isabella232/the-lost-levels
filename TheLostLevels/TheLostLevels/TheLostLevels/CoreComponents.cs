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
using System.Collections.Specialized;

namespace TheLostLevels.Components
{
public class GameScreen : Microsoft.Xna.Framework.DrawableGameComponent
{
private List<GameComponent> childComponents;
public GameScreen(Game game)
: base(game)
{
childComponents = new List<GameComponent>();
Visible = false;
Enabled = false;
}
public List<GameComponent> Components
{
get { return childComponents; }
}
public override void Initialize()
{
    base.Initialize();
}
public override void Update(GameTime gameTime)
{
    foreach (GameComponent child in childComponents)
    {
        if (child.Enabled)
        {
            child.Update(gameTime);
        }
    }
    base.Update(gameTime);
}
public override void Draw(GameTime gameTime)
{
    foreach (GameComponent child in childComponents)
    {
        if ((child is DrawableGameComponent) &&
        ((DrawableGameComponent)child).Visible)
        {
            ((DrawableGameComponent)child).Draw(gameTime);
        }
    }
    base.Draw(gameTime);
}
public virtual void Show()
{
    Visible = true;
    Enabled = true;
}
public virtual void Hide()
{
    Visible = false;
    Enabled = false;
}
}
    public class ButtonMenu : Microsoft.Xna.Framework.DrawableGameComponent
{
SpriteFont spriteFont;
SpriteBatch spriteBatch;
Texture2D buttonImage;
Color normalColor = Color.Black;
Color hiliteColor = Color.White;
KeyboardState oldState, newState;
Vector2 position = new Vector2();
int selectedIndex = 0;
private StringCollection menuItems = new StringCollection();
int width, height;
public ButtonMenu(Game game, SpriteFont spriteFont, Texture2D buttonImage)
: base(game)
{
this.spriteFont = spriteFont;
this.buttonImage = buttonImage;
spriteBatch =
(SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
}
public int Width
{
get { return width; }
    }
public int Height
{
get { return height; }
    }
public int SelectedIndex
{
get { return selectedIndex; }
set
{
selectedIndex = (int)MathHelper.Clamp(
value,
0,
menuItems.Count - 1);
}
}
public Color NormalColor
{
get { return normalColor; }
set { normalColor = value; }
}
public Color HiliteColor
{
get { return hiliteColor; }
set { hiliteColor = value; }
}
public Vector2 Position
{
get { return position; }
set { position = value; }
}
public void SetMenuItems(string[] items)
{
menuItems.Clear();
menuItems.AddRange(items);
CalculateBounds();
}
private void CalculateBounds()
{
width = buttonImage.Width;
height = 0;
foreach (string item in menuItems)
{
Vector2 size = spriteFont.MeasureString(item);
height += 5;
height += buttonImage.Height;
}
}
public override void Initialize()
{
base.Initialize();
}
public override void Update(GameTime gameTime)
{newState = Keyboard.GetState();
if (CheckKey(Keys.Down))
{
selectedIndex++;
if (selectedIndex == menuItems.Count)
selectedIndex = 0;
}
if (CheckKey(Keys.Up))
{
selectedIndex--;
if (selectedIndex == -1)
{
selectedIndex = menuItems.Count - 1;
}
}
oldState = newState;
base.Update(gameTime);
}
public bool CheckKey(Keys theKey)
{
return oldState.IsKeyDown(theKey) && newState.IsKeyUp(theKey);
}
public override void Draw(GameTime gameTime)
{
Vector2 textPosition = Position;
Rectangle buttonRectangle = new Rectangle(
(int)Position.X,
(int)Position.Y,
buttonImage.Width,
buttonImage.Height);
Color myColor;
for (int i = 0; i < menuItems.Count; i++)
{
if (i == SelectedIndex)
myColor = HiliteColor;
else
myColor = NormalColor;
spriteBatch.Draw(buttonImage,
buttonRectangle,
Color.White);
textPosition = new Vector2(
buttonRectangle.X + (buttonImage.Width / 2),
buttonRectangle.Y + (buttonImage.Height / 2));
Vector2 textSize = spriteFont.MeasureString(menuItems[i]);
textPosition.X -= textSize.X / 2;
textPosition.Y -= spriteFont.LineSpacing / 2;
spriteBatch.DrawString(spriteFont,
menuItems[i],
textPosition,
myColor);
buttonRectangle.Y += buttonImage.Height;
buttonRectangle.Y += 5;
}
base.Draw(gameTime);
}
}
    public class BackgroundComponent :
Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D background;
        SpriteBatch spriteBatch = null;
        Rectangle bgRect;
        public BackgroundComponent(Game game, Texture2D texture)
            : base(game)
        {
            this.background = texture;
            spriteBatch =
            (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            bgRect = new Rectangle(0,
            0,
            Game.Window.ClientBounds.Width,
            Game.Window.ClientBounds.Height);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
           
            base.Draw(gameTime);
        }
    }
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
{
SpriteBatch spriteBatch = null;
SpriteFont spriteFont;
Color normalColor = Color.Yellow;
Color hiliteColor = Color.Red;
KeyboardState oldState;
Vector2 position = new Vector2();
int selectedIndex = 0;
private StringCollection menuItems = new StringCollection();
int width, height;
    public MenuComponent(Game game, SpriteFont spriteFont, SpriteBatch stuff)
: base(game)
{
    this.spriteFont = spriteFont;
    spriteBatch = stuff;
}
public int Width
{
get { return width; }
}
        public int Height
{
get { return height; }
}
public int SelectedIndex
{
get { return selectedIndex; }
set
{
selectedIndex = (int)MathHelper.Clamp(
value,
0,
menuItems.Count - 1);
}
}
public Color NormalColor
{
get { return normalColor; }
set { normalColor = value; }
}
public Color HiliteColor
{
get { return hiliteColor; }
set { hiliteColor = value; }
}
public Vector2 Position
{
get { return position; }
set { position = value; }
}
public void SetMenuItems(string[] items)
{
menuItems.Clear();
menuItems.AddRange(items);
CalculateBounds();
}
private void CalculateBounds()
{
width = 0;
height = 0;
foreach (string item in menuItems)
{
Vector2 size = spriteFont.MeasureString(item);
if (size.X > width)
width = (int)size.X;
height += spriteFont.LineSpacing;
}
}
public override void Initialize()
{
base.Initialize();
}
public override void Update(GameTime gameTime)
{
    KeyboardState newState = Keyboard.GetState();
    if (CheckKey(Keys.Down))
    {
        selectedIndex++;
        if (selectedIndex == menuItems.Count)
            selectedIndex = 0;
    }
    if (CheckKey(Keys.Up))
    {
        selectedIndex--;
        if (selectedIndex == -1)
        {
            selectedIndex = menuItems.Count - 1;
        }
    }
    oldState = newState;
    base.Update(gameTime);
}
public bool CheckKey(Keys theKey)
{
    KeyboardState newState = Keyboard.GetState();
    return oldState.IsKeyDown(theKey) && newState.IsKeyUp(theKey);
}
public override void Draw(GameTime gameTime)
{
    Vector2 menuPosition = Position;
    Color myColor;
    for (int i = 0; i < menuItems.Count; i++)
    {
        if (i == SelectedIndex)
            myColor = HiliteColor;
        else
            myColor = NormalColor;
        spriteBatch.DrawString(
        spriteFont,
        menuItems[i],
        menuPosition + Vector2.One,
        Color.Black);
        spriteBatch.DrawString(spriteFont,
        menuItems[i],
        menuPosition,
        myColor);
        menuPosition.Y += spriteFont.LineSpacing;
    }
    base.Draw(gameTime);
}
}
}
