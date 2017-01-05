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
    public partial class _report_credit_note_cancel : UserControl
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
        string _check_detail = "yes";
        //---------------------------------------------------------------------------------------------------

        private string _report_name = "";
        private int _level = 0;
        private DataTable __data_po;
        //-------------------Condition------------------------
        string _data_condition = "";
        Boolean __check_submit = false;
        //----------------------------------------------------
        StringBuilder __query;
        StringBuilder __query_sub;
        _condition_po _con_po;


        public _report_credit_note_cancel()
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
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');

                        __from_ap = __data_split[0];
                        __to_ap = __data_split[1];
                        __to_date = __data_split[2];
                    }
                    this.__query.Append("select item_code,item_name,unit_code,remark,qty,total_qty,");
                    this.__query.Append("(select ic_trans.doc_no from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no  and trans_type = 1 and trans_flag in (2,4 ) ) as doc_no,");
                    this.__query.Append("(select ic_trans.doc_date from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no   and trans_type = 1 and trans_flag in (2,4 )  ) as doc_date,");
                    this.__query.Append("(select ic_trans.department_code from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no   and trans_type = 1  and trans_flag in (2,4 ) ) as department_code,");
                    this.__query.Append("(select ic_trans.user_request from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no   and trans_type = 1  and trans_flag in (2,4 ) ) as user_request,");
                    this.__query.Append("(select ic_trans.remark from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no   and trans_type = 1  and trans_flag in (2,4 ) ) as remark_head");
                    this.__query.Append(" from ic_trans_detail ");
                    this.__query.Append(" where trans_flag in (2,4)  and trans_type = 1 ");
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
                    this._dataTable = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString()).Tables[0];
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

        void _view1__getDataObject()
        {
            try
            {
                DataRow[] __dr = this._dataTable.Select();
                //DataRow[] __dr_sub = this._dataTable_sub.Select();
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)_objReport._columnList[0];
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
                Font __newFont_bold = new Font(__getColumn._fontData, FontStyle.Bold);
                int _no = 0;

                // double _sum = 0;
                string a = "";

                for (int i = 0; i < this._dataTable.Columns.Count; i++)
                {
                    a += _dataTable.Columns[i].ToString() + ",";
                }
                string[] __column = a.Split(',');

                for (int _i = 0; _i < __dr.Length; _i++)
                {

                    SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(_objReport, __dataObject);
                    string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[_i]["doc_date"].ToString()).ToShortDateString();
                    string __doc_ref_date = MyLib._myGlobal._convertDateFromQuery(__dr[_i]["doc_ref_date"].ToString()).ToShortDateString();
                    _view1._addDataColumn(_objReport, __dataObject, 0, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 2, __doc_ref_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i]["doc_ref"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i]["cust_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 5, __dr[_i]["ar_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                    // _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i][_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._amount].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                    /*double __debtBalance = Double.Parse(__dr[_i][_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._amount].ToString());
                    _view1._addDataColumn(_objReport, __dataObject, 4, (__debtBalance == 0) ? "" : string.Format(_formatNumber, __debtBalance), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                    __total += __debtBalance;*/
                    _no++;


                }
                if (_no == __dr.Length)
                {

                    SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    _view1._createEmtryColumn(_objReport, __dataObject);
                    _view1._addDataColumn(_objReport, __dataObject, 0, "รวมรายการทั้งสิ้น  : " + string.Format(_formatCountNumber, _no) + " รายการ", __newFont_bold, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont_bold, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont_bold, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
                                    "วันที่ใบลดหนี้",
                                    "เลขที่ใบลดหนี้",
                                    "รหัสเจ้าหนี้",
                                    "ชื่อเจ้าหนี้",
                                    _g.d.ic_trans._table+"."+_g.d.ic_trans._remark,
                                   
                                };
            return __result;

        }


        public void _po_config()
        {
            this.__query = new StringBuilder();

            this._dataTable = new DataTable();

            this._report_name = "รายงานยกเลิกการส่งคืนสินค้า/ลดหนี้";//_apReportName(_apType);
            //001-008

            /*string __resourceCode = _g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_details;
            this._report_name = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;
            _view1.__excelFlieName = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;*/
            string[] __width = { "15", "15", "15", "10", "10", "20", "15" };
            string[] __column = _data_main();


            this._setColumnName(__width, __column, null, null);
            this._level = 1;


            string __endDate = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._workingDate);
            string __startDate = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._workingDate);
            string __fromAr = "";
            string __toAr = "";
            string __fromDoc = "";
            string __toDoc = "";
            string _data_query = "";
            this.__query.Append("select doc_no,doc_date,doc_ref,doc_ref_date,remark,cust_code,( select name_1 from ap_supplier where ap_supplier.code = cust_code) as ar_name  from ic_trans  where trans_flag = 17 ");
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
                _data_query = this._con_po._whereUserControl1._getWhere2().Replace("where", " and ").Replace("(", "").Replace(")", " ");
            }


            this.__query.Append(" " + _data_query + " order by doc_no");


            this._dataTable = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString()).Tables[0];

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
                this._con_po = new _condition_po(0, "  trans_flag = 17 ");
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
