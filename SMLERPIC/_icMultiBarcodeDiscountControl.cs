using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icMultiBarcodeDiscountControl : UserControl
    {
        public _icMultiBarcodeDiscountControl()
        {
            InitializeComponent();

            string _formatNumber = _g.g._getFormatNumberStr(2);
            this._selectedGid._table_name = _g.d.ic_inventory_barcode._table;
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._barcode, 1, 20, 20);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._ic_code, 1, 20, 20);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._description, 1, 30, 30);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._unit_code, 1, 10, 10);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price, 3, 10, 10, false, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_2, 3, 10, 10, false, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_3, 3, 10, 10, false, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_4, 3, 10, 10, false, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._unit_name, 1, 20, 10, false, true);
            this._selectedGid._calcPersentWidthToScatter();
        }
    }
}
