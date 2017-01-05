using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;

namespace SMLERPReport.ar
{
    public class _screen_condition_ar : MyLib._myScreen
    {
        private _screenConditionArType _screenTypeResult = _screenConditionArType.None;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchDataFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;

        public _screen_condition_ar()
        {
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screen_condition_ar__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screen_condition_ar__textBoxChanged);
        }

        public _screenConditionArType _screenType
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
            if (this._screenType != _screenConditionArType.None)
            {
                if (this._screenType == _screenConditionArType.ArBillingAndDetail)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่ Invoice", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่ Invoice", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่ใบวางบิล", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่ใบวางบิล", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากพนักงานขาย", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงพนักงานขาย", 1, 20, 1, true, false);
                    MyLib._myTextBox __getTextBoxFromBilling = (MyLib._myTextBox)this._getControl("จากเลขที่ใบวางบิล");
                    MyLib._myTextBox __getTextBoxToBilling = (MyLib._myTextBox)this._getControl("ถึงเลขที่ใบวางบิล");
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกค้า");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกค้า");
                    MyLib._myTextBox __getTextBoxFromSale = (MyLib._myTextBox)this._getControl("จากพนักงานขาย");
                    MyLib._myTextBox __getTextBoxToSale = (MyLib._myTextBox)this._getControl("ถึงพนักงานขาย");
                    __getTextBoxFromBilling.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromBilling.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToBilling.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToBilling.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromSale.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToSale.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArCheckBalance)
                {
                    this._addTextBox(0, 0, 1, 0, "จากลูกหนี้", 1, 20, 1, true, false, false);
                    this._addTextBox(0, 1, 1, 0, "ถึงลูกหนี้", 1, 20, 1, true, false, false);
                    this._addNumberBox(1, 0, 1, 0, "จากยอดวงเงินเครดิต", 1, 0, true);
                    this._addNumberBox(1, 1, 1, 0, "ถึงยอดวงเงินเครดิต", 1, 0, true);
                    this._addNumberBox(2, 0, 1, 0, "จากยอดวงเงินคงเหลือ", 1, 0, true);
                    this._addNumberBox(2, 1, 1, 0, "ถึงยอดวงเงินคงเหลือ", 1, 0, true);
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกหนี้");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกหนี้");
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArCutDebtLost)
                {
                    this._addTextBox(0, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากวันที่ Invoice", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงวันที่ Invoice", 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากพนักงานขาย", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงพนักงานขาย", 1, 20, 1, true, false);
                    MyLib._myTextBox __getTextBoxFromDocNo = (MyLib._myTextBox)this._getControl("จากเลขที่เอกสาร");
                    MyLib._myTextBox __getTextBoxToDocNo = (MyLib._myTextBox)this._getControl("ถึงเลขที่เอกสาร");
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกค้า");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกค้า");
                    MyLib._myTextBox __getTextBoxFromSale = (MyLib._myTextBox)this._getControl("จากพนักงานขาย");
                    MyLib._myTextBox __getTextBoxToSale = (MyLib._myTextBox)this._getControl("ถึงพนักงานขาย");
                    __getTextBoxFromDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromDocNo.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToDocNo.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromSale.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToSale.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArDetail)
                {
                    this._addTextBox(0, 0, 1, 0, "จากลูกหนี้", 1, 20, 1, true, false, false);
                    this._addTextBox(0, 1, 1, 0, "ถึงลูกหนี้", 1, 20, 1, true, false, false);
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกหนี้");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกหนี้");
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArDocumentStartYear || this._screenType == _screenConditionArType.ArReceiptAndDetail)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากพนักงานขาย", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงพนักงานขาย", 1, 20, 1, true, false);
                    MyLib._myTextBox __getTextBoxFromDocNo = (MyLib._myTextBox)this._getControl("จากเลขที่เอกสาร");
                    MyLib._myTextBox __getTextBoxToDocNo = (MyLib._myTextBox)this._getControl("ถึงเลขที่เอกสาร");
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกค้า");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกค้า");
                    MyLib._myTextBox __getTextBoxFromSale = (MyLib._myTextBox)this._getControl("จากพนักงานขาย");
                    MyLib._myTextBox __getTextBoxToSale = (MyLib._myTextBox)this._getControl("ถึงพนักงานขาย");
                    __getTextBoxFromDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromDocNo.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToDocNo.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromSale.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToSale.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArInvoicePaymentRemain)
                {
                    this._addDateBox(0, 0, 1, 0, "ยอดคงเหลือ ณ วันที่", 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากวันที่ออกบิล", 1, true);
                    this._addDateBox(1, 1, 1, 0, "ถึงวันที่ออกบิล", 1, true);
                    this._addDateBox(2, 0, 1, 0, "จากวันที่ครบกำหนด", 1, true);
                    this._addDateBox(2, 1, 1, 0, "ถึงวันที่ครบกำหนด", 1, true);
                    this._addTextBox(3, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(4, 0, 1, 0, "จากลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(4, 1, 1, 0, "ถึงลูกค้า", 1, 20, 1, true, false);
                    MyLib._myTextBox __getTextBoxFromDocNo = (MyLib._myTextBox)this._getControl("จากเลขที่เอกสาร");
                    MyLib._myTextBox __getTextBoxToDocNo = (MyLib._myTextBox)this._getControl("ถึงเลขที่เอกสาร");
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกค้า");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกค้า");
                    __getTextBoxFromDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromDocNo.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToDocNo.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArPeriodDebtRemain)
                {
                    this._addDateBox(0, 0, 1, 0, "ประจำวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากลูกค้า", 1, 20, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงลูกค้า", 1, 20, 1, true, false, false);
                    this._addCheckBox(2, 0, "แสดงเฉพาะลูกค้าที่มียอดหนี้", false, true);
                    this._addLabel(3, 0, "", "ช่วงวันที่เกินกำหนด", "");
                    this._addTextBox(4, 0, 1, 0, "ช่วงที่1", 1, 3, 0, true, false);
                    this._addTextBox(4, 1, 1, 0, "ช่วงที่1ถึง", 1, 3, 0, true, false, false);
                    this._addTextBox(5, 0, 1, 0, "ช่วงที่2", 1, 3, 0, true, false);
                    this._addTextBox(5, 1, 1, 0, "ช่วงที่2ถึง", 1, 3, 0, true, false, false);
                    this._addTextBox(6, 0, 1, 0, "ช่วงที่3", 1, 3, 0, true, false);
                    this._addTextBox(6, 1, 1, 0, "ช่วงที่3ถึง", 1, 3, 0, true, false, false);
                    this._addTextBox(7, 0, 1, 0, "ช่วงที่4", 1, 3, 0, true, false);
                    this._addTextBox(7, 1, 1, 0, "ช่วงที่4ถึง", 1, 3, 0, true, false, false);
                    this._addTextBox(8, 0, 1, 0, "ช่วงที่5เกินกว่า", 1, 3, 0, true, false);
                    this._setDataStr("ช่วงที่1", "1");
                    this._setDataStr("ช่วงที่1ถึง", "30");
                    this._setDataStr("ช่วงที่2", "31");
                    this._setDataStr("ช่วงที่2ถึง", "60");
                    this._setDataStr("ช่วงที่3", "61");
                    this._setDataStr("ช่วงที่3ถึง", "90");
                    this._setDataStr("ช่วงที่4", "91");
                    this._setDataStr("ช่วงที่4ถึง", "120");
                    this._setDataStr("ช่วงที่5เกินกว่า", "120");
                    ((MyLib._myTextBox)this._getControl("ช่วงที่1")).textBox.Enabled = false;
                    ((MyLib._myTextBox)this._getControl("ช่วงที่2")).textBox.Enabled = false;
                    ((MyLib._myTextBox)this._getControl("ช่วงที่3")).textBox.Enabled = false;
                    ((MyLib._myTextBox)this._getControl("ช่วงที่4")).textBox.Enabled = false;
                    ((MyLib._myTextBox)this._getControl("ช่วงที่5เกินกว่า")).textBox.Enabled = false;
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกค้า");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกค้า");
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArRecord)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(1, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากพนักงานขาย", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงพนักงานขาย", 1, 20, 1, true, false);
                    this._addTextBox(4, 0, 1, 0, "จากแผนก", 1, 20, 1, true, false);
                    this._addTextBox(4, 1, 1, 0, "ถึงแผนก", 1, 20, 1, true, false);
                    MyLib._myTextBox __getTextBoxFromDocNo = (MyLib._myTextBox)this._getControl("จากเลขที่เอกสาร");
                    MyLib._myTextBox __getTextBoxToDocNo = (MyLib._myTextBox)this._getControl("ถึงเลขที่เอกสาร");
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกค้า");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกค้า");
                    MyLib._myTextBox __getTextBoxFromSale = (MyLib._myTextBox)this._getControl("จากพนักงานขาย");
                    MyLib._myTextBox __getTextBoxToSale = (MyLib._myTextBox)this._getControl("ถึงพนักงานขาย");
                    MyLib._myTextBox __getTextBoxFromDepartment = (MyLib._myTextBox)this._getControl("จากแผนก");
                    MyLib._myTextBox __getTextBoxToDepartment = (MyLib._myTextBox)this._getControl("ถึงแผนก");
                    __getTextBoxFromDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromDocNo.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToDocNo.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromSale.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToSale.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromDepartment.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromDepartment.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToDepartment.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToDepartment.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArStatus)
                {
                    this._addTextBox(0, 0, 1, 0, "ประจำงวดที่", 1, 20, 2, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากลูกหนี้", 1, 20, 1, true, false, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงลูกหนี้", 1, 20, 1, true, false, false);
                    this._addCheckBox(3, 0, "ไม่แสดงรายการที่มียอดยกไปเท่ากับศูนย์", false, true);
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกหนี้");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกหนี้");
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArTrans)
                {
                    this._addTextBox(0, 0, 1, 0, "จากงวดที่", 1, 20, 2, true, false);
                    this._addTextBox(0, 1, 1, 0, "ถึงงวดที่", 1, 20, 2, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากวันที่", 1, true, false);
                    this._addDateBox(1, 1, 1, 0, "ถึงวันที่", 1, true, false);
                    this._addTextBox(2, 0, 1, 0, "จากลูกหนี้", 1, 20, 1, true, false, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงลูกหนี้", 1, 20, 1, true, false, false);
                    this._addCheckBox(3, 0, "ขึ้นหน้าใหม่ตามลูกค้า", false, true);
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกหนี้");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกหนี้");
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                }
                else if (this._screenType == _screenConditionArType.ArTransDebtPayment)
                {
                    this._addDateBox(0, 0, 1, 0, "จากวันที่เอกสาร", 1, true, false);
                    this._addDateBox(0, 1, 1, 0, "ถึงวันที่เอกสาร", 1, true, false);
                    this._addDateBox(1, 0, 1, 0, "จากวันที่ Invoice", 1, true);
                    this._addDateBox(1, 1, 1, 0, "ถึงวันที่ Invoice", 1, true);
                    this._addTextBox(2, 0, 1, 0, "จากลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, "ถึงลูกค้า", 1, 20, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, "จากพนักงานขาย", 1, 20, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, "ถึงพนักงานขาย", 1, 20, 1, true, false);
                    this._addTextBox(4, 0, 1, 0, "จากเลขที่เอกสาร", 1, 20, 1, true, false);
                    this._addTextBox(4, 1, 1, 0, "ถึงเลขที่เอกสาร", 1, 20, 1, true, false);
                    string[] __stringCombo = { "ทั้งหมด", "เงินสด", "บัตรเครดิต", "เช็ค", "เงินโอน", "เงินสดย่อย", "ยอดเงินเกินในใบเสร็จ", "ยอดเงินเกินนำมาหัก", "รายได้อื่นๆ", "ค่าใช้จ่ายอื่นๆ" };
                    this._addComboBox(5, 0, "แสดงรายการ", true, __stringCombo, true);
                    this._addCheckBox(6, 0, "แสดงเอกสารตัดชำระหนี้", false, true);
                    this._addCheckBox(7, 0, "แสดงตามพนักงานขาย", false, true);
                    MyLib._myTextBox __getTextBoxFromAr = (MyLib._myTextBox)this._getControl("จากลูกค้า");
                    MyLib._myTextBox __getTextBoxToAr = (MyLib._myTextBox)this._getControl("ถึงลูกค้า");
                    MyLib._myTextBox __getTextBoxFromSale = (MyLib._myTextBox)this._getControl("จากพนักงานขาย");
                    MyLib._myTextBox __getTextBoxToSale = (MyLib._myTextBox)this._getControl("ถึงพนักงานขาย");
                    MyLib._myTextBox __getTextBoxFromDocNo = (MyLib._myTextBox)this._getControl("จากเลขที่เอกสาร");
                    MyLib._myTextBox __getTextBoxToDocNo = (MyLib._myTextBox)this._getControl("ถึงเลขที่เอกสาร");
                    __getTextBoxFromAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToAr.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToAr.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromSale.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToSale.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToSale.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxFromDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxFromDocNo.textBox.Leave += new EventHandler(textBox_Leave);
                    __getTextBoxToDocNo.textBox.Enter += new EventHandler(textBox_Enter);
                    __getTextBoxToDocNo.textBox.Leave += new EventHandler(textBox_Leave);
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
            try
            {
                // ค้นหาชื่อ
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                switch (this._screenType)
                {
                    case _screenConditionArType.ArStatus:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr("จากลูกหนี้") + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr("ถึงลูกหนี้") + "\'"));
                        break;
                    case _screenConditionArType.ArPeriodDebtRemain:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr("จากลูกค้า") + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr("ถึงลูกค้า") + "\'"));
                        break;
                }
                __myquery.Append("</node>");
                ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                switch (this._screenType)
                {
                    case _screenConditionArType.ArStatus:
                        if (this._searchAndWarning("จากลูกหนี้", (DataSet)__getData[0], warning) == false) { }
                        if (this._searchAndWarning("ถึงลูกหนี้", (DataSet)__getData[1], warning) == false) { }
                        break;
                    case _screenConditionArType.ArPeriodDebtRemain:
                        if (this._searchAndWarning("จากลูกค้า", (DataSet)__getData[0], warning) == false) { }
                        if (this._searchAndWarning("ถึงลูกค้า", (DataSet)__getData[1], warning) == false) { }
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

    public enum _screenConditionArType
    {
        None,
        ArBillingAndDetail,
        ArCheckBalance,
        ArCutDebtLost,
        ArDetail,
        ArDocumentStartYear,
        ArInvoicePaymentRemain,
        ArPeriodDebtRemain,
        ArReceiptAndDetail,
        ArRecord,
        ArStatus,
        ArTrans,
        ArTransDebtPayment
    }
}