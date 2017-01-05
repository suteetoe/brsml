using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace SMLERPSO
{
    //Reserved : ใบสั่งจองสินค้า
    public partial class _soReserved : UserControl
    {
        public _soReserved(string menuName)
        {
            InitializeComponent();
            this._ictrans._menuName = menuName;
            this._ictrans._myManageTrans.Disposed += new EventHandler(_myManageTrans_Disposed);
        }

        void _myManageTrans_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
