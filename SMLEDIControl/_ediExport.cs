using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;

namespace SMLEDIControl
{
    public partial class _ediExport : UserControl
    {
        MyLib._searchDataFull _search;

        public _ediExport()
        {
            InitializeComponent();


            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._build();
            }
        }

        void _build()
        {
            string __formatNumberPrice = _g.g._getFormatNumberStr(3);

            this._conditionScreen._maxColumn = 2;
            this._conditionScreen._addTextBox(0, 0, 1, 1, _g.d.edi_external._table + "." + _g.d.edi_external._code, 1, 25, 1, true, false);
            this._conditionScreen._addTextBox(0, 1, 1, 1, _g.d.edi_external._table + "." + _g.d.edi_external._ar_code, 1, 25, 1, true, false);
            this._conditionScreen._addDateBox(1, 0, 1, 1, _g.d.resource_report._table + "." + _g.d.resource_report._from_date, 1, true);
            this._conditionScreen._addDateBox(1, 1, 1, 1, _g.d.resource_report._table + "." + _g.d.resource_report._to_date, 1, true);
            this._conditionScreen._textBoxSearch += _conditionScreen__textBoxSearch;
            this._conditionScreen._enabedControl(_g.d.edi_external._table + "." + _g.d.edi_external._ar_code, false);

            this._gridDocList._table_name = _g.d.ic_trans._table;
            this._gridDocList.IsEdit = false;
            this._gridDocList._addColumn("check", 11, 10, 10);
            this._gridDocList._addColumn(_g.d.ic_trans._doc_date, 4, 35, 35, false, false, false, false, "dd/MM/yyyy");
            this._gridDocList._addColumn(_g.d.ic_trans._doc_no, 1, 35, 35);
            this._gridDocList._addColumn(_g.d.ic_trans._total_amount, 3, 20, 20, true, false, true, false, __formatNumberPrice);
            this._gridDocList._calcPersentWidthToScatter();

            this._clear();
        }

        private void _conditionScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __textbox = (MyLib._myTextBox)sender;
            if (__textbox._name == _g.d.edi_external._table + "." + _g.d.edi_external._code)
            {
                if (_search == null)
                {
                    _search = new MyLib._searchDataFull();
                    _search._dataList._loadViewFormat("screen_edi_external", MyLib._myGlobal._userSearchScreenGroup, false);
                    _search._dataList._gridData._mouseClick += (s1, e1) =>
                    {
                        if (e1._row != -1)
                        {
                            object __code = this._search._dataList._gridData._cellGet(this._search._dataList._gridData._selectRow, _g.d.edi_external._table + "." + _g.d.edi_external._code);
                            object __name = this._search._dataList._gridData._cellGet(this._search._dataList._gridData._selectRow, _g.d.edi_external._table + "." + _g.d.edi_external._name_1);
                            object __arCode = this._search._dataList._gridData._cellGet(this._search._dataList._gridData._selectRow, _g.d.edi_external._table + "." + _g.d.edi_external._ar_code);

                            this._conditionScreen._setDataStr(_g.d.edi_external._table + "." + _g.d.edi_external._code, __code.ToString(), __name.ToString(), true);
                            this._conditionScreen._setDataStr(_g.d.edi_external._table + "." + _g.d.edi_external._ar_code, __arCode.ToString());
                            this._search.Close();
                        }
                    };

                    _search._searchEnterKeyPress += (s1, e1) =>
                    {
                        if (e1 != -1)
                        {
                            object __code = this._search._dataList._gridData._cellGet(this._search._dataList._gridData._selectRow, _g.d.edi_external._table + "." + _g.d.edi_external._code);
                            object __name = this._search._dataList._gridData._cellGet(this._search._dataList._gridData._selectRow, _g.d.edi_external._table + "." + _g.d.edi_external._name_1);
                            object __arCode = this._search._dataList._gridData._cellGet(this._search._dataList._gridData._selectRow, _g.d.edi_external._table + "." + _g.d.edi_external._ar_code);

                            this._conditionScreen._setDataStr(_g.d.edi_external._table + "." + _g.d.edi_external._code, __code.ToString(), __name.ToString(), true);
                            this._conditionScreen._setDataStr(_g.d.edi_external._table + "." + _g.d.edi_external._ar_code, __arCode.ToString());
                            this._search.Close();
                        }
                    };
                }

                MyLib._myGlobal._startSearchBox(__textbox, "External EDI Search", _search);

            }
        }

        void _loadData()
        {
            string __getARCode = this._conditionScreen._getDataStr(_g.d.edi_external._table + "." + _g.d.edi_external._ar_code);
            string __getEDICode = this._conditionScreen._getDataStr(_g.d.edi_external._table + "." + _g.d.edi_external._code);

            string __query = "select " + _g.d.ic_trans._doc_date + ", " + _g.d.ic_trans._doc_no + ", " + _g.d.ic_trans._total_amount + " from " + _g.d.ic_trans._table +
                " where " + _g.d.ic_trans._trans_flag + " = 44 and " + _g.d.ic_trans._doc_date + " between " + this._conditionScreen._getDataStrQuery(_g.d.resource_report._table + "." + _g.d.resource_report._from_date) + " and " + this._conditionScreen._getDataStrQuery(_g.d.resource_report._table + "." + _g.d.resource_report._to_date) + " and " + _g.d.ic_trans._last_status + " =0  " +
                " and ( (" + _g.d.ic_trans._cust_code + " = \'" + __getARCode + "\') or (cust_code in (select ar_code from edi_ar_list where edi_code = \'" + __getEDICode + "\')) ) order by doc_date, doc_no";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort(__query);
            this._gridDocList._loadFromDataTable(__result.Tables[0]);


            _buttonCheckAll_Click(null, null);
        }

        void _clear()
        {
            this._conditionScreen._clear();
            this._gridDocList._clear();

            DateTime __beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime __endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            this._conditionScreen._setDataDate(_g.d.resource_report._table + "." + _g.d.resource_report._from_date, __beginDate);
            this._conditionScreen._setDataDate(_g.d.resource_report._table + "." + _g.d.resource_report._to_date, __endDate);
        }


        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonLoad_Click(object sender, EventArgs e)
        {
            this._loadData();
        }

        private void _buttonClear_Click(object sender, EventArgs e)
        {
            this._clear();
        }

        private void _buttonCheckAll_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < this._gridDocList._rowData.Count; row++)
            {
                this._gridDocList._cellUpdate(row, 0, 1, true);
            }
            this._gridDocList.Invalidate();
        }

        private void _buttonUncheckAll_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < this._gridDocList._rowData.Count; row++)
            {
                this._gridDocList._cellUpdate(row, 0, 0, true);
            }
            this._gridDocList.Invalidate();
        }

        private void _buttonExportPreview_Click(object sender, EventArgs e)
        {
            _process(true);
        }

        private void _buttonExport_Click(object sender, EventArgs e)
        {
            _process(false);

        }

        void _process(bool isPreview)
        {
            string __saveFilename = "";
            Boolean __isOverwrite = false;

            StringBuilder __docList = new StringBuilder();

            for (int row = 0; row < this._gridDocList._rowData.Count; row++)
            {
                if (this._gridDocList._cellGet(row, 0).ToString() == "1")
                {
                    if (__docList.Length > 0)
                    {
                        __docList.Append(",");
                    }

                    __docList.Append("\'" + this._gridDocList._cellGet(row, _g.d.ic_trans._doc_no).ToString() + "\'");
                }
            }

            if (__docList.Length > 0)
            {
                if (isPreview)
                {
                    this._resultTextbox.Text = "";
                }

                string __externalCode = this._conditionScreen._getDataStr(_g.d.edi_external._table + "." + _g.d.edi_external._code);

                string __queryGetEDI = "select * from " + _g.d.edi_external._table + " where " + _g.d.edi_external._code + "=\'" + __externalCode + "\'";

                string __queryGetDataTrans = "select " +
                    MyLib._myGlobal._fieldAndComma(
                        _g.d.ic_trans._doc_no,
                        _g.d.ic_trans._doc_date,
                        _g.d.ic_trans._doc_time,
                        _g.d.ic_trans._cust_code,
                        _g.d.ic_trans._total_amount,
                        _g.d.ic_trans._total_before_vat,
                        _g.d.ic_trans._total_vat_value,
                        _g.d.ic_trans._trans_flag,
                        _g.d.ic_trans._total_value,
                        _g.d.ic_trans._discount_word,
                        _g.d.ic_trans._total_discount,
                        _g.d.ic_trans._credit_day,
                        _g.d.ic_trans._credit_date,
                        " (select doc_ref from ic_trans as ref where ref.doc_no = (select ref_doc_no from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag limit 1)) as doc_no_po ",
                        " (select doc_ref_date from ic_trans as ref where ref.doc_no = (select ref_doc_no from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag limit 1)) as po_date ",
                        " (select ref_doc_no from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag limit 1) as ref_doc_no ",
                        "(select number from erp_branch_list where erp_branch_list.code = ic_trans.branch_code ) as branch_code_number ",
                        "(select ship_code from ap_ar_transport_label where ar_ap_type = 1 and cust_code = ic_trans.cust_code and name_1 = (select transport_name from ic_trans_shipment where ic_trans_shipment.doc_no = ic_trans.doc_no and ic_trans_shipment.trans_flag = ic_trans.trans_flag)) as ship_code",
                        "(select ship_code from ic_trans_shipment where ic_trans_shipment.doc_no = ic_trans.doc_no and ic_trans_shipment.trans_flag = ic_trans.trans_flag) as ship_code_trans",
                        "(select total_discount from ic_trans as ref where ref.doc_no = (select ref_doc_no from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag limit 1)) as po_discount") +
                        " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " in (" + __docList.ToString() + ") order by doc_date, doc_no";


                string __queryGetDataDetail = "select " +
                    MyLib._myGlobal._fieldAndComma(
                        _g.d.ic_trans_detail._doc_no,
                        _g.d.ic_trans_detail._doc_date,
                        _g.d.ic_trans_detail._doc_time,
                        _g.d.ic_trans_detail._item_code,
                        _g.d.ic_trans_detail._qty,
                        _g.d.ic_trans_detail._line_number,
                        _g.d.ic_trans_detail._price,
                        _g.d.ic_trans_detail._discount_amount,
                        _g.d.ic_trans_detail._item_name,
                        _g.d.ic_trans_detail._stand_value,
                        _g.d.ic_trans_detail._divide_value,
                        _g.d.ic_trans_detail._sum_amount,
                        _g.d.ic_trans_detail._is_permium,
                        "(select doc_ref from ic_trans where ic_trans.doc_no = ic_trans_detail.ref_doc_no) as " + _g.d.ic_trans_detail._ref_doc_no,
                        "(select doc_ref_date from ic_trans where ic_trans.doc_no = ic_trans_detail.ref_doc_no) as " + _g.d.ic_trans_detail._ref_doc_date,
                        "( select " + _g.d.edi_product_list._item_code + " from " + _g.d.edi_product_list._table + " where " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._edi_code + "=\'" + __externalCode + "\' ) as edi_itemcode",
                        "( select " + _g.d.edi_product_list._item_name + " from " + _g.d.edi_product_list._table + " where " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._edi_code + "=\'" + __externalCode + "\' ) as edi_itemname",
                        "(qty*price) as qty_price ",
                        "(select credit_day from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) as " + _g.d.ic_trans._credit_day,
                        "(select credit_date from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) as " + _g.d.ic_trans._due_date,
                        "(select vat_rate from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag) as " + _g.d.ic_trans._vat_rate,
                        _g.d.ic_trans_detail._barcode,
                        "coalesce(" +
                        // barcode external
                        " ( select " + _g.d.edi_barcode_list._edi_barcode + " from " + _g.d.edi_barcode_list._table + " where " + _g.d.edi_barcode_list._table + "." + _g.d.edi_barcode_list._barcode + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._barcode + " and " + _g.d.edi_barcode_list._table + "." + _g.d.edi_barcode_list._edi_code + "=\'" + __externalCode + "\' )" +
                        // product_code external
                        "," +
                        "( select " + _g.d.edi_product_list._item_code + " from " + _g.d.edi_product_list._table + " where " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._edi_code + "=\'" + __externalCode + "\' )" +
                        " ) as edi_barcode ",
                        _g.d.ic_trans_detail._discount) +
                    " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docList.ToString() + ") order by " + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;

                string __queryGetPO = "select " +
                    "doc_no, doc_date, doc_time, item_code, line_number, qty, price, stand_value, divide_value " +
                    ",(select doc_ref from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no) as ref_doc_no " +
                    ",(select doc_ref_date from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no) as ref_doc_date " +
                    ", ( select " + _g.d.edi_product_list._item_code + " from " + _g.d.edi_product_list._table + " where " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._edi_code + "=\'" + __externalCode + "\' ) as edi_itemcode" +
                    ", ( select " + _g.d.edi_product_list._item_name + " from " + _g.d.edi_product_list._table + " where " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._edi_code + "=\'" + __externalCode + "\' ) as edi_itemname" +
                    ", (select credit_day from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) as " + _g.d.ic_trans._credit_day +
                    ", (select credit_date from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) as " + _g.d.ic_trans._due_date +
                    ",( select " + _g.d.edi_product_list._item_packsize + " from " + _g.d.edi_product_list._table + " where " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and " + _g.d.edi_product_list._table + "." + _g.d.edi_product_list._edi_code + "=\'" + __externalCode + "\' ) as " + _g.d.edi_product_list._item_packsize +
                    " from ic_trans_detail where doc_no in ( select distinct ref_doc_no from ic_trans_detail where doc_no in (" + __docList.ToString() + ") ) order by doc_date,doc_no,line_number";

                StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetEDI));
                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetDataTrans));
                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetDataDetail));
                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetPO));

                __queryList.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());

                if (__result.Count > 0)
                {
                    DataTable __externalData = ((DataSet)__result[0]).Tables[0];
                    DataTable __transData = ((DataSet)__result[1]).Tables[0];
                    DataTable __detailData = ((DataSet)__result[2]).Tables[0];

                    int __format = MyLib._myGlobal._intPhase(__externalData.Rows[0][_g.d.edi_external._export_format].ToString());

                    switch (__format)
                    {
                        // CP EXPORT

                        case 0:
                            {
                                string __supplierCode = __externalData.Rows[0][_g.d.edi_external._supplier_ean_code].ToString();// __externalData.Rows[0][_g.d.;
                                string __buyerCode = __externalData.Rows[0][_g.d.edi_external._code].ToString();

                                if (isPreview == false)
                                {
                                    SaveFileDialog __saveCSV = new SaveFileDialog();

                                    __saveCSV.Filter = "CSV Files (*.csv)|*.csv";
                                    __saveCSV.Title = "Save SCV File";
                                    __saveCSV.FileName = DateTime.Now.ToString("yyMMdd", new CultureInfo("en-US")) + ".csv";
                                    if (__saveCSV.ShowDialog() == DialogResult.OK)
                                    {
                                        if (__saveCSV.FileName.Length > 0)
                                        {
                                            __isOverwrite = __saveCSV.OverwritePrompt;
                                            __saveFilename = __saveCSV.FileName;
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }

                                /*
                                for (int __row = 0; __row < __detailData.Rows.Count; __row++)
                                {
                                    string __DocNo = __detailData.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __docTime = __detailData.Rows[__row][_g.d.ic_trans_detail._doc_time].ToString();

                                    string[] __time = __docTime.Split(':');
                                    int __hour = MyLib._myGlobal._intPhase(__time[0]);
                                    int __minute = MyLib._myGlobal._intPhase(__time[1]);

                                    DateTime __docDateTimeTemp = MyLib._myGlobal._convertDateFromQuery(__detailData.Rows[__row][_g.d.ic_trans_detail._doc_date].ToString());
                                    DateTime __docDateTime = new DateTime(__docDateTimeTemp.Year, __docDateTimeTemp.Month, __docDateTimeTemp.Day, __hour, __minute, 0);

                                    string __DocDate = __docDateTime.ToString("yyyyMMddHHmm", new CultureInfo("en-US"));

                                    string __poNo = __detailData.Rows[__row][_g.d.ic_trans_detail._ref_doc_no].ToString();
                                    string __lineItemNo = (MyLib._myGlobal._intPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._line_number].ToString()) + 1).ToString();
                                    string __itemNo = __detailData.Rows[__row]["edi_itemcode"].ToString();
                                    string __itemQty = MyLib._myGlobal._intPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._qty].ToString()).ToString();


                                    string __dataExport = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",{5},\"{6}\",{7}", __DocNo, __DocDate, __poNo.Trim(), __supplierCode, __buyerCode, __lineItemNo, __itemNo, __itemQty);

                                    if (isPreview)
                                    {
                                        this._resultTextbox.AppendText(__dataExport + "\r\n");
                                    }
                                    else
                                    {
                                        FileMode __mode = (__row == 0 && __isOverwrite) ? FileMode.Create : FileMode.Append;
                                        //bool __isOverWriteData = (__row == 0 && __isOverwrite) ? true : false;
                                        using (FileStream fs = new FileStream(__saveFilename, __mode, FileAccess.Write))
                                        using (StreamWriter sw = new StreamWriter(fs))
                                        {
                                            sw.WriteLine(__dataExport);
                                        }
                                    }
                                }*/
                                DataTable __poTable = ((DataSet)__result[3]).Tables[0];

                                for (int __rowTrans = 0; __rowTrans < __transData.Rows.Count; __rowTrans++)
                                {
                                    string __DocNo = __transData.Rows[__rowTrans][_g.d.ic_trans._doc_no].ToString();
                                    string __docTime = __transData.Rows[__rowTrans][_g.d.ic_trans._doc_time].ToString();

                                    string[] __time = __docTime.Split(':');
                                    int __hour = MyLib._myGlobal._intPhase(__time[0]);
                                    int __minute = MyLib._myGlobal._intPhase(__time[1]);


                                    DateTime __docDateTimeTemp = MyLib._myGlobal._convertDateFromQuery(__transData.Rows[__rowTrans][_g.d.ic_trans._doc_date].ToString());
                                    DateTime __docDateTime = new DateTime(__docDateTimeTemp.Year, __docDateTimeTemp.Month, __docDateTimeTemp.Day, __hour, __minute, 0);

                                    string __DocDate = __docDateTime.ToString("yyyyMMddHHmm", new CultureInfo("th-TH"));

                                    string __poNo = __transData.Rows[__rowTrans]["ref_doc_no"].ToString();
                                    DataRow[] __poItemRow = __poTable.Select(" doc_no = \'" + __poNo + "\'");

                                    for (int __row = 0; __row < __poItemRow.Length; __row++)
                                    {
                                        string __internalItemCode = __poItemRow[__row][_g.d.ic_trans_detail._item_code].ToString();
                                        DataRow[] __saleDetailTrans = __detailData.Select(" doc_no =\'" + __DocNo + "\' and item_code =\'" + __internalItemCode + "\' ");

                                        string __lineItemNo = (MyLib._myGlobal._intPhase(__poItemRow[__row][_g.d.ic_trans_detail._line_number].ToString()) + 1).ToString();
                                        string __itemNo = __poItemRow[__row]["edi_itemcode"].ToString();
                                        string __orderNo = __poItemRow[__row][_g.d.ic_trans_detail._ref_doc_no].ToString();


                                        string __itemQty = (__saleDetailTrans.Length > 0) ? __saleDetailTrans[0][_g.d.ic_trans_detail._qty].ToString() : "0";

                                        string __docNoExport = __DocNo;

                                        if (__docNoExport.Length > 10)
                                        {
                                            if (__docNoExport.IndexOf(' ') != -1)
                                            {
                                                __docNoExport = __docNoExport.Replace(" ", string.Empty);
                                            }

                                            int __startIndex = (__docNoExport.Length > 10) ? __docNoExport.Length - 10 : 0;
                                            __docNoExport = __docNoExport.Substring(__startIndex);

                                        }

                                        string __dataExport = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",{5},\"{6}\",{7},0,0", __docNoExport, __DocDate, __orderNo.Trim(), __supplierCode, __buyerCode, __lineItemNo, __itemNo, __itemQty);

                                        if (isPreview)
                                        {
                                            this._resultTextbox.AppendText(__dataExport + "\r\n");
                                        }
                                        else
                                        {
                                            FileMode __mode = ((__rowTrans == 0 && __row == 0) && __isOverwrite) ? FileMode.Create : FileMode.Append;
                                            //bool __isOverWriteData = (__row == 0 && __isOverwrite) ? true : false;
                                            using (FileStream fs = new FileStream(__saveFilename, __mode, FileAccess.Write))
                                            using (StreamWriter sw = new StreamWriter(fs))
                                            {
                                                sw.WriteLine(__dataExport);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case 1:
                            {
                                // BIGC
                                DataTable __poTable = ((DataSet)__result[3]).Tables[0];

                                string __supplierCode = __externalData.Rows[0][_g.d.edi_external._supplier_ean_code].ToString();// __externalData.Rows[0][_g.d.;
                                string __buyerCode = __externalData.Rows[0][_g.d.edi_external._code].ToString();

                                if (isPreview == false)
                                {
                                    SaveFileDialog __saveCSV = new SaveFileDialog();

                                    __saveCSV.Filter = "CSV Files (*.csv)|*.csv";
                                    __saveCSV.Title = "Save SCV File";
                                    __saveCSV.FileName = DateTime.Now.ToString("yyMMdd", new CultureInfo("en-US")) + ".csv";
                                    if (__saveCSV.ShowDialog() == DialogResult.OK)
                                    {
                                        if (__saveCSV.FileName.Length > 0)
                                        {
                                            __isOverwrite = __saveCSV.OverwritePrompt;
                                            __saveFilename = __saveCSV.FileName;
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }

                                }

                                /*
                                
                                for (int __row = 0; __row < __detailData.Rows.Count; __row++)
                                    {
                                        string __DocNo = __detailData.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                        string __docTime = __detailData.Rows[__row][_g.d.ic_trans_detail._doc_time].ToString();

                                        string[] __time = __docTime.Split(':');
                                        int __hour = MyLib._myGlobal._intPhase(__time[0]);
                                        int __minute = MyLib._myGlobal._intPhase(__time[1]);

                                        DateTime __docDateTimeTemp = MyLib._myGlobal._convertDateFromQuery(__detailData.Rows[__row][_g.d.ic_trans_detail._doc_date].ToString());
                                        DateTime __docDateTime = new DateTime(__docDateTimeTemp.Year, __docDateTimeTemp.Month, __docDateTimeTemp.Day, __hour, __minute, 0);

                                        string __DocDate = __docDateTime.ToString("yyyyMMddHHmm", new CultureInfo("en-US"));

                                        string __poNo = __detailData.Rows[__row][_g.d.ic_trans_detail._ref_doc_no].ToString();
                                        string __poDate = (__detailData.Rows[__row][_g.d.ic_trans_detail._ref_doc_date].ToString().Length > 0) ? MyLib._myGlobal._convertDateFromQuery(__detailData.Rows[__row][_g.d.ic_trans_detail._ref_doc_date].ToString()).ToString("yyyyMMdd", new CultureInfo("en-US")) : "";

                                        string __lineItemNo = (MyLib._myGlobal._intPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._line_number].ToString()) + 1).ToString();
                                        string __itemNo = __detailData.Rows[__row]["edi_itemcode"].ToString();
                                        string __product_description = __detailData.Rows[__row]["edi_itemname"].ToString();
                                        decimal __acceptOrderQty = MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                        decimal __acceptTotalQuanity = __acceptOrderQty * (MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._stand_value].ToString()) / MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._divide_value].ToString()));


                                        string __creditDay = __detailData.Rows[__row][_g.d.ic_trans._credit_day].ToString();
                                        DateTime __dueDateTimeTemp = MyLib._myGlobal._convertDateFromQuery(__detailData.Rows[__row][_g.d.ic_trans._due_date].ToString());
                                        DateTime __dueDateTime = new DateTime(__dueDateTimeTemp.Year, __dueDateTimeTemp.Month, __dueDateTimeTemp.Day, __hour, __minute, 0);

                                        string __DueDate = __dueDateTime.ToString("yyyyMMddHHmm", new CultureInfo("en-US"));
                                        //decimal __sumAmount = MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());

                                        decimal __price = MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._price].ToString()) / (MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._stand_value].ToString()) / MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._divide_value].ToString()));

                                        string __dataExport = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",{11},{12},{13},{14}",
                                            __DocNo, __DocDate, __poNo.Trim(), __poDate, __DueDate, __creditDay, __supplierCode, __buyerCode,
                                            __itemNo, "", __product_description, MyLib._myGlobal._round(__acceptOrderQty, 3), MyLib._myGlobal._round(__acceptTotalQuanity, 3), MyLib._myGlobal._round(__price, 3), MyLib._myGlobal._round(__price * __acceptTotalQuanity, 3));

                                        if (isPreview)
                                        {
                                            this._resultTextbox.AppendText(__dataExport + "\r\n");
                                        }
                                        else
                                        {
                                            FileMode __mode = (__row == 0 && __isOverwrite) ? FileMode.Create : FileMode.Append;
                                            //bool __isOverWriteData = (__row == 0 && __isOverwrite) ? true : false;
                                            using (FileStream fs = new FileStream(__saveFilename, __mode, FileAccess.Write))
                                            using (StreamWriter sw = new StreamWriter(fs))
                                            {
                                                sw.WriteLine(__dataExport);
                                            }
                                        }
                                    }
                                
                                */

                                for (int __rowTrans = 0; __rowTrans < __transData.Rows.Count; __rowTrans++)
                                {
                                    string __poNo = __transData.Rows[__rowTrans]["ref_doc_no"].ToString();
                                    // get po by invoice doc_no
                                    DataRow[] __poItemRow = __poTable.Select(" doc_no = \'" + __poNo + "\'");

                                    string __DocNo = __transData.Rows[__rowTrans][_g.d.ic_trans._doc_no].ToString();
                                    string __docTime = __transData.Rows[__rowTrans][_g.d.ic_trans._doc_time].ToString();

                                    string[] __time = __docTime.Split(':');
                                    int __hour = MyLib._myGlobal._intPhase(__time[0]);
                                    int __minute = MyLib._myGlobal._intPhase(__time[1]);

                                    DateTime __docDateTimeTemp = MyLib._myGlobal._convertDateFromQuery(__transData.Rows[__rowTrans][_g.d.ic_trans._doc_date].ToString());
                                    DateTime __docDateTime = new DateTime(__docDateTimeTemp.Year, __docDateTimeTemp.Month, __docDateTimeTemp.Day, __hour, __minute, 0);
                                    string __DocDate = __docDateTime.ToString("yyyyMMddHHmm", new CultureInfo("en-US"));

                                    string __creditDay = __transData.Rows[__rowTrans][_g.d.ic_trans._credit_day].ToString();
                                    //DateTime __dueDateTimeTemp = MyLib._myGlobal._convertDateFromQuery(__transData.Rows[__rowTrans][_g.d.ic_trans._due_date].ToString());
                                    //DateTime __dueDateTime = new DateTime(__dueDateTimeTemp.Year, __dueDateTimeTemp.Month, __dueDateTimeTemp.Day, __hour, __minute, 0);
                                    string __DueDate = (__transData.Rows[__rowTrans][_g.d.ic_trans._credit_date].ToString().Length > 0) ? MyLib._myGlobal._convertDateFromQuery(__transData.Rows[__rowTrans][_g.d.ic_trans._credit_date].ToString()).ToString("yyyyMMdd", new CultureInfo("en-US")) : "";

                                    string __delivery_point = __transData.Rows[__rowTrans]["ship_code"].ToString();
                                    string __delevery_point_trans = __transData.Rows[__rowTrans]["ship_code_trans"].ToString();

                                    for (int __row = 0; __row < __poItemRow.Length; __row++)
                                    {
                                        string __internalItemCode = __poItemRow[__row][_g.d.ic_trans_detail._item_code].ToString();
                                        DataRow[] __saleDetailTrans = __detailData.Select(" doc_no =\'" + __DocNo + "\' and item_code =\'" + __internalItemCode + "\' ");

                                        string __orderNo = __poItemRow[__row][_g.d.ic_trans_detail._ref_doc_no].ToString();

                                        string __poDate = (__poItemRow[__row][_g.d.ic_trans_detail._ref_doc_date].ToString().Length > 0) ? MyLib._myGlobal._convertDateFromQuery(__poItemRow[__row][_g.d.ic_trans_detail._ref_doc_date].ToString()).ToString("yyyyMMdd", new CultureInfo("en-US")) : "";

                                        string __lineItemNo = (MyLib._myGlobal._intPhase(__poItemRow[__row][_g.d.ic_trans_detail._line_number].ToString()) + 1).ToString();
                                        string __itemNo = __poItemRow[__row]["edi_itemcode"].ToString();
                                        string __product_description = __poItemRow[__row]["edi_itemname"].ToString();

                                        // packsize 
                                        decimal __packSize = MyLib._myGlobal._decimalPhase(__poItemRow[__row][_g.d.edi_product_list._item_packsize].ToString());

                                        // ใส่ จำนวน PO เข้าไป
                                        //
                                        decimal __acceptOrderQty = (__saleDetailTrans.Length > 0) ? MyLib._myGlobal._decimalPhase(__saleDetailTrans[0][_g.d.ic_trans_detail._qty].ToString()) : 0M;
                                        decimal __poOrderQty = MyLib._myGlobal._decimalPhase(__poItemRow[__row][_g.d.ic_trans_detail._qty].ToString());

                                        decimal __unitQtyStandValue = MyLib._myGlobal._decimalPhase(__poItemRow[__row][_g.d.ic_trans_detail._stand_value].ToString());
                                        decimal __unitQtyDivideValue = MyLib._myGlobal._decimalPhase(__poItemRow[__row][_g.d.ic_trans_detail._divide_value].ToString());

                                        decimal __acceptUnitPrice = MyLib._myGlobal._decimalPhase(__poItemRow[__row][_g.d.ic_trans_detail._price].ToString()) / (MyLib._myGlobal._decimalPhase(__poItemRow[__row][_g.d.ic_trans_detail._stand_value].ToString()) / MyLib._myGlobal._decimalPhase(__poItemRow[__row][_g.d.ic_trans_detail._divide_value].ToString()));

                                        decimal __acceptTotalQuanity = (__acceptOrderQty != 0) ? __acceptOrderQty * ((__unitQtyStandValue / __unitQtyDivideValue)) : 0M; // 
                                        if (__acceptOrderQty == 0)
                                        {
                                            __poOrderQty = 0;
                                        }
                                        // decimal __acceptRealTotalQuanity = (__acceptOrderQty != 0) ? __acceptOrderQty * ((__unitQtyStandValue / __unitQtyDivideValue)) : 0M; // เพิ่ม เอา real qty ไป * ราคาต่อหน่วยย่อย 20160211

                                        if (__packSize > 0)
                                        {
                                            __acceptTotalQuanity = (__acceptOrderQty != 0) ? __acceptOrderQty * __packSize : 0M;
                                            __acceptUnitPrice = MyLib._myGlobal._decimalPhase(__poItemRow[__row][_g.d.ic_trans_detail._price].ToString()) / __packSize;

                                        }

                                        //decimal __sumAmount = MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());


                                        // cut space doc_no
                                        string __docNoExport = __DocNo;

                                        //if (__docNoExport.Length > 10)
                                        {
                                            if (__docNoExport.IndexOf(' ') != -1)
                                            {
                                                __docNoExport = __docNoExport.Replace(" ", string.Empty);
                                            }
                                        }


                                        /* fix for remove optional

                                        string __dataExport = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\"",
                                            __docNoExport, __DocDate, __orderNo.Trim(), __poDate, __DueDate, __creditDay, __supplierCode, __buyerCode,
                                            __itemNo, "", __product_description, MyLib._myGlobal._round(__acceptOrderQty, 3), MyLib._myGlobal._round(__acceptTotalQuanity, 3), MyLib._myGlobal._round(__acceptUnitPrice, 3), MyLib._myGlobal._round(__acceptUnitPrice * __acceptTotalQuanity, 3));
                                        */

                                        /* แก้ไข shipcode
                                        string __dataExport = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\"",
                                            __docNoExport, __DocDate, __orderNo.Trim(), __poDate, "", "", __supplierCode, __buyerCode,
                                            __itemNo, "", "", MyLib._myGlobal._round(__acceptOrderQty, 3), MyLib._myGlobal._round(__acceptTotalQuanity, 3), MyLib._myGlobal._round(__acceptUnitPrice, 3), MyLib._myGlobal._round(__acceptUnitPrice * __acceptTotalQuanity, 3));
                                        */

                                        string __dataExport = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\"",
                                            __docNoExport, __DocDate, __orderNo.Trim(), __poDate, "", "", __supplierCode, ((__delevery_point_trans.Length > 0) ? __delevery_point_trans : __delivery_point),
                                            __itemNo, "", "", MyLib._myGlobal._round(__poOrderQty, 3), MyLib._myGlobal._round(__acceptTotalQuanity, 3), MyLib._myGlobal._round(__acceptUnitPrice, 3), MyLib._myGlobal._round(__acceptUnitPrice * __acceptTotalQuanity, 3));




                                        if (isPreview)
                                        {
                                            this._resultTextbox.AppendText(__dataExport + "\r\n");
                                        }
                                        else
                                        {
                                            FileMode __mode = ((__rowTrans == 0 && __row == 0) && __isOverwrite) ? FileMode.Create : FileMode.Append;
                                            //bool __isOverWriteData = (__row == 0 && __isOverwrite) ? true : false;
                                            using (FileStream fs = new FileStream(__saveFilename, __mode, FileAccess.Write))
                                            using (StreamWriter sw = new StreamWriter(fs))
                                            {
                                                sw.WriteLine(__dataExport);
                                            }
                                        }
                                    }
                                }

                            }
                            break;
                        case 2:
                            {
                                // LOTUS TIMS
                                if (isPreview == false)
                                {
                                    FolderBrowserDialog __open = new FolderBrowserDialog();
                                    if (__open.ShowDialog() == DialogResult.OK)
                                    {
                                        __saveFilename = __open.SelectedPath;
                                    }
                                    else
                                    {
                                        return;
                                    }

                                }


                                for (int __doc = 0; __doc < __transData.Rows.Count; __doc++)
                                {
                                    // new xml filename

                                    string __invoice_number = __transData.Rows[__doc][_g.d.ic_trans._doc_no].ToString();
                                    string __order_number = __transData.Rows[__doc][_g.d.ic_trans._doc_no_po].ToString(); // po number

                                    int __breanchOrderNumber = MyLib._myGlobal._intPhase(__transData.Rows[__doc]["branch_code_number"].ToString());
                                    string __rtv_number = "";
                                    string __rfc_number = "";

                                    string __invoice_type = __transData.Rows[__doc][_g.d.ic_trans._trans_flag].ToString().Equals("44") ? "INV" : "";
                                    string __invoice_date = __transData.Rows[__doc][_g.d.ic_trans._doc_date].ToString();
                                    string __supplier_ID = __externalData.Rows[0][_g.d.edi_external._supplier_ean_code].ToString();
                                    string __delivery_point = __transData.Rows[__doc]["ship_code"].ToString();
                                    string __delevery_point_trans = __transData.Rows[__doc]["ship_code_trans"].ToString();
                                    decimal __PO_discount = MyLib._myGlobal._decimalPhase(__transData.Rows[__doc][_g.d.ic_trans._total_discount].ToString());   // MyLib._myGlobal._decimalPhase(__transData.Rows[__doc]["po_discount"].ToString());
                                    decimal __total_net = MyLib._myGlobal._decimalPhase(__transData.Rows[__doc][_g.d.ic_trans._total_value].ToString());
                                    decimal __total_taxable = MyLib._myGlobal._decimalPhase(__transData.Rows[__doc][_g.d.ic_trans._total_before_vat].ToString());
                                    decimal __total_VAT_header = MyLib._myGlobal._decimalPhase(__transData.Rows[__doc][_g.d.ic_trans._total_vat_value].ToString());
                                    decimal __total_header = MyLib._myGlobal._decimalPhase(__transData.Rows[__doc][_g.d.ic_trans._total_amount].ToString());

                                    string __discount_word = __transData.Rows[__doc][_g.d.ic_trans._discount_word].ToString();

                                    string __xmlFileName = __invoice_number + ".xml";

                                    StringBuilder __exportWord = new StringBuilder(MyLib._myGlobal._xmlHeader);

                                    __exportWord.Append("<invoiceth>");
                                    __exportWord.Append("<in_header>");
                                    __exportWord.Append("<invoice_number>" + __invoice_number + "</invoice_number>");
                                    __exportWord.Append("<order_number>" + __order_number + "</order_number>");
                                    __exportWord.Append("<VAT_branch_number>" + __breanchOrderNumber.ToString("00000") + "</VAT_branch_number>");
                                    __exportWord.Append("<RTV_number>" + __rtv_number + "</RTV_number>");
                                    __exportWord.Append("<RFC_number>" + __rfc_number + "</RFC_number>");
                                    __exportWord.Append("<invoice_type>" + __invoice_type + "</invoice_type>");
                                    __exportWord.Append("<invoice_date>" + __invoice_date + "</invoice_date>");
                                    __exportWord.Append("<supplier_ID>" + __supplier_ID + "</supplier_ID>");
                                    __exportWord.Append("<delivery_point>" + ((__delevery_point_trans.Length > 0) ? __delevery_point_trans : __delivery_point) + "</delivery_point>");
                                    __exportWord.Append("<PO_discount>" + __PO_discount.ToString("#0.00") + "</PO_discount>");
                                    __exportWord.Append("<total_net>" + __total_taxable.ToString("#0.00") + "</total_net>");
                                    __exportWord.Append("<total_taxable>" + __total_taxable.ToString("#0.00") + "</total_taxable>");
                                    __exportWord.Append("<total_VAT>" + __total_VAT_header.ToString("#0.00") + "</total_VAT>");
                                    __exportWord.Append("<total>" + __total_header.ToString("#0.00") + "</total>");
                                    __exportWord.Append("</in_header>");
                                    __exportWord.Append("<in_items>");

                                    DataRow[] __detail = __detailData.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __invoice_number + "\'");

                                    int __rowData = 0;

                                    for (int __row = 0; __row < __detail.Length; __row++)
                                    {
                                        int __lineNumber = __row + 1;
                                        string __TPN = "";
                                        string __SPN = "";
                                        string __EAN = __detail[__row]["edi_barcode"].ToString();
                                        string __product_description = __detail[__row]["edi_itemname"].ToString();
                                        decimal __qty_case = MyLib._myGlobal._decimalPhase(__detail[__row][_g.d.ic_trans_detail._qty].ToString());
                                        decimal __qty_unit = 0M;
                                        decimal __price_case = MyLib._myGlobal._decimalPhase(__detail[__row][_g.d.ic_trans_detail._price].ToString());
                                        decimal __price_unit = 0M;

                                        decimal __multiplied_price = MyLib._myGlobal._decimalPhase(__detail[__row]["qty_price"].ToString());
                                        decimal __VAT_rate = MyLib._myGlobal._decimalPhase(__detail[__row][_g.d.ic_trans._vat_rate].ToString());

                                        if (_g.g._companyProfile._calc_item_price_discount)
                                        {
                                            string __discount = __detail[__row][_g.d.ic_trans_detail._discount].ToString();
                                            decimal __price = MyLib._myGlobal._decimalPhase(__detail[__row][_g.d.ic_trans_detail._price].ToString());
                                            decimal __qty = MyLib._myGlobal._decimalPhase(__detail[__row][_g.d.ic_trans_detail._qty].ToString());
                                            Decimal __newPrice = MyLib._myGlobal._calcAfterDiscount(__discount, __price, _g.g._companyProfile._item_amount_decimal);
                                            __multiplied_price = MyLib._myGlobal._round(__qty * __newPrice, _g.g._companyProfile._item_amount_decimal);
                                            __price_case = __newPrice;
                                        }
                                        // calc
                                        decimal __item_po_discount = MyLib._myGlobal._calcDiscountOnly(__multiplied_price, __discount_word, __multiplied_price, 2)._discountAmount;    // MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._discount_amount].ToString());
                                        decimal __total_price = __multiplied_price - __item_po_discount; // MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString()); // คำนวณแปลก
                                        decimal __total_VAT = MyLib._myGlobal._round(__total_price * (__VAT_rate / 100.0M), _g.g._companyProfile._item_amount_decimal); ; // MyLib._myGlobal._decimalPhase(__detailData.Rows[__row][_g.d.ic_trans_detail._total_vat_value].ToString()); // คำนวณแปลก
                                        decimal __total = __total_price + __total_VAT;

                                        int __is_premium = MyLib._myGlobal._intPhase(__detail[__row][_g.d.ic_trans_detail._is_permium].ToString());

                                        if (__is_premium == 0)
                                        {
                                            __rowData++;

                                            __exportWord.Append("<in_item item_number=\"" + __rowData + "\">");
                                            __exportWord.Append("<TPN>" + __TPN + "</TPN>");
                                            __exportWord.Append("<SPN>" + __SPN + "</SPN>");
                                            __exportWord.Append("<EAN>" + __EAN + "</EAN>");
                                            __exportWord.Append("<product_description>" + __product_description + "</product_description>");
                                            __exportWord.Append("<qty_case>" + __qty_case.ToString("#0.00") + "</qty_case>");
                                            __exportWord.Append("<qty_unit>" + __qty_unit.ToString("#0.00") + "</qty_unit>");
                                            __exportWord.Append("<price_case>" + __price_case.ToString("#0.00") + "</price_case>");
                                            __exportWord.Append("<price_unit>" + __price_unit.ToString("#0.00") + "</price_unit>");
                                            __exportWord.Append("<multiplied_price>" + __multiplied_price.ToString("#0.00") + "</multiplied_price>");
                                            __exportWord.Append("<item_po_discount>" + __item_po_discount.ToString("#0.00") + "</item_po_discount>");
                                            __exportWord.Append("<total_price>" + __total_price.ToString("#0.00") + "</total_price>");
                                            __exportWord.Append("<VAT_rate>" + __VAT_rate.ToString("#0.00") + "</VAT_rate>");
                                            __exportWord.Append("<total_VAT>" + __total_VAT.ToString("#0.00") + "</total_VAT>");
                                            __exportWord.Append("<total>" + __total.ToString("#0.00") + "</total>");

                                            __exportWord.Append("</in_item>");
                                        }
                                    }

                                    __exportWord.Append("</in_items>");
                                    __exportWord.Append("</invoiceth>");

                                    if (isPreview)
                                    {
                                        this._resultTextbox.AppendText(__exportWord.ToString() + "\r\n");


                                        /*XmlDocument __xmlDoc = new XmlDocument();
                                        __xmlDoc.LoadXml(__exportWord.ToString());
                                        __xmlDoc.Normalize();


                                        MemoryStream memory_stream = new MemoryStream();
                                        System.Xml.XmlTextWriter __xmlWriter = new XmlTextWriter(memory_stream, System.Text.Encoding.UTF8);
                                        __xmlDoc.WriteTo(__xmlWriter);
                                        StreamReader stream_reader = new StreamReader(memory_stream);

                                        memory_stream.Seek(0, SeekOrigin.Begin);
                                        _resultTextbox.Text = stream_reader.ReadToEnd();
                                        _resultTextbox.Select(0, 0);
                                        */

                                        /*
                                        MemoryStream memory_stream = new MemoryStream();
                                        XmlDocument __xmlDoc = new XmlDocument();
                                        __xmlDoc.LoadXml(__exportWord.ToString());

                                        System.Xml.XmlTextWriter __xmlWriter = new XmlTextWriter(memory_stream, System.Text.Encoding.UTF8);
                                        __xmlDoc.WriteContentTo(__xmlWriter);


                                        StreamReader stream_reader = new StreamReader(memory_stream);

                                        memory_stream.Seek(0, SeekOrigin.Begin);
                                        _resultTextbox.Text = stream_reader.ReadToEnd();
                                        _resultTextbox.Select(0, 0);
                                        */
                                    }
                                    else
                                    {
                                        //File.WriteAllText()
                                        XmlDocument __xmlDoc = new XmlDocument();
                                        __xmlDoc.LoadXml(__exportWord.ToString());
                                        __xmlDoc.Normalize();
                                        __xmlDoc.Save(__saveFilename + "\\" + __invoice_number + ".xml");
                                    }

                                }


                            }
                            break;

                    }
                }

                MessageBox.Show("Success");
            }

        }
    }
}
