using System;
using System.IO;
using System.Windows.Forms;

namespace GaiaPulse.AnimationManager
{
    public partial class AnimationOptions : Form
    {
        public AnimationOptions()
        {
            InitializeComponent();
            LoadList();
        }

        private void LoadList()
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

            lstTypes.Items.Clear();
            
            TextReader Reader = new StreamReader(FilePath);

            String Line;

            while ((Line = Reader.ReadLine()) != null)
            {
                lstTypes.Items.Add(Line);
            }

            Reader.Close();
        }

        private void SaveList()
        {
            String FilePath = Global.AppDir + "/CommonData/" + "AnimTypes.dat";

            File.Delete(FilePath);

            TextWriter Writer = new StreamWriter(FilePath);

            foreach (String Type in lstTypes.Items)
            {
                Writer.WriteLine(Type);
            }

            Writer.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtType.Text != "")
            {
                if (lstTypes.Items.Contains(txtType.Text) == false)
                {
                    if (Helper.FileNameValid(txtType.Text) == true)
                    {
                        lstTypes.Items.Add(txtType.Text);
                        SaveList();
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
            if (lstTypes.Items.Count > 1)
            {
                lstTypes.Items.RemoveAt(lstTypes.SelectedIndex);

                SaveList();
            }
            else
            {
                MessageBox.Show("There must always be at least one type.");
            }
        }
    }
}