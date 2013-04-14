using System;
using Microsoft.Xna.Framework.Graphics;

namespace GaiaPulsePreviewer.SC.DeferredDrawingSystem
{
    public class DDSObject : IComparable
    {
        public float Layer { get; protected set; }

        public int CompareTo(object obj)
        {
            int result = 1;

            if (obj != null && obj is DDSObject)
            {
                DDSObject DDSO = obj as DDSObject;
                result = this.Layer.CompareTo(DDSO.Layer);
            }

            return result;
        }

        public virtual void Draw(SpriteBatch SpriteBatch)
        {
        }
    }
}