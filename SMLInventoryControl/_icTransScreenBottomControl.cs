using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public class _icTransScreenBottomControl : MyLib._myScreen
    {
        //#screenbottom
        public delegate void _afterCalcEvent();
        public delegate decimal _advanceAmountEvent(_g.g._vatTypeEnum vatType);
        public event _afterCalcEvent _afterCalc;
        public event _advanceAmountEvent _advanceAmount;
        //
        private _g.g._transControlTypeEnum _ictransControlTypeTemp;
        private _g.g._vatTypeEnum _vatTypeTemp = _g.g._vatTypeEnum.ภาษีแยกนอก;
        public _icTransScreenTopControl _screenTopRef;
        public _icTransItemGridControl _itemGrid = null;

        private ArrayList __searchScreenMasterList = new ArrayList();
        private SMLERPGlobal._searchProperties __searchScreenProperties = new SMLERPGlobal._searchProperties();
        private TextBox _searchTextBox;
        private string _searchName = "";
        private ArrayList _search_data_full_buffer = new ArrayList();
        private ArrayList _search_data_full_buffer_name = new ArrayList();
        private int _search_data_full_buffer_addr = -1;
        private MyLib._searchDataFull _search_data_full_pointer;
        private string _old_filed_name = "";

        public _g.g._vatTypeEnum _vatType
        {
            set
            {
                this._vatTypeTemp = value;
                if (this._itemGrid != null)
                {
                    this._calc(this._itemGrid);
                }
            }
            get
            {
                return this._vatTypeTemp;
            }
        }

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                if (MyLib._myGlobal._isDesignMode == false)
                {
                    this._ictransControlTypeTemp = value;
                    if (this._ictransControlTypeTemp != _g.g._transControlTypeEnum.ว่าง)
                    {
                        this._build();
                    }
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
            this._setDataNumber(_g.d.ic_trans._vat_rate, _g.g._companyProfile._vat_rate);
        }

        public _icTransScreenBottomControl()
        {
            this._textBoxSearch += _icTransScreenBottomControl__textBoxSearch;
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_ictransScreenBottomControl__textBoxChanged);
        }

        private void _icTransScreenBottomControl__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
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

                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                //
            }
            //   MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._label._field_name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            __searchScreenMasterList.Clear();
            try
            {
                {
                    string __extraWhere = "";
                    __searchScreenMasterList = __searchScreenProperties._setColumnSearch(this._searchName, 0, "", this._screenTopRef._screen_code);
                    if (__searchScreenMasterList.Count > 0)
                    {
                        if (!this._search_data_full_pointer._name.Equals(__searchScreenMasterList[0].ToString().ToLower()))
                        {
                            if (this._search_data_full_pointer._name.Length == 0)
                            {
                                this._search_data_full_pointer._name = __searchScreenMasterList[0].ToString();
                                this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                                // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                                this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                                this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                                //
                                this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                                this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                            }
                        }
                        __extraWhere = (__searchScreenMasterList.Count == 3) ? __searchScreenMasterList[2].ToString() : "";
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
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + " : " + __ex.StackTrace.ToString());
            }
        }

        public void _search()
        {
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // curency
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_currency._name_1 + " from " + _g.d.erp_currency._table + " where " + _g.d.erp_currency._code + "=\'" + this._getDataStr(_g.d.ic_trans._currency_code) + "\'"));

            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._getDataStr(_g.d.ic_trans._sender_code) + "\'"));

            __myquery.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            _searchAndWarning(_g.d.ic_trans._currency_code, (DataSet)__getData[0], false);
            _searchAndWarning(_g.d.ic_trans._sender_code, (DataSet)__getData[1], false);

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
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
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
                }
            }
            return __result;
        }


        void _searchAll(string screenName, int row)
        {
            int __columnNumber = 0;
            string __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._search_data_full_pointer.Visible = false;
                this._setDataStr(this._searchName, __result, "", false);
                this._search();
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
            SendKeys.Send("{ENTER}");
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
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

        public void _ictransScreenBottomControl__textBoxChanged(object sender, string name)
        {
            DateTime __docDate = MyLib._myGlobal._convertDate(this._screenTopRef._getDataStr(_g.d.ic_trans._doc_date));
            if (name.Equals(_g.d.ic_trans._expire_date))
            {
                DateTime __getDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._expire_date));
                TimeSpan __diffDate = __getDate.Subtract(__docDate);
                this._setDataNumber(_g.d.ic_trans._expire_day, (decimal)__diffDate.Days);
            }
            else
                if (name.Equals(_g.d.ic_trans._credit_date))
            {
                DateTime __getDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._credit_date));
                TimeSpan __diffDate = __getDate.Subtract(__docDate);
                this._setDataNumber(_g.d.ic_trans._credit_day, (decimal)__diffDate.Days);
            }
            else
                    if (name.Equals(_g.d.ic_trans._send_date))
            {
                DateTime __getDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ic_trans._send_date));
                TimeSpan __diffDate = __getDate.Subtract(__docDate);
                this._setDataNumber(_g.d.ic_trans._send_day, (decimal)__diffDate.Days);
            }
            this._calc(this._itemGrid);
            this.Invalidate();
        }

        /// <summary>
        /// คำนวณยอด
        /// </summary>
        public void _calc(_icTransItemGridControl itemGrid)
        {
            string __manual = this._getDataStr(_g.d.ic_trans._total_manual).ToString();

            int __sumAmountColumn = itemGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount);

            if (_g.g._companyProfile._multi_currency == true)
            {
                int __sumAmountColumn2 = itemGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount_2);
                if (__sumAmountColumn2 != -1)
                {
                    __sumAmountColumn = __sumAmountColumn2;
                }
            }

            if (__manual.Equals("1") == false)
            {
                decimal __totalAdvance = (this._advanceAmount == null) ? 0M : this._advanceAmount(this._vatType);
                this._setDataNumber(_g.d.ic_trans._advance_amount, __totalAdvance);
                //
                decimal __totalValueVat = 0M;
                decimal __totalValueNoVat = 0M;
                for (int __row = 0; __row < itemGrid._rowData.Count; __row++)
                {
                    int __itemType = (int)itemGrid._cellGet(__row, _g.d.ic_trans_detail._item_type);
                    if (__itemType != 3 && __itemType != 5)
                    {
                        // 0=มีภาษี,1=ยกเว้นภาษี
                        if ((int)itemGrid._cellGet(__row, _g.d.ic_trans_detail._tax_type) == 0)
                        {
                            __totalValueVat += (decimal)itemGrid._cellGet(__row, __sumAmountColumn);
                        }
                        else
                        {
                            __totalValueNoVat += (decimal)itemGrid._cellGet(__row, __sumAmountColumn);
                        }
                    }
                }
                //decimal __totalValue = this._getDataNumber(_g.d.ic_trans._total_value);
                decimal __totalValue = __totalValueVat + __totalValueNoVat;
                decimal __totalDiscount = __totalValue - MyLib._myGlobal._calcAfterDiscount(this._getDataStr(_g.d.ic_trans._discount_word), __totalValue, _g.g._companyProfile._item_amount_decimal);
                decimal __vatRate = this._getDataNumber(_g.d.ic_trans._vat_rate);
                decimal __beforeVat = 0;
                decimal __vatValue = 0;
                decimal __afterVat = 0;
                decimal __totalAmount = 0;
                decimal __totalServiceCharge = 0;

                if ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong) || (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro))
                {
                    // คิด service charge
                    __totalServiceCharge = this._getDataNumber(_g.d.ic_trans._total_service_charge);
                }

                switch (this._vatType)
                {
                    case _g.g._vatTypeEnum.ภาษีแยกนอก:
                        {
                            if (_g.g._companyProfile._discount_type == 1)
                            {
                                // ลดก่อนไปคิด vat
                                decimal __discountNoVatAmount = 0M;

                                // กรณี ยอดก่อน ภาษี น้อยกว่าส่วนลด 
                                // __beforeVat = (__totalValueVat - __totalDiscount) - __totalAdvance;
                                //__beforeVat = (__totalValueVat < (__totalDiscount + __totalAdvance)) ? 0 : (__totalValueVat - __totalDiscount) - __totalAdvance;

                                if (__totalValueVat < (__totalDiscount + __totalAdvance))
                                {
                                    // ส่วนลด ไม่พอลดยอดก่อนภาษี 
                                    __beforeVat = 0;
                                    __discountNoVatAmount = (__totalDiscount - __totalValueVat);
                                }
                                else
                                {
                                    // ส่วนลด พอลดยอดก่อนภาษีทั้งหมด
                                    __beforeVat = (__totalValueVat - __totalDiscount) - __totalAdvance;
                                }

                                __vatValue = (__totalValueVat < (__totalDiscount + __totalAdvance)) ? 0 : MyLib._myGlobal._round(__beforeVat * (__vatRate / 100.0M), _g.g._companyProfile._item_amount_decimal);
                                __afterVat = __beforeVat + __vatValue;
                                //__totalAmount = ( __totalValueNoVat + __afterVat ) - __discountNoVatAmount;

                                __totalValueNoVat = __totalValueNoVat - __discountNoVatAmount;
                                __totalAmount = (__totalValueNoVat + __afterVat);
                            }
                            else
                            {
                                __beforeVat = __totalValueVat - __totalAdvance;
                                __vatValue = MyLib._myGlobal._round(__beforeVat * (__vatRate / 100.0M), _g.g._companyProfile._item_amount_decimal);
                                __afterVat = __beforeVat + __vatValue;
                                __totalAmount = (__beforeVat + __totalValueNoVat + __vatValue) - (__totalDiscount);

                            }

                        }
                        break;
                    case _g.g._vatTypeEnum.ภาษีรวมใน:
                        {
                            // toe
                            if (_g.g._companyProfile._discount_type == 1)
                            {
                                // ลดกอน vat
                                __totalAmount = ((__totalValue + __totalServiceCharge) - __totalDiscount) - __totalAdvance;

                                // กรณี ยอดก่อน ภาษี น้อยกว่าส่วนลด 
                                //__beforeVat = MyLib._myGlobal._round((((__totalValueVat + __totalServiceCharge) - (__totalDiscount + __totalAdvance)) * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                //__vatValue = MyLib._myGlobal._round(((__totalValueVat + __totalServiceCharge) - (__totalDiscount + __totalAdvance)) - __beforeVat, _g.g._companyProfile._item_amount_decimal);
                                __beforeVat = ((__totalValueVat + __totalServiceCharge) < (__totalDiscount + __totalAdvance)) ? 0 : MyLib._myGlobal._round((((__totalValueVat + __totalServiceCharge) - (__totalDiscount + __totalAdvance)) * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                __vatValue = ((__totalValueVat + __totalServiceCharge) < (__totalDiscount + __totalAdvance)) ? 0 : MyLib._myGlobal._round(((__totalValueVat + __totalServiceCharge) - (__totalDiscount + __totalAdvance)) - __beforeVat, _g.g._companyProfile._item_amount_decimal);
                                __afterVat = __beforeVat + __vatValue;
                            }
                            else
                            {
                                // แบบเดิม
                                __totalAmount = (__totalValue - __totalDiscount) - __totalAdvance;
                                //__beforeVat = MyLib._myGlobal._round(((__totalValueVat) * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                __beforeVat = MyLib._myGlobal._round((((__totalValueVat + __totalServiceCharge) - __totalAdvance) * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                __vatValue = MyLib._myGlobal._round(((__totalValueVat + __totalServiceCharge) - __totalAdvance) - __beforeVat, _g.g._companyProfile._item_amount_decimal);
                                __afterVat = __beforeVat + __vatValue;
                            }
                        }
                        break;
                    case _g.g._vatTypeEnum.ยกเว้นภาษี:
                        __vatValue = 0;
                        __totalAmount = (__totalValue - __totalDiscount) - __totalAdvance;
                        break;
                }
                //
                //  this._setDataNumber(_g.d.ic_trans._total_value, __totalValueVat + __totalValueNoVat); // toe 2016-03-16
                this._setDataNumber(_g.d.ic_trans._total_value, __totalValue);
                this._setDataNumber(_g.d.ic_trans._total_discount, __totalDiscount);
                this._setDataNumber(_g.d.ic_trans._total_before_vat, __beforeVat);
                this._setDataNumber(_g.d.ic_trans._total_vat_value, __vatValue);
                this._setDataNumber(_g.d.ic_trans._total_after_vat, __afterVat);
                this._setDataNumber(_g.d.ic_trans._total_except_vat, __totalValueNoVat);

                if (_g.g._companyProfile._multi_currency)
                {
                    decimal __exchangeRate = (this._getDataNumber(_g.d.ic_trans._exchange_rate) == 0) ? 1 : this._getDataNumber(_g.d.ic_trans._exchange_rate);
                    this._setDataNumber(_g.d.ic_trans._total_amount_2, __totalAmount);
                    this._setDataNumber(_g.d.ic_trans._total_amount, MyLib._myGlobal._round(__totalAmount * __exchangeRate, _g.g._companyProfile._item_amount_decimal));
                }
                else
                {
                    this._setDataNumber(_g.d.ic_trans._total_amount, __totalAmount);
                }
            }
            try
            {
                if (this._screenTopRef != null)
                {
                    DateTime __docDate = MyLib._myGlobal._convertDate(this._screenTopRef._getDataStr(_g.d.ic_trans._doc_date));
                    // วันสิ้นอายุเอกสาร
                    int __getExpireDay = (int)this._getDataNumber(_g.d.ic_trans._expire_day);
                    this._setDataDate(_g.d.ic_trans._expire_date, __docDate.AddDays(__getExpireDay));
                    // วันเครดิต
                    int __getCreditDay = (int)this._getDataNumber(_g.d.ic_trans._credit_day);
                    this._setDataDate(_g.d.ic_trans._credit_date, __docDate.AddDays(__getCreditDay));
                    // วันรับส่งสินค้า
                    int __getSendDay = (int)this._getDataNumber(_g.d.ic_trans._send_day);
                    this._setDataDate(_g.d.ic_trans._send_date, __docDate.AddDays(__getSendDay));
                }
            }
            catch
            {
            }
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    break;

                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    {
                        if (__manual.Equals("1") && MyLib._myGlobal._OEMVersion.Equals("imex"))
                        {
                            // คำณวน ผลต่าง เอง
                            decimal __refAmount = this._getDataNumber(_g.d.ic_trans._ref_amount);
                            decimal __refDiff = this._getDataNumber(_g.d.ic_trans._total_before_vat);
                            decimal __refRealAmount = __refAmount - __refDiff;
                            if (__refDiff < 0)
                            {
                                __refDiff = __refDiff * -1;
                            }

                            this._setDataNumber(_g.d.ic_trans._ref_new_amount, __refRealAmount);
                            this._setDataNumber(_g.d.ic_trans._ref_diff, __refDiff);
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    {
                        if (__manual.Equals("1") && MyLib._myGlobal._OEMVersion.Equals("imex"))
                        {
                            // คำณวน ผลต่าง เอง
                            decimal __refAmount = this._getDataNumber(_g.d.ic_trans._ref_amount);
                            decimal __refDiff = this._getDataNumber(_g.d.ic_trans._total_before_vat);
                            decimal __refRealAmount = __refAmount + __refDiff;
                            if (__refDiff < 0)
                            {
                                __refDiff = __refDiff * -1;
                            }

                            this._setDataNumber(_g.d.ic_trans._ref_new_amount, __refRealAmount);
                            this._setDataNumber(_g.d.ic_trans._ref_diff, __refDiff);
                        }
                    }
                    break;
            }
            if (this._afterCalc != null)
            {
                this._afterCalc();
            }
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            string __formatNumber = MyLib._myGlobal._getFormatNumber(_g.g._getFormatNumberStr(3));
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            int row = 0;

            switch (this._icTransControlType)
            {
                //case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก :
                //    this._maxColumn = 4;
                //    this._table_name = _g.d.ic_trans._table;
                //    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                //    this._enabedControl(_g.d.ic_trans._total_amount, false);
                //    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;

                #region ซื้อ
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;

                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                      MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                      MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                      MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);

                        if (_g.g._companyProfile._multi_currency)
                        {
                            ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                            this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                        }

                    }
                    else
                    {
                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);

                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    }

                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    row = 0;
                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);

                    }
                    else
                    {
                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);

                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    }

                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    //
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    row = 0;

                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);


                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                      MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                      MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                      MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);

                    }
                    else
                    {
                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);

                    }

                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    //
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);

                    }
                    else
                    {
                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);

                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    }

                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    //
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    row = 0;

                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    }
                    else
                    {
                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                        this._addCheckBox(row, 2, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);

                    }

                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    //
                    break;

                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    {
                        this._maxColumn = 4;
                        this._table_name = _g.d.ic_trans._table;
                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                        this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                            this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                            this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                            this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                            this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        }
                        else
                        {
                            this._addTextBox(row, 0, 3, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                            this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                            this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                        }

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);

                        this._addCheckBox(row, 1, _g.d.ic_trans._total_manual, true, false);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);

                        if (_g.g._companyProfile._multi_currency)
                        {
                            ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                            this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                        }
                        //
                        this._enabedControl(_g.d.ic_trans._advance_amount, false);
                        this._enabedControl(_g.d.ic_trans._total_discount, false);
                        this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    row = 0;

                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                      MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                      MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                      MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);

                    }
                    else
                    {
                        this._addTextBox(row, 0, 3, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                    }

                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);

                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);

                    this._addCheckBox(row, 1, _g.d.ic_trans._total_manual, true, false);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    this._enabedControl(_g.d.ic_trans._advance_amount, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                       MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                       MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                       MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);

                    }
                    else
                    {
                        this._addTextBox(row, 0, 3, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    }

                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);

                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);

                    this._addCheckBox(row, 1, _g.d.ic_trans._total_manual, true, false);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    this._enabedControl(_g.d.ic_trans._advance_amount, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._expire_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._expire_date, 1, true, true);

                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._send_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._send_date, 1, true, true);

                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    }
                    else
                    {
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    }

                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    this._enabedControl(_g.d.ic_trans._total_value, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._total_before_vat, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                    this._enabedControl(_g.d.ic_trans._total_after_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_except_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ic_trans._send_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(1, 1, 1, 0, _g.d.ic_trans._send_date, 1, true, true);

                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    this._addNumberBox(2, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);

                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._total_value, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._total_before_vat, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                    this._enabedControl(_g.d.ic_trans._total_after_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_except_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    if (this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก)
                    {
                        this.Enabled = false;
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._expire_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._expire_date, 1, true, true);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                    this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._send_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._send_date, 1, true, true);
                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    }
                    else
                    {
                        this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    }

                    this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    this._enabedControl(_g.d.ic_trans._total_value, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._total_before_vat, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                    this._enabedControl(_g.d.ic_trans._total_after_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_except_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    //
                    if (this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ || this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก)
                    {
                        this.Enabled = false;
                    }
                    break;
                #endregion

                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    this._addNumberBox(4, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._advance_amount, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    this._addNumberBox(4, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._advance_amount, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    //if (_g.g._companyProfile._manual_total_enable) โต๋ กรณ่ีเจ้าหนี้ สามารถแก้ไขได้
                    {
                        this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    }
                    this._addNumberBox(4, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._advance_amount, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    // if (_g.g._companyProfile._manual_total_enable)  โต๋ กรณ่ีเจ้าหนี้ สามารถแก้ไขได้
                    {
                        this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    }
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    //if (_g.g._companyProfile._manual_total_enable)  โต๋ กรณ่ีเจ้าหนี้ สามารถแก้ไขได้
                    {
                        this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    }
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    if (_g.g._companyProfile._manual_total_enable)
                    {
                        this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    }
                    this._addNumberBox(4, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._advance_amount, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    if (_g.g._companyProfile._manual_total_enable)
                    {
                        this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    }
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    if (_g.g._companyProfile._manual_total_enable)
                    {
                        this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    }
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    break;
                case _g.g._transControlTypeEnum.SoInquiry:
                    this._maxColumn = 1;
                    this._table_name = _g.d.ic_trans._table;
                    this._addTextBox(0, 0, 3, 0, _g.d.ic_trans._remark, 1, 1, 0, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.SoEstimate:
                    this._maxColumn = 3;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 3, true, false, false);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 1, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    break;
                #region ขาย


                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._expire_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._expire_date, 1, true, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(1, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(2, 0, 1, 0, _g.d.ic_trans._send_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._send_date, 1, true, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (_g.g._companyProfile._multi_currency)
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                    }
                    if (_g.g._companyProfile._manual_total_enable)
                    {
                        this._addCheckBox(4, 2, _g.d.ic_trans._total_manual, true, false);
                    }
                    //
                    if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก)
                    {
                        this.Enabled = false;
                    }
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);

                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._expire_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._expire_date, 1, true, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(1, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(2, 0, 1, 0, _g.d.ic_trans._send_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._send_date, 1, true, true);

                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);

                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    if (_g.g._companyProfile._multi_currency)
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                    }
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._total_value, false);
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._total_before_vat, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                    this._enabedControl(_g.d.ic_trans._total_after_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_except_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    //
                    if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก)
                    {
                        this.Enabled = false;
                    }
                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }
                    break;

                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._expire_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._expire_date, 1, true, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(1, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                    this._addNumberBox(2, 0, 1, 0, _g.d.ic_trans._send_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._send_date, 1, true, true);

                    if (_g.g._companyProfile._multi_currency)
                    {
                        this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true, true);
                    }
                    else
                    {
                        this._addTextBox(3, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true, true);
                    }

                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (_g.g._companyProfile._manual_total_enable)
                    {
                        this._addCheckBox(4, 2, _g.d.ic_trans._total_manual, true, false);
                    }
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);

                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    {
                        row = 0;
                        this._maxColumn = 4;
                        this._table_name = _g.d.ic_trans._table;
                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                        this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);

                        this._addNumberBox(row, 0, 1, 0, _g.d.ic_trans._send_day, 1, 2, true, __formatNumberNone);
                        this._addDateBox(row, 1, 1, 0, _g.d.ic_trans._send_date, 1, true, true);
                        this._addTextBox(row, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                        this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                            this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                            this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);
                            this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);

                            this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                            this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                            this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);

                        }
                        else
                        {
                            this._addTextBox(row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
                            {
                                this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);
                            }
                            this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);

                            this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                            this._addNumberBox(row++, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);

                        }


                        this._addNumberBox(row, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                        this._addNumberBox(row, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                        if (_g.g._companyProfile._manual_total_enable)
                        {
                            this._addCheckBox(row, 1, _g.d.ic_trans._total_manual, true, false);
                        }
                        //
                        this._enabedControl(_g.d.ic_trans._total_discount, false);
                        this._enabedControl(_g.d.ic_trans._advance_amount, false);
                        this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);

                        if ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong) || (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro))
                        {
                            this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._service_charge_word, 1, 1, 0, true, false, true);
                            this._addNumberBox(row, 1, 1, 0, _g.d.ic_trans._total_service_charge, 1, 2, true, __formatNumber);
                        }

                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                        {
                            this._addTextBox(row, 0, 1, 0, _g.d.ic_trans._sender_code, 1, 1, 1, true, false, true);
                            this._addCheckBox(++row, 0, _g.d.ic_trans._is_arm, true, false, false, false, _g.d.ic_trans._is_arm);
                        }

                        if (_g.g._companyProfile._multi_currency)
                        {
                            ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                            this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);

                    if (_g.g._companyProfile._multi_currency)
                    {
                        this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(1, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    }
                    else
                        this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);

                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    if (_g.g._companyProfile._manual_total_enable || MyLib._myGlobal._OEMVersion.Equals("imex"))
                    {
                        this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    }
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }
                    break;

                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    this._maxColumn = 4;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                    this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);

                    if (_g.g._companyProfile._multi_currency)
                    {
                        this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                        this._addNumberBox(1, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
                        this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    }
                    else
                        this._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);

                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                    this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._ref_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._ref_diff, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ic_trans._ref_new_amount, 1, 2, true, __formatNumber);
                    if (_g.g._companyProfile._manual_total_enable || MyLib._myGlobal._OEMVersion.Equals("imex"))
                    {
                        this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                    }
                    //
                    this._enabedControl(_g.d.ic_trans._total_discount, false);
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    if (_g.g._companyProfile._multi_currency)
                    {
                        ((MyLib._myNumberBox)this._getControl(_g.d.ic_trans._total_amount)).Visible = false;

                        this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount_2, 1, 2, true, __formatNumber);
                    }

                    break;

                    #endregion

            }
            //
            MyLib._myCheckBox __totalManual = (MyLib._myCheckBox)this._getControl(_g.d.ic_trans._total_manual);
            if (__totalManual != null)
            {
                __totalManual.CheckStateChanged += new EventHandler(__totalManual_CheckStateChanged);
            }
            //
            /*if (__addPayAmountField)
            {
                this._addNumberBox(5, 0, 1, 0, _g.d.ic_trans._sum_pay_money_cash, 1, 2, true, __formatNumber, true, _g.d.ic_trans._sum_pay_money_cash_out);
                this._addNumberBox(5, 1, 1, 0, _g.d.ic_trans._sum_pay_money_chq, 1, 2, true, __formatNumber, true, _g.d.ic_trans._sum_pay_money_chq_out);
                this._addNumberBox(5, 2, 1, 0, _g.d.ic_trans._sum_pay_money_transfer, 1, 2, true, __formatNumber, true, _g.d.ic_trans._sum_pay_money_transfer_out);
                this._addNumberBox(5, 3, 1, 0, _g.d.ic_trans._sum_pay_money_credit, 1, 2, true, __formatNumber, true, _g.d.ic_trans._sum_pay_money_credit_out);
                this._addNumberBox(6, 0, 1, 0, _g.d.ic_trans._sum_pay_money_diff, 1, 2, true, __formatNumber);
                this._addNumberBox(6, 1, 1, 0, _g.d.ic_trans._pay_amount, 1, 2, true, __formatNumber);
                this._enabedControl(_g.d.ic_trans._sum_pay_money_cash, false);
                this._enabedControl(_g.d.ic_trans._sum_pay_money_chq, false);
                this._enabedControl(_g.d.ic_trans._sum_pay_money_transfer, false);
                this._enabedControl(_g.d.ic_trans._sum_pay_money_credit, false);
                this._enabedControl(_g.d.ic_trans._sum_pay_money_diff, false);
                this._enabedControl(_g.d.ic_trans._pay_amount, false);
            }*/
            this._newData();
            this.Invalidate();
            this.ResumeLayout();
        }

        void __totalManual_CheckStateChanged(object sender, EventArgs e)
        {
            this._saveLastControl();
            string __manual = this._getDataStr(_g.d.ic_trans._total_manual).ToString();
            this._enabedControl(_g.d.ic_trans._vat_rate, (__manual.Equals("1")) ? true : false);
            this._calc(this._itemGrid);
            this.Invalidate();
        }
    }
}
