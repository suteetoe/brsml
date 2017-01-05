using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icPointUpdateControl : UserControl
    {
        public _icPointUpdateControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._dataList._tableName = _g.d.ic_inventory._table;
            this._dataList._loadViewFormat("screen_ic_inventory_update_point_status", MyLib._myGlobal._userSearchScreenGroup, false);
            this._dataList._refreshData();
            this._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
            //
            this._pointComboBox.SelectedIndex = 0;
            this._pointComboBox.SelectedIndexChanged += new EventHandler(_pointComboBox_SelectedIndexChanged);
        }

        void _pointComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._refresh();
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                string __havePoint = this._dataList._gridData._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._have_point).ToString();
                if (__havePoint.Equals("1"))
                {
                    __result.newColor = Color.Magenta;
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + __ex.StackTrace.ToString());
            }
            return (__result);
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Update", MyLib._myGlobal._resource("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __row = 0; __row < this._dataList._gridData._rowData.Count; __row++)
                {
                    string __itemCode = this._dataList._gridData._cellGet(__row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                    string __havePoint = this._dataList._gridData._cellGet(__row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._have_point).ToString();
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory_detail._table + " set " + _g.d.ic_inventory_detail._have_point + "=" + __havePoint + " where " + _g.d.ic_inventory_detail._ic_code + "=\'" + __itemCode + "\'"));
                }
                __myQuery.Append("</node>");
                string __resultStr = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__resultStr.Length == 0)
                {
                    MessageBox.Show("Success");
                    this._refresh();
                }
                else
                {
                    MessageBox.Show(__resultStr);
                }
            }
        }

        private void _checkAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._dataList._gridData._rowData.Count; __row++)
            {
                this._dataList._gridData._cellUpdate(__row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._have_point, 1, false);
            }
            this._dataList._gridData.Invalidate();
        }

        private void _removeAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._dataList._gridData._rowData.Count; __row++)
            {
                this._dataList._gridData._cellUpdate(__row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._have_point, 0, false);
            }
            this._dataList._gridData.Invalidate();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _refresh()
        {
            this._dataList._extraWhere = "";
            if (this._pointComboBox.SelectedIndex == 1 || this._pointComboBox.SelectedIndex == 2)
            {
                int __columnNumber = this._dataList._gridData._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._have_point);
                string __fieldFormat = ((MyLib._myGrid._columnType)this._dataList._gridData._columnList[__columnNumber])._query;
                this._dataList._extraWhere = __fieldFormat + "="+((this._pointComboBox.SelectedIndex == 1) ? "1" : "0");
            }
            this._dataList._refreshData();
        }
    }
}
