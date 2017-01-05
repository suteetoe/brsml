using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icSearchLevelMultiItemScreen : UserControl
    {
        public _icSearchLevelMultiItemScreen()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            string _formatNumber = _g.g._getFormatNumberStr(2);
            this._selectedGid._table_name = _g.d.ic_inventory_barcode._table;
            this._selectedGid._addColumn("Sort", 3, 10, 10, true, false, true, false, _g.g._getFormatNumberStr(2,0));
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
            this._selectedGid._afterAddRow += _selectedGid__afterAddRow;
        }

        void _selectedGid__afterAddRow(object sender, int row)
        {
            this._selectedGid._cellUpdate(row, "Sort", (decimal)(row + 1), false);
        }
    }

}
