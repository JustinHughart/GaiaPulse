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
using GaiaPulse.PartManager.PartData;

namespace GaiaPulse.PartManager
{
    public partial class PartMain : Form
    {
        String CharacterName;
        List<String> CostumeList;
      
        public PartMain(String CharacterName, List<String> CostumeList)
        {
            this.CharacterName = CharacterName;
            this.CostumeList = CostumeList;
            InitializeComponent();
            this.Text = "Parts Manager: " + CharacterName;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtPartName.Text != "")
            {
                if (Helper.FileNameValid(txtPartName.Text))
                {
                    if (!lstPartList.Items.Contains(txtPartName.Text))
                    {
                        PartCommonData Data = new PartCommonData();
                        Data.SetAnchorNumber(1);

                        String DirectoryPath = Global.AppDir + "/Characters/" + CharacterName + "/Parts/" + txtPartName.Text + "/";

                        Directory.CreateDirectory(DirectoryPath);

                        String FilePath = DirectoryPath + "commondata.dat";

                        SerializeCommonData(Data, FilePath);

                        LoadList();
                    }
                    else
                    {
                        MessageBox.Show("Part already exists.");
                    }
                }
                else
                {
                    MessageBox.Show("Name is invalid.");
                }
            }
            else
            {
                MessageBox.Show("Please name the new part using the text box below.");
            }
        }

        private void LoadList()
        {
            String DirectoryPath = Global.AppDir + "/Characters/" + CharacterName + "/Parts/";

            lstPartList.Items.Clear();

            var DirectoryList = Directory.EnumerateDirectories(DirectoryPath);

            foreach (var Part in DirectoryList)
            {
                String Name = Part.Substring(DirectoryPath.Length);

                lstPartList.Items.Add(Name);
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
            LoadList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstPartList.SelectedItem != null)
            {
                DialogResult Result = MessageBox.Show("Are you sure you want to delete this part?", "Delete?", MessageBoxButtons.OKCancel);

                if (Result == DialogResult.OK)
                {
                    String DeleteDir = Global.AppDir + "/Characters/" + CharacterName + "/Parts/" + lstPartList.Items[lstPartList.SelectedIndex];

                    Directory.Delete(DeleteDir, true);

                    MessageBox.Show("File deleted.");

                    LoadList();
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
            if (lstPartList.SelectedItem != null)
            {
                PartCostumeSelect CostumeSelect = new PartCostumeSelect(lstPartList.Items[lstPartList.SelectedIndex].ToString(), CharacterName, CostumeList);
                CostumeSelect.Show();
            }
            else
            {
                MessageBox.Show("Please select a part.");
            }
        }
    }
}
