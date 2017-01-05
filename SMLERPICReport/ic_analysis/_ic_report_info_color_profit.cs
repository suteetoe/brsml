using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport.ic_analysis
{
    public partial class _ic_report_info_color_profit : UserControl
    {
        SMLReport._generate _report;
        _analysis_condition _condition;
        SMLERPICInfo._infoStkProfitEnum _mode;
        String _screenName = "";
        DataTable _dataTableRoot;
        DataTable _dataTableProduct;
        DataTable _dataTableDoc;
        string _levelNameProduct = "product";
        string _levelNameDoc = "doc";

        public _ic_report_info_color_profit(string screenName, SMLERPICInfo._infoStkProfitEnum mode)
        {
            InitializeComponent();
            //
            this._screenName = screenName;
            this._mode = mode;
            this._report = new SMLReport._generate(screenName, true);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._updateDecimalValue += new SMLReport._generate.UpdateDecimalValueEventHandler(_report__updateDecimalValue);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._sumTotal += new SMLReport._generate.SumTotalEventHandler(_report__sumTotal);
            this._report.Disposed += new EventHandler(_report_Disposed);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        void _report__updateDecimalValue(SMLReport._generateLevelClass sender, SMLReport._generateColumnStyle isTotal)
        {
            decimal __amount = sender._columnList[sender._findColumnName(this._fieldName(_g.d.resource_report_color._amount_net, false))]._dataNumber;
            decimal __cost = sender._columnList[sender._findColumnName(this._fieldName(_g.d.resource_report_color._cost_net, false))]._dataNumber;
            decimal __profit = sender._columnList[sender._findColumnName(this._fieldName(_g.d.resource_report_color._profit_lost_amount, false))]._dataNumber;

            decimal __profitPersent = (__amount == 0) ? 0M : (__profit * 100M) / __amount;
            sender._columnList[sender._findColumnName(this._fieldName(_g.d.resource_report_color._profit_lost_persent, false))]._dataNumber = __profitPersent;
        }

        void _report__sumTotal(SMLReport._generateLevelClass level, int columnNumber, decimal value)
        {
            if (level != null && level.__grandTotal.Count > 0)
            {
                level.__grandTotal[columnNumber]._sumTotalColumn += value;
                if (level._levelParent != null)
                {
                    this._report__sumTotal(level._levelParent, columnNumber, value);
                }
            }
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            try
            {
                switch (this._mode)
                {
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                        return level._dataTable.Select();
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                        if (level._levelName.Equals(this._levelNameProduct))
                        {
                            return this._dataTableProduct.Select();
                        }
                        else
                            if (level._levelName.Equals(this._levelNameDoc))
                            {
                                StringBuilder __where = new StringBuilder();
                                for (int __loop = 0; __loop < levelParent._columnList.Count; __loop++)
                                {
                                    if (levelParent._columnList[__loop]._fieldName.Length > 0)
                                    {
                                        if (__where.Length > 0)
                                        {
                                            __where.Append(" and ");
                                        }
                                        __where.Append(levelParent._columnList[__loop]._fieldName + "=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                                    }
                                }
                                return this._dataTableDoc.Select(__where.ToString());
                            }
                        break;
                }
            }
            catch
            {
            }
            return null;
        }

        void _report__query()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                switch (this._mode)
                {
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                        this._report._level._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, this._query(this._mode)).Tables[0];
                        break;
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                        this._dataTableRoot = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, this._query(this._mode)).Tables[0];
                        this._dataTableProduct = MyLib._dataTableExtension._selectDistinct(this._dataTableRoot, this._fieldName(_g.d.resource_report_color._ic_code, false) + "," + this._fieldName(_g.d.resource_report_color._ic_name, false) + "," + this._fieldName(_g.d.resource_report_color._ic_unit, false));
                        this._dataTableDoc = this._dataTableRoot;
                        break;
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition(screenName);
        }

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            int __widthNumber = 6;
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._color_qty, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal));
            // Base
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._sale_amount, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._sale_cost, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._sale_profit, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
            // สีผสม
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._sale_qty_1, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._sale_amount_1, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._sale_cost_1, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._sale_profit_1, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
            // รวม
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._amount_net, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._cost_net, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._profit_lost_amount, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._profit_lost_persent, false), null, __widthNumber, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
        }

        /// <summary>
        /// สินค้า
        /// </summary>
        /// <param name="sumTotal"></param>
        SMLReport._generateLevelClass _reportInitProduct(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            __columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._ic_code, false), null, 15, SMLReport._report._cellType.String, 0));
            __columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._ic_name, false), null, 20, SMLReport._report._cellType.String, 0));
            __columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._ic_unit, false), null, 5, SMLReport._report._cellType.String, 0));
            switch (this._mode)
            {
                case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    _reportInitColumnValue(__columnList);
                    break;
                case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                    // เพิ่มช่องที่เหลือให้เต็ม 100%
                    __columnList.Add(new SMLReport._generateColumnListClass("", "", 60, SMLReport._report._cellType.String, 0));
                    break;
            }
            return this._report._addLevel(this._levelNameProduct, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDocColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._doc_date, false), null, 5, SMLReport._report._cellType.DateTime, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._doc_no, false), null, 10, SMLReport._report._cellType.String, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._ar_code, false), null, 5, SMLReport._report._cellType.String, 0));
            columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.resource_report_color._ar_detail, false), null, 15, SMLReport._report._cellType.String, 0));
        }

        SMLReport._generateLevelClass _reportInitDoc(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDocColumn(__columnList, spaceFirstColumnWidth);
            switch (this._mode)
            {
                case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                    this._reportInitColumnValue(__columnList);
                    break;
            }
            return this._report._addLevel(this._levelNameDoc, levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            switch (this._mode)
            {
                case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    this._report._level = this._reportInitProduct(null, true, true);
                    break;
                case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                    this._report._level = this._reportInitProduct(null, false, false);
                    SMLReport._generateLevelClass __level2 = this._reportInitDoc(this._report._level, true, 2, true);
                    // ยอดรวมแบบ Grand Total
                    this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
                    this._reportInitDocColumn(this._report._level.__grandTotal, 2);
                    this._reportInitColumnValue(this._report._level.__grandTotal);
                    this._report._level._levelGrandTotal = __level2;
                    break;
            }
        }

        void _showCondition(string screenName)
        {
            //string __page = _apType.ToString();
            if (this._condition == null)
            {
                // jead ทำแบบนี้เพราะ เมื่อกดเงื่อนไขซ้ำ ที่เลือกไปแล้ว จะได้เหมือนเดิม
                this._condition = new _analysis_condition(screenName);
                this._condition.Text = this._screenName;
                /*this._condition._whereControl._tableName = _g.d.ap_supplier._table;
                this._condition._whereControl._addFieldComboBox(this._data_main());*/
                this._condition.Size = new Size(500, 500);
                string __tabItemName = MyLib._myGlobal._resource("เลือกช่วงสินค้า");
                string __tabCustName = MyLib._myGlobal._resource("เลือกช่วงลูกค้า");
                switch (this._mode)
                {
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                        this._condition._tabControl.TabPages.RemoveAt(1);
                        this._condition._tabControl.TabPages[0].Text = __tabItemName;
                        break;
                    /*case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                        this._condition._grouper1.Dispose();
                        this._condition._grouper2.Dock = DockStyle.Fill;
                        break;*/
                }
            }
            this._condition.ShowDialog();
            if (this._condition.__check_submit)
            {
                //this._dataCondition = this._condition.__where;
                //this.__data_ap = this._con_cash.__grid_where;
                string __beginDate = this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._to_date);
                this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                this._report._conditionText = _g.g._conditionGrid(this._condition._screen_grid_analysis1, this._report._conditionText);

                this._report._build();
            }
        }

        string _query(SMLERPICInfo._infoStkProfitEnum mode)
        {
            string __dateBegin = MyLib._myGlobal._convertDateToQuery(this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._from_date));
            string __dateEnd = MyLib._myGlobal._convertDateToQuery(this._condition._condition_analysis_search1._getDataStr(_g.d.resource_report._to_date));
            string __itemCodeList = this._condition._screen_grid_analysis1._createWhere("item_code");
            //
            if (__itemCodeList.Length > 0)
            {
                __itemCodeList = " and " + __itemCodeList;
            }

            String __transDetailWhere = "ic_trans_detail.last_status=0 and ic_trans_detail.doc_date between \'" + __dateBegin + "\' and \'" + __dateEnd + "\' ";
            String __transDetailWhereCondition = "ic_trans_detail.item_code=ic_inventory.code and ic_trans_detail.item_type=5 and " + __transDetailWhere;
            String __transDetailRefGuid = "select distinct ref_guid from ic_trans_detail where " + __transDetailWhereCondition;
            //
            StringBuilder __queryGetData = new StringBuilder();
            string __fieldAdd1 = "";
            string __fieldAdd2 = "";
            string __groupByAdd = "";
            string __orderBy = "";
            __queryGetData.Append("select ");
            switch (mode)
            {
                case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    __orderBy = "code";
                    break;
                case SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร:
                    __queryGetData.Append("doc_date as " + this._fieldName(_g.d.resource_report_color._doc_date, true) + ",");
                    __queryGetData.Append("doc_no as " + this._fieldName(_g.d.resource_report_color._doc_no, true) + ",");
                    __queryGetData.Append("cust_code as " + this._fieldName(_g.d.resource_report_color._ar_code, true) + ",");
                    __queryGetData.Append("cust_detail as " + this._fieldName(_g.d.resource_report_color._ar_detail, true) + ",");
                    __fieldAdd1 = "doc_date,doc_no,cust_code,cust_detail,";
                    __fieldAdd2 = "doc_date,doc_no,cust_code,(select name_1 from ar_customer where ar_customer.code=cust_code) as cust_detail,";
                    __groupByAdd = "doc_date,doc_no,cust_code,cust_detail,";
                    __orderBy = "code,doc_date,doc_no";
                    break;
            }
            //
            __queryGetData.Append("code as " + this._fieldName(_g.d.resource_report_color._ic_code, true) + ",");
            __queryGetData.Append("name_1 as " + this._fieldName(_g.d.resource_report_color._ic_name, true) + ",");
            __queryGetData.Append("unit_name as " + this._fieldName(_g.d.resource_report_color._ic_unit, true) + ",");
            __queryGetData.Append("color_qty as " + this._fieldName(_g.d.resource_report_color._color_qty, true) + ",");
            __queryGetData.Append("sale_amount as " + this._fieldName(_g.d.resource_report_color._sale_amount, true) + ",");
            __queryGetData.Append("sale_cost as " + this._fieldName(_g.d.resource_report_color._sale_cost, true) + ",");
            __queryGetData.Append("sale_amount-sale_cost as " + this._fieldName(_g.d.resource_report_color._sale_profit, true) + ",");
            __queryGetData.Append("sale_qty_1 as " + this._fieldName(_g.d.resource_report_color._sale_qty_1, true) + ",");
            __queryGetData.Append("sale_amount_1 as " + this._fieldName(_g.d.resource_report_color._sale_amount_1, true) + ",");
            __queryGetData.Append("sale_cost_1 as " + this._fieldName(_g.d.resource_report_color._sale_cost_1, true) + ",");
            __queryGetData.Append("sale_amount_1-sale_cost_1 as " + this._fieldName(_g.d.resource_report_color._sale_profit_1, true) + ",");
            __queryGetData.Append("sale_amount+sale_amount_1 as " + this._fieldName(_g.d.resource_report_color._amount_net, true) + ","); // ยอดขายรวม
            __queryGetData.Append("sale_cost+sale_cost_1 as " + this._fieldName(_g.d.resource_report_color._cost_net, true) + ","); // ต้นทุนรวม
            __queryGetData.Append("(sale_amount+sale_amount_1)-(sale_cost+sale_cost_1) as " + this._fieldName(_g.d.resource_report_color._profit_lost_amount, true) + ","); // กำไรสุทธิ
            __queryGetData.Append("0 as " + this._fieldName(_g.d.resource_report_color._profit_lost_persent, true)); // กำไรสุทธิ (%)
            __queryGetData.Append(" from (");
            //
            __queryGetData.Append("select " + __fieldAdd1 + "code,name_1,unit_cost||'('||coalesce((select name_1 from ic_unit where ic_unit.code=unit_cost),'')||')' as unit_name,");
            __queryGetData.Append("sum(color_qty) as color_qty,sum(sale_amount) as sale_amount,sum(sale_cost) as sale_cost,sum(sale_qty_1) as sale_qty_1,sum(sale_amount_1) as sale_amount_1,sum(sale_cost_1) as sale_cost_1");
            __queryGetData.Append(" from (");
            __queryGetData.Append("select " + __fieldAdd2 + "ic_trans_detail.item_code as code,");
            __queryGetData.Append("(select name_1 from ic_inventory where ic_inventory.code=ic_trans_detail.item_code) as name_1,");
            __queryGetData.Append("(select unit_cost from ic_inventory where ic_inventory.code=ic_trans_detail.item_code) as unit_cost,");
            __queryGetData.Append("(qty * (calc_flag * -1)) * (stand_value/divide_value) as color_qty,");
            // มูลค่าขายสี base
            __queryGetData.Append("coalesce((select sum(sum_amount_exclude_vat * (calc_flag * -1)) from ic_trans_detail as b1 where b1.item_code<>'99' and b1.last_status=0 and b1.set_ref_line=ic_trans_detail.ref_guid and b1.trans_flag in (44,46,48)),0) as sale_amount,");
            // ต้นทุนขายสี base
            __queryGetData.Append("coalesce((select sum(sum_of_cost * (calc_flag * -1)) from ic_trans_detail as b2 where b2.item_code<>'99' and b2.last_status=0 and b2.set_ref_line=ic_trans_detail.ref_guid and b2.trans_flag in (44,46,48)),0) as sale_cost,");
            // จำนวนขายสีผสม
            __queryGetData.Append("coalesce((select sum((qty * (calc_flag * -1)) * (stand_value/divide_value)) from ic_trans_detail as b3 where b3.item_code='99' and b3.last_status=0 and b3.set_ref_line=ic_trans_detail.ref_guid and b3.trans_flag in (44,46,48)),0) as sale_qty_1,");
            // มูลค่าขายสีผสม
            __queryGetData.Append("coalesce((select sum(sum_amount_exclude_vat * (calc_flag * -1)) from ic_trans_detail as b4 where b4.item_code='99' and b4.last_status=0 and b4.set_ref_line=ic_trans_detail.ref_guid and b4.trans_flag in (44,46,48)),0) as sale_amount_1,");
            // ต้นทุนขายสีผสม
            __queryGetData.Append("coalesce((select sum(sum_of_cost * (calc_flag * -1)) from ic_trans_detail as b5 where b5.item_code='99' and b5.last_status=0 and b5.set_ref_line=ic_trans_detail.ref_guid and b5.trans_flag in (44,46,48)),0) as sale_cost_1");
            //
            __queryGetData.Append(" from ic_trans_detail where trans_flag in (44,46,48) and item_type = 5 and " + __transDetailWhere + __itemCodeList);
            __queryGetData.Append(" ) as temp2 group by " + __groupByAdd + "code,name_1,unit_name");
            __queryGetData.Append(" ) as temp1 where color_qty<>0 or sale_amount<>0 or sale_cost<>0 or sale_qty_1<>0 or sale_amount_1<>0 or sale_cost_1<>0");
            __queryGetData.Append(" order by " + __orderBy);
            //
            return __queryGetData.ToString();
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private string _fieldName(string fieldName, Boolean forQuery)
        {
            return (forQuery) ? "\"" + _g.d.resource_report_color._table + "." + fieldName + "\"" : _g.d.resource_report_color._table + "." + fieldName;
        }
    }
}
