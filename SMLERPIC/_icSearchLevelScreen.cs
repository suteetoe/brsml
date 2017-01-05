using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icSearchLevelScreen : UserControl
    {
        public SMLERPControl._icmainScreenTopControl _icmainScreen = new SMLERPControl._icmainScreenTopControl();

        public _icSearchLevelScreen()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._icmainScreen.Enabled = false;
            this._icmainScreen.Dock = DockStyle.Top;
            this._panel.Controls.Add(this._icmainScreen);
            //
            this._grid._table_name = _g.d.ic_inventory_level._table;
            this._grid._addColumn(_g.d.ic_inventory_level._unit_code, 1, 10, 10, false, false, true);
            this._grid._addColumn(_g.d.ic_inventory_level._barcode, 1, 20, 20, false, false, true);
            this._grid._addColumn(_g.d.ic_inventory_level._level_1, 1, 20, 20);
            this._grid._addColumn(_g.d.ic_inventory_level._level_2, 1, 20, 20);
            this._grid._addColumn(_g.d.ic_inventory_level._level_3, 1, 20, 20);
            this._grid._addColumn(_g.d.ic_inventory_level._level_4, 1, 20, 20);
            this._grid._addColumn(_g.d.ic_inventory_level._price, 3, 15, 8, true, true, true, false, _g.g._getFormatNumberStr(3));
            this._grid._addColumn(_g.d.ic_inventory_level._suggest_remark, 1, 30, 30);
            this._grid._calcPersentWidthToScatter();
            //
            this._conditionGrid._table_name = _g.d.ic_inventory_barcode_condition._table;
            this._conditionGrid._addColumn(_g.d.ic_inventory_barcode_condition._unit_code, 1, 10, 10, false, false, true);
            this._conditionGrid._addColumn(_g.d.ic_inventory_barcode_condition._barcode, 1, 20, 20, false, false, true);
            this._conditionGrid._addColumn(_g.d.ic_inventory_barcode_condition._condition_1, 1, 500, 20);
            this._conditionGrid._addColumn(_g.d.ic_inventory_barcode_condition._condition_2, 1, 500, 20);
            this._conditionGrid._addColumn(_g.d.ic_inventory_barcode_condition._condition_3, 1, 500, 20);
            this._conditionGrid._addColumn(_g.d.ic_inventory_barcode_condition._condition_4, 1, 500, 20);
            this._conditionGrid._addColumn(_g.d.ic_inventory_barcode_condition._condition_5, 1, 500, 20);
            this._conditionGrid._addColumn(_g.d.ic_inventory_barcode_condition._condition_6, 1, 500, 20);
            this._conditionGrid._calcPersentWidthToScatter();
        }
    }
}
