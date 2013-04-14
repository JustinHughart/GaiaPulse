using System;
using Microsoft.Xna.Framework;

namespace GaiaPulse.AnimationManager.DataDevices
{
    [Serializable]
    public class FrameData
    {
        public int FrameNumber { get; private set; } //The number of the frame in the animation.

        public PartTree Grandfather { get; private set; }  //The grandfather.

        public Vector2 FrameOffset { get; private set; } //The offset of the entire frame.

        public void SetFrameNumber(int NewFrameNumber)
        {
            FrameNumber = NewFrameNumber;
        }

        public void SetGrandfather(PartTree NewGrandfather)
        {
            Grandfather = NewGrandfather;
        }

        public void SetFrameOffset(Vector2 NewOffset)
        {
            FrameOffset = NewOffset;
        }
    }
}