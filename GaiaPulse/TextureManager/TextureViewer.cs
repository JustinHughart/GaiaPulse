using System;
using System.Windows.Forms;

namespace GaiaPulse.TextureManager
{
    public partial class TextureViewer : Form //Lets you view a texture.
    {
        public TextureViewer(String ImagePath)
        {
            InitializeComponent();

            picTexture.Load(ImagePath);

            int end;

            bool stop = false;

            for (end = ImagePath.Length - 1; stop == false; end--)
            {
                if (ImagePath[end] == '/' || ImagePath[end] == '\\')
                {
                    stop = true;
                    end += 2;
                }
            }

            this.Text = "Texture Viewer: " + ImagePath.Substring(end);
        }
    }
}