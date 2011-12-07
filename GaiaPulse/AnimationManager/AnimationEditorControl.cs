using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GaiaPulse.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GaiaPulse.AnimationManager
{
    public class AnimationEditorControl : GraphicsDeviceControl
    {
        ContentManager Content;
        SpriteBatch SpriteBatch;
        SpriteFont Font;

        protected override void Initialize()
        {
            Content = new ContentManager(Services, "CommonData");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Fonts/Courier New");
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            SpriteBatch.DrawString(Font, "It works! Also Syzygy is a balonfgt.", Vector2.Zero, Color.White);
            SpriteBatch.End();
        }

        public override void LogicUpdate()
        {
            
        }
    }
}
