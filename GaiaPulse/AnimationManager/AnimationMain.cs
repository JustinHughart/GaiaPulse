using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using GaiaPulse.TextureManager;

namespace GaiaPulse.AnimationManager
{
    public partial class AnimationMain : Form
    {
        String CharacterName;
        List<String> CostumeList;
        List<String> TypeList;

        public AnimationMain(String CharacterName, List<String> CostumeList)
        {
            InitializeComponent();

            this.CharacterName = CharacterName;
            this.CostumeList = CostumeList;

            TypeList = new List<String>();

            LoadTypeList();
            LoadAnimList();
        }

        private void LoadTypeList()
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

            TypeList.Clear();

            TextReader Reader = new StreamReader(FilePath);

            String Line;

            while ((Line = Reader.ReadLine()) != null)
            {
                TypeList.Add(Line);
            }

            Reader.Close();

            cboType.Items.Add("All");

            foreach (var Type in TypeList)
            {
                cboType.Items.Add(Type);
            }

            cboType.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnimationOptions Options = new AnimationOptions();
            Options.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewAnimationScreen NewAnimScreen = new NewAnimationScreen(CharacterName, TypeList);
            NewAnimScreen.Show();
        }

        private void LoadAnimList()
        {
            lstAnimation.Items.Clear();

            String DirectoryString = Global.AppDir + "/Characters/" + CharacterName + "/Animations/";

            var FileList = Directory.EnumerateFiles(DirectoryString);

            if (cboType.Text == "All")
            {
                foreach (var file in FileList)
                {
                    String String = file.Substring(DirectoryString.Length);

                    if (String.EndsWith(".gad"))
                    {
                        lstAnimation.Items.Add(String.Substring(0, String.Length - 4));
                    }
                }
            }
            else
            {
                AnimationProfile Profile = null;

                foreach (var Path in FileList)
                {
                    String String = Path.Substring(DirectoryString.Length);

                    if (String.EndsWith(".gad"))
                    {
                        Stream Stream = File.Open(Path, FileMode.Open);
                        BinaryFormatter Formatter = new BinaryFormatter();

                        Profile = (AnimationProfile)Formatter.Deserialize(Stream);
                        Stream.Close();

                        if (Profile.Type == cboType.Text)
                        {
                            lstAnimation.Items.Add(String.Substring(0, String.Length - 4));
                        }
                    }
                }
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAnimList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lstAnimation.SelectedItem != null)
            {
                DialogResult Result = MessageBox.Show("Are you sure you want to delete this animation?", "Delete?", MessageBoxButtons.OKCancel);

                if (Result == DialogResult.OK)
                {
                    String DeleteFile = lstAnimation.Items[lstAnimation.SelectedIndex].ToString();
                    File.Delete(Global.AppDir + "/Characters/" + CharacterName + "/Animations/" + DeleteFile + ".gad");
                    MessageBox.Show("File deleted.");
                }
                else
                {
                    MessageBox.Show("Deletion aborted.");
                }
            }
            else
            {
                MessageBox.Show("Select an animation to delete first.");
            }

            LoadAnimList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstAnimation.SelectedItem != null)
            {
                AnimationEditor CostumeSelect = new AnimationEditor(lstAnimation.Items[lstAnimation.SelectedIndex].ToString(), CharacterName, CostumeList);
                CostumeSelect.Show();
            }
            else
            {
                MessageBox.Show("Please select an animation.");
            }
        }
    }
}

