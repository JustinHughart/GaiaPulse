using System;
using Microsoft.Xna.Framework;


namespace GaiaPulse.PartManager.PartData
{
    [Serializable]
    public class Anchor
    {
        public Vector2 Position { get; private set; }

        public Anchor(Vector2 Position)
        {
            this.Position = Position;
        }

        public void SetPosition(Vector2 Position)
        {
            this.Position = Position;
        }
    }
}
