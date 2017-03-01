using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _manageMasterCodeFull : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public String _refFieldAdd;
        public String _dataTableNameResult;
        /// <summary>
        /// ดึง Template จาก web service อัตโนมัติ
        /// </summary>
        public Boolean _getTemplate = false;
        /// <summary>
        /// คำสั่ง Query
        /// </summary>
        public string _getTemplateQuery = "";
        public string _extraInsertField = "";
        public string _extraInsertData = "";
        public string _extraUpdateQuery = "";
        public string _extraUpdateWhere = "";
        //
        public string _oldCode = "";

        public event AfterNewDataEvent _afterNewData;
        public event AfterClearDataEvent _afterClearData;
        public event DeleteDataEvent _deleteData;
        public event SaveDataEvent _saveData;
        public event LoadDataEvent _loadData;
        public event CheckDataForDeleteEvent _checkDataForDelete;

        //
        public delegate void AfterNewDataEvent(_manageMasterCodeFull sender);
        public delegate void AfterClearDataEvent(_manageMasterCodeFull sender);
        public delegate string DeleteDataEvent(_manageMasterCodeFull sender, string fieldData);
        public delegate string SaveDataEvent(_manageMasterCodeFull sender);
        public delegate void LoadDataEvent(_manageMasterCodeFull sender);
        public delegate Boolean CheckDataForDeleteEvent(ArrayList selectedRow);

        public _manageMasterCodeFull()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //somruk
            this._manageDataScreen._isnonPermission = false;
            this._manageDataScreen._dataList._isnonPermission = false;
            this._manageDataScreen._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            this._manageDataScreen._manageButton = this._toolBar;
            this._manageDataScreen._manageBackgroundPanel = this._myPanel;
            this._manageDataScreen._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            this._manageDataScreen._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            this._manageDataScreen._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            this._manageDataScreen._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            this._manageDataScreen._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);

            this._manageDataScreen._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            _inputScreen._saveKeyDown += new MyLib.SaveKeyDownHandler(_userScreen1__saveKeyDown);
            _inputScreen._checkKeyDown += new MyLib.CheckKeyDownHandler(_userScreen1__checkKeyDown);
            this._manageDataScreen._autoSize = true;
            this._manageDataScreen._autoSizeHeight = 500;
            this._manageDataScreen._editMode = true;

            //this.Load += new EventHandler(_manageMasterCodeFull_Load);

            //this.Disposed += new EventHandler(_user_Disposed);
            //this.Resize += new EventHandler(_user_Resize);
            this._webBrowser.Visible = false;

            if (MyLib._myGlobal._isUserLockDocument == true)
            {
                this._manageDataScreen._dataList._isLockDoc = true;

                this._manageDataScreen._dataList._buttonUnlockDoc.Visible = true;
                this._manageDataScreen._dataList._buttonLockDoc.Visible = true;
                this._manageDataScreen._dataList._separatorLockDoc.Visible = true;

            }
        }

        public String _dataTableName
        {
            set
            {
                this._dataTableNameResult = value;
                this._inputScreen._table_name = value;
                this._manageDataScreen._dataList._tableName = value;
            }
            get
            {
                return this._dataTableNameResult;
            }
        }

        public int _rowScreen = 0;
        public int _columnScreen = 0;
        public int _maxColumnTemp = 1;
        public int _columnCount = 0;

        public int _maxColumn
        {
            set
            {
                this._maxColumnTemp = value;
                this._inputScreen._maxColumn = value;
            }
            get
            {
                return this._maxColumnTemp;
            }
        }

        public void _addColumn(String code, int lengh, int width)
        {
            this._addColumn(code, lengh, width, 1, 0, true, 1);
        }

        public void _addColumn(String code, int lengh, int width, int type)
        {
            this._addColumn(code, lengh, width, 1, 0, true, type);
        }

        public void _addColumn(String code, int lengh, int width, int rowCount, int iconNumber, Boolean isQuery)
        {
            this._addColumn(code, lengh, width, rowCount, iconNumber, isQuery, 1);
        }

        public void _addColumn(String code, int lengh, int width, int rowCount, int iconNumber, Boolean isQuery, int type)
        {
            this._addColumn(code, lengh, width, rowCount, iconNumber, isQuery, type, 1);
        }

        public void _addColumn(String code, int lengh, int width, int rowCount, int iconNumber, Boolean isQuery, int type, int columnCount)
        {
            if (this._columnCount == 0)
            {
                this._refFieldAdd = code;
                this._manageDataScreen._dataList._referFieldAdd(this._refFieldAdd, 1);
                this._manageDataScreen._dataList._gridData._addColumn("check", 11, 0, 10, false, false, false, false);
            }
            if (this._columnCount < 3)
            {
                int __width = (this._columnCount == 0) ? 20 : 35;
                String __fieldName = this._manageDataScreen._dataList._tableName + "." + code;
                this._manageDataScreen._dataList._gridData._addColumn(__fieldName, type, 100, __width, false, false, true, true, "", __fieldName);
            }
            Boolean __nullValue = (this._columnCount < 2) ? false : true;
            switch (type)
            {
                case 1: this._inputScreen._addTextBox(this._rowScreen, this._columnScreen, rowCount, 0, code, columnCount, lengh, iconNumber, true, false, __nullValue, isQuery); break;
                case 2: this._inputScreen._addNumberBox(this._rowScreen, this._columnScreen, rowCount, 0, code, columnCount, 0, true); break;
            }
            if (type == 1 || type == 2)
            {
                this._columnScreen += columnCount;
                if (this._columnScreen >= this._maxColumn)
                {
                    this._columnScreen = 0;
                    this._rowScreen++;
                }
            }
            this._columnCount++;
        }

        public void _addColumnNumber(String code, int lengh, int width)
        {
        }

        public void _finish()
        {
            this._manageDataScreen._dataList._gridData._addColumn("guid_code", 1, 0, 0, false, true, false, false);
            this._manageDataScreen._dataList._gridData._addColumn("is_lock_record", 2, 0, 0, false, true, true, false);//somruk
            this._manageDataScreen._dataList._gridData._calcPersentWidthToScatter();
            if (this._getTemplate)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, "select count(*) as mycount from " + this._manageDataScreen._dataList._tableName);
                int __count = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0].ItemArray[0].ToString());
                if (__count == 0)
                {
                    string __message = "ไม่พบข้อมูลในระบบ ต้องการดึงข้อมูลจาก Server www.smlsoft.com มาเป็นข้อมูลเบื้องต้นหรือไม่";
                    if (MessageBox.Show(__message, this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            MyLib._myFrameWork __select = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                            DataTable __getData = __select._query(MyLib._myGlobal._masterDatabaseName, this._getTemplateQuery).Tables[0];
                            StringBuilder __myQuery = new StringBuilder();
                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            ArrayList __getQuery = this._inputScreen._createQueryForDatabase();
                            for (int __row = 0; __row < __getData.Rows.Count; __row++)
                            {
                                StringBuilder __insertQuery = new StringBuilder(String.Concat("insert into ", this._manageDataScreen._dataList._tableName, " (", __getQuery[0].ToString(), ") values ("));
                                for (int __column = 0; __column < __getData.Columns.Count; __column++)
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
                            string __resultQuery = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
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
            }
        }

        void _manageMasterCodeFull_Load(object sender, EventArgs e)
        {
            try
            {
                //((MyLib._myTextBox)_inputScreen._getControl(this._refFieldAdd)).IsUpperCase = true;
                _inputScreen._setUpper(this._refFieldAdd);
            }
            catch
            {
            }
        }

        void _user_Resize(object sender, EventArgs e)
        {
            if (this._manageDataScreen._dataList._loadViewDataSuccess == false)
            {
                this._manageDataScreen._dataListOpen = true;
                this._manageDataScreen._calcArea();
                this._manageDataScreen._dataList._loadViewData(0);
            }
        }

        void _user_Disposed(object sender, EventArgs e)
        {
        }

        bool _userScreen1__checkKeyDown(object sender, Keys keyData)
        {
            if (_toolBar.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
                _inputScreen._isChange = false;
                return false;
            }
            return true;
        }

        void _userScreen1__saveKeyDown(object sender)
        {
            _save_data();
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                if (this._manageDataScreen._readOnly)
                {
                    MessageBox.Show(MyLib._myGlobal._resource(this._manageDataScreen._readOnlyMessage));
                    return;
                }

                bool __pass = true;

                if (this._checkDataForDelete != null)
                {
                    __pass = this._checkDataForDelete(selectRowOrder);
                }

                if (__pass)
                {
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                    {
                        MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                        string __where = getData.whereString + ((this._manageDataScreen._dataList._extraWhere.Length == 0) ? "" : " and ") + this._manageDataScreen._dataList._extraWhere;
                        __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), this._manageDataScreen._dataList._tableName, __where));
                        if (_deleteData != null)
                        {
                            string __masterCode = this._manageDataScreen._dataList._gridData._cellGet(getData.row, 1).ToString();
                            __myQuery.Append(_deleteData(this, __masterCode));
                        }
                    } // for
                    __myQuery.Append("</node>");
                    string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("success"));
                        this._manageDataScreen._dataList._refreshData();
                    }
                    else
                    {
                        MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        void _myManageData1__clearData()
        {
            _inputScreen._clear();
            _oldCode = "";
        }

        void _myManageData1__newDataClick()
        {
            this._oldCode = "";
            Control __codeControl = _inputScreen._getControl(this._refFieldAdd);
            if (__codeControl != null)
            {
                __codeControl.Enabled = true;
            }
            _inputScreen._focusFirst();
            if (this._afterNewData != null)
            {
                this._afterNewData(this);
            }
        }

        void _save_data()
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                if (this._manageDataScreen._editMode)
                {
                    _inputScreen._saveLastControl();
                    string __getEmtry = _inputScreen._checkEmtryField();
                    if (__getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, __getEmtry);
                    }
                    else
                    {
                        ArrayList __getData = _inputScreen._createQueryForDatabase();
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        if (this._manageDataScreen._mode == 1)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + this._manageDataScreen._dataList._tableName + " (" + this._extraInsertField + __getData[0].ToString() + ") values (" + _extraInsertData + __getData[1].ToString() + ")"));
                        }
                        else
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + this._manageDataScreen._dataList._tableName + " set " + _extraUpdateQuery + __getData[2].ToString() + this._manageDataScreen._dataList._whereString + " " + _extraUpdateWhere));
                        }
                        //
                        if (this._manageDataScreen._mode == 2 && _deleteData != null)
                        {
                            __myQuery.Append(_deleteData(this, this._oldCode));
                        }
                        if (_saveData != null)
                        {
                            __myQuery.Append(_saveData(this));
                        }
                        __myQuery.Append("</node>");
                        string __result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length == 0)
                        {
                            MyLib._myGlobal._displayWarning(1, null);
                            if (this._manageDataScreen._mode == 1)
                            {
                                this._manageDataScreen._afterInsertData();
                            }
                            else
                            {
                                this._manageDataScreen._afterUpdateData();
                            }
                            if (_afterClearData != null)
                            {
                                _afterClearData(this);
                            }
                            _inputScreen._clear();
                            _inputScreen._focusFirst();
                        }
                        else
                        {
                            MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                this._oldCode = "";
            }
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData1__discardData()
        {
            bool __result = true;
            if (_inputScreen._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    __result = false;
                }
                else
                {
                    _inputScreen._isChange = false;
                }
            }
            return (__result);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                if (this._manageDataScreen._dataList._extraWhere.Length > 0)
                {
                    whereString = whereString + " and " + this._manageDataScreen._dataList._extraWhere;
                }
                string __query = "select * from " + this._manageDataScreen._dataList._tableName + whereString;
                DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                _inputScreen._clear();
                _inputScreen._loadData(__getData.Tables[0]);
                Control __codeControl = _inputScreen._getControl(this._refFieldAdd);
                __codeControl.Enabled = false;
                this._oldCode = this._inputScreen._getDataStr(this._refFieldAdd);
                if (_loadData != null)
                {
                    _loadData(this);
                }
                if (forEdit)
                {
                    _inputScreen._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            //if (MyLib._myGlobal._isAdd)
            // {
            _save_data();
            //}
            // else
            // {
            //     MessageBox.Show("ท่านไม่มีสิทธิในการเพิ่มข้อมูลใหม่", MyLib._myGlobal._warningMessage, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        public void _changeTitle(String newTitle)
        {
            this._labelTitle.Text = newTitle;
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }


    public partial class _managerMasterScreenClass : MyLib._myScreen
    {
        public _managerMasterScreenClass()
        {
            this._maxColumn = 1;
            //
        }
    }
}
