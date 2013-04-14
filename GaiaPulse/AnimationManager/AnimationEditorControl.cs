using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using GaiaPulse.AnimationManager.DataDevices;
using GaiaPulse.PartManager.EditorData;
using GaiaPulse.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SC.GaiaPulse;

namespace GaiaPulse.AnimationManager
{
    public class AnimationEditorControl : GraphicsDeviceControl
    {
        ContentManager Content;
        SpriteBatch SpriteBatch;
        SpriteFont Font;
        AnimationEditor Editor;

        EntityFrame Entity;
        Animation Anim;
        Frame GPFrame;

        protected override void Initialize()
        {
            Content = new ContentManager(Services, "CommonData");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Fonts/Courier New");

            SC.TextureManager.InitializeWithoutContent(GraphicsDevice);
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            if (GPFrame != null)
            {
                if (GPFrame.Grandfather != null)
                {
                    RecurseDraw(GPFrame.Grandfather);
                }
            }

            SpriteBatch.End();
        }

        private void RecurseDraw(Part Part)
        {
            Part.Draw(SpriteBatch);

            foreach (Part Child in Part.Children)
            {
                RecurseDraw(Child);
            }
        }

        public override void LogicUpdate()
        {
            if (Entity != null)
            {
                Entity.Update(1f, new Vector2(200, 200), new Vector2(1), 0f);
            }
        }

        public void SetEditor(AnimationEditor Editor)
        {
            this.Editor = Editor;
        }

        public void LoadData(TreeNode TreeData)
        {
            Entity = new EntityFrame();
            Animation Anim = new Animation();
            GPFrame = new Frame();

            Anim.AddFrame(GPFrame);
            Entity.SetBody(Anim);

            Anim.SetLooping(true);
            GPFrame.ChangeChangeTime(10);

            CreateNode(TreeData, null);

            if (GPFrame.Grandfather == null)
            {
                Entity = null;
                Anim = null;
                GPFrame = null;
            }
        }

        private void CreateNode(TreeNode Node, Part Parent)
        {
            Part Part = null;

            if (Node.Text != "%ROOT%" && Node.Text != "%EMPTY%")
            {
                PartTag Tag = (PartTag)Node.Tag;

                String PartID = Tag.PartID;

                Part = new Part();

                Part.ChangeColor(Tag.Color);
                Part.ChangeLayerOffset(Tag.Layer);
                Part.ChangeRotation(Tag.Rotation);
                Part.ChangeScale(Tag.Scale);

                String PartPath = Editor.CharacterPath + "Parts/" + Tag.PartID + "Battle.prt";

                TemporaryEditorPart PartData = LoadPartData(PartPath);

                Part.ChangeTextureName(Editor.CharacterPath + "/Textures/" + PartData.TextureName);
                Part.ChangeDrawArea(PartData.DrawRect);

                foreach (var Anchor in PartData.Anchors)
                {
                    Part.AddAnchor(Anchor.Position);
                }

                if (Parent != null)
                {
                    Parent.AddChild(Part);
                }
                else
                {
                    GPFrame.SetGrandfather(Part);
                }
            }

            foreach (TreeNode NextNode in Node.Nodes)
            {
                CreateNode(NextNode, Part);
            }
        }

        private Texture2D LoadTexture(String FilePath)
        {
            Texture2D OutputTexture = null;

            Stream Stream = File.OpenRead(FilePath);

            OutputTexture = Texture2D.FromStream(GraphicsDevice, Stream);

            Stream.Close();

            return OutputTexture;
        }

        public TemporaryEditorPart LoadPartData(String FilePath)
        {
            if (File.Exists(FilePath))
            {
                Stream Stream = File.Open(FilePath, FileMode.Open);
                BinaryFormatter Formatter = new BinaryFormatter();

                TemporaryEditorPart Part = (TemporaryEditorPart)Formatter.Deserialize(Stream);
                Stream.Close();

                return Part;
            }
            else
            {
                throw new Exception("File doesn't exist. (LoadPartData())");
            }
        }
    }
}