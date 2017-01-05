using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransItemGridChangeDiscountForm : Form
    {
        public event _submitButtonEventHandler _submit;

        public _icTransItemGridChangeDiscountForm()
        {
            InitializeComponent();
            this.Shown += new EventHandler(_icTransItemGridChangeDiscountForm_Shown);
        }

        void _icTransItemGridChangeDiscountForm_Shown(object sender, EventArgs e)
        {
            this._nameTextBox.textBox.Focus();
            if (this._nameTextBox.textBox.Text.Length > 0)
            {
                SendKeys.Send("{RIGHT}");
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12 || keyData == Keys.Enter)
            {
                if (this._submit != null)
                {
                    this.Close();
                    this._submit(1);
                }
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.Close();
                this._submit(0);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (this._submit != null)
            {
                this.Close();
                this._submit(1);
            }
        }
    }
}
