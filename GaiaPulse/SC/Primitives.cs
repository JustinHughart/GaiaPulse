using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SC
{
    public class Primitives //Draws lines and rectangles onscreen.
    {
        SpriteBatch SpriteBatch; //Sprite batch used to draw primitives.
        Texture2D LineTexture; //Texture used for drawing lines.
        Texture2D RectTexture;  //+Texture used for drawing rects.

        public Primitives(SpriteBatch SpriteBatch, Texture2D LineTexture, Texture2D RectTexture) //Constructor
        {
            this.SpriteBatch = SpriteBatch;
            this.LineTexture = LineTexture;
            this.RectTexture = RectTexture;
        }

        public void DrawLine(Vector2 A, Vector2 B) //Forcibly draw a line from point a to b.
        {
            DrawLine(A, B, Color.White);
        }

        public void DrawLine(Vector2 A, Vector2 B, Color Color) //Forcibly draw a line of a specific color from point a to b.
        {
            DrawLine(A, B, Color, 1f);
        }

        public void DrawLine(Vector2 A, Vector2 B, Color Color, float Layer) //Forcibly draw a line of specific color and layer from point a to b.
        {
            Vector2 origin = new Vector2(0.5f, 0.0f);
            Vector2 difference = B - A;
            Vector2 scale = new Vector2(1.0f, difference.Length() / LineTexture.Height);
            float angle = (float)(Math.Atan2(difference.Y, difference.X)) - MathHelper.PiOver2;

            SpriteBatch.Draw(LineTexture, A, null, Color, angle, origin, scale, SpriteEffects.None, Layer);
        }

        public void DrawRect(Vector2 A, Vector2 B) //Forcibly draw a rect from point a to b.
        {
            DrawRect(A, B, Color.White);
        }

        public void DrawRect(Vector2 A, Vector2 B, Color Color) //Forcibly draw a rect of a specific color from point a to b.
        {
            DrawRect(A, B, Color.White, 1f);
        }

        public void DrawRect(Vector2 A, Vector2 B, Color Color, float Layer) //Forcibly draw a rect of specific color and layer from point a to b.
        {
            Vector2 scale = B - A;

            SpriteBatch.Draw(RectTexture, A, null, Color, 0f, Vector2.Zero, scale, SpriteEffects.None, Layer);
        }
    }
}
