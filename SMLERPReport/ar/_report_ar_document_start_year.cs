using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ar
{
    public partial class _report_ar_document_start_year : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;
        private _condition_ar _form_condition = new _condition_ar();
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _fromDocumentNo;
        private string _toDocumentNo;
        private string _fromAr;
        private string _toAr;
        private string _fromEmp;
        private string _toEmp;

        public _report_ar_document_start_year()
        {
            InitializeComponent();

            //this._view1._buttonCondition.Enabled = false;
            this._view1._buttonExample.Enabled = false;
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);

            this._form_condition._screenType = _screenConditionArType.ArDocumentStartYear;
            this._showCondition();
        }

        void _view1__loadDataByThread()
        {
            // this._dataTable == null ไม่ต้อง load ซ้ำ
            if (this._dataTable == null)
            {
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                StringBuilder __where = new StringBuilder();
                StringBuilder __orderBy = new StringBuilder();
                StringBuilder __query = new StringBuilder();
                try
                {
                    //where
                    __where.Append(String.Format("({0}=2) and ({1}=23) and ({2} between \'{3}\' and \'{4}\')",
                        _g.d.ap_ar_trans._trans_type,
                        _g.d.ap_ar_trans._trans_flag,
                        _g.d.ap_ar_trans._doc_date,
                        MyLib._myGlobal._convertDateToQuery(this._fromDate),
                        MyLib._myGlobal._convertDateToQuery(this._toDate))); 
                    if (!this._fromDocumentNo.Equals("") && !this._toDocumentNo.Equals(""))
                    {
                        __where.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')",
                            _g.d.ap_ar_trans._doc_no,
                            this._fromDocumentNo,
                            this._toDocumentNo));
                    }
                    if (!this._fromAr.Equals("") && !this._toAr.Equals(""))
                    {
                        __where.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')", 
                            _g.d.ap_ar_trans._cust_code,
                            this._fromAr, 
                            this._toAr));
                    }
                    if (!this._fromEmp.Equals("") && !this._toEmp.Equals(""))
                    {
                        __where.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')", 
                            _g.d.ap_ar_trans._sale_code, 
                            this._fromEmp, 
                            this._toEmp));
                    }
                    //order by
                    __orderBy.Append(String.Format("{0}.{1}", _g.d.ap_ar_trans._table, _g.d.ap_ar_trans._doc_date));
                    //query
                    __query.Append(String.Format("select {0},{1},{2},{3},{4},{5},{6},{7} from {8} where {9} order by {10}",
                        _g.d.ap_ar_trans._doc_date,
                        _g.d.ap_ar_trans._doc_no,
                        _g.d.ap_ar_trans._cust_code,
                        "(select name_1 from ar_customer where code=ap_ar_trans.cust_code) as cust_name",
                        _g.d.ap_ar_trans._remark,
                        /*_g.d.ap_ar_trans._inquiry_type*/"\'\' as document_type",
                        _g.d.ap_ar_trans._total_net_value,
                        _g.d.ap_ar_trans._total_debt_balance,
                        _g.d.ap_ar_trans._table,
                        __where.ToString(),
                        __orderBy.ToString()));

                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                }
                catch(Exception __Ex)
                {
                    this._view1._loadDataByThreadSuccess = false;
                    return;
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานลูกหนี้ยกมาต้นปี", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontStandard);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                //พิมพ์ชื่อฟิลด์
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หมายเหตุ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ประเภทเอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดสุทธิ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดคงเหลือ", "", SMLReport._report._cellAlign.Right);
                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            Font __newFont = null;
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__detailObject._columnList[0];
            SMLReport._report._objectListType __dataObject = null;

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
                this._view1._addDataColumn(__detailObject, __dataObject, 5, __dataRows[__row].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 6, __dataRows[__row].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 7, __dataRows[__row].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
            }
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._showCondition();
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (this._form_condition._screen_condition_ar1._checkEmtryField().Length != 0)
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
            this._dataTable = null; // จะได้ load data ใหม่
            this._form_condition._process = false;
            this._form_condition.ShowDialog();
            if (this._form_condition._process)
            {
                string __date = this._form_condition._screen_condition_ar1._getDataStr("จากวันที่");
                string[] __arrayDate = __date.Split('/');
                this._fromDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());

                __date = this._form_condition._screen_condition_ar1._getDataStr("ถึงวันที่");
                __arrayDate = __date.Split('/');
                this._toDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());

                this._fromDocumentNo = this._form_condition._screen_condition_ar1._getDataStr("จากเลขที่เอกสาร");

                this._toDocumentNo = this._form_condition._screen_condition_ar1._getDataStr("ถึงเลขที่เอกสาร");

                this._fromAr = this._form_condition._screen_condition_ar1._getDataStr("จากลูกค้า");

                this._toAr = this._form_condition._screen_condition_ar1._getDataStr("ถึงลูกค้า");

                this._fromEmp = this._form_condition._screen_condition_ar1._getDataStr("จากพนักงานขาย");

                this._toEmp = this._form_condition._screen_condition_ar1._getDataStr("ถึงพนักงานขาย");

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}
