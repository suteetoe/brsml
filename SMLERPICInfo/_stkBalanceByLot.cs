using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public partial class _stkBalanceByLot : UserControl
    {
        string __formatNumberQty = _g.g._getFormatNumberStr(1);
        string __formatNumberCost = _g.g._getFormatNumberStr(2);
        string __formatNumberAmount = _g.g._getFormatNumberStr(3);
        string __formatNumberPrice = _g.g._getFormatNumberStr(2);

        public _stkBalanceByLot()
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
            this._resultGrid._addColumn(_g.d.ic_resource._lot_number, 1, 5, 10);
            //
            this._resultGrid._addColumn(_g.d.ic_resource._qty_in, 3, 10, 8, false, false, false, false, __formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                this._resultGrid._addColumn(_g.d.ic_resource._average_cost_in, 3, 10, 8, false, false, false, false, __formatNumberQty);
                this._resultGrid._addColumn(_g.d.ic_resource._amount_in, 3, 10, 10, false, false, false, false, __formatNumberCost);
            }

            this._resultGrid._addColumn(_g.d.ic_resource._qty_out, 3, 10, 8, false, false, false, false, __formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                this._resultGrid._addColumn(_g.d.ic_resource._average_cost_out, 3, 10, 8, false, false, false, false, __formatNumberCost);
                this._resultGrid._addColumn(_g.d.ic_resource._amount_out, 3, 10, 10, false, false, false, false, __formatNumberAmount);
            }

            this._resultGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {
                this._resultGrid._addColumn(_g.d.ic_resource._average_cost, 3, 10, 8, false, false, false, false, __formatNumberCost);
                this._resultGrid._addColumn(_g.d.ic_resource._balance_amount, 3, 10, 10, false, false, false, false, __formatNumberAmount);
            }
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {
                //
                this._resultGrid._addColumnTop(_g.d.ic_resource._in, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_in), this._resultGrid._findColumnByName(_g.d.ic_resource._amount_in));
                this._resultGrid._addColumnTop(_g.d.ic_resource._out, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_out), this._resultGrid._findColumnByName(_g.d.ic_resource._amount_out));
                this._resultGrid._addColumnTop(_g.d.ic_resource._net, this._resultGrid._findColumnByName(_g.d.ic_resource._balance_qty), this._resultGrid._findColumnByName(_g.d.ic_resource._balance_amount));
            }
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._qty_in, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._average_cost_in, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._amount_in, Color.AliceBlue);
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._balance_qty, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._average_cost, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._average_cost_end, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._balance_amount, Color.AliceBlue);
            this._resultGrid._mouseClick += new MyLib.MouseClickHandler(_resultGrid__mouseClick);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
            //
            this._movementGrid._columnTopActive = true;
            this._movementGrid._table_name = _g.d.ic_resource._table;
            this._movementGrid._addColumn(_g.d.ic_resource._doc_date, 4, 10, 10, false, false, true, false, "dd/MM/yyyy");
            this._movementGrid._addColumn(_g.d.ic_resource._doc_time, 1, 5, 5);
            this._movementGrid._addColumn(_g.d.ic_resource._trans_name, 1, 5, 5);
            this._movementGrid._addColumn(_g.d.ic_resource._doc_no, 1, 10, 15);
            this._movementGrid._addColumn(_g.d.ic_resource._warehouse, 1, 5, 5);
            this._movementGrid._addColumn(_g.d.ic_resource._shelf_detail, 1, 5, 5);
            this._movementGrid._addColumn(_g.d.ic_resource._qty_in, 3, 10, 8, false, false, false, false, __formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                this._movementGrid._addColumn(_g.d.ic_resource._cost_in, 3, 10, 8, false, false, false, false, __formatNumberPrice);
                this._movementGrid._addColumn(_g.d.ic_resource._amount_in, 3, 10, 10, false, false, false, false, __formatNumberAmount);
            }

            this._movementGrid._addColumn(_g.d.ic_resource._qty_out, 3, 10, 8, false, false, false, false, __formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                this._movementGrid._addColumn(_g.d.ic_resource._cost_out, 3, 10, 8, false, false, false, false, __formatNumberPrice);
                this._movementGrid._addColumn(_g.d.ic_resource._amount_out, 3, 10, 10, false, false, false, false, __formatNumberAmount);
            }

            this._movementGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                this._movementGrid._addColumn(_g.d.ic_resource._average_cost, 3, 10, 8, false, false, false, false, __formatNumberPrice);
                this._movementGrid._addColumn(_g.d.ic_resource._amount, 3, 10, 10, false, false, false, false, __formatNumberAmount);
            }
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                //
                this._movementGrid._addColumnTop(_g.d.ic_resource._in, this._movementGrid._findColumnByName(_g.d.ic_resource._qty_in), this._movementGrid._findColumnByName(_g.d.ic_resource._amount_in));
                this._movementGrid._addColumnTop(_g.d.ic_resource._out, this._movementGrid._findColumnByName(_g.d.ic_resource._qty_out), this._movementGrid._findColumnByName(_g.d.ic_resource._amount_out));
                this._movementGrid._addColumnTop(_g.d.ic_resource._amount, this._movementGrid._findColumnByName(_g.d.ic_resource._balance_qty), this._movementGrid._findColumnByName(_g.d.ic_resource._amount));
            }
            //
            this._movementGrid._setColumnBackground(_g.d.ic_resource._qty_in, Color.AliceBlue);
            this._movementGrid._setColumnBackground(_g.d.ic_resource._cost_in, Color.AliceBlue);
            this._movementGrid._setColumnBackground(_g.d.ic_resource._amount_in, Color.AliceBlue);
            this._movementGrid._setColumnBackground(_g.d.ic_resource._balance_qty, Color.AliceBlue);
            this._movementGrid._setColumnBackground(_g.d.ic_resource._average_cost, Color.AliceBlue);
            this._movementGrid._setColumnBackground(_g.d.ic_resource._amount, Color.AliceBlue);
            //
            this._movementGrid._calcPersentWidthToScatter();
            this._movementGrid._total_show = true;
        }

        void _resultGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __itemCode = this._resultGrid._cellGet(e._row, _g.d.ic_resource._ic_code).ToString();
            string __lotNumber = this._resultGrid._cellGet(e._row, _g.d.ic_resource._lot_number).ToString();
            StringBuilder __query = new StringBuilder();
            __query.Append("select ");
            __query.Append(_g.d.ic_trans_detail_lot._doc_date + " as " + _g.d.ic_resource._doc_date + ",");
            __query.Append(_g.d.ic_trans_detail_lot._doc_time + " as " + _g.d.ic_resource._doc_time + ",");
            __query.Append(_g.d.ic_trans_detail_lot._trans_flag + " as " + _g.d.ic_resource._trans_name + ",");
            __query.Append(_g.d.ic_trans_detail_lot._doc_no + " as " + _g.d.ic_resource._doc_no + ",");
            __query.Append(_g.d.ic_trans_detail_lot._wh_code + " as " + _g.d.ic_resource._warehouse + ",");
            __query.Append(_g.d.ic_trans_detail_lot._shelf_code + " as " + _g.d.ic_resource._shelf_detail + ",");
            __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=1 then " + _g.d.ic_trans_detail_lot._qty + " else 0 end as " + _g.d.ic_resource._qty_in + ",");
            __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=1 then " + _g.d.ic_trans_detail_lot._price + " else 0 end as " + _g.d.ic_resource._cost_in + ",");
            __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=1 then " + _g.d.ic_trans_detail_lot._amount + " else 0 end as " + _g.d.ic_resource._amount_in + ",");
            __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=-1 then " + _g.d.ic_trans_detail_lot._qty + " else 0 end as " + _g.d.ic_resource._qty_out + ",");
            __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=-1 then " + _g.d.ic_trans_detail_lot._price + " else 0 end as " + _g.d.ic_resource._cost_out + ",");
            __query.Append("case when " + _g.d.ic_trans_detail_lot._calc_flag + "=-1 then " + _g.d.ic_trans_detail_lot._amount + " else 0 end as " + _g.d.ic_resource._amount_out);
            __query.Append(" from " + _g.d.ic_trans_detail_lot._table + " where " + _g.d.ic_trans_detail_lot._item_code + "=\'" + __itemCode + "\' and " + _g.d.ic_trans_detail_lot._lot_number + "=\'" + __lotNumber + "\'");
            __query.Append(" order by " + _g.d.ic_trans_detail_lot._doc_date + "," + _g.d.ic_trans_detail_lot._doc_time + "," + _g.d.ic_trans_detail_lot._doc_no + "," + _g.d.ic_trans_detail_lot._line_number);
            this._movementGrid._clear();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __dt = __myFrameWork._queryShort(__query.ToString()).Tables[0];
            int __columnNumberDocDate = __dt.Columns.IndexOf(_g.d.ic_resource._doc_date);
            int __columnNumberDocTime = __dt.Columns.IndexOf(_g.d.ic_resource._doc_time);
            int __columnNumberDocNo = __dt.Columns.IndexOf(_g.d.ic_resource._doc_no);
            int __columnNumberWareHouse = __dt.Columns.IndexOf(_g.d.ic_resource._warehouse);
            int __columnNumberShelf = __dt.Columns.IndexOf(_g.d.ic_resource._shelf_detail);
            int __columnNumberQtyIn = __dt.Columns.IndexOf(_g.d.ic_resource._qty_in);
            int __columnNumberCostIn = __dt.Columns.IndexOf(_g.d.ic_resource._cost_in);
            int __columnNumberAmountIn = __dt.Columns.IndexOf(_g.d.ic_resource._amount_in);
            int __columnNumberQtyOut = __dt.Columns.IndexOf(_g.d.ic_resource._qty_out);
            int __columnNumberCostOut = __dt.Columns.IndexOf(_g.d.ic_resource._cost_out);
            int __columnNumberAmountOut = __dt.Columns.IndexOf(_g.d.ic_resource._amount_out);
            int __columnNumberTrandFlag = __dt.Columns.IndexOf(_g.d.ic_resource._trans_name);
            //
            decimal __balanceQty = 0;
            decimal __balanceAmount = 0;
            DateTime __dateBegin = this._conditionScreen._getDataDate(_g.d.ic_resource._date_begin);
            for (int __row = 0; __row < __dt.Rows.Count; __row++)
            {
                DataRow __data = __dt.Rows[__row];
                DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__data[__columnNumberDocDate].ToString());
                string __docTime = __data[__columnNumberDocTime].ToString();
                string __docNo = __data[__columnNumberDocNo].ToString();
                string __wareHouse = __data[__columnNumberWareHouse].ToString();
                string __shelf = __data[__columnNumberShelf].ToString();
                decimal __qtyIn = MyLib._myGlobal._decimalPhase(__data[__columnNumberQtyIn].ToString());
                decimal __costIn = MyLib._myGlobal._decimalPhase(__data[__columnNumberCostIn].ToString());
                decimal __amountIn = MyLib._myGlobal._decimalPhase(__data[__columnNumberAmountIn].ToString());
                decimal __qtyOut = MyLib._myGlobal._decimalPhase(__data[__columnNumberQtyOut].ToString());
                decimal __costOut = MyLib._myGlobal._decimalPhase(__data[__columnNumberCostOut].ToString());
                decimal __amountOut = MyLib._myGlobal._decimalPhase(__data[__columnNumberAmountOut].ToString());
                int __transFlag = (int)MyLib._myGlobal._decimalPhase(__data[__columnNumberTrandFlag].ToString());
                int __dateCompareValue = __docDate.CompareTo(__dateBegin);
                if (__docDate.CompareTo(__dateBegin) >= 0)
                {
                    // ยอดยกมา
                    if (this._movementGrid._rowData.Count == 0 && (__balanceQty != 0 || __balanceAmount != 0))
                    {
                        int __addrBalance = this._movementGrid._addRow();
                        this._movementGrid._cellUpdate(__addrBalance, _g.d.ic_resource._trans_name, "ยอดยกมา", false);
                        this._movementGrid._cellUpdate(__addrBalance, _g.d.ic_resource._balance_qty, __balanceQty, false);
                        this._movementGrid._cellUpdate(__addrBalance, _g.d.ic_resource._average_cost, (__balanceQty == 0) ? 0 : __balanceAmount / __balanceQty, false);
                        this._movementGrid._cellUpdate(__addrBalance, _g.d.ic_resource._amount, __balanceAmount, false);
                    }
                    __balanceQty = __balanceQty + (__qtyIn - __qtyOut);
                    __balanceAmount = __balanceAmount + (__amountIn - __amountOut);
                    //
                    int __addr = this._movementGrid._addRow();
                    string __transName = _g.g._transFlagGlobal._transName(__transFlag);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._doc_date, __docDate, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._doc_time, __docTime, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._trans_name, __transName, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._doc_no, __docNo, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._warehouse, __wareHouse, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._shelf_detail, __shelf, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._qty_in, __qtyIn, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._cost_in, __costIn, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._amount_in, __amountIn, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._qty_out, __qtyOut, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._cost_out, __costOut, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._amount_out, __amountOut, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._balance_qty, __balanceQty, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._average_cost, (__balanceQty == 0) ? 0 : __balanceAmount / __balanceQty, false);
                    this._movementGrid._cellUpdate(__addr, _g.d.ic_resource._amount, __balanceAmount, false);
                }
                else
                {
                    __balanceQty = __balanceQty + (__qtyIn - __qtyOut);
                    __balanceAmount = __balanceAmount + (__amountIn - __amountOut);

                    // รายการสุดท้าย
                    if (__row == __dt.Rows.Count-1 && (__balanceQty != 0 || __balanceAmount != 0))
                    {
                        int __addrBalance = this._movementGrid._addRow();
                        this._movementGrid._cellUpdate(__addrBalance, _g.d.ic_resource._trans_name, "ยอดยกมา", false);
                        this._movementGrid._cellUpdate(__addrBalance, _g.d.ic_resource._balance_qty, __balanceQty, false);
                        this._movementGrid._cellUpdate(__addrBalance, _g.d.ic_resource._average_cost, (__balanceQty == 0) ? 0 : __balanceAmount / __balanceQty, false);
                        this._movementGrid._cellUpdate(__addrBalance, _g.d.ic_resource._amount, __balanceAmount, false);
                    }
                }
            }
            this._movementGrid.Invalidate();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            string __itemBegin = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_begin);
            string __itemEnd = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_end);
            DateTime __dateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_end));
            Boolean __balanceOnly = (this._conditionScreen._getDataStr(_g.d.ic_resource._balance_only).Equals("1")) ? true : false;
            SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();

            this._resultGrid._loadFromDataTable(__process._stkStockInfoAndBalanceByLot(_g.g._productCostType.ปรกติ, null, __itemBegin, __itemEnd, __dateEnd, __balanceOnly, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT));
        }
    }
}
