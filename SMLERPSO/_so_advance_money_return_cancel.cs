using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSO
{
    public partial class _so_advance_money_return_cancel : UserControl
    {
        public _so_advance_money_return_cancel()
        {
            InitializeComponent();
            this._icTrans.Disposed += new EventHandler(_icTrans_Disposed);
        }

        void _icTrans_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
