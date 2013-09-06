using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GaiaPulse.SC.FBFAnimation;
using GaiaPulse.SC.FrameAnimation;
using Microsoft.Xna.Framework;

namespace GaiaPulse.AnimationManager
{
    public partial class NodeProperties : Form
    {
        private AnimNode _node;

        Dictionary<String, String> _dict;

        public NodeProperties(AnimNode node)
        {
            InitializeComponent();
            
            
            _node = node;

            LoadData();
        }

        private void LoadTags()
        {
            _dict = _node.GetTags();

            foreach (var item in _dict)
            {
                object[] str = new object[2];
                str[0] = item.Key;
                str[1] = item.Value;

                var row = new DataGridViewRow();
                row.CreateCells(dgvTags);
                row.SetValues(str);

                dgvTags.Rows.Add(row);
            }
        }

        private void SaveTags()
        {
            _dict.Clear();

            foreach (DataGridViewRow row in dgvTags.Rows)
            {
                DataGridViewCell keycell = row.Cells[0];
                DataGridViewCell valuecell = row.Cells[1];

                String key = "";
                String value = "";

                if (keycell != null)
                {
                    if (keycell.Value != null)
                    {
                        key = keycell.Value.ToString();
                    }
                }

                if (valuecell != null)
                {
                    if (valuecell.Value != null)
                    {
                        value = valuecell.Value.ToString();
                    }
                }

                if (key != "")
                {
                    _dict.Add(key, value);
                }
            }

            _node.SetTags(_dict);
        }

        private void LoadData()
        {
            txtVelocityX.Text = _node.Velocity.X.ToString();
            txtVelocityY.Text = _node.Velocity.Y.ToString();
            chkSmoothX.Checked = _node.SmoothX;
            chkSmoothY.Checked = _node.SmoothY;

            txtRotation.Text = _node.Rotation.ToString();
            chkSmoothRotation.Checked = _node.SmoothRotation;

            txtSound.Text = _node.SoundID;

            txtHitsparkGraphic.Text = _node.HitsparkGraphic;
            txtHitsparkSound.Text = _node.HitsparkSound;

            txtTimeTillNext.Text = _node.TimeTillNext.ToString();

            LoadTags();
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            _node.SetVelocity(new Vector2(float.Parse(txtVelocityX.Text), float.Parse(txtVelocityY.Text)), chkSmoothX.Checked, chkSmoothY.Checked);
            _node.SetRotation(float.Parse(txtRotation.Text), chkSmoothRotation.Checked);
            _node.SetSound(txtSound.Text);
            _node.SetHitspark(txtHitsparkGraphic.Text, txtHitsparkSound.Text);
            _node.SetTTN(int.Parse(txtTimeTillNext.Text));

            SaveTags();

            Close();
        }
    }
}
