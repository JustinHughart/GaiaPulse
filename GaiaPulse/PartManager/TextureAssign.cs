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

namespace GaiaPulse.PartManager
{
    public partial class TextureAssign : Form
    {
        String CharacterName;
        String CostumeName;

        PartEditor Owner;

        public TextureAssign(String CharacterName, String CostumeName, PartEditor Owner)
        {
            this.CharacterName = CharacterName;
            this.CostumeName = CostumeName;
            this.Owner = Owner;
            InitializeComponent();
            LoadList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextureAssign_Load(object sender, EventArgs e)
        {

        }

        private void LoadList()
        {
            String DirectoryString = Global.AppDir + "/Characters/" + CharacterName + "/Textures/";

            List<String> FileList = Directory.EnumerateFiles(DirectoryString).ToList();

            foreach (String File in FileList)
            {
                if (File.EndsWith(".tex"))
                {
                    LoadData(File);
                }
            }
        }

        private void LoadData(String Path)
        {
            Stream Stream = File.Open(Path, FileMode.Open);
            BinaryFormatter Formatter = new BinaryFormatter();

            TextureProfile Profile = (TextureProfile)Formatter.Deserialize(Stream);

            if (Profile.CostumeList.Contains(CostumeName))
            {
                lstTextures.Items.Add(Profile.ID);
            }

            Stream.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (lstTextures.SelectedItem != null)
            {
                Owner.LoadTexture(Global.AppDir + "/Characters/" + CharacterName + "/Textures/" + lstTextures.SelectedItem + ".png");
                Owner.SetTexName("Characters/" + CharacterName + "/Textures/" + lstTextures.SelectedItem + ".png");
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a texture.");
            }
        }
    }
}
