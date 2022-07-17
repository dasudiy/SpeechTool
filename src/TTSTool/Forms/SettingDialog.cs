using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TTSTool
{
    public partial class SettingDialog : Form
    {
        internal Config Config { get; set; }

        public SettingDialog(Config config)
        {
            InitializeComponent();
            this.Config = config;
            txtKey.Text = config.Key;
            txtRegion.Text = config.Region;
        }        

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Config.Key = txtKey.Text;
                Config.Region = txtRegion.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            //TODO: validate input;
            return true;
        }
    }
}
