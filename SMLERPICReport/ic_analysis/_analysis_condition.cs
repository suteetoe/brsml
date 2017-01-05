using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPICReport.ic_analysis
{
    public partial class _analysis_condition : MyLib._myForm
    {
        string __page = "";
        public Boolean __check_submit = false;
        public string __where = "";
        public DataTable __grid_where;
        private int __click_check = 0;

        public _analysis_condition(string screenName)
        {
            InitializeComponent();
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);

            //this.__from_name(__page);
            //this.__page = __page;
            this.Text = screenName;
            _condition_analysis_search1._maxColumn = 1;
            _condition_analysis_search1._init();
            _screen_grid_analysis1._setFromToColumn(_g.d.resource_report._from_item_code, _g.d.resource_report._to_item_code);
            //
            _screen_grid_analysis2._searchScreenName = _g.g._search_screen_ar;
            _screen_grid_analysis2._setFromToColumn(_g.d.resource_report._from_customer_code, _g.d.resource_report._to_customer_code);
            this._grouper1.SizeChanged += new EventHandler(_grouper1_SizeChanged);
        }

        void _grouper1_SizeChanged(object sender, EventArgs e)
        {
            if (_screen_grid_analysis2.Visible)
            {
                _screen_grid_analysis2.Height = this._grouper1.Height / 2;
            }
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this.__check_submit = false;
            this.Close();
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            this._process_click();
            this.Close();
        }

        public void _process_click()
        {
            if (this.__click_check == 0)
            {
                this.__grid_where = null;
            }
            else
            {
                // this.__grid_where = _screen_grid_so1._getCondition();
            }
            // this.__where = "" + this._condition_so_search3._createQueryForDatabase()[1].ToString();
            this.__check_submit = true;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Keys.Escape == keyData)
            {
                this.__check_submit = false;
                this.Close();
                return true;
            }
            if (Keys.F11 == keyData)
            {
                this._process_click();
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public partial class _condition_analysis_search : MyLib._myScreen
    {
        MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        MyLib._searchDataFull _search_item_code;

        string _where = "";


        public _condition_analysis_search()
        {
            this._table_name = _g.d.resource_report._table;
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_condition_so_search__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_so_search__textBoxChanged);
        }


        public void _init()
        {
            this._maxColumn = 2;

            DateTime __today = DateTime.Now;
            this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
            this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
            this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
            this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));
            //
            this._search_item_code = new MyLib._searchDataFull();
            this._search_item_code.WindowState = FormWindowState.Maximized;

            if (this._search_item_code != null)
            {
                this._search_item_code._name = _g.g._search_screen_ic_inventory;
                this._search_item_code._dataList._loadViewFormat(this._search_item_code._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_item_code._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_item_code._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_so__searchEnterKeyPress);
            }
            //}
        }

        //-------------------------------------Search-------------------------------------------------------
        void _search_so__searchEnterKeyPress(MyLib._myGrid sender, int row)
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

        private void _searchAll(string name, int row)
        {
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            string __result = "";

            if (name.Equals(_g.g._search_screen_ic_inventory))
            {
                __search = _search_item_code;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }


            //-------------------------------
            if (__result.Length > 0)
            {
                __search.Visible = false;
                this._setDataStr(_searchName, __result, "", false);
                _search(true);
            }

        }
        void _condition_so_search__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_resource._ic_name))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                //   _search(true);
            }
            if (name.Equals(_g.d.ic_resource._ic_normal))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                //   _search(true);
            }
        }

        void _condition_so_search__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _whereQuery = "";
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            if (name.Equals(_g.d.resource_report._from_item_code) || name.Equals(_g.d.resource_report._to_item_code))
            {
                __search = _search_item_code;
            }
            //------------------------------------
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            _searchName = name;
            _searchTextBox = __getControl.textBox;
            __search.WindowState = FormWindowState.Maximized;
            __search.ShowDialog();
            //MyLib._myGlobal._startSearchBox(__getControl, label_name, __search, false, true, "");
        }
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, e._row);
        }

        private void _search(bool warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + this._getDataStr(_g.d.ap_supplier._code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + this._getDataStr(_g.d.ap_supplier._code) + "\'"));

                __myquery.Append("</node>");
                ArrayList _getData = _myFramework._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.ap_supplier._code, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.ap_supplier._code, (DataSet)_getData[1], warning);
            }
            catch
            {
            }
        }
        private bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = this._getDataStr(fieldName);
                if (fieldName.Equals(_g.d.ic_resource._ic_name) || fieldName.Equals(_g.d.ic_resource._ic_normal))
                {
                    this._setDataStr(fieldName, getDataStr);
                    //this._setDataStr(fieldName, getDataStr, getData, true);
                    //  this._setDataStr(_g.d.ic_inventory._name_1, getData);
                }
                else
                {
                    this._setDataStr(fieldName, getDataStr, getData, true);
                }
            }
            else
            {
                this._setDataStr(fieldName, "");
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
    public partial class _screen_grid_analysis : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        public string _searchScreenName = _g.g._search_screen_ic_inventory;

        public _screen_grid_analysis()
        {
            this._clickSearchButton += new MyLib.SearchEventHandler(_screen_grid_so__clickSearchButton);
        }

        void _screen_grid_so__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string __searchName = this._cellGet(this._selectRow, this._selectColumn).ToString();
            string __search_text_new = this._searchScreenName;
            if (!this._search_data_full._name.Equals(__search_text_new.ToLower()))
            {
                this._search_data_full = new MyLib._searchDataFull();
                this._search_data_full._name = __search_text_new;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                //this._search_data_full._dataList._refreshData();
                //this._search_data_full._dataList._loadViewData(0);
                this._search_data_full.WindowState = FormWindowState.Maximized;
            }
            this._search_data_full.ShowDialog();
            //MyLib._myGlobal._startSearchBox(this._inputTextBox, "ค้นหารหัสสินค้า", this._search_data_full, true);
        }

        public void _setFromToColumn(string __from_column_name, string __to_column_name)
        {
            this._clear();
            this._addColumn(_g.d.resource_report._table + "." + __from_column_name, 1, 1, 50, true, false, false, true);
            this._addColumn(_g.d.resource_report._table + "." + __to_column_name, 1, 1, 50, true, false, false, true);
            this._width_by_persent = true;
            this.AllowDrop = true;
            //this._isEdit = false;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            //
            this._search_data_full.Close();
            this._cellUpdate(this._selectRow, this._selectColumn, e._text, false);
        }

        public DataTable _getCondition()
        {
            if (this._rowCount(0) == 0) return null;
            DataTable __dataTable = new DataTable("FromTo");
            __dataTable.Columns.Add("from");
            __dataTable.Columns.Add("to");
            for (int __row = 0; __row < this._rowCount(0); __row++)
            {
                DataRow __dataRow = __dataTable.NewRow();
                __dataRow[0] = this._cellGet(__row, 0).ToString();
                __dataRow[1] = this._cellGet(__row, 1).ToString();
                __dataTable.Rows.Add(__dataRow);
            }
            return __dataTable;
        }

    }

}
