using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icLotManage : UserControl
    {
        private string __formatNumberQty = _g.g._getFormatNumberStr(1);
        //public static object[] _lotSelectItem = new object[] { _g.d.ic_resource._select_auto, _g.d.ic_resource._select_manual };
        public static object[] _lotSelectValue = new object[] { "อัตโนมัติ", "เลือกเองเท่านั้น" };
        string _oldCode = "";

        public _icLotManage()
        {
            InitializeComponent();

            this._lotGrid._table_name = _g.d.ic_resource._table;
            this._lotGrid._addRowEnabled = false;
            this._lotGrid._addColumn(_g.d.ic_trans_detail_lot._wh_code, 1, 255, 10, false, false, false, false, "", "", "", _g.d.ic_resource._warehouse);
            this._lotGrid._addColumn(_g.d.ic_trans_detail_lot._shelf_code, 1, 255, 10, false, false, false, false, "", "", "", _g.d.ic_resource._location);
            this._lotGrid._addColumn(_g.d.ic_resource._lot_number, 1, 255, 20, false, false);
            this._lotGrid._addColumn(_g.d.ic_resource._sort_order, 1, 10, 10);
            this._lotGrid._addColumn(_g.d.ic_resource._lot_select, 10, 255, 15);
            this._lotGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            this._lotGrid._addColumn(_g.d.ic_resource._doc_date, 4, 255, 20, false, false, false, false, "dd/MM/yyyy");
            this._lotGrid._addColumn(_g.d.ic_resource._date_expire, 4, 255, 20, false, false, false, false, "dd/MM/yyyy");
            //this._lotGrid._addColumn(_g.d.ic_resource._mfd_date, 4, 255, 20, false, false);
            //this._lotGrid._addColumn(_g.d.ic_resource._mfn_name, 1, 255, 20, false, false);
            this._lotGrid._cellComboBoxGet += _lotGrid__cellComboBoxGet;
            this._lotGrid._cellComboBoxItem += _lotGrid__cellComboBoxItem;
            //this._lotGrid._addColumn(_g.d.ic_resource._lot_number, 1, 255, 20);
            this._lotGrid._calcPersentWidthToScatter();

            this.SuspendLayout();
            this._myManageData._dataList._lockRecord = true;
            this._myManageData._autoSize = true;
            this._myManageData._displayMode = 0;
            this._myManageData._selectDisplayMode(this._myManageData._displayMode);
            //this._myManageData._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);


            this._myManageData._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageData._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);
            this._myManageData._dataList._extraWhere = _g.d.ic_inventory._cost_type + "=2 ";
            this._myManageData._manageButton = this._myToolStrip1;
            this._myManageData._manageBackgroundPanel = this.panel1;
            this._myManageData._autoSizeHeight = 450;
            this._myManageData._dataListOpen = true;
            this._myManageData._dataList._buttonNew.Visible = false;
            this._myManageData._dataList._buttonNewFromTemp.Visible = false;
            this._myManageData._dataList._buttonDelete.Visible = false;
            this._myManageData._dataList._buttonSelectAll.Visible = false;
            this._myManageData._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData__loadDataToScreen);


            this._myManageData._closeScreen += _myManageData__closeScreen;
            this.ResumeLayout(false);
        }

        object[] _lotGrid__cellComboBoxItem(object sender, int row, int column)
        {
            if (column == this._lotGrid._findColumnByName(_g.d.ic_resource._lot_select))
            {
                return _lotSelectValue;
            }
            return null;
        }

        string _lotGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            if (column == this._lotGrid._findColumnByName(_g.d.ic_resource._lot_select))
            {
                return _lotSelectValue[select].ToString();
            }
            return "";
        }

        void _myManageData__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            System.Collections.ArrayList __rowDataArray = (System.Collections.ArrayList)rowData;
            int __getColumnCode = this._myManageData._dataList._gridData._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
            this._oldCode = __rowDataArray[__getColumnCode].ToString();

            string __wh_shelf_where = "";

            SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            // เปลี่ยนไป get trans_detail
            /*string __query = __process._stkStockInfoAndBalanceByLotQuery(_g.g._productCostType.ปรกติ, null, this._oldCode, this._oldCode, "", true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT_ที่เก็บ_เรียงตามเอกสาร_IMEX, __wh_shelf_where);
            
            //string __query = __process._stkStockInfoAndBalanceByLotQuery(costMode, itemGrid, itemBegin, itemEnd, dateEnd, getBalanceOnly, balanceType, extraWhere);

            //StringBuilder __queryStr = new StringBuilder();
            //string __getLotManageQuery = ", coalesce((select {0} from " + _g.d.ic_lot_manage._table + " where " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._ic_code + " = q." + _g.d.ic_resource._ic_code + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._lot_number + " = q." + _g.d.ic_resource._lot_number + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._wh_code + " = q." + _g.d.ic_trans_detail_lot._wh_code + " and " + _g.d.ic_lot_manage._table + "." + _g.d.ic_lot_manage._shelf_code + " = q." + _g.d.ic_trans_detail_lot._shelf_code + "), 0) as {1} ";
            //__queryStr.Append("select * ");
            //__queryStr.Append(string.Format(__getLotManageQuery, _g.d.ic_lot_manage._sort_order, _g.d.ic_resource._sort_order));
            //__queryStr.Append(string.Format(__getLotManageQuery, _g.d.ic_lot_manage._lot_select, _g.d.ic_resource._lot_select));


            //__queryStr.Append(" from ( " + __query + ") as q ");
            //__queryStr.Append(" where  balance_qty<>0 ");

            DataTable __lotBalance = __myFrameWork._queryShort(__query.ToString()).Tables[0];*/

            string __query = __process._stkLotInfoAndBalanceQuery(null, this._oldCode, this._oldCode, "", "", true).Replace(") as final order by sort_order,doc_date,doc_time,lot_number", ") as final order by wh_code, lot_number, doc_date, balance_qty");
            DataTable __lotBalance = __myFrameWork._queryShort(__query.ToString()).Tables[0];


            // get lot balance query  and load to grid
            //StringBuilder __query = new StringBuilder();
            this._lotGrid._loadFromDataTable(__lotBalance);

            return true;
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_lot_manage._table + " where " + _g.d.ic_lot_manage._ic_code + " = \'" + this._oldCode + "\'"));

            // 
            for (int __row = 0; __row < this._lotGrid._rowData.Count; __row++)
            {
                string __fieldList = MyLib._myGlobal._fieldAndComma(_g.d.ic_lot_manage._ic_code, _g.d.ic_lot_manage._lot_number, _g.d.ic_lot_manage._lot_select, _g.d.ic_lot_manage._sort_order, _g.d.ic_lot_manage._wh_code, _g.d.ic_lot_manage._shelf_code);
                string __dataList = MyLib._myGlobal._fieldAndComma("\'" + this._oldCode + "\'", "\'" + this._lotGrid._cellGet(__row, _g.d.ic_lot_manage._lot_number).ToString() + "\'", this._lotGrid._cellGet(__row, _g.d.ic_lot_manage._lot_select).ToString(), "\'" + this._lotGrid._cellGet(__row, _g.d.ic_lot_manage._sort_order).ToString() + "\'", "\'" + this._lotGrid._cellGet(__row, _g.d.ic_lot_manage._wh_code).ToString() + "\'", "\'" + this._lotGrid._cellGet(__row, _g.d.ic_lot_manage._shelf_code).ToString() + "\'");

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_lot_manage._table + " (" + __fieldList + ") values (" + __dataList + ") "));

            }

            __query.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();


            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Length > 0)
            {
                MessageBox.Show(__result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("บันทึกสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this._myManageData._newData(true);
                this._lotGrid._clear();
                this._oldCode = "";
            }
        }

    }
}
