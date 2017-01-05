using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SMLERPGL._tax;

namespace SMLERPGL._report._tax
{
    public partial class _wht : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private _whtConditionForm _condition;
        private DataTable _dataTable;
        private _whtConditionType _conditionType;
        SMLReport._report._objectListType __ojtReport;
        private string[] _tax_detail_column = {_g.d.gl_wht_list_detail._table+"."+_g.d.gl_wht_list_detail._cust_code,
                                             _g.d.gl_wht_list_detail._table+"."+_g.d.gl_wht_list_detail._doc_no,
                                             _g.d.gl_wht_list_detail._table+"."+_g.d.gl_wht_list_detail._doc_date};

        Boolean showPrintDialogByCtrl = false;
        string _screen_code = "";

        public _wht(_whtConditionType conditionType, string screenName)
        {
            InitializeComponent();
            this._conditionType = conditionType;
            this._condition = new _whtConditionForm(conditionType, screenName);
            this._view1._pageSetupDialog.PageSettings.Landscape = true;
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonExample.Enabled = false;
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            this._view1._buttonFormPrint.Click += new EventHandler(_buttonFormPrint_Click);
            //

            // toe เพิ่ม ปุ่มพิมพ์จากแบบฟอร์ม

            if (_conditionType == _whtConditionType.หักณที่จ่ายภงด53)
            {
                this._view1._buttonFormPrint.Visible = true;
                this._view1._buttonFormPrint.ResourceName = "พิมพ์แบบยื่นรายการภาษีเงินได้หัก ณ ที่จ่าย (ภงด.53)";
                this._screen_code = "RWHT53";
            }
            else if (_conditionType == _whtConditionType.หักณที่จ่ายภงด3)
            {
                this._view1._buttonFormPrint.Visible = true;
                this._view1._buttonFormPrint.ResourceName = "พิมพ์แบบยื่นรายการภาษีเงินได้หัก ณ ที่จ่าย (ภงด.3)";
                this._screen_code = "RWHT3";
            }

            //switch (conditionType)
            //{
            //    case _whtConditionType.หักณที่จ่ายภงด3:
            //        break;
            //    case _whtConditionType.หักณที่จ่ายภงด53:
            //        break;
            //    case _whtConditionType.ถูกหักณที่จ่าย:
            //        this._screen_code = "RPTWHT";
            //        break;
            //}

            this._showCondition();
        }

        void _buttonFormPrint_Click(object sender, EventArgs e)
        {
            // เรียก พิมพ์จาก form design 

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                showPrintDialogByCtrl = true;
            }

            string __begindate = MyLib._myGlobal._convertDateToQuery(this._condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_begin));
            string __enddate = MyLib._myGlobal._convertDateToQuery(this._condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_end));


            //string __begindate = MyLib._myGlobal._convertDateToQuery(DateTime.Parse(this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_begin), MyLib._myGlobal._cultureInfo()));
            //string __enddate = MyLib._myGlobal._convertDateToQuery(DateTime.Parse(this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_end), MyLib._myGlobal._cultureInfo()));

            List<SMLERPReportTool._ReportToolCondition> __condition = new List<SMLERPReportTool._ReportToolCondition>();
            __condition.Add(new SMLERPReportTool._ReportToolCondition("start_date", __begindate));
            __condition.Add(new SMLERPReportTool._ReportToolCondition("end_date", __enddate));

            SMLERPReportTool._global._printForm(_screen_code, (SMLERPReportTool._ReportToolCondition[])__condition.ToArray(), true);

            showPrintDialogByCtrl = false;

        }

        void _view1__loadDataByThread()
        {
            if (this._dataTable == null)
            {
                try
                {
                    string __getCustName = _g.d.gl_wht_list_detail._cust_code;
                    string __getCustAddress = "";
                    string __getCustTaxNo = "";
                    string __getCustStatus = "";
                    string __transFlag = "";
                    string __subWhere = " and cust_code is not null";
                    string __transFlagSale = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + "," +
                               _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + "," +
                               _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString() + "," +
                               _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString() + "," +
                               _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน).ToString() + "," +
                               _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ).ToString() + "," +
                               _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString() + "," +
                               _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย).ToString();
                    string __transFlagBuy = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString() + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString() + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString() + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString() + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีหักณที่จ่าย).ToString() + "," +
                        _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ).ToString();
                    switch (this._conditionType)
                    {
                        case _whtConditionType.หักณที่จ่ายภงด3:
                        case _whtConditionType.หักณที่จ่ายภงด53:
                            __getCustName = "coalesce((select " + _g.d.gl_wht_list._cust_name + " || case when cust_code = '' then '' else '(' || cust_code || ')' end from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ") ||'('||" + _g.d.gl_wht_list_detail._cust_code + "||')' ) ";
                            __transFlag = __transFlagBuy;
                            __getCustAddress = "coalesce((select " + _g.d.gl_wht_list._cust_address + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ap_supplier._address + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + "))";
                            __getCustTaxNo = "coalesce((select " + _g.d.gl_wht_list._tax_number + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ap_supplier_detail._tax_id + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + "))";
                            __getCustStatus = "coalesce((select " + _g.d.gl_wht_list._cust_tax_type + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ap_supplier._ap_status + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + "))";
                            break;
                        case _whtConditionType.ถูกหักณที่จ่าย:
                            __getCustName = "coalesce((select " + _g.d.gl_wht_list._cust_name + " || case when cust_code = '' then '' else '(' || cust_code || ')' end from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ") ||'('||" + _g.d.gl_wht_list_detail._cust_code + "||')' ) ";
                            __transFlag = __transFlagSale;
                            __getCustAddress = "coalesce((select " + _g.d.gl_wht_list._cust_address + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ar_customer._address + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ")) ";
                            __getCustTaxNo = "coalesce((select " + _g.d.gl_wht_list._tax_number + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ar_customer_detail._tax_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ")) ";
                            __getCustStatus = "coalesce((select " + _g.d.gl_wht_list._cust_tax_type + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ar_customer._ar_status + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ")) ";
                            break;
                    }
                    // Qurey Where
                    string __vatmonth = ((MyLib._myComboBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndex.ToString();
                    string __vatyear = this._condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_year).ToString();
                    string __begindate = this._condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_begin);
                    string __enddate = this._condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_end);
                    StringBuilder __Qureywhere = new StringBuilder();

                    if (__begindate.Length == 0 && __enddate.Length == 0)
                    {
                        string __day = GetDaysInMonth(MyLib._myGlobal._intPhase(__vatmonth) + 1, MyLib._myGlobal._intPhase(__vatyear)).ToString();
                        __Qureywhere.Append(String.Format(" where ({0} in (" + __transFlag + ")) and ({1} between \'{2}\' and \'{3}\')",
                        _g.d.gl_wht_list_detail._trans_flag,
                        _g.d.gl_wht_list_detail._due_date,
                        __vatyear + "-" + (__vatmonth + 1) + "-01",
                        __vatyear + "-" + (__vatmonth + 1) + "-" + __day));
                    }
                    else
                    {
                        DateTime __dateBegin = this._condition._conditionScreenTop._getDataDate(_g.d.resource_report_vat._date_begin);
                        DateTime __dateEnd = this._condition._conditionScreenTop._getDataDate(_g.d.resource_report_vat._date_end);

                        if (__dateBegin.Year > 2500)
                        {
                            __dateBegin = __dateBegin.AddYears(-543);
                            __dateEnd = __dateEnd.AddYears(-543);
                        }
                        string __datebeginQuery = MyLib._myGlobal._convertDateToQuery(__dateBegin);
                        string __dateEndQuery = MyLib._myGlobal._convertDateToQuery(__dateEnd);

                        __Qureywhere.Append(String.Format(" where ({0} in (" + __transFlag + ")) and ({1} between \'{2}\' and \'{3}\')",
                         _g.d.gl_wht_list_detail._trans_flag,
                        _g.d.gl_wht_list_detail._due_date,
                        __datebeginQuery,
                       __dateEndQuery));
                    }

                    int __sortByIndex = (int)((MyLib._myComboBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._sort_by)).SelectedIndex;
                    string __orderBy = this._condition._vatSortWhtByFieldName[__sortByIndex].ToString();
                    //string __orderBy = " cust_code ";
                    string __query = String.Format("select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}," + _g.d.gl_wht_list_detail._tax_doc_no + " from " + _g.d.gl_wht_list_detail._table + __Qureywhere + __subWhere + " order by " + __orderBy,
                        _g.d.gl_wht_list_detail._due_date,
                        _g.d.gl_wht_list_detail._income_type,
                        _g.d.gl_wht_list_detail._tax_rate,
                        __getCustName + " as " + _g.d.gl_wht_list_detail._cust_code,
                        __getCustAddress + " as " + _g.d.resource_report_vat._cust_address,
                        __getCustTaxNo + " as " + _g.d.resource_report_vat._cust_taxno,
                        __getCustStatus + " as " + _g.d.resource_report_vat._cust_status,
                        _g.d.gl_wht_list_detail._amount,
                        _g.d.gl_wht_list_detail._tax_value,
                        _g.d.gl_wht_list_detail._doc_no,
                        _g.d.gl_wht_list_detail._doc_date,
                        _g.d.gl_wht_list_detail._trans_flag);
                    //
                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    this._view1._loadDataByThreadSuccess = false;
                    return;
                }
            }
            this._view1._loadDataByThreadSuccess = true;
        }

        public static int GetDaysInMonth(int month, int year)
        {
            if (month < 1 || month > 12)
            {
                //
            }
            if (1 == month || 3 == month || 5 == month || 7 == month || 8 == month ||
            10 == month || 12 == month)
            {
                return 31;
            }
            else if (2 == month)
            {
                // Check for leap year
                year = year - 543;
                if (0 == (year % 4))
                {
                    // If date is divisible by 400, it's a leap year.
                    // Otherwise, if it's divisible by 100 it's not.
                    if (0 == (year % 400))
                    {
                        return 29;
                    }
                    else if (0 == (year % 100))
                    {
                        return 28;
                    }
                    // Divisible by 4 but not by 100 or 400
                    // so it leaps
                    return 29;
                }
                // Not a leap year
                return 28;
            }
            return 30;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            //Write Header Report
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                switch (this._conditionType)
                {
                    case _whtConditionType.หักณที่จ่ายภงด3:
                    case _whtConditionType.หักณที่จ่ายภงด53:
                        string __month = ((MyLib._myComboBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._vat_month)).SelectedItem.ToString();
                        this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, this._condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._report_name), SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                        this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_month, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_month))._str + "  " + __month + "  " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_year, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_year))._str + "  " + this._condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_year), SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                        this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._compamy_name, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._compamy_name))._str + "\t: " + SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                        this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._pang_no, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._pang_no))._str + " : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                        this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._business_name, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._business_name))._str + "\t: " + MyLib._myGlobal._ltdBusinessName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                        this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._workplace, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._workplace))._str + "\t: " + MyLib._myGlobal._ltdWorkplace, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                        this._view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._address, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._address))._str + "\t: " + MyLib._myGlobal._ltdAddress, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                        this._view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno))._str + "\t: " + MyLib._myGlobal._ltdTax, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);

                        return true;
                    case _whtConditionType.ถูกหักณที่จ่าย:
                        this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                        this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._title, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._title))._str + "\t: " + this._condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._report_name), SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                        this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._pang_no, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._pang_no))._str + " : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                        this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._printed_by, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._printed_by))._str + "\t: " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                        this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._printed_date, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._printed_date))._str + " : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                        return true;
                        break;
                }

            }
            else if (type == SMLReport._report._objectType.Detail)
            {

                switch (this._conditionType)
                {
                    case _whtConditionType.หักณที่จ่ายภงด3:
                    case _whtConditionType.หักณที่จ่ายภงด53:
                        if (this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._show_remark) != null)
                        {
                            if (((MyLib._myCheckBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._show_remark)).Checked)
                            {
                                //พิมพ์ชื่อฟิลด์บน
                                __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                                _view1._addColumn(__ojtReport, 4, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._number, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ar_name, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._wht_doc_no, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._d_m_y, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._amount, "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax, "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._remark, "", SMLReport._report._cellAlign.Default);
                                //พิมพ์ชื่อฟิลด์ล่าง
                                __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                                _view1._addColumn(__ojtReport, 4, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ar_address, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._pay, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._status_pay, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax_rate, "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._now_pay, "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._now_sent, "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);

                            }
                            else
                            {
                                //พิมพ์ชื่อฟิลด์บน
                                __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                                _view1._addColumn(__ojtReport, 4, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._number, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 35, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ar_name, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._wht_doc_no, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._d_m_y, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._amount, "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax, "", SMLReport._report._cellAlign.Right);

                                //พิมพ์ชื่อฟิลด์ล่าง
                                __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                                _view1._addColumn(__ojtReport, 4, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 35, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ar_address, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._pay, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._status_pay, "", SMLReport._report._cellAlign.Default);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax_rate, "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._now_pay, "", SMLReport._report._cellAlign.Right);
                                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._now_sent, "", SMLReport._report._cellAlign.Right);


                            }
                        }
                        return true;
                    case _whtConditionType.ถูกหักณที่จ่าย:
                        if (((MyLib._myCheckBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._show_remark)).Checked)
                        {
                            //พิมพ์ชื่อฟิลด์บน
                            __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                            _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ap_name, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._remark, "", SMLReport._report._cellAlign.Default);
                            //พิมพ์ชื่อฟิลด์ล่าง
                            __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                            _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ap_address, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._day_get, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._status_get, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax_rate, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._amount, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                        }
                        else
                        {
                            //พิมพ์ชื่อฟิลด์บน
                            __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                            _view1._addColumn(__ojtReport, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ap_name, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);

                            //พิมพ์ชื่อฟิลด์ล่าง
                            __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                            _view1._addColumn(__ojtReport, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ap_address, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._day_get, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._status_get, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax_rate, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._amount, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax, "", SMLReport._report._cellAlign.Right);

                        }
                        return true;
                }

            }
            return false;
        }

        void _view1__getDataObject()
        {
            try
            {
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];
                SMLReport._report._objectListType __dataObject = null;
                string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                Font __totalFont = new Font(__getColumn._fontData, FontStyle.Bold);

                DataRow[] _dr = _dataTable.Select();
                string __custCode = "";
                string __chekcustCode = "";
                //ใส่รวมล่างสุด
                decimal __sumamount = 0M;
                decimal __sumtax = 0M;
                //ใส่ค่าตามข้อมูล
                decimal __amount = 0M;
                decimal __tax = 0M;
                decimal __taxrate = 0M;
                //ใส่ผลรวมตามเจ้าหนี้ลูกหนี้
                decimal __totalamount = 0M;
                decimal __totaltax = 0M;

                int __custLength = 0;
                int __row = 1;
                for (int _row = 0; _row < _dr.Length; _row++)
                {
                    //เช็คสถานะเจ้าหนี้/ลูกหนี้
                    //0 = บุคคลธรรมดา
                    //1 = นิติบุคคล                    
                    int __custStatus = MyLib._myGlobal._intPhase(_dr[_row][_g.d.resource_report_vat._cust_status].ToString().Equals("") ? "0" : _dr[_row][_g.d.resource_report_vat._cust_status].ToString());
                    //เช็ค Type ใส่ หมายเหตุ/เงื่อนไข
                    string __getNameType = "";
                    int __transType = MyLib._myGlobal._intPhase(_dr[_row][_g.d.ic_trans._trans_flag].ToString());
                    if (__transType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ))
                    {
                        __getNameType = "ขายสินค้า";
                    }
                    else if (__transType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้))
                    {
                        __getNameType = "เพิ่มหนี้";
                    }
                    else if (__transType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้))
                    {
                        __getNameType = "รับคืน/ลดหนี้";
                    }
                    else if (__transType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้))
                    {
                        __getNameType = "ลูกหนี้/รับชำระหนี้";
                    }
                    else if (__transType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ))
                    {
                        __getNameType = "ซื้อสินค้า";
                    }
                    else if (__transType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด))
                    {
                        __getNameType = "ลดหนี้";
                    }
                    else if (__transType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด))
                    {
                        __getNameType = "ส่งคืน/ลดหนี้";
                    }
                    else if (__transType == _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้))
                    {
                        __getNameType = "เจ้าหนี้/จ่ายชำระหนี้";
                    }
                    switch (this._conditionType)
                    {
                        case _whtConditionType.หักณที่จ่ายภงด3:
                            if (((MyLib._myCheckBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._show_remark)).Checked)
                            {
                                if (__custStatus == 0)
                                {
                                    if (__chekcustCode.Equals(""))
                                    {
                                        __chekcustCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                    }
                                    //ใส่จำนวนเงินเตรียม format
                                    __taxrate = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString());
                                    __amount = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._amount].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._amount].ToString());
                                    __tax = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString());
                                    //ใส่จำนวนเงินล่างเจ้าหนี้
                                    __custCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                    if (__chekcustCode != __custCode)
                                    {
                                        //สรุป รวม รายการ ของแต่ ละ เจ้าหนี้
                                        __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        _view1._createEmtryColumn(__ojtReport, __dataObject);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 9, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        __sumamount = __totalamount + __sumamount;
                                        __sumtax = __totaltax + __sumtax;
                                        __custLength = 0;
                                        __chekcustCode = __custCode;
                                        __totalamount = 0M;
                                        __totaltax = 0M;
                                    }
                                    //ชื่อเจ้าหนี้
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, (__row).ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 9, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    //ที่อยู่ และรายละเอียด
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.resource_report_vat._cust_address].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row][_g.d.resource_report_vat._cust_taxno].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.gl_wht_list_detail._tax_doc_no].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.gl_wht_list_detail._due_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, _dr[_row][_g.d.gl_wht_list_detail._income_type].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __taxrate), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __tax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 9, __getNameType, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    __row++;
                                    //รวมจำนวนเงิน                            
                                    __totalamount = __amount + __totalamount;
                                    __totaltax = __tax + __totaltax;

                                    __custLength++;

                                }
                                if (_row == (_dr.Length - 1))
                                {
                                    //สรุปรวมรายการของแต่ละเจ้าหนี้ อันสุดท้าย
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 9, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    __sumamount = __totalamount + __sumamount;
                                    __sumtax = __totaltax + __sumtax;
                                    __custLength = 0;
                                    __chekcustCode = __custCode;
                                    __totalamount = 0M;
                                    __totaltax = 0M;
                                }
                            }
                            else
                            {
                                if (__custStatus == 0)
                                {
                                    if (__chekcustCode.Equals(""))
                                    {
                                        __chekcustCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                    }
                                    //ใส่จำนวนเงินเตรียม format
                                    __taxrate = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString());
                                    __amount = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._amount].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._amount].ToString());
                                    __tax = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString());
                                    //ใส่จำนวนเงินล่างเจ้าหนี้
                                    __custCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                    if (__chekcustCode != __custCode)
                                    {
                                        //สรุป รวม รายการ ของแต่ ละ เจ้าหนี้
                                        __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        _view1._createEmtryColumn(__ojtReport, __dataObject);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        //_view1._addDataColumn(__ojtReport, __dataObject, 8, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                                        __sumamount = __totalamount + __sumamount;
                                        __sumtax = __totaltax + __sumtax;
                                        __custLength = 0;
                                        __chekcustCode = __custCode;
                                        __totalamount = 0M;
                                        __totaltax = 0M;
                                    }
                                    //ชื่อเจ้าหนี้
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, (__row).ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    //_view1._addDataColumn(__ojtReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                                    //ที่อยู่ และรายละเอียด
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.resource_report_vat._cust_address].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row][_g.d.resource_report_vat._cust_taxno].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.gl_wht_list_detail._tax_doc_no].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.gl_wht_list_detail._due_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, _dr[_row][_g.d.gl_wht_list_detail._income_type].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __taxrate), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __tax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    //_view1._addDataColumn(__ojtReport, __dataObject, 8, __getNameType, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                                    __row++;
                                    //รวมจำนวนเงิน                            
                                    __totalamount = __amount + __totalamount;
                                    __totaltax = __tax + __totaltax;

                                    __custLength++;

                                }
                                if (_row == (_dr.Length - 1))
                                {
                                    //สรุปรวมรายการของแต่ละเจ้าหนี้ อันสุดท้าย
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    //_view1._addDataColumn(__ojtReport, __dataObject, 8, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                                    __sumamount = __totalamount + __sumamount;
                                    __sumtax = __totaltax + __sumtax;
                                    __custLength = 0;
                                    __chekcustCode = __custCode;
                                    __totalamount = 0M;
                                    __totaltax = 0M;
                                }
                            }

                            break;
                        case _whtConditionType.หักณที่จ่ายภงด53:
                            if (((MyLib._myCheckBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._show_remark)).Checked)
                            {
                                if (__custStatus == 1)
                                {
                                    if (__chekcustCode.Equals(""))
                                    {
                                        __chekcustCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                    }
                                    //ใส่จำนวนเงินเตรียม format
                                    __taxrate = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString());
                                    __amount = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._amount].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._amount].ToString());
                                    __tax = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString());
                                    //ใส่จำนวนเงินล่างเจ้าหนี้
                                    __custCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                    if (__chekcustCode != __custCode)
                                    {
                                        //สรุป รวม รายการ ของแต่ ละ เจ้าหนี้
                                        __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        _view1._createEmtryColumn(__ojtReport, __dataObject);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 9, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        __sumamount = __totalamount + __sumamount;
                                        __sumtax = __totaltax + __sumtax;
                                        __custLength = 0;
                                        __chekcustCode = __custCode;
                                        __totalamount = 0M;
                                        __totaltax = 0M;
                                    }
                                    //ชื่อเจ้าหนี้
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, (__row).ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 9, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    //ที่อยู่ และรายละเอียด
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.resource_report_vat._cust_address].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row][_g.d.resource_report_vat._cust_taxno].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.gl_wht_list_detail._tax_doc_no].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.gl_wht_list_detail._due_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, _dr[_row][_g.d.gl_wht_list_detail._income_type].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __taxrate), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __tax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 9, __getNameType, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    __row++;
                                    //รวมจำนวนเงิน                            
                                    __totalamount = __amount + __totalamount;
                                    __totaltax = __tax + __totaltax;

                                    __custLength++;

                                }
                                if (_row == (_dr.Length - 1))
                                {
                                    //สรุปรวมรายการของแต่ละเจ้าหนี้ อันสุดท้าย
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 9, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    __sumamount = __totalamount + __sumamount;
                                    __sumtax = __totaltax + __sumtax;
                                    __custLength = 0;
                                    __chekcustCode = __custCode;
                                    __totalamount = 0M;
                                    __totaltax = 0M;
                                }
                            }
                            else
                            {
                                if (__custStatus == 1)
                                {
                                    if (__chekcustCode.Equals(""))
                                    {
                                        __chekcustCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                    }
                                    //ใส่จำนวนเงินเตรียม format
                                    __taxrate = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString());
                                    __amount = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._amount].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._amount].ToString());
                                    __tax = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString());
                                    //ใส่จำนวนเงินล่างเจ้าหนี้
                                    __custCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                    if (__chekcustCode != __custCode)
                                    {
                                        //สรุป รวม รายการ ของแต่ ละ เจ้าหนี้
                                        __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        _view1._createEmtryColumn(__ojtReport, __dataObject);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                        //_view1._addDataColumn(__ojtReport, __dataObject, 8, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                                        __sumamount = __totalamount + __sumamount;
                                        __sumtax = __totaltax + __sumtax;
                                        __custLength = 0;
                                        __chekcustCode = __custCode;
                                        __totalamount = 0M;
                                        __totaltax = 0M;
                                    }
                                    //ชื่อเจ้าหนี้
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, (__row).ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    //_view1._addDataColumn(__ojtReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                                    //ที่อยู่ และรายละเอียด
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.resource_report_vat._cust_address].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row][_g.d.resource_report_vat._cust_taxno].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.gl_wht_list_detail._tax_doc_no].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.gl_wht_list_detail._due_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, _dr[_row][_g.d.gl_wht_list_detail._income_type].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __taxrate), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __tax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    //_view1._addDataColumn(__ojtReport, __dataObject, 8, __getNameType, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                                    __row++;
                                    //รวมจำนวนเงิน                            
                                    __totalamount = __amount + __totalamount;
                                    __totaltax = __tax + __totaltax;

                                    __custLength++;

                                }
                                if (_row == (_dr.Length - 1))
                                {
                                    //สรุปรวมรายการของแต่ละเจ้าหนี้ อันสุดท้าย
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    //_view1._addDataColumn(__ojtReport, __dataObject, 8, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                                    __sumamount = __totalamount + __sumamount;
                                    __sumtax = __totaltax + __sumtax;
                                    __custLength = 0;
                                    __chekcustCode = __custCode;
                                    __totalamount = 0M;
                                    __totaltax = 0M;
                                }
                            }

                            break;
                        case _whtConditionType.ถูกหักณที่จ่าย:
                            if (((MyLib._myCheckBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._show_remark)).Checked)
                            {
                                if (__chekcustCode.Equals(""))
                                {
                                    __chekcustCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                }
                                //ใส่จำนวนเงินเตรียม format
                                __taxrate = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString());
                                __amount = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._amount].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._amount].ToString());
                                __tax = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString());
                                //ใส่จำนวนเงินล่างเจ้าหนี้
                                __custCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                if (__chekcustCode != __custCode)
                                {
                                    //สรุป รวม รายการ ของแต่ ละ เจ้าหนี้
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    __sumamount = __totalamount + __sumamount;
                                    __sumtax = __totaltax + __sumtax;
                                    __custLength = 0;
                                    __chekcustCode = __custCode;
                                    __totalamount = 0M;
                                    __totaltax = 0M;
                                }

                                //ชื่อเจ้าหนี้
                                __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(__ojtReport, __dataObject);
                                _view1._addDataColumn(__ojtReport, __dataObject, 0, _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                //ที่อยู่ และรายละเอียด
                                __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(__ojtReport, __dataObject);
                                _view1._addDataColumn(__ojtReport, __dataObject, 0, _dr[_row][_g.d.resource_report_vat._cust_address].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.resource_report_vat._cust_taxno].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 2, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.gl_wht_list_detail._due_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.gl_wht_list_detail._income_type].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 4, string.Format(__formatNumber, __taxrate), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__ojtReport, __dataObject, 5, string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __tax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__ojtReport, __dataObject, 7, __getNameType, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                //รวมจำนวนเงิน                            
                                __totalamount = __amount + __totalamount;
                                __totaltax = __tax + __totaltax;

                                __custLength++;
                                if (_row == (_dr.Length - 1))
                                {
                                    //สรุปรวมรายการของแต่ละเจ้าหนี้ อันสุดท้าย
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 7, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    __sumamount = __totalamount + __sumamount;
                                    __sumtax = __totaltax + __sumtax;
                                    __custLength = 0;
                                    __chekcustCode = __custCode;
                                    __totalamount = 0M;
                                    __totaltax = 0M;
                                }
                            }
                            else
                            {

                                if (__chekcustCode.Equals(""))
                                {
                                    __chekcustCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                }
                                //ใส่จำนวนเงินเตรียม format
                                __taxrate = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString());
                                __amount = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._amount].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._amount].ToString());
                                __tax = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString());
                                //ใส่จำนวนเงินล่างเจ้าหนี้
                                __custCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                if (__chekcustCode != __custCode)
                                {
                                    //สรุป รวม รายการ ของแต่ ละ เจ้าหนี้
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    __sumamount = __totalamount + __sumamount;
                                    __sumtax = __totaltax + __sumtax;
                                    __custLength = 0;
                                    __chekcustCode = __custCode;
                                    __totalamount = 0M;
                                    __totaltax = 0M;
                                }

                                //ชื่อเจ้าหนี้
                                __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(__ojtReport, __dataObject);
                                _view1._addDataColumn(__ojtReport, __dataObject, 0, _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                //ที่อยู่ และรายละเอียด
                                __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(__ojtReport, __dataObject);
                                _view1._addDataColumn(__ojtReport, __dataObject, 0, _dr[_row][_g.d.resource_report_vat._cust_address].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.resource_report_vat._cust_taxno].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 2, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.gl_wht_list_detail._due_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.gl_wht_list_detail._income_type].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, 4, string.Format(__formatNumber, __taxrate), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__ojtReport, __dataObject, 5, string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __tax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                //รวมจำนวนเงิน                            
                                __totalamount = __amount + __totalamount;
                                __totaltax = __tax + __totaltax;

                                __custLength++;
                                if (_row == (_dr.Length - 1))
                                {
                                    //สรุปรวมรายการของแต่ละเจ้าหนี้ อันสุดท้าย
                                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 3, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._included))._str + " " + __custLength.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 5, string.Format(__formatNumber, __totalamount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __totaltax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    __sumamount = __totalamount + __sumamount;
                                    __sumtax = __totaltax + __sumtax;
                                    __custLength = 0;
                                    __chekcustCode = __custCode;
                                    __totalamount = 0M;
                                    __totaltax = 0M;
                                }
                            }

                            break;
                    }
                }

                #region total
                switch (this._conditionType)
                {
                    case _whtConditionType.หักณที่จ่ายภงด3:
                    case _whtConditionType.หักณที่จ่ายภงด53:
                        //สรุปรวมรายการของเจ้าหนี้ ทั้งหมด
                        if (((MyLib._myCheckBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._show_remark)).Checked)
                        {
                            __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(__ojtReport, __dataObject);
                            _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._total, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._total))._str + " " + (__row - 1).ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __sumamount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __sumtax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 9, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                        }
                        else
                        {
                            __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(__ojtReport, __dataObject);
                            _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 3, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 5, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._total, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._total))._str + " " + (__row - 1).ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 6, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __sumamount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __sumtax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            //_view1._addDataColumn(__ojtReport, __dataObject, 8, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);

                        }

                        break;
                    case _whtConditionType.ถูกหักณที่จ่าย:
                        //สรุปรวมรายการของเจ้าหนี้ ทั้งหมด
                        if (((MyLib._myCheckBox)this._condition._conditionScreenTop._getControl(_g.d.resource_report_vat._show_remark)).Checked)
                        {
                            __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(__ojtReport, __dataObject);
                            _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 3, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._total, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._total))._str + " " + _dr.Length.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 5, string.Format(__formatNumber, __sumamount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __sumtax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 7, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                        }
                        else
                        {
                            __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(__ojtReport, __dataObject);
                            _view1._addDataColumn(__ojtReport, __dataObject, 0, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 1, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 2, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 3, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._total, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._total))._str + " " + _dr.Length.ToString() + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._item))._str, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataObject, 4, "", null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 5, string.Format(__formatNumber, __sumamount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __sumtax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);

                        }

                        break;
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(":: ERR :: \n" + ex.Message);
            }
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            //_view1._buildReport(SMLReport._report._reportType.Standard);
            if (this._condition._conditionScreenTop._checkEmtryField().Length != 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้กำหนดเงื่อนไข"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            this._dataTable = null; // จะได้ load data ใหม่
            this._view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            this._view1._buildReport(SMLReport._report._reportType.Standard);
            this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            //this._condition.ShowDialog();
            this._showCondition();
        }

        private void _showCondition()
        {
            if (this._condition == null)
            {
                this._condition._whereControl._tableName = _g.d.gl_wht_list_detail._table;
                this._condition._whereControl._addFieldComboBox(this._tax_detail_column);
            }
            this._condition._process = false;
            this._condition.ShowDialog();
            if (this._condition._process)
            {
                this._dataTable = null; // จะได้ load data ใหม่
                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}
