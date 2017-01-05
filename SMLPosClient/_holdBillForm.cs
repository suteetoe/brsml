using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _holdBillForm : Form
    {
        public _holdBillForm(List<_holdBillClass> billList)
        {
            InitializeComponent();
            //
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            this._grid._isEdit = false;
            this._grid._table_name = _g.d.ic_trans._table;
            this._grid._addColumn(_g.d.ic_trans._doc_date, 4, 10, 20);
            this._grid._addColumn(_g.d.ic_trans._doc_time, 1, 10, 10);
            this._grid._addColumn(_g.d.ic_trans._cust_code, 1, 10, 20);
            this._grid._addColumn(_g.d.ic_trans._cust_name, 1, 10, 45);
            this._grid._addColumn(_g.d.ic_trans._total_amount, 3, 1, 15, false, true, true, false, __formatNumberAmount);
            //
            for (int __loop = 0; __loop < billList.Count; __loop++)
            {
                int __addr = this._grid._addRow();
                this._grid._cellUpdate(__addr, _g.d.ic_trans._doc_date, billList[__loop]._dateTime, false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans._doc_time, billList[__loop]._dateTime.ToString("HH:MM"), false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans._cust_code, billList[__loop]._custCode, false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans._cust_name, billList[__loop]._custName, false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans._total_amount, billList[__loop]._totalAmount, false);
            }

            this._grid._selectRow = 1;
            this._grid._gotoCell(0, 0);
            this._grid.Focus();
        }
    }
}
