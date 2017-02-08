using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPConfig
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            MyLib._manageMasterCodeFull __screenFull;
            switch (menuName.ToLower())
            {
                case "menu_setup_province_auto": return (new SMLERPConfig._provinceLoadFromWebControl()); // กำหนดสมุดเงินฝากธนาคาร
                case "menu_setup_province": // กำหนดรหัสจังหวัด
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_province._table;
                    __screenFull._addColumn(_g.d.erp_province._code, 10, 10);
                    __screenFull._addColumn(_g.d.erp_province._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.erp_province._name_2, 100, 30);
                    __screenFull._addColumn(_g.d.erp_province._lat, 100, 15);
                    __screenFull._addColumn(_g.d.erp_province._lng, 100, 15);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ampher": // กำหนดรหัสอำเภอ
                    __screenFull = new MyLib._manageMasterCodeFull();
                    //
                    __screenFull._manageDataScreen._topPanel.BorderStyle = BorderStyle.FixedSingle;
                    __screenFull._manageDataScreen._topPanel.Padding = new Padding(5, 5, 5, 5);
                    //
                    MyLib._myLabel __selectProvinceLabel = new MyLib._myLabel();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectProvinceLabel);
                    __selectProvinceLabel.ResourceName = "จังหวัด :";
                    __selectProvinceLabel.TextAlign = ContentAlignment.BottomRight;
                    __selectProvinceLabel.Invalidate();
                    //
                    MyLib._myComboBox __selectProvince = new MyLib._myComboBox();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectProvince);
                    MyLib._myFrameWork __myFrameWorkAmpher = new MyLib._myFrameWork();
                    DataSet __getProvince = __myFrameWorkAmpher._query(MyLib._myGlobal._databaseName, "select " + _g.d.erp_province._code + "," + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " order by " + _g.d.erp_province._code);
                    __selectProvince.DropDownStyle = ComboBoxStyle.DropDownList;
                    __selectProvince.SelectedIndexChanged += new EventHandler(__selectProvince_SelectedIndexChanged);
                    __selectProvince.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกจังหวัด"));
                    __selectProvince.SelectedIndex = 0;
                    for (int __row = 0; __row < __getProvince.Tables[0].Rows.Count; __row++)
                    {
                        __selectProvince.Items.Add(__getProvince.Tables[0].Rows[__row][0].ToString() + "," + __getProvince.Tables[0].Rows[__row][1].ToString());
                    }
                    //
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_amper._table;
                    __screenFull._addColumn(_g.d.erp_amper._code, 10, 10);
                    __screenFull._addColumn(_g.d.erp_amper._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.erp_amper._name_2, 100, 30);
                    __screenFull._addColumn(_g.d.erp_amper._lat, 100, 15);
                    __screenFull._addColumn(_g.d.erp_amper._lng, 100, 15);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_tambon": // กำหนดรหัสตำบล
                    __screenFull = new MyLib._manageMasterCodeFull();
                    //
                    __screenFull._manageDataScreen._topPanel.BorderStyle = BorderStyle.FixedSingle;
                    __screenFull._manageDataScreen._topPanel.Padding = new Padding(5, 5, 5, 5);
                    //
                    MyLib._myLabel __selectProvinceTambonLabel = new MyLib._myLabel();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectProvinceTambonLabel);
                    __selectProvinceTambonLabel.ResourceName = "จังหวัด";
                    __selectProvinceTambonLabel.TextAlign = ContentAlignment.BottomRight;
                    __selectProvinceTambonLabel.Invalidate();
                    //
                    MyLib._myComboBox __selectProvinceTambon = new MyLib._myComboBox();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectProvinceTambon);
                    __selectProvinceTambon.DropDownStyle = ComboBoxStyle.DropDownList;
                    __selectProvinceTambon._name = "province";
                    //
                    MyLib._myLabel __selectProvinceTambonLabel2 = new MyLib._myLabel();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectProvinceTambonLabel2);
                    __selectProvinceTambonLabel2.ResourceName = "อำเภอ";
                    __selectProvinceTambonLabel2.TextAlign = ContentAlignment.BottomRight;
                    __selectProvinceTambonLabel2.Invalidate();
                    //
                    MyLib._myComboBox __selectProvinceTambon2 = new MyLib._myComboBox();
                    __selectProvinceTambon2._name = "ampher";
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectProvinceTambon2);
                    __selectProvinceTambon2.DropDownStyle = ComboBoxStyle.DropDownList;
                    // ดึงจังหวัด
                    MyLib._myFrameWork __myFrameWorkTambon = new MyLib._myFrameWork();
                    DataSet __getProvince2 = __myFrameWorkTambon._query(MyLib._myGlobal._databaseName, "select " + _g.d.erp_province._code + "," + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " order by " + _g.d.erp_province._code);
                    __selectProvinceTambon.SelectedIndexChanged += new EventHandler(__selectProvinceTambon_SelectedIndexChanged);
                    __selectProvinceTambon.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกจังหวัด"));
                    __selectProvinceTambon.SelectedIndex = 0;
                    for (int __row = 0; __row < __getProvince2.Tables[0].Rows.Count; __row++)
                    {
                        __selectProvinceTambon.Items.Add(__getProvince2.Tables[0].Rows[__row][0].ToString() + "," + __getProvince2.Tables[0].Rows[__row][1].ToString());
                    }
                    //
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_tambon._table;
                    __screenFull._addColumn(_g.d.erp_tambon._code, 10, 10);
                    __screenFull._addColumn(_g.d.erp_tambon._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.erp_amper._name_2, 100, 30);
                    __screenFull._addColumn(_g.d.erp_amper._lat, 100, 15);
                    __screenFull._addColumn(_g.d.erp_amper._lng, 100, 15);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_company_profile": // กำหนดค่าเริ่มต้น-รายละเอียดทั่วไป
                    MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new SMLERPConfig._companyProfile()); break;
                case "menu_setup_branch": // กำหนดสาขา
                    _g.g._companyProfileLoad();
                    if (_g.g._companyProfile._branchStatus == 1)
                    {
                        __screenFull = new MyLib._manageMasterCodeFull();
                        __screenFull._labelTitle.Text = screenName;
                        __screenFull._dataTableName = _g.d.erp_branch_list._table;
                        __screenFull._addColumn(_g.d.erp_branch_list._code, 10, 20);
                        __screenFull._inputScreen._setUpper(_g.d.erp_branch_list._code);
                        __screenFull._addColumn(_g.d.erp_branch_list._name_1, 100, 40);
                        __screenFull._addColumn(_g.d.erp_branch_list._name_2, 100, 40);
                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.erp_branch_list._number, 1, 0, true);
                        __screenFull._inputScreen._addTextBox(__screenFull._rowScreen, 0, 3, 0, _g.d.erp_branch_list._address_1, 1, 0, 0, true, false, true);
                        __screenFull._rowScreen += 3;
                        __screenFull._inputScreen._addTextBox(__screenFull._rowScreen, 0, 3, 0, _g.d.erp_branch_list._address_2, 1, 0, 0, true, false, true);
                        __screenFull._rowScreen += 3;
                        __screenFull._addColumn(_g.d.erp_branch_list._telephone, 100, 40);
                        __screenFull._addColumn(_g.d.erp_branch_list._fax, 100, 40);
                        __screenFull._inputScreen._addTextBox(__screenFull._rowScreen, 0, 3, 0, _g.d.erp_branch_list._remark, 1, 0, 0, true, false, true);
                        __screenFull._rowScreen += 3;
                        __screenFull._addColumn(_g.d.erp_branch_list._serial_list, 100, 40);
                        __screenFull._addColumn(_g.d.erp_branch_list._phone_number_approve, 100, 40);
                        __screenFull._addColumn(_g.d.erp_branch_list._sale_hub_approve, 100, 40);
                        __screenFull._finish();
                        return __screenFull;
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ระบบสาขาไม่ได้เปิดใช้งาน"));
                    }
                    return null;
                case "menu_setup_staff_mis": // กำหนดรหัสพนักงาน MIS
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.mis_user._table;
                    __screenFull._addColumn(_g.d.mis_user._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.mis_user._code);
                    __screenFull._addColumn(_g.d.mis_user._user_name, 100, 40);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.mis_user._password, 1, 0, 0, true, true, true);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.mis_user._is_vendor, false, true);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.mis_user._supplier_code, 1, 0, 4, true, false, true);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_approve_credit_level":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_credit_approve_level._table;

                    __screenFull._refFieldAdd = "roworder";
                    __screenFull._manageDataScreen._dataList._referFieldAdd(__screenFull._refFieldAdd, 4);
                    __screenFull._manageDataScreen._dataList._gridData._addColumn("check", 11, 0, 10, false, false, false, false);
                    __screenFull._manageDataScreen._dataList._gridData._addColumn(_g.d.erp_credit_approve_level._table + "." + "roworder", 2, 0, 10, false, true, true, false);
                    __screenFull._manageDataScreen._dataList._gridData._addColumn(_g.d.erp_credit_approve_level._table + "." + _g.d.erp_credit_approve_level._from_amount, 3, 0, 10, false, false, true, false, "m02");
                    __screenFull._manageDataScreen._dataList._gridData._addColumn(_g.d.erp_credit_approve_level._table + "." + _g.d.erp_credit_approve_level._to_amount, 3, 0, 10, false, false, true, false, "m02");
                    __screenFull._manageDataScreen._dataList._gridData._addColumn(_g.d.erp_credit_approve_level._table + "." + _g.d.erp_credit_approve_level._user_approve, 1, 0, 10, false, false, true, false);
                    __screenFull._manageDataScreen._dataList._gridData._addColumn(_g.d.erp_credit_approve_level._table + "." + _g.d.erp_credit_approve_level._phone_number_approve, 1, 0, 10, false, false, true, false);

                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 1, _g.d.erp_credit_approve_level._from_amount, 1, 3, true);
                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 1, _g.d.erp_credit_approve_level._to_amount, 1, 3, true);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.erp_credit_approve_level._user_approve, 1, 0, 0, true, false, true);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.erp_credit_approve_level._phone_number_approve, 1, 0, 0, true, false, true);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.erp_credit_approve_level._sale_hub_auth, 1, 0, 0, true, false, true);

                    /* 
                    __screenFull._addColumn(_g.d.erp_credit_approve_level._from_amount, 10, 20, 2);
                    __screenFull._addColumn(_g.d.erp_credit_approve_level._to_amount, 10, 20, 2);
                    __screenFull._addColumn(_g.d.erp_credit_approve_level._user_approve, 10, 30);
                    __screenFull._addColumn(_g.d.erp_credit_approve_level._phone_number_approve, 10, 30);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.erp_credit_approve_level._sale_hub_auth, 1, 0, 0, true, true, true);

                    __screenFull._addColumn(_g.d.mis_user._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.mis_user._code);
                    __screenFull._addColumn(_g.d.mis_user._user_name, 100, 40);*/

                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_staff": // กำหนดรหัสพนักงาน
                    __screenFull = new MyLib._manageMasterCodeFull();

                    if (MyLib._myGlobal._isUserLockDocument == true)
                    {
                        __screenFull._manageDataScreen._dataList._isLockDoc = true;
                        __screenFull._manageDataScreen._dataList._buttonUnlockDoc.Visible = true;
                        __screenFull._manageDataScreen._dataList._buttonLockDoc.Visible = true;
                        __screenFull._manageDataScreen._dataList._separatorLockDoc.Visible = true;
                    }
                    
                    __screenFull._maxColumn = 2;
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_user._table;
                    //
                    __screenFull._addColumn(_g.d.erp_user._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_user._code);
                    __screenFull._addColumn(_g.d.erp_user._name_1, 100, 40);
                    //
                    __screenFull._addColumn(_g.d.erp_user._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.erp_user._id_card, 100, 40);
                    //
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen, 0, 3, 0, _g.d.erp_user._address, 2, 0, 0, true, false, true);
                    __screenFull._rowScreen += 3;
                    //
                    __screenFull._addColumn(_g.d.erp_user._telephone, 100, 40);
                    __screenFull._addColumn(_g.d.erp_user._mobile, 100, 40);
                    //
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.erp_user._status, true, false);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 1, _g.d.erp_user._price_level_1, true, false);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.erp_user._price_level_2, true, false);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 1, 1, 0, _g.d.erp_user._password, 1, 0, 0, true, true, true);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.erp_user._pos_cashier, true, false);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 1, _g.d.erp_user._discount_level_1, true, false);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.erp_user._cancel_pos_bill_level, true, false);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 1, _g.d.erp_user._open_cash_drawer, true, false);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user._show_total_balance, true, false);
                    //__screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user._discount_bill_pos, true, false);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen, 0, 1, 0, _g.d.erp_user._ic_wht, 1, 50, 1, true, false, true);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 1, 1, 0, _g.d.erp_user._ic_shelf, 1, 50, 1, true, false, true);


                    __screenFull._inputScreen._textBoxSearch += (object sender) =>
                    {
                        if (sender.GetType() == typeof(MyLib._myTextBox))
                        {
                            MyLib._myTextBox __searchTextbox = (MyLib._myTextBox)sender;
                            if (__searchTextbox._name == _g.d.erp_user._branch_code)
                            {
                                MyLib._searchDataFull __searchBranch = new MyLib._searchDataFull();
                                __searchBranch.Text = MyLib._myGlobal._resource("ค้นหาสาขา");
                                __searchBranch._dataList._loadViewFormat(_g.g._search_master_erp_branch_list, MyLib._myGlobal._userSearchScreenGroup, false);
                                __searchBranch._dataList._gridData._mouseClick += (s1, e1) =>
                                {
                                    string __branch_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.erp_branch_list._table + "." + _g.d.erp_branch_list._code).ToString();
                                    string __branch_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.erp_branch_list._table + "." + _g.d.erp_branch_list._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(_g.d.erp_user._branch_code, __branch_code, __branch_name, true);
                                };
                                __searchBranch._searchEnterKeyPress += (s1, e1) =>
                                {
                                    string __branch_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.erp_branch_list._table + "." + _g.d.erp_branch_list._code).ToString();
                                    string __branch_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.erp_branch_list._table + "." + _g.d.erp_branch_list._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(_g.d.erp_user._branch_code, __branch_code, __branch_name, true);
                                };

                                MyLib._myGlobal._startSearchBox(__searchTextbox, __searchTextbox, "ค้นหาสาขา", __searchBranch, false);
                            }
                            else if (__searchTextbox._name == _g.d.erp_user._ic_wht)
                            {
                                MyLib._searchDataFull __searchBranch = new MyLib._searchDataFull();
                                __searchBranch.Text = MyLib._myGlobal._resource("ค้นหาคลังสินค้า");
                                __searchBranch._dataList._loadViewFormat(_g.g._search_master_ic_warehouse, MyLib._myGlobal._userSearchScreenGroup, false);
                                __searchBranch._dataList._gridData._mouseClick += (s1, e1) =>
                                {
                                    string __wh_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();
                                    string __wh_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __wh_code, __wh_name, true);
                                };
                                __searchBranch._searchEnterKeyPress += (s1, e1) =>
                                {
                                    string __wh_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();
                                    string __wh_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __wh_code, __wh_name, true);
                                };

                                MyLib._myGlobal._startSearchBox(__searchTextbox, __searchTextbox, "ค้นหาสาขา", __searchBranch, false);
                            }
                            else if (__searchTextbox._name == _g.d.erp_user._ic_shelf)
                            {
                                MyLib._searchDataFull __searchBranch = new MyLib._searchDataFull();
                                __searchBranch.Text = MyLib._myGlobal._resource("ค้นหาที่เก็บ");
                                __searchBranch._dataList._loadViewFormat(_g.g._search_master_ic_shelf_warehouse, MyLib._myGlobal._userSearchScreenGroup, false);
                                __searchBranch._dataList._extraWhere2 = _g.d.ic_shelf._whcode + "= \'" + __screenFull._inputScreen._getDataStr(_g.d.erp_user._ic_wht) + "\' ";
                                __searchBranch._dataList._gridData._mouseClick += (s1, e1) =>
                                {
                                    string __shelf_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code).ToString();
                                    string __shelf_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ic_shelf._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __shelf_code, __shelf_name, true);
                                };
                                __searchBranch._searchEnterKeyPress += (s1, e1) =>
                                {
                                    string __shelf_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code).ToString();
                                    string __shelf_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ic_shelf._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __shelf_code, __shelf_name, true);
                                };


                                MyLib._myGlobal._startSearchBox(__searchTextbox, __searchTextbox, "ค้นหาสาขา", __searchBranch, false);
                            }
                            else if (__searchTextbox._name == _g.d.erp_user._parent_code)
                            {
                                MyLib._searchDataFull __searchBranch = new MyLib._searchDataFull();
                                __searchBranch.Text = MyLib._myGlobal._resource("ค้นหาพนักงาน");
                                __searchBranch._dataList._loadViewFormat(_g.g._search_screen_erp_user, MyLib._myGlobal._userSearchScreenGroup, false);
                                __searchBranch._dataList._gridData._mouseClick += (s1, e1) =>
                                {
                                    string __user_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.erp_user._table + "." + _g.d.erp_user._code).ToString();
                                    string __user_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.erp_user._table + "." + _g.d.erp_user._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __user_code, __user_name, true);
                                };
                                __searchBranch._searchEnterKeyPress += (s1, e1) =>
                                {
                                    string __user_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.erp_user._table + "." + _g.d.erp_user._code).ToString();
                                    string __user_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.erp_user._table + "." + _g.d.erp_user._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __user_code, __user_name, true);
                                };

                                MyLib._myGlobal._startSearchBox(__searchTextbox, __searchTextbox, "ค้นหาสาขา", __searchBranch, false);
                            }
                            else if (__searchTextbox._name == _g.d.erp_user._area_code)
                            {
                                MyLib._searchDataFull __searchBranch = new MyLib._searchDataFull();
                                __searchBranch.Text = MyLib._myGlobal._resource("ค้นหาเขตการขาย");
                                __searchBranch._dataList._loadViewFormat(_g.g._search_master_ar_area_code, MyLib._myGlobal._userSearchScreenGroup, false);
                                __searchBranch._dataList._gridData._mouseClick += (s1, e1) =>
                                {
                                    string __user_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ar_sale_area._table + "." + _g.d.ar_sale_area._code).ToString();
                                    string __user_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ar_sale_area._table + "." + _g.d.ar_sale_area._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __user_code, __user_name, true);
                                };
                                __searchBranch._searchEnterKeyPress += (s1, e1) =>
                                {
                                    string __user_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ar_sale_area._table + "." + _g.d.ar_sale_area._code).ToString();
                                    string __user_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ar_sale_area._table + "." + _g.d.ar_sale_area._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __user_code, __user_name, true);
                                };

                                MyLib._myGlobal._startSearchBox(__searchTextbox, __searchTextbox, "ค้นหาเขตการขาย", __searchBranch, false);
                            }
                            else if (__searchTextbox._name == _g.d.erp_user._area_paybill)
                            {

                                MyLib._searchDataFull __searchBranch = new MyLib._searchDataFull();
                                __searchBranch.Text = MyLib._myGlobal._resource("ค้นหาเก็บเงิน");
                                __searchBranch._dataList._loadViewFormat(_g.g._search_master_ar_paybill_area_code, MyLib._myGlobal._userSearchScreenGroup, false);
                                __searchBranch._dataList._gridData._mouseClick += (s1, e1) =>
                                {
                                    string __user_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ar_paybill_area._table + "." + _g.d.ar_paybill_area._code).ToString();
                                    string __user_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ar_paybill_area._table + "." + _g.d.ar_paybill_area._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __user_code, __user_name, true);
                                };
                                __searchBranch._searchEnterKeyPress += (s1, e1) =>
                                {
                                    string __user_code = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ar_paybill_area._table + "." + _g.d.ar_paybill_area._code).ToString();
                                    string __user_name = __searchBranch._dataList._gridData._cellGet(__searchBranch._dataList._gridData._selectRow, _g.d.ar_paybill_area._table + "." + _g.d.ar_paybill_area._name_1).ToString();
                                    __searchBranch.Close();
                                    __searchBranch.Dispose();
                                    __screenFull._inputScreen._setDataStr(__searchTextbox._name, __user_code, __user_name, true);
                                };

                                MyLib._myGlobal._startSearchBox(__searchTextbox, __searchTextbox, "ค้นหาเก็บเงิน", __searchBranch, false);

                            }
                        }
                    };

                    if (_g.g._companyProfile._branchStatus == 1)
                    {
                        __screenFull._inputScreen._addTextBox(__screenFull._rowScreen, 0, 1, 0, _g.d.erp_user._branch_code, 1, 50, 1, true, false, true);
                        __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 1, _g.d.erp_user._change_branch_code, true, false);
                    }
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.erp_user._lock_bill, true, false);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 1, _g.d.erp_user._approve_ar_credit, true, false);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS || MyLib._myGlobal._OEMVersion.Equals("imex"))
                    {
                        __screenFull._inputScreen._addTextBox(__screenFull._rowScreen, 0, 1, 1, _g.d.erp_user._parent_code, 1, 10, 1, true, false);
                        __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 1, 1, 1, _g.d.erp_user._area_code, 1, 10, 1, true, false);
                        __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 0, 1, 1, _g.d.erp_user._area_paybill, 1, 10, 1, true, false);

                    }
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.erp_user._show_item_cost, true, false);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 1, _g.d.erp_user._disable_edit_doc_no_doc_date_user, true, false);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user._delete_item_pos, true, false);

                    }

                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.erp_user._reset_print_log, true, false);
                        __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user._mobile_user, true, false);
                    }

                    if (MyLib._myGlobal._programName == "SML CM")
                    {
                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen, 0, 1, 0, _g.d.erp_user._po_approve_from, 1, 2, true);
                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 1, 1, 0, _g.d.erp_user._po_approve_to, 1, 2, true);

                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen, 0, 1, 0, _g.d.erp_user._sr_approve_from, 1, 2, true);
                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 1, 1, 0, _g.d.erp_user._sr_approve_to, 1, 2, true);

                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen, 0, 1, 0, _g.d.erp_user._sr_approve_from_percent, 1, 2, true);
                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 1, 1, 0, _g.d.erp_user._sr_approve_to_percent, 1, 2, true);

                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen, 0, 1, 0, _g.d.erp_user._ss_approve_from, 1, 2, true);
                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 1, 1, 0, _g.d.erp_user._ss_approve_to, 1, 2, true);

                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen, 0, 1, 0, _g.d.erp_user._cr_approve_from, 1, 2, true);
                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 1, 1, 0, _g.d.erp_user._cr_approve_to, 1, 2, true);

                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.erp_user._max_cr_approve, 1, 2, true);
                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.erp_user._load_approve_percent, 1, 2, true);

                        //__screenFull._inputScreen._addNumberBox(__screenFull._rowScreen, 1, 0, 1, true, _g.d.erp_user._picture_1);
                    }

                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user._is_login_user, true, false);
                    __screenFull._inputScreen._addImage(__screenFull._rowScreen, 0, 3, 1, true, _g.d.erp_user._picture_1);
                    __screenFull._inputScreen._addImage(__screenFull._rowScreen, 1, 3, 1, true, _g.d.erp_user._signature_1);
                    __screenFull._inputScreen.Dock = DockStyle.Fill;

                    //__screenFull._rowScreen += 3;
                    // sml cm                  

                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_side": //กำหนดหน่วยงาน
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_side_list._table;
                    __screenFull._addColumn(_g.d.erp_side_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_side_list._code);
                    __screenFull._addColumn(_g.d.erp_side_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_side_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_transport"://กำหนดประเภทการขนส่ง
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.transport_type._table;
                    __screenFull._addColumn(_g.d.transport_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.transport_type._code);
                    __screenFull._addColumn(_g.d.transport_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.transport_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_department": //กำหนดแผนก
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_department_list._table;
                    __screenFull._addColumn(_g.d.erp_department_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_department_list._code);
                    __screenFull._addColumn(_g.d.erp_department_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_department_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_allocate": //กำหนดการจัดสรร
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_allocate_list._table;
                    __screenFull._addColumn(_g.d.erp_allocate_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_allocate_list._code);
                    __screenFull._addColumn(_g.d.erp_allocate_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_allocate_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_project":  //กำหนด Project
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_project_list._table;
                    __screenFull._addColumn(_g.d.erp_project_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_project_list._code);
                    __screenFull._addColumn(_g.d.erp_project_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_project_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_job_type": //กำหนดประเภทงาน
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_side_list._table;
                    __screenFull._addColumn(_g.d.erp_side_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_side_list._code);
                    __screenFull._addColumn(_g.d.erp_side_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_side_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_job": //กำหนดงาน
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_job_list._table;
                    __screenFull._addColumn(_g.d.erp_job_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_job_list._code);
                    __screenFull._addColumn(_g.d.erp_job_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_job_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_book_bank": return (new SMLERPConfig._book()); //กำหนดสมุดเงินฝากธนาคาร
                case "menu_income_list": return (new SMLERPConfig._income_list()); // กำหนดรายละเอียดรายได้
                case "menu_expenses_list": return (new SMLERPConfig._expenses_list()); // กำหนดประเภทค่าใช้จ่าย
                case "menu_account_period": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new SMLERPConfig._accountPeriod._accountPeriod()); break; //กำหนดผังบัญชี
                case "menu_option": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new SMLERPConfig._option()); break;  // Option
                case "menu_setup_bank_branch": // กำหนดสาขาธนาคาร
                    __screenFull = new MyLib._manageMasterCodeFull();
                    //
                    __screenFull._manageDataScreen._topPanel.BorderStyle = BorderStyle.FixedSingle;
                    __screenFull._manageDataScreen._topPanel.Padding = new Padding(5, 5, 5, 5);
                    //
                    MyLib._myLabel __selectBankLabel = new MyLib._myLabel();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectBankLabel);
                    __selectBankLabel.ResourceName = "ธนาคาร";
                    __selectBankLabel.TextAlign = ContentAlignment.BottomRight;
                    __selectBankLabel.Invalidate();
                    //
                    MyLib._myComboBox __selectBank = new MyLib._myComboBox();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectBank);
                    MyLib._myFrameWork __myFrameWorkBank = new MyLib._myFrameWork();
                    DataSet __getBank = __myFrameWorkBank._query(MyLib._myGlobal._databaseName, "select " + _g.d.erp_bank._code + "," + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " order by " + _g.d.erp_bank._code);
                    __selectBank.DropDownStyle = ComboBoxStyle.DropDownList;
                    __selectBank.SelectedIndexChanged += new EventHandler(__selectBank_SelectedIndexChanged);
                    __selectBank.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกธนาคาร"));
                    __selectBank.SelectedIndex = 0;
                    for (int __row = 0; __row < __getBank.Tables[0].Rows.Count; __row++)
                    {
                        __selectBank.Items.Add(__getBank.Tables[0].Rows[__row][0].ToString() + "," + __getBank.Tables[0].Rows[__row][1].ToString());
                    }
                    //
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_bank_branch._table;
                    __screenFull._addColumn(_g.d.erp_bank_branch._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_bank_branch._code);
                    __screenFull._addColumn(_g.d.erp_bank_branch._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_bank_branch._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_sale_plan": return (new _salePlanManage());
            }
            return null;
        }

        /// <summary>
        /// หน้าจอ กำหนดรหัสตำบล ตอนเลือกจังหวัด
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void __selectProvinceTambon_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control __p1 = ((Control)sender).Parent;
            Control __p2 = __p1.Parent;
            MyLib._myManageData __manageData = (MyLib._myManageData)__p2.Parent.Parent.Parent.Parent.Parent.Parent;
            MyLib._manageMasterCodeFull __manageMasterCodeFull = (MyLib._manageMasterCodeFull)((Control)__manageData).Parent;
            MyLib._myComboBox __comboBox = (MyLib._myComboBox)sender;
            MyLib._myFlowLayoutPanel __myFlowLayoutPanel = (MyLib._myFlowLayoutPanel)__manageData._topPanel;
            MyLib._myComboBox __comboBoxAmpher = null;
            foreach (Control __getControl in __myFlowLayoutPanel.Controls)
            {
                if (__getControl.GetType() == typeof(MyLib._myComboBox))
                {
                    MyLib._myComboBox __temp = (MyLib._myComboBox)__getControl;
                    if (__temp._name != null && __temp._name.Equals("ampher"))
                    {
                        __comboBoxAmpher = __temp;
                        break;
                    }
                }
            }
            if (__comboBox.SelectedIndex > 0)
            {
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                string __provinceCode = __split[0].ToString();
                __comboBoxAmpher.Enabled = true;
                // ดึงอำเภอ
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __getAmpher = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.erp_amper._code + "," + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._province + "=\'" + __provinceCode + "\' order by " + _g.d.erp_amper._code);
                __comboBoxAmpher.SelectedIndexChanged -= new EventHandler(__comboBoxAmpher_SelectedIndexChanged);
                __comboBoxAmpher.SelectedIndexChanged += new EventHandler(__comboBoxAmpher_SelectedIndexChanged);
                __comboBoxAmpher.Items.Clear();
                __comboBoxAmpher.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกอำเภอ"));
                __comboBoxAmpher.SelectedIndex = 0;
                for (int __row = 0; __row < __getAmpher.Tables[0].Rows.Count; __row++)
                {
                    __comboBoxAmpher.Items.Add(__getAmpher.Tables[0].Rows[__row][0].ToString() + "," + __getAmpher.Tables[0].Rows[__row][1].ToString());
                }
            }
            else
            {
                __comboBoxAmpher.Enabled = false;
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._toolBar.Enabled = false;
            }
        }

        static void __comboBoxAmpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control __p1 = ((Control)sender).Parent;
            Control __p2 = __p1.Parent;
            MyLib._myManageData __manageData = (MyLib._myManageData)__p2.Parent.Parent.Parent.Parent.Parent.Parent;
            MyLib._manageMasterCodeFull __manageMasterCodeFull = (MyLib._manageMasterCodeFull)((Control)__manageData).Parent;
            MyLib._myComboBox __comboBox = (MyLib._myComboBox)sender;
            MyLib._myFlowLayoutPanel __myFlowLayoutPanel = (MyLib._myFlowLayoutPanel)__manageData._topPanel;
            MyLib._myComboBox __comboBoxProivince = null;
            foreach (Control __getControl in __myFlowLayoutPanel.Controls)
            {
                if (__getControl.GetType() == typeof(MyLib._myComboBox))
                {
                    MyLib._myComboBox __temp = (MyLib._myComboBox)__getControl;
                    if (__temp._name != null && __temp._name.Equals("province"))
                    {
                        __comboBoxProivince = __temp;
                        break;
                    }
                }
            }
            //
            if (__comboBox.SelectedIndex > 0)
            {
                __manageData._dataList.Enabled = true;
                __manageMasterCodeFull._toolBar.Enabled = true;
                //
                string[] __splitProvince = __comboBoxProivince.SelectedItem.ToString().Split(',');
                string __provinceCode = __splitProvince[0].ToString();
                //
                __manageData._dataList.Enabled = true;
                __manageMasterCodeFull._toolBar.Enabled = true;
                __manageMasterCodeFull._extraInsertField = _g.d.erp_tambon._province + "," + _g.d.erp_tambon._amper + ",";
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                __manageMasterCodeFull._extraInsertData = "\'" + __provinceCode + "\',\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.erp_tambon._province + "=\'" + __provinceCode + "\'," + _g.d.erp_tambon._amper + "=\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=\'" + __provinceCode + "\' and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._extraWhere = _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=\'" + __provinceCode + "\' and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._loadViewData(0);
            }
            else
            {
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._toolBar.Enabled = false;
            }
        }

        /// <summary>
        /// หน้าจอ กำหนดรหัสอำเภอ ตอนเลือกจังหวัด
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void __selectProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control __p1 = ((Control)sender).Parent;
            Control __p2 = __p1.Parent;
            MyLib._myManageData __manageData = (MyLib._myManageData)__p2.Parent.Parent.Parent.Parent.Parent.Parent;
            MyLib._manageMasterCodeFull __manageMasterCodeFull = (MyLib._manageMasterCodeFull)((Control)__manageData).Parent;
            MyLib._myComboBox __comboBox = (MyLib._myComboBox)sender;
            if (__comboBox.SelectedIndex > 0)
            {
                __manageData._dataList.Enabled = true;
                __manageMasterCodeFull._toolBar.Enabled = true;
                __manageMasterCodeFull._extraInsertField = _g.d.erp_amper._province + ",";
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                __manageMasterCodeFull._extraInsertData = "\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.erp_amper._province + "=\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._extraWhere = _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._loadViewData(0);
            }
            else
            {
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._toolBar.Enabled = false;
            }
        }
        /// <summary>
        /// หน้าจอ กำหนดสาขาธนาคาร ตอนเลือกธนาคาร
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void __selectBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control __p1 = ((Control)sender).Parent;
            Control __p2 = __p1.Parent;
            MyLib._myManageData __manageData = (MyLib._myManageData)__p2.Parent.Parent.Parent.Parent.Parent.Parent;
            MyLib._manageMasterCodeFull __manageMasterCodeFull = (MyLib._manageMasterCodeFull)((Control)__manageData).Parent;
            MyLib._myComboBox __comboBox = (MyLib._myComboBox)sender;
            if (__comboBox.SelectedIndex > 0)
            {
                __manageData._dataList.Enabled = true;
                __manageMasterCodeFull._toolBar.Enabled = true;
                __manageMasterCodeFull._extraInsertField = _g.d.erp_bank_branch._bank_code + ",";
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                __manageMasterCodeFull._extraInsertData = "\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.erp_bank_branch._bank_code + "=\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._extraWhere = _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._loadViewData(0);
            }
            else
            {
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._toolBar.Enabled = false;
            }
        }
    }
}
