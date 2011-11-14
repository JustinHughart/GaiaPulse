using System;
using System.Collections.Generic;
using GaiaPulse.PartManager.PartData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GaiaPulse.PartManager.EditorData
{
    [Serializable]
    public class TemporaryEditorPart
    {
        public String TextureName { get; private set; } //Name of the texture used to load the part.
        public Rectangle DrawRect { get; private set; } //Drawing area.
        [NonSerialized]
        public Texture2D PartTexture; //Texture used for drawing

        public List<Anchor> Anchors { get; private set; }

        public TemporaryEditorPart()
        {
            DrawRect = Rectangle.Empty;
            Anchors = new List<Anchor>();
        }

        public void DefaultDrawRect()
        {
            DrawRect = new Rectangle(0,0, PartTexture.Width, PartTexture.Height);
        }

        public void SetTextureName(String NewName)
        {
            TextureName = NewName;
        }

        public void SetPartTexture(Texture2D NewTexture)
        {
            PartTexture = NewTexture;
        }

        public void SetDrawRect(Rectangle NewRect)
        {
            DrawRect = NewRect;

            if (DrawRect.Width < 1)
            {
                DrawRect = new Rectangle(DrawRect.X, DrawRect.Y, 1, DrawRect.Height);
            }

            if (DrawRect.Height < 1)
            {
                DrawRect = new Rectangle(DrawRect.X, DrawRect.Y, DrawRect.Width, 1);
            }
        }

        public void SetUpAnchors(int NumAnchors)
        {
            while (Anchors.Count > NumAnchors)
            {
                Anchors.RemoveAt(Anchors.Count-1);
            }

            while (Anchors.Count < NumAnchors)
            {
                Anchors.Add(new Anchor(Vector2.Zero));
            }
        }

       
    }
}
