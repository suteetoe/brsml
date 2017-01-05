using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPIC._icPromotion
{
    public class _case3Class : MyLib._myGrid
    {
        public _case3Class()
        {
            this._table_name = _g.d.ic_promotion_formula_action._table;
            this._addColumn(_g.d.ic_promotion_formula_action._action_command, 1, 100, 100, true, false, true);
        }
    }
}
