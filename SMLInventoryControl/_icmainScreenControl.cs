using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icmainScreenControl : UserControl
    {
        public _icmainScreenControl()
        {
            InitializeComponent();
        }
    }

    public class _icmainScreenAccountControl : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();
        public _icmainScreenAccountControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_inventory_detail._table;
            // this._addTextBox(0, 0, 0, 0, _g.d.ic_inventory_detail._tax_import, 1, 0, 1, true, false, true);
            this._addTextBox(0, 0, 0, 0, _g.d.ic_inventory_detail._tax_import, 1, 0, 0, true, false, true);
            this._addNumberBox(0, 1, 0, 0, _g.d.ic_inventory_detail._tax_rate, 1, 2, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenAccountControl__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenAccountControl__textBoxChanged);
        }

        void _icmainScreenAccountControl__textBoxSearch(object sender)
        {
            _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                if (!this._searchMasterScreen._name.ToLower().Equals(_searchScreenMasterList[0].ToString().ToLower()))
                {
                    _searchMasterScreen = new MyLib._searchDataFull();
                    this._searchMasterScreen.Text = ((MyLib._myTextBox)sender)._labelName;
                    this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                    this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                    this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterScreen._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _icmainScreenAccountControl__textBoxChanged(object sender, string name)
        {
            //แก้ไขยกเลิก 16/11/2552 16:25 PM
            ////if (name.Equals(_g.d.ic_inventory_detail._tax_import))
            ////{
            ////    this._searchTextBox = (TextBox)sender;
            ////    this._searchName = name;
            ////    this._search(true);
            ////}
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _searchAll(string name, int row)
        {

            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            this._searchMasterScreen.Visible = false;
            if (__result.Length > 0)
            {
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }

        }

        public void _search(Boolean warning)
        {
            try
            {
                // % ภาษีนำเข้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_import_duty._name_1 + " from " + _g.d.ic_import_duty._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_import_duty._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._tax_import).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_inventory_detail._tax_import, (DataSet)_getData[0], warning) == false) { }
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    public class _icmainScreenDescripControl : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();

        public _icmainScreenDescripControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_inventory._table;
            this._addTextBox(0, 0, 0, 0, _g.d.ic_inventory._item_brand, 1, 1, 1, true, false, true);
            this._addTextBox(0, 1, 0, 0, _g.d.ic_inventory._item_pattern, 1, 0, 1, true, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.ic_inventory._item_design, 1, 0, 1, true, false, true);
            this._addTextBox(1, 1, 0, 0, _g.d.ic_inventory._item_grade, 1, 0, 1, true, false, true);
            this._addTextBox(2, 0, 0, 0, _g.d.ic_inventory._item_model, 1, 0, 1, true, false, true);
            this._addTextBox(2, 1, 0, 0, _g.d.ic_inventory._item_category, 1, 0, 1, true, false, true);
            this._addTextBox(3, 0, 0, 0, _g.d.ic_inventory._group_main, 1, 0, 1, true, false, true);
            this._addTextBox(3, 1, 0, 0, _g.d.ic_inventory._item_class, 1, 0, 1, true, false, true);
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLThai7)
            {
                this._addNumberBox(3, 1, 0, 0, _g.d.ic_inventory._item_in_stock, 1, 0, true);
            }
            this._addTextBox(4, 0, 0, 0, _g.d.ic_inventory._group_sub, 1, 0, 1, true, false, true);
            this._addTextBox(5, 0, 3, 0, _g.d.ic_inventory._description, 2, 0, 0, true, false, true);
            this._addTextBox(9, 0, 0, 0, _g.d.ic_inventory._commission_rate, 1, 0, 0, true, false, true);
            this._addTextBox(10, 0, 0, 0, _g.d.ic_inventory._serial_no_format, 1, 0, 0, true, false, true);

            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged -= new MyLib.TextBoxChangedHandler(_icmainScreenDescripControl__textBoxChanged);
            this._textBoxSearch -= new MyLib.TextBoxSearchHandler(_icmainScreenDescripControl__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenDescripControl__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenDescripControl__textBoxSearch);
            //
            MyLib._myTextBox __getItemBrandControl = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory._item_brand);
            __getItemBrandControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemBrandControl.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemPatternControl = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory._item_pattern);
            __getItemPatternControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemPatternControl.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDesignControl = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory._item_design);
            __getItemDesignControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDesignControl.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGradeControl = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory._item_grade);
            __getItemGradeControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGradeControl.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemCategoryControl = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory._item_category);
            __getItemCategoryControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemCategoryControl.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupMainControl = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory._group_main);
            __getItemGroupMainControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupMainControl.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemClassControl = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory._item_class);
            __getItemClassControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemClassControl.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemModelControl = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory._item_model);
            __getItemModelControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemModelControl.textBox.Leave += new EventHandler(textBox_Leave);

            //
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenDescripControl__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void _icmainScreenDescripControl__textBoxSearch(object sender)
        {
            _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                if (!this._searchMasterScreen._name.ToLower().Equals(_searchScreenMasterList[0].ToString().ToLower()))
                {
                    _searchMasterScreen = new MyLib._searchDataFull();
                    this._searchMasterScreen.Text = ((MyLib._myTextBox)sender)._labelName;
                    this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                    this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                    this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    if (this._searchName.Equals(_g.d.ic_inventory._group_sub))
                    {
                        this._searchMasterScreen._dataList._extraWhere = _g.d.ic_group_sub._main_group + "=\'" + this._getDataStr(_g.d.ic_inventory._group_main).ToString() + "\'";
                    }
                    this._searchMasterScreen._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _icmainScreenDescripControl__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory._item_brand))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory._item_pattern))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory._item_design))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory._item_grade))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory._item_category))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory._group_main))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory._group_sub))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory._item_class))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _searchAll(string name, int row)
        {

            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }

        }

        public void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_pattern._name_1 + " from " + _g.d.ic_pattern._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_pattern._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._item_pattern).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_design._name_1 + " from " + _g.d.ic_design._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_design._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._item_design).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_grade._name_1 + " from " + _g.d.ic_grade._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_grade._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._item_grade).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_class._name_1 + " from " + _g.d.ic_class._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_class._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._item_class).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_character._name_1 + " from " + _g.d.ic_character._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_character._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._item_character).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_brand._name_1 + " from " + _g.d.ic_brand._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_brand._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._item_brand).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_category._name_1 + " from " + _g.d.ic_category._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_category._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._item_category).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_group._name_1 + " from " + _g.d.ic_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_group._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._group_main).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_model._name_1 + " from " + _g.d.ic_model._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_model._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._item_model).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_group_sub._name_1 + " from " + _g.d.ic_group_sub._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_group_sub._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._group_sub).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_inventory._item_pattern, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory._item_design, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory._item_grade, (DataSet)_getData[2], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory._item_class, (DataSet)_getData[3], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory._item_character, (DataSet)_getData[4], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory._item_brand, (DataSet)_getData[5], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory._item_category, (DataSet)_getData[6], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory._group_main, (DataSet)_getData[7], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory._item_model, (DataSet)_getData[8], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory._group_sub, (DataSet)_getData[9], warning) == false) { }
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    public class _icmainScreenGroupControl : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();
        DataTable __getName;

        public _icmainScreenGroupControl()
        {
            bool designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designMode == false)
            {

                this.__getName = this._myFrameWork._queryShort("select " + _g.d.ic_dimension_name._dimension_number + "," + _g.d.ic_dimension_name._name_1 + " from " + _g.d.ic_dimension_name._table).Tables[0];
            }
            this._build();
        }

        string _findName(int number, string source)
        {
            try
            {
                DataRow[] __select = this.__getName.Select(_g.d.ic_dimension_name._dimension_number + "=" + number.ToString());
                return (__select.Length == 0) ? source : __select[0][_g.d.ic_dimension_name._name_1].ToString();
            }
            catch
            {
                return source;
            }
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.ic_inventory_detail._table;
            this._addTextBox(0, 0, 0, 0, _g.d.ic_inventory_detail._dimension_1, 1, 0, 1, true, false, true);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_inventory_detail._dimension_2, 1, 0, 1, true, false, true);
            this._addTextBox(2, 0, 1, 0, _g.d.ic_inventory_detail._dimension_3, 1, 0, 1, true, false, true);
            this._addTextBox(3, 0, 1, 0, _g.d.ic_inventory_detail._dimension_4, 1, 0, 1, true, false, true);
            this._addTextBox(4, 0, 1, 0, _g.d.ic_inventory_detail._dimension_5, 1, 0, 1, true, false, true);
            this._addTextBox(5, 0, 0, 0, _g.d.ic_inventory_detail._dimension_6, 1, 0, 1, true, false, true);
            this._addTextBox(6, 0, 1, 0, _g.d.ic_inventory_detail._dimension_7, 1, 0, 1, true, false, true);
            this._addTextBox(7, 0, 1, 0, _g.d.ic_inventory_detail._dimension_8, 1, 0, 1, true, false, true);
            this._addTextBox(8, 0, 1, 0, _g.d.ic_inventory_detail._dimension_9, 1, 0, 1, true, false, true);
            this._addTextBox(9, 0, 1, 0, _g.d.ic_inventory_detail._dimension_10, 1, 0, 1, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenGroupControl__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenGroupControl__textBoxSearch);

            MyLib._myTextBox __getItemGroupSubControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_1);
            __getItemGroupSubControl1._tempValue1 = 1;
            __getItemGroupSubControl1._label.Text = this._findName(1, __getItemGroupSubControl1._label.Text);
            __getItemGroupSubControl1.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl1.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupSubControl2 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_2);
            __getItemGroupSubControl2._tempValue1 = 2;
            __getItemGroupSubControl2._label.Text = this._findName(2, __getItemGroupSubControl2._label.Text);
            __getItemGroupSubControl2.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl2.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupSubControl3 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_3);
            __getItemGroupSubControl3._tempValue1 = 3;
            __getItemGroupSubControl3._label.Text = this._findName(3, __getItemGroupSubControl3._label.Text);
            __getItemGroupSubControl3.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl3.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupSubControl4 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_4);
            __getItemGroupSubControl4._tempValue1 = 4;
            __getItemGroupSubControl4._label.Text = this._findName(4, __getItemGroupSubControl4._label.Text);
            __getItemGroupSubControl4.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl4.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupSubControl5 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_5);
            __getItemGroupSubControl5._tempValue1 = 5;
            __getItemGroupSubControl5._label.Text = this._findName(5, __getItemGroupSubControl5._label.Text);
            __getItemGroupSubControl5.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl5.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupSubControl6 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_6);
            __getItemGroupSubControl6._tempValue1 = 6;
            __getItemGroupSubControl6._label.Text = this._findName(6, __getItemGroupSubControl6._label.Text);
            __getItemGroupSubControl6.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl6.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupSubControl7 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_7);
            __getItemGroupSubControl7._tempValue1 = 7;
            __getItemGroupSubControl7._label.Text = this._findName(7, __getItemGroupSubControl7._label.Text);
            __getItemGroupSubControl7.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl7.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupSubControl8 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_8);
            __getItemGroupSubControl8._tempValue1 = 8;
            __getItemGroupSubControl8._label.Text = this._findName(8, __getItemGroupSubControl8._label.Text);
            __getItemGroupSubControl8.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl8.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupSubControl9 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_9);
            __getItemGroupSubControl9._tempValue1 = 9;
            __getItemGroupSubControl9._label.Text = this._findName(9, __getItemGroupSubControl9._label.Text);
            __getItemGroupSubControl9.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl9.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemGroupSubControl10 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_10);
            __getItemGroupSubControl10._tempValue1 = 10;
            __getItemGroupSubControl10._label.Text = this._findName(10, __getItemGroupSubControl10._label.Text);
            __getItemGroupSubControl10.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemGroupSubControl10.textBox.Leave += new EventHandler(textBox_Leave);

        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenGroupControl__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;
        }

        void _icmainScreenGroupControl__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory_detail._dimension_1))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_2))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_3))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_4))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_5))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_6))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_7))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_8))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_9))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_10))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _icmainScreenGroupControl__textBoxSearch(object sender)
        {
            _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                _searchMasterScreen = new MyLib._searchDataFull();
                this._searchMasterScreen.Text = ((MyLib._myTextBox)sender)._labelName;
                this._searchMasterScreen._dataList._extraWhere = _g.d.ic_dimension._dimension_no + "=" + __getControl._tempValue1.ToString();
                this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchMasterScreen._dataList._refreshData();
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _searchAll(string name, int row)
        {

            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                string __q1 = "select " + _g.d.ic_dimension._name_1 + " from " + _g.d.ic_dimension._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_dimension._code);
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_1).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_2).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_3).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_4).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_5).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_6).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_7).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_8).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_9).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_10).ToUpper() + "\'"));

                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_1, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_2, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_3, (DataSet)_getData[2], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_4, (DataSet)_getData[3], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_5, (DataSet)_getData[4], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_6, (DataSet)_getData[5], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_7, (DataSet)_getData[6], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_8, (DataSet)_getData[7], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_9, (DataSet)_getData[8], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_10, (DataSet)_getData[9], warning) == false) { }
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    public class _icmainScreenDimesionControl : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();
        DataTable __getName;

        public _icmainScreenDimesionControl()
        {
            bool designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designMode == false)
            {
                this.__getName = this._myFrameWork._queryShort("select " + _g.d.ic_dimension_name._dimension_number + "," + _g.d.ic_dimension_name._name_1 + " from " + _g.d.ic_dimension_name._table).Tables[0];
            }
            this._build();
        }

        string _findName(int number, string source)
        {
            try
            {
                DataRow[] __select = this.__getName.Select(_g.d.ic_dimension_name._dimension_number + "=" + number.ToString());
                return (__select.Length == 0) ? source : __select[0][_g.d.ic_dimension_name._name_1].ToString();
            }
            catch
            {
                return source;
            }
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.ic_inventory_detail._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_inventory_detail._dimension_11, 1, 1, 1, true, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.ic_inventory_detail._dimension_12, 1, 0, 1, true, false, true);
            this._addTextBox(2, 0, 1, 0, _g.d.ic_inventory_detail._dimension_13, 1, 0, 1, true, false, true);
            this._addTextBox(3, 0, 1, 0, _g.d.ic_inventory_detail._dimension_14, 1, 0, 1, true, false, true);
            this._addTextBox(4, 0, 1, 0, _g.d.ic_inventory_detail._dimension_15, 1, 1, 1, true, false, true);
            this._addTextBox(5, 0, 0, 0, _g.d.ic_inventory_detail._dimension_16, 1, 0, 1, true, false, true);
            this._addTextBox(6, 0, 1, 0, _g.d.ic_inventory_detail._dimension_17, 1, 0, 1, true, false, true);
            this._addTextBox(7, 0, 1, 0, _g.d.ic_inventory_detail._dimension_18, 1, 0, 1, true, false, true);
            this._addTextBox(8, 0, 1, 0, _g.d.ic_inventory_detail._dimension_19, 1, 1, 1, true, false, true);
            this._addTextBox(9, 0, 0, 0, _g.d.ic_inventory_detail._dimension_20, 1, 0, 1, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenDimesionControl__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenDimesionControl__textBoxSearch);

            MyLib._myTextBox __getItemDimemsionControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_11);
            __getItemDimemsionControl1._tempValue1 = 11;
            __getItemDimemsionControl1._label.Text = this._findName(11, __getItemDimemsionControl1._label.Text);
            __getItemDimemsionControl1.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl1.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDimemsionControl2 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_12);
            __getItemDimemsionControl2._tempValue1 = 12;
            __getItemDimemsionControl2._label.Text = this._findName(12, __getItemDimemsionControl2._label.Text);
            __getItemDimemsionControl2.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl2.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDimemsionControl3 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_13);
            __getItemDimemsionControl3._tempValue1 = 13;
            __getItemDimemsionControl3._label.Text = this._findName(13, __getItemDimemsionControl3._label.Text);
            __getItemDimemsionControl3.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl3.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDimemsionControl4 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_14);
            __getItemDimemsionControl4._tempValue1 = 14;
            __getItemDimemsionControl4._label.Text = this._findName(14, __getItemDimemsionControl4._label.Text);
            __getItemDimemsionControl4.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl4.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDimemsionControl5 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_15);
            __getItemDimemsionControl5._tempValue1 = 15;
            __getItemDimemsionControl5._label.Text = this._findName(15, __getItemDimemsionControl5._label.Text);
            __getItemDimemsionControl5.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl5.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDimemsionControl6 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_16);
            __getItemDimemsionControl6._tempValue1 = 16;
            __getItemDimemsionControl6._label.Text = this._findName(16, __getItemDimemsionControl6._label.Text);
            __getItemDimemsionControl6.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl6.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDimemsionControl7 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_17);
            __getItemDimemsionControl7._tempValue1 = 17;
            __getItemDimemsionControl7._label.Text = this._findName(17, __getItemDimemsionControl7._label.Text);
            __getItemDimemsionControl7.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl7.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDimemsionControl8 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_18);
            __getItemDimemsionControl8._tempValue1 = 18;
            __getItemDimemsionControl8._label.Text = this._findName(18, __getItemDimemsionControl8._label.Text);
            __getItemDimemsionControl8.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl8.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDimemsionControl9 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_19);
            __getItemDimemsionControl9._tempValue1 = 19;
            __getItemDimemsionControl9._label.Text = this._findName(19, __getItemDimemsionControl9._label.Text);
            __getItemDimemsionControl9.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl9.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemDimemsionControl10 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._dimension_20);
            __getItemDimemsionControl10._tempValue1 = 20;
            __getItemDimemsionControl10._label.Text = this._findName(20, __getItemDimemsionControl10._label.Text);
            __getItemDimemsionControl10.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemDimemsionControl10.textBox.Leave += new EventHandler(textBox_Leave);
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenDimesionControl__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;

        }

        void _icmainScreenDimesionControl__textBoxSearch(object sender)
        {
            _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                _searchMasterScreen = new MyLib._searchDataFull();
                this._searchMasterScreen.Text = ((MyLib._myTextBox)sender)._labelName;
                this._searchMasterScreen._dataList._extraWhere = _g.d.ic_dimension._dimension_no + "=" + __getControl._tempValue1.ToString();
                this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchMasterScreen._dataList._refreshData();
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _icmainScreenDimesionControl__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory_detail._dimension_11))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_12))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_13))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_14))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_15))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_16))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_17))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_18))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_19))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._dimension_20))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _searchAll(string name, int row)
        {
            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                string __q1 = "select " + _g.d.ic_dimension._name_1 + " from " + _g.d.ic_dimension._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_dimension._code);
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_11).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_12).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_13).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_14).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_15).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_16).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_17).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_18).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_19).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._dimension_20).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_11, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_12, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_13, (DataSet)_getData[2], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_14, (DataSet)_getData[3], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_15, (DataSet)_getData[4], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_16, (DataSet)_getData[5], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_17, (DataSet)_getData[6], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_18, (DataSet)_getData[7], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_19, (DataSet)_getData[8], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._dimension_20, (DataSet)_getData[9], warning) == false) { }
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    public class _icmainScreenPurchaseWh : MyLib._myScreen
    {
        public delegate string ItemCodeEventHandler();
        public event ItemCodeEventHandler _itemCode;

        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();

        public _icmainScreenPurchaseWh()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.ic_inventory_detail._table;
            this._addTextBox(0, 0, 0, 0, _g.d.ic_inventory_detail._start_purchase_wh, 1, 0, 1, true, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.ic_inventory_detail._start_purchase_shelf, 1, 0, 1, true, false, true);
            this._addTextBox(2, 0, 0, 0, _g.d.ic_inventory_detail._start_purchase_unit, 1, 0, 1, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenPurchaseWh__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenPurchaseWh__textBoxSearch);

            MyLib._myTextBox __getItemstartControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._start_purchase_wh);
            __getItemstartControl1.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl1.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemstartControl2 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._start_purchase_shelf);
            __getItemstartControl2.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl2.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemstartControl3 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._start_purchase_unit);
            __getItemstartControl3.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl3.textBox.Leave += new EventHandler(textBox_Leave);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenPurchaseWh__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void _icmainScreenPurchaseWh__textBoxSearch(object sender)
        {
            ArrayList _searchScreenMasterList = new ArrayList();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                _searchMasterScreen = new MyLib._searchDataFull();
                this._searchMasterScreen.Text = ((MyLib._myTextBox)sender)._labelName;
                this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                if (this._searchName.Equals(_g.d.ic_inventory_detail._start_purchase_unit))
                {
                    this._searchMasterScreen._dataList._extraWhere = _g.d.ic_unit_use._ic_code + "=\'" + this._itemCode() + "\'";
                }
                this._searchMasterScreen._dataList._refreshData();
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _icmainScreenPurchaseWh__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory_detail._start_purchase_wh))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._start_purchase_shelf))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._start_purchase_unit))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _searchAll(string name, int row)
        {

            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_warehouse._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._start_purchase_wh).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_shelf._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._start_purchase_shelf).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._start_purchase_unit).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_inventory_detail._start_purchase_wh, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._start_purchase_shelf, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._start_purchase_unit, (DataSet)_getData[2], warning) == false) { }
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    public class _icmainScreenSaleWh : MyLib._myScreen
    {
        public delegate string ItemCodeEventHandler();
        public event ItemCodeEventHandler _itemCode;

        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();

        public _icmainScreenSaleWh()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.ic_inventory_detail._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_inventory_detail._start_sale_wh, 1, 1, 1, true, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.ic_inventory_detail._start_sale_shelf, 1, 0, 1, true, false, true);
            this._addTextBox(2, 0, 1, 0, _g.d.ic_inventory_detail._start_sale_unit, 1, 0, 1, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenSaleWh__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenSaleWh__textBoxSearch);

            MyLib._myTextBox __getItemSaleWhControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._start_sale_wh);
            __getItemSaleWhControl1.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemSaleWhControl1.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemstartControl2 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._start_sale_shelf);
            __getItemstartControl2.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl2.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemstartControl3 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._start_sale_unit);
            __getItemstartControl3.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl3.textBox.Leave += new EventHandler(textBox_Leave);
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenSaleWh__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;
        }

        void _icmainScreenSaleWh__textBoxSearch(object sender)
        {
            ArrayList _searchScreenMasterList = new ArrayList();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                _searchMasterScreen = new MyLib._searchDataFull();
                this._searchMasterScreen.Text = ((MyLib._myTextBox)sender)._labelName;
                this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                if (this._searchName.Equals(_g.d.ic_inventory_detail._start_sale_unit))
                {
                    this._searchMasterScreen._dataList._extraWhere = _g.d.ic_unit_use._ic_code + "=\'" + this._itemCode() + "\'";
                }
                this._searchMasterScreen._dataList._refreshData();
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _icmainScreenSaleWh__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory_detail._start_sale_wh))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._start_sale_shelf))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._start_sale_unit))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _searchAll(string name, int row)
        {

            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }

        }

        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_warehouse._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._start_sale_wh).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_shelf._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._start_sale_shelf).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._start_sale_unit).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_inventory_detail._start_sale_wh, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._start_sale_shelf, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._start_sale_unit, (DataSet)_getData[2], warning) == false) { }
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    /*
    public class _icmainScreenStartUnit : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();
        public _icmainScreenStartUnit()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.ic_inventory_detail._table;
            this._addTextBox(0, 0, 0, 0, _g.d.ic_inventory_detail._start_unit_code, 1, 0, 1, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenOutWh__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenOutWh__textBoxSearch);

            MyLib._myTextBox __getItemstartControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._start_unit_code);
            __getItemstartControl1.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl1.textBox.Leave += new EventHandler(textBox_Leave);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenOutWh__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void _icmainScreenOutWh__textBoxSearch(object sender)
        {
            _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                if (!this._searchMasterScreen._name.ToLower().Equals(_searchScreenMasterList[0].ToString().ToLower()))
                {
                    _searchMasterScreen = new MyLib._searchDataFull();
                    this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                    this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                    this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterScreen._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _icmainScreenOutWh__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory_detail._start_unit_code))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _searchAll(string name, int row)
        {

            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }

        }

        public void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._start_unit_code).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_inventory_detail._start_unit_code, (DataSet)_getData[0], warning) == false) { }
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
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }*/

    public class _icmainScreenOutWh : MyLib._myScreen
    {
        public delegate string ItemCodeEventHandler();
        public event ItemCodeEventHandler _itemCode;

        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();
        public _icmainScreenOutWh()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.ic_inventory_detail._table;
            this._addTextBox(0, 0, 0, 0, _g.d.ic_inventory_detail._ic_out_wh, 1, 0, 1, true, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.ic_inventory_detail._ic_out_shelf, 1, 0, 1, true, false, true);

            this._addTextBox(3, 0, 0, 0, _g.d.ic_inventory_detail._start_unit_code, 1, 0, 1, true, false, true);

            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenOutWh__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenOutWh__textBoxSearch);

            MyLib._myTextBox __getItemstartControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._ic_out_wh);
            __getItemstartControl1.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl1.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemstartControl2 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._ic_out_shelf);
            __getItemstartControl2.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl2.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemstartControl3 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._start_unit_code);
            __getItemstartControl2.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl2.textBox.Leave += new EventHandler(textBox_Leave);

        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenOutWh__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void _icmainScreenOutWh__textBoxSearch(object sender)
        {
            _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                if (!this._searchMasterScreen._name.ToLower().Equals(_searchScreenMasterList[0].ToString().ToLower()))
                {
                    _searchMasterScreen = new MyLib._searchDataFull();
                    this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                    this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                    this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);

                    if (this._searchName.Equals(_g.d.ic_inventory_detail._start_unit_code))
                    {
                        this._searchMasterScreen._dataList._extraWhere = _g.d.ic_unit_use._ic_code + "=\'" + this._itemCode() + "\'";
                    }


                    this._searchMasterScreen._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _icmainScreenOutWh__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory_detail._ic_out_wh))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._ic_out_shelf))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._start_unit_code))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _searchAll(string name, int row)
        {

            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }

        }

        public void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_warehouse._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._ic_out_wh).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_shelf._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._ic_out_shelf).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._start_unit_code).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_inventory_detail._ic_out_wh, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._ic_out_shelf, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._start_unit_code, (DataSet)_getData[2], warning) == false) { }
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
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    public class _icmainScreenGroupStatus : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();
        public _icmainScreenGroupStatus()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_inventory_detail._table;
            this._addTextBox(0, 0, 0, 0, _g.d.ic_inventory_detail._user_group_for_purchase, 1, 0, 1, true, false, true);
            //this._addTextBox(0, 1, 0, 0, "user_group_purchase", 2, 0, 0, false, false, true, false, false);
            this._addTextBox(0, 1, 0, 0, _g.d.ic_inventory_detail._user_group_for_sale, 1, 0, 1, true, false, true);
            //this._addTextBox(1, 1, 0, 0, "user_group_sale", 2, 0, 0, false, false, true, false, false);
            this._addTextBox(1, 0, 0, 0, _g.d.ic_inventory_detail._user_group_for_manage, 1, 0, 1, true, false, true);
            //this._addTextBox(2, 1, 0, 0, "user_group_manage", 2, 0, 0, false, false, true, false, false);
            this._addTextBox(1, 1, 0, 0, _g.d.ic_inventory_detail._user_group_for_warehouse, 1, 0, 1, true, false, true);
            //this._addTextBox(3, 1, 0, 0, "user_group_warehouse", 2, 0, 0, false, false, true, false, false);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenGroupStatus__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenGroupStatus__textBoxSearch);

            MyLib._myTextBox __getItemstartControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._user_group_for_purchase);
            __getItemstartControl1.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl1.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemstartControl2 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._user_group_for_sale);
            __getItemstartControl2.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl2.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemstartControl3 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._user_group_for_manage);
            __getItemstartControl3.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl3.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemstartControl4 = (MyLib._myTextBox)this._getControl(_g.d.ic_inventory_detail._user_group_for_warehouse);
            __getItemstartControl4.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemstartControl4.textBox.Leave += new EventHandler(textBox_Leave);
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenGroupStatus__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;
        }

        void _icmainScreenGroupStatus__textBoxSearch(object sender)
        {
            _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                if (!this._searchMasterScreen._name.ToLower().Equals(_searchScreenMasterList[0].ToString().ToLower()))
                {
                    _searchMasterScreen = new MyLib._searchDataFull();
                    //   this._searchMasterScreen.Text = ((MyLib._myTextBox)sender)._labelName;
                    this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                    this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                    this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterScreen._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
            /* _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                if (!this._searchMasterScreen._name.ToLower().Equals(_searchScreenMasterList[0].ToString().ToLower()))
                {
                    _searchMasterScreen = new MyLib._searchDataFull();
                    this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                    this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                    this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterScreen._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }*/
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _icmainScreenGroupStatus__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory_detail._user_group_for_purchase))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._user_group_for_sale))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._user_group_for_manage))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_inventory_detail._user_group_for_warehouse))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _searchAll(string name, int row)
        {

            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }

        }

        public void _search(Boolean warning)
        {
            try
            {
                string __q1 = "select " + _g.d.erp_user_group._name_1 + " from " + _g.d.erp_user_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group._code);
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._user_group_for_purchase).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._user_group_for_sale).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._user_group_for_manage).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__q1 + "=\'" + this._getDataStr(_g.d.ic_inventory_detail._user_group_for_warehouse).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_inventory_detail._user_group_for_purchase, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._user_group_for_sale, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._user_group_for_manage, (DataSet)_getData[2], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_inventory_detail._user_group_for_warehouse, (DataSet)_getData[3], warning) == false) { }

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
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    public class _icmainScreenStatus : MyLib._myScreen
    {
        public _icmainScreenStatus()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_inventory_detail._table;
            int __row = 0;
            this._addNumberBox(__row, 0, 0, 0, _g.d.ic_inventory_detail._purchase_point, 1, 0, true);
            this._addTextBox(__row, 1, 0, 0, _g.d.ic_inventory_detail._discount, 1, 0, 0, true, false, true);
            __row++;
            this._addNumberBox(__row, 0, 0, 0, _g.d.ic_inventory_detail._minimum_qty, 1, 0, true);
            this._addNumberBox(__row, 1, 0, 0, _g.d.ic_inventory_detail._maximum_qty, 1, 0, true);
            __row++;
            this._addCheckBox(__row, 0, _g.d.ic_inventory_detail._have_point, false, true);
            __row++;
            this._addCheckBox(__row, 0, _g.d.ic_inventory_detail._balance_control, false, true, false);
            this._addCheckBox(__row, 1, _g.d.ic_inventory_detail._accrued_control, false, true);
            __row++;
            this._addCheckBox(__row, 0, _g.d.ic_inventory_detail._lock_cost, false, true);
            __row++;
            this._addCheckBox(__row, 0, _g.d.ic_inventory_detail._is_end, false, true);
            __row++;
            this._addCheckBox(__row, 0, _g.d.ic_inventory_detail._lock_price, false, true);
            this._addCheckBox(__row, 1, _g.d.ic_inventory_detail._lock_discount, false, true);
            __row++;
            this._addCheckBox(__row, 0, _g.d.ic_inventory_detail._is_hold_purchase, false, true);
            this._addCheckBox(__row, 1, _g.d.ic_inventory_detail._is_hold_sale, false, true);
            __row++;
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public class _icmainScreenRemark : MyLib._myScreen
    {
        public _icmainScreenRemark()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_inventory._table;
            this._addTextBox(0, 0, 6, 0, _g.d.ic_inventory._remark, 2, 0, 0, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public class _icmainScreenPromotion : MyLib._myScreen
    {
        // search
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();

        public _icmainScreenPromotion()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_promotion._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_promotion._promote_code, 1, 0, 3, true, false, true);
            this._addDateBox(0, 1, 1, 0, _g.d.ic_promotion._promote_name, 1, true, false);
            this._addDateBox(1, 0, 1, 0, _g.d.ic_promotion._promote_start, 1, true, false);
            this._addDateBox(1, 1, 1, 0, _g.d.ic_promotion._promote_stop, 1, true, false);
            this._addTextBox(2, 0, 1, 0, _g.d.ic_promotion._promote_type, 1, 0, 0, true, false, true);
            this._addTextBox(3, 0, 3, 0, _g.d.ic_promotion._description, 2, 0, 0, true, false, true);
            this._addTextBox(6, 0, 1, 0, _g.d.ic_promotion._ar_type, 1, 0, 1, true, false, true);
            this._addTextBox(6, 1, 1, 0, _g.d.ic_promotion._ar_group, 1, 0, 1, true, false, true);
            this._addTextBox(7, 0, 1, 0, _g.d.ic_promotion._ar_code_1, 1, 0, 1, true, false, true);
            this._addTextBox(7, 1, 1, 0, _g.d.ic_promotion._ar_code_2, 1, 0, 1, true, false, true);
            this._addTextBox(8, 0, 3, 0, _g.d.ic_promotion._remark, 2, 0, 0, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenPromotion__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenPromotion__textBoxSearch);

            MyLib._myTextBox __getItemPromotionControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_promotion._ar_type);
            __getItemPromotionControl1.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemPromotionControl1.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemPromotionControl2 = (MyLib._myTextBox)this._getControl(_g.d.ic_promotion._ar_group);
            __getItemPromotionControl2.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemPromotionControl2.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemPromotionControl3 = (MyLib._myTextBox)this._getControl(_g.d.ic_promotion._ar_code_1);
            __getItemPromotionControl3.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemPromotionControl3.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemPromotionControl4 = (MyLib._myTextBox)this._getControl(_g.d.ic_promotion._ar_code_2);
            __getItemPromotionControl4.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemPromotionControl4.textBox.Leave += new EventHandler(textBox_Leave);
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenPromotion__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;
        }

        void _icmainScreenPromotion__textBoxSearch(object sender)
        {
            _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                if (!this._searchMasterScreen._name.ToLower().Equals(_searchScreenMasterList[0].ToString().ToLower()))
                {
                    _searchMasterScreen = new MyLib._searchDataFull();
                    this._searchMasterScreen.Text = ((MyLib._myTextBox)sender)._labelName;
                    this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                    this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                    this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterScreen._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        private void _searchAll(string name, int row)
        {
            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _icmainScreenPromotion__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_promotion._ar_code_1))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_promotion._ar_code_2))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_promotion._ar_type))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_promotion._ar_group))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        private void _search(bool warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + this._getDataStr(_g.d.ic_promotion._ar_code_1).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + this._getDataStr(_g.d.ic_promotion._ar_code_2).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_type._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_type._code) + "=\'" + this._getDataStr(_g.d.ic_promotion._ar_type).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_group._code) + "=\'" + this._getDataStr(_g.d.ic_promotion._ar_group).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_promotion._ar_code_1, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_promotion._ar_code_2, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_promotion._ar_type, (DataSet)_getData[2], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_promotion._ar_group, (DataSet)_getData[3], warning) == false) { }

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
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }


    }

    public class _icmainPurchasePrice : MyLib._myScreen
    {
        // search
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();

        public _icmainPurchasePrice()
        {
            this._build();
        }

        private void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_purchase_price._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_purchase_price._doc_no, 1, 0, 3, true, false, true);
            this._addDateBox(0, 1, 1, 0, _g.d.ic_purchase_price._doc_date, 1, true, false);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_purchase_price._doc_group, 1, 0, 1, true, false, true);
            this._addTextBox(1, 1, 1, 0, _g.d.ic_purchase_price._ap_code, 1, 0, 1, true, false, true);
            this._addDateBox(2, 0, 1, 0, _g.d.ic_purchase_price._start_date, 1, true, false);
            this._addDateBox(2, 1, 1, 0, _g.d.ic_purchase_price._stop_date, 1, true, false);
            this._addTextBox(3, 0, 1, 0, _g.d.ic_purchase_price._tax_group, 1, 0, 0, true, false, true);
            this._addTextBox(4, 0, 3, 0, _g.d.ic_purchase_price._remark, 2, 0, 0, true, false, true);

            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainPurchasePrice__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainPurchasePrice__textBoxSearch);

            MyLib._myTextBox __getItemPurchaseControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_purchase_price._doc_group);
            __getItemPurchaseControl1.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemPurchaseControl1.textBox.Leave += new EventHandler(textBox_Leave);

            MyLib._myTextBox __getItemPurchaseControl2 = (MyLib._myTextBox)this._getControl(_g.d.ic_purchase_price._ap_code);
            __getItemPurchaseControl2.textBox.Enter += new EventHandler(textBox_Enter);
            __getItemPurchaseControl2.textBox.Leave += new EventHandler(textBox_Leave);
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainPurchasePrice__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchMasterScreen.Visible = false;
        }

        void _icmainPurchasePrice__textBoxSearch(object sender)
        {
            _searchScreenMasterList.Clear();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "");
                if (!this._searchMasterScreen._name.ToLower().Equals(_searchScreenMasterList[0].ToString().ToLower()))
                {
                    _searchMasterScreen = new MyLib._searchDataFull();
                    this._searchMasterScreen.Text = ((MyLib._myTextBox)sender)._labelName;
                    this._searchMasterScreen._name = _searchScreenMasterList[0].ToString();
                    this._searchMasterScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterScreen__searchEnterKeyPress);
                    this._searchMasterScreen._dataList._loadViewFormat(this._searchMasterScreen._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterScreen._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(__getControl, ((MyLib._myTextBox)sender)._labelName, this._searchMasterScreen, false);
            }
            catch (Exception)
            {
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        private void _searchAll(string name, int row)
        {
            string __result = (string)this._searchMasterScreen._dataList._gridData._cellGet(row, 0);
            if (__result.Length > 0)
            {
                this._searchMasterScreen.Visible = false;
                this._setDataStr(this._searchName, __result, "", true);
                this._search(true);
            }
        }

        private void _search(bool warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_group._code) + "=\'" + this._getDataStr(_g.d.ic_purchase_price._doc_group).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_group._name_1 + " from " + _g.d.ap_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.ap_group._code) + "=\'" + this._getDataStr(_g.d.ic_purchase_price._ap_code).ToUpper() + "\'"));

                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_purchase_price._doc_group, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_purchase_price._ap_code, (DataSet)_getData[1], warning) == false) { }


            }
            catch
            {
            }
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _icmainPurchasePrice__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_purchase_price._doc_group))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            if (name.Equals(_g.d.ic_promotion._ar_group))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }

        }

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
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    public class _icmainRelation : MyLib._myScreen
    {
        public _icmainRelation()
        {
            this._build();
        }

        private void _build()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_relation._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_relation._doc_no, 1, 0, 3, true, false, true);
            this._addDateBox(0, 1, 1, 0, _g.d.ic_relation._doc_date, 1, true, false);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_relation._doc_ref, 1, 0, 0, true, false, false);

            this._addNumberBox(2, 0, 1, 0, _g.d.ic_relation._total_qty, 1, 2, true, __formatNumber);
            this._addNumberBox(2, 1, 1, 0, _g.d.ic_relation._total_amount, 1, 2, true, __formatNumber);
            this._addNumberBox(3, 0, 1, 0, _g.d.ic_relation._transection_flag, 1, 2, true, __formatNumber);

            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public class _icmainScreenStkBuild : MyLib._myScreen
    {
        public _icmainScreenStkBuild()
        {
            this._build();
        }

        private void _build()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_stk_build._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_stk_build._doc_no, 1, 0, 3, true, false, true);
            this._addDateBox(0, 1, 1, 0, _g.d.ic_stk_build._doc_date, 1, true, false);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_stk_build._doc_group, 1, 0, 1, true, false, true);
            this._addTextBox(1, 1, 1, 0, _g.d.ic_stk_build._doc_ref, 1, 0, 1, true, false, true);
            this._addTextBox(2, 0, 1, 0, _g.d.ic_stk_build._ic_code, 1, 0, 1, true, false, true);
            this._addTextBox(2, 1, 1, 0, _g.d.ic_stk_build._ic_name, 1, 0, 0, true, false, true);
            this._addTextBox(3, 0, 1, 0, _g.d.ic_stk_build._wh_code, 1, 0, 1, true, false, true);
            this._addTextBox(3, 1, 1, 0, _g.d.ic_stk_build._shelf_code, 1, 0, 1, true, false, true);
            this._addTextBox(4, 0, 1, 0, _g.d.ic_stk_build._unit_code, 1, 0, 1, true, false, true);
            this._addTextBox(4, 1, 1, 0, _g.d.ic_stk_build._wage, 1, 0, 0, true, false, true);
            this._addNumberBox(5, 0, 1, 0, _g.d.ic_stk_build._cost, 1, 2, true, __formatNumber);
            this._addNumberBox(5, 1, 1, 0, _g.d.ic_stk_build._qty, 1, 2, true, __formatNumber);
            this._addTextBox(6, 0, 1, 0, _g.d.ic_stk_build._person_code, 1, 0, 1, true, false, true);
            this._addTextBox(7, 0, 3, 0, _g.d.ic_stk_build._remark, 2, 0, 0, true, false, false);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();

            ////MyLib._myTextBox __getItemStkControl1 = (MyLib._myTextBox)this._getControl(_g.d.ic_stk_build._doc_group);
            ////__getItemStkControl1.textBox.Enter += new EventHandler(textBox_Enter);
            ////__getItemStkControl1.textBox.Leave += new EventHandler(textBox_Leave);
        }

        ////void textBox_Enter(object sender, EventArgs e)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////void textBox_Leave(object sender, EventArgs e)
        ////{
        ////    throw new NotImplementedException();
        ////}
    }

    public class _icmainScreenWeightCost : MyLib._myScreen
    {
        public _icmainScreenWeightCost()
        {
            this._build();
        }

        private void _build()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_weight_cost._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_weight_cost._doc_no, 1, 0, 3, true, false, true);
            this._addDateBox(0, 1, 1, 0, _g.d.ic_weight_cost._doc_date, 1, true, false);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_weight_cost._doc_group, 1, 0, 1, true, false, true);
            this._addTextBox(1, 1, 1, 0, _g.d.ic_weight_cost._doc_ref, 1, 0, 1, true, false, true);
            this._addNumberBox(2, 0, 1, 0, _g.d.ic_weight_cost._weight_by, 1, 2, true, __formatNumber);
            this._addTextBox(2, 0, 1, 0, _g.d.ic_weight_cost._bill_start, 1, 0, 1, true, false, false);
            this._addTextBox(2, 1, 1, 0, _g.d.ic_weight_cost._bill_stop, 1, 0, 1, true, false, false);
            this._addNumberBox(3, 0, 1, 0, _g.d.ic_weight_cost._weight_method, 1, 2, true, __formatNumber);
            this._addNumberBox(3, 1, 1, 0, _g.d.ic_weight_cost._amount, 1, 2, true, __formatNumber);
            this._addTextBox(4, 0, 3, 0, _g.d.ic_weight_cost._description, 2, 0, 1, true, false, true);

            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public class _icmainScreenDateAdjust : MyLib._myScreen
    {
        public _icmainScreenDateAdjust()
        {
            this.__build();
        }

        void __build()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_date_adjust._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_date_adjust._doc_no, 1, 0, 3, true, false, true);
            this._addDateBox(0, 1, 1, 0, _g.d.ic_date_adjust._doc_date, 1, true, false);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_date_adjust._doc_group, 1, 0, 0, true, false, true);
            this._addTextBox(1, 1, 1, 0, _g.d.ic_date_adjust._ap_ar_code, 1, 0, 1, true, false, true);
            this._addTextBox(2, 0, 3, 0, _g.d.ic_date_adjust._remark, 2, 0, 0, true, false, true);
            this._addNumberBox(5, 0, 1, 0, _g.d.ic_date_adjust._transection_flag, 1, 2, true, __formatNumber);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public class _icSerialNumerScreen : MyLib._myScreen
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _icSerialNumerScreen()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_serial._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_serial._serial_number, 1, 1, 0, true, false, false);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_serial._ic_code, 1, 0, 0, true, false, false);
            this._addTextBox(2, 0, 1, 0, _g.d.ic_serial._ic_unit, 1, 0, 0, true, false, false);
            this._addTextBox(3, 0, 1, 0, _g.d.ic_serial._wh_code, 1, 0, 0, true, false, false);
            this._addTextBox(4, 0, 1, 0, _g.d.ic_serial._shelf_code, 1, 0, 0, true, false, false);
            this._addTextBox(5, 0, 1, 0, _g.d.ic_serial._status, 1, 0, 0, true, false, false);
            this._addDateBox(6, 0, 1, 0, _g.d.ic_serial._void_date, 1, true, true);
            //
            this.ResumeLayout();
        }
    }

    public class _icMainFastScreen : MyLib._myScreen
    {
        public _icMainFastScreen()
        {
            this._table_name = _g.d.ic_inventory._table;
            this._maxColumn = 4;

            int __row = 0;
            this._addTextBox(__row, 0, 1, 0, _g.d.ic_inventory._code, 2, 1, 1, true, false, false);
            this._addComboBox(__row, 2, _g.d.ic_inventory._tax_type, true, new string[] { "normal_vat", "exc_vat" }, true);
            this._addComboBox(__row, 3, _g.d.ic_inventory._item_type, true, new string[] { "ic_normal", "ic_service", "ic_rent", "ic_set", "ic_consignment", "ic_color", "ic_color_mixed" }, true);
            __row++;

            this._addTextBox(__row, 0, 1, 0, _g.d.ic_inventory._name_1, 4, 0, 0, true, false, false);
            __row++;

            this._addComboBox(__row, 0, _g.d.ic_inventory._unit_type, true, new string[] { "single_unit", "many_unit" }, true);
            this._addComboBox(__row, 1, _g.d.ic_inventory._cost_type, true, new string[] { "average_cost", "fifo_cost" }, true);
            this._addTextBox(__row, 2, 0, 0, _g.d.ic_inventory._unit_cost, 1, 0, 1, true, false, false);
            this._addTextBox(__row, 3, 0, 0, _g.d.ic_inventory._unit_standard, 1, 0, 1, true, false, false);


        }
    }
}
