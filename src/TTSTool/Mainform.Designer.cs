
namespace TTSTool
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
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbVoice = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSpeakingStyle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRolePlay = new System.Windows.Forms.ComboBox();
            this.numSpeakingSpeed = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numPitch = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chkMP3 = new System.Windows.Forms.CheckBox();
            this.chkKeepWav = new System.Windows.Forms.CheckBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnGO = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtSSML = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeakingSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPitch)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(3, 3);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(902, 350);
            this.txtInput.TabIndex = 0;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "输入文字:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(934, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "语言:";
            // 
            // cbLanguage
            // 
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(934, 37);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(249, 33);
            this.cbLanguage.TabIndex = 3;
            this.cbLanguage.SelectedIndexChanged += new System.EventHandler(this.cbLanguage_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(934, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "语音:";
            // 
            // cbVoice
            // 
            this.cbVoice.FormattingEnabled = true;
            this.cbVoice.Location = new System.Drawing.Point(934, 101);
            this.cbVoice.Name = "cbVoice";
            this.cbVoice.Size = new System.Drawing.Size(249, 33);
            this.cbVoice.TabIndex = 3;
            this.cbVoice.SelectedIndexChanged += new System.EventHandler(this.cbVoice_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(934, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "说话风格:";
            // 
            // cbSpeakingStyle
            // 
            this.cbSpeakingStyle.FormattingEnabled = true;
            this.cbSpeakingStyle.Location = new System.Drawing.Point(934, 165);
            this.cbSpeakingStyle.Name = "cbSpeakingStyle";
            this.cbSpeakingStyle.Size = new System.Drawing.Size(249, 33);
            this.cbSpeakingStyle.TabIndex = 3;
            this.cbSpeakingStyle.SelectedIndexChanged += new System.EventHandler(this.RebuildSSML);
            this.cbSpeakingStyle.TextUpdate += new System.EventHandler(this.RebuildSSML);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(934, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "角色扮演:";
            // 
            // cbRolePlay
            // 
            this.cbRolePlay.FormattingEnabled = true;
            this.cbRolePlay.Location = new System.Drawing.Point(934, 229);
            this.cbRolePlay.Name = "cbRolePlay";
            this.cbRolePlay.Size = new System.Drawing.Size(249, 33);
            this.cbRolePlay.TabIndex = 3;
            this.cbRolePlay.SelectedIndexChanged += new System.EventHandler(this.RebuildSSML);
            this.cbRolePlay.TextUpdate += new System.EventHandler(this.RebuildSSML);
            // 
            // numSpeakingSpeed
            // 
            this.numSpeakingSpeed.DecimalPlaces = 2;
            this.numSpeakingSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSpeakingSpeed.Location = new System.Drawing.Point(934, 296);
            this.numSpeakingSpeed.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numSpeakingSpeed.Name = "numSpeakingSpeed";
            this.numSpeakingSpeed.Size = new System.Drawing.Size(92, 31);
            this.numSpeakingSpeed.TabIndex = 4;
            this.numSpeakingSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSpeakingSpeed.ValueChanged += new System.EventHandler(this.RebuildSSML);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(934, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 25);
            this.label6.TabIndex = 2;
            this.label6.Text = "语速:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(934, 330);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 25);
            this.label7.TabIndex = 2;
            this.label7.Text = "语调:";
            // 
            // numPitch
            // 
            this.numPitch.DecimalPlaces = 2;
            this.numPitch.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPitch.Location = new System.Drawing.Point(934, 360);
            this.numPitch.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numPitch.Name = "numPitch";
            this.numPitch.Size = new System.Drawing.Size(92, 31);
            this.numPitch.TabIndex = 4;
            this.numPitch.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPitch.ValueChanged += new System.EventHandler(this.RebuildSSML);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 450);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 25);
            this.label8.TabIndex = 1;
            this.label8.Text = "输出路径:";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(12, 478);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(916, 31);
            this.txtOutputFolder.TabIndex = 5;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(934, 475);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(112, 34);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "浏览...(&B)";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkMP3
            // 
            this.chkMP3.AutoSize = true;
            this.chkMP3.Location = new System.Drawing.Point(12, 515);
            this.chkMP3.Name = "chkMP3";
            this.chkMP3.Size = new System.Drawing.Size(131, 29);
            this.chkMP3.TabIndex = 7;
            this.chkMP3.Text = "编码为MP3";
            this.chkMP3.UseVisualStyleBackColor = true;
            this.chkMP3.CheckedChanged += new System.EventHandler(this.chkMP3_CheckedChanged);
            // 
            // chkKeepWav
            // 
            this.chkKeepWav.AutoSize = true;
            this.chkKeepWav.Enabled = false;
            this.chkKeepWav.Location = new System.Drawing.Point(149, 515);
            this.chkKeepWav.Name = "chkKeepWav";
            this.chkKeepWav.Size = new System.Drawing.Size(152, 29);
            this.chkKeepWav.TabIndex = 8;
            this.chkKeepWav.Text = "保留原始WAV";
            this.chkKeepWav.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(1052, 475);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(112, 34);
            this.btnOpen.TabIndex = 9;
            this.btnOpen.Text = "打开(&O)";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnGO
            // 
            this.btnGO.Location = new System.Drawing.Point(1071, 296);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(112, 98);
            this.btnGO.TabIndex = 10;
            this.btnGO.Text = "转换(&G)";
            this.btnGO.UseVisualStyleBackColor = true;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(934, 397);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(112, 34);
            this.btnSetting.TabIndex = 11;
            this.btnSetting.Text = "设置&KEY...";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(916, 394);
            this.tabControl1.TabIndex = 12;
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
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 555);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.chkKeepWav);
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
            this.MaximizeBox = false;
            this.Name = "Mainform";
            this.Text = "TTS Tool";
            ((System.ComponentModel.ISupportInitialize)(this.numSpeakingSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPitch)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbVoice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSpeakingStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbRolePlay;
        private System.Windows.Forms.NumericUpDown numSpeakingSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numPitch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox chkMP3;
        private System.Windows.Forms.CheckBox chkKeepWav;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtSSML;
    }
}
