namespace GaiaPulse.AnimationManager
{
    partial class CycleManager
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
            this.lstFrames = new System.Windows.Forms.ListBox();
            this.lstCycle = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnProperties = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPlaceholder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstFrames
            // 
            this.lstFrames.FormattingEnabled = true;
            this.lstFrames.Location = new System.Drawing.Point(12, 21);
            this.lstFrames.Name = "lstFrames";
            this.lstFrames.Size = new System.Drawing.Size(200, 238);
            this.lstFrames.TabIndex = 0;
            // 
            // lstCycle
            // 
            this.lstCycle.FormattingEnabled = true;
            this.lstCycle.Location = new System.Drawing.Point(218, 21);
            this.lstCycle.Name = "lstCycle";
            this.lstCycle.Size = new System.Drawing.Size(200, 238);
            this.lstCycle.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 265);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add Frame";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Delete Frame";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // btnProperties
            // 
            this.btnProperties.Location = new System.Drawing.Point(178, 265);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(75, 23);
            this.btnProperties.TabIndex = 4;
            this.btnProperties.Text = "Properties";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.BtnPropertiesClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Frames";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cycle";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(259, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
            // 
            // btnPlaceholder
            // 
            this.btnPlaceholder.Location = new System.Drawing.Point(343, 265);
            this.btnPlaceholder.Name = "btnPlaceholder";
            this.btnPlaceholder.Size = new System.Drawing.Size(75, 23);
            this.btnPlaceholder.TabIndex = 8;
            this.btnPlaceholder.Text = "Placeholder";
            this.btnPlaceholder.UseVisualStyleBackColor = true;
            // 
            // CycleManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 297);
            this.Controls.Add(this.btnPlaceholder);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnProperties);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstCycle);
            this.Controls.Add(this.lstFrames);
            this.Name = "CycleManager";
            this.Text = "CycleManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstFrames;
        private System.Windows.Forms.ListBox lstCycle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnProperties;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPlaceholder;
    }
}