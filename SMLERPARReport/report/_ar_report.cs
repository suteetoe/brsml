using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARReport.report
{
    public partial class _ar_report : UserControl
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

        public _ar_report(SMLERPReportTool._reportEnum mode, string screenName)
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
            this._showCondition();

        }

        public void _showCondition()
        {
            if (this._condition == null)
            {
                //_condition = new SMLERPReportTool._conditionScreen(_icType, "");
                this._condition = new SMLERPReportTool._conditionScreen(this._mode, this._screenName);

                this._condition._extra._tableName = _g.d.ic_inventory._table;
                this._condition._extra._searchTextWord.Visible = false;
                this._condition._extra._orderByComboBox.Visible = false;
                this._condition._extra._orderByComboBox.Dispose();
                this._condition.Size = new Size(600, 600);
            }

            this._condition.ShowDialog();
            if (this._condition._processClick)
            {

                string __beginDate = this._condition._screen._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._condition._screen._getDataStr(_g.d.resource_report._to_date);

                switch (this._mode)
                {
                    case SMLERPReportTool._reportEnum.Serial_number:
                        this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                        break;
                }


                this._report._conditionText = _g.g._conditionGrid(this._condition._grid, this._report._conditionText);

                this._report._build();
            }
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            //throw new NotImplementedException();
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
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
                        case SMLERPReportTool._reportEnum.ลูกหนี้_แต้มคงเหลือ:
                            {
                                string __where = _g.d.pos_resource._table + "." + _g.d.resource_report._custom_code + "=\'" + source[_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_code].ToString() + "\'";
                                return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                            }
                        //case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                        //    {
                        //        string __where = _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + "=\'" + source[_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code].ToString() + "\'";
                        //        return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                        //    }
                    }
                }

            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = (this._displayDetail) ? FontStyle.Bold : FontStyle.Regular;

            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.ลูกหนี้_แต้มคงเหลือ:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_code, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_name, null, 60, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._point_balance, null, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._status, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.resource_report._date_import, _g.d.resource_report._table + "." + _g.d.resource_report._date_import, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_resource._ic_cost, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_cost, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    }
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

            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));

            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.ลูกหนี้_แต้มคงเหลือ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.pos_resource._table + "." + _g.d.pos_resource._doc_date, null, 20, SMLReport._report._cellType.DateTime, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.pos_resource._table + "." + _g.d.pos_resource._doc_time, null, 20, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.pos_resource._table + "." + _g.d.pos_resource._doc_no, null, 20, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.pos_resource._table + "." + _g.d.pos_resource._point_add, null, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.pos_resource._table + "." + _g.d.pos_resource._point_use, null, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
                    break;
                //case SMLERPReportTool._reportEnum.Serial_number:
                //    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                //    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0));
                //    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no, null, 10, SMLReport._report._cellType.String, 0));
                //    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag, null, 10, SMLReport._report._cellType.String, 0));
                //    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._wh_code, null, 12, SMLReport._report._cellType.String, 0));
                //    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._shelf_code, null, 12, SMLReport._report._cellType.String, 0));
                //    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_resource._ar_detail, _g.d.ic_resource._table + "." + _g.d.ic_resource._ar_detail, 20, SMLReport._report._cellType.String, 0));
                //    break;
                //case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                //    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                //    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number, null, 40, SMLReport._report._cellType.String, 0));
                //    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._wh_code, null, 30, SMLReport._report._cellType.String, 0));
                //    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._shelf_code, null, 30, SMLReport._report._cellType.String, 0));
                //    break;

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

        void _report__init()
        {
            this._displayDetail = this._condition._screen._getDataStr(_g.d.resource_report._display_detail).ToString().Equals("1") ? true : false;
            this._report._level = this._reportInitRoot(null, true, true);
            if (this._displayDetail == true)
            {
                SMLReport._generateLevelClass __level2 = this._reportInitDetail(this._report._level, false, 2, true);
            }
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;

            /*
            this._extraWhere = extraWhere;
            this._displayDetail = this._form_condition._screen._getDataStr(_g.d.resource_report._display_detail).ToString().Equals("1") ? true : false;
            this._showBarcode = this._form_condition._screen._getDataStr(_g.d.resource_report._show_barcode).ToString().Equals("1") ? true : false;
            this._report._level = this._reportInitRoot(null, true, true);
            if (this._displayDetail == true)
            {
                this._level2 = this._reportInitDetail(this._report._level, false, 2, true);
            }
            this._report._resourceTable = ""; // แบบกำหนดเอง
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;

             */
        }

        void _report__query()
        {
            StringBuilder __query = new StringBuilder();
            StringBuilder __queryDetail = new StringBuilder();

            string __where = "";
            string __whereDetail = "";

            string __order = "";
            string __orderDetail = "";

            // get extrawhere

            // main query
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.ลูกหนี้_แต้มคงเหลือ:
                    {
                        string __getWhere = this._condition._grid._createWhere(_g.d.resource_report._table + "." + _g.d.resource_report._from_ar);
                        string __to_date = this._condition._screen._getDataStrQuery(_g.d.resource_report._to_date);
                        // StringBuilder __where = new StringBuilder();
                        __order = "code";
                        __orderDetail = "doc_date, doc_no";
                        string __whereCus = __getWhere.Replace(_g.d.resource_report._table + "." + _g.d.resource_report._from_ar, _g.d.ar_customer._table + "." + _g.d.ar_customer._code);

                        string __dateWhere = " doc_date <= " + __to_date;
                        // root
                        __query.Append("select ");
                        __query.Append(" code as \"" + _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_code + "\",");
                        __query.Append(" name_1 as \"" + _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_name + "\",");
                        __query.Append(" point_balance \"" + _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._point_balance + "\"");
                        __query.Append(" from (");
                        __query.Append("select code, name_1," +
                            "(" +
                                "coalesce((select sum(case when (trans_flag = 802) then -1*ic_trans.sum_point else ic_trans.sum_point end) from ic_trans where ic_trans.doc_date <= " + __to_date + " and ic_trans.cust_code= ar_customer.code and ( ic_trans.is_pos=1 or ic_trans.trans_flag in (801, 802) ) and ic_trans.last_status=0 and ic_trans.sum_point <> 0 ), 0) - " +
                                "coalesce((select sum(cb_trans.point_qty) from cb_trans where cb_trans.doc_date <= " + __to_date + "  and cb_trans.ap_ar_code= ar_customer.code and cb_trans.point_qty <> 0 and coalesce((select last_status from ic_trans where ic_trans.doc_no=cb_trans.doc_no  and cb_trans.trans_flag = ic_trans.trans_flag ),0)=0), 0) " +
                             ") as point_balance ");
                        __query.Append(" from ar_customer ");
                        if (__whereCus.Length > 0)
                        {
                            __query.Append(" where " + __whereCus);
                        }

                        __query.Append(" ) as temp1 ");

                        __where = ((__where.Length > 0) ? " and " + "(" + __where + ")" : "") + " point_balance > 0 ";



                        // detail
                        string __where_cus1 = __getWhere.Replace(_g.d.resource_report._table + "." + _g.d.resource_report._from_ar, _g.d.ic_trans._cust_code);
                        string __where_cus2 = __getWhere.Replace(_g.d.resource_report._table + "." + _g.d.resource_report._from_ar, _g.d.cb_trans._ap_ar_code);

                        __queryDetail.Append("select " + MyLib._myGlobal._fieldAndComma(
                            _g.d.resource_report._custom_code + " as \"" + _g.d.pos_resource._table + "." + _g.d.resource_report._custom_code + "\"",
                            _g.d.pos_resource._doc_date + " as \"" + _g.d.pos_resource._table + "." + _g.d.pos_resource._doc_date + "\"",
                            _g.d.ic_trans._trans_flag + " as \"" + _g.d.pos_resource._table + "." + _g.d.ic_trans._trans_flag + "\"",
                            _g.d.pos_resource._doc_time + " as \"" + _g.d.pos_resource._table + "." + _g.d.pos_resource._doc_time + "\"",
                            _g.d.pos_resource._doc_no + " as \"" + _g.d.pos_resource._table + "." + _g.d.pos_resource._doc_no + "\"",
                            _g.d.pos_resource._sale_code + " as \"" + _g.d.pos_resource._table + "." + _g.d.pos_resource._sale_code + "\"",
                            _g.d.pos_resource._pos_id + " as \"" + _g.d.pos_resource._table + "." + _g.d.pos_resource._pos_id + "\"",
                            _g.d.pos_resource._doc_amount + " as \"" + _g.d.pos_resource._table + "." + _g.d.pos_resource._doc_amount + "\"",
                            _g.d.pos_resource._point_add + " as \"" + _g.d.pos_resource._table + "." + _g.d.pos_resource._point_add + "\"",
                            _g.d.pos_resource._point_use + " as \"" + _g.d.pos_resource._table + "." + _g.d.pos_resource._point_use + "\""
                            ) + " from (select " +
                            MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans._cust_code + " as " + _g.d.resource_report._custom_code,
                            _g.d.ic_trans._doc_date + " as " + _g.d.pos_resource._doc_date,
                            _g.d.ic_trans._trans_flag + " as " + _g.d.ic_trans._trans_flag,
                            _g.d.ic_trans._doc_time + " as " + _g.d.pos_resource._doc_time,
                            _g.d.ic_trans._doc_no + " as " + _g.d.pos_resource._doc_no,
                            _g.d.ic_trans._sale_code + " as " + _g.d.pos_resource._sale_code,
                            _g.d.ic_trans._pos_id + " as " + _g.d.pos_resource._pos_id,
                            _g.d.ic_trans._total_amount + " as " + _g.d.pos_resource._doc_amount,
                            " case when trans_flag = 802 then -1*" + _g.d.ic_trans._sum_point + " else " + _g.d.ic_trans._sum_point + " end as " + _g.d.pos_resource._point_add,
                            "0 as " + _g.d.pos_resource._point_use) +
                            " from " + _g.d.ic_trans._table + " where ( doc_date <= " + __to_date + " and " + _g.d.ic_trans._is_pos + "=1 or " + _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString() + ", " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดแต้ม).ToString() + ") ) and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._sum_point + "<>0 " + ((__where_cus1.Length > 0) ? " and " + "(" + __where_cus1 + ")" : "") + " union all " +
                            "select " + MyLib._myGlobal._fieldAndComma(
                            _g.d.cb_trans._ap_ar_code + " as " + _g.d.resource_report._custom_code,
                            _g.d.cb_trans._doc_date + " as " + _g.d.pos_resource._doc_date,
                            _g.d.cb_trans._trans_flag + " as " + _g.d.cb_trans._trans_flag,
                            _g.d.cb_trans._doc_time + " as " + _g.d.pos_resource._doc_time,
                            _g.d.cb_trans._doc_no + " as " + _g.d.pos_resource._doc_no,
                            "\'\'" + " as " + _g.d.pos_resource._sale_code,
                            "\'\'" + " as " + _g.d.pos_resource._pos_id,
                            "0 as " + _g.d.pos_resource._doc_amount,
                            "0 as " + _g.d.pos_resource._point_add,
                            _g.d.cb_trans._point_qty + " as " + _g.d.pos_resource._point_use) +
                            " from " + _g.d.cb_trans._table + " where   doc_date <= " + __to_date + " and  " + _g.d.cb_trans._point_qty + "<>0 " + ((__where_cus2.Length > 0) ? " and " + "(" + __where_cus2 + ")" : "") + " and coalesce((select " + _g.d.ic_trans._last_status + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=" + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + " and  " + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + "=" + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + " ),0)=0) as t1 "); // order by  + MyLib._myGlobal._fieldAndComma(_g.d.pos_resource._doc_date, _g.d.pos_resource._doc_time, _g.d.pos_resource._doc_no));

                    }
                    break;
            }

            // append where
            __query.Append(((__where.Length > 0) ? " where " + __where : ""));
            __queryDetail.Append(((__whereDetail.Length > 0) ? " where " + __whereDetail : ""));

            // append order by
            __query.Append(((__order.Length > 0) ? " order by " + __order : ""));
            __queryDetail.Append(((__orderDetail.Length > 0) ? " order by " + __orderDetail : ""));

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __queryStr = __query.ToString();
            string __queryDetailStr = __queryDetail.ToString();

            this._dataTableProduct = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryStr).Tables[0];
            this._dataTableDoc = (_displayDetail) ? __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetailStr).Tables[0] : null;

        }
    }
}
