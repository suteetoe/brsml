using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReportTool
{
    public partial class _reportCustMaster : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        Boolean _displayDetail = false;
        SMLERPReportTool._reportEnum _mode;

        public _reportCustMaster(SMLERPReportTool._reportEnum mode, string screenName)
        {
            this._screenName = screenName;
            this._mode = mode;
            this._report = new SMLReport._generate(screenName, true);
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
            this._showCondition(screenName);
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = (this._displayDetail) ? FontStyle.Bold : FontStyle.Regular;
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.เจ้าหนี้_รายละเอียด:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_supplier._table + "." + _g.d.ap_supplier._code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_supplier._table + "." + _g.d.ap_supplier._name_1, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_supplier._table + "." + _g.d.ap_supplier._address, null, 30, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_supplier._table + "." + _g.d.ap_supplier._telephone, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_supplier._table + "." + _g.d.ap_supplier._fax, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_supplier._table + "." + _g.d.ap_supplier._email, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    break;
                case SMLERPReportTool._reportEnum.ลูกหนี้_รายละเอียด:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._name_1, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._address, null, 30, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._telephone, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._fax, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._email, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._ar_type, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._table + "." + _g.d.ar_customer._group_main, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
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
            }
        }

        void _report__init()
        {
            this._displayDetail = this._form_condition._screen._getDataStr(_g.d.resource_report._display_detail).ToString().Equals("1") ? true : false;
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
                    string __getWhere = "";
                    string __where = this._form_condition._extra._getWhere("");
                    switch (this._mode)
                    {
                        case _reportEnum.ลูกหนี้_รายละเอียด:
                            __getWhere = this._form_condition._grid._createWhere(_g.d.ar_customer._table + "." + _g.d.ar_customer._code);
                            break;
                        case _reportEnum.เจ้าหนี้_รายละเอียด:
                            __getWhere = this._form_condition._grid._createWhere(_g.d.ap_supplier._table + "." + _g.d.ap_supplier._code);
                            break;
                    }
                    if (__getWhere.Length > 0)
                    {
                        __getWhere = ((__where.Length == 0) ? " where " : " and ") + __getWhere;
                    }
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    //
                    StringBuilder __query = new StringBuilder();
                    StringBuilder __address = new StringBuilder("address||case when (coalesce((select name_1 from erp_tambon where erp_tambon.code=tambon),'')) = '' then '' else ' ต.'||coalesce((select name_1 from erp_tambon where erp_tambon.code=tambon),'') end");
                    __address.Append("||case when (coalesce((select name_1 from erp_amper where erp_amper.code=amper),'')) = '' then '' else ' อ.'||coalesce((select name_1 from erp_amper where erp_amper.code=amper),'') end");
                    __address.Append("||case when (coalesce((select name_1 from erp_province where erp_province.code=province),'')) = '' then '' else ' จ.'||coalesce((select name_1 from erp_province where erp_province.code=province),'') end");
                    switch (this._mode)
                    {
                        case _reportEnum.ลูกหนี้_รายละเอียด:
                            __query.Append("select ");
                            __query.Append("code as \"ar_customer.code\",");
                            __query.Append("name_1 as \"ar_customer.name_1\",");
                            __query.Append(__address +" as \"ar_customer.address\",");
                            __query.Append("telephone as \"ar_customer.telephone\",");
                            __query.Append("fax as \"ar_customer.fax\",");
                            __query.Append("email as \"ar_customer.email\",");
                            __query.Append("ar_type||'/'||coalesce((select name_1 from ar_type where ar_type.code=ar_customer.ar_type),'') as \"ar_customer.ar_type\",");
                            __query.Append("coalesce((select group_main from ar_customer_detail where ar_customer_detail.ar_code=code),'')||case when coalesce((select group_main from ar_customer_detail where ar_customer_detail.ar_code=code),'')='' then '' else '/' end||coalesce((select name_1 from ar_group where ar_group.code=coalesce((select group_main from ar_customer_detail where ar_customer_detail.ar_code=ar_customer.code),'')),'') as \"ar_customer.group_main\"");
                            __query.Append(" from ar_customer ");
                            break;
                        case _reportEnum.เจ้าหนี้_รายละเอียด:
                            __query.Append("select ");
                            __query.Append("code as \"ap_supplier.code\",");
                            __query.Append("name_1 as \"ap_supplier.name_1\",");
                            __query.Append(__address+" as \"ap_supplier.address\",");
                            __query.Append("telephone as \"ap_supplier.telephone\",");
                            __query.Append("fax as \"ap_supplier.fax\",");
                            __query.Append("email as \"ap_supplier.email\"");
                            __query.Append(" from ap_supplier ");
                            break;
                    }
                    //
                    __query.Append(__where + __getWhere);
                    __query.Append(this._form_condition._extra._getOrderBy());
                    this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        private void _showCondition(string screenName)
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
                //
                this._report._build();
            }
        }
    }
}
