using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._display
{
    public partial class _glList : UserControl
    {
        public _glList()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            string formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._listGrid._width_by_persent = true;
            this._listGrid._isEdit = false;
            this._listGrid._table_name = _g.d.gl_list_view._table;
            this._listGrid._addColumn(_g.d.gl_list_view._journal_date, 4, 0, 15);
            this._listGrid._addColumn(_g.d.gl_list_view._journal_doc_no, 1, 5, 15, true, false);
            this._listGrid._addColumn(_g.d.gl_list_view._journal_description, 1, 5, 30, true, false);
            this._listGrid._addColumn(_g.d.gl_list_view._book_code, 1, 5, 10, true, false);
            this._listGrid._addColumn(_g.d.gl_list_view._debit, 3, 0, 10, true, false, true, false, formatNumber);
            this._listGrid._addColumn(_g.d.gl_list_view._credit, 3, 0, 10, true, false, true, false, formatNumber);
            this._listGrid._addColumn(_g.d.gl_list_view._balance, 3, 0, 10, true, false, true, false, formatNumber);
            this._listGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_listGrid__beforeDisplayRow);
            // Drill Down
            this._listGrid._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_listGrid__mouseDoubleClick);
            //
            this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenTop__saveKeyDown);
            //
        }

        void _screenTop__saveKeyDown(object sender)
        {
            _process();
        }

        _glDetailDrillDown __showDrillDown;
        void _listGrid__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            DateTime __getDocDate = (DateTime)((MyLib._myGrid)sender)._cellGet(e._row, _g.d.gl_list_view._journal_date);
            string __getDocNo = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.gl_list_view._journal_doc_no).ToString();
            string __getBookNo = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.gl_list_view._book_code).ToString().Split('/')[0].ToString();
            if (__getDocDate.Year != 1000)
            {
                __showDrillDown = new _glDetailDrillDown();
                __showDrillDown._process(__getDocNo);
                __showDrillDown.ShowDialog();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                _process();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        MyLib.BeforeDisplayRowReturn _listGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            if (((DateTime)sender._cellGet(sender._rowFirst+row, _g.d.gl_list_view._journal_date)).Year == 1000)
            {
                senderRow.newColor = Color.Blue;
            }
            return senderRow;
        }

        private void _myPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            _process();
        }

        public void _process()
        {
            this._listGrid.Focus();
            this._listGrid._clear();
            string __getScreenEmtry = this._screenTop._checkEmtryField();
            if (__getScreenEmtry.Length != 0)
            {
                MessageBox.Show((MyLib._myGlobal._language == 0) ? "กรุณาบันทึกรายละเอียดให้ครบก่อน" : "Please enter ref field");
            }
            else
            {
                SMLProcess._glProcess __process = new SMLProcess._glProcess();
                string __getAccountCode = this._screenTop._getDataStr(_g.d.gl_resource._account_code);
                DateTime __getDateBegin = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.gl_resource._date_begin));
                DateTime __getDateEnd = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.gl_resource._date_end));
                int __isPass = (int)MyLib._myGlobal._decimalPhase(this._screenTop._getDataStr(_g.d.gl_resource._is_pass).ToString());
                ArrayList __result = __process._glViewJounalDetail(__getAccountCode, __getDateBegin, __getDateEnd, true, __isPass);
                for (int __row = 0; __row < __result.Count; __row++)
                {
                    SMLProcess._glViewJounalDetailListType __getRow = (SMLProcess._glViewJounalDetailListType)__result[__row];
                    if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.Balance)
                    {
                        int __newRow = this._listGrid._addRow();
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_description, __getRow._desc, false);
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._balance, __getRow._balance, false);
                    }
                    else
                        if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal)
                        {
                            int __newRow = this._listGrid._addRow();
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_description, __getRow._desc, false);
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._debit, __getRow._debit, false);
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._credit, __getRow._credit, false);
                        }
                        else
                        {
                            int __newRow = this._listGrid._addRow();
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_date, __getRow._date, false);
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_doc_no, __getRow._number, false);
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_description, __getRow._desc, false);
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._book_code, __getRow._book, false);
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._debit, __getRow._debit, false);
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._credit, __getRow._credit, false);
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._balance, __getRow._balance, false);
                        }
                }
                this._listGrid.Invalidate();
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
