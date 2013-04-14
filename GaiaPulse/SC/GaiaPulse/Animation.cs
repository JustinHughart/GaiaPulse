using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SC.GaiaPulse
{
    public enum AnimType
    {
        NULL, Body, Head, WeaponOne, WeaponTwo,
    }

    public class Animation
    {
        public float TimingIndex { get; private set; } //The timing index for the animation that determines when it changes frame.

        public int CurrentFrame { get; private set; } //The index of the current frame.

        public bool IsLooping { get; private set; } //Whether or not the animation is supposed to loop.

        public bool IsFinished { get; private set; } //Indicates when either the animation just looped or if it is finished.

        public List<Frame> Frames { get; private set; } //The frames inside the animation.

        public EntityFrame EntityFrame { get; private set; } //The entityframe that this belongs to.

        public AnimType Type { get; private set; } //The type of animation it is.

        public Animation() //Constructor, ohboyohboyohboyintializethoselists.
        {
            Frames = new List<Frame>();
        }

        public void SetLooping(bool Looping) //Sets the value of IsLooping.
        {
            IsLooping = Looping;
        }

        public void Update(float AnimSpeed, Vector2 Offset, Vector2 GlobalScale, float GlobalRotation) //Updates the animation.
        {
            TimingIndex += AnimSpeed;
            IsFinished = false;

            while (TimingIndex >= Frames[CurrentFrame].ChangeTime)
            {
                TimingIndex -= Frames[CurrentFrame].ChangeTime;
                CurrentFrame++;

                EntityFrame.AnimationChanged = true;

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
            Frames[CurrentFrame].Update(Offset, GlobalScale, GlobalRotation);
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

        public void SetEntityFrame(EntityFrame EntityFrame)
        {
            this.EntityFrame = EntityFrame;
        }

        public void SetType(AnimType NewType)
        {
            Type = NewType;
        }

        public Frame GetCurrentFrame
        {
            get { return Frames[CurrentFrame]; }
        }
    }
}