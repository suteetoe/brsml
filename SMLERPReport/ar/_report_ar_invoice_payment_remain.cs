using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ar
{
    public partial class _report_ar_invoice_payment_remain : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;
        private _condition_ar _form_condition = new _condition_ar();

        private DateTime _remainAtDate;
        private DateTime _fromBillingDate;
        private DateTime _toBillingDate;
        private DateTime _fromDueDate;
        private DateTime _toDueDate;

        private string _remainAtDateStr;
        private string _fromBillingDateStr;
        private string _toBillingDateStr;
        private string _fromDueDateStr;
        private string _toDueDateStr;

        private string _fromDocumentNo;
        private string _toDocumentNo;
        private string _fromAr;
        private string _toAr;

        public _report_ar_invoice_payment_remain()
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

            this._form_condition._screenType = _screenConditionArType.ArInvoicePaymentRemain;
            this._showCondition();
        }

        void _view1__loadDataByThread()
        {
            // this._dataTable == null ไม่ต้อง load ซ้ำ
            if (this._dataTable == null)
            {
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                try
                {
                    string __remainAtDate = MyLib._myGlobal._convertDateToQuery(this._remainAtDate);
                    string __dateBillingBegin = MyLib._myGlobal._convertDateToQuery(this._fromBillingDate);
                    string __dateBillingEnd = MyLib._myGlobal._convertDateToQuery(this._toBillingDate);
                    string __dateDueBegin = MyLib._myGlobal._convertDateToQuery(this._fromDueDate);
                    string __dateDueEnd = MyLib._myGlobal._convertDateToQuery(this._toDueDate);
                    StringBuilder __where = new StringBuilder("");

                    __where.Append(" where " + _g.d.ic_trans._doc_date + "<" + this._remainAtDateStr);

                    if (_fromBillingDateStr.Equals("null") == false)
                    {
                        __where.Append(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date + " between \'" + _fromBillingDateStr + "' and \'" + _toBillingDateStr + "'");
                    }

                    //if (this._conditionFromTo == null)
                    //{
                    //    __where.Append(_g.d.ar_customer._table + "." + _g.d.ar_customer._code + " between \'" + this._fromAr + "\' and \'" + this._toAr + "\'");
                    //}
                    //else
                    //{
                    //    for (int __row = 0; __row < this._conditionFromTo.Rows.Count; __row++)
                    //    {
                    //        __where.Append(_g.d.ar_customer._table + "." + _g.d.ar_customer._code + " between \'" + this._conditionFromTo.Rows[__row][0].ToString() + "\' and \'" + this._conditionFromTo.Rows[__row][1].ToString() + "\'");
                    //        if (__row != this._conditionFromTo.Rows.Count - 1)
                    //        {
                    //            __where.Append(" or ");
                    //        }
                    //    }
                    //}

                    //string __whereStr = "";
                    //if (__where.Length > 0)
                    //{
                    //    __whereStr = " where " + __where.ToString();
                    //}
                    string __orderBy = "order by " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code;
                    this._dataTable = __smlFrameWork._processArStatus(MyLib._myGlobal._databaseName, __dateBillingBegin, __dateBillingEnd, __where.ToString(), __orderBy, 2).Tables[0];
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานใบส่งของค้างชำระ-ตามวันที่", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
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
                float __calcWidth = 85f / 9f;
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ใบกำกับภาษี", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ครบกำหนด", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อลูกหนี้", "", SMLReport._report._cellAlign.Left);
                //this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หมายเหตุ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดตั้งหนี้", "", SMLReport._report._cellAlign.Right);
                //this._view1._addColumn(__detailObject, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดลดหนี้", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รับชำระหนี้", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดคงเหลือ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เกิน(วัน)", "", SMLReport._report._cellAlign.Right);
                //this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ข้อมูลรายวัน", "", SMLReport._report._cellAlign.Left);
                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            Font __newFont = null;
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__detailObject._columnList[0];
            SMLReport._report._objectListType __dataObject = null;

            string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            decimal __sum_cn_balance = 0;
            decimal __sum_pay_billing = 0;
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
                this._view1._addDataColumn(__detailObject, __dataObject, 6, MyLib._myUtil._moneyFormat(MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[7].ToString()), _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 7, MyLib._myUtil._moneyFormat(MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[8].ToString()), _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                __sum_cn_balance = MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[7].ToString());
                __sum_pay_billing = MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[8].ToString());
                this._view1._addDataColumn(__detailObject, __dataObject, 8, MyLib._myUtil._moneyFormat(__sum_cn_balance + __sum_pay_billing, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 9, __dataRows[__row].ItemArray[9].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
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
                //string __date = this._form_condition._screen_condition_ar1._getDataStr("ยอดคงเหลือ ณ วันที่");
                //string[] __arrayDate = __date.Split('/');
                //this._remainAtDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());
                this._remainAtDate = this._form_condition._screen_condition_ar1._getDataDate("ยอดคงเหลือ ณ วันที่");
                this._remainAtDateStr = this._form_condition._screen_condition_ar1._getDataStrQuery("ยอดคงเหลือ ณ วันที่");

                //__date = this._form_condition._screen_condition_ar1._getDataStr("จากวันที่ออกบิล");
                //__arrayDate = __date.Split('/');
                //this._fromBillingDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());
                this._fromBillingDate = this._form_condition._screen_condition_ar1._getDataDate("จากวันที่ออกบิล");
                this._fromBillingDateStr = this._form_condition._screen_condition_ar1._getDataStrQuery("จากวันที่ออกบิล");

                //__date = this._form_condition._screen_condition_ar1._getDataStr("ถึงวันที่ออกบิล");
                //__arrayDate = __date.Split('/');
                //this._toBillingDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());
                this._toBillingDate = this._form_condition._screen_condition_ar1._getDataDate("ถึงวันที่ออกบิล");
                this._toBillingDateStr = this._form_condition._screen_condition_ar1._getDataStrQuery("ถึงวันที่ออกบิล");

                //__date = this._form_condition._screen_condition_ar1._getDataStr("จากวันที่ครบกำหนด");
                //__arrayDate = __date.Split('/');
                //this._fromDueDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());
                this._fromDueDate = this._form_condition._screen_condition_ar1._getDataDate("จากวันที่ครบกำหนด");
                this._fromDueDateStr = this._form_condition._screen_condition_ar1._getDataStrQuery("จากวันที่ครบกำหนด");

                //__date = this._form_condition._screen_condition_ar1._getDataStr("ถึงวันที่ครบกำหนด");
                //__arrayDate = __date.Split('/');
                //this._toDueDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());
                this._toDueDate = this._form_condition._screen_condition_ar1._getDataDate("ถึงวันที่ครบกำหนด");
                this._toDueDateStr = this._form_condition._screen_condition_ar1._getDataStrQuery("ถึงวันที่ครบกำหนด");

                this._fromDocumentNo = this._form_condition._screen_condition_ar1._getDataStr("จากเลขที่เอกสาร");

                this._toDocumentNo = this._form_condition._screen_condition_ar1._getDataStr("ถึงเลขที่เอกสาร");

                this._fromAr = this._form_condition._screen_condition_ar1._getDataStr("จากลูกค้า");

                this._toAr = this._form_condition._screen_condition_ar1._getDataStr("ถึงลูกค้า");

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}
