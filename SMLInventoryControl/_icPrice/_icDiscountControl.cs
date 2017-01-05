using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._icPrice
{
    public partial class _icDiscountControl : UserControl
    {
        public _icDiscountControl()
        {
            InitializeComponent();
        }

        public void _clear()
        {
            this._icmainScreenTopControl1._clear();
            this._gridNormalPrice0._grid._clear();
            this._gridNormalPrice1._grid._clear();
            this._gridNormalPrice2._grid._clear();
        }
    }
}
