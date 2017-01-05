using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPICReport
{
    public partial class _report_ic_none_movement : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";

        public _report_ic_none_movement(string screenName)
        {
            this._screenName = screenName;
            this._report = new SMLReport._generate(screenName, true);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report.Disposed += new EventHandler(_report_Disposed);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (columnNumber == sender._findColumnName(_g.d.ic_resource._table + "." + _g.d.ic_resource._last_in_flag) ||
                columnNumber == sender._findColumnName(_g.d.ic_resource._table + "." + _g.d.ic_resource._last_out_flag))
            {
                sender._columnList[columnNumber]._dataStr = (sender._columnList[columnNumber]._dataStr.Length == 0) ? "" : _g.g._transFlagGlobal._transName((MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr)));
            }
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
            return this._dataTable.Select();
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = FontStyle.Regular;
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 40, SMLReport._report._cellType.String, 0, __fontStyle));
            int __width = 15;
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, __width, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._average_cost, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._average_cost_end, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_amount, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._last_in_date, null, __width, SMLReport._report._cellType.DateTime, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._last_in_flag, null, __width, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._last_out_date, null, __width, SMLReport._report._cellType.DateTime, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._last_out_flag, null, __width, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._last_sale_date, null, __width, SMLReport._report._cellType.DateTime, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._no_movement_date, null, __width, SMLReport._report._cellType.Number, 0, __fontStyle));
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            this._report._level = this._reportInitRoot(null, true, true);
            this._report._resourceTable = ""; // แบบกำหนดเอง
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
                    int __countFormDay = (int)this._form_condition._screen._getDataNumber(_g.d.resource_report._count_day_from);
                    int __countToDay = (int)this._form_condition._screen._getDataNumber(_g.d.resource_report._count_day_to);
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
                    __query.Append("average_cost as \"ic_resource.average_cost\",");
                    __query.Append("average_cost_end as \"ic_resource.average_cost_end\",");
                    __query.Append("balance_amount as \"ic_resource.balance_amount\",");
                    __query.Append("last_in_date as \"ic_resource.last_in_date\",");
                    __query.Append("last_in_flag as \"ic_resource.last_in_flag\",");
                    __query.Append("last_out_date as \"ic_resource.last_out_date\",");
                    __query.Append("last_out_flag as \"ic_resource.last_out_flag\",");
                    __query.Append("last_sale_date as \"ic_resource.last_sale_date\",");
                    __query.Append("no_movement_date as \"ic_resource.no_movement_date\"");
                    __query.Append(" from (");
                    __query.Append(__process._stkStockNoMovementQuery((MyLib._myGrid)this._form_condition._grid, "", "", __countFormDay, __countToDay, __endDate));
                    __query.Append(") as temp9 ");
                    __query.Append(this._form_condition._extra._getOrderBy());
                    //
                    this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
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
                this._form_condition = new SMLERPReportTool._conditionScreen(SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าที่ไม่มีการเคลื่อนไหว, this._screenName);
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
                int __countFormDay = (int)this._form_condition._screen._getDataNumber(_g.d.resource_report._count_day_from);
                int __countToDay = (int)this._form_condition._screen._getDataNumber(_g.d.resource_report._count_day_to);
                this._report._conditionText = "สิ้นสุดวันที่" + " : " + MyLib._myGlobal._convertDateToString(__endDate, true);
                this._report._conditionText += "จำนวนวันไม่เคลื่อนไหว ช่วง " + __countFormDay.ToString() + " ถึง " + __countToDay.ToString() + " วัน";
                this._report._conditionText = _g.g._conditionGrid(this._form_condition._grid, this._report._conditionText);
                //
                this._report._build();
            }
        }
    }
}
