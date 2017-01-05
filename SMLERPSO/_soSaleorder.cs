using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSO
{
    public partial class _soSaleorder : UserControl
    {
        public _soSaleorder(string menuName)
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
