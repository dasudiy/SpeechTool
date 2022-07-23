
namespace SpeechTool
{
    partial class Mainform
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tts1 = new SpeechTool.Forms.TTS();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.stt1 = new SpeechTool.Forms.STT();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帮助HToolStripMenuItem,
            this.帮助HToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1269, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置SToolStripMenuItem,
            this.退出XToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(85, 29);
            this.帮助HToolStripMenuItem.Text = "文件(&F)";
            // 
            // 设置SToolStripMenuItem
            // 
            this.设置SToolStripMenuItem.Name = "设置SToolStripMenuItem";
            this.设置SToolStripMenuItem.Size = new System.Drawing.Size(173, 34);
            this.设置SToolStripMenuItem.Text = "设置(&S)";
            this.设置SToolStripMenuItem.Click += new System.EventHandler(this.设置SToolStripMenuItem_Click);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(173, 34);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            // 
            // 帮助HToolStripMenuItem1
            // 
            this.帮助HToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助HToolStripMenuItem1.Name = "帮助HToolStripMenuItem1";
            this.帮助HToolStripMenuItem1.Size = new System.Drawing.Size(89, 29);
            this.帮助HToolStripMenuItem1.Text = "帮助(&H)";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.关于ToolStripMenuItem.Text = "关于(&A)";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 33);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1269, 617);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tts1);
            this.tabPage1.Location = new System.Drawing.Point(34, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1231, 609);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "文字转语音";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tts1
            // 
            this.tts1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tts1.Location = new System.Drawing.Point(3, 3);
            this.tts1.Name = "tts1";
            this.tts1.Size = new System.Drawing.Size(1225, 603);
            this.tts1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.stt1);
            this.tabPage2.Location = new System.Drawing.Point(34, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1231, 609);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "语音转文字";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // stt1
            // 
            this.stt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stt1.Location = new System.Drawing.Point(3, 3);
            this.stt1.Name = "stt1";
            this.stt1.Size = new System.Drawing.Size(1225, 603);
            this.stt1.TabIndex = 0;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 650);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Mainform";
            this.Text = "Speech Tool";
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Forms.TTS tts1;
        private Forms.STT stt1;
        private System.Windows.Forms.ToolStripMenuItem 设置SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
    }
}
