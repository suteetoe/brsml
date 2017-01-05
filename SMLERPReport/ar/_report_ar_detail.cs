using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ar
{
    public partial class _report_ar_detail : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;
        private _condition_ar_new _form_condition;
        private string _balance_date;
        private DataTable _conditionFromTo;
        private string[] _ar_detail_column = { _g.d.ar_customer._table+"."+_g.d.ar_customer._code,
                                             _g.d.ar_customer._table+"."+_g.d.ar_customer._name_1,
                                             _g.d.ar_customer._table+"."+_g.d.ar_customer._address,
                                             _g.d.ar_customer._table+"."+_g.d.ar_customer._telephone,
                                             _g.d.ar_customer._table+"."+_g.d.ar_customer._fax};

        public _report_ar_detail()
        {
            InitializeComponent();

            //this._view1._buttonCondition.Enabled = false;
            this._view1._pageSetupDialog.PageSettings.Landscape = true;
            this._view1._buttonExample.Enabled = false;
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);

            this._showCondition();
        }

        void _view1__loadDataByThread()
        {
            if (this._dataTable == null)
            {
                try
                {
                    //where user control
                    string __getUserWhere1 = this._form_condition._whereUserControl1._getWhere1("");
                    __getUserWhere1 = __getUserWhere1.Replace("\"", "");
                    string __getUserWhere2 = this._form_condition._whereUserControl1._getWhere2();
                    __getUserWhere2 = __getUserWhere2.Replace("\"", "");
                    __getUserWhere2 = __getUserWhere2.Trim().Length > 0 ? __getUserWhere2 + " and" : "where";
                    //where
                    //=======================================================================================================
                    StringBuilder __where = new StringBuilder("(balance_amount<>0) and ");
                    //StringBuilder __where = new StringBuilder();
                    //=======================================================================================================
                    if (this._conditionFromTo != null && this._conditionFromTo.Rows.Count > 0)
                    {
                        for (int __row = 0; __row < this._conditionFromTo.Rows.Count; __row++)
                        {
                            __where.Append(string.Format("({0} between \'{1}\' and \'{2}\')",
                                _g.d.ar_customer._code, this._conditionFromTo.Rows[__row][0].ToString(),
                                this._conditionFromTo.Rows[__row][1].ToString()));
                            if (__row != this._conditionFromTo.Rows.Count - 1)
                            {
                                __where.Append(" or ");
                            }
                        }
                    }
                    //=======================================================================================================
                    else
                    {
                        __where.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.ar_customer._code, "0", "z"));
                    }
                    //=======================================================================================================
                    //order by
                    string __orderBy = this._form_condition._whereUserControl1._getOrderBy();
                    __orderBy = __orderBy.Replace("\"", "");
                    //sub query
                    string __subQuery = string.Format("select {1},{2},{3},{4},{5},"
                        + "(("
                            + "(select coalesce(sum({11}),0) from {6} where {7}=2 and {8}=44 and {9}<=\'{18}\' and {10}={0}.{1})"
                            + "-(select coalesce(sum({11}),0) from {6} where {7}=2 and {8}=45 and {9}<=\'{18}\' and {10}={0}.{1})"
                        + ")-("
                            + "(select coalesce(sum({17}),0) from {12} where {13}=2 and {14}=27 and {15}<=\'{18}\' and {16}={0}.{1})"
                            + "-(select coalesce(sum({17}),0) from {12} where {13}=2 and {14}=28 and {15}<=\'{18}\' and {16}={0}.{1})"
                        + ")) as balance_amount"
                        + " from {0} {19}",
                        _g.d.ar_customer._table,            //{0}
                        _g.d.ar_customer._code,             //{1}
                        _g.d.ar_customer._name_1,           //{2}
                        _g.d.ar_customer._address,          //{3}
                        _g.d.ar_customer._telephone,        //{4}
                        _g.d.ar_customer._fax,              //{5}
                        _g.d.ic_trans._table,               //{6}
                        _g.d.ic_trans._trans_type,          //{7}
                        _g.d.ic_trans._trans_flag,          //{8}
                        _g.d.ic_trans._doc_date,            //{9}
                        _g.d.ic_trans._cust_code,           //{10}
                        _g.d.ic_trans._total_amount,        //{11}
                        _g.d.ap_ar_trans._table,            //{12}
                        _g.d.ap_ar_trans._trans_type,       //{13}
                        _g.d.ap_ar_trans._trans_flag,       //{14}
                        _g.d.ap_ar_trans._doc_date,         //{15}
                        _g.d.ap_ar_trans._cust_code,        //{16}
                        _g.d.ap_ar_trans._total_net_value, //{17}
                        this._balance_date,                 //{18}
                        __getUserWhere1);                   //{19}
                    //query
                    string __query = string.Format("select {1},{2},{3},{4},{5},{6} from ({7}) as {0} {8} {9} {10}",
                        _g.d.ar_customer._table,        //{0}
                        _g.d.ar_customer._code,         //{1}
                        _g.d.ar_customer._name_1,       //{2}
                        _g.d.ar_customer._address,      //{3}
                        _g.d.ar_customer._telephone,    //{4}
                        _g.d.ar_customer._fax,          //{5}
                        "balance_amount",               //{6}
                        __subQuery,                     //{7}
                        __getUserWhere2,                //{8}
                        __where.ToString(),             //{9}
                        __orderBy);                     //{10}
                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            this._view1._loadDataByThreadSuccess = true;
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            //Write Header Report
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "Title\t: รายงานรายละเอียดลูกหนี้", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Printed By\t: " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Printed Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Description\t: ", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                //พิมพ์ชื่อฟิลด์
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ที่อยู่", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 13, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "โทรศัพท์", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 13, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "โทรสาร/FAX", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 14, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดลูกหนี้คงค้าง", "", SMLReport._report._cellAlign.Right);
                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            Font __newFont = null;
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__detailObject._columnList[0];
            SMLReport._report._objectListType __dataObject = null;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");

            if (this._dataTable == null) return;
            DataRow[] __dataRows = this._dataTable.Select();
            for (int __row = 0; __row < __dataRows.Length; __row++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject, __dataObject);
                this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRows[__row].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 2, __dataRows[__row].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 3, __dataRows[__row].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 4, __dataRows[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                double __value = Double.Parse(__dataRows[__row].ItemArray[5].ToString());
                this._view1._addDataColumn(__detailObject, __dataObject, 5, string.Format(__formatNumber, __value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
            }
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._showCondition();
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (this._form_condition._screen_condition_ar_new1._checkEmtryField().Length != 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้กำหนดเงื่อนไข"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            this._dataTable = null; // จะได้ load data ใหม่
            this._view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            this._view1._buildReport(SMLReport._report._reportType.Standard);
            this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new _condition_ar_new(_enum_screen_report_ar.ar_detail.ToString());
                this._form_condition._whereUserControl1._tableName = _g.d.ar_customer._table;
                this._form_condition._whereUserControl1._addFieldComboBox(this._ar_detail_column);
            }
            this._form_condition._process = false;
            this._form_condition.ShowDialog();
            if (this._form_condition._process)
            {
                this._dataTable = null; // จะได้ load data ใหม่

                this._balance_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._form_condition._screen_condition_ar_new1._getDataStr(_g.d.resource_report._balance_date)));

                this._conditionFromTo = this._form_condition._grid_condition_ar_new1._getCondition();

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}