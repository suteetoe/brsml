using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;

namespace MyLib._databaseManage
{
    public partial class _viewManage : UserControl
    {
        string _tableNameView = MyLib._myGlobal._tableNameView;
        string _tableNameViewColumn = MyLib._myGlobal._tableNameViewColumn;
        Boolean _oldRecord = false;
        string _screenCode = "";
        _myGrid _fieldList = new _myGrid();
        string _oldTableName = "";
        Size _windowsSearchOldSize = new Size(600, MyLib._myGlobal._mainSize.Height - 20);
        Point _windowsDesktopLocation;
        ArrayList _editGrid = new ArrayList();
        _myForm _searchWindows;
        string _column_field_name = "column_field_name";
        string _column_name = "column_name";
        string _column_name_2 = "column_name_2";
        string _column_field_sort = "column_field_sort";
        string _column_width = "column_width";
        string _column_type = "column_type";
        string _column_align = "column_align";
        string _column_format = "column_format";
        string _column_number = "column_number";
        string _field_full_name = "field_full_name";
        string _field_name = "field_name";
        string _field_type = "field_type";
        string _field_length = "field_length";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editMode">True=แก้ได้,False=ดูอย่างเดียว</param>
        public _viewManage(Boolean editMode)
        {
            InitializeComponent();
            this._toolStrip1.Visible = editMode;
            this.Load += new EventHandler(_viewManage_Load);
            for (int __loop = 0; __loop < 9; __loop++)
            {
                _myGrid __gridTtemp = new _myGrid();
                __gridTtemp._table_name = "erp_view_column";
                __gridTtemp._width_by_persent = true;
                __gridTtemp._addColumn(_column_field_name, 1, 100, 15, true, false, true, true);
                __gridTtemp._addColumn(_column_name, 1, 100, 15, true);
                __gridTtemp._addColumn(_column_name_2, 1, 100, 15, true);
                __gridTtemp._addColumn(_column_field_sort, 1, 10, 15, true, false, true, true);
                __gridTtemp._addColumn(_column_width, 2, 10, 10, true, false, true, false, "#,0");
                __gridTtemp._addColumn(_column_align, 10, 10, 10, true);
                __gridTtemp._addColumn(_column_type, 10, 10, 10, true);
                __gridTtemp._addColumn(_column_format, 1, 10, 10, true);
                __gridTtemp._addColumn(_column_number, 2, 10, 8, true, true);
                __gridTtemp.Dock = DockStyle.Fill;
                __gridTtemp._cellComboBoxItem += new CellComboBoxItemEventHandler(_gridTemp__cellComboBoxItem);
                __gridTtemp._cellComboBoxGet += new CellComboBoxItemGetDisplay(_gridTemp__cellComboBoxGet);
                __gridTtemp._clickSearchButton += new SearchEventHandler(_gridTemp__clickSearchButton);
                __gridTtemp._isEdit = editMode;
                _editGrid.Add(__gridTtemp);
                //
                _group.TabPages[__loop].Padding = new Padding(2, 2, 2, 2);
                _group.TabPages[__loop].Controls.Add(__gridTtemp);
                _fieldList._mouseClick += new MouseClickHandler(fieldList__mouseClick);
            }
            this._workPanel.Controls.Add(this._group);
            //
            _splitContainer.Panel2.Enabled = false;
            XmlDocument __xDoc = new XmlDocument();
            _myFrameWork __myFameWork = new _myFrameWork();
            string getXml = __myFameWork._dataStruct(MyLib._myGlobal._dataViewXmlFileName).GetXml();
            __xDoc.LoadXml(getXml);
            __xDoc.DocumentElement.Normalize();
            XmlElement __xRoot = __xDoc.DocumentElement;
            XmlNodeList __xReader = __xRoot.GetElementsByTagName("main");
            for (int __table = 0; __table < __xReader.Count; __table++)
            {
                XmlNode __xFirstNode = __xReader.Item(__table);
                if (__xFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement __xTable = (XmlElement)__xFirstNode;

                    TreeNode __mainNode = _menuList.Nodes.Add(__xTable.GetAttribute("id"), __xTable.GetAttribute("name"));
                    __mainNode.ImageKey = __mainNode.SelectedImageKey = "folder";
                    XmlNodeList __xField = __xTable.GetElementsByTagName("detail");
                    for (int __field = 0; __field < __xField.Count; __field++)
                    {
                        XmlNode __xReadNode = __xField.Item(__field);
                        if (__xReadNode != null)
                        {
                            if (__xReadNode.NodeType == XmlNodeType.Element)
                            {
                                XmlElement __xGetField = (XmlElement)__xReadNode;
                                TreeNode __temp = __mainNode.Nodes.Add(__xGetField.GetAttribute("screen_code"), __xGetField.GetAttribute("name") + "/" + __xGetField.GetAttribute("screen_code"));
                                __temp.ImageKey = "window";
                                __temp.SelectedImageKey = "window_edit";
                            }
                        }
                    } // for
                }
            } // for
            _menuList.ExpandAll();
            _myScreen1._maxColumn = 1;
            _myScreen1._table_name = "erp_view_table";
            _myScreen1._addTextBox(0, 0, "name_1", 100);
            _myScreen1._addTextBox(1, 0, "name_2", 100);
            _myScreen1._addTextBox(2, 0, "table_name", 100);
            _myScreen1._addTextBox(3, 0, "table_list", 100);
            _myScreen1._addTextBox(4, 0, "filter", 100);
            _myScreen1._addTextBox(5, 0, "sort", 100);
            _myScreen1._addCheckBox(6, 0, "width_persent", true, false);
            //
            _menuList.DoubleClick += new EventHandler(_menuList_DoubleClick);
            //
            // สำหรับค้นหาชื่อ Field
            _fieldList._table_name = "erp_other";
            _fieldList._addColumn(_field_name, 1, 10, 35);
            _fieldList._addColumn(_field_full_name, 1, 10, 40);
            _fieldList._addColumn(_field_type, 1, 10, 15);
            _fieldList._addColumn(_field_length, 2, 10, 7);
            //
            this._flowLayoutPanel.Controls.Add(this.ButtonSave);
            this._flowLayoutPanel.Controls.Add(this.ButtonExit);
            this._flowLayoutPanel.Controls.Add(this.ButtonTest);
        }

        void _viewManage_Load(object sender, EventArgs e)
        {
            _windowsDesktopLocation = new Point(MyLib._myGlobal._mainSize.Width - 620, 10);
        }

        void fieldList__mouseClick(object sender, GridCellEventArgs e)
        {
            _myGrid __gridTemp = (_myGrid)_editGrid[_group.SelectedIndex];
            if (e._row < _fieldList._rowData.Count)
            {
                __gridTemp._cellUpdate(__gridTemp._selectRow, _column_field_name, _fieldList._cellGet(e._row, "field_name"), true);
                __gridTemp._cellUpdate(__gridTemp._selectRow, _column_name, _fieldList._cellGet(e._row, "field_full_name"), true);
                __gridTemp._cellUpdate(__gridTemp._selectRow, _column_field_sort, _fieldList._cellGet(e._row, "field_name"), true);
                int __fieldLength = (int)_fieldList._cellGet(e._row, "field_length") * 10;
                if (__fieldLength > 100) __fieldLength = 100;
                __gridTemp._cellUpdate(__gridTemp._selectRow, _column_width, __fieldLength, true);
                __gridTemp._cellUpdate(__gridTemp._selectRow, _column_type, MyLib._myGlobal._databaseColumnFind(_fieldList._cellGet(e._row, "field_type").ToString()), true);
                __gridTemp.Invalidate();
                _searchWindows.Controls.Remove(_fieldList);
                _searchWindows.Close();
            }
        }

        void _gridTemp__clickSearchButton(object sender, GridCellEventArgs e)
        {
            if (e._columnName.ToLower().CompareTo(_column_field_name) == 0 || e._columnName.ToLower().CompareTo(_column_field_sort) == 0)
            {
                string __getDataFromScreen = _myScreen1._getDataStr("table_list").ToLower();
                if (_oldTableName.ToLower().CompareTo(__getDataFromScreen) != 0)
                {
                    _oldTableName = __getDataFromScreen;
                    _fieldList._clear();
                    string[] __table = _oldTableName.Split(',');
                    for (int __loop = 0; __loop < __table.Length; __loop++)
                    {
                        _myFrameWork __myFrameWork = new _myFrameWork();
                        string __getXml = __myFrameWork._getSchemaTable(_myGlobal._databaseName, __table[__loop]);
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
                                string __columnName = __xTable.GetAttribute(_column_name);
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
                _searchWindows = new _myForm();
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
        }

        void searchWindows_Move(object sender, EventArgs e)
        {
            _windowsDesktopLocation = ((_myForm)sender).DesktopLocation;
        }

        void searchWindows_SizeChanged(object sender, EventArgs e)
        {
            if (((_myForm)sender).WindowState == FormWindowState.Normal)
            {
                _windowsSearchOldSize = ((_myForm)sender).Size;
            }
        }


        object[] _gridColumnAlignList = new object[] { "Left", "Center", "Right" };

        string _gridTemp__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            if (select == -1)
                return ("");
            if (columnName.ToLower().CompareTo(_column_align) == 0)
                return (_gridColumnAlignList[select].ToString());
            return (_myGlobal._databaseColumnTypeList[select].ToString());
        }

        object[] _gridTemp__cellComboBoxItem(object sender, int row, int column)
        {
            if (column == 5)
                return (_gridColumnAlignList);
            return (_myGlobal._databaseColumnTypeList);
        }

        void _menuList_DoubleClick(object sender, EventArgs e)
        {
            _splitContainer.Panel2.Enabled = true;
            _myScreen1._focusFirst();
        }

        bool _isChange()
        {
            bool __gridChange = false;
            for (int __loop = 0; __loop < 9; __loop++)
            {
                _myGrid __temp = (_myGrid)_editGrid[__loop];
                if (__temp._isChange)
                {
                    __gridChange = true;
                    break;
                }
            }
            return ((_myScreen1._isChange || __gridChange) ? true : false);
        }

        bool _isRenew()
        {
            if (_isChange())
            {
                DialogResult result = MessageBox.Show(MyLib._myGlobal._resource("warning33"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel) return (false);
            }
            return (true);
        }

        void _loadData()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                _splitContainer.Panel2.Enabled = false;
                _myScreen1._clear();
                for (int __loop = 0; __loop < 9; __loop++)
                {
                    ((_myGrid)_editGrid[__loop])._clear();
                }
                _myFrameWork __myFrameWork = new _myFrameWork();
                DataSet __dataHead = __myFrameWork._query(_myGlobal._databaseName, "select * from " + _tableNameView + " where " + _myGlobal._addUpper("screen_code") + "=\'" + _screenCode.ToUpper() + "\' order by screen_code");
                _oldRecord = _myScreen1._loadData(__dataHead.Tables[0]);
                if (_oldRecord)
                {
                    DataSet dataDetail = __myFrameWork._query(_myGlobal._databaseName, "select * from " + _tableNameViewColumn + " where " + _myGlobal._addUpper("screen_code") + "=\'" + _screenCode.ToUpper() + "\' order by screen_code");
                    for (int __loop = 0; __loop < 9; __loop++)
                    {
                        try
                        {
                            _myGrid __temp = (_myGrid)_editGrid[__loop];
                            __temp._loadFromDataTable(dataDetail.Tables[0], dataDetail.Tables[0].Select("screen_group=" + (__loop + 1)));
                        }
                        catch
                        {
                            // Debugger.Break();
                        }
                    }
                }
            }
        }

        private void _myTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_isRenew())
            {
                _screenCode = "";
                if (e.Node.IsExpanded == false)
                {
                    _screenCode = e.Node.Name;
                    _loadData();
                }
            }
        }

        private void _myScreen1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            _closeScreen();
        }

        void _closeScreen()
        {
            try
            {
                if (_isRenew())
                {
                    this.Dispose();
                }
            }
            catch
            {
                // Debugger.Break();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // save ทั้งหมด
            if (_screenCode.Length > 0)
            {
                for (int gridList = 0; gridList < 9; gridList++)
                {
                    int __loop = 0;
                    _myGrid __temp = (_myGrid)_editGrid[gridList];
                    while (__loop < __temp._rowData.Count)
                    {
                        string __getStr = __temp._cellGet(__loop, 0).ToString();
                        if (__getStr.Length == 0)
                        {
                            __temp._rowData.RemoveAt(__loop);
                        }
                        else __loop++;
                    }//while
                    for (int __row = 0; __row < __temp._rowData.Count; __row++)
                    {
                        __temp._cellUpdate(__row, "column_number", __row + 1, false);
                    } // for
                }
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append("<query>delete from " + _tableNameView + " where " + _myGlobal._addUpper("screen_code") + "=\'" + _screenCode.ToUpper() + "\'</query>");
                __myQuery.Append("<query>delete from " + _tableNameViewColumn + " where " + _myGlobal._addUpper("screen_code") + "=\'" + _screenCode.ToUpper() + "\'</query>");
                __myQuery.Append("<query>");
                ArrayList __getQueryFromScreen = _myScreen1._createQueryForDatabase();
                __myQuery.Append("insert into " + _tableNameView + " (screen_code," + __getQueryFromScreen[0] + " ) ");
                __myQuery.Append("values (\'" + _screenCode + "\'," + __getQueryFromScreen[1] + ")");
                __myQuery.Append("</query>");
                for (int __gridList = 0; __gridList < 9; __gridList++)
                {
                    _myGrid __temp = (_myGrid)_editGrid[__gridList];
                    __temp._updateRowIsChangeAll(true);
                    __myQuery.Append(__temp._createQueryForInsert(_tableNameViewColumn, "screen_group,screen_code,", (__gridList + 1) + ",\'" + _screenCode + "\',"));
                }
                __myQuery.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("save_success"), MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    for (int __loop = 0; __loop < 9; __loop++)
                    {
                        ((_myGrid)_editGrid[__loop])._isChange = false;
                    }
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _myGrid1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonLoadTemplate_Click(object sender, EventArgs e)
        {
            if (_isRenew())
            {
                try
                {
                    _myFrameWork __myFrameWork = new _myFrameWork();
                    string __getXml = __myFrameWork._getViewTemplate(MyLib._myGlobal._dataViewTemplateXmlFileName, _screenCode);
                    if (__getXml.Length > 0)
                    {
                        _myScreen1._clear();
                        _myGrid __gridTemp = (_myGrid)_editGrid[_group.SelectedIndex];
                        __gridTemp._clear();
                        XmlDocument __xDoc = new XmlDocument();
                        __xDoc.LoadXml(MyLib._myGlobal._xmlHeader + "<node>" + __getXml + "</node>");
                        __xDoc.DocumentElement.Normalize();
                        XmlElement __xRoot = __xDoc.DocumentElement;
                        XmlNodeList __xReader = __xRoot.GetElementsByTagName("screen");
                        for (int __table = 0; __table < __xReader.Count; __table++)
                        {
                            XmlNode __xFirstNode = __xReader.Item(__table);
                            if (__xFirstNode.NodeType == XmlNodeType.Element)
                            {
                                XmlElement __xTable = (XmlElement)__xFirstNode;
                                _myScreen1._setDataStr("name_1", __xTable.GetAttribute("name_1"));
                                _myScreen1._setDataStr("name_2", __xTable.GetAttribute("name_2"));
                                _myScreen1._setDataStr("table_name", __xTable.GetAttribute("table_name"));
                                _myScreen1._setDataStr("table_list", __xTable.GetAttribute("table_list"));
                                _myScreen1._setDataStr("filter", __xTable.GetAttribute("filter"));
                                _myScreen1._setDataStr("sort", __xTable.GetAttribute("sort"));
                                _myScreen1._setCheckBox("width_persent", __xTable.GetAttribute("width_by_persent"));
                                _myScreen1._isChange = false;
                                //
                                XmlNodeList __xDetail = __xTable.GetElementsByTagName("detail");
                                for (int __detail = 0; __detail < __xDetail.Count; __detail++)
                                {
                                    XmlNode __xDetailFirstNode = __xDetail.Item(__detail);
                                    if (__xDetailFirstNode.NodeType == XmlNodeType.Element)
                                    {
                                        XmlElement __xDetailData = (XmlElement)__xDetailFirstNode;
                                        __gridTemp._addRow();
                                        int __addr = __gridTemp._rowData.Count - 1;
                                        __gridTemp._cellUpdate(__addr, _column_name, __xDetailData.GetAttribute(_column_name), false);
                                        __gridTemp._cellUpdate(__addr, _column_name_2, __xDetailData.GetAttribute(_column_name_2), false);
                                        __gridTemp._cellUpdate(__addr, _column_field_name, __xDetailData.GetAttribute(_column_field_name), false);
                                        __gridTemp._cellUpdate(__addr, _column_field_sort, __xDetailData.GetAttribute(_column_field_sort), false);
                                        __gridTemp._cellUpdate(__addr, _column_width, Convert.ToInt32(__xDetailData.GetAttribute(_column_width)), false);
                                        __gridTemp._cellUpdate(__addr, _column_type, MyLib._myGlobal._databaseColumnFind(__xDetailData.GetAttribute(_column_type)), false);
                                        __gridTemp._cellUpdate(__addr, _column_format, __xDetailData.GetAttribute("format"), false);
                                        int __alignInt = 0;
                                        switch (__xDetailData.GetAttribute(_column_align).ToLower())
                                        {
                                            case "left": __alignInt = 0; break;
                                            case "center": __alignInt = 1; break;
                                            case "right": __alignInt = 2; break;
                                        }
                                        __gridTemp._cellUpdate(__addr, _column_align, __alignInt, false);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning101") + "\n" + ex.Message.ToString(), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonTest_Click(object sender, EventArgs e)
        {
            _myForm __testWindows = new _myForm();
            _myGrid __testGrid = new _myGrid();
            __testWindows.WindowState = FormWindowState.Maximized;
            __testWindows.Text = "Test";
            __testWindows.ShowIcon = true;
            __testWindows.ShowInTaskbar = true;
            __testWindows.Padding = new Padding(5, 5, 5, 5);
            __testGrid.Dock = DockStyle.Fill;
            __testGrid._isEdit = false;
            __testGrid._width_by_persent = (_myScreen1._getDataStr("width_persent").CompareTo("1") == 0) ? true : false;
            _myGrid __gridTemp = (_myGrid)_editGrid[_group.SelectedIndex];
            // string testQuery = "select top 50 "; 
            StringBuilder __testQuery = new StringBuilder("select  ");//somruk
            bool __firstPass = false;
            for (int __loop = 0; __loop < __gridTemp._rowData.Count; __loop++)
            {
                string __columnName = __gridTemp._cellGet(__loop, _column_name).ToString();
                string __columnFieldName = __gridTemp._cellGet(__loop, _column_field_name).ToString();
                string __columnFieldSort = __gridTemp._cellGet(__loop, _column_field_sort).ToString();
                int __columnType = (int)__gridTemp._cellGet(__loop, _column_type);
                int __columnWidth = (int)__gridTemp._cellGet(__loop, _column_width);
                string __formatStr = __gridTemp._cellGet(__loop, _column_format).ToString();
                if (__columnWidth < 10) __columnWidth = 10;
                if (__columnName.Length > 0)
                {
                    if (__firstPass)
                        __testQuery.Append(",");
                    __testQuery.Append(String.Concat(__columnFieldName, " as \"", __columnFieldSort, "\""));
                    __firstPass = true;
                    switch (__columnType)
                    {
                        case 0: // varchar
                            __testGrid._addColumn(__columnFieldSort, 1, 0, __columnWidth);
                            break;
                        case 2:
                        case 3:
                        case 4:// int,smallint,tinyint
                            if (__formatStr.Length == 0)
                            {
                                __formatStr = "0";
                                __gridTemp._cellUpdate(__loop, "column_format", __formatStr, true);
                            }
                            __testGrid._addColumn(__columnFieldSort, 2, 0, __columnWidth, false, false, false, false, __formatStr);
                            break;
                        case 5: // float
                            if (__formatStr.Length == 0)
                            {
                                __formatStr = "2";
                                __gridTemp._cellUpdate(__loop, "column_format", __formatStr, true);
                            }
                            __testGrid._addColumn(__columnFieldSort, 5, 0, __columnWidth, false, false, false, false, __formatStr);
                            break;
                        case 6:// date
                            __testGrid._addColumn(__columnFieldSort, 4, 0, __columnWidth);
                            break;
                    }
                }
            } // for
            try
            {
                __testQuery.Append(String.Concat(" from ", _myScreen1._getDataStr("table_list")));
                _myFrameWork __myFameWork = new _myFrameWork();
                // DataSet dataDetail = myFameWork._query(MyLib._myGlobal._databaseName, testQuery);//somruk
                MyLib._queryReturn dataDetail = __myFameWork._queryLimit(MyLib._myGlobal._databaseName, "select count (*) as xcount  from " + _myScreen1._getDataStr("table_list"), __testQuery.ToString(), 0, 50, 1);
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

        private void group1_Click(object sender, EventArgs e)
        {

        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _resetButton_Click(object sender, EventArgs e)
        {
            DialogResult __result = MessageBox.Show("Reset to Default ?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (__result == DialogResult.Yes)
            {
                _myFrameWork __myFrameWork = new _myFrameWork();
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table erp_view_column");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table erp_view_table");
                __myFrameWork._sendXmlFile(MyLib._myGlobal._dataViewTemplateXmlFileName);
                //
                string __resultMessage = __myFrameWork._systemStartup(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, "SML", MyLib._myGlobal._tableNameView, MyLib._myGlobal._tableNameViewColumn, MyLib._myGlobal._dataViewXmlFileName, MyLib._myGlobal._dataViewTemplateXmlFileName);
                if (__resultMessage.Length > 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("มีข้อผิดพลาดในการตรวจสอบระบบ") + "\n" + __resultMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Reset Success and will close this screen.", "Success", MessageBoxButtons.OK);
                    this.Dispose();
                }
            }
        }
    }
}
