using System;
using System.Collections.Generic;
using GaiaPulsePreviewer.SC.DeferredDrawingSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SC.GaiaPulse
{
    public class Part : DDSObject
    {
        public String ID { get; private set; } //The ID of the part.

        public String TextureName { get; private set; } //The name of the texture that the part uses to draw.

        public Rectangle DrawArea { get; private set; } //The draw area of the texture.

        public float Rotation { get; private set; } //The part's rotation.

        public Vector2 Scale { get; private set; } //The non-uniform scale of the part.

        public Color Color { get; private set; } //The coloration of this part.

        public List<Vector2> Anchors { get; private set; } //The anchors of the part.

        public Vector2 DrawPosition { get; private set; } //The position to draw it at.

        public float DrawRotation { get; private set; } //Used for drawing.

        public Vector2 DrawScale { get; private set; } //Used for drawing.

        public List<Vector2> DrawAnchors { get; private set; } //Used for drawing.

        public Color DrawColor { get; private set; } //Used for drawing.

        public Frame Frame { get; private set; } //The frame that the part belongs to.

        public Part Parent { get; private set; } //The part's parent.

        public List<Part> Children { get; private set; } //The children of the part.

        public bool HeadPoint { get; private set; } //Whether or not the part's position is where the head animation should start.

        public bool WeaponOnePoint { get; private set; } //Whether or not the part's postion is where the weapon one animation should start.

        public bool WeaponTwoPoint { get; private set; } //Whether or not the part's position is where the weapon two animation should start.

        public bool FlipHorizontally { get; private set; }

        private SpriteEffects FlipMode;
        private bool Flip;

        public Part() //The constructor! As mandatory as ever~
        {
            Anchors = new List<Vector2>();
            Children = new List<Part>();
        }

        public void SetFrame(Frame Frame) //Sets the frame.
        {
            this.Frame = Frame;
        }

        public void Update(float PercentThroughFrame, Vector2 AnchorOffset, Vector2 GlobalScale, float GlobalRotation) //Updates the part's draw variables.
        {
            if (DetermineFlip())
            {
                FlipMode = SpriteEffects.FlipHorizontally;
                Flip = true;
            }
            else
            {
                FlipMode = SpriteEffects.None;
                Flip = false;
            }

            if (HeadPoint)
            {
                Frame.Animation.EntityFrame.SetHeadPos(AnchorOffset, this);
            }

            if (WeaponOnePoint)
            {
                Frame.Animation.EntityFrame.SetWeaponOnePos(AnchorOffset, this);
            }

            if (WeaponTwoPoint)
            {
                Frame.Animation.EntityFrame.SetWeaponTwoPos(AnchorOffset, this);
            }

            Part NextPart = null;

            if (Frame.Animation.DoesNextFrameExist()) //Find the next part.
            {
                NextPart = Frame.Animation.Frames[Frame.Animation.CurrentFrame + 1].Grandfather.FindPart(ID);
            }
            else
            {
                if (Frame.Animation.IsLooping)
                {
                    NextPart = Frame.Animation.Frames[0].Grandfather.FindPart(ID);
                }
            }

            if (NextPart != null) //If the next part exists, calculate the tweens.
            {
                float RotDiff = Rotation - NextPart.Rotation;
                Vector2 SDiff = Scale - NextPart.Scale;

                float RDiff = Color.R - NextPart.Color.R;
                float GDiff = Color.G - NextPart.Color.G;
                float BDiff = Color.B - NextPart.Color.B;
                float ADiff = Color.A - NextPart.Color.A;

                RotDiff *= PercentThroughFrame;
                SDiff *= PercentThroughFrame;

                RDiff *= PercentThroughFrame;
                GDiff *= PercentThroughFrame;
                BDiff *= PercentThroughFrame;
                ADiff *= PercentThroughFrame;

                int R = (int)(Color.R - RDiff);
                int G = (int)(Color.G - GDiff);
                int B = (int)(Color.B - BDiff);
                int A = (int)(Color.A - ADiff);

                DrawRotation = Rotation - RotDiff;
                DrawScale = Scale - SDiff;
                DrawColor = new Color(R, G, B, A);
            }
            else //Other
            {
                DrawRotation = Rotation;
                DrawScale = Scale;
                DrawColor = Color;
            }

            DrawScale *= GlobalScale;
            //DrawRotation += GlobalRotation;

            if (Parent != null)
            {
                DrawRotation += Parent.DrawRotation;
            }
            else
            {
                if (Frame.Animation.Type == AnimType.Body)
                {
                    DrawRotation += GlobalRotation;
                }

                if (Frame.Animation.Type == AnimType.Head)
                {
                    DrawRotation += Frame.Animation.EntityFrame.HeadPart.DrawRotation;
                }

                if (Frame.Animation.Type == AnimType.WeaponOne)
                {
                    DrawRotation += Frame.Animation.EntityFrame.WeaponOnePart.DrawRotation;
                }

                if (Frame.Animation.Type == AnimType.WeaponTwo)
                {
                    DrawRotation += Frame.Animation.EntityFrame.WeaponTwoPart.DrawRotation;
                }
            }

            Matrix RotationMatrix = Matrix.CreateRotationZ(DrawRotation); //Rotate the part anchors excluding the first one, as the first one is the origin of the part.
            Matrix IRotationMatrix = Matrix.CreateRotationZ(-DrawRotation); //Rotate the part anchors excluding the first one, as the first one is the origin of the part.

            DrawPosition = AnchorOffset - new Vector2(0, 0);

            DrawAnchors = new List<Vector2>();

            DrawAnchors.Add(Anchors[0]);

            for (int i = 1; i < Anchors.Count; i++)
            {
                Vector2 NewAnchor = Vector2.Zero;

                if (Flip)
                {
                    //NewAnchor = ((new Vector2(AnchorOffset.X, AnchorOffset.Y))) +
                    // ((Vector2.Transform(new Vector2(Anchors[i].X, Anchors[i].Y), RotationMatrix))* DrawScale) -
                    //((Vector2.Transform(new Vector2(Anchors[0].X, Anchors[0].Y), RotationMatrix))* DrawScale);
                    //NewAnchor = AnchorOffset + Vector2.Transform(new Vector2(Anchors[i].X, Anchors[i].Y), RotationMatrix) + Vector2.Transform(Anchors[0], IRotationMatrix);
                    NewAnchor = (AnchorOffset + ((Vector2.Transform(new Vector2(DrawArea.Width - Anchors[i].X, Anchors[i].Y), IRotationMatrix) - Vector2.Transform(Anchors[0], IRotationMatrix))) * DrawScale);
                }
                else
                {
                    NewAnchor = (AnchorOffset + ((Vector2.Transform(Anchors[i], RotationMatrix) - Vector2.Transform(Anchors[0], RotationMatrix))) * DrawScale);
                }

                DrawAnchors.Add(NewAnchor);
            }

            for (int i = 0; i < Children.Count; i++) //Update all children using the drawanchors.
            {
                Children[i].Update(PercentThroughFrame, DrawAnchors[i + 1], GlobalScale, 0f);
            }
        }

        public override void Draw(SpriteBatch SpriteBatch) //Draws.
        {
            Texture2D Tex = TextureManager.GetTextureFromFile(TextureName, this);

            if (Flip)
            {
                SpriteBatch.Draw(Tex, DrawPosition, DrawArea, DrawColor, -DrawRotation, Anchors[0], DrawScale, FlipMode, 0f);
            }
            else
            {
                SpriteBatch.Draw(Tex, DrawPosition, DrawArea, DrawColor, DrawRotation, Anchors[0], DrawScale, FlipMode, 0f);
            }
        }

        public void ChangeID(String NewID) //All these change their respective values.
        {
            ID = NewID;
        }

        public void ChangeRotation(float NewRotation)
        {
            Rotation = NewRotation;
        }

        public void ChangeLayerOffset(float NewLayer)
        {
            Layer = NewLayer;
        }

        public void ChangeScale(Vector2 NewScale)
        {
            Scale = NewScale;
        }

        public void ChangeTextureName(String NewName)
        {
            TextureName = NewName;
        }

        public void ChangeDrawArea(Rectangle NewDrawArea)
        {
            DrawArea = NewDrawArea;
        }

        public void ClearAnchors() //Clears the anchors.
        {
            Anchors.Clear();
        }

        public void AddAnchor(Vector2 NewAnchor) //Adds an anchor.
        {
            Anchors.Add(NewAnchor);
        }

        public void SetParent(Part Parent) //Sets the parent.
        {
            this.Parent = Parent;
        }

        public void AddChild(Part Child) //Adds a child.
        {
            Children.Add(Child);
            Child.SetParent(this);
            Child.SetFrame(Frame);
        }

        public void ClearChildren() //Clears the children.
        {
            Children.Clear();
        }

        public void RemoveChild(Part Child) //Removes a child.
        {
            Children.Remove(Child);
        }

        public Part FindPart(String ID) //Returns the first instance of the part.
        {
            Part ReturnPart = null;

            if (this.ID == ID)
            {
                ReturnPart = this;
            }

            int i = 0;

            while (ReturnPart == null && i < Children.Count)
            {
                ReturnPart = Children[i].FindPart(ID);
                i++;
            }

            return ReturnPart;
        }

        public void ChangeColor(Color Color) //Sets color.
        {
            this.Color = Color;
        }

        public void SetHeadPoint(bool Value)
        {
            HeadPoint = Value;
        }

        public void SetWeaponOnePoint(bool Value)
        {
            WeaponOnePoint = Value;
        }

        public void SetWeaponTwoPoint(bool Value)
        {
            WeaponTwoPoint = Value;
        }

        public void ChangeHorizontalFlip(bool NewValue)
        {
            FlipHorizontally = NewValue;
        }

        public bool DetermineFlip()
        {
            bool GlobalFlip = Frame.Animation.EntityFrame.FlipHorizontally;

            if (FlipHorizontally)
            {
                return !GlobalFlip;
            }
            else
            {
                return GlobalFlip;
            }
        }
    }
}