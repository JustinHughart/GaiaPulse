﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GaiaPulse.TextureManager
{
    public partial class TextureMain : Form
    {
        String CharacterName;
        String CharacterPath;
        List<String> CostumeList;

        public TextureMain(String Name, String Path, List<String> CostumeList)
        {
            CharacterName = Name;
            CharacterPath = Path;
            this.CostumeList = CostumeList;
            InitializeComponent();
            this.Text = "Texture Manager: " + CharacterName;

            LoadList();
        }

        private void LoadList()
        {
            lstTextures.Items.Clear();

            String DirectoryString = CharacterPath + "Textures/";

            var FileList = Directory.EnumerateFiles(DirectoryString);

            foreach (var file in FileList)
            {
                String String = file.Substring(DirectoryString.Length);

                if (String.EndsWith(".png"))
                {
                    lstTextures.Items.Add(String);
                }
            }
        }

        private void btnAddTexture_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Multiselect = false;
            Dialog.AddExtension = true;
            Dialog.Filter = "PNG Image | *.png";
            Dialog.ShowDialog();

            try
            {
                String ID = Dialog.SafeFileName.Substring(0, Dialog.SafeFileName.Length - 4);

                if (Helper.FileNameValid(ID))
                {
                    if (File.Exists(CharacterPath + "Textures/" + Dialog.SafeFileName))
                    {
                        MessageBox.Show("File with that name already exists.");
                    }
                    else
                    {
                        File.Copy(Dialog.FileName, CharacterPath + "Textures/" + Dialog.SafeFileName);

                        TextureProfile NewData = new TextureProfile(ID, new List<String>());

                        String Path = CharacterPath + "Textures/" + ID + ".tex";

                        SerializeTextureData(NewData, Path);
                    }
                }
                else
                {
                    MessageBox.Show("Name is invalid.");
                }
            }
            catch (NullReferenceException)
            {
                throw;
            }

            LoadList();
        }

        private void btnChangeTexture_Click(object sender, EventArgs e)
        {
            if (lstTextures.SelectedItem != null)
            {
                String OldFile = lstTextures.Items[lstTextures.SelectedIndex].ToString();

                OpenFileDialog Dialog = new OpenFileDialog();
                Dialog.Multiselect = false;
                Dialog.AddExtension = true;
                Dialog.Filter = "PNG Image | *.png";
                Dialog.ShowDialog();

                if (File.Exists(Dialog.FileName))
                {
                    File.Delete(CharacterPath + "Textures/" + OldFile);
                    File.Copy(Dialog.FileName, CharacterPath + "Textures/" + OldFile);
                }
                else
                {
                    MessageBox.Show("File doesn't exist.");
                }

                LoadList();
            }
            else
            {
                MessageBox.Show("Select a texture to replace first.");
            }
        }

        private void btnDeleteTexture_Click(object sender, EventArgs e)
        {
            if (lstTextures.SelectedItem != null)
            {
                DialogResult Result = MessageBox.Show("Are you sure you want to delete this texture?", "Delete?", MessageBoxButtons.OKCancel);

                if (Result == DialogResult.OK)
                {
                    String DeleteFile = lstTextures.Items[lstTextures.SelectedIndex].ToString();
                    File.Delete(CharacterPath + "Textures/" + DeleteFile);
                    File.Delete(CharacterPath + "Textures/" + DeleteFile.Substring(0, DeleteFile.Length - 4) + ".tex");
                    MessageBox.Show("File deleted.");
                }
                else
                {
                    MessageBox.Show("Deletion aborted.");
                }
            }
            else
            {
                MessageBox.Show("Select a texture to delete first.");
            }

            LoadList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstTextures.SelectedItem != null)
            {
                String Temp = lstTextures.Items[lstTextures.SelectedIndex].ToString();

                String Path = CharacterPath + "Textures/" + Temp.Substring(0, Temp.Length - 4) + ".tex";

                CostumeAssign CostumeAssign = new CostumeAssign(CostumeList, Path);
                CostumeAssign.Show();
            }
            else
            {
                MessageBox.Show("Select a texture to assign to costumes first.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lstTextures.SelectedItem != null)
            {
                String File = CharacterPath + "Textures/" + lstTextures.Items[lstTextures.SelectedIndex].ToString();

                picTexturePreview.ImageLocation = File;
                picTexturePreview.Load();
            }
            else
            {
                MessageBox.Show("Select a texture to preview first.");
            }
        }

        private void btnViewTexture_Click(object sender, EventArgs e)
        {
            if (lstTextures.SelectedItem != null)
            {
                String File = CharacterPath + "Textures/" + lstTextures.Items[lstTextures.SelectedIndex].ToString();

                TextureViewer Viewer = new TextureViewer(File);
                Viewer.Show();
            }
            else
            {
                MessageBox.Show("Select a texture to view first.");
            }
        }

        private void SerializeTextureData(TextureProfile Data, String FileName)
        {
            Stream Stream = File.Open(FileName, FileMode.Create);
            BinaryFormatter Formatter = new BinaryFormatter();

            Formatter.Serialize(Stream, Data);
            Stream.Close();
        }
    }
}