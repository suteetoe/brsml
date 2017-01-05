using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPARControl
{
    public class _payOtherCurrencyGridControl : MyLib._myGrid
    {
        public delegate void _refreshDataEvent(_payOtherCurrencyGridControl sender);
        public event _refreshDataEvent _refreshData;

        /// <summary>
        /// doc_type = 19
        /// </summary>
        public _payOtherCurrencyGridControl()
        {
            this._table_name = _g.d.cb_trans_detail._table;
            this._width_by_persent = true;

            //
            this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 10, 10, true, false, true, true, "", "", "", _g.d.cb_trans_detail._currency_code); // currency code
            this._addColumn(_g.d.cb_trans_detail._description, 1, 20, 20, false, false, false, false, "", "", "", _g.d.cb_trans_detail._currency_name); // currency name
            this._addColumn(_g.d.cb_trans_detail._exchange_rate, 3, 10, 10, false, false, true, false, "m04");
            this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 10, true, false, true, false, "m04");
            this._total_show = true;
            this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 20, 20, false, false, true, false, "m04");

            //
            this._calcPersentWidthToScatter();

            this._clickSearchButton += _payOtherCurrencyGridControl__clickSearchButton;
            this._alterCellUpdate += _payOtherCurrencyGridControl__alterCellUpdate;
            this._queryForInsertCheck += _payOtherCurrencyGridControl__queryForInsertCheck;
        }

        private bool _payOtherCurrencyGridControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            string __getCurrencyCode = this._cellGet(row, _g.d.cb_trans_detail._trans_number).ToString();
            decimal __getAmount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._amount).ToString());
            decimal __getExchangeRate = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._exchange_rate).ToString());

            if (__getCurrencyCode.Length > 0 && __getAmount > 0 && __getExchangeRate != 0)
                return true;

            return false;
        }

        private void _payOtherCurrencyGridControl__alterCellUpdate(object sender, int row, int column)
        {
            // calc sum amount
            if (column == this._findColumnByName(_g.d.cb_trans_detail._trans_number))
            {
                string __currencyCode = this._cellGet(row, _g.d.cb_trans_detail._trans_number).ToString();
                if (__currencyCode.Length > 0)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getData = __myFrameWork._queryShort("select " + _g.d.erp_currency._name_1 + "," + _g.d.erp_currency._exchange_rate_present + " from " + _g.d.erp_currency._table + " where " + _g.d.erp_currency._code + "=\'" + __currencyCode.ToUpper() + "\'").Tables[0];
                    if (__getData.Rows.Count > 0)
                    {
                        string __name1 = __getData.Rows[0][_g.d.erp_currency._name_1].ToString();
                        decimal __exchangeRate = MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.erp_currency._exchange_rate_present].ToString());

                        this._cellUpdate(row, _g.d.cb_trans_detail._description, __name1, true);
                        this._cellUpdate(row, _g.d.cb_trans_detail._exchange_rate, __exchangeRate, true);
                    }
                }
            }
            else if (column == this._findColumnByName(_g.d.cb_trans_detail._amount))
            {
                decimal __exchangeRate = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._exchange_rate).ToString());
                decimal __amount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._amount).ToString());

                decimal __sumAmount = __amount * __exchangeRate;

                this._cellUpdate(row, _g.d.cb_trans_detail._sum_amount, __sumAmount, true);

            }

            if (this._refreshData != null)
            {
                this._refreshData(this);
            }
        }

        private void _payOtherCurrencyGridControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            // search currency code
            if (e._columnName.Equals(_g.d.cb_trans_detail._trans_number))
            {
                MyLib._searchDataFull __searchCurrency = new MyLib._searchDataFull();
                __searchCurrency.StartPosition = FormStartPosition.CenterScreen;
                __searchCurrency._dataList._loadViewFormat(_g.g._search_screen_erp_currency, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchCurrency._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        string __getIcCode = __searchCurrency._dataList._gridData._cellGet(__searchCurrency._dataList._gridData._selectRow, _g.d.erp_currency._table + "." + _g.d.erp_currency._code).ToString();
                        this._cellUpdate(e._row, _g.d.cb_trans_detail._trans_number, __getIcCode, true);
                        SendKeys.Send("{TAB}");
                        __searchCurrency.Close();
                    }

                };

                __searchCurrency._searchEnterKeyPress += (s1, e1) =>
                {
                    if (e1 != -1)
                    {
                        string __getIcCode = __searchCurrency._dataList._gridData._cellGet(__searchCurrency._dataList._gridData._selectRow, _g.d.erp_currency._table + "." + _g.d.erp_currency._code).ToString();
                        this._cellUpdate(e._row, _g.d.cb_trans_detail._trans_number, __getIcCode, true);
                        SendKeys.Send("{TAB}");
                        __searchCurrency.Close();
                    }
                };
                __searchCurrency.ShowDialog();
            }
        }
    }
}
