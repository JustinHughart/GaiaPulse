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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.assignTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frameDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.framesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Editor = new GaiaPulse.AnimationManager.EditorControl();
            this.cycleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.editToolStripMenuItem,
            this.framesToolStripMenuItem,
            this.cycleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(535, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.saveToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.SaveToolStripMenuItem1Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.assignTextureToolStripMenuItem,
            this.frameDataToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // assignTextureToolStripMenuItem
            // 
            this.assignTextureToolStripMenuItem.Name = "assignTextureToolStripMenuItem";
            this.assignTextureToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.assignTextureToolStripMenuItem.Text = "Animation Data";
            this.assignTextureToolStripMenuItem.Click += new System.EventHandler(this.AssignTextureToolStripMenuItemClick);
            // 
            // frameDataToolStripMenuItem
            // 
            this.frameDataToolStripMenuItem.Name = "frameDataToolStripMenuItem";
            this.frameDataToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.frameDataToolStripMenuItem.Text = "Frame Data";
            this.frameDataToolStripMenuItem.Click += new System.EventHandler(this.FrameDataToolStripMenuItemClick);
            // 
            // framesToolStripMenuItem
            // 
            this.framesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFrameToolStripMenuItem,
            this.deleteFrameToolStripMenuItem});
            this.framesToolStripMenuItem.Name = "framesToolStripMenuItem";
            this.framesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.framesToolStripMenuItem.Text = "Frames";
            // 
            // addFrameToolStripMenuItem
            // 
            this.addFrameToolStripMenuItem.Name = "addFrameToolStripMenuItem";
            this.addFrameToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.addFrameToolStripMenuItem.Text = "Add Frame";
            this.addFrameToolStripMenuItem.Click += new System.EventHandler(this.AddFrameToolStripMenuItemClick);
            // 
            // deleteFrameToolStripMenuItem
            // 
            this.deleteFrameToolStripMenuItem.Name = "deleteFrameToolStripMenuItem";
            this.deleteFrameToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.deleteFrameToolStripMenuItem.Text = "Delete Frame";
            this.deleteFrameToolStripMenuItem.Click += new System.EventHandler(this.DeleteFrameToolStripMenuItemClick);
            // 
            // Editor
            // 
            this.Editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Editor.Location = new System.Drawing.Point(0, 28);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(535, 326);
            this.Editor.TabIndex = 1;
            this.Editor.Text = "Editor";
            // 
            // cycleToolStripMenuItem
            // 
            this.cycleToolStripMenuItem.Name = "cycleToolStripMenuItem";
            this.cycleToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.cycleToolStripMenuItem.Text = "Cycle";
            this.cycleToolStripMenuItem.Click += new System.EventHandler(this.CycleToolStripMenuItemClick);
            // 
            // AnimationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 353);
            this.Controls.Add(this.Editor);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AnimationEditor";
            this.Text = "PartEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem assignTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frameDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem framesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFrameToolStripMenuItem;
        private EditorControl Editor;
        private System.Windows.Forms.ToolStripMenuItem cycleToolStripMenuItem;
    }
}