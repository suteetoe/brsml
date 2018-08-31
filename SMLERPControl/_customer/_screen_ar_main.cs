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

namespace SMLERPControl._customer
{
    public partial class _screen_ar_main : MyLib._myScreen
    {
        private _controlTypeEnum _controlTypeTemp;
        _g._searchChartOfAccountDialog _chartOfAccountScreen = null;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        string[] _get_every_date = { "no_select", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
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
            if (this._searchName.Equals(_g.d.ar_customer_detail._account_code)) this._setDataStr(_g.d.ar_customer_detail._account_code, __accountCode);
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
        public _screen_ar_main()
        {
            //this._build();
            this._chartOfAccountScreen = new _g._searchChartOfAccountDialog();
            this._chartOfAccountScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_chartOfAccountScreen__searchEnterKeyPress);
            this._chartOfAccountScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_chartOfAccount_gridData__mouseClick);

        }

        void _build()
        {
            int __row = 0;
            this.SuspendLayout();
            this._reset();
            switch (_controlName)
            {
                case _controlTypeEnum.Ar:
                    {
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                        {

                            this._maxColumn = 2;
                            this._table_name = _g.d.ar_customer._table;
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._code, 1, 1, 1, true, false, false);
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._name_1, 2, 0, 0, true, false, false);
                            this._addTextBox(__row, 0, 3, 0, _g.d.ar_customer._address, 2, 2, 0, true, false, true);
                            __row += 3;
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._telephone, 1, 255, 0, true, false);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer._fax, 1, 255, 0, true, false);
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._email, 1, 255, 0, true, false);
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._website, 2, 1, 0, true, false, true); //เว็บไซด์
                            MyLib._myGroupBox _ap_status_groupbox = this._addGroupBox(__row, 0, 1, 2, 1, _g.d.ar_customer._ar_status, true);
                            this._addRadioButtonOnGroupBox(0, 0, _ap_status_groupbox, _g.d.ar_customer._personality, 0, true);
                            this._addRadioButtonOnGroupBox(0, 1, _ap_status_groupbox, _g.d.ar_customer._juristic_person, 1, false);
                            MyLib._myGroupBox _status_groupbox = this._addGroupBox(__row++, 1, 1, 2, 1, _g.d.ar_customer._status, true);
                            this._addRadioButtonOnGroupBox(0, 0, _status_groupbox, _g.d.ar_customer._active, 0, true);
                            this._addRadioButtonOnGroupBox(0, 1, _status_groupbox, _g.d.ar_customer._inactive, 1, false);
                            __row++;
                            //this._addComboBox(__row, 0, _g.d.ar_customer_detail._branch_type, true, _g.g._ap_ar_branch_type, true);
                            //this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._branch_code, 1, 0, 0, true, false, true);


                        }
                        else
                        {
                            bool __allowEmpty = true;

                            this._maxColumn = 2;
                            this._table_name = _g.d.ar_customer._table;
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._code, 1, 1, 1, true, false, false);
                            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            {
                                this._addTextBox(__row, 1, 1, 0, _g.d.ar_customer._code_old, 1, 1, 0, true, false, true);
                                __allowEmpty = false;
                            }
                            __row++;
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._name_1, 2, 0, 0, true, false, false);
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._name_2, 2, 0, 0, true, false, true);
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._name_eng_1, 2, 0, 0, true, false, true);
                            this._addTextBox(__row++, 0, 2, 0, _g.d.ar_customer._address, 2, 2, 0, true, false, true);
                            __row++;
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._province, 1, 2, 1, true, false, __allowEmpty);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer._amper, 1, 2, 1, true, false, __allowEmpty);
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._tambon, 1, 2, 1, true, false, __allowEmpty);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer._zip_code, 1, 255, 0, true, false, true);
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._country, 1, 255, 0, true, false);
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._telephone, 1, 255, 0, true, false);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer._fax, 1, 255, 0, true, false);
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._sms_phonenumber, 1, 10, 0, true, false);
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._email, 1, 255, 0, true, false);
                            Boolean _arEmtryType = true;
                            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                            {
                                _arEmtryType = false;
                            }
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer._ar_type, 1, 1, 1, true, false, _arEmtryType); // ประเภทผู้จำหน่าย
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._website, 2, 1, 0, true, false, true); //เว็บไซด์
                            MyLib._myGroupBox _ap_status_groupbox = this._addGroupBox(__row, 0, 1, 2, 1, _g.d.ar_customer._ar_status, true);
                            this._addRadioButtonOnGroupBox(0, 0, _ap_status_groupbox, _g.d.ar_customer._personality, 0, true);
                            this._addRadioButtonOnGroupBox(0, 1, _ap_status_groupbox, _g.d.ar_customer._juristic_person, 1, false);
                            MyLib._myGroupBox _status_groupbox = this._addGroupBox(__row++, 1, 1, 2, 1, _g.d.ar_customer._status, true);
                            this._addRadioButtonOnGroupBox(0, 0, _status_groupbox, _g.d.ar_customer._active, 0, true);
                            this._addRadioButtonOnGroupBox(0, 1, _status_groupbox, _g.d.ar_customer._inactive, 1, false);
                            __row++;
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._prefixname, 1, 0, 0, true, false, true);
                            this._addComboBox(__row++, 1, _g.d.ar_customer._price_level, true, new string[] { _g.d.ar_customer._price_level_0, _g.d.ar_customer._price_level_1, _g.d.ar_customer._price_level_2, _g.d.ar_customer._price_level_3, _g.d.ar_customer._price_level_4, _g.d.ar_customer._price_level_5, _g.d.ar_customer._price_level_6, _g.d.ar_customer._price_level_7, _g.d.ar_customer._price_level_8, _g.d.ar_customer._price_level_9 }, false);
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._first_name, 1, 0, 0, true, false, true);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer._last_name, 1, 0, 0, true, false, true);

                            this._addDateBox(__row, 0, 1, 0, _g.d.ar_customer._birth_day, 1, true);
                            this._addDateBox(__row++, 1, 1, 0, _g.d.ar_customer._register_date, 1, true);

                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._tax_id, 1, 0, 0, true, false, true, false);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer._card_id, 1, 0, 0, true, false, true, false);
                            this._addComboBox(__row, 0, _g.d.ar_customer._branch_type, 1, true, _g.g._ap_ar_branch_type, true, _g.d.ar_customer._branch_type, true, false);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer._branch_code, 1, 0, 0, true, false, true, false);
                            //if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            //{
                            //    this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._arm_code, 1, 10, 1, true, false);
                            //    this._addComboBox(__row++, 1, _g.d.ar_customer._arm_tier, true, new string[] { _g.d.ar_customer._tier_0, _g.d.ar_customer._tier_1, _g.d.ar_customer._tier_2, _g.d.ar_customer._tier_3, _g.d.ar_customer._tier_4 }, false);
                            //    this._addCheckBox(__row, 0, _g.d.ar_customer._arm_register, false, true);
                            //    this._addDateBox(__row, 1, 1, 1, _g.d.ar_customer._arm_register_date, 1, true);
                            //    __row++;
                            //    this._addCheckBox(__row, 0, _g.d.ar_customer._arm_approve, false, true);
                            //    this._addDateBox(__row++, 1, 1, 1, _g.d.ar_customer._arm_approve_date, 1, true);
                            //    this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._ar_code_main, 1, 1, 4, true, false, true);
                            //    if (_g.g._companyProfile._customer_by_branch)
                            //    {
                            //        this._addTextBox(__row, 1, 1, 0, _g.d.ar_customer._ar_branch_code, 1, 1, 4, true, false, true);
                            //    }
                            //    __row++;
                            //    this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._nfc_id, 1, 1, 0, true, false, true);
                            //}
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer._ar_code_main, 1, 1, 4, true, false, true);
                            if (_g.g._companyProfile._customer_by_branch)
                            {
                                this._addTextBox(__row, 1, 1, 0, _g.d.ar_customer._ar_branch_code, 1, 1, 4, true, false, true);
                            }
                            __row++;
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._nfc_id, 1, 1, 0, true, false, true);

                            this._addTextBox(__row++, 0, 2, 2, _g.d.ar_customer._remark, 2, 0, 0, true, false, true, true);
                            __row++;
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer._interco, 1, 0, 0, true, false);
                            this._setUpper(_g.d.ar_customer._interco);


                            if (MyLib._myGlobal._OEMVersion == "tvdirect")
                            {
                                //this._enabedControl(_g.d.ar_customer._code, false);
                                if (_g.g._companyProfile._manual_customer_code == false)
                                {
                                    MyLib._myTextBox __textCode = (MyLib._myTextBox)this._getControl(_g.d.ar_customer._code);
                                    __textCode.textBox.ReadOnly = true;
                                }
                            }

                            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            {
                                if (_g.g._companyProfile._customer_by_branch && _g.g._companyProfile._change_branch_code == false)
                                {
                                    this._enabedControl(_g.d.ar_customer._ar_branch_code, false);
                                }
                            }
                        }
                    }
                    break;
                case _controlTypeEnum.ArDetail:
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.ar_customer_detail._table;
                        this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer_detail._tax_id, 1, 0, 0, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._card_id, 1, 0, 0, true, false, true);
                        this._addComboBox(__row, 0, _g.d.ar_customer_detail._branch_type, true, _g.g._ap_ar_branch_type, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._branch_code, 1, 0, 0, true, false, true);

                    }
                    else
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.ar_customer_detail._table;
                        this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer_detail._area_code, 1, 2, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._sale_code, 1, 2, 1, true, false, true);
                        //this._addTextBox(1, 0, 1, 0, _g.d.ar_customer_detail._logistic_area, 1, 2, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer_detail._trade_license, 1, 0, 0, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._vat_license, 1, 0, 0, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer_detail._tax_id, 1, 0, 0, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._card_id, 1, 0, 0, true, false, true);
                        this._addComboBox(__row, 0, _g.d.ar_customer_detail._branch_type, true, _g.g._ap_ar_branch_type, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._branch_code, 1, 0, 0, true, false, true);


                        //this._addTextBox(2, 1, 1, 0, _g.d.ar_customer_detail._tax_rate, 1, 0, 0, true, false, true);
                        this._addCheckBox(__row++, 0, _g.d.ar_customer_detail._set_tax_type, false, true);
                        this._addComboBox(__row, 0, _g.d.ar_customer_detail._tax_type, true, _g.g._po_so_tax_type, true);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._tax_rate, 1, 1, true);
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ar_customer_detail._account_code, 1, 2, 4, true, false, true);
                        if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLPOSLite)
                        {
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer_detail._discount_item, 1, 0, 0, true, false, true);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._discount_bill, 1, 0, 0, true, false, true);
                        }
                        // this._addTextBox(4, 1, 1, 0, _g.d.ar_customer_detail._passbook_code, 1, 2, 1, true, false, true);
                        if (MyLib._myGlobal._OEMVersion.Equals("imex"))
                        {
                            this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer_detail._area_paybill, 1, 2, 1, true, false, true);
                        }
                        this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer_detail._logistic_area, 1, 2, 1, true, false, true);
                    }
                    break;
                case _controlTypeEnum.ArDetailCredit:
                    {
                        __row = 0;
                        this._maxColumn = 2;
                        this._table_name = _g.d.ar_customer_detail._table;
                        //this._addTextBox(0, 0, 1, 0, _g.d.ar_customer_detail._credit_group_code, 1, 25, 1, true, false, true);
                        //this._addTextBox(0, 1, 1, 0, _g.d.ar_customer_detail._credit_person, 1, 2, 1, true, false, true);
                        this._addNumberBox(__row, 0, 1, 0, _g.d.ar_customer_detail._credit_money, 1, 1, true);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._credit_money_max, 1, 1, true);
                        this._addNumberBox(__row, 0, 1, 0, _g.d.ar_customer_detail._credit_day, 1, 1, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._credit_date, 1, true);
                        MyLib._myGroupBox _credit_typy = this._addGroupBox(__row, 0, 1, 3, 2, _g.d.ar_customer_detail._credit_status, true);
                        this._addRadioButtonOnGroupBox(0, 0, _credit_typy, _g.d.ar_customer_detail._open_credit, 0, true);
                        this._addRadioButtonOnGroupBox(0, 1, _credit_typy, _g.d.ar_customer_detail._close_credit, 1, false);
                        this._addRadioButtonOnGroupBox(0, 2, _credit_typy, _g.d.ar_customer_detail._stop_credit, 2, false);

                        __row += 2;
                        this._addDateBox(__row++, 0, 1, 0, _g.d.ar_customer_detail._close_credit_date, 1, true);

                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                        {
                            /*this._addComboBox(4, 0, _g.d.ar_customer_detail._close_reason, 2, true, new string[] { "ผิดเงื่อนไข ชำระเงิน", "เช็คคืน" }, false, _g.d.ar_customer_detail._close_reason, false, false);
                            Control __comboboxReason = this._getControl(_g.d.ar_customer_detail._close_reason);
                            if (__comboboxReason != null)
                            {
                                MyLib._myComboBox __comboReason = (MyLib._myComboBox)__comboboxReason;
                                __comboReason.DropDownStyle = ComboBoxStyle.DropDown;
                            }*/
                            MyLib._addLabelReturn __label = this._addLabel(__row, 0, ":", _g.d.ar_customer_detail._close_reason, _g.d.ar_customer_detail._close_reason);
                            this._addCheckBox(__row++, 0, _g.d.ar_customer_detail._close_reason_1, false, true);
                            this._addCheckBox(__row++, 0, _g.d.ar_customer_detail._close_reason_2, false, true);
                            this._addCheckBox(__row++, 0, _g.d.ar_customer_detail._close_reason_3, false, true);
                            this._addCheckBox(__row++, 0, _g.d.ar_customer_detail._close_reason_4, false, true);
                            this._addTextBox(__row++, 0, 2, 0, _g.d.ar_customer_detail._close_reason, 2, 0, 0, false, false, true);

                        }
                        else
                            this._addTextBox(__row, 0, 2, 0, _g.d.ar_customer_detail._close_reason, 2, 0, 0, true, false, true);
                    }
                    break;
                case _controlTypeEnum.ArDetailBill:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ar_customer_detail._table;
                    //this._addTextBox(0, 0, 1, 0, _g.d.ar_customer_detail._pay_bill_code, 1, 2, 1, true, false, true);
                    //this._addTextBox(0, 1, 1, 0, _g.d.ar_customer_detail._keep_chq_code, 1, 2, 1, true, false, true);
                    this._addComboBox(0, 0, _g.d.ar_customer_detail._pay_bill_date, true, _get_every_date, false);
                    this._addComboBox(0, 1, _g.d.ar_customer_detail._keep_chq_date, true, _get_every_date, false);
                    //this._addTextBox(2, 0, 1, 0, _g.d.ar_customer_detail._payment_person, 1, 2, 1, true, false, true);
                    //this._addTextBox(2, 1, 1, 0, _g.d.ar_customer_detail._keep_money_person, 1, 2, 1, true, false, true);
                    MyLib._myGroupBox _shipping_typy = this._addGroupBox(1, 0, 1, 3, 2, _g.d.ar_customer_detail._shipping_type, true);
                    this._addRadioButtonOnGroupBox(0, 0, _shipping_typy, _g.d.ar_customer_detail._to_be_responsible, 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, _shipping_typy, _g.d.ar_customer_detail._to_support, 1, false);
                    this._addRadioButtonOnGroupBox(0, 2, _shipping_typy, _g.d.ar_customer_detail._other, 2, false);
                    break;
                case _controlTypeEnum.ArDetailGroup:
                    this._maxColumn = 1;
                    this._table_name = _g.d.ar_customer_detail._table;
                    this._addTextBox(0, 0, 1, 0, _g.d.ar_customer_detail._group_main, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.ar_customer_detail._group_sub_1, 1, 2, 1, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ar_customer_detail._group_sub_2, 1, 2, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ar_customer_detail._group_sub_3, 1, 2, 1, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ar_customer_detail._group_sub_4, 1, 2, 1, true, false, true);
                    break;
                case _controlTypeEnum.ArDetailDimension:
                    this._maxColumn = 1;
                    this._table_name = _g.d.ar_customer_detail._table;
                    this._addTextBox(0, 0, 1, 0, _g.d.ar_customer_detail._dimension_1, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.ar_customer_detail._dimension_2, 1, 2, 1, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ar_customer_detail._dimension_3, 1, 2, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ar_customer_detail._dimension_4, 1, 2, 1, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ar_customer_detail._dimension_5, 1, 2, 1, true, false, true);
                    break;
                case _controlTypeEnum.ArDealer:
                    // toe for sml healty
                    //this._maxColumn = 2;

                    __row = 0;
                    this._table_name = _g.d.ar_dealer._table;

                    Boolean _is_dealer_code_empty = true;
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLHealthy)
                    {
                        _is_dealer_code_empty = false;
                    }
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ar_dealer._code, 1, 1, 0, true, false, _is_dealer_code_empty);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ar_dealer._regist_date, 1, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ar_dealer._expire_date, 1, true);
                    this._addCheckBox(__row++, 0, _g.d.ar_dealer._is_staff, false, true, false);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ar_dealer._asso_code, 1, 1, 0, true, false, true);
                        this._addDateBox(__row, 0, 1, 0, _g.d.ar_dealer._asso_regist_date, 1, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ar_dealer._asso_expire_date, 1, true);
                    }
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLHealthy ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ar_dealer._ar_code, 1, 1, 1, true, false, false);
                    }



                    break;
                case _controlTypeEnum.ArDealerHealthy:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ar_dealer._table;
                    this._addTextBox(0, 0, 0, 0, _g.d.ar_dealer._branch_code, 1, 0, 1, true, false, true, true);
                    MyLib._myGroupBox __status_SEX = this._addGroupBox(1, 0, 1, 2, 2, _g.d.ar_dealer._sex, true);
                    this._addRadioButtonOnGroupBox(0, 0, __status_SEX, _g.d.ar_dealer._sex_women, 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, __status_SEX, _g.d.ar_dealer._sex_man, 1, false);
                    this._addTextBox(3, 0, 0, 0, _g.d.ar_dealer._nationality, 1, 0, 1, true, false, true, true);
                    this._addComboBox(3, 1, _g.d.ar_dealer._mstatus, true, new string[] { _g.d.ar_dealer._mstatus_single, _g.d.ar_dealer._mstatus_married, _g.d.ar_dealer._mstatus_divorce }, true);
                    this._addTextBox(4, 0, 0, 0, _g.d.ar_dealer._blood_group, 1, 0, 0, true, false, true, true);
                    this._addTextBox(4, 1, 0, 0, _g.d.ar_dealer._address, 2, 0, 0, true, false, true, true);
                    this._addTextBox(5, 0, 0, 0, _g.d.ar_dealer._moo, 1, 0, 0, true, false, true, true);
                    this._addTextBox(5, 1, 0, 0, _g.d.ar_dealer._floor, 1, 0, 0, true, false, true, true);
                    this._addTextBox(6, 0, 0, 0, _g.d.ar_dealer._room_no, 1, 0, 0, true, false, true, true);
                    this._addTextBox(6, 1, 0, 0, _g.d.ar_dealer._soi, 1, 0, 0, true, false, true, true);
                    this._addTextBox(7, 0, 0, 0, _g.d.ar_dealer._home_phone_extend, 1, 0, 0, true, false, true, true);
                    this._addTextBox(7, 1, 0, 0, _g.d.ar_dealer._off_phone, 1, 0, 0, true, false, true, true);
                    this._addTextBox(8, 0, 0, 0, _g.d.ar_dealer._off_phone_extend, 1, 0, 0, true, false, true, true);
                    this._addTextBox(8, 1, 0, 0, _g.d.ar_dealer._mobile_phone, 1, 0, 0, true, false, true, true);


                    break;
                case _controlTypeEnum.ArDetailHealthy:
                    // top เพิ่ม ขึ้นมาสำหรับ เลือก สมาชิก ของ Healthy
                    this._maxColumn = 2;
                    this._table_name = _g.d.ar_customer._table;
                    this._addTextBox(0, 0, 1, 0, _g.d.ar_customer._code, 1, 1, 1, false, false, false);
                    this._addTextBox(0, 0, 1, 0, _g.d.ar_customer._name_1, 2, 0, 0, true, false, false);
                    this._addTextBox(1, 0, 2, 0, _g.d.ar_customer._address, 2, 2, 0, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ar_customer._province, 1, 2, 1, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ar_customer._amper, 1, 2, 1, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ar_customer._tambon, 1, 2, 1, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ar_customer._zip_code, 1, 255, 0, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.ar_customer._telephone, 1, 255, 0, true, false);
                    this._addTextBox(5, 1, 1, 0, _g.d.ar_customer._fax, 1, 255, 0, true, false);
                    this._addTextBox(6, 0, 1, 0, _g.d.ar_customer._email, 1, 255, 0, true, false);
                    this._addTextBox(6, 1, 1, 0, _g.d.ar_customer._ar_type, 1, 1, 1, true, false); // ประเภทผู้จำหน่าย
                    this._addTextBox(7, 0, 1, 0, _g.d.ar_customer._website, 2, 1, 0, true, false, true); //เว็บไซด์
                    MyLib._myGroupBox _ap_healthy_status_groupbox = this._addGroupBox(9, 0, 1, 2, 1, _g.d.ar_customer._ar_status, true);
                    this._addRadioButtonOnGroupBox(0, 0, _ap_healthy_status_groupbox, _g.d.ar_customer._personality, 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, _ap_healthy_status_groupbox, _g.d.ar_customer._juristic_person, 1, false);
                    MyLib._myGroupBox _status_healthy_groupbox = this._addGroupBox(9, 1, 1, 2, 1, _g.d.ar_customer._status, true);
                    this._addRadioButtonOnGroupBox(0, 0, _status_healthy_groupbox, _g.d.ar_customer._active, 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, _status_healthy_groupbox, _g.d.ar_customer._inactive, 1, false);
                    this._addTextBox(11, 0, 1, 0, _g.d.ar_customer._prefixname, 1, 0, 0, true, false, true);
                    this._addTextBox(12, 0, 1, 0, _g.d.ar_customer._first_name, 1, 0, 0, true, false, true);
                    this._addTextBox(12, 1, 1, 0, _g.d.ar_customer._last_name, 1, 0, 0, true, false, true);

                    break;
                case _controlTypeEnum.ArTaxOnly:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ar_customer_detail._table;
                    this._addTextBox(__row, 0, 1, 0, _g.d.ar_customer_detail._tax_id, 1, 0, 0, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._card_id, 1, 0, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ar_customer_detail._branch_type, true, _g.g._ap_ar_branch_type, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ar_customer_detail._branch_code, 1, 0, 0, true, false, true);
                    break;

                case _controlTypeEnum.Customer:
                    this._maxColumn = 2;
                    //this._table_name = _g.d.ar_customer_detail._table;

                    //this._addTextBox(1, 0, 1, 0, _g.d.ar_customer_detail._ar_channel_code, 1, 0, 1, true, false, true);
                    //this._addTextBox(1, 1, 1, 0, _g.d.ar_customer_detail._customer_type_code, 1, 0, 1, true, false, true);
                    //this._addTextBox(2, 0, 1, 0, _g.d.ar_customer_detail._ar_sub_type_1_code, 1, 0, 1, true, false, true);
                    //this._addTextBox(2, 1, 1, 0, _g.d.ar_customer_detail._ar_vehicle_code, 1, 0, 1, true, false, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ar_customer_detail._ar_equipment_code, 1, 0, 1, true, false, true);
                    //this._addTextBox(3, 1, 1, 0, _g.d.ar_customer_detail._ar_sub_equipment, 1, 0, 1, true, false, true);
                    //this._addTextBox(4, 0, 1, 0, _g.d.ar_customer_detail._ar_location_type_code, 1, 0, 1, true, false, true);
                    // this._addTextBox(1, 0, 1, 0, _g.d.ar_customer_detail._ar_project_code, 1, 0, 1, true, false, true,);
                    this._addComboBox(1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_project_code_name, 1, true, new string[] { "เลือกโครงการ", "ARM ค้าส่ง", "ARM ค้าปลีก", "ARM ON-Premise", "ARM Special Channel", "โครงการ SINGHA HAPPY", "โครงการ SINGHA EXCLUSIVE", "โครงการ SINGHA NORMAL", "MODERN TRADE", "LOCAL MODERN TRADE", "OTHER" }, false, "ar_customer_detail.ar_project_code_name", false, true);
                 //   this._addComboBox(1, 1, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._arm_tier, 1, true, new string[] { _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._tier_0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._tier_1, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._tier_2, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._tier_3, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._tier_4 }, false, "arm_tier", true, false);

                    this._addTextBox(2, 0, 1, 0, _g.d.ar_customer._table + "." + _g.d.ar_customer._arm_code, 1, 10, 1, true, false, true, false);
                    this._addComboBox(2, 1, _g.d.ar_customer._table + "." + _g.d.ar_customer._arm_tier, 1, true, new string[] { _g.d.ar_customer._table + "." + _g.d.ar_customer._tier_0, _g.d.ar_customer._table + "." + _g.d.ar_customer._tier_1, _g.d.ar_customer._table + "." + _g.d.ar_customer._tier_2, _g.d.ar_customer._table + "." + _g.d.ar_customer._tier_3, _g.d.ar_customer._table + "." + _g.d.ar_customer._tier_4 }, false, "ar_customer.arm_tier", true, false);
                    this._addCheckBox(3, 0, _g.d.ar_customer._table + "." + _g.d.ar_customer._arm_register, false, true, false, false, "ar_customer.arm_register");
                    this._addDateBox(3, 1, 1, 1, _g.d.ar_customer._table + "." + _g.d.ar_customer._arm_register_date, 1, true, false, false);

                    this._addCheckBox(4, 0, _g.d.ar_customer._table + "." + _g.d.ar_customer._arm_approve, false, true, false, false, "ar_customer.arm_approve");
                    this._addDateBox(4, 1, 1, 1, _g.d.ar_customer._table + "." + _g.d.ar_customer._arm_approve_date, 1, true, false, false);
                    //this._addTextBox(5, 0, 1, 0,_g.d.ar_customer._table+"."+_g.d.ar_customer._ar_code_main, 1, 1, 4, true, false, true);
                    //if (_g.g._companyProfile._customer_by_branch)
                    //{
                    //    this._addTextBox(5, 1, 1, 0,_g.d.ar_customer._table+"."+_g.d.ar_customer._ar_branch_code, 1, 1, 4, true, false, true);
                    //}

                    //this._addTextBox(6, 0, 1, 0,_g.d.ar_customer._table+"."+_g.d.ar_customer._nfc_id, 1, 1, 0, true, false, true);





                    //this._addTextBox(2, 1, 1, 0, "รหัสโครงการ", 1, 0, 0, false, false, true, false);
                    MyLib._addLabelReturn __labelcustomer = this._addLabel(5, 0, "", _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_customer_channel, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_customer_channel);
                    this._addTextBox(6, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype1_code, 1, 0, 1, true, false, true);
                    this._addTextBox(7, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype2_code, 1, 0, 1, true, false, true);
                    this._addTextBox(8, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype3_code, 1, 0, 1, true, false, true);
                    this._addTextBox(9, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype4_code, 1, 0, 1, true, false, true);
                    this._addTextBox(10, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype5_code, 1, 0, 1, true, false, true);
                    this._addTextBox(10, 1, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._sub_ar_shoptype5_code, 1, 0, 1, true, false, true);
                    this._addTextBox(11, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype6_code, 1, 0, 1, true, false, true);
                    this._addTextBox(12, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype7_code, 1, 0, 1, true, false, true);
                    this._addTextBox(13, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._latitude, 1, 0, 0, true, false, true);
                    this._addTextBox(13, 1, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._longitude, 1, 0, 0, true, false, true);
                    this._addTextBox(14, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._line_id, 1, 0, 0, true, false, true);
                    this._addTextBox(14, 1, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._facebook, 1, 0, 0, true, false, true);
                    this._addTextBox(15, 0, 1, 0, _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._br_cust_code, 1, 0, 1, true, false, true);

                    Control arm_approve_Control = this._getControl(_g.d.ar_customer._table + "." + _g.d.ar_customer._arm_approve);
                    Control arm_approve_date_codeControl = this._getControl(_g.d.ar_customer._table + "." + _g.d.ar_customer._arm_approve_date);
                    Control arm_register_date_codeControl = this._getControl(_g.d.ar_customer._table + "." + _g.d.ar_customer._arm_register_date);
                    arm_approve_Control.Enabled = false;
                    arm_approve_date_codeControl.Enabled = false;
                    arm_register_date_codeControl.Enabled = false;


                    break;
            }
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenArControl__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenArControl__textBoxSearch);
            this._checkBoxChanged += _screen_ar_main__checkBoxChanged;
            this._comboBoxSelectIndexChanged += _screen_ar_main__comboBoxSelectIndexChanged;
            this.Dock = DockStyle.Top;
            this.AutoSize = true;
            switch (this._controlName)
            {
                case _controlTypeEnum.Ar:
                    MyLib._myTextBox __getCodeControl = (MyLib._myTextBox)this._getControl(_g.d.ar_customer._code);
                    if (__getCodeControl != null)
                    {
                        __getCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
                        __getCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
                    }
                    MyLib._myTextBox __getTambonControl = (MyLib._myTextBox)this._getControl(_g.d.ar_customer._tambon);
                    if (__getTambonControl != null)
                    {
                        __getTambonControl.textBox.Enter += new EventHandler(textBox_Enter);
                        __getTambonControl.textBox.Leave += new EventHandler(textBox_Leave);
                    }
                    MyLib._myTextBox __getAmperControl = (MyLib._myTextBox)this._getControl(_g.d.ar_customer._amper);
                    if (__getAmperControl != null)
                    {
                        __getAmperControl.textBox.Enter += new EventHandler(textBox_Enter);
                        __getAmperControl.textBox.Leave += new EventHandler(textBox_Leave);
                    }
                    MyLib._myTextBox __getProvinceControl = (MyLib._myTextBox)this._getControl(_g.d.ar_customer._province);
                    if (__getProvinceControl != null)
                    {
                        __getProvinceControl.textBox.Enter += new EventHandler(textBox_Enter);
                        __getProvinceControl.textBox.Leave += new EventHandler(textBox_Leave);
                    }
                    MyLib._myTextBox __getArTypeControl = (MyLib._myTextBox)this._getControl(_g.d.ar_customer._ar_type);
                    if (__getArTypeControl != null)
                    {
                        __getArTypeControl.textBox.Enter += new EventHandler(textBox_Enter);
                        __getArTypeControl.textBox.Leave += new EventHandler(textBox_Leave);
                    }
                    break;
                case _controlTypeEnum.ArDetail:
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
                    {
                        MyLib._myTextBox __get_area_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._area_code);
                        __get_area_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_area_code.textBox.Leave += new EventHandler(textBox_Leave);
                        //พนักงานขาย
                        MyLib._myTextBox __get_sale_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._sale_code);
                        __get_sale_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_sale_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get__account_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._account_code);
                        __get__account_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get__account_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get__paybill_area = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._area_paybill);
                        if (__get__paybill_area != null)
                        {
                            __get__paybill_area.textBox.Enter += new EventHandler(textBox_Enter);
                            __get__paybill_area.textBox.Leave += new EventHandler(textBox_Leave);
                        }

                    }
                    //MyLib._myTextBox __get_passbook_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._passbook_code);
                    //__get_passbook_code.textBox.Enter += new EventHandler(textBox_Enter);
                    //__get_passbook_code.textBox.Leave += new EventHandler(textBox_Leave);
                    //เขตการขาย 
                    /* MyLib._myTextBox __get_logistic_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._logistic_area);
                     __get_logistic_code.textBox.Enter += new EventHandler(textBox_Enter);
                     __get_logistic_code.textBox.Leave += new EventHandler(textBox_Leave);*/
                    break;
                case _controlTypeEnum.ArDetailCredit:
                    /* MyLib._myTextBox __get_credit_group_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._credit_group_code);
                     __get_credit_group_code.textBox.Enter += new EventHandler(textBox_Enter);
                     __get_credit_group_code.textBox.Leave += new EventHandler(textBox_Leave);
                     //พนักงานสินเชื่อ
                     MyLib._myTextBox __get_credit_person = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._credit_person);
                     __get_credit_person.textBox.Enter += new EventHandler(textBox_Enter);
                     __get_credit_person.textBox.Leave += new EventHandler(textBox_Leave);*/
                    break;
                case _controlTypeEnum.ArDetailBill:
                    //รหัสวางบิล
                    /*MyLib._myTextBox __get_pay_bill_condition = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._pay_bill_code);
                    __get_pay_bill_condition.textBox.Enter += new EventHandler(textBox_Enter);
                    __get_pay_bill_condition.textBox.Leave += new EventHandler(textBox_Leave);
                    //รหัสเก็บเช็ค
                    MyLib._myTextBox __get_keep_money_condition = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._keep_chq_code);
                    __get_keep_money_condition.textBox.Enter += new EventHandler(textBox_Enter);
                    __get_keep_money_condition.textBox.Leave += new EventHandler(textBox_Leave);
                    //พนักงานวางบิล
                    MyLib._myTextBox __get_payment_person = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._payment_person);
                    __get_payment_person.textBox.Enter += new EventHandler(textBox_Enter);
                    __get_payment_person.textBox.Leave += new EventHandler(textBox_Leave);
                    //พนักงานเก็บเงิน
                    MyLib._myTextBox __get_keep_money_person = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._keep_money_person);
                    __get_keep_money_person.textBox.Enter += new EventHandler(textBox_Enter);
                    __get_keep_money_person.textBox.Leave += new EventHandler(textBox_Leave);*/
                    break;
                case _controlTypeEnum.ArDetailGroup:
                    // กลุ่ม
                    MyLib._myTextBox __getGroupMainControl = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._group_main);
                    __getGroupMainControl.textBox.Enter += new EventHandler(textBox_Enter);
                    __getGroupMainControl.textBox.Leave += new EventHandler(textBox_Leave);
                    //
                    MyLib._myTextBox __getGroupSub1Control = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._group_sub_1);
                    __getGroupSub1Control.textBox.Enter += new EventHandler(textBox_Enter);
                    __getGroupSub1Control.textBox.Leave += new EventHandler(textBox_Leave);
                    //
                    MyLib._myTextBox __getGroupSub2Control = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._group_sub_2);
                    __getGroupSub2Control.textBox.Enter += new EventHandler(textBox_Enter);
                    __getGroupSub2Control.textBox.Leave += new EventHandler(textBox_Leave);
                    //
                    MyLib._myTextBox __getGroupSub3Control = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._group_sub_3);
                    __getGroupSub3Control.textBox.Enter += new EventHandler(textBox_Enter);
                    __getGroupSub3Control.textBox.Leave += new EventHandler(textBox_Leave);
                    //
                    MyLib._myTextBox __getGroupSub4Control = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._group_sub_4);
                    __getGroupSub4Control.textBox.Enter += new EventHandler(textBox_Enter);
                    __getGroupSub4Control.textBox.Leave += new EventHandler(textBox_Leave);
                    break;
                case _controlTypeEnum.ArDetailDimension:
                    // มิติ
                    MyLib._myTextBox __getDimension1 = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._dimension_1);
                    __getDimension1.textBox.Enter += new EventHandler(textBox_Enter);
                    __getDimension1.textBox.Leave += new EventHandler(textBox_Leave);
                    //
                    MyLib._myTextBox __getDimension2 = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._dimension_2);
                    __getDimension2.textBox.Enter += new EventHandler(textBox_Enter);
                    __getDimension2.textBox.Leave += new EventHandler(textBox_Leave);
                    //
                    MyLib._myTextBox __getDimension3 = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._dimension_3);
                    __getDimension3.textBox.Enter += new EventHandler(textBox_Enter);
                    __getDimension3.textBox.Leave += new EventHandler(textBox_Leave);
                    //
                    MyLib._myTextBox __getDimension4 = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._dimension_4);
                    __getDimension4.textBox.Enter += new EventHandler(textBox_Enter);
                    __getDimension4.textBox.Leave += new EventHandler(textBox_Leave);
                    //
                    MyLib._myTextBox __getDimension5 = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._dimension_5);
                    __getDimension5.textBox.Enter += new EventHandler(textBox_Enter);
                    __getDimension5.textBox.Leave += new EventHandler(textBox_Leave);
                    break;
                case _controlTypeEnum.ArDealerHealthy:
                    // 
                    MyLib._myTextBox _nationality = (MyLib._myTextBox)this._getControl(_g.d.ar_dealer._nationality);
                    _nationality.textBox.Enter += new EventHandler(textBox_Enter);
                    _nationality.textBox.Leave += new EventHandler(textBox_Leave);
                    MyLib._myTextBox _branch_code = (MyLib._myTextBox)this._getControl(_g.d.ar_dealer._branch_code);
                    _branch_code.textBox.Enter += new EventHandler(textBox_Enter);
                    _branch_code.textBox.Leave += new EventHandler(textBox_Leave);
                    //

                    break;
                case _controlTypeEnum.Customer:
                    {

                        //this._addTextBox(1, 0, 1, 0, _g.d.ar_customer_detail._ar_channel_code, 1, 0, 1, true, false, true);
                        //this._addTextBox(1, 1, 1, 0, _g.d.ar_customer_detail._customer_type_code, 1, 0, 1, true, false, true);
                        //this._addTextBox(2, 0, 1, 0, _g.d.ar_customer_detail._ar_sub_type_1_code, 1, 0, 1, true, false, true);
                        //this._addTextBox(2, 1, 1, 0, _g.d.ar_customer_detail._ar_vehicle_code, 1, 0, 1, true, false, true);
                        //this._addTextBox(3, 0, 1, 0, _g.d.ar_customer_detail._ar_equipment_code, 1, 0, 1, true, false, true);
                        //this._addTextBox(3, 1, 1, 0, _g.d.ar_customer_detail._ar_sub_equipment, 1, 0, 1, true, false, true);
                        //this._addTextBox(4, 0, 1, 0, _g.d.ar_customer_detail._ar_location_type_code, 1, 0, 1, true, false, true);
                        //MyLib._myTextBox __get_customer_type_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._customer_type_code);
                        //__get_customer_type_code.textBox.Enter += new EventHandler(textBox_Enter);
                        //__get_customer_type_code.textBox.Leave += new EventHandler(textBox_Leave);

                        //MyLib._myTextBox __get_ar_sub_type_1_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._ar_sub_type_1_code);
                        //__get_ar_sub_type_1_code.textBox.Enter += new EventHandler(textBox_Enter);
                        //__get_ar_sub_type_1_code.textBox.Leave += new EventHandler(textBox_Leave);

                        //MyLib._myTextBox __get_ar_channel_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._ar_channel_code);
                        //__get_ar_channel_code.textBox.Enter += new EventHandler(textBox_Enter);
                        //__get_ar_channel_code.textBox.Leave += new EventHandler(textBox_Leave);

                        //MyLib._myTextBox __get__ar_location_type_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._ar_location_type_code);
                        //__get__ar_location_type_code.textBox.Enter += new EventHandler(textBox_Enter);
                        //__get__ar_location_type_code.textBox.Leave += new EventHandler(textBox_Leave);

                        //MyLib._myTextBox __get_ar_vehicle_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._ar_vehicle_code);
                        //__get_ar_vehicle_code.textBox.Enter += new EventHandler(textBox_Enter);
                        //__get_ar_vehicle_code.textBox.Leave += new EventHandler(textBox_Leave);

                        //MyLib._myTextBox __get_ar_equipment_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._ar_equipment_code);
                        //__get_ar_equipment_code.textBox.Enter += new EventHandler(textBox_Enter);
                        //__get_ar_equipment_code.textBox.Leave += new EventHandler(textBox_Leave);

                        //MyLib._myTextBox __get_ar_sub_equipment = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._ar_sub_equipment);
                        //__get_ar_sub_equipment.textBox.Enter += new EventHandler(textBox_Enter);
                        //__get_ar_sub_equipment.textBox.Leave += new EventHandler(textBox_Leave);


                        //MyLib._myTextBox __get_ar_project_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._ar_project_code);
                        //__get_ar_project_code.textBox.Enter += new EventHandler(textBox_Enter);
                        //__get_ar_project_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get_ar_shoptype1_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype1_code);
                        __get_ar_shoptype1_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_ar_shoptype1_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get_ar_shoptype2_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype2_code);
                        __get_ar_shoptype2_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_ar_shoptype2_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get_ar_shoptype3_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype3_code);
                        __get_ar_shoptype3_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_ar_shoptype3_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get_ar_shoptype4_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype4_code);
                        __get_ar_shoptype4_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_ar_shoptype4_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get_ar_shoptype5_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype5_code);
                        __get_ar_shoptype5_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_ar_shoptype5_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get_sub_ar_shoptype5_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype5_code);
                        __get_sub_ar_shoptype5_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_sub_ar_shoptype5_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get_ar_shoptype6_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype6_code);
                        __get_ar_shoptype6_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_ar_shoptype6_code.textBox.Leave += new EventHandler(textBox_Leave);

                        MyLib._myTextBox __get_ar_shoptype7_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype7_code);
                        __get_ar_shoptype7_code.textBox.Enter += new EventHandler(textBox_Enter);
                        __get_ar_shoptype7_code.textBox.Leave += new EventHandler(textBox_Leave);


                        MyLib._myTextBox __getBrCustCodeControl = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._br_cust_code);
                        if (__getBrCustCodeControl != null)
                        {
                            __getBrCustCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
                            __getBrCustCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
                        }
                    }
                    break;
            }
            this.Invalidate();
            this.ResumeLayout();
        }

        private void _screen_ar_main__comboBoxSelectIndexChanged(object sender, string name)
        {
            if (this._controlName == _controlTypeEnum.Customer)
            {
                decimal select = this._getDataNumber(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_project_code_name);

                MyLib._myTextBox __get_ar_shoptype1_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype1_code);
                MyLib._myTextBox __get_ar_shoptype2_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype2_code);
                MyLib._myTextBox __get_ar_shoptype3_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype3_code);
                MyLib._myTextBox __get_ar_shoptype4_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype4_code);
                MyLib._myTextBox __get_ar_shoptype5_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype5_code);
                MyLib._myTextBox __get_sub_ar_shoptype5_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._sub_ar_shoptype5_code);
                MyLib._myTextBox __get_ar_shoptype6_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype6_code);
                MyLib._myTextBox __get_ar_shoptype7_code = (MyLib._myTextBox)this._getControl(_g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_shoptype7_code);

                switch (select)
                {
                    case 0:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = false;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;
                        __get_ar_shoptype6_code.Enabled = false;
                        __get_ar_shoptype7_code.Enabled = false;

                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                        break;
                    case 1:
                        __get_ar_shoptype1_code.Enabled = true;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = false;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;
                        __get_ar_shoptype6_code.Enabled = false;
                        __get_ar_shoptype7_code.Enabled = false;


                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                        break;
                    case 2:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = true;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = false;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;

                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);

                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                        break;
                    case 3:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = true;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;
                        __get_ar_shoptype6_code.Enabled = false;
                        __get_ar_shoptype7_code.Enabled = false;

                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                        break;
                    case 4:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = false;
                        __get_ar_shoptype5_code.Enabled = true;
                        __get_sub_ar_shoptype5_code.Enabled = true;
                        __get_ar_shoptype6_code.Enabled = false;
                        __get_ar_shoptype7_code.Enabled = false;
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);

                        break;
                    case 5:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = true;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;
                        __get_ar_shoptype6_code.Enabled = false;
                        __get_ar_shoptype7_code.Enabled = false;


                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                        break;
                    case 6:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = true;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;
                        __get_ar_shoptype6_code.Enabled = false;
                        __get_ar_shoptype7_code.Enabled = false;

                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                        //this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                        break;
                    case 7:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = true;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;
                        __get_ar_shoptype6_code.Enabled = false;
                        __get_ar_shoptype7_code.Enabled = false;

                        break;
                    case 8:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = false;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;
                        __get_ar_shoptype6_code.Enabled = true;
                        __get_ar_shoptype7_code.Enabled = false;
                        break;
                    case 9:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = true;
                        __get_ar_shoptype4_code.Enabled = false;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;
                        __get_ar_shoptype6_code.Enabled = false;
                        __get_ar_shoptype7_code.Enabled = false;
                        break;
                    case 10:
                        __get_ar_shoptype1_code.Enabled = false;
                        __get_ar_shoptype2_code.Enabled = false;
                        __get_ar_shoptype3_code.Enabled = false;
                        __get_ar_shoptype4_code.Enabled = false;
                        __get_ar_shoptype5_code.Enabled = false;
                        __get_sub_ar_shoptype5_code.Enabled = false;
                        __get_ar_shoptype6_code.Enabled = false;
                        __get_ar_shoptype7_code.Enabled = true;

                        break;
                }
            }

        }

        private void _screen_ar_main__checkBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ar_customer._arm_register))
            {
                DateTime __today = DateTime.Now;
                MyLib._myCheckBox __get__arm_register = (MyLib._myCheckBox)this._getControl(_g.d.ar_customer._arm_register);
                MyLib._myDateBox __get__arm_register_date = (MyLib._myDateBox)this._getControl(_g.d.ar_customer._arm_register_date);
                if (__get__arm_register.Checked == true)
                {
                    this._setDataDate(_g.d.ar_customer._arm_register_date, __today);
                }
                else
                {
                    __get__arm_register_date.textBox.Text = "";
                }
            }
            if (name.Equals("ar_customer.arm_register"))
            {
                DateTime __today = DateTime.Now;
                MyLib._myCheckBox __get__arm_register = (MyLib._myCheckBox)this._getControl(_g.d.ar_customer._table + "." + _g.d.ar_customer._arm_register);
                MyLib._myDateBox __get__arm_register_date = (MyLib._myDateBox)this._getControl(_g.d.ar_customer._table + "." + _g.d.ar_customer._arm_register_date);
                if (__get__arm_register.Checked == true)
                {
                    this._setDataDate(_g.d.ar_customer._table + "." + _g.d.ar_customer._arm_register_date, __today);
                }
                else
                {
                    __get__arm_register_date.textBox.Text = "";
                }
            }
        }


        void _screenArControl__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ar_customer._ar_branch_code) == false && MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                this._setDataStr(_g.d.ar_customer._ar_branch_code, _g.g._companyProfile._branch_code);
            }

            if (name.Equals(_g.d.ar_customer._code))
            {
                MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
                string __arCode = __textBox._textFirst;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __arCode + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    string __docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __newArCode = _g.g._getAutoRun(_g.g._autoRunType.ลูกหนี้, __arCode, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง);
                    this._setDataStr(_g.d.ar_customer._code, __newArCode, "", true);
                }
                else
                {
                    if (__arCode.Length > 0)
                    {
                        try
                        {
                            string __newArCode = __arCode;
                            DataTable __dt = __myFrameWork._queryShort("select " + _g.d.ar_customer._code + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "<\'" + __arCode + "z\' order by " + _g.d.ar_customer._code + " desc limit 1").Tables[0];
                            if (__dt.Rows.Count > 0)
                            {
                                string __getArCode = __dt.Rows[0][_g.d.ar_customer._code].ToString();
                                if (__getArCode.Length > __arCode.Length)
                                {
                                    string __s1 = __getArCode.Substring(0, __arCode.Length);
                                    if (__s1.Equals(__arCode))
                                    {
                                        string __s2 = __getArCode.Remove(0, __arCode.Length);
                                        int __runningNumber = (int)MyLib._myGlobal._decimalPhase(__s2);
                                        if (__runningNumber > 0)
                                        {
                                            StringBuilder __format = new StringBuilder();
                                            for (int __loop = 0; __loop < __s2.Length; __loop++)
                                            {
                                                __format.Append("0");
                                            }
                                            __newArCode = __s1 + String.Format("{0:" + __format.ToString() + "}", ((decimal)(__runningNumber + 1)));
                                            this._setDataStr(_g.d.ar_customer._code, __newArCode, "", true);
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
            else if (name.Equals(_g.d.ar_customer_detail._br_cust_code))
            {
                MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
                string __arCode = __textBox._textFirst;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __arCode + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    string __docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __newArCode = _g.g._getAutoRun(_g.g._autoRunType.ลูกหนี้, __arCode, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง);
                    this._setDataStr(_g.d.ar_customer_detail._br_cust_code, __newArCode, "", true);
                }
                else
                {
                    if (__arCode.Length > 0)
                    {
                        try
                        {
                            string __newArCode = __arCode;
                            DataTable __dt = __myFrameWork._queryShort("select " + _g.d.ar_customer_detail._br_cust_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._br_cust_code + "<\'" + __arCode + "z\' order by " + _g.d.ar_customer_detail._br_cust_code + " desc limit 1").Tables[0];
                            if (__dt.Rows.Count > 0)
                            {
                                string __getArCode = __dt.Rows[0][_g.d.ar_customer_detail._br_cust_code].ToString();
                                if (__getArCode.Length > __arCode.Length)
                                {
                                    string __s1 = __getArCode.Substring(0, __arCode.Length);
                                    if (__s1.Equals(__arCode))
                                    {
                                        string __s2 = __getArCode.Remove(0, __arCode.Length);
                                        int __runningNumber = (int)MyLib._myGlobal._decimalPhase(__s2);
                                        if (__runningNumber > 0)
                                        {
                                            StringBuilder __format = new StringBuilder();
                                            for (int __loop = 0; __loop < __s2.Length; __loop++)
                                            {
                                                __format.Append("0");
                                            }
                                            __newArCode = __s1 + String.Format("{0:" + __format.ToString() + "}", ((decimal)(__runningNumber + 1)));
                                            this._setDataStr(_g.d.ar_customer_detail._br_cust_code, __newArCode, "", true);
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
                if (name.Equals(_g.d.ar_customer._tambon) ||
                name.Equals(_g.d.ar_customer._amper) ||
                name.Equals(_g.d.ar_customer._province) ||
                name.Equals(_g.d.ar_customer._ar_type) ||
                //_controlTypeEnum.ArDetail
                name.Equals(_g.d.ar_customer_detail._area_code) ||
                name.Equals(_g.d.ar_customer_detail._sale_code) ||
                name.Equals(_g.d.ar_customer_detail._logistic_area) ||
                name.Equals(_g.d.ar_customer_detail._account_code) ||
                // name.Equals(_g.d.ar_customer_detail._passbook_code) ||
                //_controlTypeEnum.ArDetailCredit:
                name.Equals(_g.d.ar_customer_detail._credit_group_code) ||
                //_controlTypeEnum.ArDetailBill
                name.Equals(_g.d.ar_customer_detail._pay_bill_code) ||
                name.Equals(_g.d.ar_customer_detail._keep_chq_code) ||
                name.Equals(_g.d.ar_customer_detail._payment_person) ||
                name.Equals(_g.d.ar_customer_detail._credit_person) ||
                name.Equals(_g.d.ar_customer_detail._keep_money_person) ||
                //_controlTypeEnum.ArDetailDimension:
                name.Equals(_g.d.ar_customer_detail._dimension_1) ||
                name.Equals(_g.d.ar_customer_detail._dimension_2) ||
                name.Equals(_g.d.ar_customer_detail._dimension_3) ||
                name.Equals(_g.d.ar_customer_detail._dimension_4) ||
                name.Equals(_g.d.ar_customer_detail._dimension_5) ||
                //_controlTypeEnum.ArDetailGroup:
                name.Equals(_g.d.ar_customer_detail._group_main) ||
                name.Equals(_g.d.ar_customer_detail._group_sub_1) ||
                name.Equals(_g.d.ar_customer_detail._group_sub_2) ||
                name.Equals(_g.d.ar_customer_detail._group_sub_3) ||
                  name.Equals(_g.d.ar_dealer._nationality) ||
                name.Equals(_g.d.ar_dealer._branch_code) ||
                name.Equals(_g.d.ar_customer_detail._group_sub_4) ||
                    name.Equals(_g.d.ar_customer_detail._area_paybill) ||
                //_controlTypeEnum.Customer
                name.Equals(_g.d.ar_customer_detail._customer_type_code) ||
                name.Equals(_g.d.ar_customer_detail._ar_sub_type_1_code) ||
                name.Equals(_g.d.ar_customer_detail._ar_channel_code) ||
                name.Equals(_g.d.ar_customer_detail._ar_vehicle_code) ||
                name.Equals(_g.d.ar_customer_detail._ar_equipment_code) ||
                name.Equals(_g.d.ar_customer_detail._ar_sub_equipment) ||

                 name.Equals(_g.d.ar_customer_detail._ar_project_code) ||
                  name.Equals(_g.d.ar_customer_detail._ar_shoptype1_code) ||
                   name.Equals(_g.d.ar_customer_detail._ar_shoptype2_code) ||
                    name.Equals(_g.d.ar_customer_detail._ar_shoptype3_code) ||
                     name.Equals(_g.d.ar_customer_detail._ar_shoptype4_code) ||
                      name.Equals(_g.d.ar_customer_detail._ar_shoptype5_code) ||
                       name.Equals(_g.d.ar_customer_detail._sub_ar_shoptype5_code) ||
                           name.Equals(_g.d.ar_customer_detail._ar_shoptype6_code) ||
                      name.Equals(_g.d.ar_customer_detail._ar_shoptype7_code) ||
                name.Equals(_g.d.ar_customer_detail._ar_location_type_code))
                {
                    this._searchTextBox = (TextBox)sender;
                    this._searchName = name;
                    this._search(true);
                }
            }

            if (this._controlName == _controlTypeEnum.Customer)
            {
                if (name.Equals(_g.d.ar_customer_detail._ar_shoptype1_code) && !sender.Equals(" "))
                {
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype6_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype7_code, "", "", true);
                }
                else
           if (name.Equals(_g.d.ar_customer_detail._ar_shoptype2_code) && !sender.Equals(" "))
                {
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype6_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype7_code, "", "", true);
                }
                else
           if (name.Equals(_g.d.ar_customer_detail._ar_shoptype3_code) && !sender.Equals(" "))
                {
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype6_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype7_code, "", "", true);
                }
                else
           if (name.Equals(_g.d.ar_customer_detail._ar_shoptype4_code) && !sender.Equals(" "))
                {
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype6_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype7_code, "", "", true);
                }
                else
           if (name.Equals(_g.d.ar_customer_detail._ar_shoptype5_code) && !sender.Equals(" "))
                {
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype6_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype7_code, "", "", true);
                }
                if (name.Equals(_g.d.ar_customer_detail._ar_shoptype6_code) && !sender.Equals(" "))
                {
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype7_code, "", "", true);
                }
                else
                if (name.Equals(_g.d.ar_customer_detail._ar_shoptype7_code) && !sender.Equals(" "))
                {
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype1_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype2_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype3_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype4_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._sub_ar_shoptype5_code, "", "", true);
                    this._setDataStr(_g.d.ar_customer_detail._ar_shoptype6_code, "", "", true);
                }
            }



        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._search_data_full.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _screenArControl__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void _screenArControl__textBoxSearch(object sender)
        {
            //ค้นหารหัสลูกหนี้
            // ค้นหาหน้าจอ Top
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower().Replace("ar_customer_detail.", string.Empty).Replace("ar_customer.", string.Empty);
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            //กดปุ่มค้นหาเลือกรายการกดEnter
            //if (this._searchName.Equals(_g.d.ar_customer._tambon.ToLower()))
            //{
            if (this._searchName.Equals(_g.d.ar_customer_detail._account_code))
            {
                this._chartOfAccountScreen.ShowDialog(this);
            }
            else
            {
                string _search_text_new = _search_screen_neme(this._searchName);
                if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
                {
                    this._search_data_full = new MyLib._searchDataFull();
                    this._search_data_full._name = _search_text_new;
                    this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    if (this._searchName.Equals(_g.d.ar_customer_detail._sub_ar_shoptype5_code))
                    {
                        this._search_data_full._dataList._extraWhere = _g.d.sub_ar_shoptype5._ar_shoptype5_code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype5_code).ToString() + "\'";
                    }
                    this._search_data_full._dataList._refreshData();
                }
                if (this._searchName.Equals(_g.d.ar_customer._code.ToLower()))
                {
                    string _where = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'AR\'";
                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._change_branch_code == false)
                    {
                        _where += " AND ((coalesce(" + _g.d.erp_doc_format._use_branch_select + ", 0) = 0 ) or (" + _g.d.erp_doc_format._branch_list + " like '%" + MyLib._myGlobal._branchCode + "%'))";
                    }

                    MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
                }
                else
                    if (this._searchName.Equals(_g.d.ar_customer._tambon.ToLower()))
                {
                    string _where = " " + _g.d.erp_tambon._province + "=\'" + this._getDataStr(_g.d.ar_customer._province) + "\' and " + _g.d.erp_tambon._amper + "=\'" + this._getDataStr(_g.d.ar_customer._amper) + "\'";
                    MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
                }
                else
                        if (this._searchName.Equals(_g.d.ar_customer._amper.ToLower()))
                {
                    string _where = " " + _g.d.erp_amper._province + "=\'" + this._getDataStr(_g.d.ar_customer._province) + "\'";
                    MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
                }
                else
                            if (this._searchName.Equals(_g.d.ar_customer_detail._group_sub_1) || this._searchName.Equals(_g.d.ar_customer_detail._group_sub_2) || this._searchName.Equals(_g.d.ar_customer_detail._group_sub_3) || this._searchName.Equals(_g.d.ar_customer_detail._group_sub_4))
                {
                    string _where = " " + _g.d.ar_group_sub._main_group + "=\'" + this._getDataStr(_g.d.ar_customer_detail._group_main) + "\'";
                    MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
                }
                else if (this._searchName.Equals(_g.d.ar_customer._ar_code_main))
                {
                    MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, true);

                }
                else if (this._searchName.Equals(_g.d.ar_customer._arm_code))
                {
                    // running arm code
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._doc_running + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._screen_code + "=\'ARMCODE\'").Tables[0];

                    if (__getFormat.Rows.Count == 1)
                    {
                        string __format = __getFormat.Rows[0][0].ToString();
                        string __docFormatCode = __getFormat.Rows[0][1].ToString();
                        string __newArCode = _g.g._getAutoRun(_g.g._autoRunType.ว่าง, "ARMCODE", MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง, _g.d.ar_customer._table, __getFormat.Rows[0][2].ToString(), _g.d.ar_customer._arm_code, "");
                        this._setDataStr(_g.d.ar_customer._arm_code, __newArCode, "", true);

                    }
                    else
                    {
                        string _where = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'ARMCODE\'";
                        MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
                    }
                }
                else if (this._searchName.Equals(_g.d.ar_customer._ar_branch_code))
                {
                    MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, true);
                }
                if (this._searchName.Equals(_g.d.ar_customer_detail._br_cust_code.ToLower()))
                {
                    string _where = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'ARBR\'";
                    MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false, true, _where);
                }
                else
                {
                    if (!this._searchName.Equals(_g.d.ar_customer._arm_code))
                    {


                        MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full, false);
                    }

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

            if (this._searchName.Equals(_g.d.ar_customer._arm_code))
            {
                string result = (string)this._search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length != 0)
                {
                    // running arm code
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._doc_running + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + result + "\'").Tables[0];

                    if (__getFormat.Rows.Count > 0)
                    {
                        string __format = __getFormat.Rows[0][0].ToString();
                        string __docFormatCode = __getFormat.Rows[0][1].ToString();
                        string __newArCode = _g.g._getAutoRun(_g.g._autoRunType.ว่าง, result, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง, _g.d.ar_customer._table, __getFormat.Rows[0][2].ToString(), _g.d.ar_customer._arm_code, "");
                        this._setDataStr(_g.d.ar_customer._arm_code, __newArCode, "", true);
                    }

                }
                this._search_data_full.Visible = false;

            }
            else if (this._searchName.Equals(_g.d.ar_customer_detail._br_cust_code))
            {
                string result = (string)this._search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length != 0)
                {
                    // running arm code
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._doc_running + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + result + "\'").Tables[0];
                    if (__getFormat.Rows.Count > 0)
                    {
                        string __format = __getFormat.Rows[0][0].ToString();
                        string __docFormatCode = __getFormat.Rows[0][1].ToString();
                        string __newArCode = _g.g._getAutoRun(_g.g._autoRunType.ว่าง, result, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง, _g.d.ar_customer_detail._table, __getFormat.Rows[0][2].ToString(), _g.d.ar_customer_detail._br_cust_code, "");
                        this._setDataStr(_g.d.ar_customer_detail._br_cust_code, __newArCode, "", true);
                    }
                }
                this._search_data_full.Visible = false;

            }
            else if (name.Length > 0)
            {
                string result = (string)this._search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length != 0)
                {
                    this._search_data_full.Visible = false;
                    if (this._searchName == "ar_shoptype1_code" || this._searchName == "ar_shoptype2_code" || this._searchName == "ar_shoptype3_code" || this._searchName == "ar_shoptype4_code" || this._searchName == "ar_shoptype5_code" || this._searchName == "ar_shoptype6_code" || this._searchName == "ar_shoptype4_code" || this._searchName == "sub_ar_shoptype5_code") {
                        this._searchName = "ar_customer_detail." + this._searchName;
                    }
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
                switch (_controlName)
                {
                    case _controlTypeEnum.Ar:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._code + "=\'" + this._getDataStr(_g.d.ar_customer._province) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._province + "=\'" + this._getDataStr(_g.d.ar_customer._province) + "\' and " + _g.d.erp_amper._code + "=\'" + this._getDataStr(_g.d.ar_customer._amper) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_tambon._name_1 + " from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._province + "=\'" + this._getDataStr(_g.d.ar_customer._province) + "\' and " + _g.d.erp_tambon._amper + "=\'" + this._getDataStr(_g.d.ar_customer._amper) + "\' and " + _g.d.erp_tambon._code + "=\'" + this._getDataStr(_g.d.ar_customer._tambon) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_type._name_1 + " from " + _g.d.ar_type._table + " where " + _g.d.ar_type._code + "=\'" + this._getDataStr(_g.d.ar_customer._ar_type) + "\'"));
                        break;
                    case _controlTypeEnum.ArDetail:
                        //_controlTypeEnum.ArDetail
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_sale_area._name_1 + " from " + _g.d.ar_sale_area._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._area_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._sale_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_logistic_area._name_1 + " from " + _g.d.ar_logistic_area._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._logistic_area) + "\'"));
                        //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._name_1 + " from " + _g.d.erp_pass_book._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._passbook_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this._getDataStr(_g.d.ar_customer_detail._account_code).ToUpper() + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_paybill_area._name_1 + " from " + _g.d.ar_paybill_area._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._area_paybill) + "\'"));
                        break;
                    case _controlTypeEnum.ArDetailCredit:
                        //_controlTypeEnum.ArDetailCredit:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_credit_group._name_1 + " from " + _g.d.ar_credit_group._table + " where " + _g.d.ar_credit_group._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._credit_group_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user_group._name_1 + " from " + _g.d.erp_user_group._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._credit_person) + "\'"));
                        break;
                    case _controlTypeEnum.ArDetailBill:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_pay_bill_condition._name_1 + " from " + _g.d.ar_pay_bill_condition._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._pay_bill_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_keep_chq_condition._name_1 + " from " + _g.d.ar_keep_chq_condition._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._keep_chq_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user_group._name_1 + " from " + _g.d.erp_user_group._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._payment_person) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user_group._name_1 + " from " + _g.d.erp_user_group._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._keep_money_person) + "\'"));
                        break;
                    case _controlTypeEnum.ArDetailDimension:
                        //_controlTypeEnum.ArDetailDimension:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dimension._name_1 + " from " + _g.d.ar_dimension._table + " where " + _g.d.ar_dimension._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._dimension_1) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dimension._name_1 + " from " + _g.d.ar_dimension._table + " where " + _g.d.ar_dimension._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._dimension_2) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dimension._name_1 + " from " + _g.d.ar_dimension._table + " where " + _g.d.ar_dimension._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._dimension_3) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dimension._name_1 + " from " + _g.d.ar_dimension._table + " where " + _g.d.ar_dimension._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._dimension_4) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dimension._name_1 + " from " + _g.d.ar_dimension._table + " where " + _g.d.ar_dimension._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._dimension_5) + "\'"));
                        break;
                    case _controlTypeEnum.ArDetailGroup:
                        //_controlTypeEnum.ArDetailGroup
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_group._name_1 + " from " + _g.d.ar_group._table + " where " + _g.d.ar_group._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._group_main) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_group_sub._name_1 + " from " + _g.d.ar_group_sub._table + " where " + _g.d.ar_group_sub._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._group_sub_1) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_group_sub._name_1 + " from " + _g.d.ar_group_sub._table + " where " + _g.d.ar_group_sub._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._group_sub_2) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_group_sub._name_1 + " from " + _g.d.ar_group_sub._table + " where " + _g.d.ar_group_sub._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._group_sub_3) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_group_sub._name_1 + " from " + _g.d.ar_group_sub._table + " where " + _g.d.ar_group_sub._code + "=\'" + this._getDataStr(_g.d.ar_customer_detail._group_sub_4) + "\'"));
                        break;
                    case _controlTypeEnum.ArDealerHealthy:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_nationality._name_1 + " from " + _g.d.m_nationality._table + " where " + _g.d.m_nationality._code + "=\'" + this._getDataStr(_g.d.ar_dealer._nationality) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_branch_list._name_1 + " from " + _g.d.erp_branch_list._table + " where upper(" + _g.d.erp_branch_list._code + ")=\'" + this._getDataStr(_g.d.ar_dealer._branch_code) + "\'"));
                        break;
                    case _controlTypeEnum.Customer:
                        //_controlTypeEnum.Customer
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.customer_type._name_1 + " from " + _g.d.customer_type._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._customer_type_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_channel._name_1 + " from " + _g.d.ar_channel._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table + "."+_g.d.ar_customer_detail._ar_channel_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_location_type._name_1 + " from " + _g.d.ar_location_type._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_location_type_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_sub_type_1._name_1 + " from " + _g.d.ar_sub_type_1._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_sub_type_1_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_vehicle._name_1 + " from " + _g.d.ar_vehicle._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_vehicle_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_equipment._name_1 + " from " + _g.d.ar_equipment._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_equipment_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_sub_equipment._name_1 + " from " + _g.d.ar_sub_equipment._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_sub_equipment) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_project._name_1 + " from " + _g.d.ar_project._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_project_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_shoptype1._name_1 + " from " + _g.d.ar_shoptype1._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype1_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_shoptype2._name_1 + " from " + _g.d.ar_shoptype2._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype2_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_shoptype3._name_1 + " from " + _g.d.ar_shoptype3._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype3_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_shoptype4._name_1 + " from " + _g.d.ar_shoptype4._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype4_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_shoptype5._name_1 + " from " + _g.d.ar_shoptype5._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype5_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.sub_ar_shoptype5._name_1 + " from " + _g.d.sub_ar_shoptype5._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._sub_ar_shoptype5_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_shoptype6._name_1 + " from " + _g.d.ar_shoptype6._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype6_code) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_shoptype7._name_1 + " from " + _g.d.ar_shoptype7._table + " where code=\'" + this._getDataStr(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype7_code) + "\'"));
                        break;
                }
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                switch (_controlName)
                {
                    case _controlTypeEnum.Ar:
                        if (_searchAndWarning(_g.d.ar_customer._province, (DataSet)_getData[0], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer._amper, (DataSet)_getData[1], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer._tambon, (DataSet)_getData[2], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer._ar_type, (DataSet)_getData[3], warning) == false) { }
                        break;
                    case _controlTypeEnum.ArDetail:
                        //_controlTypeEnum.ArDetail
                        if (_searchAndWarning(_g.d.ar_customer_detail._area_code, (DataSet)_getData[0], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._sale_code, (DataSet)_getData[1], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._logistic_area, (DataSet)_getData[2], warning) == false) { }
                        //if (_searchAndWarning(_g.d.ar_customer_detail._passbook_code, (DataSet)_getData[3], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._account_code, (DataSet)_getData[3], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._area_paybill, (DataSet)_getData[4], warning) == false) { }
                        break;
                    case _controlTypeEnum.ArDetailCredit:
                        //_controlTypeEnum.ArDetailCredit:
                        if (_searchAndWarning(_g.d.ar_customer_detail._credit_group_code, (DataSet)_getData[0], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._credit_person, (DataSet)_getData[1], warning) == false) { }
                        break;
                    case _controlTypeEnum.ArDetailBill:
                        if (_searchAndWarning(_g.d.ar_customer_detail._pay_bill_code, (DataSet)_getData[0], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._keep_chq_code, (DataSet)_getData[1], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._payment_person, (DataSet)_getData[2], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._keep_money_person, (DataSet)_getData[3], warning) == false) { }
                        break;
                    case _controlTypeEnum.ArDetailDimension:
                        //_controlTypeEnum.ArDetailDimension:
                        if (_searchAndWarning(_g.d.ar_customer_detail._dimension_1, (DataSet)_getData[0], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._dimension_2, (DataSet)_getData[1], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._dimension_3, (DataSet)_getData[2], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._dimension_4, (DataSet)_getData[3], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._dimension_5, (DataSet)_getData[4], warning) == false) { }
                        break;
                    case _controlTypeEnum.ArDetailGroup:
                        //_controlTypeEnum.ArDetailGroup
                        if (_searchAndWarning(_g.d.ar_customer_detail._group_main, (DataSet)_getData[0], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._group_sub_1, (DataSet)_getData[1], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._group_sub_2, (DataSet)_getData[2], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._group_sub_3, (DataSet)_getData[3], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._group_sub_4, (DataSet)_getData[4], warning) == false) { }
                        break;
                    case _controlTypeEnum.ArDealerHealthy:
                        //_controlTypeEnum.ArDetailGroup
                        if (_searchAndWarning(_g.d.ar_dealer._nationality, (DataSet)_getData[0], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_dealer._branch_code, (DataSet)_getData[1], warning) == false) { }
                        break;
                    case _controlTypeEnum.Customer:
                        //_controlTypeEnum.ArDetailGroup
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._customer_type_code, (DataSet)_getData[0], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_channel_code, (DataSet)_getData[1], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_location_type_code, (DataSet)_getData[2], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_sub_type_1_code, (DataSet)_getData[3], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_vehicle_code, (DataSet)_getData[4], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_equipment_code, (DataSet)_getData[5], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_sub_equipment, (DataSet)_getData[6], warning) == false) { }

                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_project_code, (DataSet)_getData[7], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype1_code, (DataSet)_getData[8], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype2_code, (DataSet)_getData[9], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype3_code, (DataSet)_getData[10], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype4_code, (DataSet)_getData[11], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype5_code, (DataSet)_getData[12], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._sub_ar_shoptype5_code, (DataSet)_getData[13], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype6_code, (DataSet)_getData[14], warning) == false) { }
                        if (_searchAndWarning(_g.d.ar_customer_detail._table+"."+_g.d.ar_customer_detail._ar_shoptype7_code, (DataSet)_getData[15], warning) == false) { }
                        break;
                }
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
                case "ar_type": return _g.g._search_master_ar_type;
                //_controlTypeEnum.ArDetail
                case "area_code": return _g.g._search_master_ar_area_code;
                case "area_paybill": return _g.g._search_master_ar_paybill_area_code;
                case "sale_code": return _g.g._search_screen_erp_user;
                case "logistic_area": return _g.g._search_master_ar_logistic_area;
                case "credit_group_code": return _g.g._search_master_ar_credit_group;
                case "credit_person": return _g.g._search_screen_erp_user_group;
                case "passbook_code": return _g.g._search_screen_สมุดเงินฝาก;
                //_controlTypeEnum.ArDetailBill
                case "pay_bill_code": return _g.g._search_master_ar_pay_bill_condition;
                case "keep_chq_code": return _g.g._search_master_ar_keep_chq_condition;
                case "payment_person": return _g.g._search_screen_erp_user_group; ;
                case "keep_money_person": return _g.g._search_screen_erp_user_group;
                //_controlTypeEnum.ArDetailDimension:
                case "dimension_1": return _g.g._search_master_ar_dimension;
                case "dimension_2": return _g.g._search_master_ar_dimension;
                case "dimension_3": return _g.g._search_master_ar_dimension;
                case "dimension_4": return _g.g._search_master_ar_dimension;
                case "dimension_5": return _g.g._search_master_ar_dimension;
                case "group_main": return _g.g._search_master_ar_group;
                case "group_sub_1": return _g.g._search_master_ar_group_sub;
                case "group_sub_2": return _g.g._search_master_ar_group_sub;
                case "group_sub_3": return _g.g._search_master_ar_group_sub;
                case "group_sub_4": return _g.g._search_master_ar_group_sub;
                case "nationality": return _g.g._search_screen_healthy_nationality;
                case "branch_code": return _g.g._search_master_erp_branch_list;
                // toe sml healtry 
                case "ar_code":
                case "ar_code_main":
                    return _g.g._search_screen_ar;
                case "ar_branch_code":
                    return _g.g._search_master_erp_branch_list;
                case "arm_code": return _g.g._search_screen_erp_doc_format;
                //_controlTypeEnum.Customer
                case "customer_type_code": return _g.g._search_master_customer_type;
                case "ar_channel_code": return _g.g._search_master_ar_channel;
                case "ar_location_type_code": return _g.g._search_master_ar_location_type;
                case "ar_sub_type_1_code": return _g.g._search_master_ar_sub_type_1;
                case "ar_vehicle_code": return _g.g._search_master_ar_vehicle;
                case "ar_equipment_code": return _g.g._search_master_ar_equipment;
                case "ar_sub_equipment": return _g.g._search_master_ar_sub_equipment;
                case "br_cust_code": return _g.g._search_screen_erp_doc_format;

                case "ar_project_code": return _g.g._search_master_ar_project;
                case "ar_shoptype1_code": return _g.g._search_master_ar_shoptype1;
                case "ar_shoptype2_code": return _g.g._search_master_ar_shoptype2;
                case "ar_shoptype3_code": return _g.g._search_master_ar_shoptype3;
                case "ar_shoptype4_code": return _g.g._search_master_ar_shoptype4;
                case "ar_shoptype5_code": return _g.g._search_master_ar_shoptype5;
                case "sub_ar_shoptype5_code": return _g.g._search_master_sub_ar_shoptype5;
                case "ar_shoptype6_code": return _g.g._search_master_ar_shoptype6;
                case "ar_shoptype7_code": return _g.g._search_master_ar_shoptype7;


            }
            return "";
        }
    }

    public enum _controlTypeEnum
    {
        /// <summary>
        /// ar : ลูกหนี
        /// </summary>
        Ar,
        /// <summary>
        /// ArDetail : รายละเอียดลูกหนี้
        /// </summary>
        ArDetail,
        /// <summary>
        /// ArDetail : รายละเอียดลูกหนี้วางบิล
        /// </summary>
        ArDetailBill,
        /// <summary>
        /// ArDetail : รายละเอียดลูกหนี้เคดิต
        /// </summary>
        ArDetailCredit,
        /// <summary>
        /// ArDetail : รายละเอียดกลุ่มลูกหนี้
        /// </summary>
        ArDetailGroup,
        /// <summary>
        /// ArDetail : รายละเอียดมิติลูกหนี้
        /// </summary>
        ArDetailDimension,
        /// <summary>
        /// ArDealer : รายละเอียดสมาชิก
        /// </summary>
        ArDealer,
        /// <summary>
        /// ArDealer : รายละเอียดสมาชิก Health
        /// </summary>
        ArDealerHealthy,
        /// <summary>
        /// ArDetailHealthy : ลูกหนี้ Healthy
        /// </summary>
        ArDetailHealthy,
        /// <summary>
        /// รายละเอียดภาษี
        /// </summary>
        ArTaxOnly,
        /// <summary>
        /// ลูกค้า
        /// </summary>
        Customer
    }

    public class _healthy_yourhealthy : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        //_searchProperties _searchScreenProperties = new _searchProperties();
        string _old_filed_name = "";
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        MyLib._searchDataFull _search_data_full_pointer;
        int _search_data_full_buffer_addr = -1;
        public _healthy_yourhealthy()
        {
            this._build();

        }
        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.m_yourhealthy._table;
            // toe 
            //this._addTextBox(0, 0, 0, 0, _g.d.m_yourhealthy._ar_code, 1, 0, 1, true, false, false, true);
            //this._addTextBox(0, 0, 0, 0, _g.d.m_yourhealthy._member_id, 2, 100, 0, true, false, true, true);
            this._addDateBox(0, 0, 0, 0, _g.d.m_yourhealthy._measure_date, 1, true, false);
            this._addNumberBox(1, 0, 0, 0, _g.d.m_yourhealthy._weight, 1, 1, true);
            this._addNumberBox(2, 0, 0, 0, _g.d.m_yourhealthy._high, 1, 1, true);
            this._addNumberBox(3, 0, 0, 0, _g.d.m_yourhealthy._pressure, 1, 1, true);
            this._addNumberBox(4, 0, 0, 0, _g.d.m_yourhealthy._body_fat, 1, 1, true);
            this._addNumberBox(5, 0, 0, 0, _g.d.m_yourhealthy._ldl, 1, 1, true);
            this._addNumberBox(6, 0, 0, 0, _g.d.m_yourhealthy._hdl, 1, 1, true);
            this._addNumberBox(7, 0, 0, 0, _g.d.m_yourhealthy._triglyceride, 1, 1, true);
            this._addNumberBox(8, 0, 0, 0, _g.d.m_yourhealthy._cholesterol, 1, 1, true);
            this._addNumberBox(9, 0, 0, 0, _g.d.m_yourhealthy._kkos, 1, 1, true);
            this._addNumberBox(10, 0, 0, 0, _g.d.m_yourhealthy._blood_Oxygen, 1, 1, true);
            this._addNumberBox(11, 0, 0, 0, _g.d.m_yourhealthy._bmi, 1, 1, true);
            this._addNumberBox(12, 0, 0, 0, _g.d.m_yourhealthy._blood_sugar, 1, 1, true);
            this._addNumberBox(13, 0, 0, 0, _g.d.m_yourhealthy._bun, 1, 1, true);
            this._addNumberBox(14, 0, 0, 0, _g.d.m_yourhealthy._creatinine, 1, 1, true);
            this._addTextBox(15, 0, 2, 0, _g.d.m_yourhealthy._remark, 1, 0, 0, true, false, true, true);


            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_healthy_yourhealthy__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(healthy_yourhealthy__textBoxChanged);
            foreach (Control __getControlAll in this.Controls)
            {
                if (__getControlAll.GetType() == typeof(MyLib._myTextBox))
                {
                    MyLib._myTextBox __getControlTextBox = (MyLib._myTextBox)__getControlAll;
                    if (__getControlTextBox._iconNumber == 1)
                    {
                        // กำหนดให้การค้นหาแบบจอเล็กแสดง
                        __getControlTextBox.textBox.Leave += new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.Enter += new EventHandler(textBox_Enter);
                        __getControlTextBox.textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                }
            }
            //Control codeControl = this._getControl(_g.d.m_yourhealthy._member_id);
            //codeControl.Enabled = false;
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_data_full_pointer != null)
                {
                    this._search_data_full_pointer.Visible = true;
                    this._search_data_full_pointer.Focus();
                    this._search_data_full_pointer._firstFocus();
                }
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _healthy_yourhealthy__textBoxSearch(__getControl);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }
        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.m_yourhealthy._ar_code)) return _g.g._search_screen_ar;
            return "";
        }
        string _search_screen_name_extra_where(string _name)
        {
            // if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }
        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }
        void healthy_yourhealthy__textBoxChanged(object sender, string name)
        {

            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                //__textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
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
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr(_g.d.m_yourhealthy._ar_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dealer._code + " from " + _g.d.ar_dealer._table + " where " + _g.d.ar_dealer._ar_code + "=\'" + this._getDataStr(_g.d.m_yourhealthy._ar_code) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.m_yourhealthy._ar_code, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.m_yourhealthy._member_id, (DataSet)_getData[1], warning) == false) { }

            }
            catch
            {
            }
        }
        void _healthy_yourhealthy__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _search_screen_name(this._searchName);
            string __where_query = _search_screen_name_extra_where(this._searchName);
            if (!this._old_filed_name.Equals(__getControl._name.ToLower()))
            {
                Boolean __found = false;
                this._old_filed_name = __getControl._name.ToLower();
                // jead
                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (__getControl._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(__getControl._name);
                    MyLib._searchDataFull __searchObject = new MyLib._searchDataFull();
                    this._search_data_full_buffer.Add(__searchObject);
                }
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible == true)
                {
                    this._search_data_full_pointer.Visible = false;
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                //
            }
            if (!this._search_data_full_pointer._name.Equals(_search_text_new.ToLower()))
            {
                if (this._search_data_full_pointer._name.Length == 0)
                {
                    this._search_data_full_pointer._showMode = 0;
                    this._search_data_full_pointer._name = _search_text_new;
                    this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                    this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                }
            }

            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
            }
            // jead : ทำให้ข้อมูลเรียกใหม่ทุกครั้ง ไม่ต้องมี เพราะ _startSearchBox มันดึงให้แล้ว this._search_data_full_pointer._dataList._refreshData(); เอาออกสองรอบแล้วเด้อ
            if (__getControl._iconNumber == 1)
            {
                __getControl.Focus();
                __getControl.textBox.Focus();
            }
            else
            {
                this._search_data_full_pointer.Focus();
                this._search_data_full_pointer._firstFocus();
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
        void _searchAll(string name, int row)
        {
            string _search_text_new = name;
            string __result = "";
            if (name.Length > 0)
            {
                __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                if (__result.Length != 0)
                {
                    this._search_data_full_pointer.Visible = false;
                    this._setDataStr(_searchName, __result);
                }
            }
            this._search(true);
            SendKeys.Send("{TAB}");
        }

    }

}
