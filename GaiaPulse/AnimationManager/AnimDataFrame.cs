using System;
using System.Windows.Forms;
using GaiaPulse.SC.FrameAnimation;

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
            txtDefaultTexture.Text = _anim.DefaultTexture;
            chkLooping.Checked = _anim.IsLooping;
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            _anim.SetID(txtID.Text);
            _anim.DefaultTexture = txtDefaultTexture.Text;
            _anim.SetLooping(chkLooping.Checked);

            this.Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnChooseTextureClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Program.TexturePath;
            ofd.RestoreDirectory = false;
            ofd.Multiselect = false;
            ofd.Filter = "Portable Network Graphics | *.png";
            ofd.ValidateNames = true;

            ofd.ShowDialog();

            if (ofd.FileName != "")
            {
                if (ofd.CheckFileExists)
                {
                    String filepath = ofd.FileName;

                    Uri uri1 = new Uri(Program.TexturePath);
                    Uri uri2 = new Uri(filepath);

                    txtDefaultTexture.Text = uri1.MakeRelativeUri(uri2).ToString();
                }
            }
        }
    }
}
