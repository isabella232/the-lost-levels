using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheLostLevels
{
    /*Works purely on keyboard input
     * Plan is to change this for mouse control but keyboard for now
     * */
    public class Camera
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
        private int scrollValue;
        //Camera is simulated as a spherical co-ordinate system looking at  a center 
        //(which is called target). You can modify the phi and theta value using keyboard (check keyboard input)
        static private Rectangle topRectangle;
        static private Rectangle bottomRectangle;
        static private Rectangle leftRectangle;
        static private Rectangle rightRectangle;
        
        public Camera()
        {
            ResetCamera();
            scrollValue = Mouse.GetState().ScrollWheelValue;

            topRectangle = new Rectangle(0, 0, 800, 5);
            bottomRectangle = new Rectangle(0,595,800,5);
            leftRectangle = new Rectangle(0, 0, 5, 600);
            rightRectangle = new Rectangle(795, 0, 5, 600);
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
            //center = new Vector3(0, 0, 0);
            radius = 15;
            phi = MathHelper.ToRadians(120);
            theta = MathHelper.ToRadians(70);


            Target = center;
            ViewMatrix = Matrix.CreateLookAt(center + toSpherical(radius, phi, theta), Target, Vector3.Up);
            //ViewMatrix = Matrix.CreateLookAt(new Vector3(0, 2, 0), new Vector3(1, 0, 1), Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, (float)3 / 4, 1, 500);

            speed = 0.3f;
            
        }


        public void UpdateViewMatrix()
        //This view matrix is updated after getting an input from keyboard. 
        //Position is added as the sum of target
        {
            Position = Target + toSpherical(radius, phi, theta);
            Target   = clampVector(Target,0);
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
             * Use the mouse to move the camera
             * R resets the camera
           **/
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            Point mousePoint = new Point(mouseState.X,mouseState.Y);
            Vector3 move;




            if (keyboardState.IsKeyDown(Keys.J))
            { theta = MathHelper.Clamp(theta - 0.02f, 0.1f, MathHelper.PiOver2); }
            if (keyboardState.IsKeyDown(Keys.K))
            { theta = MathHelper.Clamp(theta + 0.02f, 0.1f, MathHelper.PiOver2); }

            if(mouseState.MiddleButton==ButtonState.Pressed)
            {phi +=0.02f;}

            if(keyboardState.IsKeyDown(Keys.Space))
            {
                phi += 0.02f;
            }

            if ((scrollValue- mouseState.ScrollWheelValue) < 0)
            { 
                radius -= 0.3f; 
                scrollValue = mouseState.ScrollWheelValue;
            }
            if ((scrollValue - mouseState.ScrollWheelValue) > 0)
            {
                radius += 0.3f;
                scrollValue = mouseState.ScrollWheelValue;
            }


            if ((mouseState.LeftButton != ButtonState.Pressed)
                && (mouseState.X <= 20)
                && (mouseState.X > 0))
            {
                move = Vector3.Cross(Target - Position, Vector3.Up);
                move.Normalize();
                MoveCamera(-move);
            }

            if ((mouseState.LeftButton != ButtonState.Pressed)
                && (mouseState.X > (800 - 20))
                && (mouseState.X < 800))
            {
                move = Vector3.Cross(Target - Position, Vector3.Up);
                move.Normalize();
                MoveCamera(move);
            }

            if ((mouseState.LeftButton != ButtonState.Pressed)
                && (mouseState.Y <= 20)
                && (mouseState.Y > 0))
            {

                move = Target - Position;
                move = new Vector3(move.X, 0, move.Z);
                move.Normalize();
                MoveCamera(move);
            }

            
            if ((mouseState.LeftButton != ButtonState.Pressed)
                && (mouseState.Y > (600 - 20))
                && (mouseState.Y < 600))
            {

                move = Target - Position;
                move = new Vector3(move.X, 0, move.Z);
                move.Normalize();
                MoveCamera(-move);
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
        float myClamp(float value, float min)
        {
            if (value <= min)
                return min;
            else
                return value;
        }

        Vector3 clampVector(Vector3 v, float min)
        {
            return new Vector3(myClamp(v.X, 0), myClamp(v.Y, 0), myClamp(v.Z, 0));
        }
    }
}
