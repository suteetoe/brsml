using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _condition_po : _condition_form
    {
        private _screenConditionPoType _screenTypeResult = _screenConditionPoType.None;

        public _screenConditionPoType _screenType
        {
            get { return this._screenTypeResult; }
            set
            {
                if (value == _screenConditionPoType.PoAddGoods)
                {
                    this._grouper1.GroupTitle = "รายงานใบเพิ่มสินค้า(เจ้าหนี้)-ตามสินค้า";
                }
                else if (value == _screenConditionPoType.PoAddGoodsSum)
                {
                    this._grouper1.GroupTitle = "รายงานใบเพิ่มสินค้า(เจ้าหนี้)แบบสรุป-ตามสินค้า";
                }
                else if (value == _screenConditionPoType.PoComparePurchaseMonthly)
                {
                    this._grouper1.GroupTitle = "รายงานเปรียบเทียบยอดซื้อสินค้า12เดือน(ตามสินค้า-ราคา/ปริมาณ)";
                }
                else if (value == _screenConditionPoType.PoCutDepositPayment)
                {
                    this._grouper1.GroupTitle = "รายงานการตัดใบจ่ายเงินมัดจำ";
                }
                else if (value == _screenConditionPoType.PoCutPurchaseOrder)
                {
                    this._grouper1.GroupTitle = "รายงานการตัดใบสั่งซื้อสินค้า";
                }
                else if (value == _screenConditionPoType.PoCutReceipt)
                {
                    this._grouper1.GroupTitle = "รายงานการตัดใบรับสินค้า";
                }
                else if (value == _screenConditionPoType.PoDebtFromPurchase)
                {
                    this._grouper1.GroupTitle = "รายงานใบตั้งหนี้จากการซื้อแบบสรุป-ตามวันที่";
                }
                else if (value == _screenConditionPoType.PoDepositPaymentRemain)
                {
                    this._grouper1.GroupTitle = "รายงานใบจ่ายเงินมัดจำที่คงค้าง";
                }
                else if (value == _screenConditionPoType.PoPurchaseAnalyze)
                {
                    this._grouper1.GroupTitle = "รายงานวิเคราะห์การซื้อสุทธิ-ตามสินค้า";
                }
                else if (value == _screenConditionPoType.PoPurchaseCost)
                {
                    this._grouper1.GroupTitle = "รายงานการซื้อสินค้าพร้อมค่าใช้จ่ายอื่นๆ(ต้นทุนแฝง)";
                }
                else if (value == _screenConditionPoType.PoPurchaseExplainByTax)
                {
                    this._grouper1.GroupTitle = "รายงานการซื้อสินค้าแจกแจงตามประเภทภาษี";
                }
                else if (value == _screenConditionPoType.PoPurchaseOrderDuePayment)
                {
                    this._grouper1.GroupTitle = "รายงานใบซื้อสินค้าที่ถึงกำหนดจ่ายเงิน";
                }
                else if (value == _screenConditionPoType.PoPurchaseOrderExplain)
                {
                    this._grouper1.GroupTitle = "รายงานการสั่งซื้อสินค้าแบบแจกแจง-ตามสินค้า";
                }
                else if (value == _screenConditionPoType.PoPurchaseOrderSum)
                {
                    this._grouper1.GroupTitle = "รายงานการสั่งซื้อสินค้าแบบสรุป-ตามสินค้า";
                }
                else if (value == _screenConditionPoType.PoPurchaseSumByTax)
                {
                    this._grouper1.GroupTitle = "รายงานการซื้อสินค้าสรุปตามประเภทภาษี";
                }
                else if (value == _screenConditionPoType.PoPurchaseTotal)
                {
                    this._grouper1.GroupTitle = "รายงานยอดซื้อ(ตามวันที่)";
                }
                else if (value == _screenConditionPoType.PoRankPurchaseTotal)
                {
                    this._grouper1.GroupTitle = "รายงานการจัดอันดับยอดซื้อ(ตามสินค้า-กลุ่มสินค้า)";
                }
                else if (value == _screenConditionPoType.PoReceiptExplain)
                {
                    this._grouper1.GroupTitle = "รายงานใบรับสินค้าแบบแจกแจง-ตามวันที่";
                }
                else if (value == _screenConditionPoType.PoRecordDepositPayment)
                {
                    this._grouper1.GroupTitle = "รายงานการบันทึกใบจ่ายเงินมัดจำ";
                }
                else if (value == _screenConditionPoType.PoRecordRequisitionPurchaseAndDetail)
                {
                    this._grouper1.GroupTitle = "รายงานการบันทึกใบขออนุมัติซื้อพร้อมรายการย่อย";
                }
                else if (value == _screenConditionPoType.PoRequisitionPurchase)
                {
                    this._grouper1.GroupTitle = "รายงานใบขอซื้อ-ตามสถานะ";
                }
                else if (value == _screenConditionPoType.PoReturnExplain)
                {
                    this._grouper1.GroupTitle = "รายงานการส่งคืนสินค้า/ลดหนี้แบบแจกแจง-ตามสินค้า";
                }
                else if (value == _screenConditionPoType.PoReturnSum)
                {
                    this._grouper1.GroupTitle = "รายงานการส่งคืนสินค้า/ลดหนี้แบบสรุป-ตามสินค้า";
                }
                else if (value == _screenConditionPoType.PoStatusPurchaseOrder)
                {
                    this._grouper1.GroupTitle = "รายงานสถานะใบสั่งซื้อสินค้า";
                }
                this._screen_condition_po1._screenType = value;
                this.Invalidate();
            }
        }

        public _condition_po()
        {
            InitializeComponent();
            this._processClick += new _processClickHandler(_condition_ar__processClick);
        }

        void _condition_ar__processClick()
        {
            if (this._screen_condition_po1._checkEmtryField().Length == 0)
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
