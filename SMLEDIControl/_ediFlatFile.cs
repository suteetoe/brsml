using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;

namespace SMLEDIControl
{
    public partial class _ediFlatFile : UserControl
    {
        

        public _ediFlatFile()
        {
            InitializeComponent();


            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._build();
            }
        }

        void _build()
        {
            string __formatNumberPrice = _g.g._getFormatNumberStr(3);

            this._conditionScreen._maxColumn = 2;
            this._conditionScreen._addDateBox(0, 0, 1, 1, _g.d.resource_report._table + "." + _g.d.resource_report._from_date, 1, true);
            this._conditionScreen._addDateBox(0, 1, 1, 1, _g.d.resource_report._table + "." + _g.d.resource_report._to_date, 1, true);
            this._clear();
        }


        void _clear()
        {
            this._resultTextboxbs.Clear();
            this._resultTextboxpl.Clear();
            DateTime __beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime __endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
        }


        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonClear_Click(object sender, EventArgs e)
        {
            this._clear();
        }


        private void _buttonExportPreview_Click(object sender, EventArgs e)
        {
            _process(true);
        }

        private void _buttonExport_Click(object sender, EventArgs e)
        {
            _process(false);

        }

        void _process(bool isPreview)
        {
          
            this._resultTextboxbs.Clear();
            this._resultTextboxpl.Clear();
            // new text filename
           
            string __queryBS = "select 'ACT_V0' as \"CATEGORY\", to_char(date("+ this._conditionScreen._getDataStrQuery(_g.d.resource_report._table + "." + _g.d.resource_report._to_date) + "), 'YYYY.MM') as \"TIME\", (select entity from erp_company_profile ) as \"ENTITY\", code as \"ACCOUNT\", cust_code as \"INTERCO\", 'PC_NONE' as \"PROFIT_CENTER\", 'CC_NONE' as \"COST_CENTER\", 'F_999' as \"FLOW\", 'LC' as \"CURRENCY\", case when balance_mode = 0 then (sum(debit) - sum(credit)) else  (sum(credit) - sum(debit)) end as \"AMOUNT\", (select name_1 from gl_chart_of_account where gl_chart_of_account.code = gl_template.code ) as name_1 from (select gl_chart_of_account.account_group,gl_chart_of_account.code,gl_chart_of_account.name_1,gl_chart_of_account.account_type, gl_chart_of_account.balance_mode,case when gl_journal_detail.cust_code='' then 'I_NONE' else gl_journal_detail.cust_code end as cust_code , gl_journal_detail.account_code, gl_journal_detail.debit, gl_journal_detail.credit  from (select doc_no, trans_flag,coalesce( case when trans_flag in (19,239) then (select case when trans_flag = 19 then (select case when (coalesce(interco, '') = '' ) then 'I_NONE' else interco end as interco from ap_supplier where ap_supplier.code = ap_ar_trans.cust_code) else (select case when (coalesce(interco, '') = '' ) then 'I_NONE' else interco end as interco from ar_customer where ar_customer.code = ap_ar_trans.cust_code) end     from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_detail.doc_no and ap_ar_trans.trans_flag = gl_journal_detail.trans_flag  )  else (select  case when trans_type = 2 and trans_flag not in (260) then (select case when (coalesce(interco, '') = '' ) then 'I_NONE' else interco end as interco from ar_customer where ar_customer.code = ic_trans.cust_code)  when trans_type = 1 or trans_flag in (260) then(select case when (coalesce(interco, '') = '' ) then 'I_NONE' else interco end as interco from ap_supplier where ap_supplier.code = ic_trans.cust_code)   else 'I_NONE' end from ic_trans where ic_trans.doc_no = gl_journal_detail.doc_no and ic_trans.trans_flag = gl_journal_detail.trans_flag )  end , '') as cust_code, account_code, debit, credit from gl_journal_detail where doc_date between "+ this._conditionScreen._getDataStrQuery(_g.d.resource_report._table + "." + _g.d.resource_report._from_date) + " and "+ this._conditionScreen._getDataStrQuery(_g.d.resource_report._table + "." + _g.d.resource_report._to_date) + "      ) as gl_journal_detail    left join gl_chart_of_account on gl_chart_of_account.code = gl_journal_detail.account_code   where gl_chart_of_account.account_type in (0,1,2) ) as gl_template group by  code, cust_code, balance_mode order by code, cust_code";
            string __queryPL = "select 'ACT_V0' as \"CATEGORY\", to_char(date(" + this._conditionScreen._getDataStrQuery(_g.d.resource_report._table + "." + _g.d.resource_report._to_date) + "), 'YYYY.MM') as \"TIME\", (select entity from erp_company_profile ) as \"ENTITY\", code as \"ACCOUNT\", cust_code as \"INTERCO\", 'PC_NONE' as \"PROFIT_CENTER\", 'CC_NONE' as \"COST_CENTER\", 'F_999' as \"FLOW\", 'LC' as \"CURRENCY\", case when balance_mode = 0 then (sum(debit) - sum(credit)) else  (sum(credit) - sum(debit)) end as \"AMOUNT\", (select name_1 from gl_chart_of_account where gl_chart_of_account.code = gl_template.code ) as name_1 from (select gl_chart_of_account.account_group,gl_chart_of_account.code,gl_chart_of_account.name_1,gl_chart_of_account.account_type, gl_chart_of_account.balance_mode,case when gl_journal_detail.cust_code='' then 'I_NONE' else gl_journal_detail.cust_code end as cust_code , gl_journal_detail.account_code, gl_journal_detail.debit, gl_journal_detail.credit  from (select doc_no, trans_flag,coalesce( case when trans_flag in (19,239) then (select case when trans_flag = 19 then (select case when (coalesce(interco, '') = '' ) then 'I_NONE' else interco end as interco from ap_supplier where ap_supplier.code = ap_ar_trans.cust_code) else (select case when (coalesce(interco, '') = '' ) then 'I_NONE' else interco end as interco from ar_customer where ar_customer.code = ap_ar_trans.cust_code) end     from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_detail.doc_no and ap_ar_trans.trans_flag = gl_journal_detail.trans_flag  )  else (select  case when trans_type = 2 and trans_flag not in (260) then (select case when (coalesce(interco, '') = '' ) then 'I_NONE' else interco end as interco from ar_customer where ar_customer.code = ic_trans.cust_code)  when trans_type = 1 or trans_flag in (260) then(select case when (coalesce(interco, '') = '' ) then 'I_NONE' else interco end as interco from ap_supplier where ap_supplier.code = ic_trans.cust_code)   else 'I_NONE' end from ic_trans where ic_trans.doc_no = gl_journal_detail.doc_no and ic_trans.trans_flag = gl_journal_detail.trans_flag )  end , '') as cust_code, account_code, debit, credit from gl_journal_detail where doc_date between " + this._conditionScreen._getDataStrQuery(_g.d.resource_report._table + "." + _g.d.resource_report._from_date) + " and " + this._conditionScreen._getDataStrQuery(_g.d.resource_report._table + "." + _g.d.resource_report._to_date) + "      ) as gl_journal_detail    left join gl_chart_of_account on gl_chart_of_account.code = gl_journal_detail.account_code where gl_chart_of_account.account_type in (3,4)   ) as gl_template group by  code, cust_code, balance_mode order by code, cust_code";

            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryBS));
            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryPL));
            __queryList.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());

            

            if (__result.Count > 0)
            {
                DataTable __BsData = ((DataSet)__result[0]).Tables[0];
                DataTable __PlData = ((DataSet)__result[1]).Tables[0];

                // tab bs -------------------------------------------------------------------------
                StringBuilder __exportWord = new StringBuilder();
                __exportWord.Append("CATEGORY\tTIME\tENTITY\tACCOUNT\tINTERCO\tPROFIT CENTER\tCOST CENTER\tFLOW\tCURRENCY\tAMOUNT" + "\r\n");
                for (int __rowTrans = 0; __rowTrans < __BsData.Rows.Count; __rowTrans++)
                {
                    string __CATEGORY = __BsData.Rows[__rowTrans]["CATEGORY"].ToString();
                    string __TIME = __BsData.Rows[__rowTrans]["TIME"].ToString();
                    string __ENTITY = __BsData.Rows[__rowTrans]["ENTITY"].ToString();
                    string __ACCOUNT = __BsData.Rows[__rowTrans]["ACCOUNT"].ToString();
                    string __INTERCO = __BsData.Rows[__rowTrans]["INTERCO"].ToString();
                    string __PROFIT_CENTER = __BsData.Rows[__rowTrans]["PROFIT_CENTER"].ToString();
                    string __COST_CENTER = __BsData.Rows[__rowTrans]["COST_CENTER"].ToString();
                    string __FLOW = __BsData.Rows[__rowTrans]["FLOW"].ToString();
                    string __CURRENCY = __BsData.Rows[__rowTrans]["CURRENCY"].ToString();
                    string __AMOUNT = __BsData.Rows[__rowTrans]["AMOUNT"].ToString();
                    __exportWord.Append(__CATEGORY + "\t" + __TIME + "\t" + __ENTITY + "\t" + __ACCOUNT + "\t" + __INTERCO + "\t" + __PROFIT_CENTER + "\t" + __COST_CENTER + "\t" + __FLOW + "\t" + __CURRENCY + "\t" + __AMOUNT + "\r\n");
                }
                this._resultTextboxbs.AppendText(__exportWord.ToString() + "\r\n");


                // tab bs -------------------------------------------------------------------------
                StringBuilder __exportWord2 = new StringBuilder();
                __exportWord2.Append("CATEGORY\tTIME\tENTITY\tACCOUNT\tINTERCO\tPROFIT CENTER\tCOST CENTER\tFLOW\tCURRENCY\tAMOUNT" + "\r\n");
                for (int __rowTrans = 0; __rowTrans < __PlData.Rows.Count; __rowTrans++)
                {
                    string __CATEGORY = __PlData.Rows[__rowTrans]["CATEGORY"].ToString();
                    string __TIME = __PlData.Rows[__rowTrans]["TIME"].ToString();
                    string __ENTITY = __PlData.Rows[__rowTrans]["ENTITY"].ToString();
                    string __ACCOUNT = __PlData.Rows[__rowTrans]["ACCOUNT"].ToString();
                    string __INTERCO = __PlData.Rows[__rowTrans]["INTERCO"].ToString();
                    string __PROFIT_CENTER = __PlData.Rows[__rowTrans]["PROFIT_CENTER"].ToString();
                    string __COST_CENTER = __PlData.Rows[__rowTrans]["COST_CENTER"].ToString();
                    string __FLOW = __PlData.Rows[__rowTrans]["FLOW"].ToString();
                    string __CURRENCY = __PlData.Rows[__rowTrans]["CURRENCY"].ToString();
                    string __AMOUNT = __PlData.Rows[__rowTrans]["AMOUNT"].ToString();
                    __exportWord2.Append(__CATEGORY + "\t" + __TIME + "\t" + __ENTITY + "\t" + __ACCOUNT + "\t" + __INTERCO + "\t" + __PROFIT_CENTER + "\t" + __COST_CENTER + "\t" + __FLOW + "\t" + __CURRENCY + "\t" + __AMOUNT + "\r\n");
                }
                this._resultTextboxpl.AppendText(__exportWord2.ToString() + "\r\n");
            }

            string __saveFilename = "";
                // LOTUS TIMS
                if (isPreview == false)
                {
                    FolderBrowserDialog __open = new FolderBrowserDialog();
                    if (__open.ShowDialog() == DialogResult.OK)
                    {
                        __saveFilename = __open.SelectedPath;
                    }
                    else
                    {
                        return;
                    }
              
                string __TemplateBs = __saveFilename + "\\TemplateBs.txt";

                using (StreamWriter sw = File.CreateText(__TemplateBs))
                {
                    sw.WriteLine(this._resultTextboxbs.Text);
                }

                string __TemplatePl = __saveFilename + "\\TemplatePl.txt";

                using (StreamWriter sw = File.CreateText(__TemplatePl))
                {
                    sw.WriteLine(this._resultTextboxpl.Text);
                }

                MessageBox.Show("Success");

            }

        }
    }
}
