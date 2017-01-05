using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _formulaForm : Form
    {
        string _fieldFormula = "Formula";
        string _fieldQty = "Qty";
        string _fieldTotal = "Total";

        public _formulaForm(string code, decimal qty)
        {
            InitializeComponent();
            this.Text = code;
            string __formatNumber1 = _g.g._getFormatNumberStr(14, 14);
            string __formatNumber2 = _g.g._getFormatNumberStr(2, 2);
            this._grid._table_name = _g.d.ic_trans_detail._table;
            this._grid._addColumn(_g.d.ic_trans_detail._item_code, 1, 20, 20);
            this._grid._addColumn(_g.d.ic_trans_detail._item_name, 1, 30, 30);
            this._grid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 30, 10);
            this._grid._addColumn(this._fieldFormula, 3, 1, 20, false, false, true, false, __formatNumber1);
            this._grid._addColumn(this._fieldQty, 3, 1, 10, false, false, true, false, __formatNumber2);
            this._grid._addColumn(this._fieldTotal, 3, 1, 10, false, false, true, false, __formatNumber2);
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + _g.d.ic_inventory_set_detail._ic_code + ",(select name_1 from ic_inventory where code=ic_code) as " + _g.d.ic_inventory_set_detail._ic_name + "," + _g.d.ic_inventory_set_detail._qty + "," + _g.d.ic_inventory_set_detail._price + "," + _g.d.ic_inventory_set_detail._unit_code + " from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + code + "\' order by " + _g.d.ic_inventory_set_detail._line_number;
            DataTable __getFormula = __myFrameWork._queryShort(__query).Tables[0];
            decimal __total = 0M;
            for (int __row = 0; __row < __getFormula.Rows.Count; __row++)
            {
                DataRow __data = __getFormula.Rows[__row];
                int __addr = this._grid._addRow();
                this._grid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __data[_g.d.ic_inventory_set_detail._ic_code].ToString(), false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __data[_g.d.ic_inventory_set_detail._ic_name].ToString(), false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __data[_g.d.ic_inventory_set_detail._unit_code].ToString(), false);
                decimal __formula = MyLib._myGlobal._decimalPhase(__data[_g.d.ic_inventory_set_detail._qty].ToString());
                this._grid._cellUpdate(__addr, this._fieldFormula, __formula, false);
                this._grid._cellUpdate(__addr, this._fieldQty, qty, false);
                this._grid._cellUpdate(__addr, this._fieldTotal, qty * __formula, false);
                if (__row > 1)
                {
                    __total += qty * __formula;
                }
            }
            int __addr2 = this._grid._addRow();
            this._grid._cellUpdate(__addr2, _g.d.ic_trans_detail._item_name, "ยอดรวมเฉพาะแม่สี", false);
            this._grid._cellUpdate(__addr2, this._fieldTotal, __total, false);
            this._grid.Invalidate();
        }
    }
}
