using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPIC._icPromotion
{
    public class _case1Class : MyLib._myGrid
    {
        public _case1Class()
        {
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            this._table_name = _g.d.ic_promotion_formula_action._table;
            this._addColumn(_g.d.ic_promotion_formula_action._qty_from, 3, 30, 30, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_action._qty_1);
            this._addColumn(_g.d.ic_promotion_formula_action._action_command, 1, 70, 70, true, false, true);
        }
    }
}
