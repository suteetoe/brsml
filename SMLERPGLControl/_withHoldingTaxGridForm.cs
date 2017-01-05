using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGLControl
{
    public partial class _withHoldingTaxGridForm : Form
    {       
        private _withHoldingTaxType _whType;
        public _withHoldingTaxGrid _grid;

        public _withHoldingTaxGridForm(_withHoldingTaxType whType)
        {
            InitializeComponent();
            this._whType = whType;
            this._grid = new _withHoldingTaxGrid(whType);
            this._grid.Dock = DockStyle.Fill;
            this.Controls.Add(this._grid);
        }

        public string _queryForInsert(string docDate, string docNo, int transFlag)
        {
            try
            {
                // MOO
                for (int row = 0; row < this._grid._rowData.Count; row++)
                {
                    decimal __amount = (decimal)this._grid._cellGet(row, _g.d.gl_wht_list_detail._amount);
                    if (__amount == 0)
                    {
                        this._grid._rowData.RemoveAt(row);
                        row--;
                    }
                }
            }
            catch
            {
            }
            string __fieldList = _g.d.gl_wht_list_detail._doc_no + "," + _g.d.gl_wht_list_detail._doc_date + "," + _g.d.gl_wht_list_detail._trans_flag + ",";
            string __dataList = docNo + "," + docDate + "," + transFlag.ToString() + ",";
            string __dataListDelete = _g.d.gl_wht_list_detail._doc_no +" = "+docNo+" and "+_g.d.gl_wht_list_detail._doc_date + " = "+docDate+" and "+ _g.d.gl_wht_list_detail._trans_flag+ " = " + transFlag.ToString() + "";
            this._grid._updateRowIsChangeAll(true);
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_wht_list_detail._table + " where " + __dataListDelete));
            __myQuery.Append(this._grid._createQueryForInsert(_g.d.gl_wht_list_detail._table, __fieldList, __dataList, false, true));
            return __myQuery.ToString();
        }

        public void _custCode(string cust_code)
        {
            this._grid._cust_code = cust_code;
        }

        public decimal _sumVatValue()
        {
            try
            {
                decimal _sum_vat_pay = ((MyLib._myGrid._columnType)this._grid._columnList[this._grid._findColumnByName(_g.d.gl_wht_list_detail._tax_value)])._total;
                return _sum_vat_pay;
            }
            catch
            {
                return 0;
            }
            return 0;
        }



        public void _clear()
        {
            this._grid._clear();
        }

    }
    public enum _withHoldingTaxType
    {
        ว่าง,
        ซื้อ,
        ขาย,
        เจ้าหนี้_จ่ายชำระหนี้,
        เจ้าหนี้_ยกเลิกจ่ายชำระหนี้,
        ลูกหนี้_รับชำระหนี้,
        ลูกหนี้_ยกเลิกรับชำระหนี้
    }
}
