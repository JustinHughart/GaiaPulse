using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GaiaPulse.AnimationManager
{
    public partial class NewAnimationScreen : Form
    {
        String CharacterName;
        List<String> TypeList;

        public NewAnimationScreen(String CharacterName, List<String> TypeList)
        {
            InitializeComponent();
            this.CharacterName = CharacterName;
            this.TypeList = TypeList;

            foreach (var Type in TypeList)
            {
                cboType.Items.Add(Type);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String ID = txtID.Text;

            String Path = Global.AppDir + "/Characters/" + CharacterName + "/Animations/" + ID + ".gad";

            if (ID != "")
            {
                if (cboType.Text != "")
                {
                    if (Helper.FileNameValid(ID))
                    {
                        if (!File.Exists(Path))
                        {
                            AnimationProfile Profile = new AnimationProfile();
                            Profile.SetAnimationType(cboType.Text);
                            Profile.SetID(ID);
                            Save(Profile, Path);
                            Close();
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
                    MessageBox.Show("Please choose a proper type.");
                }
            }
            else
            {
                MessageBox.Show("Please enter the name of the new animation.");
            }

        }

        public void Save(AnimationProfile Profile, String Path)
        {
            Stream Stream = File.Open(Path, FileMode.Create);
            BinaryFormatter Formatter = new BinaryFormatter();

            Formatter.Serialize(Stream, Profile);
            Stream.Close();
        }
    }
}
