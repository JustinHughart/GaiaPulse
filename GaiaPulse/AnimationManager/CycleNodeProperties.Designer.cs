namespace GaiaPulse.AnimationManager
{
    partial class CycleNodeProperties
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtVelocityX = new System.Windows.Forms.TextBox();
            this.txtVelocityY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSmoothX = new System.Windows.Forms.CheckBox();
            this.chkSmoothY = new System.Windows.Forms.CheckBox();
            this.chkSmoothRotation = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRotation = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtTimeTillNext = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.treeXML = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Velocity X: ";
            // 
            // txtVelocityX
            // 
            this.txtVelocityX.Location = new System.Drawing.Point(76, 6);
            this.txtVelocityX.Name = "txtVelocityX";
            this.txtVelocityX.Size = new System.Drawing.Size(100, 20);
            this.txtVelocityX.TabIndex = 1;
            // 
            // txtVelocityY
            // 
            this.txtVelocityY.Location = new System.Drawing.Point(76, 32);
            this.txtVelocityY.Name = "txtVelocityY";
            this.txtVelocityY.Size = new System.Drawing.Size(100, 20);
            this.txtVelocityY.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Velocity Y: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Smooth X: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Smooth Y: ";
            // 
            // chkSmoothX
            // 
            this.chkSmoothX.AutoSize = true;
            this.chkSmoothX.Location = new System.Drawing.Point(76, 55);
            this.chkSmoothX.Name = "chkSmoothX";
            this.chkSmoothX.Size = new System.Drawing.Size(15, 14);
            this.chkSmoothX.TabIndex = 6;
            this.chkSmoothX.UseVisualStyleBackColor = true;
            // 
            // chkSmoothY
            // 
            this.chkSmoothY.AutoSize = true;
            this.chkSmoothY.Location = new System.Drawing.Point(76, 77);
            this.chkSmoothY.Name = "chkSmoothY";
            this.chkSmoothY.Size = new System.Drawing.Size(15, 14);
            this.chkSmoothY.TabIndex = 7;
            this.chkSmoothY.UseVisualStyleBackColor = true;
            // 
            // chkSmoothRotation
            // 
            this.chkSmoothRotation.AutoSize = true;
            this.chkSmoothRotation.Location = new System.Drawing.Point(278, 35);
            this.chkSmoothRotation.Name = "chkSmoothRotation";
            this.chkSmoothRotation.Size = new System.Drawing.Size(15, 14);
            this.chkSmoothRotation.TabIndex = 11;
            this.chkSmoothRotation.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Smooth Rot: ";
            // 
            // txtRotation
            // 
            this.txtRotation.Location = new System.Drawing.Point(278, 12);
            this.txtRotation.Name = "txtRotation";
            this.txtRotation.Size = new System.Drawing.Size(100, 20);
            this.txtRotation.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Rotation: ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(157, 372);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
            // 
            // txtTimeTillNext
            // 
            this.txtTimeTillNext.Location = new System.Drawing.Point(278, 73);
            this.txtTimeTillNext.Name = "txtTimeTillNext";
            this.txtTimeTillNext.Size = new System.Drawing.Size(100, 20);
            this.txtTimeTillNext.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(198, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Time Till Next: ";
            // 
            // treeXML
            // 
            this.treeXML.Location = new System.Drawing.Point(16, 99);
            this.treeXML.Name = "treeXML";
            this.treeXML.Size = new System.Drawing.Size(362, 267);
            this.treeXML.TabIndex = 21;
            // 
            // CycleNodeProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 402);
            this.Controls.Add(this.treeXML);
            this.Controls.Add(this.txtTimeTillNext);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkSmoothRotation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRotation);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkSmoothY);
            this.Controls.Add(this.chkSmoothX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtVelocityY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVelocityX);
            this.Controls.Add(this.label1);
            this.Name = "CycleNodeProperties";
            this.Text = "NodeProperties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVelocityX;
        private System.Windows.Forms.TextBox txtVelocityY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSmoothX;
        private System.Windows.Forms.CheckBox chkSmoothY;
        private System.Windows.Forms.CheckBox chkSmoothRotation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRotation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtTimeTillNext;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TreeView treeXML;
    }
}