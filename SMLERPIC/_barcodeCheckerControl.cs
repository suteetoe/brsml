using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _barcodeCheckerControl : UserControl
    {
        public _barcodeCheckerControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            this._grid._table_name = _g.d.ic_inventory_barcode._table;
            this._grid._addColumn(_g.d.ic_inventory_barcode._barcode, 1, 15, 20, true, false, true, false);
            this._grid._addColumn(_g.d.ic_inventory_barcode._ic_code, 1, 15, 20, true, false, true, false);
            this._grid._addColumn(_g.d.ic_inventory_barcode._description, 1, 15, 25, true, false, true, false);
            this._grid._addColumn(_g.d.ic_inventory_barcode._unit_code, 1, 5, 10, false, false, true, true);
            this._grid._addColumn(_g.d.ic_inventory_barcode._price, 3, 0, 10, true, false, true, false, __formatNumberPrice);
            this._grid._addColumn(_g.d.ic_inventory_barcode._price_2, 3, 0, 10, true, false, true, false, __formatNumberPrice);
            this._grid._addColumn(_g.d.ic_inventory_barcode._price_3, 3, 0, 10, true, false, true, false, __formatNumberPrice);
            this._grid._addColumn(_g.d.ic_inventory_barcode._price_4, 3, 0, 10, true, false, true, false, __formatNumberPrice);
            this._grid._isEdit = false;
            this._grid._calcPersentWidthToScatter();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            this._grid._importFromTextFile(false);
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            StringBuilder __itemCodeList = new StringBuilder();
            for (int __row = 0; __row < this._grid._rowData.Count; __row++)
            {
                if (__itemCodeList.Length > 0)
                {
                    __itemCodeList.Append(",");
                }
                __itemCodeList.Append("\'" + this._grid._cellGet(__row, _g.d.ic_inventory_barcode._barcode).ToString() + "\'");
                this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._ic_code, "", false);
                this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._description, "", false);
                this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._unit_code, "", false);
                this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._price, 0M, false);
                this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._price_2, 0M, false);
                this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._price_3, 0M, false);
                this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._price_4, 0M, false);
            }
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory_barcode._barcode, _g.d.ic_inventory_barcode._ic_code, "coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + "),\'\') as " + _g.d.ic_inventory_barcode._description, _g.d.ic_inventory_barcode._unit_code, _g.d.ic_inventory_barcode._price, _g.d.ic_inventory_barcode._price_2, _g.d.ic_inventory_barcode._price_3, _g.d.ic_inventory_barcode._price_4 + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + " in (" + __itemCodeList.ToString() + ") order by " + _g.d.ic_inventory_barcode._barcode);
            DataTable __dt = __myFrameWork._queryShort(__query).Tables[0];
            StringBuilder __error = new StringBuilder();
            for (int __row = 0; __row < this._grid._rowData.Count; __row++)
            {
                string __barcode = this._grid._cellGet(__row, _g.d.ic_inventory_barcode._barcode).ToString();
                if (__dt.Rows.Count > 0)
                {
                    DataRow[] __dataRows = __dt.Select(_g.d.ic_inventory_barcode._barcode + "=\'" + __barcode + "\'");
                    if (__dataRows.Length > 0)
                    {
                        this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._ic_code, __dataRows[0][_g.d.ic_inventory_barcode._ic_code].ToString(), false);
                        this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._description, __dataRows[0][_g.d.ic_inventory_barcode._description].ToString(), false);
                        this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._unit_code, __dataRows[0][_g.d.ic_inventory_barcode._unit_code].ToString(), false);
                        this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._price, MyLib._myGlobal._decimalPhase(__dataRows[0][_g.d.ic_inventory_barcode._price].ToString()), false);
                        this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._price_2, MyLib._myGlobal._decimalPhase(__dataRows[0][_g.d.ic_inventory_barcode._price_2].ToString()), false);
                        this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._price_3, MyLib._myGlobal._decimalPhase(__dataRows[0][_g.d.ic_inventory_barcode._price_3].ToString()), false);
                        this._grid._cellUpdate(__row, _g.d.ic_inventory_barcode._price_4, MyLib._myGlobal._decimalPhase(__dataRows[0][_g.d.ic_inventory_barcode._price_4].ToString()), false);
                    }
                    else
                    {
                        __error.Append(__barcode + "\r\n");
                    }
                }
            }
            this._errorTextBox.Text = __error.ToString();
        }
    }
}
