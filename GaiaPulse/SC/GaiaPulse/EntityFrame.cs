using GaiaPulsePreviewer.SC.DeferredDrawingSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SC.GaiaPulse
{
    public class EntityFrame : DDSList
    {
        public Animation Body { get; private set; } //The base animation. Gets run first to determine the place of the rest of the animations.

        public Animation Head { get; private set; }

        public Animation WeaponOne { get; private set; }

        public Animation WeaponTwo { get; private set; }

        public Vector2 HeadVector { get; private set; } //The draw vectors that the animations should start at.

        public Vector2 WeaponOneVector { get; private set; }

        public Vector2 WeaponTwoVector { get; private set; }

        public Part HeadPart; //The parts to be used for the origins of the animations.
        public Part WeaponOnePart;
        public Part WeaponTwoPart;

        public bool FlipHorizontally { get; private set; }

        public bool AnimationChanged;

        public void Update(float AnimSpeed, Vector2 Offset, Vector2 GlobalScale, float GlobalRotation)
        {
            HeadPart = WeaponOnePart = WeaponTwoPart = null;

            if (AnimationChanged)
            {
                PopulateDrawList();
            }

            AnimationChanged = false;

            if (Body != null)
            {
                Body.Update(AnimSpeed, Offset, GlobalScale, GlobalRotation);
            }

            if (Head != null)
            {
                if (HeadPart != null)
                {
                    Head.Update(AnimSpeed, HeadVector, GlobalScale, GlobalRotation);
                }
            }

            if (WeaponOne != null)
            {
                if (WeaponOnePart != null)
                {
                    WeaponOne.Update(AnimSpeed, WeaponOneVector, GlobalScale, GlobalRotation);
                }
            }

            if (WeaponTwo != null)
            {
                if (WeaponTwoPart != null)
                {
                    WeaponTwo.Update(AnimSpeed, WeaponTwoVector, GlobalScale, GlobalRotation);
                }
            }

            if (AnimationChanged)
            {
                PopulateDrawList();
                AnimationChanged = false;
            }
        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            /*if (Body != null)
            {
                Body.Draw(SpriteBatch);
            }

            if (Head != null)
            {
                if (HeadPart != null)
                {
                    Head.Draw(SpriteBatch);
                }
            }

            if (WeaponOne != null)
            {
                if (WeaponOnePart != null)
                {
                    WeaponOne.Draw(SpriteBatch);
                }
            }

            if (WeaponTwo != null)
            {
                if (WeaponTwoPart != null)
                {
                    WeaponTwo.Draw(SpriteBatch);
                }
            }*/

            for (int i = DrawingList.Count - 1; i > -1; i--) //foreach (var ddso in DrawingList)
            {
                DrawingList[i].Draw(SpriteBatch); //ddso.Draw(SpriteBatch);
            }

            //Global.Primitives.DrawRect(new Vector2(MasterRect.X, MasterRect.Y), new Vector2(MasterRect.X + MasterRect.Width, MasterRect.Y + MasterRect.Height));
        }

        public void SetHeadPos(Vector2 NewPos, Part Part)
        {
            HeadVector = NewPos;
            HeadPart = Part;
        }

        public void SetWeaponOnePos(Vector2 NewPos, Part Part)
        {
            WeaponOneVector = NewPos;
            WeaponOnePart = Part;
        }

        public void SetWeaponTwoPos(Vector2 NewPos, Part Part)
        {
            WeaponTwoVector = NewPos;
            WeaponTwoPart = Part;
        }

        public void SetBody(Animation NewAnim)
        {
            Body = NewAnim;
            Body.SetEntityFrame(this);
            Body.SetType(AnimType.Body);
            AnimationChanged = true;
        }

        public void SetHead(Animation NewAnim)
        {
            Head = NewAnim;
            Head.SetEntityFrame(this);
            Head.SetType(AnimType.Head);
            AnimationChanged = true;
        }

        public void SetWeaponOne(Animation NewAnim)
        {
            WeaponOne = NewAnim;
            WeaponOne.SetEntityFrame(this);
            WeaponOne.SetType(AnimType.WeaponOne);
            AnimationChanged = true;
        }

        public void SetWeaponTwo(Animation NewAnim)
        {
            WeaponTwo = NewAnim;
            WeaponTwo.SetEntityFrame(this);
            WeaponTwo.SetType(AnimType.WeaponTwo);
            AnimationChanged = true;
        }

        public void PopulateDrawList()
        {
            ClearDrawList();

            PartSearchAdd(Body.GetCurrentFrame.Grandfather);

            if (HeadPart != null && Head != null)
            {
                PartSearchAdd(Head.GetCurrentFrame.Grandfather);
            }

            if (WeaponOnePart != null && WeaponOne != null)
            {
                PartSearchAdd(WeaponOne.GetCurrentFrame.Grandfather);
            }

            if (WeaponTwoPart != null && WeaponTwo != null)
            {
                PartSearchAdd(WeaponTwo.GetCurrentFrame.Grandfather);
            }

            SortList();
        }

        public void PartSearchAdd(Part Grandfather)
        {
            AddToDrawList(Grandfather);

            foreach (var Child in Grandfather.Children)
            {
                PartSearchAdd(Child);
            }
        }

        public void SetHoriFlip(bool NewValue)
        {
            FlipHorizontally = NewValue;
        }
    }
}