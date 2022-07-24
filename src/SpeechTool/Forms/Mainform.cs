using SpeechTool.Classes;
using SpeechTool.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechTool
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
            this.tabControl1.Enabled = false;
        }

        private async void Mainform_Load(object sender, EventArgs e)
        {
            if (!await CheckKeyValid())
            {
                设置SToolStripMenuItem_Click(sender, e);
            }
            控制台CToolStripMenuItem.Checked = Program.Config.ShowConsole;
            this.tabControl1.Enabled = true;
            await this.tts1.Init();
            await this.stt1.Init();
        }

        private async void 设置SToolStripMenuItem_Click(object sender, EventArgs e)
        {
        RETRY:
            var settingDialog = new SettingDialog();
            if (settingDialog.ShowDialog() == DialogResult.OK)
            {
                if (!await CheckKeyValid())
                {
                    goto RETRY;
                }
            }
        }

        private async Task<bool> CheckKeyValid()
        {
            try
            {
                var ret = await Program.App.GetAllVoice();
                return true;
            }
            catch
            {
                //TODO: 区分异常
                return false;
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var about = new About())
            {
                about.ShowDialog();
            }
        }

        private void 控制台CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            控制台CToolStripMenuItem.Checked = !控制台CToolStripMenuItem.Checked;
            Program.Config.ShowConsole = 控制台CToolStripMenuItem.Checked;
            if (控制台CToolStripMenuItem.Checked)
            {
                WindowsApi.ShowConsoleWindow();
            }
            else
            {
                WindowsApi.HideConsoleWindow();
            }

        }
    }
}
