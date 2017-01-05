using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._utils
{
    public partial class _selectLocationUserControl : UserControl
    {
        public _selectLocationUserControl()
        {
            InitializeComponent();
            this._grid._table_name = _g.d.ic_shelf._table;
            this._grid._addColumn("Check", 11, 10, 10);
            this._grid._addColumn(_g.d.ic_shelf._code, 1, 10, 20);
            this._grid._addColumn(_g.d.ic_shelf._name_1, 1, 10, 50);
            this._grid._addColumn(_g.d.ic_shelf._whcode, 1, 10, 20);
            //
        }

        public void _load(string wareHouseList)
        {
            while (wareHouseList.IndexOf(" ") != -1)
            {
                wareHouseList=wareHouseList.Replace(" ", "");
            }
            string[] __warehouse = wareHouseList.Split(',');
            StringBuilder __wareHouseQuery = new StringBuilder();
            for (int __loop = 0; __loop < __warehouse.Length; __loop++)
            {
                if (__wareHouseQuery.Length > 0)
                {
                    __wareHouseQuery.Append(",");
                }
                __wareHouseQuery.Append("\'" + __warehouse[__loop].ToString() + "\'");
            }
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __where = (wareHouseList.Length == 0) ? "" : " where " + _g.d.ic_shelf._whcode + " in (" + __wareHouseQuery + ")";
            DataTable __select = __myFrameWork._queryShort("select " + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code + "," + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + __where + " order by " + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code).Tables[0];
            this._grid._loadFromDataTable(__select);
        }
    }
}
