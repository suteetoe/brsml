using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace SMLERPConfig
{
    public partial class _book : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        public _book()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            //this._myToolBar.Enabled = false;
            _myManageData1._displayMode = 3;
            _myManageData1._dataList._lockRecord = true;
            _myManageData1._dataList._loadViewFormat(_g.g._search_screen_สมุดเงินฝาก, MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._manageBackgroundPanel = this._myPanel1;
            _myManageData1._newDataClick+=new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 500;
            //
            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            _myManageData1._dataList._loadViewData(0);
            //
            _myManageData1.Invalidate();

            // อ้างอิงสำหรับแก้ไข และลบ
            _myManageData1._dataList._referFieldAdd(_g.d.erp_pass_book._code, 1);
            //
            _myScreen1._saveKeyDown += new MyLib.SaveKeyDownHandler(_myScreen1__saveKeyDown);
            _myScreen1._checkKeyDown += new MyLib.CheckKeyDownHandler(_myScreen1__checkKeyDown);
            _myScreen1._textBoxSearch += new MyLib.TextBoxSearchHandler(_myScreen1__textBoxSearch);
           

            //
        }
     
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                if (keyData == Keys.F12)
                {
                    this.save_data();
                    return true;
                }
                if (keyData == Keys.Escape)
                {
                    this.Dispose();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myManageData1._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void _myScreen1__textBoxSearch(object sender)
        {
            
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (_myScreen1._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    _myScreen1._isChange = false;
                }
            }
            return (result);
        }

        void _myManageData1__newDataClick()
        {
            Control codeControl = _myScreen1._getControl(_g.d.erp_pass_book._code);
            codeControl.Enabled = true;
            _myScreen1._clear();
            _myScreen1._focusFirst();

        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                DataSet getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_pass_book._table + whereString);
                _myScreen1._loadData(getData.Tables[0]);
                Control codeControl = _myScreen1._getControl(_g.d.erp_pass_book._code);
                _myScreen1._search(false);
                codeControl.Enabled = false;
                if (forEdit)
                {
                    _myScreen1._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        Boolean _myScreen1__checkKeyDown(object sender, Keys keyData)
        {
            if (this._myToolBar.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
                _myScreen1._isChange = false;
            }
						return true;
        }

        void _myScreen1__saveKeyDown(object sender)
        {
            if (this._myToolBar.Enabled)
            {
                save_data();
            }
        }

        void save_data()
        {
            string getEmtry = _myScreen1._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {
                ArrayList getData = _myScreen1._createQueryForDatabase();
                string myQuery = MyLib._myGlobal._xmlHeader + "<node>";
                if (_myManageData1._mode == 1)
                {
                    myQuery += "<query>insert into " + _g.d.erp_pass_book ._table+ " (" + getData[0].ToString() + ") values (" + getData[1].ToString() + ")</query>";
                }
                else
                {
                    myQuery += "<query>update " + _g.d.erp_pass_book ._table+ " set " + getData[2].ToString() + _myManageData1._dataList._whereString + "</query>";
                }
                //
                myQuery += "</node>";
                //MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, myQuery);
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    _myScreen1._isChange = false;
                    if (_myManageData1._mode == 1)
                    {
                        _myManageData1._afterInsertData();
                    }
                    else
                    {
                        _myManageData1._afterUpdateData();
                    }
                    _myScreen1._clear();
                    _myScreen1._focusFirst();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _saveButton_Click_1(object sender, EventArgs e)
        {
            save_data();
        }
    }
    public class _bookScreen : MyLib._myScreen
    {
        _g._searchChartOfAccountDialog _chartOfAccountScreen = null;
        MyLib._searchDataFull _searchPassBookType = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchBankCode = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchBankBranch = new MyLib._searchDataFull();

        string _searchName = "";
        TextBox _searchTextBox;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public _bookScreen()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.erp_pass_book._table; ;
            this._addTextBox(0, 0, 1, 0, _g.d.erp_pass_book._code, 1, 1, 0, true, false, false);
            this._addTextBox(1, 0, 1, 0, _g.d.erp_pass_book._account_code_1, 2, 1, 1, true, false,true);
            this._addTextBox(2, 0, 1, 0, _g.d.erp_pass_book._account_code_2, 2, 1, 1, true, false, true);
            this._addTextBox(3, 0, 1, 0, _g.d.erp_pass_book._book_type, 2, 1, 1, true, false, false);
            this._addTextBox(4, 0, 1, 0, _g.d.erp_pass_book._book_number, 1, 10, 0, true, false, false);
            this._addTextBox(5, 0, 1, 0, _g.d.erp_pass_book._name_1, 1, 0, 0, true, false, false);
            this._addTextBox(6, 0, 1, _g.d.erp_pass_book._name_2, 1, 100);
            this._addTextBox(7, 0, 1, 0, _g.d.erp_pass_book._bank_code, 2, 1, 1, true, false, false);
            this._addTextBox(8, 0, 1, 0, _g.d.erp_pass_book._bank_branch, 2, 100, 1, true, false, false);
            this._addTextBox(9, 0, _g.d.erp_pass_book._tax_number, 10);
            this._addTextBox(10, 0, 2, 0, _g.d.erp_pass_book._remark, 2, 100);
            this._addCheckBox(12, 0, _g.d.erp_pass_book._mobile_use, false, true);

            if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
            {
                this._addNumberBox(13, 0, 1, 1, _g.d.erp_pass_book._chq_fee_rate, 1, 2, true);
                this._addNumberBox(13, 1, 1, 1, _g.d.erp_pass_book._chq_fee_rate_amount, 1, 2, true);
                this._addNumberBox(14, 0, 1, 1, _g.d.erp_pass_book._min_chq_fee_rate, 1, 2, true);
                this._addNumberBox(14, 1, 1, 1, _g.d.erp_pass_book._min_chq_fee_amount, 1, 2, true);
                this._addNumberBox(15, 0, 1, 1, _g.d.erp_pass_book._max_chq_fee_rate, 1, 2, true);
                this._addNumberBox(15, 1, 1, 1, _g.d.erp_pass_book._max_chq_fee_amount, 1, 2, true);
            }

            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_bookScreen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_bookScreen__textBoxChanged);
            //
            _searchPassBookType._name = _g.g._search_screen_pass_book_type;
            _searchPassBookType._dataList._loadViewFormat(_searchPassBookType._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchPassBookType._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchPassBookType._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchBookType__searchEnterKeyPress);
            //
            _searchBankCode._name = _g.g._search_screen_bank;
            _searchBankCode._dataList._loadViewFormat(_searchBankCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchBankCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchBankCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchBookType__searchEnterKeyPress);
            //
            _searchBankBranch._name = _g.g._search_screen_bank_branch;
            _searchBankBranch._dataList._loadViewFormat(_searchBankBranch._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchBankBranch._dataList._gridData._mouseClick +=new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchBankBranch._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchBankBranch__searchEnterKeyPress);

            //
            MyLib._myTextBox __getbankBranch = (MyLib._myTextBox)this._getControl(_g.d.erp_pass_book._bank_branch);
            __getbankBranch.textBox.Enter +=new EventHandler(textBox_Enter);
            __getbankBranch.textBox.Leave +=new EventHandler(textBox_Leave);
            __getbankBranch.textBox.KeyUp +=new KeyEventHandler(textBox_KeyUp);

            MyLib._myTextBox __get_account_code_1 = (MyLib._myTextBox)this._getControl(_g.d.erp_pass_book._account_code_1);
            __get_account_code_1.textBox.Enter += new EventHandler(textBox_Enter);
            __get_account_code_1.textBox.Leave += new EventHandler(textBox_Leave);
            __get_account_code_1.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);

            MyLib._myTextBox __get_account_code_2 = (MyLib._myTextBox)this._getControl(_g.d.erp_pass_book._account_code_2);
            __get_account_code_2.textBox.Enter += new EventHandler(textBox_Enter);
            __get_account_code_2.textBox.Leave += new EventHandler(textBox_Leave);
            __get_account_code_2.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            //
            MyLib._myTextBox __getbankCode = (MyLib._myTextBox)this._getControl(_g.d.erp_pass_book._bank_code);
            __getbankCode.textBox.Enter += new EventHandler(textBox_Enter);
            __getbankCode.textBox.Leave += new EventHandler(textBox_Leave);
            __getbankCode.textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(textBox_KeyUp);
            //
            MyLib._myTextBox __getbookType = (MyLib._myTextBox)this._getControl(_g.d.erp_pass_book._book_type);
            __getbookType.textBox.Enter += new EventHandler(textBox_Enter);
            __getbookType.textBox.Leave += new EventHandler(textBox_Leave);
            __getbookType.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            this._chartOfAccountScreen = new _g._searchChartOfAccountDialog();
            this._chartOfAccountScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_chartOfAccountScreen__searchEnterKeyPress);
            this._chartOfAccountScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_chartOfAccount_gridData__mouseClick);
        }
        void _chartOfAccount_search(MyLib._myGrid sender, int row)
        {
            this._chartOfAccountScreen.Close();
            string __accountCode = sender._cellGet(row, 0).ToString();
            if (this._searchName.Equals(_g.d.erp_pass_book._account_code_1)) this._setDataStr(_g.d.erp_pass_book._account_code_1, __accountCode);
            if (this._searchName.Equals(_g.d.erp_pass_book._account_code_2)) this._setDataStr(_g.d.erp_pass_book._account_code_2, __accountCode);
            this._search(true);
        }
        void _chartOfAccount_gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._chartOfAccount_search((MyLib._myGrid)sender, e._row);
        }

        void _chartOfAccountScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._chartOfAccount_search(sender, row);
        }
        void _searchBankBranch__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {

        }

        void textBox_Leave(object sender, EventArgs e)
        {
            _searchPassBookType.Visible = false;
            _searchBankCode.Visible = false;
            _searchBankBranch.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _bookScreen__textBoxSearch(__getControl);
            __getControl.textBox.Focus();
        }

        void _bookScreen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.erp_pass_book._book_type) || name.Equals(_g.d.erp_pass_book._bank_code) || name.Equals(_g.d.erp_pass_book._bank_branch) || name.Equals(_g.d.erp_pass_book._account_code_1) || name.Equals(_g.d.erp_pass_book._account_code_2))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                _search(true);
            }
        }

        void _bookScreen__textBoxSearch(object sender)
        {
                        
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            this._searchName = name;
            if (name.Equals(_g.d.erp_pass_book._account_code_1) || name.Equals(_g.d.erp_pass_book._account_code_2))
            {
                this._chartOfAccountScreen.ShowDialog();
            }
            if (name.CompareTo(_g.d.erp_pass_book._bank_code) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchBankCode, false);
            }
            if (name.CompareTo(_g.d.erp_pass_book._book_type) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchPassBookType, false);
            }
            if (name.CompareTo(_g.d.erp_pass_book._bank_branch) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                string __whereQurey = _g.d.erp_bank_branch._bank_code + " = '" + this._getDataStr(_g.d.erp_pass_book._bank_code) + "'";
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchBankBranch, false,true,__whereQurey);
            }
        }
        void _searchBookType__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, e._row);
        }
        /************************************************************************************************/
        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }
        /************************************************************************************************/
        void _searchAll(string name, int row)
        {

            if (name.CompareTo(_g.g._search_screen_pass_book_type) == 0)
            {
                string result = (string)_searchPassBookType._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchPassBookType.Close();
                    this._setDataStr(_searchName, result);
                    _search(true);
                }
            }
            if (name.CompareTo(_g.g._search_screen_bank) == 0)
            {
                string result = (string)_searchBankCode._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchBankCode.Close();
                    this._setDataStr(_searchName, result);
                    _search(true);
                }
            }
            if (name.CompareTo(_g.g._search_screen_bank_branch) == 0)
            {
                string result = (string)_searchBankBranch._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchBankBranch.Close();
                    this._setDataStr(_searchName, result);
                    _search(true);
                }
            }
        }
        /************************************************************************************************/
        public void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book_type._name_1 + " from " + _g.d.erp_pass_book_type._table + " where code=\'" + this._getDataStr(_g.d.erp_pass_book._book_type) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where code=\'" + this._getDataStr(_g.d.erp_pass_book._bank_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where code=\'" + this._getDataStr(_g.d.erp_pass_book._bank_branch) + "\' and bank_code =\'"+this._getDataStr(_g.d.erp_pass_book._bank_code)+"\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this._getDataStr(_g.d.erp_pass_book._account_code_1).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this._getDataStr(_g.d.erp_pass_book._account_code_2).ToUpper() + "\'"));
                __myquery.Append("</node>");

                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.erp_pass_book._book_type, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.erp_pass_book._bank_code, (DataSet)_getData[1], warning);
                _searchAndWarning(_g.d.erp_pass_book._bank_branch, (DataSet)_getData[2], warning);
                _searchAndWarning(_g.d.erp_pass_book._account_code_1, (DataSet)_getData[3], warning);
                _searchAndWarning(_g.d.erp_pass_book._account_code_2, (DataSet)_getData[4], warning);
            }
            catch
            {
            }
        }
        /************************************************************************************************/
        bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
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
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }
        /************************************************************************************************/
    }
}
