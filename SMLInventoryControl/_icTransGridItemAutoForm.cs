using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransGridItemAutoForm : Form
    {
        public delegate void EnterKeyHandler(string code,decimal qty);
        public event EnterKeyHandler _enterKey;

        public _icTransGridItemAutoForm()
        {
            InitializeComponent();
            this._barcodeTextBox.Focus();

            // toe load weight digital config
            if (_g.g._companyProfile._digital_barcode_scale)
            {

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        string __code = this._barcodeTextBox.Text.Trim().ToUpper();
                        decimal __qty = 1M;
                        if (this._barcodeTextBox.Text.IndexOf('*') != -1)
                        {
                            string[] __split = this._barcodeTextBox.Text.Split('*');
                            __code = __split[1].Trim();
                            __qty = MyLib._myGlobal._decimalPhase(__split[0].Trim());
                        }
                        this._enterKey(__code, __qty);
                        this._lastBarcodeLabel.Text = this._barcodeTextBox.Text;
                        this._barcodeTextBox.Text = "";
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
