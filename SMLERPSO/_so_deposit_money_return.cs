using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSO
{
    public partial class _so_deposit_money_return : UserControl
    {
        public _so_deposit_money_return()
        {
            InitializeComponent();
            this._po_so_deposit_control1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._po_so_deposit_control1._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
