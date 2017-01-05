using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.IO;
using SMLReport._design;
using SMLReport._formReport;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace SMLBarcodeManage
{
    public partial class _itemBarcodePrint : UserControl
    {
        public _itemBarcodePrint()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip3.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            string _formatNumber = _g.g._getFormatNumberStr(2);
            this._selectedGid._columnTopActive = true;

            this._selectedGid._table_name = _g.d.ic_inventory_barcode._table;
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._barcode, 1, 20, 20);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._ic_code, 1, 20, 20);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._description, 1, 30, 30);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._unit_code, 1, 10, 10);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price, 3, 10, 10, false, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_member, 3, 0, 15, true, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_2, 3, 0, 15, true, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_member_2, 3, 0, 15, true, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_3, 3, 0, 15, true, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_member_3, 3, 0, 15, true, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_4, 3, 0, 15, true, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_member_4, 3, 0, 15, true, false, true, false, _formatNumber);
            // ราคากลาง
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._price_formula_price_0, 3, 0, 15, true, false, true, false, _formatNumber);
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._stamp_count, 3, 10, 10, true, false, true, false, "###");
            this._selectedGid._addColumn(_g.d.ic_inventory_barcode._unit_name, 1, 20, 10, false, true);


            this._selectedGid._addColumnTop(_g.d.ic_inventory_barcode._price_group_1, this._selectedGid._findColumnByName(_g.d.ic_inventory_barcode._price), this._selectedGid._findColumnByName(_g.d.ic_inventory_barcode._price_member));
            this._selectedGid._addColumnTop(_g.d.ic_inventory_barcode._price_group_2, this._selectedGid._findColumnByName(_g.d.ic_inventory_barcode._price_2), this._selectedGid._findColumnByName(_g.d.ic_inventory_barcode._price_member_2));
            this._selectedGid._addColumnTop(_g.d.ic_inventory_barcode._price_group_3, this._selectedGid._findColumnByName(_g.d.ic_inventory_barcode._price_3), this._selectedGid._findColumnByName(_g.d.ic_inventory_barcode._price_member_3));
            this._selectedGid._addColumnTop(_g.d.ic_inventory_barcode._price_group_4, this._selectedGid._findColumnByName(_g.d.ic_inventory_barcode._price_4), this._selectedGid._findColumnByName(_g.d.ic_inventory_barcode._price_member_4));

            this._selectedGid._setColumnBackground(_g.d.ic_inventory_barcode._price, Color.Honeydew);
            this._selectedGid._setColumnBackground(_g.d.ic_inventory_barcode._price_member, Color.LightYellow);
            this._selectedGid._setColumnBackground(_g.d.ic_inventory_barcode._price_2, Color.Honeydew);
            this._selectedGid._setColumnBackground(_g.d.ic_inventory_barcode._price_member_2, Color.LightYellow);
            this._selectedGid._setColumnBackground(_g.d.ic_inventory_barcode._price_3, Color.Honeydew);
            this._selectedGid._setColumnBackground(_g.d.ic_inventory_barcode._price_member_3, Color.LightYellow);
            this._selectedGid._setColumnBackground(_g.d.ic_inventory_barcode._price_4, Color.Honeydew);
            this._selectedGid._setColumnBackground(_g.d.ic_inventory_barcode._price_member_4, Color.LightYellow);

            this._selectedGid._calcPersentWidthToScatter();

            // toe เพิ่ม printer 
            _getDefaultPrinter();

            //
            this._itemList._loadViewFormat(_g.g._search_screen_ic_inventory_barcode, MyLib._myGlobal._userSearchScreenGroup, true);
            this._itemList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._priceComboBox.SelectedIndex = 0;
            this._ltdNameTextBox.Text = MyLib._myGlobal._ltdName;
        }

        void _printFormCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            _checkPrintFormDisabled();
        }

        void _checkPrintFormDisabled()
        {
            Boolean __isFormPrint = _printFormCheckBox.Checked;
            Boolean __isFixMargin = _fixMarginCheckbox.Checked;
            Boolean __isFixSize = _fixSizeCheckbox.Checked;

            _fixMarginCheckbox.Enabled = _fixSizeCheckbox.Enabled = __isFormPrint;

            this._labelHeightTextBox.Enabled = this._labelWidthTextBox.Enabled = (__isFormPrint == true && __isFixSize == false) ? false : true;
            this._topMarginTextBox.Enabled = this._leftMarginTextBox.Enabled = (__isFormPrint == true && __isFixMargin == false) ? false : true;
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

            try
            {
                _printerComboBox.SelectedIndex = __default;
            }
            catch
            {
            }

        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __barCode = this._itemList._gridData._cellGet(e._row, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
            Boolean __found = false;
            for (int __row = 0; __row < this._selectedGid._rowData.Count; __row++)
            {
                if (__barCode.Equals(this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._barcode).ToString()))
                {
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __data = __myFrameWork._queryShort("select *" +
                    ", (select " + _g.d.ic_unit._table + "." + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + " = " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " ) as " + _g.d.ic_inventory_barcode._unit_name +
                    ", (select " + _g.d.ic_inventory_price_formula._table + "." + _g.d.ic_inventory_price_formula._price_0 + " from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._table + "." + _g.d.ic_inventory_price_formula._ic_code + " = " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " and " + _g.d.ic_inventory_price_formula._table + "." + _g.d.ic_inventory_price_formula._unit_code + " = " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " limit 1 ) as " + _g.d.ic_inventory_barcode._price_formula_price_0 +
                   "  from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "=\'" + __barCode + "\'").Tables[0];
                if (__data.Rows.Count > 0)
                {
                    int __addr = this._selectedGid._addRow();
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._barcode, __data.Rows[0][_g.d.ic_inventory_barcode._barcode].ToString(), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._ic_code, __data.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString(), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._description, __data.Rows[0][_g.d.ic_inventory_barcode._description].ToString(), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._unit_code, __data.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString(), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price].ToString()), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_2, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_2].ToString()), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_3, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_3].ToString()), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_4, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_4].ToString()), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_member, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_member].ToString()), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_member_2, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_member_2].ToString()), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_member_3, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_member_3].ToString()), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_member_4, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_member_4].ToString()), false);
                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._unit_name, __data.Rows[0][_g.d.ic_inventory_barcode._unit_name].ToString(), false);

                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_formula_price_0, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_formula_price_0].ToString()), false);

                    this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._stamp_count, 0M, false);
                }
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private float _convertCentimeter(float centimeter)
        {
            return centimeter * 10F;
        }

        public int _getCustomPaperWidth
        {
            get
            {
                decimal __width = MyLib._myGlobal._decimalPhase(this._customWidthTextbox.Text);
                if (this.radioButton1.Checked == true)
                    return _inchToHOI(__width);
                else
                    return _cmToHOI(__width);
            }
        }

        public int _getCustomPaperHeight
        {
            get
            {
                decimal __height = MyLib._myGlobal._decimalPhase(this._customHeightTextbox.Text);
                if (this.radioButton1.Checked == true)
                    return _inchToHOI(__height);
                else
                    return _cmToHOI(__height);
            }
        }

        public int _cmToHOI(decimal value)
        {
            int __result = 100;
            try
            {
                decimal __resultDecmial = (value * 39.3700787M);
                __result = decimal.ToInt16(__resultDecmial);
            }
            catch
            {
            }
            return __result;
        }

        public int _inchToHOI(decimal value)
        {
            int __result = 100;
            try
            {
                decimal __resultDecmial = (value * 100);
                __result = decimal.ToInt16(__resultDecmial);

            }
            catch
            {
            }
            return __result;
        }

        private void _print(Boolean preview)
        {
            List<_barCodeDetail> __barCodeList = new List<_barCodeDetail>();
            int __maxPage = 1;
            int __maxRow = (int)MyLib._myGlobal._decimalPhase(this._maxRowTextBox.Text);
            int __maxColumn = (int)MyLib._myGlobal._decimalPhase(this._maxColumnTextBox.Text);
            int __currentRow = (int)MyLib._myGlobal._decimalPhase(this._startRowTextBox.Text);
            int __currentColumn = (int)MyLib._myGlobal._decimalPhase(this._startColumnTextBox.Text);
            for (int __row = 0; __row < this._selectedGid._rowData.Count; __row++)
            {
                string __barCode = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._barcode).ToString();
                if (__barCode.Trim().Length > 0)
                {
                    int __label = (int)MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._stamp_count).ToString());
                    if (__label <= 0) __label = 1;
                    for (int __count = 0; __count < __label; __count++)
                    {
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
                        _barCodeDetail __data = new _barCodeDetail();
                        __data._page = __maxPage;
                        __data._row = __currentRow;
                        __data._column = __currentColumn;
                        __data._barCode = __barCode;
                        __data._itemCode = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._ic_code).ToString();
                        __data._name = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._description).ToString();
                        __data._unitCode = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._unit_code).ToString();
                        __data._unitName = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._unit_name).ToString();

                        string __priceField = "";
                        switch (this._priceComboBox.SelectedIndex)
                        {
                            case 0: __priceField = _g.d.ic_inventory_barcode._price; break;
                            case 1: __priceField = _g.d.ic_inventory_barcode._price_2; break;
                            case 2: __priceField = _g.d.ic_inventory_barcode._price_3; break;
                            case 3: __priceField = _g.d.ic_inventory_barcode._price_4; break;
                            case 4: __priceField = _g.d.ic_inventory_barcode._price_formula_price_0; break;
                        }
                        __data._price = MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, __priceField).ToString());
                        if (__data._price == 0)
                        {
                            __data._price = MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._price).ToString());
                        }
                        __barCodeList.Add(__data);
                        __currentColumn++;
                    }
                }
            }
            PrintPreviewDialog __preview = new PrintPreviewDialog();
            PrintDocument __printDoc = new PrintDocument();
            //__printDoc.PrinterSettings.PrinterName = "Canon LBP5050";

            __printDoc.PrinterSettings.PrinterName = this._printerComboBox.SelectedItem.ToString();

            if (__printDoc.PrinterSettings.IsValid)
            {
                if (this._customPaperSizeCheckbox.Checked == true)
                {
                    // set custom paper size
                    PaperSize __paperSize = new PaperSize("Custom Size", _getCustomPaperWidth, _getCustomPaperHeight);
                    __printDoc.DefaultPageSettings.PaperSize = __paperSize;
                    __printDoc.PrinterSettings.DefaultPageSettings.PaperSize = __paperSize;
                }

                __printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                __preview.Document = __printDoc;
                float __leftMargin = (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._leftMarginTextBox.Text));
                float __topMargin = (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._topMarginTextBox.Text));
                float __labelWidth = this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._labelWidthTextBox.Text));
                float __labelHeight = this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._labelHeightTextBox.Text));
                int __currentPage = 0;

                __printDoc.BeginPrint += (s2, e2) =>
                {
                    __currentPage = 0;
                };

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
                    SMLBarcodeManage.Ean13 __barcodeEan13 = new Ean13();
                    Bitmap __logo = null;
                    try
                    {
                        __logo = (Bitmap)Image.FromFile(this._logoTextBox.Text.Trim(), true);
                    }
                    catch
                    {
                    }
                    for (int __row = 1; __row <= __maxRow; __row++)
                    {
                        for (int __column = 1; __column <= __maxColumn; __column++)
                        {
                            int __foundAddr = -1;
                            for (int __find = __findAddrTrial; __find < __barCodeList.Count; __find++)
                            {
                                if (__barCodeList[__find]._page == __currentPage && __barCodeList[__find]._row == __row && __barCodeList[__find]._column == __column)
                                {
                                    __foundAddr = __find;
                                    __findAddrTrial = __find + 1;
                                    break;
                                }
                            }
                            if (__foundAddr != -1)
                            {
                                float __columnFloat = (float)__column;
                                float __rowFloat = (float)__row;
                                int __x = (int)(((__columnFloat - 1.0f) * __labelWidth) + __leftMargin);
                                int __y = (int)(((__rowFloat - 1.0f) * __labelHeight) + __topMargin);
                                if (this._shelfCheckBox.Checked)
                                {
                                    // พิมพ์ติด Shelf
                                    __y += 4;
                                    int __xx = __x;
                                    int __yy = __y;
                                    float __logoWidth = 0f;
                                    float __logoHeight = 0f;
                                    if (__logo != null)
                                    {
                                        __g.DrawImage(__logo, __x, __y);
                                        __logoWidth = (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._logoWidthTextBox.Text));
                                        __logoHeight = (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._logoHeightTextBox.Text));
                                    }
                                    Font __font = new System.Drawing.Font("Tahome", 7);
                                    Font __font2 = new System.Drawing.Font("Tahome", 10);
                                    Font __font3 = new System.Drawing.Font("Tahome", 14);
                                    StringBuilder __head1 = new StringBuilder(this._ltdNameTextBox.Text.Trim());
                                    //__g.DrawString(__x.ToString()+" : "+__y.ToString(), __font, Brushes.Black, __x, __y);
                                    __g.DrawString(__head1.ToString(), __font, Brushes.Black, __logoWidth + __x, __y);
                                    __y += 5;
                                    ArrayList __word = MyLib._myUtil._cutString(__g, __barCodeList[__foundAddr]._name, __font2, (__labelWidth - __logoWidth) + ((__labelWidth - __logoWidth) * 0.1f));
                                    for (int __loop = 0; __loop < __word.Count; __loop++)
                                    {
                                        __g.DrawString(__word[__loop].ToString(), __font2, Brushes.Black, __logoWidth + __x, __y);
                                        __y += 7;
                                    }
                                    StringBuilder __head2 = new StringBuilder();
                                    if (this._showPriceCheckBox.Checked)
                                    {
                                        __head2.Append("ราคา" + " " + String.Format("{0:0,0.00}", __barCodeList[__foundAddr]._price) + "/" + ((this._unitNameCheckbox.Checked) ? __barCodeList[__foundAddr]._unitName : __barCodeList[__foundAddr]._unitCode) + " : ");
                                    }
                                    if (this._showPriceCheckBox.Checked == false)
                                    {
                                        __head2.Append(" (" + __barCodeList[__foundAddr]._unitCode + ")");
                                    }
                                    if (__head2.Length > 0)
                                    {
                                        __g.DrawString(__head2.ToString(), __font3, Brushes.Black, __x + __logoWidth, __y);
                                        __y += 7;
                                    }
                                    if (this._showItemCodeCheckBox.Checked)
                                    {
                                        if (__head1.Length > 0)
                                        {
                                            __head1.Append(" : ");
                                        }
                                        __g.DrawString("รหัส" + " : " + __barCodeList[__foundAddr]._itemCode, __font2, Brushes.Black, __x, __y);
                                        __y += 7;
                                    }
                                    try
                                    {
                                        __barcodeEan13._createEan13(__barCodeList[__foundAddr]._barCode, null);
                                        __g.DrawString("BARCODE" + " : " + __barCodeList[__foundAddr]._barCode, __font2, Brushes.Black, __x, __y);
                                        __y += 5;
                                        __g.DrawImage(__barcodeEan13.Paint(), __x, __y + 2, ((int)__labelWidth) - 10, (int)10);
                                    }
                                    catch
                                    {
                                    }
                                    if (this._borderCheckBox.Checked)
                                    {
                                        Point[] __point = { new Point(__xx, __yy), new Point(__xx + (int)__labelWidth, __yy), new Point(__xx + (int)__labelWidth, __yy + (int)__labelHeight), new Point(__xx, __yy + (int)__labelHeight) };
                                        __g.DrawPolygon(new Pen(Color.Black, 1), __point);
                                    }
                                }
                                else
                                {
                                    // Barcode Sticker
                                    Font __font = new System.Drawing.Font("Tahome", 7);
                                    Font __font2 = new System.Drawing.Font("Tahome", 5);
                                    StringBuilder __head1 = new StringBuilder(this._ltdNameTextBox.Text.Trim());
                                    if (this._showItemCodeCheckBox.Checked)
                                    {
                                        if (__head1.Length > 0)
                                        {
                                            __head1.Append(" : ");
                                        }
                                        __head1.Append(__barCodeList[__foundAddr]._itemCode);
                                    }
                                    //__g.DrawString(__x.ToString()+" : "+__y.ToString(), __font, Brushes.Black, __x, __y);
                                    __g.DrawString(__head1.ToString(), __font, Brushes.Black, __x, __y);
                                    try
                                    {
                                        __barcodeEan13._createEan13(__barCodeList[__foundAddr]._barCode, null);
                                        __g.DrawImage(__barcodeEan13.Paint(), __x + 5, __y + 2, (int)__labelWidth - 10, (int)__labelHeight - 15);
                                        if (__barCodeList[__foundAddr]._barCode.Length == 13)
                                        {
                                            __g.DrawString(__barCodeList[__foundAddr]._barCode.Substring(0, 6), __font2, Brushes.Black, __x, __y + 3);
                                            __g.DrawString(__barCodeList[__foundAddr]._barCode.Substring(6, 7), __font2, Brushes.Black, __x, __y + 5);
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    StringBuilder __head2 = new StringBuilder();
                                    if (this._showPriceCheckBox.Checked)
                                    {
                                        __head2.Append("ราคา" + " " + String.Format("{0:0,0.00}", __barCodeList[__foundAddr]._price) + "/" + ((this._unitNameCheckbox.Checked) ? __barCodeList[__foundAddr]._unitName : __barCodeList[__foundAddr]._unitCode) + " : ");
                                    }
                                    __head2.Append(__barCodeList[__foundAddr]._name);
                                    if (this._showPriceCheckBox.Checked == false)
                                    {
                                        __head2.Append(" (" + __barCodeList[__foundAddr]._unitCode + ")");
                                    }
                                    ArrayList __word = MyLib._myUtil._cutString(__g, __head2.ToString(), __font, __labelWidth + (__labelWidth * 0.1f));
                                    __g.DrawString(__word[0].ToString(), __font, Brushes.Black, __x, __y + (__labelHeight - 12));
                                    if (__word.Count > 1)
                                    {
                                        __g.DrawString(__word[1].ToString(), __font, Brushes.Black, __x, __y + (__labelHeight - 9));
                                    }
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

        public void _printForm(Boolean __isPreview)
        {
            List<_barCodeDetail> __barCodeList = new List<_barCodeDetail>();
            for (int __row = 0; __row < this._selectedGid._rowData.Count; __row++)
            {
                string __barCode = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._barcode).ToString();
                if (__barCode.Trim().Length > 0)
                {
                    int __label = (int)MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._stamp_count).ToString());
                    if (__label <= 0) __label = 1;
                    for (int __count = 0; __count < __label; __count++)
                    {
                        _barCodeDetail __data = new _barCodeDetail();
                        __data._barCode = __barCode;
                        __data._itemCode = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._ic_code).ToString();
                        __data._name = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._description).ToString();
                        __data._unitCode = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._unit_code).ToString();
                        __data._unitName = this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._unit_name).ToString();
                        string __priceField = "";
                        string __memberPriceField = "";
                        switch (this._priceComboBox.SelectedIndex)
                        {
                            case 0: __priceField = _g.d.ic_inventory_barcode._price; __memberPriceField = _g.d.ic_inventory_barcode._price_member; break;
                            case 1: __priceField = _g.d.ic_inventory_barcode._price_2; __memberPriceField = _g.d.ic_inventory_barcode._price_member_2; break;
                            case 2: __priceField = _g.d.ic_inventory_barcode._price_3; __memberPriceField = _g.d.ic_inventory_barcode._price_member_3; break;
                            case 3: __priceField = _g.d.ic_inventory_barcode._price_4; __memberPriceField = _g.d.ic_inventory_barcode._price_member_4; break;
                            case 4: __priceField = _g.d.ic_inventory_barcode._price_formula_price_0; __memberPriceField = _g.d.ic_inventory_barcode._price_formula_price_0; break;
                        }
                        __data._price = MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, __priceField).ToString());
                        __data._member_price = MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, __memberPriceField).ToString());
                        if (__data._price == 0)
                        {
                            __data._price = MyLib._myGlobal._decimalPhase(this._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._price).ToString());
                        }
                        __barCodeList.Add(__data);
                    }
                }
            }

            if (__barCodeList.Count > 0)
            {
            }
            SMLReport._formReport._formDesigner __form = new _formDesigner();

            // get form cache 
            string __currentConfigFileName = string.Format("_cache-{0}-{1}-{2}-{3}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__").Replace("/", string.Empty), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, _formCodeTextBox.Text); // "_cache + formCode + ".xml";
            string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();
            string __lastTimeUpdate = "";

            // check cache  update version
            bool _isCache = false;
            try
            {
                // check xml 
                StringBuilder __queryCheckCode = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __queryCheckCode.Append(MyLib._myUtil._convertTextToXmlForQuery("SELECT " + _g.d.formdesign._formcode + ", " + _g.d.formdesign._timeupdate + " FROM " + _g.d.formdesign._table + " WHERE " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + "=\'" + _formCodeTextBox.Text.ToUpper() + "\'"));
                __queryCheckCode.Append("</node>");

                MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                ArrayList __result = __ws._queryListGetData(MyLib._myGlobal._databaseName, __queryCheckCode.ToString());

                DataTable __da = ((DataSet)__result[0]).Tables[0];
                __lastTimeUpdate = __da.Rows[0]["timeupdate"].ToString();

                SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();

                TextReader readFile = new StreamReader(__path);
                XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                __cacheXML = (SMLReport._formReport.SMLFormDesignXml)__xsLoad.Deserialize(readFile);
                readFile.Close();

                if (__lastTimeUpdate == __cacheXML._lastUpdate)
                {
                    _isCache = true;
                }
            }
            catch (Exception ex)
            {
                _isCache = false;
            }

            // not exit get form and write cache
            try
            {
                if (_isCache)
                {
                    SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLFormDesignXml));
                    FileStream __readFileStream = new FileStream(__path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    __form._loadFromStream(__readFileStream, null, _openFormMethod.OpenFromServer);
                    __readFileStream.Close();

                }
                else
                {
                    string __query = "SELECT " + _g.d.formdesign._formcode + "," + _g.d.formdesign._guid_code + "," + _g.d.formdesign._formname + "," + _g.d.formdesign._timeupdate + "," + _g.d.formdesign._formdesigntext + " FROM " + _g.d.formdesign._table + " WHERE " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + " =\'" + _formCodeTextBox.Text.ToUpper() + "\'";

                    MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                    MyLib.SMLJAVAWS.formDesignType __formDesign = __ws._loadForm(MyLib._myGlobal._databaseName, __query);

                    try
                    {
                        // ลองดึงดู ถ้าข้อมูล Compress แล้ว ก็ผ่าน ถ้าไม่ ก็ไปดึงแบบเดิม
                        MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__formDesign._formdesign));
                        __form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                        __ms.Close();
                    }
                    catch (Exception _ex_xmlcompress_1)
                    {
                        // กรณีที่ดึงของเก่าที่ไม่ได้ Compress
                        try
                        {
                            MemoryStream __ms = new MemoryStream((byte[])__formDesign._formdesign);
                            __form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                            __ms.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                    // write cache
                    SMLFormDesignXml __formCacheXML = __form._writeXMLSource(_writeXMLSourceOption.DrawObjectOnly);
                    __formCacheXML._lastUpdate = __lastTimeUpdate;

                    XmlSerializer __colXs = new XmlSerializer(typeof(SMLFormDesignXml));
                    TextWriter __memoryStream = new StreamWriter(__path);
                    __colXs.Serialize(__memoryStream, __formCacheXML);
                    __memoryStream.Close();

                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
            // print 

            if (__form != null)
            {
                int __maxPage = __barCodeList.Count;
                float _startYPos = 0f;
                float _startXPos = 0f;
                float __paperWidth = 0f;
                float __paperHeight = 0f;

                float __labelHeight = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup._getPageHeightHOI;
                float __labelWidth = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup._getPageWidthHOI;

                int __currentLabel = 0;
                // get margin bound                

                PrintPreviewDialog __preview = new PrintPreviewDialog();
                PrintDocument __printDoc = new PrintDocument();
                //__printDoc.PrinterSettings.PrinterName = "Canon LBP5050";
                if (__printDoc.PrinterSettings.IsValid)
                {
                    if (this._customPaperSizeCheckbox.Checked == true)
                    {
                        // set custom paper size
                        PaperSize __paperSize = new PaperSize("Custom Size", _getCustomPaperWidth, _getCustomPaperHeight);
                        __printDoc.DefaultPageSettings.PaperSize = __paperSize;
                        __printDoc.PrinterSettings.DefaultPageSettings.PaperSize = __paperSize;
                    }

                    __printDoc.PrinterSettings.PrinterName = this._printerComboBox.SelectedItem.ToString();

                    __printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                    __preview.Document = __printDoc;

                    int __currentPage = 0;

                    // check size
                    if (__labelHeight > __printDoc.PrinterSettings.DefaultPageSettings.PaperSize.Height || __labelWidth > __printDoc.PrinterSettings.DefaultPageSettings.PaperSize.Width)
                    {
                        MessageBox.Show("ขนาด Label ไม่สัมพันธ์กับขนาดกระดาษ");
                        return;
                    }

                    __printDoc.BeginPrint += (_beginPrintSender, _beginPrintEvent) =>
                    {
                        __currentPage = 0;
                        __currentLabel = 0;
                        _startYPos = 0f;
                        _startXPos = 0f;
                    };

                    __printDoc.PrintPage += (s1, e1) =>
                    {
                        e1.PageSettings.Margins.Top = 0;
                        e1.PageSettings.Margins.Left = 0;
                        e1.PageSettings.Margins.Bottom = 0;
                        e1.PageSettings.Margins.Right = 0;

                        _startYPos = 0f;
                        _startXPos = 0f;

                        //e1.Graphics.PageUnit = GraphicsUnit.Millimeter;
                        Graphics __g = e1.Graphics;
                        float __leftMargin = (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._leftMarginTextBox.Text));
                        float __topMargin = (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._topMarginTextBox.Text));

                        _startXPos += __leftMargin;
                        _startYPos += __topMargin;

                        //__g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        __paperWidth = e1.PageBounds.Width;
                        __paperHeight = e1.PageBounds.Height;
                        if ((__leftMargin + __labelWidth) <= __paperWidth)
                        {
                            // if page 1 set start label
                            if (__currentPage == 0)
                            {
                                int __currentRow = (int)MyLib._myGlobal._decimalPhase(this._startRowTextBox.Text) - 1;
                                int __currentColumn = (int)MyLib._myGlobal._decimalPhase(this._startColumnTextBox.Text) - 1;

                                if (__currentRow != 0)
                                {
                                    _startYPos += (__currentRow * __labelHeight);
                                }

                                if (__currentColumn != 0)
                                {
                                    _startXPos += (__currentColumn * __labelWidth);
                                }
                            }

                            if (__barCodeList.Count > __currentLabel)
                            {
                                for (int __ibarcode = __currentLabel; __ibarcode < __barCodeList.Count; __ibarcode++)
                                {
                                    _barCodeDetail __barcode = __barCodeList[__currentLabel];

                                    if ((_startXPos + __labelWidth) <= __paperWidth && (_startYPos + __labelHeight) <= __paperHeight)
                                    {
                                        _drawBarcodeLabelForm(e1.Graphics, ((SMLReport._formReport._drawPaper)__form._paperList[0]), __barcode, new PointF(_startXPos, _startYPos));
                                        __currentLabel++;
                                        _startXPos += __labelWidth;

                                    }
                                    else
                                    {
                                        if ((_startXPos + __labelWidth) > __paperWidth && (_startYPos + __labelHeight + __labelHeight) < __paperHeight) // ถ้าขึ้นบรรทัดใหม่ แล้วไม่ล้นหน้า
                                        {
                                            // ขึ้นบรรทัดใหม่ ถ้าล้นหน้าขึ้นหน้าใหม่ไปเลย
                                            _startXPos = 0;
                                            _startXPos += __leftMargin;
                                            _startYPos += __labelHeight;

                                            _drawBarcodeLabelForm(e1.Graphics, ((SMLReport._formReport._drawPaper)__form._paperList[0]), __barcode, new PointF(_startXPos, _startYPos));
                                            _startXPos += __labelWidth;
                                            __currentLabel++;

                                        }
                                        else
                                        {
                                            // ขึ้นหน้าใหม่
                                            __currentPage++;
                                            e1.HasMorePages = true;
                                            break;
                                        }
                                    }

                                }


                                //__currentLabel++;

                                // check page height
                                //e1.HasMorePages = (__currentPage < __maxPage) ? true : false;
                            }
                        }
                        else
                        {
                            e1.HasMorePages = false;
                        }

                    };
                    if (__isPreview)
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
        }

        void _drawBarcodeLabelForm(Graphics g, SMLReport._formReport._drawPaper paperStruct, _barCodeDetail barcode, PointF drawPoint)
        {
            foreach (Control __control1 in paperStruct.Controls)
            {
                if (__control1.GetType() == typeof(SMLReport._design._drawPanel))
                {
                    SMLReport._design._drawPanel __getControl = (SMLReport._design._drawPanel)__control1;
                    //Graphics __g = __getControl.CreateGraphics();
                    for (int __loop = __getControl._graphicsList._count - 1; __loop >= 0; __loop--)
                    {
                        // call print object
                        SMLReport._design._drawObject __obj = ((SMLReport._design._drawObject)__getControl._graphicsList[__loop]);

                        if (__obj.GetType() == typeof(_drawLabel))
                        {

                            _drawLabel __drawLabel = (_drawLabel)__obj;


                            Pen __pen = new Pen(__drawLabel._lineColor, __drawLabel._penWidth);
                            __pen.DashStyle = __drawLabel._lineStyle;
                            SolidBrush __brush = new SolidBrush(__drawLabel._foreColor);
                            SolidBrush __BgBrush = new SolidBrush(__drawLabel._backColor);
                            string __str = __drawLabel._text.Replace("&item_code&", barcode._itemCode).Replace("&item_name&", barcode._name).Replace("&barcode&", barcode._barCode).Replace("&price&", barcode._price.ToString("#,##0.00")).Replace("&member_price&", barcode._member_price.ToString("#,##0.00")).Replace("&unit_code&", barcode._unitCode).Replace("&ltd_name&", _ltdNameTextBox.Text).Replace("&unit_name&", barcode._unitName).Replace("&barcode_ltd_name&", this._ltdNameTextBox.Text);
                            Regex __dateRegex = new Regex(@"&date\[(.*)\]&");
                            if (__dateRegex.IsMatch(__str))
                            {
                                Match __match = __dateRegex.Match(__str);
                                if (__match.Success)
                                {
                                    string __dateFormatStr = __match.Groups[1].Value;
                                    //DateTime __date = new DateTime((MyLib._myGlobal._year_type == 1) ? DateTime.Now.Year + 543 : DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                                    DateTime __date = DateTime.Now;
                                    __str = __dateRegex.Replace(__str, __date.ToString(__dateFormatStr, MyLib._myGlobal._cultureInfo()));
                                }
                            }
                            RectangleF __LabelRect = __drawLabel._actualSize;
                            Font __newFont = new Font(__drawLabel._font.FontFamily, __drawLabel._font.Size, __drawLabel._font.Style, __drawLabel._font.Unit, __drawLabel._font.GdiCharSet, __drawLabel._font.GdiVerticalFont);

                            PointF __strDrawPoint = new PointF(drawPoint.X + __LabelRect.X, drawPoint.Y + __LabelRect.Y);

                            // จัดตำแนห่ง แล้วพิมพ์ ตัดบรรทัดด้วยนะ
                            ArrayList __getString = SMLReport._design._drawLabel._cutString(__str, __drawLabel._font, __LabelRect.Width, __drawLabel._charSpace, __drawLabel._charWidth, __drawLabel._padding);

                            if (__drawLabel._overFlow == overFlowType.Hidden)
                            {
                                string __firstLine = (string)__getString[0];
                                __getString = new ArrayList() { __firstLine };
                            }

                            SizeF __dataStrSize = _getTextSize(__getString, __drawLabel._font, g);
                            PointF __tmpPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __dataStrSize.Width, __dataStrSize.Height, __drawLabel._textAlign, __drawLabel._padding);

                            //g.DrawString(__str, __newFont, __brush, __LabelRect.X + drawPoint.X, __LabelRect.Y + drawPoint.Y, StringFormat.GenericTypographic);

                            for (int __line = 0; __line < __getString.Count; __line++)
                            {
                                SizeF __strLineSize = _getTextSize((string)__getString[__line], __drawLabel._font, g);

                                PointF __getDrawPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __strLineSize.Width, __strLineSize.Height, __drawLabel._textAlign, __drawLabel._padding);

                                g.DrawString((string)__getString[__line], __drawLabel._font, new SolidBrush(__drawLabel._foreColor), (__strDrawPoint.X + __getDrawPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

                                __strDrawPoint.Y += __strLineSize.Height;

                            }

                            //g.DrawString(__str, __newFont, __brush, __LabelRect.X + drawPoint.X, __LabelRect.Y + drawPoint.Y, StringFormat.GenericTypographic);

                        }
                        else if (__obj.GetType() == typeof(_drawImageField))
                        {
                            _drawImageField __imageField = (_drawImageField)__obj;
                            if (__imageField.FieldType == _FieldType.Barcode)
                            {
                                if (__imageField._typeBarcode == _barcodeType.BarCode_EAN13)
                                {
                                    try
                                    {
                                        SMLBarcodeManage.Ean13 __barcodeEan13 = new Ean13();
                                        Ean13Settings __setting = new Ean13Settings();
                                        //__setting.Font = __imageField._font;
                                        __setting.BarWidth = 4;
                                        //__setting.BarCodeHeight = __imageField._actualSize.Height;
                                        //__setting.BottomMargin = 20;

                                        __barcodeEan13._createEan13(barcode._barCode, null, __setting);
                                        //__barcodeEan13._createEan13(barcode._barCode, null);
                                        Image __imgBarcode = __barcodeEan13.Paint();

                                        g.DrawImage(__imgBarcode, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));

                                        //Image __image = __imageField._getBarcodeImage(barcode._barCode, Rectangle.Round(__imageField.Size).Size, __imageField._barcodeAlignment, __imageField._typeBarcode, __imageField._showBarcodeLabel, __imageField._barcodeLabelPosition, __imageField._font, __imageField.RotateFlip, __imageField._foreColor, __imageField._backColor);
                                        //g.DrawImage(__image, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));

                                    }
                                    catch
                                    {

                                    }

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_Code39)
                                {
                                    SMLBarcodeManage._createCode39 __code39 = new SMLBarcodeManage._createCode39();
                                    try
                                    {
                                        Code39Settings __setting = new Code39Settings();
                                        __setting.DrawText = __imageField._showBarcodeLabel;
                                        g.DrawImage(__code39._createBarCode(barcode._barCode, __setting), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_Code128)
                                {
                                    try
                                    {
                                        Code39Settings __setting = new Code39Settings();
                                        __setting.DrawText = __imageField._showBarcodeLabel;

                                        Image __getBarcodeImage = __imageField._getBarcodeImage(barcode._barCode, new Size(__imageField._actualSize.Width, __imageField._actualSize.Height), __imageField._barcodeAlignment, __imageField._typeBarcode, __imageField._showBarcodeLabel, __imageField._barcodeLabelPosition, __imageField._font, __imageField.RotateFlip, __imageField._foreColor, __imageField._backColor);
                                        g.DrawImage(__getBarcodeImage, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_QRCode)
                                {
                                    //QRCodeLib.QRCode __qrCode = new QRCodeLib.QRCode();
                                    //try
                                    //{
                                    //    g.DrawImage(__qrCode._createQRCode(barcode._barCode), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                                    //}
                                    //catch
                                    //{
                                    //}

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_UPCA)
                                {
                                    BarcoderLib.BarcodeUPCA __barcode = new BarcoderLib.BarcodeUPCA();

                                    try
                                    {
                                        g.DrawImage(__barcode.Encode(barcode._barCode.Substring(0, 11)), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                                    }
                                    catch
                                    {
                                    }

                                }
                            }
                            else if (__imageField.FieldType == _FieldType.Image)
                            {
                                // get picture 

                                SMLERPControl._getImageData __getImage = new SMLERPControl._getImageData(barcode._itemCode);
                                Image __image = __getImage._getImageNow();

                                g.DrawImage(__image, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));

                            }
                        }
                        else if (__obj.GetType() == typeof(_drawImage))
                        {
                            _drawImage __drawImage = (_drawImage)__obj;

                            Pen __pen = new Pen(__drawImage._lineColor, __drawImage._penWidth);
                            SolidBrush __brush = new SolidBrush(__drawImage._backColor);


                            if (__drawImage.Image != null)
                            {

                                float __x = __drawImage._actualSize.X + drawPoint.X;
                                float __y = __drawImage._actualSize.Y + drawPoint.Y;
                                float __width = __drawImage._actualSize.Width;
                                float __height = __drawImage._actualSize.Height;

                                // get Image from sizeType


                                Image __newImage = SMLReport._design._drawImage._SizeModeImg(__drawImage._actualSize, __drawImage.Image, __drawImage.SizeMode);

                                if (__drawImage.BorderStyle != SMLReport._design._drawImage.ImageBorderStyleType.None)
                                {
                                    if (__drawImage._penWidth < 1)
                                    {
                                        __drawImage._penWidth = 1;
                                    }
                                    if (__drawImage.BorderStyle == SMLReport._design._drawImage.ImageBorderStyleType.Line)
                                    {
                                        int __newLineWidth = __drawImage._penWidth / 2;
                                        if (__newLineWidth == 0)
                                        {
                                            __newLineWidth = 1;
                                        }
                                        __x += __newLineWidth;
                                        __y += __newLineWidth;
                                        __width -= __newLineWidth;
                                        __height -= __newLineWidth;
                                    }
                                    Pen __newPen = new Pen(__drawImage._lineColor, __drawImage._penWidth);

                                    //g.DrawImage(__newImage, __x, __y, __width, __height);
                                    g.DrawRectangle(__newPen, __drawImage._actualSize);

                                }

                                RectangleF __tmpNewRect = new RectangleF(__x, __y, __width, __height);

                                g.DrawImage(__newImage, __tmpNewRect);
                                //g.DrawRectangle(pen, _drawRectangle._getNormalizedRectangle(__tmpNewRect));
                            }
                            else
                            {
                                g.FillRectangle(__brush, __drawImage._actualSize);
                            }

                            __pen.DashStyle = __drawImage._LineStyle;
                            g.DrawRectangle(__pen, __drawImage._actualSize);

                            __pen.Dispose();
                            __brush.Dispose();
                        }
                        else if (__obj.GetType() == typeof(_drawLine))
                        {
                            _drawLine __drawLine = (_drawLine)__obj;

                            Pen __linePen = new Pen(__drawLine._lineColor, __drawLine._penWidth);
                            __linePen.DashStyle = __drawLine._LineStyle;

                            PointF __tmpStartPoint = __drawLine.StartPoint;
                            __tmpStartPoint.X += drawPoint.X;
                            __tmpStartPoint.Y += drawPoint.Y;

                            PointF __tmpEndPoint = __drawLine.EndPoint;
                            __tmpEndPoint.X += drawPoint.X;
                            __tmpEndPoint.Y += drawPoint.Y;

                            //e.DrawLine(__linePen, Point.Round(__tmpStartPoint), Point.Round(__tmpEndPoint));
                            //onDrawLine(e, __linePen, Point.Round(__tmpStartPoint), Point.Round(__tmpEndPoint));
                            g.DrawLine(__linePen, __tmpStartPoint, __tmpEndPoint);


                            __linePen.Dispose();
                        }
                        else if (__obj.GetType() == typeof(_drawEllipse))
                        {
                            _drawEllipse __drawEllipse = (_drawEllipse)__obj;
                            Pen __lineEllipse = new Pen(__drawEllipse._lineColor, __drawEllipse._penWidth);
                            __lineEllipse.DashStyle = __drawEllipse._LineStyle;
                            SolidBrush __ellipseBG = new SolidBrush(__drawEllipse._backColor);

                            Rectangle __rect = new Rectangle(__drawEllipse._actualSize.X, __drawEllipse._actualSize.Y, __drawEllipse._actualSize.Width, __drawEllipse._actualSize.Height);

                            g.FillEllipse(__ellipseBG, __rect);
                            g.DrawEllipse(__lineEllipse, __rect);

                            __lineEllipse.Dispose();
                            __ellipseBG.Dispose();

                        }
                        else if (__obj.GetType() == typeof(_drawRectangle))
                        {
                            _drawRectangle __drawRect = (_drawRectangle)__obj;
                            Pen __rectPen = new Pen(__drawRect._lineColor, __drawRect._penWidth);
                            __rectPen.DashStyle = __drawRect._LineStyle;

                            SolidBrush __rectBrush = new SolidBrush(__drawRect._backColor);

                            RectangleF __rect = __drawRect._actualSize;
                            __rect.X += drawPoint.X;
                            __rect.Y += drawPoint.Y;

                            g.FillRectangle(__rectBrush, Rectangle.Round(__rect));
                            //e.DrawRectangle(__rectPen, Rectangle.Round(__rect));
                            g.DrawRectangle(__rectPen, Rectangle.Round(__rect));

                            __rectBrush.Dispose();
                            __rectPen.Dispose();

                        }
                        else if (__obj.GetType() == typeof(_drawRoundedRectangle))
                        {
                            _drawRoundedRectangle __drawRoundedRect = (_drawRoundedRectangle)__obj;

                            Pen __pen = new Pen(__drawRoundedRect._lineColor, __drawRoundedRect._penWidth);
                            __pen.DashStyle = __drawRoundedRect._LineStyle;
                            SolidBrush __brush = new SolidBrush(__drawRoundedRect._backColor);
                            GraphicsPath gfxPath = new GraphicsPath();

                            RectangleF __rect = __drawRoundedRect._actualSize;
                            __rect.X += drawPoint.X;
                            __rect.Y += drawPoint.Y;


                            gfxPath = __drawRoundedRect._getRectangleGraphic(Rectangle.Round(__rect));

                            g.FillPath(__brush, gfxPath);
                            g.DrawPath(__pen, gfxPath);

                            __pen.Dispose();
                            __brush.Dispose();
                        }
                    }
                }
            }
        }

        private SizeF _getTextSize(string __strMeasure, Font __Font, Graphics g)
        {
            SizeF _textSize = new SizeF();
            SizeF _defaultTextSize = g.MeasureString("SampleText", __Font, 0, StringFormat.GenericTypographic);

            _textSize = g.MeasureString(__strMeasure, __Font, 0, StringFormat.GenericTypographic);
            _textSize.Height = _defaultTextSize.Height;

            return _textSize;
        }


        private SizeF _getTextSize(ArrayList __strMeasureList, Font __Font, Graphics g)
        {
            SizeF _textSize = new SizeF();
            SizeF _defaultTextSize = g.MeasureString("SampleText", __Font, 0, StringFormat.GenericTypographic);
            foreach (string __strMeasure in __strMeasureList)
            {
                SizeF __strSize = g.MeasureString(__strMeasure, __Font, 0, StringFormat.GenericTypographic);
                _textSize.Height += _defaultTextSize.Height;

                if (_textSize.Width < __strSize.Width)
                {
                    _textSize.Width = __strSize.Width;
                }
            }

            return _textSize;
        }


        private Bitmap _resizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_printFormCheckBox.Checked == true)
                    this._printForm(false);
                else
                    this._print(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void _previewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_printFormCheckBox.Checked == true)
                    this._printForm(true);
                else
                    this._print(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._selectedGid._clear();
        }

        private void _formatLoadButton_Click(object sender, EventArgs e)
        {
            _barcodePrintFormatForm __load = new _barcodePrintFormatForm();
            __load.ShowDialog();
            if (__load.DialogResult == DialogResult.OK)
            {
                string _query = "select " + _g.d.sml_barcode_print_format._formatdata + " from " + _g.d.sml_barcode_print_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.sml_barcode_print_format._code) + "='" + __load._formatCodeSelect.ToUpper() + "'";
                MyLib._myFrameWork __fw = new MyLib._myFrameWork();
                byte[] __getByte = __fw._queryByte(MyLib._myGlobal._databaseName, _query);

                try
                {
                    MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__getByte));
                    XmlSerializer __xs = new XmlSerializer(typeof(_barcodePrintFormtObj));
                    _barcodePrintFormtObj __obj = (_barcodePrintFormtObj)__xs.Deserialize(__ms);

                    _codeTextBox.Text = __obj._code;
                    _nameTextBox.Text = __obj._name;
                    if (__obj._unit == _formatUnit.Centimeter)
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }

                    _showPriceCheckBox.Checked = __obj._showPrice;
                    _priceComboBox.SelectedIndex = __obj._priceIndex;
                    _topMarginTextBox.Text = __obj._topMargin.ToString("###0.##");
                    _leftMarginTextBox.Text = __obj._leftMargin.ToString("###0.##");

                    _labelWidthTextBox.Text = __obj._width.ToString("###0.##");
                    _labelHeightTextBox.Text = __obj._height.ToString("###0.##");
                    _maxRowTextBox.Text = __obj._row.ToString("###0.##");
                    _maxColumnTextBox.Text = __obj._column.ToString("###0.##");
                    _startRowTextBox.Text = __obj._startRow.ToString("###0.##");
                    _startColumnTextBox.Text = __obj._startColumn.ToString("###0.##");
                    _ltdNameTextBox.Text = __obj._companyName;
                    _printFormCheckBox.Checked = __obj._formPrint;
                    _formCodeTextBox.Text = __obj._formCode;
                    _unitNameCheckbox.Checked = __obj._useUnitName;

                    this._customPaperSizeCheckbox.Checked = __obj._customPaperSize;
                    this._customWidthTextbox.Text = __obj._customPaperWidth.ToString("###0.##");
                    this._customHeightTextbox.Text = __obj._customPaperHeight.ToString("###0.##"); //(float)MyLib._myGlobal._decimalPhase();

                    __ms.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        private void _formatSaveButton_Click(object sender, EventArgs e)
        {
            string __errorMessage = "";
            if (_codeTextBox.Text.Equals(""))
            {
                __errorMessage = ((__errorMessage.Length > 0) ? "\n" : "") + "รหัสรูปแบบ";
            }

            if (_nameTextBox.Text.Equals(""))
            {
                __errorMessage = ((__errorMessage.Length > 0) ? "\n" : "") + "ชื่อรูปแบบ";
            }

            if (__errorMessage.Length > 0)
            {
                MessageBox.Show("กรุณากรอกข้อมูลต่อไปนี้ \n" + __errorMessage, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _barcodePrintFormtObj __obj = new _barcodePrintFormtObj();
            __obj._code = _codeTextBox.Text;
            __obj._name = _nameTextBox.Text;
            __obj._unit = (radioButton1.Checked) ? _formatUnit.Centimeter : _formatUnit.Inch;
            __obj._showPrice = _showPriceCheckBox.Checked;
            __obj._priceIndex = _priceComboBox.SelectedIndex;
            __obj._topMargin = (float)MyLib._myGlobal._decimalPhase(_topMarginTextBox.Text);
            __obj._leftMargin = (float)MyLib._myGlobal._decimalPhase(_leftMarginTextBox.Text);
            __obj._width = (float)MyLib._myGlobal._decimalPhase(_labelWidthTextBox.Text);
            __obj._height = (float)MyLib._myGlobal._decimalPhase(_labelHeightTextBox.Text);
            __obj._row = (int)MyLib._myGlobal._decimalPhase(_maxRowTextBox.Text);
            __obj._column = (int)MyLib._myGlobal._decimalPhase(_maxColumnTextBox.Text);
            __obj._startRow = (int)MyLib._myGlobal._decimalPhase(_startRowTextBox.Text);
            __obj._startColumn = (int)MyLib._myGlobal._decimalPhase(_startColumnTextBox.Text);
            __obj._companyName = _ltdNameTextBox.Text;
            __obj._formPrint = _printFormCheckBox.Checked;
            __obj._formCode = _formCodeTextBox.Text;
            __obj._useUnitName = _unitNameCheckbox.Checked;

            __obj._customPaperSize = this._customPaperSizeCheckbox.Checked;
            __obj._customPaperWidth = (float)MyLib._myGlobal._decimalPhase(this._customWidthTextbox.Text);
            __obj._customPaperHeight = (float)MyLib._myGlobal._decimalPhase(this._customHeightTextbox.Text);

            //serialize 
            XmlSerializer __xs = new XmlSerializer(typeof(_barcodePrintFormtObj));
            MemoryStream __memoryStream = new MemoryStream();
            __xs.Serialize(__memoryStream, __obj);

            string _query = string.Format("insert into " + _g.d.sml_barcode_print_format._table + "(" + _g.d.sml_barcode_print_format._code + "," + _g.d.sml_barcode_print_format._name + "," + _g.d.sml_barcode_print_format._formatdata + ") VALUES('{0}','{1}',?)", _codeTextBox.Text, _nameTextBox.Text);

            byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.sml_barcode_print_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.sml_barcode_print_format._code) + "='" + _codeTextBox.Text.ToUpper() + "'");
            string __result = __myFrameWork._queryByteData(MyLib._myGlobal._databaseName, _query, new object[] { __memoryStreamCompress });
            if (__result.Equals(""))
            {
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย");
            }
            else
            {
                MessageBox.Show(__result, "wraning");
            }

        }

        private void _formatDeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการลบรูปแบบนี้หรือไม่ ", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.sml_barcode_print_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.sml_barcode_print_format._code) + "='" + _codeTextBox.Text.ToUpper() + "' and " + _g.d.sml_barcode_print_format._name + "='" + _nameTextBox.Text + "'");
                MessageBox.Show("ลบข้อมูลสำเร็จ", "Complete");
            }
        }

        private void _logoSelectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog __file = new OpenFileDialog();
            __file.Filter = "Logo file (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (__file.ShowDialog() == DialogResult.OK)
                this._logoTextBox.Text = __file.FileName;
        }

        private void _shelfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox __sender = (CheckBox)sender;
            if (__sender.Checked)
            {
                this._labelWidthTextBox.Text = "8.00";
                this._labelHeightTextBox.Text = "5.00";
                this._maxRowTextBox.Text = "5";
                this._maxColumnTextBox.Text = "2";
            }
        }

        private void _customPaperSizeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this._customWidthLabel.Enabled = this._customPaperSizeCheckbox.Checked;
            this._customHeightLabel.Enabled = this._customPaperSizeCheckbox.Checked;
            this._customWidthTextbox.Enabled = this._customPaperSizeCheckbox.Checked;
            this._customHeightTextbox.Enabled = this._customPaperSizeCheckbox.Checked;
        }
    }

    class _barCodeDetail
    {
        public int _page;
        public int _row;
        public int _column;
        public string _barCode;
        public string _itemCode;
        public string _unitCode;
        public string _unitName;
        public string _name;
        public decimal _price;
        public decimal _member_price;
        public decimal _formula_price;
    }

    public class _barcodePrintFormtObj
    {
        public String _code = "";
        public String _name = "";
        public _formatUnit _unit = _formatUnit.Centimeter;
        public Boolean _showPrice = true;
        public int _priceIndex = 0;
        public float _topMargin = 0.5f;
        public float _leftMargin = 0.02f;
        public float _width = 4f;
        public float _height = 2f;
        public int _row = 14;
        public int _column = 5;
        public int _startRow = 1;
        public int _startColumn = 1;
        public String _companyName = "";
        public Boolean _formPrint = false;
        public String _formCode = "";
        public Boolean _useUnitName = false;
        public Boolean _customPaperSize = false;
        public float _customPaperWidth = 0f;
        public float _customPaperHeight = 0f;
    }

    public enum _formatUnit
    {
        Centimeter,
        Inch
    }
}
