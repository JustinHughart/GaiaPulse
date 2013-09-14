using Microsoft.Xna.Framework;

namespace GaiaPulse.SC.FrameAnimation
{
    public enum BoundingType
    {
        Body, Attack, Guard,
    }

    public class BoundBox
    {
        public string Group { get; private set; }
        public Rectangle Rect { get; private set; }
        public int LeftOffset { get; private set; }
        public BoundingType BoundType { get; private set; }

        public BoundBox(Rectangle rect, int leftOffset, BoundingType type)
        {
            Group = "";
            Rect = rect;
            LeftOffset = leftOffset;
            BoundType = type;
        }

        public Rectangle GetRect(Vector2 position, bool facingright)
        {
            if (facingright)
            {
                return new Rectangle(Rect.X + (int) position.X, Rect.Y + (int) position.Y, Rect.Width, Rect.Height);
            }
            else
            {
                return new Rectangle((int)LeftOffset + (int)position.X, Rect.Y + (int)position.Y, Rect.Width, Rect.Height);
            }
        }

        public void SetRect(Rectangle rect, int leftoffset)
        {
            Rect = rect;
            LeftOffset = leftoffset;
        }

        public void SetType(BoundingType type)
        {
            BoundType = type;
        }

        public void SetGroup(string group)
        {
            Group = group;
        }
    }
}