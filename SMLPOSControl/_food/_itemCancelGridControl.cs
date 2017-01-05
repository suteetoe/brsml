using System;
using System.Collections.Generic;
using System.Text;

namespace SMLPOSControl._food
{
    public class _itemCancelGridControl : MyLib._myGrid
    {
        private string _formatNumberQty = _g.g._getFormatNumberStr(1);
        private string _formatNumberPrice = _g.g._getFormatNumberStr(2);
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);

        /// <summary>
        /// 0=ปรกติ,1=แสดงใบสั่งอาหารด้วย
        /// </summary>
        /// <param name="mode"></param>
        public _itemCancelGridControl()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._isEdit = false;
            this._total_show = true;
            this._table_name = _g.d.table_order_cancel._table;
            this._addColumn(_g.d.table_order_cancel._item_code, 1, 10, 10);
            this._addColumn(_g.d.table_order_cancel._item_name, 1, 20, 20, false);
            this._addColumn(_g.d.table_order_cancel._unit_code, 1, 5, 5);
            this._addColumn(_g.d.table_order_cancel._qty, 3, 5, 5, false, false, true, false, this._formatNumberQty);
            this._addColumn(_g.d.table_order_cancel._price, 3, 5, 5, false, false, true, false, this._formatNumberPrice);
            this._addColumn(_g.d.table_order_cancel._sum_amount, 3, 8, 8, false, false, true, false, this._formatNumberAmount);
            this._addColumn(_g.d.table_order_cancel._guid_line, 1, 0, 0, false, true, true);
            this._addColumn(_g.d.table_order._kitchen_code, 1, 255, 0, false, true, false);
            this._addColumn(_g.d.ic_inventory._barcode_checker_print, 2, 20, 0, false, true, false);
            this._addColumn(_g.d.ic_inventory._print_order_per_unit, 2, 20, 0, false, true, false);
            //this._addColumn(_g.d.table_order_cancel._doc_date, 1, 10, 10);

            this._calcPersentWidthToScatter();


        }


    }
}
