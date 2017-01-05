using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace SMLERPAPARControl._depositControl
{
    public class _po_so_deposit_screen_top_control : MyLib._myScreen
    {
        public string _screen_code = "";
        private _g.g._transControlTypeEnum _ictransControlTypeTemp;
        int _buildCount = 0;
        public string _docFormatCode = "";
        //ค้นหา         
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        // เอ้ต้องแยกการค้นหาเป็นคนละตัวไม่งั้นมันก็จะ load ใหม่ตลอดทำให้ช้า
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        string _searchName = "";
        TextBox _searchTextBox;
        string _old_filed_name = "";
        string _old_cust_code = "";
        /// <summary>
        /// ให้คำนวณวันที่เครดิตหรือไม่
        /// </summary>
        Boolean _processCredit = false;
        string __formatNumber_1 = _g.g._getFormatNumberStr(0, 0);

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                this._ictransControlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._ictransControlTypeTemp;
            }
        }

        public void _newData()
        {
            // 
            if (_g.g._companyProfile._disabled_edit_doc_no_doc_date == true || _g.g._companyProfile._disable_edit_doc_no_doc_date_user == true)
            {
                this._setDataDate(_g.d.ic_trans._doc_date, MyLib._myGlobal._workingDate);
                this._setDataStr(_g.d.ic_trans._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"));

                //this._enabedControl(_g.d.ic_trans._doc_no, false);
                this._enabedControl(_g.d.ic_trans._doc_date, false);
                this._enabedControl(_g.d.ic_trans._doc_time, false);
                ((MyLib._myTextBox)this._getControl(_g.d.ic_trans._doc_no)).textBox.Enabled = false;
            }

        }

        void _build()
        {
            if (this._icTransControlType == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            if (this._buildCount++ > 0)
            {
                MessageBox.Show("_PoSoDepositScreenTopControl สร้างมากกว่า 1 ครั้ง");
            }
            this.SuspendLayout();
            this._reset();
            this._table_name = _g.d.ic_trans._table;
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            string __formatNumberAmount = MyLib._myGlobal._getFormatNumber(_g.g._getFormatNumberStr(3));
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 25, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 25, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addComboBox(2, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addDateBox(3, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._deposit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(4, 1, 1, 0, _g.d.ic_trans._deposit_date, 1, true, true);
                    this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 255, 0, true, false, true, true, true);
                    this._addDateBox(5, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(6, 0, 2, 0, _g.d.ic_trans._remark, 2, 255, 0, true, false, true);
                    this._screen_code = "SRV";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 25, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addComboBox(2, 0, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 255, 4, true, false, true, true, true, _g.d.ic_trans._deposit_doc);
                    this._addDateBox(4, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, false);
                    this._addNumberBox(4, 1, 0, 0, _g.d.ic_trans._pay_deposit_buy, 1, 1, true, __formatNumberAmount, false, _g.d.ic_trans._pay_deposit_sell2);
                    this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 25, 4, true, false, true, true, true, _g.d.ic_trans._ar_code);
                    this._addNumberBox(8, 0, 0, 0, _g.d.ic_trans._deposit_day, 1, 1, true, __formatNumberNone);
                    this._addDateBox(8, 1, 1, 0, _g.d.ic_trans._deposit_date, 1, true, true);
                    this._addTextBox(9, 0, 2, 0, _g.d.ic_trans._remark_2, 2, 255, 0, true, false, true, false);
                    //
                    this._screen_code = "SRT";
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._enabedControl(_g.d.ic_trans._pay_deposit_buy, false);
                    this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._deposit_day, false);
                    this._enabedControl(_g.d.ic_trans._deposit_date, false);
                    this._enabedControl(_g.d.ic_trans._remark_2, false);

                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 25, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 25, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._deposit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(3, 1, 1, 0, _g.d.ic_trans._deposit_date, 1, true, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 255, 0, true, false, true, true, true);
                    this._addDateBox(4, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                    this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 255, 0, true, false, true);
                    this._screen_code = "SD";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                    this._maxColumn = 2;

                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 25, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 255, 4, true, false, true, true, true, _g.d.ic_trans._sell_deposit);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, false);
                    this._addNumberBox(3, 0, 0, 0, _g.d.ic_trans._pay_deposit_buy, 1, 1, true, __formatNumberAmount, false);
                    this._addTextBox(4, 0, 2, 0, _g.d.ic_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 25, 4, true, false, true, true, true, _g.d.ic_trans._ar_code);
                    this._addNumberBox(7, 0, 0, 0, _g.d.ic_trans._deposit_day, 1, 1, true, __formatNumberNone);
                    this._addDateBox(7, 1, 1, 0, _g.d.ic_trans._deposit_date, 1, true, true);
                    this._addTextBox(8, 0, 2, 0, _g.d.ic_trans._remark_2, 2, 255, 0, true, false, true, false);
                    //
                    this._screen_code = "SDR";
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._enabedControl(_g.d.ic_trans._pay_deposit_buy, false);
                    this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._deposit_day, false);
                    this._enabedControl(_g.d.ic_trans._deposit_date, false);
                    this._enabedControl(_g.d.ic_trans._remark_2, false);

                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 25, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 25, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._deposit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(3, 1, 1, 0, _g.d.ic_trans._deposit_date, 1, true, true);
                    this._addTextBox(4, 0, 2, 0, _g.d.ic_trans._remark, 2, 255, 0, true, false, true);
                    this._screen_code = "PD";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                    this._maxColumn = 2;

                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 25, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 255, 4, true, false, true, true, true, _g.d.ic_trans._buy_deposit);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, false);
                    this._addNumberBox(3, 0, 0, 0, _g.d.ic_trans._pay_deposit_buy, 1, 1, true, __formatNumberAmount, false);
                    this._addTextBox(4, 0, 2, 0, _g.d.ic_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 25, 4, true, false, true, true, true, _g.d.ic_trans._ap_code);
                    this._addNumberBox(7, 0, 0, 0, _g.d.ic_trans._deposit_day, 1, 1, true, __formatNumberNone);
                    this._addDateBox(7, 1, 1, 0, _g.d.ic_trans._deposit_date, 1, true, true);
                    this._addTextBox(8, 0, 2, 0, _g.d.ic_trans._remark_2, 2, 255, 0, true, false, true, true);
                    //
                    this._screen_code = "PDR";
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._enabedControl(_g.d.ic_trans._pay_deposit_buy, false);
                    this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._deposit_day, false);
                    this._enabedControl(_g.d.ic_trans._deposit_date, false);
                    this._enabedControl(_g.d.ic_trans._remark_2, false);
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);

                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                    {
                        int __row = 0;
                        this._maxColumn = 2;
                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 25, 1, true, false, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 25, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                        this._addComboBox(__row, 0, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);

                        if (MyLib._myGlobal._OEMVersion.Equals("imex"))
                        {
                            this._addComboBox(__row++, 0, _g.d.ic_trans._inquiry_type, true, _g.g._depositPayType, true);
                        }

                        this._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._deposit_day, 1, 2, true, __formatNumberNone);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._deposit_date, 1, true, true);
                        this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 255, 0, true, false, true);
                        this._screen_code = "PC";
                        this._processCredit = true;
                        this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    this._maxColumn = 2;

                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 25, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addComboBox(2, 0, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 255, 4, true, false, true, true, true, _g.d.ic_trans._buy_deposit);
                    this._addDateBox(4, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, false);
                    this._addNumberBox(4, 1, 0, 0, _g.d.ic_trans._pay_deposit_buy, 1, 1, true, __formatNumberAmount, false);
                    this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 25, 4, true, false, true, true, true, _g.d.ic_trans._ap_code);
                    this._addNumberBox(8, 0, 0, 0, _g.d.ic_trans._deposit_day, 1, 1, true, __formatNumberNone);
                    this._addDateBox(8, 1, 1, 0, _g.d.ic_trans._deposit_date, 1, true, true);
                    this._addTextBox(9, 0, 2, 0, _g.d.ic_trans._remark_2, 2, 255, 0, true, false, true, false);
                    //
                    this._screen_code = "PCR";
                    this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                    this._enabedControl(_g.d.ic_trans._pay_deposit_buy, false);
                    this._enabedControl(_g.d.ic_trans._cust_code, false);
                    this._enabedControl(_g.d.ic_trans._deposit_day, false);
                    this._enabedControl(_g.d.ic_trans._deposit_date, false);
                    this._enabedControl(_g.d.ic_trans._remark_2, false);

                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    this._processCredit = true;
                    break;
            }
            // jead : เอา iconNumber=1 มาเพิ่ม Event
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
                    else if (__getControlTextBox._iconNumber == 4)
                    {
                        __getControlTextBox.textBox.Leave -= new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.Leave += new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.KeyDown -= new KeyEventHandler(textBox_KeyDown);
                        __getControlTextBox.textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                }
            }
            // เวลา
            Control __timeControl = this._getControl(_g.d.ic_trans._doc_time);
            if (__timeControl != null)
            {
                ((MyLib._myTextBox)__timeControl)._isTime = true;
            }
            //
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenApControl__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_PoSoDepositScreenTopControl__textBoxChanged);
            this._textBoxSaved += new MyLib.TextBoxSavedHandler(_PoSoDepositScreenTopControl__textBoxSaved);
            this.Dock = DockStyle.Top;
            this.AutoSize = true;

            this.Invalidate();
            this.ResumeLayout();
        }

        void _PoSoDepositScreenTopControl__textBoxSaved(object sender, string name)
        {
            Control __timeControl = this._getControl(_g.d.ic_trans._doc_time);
            if (__timeControl != null)
            {
                if (this._getDataStr(_g.d.ic_trans._doc_time).Length == 0)
                {
                    this._setDataStr(_g.d.ic_trans._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"));
                }
            }
            //
            string __docDate = this._getDataStr(_g.d.ic_trans._doc_date).ToString();
            string __docNo = this._getDataStr(_g.d.ic_trans._doc_no).ToString();
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
                if (this._getDataStr(_g.d.ic_trans._tax_doc_no).ToString().Length == 0)
                {
                    this._setDataStr(_g.d.ic_trans._tax_doc_no, __docNo);
                }
            }
        }

        void _changeDocDate()
        {
            // คำนวณวันที่ใหม่ กรณีแก้ไขวันที่เอกสาร
            DateTime __dDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._doc_date));
            //
            this._setDataDate(_g.d.ic_trans._deposit_date, __dDate.AddDays((double)this._getDataNumber(_g.d.ic_trans._deposit_day)));
        }

        void _convrtDateToDay(string dateSource, string dateField, string dayField)
        {
            // แปลงค่าจากวันที่ คำนวณเป็นวัน
            DateTime __dDate = MyLib._myGlobal._convertDate(dateSource);
            DateTime __dt = MyLib._myGlobal._convertDate(this._getDataStr(dateField));
            TimeSpan __ts = __dt - __dDate;
            double __calcDay = Double.Parse(__ts.Days.ToString());
            if (__calcDay >= 0)
            {
                this._setDataNumber(dayField, decimal.Parse(__calcDay.ToString()));
            }
        }
        //somruk
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if (keyData == Keys.F9)
            {
                _screenApControl__textBoxSearch(this._getControl(_g.d.ic_trans._doc_format_code));
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        void _PoSoDepositScreenTopControl__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_trans._doc_no))
            {
                string __docNo = this._getDataStr(_g.d.ic_trans._doc_no);

                string __docType = __docNo;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                // toe กรณี running จาก POS ไป query เอา รหัสเอกสารมา
                if ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite) && _g.g._companyProfile._deposit_format_from_pos == true &&
                    (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน))
                {
                    // ดึงรหัสเอกสาร
                    DataSet __dataResult = __myFrameWork._queryShort("select * from " + _g.d.POS_ID._table + " where " + MyLib._myGlobal._addUpper(_g.d.POS_ID._MACHINECODE) + "=\'" + __docNo.ToUpper() + "\'");
                    if (__dataResult.Tables.Count > 0 && __dataResult.Tables[0].Rows.Count > 0)
                    {
                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                                __docNo = __dataResult.Tables[0].Rows[0][_g.d.POS_ID._doc_format_deposit_return].ToString();
                                break;
                            case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                                __docNo = __dataResult.Tables[0].Rows[0][_g.d.POS_ID._doc_format_deposit].ToString();
                                break;
                        }
                    }
                    else
                    {
                        __docNo = "";
                    }

                }

                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    this._docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docType, this._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, this._icTransControlType, _g.g._transControlTypeEnum.ว่าง);
                    this._setDataStr(_g.d.ic_trans._doc_no, __newDoc, "", true);
                    this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                }

            }
            else
                if (name.Equals(_g.d.ic_trans._doc_date))
            {
                this._changeDocDate();
            }
            else
                    if (name.Equals(_g.d.ic_trans._deposit_day))
            {
                this._changeDocDate();
            }
            else
                        if (name.Equals(_g.d.ic_trans._deposit_date))
            {
                this._convrtDateToDay(this._getDataStr(_g.d.ic_trans._doc_date), _g.d.ic_trans._deposit_date, _g.d.ic_trans._deposit_day);
            }
            else
                            if (name.Equals(_g.d.ic_trans._credit_day))
            {
                _calcCreditDate();
            }
            else
                                if (name.Equals(_g.d.ic_trans._tax_doc_date))
            {

                if (this._getDataStr(_g.d.ic_trans._tax_doc_date).Length == 0)
                {
                    this._setDataDate(_g.d.ic_trans._tax_doc_date, MyLib._myGlobal._workingDate);
                }
            }
            else
                                    if (name.Equals(_g.d.ic_trans._delivery_date))
            {

                DateTime dDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._delivery_date));
                if ((dDate.Year <= 1979))
                {
                    this._setDataDate(_g.d.ic_trans._delivery_date, MyLib._myGlobal._workingDate);
                }
                else
                {
                    DateTime newDate = dDate.AddDays((double)this._getDataNumber(_g.d.ic_trans._delivery_date));
                    this._setDataDate(_g.d.ic_trans._delivery_date, newDate);
                }
            }
            else
                                        if (name.Equals(_g.d.ic_trans._credit_date))
            {
                if (this._processCredit)
                {
                    DateTime __dDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._doc_date));
                    string __dateStr = this._getDataStr(_g.d.ic_trans._credit_date);
                    DateTime __date = MyLib._myGlobal._convertDate(__dateStr);
                    if (__date.Year <= 1979)
                    {
                        this._setDataDate(_g.d.ic_trans._credit_date, __dDate);
                    }
                    else
                    {
                        DateTime __dt = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._credit_date));
                        TimeSpan __ts = __dt - __dDate;
                        double __creditDay = Double.Parse(__ts.Days.ToString());
                        if (__creditDay >= 0)
                        {
                            this._setDataNumber(_g.d.ic_trans._credit_day, decimal.Parse(__creditDay.ToString()));

                            //__creditDay = 0;
                            _calcCreditDate();
                        }
                        //else
                        //{
                        //    this._setDataDate(_g.d.ic_trans._credit_date, __date);
                        //    _calcCreditDate();
                        //}
                    }
                }
            }
            else
                                            //somruk
                                            if (name.Equals(_g.d.ic_trans._doc_format_code))
            {
                string __docNo = this._getDataStr(_g.d.ic_trans._doc_format_code);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    this._docFormatCode = __getFormat.Rows[0][1].ToString();
                    this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                }
            }

            // singha 
            if (MyLib._myGlobal._OEMVersion == ("SINGHA") && name.Equals(_g.d.ic_trans._cust_code)
                && (
                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ ||
                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน))
            {
                string __cust_code = this._getDataStr(_g.d.ic_trans._cust_code);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __custStatusDataTable = __myFrameWork._queryShort("select " + _g.d.ar_customer._status + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + __cust_code + "\'").Tables[0];
                if (__custStatusDataTable.Rows.Count > 0)
                {
                    int __arStatus = MyLib._myGlobal._intPhase(__custStatusDataTable.Rows[0][_g.d.ar_customer._status].ToString());

                    if (__arStatus == 1)
                    {
                        MessageBox.Show("สถานะ ลูกค้าปิดการใช้งาน");
                        this._setDataStr(name, "", "", true);
                    }

                }

            }
            this._searchTextBox = (TextBox)sender;
            this._searchName = name;
            this._search(true);
        }

        /// <summary>
        /// หลังป้อนวันเครดิต เพื่อคำนวณหาวันที่
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void _creditDayTextBox_Leave(object sender, EventArgs e)
        //{

        //    _calcCreditDate();
        //}

        void _calcCreditDate()
        {
            if (this._processCredit)
            {
                DateTime __dDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._doc_date));
                //string xxxx = this._getDataNumber(_g.d.ic_trans._credit_day).ToString();
                if ((__dDate.Year > 1979))
                {

                    decimal __getDay = this._getDataNumber(_g.d.ic_trans._credit_day);
                    if (__getDay < 0)
                    {
                        __getDay = 0;
                        this._setDataNumber(_g.d.ic_trans._credit_day, __getDay);
                    }
                    else
                    {
                        DateTime __calcDate = __dDate.AddDays((double)__getDay);
                        //_g.g._count_credit_day = __getDay;
                        this._setDataDate(_g.d.ic_trans._credit_date, __calcDate);
                        this._setDataNumber(_g.d.ic_trans._credit_day, __getDay);

                    }

                }
            }
        }

        /// <summary>
        /// หลังป้อนวันที่ครบกำหนด เพื่อคำนวณวันที่เครดิต
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void _creditDateTextBox_Leave(object sender, EventArgs e)
        //{
        //    if (this._processCredit)
        //    {
        //        DateTime __dDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._doc_date));
        //        string __dateStr = this._getDataStr(_g.d.ic_trans._credit_date);
        //        DateTime __date = MyLib._myGlobal._convertDate(__dateStr);
        //        if (__date.Year < 1979)
        //        {
        //            this._setDataDate(_g.d.ic_trans._credit_date, __dDate);
        //        }
        //        if (__dDate.Year > 1979)
        //        {
        //            DateTime __dt = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._credit_date));
        //            TimeSpan __ts = __dt - __dDate;
        //            double __creditDay = Double.Parse(__ts.Days.ToString());
        //            if (__creditDay > 0)
        //            {
        //                this._setDataNumber(_g.d.ic_trans._credit_day, decimal.Parse(__creditDay.ToString()));

        //                //__creditDay = 0;
        //                _calcCreditDate();
        //            }
        //        }
        //    }
        //}

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible)
                {
                    this._search_data_full_pointer.Focus();
                    this._search_data_full_pointer._firstFocus();
                }
            }
        }

        public void _deleteSearchList(MyLib._myTextBox source)
        {
            Boolean __found = false;
            int __addr = 0;
            for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
            {
                if (source._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                {
                    __addr = __loop;
                    __found = true;
                    break;
                }
            }
            if (__found)
            {
                this._search_data_full_buffer_name.RemoveAt(__addr);
                this._search_data_full_buffer.RemoveAt(__addr);
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            // jead
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _screenApControl__textBoxSearch(__getControl);
        }

        void _screenApControl__textBoxSearch(object sender)
        {
            this._saveLastControl();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchName = __getControl._name.ToLower();
            string label_name = __getControl._labelName;
            string _searchScreenName = _search_screen_neme(this._searchName);
            string __where_query = "";
            if (_searchScreenName.Equals(_g.g._screen_erp_doc_format))
            {
                __where_query = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screen_code.ToUpper() + "\'";

                if ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite) && _g.g._companyProfile._deposit_format_from_pos == true &&
                    (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน))
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
                    // ดึงรหัสเครื่อง POS
                    return;
                }

            }
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
                }
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible == true)
                {
                    this._search_data_full_pointer.Visible = false;
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;

                if (this._searchName.Equals("doc_no"))
                {
                    if (_g.g._companyProfile._branchStatus == 1 || _g.g._companyProfile._change_branch_code == false)
                    {
                        this._search_data_full_pointer._dataList._extraWhere2 = " ((coalesce(" + _g.d.erp_doc_format._use_branch_select + ", 0) = 0 ) or (" + _g.d.erp_doc_format._branch_list + " like '%" + MyLib._myGlobal._branchCode + "%'))";

                    }
                }
                //
            }
            //ค้นหารหัสลูกหนี้
            //ค้นหาหน้าจอ Top               

            if (!this._search_data_full_pointer._name.Equals(_searchScreenName.ToLower()))
            {
                // แก้แบบนี้เด้อ
                if (this._search_data_full_pointer._name.Length == 0)
                {
                    this._search_data_full_pointer._showMode = 0;
                    this._search_data_full_pointer._name = _searchScreenName;
                    this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                    this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);


                }
            }

            //ส่ง string where query ให้ pop up 
            if (this._searchName.Equals(_g.d.ic_trans._doc_ref))
            {
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                        {
                            string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ).ToString();
                            string __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ).ToString();
                            __where_query = _g.d.ic_trans._trans_type + "=" + __transType + " and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                        {
                            string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า).ToString();
                            string __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน).ToString();
                            __where_query = _g.d.ic_trans._trans_type + "=" + __transType + " and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                        {
                            string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า).ToString();
                            string __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า).ToString();
                            __where_query = _g.d.ic_trans._trans_type + "=" + __transType + " and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                        {
                            string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ).ToString();
                            string __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ).ToString();
                            __where_query = _g.d.ic_trans._trans_type + "=" + __transType + " and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;
                        }
                        break;
                }
            }

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && _searchName.Equals(_g.d.ap_ar_trans._cust_code) && (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน ||
                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน ||
                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ ||
                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า))
            {
                __where_query = _g.d.ar_customer._status + "=0 ";
            }
            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false);
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
            //}
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
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _searchAll(string name, int row)
        {
            string _search_text_new = name;
            string __result = "";
            int __keyColumn = 0;
            if (name.Length > 0)
            {
                //somruk เพิ่ม || name.Equals(_g.g._screen_po_deposit)
                if (name.Equals(_g.g._screen_ap_trans) || name.Equals(_g.g._screen_ar_trans) || name.Equals(_g.g._screen_po_deposit))
                {
                    __keyColumn = 1;
                }
                __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, __keyColumn);

                if (__result.Length != 0)
                {
                    this._search_data_full_pointer.Visible = false;
                    this._setDataStr(_searchName, __result);
                    this._search(true);
                }
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                        {
                            string __query = MyLib._myUtil._convertTextToXml("select " + _g.d.ic_trans._cust_code + "," + _g.d.ic_trans._deposit_day + "," + _g.d.ic_trans._deposit_date + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._doc_date +
                                " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'");
                            DataTable __getData = _myFrameWork._queryShort(__query).Tables[0];
                            string __custCode = "";
                            decimal __totalAmount = 0M;
                            string __remark2 = "";
                            decimal __depositDay = 0M;
                            DateTime __docDate = new DateTime();
                            DateTime __depositDate = new DateTime();
                            if (__getData.Rows.Count > 0)
                            {
                                DataRow __data = (DataRow)__getData.Rows[0];
                                __custCode = __data[_g.d.ic_trans._cust_code].ToString();
                                __remark2 = __data[_g.d.ic_trans._remark].ToString();
                                __totalAmount = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans._total_amount].ToString());
                                __depositDay = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans._deposit_day].ToString());
                                __depositDate = MyLib._myGlobal._convertDateFromQuery(__data[_g.d.ic_trans._deposit_date].ToString());
                                __docDate = MyLib._myGlobal._convertDateFromQuery(__data[_g.d.ic_trans._doc_date].ToString());
                            }
                            this._setDataStr(_g.d.ic_trans._cust_code, __custCode, __custCode, true);
                            this._setDataStr(_g.d.ic_trans._remark2, __remark2, "", true);
                            this._setDataNumber(_g.d.ic_trans._pay_deposit_buy, __totalAmount);
                            this._setDataNumber(_g.d.ic_trans._deposit_day, __depositDay);
                            this._setDataDate(_g.d.ic_trans._deposit_date, __depositDate);
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                        {
                            string __query = MyLib._myUtil._convertTextToXml("select " + _g.d.ic_trans._cust_code + "," + _g.d.ic_trans._deposit_day + "," + _g.d.ic_trans._deposit_date + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._doc_date +
                                " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'");
                            DataTable __getData = _myFrameWork._queryShort(__query).Tables[0];
                            string __custCode = "";
                            decimal __totalAmount = 0M;
                            string __remark2 = "";
                            decimal __depositDay = 0M;
                            DateTime __docDate = new DateTime();
                            DateTime __depositDate = new DateTime();
                            if (__getData.Rows.Count > 0)
                            {
                                DataRow __data = (DataRow)__getData.Rows[0];
                                __custCode = __data[_g.d.ic_trans._cust_code].ToString();
                                __remark2 = __data[_g.d.ic_trans._remark].ToString();
                                __totalAmount = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans._total_amount].ToString());
                                __depositDay = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans._deposit_day].ToString());
                                __depositDate = MyLib._myGlobal._convertDateFromQuery(__data[_g.d.ic_trans._deposit_date].ToString());
                                __docDate = MyLib._myGlobal._convertDateFromQuery(__data[_g.d.ic_trans._doc_date].ToString());
                            }
                            this._setDataStr(_g.d.ic_trans._cust_code, __custCode, __custCode, true);
                            this._setDataStr(_g.d.ic_trans._remark2, __remark2, "", true);
                            this._setDataNumber(_g.d.ic_trans._pay_deposit_buy, __totalAmount);
                            this._setDataNumber(_g.d.ic_trans._deposit_day, __depositDay);
                            this._setDataDate(_g.d.ic_trans._deposit_date, __depositDate);
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                        {
                            string __query = MyLib._myUtil._convertTextToXml("select " + _g.d.ic_trans._cust_code + "," + _g.d.ic_trans._deposit_day + "," + _g.d.ic_trans._deposit_date + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._doc_date +
                                " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'");
                            DataTable __getData = _myFrameWork._queryShort(__query).Tables[0];
                            string __custCode = "";
                            decimal __totalAmount = 0M;
                            string __remark2 = "";
                            decimal __depositDay = 0M;
                            DateTime __docDate = new DateTime();
                            DateTime __depositDate = new DateTime();
                            if (__getData.Rows.Count > 0)
                            {
                                DataRow __data = (DataRow)__getData.Rows[0];
                                __custCode = __data[_g.d.ic_trans._cust_code].ToString();
                                __remark2 = __data[_g.d.ic_trans._remark].ToString();
                                __totalAmount = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans._total_amount].ToString());
                                __depositDay = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans._deposit_day].ToString());
                                __depositDate = MyLib._myGlobal._convertDateFromQuery(__data[_g.d.ic_trans._deposit_date].ToString());
                                __docDate = MyLib._myGlobal._convertDateFromQuery(__data[_g.d.ic_trans._doc_date].ToString());
                            }
                            this._setDataStr(_g.d.ic_trans._cust_code, __custCode, __custCode, true);
                            this._setDataStr(_g.d.ic_trans._remark2, __remark2, "", true);
                            this._setDataNumber(_g.d.ic_trans._pay_deposit_buy, __totalAmount);
                            this._setDataNumber(_g.d.ic_trans._deposit_day, __depositDay);
                            this._setDataDate(_g.d.ic_trans._deposit_date, __depositDate);
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);
                        }
                        break;
                    //somruk เพิ่ม
                    case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                        {
                            string __returnDocNo = this._getDataStr(_g.d.ic_trans._doc_ref);

                            string __query = MyLib._myUtil._convertTextToXml("select " + _g.d.ic_trans._cust_code + "," + _g.d.ic_trans._deposit_day + "," + _g.d.ic_trans._deposit_date + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_success +
                                " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __returnDocNo.ToUpper() + "\'");
                            DataTable __getData = _myFrameWork._queryShort(__query).Tables[0];
                            string __custCode = "";
                            decimal __totalAmount = 0M;
                            string __remark2 = "";
                            decimal __depositDay = 0M;
                            DateTime __docDate = new DateTime();
                            DateTime __depositDate = new DateTime();
                            if (__getData.Rows.Count > 0)
                            {
                                if (__getData.Rows[0][_g.d.ic_trans._doc_success].ToString().Equals("1"))
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("เอกสาร ") + __returnDocNo + " " + MyLib._myGlobal._resource("ไม่มียอดคงเหลือแล้ว"));
                                    this._setDataStr(_g.d.ic_trans._doc_ref, "");
                                }
                                else
                                {
                                    DataRow __data = (DataRow)__getData.Rows[0];
                                    __custCode = __data[_g.d.ic_trans._cust_code].ToString();
                                    __remark2 = __data[_g.d.ic_trans._remark].ToString();
                                    __totalAmount = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans._total_amount].ToString());
                                    __depositDay = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_trans._deposit_day].ToString());
                                    __depositDate = MyLib._myGlobal._convertDateFromQuery(__data[_g.d.ic_trans._deposit_date].ToString());
                                    __docDate = MyLib._myGlobal._convertDateFromQuery(__data[_g.d.ic_trans._doc_date].ToString());
                                }
                            }
                            this._setDataStr(_g.d.ic_trans._cust_code, __custCode, __custCode, true);
                            this._setDataStr(_g.d.ic_trans._remark2, __remark2, "", true);
                            this._setDataNumber(_g.d.ic_trans._pay_deposit_buy, __totalAmount);
                            this._setDataNumber(_g.d.ic_trans._deposit_day, __depositDay);
                            this._setDataDate(_g.d.ic_trans._deposit_date, __depositDate);
                            this._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);
                        }
                        break;
                }
                {
                    // ค้นหาชื่อกลุ่มลูกค้า
                    StringBuilder __myquery = new StringBuilder();
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._doc_group).ToUpper() + "\'"));
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_side_list._name_1 + " from " + _g.d.erp_side_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._side_code).ToUpper() + "\'"));
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._department_code).ToUpper() + "\'"));
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_project_list._name_1 + " from " + _g.d.erp_project_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._project_code).ToUpper() + "\'"));
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_allocate_list._name_1 + " from " + _g.d.erp_allocate_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._allocate_code).ToUpper() + "\'"));
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._sale_code).ToUpper() + "\'"));
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_job_list._name_1 + " from " + _g.d.erp_job_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._job_code).ToUpper() + "\'"));
                    if (_g.g._transType(this._icTransControlType) == _g.g._transTypeEnum.ซื้อ_เจ้าหนี้ || _g.g._transType(this._icTransControlType) == _g.g._transTypeEnum.เจ้าหนี้)
                    {
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._cust_code).ToUpper() + "\'"));
                    }
                    else
                    {
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._cust_code).ToUpper() + "\'"));
                    }
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._cust_code
                        + ","
                        + _g.d.ic_trans._sale_code
                        + ","
                        + _g.d.ic_trans._department_code
                        + ","
                        + _g.d.ic_trans._project_code
                        + ","
                        + _g.d.ic_trans._allocate_code
                        + ","
                        + _g.d.ic_trans._delivery_date
                        + ","
                        + _g.d.ic_trans._remark
                        + ","
                        + _g.d.ic_trans._tax_doc_no
                        + ","
                        + _g.d.ic_trans._tax_doc_date
                        + ","
                        + _g.d.ic_trans._vat_type
                        + ","
                        + _g.d.ic_trans._doc_group
                        + ","
                        + _g.d.ic_trans._pay_amount
                        + ","
                        + _g.d.ic_trans._credit_date
                        + ","
                        + _g.d.ic_trans._credit_day
                        + " from " + _g.d.ic_trans._table + " where doc_no=\'" + this._getDataStr(_g.d.ic_trans._doc_ref).ToUpper() + "\'"));

                    __myquery.Append("</node>");
                    ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                    _searchAndWarning(_g.d.ic_trans._doc_group, (DataSet)_getData[0], warning);
                    _searchAndWarning(_g.d.ic_trans._side_code, (DataSet)_getData[1], warning);
                    _searchAndWarning(_g.d.ic_trans._department_code, (DataSet)_getData[2], warning);
                    _searchAndWarning(_g.d.ic_trans._project_code, (DataSet)_getData[3], warning);
                    _searchAndWarning(_g.d.ic_trans._allocate_code, (DataSet)_getData[4], warning);
                    _searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[5], warning);
                    _searchAndWarning(_g.d.ic_trans._job_code, (DataSet)_getData[6], warning);
                    _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[7], warning);

                    if (this._icTransControlType != _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ && this._icTransControlType != _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า)
                    {
                        _searchAndWarning(_g.d.ic_trans._doc_ref, (DataSet)_getData[8], warning);
                    }
                }
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true); // jead เพิ่มให้
                if (fieldName.Equals(_g.d.ic_trans._cust_code))
                {
                    if (!this._old_cust_code.Equals(__getDataStr))
                    {
                        this._old_cust_code = __getDataStr;
                    }
                }
                else
                {
                    this._setDataStr(fieldName, __getDataStr, __getData, true);
                }
            }
            else
            {
                if (this._searchTextBox != null)
                {
                    if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                    {
                        if (dataResult.Tables[0].Rows.Count == 0 && warning)
                        {
                            MyLib._myTextBox __getTextBox = (MyLib._myTextBox)(MyLib._myTextBox)this._searchTextBox.Parent;
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getTextBox._labelName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            __getTextBox._textFirst = "";
                            __getTextBox._textSecond = "";
                            __getTextBox._textLast = "";
                            this._setDataStr(fieldName, "", "", true);
                            __getTextBox.Focus();
                            __getTextBox.textBox.Focus();
                            __result = true;
                        }
                    }
                }
            }
            return __result;
        }

        string _search_screen_neme(string _name)
        {
            if (_name.Equals(_g.d.ic_trans._doc_group)) return _g.g._search_screen_erp_doc_group;
            if (_name.Equals(_g.d.ic_trans._cust_code)) return (_g.g._transType(this._icTransControlType) == _g.g._transTypeEnum.เจ้าหนี้ || _g.g._transType(this._icTransControlType) == _g.g._transTypeEnum.ซื้อ_เจ้าหนี้) ? _g.g._search_screen_ap : _g.g._search_screen_ar;
            if (_name.Equals(_g.d.ic_trans._project_code)) return _g.g._search_master_erp_project_list;
            if (_name.Equals(_g.d.ic_trans._allocate_code)) return _g.g._search_master_erp_allocate_list;
            if (_name.Equals(_g.d.ic_trans._job_code)) return _g.g._search_master_erp_job_list;
            if (_name.Equals(_g.d.ic_trans._side_code)) return _g.g._search_screen_erp_side_list;
            if (_name.Equals(_g.d.ic_trans._department_code)) return _g.g._search_screen_erp_department_list;
            if (_name.Equals(_g.d.ic_trans._sale_area_code)) return _g.g._search_master_ar_area_code;
            if (_name.Equals(_g.d.ic_trans._sale_code)) return _g.g._search_screen_erp_user;
            if (_name.Equals(_g.d.ic_trans._doc_ref)) return (_g.g._transType(this._icTransControlType) == _g.g._transTypeEnum.เจ้าหนี้ || _g.g._transType(this._icTransControlType) == _g.g._transTypeEnum.ซื้อ_เจ้าหนี้) ? _g.g._screen_po_deposit : _g.g._screen_so_deposit;
            if (_name.Equals(_g.d.ic_trans._doc_no)) return _g.g._screen_erp_doc_format;
            if (_name.Equals(_g.d.ic_trans._doc_format_code)) return _g.g._screen_erp_doc_format;
            return "";
        }
    }
}
