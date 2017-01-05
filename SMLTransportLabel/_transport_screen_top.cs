using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace SMLTransportLabel
{
    public class _transport_screen_top : MyLib._myScreen
    {
        string _searchName = "";
        TextBox _searchTextBox;
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        /*
        private MyLib._searchDataFull _searchData = null; // new MyLib._searchDataFull();

        private string _searchName = "";
        private TextBox _searchTextBox;
        private ArrayList _search_data_full_buffer = new ArrayList();
        private ArrayList _search_data_full_buffer_name = new ArrayList();
        private int _search_data_full_buffer_addr = -1;
        private MyLib._searchDataFull _search_data_full_pointer;*/
        int _screenmode = 0;
        public int _mode
        {
            get { return this._screenmode; }
            set
            {
                this._screenmode = value;
                this._build();
            }
        }

        void _build()
        {
            this._maxColumn = 2;
            int __row = 0;
            this._table_name = _g.d.ap_ar_transport_label._table;

            this._addTextBox(__row++, 0, 0, 0, _g.d.ap_ar_transport_label._cust_code, 2, 0, 1, true, false, false, true, true, ((this._screenmode == 0) ? _g.d.ap_ar_transport_label._ap_name : _g.d.ap_ar_transport_label._ar_name));

            this._addTextBox(__row++, 0, 0, 0, _g.d.ap_ar_transport_label._name_1, 2, 0, 0, true, false, false);
            this._addTextBox(__row, 0, 3, 0, _g.d.ap_ar_transport_label._address, 2, 0, 0, true, false, false);
            __row += 3;

            this._addTextBox(__row++, 0, 0, 0, _g.d.ap_ar_transport_label._telephone, 1, 0, 0, true, false, false);
            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._fax, 1, 0);
            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._email, 1, 0);
            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._ship_code, 1, 0);
            this._addTextBox(__row++, 0, 0, 1, _g.d.ap_ar_transport_label._logistic_area, 1, 10, 1, true, false);
            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._latitude, 1, 0);
            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._longitude, 1, 0);

            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_transport_screen_top__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_transport_screen_top__textBoxChanged);
        }

        void _transport_screen_top__textBoxChanged(object sender, string name)
        {
            //throw new NotImplementedException();

            MyLib._myFrameWork __myFremeWork = new MyLib._myFrameWork();
            if (name.Equals(_g.d.ap_ar_transport_label._cust_code))
            {
                string __custCode = this._getDataStr(_g.d.ap_ar_transport_label._cust_code);
                if (this._mode == 0)
                {
                    DataSet __result = __myFremeWork._queryShort("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + __custCode + "\'");
                    if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                        this._setDataStr(_g.d.ap_ar_transport_label._cust_code, __custCode, __result.Tables[0].Rows[0][_g.d.ap_supplier._name_1].ToString(), true);

                }
                else if (this._mode == 1)
                {
                    DataSet __result = __myFremeWork._queryShort("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + __custCode + "\'");
                    if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                        this._setDataStr(_g.d.ap_ar_transport_label._cust_code, __custCode, __result.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString(), true);
                }
            }
        }

        MyLib._myTextBox _searchControl = null;
        void _transport_screen_top__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            //กดปุ่มค้นหาเลือกรายการกดEnter
            //if (this._searchName.Equals(_g.d.ar_customer._tambon.ToLower()))
            //{
            string _search_text_new = _search_screen_neme(this._searchName);
            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                this._search_data_full = new MyLib._searchDataFull();
                this._search_data_full._name = _search_text_new;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick += _gridData__mouseClick;
                this._search_data_full._searchEnterKeyPress += _search_data_full__searchEnterKeyPress;
                this._search_data_full._dataList._refreshData();
            }
            if (this._searchName.Equals(_g.d.ap_ar_transport_label._tambon.ToLower()))
            {
                string _where = " " + _g.d.erp_tambon._province + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._province) + "\' and " + _g.d.erp_tambon._amper + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._amper) + "\'";
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
            }
            else
                if (this._searchName.Equals(_g.d.ap_ar_transport_label._amper.ToLower()))
            {
                string _where = " " + _g.d.erp_amper._province + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._province) + "\'";
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false);
            }

            /*
            _searchControl = (MyLib._myTextBox)sender;
            _searchName = _searchControl._name.ToLower();

            if (_searchName.Equals(_g.d.ap_ar_transport_label._cust_code) && this._mode == 0)
            {
                //ค้นหาเจ้าหนี้
                if (_searchData == null)
                {
                    this._searchData = new MyLib._searchDataFull();
                    this._searchData._dataList._loadViewFormat(_g.g._search_screen_ap, MyLib._myGlobal._userSearchScreenGroup, false);
                }
            }
            else if (_searchName.Equals(_g.d.ap_ar_transport_label._cust_code) && this._mode == 1)
            {
                // ค้นหาลูกหนี้
                if (_searchData == null)
                {
                    this._searchData = new MyLib._searchDataFull();
                    this._searchData._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, false);
                }
            }

            this._searchData._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._searchData._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._searchData._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_searchData__searchEnterKeyPress);
            this._searchData._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchData__searchEnterKeyPress);

            MyLib._myGlobal._startSearchBox(_searchControl, _searchControl._labelName, this._searchData, false, true, "");

            */
        }

        string _search_screen_neme(string _name)
        {
            switch (_name)
            {
                case "cust_code":
                    {
                        if (this._mode == 0)
                            return _g.g._search_screen_ap;
                        return _g.g._search_screen_ar;
                    }
                case "logistic_area": return _g.g._search_master_ar_logistic_area;

            }
            return "";
        }


        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            /*if (this._mode == 0)
            {
                string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();
                this._search_data_full.Close();
                this._setDataStr(_g.d.ap_ar_transport_label._cust_code, __code);
            }
            else if (this._mode == 1)
            {
                string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();
                this._search_data_full.Close();
                this._setDataStr(_g.d.ap_ar_transport_label._cust_code, __code);
            }*/
            string __getColumnName = "";
            if (this._searchName.Equals("cust_code"))
            {
                if (this._mode == 0)
                {
                    __getColumnName = _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code;
                }
                else if (this._mode == 1)
                {
                    __getColumnName = _g.d.ar_customer._table + "." + _g.d.ar_customer._code;
                }
            }
            else if (this._searchName.Equals("logistic_area"))
            {
                __getColumnName = _g.d.ar_logistic_area._table + "." + _g.d.ar_logistic_area._code;
            }

            string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, __getColumnName).ToString();
            this._search_data_full.Close();
            this._setDataStr(_g.d.ap_ar_transport_label._cust_code, __code);

        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __getColumnName = "";
            if (this._searchName.Equals("cust_code"))
            {
                if (this._mode == 0)
                {
                    __getColumnName = _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code;
                    //string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code).ToString();
                    //this._search_data_full.Close();
                    //this._setDataStr(_g.d.ap_ar_transport_label._cust_code, __code);
                }
                else if (this._mode == 1)
                {
                    __getColumnName = _g.d.ar_customer._table + "." + _g.d.ar_customer._code;
                    //string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                    //this._search_data_full.Close();
                    //this._setDataStr(_g.d.ap_ar_transport_label._cust_code, __code);
                }
            }
            else if (this._searchName.Equals("logistic_area"))
            {
                __getColumnName = _g.d.ar_logistic_area._table + "." + _g.d.ar_logistic_area._code;
                //string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                //this._search_data_full.Close();
                //this._setDataStr(_g.d.ap_ar_transport_label._cust_code, __code);
            }

            string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, __getColumnName).ToString();
            this._search_data_full.Close();
            this._setDataStr(_searchName, __code);

        }

        public _transport_screen_top()
        {

        }
    }

    public class _transport_label_grid : MyLib._myGrid
    {
        public _transport_label_grid()
        {
            this._table_name = _g.d.ap_ar_transport_label._table;
            //this._addRowEnabled = false;


            this._addColumn(_g.d.ap_ar_transport_label._cust_code, 1, 15, 15, false, false, true);
            this._addColumn(_g.d.ap_ar_transport_label._name_1, 1, 0, 20, true, true);
            this._addColumn(_g.d.ap_ar_transport_label._receivename, 1, 0, 20, false, false, true);
            this._addColumn(_g.d.ap_ar_transport_label._address, 1, 255, 30, false, false, true);
            this._addColumn(_g.d.ap_ar_transport_label._telephone, 1, 15, 20, false, false, true);
            this._addColumn(_g.d.ap_ar_transport_label._label_count, 2, 0, 15);


            this._addColumn(_g.d.ap_ar_transport_label._fax, 1, 15, 0, false, true, true);
            this._addColumn(_g.d.ap_ar_transport_label._email, 1, 15, 0, false, true, true);

        }
    }

    public class _transport_screen_detail : MyLib._myScreen
    {
        public _transport_screen_detail()
        {
            this._maxColumn = 2;
            int __row = 0;
            this._table_name = _g.d.ap_ar_transport_label._table;

            this._addTextBox(__row, 0, 0, _g.d.ap_ar_transport_label._home_address, 1, 0);
            this._addTextBox(__row++, 1, 0, _g.d.ap_ar_transport_label._room_no, 1, 0);
            this._addTextBox(__row, 0, 0, _g.d.ap_ar_transport_label._building, 1, 0);
            this._addTextBox(__row++, 1, 0, _g.d.ap_ar_transport_label._floor, 1, 0);
            this._addTextBox(__row, 0, 0, _g.d.ap_ar_transport_label._home_name, 1, 0);
            this._addTextBox(__row++, 1, 0, _g.d.ap_ar_transport_label._moo, 1, 0);
            this._addTextBox(__row, 0, 0, _g.d.ap_ar_transport_label._soi, 1, 0);
            this._addTextBox(__row++, 1, 0, _g.d.ap_ar_transport_label._road, 1, 0);

            this._addTextBox(__row, 0, 0, 0, _g.d.ap_ar_transport_label._province, 1, 0, 1, true, false, true);
            this._addTextBox(__row++, 1, 0, 0, _g.d.ap_ar_transport_label._amper, 1, 0, 1, true, false, true);

            this._addTextBox(__row, 0, 0, 0, _g.d.ap_ar_transport_label._tambon, 1, 0, 1, true, false, true);
            this._addTextBox(__row++, 1, 0, _g.d.ap_ar_transport_label._zip_code, 1, 0);
            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._country, 1, 0);

            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._home_phone, 1, 0);

            this._addTextBox(__row, 0, 0, _g.d.ap_ar_transport_label._mobile1, 1, 0);
            this._addTextBox(__row++, 1, 0, _g.d.ap_ar_transport_label._mobile2, 1, 0);

            this._textBoxSearch += _transport_screen_detail__textBoxSearch;
            this._textBoxChanged += _transport_screen_detail__textBoxChanged;
            this._addTextBox(__row++, 0, 0, 0, _g.d.ap_ar_transport_label._transport_type, 1, 0, 1, true, false, true);
            this._addTextBox(__row, 0, 0, _g.d.ap_ar_transport_label._receive_day, 1, 0);
            this._addTextBox(__row++, 1, 0, _g.d.ap_ar_transport_label._time_period, 1, 0);

            this._addComboBox(__row, 0, _g.d.ap_ar_transport_label._branch_type, 1, true, _g.g._ap_ar_branch_type, true, _g.d.ap_ar_transport_label._branch_type, true, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.ap_ar_transport_label._branch_code, 1, 0, 0, true, false, true, true);

            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._tax_id, 1, 0);

            this._addTextBox(__row, 0, 3, 0, _g.d.ap_ar_transport_label._remark_1, 2, 0, 0, true, false, true);
            __row += 3;
            this._addTextBox(__row, 0, 3, 0, _g.d.ap_ar_transport_label._remark_2, 2, 0, 0, true, false, true);
            __row += 3;

            /*
            this._addTextBox(__row, 0, 0, 0, _g.d.ap_ar_transport_label._home_address, 1, 0, 0, true, false, false);
            this._addTextBox(__row++, 0, 0, 0, _g.d.ap_ar_transport_label._room_no, 1, 0, 0, true, false, false);
            this._addTextBox(__row++, 0, 0, 0, _g.d.ap_ar_transport_label._building, 1, 0, 0, true, false, false);
            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._floor, 1, 0);

            this._addTextBox(__row++, 0, 0, _g.d.ap_ar_transport_label._email, 1, 0);
             * */
            MyLib._myTextBox __getProvoinceTextbox = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_transport_label._province);
            if (__getProvoinceTextbox != null)
            {
                __getProvoinceTextbox.textBox.Enter += textBox_Enter;
                __getProvoinceTextbox.textBox.Leave += textBox_Leave;
            }

            MyLib._myTextBox __getAmperTextbox = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_transport_label._amper);
            if (__getAmperTextbox != null)
            {
                __getAmperTextbox.textBox.Enter += textBox_Enter;
                __getAmperTextbox.textBox.Leave += textBox_Leave;
            }

            MyLib._myTextBox __getTambonTextbox = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_transport_label._tambon);
            if (__getTambonTextbox != null)
            {
                __getTambonTextbox.textBox.Enter += textBox_Enter;
                __getTambonTextbox.textBox.Leave += textBox_Leave;
            }

        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._search_data_full.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _transport_screen_detail__textBoxSearch(_getControl);
            _getControl.textBox.Focus();

        }

        string _searchName = "";
        TextBox _searchTextBox;
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();

        void _transport_screen_detail__textBoxChanged(object sender, string name)
        {

        }

        string _search_screen_neme(string _name)
        {
            switch (_name)
            {
                case "tambon": return _g.g._screen_erp_tambon;
                case "amper": return _g.g._screen_erp_amper;
                case "province": return _g.g._screen_erp_province;
                case "transport_type": return _g.g._search_screen_erp_transport;

            }
            return "";
        }

        void _transport_screen_detail__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            //กดปุ่มค้นหาเลือกรายการกดEnter
            //if (this._searchName.Equals(_g.d.ar_customer._tambon.ToLower()))
            //{
            string _search_text_new = _search_screen_neme(this._searchName);
            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                this._search_data_full = new MyLib._searchDataFull();
                this._search_data_full._name = _search_text_new;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick += _gridData__mouseClick;
                this._search_data_full._searchEnterKeyPress += _search_data_full__searchEnterKeyPress;
                this._search_data_full._dataList._refreshData();
            }
            if (this._searchName.Equals(_g.d.ap_ar_transport_label._tambon.ToLower()))
            {
                string _where = " " + _g.d.erp_tambon._province + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._province) + "\' and " + _g.d.erp_tambon._amper + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._amper) + "\'";
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
            }
            else
                if (this._searchName.Equals(_g.d.ap_ar_transport_label._amper.ToLower()))
            {
                string _where = " " + _g.d.erp_amper._province + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._province) + "\'";
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false);
            }

        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
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
            if (name.Length > 0)
            {
                string result = (string)this._search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length != 0)
                {
                    this._search_data_full.Visible = false;
                    this._setDataStr(_searchName, result, "", false);
                    this._search(true);
                }
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._code + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._province) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._province + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._province) + "\' and " + _g.d.erp_amper._code + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._amper) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_tambon._name_1 + " from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._province + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._province) + "\' and " + _g.d.erp_tambon._amper + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._amper) + "\' and " + _g.d.erp_tambon._code + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._tambon) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.transport_type._name_1 + " from " + _g.d.transport_type._table + " where " + _g.d.transport_type._code + "=\'" + this._getDataStr(_g.d.ap_ar_transport_label._transport_type) + "\'"));

                __myquery.Append("</node>");
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.ap_ar_transport_label._province, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.ap_ar_transport_label._amper, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.ap_ar_transport_label._tambon, (DataSet)_getData[2], warning) == false) { }
                if (_searchAndWarning(_g.d.ap_ar_transport_label._transport_type, (DataSet)_getData[3], warning) == false) { }

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
                if (fieldName.Equals(_g.d.m_allergic._member_id))
                {
                    this._setDataStr(fieldName, __getData, "", false);
                }
                else
                {
                    this._setDataStr(fieldName, __getDataStr, __getData, true);
                }

            }
            else
            {
                this._setDataStr(fieldName, "", "", true);
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
}
