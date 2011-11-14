namespace GaiaPulse.PartManager
{
    partial class PartEditor
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
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCamera = new System.Windows.Forms.Button();
            this.btnBoundary = new System.Windows.Forms.Button();
            this.btnAnchor = new System.Windows.Forms.Button();
            this.Editor = new GaiaPulse.PartManager.EditorControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.editToolStripMenuItem,
            this.optionsToolStripMenuItem});
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
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.assignTextureToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // assignTextureToolStripMenuItem
            // 
            this.assignTextureToolStripMenuItem.Name = "assignTextureToolStripMenuItem";
            this.assignTextureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.assignTextureToolStripMenuItem.Text = "Assign Texture";
            this.assignTextureToolStripMenuItem.Click += new System.EventHandler(this.assignTextureToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // btnCamera
            // 
            this.btnCamera.BackColor = System.Drawing.SystemColors.Control;
            this.btnCamera.Location = new System.Drawing.Point(12, 38);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(90, 23);
            this.btnCamera.TabIndex = 2;
            this.btnCamera.Text = "Camera Control";
            this.btnCamera.UseVisualStyleBackColor = false;
            this.btnCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // btnBoundary
            // 
            this.btnBoundary.Location = new System.Drawing.Point(109, 38);
            this.btnBoundary.Name = "btnBoundary";
            this.btnBoundary.Size = new System.Drawing.Size(86, 23);
            this.btnBoundary.TabIndex = 3;
            this.btnBoundary.Text = "SetBoundary";
            this.btnBoundary.UseVisualStyleBackColor = true;
            this.btnBoundary.Click += new System.EventHandler(this.btnBoundary_Click);
            // 
            // btnAnchor
            // 
            this.btnAnchor.Location = new System.Drawing.Point(201, 38);
            this.btnAnchor.Name = "btnAnchor";
            this.btnAnchor.Size = new System.Drawing.Size(94, 23);
            this.btnAnchor.TabIndex = 4;
            this.btnAnchor.Text = "Set Anchor Tool";
            this.btnAnchor.UseVisualStyleBackColor = true;
            this.btnAnchor.Click += new System.EventHandler(this.btnAnchor_Click);
            // 
            // Editor
            // 
            this.Editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Editor.Location = new System.Drawing.Point(0, 28);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(535, 327);
            this.Editor.TabIndex = 5;
            this.Editor.Text = "editor";
            // 
            // PartEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 353);
            this.Controls.Add(this.btnBoundary);
            this.Controls.Add(this.btnAnchor);
            this.Controls.Add(this.btnCamera);
            this.Controls.Add(this.Editor);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PartEditor";
            this.Text = "PartEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PartEditor_Load);
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
        private EditorControl editorControl1;
        private System.Windows.Forms.Button btnCamera;
        private System.Windows.Forms.Button btnBoundary;
        private System.Windows.Forms.Button btnAnchor;
        private EditorControl Editor;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;

    }
}