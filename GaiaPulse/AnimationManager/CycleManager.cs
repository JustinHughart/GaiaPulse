using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GaiaPulse.SC.FrameAnimation;
using Microsoft.Xna.Framework;


namespace GaiaPulse.AnimationManager
{
    public partial class CycleManager : Form
    {
        EditorControl _editor;

        FrameAnimation _anim;
        BindingSource _bindingsource;
        
        public CycleManager(EditorControl editor, FrameAnimation anim)
        {
            InitializeComponent();

            _editor = editor;
            _anim = anim;

            LoadFrames();
        }

        public void LoadFrames()
        {
            List<DrawData> drawdata = new List<DrawData>();

            foreach (var dd in _anim.GetDrawDataList())
            {
                    drawdata.Add(dd);
            }

            lstFrames.DataSource = drawdata;

            _bindingsource = new BindingSource();
            _bindingsource.DataSource = _anim.Nodes;

            lstCycle.DataSource = _bindingsource;
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            if (lstFrames.SelectedItem != null)
            {
                AnimNode node = new AnimNode(_anim, 10);

                node.SetDrawData((DrawData)lstFrames.SelectedItem);

                _bindingsource.Add(node);
            }
        }

        private void Button1Click(object sender, EventArgs e)
        {
            if (lstCycle.SelectedItem != null)
            {
                _bindingsource.RemoveAt(lstCycle.SelectedIndex);
            }
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            _editor.SaveAnim();
            this.Close();
        }

        private void BtnPropertiesClick(object sender, EventArgs e)
        {
            if (lstCycle.SelectedItem != null)
            {
                NodeProperties np = new NodeProperties((AnimNode)lstCycle.SelectedItem);
                np.Show();
            }
        }
    }
}
