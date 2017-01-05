using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _screenWarehouseLocationGroup : MyLib._myScreen
    {
        public _screenWarehouseLocationGroup()
        {
            InitializeComponent();
            this._maxColumn = 2;
            this._table_name = _g.d.ic_warehouse_location_group._table;
            int __row = 0;
            this._addTextBox(__row++, 0, 1, 0, _g.d.ic_warehouse_location_group._wh_code, 1, 1, 1, true, false, false);
            this._addTextBox(__row++, 0, 1, 0, _g.d.ic_warehouse_location_group._location_group, 2, 0, 0, true, false, false);
            this._addTextBox(__row++, 0, 1, 0, _g.d.ic_warehouse_location_group._name_1, 2, 0, 0, true, false, false);
        }
    }
}
