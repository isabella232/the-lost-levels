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


namespace TheLostLevels
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : DrawableGameComponent
    {
        CustomModel Guy;

        public Point tileClicked;
        public Level CurrentLevel;
        //will be eventually read in from properties file as height the object will be drawn at in y axis (i.e. up)
        public float heightabove = 2;
        private TheLostLevelsGame TheLostLevelsGame;
       // PathFinder PlayerPathFinder;
        public Player(TheLostLevelsGame g, Vector2 position,Level level) : base(g)
     
        {
            Position = position;
            TheLostLevelsGame = g;
            CurrentLevel = level;
            //PlayerPathFinder = new PathFinder();
        }

        
        public Vector2 Position { get; set; }

        public void Draw(GameTime gameTime,GraphicsDevice graphics,Camera gameCamera)
        {
            Guy.Draw(gameTime, graphics, gameCamera,TheLostLevelsGame.celShader);

            base.Draw(gameTime);
        }


        protected override void LoadContent()
        {
            float[] prop = new float[9];
            ModelProperties.Properties.TryGetValue("guy",out prop);
            Rectangle PlayerInitialRectangle
                =Tile.GetSourceRectangle(Position);
            Microsoft.Xna.Framework.Point center = PlayerInitialRectangle.Center;
            
            Guy = new CustomModel(CurrentLevel
                  , new Vector3(center.X,0,center.Y)
                , TheLostLevelsGame.Content.Load<Model>("guy"), prop,"guy");
                
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            LoadContent();
            base.Initialize();
        }

             

        public override void Update(GameTime gameTime)
        {
            MouseState st = Mouse.GetState();


            if (st.RightButton == ButtonState.Pressed)
            {
                Vector3 dir;
                Vector3 pt1 = new Vector3(st.X, st.Y, 1);
                Vector3 pt2 = new Vector3(st.X, st.Y, 500);
                Vector3 minPointSource = GraphicsDevice.Viewport.Unproject(pt1
                    , TheLostLevelsGame.gameCamera.Projection
                    , TheLostLevelsGame.gameCamera.ViewMatrix, Matrix.Identity);

                Vector3 maxPointsource = GraphicsDevice.Viewport.Unproject
                    (pt2
                    , TheLostLevelsGame.gameCamera.Projection
                    , TheLostLevelsGame.gameCamera.ViewMatrix
                    , Matrix.Identity);

                dir = maxPointsource - minPointSource;
                dir.Normalize();

                float t = -maxPointsource.Y / dir.Y;
                Vector3 pointToGo = new Vector3(maxPointsource.X + t * dir.X, 0.0f, maxPointsource.Z + t * dir.Z);
                //TODO: Debug Pathfinder
                //int[,] matrix = new int[TileMap.MapWidth,TileMap.MapHeight];
                
                //Microsoft.Xna.Framework.Point srcTile = TileMap.GetTileIndex(new Vector3(Position.X,0,Position.Y));
                //Microsoft.Xna.Framework.Point destTile = TileMap.GetTileIndex(pointToGo);
                //Point startTile = new Point((int)srcTile.X, (int)srcTile.Y, null);
                //Point endTile = new Point((int)destTile.X,(int)destTile.Y,null);
                //List<Point> pt = PathFinder.findPath(matrix, startTile, endTile);
                Microsoft.Xna.Framework.Point tileOnMap = TileMap.GetTileIndex(pointToGo);
                Rectangle playerNextRectangle = Tile.GetSourceRectangle(new Vector2(tileOnMap.X,tileOnMap.Y));

                Microsoft.Xna.Framework.Point center = playerNextRectangle.Center;
                Position = new Vector2(center.X,center.Y);
                Guy.Position = new Vector3(Position.X, 0, Position.Y);
            }
            base.Update(gameTime);
        }
    }
}
