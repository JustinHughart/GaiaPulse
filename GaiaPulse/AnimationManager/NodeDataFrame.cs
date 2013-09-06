using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GaiaPulse.SC.FBFAnimation;
using GaiaPulse.SC.FrameAnimation;
using Microsoft.Xna.Framework;

namespace GaiaPulse.AnimationManager
{
    public partial class NodeDataFrame : Form
    {
        DrawData _drawdata;
        
        public NodeDataFrame(DrawData drawdata)
        {
            InitializeComponent();

            _drawdata = drawdata;

            InitValues();
        }

        private void InitValues()
        {
            txtID.Text =  _drawdata.ID;
            txtTexture.Text = _drawdata.TextureName;
            txtDrawX.Text = _drawdata.DrawArea.X.ToString();
            txtDrawY.Text = _drawdata.DrawArea.Y.ToString();
            txtDrawW.Text = _drawdata.DrawArea.Width.ToString();
            txtDrawH.Text = _drawdata.DrawArea.Height.ToString();
            txtXOffR.Text = _drawdata.Offsets.X.ToString();
            txtXOffL.Text = _drawdata.Offsets.Z.ToString();
            txtYOff.Text = _drawdata.Offsets.Y.ToString();
            txtOriginX.Text = _drawdata.Origin.X.ToString();
            txtOriginY.Text = _drawdata.Origin.Y.ToString();
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            _drawdata.SetID(txtID.Text);
            _drawdata.SetTexture(txtTexture.Text);
            _drawdata.SetDrawArea(new Rectangle(int.Parse(txtDrawX.Text), int.Parse(txtDrawY.Text), int.Parse(txtDrawW.Text), int.Parse(txtDrawH.Text)));
            _drawdata.SetOffsets(new Vector3(float.Parse(txtXOffR.Text),float.Parse(txtYOff.Text), float.Parse(txtXOffL.Text)));
            _drawdata.SetOrigin(new Vector2(float.Parse(txtOriginX.Text), float.Parse(txtOriginY.Text)));

            this.Close();
        }

    }
}
