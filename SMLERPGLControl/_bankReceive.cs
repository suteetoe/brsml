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
	public partial class _bankReceive : UserControl
	{
        MyLib._searchDataFull searchPassBook;
        public _bankReceive()
		{
			InitializeComponent();
/*            this._bankGrid._table_name = _g.d.gl_journal_bank_receive._table;
			this._bankGrid._width_by_persent = true;
			this._bankGrid._total_show = true;
            this._bankGrid._addColumn(_g.d.gl_journal_bank_receive._pass_book_code, 1, 20, 20, true, false, true, true);
            this._bankGrid._addColumn(_g.d.gl_journal_bank_receive._description, 1, 15, 30, false, false,true);
            this._bankGrid._addColumn(_g.d.gl_journal_bank_receive._debit, 3, 0, 10, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._bankGrid._addColumn(_g.d.gl_journal_bank_receive._credit, 3, 0, 10, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._bankGrid._addColumn(_g.d.gl_journal_bank_receive._remark, 1, 25, 30, true, false);
            this._bankGrid._clickSearchButton += new MyLib.SearchEventHandler(_bankGrid__clickSearchButton);
            this._bankGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_bankGrid__alterCellUpdate);
            this._bankGrid._totalCheck += new MyLib.TotalCheckEventHandler(_bankGrid__totalCheck);
            this._bankGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_bankGrid__queryForInsertCheck);*/
        }

        /*void _bankGrid__alterCellUpdate(object sender, int row, int column)
        {
            search(true, _bankGrid._selectRow, (string)_bankGrid._cellGet(_bankGrid._selectRow, _g.d.gl_journal_bank_receive._pass_book_code));
            this.Invalidate();
        }

        bool _bankGrid__queryForInsertCheck(object sender, int row)
        {
            return ((((string)_bankGrid._cellGet(row, _g.d.gl_journal_bank_receive._pass_book_code)).Length == 0 || ((string)_bankGrid._cellGet(row, _g.d.gl_journal_bank_receive._pass_book_code)).Length == 0) ? false : true);
        }

        bool _bankGrid__totalCheck(object sender, int row)
        {
            bool result = true;
            if (((string)this._bankGrid._cellGet(row, _g.d.gl_journal_bank_receive._pass_book_code)).ToString().Length == 0)
            {
                result = false;
            }
            return (result);
        }

        void search(Boolean warning, int gridRow, string bookCode)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string fieldList = "isnull(" + _g.d.erp_pass_book._book_number + ",\'\')+'/'+isnull(" + _g.d.erp_pass_book._name_1 + ",\'\')+'/'+isnull(" + _g.d.erp_pass_book._bank_code + ",\'\')+'/'+isnull(" + _g.d.erp_pass_book._bank_branch + ",\'\')";
            string query ="select " + fieldList + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + bookCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName,query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string description = dataResult.Tables[0].Rows[0][0].ToString();
                    //
                    _bankGrid._cellUpdate(_bankGrid._selectRow, _g.d.gl_journal_bank_receive._description, description, false);
                }
                else
                {
                    MyLib._myGlobal._displayWarning(7, "");
                    _bankGrid._cellUpdate(_bankGrid._selectRow, _g.d.gl_journal_bank_receive._pass_book_code, "", false);
                    _bankGrid._cellUpdate(_bankGrid._selectRow, _g.d.gl_journal_bank_receive._description, "", false);
                }
            }
            catch
            {
            }
        }

        void searchPassBookStart()
        {
            searchPassBook = new MyLib._searchDataFull();
            searchPassBook.WindowState = FormWindowState.Maximized;
            searchPassBook._name = _g.g._search_screen_pass_book;
            searchPassBook._dataList._loadViewFormat(searchPassBook._name, MyLib._myGlobal._userSearchScreenGroup, false);
            searchPassBook._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_pass_book) == 0)
            {
                string result = (string)searchPassBook._dataList._gridData._cellGet(e._row, _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._code);
                if (result.Length > 0)
                {
                    searchPassBook.Close();
                    _bankGrid._cellUpdate(_bankGrid._selectRow, _g.d.gl_journal_bank_receive._pass_book_code, result, true);
                }
            }
        }

        void _bankGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.CompareTo(_g.d.gl_journal_bank_receive._pass_book_code) == 0)
            {
                if (searchPassBook == null)
                {
                    searchPassBookStart();
                }
                searchPassBook.Text = e._columnName;
                searchPassBook.ShowDialog();
            }
        }
        */
		private void _myGrid1_Load(object sender, EventArgs e)
		{

		}
	}
}
