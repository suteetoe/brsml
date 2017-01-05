using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _tableOpenControl : Form
    {
        public _tableOpenControl()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this._customerCountTextbox.KeyUp += _customerCountTextbox_KeyUp;
        }

        void _customerCountTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this._customerCountTextbox.Text.Length > 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
