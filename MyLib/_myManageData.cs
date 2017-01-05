using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Crom.Controls.Docking;
using System.Text;

namespace MyLib
{
    public class _myManageData : UserControl
    {
        public Boolean _readOnly = false;
        /// <summary>
        /// แก้ไขข้อมูล (true=แก้ได้,false=แก้ไม่ได้)
        /// </summary>
        public Boolean _editDataPass = true;
        /// <summary>
        /// เพิ่มได้
        /// </summary>
        public bool _isAdd = true;
        /// <summary>
        /// แก้ไขได้
        /// </summary>
        public bool _isEdit = true;
        /// <summary>
        /// ลบได้
        /// </summary>
        public bool _isDelete = true;
        public bool _isnonPermission = false;

        public _myDataList _dataList = new _myDataList();
        public _myPanel _statusTop = new _myPanel();
        public _myLabel _statusTopWording = new _myLabel();
        public Boolean _dataListOpen = true;
        public int _displayMode = 0;
        public Boolean _swapScreen = false;
        /// <summary>ประเภทการทำรายการ (1=Insert,2=Update)</summary>
        public int _mode = 1;
        /// <summary>
        /// มีการแก้ไขข้อมูล
        /// </summary>
        public bool _editMode = false;
        public Boolean _manageInsert = true;
        public Boolean _manageDelete = true;
        public Boolean _manageUpdate = true;
        public Control _manageButton;
        public MyLib._myPanel _manageBackgroundPanel;
        public Boolean _autoSize = false;
        public int _autoSizeHeight;
        private bool _showWarningEdit = true;
        private bool _isLockRecord = false;
        

        private _myFlowLayoutPanel _topPanelTemp = new _myFlowLayoutPanel();
        /// <summary>
        /// where สำหรับ Unlock
        /// </summary>
        public string _isLockWhereString = "";
        public string _mainMenuIdTemp = "";
        public string _mainMenuCodeTemp = "";
        //
        public Boolean _isLockRecordFromDatabaseActive = true;
        public string _readOnlyMessage = "หน้าจอนี้มีไว้ตรวจสอบอย่างเดียว";
        //
        public Form _form1 = new Form();
        public Form _form2 = new Form();
        public DockContainer _dock = new DockContainer();

        public string _mainMenuId
        {
            set
            {
                this._mainMenuIdTemp = value;

            }
            get
            {
                return this._mainMenuIdTemp;
            }
        }

        public string _mainMenuCode
        {
            set
            {
                this._mainMenuCodeTemp = value;

            }
            get
            {
                return this._mainMenuCodeTemp;
            }
        }

        public void _isAccessMenu()
        {
            _PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(this._mainMenuId, this._mainMenuCode);
            _isAdd = __permission._isAdd;
            _isDelete = __permission._isDelete;
            _isEdit = __permission._isEdit;
            this._dataList._mainMenuId = _mainMenuId;
            this._dataList._mainMenuCode = _mainMenuCode;
        }

        public _myManageData()
        {
            this.BackColor = Color.WhiteSmoke;
            this.BorderStyle = BorderStyle.FixedSingle;
            this._form1.BackColor = Color.WhiteSmoke;
            this._form1.Padding = new Padding(0, 0, 0, 0);
            this._form2.BackColor = Color.WhiteSmoke;
            this._form2.Padding = new Padding(0, 0, 0, 0);
            this.TabStop = false;
            //
            this._form1.Text = MyLib._myGlobal._resource("หน้าจอเลือกข้อมูล");
            this._form2.Text = MyLib._myGlobal._resource("หน้าจอบันทึก/แก้ไขข้อมูล");
            //
            int __width = this.Width; ;
            try
            {
                __width = MyLib._myGlobal._mainForm.Width;
                this._dock.Size = new System.Drawing.Size(__width, MyLib._myGlobal._mainForm.Height);
            }
            catch
            {
            }
            DockableFormInfo __formLeft = this._dock.Add(this._form1, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formLeft.ShowCloseButton = false;
            __formLeft.ShowContextMenuButton = false;
            this._dock.DockForm(__formLeft, DockStyle.Left, zDockMode.Inner);
            this._dock.SetWidth(__formLeft, __width / 2);
            //
            DockableFormInfo __formFill = this._dock.Add(this._form2, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formFill.ShowCloseButton = false;
            __formFill.ShowContextMenuButton = false;
            this._dock.DockForm(__formFill, DockStyle.Fill, zDockMode.Inner);
            //
            _dataList.Dock = DockStyle.Fill;
            _dataList.TabStop = false;
            _statusTop.TabStop = false;
            //
            this._topPanel.Font = this.Font;
            this._topPanel.Dock = DockStyle.Top;
            this._topPanel.AutoSize = true;
            //
            _dataList.TabStop = false;
            _dataList._gridData._isEdit = false;
            if (MyLib._myGlobal._isDesignMode == false)
            {
                _dataList._gridData._mouseClick += new MouseClickHandler(_gridData__mouseClick);
                _dataList._gridData._mouseDoubleClick += new MouseDoubleClickHandler(_gridData__mouseDoubleClick);
                _dataList._buttonNew.Click += new EventHandler(_buttonNew_Click);
                _dataList._buttonNewFromTemp.Click += new EventHandler(_buttonNewFromTemp_Click);
                _dataList._closeScreen += new CloseEventHandler(_dataList__closeScreen);
                _dataList._checkLockRecord += new CheckLockRecord(_dataList__checkLockRecord);
                _dataList._getDataEvent += new GetDataEventHandler(_dataList__getDataEvent);
                _dataList._gridData._vScrollBarLock = true;
                //

                // toe
                _dataList._buttonLockDoc.Click += _buttonLockDoc_Click;
                _dataList._buttonUnlockDoc.Click += _buttonUnlockDoc_Click;

                this.SizeChanged += new EventHandler(_myManageData_SizeChanged);
                _dataListOpen = false;
                _calcArea();
                _changeBackgroundColor();
                // สำหรับดูสิทธิ์
                this._mainMenuId = MyLib._myGlobal._mainMenuIdPassTrue;
                this._mainMenuCode = MyLib._myGlobal._mainMenuCodePassTrue;
                //
                _isAccessMenu();
            }
            this._dock.Dock = DockStyle.Fill;
            this.Controls.Add(this._dock);
            this._form1.Controls.Add(this._dataList);
            this._form1.Controls.Add(this._topPanel);
        }

        void _buttonUnlockDoc_Click(object sender, EventArgs e)
        {
            if (this._readOnly)
            {
                MessageBox.Show(MyLib._myGlobal._resource(this._readOnlyMessage));
                return;
            }

            if (this._dataList._isLockDoc == true)
            {
                bool __pass = true;

                StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __row = 0; __row < this._dataList._gridData._rowData.Count && __pass == true && MyLib._myGlobal._isUserLockDocument == true; __row++)
                {
                    if ((int)this._dataList._gridData._cellGet(__row, "check") == 1)
                    {
                        string __whereString = this._dataList._getRefer(__row);
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Concat("update ", _dataList._tableName, " set is_lock_record=0 ", __whereString)));
                    }
                }
                __queryList.Append("</node>");

                if (__pass && MyLib._myGlobal._isUserLockDocument == true)
                {
                    _myFrameWork __myFrameWork = new _myFrameWork();
                    __myFrameWork._queryList(_myGlobal._databaseName, __queryList.ToString());
                    _dataList._refreshData();
                }
            }
        }

        void _buttonLockDoc_Click(object sender, EventArgs e)
        {
            if (this._readOnly)
            {
                MessageBox.Show(MyLib._myGlobal._resource(this._readOnlyMessage));
                return;
            }
            if (this._dataList._isLockDoc == true)
            {
                bool __pass = true;
                StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __row = 0; __row < this._dataList._gridData._rowData.Count && __pass == true && MyLib._myGlobal._isUserLockDocument == true; __row++)
                {
                    if ((int)this._dataList._gridData._cellGet(__row, "check") == 1)
                    {
                        string __whereString = this._dataList._getRefer(__row);
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Concat("update ", _dataList._tableName, " set is_lock_record=1 ", __whereString)));
                    }
                }
                __queryList.Append("</node>");

                if (__pass && MyLib._myGlobal._isUserLockDocument == true)
                {
                    _myFrameWork __myFrameWork = new _myFrameWork();
                    __myFrameWork._queryList(_myGlobal._databaseName, __queryList.ToString());
                    _dataList._refreshData();
                }
            }
        }

        bool _dataList__checkLockRecord(int row, _myGrid sender, string whereString)
        {
            return (_checkLockRecord(row, sender, whereString));
        }

        public _myFlowLayoutPanel _topPanel
        {
            get
            {
                return this._topPanelTemp;
            }
            set
            {
                this._topPanelTemp = value;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                Keys __keyCode = (Keys)(int)keyData & Keys.KeyCode;
                if ((keyData & Keys.Control) == Keys.Control)
                {
                    switch (__keyCode)
                    {
                        case Keys.F:
                            _dataList._searchText.textBox.Focus();
                            return true;
                        case Keys.D:
                            _dataListOpen = (_dataListOpen) ? false : true;
                            _calcArea();
                            return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _dataList__getDataEvent(int row)
        {
            _getData(row);
        }

        void _dataList__closeScreen()
        {
            bool __discardData = true;
            if (_discardData != null) __discardData = _discardData();
            if (__discardData)
            {
                _unlockRecord();
                if (_closeScreen != null) _closeScreen();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unlockRecord();
            base.Dispose(disposing);
        }

        public void _newData(Boolean clear)
        {
            if (this._readOnly)
            {
                MessageBox.Show(MyLib._myGlobal._resource(this._readOnlyMessage));
                return;
            }
            if (this._isnonPermission)
            {
                bool __discardData = true;
                if (_discardData != null)
                {
                    __discardData = _discardData();
                }
                if (_manageInsert)
                {
                    if (__discardData)
                    {
                        _unlockRecord();
                        _changeBackgroundColor();
                        _dataList._referActive = false;
                        if (clear)
                        {
                            _clearDataAll(this._form2.Controls);
                            if (_clearData != null) _clearData();
                        }
                        else
                        {
                            if (_clearDataRecurring != null) _clearDataRecurring();
                        }

                        if (_manageButton != null) _manageButton.Enabled = true;
                        if (_swapScreen) _dataListOpen = false;
                        //_calcArea();
                        if (clear)
                        {
                            if (_newDataClick != null)
                                _newDataClick();
                        }
                        else
                        {
                            // toe กันหลุด จาก Recuring ให้ไป newdata ธรรมดาเลย
                            if (_newDataFromTempClick != null)
                                _newDataFromTempClick();
                            else if (_newDataClick != null)
                                _newDataClick();
                        }
                        _mode = 1;
                        _editMode = true;
                        _changeBackgroundColor();
                    }
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning19"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                //somruk กำหนดสิทธิ์ เพิ่มข้อมูลได้หรอไม่ได้
                if (this._isAdd)
                {
                    bool __discardData = true;
                    if (_discardData != null)
                    {
                        __discardData = _discardData();
                    }
                    if (_manageInsert)
                    {
                        if (__discardData)
                        {
                            _unlockRecord();
                            _changeBackgroundColor();
                            _dataList._referActive = false;
                            if (clear)
                            {
                                _clearDataAll(this._form2.Controls);
                                if (_clearData != null) _clearData();
                            }
                            else
                            {
                                if (_clearDataRecurring != null) _clearDataRecurring();
                            }
                            if (_manageButton != null) _manageButton.Enabled = true;
                            if (_swapScreen) _dataListOpen = false;
                            //_calcArea();
                            if (clear)
                            {
                                if (_newDataClick != null)
                                    _newDataClick();
                            }
                            else
                            {
                                // toe กันหลุด จาก Recuring ให้ไป newdata ธรรมดาเลย
                                if (_newDataFromTempClick != null)
                                    _newDataFromTempClick();
                                else if (_newDataClick != null)
                                    _newDataClick();
                            }
                            _mode = 1;
                            _editMode = true;
                            _changeBackgroundColor();
                        }
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("warning44"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning44"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        void _buttonNewFromTemp_Click(object sender, EventArgs e)
        {
            _newData(false);
        }

        void _changeBackgroundColor()
        {
            if (_manageBackgroundPanel != null)
            {
                _dataList._editMode = _editMode;
                if (_editMode)
                {
                    _manageBackgroundPanel._colorBegin = Color.White;
                    _manageBackgroundPanel._colorEnd = Color.WhiteSmoke;
                }
                else
                {
                    if (_myGlobal._isVersionEnum == _myGlobal._versionType.SMLHP)
                    {
                        _manageBackgroundPanel._colorBegin = Color.Honeydew;
                        _manageBackgroundPanel._colorEnd = Color.White;
                    }
                    else
                    {
                        _manageBackgroundPanel._colorBegin = Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                        _manageBackgroundPanel._colorEnd = Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    }
                }
                _manageBackgroundPanel.Invalidate();
                foreach (Control __getControl in _manageBackgroundPanel.Controls)
                {
                    if (__getControl.GetType() == typeof(MyLib._myTabControl))
                    {
                        __getControl.Invalidate();
                    }
                }
            }
        }

        void _clearDataAll(ControlCollection sender)
        {
            foreach (Control __getControl in sender)
            {
                if (__getControl.GetType() == typeof(MyLib._myScreen))
                {
                    ((MyLib._myScreen)__getControl)._clear();
                }
                else _clearDataAll(__getControl.Controls);
            }
        }

        void _buttonNew_Click(object sender, EventArgs e)
        {
            _newData(true);
        }

        public void _unlockRecord()
        {
            if (this._isLockRecord)
            {
                this.Cursor = Cursors.WaitCursor;
                _myFrameWork __myFrameWork = new _myFrameWork();
                __myFrameWork._queryInsertOrUpdate(_myGlobal._databaseName, string.Concat("update ", _dataList._tableName, " set guid_code=null", this._isLockWhereString + " and guid_code is not null "));
                this._isLockRecord = false;
                _dataList._refreshData();
                this.Cursor = Cursors.Default;
            }
        }

        bool _checkLockRecord(int row, _myGrid sender, string whereString)
        {
            bool __pass = true;
            if (this._checkEditData != null)
            {
                __pass = this._checkEditData(row, sender);
                if (__pass == false)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("delete2"));
                }
            }
            if (__pass)
            {
                string __getLockRecord = this._dataList._gridData._cellGet(row, this._dataList._isLockRecord).ToString();
                if (this._isLockRecordFromDatabaseActive && __getLockRecord.Equals("1"))
                {
                    __pass = false;
                    MessageBox.Show(MyLib._myGlobal._resource("delete3"));
                }
            }
            if (__pass)
            {
                _myFrameWork __myFrameWork = new _myFrameWork();
                String __query = string.Concat("select guid_code from ", _dataList._tableName, whereString, " and guid_code is not null");
                DataSet __getRecordReady = __myFrameWork._query(_myGlobal._databaseName, __query);
                if (__getRecordReady.Tables.Count > 0)
                {
                    if (__getRecordReady.Tables[0].Rows.Count > 0)
                    {
                        if (__getRecordReady.Tables[0].Rows[0].ItemArray[0].ToString().Length > 0)
                        {
                            __pass = false;
                            MessageBox.Show(MyLib._myGlobal._resource("warning52"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                __getRecordReady.Dispose();
            }
            return (__pass);
        }

        void _gridData__mouseDoubleClick(object sender, GridCellEventArgs e)
        {
            if (this._readOnly)
            {
                MessageBox.Show(MyLib._myGlobal._resource(this._readOnlyMessage));
                return;
            }
            if (e._row >= 0 && e._row < _dataList._gridData._maxDataRow)
            {
                string __whereString = _dataList._getRefer(e._row);
                this._isLockWhereString = __whereString;
                _myFrameWork __myFrameWork = new _myFrameWork();
                //กำหนดสิทธิ์ somruk  แก้ไขได้หรือไมได้
                if (this._isnonPermission)
                {
                    if (_dataList._referActive)
                    {
                        if (this._editDataPass)
                        {
                            bool __pass = true;
                            if (this._checkEditData != null)
                            {
                                __pass = this._checkEditData(e._row, _dataList._gridData);
                                if (__pass == false)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("warning53"));
                                }
                            }
                            if (_dataList._lockRecord && _showWarningEdit)
                            {
                                __pass = _checkLockRecord(e._row, _dataList._gridData, this._isLockWhereString);
                                if (__pass)
                                {
                                    MyLib.My_DataList._lockForEdit __lockForEdit = new MyLib.My_DataList._lockForEdit();
                                    MyLib._myUtil._startDialog(this, "Warning", __lockForEdit);
                                    this.Invalidate();
                                    __pass = __lockForEdit._selectYes;
                                    _showWarningEdit = __lockForEdit._showAgain.Checked;
                                }
                            }
                            if (__pass)
                            {
                                if (_dataList._lockRecord)
                                {
                                    __myFrameWork._queryInsertOrUpdate(_myGlobal._databaseName, string.Concat("update ", _dataList._tableName, " set guid_code=\'", _myGlobal._guid, "\'", this._isLockWhereString));
                                    _isLockRecord = true;
                                }
                                if (__pass)
                                {
                                    _mode = 2;
                                    _editMode = true;
                                    _changeBackgroundColor();
                                    if (_manageButton != null)
                                        _manageButton.Enabled = true;
                                    _clearDataAll(this._form2.Controls);
                                    if (_loadDataToScreen != null)
                                    {
                                        _loadDataToScreen(_dataList._gridData._rowData[e._row], __whereString, true);
                                    }
                                    if (_swapScreen)
                                    {
                                        _dataListOpen = false;
                                        _calcArea();
                                    }
                                    _dataList._refreshData();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("warning54"));
                        }
                    }
                }
                else
                {
                    if (this._isEdit)
                    {
                        if (_dataList._referActive)
                        {
                            if (this._editDataPass)
                            {
                                bool __pass = true;
                                if (this._checkEditData != null)
                                {
                                    __pass = this._checkEditData(e._row, _dataList._gridData);
                                    if (__pass == false)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("warning53"));
                                    }
                                }
                                if (__pass == true && _dataList._lockRecord && _showWarningEdit)
                                {
                                    __pass = _checkLockRecord(e._row, _dataList._gridData, this._isLockWhereString);
                                    if (__pass)
                                    {
                                        MyLib.My_DataList._lockForEdit __lockForEdit = new MyLib.My_DataList._lockForEdit();
                                        MyLib._myUtil._startDialog(this, MyLib._myGlobal._resource("warning"), __lockForEdit);
                                        this.Invalidate();
                                        __pass = __lockForEdit._selectYes;
                                        _showWarningEdit = __lockForEdit._showAgain.Checked;
                                    }
                                }
                                if (__pass)
                                {
                                    if (_dataList._lockRecord)
                                    {
                                        __myFrameWork._queryInsertOrUpdate(_myGlobal._databaseName, string.Concat("update ", _dataList._tableName, " set guid_code=\'", _myGlobal._guid, "\'", this._isLockWhereString));
                                        _isLockRecord = true;
                                    }
                                    if (__pass)
                                    {
                                        _mode = 2;
                                        _editMode = true;
                                        _changeBackgroundColor();
                                        if (_manageButton != null)
                                            _manageButton.Enabled = true;
                                        _clearDataAll(this._form2.Controls);
                                        if (_loadDataToScreen != null)
                                        {
                                            _loadDataToScreen(_dataList._gridData._rowData[e._row], __whereString, true);
                                        }
                                        if (_swapScreen)
                                        {
                                            _dataListOpen = false;
                                            _calcArea();
                                        }
                                        _dataList._refreshData();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("warning54"));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("warning55"));
                    }
                }

            }

        }

        /// <summary>
        /// หลังจาก Update จะต้องเรียก Function นี้
        /// </summary>
        public void _afterUpdateData()
        {
            _unlockRecord();
            if (_swapScreen)
            {
                _dataListOpen = true;
                _calcArea();
            }
            _dataList._refreshData();
            if (_manageButton != null) _manageButton.Enabled = false;
            _editMode = false;
            _displayMode = 0;
            _changeBackgroundColor();
        }

        /// <summary>
        /// หลังจาก Insert จะต้องเรียก Function นี้
        /// </summary>
        public void _afterInsertData()
        {
            _dataList._refreshData();
        }

        public event LoadDataToScreen _loadDataToScreen;
        public event NewDataEvent _newDataClick;
        public event DiscardDataEvent _discardData;
        public event CloseScreenEvent _closeScreen;
        public event ClearDataEvent _clearData;
        public event CheckEditDataEvent _checkEditData;
        public event NewDataFromTempEvent _newDataFromTempClick;
        public event ClearDataRecurringEvent _clearDataRecurring;

        /// <summary>
        /// ดึงข้อมูล
        /// </summary>
        /// <param name="row"></param>
        void _getData(int row)
        {
            if (row != -1)
            {
                bool __discardData = true;
                if (_discardData != null) __discardData = _discardData();
                if (__discardData)
                {
                    _unlockRecord();
                    try
                    {
                        for (int __loop = 0; __loop < _dataList._gridData._rowData.Count; __loop++)
                        {
                            _dataList._gridData._cellUpdate(__loop, 0, 0, false);
                        }
                        if (_manageButton != null) _manageButton.Enabled = false;
                        string __whereString = _dataList._getRefer(row);
                        if (_dataList._referActive)
                        {
                            _mode = 2;
                            _editMode = false;
                            _changeBackgroundColor();
                            _clearDataAll(this._form2.Controls);
                            if (_loadDataToScreen != null) _loadDataToScreen(_dataList._gridData._rowData[row], __whereString, false);
                        }
                    }
                    catch
                    {
                        // Debugger.Break();
                    }
                }
            }
        }

        void _gridData__mouseClick(object sender, GridCellEventArgs e)
        {
            _getData(e._row);
        }

        public void _selectDisplayMode(int displayMode)
        {
            _dataList._button.SuspendLayout();
            _dataList._button.Dock = (_displayMode == 1) ? DockStyle.Top : DockStyle.Left;
            _dataList._button.LayoutStyle = (_displayMode == 1) ? ToolStripLayoutStyle.Flow : ToolStripLayoutStyle.VerticalStackWithOverflow;
            _dataList._searchPageNumber = 0;
            _dataList._button.ResumeLayout(false);
            _calcArea();
        }

        public void _calcArea()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                try
                {
                    int __myWidth = ((this.Width) * ((_displayMode == 1) ? 20 : 45) / 100);
                    if (_autoSize && _displayMode == 1)
                    {
                        __myWidth = this.Height - _autoSizeHeight;
                    }
                    if (_manageBackgroundPanel != null)
                    {
                        _manageBackgroundPanel.Dock = DockStyle.Fill;
                    }
                    int __minsize = (_displayMode == 1) ? _dataList._button.Height + 5 : _dataList._button.Width + 5;
                }
                catch
                {
                }
            }
        }

        void _myManageData_SizeChanged(object sender, EventArgs e)
        {
            _calcArea();
        }
    }

    public delegate bool LoadDataToScreen(object rowData, string whereString, Boolean forEdit);
    public delegate void NewDataEvent();
    public delegate bool DiscardDataEvent();
    public delegate void CloseScreenEvent();
    public delegate void ClearDataEvent();
    public delegate Boolean CheckEditDataEvent(int row, MyLib._myGrid sender);
    public delegate void NewDataFromTempEvent();
    public delegate void ClearDataRecurringEvent();

}
