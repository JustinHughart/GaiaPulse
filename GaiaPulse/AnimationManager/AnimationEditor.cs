using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using GaiaPulse.SC.FrameAnimation;
using Microsoft.Xna.Framework;

namespace GaiaPulse.AnimationManager
{
    public partial class AnimationEditor : Form
    {
        public String SavePath;

        public AnimationEditor()
        {
            InitializeComponent();
         
            SavePath = "New Animation";

            this.Text = "Gaia Pulse Animation Editor - " + SavePath + " -";
            
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
            if (SavePath == "New Animation")
            {
                OpenSaveDialog();
            }
            else
            {
                Editor.SaveAnim();
                MessageBox.Show("File saved. Old file backed up.", "File saved!");
            }
        }

        private void OpenSaveDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = Program.AppDir;
            sfd.Filter = "Gaia Pulse Animation | *.ani";
            sfd.ShowDialog();

            if (sfd.FileName != "")
            {
                SavePath = sfd.FileName;

                Text = "Gaia Pulse Animation Editor - " + SavePath + " -";

                Editor.SaveAnim();
            }
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
                NodeDataFrame ndf = new NodeDataFrame(Editor.CurrFrame(), Editor.CurrFrameNumber());
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
            Dictionary<String, DrawData> dd = new Dictionary<string, DrawData>();

            foreach (var fbf in Editor.Frames)
            {
                dd.Add(fbf.ID, fbf);
            }

            Editor.Anim.SetDrawDatas(dd);

            CycleManager cm = new CycleManager(Editor, Editor.Anim);
            cm.Show();
        }

        private void TexturesToolStripMenuItemClick(object sender, EventArgs e)
        {
            Process.Start(Program.TexturePath);
        }

        private void CheckTextureFolder()
        {
            if (!Directory.Exists(Program.TexturePath))
            {
                Directory.CreateDirectory(Program.TexturePath);
            }
        }
        
        private void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to start anew?", "Making a new file", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                Editor.NewAnimation();
            }
        }
    }
}