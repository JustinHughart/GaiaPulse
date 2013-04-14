using System;
using Microsoft.Xna.Framework;

namespace GaiaPulse.AnimationManager.DataDevices
{
    public struct PartTag //The data tag per part. It carries the data of the part, obviously.
    {
        public String PartID; //The ID of the part that the system should load.
        public Vector2 Scale; //The scale of the part in the animation. X and Y scales can be different.
        public Color Color; //The color tint of the part.
        public float Rotation; //The rotation of the part.
        public int Layer; //The layer of the part.
    }
}