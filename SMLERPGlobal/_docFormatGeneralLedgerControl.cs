using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace _g
{
    public partial class _docFormatGeneralLedgerControl : UserControl
    {
        public delegate string ScreenCodeEventHandler();
        public event ScreenCodeEventHandler _screenCode;

        _searchChartOfAccountDialog _chartOfAccountScreen = new _searchChartOfAccountDialog();
        string _fieldName = "";

        public _docFormatGeneralLedgerControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._grid._table_name = _g.d.erp_doc_format_gl._table;
            this._grid._addColumn(_g.d.erp_doc_format_gl._condition_number, 2, 20, 5, true, false);
            this._grid._addColumn(_g.d.erp_doc_format_gl._condition_name, 1, 20, 35, true, false);
            this._grid._addColumn(_g.d.erp_doc_format_gl._condition_case, 1, 20, 10, true, false);
            this._grid._addColumn(_g.d.erp_doc_format_gl._account_code_debit, 1, 15, 10, true, false, true, true);
            this._grid._addColumn(_g.d.erp_doc_format_gl._account_code_credit, 1, 15, 10, true, false, true, true);
            this._grid._addColumn(_g.d.erp_doc_format_gl._account_name2, 1, 20, 15, false, false, true);
            this._grid._addColumn(_g.d.erp_doc_format_gl._account_name, 1, 20, 15);
            this._grid._addColumn(_g.d.erp_doc_format_gl._code_compare, 1, 20, 5, false, true);
            this._grid._clickSearchButton += new MyLib.SearchEventHandler(_grid__clickSearchButton);
            this._grid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_grid__alterCellUpdate);
            this._chartOfAccountScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._chartOfAccountScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_chartOfAccountScreen__searchEnterKeyPress);
        }

        void _chartOfAccountScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._chartOfAccountScreen.Close();
            this._setAccountCode(sender._cellGet(sender._selectRow, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code).ToString());
        }

        void _setAccountCode(string code)
        {
            if (this._fieldName.Equals(_g.d.erp_doc_format_gl._account_code_debit))
            {
                this._grid._cellUpdate(this._grid._selectRow, _g.d.erp_doc_format_gl._account_code_credit, "", false);
                this._grid._cellUpdate(this._grid._selectRow, _g.d.erp_doc_format_gl._account_code_debit, code, true);
            }
            else
            {
                this._grid._cellUpdate(this._grid._selectRow, _g.d.erp_doc_format_gl._account_code_debit, "", false);
                this._grid._cellUpdate(this._grid._selectRow, _g.d.erp_doc_format_gl._account_code_credit, code, true);
            }
        }

        void _searchAll(string name, int row)
        {
            this._setAccountCode((string)this._chartOfAccountScreen._dataList._gridData._cellGet(row, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code));
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._chartOfAccountScreen.Close();
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, e._row);
        }

        void _grid__alterCellUpdate(object sender, int row, int column)
        {
            MyLib._myGrid __sender = (MyLib._myGrid)sender;
            string __debitCode = __sender._cellGet(__sender._selectRow, _g.d.erp_doc_format_gl._account_code_debit).ToString().Trim();
            string __creditCode = __sender._cellGet(__sender._selectRow, _g.d.erp_doc_format_gl._account_code_credit).ToString().Trim();
            string __accountCode = (__debitCode.Length == 0) ? __creditCode : __debitCode;
            //
            MyLib._myFrameWork __frameWork = new MyLib._myFrameWork();
            string __query = "select  " + _g.d.gl_chart_of_account._name_1 + "," + _g.d.gl_chart_of_account._name_2 + "," + _g.d.gl_chart_of_account._account_level + "," + _g.d.gl_chart_of_account._status + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._code + "=\'" + __accountCode + "\'";
            DataTable __getData = __frameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
            this._grid._cellUpdate(this._grid._selectRow, _g.d.erp_doc_format_gl._account_name2, (__getData.Rows.Count == 0) ? "" : __getData.Rows[0][_g.d.gl_chart_of_account._name_1].ToString(), false);
        }

        void _grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.CompareTo(_g.d.erp_doc_format_gl._account_code_debit) == 0)
            {
                this._fieldName = _g.d.erp_doc_format_gl._account_code_debit;
                this._chartOfAccountScreen.ShowDialog();
            }
            if (e._columnName.CompareTo(_g.d.erp_doc_format_gl._account_code_credit) == 0)
            {
                this._fieldName = _g.d.erp_doc_format_gl._account_code_credit;
                this._chartOfAccountScreen.ShowDialog();
            }
        }

        private void _loadFormatButton_Click(object sender, EventArgs e)
        {
            try
            {
                string __extraWhere = "";
                if (_g.g._companyProfile._inventory_gl_post == 0)
                {
                    // perpetual
                    __extraWhere = " and action_code not in (101, 103) ";
                }

                List<_oldValueClass> __oldValue = new List<_oldValueClass>();
                string __query = "select * from gl_doc_format where upper(screen_code) like \'%[" + this._screenCode().ToUpper() + "]%\' " + __extraWhere + " order by line_number";
                MyLib._myFrameWork __smlWebService = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                DataSet __data = __smlWebService._query(MyLib._myGlobal._masterDatabaseName, __query);
                if (__data.Tables.Count > 0)
                {
                    DialogResult __clear = MessageBox.Show(MyLib._myGlobal._resource("ต้องการล้างของเก่าหรือไม่"), "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    if (__clear == DialogResult.No)
                    {
                        for (int __row = 0; __row < this._grid._rowData.Count; __row++)
                        {
                            _oldValueClass __tempData = new _oldValueClass();
                            __tempData._accountCodeDebit = this._grid._cellGet(__row, _g.d.erp_doc_format_gl._account_code_debit).ToString();
                            __tempData._accountCodeCredit = this._grid._cellGet(__row, _g.d.erp_doc_format_gl._account_code_credit).ToString();
                            __tempData._accountName = this._grid._cellGet(__row, _g.d.erp_doc_format_gl._account_name2).ToString();
                            __tempData._accountDescription = this._grid._cellGet(__row, _g.d.erp_doc_format_gl._account_name).ToString();
                            __tempData._codeCompare = this._grid._cellGet(__row, _g.d.erp_doc_format_gl._code_compare).ToString();
                            __oldValue.Add(__tempData);
                        }
                    }
                    this._grid._clear();
                    for (int __row = 0; __row < __data.Tables[0].Rows.Count; __row++)
                    {
                        int __addr = this._grid._addRow();
                        this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._condition_case, __data.Tables[0].Rows[__row]["condition"].ToString(), false);
                        this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._condition_number, (int)MyLib._myGlobal._decimalPhase(__data.Tables[0].Rows[__row]["action_code"].ToString()), false);
                        this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._condition_name, __data.Tables[0].Rows[__row]["account_name"].ToString(), false);
                        if (__clear == DialogResult.Yes)
                        {
                            this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._account_code_debit, __data.Tables[0].Rows[__row]["account_code_debit"].ToString(), false);
                            this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._account_code_credit, __data.Tables[0].Rows[__row]["account_code_credit"].ToString(), false);
                            this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._account_name2, __data.Tables[0].Rows[__row]["account_name"].ToString(), false);
                        }
                        string __codeCompare = __data.Tables[0].Rows[__row]["guid_compare"].ToString();
                        this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._code_compare, __codeCompare, false);
                        //
                        for (int __find = 0; __find < __oldValue.Count; __find++)
                        {
                            if (__oldValue[__find]._codeCompare.Equals(__codeCompare))
                            {
                                this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._account_code_debit, __oldValue[__find]._accountCodeDebit, false);
                                this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._account_code_credit, __oldValue[__find]._accountCodeCredit, false);
                                this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._account_name2, __oldValue[__find]._accountName, false);
                                this._grid._cellUpdate(__addr, _g.d.erp_doc_format_gl._account_name, __oldValue[__find]._accountDescription, false);
                                break;
                            }
                        }
                    }
                    this._grid.Invalidate();
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่พบข้อมูลผังบัญชี : ") + this._screenCode().ToUpper());
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }

    public class _oldValueClass
    {
        public string _name = "";
        public string _accountCodeDebit = "";
        public string _accountCodeCredit = "";
        public string _accountName = "";
        public string _accountDescription = "";
        public string _codeCompare = "";
    }
}
