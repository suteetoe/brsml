using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Xml;
using System.Net;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public partial class _stkMovement : UserControl
    {
        string __formatNumberQty = _g.g._getFormatNumberStr(1);
        string __formatNumberPrice = _g.g._getFormatNumberStr(2);
        string __formatNumberAmount = _g.g._getFormatNumberStr(3);
        private SMLERPControl._selectWarehouseAndLocationForm _selectWarehouseAndLocation = null;
        private _g.g._productCostType _costMode = 0;

        /// <summary>
        /// รางานบัญชีคุมพิเศษ
        /// </summary>
        /// <param name="mode"></param>
        public _stkMovement(_g.g._productCostType costMode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._costMode = costMode;
            //
            if (costMode == _g.g._productCostType.ปรกติ || costMode == _g.g._productCostType.รวมต้นทุนแฝง)
            {
                this._resultGrid._columnTopActive = true;
            }
            this._resultGrid._table_name = _g.d.ic_resource._table;
            this._resultGrid._addColumn(_g.d.ic_resource._doc_date, 4, 10, 10, false, false, true, false, "dd/MM/yyyy");
            this._resultGrid._addColumn(_g.d.ic_resource._doc_time, 1, 10, 5);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_name, 1, 5, 5);
            this._resultGrid._addColumn(_g.d.ic_resource._doc_no, 1, 10, 15);
            this._resultGrid._addColumn(_g.d.ic_resource._warehouse, 1, 5, 5);
            this._resultGrid._addColumn(_g.d.ic_resource._location, 1, 5, 5);
            this._resultGrid._addColumn(_g.d.ic_resource._ic_unit_code, 1, 5, 5);
            this._resultGrid._addColumn(_g.d.ic_resource._qty_in, 3, 10, 8, false, false, false, false, __formatNumberQty);

            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                if (costMode == _g.g._productCostType.ปรกติ || costMode == _g.g._productCostType.รวมต้นทุนแฝง)
                {
                    this._resultGrid._addColumn(_g.d.ic_resource._cost_in, 3, 10, 8, false, false, false, false, __formatNumberPrice);
                    this._resultGrid._addColumn(_g.d.ic_resource._amount_in, 3, 10, 10, false, false, false, false, __formatNumberAmount);
                }
            }

            this._resultGrid._addColumn(_g.d.ic_resource._qty_out, 3, 10, 8, false, false, false, false, __formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                if (costMode == _g.g._productCostType.ปรกติ || costMode == _g.g._productCostType.รวมต้นทุนแฝง)
                {
                    this._resultGrid._addColumn(_g.d.ic_resource._cost_out, 3, 10, 8, false, false, false, false, __formatNumberPrice);
                    this._resultGrid._addColumn(_g.d.ic_resource._amount_out, 3, 10, 10, false, false, false, false, __formatNumberAmount);
                }
            }

            this._resultGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                if (costMode == _g.g._productCostType.ปรกติ || costMode == _g.g._productCostType.รวมต้นทุนแฝง)
                {
                    this._resultGrid._addColumn(_g.d.ic_resource._average_cost, 3, 10, 8, false, false, false, false, __formatNumberPrice);
                    this._resultGrid._addColumn(_g.d.ic_resource._amount, 3, 10, 10, false, false, false, false, __formatNumberAmount);
                }
            }
            //
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                if (costMode == _g.g._productCostType.ปรกติ || costMode == _g.g._productCostType.รวมต้นทุนแฝง)
                {
                    this._resultGrid._addColumnTop(_g.d.ic_resource._in, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_in), this._resultGrid._findColumnByName(_g.d.ic_resource._amount_in));
                    this._resultGrid._addColumnTop(_g.d.ic_resource._out, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_out), this._resultGrid._findColumnByName(_g.d.ic_resource._amount_out));
                    this._resultGrid._addColumnTop(_g.d.ic_resource._amount, this._resultGrid._findColumnByName(_g.d.ic_resource._balance_qty), this._resultGrid._findColumnByName(_g.d.ic_resource._amount));
                }
            }
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._qty_in, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._cost_in, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._amount_in, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._balance_qty, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._average_cost, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._amount, Color.AliceBlue);
            //
            this._resultGrid._calcPersentWidthToScatter();
            this._stkBalanceWareHouse._isEdit = false;
            //
            if (_g.g._companyProfile._cost_by_warehouse || costMode == _g.g._productCostType.เคลื่อนไหวสินค้า)
            {
                ToolStripButton __wh = new ToolStripButton();
                __wh.Text = "เลือกคลัง/ที่เก็บสินค้า";
                this._selectWarehouseAndLocation = new SMLERPControl._selectWarehouseAndLocationForm(true);
                if (costMode == _g.g._productCostType.ปรกติ || costMode == _g.g._productCostType.รวมต้นทุนแฝง)
                {
                    this._selectWarehouseAndLocation._locationGrid.Enabled = false;
                }
                __wh.Click += (s1, e1) =>
                {
                    this._selectWarehouseAndLocation.ShowDialog();
                };
                this._selectWarehouseAndLocation._closeButton.Click += (s1, e1) =>
                {
                    this._selectWarehouseAndLocation.Close();
                };
                this.toolStrip1.Items.Add(__wh);
                this._typeComboBox.SelectedIndexChanged += (s1, e1) =>
                {
                    switch (this._typeComboBox.SelectedIndex)
                    {
                        case 0: // ตามสินค้า
                            __wh.Visible = false;
                            break;
                        case 1: // ตามคลังสินค้า
                            __wh.Visible = true;
                            this._selectWarehouseAndLocation._whGrid.Enabled = true;
                            this._selectWarehouseAndLocation._locationGrid.Enabled = false;
                            break;
                        case 2: // ตามที่เก็บสินค้า
                            __wh.Visible = true;
                            this._selectWarehouseAndLocation._whGrid.Enabled = false;
                            this._selectWarehouseAndLocation._locationGrid.Enabled = true;
                            break;
                    }
                };
                this._typeComboBox.SelectedIndex = 0;
            }
            else
            {
                this._typeComboBox.Visible = false;
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void _process(string itemCode, string dateBegin, string dateEnd)
        {
            try
            {
                string __warehouseSelected = "";
                if (_g.g._companyProfile._cost_by_warehouse || (this._costMode == _g.g._productCostType.เคลื่อนไหวสินค้า && this._typeComboBox.SelectedIndex == 1))
                {
                    __warehouseSelected = this._selectWarehouseAndLocation._wareHouseSelected();
                    if (__warehouseSelected.Length == 0)
                    {
                        MessageBox.Show("กรุณาเลือกคลังสินค้า");
                    }
                }
                this._stkBalanceWareHouse._clear();
                this._stkBalanceLocation._clear();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                string __dateBegin = MyLib._myGlobal._convertDateToQuery(dateBegin);
                string __dateEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);
                string __dateBeginQuery = __dateBegin;
                if (this._costMode == _g.g._productCostType.เคลื่อนไหวสินค้า)
                {
                    if (this._typeComboBox.SelectedIndex == 1 || this._typeComboBox.SelectedIndex == 2)
                    {
                        // ตาม Location
                        __dateBeginQuery = "1998-1-1";
                    }
                }
                string __result = __smlFrameWork._process_stock_cost(MyLib._myGlobal._databaseName, itemCode, (this._costMode == _g.g._productCostType.ปรกติ) ? 1 : 11, __dateBeginQuery, __dateEnd);

                DataSet __dataResult = MyLib._myGlobal._convertStringToDataSet(__result);
                if (__dataResult.Tables.Count > 0)
                {
                    DataTable __data = __dataResult.Tables[0];
                    if (__data.Rows.Count > 0)
                    {
                        if (_g.g._companyProfile._cost_by_warehouse || (this._costMode == _g.g._productCostType.เคลื่อนไหวสินค้า && this._typeComboBox.SelectedIndex != 0))
                        {
                            try
                            {
                                DataTable __dataFilter = __data.Clone();
                                string __query = "warehouse in (" + __warehouseSelected + ")";
                                if (this._costMode == _g.g._productCostType.เคลื่อนไหวสินค้า && this._typeComboBox.SelectedIndex == 2)
                                {
                                    __query = this._selectWarehouseAndLocation._wareHouseLocationSelected("warehouse", "location");
                                }
                                decimal __balanceQty = 0M;
                                DataRow[] __dataRow = __data.Select(__query, "row");
                                foreach (DataRow __dr in __dataRow)
                                {
                                    if (this._costMode == _g.g._productCostType.เคลื่อนไหวสินค้า && (this._typeComboBox.SelectedIndex == 1 || this._typeComboBox.SelectedIndex == 2))
                                    {
                                        decimal __qtyIn = MyLib._myGlobal._decimalPhase(__dr[_g.d.ic_resource._qty_in].ToString());
                                        decimal __qtyOut = MyLib._myGlobal._decimalPhase(__dr[_g.d.ic_resource._qty_out].ToString());
                                        __balanceQty += __qtyIn - __qtyOut;
                                        __dr[_g.d.ic_resource._balance_qty] = __balanceQty;
                                        string __docDate = __dr[_g.d.ic_resource._doc_date].ToString();
                                        if (__docDate.CompareTo(__dateBegin) >= 0)
                                        {
                                            __dataFilter.ImportRow(__dr);
                                        }
                                    }
                                    else
                                    {
                                        __dataFilter.ImportRow(__dr);
                                    }
                                }
                                this._resultGrid._loadFromDataTable(__dataFilter);
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            this._resultGrid._loadFromDataTable(__data);
                        }
                    }
                    else
                    {
                        this._resultGrid._clear();
                    }
                    this._stkBalanceWareHouse._processNow(this._costMode, "", itemCode, itemCode, MyLib._myGlobal._convertDate(dateBegin), MyLib._myGlobal._convertDate(dateEnd), "");
                    this._stkBalanceLocation._processNow(this._costMode, "", itemCode, itemCode, MyLib._myGlobal._convertDate(dateBegin), MyLib._myGlobal._convertDate(dateEnd), "", "");
                    __data.Dispose();

                }
                else
                {
                    MessageBox.Show(__result);
                }
                __dataResult.Dispose();
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._process(_conditionScreen._getDataStr(_g.d.ic_resource._ic_code), _conditionScreen._getDataStr(_g.d.ic_resource._date_begin), _conditionScreen._getDataStr(_g.d.ic_resource._date_end));
        }
    }

    public class _stkMovementConditionScreen : MyLib._myScreen
    {
        MyLib._searchDataFull _searchItem = new MyLib._searchDataFull();

        public _stkMovementConditionScreen()
        {
            this._table_name = _g.d.ic_resource._table;
            this._maxColumn = 2;
            this._addTextBox(0, 0, 0, 0, _g.d.ic_resource._ic_code, 1, 25, 1, true, false, false);
            this._addTextBox(0, 1, 0, 0, _g.d.ic_resource._ic_name, 1, 25, 0, true, false, false);
            this._addTextBox(1, 0, 0, 0, _g.d.ic_resource._unit_code, 1, 25, 0, true, false, false);
            this._addDateBox(2, 0, 0, 0, _g.d.ic_resource._date_begin, 1, true);
            this._addDateBox(2, 1, 0, 0, _g.d.ic_resource._date_end, 1, true);
            //
            this._getControl(_g.d.ic_resource._ic_name).Enabled = false;
            this._getControl(_g.d.ic_resource._unit_code).Enabled = false;
            //
            DateTime __today = DateTime.Now;

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
            {
                this._setDataDate(_g.d.ic_resource._date_begin, new DateTime(__today.Year, __today.Month, 1));
            }
            else
                this._setDataDate(_g.d.ic_resource._date_begin, new DateTime(__today.Year, __today.Month, 1).AddMonths(-12));

            this._setDataDate(_g.d.ic_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
            //
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_stkMovementConditionScreen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_stkMovementConditionScreen__textBoxChanged);
            //
            this._searchItem._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchItem._dataList._refreshData();
            this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
        }

        void _stkMovementConditionScreen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_resource._ic_code))
            {
                try
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getItem = __myFrameWork._queryShort("select " + _g.d.ic_inventory._name_1 + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._getDataStr(_g.d.ic_resource._ic_code) + "\'").Tables[0];
                    string __itemName = "";
                    string __unitCost = "";
                    if (__getItem.Rows.Count > 0)
                    {
                        __itemName = __getItem.Rows[0][_g.d.ic_inventory._name_1].ToString();
                        __unitCost = __getItem.Rows[0][_g.d.ic_inventory._unit_cost].ToString();
                    }
                    this._setDataStr(_g.d.ic_resource._ic_name, __itemName);
                    this._setDataStr(_g.d.ic_resource._unit_code, __unitCost);
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchItem.Close();
            string __itemCode = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            this._setDataStr(_g.d.ic_resource._ic_code, __itemCode);
            SendKeys.Send("{TAB}");
        }

        void _stkMovementConditionScreen__textBoxSearch(object sender)
        {
            this._searchItem.StartPosition = FormStartPosition.CenterScreen;
            this._searchItem.ShowDialog();
        }
    }
}
