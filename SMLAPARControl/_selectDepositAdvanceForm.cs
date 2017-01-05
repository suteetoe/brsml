using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPARControl
{
    public partial class _selectDepositAdvanceForm : Form
    {
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);
        private _g.g._depositAdvanceEnum _mode;

        public _selectDepositAdvanceForm(_g.g._depositAdvanceEnum mode)
        {
            InitializeComponent();
            //
            this._mode = mode;
            string __custNameField = "";
            switch (mode)
            {
                case _g.g._depositAdvanceEnum.รับเงินมัดจำ:
                case _g.g._depositAdvanceEnum.รับเงินล่วงหน้า:
                    __custNameField = _g.d.ap_ar_resource._ar_name;
                    break;
                case _g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า:
                case _g.g._depositAdvanceEnum.จ่ายเงินมัดจำ:
                    __custNameField = _g.d.ap_ar_resource._ap_name;
                    break;
            }
            this._resultGrid._isEdit = false;
            this._resultGrid._table_name = _g.d.ap_ar_resource._table;
            this._resultGrid._addColumn(_g.d.ap_ar_resource._select, 11, 10, 5);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_no, 1, 10, 15);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_date, 4, 10, 10);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._ar_name, 1, 10, 25, false, false, false, false, "", "", "", __custNameField);
            //
            this._resultGrid._addColumn(_g.d.ap_ar_resource._amount, 3, 10, 15, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._use_amount, 3, 10, 15, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._balance_amount, 3, 15, 15, false, false, false, false, _formatNumberAmount);
            //
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._amount, Color.AliceBlue);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
        }

        public void _process(string custCode,DateTime processDate)
        {
            SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
            this._resultGrid._clear();
            this._resultGrid._loadFromDataTable(__process._depositBalanceDoc(this._mode,"", 0, custCode, custCode, "", "", processDate, ""));
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._resultGrid._rowData.Count; __row++)
            {
                this._resultGrid._cellUpdate(__row, _g.d.ap_ar_resource._select, 1, false);
            }
            this._resultGrid.Invalidate();
        }
    }
}
