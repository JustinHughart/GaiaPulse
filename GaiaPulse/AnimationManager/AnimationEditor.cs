using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace GaiaPulse.AnimationManager
{
    public partial class AnimationEditor : Form
    {
        public String AnimPath { get; private set; }
        public String TexturePath { get; private set; }

        public AnimationEditor()
        {
            InitializeComponent();
            
            AnimPath = "";

            TexturePath = "Textures";
            
            Editor.SetWinForm(this);

            CheckTextureFolder();
        }

        private void AssignTextureToolStripMenuItemClick(object sender, EventArgs e)
        {
            AnimDataFrame adf = new AnimDataFrame(Editor.Anim);
            adf.Show();
        }

        private void SaveToolStripMenuItem1Click(object sender, EventArgs e)
        {
            Editor.SaveAnim();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Editor.SaveAnim();
            this.Close();
        }

        private void FrameDataToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (Editor.Frames.Count > 0)
            {
                NodeDataFrame ndf = new NodeDataFrame(Editor.CurrFrame());
                ndf.Show();
            }
            else
            {
                MessageBox.Show("Animation has no frames.");
            }
        }

        private void AddFrameToolStripMenuItemClick(object sender, EventArgs e)
        {
            Editor.AddNewFrame();
        }

        private void DeleteFrameToolStripMenuItemClick(object sender, EventArgs e)
        {
            Editor.DeleteCurrFrame(); 
        }

        private void CycleToolStripMenuItemClick(object sender, EventArgs e)
        {
            Editor.SaveAnim();
            CycleManager cm = new CycleManager(Editor, Editor.Anim);
            cm.Show();
        }

        private void TexturesToolStripMenuItemClick(object sender, EventArgs e)
        {
            Process.Start(TexturePath);
        }

        private void CheckTextureFolder()
        {
            if (!Directory.Exists(TexturePath))
            {
                Directory.CreateDirectory(TexturePath);
            }
        }
    }
}