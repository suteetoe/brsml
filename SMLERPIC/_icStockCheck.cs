// SMLERP IC
// Create by : Anek
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icStockCheck : UserControl
    {
        public _icStockCheck(string menuName)
        {
            InitializeComponent();
            this._ictrans._menuName = menuName;
            this._ictrans.Disposed += new EventHandler(_ictrans_Disposed);
        }

        void _ictrans_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
