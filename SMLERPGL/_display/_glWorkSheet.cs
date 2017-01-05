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
    public partial class _glWorkSheet : UserControl
    {
        public _glWorkSheet()
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
            //
            this._listGrid._columnTopActive = true;
            this._listGrid._addColumnTop("งบทดลอง", 2, 3);
            this._listGrid._addColumnTop("งบกำไรขาดทุน", 4, 5);
            this._listGrid._addColumnTop("งบดุล", 6, 7);
            //
            this._listGrid._addColumn(_g.d.gl_list_view._account_code, 1, 5, 15, true, false);
            this._listGrid._addColumn(_g.d.gl_list_view._account_name, 1, 5, 25, true, false);
            this._listGrid._addColumn(_g.d.gl_list_view._prev_debit, 3, 0, 10, true, false, true, false, formatNumber);
            this._listGrid._addColumn(_g.d.gl_list_view._prev_credit, 3, 0, 10, true, false, true, false, formatNumber);
            this._listGrid._addColumn(_g.d.gl_list_view._debit, 3, 0, 10, true, false, true, false, formatNumber);
            this._listGrid._addColumn(_g.d.gl_list_view._credit, 3, 0, 10, true, false, true, false, formatNumber);
            this._listGrid._addColumn(_g.d.gl_list_view._balance_debit, 3, 0, 10, true, false, true, false, formatNumber);
            this._listGrid._addColumn(_g.d.gl_list_view._balance_credit, 3, 0, 10, true, false, true, false, formatNumber);
            //
            this._listGrid._setColumnBackground(_g.d.gl_list_view._prev_debit, Color.LavenderBlush);
            this._listGrid._setColumnBackground(_g.d.gl_list_view._prev_credit, Color.LavenderBlush);
            this._listGrid._setColumnBackground(_g.d.gl_list_view._debit, Color.AliceBlue);
            this._listGrid._setColumnBackground(_g.d.gl_list_view._credit, Color.AliceBlue);
            this._listGrid._setColumnBackground(_g.d.gl_list_view._balance_debit, Color.Honeydew);
            this._listGrid._setColumnBackground(_g.d.gl_list_view._balance_credit, Color.Honeydew);
            // ซ่อนไว้เอาไว้คุมตอนแสดงผล
            this._listGrid._addColumn(_g.d.gl_list_view._account_status, 2, 0, 10, false, true, true, false);
            this._listGrid._addColumn(_g.d.gl_list_view._account_level, 2, 0, 10, false, true, true, false);
            this._listGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_listGrid__beforeDisplayRow);
            // Drill Down
            this._listGrid._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_listGrid__mouseDoubleClick);
            //
            this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenTop__saveKeyDown);
            //
        }

        _glListDrillDown __showDrillDown;
        void _listGrid__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            __showDrillDown = new _glListDrillDown();
            __showDrillDown._glList1.Disposed += new EventHandler(_glList1_Disposed);
            __showDrillDown._glList1._screenTop._setDataDate(_g.d.gl_resource._date_begin, MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.gl_resource._date_begin).ToString()));
            __showDrillDown._glList1._screenTop._setDataDate(_g.d.gl_resource._date_end, MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.gl_resource._date_end).ToString()));
            __showDrillDown._glList1._screenTop._setDataStr(_g.d.gl_resource._account_code, ((MyLib._myGrid)sender)._cellGet(e._row, 0).ToString());
            __showDrillDown._glList1._screenTop._search(false);
            __showDrillDown._glList1._process();
            __showDrillDown.ShowDialog();
        }

        void _glList1_Disposed(object sender, EventArgs e)
        {
            __showDrillDown.Dispose();
        }

        void _screenTop__saveKeyDown(object sender)
        {
            _process();
        }

        MyLib.BeforeDisplayRowReturn _listGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            return (_g.g._glListChartOfAccountBeforeDisplay(sender, columnNumber, columnName, senderRow, columnType, rowData));
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

        void _process()
        {
            this._listGrid.Focus();
            string __getScreenEmtry = this._screenTop._checkEmtryField();
            if (__getScreenEmtry.Length != 0)
            {
                MessageBox.Show((MyLib._myGlobal._language == 0) ? "กรุณาบันทึกรายละเอียดให้ครบก่อน" : "Please enter ref field");
            }
            else
            {
                SMLProcess._glProcess __process = new SMLProcess._glProcess();
                MyLib._myTreeViewDragDrop _chartOfAccountTreeView = new MyLib._myTreeViewDragDrop();
                _chartOfAccountTreeView = __process._getChartOfAccountTreeView(_chartOfAccountTreeView);
                //
                DateTime __getDateBegin = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.gl_resource._date_begin));
                DateTime __getDateEnd = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.gl_resource._date_end));
                bool __getAllData = this._screenTop._getDataStr(_g.d.gl_resource._all_data_check).Equals("1") ? true : false;
                bool __showDebitCredit = this._screenTop._getDataStr(_g.d.gl_resource._debit_credit).Equals("1") ? false : true;
                int __isPass = (int)MyLib._myGlobal._decimalPhase(this._screenTop._getDataStr(_g.d.gl_resource._is_pass).ToString());
                //
                ArrayList __result = __process._glViewWorkSheet(__getDateBegin, __getDateEnd, _chartOfAccountTreeView, __getAllData, __showDebitCredit, __isPass);
                this._listGrid._clear();
                for (int __row = 0; __row < __result.Count; __row++)
                {
                    SMLProcess._glViewJounalDetailListType __getRow = (SMLProcess._glViewJounalDetailListType)__result[__row];
                    if (__getRow._show)
                    {
                        int __newRow = this._listGrid._addRow();
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._account_code, __getRow._accountCode, false);
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._account_name, __getRow._accountName, false);
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._prev_debit, __getRow._prevDebit, false);
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._prev_credit, __getRow._prevCredit, false);
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._debit, __getRow._debit, false);
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._credit, __getRow._credit, false);
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._balance_debit, __getRow._balanceDebit, false);
                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._balance_credit, __getRow._balanceCredit, false);
                        if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.Detail)
                        {
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._account_status, __getRow._accountStatus, false);
                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._account_level, __getRow._accountLevel, false);
                        }
                        else
                            if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal)
                            {
                                this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._account_status, 2, false);
                                this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._account_level, __getRow._accountLevel, false);
                            }
                    }
                }
                this._listGrid.Invalidate();
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            _process();
        }
    }
}
