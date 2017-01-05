using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._design
{
    public partial class _queryDesigner : UserControl
    {
        string _resourceTableName = "Table Name";
        string _resourceTableInternalName = "SML Name";
        //
        string _resourceFieldTable = "Table";
        string _resourceFieldColumn = "Column";
        string _resourceFieldAlias = "Alias";
        string _resourceFieldOutput = "Output";
        string _resourceFieldSortType = "Sort Type";
        string _resourceFieldSortOrder = "Sort Order";
        string _resourceFieldFilter = "Filter";
        //
        ArrayList _getTableFromXml;
        //
        string[] _sortType = { "Unsorted", "Ascending", "Descending" };
        Boolean _created = false;
        //
        public _queryDesigner()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this.Load += new EventHandler(_designView_Load);
        }

        void _designView_Load(object sender, EventArgs e)
        {
            try
            {
                if (this._created == false)
                {
                    this._created = true;
                    //
                    this._fieldGrid._table_name = "";
                    this._fieldGrid._addColumn(_resourceFieldTable, 1, 255, 20, true, false, false, true);
                    this._fieldGrid._addColumn(_resourceFieldColumn, 1, 25, 30, true, false, false, true);
                    this._fieldGrid._addColumn(_resourceFieldAlias, 1, 255, 15, true, false, false, false);
                    this._fieldGrid._addColumn(_resourceFieldOutput, 11, 255, 5, true, false, false, false);
                    this._fieldGrid._addColumn(_resourceFieldSortType, 10, 255, 10, false, false, false, false);
                    this._fieldGrid._addColumn(_resourceFieldSortOrder, 1, 255, 10, true, false, false, false);
                    this._fieldGrid._addColumn(_resourceFieldFilter, 1, 255, 10, true, false, false, false);
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    _getTableFromXml = __myFrameWork._getAllTable(1, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseStructFileName);
                    //
                    this._tableGrid._table_name = "";
                    this._tableGrid.AllowDrop = true;
                    this._tableGrid._isEdit = false;
                    this._tableGrid._gridType = 1;
                    this._tableGrid._addColumn(_resourceTableName, 1, 255, 50, false, false, false, false);
                    this._tableGrid._addColumn(_resourceTableInternalName, 1, 255, 50, false, false, false, false);
                    //
                    ArrayList __getTableFromDatabase = __myFrameWork._getTableFromDatabase(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName);
                    this._tableGrid._clear();
                    for (int __loop = 0; __loop < __getTableFromDatabase.Count; __loop++)
                    {
                        string getTableName = __getTableFromDatabase[__loop].ToString();
                        string getCompareName = _compareTableName(getTableName);
                        int __row = this._tableGrid._addRow();
                        this._tableGrid._cellUpdate(__row, _resourceTableName, getTableName, false);
                        this._tableGrid._cellUpdate(__row, _resourceTableInternalName, getCompareName, false);
                    }
                    //
                    _myLinkList._alterCellUpdate += new AfterCellUpdateEventHandler(_linkList__alterCellUpdate);
                    _myLinkList._afterUpdate += new AfterUpdateEventHandler(_linkList__afterUpdate);
                    _fieldGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_fieldGrid__cellComboBoxItem);
                    _fieldGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_fieldGrid__cellComboBoxGet);
                    _fieldGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_fieldGrid__alterCellUpdate);
                }
            }
            catch
            {
            }
        }

        void _linkList__afterUpdate()
        {
            _createQuery();
        }

        void _fieldGrid__alterCellUpdate(object sender, int row, int column)
        {
            _createQuery();
        }

        string _fieldGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            string __result = "";
            if (columnName.Equals(_resourceFieldSortType))
            {
                if (select != 0)
                {
                    __result = _sortType[select];
                }
            }
            return (__result);
        }

        /// <summary>
        /// Combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        object[] _fieldGrid__cellComboBoxItem(object sender, int row, int column)
        {
            ArrayList __result = new ArrayList();
            if (column == _fieldGrid._findColumnByName(_resourceFieldSortType))
            {
                __result.Add(_sortType[0]);
                __result.Add(_sortType[1]);
                __result.Add(_sortType[2]);
            }
            return (object[])__result.ToArray();
        }

        /// <summary>
        /// เมื่อมีการแก้ไข ข้อมูลใน Grid ให้ประมวลผลเอามาไว้ใน List 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="_grid"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        void _linkList__alterCellUpdate(object sender, MyLib._myGrid _grid, int row, int column)
        {
            _processColumn((_linkList)sender, _grid, row, column);
        }

        void _processColumn(_linkList _getLink, MyLib._myGrid _grid, int row, int column)
        {
            string __getDatabaseName = _grid._database_name;
            string __getTableName = _grid._table_name;
            string __getFieldName = _grid._cellGet(row, 1).ToString().Split(' ')[0].ToString();
            bool __getCheckBox = (((int)_grid._cellGet(row, 0)) == 0) ? false : true;

            bool __found = false;
            int __foundRowNumber = 0;
            for (int __loop = 0; __loop < _fieldGrid._rowData.Count; __loop++)
            {
                if (_fieldGrid._cellGet(__loop, _resourceFieldTable).ToString().Equals(__getTableName) &&
                    _fieldGrid._cellGet(__loop, _resourceFieldColumn).ToString().Equals(__getFieldName))
                {
                    __foundRowNumber = __loop;
                    __found = true;
                    __loop--;
                    break;
                }
            }
            if (__found)
            {
                if (__getCheckBox == false)
                {
                    _fieldGrid._rowData.RemoveAt(__foundRowNumber);
                }
            }
            else
            {
                if (__getCheckBox)
                {
                    // add new row
                    int __newRow = _fieldGrid._addRow();
                    _fieldGrid._cellUpdate(__newRow, _resourceFieldTable, __getTableName, false);
                    _fieldGrid._cellUpdate(__newRow, _resourceFieldColumn, __getFieldName, false);
                    _fieldGrid._cellUpdate(__newRow, _resourceFieldOutput, 1, false);
                    _fieldGrid._cellUpdate(__newRow, _resourceFieldAlias, "\"" + __getTableName.ToUpper() + "." + __getFieldName.ToUpper() + "\"", false);
                }
            }
            _fieldGrid._rowFirst = (_fieldGrid._rowData.Count - _fieldGrid._rowPerPage) + 1;
            if (_fieldGrid._rowFirst < 0)
            {
                _fieldGrid._rowFirst = 0;
            }
            _fieldGrid._selectRow = 0;
            _createAlias();
            _fieldGrid.Invalidate();
        }

        void _createAlias()
        {
            for (int __loop = 0; __loop < _fieldGrid._rowData.Count; __loop++)
            {
                string __parentNameAlias = _fieldGrid._cellGet(__loop, _resourceFieldAlias).ToString();
                string __parentName = (__parentNameAlias.Length == 0) ? _fieldGrid._cellGet(__loop, _resourceFieldColumn).ToString() : __parentNameAlias;
                int __runningNumber = 1;
                for (int __run = __loop + 1; __run < _fieldGrid._rowData.Count; __run++)
                {
                    string __runName = _fieldGrid._cellGet(__run, _resourceFieldColumn).ToString();
                    if (__runName.Equals(__parentName))
                    {
                        string __getOldString = _fieldGrid._cellGet(__run, _resourceFieldAlias).ToString();
                        if (__getOldString.Length == 0 || __getOldString[0] == '*')
                        {
                            _fieldGrid._cellUpdate(__run, _resourceFieldAlias, "*" + __runName + __runningNumber.ToString(), false);
                            __runningNumber++;
                        }
                    }
                }
            }
        }

        string _compareTableName(string databaseTableName)
        {
            for (int __loop = 0; __loop < _getTableFromXml.Count; __loop++)
            {
                string[] getTable = _getTableFromXml[__loop].ToString().Split(',');
                if (getTable[0].CompareTo(databaseTableName) == 0)
                {
                    return (getTable[MyLib._myGlobal._languageNumber + 1]);
                }
            }
            return ("");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
        }

        private void _myFlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _toolBarAreaTop_Paint(object sender, PaintEventArgs e)
        {

        }

        class _createQueryTableType
        {
            public string _tableName;
            public StringBuilder _query;
        }

        private void _createQuery()
        {
            // Create Field for select
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append("SELECT ");
            string __comma = "";
            for (int __loop = 0; __loop < _fieldGrid._rowData.Count; __loop++)
            {
                if ((int)_fieldGrid._cellGet(__loop, _resourceFieldOutput) == 1)
                {
                    __myQuery.Append(__comma);
                    string __getTableName = _fieldGrid._cellGet(__loop, _resourceFieldTable).ToString();
                    if (__getTableName.Length > 0)
                    {
                        __myQuery.Append(__getTableName).Append(".");
                    }
                    __myQuery.Append(_fieldGrid._cellGet(__loop, _resourceFieldColumn).ToString());
                    string _aliasName = _fieldGrid._cellGet(__loop, _resourceFieldAlias).ToString();
                    if (_aliasName.Length != 0)
                    {
                        if (_aliasName[0] == '*')
                        {
                            _aliasName = _aliasName.Remove(0, 1);
                        }
                        __myQuery.Append(" AS ").Append(_aliasName);
                    }
                    __comma = ", ";
                }
            }
            __myQuery.Append("\r\n FROM ");
            // สร้างการ Join
            ArrayList __tableList = new ArrayList();
            for (int __loop = 0; __loop < this._myLinkList._panel.Controls.Count; __loop++)
            {
                if (this._myLinkList._panel.Controls[__loop].GetType() == typeof(_tablePanel))
                {
                    // ดึงรายชื่อ Table
                    _tablePanel __getControl = (_tablePanel)this._myLinkList._panel.Controls[__loop];
                    _createQueryTableType __newTable = new _createQueryTableType();
                    __newTable._tableName = __getControl._tableName;
                    __newTable._query = new StringBuilder();
                    __newTable._query.Append(" " + __getControl._tableName);
                    __tableList.Add(__newTable);
                }
            }
            for (int __loop = 0; __loop < __tableList.Count; __loop++)
            {
                _createQueryTableType __getTable = (_createQueryTableType)__tableList[__loop];
                bool __firstJoin = false;
                string __addStr = "";
                string __addAnd = "";
                for (int __link = 0; __link < this._myLinkList._linkLine.Count; __link++)
                {
                    _linkPanel __getLink = (_linkPanel)this._myLinkList._linkLine[__link];
                    if (__firstJoin == false)
                    {
                        __getTable._query.Insert(0, " " + __getLink._joinComboBox.SelectedItem.ToString() + " JOIN \r\n");
                        __firstJoin = true;
                        __addStr = " ON ";
                    }
                    __getTable._query.Append(__addStr).Append(__addAnd).Append(__getLink._listBox.SelectedItem.ToString());
                    __addAnd = " AND ";
                    __addStr = "";
                }
                __tableList[__loop] = (_createQueryTableType)__getTable;
            }
            //
            /*for (int __link = 0; __link < this._myLinkList._linkLine.Count; __link++)
            {
                _linkPanel __getLink = (_linkPanel)this._myLinkList._linkLine[__link];
                int __tableListNumber = -1;
                for (int __findTable = 0; __findTable < _tableList.Count; __findTable++)
                {
                    _createQueryTableType __getTable = (_createQueryTableType)_tableList[__findTable];
                    if (__getLink._targetDatabaseName.Equals(__getTable._databaseName) && __getLink._targetTableName.Equals(__getTable._tableName))
                    {
                        __tableListNumber = __findTable;
                        break;
                    }
                }
                if (__tableListNumber != -1)
                {
                    _createQueryTableType __getTargetTable = (_createQueryTableType)_tableList[__tableListNumber];
                    if (__getTargetTable._query.Length == 0)
                    {
                        if (__tableListNumber == 0)
                        {
                            __getTargetTable._query.Append(__getLink._joinComboBox.SelectedItem.ToString()).Append(" JOIN  ").Append(__getLink._targetTableName).Append(" ");
                        }
                    }
                    _tableList[__tableListNumber] = (_createQueryTableType)__getTargetTable;
                }
            }*/
            // ประกอบ
            for (int __findTable = 0; __findTable < __tableList.Count; __findTable++)
            {
                _createQueryTableType __getTable = (_createQueryTableType)__tableList[__findTable];
                __myQuery.Append(__getTable._query.ToString());
            }
            // Order By
            ArrayList __sortOrder = new ArrayList();
            for (int __loop = 0; __loop < _fieldGrid._rowData.Count; __loop++)
            {
                if ((int)_fieldGrid._cellGet(__loop, _resourceFieldSortType) != 0)
                {
                    _orderBy __order = new _orderBy();
                    string __getFieldName = _fieldGrid._cellGet(__loop, _resourceFieldAlias).ToString();
                    if (__getFieldName.Length == 0)
                    {
                        __getFieldName = _fieldGrid._cellGet(__loop, _resourceFieldColumn).ToString();
                    }
                    else
                    {
                        if (__getFieldName[0] == '*')
                        {
                            __getFieldName = __getFieldName.Remove(0, 1);
                        }
                    }
                    __order._tableName = _fieldGrid._cellGet(__loop, _resourceFieldTable).ToString();
                    __order._fieldOrAliasName = __getFieldName;
                    __order._orderType = (int)_fieldGrid._cellGet(__loop, _resourceFieldSortType);
                    __order._filter = _fieldGrid._cellGet(__loop, _resourceFieldFilter).ToString();
                    try
                    {
                        __order._orderNumber = MyLib._myGlobal._intPhase(_fieldGrid._cellGet(__loop, _resourceFieldSortOrder).ToString());
                    }
                    catch
                    {
                        __order._orderNumber = 0;
                    }
                    __sortOrder.Add(__order);
                }
            }
            //
            __sortOrder.Sort(new _orderByComparer());
            for (int __loop = 0; __loop < __sortOrder.Count; __loop++)
            {
                if (__loop == 0)
                {
                    __myQuery.Append("\r\n ORDER BY ");
                }
                _orderBy __getData = (_orderBy)__sortOrder[__loop];
                if (__loop != 0)
                {
                    __myQuery.Append(", ");
                }
                if (__getData._tableName.Length > 0)
                {
                    __myQuery.Append(__getData._tableName).Append(".");
                }
                __myQuery.Append(__getData._fieldOrAliasName);
                if (__getData._orderType == 2)
                {
                    __myQuery.Append(" DESC ");
                }
            }
            // where
            bool __whereFirst = false;
            string __whereAnd = "";
            for (int __loop = 0; __loop < _fieldGrid._rowData.Count; __loop++)
            {
                string __getFilter = _fieldGrid._cellGet(__loop, _resourceFieldFilter).ToString();
                if (__getFilter.Length > 0)
                {
                    if (__whereFirst == false)
                    {
                        __myQuery.Append("\r\n WHERE ");
                        __whereFirst = true;
                    }
                    string __getFieldName = _fieldGrid._cellGet(__loop, _resourceFieldAlias).ToString();
                    if (__getFieldName.Length == 0)
                    {
                        __getFieldName = _fieldGrid._cellGet(__loop, _resourceFieldColumn).ToString();
                    }
                    else
                    {
                        if (__getFieldName[0] == '*')
                        {
                            __getFieldName = __getFieldName.Remove(0, 1);
                        }
                    }
                    //string _databaseName = _fieldGrid._cellGet(__loop, _resourceFieldDatabase).ToString();
                    string _tableName = _fieldGrid._cellGet(__loop, _resourceFieldTable).ToString();
                    __myQuery.Append(__whereAnd).Append("(");
                    if (_tableName.Length > 0)
                    {
                        __myQuery.Append(_tableName).Append(".");
                    }
                    __myQuery.Append(__getFieldName).Append(__getFilter).Append(")");
                    __whereAnd = " AND ";
                }
            }
            _queryTextBox.Text = __myQuery.ToString();
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._fieldGrid._clear();
        }

        private void _executeButton_Click(object sender, EventArgs e)
        {
            SMLReport._formReport._queryExecuteForm __result = new _formReport._queryExecuteForm();
            __result.__query = this._queryTextBox.Text;
            __result.ShowDialog();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    class _orderByComparer : IComparer
    {
        public _orderByComparer() : base() { }
        int IComparer.Compare(object x, object y)
        {
            _orderBy _orderX = (_orderBy)x;
            _orderBy _orderY = (_orderBy)y;
            if (_orderX == null && _orderY == null)
            {
                return 0;
            }
            if (_orderX == null && _orderY != null)
            {
                return (-1);
            }
            if (_orderX != null && _orderY == null)
            {
                return (1);
            }
            return (_orderX._orderNumber.CompareTo(_orderY._orderNumber));
        }
    }

    class _orderBy
    {
        public string _tableName;
        public string _fieldOrAliasName;
        public int _orderType;
        public int _orderNumber;
        public string _filter;
    }
}
