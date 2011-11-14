using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

            LoadList();

            foreach (var Type in TypeList)
            {
                cboType.Items.Add(Type);
            }
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

            TypeList.Clear();

            TextReader Reader = new StreamReader(FilePath);

            String Line;

            while ((Line = Reader.ReadLine()) != null)
            {
                TypeList.Add(Line);
            }

            Reader.Close();
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
    }
}
