using System;
using System.Collections.Generic;
using GaiaPulse.AnimationManager.DataDevices;

namespace GaiaPulse.AnimationManager
{
    [Serializable]
    public class AnimationProfile
    {
        public String ID { get; private set; }  //The ID of the animation.
        public List<FrameData> Frames { get; private set; }  //The data frames.
        public String Type { get; private set; } //The type of animation it is.

        public void SetID(String NewID)
        {
            ID = NewID;
        }

        public void SetAnimationType(String NewType)
        {
            Type = NewType;
        }

        public void AddFrame(FrameData NewFrame)
        {
            Frames.Add(NewFrame);
        }
    }
}
