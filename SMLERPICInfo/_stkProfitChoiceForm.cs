using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public partial class _stkProfitChoiceForm : Form
    {
        public _stkProfitChoiceEnum _result = _stkProfitChoiceEnum.Calcel;

        public _stkProfitChoiceForm(string name1, string name2)
        {
            InitializeComponent();
            //
            this._select1Button.ButtonText = name1;
            this._select2Button.ButtonText = name2;
        }

        private void _selectDocumentButton_Click(object sender, EventArgs e)
        {
            this._result = _stkProfitChoiceEnum.B1;
            this.Close();
        }

        private void _selectArButton_Click(object sender, EventArgs e)
        {
            this._result = _stkProfitChoiceEnum.B2;
            this.Close();
        }

        private void _selectCancelButton_Click(object sender, EventArgs e)
        {
            this._result = _stkProfitChoiceEnum.Calcel;
            this.Close();
        }
    }

    public enum _stkProfitChoiceEnum
    {
        B1,
        B2,
        Calcel
    }
}
