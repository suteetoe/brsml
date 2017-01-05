using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _checkPriceUserControl : UserControl
    {
        string _mainMenuId = "";
        string _mainMenuCode = "";
        public bool _isEdit = true;


        public _checkPriceUserControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._barcodeTextBox.KeyDown += (s1, e1) =>
            {
                if (e1.KeyData == Keys.Enter)
                {
                    e1.SuppressKeyPress = true;
                    this._labelBarcode.Text = this._barcodeTextBox.Text;
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __dt = __myFrameWork._queryShort("select " + _g.d.ic_inventory_barcode._description + "," + _g.d.ic_inventory_barcode._price + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "=\'" + this._barcodeTextBox.Text.Trim() + "\'").Tables[0];
                    if (__dt.Rows.Count > 0)
                    {
                        string __itemName = __dt.Rows[0][_g.d.ic_inventory_barcode._description].ToString();
                        decimal __price = MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory_barcode._price].ToString());
                        this._labelItemName.Text = __itemName;
                        this._labelPrice.Text = __price.ToString();
                        if (__price != 0)
                        {
                            MyLib._moneyToVoice __moneyToVoice = new MyLib._moneyToVoice();
                            StringBuilder __sound = new StringBuilder();
                            __sound.Append(__moneyToVoice._toText(__price));
                            __moneyToVoice._toVoice(__sound.ToString());
                        }
                    }
                    else
                    {
                        this._labelItemName.Text = "";
                        this._labelPrice.Text = "";
                    }

                    this._barcodeTextBox.Text = "";
                    this._newPriceTextBox.Text = "";
                }
            };

            this._mainMenuId = MyLib._myGlobal._mainMenuIdPassTrue;
            this._mainMenuCode = MyLib._myGlobal._mainMenuCodePassTrue;

            MyLib._PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(this._mainMenuId, this._mainMenuCode);
            _isEdit = __permission._isEdit;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        void _save()
        {
            if (_isEdit)
            {
                decimal __oldPrice = MyLib._myGlobal._decimalPhase(this._labelPrice.Text.ToString());
                decimal __newPrice = MyLib._myGlobal._decimalPhase(this._newPriceTextBox.Text.ToString());
                string __barCode = this._labelBarcode.Text.Trim().ToUpper();
                if (__barCode.Length > 0)
                {
                    if (__newPrice > 1)
                    {
                        MyLib._moneyToVoice __moneyToVoice = new MyLib._moneyToVoice();
                        StringBuilder __sound = new StringBuilder();
                        __sound.Append(__moneyToVoice._toText(__newPrice));
                        __moneyToVoice._toVoice(__sound.ToString());
                        if (MessageBox.Show("ต้องการเปลี่ยนราคาเป็น" + " : " + __newPrice.ToString() + " " + "บาท", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            string __query = "update " + _g.d.ic_inventory_barcode._table + " set " + _g.d.ic_inventory_barcode._price + "=" + __newPrice.ToString() + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._barcode) + "=\'" + __barCode + "\'";
                            string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);
                            if (__result.Length == 0)
                            {
                                MessageBox.Show("เปลี่ยนราคาสำเร็จ");
                            }
                            else
                            {
                                MessageBox.Show("ผิดพลาด" + " : " + __result.ToString());
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("กรุณายิง Barcode ก่อน");
                }
                this._barcodeTextBox.Focus();
            }
            else
            {
                MessageBox.Show("ไม่อนุญาติให้แก้ไขข้อมูล");
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._save();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F11:
                    this._newPriceTextBox.Focus();
                    return true;
                case Keys.F12:
                    this._save();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
