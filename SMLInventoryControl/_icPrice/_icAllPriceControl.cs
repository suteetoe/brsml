using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._icPrice
{
    public partial class _icAllPriceControl : UserControl
    {
        public _icAllPriceControl()
        {
            InitializeComponent();

            this._gridPriceFormula.GetUnitCode += _gridPriceFormula_GetUnitCode;
            this._gridPriceFormula.GetUnitType += _gridPriceFormula_GetUnitType;
            this._gridPriceFormula.GetItemCode += _gridPriceFormula_GetItemCode;

            if (MyLib._myGlobal._isDesignMode == false)
            {
                ((MyLib._myGrid._columnType)this._gridBarcode._columnList[this._gridBarcode._findColumnByName(_g.d.ic_inventory_barcode._barcode)])._isEdit = false;
                ((MyLib._myGrid._columnType)this._gridBarcode._columnList[this._gridBarcode._findColumnByName(_g.d.ic_inventory_barcode._unit_code)])._isEdit = false;
            }

            this._gridBarcode._message = "";
            this._gridBarcode._addRowEnabled = false;
            this._gridBarcode.Invalidate();
        }

        private string _gridPriceFormula_GetItemCode(object sender)
        {
            return this._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code);
        }

        private int _gridPriceFormula_GetUnitType(object sender)
        {
            return MyLib._myGlobal._intPhase(this._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._unit_type));
        }

        private string _gridPriceFormula_GetUnitCode(object sender)
        {
            return this._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._unit_cost);
        }

        public void _clear()
        {
            this._icmainScreenTopControl1._clear();
            this._gridNormalPrice0._grid._clear();
            this._gridNormalPrice1._grid._clear();
            this._gridNormalPrice2._grid._clear();
            this._gridStandardPrice0._grid._clear();
            this._gridStandardPrice1._grid._clear();
            this._gridStandardPrice2._grid._clear();
            this._gridPriceFormula._clear();
            this._gridBarcode._clear();
        }

    }
}
