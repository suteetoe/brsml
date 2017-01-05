using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ar
{
    public partial class _report_ar_trans_debt_payment : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;
        private _condition_ar _form_condition = new _condition_ar();
        private DateTime _fromDocumentDate;
        private DateTime _toDocumentDate;
        private DateTime _fromInvoiceDate;
        private DateTime _toInvoiceDate;
        private string _fromAr;
        private string _toAr;
        private string _fromEmp;
        private string _toEmp;
        private string _fromDocumentNo;
        private string _toDocumentNo;
        private string _showType;
        private bool _showDocumentCutPayment;
        private bool _showByEmp;

        public _report_ar_trans_debt_payment()
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
            this._view1._pageSetupDialog.PageSettings.Landscape = true;

            this._form_condition._screenType = _screenConditionArType.ArTransDebtPayment;
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
                    string __fromDocDate = this._form_condition._screen_condition_ar1._getDataStrQuery("จากวันที่เอกสาร");
                    string __toDocDate = this._form_condition._screen_condition_ar1._getDataStrQuery("ถึงวันที่เอกสาร");
                    string __fromInvoiceDate = this._form_condition._screen_condition_ar1._getDataStrQuery("จากวันที่ Invoice");
                    string __toInvoiceDate = this._form_condition._screen_condition_ar1._getDataStrQuery("ถึงวันที่ Invoice");
                    this._fromDocumentNo = this._form_condition._screen_condition_ar1._getDataStrQuery("จากเลขที่เอกสาร");
                    this._toDocumentNo = this._form_condition._screen_condition_ar1._getDataStrQuery("ถึงเลขที่เอกสาร");

                    __query.Append("select " + MyLib._myGlobal._fieldAndComma(_g.d.ap_ar_trans._doc_date,
                        _g.d.ap_ar_trans._doc_no,
                        "(select name_1 from ar_customer where code=ap_ar_trans.cust_code) as cust_name",
                        _g.d.ap_ar_trans._total_net_value,
                        _g.d.ap_ar_trans._total_discount,
                        _g.d.ap_ar_trans._total_after_discount,
                        _g.d.ap_ar_trans._total_vat_value)
                        + " from " + _g.d.ap_ar_trans._table
                        + " where " + _g.d.ap_ar_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + " and " + _g.d.ap_ar_trans._last_status + "=0" + " and " + _g.d.ap_ar_trans._trans_type + "=2 ");
                    
                    //where
                    __where.Append(" and " + _g.d.ap_ar_trans._doc_date + " between " + __fromDocDate + " and " + __toDocDate);

                    if (__fromInvoiceDate.Length > 0 && __toInvoiceDate.Length > 0)
                    {
                    }

                    
                    if (this._fromAr.Length > 0 && this._toAr.Length > 0)
                    {
                        __where.Append(" and " + _g.d.ap_ar_trans._cust_code + " between \'" + this._fromAr + "\' and \'" + this._toAr + "\' ");                            
                    }


                    if (this._fromEmp.Length  > 0 && this._toEmp.Length > 0)
                    {
                        
                    }

                    if (this._fromDocumentNo.Length > 0 && this._toDocumentNo.Length > 0)
                    {
                        
                    }

                    //order by
                    __orderBy.Append(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date);


                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                }
                catch
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานการรับชำระหนี้ประจำวัน", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
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
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่ใบเสร็จ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบเสร็จ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อลูกค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดรับชำระ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ส่วนลด", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดรับสุทธิ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เงินสด", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เช็ค", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "บัตรเครดิต", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ตัดบัญชี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ธนาคาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่รับ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันหมดอายุ", "", SMLReport._report._cellAlign.Left);
                //this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "สถานะ", "", SMLReport._report._cellAlign.Left);
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
                this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRows[__row].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 2, __dataRows[__row].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 3, __dataRows[__row].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 4, __dataRows[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 5, __dataRows[__row].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 6, __dataRows[__row].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                //this._view1._addDataColumn(__detailObject, __dataObject, 7, __dataRows[__row].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Double);
                //this._view1._addDataColumn(__detailObject, __dataObject, 8, __dataRows[__row].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Double);
                //this._view1._addDataColumn(__detailObject, __dataObject, 9, __dataRows[__row].ItemArray[9].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Double);
                //this._view1._addDataColumn(__detailObject, __dataObject, 10, __dataRows[__row].ItemArray[10].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Double);
                //this._view1._addDataColumn(__detailObject, __dataObject, 11, __dataRows[__row].ItemArray[11].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                //this._view1._addDataColumn(__detailObject, __dataObject, 12, __dataRows[__row].ItemArray[12].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                //this._view1._addDataColumn(__detailObject, __dataObject, 13, __dataRows[__row].ItemArray[13].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                //this._view1._addDataColumn(__detailObject, __dataObject, 14, __dataRows[__row].ItemArray[14].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                //this._view1._addDataColumn(__detailObject, __dataObject, 15, __dataRows[__row].ItemArray[15].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
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
                string __date = this._form_condition._screen_condition_ar1._getDataStr("จากวันที่เอกสาร");
                string[] __arrayDate = __date.Split('/');
                this._fromDocumentDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());

                __date = this._form_condition._screen_condition_ar1._getDataStr("ถึงวันที่เอกสาร");
                __arrayDate = __date.Split('/');
                this._toDocumentDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());

                __date = this._form_condition._screen_condition_ar1._getDataStr("จากวันที่ Invoice");
                if (__date.Length > 0)
                {
                    __arrayDate = __date.Split('/');
                    this._fromInvoiceDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());
                }


                __date = this._form_condition._screen_condition_ar1._getDataStr("ถึงวันที่ Invoice");
                if (__date.Length > 0)
                {
                    __arrayDate = __date.Split('/');
                    this._toInvoiceDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());
                }

                this._fromAr = this._form_condition._screen_condition_ar1._getDataStr("จากลูกค้า");

                this._toAr = this._form_condition._screen_condition_ar1._getDataStr("ถึงลูกค้า");

                this._fromEmp = this._form_condition._screen_condition_ar1._getDataStr("จากพนักงานขาย");

                this._toEmp = this._form_condition._screen_condition_ar1._getDataStr("ถึงพนักงานขาย");

 
                //0="ทั้งหมด", 1="เงินสด", 2="บัตรเครดิต", 3="เช็ค", 4="เงินโอน", 5="เงินสดย่อย", 6="ยอดเงินเกินในใบเสร็จ", 7="ยอดเงินเกินนำมาหัก", 8="รายได้อื่นๆ", 9="ค่าใช้จ่ายอื่นๆ" }
                this._showType = this._form_condition._screen_condition_ar1._getDataStr("แสดงรายการ");

                this._showDocumentCutPayment = this._form_condition._screen_condition_ar1._getDataStr("แสดงเอกสารตัดชำระหนี้") == "1" ? true : false;

                this._showByEmp = this._form_condition._screen_condition_ar1._getDataStr("แสดงตามพนักงานขาย") == "1" ? true : false;

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}
