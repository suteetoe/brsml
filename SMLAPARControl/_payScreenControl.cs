using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPARControl
{
    public partial class _payScreenControl : MyLib._myScreen
    {
        public _payScreenControl()
        {
            InitializeComponent();
            this._table_name = _g.d.ap_ar_trans._table;
        }
    }
}
