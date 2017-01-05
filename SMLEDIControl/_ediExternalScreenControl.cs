using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLEDIControl
{
    public partial class _ediExternalScreenControl : UserControl
    {
        public _ediExternalScreenControl()
        {
            InitializeComponent();

            this._build();
        }

        void _build()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                // screen top
                int __row = 0;

                this._screenTop._table_name = _g.d.edi_external._table;
                this._screenTop._maxColumn = 1;
                this._screenTop._addTextBox(__row++, 0, 1, 1, _g.d.edi_external._code, 1, 25, 0, true, false, false);
                this._screenTop._addTextBox(__row++, 0, 1, 1, _g.d.edi_external._name_1, 1, 25, 0, true, false, false);
                //this._screenTop._addTextBox(__row++, 0, _g.d.edi_external._trading_partner_id, 25);
                this._screenTop._addTextBox(__row++, 0, _g.d.edi_external._supplier_ean_code, 25);
                this._screenTop._addTextBox(__row++, 0, 1, 1, _g.d.edi_external._ar_code, 1, 25, 1, true, false);
                //this._screenTop._addTextBox(__row++, 0, _g.d.edi_external._dc_code, 25);
                this._screenTop._addComboBox(__row++, 0, _g.d.edi_external._export_format, true, new string[] { _g.d.edi_external._format_0, _g.d.edi_external._format_1, _g.d.edi_external._format_2 }, false);
                this._screenTop._textBoxSearch += _screenTop__textBoxSearch;
                this._screenTop._textBoxChanged += _screenTop__textBoxChanged;

                // product grid
                this._gridProduct._table_name = _g.d.edi_product_list._table;
                this._gridProduct._addColumn(_g.d.edi_product_list._item_code, 1, 10, 10);
                this._gridProduct._addColumn(_g.d.edi_product_list._item_name, 1, 20, 20);
                this._gridProduct._addColumn(_g.d.edi_product_list._ic_code, 1, 20, 20, true, false, true, true);
                this._gridProduct._addColumn(_g.d.edi_product_list._ic_name, 1, 20, 20, false, false, false);
                this._gridProduct._addColumn(_g.d.edi_product_list._item_packsize, 2, 10, 10, true, false, true);
                //this._gridProduct._addColumn(this._gridProduct._rowNumberName, 2, 0, 15, false, true, true);
                this._gridProduct.WidthByPersent = true;
                this._gridProduct._calcPersentWidthToScatter();
                this._gridProduct._clickSearchButton += _grid__clickSearchButton;
                this._gridProduct._alterCellUpdate += _gridProduct__alterCellUpdate;
                //this._gridProduct._queryForUpdateWhere += _gridProduct__queryForUpdateWhere;
                //this._gridProduct._queryForUpdateCheck += _gridProduct__queryForUpdateCheck;
                //this._gridProduct._queryForInsertCheck += _gridProduct__queryForInsertCheck;
                //this._gridProduct._queryForRowRemoveCheck += _gridProduct__queryForRowRemoveCheck;
                //this._gridProduct._queryForInsertPerRow += _gridProduct__queryForInsertPerRow;

                // unit grid
                this._gridUnit._table_name = _g.d.edi_unit_list._table;
                this._gridUnit._addColumn(_g.d.edi_unit_list._edi_unit_code, 1, 20, 20);
                this._gridUnit._addColumn(_g.d.edi_unit_list._edi_unit_name, 1, 20, 20);
                this._gridUnit._addColumn(_g.d.edi_unit_list._unit_code, 1, 20, 20, true, false, true, true);
                this._gridUnit._addColumn(_g.d.edi_unit_list._unit_name, 1, 20, 20, false, false, false);
                //this._gridUnit._addColumn(this._gridUnit._rowNumberName, 2, 0, 15, false, true, true);
                this._gridUnit.WidthByPersent = true;
                this._gridUnit._calcPersentWidthToScatter();
                this._gridUnit._clickSearchButton += _grid__clickSearchButton;
                this._gridUnit._alterCellUpdate += _gridUnit__alterCellUpdate;
                //this._gridUnit._queryForUpdateWhere += _gridProduct__queryForUpdateWhere;
                //this._gridUnit._queryForUpdateCheck += _gridProduct__queryForUpdateCheck;
                //this._gridUnit._queryForInsertCheck += _gridProduct__queryForInsertCheck;
                //this._gridUnit._queryForRowRemoveCheck += _gridProduct__queryForRowRemoveCheck;
                //this._gridUnit._queryForInsertPerRow += _gridProduct__queryForInsertPerRow;

                // barcode grid
                this._gridBarcode._table_name = _g.d.edi_barcode_list._table;
                this._gridBarcode._addColumn(_g.d.edi_barcode_list._edi_barcode, 1, 20, 20);
                this._gridBarcode._addColumn(_g.d.edi_barcode_list._barcode, 1, 20, 20, true, false, true, true);
                //this._gridBarcode._addColumn(this._gridBarcode._rowNumberName, 2, 0, 15, false, true, true);
                this._gridBarcode.WidthByPersent = true;
                this._gridBarcode._calcPersentWidthToScatter();
                this._gridBarcode._clickSearchButton += _grid__clickSearchButton;
                this._gridBarcode._alterCellUpdate += _gridBarcode__alterCellUpdate;
                //this._gridBarcode._queryForUpdateWhere += _gridProduct__queryForUpdateWhere;
                //this._gridBarcode._queryForUpdateCheck += _gridProduct__queryForUpdateCheck;
                //this._gridBarcode._queryForInsertCheck += _gridProduct__queryForInsertCheck;
                //this._gridBarcode._queryForRowRemoveCheck += _gridProduct__queryForRowRemoveCheck;
                //this._gridBarcode._queryForInsertPerRow += _gridProduct__queryForInsertPerRow;

                this._gridCustomer._table_name = _g.d.edi_ar_list._table;
                this._gridCustomer._addColumn(_g.d.edi_ar_list._ar_code, 1, 20, 20, true, false, true, true);
                this._gridCustomer._addColumn(_g.d.edi_ar_list._ar_name, 1, 80, 80, false, false, false);
                this._gridCustomer.WidthByPersent = true;
                this._gridCustomer._calcPersentWidthToScatter();
                this._gridCustomer._clickSearchButton += _grid__clickSearchButton;
                this._gridCustomer._alterCellUpdate += _gridCustomer__alterCellUpdate;

            }
        }

        private void _gridCustomer__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._gridCustomer._findColumnByName(_g.d.edi_ar_list._ar_code))
            {
                string __getICCode = this._gridCustomer._cellGet(row, column).ToString();
                int __row = this._gridCustomer._findData(column, __getICCode);
                if (__row != -1 && __row != row)
                {
                    MessageBox.Show(__getICCode + " : ใช้ไปแล้ว");
                    this._gridCustomer._cellUpdate(row, column, "", false);
                    this._gridCustomer._cellUpdate(row, _g.d.edi_ar_list._ar_name, "", false);
                    this._gridCustomer.Invalidate();
                }

            }
        }

        private void _gridBarcode__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._gridBarcode._findColumnByName(_g.d.edi_barcode_list._barcode))
            {
                string __getICCode = this._gridBarcode._cellGet(row, column).ToString();
                int __row = this._gridBarcode._findData(column, __getICCode);
                if (__row != -1 && __row != row)
                {
                    MessageBox.Show(__getICCode + " : ใช้ไปแล้ว");
                    this._gridBarcode._cellUpdate(row, column, "", false);
                    this._gridBarcode.Invalidate();
                }

            }
        }

        private void _gridUnit__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._gridUnit._findColumnByName(_g.d.edi_unit_list._unit_code))
            {
                string __getICCode = this._gridUnit._cellGet(row, column).ToString();
                int __row = this._gridUnit._findData(column, __getICCode);
                if (__row != -1 && __row != row)
                {
                    MessageBox.Show(__getICCode + " : ใช้ไปแล้ว");
                    this._gridUnit._cellUpdate(row, column, "", false);
                    this._gridUnit.Invalidate();
                }

            }
        }

        private void _gridProduct__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._gridProduct._findColumnByName(_g.d.edi_product_list._item_code))
            {
                /*string __getItemCode = this._gridProduct._cellGet(row, column).ToString();
                int __row = this._gridProduct._findData(column, __getItemCode);
                if (__row != -1)
                {
                    MessageBox.Show(__getItemCode + " : ใช้ไปแล้ว");
                    this._gridProduct._cellUpdate(row, column, "", false);
                    this._gridProduct.Invalidate();
                }*/

            }
            else if (column == this._gridProduct._findColumnByName(_g.d.edi_product_list._ic_code))
            {
                string __getICCode = this._gridProduct._cellGet(row, column).ToString();
                int __row = this._gridProduct._findData(column, __getICCode);
                if (__row != -1 && __row != row)
                {
                    MessageBox.Show(__getICCode + " : ใช้ไปแล้ว");
                    this._gridProduct._cellUpdate(row, column, "", false);
                    this._gridProduct.Invalidate();
                }

            }
        }

        MyLib._searchDataFull _searchProduct;
        MyLib._searchDataFull _searchUnit;
        MyLib._searchDataFull _searchBarcode;
        MyLib._searchDataFull _searchAr;

        private void _grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myGrid __grid = (MyLib._myGrid)sender;

            if (__grid._table_name == _g.d.edi_product_list._table && e._columnName == _g.d.edi_product_list._ic_code)
            {
                if (this._searchProduct == null)
                {
                    this._searchProduct = new MyLib._searchDataFull();
                    this._searchProduct.StartPosition = FormStartPosition.CenterScreen;
                    this._searchProduct._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchProduct._dataList._gridData._mouseClick += (s1, e1) =>
                    {
                        if (e1._row != -1)
                        {
                            string __getIcCode = this._searchProduct._dataList._gridData._cellGet(this._searchProduct._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                            string __getICName = this._searchProduct._dataList._gridData._cellGet(this._searchProduct._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();

                            this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_code, __getIcCode, true);
                            this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_name, __getICName, true);
                            SendKeys.Send("{TAB}");

                            this._searchProduct.Close();

                        }

                    };

                    this._searchProduct._searchEnterKeyPress += (s1, e1) =>
                    {
                        if (e1 != -1)
                        {
                            string __getIcCode = this._searchProduct._dataList._gridData._cellGet(this._searchProduct._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                            string __getICName = this._searchProduct._dataList._gridData._cellGet(this._searchProduct._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();

                            this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_code, __getIcCode, true);
                            this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_name, __getICName, true);
                            SendKeys.Send("{TAB}");

                            this._searchProduct.Close();

                        }
                    };
                }
                this._searchProduct.ShowDialog();
            }
            else if (__grid._table_name == _g.d.edi_unit_list._table && e._columnName == _g.d.edi_unit_list._unit_code)
            {
                if (this._searchUnit == null)
                {
                    this._searchUnit = new MyLib._searchDataFull();
                    this._searchUnit.StartPosition = FormStartPosition.CenterScreen;
                    this._searchUnit._dataList._loadViewFormat(_g.g._search_master_ic_unit, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchUnit._dataList._gridData._mouseClick += (s1, e1) =>
                    {
                        if (e1._row != -1)
                        {
                            string __getIcCode = this._searchUnit._dataList._gridData._cellGet(this._searchUnit._dataList._gridData._selectRow, _g.d.ic_unit._table + "." + _g.d.ic_unit._code).ToString();
                            string __getICName = this._searchUnit._dataList._gridData._cellGet(this._searchUnit._dataList._gridData._selectRow, _g.d.ic_unit._table + "." + _g.d.ic_unit._name_1).ToString();

                            this._gridUnit._cellUpdate(this._gridUnit._selectRow, _g.d.edi_unit_list._unit_code, __getIcCode, true);
                            this._gridUnit._cellUpdate(this._gridUnit._selectRow, _g.d.edi_unit_list._unit_name, __getICName, true);
                            SendKeys.Send("{TAB}");

                            this._searchUnit.Close();

                        }

                    };

                    this._searchUnit._searchEnterKeyPress += (s1, e1) =>
                    {
                        if (e1 != -1)
                        {
                            string __getIcCode = this._searchUnit._dataList._gridData._cellGet(this._searchUnit._dataList._gridData._selectRow, _g.d.ic_unit._table + "." + _g.d.ic_unit._code).ToString();
                            string __getICName = this._searchUnit._dataList._gridData._cellGet(this._searchUnit._dataList._gridData._selectRow, _g.d.ic_unit._table + "." + _g.d.ic_unit._name_1).ToString();

                            this._gridUnit._cellUpdate(this._gridUnit._selectRow, _g.d.edi_unit_list._unit_code, __getIcCode, true);
                            this._gridUnit._cellUpdate(this._gridUnit._selectRow, _g.d.edi_unit_list._unit_name, __getICName, true);
                            SendKeys.Send("{TAB}");

                            this._searchUnit.Close();
                        }
                    };
                }
                this._searchUnit.ShowDialog();
            }
            else if (__grid._table_name == _g.d.edi_barcode_list._table && e._columnName == _g.d.edi_barcode_list._barcode)
            {
                if (this._searchBarcode == null)
                {
                    this._searchBarcode = new MyLib._searchDataFull();
                    this._searchBarcode.StartPosition = FormStartPosition.CenterScreen;
                    this._searchBarcode._dataList._loadViewFormat(_g.g._search_screen_ic_inventory_barcode, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchBarcode._dataList._gridData._mouseClick += (s1, e1) =>
                    {
                        if (e1._row != -1)
                        {
                            string __getIcCode = this._searchBarcode._dataList._gridData._cellGet(this._searchBarcode._dataList._gridData._selectRow, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
                            //string __getICName = this._searchBarcode._dataList._gridData._cellGet(this._searchProduct._dataList._gridData._selectRow, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory._name_1).ToString();

                            this._gridBarcode._cellUpdate(this._gridBarcode._selectRow, _g.d.edi_barcode_list._barcode, __getIcCode, true);
                            // this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_name, __getICName, true);
                            SendKeys.Send("{TAB}");

                            this._searchBarcode.Close();

                        }

                    };

                    this._searchBarcode._searchEnterKeyPress += (s1, e1) =>
                    {
                        if (e1 != -1)
                        {
                            string __getIcCode = this._searchBarcode._dataList._gridData._cellGet(this._searchBarcode._dataList._gridData._selectRow, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
                            //string __getICName = this._searchBarcode._dataList._gridData._cellGet(this._searchProduct._dataList._gridData._selectRow, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory._name_1).ToString();

                            this._gridBarcode._cellUpdate(this._gridBarcode._selectRow, _g.d.edi_barcode_list._barcode, __getIcCode, true);
                            // this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_name, __getICName, true);
                            SendKeys.Send("{TAB}");

                            this._searchBarcode.Close();
                        }
                    };
                }
                this._searchBarcode.ShowDialog();
            }
            else if (__grid._table_name == _g.d.edi_ar_list._table && e._columnName == _g.d.edi_ar_list._ar_code)
            {
                if (this._searchAr == null)
                {
                    this._searchAr = new MyLib._searchDataFull();
                    this._searchAr.StartPosition = FormStartPosition.CenterScreen;
                    this._searchAr._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, false);

                    this._searchAr._dataList._gridData._mouseClick += (s1, e1) =>
                    {
                        if (e1._row != -1)
                        {
                            string __getCode = this._searchAr._dataList._gridData._cellGet(this._searchAr._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                            string __getName = this._searchAr._dataList._gridData._cellGet(this._searchAr._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1).ToString();

                            this._gridCustomer._cellUpdate(this._gridCustomer._selectRow, _g.d.edi_ar_list._ar_code, __getCode, true);
                            this._gridCustomer._cellUpdate(this._gridCustomer._selectRow, _g.d.edi_ar_list._ar_name, __getName, true);
                            SendKeys.Send("{TAB}");

                            this._searchAr.Close();
                        }
                    };

                    this._searchAr._searchEnterKeyPress += (s1, e1) =>
                    {
                        if (e1 != -1)
                        {
                            string __getCode = this._searchAr._dataList._gridData._cellGet(this._searchAr._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                            string __getName = this._searchAr._dataList._gridData._cellGet(this._searchAr._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1).ToString();

                            this._gridCustomer._cellUpdate(this._gridCustomer._selectRow, _g.d.edi_ar_list._ar_code, __getCode, true);
                            this._gridCustomer._cellUpdate(this._gridCustomer._selectRow, _g.d.edi_ar_list._ar_name, __getName, true);
                            SendKeys.Send("{TAB}");

                            this._searchAr.Close();
                        }
                    };
                }
                this._searchAr.ShowDialog();

            }

        }

        private void _screenTop__textBoxChanged(object sender, string name)
        {
            if (name == _g.d.edi_external._ar_code)
                _search(true);
        }

        #region Event Grid Product

        private MyLib.QueryForInsertPerRowType _gridProduct__queryForInsertPerRow(MyLib._myGrid sender, int row)
        {
            MyLib.QueryForInsertPerRowType result = new MyLib.QueryForInsertPerRowType();
            result._field = "line_number";
            result._data = row.ToString();
            return (result);
        }

        private bool _gridProduct__queryForRowRemoveCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return true;
            }
            // เป็นค่าว่างหรือไม่ (ให้ลบออก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return true;
            }
            return false;
        }

        private bool _gridProduct__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            return true;
        }

        private bool _gridProduct__queryForUpdateCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            // ดูว่าเป็น row_number เป็น 0 หรือไม่ ถ้าเป็น 0 คือเป็นรายการใหม่ (ห้ามใช้คำสั่ง Update เดี๋ยวมันจะไป Insert ให้ต่อไป)
            if (((int)sender._cellGet(row, sender._rowNumberName)) == 0)
            {
                return false;
            }
            return true;
        }

        private string _gridProduct__queryForUpdateWhere(MyLib._myGrid sender, int row)
        {
            int __getInt = (int)sender._cellGet(row, sender._rowNumberName);
            return (sender._rowNumberName + "=" + __getInt.ToString());
        }

        #endregion

        private void _screenTop__textBoxSearch(object sender)
        {

            MyLib._myTextBox __textBox = (MyLib._myTextBox)sender;
            if (__textBox._name == _g.d.edi_external._ar_code)
            {
                // do search ar
                MyLib._searchDataFull __searhAR = new MyLib._searchDataFull();
                __searhAR._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, false);
                __searhAR.WindowState = FormWindowState.Maximized;
                __searhAR._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        string __arCode = __searhAR._dataList._gridData._cellGet(__searhAR._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                        __searhAR.Close();
                        __searhAR.Dispose();
                        this._screenTop._setDataStr(_g.d.edi_external._ar_code, __arCode);
                        SendKeys.Send("{TAB}");

                    }

                };

                __searhAR._searchEnterKeyPress += (s1, e1) =>
                {
                    if (e1 != -1)
                    {
                        string __arCode = __searhAR._dataList._gridData._cellGet(__searhAR._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                        __searhAR.Close();
                        __searhAR.Dispose();
                        this._screenTop._setDataStr(_g.d.edi_external._ar_code, __arCode);
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input", serialNumber);
                    SendKeys.Send("{TAB}");

                    }

                };

                __searhAR.ShowDialog(MyLib._myGlobal._mainForm);
            }
        }

        public void _search(bool warning)
        {
            MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();

            StringBuilder __queryList = new StringBuilder();
            __queryList.Append(MyLib._myGlobal._xmlHeader + "<node>");

            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, name_1 from ar_customer where code = \'" + this._screenTop._getDataStr(_g.d.edi_external._ar_code) + "\'"));

            __queryList.Append("</node>");


            ArrayList __result = __myFramework._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());

            if (__result.Count > 0)
            {
                DataTable __customer = ((DataSet)__result[0]).Tables[0];

                string __getData = "";
                if (__customer.Rows.Count > 0)
                {
                    __getData = __customer.Rows[0][_g.d.ar_customer._name_1].ToString();
                }
                string __getDatacode = this._screenTop._getDataStr(_g.d.edi_external._ar_code);

                this._screenTop._setDataStr(_g.d.edi_external._ar_code, __getDatacode, __getData, true);
                if (__customer.Rows.Count == 0 && warning)
                {
                    MessageBox.Show("ไม่พบข้อมูล : รหัสลูกค้า");
                    this._screenTop._setDataStr(_g.d.edi_external._ar_code, "", "", true);
                }
            }
        }
    }
}
