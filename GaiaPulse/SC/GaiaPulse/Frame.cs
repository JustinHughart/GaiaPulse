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

        public Vector2 Offset { get; private set; } //The vector offset of the frame.

        public void SetAnim(Animation Animation)
        {
            this.Animation = Animation;
        }

        public void Update(Vector2 GlobalOffset, Vector2 GlobalScale, float GlobalRotation) //Starts the animation rolling by passing it through the grandfather.
        {
            float PercentThroughFrame = (float)Animation.TimingIndex / (float)ChangeTime;

            Vector2 TimeOffset = Vector2.Zero;

            if (Animation.DoesNextFrameExist())
            {
                Frame NextFrame = Animation.Frames[Animation.CurrentFrame + 1];
                TimeOffset = Offset - NextFrame.Offset;
                TimeOffset *= PercentThroughFrame;
            }
            else
            {
                if (Animation.IsLooping)
                {
                    Frame NextFrame = Animation.Frames[0];
                    TimeOffset = Offset - NextFrame.Offset;
                    TimeOffset *= PercentThroughFrame;
                }
            }

            Grandfather.Update(PercentThroughFrame, GlobalOffset + Offset - TimeOffset, GlobalScale, GlobalRotation);
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

        public void ChangeOffset(Vector2 NewOffset)
        {
            Offset = NewOffset;
        }
    }
}