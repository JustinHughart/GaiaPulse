using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GaiaPulse.AnimationManager.EditorData
{
    public class Camera
    {
        public Vector2 Position;
        public float Zoom;
        public Vector2 ControlSize;
        public bool IsDragging;

        public Camera(Vector2 controlSize)
        {
            Zoom = 2f;
            ControlSize = controlSize;
            Position = controlSize / 2;
        }

        public Matrix ReturnView()
        {
            return Matrix.CreateTranslation(Position.X, Position.Y, 0) *
                Matrix.CreateTranslation(new Vector3(-ControlSize.X / 2, -ControlSize.Y / 2, 0)) *
                Matrix.CreateScale(Zoom) *
                Matrix.CreateTranslation(new Vector3(ControlSize.X / 2, ControlSize.Y / 2, 0));
        }

        public Matrix GetSizeMatrix()
        {
            return Matrix.CreateScale(Zoom);
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

            if (input.IsKBKeyDown(Keys.Left))
            {
                Position.X -= rate;
            }

            if (input.IsKBKeyDown(Keys.Right))
            {
                Position.X += rate;
            }

            if (input.IsKBKeyDown(Keys.Up))
            {
                Position.Y -= rate;
            }

            if (input.IsKBKeyDown(Keys.Down))
            {
                Position.Y += rate;
            }

            if (input.IsKBKeyDown(Keys.Subtract))
            {
                Zoom = Math.Max(0.1f, Zoom - 0.02f);
            }

            if (input.IsKBKeyDown(Keys.Add))
            {
                Zoom = Math.Min(3f, Zoom + 0.02f);
            }
        }

        public void CheckMouseInput(Input input)
        {
            if (IsDragging)
            {
                if (input.IsMouseKeyReleased(MouseKey.Left))
                {
                    IsDragging = false;
                }
                else
                {
                    Vector2 diff = -input.GetMouseVector();

                    Position += diff;
                }
            }
            else
            {
                if (input.IsMouseKeyPressed(MouseKey.Left))
                {
                    IsDragging = true;
                }
            }
        }
    }
}