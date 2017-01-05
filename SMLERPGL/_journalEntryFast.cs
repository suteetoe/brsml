using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL
{
    public partial class _journalEntryFast : UserControl
    {
        /// <summary>
        /// Column ที่เป็นภาษีมูลค่าเพิ่ม
        /// </summary>
        private int _taxColumn = -1;

        public _journalEntryFast()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || 
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                _g.g._checkOpenPeriod();
                this._myTabControl1._getResource();
            }
            //
            this._autoDateButton._iconNumber = 0;
            this._autoDateButton.Image = imageList1.Images[this._autoDateButton._iconNumber];
            this._autoCalcButton._iconNumber = 0;
            this._autoCalcButton.Image = imageList1.Images[this._autoCalcButton._iconNumber];
            this._autoRunningNumberButton._iconNumber = 0;
            this._autoRunningNumberButton.Image = imageList1.Images[this._autoRunningNumberButton._iconNumber];
            //
            _screenTop._reLoad();
            this._screenTop.screenCode = "JV";
            this._screenTop._textBoxChanged += _screenTop__textBoxChanged;
            _recurring1._journalInputScreen = this._screenTop;
            _recurring1._journalInputDetail = this._glTemplateDetail;
            //
            this._dataGrid._afterAddRow += new MyLib.AfterAddRowEventHandler(_dataGrid__afterAddRow);
            this._dataGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_dataGrid__alterCellUpdate);
            this._dataGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_dataGrid__beforeDisplayRow);
        }

        void _screenTop__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.gl_journal._doc_date))
            {
                _g.g._accountPeriodClass __accountPeriod = _g.g._accountPeriodClassFind(this._screenTop._getDataDate(_g.d.gl_journal._doc_date));
                if (__accountPeriod != null)
                {
                    this._screenTop._setDataStr(_g.d.gl_journal._period_number, __accountPeriod._number.ToString());
                    this._screenTop._setDataStr(_g.d.gl_journal._account_year, __accountPeriod._year.ToString());
                }
                this._screenTop.Invalidate();
            }

        }

        int _checkRow(MyLib._myGrid sender, int row)
        {
            decimal __getTotal = (decimal)sender._cellGet(row, "ยอดรวม");
            if (__getTotal != 0)
            {
                decimal __sum = 0;
                decimal __sumDebit = 0;
                for (int __column = 3; __column < sender._columnList.Count; __column++)
                {
                    decimal __getValue = (decimal)sender._cellGet(row, __column);
                    __sum += __getValue;
                    if (__getValue > 0)
                    {
                        __sumDebit += __getValue;
                    }
                }
                if (__sum != 0)
                {
                    return 1;
                }
                else
                    if (__sumDebit != __getTotal)
                    {
                        return 2;
                    }
            }
            return (0);
        }

        MyLib.BeforeDisplayRowReturn _dataGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            int __checkRow = _checkRow(sender, row);
            if (__checkRow == 1)
            {
                senderRow.newColor = Color.Red;
            }
            if (__checkRow == 2)
            {
                senderRow.newColor = Color.Blue;
            }
            return senderRow;
        }

        void _dataGrid__alterCellUpdate(object sender, int row, int column)
        {
            if (this._autoCalcButton._iconNumber == 0)
            {
                int __getColumnTotal = this._dataGrid._findColumnByName("ยอดรวม");
                if (column == __getColumnTotal)
                {
                    decimal __getValue = (decimal)this._dataGrid._cellGet(row, column);
                    if (this._dataGrid._columnList.Count == 5)
                    {
                        this._dataGrid._cellUpdate(row, 3, __getValue, false);
                        this._dataGrid._cellUpdate(row, 4, __getValue * -1, false);
                    }
                    if (this._dataGrid._columnList.Count == 6 && _taxColumn != -1)
                    {
                        decimal __tax = (decimal)Math.Round(__getValue * 7 / 107, 2);
                        decimal __beforeTax = __getValue - __tax;
                        if (_taxColumn == 4)
                        {
                            this._dataGrid._cellUpdate(row, 3, __beforeTax, false);
                            this._dataGrid._cellUpdate(row, 4, __tax, false);
                            this._dataGrid._cellUpdate(row, 5, __getValue * -1, false);
                        }
                        if (_taxColumn == 5)
                        {
                            this._dataGrid._cellUpdate(row, 3, __getValue, false);
                            this._dataGrid._cellUpdate(row, 4, __beforeTax * -1, false);
                            this._dataGrid._cellUpdate(row, 5, __tax * -1, false);
                        }
                    }
                }
            }
        }

        void _dataGrid__afterAddRow(object sender, int row)
        {
            if (row > 0)
            {
                if (this._autoDateButton._iconNumber == 0)
                {
                    DateTime __getDate = (DateTime)this._dataGrid._cellGet(row - 1, _g.d.gl_journal._doc_date);
                    this._dataGrid._cellUpdate(row, _g.d.gl_journal._doc_date, __getDate, false);
                }
                if (this._autoRunningNumberButton._iconNumber == 0)
                {
                    string __getNumber = this._dataGrid._cellGet(row - 1, _g.d.gl_journal._doc_no).ToString();
                    this._dataGrid._cellUpdate(row, _g.d.gl_journal._doc_no, MyLib._myGlobal._autoRunningNumberStyleRightToLeft(__getNumber), false);
                }
            }
        }

        private void _buttonCreateDataGrid_Click(object sender, EventArgs e)
        {
            this._screenTop.Enabled = false;
            this._glTemplateDetail.Enabled = false;
            this._recurring1.Enabled = false;
            this._createDataGridButton.Enabled = false;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._dataGrid._table_name = _g.d.gl_journal._table;
            this._dataGrid._columnList.Clear();
            this._dataGrid._clear();
            this._dataGrid._width_by_persent = true;
            this._dataGrid._addColumn(_g.d.gl_journal._doc_date, 4, 0, 10);
            this._dataGrid._addColumn(_g.d.gl_journal._doc_no, 1, 0, 10);
            this._dataGrid._getResource = false;
            this._dataGrid._addColumn("ยอดรวม", 3, 0, 10, true, false, true, false, __formatNumber);
            int __count = 0;
            for (int __row = 0; __row < _glTemplateDetail._glDetailGrid._rowData.Count; __row++)
            {
                string __getAccountCode = _glTemplateDetail._glDetailGrid._cellGet(__row, 0).ToString();
                if (__getAccountCode.Length > 0)
                {
                    __count++;
                }
            }
            if (__count > 0)
            {
                for (int __row = 0; __row < _glTemplateDetail._glDetailGrid._rowData.Count; __row++)
                {
                    string __getAccountCode = _glTemplateDetail._glDetailGrid._cellGet(__row, _g.d.gl_journal_detail._account_code).ToString();
                    if (__getAccountCode.Length > 0)
                    {
                        if (_taxColumn == -1)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            string __query = "select " + _g.d.gl_chart_of_account._tax_type + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._code + "=\'" + __getAccountCode + "\'";
                            DataSet __getTax = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                            try
                            {
                                if (__getTax.Tables[0].Rows.Count > 0)
                                {
                                    int __getTaxType = MyLib._myGlobal._intPhase(__getTax.Tables[0].Rows[0].ItemArray[0].ToString());
                                    if (__getTaxType == 1 || __getTaxType == 2)
                                    {
                                        _taxColumn = this._dataGrid._columnList.Count;
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                        string __getAccountName = _glTemplateDetail._glDetailGrid._cellGet(__row, _g.d.gl_journal_detail._account_name).ToString();
                        this._dataGrid._addColumn(__getAccountName, 3, 0, (70 / __count), true, false, true, false, __formatNumber);
                    }
                }
            }
            this._dataGrid._getResource = true;
            if (_taxColumn != -1)
            {
                this._dataGrid._setColumnBackground(_taxColumn, Color.AliceBlue);
            }
            else
            {
                this._autoCalcButton.Enabled = false;
            }
            this._dataGrid._setColumnBackground("ยอดรวม", Color.AliceBlue);
            _dataGrid.Invalidate();
        }

        private void _autoDateButton_Click(object sender, EventArgs e)
        {
            this._autoDateButton._iconNumber = (this._autoDateButton._iconNumber == 0) ? 1 : 0;
            this._autoDateButton.Image = imageList1.Images[this._autoDateButton._iconNumber];
        }

        private void _autoRunningNumberButton_Click(object sender, EventArgs e)
        {
            this._autoRunningNumberButton._iconNumber = (this._autoRunningNumberButton._iconNumber == 0) ? 1 : 0;
            this._autoRunningNumberButton.Image = imageList1.Images[this._autoRunningNumberButton._iconNumber];
        }

        private void _autoCalcButton_Click(object sender, EventArgs e)
        {
            this._autoCalcButton._iconNumber = (this._autoCalcButton._iconNumber == 0) ? 1 : 0;
            this._autoCalcButton.Image = imageList1.Images[this._autoCalcButton._iconNumber];
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._dataGrid._removeLastControl();
            bool __nextStep = true;
            for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
            {
                int __checkRow = _checkRow(this._dataGrid, __row);
                if (__checkRow == 1)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("มีรายการที่ยอดไม่ตรง (ดูที่บรรทัดสีแดง)"), "Warning");
                    __nextStep = false;
                    break;
                }
            }
            if (__nextStep)
            {
                if (MessageBox.Show(MyLib._myGlobal._resource("ต้องการสร้างเป็นข้อมูลรายวันจริงหรือไม่"), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    StringBuilder __query = new StringBuilder();
                    __query.Append(MyLib._myGlobal._xmlHeader);
                    __query.Append("<node>");
                    for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
                    {
                        string __getDocDate = MyLib._myGlobal._convertDateToQuery((DateTime)this._dataGrid._cellGet(__row, 0));
                        string __getDocNo = this._dataGrid._cellGet(__row, 1).ToString();
                        string __getBookCode = this._screenTop._getDataStr(_g.d.gl_journal._book_code);
                        string __getDesc = this._screenTop._getDataStr(_g.d.gl_journal._description);
                        double __getTotalValue = (double)this._dataGrid._cellGet(__row, 2);

                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || 
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            if (_g.g._checkOpenPeriod((DateTime)this._dataGrid._cellGet(__row, 0)) == false)
                            {
                                return;
                            }
                        }

                        if (__getTotalValue != 0)
                        {
                            string __queryStr = "insert into " + _g.d.gl_journal._table + " (" + _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._book_code + "," + _g.d.gl_journal._description + "," + _g.d.gl_journal._debit + "," + _g.d.gl_journal._credit+ ") values (\'" +
                               __getDocDate + "\',\'" + __getDocNo + "\',\'" + __getBookCode + "\',\'" + __getDesc + "\',1," + __getTotalValue.ToString() + "," + __getTotalValue.ToString() + ")";
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr));
                            int __lineNumber = 0;
                            for (int __rowDetail = 0; __rowDetail < this._glTemplateDetail._glDetailGrid._rowData.Count; __rowDetail++)
                            {
                                try
                                {
                                    string __getAccountCode = this._glTemplateDetail._glDetailGrid._cellGet(__rowDetail, _g.d.gl_journal_detail._account_code).ToString();
                                    string __getAccountName = this._glTemplateDetail._glDetailGrid._cellGet(__rowDetail, _g.d.gl_journal_detail._account_name).ToString();
                                    string __getAccountDesc = this._glTemplateDetail._glDetailGrid._cellGet(__rowDetail, _g.d.gl_journal_detail._description).ToString();
                                    double __getValue = (double)this._dataGrid._cellGet(__row, __rowDetail + 3);
                                    double __getDebit = 0;
                                    double __getCredit = 0;
                                    if (__getValue != 0)
                                    {
                                        if (__getValue >= 0)
                                        {
                                            // Debit
                                            __getDebit = __getValue;
                                        }
                                        else
                                        {
                                            // Credit
                                            __getCredit = __getValue*-1;
                                        }
                                        __lineNumber++;
                                        __queryStr = "insert into " + _g.d.gl_journal_detail._table + "  (" + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._book_code + "," +
                                            _g.d.gl_journal_detail._line_number + "," + _g.d.gl_journal_detail._account_code + "," + _g.d.gl_journal_detail._account_name + "," + _g.d.gl_journal_detail._description + "," + _g.d.gl_journal_detail._debit + "," + _g.d.gl_journal_detail._credit + ") values (\'" +
                                       __getDocDate + "\',\'" + __getDocNo + "\',\'" + __getBookCode + "\'," + (__lineNumber.ToString()) + ",1,\'" + __getAccountCode + "\',\'" + __getAccountName + "\',\'" + __getAccountDesc + "\'," + __getDebit.ToString() + "," + __getCredit.ToString() + ")";
                                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStr));
                                    }
                                }
                                catch
                                {
                                }
                            }
                        } 
                    }
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal._table + " set " + _g.d.gl_journal._is_pass + "=0 where " + _g.d.gl_journal._is_pass + " is null"));
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal._is_pass + "=0 where " + _g.d.gl_journal_detail._is_pass + " is null"));
                    __query.Append("</node>");
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                    if (__result.Length != 0)
                    {
                        MessageBox.Show(__result, "Warning");
                    }
                    else
                    {
                        this._dataGrid._clear();
                    }
                }
            }
        }
    }
}
