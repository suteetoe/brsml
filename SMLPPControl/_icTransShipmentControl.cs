﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPPControl
{
    public partial class _icTransShipmentControl : UserControl
    {
        public delegate string _getCustCodeEvent();
        public event _getCustCodeEvent _getCustCode;
        MyLib._searchDataFull _searchAddress;
        int _mode = 0;

        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;

        public _icTransShipmentControl()
        {
            InitializeComponent();

            int __row = 0;
            //this.panel1.Enabled = false;

            this._shipmentScreen._table_name = _g.d.ic_trans_shipment._table;
            this._shipmentScreen._addTextBox(__row++, 0, 0, 0, _g.d.ic_trans_shipment._transport_name, 2, 0, 0, true, false, false);
            this._shipmentScreen._addTextBox(__row, 0, 3, 0, _g.d.ic_trans_shipment._transport_address, 2, 0, 0, true, false, false);
            __row += 3;
            this._shipmentScreen._addTextBox(__row, 0, 0, 0, _g.d.ic_trans_shipment._transport_province, 1, 0, 1, true, false, true);
            this._shipmentScreen._addTextBox(__row++, 1, 0, 0, _g.d.ic_trans_shipment._transport_amper, 1, 0, 1, true, false, true);

            this._shipmentScreen._addTextBox(__row, 0, 0, 0, _g.d.ic_trans_shipment._transport_tambon, 1, 0, 1, true, false, true);
            //this._shipmentScreen._addTextBox(__row++, 1, 0, _g.d.ic_trans_shipment, 1, 0);
            this._shipmentScreen._addTextBox(__row++, 1, 0, _g.d.ic_trans_shipment._transport_country, 1, 0);

            this._shipmentScreen._addTextBox(__row++, 0, 0, 0, _g.d.ic_trans_shipment._transport_telephone, 1, 0, 0, true, false, false);
            this._shipmentScreen._addTextBox(__row++, 0, 0, _g.d.ic_trans_shipment._transport_fax, 1, 0);

            this._shipmentScreen._addTextBox(__row++, 0, 0, 0, _g.d.ic_trans_shipment._transport_code, 1, 0, 1, true, false, true);
            this._shipmentScreen._addTextBox(__row++, 0, 0, _g.d.ic_trans_shipment._destination, 1, 0);

            this._shipmentScreen._textBoxSearch += _shipmentScreen__textBoxSearch;

        }


        void _shipmentScreen__textBoxSearch(object sender)
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

                this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridDataMastersearch__mouseClick);
                this._search_data_full._searchEnterKeyPress += _search_data_full__searchEnterKeyPress;

                this._search_data_full._dataList._refreshData();
            }
            if (this._searchName.Equals(_g.d.ar_customer._code.ToLower()))
            {
                string _where = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'AR\'";
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
            }
            else
                if (this._searchName.Equals(_g.d.ar_customer._tambon.ToLower()))
                {
                    string _where = " " + _g.d.erp_tambon._province + "=\'" + this._shipmentScreen._getDataStr(_g.d.ar_customer._province) + "\' and " + _g.d.erp_tambon._amper + "=\'" + this._shipmentScreen._getDataStr(_g.d.ar_customer._amper) + "\'";
                    MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
                }
                else
                    if (this._searchName.Equals(_g.d.ar_customer._amper.ToLower()))
                    {
                        string _where = " " + _g.d.erp_amper._province + "=\'" + this._shipmentScreen._getDataStr(_g.d.ar_customer._province) + "\'";
                        MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
                    }
                    else
                        if (this._searchName.Equals(_g.d.ar_customer_detail._group_sub_1) || this._searchName.Equals(_g.d.ar_customer_detail._group_sub_2) || this._searchName.Equals(_g.d.ar_customer_detail._group_sub_3) || this._searchName.Equals(_g.d.ar_customer_detail._group_sub_4))
                        {
                            string _where = " " + _g.d.ar_group_sub._main_group + "=\'" + this._shipmentScreen._getDataStr(_g.d.ar_customer_detail._group_main) + "\'";
                            MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
                        }
                        else
                        {
                            MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false);
                        }

        }
        string _search_screen_neme(string _name)
        {
            switch (_name)
            {
                case "transport_province": return _g.g._screen_erp_province;
                case "transport_amper": return _g.g._screen_erp_amper;
                case "transport_tambon": return _g.g._screen_erp_tambon;
                case "transport_code": return _g.g._search_screen_erp_transport;

            }
            return "";
        }

        private void _shipmentSearchButton_Click(object sender, EventArgs e)
        {
            if (this._mode == 0)
            {
                string __custCode = "";
                if (_getCustCode != null)
                {
                    __custCode = _getCustCode();
                }

                if (__custCode.Length > 0)
                {
                    if (_searchAddress == null)
                    {
                        _searchAddress = new MyLib._searchDataFull();
                        _searchAddress._name = "screen_ar_transport_label";
                        _searchAddress._dataList._loadViewFormat(_searchAddress._name, MyLib._myGlobal._userSearchScreenGroup, false);
                        _searchAddress._dataList._gridData._mouseClick += _gridData__mouseClick;
                        _searchAddress.StartPosition = FormStartPosition.CenterScreen;
                    }
                    // start search
                    // screen_ar_transport_label
                    //this._searchDataFull._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDataFull__searchEnterKeyPress);
                    _searchAddress._dataList._extraWhere = _g.d.ap_ar_transport_label._cust_code + "=\'" + __custCode + "\'";
                    _searchAddress._dataList._refreshData();
                    _searchAddress.ShowDialog();
                }
            }
            else if (this._mode == 1)
            {
                if (_searchAddress == null)
                {
                    _searchAddress = new MyLib._searchDataFull();
                    _searchAddress._name = "screen_ic_warehouse_address";
                    _searchAddress._dataList._loadViewFormat(_searchAddress._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    _searchAddress._dataList._gridData._mouseClick += _gridData__mouseClick;
                    _searchAddress.StartPosition = FormStartPosition.CenterScreen;
                }
                // start search
                // screen_ar_transport_label
                //this._searchDataFull._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDataFull__searchEnterKeyPress);
                _searchAddress._dataList._refreshData();
                _searchAddress.ShowDialog();
            }
        }

        void _gridDataMastersearch__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);

        }


        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
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

                string __name = (string)this._search_data_full._dataList._gridData._cellGet(row, 1);
                if (result.Length != 0)
                {
                    this._search_data_full.Visible = false;
                    //this._shipmentScreen._setDataStr(_searchName, result, "", false);
                    this._shipmentScreen._setDataStr(_searchName, result, __name, true);

                    //this._search(true);
                }
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            //throw new NotImplementedException();
            // search data

            //string __getCustCode = _searchAddress._dataList._gridData._cellGet(e._row, _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code).ToString();
            //string __getReceiveName = _searchAddress._dataList._gridData._cellGet(e._row, _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._name_1).ToString();
            //_search(__getCustCode, __getReceiveName);

            if (this._mode == 0)
            {
                string __getRowOrder = _searchAddress._dataList._gridData._cellGet(e._row, _g.d.ap_ar_transport_label._table + ".roworder").ToString();
                _searchByRowOrder(__getRowOrder);

            }
            else if (this._mode == 1)
            {
                string __getWHCode = _searchAddress._dataList._gridData._cellGet(e._row, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __result = __myFrameWork._queryShort("select * from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + "=\'" + __getWHCode + "\' ").Tables[0];
                if (__result.Rows.Count > 0)
                {
                    this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_name, __result.Rows[0][_g.d.ic_warehouse._wh_manager].ToString());
                    this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_address, __result.Rows[0][_g.d.ic_warehouse._address].ToString());
                    this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_telephone, __result.Rows[0][_g.d.ic_warehouse._telephone].ToString());
                    this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_fax, __result.Rows[0][_g.d.ic_warehouse._fax].ToString());
                }
            }


            _searchAddress.Close();
        }

        public void _search(string custCode, string receiveName)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort("select * from " + _g.d.ap_ar_transport_label._table + " where " + _g.d.ap_ar_transport_label._cust_code + "=\'" + custCode + "\' and " + _g.d.ap_ar_transport_label._name_1 + " =\'" + receiveName + "\' and " + _g.d.ap_ar_transport_label._ar_ap_type + " = 1 ").Tables[0];
            if (__result.Rows.Count > 0)
            {
                this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_name, __result.Rows[0][_g.d.ap_ar_transport_label._name_1].ToString());
                this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_address, __result.Rows[0][_g.d.ap_ar_transport_label._address].ToString());
                this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_telephone, __result.Rows[0][_g.d.ap_ar_transport_label._telephone].ToString());
                this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_fax, __result.Rows[0][_g.d.ap_ar_transport_label._fax].ToString());
            }
        }

        public void _searchByRowOrder(string roworder)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort("select * from " + _g.d.ap_ar_transport_label._table + " where roworder=" + roworder + " ").Tables[0];
            if (__result.Rows.Count > 0)
            {
                this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_name, __result.Rows[0][_g.d.ap_ar_transport_label._name_1].ToString());
                this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_address, __result.Rows[0][_g.d.ap_ar_transport_label._address].ToString());
                this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_telephone, __result.Rows[0][_g.d.ap_ar_transport_label._telephone].ToString());
                this._shipmentScreen._setDataStr(_g.d.ic_trans_shipment._transport_fax, __result.Rows[0][_g.d.ap_ar_transport_label._fax].ToString());
            }

        }
 
    }
}
