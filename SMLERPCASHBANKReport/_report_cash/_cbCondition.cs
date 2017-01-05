using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPCASHBANKReport
{
    public partial class _cbCondition : Form
    {
        public Boolean __check_submit = false;
        public string __where = "";

        private string _titleName;
        private string _pageName;
        private string _result;

        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value; }
        }

        public string TitleName
        {
            get { return _titleName; }
            set { _titleName = value; }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public _cbCondition()
        {
            InitializeComponent();
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
            this.Load += new EventHandler(_cbCondition_Load);
            this._conditionCB1._init(this._pageName);
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this.__check_submit = false;
            this.Close();
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            this._process_click();
            this.Close();
        }

        void _cbCondition_Load(object sender, EventArgs e)
        {
            this._conditionCB1.AutoSize = true;
        }

        public void _process_click()
        {
            ////////if (this.__click_check == 0)
            ////////{
            ////////    this.__grid_where = null;
            ////////}
            ////////else
            ////////{
            ////////    this.__grid_where = _screen_grid_ap._getCondition();
            ////////}
            this.__where = "" + this._conditionCB1._createQueryForDatabase()[1].ToString();
            this.__check_submit = true;

        }
    }

    public partial class _conditionCB : MyLib._myScreen
    {
        MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        /// <summary>
        /// ค้นหาเลขที่เอกสาร
        /// </summary>
        MyLib._searchDataFull _search_from_docno = new MyLib._searchDataFull();
        MyLib._searchDataFull _search_to_docno = new MyLib._searchDataFull();

        public _conditionCB()
        {
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_conditionCB__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_conditionCB__textBoxChanged);
        }

        void _conditionCB__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.resource_report._from_docno) ||
                name.Equals(_g.d.resource_report._to_docno))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                _search(true);
            }
        }

        void _conditionCB__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.Equals(_g.d.resource_report._from_docno))
            {
                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = __getControl.textBox;
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_from_docno, false);
            }
            if (name.Equals(_g.d.resource_report._to_docno))
            {
                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = __getControl.textBox;
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_to_docno, false);
            }
        }

        public void _init(string __page)
        {
            this._maxColumn = 2;
            this.SuspendLayout();
            this._table_name = _g.d.resource_report._table;

            this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
            this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
            this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
            this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);

            this.Invalidate();
            this.ResumeLayout();

            // ค้นหาเอกสาร            

            MyLib._myTextBox _get_from_docno = (MyLib._myTextBox)this._getControl(_g.d.resource_report._from_docno);
            _get_from_docno.textBox.Leave += new EventHandler(textBox_Leave);
            _get_from_docno.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);

            MyLib._myTextBox _get_to_docno = (MyLib._myTextBox)this._getControl(_g.d.resource_report._to_docno);
            _get_to_docno.textBox.Leave += new EventHandler(textBox_Leave);
            _get_to_docno.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);

            _search_from_docno._name = _g.g._search_screen_cb_trans;
            _search_from_docno._dataList._loadViewFormat(_search_from_docno._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_from_docno._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_from_docno._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_from_docno__searchEnterKeyPress);

            _search_to_docno._name = _g.g._search_screen_cb_trans;
            _search_to_docno._dataList._loadViewFormat(_search_to_docno._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_to_docno._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_to_docno._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_to_docno__searchEnterKeyPress);
        }


        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            if (__getParent1._gridData._rowData.Count > 0)
            {
                _searchAll(__getParent2._name, e._row);
                SendKeys.Send("{TAB}");
            }
        }

        void _search_to_docno__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _search_from_docno__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_from_docno.Visible)
                {
                    this._search_from_docno.Focus();
                    this._search_from_docno._firstFocus();
                }
                if (this._search_to_docno.Visible)
                {
                    this._search_to_docno.Focus();
                    this._search_to_docno._firstFocus();
                }
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._search_from_docno.Visible = false;
            this._search_to_docno.Visible = false;
        }



        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _searchAll(string name, int row)
        {

            if (name.CompareTo(_g.g._search_screen_cb_trans) == 0)
            {
                if (this._search_from_docno.Visible)
                {
                    string result = (string)this._search_from_docno._dataList._gridData._cellGet(row, 0);
                    if (result.Length > 0)
                    {
                        this._search_from_docno.Close();
                        this._setDataStr(_searchName, result);
                        _search(true);
                    }
                }
                else
                {
                    string result = (string)this._search_to_docno._dataList._gridData._cellGet(row, 0);
                    if (result.Length > 0)
                    {
                        this._search_to_docno.Close();
                        this._setDataStr(_searchName, result);
                        _search(true);
                    }
                }
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_trans._doc_no + " from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._doc_no + "=\'" + this._getDataStr(_g.d.resource_report._from_docno) + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_trans._doc_no + " from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._doc_no + "=\'" + this._getDataStr(_g.d.resource_report._to_docno) + "\'"));

                __myQuery.Append("</node>");
                ArrayList __getData = _myFramework._queryListGetData(MyLib._myGlobal._databaseName, __myQuery.ToString());
                _searchAndWarning(_g.d.resource_report._from_docno, (DataSet)__getData[0], warning);
                _searchAndWarning(_g.d.resource_report._to_docno, (DataSet)__getData[1], warning);

            }
            catch
            {
            }
        }
        bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, getDataStr.Trim());
            }
            else
            {
                this._setDataStr(fieldName, "");
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
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk); __result = true;
                    }
                }
            }
            return __result;
        }
    }
    
}

