using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SMLInventoryControl
{
    public partial class _selectBillFromPosForm : Form
    {
        public event SelectInvoiceEventHandler _selectInvoce;
        public string _extraWhere = "";
        public _selectBillFromPosForm()
        {
            InitializeComponent();
        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            string __docNoRef = this._textBoxBillNo.Text.Trim().ToUpper();
            string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._tax_doc_date, _g.d.ic_trans._tax_doc_no, _g.d.ic_trans._vat_rate, _g.d.ic_trans._cashier_code, _g.d.ic_trans._cust_code, _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._inquiry_type, _g.d.ic_trans._vat_type, _g.d.ic_trans._last_status, _g.d.ic_trans._pos_id, _g.d.ic_trans._is_pos, _g.d.ic_trans._member_code);
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + __field + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no_guid) + "=\'" + __docNoRef + "\' or " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + __docNoRef + "\'" + _extraWhere;
            DataTable __docDataTable = __myFrameWork._queryShort(__query).Tables[0];
            if (__docDataTable.Rows.Count > 0)
            {
                string __docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                this.Close();
                this._selectInvoce(__docNo);
            }
            else
            {
                MessageBox.Show("ไม่พบเอกสารเลขที่" + " : " + __docNoRef);
            }
        }
        public delegate void SelectInvoiceEventHandler(string docNo);
    }
}
