using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using GaiaPulse.AnimationManager.EditorData;
using GaiaPulse.SC;
using GaiaPulse.SC.FrameAnimation;
using GaiaPulse.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace GaiaPulse.AnimationManager
{
    public enum EditorModeState
    {
        Camera, SetDrawArea, SetOrigin, SetOffsets, SetHitboxes, Overview,
    }

    public class EditorControl : GraphicsDeviceControl
    {
        ContentManager _content;
        SpriteBatch _spritebatch;
        SpriteFont _font;

        Camera _camera;
        Input _input;

        AnimationEditor _winForm;

        Texture2D _pointTexture;
        Texture2D _lineTexture;

        Primitives _primitives;

        public EditorModeState State;
        
        public Stream Stream;

        String _animID;

        public FrameAnimation Anim;

        public List<DrawData> Frames;
        int _currframe;

        bool _facingright = true;

        Vector2 _position = new Vector2(0);

        bool _showlegend = false;

        int _currhitbox = 0;

        protected override void Initialize()
        {
            _content = new ContentManager(Services, "CommonData");
            _spritebatch = new SpriteBatch(GraphicsDevice);
            _font = _content.Load<SpriteFont>("Fonts/Courier New");
            _camera = new Camera();
            
            _pointTexture = _content.Load<Texture2D>("pixelmarker");
            _lineTexture = _content.Load<Texture2D>("1x1");

            _primitives = new Primitives(_spritebatch, _lineTexture, _lineTexture);
            _input = new Input(new Vector2(_winForm.Width, _winForm.Height));
            _camera.SetRoomSize(new Vector2(_winForm.Width, _winForm.Height));

            TextureManager.Initialize(GraphicsDevice);

            Frames = new List<DrawData>();

            if (_winForm.SavePath == "New Animation")
            {
                Anim = new FrameAnimation("", false);
            }
            else
            {
                LoadAnim();   
            }
            
            Application.Idle += delegate { Invalidate(); };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _content.Unload();
            }

            base.Dispose(disposing);
        }

        protected override void Draw()
        {
            CheckEmptyFrame();

            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spritebatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, _camera.ReturnView());
            CurrFrame().Draw(_spritebatch, _position, _facingright, new Vector2(1), 0);

            switch (State)
            {
                case EditorModeState.Camera:
                    break;
                case EditorModeState.SetDrawArea:
                    DrawDrawArea();
                    break;
                case EditorModeState.SetOrigin:
                    DrawOrigin();
                    break;
                case EditorModeState.SetOffsets:
                    DrawOffsets();
                    break;
                case EditorModeState.SetHitboxes:
                    DrawHitboxes();
                    break;
                case EditorModeState.Overview:
                    DrawOverview();
                    break;
            }

            _spritebatch.End();

            _spritebatch.Begin();
            DrawHelpText();
            _spritebatch.End();
        }

        private void DrawHelpText()
        {
            _spritebatch.DrawString(_font, "FPS: " + CurrentFPS + " Frame: " + _currframe + " Mode State: " + State + " Press F12 to toggle legend mode.", new Vector2(0, 0), Color.White);

            int numlines = 1;

            switch (State)
            {
                case EditorModeState.Camera:
                    _spritebatch.DrawString(_font, "Camera Position: " + _camera.Position + " Camera Zoom: " + _camera.Zoom, new Vector2(0, numlines * 32), Color.White);

                    numlines++;
                    numlines++;

                    if (_showlegend)
                    {
                        _spritebatch.DrawString(_font, "Up: Camera Y -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Down: Camera Y +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;
                        
                        _spritebatch.DrawString(_font, "Left: Camera X -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Right: Camera X +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "+: Camera Zoom +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "-: Camera Zoom -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;
                    }

                    break;
                case EditorModeState.SetDrawArea:
                    _spritebatch.DrawString(_font, "Draw Area: " + CurrFrame().DrawArea, new Vector2(0, numlines * 32), Color.White);

                    numlines++;
                    numlines++;

                    if (_showlegend)
                    {
                        _spritebatch.DrawString(_font, "Up: Draw Area Y -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Down: Draw Area Y +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Left: Draw Area  X -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Right: Draw Area  X +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Num Up: Draw Area Height -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Num Down: Draw Area Height +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Num Left: Draw Area  Width -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Num Right: Draw Area  Width +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;
                    }

                    break;
                case EditorModeState.SetOrigin:
                    _spritebatch.DrawString(_font, "Origin: " + CurrFrame().Origin, new Vector2(0, numlines * 32), Color.White);

                    numlines++;
                    numlines++;

                    if (_showlegend)
                    {
                        _spritebatch.DrawString(_font, "Up: Origin Y -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Down: Origin Y +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Left: Origin  X -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Right: Origin  X +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;
                    }

                    break;
                case EditorModeState.SetOffsets:
                    _spritebatch.DrawString(_font, "Offsets: " + CurrFrame().Offsets, new Vector2(0, numlines * 32), Color.White);

                    numlines++;
                    numlines++;

                    if (_showlegend)
                    {
                        _spritebatch.DrawString(_font, "Up: Offset Y -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Down: Offset Y +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Left: Offset  X -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Right: Offset  X +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;
                    }

                    break;
                case EditorModeState.SetHitboxes:
                    BoundBox box = CurrHitbox();

                    String boxdata = "";

                    if (box != null)
                    {
                        boxdata = "Group ID: " + box.Group + "Rect: " + box.Rect.ToString() + ", Left Offset: " + box.LeftOffset + ", Type: " + box.BoundType;
                    }
                    
                    _spritebatch.DrawString(_font, "Current Hitbox: " + _currhitbox + " Number of Hitboxes: " + CurrFrame().GetBoxes().Count, new Vector2(0, numlines * 32), Color.White);
                    numlines++;

                    _spritebatch.DrawString(_font, boxdata, new Vector2(0, numlines * 32), Color.White);
                    numlines++;
                                        
                    if (_showlegend)
                    {
                        _spritebatch.DrawString(_font, "Up: Hitbox Y -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Down: Hitbox Y +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Left: Hitbox  X -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Right: Hitbox  X +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Num Up: Hitbox Height -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Num Down: Hitbox Height +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Num Left: Hitbox  Width -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Num Right: Hitbox  Width +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Insert: Make New Hitbox", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "Delete: Delete Current Hitbox", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "A: Current Hitbox -", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "S: Current Hitbox +", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "D: Change Hitbox Type", new Vector2(0, numlines * 32), Color.White);
                        numlines++;

                        _spritebatch.DrawString(_font, "F: Change Group ID", new Vector2(0, numlines * 32), Color.White);
                        numlines++;
                    }

                    break;
            }

            if (_showlegend)
            {
                _spritebatch.DrawString(_font, "Tab: Switch Facing", new Vector2(0, numlines * 32), Color.White);
                numlines++;

                _spritebatch.DrawString(_font, "Q: Frame -", new Vector2(0, numlines * 32), Color.White);
                numlines++;

                _spritebatch.DrawString(_font, "W: Frame +", new Vector2(0, numlines * 32), Color.White);
                numlines++;
            }
        }

        protected void DrawDrawArea()
        {
            DrawData frame = CurrFrame();

            Rectangle drawarea = frame.DrawArea;
            drawarea.X = -(int)frame.Offsets.X;
            drawarea.Y = -(int)frame.Offsets.Y;

            if (!_facingright)
            {
                drawarea.X = -(int)frame.Offsets.Z;
            }

            _primitives.DrawLine(new Vector2(drawarea.X, drawarea.Y), new Vector2(drawarea.X + drawarea.Width, drawarea.Y), Color.Red);
            _primitives.DrawLine(new Vector2(drawarea.X, drawarea.Y), new Vector2(drawarea.X, drawarea.Y + drawarea.Height), Color.Red);
            _primitives.DrawLine(new Vector2(drawarea.X + drawarea.Width, drawarea.Y), new Vector2(drawarea.X + drawarea.Width, drawarea.Y + drawarea.Height), Color.Red);
            _primitives.DrawLine(new Vector2(drawarea.X, drawarea.Y + drawarea.Height), new Vector2(drawarea.X + drawarea.Width, drawarea.Y + drawarea.Height), Color.Red);

            _spritebatch.Draw(_pointTexture, new Vector2(drawarea.X-1, drawarea.Y-1), Color.White);
            _spritebatch.Draw(_pointTexture, new Vector2(drawarea.X + drawarea.Width - 1, drawarea.Y - 1), Color.White);
            _spritebatch.Draw(_pointTexture, new Vector2(drawarea.X - 1, drawarea.Y+ drawarea.Height - 1), Color.White);
            _spritebatch.Draw(_pointTexture, new Vector2(drawarea.X + drawarea.Width - 1, drawarea.Y+ drawarea.Height - 1), Color.White);
        }

        public void DrawOffsets()
        {
            _spritebatch.Draw(_pointTexture, Vector2.Zero - new Vector2(1), Color.White);
        }

        public void DrawOrigin()
        {
            _spritebatch.Draw(_pointTexture, Vector2.Zero - new Vector2(1), Color.White);
        }

        public void DrawHitboxes()
        {
            DrawData currframe = CurrFrame();

            foreach (var box in currframe.GetBoxes())
            {
                Color color = Color.White;

                switch (box.BoundType)
                {
                    case BoundingType.Body:
                        color = Color.Blue;
                        break; 
                    case BoundingType.Attack:
                        color = Color.Red;
                        break; 
                    case BoundingType.Guard:
                        color = Color.Green;
                        break; 
                }

                Rectangle rect = box.GetRect(_position, _facingright);

                _primitives.DrawRect(new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), color * 0.5f);
            }

        }

        public void DrawOverview()
        {
            DrawOffsets();
            DrawDrawArea();
            DrawHitboxes();
        }

        public override void LogicUpdate()
        {
            if (_winForm.ContainsFocus)
            {
                CheckEmptyFrame();

                _input.Update();
                Anim.Update(0f, _position, new Vector2(1), 0f);

                CheckStandardKeys();

                switch (State)
                {
                    case EditorModeState.Camera:
                        CameraLogic();
                        break;
                    case EditorModeState.SetDrawArea:
                        CheckDrawAreaKeys();
                        break;
                    case EditorModeState.SetOrigin:
                        CheckOriginKeys();
                        break;
                    case EditorModeState.SetOffsets:
                        CheckOffsetKeys();
                        break;
                    case EditorModeState.SetHitboxes:
                        CheckHitboxKeys();
                        break;
                }
            }
        }

        public void SetWinForm(AnimationEditor winForm)
        {
            this._winForm = winForm;
        }

        public void LoadAnim()
        {
            var animdata = FrameAnimation.LoadFromXML(_winForm.SavePath);

            _animID = animdata.Item1;
            Anim = animdata.Item2;

            Frames = Anim.GetDrawDataList();
        }

        public void SaveAnim()
        {
            String path = _winForm.SavePath;

            Dictionary<String, DrawData> dd = new Dictionary<string, DrawData>();

            foreach (var fbf in Frames)
            {
                dd.Add(fbf.ID, fbf);
            }

            Anim.SetDrawDatas(dd);

            //Set animation data.

            XElement anim = new XElement("animation");

            anim.Add(new XAttribute("name", Anim.Name));

            if (Anim.IsLooping)
            {
                anim.Add(new XAttribute("looping", "true"));
            }

            //Set Frames

            XElement frames = new XElement("frames");

            foreach (var framedata in Anim.GetDrawDataList())
            {
                XElement frame = new XElement("frame");

                if (framedata.ID != "")
                {
                    frame.Add(new XAttribute("id", framedata.ID));
                }

                if (framedata.TextureName != "")
                {
                    frame.Add(new XAttribute("texture", framedata.TextureName));
                }

                if (framedata.DrawArea != Rectangle.Empty)
                {
                    frame.Add(new XAttribute("drawarea", framedata.DrawArea.X + "," + framedata.DrawArea.Y + "," + framedata.DrawArea.Width + "," + framedata.DrawArea.Height));
                }

                if (framedata.Offsets.X != 0)
                {
                    frame.Add(new XAttribute("xoffsetright", framedata.Offsets.X));
                }

                if (framedata.Offsets.Y != 0)
                {
                    frame.Add(new XAttribute("yoffset", framedata.Offsets.Y));
                }

                if (framedata.Offsets.Z != 0)
                {
                    frame.Add(new XAttribute("xoffsetleft", framedata.Offsets.Z));
                }

                if (framedata.Origin != Vector2.Zero)
                {
                    frame.Add(new XAttribute("origin", framedata.Origin.X + "," + framedata.Origin.Y));
                }

                //Set Hitboxes

                if (framedata.GetBoxes().Count > 0)
                {
                    XElement hitboxes = new XElement("hitboxes");

                    foreach (var box in framedata.GetBoxes())
                    {
                        if (box.Rect == Rectangle.Empty)
                        {
                            continue;
                        }

                        XElement hitbox = new XElement("hitbox");

                        if (box.Group != "")
                        {
                            hitbox.Add(new XAttribute("group", box.Group));
                        }

                        hitbox.Add(new XAttribute("rectangle", box.Rect.X + "," + box.Rect.Y + "," + box.Rect.Width + "," + box.Rect.Height));

                        if (box.LeftOffset != 0)
                        {
                            hitbox.Add(new XAttribute("leftoffset", box.LeftOffset));
                        }

                        hitbox.Add(new XAttribute("boundingtype", box.BoundType));

                        hitboxes.Add(hitbox);
                    }

                    frame.Add(hitboxes);
                }

                frames.Add(frame);
            }

            anim.Add(frames);

            //Add cycle

            if (Anim.Nodes.Count > 0)
            {
                XElement cycle = new XElement("cycle");

                foreach (var animnode in Anim.Nodes)
                {
                    XElement node = new XElement("node");

                    node.Add(new XAttribute("frame", animnode.DrawData.ID));

                    if (animnode.TimeTillNext > 0)
                    {
                        node.Add(new XAttribute("timetillnext", animnode.TimeTillNext));
                    }

                    //Set Velocity

                    if (animnode.Velocity != Vector2.Zero)
                    {
                        XElement velocity = new XElement("velocity");

                        velocity.Add(new XAttribute("x", animnode.Velocity.X));
                        velocity.Add(new XAttribute("y", animnode.Velocity.Y));

                        if (animnode.SmoothX)
                        {
                            velocity.Add(new XAttribute("xsmooth", true));
                        }

                        if (animnode.SmoothY)
                        {
                            velocity.Add(new XAttribute("ysmooth", true));
                        }

                        node.Add(velocity);
                    }

                    //Set Rotation

                    if (animnode.Rotation != 0f)
                    {
                        XElement rotation = new XElement("rotation");

                        rotation.Add(new XAttribute("radians", animnode.Rotation));

                        if (animnode.SmoothRotation)
                        {
                            rotation.Add(new XAttribute("smooth", true));
                        }

                        node.Add(rotation);
                    }
                    
                    //Set Custom XML.




                    cycle.Add(node);
                }

                anim.Add(cycle);
            }

            //Save

            if (File.Exists(path))
            {
                String backuppath = path.Substring(0, path.Length - ".ani".Length) + ".bak";

                if (File.Exists(backuppath))
                {
                    File.Delete(backuppath);
                }

                File.Copy(path, backuppath);
                File.Delete(path);
            }

            XDocument doc = new XDocument(anim);

            var stream = File.OpenWrite(path);

            doc.Save(stream);
            
            stream.Close();
        }

        private void CameraLogic()
        {
            _camera.CheckKeys(_input);
        }

        private void CheckStandardKeys()
        {
            if (_input.IsKeyPressed(Keys.D1))
            {
                State = EditorModeState.Camera;
            }

            if (_input.IsKeyPressed(Keys.D2))
            {
                State = EditorModeState.SetDrawArea;
            }

            if (_input.IsKeyPressed(Keys.D3))
            {
                State = EditorModeState.SetOrigin;
            }

            if (_input.IsKeyPressed(Keys.D4))
            {
                State = EditorModeState.SetOffsets;
            }

            if (_input.IsKeyPressed(Keys.D5))
            {
                State = EditorModeState.SetHitboxes;
            }

            if (_input.IsKeyPressed(Keys.D6))
            {
                State = EditorModeState.Overview;
            }

            if (_input.IsKeyPressed(Keys.Tab))
            {
                _facingright = !_facingright;
            }

            if (_input.IsKeyPressed(Keys.F12))
            {
                _showlegend = !_showlegend;
            }

            if (_input.IsKeyPressed(Keys.Q))
            {
                _currframe = Math.Max(0, _currframe - 1);
                _currhitbox = 0;
            }

            if (_input.IsKeyPressed(Keys.W))
            {
                _currframe = Math.Min(Frames.Count-1, _currframe + 1);
                _currhitbox = 0;
            }
        }

        private void CheckDrawAreaKeys()
        {
            Rectangle drawarea = CurrFrame().DrawArea;

            int rate = 1;

            if (_input.IsKeyDown(Keys.LeftControl))
            {
                rate *= 5;
            }

            if (_input.IsKeyDown(Keys.Left))
            {
                drawarea.X = Math.Max(0, drawarea.X - rate);
            }

            if (_input.IsKeyDown(Keys.Right))
            {
                drawarea.X = Math.Min(2048, drawarea.X + rate);
            }

            if (_input.IsKeyDown(Keys.Up))
            {
                drawarea.Y = Math.Max(0, drawarea.Y - rate);
            }

            if (_input.IsKeyDown(Keys.Down))
            {
                drawarea.Y = Math.Min(2048, drawarea.Y + rate);
            }

            if (_input.IsKeyDown(Keys.NumPad4))
            {
                drawarea.Width = Math.Max(0, drawarea.Width - rate);
            }

            if (_input.IsKeyDown(Keys.NumPad6))
            {
                drawarea.Width = Math.Min(2048, drawarea.Width + rate);
            }

            if (_input.IsKeyDown(Keys.NumPad8))
            {
                drawarea.Height = Math.Max(0, drawarea.Height - rate);
            }

            if (_input.IsKeyDown(Keys.NumPad2))
            {
                drawarea.Height = Math.Min(2048, drawarea.Height + rate);
            }

            CurrFrame().DrawArea = drawarea;
        }

        private void CheckOriginKeys()
        {
            Vector2 origin = CurrFrame().Origin;

            int rate = 1;

            if (_input.IsKeyDown(Keys.LeftControl))
            {
                rate *= 5;
            }

            if (_input.IsKeyDown(Keys.Left))
            {
                origin.X = Math.Max(0, origin.X - rate);
            }

            if (_input.IsKeyDown(Keys.Right))
            {
                origin.X = Math.Min(2048, origin.X + rate);
            }

            if (_input.IsKeyDown(Keys.Up))
            {
                origin.Y = Math.Max(0, origin.Y - rate);
            }

            if (_input.IsKeyDown(Keys.Down))
            {
                origin.Y = Math.Min(2048, origin.Y + rate);
            }

            CurrFrame().Origin = origin;
        }

        public void CheckOffsetKeys()
        {
            Vector3 offsets = CurrFrame().Offsets;

            int rate = 1;

            if (_input.IsKeyDown(Keys.LeftControl))
            {
                rate *= 5;
            }

            if (_input.IsKeyDown(Keys.Left))
            {
                if (_facingright)
                {
                    offsets.X -= rate;
                }
                else
                {
                    offsets.Z -= rate;
                }
            }

            if (_input.IsKeyDown(Keys.Right))
            {
                if (_facingright)
                {
                    offsets.X += rate;
                }
                else
                {
                    offsets.Z += rate;
                }
            }

            if (_input.IsKeyDown(Keys.Up))
            {
                offsets.Y -= rate;
            }

            if (_input.IsKeyDown(Keys.Down))
            {
                offsets.Y += rate;
            }

            CurrFrame().Offsets = offsets;
        }

        private void CheckHitboxKeys()
        {
            if (_input.IsKeyPressed(Keys.A))
            {
                _currhitbox = Math.Max(0, _currhitbox - 1);
            }

            if (_input.IsKeyPressed(Keys.S))
            {
                _currhitbox = Math.Min(CurrFrame().GetBoxes().Count - 1, _currhitbox + 1);
            }

            if (_input.IsKeyPressed(Keys.Insert))
            {
                AddHitbox();
            }

            if (_input.IsKeyPressed(Keys.Delete))
            {
                DeleteHitbox();
            }

            BoundBox box = CurrHitbox();

            if (box != null)
            {
                Rectangle rect = box.Rect;
                int leftoffset = box.LeftOffset;
                BoundingType type = box.BoundType;

                int rate = 1;

                if (_input.IsKeyDown(Keys.LeftControl))
                {
                    rate *= 5;
                }

                if (_input.IsKeyDown(Keys.Left))
                {
                    if (_facingright)
                    {
                        rect.X -= rate;
                    }
                    else
                    {
                        leftoffset -= rate;
                    }
                }

                if (_input.IsKeyDown(Keys.Right))
                {
                    if (_facingright)
                    {
                        rect.X += rate;
                    }
                    else
                    {
                        leftoffset += rate;
                    }
                }

                if (_input.IsKeyDown(Keys.Up))
                {
                    rect.Y -= rate;
                }

                if (_input.IsKeyDown(Keys.Down))
                {
                    rect.Y += rate;
                }

                if (_input.IsKeyDown(Keys.NumPad4))
                {
                    rect.Width = Math.Max(0, rect.Width - rate);
                }

                if (_input.IsKeyDown(Keys.NumPad6))
                {
                    rect.Width += rate;
                }

                if (_input.IsKeyDown(Keys.NumPad8))
                {
                    rect.Height = Math.Max(0, rect.Height - rate);
                }

                if (_input.IsKeyDown(Keys.NumPad2))
                {
                    rect.Height += rate;
                }

                if (_input.IsKeyPressed(Keys.D))
                {
                    switch (type)
                    {
                        case BoundingType.Body:
                            type = BoundingType.Attack;
                            break;
                        case BoundingType.Attack:
                            type = BoundingType.Guard;
                            break;
                        case BoundingType.Guard:
                            type = BoundingType.Body;
                            break;
                    }
                }

                if (_input.IsKeyPressed(Keys.F))
                {
                    String group = Microsoft.VisualBasic.Interaction.InputBox("Please enter hitbox's group name.", "Hitbox Group ID Input", box.Group);

                    box.SetGroup(group);
                }

                box.SetRect(rect, leftoffset);
                box.SetType(type);
            }
        }

        private void CheckEmptyFrame()
        {
            if (Frames.Count == 0)
            {
                AddNewFrame();
            }
        }

        public void AddNewFrame()
        {
            int id = Frames.Count;

            bool exit = false;

            while (!exit)
            {
                bool changed = false;

                foreach (var drawData in Frames)
                {
                    if (drawData.ID == id.ToString())
                    {
                        id++;
                        changed = true;
                        break;
                    }
                }

                if (!changed)
                {
                    exit = true;
                }
            }

            Frames.Add(new DrawData(id.ToString(), Anim.DefaultTexture, new Rectangle(0, 0, 2048, 2048), new Vector3(0, 0, 0), Vector2.Zero));
        }

        public void DeleteCurrFrame()
        {
            Frames.RemoveAt(_currframe);

            if (Frames.Count == 0)
            {
                AddNewFrame();
            }

            _currframe--;

            if (_currframe == -1)
            {
                _currframe = 0;
            }
        }

        private void AddHitbox()
        {
            CurrFrame().AddBoundingBox(new BoundBox(Rectangle.Empty, 0, BoundingType.Body));
        }

        private void DeleteHitbox()
        {
            if (CurrHitbox() != null)
            {
                CurrFrame().GetBoxes().Remove(CurrHitbox());
            }
            
            _currhitbox--;

            if (_currhitbox == -1)
            {
                _currhitbox = 0;
            }
        }

        public DrawData CurrFrame()
        {
            return Frames[_currframe];
        }

        public int CurrFrameNumber()
        {
            return _currframe;
        }

        public BoundBox CurrHitbox()
        {
            DrawData currframe = CurrFrame();

            if (currframe.GetBoxes().Count > 0)
            {
                return currframe.GetBoxes()[_currhitbox];
            }
            else
            {
                return null;
            }
        }

        public void NewAnimation()
        {
            _winForm.SavePath = "New Animation";
            TextureManager.Initialize(TextureManager.GFX);
            Initialize();
            //Anim = new FrameAnimation("bah", false);
            //Frames = new List<DrawData>();
        }
    }
}