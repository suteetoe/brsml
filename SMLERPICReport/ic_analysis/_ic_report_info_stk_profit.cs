using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPICReport.ic_analysis
{
    public partial class _ic_report_info_stk_profit : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        Font __newFont;
        Font __newFont_bold;
        ArrayList __total1;

        //_conditonReceipt _conditonReceiptScreen = new _conditonReceipt();
        SMLReport._report._objectListType _objReport_ic;
        SMLReport._report._objectListType _objReport_doc;
        SMLReport._report._objectListType _objReport_cust;
        //DataSet _ds;
        DataTable _dataTable_ic;
        DataTable _dataTable_doc;
        DataTable _dataTable_cust;

        private ArrayList _col_name_1 = new ArrayList();
        private ArrayList _col_width_1 = new ArrayList();
        private ArrayList _col_name_doc = new ArrayList();
        private ArrayList _col_width_doc = new ArrayList();
        private ArrayList _col_name_cust = new ArrayList();
        private ArrayList _col_width_cust = new ArrayList();
        //---------------------------------------------------------------------------------------------------
        private string _report_name = "";
        //-------------------Condition------------------------
        string _data_condition = "";
        Boolean _check_submit = false;
        //----------------------------------------------------
        StringBuilder _query;
        StringBuilder _query_sub;
        _analysis_condition _condition;
        SMLERPICInfo._infoStkProfitEnum _mode;
        String _screenName = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0=สินค้า,1=เอกสาร,2=ลูกค้า</param>
        public _ic_report_info_stk_profit(string screenName, SMLERPICInfo._infoStkProfitEnum mode)
        {
            InitializeComponent();
            this._screenName = screenName;
            this._mode = mode;
            _view1._pageSetupDialog.PageSettings.Landscape = true;
            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            //_view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            _view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._showCondition(screenName);
        }

        void _view1__loadDataByThread()
        {
            // this._dataTable == null ไม่ต้อง load ซ้ำ
            if (this._dataTable_ic == null)
            {
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                try
                {

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }

                if (__smlFrameWork._lastError.Length > 0)
                {
                    MessageBox.Show(__smlFrameWork._lastError);
                }

            }
            this._view1._loadDataByThreadSuccess = true;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _printTotal(string title, ArrayList value)
        {
            // คำนวณ % double __profit_lost_persent = (__amount_net == 0) ? 0 : ((__profit_lost_amount * 100.0) / __amount_net);
            decimal __amount_net = (decimal)value[6];
            decimal __profit_lost_amount = (decimal)value[8];
            decimal __profit_lost_persent = (__amount_net == 0) ? 0 : ((__profit_lost_amount * 100.0M) / __amount_net);
            SMLReport._report._objectListType __dataObject_2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
            SMLReport._report._objectListType __objetReport = (_objReport_ic == null) ? _objReport_doc : _objReport_ic;
            if (__objetReport == null) __objetReport = _objReport_cust;
            _view1._createEmtryColumn(__objetReport, __dataObject_2);
            int __columnTrial = 0;
            if (_objReport_ic != null)
            {
                _view1._addDataColumn(__objetReport, __dataObject_2, __columnTrial++, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            }
            _view1._addDataColumn(__objetReport, __dataObject_2, __columnTrial++, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            _view1._addDataColumn(__objetReport, __dataObject_2, __columnTrial++, title, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            _view1._addDataColumn(__objetReport, __dataObject_2, __columnTrial++, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            for (int __loop = 0; __loop < 9; __loop++)
            {
                _view1._addDataColumn(__objetReport, __dataObject_2, __columnTrial++, MyLib._myGlobal._formatNumberForReport((__loop == 0) ? _g.g._companyProfile._item_qty_decimal : _g.g._companyProfile._item_amount_decimal, (decimal)value[__loop]), __newFont_bold, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
            }
            _view1._addDataColumn(__objetReport, __dataObject_2, __columnTrial, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __profit_lost_persent), __newFont_bold, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
        }

        void _getDataObjectByIc(Boolean fontBold, string docNo, string custCode, Boolean sumTotal)
        {
            string __query = "";
            if (docNo.Length > 0)
            {
                __query = "doc_no=\'" + docNo + "\'";
            }
            if (custCode.Length > 0)
            {
                __query = "ar_code=\'" + custCode + "\'";
            }
            DataRow[] __selectItem = this._dataTable_ic.Select(__query);
            for (int __row = 0; __row < __selectItem.Length; __row++)
            {
                SMLReport._report._objectListType __dataObject_1 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(_objReport_ic, __dataObject_1);
                string __itemCode = __selectItem[__row][0].ToString();
                Font __selectFont = (fontBold) ? __newFont_bold : __newFont;
                _view1._addDataColumn(_objReport_ic, __dataObject_1, 0, "", __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                _view1._addDataColumn(_objReport_ic, __dataObject_1, 1, __selectItem[__row][0].ToString(), __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                _view1._addDataColumn(_objReport_ic, __dataObject_1, 2, __selectItem[__row][1].ToString(), __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                _view1._addDataColumn(_objReport_ic, __dataObject_1, 3, __selectItem[__row][2].ToString(), __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                for (int __loop = 0; __loop < 10; __loop++)
                {
                    _view1._addDataColumn(_objReport_ic, __dataObject_1, __loop + 4, MyLib._myGlobal._formatNumberForReport((__loop == 0) ? _g.g._companyProfile._item_qty_decimal : _g.g._companyProfile._item_amount_decimal, this._convertNumber(__selectItem[__row][__loop + 3].ToString())), __selectFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    if (sumTotal)
                    {
                        __total1[__loop] = (decimal)__total1[__loop] + MyLib._myGlobal._decimalPhase(__selectItem[__row][__loop + 3].ToString());
                    }
                }
                switch (this._mode)
                {
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร:
                        this._getDataObjectByDoc(false, __itemCode, custCode, false, false);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                        this._getDataObjectByDoc(false, __itemCode, "", false, false);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า:
                        this._getDataObjectByCust(false, __itemCode, false, false, false, false);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร:
                        this._getDataObjectByCust(false, __itemCode, false, true, false, false);
                        break;
                }
            }
        }

        void _getDataObjectByDoc(Boolean fontBold, string itemCode, string custCode, Boolean getItem, Boolean sumTotal)
        {
            string __query = "";
            if (itemCode.Length > 0)
            {
                __query = "item_code=\'" + itemCode + "\'";
            }
            if (custCode.Length > 0)
            {
                if (__query.Length > 0)
                {
                    __query = __query + " and ";
                }
                __query = __query + "ar_code=\'" + custCode + "\'";
            }
            Font __selectFont = (fontBold) ? __newFont_bold : __newFont;
            DataRow[] __selectDoc = this._dataTable_doc.Select(__query);
            for (int __row = 0; __row < __selectDoc.Length; __row++)
            {
                string __docNo = __selectDoc[__row][1].ToString();
                SMLReport._report._objectListType __dataObject_doc = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(_objReport_doc, __dataObject_doc);
                _view1._addDataColumn(_objReport_doc, __dataObject_doc, 0, "", __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                _view1._addDataColumn(_objReport_doc, __dataObject_doc, 1, __selectDoc[__row][0].ToString(), __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);
                _view1._addDataColumn(_objReport_doc, __dataObject_doc, 2, __selectDoc[__row][1].ToString(), __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                for (int __loop = 0; __loop < this._col_name_doc.Count - 3; __loop++)
                {
                    _view1._addDataColumn(_objReport_doc, __dataObject_doc, __loop + 3, MyLib._myGlobal._formatNumberForReport((__loop == 0) ? _g.g._companyProfile._item_qty_decimal : _g.g._companyProfile._item_amount_decimal, this._convertNumber(__selectDoc[__row][__loop + 2].ToString())), __selectFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    if (sumTotal)
                    {
                        __total1[__loop] = (decimal)__total1[__loop] + MyLib._myGlobal._decimalPhase(__selectDoc[__row][__loop + 2].ToString());
                    }
                }
                if (getItem)
                {
                    _getDataObjectByIc(false, __docNo, "", false);
                }
            }
        }

        void _getDataObjectByCust(Boolean fontBold, string itemCode, Boolean printItem, Boolean printDoc, Boolean getItem, Boolean sumTotal)
        {
            string __query = "";
            if (itemCode.Length > 0)
            {
                __query = "item_code=\'" + itemCode + "\'";
            }
            DataRow[] __selectCust = this._dataTable_cust.Select(__query);
            for (int __row = 0; __row < __selectCust.Length; __row++)
            {
                string __custCode = __selectCust[__row][1].ToString();
                SMLReport._report._objectListType __dataObject_cust = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(_objReport_cust, __dataObject_cust);
                Font __selectFont = (fontBold) ? __newFont_bold : __newFont;
                _view1._addDataColumn(_objReport_cust, __dataObject_cust, 0, "", __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                _view1._addDataColumn(_objReport_cust, __dataObject_cust, 1, __selectCust[__row][1].ToString(), __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                _view1._addDataColumn(_objReport_cust, __dataObject_cust, 2, __selectCust[__row][2].ToString(), __selectFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                for (int __loop = 0; __loop < this._col_width_cust.Count - 3; __loop++)
                {
                    _view1._addDataColumn(_objReport_cust, __dataObject_cust, __loop + 3, MyLib._myGlobal._formatNumberForReport((__loop == 0) ? _g.g._companyProfile._item_qty_decimal : _g.g._companyProfile._item_amount_decimal, this._convertNumber(__selectCust[__row][__loop + 3].ToString())), __selectFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    if (sumTotal)
                    {
                        __total1[__loop] = (decimal)__total1[__loop] + MyLib._myGlobal._decimalPhase(__selectCust[__row][__loop + 3].ToString());
                    }
                }
                if (printDoc)
                {
                    this._getDataObjectByDoc(false, itemCode, __custCode, getItem, false);
                }
                if (printItem)
                {
                    _getDataObjectByIc(false, "", __custCode, false);
                }
            }
        }

        void _view1__getDataObject()
        {
            this.__newFont = new Font(_view1._fontStandard, FontStyle.Regular);
            this.__newFont_bold = new Font(_view1._fontStandard, FontStyle.Bold);
            try
            {
                this.__total1 = new ArrayList();
                for (int __loop = 0; __loop < 11; __loop++)
                {
                    __total1.Add((decimal)0M);
                }
                switch (this._mode)
                {
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า:
                        this._getDataObjectByCust(true, "", false, true, true, true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร:
                        this._getDataObjectByCust(true, "", false, true, false, true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร:
                        this._getDataObjectByCust(true, "", true, false, false, true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า:
                        this._getDataObjectByCust(true, "", true, false, false, true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                        this._getDataObjectByCust(false, "", false, false, false, true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                        this._getDataObjectByIc(false, "", "", true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า:
                        this._getDataObjectByIc(true, "", "", true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร:
                        this._getDataObjectByIc(true, "", "", true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                        this._getDataObjectByIc(true, "", "", true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร:
                        this._getDataObjectByDoc(false, "", "", false, true);
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า:
                        this._getDataObjectByDoc(true, "", "", true, true);
                        break;
                }
                Boolean __found = false;
                for (int __loop = 0; __loop < 10 && __found == false; __loop++)
                {
                    if ((decimal)__total1[__loop] != 0M)
                    {
                        __found = true;
                    }
                }
                if (__found)
                {
                    this._printTotal("รวม", __total1);
                }
            }
            catch (Exception)
            {
            }
        }

        private string[] _data_main()
        {
            string[] __result = { "",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._ic_code,
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._ic_name,
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._ic_unit_code,
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._qty_sale+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._amount_sale+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._cost_sale+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._qty_sale_return+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._amount_sale_return+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._cost_sale_return+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._amount_net+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._cost_net+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._profit_lost_amount+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._profit_lost_persent+"*"                                   
                                };
            return __result;
        }

        private string[] _data_sub_doc()
        {
            string[] __result = { "",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._doc_date,
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._doc_no,
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._qty_sale+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._amount_sale+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._cost_sale+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._qty_sale_return+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._amount_sale_return+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._cost_sale_return+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._amount_net+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._cost_net+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._profit_lost_amount+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._profit_lost_persent+"*"                                    
                                };
            return __result;
        }

        private string[] _data_sub_cust()
        {
            string[] __result = { "",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._ar_code,
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._ar_detail,
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._qty_sale+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._amount_sale+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._cost_sale+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._qty_sale_return+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._amount_sale_return+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._cost_sale_return+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._amount_net+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._cost_net+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._profit_lost_amount+"*",
                                    _g.d.ic_resource._table+"."+_g.d.ic_resource._profit_lost_persent+"*"
                                };
            return __result;
        }

        public void _config()
        {
            try
            {
                this._query = new StringBuilder();
                this._query_sub = new StringBuilder();
                this._dataTable_ic = new DataTable();
                this._dataTable_cust = new DataTable();
                this._report_name = _g.d.resource_report_ic_report_name._ic_info_stk_profit;//_apReportName(_apType);
                //001-008

                /*string __resourceCode = _g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_details;
                 this._report_name = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;
                 _view1.__excelFlieName = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;*/
                string[] __column = _data_main();
                string[] __column_doc = _data_sub_doc();
                string[] __column_cust = _data_sub_cust();
                string[] __width = { "0", "10", "20", "7", "9", "6", "6", "6", "6", "6", "6", "6", "6", "6" };
                string[] __width_doc = { "5", "12", "20", "9", "6", "6", "6", "6", "6", "6", "6", "6", "6" };
                string[] __width_cust = { "10", "10", "20", "6", "6", "6", "6", "6", "6", "6", "6", "6", "6" };
                switch (this._mode)
                {
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า:
                        __width_cust[0] = "0";
                        __width_cust[1] = "15";
                        __width_cust[2] = "25";
                        //
                        __width_doc[0] = "5";
                        __width_doc[1] = "12";
                        __width_doc[2] = "20";
                        __width_doc[3] = "9";
                        //
                        __width[0] = "10";
                        __width[1] = "10";
                        __width[2] = "12";
                        __width[3] = "5";
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร:
                        __width_cust[0] = "0";
                        __width_cust[1] = "15";
                        __width_cust[2] = "25";
                        //
                        __width_doc[0] = "10";
                        __width_doc[1] = "12";
                        __width_doc[2] = "15";
                        __width_doc[3] = "9";
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร:
                        __width_cust[0] = "0";
                        __width_cust[1] = "15";
                        __width_cust[2] = "25";
                        //
                        __width[0] = "5";
                        __width[1] = "10";
                        __width[2] = "15";
                        //
                        __width_doc[0] = "10";
                        __width_doc[1] = "12";
                        __width_doc[2] = "15";
                        __width_doc[3] = "9";
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า:
                        __width_cust[0] = "0";
                        __width_cust[1] = "15";
                        __width_cust[2] = "25";
                        //
                        __width[0] = "5";
                        __width[1] = "10";
                        __width[2] = "15";
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                        __width_cust[0] = "0";
                        __width_cust[1] = "15";
                        __width_cust[2] = "25";
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร:
                        __width_cust[0] = "5";
                        __width_cust[1] = "12";
                        __width_cust[2] = "20";
                        __width_cust[3] = "9";
                        //
                        __width_doc[0] = "10";
                        __width_doc[1] = "10";
                        __width_doc[2] = "20";
                        __width_doc[3] = "6";
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร:
                        __width_doc[0] = "0";
                        __width_doc[1] = "11";
                        __width_doc[2] = "26";
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า:
                        __width_doc[0] = "0";
                        __width_doc[1] = "11";
                        __width_doc[2] = "26";
                        //
                        __width[0] = "5";
                        __width[1] = "10";
                        __width[2] = "15";
                        break;
                }
                // string[] __type = { "1", "2", "2", "2", "4" };
                try
                {
                    this._col_width_1.Clear();
                    this._col_name_1.Clear();
                    this._col_width_doc.Clear();
                    this._col_name_doc.Clear();
                    this._col_width_cust.Clear();
                    this._col_name_cust.Clear();
                    for (int __loop = 0; __loop < __width.Length; __loop++)
                    {
                        this._col_width_1.Add(__width[__loop]);
                        this._col_name_1.Add(__column[__loop]);
                    }
                    for (int __loop = 0; __loop < __width_doc.Length; __loop++)
                    {
                        this._col_width_doc.Add(__width_doc[__loop]);
                        this._col_name_doc.Add(__column_doc[__loop]);
                    }
                    for (int __loop = 0; __loop < __width_cust.Length; __loop++)
                    {
                        this._col_width_cust.Add(__width_cust[__loop]);
                        this._col_name_cust.Add(__column_cust[__loop]);
                    }
                }
                catch
                {
                }

                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __reportGuid = Guid.NewGuid().ToString().ToLower();
                string __code_begin = "";
                string __code_end = "";
                string __date_begin = "";
                string __date_end = "";
                string __itemCodeList = "";
                string __itemCustList = "";
                if (this._check_submit)
                {
                    switch (this._mode)
                    {
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า:
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า:
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร:
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า:
                            __itemCodeList = this._condition._screen_grid_analysis1._createWhere("item_code");
                            break;
                        default:
                            __itemCodeList = this._condition._screen_grid_analysis1._createWhere("code");
                            break;
                    }
                    __itemCustList = this._condition._screen_grid_analysis2._createWhere("cust_code");
                    __code_begin = this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._from_item_code);
                    __code_end = this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._to_item_code);
                    __date_begin = this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._from_date);
                    __date_end = this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._to_date);
                }
                switch (this._mode)
                {
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า:
                        {
                            // ดึงตามลูกค้า
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_cust = __resultDataSet_2.Tables[0];
                            // ดึงตามเอกสาร
                            long __fileSize_4 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_4 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_4, "Report"));
                            this._dataTable_doc = __resultDataSet_4.Tables[0];
                            // ดึงตามสินค้า
                            long __fileSize_3 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_3 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_3, "Report"));
                            this._dataTable_ic = __resultDataSet_3.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร:
                        {
                            // ดึงตามลูกค้า
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_cust = __resultDataSet_2.Tables[0];
                            // ดึงตามเอกสาร
                            long __fileSize_4 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_4 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_4, "Report"));
                            this._dataTable_doc = __resultDataSet_4.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร:
                        {
                            // ดึงตามลูกค้า
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_cust = __resultDataSet_2.Tables[0];
                            // ดึงตามสินค้า
                            long __fileSize_3 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_3 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_3, "Report"));
                            this._dataTable_ic = __resultDataSet_3.Tables[0];
                            // ดึงตามเอกสาร
                            long __fileSize_4 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_4 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_4, "Report"));
                            this._dataTable_doc = __resultDataSet_4.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า:
                        {
                            // ดึงตามลูกค้า
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_cust = __resultDataSet_2.Tables[0];
                            // ดึงตามสินค้า
                            long __fileSize_3 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_3 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_3, "Report"));
                            this._dataTable_ic = __resultDataSet_3.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                        {
                            // ดึงตามลูกค้า
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_cust = __resultDataSet_2.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร:
                        {
                            // ดึงตามเอกสาร
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_doc = __resultDataSet_2.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า:
                        {
                            // ดึงตามเอกสาร
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_doc = __resultDataSet_2.Tables[0];
                            // ดึงตามสินค้า
                            long __fileSize = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize, "Report"));
                            this._dataTable_ic = __resultDataSet.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                        {
                            long __fileSize = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize, "Report"));
                            this._dataTable_ic = __resultDataSet.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                        {
                            // ดึงตามสินค้า
                            long __fileSize_1 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_1 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_1, "Report"));
                            this._dataTable_ic = __resultDataSet_1.Tables[0];
                            // ดึงตามเอกสาร
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_doc = __resultDataSet_2.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า:
                        {
                            // ดึงตามสินค้า
                            long __fileSize_1 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_1 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_1, "Report"));
                            this._dataTable_ic = __resultDataSet_1.Tables[0];
                            // ดึงตามลูกค้า
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_cust = __resultDataSet_2.Tables[0];
                        }
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร:
                        {
                            // ดึงตามสินค้า
                            long __fileSize_1 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_1 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_1, "Report"));
                            this._dataTable_ic = __resultDataSet_1.Tables[0];
                            // ดึงตามลูกค้า
                            long __fileSize_2 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_2 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_2, "Report"));
                            this._dataTable_cust = __resultDataSet_2.Tables[0];
                            // ดึงตามเอกสาร
                            long __fileSize_3 = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(SMLERPICInfo._global._infoStkProfitNumber(SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร), MyLib._myGlobal._databaseName, __code_begin, __code_end, "", "", __date_begin, __date_end, __itemCodeList, __itemCustList, __reportGuid));
                            DataSet __resultDataSet_3 = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize_3, "Report"));
                            this._dataTable_doc = __resultDataSet_3.Tables[0];
                        }
                        break;
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _getObjectIc(Boolean lineTop, Boolean lineBotoom)
        {
            if (lineTop) _objReport_ic = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
            if (lineBotoom) _objReport_ic = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
            if (lineTop == false && lineBotoom == false) _objReport_ic = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
            for (int i = 0; i < this._col_width_1.Count; i++)
            {
                //------------ADD Column----------------
                _view1._addColumn(_objReport_ic, MyLib._myGlobal._intPhase(this._col_width_1[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._col_name_1[i].ToString(), "", SMLReport._report._cellAlign.Left);
            }
        }

        void _getObjectDoc(Boolean lineTop, Boolean lineBotoom)
        {
            if (lineTop) _objReport_doc = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
            if (lineBotoom) _objReport_doc = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
            if (lineTop == false && lineBotoom == false) _objReport_doc = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
            for (int __i = 0; __i < this._col_width_doc.Count; __i++)
            {
                _view1._addColumn(_objReport_doc, MyLib._myGlobal._intPhase(this._col_width_doc[__i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._col_name_doc[__i].ToString(), "", SMLReport._report._cellAlign.Left);
            }
        }

        void _getObjectCust(Boolean lineTop, Boolean lineBotoom)
        {
            if (lineTop) _objReport_cust = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
            if (lineBotoom) _objReport_cust = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
            if (lineTop == false && lineBotoom == false) _objReport_cust = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
            for (int __i = 0; __i < this._col_width_cust.Count; __i++)
            {
                _view1._addColumn(_objReport_cust, MyLib._myGlobal._intPhase(this._col_width_cust[__i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._col_name_cust[__i].ToString(), "", SMLReport._report._cellAlign.Left);
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                //
                string __beginDate = this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._to_date);
                string __conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                __conditionText = _g.g._conditionGrid(this._condition._screen_grid_analysis1, __conditionText);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, __conditionText, SMLReport._report._cellAlign.Center, this._view1._fontHeader2);
//
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Title    : " + this._screenName, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "Description : ", SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                //_view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.Text, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontHeader2);          
                // _view1._excelFileName = "รายงานยอดการชำระเงิน";//
                // _view1._maxColumn = 9;
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    switch (this._mode)
                    {
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า:
                            this._getObjectCust(true, false);
                            this._getObjectDoc(false, false);
                            this._getObjectIc(false, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร:
                            this._getObjectCust(true, false);
                            this._getObjectDoc(false, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร:
                            this._getObjectCust(true, false);
                            this._getObjectIc(false, false);
                            this._getObjectDoc(false, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า:
                            this._getObjectCust(true, false);
                            this._getObjectIc(false, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                            this._getObjectCust(true, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                            this._getObjectIc(true, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร:
                            this._getObjectDoc(true, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                            this._getObjectIc(true, false);
                            this._getObjectDoc(false, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า:
                            this._getObjectIc(true, false);
                            this._getObjectCust(false, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร:
                            this._getObjectIc(true, false);
                            this._getObjectCust(false, false);
                            this._getObjectDoc(false, true);
                            break;
                        case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า:
                            this._getObjectDoc(true, false);
                            this._getObjectIc(false, true);
                            break;
                    }
                    return true;
                }
            return false;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            /* if (__check_submit.Equals("OK"))
             {*/
            this._col_width_1.Clear();
            this._col_width_doc.Clear();
            this._col_name_1.Clear();
            this._col_name_doc.Clear();
            this._config();
            //_view1__loadDataByThread();
            _view1._buildReport(SMLReport._report._reportType.Standard);
            /* }
             else
             {
                 MessageBox.Show("ยังไม่ได้เลือกเงื่อนไข");
             }*/
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._showCondition(this._report_name);
        }

        void _showCondition(string screenName)
        {
            this._dataTable_ic = null;
            //string __page = _apType.ToString();
            if (this._condition == null)
            {
                // jead ทำแบบนี้เพราะ เมื่อกดเงื่อนไขซ้ำ ที่เลือกไปแล้ว จะได้เหมือนเดิม
                this._condition = new _analysis_condition(screenName);
                this._condition._whereControl._tableName = _g.d.ap_supplier._table;
                this._condition._whereControl._addFieldComboBox(this._data_main());
                this._condition.Size = new Size(500, 500);
                string __tabItemName = MyLib._myGlobal._resource("เลือกช่วงสินค้า");
                string __tabCustName = MyLib._myGlobal._resource("เลือกช่วงลูกค้า");
                switch (this._mode)
                {
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า:
                        this._condition._tabControl.TabPages.RemoveAt(1);
                        this._condition._tabControl.TabPages[0].Text = __tabItemName;
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า:
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร:
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า:
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร:
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า:
                        this._condition._tabControl.TabPages[0].Text = __tabItemName;
                        this._condition._tabControl.TabPages[1].Text = __tabCustName;
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร:
                        this._condition._grouper1.Dispose();
                        this._condition._grouper2.Dock = DockStyle.Fill;
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร:
                        this._condition._tabControl.TabPages[1].Text = __tabCustName;
                        this._condition._tabControl.TabPages.RemoveAt(0);
                        break;
                }
            }

            this._condition.ShowDialog();
            if (this._condition.__check_submit)
            {
                this._data_condition = this._condition.__where;
                this._check_submit = this._condition.__check_submit;
                this._config();
                //this.__data_ap = this._con_cash.__grid_where;
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }

        double _convertNumber(string dataSult)
        {
            double __result = 0.00;
            Double __amount = 0.00;
            try
            {
                __amount = Double.Parse(dataSult);
                if (__amount > 0)
                {
                    __result = __amount;
                }
                else if (__amount < 0)
                {
                    __result = __amount;
                }
                else
                {
                    __result = 0;
                }
            }
            catch
            {
            }
            return __result;
        }

        string _checkNumber(string datasult)
        {
            string __result = "";
            Double __Amount = 0.00;


            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            try
            {
                __Amount = Double.Parse(datasult);
                if (__Amount > 0)
                {
                    __result = string.Format(__formatNumber, __Amount);
                }
                else
                {
                    __result = "";
                }
            }
            catch
            {
            }
            return __result;
        }

    }
}
