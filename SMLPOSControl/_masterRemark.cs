using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _masterRemark : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _masterRemark()
        {
            InitializeComponent();
            this._dataGrid._table_name = _g.d.ic_inventory_remark._table;
            this._dataGrid._addColumn(_g.d.ic_inventory_remark._remark_code, 1, 25, 25);
            this._dataGrid._addColumn(_g.d.ic_inventory_remark._remark_desc, 1, 255, 75);
            DataTable __dt = this._myFrameWork._queryShort("select * from " + _g.d.ic_inventory_remark._table + " order by " + _g.d.ic_inventory_remark._remark_code).Tables[0];
            this._dataGrid._loadFromDataTable(__dt);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_remark._table));
            for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
            {
                string __code = this._dataGrid._cellGet(__row, _g.d.ic_inventory_remark._remark_code).ToString().Trim().ToUpper();
                if (__code.Length > 0)
                {
                    StringBuilder __insertQuery = new StringBuilder(String.Concat("insert into ", _g.d.ic_inventory_remark._table, " (", _g.d.ic_inventory_remark._remark_code, ",", _g.d.ic_inventory_remark._remark_desc, ") values (\'", __code, "\',\'", this._dataGrid._cellGet(__row, _g.d.ic_inventory_remark._remark_desc).ToString(), "\')"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertQuery.ToString()));
                }
            }
            __myQuery.Append("</node>");
            string __resultQuery = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__resultQuery.Length == 0)
            {
                MessageBox.Show("Success");
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Fail : " + __resultQuery);
            }
        }
    }
}
