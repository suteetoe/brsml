using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANK
{
    public partial class _cb_credit_master : UserControl
    {
        public _cb_credit_master()
        {
            InitializeComponent();
            this._creditMasterControl1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._creditMasterControl1._myManagechqList._closeScreen += new MyLib.CloseScreenEvent(_myManageCreditMaster__closeScreen);
        }

        void _myManageCreditMaster__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
