using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GaiaPulse.AnimationManager.EditorData
{
    public class Input
    {
        Vector2 _windowSize;

        private KeyboardState _keyboardstate;
        private KeyboardState _prevstate;
        
        
        public Input(Vector2 windowSize)
        {
            this._windowSize = windowSize;
            _keyboardstate = _prevstate = Keyboard.GetState();
        }

        public void Update()
        {
            _prevstate = _keyboardstate;
            _keyboardstate = Keyboard.GetState();
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
            return _keyboardstate.IsKeyDown(key) && _prevstate.IsKeyUp(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return _keyboardstate.IsKeyUp(key) && _prevstate.IsKeyDown(key);
        }
    }
}