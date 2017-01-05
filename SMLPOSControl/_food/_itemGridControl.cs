using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SMLPOSControl._food
{
    public class _itemGridControl : MyLib._myGrid
    {
        private string _formatNumberQty = _g.g._getFormatNumberStr(1);
        private string _formatNumberPrice = _g.g._getFormatNumberStr(2);
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);

        /// <summary>
        /// 0=ปรกติ,1=แสดงใบสั่งอาหารด้วย,2=orderlist, 3 แก้ไข
        /// </summary>
        /// <param name="mode"></param>
        public _itemGridControl(int mode, Boolean full)
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._isEdit = false;
            this._total_show = true;
            this._table_name = _g.d.table_order._table;
            if (mode == 1)
            {
                this._addColumn(_g.d.table_order._doc_no, 1, 10, 10);
            }
            else
            {
                // toe สำหรับ ย้ายโต๊ะ
                this._addColumn(_g.d.table_order._doc_no, 1, 10, 10, false, true, false);
            }

            this._addColumn(_g.d.table_order._item_code, 1, 10, 10);
            this._addColumn(_g.d.table_order._item_name, 1, 20, 20);
            this._addColumn(_g.d.table_order._unit_code, 1, 5, 5);
            this._addColumn(_g.d.table_order._qty, 3, 5, 5, false, false, true, false, this._formatNumberQty);
            if (full)
            {
                this._addColumn(_g.d.table_order._qty_cancel, 3, 5, 5, false, false, true, false, this._formatNumberQty);
                this._addColumn(_g.d.table_order._qty_balance, 3, 5, 5, false, false, true, false, this._formatNumberQty);
                this._addColumn(_g.d.table_order._confirm_status, 1, 5, 5, false, false, true, false, "", "", "", _g.d.table_order._qty_send);
            }
            this._addColumn(_g.d.table_order._price, 3, 5, 5, false, false, true, false, this._formatNumberPrice);
            this._addColumn(_g.d.table_order._sum_amount, 3, 8, 8, false, false, true, false, this._formatNumberAmount);
            if (mode != 2)
            {
                this._addColumn(_g.d.table_order._order_type, 1, 5, 5);
                this._addColumn(_g.d.table_order._remark, 1, 10, 10);
                this._addColumn(_g.d.table_order._kitchen_code, 1, 5, 5);
            }
            this._addColumn(_g.d.table_order._barcode, 1, 0, 0, true, true, true);
            this._addColumn(_g.d.table_order._guid_line, 1, 0, 0, true, true, true);
            this._addColumn(_g.d.table_order._as_item_name, 1, 5, 5, false, true, true);
            this._addColumn(_g.d.table_order._confirm_guid, 1, 20, 0, true, false);

            // ซ่อน
            this._addColumn(_g.d.ic_inventory._barcode_checker_print, 2, 20, 0, true, true);
            this._addColumn(_g.d.ic_inventory._print_order_per_unit, 2, 20, 0, false, true);
            this._addColumn(_g.d.table_order._new_guid_line, 1, 0, 0, true, true, false);
            this._addColumn(_g.d.table_order._old_doc_no, 1, 0, 0, true, true, false);
            this._addColumn("old_doc_date", 4, 0, 0, true, true, false);
            this._addColumn("old_doc_time", 1, 0, 0, true, true, false);

            this._addColumn(_g.d.ic_inventory._code_old, 1, 10, 10, false, true, false); // toe สำหรับย้ายโต๊ะ
            if (mode == 3)
            {
                // จำนวนก่อนหน้า
                this._addColumn(_g.d.table_order._old_qty, 3, 10, 10);

                // ราคาก่อนแก้
                this._addColumn(_g.d.table_order._old_price, 3, 10, 10);

            }
            this._calcPersentWidthToScatter();

            this._beforeDisplayRendering += _itemGridControl__beforeDisplayRendering;
        }


        MyLib.BeforeDisplayRowReturn _itemGridControl__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData, System.Drawing.Graphics e)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            if (row < sender._rowData.Count && columnName.Equals(_g.d.table_order._table + "." + _g.d.table_order._confirm_status))
            {
                int __status = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, sender._findColumnByName(_g.d.table_order._confirm_status)).ToString());
                ((ArrayList)senderRow.newData)[columnNumber] = "";
                if (__status == 1)
                {
                    ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myGlobal._resource("เสิร์ฟ");
                }
            }
            return __result;
        }


        public _itemGridControl(int mode, Boolean full, bool confirm)
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._isEdit = false;
            this._total_show = true;
            this._table_name = _g.d.table_order._table;
            if (mode == 1)
            {
                this._addColumn(_g.d.table_order._doc_no, 1, 10, 10);
            }
            this._addColumn(_g.d.table_order._item_code, 1, 10, 10);
            this._addColumn(_g.d.table_order._item_name, 1, 20, 20);
            this._addColumn(_g.d.table_order._unit_code, 1, 5, 5);
            this._addColumn(_g.d.table_order._qty, 3, 5, 5, false, false, true, false, this._formatNumberQty);
            if (full)
            {
                //this._addColumn(_g.d.table_order._qty_send, 3, 5, 5, false, false, true, false, this._formatNumberQty);
                this._addColumn(_g.d.table_order._qty_cancel, 3, 5, 5, false, false, true, false, this._formatNumberQty);
                this._addColumn(_g.d.table_order._qty_balance, 3, 5, 5, false, false, true, false, this._formatNumberQty);
            }
            this._addColumn(_g.d.table_order._price, 3, 5, 5, false, false, true, false, this._formatNumberPrice);
            this._addColumn(_g.d.table_order._sum_amount, 3, 8, 8, false, false, true, false, this._formatNumberAmount);
            this._addColumn(_g.d.table_order._order_type, 1, 5, 5);
            this._addColumn(_g.d.table_order._remark, 1, 10, 10);
            this._addColumn(_g.d.table_order._kitchen_code, 1, 5, 5);
            this._addColumn(_g.d.table_order._barcode, 1, 0, 0, true, true, true);
            this._addColumn(_g.d.table_order._guid_line, 1, 0, 0, true, true, true);
            this._addColumn(_g.d.table_order._as_item_name, 1, 5, 5, false, true, true);
            this._addColumn(_g.d.table_order._confirm_guid, 1, 20, 0, true, false);
            if (confirm)
            {
                this._addColumn(_g.d.table_order._confirm_status, 11, 5, 5, true, false, true, false, "", "", "", _g.d.table_order._qty_send);
            }
            this._calcPersentWidthToScatter();
        }

        public void calcSumAmountBalanceQtyLine(int row)
        {
            decimal __qty = (decimal)this._cellGet(row, _g.d.table_order._qty);
            decimal __price = (decimal)this._cellGet(row, _g.d.table_order._price);
            decimal __cancel_qty = (decimal)this._cellGet(row, _g.d.table_order._qty_cancel);

            decimal __balance_qty = __qty - __cancel_qty;
            decimal __sumAmoun = __balance_qty * __price;

            this._cellUpdate(row, _g.d.table_order._qty_balance, __balance_qty, true);
            this._cellUpdate(row, _g.d.table_order._sum_amount, __sumAmoun, true);

            this.Invalidate();

        }

    }
}
