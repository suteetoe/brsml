using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _posChangeDetailForm : Form
    {
        _posClientItemClass _item = null;
        public _posChangeDetailForm(_posClientItemClass __item)
        {
            InitializeComponent();
            _item = __item;
            _build();

        }

        private void _build()
        {
            if (_item != null)
            {
                _itemCodeTextbox.Text = _item._itemCode;
                _itemBarcodeTextbox.Text = _item._barCode;
                _itemNameTextbox.Text = _item._itemName;
                _dozenTextbox.Text = _item._m_dozen;
                _frequencyTextbox.Text = _item._m_frequency;
                _timesTextbox.Text = _item._m_times;
                _wraningTextbox.Text = _item._m_warning;
                _remarkTextbox.Text = _item._m_remark;
            }
        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape :
                    _buttonCancel_Click(this, null);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
