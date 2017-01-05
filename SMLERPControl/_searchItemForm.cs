using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl
{
    public class _searchItemForm : MyLib._searchDataFull
    {
        public delegate void SelectedHandler(string itemCode, string wareHouse, string location, int unitType);
        public event SelectedHandler _selected;
        //
        private _barcodeSearchForm _barcodeSearch;
        private Boolean _extraSearch = false;
        public string _whCode = "";
        public string _locationCode = "";

        public _searchItemForm(Boolean extraSearch)
        {
            this._extraSearch = extraSearch;
            this._dataList._gridData._message = ((extraSearch) ? "F3=เลือกตามคลัง,พื้นที่เก็บ,F4=เลือกตามคลัง,พื้นที่เก็บ,หน่วยนับ," : "") + "F8=แสดงยอดคงเหลือตามคลังสินค้า";
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
            {
                this._dataList._loadViewFormat("screen_mini_ic_inventory", MyLib._myGlobal._userSearchScreenGroup, false);
            }
            else
            {
                this._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);
            }
            this.WindowState = FormWindowState.Maximized;
            this.FormClosed += new FormClosedEventHandler(_searchInventoryDialog_FormClosed);
        }

        void _searchInventoryDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this._barcodeSearch != null)
                {
                    this._barcodeSearch.Dispose();
                }
            }
            catch
            {
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                Keys __keyCode = (Keys)(int)keyData & Keys.KeyCode;
                if ((keyData & Keys.Control) == Keys.Control && (__keyCode == Keys.B))
                {
                    this._barcodeSearch = new _barcodeSearchForm();
                    this._barcodeSearch.TopMost = true;
                    this._barcodeSearch._textBoxBarcode.KeyDown += (s, e) =>
                    {
                        if (e.KeyData == Keys.Enter)
                        {
                            try
                            {
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                string __query = "select " + _g.d.ic_inventory_barcode._ic_code + " from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._barcode) + "=\'" + _barcodeSearch._textBoxBarcode.Text.Trim().ToUpper() + "\'";
                                DataTable __findItem = __myFrameWork._queryShort(__query).Tables[0];
                                if (__findItem.Rows.Count > 0)
                                {
                                    string __itemCode = __findItem.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString();
                                    this._dataList._searchText.textBox.Text = __itemCode;
                                    this._dataList._refreshData();
                                }
                                this._barcodeSearch._labelBarcode.Text = _barcodeSearch._textBoxBarcode.Text;
                                this._barcodeSearch._textBoxBarcode.Text = "";
                                e.Handled = true;
                            }
                            catch
                            {
                            }
                        }
                    };
                    this._barcodeSearch.Show();
                }
                else
                {
                    switch (__keyCode)
                    {
                        case Keys.F3:
                        case Keys.F4:
                            if (this._extraSearch)
                            {
                                {
                                    string __itemCode = this._dataList._gridData._cellGet(this._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString().ToUpper();
                                    string __itemName = this._dataList._gridData._cellGet(this._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString().ToUpper();
                                    _searchItemByWarehouseForm __wareHouse = new _searchItemByWarehouseForm(__itemCode, __itemName);
                                    __wareHouse._selected += (string wareHouse, string location, int unitType) =>
                                    {
                                        this.Close();
                                        this._selected(__itemCode, wareHouse, location, (__keyCode == Keys.F4) ? unitType : 0);
                                    };
                                    __wareHouse.ShowDialog();
                                }
                                return true;
                            }
                            break;
                        case Keys.F8:
                            {
                                try
                                {
                                    string __itemCode = this._dataList._gridData._cellGet(this._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString().ToUpper();
                                    _icBalanceForm __icBalance = new _icBalanceForm();
                                    __icBalance._load(__itemCode);
                                    __icBalance.ShowDialog();
                                    __icBalance.Close();
                                }
                                catch
                                {
                                }
                            }
                            return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
