using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLFastReport
{
    public partial class _saveForm : Form
    {
        public event _beforeSaveDesignReportDispose _beforeDispose;
        public _saveForm()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isUserTest || (MyLib._myGlobal._masterWebservice.Equals(MyLib._myGlobal._getFirstWebServiceServer) && MyLib._myGlobal._masterDatabaseName.Equals(MyLib._myGlobal._databaseName)))
            {
                this.Size = new Size(this.Width, 160);
            }
        }

        private void _disposeForm()
        {
            if (_beforeDispose != null)
            {
                _beforeDispose(this, null);
            }
            this.Dispose();
        }

        private void _buttonCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            _disposeForm();

        }

        private void _buttonSave_Click_1(object sender, EventArgs e)
        {

            this._screen._saveLastControl();
            string _errorMessage = "";

            //if (_menuIdTextBox.Text.Trim().Equals(""))
            //{
            //    _errorMessage = ((_errorMessage.Length > 0) ? _errorMessage + "\n" : "") + "Menu Id ";
            //}

            //if (_menuNameTextBox.Text.Trim().Equals(""))
            //{
            //    _errorMessage = ((_errorMessage.Length > 0) ?  _errorMessage + "\n" : "") + "Menu Name ";
            //}

            if (this._screen._getDataStr(_g.d.sml_fastreport._menuid).Trim().Equals(""))
            {
                _errorMessage = ((_errorMessage.Length > 0) ? _errorMessage + "\n" : "") + "Menu Id ";
            }

            if (this._screen._getDataStr(_g.d.sml_fastreport._menuid).Trim().Equals(""))
            {
                _errorMessage = ((_errorMessage.Length > 0) ? _errorMessage + "\n" : "") + "Menu Name ";
            }


            if (_errorMessage.Length > 0)
            {
                MessageBox.Show("กรุณากรอกข้อมูล \n" + _errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            _disposeForm();
        }
    }

    public class _screen_save_report : MyLib._myScreen
    {
        public _screen_save_report()
        {
            this._maxColumn = 1;
            this._autoUpperString = false;
            this._table_name = _g.d.sml_fastreport._table;
            this._addTextBox(0, 0, _g.d.sml_fastreport._menuid, 255);
            this._addTextBox(1, 0, _g.d.sml_fastreport._menuname, 255);
            this._addComboBox(2, 0, _g.d.sml_fastreport._report_type, true, new string[] { _g.d.sml_fastreport._general_report, _g.d.sml_fastreport._inventory_report, _g.d.sml_fastreport._purchase_report, _g.d.sml_fastreport._sale_report, _g.d.sml_fastreport._supplier_report, _g.d.sml_fastreport._customer_report, _g.d.sml_fastreport._cashbank_report, _g.d.sml_fastreport._asset_report, _g.d.sml_fastreport._account_report }, true);

            if (MyLib._myGlobal._isUserTest || (MyLib._myGlobal._masterWebservice.Equals(MyLib._myGlobal._getFirstWebServiceServer) && MyLib._myGlobal._masterDatabaseName.Equals(MyLib._myGlobal._databaseName)))
            {
                this._addTextBox(3, 0, _g.d.sml_fastreport._owner_code, 255);
            }
        }
    }



    public delegate void _beforeSaveDesignReportDispose(object sender, EventArgs e);
}
