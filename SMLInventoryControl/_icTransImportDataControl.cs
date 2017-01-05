using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransImportDataControl : Form
    {
        private DataTable _dataTable = null;

        public _icTransImportDataControl()
        {
            InitializeComponent();

            //this._screen._table_name = _g.d.ic_trans_detail._table;
            this._screen._maxColumn = 2;
            this._screen._addTextBox(0, 0, 1, 1, _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, 1, 20, 1, true, false, false, false, true, _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code);
            this._screen._addTextBox(0, 1, 1, 1, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense_code, 1, 20, 1, true, false, false, false);
            this._screen._addTextBox(1, 0, 1, 1, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_format_code, 1, 20, 1, true, false, false, false);
            this._screen._textBoxSearch += _screen__textBoxSearch;

            this._splitComboBox.SelectedIndex = 0;
            this._encodingComboBox.SelectedIndex = 0;

        }

        private void _screen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __search = (MyLib._myTextBox)sender;
            if (__search._name.Equals(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code))
            {
                MyLib._searchDataFull __searchSupplier = new MyLib._searchDataFull();
                __searchSupplier._dataList._loadViewFormat(_g.g._search_screen_ap, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchSupplier._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        object __code = __searchSupplier._dataList._gridData._cellGet(__searchSupplier._dataList._gridData._selectRow, _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code);
                        object __name = __searchSupplier._dataList._gridData._cellGet(__searchSupplier._dataList._gridData._selectRow, _g.d.ap_supplier._table + "." + _g.d.ap_supplier._name_1);

                        this._screen._setDataStr(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __code.ToString(), __name.ToString(), true);
                        __searchSupplier.Close();

                    }
                };

                __searchSupplier._searchEnterKeyPress += (s1, e1) =>
                {
                    if (e1 != -1)
                    {
                        object __code = __searchSupplier._dataList._gridData._cellGet(__searchSupplier._dataList._gridData._selectRow, _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code);
                        object __name = __searchSupplier._dataList._gridData._cellGet(__searchSupplier._dataList._gridData._selectRow, _g.d.ap_supplier._table + "." + _g.d.ap_supplier._name_1);

                        this._screen._setDataStr(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __code.ToString(), __name.ToString(), true);
                        __searchSupplier.Close();
                    }
                };
                MyLib._myGlobal._startSearchBox(__search, "ค้นหาเจ้าหนี้", __searchSupplier);
            }
            else if (__search._name.Equals(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense_code))
            {
                MyLib._searchDataFull __searchExpense = new MyLib._searchDataFull();
                __searchExpense._dataList._loadViewFormat(_g.g._search_screen_expenses_list, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchExpense._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        object __code = __searchExpense._dataList._gridData._cellGet(__searchExpense._dataList._gridData._selectRow, _g.d.erp_expenses_list._table + "." + _g.d.erp_expenses_list._code);
                        object __name = __searchExpense._dataList._gridData._cellGet(__searchExpense._dataList._gridData._selectRow, _g.d.erp_expenses_list._table + "." + _g.d.erp_expenses_list._name_1);

                        this._screen._setDataStr(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense_code, __code.ToString(), __name.ToString(), true);
                        __searchExpense.Close();

                    }
                };

                __searchExpense._searchEnterKeyPress += (s1, e1) =>
                {
                    if (e1 != -1)
                    {
                        object __code = __searchExpense._dataList._gridData._cellGet(__searchExpense._dataList._gridData._selectRow, _g.d.erp_expenses_list._table + "." + _g.d.erp_expenses_list._code);
                        object __name = __searchExpense._dataList._gridData._cellGet(__searchExpense._dataList._gridData._selectRow, _g.d.erp_expenses_list._table + "." + _g.d.erp_expenses_list._name_1);

                        this._screen._setDataStr(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense_code, __code.ToString(), __name.ToString(), true);
                        __searchExpense.Close();
                    }
                };
                MyLib._myGlobal._startSearchBox(__search, "ค้นหาค่าใช้จ่าย", __searchExpense);
            }
            else if (__search._name.Equals(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_format_code))
            {
                MyLib._searchDataFull __searchDocFormat = new MyLib._searchDataFull();
                __searchDocFormat._dataList._loadViewFormat(_g.g._screen_erp_doc_format, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchDocFormat._dataList._extraWhere2 = _g.d.erp_doc_format._screen_code + "=\'COB\'";
                __searchDocFormat._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        object __code = __searchDocFormat._dataList._gridData._cellGet(__searchDocFormat._dataList._gridData._selectRow, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code);

                        this._screen._setDataStr(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_format_code, __code.ToString());
                        __searchDocFormat.Close();

                    }
                };

                __searchDocFormat._searchEnterKeyPress += (s1, e1) =>
                {
                    if (e1 != -1)
                    {
                        object __code = __searchDocFormat._dataList._gridData._cellGet(__searchDocFormat._dataList._gridData._selectRow, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code);

                        this._screen._setDataStr(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_format_code, __code.ToString());
                        __searchDocFormat.Close();
                    }
                };
                MyLib._myGlobal._startSearchBox(__search, "ค้นหาประเภทเอกสาร", __searchDocFormat);
            }
        }

        void _getData(StreamReader reader)
        {
            string __input = null;
            Boolean __first = false;
            while ((__input = reader.ReadLine()) != null)
            {
                string[] __split = __input.Split(this._splitComboBox.SelectedIndex == 1 ? ',' : '\t');
                if (__first == false)
                {
                    __first = true;
                    this._dataTable = new DataTable();
                    for (int __loop = 0; __loop < __split.Length; __loop++)
                    {
                        string __name = "C" + __loop.ToString();
                        DataColumn __column = new DataColumn(__name);
                        this._dataTable.Columns.Add(__column);
                    }
                }
                //
                try
                {
                    this._dataTable.Rows.Add(__split);
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                    break;
                }
            }
            this._dataGridView.DataSource = this._dataTable;
            //
            /*
            DataGridViewTextBoxColumn __column1 = new DataGridViewTextBoxColumn();
            __column1.HeaderText = "Target Field";
            __column1.ValueType = typeof(String);
            __column1.ReadOnly = true;
            //
            DataGridViewTextBoxColumn __column2 = new DataGridViewTextBoxColumn();
            __column2.HeaderText = "Source Field";
            __column2.ValueType = typeof(String);
            //
            this._mapFieldView.Columns.Clear();
            this._mapFieldView.Columns.Add(__column1);
            this._mapFieldView.Columns.Add(__column2);
            //
            this._mapFieldView.Rows.Clear();
            for (int __column = 0; __column < this._columnList.Count; __column++)
            {
                MyLib._myGrid._columnType __myColumn = (MyLib._myGrid._columnType)_columnList[__column];
                if (__myColumn._isHide == false)
                {
                    this._mapFieldView.Rows.Add(__myColumn._name, "C" + __column.ToString());
                }
            }
            */
        }

        private void _importButton_Click(object sender, EventArgs e)
        {

            string __empty = this._screen._checkEmtryField();
            if (__empty.Length > 0)
            {
                MessageBox.Show("กรุณาป้อน \n" + __empty, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    // start import
                    StringBuilder __query = new StringBuilder();
                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");

                    this._screen._saveLastControl();

                    string __custCode = this._screen._getDataStr(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                    string __expenseCode = this._screen._getDataStr(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense_code);
                    string __docFormatCode = this._screen._getDataStr(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_format_code);

                    DateTime __processDate = DateTime.Now;

                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                    string __getDataQuery = "select " +
                        " (select name_1 from ap_supplier where ap_supplier.code = \'" + __custCode + "\') as name_1," +
                        " (select tax_id from ap_supplier_detail where ap_supplier_detail.ap_code = \'" + __custCode + "\') as tax_id," +
                        " (select branch_type from ap_supplier_detail where ap_supplier_detail.ap_code = \'" + __custCode + "\') as branch_type," +
                        " (select branch_code from ap_supplier_detail where ap_supplier_detail.ap_code = \'" + __custCode + "\') as branch_code," +
                        " (select name_1 from erp_expenses_list where erp_expenses_list.code = \'" + __expenseCode + "\') as expense_name ";

                    DataTable __resultData = __myFrameWork._queryShort(__getDataQuery).Tables[0];

                    string __apName = __resultData.Rows[0]["name_1"].ToString();
                    int __branchType = MyLib._myGlobal._intPhase(__resultData.Rows[0]["branch_type"].ToString());
                    string __branchTypeCode = __resultData.Rows[0]["branch_code"].ToString();
                    string __taxID = __resultData.Rows[0]["tax_id"].ToString();
                    string __expenseName = __resultData.Rows[0]["expense_name"].ToString();


                    for (int __row = 0; __row < this._dataTable.Rows.Count; __row++)
                    {


                        DateTime __docDate = MyLib._myGlobal._convertDate(this._dataTable.Rows[__row][1].ToString(), 0); // 1
                        string __docReference = this._dataTable.Rows[__row][3].ToString(); // 3
                        string __docNumber = this._dataTable.Rows[__row][0].ToString() + this._dataTable.Rows[__row][5].ToString() + this._dataTable.Rows[__row][4].ToString(); // 4
                        string __businessPlace = this._dataTable.Rows[__row][7].ToString(); // 7
                        DateTime __dueDate = MyLib._myGlobal._convertDate(this._dataTable.Rows[__row][8].ToString(), 0); //8
                        decimal __amount = MyLib._myGlobal._decimalPhase(this._dataTable.Rows[__row][15].ToString()); // Amount LC
                        string __branchCode = (this._dataTable.Columns.Count >= 19) ? this._dataTable.Rows[__row][18].ToString() : ""; // 18

                        int __taxType = (this._dataTable.Columns.Count >= 20) ? MyLib._myGlobal._intPhase(this._dataTable.Rows[__row][19].ToString()) : 1; // 16 default รวมใน

                        string __branchCodeTrans = (this._dataTable.Columns.Count >= 21) ? this._dataTable.Rows[__row][20].ToString() : ""; // 15

                        string __taxDocNo = __businessPlace + "/" + __docReference;
                        decimal __vatRate = _g.g._companyProfile._vat_rate;

                        decimal __exceptVat  = MyLib._myGlobal._decimalPhase(this._dataTable.Rows[__row][12].ToString());
                        decimal __beforeVat  = MyLib._myGlobal._decimalPhase(this._dataTable.Rows[__row][13].ToString());
                        decimal __totalvalue = __exceptVat + __beforeVat;
                        decimal __vatValue = MyLib._myGlobal._decimalPhase(this._dataTable.Rows[__row][14].ToString());

                        /*
                        if (__taxType == 1)
                        {
                            __beforeVat = MyLib._myGlobal._round((__amount * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                            __vatValue = MyLib._myGlobal._round(__amount - __beforeVat, _g.g._companyProfile._item_amount_decimal);

                            __totalvalue = __beforeVat;
                        }
                        else if (__taxType == 0)
                        {
                            __beforeVat = __amount;
                            __vatValue = MyLib._myGlobal._round(__beforeVat * (__vatRate / 100.0M), _g.g._companyProfile._item_amount_decimal);
                            __amount = __beforeVat + __vatValue;                            
                        }*/


                        double __creditDay = (__dueDate - __docDate).TotalDays;

                        int __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น);
                        int __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น);



                        // ic_trans
                        string __fieldList = MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans._doc_format_code,
                            _g.d.ic_trans._cust_code,
                            _g.d.ic_trans._trans_flag,
                            _g.d.ic_trans._trans_type,
                            _g.d.ic_trans._doc_no,
                            _g.d.ic_trans._doc_date,
                            _g.d.ic_trans._doc_time,
                            _g.d.ic_trans._tax_doc_no,
                            _g.d.ic_trans._tax_doc_date,

                            _g.d.ic_trans._credit_day,
                            _g.d.ic_trans._credit_date,

                            _g.d.ic_trans._vat_rate,
                            _g.d.ic_trans._total_value,
                            _g.d.ic_trans._total_before_vat,
                            _g.d.ic_trans._total_except_vat,

                            _g.d.ic_trans._total_vat_value,
                            _g.d.ic_trans._total_amount,

                            _g.d.ic_trans._branch_code,
                            _g.d.ic_trans._vat_type,

                            _g.d.ic_trans._creator_code,
                            _g.d.ic_trans._create_datetime
                            );

                        string __valueList = MyLib._myGlobal._fieldAndComma(
                            "\'" + __docFormatCode + "\'",
                            "\'" + __custCode + "\'",
                            __transFlag.ToString(),
                            __transType.ToString(),
                            "\'" + __docNumber + "\'",
                            "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'",
                            "\'" + "08:00" + "\'",
                            "\'" + __taxDocNo + "\'",
                            "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'",

                            __creditDay.ToString(),
                            "\'" + MyLib._myGlobal._convertDateToQuery(__dueDate) + "\'",

                            __vatRate.ToString(),
                            __totalvalue.ToString(),
                            __beforeVat.ToString(),
                            __exceptVat.ToString(),

                            __vatValue.ToString(),
                            __amount.ToString(),

                            "\'" + __branchCodeTrans + "\'",
                            __taxType.ToString(),

                            "\'" + MyLib._myGlobal._userCode + "\'",
                            "\'" + MyLib._myGlobal._convertDateTimeToQuery(__processDate) + "\'"
                            );

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table + "(" + __fieldList + ") values (" + __valueList + ")"));

                        // ic_trans_detail

                        __fieldList = MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans_detail._trans_flag,
                            _g.d.ic_trans_detail._trans_type,
                            _g.d.ic_trans_detail._calc_flag,

                            _g.d.ic_trans_detail._doc_date,
                            _g.d.ic_trans_detail._doc_no,
                            _g.d.ic_trans_detail._doc_time,
                            _g.d.ic_trans_detail._cust_code,
                            _g.d.ic_trans_detail._item_code,
                            _g.d.ic_trans_detail._item_name,

                            _g.d.ic_trans_detail._sum_amount,
                            _g.d.ic_trans_detail._sum_amount_exclude_vat
                            );

                        __valueList = MyLib._myGlobal._fieldAndComma(
                            __transFlag.ToString(),
                            __transType.ToString(),
                            "1",

                            "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'",
                            "\'" + __docNumber + "\'",
                            "\'" + "08:00" + "\'",
                            "\'" + __custCode + "\'",
                            "\'" + __expenseCode + "\'",
                            "\'" + __expenseName + "\'",

                            __totalvalue.ToString(),
                            ((__taxType == 1) ? __amount.ToString() : __totalvalue.ToString())
                            );
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail._table + "(" + __fieldList + ") values (" + __valueList + ")"));

                        // vat
                        __fieldList = MyLib._myGlobal._fieldAndComma(
                           _g.d.gl_journal_vat_buy._trans_flag,
                           _g.d.gl_journal_vat_buy._trans_type,

                           _g.d.gl_journal_vat_buy._doc_date,
                           _g.d.gl_journal_vat_buy._doc_no,
                           _g.d.gl_journal_vat_buy._vat_date,
                           _g.d.gl_journal_vat_buy._vat_doc_no,

                           _g.d.gl_journal_vat_buy._vat_base_amount,
                           _g.d.gl_journal_vat_buy._vat_rate,
                           _g.d.gl_journal_vat_buy._vat_total_amount,
                           _g.d.gl_journal_vat_buy._vat_amount,
                           _g.d.gl_journal_vat_buy._vat_except_amount_1,
                           _g.d.gl_journal_vat_buy._vat_effective_period,
                           _g.d.gl_journal_vat_buy._vat_effective_year,

                           _g.d.gl_journal_vat_buy._ap_code,
                           _g.d.gl_journal_vat_buy._ap_name,
                           _g.d.gl_journal_vat_buy._tax_no,
                           _g.d.gl_journal_vat_buy._branch_type,
                           _g.d.gl_journal_vat_buy._branch_code,
                           _g.d.gl_journal_vat_buy._vat_calc,
                           _g.d.gl_journal_vat_buy._vat_type
                           );

                        __valueList = MyLib._myGlobal._fieldAndComma(
                            __transFlag.ToString(),
                            __transType.ToString(),

                            "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'",
                            "\'" + __docNumber + "\'",
                            "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'",
                            "\'" + __taxDocNo + "\'",

                            __beforeVat.ToString(),
                            __vatRate.ToString(),
                            __amount.ToString(),
                            __vatValue.ToString(),
                            __exceptVat.ToString(),
                            __docDate.Month.ToString(),
                            (__docDate.Year + 543).ToString(),

                            "\'" + __custCode + "\'",
                            "\'" + __apName + "\'",
                            "\'" + __taxID + "\'",
                            (__branchCode.Equals("00000") ? "0" : "1"), //__branchType.ToString(),
                            "\'" + __branchCode + "\'", //  __branchTypeCode 
                            "1",
                            "0"
                            );

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_vat_buy._table + "(" + __fieldList + ") values (" + __valueList + ")"));


                    }

                    __query.Append("</node>");

                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                    if (__result.Length != 0)
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Success");

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void _fromClipboardButton_Click(object sender, EventArgs e)
        {
            try
            {
                IDataObject __data = Clipboard.GetDataObject();
                string __text = __data.GetData(DataFormats.Text).ToString();
                //
                byte[] _byteArray = Encoding.UTF8.GetBytes(__text);
                MemoryStream __stream = new MemoryStream(_byteArray);
                StreamReader __reader = new StreamReader(__stream);
                this._getData(__reader);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _openTextFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                this._dataTable = null;
                OpenFileDialog __file = new OpenFileDialog();
                __file.Title = "Select Text File";
                __file.Multiselect = false;
                __file.Filter = "All Supported File (*.txt,*.csv) | *.txt;*.csv";
                if (__file.ShowDialog() == DialogResult.OK)
                {
                    System.Text.Encoding __encoder = System.Text.Encoding.GetEncoding(874);
                    if (this._encodingComboBox.SelectedIndex == 1)
                    {
                        __encoder = System.Text.Encoding.UTF8;
                    }
                    StreamReader __re = new StreamReader(__file.FileName.ToString(), __encoder);
                    this._getData(__re);
                    __re.Close();
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }

        }
    }
}
