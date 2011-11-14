using System;
using System.IO;
using System.Windows.Forms;
using GaiaPulse.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SC;

namespace GaiaPulse.PartManager.EditorData
{
    public class Camera
    {
        public Vector2 Position;
        public float Zoom;
        public Vector2 RoomSize;

        public Camera()
        {
            Position = Vector2.Zero;
            Zoom = 1f;
        }

        public void SetRoomSize(Vector2 RoomSize)
        {
            this.RoomSize = RoomSize;
            Position = RoomSize / 2;
        }

        public Matrix ReturnView()
        {
            return Matrix.CreateTranslation(Position.X, Position.Y, 0) * Matrix.CreateTranslation(new Vector3(-RoomSize.X / 2, -RoomSize.Y / 2, 0)) * Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(new Vector3(RoomSize.X/2, RoomSize.Y/2, 0));
        }

        public void MoveCamera(Vector2 Velocity)
        {
            Position += Velocity;
        }

        public void ChangeZoom(float AddedValue)
        {
            Zoom += AddedValue;
        }

    }
}
