using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using GaiaPulse.AnimationManager;
using GaiaPulse.PartManager;
using GaiaPulse.TextureManager;

namespace GaiaPulse
{
    public partial class CharacterScreen : Form
    {
        List<String> Textures;
        List<String> Parts;
        List<String> Animations;

        bool FileInUse = false;

        String CharacterName;
        String CharacterPath;

        public CharacterScreen(String Name, String Path)
        {
            Textures = new List<string>();
            Parts = new List<string>();
            Animations = new List<string>();

            CharacterPath = Path;

            LoadCharacter(Path);
            InitializeComponent();
            Text = "Gaia Pulse: " + Name;
            lblname.Text = Name;
            CharacterName = Name;

            DetectData();
            LoadCostumeList();
        }

        private void LoadCharacter(String Path)
        {
        }

        private void DetectData()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> CostumeList = new List<string>();

            foreach (var item in lstCostumes.Items)
            {
                CostumeList.Add(item.ToString());
            }

            TextureMain TextureManager = new TextureMain(CharacterName, CharacterPath, CostumeList);
            TextureManager.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<String> CostumeList = new List<string>();

            foreach (var item in lstCostumes.Items)
            {
                CostumeList.Add(item.ToString());
            }

            PartMain PartsManager = new PartMain(CharacterName, CharacterPath, CostumeList);
            PartsManager.Show();
        }

        private void LoadCostumeList()
        {
            lstCostumes.Items.Clear();

            String FilePath = CharacterPath + "CostumeList.txt";

            if (File.Exists(FilePath))
            {
                if (FileInUse == false)
                {
                    FileInUse = true;
                    TextReader Reader = new StreamReader(FilePath);

                    String Line;

                    while ((Line = Reader.ReadLine()) != null)
                    {
                        lstCostumes.Items.Add(Line);
                    }

                    Reader.Close();

                    FileInUse = false;
                }
            }
            else
            {
                if (FileInUse == false)
                {
                    FileInUse = true;
                    TextWriter Writer = new StreamWriter(FilePath);

                    Writer.WriteLine("Battle");

                    Writer.Close();

                    FileInUse = true;

                    LoadCostumeList();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lstCostumes.Items.Count > 1)
            {
                lstCostumes.Items.RemoveAt(lstCostumes.SelectedIndex);

                SaveCostumesList();
            }
            else
            {
                MessageBox.Show("There must always be at least one costume.");
            }
        }

        private void SaveCostumesList()
        {
            if (FileInUse == false)
            {
                FileInUse = true;

                String FilePath = CharacterPath + "CostumeList.txt";

                File.Delete(FilePath);

                TextWriter Writer = new StreamWriter(FilePath);

                for (int i = 0; i < lstCostumes.Items.Count; i++)
                {
                    Writer.WriteLine(lstCostumes.Items[i]);
                }

                Writer.Close();

                FileInUse = false;

                LoadCostumeList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtCostumeInput.Text != "")
            {
                if (lstCostumes.Items.Contains(txtCostumeInput.Text) == false)
                {
                    if (Helper.FileNameValid(txtCostumeInput.Text) == true)
                    {
                        lstCostumes.Items.Add(txtCostumeInput.Text);
                        SaveCostumesList();
                    }
                    else
                    {
                        MessageBox.Show("Name is invalid.");
                    }
                }
                else
                {
                    MessageBox.Show("This entry already exists.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a name into the text box below.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<String> CostumeList = new List<string>();

            foreach (var item in lstCostumes.Items)
            {
                CostumeList.Add(item.ToString());
            }

            AnimationMain AnimationMain = new AnimationMain(CharacterName, CharacterPath, CostumeList);
            AnimationMain.Show();
        }
    }
}