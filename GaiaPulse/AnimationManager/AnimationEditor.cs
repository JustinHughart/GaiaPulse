using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using GaiaPulse.AnimationManager.DataDevices;
using GaiaPulse.PartManager.PartData;
using Microsoft.Xna.Framework;

namespace GaiaPulse.AnimationManager
{
    public partial class AnimationEditor : Form
    {
        public String CharacterName;
        public String CharacterPath;
        public String AnimPath;
        String ID;
        AnimationProfile Profile;

        public AnimationEditor(String ID, String CharacterName, String CharacterPath, String AnimPath)
        {
            this.CharacterName = CharacterName;
            this.CharacterPath = CharacterPath;
            this.AnimPath = AnimPath;
            this.ID = ID;
            InitializeComponent();
            Load(AnimPath + "animdata.gad");
            EditorControl1.SetEditor(this);
            LoadLibrary();
            InitializeTree();
        }

        public void InitializeTree()
        {
            TreeView.Nodes.Add("%ROOT%");
            TreeView.Nodes[0].Nodes.Add("%EMPTY%");
            TreeView.ExpandAll();
            TreeView.HideSelection = false;
        }

        public void AddNode(String ID, PartTag PartTag)
        {
            TreeView.SelectedNode.Text = ID;
            TreeView.SelectedNode.Tag = PartTag;
            EditorControl1.LoadData(TreeView.Nodes[0]);
        }

        public void Save(String Path)
        {
            Stream Stream = File.Open(Path, FileMode.Create);
            BinaryFormatter Formatter = new BinaryFormatter();

            Formatter.Serialize(Stream, Profile);
            Stream.Close();
        }

        public void Load(String Path)
        {
            Stream Stream = File.Open(Path, FileMode.Open);
            BinaryFormatter Formatter = new BinaryFormatter();

            Profile = (AnimationProfile)Formatter.Deserialize(Stream);
            Stream.Close();
        }

        public void LoadLibrary()
        {
            String DirectoryPath = CharacterPath + "Parts/";

            TreeParts.Nodes.Clear();

            var DirectoryList = Directory.EnumerateDirectories(DirectoryPath);

            if (!Directory.Exists(CharacterPath + "Parts/"))
            {
                Directory.CreateDirectory(CharacterPath + "Parts/");
            }

            String Path = CharacterPath + "Parts/";

            RecursePartsDir(Path, null);
        }

        private void RecursePartsDir(String DirPath, TreeNode ParentNode)
        {
            var DirectoryList = Directory.EnumerateDirectories(DirPath);

            foreach (var directory in DirectoryList)
            {
                String String = "";

                if (Directory.GetFiles(directory).Length > 0)
                {
                    String = directory.Substring(DirPath.Length);
                }
                else
                {
                    String = "!" + directory.Substring(DirPath.Length) + "!";
                }

                TreeNode Node = new TreeNode(String);

                if (ParentNode != null)
                {
                    ParentNode.Nodes.Add(Node);
                }
                else
                {
                    TreeParts.Nodes.Add(Node);
                }

                if (Directory.GetFiles(directory).Length == 0)
                {
                    RecursePartsDir(directory + "/", Node);
                }
            }
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private String GetPartsNodePath(TreeNode Node)
        {
            String Path = "";

            bool End = false;

            while (!End)
            {
                if (Node.Text.StartsWith("!") && Node.Text.EndsWith("!"))
                {
                    Path = Node.Text.Substring(1, Node.Text.Length - 2) + "/" + Path;
                }
                else
                {
                    Path = Node.Text + "/" + Path;
                }

                if (Node.Parent == null)
                {
                    End = true;
                }
                else
                {
                    Node = Node.Parent;
                }
            }

            Path = CharacterPath + "Parts/" + Path;

            return Path;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (TreeView.SelectedNode != null)
            {
                if (TreeParts.SelectedNode != null)
                {
                    if (TreeView.SelectedNode.Text != "%ROOT%")
                    {
                        String PartID = Microsoft.VisualBasic.Interaction.InputBox("Please enter ID name of new part.", "Part ID Input");

                        if (PartID.Length > 1 && !PartID.Contains("%"))
                        {
                            String Path = GetPartsNodePath(TreeParts.SelectedNode) + "/commondata.dat";

                            PartCommonData PartCommonData = LoadSerializedCommonData(Path);

                            for (int i = PartCommonData.AnchorPoints - 1; i > 0; i--)
                            {
                                TreeView.SelectedNode.Nodes.Add("%EMPTY%");
                            }

                            PartTag PartTag = new PartTag();
                            PartTag.PartID = GetPartsNodePath(TreeParts.SelectedNode).Substring(CharacterPath.Length + "Parts/".Length);
                            PartTag.Color = Microsoft.Xna.Framework.Color.White;
                            PartTag.Layer = 0;
                            PartTag.Scale = new Vector2(1f);
                            PartTag.Rotation = 0f;

                            AddNode(PartID, PartTag);
                        }
                        else
                        {
                            MessageBox.Show("Bad name. Name is either too short or using system reserved % signs.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot overwrite the root.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a library item.");
                }
            }
            else
            {
                MessageBox.Show("Please select a tree node.");
            }
        }

        private PartCommonData LoadSerializedCommonData(String Path)
        {
            Stream Stream = File.Open(Path, FileMode.Open);
            BinaryFormatter Formatter = new BinaryFormatter();

            PartCommonData CommonData = (PartCommonData)Formatter.Deserialize(Stream);
            Stream.Close();

            return CommonData;
        }

        private void EditorControl1_Click(object sender, EventArgs e)
        {
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (TreeView.SelectedNode != null)
            {
                if (TreeView.SelectedNode.Text != "%ROOT%")
                {
                    TreeView.SelectedNode.Text = "%EMPTY%";
                    TreeView.SelectedNode.Tag = null;
                    TreeView.SelectedNode.Nodes.Clear();
                    EditorControl1.LoadData(TreeView.Nodes[0]);
                    EditorControl1.Refresh();
                }
                else
                {
                    MessageBox.Show("Cannot overwrite the root.");
                }
            }
            else
            {
                MessageBox.Show("Please select a node in the tree.");
            }
        }
    }
}