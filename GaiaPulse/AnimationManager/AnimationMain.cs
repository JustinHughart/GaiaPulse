using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GaiaPulse.AnimationManager
{
    public partial class AnimationMain : Form
    {
        String CharacterName;
        String CharacterPath;
        List<String> CostumeList;
        List<String> TypeList;

        public AnimationMain(String CharacterName, String CharacterPath, List<String> CostumeList)
        {
            InitializeComponent();

            this.CharacterName = CharacterName;
            this.CharacterPath = CharacterPath;
            this.CostumeList = CostumeList;

            TypeList = new List<String>();

            LoadTree();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String ID = Microsoft.VisualBasic.Interaction.InputBox("Please enter ID name of new animation.", "Folder ID Input");

            String Path = "";

            if (TreeAnims.SelectedNode == null)
            {
                Path = CharacterPath + "Animations/" + ID + "/";
            }
            else
            {
                Path = GetNodePath(TreeAnims.SelectedNode) + ID + "/";
            }

            if (ID != "")
            {
                if (Helper.FileNameValid(ID))
                {
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);

                        AnimationProfile Profile = new AnimationProfile();
                        Profile.SetID(ID);
                        SaveAnim(Profile, Path + "animdata.gad");
                        LoadTree();
                    }
                    else
                    {
                        MessageBox.Show("ID exists already.");
                    }
                }
                else
                {
                    MessageBox.Show("ID is invalid.");
                }
            }
            else
            {
                MessageBox.Show("Please enter the name of the new animation.");
            }
        }

        private void SaveAnim(AnimationProfile Profile, String Path)
        {
            Stream Stream = File.Open(Path, FileMode.Create);
            BinaryFormatter Formatter = new BinaryFormatter();

            Formatter.Serialize(Stream, Profile);
            Stream.Close();
        }

        private void LoadTree()
        {
            String DirectoryPath = CharacterPath + "Animations/";

            TreeAnims.Nodes.Clear();

            var DirectoryList = Directory.EnumerateDirectories(DirectoryPath);

            if (!Directory.Exists(CharacterPath + "Animations/"))
            {
                Directory.CreateDirectory(CharacterPath + "Animations/");
            }

            String Path = CharacterPath + "Animations/";

            RecurseDir(Path, null);
        }

        private void RecurseDir(String DirPath, TreeNode ParentNode)
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
                    TreeAnims.Nodes.Add(Node);
                }

                if (Directory.GetFiles(directory).Length == 0)
                {
                    RecurseDir(directory + "/", Node);
                }
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTree();
        }

        private String GetNodePath(TreeNode Node)
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

            Path = CharacterPath + "Animations/" + Path;

            return Path;
        }

        private void button2_Click(object sender, EventArgs e) //Delete Node
        {
            if (TreeAnims.SelectedNode != null)
            {
                DialogResult Result = MessageBox.Show("Are you sure you want to delete this node and all subnodes?", "Delete?", MessageBoxButtons.OKCancel);

                if (Result == DialogResult.OK)
                {
                    String DeletePath = GetNodePath(TreeAnims.SelectedNode);
                    Directory.Delete(DeletePath, true);
                    MessageBox.Show("Node deleted.");
                    LoadTree();
                }
                else
                {
                    MessageBox.Show("Deletion aborted.");
                }
            }
            else
            {
                MessageBox.Show("Select a node to delete first.");
            }

            LoadTree();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (TreeAnims.SelectedNode != null)
            {
                AnimationEditor Editor = new AnimationEditor(TreeAnims.SelectedNode.Text, CharacterName, CharacterPath, GetNodePath(TreeAnims.SelectedNode));
                Editor.Show();
            }
            else
            {
                MessageBox.Show("Please select an animation.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TreeAnims.SelectedNode = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (TreeAnims.SelectedNode != null)
            {
                if (!TreeAnims.SelectedNode.Text.Contains("!"))
                {
                    MessageBox.Show("Cannot place a folder within an part.");
                    return;
                }
            }

            String FolderID = Microsoft.VisualBasic.Interaction.InputBox("Please enter ID name of new folder.", "Folder ID Input");

            if (Helper.FileNameValid(FolderID))
            {
                String Path = "";

                if (TreeAnims.SelectedNode == null)
                {
                    Path = CharacterPath + "Animations/" + FolderID;
                }
                else
                {
                    Path = GetNodePath(TreeAnims.SelectedNode) + FolderID;
                }

                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                    LoadTree();
                }
                else
                {
                    MessageBox.Show("Folder exists.");
                }
            }
            else
            {
                MessageBox.Show("ID contains invalid characters.");
            }
        }
    }
}