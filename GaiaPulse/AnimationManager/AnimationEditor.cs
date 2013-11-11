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

            ChangeTitle();
            
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

                ChangeTitle();

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

        private void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenSaveDialog();
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.InitialDirectory = Program.AppDir;
            ofd.Filter = "Gaia Pulse Animation | *.ani";
            ofd.ShowDialog();

            if (ofd.FileName != "")
            {
                SavePath = ofd.FileName;

                ChangeTitle();

                Editor.LoadAnim();
            }
        }

        private void ChangeTitle()
        {
            Text = "Gaia Pulse Animation Editor - " + SavePath + " -";
        }

        private void ToolCameraClick(object sender, EventArgs e)
        {
            Editor.ChangeMode(EditorModeState.Camera);
        }

        private void ToolDrawAreaClick(object sender, EventArgs e)
        {
            Editor.ChangeMode(EditorModeState.SetDrawArea);
        }

        private void ToolOriginClick(object sender, EventArgs e)
        {
            Editor.ChangeMode(EditorModeState.SetOrigin);
        }

        private void ToolOffsetsClick(object sender, EventArgs e)
        {
            Editor.ChangeMode(EditorModeState.SetOffsets);
        }

        private void ToolHitboxesClick(object sender, EventArgs e)
        {
            Editor.ChangeMode(EditorModeState.SetHitboxes);
        }

        private void ToolPreviewClick(object sender, EventArgs e)
        {
            Editor.ChangeMode(EditorModeState.Overview);
        }
    }
}