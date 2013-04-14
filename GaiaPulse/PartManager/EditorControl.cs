using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using GaiaPulse.PartManager.EditorData;
using GaiaPulse.PartManager.PartData;
using GaiaPulse.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SC;

using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace GaiaPulse.PartManager
{
    public enum EditorModeState
    {
        Camera, SetBoundary, SetAnchor,
    }

    public class EditorControl : GraphicsDeviceControl
    {
        ContentManager Content;
        SpriteBatch SpriteBatch;
        SpriteFont Font;

        Camera Camera;
        Input Input;

        PartEditor WinForm;

        public String PartPath { get; private set; }

        public TemporaryEditorPart Part { get; private set; }

        Texture2D PointTexture;
        Texture2D LineTexture;
        Texture2D AnchorTexture;

        Primitives Primitives;

        Vector2 UpperLeft;
        Vector2 UpperRight;
        Vector2 LowerLeft;
        Vector2 LowerRight;

        public EditorModeState State;
        Boolean TabDown;
        int AnchorWorkingOn;
        bool PlusPressed;
        bool MinusPressed;

        public Stream Stream;

        protected override void Initialize()
        {
            Content = new ContentManager(Services, "CommonData");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Fonts/Courier New");
            Camera = new Camera();

            PartPath = WinForm.PartPath + WinForm.CostumeName + ".prt";

            LoadPart(PartPath);

            Part.SetUpAnchors(WinForm.NumAnchors);

            PointTexture = Content.Load<Texture2D>("pixelmarker");
            LineTexture = Content.Load<Texture2D>("1x1");
            AnchorTexture = Content.Load<Texture2D>("AnchorPoint");

            Primitives = new Primitives(SpriteBatch, LineTexture, LineTexture);
            Input = new Input(new Vector2(WinForm.Width, WinForm.Height));
            Camera.SetRoomSize(new Vector2(WinForm.Width, WinForm.Height));

            RecalcDrawBox(Rectangle.Empty);

            Application.Idle += delegate { Invalidate(); };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Content.Unload();
            }

            base.Dispose(disposing);
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, Camera.ReturnView());

            if (Part.PartTexture != null)
            {
                SpriteBatch.Draw(Part.PartTexture, Vector2.Zero, Part.DrawRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                Primitives.DrawLine(UpperLeft, UpperRight, Color.Red, .0000000009f);
                Primitives.DrawLine(UpperRight, LowerRight, Color.Red, .0000000009f);
                Primitives.DrawLine(LowerRight, LowerLeft, Color.Red, .0000000009f);
                Primitives.DrawLine(LowerLeft, UpperLeft, Color.Red, .0000000009f);

                SpriteBatch.Draw(PointTexture, UpperLeft - new Vector2(1, 1), new Rectangle(0, 0, 3, 3), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .000000001f);
                SpriteBatch.Draw(PointTexture, UpperRight - new Vector2(1, 1), new Rectangle(0, 0, 3, 3), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .000000001f);
                SpriteBatch.Draw(PointTexture, LowerLeft - new Vector2(1, 1), new Rectangle(0, 0, 3, 3), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .000000001f);
                SpriteBatch.Draw(PointTexture, LowerRight - new Vector2(1, 1), new Rectangle(0, 0, 3, 3), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .000000001f);

                int NumOfAnchors = 1;

                foreach (Anchor Anchor in Part.Anchors)
                {
                    SpriteBatch.Draw(AnchorTexture, Anchor.Position - new Vector2(3, 3), new Rectangle(0, 0, AnchorTexture.Width, AnchorTexture.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .00000000185f + (.0000000000001f * NumOfAnchors));
                    SpriteBatch.DrawString(Font, NumOfAnchors.ToString(), Anchor.Position + new Vector2(5, -10), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .00000000188f + (.0000000000001f * NumOfAnchors));
                    NumOfAnchors++;
                }
            }
            else
            {
                SpriteBatch.DrawString(Font, "Texture not loaded.", new Vector2(0, 64), Color.White);
            }

            SpriteBatch.End();

            Rectangle DebugRect = new Rectangle((int)((UpperLeft.X - 1 + (Camera.Position.X)) * Camera.Zoom), (int)((UpperLeft.Y - 1 + (Camera.Position.Y)) * Camera.Zoom), (int)(3 * Camera.Zoom), (int)(3 * Camera.Zoom));

            SpriteBatch.Begin();
            SpriteBatch.DrawString(Font, "FPS: " + CurrentFPS + ", Mode State: " + State + ", Detection Rectangle: " + DebugRect.ToString() + ", Mouse Pos: " + Input.GetCurrentPosition().ToString(), new Vector2(0, 32), Color.White);

            if (State == EditorModeState.SetAnchor)
            {
                SpriteBatch.DrawString(Font, "Currently editing Anchor " + (AnchorWorkingOn + 1), new Vector2(0, 64), Color.White);
            }
            SpriteBatch.End();
        }

        public override void LogicUpdate()
        {
            Input.Update();

            ChangeModes();

            switch (State)
            {
                case EditorModeState.Camera:
                    CheckCameraHotkeys();
                    CheckMouseCameraControl();
                    break;
                case EditorModeState.SetBoundary:
                    CheckBoundaryHotkeys();
                    CheckMouseBoundaryControl();
                    break;
                case EditorModeState.SetAnchor:
                    CheckAnchorHotkeys();
                    CheckMouseAnchorControl();
                    break;
            }
        }

        public Texture2D LoadTexture(String FilePath)
        {
            Texture2D OutputTexture = null;

            Stream = File.OpenRead(FilePath);

            OutputTexture = Texture2D.FromStream(GraphicsDevice, Stream);

            Stream.Close();

            return OutputTexture;
        }

        public void RecalcDrawBox(Rectangle RectVelocity)
        {
            Part.SetDrawRect(new Rectangle(Part.DrawRect.X + RectVelocity.X, Part.DrawRect.Y + RectVelocity.Y, Part.DrawRect.Width + RectVelocity.Width, Part.DrawRect.Height + RectVelocity.Height));

            UpperLeft = new Vector2(0, 0);
            UpperRight = new Vector2(Part.DrawRect.Width, 0);
            LowerLeft = new Vector2(0, Part.DrawRect.Height);
            LowerRight = new Vector2(Part.DrawRect.Width, Part.DrawRect.Height);
        }

        public void CheckCameraHotkeys()
        {
            if (Input.KeyboardState.IsKeyDown(Keys.Add)) //Zoom in
            {
                Camera.ChangeZoom(0.05f);
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Subtract)) //Zoom Out
            {
                Camera.ChangeZoom(-0.05f);
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Left)) //Camera Left
            {
                Camera.Position.X--;
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Right)) //Camera Right
            {
                Camera.Position.X++;
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Up)) //Camera Up
            {
                Camera.Position.Y--;
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Down)) //Camera Down
            {
                Camera.Position.Y++;
            }
        }

        public void CheckBoundaryHotkeys()
        {
            if (Input.KeyboardState.IsKeyDown(Keys.Up)) //Move draw area up.
            {
                RecalcDrawBox(new Rectangle(0, -1, 0, 0));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Down)) //Move draw area down.
            {
                RecalcDrawBox(new Rectangle(0, 1, 0, 0));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Left)) //Move draw area left.
            {
                RecalcDrawBox(new Rectangle(-1, 0, 0, 0));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Right)) //Move draw area right.
            {
                RecalcDrawBox(new Rectangle(1, 0, 0, 0));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.NumPad4)) //Make the X axis of the draw area shrink
            {
                RecalcDrawBox(new Rectangle(0, 0, -1, 0));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.NumPad6)) //Make the X axis of the draw area grow
            {
                RecalcDrawBox(new Rectangle(0, 0, 1, 0));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.NumPad8)) //Make the Y axis of the draw area shrink
            {
                RecalcDrawBox(new Rectangle(0, 0, 0, -1));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.NumPad2)) //Make the Y axis of the draw area grow
            {
                RecalcDrawBox(new Rectangle(0, 0, 0, 1));
            }
        }

        public void CheckAnchorHotkeys()
        {
            if (Input.KeyboardState.IsKeyDown(Keys.Add)) //Increments anchor being worked on.
            {
                if (!PlusPressed)
                {
                    PlusPressed = true;
                    AnchorWorkingOn++;

                    if (AnchorWorkingOn > Part.Anchors.Count - 1)
                    {
                        AnchorWorkingOn = 0;
                    }
                }
            }
            else
            {
                PlusPressed = false;
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Subtract)) //Decrements anchor being worked on.
            {
                if (!MinusPressed)
                {
                    MinusPressed = true;
                    AnchorWorkingOn--;

                    if (AnchorWorkingOn < 0)
                    {
                        AnchorWorkingOn = Part.Anchors.Count - 1;
                    }
                }
            }
            else
            {
                MinusPressed = false;
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Up)) //Move anchor point up.
            {
                Vector2 OldPos = Part.Anchors[AnchorWorkingOn].Position;
                Part.Anchors[AnchorWorkingOn].SetPosition(new Vector2(OldPos.X, OldPos.Y - 1));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Down)) //Move anchor point down.
            {
                Vector2 OldPos = Part.Anchors[AnchorWorkingOn].Position;
                Part.Anchors[AnchorWorkingOn].SetPosition(new Vector2(OldPos.X, OldPos.Y + 1));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Left)) //Move anchor point left.
            {
                Vector2 OldPos = Part.Anchors[AnchorWorkingOn].Position;
                Part.Anchors[AnchorWorkingOn].SetPosition(new Vector2(OldPos.X - 1, OldPos.Y));
            }

            if (Input.KeyboardState.IsKeyDown(Keys.Right)) //Move anchor point right.
            {
                Vector2 OldPos = Part.Anchors[AnchorWorkingOn].Position;
                Part.Anchors[AnchorWorkingOn].SetPosition(new Vector2(OldPos.X + 1, OldPos.Y));
            }
        }

        public void ChangeModes()
        {
            if (Input.KeyboardState.IsKeyDown(Keys.Tab))
            {
                if (!TabDown)
                {
                    TabDown = true;

                    switch (State)
                    {
                        case EditorModeState.Camera:
                            State = EditorModeState.SetBoundary;
                            break;
                        case EditorModeState.SetBoundary:
                            State = EditorModeState.SetAnchor;
                            break;
                        case EditorModeState.SetAnchor:
                            State = EditorModeState.Camera;
                            break;
                    }
                }
            }
            else
            {
                TabDown = false;
            }
        }

        public void CheckMouseCameraControl()
        {
            if (Input.IsDragging())
            {
                Camera.MoveCamera(-Input.GetDrag() / Camera.Zoom);
            }
        }

        public void CheckMouseBoundaryControl()
        {
            if (Input.CheckTouch(new Rectangle((int)((UpperLeft.X - 1 + (Camera.Position.X)) * Camera.Zoom), (int)((UpperLeft.Y - 1 + (Camera.Position.Y)) * Camera.Zoom), (int)(3 * Camera.Zoom), (int)(3 * Camera.Zoom))))
            {
                MessageBox.Show("click!");
            }
        }

        public void CheckMouseAnchorControl()
        {
        }

        public void SetWinForm(PartEditor WinForm)
        {
            this.WinForm = WinForm;
        }

        public void SavePart(String FilePath)
        {
            Stream Stream = File.Open(FilePath, FileMode.Create);
            BinaryFormatter Formatter = new BinaryFormatter();

            Formatter.Serialize(Stream, Part);
            Stream.Close();
        }

        public void LoadPart(String FilePath)
        {
            if (File.Exists(FilePath))
            {
                Stream Stream = File.Open(FilePath, FileMode.Open);
                BinaryFormatter Formatter = new BinaryFormatter();

                Part = (TemporaryEditorPart)Formatter.Deserialize(Stream);
                Stream.Close();

                if (Part.TextureName != "")
                {
                    Part.SetPartTexture(LoadTexture(WinForm.CharacterPath + "Textures/" + Part.TextureName));
                }
            }
            else
            {
                Part = new TemporaryEditorPart();
            }
        }
    }
}