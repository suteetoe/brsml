using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPPOReport._report
{
    public partial class _report_offer_buy : UserControl
    {
      
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        
        //_conditonReceipt _conditonReceiptScreen = new _conditonReceipt();
        SMLReport._report._objectListType _objReport;
        SMLReport._report._objectListType _objReport2;
        //DataSet _ds;
        DataTable _dataTable;
        DataTable _dataTable_sub;
        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");

        private ArrayList _col_name_1 = new ArrayList();
        private ArrayList _col_name_2 = new ArrayList();
        private ArrayList _col_width_1 = new ArrayList();
        private ArrayList _col_width_2 = new ArrayList();
        private ArrayList _col_type_1 = new ArrayList(); // 1 = Date ,  2 = string , 3 = int  , 4 = double
        private ArrayList _col_type_2 = new ArrayList(); // 1 = Date ,  2 = string , 3 = int  , 4 = double
        //---------------------------------------------------------------------------------------------------
        string[] __width;
        string[] __column;
        string[] __width_2;
        string[] __column_2;
        //---------------------------------------------------------------------------------------------------

        private string _report_name = "";
        private int _level = 0;
        private DataTable __data_po;
        private DataTable __data_po_sub;
        //-------------------Condition------------------------
        string _data_condition = "";
        Boolean __check_submit = false;
        //----------------------------------------------------
        StringBuilder __query;
        StringBuilder __query_sub;
        _condition_po _con_po;


        public _report_offer_buy()
        {
            InitializeComponent();
            _view1._pageSetupDialog.PageSettings.Landscape = true;
            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            _view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            
        }

        void _view1__loadDataByThread()
        {
            // this._dataTable == null ไม่ต้อง load ซ้ำ
            if (this._dataTable == null)
            {
               
                    string __from_ap = "";
                    string __to_ap = "";
                    string __to_date = "";
                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                   
                    try
                    {
                        /*if (this.__check_submit.Equals("OK"))
                        {
                            string[] __data_split = this._data_condition.Replace("\'", "").Split(',');

                            __from_ap = __data_split[0];
                            __to_ap = __data_split[1];
                            __to_date = __data_split[2];
                        }*/
                        string[] __data = this._con_po.__where.Replace("\'", "").Split(','); // (_g.d.resource_report._balance_date).Replace("'", "");
                        this.__query.Append("select doc_no,doc_date,due_date");
                        this.__query.Append(",( select code from ar_customer where ar_customer.code = cust_code) as ar_code");
                        this.__query.Append(",( select name_1 from ar_customer where ar_customer.code = cust_code) as ar_name");
                        this.__query.Append(",sum(coalesce(total_vat_value,0)) as total_vat_value");
                        this.__query.Append(",sum(coalesce(total_after_vat,0)) as total_after_vat");
                        this.__query.Append(",sum(coalesce(total_except_vat,0)) as total_except_vat");
                        this.__query.Append(",sum(coalesce(total_amount,0)) as total_amount");
                        this.__query.Append(" from ic_trans ");
                        this.__query.Append(" group by ");
                        this.__query.Append(" doc_no,doc_date,due_date");
                        this.__query.Append(",( select code from ar_customer where ar_customer.code = cust_code) ");
                        this.__query.Append(",( select name_1 from ar_customer where ar_customer.code = cust_code) ");

                        this.__query_sub.Append("select doc_no,doc_date,item_code,item_name,unit_code,coalesce(price,0) as price,qty,discount");
                        this.__query_sub.Append(" from ic_trans_detail ");
                        this._dataTable = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString()).Tables[0];
                        this._dataTable_sub = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_sub.ToString()).Tables[0];
                       /* StringBuilder __where_data = new StringBuilder();
                        if (this.__data_po != null)
                        {
                            for (int __row = 0; __row < this.__data_po.Rows.Count; __row++)
                            {
                                __where_data.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.ar_customer._code, this.__data_po.Rows[__row][0].ToString(), this.__data_po.Rows[__row][1].ToString()));
                                if (__row != this.__data_po.Rows.Count - 1)
                                {
                                    __where_data.Append(" or ");
                                }
                            }
                        }
                        else
                        {
                            __where_data.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.ar_customer._code, __from_ap, __to_ap));
                        }*/
                        //this._dataTable = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString()).Tables[0];
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
            this.Parent.Dispose();
        }

        void _view1__getDataObject()
        {
            try
            {
                DataRow[] __dr = this._dataTable.Select();
                DataRow[] __dr_sub = this._dataTable_sub.Select();
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)_objReport._columnList[0];
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
                Font __newFont_bold = new Font(__getColumn._fontData, FontStyle.Bold);
                int _no = 0;
                int _no_sub = 0;
                // double _sum = 0;
                string a = "";

                for (int i = 0; i < this._dataTable.Columns.Count; i++)
                {
                    a += _dataTable.Columns[i].ToString() + ",";
                }
                string[] __column = a.Split(',');
                int _j = 0;
                double __total = 0;

                for (int _i = 0; _i < __dr.Length; _i++)
                {

                    SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(_objReport, __dataObject);
                    string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[_i]["doc_date"].ToString()).ToShortDateString();
                    _view1._addDataColumn(_objReport, __dataObject, 0, __doc_date, __newFont_bold, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i]["doc_no"].ToString(), __newFont_bold, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i]["ar_code"].ToString(), __newFont_bold, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i]["ar_name"].ToString(), __newFont_bold, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 4, (this._convertNumber(__dr[_i]["total_value"].ToString()) == 0) ? "" : string.Format(_formatNumber, this._convertNumber(__dr[_i]["total_value"].ToString())), __newFont_bold, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    _view1._addDataColumn(_objReport, __dataObject, 5, (this._convertNumber(__dr[_i]["total_discount"].ToString()) == 0) ? "" : string.Format(_formatNumber, this._convertNumber(__dr[_i]["total_discount"].ToString())), __newFont_bold, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    _view1._addDataColumn(_objReport, __dataObject, 6, (this._convertNumber(__dr[_i]["total_after_discount"].ToString()) == 0) ? "" : string.Format(_formatNumber, this._convertNumber(__dr[_i]["total_after_discount"].ToString())), __newFont_bold, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    _view1._addDataColumn(_objReport, __dataObject, 7, (this._convertNumber(__dr[_i]["total_vat_value"].ToString()) == 0) ? "" : string.Format(_formatNumber, this._convertNumber(__dr[_i]["total_vat_value"].ToString())), __newFont_bold, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    _view1._addDataColumn(_objReport, __dataObject, 8, (this._convertNumber(__dr[_i]["total_amount"].ToString()) == 0) ? "" : string.Format(_formatNumber, this._convertNumber(__dr[_i]["total_amount"].ToString())), __newFont_bold, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                    _no++;
                    __total += this._convertNumber(__dr[_i]["total_amount"].ToString());
                    //__dr_sub. = this._dataTable_sub.Select(__dr[_i]["doc_no"].ToString());
                    for (_j = 0; _j < __dr_sub.Length; _j++)
                    {
                        if (__dr_sub[_j]["doc_no"].ToString().Equals(__dr[_i]["doc_no"].ToString()) && __dr_sub[_j]["doc_date"].ToString().Equals(__dr[_i]["doc_date"].ToString()))
                        {
                            SMLReport._report._objectListType __dataObject2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(_objReport2, __dataObject2);
                            _view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 1, __dr_sub[_j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 2, __dr_sub[_j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 3, __dr_sub[_j]["unit_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 4, (this._convertNumber(__dr_sub[_j]["price"].ToString()) == 0) ? "" : string.Format(_formatNumber, this._convertNumber(__dr_sub[_j]["price"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(_objReport2, __dataObject2, 5, string.Format(_formatNumber, this._convertNumber(__dr_sub[_j]["qty"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, this._convertNumber(__dr_sub[_j]["sum_all"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(_objReport2, __dataObject2, 7, __dr_sub[_j]["discount"].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(_objReport2, __dataObject2, 8, (this._convertNumber(__dr_sub[_j]["sum_amount"].ToString()) == 0) ? "" : string.Format(_formatNumber, this._convertNumber(__dr_sub[_j]["sum_amount"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(_objReport2, __dataObject2, 9, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 10, (this._convertNumber(__dr_sub[_j]["sum_amount"].ToString()) == 0) ? "" : string.Format(_formatNumber, this._convertNumber(__dr_sub[_j]["sum_amount"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            _no_sub++;

                        }

                    }
                    //_view1._reportProgressBar.Value = loopprogressbar;
                }
                if (_no == __dr.Length)
                {
                    SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(_objReport, __dataObject);
                    _view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 1, "รวมรายการทั้งสิ้น  : " + string.Format(_formatCountNumber, _no) + " รายการ", __newFont_bold, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 8, string.Format(_formatNumber, __total), __newFont_bold, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                }
            }
            catch (Exception)
            {
            }
        }

        private string[] _data_main()
        {
            //string[] __column = { "วันที่", "เลขที่เอกสาร", "สมุดเงินฝาก", "หมายเหตุ/รายละเอียด", "จำนวนเงิน" };
            string[] __result = { _g.d.ic_trans._table+"."+_g.d.ic_trans._doc_date,
                                    _g.d.ic_trans._table+"."+_g.d.ic_trans._doc_no,
                                    "รหัสลูกค้า",//_g.d.ar_customer._table+"."+_g.d.ar_customer._code,
                                    "ชื่อลูกค้า",//_g.d.ar_customer._table+"."+_g.d.ar_customer._name_1,
                                    _g.d.ic_trans._table+"."+_g.d.ic_trans._total_value+"*",
                                    _g.d.ic_trans._table+"."+_g.d.ic_trans._total_discount+"*",
                                    "มูลค่าหลังหักส่วนลด"+"*",
                                    _g.d.ic_trans._table+"."+_g.d.ic_trans._total_vat_value+"*",
                                    _g.d.ic_trans._table+"."+_g.d.ic_trans._total_amount+"*"
                                };
            return __result;
        }
        private string[] _data_sub()
        {

            //string[] __column = { "วันที่", "เลขที่เอกสาร", "สมุดเงินฝาก", "หมายเหตุ/รายละเอียด", "จำนวนเงิน" };
            string[] __result = {"", _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_code,
                                    _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_name,
                                    _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._unit_code,
                                    _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._price+"*",
                                    _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._qty+"*",
                                    "มูลค่าสินค้า"+"*",
                                    _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._discount+"*",
                                    _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._sum_amount+"*",
                                    "มูลค่ายกเว้นภาษี"+"*",
                                    _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._sum_amount+"*",
                                };
 

            return __result;
        }

        public void _po_config()
        {
            this.__query = new StringBuilder();
            this.__query_sub = new StringBuilder();
            this._dataTable = new DataTable();
            this._dataTable_sub = new DataTable();
            this._report_name = "รายงานบันทึกใบขอซื้อ";//_apReportName(_apType);
            //001-008
          
               /* string __resourceCode = _g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_details;
                this._report_name = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;
                _view1.__excelFlieName = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;*/
                string[] __width = { "11", "16", "9", "18", "9", "9", "9", "9", "10" };
                string[] __column = _data_main();
                string[] __width_2 = { "2", "9", "16", "9", "9", "9", "9", "9", "9", "9", "10" };
                string[] __column_2 = _data_sub();
                
               // string[] __type = { "1", "2", "2", "2", "4" };
                this._setColumnName(__width, __column, __width_2, __column_2);
                this._level = 2;

                string __endDate = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._workingDate);
                string __startDate = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._workingDate);
                string __fromAr = "";
                string __toAr = "";
                string __fromDoc = "";
                string __toDoc = "";

                this.__query.Append("select doc_no,to_char(doc_date,'dd/mm/yyyy' ) as doc_date");
                this.__query.Append(",( select code from ap_supplier where ap_supplier.code = cust_code) as ar_code");
                this.__query.Append(",( select name_1 from ap_supplier where ap_supplier.code = cust_code) as ar_name");

                this.__query.Append(",sum(coalesce(total_value,0)) as total_value");
                this.__query.Append(",sum(coalesce(total_discount,0)) as total_discount");
                this.__query.Append(",(sum(coalesce(total_value,0))-sum(coalesce(total_discount,0))) as total_after_discount");
                this.__query.Append(",sum(coalesce(total_vat_value,0)) as total_vat_value");
                this.__query.Append(",sum(coalesce(total_amount,0)) as total_amount");
                this.__query.Append(" from ic_trans   where trans_type = 1 and trans_flag = 2 ");
                if (this.__check_submit)
                {
                    
                    __startDate = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._from_date).Replace("'", "");
                    __endDate = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._to_date).Replace("'", "");
                    __fromAr = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._from_payable).Replace("'", "");
                    __toAr = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._to_payable).Replace("'", "");
                    __fromDoc = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._from_docno).Replace("'", "");
                    __toDoc = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._to_docno).Replace("'", "");
                    string __getWhere = this._con_po._screen_grid_po1._createWhere(_g.d.ar_customer._table + "." + _g.d.ar_customer._code);
                    if (!__startDate.Equals("null") && !__endDate.Equals("null"))
                    {
                        this.__query.Append(" and doc_date between '" + __startDate + "' and '" + __endDate + "'");
                    }
                    if (!__fromDoc.Equals("null") && !__toDoc.Equals("null"))
                    {
                        this.__query.Append(" and doc_no between '" + __fromDoc + "' and '" + __toDoc + "'");
                    }
                    if (!__fromAr.Equals("null") && !__toAr.Equals("null"))
                    {
                        this.__query.Append(" and cust_code between '" + __fromAr + "' and '" + __toAr + "'");
                    }
                    
                }


                this.__query.Append(" group by ");
                this.__query.Append(" doc_no,doc_date");
                this.__query.Append(",( select code from ap_supplier where ap_supplier.code = cust_code) ");
                this.__query.Append(",( select name_1 from ap_supplier where ap_supplier.code = cust_code) ");
                this.__query.Append(" order by ");
                this.__query.Append(" doc_date,doc_no");
                //------------------------------------------------------------------------------------------------------------------------
                this.__query_sub.Append("select doc_no,to_char(doc_date,'dd/mm/yyyy' ) as doc_date,item_code,item_name,coalesce(price,0) as price,qty,sum_amount,discount,(price*qty) as sum_all");
                this.__query_sub.Append(",unit_code||'~'||(select name_1 from ic_unit where code = unit_code) as unit_code ");
                this.__query_sub.Append(" from ic_trans_detail   where trans_type = 1 and trans_flag = 2 ");
                if (this.__check_submit)
                {

                    __startDate = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._from_date).Replace("'", "");
                    __endDate = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._to_date).Replace("'", "");
                    __fromDoc = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._from_docno).Replace("'", "");
                    __toDoc = this._con_po._condition_po_search1._getDataStrQuery(_g.d.resource_report._to_docno).Replace("'", "");
                    if (!__startDate.Equals("null") && !__endDate.Equals("null"))
                    {
                        this.__query_sub.Append(" and doc_date between '" + __startDate + "' and '" + __endDate + "'");
                    }
                    if (!__fromDoc.Equals("null") && !__toDoc.Equals("null"))
                    {
                        this.__query_sub.Append(" and doc_no between '" + __fromDoc + "' and '" + __toDoc + "'");
                    }

                }
              
                
                this._dataTable = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString()).Tables[0];
                this._dataTable_sub = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_sub.ToString()).Tables[0];
        }
        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {

            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Title    : " + this._report_name, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print Date: " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
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
                    _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    if (this._level == 1)
                    {
                        _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    }
                    for (int i = 0; i < this._col_width_1.Count; i++)
                    {
                        //------------ADD Column----------------
                        _view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._col_width_1[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._col_name_1[i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    //--------------Level 2---------------
                    if (this._level == 2 || this._level == 3 || this._level == 4)
                    {
                        if (this._level == 3 || this._level == 4)
                        {
                            _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        }
                        else
                        {
                            _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                        }

                        for (int i = 0; i < this._col_width_2.Count; i++)
                        {
                            //------------ADD Column----------------
                            _view1._addColumn(_objReport2, MyLib._myGlobal._intPhase(this._col_width_2[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._col_name_2[i].ToString(), "", SMLReport._report._cellAlign.Left);
                        }
                    }
                    //--------------Level 3---------------
                    /* if (this._cb_level == 3 || this._cb_level == 4)
                     {
                         if (this._cb_level == 4)
                         {
                             _objReport3 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                         }
                         else
                         {
                             _objReport3 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                         }

                         for (int i = 0; i < this._cb_width_3.Count; i++)
                         {
                             //------------ADD Column----------------
                             _view1._addColumn(_objReport3, MyLib._myGlobal._intPhase(this._cb_width_3[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._cb_column_3[i].ToString(), "", SMLReport._report._cellAlign.Left);
                         }
                     }
                     //--------------Level 4---------------
                     if (this._cb_level == 4)
                     {
                         _objReport4 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);

                         for (int i = 0; i < this._cb_width_4.Count; i++)
                         {
                             //------------ADD Column----------------
                             _view1._addColumn(_objReport4, MyLib._myGlobal._intPhase(this._cb_width_4[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._cb_column_4[i].ToString(), "", SMLReport._report._cellAlign.Left);
                         }
                     }*/
                    return true;
                }


            return false;
        }

        bool _view1__loadData()
        {

            if (this._dataTable == null)
            {
                try
                {
                    _dataTable = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString()).Tables[0];
                }
                catch
                {
                    this.Cursor = Cursors.Default;
                    //_view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
                    return false;
                }
                //_view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
            }
            //this.Cursor = Cursors.Default;
            return true;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            /* if (__check_submit.Equals("OK"))
             {*/
            this._col_width_1.Clear();
            this._col_width_2.Clear();
            this._col_name_1.Clear();
            this._col_name_2.Clear();
            this._po_config();
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
            this._showCondition();
        }



        void _showCondition()
        {
            this._dataTable = null;
            //string __page = _apType.ToString();
            if (this._con_po == null)
            {
                // jead ทำแบบนี้เพราะ เมื่อกดเงื่อนไขซ้ำ ที่เลือกไปแล้ว จะได้เหมือนเดิม
                this._con_po = new _condition_po(1, " trans_type = 1 and trans_flag = 2 ");
                this._con_po._whereUserControl1._tableName = _g.d.ap_supplier._table;
                this._con_po._whereUserControl1._addFieldComboBox(this._data_main());
                this._con_po.Size = new Size(500, 500);
            }

            this._con_po.ShowDialog();
            if (this._con_po.__check_submit)
            {
                this._data_condition = this._con_po.__where;
                this.__check_submit = this._con_po.__check_submit;
                this._po_config();
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


        void _setColumnName(string[] __width, string[] __column, string[] __width_2, string[] __column_2)
        {
            try
            {
                this._col_width_1.Clear();
                this._col_name_1.Clear();
                this._col_width_2.Clear();
                this._col_name_2.Clear();
                for (int i = 0; i < __width.Length; i++)
                {
                    this._col_width_1.Add(__width[i]);
                    this._col_name_1.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._col_width_2.Add(__width_2[i]);
                    this._col_name_2.Add(__column_2[i]);
                }
            }
            catch
            {
            }
        }

    }
}
