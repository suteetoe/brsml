using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGLControl
{
    public partial class _searchVatForm : Form
    {
        string _ap_code = "";

        public _searchVatForm()
        {
            InitializeComponent();

            string formatNumber = MyLib._myGlobal._getFormatNumber("m02");

            this._resultGrid._table_name = _g.d.gl_journal_vat_buy._table;
            this._resultGrid._width_by_persent = true;
            this._resultGrid._isEdit = false;
            this._resultGrid._addColumn(_g.d.ap_ar_resource._select, 11, 10, 5);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_date, 4, 0, 12);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_doc_no, 1, 25, 12, true, false);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_effective_period, 2, 0, 9, true, false, true, false);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_effective_year, 2, 0, 9, true, false, true, false);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_base_amount, 3, 0, 9, true, false, true, false, formatNumber);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_rate, 3, 0, 8, true, false, true, false, formatNumber);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_amount, 3, 0, 12, true, false, true, false, formatNumber);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_total_amount, 3, 0, 12, true, false, true, false, formatNumber);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_except_amount_1, 3, 0, 12, true, false, true, false, formatNumber);

            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_new_number, 1, 5, 8, true, true);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_description, 1, 255, 0, true, true);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_group, 1, 10, 0, true, true, true, true);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._vat_average, 3, 0, 0, true, true, true, false, formatNumber);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._doc_date, 4, 0, 12, true, true);
            this._resultGrid._addColumn(_g.d.gl_journal_vat_buy._doc_no, 1, 25, 12, true, true);

            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
            this._resultGrid.Invalidate();

        }

        public void _process(string _custCode)
        {
            this._ap_code = _custCode;
            string __query = "select * from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._vat_type + "=2 and " + _g.d.gl_journal_vat_buy._ap_code + "=\'" + this._ap_code + "\' and vat_amount <> (select coalesce(sum(vat_amount), 0) from gl_journal_vat_buy as x where x.ref_vat_no = gl_journal_vat_buy.vat_doc_no)  ";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort(__query).Tables[0];
            this._resultGrid._loadFromDataTable(__result);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
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
