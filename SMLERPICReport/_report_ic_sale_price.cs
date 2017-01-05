using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport
{
    public partial class _report_ic_sale_price : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTableRoot;
        private DataTable _dataTable;
        private DataTable _dataTableDetail;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        string _levelNameDetail = "doc";
        SMLReport._generateLevelClass _level2;

        /// <summary>
        /// 0 ราคาขายมาตรฐาน, 1 ราคาขายทั่วไป
        /// </summary>
        int _priceMode = 0;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenName">ชื่อจอ</param>
        /// <param name="mode">0 ราคาขายมาตรฐาน, 1 ราคาขายทั่วไป</param>
        public _report_ic_sale_price(string screenName, int mode)
        {
            this._priceMode = mode;
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
                    return this._dataTableDetail.Select(__where.ToString());
                }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Bold));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 50, SMLReport._report._cellType.String, 0, FontStyle.Bold));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, 30, SMLReport._report._cellType.String, 0, FontStyle.Bold));
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDetailColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._unit_code, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._from_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._to_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._from_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._to_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._sale_price1, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._sale_price2, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
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
            this._level2 = this._reportInitDetail(this._report._level, false, 2, true);
            this._report._resourceTable = ""; // แบบกำหนดเอง
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                try
                {
                    //
                    string __getWhere = this._form_condition._grid._createWhere(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._ic_code);
                    string __where = this._form_condition._extra._getWhere(__getWhere);
                    string __fieldName = "*fieldname*";
                    string __query1 = "||'~'||coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + __fieldName + "),'') ";
                    //

                    if (__where.Length > 0)
                    {
                        __where += " and ";
                    }
                    else
                    {
                        __where += " where ";
                    }
                    __where += _g.d.ic_inventory_price._price_mode + "=" + this._priceMode.ToString();

                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    StringBuilder __query = new StringBuilder();
                    __query.Append("select ");
                    __query.Append("ic_code as \"ic_resource.ic_code\",");
                    __query.Append("ic_name as \"ic_resource.ic_name\",");
                    __query.Append("ic_unit" + __query1.Replace(__fieldName, "ic_unit") + " as \"ic_resource.ic_unit_code\",");
                    __query.Append("unit_code" + __query1.Replace(__fieldName, "unit_code") + " as \"ic_inventory_price.unit_code\",");
                    __query.Append("from_date as \"ic_inventory_price.from_date\",");
                    __query.Append("to_date as \"ic_inventory_price.to_date\",");
                    __query.Append("from_qty as \"ic_inventory_price.from_qty\",");
                    __query.Append("to_qty as \"ic_inventory_price.to_qty\",");
                    __query.Append("sale_price1 as \"ic_inventory_price.sale_price1\",");
                    __query.Append("sale_price2 as \"ic_inventory_price.sale_price2\"");
                    __query.Append(" from (select ");
                    __query.Append("ic_code,");
                    __query.Append("(select name_1 from ic_inventory where ic_inventory.code=ic_inventory_price.ic_code) as ic_name,");
                    __query.Append("(select unit_standard from ic_inventory where ic_inventory.code=ic_inventory_price.ic_code) as ic_unit,");
                    __query.Append("unit_code,from_date,to_date,from_qty,to_qty,sale_price1,sale_price2 from ic_inventory_price " + __where + " order by ic_inventory_price.ic_code,from_date) as temp1 " + this._form_condition._extra._getOrderBy());
                    //
                    this._dataTableRoot = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                    //this._dataTable = MyLib._dataTableExtension._selectDistinct(this._dataTableRoot, _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_set_name+ "," + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_set_code+ "," + _g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_set_unit);
                    this._dataTable = MyLib._dataTableExtension._selectDistinct(this._dataTableRoot, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code + "," + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name + "," + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code);
                    this._dataTableDetail = this._dataTableRoot;
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
                this._report__init();
                this._form_condition = new SMLERPReportTool._conditionScreen(SMLERPReportTool._reportEnum.สินค้า_รายงานราคาขายสินค้า, this._screenName);
                this._form_condition._extra._tableName = _g.d.ic_inventory_price._table;
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
