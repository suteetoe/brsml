using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLGetPrice
{
    public partial class _validForm : Form
    {
        private string _code = "";
        public bool _pass = false;

        public _validForm()
        {
            InitializeComponent();
            //
            this._code = "1234";
            this._displayTextBox.Text = this._code;
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            if (this._code.Equals(this._inputTextBox.Text))
            {
                this._pass = true;
                this.Close();
            } else
            {
                MessageBox.Show("รหัสไม่ตรงกัน");
            }
        }
    }
}
