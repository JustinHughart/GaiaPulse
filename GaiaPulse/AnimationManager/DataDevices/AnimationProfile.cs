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

        public void SetID(String NewID)
        {
            ID = NewID;
        }

        public void AddFrame(FrameData NewFrame)
        {
            Frames.Add(NewFrame);
        }
    }
}