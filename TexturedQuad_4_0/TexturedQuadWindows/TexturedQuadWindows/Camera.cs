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
    class Camera
    {
        
        private float yaw, pitch, roll;
        private float speed;
        private Matrix cameraRotation;
        private Vector3 Position,Target;
        public Matrix ViewMatrix;

        public Camera()
        {
            ResetCamera();
        }

        public void ResetCamera()
        {
            yaw = 0.0f;
            pitch = 0.0f;
            roll = 0.0f;

            speed = .3f;

            cameraRotation = Matrix.Identity;
            Position = new Vector3(-10, 10, 10);
            Target = Vector3.Zero;
            cameraRotation.Up = Vector3.Up;
            ViewMatrix = Matrix.CreateLookAt(new Vector3(-10, 10, 10), Vector3.Zero, Vector3.Up);

        }


        public void UpdateViewMatrix()
        {

            cameraRotation.Forward.Normalize();
            cameraRotation.Up.Normalize();
            cameraRotation.Right.Normalize();

            cameraRotation *= Matrix.CreateFromAxisAngle(cameraRotation.Right, pitch);
            cameraRotation *= Matrix.CreateFromAxisAngle(cameraRotation.Up, yaw);
            cameraRotation *= Matrix.CreateFromAxisAngle(cameraRotation.Forward, roll);

            yaw = 0.0f;
            pitch = 0.0f;
            roll = 0.0f;

            Target = Position + cameraRotation.Forward;

            ViewMatrix = Matrix.CreateLookAt(Position, Target, cameraRotation.Up);
        }

        public void Update()
        {
            HandleInput();
            UpdateViewMatrix();
        }

        private void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.J))
            {
                yaw += .02f;
            }
            if (keyboardState.IsKeyDown(Keys.L))
            {
                yaw += -.02f;
            }
            if (keyboardState.IsKeyDown(Keys.I))
            {
                pitch += -.02f;
            }
            if (keyboardState.IsKeyDown(Keys.K))
            {
                pitch += .02f;
            }
            
            if (keyboardState.IsKeyDown(Keys.W))
            {
                MoveCamera(cameraRotation.Forward);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                MoveCamera(-cameraRotation.Forward);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                MoveCamera(-cameraRotation.Right);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                MoveCamera(cameraRotation.Right);
            }
            if (keyboardState.IsKeyDown(Keys.E))
            {
                MoveCamera(cameraRotation.Up);
            }
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                MoveCamera(-cameraRotation.Up);
            }
            if(keyboardState.IsKeyDown(Keys.R))
            {
                ResetCamera();
            }
        }

        private void MoveCamera(Vector3 addedVector)
        {
            Position += speed * addedVector;
        }


            
    }
}
