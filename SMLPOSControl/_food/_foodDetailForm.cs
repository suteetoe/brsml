using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _foodDetailForm : MyLib._myForm
    {
        public _foodDetailForm(string productName, decimal qty, decimal price, string suggest_remark)
        {
            InitializeComponent();

            _itemNameLabel.Text = productName;
            _qtyTextBox.Text = qty.ToString();
            _priceTextBox.Text = price.ToString();
            _remarkTextBox.Text = suggest_remark;

        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
