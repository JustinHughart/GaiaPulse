using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SC.GaiaPulse
{
    public class Animation
    {
        public int TimingIndex { get; private set; } //The timing index for the animation that determines when it changes frame.
        public int CurrentFrame { get; private set; } //The index of the current frame.
        public bool IsLooping { get; private set; } //Whether or not the animation is supposed to loop.
        public bool IsFinished { get; private set; } //Indicates when either the animation just looped or if it is finished.
        public List<Frame> Frames { get; private set; } //The frames inside the animation.
        public TextureManager TextureManager { get; private set; } //Temporary location of the texture manager.
        
        //!DO NOT LEAVE TEXTURE MANAGER IN HERE, THAT SHOULD BE AN OUTSIDE RESOURCE.
        //!UPDATE TO INCLUDE NONSTATIC ANIMATION SPEEDS

        public Animation() //Constructor, ohboyohboyohboyintializethoselists.
        {
            Frames = new List<Frame>();
            TextureManager = new TextureManager();
        }

        public void SetLooping(bool Looping) //Sets the value of IsLooping.
        {
            IsLooping = Looping;
        }

        public void Update(Vector2 Offset) //Updates the animation. 
        {
            TimingIndex++;
            IsFinished = false;

            if (TimingIndex >= Frames[CurrentFrame].ChangeTime)
            {
                TimingIndex = 0;
                CurrentFrame++;

                if (CurrentFrame >= Frames.Count)
                {
                    if (IsLooping)
                    {
                        CurrentFrame = 0;
                    }
                    else
                    {
                        CurrentFrame--;
                    }

                    IsFinished = true;
                }
            }
            Frames[CurrentFrame].Update(Offset);
        }

        public bool DoesNextFrameExist() //Checks if the next frame exists.
        {
            return CurrentFrame + 1 < Frames.Count;
        }

        public void Draw(SpriteBatch SpriteBatch) //Actually draws the frame.
        {
            Frames[CurrentFrame].Draw(SpriteBatch);
        }

        public void AddFrame(Frame Frame) //Adds a frame to the animation.
        {
            Frame.ChangeFrameNumber(Frames.Count);
            Frame.SetAnim(this);
            Frames.Add(Frame);
        }
    }
}
