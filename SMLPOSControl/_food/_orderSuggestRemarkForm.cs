using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _orderSuggestRemarkForm : MyLib._myForm
    {
        private string _suggest_remark = "";

        public string _suggest_select_value = "";
        public _orderSuggestRemarkForm(string suggest_remark)
        {
            InitializeComponent();
            this._suggest_remark = suggest_remark;

            _process_suggest();
        }

        void _process_suggest()
        {
            string[] _suggest = this._suggest_remark.Split(',');
            this.SuspendLayout();
            for (int __i = 0; __i < _suggest.Length; __i++)
            {
                if (_suggest[__i].Length > 0)
                    _addSuggestButton(_suggest[__i]);
            }
            this.ResumeLayout();
        }

        void _addSuggestButton(string __suggestText)
        {
            MyLib.VistaButton __button = new MyLib.VistaButton();

            __button.ButtonText = __suggestText;
            __button.Tag = __suggestText;
            __button.Size = new Size(100, 60);
            __button.Click += (s1, e1) =>
            {
                MyLib.VistaButton __buttonSender = (MyLib.VistaButton)s1;
                if (this._remarkTextBox.TextBox.Text.Length > 0)
                {
                    this._remarkTextBox.TextBox.Text = this._remarkTextBox.TextBox.Text + ",";
                }
                this._remarkTextBox.TextBox.Text = this._remarkTextBox.TextBox.Text + __buttonSender.Tag.ToString();
                this._remarkTextBox.TextBox.Invalidate();
            };

            this._myFlowLayoutPanel1.Controls.Add(__button);
        }

        private void _buttonClear_Click(object sender, EventArgs e)
        {
            this._remarkTextBox.TextBox.Text = "";
            this._remarkTextBox.TextBox.Invalidate();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            this._suggest_select_value = this._remarkTextBox.TextBox.Text;
            this.Close();
        }
    }
}
