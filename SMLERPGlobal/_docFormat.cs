using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyLib;
using System.IO;

namespace _g
{
    public class _docFormat : MyLib._manageMasterCodeFull
    {
        private _docFormatGeneralLedgerControl __gl;
        private MyLib._searchDataFull _searchJournalBook;
        private string _searchName = "";
        private TextBox _searchTextBox;

        public _docFormat(string menuName, string screenName, string programName)
        {
            // get master
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, "select count(*) as mycount from " + _g.d.erp_doc_format._table);
            int __count = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0].ItemArray[0].ToString());
            if (__count == 0)
            {
                string __message = "ไม่พบข้อมูลในระบบ ต้องการดึงข้อมูลจาก Server www.smlsoft.com มาเป็นข้อมูลเบื้องต้นหรือไม่";
                if (MessageBox.Show(__message, this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string[] __fieldList = { _g.d.erp_doc_format._code, _g.d.erp_doc_format._screen_code, _g.d.erp_doc_format._name_1, _g.d.erp_doc_format._name_2, _g.d.erp_doc_format._format, _g.d.erp_doc_format._tax_format, _g.d.erp_doc_format._doc_running, _g.d.erp_doc_format._form_code, _g.d.erp_doc_format._form_code_1, _g.d.erp_doc_format._form_code_2, _g.d.erp_doc_format._form_code_3, _g.d.erp_doc_format._form_code_4, _g.d.erp_doc_format._form_code_5, _g.d.erp_doc_format._gl_book, _g.d.erp_doc_format._gl_description };
                        StringBuilder __field = new StringBuilder();
                        for (int __loop = 0; __loop < __fieldList.Length; __loop++)
                        {
                            if (__field.Length > 0)
                            {
                                __field.Append(",");
                            }
                            __field.Append(__fieldList[__loop]);
                        }
                        //
                        MyLib._myFrameWork __selectFromServerFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                        DataTable __getData = __selectFromServerFrameWork._query(MyLib._myGlobal._masterDatabaseName, "select " + __field.ToString() + " from " + _g.d.erp_doc_format._table).Tables[0];
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        ArrayList __getQuery = this._inputScreen._createQueryForDatabase();
                        for (int __row = 0; __row < __getData.Rows.Count; __row++)
                        {
                            StringBuilder __insertQuery = new StringBuilder(String.Concat("insert into ", _g.d.erp_doc_format._table, " (", __field.ToString(), ") values ("));
                            for (int __column = 0; __column < __fieldList.Length; __column++)
                            {
                                if (__column != 0)
                                {
                                    __insertQuery.Append(",");
                                }
                                __insertQuery.Append(String.Concat("\'", __getData.Rows[__row].ItemArray[__column].ToString(), "\'"));
                            }
                            __insertQuery.Append(")");
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertQuery.ToString()));
                        }
                        __myQuery.Append("</node>");
                        string __resultQuery = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__resultQuery.Length != 0)
                        {
                            MessageBox.Show("Fail : " + __resultQuery);
                        }
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show("Fail : " + __ex.Message.ToString());
                    }
                }
            }
            //
            _searchJournalBook = new _searchDataFull();
            _searchJournalBook._name = _g.g._search_screen_gl_journal_book;
            _searchJournalBook._dataList._loadViewFormat(_searchJournalBook._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchJournalBook._dataList._gridData._mouseClick += new MouseClickHandler(_gridData__mouseClick);
            _searchJournalBook._searchEnterKeyPress += new SearchEnterKeyPressEventHandler(_searchJournalBook__searchEnterKeyPress);
            //
            this._labelTitle.Text = screenName;
            this._maxColumn = 4;
            this._dataTableName = _g.d.erp_doc_format._table;
            this._addColumn(_g.d.erp_doc_format._code, 10, 10, 1, 0, true, 1, 1);
            this._inputScreen._setUpper(_g.d.erp_doc_format._code);
            this._addColumn(_g.d.erp_doc_format._screen_code, 10, 10, 1, 0, true, 1, 1);
            this._addColumn(_g.d.erp_doc_format._name_1, 100, 20, 1, 0, true, 1, 2);
            this._addColumn(_g.d.erp_doc_format._name_2, 100, 20);
            this._addColumn(_g.d.erp_doc_format._format, 100, 30);
            this._addColumn(_g.d.erp_doc_format._tax_format, 100, 30);
            this._addColumn(_g.d.erp_doc_format._doc_running, 100, 30);
            this._addColumn(_g.d.erp_doc_format._form_code, 1000, 30);
            this._addColumn(_g.d.erp_doc_format._form_code_1, 100, 30);
            this._addColumn(_g.d.erp_doc_format._form_code_2, 100, 30);
            this._addColumn(_g.d.erp_doc_format._form_code_3, 100, 30);
            this._addColumn(_g.d.erp_doc_format._form_code_4, 100, 30);
            this._addColumn(_g.d.erp_doc_format._form_code_5, 100, 30);
            this._addColumn(_g.d.erp_doc_format._gl_book, 100, 30, 1, 1, true);
            this._rowScreen++;
            this._columnScreen = 0;
            this._addColumn(_g.d.erp_doc_format._scan_folder, 100, 20, 1, 1, true, 1, 2);
            this._addColumn(_g.d.erp_doc_format._scan_computer_id, 100, 20, 1, 0, true, 1, 2);
            this._columnScreen = 0;
            this._addColumn(_g.d.erp_doc_format._gl_description, 100, 30, 2, 0, true, 1, (_g.g._companyProfile._branchStatus == 1) ? 2 : 4);
            //

            // branch
            if (_g.g._companyProfile._branchStatus == 1)
            {
                this._inputScreen._addCheckBox(this._rowScreen, 2, _g.d.erp_doc_format._use_branch_select, true, false);
                this._inputScreen._addTextBox(this._rowScreen + 1, 2, 1, 1, _g.d.erp_doc_format._branch_list, 2, 255);
            }

            if (MyLib._myGlobal._OEMVersion.Equals("imex"))
            {
                this._rowScreen++;
                this._inputScreen._addComboBox(this._rowScreen, 0, _g.d.erp_doc_format._vat_type, true, new string[] { _g.d.erp_doc_format._normal_vat, _g.d.erp_doc_format._exclude_vat, _g.d.erp_doc_format._include_vat, _g.d.erp_doc_format._zero_vat }, false);
            }

            this._inputScreen._textBoxSearch += new TextBoxSearchHandler(_inputScreen__textBoxSearch);
            this._inputScreen._textBoxChanged += new TextBoxChangedHandler(_inputScreen__textBoxChanged);



            MyLib._myTextBox __getJournalBookControl = (MyLib._myTextBox)this._inputScreen._getControl(_g.d.erp_doc_format._gl_book);
            __getJournalBookControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getJournalBookControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getJournalBookControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            this._finish();
            this._webBrowser.Dispose();
            this._inputScreen._refresh();
            this.Invalidate();
            //
            TabControl __tab = new TabControl();
            //
            WebBrowser __help = new WebBrowser();
            //
            __tab.TabPages.Add("GL");
            __tab.TabPages.Add("Help");
            __gl = new _docFormatGeneralLedgerControl();
            __gl.Dock = DockStyle.Fill;
            __tab.TabPages[0].Controls.Add(__gl);
            __tab.TabPages[0].Text = "GL";
            __tab.TabPages[1].Controls.Add(__help);
            __tab.TabPages[1].Text = "Help";
            __tab.Dock = DockStyle.Fill;
            this._extraPanel.Dock = DockStyle.Fill;
            __help.ScriptErrorsSuppressed = true;
            __help.Dock = DockStyle.Fill;

            //__help.Navigate("https://docs.google.com/spreadsheets/d/1w0riTg0DQAYCQRM2r4Cy0ED9MXL-j3dirYojxsTTk5I/pubhtml");
            //string curDir = Directory.GetCurrentDirectory();
            // __help.Url = new Uri(String.Format("file:///{0}/screen_code.html", curDir));
            __help.Navigate("http://www.smlsoft.com/screen_code.html");

            // __help.Document.Body.ScrollIntoView(true);
            //this._panel1.Dock = DockStyle.Fill;
            this._extraPanel.Controls.Add(__tab);
            //
            this._saveData += new SaveDataEvent(_docFormat__saveData);
            this._afterClearData += new AfterClearDataEvent(_docFormat__afterClearData);
            this._loadData += new LoadDataEvent(_docFormat__loadData);
            this._deleteData += new DeleteDataEvent(_docFormat__deleteData);
            this._afterNewData += _docFormat__afterNewData;
            //
            this.__gl._screenCode += new _docFormatGeneralLedgerControl.ScreenCodeEventHandler(__gl__screenCode);

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLBIllFree)
            {
                __tab.TabPages.RemoveAt(0);
            }
        }

        void _docFormat__afterNewData(_manageMasterCodeFull sender)
        {
            this._inputScreen._clearComcoBox(_g.d.erp_doc_format._vat_type);
        }

        string __gl__screenCode()
        {
            string __screenCode = ((MyLib._myTextBox)this._inputScreen._getControl(_g.d.erp_doc_format._screen_code))._textFirst;
            return __screenCode;
        }

        string _docFormat__deleteData(_manageMasterCodeFull sender, string fieldData)
        {
            StringBuilder __result = new StringBuilder();
            __result.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_doc_format_gl._table + " where " + _g.d.erp_doc_format_gl._doc_code + "=\'" + fieldData + "\'"));
            return __result.ToString();
        }

        void _docFormat__loadData(_manageMasterCodeFull sender)
        {
            __gl._grid._clear();
            _myFrameWork __myFrameWork = new _myFrameWork();
            DataTable __getData = __myFrameWork._queryShort("select * from " + _g.d.erp_doc_format_gl._table + " where " + _g.d.erp_doc_format_gl._doc_code + "=\'" + sender._oldCode + "\' and " + _g.d.erp_doc_format_gl._condition_number + ">0 order by " + _g.d.erp_doc_format_gl._line_number).Tables[0];
            __gl._grid._loadFromDataTable(__getData);
        }

        void _docFormat__afterClearData(_manageMasterCodeFull sender)
        {
            __gl._grid._clear();
            this._inputScreen._clearComcoBox(_g.d.erp_doc_format._vat_type);
        }

        string _docFormat__saveData(_manageMasterCodeFull sender)
        {
            __gl._grid._updateRowIsChangeAll(true);
            StringBuilder __result = new StringBuilder();
            string __code = sender._inputScreen._getDataStr(_g.d.erp_doc_format._code);
            __result.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_doc_format_gl._table + " where " + _g.d.erp_doc_format_gl._doc_code + "=\'" + sender._oldCode + "\'"));
            __result.Append(__gl._grid._createQueryForInsert(_g.d.erp_doc_format_gl._table, _g.d.erp_doc_format_gl._doc_code + ",", "\'" + __code + "\',", false, true));
            return __result.ToString();
        }

        void _inputScreen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.erp_doc_format._gl_book))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                _search(true);
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            _searchJournalBook.Visible = false;
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

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _textBoxStartSearch(__getControl);
            __getControl.textBox.Focus();
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _searchAll(string name, int row)
        {
            if (name.Equals(_g.g._search_screen_gl_journal_book))
            {
                string result = (string)_searchJournalBook._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchJournalBook.Visible = false;
                    this._inputScreen._setDataStr(_searchName, result, "", true);
                    _search(true);
                }
            }
        }

        void _searchJournalBook__searchEnterKeyPress(_myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, e._row);
        }

        void _textBoxStartSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.Equals(_g.d.erp_doc_format._gl_book))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchJournalBook, false);
            }
            if (name.Equals(_g.d.erp_doc_format._scan_folder))
            {
                FolderBrowserDialog __fbd = new FolderBrowserDialog();
                DialogResult __result = __fbd.ShowDialog();
                if (__result == DialogResult.OK)
                {
                    this._inputScreen._setDataStr(_g.d.erp_doc_format._scan_folder, __fbd.SelectedPath);
                }
            }
        }

        void _inputScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender);
            _textBoxStartSearch(__getControl);
            __getControl.textBox.Focus();
        }

        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาสมุดบัญชี
                string query = "select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + _g.d.gl_journal_book._code + "=\'" + MyLib._myUtil._convertTextToXml(this._inputScreen._getDataStr(_g.d.erp_doc_format._gl_book)) + "\'";
                _searchAndWarning(_g.d.erp_doc_format._gl_book, query, warning);
            }
            catch
            {
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
                string getDataStr = this._inputScreen._getDataStr(fieldName);
                this._inputScreen._setDataStr(fieldName, getDataStr, getData, true);
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && this._inputScreen._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._inputScreen._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

        private void InitializeComponent()
        {
            //this._panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _inputScreen
            // 
            this._inputScreen.Size = new System.Drawing.Size(803, 0);
            // 
            // _extraPanel
            // 
            this._extraPanel.Size = new System.Drawing.Size(803, 0);
            // 
            // _panel1
            // 
            //this._panel1.Size = new System.Drawing.Size(803, 0);
            // 
            // _docFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "_docFormat";
            //this._panel1.ResumeLayout(false);
            //this._panel1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
