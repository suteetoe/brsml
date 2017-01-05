using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;

namespace SMLERPReport.po
{
    class _screen_condition_po : MyLib._myScreen
    {
        private _screenConditionPoType _screenTypeResult = _screenConditionPoType.None;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchDataFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;

        public _screen_condition_po()
        {
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screen_condition_ar__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screen_condition_ar__textBoxChanged);
        }

        public _screenConditionPoType _screenType
        {
            get { return this._screenTypeResult; }
            set
            {
                this._screenTypeResult = value;
                this.SuspendLayout();
                this.Controls.Clear();
                this._createScreen();
                this.ResumeLayout(false);
                this.PerformLayout();
            }
        }

        private void _createScreen()
        {
            this.AutoSize = true;
            if (this._screenType != _screenConditionPoType.None)
            {
                if (this._screenType == _screenConditionPoType.PoAddGoods)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากสินค้า", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงสินค้า", 1, true, false);
                    this._addDateBox(2, 0, 1, 0, "จากจำนวน", 1, true, false);
                    this._addDateBox(2, 1, 1, 0, "ถึงจำนวน", 1, true, false);
                    this._addDateBox(3, 0, 1, 0, "จากจำนวนเงิน", 1, true, false);
                    this._addDateBox(3, 1, 1, 0, "ถึงจำนวนเงิน", 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoAddGoodsSum)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากสินค้า", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงสินค้า", 1, true, false);
                    this._addDateBox(2, 0, 1, 0, "จากจำนวน", 1, true, false);
                    this._addDateBox(2, 1, 1, 0, "ถึงจำนวน", 1, true, false);
                    this._addDateBox(3, 0, 1, 0, "จากจำนวนเงิน", 1, true, false);
                    this._addDateBox(3, 1, 1, 0, "ถึงจำนวนเงิน", 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoComparePurchaseMonthly)
                {
                    this._addNumberBox(0, 0, 1, 0, "ระบุปี", 1, 0, true);
                    this._addTextBox(1, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากกลุ่ม", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงกลุ่ม", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากสินค้า", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงสินค้า", 1, 20, 1, true, false);
                    string[] __stringCombo = { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
                    this._addComboBox(5, 0, "จากเดือน", true, __stringCombo, true);
                    this._addComboBox(5, 0, "ถึงเดือน", true, __stringCombo, true);
                }
                else if (this._screenType == _screenConditionPoType.PoCutDepositPayment)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเลขที่ใบซื้อ", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเลขที่ใบซื้อ", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoCutPurchaseOrder)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่ใบสั่งซื้อ", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่ใบสั่งซื้อ", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoCutReceipt)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                    string[] __stringCombo = { "รายการทั้งหมด", "ยอดคงค้าง", "เฉพาะที่ตัดหมดแล้ว" };
                    this._addComboBox(5, 0, "แสดงเฉพาะ", true, __stringCombo, true);
                }
                else if (this._screenType == _screenConditionPoType.PoDebtFromPurchase)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่ใบตั้งหนี้", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่ใบตั้งหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoDepositPaymentRemain)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                    //string[] __stringCombo = { "ทั้งหมด", "เฉพาะที่ผ่านการอนุมัติ", "เฉพาะที่ไม่ผ่านการอนุมัติ" };
                    //this._addComboBox(5, 0, "แสดงเฉพาะ", true, __stringCombo, true);
                }
                else if (this._screenType == _screenConditionPoType.PoPurchaseAnalyze)
                {
                    this._addTextBox(0, 0, 1, 0, "จากสินค้า", 1, 20, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, "ถึงสินค้า", 1, 20, 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากจำนวนเงิน", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงจำนวนเงิน", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoPurchaseCost)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoPurchaseExplainByTax)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addDateBox(3, 0, 1, 0, "จากสินค้า", 1, true, false);
                    this._addDateBox(3, 1, 1, 0, "ถึงสินค้า", 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoPurchaseOrderDuePayment)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่ครบกำหนด", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่ครบกำหนด", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากสินค้า", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงสินค้า", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoPurchaseOrderExplain)
                {
                    this._addDateBox(0, 0, 1, 0, "จากสินค้า", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงสินค้า", 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoPurchaseOrderSum)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากสินค้า", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงสินค้า", 1, true, false);
                    this._addDateBox(2, 0, 1, 0, "จากจำนวน", 1, true, false);
                    this._addDateBox(2, 1, 1, 0, "ถึงจำนวน", 1, true, false);
                    this._addDateBox(3, 0, 1, 0, "จากจำนวนเงิน", 1, true, false);
                    this._addDateBox(3, 1, 1, 0, "ถึงจำนวนเงิน", 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoPurchaseSumByTax)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addDateBox(3, 0, 1, 0, "จากสินค้า", 1, true, false);
                    this._addDateBox(3, 1, 1, 0, "ถึงสินค้า", 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoPurchaseTotal)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoRankPurchaseTotal)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากสินค้า", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงสินค้า", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากกลุ่ม", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงกลุ่ม", 1, 20, 1, true, false);
                    string[] __stringCombo = new string[] { "เรียงตามปริมาณ", "เรียงตามมูลค่า" };
                    this._addComboBox(3, 0, "พิมพ์เรียงตาม", true, __stringCombo, true);
                    __stringCombo = new string[] { "เรียงจากน้อยไปมาก", "เรียงจากมากไปน้อย" };
                    this._addComboBox(5, 0, "พิมพ์เรียงจาก", true, __stringCombo, true);
                    this._addCheckBox(4, 0, "แสดงยอดหักมูลค่าลดหนี้", false, true);
                    this._addCheckBox(5, 0, "แสดงยอดรวมมูลค่าใบเพิ่มหนี้", false, true);
                    this._addCheckBox(6, 0, "แสดงเอกสารที่ยกเลิก", false, true);
                }
                else if (this._screenType == _screenConditionPoType.PoReceiptExplain)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากกลุ่ม", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงกลุ่ม", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากสินค้า", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงสินค้า", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoRecordDepositPayment)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงเจ้าหนี้", 1, 20, 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoRecordRequisitionPurchaseAndDetail)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false, false);
                    string[] __stringCombo = { "ทั้งหมด", "เฉพาะที่ผ่านการอนุมัติ", "เฉพาะที่ไม่ผ่านการอนุมัติ" };
                    this._addComboBox(2, 0, "แสดงเฉพาะ", true, __stringCombo, true);
                }
                else if (this._screenType == _screenConditionPoType.PoRequisitionPurchase)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่ใบเสนอซื้อ", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่ใบเสนอซื้อ", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากสินค้า", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงสินค้า", 1, 20, 1, true, false);
                    string[] __stringCombo = new string[] { "อนุมัติไปแล้ว", "ไม่อนุมัติ", "ทั้งหมด" };
                    this._addComboBox(3, 0, "สถานะเอกสาร", true, __stringCombo, true);
                    __stringCombo = new string[] { "ยอดคงค้าง", "อ้างอิงไปบางส่วน", "ทั้งหมด" };
                    this._addComboBox(4, 0, "สถานะของบิล", true, __stringCombo, true);
                }
                else if (this._screenType == _screenConditionPoType.PoReturnExplain)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากสินค้า", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงสินค้า", 1, true, false);
                    this._addDateBox(2, 0, 1, 0, "จากจำนวน", 1, true, false);
                    this._addDateBox(2, 1, 1, 0, "ถึงจำนวน", 1, true, false);
                    this._addDateBox(3, 0, 1, 0, "จากจำนวนเงิน", 1, true, false);
                    this._addDateBox(3, 1, 1, 0, "ถึงจำนวนเงิน", 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoReturnSum)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากสินค้า", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงสินค้า", 1, true, false);
                    this._addDateBox(2, 0, 1, 0, "จากจำนวน", 1, true, false);
                    this._addDateBox(2, 1, 1, 0, "ถึงจำนวน", 1, true, false);
                    this._addDateBox(3, 0, 1, 0, "จากจำนวนเงิน", 1, true, false);
                    this._addDateBox(3, 1, 1, 0, "ถึงจำนวนเงิน", 1, true, false);
                }
                else if (this._screenType == _screenConditionPoType.PoStatusPurchaseOrder)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากเจ้าหนี้", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงหนี้", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากสินค้า", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงสินค้า", 1, 20, 1, true, false);
                    string[] __stringCombo = { "จากรายการทั้งหมด", "เฉพาะที่ทำซื้อหมดแล้ว", "เฉพาะรายการที่ค้างส่ง" };
                    this._addComboBox(5, 0, "แสดงเฉพาะ", true, __stringCombo, true);
                }
                this._refresh();
                this._focusFirst();
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            this._screen_condition_ar__textBoxSearch(_getControl);
            //_getControl.textBox.Focus();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._searchDataFull.Visible = false;
        }

        void _screen_condition_ar__textBoxSearch(object sender)
        {
            //ค้นหารหัสลูกหนี้
            // ค้นหาหน้าจอ Top
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;

            //if (this._searchName.Equals(_g.d.ar_customer._code.ToLower()))
            //{
            //    this._setDataStr(this._searchName, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code).ToString(), "", true);
            //}

            string __searchTextNew = this._search_screen_neme(this._searchName);
            if (!this._searchDataFull._name.Equals(__searchTextNew.ToLower()))
            {
                this._searchDataFull = new MyLib._searchDataFull();
                this._searchDataFull._name = __searchTextNew;
                this._searchDataFull._dataList._loadViewFormat(this._searchDataFull._name, MyLib._myGlobal._userSearchScreenGroup, false);
                //this._searchDataFull._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDataFull__searchEnterKeyPress);
                this._searchDataFull._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchDataFull._dataList._refreshData();
            }

            //if (this._searchName.Equals("จากลูกหนี้") || this._searchName.Equals("ถึงลูกหนี้"))
            //{
            //    //string _where = " " + _g.d.erp_tambon._province + "=\'" + this._getDataStr(_g.d.ar_customer._province) + "\' and " + _g.d.erp_tambon._amper + "=\'" + this._getDataStr(_g.d.ar_customer._amper) + "\'";
            //    //MyLib._myGlobal._startSearchBox(__getControl, label_name, this._searchDataFull, false, true, _where);
            //    MyLib._myGlobal._startSearchBox(__getControl, label_name, this._searchDataFull, false);
            //}
            //else
            //{
            MyLib._myGlobal._startSearchBox(__getControl, label_name, this._searchDataFull, false);
            //}
        }

        ////void _searchDataFull__searchEnterKeyPress(MyLib._myGrid sender, int row)
        ////{
        ////    MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
        ////    MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
        ////    this._searchAll(__getParent2._name, row);
        ////}

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _screen_condition_ar__textBoxChanged(object sender, string name)
        {
            if (name.Equals("จากลูกหนี้") ||
                name.Equals("ถึงลูกหนี้") ||
                name.Equals("จากลูกค้า") ||
                name.Equals("ถึงลูกค้า"))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            else if (name.Equals("ช่วงที่1ถึง") ||
                name.Equals("ช่วงที่2ถึง") ||
                name.Equals("ช่วงที่3ถึง") ||
                name.Equals("ช่วงที่4ถึง"))
            {
                this._textBoxPeriodChange(name);
            }
        }

        private string _search_screen_neme(string _name)
        {
            switch (_name)
            {
                case "จากลูกหนี้": return _g.g._search_screen_ar;
                case "ถึงลูกหนี้": return _g.g._search_screen_ar;
                case "จากลูกค้า": return _g.g._search_screen_ar;
                case "ถึงลูกค้า": return _g.g._search_screen_ar;
                //case "จากเลขที่ใบวางบิล": return _g.g._search_screen_ar;
                //case "ถึงเลขที่ใบวางบิล": return _g.g._search_screen_ar;
                case "จากพนักงานขาย": return _g.g._search_screen_erp_user;
                case "ถึงพนักงานขาย": return _g.g._search_screen_erp_user;
                //case "จากเลขที่เอกสาร": return _g.g._search_screen_ar;
                //case "ถึงเลขที่เอกสาร": return _g.g._search_screen_ar;
                case "จากแผนก": return _g.g._search_screen_erp_department_list;
                case "ถึงแผนก": return _g.g._search_screen_erp_department_list;
            }
            return "";
        }

        private void _searchAll(string name, int row)
        {
            if (name.Length > 0)
            {
                string result = (string)this._searchDataFull._dataList._gridData._cellGet(row, 0);
                if (result.Length != 0)
                {
                    this._searchDataFull.Visible = false;
                    this._setDataStr(_searchName, result, "", true);
                    this._search(true);
                }
            }
        }

        public void _search(Boolean warning)
        {
            //try
            //{
            //    // ค้นหาชื่อ
            //    StringBuilder __myquery = new StringBuilder();
            //    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            //    switch (this._screenType)
            //    {
            //        case _screenConditionPoType.ArStatus:
            //            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr("จากลูกหนี้") + "\'"));
            //            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr("ถึงลูกหนี้") + "\'"));
            //            break;
            //        case _screenConditionPoType.ArPeriodDebtRemain:
            //            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr("จากลูกค้า") + "\'"));
            //            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr("ถึงลูกค้า") + "\'"));
            //            break;
            //    }
            //    __myquery.Append("</node>");
            //    ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            //    switch (this._screenType)
            //    {
            //        case _screenConditionPoType.ArStatus:
            //            if (this._searchAndWarning("จากลูกหนี้", (DataSet)__getData[0], warning) == false) { }
            //            if (this._searchAndWarning("ถึงลูกหนี้", (DataSet)__getData[1], warning) == false) { }
            //            break;
            //        case _screenConditionPoType.ArPeriodDebtRemain:
            //            if (this._searchAndWarning("จากลูกค้า", (DataSet)__getData[0], warning) == false) { }
            //            if (this._searchAndWarning("ถึงลูกค้า", (DataSet)__getData[1], warning) == false) { }
            //            break;
            //    }
            //}
            //catch
            //{
            //}
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
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") +" : "+ this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void _textBoxPeriodChange(string name)
        {
            if (name.Length > 0)
            {
                int __num = 0;
                bool __tryParse = int.TryParse(this._getDataStr(name), out __num);
                switch (name)
                {
                    case "ช่วงที่1ถึง":
                        if (__tryParse)
                        { this._setDataStr("ช่วงที่2", (++__num).ToString()); }
                        else
                        { this._setDataStr(name, "30"); }
                        break;
                    case "ช่วงที่2ถึง":
                        if (__tryParse)
                        { this._setDataStr("ช่วงที่3", (++__num).ToString()); }
                        else
                        { this._setDataStr(name, "60"); }
                        break;
                    case "ช่วงที่3ถึง":
                        if (__tryParse)
                        { this._setDataStr("ช่วงที่4", (++__num).ToString()); }
                        else
                        { this._setDataStr(name, "90"); }
                        break;
                    case "ช่วงที่4ถึง":
                        if (__tryParse)
                        { this._setDataStr("ช่วงที่5เกินกว่า", (__num).ToString()); }
                        else
                        { this._setDataStr(name, "120"); }
                        break;
                }
            }
        }
    }

    public enum _screenConditionPoType
    {
        None,
        PoAddGoods,
        PoAddGoodsSum,
        PoComparePurchaseMonthly,
        PoCutDepositPayment,
        PoCutPurchaseOrder,
        PoCutReceipt,
        PoDebtFromPurchase,
        PoDepositPaymentRemain,
        PoPurchaseAnalyze,
        PoPurchaseCost,
        PoPurchaseExplainByTax,
        PoPurchaseOrderDuePayment,
        PoPurchaseOrderExplain,
        PoPurchaseOrderSum,
        PoPurchaseSumByTax,
        PoPurchaseTotal,
        PoRankPurchaseTotal,
        PoReceiptExplain,
        PoRecordDepositPayment,
        PoRecordRequisitionPurchaseAndDetail,
        PoRequisitionPurchase,
        PoReturnExplain,
        PoReturnSum,
        PoStatusPurchaseOrder
    }
}
