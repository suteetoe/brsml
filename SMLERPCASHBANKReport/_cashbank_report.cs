using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANKReport
{
    public partial class _cashbank_report : UserControl
    {
        SMLReport._generate _report;
        String _screenName = "";
        DataTable _dataTableProduct;
        DataTable _dataTableDoc;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLERPReportTool._reportEnum _mode;
        SMLERPReportTool._conditionScreen _condition;
        Boolean _displayDetail = false;
        decimal _balance_amount = 0M;
        _g.g._transControlTypeEnum _transFlag = _g.g._transControlTypeEnum.ว่าง;

        public _cashbank_report(SMLERPReportTool._reportEnum mode, _g.g._transControlTypeEnum transFlag, string screenName)
        {
            InitializeComponent();


            this._mode = mode;
            this._transFlag = transFlag; // "44"; // (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้) ? "44" : "12";
            this._screenName = screenName;
            this._report = new SMLReport._generate(screenName, false);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report.Disposed += new EventHandler(_report_Disposed);
            ////
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            //this._report__showCondition(screenName);
            this._showCondition();
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Data)
            {
                if (sender._levelName == this._levelNameRoot)
                {
                    switch (this._mode)
                    {
                        case SMLERPReportTool._reportEnum.เคลื่อนไหวเงินสด:
                            {

                                int __transFlagColumnNumber = sender._findColumnName(_g.d.resource_report._table + "." + _g.d.cb_trans._trans_flag);
                                int __balanceColumnNumber = sender._findColumnName(_g.d.resource_report._table + "." + _g.d.ic_resource._balance_amount);
                                int __docDateColumnNumber = sender._findColumnName(_g.d.resource_report._table + "." + _g.d.cb_trans._doc_date);
                                int __payAmountColumNumber = sender._findColumnName(_g.d.resource_report._table + "." + _g.d.resource_report._sum_return);
                                int __receiptAmountColumNumber = sender._findColumnName(_g.d.resource_report._table + "." + _g.d.resource_report._sum_receive);
                                

                                if (columnNumber == __transFlagColumnNumber)
                                {
                                    if (sender._columnList[columnNumber]._dataStr == "0")
                                    {
                                        sender._columnList[columnNumber]._dataStr = "ยอดยกมาจากการคำณวน";
                                        //this._balance_amount = sender._columnList[sender._findColumnName(_g.d.resource_report._table + "." + _g.d.resource_report._amount)]._dataNumber;  
                                    }
                                    else
                                    {
                                        sender._columnList[columnNumber]._dataStr = _g.g._transFlagGlobal._transName(MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr));
                                    }
                                }

                                if (columnNumber == __balanceColumnNumber)
                                {
                                    decimal __payAmount = sender._columnList[__payAmountColumNumber]._dataNumber;
                                    decimal __receiptAmount = sender._columnList[__receiptAmountColumNumber]._dataNumber;
                                    if (sender._columnList[__transFlagColumnNumber]._dataStr == "0" || sender._columnList[__transFlagColumnNumber]._dataStr  == "ยอดยกมาจากการคำณวน")
                                        __receiptAmount = sender._columnList[__balanceColumnNumber]._dataNumber;  

                                    this._balance_amount = (this._balance_amount + __receiptAmount) + (-1 * __payAmount);
                                    sender._columnList[columnNumber]._dataNumber = this._balance_amount;
                                }

                                if (columnNumber == __docDateColumnNumber)
                                {
                                    if (sender._columnList[__transFlagColumnNumber]._dataStr == "0")
                                    {
                                        sender._columnList[columnNumber]._dataStr = "";
                                    }
                                }


                            }
                            break;                       
                    }
                }
            }
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            try
            {
                if (level._levelName.Equals(this._levelNameRoot))
                {
                    this._balance_amount = 0M;
                    return this._dataTableProduct.Select();
                }
                else
                    if (level._levelName.Equals(this._levelNameDetail))
                    {
                        switch (this._mode)
                        {
                            case SMLERPReportTool._reportEnum.เคลื่อนไหวเงินสด:
                                {
                                    string __where = _g.d.resource_report._table + "." + _g.d.resource_report._item_code + "=\'" + source[_g.d.resource_report._table + "." + _g.d.resource_report._item_code].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                                }
                        }
                    }

            }
            catch (Exception ex)
            {
            }

            return null;
        }

        void _report__query()
        {
            StringBuilder __query = new StringBuilder();
            StringBuilder __query_sub = new StringBuilder();
            StringBuilder __where = new StringBuilder();
            StringBuilder __whereDetail = new StringBuilder();
            string __orderDetail = "";
            string __orderMaster = "";

            DateTime __begin_date = this._condition._screen._getDataDate(_g.d.resource_report._from_date);
            DateTime __end_date = this._condition._screen._getDataDate(_g.d.resource_report._to_date);

            //string __code_begin = this._condition._screen._getDataStrQuery(_g.d.resource_report._from_item_code).Replace("null", "");
            //string __code_end = this._condition._screen._getDataStrQuery(_g.d.resource_report._to_item_code).Replace("null", "");

            // condition 
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.เงินสดย่อย_สถานะ:
                    {

                    }
                    break;
            }

            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.เงินสดย่อย_สถานะ:
                    {
                        string __getWhere = this._condition._grid._createWhere("code");

                        __query.Append("select ");
                        __query.Append(" code as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._item_code + "\"");
                        __query.Append(", name_1 as \"" + _g.d.resource_report._table + "." + _g.d.ic_resource._ic_name + "\"");
                        __query.Append(", balance_first as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._balance_first + "\"");
                        __query.Append(", request_amount as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._sum_request + "\"");
                        __query.Append(", return_amount as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._sum_return + "\"");
                        __query.Append(", pay_amount as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._sum_pay + "\"");
                        __query.Append(", receipt_amount as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._sum_receive + "\"");
                        __query.Append(", balance_end as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._balance_end + "\"");
                        __query.Append(" from ( ");
                        SMLProcess._cbInfoProcess __cbProcess = new SMLProcess._cbInfoProcess();
                        __query.Append(__cbProcess._cbStatusQuery(this._condition._screen._getDataDate(_g.d.resource_report._from_date), this._condition._screen._getDataDate(_g.d.resource_report._to_date)));
                        __query.Append(" ) as temp99 ");

                        if (__getWhere.Length > 0)
                        {
                            __query.Append(" where " + __getWhere);
                        }
                    }
                    break;
                case SMLERPReportTool._reportEnum.เคลื่อนไหวเงินสด:
                    {
                        string __apFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด) + ","
                            
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน) + ","
                            
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน) + ","
                            
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) + ","
                            
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้);

                        string __arFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + ","

                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน) + ","

                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน) + ","

                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + ","

                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้);


                        __query.Append("select ");
                        __query.Append(" doc_date as \"" + _g.d.resource_report._table + "." + _g.d.cb_trans._doc_date + "\"");
                        __query.Append(", doc_no as \"" + _g.d.resource_report._table + "." + _g.d.cb_trans._doc_no + "\"");
                        __query.Append(", trans_type as \"" + _g.d.resource_report._table + "." + _g.d.cb_trans._trans_flag + "\"");
                        __query.Append(", ap_ar_code || (case when trans_type in (" + __apFlag + ") then (select '~' ||  name_1 from ap_supplier where ap_supplier.code = ap_ar_code ) else (case when trans_type in (" + __arFlag + ") then  (select '~' ||  name_1 from ar_customer where ar_customer.code = ap_ar_code )  else '' end) end) as \"" + _g.d.resource_report._table + "." + _g.d.cb_trans._ap_ar_code + "\"");
                        __query.Append(", income_amount as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._sum_receive + "\"");
                        __query.Append(", pay_amount as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._sum_return + "\"");
                        __query.Append(", amount as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._amount + "\"");
                        __query.Append(", balance_amount as \"" + _g.d.resource_report._table + "." + _g.d.ic_resource._balance_amount + "\"");
                        __query.Append(" from ( ");
                        SMLProcess._cbInfoProcess __cbProcess = new SMLProcess._cbInfoProcess();
                        __query.Append(__cbProcess._cashMovementQuery(this._condition._screen._getDataDate(_g.d.resource_report._from_date), this._condition._screen._getDataDate(_g.d.resource_report._to_date)));
                        __query.Append(" ) as temp99 ");

                        __orderMaster = " order by doc_type, doc_date, doc_no ";

                    }
                    break;
                case SMLERPReportTool._reportEnum.เงินสดย่อย_เคลื่อนไหว:
                    {
                        string __getWhere = this._condition._grid._createWhere(_g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._code);

                        // รายวัน
                        string __payFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ) + ","
    + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า) + ","
    + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + ","
    + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น) + ","
    + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้);

                        string __receiptFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น);

                        string dateWhere = " doc_date between " + MyLib._myGlobal._sqlDateFunction(MyLib._myGlobal._convertDateToQuery(__begin_date)) + " and " + MyLib._myGlobal._sqlDateFunction(MyLib._myGlobal._convertDateToQuery(__end_date));
                        // รายวัน
                        //1. เบิก
                        StringBuilder __query_trans_detail = new StringBuilder(); // and item_code = cb_petty_cash.code 
                        __query_trans_detail.Append(" select 1 as calc_type, doc_date, doc_no, item_code, sum_amount as sum_amount, trans_flag  from ic_trans_detail where trans_flag = 300 and last_status = 0 " + ((dateWhere.Length > 0) ? " and " + dateWhere : ""));

                        // 2. คืน
                        // 
                        __query_trans_detail.Append(" union all ");//  and item_code = cb_petty_cash.code
                        __query_trans_detail.Append(" select 2 as calc_type, doc_date, doc_no, item_code, sum_amount, trans_flag  from ic_trans_detail where trans_flag = 301 and last_status = 0 " + ((dateWhere.Length > 0) ? " and " + dateWhere : ""));

                        // ตัดชำระ
                        __query_trans_detail.Append(" union all ");// and trans_number = cb_petty_cash.code
                        __query_trans_detail.Append("select 3 as calc_type, doc_date, doc_no, trans_number  as item_code, amount as sum_amount, trans_flag from cb_trans_detail where doc_type = 4 and trans_flag in (" + __payFlag + ")  " + ((dateWhere.Length > 0) ? " and " + dateWhere : ""));

                        // รับชำระ
                        __query_trans_detail.Append(" union all ");// and trans_number = cb_petty_cash.code
                        __query_trans_detail.Append(" select 4 as calc_type, doc_date, doc_no, trans_number  as item_code, amount as sum_amount, trans_flag from cb_trans_detail where doc_type = 4 and trans_flag in (" + __receiptFlag + ")  " + ((dateWhere.Length > 0) ? " and " + dateWhere : ""));



                        SMLProcess._cbInfoProcess __cbProcess = new SMLProcess._cbInfoProcess();
                        __query.Append("select code as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._item_code + "\", name_1 as \"" + _g.d.resource_report._table + "." + _g.d.ic_resource._ic_name + "\" from " + _g.d.cb_petty_cash._table);

                        if (__getWhere.Length > 0)
                        {
                            __query.Append(" where " + __getWhere);
                        }

                        __query_sub.Append("select ");
                        __query_sub.Append(" item_code as \"" + _g.d.resource_report._table + "." + _g.d.resource_report._item_code + "\"");
                        __query_sub.Append(" , doc_no as \"" + _g.d.cb_resource._table + "." + _g.d.cb_resource._doc_no + "\"");
                        __query_sub.Append(" , doc_date as \"" + _g.d.cb_resource._table + "." + _g.d.cb_resource._doc_date + "\"");
                        __query_sub.Append(" , trans_flag as \"" + _g.d.cb_resource._table + "." + _g.d.cb_resource._doc_type + "\"");
                        __query_sub.Append(" , sum_pay as \"" + _g.d.cb_resource._table + "." + _g.d.resource_report._sum_pay + "\"");
                        __query_sub.Append(" , sum_receive as \"" + _g.d.cb_resource._table + "." + _g.d.resource_report._sum_receive + "\"");
                        __query_sub.Append(" , 0 as \"" + _g.d.cb_resource._table + "." + _g.d.cb_trans_detail._balance_amount + "\" ");

                        __query_sub.Append(" from (");

                        // ยกมา date <= begin_date
                        string __balance_doc_query = "select code as item_code, '' as doc_no, '1900-01-01' as doc_date, 0 as trans_flag " +
                            ", 0 as sum_pay" +
                            " ,(select sum(sum_amount) from (" + __cbProcess._createPettyCashQuery(" doc_date < " + MyLib._myGlobal._sqlDateFunction(MyLib._myGlobal._convertDateToQuery(__begin_date))) + " ) as temp9 ) as sum_receive " +

                            ", 0 as balance_amount" +
                            " from cb_petty_cash ";

                        if (__getWhere.Length > 0)
                        {
                            __balance_doc_query = __balance_doc_query + " where " + __getWhere;
                        }

                        __query_sub.Append(__balance_doc_query);

                        __query_sub.Append(" union all ");


                        string __query_trans_doc = "select item_code, doc_no, doc_date, trans_flag " +
                            ", case when calc_type = 1 or calc_type = 3 then sum_amount else 0 end as sum_pay " +
                            ", case when calc_type = 2 or calc_type = 4 then sum_amount else 0 end as sum_receive " +
                            ", 0 as balance_amount " +
                            " from (" + __query_trans_detail.ToString() + ") as temp99 ";

                        if (__getWhere.Length > 0)
                        {
                            __query_trans_doc = __query_trans_doc + " where " + __getWhere.Replace(_g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._code, "item_code");
                        }

                        __query_sub.Append(__query_trans_doc);
                        __query_sub.Append(" ) as temp9 order by doc_date ");
                    }
                    break;
            }

            __query.Append(((__where.Length > 0) ? " where " + __where : "") + __orderMaster);

            __query_sub.Append(((__whereDetail.Length > 0) ? " where " + __whereDetail : "") + __orderDetail);

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __queryHead = __query.ToString();
            string __queryDetail = __query_sub.ToString();

            try
            {
                this._dataTableProduct = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryHead).Tables[0];
                this._dataTableDoc = (_displayDetail) ? __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetail).Tables[0] : null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void _report__init()
        {

            switch (this._mode)
            {

                case SMLERPReportTool._reportEnum.เงินสดย่อย_เคลื่อนไหว:
                    this._displayDetail = true;
                    break;
            }

            //this._displayDetail = this._con_so._condition_so_search3._getDataStr(_g.d.resource_report._display_detail).ToString().Equals("1") ? true : false;

            this._report._level = this._reportInitRoot(null, true, true);
            if (this._displayDetail == true)
            {
                SMLReport._generateLevelClass __level2 = this._reportInitDetail(this._report._level, true, 2, true);
            }
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDetailColumn(__columnList, spaceFirstColumnWidth);
            return this._report._addLevel(this._levelNameDetail, levelParent, __columnList, sumTotal, autoWidth);
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = (this._displayDetail) ? FontStyle.Bold : FontStyle.Regular;

            switch (this._mode)
            {

                case SMLERPReportTool._reportEnum.เงินสดย่อย_สถานะ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._item_code, _g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._code, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.ic_resource._ic_name, _g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._name_1, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._balance_first, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._sum_request, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._sum_return, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._sum_pay, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._sum_receive, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._balance_end, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));

                    break;
                case SMLERPReportTool._reportEnum.เงินสดย่อย_เคลื่อนไหว:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._item_code, _g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._code, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.ic_resource._ic_name, _g.d.cb_petty_cash._table + "." + _g.d.cb_petty_cash._name_1, 80, SMLReport._report._cellType.String, 0, __fontStyle));

                    break;
                case SMLERPReportTool._reportEnum.เคลื่อนไหวเงินสด:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.cb_trans._doc_date, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_date, 12, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.cb_trans._doc_no, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_no, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.cb_trans._trans_flag, _g.d.cb_resource._table + "." + _g.d.cb_resource._doc_type, 18, SMLReport._report._cellType.String, 0));

                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.cb_trans._ap_ar_code, _g.d.cb_trans._table + "." + _g.d.cb_trans._ap_ar_name, 50, SMLReport._report._cellType.String, 0, __fontStyle));

                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._sum_receive, _g.d.resource_report._table + "." + _g.d.resource_report._sum_receive, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.resource_report._sum_return, _g.d.temp_cash_movement._table + "." + _g.d.temp_cash_movement._amount_out, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report._table + "." + _g.d.ic_resource._balance_amount, _g.d.cb_resource._table + "." + _g.d.cb_resource._amount_balance, 15, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle, false, false));

                    break;
            }

        }

        private void _reportInitDetailColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.เงินสดย่อย_เคลื่อนไหว:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._table + "." + _g.d.cb_resource._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._table + "." + _g.d.cb_resource._doc_no, null, 10, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._table + "." + _g.d.cb_resource._doc_type, null, 10, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._table + "." + _g.d.resource_report._sum_receive, _g.d.resource_report._table + "." + _g.d.resource_report._sum_receive, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._table + "." + _g.d.resource_report._sum_pay, _g.d.resource_report._table + "." + _g.d.resource_report._sum_pay, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._table + "." + _g.d.cb_trans_detail._balance_amount, _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._balance_amount, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular, true, false));
                    break;
            }

            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 8, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 15, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 6, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount_amount, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, null, 4, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, null, 4, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 6, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount_exclude_vat, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _showCondition()
        {
            if (this._condition == null)
            {
                //_condition = new SMLERPReportTool._conditionScreen(_icType, "");
                this._condition = new SMLERPReportTool._conditionScreen(this._mode, this._screenName);

                this._condition._extra._tableName = _g.d.ic_inventory._table;
                this._condition._extra._searchTextWord.Visible = false;
                this._condition._extra._orderByComboBox.Visible = false;
                this._condition._extra._orderByComboBox.Dispose();


                // new ArrayList ใหม่  เพราะ ป้องกันการ  add width column  เพิ่ม

                this._condition.Size = new Size(600, 600);
            }

            this._condition.ShowDialog();
            if (this._condition._processClick)
            {

                //this._check_submit = this._condition._processClick;
                // new ArrayList ใหม่  เพราะ ป้องกันการ  add width column  เพิ่ม
                //_width.Clear(); _width_2.Clear(); _width_3.Clear();
                //_column.Clear(); _column_2.Clear(); _column_3.Clear();
                //this._config();
                string __beginDate = this._condition._screen._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._condition._screen._getDataStr(_g.d.resource_report._to_date);

                if (this._mode == SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ)
                {
                    //this._report._conditionText = MyLib._myGlobal._resource("ตามเอกสาร") + " : " + _getDocNo_stock_check(false);
                }
                else
                {
                    switch (this._mode)
                    {
                        case SMLERPReportTool._reportEnum.เงินสดย่อย_สถานะ:
                        case SMLERPReportTool._reportEnum.เงินสดย่อย_เคลื่อนไหว:
                        case SMLERPReportTool._reportEnum.เคลื่อนไหวเงินสด :
                            this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                            break;
                        //case SMLERPReportTool._reportEnum.Diff_from_count :
                        //    this._report._conditionText = MyLib._myGlobal._resource("ตามเอกสาร") + " : " + _getDocNo_stock_check();
                        //    break;
                        case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                            {
                                string __ar_type = this._condition._screen._getDataStr(_g.d.resource_report._ar_type);
                                string __condigion = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                                //this._report._conditionText = MyLib._myGlobal._resource("ตามเอกสาร") + " : " + _getDocNo_stock_check(false);
                                if (__ar_type.Length > 0)
                                {
                                    __condigion += MyLib._myGlobal._resource("ประเภทลูกหนี้") + " : " + ((MyLib._myTextBox)this._condition._screen._getControl(_g.d.resource_report._ar_type))._textLast;
                                }
                                this._report._conditionText = __condigion;
                            }
                            break;
                    }
                    this._report._conditionText = _g.g._conditionGrid(this._condition._grid, this._report._conditionText);
                }
                this._report._build();
            }

        }


    }
}
