using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport
{
    public partial class _report_ic_barcode : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private SMLERPReportTool._conditionScreen _form_condition;

        public _report_ic_barcode(string screenName)
        {
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
            return this._dataTable.Select();
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        SMLReport._generateLevelClass _reportInitProduct(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._description, null, 40, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._price, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
            return this._report._addLevel("temp", levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            this._report._level = this._reportInitProduct(null, true, true);
            this._report._resourceTable = ""; // แบบกำหนดเอง
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                try
                {
                    //
                    string __getWhere = this._form_condition._grid._createWhere(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code);
                    string __where = this._form_condition._extra._getWhere(__getWhere);
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "select " + this._report._level.__fieldList(true) + " from " + _g.d.ic_inventory_barcode._table + __where + this._form_condition._extra._getOrderBy();
                    string __query1 = "||'~'||coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=";
                    __query = __query.Replace("," + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code, "," + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + __query1 + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + "),'')");
                    this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query).Tables[0];
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
                this._form_condition = new SMLERPReportTool._conditionScreen(SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดบาร์โค๊ด, this._screenName);
                this._form_condition._extra._tableName = _g.d.ic_inventory_barcode._table;
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

