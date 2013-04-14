using System;
using System.IO;
using System.Windows.Forms;

namespace GaiaPulse
{
    public partial class EntryForm : Form
    {
        public EntryForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadTree();
        }

        private void LoadTree()
        {
            TreeCharacters.Nodes.Clear();

            if (!Directory.Exists(Global.AppDir + "/Characters/"))
            {
                Directory.CreateDirectory(Global.AppDir + "/Characters/");
            }

            String Path = Global.AppDir + "/Characters/";

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
                    TreeCharacters.Nodes.Add(Node);
                }

                if (Directory.GetFiles(directory).Length == 0)
                {
                    RecurseDir(directory + "/", Node);
                }
            }
        }

        private void CreateNewCharacter()
        {
            String CharID = Microsoft.VisualBasic.Interaction.InputBox("Please enter ID name of new character.", "Character ID Input");

            if (TreeCharacters.SelectedNode != null)
            {
                if (!TreeCharacters.SelectedNode.Text.Contains("!"))
                {
                    MessageBox.Show("Please select a folder to put the character in. Folder cannot be another character.");
                    return;
                }

                if (CharID != "")
                {
                    String Path = GetNodePath(TreeCharacters.SelectedNode) + CharID;

                    if (Directory.Exists(Path) == false)
                    {
                        Directory.CreateDirectory(Path);
                        Directory.CreateDirectory(Path + "/Textures");
                        Directory.CreateDirectory(Path + "/Parts");
                        Directory.CreateDirectory(Path + "/Animations");

                        TextWriter Writer = new StreamWriter(Path + "/CostumeList.txt");

                        Writer.WriteLine("Battle");

                        Writer.Close();

                        LoadTree();
                    }
                    else
                    {
                        MessageBox.Show("Character already exists. Cancelling operation.");
                    }
                }
                else
                {
                    MessageBox.Show("Empty input. Cancelling action.");
                }
            }
            else
            {
                MessageBox.Show("Please select a folder to put the character in.");
            }
        }

        private void openCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadCharacter(String Name, String Path)
        {
            Form Form = new CharacterScreen(Name, Path);
            Form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateNewCharacter();
        }

        private void button3_Click(object sender, EventArgs e) //Load Character.
        {
            if (TreeCharacters.SelectedNode.Text.Contains("!"))
            {
                MessageBox.Show("Please select a character, not a folder.");
            }
            else
            {
                LoadCharacter(TreeCharacters.SelectedNode.Text, GetNodePath(TreeCharacters.SelectedNode));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalOptions Options = new GlobalOptions();
            Options.Show();
        }

        private void CheckGlobalAnimFile()
        {
            String FilePath = Global.AppDir + "/CommonData/" + "AnimTypes.dat";

            if (!File.Exists(FilePath))
            {
                TextWriter Writer = new StreamWriter(FilePath);

                Writer.WriteLine("Head");
                Writer.WriteLine("Body");
                Writer.WriteLine("Weapon");

                Writer.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e) //Create Dir
        {
            String FolderID = Microsoft.VisualBasic.Interaction.InputBox("Please enter ID name of new folder.", "Folder ID Input");

            if (Helper.FileNameValid(FolderID))
            {
                String Path = "";

                if (TreeCharacters.SelectedNode == null)
                {
                    Path = Global.AppDir + "/Characters/" + FolderID;
                }
                else
                {
                    Path = GetNodePath(TreeCharacters.SelectedNode) + FolderID;
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

        private void button5_Click(object sender, EventArgs e) //Delete Dir
        {
            if (TreeCharacters.SelectedNode != null)
            {
                DialogResult Result = MessageBox.Show("Are you sure you want to delete this node and all subnodes?", "Think about it carefully.", MessageBoxButtons.YesNo);

                switch (Result)
                {
                    case DialogResult.Yes:
                        Directory.Delete(GetNodePath(TreeCharacters.SelectedNode), true);
                        LoadTree();
                        break;
                }
            }
            else
            {
                MessageBox.Show("No node selected.");
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

            Path = Global.AppDir + "/Characters/" + Path;

            return Path;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TreeCharacters.SelectedNode = null;
        }
    }
}