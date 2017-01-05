using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;

namespace SMLERPGLControl
{
    public partial class _journalScreen : MyLib._myScreen
    {
        MyLib._searchDataFull _searchJournalBook = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchJournalDocNo = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchBranch = new MyLib._searchDataFull();

        public string screenCode = "";
        string _searchName = "";
        TextBox _searchTextBox;
        string _docFormatCode = "";

        /// <summary>
        /// 0=ปรกติ,1=จากรายการรายวัน
        /// </summary>
        /// <param name="mode"></param>
        public _journalScreen()
        {
            int __row = 0;
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.gl_journal._table;

            this._addDateBox(__row, 0, 1, 0, _g.d.gl_journal._doc_date, 1, true, false);
            this._addCheckBox(__row++, 1, _g.d.gl_journal._trans_direct, true, false, false, true, _g.d.gl_journal._trans_direct);

            this._addTextBox(__row, 0, 1, 0, _g.d.gl_journal._doc_no, 1, 10, 1, true, false, false);
            this._addTextBox(__row++, 1, 1, 0, _g.d.gl_journal._doc_format_code, 1, 10, 0, true, false, true);

            this._addDateBox(__row, 0, 1, 0, _g.d.gl_journal._ref_date, 1, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.gl_journal._ref_no, 1, 10, 0, true, false);

            // toe
            this._addTextBox(__row, 0, 1, 0, _g.d.gl_journal._period_number, 1, 10, 0, true, false, false, true, false, _g.d.gl_journal._account_period);
            this._addTextBox(__row++, 1, 1, 0, _g.d.gl_journal._account_year, 1, 10, 0, true, false, false);


            this._addTextBox(__row, 0, 1, 0, _g.d.gl_journal._book_code, 1, 10, 1, true, false, false);
            this._addComboBox(__row++, 1, _g.d.gl_journal._journal_type, true, new string[] { _g.d.gl_journal._daily, _g.d.gl_journal._balance_period, _g.d.gl_journal._close_period }, true);

            if (_g.g._companyProfile._branchStatus == 1)
            {
                this._addTextBox(__row++, 0, 1, 0, _g.d.gl_journal._branch_code, 1, 10, 1, true, false, false);
            }

            this._addTextBox(__row, 0, 2, 0, _g.d.gl_journal._description, 2, 255, 0, true, false, true);
            this._addTextBox(__row, 3, 1, 0, _g.d.gl_journal._ap_ar_code, 1, 10, 0, true, false);
            this._addTextBox(__row, 3, 1, 0, _g.d.gl_journal._ap_ar_originate_from, 1, 10, 0, true, false);

            this._enabedControl(_g.d.gl_journal._doc_format_code, false);
            this._enabedControl(_g.d.gl_journal._account_year, false);
            this._enabedControl(_g.d.gl_journal._period_number, false);

            this._afterClear += new MyLib.AfterClearHandler(_journalScreen__afterClear);
            this._clear();
            this.Invalidate();
            //
            // ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
            MyLib._myTextBox __getJournalBookControl = (MyLib._myTextBox)this._getControl(_g.d.gl_journal._book_code);
            __getJournalBookControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getJournalBookControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getJournalBookControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);

            MyLib._myTextBox __getJournalDocNoControl = (MyLib._myTextBox)this._getControl(_g.d.gl_journal._doc_no);
            __getJournalDocNoControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getJournalDocNoControl.textBox.Leave += new EventHandler(textBox_Leave);
            // Event
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_journalScreen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_journalScreen__textBoxChanged);

            _searchJournalBook._name = _g.g._search_screen_gl_journal_book;
            _searchJournalBook._dataList._loadViewFormat(_searchJournalBook._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchJournalBook._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchJournalBook._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchJournalBook__searchEnterKeyPress);

            _searchJournalDocNo._name = _g.g._search_screen_erp_doc_format;
            _searchJournalDocNo._dataList._loadViewFormat(_searchJournalDocNo._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchJournalDocNo._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchJournalDocNo._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchJournalBook__searchEnterKeyPress);

            _searchBranch._name = _g.g._search_master_erp_branch_list;
            _searchBranch._dataList._loadViewFormat(_searchBranch._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchBranch._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchBranch._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchJournalBook__searchEnterKeyPress);

        }

        public void _enableTransDirect(Boolean value)
        {
            this._enabedControl(_g.d.gl_journal._trans_direct, value);
        }

        void _journalScreen__afterClear(object sender)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab || keyData == Keys.Right || keyData == Keys.Down)
            {
                // วันที่อัตโนมัติ
                MyLib._myDateBox __getDateBox = (MyLib._myDateBox)this._getControl(_g.d.gl_journal._doc_date);
                if (__getDateBox.textBox.Text.Length == 0)
                {
                    this._setDataDate(_g.d.gl_journal._doc_date, MyLib._myGlobal._workingDate);
                    __getDateBox._checkDate(true, true);
                    this._isChange = false;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _journalScreen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.gl_journal._book_code))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                _search(true);
            }
            else if (name.Equals(_g.d.gl_journal._doc_no))
            {
                string __docNo = this._getDataStr(_g.d.gl_journal._doc_no);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    this._docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.บัญชี_ข้อมูลรายวัน, __docNo, this._getDataStr(_g.d.gl_journal._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง);
                    this._setDataStr(_g.d.gl_journal._doc_no, __newDoc, "", true);
                    this._setDataStr(_g.d.gl_journal._doc_format_code, _docFormatCode, "", true);
                }
                if (this._docFormatCode.Trim().Length == 0)
                {
                    string __firstString = MyLib._myGlobal._firstString(__docNo);
                    if (__firstString.Length > 0)
                    {
                        this._docFormatCode = __firstString;
                        this._setDataStr(_g.d.gl_journal._doc_format_code, _docFormatCode, "", true);
                    }
                }
            }
            else if (name.Equals(_g.d.gl_journal._branch_code))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                _search(true);
                //_searchAndWarning(_g.d.gl_journal._book_code, "select " + _g.d.erp_branch_list._name_1 + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + "=\'" + MyLib._myUtil._convertTextToXml(this._getDataStr(_g.d.gl_journal._branch_code)) + "\'", true);
            }
        }

        void _journalScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender);



            _textBoxStartSearch(__getControl);
            __getControl.textBox.Focus();
        }

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (_searchJournalBook.Visible)
                {
                    _searchJournalBook.Focus();
                    _searchJournalBook._firstFocus();
                }
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาสมุดบัญชี
                string query = "select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + _g.d.gl_journal_book._code + "=\'" + MyLib._myUtil._convertTextToXml(this._getDataStr(_g.d.gl_journal._book_code)) + "\'";
                _searchAndWarning(_g.d.gl_journal._book_code, query, warning);

                // ค้นหาสาขา
                _searchAndWarning(_g.d.gl_journal._branch_code, "select " + _g.d.erp_branch_list._name_1 + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + "=\'" + MyLib._myUtil._convertTextToXml(this._getDataStr(_g.d.gl_journal._branch_code)) + "\'", warning);

            }
            catch
            {
                if (MyLib._myGlobal._isUserTest)
                    System.Diagnostics.Debugger.Break();
            }
        }

        bool _searchAndWarning(string fieldName, string query, Boolean warning)
        {
            bool __result = false;
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, getDataStr, getData, true);
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && this._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;

            if (__getControl._name.Equals(_g.d.gl_journal._book_code))
            {
                _searchJournalBook.Visible = false;
            }
            else if (__getControl._name.Equals(_g.d.gl_journal._doc_no))
            {
                _searchJournalDocNo.Visible = false;
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;

            _textBoxStartSearch(__getControl);
            __getControl.textBox.Focus();
        }

        void _textBoxStartSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.Equals(_g.d.gl_journal._book_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchJournalBook, false);
            }
            else if (name.Equals(_g.d.gl_journal._doc_no))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchJournalDocNo, false, true, MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'" + screenCode.ToUpper() + "\'");

                //_g.g._search_screen_erp_doc_format
            }
            else if (name.Equals(_g.d.gl_journal_detail._branch_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchBranch, false);

            }
        }

        /// <summary>
        /// ดึงรายละเอียดต่างๆ (ในกรณี Enable)
        /// </summary>
        public void _reLoad()
        {
            _searchJournalBook._name = _g.g._search_screen_gl_journal_book;
        }

        /// <summary>
        /// กดปุ่ม Enter ในหน้าจอค้นหา
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="row"></param>
        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _searchJournalBook__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, e._row);
        }

        /// <summary>
        /// กด Mouse ตอนค้นหา หรือ Enter ตอนค้นหา
        /// </summary>
        /// <param name="name"></param>
        /// <param name="row"></param>
        void _searchAll(string name, int row)
        {
            if (name.Equals(_g.g._search_screen_gl_journal_book))
            {
                string result = (string)_searchJournalBook._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchJournalBook.Visible = false;
                    this._setDataStr(_searchName, result, "", true);
                    _search(true);
                }
            }
            else if (name.Equals(_g.g._screen_erp_doc_format))
            {
                string result = (string)_searchJournalDocNo._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchJournalDocNo.Visible = false;
                    this._setDataStr(_searchName, result);
                }
            }
            else if (name.Equals(_g.g._search_master_erp_branch_list))
            {
                string result = (string)_searchBranch._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchBranch.Visible = false;
                    this._setDataStr(_searchName, result);
                    _search(true);
                    //_searchAndWarning(_g.d.gl_journal._branch_code, "select " + _g.d.erp_branch_list._name_1 + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + "=\'" + MyLib._myUtil._convertTextToXml(this._getDataStr(_g.d.gl_journal._branch_code)) + "\'", true);
                }

            }
        }
    }
}
