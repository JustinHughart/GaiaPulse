namespace GaiaPulse.TextureManager
{
    partial class TextureMain
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
            this.btnAddTexture = new System.Windows.Forms.Button();
            this.btnViewTexture = new System.Windows.Forms.Button();
            this.btnChangeTexture = new System.Windows.Forms.Button();
            this.btnDeleteTexture = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lstTextures = new System.Windows.Forms.ListBox();
            this.picTexturePreview = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picTexturePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddTexture
            // 
            this.btnAddTexture.Location = new System.Drawing.Point(13, 13);
            this.btnAddTexture.Name = "btnAddTexture";
            this.btnAddTexture.Size = new System.Drawing.Size(75, 23);
            this.btnAddTexture.TabIndex = 0;
            this.btnAddTexture.Text = "Add Texture";
            this.btnAddTexture.UseVisualStyleBackColor = true;
            this.btnAddTexture.Click += new System.EventHandler(this.btnAddTexture_Click);
            // 
            // btnViewTexture
            // 
            this.btnViewTexture.Location = new System.Drawing.Point(393, 12);
            this.btnViewTexture.Name = "btnViewTexture";
            this.btnViewTexture.Size = new System.Drawing.Size(81, 23);
            this.btnViewTexture.TabIndex = 5;
            this.btnViewTexture.Text = "View Texture";
            this.btnViewTexture.UseVisualStyleBackColor = true;
            this.btnViewTexture.Click += new System.EventHandler(this.btnViewTexture_Click);
            // 
            // btnChangeTexture
            // 
            this.btnChangeTexture.Location = new System.Drawing.Point(94, 13);
            this.btnChangeTexture.Name = "btnChangeTexture";
            this.btnChangeTexture.Size = new System.Drawing.Size(91, 23);
            this.btnChangeTexture.TabIndex = 1;
            this.btnChangeTexture.Text = "Change Texture";
            this.btnChangeTexture.UseVisualStyleBackColor = true;
            this.btnChangeTexture.Click += new System.EventHandler(this.btnChangeTexture_Click);
            // 
            // btnDeleteTexture
            // 
            this.btnDeleteTexture.Location = new System.Drawing.Point(191, 13);
            this.btnDeleteTexture.Name = "btnDeleteTexture";
            this.btnDeleteTexture.Size = new System.Drawing.Size(91, 23);
            this.btnDeleteTexture.TabIndex = 2;
            this.btnDeleteTexture.Text = "Delete Texture";
            this.btnDeleteTexture.UseVisualStyleBackColor = true;
            this.btnDeleteTexture.Click += new System.EventHandler(this.btnDeleteTexture_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(480, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Assign Costumes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstTextures
            // 
            this.lstTextures.FormattingEnabled = true;
            this.lstTextures.Location = new System.Drawing.Point(13, 80);
            this.lstTextures.Name = "lstTextures";
            this.lstTextures.Size = new System.Drawing.Size(218, 368);
            this.lstTextures.TabIndex = 7;
            // 
            // picTexturePreview
            // 
            this.picTexturePreview.Location = new System.Drawing.Point(247, 80);
            this.picTexturePreview.Name = "picTexturePreview";
            this.picTexturePreview.Size = new System.Drawing.Size(332, 367);
            this.picTexturePreview.TabIndex = 6;
            this.picTexturePreview.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Texture Selection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Texture Preview";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(288, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Preview Texture";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TextureMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 469);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picTexturePreview);
            this.Controls.Add(this.lstTextures);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteTexture);
            this.Controls.Add(this.btnChangeTexture);
            this.Controls.Add(this.btnViewTexture);
            this.Controls.Add(this.btnAddTexture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "TextureMain";
            this.Text = "TextureMain";
            ((System.ComponentModel.ISupportInitialize)(this.picTexturePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddTexture;
        private System.Windows.Forms.Button btnViewTexture;
        private System.Windows.Forms.Button btnChangeTexture;
        private System.Windows.Forms.Button btnDeleteTexture;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lstTextures;
        private System.Windows.Forms.PictureBox picTexturePreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}