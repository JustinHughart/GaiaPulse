using System;
using System.Windows.Forms;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace GaiaPulse.PartManager
{
    public partial class PartEditor : Form
    {
        public String CharacterName { get; private set; }

        public String CharacterPath { get; private set; }

        public String PartPath { get; private set; }

        public String PartName { get; private set; }

        public String CostumeName { get; private set; }

        public int NumAnchors { get; private set; }

        public PartEditor(String CharacterName, String CharacterPath, String PartName, String Path, String CostumeName, int NumAnchors)
        {
            InitializeComponent();

            this.CharacterName = CharacterName;
            this.CharacterPath = CharacterPath;
            this.PartName = PartName;
            this.CostumeName = CostumeName;
            this.NumAnchors = NumAnchors;

            PartPath = Path;

            Editor.SetWinForm(this);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void btnCamera_Click(object sender, EventArgs e)
        {
            Editor.State = EditorModeState.Camera;
        }

        private void btnBoundary_Click(object sender, EventArgs e)
        {
            Editor.State = EditorModeState.SetBoundary;
        }

        private void btnAnchor_Click(object sender, EventArgs e)
        {
            Editor.State = EditorModeState.SetAnchor;
        }

        private void assignTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextureAssign TextureAssign = new TextureAssign(CharacterName, CharacterPath, CostumeName, this);
            TextureAssign.Show();
        }

        private void textualEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Editor.SavePart(Editor.PartPath);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PartEditor_Load(object sender, EventArgs e)
        {
        }

        public void LoadTexture(String Path)
        {
            Editor.Part.SetTextureName(Path);
            Editor.Part.SetPartTexture(Editor.LoadTexture(Path));

            if (Editor.Part.DrawRect == new Rectangle(0, 0, 1, 1))
            {
                Editor.Part.DefaultDrawRect();
                Editor.RecalcDrawBox(Rectangle.Empty);
            }
        }

        public void LoadPart()
        {
            Editor.Part.SetUpAnchors(NumAnchors);
        }

        public void SetTexName(String Path)
        {
            Editor.Part.SetTextureName(Path);
        }
    }
}