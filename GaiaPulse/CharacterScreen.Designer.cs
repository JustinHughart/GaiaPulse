namespace GaiaPulse
{
    partial class CharacterScreen
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
            this.btnTextureManager = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblname = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblTexture = new System.Windows.Forms.Label();
            this.lblParts = new System.Windows.Forms.Label();
            this.lblAnims = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lstCostumes = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtCostumeInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTextureManager
            // 
            this.btnTextureManager.Location = new System.Drawing.Point(12, 44);
            this.btnTextureManager.Name = "btnTextureManager";
            this.btnTextureManager.Size = new System.Drawing.Size(98, 23);
            this.btnTextureManager.TabIndex = 0;
            this.btnTextureManager.Text = "Texture Manager";
            this.btnTextureManager.UseVisualStyleBackColor = true;
            this.btnTextureManager.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name: ";
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Location = new System.Drawing.Point(47, 13);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(61, 13);
            this.lblname.TabIndex = 2;
            this.lblname.Text = "Name Here";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Part Manager";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Animations";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblTexture
            // 
            this.lblTexture.AutoSize = true;
            this.lblTexture.Location = new System.Drawing.Point(113, 49);
            this.lblTexture.Name = "lblTexture";
            this.lblTexture.Size = new System.Drawing.Size(54, 13);
            this.lblTexture.TabIndex = 5;
            this.lblTexture.Text = "Textures: ";
            // 
            // lblParts
            // 
            this.lblParts.AutoSize = true;
            this.lblParts.Location = new System.Drawing.Point(116, 78);
            this.lblParts.Name = "lblParts";
            this.lblParts.Size = new System.Drawing.Size(37, 13);
            this.lblParts.TabIndex = 6;
            this.lblParts.Text = "Parts: ";
            // 
            // lblAnims
            // 
            this.lblAnims.AutoSize = true;
            this.lblAnims.Location = new System.Drawing.Point(116, 108);
            this.lblAnims.Name = "lblAnims";
            this.lblAnims.Size = new System.Drawing.Size(64, 13);
            this.lblAnims.TabIndex = 7;
            this.lblAnims.Text = "Animations: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Costumes";
            // 
            // lstCostumes
            // 
            this.lstCostumes.FormattingEnabled = true;
            this.lstCostumes.Location = new System.Drawing.Point(12, 162);
            this.lstCostumes.Name = "lstCostumes";
            this.lstCostumes.Size = new System.Drawing.Size(213, 186);
            this.lstCostumes.TabIndex = 9;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 354);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(93, 354);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Remove";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtCostumeInput
            // 
            this.txtCostumeInput.Location = new System.Drawing.Point(12, 385);
            this.txtCostumeInput.MaxLength = 100;
            this.txtCostumeInput.Name = "txtCostumeInput";
            this.txtCostumeInput.Size = new System.Drawing.Size(215, 20);
            this.txtCostumeInput.TabIndex = 12;
            // 
            // CharacterScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 417);
            this.Controls.Add(this.txtCostumeInput);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lstCostumes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblAnims);
            this.Controls.Add(this.lblParts);
            this.Controls.Add(this.lblTexture);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTextureManager);
            this.Name = "CharacterScreen";
            this.Text = "CharacterScreen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTextureManager;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblTexture;
        private System.Windows.Forms.Label lblParts;
        private System.Windows.Forms.Label lblAnims;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lstCostumes;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtCostumeInput;
    }
}