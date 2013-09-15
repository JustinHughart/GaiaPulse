using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GaiaPulse.SC.FrameAnimation
{
    public class AnimNode //The node that holds all the frame data.
    {
        //Owner
        FrameAnimation _owner;

        //Animation Data
        public DrawData DrawData { get; private set; } //The draw data
        public int TimeTillNext { get; private set; } //The time until it changes.

        //Velocity
        public Vector2 Velocity { get; private set; }
        public bool SmoothX { get; private set; }
        public bool SmoothY { get; private set; }
        
        //Rotation
        public float Rotation { get; private set; }
        public bool SmoothRotation { get; private set; }

        //Custom XML
        public XElement CustomXML;

        public AnimNode(FrameAnimation owner, int timeTillNext) //Constructor
        {
            _owner = owner;
            TimeTillNext = timeTillNext;
            CustomXML = new XElement("customxml");
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position, bool facingRight, Vector2 scale, float rotation)
        {
            DrawData.Draw(spriteBatch, position, facingRight, scale, rotation);
        }
                
        public List<BoundBox> GetBoxes()
        {
            return DrawData.GetBoxes();
        }

        public void SetVelocity(Vector2 velocity, bool smoothx, bool smoothy)
        {
            Velocity = velocity;
            SmoothX = smoothx;
            SmoothY = smoothy;
        }

        public void SetRotation(float rotation, bool smoothrotation)
        {
            Rotation = rotation;
            SmoothRotation = smoothrotation;
        }

        public void SetTTN(int ttn)
        {
            TimeTillNext = ttn;
        }

        public void SetDrawData(DrawData dd)
        {
            DrawData = dd;
        }

        public override string ToString()
        {
            return DrawData.ToString();
        }
    }
}