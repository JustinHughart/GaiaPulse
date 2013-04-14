using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GaiaPulse.TextureManager
{
    public partial class CostumeAssign : Form
    {
        String FilePath;
        TextureProfile Profile;
        bool FileInUse = false;

        public CostumeAssign(List<String> Costumes, String LoadPath)
        {
            InitializeComponent();

            FilePath = LoadPath;

            foreach (String Costume in Costumes)
            {
                lstAvailable.Items.Add(Costume);
            }

            int end;
            bool stop = false;

            for (end = LoadPath.Length - 1; stop == false; end--)
            {
                if (LoadPath[end] == '/' || LoadPath[end] == '\\')
                {
                    stop = true;
                    end += 2;
                }
            }

            this.Text = "Assign Texture To Costume(s): " + FilePath.Substring(end, FilePath.Substring(end).Length - 4);

            LoadAssignedList(FilePath);
        }

        private void CostumeAssign_Load(object sender, EventArgs e)
        {
        }

        private void LoadAssignedList(String Path)
        {
            Stream Stream = File.Open(Path, FileMode.Open);
            BinaryFormatter Formatter = new BinaryFormatter();

            Profile = (TextureProfile)Formatter.Deserialize(Stream);
            Stream.Close();

            foreach (String Costume in Profile.CostumeList)
            {
                lstAssigned.Items.Add(Costume);
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (lstAvailable.SelectedItem != null)
            {
                String Selected = lstAvailable.Items[lstAvailable.SelectedIndex].ToString();

                if (lstAssigned.Items.Contains(Selected))
                {
                    MessageBox.Show("Costume is already assigned.");
                }
                else
                {
                    lstAssigned.Items.Add(Selected);
                }
            }
            else
            {
                MessageBox.Show("Please select a costume first.");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstAssigned.SelectedItem != null)
            {
                lstAssigned.Items.Remove(lstAssigned.SelectedItem);
            }
            else
            {
                MessageBox.Show("Please select an item to remove first.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FileInUse = true;
            Profile.CostumeList.Clear();

            foreach (String Costume in lstAssigned.Items)
            {
                Profile.CostumeList.Add(Costume);
            }

            Stream Stream = File.Open(FilePath, FileMode.Create);
            BinaryFormatter Formatter = new BinaryFormatter();

            Formatter.Serialize(Stream, Profile);
            Stream.Close();
            FileInUse = false;
        }
    }
}