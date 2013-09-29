using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GaiaPulse.AnimationManager.EditorData
{
    public enum MouseKey
    {
        Left, Right, Middle,
    }


    public class Input
    {
        Vector2 _windowSize;

        private KeyboardState _keyboardstate;
        private KeyboardState _prevkbstate;

        private MouseState _mousestate;
        private MouseState _prevmousestate;

        public Input(Vector2 windowSize)
        {
            this._windowSize = windowSize;
            _keyboardstate = _prevkbstate = Keyboard.GetState();
            _mousestate = _prevmousestate = Mouse.GetState();
        }

        public void Update()
        {
            _prevkbstate = _keyboardstate;
            _keyboardstate = Keyboard.GetState();
            _prevmousestate = _mousestate;
            _mousestate = Mouse.GetState();
        }

        public bool IsKBKeyDown(Keys key)
        {
            return _keyboardstate.IsKeyDown(key);
        }

        public bool IsKBKeyUp(Keys key)
        {
            return _keyboardstate.IsKeyUp(key);
        }

        public bool IsKBKeyPressed(Keys key)
        {
            return _keyboardstate.IsKeyDown(key) && _prevkbstate.IsKeyUp(key);
        }

        public bool IsKBKeyReleased(Keys key)
        {
            return _keyboardstate.IsKeyUp(key) && _prevkbstate.IsKeyDown(key);
        }

        public bool IsMouseKeyPressed(MouseKey key)
        {
            ButtonState curr = ButtonState.Released;
            ButtonState prev = ButtonState.Released;

            switch (key)
            {
                case MouseKey.Left:
                    curr = _mousestate.LeftButton;
                    prev = _prevmousestate.LeftButton;
                    break;
                case MouseKey.Right:
                    curr = _mousestate.RightButton;
                    prev = _prevmousestate.RightButton;
                    break;
                case MouseKey.Middle:
                    curr = _mousestate.MiddleButton;
                    prev = _prevmousestate.MiddleButton;
                    break;
            }

            return (curr == ButtonState.Pressed && prev == ButtonState.Released);
        }

        public bool IsMouseKeyReleased(MouseKey key)
        {
            ButtonState curr = ButtonState.Released;
            ButtonState prev = ButtonState.Released;

            switch (key)
            {
                case MouseKey.Left:
                    curr = _mousestate.LeftButton;
                    prev = _prevmousestate.LeftButton;
                    break;
                case MouseKey.Right:
                    curr = _mousestate.RightButton;
                    prev = _prevmousestate.RightButton;
                    break;
                case MouseKey.Middle:
                    curr = _mousestate.MiddleButton;
                    prev = _prevmousestate.MiddleButton;
                    break;
            }

            return (curr == ButtonState.Released && prev == ButtonState.Pressed);
        }

        public bool IsMouseKeyDown(MouseKey key)
        {
            switch (key)
            {
                case MouseKey.Left:
                    return _mousestate.LeftButton == ButtonState.Pressed;
                case MouseKey.Right:
                    return _mousestate.RightButton == ButtonState.Pressed;
                case MouseKey.Middle:
                    return _mousestate.MiddleButton == ButtonState.Pressed;
            }

            return false;
        }

        public bool IsMouseKeyUp(MouseKey key)
        {
            switch (key)
            {
                case MouseKey.Left:
                    return _mousestate.LeftButton == ButtonState.Released;
                case MouseKey.Right:
                    return _mousestate.RightButton == ButtonState.Released;
                case MouseKey.Middle:
                    return _mousestate.MiddleButton == ButtonState.Released;
            }

            return false;
        }
    }
}