using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._ic
{
    public partial class _icPurchasePointForm : Form
    {
        public _icPurchasePointForm()
        {
            InitializeComponent();

            string __formatNumberQty = _g.g._getFormatNumberStr(1);

            this.Text = "สินค้าถึงจุดสั่งซื้อ";
            this._gridPurchasePoint._isEdit = false;
            this._gridPurchasePoint._table_name = _g.d.ic_resource._table;
            this._gridPurchasePoint._addColumn(_g.d.ic_resource._ic_code, 1, 20, 20);

            this._gridPurchasePoint._addColumn(_g.d.ic_resource._ic_name, 1, 30, 30);
            this._gridPurchasePoint._addColumn(_g.d.ic_resource._ic_unit_code, 1, 10, 10);

            this._gridPurchasePoint._addColumn(_g.d.ic_resource._purchase_point, 3, 20, 20, true, false, true, false, __formatNumberQty);
            this._gridPurchasePoint._addColumn(_g.d.ic_resource._balance_qty, 3, 20, 20, true, false, true, false, __formatNumberQty);

            this._gridPurchasePoint._addColumn(_g.d.ic_resource._purchase_balance_qty, 3, 20, 20, true, false, true, false, __formatNumberQty);
            this._gridPurchasePoint._addColumn(_g.d.ic_resource._accrued_out_qty, 3, 20, 20, true, false, true, false, __formatNumberQty);
            this._gridPurchasePoint._addColumn(_g.d.ic_resource._book_out_qty, 3, 20, 20, true, false, true, false, __formatNumberQty);
            
        }
        
        
    }

}
