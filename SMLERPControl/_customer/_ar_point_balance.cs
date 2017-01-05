using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl._customer
{
    public partial class _ar_point_balance : UserControl
    {
        public _ar_point_balance()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData1._dataList._lockRecord = false; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_ar_customer", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.ar_customer._code, 1);
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            this._screenTop.Enabled = false;
            this._grid_ar_point_movement1.IsEdit = false;
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                string __typeField = "doc_type";

                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _myManageData1._dataList._tableName + whereString));
                //__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ar_dealer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_dealer._ar_code) + "=\'" + ((ArrayList)rowData)[1].ToString().ToUpper() + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from (select " +
                MyLib._myGlobal._fieldAndComma(
                _g.d.ic_trans._doc_date + " as " + _g.d.pos_resource._doc_date,
                _g.d.ic_trans._doc_time + " as " + _g.d.pos_resource._doc_time,
                _g.d.ic_trans._doc_no + " as " + _g.d.pos_resource._doc_no,
                "1 as " + __typeField,
                _g.d.ic_trans._sale_code + " as " + _g.d.pos_resource._sale_code,
                _g.d.ic_trans._pos_id + " as " + _g.d.pos_resource._pos_id,
                _g.d.ic_trans._total_amount + " as " + _g.d.pos_resource._doc_amount,
                _g.d.ic_trans._sum_point + " as " + _g.d.pos_resource._point_add,
                "0 as " + _g.d.pos_resource._point_use) +
                " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._cust_code + "=\'" + ((ArrayList)rowData)[1].ToString().ToUpper() + "\' and ( " + _g.d.ic_trans._is_pos + "=1 or " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString() + " ) and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._sum_point + "<>0 union all " +

                " select " +
                MyLib._myGlobal._fieldAndComma(
                _g.d.ic_trans._doc_date + " as " + _g.d.pos_resource._doc_date,
                _g.d.ic_trans._doc_time + " as " + _g.d.pos_resource._doc_time,
                _g.d.ic_trans._doc_no + " as " + _g.d.pos_resource._doc_no,
                "1 as " + __typeField,
                _g.d.ic_trans._sale_code + " as " + _g.d.pos_resource._sale_code,
                _g.d.ic_trans._pos_id + " as " + _g.d.pos_resource._pos_id,
                _g.d.ic_trans._total_amount + " as " + _g.d.pos_resource._doc_amount,
                "0 as " + _g.d.pos_resource._point_add,
                _g.d.ic_trans._sum_point + " as " + _g.d.pos_resource._point_use) +
                " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._cust_code + "=\'" + ((ArrayList)rowData)[1].ToString().ToUpper() + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดแต้ม).ToString() + " and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._sum_point + "<>0 union all " +

                "select " + MyLib._myGlobal._fieldAndComma(
                _g.d.cb_trans._doc_date + " as " + _g.d.pos_resource._doc_date,
                _g.d.cb_trans._doc_time + " as " + _g.d.pos_resource._doc_time,
                _g.d.cb_trans._doc_no + " as " + _g.d.pos_resource._doc_no,
                "2 as " + __typeField,
                "\'\'" + " as " + _g.d.pos_resource._sale_code,
                "\'\'" + " as " + _g.d.pos_resource._pos_id,
                "0 as " + _g.d.pos_resource._doc_amount,
                "0 as " + _g.d.pos_resource._point_add,
                _g.d.cb_trans._point_qty + " as " + _g.d.pos_resource._point_use) +
                " from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._ap_ar_code + "=\'" + ((ArrayList)rowData)[1].ToString().ToUpper() + "\' and " + _g.d.cb_trans._point_qty + "<>0 and coalesce((select " + _g.d.ic_trans._last_status + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=" + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "),0)=0) as t1 order by " + MyLib._myGlobal._fieldAndComma(_g.d.pos_resource._doc_date, _g.d.pos_resource._doc_time, __typeField, _g.d.pos_resource._doc_no)));

                __myQuery.Append("</node>");

                ArrayList getDataList = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myQuery.ToString());

                DataSet getData = (DataSet)getDataList[0];
                DataSet dealerData = (DataSet)getDataList[1];

                this._screenTop._loadData(getData.Tables[0]);
                this._grid_ar_point_movement1._loadFromDataTable(dealerData.Tables[0]);
                decimal __balance = 0M;
                for (int __row = 0; __row < this._grid_ar_point_movement1._rowData.Count; __row++)
                {
                    decimal __inValue = (decimal)this._grid_ar_point_movement1._cellGet(__row, _g.d.pos_resource._point_add);
                    decimal __outValue = (decimal)this._grid_ar_point_movement1._cellGet(__row, _g.d.pos_resource._point_use);
                    __balance += (__inValue - __outValue);
                    this._grid_ar_point_movement1._cellUpdate(__row, _g.d.pos_resource._point_balance, __balance, false);
                }
                this._grid_ar_point_movement1.Invalidate();


                this._screenTop._search(true);
                this._screenTop._isChange = false;
                if (forEdit)
                {
                    this._screenTop._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }
    }

}
