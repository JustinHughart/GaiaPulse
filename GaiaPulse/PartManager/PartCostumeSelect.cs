﻿using System;
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
    public partial class PartCostumeSelect : Form
    {
        String PartName;
        String CharacterName;
        List<String> CostumeList;
        PartCommonData CommonData;
        
        public PartCostumeSelect(String PartName,  String CharacterName, List<String> CostumeList)
        {
            this.CharacterName = CharacterName;
            this.CostumeList = CostumeList;
            this.PartName = PartName;
            InitializeComponent();

            this.Text = "Costume Select: " + PartName;

            foreach (String Costume in CostumeList)
            {
                lstCostume.Items.Add(Costume);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommonData.SetAnchorNumber((int)numericUpDown1.Value);

            String Path = Global.AppDir + "/Characters/" + CharacterName + "/Parts/" + PartName + "/commondata.dat";

            SerializeCommonData(CommonData, Path);
        }

        private void SerializeCommonData(PartCommonData Data, String FileName)
        {
            Stream Stream = File.Open(FileName, FileMode.Create);
            BinaryFormatter Formatter = new BinaryFormatter();

            Formatter.Serialize(Stream, Data);
            Stream.Close();
        }

        private void LoadSerializedCommonData(String Path)
        {
            Stream Stream = File.Open(Path, FileMode.Open);
            BinaryFormatter Formatter = new BinaryFormatter();

            CommonData = (PartCommonData)Formatter.Deserialize(Stream);
            Stream.Close();
        }

        private void LoadData()
        {
            String Path = Global.AppDir + "/Characters/" + CharacterName + "/Parts/" + PartName + "/commondata.dat";
            LoadSerializedCommonData(Path);
            numericUpDown1.Value = CommonData.AnchorPoints;
        }

        private void PartCostumeSelect_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstCostume.SelectedItem != null)
            {
                LoadData();
                PartEditor Editor = new PartEditor(CharacterName, PartName, lstCostume.SelectedItem.ToString(), int.Parse(numericUpDown1.Text));
                Editor.Show();
            }
            else
            {
                MessageBox.Show("Please select an item.");
            }
        }
    }
}
