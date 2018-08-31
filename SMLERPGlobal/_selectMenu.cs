using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MyLib;
using Crom.Controls.Docking;

namespace _g
{
    public class _selectMenu
    {
        private _userGroupDetailControl _userGroupDetail;
        private _userGroupDetailControl _userGroupPOApprove;
        private _userGroupDetailControl _userGroupSRApprove;
        private _userGroupDetailControl _userGroupSSApprove;


        private _g._searchChartOfAccountDialog _chartOfAccountScreen = null;
        private string _searchName = "";
        private TextBox _searchTextBox;
        private MyLib._manageMasterCodeFull __screenFull;
        private MyLib._myFrameWork _myFrameWork = new _myFrameWork();

        public Control _getObject(string menuName, string screenName, string programName)
        {

            switch (menuName.ToLower())
            {
                case "menu_change_code_item":
                    return new _changeCode._itemCodeUserControl();
                //case "menu_ap_detail": return(new SMLERPAP._ap()); break;
                //case "menu_ar_pay_bill": if (selectTab(menuName) == false) createAndSelectTab(menuName, screenName, new SMLERPAR._arPayBill()); break;
                // case "menu_so_quotation_order": if (selectTab(menuName) == false) createAndSelectTab(menuName, screenName, new SMLERPSO._so_quotation_order()); break;
                case "menu_change_code_supplier":
                    return new _changeCode._changeApArCodeUserControl(g._transTypeEnum.เจ้าหนี้);
                case "menu_change_code_customer":
                    return new _changeCode._changeApArCodeUserControl(g._transTypeEnum.ลูกหนี้);

                case "menu_view_capture_screen":
                    return new _viewCapture._viewCaptureScreen();
                case "menu_view_capture_screen_realtime":
                    return new _viewCapture._viewScreen();
                case "menu_setup_doc_format":
                    return new _docFormat(menuName, screenName, programName);
                case "menu_setup_credit_type":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_credit_type._table;
                    __screenFull._addColumn(_g.d.erp_credit_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_credit_type._code);
                    __screenFull._addColumn(_g.d.erp_credit_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_credit_type._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.erp_credit_type._charge_rate, 100, 40);
                    __screenFull._addColumn(_g.d.erp_credit_type._account_code, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_credit":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_credit._table;
                    __screenFull._addColumn(_g.d.erp_credit._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_credit._code);
                    __screenFull._addColumn(_g.d.erp_credit._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_credit._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_doc_group":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_doc_group._table;
                    __screenFull._addColumn(_g.d.erp_doc_group._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_doc_group._code);
                    __screenFull._addColumn(_g.d.erp_doc_group._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_doc_group._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                // กำหนดค่าเริ่มต้น-เจ้าหนี้
                case "menu_setup_ap_type":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ap_type._table;
                    __screenFull._addColumn(_g.d.ap_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ap_type._code);
                    __screenFull._addColumn(_g.d.ap_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ap_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ap_group_main":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ap_group._table;
                    __screenFull._addColumn(_g.d.ap_group._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ap_group._code);
                    __screenFull._addColumn(_g.d.ap_group._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ap_group._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ap_group_sub":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    //
                    __screenFull._manageDataScreen._topPanel.BorderStyle = BorderStyle.FixedSingle;
                    __screenFull._manageDataScreen._topPanel.Padding = new Padding(5, 5, 5, 5);
                    //
                    MyLib._myLabel __selectGroupMain = new MyLib._myLabel();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectGroupMain);
                    __selectGroupMain.ResourceName = "กลุ่มหลัก";
                    __selectGroupMain.TextAlign = ContentAlignment.BottomRight;
                    __selectGroupMain.Invalidate();
                    //
                    MyLib._myComboBox __selectComboGroupMain = new MyLib._myComboBox();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectComboGroupMain);
                    MyLib._myFrameWork __myAPFrameWork = new MyLib._myFrameWork();
                    DataSet __getDSAP = __myAPFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ap_group._code + "," + _g.d.ap_group._name_1 + " from " + _g.d.ap_group._table + " order by " + _g.d.ap_group._code);
                    __selectComboGroupMain.DropDownStyle = ComboBoxStyle.DropDownList;
                    __selectComboGroupMain.SelectedIndexChanged += new EventHandler(__selectComboGroupMain_SelectedIndexChanged);
                    __selectComboGroupMain.Items.Add(MyLib._myResource._findResource("กรุณาเลือกกลุ่มหลัก")._str);
                    __selectComboGroupMain.SelectedIndex = 0;
                    for (int __row = 0; __row < __getDSAP.Tables[0].Rows.Count; __row++)
                    {
                        __selectComboGroupMain.Items.Add(__getDSAP.Tables[0].Rows[__row][0].ToString() + "," + __getDSAP.Tables[0].Rows[__row][1].ToString());
                    }
                    //__screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ap_group_sub._table;
                    __screenFull._addColumn(_g.d.ap_group_sub._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ap_group_sub._code);
                    __screenFull._addColumn(_g.d.ap_group_sub._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ap_group_sub._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.ap_group_sub._sub_no, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ap_dimension":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ap_dimension._table;
                    __screenFull._addColumn(_g.d.ap_dimension._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ap_dimension._code);
                    __screenFull._addColumn(_g.d.ap_dimension._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ap_dimension._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.ap_dimension._remark, 100, 40);
                    __screenFull._addColumnNumber(_g.d.ap_dimension._dimension_no, 5, 10);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_type": // กำหนดค่าเริ่มต้น-ลูกหนี้
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_type._table;
                    __screenFull._addColumn(_g.d.ar_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_type._code);
                    __screenFull._addColumn(_g.d.ar_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_group_main":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_group._table;
                    __screenFull._addColumn(_g.d.ar_group._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_group._code);
                    __screenFull._addColumn(_g.d.ar_group._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_group._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_group_sub":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    //
                    __screenFull._manageDataScreen._topPanel.BorderStyle = BorderStyle.FixedSingle;
                    __screenFull._manageDataScreen._topPanel.Padding = new Padding(5, 5, 5, 5);
                    //
                    MyLib._myLabel __selectARGroupMain = new MyLib._myLabel();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectARGroupMain);
                    __selectARGroupMain.ResourceName = "กลุ่มหลัก";
                    __selectARGroupMain.TextAlign = ContentAlignment.BottomRight;
                    __selectARGroupMain.Invalidate();
                    //
                    MyLib._myComboBox __selectARComboGroupMain = new MyLib._myComboBox();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectARComboGroupMain);
                    MyLib._myFrameWork __myFrameWorkAR = new MyLib._myFrameWork();
                    DataSet __getArDS = __myFrameWorkAR._query(MyLib._myGlobal._databaseName, "select " + _g.d.ar_group._code + "," + _g.d.ar_group._name_1 + " from " + _g.d.ar_group._table + " order by " + _g.d.ar_group._code);
                    __selectARComboGroupMain.DropDownStyle = ComboBoxStyle.DropDownList;
                    __selectARComboGroupMain.SelectedIndexChanged += new EventHandler(__selectARComboGroupMain_SelectedIndexChanged);
                    __selectARComboGroupMain.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกกลุ่มหลัก"));
                    __selectARComboGroupMain.SelectedIndex = 0;
                    for (int __row = 0; __row < __getArDS.Tables[0].Rows.Count; __row++)
                    {
                        __selectARComboGroupMain.Items.Add(__getArDS.Tables[0].Rows[__row][0].ToString() + "," + __getArDS.Tables[0].Rows[__row][1].ToString());
                    }
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_group_sub._table;
                    __screenFull._addColumn(_g.d.ar_group_sub._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_group_sub._code);
                    __screenFull._addColumn(_g.d.ar_group_sub._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_group_sub._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.ar_group_sub._sub_no, 5, 10);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_dimension":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_dimension._table;
                    __screenFull._addColumn(_g.d.ar_dimension._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_dimension._code);
                    __screenFull._addColumn(_g.d.ar_dimension._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_dimension._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.ar_dimension._remark, 100, 40);
                    __screenFull._addColumn(_g.d.ar_dimension._dimension_no, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_work_title":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_work_title._table;
                    __screenFull._addColumn(_g.d.ar_work_title._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_work_title._code);
                    __screenFull._addColumn(_g.d.ar_work_title._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_work_title._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_project_list":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_project_list._table;
                    __screenFull._addColumn(_g.d.erp_project_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_project_list._code);
                    __screenFull._addColumn(_g.d.erp_project_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_project_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_allocate_list":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_allocate_list._table;
                    __screenFull._addColumn(_g.d.erp_allocate_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_allocate_list._code);
                    __screenFull._addColumn(_g.d.erp_allocate_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_allocate_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_job_list":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_job_list._table;
                    __screenFull._addColumn(_g.d.erp_job_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_job_list._code);
                    __screenFull._addColumn(_g.d.erp_job_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_job_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_side_list":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_side_list._table;
                    __screenFull._addColumn(_g.d.erp_side_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_side_list._code);
                    __screenFull._addColumn(_g.d.erp_side_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_side_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_sale_area":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_sale_area._table;
                    __screenFull._addColumn(_g.d.ar_sale_area._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_sale_area._code);
                    __screenFull._addColumn(_g.d.ar_sale_area._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_sale_area._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_ar_setup_paybill_area":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_paybill_area._table;
                    __screenFull._addColumn(_g.d.ar_paybill_area._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_paybill_area._code);
                    __screenFull._addColumn(_g.d.ar_paybill_area._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_paybill_area._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_logistic_area":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_logistic_area._table;
                    __screenFull._addColumn(_g.d.ar_logistic_area._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_logistic_area._code);
                    __screenFull._addColumn(_g.d.ar_logistic_area._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_logistic_area._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_credit_approve":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_credit_approve._table;
                    __screenFull._addColumn(_g.d.ar_credit_approve._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_credit_approve._code);
                    __screenFull._addColumn(_g.d.ar_credit_approve._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_credit_approve._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_pay_bill_reason":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_pay_bill_condition._table;
                    __screenFull._addColumn(_g.d.ar_pay_bill_condition._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_pay_bill_condition._code);
                    __screenFull._addColumn(_g.d.ar_pay_bill_condition._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_pay_bill_condition._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_keep_chq_reason":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_keep_chq_condition._table;
                    __screenFull._addColumn(_g.d.ar_keep_chq_condition._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_keep_chq_condition._code);
                    __screenFull._addColumn(_g.d.ar_keep_chq_condition._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_keep_chq_condition._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ar_keep_money":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_keep_money_condition._table;
                    __screenFull._addColumn(_g.d.ar_keep_money_condition._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_keep_money_condition._code);
                    __screenFull._addColumn(_g.d.ar_keep_money_condition._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ar_keep_money_condition._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_type": // กำหนดค่าเริ่มต้น-สินค้า
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_type._table;
                    __screenFull._addColumn(_g.d.ic_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_type._code);
                    __screenFull._addColumn(_g.d.ic_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_group_main":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_group._table;
                    __screenFull._addColumn(_g.d.ic_group._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_group._code);
                    __screenFull._addColumn(_g.d.ic_group._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_group._name_2, 100, 40);
                    if (MyLib._myGlobal._OEMVersion.Equals("imex"))
                    {
                        __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 1, _g.d.ic_group._sort_order, 1, 4, true, MyLib._myGlobal._getFormatNumber("m00"));

                    }
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_model":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_model._table;
                    __screenFull._addColumn(_g.d.ic_model._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_model._code);
                    __screenFull._addColumn(_g.d.ic_model._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_model._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_group_sub":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    //
                    __screenFull._manageDataScreen._topPanel.BorderStyle = BorderStyle.FixedSingle;
                    __screenFull._manageDataScreen._topPanel.Padding = new Padding(5, 5, 5, 5);
                    //
                    MyLib._myLabel __selectProductGroupLabel = new MyLib._myLabel();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectProductGroupLabel);
                    __selectProductGroupLabel.ResourceName = "กลุ่มสินค้าหลัก";
                    __selectProductGroupLabel.TextAlign = ContentAlignment.BottomRight;
                    __selectProductGroupLabel.Invalidate();
                    //
                    MyLib._myComboBox __selectProductGroup = new MyLib._myComboBox();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectProductGroup);
                    MyLib._myFrameWork __myFrameProdctGroup = new MyLib._myFrameWork();
                    DataSet __getProductGroup = __myFrameProdctGroup._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_group._code + "," + _g.d.ic_group._name_1 + " from " + _g.d.ic_group._table + " order by " + _g.d.ic_group._code);
                    __selectProductGroup.DropDownStyle = ComboBoxStyle.DropDownList;
                    __selectProductGroup.Width = 200;
                    __selectProductGroup.SelectedIndexChanged += new EventHandler(__selectProductGroup_SelectedIndexChanged);
                    __selectProductGroup.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกกลุ่มสินค้าหลัก"));
                    __selectProductGroup.SelectedIndex = 0;
                    for (int __row = 0; __row < __getProductGroup.Tables[0].Rows.Count; __row++)
                    {
                        __selectProductGroup.Items.Add(__getProductGroup.Tables[0].Rows[__row][0].ToString() + "," + __getProductGroup.Tables[0].Rows[__row][1].ToString());
                    }
                    //
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_group_sub._table;
                    __screenFull._addColumn(_g.d.ic_group_sub._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_group_sub._code);
                    __screenFull._addColumn(_g.d.ic_group_sub._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_group_sub._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.ic_group_sub._sub_no, 5, 20);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_category":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_category._table;
                    __screenFull._addColumn(_g.d.ic_category._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_category._code);
                    __screenFull._addColumn(_g.d.ic_category._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_category._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_brand":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_brand._table;
                    __screenFull._addColumn(_g.d.ic_brand._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_brand._code);
                    __screenFull._addColumn(_g.d.ic_brand._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_brand._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_pattern":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_pattern._table;
                    __screenFull._addColumn(_g.d.ic_pattern._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_pattern._code);
                    __screenFull._addColumn(_g.d.ic_pattern._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_pattern._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_design":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_design._table;
                    __screenFull._addColumn(_g.d.ic_design._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_design._code);
                    __screenFull._addColumn(_g.d.ic_design._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_design._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_grade":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_grade._table;
                    __screenFull._addColumn(_g.d.ic_grade._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_grade._code);
                    __screenFull._addColumn(_g.d.ic_grade._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_grade._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_class":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_class._table;
                    __screenFull._addColumn(_g.d.ic_class._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_class._code);
                    __screenFull._addColumn(_g.d.ic_class._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_class._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_color":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_color._table;
                    __screenFull._addColumn(_g.d.ic_color._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_color._code);
                    __screenFull._addColumn(_g.d.ic_color._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_color._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_size":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_size._table;
                    __screenFull._addColumn(_g.d.ic_size._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_size._code);
                    __screenFull._addColumn(_g.d.ic_size._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_size._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.ic_size._width_length_height, 100, 40);
                    __screenFull._addColumn(_g.d.ic_size._weight, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_dimension_name":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_dimension_name._table;
                    __screenFull._addColumn(_g.d.ic_dimension_name._dimension_number, 10, 20, 2);
                    __screenFull._addColumn(_g.d.ic_dimension_name._name_1, 100, 80);
                    __screenFull._manageDataScreen._dataList._referFieldList.Clear();
                    __screenFull._refFieldAdd = _g.d.ic_dimension_name._dimension_number;
                    __screenFull._manageDataScreen._dataList._referFieldAdd(__screenFull._refFieldAdd, 4);

                    __screenFull._finish();

                    return __screenFull;
                case "menu_setup_ic_dimension":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    //
                    __screenFull._manageDataScreen._topPanel.BorderStyle = BorderStyle.FixedSingle;
                    __screenFull._manageDataScreen._topPanel.Padding = new Padding(5, 5, 5, 5);
                    //
                    MyLib._myLabel __selectProvinceLabel = new MyLib._myLabel();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectProvinceLabel);
                    __selectProvinceLabel.ResourceName = "มิติที่ :";
                    __selectProvinceLabel.TextAlign = ContentAlignment.BottomRight;
                    __selectProvinceLabel.Invalidate();
                    //
                    MyLib._myComboBox __selectICDimension = new MyLib._myComboBox();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectICDimension);
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    __selectICDimension.DropDownStyle = ComboBoxStyle.DropDownList;
                    __selectICDimension.SelectedIndexChanged += new EventHandler(__selectICDimension_SelectedIndexChanged);
                    __selectICDimension.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกมิติ"));
                    __selectICDimension.SelectedIndex = 0;
                    DataTable __getName = __myFrameWork._queryShort("select " + _g.d.ic_dimension_name._dimension_number + "," + _g.d.ic_dimension_name._name_1 + " from " + _g.d.ic_dimension_name._table).Tables[0];
                    if (__getName.Rows.Count > 0)
                    {
                        for (int __row = 1; __row <= 20; __row++)
                        {
                            DataRow[] __select = __getName.Select(_g.d.ic_dimension_name._dimension_number + "=" + __row.ToString());
                            string __diemensionName = (__select.Length == 0) ? "" : __select[0][_g.d.ic_dimension_name._name_1].ToString();
                            if (__diemensionName.Length > 0)
                            {
                                __selectICDimension.Items.Add(__row.ToString() + "," + __diemensionName);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบข้อมูลชื่อมิติ"));
                    }
                    //
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_dimension._table;
                    __screenFull._addColumn(_g.d.ic_dimension._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_dimension._code);
                    __screenFull._addColumn(_g.d.ic_dimension._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_dimension._name_2, 100, 40);
                    __screenFull._finish();
                    //
                    /*__screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_dimension._table;
                    __screenFull._addColumn(_g.d.ic_dimension._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_dimension._code);
                    __screenFull._addColumn(_g.d.ic_dimension._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_dimension._name_2, 100, 40);
                    __screenFull._finish();*/
                    return __screenFull;
                case "menu_setup_ic_serial_deimension":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_serial_dimension._table;
                    __screenFull._addColumn(_g.d.ic_serial_dimension._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_serial_dimension._code);
                    __screenFull._addColumn(_g.d.ic_serial_dimension._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_serial_dimension._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.ic_serial_dimension._remark, 100, 40);
                    __screenFull._addColumnNumber(_g.d.ic_serial_dimension._dimension_no, 5, 20);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_unit_type":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_unit_type._table;
                    __screenFull._addColumn(_g.d.ic_unit_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_unit_type._code);
                    __screenFull._addColumn(_g.d.ic_unit_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_unit_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_unit":
                case "menu_setup_ic_unit_2":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._getTemplate = true;
                    __screenFull._getTemplateQuery = "select code as " + _g.d.gl_journal_book._code + ",name_1 as " + _g.d.gl_journal_book._name_1 + ",name_2 as " + _g.d.gl_journal_book._name_2 + " from ic_unit";
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_unit._table;
                    __screenFull._addColumn(_g.d.ic_unit._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_unit._code);
                    __screenFull._addColumn(_g.d.ic_unit._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_unit._name_2, 100, 40);
                    __screenFull.Disposed += new EventHandler(__screenFull_Disposed);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_wh":
                    {
                        __screenFull = new MyLib._manageMasterCodeFull();
                        __screenFull._labelTitle.Text = screenName;
                        __screenFull._dataTableName = _g.d.ic_warehouse._table;
                        __screenFull._addColumn(_g.d.ic_warehouse._code, 10, 20);
                        __screenFull._inputScreen._setUpper(_g.d.ic_warehouse._code);
                        __screenFull._addColumn(_g.d.ic_warehouse._name_1, 100, 40);
                        __screenFull._addColumn(_g.d.ic_warehouse._name_2, 100, 40);
                        __screenFull._addColumn(_g.d.ic_warehouse._address, 250, 50);
                        __screenFull._addColumn(_g.d.ic_warehouse._telephone, 50, 40);
                        __screenFull._addColumn(_g.d.ic_warehouse._fax, 50, 40);
                        __screenFull._addColumn(_g.d.ic_warehouse._user_group, 10, 20);
                        __screenFull._addColumn(_g.d.ic_warehouse._wh_manager, 25, 30);
                        __screenFull._addColumn(_g.d.ic_warehouse._branch_code, 25, 30);
                        __screenFull._addColumn(_g.d.ic_warehouse._branch_use, 25, 30);

                        __screenFull._checkDataForDelete += (selectedRow) =>
                        {
                            ArrayList selectRowOrder = (ArrayList)selectedRow;
                            Boolean __result = true;
                            StringBuilder __codeList = new StringBuilder();
                            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                            {
                                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                                //string __where = getData.whereString + ((this.__screenFull._manageDataScreen._dataList._extraWhere.Length == 0) ? "" : " and ") + this.__screenFull._manageDataScreen._dataList._extraWhere;
                                string __getCode = this.__screenFull._manageDataScreen._dataList._gridData._cellGet(getData.row, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();
                                if (__codeList.Length > 0)
                                {
                                    __codeList.Append(",");
                                }
                                __codeList.Append("\'" + __getCode + "\'");
                            }

                            // start check server by query 
                            //MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            MyLib._myFrameWork __myFrameWorkWareHouse = new MyLib._myFrameWork();
                            DataTable __table = __myFrameWorkWareHouse._queryShort("select " +
                                " (select count(*) as xCount from ic_trans_detail where  (" + _g.d.ic_trans_detail._wh_code + " in (" + __codeList.ToString() + ")) or (" + _g.d.ic_trans_detail._wh_code_2 + " in (" + __codeList.ToString() + ")) ) as count1, " +
                                " (select count(*) as xcount from ic_shelf where ic_shelf.whcode in (" + __codeList.ToString() + ")) as count2 , " +
                                " (select count(*) as xcount from ic_wh_shelf where ic_wh_shelf.wh_code in (" + __codeList.ToString() + ")) as count3 ").Tables[0];
                            if (__table.Rows.Count > 0 && (MyLib._myGlobal._intPhase(__table.Rows[0][0].ToString()) > 0 || MyLib._myGlobal._intPhase(__table.Rows[0][1].ToString()) > 0 || MyLib._myGlobal._intPhase(__table.Rows[0][2].ToString()) > 0))
                            {
                                __result = false;
                            }
                            if (__result == false)
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("มีการใช้งานรหัสดังกล่าวไปแล้ว กรุณาตรวจสอบ"), MyLib._myGlobal._resource("ผิดพลาด"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            return __result;
                        };

                        __screenFull._finish();
                        return __screenFull;
                    }
                case "menu_setup_ic_shelf":
                    {
                        __screenFull = new MyLib._manageMasterCodeFull();
                        //
                        __screenFull._manageDataScreen._topPanel.BorderStyle = BorderStyle.FixedSingle;
                        __screenFull._manageDataScreen._topPanel.Padding = new Padding(5, 5, 5, 5);
                        //
                        MyLib._myLabel __selectWareHouseLabel = new MyLib._myLabel();
                        __screenFull._manageDataScreen._topPanel.Controls.Add(__selectWareHouseLabel);
                        __selectWareHouseLabel.ResourceName = "คลังสินค้า";
                        __selectWareHouseLabel.TextAlign = ContentAlignment.BottomRight;
                        __selectWareHouseLabel.Invalidate();
                        //
                        MyLib._myComboBox __selectWareHouse = new MyLib._myComboBox();
                        __screenFull._manageDataScreen._topPanel.Controls.Add(__selectWareHouse);
                        MyLib._myFrameWork __myFrameWorkWareHouse = new MyLib._myFrameWork();
                        DataSet __getWareHouse = __myFrameWorkWareHouse._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_warehouse._code + "," + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " order by " + _g.d.ic_warehouse._code);
                        __selectWareHouse.DropDownStyle = ComboBoxStyle.DropDownList;
                        __selectWareHouse.Width = 200;
                        __selectWareHouse.SelectedIndexChanged += new EventHandler(__selectWareHouse_SelectedIndexChanged);
                        __selectWareHouse.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกคลังสินค้า"));
                        __selectWareHouse.SelectedIndex = 0;
                        for (int __row = 0; __row < __getWareHouse.Tables[0].Rows.Count; __row++)
                        {
                            __selectWareHouse.Items.Add(__getWareHouse.Tables[0].Rows[__row][0].ToString() + "," + __getWareHouse.Tables[0].Rows[__row][1].ToString());
                        }
                        //
                        __screenFull._labelTitle.Text = screenName;
                        __screenFull._dataTableName = _g.d.ic_shelf._table;
                        __screenFull._addColumn(_g.d.ic_shelf._code, 10, 20);
                        __screenFull._inputScreen._setUpper(_g.d.ic_shelf._code);
                        __screenFull._addColumn(_g.d.ic_shelf._name_1, 100, 40);
                        __screenFull._addColumn(_g.d.ic_shelf._name_2, 100, 40);
                        __screenFull._addColumn(_g.d.ic_shelf._width, 10, 20);
                        __screenFull._addColumn(_g.d.ic_shelf._height, 10, 20);
                        __screenFull._addColumn(_g.d.ic_shelf._depth, 10, 20);
                        __screenFull._addColumn(_g.d.ic_shelf._remark, 100, 40);

                        __screenFull._checkDataForDelete += (selectedRow) =>
                        {
                            ArrayList selectRowOrder = (ArrayList)selectedRow;
                            Boolean __result = true;
                            StringBuilder __codeList = new StringBuilder();
                            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                            {
                                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                                //string __where = getData.whereString + ((this.__screenFull._manageDataScreen._dataList._extraWhere.Length == 0) ? "" : " and ") + this.__screenFull._manageDataScreen._dataList._extraWhere;
                                string __getCode = this.__screenFull._manageDataScreen._dataList._gridData._cellGet(getData.row, _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code).ToString();
                                if (__codeList.Length > 0)
                                {
                                    __codeList.Append(",");
                                }
                                __codeList.Append("\'" + __getCode + "\'");
                            }

                            string __whCode = "";
                            if (__selectWareHouse.SelectedIndex > 0)
                            {
                                string[] __split = __selectWareHouse.SelectedItem.ToString().Split(',');
                                __whCode = __split[0];
                            }

                            // start check server by query 
                            DataTable __table = __myFrameWorkWareHouse._queryShort("select " +
                                "( select count(*) as xCount from ic_trans_detail where  ic_trans_detail.wh_code = \'" + __whCode + "\' and ((" + _g.d.ic_trans_detail._shelf_code + " in (" + __codeList.ToString() + ")) or (" + _g.d.ic_trans_detail._shelf_code_2 + " in (" + __codeList.ToString() + ")) ) ) as count1," +
                                " (select count(*) as xcount from ic_wh_shelf where ic_wh_shelf.shelf_code in (" + __codeList.ToString() + ") and ic_wh_shelf.wh_code = \'" + __whCode + "\') as count2 ").Tables[0];
                            if (__table.Rows.Count > 0 && (MyLib._myGlobal._intPhase(__table.Rows[0][0].ToString()) > 0 || MyLib._myGlobal._intPhase(__table.Rows[0][1].ToString()) > 0))
                            {
                                __result = false;
                            }
                            if (__result == false)
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("มีการใช้งานรหัสดังกล่าวไปแล้ว กรุณาตรวจสอบ"), MyLib._myGlobal._resource("ผิดพลาด"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            return __result;
                        };


                        __screenFull._finish();

                        return __screenFull;
                    }
                case "menu_setup_ic_close_reason":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_close_reason._table;
                    __screenFull._addColumn(_g.d.ic_close_reason._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_close_reason._code);
                    __screenFull._addColumn(_g.d.ic_close_reason._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_close_reason._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_acccount_group":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_account_group._table;
                    __screenFull._addColumn(_g.d.ic_account_group._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_account_group._code);
                    __screenFull._addColumn(_g.d.ic_account_group._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_account_group._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.ic_account_group._income_acc_code, 10, 20);
                    __screenFull._addColumn(_g.d.ic_account_group._item_acc_code, 10, 20);
                    __screenFull._addColumn(_g.d.ic_account_group._cost_acc_code, 10, 20);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_import_duty_point":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_import_duty._table;
                    __screenFull._addColumn(_g.d.ic_import_duty._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_import_duty._code);
                    __screenFull._addColumn(_g.d.ic_import_duty._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_import_duty._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_stk_adjust":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_stk_adjust_reason._table;
                    __screenFull._addColumn(_g.d.ic_stk_adjust_reason._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_stk_adjust_reason._code);
                    __screenFull._addColumn(_g.d.ic_stk_adjust_reason._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_stk_adjust_reason._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_issue_type":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ic_issue_type._table;
                    __screenFull._addColumn(_g.d.ic_issue_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ic_issue_type._code);
                    __screenFull._addColumn(_g.d.ic_issue_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.ic_issue_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;

                // กำหนดค่าเริ่มต้น-เงินสด ธนาคาร
                case "menu_income_list":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_income_list._table;
                    __screenFull._addColumn(_g.d.erp_income_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_income_list._code);
                    __screenFull._addColumn(_g.d.erp_income_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_income_list._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.erp_income_list._gl_account_code, 1, 20, 1, 1, true);
                    __screenFull._finish();
                    this._chartOfAccountScreen = new _g._searchChartOfAccountDialog();
                    this._chartOfAccountScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_chartOfAccountScreen__searchEnterKeyPress);
                    this._chartOfAccountScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_chartOfAccount_gridData__mouseClick);
                    __screenFull._inputScreen._textBoxSearch += new TextBoxSearchHandler(_inputScreen__textBoxSearch);
                    __screenFull._inputScreen._textBoxChanged += new TextBoxChangedHandler(_inputScreen__textBoxChanged);
                    __screenFull._loadData += new _manageMasterCodeFull.LoadDataEvent(__screenFull__loadData);
                    return __screenFull;
                case "menu_book_bank_trans_type":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_book_bank_trans_type._table;
                    __screenFull._addColumn(_g.d.erp_book_bank_trans_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_book_bank_trans_type._code);
                    __screenFull._addColumn(_g.d.erp_book_bank_trans_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_book_bank_trans_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_expenses_list":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_expenses_list._table;
                    __screenFull._addColumn(_g.d.erp_expenses_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_expenses_list._code);
                    __screenFull._addColumn(_g.d.erp_expenses_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_expenses_list._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.erp_expenses_list._gl_account_code, 1, 20, 1, 1, true);
                    __screenFull._finish();
                    this._chartOfAccountScreen = new _g._searchChartOfAccountDialog();
                    this._chartOfAccountScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_chartOfAccountScreen__searchEnterKeyPress);
                    this._chartOfAccountScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_chartOfAccount_gridData__mouseClick);
                    __screenFull._inputScreen._textBoxSearch += new TextBoxSearchHandler(_inputScreen__textBoxSearch);
                    __screenFull._inputScreen._textBoxChanged += new TextBoxChangedHandler(_inputScreen__textBoxChanged);
                    return __screenFull;
                case "menu_tax_group":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.gl_tax_group._table;
                    __screenFull._addColumn(_g.d.gl_tax_group._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.gl_tax_group._code);
                    __screenFull._addColumn(_g.d.gl_tax_group._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.gl_tax_group._name_2, 100, 40);
                    __screenFull._addColumnNumber(_g.d.gl_tax_group._tax_rate, 100, 10);
                    __screenFull._addColumn(_g.d.gl_tax_group._remark, 255, 20);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_petty_cash":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.cb_petty_cash._table;
                    __screenFull._addColumn(_g.d.cb_petty_cash._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.cb_petty_cash._code);
                    __screenFull._addColumn(_g.d.cb_petty_cash._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.cb_petty_cash._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.cb_petty_cash._remark, 255, 30);
                    __screenFull._addColumnNumber(_g.d.cb_petty_cash._credit_money, 100, 10);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_book_bank_type":  // ประเภทสมุดเงินฝาก
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_pass_book_type._table;
                    __screenFull._addColumn(_g.d.erp_pass_book_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_pass_book_type._code);
                    __screenFull._addColumn(_g.d.erp_pass_book_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_pass_book_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                // ระบบสินทรัพย์
                case "menu_asset_setup_type": //  กำหนดประเภทสินทรัพย์
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.as_asset_type._table;
                    __screenFull._addColumn(_g.d.as_asset_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.as_asset_type._code);
                    __screenFull._addColumn(_g.d.as_asset_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.as_asset_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_asset_setup_location": // กำหนดตำแหน่งที่ตั้งสินทรัพย์
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.as_asset_location._table;
                    __screenFull._addColumn(_g.d.as_asset_location._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.as_asset_location._code);
                    __screenFull._addColumn(_g.d.as_asset_location._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.as_asset_location._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_asset_setup_maintain": // กำหนดการบำรุงรักษาสินทรัพย์
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.as_asset_maintain._table;
                    __screenFull._addColumn(_g.d.as_asset_maintain._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.as_asset_maintain._code);
                    __screenFull._addColumn(_g.d.as_asset_maintain._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.as_asset_maintain._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_asset_setup_maintain_unit": // กำหนดหน่วยการบำรุงรักษาสินทรัพย์
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.as_asset_maintain_unit._table;
                    __screenFull._addColumn(_g.d.as_asset_maintain_unit._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.as_asset_maintain_unit._code);
                    __screenFull._addColumn(_g.d.as_asset_maintain_unit._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.as_asset_maintain_unit._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_asset_setup_retire": // กำหนดประเภทการตัดจำหน่าย
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.as_asset_retire._table;
                    __screenFull._addColumn(_g.d.as_asset_retire._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.as_asset_retire._code);
                    __screenFull._addColumn(_g.d.as_asset_retire._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.as_asset_retire._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;

                //กำหนดค่าเริ่มต้น-บัญชีแยกประเภท
                case "menu_setup_bank": // กำหนดรายละเอียดธนาคาร
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_bank._table;
                    __screenFull._addColumn(_g.d.erp_bank._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_bank._code);
                    __screenFull._addColumn(_g.d.erp_bank._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.erp_bank._name_2, 100, 30);
                    __screenFull._addColumn(_g.d.erp_bank._chq_income_account_code, 100, 20);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_gl_journal_book": // กำหนดสมุดบัญชี
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._getTemplate = true;
                    __screenFull._getTemplateQuery = "select code as " + _g.d.gl_journal_book._code + ",name_1 as " + _g.d.gl_journal_book._name_1 + ",name_2 as " + _g.d.gl_journal_book._name_2 + " from gl_journal_book";
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.gl_journal_book._table;
                    __screenFull._addColumn(_g.d.gl_journal_book._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.gl_journal_book._code);
                    __screenFull._addColumn(_g.d.gl_journal_book._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.gl_journal_book._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_gl_account_type": // กำหนดประเภทบัญชี
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.gl_account_type._table;
                    __screenFull._addColumn(_g.d.gl_account_type._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.gl_account_type._code);
                    __screenFull._addColumn(_g.d.gl_account_type._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.gl_account_type._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_gl_account_group":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.gl_account_group._table;
                    __screenFull._addColumn(_g.d.gl_account_group._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.gl_account_group._code);
                    __screenFull._addColumn(_g.d.gl_account_group._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.gl_account_group._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_gl_group_for_cash_flow":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.gl_cash_flow_group._table;
                    __screenFull._addColumn(_g.d.gl_cash_flow_group._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.gl_cash_flow_group._code);
                    __screenFull._addColumn(_g.d.gl_cash_flow_group._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.gl_cash_flow_group._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_gl_account_cost":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.gl_account_cost._table;
                    __screenFull._addColumn(_g.d.gl_account_cost._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.gl_account_cost._code);
                    __screenFull._addColumn(_g.d.gl_account_cost._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.gl_account_cost._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_department_list":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_department_list._table;
                    __screenFull._addColumn(_g.d.erp_department_list._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_department_list._code);
                    __screenFull._addColumn(_g.d.erp_department_list._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_department_list._name_2, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_ar_credit_group":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.ar_credit_group._table;
                    __screenFull._addColumn(_g.d.ar_credit_group._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.ar_credit_group._code);
                    __screenFull._addColumn(_g.d.ar_credit_group._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.ar_credit_group._name_2, 100, 30);
                    __screenFull._addColumn(_g.d.ar_credit_group._credit_money, 50, 10);
                    __screenFull._addColumn(_g.d.ar_credit_group._credit_max, 50, 10);
                    __screenFull._finish();
                    return __screenFull;
                // ระบบข้อมูล menu_view_manager                            
                case "menu_import_data_master": return (new MyLib._databaseManage._importDataControl());
                case "menu_gen_qeury": return (new MyLib._databaseManage._genQueryControl());
                case "menu_permissions_user": return (new MyLib._databaseManage._menupermissions_user());
                case "menu_permissions_group": return (new MyLib._databaseManage._menupermissions_group());
                case "menu_view_manager": return (new MyLib._databaseManage._viewManage(true));
                case "menu_database_struct": return (new MyLib._databaseManage._databaseStruct());
                case "menu_import": return (new MyLib._databaseManage._importExport._import());
                case "menu_query": return (new MyLib._databaseManage._queryDataView());
                case "menu_verify_database": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._verifyDatabase()); break;
                case "menu_shink_database": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._shrinkDatabase()); break;
                case "menu_change_password": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._smlChangePassword(MyLib._myGlobal._userCode)); break; //  new MyLib._databaseManage._smlChangePassword("xxxxxxxx")
                case "ic_auto_add_warehouse_and_location": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new _ic_add_warehouse_shelf_form()); break;
                case "": break;
                case "menu_setup_user_group":
                    {
                        __screenFull = new MyLib._manageMasterCodeFull();
                        __screenFull._labelTitle.Text = screenName;
                        __screenFull._dataTableName = _g.d.erp_user_group._table;
                        __screenFull._addColumn(_g.d.erp_user_group._code, 10, 20);
                        __screenFull._inputScreen._setUpper(_g.d.erp_user_group._code);
                        __screenFull._addColumn(_g.d.erp_user_group._name_1, 100, 40);
                        __screenFull._addColumn(_g.d.erp_user_group._name_2, 100, 40);
                        __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user_group._is_po, true, false);
                        __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user_group._is_so, true, false);
                        __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user_group._is_wh, true, false);
                        __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user_group._is_sale_group, true, false);
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user_group._is_pr_approve, true, false);
                            __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user_group._is_po_approve, true, false);
                            __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user_group._is_price_list_approve, true, false);
                            __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user_group._is_so_approve, true, false);
                        }
                        __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.erp_user_group._is_change_masterdata, true, false, true, true, _g.d.erp_user_group._is_change_masterdata, true);

                        _userGroupDetail = new _userGroupDetailControl();
                        //

                        if (MyLib._myGlobal._programName.Equals("SML CM"))
                        {
                            // tabcontrol
                            MyLib._myTabControl __tabControl = new _myTabControl();
                            __tabControl.TableName = _g.d.erp_user_group._table;

                            // tab User Group
                            MyLib._myTabPage tab_user_list = new _myTabPage();
                            tab_user_list.Name = "tab_user_list";
                            tab_user_list.Text = "1.tab_user_list";

                            MyLib._myTabPage tab_po_approve = new _myTabPage();
                            tab_po_approve.Name = "tab_po_approve";
                            tab_po_approve.Text = "2.tab_po_approve";

                            MyLib._myTabPage tab_ss_approve = new _myTabPage();
                            tab_ss_approve.Name = "tab_ss_approve";
                            tab_ss_approve.Text = "3.tab_ss_approve";

                            MyLib._myTabPage tab_sr_approve = new _myTabPage();
                            tab_sr_approve.Name = "tab_sr_approve";
                            tab_sr_approve.Text = "4.tab_sr_approve";

                            __tabControl.TabPages.Add(tab_user_list);
                            __tabControl.TabPages.Add(tab_po_approve);
                            __tabControl.TabPages.Add(tab_ss_approve);
                            __tabControl.TabPages.Add(tab_sr_approve);

                            __tabControl.Dock = DockStyle.Fill;


                            _userGroupPOApprove = new _userGroupDetailControl();
                            _userGroupSSApprove = new _userGroupDetailControl();
                            _userGroupSRApprove = new _userGroupDetailControl();

                            tab_user_list.Controls.Add(_userGroupDetail);
                            tab_po_approve.Controls.Add(_userGroupPOApprove);
                            tab_ss_approve.Controls.Add(_userGroupSSApprove);
                            tab_sr_approve.Controls.Add(_userGroupSRApprove);

                            __screenFull._extraPanel.Controls.Add(__tabControl);
                        }
                        else
                        {
                            __screenFull._extraPanel.Controls.Add(_userGroupDetail);
                        }

                        __screenFull._extraPanel.Dock = DockStyle.Fill; // = false;
                        //__screenFull._extraPanel.AutoSize = false;
                        //__screenFull._extraPanel.Height = 400;


                        __screenFull._afterNewData += new _manageMasterCodeFull.AfterNewDataEvent(__screenFull_AfterNewData);
                        __screenFull._afterClearData += new _manageMasterCodeFull.AfterClearDataEvent(__screenFull__afterClearData);
                        __screenFull._saveData += new _manageMasterCodeFull.SaveDataEvent(__screenFull__saveData);
                        __screenFull._deleteData += new _manageMasterCodeFull.DeleteDataEvent(__screenFull__deleteData);
                        __screenFull._loadData += new _manageMasterCodeFull.LoadDataEvent(__screenFull__loadData);
                        _userGroupNewData();
                        __screenFull._finish();
                        return __screenFull;
                    }
                // toe
                case "menu_view_manager_custom":
                    return (new _viewManageCustom());
                case "menu_setup_user_group_warehouse_shelf":
                    return (new _userGroupWarehouseShelf());
                case "menu_setup_currency":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_currency._table;
                    __screenFull._addColumn(_g.d.erp_currency._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.erp_currency._code);
                    __screenFull._addColumn(_g.d.erp_currency._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.erp_currency._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.erp_currency._symbol, 100, 40);
                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 1, _g.d.erp_currency._exchange_rate_present, 1, 4, true, MyLib._myGlobal._getFormatNumber("m04"));


                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_customer_type": return (new _customerType(screenName));
                case "menu_setup_ar_channel": return (new _arChannel(screenName));
                case "menu_setup_ar_location_type": return (new _arLocationType(screenName));
                case "menu_setup_ar_sub_type_1": return (new _arSubType1(screenName));
                case "menu_setup_ar_vehicle": return (new _arVehicle(screenName));
                case "menu_setup_ar_equipment": return (new _arEquipment(screenName));
                case "menu_setup_ar_sub_equipment": return (new _arSubEquipment(screenName));
                case "menu_setup_ar_project": return (new _arProject(screenName));
                case "menu_setup_ar_shoptype1": return (new _arShopType1(screenName));
                case "menu_setup_ar_shoptype2": return (new _arShopType2(screenName));
                case "menu_setup_ar_shoptype3": return (new _arShopType3(screenName));
                case "menu_setup_ar_shoptype4": return (new _arShopType4(screenName));
                case "menu_setup_ar_shoptype5": return (new _arShopType5(screenName));
                case "menu_setup_ar_shoptype6": return (new _arShopType6(screenName));
                case "menu_setup_ar_shoptype7": return (new _arShopType7(screenName));
                case "menu_setup_sub_ar_shoptype5":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    //
                    __screenFull._manageDataScreen._topPanel.BorderStyle = BorderStyle.FixedSingle;
                    __screenFull._manageDataScreen._topPanel.Padding = new Padding(5, 5, 5, 5);
                    //
                    MyLib._myLabel __selectSpecialChannelLabel = new MyLib._myLabel();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selectSpecialChannelLabel);
                    __selectSpecialChannelLabel.ResourceName = "Special Channel";
                    __selectSpecialChannelLabel.TextAlign = ContentAlignment.BottomRight;
                    __selectSpecialChannelLabel.Invalidate();
                    //
                    MyLib._myComboBox __selecSpecialChannel = new MyLib._myComboBox();
                    __screenFull._manageDataScreen._topPanel.Controls.Add(__selecSpecialChannel);
                    MyLib._myFrameWork __myFrameProdctGroup2 = new MyLib._myFrameWork();
                    DataSet __getProductGroup2 = __myFrameProdctGroup2._query(MyLib._myGlobal._databaseName, "select " + _g.d.ar_shoptype5._code + "," + _g.d.ar_shoptype5._name_1 + " from " + _g.d.ar_shoptype5._table + " order by " + _g.d.ar_shoptype5._code);
                    __selecSpecialChannel.DropDownStyle = ComboBoxStyle.DropDownList;
                    __selecSpecialChannel.Width = 200;
                    __selecSpecialChannel.SelectedIndexChanged += new EventHandler(__selecSpecialChannel_SelectedIndexChanged);
                    __selecSpecialChannel.Items.Add(MyLib._myGlobal._resource("กรุณาเลือก Special Channel"));
                    __selecSpecialChannel.SelectedIndex = 0;



                    for (int __row = 0; __row < __getProductGroup2.Tables[0].Rows.Count; __row++)
                    {
                        __selecSpecialChannel.Items.Add(__getProductGroup2.Tables[0].Rows[__row][0].ToString() + "," + __getProductGroup2.Tables[0].Rows[__row][1].ToString());
                    }

                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.sub_ar_shoptype5._table;
                    __screenFull._addColumn(_g.d.sub_ar_shoptype5._code, 10, 20);
                    __screenFull._inputScreen._setUpper(_g.d.sub_ar_shoptype5._code);
                    __screenFull._addColumn(_g.d.sub_ar_shoptype5._name_1, 100, 40);
                    __screenFull._addColumn(_g.d.sub_ar_shoptype5._name_2, 100, 40);
                    __screenFull._addColumn(_g.d.sub_ar_shoptype5._remark, 100, 40);
                    __screenFull._finish();

                    return __screenFull;
                    //return (new _arSubShopType5(screenName));

            }

            return null;
        }

        void __selectICDimension_SelectedIndexChanged(object sender, EventArgs e)
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
                __manageMasterCodeFull._extraInsertField = _g.d.ic_dimension._dimension_no + ",";
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                __manageMasterCodeFull._extraInsertData = __split[0].ToString() + ",";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.ic_dimension._dimension_no + "=" + __split[0].ToString() + ",";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.ic_dimension._table + "." + _g.d.ic_dimension._dimension_no + "=" + __split[0].ToString();
                __manageData._dataList._extraWhere = _g.d.ic_dimension._table + "." + _g.d.ic_dimension._dimension_no + "=" + __split[0].ToString();
                __manageData._dataList._loadViewData(0);
            }
            else
            {
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._toolBar.Enabled = false;
            }
        }

        void _inputScreen__textBoxChanged(object sender, string name)
        {

            if (name.Equals(_g.d.erp_income_list._gl_account_code) || name.Equals(_g.d.erp_expenses_list._gl_account_code))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }


        void _chartOfAccount_search(MyLib._myGrid sender, int row)
        {
            this._chartOfAccountScreen.Close();
            string __accountCode = sender._cellGet(row, 0).ToString();
            if (this._searchName.Equals(_g.d.erp_income_list._gl_account_code)) this.__screenFull._inputScreen._setDataStr(_g.d.erp_income_list._gl_account_code, __accountCode);
            if (this._searchName.Equals(_g.d.erp_expenses_list._gl_account_code)) this.__screenFull._inputScreen._setDataStr(_g.d.erp_expenses_list._gl_account_code, __accountCode);

            this._search(true);
        }

        void _chartOfAccount_gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._chartOfAccount_search((MyLib._myGrid)sender, e._row);
        }

        void _chartOfAccountScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._chartOfAccount_search(sender, row);
        }
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this.__screenFull._inputScreen._getDataStr(_g.d.erp_income_list._gl_account_code).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this.__screenFull._inputScreen._getDataStr(_g.d.erp_expenses_list._gl_account_code).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.erp_income_list._gl_account_code, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.erp_expenses_list._gl_account_code, (DataSet)_getData[1], warning);
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
                string __getDataStr = __screenFull._inputScreen._getDataStr(fieldName);
                __screenFull._inputScreen._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            else
            {
                // __screenFull._inputScreen._setDataStr(fieldName, "", "", true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && __screenFull._inputScreen._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        string __message = "";
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __screenFull._inputScreen._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        __screenFull._inputScreen._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }
        void _inputScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchName = __getControl._name.ToLower();
            string label_name = __getControl._labelName;
            if (this._searchName.Equals(_g.d.erp_income_list._gl_account_code) || this._searchName.Equals(_g.d.erp_expenses_list._gl_account_code))
            {
                this._chartOfAccountScreen.ShowDialog();
            }
        }

        void __screenFull__loadData(_manageMasterCodeFull sender)
        {
            if (sender._dataTableName.Equals(_g.d.erp_income_list._table) || sender._dataTableName.Equals(_g.d.erp_expenses_list._table))
            {
                this._search(false);
            }
            _userGroupNewData();


            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            string __query = "select " + _g.d.erp_user_group_detail._user_code + " from " + _g.d.erp_user_group_detail._table + " where " + _g.d.erp_user_group_detail._group_code + "=\'" + sender._oldCode + "\'";

            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));

            if (_userGroupPOApprove != null)
            {
                string __queryApproveGroup = "select " + _g.d.erp_user_group_approve._user_code + " from " + _g.d.erp_user_group_approve._table + " where " + _g.d.erp_user_group_approve._group_code + "=\'" + sender._oldCode + "\' and approve_flag = {0} ";

                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryApproveGroup, "0")));
                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryApproveGroup, "1")));
                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryApproveGroup, "2")));


            }
            __queryList.Append("</node>");

            _myFrameWork __frameWork = new _myFrameWork();

            ArrayList __getDataResult = __frameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());

            if (__getDataResult.Count > 0)
            {

                //DataTable __getData = __frameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                DataTable __getData = ((DataSet)__getDataResult[0]).Tables[0];
                for (int __row = 0; __row < __getData.Rows.Count; __row++)
                {
                    string __userCode = __getData.Rows[__row][0].ToString();
                    int __addr = _userGroupDetail._userListGrid._findData(1, __userCode);
                    if (__addr != -1)
                    {
                        _userGroupDetail._userListGrid._cellUpdate(__addr, 0, 1, true);
                    }
                }
            }

            _userGroupDetail._userListGrid.Invalidate();

            if (_userGroupPOApprove != null)
            {
                DataTable __getDataPO = ((DataSet)__getDataResult[1]).Tables[0];
                for (int __row = 0; __row < __getDataPO.Rows.Count; __row++)
                {
                    string __userCode = __getDataPO.Rows[__row][0].ToString();
                    int __addr = _userGroupPOApprove._userListGrid._findData(1, __userCode);
                    if (__addr != -1)
                    {
                        _userGroupPOApprove._userListGrid._cellUpdate(__addr, 0, 1, true);
                    }
                }

                DataTable __getDataSR = ((DataSet)__getDataResult[2]).Tables[0];
                for (int __row = 0; __row < __getDataSR.Rows.Count; __row++)
                {
                    string __userCode = __getDataSR.Rows[__row][0].ToString();
                    int __addr = _userGroupSRApprove._userListGrid._findData(1, __userCode);
                    if (__addr != -1)
                    {
                        _userGroupSRApprove._userListGrid._cellUpdate(__addr, 0, 1, true);
                    }
                }


                DataTable __getDataSS = ((DataSet)__getDataResult[3]).Tables[0];
                for (int __row = 0; __row < __getDataSS.Rows.Count; __row++)
                {
                    string __userCode = __getDataSS.Rows[__row][0].ToString();
                    int __addr = _userGroupSSApprove._userListGrid._findData(1, __userCode);
                    if (__addr != -1)
                    {
                        _userGroupSSApprove._userListGrid._cellUpdate(__addr, 0, 1, true);
                    }
                }

                _userGroupPOApprove._userListGrid.Invalidate();
                _userGroupSRApprove._userListGrid.Invalidate();
                _userGroupSSApprove._userListGrid.Invalidate();

            }

        }

        void __screenFull__afterClearData(_manageMasterCodeFull sender)
        {
            _userGroupNewData();
        }

        string __screenFull__deleteData(_manageMasterCodeFull sender, string fieldData)
        {
            StringBuilder __queryList = new StringBuilder();

            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _userGroupDetail._userSelectedGrid._table_name + " where " + _g.d.erp_user_group_detail._group_code + "=\'" + fieldData + "\'"));
            if (_userGroupPOApprove != null)
                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_user_group_approve._table + " where " + _g.d.erp_user_group_approve._group_code + "=\'" + fieldData + "\'"));

            return __queryList.ToString();
        }

        string __screenFull__saveData(_manageMasterCodeFull sender)
        {
            _userGroupDetail._userSelectedGrid._updateRowIsChangeAll(true);
            string __mainCode = sender._inputScreen._getDataStr(_g.d.erp_user_group._code).ToString();

            StringBuilder __queryList = new StringBuilder();

            __queryList.Append(_userGroupDetail._userSelectedGrid._createQueryForInsert(_userGroupDetail._userSelectedGrid._table_name, _g.d.erp_user_group_detail._group_code + ",", "\'" + __mainCode + "\',"));

            if (this._userGroupPOApprove != null)
            {
                _userGroupPOApprove._userSelectedGrid._updateRowIsChangeAll(true);
                _userGroupSRApprove._userSelectedGrid._updateRowIsChangeAll(true);
                _userGroupSSApprove._userSelectedGrid._updateRowIsChangeAll(true);

                __queryList.Append(_userGroupPOApprove._userSelectedGrid._createQueryForInsert(_g.d.erp_user_group_approve._table, _g.d.erp_user_group_approve._group_code + "," + "approve_flag,", "\'" + __mainCode + "\',0,"));
                __queryList.Append(_userGroupSRApprove._userSelectedGrid._createQueryForInsert(_g.d.erp_user_group_approve._table, _g.d.erp_user_group_approve._group_code + "," + "approve_flag,", "\'" + __mainCode + "\',1,"));
                __queryList.Append(_userGroupSSApprove._userSelectedGrid._createQueryForInsert(_g.d.erp_user_group_approve._table, _g.d.erp_user_group_approve._group_code + "," + "approve_flag,", "\'" + __mainCode + "\',2,"));
            }

            return __queryList.ToString();
        }



        void _userGroupNewData()
        {
            // กลุ่มพนักงาน
            _userGroupDetail._userListGrid._clear();
            _userGroupDetail._userSelectedGrid._clear();
            // ดึงชื่อพนักงานทั้งหมด
            string __query = "select " + _g.d.erp_user._code + " as \"" + _g.d.erp_user_group_detail._user_code + "\"," +
                _g.d.erp_user._name_1 + " as \"" + _g.d.erp_user_group_detail._user_name + "\"" +
                " from " + _g.d.erp_user._table + " order by " + _g.d.erp_user._code;
            _myFrameWork __frameWork = new _myFrameWork();
            DataTable __getData = __frameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
            _userGroupDetail._userListGrid._loadFromDataTable(__getData);

            if (this._userGroupPOApprove != null)
            {
                _userGroupPOApprove._userListGrid._clear();
                _userGroupPOApprove._userSelectedGrid._clear();
                _userGroupPOApprove._userListGrid._loadFromDataTable(__getData);

                _userGroupSRApprove._userListGrid._clear();
                _userGroupSRApprove._userSelectedGrid._clear();
                _userGroupSRApprove._userListGrid._loadFromDataTable(__getData);

                _userGroupSSApprove._userListGrid._clear();
                _userGroupSSApprove._userSelectedGrid._clear();
                _userGroupSSApprove._userListGrid._loadFromDataTable(__getData);

            }
        }

        void __screenFull_AfterNewData(_manageMasterCodeFull sender)
        {
            _userGroupNewData();
        }

        void __screenFull_Disposed(object sender, EventArgs e)
        {
            // Update ชื่อหน่วยนับ
            _g._utils __utils = new _utils();
            __utils._updateInventoryMaster("");
            Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
            __thread.Start();
        }

        void __selectARComboGroupMain_SelectedIndexChanged(object sender, EventArgs e)
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
                __manageMasterCodeFull._extraInsertField = _g.d.ar_group_sub._main_group + ",";
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                __manageMasterCodeFull._extraInsertData = "\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.ar_group_sub._main_group + "=\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._main_group + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._extraWhere = _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._main_group + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._loadViewData(0);
            }
            else
            {
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._toolBar.Enabled = false;
            }
        }

        void __selectComboGroupMain_SelectedIndexChanged(object sender, EventArgs e)
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
                __manageMasterCodeFull._extraInsertField = _g.d.ap_group_sub._main_group + ",";
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                __manageMasterCodeFull._extraInsertData = "\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.ap_group_sub._main_group + "=\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.ap_group_sub._table + "." + _g.d.ap_group_sub._main_group + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._extraWhere = _g.d.ap_group_sub._table + "." + _g.d.ap_group_sub._main_group + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._loadViewData(0);
            }
            else
            {
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._toolBar.Enabled = false;
            }
        }

        void __selectWareHouse_SelectedIndexChanged(object sender, EventArgs e)
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
                __manageMasterCodeFull._extraInsertField = _g.d.ic_shelf._whcode + ",";
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                __manageMasterCodeFull._extraInsertData = "\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.ic_shelf._whcode + "=\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._extraWhere = _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._loadViewData(0);
            }
            else
            {
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._toolBar.Enabled = false;
            }
        }

        void __selectProductGroup_SelectedIndexChanged(object sender, EventArgs e)
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
                __manageMasterCodeFull._extraInsertField = _g.d.ic_group_sub._main_group + ",";
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                __manageMasterCodeFull._extraInsertData = "\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.ic_group_sub._main_group + "=\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.ic_group_sub._table + "." + _g.d.ic_group_sub._main_group + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._extraWhere = _g.d.ic_group_sub._table + "." + _g.d.ic_group_sub._main_group + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._loadViewData(0);
            }
            else
            {
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._toolBar.Enabled = false;
            }
        }

        void __selecSpecialChannel_SelectedIndexChanged(object sender, EventArgs e)
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
                __manageMasterCodeFull._extraInsertField = _g.d.sub_ar_shoptype5._ar_shoptype5_code + ",";
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');
                __manageMasterCodeFull._extraInsertData = "\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.sub_ar_shoptype5._ar_shoptype5_code + "=\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.sub_ar_shoptype5._table + "." + _g.d.sub_ar_shoptype5._ar_shoptype5_code + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._extraWhere = _g.d.sub_ar_shoptype5._table + "." + _g.d.sub_ar_shoptype5._ar_shoptype5_code + "=\'" + __split[0].ToString() + "\'";
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
