using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace _g
{
    public partial class _viewManageCustom : UserControl
    {
        private MyLib._searchDataFull _searchViewTable = new MyLib._searchDataFull();
        MyLib._myForm _searchWindows;
        MyLib._myGrid _fieldList = new MyLib._myGrid();
        Size _windowsSearchOldSize = new Size(600, MyLib._myGlobal._mainSize.Height - 20);
        Point _windowsDesktopLocation;
        string _oldTableName = "";

        string _field_full_name = "field_full_name";
        string _field_name = "field_name";
        string _field_type = "field_type";
        string _field_length = "field_length";


        string _searchField = "";

        public _viewManageCustom()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._myGridTooblar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_erp_view_table_custom", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.erp_view_table_custom._screen_code, 1);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._manageBackgroundPanel = this._myPanel;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._screen_view_manage_custom._saveKeyDown += new MyLib.SaveKeyDownHandler(_posMachineScreen1__saveKeyDown);
            this._screen_view_manage_custom._checkKeyDown += new MyLib.CheckKeyDownHandler(_posMachineScreen1__checkKeyDown);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 500;


            _viewColumnGrid._table_name = _g.d.erp_view_column_custom._table;
            _viewColumnGrid._width_by_persent = true;
            _viewColumnGrid._autoUpperSearchString = false;
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_field_name, 1, 100, 15, true, false, true, true);
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_name, 1, 100, 15, true);
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_name_2, 1, 100, 10, true);
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_field_sort, 1, 10, 10, true, false, true, true);
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_field_search, 1, 10, 10, true, false, true, false);
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_width, 2, 10, 10, true, false, true, false, "#,0");
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_align, 10, 10, 10, true);
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_type, 10, 10, 10, true);
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_format, 1, 10, 10, true);
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_resource, 1, 10, 10, true);
            _viewColumnGrid._addColumn(_g.d.erp_view_column_custom._column_number, 2, 10, 8, true, true);

            _viewColumnGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_gridTemp__cellComboBoxItem);
            _viewColumnGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_gridTemp__cellComboBoxGet);
            _viewColumnGrid._clickSearchButton += new MyLib.SearchEventHandler(_gridTemp__clickSearchButton);
            _viewColumnGrid._width_by_persent = true;
            _viewColumnGrid._calcPersentWidthToScatter();
            //this._searchViewTable.StartPosition = FormStartPosition.CenterScreen;
            this._searchViewTable.WindowState = FormWindowState.Maximized;
            this._searchViewTable.Text = MyLib._myGlobal._resource("ค้นหาคลังสินค้า");
            this._searchViewTable._dataList._loadViewFormat("screen_search_erp_view_table", MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchViewTable._dataList._gridData._mouseClick += (s1, e1) =>
            {
                string __code = this._searchViewTable._dataList._gridData._cellGet(this._searchViewTable._dataList._gridData._selectRow, _g.d.erp_view_table._table + "." + _g.d.erp_view_table._screen_code).ToString();
                this._searchViewTable.Close();
                this._screen_view_manage_custom._setDataStr(_g.d.erp_view_table._screen_code, __code);
                this._getScreenTableAndColumn(__code);
            };
            this._searchViewTable._searchEnterKeyPress += (s1, e1) =>
            {
                string __code = this._searchViewTable._dataList._gridData._cellGet(this._searchViewTable._dataList._gridData._selectRow, _g.d.erp_view_table._table + "." + _g.d.erp_view_table._screen_code).ToString();
                this._searchViewTable.Close();
                this._screen_view_manage_custom._setDataStr(_g.d.erp_view_table._screen_code, __code);
                this._getScreenTableAndColumn(__code);
            };

            //_viewColumnGrid.Dock = DockStyle.Fill;
            //_viewColumnGrid._cellComboBoxItem += new CellComboBoxItemEventHandler(_gridTemp__cellComboBoxItem);
            //_viewColumnGrid._cellComboBoxGet += new CellComboBoxItemGetDisplay(_gridTemp__cellComboBoxGet);
            //_viewColumnGrid._clickSearchButton += new SearchEventHandler(_gridTemp__clickSearchButton);
            //_viewColumnGrid._isEdit = editMode;

            // field list 
            _fieldList._table_name = "erp_other";
            _fieldList._addColumn(_field_name, 1, 10, 35);
            _fieldList._addColumn(_field_full_name, 1, 10, 40);
            _fieldList._addColumn(_field_type, 1, 10, 15);
            _fieldList._addColumn(_field_length, 2, 10, 7);
            _fieldList._mouseClick += new MyLib.MouseClickHandler(fieldList__mouseClick);

            this._screen_view_manage_custom._enabedControl(_g.d.erp_view_table_custom._name_1, false);
            this._screen_view_manage_custom._enabedControl(_g.d.erp_view_table_custom._name_2, false);
            this._screen_view_manage_custom._enabedControl(_g.d.erp_view_table_custom._table_name, false);
            //this._screen_view_manage_custom._enabedControl(_g.d.erp_view_table_custom._table_list, false);
            //this._screen_view_manage_custom._enabedControl(_g.d.erp_view_table_custom._sort, false);
            //this._screen_view_manage_custom._enabedControl(_g.d.erp_view_table_custom._filter, false);
            this._screen_view_manage_custom._enabedControl(_g.d.erp_view_table_custom._width_persent, false);

            this._screen_view_manage_custom._textBoxSearch += new MyLib.TextBoxSearchHandler(_screen_view_manage_custom__textBoxSearch);
            this.Resize += new EventHandler(_side_Resize);
        }

        void fieldList__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myGrid __gridTemp = (MyLib._myGrid)_viewColumnGrid;
            if (e._row < _fieldList._rowData.Count)
            {
                if (_searchField.ToLower().CompareTo(_g.d.erp_view_column_custom._column_field_name) == 0)
                {
                    __gridTemp._cellUpdate(__gridTemp._selectRow, _g.d.erp_view_column_custom._column_field_name, _fieldList._cellGet(e._row, "field_name"), true);
                    __gridTemp._cellUpdate(__gridTemp._selectRow, _g.d.erp_view_column_custom._column_name, _fieldList._cellGet(e._row, "field_full_name"), true);
                    __gridTemp._cellUpdate(__gridTemp._selectRow, _g.d.erp_view_column_custom._column_field_sort, _fieldList._cellGet(e._row, "field_name"), true);
                    int __fieldLength = (int)_fieldList._cellGet(e._row, "field_length") * 10;
                    if (__fieldLength > 100) __fieldLength = 100;
                    __gridTemp._cellUpdate(__gridTemp._selectRow, _g.d.erp_view_column_custom._column_width, __fieldLength, true);
                    __gridTemp._cellUpdate(__gridTemp._selectRow, _g.d.erp_view_column_custom._column_type, MyLib._myGlobal._databaseColumnFind(_fieldList._cellGet(e._row, "field_type").ToString()), true);
                    __gridTemp.Invalidate();
                }
                else if (_searchField.ToLower().CompareTo(_g.d.erp_view_column_custom._column_field_sort) == 0)
                {
                    __gridTemp._cellUpdate(__gridTemp._selectRow, _g.d.erp_view_column_custom._column_field_sort, _fieldList._cellGet(e._row, "field_name"), true);
                    __gridTemp.Invalidate();
                }
                else if (_searchField.ToLower().CompareTo(_g.d.erp_view_table_custom._sort) == 0)
                {
                    this._screen_view_manage_custom._setDataStr(_g.d.erp_view_table_custom._sort, _fieldList._cellGet(e._row, "field_name").ToString());
                }
                
                _searchWindows.Controls.Remove(_fieldList);
                _searchWindows.Close();
            }
        }

        void _closeButton_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }

        void _saveButton_Click(object sender, System.EventArgs e)
        {
            _save_data();
        }

        void _getScreenTableAndColumn(string screenCode)
        {

            _viewColumnGrid._clear();

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.erp_view_table._table + " where " + _g.d.erp_view_table._screen_code + "=\'" + screenCode + "\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.erp_view_column_custom._table + " where " + _g.d.erp_view_column_custom._screen_code + "=\'" + screenCode + "\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.erp_view_column._table + " where " + _g.d.erp_view_column._screen_code + "=\'" + screenCode + "\'"));
            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            DataTable __mainTable = ((DataSet)__result[0]).Tables[0];
            DataTable __viewCustomColumn = ((DataSet)__result[1]).Tables[0];
            DataTable __viewColumn = ((DataSet)__result[2]).Tables[0];

            if (__mainTable.Rows.Count > 0)
            {
                this._screen_view_manage_custom._setDataStr(_g.d.erp_view_table_custom._name_1, __mainTable.Rows[0][_g.d.erp_view_table_custom._name_1].ToString());
                this._screen_view_manage_custom._setDataStr(_g.d.erp_view_table_custom._name_2, __mainTable.Rows[0][_g.d.erp_view_table_custom._name_2].ToString());
                this._screen_view_manage_custom._setDataStr(_g.d.erp_view_table_custom._table_name, __mainTable.Rows[0][_g.d.erp_view_table_custom._table_name].ToString());
                this._screen_view_manage_custom._setDataStr(_g.d.erp_view_table_custom._table_list, __mainTable.Rows[0][_g.d.erp_view_table_custom._table_list].ToString());
                this._screen_view_manage_custom._setDataStr(_g.d.erp_view_table_custom._sort, __mainTable.Rows[0][_g.d.erp_view_table_custom._sort].ToString());
                this._screen_view_manage_custom._setCheckBox(_g.d.erp_view_table_custom._width_persent, __mainTable.Rows[0][_g.d.erp_view_table_custom._width_persent].ToString());
            }

            if (__viewCustomColumn.Rows.Count > 0)
            {
                for (int __row = 0; __row < __viewCustomColumn.Rows.Count; __row++)
                {
                    int __newRow = this._viewColumnGrid._addRow();
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_field_name, __viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_field_name].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_name, __viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_name].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_name_2, __viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_name_2].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_field_sort, __viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_field_sort].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_width, (int)MyLib._myGlobal._decimalPhase(__viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_width].ToString()), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_align, (int)MyLib._myGlobal._decimalPhase(__viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_align].ToString()), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_type, (int)MyLib._myGlobal._decimalPhase(__viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_type].ToString()), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_format, __viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_format].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_number, (int)MyLib._myGlobal._decimalPhase(__viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_number].ToString()), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_resource, __viewCustomColumn.Rows[__row][_g.d.erp_view_column_custom._column_resource].ToString(), false);
                }

            }
            else
            {
                for (int __row = 0; __row < __viewColumn.Rows.Count; __row++)
                {
                    int __newRow = this._viewColumnGrid._addRow();
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_field_name, __viewColumn.Rows[__row][_g.d.erp_view_column._column_field_name].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_name, __viewColumn.Rows[__row][_g.d.erp_view_column._column_name].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_name_2, __viewColumn.Rows[__row][_g.d.erp_view_column._column_name_2].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_field_sort, __viewColumn.Rows[__row][_g.d.erp_view_column._column_field_sort].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_width, (int)MyLib._myGlobal._decimalPhase(__viewColumn.Rows[__row][_g.d.erp_view_column._column_width].ToString()), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_align, (int)MyLib._myGlobal._decimalPhase(__viewColumn.Rows[__row][_g.d.erp_view_column._column_align].ToString()), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_type, (int)MyLib._myGlobal._decimalPhase(__viewColumn.Rows[__row][_g.d.erp_view_column._column_type].ToString()), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_format, __viewColumn.Rows[__row][_g.d.erp_view_column._column_format].ToString(), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_number, (int)MyLib._myGlobal._decimalPhase(__viewColumn.Rows[__row][_g.d.erp_view_column._column_number].ToString()), false);
                    this._viewColumnGrid._cellUpdate(__newRow, _g.d.erp_view_column_custom._column_resource, __viewColumn.Rows[__row][_g.d.erp_view_column._column_resource].ToString(), false);
                }
            }

        }

        void _screen_view_manage_custom__textBoxSearch(object sender)
        {
            // start search erp_view_table
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            string __searchName = __getControl._name.ToLower();

            if (__searchName.Equals(_g.d.erp_view_table_custom._screen_code))
            {
                this._searchViewTable.ShowDialog();
            }
            else if (__searchName.Equals(_g.d.erp_view_table_custom._sort))
            {
                _searchField = __searchName;
                _showSearchColumnGrid();
            }

        }

        void _save_data()
        {
            this._viewColumnGrid._updateRowIsChangeAll(true);

            string __screen_code = this._screen_view_manage_custom._getDataStr(_g.d.erp_view_table_custom._screen_code);
            ArrayList __query_table = this._screen_view_manage_custom._createQueryForDatabase();

            string __fieldList = MyLib._myGlobal._fieldAndComma(_g.d.erp_view_column_custom._screen_code, _g.d.erp_view_column_custom._screen_group, _g.d.erp_view_column_custom._column_number, _g.d.erp_view_column_custom._column_name, _g.d.erp_view_column_custom._column_name_2, _g.d.erp_view_column_custom._column_field_name, _g.d.erp_view_column_custom._column_field_sort, _g.d.erp_view_column_custom._column_width, _g.d.erp_view_column_custom._column_type, _g.d.erp_view_column_custom._column_align, _g.d.erp_view_column_custom._column_format, _g.d.erp_view_column_custom._column_field_search);
            string __valueList = "\'" + __screen_code + "\'" + ",1,";

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_view_table_custom._table + " where " + _g.d.erp_view_table_custom._screen_code + "=\'" + __screen_code + "\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_view_table_custom._table + "(" + __query_table[0] + " ) VALUES (" + __query_table[1] + ")"));
            // __query_table[0].ToString()));

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_view_column_custom._table + " where " + _g.d.erp_view_column_custom._screen_code + "=\'" + __screen_code + "\'"));

            //__query.Append(this._viewColumnGrid._createQueryForInsert(_g.d.erp_view_column_custom._table, __fieldList, __valueList, false));
            // for โลด
            int __rowNumber = 0;
            for (int __row = 0; __row < this._viewColumnGrid._rowData.Count; __row++)
            {
                int __column_number = __row + 1;
                string __column_name = MyLib._myGlobal._convertStrToQuery(this._viewColumnGrid._cellGet(__row, _g.d.erp_view_column_custom._column_name, false).ToString());
                string __column_name_2 = MyLib._myGlobal._convertStrToQuery(this._viewColumnGrid._cellGet(__row, _g.d.erp_view_column_custom._column_name_2, false).ToString());
                string __column_field_name = MyLib._myGlobal._convertStrToQuery(this._viewColumnGrid._cellGet(__row, _g.d.erp_view_column_custom._column_field_name, false).ToString());
                string __column_field_sort = MyLib._myGlobal._convertStrToQuery(this._viewColumnGrid._cellGet(__row, _g.d.erp_view_column_custom._column_field_sort, false).ToString());
                int __column_Width = (int)MyLib._myGlobal._decimalPhase(this._viewColumnGrid._cellGet(__row, _g.d.erp_view_column_custom._column_width).ToString());
                int __column_type = (int)MyLib._myGlobal._decimalPhase(this._viewColumnGrid._cellGet(__row, _g.d.erp_view_column_custom._column_type).ToString());
                int __column_align = (int)MyLib._myGlobal._decimalPhase(this._viewColumnGrid._cellGet(__row, _g.d.erp_view_column_custom._column_align).ToString());
                string __column_format = MyLib._myGlobal._convertStrToQuery(this._viewColumnGrid._cellGet(__row, _g.d.erp_view_column_custom._column_format, false).ToString());
                string __column_field_search = MyLib._myGlobal._convertStrToQuery(this._viewColumnGrid._cellGet(__row, _g.d.erp_view_column_custom._column_field_search, false).ToString());
                if (__column_field_name.Length > 0)
                {
                    __rowNumber++;
                    string __columnQuery = "insert into " + _g.d.erp_view_column_custom._table + "(" + __fieldList + ") values(\'" + __screen_code + "\',1," + __rowNumber + ",\'" + __column_name + "\',\'" + __column_name_2 + "\',\'" + __column_field_name + "\',\'" + __column_field_sort + "\'," + __column_Width + "," + __column_type + "," + __column_align + ",\'" + __column_format + "\',\'" + __column_field_search + "\')";
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__columnQuery));
                }
            }

            __query.Append("</node>");


            string __debuqQuery = __query.ToString();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, null);
                if (_myManageData1._mode == 1)
                {
                    _myManageData1._dataList._refreshData();
                    _myManageData1._afterInsertData();
                }
                else
                {
                    _myManageData1._afterUpdateData();
                }
                _screen_view_manage_custom._clear();
                _viewColumnGrid._clear();
                _screen_view_manage_custom._focusFirst();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        object[] _gridColumnAlignList = new object[] { "Left", "Center", "Right" };

        void _gridTemp__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.ToLower().CompareTo(_g.d.erp_view_column_custom._column_field_name) == 0 || e._columnName.ToLower().CompareTo(_g.d.erp_view_column_custom._column_field_sort) == 0)
            {
                _searchField = e._columnName;
                _showSearchColumnGrid();                
            }
        }

        void _showSearchColumnGrid()
        {
            string __getDataFromScreen = _screen_view_manage_custom._getDataStr(_g.d.erp_view_table_custom._table_list).ToLower();
            if (_oldTableName.ToLower().CompareTo(__getDataFromScreen) != 0)
            {
                _oldTableName = __getDataFromScreen;
                _fieldList._clear();
                string[] __table = _oldTableName.Split(',');
                for (int __loop = 0; __loop < __table.Length; __loop++)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __getXml = __myFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, __table[__loop]);
                    XmlDocument __xDoc = new XmlDocument();
                    __xDoc.LoadXml(__getXml);
                    __xDoc.DocumentElement.Normalize();
                    XmlElement __xRoot = __xDoc.DocumentElement;
                    XmlNodeList __xReader = __xRoot.GetElementsByTagName("detail");
                    for (int __detail = 0; __detail < __xReader.Count; __detail++)
                    {
                        XmlNode __xFirstNode = __xReader.Item(__detail);
                        if (__xFirstNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement __xTable = (XmlElement)__xFirstNode;
                            int __addr = _fieldList._addRow();
                            string __tableName = __xTable.GetAttribute("table_name");
                            string __columnName = __xTable.GetAttribute(_g.d.erp_view_column_custom._column_name);
                            string __columnLength = __xTable.GetAttribute("length");
                            string __mixedName = __tableName + "." + __columnName;
                            string __getFromResource = __mixedName;
                            if (MyLib._myGlobal._isDesignMode == false)
                            {
                                __getFromResource = MyLib._myResource._findResource(__mixedName, __mixedName)._str;
                            }
                            _fieldList._cellUpdate(__addr, _field_full_name, __getFromResource, false);
                            _fieldList._cellUpdate(__addr, _field_name, __mixedName, false);
                            _fieldList._cellUpdate(__addr, _field_type, __xTable.GetAttribute("type"), false);
                            _fieldList._cellUpdate(__addr, _field_length, (__columnLength.Length == 0) ? 0 : Convert.ToInt32(__columnLength), false);
                        }
                    }
                } // for
            }
            // แสดงให้เลือก
            _searchWindows = new MyLib._myForm();
            _searchWindows.Size = _windowsSearchOldSize;
            _searchWindows.MinimumSize = new Size(400, 500);
            _searchWindows.DesktopLocation = _windowsDesktopLocation;
            _searchWindows.StartPosition = FormStartPosition.Manual;
            _searchWindows.Text = "Search Field";
            _searchWindows.ShowIcon = true;
            _searchWindows.ShowInTaskbar = true;
            _searchWindows.MinimizeBox = false;
            _searchWindows.Padding = new Padding(5, 5, 5, 5);
            _fieldList.Dock = DockStyle.Fill;
            _fieldList._isEdit = false;
            _searchWindows.Controls.Add(_fieldList);
            _searchWindows.SizeChanged += new EventHandler(searchWindows_SizeChanged);
            _searchWindows.Move += new EventHandler(searchWindows_Move);
            _searchWindows.ShowDialog(this);
        }

        void searchWindows_Move(object sender, EventArgs e)
        {
            _windowsDesktopLocation = ((MyLib._myForm)sender).DesktopLocation;
        }

        void searchWindows_SizeChanged(object sender, EventArgs e)
        {
            if (((MyLib._myForm)sender).WindowState == FormWindowState.Normal)
            {
                _windowsSearchOldSize = ((MyLib._myForm)sender).Size;
            }
        }

        string _gridTemp__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            if (select == -1)
                return ("");
            if (columnName.ToLower().CompareTo(_g.d.erp_view_column_custom._column_align) == 0)
                return (_gridColumnAlignList[select].ToString());
            return (MyLib._myGlobal._databaseColumnTypeList[select].ToString());
        }

        object[] _gridTemp__cellComboBoxItem(object sender, int row, int column)
        {
            if (column == 5)
                return (_gridColumnAlignList);
            return (MyLib._myGlobal._databaseColumnTypeList);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                int __columnNumber = this._myManageData1._dataList._gridData._findColumnByName(_g.d.erp_view_table_custom._table + "." + _g.d.erp_view_table_custom._screen_code);
                ArrayList __rowDataArray = (ArrayList)rowData;
                string screenCode = __rowDataArray[__columnNumber].ToString();

                //DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString);
                //_posMachineScreen1._loadData(__getData.Tables[0]);
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.erp_view_table_custom._table + " where " + _g.d.erp_view_table_custom._screen_code + "=\'" + screenCode + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.erp_view_column_custom._table + " where " + _g.d.erp_view_column_custom._screen_code + "=\'" + screenCode + "\'"));
                __query.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                DataTable __mainTable = ((DataSet)__result[0]).Tables[0];
                DataTable __viewColumn = ((DataSet)__result[1]).Tables[0];

                Control __codeControl = _screen_view_manage_custom._getControl(_g.d.erp_view_table_custom._screen_code);
                __codeControl.Enabled = false;
                if (forEdit)
                {
                    _screen_view_manage_custom._focusFirst();
                }
                _screen_view_manage_custom._loadData(__mainTable);
                this._viewColumnGrid._loadFromDataTable(__viewColumn);

                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        bool _myManageData1__discardData()
        {
            bool __result = true;
            if (_screen_view_manage_custom._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    __result = false;
                }
                else
                {
                    _screen_view_manage_custom._isChange = false;
                }
            }
            return (__result);
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                string __screen_code = this._myManageData1._dataList._gridData._cellGet(getData.row, _g.d.erp_view_table_custom._table + "." + _g.d.erp_view_table_custom._screen_code).ToString();

                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + _g.d.erp_view_table_custom._screen_code + " =\'{1}\'"), _myManageData1._dataList._tableName, __screen_code));
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + _g.d.erp_view_column_custom._screen_code + " =\'{1}\'"), _g.d.erp_view_column_custom._table, __screen_code));
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

        void _myManageData1__newDataClick()
        {
            Control __codeControl = _screen_view_manage_custom._getControl(_g.d.erp_view_table_custom._screen_code);
            __codeControl.Enabled = true;
            _screen_view_manage_custom._focusFirst();
        }

        void _myManageData1__clearData()
        {
            _screen_view_manage_custom._clear();
            this._viewColumnGrid._clear();
        }

        Boolean _posMachineScreen1__checkKeyDown(object sender, Keys keyData)
        {
            if (_myToolBar.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
                this._screen_view_manage_custom._isChange = false;
            }
            return true;
        }

        void _posMachineScreen1__saveKeyDown(object sender)
        {
            _save_data();
        }

        void _side_Resize(object sender, EventArgs e)
        {
            if (_myManageData1._dataList._loadViewDataSuccess == false)
            {
                _myManageData1._dataListOpen = true;
                _myManageData1._calcArea();
                _myManageData1._dataList._loadViewData(0);
            }
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _previewButton_Click(object sender, System.EventArgs e)
        {
            MyLib._myForm __testWindows = new MyLib._myForm();
            MyLib._myGrid __testGrid = new MyLib._myGrid();
            __testWindows.WindowState = FormWindowState.Maximized;
            __testWindows.Text = "Test";
            __testWindows.ShowIcon = true;
            __testWindows.ShowInTaskbar = true;
            __testWindows.Padding = new Padding(5, 5, 5, 5);
            __testGrid.Dock = DockStyle.Fill;
            __testGrid._isEdit = false;
            __testGrid._width_by_persent = (_screen_view_manage_custom._getDataStr("width_persent").CompareTo("1") == 0) ? true : false;
            MyLib._myGrid __gridTemp = (MyLib._myGrid)_viewColumnGrid;
            // string testQuery = "select top 50 "; 
            StringBuilder __testQuery = new StringBuilder("select  ");//somruk
            bool __firstPass = false;
            for (int __loop = 0; __loop < __gridTemp._rowData.Count; __loop++)
            {
                string __columnName = __gridTemp._cellGet(__loop, _g.d.erp_view_column_custom._column_name).ToString();
                string __columnFieldName = __gridTemp._cellGet(__loop, _g.d.erp_view_column_custom._column_field_name, false).ToString();
                string __columnFieldSort = __gridTemp._cellGet(__loop, _g.d.erp_view_column_custom._column_field_sort, false).ToString().Split(',')[0].ToString();
                int __columnType = (int)__gridTemp._cellGet(__loop, _g.d.erp_view_column_custom._column_type);
                int __columnWidth = (int)__gridTemp._cellGet(__loop, _g.d.erp_view_column_custom._column_width);
                string __formatStr = __gridTemp._cellGet(__loop, _g.d.erp_view_column_custom._column_format).ToString();
                //if (__columnWidth < 10) __columnWidth = 10;
                if (__columnName.Length > 0)
                {
                    if (__firstPass)
                        __testQuery.Append(",");
                    // toe __testQuery.Append(String.Concat(__columnFieldName, " as \"", __columnFieldSort, "\""));
                    __testQuery.Append(String.Concat(__columnFieldName, " as \"", __columnFieldName, "\""));
                    __firstPass = true;
                    switch (__columnType)
                    {
                        case 0: // varchar
                            __testGrid._addColumn(__columnFieldSort, 1, 0, __columnWidth, false, ((__columnWidth == 0) ? true : false));
                            break;
                        case 2:
                        case 3:
                        case 4:// int,smallint,tinyint
                            if (__formatStr.Length == 0)
                            {
                                __formatStr = "0";
                                __gridTemp._cellUpdate(__loop, "column_format", __formatStr, true);
                            }
                            __testGrid._addColumn(__columnFieldSort, 2, 0, __columnWidth, false, ((__columnWidth == 0) ? true : false), false, false, __formatStr);
                            break;
                        case 5: // float
                            if (__formatStr.Length == 0)
                            {
                                __formatStr = "2";
                                __gridTemp._cellUpdate(__loop, "column_format", __formatStr, true);
                            }
                            __testGrid._addColumn(__columnFieldSort, 5, 0, __columnWidth, false, ((__columnWidth == 0) ? true : false), false, false, __formatStr);
                            break;
                        case 6:// date
                            __testGrid._addColumn(__columnFieldSort, 4, 0, __columnWidth, false, ((__columnWidth == 0) ? true : false));
                            break;
                    }
                }
            } // for
            try
            {
                __testQuery.Append(String.Concat(" from ", _screen_view_manage_custom._getDataStr("table_list")));
                MyLib._myFrameWork __myFameWork = new MyLib._myFrameWork();
                // DataSet dataDetail = myFameWork._query(MyLib._myGlobal._databaseName, testQuery);//somruk
                MyLib._queryReturn dataDetail = __myFameWork._queryLimit(MyLib._myGlobal._databaseName, "select count (*) as xcount  from " + _screen_view_manage_custom._getDataStr("table_list"), __testQuery.ToString(), 0, 50, 1);
                // testGrid._loadFromDataTable(dataDetail.Tables[0]);
                __testGrid._loadFromDataTable(dataDetail.detail.Tables[0]);//somruk
                __testWindows.Controls.Add(__testGrid);
                __testWindows.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail.\n" + ex.Message.ToString(), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

    public class _screen_view_manage_custom : MyLib._myScreen
    {
        public _screen_view_manage_custom()
        {
            this._maxColumn = 1;
            this._autoUpperString = false;
            this._table_name = _g.d.erp_view_table_custom._table;
            this._addTextBox(0, 0, 1, 0, _g.d.erp_view_table_custom._screen_code, 1, 20, 1, true, false);
            this._addTextBox(1, 0, _g.d.erp_view_table_custom._name_1, 20);
            this._addTextBox(2, 0, _g.d.erp_view_table_custom._name_2, 20);
            this._addTextBox(3, 0, _g.d.erp_view_table_custom._table_name, 20);
            this._addTextBox(4, 0, _g.d.erp_view_table_custom._table_list, 20);
            this._addTextBox(5, 0, 1, 0, _g.d.erp_view_table_custom._sort, 1, 20, 1, true, false);
            this._addTextBox(6, 0, _g.d.erp_view_table_custom._filter, 20);
            //this._addNumberBox(7, 0, 1, 1, _g.d.erp_view_table_custom._width_persent, 1, 1, true);
            this._addCheckBox(7, 0, _g.d.erp_view_table_custom._width_persent, false, true);
            this._addComboBox(8, 0, _g.d.erp_view_table_custom._sort_mode, true, new string[] { _g.d.erp_view_table_custom._sort_ascending, _g.d.erp_view_table_custom._sort_descending }, true);
            
        }
    }
}
