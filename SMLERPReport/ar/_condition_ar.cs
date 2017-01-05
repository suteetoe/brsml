using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ar
{
    public partial class _condition_ar : _condition_form
    {
        private _screenConditionArType _screenTypeResult = _screenConditionArType.None;

        public _screenConditionArType _screenType
        {
            get { return this._screenTypeResult; }
            set
            {
                if (value == _screenConditionArType.ArBillingAndDetail)
                {
                    this._grouper1.GroupTitle = "รายงานใบวางบิลพร้อมรายการย่อย";
                }
                else if (value == _screenConditionArType.ArCheckBalance)
                {
                    this._grouper1.GroupTitle = "รายงานตรวจสอบยอดวงเงิน";
                }
                else if (value == _screenConditionArType.ArCutDebtLost)
                {
                    this._grouper1.GroupTitle = "รายงานการตัดหนี้สูญ(ลูกหนี้)พร้อมรายการย่อย";
                }
                else if (value == _screenConditionArType.ArDetail)
                {
                    this._grouper1.GroupTitle = "รายงานรายละเอียดลูกค้า";
                }
                else if (value == _screenConditionArType.ArDocumentStartYear)
                {
                    this._grouper1.GroupTitle = "รายงานลูกหนี้ยกมาต้นปี";
                }
                else if (value == _screenConditionArType.ArInvoicePaymentRemain)
                {
                    this._grouper1.GroupTitle = "รายงานใบส่งของค้างชำระ-ตามวันที่";
                }
                else if (value == _screenConditionArType.ArPeriodDebtRemain)
                {
                    this._grouper1.GroupTitle = "รายงานอายุลูกหนี้แสดงยอดหนี้คงค้าง";
                }
                else if (value == _screenConditionArType.ArReceiptAndDetail)
                {
                    this._grouper1.GroupTitle = "รายงานใบเสร็จรับเงินพร้อมรายการย่อย";
                }
                else if (value == _screenConditionArType.ArRecord)
                {
                    this._grouper1.GroupTitle = "รายงานการตั้งลูกหนี้อื่นๆ";
                }
                else if (value == _screenConditionArType.ArStatus)
                {
                    this._grouper1.GroupTitle = "รายงานสถานะลูกหนี้";
                }
                else if (value == _screenConditionArType.ArTrans)
                {
                    this._grouper1.GroupTitle = "รายงานเคลื่อนไหวลูกหนี้";
                }
                else if (value == _screenConditionArType.ArTransDebtPayment)
                {
                    this._grouper1.GroupTitle = "รายงานการรับชำระหนี้ประจำวัน";
                }

                this._screen_condition_ar1._screenType = value;
                this.Invalidate();
            }
        }

        public _condition_ar()
        {
            InitializeComponent();
            this._processClick += new _processClickHandler(_condition_ar__processClick);
        }

        void _condition_ar__processClick()
        {
            if (this._screen_condition_ar1._checkEmtryField().Length == 0)
            {
                this._process = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้กำหนดเงื่อนไข"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
