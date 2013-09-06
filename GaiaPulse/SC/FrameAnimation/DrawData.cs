using System;
using System.Collections.Generic;
using GaiaPulse.SC.FBFAnimation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GaiaPulse.SC.FrameAnimation
{
    /// <summary>
    /// The draw data for the frames.
    /// </summary>
    public class DrawData
    {
        /// <summary>
        /// The ID of the frame.
        /// </summary>
        public String ID;

        /// <summary>
        /// The texture's name
        /// </summary>
        public String TextureName;

        /// <summary>
        /// The loaded texture.
        /// </summary>
        /// <value>
        /// The texture.
        /// </value>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// The area that will be drawn.
        /// </summary>
        public Rectangle DrawArea;

        /// <summary>
        /// The offsets for the animation frame. X -= X offset facing right, Y = Y offset, Z = X Offset facing left.
        /// </summary>
        public Vector3 Offsets; 

        /// <summary>
        /// The origin of the frame.
        /// </summary>
        public Vector2 Origin;

        /// <summary>
        /// The bounding boxes for the frame.
        /// </summary>
        private List<BoundBox> _boundingboxes = new List<BoundBox>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawData"/> class.
        /// </summary>
        /// <param name="id">The draw data's ID..</param>
        /// <param name="texturename">The texture's name.</param>
        /// <param name="drawarea">The area of the texture that will be drawn.</param>
        /// <param name="offsets">The offsets.</param>
        /// <param name="origin">The origin.</param>
        public DrawData(String id, String texturename, Rectangle drawarea, Vector3 offsets, Vector2 origin)
        {
            ID = id;
            TextureName = texturename;
            DrawArea = drawarea;
            Offsets = offsets;
            Origin = origin;
        }

        /// <summary>
        /// Draws the animation. Does not draw the additional data such as hitboxes.
        /// </summary>
        /// <param name="spritebatch">The sprite batch that will be used for drawing..</param>
        /// <param name="position">The position.</param>
        /// <param name="facingright">if set to <c>true</c> [facing right].</param>
        /// <param name="scale">The scale of the frame.</param>
        /// <param name="rotation">The rotation of the frame.</param>
        public void Draw(SpriteBatch spritebatch, Vector2 position, bool facingright, Vector2 scale, float rotation)
        {
            CheckTexture();

            if (Texture != null)
            {
                Vector2 drawPosition = Vector2.Zero;

                if (facingright)
                {
                    drawPosition = position;

                    spritebatch.Draw(Texture, drawPosition - new Vector2(Offsets.X, Offsets.Y), DrawArea, Color.White, rotation, Origin, scale, SpriteEffects.None, 0f);
                }
                else
                {
                    drawPosition = position;

                    spritebatch.Draw(Texture, drawPosition - new Vector2(Offsets.Z, Offsets.Y), DrawArea, Color.White, rotation, Origin, scale, SpriteEffects.FlipHorizontally, 0f);
                }
            }
        }

        /// <summary>
        /// Checks the texture. If it needs to load it, loads it.
        /// </summary>
        private void CheckTexture()
        {
            if (Texture != null)
            {
                if (Texture.Name != TextureName)
                {
                    LoadTexture();
                }
            }
            else
            {
                LoadTexture();
            }
        }

        /// <summary>
        /// Loads the texture.
        /// </summary>
        private void LoadTexture()
        {
            if (TextureName != "")
            {
                Texture2D tex = TextureManager.GetTexture(TextureName);
                tex.Name = TextureName;
                Texture = tex;
            }
        }

        /// <summary>
        /// Adds a bounding box to the list.
        /// </summary>
        /// <param name="box">The box to add.</param>
        public void AddBoundingBox(BoundBox box)
        {
            _boundingboxes.Add(box);
        }

        /// <summary>
        /// Gets the bounding boxes.
        /// </summary>
        /// <returns></returns>
        public List<BoundBox> GetBoxes()
        {
            return _boundingboxes;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ID;
        }
    }
}
