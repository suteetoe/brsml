using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport
{
    public partial class _report_ic_balance : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private DataTable _dataTableWarehouse;
        private DataTable _dataTableLocation;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        string _levelNameWareHouse = "warehouse";
        string _levelNameLocation = "location";
        SMLReport._generateLevelClass _level2;
        SMLReport._generateLevelClass _level3;
        Boolean _displayWarehouse = false;
        Boolean _displayLocation = false;
        SMLERPReportTool._reportEnum _mode;

        public _report_ic_balance(SMLERPReportTool._reportEnum mode, string screenName)
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
                if (level._levelName.Equals(this._levelNameWareHouse))
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
                            __where.Append("[" + levelParent._columnList[__loop]._fieldName + "]=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                        }
                    }
                    return (this._dataTableWarehouse == null) ? null : this._dataTableWarehouse.Select(__where.ToString());
                }
                else
                    if (level._levelName.Equals(this._levelNameLocation))
                    {
                        StringBuilder __where = new StringBuilder();
                        for (int __loop = 0; __loop < 3; __loop++)
                        {
                            if (levelParent._columnList[__loop]._fieldName.Length > 0)
                            {
                                if (__where.Length > 0)
                                {
                                    __where.Append(" and ");
                                }
                                __where.Append("[" + levelParent._columnList[__loop]._fieldName + "]=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                            }
                        }
                        return (this._dataTableLocation == null) ? null : this._dataTableLocation.Select(__where.ToString());
                    }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = (this._displayWarehouse) ? FontStyle.Bold : FontStyle.Regular;
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 45, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
                    {
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._average_cost, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._average_cost_end, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า_Lot:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 35, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._lot_number, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._average_cost, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
            }
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitWarehouseColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        private void _reportInitLocationColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        private void _reportInitWarehouseColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 0, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse, null, 28, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse_name, null, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._balance_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass("", null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    break;
            }
        }

        private void _reportInitLocationColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf, null, 28, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf_name, null, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._balance_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass("", null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    break;
            }
        }

        SMLReport._generateLevelClass _reportInitWarehouse(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitWarehouseColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitWarehouseColumnValue(__columnList);
            return this._report._addLevel(this._levelNameWareHouse, levelParent, __columnList, sumTotal, autoWidth);
        }

        SMLReport._generateLevelClass _reportInitLocation(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitLocationColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitLocationColumnValue(__columnList);
            return this._report._addLevel(this._levelNameLocation, levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า:
                    {
                        this._displayWarehouse = this._form_condition._screen._getDataStr(_g.d.resource_report._warehouse).ToString().Equals("1") ? true : false;
                        this._displayLocation = this._form_condition._screen._getDataStr(_g.d.resource_report._display_shelf).ToString().Equals("1") ? true : false;
                    }
                    break;
            }

            this._report._level = this._reportInitRoot(null, true, true);
            if (this._displayWarehouse == true)
            {
                this._level2 = this._reportInitWarehouse(this._report._level, false, 2, true);
                if (this._displayLocation)
                {
                    this._level3 = this._reportInitLocation(this._level2, false, 4, true);
                }
            }
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
                    Boolean __wareHouse = this._form_condition._screen._getDataStr(_g.d.resource_report._warehouse).ToString().Equals("1") ? true : false;
                    Boolean __allItem = this._form_condition._screen._getDataStr(_g.d.resource_report._displpay_item_balance_equal_zero).ToString().Equals("1") ? true : false;
                    Boolean __showBarcode = true;
                    string __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code);
                    string __where = this._form_condition._extra._getWhere("").Replace(" where ", " and ");
                    this._report._reportDescripton = MyLib._myGlobal._resource("ยอดคงเหลือสิ้นสุด ณ. วันที่") + " : " + this._form_condition._screen._getDataStr(_g.d.resource_report._to_date).ToString();
                    if (this._form_condition._selectWarehouseAndLocation != null)
                    {
                        this._report._reportDescripton = this._report._reportDescripton + " " + this._form_condition._selectWarehouseAndLocation._header();
                        this._report._reportDescripton = this._report._reportDescripton.Trim();
                    }
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                    //
                    switch (this._mode)
                    {
                        case SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า:
                            {
                                StringBuilder __query = new StringBuilder();
                                __query.Append("select ");
                                __query.Append("ic_code as \"ic_resource.ic_code\",");
                                __query.Append("ic_name as \"ic_resource.ic_name\",");
                                __query.Append("ic_unit_code as \"ic_resource.ic_unit_code\",");
                                __query.Append("balance_qty as \"ic_resource.balance_qty\",");
                                __query.Append("average_cost as \"ic_resource.average_cost\",");
                                __query.Append("average_cost_end as \"ic_resource.average_cost_end\",");
                                __query.Append("balance_amount as \"ic_resource.balance_amount\"");
                                __query.Append(" from (");
                                __query.Append(__process._stkStockInfoAndBalanceQuery(_g.g._productCostType.ปรกติ, (MyLib._myGrid)this._form_condition._grid, "", "", __endDate, __endDate, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามสินค้า, this._form_condition._selectWarehouseAndLocation._wareHouseSelected(), this._form_condition._selectWarehouseAndLocation._locationSelected(), (__allItem == true) ? false : true, __showBarcode, ""));
                                __query.Append(") as temp9 where ic_code<>'' " + ((__allItem) ? "" : " and balance_qty <> 0 ")); 
                                __query.Append(this._form_condition._extra._getOrderBy());
                                //
                                // toe fix bug error 0

                                // toe test
                                MyLib._myGlobal._writeLogFile(@"c:\smlsoft\query.txt", __query.ToString(), true);
                                DataSet __dataTableResult = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query.ToString());


                                this._dataTable = (__dataTableResult != null && __dataTableResult.Tables.Count > 0) ? __dataTableResult.Tables[0] : null; // __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                                if (this._dataTable != null && this._dataTable.Columns.Count > 0)
                                    this._dataTable.PrimaryKey = new DataColumn[] { this._dataTable.Columns[0] };
                                this._dataTableWarehouse = null;
                                this._dataTableLocation = null;
                                if (this._displayWarehouse)
                                {
                                    StringBuilder __queryWareHouse = new StringBuilder();
                                    __queryWareHouse.Append("select ");
                                    __queryWareHouse.Append("ic_code as \"ic_resource.ic_code\","); // ต้องเป็น ic_resource เพื่อใช้ในการอ้างอิง
                                    __queryWareHouse.Append("warehouse as \"resource_report_ic_column.warehouse\",");
                                    __queryWareHouse.Append("coalesce((select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=warehouse),'') as \"resource_report_ic_column.warehouse_name\",");
                                    __queryWareHouse.Append("ic_unit_code||'~'||coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=ic_unit_code" + "),ic_unit_code) as \"resource_report_ic_column.unit\",");
                                    __queryWareHouse.Append("balance_qty as \"resource_report_ic_column.balance_qty\"");
                                    __queryWareHouse.Append(" from (");
                                    __queryWareHouse.Append(__process._stkStockInfoAndBalanceQuery(_g.g._productCostType.ปรกติ, (MyLib._myGrid)this._form_condition._grid, "", "", __endDate, __endDate, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามคลัง, this._form_condition._selectWarehouseAndLocation._wareHouseSelected(), this._form_condition._selectWarehouseAndLocation._locationSelected(), (__allItem) ? false : true, __showBarcode, ""));
                                    __queryWareHouse.Append(") as temp9 where ic_code<>'' and warehouse<>'' order by ic_code,warehouse");
                                    //

                                    DataSet __dataWarehouseResult = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryWareHouse.ToString());
                                    this._dataTableWarehouse = (__dataWarehouseResult.Tables.Count > 0) ? __dataWarehouseResult.Tables[0] : null;
                                    if (this._dataTableWarehouse != null && this._dataTableWarehouse.Columns.Count > 0)
                                        this._dataTableWarehouse.PrimaryKey = new DataColumn[] { this._dataTableWarehouse.Columns[0], this._dataTableWarehouse.Columns[1] };
                                    if (this._displayLocation)
                                    {
                                        StringBuilder __queryLocation = new StringBuilder();
                                        __queryLocation.Append("select ");
                                        __queryLocation.Append("ic_code as \"ic_resource.ic_code\","); // ต้องเป็น ic_resource เพื่อใช้ในการอ้างอิง
                                        __queryLocation.Append("warehouse as \"resource_report_ic_column.warehouse\",");
                                        __queryLocation.Append("location as \"resource_report_ic_column.shelf\",");
                                        __queryLocation.Append("coalesce((select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=warehouse),'') as \"resource_report_ic_column.warehouse_name\",");
                                        __queryLocation.Append("ic_unit_code||'~'||coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=ic_unit_code" + "),ic_unit_code) as \"resource_report_ic_column.unit\",");
                                        __queryLocation.Append("balance_qty as \"resource_report_ic_column.balance_qty\"");
                                        __queryLocation.Append(" from (");
                                        __queryLocation.Append(__process._stkStockInfoAndBalanceQuery(_g.g._productCostType.ปรกติ, (MyLib._myGrid)this._form_condition._grid, "", "", __endDate, __endDate, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามที่เก็บ, this._form_condition._selectWarehouseAndLocation._wareHouseSelected(), this._form_condition._selectWarehouseAndLocation._locationSelected(), (__allItem) ? false : true, __showBarcode, ""));
                                        __queryLocation.Append(") as temp9 where ic_code<>'' and warehouse<>'' and location<>'' order by ic_code,warehouse,location");
                                        //

                                        DataSet __dataLoctionResult = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryLocation.ToString());
                                        this._dataTableLocation = (__dataLoctionResult.Tables.Count > 0) ? __dataLoctionResult.Tables[0] : null;
                                        if (this._dataTableLocation != null && this._dataTableLocation.Columns.Count > 0)
                                            this._dataTableLocation.PrimaryKey = new DataColumn[] { this._dataTableLocation.Columns[0], this._dataTableLocation.Columns[1], this._dataTableLocation.Columns[2] };
                                    }
                                }
                            }
                            break;
                        case SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า_Lot:
                            {
                                string __query = __process._stkStockInfoAndBalanceByLotQuery(_g.g._productCostType.ปรกติ, this._form_condition._grid, "", "", __endDate, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT);

                                StringBuilder __query1 = new StringBuilder();
                                __query1.Append("select ");
                                __query1.Append(_g.d.ic_resource._ic_code + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code + "\" ");
                                __query1.Append("," + _g.d.ic_resource._ic_name + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name + "\" ");
                                __query1.Append("," + _g.d.ic_resource._ic_unit_code + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code + "\" ");
                                __query1.Append("," + _g.d.ic_resource._lot_number + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._lot_number + "\" ");
                                __query1.Append("," + _g.d.ic_resource._balance_qty + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty + "\" ");
                                __query1.Append("," + _g.d.ic_resource._average_cost + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._average_cost + "\" ");
                                __query1.Append("," + _g.d.ic_resource._balance_amount + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._balance_amount + "\" ");

                                __query1.Append(" from (");
                                __query1.Append(__query);
                                __query1.Append(" ) as temp99 ");

                                DataSet __dataTableResult = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query1.ToString());
                                this._dataTable = (__dataTableResult != null && __dataTableResult.Tables.Count > 0) ? __dataTableResult.Tables[0] : null; // __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];

                            }
                            break;
                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.ToString());
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
                this._report._conditionText = _g.g._conditionGrid(this._form_condition._grid, this._report._conditionText);
                //
                this._report._build();
            }
        }
    }
}
