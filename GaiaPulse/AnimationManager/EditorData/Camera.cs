using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GaiaPulse.AnimationManager.EditorData
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

        public void SetRoomSize(Vector2 roomSize)
        {
            this.RoomSize = roomSize;
            Position = roomSize / 2;
        }

        public Matrix ReturnView()
        {
            return Matrix.CreateTranslation(Position.X, Position.Y, 0) * Matrix.CreateTranslation(new Vector3(-RoomSize.X / 2, -RoomSize.Y / 2, 0)) * Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(new Vector3(RoomSize.X / 2, RoomSize.Y / 2, 0));
        }

        public void MoveCamera(Vector2 velocity)
        {
            Position += velocity;
        }

        public void ChangeZoom(float addedValue)
        {
            Zoom += addedValue;
        }

        public void CheckKeys(Input input)
        {
            float rate = 3;

            if (input.IsKeyDown(Keys.Left))
            {
                Position.X -= rate;
            }

            if (input.IsKeyDown(Keys.Right))
            {
                Position.X += rate;
            }

            if (input.IsKeyDown(Keys.Up))
            {
                Position.Y -= rate;
            }

            if (input.IsKeyDown(Keys.Down))
            {
                Position.Y += rate;
            }

            if (input.IsKeyDown(Keys.Subtract))
            {
                Zoom = Math.Max(0.1f, Zoom - 0.02f);
            }

            if (input.IsKeyDown(Keys.Add))
            {
                Zoom = Math.Min(3f, Zoom + 0.02f);
            }
        }
    }
}