using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPICInfo
{
    public partial class _stkNoMovement : UserControl
    {
        string __formatNumberQty = _g.g._getFormatNumberStr(1);
        string __formatNumberCost = _g.g._getFormatNumberStr(2);
        string __formatNumberAmount = _g.g._getFormatNumberStr(3);
        string __formatDate = "dd/MM/yyyy";

        public _stkNoMovement()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._resultGrid._columnTopActive = true;
            this._resultGrid._table_name = _g.d.ic_resource._table;
            this._resultGrid._addColumn(_g.d.ic_resource._ic_code, 1, 10, 10);
            this._resultGrid._addColumn(_g.d.ic_resource._ic_name, 1, 10, 20);
            this._resultGrid._addColumn(_g.d.ic_resource._ic_unit_code, 1, 5, 10);
            //
            this._resultGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            this._resultGrid._addColumn(_g.d.ic_resource._average_cost, 3, 10, 8, false, false, false, false, __formatNumberCost);
            this._resultGrid._addColumn(_g.d.ic_resource._average_cost_end, 3, 10, 8, false, false, false, false, __formatNumberCost);
            this._resultGrid._addColumn(_g.d.ic_resource._balance_amount, 3, 10, 10, false, false, false, false, __formatNumberAmount);
            //
            this._resultGrid._addColumn(_g.d.ic_resource._last_in_date, 4, 5, 10, true, false, true, false, __formatDate);
            this._resultGrid._addColumn(_g.d.ic_resource._last_in_flag, 1, 5,5);
            this._resultGrid._addColumn(_g.d.ic_resource._last_out_date, 4, 5, 10, true, false, true, false, __formatDate);
            this._resultGrid._addColumn(_g.d.ic_resource._last_out_flag, 1, 5, 5);
            this._resultGrid._addColumn(_g.d.ic_resource._last_sale_date, 4, 5, 10, true, false, true, false, __formatDate);
            this._resultGrid._addColumn(_g.d.ic_resource._no_movement_date, 3, 5, 8, false, false, false, false, _g.g._getFormatNumberStr(0, 0));
            //
            this._resultGrid._addColumnTop(_g.d.ic_resource._balance_qty, this._resultGrid._findColumnByName(_g.d.ic_resource._balance_qty), this._resultGrid._findColumnByName(_g.d.ic_resource._balance_amount));
            this._resultGrid._addColumnTop(_g.d.ic_resource._in, this._resultGrid._findColumnByName(_g.d.ic_resource._last_in_date), this._resultGrid._findColumnByName(_g.d.ic_resource._last_in_flag));
            this._resultGrid._addColumnTop(_g.d.ic_resource._out, this._resultGrid._findColumnByName(_g.d.ic_resource._last_out_date), this._resultGrid._findColumnByName(_g.d.ic_resource._last_out_flag));
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._balance_qty, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._average_cost, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._average_cost_end, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._balance_amount, Color.AliceBlue);
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._last_out_date, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._last_out_flag, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._amount_in, Color.AliceBlue);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
            this._resultGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_resultGrid__beforeDisplayRow);
        }

        MyLib.BeforeDisplayRowReturn _resultGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            if (columnName.Equals(_g.d.ic_resource._table + "." + _g.d.ic_resource._last_in_flag))
            {
                string __getStr = sender._cellGet(row, _g.d.ic_resource._last_in_flag).ToString();
                int __getTransFlag = (__getStr.Length == 0) ? 0 : MyLib._myGlobal._intPhase(__getStr);
                if (__getTransFlag != 0)
                {
                    ((ArrayList)senderRow.newData)[sender._findColumnByName(_g.d.ic_resource._last_in_flag)] = _g.g._transFlagGlobal._transName(__getTransFlag);
                }
            }
            else
                if (columnName.Equals(_g.d.ic_resource._table + "." + _g.d.ic_resource._last_out_flag))
                {
                    string __getStr = sender._cellGet(row, _g.d.ic_resource._last_out_flag).ToString();
                    int __getTransFlag = (__getStr.Length == 0) ? 0 : MyLib._myGlobal._intPhase(__getStr);
                    if (__getTransFlag != 0)
                    {
                        ((ArrayList)senderRow.newData)[sender._findColumnByName(_g.d.ic_resource._last_out_flag)] = _g.g._transFlagGlobal._transName(__getTransFlag);
                    }
                }
            return senderRow;
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            string __itemBegin = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_begin);
            string __itemEnd = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_end);
            int __dayFrom = (int)this._conditionScreen._getDataNumber(_g.d.ic_resource._count_day_from);
            int __DayTo = (int)this._conditionScreen._getDataNumber(_g.d.ic_resource._count_day_to);
            DateTime __dateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_end));
            SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
            this._resultGrid._loadFromDataTable(__process._stkStockNoMovement(null,__itemBegin, __itemEnd, __dayFrom, __DayTo, __dateEnd));
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
