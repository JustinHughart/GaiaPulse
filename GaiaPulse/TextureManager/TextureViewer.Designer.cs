namespace GaiaPulse.TextureManager
{
    partial class TextureViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picTexture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTexture)).BeginInit();
            this.SuspendLayout();
            // 
            // picTexture
            // 
            this.picTexture.Location = new System.Drawing.Point(0, 0);
            this.picTexture.Name = "picTexture";
            this.picTexture.Size = new System.Drawing.Size(100, 50);
            this.picTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTexture.TabIndex = 0;
            this.picTexture.TabStop = false;
            // 
            // TextureViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(116, 56);
            this.Controls.Add(this.picTexture);
            this.Name = "TextureViewer";
            this.Text = "TextureViewer";
            ((System.ComponentModel.ISupportInitialize)(this.picTexture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picTexture;
    }
}