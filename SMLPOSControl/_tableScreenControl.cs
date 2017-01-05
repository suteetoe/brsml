using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _tableScreenControl : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        int _dataRow = 0;
        int _lineCount = 0;
        int _printRow = 0;
        int _printColumn = 0;
        DataTable __dataTable = null;
        /// <summary>
        /// 0=QRCode,1=Barcode
        /// </summary>
        int _printMode = 0;

        public _tableScreenControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._myScreen1._table_name = _g.d.table_master._table;
            this._myScreen1._addTextBox(0, 0, 0, 0, _g.d.table_master._number, 1, 10, 0, true, false, false);
            this._myScreen1._addTextBox(1, 0, 0, 0, _g.d.table_master._name_1, 1, 10, 0, true, false, false);
            this._myScreen1._addTextBox(2, 0, 0, 0, _g.d.table_master._level_1, 1, 10, 0, true, false, false);
            this._myScreen1._addTextBox(3, 0, _g.d.table_master._level_2, 10);
            this._myScreen1._addTextBox(4, 0, _g.d.table_master._level_3, 10);
            this._myScreen1._addTextBox(5, 0, _g.d.table_master._level_4, 10);
            this._myScreen1._addNumberBox(6, 0, 0, 0, _g.d.table_master._priority, 1, 0, true);
            this._myScreen1._addTextBox(7, 0, 0, 0, _g.d.table_master._table_barcode, 1, 10, 0, true, false, true);
            this._myScreen1._addTextBox(8, 0, 0, 0, _g.d.table_master._table_qrcode, 1, 10, 0, true, false, true);
            this._myScreen1._enabedControl(_g.d.table_master._table_qrcode, false);
            this._qrCodeGenButton.Click += _qrCodeGenButton_Click;
            this._qrCodePrintButton.Click += _qrCodePrintButton_Click;
            this._printDocument.PrintPage += _printDocument_PrintPage;
            this._printDocument.BeginPrint += _printDocument_BeginPrint;
        }

        void _printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _dataRow = 0;
            _lineCount = 0;
            _printRow = 0;
            _printColumn = 0;
        }

        void _printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            SMLReport._design._drawImageField __barcode = new SMLReport._design._drawImageField(); //SMLReport._design._drawImageField._getBarcodeImage
            while (_dataRow < __dataTable.Rows.Count)
            {
                string __tableNumber = __dataTable.Rows[_dataRow][0].ToString();
                string __code = __dataTable.Rows[_dataRow][1].ToString();
                _dataRow++;
                Font __drawFont = new Font("Tahoma", 16);
                float __x = (_printColumn * 100) + 40;
                float __y = (_printRow * 160) + 40;

                if (_printMode == 1)
                {
                    __x = (_printColumn * 110) + 40;
                    __y = (_printRow * 160) + 40;
                }

                e.Graphics.DrawString(__tableNumber, __drawFont, Brushes.Black, __x + 30, __y);
                if (this._printMode == 0)
                {
                    string __url = "http://www.smlsoft.com/t.php?id=" + MyLib._myGlobal._productCode + "&d=" + MyLib._myGlobal._databaseName + "&p=" + MyLib._myGlobal._providerCode + "&c=" + __code;

                    if (_isQRLocal)
                    {
                        __url = "http://" + MyLib._myGlobal._getFirstWebServiceServer + "/SMLJavaWebService/orderlist.jsp?id=" + MyLib._myGlobal._productCode + "&d=" + MyLib._myGlobal._databaseName + "&p=" + MyLib._myGlobal._providerCode + "&c=" + __code;
                    }
                    byte[] __dateByte = _myFrameWork._qrCodeByte(__url);
                    Image __image = Image.FromStream(new MemoryStream(__dateByte));
                    e.Graphics.DrawImage(__image, __x, __y + 30);
                }
                else
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    Image __image = __barcode._getBarcodeImage(__code, new Size(120, 80), SMLReport._design._textAlign.Center, SMLReport._design._barcodeType.BarCode_Code128, true, SMLReport._design._barcodeLabelPosition.TopLeft, __drawFont, RotateFlipType.RotateNoneFlipNone, System.Drawing.Color.Black, System.Drawing.Color.White);
                    e.Graphics.DrawImage(__image, new RectangleF(__x, __y + 30, 120, 80));
                }
                _printColumn++;
                if (_printColumn > 6)
                {
                    _printColumn = 0;
                    _printRow++;
                    _lineCount++;
                    if (_lineCount > 6)
                    {
                        _printRow = 0;
                        _lineCount = 0;
                        e.HasMorePages = true;
                        break;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }
            }
        }

        void _printCode(int mode)
        {
            this._printMode = mode;
            _dataRow = 0;
            _lineCount = 0;
            _printRow = 0;
            _printColumn = 0;
            __dataTable = null;
            //
            try
            {
                if (mode == 0)
                {
                    __dataTable = _myFrameWork._queryShort("select " + _g.d.table_master._number + "," + _g.d.table_master._table_qrcode + " from " + _g.d.table_master._table + " order by " + _g.d.table_master._number).Tables[0];
                }
                else
                {
                    __dataTable = _myFrameWork._queryShort("select " + _g.d.table_master._number + "," + _g.d.table_master._table_barcode + " from " + _g.d.table_master._table + " where " + _g.d.table_master._table_barcode + " is not null and " + _g.d.table_master._table_barcode + "<>\'\' order by " + _g.d.table_master._number).Tables[0];
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
            //
            _printDialog.Document = _printDocument;
            if (_printDialog.ShowDialog() == DialogResult.OK)
            {
                _printPreviewDialog.Document = _printDocument;
                _printPreviewDialog.ShowDialog();
            }
        }

        void _qrCodePrintButton_Click(object sender, EventArgs e)
        {
            this._printCode(0);
        }

        void _qrCodeGenButton_Click(object sender, EventArgs e)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __table = __myFrameWork._queryShort("select " + _g.d.table_master._number + " from " + _g.d.table_master._table + " where " + _g.d.table_master._table_qrcode + "=\'\' or " + _g.d.table_master._table_qrcode + " is null").Tables[0];
                //DataTable __table = __myFrameWork._queryShort("select " + _g.d.table_master._number + " from " + _g.d.table_master._table).Tables[0];
                for (int __row = 0; __row < __table.Rows.Count; __row++)
                {
                    while (true)
                    {
                        string __ticks = DateTime.Now.Ticks.ToString();
                        __ticks = __ticks.Substring(__ticks.Length - 6);
                        DataTable __tableCheck = __myFrameWork._queryShort("select " + _g.d.table_master._number + " from " + _g.d.table_master._table + " where " + _g.d.table_master._table_qrcode + "=\'" + __ticks + "\'").Tables[0];
                        if (__tableCheck.Rows.Count == 0)
                        {
                            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.table_master._table + " set " + _g.d.table_master._table_qrcode + "=\'" + __ticks + "\' where " + _g.d.table_master._number + "=\'" + __table.Rows[__row][0].ToString() + "\'");
                            break;
                        }
                    }
                }
                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private Boolean _isQRLocal = false;

        private void _barcodePrintButton_Click(object sender, EventArgs e)
        {
            this._printCode(1);
        }

        private void _qrCodePrintButton_Click_1(object sender, EventArgs e)
        {

        }

        private void _qrCodeLocalButton_Click(object sender, EventArgs e)
        {
            _isQRLocal = true;
            this._printCode(0);
            _isQRLocal = false;
        }
    }
}
