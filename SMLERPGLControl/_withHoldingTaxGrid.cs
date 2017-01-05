using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGLControl
{
    public partial class _withHoldingTaxGrid : MyLib._myGrid
    {
        public _withHoldingTaxType _whType;
        MyLib._searchDataFull __searchCustCode = new MyLib._searchDataFull();
        string _loadViewFormat = "";
        string __cust_code;

        public string _cust_code
        {
            get { return __cust_code; }
            set { __cust_code = value; }
        }

        public _withHoldingTaxGrid(_withHoldingTaxType whType)
        {
            this._whType = whType;
            this._build(whType);
            _loadViewFormat = _g.g._search_screen_ap;
            if ((whType == _withHoldingTaxType.ขาย) || (whType == _withHoldingTaxType.ลูกหนี้_รับชำระหนี้) || (whType == _withHoldingTaxType.ลูกหนี้_ยกเลิกรับชำระหนี้)) _loadViewFormat = _g.g._search_screen_ar;
            //
            int __columnNumber = this._findColumnByName(_g.d.gl_wht_list_detail._cust_code);
            this.__searchCustCode.StartPosition = FormStartPosition.CenterScreen;
            this.__searchCustCode.Text = ((MyLib._myGrid._columnType)this._columnList[__columnNumber])._name;
            this.__searchCustCode._dataList._loadViewFormat(_loadViewFormat, MyLib._myGlobal._userSearchScreenGroup, false);
            this.__searchCustCode._dataList._refreshData();
        }

        public string _queryForInsert(string docDate, string docNo, int transFlag)
        {
            try
            {
                // MOO
                for (int row = 0; row < this._rowData.Count; row++)
                {
                    decimal __amount = (decimal)this._cellGet(row, _g.d.gl_wht_list_detail._amount);
                    if (__amount == 0)
                    {
                        this._rowData.RemoveAt(row);
                        row--;
                    }
                }
            }
            catch
            {
            }
            string __fieldList = _g.d.gl_wht_list_detail._doc_no + "," + _g.d.gl_wht_list_detail._doc_date + "," + _g.d.gl_wht_list_detail._trans_flag + ",";
            string __dataList = docNo + "," + docDate + "," + transFlag.ToString() + ",";
            string __dataListDelete = _g.d.gl_wht_list_detail._doc_no + " = " + docNo + " and " + _g.d.gl_wht_list_detail._doc_date + " = " + docDate + " and " + _g.d.gl_wht_list_detail._trans_flag + " = " + transFlag.ToString() + "";
            this._updateRowIsChangeAll(true);
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_wht_list_detail._table + " where " + __dataListDelete));
            __myQuery.Append(this._createQueryForInsert(_g.d.gl_wht_list_detail._table, __fieldList, __dataList, false, true));
            return __myQuery.ToString();
        }

        public void _custCode(string cust_code)
        {
            this._cust_code = cust_code;
        }

        public decimal _sumVatValue()
        {
            try
            {
                decimal _sum_vat_pay = ((MyLib._myGrid._columnType)this._columnList[this._findColumnByName(_g.d.gl_wht_list_detail._tax_value)])._total;
                return _sum_vat_pay;
            }
            catch
            {
                return 0M;
            }
        }

        private void _build(_withHoldingTaxType whType)
        {
            this._columnList.Clear();
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);
            this._width_by_persent = true;
            this._total_show = true;
            //
            this._table_name = _g.d.gl_wht_list_detail._table;
            this._addColumn(_g.d.gl_wht_list_detail._income_type, 1, 1, 20, true, false, true, false);
            this._addColumn(_g.d.gl_wht_list_detail._amount, 3, 1, 14, true, false, true, false, __formatNumberAmount);
            this._addColumn(_g.d.gl_wht_list_detail._tax_rate, 3, 1, 5, true, false, true, false, __formatNumberAmount);
            this._addColumn(_g.d.gl_wht_list_detail._tax_value, 3, 1, 12, false, false, true, false, __formatNumberAmount);
            this._addColumn(_g.d.gl_wht_list_detail._sum_amount, 3, 1, 14, false, false, true, false, __formatNumberAmount);
            this._addColumn(_g.d.gl_wht_list_detail._due_date, 4, 1, 15, true, false, true, true);
            if ((whType == _withHoldingTaxType.ขาย) || (whType == _withHoldingTaxType.ลูกหนี้_รับชำระหนี้) || (whType == _withHoldingTaxType.ลูกหนี้_ยกเลิกรับชำระหนี้))
            {
                this._addColumn(_g.d.gl_wht_list_detail._cust_code, 1, 1, 20, true, false, true, true, "", "", "", _g.d.gl_wht_list_detail._ar_code);
            }
            else
            {
                this._addColumn(_g.d.gl_wht_list_detail._cust_code, 1, 1, 20, true, false, true, true, "", "", "", _g.d.gl_wht_list_detail._ap_code);
            }
            this._width_by_persent = true;
            this.AllowDrop = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;
            //
            this._calcPersentWidthToScatter();
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_withHoldingTaxGrid__afterAddRow);
            this._clickSearchButton += new MyLib.SearchEventHandler(_withHoldingTaxGrid__clickSearchButton);
            //
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_withHoldingTaxGrid__alterCellUpdate);
        }

        void _withHoldingTaxGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            this.__searchCustCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this.__searchCustCode.ShowDialog();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this.__searchCustCode.Close();
            string __custCode = "";
            if ((_whType == _withHoldingTaxType.ขาย) || (_whType == _withHoldingTaxType.ลูกหนี้_รับชำระหนี้) || (_whType == _withHoldingTaxType.ลูกหนี้_ยกเลิกรับชำระหนี้))
            {
                __custCode = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
            }
            else
            {
                __custCode = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code).ToString();
            }
            this._cellUpdate(this._selectRow, _g.d.gl_wht_list_detail._cust_code, __custCode, true);
        }

        void _withHoldingTaxGrid__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.gl_wht_list_detail._tax_rate, _g.g._companyProfile._wht_rate, false);
        }

        void _withHoldingTaxGrid__alterCellUpdate(object sender, int row, int column)
        {
            this._alterCellUpdate -= new MyLib.AfterCellUpdateEventHandler(_withHoldingTaxGrid__alterCellUpdate);
            //
            decimal __amount = (decimal)this._cellGet(row, _g.d.gl_wht_list_detail._amount);
            //if (__amount != 0) this._cellUpdate(row, _g.d.gl_wht_list_detail._tax_rate, _g.g._companyProfile._wht_rate, false);
            decimal __taxRate = (decimal)this._cellGet(row, _g.d.gl_wht_list_detail._tax_rate);
            decimal __taxValue = (__taxRate == 0) ? 0 : __amount * (__taxRate / 100M);

            this._cellUpdate(row, _g.d.gl_wht_list_detail._tax_value, __taxValue, false);
            this._cellUpdate(row, _g.d.gl_wht_list_detail._sum_amount, (__amount - __taxValue), false);
            if (!this._cust_code.Equals("")) this._cellUpdate(row, _g.d.gl_wht_list_detail._cust_code, this._cust_code, false);
            //
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_withHoldingTaxGrid__alterCellUpdate);
        }

    }
}
