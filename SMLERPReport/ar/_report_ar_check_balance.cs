using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ar
{
    public partial class _report_ar_check_balance : UserControl
    {
        private SMLReport._generate _report;

        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;
        private _condition_ar _form_condition = new _condition_ar();
        private string _fromAr;
        private string _toAr;
        private decimal _fromCreditBalance;
        private decimal _toCreditBalance;
        private decimal _fromRemainBalance;
        private decimal _toRemainBalance;

        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";

        public _report_ar_check_balance(string screenName)
        {
            InitializeComponent();

            this._report = new SMLReport._generate(screenName, true);
            this._form_condition._screenType = _screenConditionArType.ArCheckBalance;

            //this._view1._buttonCondition.Enabled = false;
            //this._view1._buttonExample.Enabled = false;
            //this._view1._loadDataByThread+=new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            //this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            //this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            //this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            //this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            //this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            this._report._query += _report__query;
            this._report._init += _report__init;
            this._report._showCondition += _report__showCondition;
            this._report._dataRowSelect += _report__dataRowSelect;
            this._report._renderValue += _report__renderValue;
            this._report._renderFont += _report__renderFont;
            this._report.Disposed += _report_Disposed;
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);

            //this._showCondition();
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        Font _report__renderFont(DataRow data, SMLReport._generateColumnListClass source, SMLReport._generateLevelClass sender)
        {
            return source._font;
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {

        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (this._dataTable == null)
            {
                return null;
            }
            if (level._levelName.Equals(this._levelNameRoot))
            {
                return this._dataTable.Select();
            }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        void _report__init()
        {
            this._report._level = this._reportInitRoot(null, true, true);
            this._report._resourceTable = ""; // แบบกำหนดเอง
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = FontStyle.Regular;

            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._code, null, 5, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._name_1, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_name, 15, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ap_ar_resource._credit_money_max, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._credit_money_max, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle, false, false));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ap_ar_resource._credit_money, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._credit_money, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle, false, false));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_balance, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.cb_petty_cash._balance_money, _g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._balance_money, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));

            /*
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._name_1, null, 15, SMLReport._report._cellType.DateTime, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));*/

        }


        void _report__query()
        {
            if (this._dataTable == null)
            {
                try
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    SMLERPARAPInfo._process __arapProcess = new SMLERPARAPInfo._process();
                    StringBuilder __custWhere = new StringBuilder();
                    StringBuilder __amountWhere = new StringBuilder();
                    
                    if (this._fromAr.Length > 0 && this._toAr.Length > 0)
                        __custWhere.Append(" where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " between \'" + this._fromAr + "\' and \'" + this._toAr + "\'");

                    if (this._fromCreditBalance != 0 || this._toCreditBalance != 0)
                    {
                        __amountWhere.Append(" where " +  _g.d.ap_ar_resource._credit_money + " between " + this._fromCreditBalance + " and " + this._toCreditBalance);
                    }

                    if (this._fromRemainBalance != 0 || this._toRemainBalance != 0)
                    {
                        if (__amountWhere.Length > 0)
                            __amountWhere.Append(" and ");
                        else {
                            __amountWhere.Append(" where ");
                        }
                        __amountWhere.Append("(" + _g.d.ap_ar_resource._credit_money + " - " + _g.d.ap_ar_resource._ar_balance + ") between " + this._fromRemainBalance + " and " + this._toRemainBalance);
                    }


                    string __queryHeadStr = "select " + _g.d.ar_customer._code + "" +
                        ", " + _g.d.ar_customer._name_1 + " " +
                        ", coalesce((select " + _g.d.ar_customer_detail._credit_money + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), 0) as " + _g.d.ap_ar_resource._credit_money + " " + // วงเงิน
                        ",coalesce((select " + _g.d.ar_customer_detail._credit_money_max + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), 0) as " + _g.d.ap_ar_resource._credit_money_max + " " + // วงเงินสูงสุด
                        ",coalesce((select " + _g.d.ar_customer_detail._credit_status + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "), 0) as " + _g.d.ar_customer_detail._credit_status + " " + // สถานะเครดิต
                        ",(select coalesce(sum(amount),0) from (" + __arapProcess._custStatQuery(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้, "") + ") as temp6) as " + _g.d.ap_ar_resource._ar_balance + " " + // หนี้ค้าง
                        " from " + _g.d.ar_customer._table + " " + __custWhere;

                    string __query2 = "select " +
                        _g.d.ar_customer._code + " as \"" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "\" " +
                         "," + _g.d.ar_customer._name_1 + " as \"" + _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1 + "\" " +
                         "," + _g.d.ap_ar_resource._credit_money + " as \"" + _g.d.ar_customer._table + "." + _g.d.ap_ar_resource._credit_money + "\" " +
                         "," + _g.d.ap_ar_resource._credit_money_max + " as \"" + _g.d.ar_customer._table + "." + _g.d.ap_ar_resource._credit_money_max + "\" " +
                         "," + _g.d.ap_ar_resource._ar_balance + " as \"" + _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_balance + "\" " +
                         ", (" + _g.d.ap_ar_resource._credit_money + " - " + _g.d.ap_ar_resource._ar_balance + ") as \"" + _g.d.ap_ar_resource._table + "." + _g.d.cb_petty_cash._balance_money + "\" " +
                         " from (" + __queryHeadStr + ") as temp1 " + __amountWhere.ToString() + " order by " + _g.d.ar_customer._code;

                    this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query2).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        /*

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            //Write Header Report
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานตรวจสอบยอดวงเงิน", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
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
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วงเงินเครดิตสูงสุด", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วงเงินเครดิตที่อนุมัติ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดวงเงินคงเหลือ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าเช็คคืน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าเช็คคงค้าง", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "บัตรเครดิตในมือ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดลูกหนี้คงค้าง", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รวมมูลค่าทั้งสิ้น", "", SMLReport._report._cellAlign.Right);
                return true;
            }
            return false;
        }
        */
        /*
        void _view1__getDataObject()
        {
            Font __newFont = null;
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__detailObject._columnList[0];
            SMLReport._report._objectListType __dataObject = null;
            //this._view1._reportProgressBar.Value = 0;
            //this._view1._reportProgressBar.Minimum = 0;
            //this._view1._reportProgressBar.Maximum = this._dataTable.Rows.Count;

            if (this._dataTable == null) return;
            DataRow[] __dataRows = this._dataTable.Select();
            for (int __row = 0; __row < __dataRows.Length; __row++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject, __dataObject);
                this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRows[__row].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 2, __dataRows[__row].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 3, __dataRows[__row].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 4, __dataRows[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 5, __dataRows[__row].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 6, __dataRows[__row].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 7, __dataRows[__row].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 8, __dataRows[__row].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 9, __dataRows[__row].ItemArray[9].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                //this._view1._reportProgressBar.Value++;
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
        */

        private void _showCondition()
        {
            this._dataTable = null; // จะได้ load data ใหม่
            this._form_condition._process = false;
            this._form_condition.ShowDialog();
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());

            if (this._form_condition._process)
            {
                this._fromAr = this._form_condition._screen_condition_ar1._getDataStr("จากลูกหนี้");

                this._toAr = this._form_condition._screen_condition_ar1._getDataStr("ถึงลูกหนี้");

                this._fromCreditBalance = this._form_condition._screen_condition_ar1._getDataNumber("จากยอดวงเงินเครดิต");

                this._toCreditBalance = this._form_condition._screen_condition_ar1._getDataNumber("ถึงยอดวงเงินเครดิต");

                this._fromRemainBalance = this._form_condition._screen_condition_ar1._getDataNumber("จากยอดวงเงินคงเหลือ");

                this._toRemainBalance = this._form_condition._screen_condition_ar1._getDataNumber("ถึงยอดวงเงินคงเหลือ");

                //this._view1._buildReport(SMLReport._report._reportType.Standard);
                StringBuilder __conditionText = new StringBuilder();
                if (this._toAr.Length > 0 && this._fromAr.Length > 0)
                {
                    __conditionText.Append(MyLib._myGlobal._resource("จากรหัสลูกหนี้") + " " + this._fromAr + " " + MyLib._myGlobal._resource("ถึงรหัสลูกหนี้") + " " + this._toAr);
                }

                if ((this._fromCreditBalance != 0 || this._toCreditBalance != 0))
                {
                    if (__conditionText.Length > 0)
                        __conditionText.Append(",");
                    __conditionText.Append(MyLib._myGlobal._resource("จากยอดวงเงินเครดิต") + " " + string.Format(__formatNumber, this._fromCreditBalance) + " " + MyLib._myGlobal._resource("ถึงยอดวงเงินเครดิต") + " " + string.Format(__formatNumber, this._toCreditBalance));
                }

                if ((this._fromRemainBalance != 0 || this._toRemainBalance != 0))
                {
                    if (__conditionText.Length > 0)
                        __conditionText.Append(",");
                    __conditionText.Append(MyLib._myGlobal._resource("จากยอดวงเงินคงเหลือ") + " " + string.Format(__formatNumber, this._fromRemainBalance) + " " + MyLib._myGlobal._resource("ถึงยอดวงเงินคงเหลือ") + " " + string.Format(__formatNumber, this._toRemainBalance));
                }

                this._report._conditionTextDetail = __conditionText.ToString();
                this._report._build();

            }
        }
    }
}