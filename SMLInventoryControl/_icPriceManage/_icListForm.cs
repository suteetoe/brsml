using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._icPriceManage
{
    public partial class _icListForm : MyLib._myForm
    {
        public _icListForm()
        {
            InitializeComponent();

            this._icGrid._table_name = _g.d.ic_inventory._table;
            this._icGrid._width_by_persent = true;
            this._icGrid._addColumn(_g.d.ic_inventory._code, 1, 10, 20);
            this._icGrid._addColumn(_g.d.ic_inventory._name_1, 1, 10, 80);
            this._icGrid._calcPersentWidthToScatter();
        }
    }
}
