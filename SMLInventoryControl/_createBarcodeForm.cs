using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _createBarcodeForm : Form
    {
        public _createBarcodeForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(_createBarcodeForm_Load);
            this._firstDigitTextBox.TextChanged += new EventHandler(_firstDigitTextBox_TextChanged);
        }

        void _firstDigitTextBox_TextChanged(object sender, EventArgs e)
        {
            this._genBarcode();
        }

        void _createBarcodeForm_Load(object sender, EventArgs e)
        {
            this._genBarcode();
        }

        private void _genBarcode()
        {
            try
            {
                SMLBarcodeManage._itemBarcodeCheckerEan13 __barcode = new SMLBarcodeManage._itemBarcodeCheckerEan13();

                string _firstdigit = this._firstDigitTextBox.Text.Trim().ToUpper();
                if (_firstdigit.Length == 0)
                {
                    MyLib.RandomStringGenerator __random = new MyLib.RandomStringGenerator();
                    _firstdigit = __random.NextString(1, false, false, true, false, false);
                }
                this._barCodelabel.Text = __barcode._genBarcode(_firstdigit);
                this._barCodelabel.Invalidate();
            }
            catch
            {
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
