using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAPARControl
{
    public partial class _ap_ar_vat_buy : Form
    {
        private StringBuilder __resource;

        public StringBuilder _resource
        {
            get { return __resource; }
            set { __resource = value; }
        }


        private string _save_data_name;

        public string save_data_name
        {
            get { return _save_data_name; }
            set { _save_data_name = value; }
        }

        public _ap_ar_vat_buy()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _setControlEnable(_g.d.gl_journal._doc_date, false);
            __resource = new StringBuilder();
        }

        void _setControlEnable(string controlName, bool _bool)
        {
            try
            {
                MyLib._myTextBox _getControlName = (MyLib._myTextBox)this._screenTop._getControl(controlName);
                _getControlName.Enabled = _bool;
                _getControlName.TextBox.BackColor = ((_bool == true) ? System.Drawing.Color.White : System.Drawing.Color.WhiteSmoke);
            }
            catch (Exception ex)
            {
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            if (keyData == Keys.F12)
            {
                if (this._save_data())
                {
                    this.Close();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        public void _setText(string _fild_name, string _date_value)
        {
            this._screenTop._setDataStr(_fild_name, _date_value);
        }

        public void _setDate(string _fild_name, string _date_value)
        {
            this._screenTop._setDataDate(_fild_name, MyLib._myGlobal._convertDate(_date_value));
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (this._save_data())
            {
                this.Close();
            }
        }

        private bool _save_data()
        {
            try
            {
                ArrayList __getData = this._screenTop._createQueryForDatabase();
                __resource = new StringBuilder();
                Control __getControl = this._screenTop._getControl(_g.d.gl_journal._doc_date);
                DateTime __getDate = ((MyLib._myDateBox)__getControl)._dateTime;
                int __periodNumber = _g.g._accountPeriodFind(__getDate);
                // grid
                string __fieldList = _g.d.gl_journal_vat_buy._doc_date + "," + _g.d.gl_journal_vat_buy._book_code + "," + _g.d.gl_journal_vat_buy._doc_no + "," + _g.d.gl_journal._journal_type + "," + _g.d.gl_journal._ap_ar_code + "," + _g.d.gl_journal._ap_ar_code + ",";
                string __dataList = this._screenTop._getDataStrQuery(_g.d.gl_journal_vat_buy._doc_date) + "," + this._screenTop._getDataStrQuery(_g.d.gl_journal._book_code) + "," + this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_no) + "," +
                    this._screenTop._getDataStr(_g.d.gl_journal._journal_type) + "," + this._screenTop._getDataStr(_g.d.gl_journal._ap_ar_code) + "," + this._screenTop._getDataStr(_g.d.gl_journal._ap_ar_originate_from) + ",";
                //
                __resource = new StringBuilder();

                if (this._vatBuy1._vatGrid._rowData.Count > 0)
                {
                    this._vatBuy1._vatGrid._updateRowIsChangeAll(true);
                    // ต่อท้ายด้วย Insert บรรทัดใหม่
                    __resource.Append(this._vatBuy1._vatGrid._createQueryForInsert(_g.d.gl_journal_vat_buy._table, __fieldList, __dataList, true));
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
