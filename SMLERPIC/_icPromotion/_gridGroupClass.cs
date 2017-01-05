using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPIC._icPromotion
{
    public class _gridGroupClass : MyLib._myGrid
    {
        public _gridGroupClass()
        {
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            this._table_name = _g.d.ic_promotion_formula_group_qty._table;
            this._addColumn(_g.d.ic_promotion_formula_group_qty._group_number, 3, 50, 50, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_group_qty._group_number);
            this._addColumn(_g.d.ic_promotion_formula_group_qty._qty, 3, 50, 50, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_group_qty._qty);
        }

        public void _build(_promotionCaseEnum promotionCase)
        {
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);

            switch (promotionCase)
            {
                case _promotionCaseEnum.ของแถมตามมูลค่าสินค้า:
                    {
                        this._reset();

                        this._table_name = _g.d.ic_promotion_formula_group_qty._table;
                        this._addColumn(_g.d.ic_promotion_formula_group_qty._group_number, 3, 30, 30, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_group_qty._group_number);
                        this._addColumn(_g.d.ic_promotion_formula_group_qty._qty, 3, 30, 30, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_group_qty._qty);
                        this._addColumn(_g.d.ic_promotion_formula_group_qty._group_amount, 3, 40, 40, true, false, true, false, __formatNumberPrice, "", "", _g.d.ic_promotion_formula_group_qty._group_amount);
                    }
                    break;
                case _promotionCaseEnum.ส่วนลดตามจำนวนเต็มสินค้าจัดชุดตามกลุ่ม:
                    {
                        this._reset();
                        this._table_name = _g.d.ic_promotion_formula_group_qty._table;
                        this._addColumn(_g.d.ic_promotion_formula_group_qty._group_number, 3, 50, 50, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_group_qty._group_number);
                        this._addColumn(_g.d.ic_promotion_formula_group_qty._qty, 3, 50, 50, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_group_qty._qty);

                    }
                    break;
                default:
                    {
                        this._reset();
                        this._table_name = _g.d.ic_promotion_formula_group_qty._table;
                        this._addColumn(_g.d.ic_promotion_formula_group_qty._group_number, 3, 50, 50, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_group_qty._group_number);
                        this._addColumn(_g.d.ic_promotion_formula_group_qty._qty, 3, 50, 50, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_group_qty._qty);
                    }
                    break;
            }
        }
    }
}
