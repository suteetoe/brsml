using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SMLERPICReport
{
    public partial class _report_ic_stk_movement : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLReport._generateLevelClass _level2;
        SMLERPReportTool._reportEnum _mode;
        string _beginDate;
        string _endDate;

        public _report_ic_stk_movement(SMLERPReportTool._reportEnum mode, string screenName)
        {
            this._screenName = screenName;
            this._mode = mode;
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
            if (isTotal == SMLReport._generateColumnStyle.Total)
            {
                if (columnNumber == sender._findColumnName(_g.d.ic_resource._cost_in) ||
                    columnNumber == sender._findColumnName(_g.d.ic_resource._cost_out) ||
                    columnNumber == sender._findColumnName(_g.d.ic_resource._balance_qty) ||
                    columnNumber == sender._findColumnName(_g.d.ic_resource._average_cost) ||
                    columnNumber == sender._findColumnName(_g.d.ic_resource._amount))
                {
                    sender._columnList[columnNumber]._dataNumber = 0M;
                }
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
            if (level._levelName.Equals(this._levelNameRoot))
            {
                return this._dataTable.Select();
            }
            else
                if (level._levelName.Equals(this._levelNameDetail))
                {
                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                    string __itemCode = source[levelParent._columnList[0]._fieldName].ToString().Replace("\'", "\'\'");
                    string __result = __smlFrameWork._process_stock_cost(MyLib._myGlobal._databaseName, __itemCode.ToString(), 1, this._beginDate, this._endDate);


                    DataSet __ds = MyLib._myGlobal._convertStringToDataSet(__result);
                    if (__ds.Tables.Count == 0)
                    {

                        MessageBox.Show("error : " + __itemCode + "\n" + __result);
                        return null;
                    }
                    else if (this._mode == SMLERPReportTool._reportEnum.สินค้า_รายงานเคลื่อนไหว && this._form_condition._screen._getDataStr(_g.d.resource_report._show_remark).Equals("1"))
                    {
                        // get remark and add to new result 
                        StringBuilder __docNoList = new StringBuilder();
                        for (int __row = 0; __row < __ds.Tables[0].Rows.Count; __row++)
                        {
                            string __getDocNo = __ds.Tables[0].Rows[__row][_g.d.ic_resource._doc_no].ToString();

                            if (__docNoList.Length > 0)
                            {
                                __docNoList.Append(",");
                            }
                            __docNoList.Append("\'" + __getDocNo + "\'");
                        }

                        // query
                        if (__docNoList.Length > 0)
                        {
                            string __getRemarkQuery = "select " + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._remark + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " in (" + __docNoList.ToString() + ")";
                            DataTable __remarkTable = __smlFrameWork._queryShort(__getRemarkQuery).Tables[0];

                            XmlDocument __doc = new XmlDocument();
                            __doc.LoadXml(__result);

                            XmlNodeList __nodeList = __doc.SelectNodes("ResultSet/Row");
                            int __count = __nodeList.Count;

                            int __docNoIndex = -1;
                            for (int nodeIndex = 0; nodeIndex < __nodeList[0].ChildNodes.Count; nodeIndex++)
                            {
                                if (__nodeList[0].ChildNodes[nodeIndex].Name.Equals("doc_no"))
                                {
                                    __docNoIndex = nodeIndex;
                                    break;
                                }
                            }

                            foreach (XmlNode __node in __nodeList)
                            {
                                string __getDocNo = __node.ChildNodes[__docNoIndex].InnerText;

                                DataRow[] __row = __remarkTable.Select("doc_no=\'" + __getDocNo + "\'");
                                string __remark = "";
                                if (__row.Length > 0)
                                {
                                    __remark = __row[0][_g.d.ic_trans._remark].ToString();
                                    // add remark node
                                }

                                string __strReplace = "<" + _g.d.ic_trans_detail._remark + ">" + __remark + "</" + _g.d.ic_trans_detail._remark + ">";
                                __result = __result.Replace(__node.ChildNodes[__docNoIndex].OuterXml, __node.ChildNodes[__docNoIndex].OuterXml + __strReplace);
                            }

                            __ds = MyLib._myGlobal._convertStringToDataSet(__result);
                        }

                    }

                    string __getWHWhere = (this._form_condition._selectWarehouseAndLocation != null) ? this._form_condition._selectWarehouseAndLocation._wareHouseSelected() : "";
                    if (__getWHWhere.Length > 0)
                    {
                        return __ds.Tables[0].Select(_g.d.ic_resource._warehouse + " in (" + __getWHWhere + ") ");
                    }

                    return __ds.Tables[0].Select();
                }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = FontStyle.Bold;
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.สินค้า_รายงานเคลื่อนไหว:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 7, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 32, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, 7, SMLReport._report._cellType.String, 0, __fontStyle));
                    break;
                case SMLERPReportTool._reportEnum.สินค้า_รายงานบัญชีคุมพิเศษ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 7, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 32, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, 7, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._in, null, 18, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._out, null, 18, SMLReport._report._cellType.String, 0, __fontStyle));
                    if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._amount, null, 18, SMLReport._report._cellType.String, 0, __fontStyle));
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

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            int __width = 6;
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.สินค้า_รายงานเคลื่อนไหว:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._doc_date, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_date, 7, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._trans_name, _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_name, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._doc_no, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_no, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    if (this._mode == SMLERPReportTool._reportEnum.สินค้า_รายงานเคลื่อนไหว && this._form_condition._screen._getDataStr(_g.d.resource_report._show_remark).Equals("1"))
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._warehouse, _g.d.ic_resource._table + "." + _g.d.ic_resource._warehouse, 7, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._ic_unit_code, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, 7, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._qty_in, _g.d.ic_resource._table + "." + _g.d.ic_resource._qty_in, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._qty_out, _g.d.ic_resource._table + "." + _g.d.ic_resource._qty_out, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._balance_qty, _g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    break;
                case SMLERPReportTool._reportEnum.สินค้า_รายงานบัญชีคุมพิเศษ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._doc_date, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_date, 7, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._trans_name, _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_name, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._doc_no, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_no, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._warehouse, _g.d.ic_resource._table + "." + _g.d.ic_resource._warehouse, 7, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._location, _g.d.ic_resource._table + "." + _g.d.ic_resource._location, 7, SMLReport._report._cellType.String, 0, FontStyle.Regular));

                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._ic_unit_code, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, 7, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._qty_in, _g.d.ic_resource._table + "." + _g.d.ic_resource._qty_in, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._cost_in, _g.d.ic_resource._table + "." + _g.d.ic_resource._cost_in, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._amount_in, _g.d.ic_resource._table + "." + _g.d.ic_resource._amount_in, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    }
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._qty_out, _g.d.ic_resource._table + "." + _g.d.ic_resource._qty_out, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._cost_out, _g.d.ic_resource._table + "." + _g.d.ic_resource._cost_out, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._amount_out, _g.d.ic_resource._table + "." + _g.d.ic_resource._amount_out, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    }
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._balance_qty, _g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._average_cost, _g.d.ic_resource._table + "." + _g.d.ic_resource._average_cost, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._amount, _g.d.ic_resource._table + "." + _g.d.ic_resource._amount, __width, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    }
                    break;
            }
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitColumnValue(__columnList);
            return this._report._addLevel(this._levelNameDetail, levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            this._report._level = this._reportInitRoot(null, true, true);
            this._level2 = this._reportInitDetail(this._report._level, true, 2, true);
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
                    this._beginDate = MyLib._myGlobal._convertDateToQuery(this._form_condition._screen._getDataStr(_g.d.resource_report._from_date));
                    this._endDate = MyLib._myGlobal._convertDateToQuery(this._form_condition._screen._getDataStr(_g.d.resource_report._to_date));
                    Boolean __wareHouse = this._form_condition._screen._getDataStr(_g.d.resource_report._warehouse).ToString().Equals("1") ? true : false;
                    string __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code);

                    string __getWHWhere = (this._form_condition._selectWarehouseAndLocation != null) ? this._form_condition._selectWarehouseAndLocation._wareHouseSelected() : "";

                    if (__getWhere.Length > 0)
                    {
                        __getWhere = " and " + __getWhere;
                    }

                    if (__getWHWhere.Length > 0)
                    {
                        __getWhere += " and wh_code in (" + __getWHWhere + ") ";
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
                    __query.Append("'' as \"ic_resource.in\",");
                    __query.Append("'' as \"ic_resource.out\",");
                    __query.Append("'' as \"ic_resource.amount\"");
                    __query.Append(" from (");
                    __query.Append("select code as ic_code,name_1 as ic_name,unit_cost as ic_unit_code from ic_inventory where item_type<>5 and ");
                    __query.Append("code in (select item_code from ic_trans_detail where item_type<>5 and doc_date between \'" + this._beginDate + "\' and \'" + this._endDate + "\' and " + _g._icInfoFlag._allFlagQty + __getWhere + " )");
                    __query.Append(") as temp9");
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
                string __beginDate = this._form_condition._screen._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._form_condition._screen._getDataStr(_g.d.resource_report._to_date);
                this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                this._report._conditionText += _g.g._conditionGrid(this._form_condition._grid, this._report._conditionText);
                if (this._form_condition._selectWarehouseAndLocation != null)
                {
                    this._report._conditionText += this._form_condition._selectWarehouseAndLocation._header();
                }
                //
                this._report._build();
            }
        }
    }
}
