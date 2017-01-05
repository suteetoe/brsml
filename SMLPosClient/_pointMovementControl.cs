using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _pointMovementControl : UserControl
    {
        private string _custCode = "";

        public _pointMovementControl(string custCode)
        {
            InitializeComponent();

            string __formatNumberAmount = _g.g._getFormatNumberStr(3);
            this._custCode = custCode;

            this._grid._isEdit = false;
            this._grid._table_name = _g.d.pos_resource._table;
            this._grid._addColumn(_g.d.pos_resource._doc_date, 4, 20, 20);
            this._grid._addColumn(_g.d.pos_resource._doc_time, 1, 10, 10);
            this._grid._addColumn(_g.d.pos_resource._doc_no, 1, 20, 20);
            this._grid._addColumn(_g.d.pos_resource._sale_code, 1, 10, 10);
            this._grid._addColumn(_g.d.pos_resource._pos_id, 1, 20, 20);
            this._grid._addColumn(_g.d.pos_resource._doc_amount, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._grid._addColumn(_g.d.pos_resource._point_add, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._grid._addColumn(_g.d.pos_resource._point_use, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._grid._addColumn(_g.d.pos_resource._point_balance, 3, 10, 8, false, false, false, false, __formatNumberAmount);

            if (_g.g._companyProfile._activeSync && _g.g._companyProfile._use_point_center)
            {
                //this._grid._addColumn(_g.d.pos_resource._branch_code, 1, 10, 8);
            }

            this._grid._calcPersentWidthToScatter();
            // load
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            string __branchField = "";
            if (_g.g._companyProfile._activeSync && _g.g._companyProfile._use_point_center)
            {
                //__myFrameWork = new MyLib._myFrameWork(_g.g._companyProfile._activeSyncServer, "SMLConfig" + _g.g._companyProfile._activeSyncProvider + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                //__branchField = ",branch_sync as " + _g.d.pos_resource._point_balance ;
            }

            StringBuilder __myquery = new StringBuilder();
            string __typeField = "doc_type";
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            string __dateQuery = MyLib._myGlobal._convertDateToQuery(DateTime.Now);
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from (select " +
                MyLib._myGlobal._fieldAndComma(
                _g.d.ic_trans._doc_date + " as " + _g.d.pos_resource._doc_date,
                _g.d.ic_trans._doc_time + " as " + _g.d.pos_resource._doc_time,
                _g.d.ic_trans._doc_no + " as " + _g.d.pos_resource._doc_no,
                "1 as " + __typeField,
                _g.d.ic_trans._sale_code + " as " + _g.d.pos_resource._sale_code,
                _g.d.ic_trans._pos_id + " as " + _g.d.pos_resource._pos_id,
                _g.d.ic_trans._total_amount+ " as " + _g.d.pos_resource._doc_amount,
                _g.d.ic_trans._sum_point + " as " + _g.d.pos_resource._point_add,
                "0 as " + _g.d.pos_resource._point_use) +
                " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._cust_code + "=\'" + custCode + "\' and ( " + _g.d.ic_trans._is_pos + "=1 or " + _g.d.ic_trans._trans_flag + "="+  _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString() +" ) and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._sum_point + "<>0 union all " +
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
                __branchField +
                " from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._ap_ar_code + "=\'" + custCode + "\' and " + _g.d.cb_trans._point_qty + "<>0 and coalesce((select " + _g.d.ic_trans._last_status + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=" + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "),0)=0) as t1 order by " + MyLib._myGlobal._fieldAndComma(_g.d.pos_resource._doc_date, _g.d.pos_resource._doc_time, __typeField, _g.d.pos_resource._doc_no)));
            __myquery.Append("</node>");
            ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            if (__data.Count > 0)
            {
                DataTable __movement = ((DataSet)__data[0]).Tables[0];
                this._grid._loadFromDataTable(__movement);
                decimal __balance = 0M;
                for (int __row = 0; __row < this._grid._rowData.Count; __row++)
                {
                    decimal __inValue = (decimal)this._grid._cellGet(__row, _g.d.pos_resource._point_add);
                    decimal __outValue = (decimal)this._grid._cellGet(__row, _g.d.pos_resource._point_use);
                    __balance += (__inValue - __outValue);
                    this._grid._cellUpdate(__row, _g.d.pos_resource._point_balance, __balance, false);
                }
                this._grid.Invalidate();
            }
        }
    }
}
