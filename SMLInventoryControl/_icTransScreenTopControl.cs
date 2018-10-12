using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace SMLInventoryControl
{
    public class _icTransScreenTopControl : MyLib._myScreen
    {
        public string _icTransTable = "";
        //#screentop
        private int _buildCount = 0;
        private _g.g._transControlTypeEnum _ictransControlTypeTemp;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private _search_bill _searchBills = new _search_bill();
        //MyLib._searchDataFull _searchMasterScreen = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();

        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        public event RadioButtonCheckedChangedEventHandler _radiotButtonCheckedChanged;
        public event ChangeCreditDayEventHandler _changeCreditDay;
        public event VatCaseEventHandler _vatCase;
        public event SelectInvoiceFromPosEventHandler _selectInvoceFromPos;
        public event VatTypeEventHandler _vatType;

        string _creditDayFieldName = "creditday";
        string _old_filed_name = "";
        public string _last_cust_code = "";
        // สำหรับเตือนกรณีแก้วันที่หรือรหัสลูกค้า
        public string _last_check_doc_date = "";
        public string _last_check_cust_code = "";
        /// <summary>
        /// รหัสหน้าจอ
        /// </summary>
        public string _screen_code = "";
        public MyLib._myManageData _managerData;
        public _icTransItemGridControl _icTransItemGrid;
        public string _oldTaxDocNo = "";
        // รหัสเอกสาร เอาไว้พิมพ์ฟอร์ม
        public string _docFormatCode = "";
        public string _oldDocRef = "";

        //public _icTransScreenBottomControl _icTransScreenBottom = null;
        public delegate string BranchCodeHandler(int mode);
        public event BranchCodeHandler _getBranchCode;
        public string _menuName = "";

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                if (MyLib._myGlobal._isDesignMode == false)
                {
                    this._ictransControlTypeTemp = value;
                    this._build();
                    this.Invalidate();
                }
            }
            get
            {
                return this._ictransControlTypeTemp;
            }
        }

        public void _newData()
        {
            Boolean __fixedTime = (_g.g._transCalcTypeGlobal._transStockCalcType(this._icTransControlType) == -1) ? false : true;
            this._last_cust_code = "";
            this._last_check_doc_date = "";
            this._last_check_cust_code = "";
            this._setDataStr(_g.d.ic_trans._branch_code, MyLib._myGlobal._branchCode);
            this._setDataNumber(_g.d.ic_trans._vat_rate, _g.g._companyProfile._vat_rate);

            //
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:

                case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    __fixedTime = false;
                    break;
            }

            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:

                // toe
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                    this._setDataDate(_g.d.ic_trans._doc_date, MyLib._myGlobal._workingDate);
                    break;

                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:

                    if (MyLib._myGlobal._programName == "SML CM")
                    {
                        this._setDataDate(_g.d.ic_trans._doc_date, MyLib._myGlobal._workingDate);
                    }
                    break;
            }
            //
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    this._setDataStr(_g.d.ic_trans._user_request, MyLib._myGlobal._userCode);
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    this._setDataStr(_g.d.ic_trans._user_approve, MyLib._myGlobal._userCode);
                    __fixedTime = false;
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                    if (MyLib._myGlobal._programName == "SML CM")
                    {
                        this._setDataStr(_g.d.ic_trans._sale_code, MyLib._myGlobal._userCode);
                        this._enabedControl(_g.d.ic_trans._sale_code, false);
                    }
                    break;

            }
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    this._setComboBox(_g.d.ic_trans._vat_type, _g.g._companyProfile._vat_type_1);
                    break;
                default:
                    this._setComboBox(_g.d.ic_trans._vat_type, _g.g._companyProfile._vat_type);
                    break;
            }

            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:

                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:

                    __fixedTime = false;
                    break;
            }

            if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex") || _g.g._companyProfile._real_doc_date_doc_time)
            {
                __fixedTime = false;
            }


            Control __timeControl = this._getControl(_g.d.ic_trans._doc_time);
            if (__timeControl != null)
            {
                ((MyLib._myTextBox)__timeControl)._isTime = true;
                if (__fixedTime == false)
                {
                    Boolean __default_time = true;

                    if (_g.g._companyProfile._auto_insert_time)
                    {
                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                            case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                //case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                __default_time = false;
                                //this._enabedControl(_g.d.ic_trans._doc_time, false);
                                break;
                        }
                    }

                    if (__default_time)
                    {
                        this._setDataStr(_g.d.ic_trans._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"), "", true);
                    }
                }
                else
                {
                    this._setDataStr(_g.d.ic_trans._doc_time, "08:00", "", true);
                }
                this._isChange = false;
            }

            this._enabedControl(_g.d.ic_trans._doc_no, true);
            this._enabedControl(_g.d.ic_trans._cust_code, true);

            if (_g.g._companyProfile._running_doc_no_only == true)
            {
                ((MyLib._myTextBox)this._getControl(_g.d.ic_trans._doc_no)).textBox.Enabled = false;
            }

            if (_g.g._companyProfile._disabled_edit_doc_no_doc_date == true || _g.g._companyProfile._disable_edit_doc_no_doc_date_user == true)
            {
                this._setDataDate(_g.d.ic_trans._doc_date, MyLib._myGlobal._workingDate);
                this._setDataStr(_g.d.ic_trans._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"), "", true);

                //this._enabedControl(_g.d.ic_trans._doc_no, false);
                this._enabedControl(_g.d.ic_trans._doc_date, false);
                this._enabedControl(_g.d.ic_trans._doc_time, false);
                ((MyLib._myTextBox)this._getControl(_g.d.ic_trans._doc_no)).textBox.Enabled = false;
            }

        }

        /// <summary>
        /// คำนวณยอด
        /// </summary>
        public void _calc()
        {
            decimal __totalAmount = 0;
            decimal __vatRate = this._getDataNumber(_g.d.ic_trans._vat_rate);
            decimal __totalValue = 0;
            decimal __vatValue = 0;
            this._enabedControl(_g.d.ic_trans._total_value, false);
            this._enabedControl(_g.d.ic_trans._total_amount, false);
            switch (this._vatType())
            {
                case _g.g._vatTypeEnum.ภาษีแยกนอก:
                    __totalValue = this._getDataNumber(_g.d.ic_trans._total_value);
                    __vatValue = MyLib._myGlobal._round((__totalValue * __vatRate) / 100.0M, _g.g._companyProfile._item_amount_decimal);
                    __totalAmount = __totalValue + __vatValue;
                    this._setDataNumber(_g.d.ic_trans._total_vat_value, __vatValue);
                    this._setDataNumber(_g.d.ic_trans._total_amount, __totalAmount);
                    this._enabedControl(_g.d.ic_trans._total_value, true);
                    break;
                case _g.g._vatTypeEnum.ภาษีรวมใน:
                    __totalAmount = this._getDataNumber(_g.d.ic_trans._total_amount);
                    __vatValue = MyLib._myGlobal._round((__totalAmount * __vatRate) / (__vatRate + 100.0M), _g.g._companyProfile._item_amount_decimal);
                    __totalValue = __totalAmount - __vatValue;
                    this._setDataNumber(_g.d.ic_trans._total_vat_value, __vatValue);
                    this._setDataNumber(_g.d.ic_trans._total_value, __totalValue);
                    this._enabedControl(_g.d.ic_trans._total_amount, true);
                    break;
                case _g.g._vatTypeEnum.ยกเว้นภาษี:
                    this._setDataNumber(_g.d.ic_trans._total_vat_value, __vatValue);
                    this._setDataNumber(_g.d.ic_trans._total_value, __totalValue);
                    this._enabedControl(_g.d.ic_trans._total_amount, true);
                    break;
            }
        }

        //somruk
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            switch (keyData)
            {
                case Keys.F8:
                    if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
                    {
                        // ดึง POS มาออกใบกำกับภาษีแบบเต็ม
                        if (this._selectInvoceFromPos != null)
                        {
                            this._selectInvoceFromPos();
                        }
                    }
                    else if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ)
                    {
                        // ดึง POS มาออกใบกำกับภาษีแบบเต็ม
                        if (this._selectInvoceFromPos != null)
                        {
                            this._selectInvoceFromPos();
                        }

                    }
                    else if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                    {
                        // ดึง POS มาออกใบกำกับภาษีแบบเต็ม
                        if (this._selectInvoceFromPos != null)
                        {
                            this._selectInvoceFromPos();
                        }

                    }
                    return true;
                case Keys.F9:
                    _ictransScreenTopControl__textBoxSearch(this._getControl(_g.d.ic_trans._doc_format_code));
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _build()
        {
            if (this._icTransControlType == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            //
            this._buildCount++;
            if (this._buildCount > 1)
            {
                MessageBox.Show("TransScreenTop : มีการสร้างจอสองครั้ง");
            }
            //
            string[] __inquiryType = { _g.d.ic_trans._credit_purchase, _g.d.ic_trans._cash_purchase, _g.d.ic_trans._credit_purchase_service, _g.d.ic_trans._cash_purchase_service };
            int __row = 0;
            this.SuspendLayout();
            this._reset();
            //
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_ictransScreenTopControl__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_ictransScreenTopControl__textBoxChanged);
            this._textBoxSaved += new MyLib.TextBoxSavedHandler(_ictransScreenTopControl__textBoxSaved);
            this._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(_ictransScreenTopControl__comboBoxSelectIndexChanged);
            //this._afterClear += _icTransScreenTopControl__afterClear;
            //
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this._table_name = _g.d.ic_trans._table;
            this._maxColumn = 2;
            switch (this._icTransControlType)
            {
                #region ซื้อ_เสนอซื้อ
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ap_code);
                    this._addCheckBox(__row++, 1, _g.d.ic_trans._on_hold, true, false);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, __inquiryType, true);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._approve_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //this._addCheckBox(7, 0, _g.d.ic_trans._send_mail, true, false);
                    //this._addCheckBox(7, 1, _g.d.ic_trans._send_sms, true, false);
                    //
                    this._enabedControl(_g.d.ic_trans._user_request, false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 0, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addComboBox(3, 0, _g.d.ic_trans._inquiry_type, true, __inquiryType, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._doc_po_request);
                    this._addTextBox(4, 1, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 0, true, false, true, true, true, _g.d.ic_trans._user_cancel);
                    this._addComboBox(5, 0, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._user_approve, false);
                    this._enabedControl(_g.d.ic_trans._vat_type, false);
                    this._enabedControl(_g.d.ic_trans._inquiry_type, false);
                    this._enabedControl(_g.d.ic_trans._user_request, false);
                    this._enabedControl(_g.d.ic_trans._department_code, false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 0, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_po_request);
                    this._addTextBox(3, 1, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 0, true, false, true);
                    this._addCheckBox(4, 1, _g.d.ic_trans._not_approve_1, true, false, false);
                    this._addComboBox(5, 0, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addComboBox(5, 1, _g.d.ic_trans._inquiry_type, true, __inquiryType, true);
                    this._addTextBox(6, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._user_approve, false);
                    this._enabedControl(_g.d.ic_trans._vat_type, false);
                    this._enabedControl(_g.d.ic_trans._inquiry_type, false);
                    this._enabedControl(_g.d.ic_trans._user_request, false);
                    break;
                #endregion
                #region ซื้อ_ใบสั่งซื้อ
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    this._maxColumn = 4;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row++, 2, 1, 0, _g.d.ic_trans._contactor, 2, 1, 1, true, false, true, true, true);

                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, __inquiryType, true);
                    this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    //
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._user_request, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._approve_code, 1, 1, 1, true, false, true);
                        this._enabedControl(_g.d.ic_trans._user_request, false);
                    }
                    /*this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);*/
                    //
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                    this._maxColumn = 4;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(0, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(0, 3, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_no_po);
                    this._addTextBox(1, 3, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                    this._addTextBox(1, 4, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 1, true, false, true);
                    this._addCheckBox(2, 0, _g.d.ic_trans._not_approve_1, true, false, false);
                    this._addComboBox(2, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addComboBox(2, 2, _g.d.ic_trans._inquiry_type, true, __inquiryType, true);
                    this._addTextBox(3, 0, 2, 0, _g.d.ic_trans._remark, 4, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._user_request, false);
                    this._enabedControl(_g.d.ic_trans._user_approve, false);
                    this._enabedControl(_g.d.ic_trans._vat_type, false);
                    this._enabedControl(_g.d.ic_trans._inquiry_type, false);
                    this._enabedControl(_g.d.ic_trans._user_request, false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    {
                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_no_po);
                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_date_po);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                        MyLib._myGroupBox __groupBox = this._addGroupBox(__row++, 0, 2, 1, 1, _g.d.ic_trans._cancel_type, true);
                        this._addRadioButtonOnGroupBox(0, 0, __groupBox, _g.d.ic_trans._cancel_type_1, 1, true).CheckedChanged += new EventHandler(_icTransScreenTopControl_CheckedChanged);
                        this._addRadioButtonOnGroupBox(1, 0, __groupBox, _g.d.ic_trans._cancel_type_2, 2, false).CheckedChanged += new EventHandler(_icTransScreenTopControl_CheckedChanged);
                        __row += 2;
                        this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        //
                        this._enabedControl(_g.d.ic_trans._cust_code, false);
                        this._enabedControl(_g.d.ic_trans._user_request, false);
                        this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                        this._enabedControl(_g.d.ic_trans._user_approve, false);
                    }
                    break;
                #endregion
                #region ซื้อ_พาเชียล_รับสินค้า
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    break;
                #endregion
                #region ซื้อ_ซื้อสินค้า
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    this._maxColumn = 4;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addDateBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._purchaseType, true);
                    this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addDateBox(__row, 2, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 2, 1, 1, true, false, true);
                    this._addTextBox(__row++, 2, 1, 0, _g.d.ic_trans._location_from, 2, 1, 1, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_purchase_no);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_purchase_date);

                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ซื้อ_ซื้อสินค้า
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._purchaseType, true);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    break;
                #endregion
                #region ซื้อ_พาเชียล_ลดหนี้
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._payable_sub_type_1, _g.d.ic_trans._payable_sub_type_2, _g.d.ic_trans._payable_sub_type_3, _g.d.ic_trans._payable_sub_type_4 }, true, _g.d.ic_trans._payable_add_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                #endregion
                #region ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._payable_sub_type_1, _g.d.ic_trans._payable_sub_type_2, _g.d.ic_trans._payable_sub_type_3, _g.d.ic_trans._payable_sub_type_4 }, true, _g.d.ic_trans._payable_add_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_purchase_credit_no);
                    this._addDateBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_purchase_credit_date);

                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ซื้อ_พาเชียล_ราคาผิด
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._payable_sub_type_1_1, _g.d.ic_trans._payable_sub_type_1_2 }, true, _g.d.ic_trans._payable_add_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    break;
                #endregion
                #region ซื้อ_พาเชียล_เพิ่มหนี้
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._payable_add_type_1, _g.d.ic_trans._payable_add_type_2 }, true, _g.d.ic_trans._payable_add_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    break;
                #endregion
                #region ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._payable_add_type_1, _g.d.ic_trans._payable_add_type_2 }, true, _g.d.ic_trans._payable_add_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_purchase_debit_no);
                    this._addDateBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_purchase_debit_date);

                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ซื้อ_จ่ายเงินมัดจำ
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._buy_deposit2);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_advance_in_date);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    //this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_ref_name_2);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_advance_in_date);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    //this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ซื้อ_จ่ายเงินล่วงหน้า
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    //
                    this._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber, true, _g.d.ic_trans._total_before_vat);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._total_value, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                    this._calc();
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_advance_in_no);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_advance_in_date);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    //this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_ref_name_1);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_advance_in_date);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    //this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region เจ้าหนี้_อื่น
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    break;
                #endregion
                #region ลูกหนี้_อื่น
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row++, 0, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);

                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    break;
                #endregion
                #region เงินสดธนาคาร_รายจ่ายอื่น
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._credit_purchase_other, _g.d.ic_trans._cash_purchase_other, _g.d.ic_trans._cash_purchase_service_other, _g.d.ic_trans._credit_purchase_service_other }, true);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._reduction_expense_other_credit, _g.d.ic_trans._reduction_expense_other_cash, _g.d.ic_trans._reduction_expense_other_credit_service, _g.d.ic_trans._reduction_expense_other_cash_service }, true);
                    this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._invoice_add_expense_other_credit, _g.d.ic_trans._invoice_add_expense_other_cash, _g.d.ic_trans._invoice_add_expense_other_credit_service, _g.d.ic_trans._invoice_add_expense_other_cash_service }, true);
                    this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region เงินสดธนาคาร_รายได้อื่น
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._contactor, 1, 1, 1, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._saleOtherType, true, _g.d.ic_trans._sale_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._ret_credit_other, _g.d.ic_trans._ret_cash_other, _g.d.ic_trans._ret_credit_service_other, _g.d.ic_trans._ret_cash_service_other }, true, _g.d.ic_trans._return_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._invoice_add_credit_other, _g.d.ic_trans._invoice_add_cash_other, _g.d.ic_trans._invoice_add_credit_service_other, _g.d.ic_trans._invoice_add_cash_service_other }, true, _g.d.ic_trans._sale_add_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);

                    break;
                #endregion
                #region ขาย_ใบเสนอราคา
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    this._maxColumn = 4;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row++, 2, 1, 0, _g.d.ic_trans._contactor, 2, 1, 1, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._saleType, true, _g.d.ic_trans._sale_type);
                    this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._user_request, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 3, 1, 0, _g.d.ic_trans._approve_code, 1, 1, 1, true, false, true);
                        this._enabedControl(_g.d.ic_trans._user_request, false);
                    }
                    __row++;
                    this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 4, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                    {
                        this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                        this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                        this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                        this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 1, true, false, true);
                        this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                        this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                        this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                        this._addTextBox(3, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_quotation_no);
                        this._addCheckBox(4, 1, _g.d.ic_trans._not_approve_1, true, false, false);
                        this._addComboBox(4, 0, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                        this._addComboBox(5, 0, _g.d.ic_trans._inquiry_type, true, _g.g._saleType, true, _g.d.ic_trans._sale_type);
                        this._addTextBox(6, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        //
                        this._enabedControl(_g.d.ic_trans._cust_code, false);
                        this._enabedControl(_g.d.ic_trans._user_request, false);
                        this._enabedControl(_g.d.ic_trans._user_approve, false);
                        this._enabedControl(_g.d.ic_trans._vat_type, false);
                        this._enabedControl(_g.d.ic_trans._inquiry_type, false);
                        this._enabedControl(_g.d.ic_trans._user_request, false);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                    {
                        this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                        this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                        this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                        this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 1, true, false, true);
                        this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                        this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                        this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                        this._addTextBox(3, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_quotation_no);
                        this._addComboBox(4, 0, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                        this._addComboBox(4, 1, _g.d.ic_trans._inquiry_type, true, _g.g._saleType, true, _g.d.ic_trans._sale_type);
                        this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        //
                        this._enabedControl(_g.d.ic_trans._cust_code, false);
                        this._enabedControl(_g.d.ic_trans._user_request, false);
                        this._enabedControl(_g.d.ic_trans._user_approve, false);
                        this._enabedControl(_g.d.ic_trans._vat_type, false);
                        this._enabedControl(_g.d.ic_trans._inquiry_type, false);
                        this._enabedControl(_g.d.ic_trans._user_request, false);
                    }
                    break;
                #endregion
                #region ขาย_สั่งจองสินค้าและสั่งซื้อสินค้า
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    this._maxColumn = 4;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);

                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._contactor, 1, 1, 1, true, false, true);
                    this._addDateBox(__row, 2, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);

                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._saleType, true, _g.d.ic_trans._sale_type);
                    this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);
                    if (MyLib._myGlobal._programName.Equals("SML CM"))
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref_trans, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._doc_advance_so_no);
                    }
                    this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 4, 1, 0, true, false, true);

                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_no_po);
                    this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 1, true, false, true);
                    this._addCheckBox(4, 1, _g.d.ic_trans._not_approve_1, true, false, false);
                    this._addComboBox(5, 0, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addComboBox(5, 1, _g.d.ic_trans._inquiry_type, true, __inquiryType, true);
                    this._addTextBox(6, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._user_request, false);
                    this._enabedControl(_g.d.ic_trans._user_approve, false);
                    this._enabedControl(_g.d.ic_trans._vat_type, false);
                    this._enabedControl(_g.d.ic_trans._inquiry_type, false);
                    this._enabedControl(_g.d.ic_trans._user_request, false);
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                    {
                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_no_po);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_date_po);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                        MyLib._myGroupBox __groupBox = this._addGroupBox(__row++, 0, 2, 1, 1, _g.d.ic_trans._cancel_type, true);
                        this._addRadioButtonOnGroupBox(0, 0, __groupBox, _g.d.ic_trans._cancel_type_1, 1, true).CheckedChanged += new EventHandler(_icTransScreenTopControl_CheckedChanged);
                        this._addRadioButtonOnGroupBox(1, 0, __groupBox, _g.d.ic_trans._cancel_type_2, 2, false).CheckedChanged += new EventHandler(_icTransScreenTopControl_CheckedChanged);
                        __row += 2;
                        this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        //
                        this._enabedControl(_g.d.ic_trans._cust_code, false);
                        this._enabedControl(_g.d.ic_trans._user_request, false);
                        this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                        this._enabedControl(_g.d.ic_trans._user_approve, false);
                    }
                    break;
                #endregion
                #region ขาย_ใบสั่งขาย
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    this._maxColumn = 4;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);

                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._contactor, 1, 1, 1, true, false, true);
                    this._addDateBox(__row, 2, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);

                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._saleType, true, _g.d.ic_trans._sale_type);
                    this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 1, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_saleorder_no);
                    this._addCheckBox(4, 0, _g.d.ic_trans._not_approve_1, true, false, false);
                    this._addComboBox(4, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addComboBox(5, 0, _g.d.ic_trans._inquiry_type, true, __inquiryType, true);
                    //this._addTextBox(6, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._user_request, false);
                    this._enabedControl(_g.d.ic_trans._user_approve, false);
                    this._enabedControl(_g.d.ic_trans._vat_type, false);
                    this._enabedControl(_g.d.ic_trans._inquiry_type, false);
                    this._enabedControl(_g.d.ic_trans._user_request, false);
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    {
                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_saleorder_date);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_saleorder_no);
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._user_request, 1, 1, 0, true, false, true);
                        MyLib._myGroupBox __groupBox = this._addGroupBox(__row++, 0, 2, 1, 1, _g.d.ic_trans._cancel_type, true);
                        this._addRadioButtonOnGroupBox(0, 0, __groupBox, _g.d.ic_trans._cancel_type_1, 1, true).CheckedChanged += new EventHandler(_icTransScreenTopControl_CheckedChanged);
                        this._addRadioButtonOnGroupBox(1, 0, __groupBox, _g.d.ic_trans._cancel_type_2, 2, false).CheckedChanged += new EventHandler(_icTransScreenTopControl_CheckedChanged);
                        __row += 2;
                        //this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        //
                        this._enabedControl(_g.d.ic_trans._cust_code, false);
                        this._enabedControl(_g.d.ic_trans._user_request, false);
                        this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                        this._enabedControl(_g.d.ic_trans._user_approve, false);
                    }
                    break;
                #endregion
                #region ขาย_รับเงินล่วงหน้า
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_advance_so_no);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_advance_so_date);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ขาย_คืนเงินล่วงหน้า
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_advance_so_return_no);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_advance_so_return_date);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ขาย_รับเงินมัดจำ
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._deposit_doc);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._deposit_doc_date);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ขาย_คืนเงินมัดจำ
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._deposit_return_doc);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._deposit_return_doc_date);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ขาย_ขายสินค้าและบริการ
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                    {
                        this._maxColumn = 2;
                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);

                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);

                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 3, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);

                        this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._saleType, true, _g.d.ic_trans._sale_type);
                        this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);

                    }
                    else
                    {
                        this._maxColumn = 4;
                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                        this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                        this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                        this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);

                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                        this._addTextBox(__row++, 2, 1, 0, _g.d.ic_trans._contactor, 2, 1, 1, true, false, true);

                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                        this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 3, true, false, true);
                        this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                        this._addDateBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);

                        this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._saleType, true, _g.d.ic_trans._sale_type);
                        this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                        this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_invoice_no);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_invoice_date);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ขาย_รับคืนสินค้าจากการขายและลดหนี้
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 3, true, false, true);

                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);

                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._ret_credit, _g.d.ic_trans._ret_cash, _g.d.ic_trans._ret_credit_1, _g.d.ic_trans._ret_cash_1, _g.d.ic_trans._ret_credit_service, _g.d.ic_trans._ret_cash_service }, true, _g.d.ic_trans._sale_return_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_ar_credit_note_no);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_ar_credit_note_date);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ขาย_เพิ่มหนี้
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._doc_ar_debit_note_no);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true, _g.d.ic_trans._doc_ar_debit_note_date);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ขาย_เพิ่มหนี้
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 3, true, false, true);

                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);

                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);
                    this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._invoice_add_credit, _g.d.ic_trans._invoice_add_credit_1, _g.d.ic_trans._invoice_add_credit_service }, true, _g.d.ic_trans._sale_add_type);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    break;
                #endregion
                #region ระบบคลังสินค้า
                case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 0, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 0, true, false, true);
                    this._addCheckBox(__row, 1, _g.d.ic_trans._recheck_count, true, false);
                    this._addNumberBox(__row++, 0, 1, 0, _g.d.ic_trans._recheck_count_day, 1, 2, true, __formatNumber);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ap_code);
                    this._addComboBox(__row++, 1, _g.d.ic_trans._inquiry_type, true, new string[] { _g.d.ic_trans._wh_in_1, _g.d.ic_trans._wh_in_2, _g.d.ic_trans._wh_in_3, _g.d.ic_trans._wh_in_4, _g.d.ic_trans._wh_in_5 }, true, _g.d.ic_trans._wh_in);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addNumberBox(__row++, 0, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addCheckBox(__row, 0, _g.d.ic_trans._recheck_count, true, false);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._recheck_count_day, 1, 2, true, __formatNumber);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;

                case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 4, true, false, true, true, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;

                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 4, true, false, true, true, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    //this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 4, true, false, true, true, true);

                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    //this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                #endregion
                #region ระบบสต๊อกสินค้า
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    //    this._addNumberBox(__row++, 0, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber); anek
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    //    this._addNumberBox(__row++, 0, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber); anek
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                    // ยอดสินค้าคงเหลือยกมา (ยอดยกมา)
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addNumberBox(__row++, 0, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.StockCheck:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.StockCheckResult:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    if (MyLib._myGlobal._mainMenuCodePassTrue.Equals("menu_ic_finish_receive_ar"))
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ar_code);
                    }
                    else if (MyLib._myGlobal._mainMenuCodePassTrue.Equals("menu_ic_finish_receive_ap"))
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ap_code);
                    }
                    else
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ap_code);
                    }

                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addNumberBox(__row++, 0, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ)
                    {
                        if (MyLib._myGlobal._mainMenuCodePassTrue.Equals("menu_ic_issue_ar"))
                        {
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ar_code);
                        }
                        else if (MyLib._myGlobal._mainMenuCodePassTrue.Equals("menu_ic_issue_ap"))
                        {
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, true, true, true, _g.d.ic_trans._ap_code);
                        }
                        else
                        {
                            this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, true, true, true, _g.d.ic_trans._ar_code);
                        }
                    }
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    //
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, true, true, true, _g.d.ic_trans._ar_code);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);

                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true, true, false, _g.d.ic_trans._user_request_transfer);

                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_to, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_to, 1, 1, 1, true, false, true);
                    this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ระบบเงินสดย่อย
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                #endregion
                #region ธนาคาร
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._pass_book_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;

                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._pass_book_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);

                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._pass_book_code, 1, 1, 1, true, false, true);

                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);

                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);

                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false, true, true);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    //
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);

                    break;

                #endregion
                case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);

                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true, true, false, _g.d.ic_trans._user_request_transfer);

                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_to, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_to, 1, 1, 1, true, false, true);
                    this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    break;

                default:
                    if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                    {
                        MessageBox.Show("screentop Case not found : " + this._icTransControlType);
                    }
                    break;
            }
            this._enabedControl(_g.d.ic_trans._doc_format_code, false);
            //
            // toe เอา รหัสเจ้าหนี้ออกจาก recent
            //this._setRecent(_g.d.ic_trans._cust_code, true);
            this._setRecent(_g.d.ic_trans._wh_from, true);
            this._setRecent(_g.d.ic_trans._wh_to, true);
            this._setRecent(_g.d.ic_trans._location_from, true);
            this._setRecent(_g.d.ic_trans._location_to, true);
            this._setRecent(_g.d.ic_trans._inquiry_type, true);
            this._setRecent(_g.d.ic_trans._vat_type, true);

            /*if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
            {
                this._setRecent(_g.d.ic_trans._doc_date, true);
                this._setRecent(_g.d.ic_trans._doc_format_code, true);
            }*/

            this.Invalidate();
            this.ResumeLayout();
            //
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
                        __getControlTextBox.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
                    }
                }
            }

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                if (this._getControl(_g.d.ic_trans._wh_from) != null)
                    ((MyLib._myTextBox)this._getControl(_g.d.ic_trans._wh_from)).textBox.Enabled = false;

                if (this._getControl(_g.d.ic_trans._wh_to) != null)
                    ((MyLib._myTextBox)this._getControl(_g.d.ic_trans._wh_to)).textBox.Enabled = false;

                if (this._getControl(_g.d.ic_trans._location_from) != null)
                    ((MyLib._myTextBox)this._getControl(_g.d.ic_trans._location_from)).textBox.Enabled = false;

                if (this._getControl(_g.d.ic_trans._location_to) != null)
                    ((MyLib._myTextBox)this._getControl(_g.d.ic_trans._location_to)).textBox.Enabled = false;

            }
        }

        // ไม่ work event มันทำงาน ต้อง run doc ล่าสุดเอง ย้ายไปทำหลัง Save เอา
        /*private void _icTransScreenTopControl__afterClear(object sender)
        {
            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
            {
                string __getDocFormat = this._getDataStr(_g.d.ic_trans._doc_format_code);
                // this._setRecent(_g.d.ic_trans._doc_no, true);
                if (__getDocFormat.Length > 0)
                {
                    // ไม่ work event มันทำงาน ต้อง run doc ล่าสุดเอง
                    // this._setDataStr(_g.d.ic_trans._doc_no, __getDocFormat);
                    // this._isChange = false;
                }
            }

        }*/

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                TextBox __textBox = (TextBox)sender;
                this._search_data_full_pointer._dataList._searchText.TextBox.Text = __textBox.Text;
                this._search_data_full_pointer._dataList._refreshData();
            }
        }

        void _icTransScreenTopControl_CheckedChanged(object sender, EventArgs e)
        {
            if (_radiotButtonCheckedChanged != null)
            {
                _radiotButtonCheckedChanged(sender);
            }
        }

        void _ictransScreenTopControl__comboBoxSelectIndexChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_trans._vat_type))
            {
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                        _calc();
                        break;
                }
            }
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
            if (__getControl._name.Equals(_g.d.ic_trans._doc_ref_trans) == false)
            {
                Boolean __found = false;
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
                    this._search_data_full_buffer.Add((MyLib._searchDataFull)new MyLib._searchDataFull());
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                _ictransScreenTopControl__textBoxSearch(__getControl);
            }
            __getControl.textBox.Focus();
        }

        /*void xtextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible)
                {
                    this._search_data_full_pointer.Focus();
                    this._search_data_full_pointer._firstFocus();
                }
                if (this._searchBills.Visible)
                {
                    _searchBills.Focus();
                }
            }
        }*/

        void textBox_Leave(object sender, EventArgs e)
        {
            //_searchMasterScreen.Visible = false;
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
            if (this._searchBills != null)
            {
                this._searchBills.Visible = false;
            }
        }

        void _ictransScreenTopControl__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((TextBox)sender).Parent;


            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;

            if (name.Equals(_g.d.ic_trans._tax_doc_no))
            {
                // ตรวจสอบใบกำกับภาษีห้ามซ้ำ
                string __taxDocNo = this._getDataStr(_g.d.ic_trans._tax_doc_no).Trim().ToUpper();
                string __custCode = this._getDataStr(_g.d.ic_trans._cust_code).Trim().ToUpper();
                int __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType);
                if (__taxDocNo.Length > 0 && __taxDocNo.Equals(this._oldTaxDocNo.ToUpper()) == false)
                {
                    string __query = "";
                    if (this._vatCase(this) == 0)
                    {
                        DateTime __vatDocDate = this._getDataDate(_g.d.ic_trans._tax_doc_date);
                        int __year = __vatDocDate.Year + MyLib._myGlobal._year_add;
                        // ภาษีซื้อ
                        //__query = "select " + _g.d.gl_journal_vat_buy._vat_doc_no + " from " + _g.d.gl_journal_vat_buy._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_vat_buy._vat_doc_no) + "=\'" + __taxDocNo + "\' and " + MyLib._myGlobal._addUpper(_g.d.gl_journal_vat_buy._ap_code) + "=\'" + __custCode + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + __transFlag.ToString();
                        // แก้ให้ตรวจ เฉพาะเลขที่ใบกำกับภาษีซื้อ
                        __query = "select " + _g.d.gl_journal_vat_buy._vat_doc_no + " from " + _g.d.gl_journal_vat_buy._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_vat_buy._vat_doc_no) + "=\'" + __taxDocNo + "\' " + ((__custCode.Length > 0) ? "  and " + MyLib._myGlobal._addUpper(_g.d.gl_journal_vat_buy._ap_code) + "=\'" + __custCode + "\' " : "") + " and  " + _g.d.gl_journal_vat_buy._vat_effective_year + "=" + __year + " and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + __transFlag.ToString();
                    }
                    else
                    {
                        // ภาษีขาย
                        __query = "select " + _g.d.gl_journal_vat_sale._vat_number + " from " + _g.d.gl_journal_vat_sale._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_vat_sale._vat_number) + "=\'" + __taxDocNo + "\'";
                    }
                    DataTable __getTaxNo = __myFrameWork._queryShort(__query).Tables[0];
                    if (__getTaxNo.Rows.Count > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("เลขที่ใบกำกับภาษีซ้ำ"));
                        this._setDataStr(_g.d.ic_trans._tax_doc_no, this._oldTaxDocNo, "", true);
                    }
                }
            }
            else if (name.Equals(_g.d.ic_trans._doc_no))
            {
                if (this._getDataStr(_g.d.ic_trans._tax_doc_date).ToString().Length == 0)
                {
                    this._setDataDate(_g.d.ic_trans._tax_doc_date, (this._getDataDate(_g.d.ic_trans._doc_date)));
                }

                // ต้องป้อนตัวหน้าก่อน เพื่อจะหารูปแบบเอกสาร
                string __docNo = this._getDataStr(_g.d.ic_trans._doc_no);
                string __docType = __docNo;

                // กรณี running จาก POS ไป query เอารหัสเอกสารมา
                if ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite) && _g.g._companyProfile._deposit_format_from_pos == true &&
                (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก))
                {
                    // ดึงรหัสเอกสาร
                    DataSet __dataResult = __myFrameWork._queryShort("select * from " + _g.d.POS_ID._table + " where " + MyLib._myGlobal._addUpper(_g.d.POS_ID._MACHINECODE) + "=\'" + __docNo.ToUpper() + "\'");
                    if (__dataResult.Tables.Count > 0 && __dataResult.Tables[0].Rows.Count > 0)
                    {
                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                                __docNo = __dataResult.Tables[0].Rows[0][_g.d.POS_ID._doc_format_deposit_return].ToString();
                                break;
                            case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                                __docNo = __dataResult.Tables[0].Rows[0][_g.d.POS_ID._doc_format_deposit_return_cancel].ToString();
                                break;

                        }
                    }
                    else
                    {
                        __docNo = "";
                    }
                }

                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._tax_format + "," + _g.d.erp_doc_format._doc_running + "," + _g.d.erp_doc_format._vat_type + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    this._docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __docFormatCodeTemp = ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite) && _g.g._companyProfile._deposit_format_from_pos == true &&
(this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก)) ? __docType : _docFormatCode;

                    string __startRunningNumber = __getFormat.Rows[0][_g.d.erp_doc_format._doc_running].ToString();
                    string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docFormatCodeTemp, this._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, this._icTransControlType, _g.g._transControlTypeEnum.ว่าง, this._icTransTable, __startRunningNumber);
                    string __taxFormat = __getFormat.Rows[0][_g.d.erp_doc_format._tax_format].ToString();

                    // toe แทรก tax doc no ไปก่อน
                    if (_docFormatCode.Length > 0 && __taxFormat.Length > 0 && this._getDataStr(_g.d.ic_trans._tax_doc_date).ToString().Length > 0)
                    {
                        if (__taxFormat.Trim().Length > 0)
                        {
                            // new tax auto run

                            string __newTaxDoc = _g.g._getAutoRun(_g.g._autoRunType.ใบกำกับภาษี, _docFormatCode, this._getDataStr(_g.d.ic_trans._doc_date).ToString(), __taxFormat, this._icTransControlType, _g.g._transControlTypeEnum.ว่าง, this._icTransTable);
                            this._setDataStr(_g.d.ic_trans._tax_doc_no, __newTaxDoc);
                        }
                    }

                    this._setDataStr(_g.d.ic_trans._doc_no, __newDoc, "", true);
                    this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);

                    if (MyLib._myGlobal._OEMVersion.Equals("imex") && MyLib._myGlobal._intPhase(__getFormat.Rows[0][_g.d.erp_doc_format._vat_type].ToString()) != 0)
                    {
                        // set tax type
                        int __taxType = MyLib._myGlobal._intPhase(__getFormat.Rows[0][_g.d.erp_doc_format._vat_type].ToString()) - 1;
                        this._setComboBox(_g.d.ic_trans._vat_type, __taxType);
                    }
                }
                if (this._docFormatCode.Trim().Length == 0)
                {
                    string __firstString = MyLib._myGlobal._firstString(__docNo);
                    if (__firstString.Length > 0)
                    {
                        this._docFormatCode = __firstString;
                        this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }
                if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                {
                    if (_g.g._companyProfile._doc_sale_tax_number_fixed && this._managerData._mode == 1)
                    {
                        // เลขที่ใบกำกับตรงกับเลขที่เอกสาร
                        this._setDataDate(_g.d.ic_trans._tax_doc_date, this._getDataDate(_g.d.ic_trans._doc_date));
                        this._setDataStr(_g.d.ic_trans._tax_doc_no, this._getDataStr(_g.d.ic_trans._doc_no), "", true);
                    }
                }
                /*if (this._docFormatCode.Equals(MyLib._myGlobal._firstString(__docNo)) == false)
                {
                    DialogResult __message = MessageBox.Show("ประเภทเอกสารไม่สัมพันธ์กับเลขที่เอกสาร ต้องการเปลี่ยนตามเลขที่เอกสารเลยหรือไม่", "Doc Number", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    if (__message == DialogResult.Yes)
                    {
                        this._docFormatCode = MyLib._myGlobal._firstString(__docNo);
                        this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }*/
            }
            else
     if (name.Equals(_g.d.ic_trans._doc_group) || name.Equals(_g.d.ic_trans._doc_ref) || name.Equals(_g.d.ic_trans._sale_code) || name.Equals(_g.d.ic_trans._sale_group) ||
         name.Equals(_g.d.ic_trans._department_code) || name.Equals(_g.d.ic_trans._adjust_reason))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = __getControl._label._field_name;
                this._search(true);
            }
            else
         if (__getControl._name.Equals(_g.d.ic_trans._expire_day))
            {
                if (this._getDataNumber(_g.d.ic_trans._expire_day) > 0)
                {
                    DateTime dDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._doc_date));
                    if ((dDate.Year > 1979))
                    {
                        DateTime newDate = dDate.AddDays((double)this._getDataNumber(_g.d.ic_trans._expire_day));
                        this._setDataDate(_g.d.ic_trans._expire_date, newDate);
                    }
                }
            }
            else
             if (__getControl._name.Equals(_g.d.ic_trans._expire_date))
            {
                DateTime dDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._doc_date));
                if ((dDate.Year > 1979))
                {
                    TimeSpan ts = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._expire_date)) - dDate;
                    this._setDataNumber(_g.d.ic_trans._expire_day, MyLib._myGlobal._decimalPhase(ts.Days.ToString()));
                }
            }
            else
                 //somruk
                 if (__getControl._name.Equals(_g.d.ic_trans._doc_format_code))
            {
                string __docNo = this._getDataStr(_g.d.ic_trans._doc_format_code);
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    this._docFormatCode = __getFormat.Rows[0][1].ToString();
                    this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                }
            }
            /*else
                //toe 
                if (__getControl._name.Equals(_g.d.ic_trans._cust_code))
                {
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            {
                                // check default sale 
                                string __cust_code = this._getDataStr(_g.d.ic_trans._cust_code);
                                DataTable __getDefaultSale = __myFrameWork._queryShort("select " + _g.d.ar_customer_detail._sale_code + "," + _g.d.ar_customer_detail._discount_bill + "," + _g.d.ar_customer_detail._discount_item + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + "=\'" + __cust_code + "\'").Tables[0];
                                if (__getDefaultSale.Rows.Count > 0)
                                {
                                    string __saleCode = __getDefaultSale.Rows[0][0].ToString();
                                    if (__saleCode.Length > 0)
                                    {
                                        this._setDataStr(_g.d.ic_trans._sale_code, __saleCode, "", true);
                                    }
                                }
                            }
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                            {
                                string __cust_code = this._getDataStr(_g.d.ic_trans._cust_code);
                                DataTable __getDefaultSale = __myFrameWork._queryShort("select " + _g.d.ap_supplier_detail._discount_bill + "," + _g.d.ap_supplier_detail._discount_item + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._ap_code + "=\'" + __cust_code + "\'").Tables[0];
                                if (__getDefaultSale.Rows.Count > 0)
                                {
                                    string __saleCode = __getDefaultSale.Rows[0][0].ToString();
                                    if (__saleCode.Length > 0)
                                    {
                                        this._setDataStr(_g.d.ic_trans._sale_code, __saleCode, "", true);
                                    }
                                }

                            }
                            break;
                    }

                }*/

            // toe เพิ่ม 20130311
            if ((__textBox.GetType() != typeof(MyLib._myDateBox) && __textBox.GetType() != typeof(MyLib._myNumberBox)) && __textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                __textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
            }

            // toe เพิ่มหากเป็น textsearch ได้ให้ search
            // && __textBox._iconNumber != 0 
            if (__textBox.GetType() != typeof(MyLib._myDateBox) && __textBox.GetType() != typeof(MyLib._myNumberBox))
            {
                this._search(true);
            }
        }

        void _ictransScreenTopControl__textBoxSaved(object sender, string name)
        {
            string __docDate = this._getDataStr(_g.d.ic_trans._doc_date).ToString();
            //
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    {
                        if (this._icTransItemGrid != null)
                        {
                            if (this._icTransItemGrid._countByItem() > 0)
                            {
                                if (name.Equals(_g.d.ic_trans._doc_date))
                                {
                                    if (this._last_check_doc_date == "")
                                    {
                                        this._last_check_doc_date = __docDate;
                                    }
                                    if (this._last_check_doc_date.Equals(__docDate) == false)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("มีการแก้ไขวันที่ อาจจะทำให้ราคาสินค้าผิดพลาด"));
                                        this._last_check_doc_date = __docDate;
                                    }
                                }
                                this._custCheckChange();
                            }
                        }
                    }
                    break;
            }
            // กรณีไม่ป้อนวันที่ ให้ดึงวันที่ทำงานขึ้นมา
            if (name.Equals(_g.d.ic_trans._doc_date))
            {
                if (__docDate.Length == 0)
                {
                    this._setDataDate(_g.d.ic_trans._doc_date, MyLib._myGlobal._workingDate);
                    __docDate = this._getDataStr(_g.d.ic_trans._doc_date).ToString();
                }
            }
            //
            string __docNo = this._getDataStr(_g.d.ic_trans._doc_no).ToString();
            Boolean __copyDocNoAndDocDate = false;
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                    if (name.Equals(_g.d.ic_trans._doc_ref) || name.Equals(_g.d.ic_trans._doc_no_po))
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __query = "select " + _g.d.ic_trans._doc_date + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'";
                        DataTable __dt = __myFrameWork._queryShort(__query).Tables[0];
                        if (__dt.Rows.Count > 0)
                        {
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDateFromQuery(__dt.Rows[0][_g.d.ic_trans._doc_date].ToString()));
                            this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                        }
                        else
                        {
                            this._enabedControl(_g.d.ic_trans._doc_ref_date, true);
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                    if (name.Equals(_g.d.ic_trans._doc_advance_in_no) || name.Equals(_g.d.ic_trans._doc_ref_name_1) || name.Equals(_g.d.ic_trans._buy_deposit2) || name.Equals(_g.d.ic_trans._doc_ref_name_2))
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __query = "select " + _g.d.ic_trans._cust_code + "," + _g.d.ic_trans._total_amount + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'";
                        DataTable __dt = __myFrameWork._queryShort(__query).Tables[0];
                        if (__dt.Rows.Count > 0)
                        {
                            this._setDataStr(_g.d.ic_trans._cust_code, __dt.Rows[0][_g.d.ic_trans._cust_code].ToString());
                            this._setDataNumber(_g.d.ic_trans._total_amount, MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_trans._total_amount].ToString()));
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    __copyDocNoAndDocDate = true;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    _calc();
                    break;
            }
            if (__copyDocNoAndDocDate && this._managerData._mode == 1)
            {
                if (name.Equals(_g.d.ic_trans._doc_date))
                {
                    if (this._getDataStr(_g.d.ic_trans._tax_doc_date).ToString().Length == 0)
                    {
                        this._setDataDate(_g.d.ic_trans._tax_doc_date, MyLib._myGlobal._convertDate(__docDate));
                    }
                }
                else
                    if (name.Equals(_g.d.ic_trans._doc_no))
                {
                    if (this._getDataStr(_g.d.ic_trans._tax_doc_date).ToString().Length > 0)
                    {
                        if (this._getDataStr(_g.d.ic_trans._tax_doc_no).ToString().Length == 0)
                        {
                            this._setDataStr(_g.d.ic_trans._tax_doc_no, __docNo);
                        }
                    }
                }
            }
            // กรณี ไม่มีวันที่ใบกำกับ ให้ลบเลขที่ใบกำกับด้วย
            if (this._getDataStr(_g.d.ic_trans._tax_doc_date).ToString().Length == 0 && this._getDataStr(_g.d.ic_trans._tax_doc_no).ToString().Length != 0)
            {
                this._setDataStr(_g.d.ic_trans._tax_doc_no, "", "", true);
            }
        }

        public string   _check_perm_wh_shelf(string _wh_shelf, string _extraWhere ,string screen_type) {

            string __screen_type = "and screen_code='"+ screen_type + "'" ;
            string __and = "";
            if (_extraWhere.Length > 0)
            {
                __and = " and ";
            }
            if (_wh_shelf.Equals(_g.d.ic_trans._wh_from) || _wh_shelf.Equals(_g.d.ic_trans._wh_to)) {
                _extraWhere += __and +" exists ( " +
                " select wh_code, shelf_code from erp_user_group_wh_shelf where group_code in (select group_code from erp_user_group_detail where   " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "' )  " + __screen_type + " " +
                " and ic_warehouse.code = erp_user_group_wh_shelf.wh_code  ) ";
            }
             else if(_wh_shelf.Equals(_g.d.ic_trans._location_from) || _wh_shelf.Equals(_g.d.ic_trans._location_to)) {
                _extraWhere += __and + " exists ( " +
                " select wh_code, shelf_code from erp_user_group_wh_shelf where group_code in (select group_code from erp_user_group_detail where   " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "' )  " + __screen_type + " " +
                " and ic_shelf.whcode = erp_user_group_wh_shelf.wh_code  and ic_shelf.code = erp_user_group_wh_shelf.shelf_code  ) ";
            }
            return _extraWhere;
        }

        void _ictransScreenTopControl__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            //ย้ายมาเพื่อใช้กับทุกอัน
        
            string __screen_type = "";

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
                    if (__getControl._iconNumber == 4)
                    {
                        __searchObject.WindowState = FormWindowState.Maximized;
                    }
                    this._search_data_full_buffer.Add(__searchObject);
                    __searchObject._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                }

                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                if (__getControl.textBox.Text == "")
                {
                    this._search_data_full_pointer._dataList._searchText.TextBox.Text = "";
                }
                //
            }
            //   MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._label._field_name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            _searchScreenMasterList.Clear();
            try
            {
                /*if (this._searchName.Equals(_g.d.ic_trans._approve_code))
                {
                    if (_searchBills._fieldNameSearch == null)
                    {
                        string __myQuery = "select " + _g.d.erp_user._code + "," + _g.d.erp_user._name_1 + "," + _g.d.erp_user._department + ",(select name_1 from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.erp_user._table + "." + _g.d.erp_user._department + ") as " + _departmentName + " from " + _g.d.erp_user._table;
                        _searchBills._ictransControlTypeEnumTemp = _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ.ToString();
                        _searchBills._fieldNameSearch = _g.d.ic_trans._approve_code;
                        _searchBills._x_query = __myQuery;
                        _searchBills._okButton -= new _Search_bill.EventOkSearchHandler(_searchBills__okButton);
                        _searchBills._okButton += new _Search_bill.EventOkSearchHandler(_searchBills__okButton);
                    }
                    if (_searchBills._fieldNameSearch.Length > 0)
                    {
                        if (_searchBills.Visible == false)
                        {
                            _searchBills.Show();
                        }
                    }
                }
                else*/
                {
                    string __extraWhere = "";
                    _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "", this._screen_code);

                    if (_searchScreenMasterList.Count > 0 && _searchScreenMasterList[0].ToString().Equals(_g.g._screen_erp_doc_format) && ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite) && _g.g._companyProfile._deposit_format_from_pos == true &&
                    (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก)))
                    {
                        try
                        {
                            // อ่าน config เครื่อง POS
                            string __localPath = string.Format(@"c:\\smlsoft\\smlPOSScreenConfig-{0}-{1}-{2}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__").Replace("/", string.Empty), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName);
                            XmlDocument xDoc = new XmlDocument();
                            try
                            {
                                xDoc.Load(__localPath);
                            }
                            catch
                            {
                            }

                            xDoc.DocumentElement.Normalize();
                            XmlElement __xRoot = xDoc.DocumentElement;

                            string __posId = __xRoot.GetElementsByTagName("_posid").Item(0).InnerText;
                            this._setDataStr(this._searchName, __posId);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Cannot Load Pos Config", "ผิดพลาด");
                        }

                    }
                    else
                    {

                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            case _g.g._transControlTypeEnum.สินค้า_ขอโอน:

                                __screen_type = "IM";
                                if (this._searchName.Equals(_g.d.ic_trans._wh_from))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._search_master_ic_warehouse);
                                    _searchScreenMasterList.Add(_g.d.ic_warehouse._table);

                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._change_branch_code == false)
                                    {
                                        if (this._getBranchCode != null)
                                        {
                                            //แก้ ให้ดึงสาขาขากพนักงาน เท่านั้น 
                                            string __getBranchSelect  = MyLib._myGlobal._branchCode;

                                            if (__getBranchSelect.Length > 0)
                                            {
                                                // where branch
                                                __extraWhere = " coalesce(" + _g.d.ic_warehouse._branch_use + ", branch_code)  like \'%" + __getBranchSelect + "%\' ";
                                            }
                                        }
                                    }
                                    if (_g.g._companyProfile._perm_wh_shelf)
                                    {
                                        __extraWhere = _check_perm_wh_shelf(this._searchName, __extraWhere, __screen_type);
                                    }
                                }
                                else
                                    if (this._searchName.Equals(_g.d.ic_trans._location_from))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._search_master_ic_shelf);
                                    _searchScreenMasterList.Add(_g.d.ic_shelf._table);
                                    //_searchScreenMasterList.Add(_g.d.ic_shelf._whcode + "=\'" + this._getDataStr(_g.d.ic_trans._wh_from) + "\'");
                                    __extraWhere = _g.d.ic_shelf._whcode + "=\'" + this._getDataStr(_g.d.ic_trans._wh_from) + "\'";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._change_branch_code == false)
                                    {
                                        if (this._getBranchCode != null)
                                        {
                                            string __getBranchSelect = MyLib._myGlobal._branchCode;

                                            if (__getBranchSelect.Length > 0)
                                            {
                                                // where branch
                                                //this._icTransItemGridSelectWareHouse._extraWhere = " wh_code in (select code from ic_warehouse where branch_code like \'%" + __getBranchSelect + "%\') ";
                                                __extraWhere += " and whcode in (select code from ic_warehouse where coalesce(" + _g.d.ic_warehouse._branch_use + ", branch_code) like \'%" + __getBranchSelect + "%\')  ";
                                                //__extraWhere += " and whcode in (select code from ic_warehouse where coalesce( branch_code) like \'%" + __getBranchSelect + "%\')  ";
                                            }
                                        }
                                    }
                                    if (_g.g._companyProfile._perm_wh_shelf)
                                    {
                                        __extraWhere = _check_perm_wh_shelf(this._searchName, __extraWhere, __screen_type);
                                    }

                                }
                                else
                                        if (this._searchName.Equals(_g.d.ic_trans._wh_to))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._search_master_ic_warehouse);
                                    _searchScreenMasterList.Add(_g.d.ic_warehouse._table);

                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._change_branch_code == false)
                                    {
                                        if (this._getBranchCode != null)
                                        {
                                            string __getBranchSelect = MyLib._myGlobal._branchCode;

                                            if (__getBranchSelect.Length > 0)
                                            {
                                                // where branch
                                                __extraWhere = " coalesce(" + _g.d.ic_warehouse._branch_use + ", branch_code)  like \'%" + __getBranchSelect + "%\' ";
                                                //__extraWhere = " coalesce(branch_code)  like \'%" + __getBranchSelect + "%\' ";
                                            }
                                        }
                                    }
                                    if (_g.g._companyProfile._perm_wh_shelf)
                                    {
                                        __extraWhere = _check_perm_wh_shelf(this._searchName, __extraWhere, __screen_type);
                                    }
                                }
                                else
                                            if (this._searchName.Equals(_g.d.ic_trans._location_to))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._search_master_ic_shelf);
                                    _searchScreenMasterList.Add(_g.d.ic_shelf._table);
                                    //_searchScreenMasterList.Add(_g.d.ic_shelf._whcode + "=\'" + this._getDataStr(_g.d.ic_trans._wh_to) + "\'");
                                    __extraWhere = _g.d.ic_shelf._whcode + "=\'" + this._getDataStr(_g.d.ic_trans._wh_to) + "\'";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._change_branch_code == false)
                                    {
                                        if (this._getBranchCode != null)
                                        {
                                            string __getBranchSelect = MyLib._myGlobal._branchCode;

                                            if (__getBranchSelect.Length > 0)
                                            {
                                                // where branch
                                                //this._icTransItemGridSelectWareHouse._extraWhere = " wh_code in (select code from ic_warehouse where branch_code like \'%" + __getBranchSelect + "%\') ";
                                                __extraWhere += " and  whcode in (select code from ic_warehouse where coalesce(" + _g.d.ic_warehouse._branch_use + ", branch_code) like \'%" + __getBranchSelect + "%\')  ";
                                                //__extraWhere += " and whcode in (select code from ic_warehouse where coalesce( branch_code) like \'%" + __getBranchSelect + "%\')  ";
                                            }
                                        }
                                    }
                                    if (_g.g._companyProfile._perm_wh_shelf)
                                    {
                                        __extraWhere = _check_perm_wh_shelf(this._searchName, __extraWhere, __screen_type);
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                            case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                            case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:

                                if (this._searchName.Equals(_g.d.ic_trans._wh_from))
                                {
                                   
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._search_master_ic_warehouse);
                                    _searchScreenMasterList.Add(_g.d.ic_warehouse._table);
                                    //if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._change_branch_code == false)
                                    //{
                                    //    if (this._getBranchCode != null)
                                    //    {
                                    //        //แก้ ให้ดึงสาขาขากพนักงาน เท่านั้น 
                                    //        // string __getBranchSelect = this._getBranchCode();
                                    //        if (__getBranchSelect.Length > 0)
                                    //        {
                                    //            // where branch
                                    //            __extraWhere = " coalesce(" + _g.d.ic_warehouse._branch_use + ", branch_code)  like \'%" + __getBranchSelect + "%\' ";
                                    //        }
                                    //    }
                                    //}
                                    //if (_g.g._companyProfile._perm_wh_shelf)
                                    //{
                                    //    __extraWhere = _check_perm_wh_shelf(this._searchName, __extraWhere);
                                    //}
                                }
                                else
                                    if (this._searchName.Equals(_g.d.ic_trans._location_from))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._search_master_ic_shelf);
                                    _searchScreenMasterList.Add(_g.d.ic_shelf._table);
                                    _searchScreenMasterList.Add(_g.d.ic_shelf._whcode + "=\'" + this._getDataStr(_g.d.ic_trans._wh_from) + "\'");
                                    //if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._change_branch_code == false)
                                    //{
                                    //    if (this._getBranchCode != null)
                                    //    {
                                    //        //แก้ ให้ดึงสาขาขากพนักงาน เท่านั้น 
                                    //        // string __getBranchSelect = this._getBranchCode();
                                    //        if (__getBranchSelect.Length > 0)
                                    //        {
                                    //            // where branch
                                    //            __extraWhere = " coalesce(" + _g.d.ic_warehouse._branch_use + ", branch_code)  like \'%" + __getBranchSelect + "%\' ";
                                    //        }
                                    //    }
                                    //}
                                    //if (_g.g._companyProfile._perm_wh_shelf)
                                    //{
                                    //    __extraWhere = _check_perm_wh_shelf(this._searchName, __extraWhere);
                                    //}
                                }
                                break;
                            case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_no_po))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ));
                                }
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_โอนออก));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._doc_success + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                                // toe
                                if (this._searchName.Equals(_g.d.ic_trans._contactor))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._search_screen_ar_contactor);
                                    _searchScreenMasterList.Add(_g.d.ar_contactor._table);
                                    _searchScreenMasterList.Add(_g.d.ar_contactor._ar_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\'");
                                }
                                else
                                {
                                    _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "", this._screen_code, _g.d.erp_user_group._is_price_list_approve + "=1");
                                }

                                if (this._searchName.Equals(_g.d.ic_trans._sale_code) && MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                                {
                                    __extraWhere += "coalesce(" + _g.d.erp_user._is_login_user + ", 0)=0 ";
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "", this._screen_code, _g.d.erp_user_group._is_pr_approve + "=1");
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                            case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_po_request))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    // เอกสารเสนอซื้อที่ยังไม่ยกเลิก และยังไม่มีการอ้างอิง
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_quotation_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    // เอกสารสั่งซื้อที่ยังไม่ยกเลิก และยังไม่มีการอ้างอิง
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เสนอราคา));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา) + __docBranch);
                                }
                                else
                                    if (this._searchName.Equals(_g.d.ic_trans._contactor))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._search_screen_ar_contactor);
                                    _searchScreenMasterList.Add(_g.d.ar_contactor._table);
                                    _searchScreenMasterList.Add(_g.d.ar_contactor._ar_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\'");
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_no_po))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    // เอกสารสั่งซื้อที่ยังไม่ยกเลิก และยังไม่มีการอ้างอิง
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and (" + _g.d.ic_trans._used_status + "=0 " + ((this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก) ? " or " + _g.d.ic_trans._doc_success + "=0 " : "") + ") and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_saleorder_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_สั่งขาย));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย) + " and " + _g.d.ic_trans._last_status + "=0 and ( " + _g.d.ic_trans._used_status + "=0 " + ((this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก) ? " or " + _g.d.ic_trans._doc_success + "=0 " : "") + " )" + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._contactor))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._search_screen_ap_contactor);
                                        _searchScreenMasterList.Add(_g.d.ap_contactor._table);
                                        _searchScreenMasterList.Add(_g.d.ap_contactor._ap_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\'");
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_no_po))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    string __extraWhereApprove = "";

                                    if (this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ)
                                        __extraWhereApprove = " and " + _g.d.ic_trans._approve_status + "=0 ";
                                    // เอกสารสั่งซื้อที่ยังไม่ยกเลิก และยังไม่มีการอ้างอิง
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._doc_success + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ) + __docBranch + __extraWhereApprove);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._buy_deposit2))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    //_searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\'  and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ));

                                    // toe เพิ่ม custom where
                                    StringBuilder __customWhere = new StringBuilder();
                                    __customWhere.Append(_g.d.ic_trans._last_status + "=0 and coalesce(" + _g.d.ic_trans._used_status + ", 0)=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ));

                                    string __getCustCode = this._getDataStr(_g.d.ic_trans._cust_code);
                                    if (__getCustCode.Length > 0)
                                    {
                                        __customWhere.Append(" and " + _g.d.ic_trans._cust_code + "=\'" + __getCustCode + "\' ");
                                    }
                                    _searchScreenMasterList.Add(__customWhere.ToString() + __docBranch);

                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref_name_2))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    //_searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน));

                                    // toe เพิ่ม custom where
                                    StringBuilder __customWhere = new StringBuilder();
                                    __customWhere.Append(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน));

                                    string __getCustCode = this._getDataStr(_g.d.ic_trans._cust_code);
                                    if (__getCustCode.Length > 0)
                                    {
                                        __customWhere.Append(" and " + _g.d.ic_trans._cust_code + "=\'" + __getCustCode + "\' ");
                                    }
                                    _searchScreenMasterList.Add(__customWhere.ToString() + __docBranch);

                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_advance_in_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    //_searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า));

                                    // toe เพิ่ม custom where
                                    StringBuilder __customWhere = new StringBuilder();
                                    __customWhere.Append(_g.d.ic_trans._last_status + "=0 and coalesce(" + _g.d.ic_trans._used_status + ", 0)=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า));

                                    string __getCustCode = this._getDataStr(_g.d.ic_trans._cust_code);
                                    if (__getCustCode.Length > 0)
                                    {
                                        __customWhere.Append(" and " + _g.d.ic_trans._cust_code + "=\'" + __getCustCode + "\'");
                                    }
                                    _searchScreenMasterList.Add(__customWhere.ToString() + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_advance_so_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    // toe เพิ่ม where ตามลูกหนี้
                                    StringBuilder __where = new StringBuilder(_g.d.ic_trans._last_status + "=0 and coalesce(" + _g.d.ic_trans._used_status + ", 0)=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า));
                                    string __getCustCode = this._getDataStr(_g.d.ic_trans._cust_code);

                                    //if (__getCustCode.Length > 0) {
                                    __where.Append(" and " + _g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' ");
                                    //}

                                    _searchScreenMasterList.Add(__where.ToString() + __docBranch);

                                    /*__searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._PoSoDepositGlobal._PoSoDepositFlag(_g.g._PoSoDepositControlFlagEnum.so_advance));*/
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_advance_so_return_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    // toe เพิ่ม where ตามลูกหนี้
                                    string __getCustCode = this._getDataStr(_g.d.ic_trans._cust_code);
                                    StringBuilder __where = new StringBuilder(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน));

                                    //if (__getCustCode.Length > 0) {
                                    __where.Append(" and " + _g.d.ic_trans._cust_code + "=\'" + __getCustCode + "\' ");
                                    //}
                                    _searchScreenMasterList.Add(__where.ToString() + __docBranch);
                                    /*__searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._PoSoDepositGlobal._PoSoDepositFlag(_g.g._PoSoDepositControlFlagEnum.so_advance_return));*/
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._deposit_doc))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    // toe เพิ่ม where ตามลูกหนี้
                                    string __getCustCode = this._getDataStr(_g.d.ic_trans._cust_code);
                                    StringBuilder __where = new StringBuilder(_g.d.ic_trans._last_status + "=0 and coalesce(" + _g.d.ic_trans._used_status + ", 0)=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ));

                                    //if (__getCustCode.Length > 0)
                                    //{
                                    __where.Append(" and " + _g.d.ic_trans._cust_code + "=\'" + __getCustCode + "\' ");
                                    //}

                                    _searchScreenMasterList.Add(__where.ToString() + __docBranch);
                                    // toe เอา comment ออกให้ where ลูกหนี้
                                    /*__searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._PoSoDepositGlobal._PoSoDepositFlag(_g.g._PoSoDepositControlFlagEnum.so_advance));*/
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._deposit_return_doc))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    // toe เพิ่ม where ตามลูกหนี้
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน) + __docBranch);
                                    /*__searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._PoSoDepositGlobal._PoSoDepositFlag(_g.g._PoSoDepositControlFlagEnum.so_advance_return));*/
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_invoice_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ar_credit_note_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ar_debit_note_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref_name_1))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    //_searchScreenMasterList.Add(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน));

                                    // toe เพิ่ม custom where
                                    StringBuilder __customWhere = new StringBuilder();
                                    __customWhere.Append(_g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._used_status + "=0 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน) + __docBranch);

                                    string __getCustCode = this._getDataStr(_g.d.ic_trans._cust_code);
                                    if (__getCustCode.Length > 0)
                                    {
                                        __customWhere.Append(" and " + _g.d.ic_trans._cust_code + "=\'" + __getCustCode + "\'");
                                    }
                                    _searchScreenMasterList.Add(__customWhere.ToString());

                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_purchase_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_purchase_debit_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_purchase_credit_no))
                                {
                                    string __docBranch = "";
                                    if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                        __docBranch = " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";

                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด) + __docBranch);
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                            case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                                // toe
                                if (this._searchName.Equals(_g.d.ic_trans._contactor))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._search_screen_ar_contactor);
                                    _searchScreenMasterList.Add(_g.d.ar_contactor._table);
                                    _searchScreenMasterList.Add(_g.d.ar_contactor._ar_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\'");
                                }

                                if (this._searchName.Equals(_g.d.ic_trans._doc_advance_so_no))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\' " +
                                        " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + " and not exists (select doc_no from ic_trans as sr where sr.trans_flag = 34 and sr.last_status = 0 and ic_trans.doc_no = sr.doc_ref_trans) ");
                                }


                                if (this._searchName.Equals(_g.d.ic_trans._sale_code) && MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                                {
                                    __extraWhere += "coalesce(" + _g.d.erp_user._is_login_user + ", 0)=0 ";
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);

                                        StringBuilder __docWhere = new StringBuilder();

                                        if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก));
                                        }
                                        else if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน));
                                        }
                                        else if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน));
                                        }
                                        else if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก));
                                        }
                                        else if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่));
                                        }
                                        else if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค));
                                        }
                                        __docWhere.Append(" and coalesce(" + _g.d.ic_trans._used_status + ", 0)=0 and  coalesce(" + _g.d.ic_trans._last_status + ", 0)=0");
                                        _searchScreenMasterList.Add(__docWhere.ToString());
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);

                                        StringBuilder __docWhere = new StringBuilder();

                                        if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน));
                                        }
                                        else if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน));
                                        }
                                        else if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก));
                                        }
                                        else if (_icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก)
                                        {
                                            __docWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค));
                                        }
                                        __docWhere.Append(" and coalesce(" + _g.d.ic_trans._used_status + ", 0)=0 and  coalesce(" + _g.d.ic_trans._last_status + ", 0)=0");
                                        _searchScreenMasterList.Add(__docWhere.ToString());

                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                                if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                {
                                    _searchScreenMasterList.Clear();
                                    _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
                                    _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                    _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน));
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร));
                                    }

                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย));
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย));
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น));
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้));
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้));
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น));
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้));
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้));
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:

                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._pass_book_code))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._search_screen_สมุดเงินฝาก);
                                        _searchScreenMasterList.Add(_g.d.erp_pass_book._table);
                                        //_searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้));
                                    }
                                }
                                break;

                            case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
                                    {
                                        _searchScreenMasterList.Clear();
                                        _searchScreenMasterList.Add(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ));
                                        _searchScreenMasterList.Add(_g.d.ic_trans._table);
                                        _searchScreenMasterList.Add(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " and " + _g.d.ic_trans._cust_code + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code) + "\'");
                                    }
                                }
                                break;

                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._sale_code) && MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                                    {
                                        __extraWhere += "coalesce(" + _g.d.erp_user._is_login_user + ", 0)=0 ";
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                {
                                    if (this._searchName.Equals(_g.d.ic_trans._sale_code) && MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                                    {
                                        __extraWhere += "coalesce(" + _g.d.erp_user._is_login_user + ", 0)=0 ";
                                    }
                                }
                                break;
                        }

                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._searchName.Equals(_g.d.ic_trans._ar_code) && (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้ ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า))
                        {
                            __extraWhere = _g.d.ar_customer._status + "=0 ";

                            if (_g.g._companyProfile._customer_by_branch && _g.g._companyProfile._change_branch_code == false)
                            {
                                __extraWhere += " and " + _g.d.ar_customer._ar_branch_code + "=\'" + _g.g._companyProfile._branch_code + "\' ";
                            }
                        }
                        if (_searchScreenMasterList.Count > 0)
                        {
                            if (this._search_data_full_pointer._name.Equals(_searchScreenMasterList[0].ToString().ToLower()) == false)
                            {
                                if (this._search_data_full_pointer._name.Length == 0)
                                {
                                    this._search_data_full_pointer._name = _searchScreenMasterList[0].ToString();
                                    this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                                    // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                                    this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                                    //
                                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                                }
                            }
                            __extraWhere = (_searchScreenMasterList.Count == 3) ? _searchScreenMasterList[2].ToString() : __extraWhere;


                            MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __extraWhere);
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
                    }

                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + " : " + __ex.StackTrace.ToString());
            }
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                int __usedStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status);
                int __docSuccessColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_success);
                int __lastStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);
                if (__usedStatusColumn != -1)
                {
                    // มีการนำไปใช้แล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.LightSeaGreen;
                    }
                }
                if (__docSuccessColumn != -1)
                {
                    // มีการอ้างอิงครบแล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __docSuccessColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.SlateBlue;
                    }
                }
                if (__lastStatusColumn != -1)
                {
                    // เอกสารมีการยกเลิก
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __lastStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.Red;
                    }
                }
            }
            catch
            {
            }
            return (__result);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
            SendKeys.Send("{ENTER}");
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
            SendKeys.Send("{TAB}");
        }

        void _searchBills__okButton(object sender, string _screentype, string _tabletaget)
        {
            try
            {
                MyLib._myGrid __myGridTemp = (MyLib._myGrid)(sender);
                string __result = "";
                string __appCode = "";
                string __appName = "";
                /*if (_icTransControlType == _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ || _icTransControlType == _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ)
                {
                    if (__myGridTemp._rowData.Count > 0)
                    {
                        for (int __loop = 0; __loop < __myGridTemp._rowData.Count; __loop++)
                        {
                            if ((int)__myGridTemp._cellGet(__loop, 0) == 1)
                            {
                                __appCode = __myGridTemp._cellGet(__loop, _g.d.erp_user._code).ToString();
                                __appName = __myGridTemp._cellGet(__loop, _g.d.erp_user._name_1).ToString();
                                if (__result.Length > 0)
                                {
                                    __result = __result + "," + __appCode;
                                }
                                else
                                {
                                    __result = __appCode;
                                }
                            }
                        }
                        if (__result.Length > 0)
                        {
                            this._setDataStr(_g.d.ic_trans._approve_code, __result);
                        }
                    }
                }*/
                this._searchBills.Visible = false;
            }
            catch (Exception ex)
            {
            }
        }

        void _searchAll(string screenName, int row)
        {
            int __columnNumber = 0;
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_โอนออก)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;

                case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เสนอราคา)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_สั่งขาย)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                    {
                        if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)))
                        {
                            __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                        }
                    }
                    break;
            }
            string __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._search_data_full_pointer.Visible = false;
                this._setDataStr(this._searchName, __result, "", false);
                this._search(true);
            }
        }

        private void _checkCreditDay(string __custCode, DataSet getData)
        {
            if (this._changeCreditDay != null)
            {
                if (this._last_cust_code.Equals(__custCode) == false)
                {
                    DataTable __dt = getData.Tables[0];
                    decimal __getCreditDay = MyLib._myGlobal._decimalPhase(__dt.Rows[0][this._creditDayFieldName].ToString());
                    this._changeCreditDay((int)__getCreditDay);
                }
            }
        }

        public void _custCheckChange()
        {
            if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา)
            {
                if (this._icTransItemGrid != null)
                {
                    if (this._icTransItemGrid._countByItem() > 0)
                    {
                        string __custCode = this._getDataStr(_g.d.ic_trans._cust_code);
                        if (this._last_check_cust_code == "")
                        {
                            this._last_check_cust_code = __custCode;
                        }
                        if (this._last_check_cust_code.Equals(__custCode) == false)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("มีการแก้ไขรหัสลูกค้า อาจจะทำให้ราคาสินค้าผิดพลาด"));
                            this._last_check_cust_code = __custCode;
                        }
                    }
                }
            }
        }

        public void _search(Boolean warning)
        {
            if (MyLib._myGlobal._isDesignMode)
            {
                return;
            }
            string __docWarning1 = MyLib._myGlobal._resource("เอกสารนี้มีการยกเลิกไปแล้ว ไม่สามารถนำมายกเลิกได้");
            string __docWarning2 = MyLib._myGlobal._resource("เอกสารนี้มีปิดรายการไปแล้ว ไม่สามารถนำมายกเลิกได้");
            string __docWarning3 = MyLib._myGlobal._resource("เอกสารนี้มีการอ้างอิงรายการไปแล้ว ไม่สามารถนำมายกเลิกได้");
            string __custCode = this._getDataStr(_g.d.ic_trans._cust_code);
            try
            {
                int __tableCount = 0;
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_group._code) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_group).ToUpper() + "\'"));
                __tableCount++;
                //
                string __querySearchArName = "select " + _g.d.ar_customer._name_1 + "," +
                    "coalesce((select " + _g.d.ar_customer_detail._credit_day + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "),0)  as " + this._creditDayFieldName + "," +
                    "coalesce((select " + _g.d.ar_customer_detail._credit_status + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "),0)  as " + _g.d.ar_customer_detail._credit_status +
                    "," + _g.d.ar_customer._status +
                    " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + __custCode.ToUpper() + "\'";
                //
                string __querySearchApName = "select " + _g.d.ap_supplier._name_1 + "," +
                    "coalesce((select " + _g.d.ap_supplier_detail._credit_day + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "),0)  as " + this._creditDayFieldName +
                    " from " + _g.d.ap_supplier._table + " where " + MyLib._myGlobal._addUpper(_g.d.ap_supplier._code) + "=\'" + __custCode.ToUpper() + "\'";
                //
                string __querySearchUserRequest = "select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user._code) + "=\'" + this._getDataStr(_g.d.ic_trans._user_request).ToUpper() + "\'";
                //
                string __querySearchUserApprove = "select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user._code) + "=\'" + this._getDataStr(_g.d.ic_trans._user_approve).ToUpper() + "\'";
                //
                string __querySearchGroupApprove = "select " + _g.d.erp_user_group._name_1 + " from " + _g.d.erp_user_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group._code) + "=\'" + this._getDataStr(_g.d.ic_trans._approve_code).ToUpper() + "\'";
                //
                string __querySearchDocRef = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'";
                //

                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserRequest));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserApprove));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserRequest));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserApprove));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserRequest));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserApprove));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserRequest));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserApprove));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserRequest));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchGroupApprove));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserApprove));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserRequest));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchGroupApprove));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserApprove));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserRequest));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchGroupApprove));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserRequest));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchGroupApprove));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchUserApprove));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + "," + _g.d.ic_trans._used_status + "," + _g.d.ic_trans._used_status_2 + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                        __tableCount++;
                        break;
                    /* toe move ไปรวมกับ ยกเลิกซื้อสินค้า
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + "," + _g.d.ic_trans._used_status + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                    __tableCount++;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + "," + _g.d.ic_trans._used_status + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                    __tableCount++;
                    break;
                    */
                    /* toe รวม เข้าไปเป็นอันเดียว
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                    __tableCount++;
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                    __tableCount++;
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                    __tableCount++;
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + "," + _g.d.ic_trans._used_status + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                    __tableCount++;
                        
                    break;*/
                    case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + "," + _g.d.ic_trans._used_status + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + "," + _g.d.ic_trans._used_status + "," + _g.d.ic_trans._used_status_2 + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                        __tableCount++;
                        break;
                    /* toe move ไปรับ กับ ยกเลิก ขายสินค้าและบริการ
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                    __tableCount++;
                    break;
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                    __tableCount++;
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                    __tableCount++;
                    break;*/
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + "," + _g.d.ic_trans._used_status + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:

                        if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป && this._menuName.Equals("menu_ic_finish_receive_ar"))
                        {
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        }
                        else
                        {
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        }
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._used_status + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._used_status + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:

                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._used_status + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                        __tableCount++;
                        break;

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._name_1 + " from " + _g.d.erp_pass_book._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_pass_book._code) + "=\'" + this._getDataStr(_g.d.ic_trans._pass_book_code).ToUpper() + "\'"));
                        __tableCount++;

                        break;

                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        __tableCount++;
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_date + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));
                        __tableCount++;
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                        if (this._menuName.Equals("menu_ic_issue_ap"))
                        {
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchApName));

                        }
                        else
                        {
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
                        }
                        __tableCount++;
                        break;
                }
                // พนักงานขาย
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user._code) + "=\'" + this._getDataStr(_g.d.ic_trans._sale_code).ToUpper() + "\'"));
                int __tableSaleCode = __tableCount;
                __tableCount++;
                // กลุ่มพนักงานขาย
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user_group._name_1 + " from " + _g.d.erp_user_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group._code) + "=\'" + this._getDataStr(_g.d.ic_trans._sale_group).ToUpper() + "\'"));
                __tableCount++;
                // คลังสินค้า
                int __tableCountWareHouse = __tableCount;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_warehouse._code) + "=\'" + this._getDataStr(_g.d.ic_trans._wh_from).ToUpper() + "\'"));
                __tableCount++;
                // ที่เก็บสินค้า
                int __tableCountLocation = __tableCount;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_shelf._whcode) + "=\'" + this._getDataStr(_g.d.ic_trans._wh_from).ToUpper() + "\' and " + MyLib._myGlobal._addUpper(_g.d.ic_shelf._code) + "=\'" + this._getDataStr(_g.d.ic_trans._location_from).ToUpper() + "\'"));
                __tableCount++;
                // คลังสินค้าปลายทาง
                int __tableCountWareHouseTo = __tableCount;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_warehouse._code) + "=\'" + this._getDataStr(_g.d.ic_trans._wh_to).ToUpper() + "\'"));
                __tableCount++;
                // ที่เก็บสินค้าปลายทาง
                int __tableCountLocationTo = __tableCount;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_shelf._whcode) + "=\'" + this._getDataStr(_g.d.ic_trans._wh_to).ToUpper() + "\' and " + MyLib._myGlobal._addUpper(_g.d.ic_shelf._code) + "=\'" + this._getDataStr(_g.d.ic_trans._location_to).ToUpper() + "\'"));
                __tableCount++;
                // ลูกหนี้
                int __tableCountAr = __tableCount;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + this._getDataStr(_g.d.ic_trans._cust_code).ToUpper() + "\'"));
                __tableCount++;


                // string __querySearcAr = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + this._getDataStr(_g.d.ic_trans._ar_code).ToUpper() + "\'";
                //
                //
                __myquery.Append("</node>");
                //
                string __queryStr = __myquery.ToString();
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
                _searchAndWarning(_g.d.ic_trans._doc_group, (DataSet)_getData[0], warning);
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                        _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[1], warning);

                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                    case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                    case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด:
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                        {
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[__tableCountAr], warning);
                        }
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                        {
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], false);
                        }
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);

                        break;
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
                        _searchAndWarning(_g.d.ic_trans._wh_to, (DataSet)_getData[__tableCountWareHouseTo], warning);
                        _searchAndWarning(_g.d.ic_trans._location_to, (DataSet)_getData[__tableCountLocationTo], warning);

                        _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[__tableSaleCode], warning);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._user_request, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._user_approve, (DataSet)_getData[3], warning);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._user_request, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._user_approve, (DataSet)_getData[3], warning);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._user_request, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._user_approve, (DataSet)_getData[3], warning);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._user_request, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._user_approve, (DataSet)_getData[3], warning);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._user_request, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._approve_code, (DataSet)_getData[3], warning);
                        _searchAndWarning(_g.d.ic_trans._user_approve, (DataSet)_getData[4], warning);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                        {
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                            DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            if (this._managerData._mode == 1)
                            {
                                int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                                int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                                if (__lastStatus == 1 || __docSuccess == 1)
                                {
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                    }
                                    else
                                        if (__docSuccess == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                    }
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                            }
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_group, (DataSet)_getData[3], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_group, (DataSet)_getData[3], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_group, (DataSet)_getData[3], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_group, (DataSet)_getData[3], warning);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_group, (DataSet)_getData[3], warning);
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._user_request, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._approve_code, (DataSet)_getData[3], warning);
                        _searchAndWarning(_g.d.ic_trans._user_approve, (DataSet)_getData[4], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[5], warning);
                        _searchAndWarning(_g.d.ic_trans._sale_group, (DataSet)_getData[6], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._user_request, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._approve_code, (DataSet)_getData[3], warning);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._user_request, (DataSet)_getData[2], warning);
                        _searchAndWarning(_g.d.ic_trans._approve_code, (DataSet)_getData[3], warning);
                        _searchAndWarning(_g.d.ic_trans._user_approve, (DataSet)_getData[4], warning);
                        //
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                        {
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                            DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            if (this._managerData._mode == 1)
                            {
                                int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                                int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                                int __usedSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._used_status].ToString());
                                int __used_status_2 = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._used_status_2].ToString());
                                if (__lastStatus == 1 || __docSuccess == 1 || __usedSuccess == 1 || __used_status_2 == 1)
                                {
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                    }
                                    else
                                        if (__docSuccess == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                    }
                                    else
                                            if (__usedSuccess == 1 | __used_status_2 == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning3));
                                    }
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                            }
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                            this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        }
                        break;
                    /* toe move ไปรวมกับ ยกเลิกซื้อสินค้า
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก:
                    {
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                        string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                        if (this._managerData._mode == 1)
                        {
                            int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                            int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                            int __usedSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._used_status].ToString());
                            if (__lastStatus == 1 || __docSuccess == 1 || __usedSuccess == 1)
                            {
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                if (__lastStatus == 1)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                }
                                else
                                    if (__docSuccess == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                    }
                                    else
                                        if (__usedSuccess == 1)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource(__docWarning3));
                                        }
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                            }
                        }
                        this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                    {
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                        string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                        if (this._managerData._mode == 1)
                        {
                            int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                            int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                            int __usedSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._used_status].ToString());
                            if (__lastStatus == 1 || __docSuccess == 1 || __usedSuccess == 1)
                            {
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                if (__lastStatus == 1)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                }
                                else
                                    if (__docSuccess == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                    }
                                    else
                                        if (__usedSuccess == 1)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource(__docWarning3));
                                        }
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                            }
                        }
                        this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                    }
                    break;*/
                    /*case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:{
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                        string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                        if (this._managerData._mode == 1)
                        {
                            int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                            int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                            if (__lastStatus == 1 || __docSuccess == 1)
                            {
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                if (__lastStatus == 1)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                }
                                else
                                    if (__docSuccess == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                    }
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                            }
                        }
                        this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                    }
                    break;
                    case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                        {
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                            DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            if (this._managerData._mode == 1)
                            {
                                int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                                int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                                if (__lastStatus == 1 || __docSuccess == 1)
                                {
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                    }
                                    else
                                        if (__docSuccess == 1)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                        }
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                            }
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                        {
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                            DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            if (this._managerData._mode == 1)
                            {
                                int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                                int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                                int __used_status = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._used_status].ToString());

                                if (__lastStatus == 1 || __docSuccess == 1 || __used_status == 1)
                                {
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                    }
                                    else
                                        if (__docSuccess == 1)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                        }
                                        else
                                            if (__used_status == 1)
                                            {
                                                MessageBox.Show(__docWarning3);
                                            }
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                            }
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                        }
                        break;*/
                    case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                        {
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                            DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            if (this._managerData._mode == 1)
                            {
                                int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                                int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                                int __used_status = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._used_status].ToString());
                                if (__lastStatus == 1 || __docSuccess == 1 || __used_status == 1)
                                {
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                    }
                                    else
                                        if (__docSuccess == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                    }
                                    else
                                            if (__used_status == 1)
                                    {
                                        MessageBox.Show(__docWarning3);
                                    }
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                            }
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                        {
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                            DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            if (this._managerData._mode == 1)
                            {
                                int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                                int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                                int __used_status = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._used_status].ToString());
                                int __used_status_2 = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._used_status_2].ToString());

                                if (__lastStatus == 1 || __docSuccess == 1 || __used_status == 1 || __used_status_2 == 1)
                                {
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                    }
                                    else
                                        if (__docSuccess == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                    }
                                    else
                                            if (__used_status == 1 || __used_status_2 == 1)
                                    {
                                        MessageBox.Show(__docWarning3);
                                    }
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                            }
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                        }
                        break;
                    /* toe move ไปรวมกับ ยกเลิกขาย
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                    {
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                        string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                        if (this._managerData._mode == 1)
                        {
                            int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                            int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                            if (__lastStatus == 1 || __docSuccess == 1)
                            {
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                if (__lastStatus == 1)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                }
                                else
                                    if (__docSuccess == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                    }
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                            }
                        }
                        this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    {
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                        string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                        if (this._managerData._mode == 1)
                        {
                            int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                            int __docSuccess = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._doc_success].ToString());
                            if (__lastStatus == 1 || __docSuccess == 1)
                            {
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                if (__lastStatus == 1)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                }
                                else
                                    if (__docSuccess == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning2));
                                    }
                                this._setDataStr(_g.d.ic_trans._doc_ref, "");
                            }
                        }
                        this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDocDate));
                    }
                    break;*/
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
                        _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                        {
                            DataTable __tableDocDate = ((DataSet)_getData[1]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            if (this._managerData._mode == 1)
                            {
                                int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                                if (__lastStatus == 1)
                                {
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                    }

                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                            }
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDateFromQuery(__getDocDate));
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[2], warning);
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                        {
                            DataTable __tableDocDate = ((DataSet)_getData[1]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            if (this._managerData._mode == 1)
                            {
                                int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                                if (__lastStatus == 1)
                                {
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                    }

                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                            }
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDateFromQuery(__getDocDate));
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[2], warning);
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:

                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        this._checkCreditDay(__custCode, (DataSet)_getData[1]);
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                        _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก:

                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก:
                        {
                            DataTable __tableDocDate = ((DataSet)_getData[1]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            if (this._managerData._mode == 1)
                            {
                                int __lastStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._last_status].ToString());
                                int __usedStatus = (__tableDocDate.Rows.Count == 0) ? 0 : MyLib._myGlobal._intPhase(__tableDocDate.Rows[0][_g.d.ic_trans._used_status].ToString());

                                if (__lastStatus == 1 || __usedStatus == 1)
                                {
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource(__docWarning1));
                                    }
                                    else
                                        if (__usedStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("มีการอ้างอิงเอกสารไปแล้ว ไม่สามารถดำเนินการยกเลิกได้"));
                                    }
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                            }
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDateFromQuery(__getDocDate));
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:

                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                        {
                            _searchAndWarning(_g.d.ic_trans._pass_book_code, (DataSet)_getData[1], warning);

                        }
                        break;
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                        {
                            _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);

                            DataTable __tableDocDate = ((DataSet)_getData[2]).Tables[0];
                            string __getDocDate = (__tableDocDate.Rows.Count == 0) ? "" : __tableDocDate.Rows[0][_g.d.ic_trans._doc_date].ToString();
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, MyLib._myGlobal._convertDateFromQuery(__getDocDate));

                            _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[3], warning);

                        }
                        break;

                }
                this._last_cust_code = __custCode;
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet __dataResult, Boolean warning)
        {
            bool __result = false;
            string __getData = "";
            string __getDataStr = this._getDataStr(fieldName);
            string __getDataStr1 = this._getDataStr(fieldName);
            this._setDataStr(fieldName, __getDataStr, __getData, true);
            if (__dataResult.Tables[0].Rows.Count > 0)
            {
                __getData = __dataResult.Tables[0].Rows[0][0].ToString();
            }
            this._setDataStr(fieldName, __getDataStr, __getData, true);
            if (this._searchTextBox != null)
            {

                // toe เพิ่ม 20130311
                // 20160810 toe เอาออก && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ 
                if (this._searchName.CompareTo(fieldName) == 0 && (fieldName.Equals(_g.d.ic_trans._cust_code) ||
                    fieldName.Equals(_g.d.ic_trans._wh_from) ||
                    fieldName.Equals(_g.d.ic_trans._location_from) ||
                    fieldName.Equals(_g.d.ic_trans._wh_to) ||
                    fieldName.Equals(_g.d.ic_trans._location_to)) == true && __dataResult.Tables[0].Rows.Count == 0 && warning == true)
                {
                    MessageBox.Show("ไม่พบข้อมูล : " + ((MyLib._myTextBox)this._searchTextBox.Parent)._labelName);
                    this._setDataStr(fieldName, "", "", true);
                }

                // jead เอาไว้แก้ทีหลัง
                /*if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (__dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("เอกสารเลขที่ หรือ ยอดตัดจ่าย ห้ามว่าง"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }*/
            }
            return __result;
        }

    }

  

    public delegate void RadioButtonCheckedChangedEventHandler(object sender);
    public delegate void ChangeCreditDayEventHandler(int creditDay);
    public delegate void SelectInvoiceFromPosEventHandler();
    public delegate int VatCaseEventHandler(object sender);
    public delegate _g.g._vatTypeEnum VatTypeEventHandler();
}
