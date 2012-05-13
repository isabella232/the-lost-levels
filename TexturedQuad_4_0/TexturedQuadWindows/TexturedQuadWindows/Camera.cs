using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TexturedQuadWindows
{
    /*Works purely on keyboard input
     * Plan is to change this for mouse control but keyboard for now
     * */
    class Camera
    {

        //my code
        public Vector3 center;
        public float radius;
        public float theta;
        public float phi;
        public float speed;
        private Vector3 Position, Target;
        public Matrix ViewMatrix;
        public Matrix Projection;


        //Camera is simulated as a spherical co-ordinate system looking at  a center 
        //(which is called target). You can modify the phi and theta value using keyboard (check keyboard input)
        
        public Camera()
        {
            ResetCamera();

        }

        
        Vector3 toSpherical(float r, float phi, float theta)
        //given spherical coordinates in radians find the point on x,y,z axes
        {
            return new Vector3((float)(r * Math.Cos(phi) * Math.Cos(theta))
                , (float)(r * Math.Sin(theta))
                , (float)(r * Math.Sin(phi) * Math.Cos(theta)));
        }



        public void ResetCamera()
        //Reset Camera Settings to inital position
        {
            //my code
            center = new Vector3(5, 0, 5);
            radius = 15;
            phi = MathHelper.ToRadians(120);
            theta = MathHelper.ToRadians(70);


            Target = center;
            ViewMatrix = Matrix.CreateLookAt(center + toSpherical(radius, phi, theta), Target, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, (float)3 / 4, 1, 500);

            speed = 0.3f;
        }


        public void UpdateViewMatrix()
        //This view matrix is updated after getting an input from keyboard. 
        //Position is added as the sum of target
        {
            Position = Target + toSpherical(radius, phi, theta);
            ViewMatrix = Matrix.CreateLookAt(Position, Target, Vector3.Up);
        }

        public void Update()
        {
            HandleInput();
            UpdateViewMatrix();
        }

        private void HandleInput()
            /**
             * J and L changes the phi value.
             * I and K changes theta value.
             * Up and down changes the zoom (changes the radius)
             * W,S,A,D to move the camera forward,backward,right and left respectively
             * R resets the camera
           **/
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.J))
            {phi-=0.02f;}

            if(keyboardState.IsKeyDown(Keys.L))
            {phi +=0.02f;}

            if(keyboardState.IsKeyDown(Keys.I))
            {theta -=0.02f;}
            
            if(keyboardState.IsKeyDown(Keys.K))
            {theta +=0.02f;}

            if(keyboardState.IsKeyDown(Keys.Up))
            {radius -=0.07f;}

            if(keyboardState.IsKeyDown(Keys.Down))
            {radius +=0.07f;}
            
            if (keyboardState.IsKeyDown(Keys.W))
            {
                MoveCamera(ViewMatrix.Forward);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                MoveCamera(-ViewMatrix.Forward);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                MoveCamera(-ViewMatrix.Right);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                MoveCamera(ViewMatrix.Right);
            }
                        
            if (keyboardState.IsKeyDown(Keys.R))
            {
                ResetCamera();
            }
        }

        //Changes the camera for forward, backward, sideways movement.
        private void MoveCamera(Vector3 addedVector)
        {
            Position += speed * (addedVector * (new Vector3(0.414f, 0, 0.414f)));
            Target += speed * (addedVector * (new Vector3(0.414f, 0, 0.414f)));
        }
    }
}
