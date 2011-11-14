using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GaiaPulse.PartManager.PartData
{
    [Serializable]
    public class PartSaveLoad
    {
        public String Texture { get; private set; } //The name of the texture from /Textures/ to load.
        public List<Anchor> Anchors { get; private set; } //The anchors that attach to other parts.
        public Rectangle DrawSquare { get; private set; } //The part of the texture that is drawn.
    }
}
