using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport.ic_analysis
{
    // ต่อไปต้องมีตามผู้จำหน่าย, ตามกลุ่มสินค้า
    public partial class _report_ic_stk_reorder : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private DataTable _dataTableDetail;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLReport._generateLevelClass _level2;
        SMLERPReportTool._reportEnum _mode;

        public _report_ic_stk_reorder(SMLERPReportTool._reportEnum mode, string screenName)
        {
            this._screenName = screenName;
            this._mode = mode;
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
            if (this._dataTable == null)
            {
                return null;
            }
            if (level._levelName.Equals(this._levelNameRoot))
            {
                return this._dataTable.Select();
            }
            else
                if (level._levelName.Equals(this._levelNameDetail))
            {
                StringBuilder __where = new StringBuilder();
                for (int __loop = 0; __loop < 1; __loop++)
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
                return (this._dataTableDetail == null) ? null : this._dataTableDetail.Select(__where.ToString());
            }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = FontStyle.Regular;
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.สินค้า_ถึงจุดสั่งซื้อ_ตามสินค้า:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 45, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._purchase_point, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, __fontStyle));
                    break;
            }
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDetailColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.สินค้า_ถึงจุดสั่งซื้อ_ตามสินค้า:
                    /*columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse, null, 28, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse_name, null, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._balance_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass("", null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));*/
                    break;
            }
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDetailColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitColumnValue(__columnList);
            return this._report._addLevel(this._levelNameDetail, levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            this._report._level = this._reportInitRoot(null, true, true);
            /*if (this._displayDetail == true)
            {
                this._level2 = this._reportInitDetail(this._report._level, false, 2, true);
            }*/
            this._report._resourceTable = ""; // แบบกำหนดเอง
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                try
                {
                    //
                    DateTime __endDate = MyLib._myGlobal._convertDate(this._form_condition._screen._getDataStr(_g.d.resource_report._to_date));
                    string __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code);
                    string __where = this._form_condition._extra._getWhere("").Replace(" where ", " and ");
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                    //
                    StringBuilder __query = new StringBuilder();
                    __query.Append("select ");
                    __query.Append("ic_code as \"ic_resource.ic_code\",");
                    __query.Append("ic_name as \"ic_resource.ic_name\",");
                    __query.Append("ic_unit_code as \"ic_resource.ic_unit_code\",");
                    __query.Append("balance_qty as \"ic_resource.balance_qty\",");
                    __query.Append("purchase_point as \"ic_resource.purchase_point\"");
                    __query.Append(" from (");
                    __query.Append("select ");
                    __query.Append("ic_code,");
                    __query.Append("ic_name,");
                    __query.Append("ic_unit_code,");
                    __query.Append("balance_qty,");
                    __query.Append("coalesce((select purchase_point from ic_inventory_detail where ic_inventory_detail.ic_code=temp9.ic_code),0) as purchase_point");
                    __query.Append(" from (");
                    __query.Append(__process._stkStockInfoAndBalanceQuery(_g.g._productCostType.ปรกติ, (MyLib._myGrid)this._form_condition._grid, "", "", __endDate, __endDate, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามสินค้า, "", "", false, false, ""));
                    __query.Append(") as temp9");
                    __query.Append(") as temp10 where purchase_point>0 and purchase_point>balance_qty");
                    __query.Append(this._form_condition._extra._getOrderBy());
                    //
                    /*StringBuilder __queryWareHouse = new StringBuilder();
                    __queryWareHouse.Append("select ");
                    __queryWareHouse.Append("ic_code as \"ic_resource.ic_code\","); // ต้องเป็น ic_resource เพื่อใช้ในการอ้างอิง
                    __queryWareHouse.Append("warehouse as \"resource_report_ic_column.warehouse\",");
                    __queryWareHouse.Append("coalesce((select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=warehouse),'') as \"resource_report_ic_column.warehouse_name\",");
                    __queryWareHouse.Append("ic_unit_code||'~'||coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=ic_unit_code" + "),ic_unit_code) as \"resource_report_ic_column.unit\",");
                    __queryWareHouse.Append("balance_qty as \"resource_report_ic_column.balance_qty\"");
                    __queryWareHouse.Append(" from (");
                    __queryWareHouse.Append(__process._stkStockInfoAndBalanceQuery((MyLib._myGrid)this._form_condition._condition_ic_grid, "", "", __endDate, __endDate, true, SMLERPICInfo._icInfoProcess._stockBalanceType.ยอดคงเหลือตามคลัง));
                    __queryWareHouse.Append(") as temp9");*/
                    //
                    this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                    this._dataTableDetail = null; //  __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryWareHouse.ToString()).Tables[0];
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new SMLERPReportTool._conditionScreen(this._mode, this._screenName);
                this._report__init();
                this._form_condition._extra._tableName = _g.d.ic_trans._table;
                string[] __fieldList = this._report._level.__fieldList(false).Split(',');
                this._form_condition._extra._addFieldComboBox(__fieldList);
                //this._form_condition._title = this._screenName;
            }
            this._form_condition.ShowDialog();
            if (this._form_condition._processClick)
            {
                this._dataTable = null; // จะได้ load data ใหม่
                DateTime __endDate = MyLib._myGlobal._convertDate(this._form_condition._screen._getDataStr(_g.d.resource_report._to_date));
                this._report._conditionText = "สิ้นสุดวันที่" + " : " + MyLib._myGlobal._convertDateToString(__endDate, true);
                this._report._conditionText = _g.g._conditionGrid(this._form_condition._grid, this._report._conditionText);
                //
                this._report._build();
            }
        }
    }
}
