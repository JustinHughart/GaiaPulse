using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GaiaPulse.PartManager.EditorData
{
    public class Input
    {
        Vector2 WindowSize;

        public KeyboardState KeyboardState;
        public MouseState MouseState;

        private ButtonState CurrentState;
        private Vector2 CurrentPosition;
        private ButtonState PreviousState;
        private Vector2 PreviousPosition;

        public Input(Vector2 WindowSize)
        {
            this.WindowSize = WindowSize;
        }


        public void Update()
        {
            KeyboardState = Keyboard.GetState();
            MouseState = Mouse.GetState();
            PreviousState = CurrentState;
            PreviousPosition = CurrentPosition;
            CurrentState = MouseState.LeftButton;
            CurrentPosition = new Vector2(MouseState.X, MouseState.Y) + new Vector2(0,-50);
        }

        public bool IsDown(Rectangle Rect)
        {
            bool returnvalue = false;

            if (InsideBounds() == true)
            {
                if (CurrentState == ButtonState.Pressed)
                {
                    if (CheckTouch(Rect) == true)
                    {
                        returnvalue = true;
                    }
                }
            }

            return returnvalue;
        }

        public bool NewPress(Rectangle Rect)
        {
            bool returnvalue = false;

            if (InsideBounds() == true)
            {
                if (CurrentState == ButtonState.Pressed && PreviousState == ButtonState.Released)
                {
                    if (CheckTouch(Rect) == true)
                    {
                        returnvalue = true;
                    }
                }
            }

            return returnvalue;
        }

        public bool Released(Rectangle Rect)
        {
            bool returnvalue = false;

            if (InsideBounds() == true)
            {
                if (CurrentState == ButtonState.Released && PreviousState == ButtonState.Pressed)
                {
                    if (CheckTouch(Rect) == true)
                    {
                        returnvalue = true;
                    }
                }
            }

            return returnvalue;
        }

        public Vector2 GetPosition()
        {
            return CurrentPosition;
        }

        public bool IsDragging()
        {
            bool returnvalue = false;

            if (InsideBounds() == true)
            {
                if (CurrentState == ButtonState.Pressed && PreviousState == ButtonState.Pressed)
                {
                    returnvalue = true;
                }
            }

            return returnvalue;
        }

        public Vector2 GetDrag()
        {
            int X = -(int)(CurrentPosition.X - PreviousPosition.X);
            int Y = -(int)(CurrentPosition.Y - PreviousPosition.Y);

            return new Vector2(X, Y);
        }

        public Vector2 GetCurrentPosition()
        {
            return CurrentPosition;
        }

        public bool CheckTouch(Rectangle Rect)
        {
            bool returnvalue = false;

            if (InsideBounds() == true)
            {
                if (CurrentPosition.X >= Rect.X && CurrentPosition.X <= (Rect.X + Rect.Width) &&
                    CurrentPosition.Y >= Rect.Y && CurrentPosition.Y <= (Rect.Y + Rect.Height))
                {
                    returnvalue = true;
                }
            }

            return returnvalue;
        }

        public bool InsideBounds()
        {
            bool returnvalue = true;

            if (MouseState.X < 0)
            {
                returnvalue = false;
            }

            if (MouseState.Y < 0)
            {
                returnvalue = false;
            }

            if (MouseState.X > WindowSize.X)
            {
                returnvalue = false;
            }

            if (MouseState.Y > WindowSize.Y)
            {
                returnvalue = false;
            }

            return returnvalue;
        }
    }
}

