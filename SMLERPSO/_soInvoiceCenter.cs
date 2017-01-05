using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPSO
{
    public partial class _soInvoiceCenter : UserControl
    {
        DataTable _transTable = new DataTable();
        DataTable _transDetailTable = new DataTable();
        string _custCode = "";
        string _memberCode = "";
        public _soInvoiceCenter(string custCode, string memberCode)
        {

            InitializeComponent();

            this._custCode = custCode;
            this._memberCode = memberCode;
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);


            this._screenSummary._maxColumn = 2;

            int __row = 0;
            this._screenSummary._addTextBox(__row, 0, _g.d.ar_dealer._table + "." + _g.d.ar_dealer._ar_code, 255);
            this._screenSummary._addTextBox(__row++, 1, _g.d.ar_dealer._table + "." + _g.d.ar_dealer._code, 255);

            this._screenSummary._addDateBox(__row, 0, 1, 1, _g.d.ar_dealer._table + "." + _g.d.ar_dealer._regist_date, 1, true);
            this._screenSummary._addDateBox(__row++, 1, 1, 1, _g.d.ar_dealer._table + "." + _g.d.ar_dealer._expire_date, 1, true);

            this._screenSummary._addTextBox(__row, 0, _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1, 255);
            this._screenSummary._addDateBox(__row++, 1, 1, 1, _g.d.ar_customer._table + "." + _g.d.ar_customer._birth_day, 1, true);

            this._screenSummary._addDateBox(__row, 0, 1, 1, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._from_doc_date, 1, true);
            this._screenSummary._addDateBox(__row++, 1, 1, 1, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._to_doc_date, 1, true);

            this._screenSummary._enabedControl(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._ar_code, false);
            this._screenSummary._enabedControl(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._code, false);
            this._screenSummary._enabedControl(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._regist_date, false);
            this._screenSummary._enabedControl(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._expire_date, false);
            this._screenSummary._enabedControl(_g.d.ar_customer._table + "." + _g.d.ar_customer._name_1, false);
            this._screenSummary._enabedControl(_g.d.ar_customer._table + "." + _g.d.ar_customer._birth_day, false);


            this._gridTrans._table_name = _g.d.ic_trans._table;
            this._gridTrans._isEdit = false;
            this._gridTrans._total_show = true;
            this._gridTrans._addColumn(_g.d.ic_trans._doc_date, 4, 10, 10);
            this._gridTrans._addColumn(_g.d.ic_trans._doc_time, 1, 10, 10);
            this._gridTrans._addColumn(_g.d.ic_trans._branch_code, 1, 15, 15);
            this._gridTrans._addColumn(_g.d.ic_trans._doc_no, 1, 15, 15);
            this._gridTrans._addColumn(_g.d.ic_trans._cashier_code, 1, 15, 15);
            this._gridTrans._addColumn(_g.d.ic_trans._sale_code, 1, 15, 15);
            this._gridTrans._addColumn(_g.d.ic_trans._total_discount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTrans._addColumn(_g.d.ic_trans._total_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTrans._calcPersentWidthToScatter();
            this._gridTrans._mouseClick += _gridTrans__mouseClick;

            this._gridDetail._table_name = _g.d.ic_trans_detail._table;
            this._gridDetail._isEdit = false;
            this._gridDetail._total_show = true;
            this._gridDetail._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 20, false, false, true, false);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, false, false, true, false, __formatNumberPrice);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, false, false, true);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridDetail._calcPersentWidthToScatter();

            this._screenSummary._setDataStr(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._ar_code, this._custCode);
            this._screenSummary._setDataStr(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._code, this._memberCode);

            // set default data
            DateTime __firstDate = DateTime.Now;
            __firstDate = __firstDate.AddYears(-1);

            this._screenSummary._setDataDate(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._from_doc_date, __firstDate);
            this._screenSummary._setDataDate(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._to_doc_date, DateTime.Now);

            try
            {


                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(_g.g._companyProfile._activeSyncServer, "SMLConfig" + _g.g._companyProfile._activeSyncProvider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                __myFrameWork._findDatabaseType();

                DataSet __result = __myFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select name_1, birth_day" +
                    ", (select regist_date from ar_dealer where upper(ar_dealer.code) = \'" + this._memberCode + "\' and ar_code = ar_customer.code ) as regist_date " +
                    ", (select expire_date from ar_dealer where upper(ar_dealer.code) = \'" + this._memberCode + "\' and ar_code = ar_customer.code ) as expire_date " +
                    "  from ar_customer where code = \'" + this._custCode + "\'");
                if (__result.Tables.Count > 0)
                {
                    DataTable __custTable = __result.Tables[0];
                    if (__custTable.Rows.Count > 0)
                    {
                        string __cust_name = __custTable.Rows[0][_g.d.ar_customer._name_1].ToString();
                        DateTime __birthDay = MyLib._myGlobal._convertDateFromQuery(__custTable.Rows[0][_g.d.ar_customer._birth_day].ToString());
                        DateTime __regist_date = MyLib._myGlobal._convertDateFromQuery(__custTable.Rows[0][_g.d.ar_dealer._regist_date].ToString());
                        DateTime __expire_date = MyLib._myGlobal._convertDateFromQuery(__custTable.Rows[0][_g.d.ar_dealer._expire_date].ToString());

                        this._screenSummary._setDataStr(_g.d.ar_customer._table + "." + _g.d.ar_customer._name_1, __cust_name);
                        this._screenSummary._setDataDate(_g.d.ar_customer._table + "." + _g.d.ar_customer._birth_day, __birthDay);
                        this._screenSummary._setDataDate(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._regist_date, __regist_date);
                        this._screenSummary._setDataDate(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._expire_date, __expire_date);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void _gridTrans__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // click to view detail grid
            if (e._row < this._gridTrans._rowData.Count)
            {
                string __getDocNo = this._gridTrans._cellGet(e._row, _g.d.ic_trans._doc_no).ToString();
                _loadDetail(__getDocNo);
            }
        }

        void _myManageTrans_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {

        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            _loadData();
        }

        void _loadData()
        {
            string __fromDate = this._screenSummary._getDataStrQuery(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._from_doc_date);
            string __toDate = this._screenSummary._getDataStrQuery(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._to_doc_date);
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(_g.g._companyProfile._activeSyncServer, "SMLConfig" + _g.g._companyProfile._activeSyncProvider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                __myFrameWork._findDatabaseType();

                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                // trans
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select doc_date, doc_time, branch_sync as branch_code, doc_no, (cashier_code  || '~' || coalesce((select name_1 from erp_user where erp_user.code = ic_trans.cashier_code), '') ) as cashier_code, (sale_code || '~' || coalesce((select name_1 from erp_user where erp_user.code = ic_trans.sale_code), '')) as sale_name, total_discount, total_amount from ic_trans where trans_flag = 44 and last_status = 0 and cust_code = \'" + this._custCode + "\' and doc_date between " + __fromDate + " and " + __toDate + " order by doc_date, doc_time, doc_no "));

                __query.Append("</node>");

                ArrayList __result = __myFrameWork._queryListGetData(_g.g._companyProfile._activeSyncDatabase, __query.ToString());
                if (__result.Count > 0)
                {
                    _transTable = ((DataSet)__result[0]).Tables[0];
                    this._gridTrans._loadFromDataTable(_transTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void _loadDetail(string docNo)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(_g.g._companyProfile._activeSyncServer, "SMLConfig" + _g.g._companyProfile._activeSyncProvider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                __myFrameWork._findDatabaseType();

                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                // trans_detail
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select item_code, (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code) as item_name, unit_code, qty, price, discount, sum_amount from ic_trans_detail where trans_flag = 44 and last_status = 0 and cust_code = \'" + this._custCode + "\' and doc_no = \'" + docNo + "\' order by line_number "));
                __query.Append("</node>");

                ArrayList __result = __myFrameWork._queryListGetData(_g.g._companyProfile._activeSyncDatabase, __query.ToString());
                if (__result.Count > 0)
                {
                    _transDetailTable = ((DataSet)__result[0]).Tables[0];
                    this._gridDetail._loadFromDataTable(_transDetailTable);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
