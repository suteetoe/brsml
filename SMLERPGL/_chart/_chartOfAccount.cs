using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace SMLERPGL._chart
{
    public partial class _chartOfAccount : UserControl
    {
        _g._searchChartOfAccountDialog _searchChartOfAccount = new _g._searchChartOfAccountDialog();
        MyLib._searchDataFull _searchAccountGroup = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchCashFlowGroup = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLERPControl._selectCode _selectCodeSide = new SMLERPControl._selectCode(_g.d.erp_side_list._table);
        SMLERPControl._selectCode _selectCodeDepartment = new SMLERPControl._selectCode(_g.d.erp_department_list._table);
        SMLERPControl._selectCode _selectCodeAllocate = new SMLERPControl._selectCode(_g.d.erp_allocate_list._table);
        SMLERPControl._selectCode _selectCodeJob = new SMLERPControl._selectCode(_g.d.erp_job_list._table);
        SMLERPControl._selectCode _selectCodeProject = new SMLERPControl._selectCode(_g.d.erp_project_list._table);
        //
        ArrayList _listSide = new ArrayList();
        ArrayList _listDepartment = new ArrayList();
        ArrayList _listAllocate = new ArrayList();
        ArrayList _listJob = new ArrayList();
        ArrayList _listProject = new ArrayList();

        public _chartOfAccount()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            // ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
            MyLib._myTextBox __getAccountGroupControl = (MyLib._myTextBox)_screenTop._getControl(_g.d.gl_chart_of_account._account_group);
            __getAccountGroupControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getAccountGroupControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getAccountGroupControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            //
            // ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
            MyLib._myTextBox __getCashFlowControl = (MyLib._myTextBox)_screenTop._getControl(_g.d.gl_chart_of_account._cash_flow_group);
            __getCashFlowControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getCashFlowControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getCashFlowControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            //
            // Event
            _screenTop._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenTop__checkKeyDown);
            _screenTop._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenTop__textBoxSearch);
            _screenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenTop__textBoxChanged);
            _screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenTop__saveKeyDown);
            _screenTop._afterRefresh += new MyLib.AfterRefreshHandler(_screenTop__afterRefresh);
            _screenTop._checkBoxChanged += new MyLib.CheckBoxChangedHandler(_screenTop__checkBoxChanged);
            _screenTop._afterClear += new MyLib.AfterClearHandler(_screenTop__afterClear);
            //
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_gl_chart_of_account", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._gridData._addColumn(_g.d.gl_chart_of_account._account_level, 2, 0, 0, false, true, true, false, "", _g.d.gl_chart_of_account._account_level);
            _myManageData1._dataList._gridData._addColumn(_g.d.gl_chart_of_account._status, 2, 0, 0, false, true, true, false, "", _g.d.gl_chart_of_account._status);
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._manageBackgroundPanel = this._myPanel;
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 500;
            // ตัวนี้เอาไว้อ้างเพื่อเป็นตัวเชื่อม (relations) เพื่อใช้ในการแก้ไข จะได้ Update ถูก Record
            _myManageData1._dataList._referFieldAdd(_g.d.gl_chart_of_account._code, 1);
            //
            _searchChartOfAccount._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchChartOfAccount._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchChartOfAccount__searchEnter);
            //
            _searchAccountGroup._name = _g.g._search_screen_gl_account_group;
            _searchAccountGroup._dataList._loadViewFormat(_searchAccountGroup._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchAccountGroup._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchAccountGroup._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchAccountGroup__searchEnter);
            //
            _searchCashFlowGroup._name = _g.g._search_screen_gl_cash_flow_group;
            _searchCashFlowGroup._dataList._loadViewFormat(_searchCashFlowGroup._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchCashFlowGroup._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchCashFlowGroup._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchCashFlowGroup__searchEnter);
            //
            this.Disposed += new EventHandler(_chartOfAccount_Disposed);
            //
            this.Resize += new EventHandler(_chartOfAccount_Resize);
            //
            _screenTop._clear();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (SystemInformation.ComputerName.ToLower().IndexOf("jead8")!=-1)
            {
                if (keyData == Keys.F8)
                {
                    GenDataForm __genData = new GenDataForm();
                    __genData.ShowDialog();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _screenTop__afterClear(object sender)
        {
            _buttonCheckEnable();
            Control __getControl = this._screenTop._getControl(_g.d.gl_chart_of_account._active_status);
            MyLib._myCheckBox __getCheckBox = (MyLib._myCheckBox)__getControl;
            __getCheckBox.Checked = true;
            //
            _listDepartment.Clear();
        }

        void _screenTop__checkBoxChanged(object sender, string name)
        {
            _buttonCheckEnable();
        }

        void _chartOfAccount_Resize(object sender, EventArgs e)
        {
            if (_myManageData1._dataList._loadViewDataSuccess == false)
            {
                _myManageData1._dataListOpen = true;
                _myManageData1._calcArea();
                _myManageData1._dataList._loadViewData(0);
            }
        }

        // Focus อยู่ในตัวที่ Popup อัตโนมัติ ผู้ใช้ต้องการเข้าไปเลือก โดยการกด F2
        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (_searchAccountGroup.Visible)
                {
                    _searchAccountGroup.Focus();
                    _searchAccountGroup._firstFocus();
                }
                if (_searchCashFlowGroup.Visible)
                {
                    _searchCashFlowGroup.Focus();
                    _searchCashFlowGroup._firstFocus();
                }
            }
        }

        /// <summary>
        /// ออกจากช่องค้นหาอัตโนมัติ ปิดจอ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void textBox_Leave(object sender, EventArgs e)
        {
            _searchAccountGroup.Visible = false;
            _searchCashFlowGroup.Visible = false;
        }

        /// <summary>
        /// เข้าช่องค้นหาอัตโนมัติ เปิดจอ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _screenTop__textBoxSearch(__getControl);
            __getControl.textBox.Focus();
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

        void _searchCashFlowGroup__searchEnter(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchAccountGroup__searchEnter(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchChartOfAccount__searchEnter(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _checkList(ArrayList data, MyLib._myGrid grid)
        {
            for (int __row = 0; __row < grid._rowData.Count; __row++)
            {
                grid._cellUpdate(__row, 0, 0, false);
            }
            for (int __array = 0; __array < data.Count; __array++)
            {
                for (int __row = 0; __row < grid._rowData.Count; __row++)
                {
                    if (data[__array].Equals(grid._cellGet(__row, 1).ToString().ToLower()))
                    {
                        grid._cellUpdate(__row, 0, 1, false);
                    }
                }
            }
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString);
                _screenTop._loadData(__getData.Tables[0]);
                Control codeControl = _screenTop._getControl(_g.d.gl_chart_of_account._code);
                codeControl.Enabled = false;
                //
                _search(false);
                if (forEdit)
                {
                    _screenTop._focusFirst();
                }
                _buttonCheckEnable();
                if (forEdit)
                {
                    string __accountCode = _screenTop._getDataStr(_g.d.gl_chart_of_account._code);
                    //
                    this._listSide.Clear();
                    DataSet __getDataListSide = _myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_chart_of_account_side_list._side_code + " from " + _g.d.gl_chart_of_account_side_list._table + " where " + _g.d.gl_chart_of_account_side_list._account_code + "=\'" + __accountCode + "\'");
                    for (int __row = 0; __row < __getDataListSide.Tables[0].Rows.Count; __row++)
                    {
                        this._listSide.Add(__getDataListSide.Tables[0].Rows[__row].ItemArray[0].ToString().ToLower());
                    }
                    this._checkList(this._listSide, this._selectCodeSide._dataGrid);
                    //
                    this._listDepartment.Clear();
                    DataSet __getDataListDepartment = _myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_chart_of_account_depart_list._department_code + " from " + _g.d.gl_chart_of_account_depart_list._table + " where " + _g.d.gl_chart_of_account_depart_list._account_code + "=\'" + __accountCode + "\'");
                    for (int __row = 0; __row < __getDataListDepartment.Tables[0].Rows.Count; __row++)
                    {
                        this._listDepartment.Add(__getDataListDepartment.Tables[0].Rows[__row].ItemArray[0].ToString().ToLower());
                    }
                    this._checkList(this._listDepartment, this._selectCodeDepartment._dataGrid);
                    //
                    this._listAllocate.Clear();
                    DataSet __getDataListAllocate = _myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_chart_of_account_allocate_list._allocate_code + " from " + _g.d.gl_chart_of_account_allocate_list._table + " where " + _g.d.gl_chart_of_account_allocate_list._account_code + "=\'" + __accountCode + "\'");
                    for (int __row = 0; __row < __getDataListAllocate.Tables[0].Rows.Count; __row++)
                    {
                        this._listAllocate.Add(__getDataListAllocate.Tables[0].Rows[__row].ItemArray[0].ToString().ToLower());
                    }
                    this._checkList(this._listAllocate, this._selectCodeAllocate._dataGrid);
                    //
                    this._listProject.Clear();
                    DataSet __getDataListProject = _myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_chart_of_account_project_list._project_code + " from " + _g.d.gl_chart_of_account_project_list._table + " where " + _g.d.gl_chart_of_account_project_list._account_code + "=\'" + __accountCode + "\'");
                    for (int __row = 0; __row < __getDataListProject.Tables[0].Rows.Count; __row++)
                    {
                        this._listProject.Add(__getDataListProject.Tables[0].Rows[__row].ItemArray[0].ToString().ToLower());
                    }
                    this._checkList(this._listProject, this._selectCodeProject._dataGrid);
                    //
                    this._listJob.Clear();
                    DataSet __getDataListJob = _myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_chart_of_account_job_list._job_code + " from " + _g.d.gl_chart_of_account_job_list._table + " where " + _g.d.gl_chart_of_account_job_list._account_code + "=\'" + __accountCode + "\'");
                    for (int __row = 0; __row < __getDataListJob.Tables[0].Rows.Count; __row++)
                    {
                        this._listJob.Add(__getDataListJob.Tables[0].Rows[__row].ItemArray[0].ToString().ToLower());
                    }
                    this._checkList(this._listJob, this._selectCodeJob._dataGrid);
                    //
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (_screenTop._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    _screenTop._isChange = false;
                }
            }
            return (result);
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _screenTop__afterRefresh(object sender)
        {
            _myManageData1._autoSizeHeight = _screenTop.Height + _myToolBar.Height + _myPanel.Padding.Top + _myPanel.Padding.Bottom + 20;
        }

        void _chartOfAccount_Disposed(object sender, EventArgs e)
        {
            _chartOfAccountProcessFlow __process = new _chartOfAccountProcessFlow();
            __process.ShowDialog();
        }

        Boolean _screenTop__checkKeyDown(object sender, Keys keyData)
        {
            if (_myToolBar.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
            }
            return true;
        }

        void _myManageData1__newDataClick()
        {
            _screenTop._clear();
            Control codeControl = _screenTop._getControl(_g.d.gl_chart_of_account._code);
            codeControl.Enabled = true;
            _screenTop._focusFirst();
        }

        void _screenTop__saveKeyDown(object sender)
        {
            _save_data();
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            string __deleteCommand = "delete from ";
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery(__deleteCommand + "{0} {1}"), _myManageData1._dataList._tableName, getData.whereString));
            } // for
            // Delete all List
            string __whereList = " not in (select distinct " + _g.d.gl_chart_of_account._code + " from " + _g.d.gl_chart_of_account._table + ")";
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__deleteCommand + _g.d.gl_chart_of_account_side_list._table + " where " + _g.d.gl_chart_of_account_side_list._account_code + __whereList));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__deleteCommand + _g.d.gl_chart_of_account_depart_list._table + " where " + _g.d.gl_chart_of_account_depart_list._account_code + __whereList));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__deleteCommand + _g.d.gl_chart_of_account_allocate_list._table + " where " + _g.d.gl_chart_of_account_allocate_list._account_code + __whereList));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__deleteCommand + _g.d.gl_chart_of_account_job_list._table + " where " + _g.d.gl_chart_of_account_job_list._account_code + __whereList));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__deleteCommand + _g.d.gl_chart_of_account_project_list._table + " where " + _g.d.gl_chart_of_account_project_list._account_code + __whereList));
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

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            return (_g.g._chartOfAccountBeforeDisplay(sender, columnNumber, columnName, senderRow, columnType, rowData));
        }

        void _screenTop__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.gl_chart_of_account._main_code) || name.Equals(_g.d.gl_chart_of_account._account_close) || name.Equals(_g.d.gl_chart_of_account._account_group) || name.Equals(_g.d.gl_chart_of_account._cash_flow_group))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                _search(true);
            }
        }

        /// <summary>
        /// กด Mouse ตอนค้นหา หรือ Enter ตอนค้นหา
        /// </summary>
        /// <param name="name"></param>
        /// <param name="row"></param>
        void _searchAll(string name, int row)
        {
            if (name.Equals(_g.g._search_screen_gl_chart_of_account))
            {
                string __result = (string)_searchChartOfAccount._dataList._gridData._cellGet(row, _screenTop._table_name + "." + _g.d.gl_chart_of_account._code);
                if (__result.Length > 0)
                {
                    _searchChartOfAccount.Close();
                    _screenTop._setDataStr(_searchName, __result, "", true);
                    _search(true);
                }
            }
            else
                if (name.Equals(_g.g._search_screen_gl_account_group))
                {
                    string __result = (string)_searchAccountGroup._dataList._gridData._cellGet(row, 0);
                    if (__result.Length > 0)
                    {
                        _searchAccountGroup.Visible = false;
                        _screenTop._setDataStr(_searchName, __result, "", true);
                        _search(true);
                    }
                }
                else
                    if (name.Equals(_g.g._search_screen_gl_cash_flow_group))
                    {
                        string __result = (string)_searchCashFlowGroup._dataList._gridData._cellGet(row, 0);
                        if (__result.Length > 0)
                        {
                            _searchCashFlowGroup.Close();
                            _screenTop._setDataStr(_searchName, __result, "", true);
                            _search(true);
                        }
                    }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, e._row);
        }

        bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = _screenTop._getDataStr(fieldName);
                _screenTop._setDataStr(fieldName, getDataStr, getData, true);
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && _screenTop._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        //_searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") , MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

        void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _screenTop._table_name + " where " + _g.d.gl_chart_of_account._code + "=\'" + _screenTop._getDataStr(_g.d.gl_chart_of_account._main_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _screenTop._table_name + " where " + _g.d.gl_chart_of_account._code + "=\'" + _screenTop._getDataStr(_g.d.gl_chart_of_account._account_close) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_account_group._name_1 + " from " + _g.d.gl_account_group._table + " where " + _g.d.gl_account_group._code + "=\'" + _screenTop._getDataStr(_g.d.gl_chart_of_account._account_group) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_cash_flow_group._name_1 + " from " + _g.d.gl_cash_flow_group._table + " where " + _g.d.gl_cash_flow_group._code + "=\'" + _screenTop._getDataStr(_g.d.gl_chart_of_account._cash_flow_group) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.gl_chart_of_account._main_code, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.gl_chart_of_account._account_close, (DataSet)_getData[1], warning);
                _searchAndWarning(_g.d.gl_chart_of_account._account_group, (DataSet)_getData[2], warning);
                _searchAndWarning(_g.d.gl_chart_of_account._cash_flow_group, (DataSet)_getData[3], warning);
            }
            catch
            {
            }
        }

        void _screenTop__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.Equals(_g.d.gl_chart_of_account._main_code) || name.Equals(_g.d.gl_chart_of_account._account_close))
            {
                _searchName = name;
                _searchTextBox = ((MyLib._myTextBox)sender).textBox;
                _searchChartOfAccount.Text = label_name;
                _searchChartOfAccount.ShowDialog();
            }
            if (name.Equals(_g.d.gl_chart_of_account._account_group))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchAccountGroup, false);
            }
            if (name.Equals(_g.d.gl_chart_of_account._cash_flow_group))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchCashFlowGroup, false);
            }
        }

        void _save_data()
        {
            _screenTop._saveLastControl();
            string getEmtry = _screenTop._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {
                ArrayList __getData = _screenTop._createQueryForDatabase();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                if (_myManageData1._mode == 1)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                }
                else
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + __getData[2].ToString() + _myManageData1._dataList._whereString));
                }
                // Delete all List
                string __getAccountCode = _screenTop._getDataStr(_g.d.gl_chart_of_account._code);
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_chart_of_account_side_list._table + " where " + _g.d.gl_chart_of_account_side_list._account_code + "=\'" + __getAccountCode + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_chart_of_account_depart_list._table + " where " + _g.d.gl_chart_of_account_depart_list._account_code + "=\'" + __getAccountCode + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_chart_of_account_allocate_list._table + " where " + _g.d.gl_chart_of_account_allocate_list._account_code + "=\'" + __getAccountCode + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_chart_of_account_job_list._table + " where " + _g.d.gl_chart_of_account_job_list._account_code + "=\'" + __getAccountCode + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_chart_of_account_project_list._table + " where " + _g.d.gl_chart_of_account_project_list._account_code + "=\'" + __getAccountCode + "\'"));
                // Insert all List
                for (int __row = 0; __row < this._selectCodeSide._dataGrid._rowData.Count; __row++)
                {
                    if ((int)this._selectCodeSide._dataGrid._cellGet(__row, 0) == 1)
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_chart_of_account_side_list._table + " (" + _g.d.gl_chart_of_account_side_list._account_code + "," + _g.d.gl_chart_of_account_side_list._side_code + ") values (\'" + __getAccountCode + "\',\'" + this._selectCodeSide._dataGrid._cellGet(__row, 1) + "\')"));
                    }
                }
                for (int __row = 0; __row < this._selectCodeDepartment._dataGrid._rowData.Count; __row++)
                {
                    if ((int)this._selectCodeDepartment._dataGrid._cellGet(__row, 0) == 1)
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_chart_of_account_depart_list._table + " (" + _g.d.gl_chart_of_account_depart_list._account_code + "," + _g.d.gl_chart_of_account_depart_list._department_code + ") values (\'" + __getAccountCode + "\',\'" + this._selectCodeDepartment._dataGrid._cellGet(__row, 1) + "\')"));
                    }
                }
                for (int __row = 0; __row < this._selectCodeAllocate._dataGrid._rowData.Count; __row++)
                {
                    if ((int)this._selectCodeAllocate._dataGrid._cellGet(__row, 0) == 1)
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_chart_of_account_allocate_list._table + " (" + _g.d.gl_chart_of_account_allocate_list._account_code + "," + _g.d.gl_chart_of_account_allocate_list._allocate_code + ") values (\'" + __getAccountCode + "\',\'" + this._selectCodeAllocate._dataGrid._cellGet(__row, 1) + "\')"));
                    }
                }
                for (int __row = 0; __row < this._selectCodeProject._dataGrid._rowData.Count; __row++)
                {
                    if ((int)this._selectCodeProject._dataGrid._cellGet(__row, 0) == 1)
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_chart_of_account_project_list._table + " (" + _g.d.gl_chart_of_account_project_list._account_code + "," + _g.d.gl_chart_of_account_project_list._project_code + ") values (\'" + __getAccountCode + "\',\'" + this._selectCodeProject._dataGrid._cellGet(__row, 1) + "\')"));
                    }
                }
                for (int __row = 0; __row < this._selectCodeJob._dataGrid._rowData.Count; __row++)
                {
                    if ((int)this._selectCodeJob._dataGrid._cellGet(__row, 0) == 1)
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_chart_of_account_job_list._table + " (" + _g.d.gl_chart_of_account_job_list._account_code + "," + _g.d.gl_chart_of_account_job_list._job_code + ") values (\'" + __getAccountCode + "\',\'" + this._selectCodeJob._dataGrid._cellGet(__row, 1) + "\')"));
                    }
                }
                //
                __myQuery.Append("</node>");
                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    _screenTop._isChange = false;
                    if (_myManageData1._mode == 1)
                    {
                        _myManageData1._afterInsertData();
                    }
                    else
                    {
                        _myManageData1._afterUpdateData();
                    }
                    _screenTop._clear();
                    _screenTop._focusFirst();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _buttonCheckEnable()
        {
            this._buttonSelectSide.Enabled = (this._screenTop._getDataStr(_g.d.gl_chart_of_account._side_status).Equals("1")) ? true : false;
            this._buttonSelectSide.Invalidate();
            this._buttonSelectDepartment.Enabled = (this._screenTop._getDataStr(_g.d.gl_chart_of_account._department_status).Equals("1")) ? true : false;
            this._buttonSelectDepartment.Invalidate();
            this._buttonSelectAllocate.Enabled = (this._screenTop._getDataStr(_g.d.gl_chart_of_account._allocate_status).Equals("1")) ? true : false;
            this._buttonSelectAllocate.Invalidate();
            this._buttonSelectJob.Enabled = (this._screenTop._getDataStr(_g.d.gl_chart_of_account._job_status).Equals("1")) ? true : false;
            this._buttonSelectJob.Invalidate();
            this._buttonSelectProject.Enabled = (this._screenTop._getDataStr(_g.d.gl_chart_of_account._project_status).Equals("1")) ? true : false;
            this._buttonSelectProject.Invalidate();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        private void _buttonSelectSide_Click(object sender, EventArgs e)
        {
            this._selectCodeSide.Text = _buttonSelectSide.Text;
            this._selectCodeSide.ShowDialog();
        }

        private void _buttonSelectDepartment_Click(object sender, EventArgs e)
        {
            this._selectCodeDepartment.Text = _buttonSelectDepartment.Text;
            this._selectCodeDepartment.ShowDialog();
        }

        private void _buttonSelectAllocate_Click(object sender, EventArgs e)
        {
            this._selectCodeAllocate.Text = _buttonSelectAllocate.Text;
            this._selectCodeAllocate.ShowDialog();
        }

        private void _buttonSelectJob_Click(object sender, EventArgs e)
        {
            this._selectCodeJob.Text = _buttonSelectJob.Text;
            this._selectCodeJob.ShowDialog();
        }

        private void _buttonSelectProject_Click(object sender, EventArgs e)
        {
            this._selectCodeProject.Text = _buttonSelectProject.Text;
            this._selectCodeProject.ShowDialog();
        }
    }
}
