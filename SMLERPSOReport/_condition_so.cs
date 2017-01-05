using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPSOReport
{
    public partial class _condition_so : MyLib._myForm
    {
        string __page = "";
        public Boolean __check_submit = false;
        public string __where = "";
        public DataTable __grid_where;
        private int __click_check = 0;

        public _condition_so(SMLERPReportTool._reportEnum mode, string _where)
        {
            InitializeComponent();

            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);

            //this.__from_name(__page);
            //this.__page = __page;
            _condition_so_search3._maxColumn = 1;
            _condition_so_search3._init(mode, _where);
            this._screen_grid_so1._mode = mode;
            if (SMLERPReportTool._global._reportType(mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้)
            {
                _screen_grid_so1._setFromToColumn(_g.d.resource_report._from_ar, _g.d.resource_report._to_ar);
            }
            else if (mode == SMLERPReportTool._reportEnum.สินค้า_sell_by_sale)
            {
                _screen_grid_so1._setFromToColumn(_g.d.resource_report._from_sale_person, _g.d.resource_report._to_sale_person);
            }
            else
            {
                _screen_grid_so1._setFromToColumn(_g.d.resource_report._from_ap, _g.d.resource_report._to_ap);
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
                this.__grid_where = _screen_grid_so1._getCondition();
            }
            this.__where = "" + this._condition_so_search3._createQueryForDatabase()[1].ToString();
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

    public partial class _condition_so_search : MyLib._myScreen
    {
        MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        MyLib._searchDataFull _search_doc_no;
        MyLib._searchDataFull _search_cust;
        MyLib._searchDataFull _search_item;
        string _where = "";
        public SMLERPReportTool._reportEnum _mode;

        public _condition_so_search()
        {
            this._table_name = _g.d.resource_report._table;
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_condition_so_search__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_so_search__textBoxChanged);
        }

        public void _init(SMLERPReportTool._reportEnum mode,string _where)
        {
            this._mode = mode;
            this._maxColumn = 2;
            this._where = _where;
            DateTime __fromDate = new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1);
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.ซื้อ_รายงานซื้อสินค้าแยกตามสินค้า:
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                    this._addCheckBox(2, 0, _g.d.resource_report._display_detail, true, false, true);
                    this._search_item = new MyLib._searchDataFull();
                    break;
                case SMLERPReportTool._reportEnum.ขาย_รายงานขายสินค้าแยกตามสินค้า:
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                    this._addCheckBox(2, 0, _g.d.resource_report._display_detail, true, false, true);
                    this._search_item = new MyLib._searchDataFull();
                    break;
                case SMLERPReportTool._reportEnum.สินค้า_sell_by_sale :
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);                    
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, true, false, true);
                    break;
                default:
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);
                    if (mode == SMLERPReportTool._reportEnum.ขาย_เสนอราคา)
                    {
                        this._addCheckBox(3, 0, _g.d.resource_report._display_detail, true, false, true);
                        this._addCheckBox(4, 0, _g.d.resource_report._show_cancel_document, true, false, true);
                    }
                    else if (mode == SMLERPReportTool._reportEnum.ขาย_รายงานใบเสนอซื้อสินค้า)
                    {
                        this._addCheckBox(3, 0, _g.d.resource_report._show_cancel_document, true, false, true);
                    }
                    this._search_doc_no = new MyLib._searchDataFull();
                    this._search_cust = new MyLib._searchDataFull();
                    break;
            }
            this._setDataDate(_g.d.resource_report._from_date, __fromDate);
            this._setDataDate(_g.d.resource_report._to_date, MyLib._myGlobal._workingDate);

            // ค้นหาเอกสาร

            if (this._search_doc_no != null)
            {
                this._search_doc_no._name = _g.g._search_screen_ic_trans;
                this._search_doc_no._dataList._loadViewFormat(this._search_doc_no._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_doc_no._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_doc_no._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_so__searchEnterKeyPress);
            }
            if (this._search_cust != null)
            {
                this._search_cust._name = (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้) ? _g.g._search_screen_ar : _g.g._search_screen_ap;
                this._search_cust._dataList._loadViewFormat(this._search_cust._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_cust._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_cust._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_so__searchEnterKeyPress);
            }
            if (this._search_item != null)
            {
                this._search_item._name = _g.g._search_screen_ic_inventory;
                this._search_item._dataList._loadViewFormat(this._search_item._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_item._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_item._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_so__searchEnterKeyPress);
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

            if (name.Equals(_g.g._search_screen_ic_trans))
            {
                __search = _search_doc_no;
                __result = (string)__search._dataList._gridData._cellGet(row, 1);
            }
            if (name.Equals(_g.g._search_screen_ar))
            {
                __search = _search_cust;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_ap))
            {
                __search = _search_cust;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_ic_inventory))
            {
                __search = _search_item;
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
            if (name.Equals(_g.d.resource_report._from_docno) || name.Equals(_g.d.resource_report._to_docno))
            {
                __search = _search_doc_no;
                _whereQuery = this._where;
            }
            if (name.Equals(_g.d.resource_report._from_ar) || name.Equals(_g.d.resource_report._to_ar))
            {
                __search = _search_cust;
                _whereQuery = "";
            }
            if (name.Equals(_g.d.resource_report._from_item_code) || name.Equals(_g.d.resource_report._to_item_code))
            {
                __search = _search_item;
                _whereQuery = "";
            }
            //------------------------------------
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            _searchName = name;
            _searchTextBox = __getControl.textBox;
            MyLib._myGlobal._startSearchBox(__getControl, label_name, __search, false, true, _whereQuery);
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

    public partial class _screen_grid_so : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        public SMLERPReportTool._reportEnum _mode;

        public _screen_grid_so()
        {
            this._clickSearchButton += new MyLib.SearchEventHandler(_screen_grid_so__clickSearchButton);
        }

        void _screen_grid_so__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string _searchName = this._cellGet(this._selectRow, this._selectColumn).ToString();
            string _search_text_new = (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้) ? _g.g._search_screen_ar : _g.g._search_screen_ap;

            if (this._mode == SMLERPReportTool._reportEnum.สินค้า_sell_by_sale)
            {
                _search_text_new = _g.g._search_screen_erp_user;
            }

            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                this._search_data_full = new MyLib._searchDataFull();
                this._search_data_full._name = _search_text_new;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._refreshData();
                this._search_data_full._dataList._loadViewData(0);
            }
            MyLib._myGlobal._startSearchBox(this._inputTextBox, "Search", this._search_data_full, true);
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
            if (name.CompareTo(_g.g._search_screen_ar) == 0 || name.CompareTo(_g.g._search_screen_ap) == 0 || name.CompareTo(_g.g._search_screen_erp_user) == 0)
            {
                this._search_data_full.Close();
                this._cellUpdate(this._selectRow, this._selectColumn, e._text, false);
            }
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



