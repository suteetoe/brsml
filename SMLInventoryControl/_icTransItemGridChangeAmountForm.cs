using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransItemGridChangeAmountForm : Form
    {
        public event _submitButtonEventHandler _submit;

        public _icTransItemGridChangeAmountForm()
        {
            InitializeComponent();
            this._cancelButton.Click += new EventHandler(_cancelButton_Click);
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._nameNumberBox._point = _g.g._companyProfile._item_amount_decimal;
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
}
