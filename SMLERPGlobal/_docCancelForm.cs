using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _g
{
    public partial class _docCancelForm : Form
    {
        public _docCancelForm(string docNo)
        {
            InitializeComponent();
            this._textDocNo.Text = docNo;
            this._comboCancelConfirm.SelectedIndex = 0;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.No;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
