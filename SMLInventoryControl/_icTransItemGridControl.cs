using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SMLInventoryControl
{
    public class _icTransItemGridControl : MyLib._myGrid
    {
        public delegate _g.g._vatTypeEnum VatTypeEventHandler(object sender);
        public delegate string DocDateEventHandler(object sender);
        public delegate decimal VatRateEventHandler();
        public delegate Boolean RecheckCountEventHandler();
        public delegate decimal RecheckCountDayEventHandler();
        public delegate void RecalcEventHandler();
        public delegate int VatTypeNumberHandler();
        public delegate void AfterProcessEventHandler(string discountWord, decimal discountAmount);
        public delegate string DocNoHandler();
        public delegate string DocNoOldHandler();
        public delegate void ItemInfoHandler(string itemCode);
        public delegate void LotHandler(bool loadLotControl);
        public delegate void ItemReplacementHandler(string itemCode);
        public delegate void SetRemarkEventHandler(string fieldName, string remark);
        public delegate decimal ExchangeRateGetEventHandler();
        public delegate string BranchCodeHandler();
        //
        public event VatTypeEventHandler _vatType;
        public event DocDateEventHandler _getDocDate;
        public event VatRateEventHandler _vatRate;
        public event RecalcEventHandler _reCalc;
        public event AfterProcessEventHandler _afterProcess;
        public event VatTypeNumberHandler _vatTypeNumber;
        public event RecheckCountEventHandler _recheckCount;
        public event RecheckCountDayEventHandler _recheckCountDay;
        public event DocNoHandler _docNo;
        public event DocNoOldHandler _docNoOld;
        public event ItemInfoHandler _itemInfo;
        public event LotHandler _lot;
        public event ItemReplacementHandler _itemReplacement;
        public event ChangeCreditDayEventHandler _changeCreditDay;
        public event SetRemarkEventHandler _setRemark;
        public event ExchangeRateGetEventHandler _exchangeRateGet;
        public event BranchCodeHandler _getBranchCode;
        /// <summary>
        /// กรณีป้อนจำนวนพร้อมราคา (มูลค่า/จำนวน) แล้วโปรแกรมจะกระจายไปสองช่องให้เอง (Qty,Price)
        /// </summary>
        public Boolean _checkAutoQty = true;
        /// <summary>
        /// กำหนดให้ดึง 2 บรรทัดแรก กรณีแม่สี (=5)
        /// </summary>
        public Boolean _fixedItemSetRow = true;
        // ให้แสดงจำนวนแม่สีหรือไม่
        public Boolean _colorQtyShow = true;

        //#grid

        private _g.g._transControlTypeEnum _ictransControlTemp;
        public _icTransSerialNumberDockForm _serialNumberForm = null;
        private MyLib._searchDataFull _searchMaster;
        public SMLERPControl._searchItemForm _searchItem;
        public MyLib._searchDataFull _searchBarcode;
        private MyLib._myFrameWork _myFrameWork;
        private SMLERPGlobal._searchProperties __searchProperties;
        private ArrayList __searchMasterList;
        private ArrayList _priorityLevelArrayList;
        private _icTransItemGridChangeAmountForm _icTransItemGridChangeAmount;
        private _icTransItemGridChangeDiscountForm _icTransItemGridChangeDiscount;
        private _icTransItemGridChangePriceForm _icTransItemGridChangePrice;
        private _icTransItemGridChangeQtyForm _icTransItemGridChangeQty;
        private _icTransItemGridChangeNameForm _icTransItemGridChangeName;
        private _icTransItemGridChangeNameForm _icTransItemGridChangeRemark;
        private SMLERPControl._ic._icTransItemGridSelectUnitForm _icTransItemGridSelectUnit;
        private _icTransItemGridSelectWareHouseAndShelfForm _icTransItemGridSelectWareHouse;

        private string _columnCustName = _g.d.ic_trans._cust_name;
        //
        private string _columnUnitName = _g.d.ic_trans_detail._unit_name;
        private string _columnUnitType = _g.d.ic_trans_detail._unit_type;
        public string _columnSerialNumber = _g.d.ic_trans_detail._serial_number;
        public string _columnSerialNumberCount = "serial_number_count";
        public string _columnCostType = "cost_type";
        private string _columnPriceRoworder = "price_roworder";
        public string _columnAverageCostUnitStand = "average_cost_stand";
        public string _columnAverageCostUnitDiv = "average_cost_div";

        public string _columnDepartment = _g.d.ic_trans_detail._department;
        public string _columnProject = _g.d.ic_trans_detail._project;
        public string _columnAlloCate = _g.d.ic_trans_detail._allocate;
        public string _columnSideList = "side_list";
        public string _columnJobsList = "jobs_list";

        /// <summary>
        /// ผู้อนุมัติราคาล่าสุด
        /// </summary>
        public string _lastApprovePrice = "";
        /// <summary>
        /// สินค้าดึง Remark มาประกอบชื่อสินค้าหรือไม่
        /// </summary>
        private string _columnIsGetItemRemark = "item_remark_replace";

        private object[] _priorityLevel = new object[] { _g.d.ic_trans_detail._priority_level_1, _g.d.ic_trans_detail._priority_level_2, _g.d.ic_trans_detail._priority_level_3, _g.d.ic_trans_detail._priority_level_4 };
        // อ้าง Grid บิลต่างๆ
        private _icTransRefControl _icTransRefTemp;
        private string _custCodeTemp = "";
        public _icTransScreenTopControl _icTransScreenTop;
        public Boolean _isSerialNumber = false;
        //

        // toe weight scale
        string _weight_scale_prefix = "";
        int _weight_prefix_start;
        int _weight_prefix_stop;
        int _weight_ic_code_start;
        int _weight_ic_code_stop;
        int _weight_price_start;
        int _weight_price_stop;

        private _search_chq_form _search_chq_form;

        public string _custCode
        {
            set
            {
                this._custCodeTemp = value;
            }
            get
            {
                return this._custCodeTemp;
            }
        }

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                this._ictransControlTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._ictransControlTemp;
            }
        }

        public _icTransRefControl _icTransRef
        {
            set
            {
                this._icTransRefTemp = value;
                if (this._icTransRefTemp != null)
                {
                    this._icTransRefTemp._processButton -= new _icTransRefControl.ProcessButtonEventHandler(_icTransRefControlTemp__processButton);
                    this._icTransRefTemp._processButton += new _icTransRefControl.ProcessButtonEventHandler(_icTransRefControlTemp__processButton);
                    this._icTransRefTemp._refCheck.Click -= new EventHandler(_refCheck_Click);
                    this._icTransRefTemp._refCheck.Click += new EventHandler(_refCheck_Click);
                    this._icTransRefTemp._vatTypeNumber -= new _icTransRefControl.VatTypeNumberHandler(_icTransRefTemp__vatTypeNumber);
                    this._icTransRefTemp._vatTypeNumber += new _icTransRefControl.VatTypeNumberHandler(_icTransRefTemp__vatTypeNumber);
                    this._icTransRefTemp._mapRefLineNumberButton.Click -= new EventHandler(_mapRefLineNumberButton_Click);
                    this._icTransRefTemp._mapRefLineNumberButton.Click += new EventHandler(_mapRefLineNumberButton_Click);
                }
            }
            get
            {
                return this._icTransRefTemp;
            }
        }

        void _mapRefLineNumberButton_Click(object sender, EventArgs e)
        {
            // on map
            _mapLineNumberRefer();
        }

        public void _mapLineNumberRefer()
        {
            int __columnRefLineNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_row);
            if (__columnRefLineNumber != -1)
            {
                MyLib._myGrid._columnType __checkColumn = (MyLib._myGrid._columnType)this._columnList[__columnRefLineNumber];
                __checkColumn._isHide = false;
                __checkColumn._widthPercent = 10;
                this._columnList[__columnRefLineNumber] = (MyLib._myGrid._columnType)__checkColumn;

                this._calcPersentWidthToScatter();
                this._recalcColumnWidth(true);
                this.Invalidate();

                // process map
                StringBuilder __getRefDocNo = new StringBuilder();

                // for ref
                for (int __row = 0; __row < this._icTransRefTemp._transGrid._rowData.Count; __row++)
                {
                    string __getDocNo = this._icTransRefTemp._transGrid._cellGet(__row, _g.d.ap_ar_trans_detail._billing_no).ToString();

                    if (__getDocNo.Length > 0)
                    {

                        if (__getRefDocNo.Length > 0)
                        {
                            __getRefDocNo.Append(",");
                        }

                        __getRefDocNo.Append("\'" + __getDocNo + "\'"); // " 'X01-POV5908-00356' ";
                    }
                }


                String __queryGetItemRef = "select doc_no, item_code, line_number, qty, unit_code, price, discount, sum_amount from ic_trans_detail where doc_no in (" + __getRefDocNo + ") ";
                DataTable __refTable = _myFrameWork._queryShort(__queryGetItemRef).Tables[0];

                // start map row
                for (int __row = 0; __row < this._rowData.Count; __row++)
                {
                    string __itemCode = this._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();

                    DataRow[] __getRow = __refTable.Select(" item_code = \'" + __itemCode + "\'");
                    if (__getRow.Length > 0)
                    {
                        if (__getRow.Length > 1)
                        {
                            this._cellUpdate(__row, _g.d.ic_trans_detail._ref_row, -1, false);
                        }
                        else
                        {
                            this._cellUpdate(__row, _g.d.ic_trans_detail._ref_row, MyLib._myGlobal._intPhase(__getRow[0][_g.d.ic_trans_detail._line_number].ToString()), false);
                        }
                    }
                }
            }
        }

        int _icTransRefTemp__vatTypeNumber()
        {
            return this._vatTypeNumber();
        }

        void _refCheck_Click(object sender, EventArgs e)
        {
            this._icTransRefTemp.Focus();
            this._icTransRefTemp._refCheckStatus = (this._icTransRefTemp._refCheckStatus == false) ? true : false;
            _visableRefColumn(this._icTransRefTemp._refCheckStatus);
        }

        public int _countByItem()
        {
            int __result = 0;
            for (int __row = 0; __row < this._rowData.Count; __row++)
            {
                string __itemCode = this._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString().Trim();
                if (__itemCode.Length > 0)
                {
                    __result++;
                }
            }
            return __result;
        }

        decimal _getExchangeRate()
        {
            decimal __result = 0M;

            if (this._exchangeRateGet != null)
                __result = this._exchangeRateGet();

            return __result;
        }

        private int _buildCount = 0;

        private void _build()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (this._icTransControlType == _g.g._transControlTypeEnum.ว่าง)
                {
                    return;
                }
                this._buildCount++;
                if (this._buildCount > 1)
                {
                    MessageBox.Show("Trans Grid : Build Duplicate");
                }
                string __gridMessage1 = MyLib._myGlobal._resource("CTRL+1=ประวัติการซื้อ");
                string __gridMessage2 = MyLib._myGlobal._resource("CTRL+1=ประวัติการขาย");
                string __gridMessage3 = MyLib._myGlobal._resource("CTRL+3=ตารางราคา") + "," + MyLib._myGlobal._resource("CTRL+4=ราคาขาย");
                string __gridMessage4 = MyLib._myGlobal._resource("F7=Serial Number");
                string __gridMessage9 = MyLib._myGlobal._resource("CTRL+2=แสดงปริมาณคงเหลือ");
                string __gridMessage10 = MyLib._myGlobal._resource("CTRL+6=แสดงสินค้าทดแทน");
                string __gridMessageLot = MyLib._myGlobal._resource("CTRL+8=Lot");
                if (this._icTransControlType != _g.g._transControlTypeEnum.ว่าง)
                {
                    this._columnList.Clear();
                    string __formatNumberQty = _g.g._getFormatNumberStr(1);
                    string __formatNumberPrice = _g.g._getFormatNumberStr(2);
                    string __formatNumberAmount = _g.g._getFormatNumberStr(3);
                    this._width_by_persent = true;
                    //
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            this._message = __gridMessage2 + "," + __gridMessage9 + "," + __gridMessage3 + "," + __gridMessage10;
                            if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้ ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้)
                            {
                                this._isSerialNumber = true;
                                //this._message = this._message;
                            }
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                            this._message = __gridMessage1 + "," + __gridMessage9 + ((MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLPOSLite && MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLTomYumGoongPro) ? "," + __gridMessageLot : "");
                            if (this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด)
                            {
                                this._isSerialNumber = true;
                                // this._message = this._message;
                            }
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา_ยกเลิก:
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                            this._message = __gridMessage9;
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                        case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:

                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด: // toe

                        case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                        case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                        case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                        case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                        case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                        case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                        case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                        case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                        case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                            this._isSerialNumber = true;
                            this._message = __gridMessage9;
                            break;
                    }
                    if (this._isSerialNumber == true)
                    {
                        this._message = this._message + "," + __gridMessage4;
                    }

                    #region Ref Doc No

                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            break;
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, ((MyLib._myGlobal._OEMVersion.Equals("SINGHA")) ? false : true), true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref_sale_order);
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_สูตรสี:
                            __formatNumberQty = _g.g._getFormatNumberStr(14);
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_สินค้าจัดชุด:
                            __formatNumberQty = _g.g._getFormatNumberStr(1);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref_purchase_order);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref_partial);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref_purchase_order);
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, false, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref_requisition);
                            break;
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, false, true, true);
                            break;
                        case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, false, true, true);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, false, true, true);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, false, true, true);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);
                            break;
                        case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, false, true, true);
                            this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_ictransItemGridControl__cellComboBoxGet);
                            this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_ictransItemGridControl__cellComboBoxItem);

                            break;
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, false, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref_requisition);
                            break;

                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, false, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref_partial);
                            break;
                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                            this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref_partial);
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            //  this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, false, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            break;
                    }

                    // ย้ายมาจากท้ายตาราง เพื่อให้แสดงได้ว่า อ้างอิง line ไหน
                    this._addColumn(_g.d.ic_trans_detail._ref_row, 2, 1, 5, false, true, true);

                    #endregion
                    Boolean __editItemCode = true;
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                            __editItemCode = false;
                            break;
                    }

                    #region Item Code & Item Name

                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._book_bank_code);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._chq_number, 1, 1, 25, true, false, true, true, "", "", "", _g.d.ic_trans_detail._credit_card_no);
                            this._addColumn(_g.d.ic_trans_detail._doc_ref, 1, 1, 15, false, false, true, false);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, true, true, true, "", "", "", _g.d.ic_trans_detail._book_bank_code);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, false, true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, false, true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._chq_number, 1, 1, 25, true, false, true, true, "", "", "", _g.d.ic_trans_detail._credit_card_no);
                            this._addColumn(_g.d.ic_trans_detail._doc_ref, 1, 1, 15, false, false, true, false);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, true, true, true, "", "", "", _g.d.ic_trans_detail._book_bank_code);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, false, true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, false, true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._chq_number, 1, 1, 25, true, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._doc_ref, 1, 1, 15, false, false, true, false);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, false, "", "", "", _g.d.ic_trans_detail._chq_number);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, true, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, true, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._date_due, 4, 1, 25, true, false, true);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._book_bank_code);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._chq_number, 1, 1, 25, true, false, true, true);

                            if (this._icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา || this._icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_due, 4, 1, 25, true, false, true);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_due, 4, 1, 25, false, false, true);
                            }
                            this._addColumn(_g.d.ic_trans_detail._doc_ref, 1, 1, 15, false, false, true, false);

                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._chq_number);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._date_due, 4, 1, 25, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._doc_ref, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._chq_number, 1, 1, 25, true, true, true, true);

                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._book_bank_code);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._chq_number, 1, 1, 25, true, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._doc_ref, 1, 1, 15, false, false, true, false);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._book_bank_code);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._chq_number, 1, 1, 15, true, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._date_due, 4, 1, 25, true, false, true);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._book_bank_code);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._chq_number, 1, 1, 15, true, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._date_due, 4, 1, 25, true, false, true);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._book_bank_code_out);
                            this._addColumn(_g.d.ic_trans_detail._bank_name, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._item_code_2, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._book_bank_code_in);
                            this._addColumn(_g.d.ic_trans_detail._bank_name_2, 1, 1, 15, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._bank_branch_2, 1, 1, 15, false, false, true, false);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._cash_sub_code);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 25, false, false, true, false, "", "", "", _g.d.ic_trans_detail._cash_sub_name);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true, "", "", "", _g.d.ic_trans_detail._cash_sub_code);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 25, false, false, true, false, "", "", "", _g.d.ic_trans_detail._cash_sub_name);
                            break;
                        case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, __editItemCode, false, true, true, "", "", "", _g.d.ic_trans_detail._expense_code);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 25, false, false, true, false, "", "", "", _g.d.ic_trans_detail._expense_name);

                            // singha 
                            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && _g.g._companyProfile._branchStatus == 1 &&
                                (this._icTransControlType == _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น || this._icTransControlType == _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น || this._icTransControlType == _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น || this._icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น))
                            {
                                this._addColumn(_g.d.ic_trans_detail._branch_code, 1, 1, 15, true, false, true, true, "", "", "");
                            }
                            break;
                        case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                        case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                        case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, __editItemCode, false, true, true, "", "", "", _g.d.ic_trans_detail._income_code);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 25, false, false, true, false, "", "", "", _g.d.ic_trans_detail._income_name);
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 10, true, false, true, true, "", "", "", _g.d.ic_trans_detail._doc_no);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 15, false, false, true);
                            this._addColumn(_g.d.ic_trans._cust_name, 1, 10, 10, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_date, 4, 1, 15, false, false, true, false, "dd/MM/yyyy", "", "", _g.d.ic_trans_detail._doc_date);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 20, false, false, true, false, __formatNumberAmount);

                            break;
                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 20, false, false, true, false);

                            break;
                        default:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 20, false, false, true, false);
                            break;
                    }

                    #endregion
                    //
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, false, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._amount2);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 45, true, false, true, false, "", "", "", _g.d.ic_trans_detail._detail);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._amount2);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 45, true, false, true, false, "", "", "", _g.d.ic_trans_detail._detail);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 15, false, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._sum_amount_exclude_vat);
                            this._addColumn(_g.d.ic_trans_detail._sum_of_cost, 3, 1, 15, true, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._expense);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, false, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._amount2);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 45, true, false, true, false, "", "", "", _g.d.ic_trans_detail._detail);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, false, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._amount2);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 45, true, false, true, false, "", "", "", _g.d.ic_trans_detail._detail);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._amount2);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 45, true, false, true, false, "", "", "", _g.d.ic_trans_detail._detail);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._amount2);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 30, true, false, true, false, "", "", "", _g.d.ic_trans_detail._detail);
                            this._addColumn(_g.d.ic_trans_detail._transfer_amount, 3, 1, 10, true, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._fee_amount, 3, 1, 10, true, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, false, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._sum_amount);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 45, true, false, true, false, "", "", "", _g.d.ic_trans_detail._detail);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._amount2);
                            break;
                        case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 45, true, false, true, false, "", "", "", _g.d.ic_trans_detail._detail);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._amount2);
                            break;
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
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 45, true, false, true, true, "", "", "", _g.d.ic_trans_detail._detail);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, true, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._amount2);
                            break;

                        #region ขายสินค้า
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, false, __formatNumberQty);

                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 15, ((MyLib._myGlobal._programName.Equals("SML CM")) ? false : true), false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);
                            if (_g.g._companyProfile._ic_price_formula_control)
                            {
                                this._addColumn(_g.d.ic_inventory_price_formula._price_0, 3, 1, 0, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            if (_g.g._companyProfile._stock_reserved_control_location)
                            {
                                this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                                this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            }
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, false, __formatNumberQty);

                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10, false, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);
                            if (_g.g._companyProfile._ic_price_formula_control)
                            {
                                this._addColumn(_g.d.ic_inventory_price_formula._price_0, 3, 1, 0, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, false, false, true, true, __formatNumberQty, "", "", _g.d.ic_trans_detail._po_qty);

                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, _g.g._companyProfile._column_price_enable, false, true, true, __formatNumberQty);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, false, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }

                            this._isEdit = false;
                            break;


                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_trans_detail._cancel_qty);
                            break;
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            {
                                this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                                this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                                this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                                this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);

                                if (_g.g._companyProfile._multi_currency == true)
                                {
                                    this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                    this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                    this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                    this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                    this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                                }
                                else
                                {
                                    this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
                                    this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, ((MyLib._myGlobal._programName.Equals("SML CM")) ? false : true), false, true);
                                    this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, true, false, true, false, __formatNumberAmount);
                                }
                                this._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
                                this._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
                                this._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);
                                if (_g.g._companyProfile._ic_price_formula_control)
                                {
                                    this._addColumn(_g.d.ic_inventory_price_formula._price_0, 3, 1, 0, false);
                                }
                            }
                            break;
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, false, false, true, true, __formatNumberQty, "", "", _g.d.ic_trans_detail._po_qty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, false, false, true, true, __formatNumberQty);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, false, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._isEdit = false;
                            break;
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_trans_detail._cancel_qty);
                            break;
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, false, false, true, true, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, false, false, true, true, __formatNumberQty);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, false, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._isEdit = false;
                            break;
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, false, false, true, true, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {

                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, false, false, true, true, __formatNumberQty);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, false, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._isEdit = false;
                            break;
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);

                            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
                            {
                                this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            }
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);

                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {

                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree) ? true : _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, false, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }

                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_income_1) ? true : false, (_g.g._companyProfile._hidden_income_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_2);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);
                            if (_g.g._companyProfile._ic_price_formula_control)
                            {
                                this._addColumn(_g.d.ic_inventory_price_formula._price_0, 3, 1, 0, false);
                            }
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, false, true, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_income_1) ? true : false, (_g.g._companyProfile._hidden_income_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_2);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 8, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 8, false, true, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 8, false, true, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);
                            if (_g.g._companyProfile._ic_price_formula_control)
                            {
                                this._addColumn(_g.d.ic_inventory_price_formula._price_0, 3, 1, 0, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                            {
                                this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                                this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                                this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                                this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                                // toe //this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
                                if (_g.g._companyProfile._multi_currency == true)
                                {
                                    this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                    this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                    this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                    this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                    this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                                }
                                else
                                {
                                    this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                                    this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                    this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                }
                                this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                                if (_g.g._companyProfile._use_expire)
                                {
                                    this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                                }
                                this._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
                                this._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
                                this._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
                            }
                            break;
                        #endregion

                        case _g.g._transControlTypeEnum.สินค้า_เงื่อนไขแถมตอนซื้อ:
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, false, __formatNumberQty);
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_สินค้าแถมตอนซื้อ:
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, false, __formatNumberQty);
                            break;

                        #region ซื้อ
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, true, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._due_date, 4, 1, 10, true, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 15, true, false, true);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, false, false, true, true, __formatNumberPrice);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, false, false, true, true, __formatNumberPrice);
                            this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, false, false, true);
                            this._addColumn(_g.d.ic_trans_detail._due_date, 4, 1, 10, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            this._isEdit = false;
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._temp_float_1, 3, 1, 10, false, false, true, true, __formatNumberQty, "", "", _g.d.ic_trans_detail._qty);
                            this._addColumn(_g.d.ic_trans_detail._temp_float_2, 3, 1, 10, false, false, true, true, __formatNumberQty, "", "", _g.d.ic_trans_detail._price);
                            this._addColumn(_g.d.ic_trans_detail._temp_string_1, 1, 1, 8, false, false, true, false, "", "", "", _g.d.ic_trans_detail._discount);
                            this._addColumn(_g.d.ic_trans_detail._due_date, 4, 1, 10, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, true, __formatNumberPrice, "", "", _g.d.ic_trans_detail._approval_qty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, false, true, true, __formatNumberPrice, "", "", _g.d.ic_trans_detail._approval_price);
                            this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            // Ref
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, false, true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._ref_line_number, 2, 1, 10, false, true, true, false);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_trans_detail._po_qty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_trans_detail._cancel_qty);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, false, false, true, true, __formatNumberQty, "", "", _g.d.ic_trans_detail._po_qty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, false, false, true, true, __formatNumberQty);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, false, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._isEdit = false;
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_income_1) ? true : false, (_g.g._companyProfile._hidden_income_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_2);
                            break;
                        #region พาเชียล
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._multi_currency == true)
                            {
                                this._addColumn(_g.d.ic_trans_detail._price_2, 3, 1, 10, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, true, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount_2, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, true, true, false, __formatNumberAmount);
                            }
                            else
                            {
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, true, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            }
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_income_1) ? true : false, (_g.g._companyProfile._hidden_income_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_2);
                            break;
                        #endregion
                        #endregion
                        case _g.g._transControlTypeEnum.สินค้า_สูตรสี:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_สินค้าจัดชุด:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            break;


                        case _g.g._transControlTypeEnum.SoEstimate:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 10);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 20);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, false, __formatNumberQty);
                            this._addColumn(_columnUnitName, 1, 1, 0, true, true, false, false);
                            this._addColumn(_columnUnitType, 2, 1, 0, true, true, false, false);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, false, false, true, false, __formatNumberPrice);
                            //this._addColumn(_g.d.ic_trans_detail._extra_str_1, 1, 1, 10);
                            //this._addColumn(_g.d.ic_trans_detail._extra_float_1, 3, 1, 10);
                            this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 20, false, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_cust_code, 1, 1, 15, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._ref_doc_date, 4, 1, 15, false, true, true);
                            //this._addColumn(_g.d.ic_trans_detail._ref_line_guid, 1, 1, 20, false, true, true);
                            //this._addColumn(_g.d.ic_trans_detail._line_guid, 1, 1, 20, false, true, true);
                            break;
                        case _g.g._transControlTypeEnum.SoInquiry:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 20, true, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 40, false, false, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 20, false, false, true);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 20, true, false, true, false, __formatNumberQty);
                            this._addColumn(_columnUnitName, 1, 1, 0, true, true, false, false);
                            this._addColumn(_columnUnitType, 2, 1, 0, true, true, false, false);
                            break;
                        case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, false, false, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._sum_of_cost, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._profit, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 20, false, false, true);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);
                            break;

                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด: // toe
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice, "", "", _g.d.ic_trans_detail._cost);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, true, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                            // ยอดสินค้าคงเหลือยกมา (ยอดยกมา)
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice, "", "", _g.d.ic_trans_detail._cost);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, true, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            //
                            break;
                        case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, true, false, true, false, __formatNumberAmount);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            break;
                        case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;


                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            break;

                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._total_qty, 3, 1, 8, false, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            break;

                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._total_qty, 3, 1, 8, false, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            break;

                        //case _g.g._ictransControlTypeEnum.StockBuild:
                        //    this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 20, true, false, true, true);
                        //    this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 20, true, false, true);
                        //    this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 20, true, false, true);
                        //    this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 20, true, false, true);
                        //    this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 20, true, false, true, false, __formatNumber);
                        //    this._addColumn(_g.d.ic_trans_detail._row_number, 3, 1, 20, false, true, true);
                        //    break;
                        case _g.g._transControlTypeEnum.StockCheck:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 20, true, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 20, true, false, true);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 20, true, false, true);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 20, true, false, true);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 20, true, false, true, false, __formatNumberQty);
                            break;
                        case _g.g._transControlTypeEnum.StockCheckResult:
                            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 10, true, false, true, true);
                            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 20, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._total_qty, 3, 1, 10, false, false, true, false, __formatNumberPrice);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_trans_detail._check_stock_1); // เอา Qty มาทำเป็นยอดที่ตรวจนับได้
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._check_stock_2); // เอา _sum_amount มาทำเป็นผลต่างการตรวจนับ
                            this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 10, false, false, true, true);
                            //
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice, "", "", _g.d.ic_trans_detail._cost);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, true, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            // เบิกสินค้า
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                            // รับคืนจากการเบิก
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, true, false, true, false, __formatNumberPrice, "", "", _g.d.ic_trans_detail._cost);
                            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, true, false, true, false, __formatNumberAmount);
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;

                        case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                            {
                                this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                                this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            }
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false, "", "", "", _g.d.ic_trans_detail._wh_name_out);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false, "", "", "", _g.d.ic_trans_detail._shelf_name_out);
                            this._addColumn(_g.d.ic_trans_detail._wh_code_2, 1, 1, 10, false, false, true, false, "", "", "", _g.d.ic_trans_detail._wh_name_in);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code_2, 1, 1, 10, false, false, true, false, "", "", "", _g.d.ic_trans_detail._shelf_name_in);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._columnExtraWord(_g.d.ic_trans_detail._wh_code_2, "(F6)");
                            this._columnExtraWord(_g.d.ic_trans_detail._shelf_code_2, "(F6)");
                            this._addColumn(_g.d.ic_trans_detail._wh_name_2, 1, 1, 0, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_name_2, 1, 1, 0, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_cost_1) ? true : false, (_g.g._companyProfile._hidden_cost_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_1);
                            if (_g.g._companyProfile._use_expire)
                            {
                                this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, true, false, true, false);
                            }
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                            break;
                        case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                            this._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
                            this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false, "", "", "", _g.d.ic_trans_detail._wh_name_out);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false, "", "", "", _g.d.ic_trans_detail._shelf_name_out);
                            this._addColumn(_g.d.ic_trans_detail._wh_code_2, 1, 1, 10, false, false, true, false, "", "", "", _g.d.ic_trans_detail._wh_name_in);
                            this._addColumn(_g.d.ic_trans_detail._shelf_code_2, 1, 1, 10, false, false, true, false, "", "", "", _g.d.ic_trans_detail._shelf_name_in);
                            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                            this._columnExtraWord(_g.d.ic_trans_detail._wh_code_2, "(F6)");
                            this._columnExtraWord(_g.d.ic_trans_detail._shelf_code_2, "(F6)");
                            this._addColumn(_g.d.ic_trans_detail._wh_name_2, 1, 1, 0, false, true, false);
                            this._addColumn(_g.d.ic_trans_detail._shelf_name_2, 1, 1, 0, false, true, false);
                            break;

                    }
                    // Add roworder
                    //this._addColumn(_g.d.ic_trans_detail._row_number, 3, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._extra, 12, 1, 5, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._dimension, 12, 1, 5, false, true, false);
                    // Field ซ่อน
                    this._addColumn(_g.d.ic_trans_detail._unit_name, 1, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._wh_name, 1, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._shelf_name, 1, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._wh_name_out, 1, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._shelf_name_out, 1, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._unit_type, 2, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._stand_value, 3, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._divide_value, 3, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._item_type, 2, 1, 10, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._item_code_main, 1, 1, 10, false, true, true);
                    //this._addColumn(_g.d.ic_trans_detail._ref_row, 2, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._ref_guid, 1, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._is_permium, 2, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._is_get_price, 2, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._average_cost, 3, 1, 0, false, true, false, false, __formatNumberPrice);
                    this._addColumn(_g.d.ic_trans_detail._price_exclude_vat, 3, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._total_vat_value, 3, 1, 15, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._sum_amount_exclude_vat, 3, 1, 0, false, true, true, false, __formatNumberAmount);
                    this._addColumn(_g.d.ic_trans_detail._hidden_cost_1_exclude_vat, 3, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._discount_amount, 3, 1, 0, false, true, true);
                    //this._addColumn(_g.d.ic_trans_detail._doc_date_calc, 4, 1, 0, false, true, true);
                    //this._addColumn(_g.d.ic_trans_detail._doc_time_calc, 1, 1, 0, false, true, true);
                    this._addColumn(this._columnAverageCostUnitStand, 3, 1, 0, false, true, false);
                    this._addColumn(this._columnAverageCostUnitDiv, 3, 1, 0, false, true, false);
                    this._addColumn(this._columnPriceRoworder, 2, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._user_approve, 1, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._price_mode, 1, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._price_type, 1, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._is_serial_number, 2, 1, 0, false, true, true);
                    this._addColumn(_g.d.ic_trans_detail._tax_type, 2, 1, 0, false, true, true);
                    this._addColumn(this._columnSerialNumber, 12, 1, 0, false, true, false);
                    this._addColumn(this._columnSerialNumberCount, 3, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._lot_number_1, 1, 1, 0, false, true, true);
                    this._addColumn(this._columnCostType, 2, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._use_expire, 2, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._price_set_ratio, 3, 1, 0, false, true, true);
                    this._addColumn(this._columnIsGetItemRemark, 2, 1, 0, false, true, false);
                    this._addColumn(_g.d.ic_trans_detail._price_guid, 1, 1, 0, false, true, true);

                    // toe for imex
                    // งง ว่าทำไมเอาเฉพาะ จอ && (this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ || this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า || this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า)
                    //if ((MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims"))
                    //{
                    this._addColumn(_g.d.ic_trans_detail._mfd_date, 4, 1, 8, true, true, true, false);
                    this._addColumn(_g.d.ic_trans_detail._mfn_name, 1, 1, 0, false, true, true);

                    //}

                    if (this._findColumnByName(_g.d.ic_trans_detail._remark) == -1)
                    {
                        this._addColumn(_g.d.ic_trans_detail._remark, 1, 1, 0, true, true, true);
                    }

                    // สาขา weight
                    if (_g.g._companyProfile._use_department == 1)
                    {
                        this._addColumn(this._columnDepartment, 12, 1, 0, false, true, false);
                    }

                    if (_g.g._companyProfile._use_project == 1)
                    {
                        this._addColumn(this._columnProject, 12, 1, 0, false, true, false);
                    }

                    if (_g.g._companyProfile._use_allocate == 1)
                    {
                        this._addColumn(this._columnAlloCate, 12, 1, 0, false, true, false);
                    }

                    if (_g.g._companyProfile._use_unit == 1)
                    {
                        this._addColumn(this._columnSideList, 12, 1, 0, false, true, false);
                    }

                    if (_g.g._companyProfile._use_job == 1)
                    {
                        this._addColumn(this._columnJobsList, 12, 1, 0, false, true, false);
                    }


                    // ใส่ข้อความหลังชื่อ Column
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                            break;
                        default:
                            this._columnExtraWord(_g.d.ic_trans_detail._barcode, "(F1)");
                            this._columnExtraWord(_g.d.ic_trans_detail._item_code, "(F2)");
                            this._columnExtraWord(_g.d.ic_trans_detail._item_name, "(F3)");
                            this._columnExtraWord(_g.d.ic_trans_detail._wh_code, "(F4)");
                            this._columnExtraWord(_g.d.ic_trans_detail._shelf_code, "(F4)");
                            this._columnExtraWord(_g.d.ic_trans_detail._unit_code, "(F5)");
                            break;
                    }
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                            this._columnExtraWord(_g.d.ic_trans_detail._qty, "(F10)");
                            this._columnExtraWord(_g.d.ic_trans_detail._price, "(F8)");
                            this._columnExtraWord(_g.d.ic_trans_detail._discount, "(F9)");
                            break;
                    }
                    // Auto Width
                    this._calcPersentWidthToScatter();
                    // Message
                    //this._message = "<b>F9</b>=กำหนด Lot,<b>F10</b>=รายละเอียดอื่นๆ,<b>F11</b>=กำหนดมิติ";
                    // ใส่สีตีไข่
                    int __fillBegin1 = this._findColumnByName(_g.d.ic_trans_detail._qty);
                    int __fillEnd1 = this._findColumnByName(_g.d.ic_trans_detail._sum_amount);
                    if (__fillBegin1 != -1)
                    {
                        if (__fillEnd1 == -1) __fillEnd1 = __fillBegin1;
                        for (int __loop = __fillBegin1; __loop <= __fillEnd1; __loop++)
                        {
                            this._setColumnBackground(__loop, Color.AliceBlue);
                        }
                    }
                    //
                    int __fillItemName = this._findColumnByName(_g.d.ic_trans_detail._item_name);
                    if (__fillItemName != -1)
                    {
                        this._setColumnBackground(__fillItemName, Color.AliceBlue);
                    }
                    this._afterAddRow -= new MyLib.AfterAddRowEventHandler(_ictransItemGridControl__afterAddRow);
                    this._afterAddRow += new MyLib.AfterAddRowEventHandler(_ictransItemGridControl__afterAddRow);
                    this._totalCheck -= new MyLib.TotalCheckEventHandler(_ictransItemGridControl__totalCheck);
                    this._totalCheck += new MyLib.TotalCheckEventHandler(_ictransItemGridControl__totalCheck);
                    this._beforeInputCell -= new MyLib.BeforeInputCellEventHandler(_ictransItemGridControl__beforeInputCell);
                    this._beforeInputCell += new MyLib.BeforeInputCellEventHandler(_ictransItemGridControl__beforeInputCell);
                    this._clickSearchButton -= new MyLib.SearchEventHandler(_ictransItemGridControl__clickSearchButton);
                    this._clickSearchButton += new MyLib.SearchEventHandler(_ictransItemGridControl__clickSearchButton);
                    this._alterCellUpdate -= new MyLib.AfterCellUpdateEventHandler(_ictransItemGridControl__alterCellUpdate);
                    this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_ictransItemGridControl__alterCellUpdate);
                    this._beforeDisplayRow -= new MyLib.BeforeDisplayRowEventHandler(_ictransItemGridControl__beforeDisplayRow);
                    this._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_ictransItemGridControl__beforeDisplayRow);
                    this._afterImportDataWork += _icTransItemGridControl__afterImportDataWork;
                    if (_g.g._companyProfile._digital_barcode_scale == true &&
                        (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS) || (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        )
                    {
                        // load weight digital barcode config 
                        DataTable __posConfig = _myFrameWork._queryShort("select * from " + _g.d.POSConfig._table).Tables[0];
                        if (__posConfig.Rows.Count > 0)
                        {
                            this._weight_scale_prefix = __posConfig.Rows[0][_g.d.POSConfig._weight_scale_prefix].ToString();
                            this._weight_prefix_start = (int)MyLib._myGlobal._decimalPhase(__posConfig.Rows[0][_g.d.POSConfig._weight_prefix_start].ToString());
                            this._weight_prefix_stop = (int)MyLib._myGlobal._decimalPhase(__posConfig.Rows[0][_g.d.POSConfig._weight_prefix_stop].ToString());
                            this._weight_ic_code_start = (int)MyLib._myGlobal._decimalPhase(__posConfig.Rows[0][_g.d.POSConfig._weight_ic_code_start].ToString());
                            this._weight_ic_code_stop = (int)MyLib._myGlobal._decimalPhase(__posConfig.Rows[0][_g.d.POSConfig._weight_ic_code_stop].ToString());
                            this._weight_price_start = (int)MyLib._myGlobal._decimalPhase(__posConfig.Rows[0][_g.d.POSConfig._weight_price_start].ToString());
                            this._weight_price_stop = (int)MyLib._myGlobal._decimalPhase(__posConfig.Rows[0][_g.d.POSConfig._weight_price_stop].ToString());
                        }

                    }
                }
            }
        }

        private void _icTransItemGridControl__afterImportDataWork(object sender)
        {
            this._searchUnitNameWareHouseNameShelfNameAll();
        }

        /// <summary>
        /// แสดงช่องอ้างอิงในกลิด
        /// </summary>
        /// <param name="value">true=แสดง,false=ปิดไว้</param>
        public void _visableRefColumn(Boolean value)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                int __checkColumnNumberDocRefNo = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                int __checkColumnNumberBillType = this._findColumnByName(_g.d.ic_trans_detail._doc_ref_type);
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        {
                            MyLib._myGrid._columnType __checkColumn = (MyLib._myGrid._columnType)this._columnList[__checkColumnNumberDocRefNo];
                            __checkColumn._isHide = !value;
                            __checkColumn._widthPercent = (value) ? 10 : 0;
                            this._columnList[__checkColumnNumberDocRefNo] = (MyLib._myGrid._columnType)__checkColumn;
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        {
                            MyLib._myGrid._columnType __checkColumn = (MyLib._myGrid._columnType)this._columnList[__checkColumnNumberDocRefNo];
                            __checkColumn._isHide = !value;
                            __checkColumn._widthPercent = (value) ? 10 : 0;
                            this._columnList[__checkColumnNumberDocRefNo] = (MyLib._myGrid._columnType)__checkColumn;
                        }
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                        {
                            MyLib._myGrid._columnType __checkColumn = (MyLib._myGrid._columnType)this._columnList[__checkColumnNumberDocRefNo];
                            __checkColumn._isHide = !value;
                            __checkColumn._widthPercent = (value) ? 10 : 0;
                            this._columnList[__checkColumnNumberDocRefNo] = (MyLib._myGrid._columnType)__checkColumn;
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        {
                            MyLib._myGrid._columnType __checkColumn = (MyLib._myGrid._columnType)this._columnList[__checkColumnNumberDocRefNo];
                            __checkColumn._isHide = true;
                            __checkColumn._widthPercent = 0;
                            this._columnList[__checkColumnNumberDocRefNo] = (MyLib._myGrid._columnType)__checkColumn;
                        }
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                        {
                            if (__checkColumnNumberDocRefNo != -1)
                            {
                                MyLib._myGrid._columnType __checkColumn = (MyLib._myGrid._columnType)this._columnList[__checkColumnNumberDocRefNo];
                                if (value == true)
                                {
                                    // กรณีมีเอกสารอ้างอิง
                                    if (__checkColumn._isHide == true)
                                    {
                                        __checkColumn._isHide = false;
                                        __checkColumn._widthPercent = 10;
                                        this._columnList[__checkColumnNumberDocRefNo] = (MyLib._myGrid._columnType)__checkColumn;
                                        //
                                    }
                                }
                                else
                                {
                                    // กรณีไม่มีเอกสารอ้างอืง
                                    __checkColumn._isHide = true;
                                    __checkColumn._widthPercent = 0;
                                    this._columnList[__checkColumnNumberDocRefNo] = (MyLib._myGrid._columnType)__checkColumn;
                                    //
                                }
                            }
                        }
                        break;

                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:

                        // show always
                        if (__checkColumnNumberDocRefNo != -1)
                        {
                            MyLib._myGrid._columnType __checkColumn = (MyLib._myGrid._columnType)this._columnList[__checkColumnNumberDocRefNo];
                            __checkColumn._isHide = false;
                            __checkColumn._widthPercent = 10;
                            this._columnList[__checkColumnNumberDocRefNo] = (MyLib._myGrid._columnType)__checkColumn;
                            //
                        }
                        break;



                    default:
                        {
                            if (__checkColumnNumberDocRefNo != -1)
                            {
                                MyLib._myGrid._columnType __checkColumn = (MyLib._myGrid._columnType)this._columnList[__checkColumnNumberDocRefNo];
                                if (value == true)
                                {
                                    // กรณีมีเอกสารอ้างอิง
                                    if (__checkColumn._isHide == true)
                                    {
                                        __checkColumn._isHide = false;
                                        __checkColumn._widthPercent = 10;
                                        this._columnList[__checkColumnNumberDocRefNo] = (MyLib._myGrid._columnType)__checkColumn;
                                        //
                                        if (__checkColumnNumberBillType != -1)
                                        {
                                            __checkColumn = (MyLib._myGrid._columnType)this._columnList[__checkColumnNumberBillType];
                                            __checkColumn._isHide = false;
                                            __checkColumn._widthPercent = 10;
                                            this._columnList[__checkColumnNumberBillType] = (MyLib._myGrid._columnType)__checkColumn;
                                        }
                                    }
                                }
                                else
                                {
                                    // กรณีไม่มีเอกสารอ้างอืง
                                    __checkColumn._isHide = true;
                                    __checkColumn._widthPercent = 0;
                                    this._columnList[__checkColumnNumberDocRefNo] = (MyLib._myGrid._columnType)__checkColumn;
                                    if (__checkColumnNumberBillType != -1)
                                    {
                                        __checkColumn = (MyLib._myGrid._columnType)this._columnList[__checkColumnNumberBillType];
                                        __checkColumn._isHide = true;
                                        __checkColumn._widthPercent = 0;
                                        this._columnList[__checkColumnNumberBillType] = (MyLib._myGrid._columnType)__checkColumn;
                                    }
                                }
                            }
                        }
                        break;
                }
                this._calcPersentWidthToScatter();
                this._recalcColumnWidth(true);
                this.Invalidate();
            }
        }

        private void _icTransRefControlTemp__processButton(object sender)
        {
            _icTransRefProcessButton(sender, null);
        }

        /// <summary>
        /// หลักจากกด Process ดึงอ้างอิงเอกสาร
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="docRefNo"></param>
        public void _icTransRefProcessButton(object sender, string docRefNo)
        {
            int __priceColumnNumber2 = this._findColumnByName(_g.d.ic_trans_detail._price_2);
            int __sumAmountColumnNumber2 = this._findColumnByName(_g.d.ic_trans_detail._sum_amount_2);

            try
            {
                this._icTransRef._transGrid._removeLastControl();

                MyLib._myGlobal._lastTextBox = "";
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query;
                DataSet __result;
                string __wareHouseAndShelfCodeField = "";
                Boolean __foundRef = false;
                switch (this._icTransControlType)
                {
                    #region สินค้า

                    #region สินค้า_เบิกสินค้าวัตถุดิบ               
                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + "," + _g.d.ic_trans_detail._qty
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());

                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                    this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region สินค้า_รับคืนสินค้าจากการเบิก
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + "," + _g.d.ic_trans_detail._qty
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);

                                    if (MyLib._myGlobal._programName.Equals("SML CM"))
                                    {
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                    }
                                    this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region สินค้า_โอนออก
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                        {
                            this._clear();
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                string __GetRemark = "";
                                string __whFrom = "";
                                string __whTo = "";
                                string __locationFrom = "";
                                string __locationTo = "";

                                // ดึงหัวเอกสาร
                                __query = "select " + _g.d.ic_trans._wh_from + "," + _g.d.ic_trans._wh_to + "," + _g.d.ic_trans._location_from + " ," + _g.d.ic_trans._location_to + " ," + _g.d.ic_trans._remark +
                                    " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ขอโอน).ToString();
                                DataTable __docTable = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                                for (int __rowDiscount = 0; __rowDiscount < __docTable.Rows.Count; __rowDiscount++)
                                {
                                    __GetRemark = __docTable.Rows[__rowDiscount][_g.d.ic_trans._remark].ToString();
                                    __whFrom = __docTable.Rows[__rowDiscount][_g.d.ic_trans._wh_from].ToString();
                                    __whTo = __docTable.Rows[__rowDiscount][_g.d.ic_trans._wh_to].ToString();
                                    __locationFrom = __docTable.Rows[__rowDiscount][_g.d.ic_trans._location_from].ToString();
                                    __locationTo = __docTable.Rows[__rowDiscount][_g.d.ic_trans._location_to].ToString();

                                }

                                if (__GetRemark.Length > 0)
                                    this._setRemark(_g.d.ic_trans._remark, __GetRemark);
                                if (__whFrom.Length > 0)
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._wh_from, __whFrom);
                                if (__whTo.Length > 0)
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._wh_to, __whTo);
                                if (__locationFrom.Length > 0)
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._location_from, __locationFrom);
                                if (__locationTo.Length > 0)
                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._location_to, __locationTo);

                                string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";

                                // ดึงรายละเอียด
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + ","
                                    + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + ","
                                    + _g.d.ic_trans_detail._wh_code_2 + ","
                                    + _g.d.ic_trans_detail._shelf_code_2 + "," +
                                     "(" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ") as " + _g.d.ic_trans_detail._qty
                                  + " from " + _g.d.ic_trans_detail._table
                                  + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ขอโอน).ToString()
                                  + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    string __wareHouseCode2 = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code_2].ToString();
                                    string __shelfCode2 = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code_2].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());

                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code_2, __wareHouseCode2, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code_2, __shelfCode2, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                    this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                            }
                        }
                        break;
                    #endregion

                    #endregion

                    #region ซิ้อ

                    #region ซื้อ_ใบสั่งซื้อ_ยกเลิก
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                        {
                            this._clear();
                            //
                            string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);

                            string __balanceQty = "case when coalesce(item_code, '') = '' then 0 else   (((" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0),0))/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ")) end ";

                            DataTable __getData = __myFrameWork._queryShort("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._barcode, _g.d.ic_trans_detail._unit_code, __balanceQty + " as " + _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._line_number) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'" + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString() + " and " + __balanceQty + "<>0 order by line_number ").Tables[0];
                            // ปรับปรุงหน่วยนับ เพื่อคำนวณ
                            for (int __row = 0; __row < __getData.Rows.Count; __row++)
                            {
                                int __addr = this._addRow();
                                int __refRow = MyLib._myGlobal._intPhase(__getData.Rows[__row][_g.d.ic_trans_detail._line_number].ToString());

                                this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __getData.Rows[__row][_g.d.ic_trans_detail._item_code].ToString(), true);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __getData.Rows[__row][_g.d.ic_trans_detail._barcode].ToString(), true);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __getData.Rows[__row][_g.d.ic_trans_detail._unit_code].ToString(), true);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, MyLib._myGlobal._decimalPhase(__getData.Rows[__row][_g.d.ic_trans_detail._qty].ToString()), false);

                                this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);
                            }
                        }
                        break;
                    #endregion
                    #region ซื้อ_ใบสั่งซื้อ
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                        {
                            this._clear();
                            // ค้นหาตามประเภทเอกสาร
                            for (int __docType = 0; __docType <= 1; __docType++)
                            {
                                string __docNoPack = this._icTransRef._getDocRefPackForQuery(__docType);
                                if (__docNoPack.Length > 0)
                                {
                                    string __transFlag = "";
                                    string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))";
                                    //switch (__docType)
                                    //{

                                    //    case 2: // สั่งจอง
                                    //        __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                    //        break;
                                    //    case 3: // สั่งขาย
                                    //        __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                    //        break;
                                    //}
                                    switch (__docType)
                                    {
                                        case 0:
                                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ).ToString();
                                            __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0 and x.doc_no != \'" + this._docNo() + "\' ),0)";
                                            break;
                                        case 1:
                                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ).ToString();
                                            __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0 and x.doc_no != \'" + this._docNo() + "\' ),0)";
                                            break;
                                    }
                                    __wareHouseAndShelfCodeField = "wh_temp";
                                    __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","

                                        + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._ref_guid + "," + _g.d.ic_trans_detail._ref_row + ","
                                        + _g.d.ic_trans_detail._item_code_main + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._is_get_price + ","
                                        + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._set_ref_price + "," + _g.d.ic_trans_detail._set_ref_line + ","

                                        + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                        + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                        + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + ", (" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ") as " + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._sum_amount + ","
                                        + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                        + " from " + _g.d.ic_trans_detail._table
                                        + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag
                                        + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                    __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                    for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                    {
                                        __foundRef = true;
                                        string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                        string __refGuid = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_guid].ToString();
                                        int __refRow = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_row].ToString());
                                        string __itemCodeMain = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code_main].ToString();
                                        int __itemType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_type].ToString());
                                        int __isGetPrice = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_get_price].ToString());
                                        decimal __setRefQty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_qty].ToString());
                                        decimal __setRefPrice = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_price].ToString());
                                        string __setRefLine = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_line].ToString();

                                        string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                        string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                        string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                        string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                        string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                        string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                        decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                        decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                        decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                                        int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                        int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                        string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                        if (__vatType != this._vatTypeNumber())
                                        {
                                            if (this._vatTypeNumber() == 0)
                                            {
                                                // แยกนอก แสดงว่าใบเสนอราคารวมใน ให้เอาราคาใบเสนอราคารวมในมาแยกนอก เพื่อให้สัมพันธ์กับการเสนอราคา
                                                __price = (__price * 100M) / (100M + this._vatRate());
                                            }
                                            if (this._vatTypeNumber() == 1)
                                            {
                                                // รวมใน แสดงว่าใบเสนอราคาแยกนอก
                                                __price = __price + ((__price * this._vatRate()) / 100M);
                                            }
                                        }
                                        if (__wareHouseCode.Trim().Length == 0)
                                        {
                                            string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                        }
                                        int __addr = this._addRow();
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref_type, __docType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_guid, __refGuid, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_line, __setRefLine, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_qty, __setRefQty, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_price, __setRefPrice, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code_main, __itemCodeMain, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_type, __itemType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._is_get_price, __isGetPrice, false);

                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                        if (__priceColumnNumber2 != -1)
                                        {
                                            this._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                        }
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                        if (__sumAmountColumnNumber2 != -1)
                                        {
                                            this._cellUpdate(__addr, __sumAmountColumnNumber2, __amount, false);
                                        }
                                        //this._searchUnitNameWareHouseNameShelfName(__addr);

                                        this._calcItemPrice(__addr, __addr, this._findColumnByName(_g.d.ic_trans_detail._discount));
                                    }
                                    this._searchUnitNameWareHouseNameShelfNameAll();
                                }
                            }
                        }
                        break;

                    #endregion
                    #region ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __wareHouseAndShelfCodeField = "wh_temp";
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                    + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._qty + ","
                                    + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                    int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                    int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                    string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                    if (__vatType != this._vatTypeNumber())
                                    {
                                        if (this._vatTypeNumber() == 0)
                                        {
                                            // แยกนอก แสดงว่าใบสั่งซื้อรวมใน ให้เอาราคาใบสั่งซื้อรวมในมาแยกนอก เพื่อให้สัมพันธ์กับการซื้อ
                                            __price = (__price * 100M) / (100M + this._vatRate());
                                        }
                                        if (this._vatTypeNumber() == 1)
                                        {
                                            // รวมใน แสดงว่าใบสั่งซื้อแยกนอก
                                            __price = __price + ((__price * this._vatRate()) / 100);
                                        }
                                    }
                                    if (__wareHouseCode.Trim().Length == 0)
                                    {
                                        string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                        if (__wareHouseAndShelfCodeTemp.Length > 1)
                                        {
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                        }
                                    }
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    if (__priceColumnNumber2 != -1)
                                    {
                                        this._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                    }
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true); // เอาไว้ด้านล่าง เพราะมันจะได้คำนวณ
                                    //this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();
                            }
                        }
                        break;
                    #endregion
                    #region ซื้อ_พาเชียล_เพิ่มหนี้
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __wareHouseAndShelfCodeField = "wh_temp";
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                    + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._qty + ","
                                    + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                    int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                    int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                    string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                    if (__vatType != this._vatTypeNumber())
                                    {
                                        if (this._vatTypeNumber() == 0)
                                        {
                                            // แยกนอก แสดงว่าใบสั่งซื้อรวมใน ให้เอาราคาใบสั่งซื้อรวมในมาแยกนอก เพื่อให้สัมพันธ์กับการซื้อ
                                            __price = (__price * 100M) / (100M + this._vatRate());
                                        }
                                        if (this._vatTypeNumber() == 1)
                                        {
                                            // รวมใน แสดงว่าใบสั่งซื้อแยกนอก
                                            __price = __price + ((__price * this._vatRate()) / 100);
                                        }
                                    }
                                    if (__wareHouseCode.Trim().Length == 0)
                                    {
                                        string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                        if (__wareHouseAndShelfCodeTemp.Length > 1)
                                        {
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                        }
                                    }
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    if (__priceColumnNumber2 != -1)
                                    {
                                        this._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                    }
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true); // เอาไว้ด้านล่าง เพราะมันจะได้คำนวณ
                                    //this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();
                            }
                        }
                        break;
                    #endregion
                    #region ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __wareHouseAndShelfCodeField = "wh_temp";
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","

                                    // โต๋ เอา wh shelf เดิมมา
                                    //+ "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                    //+ "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","

                                    + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + ","

                                    + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._qty + ","
                                    + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                    int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                    int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                    string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                    if (__vatType != this._vatTypeNumber())
                                    {
                                        if (this._vatTypeNumber() == 0)
                                        {
                                            // แยกนอก แสดงว่าใบสั่งซื้อรวมใน ให้เอาราคาใบสั่งซื้อรวมในมาแยกนอก เพื่อให้สัมพันธ์กับการซื้อ
                                            __price = (__price * 100M) / (100M + this._vatRate());
                                        }
                                        if (this._vatTypeNumber() == 1)
                                        {
                                            // รวมใน แสดงว่าใบสั่งซื้อแยกนอก
                                            __price = __price + ((__price * this._vatRate()) / 100);
                                        }
                                    }
                                    if (__wareHouseCode.Trim().Length == 0)
                                    {
                                        string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                        __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                        __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                    }
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    if (__priceColumnNumber2 != -1)
                                    {
                                        this._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                    }
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true); // เอาไว้ด้านล่าง เพราะมันจะได้คำนวณ
                                    //this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();
                            }
                        }
                        break;
                    #endregion
                    #region ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __wareHouseAndShelfCodeField = "wh_temp";
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                    + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._qty + ","
                                    + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                    int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                    int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                    string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                    if (__vatType != this._vatTypeNumber())
                                    {
                                        if (this._vatTypeNumber() == 0)
                                        {
                                            // แยกนอก แสดงว่าใบสั่งซื้อรวมใน ให้เอาราคาใบสั่งซื้อรวมในมาแยกนอก เพื่อให้สัมพันธ์กับการซื้อ
                                            __price = (__price * 100M) / (100M + this._vatRate());
                                        }
                                        if (this._vatTypeNumber() == 1)
                                        {
                                            // รวมใน แสดงว่าใบสั่งซื้อแยกนอก
                                            __price = __price + ((__price * this._vatRate()) / 100M);
                                        }
                                    }
                                    if (__wareHouseCode.Trim().Length == 0)
                                    {
                                        string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                        if (__wareHouseAndShelfCodeTemp.Length > 1)
                                        {
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                        }
                                    }
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    if (__priceColumnNumber2 != -1)
                                    {
                                        this._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                    }
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true); // เอาไว้ด้านล่าง เพราะมันจะได้คำนวณ
                                    //this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();
                            }
                        }
                        break;
                    #endregion
                    #region ซื้อ_พาเชียล_ลดหนี้
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __wareHouseAndShelfCodeField = "wh_temp";
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                    + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._qty + ","
                                    + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                    int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                    int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                    string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                    if (__vatType != this._vatTypeNumber())
                                    {
                                        if (this._vatTypeNumber() == 0)
                                        {
                                            // แยกนอก แสดงว่าใบสั่งซื้อรวมใน ให้เอาราคาใบสั่งซื้อรวมในมาแยกนอก เพื่อให้สัมพันธ์กับการซื้อ
                                            __price = (__price * 100M) / (100M + this._vatRate());
                                        }
                                        if (this._vatTypeNumber() == 1)
                                        {
                                            // รวมใน แสดงว่าใบสั่งซื้อแยกนอก
                                            __price = __price + ((__price * this._vatRate()) / 100M);
                                        }
                                    }
                                    if (__wareHouseCode.Trim().Length == 0)
                                    {
                                        string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                        if (__wareHouseAndShelfCodeTemp.Length > 1)
                                        {
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                        }
                                    }
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    if (__priceColumnNumber2 != -1)
                                    {
                                        this._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                    }
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true); // เอาไว้ด้านล่าง เพราะมันจะได้คำนวณ
                                    //this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();
                            }
                        }
                        break;
                    #endregion
                    #region ซื้อ_ซื้อสินค้าและค่าบริการ
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        {
                            this._clear();

                            string __discountWord = "x";
                            decimal __discountAmount = 0.0M;

                            string __transFlag = "";
                            string __sale_code = "";
                            int __inquiry_type = 0;
                            int __vat_type = 0;
                            int __getCreditDay = 0;
                            string __GetRemark = "";

                            //
                            // toe เช็ค คลังและที่เก็บ จาก screen top ด้วย
                            string __whCodeScreenSelected = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from);
                            string __shelfCodeScreenSelected = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_from);

                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {

                                // toe ดึงส่วนลด
                                try
                                {

                                    __query = "select " + _g.d.ic_trans._discount_word + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._sale_code + " ," + _g.d.ic_trans._inquiry_type + ", " + _g.d.ic_trans._vat_type +
                                        ", " + _g.d.ic_trans._credit_day +
                                        ", " + _g.d.ic_trans._remark +
                                        ", " + _g.d.ic_trans._branch_code +
                                        ", " + _g.d.ic_trans._department_code +

                                        " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString();
                                    DataTable __discountTable = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                                    for (int __rowDiscount = 0; __rowDiscount < __discountTable.Rows.Count; __rowDiscount++)
                                    {
                                        if (__discountWord.Equals("x"))
                                        {
                                            __discountWord = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._discount_word].ToString();
                                        }
                                        else
                                        {
                                            __discountWord = "";
                                        }
                                        __discountAmount += MyLib._myGlobal._decimalPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._total_discount].ToString());

                                        __sale_code = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._sale_code].ToString();
                                        __inquiry_type = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._inquiry_type].ToString());
                                        __vat_type = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._vat_type].ToString());
                                        __getCreditDay = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._credit_day].ToString());
                                        __GetRemark = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._remark].ToString();
                                    }

                                    if (__sale_code.Length > 0)
                                    {
                                        this._icTransScreenTop._setDataStr(_g.d.ic_trans._sale_code, __sale_code);
                                    }
                                    this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, __inquiry_type);
                                    this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __vat_type);

                                    if (__getCreditDay != 0M)
                                    {
                                        if (this._changeCreditDay != null)
                                        {
                                            this._changeCreditDay(__getCreditDay);
                                        }
                                    }

                                    if (__GetRemark.Length > 0)
                                    {
                                        if (this._setRemark != null)
                                        {
                                            this._setRemark(_g.d.ic_trans._remark, __GetRemark);
                                        }
                                    }

                                    if (MyLib._myGlobal._programName.Equals("SML CM"))
                                    {
                                        for (int __rowDiscount = 0; __rowDiscount < __discountTable.Rows.Count; __rowDiscount++)
                                        {
                                            string __branchCode = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._branch_code].ToString();
                                            string __departmentCode = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._department_code].ToString();

                                            if (__branchCode.Length > 0)
                                                this._setRemark(_g.d.ic_trans._branch_code, __branchCode);
                                            if (__departmentCode.Length > 0)
                                                this._setRemark(_g.d.ic_trans._department_code, __departmentCode);

                                        }
                                    }
                                }
                                catch
                                {

                                }

                                __wareHouseAndShelfCodeField = "wh_temp";

                                //string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() +"," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";

                                // โต๋ เพิ่มดึงเฉพาะจำนวนที่เหลือออกมาก็พอ
                                string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";


                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","

                                    // โต๋ดึง wh shelf จากอ้างอิงมา
                                    // + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                    // + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                    + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + ","

                                    + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + ", case when coalesce(item_code, '') = '' then 0 else   ((" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ")) end as " + _g.d.ic_trans_detail._qty + ","
                                    + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                    + "," + _g.d.ic_trans_detail._is_serial_number
                                    + "," + _g.d.ic_trans_detail._line_number

                                    //+ ",() as " + _g.d.ic_trans_detail._item_type
                                    //+ "," + _g.d.ic_trans_detail._line_number
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    int __refRow = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._line_number].ToString());

                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                    int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                    int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                    int __isSerialNumber = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_serial_number].ToString());
                                    string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                    if (__vatType != this._vatTypeNumber())
                                    {
                                        if (this._vatTypeNumber() == 0)
                                        {
                                            // แยกนอก แสดงว่าใบสั่งซื้อรวมใน ให้เอาราคาใบสั่งซื้อรวมในมาแยกนอก เพื่อให้สัมพันธ์กับการซื้อ
                                            __price = (__price * 100M) / (100M + this._vatRate());
                                        }
                                        if (this._vatTypeNumber() == 1)
                                        {
                                            // รวมใน แสดงว่าใบสั่งซื้อแยกนอก
                                            __price = __price + ((__price * this._vatRate()) / 100M);
                                        }
                                    }
                                    if (__wareHouseCode.Trim().Length == 0)
                                    {
                                        string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');

                                        if (__wareHouseAndShelfCodeTemp.Length > 1)
                                        {
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();

                                            if (__whCodeScreenSelected.Trim().Length > 0)
                                            {
                                                __wareHouseCode = __whCodeScreenSelected;
                                            }

                                            if (__shelfCodeScreenSelected.Trim().Length > 0)
                                            {
                                                __shelfCode = __shelfCodeScreenSelected;
                                            }
                                        }
                                    }

                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);

                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    if (__priceColumnNumber2 != -1)
                                    {
                                        this._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                    }
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._is_serial_number, __isSerialNumber, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true); // เอาไว้ด้านล่าง เพราะมันจะได้คำนวณ
                                    //this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();
                            }
                            if (this._afterProcess != null)
                            {
                                this._afterProcess(__discountWord, __discountAmount);
                            }
                        }
                        break;
                    #endregion
                    #region ซื้อ_พาเชียล_ตั้งหนี้
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __wareHouseAndShelfCodeField = "wh_temp";

                                // โต๋แก้ wh_temp ดึงจาก ic_trans แทน + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField

                                //string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() +"," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                    + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                    + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._qty + ","
                                    + "(" + _g.d.ic_trans_detail._wh_code + "||'~'||" + _g.d.ic_trans_detail._shelf_code + ") as " + __wareHouseAndShelfCodeField
                                    + ",line_number "
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                    int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                    int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                    string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                    if (__vatType != this._vatTypeNumber())
                                    {
                                        if (this._vatTypeNumber() == 0)
                                        {
                                            // แยกนอก แสดงว่าใบสั่งซื้อรวมใน ให้เอาราคาใบสั่งซื้อรวมในมาแยกนอก เพื่อให้สัมพันธ์กับการซื้อ
                                            __price = (__price * 100M) / (100M + this._vatRate());
                                        }
                                        if (this._vatTypeNumber() == 1)
                                        {
                                            // รวมใน แสดงว่าใบสั่งซื้อแยกนอก
                                            __price = __price + ((__price * this._vatRate()) / 100M);
                                        }
                                    }
                                    if (__wareHouseCode.Trim().Length == 0)
                                    {
                                        string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                        if (__wareHouseAndShelfCodeTemp.Length > 1)
                                        {
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                        }
                                    }
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    if (__priceColumnNumber2 != -1)
                                    {
                                        this._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                    }
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true); // เอาไว้ด้านล่าง เพราะมันจะได้คำนวณ
                                    // this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();
                            }
                        }
                        break;
                    #endregion
                    #region ซื้อ_พาเชียล_รับสินค้า
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __wareHouseAndShelfCodeField = "wh_temp";

                                string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";


                                //string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","

                                    // โต๋เอาคลังที่เปิดขอ PO มา
                                    //+ "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                    //+ "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","

                                    + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + ","

                                    + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + ", case when coalesce(item_code, '') = '' then 0 else   ((" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ")) end as " + _g.d.ic_trans_detail._qty + ","
                                    + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                    + "," + _g.d.ic_trans_detail._is_serial_number
                                    + "," + _g.d.ic_trans_detail._line_number
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString()
                                    + " and ( coalesce(item_code, '') = '' or  ((" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ")) <> 0 ) "
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;

                                    int __refRow = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._line_number].ToString());


                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                    int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                    int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                    string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                    int __isSerialNumber = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_serial_number].ToString());

                                    if (__vatType != this._vatTypeNumber())
                                    {
                                        if (this._vatTypeNumber() == 0)
                                        {
                                            // แยกนอก แสดงว่าใบสั่งซื้อรวมใน ให้เอาราคาใบสั่งซื้อรวมในมาแยกนอก เพื่อให้สัมพันธ์กับการซื้อ
                                            __price = (__price * 100M) / (100M + this._vatRate());
                                        }
                                        if (this._vatTypeNumber() == 1)
                                        {
                                            // รวมใน แสดงว่าใบสั่งซื้อแยกนอก
                                            __price = __price + ((__price * this._vatRate()) / 100M);
                                        }
                                    }
                                    if (__wareHouseCode.Trim().Length == 0)
                                    {
                                        string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                        if (__wareHouseAndShelfCodeTemp.Length > 1)
                                        {
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                        }
                                    }
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);

                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    if (__priceColumnNumber2 != -1)
                                    {
                                        this._cellUpdate(__addr, __priceColumnNumber2, __price, false);
                                    }
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._is_serial_number, __isSerialNumber, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, true); // เอาไว้ด้านล่าง เพราะมันจะได้คำนวณ
                                    //this._searchUnitNameWareHouseNameShelfName(__addr);
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();
                            }
                        }
                        break;
                    #endregion

                    #endregion

                    #region ขาย

                    #region ขาย_สั่งจองและสั่งซื้อสินค้า
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                string __sale_code = "";
                                int __inquiry_type = 0;
                                int __vat_type = 0;
                                // ดึง sale
                                try
                                {
                                    __query = "select " + _g.d.ic_trans._inquiry_type + ", " + _g.d.ic_trans._vat_type + "," + _g.d.ic_trans._sale_code + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString();
                                    DataTable __discountTable = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                                    for (int __rowDiscount = 0; __rowDiscount < __discountTable.Rows.Count; __rowDiscount++)
                                    {
                                        __sale_code = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._sale_code].ToString();
                                        __inquiry_type = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._inquiry_type].ToString());
                                        __vat_type = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._vat_type].ToString());
                                    }

                                    if (__sale_code.Length > 0)
                                    {
                                        this._icTransScreenTop._setDataStr(_g.d.ic_trans._sale_code, __sale_code);
                                    }
                                    this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, __inquiry_type);
                                    this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __vat_type);
                                }
                                catch
                                {
                                }

                                string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";

                                __wareHouseAndShelfCodeField = "wh_temp";
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type
                                    + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._ref_guid + "," + _g.d.ic_trans_detail._ref_row
                                    + "," + _g.d.ic_trans_detail._item_code_main + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._is_get_price
                                    + "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._set_ref_price + "," + _g.d.ic_trans_detail._set_ref_line + "," + _g.d.ic_trans_detail._is_serial_number
                                    + "," + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code
                                    + "," + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code
                                    + "," + _g.d.ic_trans_detail._price
                                    + "," + _g.d.ic_trans_detail._discount
                                    + ",(" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ") as " + _g.d.ic_trans_detail._qty
                                    + "," + _g.d.ic_trans_detail._sum_amount
                                    + "," + _g.d.ic_trans_detail._is_permium
                                    + ",(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                    + " from " + _g.d.ic_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __refGuid = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_guid].ToString();
                                    int __refRow = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_row].ToString());
                                    string __itemCodeMain = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code_main].ToString();
                                    int __itemType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_type].ToString());
                                    int __isGetPrice = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_get_price].ToString());
                                    decimal __setRefQty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_qty].ToString());
                                    decimal __setRefPrice = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_price].ToString());
                                    string __setRefLine = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_line].ToString();

                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                    decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                                    int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                    int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                    int __is_serial_number = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_serial_number].ToString());
                                    int __is_permium = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_permium].ToString());

                                    string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                    if (__vatType != this._vatTypeNumber())
                                    {
                                        if (this._vatTypeNumber() == 0)
                                        {
                                            // แยกนอก แสดงว่าใบเสนอราคารวมใน ให้เอาราคาใบเสนอราคารวมในมาแยกนอก เพื่อให้สัมพันธ์กับการเสนอราคา
                                            __price = (__price * 100M) / (100M + this._vatRate());
                                        }
                                        if (this._vatTypeNumber() == 1)
                                        {
                                            // รวมใน แสดงว่าใบเสนอราคาแยกนอก
                                            __price = __price + ((__price * this._vatRate()) / 100M);
                                        }
                                    }
                                    if (__wareHouseCode.Trim().Length == 0)
                                    {
                                        string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                        __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                        __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                    }
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_guid, __refGuid, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_line, __setRefLine, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_qty, __setRefQty, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_price, __setRefPrice, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code_main, __itemCodeMain, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_type, __itemType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._is_get_price, __isGetPrice, false);

                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);

                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._is_serial_number, __is_serial_number, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._is_permium, __is_permium, false);

                                    //this._searchUnitNameWareHouseNameShelfName(__addr);
                                    this._calcItemPrice(__addr, __addr, this._findColumnByName(_g.d.ic_trans_detail._discount));

                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();
                            }
                        }
                        break;
                    #endregion
                    #region ขาย_สั่งขาย
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        {
                            this._clear();
                            // ค้นหาตามประเภทเอกสาร
                            for (int __docType = 1; __docType <= 2; __docType++)
                            {
                                string __docNoPack = this._icTransRef._getDocRefPackForQuery(__docType);
                                if (__docNoPack.Length > 0)
                                {
                                    string __transFlag = "";
                                    string __sale_code = "";
                                    int __inquiry_type = 0;
                                    int __vat_type = 0;

                                    string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))";

                                    switch (__docType)
                                    {
                                        case 1:
                                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString();
                                            __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                            break;
                                        case 2:
                                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString();
                                            __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                            break;
                                    }

                                    try
                                    {
                                        __query = "select " + _g.d.ic_trans._discount_word + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._sale_code + "," + _g.d.ic_trans._inquiry_type + ", " + _g.d.ic_trans._vat_type + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag;
                                        DataTable __discountTable = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                                        for (int __rowDiscount = 0; __rowDiscount < __discountTable.Rows.Count; __rowDiscount++)
                                        {
                                            __sale_code = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._sale_code].ToString();
                                            __inquiry_type = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._inquiry_type].ToString());
                                            __vat_type = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._vat_type].ToString());

                                        }

                                        if (__sale_code.Length > 0)
                                        {
                                            this._icTransScreenTop._setDataStr(_g.d.ic_trans._sale_code, __sale_code);
                                        }
                                        this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, __inquiry_type);
                                        this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __vat_type);

                                    }
                                    catch
                                    {
                                    }

                                    __wareHouseAndShelfCodeField = "wh_temp";
                                    __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","

                                        + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._ref_guid + "," + _g.d.ic_trans_detail._ref_row + ","
                                        + _g.d.ic_trans_detail._item_code_main + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._is_get_price + ","
                                        + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._set_ref_price + "," + _g.d.ic_trans_detail._set_ref_line + "," + _g.d.ic_trans_detail._is_serial_number + ","

                                        + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                        + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                        + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + ", (" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ") as " + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._sum_amount + ","
                                        + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                        + "," + _g.d.ic_trans_detail._is_permium
                                        + " from " + _g.d.ic_trans_detail._table
                                        + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag
                                        + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                    __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                    for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                    {
                                        __foundRef = true;
                                        string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                        string __refGuid = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_guid].ToString();
                                        int __refRow = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_row].ToString());
                                        string __itemCodeMain = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code_main].ToString();
                                        int __itemType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_type].ToString());
                                        int __isGetPrice = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_get_price].ToString());
                                        decimal __setRefQty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_qty].ToString());
                                        decimal __setRefPrice = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_price].ToString());
                                        string __setRefLine = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_line].ToString();

                                        string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                        string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                        string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                        string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                        string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                        string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                        decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                        decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                        decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                                        int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                        int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                        int __is_serial_number = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_serial_number].ToString());
                                        int __is_permium = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_permium].ToString());

                                        string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                        if (__vatType != this._vatTypeNumber())
                                        {
                                            if (this._vatTypeNumber() == 0)
                                            {
                                                // แยกนอก แสดงว่าใบเสนอราคารวมใน ให้เอาราคาใบเสนอราคารวมในมาแยกนอก เพื่อให้สัมพันธ์กับการเสนอราคา
                                                __price = (__price * 100M) / (100M + this._vatRate());
                                            }
                                            if (this._vatTypeNumber() == 1)
                                            {
                                                // รวมใน แสดงว่าใบเสนอราคาแยกนอก
                                                __price = __price + ((__price * this._vatRate()) / 100M);
                                            }
                                        }
                                        if (__wareHouseCode.Trim().Length == 0)
                                        {
                                            string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                        }
                                        int __addr = this._addRow();
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref_type, __docType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_guid, __refGuid, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_line, __setRefLine, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_qty, __setRefQty, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_price, __setRefPrice, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code_main, __itemCodeMain, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_type, __itemType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._is_get_price, __isGetPrice, false);

                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);

                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._is_serial_number, __is_serial_number, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._is_permium, __is_permium, false);

                                        //this._searchUnitNameWareHouseNameShelfName(__addr);
                                        this._calcItemPrice(__addr, __addr, this._findColumnByName(_g.d.ic_trans_detail._discount));

                                    }
                                    this._searchUnitNameWareHouseNameShelfNameAll();
                                }
                            }
                        }
                        break;
                    #endregion
                    #region ขาย_ขายสินค้าและบริการ
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        {
                            this._clear();
                            string __discountWord = "x";
                            decimal __discountAmount = 0.0M;
                            // ค้นหาตามประเภทเอกสาร
                            for (int __docType = 1; __docType <= 3; __docType++)
                            {
                                string __docNoPack = this._icTransRef._getDocRefPackForQuery(__docType);

                                if (__docType == 3 && MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._icTransRef._transGrid._rowData.Count > 0)
                                {
                                    string __ref_doc_no = this._icTransRef._transGrid._cellGet(0, _g.d.ap_ar_trans_detail._ref_doc_no).ToString();
                                    DateTime __ref_doc_date = (DateTime)this._icTransRef._transGrid._cellGet(0, _g.d.ap_ar_trans_detail._ref_doc_date);


                                    this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_ref, __ref_doc_no);
                                    this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, __ref_doc_date);

                                }

                                if (__docNoPack.Length > 0)
                                {
                                    string __transFlag = "";
                                    string __sale_code = "";
                                    int __inquiry_type = 0;
                                    int __vat_type = 0;
                                    int __getCreditDay = 0;
                                    string __GetRemark = "";
                                    string __remark2 = "";
                                    string __remark3 = "";

                                    switch (__docType)
                                    {
                                        case 1: __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString(); break;
                                        case 2: __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString(); break;
                                        case 3: __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString(); break;
                                    }
                                    // ดึงส่วนลด
                                    try
                                    {
                                        __query = "select " + _g.d.ic_trans._discount_word + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._sale_code + " ," + _g.d.ic_trans._inquiry_type +
                                            ", " + _g.d.ic_trans._vat_type + ", " + _g.d.ic_trans._credit_day +
                                            ", " + _g.d.ic_trans._remark +
                                            ", " + _g.d.ic_trans._remark_2 +
                                            ", " + _g.d.ic_trans._remark_3 +
                                            ", " + _g.d.ic_trans._branch_code +
                                            ", " + _g.d.ic_trans._department_code +

                                            " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag;
                                        DataTable __discountTable = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                                        for (int __rowDiscount = 0; __rowDiscount < __discountTable.Rows.Count; __rowDiscount++)
                                        {
                                            if (__discountWord.Equals("x"))
                                            {
                                                __discountWord = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._discount_word].ToString();
                                            }
                                            else
                                            {
                                                __discountWord = "";
                                            }
                                            __discountAmount += MyLib._myGlobal._decimalPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._total_discount].ToString());

                                            __sale_code = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._sale_code].ToString();
                                            __inquiry_type = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._inquiry_type].ToString());
                                            __vat_type = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._vat_type].ToString());
                                            __getCreditDay = MyLib._myGlobal._intPhase(__discountTable.Rows[__rowDiscount][_g.d.ic_trans._credit_day].ToString());
                                            __GetRemark = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._remark].ToString();
                                            __remark2 = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._remark_2].ToString();
                                            __remark3 = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._remark_3].ToString();

                                        }

                                        if (__sale_code.Length > 0)
                                        {
                                            this._icTransScreenTop._setDataStr(_g.d.ic_trans._sale_code, __sale_code);
                                        }
                                        this._icTransScreenTop._setComboBox(_g.d.ic_trans._inquiry_type, __inquiry_type);
                                        this._icTransScreenTop._setComboBox(_g.d.ic_trans._vat_type, __vat_type);

                                        if (__getCreditDay != 0M)
                                        {
                                            if (this._changeCreditDay != null)
                                            {
                                                this._changeCreditDay(__getCreditDay);
                                            }
                                        }

                                        if (__GetRemark.Length > 0 || __remark2.Length > 0 || __remark3.Length > 0)
                                        {
                                            if (this._setRemark != null)
                                            {
                                                if (__GetRemark.Length > 0)
                                                    this._setRemark(_g.d.ic_trans._remark, __GetRemark);
                                                if (__remark2.Length > 0)
                                                    this._setRemark(_g.d.ic_trans._remark_2, __remark2);
                                                if (__remark3.Length > 0)
                                                    this._setRemark(_g.d.ic_trans._remark_3, __remark3);
                                            }
                                        }

                                        if (MyLib._myGlobal._programName.Equals("SML CM"))
                                        {
                                            for (int __rowDiscount = 0; __rowDiscount < __discountTable.Rows.Count; __rowDiscount++)
                                            {
                                                string __branchCode = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._branch_code].ToString();
                                                string __departmentCode = __discountTable.Rows[__rowDiscount][_g.d.ic_trans._department_code].ToString();

                                                if (__branchCode.Length > 0)
                                                    this._setRemark(_g.d.ic_trans._branch_code, __branchCode);
                                                if (__departmentCode.Length > 0)
                                                    this._setRemark(_g.d.ic_trans._department_code, __departmentCode);

                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    // ดึงสินค้า
                                    __wareHouseAndShelfCodeField = "wh_temp";
                                    string __getWh_code = "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code;
                                    string __getShelf_code = "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code;

                                    string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))";
                                    switch (__docType)
                                    {
                                        // เสนอราคา
                                        case 1:
                                            __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                            break;
                                        case 2: // สั่งจอง
                                            __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                            if (_g.g._companyProfile._stock_reserved_control_location)
                                            {
                                                __getWh_code = _g.d.ic_trans_detail._wh_code;
                                                __getShelf_code = _g.d.ic_trans_detail._shelf_code;
                                            }
                                            break;
                                        case 3: // สั่งขาย
                                            __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                            __getWh_code = _g.d.ic_trans_detail._wh_code;
                                            __getShelf_code = _g.d.ic_trans_detail._shelf_code;
                                            break;
                                    }
                                    __query = "select * from ( select line_number,  " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","

                                        + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._ref_guid + "," + _g.d.ic_trans_detail._ref_row + ","
                                        + _g.d.ic_trans_detail._item_code_main + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._is_get_price + ","
                                        + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._set_ref_price + "," + _g.d.ic_trans_detail._set_ref_line + "," + _g.d.ic_trans_detail._is_serial_number + ","

                                        // toe ดึงคลังและที่เก็บจากเอกสารต้นทาง
                                        //+ "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                        //+ "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                        + __getWh_code + ","
                                        + __getShelf_code + ","
                                        + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + ",(" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ") as " + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._sum_amount
                                        + ", (select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                        + ", (select " + _g.d.ic_inventory_detail._is_hold_sale + " from " + _g.d.ic_inventory_detail._table + " where  " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " = " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + ") as " + _g.d.ic_inventory_detail._is_hold_sale
                                        //+ ", (select " + _g.d.ic_inventory._unit_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._unit_type
                                        //+ ", " + _g.d.ic_trans_detail._stand_value
                                        //+ ", " + _g.d.ic_trans_detail._divide_value
                                        + "," + _g.d.ic_trans_detail._lot_number_1
                                        + "," + _g.d.ic_trans_detail._date_expire
                                        + "," + _g.d.ic_trans_detail._mfd_date
                                        + "," + _g.d.ic_trans_detail._mfn_name
                                        + "," + _g.d.ic_trans_detail._is_permium


                                        + " from " + _g.d.ic_trans_detail._table
                                        + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag
                                        + " ) as temp2 where qty <> 0 "
                                        + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                    __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                    for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                    {
                                        __foundRef = true;
                                        string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                        string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                        string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                        string __refGuid = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_guid].ToString();
                                        int __refRow = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._line_number].ToString()); //  MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_row].ToString());
                                        string __itemCodeMain = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code_main].ToString();
                                        int __itemType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_type].ToString());
                                        int __isGetPrice = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_get_price].ToString());
                                        decimal __setRefQty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_qty].ToString());
                                        decimal __setRefPrice = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_price].ToString());
                                        string __setRefLine = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_line].ToString();

                                        string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                        string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                        string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                        string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                        decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                        decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                        decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString()); //__qty * __price; 
                                        int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                        int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                        int __is_serial_number = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_serial_number].ToString());

                                        int __is_hold_sale = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_inventory_detail._is_hold_sale].ToString());
                                        //int __unitType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_type].ToString());
                                        //decimal __standValue = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._stand_value].ToString());
                                        //decimal __divideValue = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._divide_value].ToString());
                                        int __is_premium = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_permium].ToString());

                                        string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();

                                        // for lot
                                        string __lotNumber = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._lot_number_1].ToString();
                                        string __mfnName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._mfn_name].ToString();
                                        DateTime __dateExpire = MyLib._myGlobal._convertDateFromQuery(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._date_expire].ToString());
                                        DateTime __mfdDate = MyLib._myGlobal._convertDateFromQuery(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._mfd_date].ToString());

                                        if (__is_hold_sale == 0)
                                        {


                                            if (__vatType != this._vatTypeNumber())
                                            {
                                                if (this._vatTypeNumber() == 0)
                                                {
                                                    // แยกนอก แสดงว่าใบเสนอราคารวมใน ให้เอาราคาใบเสนอราคารวมในมาแยกนอก เพื่อให้สัมพันธ์กับการเสนอราคา
                                                    __price = (__price * 100M) / (100M + this._vatRate());
                                                }
                                                if (this._vatTypeNumber() == 1)
                                                {
                                                    // รวมใน แสดงว่าใบเสนอราคาแยกนอก
                                                    __price = __price + ((__price * this._vatRate()) / 100M);
                                                }
                                            }
                                            if (__wareHouseCode.Trim().Length == 0)
                                            {
                                                string __wareHouseAndShelfCodeStr = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Trim();
                                                if (__wareHouseAndShelfCodeStr.Length > 0)
                                                {
                                                    string[] __wareHouseAndShelfCodeTemp = __wareHouseAndShelfCodeStr.Split('~');
                                                    __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                                    __shelfCode = (__wareHouseAndShelfCodeTemp.Length > 1) ? __wareHouseAndShelfCodeTemp[1].ToString() : "";
                                                }
                                            }

                                            int __addr = this._addRow();

                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref_type, __docType, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);

                                            //this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_type, __unitType, false);
                                            //this._cellUpdate(__addr, _g.d.ic_trans_detail._stand_value, __standValue, false);
                                            //this._cellUpdate(__addr, _g.d.ic_trans_detail._divide_value, __divideValue, false);

                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_guid, __refGuid, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_line, __setRefLine, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_qty, __setRefQty, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_price, __setRefPrice, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code_main, __itemCodeMain, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._item_type, __itemType, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._is_get_price, __isGetPrice, false);

                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false); // toe ให้ event active = true เพื่อไปตรวจสอบยอดคงเหลือหรือค้างจอง
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._is_permium, __is_premium, false);

                                            this._cellUpdate(__addr, _g.d.ic_trans_detail._is_serial_number, __is_serial_number, true);
                                            if (__docType == 3)
                                            {
                                                //this._searchUnitNameWareHouseNameShelfName(__addr); // ย้ายไปทำสุดท้ายครั้งเดียว
                                                this._cellUpdate(__addr, _g.d.ic_trans_detail._lot_number_1, __lotNumber, false);
                                                this._cellUpdate(__addr, _g.d.ic_trans_detail._date_expire, __dateExpire, false);
                                                this._cellUpdate(__addr, _g.d.ic_trans_detail._mfd_date, __mfdDate, false);
                                                this._cellUpdate(__addr, _g.d.ic_trans_detail._mfn_name, __mfnName, false);
                                            }

                                            // toe เพิ่ม สั่งคำณวนราคาใหม่
                                            this._calcItemPrice(__addr, __addr, this._findColumnByName(_g.d.ic_trans_detail._discount));
                                        }
                                        else
                                        {
                                            MessageBox.Show("สินค้า " + __itemCode + " : " + __itemName + " ห้ามขาย");
                                        }
                                    }

                                    //  
                                    this._searchUnitNameWareHouseNameShelfNameAll();

                                }
                            }
                            if (this._afterProcess != null)
                            {
                                this._afterProcess(__discountWord, __discountAmount);
                            }

                        }
                        break;
                    #endregion
                    #region ขาย_เพิ่มหนี้
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        {
                            this._clear();
                            // ค้นหาตามประเภทเอกสาร
                            for (int __docType = 1; __docType <= 1; __docType++)
                            {
                                string __docNoPack = this._icTransRef._getDocRefPackForQuery(__docType);
                                if (__docNoPack.Length > 0)
                                {
                                    string __transFlag = "";
                                    switch (__docType)
                                    {
                                        case 1: __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString(); break;
                                    }
                                    __wareHouseAndShelfCodeField = "wh_temp";
                                    __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","

                                        + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._ref_guid + "," + _g.d.ic_trans_detail._ref_row + ","
                                        + _g.d.ic_trans_detail._item_code_main + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._is_get_price + ","
                                        + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._set_ref_price + "," + _g.d.ic_trans_detail._set_ref_line + ","

                                        + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code + ","
                                        + "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code + ","
                                        + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._sum_amount + ","
                                        + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                        + " from " + _g.d.ic_trans_detail._table
                                        + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag
                                        + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                    __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                    for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                    {
                                        __foundRef = true;
                                        string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                        string __refGuid = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_guid].ToString();
                                        int __refRow = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_row].ToString());
                                        string __itemCodeMain = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code_main].ToString();
                                        int __itemType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_type].ToString());
                                        int __isGetPrice = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_get_price].ToString());
                                        decimal __setRefQty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_qty].ToString());
                                        decimal __setRefPrice = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_price].ToString());
                                        string __setRefLine = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_line].ToString();

                                        string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                        string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                        string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                        string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                        string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                        string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                        decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                        decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                        decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                                        int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                        int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());
                                        string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                        if (__vatType != this._vatTypeNumber())
                                        {
                                            if (this._vatTypeNumber() == 0)
                                            {
                                                // แยกนอก แสดงว่าใบเสนอราคารวมใน ให้เอาราคาใบเสนอราคารวมในมาแยกนอก เพื่อให้สัมพันธ์กับการเสนอราคา
                                                __price = (__price * 100M) / (100M + this._vatRate());
                                            }
                                            if (this._vatTypeNumber() == 1)
                                            {
                                                // รวมใน แสดงว่าใบเสนอราคาแยกนอก
                                                __price = __price + ((__price * this._vatRate()) / 100M);
                                            }
                                        }
                                        if (__wareHouseCode.Trim().Length == 0)
                                        {
                                            string[] __wareHouseAndShelfCodeTemp = __result.Tables[0].Rows[__row][__wareHouseAndShelfCodeField].ToString().Split('~');
                                            __wareHouseCode = __wareHouseAndShelfCodeTemp[0].ToString();
                                            __shelfCode = __wareHouseAndShelfCodeTemp[1].ToString();
                                        }
                                        int __addr = this._addRow();
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref_type, __docType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_guid, __refGuid, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_line, __setRefLine, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_qty, __setRefQty, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_price, __setRefPrice, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code_main, __itemCodeMain, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_type, __itemType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._is_get_price, __isGetPrice, false);

                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._price, ((MyLib._myGlobal._OEMVersion.Equals("SINGHA")) ? __price : 0M), false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, ((MyLib._myGlobal._OEMVersion.Equals("SINGHA")) ? __discount : ""), false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, ((MyLib._myGlobal._OEMVersion.Equals("SINGHA")) ? __qty : 0M), false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, ((MyLib._myGlobal._OEMVersion.Equals("SINGHA")) ? __amount : 0M), false);

                                        //this._searchUnitNameWareHouseNameShelfName(__addr);
                                    }

                                    this._searchUnitNameWareHouseNameShelfNameAll();
                                }
                            }
                        }
                        break;
                    #endregion
                    #region ขาย_รับคืนสินค้าจากการขายและลดหนี้
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                        {
                            this._clear();
                            // ค้นหาตามประเภทเอกสาร
                            for (int __docType = 1; __docType <= 3; __docType++)
                            {
                                string __docNoPack = this._icTransRef._getDocRefPackForQuery(__docType);
                                if (__docNoPack.Length > 0)
                                {
                                    string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString();
                                    __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","
                                        + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._sum_amount + "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._set_ref_line + "," + _g.d.ic_trans_detail._item_code_main + "," + _g.d.ic_trans_detail._ref_guid + "," + _g.d.ic_trans_detail._is_permium + "  from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                    __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                    for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                    {
                                        __foundRef = true;
                                        string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                        string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                        string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                        string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                        string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                        string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                        string __discount = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                                        decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                        decimal __price = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._price].ToString());
                                        decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                                        int __vatType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._vat_type].ToString());
                                        int __taxType = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._tax_type].ToString());

                                        string __itemCodeMain = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code_main].ToString();

                                        string __refGuid = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._ref_guid].ToString();
                                        string __setRefLine = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._set_ref_line].ToString();
                                        int __is_premium = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._is_permium].ToString());


                                        //
                                        int __addr = this._addRow();
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._doc_ref_type, __docType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __barcode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._price, __price, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._vat_type, __vatType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._tax_type, __taxType, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code_main, __itemCodeMain, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._is_permium, __is_premium, false);

                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_guid, __refGuid, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._set_ref_line, __setRefLine, false);


                                        // this._searchUnitNameWareHouseNameShelfName(__addr);
                                    }
                                    this._searchUnitNameWareHouseNameShelfNameAll();


                                }
                            }
                        }
                        break;
                    #endregion
                    #region ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        {
                            this._clear();
                            //
                            string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);

                            string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";

                            DataTable __getData = __myFrameWork._queryShort("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._barcode, _g.d.ic_trans_detail._unit_code, __balanceQty + " as " + _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._line_number) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'" + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString() + " and " + __balanceQty + "<> 0 order by " + _g.d.ic_trans_detail._line_number).Tables[0];
                            // ปรับปรุงหน่วยนับ เพื่อคำนวณ
                            for (int __row = 0; __row < __getData.Rows.Count; __row++)
                            {
                                int __addr = this._addRow();
                                int __refRow = MyLib._myGlobal._intPhase(__getData.Rows[__row][_g.d.ic_trans_detail._line_number].ToString());

                                this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __getData.Rows[__row][_g.d.ic_trans_detail._item_code].ToString(), true);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __getData.Rows[__row][_g.d.ic_trans_detail._barcode].ToString(), true);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __getData.Rows[__row][_g.d.ic_trans_detail._unit_code].ToString(), true);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, MyLib._myGlobal._decimalPhase(__getData.Rows[__row][_g.d.ic_trans_detail._qty].ToString()), false);

                                this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);

                            }
                        }
                        break;
                    #endregion
                    #region ขาย_สั่งขาย_ยกเลิก
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                        {
                            this._clear();
                            //
                            string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);

                            string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                            string __getWh_code = _g.d.ic_trans_detail._wh_code;
                            string __getShelf_code = _g.d.ic_trans_detail._shelf_code;
                            __wareHouseAndShelfCodeField = "wh_temp";
                            __query = "select * from ( select line_number,  " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","

                                       + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._ref_guid + "," + _g.d.ic_trans_detail._ref_row + ","
                                       + _g.d.ic_trans_detail._item_code_main + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._is_get_price + ","
                                       + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._set_ref_price + "," + _g.d.ic_trans_detail._set_ref_line + "," + _g.d.ic_trans_detail._is_serial_number + ","

                                       + __getWh_code + ","
                                       + __getShelf_code + ","
                                       + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + ",(" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ") as " + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._sum_amount + ","
                                       + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField
                                       + " from " + _g.d.ic_trans_detail._table
                                       + " where " + _g.d.ic_trans_detail._doc_no + " in (\'" + __docNo + "\') and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString()
                                       + " ) as temp2 where qty <> 0 "
                                       + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;

                            // __myFrameWork._queryShort("select " + MyLib._myGllbobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._barcode, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._qty) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'" + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString()).Tables[0];

                            DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                            // ปรับปรุงหน่วยนับ เพื่อคำนวณ
                            for (int __row = 0; __row < __getData.Rows.Count; __row++)
                            {
                                int __addr = this._addRow();
                                int __refRow = MyLib._myGlobal._intPhase(__getData.Rows[__row][_g.d.ic_trans_detail._line_number].ToString());
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __getData.Rows[__row][_g.d.ic_trans_detail._item_code].ToString(), true);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, __getData.Rows[__row][_g.d.ic_trans_detail._barcode].ToString(), true);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __getData.Rows[__row][_g.d.ic_trans_detail._unit_code].ToString(), true);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, MyLib._myGlobal._decimalPhase(__getData.Rows[__row][_g.d.ic_trans_detail._qty].ToString()), false);
                                this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __refRow, false);
                            }
                        }
                        break;
                    #endregion

                    #endregion

                    #region เจ้าหนี้

                    #region เจ้าหนี้_ลดหนี้อื่น เจ้าหนี้_เพิ่มหนี้อื่น
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                        {
                            this._clear();
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(1);
                            if (__docNoPack.Length > 0)
                            {
                                string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น).ToString();
                                __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._sum_amount) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __remark = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._remark].ToString();
                                    decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                                    //
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._remark, __remark, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                    __foundRef = true;
                                }
                                this.Invalidate();
                            }
                        }
                        break;
                    #endregion

                    #endregion

                    #region ลูกหนี้

                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                        {
                            this._clear();
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น).ToString();
                                __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._sum_amount) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __remark = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._remark].ToString();
                                    decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                                    //
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._remark, __remark, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                    __foundRef = true;
                                }
                                this.Invalidate();
                            }
                        }
                        break;

                    #endregion

                    #region เงินสด/ธนาคาร
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        {
                            this._clear();
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(0);
                            if (__docNoPack.Length > 0)
                            {
                                string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString();
                                __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._sum_amount) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __remark = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._remark].ToString();
                                    decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                                    //
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._remark, __remark, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                    __foundRef = true;
                                }
                                this.Invalidate();
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                        {
                            this._clear();
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(0);
                            if (__docNoPack.Length > 0)
                            {
                                string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString();
                                __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._sum_amount) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __remark = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._remark].ToString();
                                    decimal __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString());
                                    //
                                    int __addr = this._addRow();
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._remark, __remark, false);
                                    this._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, __amount, false);
                                    __foundRef = true;
                                }
                                this.Invalidate();
                            }
                        }
                        break;
                    #endregion

                    #region คลัง

                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                string __returnDepositQuery = "coalesce((select sum(ref2.qty*(ref2.stand_value/ref2.divide_value)) from ic_wms_trans_detail as ref2 where ref1.doc_no = ref2.ref_doc_no and ref1.item_code = ref2.item_code and ref1.unit_code = ref2.unit_code and ref1.wh_code = ref2.wh_code and ref1.shelf_code = ref2.shelf_code and ref2.trans_flag = 523 and ref2.ref_row = ref1.line_number ) ,0)";

                                string __depositQuery = "coalesce((select sum( qty*(ref1.stand_value/ref1.divide_value) -" + __returnDepositQuery + " ) from ic_wms_trans_detail as ref1 where trans_flag = 522 and ref1.ref_doc_no = ic_wms_trans_detail.doc_no and ref1.item_code = ic_wms_trans_detail.item_code and ref1.unit_code = ic_wms_trans_detail.unit_code and ref1.wh_code = ic_wms_trans_detail.wh_code and ref1.shelf_code = ic_wms_trans_detail.shelf_code ), 0)";

                                /*__query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + ",((qty*(stand_value/divide_value)) - " + __depositQuery + ")/(stand_value*divide_value) as " + _g.d.ic_trans_detail._qty
                                    + " from ( "

                                    + " select doc_no, item_code, item_name, unit_code, wh_code, shelf_code, sum(qty) as qty, stand_value, divide_value "
                                    + " from ic_wms_trans_detail "
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก).ToString()
                                    + " group by doc_no, item_code, item_name, unit_code, wh_code, shelf_code, stand_value,divide_value "
                                    + ") as "

                                    + _g.d.ic_wms_trans_detail._table
                                    + " order by " + _g.d.ic_trans_detail._doc_no;*/

                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._doc_no + ",(select name_1 from ic_inventory where ic_inventory.code = item_code) as " + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + ",((qty*(stand_value/divide_value)) - " + __depositQuery + ")/(stand_value*divide_value) as " + _g.d.ic_trans_detail._qty
                                    + " from ( "

                                    + " select doc_no, item_code, unit_code, wh_code, shelf_code, sum(qty) as qty, stand_value, divide_value "
                                    + " from ic_wms_trans_detail "
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก).ToString()
                                    + " group by doc_no, item_code, unit_code, wh_code, shelf_code, stand_value,divide_value "
                                    + ") as "

                                    + _g.d.ic_wms_trans_detail._table
                                    + " order by " + _g.d.ic_trans_detail._doc_no;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    // string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());

                                    if (__qty != 0)
                                    {
                                        int __addr = this._addRow();
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, true);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._total_qty, __qty, false);
                                        this._icTransScreenTop._enabedControl(_g.d.ic_trans._cust_code, false);
                                    }
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();

                            }
                        }
                        break;

                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                        {
                            this._clear();
                            //
                            string __docNoPack = this._icTransRef._getDocRefPackForQuery(-1);
                            if (__docNoPack.Length > 0)
                            {
                                __query = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._wh_code + ","
                                    + _g.d.ic_trans_detail._shelf_code + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._line_number
                                    + " from " + _g.d.ic_wms_trans_detail._table
                                    + " where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoPack + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก).ToString()
                                    + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;
                                __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                                {
                                    __foundRef = true;
                                    string __itemCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_code].ToString();
                                    string __itemName = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._item_name].ToString();
                                    string __barcode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._barcode].ToString();
                                    string __docNo = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                                    string __unitCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._unit_code].ToString();
                                    string __wareHouseCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._wh_code].ToString();
                                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString();
                                    decimal __qty = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                                    int __lineNumber = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[__row][_g.d.ic_trans_detail._line_number].ToString());

                                    if (__qty > 0)
                                    {

                                        int __addr = this._addRow();
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_doc_no, __docNo, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, true);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __wareHouseCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._total_qty, __qty, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                                        this._cellUpdate(__addr, _g.d.ic_trans_detail._ref_row, __lineNumber, true);
                                        this._icTransScreenTop._enabedControl(_g.d.ic_trans._cust_code, false);
                                    }
                                }
                                this._searchUnitNameWareHouseNameShelfNameAll();

                            }
                        }
                        break;

                        #endregion
                }
                if (__foundRef)
                {
                    this._icTransRefTemp._refCheckStatus = true;
                    this._icTransRefTemp._refCheck.Enabled = false;
                    _visableRefColumn(this._icTransRefTemp._refCheckStatus);

                    // toe update doc_ref และ doc_ref_date
                    // ยกเลิก function นี้
                    /*
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                            {
                                // toe กลับไป updae doc_ref
                                DateTime __docDate = this._icTransRef._getDocDateRef();
                                string __docRefPack = this._icTransRef._getDocRef();
                                this._icTransScreenTop._setDataStr(_g.d.ic_trans._doc_ref, __docRefPack);
                                this._icTransScreenTop._setDataDate(_g.d.ic_trans._doc_ref_date, __docDate);
                            }
                            break;
                    }*/

                }
                if (this._reCalc != null)
                {
                    this._reCalc();
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        public void _searchItemRenew()
        {

            this._searchItem = new SMLERPControl._searchItemForm(true);
            this._searchItem._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
            this._searchItem._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
            this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._searchItem._selected += new SMLERPControl._searchItemForm.SelectedHandler(_searchItem__selected);
            this._searchItem._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);

        }

        public _icTransItemGridControl()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._searchItemRenew();
                //
                this._searchMaster = new MyLib._searchDataFull();
                this._searchBarcode = new MyLib._searchDataFull();
                this._myFrameWork = new MyLib._myFrameWork();
                this.__searchProperties = new SMLERPGlobal._searchProperties();
                this.__searchMasterList = new ArrayList();
                this._priorityLevelArrayList = new ArrayList();
                this._icTransItemGridChangeDiscount = new _icTransItemGridChangeDiscountForm();
                this._icTransItemGridChangeAmount = new _icTransItemGridChangeAmountForm();
                this._icTransItemGridChangePrice = new _icTransItemGridChangePriceForm();
                this._icTransItemGridChangeQty = new _icTransItemGridChangeQtyForm();
                this._icTransItemGridChangeName = new _icTransItemGridChangeNameForm();
                this._icTransItemGridChangeRemark = new _icTransItemGridChangeNameForm();
                this._icTransItemGridSelectUnit = new SMLERPControl._ic._icTransItemGridSelectUnitForm();
                this._icTransItemGridSelectWareHouse = new _icTransItemGridSelectWareHouseAndShelfForm();
                //
                this._table_name = _g.d.ic_trans_detail._table;
                for (int __loop = 0; __loop < this._priorityLevel.Length; __loop++)
                {
                    string __fieldName = this._table_name + "." + this._priorityLevel[__loop];
                    this._priorityLevelArrayList.Add(MyLib._myResource._findResource(__fieldName, __fieldName)._str);
                }
                // Event พิเศษ
                this._icTransItemGridChangeAmount._submit -= new _submitButtonEventHandler(_icTransItemGridChangeAmount__submit);
                this._icTransItemGridChangeDiscount._submit -= new _submitButtonEventHandler(_icTransItemGridChangeDiscount__submit);
                this._icTransItemGridChangePrice._submit -= new _submitButtonEventHandler(_icTransItemGridChangePrice__submit);
                this._icTransItemGridChangeQty._submit -= new _submitButtonEventHandler(_icTransItemGridChangeQty__submit);
                this._icTransItemGridChangeName._submit -= new _submitButtonEventHandler(_icTransItemGridChangeName__submit);
                this._icTransItemGridChangeRemark._submit -= new _submitButtonEventHandler(_icTransItemGridChangeRemark__submit);
                this._icTransItemGridSelectUnit._selectUnitCode -= new SMLERPControl._ic._icTransItemGridSelectUnitForm._selectUnitCodeEventHandler(_icTransItemGridSelectUnit__selectUnitCode);
                this._icTransItemGridSelectWareHouse._selectWareHouseAndShelf -= new _icTransItemGridSelectWareHouseAndShelfForm._selectWareHouseAndShelfEventHandler(_icTransItemGridSelectWareHouse__selectWareHouseAndShelf);
                //
                this._icTransItemGridChangeAmount._submit += new _submitButtonEventHandler(_icTransItemGridChangeAmount__submit);
                this._icTransItemGridChangeDiscount._submit += new _submitButtonEventHandler(_icTransItemGridChangeDiscount__submit);
                this._icTransItemGridChangePrice._submit += new _submitButtonEventHandler(_icTransItemGridChangePrice__submit);
                this._icTransItemGridChangeQty._submit += new _submitButtonEventHandler(_icTransItemGridChangeQty__submit);
                this._icTransItemGridChangeName._submit += new _submitButtonEventHandler(_icTransItemGridChangeName__submit);
                this._icTransItemGridChangeRemark._submit += new _submitButtonEventHandler(_icTransItemGridChangeRemark__submit);

                this._icTransItemGridSelectUnit._selectUnitCode += new SMLERPControl._ic._icTransItemGridSelectUnitForm._selectUnitCodeEventHandler(_icTransItemGridSelectUnit__selectUnitCode);
                this._icTransItemGridSelectWareHouse._selectWareHouseAndShelf += new _icTransItemGridSelectWareHouseAndShelfForm._selectWareHouseAndShelfEventHandler(_icTransItemGridSelectWareHouse__selectWareHouseAndShelf);
            }
            // this.__build();
        }

        private void _icTransItemGridChangeRemark__submit(int mode)
        {
            if (mode == 1)
            {
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._remark, this._icTransItemGridChangeRemark._nameTextBox.Text, false);
            }
            this.Invalidate();
            this._inputCell(this._selectRow, this._selectColumn);
        }

        private void _icTransItemGridChangeAmount__submit(int mode)
        {
            // แก้ไขมูลค่า
            if (mode == 1)
            {
                decimal __value = 0;
                try
                {
                    __value = MyLib._myGlobal._decimalPhase(this._icTransItemGridChangeAmount._nameNumberBox.textBox.Text);
                }
                catch
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ตัวเลขผิดพลาด"));
                }
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._sum_amount, __value, false);
                decimal __qtyValue = MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._qty).ToString());
                decimal __valuePrice = (__qtyValue == 0) ? 0 : MyLib._myGlobal._round(__value / __qtyValue, _g.g._companyProfile._item_price_decimal);
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._set_ref_price, __valuePrice, false);
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._price, __valuePrice, false);
            }
            this.Invalidate();
            this._inputCell(this._selectRow, this._selectColumn);
        }

        private void _icTransItemGridChangeDiscount__submit(int mode)
        {
            // แก้ไขส่วนลด
            if (mode == 1)
            {
                string __discount_word = this._icTransItemGridChangeDiscount._nameTextBox.textBox.Text;

                int __itemTypeChange = (int)this._cellGet(this.SelectRow, _g.d.ic_trans_detail._item_type);
                if (__itemTypeChange == 3)
                {
                    // กรณี สินค้าชุด
                    if (_g.g._companyProfile._fix_item_set_price == true)
                    {
                        string __ref_guid = (string)this._cellGet(this._selectRow, _g.d.ic_trans_detail._ref_guid);
                        if (__discount_word.IndexOf("%") != -1 && __discount_word.IndexOf(",") == -1)
                        {
                            // กรณี เป็น % เอาไปใส่ set ทุดตัว

                            for (int __row = 0; __row < this._rowData.Count; __row++)
                            {
                                // check ref guid
                                string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                                if (__set_ref_guid.Equals(__ref_guid))
                                {
                                    this._cellUpdate(__row, _g.d.ic_trans_detail._discount, __discount_word, true);
                                    _calcItemSetNow(__row);
                                }
                            }
                        }
                        else
                        {
                            // หายอดส่วนลด
                            decimal __qty = (decimal)this._cellGet(this._selectRow, _g.d.ic_trans_detail._qty);
                            decimal __price = (decimal)this._cellGet(this._selectRow, _g.d.ic_trans_detail._price);
                            decimal __amount = MyLib._myGlobal._round(__qty * __price, _g.g._companyProfile._item_amount_decimal);
                            decimal __newAmount = MyLib._myGlobal._calcAfterDiscount(__discount_word, __amount, _g.g._companyProfile._item_amount_decimal);

                            decimal __discount_amount = __amount - __newAmount;

                            // คำณวนส่วนลดเฉลี่ยไปหาตัวลูกทุกตัว
                            for (int __row = 0; __row < this._rowData.Count; __row++)
                            {
                                // check ref guid
                                string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                                if (__set_ref_guid.Equals(__ref_guid))
                                {
                                    decimal __sum_amount_set = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._sum_amount);
                                    decimal __ratio = MyLib._myGlobal._round((__sum_amount_set / __amount) * 100, 2);  // (decimal)this._cellGet(__row, _g.d.ic_trans_detail._price_set_ratio);

                                    decimal __discount = MyLib._myGlobal._round(((__ratio * __discount_amount) / 100), 2);// MyLib._myGlobal._round((__ratio * (decimal)this._cellGet(__row, _g.d.ic_trans_detail._set_ref_qty)) * __discount_amount, _g.g._companyProfile._item_amount_decimal);

                                    this._cellUpdate(__row, _g.d.ic_trans_detail._discount, __discount.ToString(), true);
                                    _calcItemSetNow(__row);

                                }
                            }

                            // check sum price

                            decimal __sumDiscount = 0M;
                            for (int __row = 0; __row < this._rowData.Count; __row++)
                            {
                                // check ref guid
                                string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                                if (__set_ref_guid.Equals(__ref_guid))
                                {
                                    decimal __discount = MyLib._myGlobal._decimalPhase(this._cellGet(__row, _g.d.ic_trans_detail._discount).ToString());
                                    __sumDiscount += __discount;
                                }
                            }

                            if (__sumDiscount != __discount_amount)
                            {
                                decimal __diff_value = __discount_amount - __sumDiscount;
                                for (int __row = this._rowData.Count; __row > 0; __row--)
                                {
                                    // check ref guid
                                    string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                                    if (__set_ref_guid.Equals(__ref_guid))
                                    {
                                        decimal __newPrice = MyLib._myGlobal._decimalPhase(this._cellGet(__row, _g.d.ic_trans_detail._discount).ToString());
                                        __newPrice += __diff_value;

                                        //this._cellUpdate(__row, _g.d.ic_trans_detail._set_ref_price, __newPrice, false);
                                        this._cellUpdate(__row, _g.d.ic_trans_detail._discount, __newPrice.ToString(), true);

                                        break;
                                    }
                                }

                            }


                        }
                    }
                }
                else
                {
                    this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._discount, __discount_word, true);
                    _calcItemSetNow(this._selectRow);
                }
            }
            this.Invalidate();
            this._inputCell(this._selectRow, this._selectColumn);
        }

        private void _icTransItemGridChangeQty__submit(int mode)
        {
            // แก้ไขจำนวน
            if (mode == 1)
            {
                decimal __value = 0;
                try
                {
                    __value = MyLib._myGlobal._decimalPhase(this._icTransItemGridChangeQty._nameNumberBox.textBox.Text);
                }
                catch
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ตัวเลขผิดพลาด"));
                }
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._qty, __value, true);
            }
            this.Invalidate();
        }

        private void _icTransItemGridChangePrice__submit(int mode)
        {
            // แก้ไขราคา
            if (mode == 1)
            {
                decimal __value = 0;
                try
                {
                    __value = MyLib._myGlobal._decimalPhase(this._icTransItemGridChangePrice._nameNumberBox.textBox.Text);
                }
                catch
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ตัวเลขผิดพลาด"));
                }
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._set_ref_price, __value, false);
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._price, __value, true);
                int __itemTypeChange = (int)this._cellGet(this.SelectRow, _g.d.ic_trans_detail._item_type);

                // กรณีแก้ไข ราคาสินค้าชุด
                if (_g.g._companyProfile._fix_item_set_price == true && __itemTypeChange == 3)
                {
                    // คำณวนอัตราส่วน ราคาไปที่ตัวลูก
                    string __ref_guid = (string)this._cellGet(this._selectRow, _g.d.ic_trans_detail._ref_guid);

                    for (int __row = 0; __row < this._rowData.Count; __row++)
                    {
                        // check ref guid
                        string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                        if (__set_ref_guid.Equals(__ref_guid))
                        {
                            decimal __ratio = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._price_set_ratio);
                            decimal __newPrice = __ratio * __value;
                            this._cellUpdate(__row, _g.d.ic_trans_detail._set_ref_price, __newPrice, false);
                            this._cellUpdate(__row, _g.d.ic_trans_detail._price, __newPrice, true);

                        }
                    }

                    // check sum price

                    decimal __sumPrice = 0M;
                    for (int __row = 0; __row < this._rowData.Count; __row++)
                    {
                        // check ref guid
                        string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                        if (__set_ref_guid.Equals(__ref_guid))
                        {
                            decimal __price = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._price) * (decimal)this._cellGet(__row, _g.d.ic_trans_detail._set_ref_qty);
                            __sumPrice += __price;
                        }
                    }

                    if (__sumPrice != __value)
                    {
                        decimal __diff_value = __value - __sumPrice;
                        for (int __row = this._rowData.Count; __row > 0; __row--)
                        {
                            // check ref guid
                            string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                            if (__set_ref_guid.Equals(__ref_guid))
                            {
                                decimal __newPrice = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._price);
                                __newPrice += __diff_value;

                                this._cellUpdate(__row, _g.d.ic_trans_detail._set_ref_price, __newPrice, false);
                                this._cellUpdate(__row, _g.d.ic_trans_detail._price, __newPrice, true);

                                break;
                            }
                        }

                    }
                }
                else
                {
                    // คำนวณยอดรวท กรณีสินค้าชุด
                    int __rowFirst = this._findFirstRow(this._selectRow);
                    if (__rowFirst != -1)
                    {
                        int __priceColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._price);
                        int __totalColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._sum_amount);
                        int __discountColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._discount);
                        int __averageCostColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._average_cost);
                        int __qtyColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._qty);
                        //
                        int __itemType = (int)this._cellGet(__rowFirst, _g.d.ic_trans_detail._item_type);
                        string __discountWord = (__discountColumnNumber == -1) ? "0.0" : this._cellGet(__rowFirst, __discountColumnNumber).ToString();
                        decimal __qty = (decimal)this._cellGet(__rowFirst, __qtyColumnNumber);
                        string __guid = this._cellGet(__rowFirst, _g.d.ic_trans_detail._ref_guid).ToString();
                        //
                        if (__guid.Length > 0)
                        {
                            switch (__itemType)
                            {
                                case 3: // สินค้าจัดชุด
                                    {
                                        decimal __amount = 0;
                                        for (int __refRowLoop = __rowFirst + 1; __refRowLoop < this._rowData.Count; __refRowLoop++)
                                        {
                                            if (this._cellGet(__refRowLoop, _g.d.ic_trans_detail._set_ref_line).ToString().Equals(__guid))
                                            {
                                                decimal __total = (decimal)this._cellGet(__refRowLoop, __totalColumnNumber);
                                                __amount += __total;
                                            }
                                        }
                                        decimal __price = (__qty == 0) ? 0 : MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round((__amount / __qty), _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
                                        this._cellUpdate(__rowFirst, __totalColumnNumber, __amount, false);
                                        this._cellUpdate(__rowFirst, __priceColumnNumber, __price, false);
                                        this.Invalidate();
                                    }
                                    break;
                                case 5: // สูตรสี
                                    {
                                        decimal __amount = 0;
                                        for (int __refRowLoop = __rowFirst + 1; __refRowLoop < this._rowData.Count; __refRowLoop++)
                                        {
                                            if (this._cellGet(__refRowLoop, _g.d.ic_trans_detail._set_ref_line).ToString().Equals(__guid))
                                            {
                                                decimal __total = (decimal)this._cellGet(__refRowLoop, __totalColumnNumber);
                                                __amount += __total;
                                            }
                                        }
                                        decimal __price = (__qty == 0) ? 0 : MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round((__amount / __qty), _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
                                        this._cellUpdate(__rowFirst, __totalColumnNumber, __amount, false);
                                        this._cellUpdate(__rowFirst, __priceColumnNumber, __price, false);
                                        this.Invalidate();
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            this.Invalidate();
            this._inputCell(this._selectRow, this._selectColumn);
        }

        private object[] _ictransItemGridControl__cellComboBoxItem(object sender, int row, int column)
        {
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้: return _g.g._so_sale_order_type_4;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้: return _g.g._so_sale_order_type_3;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ: return _g.g._so_sale_order_type_2;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย: return _g.g._so_sale_order_type_1;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด: return _g.g._ap_bill_type_2;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้: return _g.g._ap_bill_type_4;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด: return _g.g._ap_bill_type_3;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด: return _g.g._ap_bill_type_1;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้: return _g.g._ap_bill_type_4;
            }
            return null;
        }

        private string _ictransItemGridControl__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้: return _g.g._so_sale_order_type_4[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้: return _g.g._so_sale_order_type_3[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ: return _g.g._so_sale_order_type_2[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย: return _g.g._so_sale_order_type_1[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด: return _g.g._ap_bill_type_2[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้: return _g.g._ap_bill_type_4[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด: return _g.g._ap_bill_type_3[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด: return _g.g._ap_bill_type_1[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้: return _g.g._ap_bill_type_4[(select == -1) ? 0 : select].ToString();
            }
            return null;
        }

        private bool _ictransItemGridControl__beforeInputCell(MyLib._myGrid sender, int row, int column)
        {
            bool __result = true;
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    if (row != -1 && column != -1 && row < this._rowData.Count)
                    {
                        int __columnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_row);
                        if (__columnNumber != 1)
                        {
                            if ((int)this._cellGet(row, __columnNumber) == -1)
                            {
                                __result = false;
                            }
                        }
                    }
                    if (__result)
                    {
                        // กรณีสินค้าชุด ห้ามแก้ไขราคา (เพราะจะคำนวณจากสินค้าย่อย)
                        int __itemType = (int)this._cellGet(row, _g.d.ic_trans_detail._item_type);
                        if (__itemType == 3)
                        {
                            int __columnNumber = this._findColumnByName(_g.d.ic_trans_detail._price);
                            if (__columnNumber != 1 && column == __columnNumber)
                            {
                                __result = false;
                            }
                        }
                    }
                    break;
            }
            return __result;
        }

        private bool _ictransItemGridControl__totalCheck(object sender, int row, int column)
        {
            bool __result = true;
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    int __columnNumber = this._findColumnByName(_g.d.ic_trans_detail._item_type);
                    if (__columnNumber != 1)
                    {
                        if ((int)this._cellGet(row, __columnNumber) == 3 || (int)this._cellGet(row, __columnNumber) == 5)
                        {
                            __result = false;
                        }
                    }
                    break;
            }
            return __result;
        }

        private void _ictransItemGridControl__afterAddRow(object sender, int row)
        {
            int __columnNumber = this._findColumnByName(_g.d.ic_trans_detail._set_ref_line);
            if (__columnNumber != 1)
            {
                this._cellUpdate(row, _g.d.ic_trans_detail._set_ref_line, "", false);
            }
            int __columnCalcPriceNumber = this._findColumnByName(_g.d.ic_trans_detail._is_get_price);
            if (__columnCalcPriceNumber != 1)
            {
                this._cellUpdate(row, _g.d.ic_trans_detail._is_get_price, 1, false);
            }
            this._cellUpdate(row, _g.d.ic_trans_detail._is_serial_number, 0, false);
            this._cellUpdate(row, this._columnSerialNumber, new _serialNumberStruct(), false);

            int __columnDepartmentNumber = this._findColumnByName(this._columnDepartment);
            if (__columnDepartmentNumber != -1)
            {
                this._cellUpdate(row, this._columnDepartment, new icTransWeightStruct(), false);
            }

            int __columnAllocateNumber = this._findColumnByName(this._columnAlloCate);
            if (__columnAllocateNumber != -1)
            {
                this._cellUpdate(row, this._columnAlloCate, new icTransWeightStruct(), false);
            }

            int __columnProjectNumber = this._findColumnByName(this._columnProject);
            if (__columnProjectNumber != -1)
            {
                this._cellUpdate(row, this._columnProject, new icTransWeightStruct(), false);
            }

            int __columnSiteNumber = this._findColumnByName(this._columnSideList);
            if (__columnSiteNumber != -1)
            {
                this._cellUpdate(row, this._columnSideList, new icTransWeightStruct(), false);
            }

            int __columnJobsNumber = this._findColumnByName(this._columnJobsList);
            if (__columnJobsNumber != -1)
            {
                this._cellUpdate(row, this._columnJobsList, new icTransWeightStruct(), false);
            }

            /*switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    {
                        if (this._icTransScreenTop._getDataStr(_g.d.ic_trans._pass_book_code).Length > 0)
                        {
                            this._cellUpdate(row, _g.d.ic_trans_detail._item_code, this._icTransScreenTop._getDataStr(_g.d.ic_trans._pass_book_code), false);
                        }
                    }
                    break;
            }*/

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้)
            {
                if (this._icTransRef._transGrid._rowData.Count > 0)
                {
                    string __refDocNo = this._icTransRef._transGrid._cellGet(0, _g.d.ap_ar_trans_detail._billing_no).ToString();
                    int __refBillType = MyLib._myGlobal._intPhase(this._icTransRef._transGrid._cellGet(0, _g.d.ap_ar_trans_detail._bill_type).ToString());

                    if (__refBillType == 2 && __refDocNo.Length > 0)
                    {
                        this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref_type, __refBillType, false);
                        this._cellUpdate(row, _g.d.ic_trans_detail._ref_doc_no, __refDocNo, true);
                        this.Invalidate();
                    }

                }
            }
        }

        private ArrayList _docRef()
        {
            ArrayList __result = new ArrayList();
            __result.Add("<ไม่อ้างอิง>");
            for (int __row = 0; __row < this._icTransRef._transGrid._rowData.Count; __row++)
            {
                string __invoiceNumnber = this._icTransRef._transGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString().ToUpper();
                if (__invoiceNumnber.Length > 0)
                {
                    __result.Add(__invoiceNumnber);
                }
            }
            return __result;
        }

        protected void _historyShow(SMLInventoryControl._transHistoryType historyType)
        {
            Form __saleHistoryForm = new Form();
            __saleHistoryForm.WindowState = FormWindowState.Maximized;
            SMLInventoryControl._transHistory __saleHistory = new SMLInventoryControl._transHistory(historyType, false);
            __saleHistory.Dock = DockStyle.Fill;
            __saleHistoryForm.Controls.Add(__saleHistory);
            string __getCustCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code).ToString();
            string __getItemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString();
            __saleHistory._condition._setDataStr(_g.d.ic_resource._cust_code_begin, __getCustCode);
            __saleHistory._condition._setDataStr(_g.d.ic_resource._cust_code_end, __getCustCode);
            __saleHistory._condition._setDataStr(_g.d.ic_resource._ic_code_begin, __getItemCode);
            __saleHistory._condition._setDataStr(_g.d.ic_resource._ic_code_end, __getItemCode);
            __saleHistory._condition.Enabled = false;
            __saleHistory._toolStrip.Visible = false;
            __saleHistory._process();
            __saleHistoryForm.ShowDialog();
            __saleHistoryForm.Dispose();
        }

        /// <summary>
        /// ตรวจสินค้ามีหลายเลขเครื่อง
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Boolean _isSerial(int row)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __itemCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().Trim().ToUpper();
            string __query = "select coalesce(" + _g.d.ic_inventory._ic_serial_no + ",0) as " + _g.d.ic_inventory._ic_serial_no + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'";
            DataTable __data = __myFrameWork._queryShort(__query).Tables[0];
            if (__data.Rows.Count > 0)
            {
                int __isSerial = (int)MyLib._myGlobal._decimalPhase((__data.Rows[0][_g.d.ic_inventory._ic_serial_no].ToString()));
                if (__isSerial != 1)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("สินค้านี้ไม่มีการบันทึกหมายเลขเครื่อง"));
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Control | Keys.D1:
                        if (this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้ ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้ ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้ ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด)
                        {
                            _historyShow(SMLInventoryControl._transHistoryType.ประวัติการซื้อ);
                        }
                        else
                            if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                        {
                            _historyShow(SMLInventoryControl._transHistoryType.ประวัติการขาย);
                        }
                        return true;
                    case Keys.Control | Keys.D2:
                        try
                        {
                            if (this._itemInfo != null)
                            {
                                string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString();
                                this._itemInfo(__itemCode);
                            }
                        }
                        catch
                        {
                        }
                        return true;
                    case Keys.Control | Keys.D3:
                        // แสดงตารางราคาขาย
                        try
                        {
                            if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                            {
                                if (_g.g._companyProfile._hidden_price_formula == false)
                                {
                                    SMLERPControl._ic._priceFormulaInfoForm __priceInfo = new SMLERPControl._ic._priceFormulaInfoForm(this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString(), MyLib._myGlobal._intPhase((this._cellGet(this._selectRow, this._columnPriceRoworder).ToString())), this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code).ToString());
                                    __priceInfo.ShowDialog();
                                    __priceInfo.Dispose();
                                }
                            }
                        }
                        catch
                        {
                        }
                        return true;
                    case Keys.Control | Keys.D4:
                        // แสดงราคาขาย
                        try
                        {
                            if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                            {
                                _priceInfoForm __priceInfo = new _priceInfoForm(this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString(), MyLib._myGlobal._intPhase((this._cellGet(this._selectRow, this._columnPriceRoworder).ToString())), this._icTransScreenTop._getDataStr(_g.d.ic_trans._cust_code).ToString());
                                __priceInfo.ShowDialog();
                                __priceInfo.Dispose();
                            }
                        }
                        catch
                        {
                        }
                        return true;
                    case Keys.Control | Keys.D5:
                        if (this._findColumnByName(_g.d.ic_trans_detail._barcode) != -1)
                        {
                            try
                            {
                                _icTransGridItemAutoForm __itemAuto = new _icTransGridItemAutoForm();
                                __itemAuto._enterKey += new _icTransGridItemAutoForm.EnterKeyHandler(__itemAuto__enterKey);
                                __itemAuto.ShowDialog();
                            }
                            catch
                            {
                            }
                        }
                        return true;
                    case Keys.Control | Keys.D6:
                        try
                        {
                            if (this._itemReplacement != null)
                            {
                                string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString();
                                this._itemReplacement(__itemCode);
                            }
                        }
                        catch
                        {
                        }
                        return true;
                    case Keys.Control | Keys.D8:
                        {
                            try
                            {
                                // Lot
                                if (this._lot != null)
                                {
                                    this._lot(true);
                                }
                            }
                            catch
                            {
                            }
                            return true;
                        }
                    case Keys.Control | Keys.D9:
                        {
                            string __itemCode2 = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString();
                            decimal __qty2 = MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._qty).ToString());
                            _formulaForm __formula = new _formulaForm(__itemCode2, __qty2);
                            __formula.ShowDialog();
                            return true;
                        }
                    case Keys.Control | Keys.F:
                        {
                            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            {
                                if (this._selectRow != -1 && this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString().Length > 0)
                                {
                                    this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._is_permium, 1, true);
                                    this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._price, 0M, true);
                                }
                                return true;
                            }
                            return false;
                        }
                    case Keys.F1:
                        this._lastControl = null;
                        if (this._findColumnByName(_g.d.ic_trans_detail._barcode) != -1)
                        {
                            this._searchBarCodeDialog(this._selectRow);
                        }
                        return true;
                    case Keys.F2:
                        this._lastControl = null;
                        this._searchItemDialog(_g.d.ic_trans_detail._item_code, this._selectRow);
                        return true;
                    case Keys.F3:
                        _changeProductName();
                        return true;
                    case Keys.F4:
                        if (this._findColumnByName(_g.d.ic_trans_detail._wh_code) != -1)
                        {
                            _selectWareHouseAndShelf(0);
                        }
                        return true;
                    case Keys.F5:
                        _selectUnitCode();
                        return true;
                    case Keys.F6:
                        if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก ||
                            this._ictransControlTemp == _g.g._transControlTypeEnum.สินค้า_ขอโอน)
                        {
                            _selectWareHouseAndShelf(1);
                        }
                        return true;
                    case Keys.F7:
                        {
                            if (this._isSerialNumber)
                            {
                                if (this._isSerial(this._selectRow))
                                {
                                    this._removeLastControl();
                                    string __refDocNo = "";
                                    if (this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no) != -1)
                                    {
                                        __refDocNo = this._cellGet(this._selectRow, _g.d.ic_trans_detail._ref_doc_no).ToString();
                                    }
                                    //
                                    string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString();
                                    string __itemName = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name).ToString();
                                    int __maxRow = (int)MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._qty).ToString());
                                    this._serialNumberForm = new _icTransSerialNumberDockForm(this._docNo(), this._docNoOld(), __refDocNo, __itemCode, __itemName, this._selectRow, this._icTransControlType, __maxRow);
                                    this._serialNumberForm._saveData += new _icTransSerialNumberDockForm.SaveDataEventHandler(_serialNumberForm__saveData);
                                    try
                                    {
                                        this._serialNumberForm._serialNumber._loadData((_serialNumberStruct)this._cellGet(this._selectRow, this._columnSerialNumber));
                                    }
                                    catch
                                    {
                                    }
                                    this._serialNumberForm._serialNumber._checkSerialNumber += new _icTransSerialNumberForm.CheckSerialNumberEventHandler(_serialNumberForm__checkSerialNumber);
                                    this._serialNumberForm._serialNumber._price += new _icTransSerialNumberForm.PriceEventHandler(_serialNumberForm__price);
                                    // toe
                                    this._serialNumberForm._serialNumber._getWarehouse += new _icTransSerialNumberForm.WareHouseEventHandler(_serialNumber__getWarehouse);
                                    this._serialNumberForm._serialNumber._getLocation += new _icTransSerialNumberForm.LocationEventHandler(_serialNumber__getLocation);
                                    this._serialNumberForm.ShowDialog();
                                    // Focus
                                    this._gotoCell(this._selectRow, this._selectColumn);
                                }
                            }
                        }
                        return true;
                    case Keys.F8:
                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                _changeProductPrice();
                                break;
                        }
                        return true;
                    case Keys.F9:
                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                _changeProductDiscount();
                                break;
                        }
                        return true;
                    case Keys.F10:
                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                _changeProductQty();
                                break;
                        }
                        return true;
                    case Keys.Control | Keys.F4:
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            string __defaultWareHouse = this._cellGet(0, _g.d.ic_trans_detail._wh_code).ToString().Trim().ToUpper().Split('~')[0].ToString();
                            string __defaultShelf = this._cellGet(0, _g.d.ic_trans_detail._shelf_code).ToString().Trim().ToUpper().Split('~')[0].ToString();
                            string __defaultWareHouse2 = "";
                            string __defaultShelf2 = "";
                            if (this._findColumnByName(_g.d.ic_trans_detail._wh_code_2) != -1)
                            {
                                __defaultWareHouse2 = this._cellGet(0, _g.d.ic_trans_detail._wh_code_2).ToString().Trim().ToUpper().Split('~')[0].ToString();
                                __defaultShelf2 = this._cellGet(0, _g.d.ic_trans_detail._shelf_code_2).ToString().Trim().ToUpper().Split('~')[0].ToString(); ;
                            }
                            //
                            DataTable __dataWareHouseName = __myFrameWork._queryShort("select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + "=\'" + __defaultWareHouse + "\'").Tables[0];
                            string __defaultWareHouseName = (__dataWareHouseName.Rows.Count == 0) ? "" : __dataWareHouseName.Rows[0][0].ToString();
                            //
                            DataTable __dataShelfName = __myFrameWork._queryShort("select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._whcode + "=\'" + __defaultWareHouse + "\' and " + _g.d.ic_shelf._code + "=\'" + __defaultShelf + "\'").Tables[0];
                            string __defaultShelfName = (__dataShelfName.Rows.Count == 0) ? "" : __dataShelfName.Rows[0][0].ToString(); ;
                            //
                            DialogResult __result = MessageBox.Show("Auto Warehouse and location all (WareHouse : " + __defaultWareHouse + ",Shelf : " + __defaultShelf + ")", "Auto", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                            if (__result == DialogResult.Yes)
                            {
                                StringBuilder __itemCodeList = new StringBuilder();
                                for (int __loop = 1; __loop < this._rowData.Count; __loop++)
                                {
                                    this._cellUpdate(__loop, _g.d.ic_trans_detail._wh_code, __defaultWareHouse, false);
                                    this._cellUpdate(__loop, _g.d.ic_trans_detail._shelf_code, __defaultShelf, false);
                                    if (this._findColumnByName(_g.d.ic_trans_detail._wh_code_2) != -1)
                                    {
                                        this._cellUpdate(__loop, _g.d.ic_trans_detail._wh_code_2, __defaultWareHouse2, false);
                                        this._cellUpdate(__loop, _g.d.ic_trans_detail._shelf_code_2, __defaultShelf2, false);
                                    }
                                }
                            }
                        }
                        return true;
                    case Keys.Control | Keys.F5:
                        if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา && MyLib._myGlobal._OEMVersion.Equals("SINGHA")))
                        {
                            // ค้นหาจาก level
                            //MessageBox.Show("ค้นหาจาก level");
                            Form __levelForm = new Form();
                            __levelForm.WindowState = FormWindowState.Maximized;
                            SMLInventoryControl._itemSearchLevelControl __control = new SMLInventoryControl._itemSearchLevelControl(false);
                            __control._menuItemClick += (menuSender, menuEvent) =>
                            {

                                SMLInventoryControl._itemSearchLevelMenuControl __obj = (SMLInventoryControl._itemSearchLevelMenuControl)menuSender;

                                __levelForm.Close();
                                string __itemCode = __obj._itemCode;

                                // checkrowdata
                                int __newRow = (this._selectRow > this._rowData.Count || this._selectRow == -1) ? this._addRow() : this._selectRow;

                                this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_code, __itemCode, true);
                                SendKeys.Send("{TAB}");

                            };
                            __control.Dock = DockStyle.Fill;
                            __levelForm.Controls.Add(__control);
                            __levelForm.ShowDialog();
                        }
                        return true;
                    case Keys.Control | Keys.F8:
                        // แก้ไขมูลค่า
                        try
                        {
                            this._changeProductAmount();
                        }
                        catch
                        {
                        }
                        break;
                    case Keys.Control | Keys.F1:
                        {
                            // weight แผนก
                            if (_g.g._companyProfile._use_department == 1)
                            {
                                // get value
                                switch (this._icTransControlType)
                                {
                                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                        break;
                                    default:
                                        {
                                            string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "~" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name);
                                            decimal __amount = (this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount) == null) ? 0M : MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount).ToString());
                                            _icTransWeightControl __weightControl = new _icTransWeightControl(_g.g.ProjectAllowcateEnum.แผนก, __itemCode, __amount);
                                            __weightControl._saveData -= new _icTransWeightControl.SaveDataEventHandler(__departmentWeightControl__saveData);
                                            __weightControl._saveData += new _icTransWeightControl.SaveDataEventHandler(__departmentWeightControl__saveData);
                                            // load
                                            try
                                            {
                                                __weightControl._loadData((icTransWeightStruct)this._cellGet(this._selectRow, this._columnDepartment));
                                            }
                                            catch
                                            {
                                            }

                                            __weightControl.ShowDialog();
                                        }
                                        break;

                                }

                            }
                        }
                        return true;
                    case Keys.Control | Keys.F2:
                        {
                            if (_g.g._companyProfile._use_job == 1)
                            {
                                // weight โครงการ
                                // get value
                                switch (this._icTransControlType)
                                {
                                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                        break;
                                    default:
                                        {
                                            string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "~" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name);
                                            decimal __amount = (this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount) == null) ? 0M : MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount).ToString());
                                            _icTransWeightControl __weightControl = new _icTransWeightControl(_g.g.ProjectAllowcateEnum.โครงการ, __itemCode, __amount);
                                            __weightControl._saveData -= new _icTransWeightControl.SaveDataEventHandler(__projectWeightControl__saveData);
                                            __weightControl._saveData += new _icTransWeightControl.SaveDataEventHandler(__projectWeightControl__saveData);
                                            try
                                            {
                                                __weightControl._loadData((icTransWeightStruct)this._cellGet(this._selectRow, this._columnProject));
                                            }
                                            catch
                                            {
                                            }

                                            __weightControl.ShowDialog();
                                        }
                                        break;
                                }
                            }
                            return true;
                        }
                    case Keys.Control | Keys.F3:
                        {
                            if (_g.g._companyProfile._use_allocate == 1)
                            {
                                // weight การจัดสรร
                                switch (this._icTransControlType)
                                {
                                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                        break;
                                    default:
                                        {
                                            string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "~" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name);
                                            decimal __amount = (this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount) == null) ? 0M : MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount).ToString());
                                            _icTransWeightControl __weightControl = new _icTransWeightControl(_g.g.ProjectAllowcateEnum.การจัดสรร, __itemCode, __amount);
                                            __weightControl._saveData -= new _icTransWeightControl.SaveDataEventHandler(__allocateweightControl__saveData);
                                            __weightControl._saveData += new _icTransWeightControl.SaveDataEventHandler(__allocateweightControl__saveData);
                                            try
                                            {
                                                __weightControl._loadData((icTransWeightStruct)this._cellGet(this._selectRow, this._columnAlloCate));
                                            }
                                            catch
                                            {
                                            }

                                            __weightControl.ShowDialog();
                                        }
                                        break;
                                }
                            }

                        }
                        return true;
                    case Keys.Control | Keys.F6:
                        {
                            // weight แผนก
                            if (_g.g._companyProfile._use_unit == 1)
                            {
                                // get value
                                switch (this._icTransControlType)
                                {
                                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                        break;
                                    default:
                                        {
                                            string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "~" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name);
                                            decimal __amount = (this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount) == null) ? 0M : MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount).ToString());
                                            _icTransWeightControl __weightControl = new _icTransWeightControl(_g.g.ProjectAllowcateEnum.หน่วยงาน, __itemCode, __amount);
                                            __weightControl._saveData -= new _icTransWeightControl.SaveDataEventHandler(__SiteWeightControl__saveData);
                                            __weightControl._saveData += new _icTransWeightControl.SaveDataEventHandler(__SiteWeightControl__saveData);
                                            // load
                                            try
                                            {
                                                __weightControl._loadData((icTransWeightStruct)this._cellGet(this._selectRow, this._columnSideList));
                                            }
                                            catch
                                            {
                                            }

                                            __weightControl.ShowDialog();
                                        }
                                        break;
                                }
                            }
                        }
                        return true;
                    case Keys.Control | Keys.F7:
                        {
                            // weight แผนก
                            if (_g.g._companyProfile._use_job == 1)
                            {
                                // get value
                                switch (this._icTransControlType)
                                {
                                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                        break;
                                    default:
                                        {
                                            string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "~" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name);
                                            decimal __amount = (this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount) == null) ? 0M : MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount).ToString());
                                            _icTransWeightControl __weightControl = new _icTransWeightControl(_g.g.ProjectAllowcateEnum.งาน, __itemCode, __amount);
                                            __weightControl._saveData -= new _icTransWeightControl.SaveDataEventHandler(__jobsWweightControl__saveData);
                                            __weightControl._saveData += new _icTransWeightControl.SaveDataEventHandler(__jobsWweightControl__saveData);
                                            // load
                                            try
                                            {
                                                __weightControl._loadData((icTransWeightStruct)this._cellGet(this._selectRow, this._columnJobsList));
                                            }
                                            catch
                                            {
                                            }

                                            __weightControl.ShowDialog();
                                        }
                                        break;
                                }
                            }
                        }
                        return true;
                    case Keys.Control | Keys.E:
                        _changeProductRemark();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        bool _saveICTransWeightData(_icTransWeightControl control, int updateColumnIndex)
        {
            icTransWeightStruct __data = new icTransWeightStruct();
            for (int __row = 0; __row < control._grid._rowData.Count; __row++)
            {
                icTransWeightDetailStruct __detail = new icTransWeightDetailStruct();

                __detail._code = control._grid._cellGet(__row, control._columnItemCode).ToString().Trim().ToUpper();
                if (__detail._code.Length > 0)
                {
                    __detail._name = control._grid._cellGet(__row, control._ColumnItemName).ToString().Trim().ToUpper();
                    __detail._ratio = (decimal)control._grid._cellGet(__row, _g.d.ic_trans_detail_department._ratio);
                    __detail._amount = (decimal)control._grid._cellGet(__row, _g.d.ic_trans_detail_department._amount);
                    __data.__details.Add(__detail);
                }
            }
            this._cellUpdate(this._selectRow, updateColumnIndex, __data, false);
            return true;
        }

        bool __departmentWeightControl__saveData(object sender)
        {
            _icTransWeightControl control = (_icTransWeightControl)sender;
            return _saveICTransWeightData(control, this._findColumnByName(this._columnDepartment));
        }

        bool __projectWeightControl__saveData(object sender)
        {
            _icTransWeightControl control = (_icTransWeightControl)sender;
            return _saveICTransWeightData(control, this._findColumnByName(this._columnProject));
        }

        bool __allocateweightControl__saveData(object sender)
        {
            _icTransWeightControl control = (_icTransWeightControl)sender;
            return _saveICTransWeightData(control, this._findColumnByName(this._columnAlloCate));
        }

        bool __SiteWeightControl__saveData(object sender)
        {
            _icTransWeightControl control = (_icTransWeightControl)sender;
            return _saveICTransWeightData(control, this._findColumnByName(this._columnSideList));
        }

        bool __jobsWweightControl__saveData(object sender)
        {
            _icTransWeightControl control = (_icTransWeightControl)sender;
            return _saveICTransWeightData(control, this._findColumnByName(this._columnJobsList));
        }

        private void __itemAuto__enterKey(string code, decimal qty)
        {
            Boolean __autoWhAndShelf = false;
            string __oldWhCode1 = "";
            string __oldShelfCode1 = "";
            string __oldWhCode2 = "";
            string __oldShelfCode2 = "";

            Boolean __isWeightDigitalBarcode = false;
            decimal __getPriceWeight = 0M;
            string __oldBarcode = code;

            // toe check weight digital barcode
            if (_g.g._companyProfile._digital_barcode_scale == true &&
                       (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS) || (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                       )
            {
                if (code.Length == 13 && this._weight_scale_prefix.Length > 0)
                {
                    // check
                    string __getPrefix = "";
                    string __getBarcode = "";
                    //decimal __getPriceWeight = 0M;

                    try
                    {
                        __getPrefix = code.Substring(this._weight_prefix_start - 1, (this._weight_prefix_stop - this._weight_prefix_start) + 1);
                        __getBarcode = code.Substring(this._weight_ic_code_start - 1, (this._weight_ic_code_stop - this._weight_ic_code_start) + 1);
                        __getPriceWeight = MyLib._myGlobal._decimalPhase(code.Substring(this._weight_price_start - 1, (this._weight_price_stop - this._weight_price_start) + 1));
                    }
                    catch
                    {
                    }

                    if (__getPrefix.Equals(this._weight_scale_prefix) == true)
                    {
                        __isWeightDigitalBarcode = true;
                        code = __getBarcode;
                    }
                }
            }

            // ลบบรรทัดที่ว่าง
            int __addrDelete = 0;
            while (__addrDelete < this._rowData.Count)
            {
                if (this._cellGet(__addrDelete, _g.d.ic_trans_detail._item_code).ToString().Trim().Length == 0 && this._cellGet(__addrDelete, _g.d.ic_trans_detail._barcode).ToString().Trim().Length == 0)
                {
                    this._rowData.RemoveAt(__addrDelete);
                }
                else
                {
                    __addrDelete++;
                }
            }
            if (this._findColumnByName(_g.d.ic_trans_detail._wh_code) != -1)
            {
                // ตรวจดูว่ามีการกำหนดคลังที่เก็บในหัวเอกสารหรือไม่
                string __topWareHouseCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from).ToString().Trim();
                if (__topWareHouseCode.Length > 0)
                {
                    __autoWhAndShelf = true;
                    __oldWhCode1 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from).ToString().Trim();
                    __oldShelfCode1 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_from).ToString().Trim();
                    if (this._findColumnByName(_g.d.ic_trans_detail._wh_code_2) != -1)
                    {
                        __oldWhCode2 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_to).ToString().Trim();
                        __oldShelfCode2 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_to).ToString().Trim();
                    }
                }
                else
                {
                    // เอาคลัง+ที่เกิบเดิมมาเพื่อความเร็วในการบันทึก
                    if (this._rowData.Count > 0)
                    {
                        __autoWhAndShelf = true;
                        __oldWhCode1 = this._cellGet(this._rowData.Count - 1, _g.d.ic_trans_detail._wh_code).ToString();
                        __oldShelfCode1 = this._cellGet(this._rowData.Count - 1, _g.d.ic_trans_detail._shelf_code).ToString();
                        if (this._findColumnByName(_g.d.ic_trans_detail._wh_code_2) != -1)
                        {
                            __oldWhCode2 = this._cellGet(this._rowData.Count - 1, _g.d.ic_trans_detail._wh_code_2).ToString();
                            __oldShelfCode2 = this._cellGet(this._rowData.Count - 1, _g.d.ic_trans_detail._shelf_code_2).ToString();
                        }
                    }
                }
            }

            // รวมอัตโนมัติ ตาม barcode
            Boolean __found = false;
            for (int __row = 0; __row < this._rowData.Count; __row++)
            {
                string __barCode = this._cellGet(__row, _g.d.ic_trans_detail._barcode).ToString();
                if (code.Equals(__barCode))
                {
                    if (__isWeightDigitalBarcode == true)
                    {
                        decimal __oldDiscount = MyLib._myGlobal._decimalPhase(this._cellGet(__row, _g.d.ic_trans_detail._discount).ToString()); //  (Decimal) this._cellGet(__row, _g.d.ic_trans_detail._discount_amount);  // MyLib._myGlobal._decimalPhase(this._cellGet(__row, _g.d.ic_trans_detail._discount_amount).ToString());
                        decimal __getPrice = (Decimal)this._cellGet(__row, _g.d.ic_trans_detail._price);
                        decimal __getUnitWeight = MyLib._myGlobal._round((__getPriceWeight / 100) / __getPrice, _g.g._companyProfile._item_qty_decimal);

                        decimal __discount = (__getPrice * __getUnitWeight) - (__getPriceWeight / 100);


                        decimal __qty = MyLib._myGlobal._decimalPhase(this._cellGet(__row, _g.d.ic_trans_detail._qty).ToString()) + __getUnitWeight;

                        // update disconnt
                        this._cellUpdate(__row, _g.d.ic_trans_detail._qty, __qty, true);
                        this._cellUpdate(__row, _g.d.ic_trans_detail._discount, __discount + __oldDiscount, true);

                    }
                    else
                    {
                        decimal __qty = MyLib._myGlobal._decimalPhase(this._cellGet(__row, _g.d.ic_trans_detail._qty).ToString()) + qty;
                        this._cellUpdate(__row, _g.d.ic_trans_detail._qty, __qty, true);
                        this._gotoCell(__row, this._findColumnByName(_g.d.ic_trans_detail._item_name));
                    }
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {
                int __addr = this._addRow();
                this._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, code, true);
                if (__autoWhAndShelf)
                {
                    this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code, __oldWhCode1, false);
                    this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code, __oldShelfCode1, false);
                    if (this._findColumnByName(_g.d.ic_trans_detail._wh_code_2) != -1)
                    {
                        this._cellUpdate(__addr, _g.d.ic_trans_detail._wh_code_2, __oldWhCode2, false);
                        this._cellUpdate(__addr, _g.d.ic_trans_detail._shelf_code_2, __oldShelfCode2, false);
                    }
                }

                string __itemCode = this._cellGet(__addr, _g.d.ic_trans_detail._item_code).ToString();
                if (__itemCode.Length > 0)
                {
                    this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, qty, true);

                    if (__isWeightDigitalBarcode)
                    {

                        decimal __getPrice = (Decimal)this._cellGet(__addr, _g.d.ic_trans_detail._price);
                        decimal __getUnitWeight = MyLib._myGlobal._round((__getPriceWeight / 100) / __getPrice, _g.g._companyProfile._item_qty_decimal);

                        decimal __discount = (__getPrice * __getUnitWeight) - (__getPriceWeight / 100);

                        // update disconnt
                        this._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __getUnitWeight, true);
                        this._cellUpdate(__addr, _g.d.ic_trans_detail._discount, __discount, true);

                    }
                }
                this._gotoCell(__addr, this._findColumnByName(_g.d.ic_trans_detail._item_name));
            }
        }

        private decimal _serialNumberForm__price()
        {
            if (this._findColumnByName(_g.d.ic_trans_detail._price) != -1) return MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._price).ToString());
            if (this._findColumnByName(_g.d.ic_trans_detail._cost) != -1) return MyLib._myGlobal._decimalPhase(this._cellGet(this._selectRow, _g.d.ic_trans_detail._cost).ToString());
            return 0M;
        }

        private string _serialNumber__getLocation()
        {
            if (this._findColumnByName(_g.d.ic_trans_detail._wh_code) != -1) return this._cellGet(this._selectRow, _g.d.ic_trans_detail._shelf_code).ToString();
            return "";
        }

        private string _serialNumber__getWarehouse()
        {
            if (this._findColumnByName(_g.d.ic_trans_detail._shelf_code) != -1) return this._cellGet(this._selectRow, _g.d.ic_trans_detail._wh_code).ToString();
            return "";
        }



        private bool _serialNumberForm__checkSerialNumber(int gridRow, string serialNumber)
        {
            Boolean __result = false;
            for (int __row = 0; __row < this._rowData.Count; __row++)
            {
                int __isSerial = (int)MyLib._myGlobal._decimalPhase(this._cellGet(__row, _g.d.ic_trans_detail._is_serial_number).ToString());
                if (__isSerial == 1 && serialNumber.Length > 0)
                {
                    _serialNumberStruct __data = (_serialNumberStruct)this._cellGet(__row, _g.d.ic_trans_detail._serial_number);
                    //if (__data != null)
                    //{
                    // ตรวจใน grid ก่อนว่าซ้ำหรือเปล่า
                    for (int __rowFind = 0; __rowFind < __data.__details.Count; __rowFind++)
                    {
                        if (gridRow != __row)
                        {
                            string __serialNumber2 = __data.__details[__rowFind]._serialNumber;
                            if (serialNumber.Equals(__serialNumber2))
                            {
                                __result = true;
                                MessageBox.Show(MyLib._myGlobal._resource("บันทึกหมายเลขเครื่องซ้ำกับรายการอื่นในเอกสาร"));
                                break;
                            }
                        }
                    }
                    //}
                }
            }
            return __result;
        }

        /// <summary>
        /// serial save
        /// </summary>
        private bool _serialNumberForm__saveData()
        {
            Decimal __qtySum = 0M;
            _serialNumberStruct __data = new _serialNumberStruct();
            for (int __row = 0; __row < this._serialNumberForm._serialNumber._grid._rowData.Count; __row++)
            {
                _serialNumberDetailStruct __detail = new _serialNumberDetailStruct();
                __detail._serialNumber = this._serialNumberForm._serialNumber._grid._cellGet(__row, _g.d.ic_trans_serial_number._serial_number).ToString().Trim().ToUpper();
                if (__detail._serialNumber.Length > 0)
                {
                    __detail._description = this._serialNumberForm._serialNumber._grid._cellGet(__row, _g.d.ic_trans_serial_number._description).ToString().Trim();
                    __detail._voidDate = MyLib._myGlobal._convertDateFromQuery(this._serialNumberForm._serialNumber._grid._cellGet(__row, _g.d.ic_trans_serial_number._void_date).ToString());
                    __detail._price = (decimal)this._serialNumberForm._serialNumber._grid._cellGet(__row, _g.d.ic_trans_serial_number._price);
                    __data.__details.Add(__detail);
                    __qtySum++;
                }
            }
            this._cellUpdate(this._selectRow, this._columnSerialNumber, __data, false);
            this._cellUpdate(this._selectRow, this._columnSerialNumberCount, __qtySum, false);
            return true;
        }

        public int _selectWareHouseAndShelfMode = 0;
        /// <summary>
        /// เลือกคลังและพื้นที่เก็บ
        /// </summary>
        /// <param name="mode">0=คลังที่เก็บปรกติ,1=คลังที่เก็บโอนเข้า</param>
        protected void _selectWareHouseAndShelf(int mode)
        {
            this._selectWareHouseAndShelfMode = mode;
            this._icTransItemGridSelectWareHouse._controlType = this._icTransControlType;
            string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString();
            string __itemDesc = __itemCode + "," + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name).ToString();
            string __wareHouseCode = this._cellGet(this._selectRow, (this._selectWareHouseAndShelfMode == 0) ? _g.d.ic_trans_detail._wh_code : _g.d.ic_trans_detail._wh_code_2).ToString();
            string __shelfCode = this._cellGet(this._selectRow, (this._selectWareHouseAndShelfMode == 0) ? _g.d.ic_trans_detail._shelf_code : _g.d.ic_trans_detail._shelf_code_2).ToString();
            this._icTransItemGridSelectWareHouse._itemCode = __itemCode;
            this._icTransItemGridSelectWareHouse._lastCode = __wareHouseCode.Split('~')[0].ToString() + "/" + __shelfCode.Split('~')[0].ToString();
            this._icTransItemGridSelectWareHouse.Text = __itemDesc;
            this._icTransItemGridSelectWareHouse._extraWhere = "";
            if (_g.g._companyProfile._branchStatus == 1) // MyLib._myGlobal._OEMVersion.Equals("SINGHA") && 
            {
                if (this._getBranchCode != null)
                {
                    string __getBranchSelect = this._getBranchCode();

                    if (__getBranchSelect.Length > 0)
                    {
                        // where branch
                        this._icTransItemGridSelectWareHouse._extraWhere = " wh_code in (select code from ic_warehouse where coalesce(" + _g.d.ic_warehouse._branch_use + ", branch_code) like \'%" + __getBranchSelect + "%\') ";
                    }
                }
            }

            this._icTransItemGridSelectWareHouse.ShowDialog();
        }

        void _icTransItemGridSelectWareHouse__selectWareHouseAndShelf(int mode, string wareHouseCode, string shelfCode)
        {
            if (mode == 1)
            {
                this._cellUpdate(this._selectRow, (this._selectWareHouseAndShelfMode == 0) ? _g.d.ic_trans_detail._wh_code : _g.d.ic_trans_detail._wh_code_2, wareHouseCode, false);
                this._cellUpdate(this._selectRow, (this._selectWareHouseAndShelfMode == 0) ? _g.d.ic_trans_detail._shelf_code : _g.d.ic_trans_detail._shelf_code_2, shelfCode, true);
                _searchUnitNameWareHouseNameShelfName(this._selectRow);

                if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
                {
                    if (this._lot != null)
                    {
                        this._lot(true);
                    }
                }
            }
            this._inputCell(this._selectRow, this._selectColumn);
        }

        /// <summary>
        /// เลือกหน่วยนับ
        /// </summary>
        protected void _selectUnitCode()
        {
            if (this._selectRow != -1)
            {
                int __unitType = (int)this._cellGet(this._selectRow, _g.d.ic_trans_detail._unit_type);
                string __itemCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString();
                string __itemDesc = __itemCode + "," + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name).ToString();
                if (__unitType == 0)
                {
                    MessageBox.Show(__itemDesc + " : " + MyLib._myGlobal._resource("สินค้านี้มีหน่วยนับเดียว"));
                }
                else
                {
                    string __unitCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._unit_code).ToString();
                    this._icTransItemGridSelectUnit._itemCode = __itemCode;
                    this._icTransItemGridSelectUnit._lastCode = __unitCode;
                    this._icTransItemGridSelectUnit.Text = __itemDesc;
                    this._icTransItemGridSelectUnit.ShowDialog();
                    if (this._icTransItemGridSelectUnit._selected)
                    {
                        this._afterUpdateClearQtyAndPrice(this._selectRow, false);
                    }
                }
            }
        }

        private void _icTransItemGridSelectUnit__selectUnitCode(int mode, string unitCode)
        {
            if (mode == 1)
            {
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._unit_code, unitCode, true);
                _searchUnitNameWareHouseNameShelfName(this._selectRow);
            }
            this._inputCell(this._selectRow, this._selectColumn);
        }

        /// <summary>
        /// แก้ไขชื่อ
        /// </summary>
        protected void _changeProductName()
        {
            if (this._selectRow >= this._rowData.Count)
            {
                this._selectRow = this._addRow();
                this.Invalidate();
            }
            this._icTransItemGridChangeName._nameTextBox.Text = this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name).ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
            this._icTransItemGridChangeName.Text = "Code : " + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString();
            this._icTransItemGridChangeName.ShowDialog();
        }

        protected void _changeProductRemark()
        {
            if (this._selectRow >= this._rowData.Count)
            {
                return;
            }
            this._icTransItemGridChangeRemark._nameTextBox.Text = this._cellGet(this._selectRow, _g.d.ic_trans_detail._remark).ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
            this._icTransItemGridChangeRemark.Text = "Code : " + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString();
            this._icTransItemGridChangeRemark.ShowDialog();
        }

        /// <summary>
        /// แก้ไขจำนวน
        /// </summary>
        protected void _changeProductQty()
        {
            try
            {
                this._icTransItemGridChangeQty._nameNumberBox._double = (decimal)this._cellGet(this._selectRow, _g.d.ic_trans_detail._qty);
                this._icTransItemGridChangeQty._nameNumberBox.textBox.Text = this._cellGet(this._selectRow, _g.d.ic_trans_detail._qty).ToString();
                this._icTransItemGridChangeQty.Text = "แก้ไขจำนวนสินค้า" + " : " + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "~" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name).ToString();
                this._icTransItemGridChangeQty.ShowDialog();
            }
            catch
            {
            }
        }

        /// <summary>
        /// แก้ไขราคา
        /// </summary>
        protected void _changeProductPrice()
        {
            try
            {
                // toe เพิ่ม option ดึงราคาสินค้าชุด และ คำณวนกลับ
                if (((int)this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_type) == 3 || (int)this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_type) == 5) &&
                    _g.g._companyProfile._fix_item_set_price == false)
                {
                    // สินค้าห้ามแก้ไขราคา
                    MessageBox.Show(MyLib._myGlobal._resource("สินค้านี้ไม่สามารถแก้ไขราคาได้"));
                }
                else
                {
                    // ตรวจสอบสิทธิ์
                    Boolean __canChange = false;
                    if (_g.g._companyProfile._warning_price_3)
                    {
                        if (MyLib._myGlobal._programName.Equals("SML CM") && _lastApprovePrice.Length > 0)
                        {
                            __canChange = true;
                            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._user_approve, _lastApprovePrice, false);
                        }
                        else
                        {
                            _pricePasswordForm __pricePassword = new _pricePasswordForm(1);
                            __pricePassword.ShowDialog();
                            if (__pricePassword._passwordPass)
                            {
                                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._user_approve, __pricePassword._userCode, false);
                                _lastApprovePrice = __pricePassword._userCode;
                                __canChange = true;
                            }
                            __pricePassword.Dispose();
                        }
                    }
                    else
                    {
                        __canChange = true;
                    }
                    //
                    if (__canChange)
                    {
                        this._icTransItemGridChangePrice._nameNumberBox._double = (decimal)this._cellGet(this._selectRow, _g.d.ic_trans_detail._price);
                        this._icTransItemGridChangePrice._nameNumberBox.textBox.Text = this._cellGet(this._selectRow, _g.d.ic_trans_detail._price).ToString();
                        this._icTransItemGridChangePrice.Text = "แก้ไขราคาสินค้า" + " : " + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "~" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name).ToString();
                        this._icTransItemGridChangePrice.ShowDialog();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// แก้ไขมูลค่า
        /// </summary>
        protected void _changeProductAmount()
        {
            try
            {
                if ((int)this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_type) == 3 || (int)this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_type) == 5)
                {
                    // สินค้าห้ามแก้ไขราคา
                    MessageBox.Show(MyLib._myGlobal._resource("สินค้านี้ไม่สามารถแก้ไขราคาได้"));
                }
                else
                {
                    // ตรวจสอบสิทธิ์
                    Boolean __canChange = false;
                    if (_g.g._companyProfile._warning_price_3)
                    {
                        _pricePasswordForm __pricePassword = new _pricePasswordForm(1);
                        __pricePassword.ShowDialog();
                        if (__pricePassword._passwordPass)
                        {
                            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._user_approve, __pricePassword._userCode, false);
                            __canChange = true;
                        }
                        __pricePassword.Dispose();
                    }
                    else
                    {
                        __canChange = true;
                    }
                    //
                    if (__canChange)
                    {
                        this._icTransItemGridChangeAmount._nameNumberBox._double = (decimal)this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount);
                        this._icTransItemGridChangeAmount._nameNumberBox.textBox.Text = this._cellGet(this._selectRow, _g.d.ic_trans_detail._sum_amount).ToString();
                        this._icTransItemGridChangeAmount.Text = "แก้ไขมูลค่าสินค้า" + " : " + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "~" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name).ToString();
                        this._icTransItemGridChangeAmount.ShowDialog();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// แก้ไขส่วนลด
        /// </summary>
        protected void _changeProductDiscount()
        {
            if (((int)this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_type) == 3 || (int)this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_type) == 5) &&
                    _g.g._companyProfile._fix_item_set_price == false)
            {
                MessageBox.Show(MyLib._myGlobal._resource("สินค้านี้ไม่สามารถแก้ไขส่วนลดได้"));
            }
            else
            {
                // ตรวจสอบสิทธิ์
                Boolean __canChange = false;
                if (_g.g._companyProfile._warning_discount_1)
                {
                    _pricePasswordForm __pricePassword = new _pricePasswordForm(4);
                    __pricePassword.ShowDialog();
                    if (__pricePassword._passwordPass)
                    {
                        this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._user_approve, __pricePassword._userCode, false);
                        __canChange = true;
                    }
                    __pricePassword.Dispose();
                }
                else
                {
                    __canChange = true;
                }
                //
                if (__canChange)
                {
                    this._icTransItemGridChangeDiscount._nameTextBox.textBox.Text = this._cellGet(this._selectRow, _g.d.ic_trans_detail._discount).ToString();
                    this._icTransItemGridChangeDiscount.Text = "Detail : " + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_code).ToString() + "~" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._item_name).ToString();
                    this._icTransItemGridChangeDiscount.ShowDialog();
                }
            }
        }

        private void _icTransItemGridChangeName__submit(int mode)
        {
            // แก้ไขชื่อ
            if (mode == 1)
            {
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_name, this._icTransItemGridChangeName._nameTextBox.Text, false);
            }
            this.Invalidate();
            this._inputCell(this._selectRow, this._selectColumn);
        }

        private MyLib.BeforeDisplayRowReturn _ictransItemGridControl__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            if (this._isSerialNumber)
            {
                // Serial Number
                int __columnIsSerialNumber = this._findColumnByName(_g.d.ic_trans_detail._is_serial_number);
                if (__columnIsSerialNumber != -1)
                {
                    int _isSerial = (int)MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._is_serial_number).ToString());
                    if (_isSerial == 1)
                    {
                        decimal __qty = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._qty).ToString());
                        decimal __serialQty = MyLib._myGlobal._decimalPhase(this._cellGet(row, this._columnSerialNumberCount).ToString());
                        if (__qty != __serialQty)
                        {
                            senderRow.newColor = Color.MediumVioletRed;
                        }
                    }
                }
            }

            int __columnExpireDate = this._findColumnByName(_g.d.ic_trans_detail._date_expire);
            if (__columnExpireDate != -1)
            {
                // check expire date
                int _is_use_expire = (int)MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._use_expire).ToString());
                if (_is_use_expire == 1)
                {
                    DateTime __getDate = (DateTime)this._cellGet(row, _g.d.ic_trans_detail._date_expire);
                    string __cellData = (__getDate.Year < 1900) ? "" : MyLib._myGlobal._convertDateToString(__getDate, false);
                    if (__cellData == "")
                    {
                        senderRow.newColor = Color.BlueViolet;
                    }
                }
            }

            //
            int __columnIsPermiumNumber = this._findColumnByName(_g.d.ic_trans_detail._is_permium);
            if (__columnIsPermiumNumber != 1)
            {
                if (this._cellGet(row, __columnIsPermiumNumber).ToString().Equals("1"))
                {
                    senderRow.newColor = Color.YellowGreen;
                }
            }
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    int __columnSetRefLineNumber = this._findColumnByName(_g.d.ic_trans_detail._set_ref_line);
                    if (__columnSetRefLineNumber != 1)
                    {
                        if (this._cellGet(row, __columnSetRefLineNumber).ToString().Length > 0)
                        {
                            senderRow.newColor = Color.SteelBlue;
                        }
                    }
                    int __itemType = (int)this._cellGet(row, _g.d.ic_trans_detail._item_type);
                    if (__itemType == 3 || __itemType == 5)
                    {
                        senderRow.newColor = Color.Blue;
                    }
                    if (columnNumber == this._findColumnByName(_g.d.ic_trans_detail._price))
                    {
                        if (_g.g._companyProfile._warning_low_cost && _checkPriceLowCost(row, false))
                        {
                            senderRow.newColor = Color.DarkOrange;
                        }
                    }

                    if (((int)this._cellGet(row, _g.d.ic_inventory._have_replacement)) != 0)
                    {
                        senderRow.newColor = Color.Orange;
                    }
                    break;
            }
            //
            if (columnName.Equals(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code))
            {
                int __unitNameColumn = this._findColumnByName(_g.d.ic_trans_detail._unit_name);
                string __unitName = this._cellGet(row, __unitNameColumn).ToString();
                if (__unitName.Length > 0) ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __unitName);
            }
            else
                if (columnName.Equals(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code))
            {
                int __whNameColumn = this._findColumnByName(_g.d.ic_trans_detail._wh_name);
                string __whName = this._cellGet(row, __whNameColumn).ToString();
                if (__whName.Length > 0) ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __whName);
            }
            else
                    if (columnName.Equals(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code))
            {
                int __shelfNameColumn = this._findColumnByName(_g.d.ic_trans_detail._shelf_name);
                string __shelfName = this._cellGet(row, __shelfNameColumn).ToString();
                if (__shelfName.Length > 0) ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __shelfName);
            }
            else
                        if (columnName.Equals(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code_2))
            {
                int __whNameColumn = this._findColumnByName(_g.d.ic_trans_detail._wh_name_2);
                string __whName = this._cellGet(row, __whNameColumn).ToString();
                if (__whName.Length > 0) ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __whName);
            }
            else
                            if (columnName.Equals(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code_2))
            {
                int __shelfNameColumn = this._findColumnByName(_g.d.ic_trans_detail._shelf_name_2);
                string __shelfName = this._cellGet(row, __shelfNameColumn).ToString();
                if (__shelfName.Length > 0)
                {
                    ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __shelfName);
                }
            }

            if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง && columnName.Equals(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name))
            {
                int __custNameColumnNumber = this._findColumnByName(_g.d.ic_trans._cust_name);
                string __getCustName = this._cellGet(row, __custNameColumnNumber).ToString();
                if (__getCustName.Length > 0)
                {
                    ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __getCustName);
                }
            }

            // toe ของแถม singha

            /*if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && columnName.Equals(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name))
            {
                int __premiumColumn = this._findColumnByName(_g.d.ic_trans_detail._is_permium);
                int __isPremium = MyLib._myGlobal._intPhase(this._cellGet(row, __premiumColumn).ToString());

                if (__isPremium == 1)
                {
                    ((ArrayList)senderRow.newData)[columnNumber] = string.Concat("ของแถม ", ((ArrayList)senderRow.newData)[columnNumber].ToString().Replace("ของแถม ", string.Empty));

                }

            }*/

            // แสดงสี กรณียอดคำนวณแล้วไม่ตรงกับที่ป้อน
            int __qtyColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._qty);
            int __priceColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._price);
            int __totalColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._sum_amount);
            int __discountColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._discount);
            if (__priceColumnNumber != -1 && __totalColumnNumber != -1 && __qtyColumnNumber != -1)
            {
                decimal __qty = (decimal)this._cellGet(row, __qtyColumnNumber);
                decimal __price = (decimal)this._cellGet(row, __priceColumnNumber);
                decimal __amount = (decimal)this._cellGet(row, __totalColumnNumber);
                string __discountWord = (__discountColumnNumber == -1) ? "0.0" : this._cellGet(row, __discountColumnNumber).ToString();


                decimal __amountCompare = _calcItemAmount(__discountWord, __qty, __price);  // โต๋ย้าย เข้า function MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__qty * __price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, __qty);
                if (__amount != __amountCompare)
                {
                    senderRow.newColor = Color.Red;
                }
                else
                {
                    int __taxType = (int)this._cellGet(row, _g.d.ic_trans_detail._tax_type);
                    if (__taxType == 1)
                    {
                        senderRow.newColor = Color.Magenta;
                    }
                }
            }
            // กรณีส่วนลด ให้ชิดขวา
            if (columnNumber == __discountColumnNumber)
            {
                senderRow.align = ContentAlignment.MiddleRight;
            }
            return (senderRow);
        }

        private void _clearDataGuidNotFound()
        {
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    int __row = 0;
                    int __columnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_guid);
                    while (__row < this._rowData.Count)
                    {
                        string __getGuid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                        if (__getGuid.Length > 0)
                        {
                            int __find = this._findData(__columnNumber, __getGuid);
                            if (__find == -1)
                            {
                                this._rowData.RemoveAt(__row);
                            }
                            else
                            {
                                __row++;
                            }
                        }
                        else
                        {
                            __row++;
                        }
                    }
                    break;
            }
        }

        // toe 
        void _checkSyncColor(string itemSetCode)
        {
            string __query = "select count(*) as xcount from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + itemSetCode + "\' ";
            DataTable __result = _myFrameWork._queryShort(__query).Tables[0];
            if (__result.Rows.Count > 0 && MyLib._myGlobal._intPhase(__result.Rows[0][0].ToString()) == 0)
            {
                // import ใหม่
                if ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore) && _g.g._companyProfile._sync_wbservice_url.Trim().Length > 0 && _g.g._companyProfile._sync_product)
                {
                    SMLProcess._syncClass __sync = new SMLProcess._syncClass();
                    bool __found = __sync._findSetDetail(itemSetCode);

                    if (__found == true)
                    {
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                        {
                            StringBuilder __querySync = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __querySync.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + itemSetCode + "\'"));
                            __querySync.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + itemSetCode + "\'"));
                            __querySync.Append("</node>");

                            ArrayList __syncResult = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __querySync.ToString());
                            DataTable __itemTableResult = ((DataSet)__syncResult[0]).Tables[0];
                            DataTable __itemSetTableResult = ((DataSet)__syncResult[1]).Tables[0];

                            if (__itemTableResult.Rows.Count > 0)
                            {
                                string __getItemType = __itemTableResult.Rows[0][_g.d.ic_inventory._item_type].ToString();
                                if (__getItemType.Equals("3") || __getItemType.Equals("5"))
                                {
                                    for (int __row = 0; __row < __itemSetTableResult.Rows.Count; __row++)
                                    {
                                        string __getItemDetailCode = __itemSetTableResult.Rows[__row][_g.d.ic_inventory_set_detail._ic_code].ToString();
                                        __sync._findProduct(__getItemDetailCode);
                                    }
                                }
                            }
                            //__sync._foundItemCode = __findBarcode;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ค้นหารายละเอียดสินค้า
        /// </summary>
        /// <param name="itemCode"></param>
        private Boolean _gridFindItem(string itemCode, string barCode, string unitCodeFixed, int row)
        {
            Boolean __found = false;
            int __wareHouseColmnNumber = this._findColumnByName(_g.d.ic_trans_detail._wh_code);
            int __itemType = 0;
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __oldWhCode1 = "";
            string __oldShelfCode1 = "";
            string __oldWhCode2 = "";
            string __oldShelfCode2 = "";
            if (this._findColumnByName(_g.d.ic_trans_detail._wh_code) != -1)
            {
                // ตรวจดูว่ามีการกำหนดคลังที่เก็บในหัวเอกสารหรือไม่
                // toe check null (Fix Bug Color Store)
                if (this._icTransScreenTop != null)
                {
                    string __topWareHouseCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from).ToString().Trim();
                    if (__topWareHouseCode.Length > 0)
                    {
                        __oldWhCode1 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from).ToString().Trim();
                        __oldShelfCode1 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_from).ToString().Trim();
                        if (this._findColumnByName(_g.d.ic_trans_detail._wh_code_2) != -1)
                        {
                            __oldWhCode2 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_to).ToString().Trim();
                            __oldShelfCode2 = this._icTransScreenTop._getDataStr(_g.d.ic_trans._location_to).ToString().Trim();
                        }
                    }
                    else
                    {
                        if (row > 0)
                        {
                            // เอาคลัง+ที่เกิบเดิมมาเพื่อความเร็วในการบันทึก
                            __oldWhCode1 = this._cellGet(row - 1, _g.d.ic_trans_detail._wh_code).ToString();
                            __oldShelfCode1 = this._cellGet(row - 1, _g.d.ic_trans_detail._shelf_code).ToString();
                            if (this._findColumnByName(_g.d.ic_trans_detail._wh_code_2) != -1)
                            {
                                __oldWhCode2 = this._cellGet(row - 1, _g.d.ic_trans_detail._wh_code_2).ToString();
                                __oldShelfCode2 = this._cellGet(row - 1, _g.d.ic_trans_detail._shelf_code_2).ToString();
                            }
                        }
                    }
                }
            }
            StringBuilder __query = new StringBuilder();

            // // toe เพิ่ม filter warehouse และ shelf ตามกลุ่มพนักงาน
            StringBuilder __extraWhereGetDefaultWhShelf = new StringBuilder();
            if (_g.g._companyProfile._perm_wh_shelf)
            {
                // _g.d.erp_user_group_wh_shelf._screen_code
                __extraWhereGetDefaultWhShelf.Append(_g._icInfoFlag._icWhShelfUserPermissionWhereQuery(this._ictransControlTemp));
            }

            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // 0,ชื่อสินค้า,คลังสินค้าซื้อ,พื้นที่เก็บสินค้าซื้อ,หน่วยนับมาตรฐาน
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._tax_type + "," + _g.d.ic_inventory._average_cost +
                "," + "(select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + this._columnAverageCostUnitStand +
                "," + "(select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + this._columnAverageCostUnitDiv +
                "," + _g.d.ic_inventory._name_1 + "," + _g.d.ic_inventory._unit_type + "," + _g.d.ic_inventory._item_type + "," + _g.d.ic_inventory._cost_type +
                "," + "coalesce((select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + _g.d.ic_wh_shelf._wh_code + " from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_inventory._code + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + " order by " + _g.d.ic_wh_shelf._wh_code + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + "),'') as " + _g.d.ic_wh_shelf._wh_code +
                "," + "coalesce((select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + _g.d.ic_wh_shelf._shelf_code + " from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_inventory._code + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + " order by " + _g.d.ic_wh_shelf._wh_code + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + "),'') as " + _g.d.ic_wh_shelf._shelf_code +
                "," + _g.d.ic_inventory._unit_standard + "," + _g.d.ic_inventory._ic_serial_no +
                ",( select count(" + _g.d.ic_inventory_replacement._table + "." + _g.d.ic_inventory_replacement._ic_code + ") from " + _g.d.ic_inventory_replacement._table + " where " + _g.d.ic_inventory_replacement._table + "." + _g.d.ic_inventory_replacement._ic_replace_code + " = " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") as " + _g.d.ic_inventory._have_replacement +
                "," + _g.d.ic_inventory._use_expire +
                "," + _g.d.ic_inventory._remark +
                //"," + " ( select " + _g.d.ic_inventory_detail._discount + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") as " + _g.d.ic_inventory_detail._discount +

                // toe หน่วยเริ่มต้น
                ",(select " + _g.d.ic_inventory_detail._start_unit_code + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " ) as " + _g.d.ic_inventory_detail._start_unit_code +

                " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + itemCode + "\'"));
            // 1,คลังสินค้าซื้อ,พื้นที่เก็บสินค้าซื้อ,หน่วยนับซื้อ (เริ่มต้น)
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_detail._start_purchase_wh + "," + _g.d.ic_inventory_detail._start_purchase_shelf + "," + _g.d.ic_inventory_detail._start_purchase_unit + "," + _g.d.ic_inventory_detail._discount +
                ", (select " + _g.d.ap_supplier_detail._discount_item + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._ap_code + " = \'" + this._custCode + "\' ) as " + _g.d.ap_supplier_detail._discount_item +
                ", " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._ic_code + "=\'" + itemCode + "\'"));
            // 2,คลังสินค้าขาย,พื้นที่เก็บสินค้าขาย,หน่วยนับขาย (เริ่มต้น)
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_detail._start_sale_wh + "," + _g.d.ic_inventory_detail._start_sale_shelf + "," + _g.d.ic_inventory_detail._start_sale_unit + "," + _g.d.ic_inventory_detail._discount +
                ", (select " + _g.d.ar_customer_detail._discount_item + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + " = \'" + this._custCode + "\' ) as " + _g.d.ar_customer_detail._discount_item +
                "," + _g.d.ic_inventory_detail._is_hold_sale + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._ic_code + "=\'" + itemCode + "\'"));
            // toe
            // 3,คลัง,พื้นที่เก็บตามพนักงาน 
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._ic_wht + "," + _g.d.erp_user._ic_shelf + " from " + _g.d.erp_user._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user._code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\'"));

            // ref row enable
            if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
            {
                string __docRefNo = "";
                int __docRefNoColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                int __checkColumnNumberBillType = this._findColumnByName(_g.d.ic_trans_detail._doc_ref_type);

                if (__docRefNoColumnNumber != -1 && __checkColumnNumberBillType != -1)
                {

                    __docRefNo = this._cellGet(row, __docRefNoColumnNumber).ToString();
                    int __selectTransFlag = MyLib._myGlobal._intPhase(this._cellGet(row, __checkColumnNumberBillType).ToString());
                    if (__docRefNo.Length > 0)
                    {
                        string __transFlag = "";
                        string __getWh_code = "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_wh + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._wh_code;
                        string __getShelf_code = "coalesce((select " + _g.d.ic_inventory_detail._start_purchase_shelf + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "),'') as " + _g.d.ic_trans_detail._shelf_code;
                        string __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))";

                        string __wareHouseAndShelfCodeField = "wh_temp";

                        switch (__selectTransFlag)
                        {
                            case 0: // ไม่เลือก
                                break;
                            case 1:
                                __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString();
                                __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                break;
                            case 2:
                                __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString();
                                __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                if (_g.g._companyProfile._stock_reserved_control_location)
                                {
                                    __getWh_code = _g.d.ic_trans_detail._wh_code;
                                    __getShelf_code = _g.d.ic_trans_detail._shelf_code;
                                }
                                break;
                            case 3:
                                __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString();
                                __balanceQty = "(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))-coalesce((select sum(" + "x." + _g.d.ic_trans_detail._qty + "*(" + "x." + _g.d.ic_trans_detail._stand_value + "/" + "x." + _g.d.ic_trans_detail._divide_value + ")) from " + _g.d.ic_trans_detail._table + " as x where " + "x." + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ") and " + "x." + _g.d.ic_trans_detail._ref_doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and x." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and x." + _g.d.ic_trans_detail._ref_row + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " and x." + _g.d.ic_trans_detail._last_status + "=0),0)";
                                __getWh_code = _g.d.ic_trans_detail._wh_code;
                                __getShelf_code = _g.d.ic_trans_detail._shelf_code;
                                break;
                        }

                        string __queryQtyPriceRefStr = "select * from ( select line_number,  " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ","

                                       + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._ref_guid + "," + _g.d.ic_trans_detail._ref_row + ","
                                       + _g.d.ic_trans_detail._item_code_main + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._is_get_price + ","
                                       + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._set_ref_price + "," + _g.d.ic_trans_detail._set_ref_line + "," + _g.d.ic_trans_detail._is_serial_number + ","
                                       + __getWh_code + ","
                                       + __getShelf_code + ","
                                       + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + ",(" + __balanceQty + ")/(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ") as " + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._sum_amount + ","
                                       + "(select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + "wh_code||'~'||shelf_code from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " order by roworder " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1] + ") as " + __wareHouseAndShelfCodeField

                                       + " from " + _g.d.ic_trans_detail._table
                                       + " where " + _g.d.ic_trans_detail._doc_no + " =\'" + __docRefNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " and " + _g.d.ic_trans_detail._item_code + " = \'" + itemCode + "\'"
                                       + " ) as temp2 where qty <> 0 "
                                       + " order by " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number;

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryQtyPriceRefStr));
                    }
                    // ขายสินค้า กรณีอ้างอิง จากสั่งขาย เสนอราคา สั่งจอง ให้ดึงจำนวนและราคาที่คงเหลือมาด้วย
                }

            }

            __query.Append("</node>");
            String __queryStr = __query.ToString();
            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
            //
            string __itemName = "";
            string __whCode = "";
            string __shelfCode = "";
            string __whCode2 = "";
            string __shelfCode2 = "";
            string __itemDiscount = "";
            string __unitCode = unitCodeFixed;
            decimal __unitaverageCoste = 0M;
            decimal __unitCostStand = 0M;
            decimal __unitCostDiv = 0M;
            string __getRemrk = "";
            int __unitType = 0; // 0=หน่วยนับเดียว,1=หลายหน่วยนับ,2=หน่วยนับเดียว+หน่วยนับขนาน,3=หลายหน่วยนับ+หน่วยนับขนาน
            int __taxType = 0; // 0=มีภาษี,1=ยกเว้นภาษี
            int __isSerialNumber = 0; // 0= ไม่มี Serial,1=มี Serial
            int __costType = 0;
            int __use_expire = 0;
            Boolean __is_hold_sale = false;
            Boolean __is_hold_purchase = false;

            //
            DataTable __t0 = ((DataSet)__dataResult[0]).Tables[0];
            DataTable __t1 = ((DataSet)__dataResult[1]).Tables[0];
            DataTable __t4 = ((DataSet)__dataResult[2]).Tables[0];
            DataTable __t5 = ((DataSet)__dataResult[3]).Tables[0];

            if (__t0.Rows.Count > 0)
            {
                __found = true;
                // ชื่อสินค้า
                __itemName = __t0.Rows[0][_g.d.ic_inventory._name_1].ToString();
                __unitType = MyLib._myGlobal._intPhase(__t0.Rows[0][_g.d.ic_inventory._unit_type].ToString());
                __itemType = MyLib._myGlobal._intPhase(__t0.Rows[0][_g.d.ic_inventory._item_type].ToString());
                __unitaverageCoste = MyLib._myGlobal._decimalPhase(__t0.Rows[0][_g.d.ic_inventory._average_cost].ToString());
                __unitCostStand = MyLib._myGlobal._decimalPhase(__t0.Rows[0][this._columnAverageCostUnitStand].ToString());
                __unitCostDiv = MyLib._myGlobal._decimalPhase(__t0.Rows[0][this._columnAverageCostUnitDiv].ToString());
                __isSerialNumber = (int)MyLib._myGlobal._decimalPhase(__t0.Rows[0][_g.d.ic_inventory._ic_serial_no].ToString());
                __use_expire = (int)MyLib._myGlobal._decimalPhase(__t0.Rows[0][_g.d.ic_inventory._use_expire].ToString());
                __taxType = (int)MyLib._myGlobal._decimalPhase(__t0.Rows[0][_g.d.ic_inventory._tax_type].ToString());
                __costType = (int)MyLib._myGlobal._decimalPhase(__t0.Rows[0][this._columnCostType].ToString());
                __getRemrk = __t0.Rows[0][_g.d.ic_inventory._remark].ToString();

                __is_hold_sale = (__t4.Rows.Count > 0) ? (__t4.Rows[0][_g.d.ic_inventory_detail._is_hold_sale].ToString().Equals("1") ? true : false) : false;

                string __start_unit_code = __t0.Rows[0][_g.d.ic_inventory_detail._start_unit_code].ToString();
                if (unitCodeFixed.Length == 0 && __start_unit_code.Length > 0)
                {
                    __unitCode = __start_unit_code;
                }

                if (__wareHouseColmnNumber != -1)
                {
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ:
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            {
                                // คลัง
                                __whCode = (__t4.Rows.Count == 0) ? "" : __t4.Rows[0][_g.d.ic_inventory_detail._start_sale_wh].ToString();
                                if (__whCode.Length == 0)
                                {
                                    __whCode = __t0.Rows[0][_g.d.ic_wh_shelf._wh_code].ToString();
                                }
                                // ที่เก็บ
                                __shelfCode = (__t4.Rows.Count == 0) ? "" : __t4.Rows[0][_g.d.ic_inventory_detail._start_sale_shelf].ToString();
                                if (__shelfCode.Length == 0)
                                {
                                    __shelfCode = __t0.Rows[0][_g.d.ic_wh_shelf._shelf_code].ToString();
                                }
                            }
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                        case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                        case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                        case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                        case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                        case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                        case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                        case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                        case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                        case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                        case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                        case _g.g._transControlTypeEnum.สินค้า_ขอโอน:
                            {
                                // คลัง
                                __whCode = (__t1.Rows.Count == 0) ? "" : __t1.Rows[0][_g.d.ic_inventory_detail._start_purchase_wh].ToString();
                                if (__whCode.Length == 0)
                                {
                                    __whCode = __t0.Rows[0][_g.d.ic_wh_shelf._wh_code].ToString();
                                }
                                // ที่เก็บ
                                __shelfCode = (__t1.Rows.Count == 0) ? "" : __t1.Rows[0][_g.d.ic_inventory_detail._start_purchase_shelf].ToString();
                                if (__shelfCode.Length == 0)
                                {
                                    __shelfCode = __t0.Rows[0][_g.d.ic_wh_shelf._shelf_code].ToString();
                                }
                            }
                            break;
                    }
                }

                if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && __is_hold_sale)
                {
                    MessageBox.Show("สินค้า " + __itemName + " เลิกขาย");
                    __whCode = "";
                    __shelfCode = "";
                    __unitCode = unitCodeFixed;
                    __unitType = 0;
                    __whCode2 = "";
                    __shelfCode2 = "";
                    this._cellUpdate(row, _g.d.ic_trans_detail._item_code, "", false);
                    return false;
                }

                // หน่วยนับ
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        {
                            if (unitCodeFixed.Length == 0)
                            {
                                string __getUnitCode = "";
                                try
                                {
                                    __getUnitCode = (__t4.Rows.Count == 0) ? "" : __t4.Rows[0][_g.d.ic_inventory_detail._start_sale_unit].ToString();
                                }
                                catch
                                {
                                }
                                __unitCode = __getUnitCode;
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        {
                            if (unitCodeFixed.Length == 0)
                            {
                                string __getUnitCode = "";
                                try
                                {
                                    __getUnitCode = (__t1.Rows.Count == 0) ? "" : __t1.Rows[0][_g.d.ic_inventory_detail._start_purchase_unit].ToString();
                                }
                                catch
                                {
                                }
                                __unitCode = __getUnitCode;
                            }
                        }
                        break;
                }

                // toe ส่วนลด  
                // หน่วยนับ

                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        {
                            string __getDiscountItem = "";
                            string __getDiscountCust = "";
                            try
                            {
                                __getDiscountItem = (__t4.Rows.Count == 0) ? "" : __t4.Rows[0][_g.d.ic_inventory_detail._discount].ToString();
                                if (__getDiscountItem.Equals("0.0") || __getDiscountItem.Equals("0"))
                                {
                                    __getDiscountItem = "";
                                }
                                __getDiscountCust = (__t4.Rows.Count == 0) ? "" : __t4.Rows[0][_g.d.ar_customer_detail._discount_item].ToString();
                                if (__getDiscountCust.Equals("0.0") || __getDiscountCust.Equals("0"))
                                {
                                    __getDiscountCust = "";
                                }

                            }
                            catch
                            {
                            }
                            __itemDiscount = (__getDiscountItem.Length > 0) ? __getDiscountItem : __getDiscountCust;
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        {

                            string __getDiscountItem = "";
                            string __getDiscountCust = "";
                            try
                            {
                                __getDiscountItem = (__t1.Rows.Count == 0) ? "" : __t1.Rows[0][_g.d.ic_inventory_detail._discount].ToString();
                                if (__getDiscountItem.Equals("0.0") || __getDiscountItem.Equals("0"))
                                {
                                    __getDiscountItem = "";
                                }
                                __getDiscountCust = (__t1.Rows.Count == 0) ? "" : __t1.Rows[0][_g.d.ap_supplier_detail._discount_item].ToString();
                                if (__getDiscountCust.Equals("0.0") || __getDiscountCust.Equals("0"))
                                {
                                    __getDiscountCust = "";
                                }

                            }
                            catch
                            {
                            }
                            __itemDiscount = (__getDiscountItem.Length > 0) ? __getDiscountItem : __getDiscountCust;
                        }
                        break;
                }

                // สินค้าทดแทน
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                    case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                    case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                    case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                    case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                    case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                    case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                    case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                    case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:

                        int __replacement = (int)MyLib._myGlobal._decimalPhase(__t0.Rows[0][_g.d.ic_inventory._have_replacement].ToString());
                        this._cellUpdate(row, _g.d.ic_inventory._have_replacement, __replacement, false);

                        break;
                }

                if (__unitCode.Length == 0)
                {
                    __unitCode = __t0.Rows[0][_g.d.ic_inventory._unit_standard].ToString();
                }
            }
            if (__itemName.Length == 0)
            {
                if (itemCode.Length > 0)
                {
                    if ((barCode.Length > 0 && _g.g._companyProfile._sync_wbservice_url.Trim().Length > 0 && _g.g._companyProfile._sync_product)
                        || MyLib._myGlobal._programName.ToUpper().Equals("POS-RETAIL"))
                    {
                        SMLProcess._syncClass __sync = new SMLProcess._syncClass();
                        string __findBarcode = (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore) ? itemCode : barCode;
                        __found = __sync._findProduct(__findBarcode);
                        if (__found)
                        {
                            // check หากเป็นสินค้าชุด
                            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                            {
                                StringBuilder __querySync = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                __querySync.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __findBarcode + "\'"));
                                __querySync.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + __findBarcode + "\'"));
                                __querySync.Append("</node>");

                                ArrayList __syncResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __querySync.ToString());
                                DataTable __itemTableResult = ((DataSet)__syncResult[0]).Tables[0];
                                DataTable __itemSetTableResult = ((DataSet)__syncResult[1]).Tables[0];

                                if (__itemTableResult.Rows.Count > 0)
                                {
                                    string __getItemType = __itemTableResult.Rows[0][_g.d.ic_inventory._item_type].ToString();
                                    if (__getItemType.Equals("3") || __getItemType.Equals("5"))
                                    {
                                        for (int __row = 0; __row < __itemSetTableResult.Rows.Count; __row++)
                                        {
                                            string __getItemDetailCode = __itemSetTableResult.Rows[__row][_g.d.ic_inventory_set_detail._ic_code].ToString();
                                            __sync._findProduct(__getItemDetailCode);
                                        }
                                    }
                                }
                                //__sync._foundItemCode = __findBarcode;
                            }

                            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                            {
                                return this._gridFindItem(__findBarcode, barCode, __unitCode, row);
                            }
                            else
                                return this._gridFindItem(__sync._foundItemCode, barCode, __unitCode, row);

                        }
                    }
                    if (__found == false)
                    {
                        if (this._importWorking == false)
                        {
                            if (MyLib._myGlobal._programName.ToUpper().Equals("POS-RETAIL"))
                            {
                                if (MessageBox.Show(MyLib._myGlobal._resource("ไม่พบรหัสสินค้า ทำการเพิ่มรายละเอียดสินค้าหรือไม่") + " : " + itemCode, "เตือน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    __found = false;
                                    _icUpdateForm __updateIcForm = new _icUpdateForm();
                                    __updateIcForm._updateControl._startQuickAddItem(itemCode);
                                    __updateIcForm._updateControl._saveSuccess += (saveItemCode, saveBarcode) =>
                                    {
                                        __updateIcForm.Close();
                                    };
                                    __updateIcForm.ShowDialog();
                                    if (__updateIcForm._updateControl._insertSuccess == true)
                                    {
                                        return this._gridFindItem(__updateIcForm._updateControl._getOldCode, __updateIcForm._updateControl._getOldBarcode, __unitCode, row);
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("ไม่พบรหัสสินค้า") + " : " + itemCode);
                            }
                        }
                    }
                }
                __whCode = "";
                __shelfCode = "";
                __unitCode = unitCodeFixed;
                __unitType = 0;
                __whCode2 = "";
                __shelfCode2 = "";
                this._cellUpdate(row, _g.d.ic_trans_detail._item_code, "", false);
            }
            if (this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().Trim().Length == 0)
            {
                this._cellUpdate(row, _g.d.ic_trans_detail._qty, 0.0M, false);
                this._cellUpdate(row, _g.d.ic_trans_detail._price, 0.0M, false);
                this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, 0.0M, false);
            }
            this._cellUpdate(row, _g.d.ic_trans_detail._ref_guid, "", false);
            this._cellUpdate(row, _g.d.ic_trans_detail._is_serial_number, __isSerialNumber, false);
            this._cellUpdate(row, _g.d.ic_trans_detail._use_expire, __use_expire, false);
            this._cellUpdate(row, _g.d.ic_trans_detail._tax_type, __taxType, false);
            this._cellUpdate(row, this._columnCostType, __costType, false);

            // toe check costType = 1 show lot
            // Lot
            // toe เอาออก ( MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS  && __costType == 1 )||

            /* โต๋ ย้ายลงไปหลังคลังที่เก็บ
             * if ((__costType == 1 && (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccount && MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccountProfessional  && MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccountPOS)) ||
                ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS) && __costType == 2))
            {
                if (this._lot != null)
                {

                    this._lot();
                }
            }
            */
            _clearDataGuidNotFound();
            this._cellUpdate(row, _g.d.ic_trans_detail._item_name, __itemName, false);

            if (__getRemrk.IndexOf("#") == 0)
            {
                this._cellUpdate(row, this._columnIsGetItemRemark, 1, false);
            }

            if (__wareHouseColmnNumber != -1)
            {
                // toe แก้ไข กำหนดคลังและที่เก็บตามพนักงาน
                if (_g.g._companyProfile._perm_wh_shelf)
                {
                    if (__t5.Rows.Count > 0 && (__t5.Rows[0][_g.d.erp_user._ic_wht].ToString().Length > 0 || __t5.Rows[0][_g.d.erp_user._ic_shelf].ToString().Length > 0))
                    {
                        // update เป็นคลัง และที่เก็บตามพนักงาน
                        __whCode = __t5.Rows[0][_g.d.erp_user._ic_wht].ToString();
                        __shelfCode = __t5.Rows[0][_g.d.erp_user._ic_shelf].ToString();
                    }
                }

                if (__whCode.Length > 0)
                {
                    // เอาคลัง+ที่เกิบเดิมมาเพื่อความเร็วในการบันทึก
                    if (__oldWhCode1.Length > 0)
                    {
                        __whCode = __oldWhCode1;
                        __shelfCode = __oldShelfCode1;
                    }
                    if (__oldWhCode2.Length > 0)
                    {
                        __whCode2 = __oldWhCode2;
                        __shelfCode2 = __oldShelfCode2;
                    }
                }
                this._cellUpdate(row, _g.d.ic_trans_detail._wh_code, __whCode, false);
                this._cellUpdate(row, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                // 
                if (this._findColumnByName(_g.d.ic_trans_detail._wh_code_2) != -1)
                {
                    // ตรวจว่ามี whare house 2 หรือเปล่า
                    this._cellUpdate(row, _g.d.ic_trans_detail._wh_code_2, __whCode2, false);
                    this._cellUpdate(row, _g.d.ic_trans_detail._shelf_code_2, __shelfCode2, false);
                }
            }



            this._cellUpdate(row, _g.d.ic_trans_detail._unit_code, __unitCode, false);
            this._cellUpdate(row, _g.d.ic_trans_detail._unit_type, __unitType, false);
            this._cellUpdate(row, _g.d.ic_trans_detail._item_type, __itemType, false);
            this._cellUpdate(row, _g.d.ic_trans_detail._discount, __itemDiscount, true);
            this._cellUpdate(row, _g.d.ic_trans_detail._average_cost, __unitaverageCoste, false);
            this._cellUpdate(row, this._columnAverageCostUnitStand, __unitCostStand, false);
            this._cellUpdate(row, this._columnAverageCostUnitDiv, __unitCostDiv, false);

            _searchUnitNameWareHouseNameShelfName(row);

            // toe doc_ref_select 
            if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
            {
                string __docRefNo = "";
                int __docRefNoColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                int __checkColumnNumberBillType = this._findColumnByName(_g.d.ic_trans_detail._doc_ref_type);

                if (__docRefNoColumnNumber != -1 && __checkColumnNumberBillType != -1)
                {
                    __docRefNo = this._cellGet(row, __docRefNoColumnNumber).ToString();
                    int __selectTransFlag = MyLib._myGlobal._intPhase(this._cellGet(row, __checkColumnNumberBillType).ToString());
                    if (__docRefNo.Length > 0)
                    {
                        try
                        {
                            DataTable __t6 = ((DataSet)__dataResult[4]).Tables[0];

                            if (__t6.Rows.Count > 0)
                            {
                                //int __qtyColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._qty);
                                decimal __qty = MyLib._myGlobal._decimalPhase(__t6.Rows[0][_g.d.ic_trans_detail._qty].ToString());
                                decimal __price = MyLib._myGlobal._decimalPhase(__t6.Rows[0][_g.d.ic_trans_detail._price].ToString());
                                decimal __amount = MyLib._myGlobal._decimalPhase(__t6.Rows[0][_g.d.ic_trans_detail._sum_amount].ToString()); //__qty * __price; 
                                __unitCode = __t6.Rows[0][_g.d.ic_trans_detail._unit_code].ToString();
                                string __discount = __t6.Rows[0][_g.d.ic_trans_detail._discount].ToString();

                                // update unit_code, qty, price
                                this._cellUpdate(row, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                                _searchUnitNameWareHouseNameShelfName(row);
                                this._cellUpdate(row, _g.d.ic_trans_detail._qty, __qty, true);
                                this._cellUpdate(row, _g.d.ic_trans_detail._discount, __discount, false);
                                this._cellUpdate(row, _g.d.ic_trans_detail._price, __price, true);
                                this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, __amount, false);

                                // toe เพิ่ม สั่งคำณวนราคาใหม่
                                this._calcItemPrice(row, row, this._findColumnByName(_g.d.ic_trans_detail._discount));

                            }


                        }
                        catch
                        {

                        }
                    }
                }
            }

            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    {
                        if (__itemName.Length > 0)
                        {
                            switch (__itemType)
                            {
                                case 3: // สินค้าจัดชุด
                                    {
                                        string __guid = System.Guid.NewGuid().ToString();
                                        this._cellUpdate(row, _g.d.ic_trans_detail._ref_guid, __guid, false);
                                        __query = new StringBuilder();
                                        __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_set_detail._ic_code + "," + _g.d.ic_inventory_set_detail._qty + "," + _g.d.ic_inventory_set_detail._price + "," + _g.d.ic_inventory_set_detail._unit_code + "," + _g.d.ic_inventory_set_detail._price_ratio +
                                            "," + " (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._unit_code + ") as " + _g.d.ic_inventory_set_detail._unit_name +
                                            "," + " (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code + ") as " + _g.d.ic_inventory_set_detail._ic_name +
                                            "," + " (select " + _g.d.ic_inventory._ic_serial_no + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code + ") as " + _g.d.ic_inventory._ic_serial_no +
                                            "," + " (select " + _g.d.ic_inventory._tax_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code + ") as " + "tax_type" +
                                            "," + " (select " + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code + ") as " + _g.d.ic_inventory_set_detail._item_type +
                                            "," + " (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=\'" + __whCode + "\') as " + _g.d.ic_inventory_set_detail._wh_name +
                                            "," + " (select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=\'" + __whCode + "\' and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=\'" + __shelfCode + "\') as " + _g.d.ic_inventory_set_detail._shelf_name +
                                            "," + " (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=\'" + __unitCode + "\') as " + _g.d.ic_inventory_set_detail._stand_value +
                                            "," + " (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=\'" + __unitCode + "\') as " + _g.d.ic_inventory_set_detail._divide_value +
                                            "," + _g.d.ic_inventory_set_detail._sum_amount +
                                            " from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + itemCode + "\' order by " + _g.d.ic_inventory_set_detail._line_number));
                                        __query.Append("</node>");
                                        __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                        DataTable __itemSetTable = ((DataSet)__dataResult[0]).Tables[0];
                                        int __rowEnd = __itemSetTable.Rows.Count;

                                        // ตัวแปรทำมาเพื่อตรวจสอบ อัตราส่วน สินค้าชุด
                                        decimal __total_set_price = 0M;

                                        for (int __row = 0; __row < __rowEnd; __row++)
                                        {
                                            string __itemCode2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._ic_code].ToString();
                                            string __itemName2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._ic_name].ToString();
                                            string __wareHoueName2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._wh_name].ToString();
                                            string __shelfName2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._shelf_name].ToString();
                                            string __unitCode2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._unit_code].ToString();
                                            string __unitName2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._unit_name].ToString();
                                            string __getQty2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._qty].ToString();
                                            decimal __refQty = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._qty].ToString());
                                            decimal __refPrice = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._price].ToString());
                                            decimal __priceSetRatio = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._price_ratio].ToString());
                                            int __itemType2 = MyLib._myGlobal._intPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._item_type].ToString());
                                            int __taxType2 = MyLib._myGlobal._intPhase(__itemSetTable.Rows[__row]["tax_type"].ToString());
                                            decimal __sum_amount = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._sum_amount].ToString());
                                            int __isSerialItem = (int)MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory._ic_serial_no].ToString());
                                            __total_set_price += __sum_amount;

                                            int __newRow = this._selectRow + __row + 1;
                                            this._addRow(__newRow);
                                            if (__row > 1 && this._colorQtyShow == false)
                                            {
                                                __refQty = 0M;
                                            }
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._is_get_price, (__itemType2 == 6) ? 0 : 1, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_code_main, itemCode, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_code, __itemCode2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_name, __itemName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._wh_code, __whCode, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._wh_name, __wareHoueName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._shelf_name, __shelfName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_name, __itemName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._unit_code, __unitCode2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._unit_name, __unitName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._set_ref_line, __guid, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._set_ref_qty, __refQty, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_type, __itemType2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._tax_type, __taxType2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._ref_row, -1, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._price_set_ratio, __priceSetRatio, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._is_serial_number, __isSerialItem, false);
                                            // ถ้าราคา -1 คือดึงราคาขาย
                                            if (__refPrice == -1)
                                            {
                                                _priceStruct __getPrice = this._findPrice(__newRow, __itemCode2, "", __unitCode2, __refQty, this._custCode, false, false, false);
                                                __refPrice = __getPrice._price;
                                                this._cellUpdate(__newRow, _g.d.ic_trans_detail._price_type, __getPrice._type, false);
                                                this._cellUpdate(__newRow, _g.d.ic_trans_detail._price_mode, __getPrice._mode, false);
                                                this._cellUpdate(__newRow, this._columnPriceRoworder, __getPrice._roworder, false);
                                                //__refPrice = ((_priceStruct)this._findPrice(__newRow, __itemType2, __itemCode2, __unitCode2, __refQty, this._custCode, "", false))._price;
                                            }
                                            decimal __standValue = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._stand_value].ToString());
                                            decimal __divideValue = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._divide_value].ToString());
                                            //
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._set_ref_price, __refPrice, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._stand_value, __standValue, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._divide_value, __divideValue, false);

                                            this._cellUpdateChangeStatus(__newRow, true);
                                        }

                                        // ตรวจสอบ อัตราส่วน
                                        if (_g.g._companyProfile._fix_item_set_price == true)
                                        {
                                            string __ref_guid = (string)this._cellGet(this._selectRow, _g.d.ic_trans_detail._ref_guid);
                                            for (int __row = 0; __row < this._rowData.Count; __row++)
                                            {
                                                string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                                                if (__set_ref_guid.Equals(__ref_guid))
                                                {
                                                    decimal __price = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._set_ref_price);
                                                    decimal __ratio = 0M;
                                                    try
                                                    {
                                                        __ratio = MyLib._myGlobal._round(__price / __total_set_price, 2);
                                                    }
                                                    catch
                                                    {
                                                    }
                                                    this._cellUpdate(__row, _g.d.ic_trans_detail._price_set_ratio, __ratio, false);
                                                }
                                            }

                                            // ตรวจสอบ การปัดเศษ
                                            decimal __calcRate = 0M;
                                            for (int __row = 0; __row < this._rowData.Count; __row++)
                                            {
                                                string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                                                if (__set_ref_guid.Equals(__ref_guid))
                                                {
                                                    __calcRate += (decimal)this._cellGet(__row, _g.d.ic_trans_detail._set_ref_qty) * (decimal)this._cellGet(__row, _g.d.ic_trans_detail._price_set_ratio);
                                                }
                                            }

                                            decimal __diff_value = 1 - __calcRate;

                                            if (__diff_value != 0)
                                            {
                                                for (int __row = this._rowData.Count; __row > 0; __row--)
                                                {
                                                    string __set_ref_guid = this._cellGet(__row, _g.d.ic_trans_detail._set_ref_line).ToString();
                                                    if (__set_ref_guid.Equals(__ref_guid))
                                                    {
                                                        decimal __last_item_rato = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._price_set_ratio);
                                                        this._cellUpdate(__row, _g.d.ic_trans_detail._price_set_ratio, (__last_item_rato + __diff_value), false);
                                                        break;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    break;
                                case 5: // สูตรสี
                                    {
                                        string __guid = System.Guid.NewGuid().ToString();
                                        this._cellUpdate(row, _g.d.ic_trans_detail._ref_guid, __guid, false);

                                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                                        {
                                            _checkSyncColor(itemCode);
                                        }
                                        __query = new StringBuilder();
                                        __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_set_detail._ic_code + "," + _g.d.ic_inventory_set_detail._qty + "," + _g.d.ic_inventory_set_detail._price + "," + _g.d.ic_inventory_set_detail._unit_code + "," +
                                            " (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._unit_code + ") as " + _g.d.ic_inventory_set_detail._unit_name + "," + " (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code + ") as " + _g.d.ic_inventory_set_detail._ic_name + "," + " (select " + _g.d.ic_inventory._tax_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code + ") as " + "tax_type" + "," + " (select " + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code + ") as " + _g.d.ic_inventory_set_detail._item_type + "," + " (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=\'" + __whCode + "\') as " + _g.d.ic_inventory_set_detail._wh_name + "," + " (select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=\'" + __whCode + "\' and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=\'" + __shelfCode + "\') as " + _g.d.ic_inventory_set_detail._shelf_name + "," + " (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._unit_code + ") as " + _g.d.ic_inventory_set_detail._stand_value + "," + " (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._unit_code + ") as " + _g.d.ic_inventory_set_detail._divide_value + " from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + itemCode + "\' order by " + _g.d.ic_inventory_set_detail._line_number));
                                        __query.Append("</node>");
                                        __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                        DataTable __itemSetTable = ((DataSet)__dataResult[0]).Tables[0];
                                        // กรณีสินค้าชุด กรณี == 5 คือ เป็นสี เอาสองบรรทัดแรก
                                        int __rowEnd = (this._fixedItemSetRow && __itemSetTable.Rows.Count > 0) ? 2 : __itemSetTable.Rows.Count;
                                        for (int __row = 0; __row < __rowEnd; __row++)
                                        {
                                            string __itemCode2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._ic_code].ToString();
                                            string __itemName2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._ic_name].ToString();
                                            string __wareHoueName2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._wh_name].ToString();
                                            string __shelfName2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._shelf_name].ToString();
                                            string __unitCode2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._unit_code].ToString();
                                            string __unitName2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._unit_name].ToString();
                                            string __getQty2 = __itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._qty].ToString();
                                            decimal __refQty = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._qty].ToString());
                                            decimal __refPrice = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._price].ToString());
                                            int __itemType2 = MyLib._myGlobal._intPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._item_type].ToString());
                                            int __taxType2 = MyLib._myGlobal._intPhase(__itemSetTable.Rows[__row]["tax_type"].ToString());

                                            int __newRow = this._selectRow + __row + 1;
                                            this._addRow(__newRow);
                                            if (__row > 1 && this._colorQtyShow == false)
                                            {
                                                __refQty = 0M;
                                            }
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._is_get_price, (__itemType2 == 6) ? 0 : 1, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_code_main, itemCode, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_code, __itemCode2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_name, __itemName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._wh_code, __whCode, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._wh_name, __wareHoueName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._shelf_code, __shelfCode, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._shelf_name, __shelfName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_name, __itemName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._unit_code, __unitCode2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._unit_name, __unitName2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._set_ref_line, __guid, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._set_ref_qty, __refQty, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_type, __itemType2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._tax_type, __taxType2, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._ref_row, -1, false);
                                            // ถ้าราคา -1 คือดึงราคาขาย
                                            if (__refPrice == -1)
                                            {
                                                _priceStruct __getPrice = this._findPrice(__newRow, __itemCode2, "", __unitCode2, __refQty, this._custCode, false, false, false);
                                                __refPrice = __getPrice._price;
                                                this._cellUpdate(__newRow, _g.d.ic_trans_detail._price_type, __getPrice._type, false);
                                                this._cellUpdate(__newRow, _g.d.ic_trans_detail._price_mode, __getPrice._mode, false);
                                                this._cellUpdate(__newRow, this._columnPriceRoworder, __getPrice._roworder, false);
                                                //__refPrice = ((_priceStruct)this._findPrice(__newRow, __itemType2, __itemCode2, __unitCode2, __refQty, this._custCode, "", false))._price;
                                            }
                                            decimal __standValue = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._stand_value].ToString());
                                            decimal __divideValue = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._divide_value].ToString());
                                            //
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._set_ref_price, __refPrice, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._stand_value, __standValue, false);
                                            this._cellUpdate(__newRow, _g.d.ic_trans_detail._divide_value, __divideValue, false);

                                            if (this._icTransControlType == _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี)
                                            {
                                                // ดึงราคาซื้อ
                                                string __queryPurchasePrice = "select " + _g.d.ic_inventory_purchase_price._unit_code + "," + _g.d.ic_inventory_purchase_price._sale_price1 + " from " + _g.d.ic_inventory_purchase_price._table + " where " + _g.d.ic_inventory_purchase_price._table + "." + _g.d.ic_inventory_purchase_price._ic_code + "=\'" + __itemCode2 + "\'";
                                                DataTable __getPurchasePrice = __myFrameWork._queryShort(__queryPurchasePrice).Tables[0];
                                                if (__getPurchasePrice.Rows.Count > 0)
                                                {
                                                    decimal __price = MyLib._myGlobal._decimalPhase(__getPurchasePrice.Rows[0][_g.d.ic_inventory_purchase_price._sale_price1].ToString());
                                                    string __remark = String.Format("{0}/{1}", String.Format("{0:0.00}", __price), __getPurchasePrice.Rows[0][_g.d.ic_inventory_purchase_price._unit_code].ToString());
                                                    this._cellUpdate(__newRow, _g.d.ic_trans_detail._remark, __remark, false);
                                                }
                                            }
                                            this._cellUpdateChangeStatus(__newRow, true);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }



            // toe check costType = 1 show lot
            // Lot
            // toe เอาออก ( MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS  && __costType == 1 )||
            if ((__costType == 1 && (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccount && MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccountProfessional && MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLAccountPOS)) ||
                ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional) && __costType == 2))
            {

                //if (((this._ictransControlTemp == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._ictransControlTemp == _g.g._transControlTypeEnum.ขาย_สั่งขาย) && (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")) == false)
                //{
                //}
                if (this._lot != null)
                {
                    bool __loadLotControl = (((this._ictransControlTemp == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._ictransControlTemp == _g.g._transControlTypeEnum.ขาย_สั่งขาย) && (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")) == false);
                    this._lot(__loadLotControl);
                }
            }
            return __found;
        }

        public decimal _findCostByItemSet(string itemCodeMain, decimal qty)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            decimal __result = 0;
            // สูตรสีผสมให้คำนวณราคาใหม่
            DataTable __getFormula = __myFrameWork._queryShort("select " + _g.d.ic_inventory_set_detail._ic_code + "," + _g.d.ic_inventory_set_detail._qty + "," + _g.d.ic_inventory_set_detail._price + "," + _g.d.ic_inventory_set_detail._unit_code + " from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + itemCodeMain + "\' order by " + _g.d.ic_inventory_set_detail._line_number).Tables[0];
            for (int __row = 2; __row < __getFormula.Rows.Count; __row++)
            {
                string __itemCodeForPrice = __getFormula.Rows[__row][_g.d.ic_inventory_set_detail._ic_code].ToString();
                string __itemUnitCode = __getFormula.Rows[__row][_g.d.ic_inventory_set_detail._unit_code].ToString();
                // ปัดจำนวนขึ้นก่อน
                decimal __qtyFormula = Math.Ceiling(MyLib._myGlobal._decimalPhase(__getFormula.Rows[__row][_g.d.ic_inventory_set_detail._qty].ToString()));
                // ดึงต้นทุนแม่สี
                DataTable __getPrice = __myFrameWork._queryShort("select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._average_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCodeForPrice + "\'").Tables[0];
                Decimal __cost = 0.0M;
                if (__getPrice.Rows.Count > 0)
                {
                    __cost = MyLib._myGlobal._round(MyLib._myGlobal._decimalPhase(__getPrice.Rows[0][_g.d.ic_inventory._average_cost].ToString()), 3);
                    Decimal __qty = MyLib._myGlobal._decimalPhase(__getFormula.Rows[0][_g.d.ic_inventory_set_detail._qty].ToString());
                    Decimal __qty2 = __qty * __qtyFormula;
                    __result += (__qty2 * __cost) * qty;
                }
            }
            return __result;
        }

        /// <summary>
        /// ค้นหาราคาตามสูตรสี
        /// </summary>
        /// <param name="itemCodeMain">รหัสสูตรสี</param>
        /// <param name="qty">จำนวนเพื่อเป็นเงื่อนไขในการค้นหาราคา</param>
        /// <returns></returns>
        public decimal _findAmountByItemSet(string itemCodeMain, string custCode, decimal qty)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            decimal __result = 0;
            // สูตรสีผสมให้คำนวณราคาใหม่
            DataTable __getFormula = __myFrameWork._queryShort("select " + _g.d.ic_inventory_set_detail._ic_code + "," + _g.d.ic_inventory_set_detail._qty + "," + _g.d.ic_inventory_set_detail._price + "," + _g.d.ic_inventory_set_detail._unit_code + " from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + itemCodeMain + "\' order by " + _g.d.ic_inventory_set_detail._line_number).Tables[0];
            for (int __row = 2; __row < __getFormula.Rows.Count; __row++)
            {
                string __itemCodeForPrice = __getFormula.Rows[__row][_g.d.ic_inventory_set_detail._ic_code].ToString();
                string __itemUnitCode = __getFormula.Rows[__row][_g.d.ic_inventory_set_detail._unit_code].ToString();
                // ปัดจำนวนขึ้นก่อน
                decimal __qtyFormula = Math.Ceiling(MyLib._myGlobal._decimalPhase(__getFormula.Rows[__row][_g.d.ic_inventory_set_detail._qty].ToString()));
                // ดึงราคาแม่สี
                /*string __today = (this._getDocDate == null) ? MyLib._myGlobal._convertDateToQuery(DateTime.Now) : MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._getDocDate(this)));
                string __todayCondition = "(\'" + __today + "\' >= " + _g.d.ic_inventory_price._from_date + " and \'" + __today + "\' <= " + _g.d.ic_inventory_price._to_date + ")";
                string __query = "select " + _g.d.ic_inventory_price._ic_code + "," + _g.d.ic_inventory_price._sale_price1 + "," + _g.d.ic_inventory_price._sale_price2 + " from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._ic_code + "=\'" + __itemCodeForPrice + "\' and " + _g.d.ic_inventory_price._unit_code + "=\'" + __itemUnitCode + "\' and " + __todayCondition;
                DataTable __getPrice = __myFrameWork._queryShort(__query).Tables[0];*/
                _priceStruct __getPrice = this._findPriceByItem(__itemCodeForPrice, "", __itemUnitCode, __qtyFormula, custCode, "", false, "");
                Decimal __price = 0.0M;
                if (__getPrice._foundPrice)
                {
                    switch (this._vatType(this))
                    {
                        case _g.g._vatTypeEnum.ภาษีแยกนอก:
                            __price = MyLib._myGlobal._round(__getPrice._price1, 3);
                            break;
                        case _g.g._vatTypeEnum.ภาษีรวมใน:
                            __price = MyLib._myGlobal._round(__getPrice._price2, 3);
                            break;
                    }
                    Decimal __qty = MyLib._myGlobal._decimalPhase(__getFormula.Rows[0][_g.d.ic_inventory_set_detail._qty].ToString());
                    // ปัดจำนวนขึ้น
                    Decimal __qty2 = Math.Ceiling(__qty * __qtyFormula);
                    __result += (__qty2 * __price) * qty;
                }
            }
            return __result;
        }

        public _priceStruct _findPriceByItem(string itemCode, string barcode, string unitCode, decimal qty, string custCode, string memberCode, Boolean approve, string getUserApprove)
        {
            return this._findPriceByItem(itemCode, barcode, unitCode, qty, custCode, memberCode, approve, getUserApprove, 0, 0);
        }

        public _priceStruct _findPriceByItem(string itemCode, string barcode, string unitCode, decimal qty, string custCode, string memberCode, Boolean approve, string getUserApprove, int sale_type, int transport_type)
        {
            string __today = (this._getDocDate == null) ? MyLib._myGlobal._convertDateToQuery(DateTime.Now) : MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._getDocDate(this)));

            if (_g.g._companyProfile._use_price_center)
            {

                int __saleType = 0;
                switch (this._ictransControlTemp)
                {
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        {

                            int __saleInquiryType = (int)MyLib._myGlobal._decimalPhase(this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type));
                            if (__saleInquiryType == 0 || __saleInquiryType == 2)
                            {
                                // ขายเชื่อ
                                __saleType = 2;
                            }
                            else
                                __saleType = 1;

                        }
                        break;

                }

                _findPriceCenter __priceCenter = new _findPriceCenter();
                _priceStruct __resultCenter = __priceCenter._findPrice(this._getBranchCode(), itemCode, barcode, unitCode, qty, custCode, memberCode, approve, getUserApprove, __today, this._vatType(this), this._vatRate(), __saleType, transport_type);
                return __resultCenter;
            }

            _findPriceClass __price = new _findPriceClass();
            _priceStruct __result = __price._findPriceByItem(itemCode, barcode, unitCode, qty, custCode, memberCode, approve, getUserApprove, __today, this._vatType(this), this._vatRate(), sale_type, transport_type);

            return __result;
        }

        public _priceStruct _findPrice(int row, string itemCode, string barcode, string unitCode, decimal qty, string custCode, Boolean approve, Boolean warning1, Boolean warning2)
        {

            /* Step
             * 1.หาราคาตามลูกค้า+สินค้า+วันที่
             * 2.หาราคาตามกลุ่มลูกค้า+วันที่
             * 3.ราคาขายทั่วไป
             * 4.หาราคาขายทั่วไป+วันที่
             * 5.หาราคาตาม Barcode
             * pricetype (1=ราคาทั่วไป,2=ราคาตามกลุ่มลูกค้า,3=ราคาตามกลุ่มสินค้า)
             * pricemode (0=ราคาทั่วไป,1=ราคายืน (มาตรฐาน))
             */
            _priceStruct __result = new _priceStruct();
            int __columnGetPrice = this._findColumnByName(_g.d.ic_trans_detail._is_get_price);
            int __getPrice = (__columnGetPrice == -1) ? 1 : MyLib._myGlobal._intPhase(this._cellGet(row, _g.d.ic_trans_detail._is_get_price).ToString());
            Boolean __foundPrice = false;
            if (__getPrice == 1)
            {
                {
                    string __getUserApprove = this._cellGet(row, _g.d.ic_trans_detail._user_approve).ToString();

                    // โต๋เพิ่ม กรองราคา จากประเภทการขายและ ประเภทการขนส่ง
                    int __sale_type = 0;

                    if (this._icTransControlType != _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี)
                    {
                        __sale_type = (int)MyLib._myGlobal._decimalPhase(this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type));
                    }

                    __result = this._findPriceByItem(itemCode, barcode, unitCode, qty, custCode, "", approve, __getUserApprove, __sale_type, 0);
                    this._cellUpdate(row, _g.d.ic_trans_detail._user_approve, __result._user_approve, false);
                    __foundPrice = __result._foundPrice;
                }
            }
            if (warning1 && _g.g._companyProfile._warning_price_1 && __foundPrice == false)
            {
                // กรณี ItemType=5 (สินค้าชุด) ไม่ต้องเตือน
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                int __itemType = 0;
                string __query = "select " + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + itemCode + "\'";
                DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                if (__getData.Rows.Count > 0)
                {
                    __itemType = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.ic_inventory._item_type].ToString());
                }
                // เตือนเมื่อไม่พบราคา (กรณีสินค้าชุดหรือสูตรสีไม่เตือน)
                if (__itemType != 3 && __itemType != 5)
                {
                    if (this._importWorking == false)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบราคาสินค้า"));
                    }
                }
            }
            if (warning2 && _g.g._companyProfile._warning_price_2 && __foundPrice == true && __result._price == 0.0M)
            {
                // เตือนกรณีภาษีแยกนอก และรวมใน ให้คำนวณกลับไปมา
                if (this._vatType(this) == _g.g._vatTypeEnum.ภาษีแยกนอก && __result._price2 != 0.0M)
                {
                    DialogResult __selectPrice = MessageBox.Show(MyLib._myGlobal._resource("ไม่พบราคาแยกนอก พบแต่ราคารวมใน คือ") + " : " + __result._price2.ToString() + " " + MyLib._myGlobal._resource("ต้องการใช้ราคานี้หรือไม่"), "", MessageBoxButtons.YesNo);
                    if (__selectPrice == DialogResult.Yes)
                    {
                        __result._price = (__result._price2 * 100.0M) / (100.0M + this._vatRate());
                    }
                }
                if (this._vatType(this) == _g.g._vatTypeEnum.ภาษีรวมใน && __result._price1 != 0.0M)
                {
                    DialogResult __selectPrice = MessageBox.Show(MyLib._myGlobal._resource("ไม่พบราคารวมใน พบแต่ราคาแยกนอก คือ") + " : " + __result._price1.ToString() + " " + MyLib._myGlobal._resource("ต้องการใช้ราคานี้หรือไม่"), "", MessageBoxButtons.YesNo);
                    if (__selectPrice == DialogResult.Yes)
                    {
                        __result._price = (__result._price1 * (100.0M + this._vatRate())) / 100.0M;
                    }
                }
            }
            return __result;
        }

        public decimal _findPurchasePrice(int row, string itemCode, string unitCode, decimal qty, string apCode)
        {

            decimal __result = 0;
            string __today = MyLib._myGlobal._convertDateToQuery(DateTime.Now);
            string __todayCondition = "(\'" + __today + "\' >= " + _g.d.ic_inventory_price._from_date + " and \'" + __today + "\' <= " + _g.d.ic_inventory_price._to_date + ")";
            string __qtyCondition = "(" + qty.ToString() + " >= " + _g.d.ic_inventory_price._from_qty + " and " + qty.ToString() + " <= " + _g.d.ic_inventory_price._to_qty + ")";
            string __fieldPrice = (this._vatType(this) == _g.g._vatTypeEnum.ภาษีแยกนอก) ? _g.d.ic_inventory_purchase_price._sale_price1 : _g.d.ic_inventory_purchase_price._sale_price2;
            Boolean __foundPrice = false;
            int __sale_type = (int)MyLib._myGlobal._decimalPhase(this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type));

            if ((int)this._cellGet(row, _g.d.ic_trans_detail._is_permium) != 1)
            {
                // toe ไปเรียก class _findPurchasePriceClass แทน
                /*
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                // หาตามเจ้าหนี้
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_purchase_price._sale_price1 + "," + _g.d.ic_inventory_purchase_price._sale_price2 + " from " + _g.d.ic_inventory_purchase_price._table + " where " + _g.d.ic_inventory_purchase_price._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_purchase_price._supplier_code + "=\'" + apCode + "\' and " + _g.d.ic_inventory_purchase_price._unit_code + "=\'" + unitCode + "\' and " + __todayCondition + " and " + __qtyCondition + " and " + _g.d.ic_inventory_purchase_price._price_type + "=3"));
                // ราคาซื้อทั่วไป
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_purchase_price._sale_price1 + "," + _g.d.ic_inventory_purchase_price._sale_price2 + " from " + _g.d.ic_inventory_purchase_price._table + " where " + _g.d.ic_inventory_purchase_price._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_purchase_price._unit_code + "=\'" + unitCode + "\' and " + __todayCondition + " and " + __qtyCondition + " and " + _g.d.ic_inventory_purchase_price._price_type + "=1"));
                __query.Append("</node>");
                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                {
                    // หาตามเจ้าหนี้
                    DataTable __itemSetTable = ((DataSet)__dataResult[0]).Tables[0];
                    if (__itemSetTable.Rows.Count > 0)
                    {
                        __result = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice].ToString());
                        __foundPrice = true;
                    }
                }
                if (__foundPrice == false)
                {
                    // ราคาซื้อทั่วไป
                    DataTable __itemSetTable = ((DataSet)__dataResult[1]).Tables[0];
                    if (__itemSetTable.Rows.Count > 0)
                    {
                        __result = MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[0][__fieldPrice].ToString());
                        __foundPrice = true;
                    }
                }
                 * */
                _findPurchasePriceClass __price = new _findPurchasePriceClass();
                _priceStruct __priceStruct = __price._findPurchasePrice(itemCode, unitCode, qty, apCode, __today, this._vatType(this), this._vatRate(), __sale_type);
                if (__priceStruct._foundPrice == true)
                {
                    __result = __priceStruct._price;
                }
            }
            return __result;
        }

        public void _findPriceAll()
        {
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    for (int __row = 0; __row < this._rowData.Count; __row++)
                    {
                        int __itemType = (int)this._cellGet(__row, _g.d.ic_trans_detail._item_type);
                        if (__itemType != 3 && __itemType != 5)
                        {
                            string __itemCode = this._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                            string __barcode = this._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                            string __unitCode = this._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString();
                            decimal __qty = MyLib._myGlobal._decimalPhase(this._cellGet(__row, _g.d.ic_trans_detail._qty).ToString());
                            if (__qty != 0)
                            {
                                _priceStruct __getPrice = this._findPrice(__row, __itemCode, __barcode, __unitCode, __qty, this._custCode, false, false, false);
                                this._cellUpdate(__row, _g.d.ic_trans_detail._price_type, __getPrice._type, false);
                                this._cellUpdate(__row, _g.d.ic_trans_detail._price_mode, __getPrice._mode, false);
                                this._cellUpdate(__row, this._columnPriceRoworder, __getPrice._roworder, false);
                                //decimal __price = ((_priceStruct)this._findPrice(__row, __itemType, __itemCode, __unitCode, __qty, this._custCode, "", false))._price;
                                this._cellUpdate(__row, _g.d.ic_trans_detail._set_ref_price, __getPrice._price, false);
                                this._cellUpdate(__row, _g.d.ic_trans_detail._price, __getPrice._price, true);
                                _calcItemSetNow(__row);
                            }
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    for (int __row = 0; __row < this._rowData.Count; __row++)
                    {
                        int __itemType = (int)this._cellGet(__row, _g.d.ic_trans_detail._item_type);
                        if (__itemType != 3 && __itemType != 5)
                        {
                            string __itemCode = this._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                            string __unitCode = this._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString();
                            decimal __qty = MyLib._myGlobal._decimalPhase(this._cellGet(__row, _g.d.ic_trans_detail._qty).ToString());
                            if (__qty != 0)
                            {
                                decimal __price = this._findPurchasePrice(__row, __itemCode, __unitCode, __qty, this._custCode);
                                this._cellUpdate(__row, _g.d.ic_trans_detail._set_ref_price, __price, false);
                                this._cellUpdate(__row, _g.d.ic_trans_detail._price, __price, true);
                                _calcItemSetNow(__row);
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// คำนวณหาจำนวนของสีผสม
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public decimal _calcFormulaForColor(string itemCode, decimal qty)
        {
            decimal __result = 0;
            if (qty != 0)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(
                    "select " + _g.d.ic_inventory_set_detail._qty + "," + _g.d.ic_inventory_set_detail._price + "," + _g.d.ic_inventory_set_detail._unit_code + " from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + itemCode + "\' order by " + _g.d.ic_inventory_set_detail._line_number));
                __query.Append("</node>");
                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                DataTable __itemSetTable = ((DataSet)__dataResult[0]).Tables[0];
                for (int __row = 2; __row < __itemSetTable.Rows.Count; __row++)
                {
                    // ปัดขั้น
                    __result += qty * Math.Ceiling(MyLib._myGlobal._decimalPhase(__itemSetTable.Rows[__row][_g.d.ic_inventory_set_detail._qty].ToString()));
                }
            }
            return __result;
        }

        public void _calcItemPrice(int selectRow, int row, int column)
        {
            int __itemCodeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._item_code);
            int __unitCodeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._unit_code);
            int __qtyColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._qty);
            int __priceColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._price);
            int __totalColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._sum_amount);
            int __discountColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._discount);
            int __averageCostColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._average_cost);
            int __itemType = (int)this._cellGet(row, _g.d.ic_trans_detail._item_type);

            // multi currency
            int __priceColumnNumber2 = this._findColumnByName(_g.d.ic_trans_detail._price_2);
            int __totalColumnNumber2 = this._findColumnByName(_g.d.ic_trans_detail._sum_amount_2);

            decimal __exchangeRate = this._getExchangeRate();

            decimal __qty = (decimal)this._cellGet(row, __qtyColumnNumber);
            decimal __qty4 = (decimal)this._cellGet(selectRow, __qtyColumnNumber);
            decimal __price = (__priceColumnNumber == -1) ? 0 : (decimal)this._cellGet(row, __priceColumnNumber);
            decimal __amount = (__totalColumnNumber == -1) ? 0 : (decimal)this._cellGet(row, __totalColumnNumber);

            decimal __price2 = (__priceColumnNumber2 == -1) ? 0 : (decimal)this._cellGet(row, __priceColumnNumber2);
            decimal __amount2 = (__totalColumnNumber2 == -1) ? 0 : (decimal)this._cellGet(row, __totalColumnNumber2);

            //
            string __discountWord = (__discountColumnNumber == -1) ? "0.0" : this._cellGet(row, __discountColumnNumber).ToString();
            Boolean __update = false;
            if (column == __qtyColumnNumber)
            {
                string __guid = this._cellGet(row, _g.d.ic_trans_detail._ref_guid).ToString();
                if (__qtyColumnNumber != -1 && __priceColumnNumber != -1 && __totalColumnNumber != -1)
                {
                    if ((__itemType == 3 || __itemType == 5) && __guid.Length > 0)
                    {
                        switch (__itemType)
                        {
                            case 3: // สินค้าชุด
                                {
                                    __amount = 0;
                                    for (int __refRowLoop = 0; __refRowLoop < this._rowData.Count; __refRowLoop++)
                                    {
                                        if (this._cellGet(__refRowLoop, _g.d.ic_trans_detail._set_ref_line).ToString().Equals(__guid))
                                        {
                                            string __itemCode2 = this._cellGet(__refRowLoop, __itemCodeColumnNumber).ToString();
                                            string __unitCode2 = this._cellGet(__refRowLoop, __unitCodeColumnNumber).ToString();
                                            decimal __qty5 = (decimal)this._cellGet(__refRowLoop, _g.d.ic_trans_detail._set_ref_qty);
                                            decimal __newQty = 0M;
                                            if (selectRow == row)
                                            {
                                                // กรณีแก้ที่หัวบรรทัด
                                                __newQty = __qty5 * __qty;
                                            }
                                            else
                                                if (selectRow == __refRowLoop)
                                            {
                                                // กรณี แก้จำนวนในบรรทัดย่อย
                                                __newQty = __qty4;
                                            }
                                            else
                                            {
                                                // บรรทัดอื่นๆ จำนวนเท่าเดิม
                                                __newQty = (decimal)this._cellGet(__refRowLoop, __qtyColumnNumber);
                                            }

                                            // โต๋ เพิ่มให้ส่ง barcode ไปหาด้วย
                                            string __barcode2 = (this._cellGet(__refRowLoop, _g.d.ic_trans_detail._barcode) != null) ? (string)this._cellGet(__refRowLoop, _g.d.ic_trans_detail._barcode) : "";

                                            _priceStruct __getPrice = this._findPrice(__refRowLoop, __itemCode2, "", __unitCode2, __newQty, this._custCode, true, false, true);
                                            //_priceStruct __getPrice = this._findPrice(__refRowLoop, __itemCode2, __unitCode2, __newQty, this._custCode, true, false, false);
                                            decimal __newPrice = __getPrice._price;
                                            this._cellUpdate(__refRowLoop, _g.d.ic_trans_detail._price_type, __getPrice._type, false);
                                            this._cellUpdate(__refRowLoop, _g.d.ic_trans_detail._price_mode, __getPrice._mode, false);
                                            this._cellUpdate(__refRowLoop, this._columnPriceRoworder, __getPrice._roworder, false);

                                            // toe check option fix price
                                            if (_g.g._companyProfile._fix_item_set_price == true)
                                            {
                                                __newPrice = (decimal)this._cellGet(__refRowLoop, _g.d.ic_trans_detail._set_ref_price);
                                            }

                                            string __discountWord2 = (__discountColumnNumber == -1) ? "0.0" : this._cellGet(__refRowLoop, __discountColumnNumber).ToString();
                                            //
                                            decimal __newAmount = MyLib._myGlobal._calcAfterDiscount(__discountWord2, MyLib._myGlobal._round(__newQty * __newPrice, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, __qty);
                                            this._cellUpdate(__refRowLoop, __qtyColumnNumber, __newQty, false);
                                            this._cellUpdate(__refRowLoop, __priceColumnNumber, __newPrice, false);
                                            this._cellUpdate(__refRowLoop, __totalColumnNumber, __newAmount, false);
                                            __amount += __newAmount;
                                            // ดึงต้นทุน
                                            if (__averageCostColumnNumber != -1)
                                            {
                                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                                string __query = "select " + _g.d.ic_inventory._average_cost + ","
                                                + "(select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + this._columnAverageCostUnitStand + ","
                                                + "(select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + this._columnAverageCostUnitDiv
                                                + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._cellGet(__refRowLoop, _g.d.ic_trans_detail._item_code).ToString() + "\'";
                                                DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                                                if (__getData.Rows.Count > 0)
                                                {
                                                    decimal __averageCost = MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.ic_inventory._average_cost].ToString());
                                                    decimal __standValue = MyLib._myGlobal._decimalPhase(__getData.Rows[0][this._columnAverageCostUnitStand].ToString());
                                                    decimal __divideValue = MyLib._myGlobal._decimalPhase(__getData.Rows[0][this._columnAverageCostUnitDiv].ToString());
                                                    this._cellUpdate(__refRowLoop, __averageCostColumnNumber, __averageCost, false);
                                                    this._cellUpdate(__refRowLoop, this._columnAverageCostUnitStand, __standValue, false);
                                                    this._cellUpdate(__refRowLoop, this._columnAverageCostUnitDiv, __divideValue, false);
                                                }
                                            }
                                        }
                                    }
                                    // คำนวณ amount ก่อน price เพราะยอดรวมต้องเท่ากับยอดรวมชุด
                                    __amount = MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__amount, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal);
                                    __price = (__qty == 0) ? 0 : MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round((__amount / __qty), _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
                                }
                                break;
                            case 5: // สูตรสี
                                {
                                    __amount = 0;
                                    for (int __refRowLoop = 0; __refRowLoop < this._rowData.Count; __refRowLoop++)
                                    {
                                        if (this._cellGet(__refRowLoop, _g.d.ic_trans_detail._set_ref_line).ToString().Equals(__guid))
                                        {
                                            string __itemCode2 = this._cellGet(__refRowLoop, __itemCodeColumnNumber).ToString();
                                            string __unitCode2 = this._cellGet(__refRowLoop, __unitCodeColumnNumber).ToString();
                                            decimal __newQty = (decimal)this._cellGet(__refRowLoop, _g.d.ic_trans_detail._set_ref_qty) * __qty;

                                            // โต๋ เพิ่มให้ส่ง barcode ไปหาด้วย
                                            string __barcode2 = (this._cellGet(__refRowLoop, _g.d.ic_trans_detail._barcode) != null) ? (string)this._cellGet(__refRowLoop, _g.d.ic_trans_detail._barcode) : "";

                                            _priceStruct __getPrice = this._findPrice(__refRowLoop, __itemCode2, "", __unitCode2, __newQty, this._custCode, true, false, true);
                                            //_priceStruct __getPrice = this._findPrice(__refRowLoop, __itemCode2, __unitCode2, __newQty, this._custCode, true, false, false);
                                            decimal __newPrice = __getPrice._price;
                                            this._cellUpdate(__refRowLoop, _g.d.ic_trans_detail._price_type, __getPrice._type, false);
                                            this._cellUpdate(__refRowLoop, _g.d.ic_trans_detail._price_mode, __getPrice._mode, false);
                                            this._cellUpdate(__refRowLoop, this._columnPriceRoworder, __getPrice._roworder, false);
                                            //decimal __newPrice = (decimal)this._cellGet(__refRowLoop, _g.d.ic_trans_detail._set_ref_price);
                                            string __discountWord2 = (__discountColumnNumber == -1) ? "0.0" : this._cellGet(__refRowLoop, __discountColumnNumber).ToString();
                                            // กรณีสี คำนวณยอดคงเหลือใหม่
                                            if ((int)this._cellGet(__refRowLoop, _g.d.ic_trans_detail._item_type) == 6)
                                            {
                                                // กรณีสีผสม ให้ไปเข้าสูตรหาจำนวน
                                                string __itemCodeMain = this._cellGet(__refRowLoop, _g.d.ic_trans_detail._item_code_main).ToString();
                                                __newQty = _calcFormulaForColor(__itemCodeMain, __qty);
                                                // คำนวณราคาสีผสมใหม่
                                                decimal __getAmount = _findAmountByItemSet(__itemCodeMain, this._custCode, __qty);
                                                __newPrice = (__newQty == 0) ? 0 : MyLib._myGlobal._round((__getAmount / __newQty), _g.g._companyProfile._item_price_decimal);
                                                //__newPrice = (__newQty == 0) ? 0 : MyLib._myGlobal._ceiling((_findAmountByItemSet(__itemCodeMain, __qty) / __newQty), _g.g._companyProfile._item_price_decimal);
                                            }
                                            //
                                            decimal __newAmount = MyLib._myGlobal._calcAfterDiscount(__discountWord2, MyLib._myGlobal._round(__newQty * __newPrice, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal);
                                            this._cellUpdate(__refRowLoop, __qtyColumnNumber, __newQty, false);
                                            this._cellUpdate(__refRowLoop, __priceColumnNumber, __newPrice, false);
                                            this._cellUpdate(__refRowLoop, __totalColumnNumber, __newAmount, false);
                                            __amount += __newAmount;
                                            // ดึงต้นทุน
                                            if (__averageCostColumnNumber != -1)
                                            {
                                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                                string __query = "select " + _g.d.ic_inventory._average_cost + ","
                                                + "(select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + this._columnAverageCostUnitStand + ","
                                                + "(select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + this._columnAverageCostUnitDiv
                                                + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._cellGet(__refRowLoop, _g.d.ic_trans_detail._item_code).ToString() + "\'";
                                                DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                                                if (__getData.Rows.Count > 0)
                                                {
                                                    decimal __averageCost = MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.ic_inventory._average_cost].ToString());
                                                    decimal __standValue = MyLib._myGlobal._decimalPhase(__getData.Rows[0][this._columnAverageCostUnitStand].ToString());
                                                    decimal __divideValue = MyLib._myGlobal._decimalPhase(__getData.Rows[0][this._columnAverageCostUnitDiv].ToString());
                                                    this._cellUpdate(__refRowLoop, __averageCostColumnNumber, __averageCost, false);
                                                    this._cellUpdate(__refRowLoop, this._columnAverageCostUnitStand, __standValue, false);
                                                    this._cellUpdate(__refRowLoop, this._columnAverageCostUnitDiv, __divideValue, false);
                                                }
                                            }
                                        }
                                    }
                                    // คำนวณ amount ก่อน price เพราะยอดรวมต้องเท่ากับยอดรวมชุด
                                    __amount = MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__amount, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal);
                                    __price = (__qty == 0) ? 0 : MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round((__amount / __qty), _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
                                }
                                break;
                        }
                    }
                    else
                    {

                        __amount = _calcItemAmount(__discountWord, __qty, __price);  // โต๋ย้าย เข้า function MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__qty * __price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, __qty);
                        __amount2 = _calcItemAmount(__discountWord, __qty, __price2);
                    }
                }
                __update = true;
            }
            else
                if (column == __priceColumnNumber)
            {
                __amount = _calcItemAmount(__discountWord, __qty, __price);  // โต๋ย้าย เข้า function MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__qty * __price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, __qty);
                __update = true;
            }
            else if (column == __priceColumnNumber2)
            {
                __amount2 = _calcItemAmount(__discountWord, __qty, __price2);  // โต๋ย้าย เข้า function MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__qty * __price2, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, __qty);
                __price = (__exchangeRate == 0) ? __price2 : MyLib._myGlobal._round(__price2 * __exchangeRate, _g.g._companyProfile._item_amount_decimal);
                __amount = _calcItemAmount(__discountWord, __qty, __price);  // โต๋ย้าย เข้า function  MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__qty * __price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, __qty);

                __update = true;
                //__amount = MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__qty * __price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, __qty);
                //__update = true;

            }
            else
                    /*if (column == __totalColumnNumber)
                    {
                        __price = 0;
                        if (__qty != 0)
                        {
                            decimal __amountCalc = __price * __qty;
                            __price = MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__amount / __qty, _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
                        }
                        __update = true;
                    }
                    else*/
                    if (column == __discountColumnNumber)
            {
                __amount = _calcItemAmount(__discountWord, __qty, __price);  // โต๋ย้าย เข้า function MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__qty * __price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, __qty);
                __update = true;
            }
            if (__update)
            {
                this._cellUpdate(row, __qtyColumnNumber, __qty, false);
                this._cellUpdate(row, __priceColumnNumber, __price, false);
                this._cellUpdate(row, __totalColumnNumber, __amount, false);

                if (__priceColumnNumber2 != -1)
                    this._cellUpdate(row, __priceColumnNumber2, __price2, false);

                if (__totalColumnNumber2 != -1)
                    this._cellUpdate(row, __totalColumnNumber2, __amount2, false);
            }
        }

        private decimal _calcItemAmount(string discountWord, decimal qty, decimal price)
        {
            decimal __amount = 0M;

            if (_g.g._companyProfile._calc_item_price_discount)
            {
                // SINGHA
                decimal __priceAfterDiscount = MyLib._myGlobal._calcAfterDiscount(discountWord, price, _g.g._companyProfile._item_price_decimal);
                __amount = MyLib._myGlobal._round(qty * __priceAfterDiscount, _g.g._companyProfile._item_amount_decimal);
                //MyLib._myGlobal._calcAfterDiscount(discountWord, MyLib._myGlobal._round(__priceAfterDiscountqty * price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, qty);
            }
            else
            {
                __amount = MyLib._myGlobal._calcAfterDiscount(discountWord, MyLib._myGlobal._round(qty * price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal, qty);
            }


            return __amount;
        }

        /// <summary>
        /// ค้นหาแม่สินค้าชุด หรือสีผสม 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private int _findFirstRow(int row)
        {
            int __findRow = -1;
            string __ref_guid = (string)this._cellGet(row, _g.d.ic_trans_detail._set_ref_line);

            for (int __rowLoop = row; __rowLoop >= 0; __rowLoop--)
            {

                int __itemType2 = (int)this._cellGet(__rowLoop, _g.d.ic_trans_detail._item_type);
                if (__itemType2 == 3 || __itemType2 == 5)
                {
                    // toe check ref row หรือเปล่า
                    string __guid_line = (string)this._cellGet(__rowLoop, _g.d.ic_trans_detail._ref_guid);
                    if (__guid_line == __ref_guid)
                    {
                        __findRow = __rowLoop;
                        break;
                    }
                }
            }
            return __findRow;
        }

        private void _calcItemSetNow(int row)
        {
            int __findRow = this._findFirstRow(row);
            if (__findRow != -1)
            {
                _calcItemPrice(row, __findRow, this._findColumnByName(_g.d.ic_trans_detail._qty));
            }
        }

        private void _afterUpdateClearQtyAndPrice(int row, Boolean updateUserApprove)
        {
            int __priceColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._price);
            if (__priceColumnNumber != -1)
            {
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                        {
                            Boolean __clearNow = true;
                            int __refLineColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._set_ref_line);
                            if (__refLineColumnNumber != -1)
                            {
                                string __guid = this._cellGet(row, __refLineColumnNumber).ToString();
                                if (__guid.Length > 0)
                                {
                                    // กรณีสินค้าชุด หรือสีผสม
                                    __clearNow = false;
                                    string __itemCode = this._cellGet(row, this._findColumnByName(_g.d.ic_trans_detail._item_code)).ToString();
                                    string __barCode = this._cellGet(row, this._findColumnByName(_g.d.ic_trans_detail._barcode)).ToString();
                                    if (this._gridFindItem(__itemCode, __barCode, "", row))
                                    {
                                        /*decimal __qty = (decimal)this._cellGet(row, this._findColumnByName(_g.d.ic_trans_detail._qty));
                                        string __unitCode = this._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString();
                                        _priceStruct __getPrice = this._findPrice(row, __itemCode, __unitCode, __qty, this._custCode, false, true, true);
                                        this._cellUpdate(row, __priceColumnNumber, __getPrice._price, true);*/
                                        this._calcItemSetNow(row);
                                    }
                                }
                            }
                            if (__clearNow)
                            {
                                this._cellUpdate(row, _g.d.ic_trans_detail._qty, 0M, false);
                                this._cellUpdate(row, _g.d.ic_trans_detail._set_ref_price, 0M, false);
                                this._cellUpdate(row, __priceColumnNumber, 0M, true);
                            }
                            if (updateUserApprove)
                            {
                                this._cellUpdate(row, _g.d.ic_trans_detail._user_approve, "", false);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                        {
                            this._cellUpdate(row, _g.d.ic_trans_detail._qty, 0M, false);
                            this._cellUpdate(row, _g.d.ic_trans_detail._set_ref_price, 0M, false);
                            this._cellUpdate(row, __priceColumnNumber, 0M, true);
                            if (updateUserApprove)
                            {
                                this._cellUpdate(row, _g.d.ic_trans_detail._user_approve, "", false);
                            }
                        }
                        break;
                }
            }
            else
            {
                int __qtyColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._qty);
                if (__qtyColumnNumber != -1)
                {
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                            this._cellUpdate(row, _g.d.ic_trans_detail._qty, 0M, false);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// ตรวจสอบยอดห้ามติดลบก่อน Save
        /// </summary>
        /// <returns></returns>
        public string _checkBalanceAll()
        {
            StringBuilder __result = new StringBuilder();
            if ((MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
                && _g.g._companyProfile._sale_order_banalce_control == true && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย)
            {
                return __result.ToString();
            }

            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    {

                        if ((this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้))
                        {
                            string __docType = this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type);
                            // กรณีไม่กระทบ stock
                            if (__docType == "1" || __docType == "2")
                                return __result.ToString();
                        }

                        Boolean __checkBalance = _g.g._companyProfile._balance_control;

                        Boolean __checkReserve = (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && _g.g._companyProfile._stock_reserved_control) ? true : false;
                        if (
                            (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน ||
                            this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                            && _g.g._companyProfile._ic_stock_control == false
                            )
                        {
                            // กรณีเป็นการขาย แต่ยอมให้ติดลบ (แค่เตือน)
                            __checkBalance = false;
                        }

                        if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && _g.g._companyProfile._ic_stock_control)
                        {
                            __checkBalance = true;
                        }

                        if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ && _g.g._companyProfile._issue_stock_control == true)
                        {
                            __checkBalance = true;
                        }

                        if (__checkBalance || __checkReserve)
                        {
                            int __wareHouseColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._wh_code);
                            int __locationColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._shelf_code);
                            int __itemCodeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._item_code);
                            int __unitCodeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._unit_code);
                            int __qtyColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._qty);
                            int __unitStandColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._stand_value);
                            int __unitDivideColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._divide_value);
                            int __itemTypeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._item_type);

                            Boolean __lotBalanceCheck = false;

                            // ประมวลว่า จะหาสินค้าอะไร,คลัง,ที่เก็บ
                            List<_itemListStruct> __itemList = new List<_itemListStruct>();
                            for (int __row1 = 0; __row1 < this._rowData.Count; __row1++)
                            {
                                string __itemCode = this._cellGet(__row1, __itemCodeColumnNumber).ToString().ToUpper();
                                string __whCode = this._cellGet(__row1, __wareHouseColumnNumber).ToString().ToUpper();
                                string __locationCode = this._cellGet(__row1, __locationColumnNumber).ToString().ToUpper();
                                int __item_type = MyLib._myGlobal._intPhase(this._cellGet(__row1, __itemTypeColumnNumber).ToString());
                                int __costType = MyLib._myGlobal._intPhase(this._cellGet(__row1, _columnCostType).ToString());

                                string __lotNumber = (__costType == 2) ? this._cellGet(__row1, _g.d.ic_trans_detail._lot_number_1).ToString() : "";
                                // toe ไม่เอาสินค้าบริการ

                                if (__item_type == 0 || __item_type == 2 || __item_type == 4)
                                {
                                    Boolean __found = false;
                                    for (int __row2 = 0; __row2 < __itemList.Count; __row2++)
                                    {
                                        Boolean __compare = false;
                                        __compare = __itemCode.Equals(__itemList[__row2]._itemCode) && __whCode.Equals(__itemList[__row2]._whCode) && __locationCode.Equals(__itemList[__row2]._locationCode) && __lotNumber.Equals(__itemList[__row2]._lotNumber);
                                        if (__compare)
                                        {
                                            __found = true;
                                            break;
                                        }
                                    }
                                    if (__found == false)
                                    {
                                        if (__itemCode.Length > 0)
                                        {
                                            _itemListStruct __newData = new _itemListStruct();
                                            __newData._itemCode = __itemCode;
                                            __newData._whCode = __whCode;
                                            __newData._locationCode = __locationCode;
                                            __newData._lotNumber = __lotNumber;
                                            __itemList.Add(__newData);
                                        }
                                    }
                                }

                                if (__costType == 2)
                                {
                                    __lotBalanceCheck = true;
                                }
                            }
                            // รวมจำนวน
                            int __refTypeColumn = this._findColumnByName(_g.d.ic_trans_detail._doc_ref_type);

                            for (int __row1 = 0; __row1 < __itemList.Count; __row1++)
                            {
                                for (int __row2 = 0; __row2 < this._rowData.Count; __row2++)
                                {
                                    string __itemCode = this._cellGet(__row2, __itemCodeColumnNumber).ToString().ToUpper();
                                    string __whCode = this._cellGet(__row2, __wareHouseColumnNumber).ToString().ToUpper();
                                    string __locationCode = this._cellGet(__row2, __locationColumnNumber).ToString().ToUpper();
                                    int __costType = MyLib._myGlobal._intPhase(this._cellGet(__row2, _columnCostType).ToString());
                                    string __lotNumber = (__costType == 2) ? this._cellGet(__row2, _g.d.ic_trans_detail._lot_number_1).ToString() : "";

                                    Boolean __compare = false;
                                    __compare = __itemCode.Equals(__itemList[__row1]._itemCode) && __whCode.Equals(__itemList[__row1]._whCode) && __locationCode.Equals(__itemList[__row1]._locationCode) && __lotNumber.Equals(__itemList[__row1]._lotNumber);
                                    if (__compare)
                                    {
                                        decimal __standValue = (decimal)this._cellGet(__row2, __unitStandColumnNumber);
                                        decimal __divideValue = (decimal)this._cellGet(__row2, __unitDivideColumnNumber);
                                        __itemList[__row1]._qty += (decimal)this._cellGet(__row2, __qtyColumnNumber) * (__standValue / __divideValue);
                                    }

                                    //reserve refer qty
                                    if (__refTypeColumn != -1)
                                    {
                                        if (__compare && this._cellGet(__row2, __refTypeColumn).ToString().Equals("2"))
                                        {
                                            decimal __standValue = (decimal)this._cellGet(__row2, __unitStandColumnNumber);
                                            decimal __divideValue = (decimal)this._cellGet(__row2, __unitDivideColumnNumber);
                                            __itemList[__row1]._referReservQty += (decimal)this._cellGet(__row2, __qtyColumnNumber) * (__standValue / __divideValue);
                                        }
                                    }
                                }
                            }
                            // ดึงยอดคงเหลือ แล้วดูว่าตัวไหนติดลบบ้าง
                            StringBuilder __itemListCode = new StringBuilder();
                            for (int __row1 = 0; __row1 < __itemList.Count; __row1++)
                            {
                                if (__itemListCode.Length > 0)
                                {
                                    __itemListCode.Append(",");
                                }
                                __itemListCode.Append("\'" + __itemList[__row1]._itemCode + "\'");
                            }
                            //
                            string __fieldBalanceControl = "balance_control";
                            string __fieldAccruedControl = "accrued_control";
                            int __queryIndex = 0;
                            int __reserveTableIndex = -1;

                            StringBuilder __query = new StringBuilder();
                            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            string __subQuery1 = MyLib._myGlobal._isnull("(select " + _g.d.ic_inventory_detail._balance_control + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ")", "0");
                            string __subQuery2 = MyLib._myGlobal._isnull("(select " + _g.d.ic_inventory_detail._accrued_control + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ")", "0");
                            SMLProcess._icProcess __process = new SMLProcess._icProcess();

                            string __balanceFieldName = "(select sum(" + _g.d.ic_resource._qty + ") from (" + __process._queryItemListBalance(__itemListCode.ToString(), "", _g.d.ic_resource._qty, "", " ) as temp1)");

                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._code + "," + __subQuery1 + " as " + __fieldBalanceControl + "," + __subQuery2 + " as " + __fieldAccruedControl + ", " + __balanceFieldName + " as " + _g.d.ic_inventory._balance_qty + "," + _g.d.ic_inventory._accrued_out_qty + "," + _g.d.ic_inventory._book_out_qty + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " in (" + __itemListCode.ToString() + ")"));
                            __queryIndex++;
                            int __icBalanceWarehouseTableIndex = -1;
                            if (_g.g._companyProfile._balance_control_type != 0)
                            {
                                // กรณียอดคงเหลือตามคลังและที่เก็บ
                                __icBalanceWarehouseTableIndex = __queryIndex++;
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__process._queryItemListBalance(__itemListCode.ToString(), _g.d.ic_trans_detail._item_code + " as " + _g.d.ic_resource._item_code + "," + _g.d.ic_trans_detail._wh_code + " as " + _g.d.ic_resource._warehouse + "," + _g.d.ic_trans_detail._shelf_code + " as " + _g.d.ic_resource._location, _g.d.ic_resource._qty, _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code, "")));
                            }

                            // ยอดคงเหลือ ตาม LOT ตามที่เก็บ
                            int __lotBalanceTableIndex = -1;
                            if (__lotBalanceCheck == true)
                            {
                                SMLERPControl._icInfoProcess __icInfoProcess = new SMLERPControl._icInfoProcess();
                                __lotBalanceTableIndex = __queryIndex++;

                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__icInfoProcess._stkStockInfoAndBalanceByLotQuery(_g.g._productCostType.ปรกติ, null, __itemListCode.ToString(), "", "", true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT_ที่เก็บ, "")));

                            }

                            if (__checkReserve && _g.g._companyProfile._stock_reserved_control_location)
                            {
                                // ดึงยอดจอง 
                                // query ดึงยอดค้างจองตามที่เก็บ
                                // กรณีอ้างอิง ให้ doc_no ใบจองใบนั้นออก

                                string __reserveBalanceByDoc = "select doc_no, item_code, doc_date, wh_code, shelf_code, (qty * (stand_value/divide_value)) as reserve_add_qty, " +
                                    " coalesce((select sum(qty * (stand_value/divide_value)) from ic_trans_detail as a where a.item_code = ic_trans_detail.item_code and a.ref_doc_no = ic_trans_detail.doc_no and ic_trans_detail.line_number = a.ref_row and a.trans_flag in (36,44,39) and a.last_status = 0), 0) as reduce_qty " +
                                    " from ic_trans_detail where last_status = 0 and trans_flag = 34 and item_code in (" + __itemListCode.ToString() + ")";

                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(" select item_code, wh_code, shelf_code, sum(reserve_add_qty - reduce_qty) as book_out_qty from (" + __reserveBalanceByDoc + " ) as temp1 group by item_code, wh_code, shelf_code "));
                                __reserveTableIndex = __queryIndex;
                                __queryIndex++;
                            }

                            __query.Append("</node>");
                            ArrayList __queryResult = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                            if (__queryResult.Count > 0 && ((DataSet)__queryResult[0]).Tables.Count > 0)
                            {
                                DataTable __item = ((DataSet)__queryResult[0]).Tables[0];
                                if (__item.Rows.Count > 0)
                                {
                                    for (int __loop = 0; __loop < __itemList.Count; __loop++)
                                    {
                                        #region เช็คยอดคงเหลือ
                                        if ((int)MyLib._myGlobal._decimalPhase(__item.Rows[0][__fieldBalanceControl].ToString()) != 1 && __checkBalance)
                                        {
                                            // เริ่มตรวจ สินค้าห้ามติดลบ
                                            switch (_g.g._companyProfile._balance_control_type)
                                            {
                                                case 0: // ห้ามติดลบทั้งระบบ
                                                    {
                                                        DataRow[] __selectRow = __item.Select(_g.d.ic_inventory._code + "=\'" + __itemList[__loop]._itemCode + "\'");
                                                        if (__selectRow.Length > 0)
                                                        {
                                                            decimal __balanceQty = (decimal)MyLib._myGlobal._decimalPhase(__selectRow[0][_g.d.ic_inventory._balance_qty].ToString());
                                                            if (__balanceQty - __itemList[__loop]._qty < 0)
                                                            {
                                                                __result.Append("สินค้า" + " [" + __itemList[__loop]._itemCode + "] " + "ห้ามติดลบ\r\n");
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 1: // ห้ามติดลบตามคลัง
                                                    {
                                                        if (__queryResult.Count > 1)
                                                        {
                                                            // เอายอดที่บันทึก กรณีสินค้ารหัสเดียวกัน เอามารวมยอดกัน
                                                            decimal __qty = 0M;
                                                            for (int __row = 0; __row < __itemList.Count; __row++)
                                                            {
                                                                if (__itemList[__loop]._itemCode.Equals(__itemList[__row]._itemCode) && __itemList[__loop]._whCode.Equals(__itemList[__row]._whCode))
                                                                {
                                                                    __qty += __itemList[__row]._qty;
                                                                }
                                                            }
                                                            //
                                                            DataTable __dt = ((DataSet)__queryResult[1]).Tables[0];

                                                            decimal __balanceQty = 0M;

                                                            if (__dt.Rows.Count > 0)
                                                            {
                                                                DataRow[] __selectRow = ((DataSet)__queryResult[1]).Tables[0].Select(_g.d.ic_resource._item_code + "=\'" + __itemList[__loop]._itemCode + "\' and " + _g.d.ic_resource._warehouse + "=\'" + __itemList[__loop]._whCode + "\'");
                                                                for (int __row2 = 0; __row2 < __selectRow.Length; __row2++)
                                                                {
                                                                    __balanceQty += (decimal)MyLib._myGlobal._decimalPhase(__selectRow[__row2][_g.d.ic_resource._qty].ToString());
                                                                }
                                                            }

                                                            if (__balanceQty - __qty < 0)
                                                            {
                                                                __result.Append("สินค้า" + " : " + __itemList[__loop]._itemCode + "," + "คลัง" + " : " + __itemList[__loop]._whCode + " " + "ห้ามติดลบ\r\n");
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 2: // ห้ามติดลบตามที่เก็บ
                                                    {
                                                        // เอายอดที่บันทึก กรณีสินค้ารหัสเดียวกัน เอามารวมยอดกัน
                                                        if (__queryResult.Count > 1)
                                                        {
                                                            // เอายอดที่บันทึก กรณีสินค้ารหัสเดียวกัน เอามารวมยอดกัน
                                                            decimal __qty = 0M;
                                                            for (int __row = 0; __row < __itemList.Count; __row++)
                                                            {
                                                                if (__itemList[__loop]._itemCode.Equals(__itemList[__row]._itemCode) && __itemList[__loop]._whCode.Equals(__itemList[__row]._whCode) && __itemList[__loop]._locationCode.Equals(__itemList[__row]._locationCode))
                                                                {
                                                                    __qty += __itemList[__row]._qty;
                                                                }
                                                            }
                                                            //
                                                            decimal __balanceQty = 0M;
                                                            DataTable __dt = ((DataSet)__queryResult[1]).Tables[0];
                                                            if (__dt.Rows.Count > 0)
                                                            {
                                                                DataRow[] __selectRow = ((DataSet)__queryResult[1]).Tables[0].Select(_g.d.ic_resource._item_code + "=\'" + __itemList[__loop]._itemCode + "\' and " + _g.d.ic_resource._warehouse + "=\'" + __itemList[__loop]._whCode + "\' and " + _g.d.ic_resource._location + "=\'" + __itemList[__loop]._locationCode + "\'");
                                                                for (int __row2 = 0; __row2 < __selectRow.Length; __row2++)
                                                                {
                                                                    __balanceQty += (decimal)MyLib._myGlobal._decimalPhase(__selectRow[__row2][_g.d.ic_resource._qty].ToString());
                                                                }
                                                            }
                                                            if (__balanceQty - __qty < 0)
                                                            {
                                                                __result.Append("สินค้า" + " : " + __itemList[__loop]._itemCode + "," + "คลัง" + " : " + __itemList[__loop]._whCode + "," + "ที่เก็บ" + " : " + __itemList[__loop]._locationCode + " " + "ห้ามติดลบ\r\n");
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }

                                            #region Lot Balance Check
                                            // ตรวจสินค้าห้ามติดลบตาม LOT
                                            if (__result.Length == 0 && __itemList[__loop]._lotNumber.Length > 0)
                                            {
                                                switch (_g.g._companyProfile._balance_control_type)
                                                {
                                                    case 0: // ทั้งระบบ
                                                        {
                                                            // เอายอดคงเหลือ ที่ LOT เดียวกันมารวมกัน
                                                            decimal __qty = 0M;
                                                            for (int __row = 0; __row < __itemList.Count; __row++)
                                                            {
                                                                if (__itemList[__loop]._itemCode.Equals(__itemList[__row]._itemCode) && __itemList[__loop]._lotNumber.Equals(__itemList[__row]._lotNumber))
                                                                {
                                                                    __qty += __itemList[__row]._qty;
                                                                }
                                                            }

                                                            DataTable __dt = ((DataSet)__queryResult[__lotBalanceTableIndex]).Tables[0];

                                                            decimal __balanceQty = 0M;

                                                            if (__dt.Rows.Count > 0)
                                                            {
                                                                DataRow[] __selectRow = __dt.Select(_g.d.ic_resource._ic_code + "=\'" + __itemList[__loop]._itemCode + "\' and " + _g.d.ic_resource._lot_number + "=\'" + __itemList[__loop]._lotNumber + "\'");
                                                                for (int __row2 = 0; __row2 < __selectRow.Length; __row2++)
                                                                {
                                                                    __balanceQty += (decimal)MyLib._myGlobal._decimalPhase(__selectRow[__row2][_g.d.ic_resource._balance_qty].ToString());
                                                                }
                                                            }

                                                            if (__balanceQty - __qty < 0)
                                                            {
                                                                __result.Append("สินค้า" + " : " + __itemList[__loop]._itemCode + ",Lot : " + __itemList[__loop]._lotNumber + " " + "ห้ามติดลบ\r\n");
                                                            }

                                                        }
                                                        break;
                                                    case 1: // คลัง
                                                        {
                                                            // เอายอดคงเหลือ ที่ LOT เดียวกัน คลังเดียวกัน มารวมกัน
                                                            decimal __qty = 0M;
                                                            for (int __row = 0; __row < __itemList.Count; __row++)
                                                            {
                                                                if (__itemList[__loop]._itemCode.Equals(__itemList[__row]._itemCode) && __itemList[__loop]._whCode.Equals(__itemList[__row]._whCode) && __itemList[__loop]._lotNumber.Equals(__itemList[__row]._lotNumber))
                                                                {
                                                                    __qty += __itemList[__row]._qty;
                                                                }
                                                            }

                                                            DataTable __dt = ((DataSet)__queryResult[__lotBalanceTableIndex]).Tables[0];

                                                            decimal __balanceQty = 0M;

                                                            if (__dt.Rows.Count > 0)
                                                            {
                                                                DataRow[] __selectRow = __dt.Select(_g.d.ic_resource._ic_code + "=\'" + __itemList[__loop]._itemCode + "\' and " + _g.d.ic_trans_detail_lot._wh_code + " = \'" + __itemList[__loop]._whCode + "\'  and " + _g.d.ic_resource._lot_number + "=\'" + __itemList[__loop]._lotNumber + "\'");
                                                                for (int __row2 = 0; __row2 < __selectRow.Length; __row2++)
                                                                {
                                                                    __balanceQty += (decimal)MyLib._myGlobal._decimalPhase(__selectRow[__row2][_g.d.ic_resource._balance_qty].ToString());
                                                                }
                                                            }

                                                            if (__balanceQty - __qty < 0)
                                                            {
                                                                __result.Append("สินค้า" + " : " + __itemList[__loop]._itemCode + "," + "คลัง" + " : " + __itemList[__loop]._whCode + "," + "Lot : " + __itemList[__loop]._lotNumber + " " + "ห้ามติดลบ\r\n");
                                                            }

                                                        }
                                                        break;
                                                    case 2: // ที่เก็บ
                                                        {
                                                            // เอายอดคงเหลือ ที่ LOT เดียวกัน คลังเดียวกัน ที่เก็บเดียวกัน มารวมกัน
                                                            decimal __qty = 0M;
                                                            for (int __row = 0; __row < __itemList.Count; __row++)
                                                            {
                                                                if (__itemList[__loop]._itemCode.Equals(__itemList[__row]._itemCode) && __itemList[__loop]._whCode.Equals(__itemList[__row]._whCode) && __itemList[__loop]._locationCode.Equals(__itemList[__row]._locationCode) && __itemList[__loop]._lotNumber.Equals(__itemList[__row]._lotNumber))
                                                                {
                                                                    __qty += __itemList[__row]._qty;
                                                                }
                                                            }

                                                            DataTable __dt = ((DataSet)__queryResult[__lotBalanceTableIndex]).Tables[0];

                                                            decimal __balanceQty = 0M;

                                                            if (__dt.Rows.Count > 0)
                                                            {
                                                                DataRow[] __selectRow = __dt.Select(_g.d.ic_resource._ic_code + "=\'" + __itemList[__loop]._itemCode + "\' and " + _g.d.ic_trans_detail_lot._wh_code + " = \'" + __itemList[__loop]._whCode + "\' and " + _g.d.ic_trans_detail_lot._shelf_code + " = \'" + __itemList[__loop]._locationCode + "\'  and " + _g.d.ic_resource._lot_number + "=\'" + __itemList[__loop]._lotNumber + "\'");
                                                                for (int __row2 = 0; __row2 < __selectRow.Length; __row2++)
                                                                {
                                                                    __balanceQty += (decimal)MyLib._myGlobal._decimalPhase(__selectRow[__row2][_g.d.ic_resource._balance_qty].ToString());
                                                                }
                                                            }

                                                            if (__balanceQty - __qty < 0)
                                                            {
                                                                __result.Append("สินค้า" + " : " + __itemList[__loop]._itemCode + "," + "คลัง" + " : " + __itemList[__loop]._whCode + "," + "ที่เก็บ" + " : " + __itemList[__loop]._locationCode + "," + "Lot : " + __itemList[__loop]._lotNumber + " " + "ห้ามติดลบ\r\n");
                                                            }

                                                        }
                                                        break;
                                                }
                                            }

                                            #endregion

                                        }
                                        #endregion

                                        // reserve control
                                        if (__checkReserve)
                                        {
                                            if (_g.g._companyProfile._stock_reserved_control_location == true)
                                            {
                                                // เช็คตามที่เก็บ
                                                DataTable __tableReserveBalance = ((DataSet)__queryResult[__reserveTableIndex]).Tables[0];

                                                // เช็คจำนวน กับยอดจอง
                                                decimal __balanceQty = 0M;
                                                decimal __book_out_qty = 0M;

                                                decimal __referReserveQty = 0M;

                                                decimal __qty = 0M;
                                                for (int __row = 0; __row < __itemList.Count; __row++)
                                                {
                                                    if (__itemList[__loop]._itemCode.Equals(__itemList[__row]._itemCode) && __itemList[__loop]._whCode.Equals(__itemList[__row]._whCode) && __itemList[__loop]._locationCode.Equals(__itemList[__row]._locationCode))
                                                    {
                                                        __qty += __itemList[__row]._qty;
                                                        __referReserveQty += __itemList[__row]._referReservQty;
                                                    }
                                                }

                                                // check balance warehouse 
                                                if (_g.g._companyProfile._balance_control_type > 0)
                                                {
                                                    DataTable __dt = ((DataSet)__queryResult[1]).Tables[0];
                                                    if (__dt.Rows.Count > 0)
                                                    {
                                                        DataRow[] __selectRow = ((DataSet)__queryResult[1]).Tables[0].Select(_g.d.ic_resource._item_code + "=\'" + __itemList[__loop]._itemCode + "\' and " + _g.d.ic_resource._warehouse + "=\'" + __itemList[__loop]._whCode + "\' and " + _g.d.ic_resource._location + "=\'" + __itemList[__loop]._locationCode + "\'");
                                                        for (int __row2 = 0; __row2 < __selectRow.Length; __row2++)
                                                        {
                                                            __balanceQty += (decimal)MyLib._myGlobal._decimalPhase(__selectRow[__row2][_g.d.ic_resource._qty].ToString());
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataRow[] __selectRow = __item.Select(_g.d.ic_inventory._code + "=\'" + __itemList[__loop]._itemCode + "\'");
                                                    if (__selectRow.Length > 0)
                                                    {
                                                        __balanceQty = (decimal)MyLib._myGlobal._decimalPhase(__selectRow[0][_g.d.ic_inventory._balance_qty].ToString());
                                                    }
                                                }


                                                if (__tableReserveBalance.Rows.Count > 0)
                                                {
                                                    DataRow[] __selectRowReserve = __tableReserveBalance.Select("item_code = \'" + __itemList[__loop]._itemCode + "\' and wh_code = \'" + __itemList[__loop]._whCode + "\' and shelf_code = \'" + __itemList[__loop]._locationCode + "\' ");
                                                    if (__selectRowReserve.Length > 0)
                                                    {
                                                        __book_out_qty = (decimal)MyLib._myGlobal._decimalPhase(__selectRowReserve[0]["book_out_qty"].ToString());
                                                        // ยอดคงเหลือ ตามคลัง - จองตามคลัง - จำนวนขาย
                                                        if ((__balanceQty - (__book_out_qty - __referReserveQty)) - __qty < 0)
                                                        {
                                                            __result.Append(MyLib._myGlobal._resource("สินค้า") + " [" + __itemList[__loop]._itemCode + "] \nคลัง [" + __itemList[__loop]._whCode + "] \nที่เก็บ [" + __itemList[__loop]._locationCode + "]" + MyLib._myGlobal._resource("ห้ามขายเกินยอดคงเหลือลบด้วยค้างจอง"));
                                                        }
                                                    }
                                                }
                                                // ไม่มียอดจอง

                                            }
                                            else
                                            {
                                                // เช็คทั้งระบบ
                                                DataRow[] __selectRow = __item.Select(_g.d.ic_inventory._code + "=\'" + __itemList[__loop]._itemCode + "\'");
                                                if (__selectRow.Length > 0)
                                                {
                                                    decimal __balanceQty = (decimal)MyLib._myGlobal._decimalPhase(__selectRow[0][_g.d.ic_inventory._balance_qty].ToString());
                                                    decimal __book_out_qty = (decimal)MyLib._myGlobal._decimalPhase(__selectRow[0][_g.d.ic_inventory._book_out_qty].ToString());

                                                    if ((__balanceQty - __book_out_qty) - __itemList[__loop]._qty < 0)
                                                    {
                                                        __result.Append(MyLib._myGlobal._resource("สินค้า") + " [" + __itemList[__loop]._itemCode + "] " + MyLib._myGlobal._resource("ห้ามขายเกินยอดคงเหลือลบด้วยค้างจอง"));
                                                    }
                                                }

                                            }
                                        }
                                    }
                                } // end loop
                            }
                        }
                        // end check 
                    }
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                    {
                        for (int __row2 = 0; __row2 < this._rowData.Count; __row2++)
                        {
                            decimal __totalQty = (decimal)this._cellGet(__row2, _g.d.ic_trans_detail._total_qty);
                            decimal __qty = (decimal)this._cellGet(__row2, _g.d.ic_trans_detail._qty);

                            if (__qty > __totalQty)
                            {
                                __result.Append("สินค้า " + this._cellGet(__row2, _g.d.ic_trans_detail._item_code).ToString() + " ห้ามเบิกเกินยอดฝากสินค้า");
                            }

                        }
                    }
                    break;
            }
            return __result.ToString();
        }

        private Boolean _checkBalance(object sender, int row, int column, string itemCodeProcess, int itemCodeColumnNumber, int qtyColumnNumber)
        {
            int __itemType = (int)this._cellGet(row, _g.d.ic_trans_detail._item_type);

            if ((MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
                && _g.g._companyProfile._sale_order_banalce_control == true && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย)
            {
                return false;
            }

            Boolean __warning = false;
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    {

                        if ((this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้))
                        {
                            string __docType = this._icTransScreenTop._getDataStr(_g.d.ic_trans._inquiry_type);
                            // กรณีไม่กระทบ stock
                            if (__docType == "1" || __docType == "2")
                                return false;
                        }

                        // เอายอดที่บันทึก กรณีสินค้ารหัสเดียวกัน เอามารวมยอดกัน
                        decimal __qty = 0M;
                        for (int __rowLoop = 0; __rowLoop < this._rowData.Count; __rowLoop++)
                        {
                            if (itemCodeProcess.Equals(this._cellGet(__rowLoop, itemCodeColumnNumber).ToString().ToUpper()))
                            {
                                decimal __standValue = (decimal)this._cellGet(__rowLoop, _g.d.ic_trans_detail._stand_value);
                                decimal __divideValue = (decimal)this._cellGet(__rowLoop, _g.d.ic_trans_detail._divide_value);
                                __qty += (decimal)this._cellGet(__rowLoop, qtyColumnNumber) * (__standValue / __divideValue);
                            }
                        }
                        //
                        if (__qty != 0 && itemCodeProcess.Length > 0)
                        {
                            Boolean __balanceControl = _g.g._companyProfile._balance_control; // สินค้าห้ามติดลบทั้งระบบ
                            Boolean __accruedControl = _g.g._companyProfile._accrued_control; // สินค้าห้ามติดลบทั้งระบบ
                            Boolean __transferControl = _g.g._companyProfile._transfer_stock_control;
                            Boolean __issueControl = _g.g._companyProfile._issue_stock_control; // ห้าเบิกสินค้าติดลบ
                            Boolean __reservedControl = _g.g._companyProfile._stock_reserved_control;

                            // toe กรณี ไม่ใช่สินค้าบริการ ค่อยให้ทำ
                            if (__itemType == 0 || __itemType == 2 || __itemType == 4)
                            {
                                int __queryIndex = 0;
                                int __queryBalanceIndex = -1;
                                int __queryReserveindex = -1;
                                if (__balanceControl || __accruedControl || __transferControl || __issueControl || __reservedControl)
                                {
                                    string __fieldBalanceControl = "balance_control";
                                    string __fieldAccruedControl = "accrued_control";
                                    StringBuilder __query = new StringBuilder();
                                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    string __subQuery1 = MyLib._myGlobal._isnull("(select " + _g.d.ic_inventory_detail._balance_control + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ")", "0");
                                    string __subQuery2 = MyLib._myGlobal._isnull("(select " + _g.d.ic_inventory_detail._accrued_control + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ")", "0");
                                    SMLProcess._icProcess __process = new SMLProcess._icProcess();
                                    string __balanceFieldName = "(select sum(" + _g.d.ic_resource._qty + ") from (" + __process._queryItemBalance(itemCodeProcess, "", _g.d.ic_resource._qty, "", " ) as temp1)");

                                    string __sumAccruedAddDocGroup = "(select doc_no,item_code from ic_trans_detail where ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.trans_flag=36 and ic_trans_detail.last_status=0 group by ic_trans_detail.doc_no,ic_trans_detail.item_code)";
                                    string __sumAccruedAdd = "(select doc_no,item_code,coalesce(sum(qty * (stand_value/divide_value)),0) as qty from ic_trans_detail where ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.trans_flag=36 and ic_trans_detail.last_status=0 group by ic_trans_detail.doc_no,ic_trans_detail.item_code)";
                                    string __sumAccruedReduce = "(select ref_doc_no,item_code,coalesce(sum(qty * (stand_value/divide_value)),0) as qty from ic_trans_detail where ic_trans_detail.ref_doc_no<>'' and ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.trans_flag in (44) and ic_trans_detail.last_status=0 group by ic_trans_detail.ref_doc_no,ic_trans_detail.item_code)";
                                    string __sumAccruedByDoc = "(select doc_no,item_code,(select qty from (" + __sumAccruedAdd + ") as a where a.doc_no=c.doc_no and a.item_code=c.item_code)-coalesce((select qty from (" + __sumAccruedReduce + ") as b where b.ref_doc_no=c.doc_no and b.item_code=c.item_code),0) as qty from (" + __sumAccruedAddDocGroup + ") as c group by c.doc_no,c.item_code)";
                                    string __sumAccrueByDocCase = "(select doc_no,item_code,case when qty < 0 then 0 else qty end as qty from " + __sumAccruedByDoc + " as x)";
                                    string __sumAccrue = "coalesce((select sum(qty) from (" + __sumAccrueByDocCase + ") as a where a.item_code=ic_inventory.code),0)";

                                    string __sumResurcedAddDocGroup = "(select doc_no,item_code from ic_trans_detail where ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0 group by ic_trans_detail.doc_no,ic_trans_detail.item_code)";
                                    string __sumResurcedAdd = "(select doc_no,item_code,coalesce(sum(qty * (stand_value/divide_value)),0) as qty from ic_trans_detail where ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0 group by ic_trans_detail.doc_no,ic_trans_detail.item_code)";
                                    string __sumResurcedReduce = "(select ref_doc_no,item_code,coalesce(sum(qty * (stand_value/divide_value)),0) as qty from ic_trans_detail where ic_trans_detail.ref_doc_no<>'' and ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.trans_flag in (44) and ic_trans_detail.last_status=0 group by ic_trans_detail.ref_doc_no,ic_trans_detail.item_code)";
                                    string __sumResurcedByDoc = "(select doc_no,item_code,(select qty from (" + __sumResurcedAdd + ") as a where a.doc_no=c.doc_no and a.item_code=c.item_code)-coalesce((select qty from (" + __sumResurcedReduce + ") as b where b.ref_doc_no=c.doc_no and b.item_code=c.item_code),0) as qty from (" + __sumResurcedAddDocGroup + ") as c group by c.doc_no,c.item_code)";
                                    string __sumResurcedByDocCase = "(select doc_no,item_code,case when qty < 0 then 0 else qty end as qty from " + __sumResurcedByDoc + " as x)";
                                    string __sumResurced = "coalesce((select sum(qty) from (" + __sumResurcedByDocCase + ") as a where a.item_code=ic_inventory.code),0)";


                                    if (this._docNoOld != null && this._docNoOld().Length > 0)
                                    {
                                        __balanceFieldName = "(select sum(" + _g.d.ic_resource._qty + ") from (" + __process._queryItemBalance(itemCodeProcess, "", _g.d.ic_resource._qty, "", " and " + _g.d.ic_trans_detail._doc_no + " <> \'" + this._docNoOld() + "\') as temp1)");
                                    }
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __subQuery1 + " as " + __fieldBalanceControl + "," + __subQuery2 + " as " + __fieldAccruedControl + "," + __balanceFieldName + " as " + _g.d.ic_inventory._balance_qty + "," + __sumAccrue + " as " + _g.d.ic_inventory._accrued_out_qty + ", " + __sumResurced + " as " + _g.d.ic_inventory._book_out_qty + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + itemCodeProcess + "\'"));
                                    __queryIndex++;

                                    if (_g.g._companyProfile._balance_control_type != 0 || (__transferControl && this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก) || (__issueControl && this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ) || (__reservedControl && _g.g._companyProfile._stock_reserved_control_location))
                                    {
                                        // กรณียอดคงเหลือตามคลังและที่เก็บ
                                        //SMLProcess._icProcess __process = new SMLProcess._icProcess();
                                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__process._queryItemBalance(itemCodeProcess, _g.d.ic_trans_detail._wh_code + " as " + _g.d.ic_resource._warehouse + "," + _g.d.ic_trans_detail._shelf_code + " as " + _g.d.ic_resource._location, _g.d.ic_resource._qty, _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code, (this._docNoOld != null && this._docNoOld().Length > 0) ? " and " + _g.d.ic_trans_detail._doc_no + " <> \'" + this._docNoOld() + "\'" : "")));
                                        __queryBalanceIndex = __queryIndex;
                                        __queryIndex++;
                                    }
                                    if (__reservedControl && _g.g._companyProfile._stock_reserved_control_location)
                                    {
                                        string __wareHouseCodeReserve = this._cellGet(row, _g.d.ic_trans_detail._wh_code).ToString().Trim().ToUpper();
                                        string __locationCodeReserve = this._cellGet(row, _g.d.ic_trans_detail._shelf_code).ToString().Trim().ToUpper();

                                        // query ดึงยอดค้างจองตามที่เก็บ
                                        String __sumAddDocGroup = "(select doc_no,item_code from ic_trans_detail where ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0 and wh_code = \'" + __wareHouseCodeReserve + "\' and shelf_code = \'" + __locationCodeReserve + "\'  group by ic_trans_detail.doc_no,ic_trans_detail.item_code)";
                                        String __sumAdd = "(select doc_no,item_code,coalesce(sum(qty * (stand_value/divide_value)),0) as qty from ic_trans_detail where ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.trans_flag=34 and ic_trans_detail.last_status=0 and wh_code = \'" + __wareHouseCodeReserve + "\' and shelf_code = \'" + __locationCodeReserve + "\' group by ic_trans_detail.doc_no,ic_trans_detail.item_code)";
                                        String __sumReduce = "(select ref_doc_no,item_code,coalesce(sum(qty * (stand_value/divide_value)),0) as qty from ic_trans_detail where ic_trans_detail.ref_doc_no<>'' and ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.trans_flag in (36,44,39) and ic_trans_detail.last_status=0 and wh_code = \'" + __wareHouseCodeReserve + "\' and shelf_code = \'" + __locationCodeReserve + "\' group by ic_trans_detail.ref_doc_no,ic_trans_detail.item_code)";
                                        String __sumByDoc = "(select doc_no,item_code,(select qty from (" + __sumAdd + ") as a where a.doc_no=c.doc_no and a.item_code=c.item_code)-coalesce((select qty from (" + __sumReduce + ") as b where b.ref_doc_no=c.doc_no and b.item_code=c.item_code),0) as qty from (" + __sumAddDocGroup + ") as c group by c.doc_no,c.item_code)";
                                        String __sumByDocCase = "(select doc_no,item_code,case when qty < 0 then 0 else qty end as qty from " + __sumByDoc + " as x)";
                                        //String __sum = "coalesce((select sum(qty) from (" + __sumByDocCase + ") as a where a.item_code=ic_inventory.code)/(select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard),0)";
                                        String __sum = "coalesce((select sum(qty) from (" + __sumByDocCase + ") as a where a.item_code=ic_inventory.code),0)";

                                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, (" + __sum + ") as book_out_qty from ic_inventory where code = \'" + itemCodeProcess + "\' "));
                                        __queryReserveindex = __queryIndex;
                                        __queryIndex++;
                                    }
                                    __query.Append("</node>");
                                    ArrayList __queryResult = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                    DataTable __item = ((DataSet)__queryResult[0]).Tables[0];
                                    if (__item.Rows.Count > 0)
                                    {
                                        // ตรวจสอบสินค้าติดลบ
                                        if ((int)MyLib._myGlobal._decimalPhase(__item.Rows[0][__fieldBalanceControl].ToString()) == 1)
                                        {
                                            __balanceControl = false;
                                        }
                                        if (__balanceControl || (__issueControl && this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ))
                                        {
                                            int __balance_control_type = _g.g._companyProfile._balance_control_type;
                                            if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก && __transferControl)
                                            {
                                                __balance_control_type = 2;
                                            }

                                            // เริ่มตรวจ สินค้าห้ามติดลบ
                                            switch (__balance_control_type)
                                            {
                                                case 0: // ห้ามติดลบทั้งระบบ
                                                    {
                                                        decimal __balanceQty = (decimal)MyLib._myGlobal._decimalPhase(__item.Rows[0][_g.d.ic_inventory._balance_qty].ToString());
                                                        if (__balanceQty - __qty < 0)
                                                        {
                                                            MessageBox.Show(MyLib._myGlobal._resource("สินค้า") + " [" + itemCodeProcess + "] " + MyLib._myGlobal._resource("ห้ามติดลบ"));
                                                            __warning = true;
                                                            if (_g.g._companyProfile._ic_stock_control || (__issueControl && this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ))
                                                            {
                                                                this._cellUpdate(row, qtyColumnNumber, 0M, true);
                                                                return true;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 1: // ห้ามติดลบตามคลัง
                                                    {
                                                        // เอายอดที่บันทึก กรณีสินค้ารหัสเดียวกัน เอามารวมยอดกัน
                                                        string __wareHouseCode = this._cellGet(row, _g.d.ic_trans_detail._wh_code).ToString().Trim().ToUpper();
                                                        __qty = 0M;
                                                        for (int __row = 0; __row < this._rowData.Count; __row++)
                                                        {
                                                            if (itemCodeProcess.Equals(this._cellGet(__row, itemCodeColumnNumber).ToString().ToUpper()) &&
                                                                __wareHouseCode.Equals(this._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString().Trim().ToUpper()))
                                                            {
                                                                decimal __standValue = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._stand_value);
                                                                decimal __divideValue = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._divide_value);
                                                                __qty += (decimal)this._cellGet(__row, qtyColumnNumber) * (__standValue / __divideValue);
                                                            }
                                                        }
                                                        //
                                                        DataTable __item2 = ((DataSet)__queryResult[1]).Tables[0];
                                                        decimal __balanceQty = 0M;
                                                        for (int __row2 = 0; __row2 < __item2.Rows.Count; __row2++)
                                                        {
                                                            if (__wareHouseCode.Equals(__item2.Rows[__row2][_g.d.ic_resource._warehouse].ToString().ToString().Trim().ToUpper()))
                                                            {
                                                                __balanceQty += (decimal)MyLib._myGlobal._decimalPhase(__item2.Rows[__row2][_g.d.ic_resource._qty].ToString());
                                                            }
                                                        }
                                                        if (__balanceQty - __qty < 0)
                                                        {
                                                            __warning = true;
                                                            MessageBox.Show(MyLib._myGlobal._resource("สินค้า") + " : " + itemCodeProcess + "," + MyLib._myGlobal._resource("คลัง") + " : " + __wareHouseCode + " " + MyLib._myGlobal._resource("ห้ามติดลบ"));
                                                            if (_g.g._companyProfile._ic_stock_control)
                                                            {
                                                                this._cellUpdate(row, qtyColumnNumber, 0M, true);
                                                                return true;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 2: // ห้ามติดลบตามที่เก็บ
                                                    {
                                                        // เอายอดที่บันทึก กรณีสินค้ารหัสเดียวกัน เอามารวมยอดกัน
                                                        string __wareHouseCode = this._cellGet(row, _g.d.ic_trans_detail._wh_code).ToString().Trim().ToUpper();
                                                        string __locationCode = this._cellGet(row, _g.d.ic_trans_detail._shelf_code).ToString().Trim().ToUpper();
                                                        __qty = 0M;
                                                        for (int __row = 0; __row < this._rowData.Count; __row++)
                                                        {
                                                            if (itemCodeProcess.Equals(this._cellGet(__row, itemCodeColumnNumber).ToString().ToUpper()) &&
                                                                __wareHouseCode.Equals(this._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString().Trim().ToUpper()) &&
                                                                __locationCode.Equals(this._cellGet(__row, _g.d.ic_trans_detail._shelf_code).ToString().Trim().ToUpper()))
                                                            {
                                                                decimal __standValue = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._stand_value);
                                                                decimal __divideValue = (decimal)this._cellGet(__row, _g.d.ic_trans_detail._divide_value);
                                                                __qty += (decimal)this._cellGet(__row, qtyColumnNumber) * (__standValue / __divideValue);
                                                            }
                                                        }
                                                        //
                                                        DataTable __item2 = ((DataSet)__queryResult[1]).Tables[0];
                                                        decimal __balanceQty = 0M;
                                                        for (int __row2 = 0; __row2 < __item2.Rows.Count; __row2++)
                                                        {
                                                            if (__wareHouseCode.Equals(__item2.Rows[__row2][_g.d.ic_resource._warehouse].ToString().ToString().Trim().ToUpper()) &&
                                                                __locationCode.Equals(__item2.Rows[__row2][_g.d.ic_resource._location].ToString().ToString().Trim().ToUpper()))
                                                            {
                                                                __balanceQty += (decimal)MyLib._myGlobal._decimalPhase(__item2.Rows[__row2][_g.d.ic_resource._qty].ToString());
                                                            }
                                                        }
                                                        if (__balanceQty - __qty < 0)
                                                        {
                                                            __warning = true;
                                                            MessageBox.Show(MyLib._myGlobal._resource("สินค้า") + " : " + itemCodeProcess + "," + MyLib._myGlobal._resource("คลัง") + " : " + __wareHouseCode + "," + MyLib._myGlobal._resource("ที่เก็บ") + " : " + __locationCode + " " + MyLib._myGlobal._resource("ห้ามติดลบ"));
                                                            if (_g.g._companyProfile._ic_stock_control || (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก && _g.g._companyProfile._transfer_stock_control)) // toe
                                                            {
                                                                this._cellUpdate(row, qtyColumnNumber, 0M, true);
                                                                return true;
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        // ตรวจสอบสินค้าห้ามขายเกินสินค้าค้างส่ง (สั่งขาย)
                                        if ((int)MyLib._myGlobal._decimalPhase(__item.Rows[0][__fieldAccruedControl].ToString()) == 1)
                                        {
                                            __accruedControl = false;
                                        }
                                        if (__accruedControl && __warning == false)
                                        {
                                            switch (_g.g._companyProfile._balance_control_type)
                                            {
                                                case 0: // ห้ามติดลบทั้งระบบ
                                                    // เริ่มตรวจ สินค้าห้ามขายเกินค้างส่ง
                                                    decimal __balanceQty = (decimal)MyLib._myGlobal._decimalPhase(__item.Rows[0][_g.d.ic_inventory._balance_qty].ToString());
                                                    decimal __accruedOutQty = (decimal)MyLib._myGlobal._decimalPhase(__item.Rows[0][_g.d.ic_inventory._accrued_out_qty].ToString());
                                                    if ((__balanceQty - __accruedOutQty) - __qty < 0)
                                                    {
                                                        MessageBox.Show(MyLib._myGlobal._resource("สินค้า") + " [" + itemCodeProcess + "] " + MyLib._myGlobal._resource("ห้ามขายเกินยอดคงเหลือลบด้วยค้างส่ง"));
                                                        if (_g.g._companyProfile._ic_stock_control)
                                                        {
                                                            this._cellUpdate(row, qtyColumnNumber, 0M, true);
                                                            return true;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }

                                        // check reserve
                                        if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && __reservedControl && __warning == false)
                                        {
                                            if (_g.g._companyProfile._stock_reserved_control_location == true)
                                            {
                                                string __wareHouseCode = this._cellGet(row, _g.d.ic_trans_detail._wh_code).ToString().Trim().ToUpper();
                                                string __locationCode = this._cellGet(row, _g.d.ic_trans_detail._shelf_code).ToString().Trim().ToUpper();
                                                DataTable __item2 = ((DataSet)__queryResult[__queryBalanceIndex]).Tables[0];
                                                DataTable __reserveShelfQty = ((DataSet)__queryResult[__queryReserveindex]).Tables[0];

                                                decimal __balanceQty = 0M;
                                                for (int __row2 = 0; __row2 < __item2.Rows.Count; __row2++)
                                                {
                                                    if (__wareHouseCode.Equals(__item2.Rows[__row2][_g.d.ic_resource._warehouse].ToString().ToString().Trim().ToUpper()) &&
                                                        __locationCode.Equals(__item2.Rows[__row2][_g.d.ic_resource._location].ToString().ToString().Trim().ToUpper()))
                                                    {
                                                        __balanceQty += (decimal)MyLib._myGlobal._decimalPhase(__item2.Rows[__row2][_g.d.ic_resource._qty].ToString());
                                                    }
                                                }

                                                decimal __reservdOutQty = (decimal)MyLib._myGlobal._decimalPhase(__reserveShelfQty.Rows[0][_g.d.ic_inventory._book_out_qty].ToString());
                                                if ((__balanceQty - __reservdOutQty) - __qty < 0)
                                                {
                                                    MessageBox.Show(MyLib._myGlobal._resource("สินค้า") + " [" + itemCodeProcess + "] " + MyLib._myGlobal._resource("ห้ามขายเกินยอดคงเหลือลบด้วยค้างจอง"));
                                                    this._cellUpdate(row, qtyColumnNumber, 0M, true);
                                                    return true;
                                                }


                                            }
                                            else
                                            {
                                                //switch (_g.g._companyProfile._balance_control_type)
                                                //{
                                                //case 0: // ห้ามติดลบทั้งระบบ
                                                // เริ่มตรวจ สินค้าห้ามขายเกินค้างจอง
                                                decimal __balanceQty = (decimal)MyLib._myGlobal._decimalPhase(__item.Rows[0][_g.d.ic_inventory._balance_qty].ToString());
                                                decimal __reservdOutQty = (decimal)MyLib._myGlobal._decimalPhase(__item.Rows[0][_g.d.ic_inventory._book_out_qty].ToString());
                                                if ((__balanceQty - __reservdOutQty) - __qty < 0)
                                                {
                                                    MessageBox.Show(MyLib._myGlobal._resource("สินค้า") + " [" + itemCodeProcess + "] " + MyLib._myGlobal._resource("ห้ามขายเกินยอดคงเหลือลบด้วยค้างจอง"));
                                                    this._cellUpdate(row, qtyColumnNumber, 0M, true);
                                                    return true;
                                                }
                                            }
                                            //break;                                              
                                            //}
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            //
            return false;
        }

        private void _changeItemNameRemarkQty(int row)
        {
            MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
            string __query = "select " + _g.d.ic_inventory._name_1 + "," + _g.d.ic_inventory._remark + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._cellGet(row, _g.d.ic_trans_detail._item_code) + "\'";
            DataTable __result = __myFramework._queryShort(__query).Tables[0];

            if (__result.Rows.Count > 0)
            {
                // process
                string __getItemName = __result.Rows[0][_g.d.ic_inventory._name_1].ToString();
                string __getItemRemark = __result.Rows[0][_g.d.ic_inventory._remark].ToString();

                decimal __qty = (decimal)this._cellGet(row, _g.d.ic_trans_detail._qty);

                Regex __regex = new Regex(@"(\{.(.*).\})");
                MatchCollection __matchs = __regex.Matches(__getItemRemark);
                foreach (Match __match in __matchs)
                {
                    // get match and replace
                    string __matValue = __match.Value;
                    string __getValue = __matValue.Replace("@qty@", __qty.ToString());

                    var __resultProcess = new DataTable().Compute(__getValue.Replace("{", string.Empty).Replace("}", string.Empty), null);
                    __getItemRemark = __getItemRemark.Replace(__matValue, __resultProcess.ToString());
                }

                // update 

                this._cellUpdate(row, _g.d.ic_trans_detail._item_name, __getItemName + "\n" + __getItemRemark.Remove(0, 1), false);
            }
        }

        private void _aferCellUpdateItem(object sender, int row, int column)
        {
            int __wareHouseColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._wh_code);
            int __locationColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._shelf_code);
            int __itemCodeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._item_code);
            int __unitCodeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._unit_code);
            int __priceColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._price);
            int __totalColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._sum_amount);
            int __qtyColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._qty);
            int __discountColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._discount);
            int __barcodeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._barcode);
            int __itemType = (int)this._cellGet(row, _g.d.ic_trans_detail._item_type);
            int __isSerialNumber = (int)this._cellGet(row, _g.d.ic_trans_detail._is_serial_number);

            int __priceColumnNumber2 = this._findColumnByName(_g.d.ic_trans_detail._price_2);
            int __hiddenCostColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._hidden_cost_1);

            string __itemCode = this._cellGet(row, __itemCodeColumnNumber).ToString().ToUpper();
            string __barcode = (__barcodeColumnNumber == -1) ? "" : this._cellGet(row, _g.d.ic_trans_detail._barcode).ToString().ToUpper();

            //
            if (column == __itemCodeColumnNumber)
            {

                if (_g.g._companyProfile._warning_input_customer && (
                    this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เสนอราคา ||
                    this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย ||
                    this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า ||
                    this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้ ||
                    this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้
                    ))
                {
                    if (this._custCode.Length == 0)
                    {
                        MessageBox.Show("ไม่พบรหัสลูกค้า");
                        this._cellUpdate(row, __itemCodeColumnNumber, "", false);
                        return;
                    }
                }


                this._afterUpdateClearQtyAndPrice(row, true);
                // ค้นหา BarCode ก่อน
                string __barCodeStr = __itemCode.Replace("*", "").Replace(" ", "");
                string __unitCode = "";
                //
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_barcode._ic_code + "," + _g.d.ic_inventory_barcode._unit_code + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "=\'" + __barCodeStr + "\'"));
                __query.Append("</node>");
                ArrayList __queryResult = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                //
                DataTable __barCode = ((DataSet)__queryResult[0]).Tables[0];
                if (__barCode.Rows.Count > 0)
                {
                    __itemCode = __barCode.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString();
                    __unitCode = __barCode.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString();
                    this._cellUpdate(row, _g.d.ic_trans_detail._item_code, __itemCode, false);
                    this._cellUpdate(row, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                    if (this._findColumnByName(_g.d.ic_trans_detail._barcode) != -1)
                    {
                        this._cellUpdate(row, _g.d.ic_trans_detail._barcode, __barCodeStr, false);
                    }
                }
                // ตรวจสอบกรณีตรวจนับซ้ำ
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                    case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                        if (this._recheckCount())
                        {
                            // วันที่ตรวจนับซ้ำ ถอยไปกี่วัน
                            DateTime __dateCheck = DateTime.Now.AddDays(((double)(this._recheckCountDay() * -1M)));
                            string __whCode = this._icTransScreenTop._getDataStr(_g.d.ic_trans._wh_from).Trim();
                            __query = new StringBuilder();
                            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType) + " and " + _g.d.ic_trans_detail._item_code + "=\'" + __itemCode.ToUpper() + "\' and " + _g.d.ic_trans_detail._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(__dateCheck) + "\' and " + _g.d.ic_trans_detail._wh_code + "=\'" + __whCode.ToUpper() + "\'"));
                            __query.Append("</node>");
                            __queryResult = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                            DataTable __transDetail = ((DataSet)__queryResult[0]).Tables[0];
                            if (__transDetail.Rows.Count > 0)
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("สินค้าเคยตรวจนับไปแล้ว") + " : " + MyLib._myGlobal._resource("เอกสารเลขที่") + " : " + __transDetail.Rows[0][_g.d.ic_trans_detail._doc_no].ToString() + " " + MyLib._myGlobal._resource("เอกสารวันที่") + " : " + __transDetail.Rows[0][_g.d.ic_trans_detail._doc_date].ToString() + " " + MyLib._myGlobal._resource("จำนวน") + " : " + __transDetail.Rows[0][_g.d.ic_trans_detail._qty].ToString() + " " + __transDetail.Rows[0][_g.d.ic_trans_detail._unit_code].ToString() + " : " + __transDetail.Rows[0][_g.d.ic_trans_detail._wh_code].ToString() + "/" + __transDetail.Rows[0][_g.d.ic_trans_detail._shelf_code].ToString());
                            }
                        }
                        break;
                }
                // ค้นหาสินค้าจาก server กลาง
                if (this._gridFindItem(__itemCode, __barCodeStr, __unitCode, row) == false)
                {
                    this._stopMove = true;
                    this._gotoCell(row, __itemCodeColumnNumber);
                    return;
                }
            }
            if (column == __wareHouseColumnNumber || column == __locationColumnNumber)
            {
                if (this._checkBalance(sender, row, column, __itemCode, __itemCodeColumnNumber, __qtyColumnNumber) == true)
                {
                    return;
                }
            }
            if (column == __qtyColumnNumber)
            {
                if (__priceColumnNumber != -1 && __totalColumnNumber != -1)
                {
                    //
                    if (this._checkAutoQty && MyLib._myGlobal._lastTextBox.IndexOf('/') != -1)
                    {
                        string[] __qtySplit = MyLib._myGlobal._lastTextBox.Split('/');
                        decimal __amount = MyLib._myGlobal._decimalPhase(__qtySplit[0]);
                        decimal __qty = MyLib._myGlobal._decimalPhase(__qtySplit[1]);
                        decimal __price = (__qty == 0) ? 0M : MyLib._myGlobal._round(__amount / __qty, _g.g._companyProfile._item_price_decimal);
                        this._cellUpdate(row, __qtyColumnNumber, __qty, true);
                        this._cellUpdate(row, __priceColumnNumber, __price, true);

                        if (__amount != (__qty * __price))
                        {
                            decimal __discount = (__qty * __price) - __amount;
                            this._cellUpdate(row, __discountColumnNumber, __discount, true);
                        }
                    }
                    else
                    {
                        if (this._checkBalance(sender, row, column, __itemCode, __itemCodeColumnNumber, __qtyColumnNumber) == true)
                        {
                            return;
                        }
                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                            case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                            case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                            case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                            case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                            case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                {
                                    if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า && _g.g._companyProfile._count_stock_display_saleprice == false)
                                    {
                                        decimal __price = (decimal)this._cellGet(row, _g.d.ic_trans_detail._average_cost);
                                        this._cellUpdate(row, __priceColumnNumber, __price, true);
                                    }
                                    else
                                    {
                                        decimal __qty = (decimal)this._cellGet(row, __qtyColumnNumber);
                                        if (__qty != 0)
                                        {
                                            Boolean __findPrice = true;
                                            int __refDocNoColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                                            if (__refDocNoColumnNumber != -1)
                                            {
                                                if (this._cellGet(row, __refDocNoColumnNumber).ToString().Trim().Length > 0)
                                                {
                                                    __findPrice = false;
                                                }
                                            }
                                            if (__findPrice)
                                            {
                                                decimal __price = (decimal)this._cellGet(row, __priceColumnNumber);
                                                _priceStruct __getPrice = this._findPrice(row, __itemCode, __barcode, this._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString(), __qty, this._custCode, true, true, true);
                                                __price = __getPrice._price;
                                                if (__getPrice._discountWordFormula.Length > 0)
                                                {
                                                    this._cellUpdate(row, _g.d.ic_trans_detail._discount, __getPrice._discountWordFormula, false);
                                                }
                                                this._cellUpdate(row, _g.d.ic_trans_detail._price_type, __getPrice._type, false);
                                                this._cellUpdate(row, _g.d.ic_trans_detail._price_mode, __getPrice._mode, false);
                                                this._cellUpdate(row, this._columnPriceRoworder, __getPrice._roworder, false);
                                                this._cellUpdate(row, _g.d.ic_trans_detail._set_ref_price, __price, false);
                                                this._cellUpdate(row, __priceColumnNumber, __price, true);
                                                this._cellUpdate(row, _g.d.ic_trans_detail._price_guid, __getPrice._price_guid, true);

                                                if (__priceColumnNumber2 != -1)
                                                {
                                                    this._cellUpdate(row, __priceColumnNumber2, __price, true);
                                                }

                                                // toe 
                                                if (_g.g._companyProfile._ic_price_formula_control)
                                                {
                                                    this._cellUpdate(row, _g.d.ic_inventory_price_formula._price_0, __getPrice._stand_price, false);
                                                }

                                                if (__getPrice._defaultDiscount.Length > 0)
                                                {
                                                    this._cellUpdate(row, _g.d.ic_trans_detail._discount, __getPrice._defaultDiscount, true);
                                                }
                                            }

                                            _calcItemSetNow(row);
                                        }
                                    }
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                {
                                    if (this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ && _g.g._companyProfile._get_purchase_price == false)
                                    {
                                        break;
                                    }

                                    decimal __qty = (decimal)this._cellGet(row, __qtyColumnNumber);
                                    if (__qty != 0)
                                    {
                                        Boolean __findPrice = true;
                                        int __refDocNoColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                                        int __refDocTypeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._doc_ref_type);
                                        if (__refDocNoColumnNumber != -1)
                                        {
                                            if (this._cellGet(row, __refDocNoColumnNumber).ToString().Trim().Length > 0)
                                            {
                                                __findPrice = false;
                                            }

                                            if (this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ)
                                            {
                                                __findPrice = true;
                                                if (this._cellGet(row, __refDocNoColumnNumber).ToString().Trim().Length > 0 && (this._cellGet(row, __refDocTypeColumnNumber).ToString().Equals("99") == false))
                                                {
                                                    __findPrice = false;
                                                }

                                            }


                                        }
                                        if (__findPrice)
                                        {
                                            decimal __price = (decimal)this._cellGet(row, __priceColumnNumber);
                                            __price = _findPurchasePrice(row, __itemCode, this._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString(), __qty, this._custCode);
                                            this._cellUpdate(row, _g.d.ic_trans_detail._set_ref_price, __price, false);
                                            this._cellUpdate(row, __priceColumnNumber, __price, true);

                                            if (__priceColumnNumber2 != -1)
                                            {
                                                this._cellUpdate(row, __priceColumnNumber2, __price, true);
                                            }
                                        }
                                        _calcItemSetNow(row);
                                    }
                                }
                                break;
                                // toe เอาใส่ option
                                //case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                                //    {
                                //        decimal __price = (decimal)this._cellGet(row, _g.d.ic_trans_detail._average_cost);
                                //        this._cellUpdate(row, __priceColumnNumber, __price, true);
                                //    }
                                //    break;
                        }
                    }
                }
                else
                {
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                        case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                            if (this._checkBalance(sender, row, column, __itemCode, __itemCodeColumnNumber, __qtyColumnNumber) == true)
                            {
                                return;
                            }
                            break;
                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก:
                            {
                                decimal __qty = (decimal)this._cellGet(row, __qtyColumnNumber);
                                if (__qty < 0)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("ห้ามฝากสินค้าจำนวนติดลบ"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this._cellUpdate(row, __qtyColumnNumber, 0M, false);
                                }

                            }
                            break;
                    }
                }

                if (this._cellGet(row, this._columnIsGetItemRemark).ToString().Equals("1"))
                {
                    // call change item name and replace remark
                    _changeItemNameRemarkQty(row);
                }
            }

            if (column == __discountColumnNumber && MyLib._myGlobal._lastTextBox.IndexOf('=') != -1)
            {
                //
                string __discountAmount = MyLib._myGlobal._lastTextBox.Replace("=", string.Empty).Replace('x', '*').Replace('X', '*');

                MyLib._mathParser __formula = new MyLib._mathParser();
                decimal __calcDiscount = (decimal)__formula.Calculate(__discountAmount);
                this._cellUpdate(row, __discountColumnNumber, __calcDiscount.ToString(), true);

            }

            if (column == __hiddenCostColumnNumber && MyLib._myGlobal._lastTextBox.IndexOf('=') != -1)
            {
                /*string __costAmount = MyLib._myGlobal._lastTextBox.Replace("=", string.Empty).Replace('x', '*').Replace('X', '*');
                
                MyLib._mathParser __formula = new MyLib._mathParser();
                decimal __calcCostAmount = (decimal)__formula.Calculate(__costAmount);
                this._cellUpdate(row, __hiddenCostColumnNumber, __calcCostAmount, true);*/
            }

            if (__priceColumnNumber != -1 && __totalColumnNumber != -1)
            {
                _calcItemPrice(row, row, column);
            }
            else
            {
                if (column == __qtyColumnNumber)
                {
                    _calcItemPrice(row, row, column);
                }
            }
            // เตือนเมื่อมีการขายต่ำกว่าทุน  โต๋ เอาออก column == __qtyColumnNumber || 
            if (column == __priceColumnNumber || column == __totalColumnNumber || column == __discountColumnNumber)
            {
                if (_g.g._companyProfile._warning_low_cost)
                {
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                            _checkPriceLowCost(this._selectRow, true);
                            break;
                    }
                }

                // เตือนขายตำกว่าราคากลาง

                if (_g.g._companyProfile._ic_price_formula_control)
                {
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                            if (_checkPriceLowStandPrice(this._selectRow, true))
                            {
                                // เช็คมาจากไหน
                                if (column == __priceColumnNumber)
                                {
                                    this._cellUpdate(row, __priceColumnNumber, 0.0M, true);
                                }

                                if (column == __discountColumnNumber)
                                {
                                    this._cellUpdate(row, __discountColumnNumber, 0.0M, true);
                                }

                                if (column == __totalColumnNumber)
                                {
                                    this._cellUpdate(row, __totalColumnNumber, 0.0M, true);
                                }
                            }
                            break;
                    }
                }

            }

            // ตรวจสอบการอ้างบิล พร้อมดึงยอดตั้งต้น
            if (column == __itemCodeColumnNumber || column == __unitCodeColumnNumber)
            {
                ArrayList __dataResult = null;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __queryRef = new StringBuilder();
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        {
                            int __docRefColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefColumnNumber != -1 && __itemCode.Trim().Length > 0)
                            {
                                string __docRefNo = this._cellGet(row, __docRefColumnNumber).ToString().ToUpper();
                                string __unitCode = this._cellGet(row, __unitCodeColumnNumber).ToString().ToUpper();
                                if (__docRefNo.Length > 0)
                                {
                                    __queryRef.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    // ดึงอัตราส่วนหน่วยนับ
                                    __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                                        " where " + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' and " + _g.d.ic_unit_use._code + "=\'" + __unitCode + "\'"));
                                    // ค้นสินค้าว่ามีหรือเปล่า
                                    __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table +
                                        " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'" +
                                        " and " + _g.d.ic_trans_detail._item_code + "=\'" + __itemCode + "\'" +
                                        " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString()));
                                    __queryRef.Append("</node>");
                                    __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryRef.ToString());
                                    // ปรับปรุงหน่วยนับ เพื่อคำนวณ
                                    DataTable __tablePacking = ((DataSet)__dataResult[0]).Tables[0];
                                    decimal __stand_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._stand_value].ToString());
                                    decimal __divide_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._divide_value].ToString());
                                    //
                                    DataTable __t1 = ((DataSet)__dataResult[1]).Tables[0];
                                    if (__t1.Rows.Count == 0)
                                    {
                                        // ไม่มีสินค้าในการอ้างอิงให้ฟ้อง
                                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบสินค้าในเอกสารอ้างอิง"));
                                        this._cellUpdate(row, __itemCodeColumnNumber, "", false);
                                        this._cellUpdate(row, __qtyColumnNumber, 0M, false);
                                    }
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                        {
                            int __docRefColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefColumnNumber != -1 && __itemCode.Trim().Length > 0)
                            {
                                int __docType = (int)this._cellGet(row, _g.d.ic_trans_detail._doc_ref_type);
                                if (__docType == 2 || __docType == 3)
                                {
                                    // 2=ใบสั่งซื้อ,3=ใบสั่งขาย
                                    string __docRefNo = this._cellGet(row, __docRefColumnNumber).ToString().ToUpper();
                                    string __unitCode = this._cellGet(row, __unitCodeColumnNumber).ToString().ToUpper();
                                    _g.g._transControlTypeEnum __refType1 = (__docType == 2) ? _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า : _g.g._transControlTypeEnum.ขาย_สั่งขาย;
                                    if (__docRefNo.Length > 0)
                                    {
                                        __queryRef.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                        // ดึงอัตราส่วนหน่วยนับ
                                        __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                                            " where " + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' and " + _g.d.ic_unit_use._code + "=\'" + __unitCode + "\'"));
                                        // ค้นสินค้าว่ามีหรือเปล่า
                                        __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table +
                                            " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'" +
                                            " and " + _g.d.ic_trans_detail._item_code + "=\'" + __itemCode + "\'" +
                                            " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(__refType1).ToString()));
                                        __queryRef.Append("</node>");
                                        __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryRef.ToString());
                                        // ปรับปรุงหน่วยนับ เพื่อคำนวณ
                                        DataTable __tablePacking = ((DataSet)__dataResult[0]).Tables[0];
                                        decimal __stand_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._stand_value].ToString());
                                        decimal __divide_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._divide_value].ToString());
                                        //
                                        DataTable __t1 = ((DataSet)__dataResult[1]).Tables[0];
                                        if (__t1.Rows.Count == 0)
                                        {
                                            // ไม่มีสินค้าในการอ้างอิงให้ฟ้อง
                                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบสินค้าในเอกสารอ้างอิง"));
                                            this._cellUpdate(row, __itemCodeColumnNumber, "", false);
                                            this._cellUpdate(row, __qtyColumnNumber, 0M, false);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        {
                            int __docRefColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefColumnNumber != -1 && __itemCode.Trim().Length > 0)
                            {
                                string __docRefNo = this._cellGet(row, __docRefColumnNumber).ToString().ToUpper();
                                string __unitCode = this._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString().ToUpper();
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        {
                            int __docRefColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefColumnNumber != -1 && __itemCode.Trim().Length > 0)
                            {
                                string __docRefNo = this._cellGet(row, __docRefColumnNumber).ToString().ToUpper();
                                string __unitCode = this._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString().ToUpper();
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        {
                            int __docRefColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefColumnNumber != -1 && __itemCode.Trim().Length > 0)
                            {
                                string __docRefNo = this._cellGet(row, __docRefColumnNumber).ToString().ToUpper();
                                string __unitCode = this._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString().ToUpper();
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        {
                            string __docRefNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);
                            string __unitCode = this._cellGet(row, __unitCodeColumnNumber).ToString().ToUpper();
                            if (__docRefNo.Length > 0)
                            {
                                this._processInquiryBalanceQty(row, __itemCode, __unitCode, __docRefNo);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                        {
                            string __docRefNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);
                            string __unitCode = this._cellGet(row, __unitCodeColumnNumber).ToString().ToUpper();
                            if (__docRefNo.Length > 0)
                            {
                                this._processSaleOrderBalanceQty(row, __itemCode, __unitCode, __docRefNo);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                        {
                            string __docRefNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);
                            string __unitCode = this._cellGet(row, __unitCodeColumnNumber).ToString().ToUpper();
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        {
                            int __docRefColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefColumnNumber != -1 && __itemCode.Trim().Length > 0)
                            {
                                string __docRefNo = this._cellGet(row, __docRefColumnNumber).ToString().ToUpper();
                                string __unitCode = this._cellGet(row, __unitCodeColumnNumber).ToString().ToUpper();
                                if (__docRefNo.Length > 0)
                                {
                                    __queryRef.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    // ดึงอัตราส่วนหน่วยนับ
                                    __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                                        " where " + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' and " + _g.d.ic_unit_use._code + "=\'" + __unitCode + "\'"));
                                    // ค้นสินค้าว่ามีหรือเปล่า
                                    __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table +
                                        " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'" +
                                        " and " + _g.d.ic_trans_detail._item_code + "=\'" + __itemCode + "\'" +
                                        " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString()));
                                    __queryRef.Append("</node>");
                                    __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryRef.ToString());
                                    // ปรับปรุงหน่วยนับ เพื่อคำนวณ
                                    DataTable __tablePacking = ((DataSet)__dataResult[0]).Tables[0];
                                    decimal __stand_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._stand_value].ToString());
                                    decimal __divide_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._divide_value].ToString());
                                    //
                                    DataTable __t1 = ((DataSet)__dataResult[1]).Tables[0];
                                    if (__t1.Rows.Count == 0)
                                    {
                                        // ไม่มีสินค้าในการอ้างอิงให้ฟ้อง
                                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบสินค้าในเอกสารอ้างอิง"));
                                        this._cellUpdate(row, __itemCodeColumnNumber, "", false);
                                        this._cellUpdate(row, __qtyColumnNumber, 0M, false);
                                    }
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                        {
                            int __docRefColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefColumnNumber != -1 && __itemCode.Trim().Length > 0)
                            {
                                string __docRefNo = this._cellGet(row, __docRefColumnNumber).ToString().ToUpper();
                                string __unitCode = this._cellGet(row, __unitCodeColumnNumber).ToString().ToUpper();
                                if (__docRefNo.Length > 0)
                                {
                                    __queryRef.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    // ดึงอัตราส่วนหน่วยนับ
                                    __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                                        " where " + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' and " + _g.d.ic_unit_use._code + "=\'" + __unitCode + "\'"));
                                    // ค้นสินค้าว่ามีหรือเปล่า
                                    __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table +
                                        " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'" +
                                        " and " + _g.d.ic_trans_detail._item_code + "=\'" + __itemCode + "\'" +
                                        " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString()));
                                    __queryRef.Append("</node>");
                                    __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryRef.ToString());
                                    // ปรับปรุงหน่วยนับ เพื่อคำนวณ
                                    DataTable __tablePacking = ((DataSet)__dataResult[0]).Tables[0];
                                    decimal __stand_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._stand_value].ToString());
                                    decimal __divide_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._divide_value].ToString());
                                    //
                                    DataTable __t1 = ((DataSet)__dataResult[1]).Tables[0];
                                    if (__t1.Rows.Count == 0)
                                    {
                                        // ไม่มีสินค้าในการอ้างอิงให้ฟ้อง
                                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบสินค้าในเอกสารอ้างอิง"));
                                        this._cellUpdate(row, __itemCodeColumnNumber, "", false);
                                        this._cellUpdate(row, __qtyColumnNumber, 0M, false);
                                    }
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                        {
                            int __docRefColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefColumnNumber != -1 && __itemCode.Trim().Length > 0)
                            {
                                string __docRefNo = this._cellGet(row, __docRefColumnNumber).ToString().ToUpper();
                                string __unitCode = this._cellGet(row, __unitCodeColumnNumber).ToString().ToUpper();
                                if (__docRefNo.Length > 0)
                                {
                                    __queryRef.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    // ดึงอัตราส่วนหน่วยนับ
                                    __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                                        " where " + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' and " + _g.d.ic_unit_use._code + "=\'" + __unitCode + "\'"));
                                    // ค้นสินค้าว่ามีหรือเปล่า
                                    __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table +
                                        " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'" +
                                        " and " + _g.d.ic_trans_detail._item_code + "=\'" + __itemCode + "\'" +
                                        " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ).ToString()));
                                    __queryRef.Append("</node>");
                                    __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryRef.ToString());
                                    // ปรับปรุงหน่วยนับ เพื่อคำนวณ
                                    DataTable __tablePacking = ((DataSet)__dataResult[0]).Tables[0];
                                    decimal __stand_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._stand_value].ToString());
                                    decimal __divide_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._divide_value].ToString());
                                    //
                                    DataTable __t1 = ((DataSet)__dataResult[1]).Tables[0];
                                    if (__t1.Rows.Count == 0)
                                    {
                                        // ไม่มีสินค้าในการอ้างอิงให้ฟ้อง
                                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบสินค้าในเอกสารอ้างอิง"));
                                        this._cellUpdate(row, __itemCodeColumnNumber, "", false);
                                        this._cellUpdate(row, __qtyColumnNumber, 0M, false);
                                    }
                                }
                            }
                        }
                        break;
                }
            }

            // toe ของแถม
            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && column == this._findColumnByName(_g.d.ic_trans_detail._is_permium))
            {
                int __isPremium = MyLib._myGlobal._intPhase(this._cellGet(row, _g.d.ic_trans_detail._is_permium).ToString());

                if (__isPremium == 1)
                {
                    string __itemName = this._cellGet(row, _g.d.ic_trans_detail._item_name).ToString().Replace("แถม ", string.Empty);
                    this._cellUpdate(row, _g.d.ic_trans_detail._item_name, "แถม " + __itemName, true);
                }
            }

            this._calcRefQty();
        }

        public void _ictransItemGridControl__alterCellUpdate(object sender, int row, int column)
        {
            try
            {
                // ปรับสถานะ
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        {
                            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้)
                            {

                            }
                            else
                            {
                                string __docRefNo = this._cellGet(row, _g.d.ic_trans_detail._ref_doc_no).ToString().Trim();
                                this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref_type, (__docRefNo.Length > 0) ? 1 : 0, false);
                            }
                        }
                        break;
                }
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                        {
                            if (column == this._findColumnByName(_g.d.ic_trans_detail._item_code))
                            {
                                // search data relate doc
                                string __getDocNo = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();

                                string __query = "select " + _g.d.ic_trans._doc_no + ", " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._cust_code + ",((select name_1 from ar_customer where ar_customer.code = ic_trans.cust_code)) as " + _g.d.ic_trans._cust_name + " from " + _g.d.ic_trans._table + " where doc_no = '" + __getDocNo + "' and trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " and last_status = 0";

                                DataTable __result = _myFrameWork._queryShort(__query).Tables[0];
                                if (__result.Rows.Count > 0)
                                {
                                    DateTime __getDocDate = MyLib._myGlobal._convertDateFromQuery(__result.Rows[0][_g.d.ic_trans._doc_date].ToString());
                                    string __getCustCode = __result.Rows[0][_g.d.ic_trans._cust_code].ToString();
                                    string __getCustName = __result.Rows[0][_g.d.ic_trans._cust_name].ToString();

                                    Decimal __getTotalAmount = MyLib._myGlobal._decimalPhase(__result.Rows[0][_g.d.ic_trans._total_amount].ToString());

                                    this._cellUpdate(row, _g.d.ic_trans_detail._ref_doc_date, __getDocDate, true);
                                    this._cellUpdate(row, _g.d.ic_trans_detail._item_name, __getCustCode, true);
                                    this._cellUpdate(row, _g.d.ic_trans._cust_name, __getCustName, true);
                                    this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, __getTotalAmount, true);

                                }
                                else
                                {
                                    MessageBox.Show("ไม่พบข้อมูลเอกสาร " + __getDocNo);
                                }

                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        {
                            if (column == this._findColumnByName(_g.d.ic_trans_detail._item_code))
                            {
                                string __chqNumber = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                                string __doc_ref = this._cellGet(row, _g.d.ic_trans_detail._doc_ref).ToString();
                                string __ref_row = this._cellGet(row, _g.d.ic_trans_detail._ref_row).ToString();

                                string __extraChqFilter = "";
                                if (__doc_ref.Length > 0)
                                {
                                    __extraChqFilter = " and " + _g.d.cb_chq_list._doc_ref + "=\'" + __doc_ref + "\' and coalesce(" + _g.d.cb_chq_list._doc_line_number + ", 0)=" + __ref_row;
                                }

                                if (__chqNumber.Length > 0)
                                {
                                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                    StringBuilder __query = new StringBuilder();
                                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_chq_list._bank_code + "," + _g.d.cb_chq_list._bank_branch + "," + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_type + "," + _g.d.cb_chq_list._amount + "," + _g.d.cb_chq_list._doc_ref + "," + _g.d.cb_chq_list._doc_line_number + "," + _g.d.cb_chq_list._chq_due_date + " from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + "=\'" + __chqNumber + "\' and " + _g.d.cb_chq_list._chq_type + "=2" + __extraChqFilter));
                                    __query.Append("</node>");
                                    ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                                    DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                    if (__getData.Rows.Count == 0)
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref, "", false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref, 0, false);
                                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเลขที่เช็ค"));
                                        this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, 0M, true);

                                    }
                                    else
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._date_due, MyLib._myGlobal._convertDateFromQuery(__getData.Rows[0][_g.d.cb_chq_list._chq_due_date].ToString()), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __getData.Rows[0][_g.d.cb_chq_list._bank_code].ToString(), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __getData.Rows[0][_g.d.cb_chq_list._bank_branch].ToString(), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.cb_chq_list._amount].ToString()), true);
                                    }

                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                        {
                            if (column == this._findColumnByName(_g.d.ic_trans_detail._item_code))
                            {
                                string __chqNumber = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                                string __doc_ref = this._cellGet(row, _g.d.ic_trans_detail._doc_ref).ToString();
                                string __ref_row = this._cellGet(row, _g.d.ic_trans_detail._ref_row).ToString();

                                string __extraChqFilter = "";
                                if (__doc_ref.Length > 0)
                                {
                                    __extraChqFilter = " and " + _g.d.cb_chq_list._doc_ref + "=\'" + __doc_ref + "\' and coalesce(" + _g.d.cb_chq_list._doc_line_number + ", 0)=" + __ref_row;
                                }

                                if (__chqNumber.Length > 0)
                                {
                                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                    StringBuilder __query = new StringBuilder();
                                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_chq_list._bank_code + "," + _g.d.cb_chq_list._bank_branch + "," + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_type + "," + _g.d.cb_chq_list._amount + "," + _g.d.cb_chq_list._doc_ref + "," + _g.d.cb_chq_list._doc_line_number + "," + _g.d.cb_chq_list._chq_due_date + " from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + "=\'" + __chqNumber + "\' and " + _g.d.cb_chq_list._chq_type + "=1" + __extraChqFilter));
                                    __query.Append("</node>");
                                    ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                                    DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                    if (__getData.Rows.Count == 0)
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref, "", false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref, 0, false);
                                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเลขที่เช็ค"));
                                        this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, 0M, true);

                                    }
                                    else
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._date_due, MyLib._myGlobal._convertDateFromQuery(__getData.Rows[0][_g.d.cb_chq_list._chq_due_date].ToString()), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __getData.Rows[0][_g.d.cb_chq_list._bank_code].ToString(), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __getData.Rows[0][_g.d.cb_chq_list._bank_branch].ToString(), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.cb_chq_list._amount].ToString()), true);
                                    }

                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            if (sender == null || column == this._findColumnByName(_g.d.ic_trans_detail._bank_name))
                            {
                                StringBuilder __query = new StringBuilder();
                                string __bankCode = this._cellGet(row, _g.d.ic_trans_detail._bank_name).ToString().Trim();
                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._code + "=\'" + __bankCode + "\'"));
                                __query.Append("</node>");
                                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                if (__getData.Rows.Count > 0)
                                {
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __bankCode + "~" + __getData.Rows[0][0].ToString(), false);
                                    if (sender != null)
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, "", false);
                                    }
                                }
                                else
                                {
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, "", false);
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, "", false);
                                }
                            }
                            if (sender == null || column == this._findColumnByName(_g.d.ic_trans_detail._bank_branch))
                            {
                                StringBuilder __query = new StringBuilder();
                                string __bankCode = this._cellGet(row, _g.d.ic_trans_detail._bank_name).ToString().Trim();
                                string __bankBranchCode = this._cellGet(row, _g.d.ic_trans_detail._bank_branch).ToString().Trim();
                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._bank_code + "=\'" + __bankCode + "\' and " + _g.d.erp_bank_branch._code + "=\'" + __bankBranchCode + "\'"));
                                __query.Append("</node>");
                                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                if (__getData.Rows.Count > 0)
                                {
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __bankBranchCode + "~" + __getData.Rows[0][0].ToString(), false);
                                }
                                else
                                {
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, "", false);
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, "", false);
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                        {
                            if (this._icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก)
                            {
                                this._cellUpdate(row, _g.d.ic_trans_detail._item_code, this._cellGet(row, _g.d.ic_trans_detail._chq_number).ToString(), false);
                            }
                            if (column == this._findColumnByName(_g.d.ic_trans_detail._price) || column == this._findColumnByName(_g.d.ic_trans_detail._sum_of_cost))
                            {
                                decimal __amount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._price).ToString());
                                decimal __expense = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._sum_of_cost).ToString());
                                this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, __amount - __expense, true);
                            }
                            else
                            {
                                string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                                string __chqNumber = this._cellGet(row, _g.d.ic_trans_detail._chq_number).ToString().ToUpper();
                                string __doc_ref = this._cellGet(row, _g.d.ic_trans_detail._doc_ref).ToString();
                                string __ref_row = this._cellGet(row, _g.d.ic_trans_detail._ref_row).ToString();

                                // toe ใส่ป้องกันข้อมูลเก่า
                                string __extraChqFilter = "";
                                if (__doc_ref.Length > 0)
                                {
                                    __extraChqFilter = " and " + _g.d.cb_chq_list._doc_ref + "=\'" + __doc_ref + "\' and coalesce(" + _g.d.cb_chq_list._doc_line_number + ", 0)=" + __ref_row;
                                }

                                //
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                StringBuilder __query = new StringBuilder();
                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._bank_code + "||\'~\'||(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + ") as " + _g.d.erp_pass_book._bank_code + "," + _g.d.erp_pass_book._bank_branch + "||\'~\'||(select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + " and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_branch + ") as " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + __bookCode + "\'"));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_type + ", " + _g.d.cb_chq_list._sum_amount + " as " + _g.d.cb_chq_list._amount + " from " + _g.d.cb_chq_list._table + " where " + MyLib._myGlobal._addUpper(_g.d.cb_chq_list._chq_number) + "=\'" + __chqNumber + "\' and " + _g.d.cb_chq_list._chq_type + "=3 and coalesce(" + _g.d.cb_chq_list._status + ", 0)=0" + __extraChqFilter));
                                __query.Append("</node>");
                                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                {
                                    DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                    if (__getData.Rows.Count > 0)
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __getData.Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __getData.Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
                                    }
                                }
                                if (column == this._findColumnByName(_g.d.ic_trans_detail._chq_number))
                                {
                                    if (__chqNumber.Length > 0)
                                    {
                                        DataTable __getData = ((DataSet)__dataResult[1]).Tables[0];
                                        if (__getData.Rows.Count == 0)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเลขที่บัตรเครดิต"));
                                            this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, 0M, true);
                                            this._cellUpdate(row, _g.d.ic_trans_detail._chq_number, "", false);
                                        }
                                        else
                                        {
                                            this._cellUpdate(row, _g.d.ic_trans_detail._price, MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.cb_chq_list._amount].ToString()), true);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                        {
                            /*if (this._icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก)
                            {
                                this._cellUpdate(row, _g.d.ic_trans_detail._item_code, this._cellGet(row, _g.d.ic_trans_detail._chq_number).ToString(), false);
                            }*/
                            if (column == this._findColumnByName(_g.d.ic_trans_detail._price) || column == this._findColumnByName(_g.d.ic_trans_detail._sum_of_cost))
                            {
                                decimal __amount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._price).ToString());
                                decimal __expense = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._sum_of_cost).ToString());
                                this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, __amount - __expense, true);
                            }
                            else
                            {
                                string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                                string __chqNumber = this._cellGet(row, _g.d.ic_trans_detail._chq_number).ToString().ToUpper();

                                string __extra_chq_filter = "";
                                if (this._icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน)
                                {
                                    __extra_chq_filter = " and " + _g.d.cb_chq_list._status + " in (0, 1, 3) ";
                                }

                                //
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                StringBuilder __query = new StringBuilder();
                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._bank_code + "||\'~\'||(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + ") as " + _g.d.erp_pass_book._bank_code + "," + _g.d.erp_pass_book._bank_branch + "||\'~\'||(select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + " and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_branch + ") as " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + __bookCode + "\'"));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_type + "," + _g.d.cb_chq_list._amount + "," + _g.d.cb_chq_list._status + "," + _g.d.cb_chq_list._chq_due_date + "," + _g.d.cb_chq_list._doc_line_number + "," + _g.d.cb_chq_list._doc_ref + " from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + "=\'" + __chqNumber + "\' and " + _g.d.cb_chq_list._chq_type + "=2" + __extra_chq_filter));
                                __query.Append("</node>");
                                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                {
                                    DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                    if (__getData.Rows.Count > 0)
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __getData.Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __getData.Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
                                    }
                                }
                                if (column == this._findColumnByName(_g.d.ic_trans_detail._chq_number))
                                {
                                    if (__chqNumber.Length > 0)
                                    {
                                        DataTable __getData = ((DataSet)__dataResult[1]).Tables[0];
                                        if (__getData.Rows.Count == 0)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเลขที่เช็ค"));
                                            this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, 0M, true);
                                            this._cellUpdate(row, _g.d.ic_trans_detail._chq_number, "", false);
                                            this._cellUpdate(row, _g.d.ic_trans_detail._price, 0M, false);
                                        }
                                        else
                                        {
                                            this._cellUpdate(row, _g.d.ic_trans_detail._price, MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.cb_chq_list._amount].ToString()), true);

                                            //int __doc_line_Number = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.cb_chq_list._doc_line_number].ToString());
                                            //this._cellUpdate(row, _g.d.ic_trans_detail._ref_row, __doc_line_Number, false);

                                            int __getChqDueDateColumnsIndex = this._findColumnByName(_g.d.ic_trans_detail._date_due);
                                            if (__getChqDueDateColumnsIndex != -1)
                                            {
                                                this._cellUpdate(row, _g.d.ic_trans_detail._date_due, MyLib._myGlobal._convertDateFromQuery(__getData.Rows[0][_g.d.cb_chq_list._chq_due_date].ToString()), true);
                                            }

                                            string __doc_ref_tmp = this._cellGet(row, _g.d.ic_trans_detail._doc_ref).ToString();
                                            if (__doc_ref_tmp.Length == 0)
                                            {
                                                this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref, __getData.Rows[0][_g.d.cb_chq_list._doc_ref].ToString(), true);
                                                this._cellUpdate(row, _g.d.ic_trans_detail._ref_row, MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.cb_chq_list._doc_line_number].ToString()), true);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                        {
                            if (this._icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก)
                            {
                                this._cellUpdate(row, _g.d.ic_trans_detail._item_code, this._cellGet(row, _g.d.ic_trans_detail._chq_number).ToString(), false);
                            }
                            if (column == this._findColumnByName(_g.d.ic_trans_detail._price) || column == this._findColumnByName(_g.d.ic_trans_detail._sum_of_cost))
                            {
                                decimal __amount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._price).ToString());
                                decimal __expense = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._sum_of_cost).ToString());
                                this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, __amount - __expense, true);
                            }
                            else
                            {
                                string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                                string __chqNumber = this._cellGet(row, _g.d.ic_trans_detail._chq_number).ToString().ToUpper();
                                string __doc_ref = this._cellGet(row, _g.d.ic_trans_detail._doc_ref).ToString();
                                string __ref_row = this._cellGet(row, _g.d.ic_trans_detail._ref_row).ToString();

                                // toe ใส่ป้องกันข้อมูลเก่า
                                string __extraChqFilter = "";
                                if (__doc_ref.Length > 0)
                                {
                                    __extraChqFilter = " and " + _g.d.cb_chq_list._doc_ref + "=\'" + __doc_ref + "\' and coalesce(" + _g.d.cb_chq_list._doc_line_number + ", 0)=" + __ref_row;
                                }
                                //

                                string __extra_chq_filter = "";
                                if (this._icTransControlType == _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน)
                                {
                                    __extra_chq_filter = " and " + _g.d.cb_chq_list._status + " in (1) ";
                                }

                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                StringBuilder __query = new StringBuilder();
                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._bank_code + "||\'~\'||(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + ") as " + _g.d.erp_pass_book._bank_code + "," +
                                    _g.d.erp_pass_book._bank_branch + "||\'~\'||(select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + " and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_branch + ") as " + _g.d.erp_pass_book._bank_branch + "," +
                                    _g.d.erp_pass_book._chq_fee_rate + "," +
                                    _g.d.erp_pass_book._chq_fee_rate_amount + "," +
                                    _g.d.erp_pass_book._max_chq_fee_rate + "," +
                                    _g.d.erp_pass_book._max_chq_fee_amount + "," +
                                    _g.d.erp_pass_book._min_chq_fee_rate + "," +
                                    _g.d.erp_pass_book._min_chq_fee_amount +
                                    " from " + _g.d.erp_pass_book._table +
                                    " where " + _g.d.erp_pass_book._code + "=\'" + __bookCode + "\'"));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_type + "," + _g.d.cb_chq_list._amount + "," + _g.d.cb_chq_list._chq_due_date + "," + _g.d.cb_chq_list._external_chq + "," + _g.d.cb_chq_list._doc_ref + "," + _g.d.cb_chq_list._doc_line_number + " from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + "=\'" + __chqNumber + "\' and " + _g.d.cb_chq_list._chq_type + "=1" + __extra_chq_filter + __extraChqFilter));
                                __query.Append("</node>");
                                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                {
                                    DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                    if (__getData.Rows.Count > 0)
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __getData.Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __getData.Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
                                    }
                                }
                                if (column == this._findColumnByName(_g.d.ic_trans_detail._chq_number))
                                {
                                    if (__chqNumber.Length > 0)
                                    {
                                        DataTable __getData = ((DataSet)__dataResult[1]).Tables[0];
                                        if (__getData.Rows.Count == 0)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเลขที่เช็ค"));
                                            this._cellUpdate(row, _g.d.ic_trans_detail._chq_number, "", false);
                                            this._cellUpdate(row, _g.d.ic_trans_detail._price, 0M, false);
                                            this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, 0M, true);
                                        }
                                        else
                                        {
                                            int __external_chq = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.cb_chq_list._external_chq].ToString());
                                            decimal __chq_amount = MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.cb_chq_list._amount].ToString());

                                            this._cellUpdate(row, _g.d.ic_trans_detail._price, __chq_amount, true);

                                            // calc chq fee rate
                                            if (__external_chq == 1 && (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex")))
                                            {
                                                DataTable __getRateData = ((DataSet)__dataResult[0]).Tables[0];
                                                if (__getRateData.Rows.Count > 0)
                                                {
                                                    decimal __chq_fee_rate = MyLib._myGlobal._decimalPhase(__getRateData.Rows[0][_g.d.erp_pass_book._chq_fee_rate].ToString());
                                                    decimal __chq_fee_rate_amount = MyLib._myGlobal._decimalPhase(__getRateData.Rows[0][_g.d.erp_pass_book._chq_fee_rate_amount].ToString());
                                                    decimal __min_chq_fee_rate = MyLib._myGlobal._decimalPhase(__getRateData.Rows[0][_g.d.erp_pass_book._min_chq_fee_rate].ToString());
                                                    decimal __min_chq_fee_amount = MyLib._myGlobal._decimalPhase(__getRateData.Rows[0][_g.d.erp_pass_book._min_chq_fee_amount].ToString());
                                                    decimal __max_chq_fee_rate = MyLib._myGlobal._decimalPhase(__getRateData.Rows[0][_g.d.erp_pass_book._max_chq_fee_rate].ToString());
                                                    decimal __max_chq_fee_amount = MyLib._myGlobal._decimalPhase(__getRateData.Rows[0][_g.d.erp_pass_book._max_chq_fee_amount].ToString());

                                                    if (__chq_fee_rate_amount != 0)
                                                    {
                                                        decimal __feeAmount = (__chq_amount / __chq_fee_rate_amount) * __chq_fee_rate;

                                                        if (__feeAmount < __min_chq_fee_rate)
                                                        {
                                                            __feeAmount = __min_chq_fee_rate;
                                                        }
                                                        else if (__feeAmount > __max_chq_fee_rate)
                                                        {
                                                            __feeAmount = __max_chq_fee_rate;
                                                        }

                                                        this._cellUpdate(row, _g.d.ic_trans_detail._sum_of_cost, __feeAmount, true);

                                                    }
                                                }
                                            }

                                            int __getChqDueDateColumnsIndex = this._findColumnByName(_g.d.ic_trans_detail._date_due);
                                            if (__getChqDueDateColumnsIndex != -1)
                                            {
                                                this._cellUpdate(row, _g.d.ic_trans_detail._date_due, MyLib._myGlobal._convertDateFromQuery(__getData.Rows[0][_g.d.cb_chq_list._chq_due_date].ToString()), true);
                                            }


                                            string __doc_ref_tmp = this._cellGet(row, _g.d.ic_trans_detail._doc_ref).ToString();
                                            if (__doc_ref_tmp.Length == 0)
                                            {
                                                this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref, __getData.Rows[0][_g.d.cb_chq_list._doc_ref].ToString(), true);
                                                this._cellUpdate(row, _g.d.ic_trans_detail._ref_row, MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.cb_chq_list._doc_line_number].ToString()), true);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                        {
                            string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                            //
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            StringBuilder __query = new StringBuilder();
                            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._bank_code + "||\'~\'||(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + ") as " + _g.d.erp_pass_book._bank_code + "," + _g.d.erp_pass_book._bank_branch + "||\'~\'||(select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + " and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_branch + ") as " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + __bookCode + "\'"));
                            __query.Append("</node>");
                            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                            {
                                DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                if (__getData.Rows.Count > 0)
                                {
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __getData.Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __getData.Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                        {
                            string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                            string __chqNumber = this._cellGet(row, _g.d.ic_trans_detail._chq_number).ToString().ToUpper();
                            string __doc_ref = this._cellGet(row, _g.d.ic_trans_detail._doc_ref).ToString();
                            string __ref_row = this._cellGet(row, _g.d.ic_trans_detail._ref_row).ToString();

                            // toe ใส่ป้องกันข้อมูลเก่า
                            string __extraChqFilter = "";
                            if (__doc_ref.Length > 0)
                            {
                                __extraChqFilter = " and " + _g.d.cb_chq_list._doc_ref + "=\'" + __doc_ref + "\' and coalesce(" + _g.d.cb_chq_list._doc_line_number + ", 0)=" + __ref_row;
                            }

                            //
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            StringBuilder __query = new StringBuilder();
                            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._bank_code + "||\'~\'||(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + ") as " + _g.d.erp_pass_book._bank_code + "," + _g.d.erp_pass_book._bank_branch + "||\'~\'||(select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + " and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_branch + ") as " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + __bookCode + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_type + "," + _g.d.cb_chq_list._amount + "," + _g.d.cb_chq_list._doc_ref + "," + _g.d.cb_chq_list._doc_line_number + " from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + "=\'" + __chqNumber + "\' and " + _g.d.cb_chq_list._chq_type + "=1" + __extraChqFilter));
                            __query.Append("</node>");
                            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                            {
                                DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                if (__getData.Rows.Count > 0)
                                {
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __getData.Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __getData.Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
                                }
                            }
                            if (column == this._findColumnByName(_g.d.ic_trans_detail._chq_number))
                            {
                                if (__chqNumber.Length > 0)
                                {
                                    DataTable __getData = ((DataSet)__dataResult[1]).Tables[0];
                                    if (__getData.Rows.Count == 0)
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref, "", false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref, 0, false);
                                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเลขที่เช็ค"));
                                        this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, 0M, true);

                                    }
                                    else
                                    {

                                        this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.cb_chq_list._amount].ToString()), true);


                                        string __doc_ref_tmp = this._cellGet(row, _g.d.ic_trans_detail._doc_ref).ToString();
                                        if (__doc_ref_tmp.Length == 0)
                                        {
                                            this._cellUpdate(row, _g.d.ic_trans_detail._doc_ref, __getData.Rows[0][_g.d.cb_chq_list._doc_ref].ToString(), true);
                                            this._cellUpdate(row, _g.d.ic_trans_detail._ref_row, MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.cb_chq_list._doc_line_number].ToString()), true);

                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                        {
                            string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                            //
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            StringBuilder __query = new StringBuilder();
                            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._bank_code + "||\'~\'||(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + ") as " + _g.d.erp_pass_book._bank_code + "," + _g.d.erp_pass_book._bank_branch + "||\'~\'||(select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + " and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_branch + ") as " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + __bookCode + "\'"));
                            __query.Append("</node>");
                            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                            {
                                DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                if (__getData.Rows.Count > 0)
                                {
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __getData.Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                                    this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __getData.Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
                                }
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                        {
                            int __columnBookCodeFrom = this._findColumnByName(_g.d.ic_trans_detail._item_code);
                            int __columnBookCodeTo = this._findColumnByName(_g.d.ic_trans_detail._item_code_2);
                            int __columnTransferAmount = this._findColumnByName(_g.d.ic_trans_detail._transfer_amount);
                            int __columnFeeAmount = this._findColumnByName(_g.d.ic_trans_detail._fee_amount);

                            if (column == __columnBookCodeFrom)
                            {
                                string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                StringBuilder __query = new StringBuilder();
                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._bank_code + "||\'~\'||(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + ") as " + _g.d.erp_pass_book._bank_code + "," + _g.d.erp_pass_book._bank_branch + "||\'~\'||(select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + " and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_branch + ") as " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + __bookCode + "\'"));
                                __query.Append("</node>");
                                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                {
                                    DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                    if (__getData.Rows.Count > 0)
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_name, __getData.Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch, __getData.Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
                                    }
                                }

                            }

                            if (column == __columnBookCodeTo)
                            {
                                string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code_2).ToString().ToUpper();
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                StringBuilder __query = new StringBuilder();
                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_pass_book._bank_code + "||\'~\'||(select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._table + "." + _g.d.erp_bank._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + ") as " + _g.d.erp_pass_book._bank_code + "," + _g.d.erp_pass_book._bank_branch + "||\'~\'||(select " + _g.d.erp_bank_branch._name_1 + " from " + _g.d.erp_bank_branch._table + " where " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._bank_code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_code + " and " + _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code + "=" + _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._bank_branch + ") as " + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + __bookCode + "\'"));
                                __query.Append("</node>");
                                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                                {
                                    DataTable __getData = ((DataSet)__dataResult[0]).Tables[0];
                                    if (__getData.Rows.Count > 0)
                                    {
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_name_2, __getData.Rows[0][_g.d.erp_pass_book._bank_code].ToString(), false);
                                        this._cellUpdate(row, _g.d.ic_trans_detail._bank_branch_2, __getData.Rows[0][_g.d.erp_pass_book._bank_branch].ToString(), false);
                                    }
                                }

                            }

                            if (column == __columnTransferAmount || column == __columnFeeAmount)
                            {
                                //
                                decimal __transferAmount = (decimal)this._cellGet(row, _g.d.ic_trans_detail._transfer_amount);
                                decimal __feeAmount = (decimal)this._cellGet(row, _g.d.ic_trans_detail._fee_amount);
                                decimal __sumAmount = (__transferAmount + __feeAmount);

                                this._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, __sumAmount, true);
                            }

                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                        {
                            int __codeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._item_code);
                            string __code = this._cellGet(row, __codeColumnNumber).ToString().ToUpper();
                            string __name = "";
                            //
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            string __query = "select " + _g.d.cb_petty_cash._name_1 + " from " + _g.d.cb_petty_cash._table + " where " + _g.d.cb_petty_cash._code + "=\'" + __code + "\'";
                            DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                            if (__getData.Rows.Count > 0)
                            {
                                __name = __getData.Rows[0][_g.d.cb_petty_cash._name_1].ToString();
                                this._cellUpdate(row, _g.d.ic_trans_detail._item_name, __name, false);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        {
                            int __codeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._item_code);
                            string __code = this._cellGet(row, __codeColumnNumber).ToString().ToUpper();
                            string __name = "";
                            //
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            string __query = "select " + _g.d.erp_expenses_list._name_1 + " from " + _g.d.erp_expenses_list._table + " where " + _g.d.erp_expenses_list._code + "=\'" + __code + "\'";
                            DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                            if (__getData.Rows.Count > 0)
                            {
                                __name = __getData.Rows[0][_g.d.erp_expenses_list._name_1].ToString();
                                this._cellUpdate(row, _g.d.ic_trans_detail._item_name, __name, false);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        {
                            int __codeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._item_code);
                            int __amountColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._sum_amount);
                            string __code = this._cellGet(row, __codeColumnNumber).ToString().ToUpper();
                            string __name = "";
                            //
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            string __query = "select " + _g.d.erp_income_list._name_1 + " from " + _g.d.erp_income_list._table + " where " + _g.d.erp_income_list._code + "=\'" + __code + "\'";
                            DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                            if (__getData.Rows.Count > 0)
                            {
                                __name = __getData.Rows[0][_g.d.erp_income_list._name_1].ToString();
                                this._cellUpdate(row, _g.d.ic_trans_detail._item_name, __name, false);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                        {


                            int __columnQty = this._findColumnByName(_g.d.ic_trans_detail._qty);
                            int __columnItemCode = this._findColumnByName(_g.d.ic_trans_detail._item_code);
                            if (column == __columnItemCode)
                            {
                                // check doc ref
                                string __getDocRef = this._cellGet(row, _g.d.ic_trans_detail._ref_doc_no).ToString();
                                if (__getDocRef.Length == 0)
                                {
                                    MessageBox.Show("กรุณาเลือกเอกสารอ้างอิง");
                                    this._cellUpdate(row, _g.d.ic_trans_detail._item_code, "", false);
                                }
                            }

                            if (column == __columnQty)
                            {
                                // check ยอดคงเหลือ
                                decimal __totalQty = (decimal)this._cellGet(row, _g.d.ic_trans_detail._total_qty);
                                decimal __qty = (decimal)this._cellGet(row, _g.d.ic_trans_detail._qty);

                                string __getDocRef = this._cellGet(row, _g.d.ic_trans_detail._ref_doc_no).ToString();
                                if (__getDocRef.Length == 0)
                                {
                                    MessageBox.Show("กรุณาเลือกเอกสารอ้างอิง");
                                    this._cellUpdate(row, _g.d.ic_trans_detail._qty, 0M, false);
                                }


                                if (__qty > __totalQty)
                                {
                                    if (this._icTransControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก)
                                        MessageBox.Show("ห้ามรับคืนเกินยอดเบิกสินค้า");
                                    else
                                        MessageBox.Show("ห้ามเบิกเกินยอดฝากสินค้า");
                                    this._cellUpdate(row, _g.d.ic_trans_detail._qty, 0M, false);
                                }
                            }
                        }
                        break;
                    default:
                        this._aferCellUpdateItem(sender, row, column);
                        break;
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + ":" + __ex.StackTrace.ToString());
            }
            this.Invalidate();
        }

        /// <summary>
        /// กระทบยอดคงเหลือทำรายการได้
        /// </summary>
        private void _calcRefQty()
        {
            int __columnItemCode = this._findColumnByName(_g.d.ic_trans_detail._item_code);
            int __columnQty = this._findColumnByName(_g.d.ic_trans_detail._qty);
            int __columnStandValue = this._findColumnByName(_g.d.ic_trans_detail._stand_value);
            int __columnDivideValue = this._findColumnByName(_g.d.ic_trans_detail._divide_value);
            //
            if (__columnDivideValue != -1 && __columnQty != -1 && __columnStandValue != -1)
            {
                ArrayList __itemList = new ArrayList();
                for (int __row = 0; __row < this._rowData.Count; __row++)
                {
                    string __itemCode = this._cellGet(__row, __columnItemCode).ToString();
                    if (__itemCode.Length > 0)
                    {
                        Boolean __found = false;
                        for (int __find = 0; __find < __itemList.Count; __find++)
                        {
                            string __itemCodeCompare = __itemList[__find].ToString();
                            if (__itemCodeCompare.ToLower().Equals(__itemCode.ToLower()))
                            {
                                __found = true;
                                break;
                            }
                        }
                        if (__found == false)
                        {
                            __itemList.Add(__itemCode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// คำนวณหายอดยกเลิกใบสั่งซื้อ/สั่งจอง (ขาย)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="itemCode"></param>
        /// <param name="unitCode"></param>
        /// <param name="docRefNo"></param>
        private void _processInquiryBalanceQty(int row, string itemCode, string unitCode, string docRefNo)
        {
            StringBuilder __queryRef = new StringBuilder();
            __queryRef.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // ดึงอัตราส่วนหน่วยนับ
            __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                " where " + _g.d.ic_unit_use._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_unit_use._code + "=\'" + unitCode + "\'"));
            __queryRef.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryRef.ToString());
            // ปรับปรุงหน่วยนับ เพื่อคำนวณ
            DataTable __tablePacking = ((DataSet)__dataResult[0]).Tables[0];
            decimal __stand_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._stand_value].ToString());
            decimal __divide_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._divide_value].ToString());
        }

        private void _processSaleOrderBalanceQty(int row, string itemCode, string unitCode, string docRefNo)
        {
            StringBuilder __queryRef = new StringBuilder();
            __queryRef.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // ดึงอัตราส่วนหน่วยนับ
            __queryRef.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                " where " + _g.d.ic_unit_use._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_unit_use._code + "=\'" + unitCode + "\'"));
            __queryRef.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryRef.ToString());
            // ปรับปรุงหน่วยนับ เพื่อคำนวณ
            DataTable __tablePacking = ((DataSet)__dataResult[0]).Tables[0];
            decimal __stand_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._stand_value].ToString());
            decimal __divide_value = MyLib._myGlobal._decimalPhase(__tablePacking.Rows[0][_g.d.ic_trans_detail._divide_value].ToString());
        }

        private Boolean _checkPriceLowStandPrice(int row, Boolean warning)
        {
            Boolean __result = false;
            try
            {
                decimal __stand_price = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_inventory_price_formula._price_0).ToString());
                decimal __price = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._price).ToString());
                decimal __qty = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._qty).ToString());

                decimal __calc = __price * __qty;
                string __discountWord = this._cellGet(row, _g.d.ic_trans_detail._discount).ToString();

                decimal __total_amount = MyLib._myGlobal._calcAfterDiscount(__discountWord, MyLib._myGlobal._round(__calc, _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
                decimal __standPriceQty = __stand_price * __qty;

                if (__standPriceQty > __total_amount)
                {
                    if (warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ห้ามขายต่ำกว่าราคากลาง"), "Warning");
                    }
                    __result = true;
                }
            }
            catch (Exception ex)
            {
            }
            return __result;
        }

        private Boolean _checkPriceLowCost(int row, Boolean warning)
        {
            Boolean __result = false;
            try
            {
                decimal __amount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._sum_amount).ToString());
                decimal __qty = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._qty).ToString());
                decimal __unitStand = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._stand_value).ToString());
                decimal __unitDiv = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._divide_value).ToString());
                decimal __price = (__qty == 0 || __unitDiv == 0) ? 0 : (__amount / (__qty * (__unitStand / __unitDiv)));
                //decimal __price = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._price).ToString());
                decimal __unitCostStand = MyLib._myGlobal._decimalPhase(this._cellGet(row, this._columnAverageCostUnitStand).ToString());
                decimal __unitCostDiv = MyLib._myGlobal._decimalPhase(this._cellGet(row, this._columnAverageCostUnitDiv).ToString());
                decimal __cost = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.ic_trans_detail._average_cost).ToString());

                if (this._vatTypeNumber() == 1)
                {
                    // รวมใน แสดงว่าใบเสนอราคาแยกนอก
                    //__price = __price - ((__price * _g.g._companyProfile._vat_rate) / 100M);
                    __price = (__price * 100M) / (100M + this._vatRate());
                }
                __price = (__unitCostDiv == 0M) ? 0M : __price * (__unitCostStand / __unitCostDiv);
                if (__qty != 0M && __price != 0M && __price < __cost)
                {
                    if (warning)
                    {
                        DialogResult __check = MessageBox.Show(MyLib._myGlobal._resource("ราคาขายต่ำกว่าต้นทุน"), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        if (__check == DialogResult.No)
                        {
                            this._cellUpdate(row, _g.d.ic_trans_detail._price, 0.0M, true);
                        }
                    }
                    __result = true;
                }
            }
            catch (Exception __ex)
            {
            }

            return __result;
        }

        /// <summary>
        /// เอาไว้ call หลังจาก กด pocess อ้างอิงแล้ว
        /// </summary>
        public void _searchUnitNameWareHouseNameShelfNameAll()
        {
            // step 
            // 1. packcode
            // 2. query
            // 3. update
            int __wareHouseColmnNumber = this._findColumnByName(_g.d.ic_trans_detail._wh_code);

            List<string> __itemCodeList = new List<string>();
            List<string> __whCodeList = new List<string>();
            List<string> __shelfCodeList = new List<string>();
            List<string> __unitList = new List<string>();

            if (this._rowData.Count > 0)
            {
                // 1. pack code
                for (int row = 0; row < this._rowData.Count; row++)
                {
                    string __getItemCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                    string __getWHcode = (__wareHouseColmnNumber != -1) ? this._cellGet(row, _g.d.ic_trans_detail._wh_code).ToString() : "";
                    string __getShelfCode = (__wareHouseColmnNumber != -1) ? this._cellGet(row, _g.d.ic_trans_detail._shelf_code).ToString() : "";
                    string __getUnitcode = this._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString();

                    if (Array.IndexOf(__itemCodeList.ToArray(), "\'" + __getItemCode + "\'") == -1)
                    {
                        __itemCodeList.Add("\'" + __getItemCode + "\'");
                    }
                    if (__getWHcode.Length > 0)
                    {
                        if (Array.IndexOf(__whCodeList.ToArray(), "\'" + __getWHcode + "\'") == -1)
                        {
                            __whCodeList.Add("\'" + __getWHcode + "\'");
                        }
                    }

                    if (__getWHcode.Length > 0)
                    {
                        if (Array.IndexOf(__shelfCodeList.ToArray(), "\'" + __getShelfCode + "\'") == -1)
                        {
                            __shelfCodeList.Add("\'" + __getShelfCode + "\'");
                        }
                    }

                    if (Array.IndexOf(__unitList.ToArray(), "\'" + __getUnitcode + "\'") == -1)
                    {
                        __unitList.Add("\'" + __getUnitcode + "\'");
                    }

                    if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                    {
                        string __wareHouseCode2 = this._cellGet(row, _g.d.ic_trans_detail._wh_code_2).ToString().ToUpper();
                        string __shelfCode2 = this._cellGet(row, _g.d.ic_trans_detail._shelf_code_2).ToString().ToUpper();

                        if (Array.IndexOf(__whCodeList.ToArray(), __wareHouseCode2) == -1)
                        {
                            __whCodeList.Add("\'" + __wareHouseCode2 + "\'");
                        }

                        if (Array.IndexOf(__shelfCodeList.ToArray(), __shelfCode2) == -1)
                        {
                            __shelfCodeList.Add("\'" + __shelfCode2 + "\'");
                        }
                    }
                }

                // 2. query
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + "," + _g.d.ic_unit._code + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " in (" + string.Join(",", __unitList.ToArray()) + ") "));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + "," + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._code + " from " + _g.d.ic_unit_use._table +
                    " where " + _g.d.ic_unit_use._ic_code + " in (" + string.Join(",", __itemCodeList.ToArray()) + ") and " + _g.d.ic_unit_use._code + " in (" + string.Join(",", __unitList.ToArray()) + ") "));
                // cost type 
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._code + ", " + _g.d.ic_inventory._cost_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " in (" + string.Join(",", __itemCodeList.ToArray()) + ") "));

                if (__whCodeList.Count > 0)
                {
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_warehouse._name_1 + "," + _g.d.ic_warehouse._code + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " in (" + string.Join(",", __whCodeList.ToArray()) + ") "));

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_shelf._name_1 + "," + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._whcode + " in (" + string.Join(",", __whCodeList.ToArray()) + ")  and " + _g.d.ic_shelf._code + " in (" + string.Join(",", __shelfCodeList.ToArray()) + ") "));
                }

                __query.Append("</node>");
                String __queryStr = __query.ToString();
                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
                DataTable __t1 = ((DataSet)__dataResult[0]).Tables[0];
                DataTable __t4 = ((DataSet)__dataResult[1]).Tables[0];
                DataTable __t5 = ((DataSet)__dataResult[2]).Tables[0];

                DataTable __t2 = null;
                DataTable __t3 = null;


                if (__whCodeList.Count > 0)
                {
                    __t2 = ((DataSet)__dataResult[3]).Tables[0];
                    __t3 = ((DataSet)__dataResult[4]).Tables[0];
                }


                // 3. update
                for (int row = 0; row < this._rowData.Count; row++)
                {
                    // filter by row
                    string __getItemCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                    string __getUnitcode = this._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString();

                    string __getWHcode = "";
                    string __getShelfCode = "";

                    if (__whCodeList.Count > 0)
                    {

                        __getWHcode = this._cellGet(row, _g.d.ic_trans_detail._wh_code).ToString();
                        __getShelfCode = this._cellGet(row, _g.d.ic_trans_detail._shelf_code).ToString();
                    }

                    string __unitName = "";
                    string __wareHouseName = "";
                    string __shelfName = "";
                    decimal __standValue = 1;
                    decimal __divValue = 1;
                    int __costType = 0;
                    /*
                    if (__t1.Rows.Count > 0) __unitName = __t1.Rows[0][_g.d.ic_unit._name_1].ToString();
                    if (__t2.Rows.Count > 0) __wareHouseName = __t2.Rows[0][_g.d.ic_warehouse._name_1].ToString();
                    if (__t3.Rows.Count > 0) __shelfName = __t3.Rows[0][_g.d.ic_shelf._name_1].ToString();
                    if (__t4.Rows.Count > 0)
                    {
                        __standValue = MyLib._myGlobal._decimalPhase(__t4.Rows[0][_g.d.ic_unit_use._stand_value].ToString());
                        __divValue = MyLib._myGlobal._decimalPhase(__t4.Rows[0][_g.d.ic_unit_use._divide_value].ToString());
                    }*/

                    // stand vaue 

                    if (__t5.Rows.Count > 0)
                    {
                        DataRow[] __unitRatioRow = __t5.Select(_g.d.ic_inventory._code + "=\'" + __getItemCode + "\'");
                        if (__unitRatioRow.Length > 0)
                        {
                            __costType = MyLib._myGlobal._intPhase(__unitRatioRow[0][_g.d.ic_inventory._cost_type].ToString());
                        }

                    }

                    // unit
                    if (__t1.Rows.Count > 0)
                    {
                        DataRow[] __unitNameRow = __t1.Select(_g.d.ic_unit._code + "=\'" + __getUnitcode + "\'");
                        if (__unitNameRow.Length > 0)
                            __unitName = __unitNameRow[0][_g.d.ic_unit._name_1].ToString();
                    }

                    // wh_code
                    if (__t2 != null && __t2.Rows.Count > 0)
                    {
                        DataRow[] __whCodeRow = __t2.Select(_g.d.ic_warehouse._code + "=\'" + __getWHcode + "\'");
                        if (__whCodeRow.Length > 0)
                            __wareHouseName = __whCodeRow[0][_g.d.ic_warehouse._name_1].ToString();
                    }

                    // shelf_code
                    if (__t3 != null && __t3.Rows.Count > 0)
                    {
                        DataRow[] __shelfcodeRow = __t3.Select(_g.d.ic_shelf._code + "=\'" + __getShelfCode + "\' and " + _g.d.ic_shelf._whcode + "=\'" + __getWHcode + "\'");
                        if (__shelfcodeRow.Length > 0)
                            __shelfName = __shelfcodeRow[0][_g.d.ic_shelf._name_1].ToString();
                    }

                    // stand, divide
                    if (__t4.Rows.Count > 0)
                    {
                        DataRow[] __unitRatioRow = __t4.Select(_g.d.ic_unit_use._code + "=\'" + __getUnitcode + "\' and " + _g.d.ic_unit_use._ic_code + "=\'" + __getItemCode + "\'");
                        if (__unitRatioRow.Length > 0)
                        {
                            __standValue = MyLib._myGlobal._decimalPhase(__unitRatioRow[0][_g.d.ic_unit_use._stand_value].ToString());
                            __divValue = MyLib._myGlobal._decimalPhase(__unitRatioRow[0][_g.d.ic_unit_use._divide_value].ToString());
                        }
                    }

                    if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก || this._ictransControlTemp == _g.g._transControlTypeEnum.สินค้า_ขอโอน)
                    {
                        string __wareHouseCode2 = this._cellGet(row, _g.d.ic_trans_detail._wh_code_2).ToString().ToUpper();
                        string __shelfCode2 = this._cellGet(row, _g.d.ic_trans_detail._shelf_code_2).ToString().ToUpper();

                        string __wareHouseName2 = "";
                        string __shelfName2 = "";

                        if (__t2.Rows.Count > 0)
                        {
                            DataRow[] __whCodeRow = __t2.Select(_g.d.ic_warehouse._code + "=\'" + __wareHouseCode2 + "\'");
                            if (__whCodeRow.Length > 0)
                                __wareHouseName2 = __whCodeRow[0][_g.d.ic_warehouse._name_1].ToString();
                        }

                        if (__t3.Rows.Count > 0)
                        {
                            DataRow[] __shelfcodeRow = __t3.Select(_g.d.ic_shelf._code + "=\'" + __shelfCode2 + "\' and " + _g.d.ic_shelf._whcode + "=\'" + __wareHouseCode2 + "\'");
                            if (__shelfcodeRow.Length > 0)
                                __shelfName2 = __shelfcodeRow[0][_g.d.ic_shelf._name_1].ToString();
                        }

                        this._cellUpdate(row, _g.d.ic_trans_detail._wh_name_2, __wareHouseName2, false);
                        this._cellUpdate(row, _g.d.ic_trans_detail._shelf_name_2, __shelfName2, false);
                    }
                    this._cellUpdate(row, _g.d.ic_trans_detail._unit_name, __unitName, false);
                    this._cellUpdate(row, this._columnCostType, __costType, false);
                    if (__wareHouseColmnNumber != -1)
                    {
                        this._cellUpdate(row, _g.d.ic_trans_detail._wh_name, __wareHouseName, false);
                        this._cellUpdate(row, _g.d.ic_trans_detail._shelf_name, __shelfName, false);
                    }
                    this._cellUpdate(row, _g.d.ic_trans_detail._stand_value, __standValue, false);
                    this._cellUpdate(row, _g.d.ic_trans_detail._divide_value, __divValue, false);
                }
            }
        }

        public void _searchUnitNameWareHouseNameShelfName(int row)
        {
            try
            {
                int __wareHouseColmnNumber = this._findColumnByName(_g.d.ic_trans_detail._wh_code);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __itemCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().ToUpper();
                string __unitCode = this._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString().ToUpper();
                string __wareHouseCode = (__wareHouseColmnNumber == -1) ? "" : this._cellGet(row, _g.d.ic_trans_detail._wh_code).ToString().ToUpper();
                string __shelfCode = (__wareHouseColmnNumber == -1) ? "" : this._cellGet(row, _g.d.ic_trans_detail._shelf_code).ToString().ToUpper();
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table +
                    " where " + _g.d.ic_unit._code + "=\'" + __unitCode + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table +
                    " where " + _g.d.ic_warehouse._code + "=\'" + __wareHouseCode + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table +
                    " where " + _g.d.ic_shelf._whcode + "=\'" + __wareHouseCode + "\' and " + _g.d.ic_shelf._code + "=\'" + __shelfCode + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table +
                    " where " + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' and " + _g.d.ic_unit_use._code + "=\'" + __unitCode + "\'"));
                if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก || this._ictransControlTemp == _g.g._transControlTypeEnum.สินค้า_ขอโอน)
                {
                    string __wareHouseCode2 = this._cellGet(row, _g.d.ic_trans_detail._wh_code_2).ToString().ToUpper();
                    string __shelfCode2 = this._cellGet(row, _g.d.ic_trans_detail._shelf_code_2).ToString().ToUpper();
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table +
                        " where " + _g.d.ic_warehouse._code + "=\'" + __wareHouseCode2 + "\'"));
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table +
                        " where " + _g.d.ic_shelf._whcode + "=\'" + __wareHouseCode2 + "\' and " + _g.d.ic_shelf._code + "=\'" + __shelfCode2 + "\'"));
                }
                __query.Append("</node>");
                String __queryStr = __query.ToString();
                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
                //
                string __unitName = "";
                string __wareHouseName = "";
                string __shelfName = "";
                decimal __standValue = 1;
                decimal __divValue = 1;
                //
                DataTable __t1 = ((DataSet)__dataResult[0]).Tables[0];
                DataTable __t2 = ((DataSet)__dataResult[1]).Tables[0];
                DataTable __t3 = ((DataSet)__dataResult[2]).Tables[0];
                DataTable __t4 = ((DataSet)__dataResult[3]).Tables[0];
                if (__t1.Rows.Count > 0) __unitName = __t1.Rows[0][_g.d.ic_unit._name_1].ToString();
                if (__t2.Rows.Count > 0) __wareHouseName = __t2.Rows[0][_g.d.ic_warehouse._name_1].ToString();
                if (__t3.Rows.Count > 0) __shelfName = __t3.Rows[0][_g.d.ic_shelf._name_1].ToString();
                if (__t4.Rows.Count > 0)
                {
                    __standValue = MyLib._myGlobal._decimalPhase(__t4.Rows[0][_g.d.ic_unit_use._stand_value].ToString());
                    __divValue = MyLib._myGlobal._decimalPhase(__t4.Rows[0][_g.d.ic_unit_use._divide_value].ToString());
                }
                if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก || this._ictransControlTemp == _g.g._transControlTypeEnum.สินค้า_ขอโอน)
                {
                    DataTable __t5 = ((DataSet)__dataResult[4]).Tables[0];
                    DataTable __t6 = ((DataSet)__dataResult[5]).Tables[0];
                    string __wareHouseName2 = "";
                    string __shelfName2 = "";
                    if (__t5.Rows.Count > 0) __wareHouseName2 = __t5.Rows[0][_g.d.ic_warehouse._name_1].ToString();
                    if (__t6.Rows.Count > 0) __shelfName2 = __t6.Rows[0][_g.d.ic_shelf._name_1].ToString();
                    this._cellUpdate(row, _g.d.ic_trans_detail._wh_name_2, __wareHouseName2, false);
                    this._cellUpdate(row, _g.d.ic_trans_detail._shelf_name_2, __shelfName2, false);
                }
                this._cellUpdate(row, _g.d.ic_trans_detail._unit_name, __unitName, false);
                if (__wareHouseColmnNumber != -1)
                {
                    this._cellUpdate(row, _g.d.ic_trans_detail._wh_name, __wareHouseName, false);
                    this._cellUpdate(row, _g.d.ic_trans_detail._shelf_name, __shelfName, false);
                }
                this._cellUpdate(row, _g.d.ic_trans_detail._stand_value, __standValue, false);
                this._cellUpdate(row, _g.d.ic_trans_detail._divide_value, __divValue, false);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _searchBarCodeDialog(int row)
        {
            this._searchBarcode = new MyLib._searchDataFull();
            this._searchBarcode.Text = MyLib._myGlobal._resource("ค้นหาสินค้าตาม Barcode");
            this._searchBarcode._dataList._loadViewFormat(_g.g._search_screen_ic_barcode, MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchBarcode.WindowState = FormWindowState.Maximized;
            this._searchBarcode._dataList._gridData._mouseClick += (s1, e1) =>
            {
                string __barCode = this._searchBarcode._dataList._gridData._cellGet(this._searchBarcode._dataList._gridData._selectRow, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
                this._searchBarcode.Close();

                if (this._rowData.Count == 0)
                {
                    this._addRow();
                }
                //
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __barCode, true);
                //this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._barcode, __barCode, false);
                SendKeys.Send("{TAB}");
            };
            this._searchBarcode._searchEnterKeyPress += (s1, e1) =>
            {
                string __barCode = this._searchBarcode._dataList._gridData._cellGet(this._searchBarcode._dataList._gridData._selectRow, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
                this._searchBarcode.Close();
                //
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __barCode, true);
                //this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._barcode, __barCode, false);
                SendKeys.Send("{TAB}");
            };
            this._searchBarcode.ShowDialog();
        }

        /// <summary>
        /// ค้นหาเช็ค
        /// </summary>
        /// <param name="extraWhere">เงื่อนไขพิเศษ</param>
        private void _startSearchChq(string extraWhere)
        {
            if (this._search_chq_form == null)
            {
                this._search_chq_form = new _search_chq_form(this._icTransControlType);
                this._search_chq_form._processButton.Click += new EventHandler(_searchChqFormProcessButton_Click);
            }

            string __chqNumberColumnName = _g.d.ic_trans_detail._chq_number;

            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                    __chqNumberColumnName = _g.d.ic_trans_detail._item_code;
                    break;
            }

            this._search_chq_form._process(extraWhere);
            // ลบรายการที่เลือกไปแล้ว
            for (int __row = 0; __row < this._rowData.Count; __row++)
            {
                string __docNo = this._cellGet(__row, __chqNumberColumnName).ToString();
                int __addr = this._search_chq_form._resultGrid._findData(this._search_chq_form._resultGrid._findColumnByName(_g.d.cb_chq_list._chq_number), __docNo);
                if (__addr != -1)
                {
                    this._search_chq_form._resultGrid._rowData.RemoveAt(__addr);
                }
            }
            this._search_chq_form.ShowDialog(MyLib._myGlobal._mainForm);
        }

        private void _searchItemDialog(string columnName, int row)
        {
            int __columnNumber = this._findColumnByName(columnName);
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                        {
                            /*SMLERPAPARControl._selectBillForm __selectBillForm = new SMLERPAPARControl._selectBillForm(this._icTransControlType);
                            __selectBillForm.ShowDialog();*/

                            this._searchMaster = new MyLib._searchDataFull();
                            string __screenName = "";
                            StringBuilder __extraWhere = new StringBuilder();

                            __screenName = _g.g._search_screen_sale;
                            __extraWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ));
                            __extraWhere.Append(" and " + _g.d.ic_trans._last_status + "= 0 ");
                            //this._searchMaster._dataList.first
                            this._searchMaster._name = __screenName;
                            //
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, true);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            if (__extraWhere.Length > 0)
                            {
                                this._searchMaster._dataList._extraWhere = __extraWhere.ToString();
                            }
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;

                            MyLib.ToolStripMyButton __buttonProcessSelected = new MyLib.ToolStripMyButton();
                            __buttonProcessSelected.DisplayStyle = ToolStripItemDisplayStyle.Image;
                            //SMLInventoryControl.Properties.Resources.flash
                            __buttonProcessSelected.Image = (Image)global::SMLInventoryControl.Properties.Resources.flash;
                            __buttonProcessSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
                            __buttonProcessSelected.Name = "__buttonProcessSelected";
                            __buttonProcessSelected.Padding = new System.Windows.Forms.Padding(1);
                            __buttonProcessSelected.ResourceName = "";
                            __buttonProcessSelected.Size = new System.Drawing.Size(30, 22);
                            __buttonProcessSelected.Text = "Process Selected";
                            __buttonProcessSelected.ToolTipText = "Process Selected";
                            __buttonProcessSelected.Click += (s1, e1) =>
                            {
                                bool __first = true;
                                for (int __row = 0; __row < this._searchMaster._dataList._gridData._rowData.Count; __row++)
                                {
                                    if (this._searchMaster._dataList._gridData._cellGet(__row, "check").ToString().Equals("1"))
                                    {
                                        MyLib._myGrid __grid = this._searchMaster._dataList._gridData;

                                        string __docNo = __grid._cellGet(__row, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();

                                        int __searchResult = this._findData(this._findColumnByName(_g.d.ic_trans_detail._item_code), __docNo);

                                        if (__searchResult == -1)
                                        {
                                            if (__first == false)
                                            {
                                                this._selectRow = this._addRow();
                                            }

                                            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __docNo, true);
                                            __first = false;
                                        }

                                        //DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__grid._cellGet(row, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date).ToString());
                                        //string __custCust = __grid._cellGet(row, _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code).ToString();
                                        //decimal __sumAmount = MyLib._myGlobal._decimalPhase(__grid._cellGet(row, _g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount).ToString());
                                        //this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._cust_code, __custCust, false);
                                        //this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._ref_doc_date, __docDate, true);
                                        //this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._sum_amount, __sumAmount, true);

                                    }
                                }
                                this._searchMaster.Close();
                                SendKeys.Send("{TAB}");

                            };
                            this._searchMaster._dataList._button.Items.Add(__buttonProcessSelected);

                            // add process button
                            //this._searchMaster._dataList._button.Items.Add(

                            this._searchMaster.ShowDialog();
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                    if (columnName.Equals(_g.d.ic_trans_detail._item_code) || columnName.Equals(_g.d.ic_trans_detail._chq_number))
                    {
                        StringBuilder __extraWhere = new StringBuilder();
                        // __extraWhere.Append(_g.d.cb_chq_list._chq_type + "=1");
                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0,3) ");
                        __extraWhere.Append(" and " + _g.d.cb_chq_list._ap_ar_code + "=\'" + this._custCode + "\' ");
                        _startSearchChq(__extraWhere.ToString());


                        /*this._searchMaster = new MyLib._searchDataFull();
                        string __screenName = "";
                        StringBuilder __extraWhere = new StringBuilder();

                        __screenName = _g.g._search_screen_cb_เช็ครับ;
                        __extraWhere.Append(_g.d.cb_chq_list._chq_type + "=1");
                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0,3) ");
                        __extraWhere.Append(" and " + _g.d.cb_chq_list._ap_ar_code + "=\'" + this._custCode + "\' ");

                        this._searchMaster._name = __screenName;
                        //
                        this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                        this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                        if (__extraWhere.Length > 0)
                        {
                            this._searchMaster._dataList._extraWhere = __extraWhere.ToString();
                        }
                        this._searchMaster._dataList._refreshData();
                        this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                        this._searchMaster.ShowDialog();*/

                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                    if (columnName.Equals(_g.d.ic_trans_detail._item_code) || columnName.Equals(_g.d.ic_trans_detail._chq_number))
                    {
                        StringBuilder __extraWhere = new StringBuilder();
                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0, 3) "); // 
                        __extraWhere.Append(" and " + _g.d.cb_chq_list._ap_ar_code + "=\'" + this._custCode + "\' ");

                        _startSearchChq(__extraWhere.ToString());
                        /*
                        this._searchMaster = new MyLib._searchDataFull();
                        string __screenName = "";
                        StringBuilder __extraWhere = new StringBuilder();

                        __screenName = _g.g._search_screen_cb_เช็คจ่าย;
                        __extraWhere.Append(_g.d.cb_chq_list._chq_type + "=2");
                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0, 3) "); // 
                        __extraWhere.Append(" and " + _g.d.cb_chq_list._ap_ar_code + "=\'" + this._custCode + "\' ");
                        this._searchMaster._name = __screenName;

                        this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                        this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                        if (__extraWhere.Length > 0)
                        {
                            this._searchMaster._dataList._extraWhere = __extraWhere.ToString();
                        }
                        this._searchMaster._dataList._refreshData();
                        this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                        this._searchMaster.ShowDialog();
                        */
                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา:
                    if (columnName.Equals(_g.d.ic_trans_detail._bank_name))
                    {
                        MyLib._searchDataFull __search = new MyLib._searchDataFull();
                        __search._dataList._loadViewFormat(_g.g._search_screen_bank, MyLib._myGlobal._userSearchScreenGroup, false);
                        __search._searchEnterKeyPress += (MyLib._myGrid sender, int rowSelect) =>
                        {
                            string __bankCode = sender._cellGet(rowSelect, _g.d.erp_bank._table + "." + _g.d.erp_bank._code).ToString().Trim();
                            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._bank_name, __bankCode, true);
                            __search.Dispose();
                            SendKeys.Send("{TAB}");
                        };
                        __search._dataList._gridData._mouseClick += (s1, e1) =>
                        {
                            MyLib._myGrid __grid = (MyLib._myGrid)s1;
                            string __bankCode = __grid._cellGet(e1._row, _g.d.erp_bank._table + "." + _g.d.erp_bank._code).ToString().Trim();
                            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._bank_name, __bankCode, true);
                            __search.Dispose();
                            SendKeys.Send("{TAB}");
                        };
                        __search.StartPosition = FormStartPosition.CenterScreen;
                        __search.ShowDialog();
                    }
                    if (columnName.Equals(_g.d.ic_trans_detail._bank_branch))
                    {
                        MyLib._searchDataFull __search = new MyLib._searchDataFull();
                        __search._dataList._loadViewFormat(_g.g._search_screen_bank_branch, MyLib._myGlobal._userSearchScreenGroup, false);
                        __search._dataList._extraWhere = _g.d.erp_bank_branch._bank_code + "=\'" + this._cellGet(this._selectRow, _g.d.ic_trans_detail._bank_name).ToString().Trim() + "\'";
                        __search._searchEnterKeyPress += (MyLib._myGrid sender, int rowSelect) =>
                        {
                            string __bankCode = sender._cellGet(rowSelect, _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code).ToString().Trim();
                            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._bank_branch, __bankCode, true);
                            __search.Dispose();
                            SendKeys.Send("{TAB}");
                        };
                        __search._dataList._gridData._mouseClick += (s1, e1) =>
                        {
                            MyLib._myGrid __grid = (MyLib._myGrid)s1;
                            string __bankCode = __grid._cellGet(e1._row, _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code).ToString().Trim();
                            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._bank_branch, __bankCode, true);
                            __search.Dispose();
                            SendKeys.Send("{TAB}");
                        };
                        __search.StartPosition = FormStartPosition.CenterScreen;
                        __search.ShowDialog();
                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._chq_number))
                        {
                            StringBuilder __extraWhere = new StringBuilder();
                            __extraWhere.Append(" and coalesce(" + _g.d.cb_chq_list._status + ",0)=0");

                            _startSearchChq(__extraWhere.ToString());
                        }
                        else if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                        {
                            StringBuilder __extraWhere = new StringBuilder();

                            this._searchMaster = new MyLib._searchDataFull();
                            string __screenName = "";
                            //if (columnName.Equals(_g.d.ic_trans_detail._chq_number))
                            //{
                            //    __screenName = _g.g._search_screen_บัตรเครดิต;
                            //    __extraWhere.Append(_g.d.cb_chq_list._chq_type + "=3 and coalesce(" + _g.d.cb_chq_list._status + ",0)=0");
                            //}
                            //else
                            //    if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                            {
                                __screenName = _g.g._search_screen_สมุดเงินฝาก;
                            }
                            this._searchMaster._name = __screenName;
                            //
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            if (__extraWhere.Length > 0)
                            {
                                this._searchMaster._dataList._extraWhere = __extraWhere.ToString();
                            }
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                            this._searchMaster.ShowDialog();
                        }
                    }
                    break;
                #region เช็คจ่าย
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                    if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                    {
                        this._searchMaster = new MyLib._searchDataFull();
                        string __screenName = _g.g._search_screen_สมุดเงินฝาก;
                        this._searchMaster._name = __screenName;
                        //
                        this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                        this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                        this._searchMaster._dataList._refreshData();
                        this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                        this._searchMaster.ShowDialog();
                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._chq_number))
                        {
                            StringBuilder __extraWhere = new StringBuilder();
                            // __extraWhere.Append(_g.d.cb_chq_list._chq_type + "=2");

                            string __pass_book = this._icTransScreenTop._getDataStr(_g.d.ic_trans._pass_book_code);
                            switch (this._icTransControlType)
                            {
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                                    {
                                        string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                        __extraWhere.Append(" and " + _g.d.cb_chq_list._pass_book_code + "=\'" + ((__bookCode.Length > 0) ? __bookCode : __pass_book) + "\'");
                                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0, 3) "); // พี่ระเอาเข็คคืน ไม่ขาดสิทธิ์ มาทำผ่านได้อีก
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                                    {
                                        string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                        __extraWhere.Append(" and " + _g.d.cb_chq_list._pass_book_code + "=\'" + ((__bookCode.Length > 0) ? __bookCode : __pass_book) + "\'");
                                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (2) "); // พี่ระ เอาเฉพาะ เช็คที่ผ่านแล้วเท่านั้น
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                                    {
                                        string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                        __extraWhere.Append(" and " + _g.d.cb_chq_list._pass_book_code + "=\'" + ((__bookCode.Length > 0) ? __bookCode : __pass_book) + "\'");
                                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0, 3) "); // พี่ระ เอาเฉพาะ เช็คที่ผ่านแล้วเท่านั้น

                                    }
                                    break;
                            }

                            _startSearchChq(__extraWhere.ToString());
                        }
                        else if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                        {
                            this._searchMaster = new MyLib._searchDataFull();
                            string __screenName = "";
                            StringBuilder __extraWhere = new StringBuilder();

                            /*if (columnName.Equals(_g.d.ic_trans_detail._chq_number))
                            {
                                __screenName = _g.g._search_screen_cb_เช็คจ่าย;
                                __extraWhere.Append(_g.d.cb_chq_list._chq_type + "=2");
                                switch (this._icTransControlType)
                                {
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                                        {
                                            string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                            __extraWhere.Append(" and " + _g.d.cb_chq_list._pass_book_code + "=\'" + __bookCode + "\'");
                                            __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0, 3) "); // พี่ระเอาเข็คคืน ไม่ขาดสิทธิ์ มาทำผ่านได้อีก
                                        }
                                        break;
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                                        {
                                            string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                            __extraWhere.Append(" and " + _g.d.cb_chq_list._pass_book_code + "=\'" + __bookCode + "\'");
                                            __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (2) "); // พี่ระ เอาเฉพาะ เช็คที่ผ่านแล้วเท่านั้น
                                        }
                                        break;
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                                        {
                                            string __bookCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                            __extraWhere.Append(" and " + _g.d.cb_chq_list._pass_book_code + "=\'" + __bookCode + "\'");
                                            __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0, 3) "); // พี่ระ เอาเฉพาะ เช็คที่ผ่านแล้วเท่านั้น

                                        }
                                        break;
                                }

                                //and " + _g.d.cb_chq_list._bank_code + " = \'" + __bookCode + "\'";
                            }
                            else*/
                            // if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                            {
                                __screenName = _g.g._search_screen_สมุดเงินฝาก;
                            }
                            this._searchMaster._name = __screenName;
                            //
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            if (__extraWhere.Length > 0)
                            {
                                this._searchMaster._dataList._extraWhere = __extraWhere.ToString();
                            }
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                            this._searchMaster.ShowDialog();
                        }
                    }
                    break;
                #endregion
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._chq_number))
                        {
                            StringBuilder __extraWhere = new StringBuilder();

                            switch (this._icTransControlType)
                            {
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                                    {
                                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + "=1");
                                        // toe เพิ่ม filter ตาม bankcode ตอนฝากด้วย
                                        string __bankCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                        if (__bankCode != "")
                                        {
                                            __extraWhere.Append(" and (select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " item_code from ic_trans_detail where ic_trans_detail.chq_number = cb_chq_list.chq_number and last_status = 0 and ic_trans_detail.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก).ToString() + " order by doc_date desc, doc_time desc " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + ")" + "=\'" + __bankCode + "\'");
                                        }
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                                    {
                                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + "=2");
                                        // toe เพิ่ม filter ตาม bankcode ตอนฝากด้วย
                                        string __bankCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                        if (__bankCode != "")
                                        {
                                            __extraWhere.Append(" and (select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " item_code from ic_trans_detail where ic_trans_detail.chq_number = cb_chq_list.chq_number and last_status = 0 and ic_trans_detail.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน).ToString() + " order by doc_date desc, doc_time desc " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + ")" + "=\'" + __bankCode + "\'");
                                        }
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                                    __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0,3) ");
                                    break;
                            }
                            _startSearchChq(__extraWhere.ToString());
                        }
                        else if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                        {
                            this._searchMaster = new MyLib._searchDataFull();
                            StringBuilder __extraWhere = new StringBuilder();
                            string __screenName = "";
                            // โต๋ ย้ายแยกไป ทำค้นหาใหม่

                            /*if (columnName.Equals(_g.d.ic_trans_detail._chq_number))
                            {
                                
                                __screenName = _g.g._search_screen_cb_เช็ครับ;
                                //this._searchMaster._dataList._extraWhere = _g.d.cb_chq_list._chq_type + "=1";
                                __extraWhere.Append(_g.d.cb_chq_list._chq_type + "=1");

                                switch (this._icTransControlType)
                                {
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                                        {
                                            __extraWhere.Append(" and " + _g.d.cb_chq_list._status + "=1");

                                            // toe เพิ่ม filter ตาม bankcode ตอนฝากด้วย
                                            string __bankCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                            if (__bankCode != "")
                                            {
                                                __extraWhere.Append(" and (select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " item_code from ic_trans_detail where ic_trans_detail.chq_number = cb_chq_list.chq_number and last_status = 0 and ic_trans_detail.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก).ToString() + " order by doc_date desc, doc_time desc " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + ")" + "=\'" + __bankCode + "\'");
                                            }
                                        }
                                        break;
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                                        {
                                            __extraWhere.Append(" and " + _g.d.cb_chq_list._status + "=2");
                                            // toe เพิ่ม filter ตาม bankcode ตอนฝากด้วย
                                            string __bankCode = this._cellGet(row, _g.d.ic_trans_detail._item_code).ToString();
                                            if (__bankCode != "")
                                            {
                                                __extraWhere.Append(" and (select " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[0] + " item_code from ic_trans_detail where ic_trans_detail.chq_number = cb_chq_list.chq_number and last_status = 0 and ic_trans_detail.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน).ToString() + " order by doc_date desc, doc_time desc " + MyLib._myGlobal._getTopAndLimitOneRecord(1)[1] + ")" + "=\'" + __bankCode + "\'");
                                            }
                                        }
                                        break;
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                                        __extraWhere.Append(" and " + _g.d.cb_chq_list._status + " in (0,3) ");
                                        break;

                                }
                            }
                            else
                                if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                                {
                                    __screenName = _g.g._search_screen_สมุดเงินฝาก;
                                }}*/
                            __screenName = _g.g._search_screen_สมุดเงินฝาก;

                            this._searchMaster._name = __screenName;
                            //
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            if (__extraWhere.Length > 0)
                            {
                                this._searchMaster._dataList._extraWhere = __extraWhere.ToString();
                            }
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                            this._searchMaster.ShowDialog();
                        }
                    }
                    break;
                /*{
                    if (columnName.Equals(_g.d.ic_trans_detail._item_code) || columnName.Equals(_g.d.ic_trans_detail._chq_number))
                    {
                        string __screenName = "";
                        string __extraWhere = "";
                        if (columnName.Equals(_g.d.ic_trans_detail._chq_number))
                        {
                            __screenName = _g.g._search_screen_cb_เช็ครับ;
                            __extraWhere = _g.d.cb_chq_list._chq_type + "=1 and " + _g.d.cb_chq_list._status + " in (0) ";
                        }
                        else
                            if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                            {
                                __screenName = _g.g._search_screen_สมุดเงินฝาก;
                            }
                        this._searchMaster = new MyLib._searchDataFull();
                        this._searchMaster._name = __screenName;
                        //
                        this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                        this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                        if (__extraWhere.Length > 0)
                        {
                            this._searchMaster._dataList._extraWhere = __extraWhere;
                        }
                        this._searchMaster._dataList._refreshData();
                        this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                        this._searchMaster.ShowDialog();
                    }
                }
                break;*/
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                        {
                            this._searchMaster = new MyLib._searchDataFull();
                            this._searchMaster._name = _g.g._search_screen_สมุดเงินฝาก;
                            //
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                            this._searchMaster.ShowDialog();
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._item_code) || columnName.Equals(_g.d.ic_trans_detail._item_code_2))
                        {
                            this._searchMaster = new MyLib._searchDataFull();
                            this._searchMaster._name = _g.g._search_screen_สมุดเงินฝาก;
                            //
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                            this._searchMaster.ShowDialog();
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                        {
                            this._searchMaster = new MyLib._searchDataFull();
                            this._searchMaster._name = _g.g._search_screen_cb_petty_cash;
                            //
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                            this._searchMaster.ShowDialog();
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                        {
                            // ค่าใช้จ่ายอื่นๆ
                            this._searchMaster = new MyLib._searchDataFull();
                            this._searchMaster._name = _g.g._search_screen_expenses_list;
                            //
                            string __extraWhere = "";
                            string __extraWhereMasterCode = "";
                            string __docRefNo = "";
                            int __docRefNoColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefNoColumnNumber != -1)
                            {
                                __docRefNo = this._cellGet(row, __docRefNoColumnNumber).ToString();
                                __extraWhereMasterCode = _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table + " where " +
    _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'" + " and " + _g.d.ic_trans_detail._trans_flag + "={0})";
                            }
                            string __dialogText = "";
                            switch (this._icTransControlType)
                            {
                                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = String.Format(__extraWhereMasterCode, _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น).ToString());
                                        __dialogText = "จากใบตั้งหนี้อื่นเลขที่";
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = String.Format(__extraWhereMasterCode, _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString());
                                        __dialogText = "จากใบตั้งหนี้อื่นเลขที่";
                                    }
                                    break;
                            }
                            //
                            this._searchMaster.Text = this._searchMaster.Text + " : (" + __dialogText + " : " + __docRefNo + ")";
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            this._searchMaster._dataList._extraWhere = __extraWhere;
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                            this._searchMaster.ShowDialog();
                        }
                        else if (columnName.Equals(_g.d.ic_trans_detail._branch_code))
                        {
                            this._searchMaster = new MyLib._searchDataFull();
                            this._searchMaster._name = _g.g._search_master_erp_branch_list;
                            this._searchMaster.Text = "ค้นหาสาขา";
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                            this._searchMaster.ShowDialog();

                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                        {
                            // ค่าใช้จ่ายอื่นๆ
                            this._searchMaster = new MyLib._searchDataFull();
                            this._searchMaster._name = _g.g._search_screen_income_list;
                            //
                            string __extraWhere = "";
                            string __extraWhereMasterCode = "";
                            string __docRefNo = "";
                            int __docRefNoColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefNoColumnNumber != -1)
                            {
                                __docRefNo = this._cellGet(row, __docRefNoColumnNumber).ToString();
                                __extraWhereMasterCode = _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table + " where " +
    _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'" + " and " + _g.d.ic_trans_detail._trans_flag + "={0})";
                            }
                            string __dialogText = "";
                            switch (this._icTransControlType)
                            {
                                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = String.Format(__extraWhereMasterCode, _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น).ToString());
                                        __dialogText = "จากใบตั้งหนี้อื่นเลขที่";
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = String.Format(__extraWhereMasterCode, _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString());
                                        __dialogText = "จากใบรายได้อื่นเลขที่";
                                    }
                                    break;
                            }
                            //
                            this._searchMaster.Text = this._searchMaster.Text + " : (" + __dialogText + " : " + __docRefNo + ")";
                            this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                            this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            this._searchMaster._dataList._extraWhere = __extraWhere;
                            this._searchMaster._dataList._refreshData();
                            this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                            this._searchMaster.ShowDialog();
                        }
                    }
                    break;
                default:
                    {
                        if (columnName.Equals(_g.d.ic_trans_detail._item_code))
                        {
                            // ค้นหารายการสินค้า

                            if (this._icTransControlType == _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก)
                            {
                                // check doc_ref
                                string __getDocRef = this._cellGet(row, _g.d.ic_trans_detail._ref_doc_no).ToString();
                                if (__getDocRef.Length == 0)
                                {
                                    MessageBox.Show("กรุณาเลือกเอกสารอ้างอิง");
                                    return;
                                }
                            }

                            /*this._searchItem = new MyLib._searchDataFull();
                            this._searchItem.Text = ((MyLib._myGrid._columnType)this._columnList[__columnNumber])._name;
                            this._searchItem._name = _g.g._search_screen_ic_inventory;*/
                            this._searchItem.Text = ((MyLib._myGrid._columnType)this._columnList[__columnNumber])._name;
                            _searchItem._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
                            //
                            string __extraWhere = "";
                            string __extraWhereProduct = "";
                            string __docRefNo = "";
                            int __docRefNoColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                            if (__docRefNoColumnNumber != -1)
                            {
                                __docRefNo = this._cellGet(row, __docRefNoColumnNumber).ToString();
                                __extraWhereProduct = _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table + " where " +
                                    _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'" + " and " + _g.d.ic_trans_detail._trans_flag + "={0})";
                            }
                            string __dialogText = "";
                            switch (this._icTransControlType)
                            {
                                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";

                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = String.Format(__extraWhereProduct, _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ).ToString());
                                        __dialogText = "สินค้าจากใบเสนอซื้อเลขที่";
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;

                                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_sale + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;

                                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = String.Format(__extraWhereProduct, _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString());
                                        __dialogText = "สินค้าจากใบขายเลขที่";
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                    if (__docRefNo.Length > 0)
                                    {
                                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && MyLib._myGlobal._intPhase(this._cellGet(row, _g.d.ic_trans_detail._doc_ref_type).ToString()) == 2)
                                        {
                                            __dialogText = "ค้นหาสินค้า";
                                        }
                                        else
                                        {
                                            // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                            __extraWhere = String.Format(__extraWhereProduct, _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString());
                                            __dialogText = "สินค้าจากใบขายเลขที่";
                                        }

                                    }
                                    break;
                                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = String.Format(__extraWhereProduct, _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString());
                                        __dialogText = "สินค้าจากใบเสนอราคาเลขที่";
                                    }
                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_sale + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        string __transFlag = "";
                                        string __transName = "";
                                        int __selectTransFlag = MyLib._myGlobal._intPhase(this._cellGet(this._selectRow, this._findColumnByName(_g.d.ic_trans_detail._doc_ref_type)).ToString());
                                        switch (__selectTransFlag)
                                        {
                                            case 0: // ไม่เลือก
                                                break;
                                            case 1:
                                                __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString();
                                                __transName = "ใบเสนอราคา";
                                                break;
                                            case 2:
                                                __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString();
                                                __transName = "ใบสั่งจอง/สั่งซื้อ";
                                                break;
                                            case 3:
                                                __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString();
                                                __transName = "ใบสั่งขาย";
                                                break;
                                        }
                                        __extraWhere = _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'";
                                        if (__transFlag.Length > 0)
                                        {
                                            __extraWhere += " and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag;
                                        }
                                        __extraWhere += ")";
                                        if (__transName.Length > 0)
                                        {
                                            __dialogText = "สินค้าจาก" + __transName;
                                        }
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_sale + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        string __transFlag = "";
                                        string __transName = "";
                                        int __selectTransFlag = MyLib._myGlobal._intPhase(this._cellGet(this._selectRow, this._findColumnByName(_g.d.ic_trans_detail._doc_ref_type)).ToString());
                                        switch (__selectTransFlag)
                                        {
                                            case 0: // ไม่เลือก
                                                break;
                                            case 1:
                                                __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString();
                                                __transName = "ใบเสนอราคา";
                                                break;
                                            case 2:
                                                __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString();
                                                __transName = "ใบสั่งจอง/สั่งซื้อ";
                                                break;
                                        }
                                        __extraWhere = _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'";
                                        if (__transFlag.Length > 0)
                                        {
                                            __extraWhere += " and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag;
                                        }
                                        __extraWhere += ")";
                                        if (__transName.Length > 0)
                                        {
                                            __dialogText = "สินค้าจาก" + __transName;
                                        }
                                    }
                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_sale + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + ")";
                                        __dialogText = "สินค้าจากซื้อเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString() + ")";
                                        __dialogText = "สินค้าจากซื้อเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString() + ")";
                                        __dialogText = "สินค้าจากรับเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + ")";
                                        __dialogText = "สินค้าจากซื้อเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString() + ")";
                                        __dialogText = "สินค้าจากซื้อเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                    __docRefNo = this._cellGet(row, this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no)).ToString();
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString() + ")";
                                        __dialogText = "สินค้าจากใบสั่งซื้อเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";

                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                                    __docRefNo = this._cellGet(row, this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no)).ToString();
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString() + ")";
                                        __dialogText = "สินค้าจากใบรับเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                    __docRefNo = this._cellGet(row, this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no)).ToString();
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString() + ")";
                                        __dialogText = "สินค้าจากใบสั่งซื้อเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }

                                    __extraWhere += ((__extraWhere.Length > 0) ? " and " : "") + " coalesce((select " + _g.d.ic_inventory_detail._is_hold_purchase + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "), 0)=0 ";
                                    break;
                                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                    __docRefNo = this._cellGet(row, this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no)).ToString();
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere += _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ).ToString() + ")";
                                        __dialogText = "สินค้าจากใบเบิกเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                                    __docRefNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_ref);
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString() + ")";
                                        __dialogText = "สินค้าจากใบสั่งซื้อเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString() + ")";
                                        __dialogText = "สินค้าจากใบสั่งซื้อ/สั่งจองเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere += _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table
                                            + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString() + ")";
                                        __dialogText = "สินค้าจากใบสั่งขายเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }
                                    break;
                                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                                    if (__docRefNo.Length > 0)
                                    {
                                        // ค้นหาสินค้า เฉพาะสินค้าที่มีในเอกสารนั้นๆ
                                        __extraWhere += _g.d.ic_inventory._item_type + "<>5 and " + _g.d.ic_inventory._code + " in (select " + _g.d.ic_wms_trans_detail._item_code + " from " + _g.d.ic_wms_trans_detail._table
                                            + " where " + _g.d.ic_wms_trans_detail._doc_no + "=\'" + __docRefNo.ToUpper() + "\'"
                                            + " and " + _g.d.ic_wms_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก).ToString() + ")";
                                        __dialogText = "สินค้าจากใบสั่งขายเลขที่";
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }
                                    break;
                                default:
                                    if (__docRefNo.Length > 0)
                                    {
                                        if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                                        {
                                            MessageBox.Show("case not found : " + this._icTransControlType.ToString());
                                        }
                                    }
                                    else
                                    {
                                        __extraWhere = _g.d.ic_inventory._item_type + "<>5";
                                    }
                                    break;
                            }
                            this._searchItem.Text = this._searchItem.Text + " : (" + __dialogText + " : " + __docRefNo + ")";
                            if (this._searchItem._dataList._extraWhere.Equals(__extraWhere) == false)
                            {
                                this._searchItemRenew();
                                this._searchItem._dataList._extraWhere = __extraWhere;
                                this._searchItem._dataList._refreshData();
                            }
                            this._searchItem.WindowState = FormWindowState.Maximized;
                            SendKeys.Send("{TAB}");
                            this._searchItem._dataList.Invalidate();
                            this._searchItem.ShowDialog();
                        }
                    }
                    break;
            }
        }

        void _searchChqFormProcessButton_Click(object sender, EventArgs e)
        {
            this._search_chq_form.Close();

            string __chqNumberColumnName = _g.d.ic_trans_detail._chq_number;

            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:

                    __chqNumberColumnName = _g.d.ic_trans_detail._item_code;
                    break;
            }

            string __pass_book_code = this._icTransScreenTop._getDataStr(_g.d.ic_trans._pass_book_code);

            // เพิ่มบรรทัดใหม่
            for (int __row = 0; __row < this._search_chq_form._resultGrid._rowData.Count; __row++)
            {
                // ลบบรรทัดที่ว่าง
                int __rowDelete = 0;
                while (__rowDelete < this._rowData.Count)
                {
                    if (this._cellGet(__rowDelete, __chqNumberColumnName).ToString().Trim().Length == 0)
                    {
                        this._rowData.RemoveAt(__rowDelete);
                    }
                    else
                    {
                        __rowDelete++;
                    }
                }

                if ((int)this._search_chq_form._resultGrid._cellGet(__row, _g.d.ap_ar_resource._select) == 1)
                {
                    int __rowAddr = this._addRow();
                    // this._cellUpdate(__rowAddr, _g.d.ic_trans_detail._chq_number, this._search_chq_form._resultGrid._cellGet(__row, _g.d.cb_chq_list._chq_number).ToString(), true);

                    string __code = this._search_chq_form._resultGrid._cellGet(__row, _g.d.cb_chq_list._chq_number).ToString();
                    string __doc_ref = this._search_chq_form._resultGrid._cellGet(__row, _g.d.cb_chq_list._doc_ref).ToString();
                    int __doc_line_Number = MyLib._myGlobal._intPhase(this._search_chq_form._resultGrid._cellGet(__row, _g.d.cb_chq_list._doc_line_number).ToString());


                    if (__pass_book_code.Length > 0)
                    {
                        this._cellUpdate(__rowAddr, _g.d.ic_trans_detail._item_code, __pass_book_code, false);
                    }
                    this._cellUpdate(__rowAddr, _g.d.ic_trans_detail._doc_ref, __doc_ref, false);
                    this._cellUpdate(__rowAddr, _g.d.ic_trans_detail._ref_row, __doc_line_Number, false);
                    this._cellUpdate(__rowAddr, __chqNumberColumnName, __code, true);


                    // อนาคต ให้เอา event after update ออก แล้ว search รอบเดียวพอ

                }
            }
            this._gotoCell(this._rowData.Count, this._findColumnByName(_g.d.ap_ar_trans_detail._billing_no));
        }

        private void _searchItem__selected(string itemCode, string wareHouse, string location, int unitType)
        {
            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, itemCode, true);
            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._wh_code, wareHouse, true);
            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._shelf_code, location, true);
            SendKeys.Send("{TAB}");
            if (unitType == 1)
            {
                SendKeys.Send("{F5}");
            }
        }

        private MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            int __itemTypeColumnNumber = sender._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._item_type);
            if (__itemTypeColumnNumber != -1)
            {
                if (MyLib._myGlobal._intPhase(sender._cellGet(row, __itemTypeColumnNumber).ToString()) == 5)
                {
                    senderRow.newColor = Color.Blue;
                }
            }
            return senderRow;
        }

        private void _ictransItemGridControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.Equals(_g.d.ic_trans_detail._ref_doc_no))
            {
                // ค้นหาเอกสาร
                int __columnNumber = this._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
                int __selectTransFlag = MyLib._myGlobal._intPhase(this._cellGet(this._selectRow, this._findColumnByName(_g.d.ic_trans_detail._doc_ref_type)).ToString());
                this._icTransRef._displayIcTransSearch(((MyLib._myGrid._columnType)this._columnList[__columnNumber])._name, this._custCode, sender, true, __selectTransFlag, this._vatTypeNumber());
            }
            else
                _searchItemDialog(e._columnName, e._row);
        }

        private string _dataList__columnFieldNameReplace(string source)
        {
            return source.Replace("sml_value_branch_code", MyLib._myGlobal._branchCode);
        }

        private void _gridDataItemCode(MyLib._myGrid sender, int row)
        {
            // รหัสสินค้า
            this._searchItem.Close();
            string __itemCode = sender._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __itemCode, true);
            SendKeys.Send("{TAB}");
        }

        private void _gridDataExpenses(MyLib._myGrid sender, int row)
        {
            if (this._searchMaster._name.Equals(_g.g._search_master_erp_branch_list))
            {
                this._searchMaster.Close();
                string __code = sender._cellGet(row, _g.d.erp_branch_list._table + "." + _g.d.erp_branch_list._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._branch_code, __code, true);
            }
            else
            {
                // รหัสค่าใช้จ่าย
                this._searchMaster.Close();
                string __code = sender._cellGet(row, _g.d.erp_expenses_list._table + "." + _g.d.erp_expenses_list._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __code, true);
            }
            SendKeys.Send("{TAB}");
        }

        private void _gridDataPettyCash(MyLib._myGrid sender, int row)
        {
            // รหัสวงเงินสดย่อย
            this._searchMaster.Close();
            string __code = sender._cellGet(row, _g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._code).ToString();
            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __code, true);
            SendKeys.Send("{TAB}");
        }

        private void _gridDataBookBank(MyLib._myGrid sender, int row)
        {
            // สมุดเงินฝาก
            if (this._selectColumn == this._findColumnByName(_g.d.ic_trans_detail._item_code_2))
            {
                this._searchMaster.Close();
                string __code = sender._cellGet(row, _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code_2, __code, true);
                SendKeys.Send("{TAB}");
            }
            else
            {
                this._searchMaster.Close();
                string __code = sender._cellGet(row, _g.d.erp_pass_book._table + "." + _g.d.erp_pass_book._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __code, true);
                SendKeys.Send("{TAB}");
            }
        }

        private void _gridDataChq(MyLib._myGrid sender, int row)
        {
            _gridDataChq(sender, row, _g.d.ic_trans_detail._chq_number);
        }

        private void _gridDataChq(MyLib._myGrid sender, int row, string columnChqNumber)
        {
            // เช็ค
            this._searchMaster.Close();
            string __code = sender._cellGet(row, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number).ToString();
            string __doc_ref = sender._cellGet(row, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_ref).ToString();
            int __doc_line_Number = MyLib._myGlobal._intPhase(sender._cellGet(row, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_line_number).ToString());
            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._doc_ref, __doc_ref, false);
            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._ref_row, __doc_line_Number, false);
            this._cellUpdate(this._selectRow, columnChqNumber, __code, true);
            SendKeys.Send("{TAB}");
        }

        private void _gridDataIncome(MyLib._myGrid sender, int row)
        {
            // รหัสรายได้
            this._searchMaster.Close();
            string __code = sender._cellGet(row, _g.d.erp_income_list._table + "." + _g.d.erp_income_list._code).ToString();
            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __code, true);
            SendKeys.Send("{TAB}");
        }

        private void _gridDataShiping(MyLib._myGrid sender, int row)
        {
            this._searchMaster.Close();
            MyLib._myGrid __grid = (MyLib._myGrid)sender;

            string __docNo = __grid._cellGet(row, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();
            //DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__grid._cellGet(row, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date).ToString());
            //string __custCust = __grid._cellGet(row, _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code).ToString();
            //decimal __sumAmount = MyLib._myGlobal._decimalPhase(__grid._cellGet(row, _g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount).ToString());
            this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __docNo, true);
            //this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._cust_code, __custCust, false);
            //this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._ref_doc_date, __docDate, true);
            //this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._sum_amount, __sumAmount, true);
            SendKeys.Send("{TAB}");
        }

        private void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row != -1)
            {
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง:
                        {

                            this._gridDataShiping((MyLib._myGrid)sender, e._row);
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา:
                        if (this._searchMaster._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
                        {
                            this._gridDataBookBank((MyLib._myGrid)sender, e._row);
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
                            {
                                this._gridDataBookBank((MyLib._myGrid)sender, e._row);
                            }
                            else
                                if (this._searchMaster._name.Equals(_g.g._search_screen_บัตรเครดิต))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, e._row);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็คจ่าย))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, e._row, _g.d.ic_trans_detail._item_code);

                                /*
                                this._searchMaster.Close();
                                MyLib._myGrid __grid = (MyLib._myGrid)sender;

                                string __code = __grid._cellGet(e._row, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number).ToString();
                                string __doc_ref = __grid._cellGet(e._row, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_ref).ToString();
                                int __doc_line_Number = MyLib._myGlobal._intPhase(__grid._cellGet(e._row, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_line_number).ToString());
                                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._doc_ref, __doc_ref, false);
                                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._ref_row, __doc_line_Number, false);
                                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __code, true);
                                SendKeys.Send("{TAB}");*/
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
                            {
                                this._gridDataBookBank((MyLib._myGrid)sender, e._row);
                            }
                            else
                                if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็คจ่าย))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, e._row);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
                            {
                                this._gridDataBookBank((MyLib._myGrid)sender, e._row);
                            }
                            else
                                if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็ครับ))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, e._row);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็ครับ))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, e._row, _g.d.ic_trans_detail._item_code);
                                /*
                                this._searchMaster.Close();
                                MyLib._myGrid __grid = (MyLib._myGrid)sender;

                                string __code = __grid._cellGet(e._row, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number).ToString();
                                string __doc_ref = __grid._cellGet(e._row, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_ref).ToString();
                                int __doc_line_Number = MyLib._myGlobal._intPhase(__grid._cellGet(e._row, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_line_number).ToString());
                                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._doc_ref, __doc_ref, false);
                                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._ref_row, __doc_line_Number, false);
                                this._cellUpdate(this._selectRow, _g.d.ic_trans_detail._item_code, __code, true);
                                SendKeys.Send("{TAB}");*/
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
                            {
                                this._gridDataBookBank((MyLib._myGrid)sender, e._row);
                            }
                            else
                                if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็ครับ))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, e._row);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร:
                        {

                            this._gridDataBookBank((MyLib._myGrid)sender, e._row);
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                        this._gridDataPettyCash((MyLib._myGrid)sender, e._row);
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        // รหัสค่าใช้จ่าย
                        this._gridDataExpenses((MyLib._myGrid)sender, e._row);
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        // รหัสรายได้
                        this._gridDataIncome((MyLib._myGrid)sender, e._row);
                        break;
                    default:
                        // รหัสสินค้า
                        this._gridDataItemCode((MyLib._myGrid)sender, e._row);
                        break;
                }
            }
        }

        private void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            if (row != -1)
            {
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
                            {
                                this._gridDataBookBank((MyLib._myGrid)sender, row);
                            }
                            else
                                if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็คจ่าย))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, row);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
                            {
                                this._gridDataBookBank((MyLib._myGrid)sender, row);
                            }
                            else
                                if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็ครับ))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, row);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็ครับ))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, row, _g.d.ic_trans_detail._item_code);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็คจ่าย))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, row, _g.d.ic_trans_detail._item_code);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                        {
                            if (this._searchMaster._name.Equals(_g.g._search_screen_สมุดเงินฝาก))
                            {
                                this._gridDataBookBank((MyLib._myGrid)sender, row);
                            }
                            else
                                if (this._searchMaster._name.Equals(_g.g._search_screen_cb_เช็ครับ))
                            {
                                this._gridDataChq((MyLib._myGrid)sender, row);
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                        this._gridDataBookBank((MyLib._myGrid)sender, row);
                        break;
                    case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                        this._gridDataPettyCash((MyLib._myGrid)sender, row);
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                        // รหัสค่าใช้จ่าย
                        this._gridDataExpenses((MyLib._myGrid)sender, row);
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                        // รหัสรายได้
                        this._gridDataIncome((MyLib._myGrid)sender, row);
                        break;
                    default:
                        // รหัสสินค้า
                        this._gridDataItemCode((MyLib._myGrid)sender, row);
                        break;
                }
            }
        }

        /*void _gridDataMaster__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            try
            {
                MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
                MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
                string __name = getParent2._name;
                string __fieldName = _searchItem.Text;
                if (__name.CompareTo(_g.g._search_master_ic_unit_use) == 0)
                {
                    string __result = (string)_searchItem._dataList._gridData._cellGet(e._row, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code);
                    if (__result.Length > 0)
                    {
                        _searchItem.Close();
                        this._cellUpdate(this._selectRow, __fieldName, __result, false);
                    }
                }
                else if (__name.CompareTo(_g.g._search_master_ic_warehouse) == 0)
                {
                    string __result = (string)_searchItem._dataList._gridData._cellGet(e._row, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code);
                    if (__result.Length > 0)
                    {
                        _searchItem.Close();
                        this._cellUpdate(this._selectRow, __fieldName, __result, false);
                    }
                }
                else if (__name.CompareTo(_g.g._search_master_ic_shelf) == 0)
                {
                    string __result = (string)_searchItem._dataList._gridData._cellGet(e._row, _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code);
                    if (__result.Length > 0)
                    {
                        _searchItem.Close();
                        this._cellUpdate(this._selectRow, __fieldName, __result, false);
                    }
                }

            }
            catch
            {
            }
        }*/

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // _inputComboBox
            // 
            this._inputComboBox.Size = new System.Drawing.Size(121, 22);
            // 
            // _icTransItemGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "_icTransItemGridControl";
            this.Size = new System.Drawing.Size(792, 445);
            this.ResumeLayout(false);

        }

        public class _priceStruct
        {
            /*
             * __result._mode = -1;
            __result._price = 0;
            __result._type = -1;
            __result._roworder = -1;
            __result._user_approve = "";
            __result._foundByCondition = false;
            __result._foundPrice = false;
            __result._price1 = 0M;
            __result._price2 = 0M;
            */
            public decimal _price = 0M;
            /// <summary>
            /// -1=Null,0=ราคาทั่วไป,1=ราคายืน (มาตรฐาน),5=ราคาตาม Barcode,6=ราคาตามสูตร,7=ราคาสมาชิก
            /// </summary>
            public int _mode = -1;
            /// <summary>
            /// -1=Null,1=ราคาทั่วไป,2=ราคาตามกลุ่มลูกค้า,3=ราคาตามกลุ่มสินค้า,5=ราคาตาม Barcode,6=ราคาตามสูตร,7=ราคาสมาชิก
            /// </summary>
            public int _type = -1;
            /// <summary>
            /// roworder
            /// </summary>
            public int _roworder;
            /// <summary>
            /// รหัสผู้อนุมัติ
            /// </summary>
            public string _user_approve = "";
            /// <summary>
            /// พบราคาตามเงื่อนไข
            /// </summary>
            public bool _foundByCondition = false;
            /// <summary>
            /// พบราคา
            /// </summary>
            public bool _foundPrice = false;
            /// <summary>
            /// ราคาภาษีแยกนอก
            /// </summary>
            public decimal _price1 = 0M;
            /// <summary>
            /// ราคาภาษีรวมใน
            /// </summary>
            public decimal _price2 = 0M;
            /// <summary>ข้อความส่วนลด สำหรับ POS</summary>
            public string _discountWord = "";
            /// <summary>ลำดับส่วนลด</summary>
            public int _discountNumber = 0;
            /// <summary>รายละเอียดราคา</summary>
            public string _price_info = "";
            /// <summary>
            /// ส่วนลดตามสูตรราคา
            /// </summary>
            public string _discountWordFormula = "";
            /// <summary>
            /// ราคากลาง
            /// </summary>
            public decimal _stand_price = 0M;
            /// <summary>
            /// ส่วนลดที่กำหนดไว้
            /// </summary>
            public string _defaultDiscount = "";
            /// <summary>
            /// Price GUID
            /// </summary>

            public string _price_guid = "";
        }

        public class _serialNumberStruct
        {
            public List<_serialNumberDetailStruct> __details = new List<_serialNumberDetailStruct>();
        }

        public class _serialNumberDetailStruct
        {
            public string _serialNumber = "";
            public DateTime _voidDate = new DateTime();
            public string _description = "";
            public decimal _price = 0M;
        }

        public class _itemListStruct
        {
            public string _itemCode = "";
            public string _whCode = "";
            public string _locationCode = "";
            public decimal _qty = 0M;
            public string _lotNumber = "";
            public decimal _referReservQty = 0M;
        }

        public class icTransWeightStruct
        {
            public List<icTransWeightDetailStruct> __details = new List<icTransWeightDetailStruct>();

            public decimal _getTotal
            {
                get
                {
                    decimal __result = 0;
                    for (int __row = 0; __row < __details.Count; __row++)
                    {
                        __result += __details[__row]._amount;
                    }
                    return __result;
                }
            }
        }

        public class icTransWeightDetailStruct
        {
            public string _code = "";
            public string _name = "";
            public decimal _ratio = 0M;
            public decimal _amount = 0M;
        }
    }
}
