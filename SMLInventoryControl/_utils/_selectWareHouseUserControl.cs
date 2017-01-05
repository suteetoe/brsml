using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._utils
{
    public partial class _selectWareHouseUserControl : UserControl
    {
        public _selectWareHouseUserControl()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                InitializeComponent();
                this._grid._table_name = _g.d.ic_warehouse._table;
                this._grid._addColumn("Check", 11, 10, 10);
                this._grid._addColumn(_g.d.ic_warehouse._code, 1, 10, 30);
                this._grid._addColumn(_g.d.ic_warehouse._name_1, 1, 10, 60);
                //
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __select = __myFrameWork._queryShort("select " + _g.d.ic_warehouse._code + "," + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " order by " + _g.d.ic_warehouse._code).Tables[0];
                this._grid._loadFromDataTable(__select);
            }
        }
    }
}
