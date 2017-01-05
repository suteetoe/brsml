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
    public partial class _report_ic_info_stk_movement_sum : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        int _calcMode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0=ตามปริมาณ,1=ตามมูลค่า</param>
        /// <param name="screenName"></param>
        public _report_ic_info_stk_movement_sum(int calcMode, string screenName)
        {
            this._calcMode = calcMode;
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
            int __width = 13;
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, __width, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty_first, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_12, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_48, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_58, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_60, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_66, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_54, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_44, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_16, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_56, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_68, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, null, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
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
                    Boolean __movementOnly = this._form_condition._screen._getDataStr(_g.d.resource_report._movement_only).ToString().Equals("1") ? true : false;
                    DateTime __beginDate = MyLib._myGlobal._convertDate(this._form_condition._screen._getDataStr(_g.d.resource_report._from_date));
                    DateTime __endDate = MyLib._myGlobal._convertDate(this._form_condition._screen._getDataStr(_g.d.resource_report._to_date));
                    string __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code);
                    string __where = this._form_condition._extra._getWhere("").Replace(" where ", " and ");
                    string __itemGroup = this._form_condition._screen._getDataStr(_g.d.resource_report._item_group_code);
                    string __extraWhere = "";
                    if (__itemGroup.Length > 0)
                    {
                        string __genGroupList = MyLib._myUtil._genCodeList(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._group_main, __itemGroup);
                        __extraWhere = " exists (select code from ic_inventory where ic_inventory.code=" + _g.d.ic_resource._ic_code + " and " + __genGroupList + ")";
                    }
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                    //
                    StringBuilder __query = new StringBuilder();
                    __query.Append("select ");
                    __query.Append("ic_code as \"ic_resource.ic_code\",");
                    __query.Append("ic_name as \"ic_resource.ic_name\",");
                    __query.Append("ic_unit_code as \"ic_resource.ic_unit_code\",");
                    __query.Append("balance_qty_first as \"ic_resource.balance_qty_first\",");
                    __query.Append("balance_qty as \"ic_resource.balance_qty\",");
                    __query.Append("trans_flag_12 as \"ic_resource.trans_flag_12\",");
                    __query.Append("trans_flag_48 as \"ic_resource.trans_flag_48\",");
                    __query.Append("trans_flag_58 as \"ic_resource.trans_flag_58\",");
                    __query.Append("trans_flag_60 as \"ic_resource.trans_flag_60\",");
                    __query.Append("trans_flag_66 as \"ic_resource.trans_flag_66\",");
                    __query.Append("trans_flag_54 as \"ic_resource.trans_flag_54\",");
                    __query.Append("trans_flag_44 as \"ic_resource.trans_flag_44\",");
                    __query.Append("trans_flag_16 as \"ic_resource.trans_flag_16\",");
                    __query.Append("trans_flag_56 as \"ic_resource.trans_flag_56\",");
                    __query.Append("trans_flag_68 as \"ic_resource.trans_flag_68\"");
                    __query.Append(" from (");
                    __query.Append(__process._stkStockMovementSumQuery(_g.g._productCostType.ปรกติ, this._calcMode, (MyLib._myGrid)this._form_condition._grid, "", "", __beginDate, __endDate, __movementOnly, __extraWhere));
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
                this._form_condition = new SMLERPReportTool._conditionScreen(SMLERPReportTool._reportEnum.สินค้า_รายงานสรุปเคลื่อนไหวตามปริมาณ, this._screenName);
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
                //
                string __beginDate = this._form_condition._screen._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._form_condition._screen._getDataStr(_g.d.resource_report._to_date);
                this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                this._report._conditionText = _g.g._conditionGrid(this._form_condition._grid, this._report._conditionText);
                //
                this._report._build();
            }
        }
    }
}
