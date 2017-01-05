using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;

namespace SMLERPAR
{
    public partial class _ar_label_print : UserControl
    {
        public _ar_label_print()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._itemList._buttonDelete.Visible = false;
            this._itemList._buttonNew.Visible = false;
            this._itemList._buttonNewFromTemp.Visible = false;

            this._selectedGid._table_name = _g.d.ar_customer._table;
            this._selectedGid._addColumn(_g.d.ar_customer._code, 1, 20, 20);
            this._selectedGid._addColumn(_g.d.ar_customer._name_1, 1, 40, 40);
            this._selectedGid._addColumn(_g.d.ar_customer._address, 1, 40, 40);

            this._itemList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, true);
            this._itemList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._itemList._closeScreen += new MyLib.CloseEventHandler(_itemList__closeScreen);
            //this._priceComboBox.SelectedIndex = 0;
            //this._ltdNameTextBox.Text = MyLib._myGlobal._ltdName;

            _getDefaultPrinter();

        }

        void _itemList__closeScreen()
        {
            this.Dispose();
        }

        void _getDefaultPrinter()
        {
            int __default = 0;
            int __count = 0;
            _printerComboBox.Items.Clear();

            //foreach (ManagementObject __getPrinter in __printerList.Get())
            foreach (MyLib._printerListClass __getPrinter in MyLib._myGlobal._printerList)
            {
                string __printerName = __getPrinter._printerName;
                if (__getPrinter._isDefault)
                {
                    __default = __count;
                }
                _printerComboBox.Items.Add(__printerName);
                __count++;
            }
            _printerComboBox.SelectedIndex = __default;

        }

        private float _convertCentimeter(float centimeter)
        {
            return centimeter * 10F;
        }


        private void _print(Boolean preview)
        {
            List<_arDetail> __apCodeList = new List<_arDetail>();
            int __maxPage = 1;
            int __maxRow = (int)MyLib._myGlobal._decimalPhase(this._maxRowTextBox.Text);
            int __maxColumn = (int)MyLib._myGlobal._decimalPhase(this._maxColumnTextBox.Text);
            int __currentRow = (int)MyLib._myGlobal._decimalPhase(this._startRowTextBox.Text);
            int __currentColumn = (int)MyLib._myGlobal._decimalPhase(this._startColumnTextBox.Text);
            for (int __row = 0; __row < this._selectedGid._rowData.Count; __row++)
            {
                string __arCode = this._selectedGid._cellGet(__row, _g.d.ar_customer._code).ToString();
                if (__arCode.Trim().Length > 0)
                {
                    //int __label = (int)MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._stamp_count).ToString());
                    //if (__label <= 0) __label = 1;
                    //for (int __count = 0; __count < __label; __count++)
                    //{
                    if (__currentColumn > __maxColumn)
                    {
                        __currentRow++;
                        __currentColumn = 1;
                    }
                    if (__currentRow >= __maxRow + 1)
                    {
                        __maxPage++;
                        __currentRow = 1;
                        __currentColumn = 1;
                    }

                    _arDetail __data = new _arDetail();
                    __data._page = __maxPage;
                    __data._row = __currentRow;
                    __data._column = __currentColumn;
                    __data._ar_code = __arCode;
                    __data._ar_name = this._selectedGid._cellGet(__row, _g.d.ar_customer._name_1).ToString();
                    __data._address = this._selectedGid._cellGet(__row, _g.d.ar_customer._address).ToString();

                    //__data._name = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._description).ToString();
                    //string __priceField = "";
                    //switch (this._priceComboBox.SelectedIndex)
                    //{
                    //    case 0: __priceField = _g.d.ic_inventory_barcode._price; break;
                    //    case 1: __priceField = _g.d.ic_inventory_barcode._price_2; break;
                    //    case 2: __priceField = _g.d.ic_inventory_barcode._price_3; break;
                    //    case 3: __priceField = _g.d.ic_inventory_barcode._price_4; break;
                    //}
                    //__data._price = MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, __priceField).ToString());
                    //if (__data._price == 0)
                    //{
                    //    __data._price = MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._price).ToString());
                    //}
                    __apCodeList.Add(__data);
                    __currentColumn++;
                    //}
                }
            }

            PrintPreviewDialog __preview = new PrintPreviewDialog();
            PrintDocument __printDoc = new PrintDocument();
            //__printDoc.PrinterSettings.PrinterName = "Canon LBP5050";
            if (__printDoc.PrinterSettings.IsValid)
            {
                __printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                __preview.Document = __printDoc;
                float __leftMargin = (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._leftMarginTextBox.Text));
                float __topMargin = (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._topMarginTextBox.Text));
                float __labelWidth = this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._labelWidthTextBox.Text));
                float __labelHeight = this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._labelHeightTextBox.Text));
                int __currentPage = 0;
                __printDoc.PrintPage += (s1, e1) =>
                {
                    __currentPage++;
                    e1.PageSettings.Margins.Top = 0;
                    e1.PageSettings.Margins.Left = 0;
                    e1.PageSettings.Margins.Bottom = 0;
                    e1.PageSettings.Margins.Right = 0;
                    e1.Graphics.PageUnit = GraphicsUnit.Millimeter;
                    Graphics __g = e1.Graphics;
                    //__g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    int __findAddrTrial = 0;

                    //SMLBarcodeManage.Ean13 __barcodeEan13 = new Ean13();
                    for (int __row = 1; __row <= __maxRow; __row++)
                    {
                        for (int __column = 1; __column <= __maxColumn; __column++)
                        {
                            int __foundAddr = -1;
                            for (int __find = __findAddrTrial; __find < __apCodeList.Count; __find++)
                            {
                                if (__apCodeList[__find]._page == __currentPage && __apCodeList[__find]._row == __row && __apCodeList[__find]._column == __column)
                                {
                                    __foundAddr = __find;
                                    __findAddrTrial = __find + 1;
                                    break;
                                }
                            }
                            if (__foundAddr != -1)
                            {
                                int __x = (int)(((__column - 1) * __labelWidth) + __leftMargin);
                                int __y = (int)(((__row - 1) * __labelHeight) + __topMargin);
                                Font __font = new System.Drawing.Font("Tahome", 7);

                                //StringBuilder __head1 = new StringBuilder(__apCodeList[__foundAddr]._ar_code.Trim());
                                __g.DrawString(__apCodeList[__foundAddr]._ar_code.Trim(), __font, Brushes.Black, __x, __y);
                                __g.DrawString(__apCodeList[__foundAddr]._ar_name.Trim(), __font, Brushes.Black, __x, __y + (__labelHeight - 12));
                                //__g.DrawString(__apCodeList[__foundAddr]._ar_code.Trim(), __font, Brushes.Black, __x, __y + (__labelHeight - 9));
                                //if (this._showItemCodeCheckBox.Checked)
                                //{
                                //    if (__head1.Length > 0)
                                //    {
                                //        __head1.Append(" : ");
                                //    }
                                //    __head1.Append(__apCodeList[__foundAddr]._ar_name);
                                //}
                                //try
                                //{
                                //    __barcodeEan13._createEan13(__apCodeList[__foundAddr]._ar_code, null);
                                //    __g.DrawImage(__barcodeEan13.Paint(), __x, __y + 2, (int)__labelWidth - 10, (int)__labelHeight - 12);
                                //}
                                //catch
                                //{
                                //}
                                //StringBuilder __head2 = new StringBuilder();
                                //if (this._showPriceCheckBox.Checked)
                                //{
                                //    __head2.Append(String.Format("{0:0,0.00}", __apCodeList[__foundAddr]._price) + "/" + __apCodeList[__foundAddr]._address + " : ");
                                //}
                                //__head2.Append(__apCodeList[__foundAddr]._ar_name);
                                //if (this._showPriceCheckBox.Checked == false)
                                //{
                                //    __head2.Append(" (" + __apCodeList[__foundAddr]._address + ")");
                                //}
                                ArrayList __word = MyLib._myUtil._cutString(__g, __apCodeList[__foundAddr]._address.Trim(), __font, __labelWidth + (__labelWidth * 0.1f));
                                __g.DrawString(__word[0].ToString(), __font, Brushes.Black, __x, __y + (__labelHeight - 9));
                                if (__word.Count > 1)
                                {
                                    __g.DrawString(__word[1].ToString(), __font, Brushes.Black, __x, __y + (__labelHeight - 6));
                                }
                            }
                        }
                    }
                    e1.HasMorePages = (__currentPage != __maxPage) ? true : false;
                };
                if (preview)
                {
                    __preview.ShowDialog();
                }
                else
                {
                    __printDoc.Print();
                }
            }
            else
            {
                MessageBox.Show("Printer is invalid.");
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __arCode = this._itemList._gridData._cellGet(e._row, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
            Boolean __found = false;
            for (int __row = 0; __row < this._selectedGid._rowData.Count; __row++)
            {
                if (__arCode.Equals(this._selectedGid._cellGet(__row, _g.d.ar_customer._code).ToString()))
                {
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __data = __myFrameWork._queryShort("select * from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + __arCode + "\'").Tables[0];
                if (__data.Rows.Count > 0)
                {
                    int __addr = this._selectedGid._addRow();
                    this._selectedGid._cellUpdate(__addr, _g.d.ar_customer._code, __data.Rows[0][_g.d.ar_customer._code].ToString(), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ar_customer._name_1, __data.Rows[0][_g.d.ar_customer._name_1].ToString(), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ar_customer._address, __data.Rows[0][_g.d.ar_customer._address].ToString(), false);
                }
            }
        }

        private void _resetButton_Click(object sender, EventArgs e)
        {
            this._selectedGid._clear();
        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            _print(false);
        }

        private void _previewButton_Click(object sender, EventArgs e)
        {
            _print(true);
        }
    }

    class _arDetail
    {
        public int _page;
        public int _row;
        public int _column;
        public string _ar_code;
        public string _ar_name;
        public string _address;
    }

}
