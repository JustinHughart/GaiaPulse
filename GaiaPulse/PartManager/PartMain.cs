using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using GaiaPulse.PartManager.PartData;

namespace GaiaPulse.PartManager
{
    public partial class PartMain : Form
    {
        String CharacterName;
        String CharacterPath;
        List<String> CostumeList;

        public PartMain(String Name, String Path, List<String> CostumeList)
        {
            CharacterName = Name;
            CharacterPath = Path;
            this.CostumeList = CostumeList;
            InitializeComponent();
            this.Text = "Parts Manager: " + CharacterName;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (TreeParts.SelectedNode != null)
            {
                if (!TreeParts.SelectedNode.Text.Contains("!"))
                {
                    MessageBox.Show("Cannot place a folder within an part.");
                    return;
                }
            }

            String PartID = Microsoft.VisualBasic.Interaction.InputBox("Please enter ID name of new character.", "Character ID Input");

            if (PartID != "")
            {
                if (Helper.FileNameValid(PartID))
                {
                    PartCommonData Data = new PartCommonData();
                    Data.SetAnchorNumber(1);

                    String DirectoryPath = "";

                    if (TreeParts.SelectedNode == null)
                    {
                        DirectoryPath = CharacterPath + "/Parts/" + PartID + "/";
                    }
                    else
                    {
                        DirectoryPath = GetNodePath(TreeParts.SelectedNode) + PartID + "/";
                    }

                    Directory.CreateDirectory(DirectoryPath);

                    String FilePath = DirectoryPath + "commondata.dat";

                    SerializeCommonData(Data, FilePath);

                    LoadTree();
                }
                else
                {
                    MessageBox.Show("Name is invalid.");
                }
            }
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

            Path = CharacterPath + "Parts/" + Path;

            return Path;
        }

        private void LoadTree()
        {
            String DirectoryPath = CharacterPath + "Parts/";

            TreeParts.Nodes.Clear();

            var DirectoryList = Directory.EnumerateDirectories(DirectoryPath);

            if (!Directory.Exists(CharacterPath + "Parts/"))
            {
                Directory.CreateDirectory(CharacterPath + "Parts/");
            }

            String Path = CharacterPath + "Parts/";

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
                    TreeParts.Nodes.Add(Node);
                }

                if (Directory.GetFiles(directory).Length == 0)
                {
                    RecurseDir(directory + "/", Node);
                }
            }
        }

        private void SerializeCommonData(PartCommonData Data, String FileName)
        {
            Stream Stream = File.Open(FileName, FileMode.Create);
            BinaryFormatter Formatter = new BinaryFormatter();

            Formatter.Serialize(Stream, Data);
            Stream.Close();
        }

        private void PartMain_Load(object sender, EventArgs e)
        {
            LoadTree();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (TreeParts.SelectedNode != null)
            {
                DialogResult Result = MessageBox.Show("Are you sure you want to delete this node and all subnodes?", "Delete?", MessageBoxButtons.OKCancel);

                if (Result == DialogResult.OK)
                {
                    String DeleteDir = GetNodePath(TreeParts.SelectedNode);

                    Directory.Delete(DeleteDir, true);

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
                MessageBox.Show("Please select a part first.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (TreeParts.SelectedNode != null)
            {
                PartCostumeSelect CostumeSelect = new PartCostumeSelect(TreeParts.SelectedNode.Text, CharacterName, CharacterPath, GetNodePath(TreeParts.SelectedNode), CostumeList);
                CostumeSelect.Show();
            }
            else
            {
                MessageBox.Show("Please select a part.");
            }
        }

        private void btnCreateDir_Click(object sender, EventArgs e)
        {
            if (TreeParts.SelectedNode != null)
            {
                if (!TreeParts.SelectedNode.Text.Contains("!"))
                {
                    MessageBox.Show("Cannot place a folder within an part.");
                    return;
                }
            }

            String FolderID = Microsoft.VisualBasic.Interaction.InputBox("Please enter ID name of new folder.", "Folder ID Input");

            if (Helper.FileNameValid(FolderID))
            {
                String Path = "";

                if (TreeParts.SelectedNode == null)
                {
                    Path = CharacterPath + "Parts/" + FolderID;
                }
                else
                {
                    Path = GetNodePath(TreeParts.SelectedNode) + FolderID;
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

        private void btnUnselect_Click(object sender, EventArgs e)
        {
            TreeParts.SelectedNode = null;
        }
    }
}