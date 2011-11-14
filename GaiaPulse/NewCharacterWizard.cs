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
    public partial class NewCharacterWizard : Form
    {
        public NewCharacterWizard()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e) //Cancel
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //Okay
        {
            String Name = txtName.Text;

            if (Directory.Exists(Global.AppDir + "/Characters/" + Name) == false)
            {
                Directory.CreateDirectory(Global.AppDir + "/Characters/" + Name);
                Directory.CreateDirectory(Global.AppDir + "/Characters/" + Name + "/Textures");
                Directory.CreateDirectory(Global.AppDir + "/Characters/" + Name + "/Parts");
                Directory.CreateDirectory(Global.AppDir + "/Characters/" + Name + "/Animations");
                Directory.CreateDirectory(Global.AppDir + "/Characters/" + Name + "/Game_Output");
                
                Global.CurrentForm = new EntryForm(Name);
            }
            else
            {
                MessageBox.Show("Character already exists. Cancelling operation.");
            }
            this.Close();
        }

        private void NewCharacterWizard_Load(object sender, EventArgs e)
        {

        }
    }
}
