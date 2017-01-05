using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransExtraControl : Form
    {
        private string _titleLabel;        
        MyLib._searchDataFull _searchPattern = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchColor = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchSize = new MyLib._searchDataFull();
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        public bool _manageData = true;
        public _icTransExtraControl(string captionLabel)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _titleLabel = captionLabel.ToString();       
            this.Load += new EventHandler(_ictransExtraControl_Load);
        }

        void _ictransExtraControl_Load(object sender, EventArgs e)
        {
            this.Text = _titleLabel.ToString();      
            _searchPattern._name = _g.g._search_master_ic_pattern;
            _searchPattern._dataList._loadViewFormat(_searchPattern._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchPattern._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchPattern._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchPattern__searchEnterKeyPress);

            _searchColor._name = _g.g._search_master_ic_color;
            _searchColor._dataList._loadViewFormat(_searchColor._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchColor._dataList._gridData._mouseClick +=new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchColor._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchColor__searchEnterKeyPress);

            _searchSize._name = _g.g._search_master_ic_size;
            _searchSize._dataList._loadViewFormat(_searchSize._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchSize._dataList._gridData._mouseClick +=new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchSize._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchSize__searchEnterKeyPress);

            if (_manageData == false)
            {
                this._myToolBar.Focus();
            }
            this._ictransExtraControlScreenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_ictransExtraControlScreenTop__textBoxChanged);
            this._ictransExtraControlScreenTop._textBoxSearch += new MyLib.TextBoxSearchHandler(_ictransExtraControlScreenTop__textBoxSearch);
        }

        void _ictransExtraControlScreenTop__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.Equals(_g.d.ic_extra_detail._ic_pattern))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchPattern, false);
            }
            if (name.Equals(_g.d.ic_extra_detail._ic_color))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchColor, false);
            }
            if (name.Equals(_g.d.ic_extra_detail._ic_size))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchSize, false);
            }
        }

        void _ictransExtraControlScreenTop__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_extra_detail._ic_pattern) || name.Equals(_g.d.ic_extra_detail._ic_color) || name.Equals(_g.d.ic_extra_detail._ic_size))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                _search(true);
            }
        }

        void _searchSize__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchColor__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchPattern__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
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
            if (name.Equals(_g.g._search_master_ic_pattern))
            {
                string result = (string)_searchPattern._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchPattern.Visible = false;
                    this._ictransExtraControlScreenTop._setDataStr(_searchName, result, "", false);
                    _search(true);
                }
            }
            if (name.Equals(_g.g._search_master_ic_color))
            {
                string result = (string)_searchColor._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchColor.Visible = false;
                    this._ictransExtraControlScreenTop._setDataStr(_searchName, result, "", false);
                    _search(true);
                }
            }
            if (name.Equals(_g.g._search_master_ic_size))
            {
                string result = (string)_searchSize._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchSize.Visible = false;
                    this._ictransExtraControlScreenTop._setDataStr(_searchName, result, "", false);
                    _search(true);
                }
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_master_ic_pattern) == 0)
            {
                string result = (string)_searchPattern._dataList._gridData._cellGet(e._row, _g.d.ic_pattern._table + "." + _g.d.ic_pattern._code);            
                if (result.Length > 0)
                {
                    _searchPattern.Close();
                    this._ictransExtraControlScreenTop._setDataStr(_g.d.ic_extra_detail._ic_pattern, result.Trim(), "", false);
                    //_search(true);
                }
            }
            if (name.CompareTo(_g.g._search_master_ic_color) == 0)
            {
                string result = (string)_searchColor._dataList._gridData._cellGet(e._row, _g.d.ic_color._table + "." + _g.d.ic_color._code);
                if (result.Length > 0)
                {
                    _searchColor.Close();
                    this._ictransExtraControlScreenTop._setDataStr(_g.d.ic_extra_detail._ic_color, result.Trim(), "", false);
                }
            }
            if (name.CompareTo(_g.g._search_master_ic_size) == 0)
            {
                string result = (string)_searchSize._dataList._gridData._cellGet(e._row, _g.d.ic_size._table + "." + _g.d.ic_size._code);
                if (result.Length > 0)
                {
                    _searchSize.Close();
                    this._ictransExtraControlScreenTop._setDataStr(_g.d.ic_extra_detail._ic_size, result.Trim(), "", false);
                }
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_pattern._name_1 + " from " + _g.d.ic_pattern._table + " where " + _g.d.ic_pattern._code + "=\'" + this._ictransExtraControlScreenTop._getDataStr(_g.d.ic_extra_detail._ic_pattern) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_color._name_1 + " from " + _g.d.ic_color._table + " where " + _g.d.ic_color._code + "=\'" + this._ictransExtraControlScreenTop._getDataStr(_g.d.ic_extra_detail._ic_color) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_size._name_1 + " from " + _g.d.ic_size._table + " where " + _g.d.ic_size._code + "=\'" + this._ictransExtraControlScreenTop._getDataStr(_g.d.ic_extra_detail._ic_size) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ic_extra_detail._ic_pattern, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_extra_detail._ic_color, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ic_extra_detail._ic_size, (DataSet)_getData[2], warning) == false) { } 
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
                string __getDataStr = this._ictransExtraControlScreenTop._getDataStr(fieldName);
                this._ictransExtraControlScreenTop._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._ictransExtraControlScreenTop._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + this._ictransExtraControlScreenTop._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._ictransExtraControlScreenTop._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }

    public partial class _ictransExtraControlScreenTop : MyLib._myScreen
    {
        public _ictransExtraControlScreenTop()
        {
            this._maxColumn = 1;
            this._table_name = _g.d.ic_extra_detail._table;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_extra_detail._ic_pattern, 1, 1, 1, true, false, true, true);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_extra_detail._ic_color, 1, 1, 1, true, false, true, true);
            this._addTextBox(2, 0, 1, 0, _g.d.ic_extra_detail._ic_size, 1, 1, 1, true, false, true, true);
            this.Invalidate();
        }        
    }
}
