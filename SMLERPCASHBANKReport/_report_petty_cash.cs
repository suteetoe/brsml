using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANKReport
{
    public partial class _report_petty_cash : UserControl
    {
        SMLReport._generate _report;
        String _screenName = "";
        DataTable _dataTableProduct;
        DataTable _dataTableDoc;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLERPReportTool._reportEnum _mode;
        string _transFlag;
        SMLERPReportTool._conditionScreen _condition;
        Boolean _displayDetail = false;

        public _report_petty_cash(SMLERPReportTool._reportEnum mode, string screenName)
        {
            InitializeComponent();


            this._mode = mode;
            //this._transFlag = "44"; // (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้) ? "44" : "12";
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
                switch (this._mode)
                {
                    case SMLERPReportTool._reportEnum.Serial_number:
                        {
                            int __transFlagColumnNumber = sender._findColumnName(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag);
                            if (__transFlagColumnNumber != -1 && __transFlagColumnNumber == columnNumber)
                            {
                                //int __value = MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                sender._columnList[columnNumber]._dataStr = _g.g._transFlagGlobal._transName(MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr));
                            }

                        }
                        break;
                }
            }
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            try
            {
                if (level._levelName.Equals(this._levelNameRoot))
                {
                    return this._dataTableProduct.Select();
                }
                else
                    if (level._levelName.Equals(this._levelNameDetail))
                    {
                        switch (this._mode)
                        {
                            case SMLERPReportTool._reportEnum.Serial_number:
                                {
                                    string __where = _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + "=\'" + source[_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                                }
                            case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                                {
                                    string __where = _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + "=\'" + source[_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                                }
                            case SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                                {
                                    string __where = _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._ic_code + "=\'" + source[_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                                }
                            case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                                {
                                    string __where = _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + "=\'" + source[_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code].ToString() + "\'";
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

            string __begin_date = this._condition._screen._getDataStrQuery(_g.d.resource_report._from_date);
            string __end_date = this._condition._screen._getDataStrQuery(_g.d.resource_report._to_date);
            string __code_begin = this._condition._screen._getDataStrQuery(_g.d.resource_report._from_item_code).Replace("null", "");
            string __code_end = this._condition._screen._getDataStrQuery(_g.d.resource_report._to_item_code).Replace("null", "");

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
                        /*
                         * 
                        string __serialFlagIn = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา).ToString() + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString();

                        string __serialExceptFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString() + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้่า_ตรวจสอบสินค้า_serial).ToString();

                        __query.Append(" select " + _g.d.ic_serial._serial_number + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + "\" ");
                        __query.Append(" , " + _g.d.ic_serial._ic_code + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._item_name + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_resource._item_name + "\" ");
                        __query.Append(" , " + _g.d.ic_serial._description + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._description + "\" ");
                        __query.Append(" , " + _g.d.ic_serial._status + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._status + "\" ");
                        __query.Append(" , " + _g.d.resource_report._date_import + " as \"" + _g.d.ic_serial._table + "." + _g.d.resource_report._date_import + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._ic_cost + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_resource._ic_cost + "\" ");
                        __query.Append(" from (");
                        __query.Append("select " + _g.d.ic_serial._serial_number + " , " + _g.d.ic_serial._ic_code);
                        __query.Append(" , (select " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + ") as " + _g.d.ic_resource._item_name);
                        __query.Append(" , " + _g.d.ic_serial._description);
                        __query.Append(" , case  when " + _g.d.ic_serial._status + "= 0 then '" + MyLib._myGlobal._resource("ในสต๊อค") + "' else '" + MyLib._myGlobal._resource("ในสต๊อค") + "' end as " + _g.d.ic_serial._status);
                        __query.Append(" , (select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + " = " + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + " and " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag + " in (" + __serialFlagIn + ") order by " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ) as " + _g.d.resource_report._date_import);
                        __query.Append(", (select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._price + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + " = " + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + " and " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag + " in (" + __serialFlagIn + ") order by " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ) as " + _g.d.ic_resource._ic_cost + "");
                        __query.Append(" from " + _g.d.ic_serial._table);
                        __query.Append(" ) as temp1 ");

                        __orderMaster = " order by " + _g.d.ic_serial._ic_code + "," + _g.d.ic_serial._serial_number;

                        __query_sub.Append("select " + _g.d.ic_trans_serial_number._doc_date + " as  \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + "\"");
                        __query_sub.Append(" , " + _g.d.ic_trans_serial_number._serial_number + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + "\"");
                        __query_sub.Append(" , " + _g.d.ic_trans_serial_number._doc_no + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no + "\"");
                        __query_sub.Append(" , " + _g.d.ic_trans_serial_number._trans_flag + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag + "\"");
                        __query_sub.Append(" , " + _g.d.ic_trans_serial_number._wh_code + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._wh_code + "\"");
                        __query_sub.Append(" ," + _g.d.ic_trans_serial_number._shelf_code + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._shelf_code + "\"");
                        __query_sub.Append(" ," + _g.d.resource_report._date_import + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.resource_report._date_import + "\"");
                        __query_sub.Append(" ," + _g.d.ic_resource._ar_detail + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_resource._ar_detail + "\"");
                        __query_sub.Append("  from ( select * ");
                        __query_sub.Append(" , case when " + _g.d.ic_trans_serial_number._trans_flag + " in (44) then ( select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where  " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans_serial_number._cust_code + ") else '' end as " + _g.d.ic_resource._ar_detail);
                        __query_sub.Append(" , (select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " r." + _g.d.ic_trans_serial_number._doc_date + " from " + _g.d.ic_trans_serial_number._table + " as r where r." + _g.d.ic_trans_serial_number._serial_number + " = " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + " and r." + _g.d.ic_trans_serial_number._trans_flag + " in (" + __serialFlagIn + ") order by " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ) as " + _g.d.resource_report._date_import);
                        __query_sub.Append(" from " + _g.d.ic_trans_serial_number._table);
                        __query_sub.Append(" where " + _g.d.ic_trans_serial_number._trans_flag + " not in (" + __serialExceptFlag + ")");
                        __query_sub.Append(" ) as temp1 ");

                        __orderDetail = " order by " + _g.d.ic_trans_serial_number._serial_number + "," + _g.d.ic_trans_serial_number._doc_date;
                         * */
                    }
                    break;
                case SMLERPReportTool._reportEnum.เงินสดย่อย_เคลื่อนไหว:
                    {

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

            //switch (this._mode)
            //{
            //    case SMLERPReportTool._reportEnum.Serial_number:
            //    case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
            //    case SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ:
            //    case SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
            //    case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
            //        this._displayDetail = true;
            //        break;
            //}

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
                case SMLERPReportTool._reportEnum.Serial_number:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_resource._item_name, _g.d.ic_resource._table + "." + _g.d.ic_resource._item_name, 30, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._status, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.resource_report._date_import, _g.d.resource_report._table + "." + _g.d.resource_report._date_import, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_resource._ic_cost, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_cost, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    }
                    break;
                case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 45, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    break;
                case SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 26, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._total_qty, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._check_stock_1, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._check_stock_2, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));

                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));


                    break;
                case SMLERPReportTool._reportEnum.Stock_no_count_no_balance:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    break;
                case SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._check_stock_1, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    break;
                case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_code, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans._cust_name, _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name, 40, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ar_customer._ar_type, _g.d.resource_report._table + "." + _g.d.resource_report._ar_type, 40, SMLReport._report._cellType.String, 0, __fontStyle));
                    break;

            }

            /*
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sale_name, null, 17, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, null, 17, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_before_vat, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._total_tax_at_pay, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._unit_code, false), null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._qty, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass("", "", 6, SMLReport._report._cellType.String, 0, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._discount_amount, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._sum_amount, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            */
        }

        private void _reportInitDetailColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.Serial_number:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no, null, 10, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag, null, 10, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._wh_code, null, 12, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._shelf_code, null, 12, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_resource._ar_detail, _g.d.ic_resource._table + "." + _g.d.ic_resource._ar_detail, 20, SMLReport._report._cellType.String, 0));
                    break;
                case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number, null, 40, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._wh_code, null, 30, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._shelf_code, null, 30, SMLReport._report._cellType.String, 0));
                    break;
                case SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number, null, 12, SMLReport._report._cellType.DateTime, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no, null, 10, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._wh_code, null, 12, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._shelf_code, null, 12, SMLReport._report._cellType.String, 0));
                    break;
                case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 14, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 32, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 6, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_resource._qty_sale, _g.d.ic_resource._table + "." + _g.d.ic_resource._qty_sale, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular, false, false));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount_exclude_vat, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_of_cost, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._profit, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_resource._profit_lost_persent, _g.d.ic_resource._table + "." + _g.d.ic_resource._profit_lost_persent, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular, false, false));

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
                        case SMLERPReportTool._reportEnum.Serial_number:
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
