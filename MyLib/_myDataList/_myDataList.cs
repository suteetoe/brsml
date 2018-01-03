using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyLib
{
    public partial class _myDataList : UserControl
    {
        /// <summary>
        /// เพิ่มเครื่องหมาย quot ใน Field ตอนค้นหา
        /// </summary>
        public Boolean _addQuotWhere = false;
        /// <summary>หน้าปัจจุบัน</summary>
        public int _searchPageNumber = 0;
        /// <summary>ชื่อ Table ที่กำลังทำรายการ</summary>
        public string _tableName = "";
        /// <summary>ชื่อ Table ที่เรียกข้อมูล</summary>
        public string _tableList = "";

        /// <summary>เรียงตาม Field อะไร</summary>
        public string _getOrderBy = "";
        /// <summary>
        /// ใช้ระบบ Lock Record
        /// </summary>
        public bool _lockRecord = false;
        /// <summary>
        /// ใช้ระบบ lock เอกสาร
        /// </summary>
        public bool _isLockDoc = false;
        /// <summary>
        /// จำนวน ในบรรทัด เช่น 5*ปลาร้า
        /// </summary>
        public decimal _qty = 1;
        /// <summary>
        /// Auto Upper
        /// </summary>
        public Boolean _autoUpper = true;
        /// <summary>
        /// เลือกหลายรายการพร้อมกัน
        /// </summary>
        private bool _multiSelectTemp = false;
        public bool _multiSelect
        {
            set
            {
                this._multiSelectTemp = value;
                if (value)
                {
                    this._multiPanel.Visible = true;
                    this._gridData._mouseClickEnable = false;
                }
                else
                {
                    this._gridData._mouseClickEnable = true;
                }
            }
            get
            {
                return this._multiSelectTemp;
            }
        }

        private string _multiSelectColumnNameTemp = "";
        public string _multiSelectColumnName
        {
            get
            {
                return _multiSelectColumnNameTemp;
            }
            set
            {
                _multiSelectColumnNameTemp = value;
                // find grid column index
                _multiSelectColumnIndexTemp = this._gridData._findColumnByName(_multiSelectColumnNameTemp);
            }
        }

        private int _multiSelectColumnIndexTemp = 0;
        public int _multiSelectColumnIndex
        {
            get
            {
                return (_multiSelectColumnIndexTemp == -1) ? 0 : _multiSelectColumnIndexTemp;
            }
        }

        /// <summary>
        /// Mode แก้ไข
        /// </summary>
        public bool _editMode = false;
        int _searchPageTotal = 0;
        int _searchRecordPerPage = 0;
        int _searchTotalRecord = 0;
        string _oldText = "";
        public Boolean _referActive = false;
        public ArrayList _referFieldList = new ArrayList();
        public string _whereString = "";
        /// <summary>
        /// ใส่ filter ของ script จอ
        /// </summary>
        public string _table_list_filter = "";
        //private string _is_lock_prefix = "";

        private bool _fullModeResult = true;
        public string _extraWhereTemp = "";
        public string _extraWhere
        {
            set
            {
                this._extraWhereTemp = value;
            }
            get
            {
                if (this._extraWhereEvent == null)
                {
                    return this._extraWhereTemp;
                }
                else
                {
                    return this._extraWhereEvent();
                }
            }
        }
        public string _extraWhere2 = "";
        public bool _isnonPermission = false;
        /// <summary>
        /// ดึงข้อมูลหรือยัง
        /// </summary>
        public bool _loadViewDataSuccess = false;
        public string _screenCode;
        //
        public string _mainMenuId = "";
        public string _mainMenuCode = "";

        public Boolean _orderByRoworder = false;
        public string _isLockRecord = "is_lock_record";
        public string _guidCode = "guid_code";

        public string _isLockRecordShowColumn = "lock";


        // toe connect other server
        public string _webServiceURL = "";
        public string _ProviderName = "";
        public _myGlobal._databaseType _databaseType = _myGlobal._databaseType.PostgreSql;
        public string _databaseName = "";
        public Boolean _userOhterConnection = false;

        protected string _connectDatabaseName
        {
            get
            {
                if (_userOhterConnection == true)
                {
                    return _databaseName;
                }
                return MyLib._myGlobal._databaseName;
            }
        }

        public _myDataList()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._button.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._flowButton.Enabled = false;
                this._docPictureButton.Enabled = false;

                this._buttonNext.Click += new EventHandler(_buttonNext_Click);
                this.SizeChanged += new EventHandler(_myDataList_SizeChanged);
                this._gridData._processKeyEnable = false;
                this._gotoPage.LostFocus += new EventHandler(_gotoPage_LostFocus);
                this._gridData._mouseClick += new MouseClickHandler(_gridData__mouseClick);
                this._gridData._mouseClick2 += new MouseClickHandler(_gridData__mouseClick2);
                this._gridData._beforeDisplayRow += new BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                this.TabStop = false;
                this._searchText.textBox.TextChanged += new EventHandler(textBox_TextChanged);
                this.Load += new EventHandler(_myDataList_Load);
                _searchAuto.Checked = true;
                this._multiPanel.Visible = false;
                //
                this._selectedGrid._getResource = false;
                this._selectedGrid._addColumn("From", 1, 50, 50);
                this._selectedGrid._addColumn("To", 1, 50, 50);
                this._styleComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// ดึงค่าที่ select แบบ multi
        /// </summary>
        /// <returns></returns>
        public string _selectList()
        {
            StringBuilder __result = new StringBuilder();
            for (int __row = 0; __row < this._selectedGrid._rowData.Count; __row++)
            {
                string __from = this._selectedGrid._cellGet(__row, 0).ToString().Trim();
                string __to = this._selectedGrid._cellGet(__row, 1).ToString().Trim();
                if (__from.Length != 0 && __to.Length != 0)
                {
                    if (__result.Length > 0)
                    {
                        __result.Append(",");
                    }
                    __result.Append(__from + ":" + __to);
                }
                else
                    if (__from.Length != 0)
                {
                    if (__result.Length > 0)
                    {
                        __result.Append(",");
                    }
                    __result.Append(__from);
                }
                else
                        if (__to.Length != 0)
                {
                    if (__result.Length > 0)
                    {
                        __result.Append(",");
                    }
                    __result.Append(__to);
                }
            }
            return __result.ToString();
        }

        void _sortGrid(GridCellEventArgs e)
        {
            if (e._row == -1 && e._column != -1)
            {
                MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)_gridData._columnList[e._column];
                string __newOrder = this._columnRepackOrderBy(__getColumnType, "");
                if (__newOrder.CompareTo(this._getOrderBy) == 0)
                {
                    this._getOrderBy = this._columnRepackOrderBy(__getColumnType, "desc");
                }
                else
                {
                    this._getOrderBy = __newOrder;
                }
                _loadViewData(0, this._extraWhere);
            }
        }

        void _gridData__mouseClick(object sender, GridCellEventArgs e)
        {
            this._sortGrid(e);
        }

        void _gridData__mouseClick2(object sender, GridCellEventArgs e)
        {
            // Multi Select
            this._sortGrid(e);
            if (e._row > -1)
            {
                if (this._selectedGrid._rowData.Count == 0)
                {
                    this._selectedGrid._addRow();
                    this._selectedGrid._selectColumn = 0;
                    this._selectedGrid._selectRow = 0;
                }
                if (this._selectedGrid._selectColumn != -1 && this._selectedGrid._selectRow != -1)
                {
                    string __data = this._gridData._cellGet(e._row, _multiSelectColumnIndexTemp).ToString();
                    this._selectedGrid.Select();
                    this._selectedGrid._cellUpdate(this._selectedGrid._selectRow, this._selectedGrid._selectColumn, __data, false);
                    this._selectedGrid.Invalidate();
                    SendKeys.Send((this._styleComboBox.SelectedIndex == 0) ? "{Down}" : "{TAB}");
                }
            }
        }

        void _myDataList_Load(object sender, EventArgs e)
        {
            this._timerLoadData.Enabled = true;
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            this._gridData._selectRow = -1;
        }

        string _from_str = "_form";
        string _to_str = "_to";

        void _filterScreen()
        {
            List<string> __field = new List<string>();
            List<string> __fieldForm = new List<string>();
            List<string> __fieldTo = new List<string>();
            List<int> __fieldType = new List<int>();
            //
            _filterForm __form = new _filterForm();
            __form.TopMost = true;
            __form._screen._table_name = this._gridData._table_name;
            __form._screen._maxColumn = 2;
            int __row = 0;
            for (int __loop = 0; __loop < this._gridData._columnList.Count; __loop++)
            {
                MyLib._myGrid._columnType __column = (MyLib._myGrid._columnType)this._gridData._columnList[__loop];
                if (__column._isHide == false)
                {
                    Boolean __created = false;
                    switch (__column._type)
                    {
                        case 1: // String
                            __form._screen._addTextBox(__row, 0, 0, 0, __column._originalName + this._from_str, 1, 1, 0, true, false, false, false, true, __column._name);
                            __form._screen._addTextBox(__row, 1, 0, 0, __column._originalName + this._to_str, 1, 1, 0, true, false, false, false, true, __column._name);
                            __created = true;
                            break;
                        case 3:
                        case 5: // Decimal
                            __form._screen._addNumberBox(__row, 0, 0, 0, __column._originalName + this._from_str, 1, 2, true, "", false, __column._name);
                            __form._screen._addNumberBox(__row, 1, 0, 0, __column._originalName + this._to_str, 1, 2, true, "", false, __column._name);
                            __created = true;
                            break;
                        case 4: // Date
                            __form._screen._addDateBox(__row, 0, 0, 0, __column._originalName + this._from_str, 1, true, true, false, __column._name);
                            __form._screen._addDateBox(__row, 1, 0, 0, __column._originalName + this._to_str, 1, true, true, false, __column._name);
                            __created = true;
                            break;
                    }
                    if (__created)
                    {
                        __row++;
                        __field.Add((__column._searchField != null && __column._searchField.Length > 0) ? __column._searchField : __column._originalName);
                        __fieldForm.Add(__column._originalName + this._from_str);
                        __fieldTo.Add(__column._originalName + this._to_str);
                        __fieldType.Add(__column._type);
                    }
                }
            }
            __form._clearButton.Click += (s1, e1) =>
            {
                __form._screen._clear();
                this._extraWhere2 = "";
                this._refreshData();
            };
            __form._closeButton.Click += (s1, e1) =>
            {
                __form.Dispose();
            };
            __form._filterButton.Click += (s1, e1) =>
            {
                StringBuilder __extraQuery = new StringBuilder();
                for (int __loop = 0; __loop < __fieldType.Count; __loop++)
                {
                    string __queryWhere = "";
                    switch (__fieldType[__loop])
                    {
                        case 1:
                            {
                                // String
                                string __formValue = __form._screen._getDataStr(__fieldForm[__loop]).Trim();
                                string __toValue = __form._screen._getDataStr(__fieldTo[__loop]).Trim();
                                if (__formValue.Length > 0 && __toValue.Length > 0)
                                {
                                    __queryWhere = __field[__loop] + " between \'" + __formValue + "\' and \'" + __toValue + "\'";
                                }
                                else
                                {
                                    if (__formValue.Length > 0)
                                    {
                                        __queryWhere = __field[__loop] + " >= \'" + __formValue + "\'";
                                    }
                                    else
                                    {
                                        if (__toValue.Length > 0)
                                        {
                                            __queryWhere = __field[__loop] + " <= \'" + __toValue + "\'";
                                        }
                                    }
                                }
                            }
                            break;
                        case 3:
                        case 5:
                            {
                                // Decimal
                                decimal __formValue = __form._screen._getDataNumber(__fieldForm[__loop]);
                                decimal __toValue = __form._screen._getDataNumber(__fieldTo[__loop]);
                                if (__formValue != 0 && __toValue != 0)
                                {
                                    __queryWhere = __field[__loop] + " between " + __formValue + " and " + __toValue;
                                }
                                else
                                {
                                    if (__formValue != 0)
                                    {
                                        __queryWhere = __field[__loop] + " >= " + __formValue;
                                    }
                                    else
                                    {
                                        if (__toValue != 0)
                                        {
                                            __queryWhere = __field[__loop] + " <= " + __toValue;
                                        }
                                    }
                                }
                            }
                            break;
                        case 4:
                            {
                                // Date
                                string __formValue = __form._screen._getDataStr(__fieldForm[__loop]).Trim();
                                string __toValue = __form._screen._getDataStr(__fieldTo[__loop]).Trim();
                                string __formValueQuery = __form._screen._getDataStrQuery(__fieldForm[__loop]).Trim();
                                string __toValueQuery = __form._screen._getDataStrQuery(__fieldTo[__loop]).Trim();
                                if (__formValue.Length > 0 && __toValue.Length > 0)
                                {
                                    __queryWhere = __field[__loop] + " between " + __formValueQuery + " and " + __toValueQuery;
                                }
                                else
                                {
                                    if (__formValue.Length > 0)
                                    {
                                        __queryWhere = __field[__loop] + " >= " + __formValueQuery;
                                    }
                                    else
                                    {
                                        if (__toValue.Length > 0)
                                        {
                                            __queryWhere = __field[__loop] + " <= " + __toValueQuery;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                    if (__queryWhere.Length > 0)
                    {
                        if (__extraQuery.Length > 0)
                        {
                            __extraQuery.Append(" and ");
                        }
                        __extraQuery.Append("(" + __queryWhere + ")");
                    }
                }
                this._extraWhere2 = __extraQuery.ToString();
                this._refreshData();
            };
            __form.Show();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                this._timer.Stop();
                this._timer.Start();
                switch (keyData)
                {
                    case Keys.F2:
                        this._filterScreen();
                        return true;
                    case Keys.Control | Keys.F:
                        if (_controlKeyEvent != null)
                        {
                            _controlKeyEvent(this._gridData, Keys.F);
                            return true;
                        }
                        break;
                    case Keys.Enter:
                    case Keys.Tab:
                        if (SelectRow == -1)
                        {
                            _searchPageNumber = 0;
                            _loadViewData(0, this._extraWhere);
                            return true;
                        }
                        break;
                    case Keys.PageDown:
                        _nextPage();
                        _gridData.Invalidate();
                        return true;
                    case Keys.PageUp:
                        _prevPage();
                        _gridData.Invalidate();
                        return true;
                    case Keys.Down:
                        _nextRecord();
                        _gridData.Invalidate();
                        return true;
                    case Keys.Up:
                        _prevRecord();
                        _gridData.Invalidate();
                        return true;
                    case Keys.Home:
                        _searchPageNumber = 0;
                        _loadViewData(_searchPageNumber * _searchRecordPerPage, this._extraWhere);
                        _gridData.Invalidate();
                        return true;
                    case Keys.End:
                        if (_searchPageTotal > 1)
                        {
                            _searchPageNumber = _searchPageTotal - 1;
                            _loadViewData(_searchPageNumber * _searchRecordPerPage, this._extraWhere);
                        }
                        _gridData.Invalidate();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        [Category("_SML")]
        [Description("True=ทำงานเต็มรูปแบบ,False=ใช้สำหรับการค้นหาเท่านั้น")]
        [DisplayName("Full Mode : ทำได้ทุกอย่าง")]
        [DefaultValue(true)]
        public bool _fullMode
        {
            get
            {
                return _fullModeResult;
            }
            set
            {
                _fullModeResult = value;
                this._buttonNew.Visible = _fullModeResult;
                this._buttonNewFromTemp.Visible = _fullModeResult;
                this._buttonDelete.Visible = _fullModeResult;
                this._gridData._isEdit = _fullModeResult;
            }
        }

        private bool _showIsLockColumnResult = false;
        public Boolean _showIsLockColumn
        {
            get
            {
                return _showIsLockColumnResult;
            }

            set
            {
                _showIsLockColumnResult = value;
            }
        }

        BeforeDisplayRowReturn _gridData__beforeDisplayRow(_myGrid sender, int row, int columnNumber, string columnName, BeforeDisplayRowReturn senderRow, _myGrid._columnType columnType, ArrayList rowData)
        {
            BeforeDisplayRowReturn __result = senderRow;
            try
            {
                string __getLockRecord = this._gridData._cellGet(row, this._isLockRecord).ToString();
                if (__getLockRecord.Equals("1"))
                {
                    if (_showIsLockColumnResult == false)
                    {
                        __result.newColor = Color.Maroon;
                        //this._gridData._cellGet(row, this._isLockRecord).ToString();
                    }
                }
                if (this._lockRecord)
                {
                    string guid = this._gridData._cellGet(row, this._guidCode).ToString();
                    if (guid.Length > 0)
                    {
                        __result.newColor = Color.DarkOrange;
                        this._gridData._cellGet(row, this._guidCode).ToString();
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + __ex.StackTrace.ToString());
            }
            return (__result);
        }

        /// <summary>
        /// เพิ่ม Field ในการอ้างอิง จะต้องห้ามซ้ำ
        /// </summary>
        /// <param name="fieldName">ชื่อ Field</param>
        /// <param name="fieldType">ประเภท Field (1=String,2=Date,3=Number,4=Int)</param>
        public void _referFieldAdd(string fieldName, int fieldType)
        {
            _myGlobal._referFieldType __refField = new _myGlobal._referFieldType();
            __refField._fieldName = fieldName;
            __refField._fieldDataType = fieldType;
            __refField._fieldData = null;
            this._referFieldList.Add(__refField);
        }

        void _gotoPage_LostFocus(object sender, EventArgs e)
        {
            try
            {
                _searchPageNumber = Convert.ToInt32(this._gotoPage.Text) - 1;
                if (_searchPageNumber < 0 || _searchPageNumber > _searchPageTotal)
                    _searchPageNumber = 0;
                _loadViewData(_searchPageNumber * _searchRecordPerPage, this._extraWhere);
            }
            catch
            {
                // Debugger.Break();
            }
        }

        public int SelectRow
        {
            get
            {
                return _gridData.SelectRow;
            }
        }

        /// <summary>
        /// รายการต่อไป
        /// </summary>
        void _nextRecord()
        {
            if (_gridData._selectRow < _gridData._rowData.Count - 1)
            {
                _gridData._selectRow++;
                if (_getDataEvent != null)
                {
                    _getDataEvent(_gridData._selectRow);
                }
            }
            else
            {
                _nextPage();
                _gridData._selectRow = 0;
            }
        }

        /// <summary>
        /// ข้อมูลรายการที่แล้ว
        /// </summary>
        void _prevRecord()
        {
            if (_gridData._selectRow > 0)
            {
                _gridData._selectRow--;
                if (_getDataEvent != null)
                {
                    _getDataEvent(_gridData._selectRow);
                }
            }
            else
            {
                _prevPage();
                _gridData._selectRow = _gridData._rowData.Count - 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (_searchAuto.Checked)
                {
                    if (_oldText.CompareTo(this._searchText.textBox.Text) != 0)
                    {
                        _oldText = this._searchText.textBox.Text;
                        _searchPageNumber = 0;
                        _loadViewData(0, this._extraWhere);
                    }
                }
            }
        }

        /// <summary>
        /// คำนวณตำแหน่งใหม่
        /// </summary>
        public void _recalcPosition()
        {
            Graphics __e = this.CreateGraphics();
            SizeF __stringSize1 = __e.MeasureString(this._searchTextWord.Text, _myGlobal._myFont);
            SizeF __stringSize2 = __e.MeasureString(this._infoLabel.Text, _myGlobal._myFont);
            SizeF __stringSize3 = __e.MeasureString(this._searchAuto.Text, _myGlobal._myFont);
            float __allWidth = __stringSize1.Width + __stringSize2.Width + __stringSize3.Width + this._filterButton.Width;
            this._searchText.Width = this._searchPanel.Width - ((int)__allWidth + 60);
            _searchRecordPerPage = ((int)(_gridData._calcHeight() / _gridData._cellHeight)) - 1;
        }

        void _myDataList_SizeChanged(object sender, EventArgs e)
        {
            _recalcPosition();
        }

        /// <summary>
        /// แสดงข้อมูลใหม่ หลังจาก insert,update,delete
        /// </summary>
        public void _refreshData()
        {
            _loadViewData(_searchPageNumber * _searchRecordPerPage, this._extraWhere);
        }

        /// <summary>
        /// ข้อมูลหน้าที่แล้ว
        /// </summary>
        public void _prevPage()
        {
            if (_searchPageNumber > 0)
            {
                _searchPageNumber--;
                _loadViewData(_searchPageNumber * _searchRecordPerPage, this._extraWhere);
            }
        }

        /// <summary>
        /// ข้อมูลหน้าต่อไป
        /// </summary>
        public void _nextPage()
        {
            if (_searchPageNumber < _searchPageTotal - 1)
            {
                _searchPageNumber++;
                _loadViewData(_searchPageNumber * _searchRecordPerPage, this._extraWhere);
            }
        }

        void _buttonNext_Click(object sender, EventArgs e)
        {
            _nextPage();
        }

        private void _buttonPrev_Click(object sender, EventArgs e)
        {
            _prevPage();
        }

        private void _gridData_Load(object sender, EventArgs e)
        {

        }

        public void _loadViewData(int recordTop)
        {
            _loadViewData(recordTop, this._extraWhere);
        }

        private string _repackOrderBy(string name, string originalName, string desc)
        {
            StringBuilder __createOrderBy = new StringBuilder();
            string[] __orderBySplit = (name == null || name.Length == 0) ? originalName.Split(',') : name.Split(',');
            for (int __orderByPack = 0; __orderByPack < __orderBySplit.Length; __orderByPack++)
            {
                if (__orderByPack != 0)
                {
                    __createOrderBy.Append(",");
                }

                // toe check desc inline
                if (__orderBySplit[__orderByPack].ToString().IndexOf(" desc") != -1)
                {
                    __createOrderBy.Append(string.Concat("\"", __orderBySplit[__orderByPack].ToString().Replace(" desc", string.Empty), "\" ", "desc"));
                }
                else
                    __createOrderBy.Append(string.Concat("\"", __orderBySplit[__orderByPack].ToString(), "\" ", desc));
            }
            return __createOrderBy.ToString();
        }

        private string _columnRepackOrderBy(MyLib._myGrid._columnType source, string desc)
        {
            return _repackOrderBy(source._orderBy, source._originalName, desc);
        }

        public void _loadViewData(int recordTop, string extraWhere)
        {
            if (this._tableName.Length == 0 || this._connectDatabaseName.Length == 0)
            {
                return;
            }
            try
            {
                this._extraWhere = extraWhere;
                this._loadViewDataSuccess = true;
                StringBuilder __queryForCount = new StringBuilder();
                StringBuilder __query = new StringBuilder();
                __queryForCount.Append("select count(*) as rowcount ");
                __query.Append("select ");
                string __searchTextTrim = this._searchText.textBox.Text.Trim();
                this._qty = 1;
                if (__searchTextTrim.IndexOf('*') != -1)
                {
                    __searchTextTrim = __searchTextTrim.Replace("*", @"\*");
                    //string __qtyStr = __searchTextTrim.Split('*')[0];
                    //this._qty = MyLib._myGlobal._decimalPhase(__qtyStr);
                    //__searchTextTrim = __searchTextTrim.Remove(0, __qtyStr.Length + 1);
                }
                string[] __searchTextSplit = __searchTextTrim.Split(' ');
                this.Cursor = Cursors.WaitCursor;
                _searchTotalRecord = 0;
                _infoLabel.Text = "";
                Boolean __guid = true;
                // ประกอบ query
                Boolean __firstField = false;
                for ( int __loop = 0; __loop < _gridData._columnList.Count; __loop++)
                {
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)_gridData._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true)
                    {
                        if (__guid == true && this._lockRecord)
                        {
                            __guid = false;
                            __query.Append(this._guidCode + ((this._tableList.Equals(this._tableName)) ? "" : " as \"" + this._guidCode + "\"") + ","); // โต๋ เพิ่มให้ support multitable query
                            __firstField = true;
                        }
                        else
                        {
                            if (__firstField)
                            {
                                __query.Append(",");
                            }
                            __firstField = true;
                        }
                        if (this._getOrderBy.Length == 0)
                        {
                            this._getOrderBy = this._columnRepackOrderBy(__getColumnType, "");
                        }
                        __query.Append(string.Concat(__getColumnType._query, " as \"", __getColumnType._originalName, "\""));
                    }
                } // for
                // ประกอบ where
                StringBuilder __where = new StringBuilder();
                if (__searchTextSplit.Length > 1)
                {
                    // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                    for (int __loop = 0; __loop < _gridData._columnList.Count; __loop++)
                    {
                        bool __whereFirst = false;
                        MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)_gridData._columnList[__loop];
                        if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                        {
                            if (__getColumnType._isHide == false)
                            {
                                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                                bool __first2 = false;
                                for (int __searchIndex = 0; __searchIndex < __searchTextSplit.Length; __searchIndex++)
                                {
                                    if (__searchTextSplit[__searchIndex].Length > 0)
                                    {
                                        string __getValue = __searchTextSplit[__searchIndex].ToUpper();
                                        string __newDateValue = __getValue;
                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                        {
                                            __newDateValue = __getValue.ToString().Remove(0, 1);
                                        }
                                        switch (__getColumnType._type)
                                        {
                                            case 2: // Number
                                            case 3:
                                            case 5:
                                                //
                                                decimal __newValue = 0M;
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newValue = MyLib._myGlobal._decimalPhase(__newDateValue);
                                                        //
                                                        if (__newValue != 0)
                                                        {
                                                            if (__whereFirst == false)
                                                            {
                                                                if (__where.Length > 0)
                                                                {
                                                                    __where.Append(" or ");
                                                                }
                                                                __where.Append("(");
                                                                __whereFirst = true;
                                                            }
                                                            if (__first2)
                                                            {
                                                                __where.Append(" and ");
                                                            }
                                                            __first2 = true;
                                                            //
                                                            if (this._addQuotWhere)
                                                                __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                            else
                                                                __where.Append(string.Concat(__getColumnType._query));
                                                            //
                                                            if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                                __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                            else
                                                                __getValue = String.Concat("=", __newValue.ToString());
                                                            __where.Append(__getValue);
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 4: // Date
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        DateTime __test = DateTime.Parse(__newDateValue, _myGlobal._cultureInfo());
                                                        //
                                                        if (__whereFirst == false)
                                                        {
                                                            if (__where.Length > 0)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __where.Append("(");
                                                            __whereFirst = true;
                                                        }
                                                        if (__first2)
                                                        {
                                                            __where.Append(" and ");
                                                        }
                                                        __first2 = true;
                                                        //
                                                        if (this._addQuotWhere)
                                                            __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        else
                                                            __where.Append(string.Concat(__getColumnType._query));
                                                        DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                        {
                                                            __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                            __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                        else
                                                        {
                                                            __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            default:// String
                                                if (__whereFirst == false)
                                                {
                                                    if (__where.Length > 0)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __where.Append("(");
                                                    __whereFirst = true;
                                                }
                                                if (__first2)
                                                {
                                                    __where.Append(" and ");
                                                }
                                                __first2 = true;
                                                //
                                                //if (this._addQuotWhere)
                                                //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                //else
                                                //    __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                                // check can upper 
                                                // concept คือ หาก ทำการ upper และ lower แล้วได้ค่าไม่ต่างกัน ก็ไม่ต้องใส่ upper เข้าไปข้างใน
                                                // แต่ถ้าได้ค่าต่างกัน ก็ทำ or 2 ครั้ง คือ field like '%TOUPPER()%' or field like '%TOLOWER()%'
                                                // ทดสอบแล้วเร็วกว่า ใส่ upper 10%


                                                if (__newDateValue.ToUpper() == __newDateValue.ToLower())
                                                {

                                                    // no upper mode
                                                    if (this._addQuotWhere)
                                                        __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    else
                                                        __where.Append(string.Concat(" " + __getColumnType._query + " "));

                                                    if (__searchTextTrim[0] == '+')
                                                    {
                                                        __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                    }
                                                }
                                                else
                                                {
                                                    // upper mode โดยแก้ไข ทำ where 2 ครั้ง
                                                    if (this._addQuotWhere)
                                                        __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    else
                                                        __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));

                                                    if (__searchTextTrim[0] == '+')
                                                    {
                                                        __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                    }
                                                }

                                                break;
                                        }
                                    }
                                }
                                if (__whereFirst)
                                {
                                    __where.Append(")");
                                }
                            }
                        }
                    } // for
                }
                else
                {
                    bool __whereFirst = false;
                    for (int __loop = 0; __loop < _gridData._columnList.Count; __loop++)
                    {
                        MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)_gridData._columnList[__loop];
                        if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                        {
                            if (__getColumnType._isHide == false)
                            {
                                // กรณีการค้นหาตัวเดียว
                                if (__searchTextTrim.Length > 0)
                                {
                                    try
                                    {
                                        string __getValue = __searchTextTrim;
                                        string __newDateValue = __getValue;
                                        Boolean __valueExtra = false;
                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                        {
                                            __newDateValue = __getValue.ToString().Remove(0, 1);
                                            __valueExtra = true;
                                        }
                                        switch (__getColumnType._type)
                                        {
                                            case 2: // Number
                                            case 3:
                                            case 5:
                                                double __newValue = 0;
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newValue = Double.Parse(__newDateValue);
                                                        //
                                                        if (__newValue != 0)
                                                        {
                                                            if (__whereFirst == true)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __whereFirst = true;
                                                            //
                                                            if (this._addQuotWhere)
                                                                __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                            else
                                                                __where.Append(string.Concat(__getColumnType._query));
                                                            //
                                                            if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                                __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                            else
                                                                __getValue = String.Concat("=", __newValue.ToString());
                                                            __where.Append(__getValue);
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 4: // Date
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        DateTime __test = DateTime.Parse(__newDateValue, _myGlobal._cultureInfo());
                                                        //
                                                        if (__whereFirst == true)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __whereFirst = true;
                                                        //
                                                        if (this._addQuotWhere)
                                                            __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        else
                                                            __where.Append(string.Concat(__getColumnType._query));
                                                        DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                        {
                                                            __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                            __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                        else
                                                        {
                                                            __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            default:// String
                                                //
                                                if (__valueExtra == false)
                                                {
                                                    if (__whereFirst == true)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __whereFirst = true;
                                                    //

                                                    // check can upper 
                                                    // concept คือ หาก ทำการ upper และ lower แล้วได้ค่าไม่ต่างกัน ก็ไม่ต้องใส่ upper เข้าไปข้างใน
                                                    // แต่ถ้าได้ค่าต่างกัน ก็ทำ or 2 ครั้ง คือ field like '%TOUPPER()%' or field like '%TOLOWER()%'
                                                    // ทดสอบแล้วเร็วกว่า ใส่ upper 10%


                                                    if (__newDateValue.ToUpper() == __newDateValue.ToLower())
                                                    {
                                                        if (this._addQuotWhere)
                                                            __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        else
                                                            __where.Append(string.Concat(" " + __getColumnType._query + " "));

                                                    }
                                                    else
                                                    {
                                                        if (this._addQuotWhere)
                                                            __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        else
                                                            __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                                    }

                                                    if (__searchTextTrim[0] == '+')
                                                    {
                                                        __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    } // for
                }
                if (__where.Length > 0)
                {
                    __where = new StringBuilder("(" + __where.ToString() + ")");
                }
                __query.Append(string.Concat(" from ", (this._tableName.Equals(this._tableList) || this._tableList.Length == 0) ? this._tableName : this._tableList));
                __queryForCount.Append(string.Concat(" from ", (this._tableName.Equals(this._tableList) || this._tableList.Length == 0) ? this._tableName : this._tableList));
                if (__where.Length > 0)
                {
                    __query.Append(string.Concat(" where (", __where, ")"));
                    __queryForCount.Append(string.Concat(" where (", __where, ")"));
                    if (this._extraWhere.Length > 0)
                    {
                        __query.Append(string.Concat(" and ", this._extraWhere));
                        __queryForCount.Append(string.Concat(" and ", this._extraWhere));
                    }
                    if (this._extraWhere2.Length > 0)
                    {
                        __query.Append(" and (" + this._extraWhere2 + ")");
                        __queryForCount.Append(" and (" + this._extraWhere2 + ")");
                    }

                    if (this._table_list_filter.Length > 0)
                    {
                        __query.Append(" and (" + this._table_list_filter + ")");
                        __queryForCount.Append(" and (" + this._table_list_filter + ")");
                    }
                }
                else
                {
                    Boolean __whereAdded = false;
                    if (this._extraWhere.Length > 0)
                    {
                        __query.Append(string.Concat(" where ", "(" + this._extraWhere + ")"));
                        __queryForCount.Append(string.Concat(" where ", "(" + this._extraWhere + ")"));
                        __whereAdded = true;
                    }
                    if (this._extraWhere2.Length > 0)
                    {
                        if (this._extraWhere.Length == 0)
                        {
                            if (__whereAdded == false)
                            {
                                __query.Append(" where ");
                                __queryForCount.Append(" where ");
                                __whereAdded = true;
                            }
                        }
                        else
                        {
                            if (__whereAdded == false)
                            {
                                __query.Append(" where ");
                                __queryForCount.Append(" where ");
                                __whereAdded = true;
                            }
                            else
                            {
                                __query.Append(" and ");
                                __queryForCount.Append(" and ");
                            }
                        }
                        __query.Append("(" + this._extraWhere2 + ")");
                        __queryForCount.Append("(" + this._extraWhere2 + ")");
                    }

                    // toe
                    if (this._table_list_filter.Length > 0)
                    {
                        if (__whereAdded == false)
                        {
                            if (this._extraWhere.Length == 0)
                            {
                                __query.Append(" where ");
                                __queryForCount.Append(" where ");
                                __whereAdded = true;
                            }

                            if (this._extraWhere2.Length == 0 && __whereAdded == false)
                            {
                                __query.Append(" where ");
                                __queryForCount.Append(" where ");
                                __whereAdded = true;
                            }
                        }
                        else
                        {
                            __query.Append(" and ");
                            __queryForCount.Append(" and ");
                        }

                        __query.Append(" (" + this._table_list_filter + ")");
                        __queryForCount.Append(" (" + this._table_list_filter + ")");

                    }
                }
                __query.Append(string.Concat(" order by ", this._getOrderBy));
                if (this._orderByRoworder)
                {
                    __query.Append(",roworder");
                }
                MyLib._myFrameWork __myFrameWork;
                // toe connect other server
                if (this._userOhterConnection)
                {
                    __myFrameWork = new _myFrameWork(this._webServiceURL, this._ProviderName, this._databaseType);
                }
                else
                {
                    __myFrameWork = new _myFrameWork();
                }
                MyLib._queryReturn __dataResult = null;
                if (this._ownerQuery == null)
                {
                    __dataResult = __myFrameWork._queryLimit(this._connectDatabaseName, __queryForCount.ToString(), __query.ToString(), recordTop, _searchRecordPerPage, 1);
                }
                else
                {
                    string __whereStr = __where.ToString();
                    /*if (__whereStr.Length != 0)
                    {
                        __whereStr = " where " + __whereStr;
                    }*/
                    __dataResult = this._ownerQuery(__whereStr, " order by " + this._getOrderBy + ((this._orderByRoworder == true) ? ",roworder" : ""), recordTop, _searchRecordPerPage);
                }
                _gridData._loadFromDataTable(__dataResult.detail.Tables[0]);
                _gridData._rowStartNumber = recordTop;
                _searchPageTotal = (__dataResult.totalRecord / _searchRecordPerPage) + 1;
                _searchTotalRecord = __dataResult.totalRecord;
                this._gotoPage.Text = string.Format("{0}", _searchPageNumber + 1);
                _infoLabel.Text = string.Format("{0:#,0} ({1:#,0}/{2:#,0})", __dataResult.totalRecord, _searchPageNumber + 1, _searchPageTotal);
                _recalcPosition();
                //
                this.Cursor = Cursors.Default;
                this._gridData.Invalidate();

                __dataResult.detail.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }
        }

        /// <summary>
        /// เรียกหน้าจอค้นหาข้อมูลจาก Database (View)
        /// </summary>
        /// <param name="screenCode">รหัสหน้าจอ</param>
        /// <param name="groupCode">กลุ่มจอภาพ (ถ้าไม่มี โปรแกรมจะเอากลุ่มแรก)</param>
        public void _loadViewFormat(string screenCode, int groupCode, Boolean checkBoxFirstColumn)
        {
            string __query = "";
            string __queryCustomView = "";
            bool __customScreen = false;
            //string __table_view_format_name  = "";

            if (MyLib._myGlobal._isDesignMode == false && this._connectDatabaseName.Length > 0)
            {
                this._screenCode = screenCode;
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    // toe connect other server
                    if (this._userOhterConnection)
                    {
                        __myFrameWork = new _myFrameWork(this._webServiceURL, this._ProviderName, this._databaseType);
                    }
                    __query = string.Concat("select * from ", MyLib._myGlobal._tableNameView, ",", MyLib._myGlobal._tableNameViewColumn, " where ", _myGlobal._addUpper(MyLib._myGlobal._tableNameView + ".screen_code") + "=\'", screenCode.ToUpper(), "\' and (screen_group=1 or " + "screen_group" + "=", groupCode.ToString().ToUpper(), ") and (", _myGlobal._addUpper(MyLib._myGlobal._tableNameView + ".screen_code") + "=", _myGlobal._addUpper(MyLib._myGlobal._tableNameViewColumn + ".screen_code") + ") order by column_number");
                    __queryCustomView = string.Concat("select * from ", MyLib._myGlobal._tableCustomNameView, ",", MyLib._myGlobal._tableCustomNameViewColumn, " where ", _myGlobal._addUpper(MyLib._myGlobal._tableCustomNameView + ".screen_code") + "=\'", screenCode.ToUpper(), "\' and (screen_group=1 or " + "screen_group" + "=", groupCode.ToString().ToUpper(), ") and (", _myGlobal._addUpper(MyLib._myGlobal._tableCustomNameView + ".screen_code") + "=", _myGlobal._addUpper(MyLib._myGlobal._tableCustomNameViewColumn + ".screen_code") + ") order by column_number");

                    StringBuilder __queryStr = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    __queryStr.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                    if (MyLib._myGlobal._tableCustomNameView != null && MyLib._myGlobal._tableCustomNameViewColumn != null && MyLib._myGlobal._tableCustomNameView.Length > 0 && MyLib._myGlobal._tableCustomNameViewColumn.Length > 0)
                    {
                        __queryStr.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryCustomView));
                    }
                    __queryStr.Append("</node>");

                    // toe custom erp_view_column
                    ArrayList __result = __myFrameWork._queryListGetData(this._connectDatabaseName, __queryStr.ToString());
                    DataSet __dataResult = (DataSet)__result[0];
                    if (MyLib._myGlobal._tableCustomNameView != null && MyLib._myGlobal._tableCustomNameViewColumn != null && MyLib._myGlobal._tableCustomNameView.Length > 0 && MyLib._myGlobal._tableCustomNameViewColumn.Length > 0)
                    {
                        // compare
                        DataSet __dataCustomResult = (DataSet)__result[1];
                        if (__dataCustomResult.Tables.Count > 0 && __dataCustomResult.Tables[0].Rows.Count > 0)
                        {
                            __dataResult = (DataSet)__result[1];
                            __customScreen = true;
                        }
                    }

                    //DataSet __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    if (__dataResult.Tables.Count > 0 && __dataResult.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] __dataRow = __dataResult.Tables[0].Select(string.Concat("screen_group=", groupCode));
                        if (__dataRow.Length == 0)
                        {
                            __dataRow = __dataResult.Tables[0].Select("screen_group=1");
                        }
                        int __width_persent_ordinal = __dataResult.Tables[0].Columns["width_persent"].Ordinal;
                        int __column_name_ordinal = __dataResult.Tables[0].Columns["column_name"].Ordinal;
                        int __column_field_name_ordinal = __dataResult.Tables[0].Columns["column_field_name"].Ordinal;
                        int __column_field_name_sort_ordinal = __dataResult.Tables[0].Columns["column_field_sort"].Ordinal;
                        int __column_width_ordinal = __dataResult.Tables[0].Columns["column_width"].Ordinal;
                        int __column_type_ordinal = __dataResult.Tables[0].Columns["column_type"].Ordinal;
                        int __column_format_ordinal = __dataResult.Tables[0].Columns["column_format"].Ordinal;
                        int __column_resource = __dataResult.Tables[0].Columns["column_resource"].Ordinal;
                        int __column_table_filter = __dataResult.Tables[0].Columns["filter"].Ordinal;
                        int __column_view_format_table_name = __dataResult.Tables[0].Columns["table_list"].Ordinal;
                        int __column_field_search_ordinal = __dataResult.Tables[0].Columns["column_field_search"].Ordinal;

                        _gridData._width_by_persent = (__dataRow[0].ItemArray[__width_persent_ordinal].ToString().CompareTo("0") == 0) ? false : true;
                        this._tableName = __dataRow[0].ItemArray[__dataResult.Tables[0].Columns["table_name"].Ordinal].ToString();

                        this._table_list_filter = __dataRow[0].ItemArray[__column_table_filter].ToString();
                        this._tableList = __dataRow[0].ItemArray[__column_view_format_table_name].ToString();

                        if (__customScreen)
                        {
                            try
                            {
                                int __column_sort = __dataResult.Tables[0].Columns["sort"].Ordinal;
                                int __column_sort_mode = __dataResult.Tables[0].Columns["sort_mode"].Ordinal;
                                string __sort_field = __dataRow[0].ItemArray[__column_sort].ToString();
                                string __sort_mode = __dataRow[0].ItemArray[__column_sort_mode].ToString().Equals("1") ? "desc" : "";
                                if (__sort_field.Length > 0)
                                {
                                    this._getOrderBy = this._repackOrderBy(__sort_field, "", __sort_mode);
                                }

                                //this._table_list_filter = __dataRow[0].ItemArray[__column_table_filter].ToString();
                                //__table_view_format_name = __dataRow[0].ItemArray[__column_view_format_table_name].ToString();
                            }
                            catch
                            {
                            }

                            //this._getOrderBy = this._columnRepackOrderBy(__getColumnType, "");
                        }

                        if (checkBoxFirstColumn)
                        {
                            int __column_width = (_gridData._width_by_persent) ? 5 : 40;
                            _gridData._addColumn("check", 11, 0, __column_width, false, false, false, false);
                        }

                        if (this._showIsLockColumn)
                        {
                            _gridData._addColumn(this._isLockRecordShowColumn, 1, 5, 5, false, false, false, false);
                        }

                        for (int row = 0; row < __dataRow.Length; row++)
                        {
                            string __column_name = __dataRow[row].ItemArray[__column_name_ordinal].ToString();
                            string __column_field_name = __dataRow[row].ItemArray[__column_field_name_ordinal].ToString();
                            string __column_field_name_sort = __dataRow[row].ItemArray[__column_field_name_sort_ordinal].ToString();
                            string __column_type = __dataRow[row].ItemArray[__column_type_ordinal].ToString();
                            string __column_format = __dataRow[row].ItemArray[__column_format_ordinal].ToString();
                            string __resourceFieldName = __dataRow[row].ItemArray[__column_resource].ToString();

                            string __column_field_search = "";
                            if (__column_field_search_ordinal != -1)
                            {
                                __column_field_search = __dataRow[row].ItemArray[__column_field_search_ordinal].ToString();
                            }

                            int __column_width = 0;
                            try
                            {
                                __column_width = MyLib._myGlobal._intPhase(__dataRow[row].ItemArray[__column_width_ordinal].ToString());
                            }
                            catch
                            {
                                // Debugger.Break();
                            }
                            // ในกรณีเป็น % ต้องลดลง เพราะมี Check Box
                            if (_gridData._width_by_persent && checkBoxFirstColumn)
                            {
                                __column_width = __column_width * 95 / 100;
                            }
                            int __set_column_type = 1;
                            switch (__column_type)
                            {
                                case "0": __set_column_type = 1; break;
                                case "2": __set_column_type = 2; break;
                                case "6": __set_column_type = 4; break;
                                case "5": __set_column_type = 5; break;
                            }
                            // Moo เพิ่ม __set_column_type == 5 
                            //if (__set_column_type == 2 || __set_column_type == 3)
                            if (__set_column_type == 2 || __set_column_type == 3 || __set_column_type == 5)
                            {
                                __column_format = MyLib._myGlobal._getFormatNumber(__column_format);
                            }
                            string __columnName = __column_field_name_sort.Split(',')[0].ToString();
                            if (this._columnFieldNameReplace != null)
                            {
                                __column_field_name = this._columnFieldNameReplace(__column_field_name);
                            }
                            // toe แก้ไข กรณี column width เป็น 0 ให้ซ่อน column ไว้
                            if (__column_name.Length > 0 && __column_name[0] == '*')
                            {
                                //_gridData._addColumn(__columnName, 11, 0, __column_width, false, false, true, false);
                                _gridData._addColumn(__columnName, 11, 100, __column_width, false, (__column_width == 0) ? true : false, true, true, __column_format, __column_field_name, "", __column_field_search, __resourceFieldName);
                            }
                            else
                            {
                                _gridData._addColumn(__columnName, __set_column_type, 100, __column_width, false, (__column_width == 0) ? true : false, true, true, __column_format, __column_field_name, "", __column_field_search, __resourceFieldName);
                            }
                            int __columnAddr = _gridData._findColumnByName(__columnName);
                            ((_myGrid._columnType)_gridData._columnList[__columnAddr])._orderBy = __column_field_name_sort;
                            //
                        } // for
                        if (_lockRecord)
                        {
                            if (this._table_list_filter.Length > 0 && (this._tableName.Equals(this._tableList) == false))
                            {
                                this._guidCode = this._tableName + "." + this._guidCode;
                            }

                            _gridData._addColumn(this._guidCode, 1, 0, 0, false, true, false, false);
                        }

                        if (this._table_list_filter.Length > 0 && (this._tableName.Equals(this._tableList) == false))
                        {
                            this._isLockRecord = this._tableName + "." + this._isLockRecord;
                        }

                        // field check ห้ามแก้เด็ดขาด
                        _gridData._addColumn(this._isLockRecord, 2, 0, 0, false, true, true, false);

                        //
                        if (_loadViewAddColumn != null)
                        {
                            _loadViewAddColumn(_gridData);
                        }
                    }
                    if (_gridData._width_by_persent)
                    {
                        _gridData._calcPersentWidthToScatter();
                    }
                    __dataResult.Dispose();
                }
                catch (Exception ex)
                {
                    //  --  
                    // Debugger.Break();
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message.ToString(), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void _buttonPageBegin_Click(object sender, EventArgs e)
        {
            _searchPageNumber = 0;
            _loadViewData(_searchPageNumber * _searchRecordPerPage);
        }

        private void _buttonPageEnd_Click(object sender, EventArgs e)
        {
            _searchPageNumber = _searchPageTotal - 1;
            _loadViewData(_searchPageNumber * _searchRecordPerPage);
        }

        private void _buttonSwapScreen_Click(object sender, EventArgs e)
        {

        }

        public event DeleteDataEventHandler _deleteData;
        public event CloseEventHandler _closeScreen;
        public event FlowEventHandler _flowEvent;
        public event LoadViewAddColumn _loadViewAddColumn;
        public event CheckLockRecord _checkLockRecord;
        public event GetDataEventHandler _getDataEvent;
        public event ControlKeyEventHandler _controlKeyEvent;
        public event OwnerQueryHandler _ownerQuery;
        public event ColumnFieldNameReplaceEventHandler _columnFieldNameReplace;
        public event ExtraWhereEventHandler _extraWhereEvent;
        public event DocumentPictureEventHandler _docPictureEvent;
        public event PrintMultiDocumentEventHandler _multiPrintEvent;

        public string _getRefer(int row)
        {
            StringBuilder __result = new StringBuilder();
            __result.Append(" where ");
            for (int __loop = 0; __loop < this._referFieldList.Count; __loop++)
            {
                _myGlobal._referFieldType __getRefer = (_myGlobal._referFieldType)this._referFieldList[__loop];
                string __columnName = this._tableName + "." + __getRefer._fieldName;
                object __getData = (object)_gridData._cellGet(row, __columnName);
                if (__loop != 0)
                {
                    __result.Append(" and ");
                }
                switch (__getRefer._fieldDataType)
                {
                    case 1: // string
                        __getRefer._fieldData = (string)__getData;
                        if (this._autoUpper)
                        {
                            __result.Append(string.Concat(_myGlobal._addUpper(__columnName), "=\'", __getRefer._fieldData.ToString().ToUpper(), "\'"));
                        }
                        else
                        {
                            __result.Append(string.Concat(__columnName, "=\'", __getRefer._fieldData.ToString().ToUpper(), "\'"));
                        }
                        _referActive = true;
                        break;
                    case 2: // date
                        __getRefer._fieldData = (DateTime)__getData;
                        __result.Append(string.Concat(__columnName, "=\'", MyLib._myGlobal._convertDateToQuery((DateTime)__getRefer._fieldData), "\'"));
                        _referActive = true;
                        break;
                    case 3: // double
                        __getRefer._fieldData = (decimal)__getData;
                        __result.Append(string.Concat(__columnName, "=", (decimal)__getRefer._fieldData));
                        _referActive = true;
                        break;
                    case 4: // int
                        __getRefer._fieldData = (int)__getData;
                        __result.Append(string.Concat(__columnName, "=", (int)__getRefer._fieldData));
                        _referActive = true;
                        break;
                }
            }
            _whereString = __result.ToString();
            return (__result.ToString());
        }

        public void _deleteDataBegin()
        {
            if (_editMode)
            {
                // อยู่ในการแก้ไขหรือเพิ่ม
                MessageBox.Show(MyLib._myGlobal._resource("warning85"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ArrayList __selectListRefer = new ArrayList();
                bool pass = true;
                for (int __row = 0; __row < _gridData._rowData.Count && pass == true; __row++)
                {
                    if ((int)_gridData._cellGet(__row, "check") == 1)
                    {
                        _deleteDataType __setData = new _deleteDataType();
                        __setData.row = __row;
                        __setData.whereString = _getRefer(__row);
                        if (_checkLockRecord != null)
                        {
                            pass = _checkLockRecord(__row, _gridData, __setData.whereString);
                            if (pass == false)
                            {
                                break;
                            }
                        }
                        __selectListRefer.Add(__setData);
                    }
                }
                if (pass)
                {
                    if (__selectListRefer.Count == 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("warning86"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //  --  
                        string __message = String.Format(MyLib._myGlobal._resource("warning87"), __selectListRefer.Count.ToString());
                        DialogResult __result = MessageBox.Show(__message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        if (__result == DialogResult.Yes)
                        {
                            if (_deleteData != null)
                            {
                                _deleteData(__selectListRefer);
                            }
                        }
                    }
                }
            }
        }

        private void _buttonDelete_Click(object sender, EventArgs e)
        {
            //กำหนดสิทธิ์ somruk  ลบได้หรือไมได้
            //somruk
            _PermissionsType __premissons = MyLib._myGlobal._isAccessMenuPermision(this._mainMenuId, this._mainMenuCode);
            if (__premissons._isDelete == true || this._isnonPermission)
            {
                _deleteDataBegin();
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("delete1"));

            }
        }

        private void _buttonNew_Click(object sender, EventArgs e)
        {
        }

        private void _buttonNewFromTemp_Click(object sender, EventArgs e)
        {
        }

        private void _buttonSelectAll_Click(object sender, EventArgs e)
        {
            DialogResult __result = MessageBox.Show(MyLib._myGlobal._resource("warning91"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (__result == DialogResult.Yes)
            {
                for (int __row = 0; __row < _gridData._rowData.Count; __row++)
                {
                    _gridData._cellUpdate(__row, "check", 1, false);
                } // for
                _gridData.Invalidate();
            }
        }

        private void _buttonClose_Click_1(object sender, EventArgs e)
        {
            if (_closeScreen != null)
            {
                _closeScreen();
            }
        }

        private void _buttonDesktop_Click(object sender, EventArgs e)
        {

        }

        private void _buttonNext_Click_1(object sender, EventArgs e)
        {

        }

        private void _buttonSelectDisplayMode_Click(object sender, EventArgs e)
        {
        }

        private void _button_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.Focus();
        }

        private void _timerLoadData_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false && this._connectDatabaseName.Length > 0)
            {
                if (this._loadViewDataSuccess == false)
                {
                    this._loadViewData(0);
                }
            }
            this._timerLoadData.Stop();
            this._timerLoadData.Dispose();
        }

        private void _flowButton_Click(object sender, EventArgs e)
        {
            if (this._flowEvent != null)
            {
                this._flowEvent(this._gridData);
            }
        }



        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._selectedGrid._clear();
        }

        private void _filterButton_Click(object sender, EventArgs e)
        {
            this._filterScreen();
        }

        private void _docPictureButton_Click(object sender, EventArgs e)
        {
            if (this._docPictureEvent != null)
            {
                this._docPictureEvent(this._gridData);
            }
        }

        private void _printRangeButton_Click(object sender, EventArgs e)
        {
            if (_multiPrintEvent != null)
            {
                ArrayList __selectedRow = new ArrayList();
                bool pass = false;

                for (int __row = 0; __row < _gridData._rowData.Count; __row++)
                {
                    if ((int)_gridData._cellGet(__row, "check") == 1)
                    {
                        pass = true;
                        __selectedRow.Add(__row);
                    }
                }

                if (pass)
                {
                    _multiPrintEvent(__selectedRow);
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("กรุณาเลือกข้อมูล"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
    }
    public delegate void DeleteDataEventHandler(ArrayList selectRowOrder);
    public delegate void CloseEventHandler();
    public delegate void FlowEventHandler(MyLib._myGrid grid);
    public delegate void LoadViewAddColumn(MyLib._myGrid myGrid);
    public delegate bool CheckLockRecord(int row, MyLib._myGrid sender, string whereString);
    public delegate void GetDataEventHandler(int row);
    public delegate void ControlKeyEventHandler(_myGrid grid, Keys key);
    public delegate MyLib._queryReturn OwnerQueryHandler(string where, string orderBy, int recordTop, int searchRecordPerPage);
    public delegate string ColumnFieldNameReplaceEventHandler(string source);
    public delegate string ExtraWhereEventHandler();
    public delegate void DocumentPictureEventHandler(MyLib._myGrid myGrid);
    public delegate void PrintMultiDocumentEventHandler(ArrayList selectedRow);
    public class _deleteDataType
    {
        public int row;
        public string whereString;
    }
}
