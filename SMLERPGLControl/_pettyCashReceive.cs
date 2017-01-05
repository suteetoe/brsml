using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace SMLERPGLControl
{
    public partial class _pettyCashReceive : UserControl
    {
        MyLib._searchDataFull searchPettyCash;
        public _pettyCashReceive()
        {
            InitializeComponent();
/*            this._pettyCashGrid._table_name = _g.d.gl_journal_petty_cash_receive._table;
            this._pettyCashGrid._width_by_persent = true;
            this._pettyCashGrid._total_show = true;
            this._pettyCashGrid._addColumn(_g.d.gl_journal_petty_cash_receive._petty_code, 1, 25, 10, true, false, true, true);
            this._pettyCashGrid._addColumn(_g.d.gl_journal_petty_cash_receive._description, 1, 25, 40, false, false, true, false);
            this._pettyCashGrid._addColumn(_g.d.gl_journal_petty_cash_receive._debit, 3, 0, 10, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._pettyCashGrid._addColumn(_g.d.gl_journal_petty_cash_receive._credit, 3, 0, 10, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._pettyCashGrid._addColumn(_g.d.gl_journal_petty_cash_receive._remark, 1, 25, 30, true, false);
            this._pettyCashGrid._clickSearchButton += new MyLib.SearchEventHandler(_pettyCashGrid__clickSearchButton);
            this._pettyCashGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_pettyCashGrid__alterCellUpdate);
            this._pettyCashGrid._totalCheck += new MyLib.TotalCheckEventHandler(_pettyCashGrid__totalCheck);
            this._pettyCashGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_pettyCashGrid__queryForInsertCheck);*/
        }

/*        void _pettyCashGrid__alterCellUpdate(object sender, int row, int column)
        {
            search(true, _pettyCashGrid._selectRow, (string)_pettyCashGrid._cellGet(_pettyCashGrid._selectRow, _g.d.gl_journal_petty_cash_receive._petty_code));
            this.Invalidate();
        }

        bool _pettyCashGrid__queryForInsertCheck(object sender, int row)
        {
            return ((((string)_pettyCashGrid._cellGet(row, _g.d.gl_journal_petty_cash_receive._petty_code)).Length == 0 || ((string)_pettyCashGrid._cellGet(row, _g.d.gl_journal_petty_cash_receive._petty_code)).Length == 0) ? false : true);
        }

        bool _pettyCashGrid__totalCheck(object sender, int row)
        {
            bool result = true;
            if (((string)this._pettyCashGrid._cellGet(row, _g.d.gl_journal_petty_cash_receive._petty_code)).ToString().Length == 0)
            {
                result = false;
            }
            return (result);
        }

        void searchPettyCashStart()
        {
            searchPettyCash = new MyLib._searchDataFull();
            searchPettyCash._name = _g.g._search_screen_gl_petty_cash;
            searchPettyCash._dataList._loadViewFormat(searchPettyCash._name, MyLib._myGlobal._userSearchScreenGroup, false);
            searchPettyCash._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            searchPettyCash._dataList._refreshData();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_gl_petty_cash) == 0)
            {
                string resultCode = (string)searchPettyCash._dataList._gridData._cellGet(e._row, _g.d.gl_petty_cash._table + "." + _g.d.gl_petty_cash._code);
                string resultName = (string)searchPettyCash._dataList._gridData._cellGet(e._row, _g.d.gl_petty_cash._table + "." + _g.d.gl_petty_cash._name_1);
                if (resultCode.Length > 0)
                {
                    searchPettyCash.Close();
                    _pettyCashGrid._cellUpdate(_pettyCashGrid._selectRow, _g.d.gl_journal_petty_cash_receive._petty_code, resultCode, false);
                    _pettyCashGrid._cellUpdate(_pettyCashGrid._selectRow, _g.d.gl_journal_petty_cash_receive._description, resultName, true);
                }
            }
        }

        void _pettyCashGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (searchPettyCash == null)
            {
                searchPettyCashStart();
            }
            searchPettyCash.Text = e._columnName;
            MyLib._myGlobal._startSearchBox(this._pettyCashGrid._inputTextBox, e._columnName, searchPettyCash);
        }

        void search(Boolean warning, int gridRow, string pettyCashCode)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string query = "select " + _g.d.gl_petty_cash._name_1 + " from " + _g.d.gl_petty_cash._table + " where " + _g.d.gl_petty_cash._code + "=\'" + pettyCashCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName,query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    _pettyCashGrid._cellUpdate(_pettyCashGrid._selectRow, _g.d.gl_journal_petty_cash_receive._description, dataResult.Tables[0].Rows[0][0].ToString(), true);
                }
                else
                {
                    MyLib._myGlobal._displayWarning(7, "");
                    _pettyCashGrid._cellUpdate(_pettyCashGrid._selectRow, _g.d.gl_journal_petty_cash_receive._petty_code, "", false);
                    _pettyCashGrid._cellUpdate(_pettyCashGrid._selectRow, _g.d.gl_journal_petty_cash_receive._description, "", false);
                }
            }
            catch
            {
            }
        }*/
    }
}
