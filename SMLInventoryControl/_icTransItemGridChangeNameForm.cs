using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransItemGridChangeNameForm : Form
    {
        public event _submitButtonEventHandler _submit;

        public _icTransItemGridChangeNameForm()
        {
            InitializeComponent();
            this._cancelButton.Click += new EventHandler(_cancelButton_Click);
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this.Shown += new EventHandler(_icTransItemGridChangeNameForm_Shown);
        }

        void _icTransItemGridChangeNameForm_Shown(object sender, EventArgs e)
        {
            this._nameTextBox.Focus();
            if (this._nameTextBox.Text.Length > 0)
            {
                SendKeys.Send("{RIGHT}");
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //  || keyData == Keys.Enter
            if (keyData == Keys.F12)
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

        void _saveButton_Click(object sender, EventArgs e)
        {
            if (this._submit != null)
            {
                this.Close();
                this._submit(1);
            }
        }

        void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public delegate void _submitButtonEventHandler(int mode);
}
