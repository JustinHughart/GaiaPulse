using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SC.GaiaPulse
{
    public class Frame
    {
        public int FrameNumber { get; private set; } //The number of the frame within the animation.
        public int ChangeTime { get; private set; } //The time at which the frame should move to the next frame.
        public Part Grandfather { get; private set; } //The first part in the linked list of parts.
        public Animation Animation { get; private set; } //The animation that this belongs to.

        public void SetAnim(Animation Animation)
        {
            this.Animation = Animation;   
        }

        public void Update(Vector2 Offset) //Starts the animation rolling by passing it through the grandfather.
        {
            float PercentThroughFrame = (float)Animation.TimingIndex / (float)ChangeTime;
            Grandfather.Update(PercentThroughFrame, Offset);
        }

        public void ChangeFrameNumber(int NewNumber) //Change's the frame's number.
        {
            FrameNumber = NewNumber;
        }

        public void ChangeChangeTime(int NewTime) //Changes the change time of the frame.
        {
            ChangeTime = NewTime;
        }

        public void SetGrandfather(Part NewGramps) //Sets the grandfather.
        {
            Grandfather = NewGramps;
            Grandfather.SetFrame(this);
        }

        public void Draw(SpriteBatch SpriteBatch) //Starts the draw by sending it through the grandfather

        {
            Grandfather.Draw(SpriteBatch);
        }
    }
}
