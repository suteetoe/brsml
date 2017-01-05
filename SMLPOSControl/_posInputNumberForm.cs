using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _posInputNumberForm : Form
    {
        public string _getNumberValue
        {
            get
            {
                return _textNumber.Text;
            }
        }

        public _posInputNumberForm()
        {
            InitializeComponent();
            this._textNumber.KeyUp += new KeyEventHandler(_textNumber_KeyUp);
            this._textNumber.Focus();
        }

        public _posInputNumberForm(string _screenName)
        {
            InitializeComponent();
            this._titleLabel.Text = _screenName;
            this._textNumber.KeyUp += new KeyEventHandler(_textNumber_KeyUp);
            this._textNumber.Focus();
        }

        void _textNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else if (e.KeyData == Keys.Escape)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        private void _buttonSelect_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void _buttonEsc_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            //__textBoxList[__loop].Text.Remove(__textBoxList[__loop].Text.Length - 1, 1);
        }

        private void _buttonNumber_click(object sender, EventArgs e)
        {
            Button _sender = (Button)sender;
            if (_textNumber.SelectionStart == _textNumber.Text.Length)
            {
                _textNumber.Text += (_textNumber.Text.IndexOf("%") == -1) ? _sender.Text : "";
                _textNumber.Focus();
                _textNumber.SelectionStart = _textNumber.Text.Length;
            }
            else
            {
                _textNumber.Focus();
                int __index = _textNumber.SelectionStart;
                if (_textNumber.Text.IndexOf("%") == -1)
                {
                    _textNumber.Text = _textNumber.Text.Insert(__index, _sender.Text);
                    _textNumber.SelectionStart = __index + 1;

                }
                else
                    _textNumber.SelectionStart = __index;
            }
        }

        private void _buttonDot_Click(object sender, EventArgs e)
        {
            if (_textNumber.Text.IndexOf(".") == -1)
            {
                if (_textNumber.SelectionStart == _textNumber.Text.Length)
                {
                    _textNumber.Text += ".";
                    _textNumber.SelectionStart = _textNumber.Text.Length;
                }
                else
                {
                    _textNumber.Focus();
                    int __index = _textNumber.SelectionStart;
                    _textNumber.Text = _textNumber.Text.Insert(__index, ".");
                    //_textNumber.Focus();
                    _textNumber.SelectionStart = __index + 1;
                }
            }
            else
            {
                _textNumber.Focus();
                int __defaultIndex = _textNumber.SelectionStart;
                _textNumber.SelectionStart = __defaultIndex;
            }

        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_textNumber.Text.Length > 0)
                {
                    _textNumber.Focus();
                    int __newSelectindex = (_textNumber.SelectionStart == _textNumber.Text.Length) ? _textNumber.Text.Length - 1 : _textNumber.SelectionStart - 1;
                    _textNumber.Text = _textNumber.Text.Remove(((_textNumber.SelectionStart == _textNumber.Text.Length) ? _textNumber.Text.Length - 1 : _textNumber.SelectionStart - 1), 1);
                    _textNumber.SelectionStart = __newSelectindex;
                }
            }
            catch
            {
            }
        }

        private void _butonLeft_Click(object sender, EventArgs e)
        {
            _textNumber.Focus();
            if (_textNumber.Text.Length > 0)
                _textNumber.SelectionStart = _textNumber.SelectionStart - 1;
            else
                _textNumber.SelectionStart = 0;

        }

        private void _buttonRight_Click(object sender, EventArgs e)
        {
            _textNumber.Focus();
            if (_textNumber.Text.Length <= _textNumber.Text.Length)
                _textNumber.SelectionStart = _textNumber.SelectionStart + 1;
            else
                _textNumber.SelectionStart = _textNumber.Text.Length;

        }

        private void _buttonPercent_Click(object sender, EventArgs e)
        {
            _textNumber.Focus();
            if (_textNumber.Text.IndexOf("%") == -1)
                _textNumber.Text += "%";
            _textNumber.SelectionStart = _textNumber.Text.Length;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
