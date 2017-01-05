using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPReport.Cheque_Card
{
    public partial class _report_Cheque_Card : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        //_conditonReceipt _conditonReceiptScreen = new _conditonReceipt();
        SMLReport._report._objectListType _objReport;
        SMLReport._report._objectListType _objReport2;
        DataSet _ds;
        string _report_name = "";
        ArrayList _width = new ArrayList();
        ArrayList _column = new ArrayList();
        ArrayList _width_2 = new ArrayList();
        ArrayList _column_2 = new ArrayList();
        DataTable _dt;
        int _level = 0;
        string _format_number = MyLib._myGlobal._getFormatNumber("m00");
        private _cheque_cardEnumration _cheque_card_Enum;
        public _cheque_cardEnumration _cheque_card_Type
        {
            set
            {
                this._cheque_card_Enum = value;
                this.Invalidate();
            }
            get
            {
                return this._cheque_card_Enum;
            }

        }

        public _report_Cheque_Card()
        {
            InitializeComponent();
            this._view1._buttonExample.Enabled = false;
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            //this._view1._loadData +=new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);

        }

        void _view1__loadDataByThread()
        {
            SMLProcess._cbProcess __myProcess;
            int __type = 0;
            //if (_ds == null)
            // {
            string __start_date = "";
            string __end_date = "";
            string __where = "";
            string __order_by = "";

            try
            {
                __myProcess = new SMLProcess._cbProcess();
                int __test = _cheque_card_Global._int_cheque_card(_cheque_card_Type);
                switch (_cheque_card_Type)
                {

                    case _cheque_cardEnumration.EndDate_Card:  //  รายงานรายละเอียดบัตรเครดิต-ครบกำหนด
                        //_ds = __myProcess._processCBStatement(MyLib._myGlobal._databaseName, __start_date, __end_date, __where, __order_by, _cheque_card_Global._int_cheque_card(_cheque_card_Type));
                        break;
                    case _cheque_cardEnumration.EndDate_Pay_Cheque:  //  รายงานรายละเอียดเช็คจ่าย-วันครบกำหนด
                        //_ds = __myProcess._processCBStatement(MyLib._myGlobal._databaseName, __start_date, __end_date, __where, __order_by, _cheque_card_Global._int_cheque_card(_cheque_card_Type));
                        break;
                    case _cheque_cardEnumration.Cash_money_detail:  //  รายงานรายวันขี้นเงินพร้อมรายการย่อย
                        //_ds = __myProcess._processCBStatement(MyLib._myGlobal._databaseName, __start_date, __end_date, __where, __order_by, _cheque_card_Global._int_cheque_card(_cheque_card_Type));
                        break;
                    case _cheque_cardEnumration.Cancel_cheque_detail:  //  รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย
                        break;
                    case _cheque_cardEnumration.Cancel_Card_detail:  //  รายงานการยกเลิกบัตรพร้อมรายการย่อย
                        break;
                    case _cheque_cardEnumration.Disposit_Cheque_detail:  //  รายงานใบนำฝากเช็ครับพร้อมรายการย่อย 
                        break;
                    case _cheque_cardEnumration.Detail_Cheque_by_Date_import:  //  รายงานรายละเอียดเช็ครับ-ตามวันที่รับ
                        break;

                }
                _dt = _ds.Tables[0];
            }
            catch (Exception __e)
            {
                string __error = __e.Message;
                _view1._loadDataByThreadSuccess = false;
            }
            // }
            this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
            //this.Cursor = Cursors.Default;
            _view1._loadDataByThreadSuccess = true;
        }

        private void _config()
        {
            if (_cheque_card_Type == _cheque_cardEnumration.EndDate_Card)
            {
                // รายงานรายละเอียดบัตรเครดิต-ครบกำหนด
                this._report_name = "รายงานรายละเอียดบัตรเครดิต-ครบกำหนด";
                int[] __width = { 10, 10, 10, 15, 10, 20, 10, 10, 20 };
                string[] __column = { "วันที่ครบกำหนด", "เลขที่เอกสาร", "วันที่รับบัตรเครดิต", "ชื่อลูกหนี้", "เลขที่บัตรเครดิต", "ธนาคาร/สาขา", "สถานะ", "จำนวนเงิน", "หมายเหตุคำอธิบาย" };
                this._level = 1;
                for (int __i = 0; __i < __width.Length; __i++)
                {

                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                //this._query = new StringBuilder();


                //this._query.Append(" select " + _g.d.cb_credit_card._credit_due_date + " , " + _g.d.cb_credit_card._doc_no + " , " + _g.d.cb_credit_card._credit_get_date);
                //this._query.Append(" , (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + " = " + _g.d.cb_credit_card._table + "." + _g.d.cb_credit_card._ar_code + " ) as ar_name ");
                //this._query.Append(" , " + _g.d.cb_credit_card._credit_card_no);
                //this._query.Append(" ,(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._code + " = (select " + _g.d.erp_pass_book._bank_code + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + " = " + _g.d.cb_credit_card._table + "." + _g.d.cb_credit_card._pass_book_code + ")) || ' / ' || (select " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + " = " + _g.d.cb_credit_card._table + "." + _g.d.cb_credit_card._pass_book_code + " ) as bank_name");
                //this._query.Append(" , " + _g.d.cb_credit_card._status + "," + _g.d.cb_credit_card._amount + "," + _g.d.cb_credit_card._remark);
                //this._query.Append(" from " + _g.d.cb_credit_card._table);


            }
            else if (_cheque_card_Type == _cheque_cardEnumration.EndDate_Pay_Cheque)
            {
                // รายงานรายละเอียดเช็คจ่าย-วันครบกำหนด
                this._report_name = "รายงานรายละเอียดเช็คจ่าย-วันครบกำหนด";
                int[] __width = { 10, 10, 10, 15, 10, 20, 10, 10, 15 };
                string[] __column = { "วันที่ครบกำหนด", "เลขที่เอกสาร", "วันที่จ่ายเช็ค", "ชื่อเจ้าหนี้", "เลขที่จ่ายเช็ค", "ธนาคาร/สาขา", "สถานะ", "จำนวนเงิน", "หมายเหตุคำอธิบาย" };
                this._level = 1;
                for (int __i = 0; __i < __width.Length; __i++)
                {

                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                //this._query = new StringBuilder();

                //this._query.Append(" selct " + _g.d.cb_chq_list._chq_due_date + " , " + _g.d.cb_chq_list._doc_no + " , " + _g.d.cb_chq_list._chq_get_date);
                //this._query.Append(" , ( select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + " = " + _g.d.cb_chq_list._ap_ar_code + " ) as ap_name");
                //this._query.Append(" , " + _g.d.cb_chq_list._chq_number);
                //this._query.Append(" ,(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._code + " = (select " + _g.d.erp_pass_book._bank_code + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + " = " + _g.d.cb_credit_card._table + "." + _g.d.cb_credit_card._pass_book_code + ")) || ' / ' || (select " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + " = " + _g.d.cb_credit_card._table + "." + _g.d.cb_credit_card._pass_book_code + " ) as bank_name");
                //this._query.Append(" , " + _g.d.cb_chq_list._status + " , " + _g.d.cb_chq_list._amount + " , " + _g.d.cb_chq_list._remark);
                //this._query.Append(" from " + _g.d.cb_chq_list._table);
                //this._query.Append(" where " + _g.d.cb_chq_list._chq_type + " = 2 ");
                
            }
            else if (_cheque_card_Type == _cheque_cardEnumration.Cash_money_detail)
            {
                // รายงานรายวันขี้นเงินพร้อมรายการย่อย
                this._report_name = "รายงานรายวันขี้นเงินพร้อมรายการย่อย";
                int[] __width = { 10, 10, 15, 10, 15, 15 };
                string[] __column = { "เลขที่เอกสาร", "วันที่เอกสาร", "รหัสสมุดเงินฝาก", "จำนวนเงินรวม", "รวมค่าใช้จ่าย", "ยอดที่หักค่าใช้จ่ายแล้ว" };
                int[] __width_2 = { 10, 15, 10, 10, 10, 10, 10, 15, 15 };
                string[] __column_2 = { "เลขที่บัตรเครดิต", "ธนาคาร", "", "", "วันที่ครบกำหนด", "จำนวนเงินบนบัตร", "จำนวนเงิน", "ค่าใช้จ่าย", "ยอดที่หักค่าใช้จ่ายแล้ว" };
                this._level = 2;
                for (int __i = 0; __i < __width.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this._query = new StringBuilder();
                this._query.Append("");

            }
            else if (_cheque_card_Type == _cheque_cardEnumration.Cancel_cheque_detail)
            {
                // รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย
                this._report_name = "รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย";
                int[] __width = { 10, 10, 15, 10, 10, 10 };
                string[] __column = { "เลขที่เอกสาร", "วันที่เกสาร", "รหัสสมุดเงินฝาก", "จำนวนเงินรวม", "รวมค่าใช้จ่าย", "ยอดที่หักค่าใช้จ่ายแล้ว" };
                int[] __width_2 = { 10, 30, 10, 10, 10, 10, 10, 10, 10 };
                string[] __column_2 = { "เลขที่เช็ค", "ธนาคาร", "", "", "วันี่ทครบกำหนด", "จำนวนวเงินบนเช็ค", "จำนวนเงิน", "ค่าใช้จ่าย", "ยอดที่หักค่าใช้จ่ายแล้ว" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this._query = new StringBuilder();
                this._query.Append("");
            }
            else if (_cheque_card_Type == _cheque_cardEnumration.Cancel_Card_detail)
            {
                // รายงานการยกเลิกบัตรพร้อมรายการย่อย
                this._report_name = "รายงานการยกเลิกบัตรพร้อมรายการย่อย";
                int[] __width = { 10, 10, 15, 10, 10, 10 };
                string[] __column = { "เลขที่เอกสาร", "วันที่เกสาร", "รหัสสมุดเงินฝาก", "จำนวนเงินรวม", "รวมค่าใช้จ่าย", "ยอดที่หักค่าใช้จ่ายแล้ว" };
                int[] __width_2 = { 10, 30, 10, 10, 10, 10, 10, 10, 10 };
                string[] __column_2 = { "เลขที่เช็ค", "ธนาคาร", "", "", "วันี่ทครบกำหนด", "จำนวนวเงินบนเช็ค", "จำนวนเงิน", "ค่าใช้จ่าย", "ยอดที่หักค่าใช้จ่ายแล้ว" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this._query = new StringBuilder();
                this._query.Append("");
            }
            else if (_cheque_card_Type == _cheque_cardEnumration.Disposit_Cheque_detail)
            {
                // รายงานใบนำฝากเช็ครับพร้อมรายการย่อย             
                this._report_name = "รายงานใบนำฝากเช็ครับพร้อมรายการย่อย";
                int[] __width = { 10, 10, 15, 10, 10, 10 };
                string[] __column = { "เลขที่เอกสาร", "วันที่เกสาร", "รหัสสมุดเงินฝาก", "จำนวนเงินรวม", "รวมค่าใช้จ่าย", "ยอดที่หักค่าใช้จ่ายแล้ว" };
                int[] __width_2 = { 10, 20, 10, 10, 10, 10, 15, 15, 15, 15 };
                string[] __column_2 = { "เลขที่เช็ค", "ธนาคาร", "", "วันที่ครบกำหนด", "จำนวนเงินบนเช็ค", "จำนวนเงิน", "ค่าใช้จ่าย", "ยอดที่หักค่าใช้จ่ายแล้ว", "ชื่อลูกค้า/บริษัท", "รายวัน/คำอธิบาย" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this._query = new StringBuilder();
                this._query.Append("");
            }
            else if (_cheque_card_Type == _cheque_cardEnumration.Detail_Cheque_by_Date_import)
            {
                // รายงานรายละเอียดเช็ครับ-ตามวันที่รับ
                int[] __width = { 10, 15, 10, 15, 20, 15, 15, 10, 10, 15 };
                string[] __column = { "วันที่รับเช็ค", "เลขที่เอกสาร", "วันที่ครบกำหนด", "รหัสลูกค้า", "ชื่อลูกหนี้", "เลขที่เช็ครับ", "ธนาคาร/สาขา", "สถานะ", "จำนวนเงิน", "หมายเหตุ/คำอธิบาย" };
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                this._query = new StringBuilder();
                //this._query.Append(" selct " + _g.d.cb_chq_list._chq_due_date + " , " + _g.d.cb_chq_list._doc_no + " , " + _g.d.cb_chq_list._chq_get_date);
                //this._query.Append(" , ( select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + " = " + _g.d.cb_chq_list._ap_ar_code + " ) as ap_name");
                //this._query.Append(" , " + _g.d.cb_chq_list._chq_number);
                //this._query.Append(" ,(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._code + " = (select " + _g.d.erp_pass_book._bank_code + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + " = " + _g.d.cb_credit_card._table + "." + _g.d.cb_credit_card._pass_book_code + ")) || ' / ' || (select " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + " = " + _g.d.cb_credit_card._table + "." + _g.d.cb_credit_card._pass_book_code + " ) as bank_name");
                //this._query.Append(" , " + _g.d.cb_chq_list._status + " , " + _g.d.cb_chq_list._amount + " , " + _g.d.cb_chq_list._remark);
                //this._query.Append(" from " + _g.d.cb_chq_list._table);
                //this._query.Append(" where " + _g.d.cb_chq_list._chq_type + " = 1 ");
            }

        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        void _view1__getDataObject()
        {
            try
            {
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)_objReport._columnList[0];
               
                DataRow[] __dr = _dt.Select("");
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
                //int _no = 1;
                //  this._view1._reportProgressBar.Value = 0;
                //  int _rowprogreesbar = 0;
                if (__dr.Length > 0)
                {
                    //     _rowprogreesbar = (100 / __dr.Length);
                }
                int loopprogressbar = 0;

                if (_cheque_card_Type == _cheque_cardEnumration.EndDate_Card)
                {
                    // รายงานรายละเอียดบัตรเครดิต-ครบกำหนด            
                    int __no = 0;
                    double __total_amount = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        __no++;
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["credit_due_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่ครบกำหนด
                        this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เอกสาร
                        this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["credit_get_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่รับบัตรเครดิต
                        this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["ar_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ชื่อลูกหนี้
                        this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["credit_card_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่บัตรเครดิต
                        this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["bank_name"].ToString() + "/" + __dr[__i]["bank_branch"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ธนาคาร/สาขา
                        this._view1._addDataColumn(_objReport, __dataObject, 6, _convert_status(__dr[__i]["credit_card_type"].ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // สถานะ
                        this._view1._addDataColumn(_objReport, __dataObject, 7, __dr[__i]["sum_received"].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงิน
                        this._view1._addDataColumn(_objReport, __dataObject, 8, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // หมายเหตุ

                        if (__i == __dr.Length - 1)
                        {
                            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "รวม " + __no.ToString("#,###.##") + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, __total_amount.ToString("#,###.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        }
                    }
                }
                else if (_cheque_card_Type == _cheque_cardEnumration.EndDate_Pay_Cheque)
                {
                    // รายงานรายละเอียดเช็คจ่าย-วันครบกำหนด
                    int __no = 0;
                    double __total_amount = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        __no++;
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["chq_due_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่ครบกำหนด
                        this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เอกสาร
                        this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["chq_get_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่จ่ายเช็ค
                        this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["ap_ar_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ชื่อเจ้าหนี้
                        this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["chq_number"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เช็คจ่าย
                        this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["bank_name"].ToString() + "/" + __dr[__i]["bank_branch"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ธนาคาร/สาขา
                        this._view1._addDataColumn(_objReport, __dataObject, 6, _convert_status(__dr[__i]["chq_type"].ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // สถานะ
                        //this._view1._addDataColumn(_objReport, __dataObject, 7, double.Parse(__dr[__i]["sum_received"].ToString()).ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);  // จำนวนเงิน
                        this._view1._addDataColumn(_objReport, __dataObject, 7, double.Parse(__dr[__i]["sum_received"].ToString()).ToString("#,###.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงิน
                        this._view1._addDataColumn(_objReport, __dataObject, 8, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // หมายเหตุ

                        if (__i == __dr.Length - 1)
                        {
                            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "รวม " + __no + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, __total_amount.ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        }
                    }

                }
                else if (_cheque_card_Type == _cheque_cardEnumration.Cash_money_detail)
                {
                    // รายงานรายวันขี้นเงินพร้อมรายการย่อย
                    string __doc_no = "";
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เอกสาร
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["chq_get_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  วันที่เอกสาร
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["chq_number"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // รหัสสมุดเงินฝาก
                            this._view1._addDataColumn(_objReport, __dataObject, 3,"", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  จำนวนเงินรวม
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  รวมค่าใช้จ่าย
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  ยอดที่หักค่าใช้จ่ายแล้ว
                            DataRow[] __dr2 = _ds.Tables[0].Select("doc_no = \'" + __doc_no + "\'");

                            int __no = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no++;
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่บัตรเครดิต
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["bank_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ธนาคาร
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["chq_due_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่ครบกำหนด
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงินบนบัตร                        
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // จำนวนเงิน
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ค่าใช้จ่าย
                                this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ยอดที่หักค่าใช้จ่ายแล้ว

                                if (__j == __dr2.Length - 1)
                                {
                                    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                }
                            }
                        }
                    }

                }
                else if (_cheque_card_Type == _cheque_cardEnumration.Cancel_cheque_detail)
                {
                    // รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย
                    string __doc_no = "";
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เอกสาร
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่เอกสาร
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // รหัสสมุดเงินฝาก
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงินรวม
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // รวมค่าใช้จ่าย
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ยอดที่หักค่าใช้จ่ายแล้ว
                            DataRow[] __dr2 = _ds.Tables[0].Select("doc_no = \'" + __doc_no + "\'");
                            int __no = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no++;
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เช็ค
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ธนาคาร
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่ครบกำหนด
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงินบนเช็ค
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงิน
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ค่าใช้จ่าย
                                this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ยอดที่หักค่าใช้จ่ายแล้ว

                                if (__j == __dr2.Length - 1)
                                {
                                    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เช็ค
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                }
                            }

                        }
                    }

                }
                else if (_cheque_card_Type == _cheque_cardEnumration.Cancel_Card_detail)
                {
                    // รายงานการยกเลิกบัตรพร้อมรายการย่อย
                    string __doc_no = "";
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เอกสาร
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่เอกสาร
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // รหัสสมุดเงินฝาก
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงินรวม
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // รวมค่าใช้จ่าย
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ยอดที่หักค่าใช้จ่ายแล้ว
                            DataRow[] __dr2 = _ds.Tables[0].Select("doc_no = \'" + __doc_no + "\'");
                            int __no = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no++;
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เช็ค
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ธนาคาร
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่ครบกำหนด
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงินบนเช็ค
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงิน
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ค่าใช้จ่าย
                                this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ยอดที่หักค่าใช้จ่ายแล้ว

                                if (__j == __dr2.Length - 1)
                                {
                                    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เช็ค
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                }
                            }

                        }
                    }

                }
                else if (_cheque_card_Type == _cheque_cardEnumration.Disposit_Cheque_detail)
                {
                    // รายงานใบนำฝากเช็ครับพร้อมรายการย่อย 
                    string __doc_no = "";
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เอกสาร
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่เอกสาร
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // รหัสสมุดเงินฝาก
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงินรวม
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // รวมค่าใช้จ่าย
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ยอดที่หักค่าใช้จ่ายแล้ว
                            DataRow[] __dr2 = _ds.Tables[0].Select("doc_no = \'" + __doc_no + "\'");
                            int __no = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no++;
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เช็ค
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ธนาคาร
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // วันที่ครบกำหนด
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงินบนเช็ค
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงิน
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ค่าใช้จ่าย
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ยอดที่หักค่าใช้จ่ายแล้ว
                                this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ชื่อลูกค้า
                                this._view1._addDataColumn(_objReport2, __dataObject2, 9, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // คำอธิบาย
                                if (__j == __dr2.Length - 1)
                                {
                                    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 9, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                }
                            }
                        }
                    }
                }
                else if (_cheque_card_Type == _cheque_cardEnumration.Detail_Cheque_by_Date_import)
                {
                    // รายงานรายละเอียดเช็ครับ-ตามวันที่รับ
                    int __no = 0;
                    double __total_amount = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        __no++;
                        __total_amount += double.Parse(__dr[__i][""].ToString());
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่รับเช็ค
                        this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เอกสาร
                        this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // วันที่ครบกำหนด
                        this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // รหัสลูกค้า
                        this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ชื่อลูกหนี้
                        this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เช็ครับ
                        this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ธนาคาร/สาขา
                        this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // สถานะ
                        this._view1._addDataColumn(_objReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวนเงิน
                        this._view1._addDataColumn(_objReport, __dataObject, 9, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // หมายเหตุ\
                        if (__i == __dr.Length - 1)
                        {
                            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "รวม " + __no + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 8, __total_amount.ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 9, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(":: ERR :: \n" + ex.Message);
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                // this._view_ic._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.Text, SMLReport._report._reportValueDefault._ltdaddress, SMLReport._report._cellAlign.Center, this._view_ic._fontStandard); 

                this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, this._report_name, SMLReport._report._cellAlign.Center, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontStandard);
                //this._view_ic._excelFileName = "รายงานยอดการชำระเงิน";//
                //this._view_ic._maxColumn = 9;
                return true;

            }
            else if (type == SMLReport._report._objectType.Detail)
            {
                if (this._level == 1)
                {
                    _objReport = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    for (int __i = 0; __i < this._column.Count; __i++)
                    {
                        this._view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._width[__i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column[__i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                }
                else if (this._level == 2)
                {
                    _objReport = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    _objReport2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    for (int __i = 0; __i < this._column.Count; __i++)
                    {
                        this._view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._width[__i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column[__i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    for (int __j = 0; __j < this._column_2.Count; __j++)
                    {
                        this._view1._addColumn(_objReport2, MyLib._myGlobal._intPhase(this._width_2[__j].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column_2[__j].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                }

                return true;
            }
            return false;
        }
        StringBuilder _query;

        string _convert_status(string __value)
        {
            string __result = "";
            if (__value != "null")
            {
                if (__value == "1") __result = "ในมือ";
                if (__value == "2") __result = "ขึ้นเงินแล้ว";
                if (__value == "3") __result = "ยกเลิก";
            }
            return __result;
        }
        void _view1__loadData()
        {

        }


        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (_dt != null)
            {
                _width.Clear();
                _column.Clear();
                _width_2.Clear();
                _column_2.Clear();

            }
            this._config();
            this._view1._buildReport(SMLReport._report._reportType.Standard);
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }

        void _showCondition()
        {
            SMLERPReport.Cheque_Card.condition_cheque_card __condition = new condition_cheque_card();
            __condition._page = _cheque_card_Type.ToString();
            __condition._condition_cheque_card1._clear();
            __condition.ShowDialog();
        }
    }


    public static class _cheque_card_Global
    {
        public static int _int_cheque_card(_cheque_cardEnumration ChequeCardType)
        {
            switch (ChequeCardType)
            {
                case _cheque_cardEnumration.EndDate_Card: return 14;      // รายงานรายละเอียดบัตรเครดิต-ครบกำหนด
                case _cheque_cardEnumration.EndDate_Pay_Cheque: return 15;     // รายงานรายละเอียดเช็คจ่าย-วันครบกำหนด
                case _cheque_cardEnumration.Cash_money_detail: return 16;     // รายงานรายวันขี้นเงินพร้อมรายการย่อย
                case _cheque_cardEnumration.Cancel_cheque_detail: return 17;     // รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย
                case _cheque_cardEnumration.Cancel_Card_detail: return 18;     // รายงานการยกเลิกบัตรพร้อมรายการย่อย
                case _cheque_cardEnumration.Disposit_Cheque_detail: return 19;    // รายงานใบนำฝากเช็ครับพร้อมรายการย่อย 
                case _cheque_cardEnumration.Detail_Cheque_by_Date_import: return 20;     // รายงานรายละเอียดเช็ครับ-ตามวันที่รับ
            }
            return 0;
        }
    }


    public enum _cheque_cardEnumration
    {
        /// <summary>
        /// รายงานรายละเอียดบัตรเครดิต-ครบกำหนด
        /// </summary>
        EndDate_Card,
        /// <summary>
        /// รายงานรายละเอียดเช็คจ่าย-วันครบกำหนด
        /// </summary>
        EndDate_Pay_Cheque,
        /// <summary>
        /// รายงานรายวันขี้นเงินพร้อมรายการย่อย
        /// </summary>
        Cash_money_detail,
        /// <summary>
        /// รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย
        /// </summary>
        Cancel_cheque_detail,
        /// <summary>
        /// รายงานการยกเลิกบัตรพร้อมรายการย่อย
        /// </summary>
        Cancel_Card_detail,
        /// <summary>
        /// รายงานใบนำฝากเช็ครับพร้อมรายการย่อย 
        /// </summary>
        Disposit_Cheque_detail,
        /// <summary>
        /// รายงานรายละเอียดเช็ครับ-ตามวันที่รับ
        /// </summary>
        Detail_Cheque_by_Date_import
    }
}
