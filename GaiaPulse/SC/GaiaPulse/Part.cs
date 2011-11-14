using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SC.GaiaPulse
{
    public class Part
    {
        public String ID { get; private set; } //The ID of the part.
        public String TextureName { get; private set; } //The name of the texture that the part uses to draw.
        public Rectangle DrawArea { get; private set; } //The draw area of the texture.

        public float Rotation { get; private set; } //The part's rotation.
        public float LayerOffset { get; private set; } //The offset of the part's layering.
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

        public Part() //The constructor! As mandatory as ever~
        {
            Anchors = new List<Vector2>();
            Children = new List<Part>();
        }

        public void SetFrame(Frame Frame) //Sets the frame. 
        {
            this.Frame = Frame;
        }

        public void Update(float PercentThroughFrame, Vector2 AnchorOffset) //Updates the part's draw variables.
        {
            Part NextPart = null;

            if (Frame.Animation.DoesNextFrameExist()) //Find the next part.
            {
                NextPart = Frame.Animation.Frames[Frame.Animation.CurrentFrame + 1].Grandfather.FindPart(ID);
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
                DrawColor = new Color(R,G,B,A);
            }
            else //Other
            {
                DrawRotation = Rotation;
                DrawScale = Scale;
                DrawColor = Color;
            }
            
            if (Parent != null)
            {
                DrawRotation += Parent.DrawRotation;
            }

            Matrix RotationMatrix = Matrix.CreateRotationZ(DrawRotation); //Rotate the part anchors excluding the first one, as the first one is the origin of the part.

            DrawPosition = AnchorOffset;

            DrawAnchors = new List<Vector2>();

            DrawAnchors.Add(Anchors[0]);
            
            for (int i = 1; i < Anchors.Count; i++)
            {
                Vector2 NewAnchor = AnchorOffset + ((Vector2.Transform(Anchors[i], RotationMatrix) - Vector2.Transform(Anchors[0], RotationMatrix)) * DrawScale);
                DrawAnchors.Add(NewAnchor);
            }

            for (int i = 0; i < Children.Count; i++) //Update all children using the drawanchors.
            {
                Children[i].Update(PercentThroughFrame, DrawAnchors[i + 1]);
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
            LayerOffset = NewLayer;
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
            }

            return ReturnPart;
        }

        public void SetColor(Color Color) //Sets color.
        {
            this.Color = Color;
        }

        public void Draw(SpriteBatch SpriteBatch) //Draws.
        {
            Texture2D Tex = Frame.Animation.TextureManager.GetTexture(TextureName, this);
            SpriteEffects Eff = SpriteEffects.None; 

            SpriteBatch.Draw(Tex, DrawPosition, DrawArea, DrawColor, DrawRotation, Anchors[0], DrawScale, Eff, LayerOffset);

            foreach (var Child in Children)
            {
                Child.Draw(SpriteBatch);
            }
        }
    }
}
