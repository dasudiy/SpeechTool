using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechTool.Forms
{
    public partial class STT : UserControl
    {
        private CancellationTokenSource cts;

        public STT()
        {
            InitializeComponent();
        }

        public async Task Init()
        {
            var allVoicies = await Program.App.GetAllVoice();

            var locales = allVoicies.GroupBy(v => v.Locale).Select(t => new { t.Key, Name = CultureInfo.GetCultureInfo(t.Key).DisplayName }).ToArray();
            cbLanguage.DataSource = locales;
            cbLanguage.ValueMember = "Key";
            cbLanguage.DisplayMember = "Name";
            cbLanguage.DataBindings.Add(new Binding("SelectedValue", Program.Config, nameof(Program.Config.Lang)));

            chkSRT.DataBindings.Add(new Binding("Checked", Program.Config, nameof(Program.Config.SRT)));
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtInputFile.Text = openFileDialog1.FileName;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtInputFile.Text))
            {
                var path = Path.GetDirectoryName(txtInputFile.Text);
                Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = path
                });
            }
        }

        private async void btnGo_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtInputFile.Text))
            {
                btnGo.Visible = false;
                btnCancel.Visible = true;                
                this.cts = new CancellationTokenSource();
                txtOutput.Text = await Program.App.SpeechToText(txtInputFile.Text, cbLanguage.SelectedValue.ToString(), null, chkSRT.Checked, cts.Token);
                btnCancel.Visible = false;
                btnGo.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.cts?.Cancel();
            this.cts?.Dispose();
        }
    }
}
