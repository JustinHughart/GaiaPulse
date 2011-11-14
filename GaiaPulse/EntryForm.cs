using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GaiaPulse
{
    public partial class EntryForm : Form
    {
        public EntryForm()
        {
            InitializeComponent();
        }

        public EntryForm(String Name)
        {
            LoadCharacter(Name);
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Global.AppDir + "/Characters/") == false)
            {
                Directory.CreateDirectory(Global.AppDir + "/Characters/");
            }

            var DirectoryList = Directory.EnumerateDirectories(Global.AppDir + "/Characters/");

            foreach (var directory in DirectoryList)
            {
                String String = directory.Substring(Global.AppDir.Length + "/Characters/".Length);
                
                CharacterBox.Items.Add(String);
            }
        }

        private void newCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.CurrentForm = new NewCharacterWizard();
            Global.CurrentForm.Show();
        }

        private void openCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadCharacter(String Name)
        {
            Global.CurrentForm = new CharacterScreen(Name);
            Global.CurrentForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newCharacterToolStripMenuItem_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadCharacter(CharacterBox.Items[CharacterBox.SelectedIndex].ToString());
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
    }
}
