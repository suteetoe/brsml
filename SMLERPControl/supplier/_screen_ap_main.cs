using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

/*
* @author MooAe 
* @copyright 2009
* @mail naiay@msn.com
*/

namespace SMLERPControl.supplier
{
    public partial class _screen_ap_main : MyLib._myScreen
    {
        private _controlTypeEnum _controlTypeTemp;
        _g._searchChartOfAccountDialog _chartOfAccountScreen = null;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full;
        string[] _get_every_date = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28" };
        string _searchName = "";
        TextBox _searchTextBox;

        public _controlTypeEnum _controlName
        {
            set
            {
                this._controlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._controlTypeTemp;
            }
        }
        void _chartOfAccount_search(MyLib._myGrid sender, int row)
        {
            this._chartOfAccountScreen.Close();
            string __accountCode = sender._cellGet(row, 0).ToString();
            if (this._searchName.Equals(_g.d.ap_supplier_detail._account_code)) this._setDataStr(_g.d.ap_supplier_detail._account_code, __accountCode);
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
        public _screen_ap_main()
        {
            //this._build();
            this._chartOfAccountScreen = new _g._searchChartOfAccountDialog();
            this._chartOfAccountScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_chartOfAccountScreen__searchEnterKeyPress);
            this._chartOfAccountScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_chartOfAccount_gridData__mouseClick);
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            int __row = 0;
            switch (_controlName)
            {
                case _controlTypeEnum.Ap:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_supplier._table;
                    this._addTextBox(__row, 0, 1, 0, _g.d.ap_supplier._code, 1, 1, 1, true, false, false);                   //รหัสผู้จำหน่าย
                    // toe ย้าย มาเพิ่ม prefixname fitstname lastname
                    MyLib._myGroupBox _ap_status_groupbox = this._addGroupBox(__row, 1, 1, 2, 1, _g.d.ap_supplier._ap_status, true);
                    this._addRadioButtonOnGroupBox(__row, 0, _ap_status_groupbox, _g.d.ap_supplier._personality, 0, true);
                    this._addRadioButtonOnGroupBox(__row++, 1, _ap_status_groupbox, _g.d.ap_supplier._juristic_person, 1, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ap_supplier._prefixname, 1, 15, 0, true, false, true);            // คำนำหน้า
                    this._addTextBox(__row, 0, 1, 0, _g.d.ap_supplier._firstname, 1, 50, 0, true, false, true);             // ชื่อ
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ap_supplier._lastname, 1, 70, 0, true, false, true);              // นามสกุล
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ap_supplier._name_1, 2, 0, 0, true, false, false);                 //ชื่อผู้จำหน่าย 1 ไทย

                    this._addTextBox(__row++, 0, 2, 0, _g.d.ap_supplier._address, 2, 2, 0, true, false, true);              //ที่อยู่ ไทย
                    __row++;
                    this._addTextBox(__row, 0, 1, 0, _g.d.ap_supplier._province, 1, 1, 1, true, false, true);               //จังหวัด
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ap_supplier._amper, 1, 1, 1, true, false, true);                  //อำเภอ
                    this._addTextBox(__row, 0, 1, 0, _g.d.ap_supplier._tambon, 1, 1, 1, true, false, true);                 //ตำบล
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ap_supplier._zip_code, 1, 1, 0, true, false, true);               //รหัสไปรษณีย์
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ap_supplier._country, 1, 1, 0, true, false, true);                   // ประเทศ
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ap_supplier._code_old, 1, 1, 0, true, false, true);
                    }
                    this._addTextBox(__row, 0, 1, 0, _g.d.ap_supplier._telephone, 1, 1, 0, true, false, true);                   //โทรศัพท์
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ap_supplier._fax, 1, 1, 0, true, false, true);                         //โทรสาร
                    this._addTextBox(__row, 0, 1, 0, _g.d.ap_supplier._email, 1, 1, 0, true, false, true);                       //อีเมล์
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ap_supplier._ap_type, 1, 1, 1, true, false, true);               //ประเภทผู้จำหน่าย
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ap_supplier._website, 2, 1, 0, true, false, true);                     //เว็บไซด์

                    this._addTextBox(__row, 0, 1, 0, _g.d.ap_supplier._tax_id, 1, 0, 0, true, false, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ap_supplier._card_id, 1, 0, 0, true, false, true, false);
                    this._addComboBox(__row, 0, _g.d.ap_supplier._branch_type, 1, true, _g.g._ap_ar_branch_type, true, _g.d.ap_supplier._branch_type, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ap_supplier._branch_code, 1, 0, 0, true, false, true, false);

                    this._addTextBox(__row, 0, 2, 0, _g.d.ap_supplier._remark, 1, 100, 0, true, false, true);              //หมายเหตุ
                    MyLib._myGroupBox _status_groupbox = this._addGroupBox(__row, 1, 1, 2, 1, _g.d.ap_supplier._status, true);
                    this._addRadioButtonOnGroupBox(0, 0, _status_groupbox, _g.d.ap_supplier._active, 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, _status_groupbox, _g.d.ap_supplier._inactive, 1, false);
                    __row++;
                    __row++;
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ap_supplier._interco, 1, 0, 0, true, false);

                    __enableFullName(true);
                    break;
                case _controlTypeEnum.ApDetail:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_supplier_detail._table;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_supplier_detail._staff_pay_code, 1, 2, 1, true, false, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_supplier_detail._payment_way, 1, 2, 0, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_supplier_detail._pay_bill_way, 1, 2, 0, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_supplier_detail._pay_condition, 1, 2, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_supplier_detail._credit_purchase, 1, 0, 0, true, false, true);
                    //this._addTextBox(2, 1, 1, 0, _g.d.ap_supplier_detail._credit_code, 1, 0, 0, true, false, true);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_supplier_detail._discount_item, 1, 0, 0, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_supplier_detail._discount_bill, 1, 0, 0, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ap_supplier_detail._credit_day, 1, 0, 0, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_supplier_detail._tax_id, 1, 0, 0, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ap_supplier_detail._card_id, 1, 0, 0, true, false, true);

                    this._addComboBox(5, 0, _g.d.ar_customer_detail._branch_type, true, _g.g._ap_ar_branch_type, true);
                    this._addTextBox(5, 1, 1, 0, _g.d.ar_customer_detail._branch_code, 1, 0, 0, true, false, true);

                    this._addCheckBox(6, 0, _g.d.ap_supplier_detail._set_tax_type, false, true);
                    this._addComboBox(7, 0, _g.d.ap_supplier_detail._tax_type, true, _g.g._po_so_tax_type, true);
                    this._addTextBox(7, 1, 1, 0, _g.d.ap_supplier_detail._tax_rate, 1, 0, 0, true, false, true);
                    //this._addTextBox(6, 0, 1, 0, _g.d.ap_supplier_detail._account_code, 1, 0, 0, true, false, true);
                    this._addTextBox(8, 0, 1, 0, _g.d.ap_supplier_detail._shipping_type, 1, 0, 0, true, false, true);
                    this._addTextBox(9, 0, 1, 0, _g.d.ap_supplier_detail._account_code, 1, 2, 4, true, false, true);
                    //this._addTextBox(7, 1, 1, 0, _g.d.ap_supplier_detail._passbook_code, 1, 2, 1, true, false, true);
                    /* MyLib._myGroupBox _tax_typy = this._addGroupBox(4, 0, 1, 3, 2, _g.d.ap_supplier_detail._tax_type, true);
                     this._addRadioButtonOnGroupBox(0, 0, _tax_typy, _g.d.ic_resource._zero_vat, 0, true);
                     this._addRadioButtonOnGroupBox(0, 1, _tax_typy, _g.d.ic_resource._excise_vat, 1, false);
                     this._addRadioButtonOnGroupBox(0, 2, _tax_typy, _g.d.ic_resource._exc_vat, 2, false);*/

                    break;
                case _controlTypeEnum.ApDetailCredit:
                    /*this._maxColumn = 2;
                    this._table_name = _g.d.ap_supplier_detail._table;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_supplier_detail._credit_group_code, 1, 25, 1, true, false, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_supplier_detail._credit_person, 1, 2, 1, true, false, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ap_supplier_detail._credit_money, 1, 1, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ap_supplier_detail._credit_money_max, 1, 1, true);
                    this._addNumberBox(2, 0, 1, 0, _g.d.ap_supplier_detail._credit_day, 1, 1, true);
                    this._addDateBox(2, 1, 1, 0, _g.d.ap_supplier_detail._credit_date, 1, true);
                    MyLib._myGroupBox _credit_typy = this._addGroupBox(3, 0, 1, 3, 2, _g.d.ap_supplier_detail._credit_status, true);
                    this._addRadioButtonOnGroupBox(0, 0, _credit_typy, _g.d.ap_Ap_resource._open_credit, 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, _credit_typy, _g.d.ap_Ap_resource._close_credit, 1, false);
                    this._addRadioButtonOnGroupBox(0, 2, _credit_typy, _g.d.ap_Ap_resource._stop_credit, 2, false);*/
                    break;
                case _controlTypeEnum.ApDetailBill:
                    /*this._maxColumn = 2;
                    this._table_name = _g.d.ap_supplier_detail._table;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_supplier_detail._pay_bill_code, 1, 2, 1, true, false, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_supplier_detail._keep_chq_code, 1, 2, 1, true, false, true);
                    this._addComboBox(1, 0, _g.d.ap_supplier_detail._pay_bill_date, true, _get_every_date, false);
                    this._addComboBox(1, 1, _g.d.ap_supplier_detail._keep_chq_date, true, _get_every_date, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_supplier_detail._payment_person, 1, 2, 1, true, false, true);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_supplier_detail._keep_money_person, 1, 2, 1, true, false, true);
                    MyLib._myGroupBox _shipping_typy = this._addGroupBox(3, 0, 1, 3, 2, _g.d.ap_supplier_detail._shipping_type, true);
                    this._addRadioButtonOnGroupBox(0, 0, _shipping_typy, "รับเอง", 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, _shipping_typy, "ส่งให้", 1, false);
                    this._addRadioButtonOnGroupBox(0, 2, _shipping_typy, "อื่น", 2, false);*/
                    break;
                case _controlTypeEnum.ApDetailGroup:
                    this._maxColumn = 1;
                    this._table_name = _g.d.ap_supplier_detail._table;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_supplier_detail._group_main, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_supplier_detail._group_sub_1, 1, 2, 1, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_supplier_detail._group_sub_2, 1, 2, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_supplier_detail._group_sub_3, 1, 2, 1, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_supplier_detail._group_sub_4, 1, 2, 1, true, false, true);
                    break;
                case _controlTypeEnum.ApDetailDimension:
                    this._maxColumn = 1;
                    this._table_name = _g.d.ap_supplier_detail._table;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_supplier_detail._dimension_1, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_supplier_detail._dimension_2, 1, 2, 1, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_supplier_detail._dimension_3, 1, 2, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_supplier_detail._dimension_4, 1, 2, 1, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_supplier_detail._dimension_5, 1, 2, 1, true, false, true);
                    break;
            }

            if (MyLib._myGlobal._isDesignMode == false)
            {
                switch (this._controlName)
                {
                    case _controlTypeEnum.Ap:
                        MyLib._myTextBox __getCodeControl = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._code);
                        if (__getCodeControl != null)
                        {
                            __getCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
                            __getCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
                        }
                        MyLib._myTextBox __getTambonControl = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._tambon);
                        if (__getTambonControl != null)
                        {
                            __getTambonControl.textBox.Enter += new EventHandler(textBox_Enter);
                            __getTambonControl.textBox.Leave += new EventHandler(textBox_Leave);
                        }
                        MyLib._myTextBox __getAmperControl = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._amper);
                        if (__getAmperControl != null)
                        {
                            __getAmperControl.textBox.Enter += new EventHandler(textBox_Enter);
                            __getAmperControl.textBox.Leave += new EventHandler(textBox_Leave);
                        }
                        MyLib._myTextBox __getProvinceControl = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._province);
                        if (__getProvinceControl != null)
                        {
                            __getProvinceControl.textBox.Enter += new EventHandler(textBox_Enter);
                            __getProvinceControl.textBox.Leave += new EventHandler(textBox_Leave);
                        }
                        MyLib._myTextBox __getTypeControl = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._ap_type);
                        if (__getTypeControl != null)
                        {
                            __getTypeControl.textBox.Enter += new EventHandler(textBox_Enter);
                            __getTypeControl.textBox.Leave += new EventHandler(textBox_Leave);
                        }

                        // toe add event radio click
                        MyLib._myGroupBox __getAPStatus = (MyLib._myGroupBox)this._getControl(_g.d.ap_supplier._ap_status);
                        if (__getAPStatus != null)
                        {
                            //__getAPStatus.CheckedChanged += new EventHandler(__getAPType_CheckedChanged);
                            foreach (Control __getControlInGroupBox in __getAPStatus.Controls)
                            {
                                if (__getControlInGroupBox.GetType() == typeof(MyLib._myRadioButton))
                                {
                                    MyLib._myRadioButton __getRadioButtonOption = (MyLib._myRadioButton)__getControlInGroupBox;
                                    __getRadioButtonOption.CheckedChanged += new EventHandler(__getAPType_CheckedChanged);
                                }

                            }
                        }

                        MyLib._myTextBox __getPrefixName = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._prefixname);
                        if (__getPrefixName != null)
                        {
                            __getPrefixName.textBox.Leave += new EventHandler(textBoxFullName_Leave);
                        }

                        MyLib._myTextBox __getFirstName = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._firstname);
                        if (__getFirstName != null)
                        {
                            __getFirstName.textBox.Leave += new EventHandler(textBoxFullName_Leave);
                        }

                        MyLib._myTextBox __getLastName = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._lastname);
                        if (__getLastName != null)
                        {
                            __getLastName.textBox.Leave += new EventHandler(textBoxFullName_Leave);
                        }

                        break;
                    case _controlTypeEnum.ApDetail:
                        //พนักงานขาย
                        MyLib._myTextBox __get_sale_code = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._staff_pay_code);
                        __get_sale_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_sale_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get_account_code = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._account_code);
                        __get_account_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_account_code.textBox.Leave += new EventHandler(textBox_Leave);

                        //MyLib._myTextBox __get_passbook_code = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._passbook_code);
                        //__get_passbook_code.textBox.Enter += new EventHandler(textBox_Enter);
                        //__get_passbook_code.textBox.Leave += new EventHandler(textBox_Leave);
                        //เขตการขาย 
                        break;
                    case _controlTypeEnum.ApDetailGroup:
                        // กลุ่ม
                        MyLib._myTextBox __getGroupMainControl = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._group_main);
                        __getGroupMainControl.textBox.Enter += new EventHandler(textBox_Enter);
                        __getGroupMainControl.textBox.Leave += new EventHandler(textBox_Leave);
                        //
                        MyLib._myTextBox __getGroupSub1Control = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._group_sub_1);
                        __getGroupSub1Control.textBox.Enter += new EventHandler(textBox_Enter);
                        __getGroupSub1Control.textBox.Leave += new EventHandler(textBox_Leave);
                        //
                        MyLib._myTextBox __getGroupSub2Control = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._group_sub_2);
                        __getGroupSub2Control.textBox.Enter += new EventHandler(textBox_Enter);
                        __getGroupSub2Control.textBox.Leave += new EventHandler(textBox_Leave);
                        //
                        MyLib._myTextBox __getGroupSub3Control = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._group_sub_3);
                        __getGroupSub3Control.textBox.Enter += new EventHandler(textBox_Enter);
                        __getGroupSub3Control.textBox.Leave += new EventHandler(textBox_Leave);
                        //
                        MyLib._myTextBox __getGroupSub4Control = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._group_sub_4);
                        __getGroupSub4Control.textBox.Enter += new EventHandler(textBox_Enter);
                        __getGroupSub4Control.textBox.Leave += new EventHandler(textBox_Leave);
                        break;
                    case _controlTypeEnum.ApDetailDimension:
                        // มิติ
                        MyLib._myTextBox __getDimension1 = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._dimension_1);
                        __getDimension1.textBox.Enter += new EventHandler(textBox_Enter);
                        __getDimension1.textBox.Leave += new EventHandler(textBox_Leave);
                        //
                        MyLib._myTextBox __getDimension2 = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._dimension_2);
                        __getDimension2.textBox.Enter += new EventHandler(textBox_Enter);
                        __getDimension2.textBox.Leave += new EventHandler(textBox_Leave);
                        //
                        MyLib._myTextBox __getDimension3 = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._dimension_3);
                        __getDimension3.textBox.Enter += new EventHandler(textBox_Enter);
                        __getDimension3.textBox.Leave += new EventHandler(textBox_Leave);
                        //
                        MyLib._myTextBox __getDimension4 = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._dimension_4);
                        __getDimension4.textBox.Enter += new EventHandler(textBox_Enter);
                        __getDimension4.textBox.Leave += new EventHandler(textBox_Leave);
                        //
                        MyLib._myTextBox __getDimension5 = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier_detail._dimension_5);
                        __getDimension5.textBox.Enter += new EventHandler(textBox_Enter);
                        __getDimension5.textBox.Leave += new EventHandler(textBox_Leave);
                        break;
                }
                this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenApControl__textBoxSearch);
                this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenApControl__textBoxChanged);
            }
            this.Dock = DockStyle.Top;
            this.AutoSize = true;
            this.Invalidate();
            this.ResumeLayout();
        }

        // toe
        void __enableFullName(bool __disabled)
        {
            MyLib._myTextBox __getName_1 = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._name_1);
            MyLib._myTextBox __getPrefixName = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._prefixname);
            MyLib._myTextBox __getFirstName = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._firstname);
            MyLib._myTextBox __getLastName = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._lastname);
            if (__disabled == true)
            {
                __getName_1.textBox.Enabled = false;
                __getFirstName.textBox.Enabled = true;
                __getLastName.textBox.Enabled = true;
                __getPrefixName.textBox.Enabled = true;
            }
            else
            {
                __getName_1.textBox.Enabled = true;
                __getFirstName.textBox.Enabled = false;
                __getLastName.textBox.Enabled = false;
                __getPrefixName.textBox.Enabled = false;
            }
        }

        void __getAPType_CheckedChanged(object sender, EventArgs e)
        {
            MyLib._myRadioButton __getRadioButton = (MyLib._myRadioButton)sender;

            if (__getRadioButton.Name.Equals(_g.d.ap_supplier._personality) && __getRadioButton.Checked.Equals(true))
            {
                __enableFullName(true);
            }

            if (__getRadioButton.Name.Equals(_g.d.ap_supplier._juristic_person) && __getRadioButton.Checked.Equals(true))
            {
                __enableFullName(false);
            }
        }

        void textBoxFullName_Leave(object sender, EventArgs e)
        {
            MyLib._myTextBox __getPrefixName = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._prefixname);
            MyLib._myTextBox __getFirstName = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._firstname);
            MyLib._myTextBox __getLastName = (MyLib._myTextBox)this._getControl(_g.d.ap_supplier._lastname);

            string __strName_1 = string.Format("{0}{1} {2}", __getPrefixName.textBox.Text, __getFirstName.textBox.Text, __getLastName.textBox.Text);
            this._setDataStr(_g.d.ap_supplier._name_1, __strName_1);

        }


        void _screenApControl__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ap_supplier._code))
            {
                // autorun
                MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
                string __apCode = __textBox._textFirst.ToUpper();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __apCode + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    string __docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __newArCode = _g.g._getAutoRun(_g.g._autoRunType.เจ้าหนี้, __apCode, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง);
                    this._setDataStr(_g.d.ap_supplier._code, __newArCode, "", true);
                }
                else
                {
                    if (__apCode.Length > 0)
                    {
                        try
                        {
                            string __newApCode = __apCode;
                            DataTable __dt = __myFrameWork._queryShort("select " + _g.d.ap_supplier._code + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "<\'" + __apCode + "z\' order by " + _g.d.ap_supplier._code + " desc limit 1").Tables[0];
                            if (__dt.Rows.Count > 0)
                            {
                                string __getApCode = __dt.Rows[0][_g.d.ap_supplier._code].ToString();
                                if (__getApCode.Length > __apCode.Length)
                                {
                                    string __s1 = __getApCode.Substring(0, __apCode.Length);
                                    if (__s1.Equals(__apCode))
                                    {
                                        string __s2 = __getApCode.Remove(0, __apCode.Length);
                                        int __runningNumber = (int)MyLib._myGlobal._decimalPhase(__s2);
                                        if (__runningNumber > 0)
                                        {
                                            StringBuilder __format = new StringBuilder();
                                            for (int __loop = 0; __loop < __s2.Length; __loop++)
                                            {
                                                __format.Append("0");
                                            }
                                            __newApCode = __s1 + String.Format("{0:" + __format.ToString() + "}", ((decimal)(__runningNumber + 1)));
                                            this._setDataStr(_g.d.ap_supplier._code, __newApCode, "", true);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            else
            {
                if (name.Equals(_g.d.ap_supplier._tambon) ||
                                name.Equals(_g.d.ap_supplier._amper) ||
                                name.Equals(_g.d.ap_supplier._province) ||

                                name.Equals(_g.d.ap_supplier_detail._staff_pay_code) ||
                                 name.Equals(_g.d.ap_supplier_detail._account_code) ||
                                // name.Equals(_g.d.ap_supplier_detail._passbook_code) ||
                                //_controlTypeEnum.ApDetailDimension:
                                name.Equals(_g.d.ap_supplier_detail._dimension_1) ||
                                name.Equals(_g.d.ap_supplier_detail._dimension_2) ||
                                name.Equals(_g.d.ap_supplier_detail._dimension_3) ||
                                name.Equals(_g.d.ap_supplier_detail._dimension_4) ||
                                name.Equals(_g.d.ap_supplier_detail._dimension_5) ||
                                //_controlTypeEnum.ApDetailGroup:
                                name.Equals(_g.d.ap_supplier_detail._group_main) ||
                                name.Equals(_g.d.ap_supplier_detail._group_sub_1) ||
                                name.Equals(_g.d.ap_supplier_detail._group_sub_2) ||
                                name.Equals(_g.d.ap_supplier_detail._group_sub_3) ||
                                name.Equals(_g.d.ap_supplier_detail._group_sub_4)
                            )
                {
                    this._searchTextBox = (TextBox)sender;
                    this._searchName = name;
                    this._search(true);
                }
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full != null)
            {
                this._search_data_full.Visible = false;
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _screenApControl__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void _screenApControl__textBoxSearch(object sender)
        {
            //ค้นหารหัสลูกหนี้
            // ค้นหาหน้าจอ Top
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _search_screen_neme(this._searchName);
            if (this._searchName.Equals(_g.d.ar_customer_detail._account_code))
            {
                this._chartOfAccountScreen.ShowDialog();
            }
            else
            {
                if (this._search_data_full == null || !this._search_data_full._name.Equals(_search_text_new.ToLower()))
                {
                    this._search_data_full = new MyLib._searchDataFull();
                    this._search_data_full._name = _search_text_new;
                    this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    this._search_data_full._dataList._refreshData();

                }
                if (this._searchName.Equals(_g.d.ap_supplier._code.ToLower()))
                {
                    string _where = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'AP\'";
                    MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full, false, true, _where);
                }
                else
                    if (this._searchName.Equals(_g.d.ap_supplier._tambon.ToLower()))
                {
                    string _where = " " + _g.d.erp_tambon._province + "=\'" + this._getDataStr(_g.d.ap_supplier._province) + "\' and " + _g.d.erp_tambon._amper + "=\'" + this._getDataStr(_g.d.ap_supplier._amper) + "\'";
                    MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full, false, true, _where);
                }
                else
                        if (this._searchName.Equals(_g.d.ap_supplier._amper.ToLower()))
                {
                    string _where = " " + _g.d.erp_amper._province + "=\'" + this._getDataStr(_g.d.ap_supplier._province) + "\'";
                    MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full, false, true, _where);
                }
                else
                {
                    MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full, false);
                }
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

        public void _search(Boolean wApning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                switch (_controlName)
                {
                    case _controlTypeEnum.Ap:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._code + "=\'" + this._getDataStr(_g.d.ap_supplier._province) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._province + "=\'" + this._getDataStr(_g.d.ap_supplier._province) + "\' and " + _g.d.erp_amper._code + "=\'" + this._getDataStr(_g.d.ap_supplier._amper) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_tambon._name_1 + " from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._province + "=\'" + this._getDataStr(_g.d.ap_supplier._province) + "\' and " + _g.d.erp_tambon._amper + "=\'" + this._getDataStr(_g.d.ap_supplier._amper) + "\' and " + _g.d.erp_tambon._code + "=\'" + this._getDataStr(_g.d.ap_supplier._tambon) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_type._name_1 + " from " + _g.d.ap_type._table + " where " + _g.d.ap_type._code + "=\'" + this._getDataStr(_g.d.ap_supplier._ap_type) + "\'"));
                        break;
                    case _controlTypeEnum.ApDetail:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._staff_pay_code) + "\'"));
                        //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._name_1 + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._passbook_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._account_code).ToUpper() + "\'"));
                        //_controlTypeEnum.ApDetail
                        //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.Ap_sale_Apea._name_1 + " from " + _g.d.Ap_sale_Apea._table + " where code=\'" + this._getDataStr(_g.d.ap_supplier_detail._Apea_code) + "\'"));
                        //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user_group._name_1 + " from " + _g.d.erp_user_group._table + " where code=\'" + this._getDataStr(_g.d.ap_supplier_detail._sale_code) + "\'"));
                        //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.Ap_logistic_Apea._name_1 + " from " + _g.d.Ap_logistic_Apea._table + " where code=\'" + this._getDataStr(_g.d.ap_supplier_detail._logistic_Apea) + "\'"));
                        break;

                    case _controlTypeEnum.ApDetailDimension:
                        //_controlTypeEnum.ApDetailDimension:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_dimension._name_1 + " from " + _g.d.ap_dimension._table + " where " + _g.d.ap_dimension._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._dimension_1) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_dimension._name_1 + " from " + _g.d.ap_dimension._table + " where " + _g.d.ap_dimension._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._dimension_2) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_dimension._name_1 + " from " + _g.d.ap_dimension._table + " where " + _g.d.ap_dimension._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._dimension_3) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_dimension._name_1 + " from " + _g.d.ap_dimension._table + " where " + _g.d.ap_dimension._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._dimension_4) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_dimension._name_1 + " from " + _g.d.ap_dimension._table + " where " + _g.d.ap_dimension._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._dimension_5) + "\'"));
                        break;
                    case _controlTypeEnum.ApDetailGroup:
                        //_controlTypeEnum.ApDetailGroup
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_group._name_1 + " from " + _g.d.ap_group._table + " where " + _g.d.ap_group._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._group_main) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_group_sub._name_1 + " from " + _g.d.ap_group_sub._table + " where " + _g.d.erp_tambon._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._group_sub_1) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_group_sub._name_1 + " from " + _g.d.ap_group_sub._table + " where " + _g.d.erp_tambon._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._group_sub_2) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_group_sub._name_1 + " from " + _g.d.ap_group_sub._table + " where " + _g.d.erp_tambon._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._group_sub_3) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_group_sub._name_1 + " from " + _g.d.ap_group_sub._table + " where " + _g.d.erp_tambon._code + "=\'" + this._getDataStr(_g.d.ap_supplier_detail._group_sub_4) + "\'"));
                        break;
                }
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                switch (_controlName)
                {
                    case _controlTypeEnum.Ap:
                        if (_searchAndWApning(_g.d.ap_supplier._province, (DataSet)_getData[0], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier._amper, (DataSet)_getData[1], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier._tambon, (DataSet)_getData[2], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier._ap_type, (DataSet)_getData[3], wApning) == false) { }
                        break;
                    case _controlTypeEnum.ApDetail:
                        if (_searchAndWApning(_g.d.ap_supplier_detail._staff_pay_code, (DataSet)_getData[0], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier_detail._account_code, (DataSet)_getData[1], wApning) == false) { }
                        // if (_searchAndWApning(_g.d.ap_supplier_detail._passbook_code, (DataSet)_getData[1], wApning) == false) { }
                        //_controlTypeEnum.ApDetail
                        // if (_searchAndWApning(_g.d.ap_supplier_detail._Apea_code, (DataSet) _getData[0], wApning) == false) { }
                        // if (_searchAndWApning(_g.d.ap_supplier_detail._sale_code, (DataSet) _getData[1], wApning) == false) { }
                        // if (_searchAndWApning(_g.d.ap_supplier_detail._logistic_Apea, (DataSet) _getData[2], wApning) == false) { }
                        break;
                    case _controlTypeEnum.ApDetailDimension:
                        //_controlTypeEnum.ApDetailDimension:
                        if (_searchAndWApning(_g.d.ap_supplier_detail._dimension_1, (DataSet)_getData[0], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier_detail._dimension_2, (DataSet)_getData[1], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier_detail._dimension_3, (DataSet)_getData[2], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier_detail._dimension_4, (DataSet)_getData[3], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier_detail._dimension_5, (DataSet)_getData[4], wApning) == false) { }
                        break;
                    case _controlTypeEnum.ApDetailGroup:
                        //_controlTypeEnum.ApDetailGroup
                        if (_searchAndWApning(_g.d.ap_supplier_detail._group_main, (DataSet)_getData[12], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier_detail._group_sub_1, (DataSet)_getData[13], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier_detail._group_sub_2, (DataSet)_getData[14], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier_detail._group_sub_3, (DataSet)_getData[15], wApning) == false) { }
                        if (_searchAndWApning(_g.d.ap_supplier_detail._group_sub_3, (DataSet)_getData[16], wApning) == false) { }
                        break;
                }
            }
            catch
            {
            }
        }

        bool _searchAndWApning(string fieldName, DataSet _datApesult, Boolean wApning)
        {
            bool __result = false;
            if (_datApesult.Tables[0].Rows.Count > 0)
            {
                string __getData = _datApesult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true);

            }
            else
            {
                if (wApning == true)
                    this._setDataStr(fieldName, "", "", true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_datApesult.Tables[0].Rows.Count == 0 && wApning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("เอกสารเลขที่ หรือ ยอดตัดจ่าย ห้ามว่าง"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_data_full.Visible)
                {
                    this._search_data_full.Focus();
                    this._search_data_full._firstFocus();
                }
            }
        }

        string _search_screen_neme(string _name)
        {
            switch (_name)
            {
                case "code": return _g.g._search_screen_erp_doc_format;
                case "doc_format_code": return _g.g._search_screen_erp_doc_format;
                case "tambon": return _g.g._screen_erp_tambon;
                case "amper": return _g.g._screen_erp_amper;
                case "province": return _g.g._screen_erp_province;
                case "ap_type": return _g.g._search_master_ap_type;
                case "passbook_code": return _g.g._search_screen_สมุดเงินฝาก;
                //_controlTypeEnum.ApDetail
                /*case "Apea_code": return _g.g._search_master_ap_Apea_code;
                case "sale_code": return _g.g._search_screen_erp_user_group;
                case "logistic_Apea": return _g.g._search_master_ap_logistic_Apea;
                case "credit_group_code": return _g.g._search_master_ap_credit_group;
                case "credit_person": return _g.g._search_screen_erp_user_group;
                //_controlTypeEnum.ApDetailBill
                case "pay_bill_code": return _g.g._search_master_ap_pay_bill_condition;
                case "keep_chq_code": return _g.g._search_master_ap_keep_money;
                case "payment_person": return _g.g._search_screen_erp_user_group; ;
                case "keep_money_person": return _g.g._search_screen_erp_user_group;*/
                //_controlTypeEnum.ApDetailDimension:

                case "staff_pay_code": return _g.g._search_screen_erp_user;
                case "dimension_1": return _g.g._search_master_ap_dimension;
                case "dimension_2": return _g.g._search_master_ap_dimension;
                case "dimension_3": return _g.g._search_master_ap_dimension;
                case "dimension_4": return _g.g._search_master_ap_dimension;
                case "dimension_5": return _g.g._search_master_ap_dimension;
                case "group_main": return _g.g._search_master_ap_group;
                case "group_sub_1": return _g.g._search_master_ap_group_sub;
                case "group_sub_2": return _g.g._search_master_ap_group_sub;
                case "group_sub_3": return _g.g._search_master_ap_group_sub;
                case "group_sub_4": return _g.g._search_master_ap_group_sub;
            }
            return "";
        }

        public string __newApCode { get; set; }
    }

    public enum _controlTypeEnum
    {
        /// <summApy>
        /// Ap : ลูกหนี
        /// </summApy>
        Ap,
        /// <summApy>
        /// ApDetail : รายละเอียดลูกหนี้
        /// </summApy>
        ApDetail,
        /// <summApy>
        /// ApDetail : รายละเอียดลูกหนี้วางบิล
        /// </summApy>
        ApDetailBill,
        /// <summApy>
        /// ApDetail : รายละเอียดลูกหนี้เคดิต
        /// </summApy>
        ApDetailCredit,
        /// <summApy>
        /// ApDetail : รายละเอียดกลุ่มลูกหนี้
        /// </summApy>
        ApDetailGroup,
        /// <summApy>
        /// ApDetail : รายละเอียดมิติลูกหนี้
        /// </summApy>
        ApDetailDimension,
    }
}
