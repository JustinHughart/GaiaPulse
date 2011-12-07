using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GaiaPulse.AnimationManager
{
    public partial class AnimationEditor : Form
    {
        String CharacterName;
        List<String> TypeList;
        String ID;

        public AnimationEditor(String ID, String CharacterName, List<String> TypeList)
        {
            this.CharacterName = CharacterName;
            this.TypeList = TypeList;
            InitializeComponent();
        }



    }
}
