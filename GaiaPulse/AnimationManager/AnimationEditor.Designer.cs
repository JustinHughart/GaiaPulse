namespace GaiaPulse.AnimationManager
{
    partial class AnimationEditor
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
            this.animationEditorControl1 = new GaiaPulse.AnimationManager.AnimationEditorControl();
            this.SuspendLayout();
            // 
            // animationEditorControl1
            // 
            this.animationEditorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.animationEditorControl1.Location = new System.Drawing.Point(1, 0);
            this.animationEditorControl1.Name = "animationEditorControl1";
            this.animationEditorControl1.Size = new System.Drawing.Size(584, 536);
            this.animationEditorControl1.TabIndex = 0;
            this.animationEditorControl1.Text = "animationEditorControl1";
            // 
            // AnimationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 529);
            this.Controls.Add(this.animationEditorControl1);
            this.Name = "AnimationEditor";
            this.Text = "AnimationEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private AnimationEditorControl animationEditorControl1;
    }
}