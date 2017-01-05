using System;
using System.Collections.Generic;
using System.Text;

namespace SMLBalanceTransfer
{
    public class _icBalancGrid : MyLib._myGrid
    {
        public _icBalancGrid()
        {
            if (this.DesignMode == false)
            {
                this._isEdit = false;

                this._addColumn("ic_code", 1, 0, 15);
                this._addColumn("new_ic_code", 1, 0, 15);
                this._addColumn("ic_name", 1, 0, 20);

                this._addColumn("unit_code", 1, 0, 20);
                this._addColumn("wh_code", 1, 0, 10);
                this._addColumn("shelf_code", 1, 0, 10);
                this._addColumn("qty", 3, 0, 10);
            }
        }
    }

    public class _apBalanceGrid : MyLib._myGrid
    {
        public _apBalanceGrid()
        {
            if (this.DesignMode == false)
            {

                this._addColumn("ap_code", 1, 0, 20);
                this._addColumn("ap_name", 1, 0, 60);

                this._addColumn("ap_balance", 3, 0, 20);
            }
        }
    }

    public class _arBalanceGrid : MyLib._myGrid
    {
        public _arBalanceGrid()
        {
            if (this.DesignMode == false)
            {

                this._addColumn("ap_code", 1, 0, 20);
                this._addColumn("ap_name", 1, 0, 60);

                this._addColumn("ap_balance", 1, 0, 20);
            }
        }
    }
}
