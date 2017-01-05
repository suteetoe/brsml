using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl
{
    public partial class _chqReceive : UserControl
    {
        _chqReceiveScreen screen = new _chqReceiveScreen();
        MyLib._searchDataFull searchGLChqReceive;
        public _chqReceive()
        {
            InitializeComponent();
/*            this._receiveChqGrid._table_name = _g.d.gl_journal_chq_receive._table;
            this._receiveChqGrid._width_by_persent = true;
            this._receiveChqGrid._total_show = true;
            this._receiveChqGrid._addColumn(_g.d.gl_journal_chq_receive._chq_number, 1, 25, 10, true, false, true, true);
            this._receiveChqGrid._addColumn(_g.d.gl_journal_chq_receive._bank_code, 1, 25, 10, false, false, true, false);
            this._receiveChqGrid._addColumn(_g.d.gl_journal_chq_receive._description, 1, 25, 40, false, false, false, false);
            this._receiveChqGrid._addColumn(_g.d.gl_journal_chq_receive._chq_amount, 3, 0, 10, false, false, false, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._receiveChqGrid._addColumn(_g.d.gl_journal_chq_receive._debit, 3, 0, 10, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._receiveChqGrid._addColumn(_g.d.gl_journal_chq_receive._credit, 3, 0, 10, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._receiveChqGrid._addColumn(_g.d.gl_journal_chq_receive._remark, 1, 25, 10, true, false);
            this._receiveChqGrid._clickSearchButton += new MyLib.SearchEventHandler(_receiveChqGrid__clickSearchButton);
            this._receiveChqGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_receiveChqGrid__alterCellUpdate);
            this._receiveChqGrid._totalCheck += new MyLib.TotalCheckEventHandler(_receiveChqGrid__totalCheck);
            this._receiveChqGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_receiveChqGrid__queryForInsertCheck);*/
        }

/*        void _receiveChqGrid__alterCellUpdate(object sender, int row, int column)
        {
            search(true, _receiveChqGrid._selectRow, (string)_receiveChqGrid._cellGet(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._chq_number), (string)_receiveChqGrid._cellGet(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._bank_code));
            this.Invalidate();
        }

        bool _receiveChqGrid__queryForInsertCheck(object sender, int row)
        {
            return ((((string)_receiveChqGrid._cellGet(row, _g.d.gl_journal_chq_receive._chq_number)).Length == 0 || ((string)_receiveChqGrid._cellGet(row, _g.d.gl_journal_chq_receive._chq_number)).Length == 0) ? false : true);
        }

        bool _receiveChqGrid__totalCheck(object sender, int row)
        {
            bool result = true;
            if (((string)this._receiveChqGrid._cellGet(row, _g.d.gl_journal_chq_receive._chq_number)).ToString().Length == 0)
            {
                result = false;
            }
            return (result);
        }

        void search(Boolean warning, int gridRow, string chqNumber, string bankCode)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string fieldList = _g.d.gl_chq_receive._bank_code + ",isnull((select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._code + "=" + _g.d.gl_chq_receive._table + "." + _g.d.gl_chq_receive._bank_code + "),\'\'),";
            fieldList += "isnull(" + _g.d.gl_chq_receive._bank_branch + ",\'\')" + ",isnull(" + _g.d.gl_chq_receive._owner_name + ",\'\')," + _g.d.gl_chq_receive._amount;
            string query = "select " + fieldList + " from " + _g.d.gl_chq_receive._table + " where " + _g.d.gl_chq_receive._chq_number + "=\'" + chqNumber + "\' and " + _g.d.gl_chq_receive._bank_code + "=\'" + bankCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName,query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getBankCode = dataResult.Tables[0].Rows[0][0].ToString();
                    string getBankDescription = dataResult.Tables[0].Rows[0][1].ToString();
                    getBankDescription += "/";
                    getBankDescription += dataResult.Tables[0].Rows[0][2].ToString();
                    getBankDescription += "/";
                    getBankDescription += dataResult.Tables[0].Rows[0][3].ToString();
                    decimal getAmount = (double)dataResult.Tables[0].Rows[0][4];
                    //
                    _receiveChqGrid._cellUpdate(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._chq_amount, getAmount, false);
                    _receiveChqGrid._cellUpdate(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._description, getBankDescription, false);
                    _receiveChqGrid._cellUpdate(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._bank_code, getBankCode, false);
                }
                else
                {
                    MyLib._myGlobal._displayWarning(7, "");
                    _receiveChqGrid._cellUpdate(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._chq_number, "", false);
                    _receiveChqGrid._cellUpdate(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._chq_amount, 0.0, false);
                    _receiveChqGrid._cellUpdate(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._description, "", false);
                    _receiveChqGrid._cellUpdate(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._bank_code, "", false);
                }
            }
            catch
            {
            }
        }

        void searchGLChqReceiveStart()
        {
            searchGLChqReceive = new MyLib._searchDataFull();
            searchGLChqReceive.WindowState = FormWindowState.Maximized;
            searchGLChqReceive._name = _g.g._search_screen_gl_chq_receive;
            searchGLChqReceive._dataList._loadViewFormat(searchGLChqReceive._name, MyLib._myGlobal._userSearchScreenGroup, false);
            searchGLChqReceive._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_gl_chq_receive) == 0)
            {
                string resultChqNumber = (string)searchGLChqReceive._dataList._gridData._cellGet(e._row, _g.d.gl_chq_receive._table + "." + _g.d.gl_chq_receive._chq_number);
                string resultBankCode = (string)searchGLChqReceive._dataList._gridData._cellGet(e._row, _g.d.gl_chq_receive._table + "." + _g.d.gl_chq_receive._bank_code);
                if (resultChqNumber.Length > 0)
                {
                    searchGLChqReceive.Close();
                    _receiveChqGrid._cellUpdate(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._bank_code, resultBankCode, false);
                    _receiveChqGrid._cellUpdate(_receiveChqGrid._selectRow, _g.d.gl_journal_chq_receive._chq_number, resultChqNumber, true);
                }
            }
        }

        void _receiveChqGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.CompareTo(_g.d.gl_journal_chq_receive._chq_number) == 0)
            {
                if (searchGLChqReceive == null)
                {
                    searchGLChqReceiveStart();
                }
                searchGLChqReceive.Text = e._columnName;
                searchGLChqReceive.ShowDialog();
            }
        }
        */
        private void _receiveChqGrid_Load(object sender, EventArgs e)
        {

        }
        private void _newChqButton_Click(object sender, EventArgs e)
        {
            /*int newLocationX = this._newChqButton.Width;
            int newLocationY = 0;
            Point x = this._newChqButton.PointToScreen(new Point(newLocationX, newLocationY));
            screen.DesktopLocation = x;
            screen.StartPosition = FormStartPosition.Manual;
            screen.ShowDialog(this);*/
        }
    }
}
