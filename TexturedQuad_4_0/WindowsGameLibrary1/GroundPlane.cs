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

namespace RPGLibrary
{
    //Draw a ground plane on the x-z plane that will go from  +/- span
    //with grid spacing xygap between lines parallel between x and z axes

    //Direct C# port of Prof Tumblin's drawGndPlane code :)
    public class GroundPlane
    {
        public VertexPositionColor[] Vertices;
        public short[] Indices;

        private float Span;

        private float Xygap;
       
        private Color LineColor;
        private int NumPrimitives;
       

        public GroundPlane(float span, float xygap,Color linecolor)
        {
            Span = span;
            Xygap = xygap;
            LineColor = linecolor;

            Initialize();
         }

        private void Initialize()
        {
            int ij, ijmax;

            if (Xygap <= 0)
            { Xygap = 1.0f; }

            ijmax = (int)(2.0 * Span / Xygap);

            NumPrimitives = 2 * ijmax + 1;
            Vertices = new VertexPositionColor[4 * ijmax + 4];
            Indices = new short[4 * ijmax + 4];
            short count = 0;
            //Draw all the lines parallel to x-axis


            

            for (ij = 0; ij <= 2*ijmax; ij += 4)
            {

                Vertices[ij] = new VertexPositionColor(new Vector3(-Span,0.0f,-Span+ij*Xygap),LineColor);
                
                Indices[ij] = count++;
                
                Vertices[ij + 1] = new VertexPositionColor(new Vector3(2.5f*Span, 0.0f, -Span + ij * Xygap), LineColor);
                Indices[ij + 1] = count++;


                Vertices[ij+2] = new VertexPositionColor(new Vector3(-Span + ij * Xygap, 0.0f, -Span), LineColor);
                Indices[ij+2] = count++;

                Vertices[ij + 3] = new VertexPositionColor(new Vector3(-Span + ij * Xygap, 0.0f, 2.5f*Span), LineColor);
                Indices[ij + 3] = count++;
            
                
            }
            
            
       }

        public int getNumPrimitives()
        { return NumPrimitives; }
        

                

       
    }
}
