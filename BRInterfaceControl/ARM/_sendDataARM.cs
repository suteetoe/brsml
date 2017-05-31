using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BRInterfaceControl.ARM
{
    public partial class _sendDataARM : UserControl
    {
        string _arm_code_company = "";
        public _sendDataARM()
        {
            InitializeComponent();

            this._myScreen1._table_name = _g.d.resource_report._table;
            this._myScreen1._maxColumn = 2;
            this._myScreen1._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
            this._myScreen1._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

            DateTime __today = DateTime.Now;

            this._myScreen1._setDataDate(_g.d.resource_report._from_date, __today);
            this._myScreen1._setDataDate(_g.d.resource_report._to_date, __today);

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            string __queryGetARMCompany = "select  " + _g.d.erp_company_profile._arm_code + " from " + _g.d.erp_company_profile._table;
            DataSet __result = __myFrameWork._queryShort(__queryGetARMCompany);
            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                _arm_code_company = __result.Tables[0].Rows[0][_g.d.erp_company_profile._arm_code].ToString();
            }

            //this._previewGrid._table_name = _g.d.ic_trans_detail._table;

            this._previewGrid._isEdit = false;
            this._previewGrid._addColumn("branch_sync", 1, 10, 10);
            this._previewGrid._addColumn("branch_name", 1, 10, 10);
            this._previewGrid._addColumn("arm_agent_code", 1, 10, 10);
            this._previewGrid._addColumn("ar_code", 1, 10, 10);
            this._previewGrid._addColumn("arm_sub_agent_code", 1, 10, 10);
            this._previewGrid._addColumn("sale_code", 1, 10, 10);
            this._previewGrid._addColumn("prod_code", 1, 10, 10);
            this._previewGrid._addColumn("buy_date", 1, 10, 10);
            this._previewGrid._addColumn("invoice_no", 1, 10, 10);
            this._previewGrid._addColumn("qty", 1, 10, 10);
            this._previewGrid._addColumn("uom", 1, 10, 10);
            this._previewGrid._calcPersentWidthToScatter();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        int _processDay = 0;
        int __currentProcess = 0;
        int __maxProcess = 0;
        Boolean _isPreview = false;

        void _process()
        {
            /*
            __maxProcess = _processDay * 6;

            for (int __process = 0; __process < _processDay; __process++)
            {
                for (int __step = 0; __step <= 6; __step++)
                {
                    __currentProcess++;
                    Thread.Sleep(1500);
                }
            }*/

            string __getFromDate = this._myScreen1._getDataStrQuery(_g.d.resource_report._from_date);
            string __getToDate = this._myScreen1._getDataStrQuery(_g.d.resource_report._to_date);


            StringBuilder __queryGetARM = new StringBuilder();
            __queryGetARM.Append("select ");
            __queryGetARM.Append(" ");
            __queryGetARM.Append(" '' as branch_sync, ");
            __queryGetARM.Append(" '' as branch_name, ");
            __queryGetARM.Append(" (select " + _g.d.erp_branch_list._arm_code + " from erp_branch_list where code = ( select branch_code from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) ) as arm_agent_code, ");
            __queryGetARM.Append(" cust_code as ar_code, ");
            __queryGetARM.Append(" (select " + _g.d.ar_customer._arm_code + " from ar_customer where ar_customer.code = ic_trans_detail.cust_code ) as arm_sub_agent_code, ");
            __queryGetARM.Append(" sale_code, ");
            __queryGetARM.Append(" item_code as prod_code, ");
            __queryGetARM.Append(" doc_date as buy_date, ");
            __queryGetARM.Append(" doc_no as invoice_no, ");
            __queryGetARM.Append(" qty, ");
            __queryGetARM.Append(" date(now()) as import_date, ");
            __queryGetARM.Append(" '' as import_by, ");
            __queryGetARM.Append(" 'ARM' as doc_type, ");
            __queryGetARM.Append(" unit_code as uom ");

            __queryGetARM.Append(" from ic_trans_detail ");
            __queryGetARM.Append(" where ");
            __queryGetARM.Append(" trans_flag = 44 and last_status = 0 ");
            __queryGetARM.Append(" and doc_date between " + __getFromDate + " and " + __getToDate + " ");
            __queryGetARM.Append(" and(select ref_doc_type from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag) = 'ARM' ");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __getArmData = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryGetARM.ToString());

            if (__getArmData.Tables.Count > 0 && __getArmData.Tables[0].Rows.Count > 0)
            {
                __maxProcess = __getArmData.Tables[0].Rows.Count;

                DataTable __armData = __getArmData.Tables[0];

                if (_isPreview)
                {
                    //this._previewGrid._loadFromDataTable(__armData);
                    this._previewGrid._clear();

                    for (int __row = 0; __row < __armData.Rows.Count; __row++)
                    {
                        int __addr = this._previewGrid._addRow();

                        //int __trans_id = MyLib._myGlobal._intPhase(__armData.Rows[__row]["trans_id"].ToString());
                        string __branch_sync = _g.g._companyProfile._activeSyncBranchCode; //__armData.Rows[__row]["branch_sync"].ToString();
                        string __branch_name = MyLib._myGlobal._ltdName; // __armData.Rows[__row]["branch_name"].ToString();
                        string __arm_agent_code = (__armData.Rows[__row]["arm_agent_code"].ToString().Length > 0) ? __armData.Rows[__row]["arm_agent_code"].ToString() : _arm_code_company;
                        string __ar_code = __armData.Rows[__row]["ar_code"].ToString();
                        string __arm_sub_agent_code = __armData.Rows[__row]["arm_sub_agent_code"].ToString();
                        string __sale_code = __armData.Rows[__row]["sale_code"].ToString();
                        string __prod_code = __armData.Rows[__row]["prod_code"].ToString();
                        string __buy_date = __armData.Rows[__row]["buy_date"].ToString();
                        string __invoice_no = __armData.Rows[__row]["invoice_no"].ToString();
                        decimal __qty = MyLib._myGlobal._decimalPhase(__armData.Rows[__row]["qty"].ToString());
                        string __import_date = __armData.Rows[__row]["import_date"].ToString();
                        string __import_by = MyLib._myGlobal._userCode;// __armData.Rows[__row]["import_by"].ToString();
                        string __doc_type = __armData.Rows[__row]["doc_type"].ToString();
                        string __uom = __armData.Rows[__row]["uom"].ToString();

                        this._previewGrid._cellUpdate(__addr, "branch_sync", __branch_sync, true);
                        this._previewGrid._cellUpdate(__addr, "branch_name", __branch_name, true);
                        this._previewGrid._cellUpdate(__addr, "arm_agent_code", __arm_agent_code, true);
                        this._previewGrid._cellUpdate(__addr, "ar_code", __ar_code, true);
                        this._previewGrid._cellUpdate(__addr, "arm_sub_agent_code", __arm_sub_agent_code, true);
                        this._previewGrid._cellUpdate(__addr, "sale_code", __sale_code, true);
                        this._previewGrid._cellUpdate(__addr, "prod_code", __prod_code, true);
                        this._previewGrid._cellUpdate(__addr, "buy_date", __buy_date, true);
                        this._previewGrid._cellUpdate(__addr, "invoice_no", __invoice_no, true);
                        this._previewGrid._cellUpdate(__addr, "qty", __qty, true);
                        this._previewGrid._cellUpdate(__addr, "uom", __uom, true);
                    }
                    this._previewGrid.Invalidate();
                }
                else
                {
                    string __fieldList = "branch_sync,branch_name,arm_agent_code,ar_code,arm_sub_agent_code,sale_code,prod_code,buy_date,invoice_no,qty,import_date,import_by,uom";
                    int __queryRowCount = 0;
                    StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                    // delete old
                    __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from tran_sale where branch_sync = \'" + _g.g._companyProfile._activeSyncBranchCode + "\' and buy_date between " + __getFromDate + " and " + __getToDate + " "));

                    MyLib._myFrameWork __armServer = new MyLib._myFrameWork("ws.brteasy.com:8080", "SMLConfigARM.xml", MyLib._myGlobal._databaseType.PostgreSql);

                    for (int __row = 0; __row < __armData.Rows.Count; __row++)
                    {
                        __queryRowCount++;
                        __currentProcess = __row + 1;

                        //int __trans_id = MyLib._myGlobal._intPhase(__armData.Rows[__row]["trans_id"].ToString());
                        string __branch_sync = _g.g._companyProfile._activeSyncBranchCode; //__armData.Rows[__row]["branch_sync"].ToString();
                        string __branch_name = MyLib._myGlobal._ltdName; // __armData.Rows[__row]["branch_name"].ToString();
                        string __arm_agent_code = (__armData.Rows[__row]["arm_agent_code"].ToString().Length > 0) ? __armData.Rows[__row]["arm_agent_code"].ToString() : _arm_code_company;
                        string __ar_code = __armData.Rows[__row]["ar_code"].ToString();
                        string __arm_sub_agent_code = __armData.Rows[__row]["arm_sub_agent_code"].ToString();
                        string __sale_code = __armData.Rows[__row]["sale_code"].ToString();
                        string __prod_code = __armData.Rows[__row]["prod_code"].ToString();
                        string __buy_date = __armData.Rows[__row]["buy_date"].ToString();
                        string __invoice_no = __armData.Rows[__row]["invoice_no"].ToString();
                        decimal __qty = MyLib._myGlobal._decimalPhase(__armData.Rows[__row]["qty"].ToString());
                        string __import_date = __armData.Rows[__row]["import_date"].ToString();
                        string __import_by = MyLib._myGlobal._userCode;// __armData.Rows[__row]["import_by"].ToString();
                        string __doc_type = __armData.Rows[__row]["doc_type"].ToString();
                        string __uom = __armData.Rows[__row]["uom"].ToString();

                        string __valueList = MyLib._myGlobal._fieldAndComma(
                            "\'" + __branch_sync + "\'",
                            "\'" + __branch_name + "\'",
                            "\'" + __arm_agent_code + "\'",
                            "\'" + __ar_code + "\'",
                            "\'" + __arm_sub_agent_code + "\'",
                            "\'" + __sale_code + "\'",
                            "\'" + __prod_code + "\'",
                            "\'" + __buy_date + "\'",
                            "\'" + __invoice_no + "\'",
                            "" + __qty.ToString() + "",
                            "\'" + __import_date + "\'",
                            "\'" + __import_by + "\'",
                            //"\'" + __doc_type + "\'",
                            "\'" + __uom + "\'");

                        string __queryInsert = "insert into tran_sale(" + __fieldList + ") values (" + __valueList + ")";

                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryInsert));
                        if (__queryRowCount >= 5000)
                        {
                            // insert
                            __queryList.Append("</node>");
                            string __result3 = __armServer._queryList("singhaarm", __queryList.ToString());
                            if (__result3.Length > 0)
                            {
                                MessageBox.Show(__result3);

                            }
                            __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                        }
                    }

                    __queryList.Append("</node>");

                    //DataSet result = __myFrameWork._query("singhaarm", "");

                    string __result2 = __armServer._queryList("singha_arm_sml", __queryList.ToString());
                    if (__result2.Length > 0)
                    {
                        MessageBox.Show(__result2);
                    }
                }
            }

            MessageBox.Show("Success");
        }

        private void _syncButton_Click(object sender, EventArgs e)
        {

            //DateTime __fromDateTimeCompare = new DateTime(__getFromDate.Year, __getFromDate.Month, __getFromDate.Day);
            //DateTime __toDateTimeCompare = new DateTime(__getToDate.Year, __getToDate.Month, __getToDate.Day);

            //_processDay = ((int)(__toDateTimeCompare - __fromDateTimeCompare).TotalDays) + 1;

            //__currentProcess = 0;
            //__maxProcess = _processDay * 6;
            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = __maxProcess;
            _isPreview = false;
            timer1.Start();
            Thread __thred = new Thread(new ThreadStart(_process));
            __thred.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Value = __currentProcess;

            if (__maxProcess == __currentProcess)
            {
                timer1.Stop();
            }
        }

        private void _previewButton_Click(object sender, EventArgs e)
        {
            _isPreview = true;
            _process();
        }
    }
}
