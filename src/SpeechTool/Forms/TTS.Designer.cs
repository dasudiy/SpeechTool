
namespace SpeechTool.Forms
{
    partial class TTS
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkStero = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtSSML = new System.Windows.Forms.TextBox();
            this.btnGO = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.chkMP3 = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.numPitch = new System.Windows.Forms.NumericUpDown();
            this.numSpeakingSpeed = new System.Windows.Forms.NumericUpDown();
            this.cbRolePlay = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSpeakingStyle = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbVoice = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chkSRT = new System.Windows.Forms.CheckBox();
            this.chkPlay = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeakingSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // chkStero
            // 
            this.chkStero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkStero.AutoSize = true;
            this.chkStero.Location = new System.Drawing.Point(153, 519);
            this.chkStero.Name = "chkStero";
            this.chkStero.Size = new System.Drawing.Size(133, 29);
            this.chkStero.TabIndex = 36;
            this.chkStero.Text = "转为立体声";
            this.chkStero.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(16, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(916, 394);
            this.tabControl1.TabIndex = 35;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtInput);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(908, 356);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "文字";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(3, 3);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(902, 350);
            this.txtInput.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtSSML);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(908, 356);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SSML";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtSSML
            // 
            this.txtSSML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSSML.Location = new System.Drawing.Point(3, 3);
            this.txtSSML.Multiline = true;
            this.txtSSML.Name = "txtSSML";
            this.txtSSML.Size = new System.Drawing.Size(902, 350);
            this.txtSSML.TabIndex = 0;
            // 
            // btnGO
            // 
            this.btnGO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGO.Location = new System.Drawing.Point(1075, 300);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(112, 98);
            this.btnGO.TabIndex = 33;
            this.btnGO.Text = "转换(&G)";
            this.btnGO.UseVisualStyleBackColor = true;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(1056, 479);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(112, 34);
            this.btnOpen.TabIndex = 32;
            this.btnOpen.Text = "打开(&O)";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // chkMP3
            // 
            this.chkMP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMP3.AutoSize = true;
            this.chkMP3.Location = new System.Drawing.Point(16, 519);
            this.chkMP3.Name = "chkMP3";
            this.chkMP3.Size = new System.Drawing.Size(131, 29);
            this.chkMP3.TabIndex = 30;
            this.chkMP3.Text = "编码为MP3";
            this.chkMP3.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(938, 479);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(112, 34);
            this.btnBrowse.TabIndex = 29;
            this.btnBrowse.Text = "浏览...(&B)";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Location = new System.Drawing.Point(16, 482);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(916, 31);
            this.txtOutputFolder.TabIndex = 28;
            // 
            // numPitch
            // 
            this.numPitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numPitch.DecimalPlaces = 2;
            this.numPitch.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPitch.Location = new System.Drawing.Point(938, 364);
            this.numPitch.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numPitch.Name = "numPitch";
            this.numPitch.Size = new System.Drawing.Size(92, 31);
            this.numPitch.TabIndex = 26;
            this.numPitch.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numSpeakingSpeed
            // 
            this.numSpeakingSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numSpeakingSpeed.DecimalPlaces = 2;
            this.numSpeakingSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSpeakingSpeed.Location = new System.Drawing.Point(938, 300);
            this.numSpeakingSpeed.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numSpeakingSpeed.Name = "numSpeakingSpeed";
            this.numSpeakingSpeed.Size = new System.Drawing.Size(92, 31);
            this.numSpeakingSpeed.TabIndex = 27;
            this.numSpeakingSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbRolePlay
            // 
            this.cbRolePlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRolePlay.FormattingEnabled = true;
            this.cbRolePlay.Location = new System.Drawing.Point(938, 233);
            this.cbRolePlay.Name = "cbRolePlay";
            this.cbRolePlay.Size = new System.Drawing.Size(249, 33);
            this.cbRolePlay.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(938, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 25);
            this.label7.TabIndex = 21;
            this.label7.Text = "语调:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(938, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 25);
            this.label6.TabIndex = 20;
            this.label6.Text = "语速:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(938, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 25);
            this.label5.TabIndex = 19;
            this.label5.Text = "角色扮演:";
            // 
            // cbSpeakingStyle
            // 
            this.cbSpeakingStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSpeakingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSpeakingStyle.FormattingEnabled = true;
            this.cbSpeakingStyle.Location = new System.Drawing.Point(938, 169);
            this.cbSpeakingStyle.Name = "cbSpeakingStyle";
            this.cbSpeakingStyle.Size = new System.Drawing.Size(249, 33);
            this.cbSpeakingStyle.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(938, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 25);
            this.label4.TabIndex = 18;
            this.label4.Text = "说话风格:";
            // 
            // cbVoice
            // 
            this.cbVoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVoice.FormattingEnabled = true;
            this.cbVoice.Location = new System.Drawing.Point(938, 105);
            this.cbVoice.Name = "cbVoice";
            this.cbVoice.Size = new System.Drawing.Size(249, 33);
            this.cbVoice.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(938, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = "语音:";
            // 
            // cbLanguage
            // 
            this.cbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(938, 41);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(249, 33);
            this.cbLanguage.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(938, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "语言:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 454);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 25);
            this.label8.TabIndex = 15;
            this.label8.Text = "输出路径:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "输入文字:";
            // 
            // chkSRT
            // 
            this.chkSRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSRT.AutoSize = true;
            this.chkSRT.Location = new System.Drawing.Point(292, 519);
            this.chkSRT.Name = "chkSRT";
            this.chkSRT.Size = new System.Drawing.Size(181, 29);
            this.chkSRT.TabIndex = 37;
            this.chkSRT.Text = "生成SRT字幕文件";
            this.chkSRT.UseVisualStyleBackColor = true;
            // 
            // chkPlay
            // 
            this.chkPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPlay.AutoSize = true;
            this.chkPlay.Location = new System.Drawing.Point(479, 519);
            this.chkPlay.Name = "chkPlay";
            this.chkPlay.Size = new System.Drawing.Size(133, 29);
            this.chkPlay.TabIndex = 38;
            this.chkPlay.Text = "完成后播放";
            this.chkPlay.UseVisualStyleBackColor = true;
            // 
            // TTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkPlay);
            this.Controls.Add(this.chkSRT);
            this.Controls.Add(this.chkStero);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.chkMP3);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.numPitch);
            this.Controls.Add(this.numSpeakingSpeed);
            this.Controls.Add(this.cbRolePlay);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbSpeakingStyle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbVoice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "TTS";
            this.Size = new System.Drawing.Size(1205, 577);
            this.Load += new System.EventHandler(this.TTS_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeakingSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkStero;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtSSML;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.CheckBox chkMP3;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.NumericUpDown numPitch;
        private System.Windows.Forms.NumericUpDown numSpeakingSpeed;
        private System.Windows.Forms.ComboBox cbRolePlay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbSpeakingStyle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbVoice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox chkSRT;
        private System.Windows.Forms.CheckBox chkPlay;
    }
}
