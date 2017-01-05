using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLBarcodeManage
{
    public partial class _itemBarcodeCheckerEan13 : UserControl
    {
        public _itemBarcodeCheckerEan13()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new System.Drawing.Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._selectedGid._table_name = _g.d.ic_inventory_barcode._table;
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._barcode, 1, 20, 20);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._ic_code, 1, 20, 20);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._description, 1, 30, 30);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._unit_code, 1, 10, 20);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._barcode_new, 1, 20, 20);
            this._selectedGid._calcPersentWidthToScatter();
            this._selectedGid._isEdit = false;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._selectedGid._clear();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __data = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory_barcode._barcode + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "<>\'\' and " + _g.d.ic_inventory_barcode._barcode + " is not null order by " + _g.d.ic_inventory_barcode._ic_code).Tables[0];
            StringBuilder __barcodeError = new StringBuilder();
            for (int __loop = 0; __loop < __data.Rows.Count; __loop++)
            {
                string __barcode = __data.Rows[__loop][0].ToString();
                if (__barcode.Length == 13)
                {
                    decimal __numberCheck = MyLib._myGlobal._decimalPhase(__barcode);

                    if (__numberCheck > 0)
                    {
                        string __newBarCode = MyLib._myUtil._genBarCodeEan13(__barcode.Substring(0, 12));
                        if (__barcode.Equals(__newBarCode) == false)
                        {
                            if (__barcodeError.Length > 0)
                            {
                                __barcodeError.Append(",");
                            }
                            __barcodeError.Append("\'" + __barcode + "\'");
                        }
                    }
                    else
                    {
                        __barcodeError.Append("\'" + __barcode + "\'");
                    }
                }
            }
            if (__barcodeError.Length > 0)
            {
                this._selectedGid._loadFromDataTable(__myFrameWork._queryStream(MyLib._myGlobal._databaseName, "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory_barcode._barcode, _g.d.ic_inventory_barcode._ic_code, _g.d.ic_inventory_barcode._description, _g.d.ic_inventory_barcode._unit_code) + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + " in (" + __barcodeError.ToString() + ")").Tables[0]);
            }
            MessageBox.Show("Success");
        }

        private string _generateRandomNumber(int count)
        {
            StringBuilder builder = new StringBuilder();

            Random __ran = new Random();
            for (int i = 0; i < count; i++)
            {
                builder.Append(__ran.Next(10));
            }

            return builder.ToString();
        }

        public string _genBarcode(string startDigit)
        {
            startDigit = startDigit.Trim();
            string __barcode = this._generateRandomNumber(12);
            __barcode = startDigit + __barcode.Remove(0, startDigit.Length);
            return MyLib._myUtil._genBarCodeEan13(__barcode);
        }

        private void _changeButton_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __data = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory_barcode._barcode + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "<>\'\' and " + _g.d.ic_inventory_barcode._barcode + " is not null order by " + _g.d.ic_inventory_barcode._barcode).Tables[0];
            for (int __row = 0; __row < this._selectedGid._rowData.Count; __row++)
            {
                while (true)
                {
                    string __newBarCode = "";
                    Boolean __found = true;
                    while (__found == true) 
                    {
                        __found = false;
                        __newBarCode = this._genBarcode(this._startDigitTextBox.Text);
                        for (int __find = 0; __find < this._selectedGid._rowData.Count; __find++)
                        {
                            if (this._selectedGid._cellGet(__find, _g.d.ic_inventory_barcode._barcode_new).ToString().Equals(__newBarCode))
                            {
                                __found = true;
                                break;
                            }
                        }
                    } 
                    if (__data.Select(_g.d.ic_inventory_barcode._barcode + "=\'" + __newBarCode + "\'").Length == 0)
                    {
                        this._selectedGid._cellUpdate(__row, _g.d.ic_inventory_barcode._barcode_new, __newBarCode, false);
                        break;
                    }
                }
            }
            this._selectedGid.Invalidate();
            MessageBox.Show("Success");
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __row = 0; __row < this._selectedGid._rowData.Count; __row++)
            {
                string __oldBarCode = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._barcode).ToString();
                string __newBarCode = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._barcode_new).ToString();
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory_barcode._table + " set " + _g.d.ic_inventory_barcode._barcode + "=\'" + __newBarCode + "\' where " + _g.d.ic_inventory_barcode._barcode + "=\'" + __oldBarCode + "\'"));
            }
            __myQuery.Append("</node>");
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__result.Length == 0)
            {
                MessageBox.Show("Success");
                this._selectedGid._clear();
            }
            else
            {
                MessageBox.Show(__result.ToString());
            }
        }
    }
}
