using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSO
{
    public partial class _soQuotationApproval : UserControl
    {
        public _soQuotationApproval()
        {
            InitializeComponent();
            this._ictrans._myManageTrans._closeScreen += new MyLib.CloseScreenEvent(_myManageTrans__closeScreen);
            this._ictrans.Disposed += new EventHandler(_ictrans_Disposed);
        }

        void _ictrans_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _myManageTrans__closeScreen()
        {
            this.Dispose();
        }
    }
}
