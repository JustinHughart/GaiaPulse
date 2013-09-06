using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GaiaPulse.SC
{
    /// <summary>
    /// Draws primitives in XNA using the sprite batcher.
    /// </summary>
    public class Primitives
    {
        /// <summary>
        /// The sprite batch used for drawing.
        /// </summary>
        SpriteBatch _spritebatch;
        /// <summary>
        /// The texture of the line. Should be n*n in size.
        /// </summary>
        Texture2D _lineTexture;
        /// <summary>
        /// The texture for rects. Must be 1*1 in size to work right.
        /// </summary>
        Texture2D _rectTexture;

        /// <summary>
        /// Initializes a new instance of the <see cref="Primitives"/> class.
        /// </summary>
        /// <param name="spritebatch">The sprite batch used for drawing.</param>
        /// <param name="lineTexture">The texture of the line. Should be n*n in size.</param>
        /// <param name="rectTexture">The texture for rects. Must be 1*1 in size to work right.</param>
        public Primitives(SpriteBatch spritebatch, Texture2D lineTexture, Texture2D rectTexture)
        {
            this._spritebatch = spritebatch;
            this._lineTexture = lineTexture;
            this._rectTexture = rectTexture;
        }

        /// <summary>
        /// Draws a line from A to B..
        /// </summary>
        /// <param name="a">The starting point.</param>
        /// <param name="b">The ending point.</param>
        public void DrawLine(Vector2 a, Vector2 b) 
        {
            DrawLine(a, b, Color.Black);
        }

        /// <summary>
        /// Draws a line from A to B..
        /// </summary>
        /// <param name="a">The starting point.</param>
        /// <param name="b">The ending point.</param>
        /// <param name="color">The color of the line.</param>
        public void DrawLine(Vector2 a, Vector2 b, Color color) 
        {
            DrawLine(a, b, color, 0f);
        }

        /// <summary>
        /// Draws a line from A to B..
        /// </summary>
        /// <param name="a">The starting point.</param>
        /// <param name="b">The ending point.</param>
        /// <param name="color">The color of the line.</param>
        /// <param name="layer">The layer to draw the line on.</param>
        public void DrawLine(Vector2 a, Vector2 b, Color color, float layer) 
        {
            Vector2 origin = new Vector2(0.5f, 0.0f);
            Vector2 difference = b - a;
            Vector2 scale = new Vector2(1.0f, difference.Length() / _lineTexture.Height);
            float angle = (float)(Math.Atan2(difference.Y, difference.X)) - MathHelper.PiOver2;

            _spritebatch.Draw(_lineTexture, a, null, color, angle, origin, scale, SpriteEffects.None, layer);
        }

        /// <summary>
        /// Draws a rectangle from point A to B.
        /// </summary>
        /// <param name="a">The upperleft point of the rectangle.</param>
        /// <param name="b">The lowerright point of the rectangle.</param>
        public void DrawRect(Vector2 a, Vector2 b)
        {
            DrawRect(a, b, Color.Black);
        }

        /// <summary>
        /// Draws a rectangle from point A to B.
        /// </summary>
        /// <param name="a">The upperleft point of the rectangle.</param>
        /// <param name="b">The lowerright point of the rectangle.</param>
        /// <param name="color">The color of the rectangle.</param>
        public void DrawRect(Vector2 a, Vector2 b, Color color)
        {
            DrawRect(a, b, color, 0f);
        }

        /// <summary>
        /// Draws a rectangle from point A to B.
        /// </summary>
        /// <param name="a">The upperleft point of the rectangle.</param>
        /// <param name="b">The lowerright point of the rectangle.</param>
        /// <param name="color">The color of the rectangle.</param>
        /// <param name="layer">The layer to draw the rectangle on.</param>
        public void DrawRect(Vector2 a, Vector2 b, Color color, float layer)
        {
            Vector2 scale = b - a;

            _spritebatch.Draw(_rectTexture, a, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, layer);
        }

        /// <summary>
        /// Draws a rectangle from point A to B.
        /// </summary>
        /// <param name="rect">The rectangle to draw.</param>
        /// <param name="color">The color of the rectangle.</param>
        /// <param name="layer">The layer to draw the rectangle on.</param>
        public void DrawRect(Rectangle rect, Color color, float layer)
        {
            Vector2 a = new Vector2(rect.X, rect.Y);
            Vector2 b = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);

            DrawRect(a, b, color, layer);
        }
    }
}