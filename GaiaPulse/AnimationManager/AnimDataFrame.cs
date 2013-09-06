using System;
using System.Windows.Forms;
using GaiaPulse.SC.FBFAnimation;

namespace GaiaPulse.AnimationManager
{
    public partial class AnimDataFrame : Form
    {
        FrameAnimation _anim;

        public AnimDataFrame(FrameAnimation anim)
        {
            _anim = anim;

            InitializeComponent();

            Init();
        }

        private void Init()
        {
            txtID.Text = _anim.Name;
            txtAutoRotate.Text = _anim.AutoRotation.ToString();
            chkLooping.Checked = _anim.IsLooping;
        }

        private void Button1Click(object sender, EventArgs e)
        {
            _anim.SetID(txtID.Text);
            _anim.SetAutoRotation(float.Parse(txtAutoRotate.Text));
            _anim.SetLooping(chkLooping.Checked);

            this.Close();
        }
    }
}
