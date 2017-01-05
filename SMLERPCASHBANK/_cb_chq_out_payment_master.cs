using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANK
{
    public partial class _cb_chq_out_payment_master : UserControl
    {
        public _cb_chq_out_payment_master()
        {
            InitializeComponent();
            this._chqListControl1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._chqListControl1._myManagechqList._closeScreen += new MyLib.CloseScreenEvent(_myManagechqList__closeScreen);
        }

        void _myManagechqList__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
