
namespace SpeechTool.Forms
{
    partial class STT
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.chkSRT = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.Filter = "视频文件(*.mp4;*.avi)|*.mp4;*.avi|音频文件(*.mp3;*.wav)|*.mp3;*.wav|所有文件 (*.*)|*.*";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(893, 48);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(150, 34);
            this.btnOpen.TabIndex = 35;
            this.btnOpen.Text = "打开文件夹(&O)";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(775, 48);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(112, 34);
            this.btnBrowse.TabIndex = 34;
            this.btnBrowse.Text = "浏览...(&B)";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(315, 50);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(454, 31);
            this.txtInputFile.TabIndex = 33;
            // 
            // cbLanguage
            // 
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(17, 50);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(249, 33);
            this.cbLanguage.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 25);
            this.label2.TabIndex = 36;
            this.label2.Text = "语言:";
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(17, 89);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(1176, 695);
            this.txtOutput.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 25);
            this.label1.TabIndex = 39;
            this.label1.Text = "输入文件:";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(1072, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(121, 70);
            this.btnGo.TabIndex = 40;
            this.btnGo.Text = "开始";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // chkSRT
            // 
            this.chkSRT.AutoSize = true;
            this.chkSRT.Location = new System.Drawing.Point(862, 13);
            this.chkSRT.Name = "chkSRT";
            this.chkSRT.Size = new System.Drawing.Size(181, 29);
            this.chkSRT.TabIndex = 41;
            this.chkSRT.Text = "生成SRT字幕文件";
            this.chkSRT.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(1072, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(121, 70);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // STT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkSRT);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtInputFile);
            this.DoubleBuffered = true;
            this.Name = "STT";
            this.Size = new System.Drawing.Size(1205, 798);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.CheckBox chkSRT;
        private System.Windows.Forms.Button btnCancel;
    }
}
