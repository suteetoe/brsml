using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public class _stkBalanceWareHouseGrid : MyLib._myGrid
    {
        string _formatNumberQty = _g.g._getFormatNumberStr(1);
        string _formatNumberCost = _g.g._getFormatNumberStr(2);
        string _formatNumberAmount = _g.g._getFormatNumberStr(3);
        ArrayList _itemListCode = new ArrayList();
        ArrayList _itemListColor = new ArrayList();

        public _stkBalanceWareHouseGrid()
        {
            if (MyLib._myGlobal._isDesignMode) return;
            //
            this._columnTopActive = true;
            this._table_name = _g.d.ic_resource._table;
            this._addColumn(_g.d.ic_resource._ic_code, 1, 10, 10);
            this._addColumn(_g.d.ic_resource._ic_name, 1, 10, 20);
            this._addColumn(_g.d.ic_resource._ic_unit_code, 1, 5, 10);
            this._addColumn(_g.d.ic_resource._warehouse, 1, 5, 5);
            //
            this._addColumn(_g.d.ic_resource._qty_in, 3, 10, 8, false, false, false, false, _formatNumberQty);

            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {
                this._addColumn(_g.d.ic_resource._average_cost_in, 3, 10, 8, false, false, false, false, _formatNumberQty);
                this._addColumn(_g.d.ic_resource._amount_in, 3, 10, 10, false, false, false, false, _formatNumberCost);
            }

            this._addColumn(_g.d.ic_resource._qty_out, 3, 10, 8, false, false, false, false, _formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {
                this._addColumn(_g.d.ic_resource._average_cost_out, 3, 10, 8, false, false, false, false, _formatNumberCost);
                this._addColumn(_g.d.ic_resource._amount_out, 3, 10, 10, false, false, false, false, _formatNumberAmount);
            }
            this._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, _formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {
                this._addColumn(_g.d.ic_resource._average_cost, 3, 10, 8, false, false, false, false, _formatNumberCost);
                this._addColumn(_g.d.ic_resource._average_cost_end, 3, 10, 8, false, false, false, false, _formatNumberCost);
                this._addColumn(_g.d.ic_resource._balance_amount, 3, 10, 10, false, false, false, false, _formatNumberAmount);
            }
            //
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {
                this._addColumnTop(_g.d.ic_resource._in, this._findColumnByName(_g.d.ic_resource._qty_in), this._findColumnByName(_g.d.ic_resource._amount_in));
                this._addColumnTop(_g.d.ic_resource._out, this._findColumnByName(_g.d.ic_resource._qty_out), this._findColumnByName(_g.d.ic_resource._amount_out));
                this._addColumnTop(_g.d.ic_resource._net, this._findColumnByName(_g.d.ic_resource._balance_qty), this._findColumnByName(_g.d.ic_resource._balance_amount));
            }
            //
            this._setColumnBackground(_g.d.ic_resource._qty_in, Color.AliceBlue);
            this._setColumnBackground(_g.d.ic_resource._average_cost_in, Color.AliceBlue);
            this._setColumnBackground(_g.d.ic_resource._amount_in, Color.AliceBlue);
            //
            this._setColumnBackground(_g.d.ic_resource._balance_qty, Color.AliceBlue);
            this._setColumnBackground(_g.d.ic_resource._average_cost, Color.AliceBlue);
            this._setColumnBackground(_g.d.ic_resource._average_cost_end, Color.AliceBlue);
            this._setColumnBackground(_g.d.ic_resource._balance_amount, Color.AliceBlue);
            //
            this._total_show = true;
            this._calcPersentWidthToScatter();
            this._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_stkBalanceWareHouseGrid__beforeDisplayRendering);
            this._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_stkBalanceWareHouseGrid__mouseDoubleClick);
        }

        void _stkBalanceWareHouseGrid__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __itemCode = this._cellGet(e._row, _g.d.ic_resource._ic_code).ToString();
            SMLERPControl._icBalanceForm __icBalance = new SMLERPControl._icBalanceForm();
            __icBalance._load(__itemCode);
            __icBalance.ShowDialog();
            __icBalance.Dispose();
        }

        MyLib.BeforeDisplayRowReturn _stkBalanceWareHouseGrid__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData, Graphics e)
        {
            string __itemCodeByGrid = this._cellGet(row, _g.d.ic_resource._ic_code).ToString();
            int __addr = this._itemListCode.BinarySearch(__itemCodeByGrid);
            if (__addr >= 0)
            {
                senderRow.newColor = (Color)this._itemListColor[__addr];
            }
            return senderRow;
        }

        public void _processNow(_g.g._productCostType costMode, string itemName, string codeBegin, string codeEnd, DateTime dateBegin, DateTime dateEnd, string warehouseSelected)
        {
            try
            {
                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                this._loadFromDataTable(__process._stkStockInfoAndBalanceByWareHouse(costMode, null, codeBegin, codeEnd, dateBegin, dateEnd, false, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามคลัง, warehouseSelected, "", true, false, itemName));
                //
                this._itemListCode.Clear();
                this._itemListColor.Clear();
                int __count = 0;
                for (int __loop = 0; __loop < this._rowData.Count; __loop++)
                {
                    string __itemCodeByGrid = this._cellGet(__loop, _g.d.ic_resource._ic_code).ToString();
                    int __addr = this._itemListCode.BinarySearch(__itemCodeByGrid);
                    if (__addr < 0)
                    {
                        this._itemListCode.Add(__itemCodeByGrid);
                        __count++;
                        switch (__count)
                        {
                            case 1: this._itemListColor.Add(Color.Green); break;
                            case 2: this._itemListColor.Add(Color.Blue); break;
                            case 3: this._itemListColor.Add(Color.Brown); break;
                            case 4: this._itemListColor.Add(Color.Black); break;
                            case 5: this._itemListColor.Add(Color.DarkRed); break;
                            case 6: this._itemListColor.Add(Color.RosyBrown);
                                __count = 0;
                                break;
                        }
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }
}
