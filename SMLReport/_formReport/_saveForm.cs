using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._formReport
{
    public partial class _saveForm : Form
    {

        public _saveForm()
        {
            InitializeComponent();
            this._formTypeCombobox.Items.Clear();
            this._formTypeCombobox.Items.Add(MyLib._myResource._findResource(_g.d.formdesign._table + "." + _g.d.formdesign._form_all)._str);
            this._formTypeCombobox.Items.Add(MyLib._myResource._findResource(_g.d.formdesign._table + "." + _g.d.formdesign._form_print)._str);
            this._formTypeCombobox.Items.Add(MyLib._myResource._findResource(_g.d.formdesign._table + "." + _g.d.formdesign._form_barcode)._str);
            this._formTypeCombobox.Items.Add(MyLib._myResource._findResource(_g.d.formdesign._table + "." + _g.d.formdesign._form_transportlabel)._str);
        }

        protected override void OnShown(EventArgs e)
        {
            this._saveFormCode.Focus();
            base.OnShown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter :
                    _buttonSave_Click(this, null);
                    return true;
                case Keys.Escape:
                    _buttonCancle_Click(this, null);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void _buttonCancle_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            if (( this._saveFormCode.Text == "") || (this._saveFormName.Text == "" ))
            {
                return;
            }

            if (_afterSaveButtonClick != null)
            {
                _afterSaveButtonClick(this, this._saveFormCode.Text, this._saveFormName.Text);
            }
            this.Dispose();
        }
        public event AfterClickSaveButton _afterSaveButtonClick;
    }

    public delegate void AfterClickSaveButton(object sender, string fileCode, string fileName);
}
