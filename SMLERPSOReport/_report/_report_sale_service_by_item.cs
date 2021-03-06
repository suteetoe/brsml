﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace SMLERPSOReport._report
{
    public partial class _report_sale_service_by_item : UserControl
    {
        SMLReport._generate _report;
        String _screenName = "";
        DataTable _dataTableProduct;
        DataTable _dataTableDoc;
        string _levelNameProduct = "product";
        string _levelNameDoc = "doc";
        SMLERPReportTool._reportEnum _mode;
        string _transFlag;
        _condition_so _con_so;
        Boolean _displayDetail = false;

        public _report_sale_service_by_item(SMLERPReportTool._reportEnum mode, string screenName)
        {
            this._mode = mode;
            this._transFlag = (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้) ? "44" : "12";
            this._screenName = screenName;
            this._report = new SMLReport._generate(screenName, false);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report.Disposed += new EventHandler(_report_Disposed);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (level._levelName.Equals(this._levelNameProduct))
            {
                return this._dataTableProduct.Select();
            }
            else
                if (level._levelName.Equals(this._levelNameDoc))
                {
                    string __where = this._fieldName(_g.d.ic_trans_detail._item_code, false) + "=\'" + source[this._fieldName(_g.d.ic_trans_detail._item_code, false)].ToString() + "\'";
                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _reportInitProductColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = (this._displayDetail) ? FontStyle.Bold : FontStyle.Regular;
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._item_code, false), null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._item_name, false), null, 38, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._unit_code, false), null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._qty, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass("", "", 6, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._discount_amount, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._sum_amount, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
        }

        SMLReport._generateLevelClass _reportInitProduct(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitProductColumn(__columnList);
            /*FontStyle __fontStyle = (this._displayDetail) ? FontStyle.Bold : FontStyle.Regular;
            __columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._item_code, false), null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
            __columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._item_name, false), null, 38, SMLReport._report._cellType.String, 0, __fontStyle));
            __columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._unit_code, false), null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
            __columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._qty, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            __columnList.Add(new SMLReport._generateColumnListClass("", "", 6, SMLReport._report._cellType.String, 0, __fontStyle));
            __columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._discount_amount, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            __columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._sum_amount, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));*/
            return this._report._addLevel(this._levelNameProduct, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDocColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._doc_date, false), null, 8, SMLReport._report._cellType.DateTime, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._doc_no, false), null, 10, SMLReport._report._cellType.String, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName((SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้) ? _g.d.ic_trans_detail._ar_detail : _g.d.ic_trans_detail._ap_detail, false), null, 29, SMLReport._report._cellType.String, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._wh_code, false), null, 4, SMLReport._report._cellType.String, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._shelf_code, false), null, 4, SMLReport._report._cellType.String, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._unit_code, false), null, 6, SMLReport._report._cellType.String, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._qty, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._price, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._discount_amount, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._sum_amount_exclude_vat, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
        }

        SMLReport._generateLevelClass _reportInitDoc(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDocColumn(__columnList, spaceFirstColumnWidth);
            return this._report._addLevel(this._levelNameDoc, levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            this._displayDetail = this._con_so._condition_so_search3._getDataStr(_g.d.resource_report._display_detail).ToString().Equals("1") ? true : false;
            this._report._level = this._reportInitProduct(null, true, true);
            if (this._displayDetail == true)
            {
                SMLReport._generateLevelClass __level2 = this._reportInitDoc(this._report._level, false, 2, true);
            }
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitProductColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;
            /*this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitDocColumn(this._report._level.__grandTotal, 2);
            this._report._level._levelGrandTotal = __level2;*/
        }

        void _report__query()
        {
            string __dataQuery = "";
            string __startDate = this._con_so._condition_so_search3._getDataStrQuery(_g.d.resource_report._from_date).Replace("'", "");
            string __endDate = this._con_so._condition_so_search3._getDataStrQuery(_g.d.resource_report._to_date).Replace("'", "");
            string __fromItem = this._con_so._condition_so_search3._getDataStrQuery(_g.d.resource_report._from_item_code).Replace("'", "");
            string __toItem = this._con_so._condition_so_search3._getDataStrQuery(_g.d.resource_report._to_item_code).Replace("'", "");
            //
            string __getWhere = this._con_so._screen_grid_so1._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code);
            string __where = this._con_so._whereControl._getWhere(__getWhere).Replace(" where ", " and ");
            //
            StringBuilder __query = new StringBuilder();
            StringBuilder __query_sub = new StringBuilder();
            __query.Append("select item_code as " + this._fieldName(_g.d.ic_trans_detail._item_code, true) + ",");
            __query.Append("(select name_1 from ic_inventory where ic_inventory.code=ic_trans_detail.item_code) as " + this._fieldName(_g.d.ic_trans_detail._item_name, true) + ",");
            __query.Append("(select unit_cost from ic_inventory where ic_inventory.code=ic_trans_detail.item_code) as " + this._fieldName(_g.d.ic_trans_detail._unit_code, true) + ",");
            __query.Append("sum(qty * (stand_value/divide_value)) as " + this._fieldName(_g.d.ic_trans_detail._qty, true) + ",");
            __query.Append("sum(discount_amount) as " + this._fieldName(_g.d.ic_trans_detail._discount_amount, true) + ",");
            __query.Append("sum(sum_amount_exclude_vat) as " + this._fieldName(_g.d.ic_trans_detail._sum_amount, true));
            __query.Append(" from ic_trans_detail where last_status=0 and trans_flag=" + this._transFlag + " and item_type<>5" + __where);

            if (!__startDate.Equals("null") && !__endDate.Equals("null"))
            {
                __query.Append(" and doc_date between '" + __startDate + "' and '" + __endDate + "'");
            }
            if (!__fromItem.Equals("null") && !__toItem.Equals("null"))
            {
                __query.Append(" and item_code between '" + __fromItem + "' and '" + __toItem + "'");
            }

            __dataQuery = this._con_so._whereControl._getWhere2().Replace("where", " and ").Replace("(", "").Replace(")", " ");

            __query.Append(" " + __dataQuery + " group by item_code  ");
            __query.Append(" order by item_code");
            //------------------------------------------------------------------------------------------------------------------------
            __query_sub.Append("select item_code as " + this._fieldName(_g.d.ic_trans_detail._item_code, true) + ",");
            __query_sub.Append("doc_date as " + this._fieldName(_g.d.ic_trans_detail._doc_date, true) + ",");
            __query_sub.Append("doc_no as " + this._fieldName(_g.d.ic_trans_detail._doc_no, true) + ",");
            __query_sub.Append("cust_code,");
            if (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้)
            {
                __query_sub.Append("(select name_1 from ar_customer where ar_customer.code=cust_code)||' ('||cust_code||')' as " + this._fieldName(_g.d.ic_trans_detail._ar_detail, true) + ",");
            }
            else
            {
                __query_sub.Append("(select name_1 from ap_supplier where ap_supplier.code=cust_code)||' ('||cust_code||')' as " + this._fieldName(_g.d.ic_trans_detail._ap_detail, true) + ",");
            }
            __query_sub.Append("wh_code as " + this._fieldName(_g.d.ic_trans_detail._wh_code, true) + ",");
            __query_sub.Append("shelf_code as " + this._fieldName(_g.d.ic_trans_detail._shelf_code, true) + ",");
            __query_sub.Append("unit_code as " + this._fieldName(_g.d.ic_trans_detail._unit_code, true) + ",");
            __query_sub.Append("qty as " + this._fieldName(_g.d.ic_trans_detail._qty, true) + ",");
            __query_sub.Append("price_exclude_vat as " + this._fieldName(_g.d.ic_trans_detail._price, true) + ",");
            __query_sub.Append("discount_amount as " + this._fieldName(_g.d.ic_trans_detail._discount_amount, true) + ",");
            __query_sub.Append("sum_amount_exclude_vat as " + this._fieldName(_g.d.ic_trans_detail._sum_amount_exclude_vat, true));
            __query_sub.Append(" from ic_trans_detail where last_status=0 and trans_flag=" + this._transFlag + " and item_type<>5");
            __query_sub.Append(" and doc_date between '" + __startDate + "' and '" + __endDate + "'" + __where);
            __query_sub.Append(" order by doc_date,doc_time");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __queryHead = __query.ToString();
            string __queryDetail = __query_sub.ToString();
            this._dataTableProduct = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryHead).Tables[0];
            this._dataTableDoc = (_displayDetail) ? __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetail).Tables[0] : null;
        }

        private string _fieldName(string fieldName, Boolean forQuery)
        {
            return (forQuery) ? "\"" + _g.d.ic_trans_detail._table + "." + fieldName + "\"" : _g.d.ic_trans_detail._table + "." + fieldName;
        }

        void _showCondition()
        {
            //string __page = _apType.ToString();
            if (this._con_so == null)
            {
                // jead ทำแบบนี้เพราะ เมื่อกดเงื่อนไขซ้ำ ที่เลือกไปแล้ว จะได้เหมือนเดิม
                this._con_so = new _condition_so(this._mode, "  trans_flag =  " + this._transFlag);
                this._con_so.Text = this._screenName;
                this._con_so._whereControl._tableName = (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้) ? _g.d.ar_customer._table : _g.d.ap_supplier._table;
                //this._con_so._whereControl._addFieldComboBox(this._data_main());
                this._con_so.Size = new Size(500, 500);
            }

            this._con_so.ShowDialog();
            if (this._con_so.__check_submit)
            {
                /*this._data_condition = this._con_so.__where;
                this.__check_submit = this._con_so.__check_submit;
                this._so_config();*/
                //this.__data_ap = this._con_cash.__grid_where;
                //_view1._buildReport(SMLReport._report._reportType.Standard);
                string __beginDate = this._con_so._condition_so_search3._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._con_so._condition_so_search3._getDataStr(_g.d.resource_report._to_date);
                this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                this._report._conditionText = _g.g._conditionGrid(this._con_so._screen_grid_so1, this._report._conditionText);
                this._report._build();
            }
        }
    }
}
