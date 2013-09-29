using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GaiaPulse.AnimationManager.EditorData
{
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

        public bool IsKeyDown(Keys key)
        {
            return _keyboardstate.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return _keyboardstate.IsKeyUp(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return _keyboardstate.IsKeyDown(key) && _prevkbstate.IsKeyUp(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return _keyboardstate.IsKeyUp(key) && _prevkbstate.IsKeyDown(key);
        }
    }
}