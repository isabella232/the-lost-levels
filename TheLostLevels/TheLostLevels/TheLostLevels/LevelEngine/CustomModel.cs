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
    public class CustomModel : DrawableGameComponent
    {
        Model TheModel;

        float[] Properties;

        //will be eventually read in from properties file as height the object will be drawn at in y axis (i.e. up)
        public String ModelName;
        private Level CurrentLevel;
        //private float[] Properties;
        //constructor will eventually need file name of mesh model to be given as one of the inputs
        public CustomModel(Level l, Vector3 position, Model model,float[] properties,String modelName)   
            : base(l.TheLostLevelsGame)
        {
            Position =position;
            CurrentLevel= l; 
            TheModel = model;
            Properties = properties;
            ModelName = modelName;
        }

        public Vector3 Position { get; set; }

        public void Draw(GameTime gameTime, GraphicsDevice graphics,Camera cam, Effect celEffect,Vector4 diffuseColor)
        {
            if (celEffect==null)
            {
                Matrix[] transforms = new Matrix[TheModel.Bones.Count];
                float aspectRatio = graphics.Viewport.AspectRatio;
                TheModel.CopyAbsoluteBoneTransformsTo(transforms);
                Matrix projection = cam.Projection;
                Matrix view = cam.ViewMatrix;


                foreach (ModelMesh mesh in TheModel.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        
                        effect.EnableDefaultLighting();
                        effect.View = view;
                        effect.Projection = projection;
                        
                        effect.World = transforms[mesh.ParentBone.Index]
                            * Matrix.CreateScale(Properties[0], Properties[1], Properties[2])
                            * Matrix.CreateRotationX(MathHelper.ToRadians(Properties[3]))
                            * Matrix.CreateRotationY(MathHelper.ToRadians(Properties[4]))
                            * Matrix.CreateRotationZ(MathHelper.ToRadians(Properties[5]))
                            * Matrix.CreateTranslation(Position
                                                    + new Vector3(0, 2, 0)
                                                    + new Vector3(Properties[6], Properties[7], Properties[8]));

                    }
                    mesh.Draw();
                }
            }
            else
            {
                Texture2D texture = null;
                int i=0;
                if(ModelName=="tree")
                {
                    diffuseColor = new Vector4(0, 1, 0, 0);
                }
                else if(ModelName == "well" || ModelName == "guy")
                {
                    diffuseColor = new Vector4(0.164f,0.650f,0.800f,0.0f);
                }
                foreach (ModelMesh mesh in TheModel.Meshes)
                {
                                      

                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        
                        Vector3 x = part.Effect.Parameters["SpecularColor"].GetValueVector3();
                       
                        Vector3 y = part.Effect.Parameters["EmissiveColor"].GetValueVector3();
                        Vector4 z = part.Effect.Parameters["DiffuseColor"].GetValueVector4();
                        part.Effect = celEffect;
                        
                        Matrix worldT =mesh.ParentBone.Transform*Matrix.CreateScale(Properties[0], Properties[1], Properties[2])
                            * Matrix.CreateRotationX(MathHelper.ToRadians(Properties[3]))
                            * Matrix.CreateRotationY(MathHelper.ToRadians(Properties[4]))
                            * Matrix.CreateRotationZ(MathHelper.ToRadians(Properties[5]))
                            * Matrix.CreateTranslation(Position + new Vector3(0, 2, 0) + new Vector3(Properties[6], Properties[7], Properties[8]) + new Vector3(-1f, 0f, -1f));
                        celEffect.Parameters["World"].SetValue(worldT);
                        celEffect.Parameters["WorldInverseTranspose"].SetValue((Matrix.Invert(worldT)));
                        celEffect.Parameters["View"].SetValue(cam.ViewMatrix);
                        celEffect.Parameters["Projection"].SetValue(cam.Projection);
                        celEffect.Parameters["DiffuseColor"].SetValue(diffuseColor);
                        celEffect.Parameters["EmissiveColor"].SetValue(y);
                        celEffect.Parameters["SpecularColor"].SetValue(x);
                        celEffect.Parameters["cameraPosition"].SetValue(cam.GetPosition());

                    }
                              
                    
                    mesh.Draw();
                    i++;
                }
            }
            


            base.Draw(gameTime);
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


        protected override void LoadContent()
        {
            // model file name needs to be read in to be given as input here. 

            //
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }




        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        private double timeelapsed = 0;

        public override void Update(GameTime gameTime)
        {
            

            


            base.Update(gameTime);
        }
    }
}
