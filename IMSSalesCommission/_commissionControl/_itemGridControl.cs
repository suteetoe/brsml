using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IMSSalesCommission
{
    public class _itemGridControl : MyLib._myGrid
    {
        public _itemGridControl()
        {
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            this._table_name = _g.d.ic_trans_detail_commission._table;

            this._addColumn(_g.d.ic_trans_detail_commission._item_code, 1, 1, 10, false, false, true, true);
            this._addColumn(_g.d.ic_trans_detail_commission._item_name, 1, 1, 15, false, false, false, false);
            this._addColumn(_g.d.ic_trans_detail_commission._unit_code, 1, 1, 8, false, false, false, false);
            this._addColumn(_g.d.ic_trans_detail_commission._qty, 3, 1, 8, false, false, false, false, __formatNumberQty);
            this._addColumn(_g.d.ic_trans_detail_commission._price, 3, 1, 8, false, false, false, false, __formatNumberPrice);
            this._addColumn(_g.d.ic_trans_detail_commission._discount, 1, 1, 8, false, false, false);
            this._addColumn(_g.d.ic_trans_detail_commission._sum_amount, 3, 1, 10, false, false, false, false, __formatNumberAmount);
            this._addColumn(_g.d.ic_trans_detail_commission._sum_amount_exclude_vat, 3, 1, 10, false, false, false, false, __formatNumberAmount);

            this._addColumn(_g.d.ic_trans_detail_commission._discount_doc_word, 1, 1, 8, false, false, false);
            this._addColumn(_g.d.ic_trans_detail_commission._discount_doc_amount, 3, 1, 10, false, false, false, false, __formatNumberAmount);

            this._addColumn(_g.d.ic_trans_detail_commission._commission_base, 3, 1, 10, false, false, false, false, __formatNumberAmount);

            this._addColumn(_g.d.ic_trans_detail_commission._commission, 1, 1, 10, true, false, true);
            this._addColumn(_g.d.ic_trans_detail_commission._commission_amount, 3, 1, 10, false, false, true, false, __formatNumberPrice);

            this._addColumn(_g.d.ic_trans_detail_commission._remark + "temp", 12, 0, 10, true, false, false, false, "", "", "", _g.d.ic_trans_detail_commission._remark);
            this._addColumn(_g.d.ic_trans_detail_commission._remark, 1, 0, 10, true, true, true);


            // ซ่อน 
            this._addColumn(_g.d.ic_trans_detail_commission._unit_name, 1, 1, 0, false, true, false);
            this._addColumn(_g.d.ic_trans_detail_commission._wh_name, 1, 1, 0, false, true, false);
            this._addColumn(_g.d.ic_trans_detail_commission._shelf_name, 1, 1, 0, false, true, false);

            // Auto Width
            this._calcPersentWidthToScatter();

            this._alterCellUpdate += _itemGridControl__alterCellUpdate;
            this._mouseClickClip += _itemGridControl__mouseClickClip;

        }

        void _itemGridControl__mouseClickClip(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.Equals(_g.d.ic_trans_detail_commission._remark + "temp"))
            {
                string __itemName = this._cellGet(e._row, _g.d.ic_trans_detail_commission._item_name).ToString();
                string __getRemark = this._cellGet(e._row, _g.d.ic_trans_detail_commission._remark).ToString().Replace("\n\n", "\r\n"); //.Replace("\r\n", "\n")
                _itemRemarkForm __remarkForm = new _itemRemarkForm();
                __remarkForm.Text = "หมายเหตุ : " + __itemName;
                __remarkForm._textRemark.Text = __getRemark;

                DialogResult __result =  __remarkForm.ShowDialog(MyLib._myGlobal._mainForm);
                if (__result == DialogResult.OK)
                {
                    // update cell item
                    string __remark = __remarkForm._textRemark.Text;
                    this._cellUpdate(e._row, _g.d.ic_trans_detail_commission._remark, __remark, true);
                }
            }
        }

        private void _itemGridControl__alterCellUpdate(object sender, int row, int column)
        {
            int __columnCommissionStr = this._findColumnByName(_g.d.ic_trans_detail_commission._commission);

            if (column == __columnCommissionStr)
            {
                // get total_amount and update commission
                decimal __amount = (decimal)this._cellGet(row, _g.d.ic_trans_detail_commission._commission_base);
                string __discount_word = this._cellGet(row, _g.d.ic_trans_detail_commission._commission).ToString();

                decimal __newAmount = MyLib._myGlobal._calcDiscountOnly(__amount, __discount_word, __amount, _g.g._companyProfile._item_amount_decimal)._discountAmount;

                this._cellUpdate(row, _g.d.ic_trans_detail_commission._commission_amount, __newAmount, false);
            }
        }

    }

}
