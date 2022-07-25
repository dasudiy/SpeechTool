using SpeechTool.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechTool.Forms
{
    public partial class TTS : UserControl
    {
        public TTS()
        {
            InitializeComponent();
        }

        private void TTS_Load(object sender, EventArgs e)
        {

        }

        public async Task Init()
        {
            var allVoicies = await Program.App.GetAllVoice();

            var locales = allVoicies.GroupBy(v => v.Locale).Select(t => new { t.Key, Name = CultureInfo.GetCultureInfo(t.Key).DisplayName }).ToArray();
            cbLanguage.DataSource = locales;
            cbLanguage.ValueMember = "Key";
            cbLanguage.DisplayMember = "Name";
            cbLanguage.DataBindings.Add(new Binding("SelectedValue", Program.Config, nameof(Program.Config.Lang)));
            cbLanguage.SelectedValueChanged += (sender, e) =>
            {
                var voices = allVoicies.Where(t => t.Locale == cbLanguage.SelectedValue?.ToString()).ToArray();
                cbVoice.DataSource = voices;
            };

            cbVoice.DataSource = allVoicies.Where(t => Program.Config.Lang == null || t.Locale == Program.Config.Lang).ToArray();
            cbVoice.ValueMember = "ShortName";
            cbVoice.DisplayMember = "LocalName";
            cbVoice.DataBindings.Add(new Binding("SelectedValue", Program.Config, nameof(Program.Config.Voice)));
            cbVoice.SelectedValueChanged += (sender, e) =>
            {
                var voice = allVoicies.FirstOrDefault(t => t.ShortName == cbVoice.SelectedValue?.ToString());
                if (voice != null)
                {
                    cbSpeakingStyle.DataSource = voice.StyleList.Select(t => new KeyValuePair<string, string>(t, t)).ToArray();
                }
            };

            cbSpeakingStyle.DataSource = allVoicies.FirstOrDefault(t => t.ShortName == Program.Config.Voice)?.StyleList?.Select(t => new KeyValuePair<string, string>(t, t))?.ToArray() ?? new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(string.Empty, string.Empty) };
            cbSpeakingStyle.ValueMember = "Key";
            cbSpeakingStyle.DisplayMember = "Value";
            cbSpeakingStyle.DataBindings.Add(new Binding("SelectedValue", Program.Config, nameof(Program.Config.Style)));

            numSpeakingSpeed.DataBindings.Add(new Binding("Value", Program.Config, nameof(Program.Config.Speed)));
            numPitch.DataBindings.Add(new Binding("Value", Program.Config, nameof(Program.Config.Pitch)));

            chkMP3.DataBindings.Add(new Binding("Checked", Program.Config, nameof(Program.Config.MP3)));
            chkStero.DataBindings.Add(new Binding("Checked", Program.Config, nameof(Program.Config.Stero)));
            chkSRT.DataBindings.Add(new Binding("Checked", Program.Config, nameof(Program.Config.SRT)));
            chkPlay.DataBindings.Add(new Binding("Checked", Program.Config, nameof(Program.Config.Playback)));

            txtOutputFolder.DataBindings.Add(new Binding("Text", Program.Config, nameof(Program.Config.OutputFolder)));
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = txtOutputFolder.Text
            });
        }

        private async void btnGO_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                txtSSML.Text = Program.App.GetSSMLWithAutoAddBookmark(
                txtInput.Text,
                cbVoice.SelectedValue?.ToString(),
                cbSpeakingStyle.SelectedValue?.ToString(),
                cbRolePlay.SelectedValue?.ToString(),
                MapValue(numSpeakingSpeed.Value.To<float>(), numSpeakingSpeed.Minimum.To<float>(), numSpeakingSpeed.Maximum.To<float>(), -100, 200).To<int>(),
                MapValue(numPitch.Value.To<float>(), numPitch.Minimum.To<float>(), numPitch.Maximum.To<float>(), -50, 50).To<int>(),
                 "。", "\r\n", "\n", "，"
                );
            }

            btnGO.Enabled = false;
            await Program.App.TextToSpeech(
                txtSSML.Text,
                null,
                true,
                cbLanguage.SelectedValue.ToString(),
                cbVoice.SelectedValue.ToString(),
                GetFilename(txtInput.Text) + (chkMP3.Checked ? ".mp3" : ".wav"),
                chkStero.Checked,
                chkMP3.Checked,
                chkSRT.Checked,
                chkPlay.Checked);
            btnGO.Enabled = true;
        }

        private float MapValue(float x, float v1, float v2, float v3, float v4)
        {
            return ((x - v1) / (v2 - v1)) * (v4 - v3) + v3;
        }

        private string GetFilename(string text)
        {
            //remove invalid chars
            foreach (var item in Path.GetInvalidFileNameChars().Union(Path.GetInvalidFileNameChars()))
            {
                text = text.Replace(item, '_');
            }
            return Path.Combine(txtOutputFolder.Text, text.Substring(0, Math.Min(text.Length, 250 - txtOutputFolder.Text.Length)));
        }
    }
}
