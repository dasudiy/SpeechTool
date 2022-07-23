using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SpeechTool.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            using (var stream = typeof(About).Assembly.GetManifestResourceStream("SpeechTool.Resources.讨饭码.png"))
            {
                pictureBox1.BackgroundImage = Image.FromStream(stream);
            }
        }
    }
}
