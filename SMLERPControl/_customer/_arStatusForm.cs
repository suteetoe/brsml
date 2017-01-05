using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._customer
{
    public partial class _arStatusForm : Form
    {
        string _arCode = "";
        public _arStatusForm()
        {
            InitializeComponent();

            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string _formatNumberAmount = _g.g._getFormatNumberStr(3);

            this._arStatusGrid._table_name = _g.d.ap_ar_resource._table;
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._doc_no, 1, 10, 8);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._doc_date, 1, 10, 10);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._doc_type, 1, 10, 8);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._due_date, 1, 10, 10);
            //
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._amount, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._ar_balance, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            /*this._arStatusGrid._addColumn(_g.d.ap_ar_resource.branm, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._term_0, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._term_1, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._term_2, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._term_3, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._term_4, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._term_5, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._due_day, 3, 10, 8, false, false, false, false, _g.g._getFormatNumberStr(0, 0));*/
            this._arStatusGrid._addColumn(_g.d.ap_ar_resource._remark, 1, 15, 15);
            this._arStatusGrid._total_show = true;
            this._arStatusGrid._calcPersentWidthToScatter();

            this._arChqGrid._table_name = _g.d.cb_chq_list._table;
            this._arChqGrid._addColumn(_g.d.cb_chq_list._chq_number, 1, 10, 15);
            this._arChqGrid._addColumn(_g.d.cb_chq_list._chq_get_date, 4, 10, 15);
            this._arChqGrid._addColumn(_g.d.cb_chq_list._doc_ref, 1, 10, 15);
            this._arChqGrid._addColumn(_g.d.cb_chq_list._chq_due_date, 4, 10, 10);

            this._arChqGrid._addColumn(_g.d.cb_chq_list._amount, 3, 10, 20, false, false, false, false, _formatNumberAmount);
            this._arChqGrid._addColumn(_g.d.cb_chq_list._status, 1, 10, 15);
            this._arChqGrid._total_show = true;
            this._arChqGrid._calcPersentWidthToScatter();

            this._srssGrid._table_name = _g.d.ic_trans._table;
            this._srssGrid._addColumn(_g.d.ic_trans._doc_date, 4, 10, 15);
            this._srssGrid._addColumn(_g.d.ic_trans._doc_no, 1, 10, 15);
            this._srssGrid._addColumn(_g.d.ic_trans._trans_flag, 1, 10, 15);
            this._srssGrid._addColumn(_g.d.ic_trans._total_amount, 3, 10, 20, false, false, false, false, _formatNumberAmount);
            this._srssGrid._total_show = true;
            this._srssGrid._calcPersentWidthToScatter();
        }

        public void _load(string arCode)
        {
            this._arCode = arCode;

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            SMLERPARAPInfo._process __arapProcess = new SMLERPARAPInfo._process();

            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            // info
            //__queryList.Append("select " + MyLib._myGlobal._fieldAndComma(_g.d.ar_customer_detail._credit_status, _g.d.ar_customer_detail.credit) + " from " + _g.d.ar_customer_detail._table + " where "  +_g.d.ar_customer_detail._ar_code + "=\'" + this._arCode + "\'");

            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__arapProcess._arCreditMoneyBalanceQuery(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้, this._arCode, "")));

            // status
            StringBuilder __queryDoc = new StringBuilder(__arapProcess._createQuery(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_ยอดคงเหลือตามเอกสาร, 0, " cust_code=\'" + this._arCode + "\'", "", DateTime.Now.AddYears(10).ToString("yyyy-MM-dd", new CultureInfo("en-US"))));

            StringBuilder __queryArStatus = new StringBuilder("select cust_code as " + _g.d.ap_ar_resource._ar_code + "," +
                    "doc_no as " + _g.d.ap_ar_resource._doc_no + "," +
                    "doc_date as " + _g.d.ap_ar_resource._doc_date + "," +
                    "due_date as " + _g.d.ap_ar_resource._due_date + "," +
                    "amount as " + _g.d.ap_ar_resource._amount + "," +
                    "trans_flag(doc_type) as " + _g.d.ap_ar_resource._doc_type + "," +
                    "used_status as " + _g.d.ap_ar_resource._status + "," +
                    //"case when due_date > 0  then due_date else 0 end as " + _g.d.ap_ar_resource._due_day + "," +
                    "balance_amount as " + _g.d.ap_ar_resource._ar_balance + "," +
                    _g.d.ap_ar_resource._ref_doc_no + " as " + _g.d.ap_ar_resource._ref_doc_no + "," +
                    _g.d.ap_ar_resource._ref_doc_date + " as " + _g.d.ap_ar_resource._ref_doc_date + "," +
                    "branch_code as " + _g.d.ap_ar_resource._branch_code + "," +
                    "tax_doc_no as " + _g.d.ap_ar_resource._tax_doc_no + "," +
                    "tax_doc_date as " + _g.d.ap_ar_resource._tax_doc_date + "," +
                    "remark as " + _g.d.ap_ar_resource._remark +

                " from (" + __queryDoc + ") as xdoc where balance_amount <> 0 ");

            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryArStatus.ToString()));

            // chq

            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select "  + MyLib._myGlobal._fieldAndComma(
                _g.d.cb_chq_list._chq_number,
                _g.d.cb_chq_list._chq_get_date,
                _g.d.cb_chq_list._doc_ref,
                _g.d.cb_chq_list._chq_due_date,
                _g.d.cb_chq_list._amount, 
                "(case when (status =0) then \'เช็คในมือ\' when(status = 1) then \'เช็คนำฝาก\' when(status = 2) then \'เช็คผ่าน\' when(status = 3) then \'เช็ครับคืน\' when (status = 4) then \'เช็คยกเลิก\' when (status = 5) then \'เช็คขายลด\'  when (status = 6) then \'เช็คคืนนำเข้าใหม่\'  when(status = 7) then \'เช็คเปลี่ยน\' else \'\' end) as status " ) +
                " from cb_chq_list where chq_type = 1 and status != 2 and ap_ar_code = \'" + this._arCode + "\' "));

            // sr ss
            StringBuilder __srssquery = new StringBuilder("select * from ( ");
            __srssquery.Append("select doc_no, doc_date, trans_flag(trans_flag) as trans_flag, (total_amount-(select total_amount from ic_trans as deposit  where deposit.doc_no=ic_trans.doc_ref_trans and deposit.trans_flag=40 and deposit.last_status=0 )) as total_amount from ic_trans where trans_flag = 34 and last_status = 0 and doc_success = 0 and approve_status in (0,1) and ic_trans.cust_code = \'" + this._arCode + "\' ");
            __srssquery.Append(" union all ");
            __srssquery.Append("select doc_no, doc_date, trans_flag(trans_flag) as trans_flag, total_amount from ic_trans where trans_flag = 36 and last_status = 0 and doc_success = 0 and approve_status in (0,1) and ic_trans.cust_code = \'" + this._arCode + "\' ");
            __srssquery.Append(" ) as temp1 order by doc_date, doc_no ");

            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__srssquery.ToString()));


            __queryList.Append("</node>");

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());
            if (__result.Count > 0)
            {
                StringBuilder __html = new StringBuilder(@"
    <html><head><style>body {
	font-family: tahoma, verdana, geneva, arial, helvetica, sans-serif;
	font-size: 9pt;
    color='#585858';
	margin: 2pt;
	margin-bottom: 0pt;
	margin-left: 2pt;
	margin-right: 0pt;
	margin-top: 2pt;	
    }</style></head><body bgcolor='#E0F8F7'>");

                DataTable __arInfo = ((DataSet)__result[0]).Tables[0];

                if (__arInfo.Rows.Count > 0)
                {

                    decimal __creditMoney = MyLib._myGlobal._decimalPhase(__arInfo.Rows[0][_g.d.ap_ar_resource._credit_money].ToString());
                    decimal __sr_remain = MyLib._myGlobal._decimalPhase(__arInfo.Rows[0]["sr_remain"].ToString());
                    decimal __ss_remain = MyLib._myGlobal._decimalPhase(__arInfo.Rows[0]["ss_remain"].ToString());
                    decimal __chq = MyLib._myGlobal._decimalPhase(__arInfo.Rows[0]["chq_outstanding"].ToString());
                    decimal __balanceEnd = MyLib._myGlobal._decimalPhase(__arInfo.Rows[0]["balance_end"].ToString());

                    __html.Append("<table width = '100%'>");

                    {
                        __html.Append("<tr>");
                        __html.Append("<td>");
                        __html.Append(MyLib._myGlobal._resource("วงเงินเครดิต"));
                        __html.Append("</td>");
                        __html.Append("<td align='right'>");
                        __html.Append("<font color='green'><b>");
                        __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __creditMoney));
                        __html.Append("&nbsp;</b></font></td>");
                        __html.Append("<td>");
                        __html.Append("</tr>");
                    }

                    __html.Append("<tr>");
                    __html.Append("<td>");
                    __html.Append(MyLib._myGlobal._resource("สั่งจอง/สั่งซื้อสินค้า"));
                    __html.Append("</td>");
                    __html.Append("<td align='right'>");
                    __html.Append("<font color='green'><b>");
                    __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __sr_remain));
                    __html.Append("&nbsp;</b></font></td>");
                    __html.Append("<td>");
                    __html.Append("</tr>");

                    __html.Append("<tr>");
                    __html.Append("<td>");
                    __html.Append(MyLib._myGlobal._resource("สั่งขาย"));
                    __html.Append("</td>");
                    __html.Append("<td align='right'>");
                    __html.Append("<font color='green'><b>");
                    __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __ss_remain));
                    __html.Append("&nbsp;</b></font></td>");
                    __html.Append("<td>");
                    __html.Append("</tr>");

                    __html.Append("<tr>");
                    __html.Append("<td>");
                    __html.Append(MyLib._myGlobal._resource("เช็คคงค้าง"));
                    __html.Append("</td>");
                    __html.Append("<td align='right'>");
                    __html.Append("<font color='green'><b>");
                    __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __chq));
                    __html.Append("&nbsp;</b></font></td>");
                    __html.Append("<td>");
                    __html.Append("</tr>");

                    __html.Append("<tr>");
                    __html.Append("<td>");
                    __html.Append(MyLib._myGlobal._resource("หนี้คงค้าง"));
                    __html.Append("</td>");
                    __html.Append("<td align='right'>");
                    __html.Append("<font color='green'><b>");
                    __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __balanceEnd));
                    __html.Append("&nbsp;</b></font></td>");
                    __html.Append("<td>");
                    __html.Append("</tr>");

                    __html.Append("</table>");

                }
                __html.Append("</body></html>");

                _webBrowser.Navigate("about:blank");
                try
                {
                    if (_webBrowser.Document != null)
                    {
                        _webBrowser.Document.Write(string.Empty);
                    }
                }
                catch (Exception e)
                { } // do nothing with this
                this._webBrowser.DocumentText = __html.ToString();

                this._arStatusGrid._loadFromDataTable(((DataSet)__result[1]).Tables[0]);
                this._arChqGrid._loadFromDataTable(((DataSet)__result[2]).Tables[0]);
                this._srssGrid._loadFromDataTable(((DataSet)__result[3]).Tables[0]);

            }

        }

        public void _clear()
        {
            this._webBrowser.DocumentText = "";

        }

    }
}
