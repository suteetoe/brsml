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
    public partial class _report_ic_summary : UserControl
    {
        SMLReport._generate _report;
        String _screenName = "";
        DataTable _dataTableProduct;
        DataTable _dataTableDoc;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLERPReportTool._reportEnum _mode;
        string _transFlag;
        SMLERPReportTool._conditionScreen _condition;
        Boolean _displayDetail = false;

        /// <summary>
        /// โต๋สร้างมาใช้แทน report_ic ดูแล้วงง งง เลยทำใหม่เลย
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="screenName"></param>
        public _report_ic_summary(SMLERPReportTool._reportEnum mode, string screenName)
        {
            InitializeComponent();

            this._mode = mode;

            bool __landscape = false;
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.Item_status:
                    __landscape = true;
                    break;
            }

            this._transFlag = "44"; // (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้) ? "44" : "12";
            this._screenName = screenName;
            this._report = new SMLReport._generate(screenName, __landscape);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report.Disposed += new EventHandler(_report_Disposed);
            ////
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            //this._report__showCondition(screenName);
            this._showCondition();

        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Data)
            {
                switch (this._mode)
                {

                    case SMLERPReportTool._reportEnum.Item_by_serial:
                        {
                            int __serialStatusColumnNumber = sender._findColumnName(_g.d.ic_serial._table + "." + _g.d.ic_serial._status);
                            if (__serialStatusColumnNumber != -1 && __serialStatusColumnNumber == columnNumber)
                            {
                                //int __value = MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                sender._columnList[columnNumber]._dataStr = sender._columnList[columnNumber]._dataStr.ToString().Equals("1") ? "OUT" : "IN";  //_g.g._transFlagGlobal._transName(MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr));
                            }


                        }
                        break;
                    case SMLERPReportTool._reportEnum.Serial_number:
                        {
                            int __transFlagColumnNumber = sender._findColumnName(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag);
                            if (__transFlagColumnNumber != -1 && __transFlagColumnNumber == columnNumber)
                            {
                                //int __value = MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                sender._columnList[columnNumber]._dataStr = _g.g._transFlagGlobal._transName(MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr));
                            }

                        }
                        break;
                    /*case SMLERPReportTool._reportEnum.Item_status:
                        {
                            int __columnBalanceFirst = sender._findColumnName(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty_first);
                            if (__columnBalanceFirst != -1 && __columnBalanceFirst == columnNumber)
                            {
                                int __item_typeColumn = sender._findColumnName(_g.d.ic_resource._table + ".item_type");
                                string __itemType = sender._columnList[__item_typeColumn]._dataStr.ToString();
                                //int __value = MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                if (__itemType == "2" || __itemType == "4")
                                {
                                    sender._columnList[columnNumber]._dataStr = "";
                                }
                            }


                            int __columnBalanceEnd = sender._findColumnName(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty_first);
                            if (__columnBalanceEnd != -1 && __columnBalanceEnd == columnNumber)
                            {
                                int __item_typeColumn = sender._findColumnName(_g.d.ic_resource._table + ".item_type");
                                string __itemType = sender._columnList[__item_typeColumn]._dataStr.ToString();
                                //int __value = MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                if (__itemType == "2" || __itemType == "4")
                                {
                                    sender._columnList[columnNumber]._dataStr = "";
                                }
                            }

                        }
                        break;*/
                }
            }
            
        }

        string _convert_flag_to_description(string trans_flag)
        {
            string __result = "";
            switch (trans_flag)
            {
                case "12":
                    __result = MyLib._myGlobal._resource("บันทึกซื้อสินค้าบริการ");
                    break;
                case "44":
                    __result = MyLib._myGlobal._resource("บันทึกขายสินค้าบริการ");
                    break;
            }
            return __result;
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            try
            {
                if (level._levelName.Equals(this._levelNameRoot))
                {
                    return this._dataTableProduct.Select();
                }
                else
                    if (level._levelName.Equals(this._levelNameDetail))
                    {
                        switch (this._mode)
                        {
                            case SMLERPReportTool._reportEnum.Item_by_serial:
                                {
                                    string __where = _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + "=\'" + source[_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                                }
                            case SMLERPReportTool._reportEnum.Serial_number:
                                {
                                    string __where = _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + "=\'" + source[_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                                }
                            case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                                {
                                    string __where = _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + "=\'" + source[_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                                }
                            case SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                                {
                                    string __where = _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._ic_code + "=\'" + source[_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                                }
                            case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                                {
                                    string __where = _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + "=\'" + source[_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);
                                }
                            case SMLERPReportTool._reportEnum.Item_Giveaway:
                                {
                                    string __where = _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._permium_code + "=\'" + source[_g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._permium_code].ToString() + "\'";
                                    return (this._dataTableDoc == null) ? null : this._dataTableDoc.Select(__where);

                                }
                                break;
                        }
                    }

            }
            catch (Exception ex)
            {
            }

            return null;
        }

        void _report__query()
        {
            StringBuilder __query = new StringBuilder();
            StringBuilder __query_sub = new StringBuilder();
            StringBuilder __where = new StringBuilder();
            StringBuilder __whereDetail = new StringBuilder();
            string __orderDetail = "";
            string __orderMaster = "";

            string __begin_date = this._condition._screen._getDataStrQuery(_g.d.resource_report._from_date);
            string __end_date = this._condition._screen._getDataStrQuery(_g.d.resource_report._to_date);
            string __code_begin = this._condition._screen._getDataStrQuery(_g.d.resource_report._from_item_code).Replace("null", "");
            string __code_end = this._condition._screen._getDataStrQuery(_g.d.resource_report._to_item_code).Replace("null", "");

            // condition 
            switch (this._mode)
            {

                case SMLERPReportTool._reportEnum.Serial_number:
                    {
                        string __getWhere = this._condition._grid._createWhere(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number);
                        __where.Append(__getWhere.Replace(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number, _g.d.ic_trans_serial_number._serial_number));
                        __whereDetail.Append(__getWhere.Replace(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number, _g.d.ic_trans_serial_number._serial_number));

                        string __whereDate = "";
                        string __whereitemCode = "";

                        if (__begin_date.Length > 0 && __end_date.Length > 0)
                        {
                            __whereDate = _g.d.resource_report._date_import + " between " + __begin_date + " and " + __end_date;
                            __where.Append(((__where.Length > 0) ? " and " : "") + __whereDate);

                            __whereDetail.Append(((__whereDetail.Length > 0) ? " and " : "") + __whereDate);
                        }

                        if (__code_begin.Length > 0 && __code_end.Length > 0)
                        {
                            __whereitemCode = _g.d.ic_serial._ic_code + " between " + __code_begin + " and " + __code_end;
                            __where.Append(((__where.Length > 0) ? " and " : "") + __whereitemCode);

                            __whereDetail.Append(((__whereDetail.Length > 0) ? " and " : "") + __whereitemCode);
                        }


                    }
                    break;
            }


            // start query
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.Item_by_serial:
                    {
                        string __getICWhere = this._condition._grid._createWhere(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                        __query.Append("select " + _g.d.ic_inventory._code + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code + "\" " +
                            " , " + _g.d.ic_inventory._name_1 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name + "\"" +
                            " , " + _g.d.ic_inventory._group_main + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_group + "\" " +
                            " from " + _g.d.ic_inventory._table + " ");

                        __where.Append(_g.d.ic_inventory._ic_serial_no + "=1 ");
                        if (__getICWhere.Length > 0)
                        {
                            __where.Append(" and (" + __getICWhere + ") ");
                        }
                        __orderMaster = " order by " + _g.d.ic_inventory._code;

                        string __getICWhereSub = this._condition._grid._createWhere(_g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code);

                        __query_sub.Append("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_serial._ic_code + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + "\" "
                            , _g.d.ic_serial._serial_number + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + "\" "
                            , _g.d.ic_serial._wh_code + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._wh_code + "\" "
                            , _g.d.ic_serial._shelf_code + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._shelf_code + "\" "
                            , _g.d.ic_serial._status + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._status + "\" "
                            , _g.d.ic_serial._void_date + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._void_date + "\" ") +
                            " from " + _g.d.ic_serial._table);

                        if (__getICWhereSub.Length > 0)
                        {
                            __whereDetail.Append(__getICWhereSub);
                        }

                        __orderDetail = " order by " + _g.d.ic_serial._ic_code + "," + _g.d.ic_serial._serial_number;
                    }
                    break;
                case SMLERPReportTool._reportEnum.Item_Giveaway:
                    {
                        __query.Append("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_purchase_permium._permium_code + " as \"" + _g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._permium_code + "\" "
                            , _g.d.ic_purchase_permium._name_1 + " as \"" + _g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._name_1 + "\" ",
                            _g.d.ic_purchase_permium._important + " as \"" + _g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._important + "\" ",
                            _g.d.ic_purchase_permium._date_begin + " as \"" + _g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._date_begin + "\" ",
                            _g.d.ic_purchase_permium._date_end + " as \"" + _g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._date_end + "\" ") + " from " + _g.d.ic_purchase_permium._table);

                        __where.Append(_g.d.ic_purchase_permium._date_begin + ">= " + __begin_date + " and " + _g.d.ic_purchase_permium._date_end + "<=" + __end_date + "");

                        __orderMaster = " order by " + _g.d.ic_purchase_permium._permium_code;

                        __query_sub.Append("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_purchase_permium_condition._permium_code + " as \"" + _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._permium_code + "\" ",
                            _g.d.ic_purchase_permium_condition._ic_code + " as \"" + _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._ic_code + "\" ",
                            "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._ic_code + ") " + " as \"" + _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._ic_name + "\" ",
                            _g.d.ic_purchase_permium_condition._unit_code + " as \"" + _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._unit_code + "\" ",
                            _g.d.ic_purchase_permium_condition._qty + " as \"" + _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._qty + "\" ") + " from " + _g.d.ic_purchase_permium_condition._table);

                        __whereDetail.Append(" exists (select " + _g.d.ic_purchase_permium._permium_code + " from " + _g.d.ic_purchase_permium._table + " where " + _g.d.ic_purchase_permium._date_begin + ">= " + __begin_date + " and " + _g.d.ic_purchase_permium._date_end + "<=" + __end_date + " and " + _g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._permium_code + "=" + _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._permium_code + ")");
                        __orderDetail = " order by " + _g.d.ic_purchase_permium_condition._permium_code + "," + _g.d.ic_purchase_permium_condition._ic_code;
                    }
                    break;
                case SMLERPReportTool._reportEnum.Item_status:
                    {
                        __query.Append("select " + _g.d.ic_resource._ic_code + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._ic_name + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._ic_unit_code + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code + "\" ");
                        __query.Append(" , case when coalesce((select item_type from ic_inventory where ic_inventory.code= movement.ic_code), 0) in (1,3) then 0 else " + _g.d.ic_resource._balance_qty_first + " end as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty_first + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_12 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_12 + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_54 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_54 + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_58 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_58 + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_60 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_60 + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_66 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_66 + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_48 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_48 + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_44 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_44 + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_56 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_56 + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_16 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_16 + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._trans_flag_68 + " as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_68 + "\" ");
                        __query.Append(" , case when coalesce((select item_type from ic_inventory where ic_inventory.code= movement.ic_code), 0) in (1,3) then 0 else " + _g.d.ic_resource._balance_qty + " end as \"" + _g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty + "\" ");
                        __query.Append(" , (select " + _g.d.ic_inventory._item_type + " from ic_inventory where ic_inventory.code = movement.ic_code) as \"" + _g.d.ic_resource._table + ".item_type\" ");
                        __query.Append(" from (");

                        SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                        string __getQuery = __process._stkStockMovementSumQuery(_g.g._productCostType.ปรกติ, 0, this._condition._grid, "", "", this._condition._screen._getDataDate(_g.d.resource_report._from_date), this._condition._screen._getDataDate(_g.d.resource_report._to_date), false);
                        __query.Append(__getQuery);
                        __query.Append(" ) as movement");

                    }
                    break;
                case SMLERPReportTool._reportEnum.Serial_number:
                    {
                        string __serialFlagIn = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา).ToString() + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป) + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString();

                        string __serialExceptFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString() + ","
                            + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้่า_ตรวจสอบสินค้า_serial).ToString();

                        __query.Append(" select " + _g.d.ic_serial._serial_number + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + "\" ");
                        __query.Append(" , " + _g.d.ic_serial._ic_code + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._item_name + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_resource._item_name + "\" ");
                        __query.Append(" , " + _g.d.ic_serial._description + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._description + "\" ");
                        __query.Append(" , " + _g.d.ic_serial._status + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._status + "\" ");
                        __query.Append(" , " + _g.d.resource_report._date_import + " as \"" + _g.d.ic_serial._table + "." + _g.d.resource_report._date_import + "\" ");
                        __query.Append(" , " + _g.d.ic_resource._ic_cost + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_resource._ic_cost + "\" ");
                        __query.Append(" from (");
                        __query.Append("select " + _g.d.ic_serial._serial_number + " , " + _g.d.ic_serial._ic_code);
                        __query.Append(" , (select " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + ") as " + _g.d.ic_resource._item_name);
                        __query.Append(" , " + _g.d.ic_serial._description);
                        __query.Append(" , case  when " + _g.d.ic_serial._status + "= 0 then '" + MyLib._myGlobal._resource("ในสต๊อค") + "' else '" + MyLib._myGlobal._resource("ในสต๊อค") + "' end as " + _g.d.ic_serial._status);
                        __query.Append(" , (select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + " = " + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + " and " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag + " in (" + __serialFlagIn + ") order by " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ) as " + _g.d.resource_report._date_import);
                        __query.Append(", (select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._price + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + " = " + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + " and " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag + " in (" + __serialFlagIn + ") order by " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ) as " + _g.d.ic_resource._ic_cost + "");
                        __query.Append(" from " + _g.d.ic_serial._table);
                        __query.Append(" ) as temp1 ");

                        __orderMaster = " order by " + _g.d.ic_serial._ic_code + "," + _g.d.ic_serial._serial_number;

                        __query_sub.Append("select " + _g.d.ic_trans_serial_number._doc_date + " as  \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + "\"");
                        __query_sub.Append(" , " + _g.d.ic_trans_serial_number._serial_number + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + "\"");
                        __query_sub.Append(" , " + _g.d.ic_trans_serial_number._doc_no + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no + "\"");
                        __query_sub.Append(" , " + _g.d.ic_trans_serial_number._trans_flag + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag + "\"");
                        __query_sub.Append(" , " + _g.d.ic_trans_serial_number._wh_code + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._wh_code + "\"");
                        __query_sub.Append(" ," + _g.d.ic_trans_serial_number._shelf_code + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._shelf_code + "\"");
                        __query_sub.Append(" ," + _g.d.resource_report._date_import + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.resource_report._date_import + "\"");
                        __query_sub.Append(" ," + _g.d.ic_resource._ar_detail + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_resource._ar_detail + "\"");
                        __query_sub.Append("  from ( select * ");
                        __query_sub.Append(" , case when " + _g.d.ic_trans_serial_number._trans_flag + " in (44) then ( select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where  " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans_serial_number._cust_code + ") else '' end as " + _g.d.ic_resource._ar_detail);
                        __query_sub.Append(" , (select " + MyLib._myGlobal._getTopAndLimitOneRecord()[0].ToString() + " r." + _g.d.ic_trans_serial_number._doc_date + " from " + _g.d.ic_trans_serial_number._table + " as r where r." + _g.d.ic_trans_serial_number._serial_number + " = " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + " and r." + _g.d.ic_trans_serial_number._trans_flag + " in (" + __serialFlagIn + ") order by " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + " " + MyLib._myGlobal._getTopAndLimitOneRecord()[1].ToString() + " ) as " + _g.d.resource_report._date_import);
                        __query_sub.Append(" from " + _g.d.ic_trans_serial_number._table);
                        __query_sub.Append(" where " + _g.d.ic_trans_serial_number._trans_flag + " not in (" + __serialExceptFlag + ")");
                        __query_sub.Append(" ) as temp1 ");

                        __orderDetail = " order by " + _g.d.ic_trans_serial_number._serial_number + "," + _g.d.ic_trans_serial_number._doc_date;
                    }
                    break;
                case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                    {
                        Boolean __allItem = this._condition._screen._getDataStr(_g.d.resource_report._displpay_item_balance_equal_zero).ToString().Equals("1") ? true : false;
                        DateTime __date = DateTime.Now.AddDays(1);
                        Boolean __showBarcode = true;

                        __where = new StringBuilder();
                        __whereDetail = new StringBuilder();

                        SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                        __query.Append("select ");
                        __query.Append("ic_code as \"ic_resource.ic_code\",");
                        __query.Append(" ( select " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " =  ic_code ) as \"ic_resource.ic_name\",");
                        __query.Append("ic_unit_code as \"ic_resource.ic_unit_code\",");
                        __query.Append("balance_qty as \"ic_resource.balance_qty\",");
                        __query.Append("average_cost_end as \"ic_resource.average_cost_end\",");
                        __query.Append("balance_amount as \"ic_resource.balance_amount\"");
                        __query.Append(" from (");
                        __query.Append(__process._stkStockInfoAndBalanceQuery(_g.g._productCostType.ปรกติ, (MyLib._myGrid)this._condition._grid, "", "", __date, __date, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามสินค้า, this._condition._selectWarehouseAndLocation._wareHouseSelected(), this._condition._selectWarehouseAndLocation._locationSelected(), (__allItem == true) ? false : true, __showBarcode, "", true));
                        __query.Append(") as temp9 where ic_code<>''");
                        __query.Append(" order by ic_code ");
                        //__query.Append(this._form_condition._extra._getOrderBy());
                        //
                        string __getWhere = this._condition._grid._createWhere(_g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code);
                        __query_sub.Append("select ");
                        __query_sub.Append("" + _g.d.ic_serial._ic_code + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + "\",");
                        __query_sub.Append("" + _g.d.ic_serial._serial_number + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + "\",");
                        __query_sub.Append("" + _g.d.ic_serial._wh_code + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._wh_code + "\",");
                        __query_sub.Append("" + _g.d.ic_serial._shelf_code + " as \"" + _g.d.ic_serial._table + "." + _g.d.ic_serial._shelf_code + "\" ");

                        __query_sub.Append(" from " + _g.d.ic_serial._table + " where " + _g.d.ic_serial._status + "=0 ");
                        if (__getWhere.Length > 0)
                        {
                            __getWhere.Replace(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code, _g.d.ic_serial._ic_code);
                            __query_sub.Append(" and (" + __getWhere + ")");
                        }
                        __query_sub.Append(" order by " + _g.d.ic_serial._ic_code + "," + _g.d.ic_serial._serial_number);
                    }
                    break;
                case SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ:
                    _query_stock_count_diff();
                    return;
                case SMLERPReportTool._reportEnum.Stock_no_count_no_balance:
                    {
                        DateTime _endDate = MyLib._myGlobal._convertDate(this._condition._screen._getDataStr(_g.d.resource_report._to_date));
                        string __docNoPack = _getDocNo_stock_check(true);
                        string __whPack = this._whPack();

                        string __queryNotExists = "select * from ic_trans_detail as x where x.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString() + " and x.item_code=ic_trans_detail.item_code and x.wh_code=ic_trans_detail.wh_code and x.shelf_code=ic_trans_detail.shelf_code and x.doc_no in (" + __docNoPack + ")";

                        string __querySub = "select " +
                            _g.d.ic_trans_detail._item_code + "," +
                            "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name + "," +
                            _g.d.ic_trans_detail._wh_code + "," +
                            _g.d.ic_trans_detail._shelf_code + "," +
                            "sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end) * (" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) as " + _g.d.ic_trans_detail._qty +
                            " from " + _g.d.ic_trans_detail._table +
                            " where " +
                            _g._icInfoFlag._allFlagQty +
                            ((__whPack.Length > 0) ? " and " + _g.d.ic_trans_detail._wh_code + " in (" + this._whPack() + ") " : "") +
                            "and (" + _g.d.ic_trans_detail._doc_date_calc + "<=\'" + MyLib._myGlobal._convertDateToQuery(_endDate.AddDays(-1)) + "\') and " +
                            " not exists ( " + __queryNotExists + ") group by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + "";

                        string __queryStr = "select " +
                            MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans_detail._item_code + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "\"",
                            _g.d.ic_trans_detail._item_name + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name + "\"",
                            _g.d.ic_trans_detail._wh_code + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + "\"",
                            _g.d.ic_trans_detail._shelf_code + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + "\"",
                            _g.d.ic_trans_detail._qty + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty + "\"",
                            "(select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._item_code + ") " + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + "\"",
                            "(select " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ") as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_type + "\"") +
                            " from ( " + __querySub + ") as temp1  where " + _g.d.ic_trans_detail._qty + "<>0 order by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code;

                        __query.Append(__queryStr);
                    }
                    break;
                case SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                    {
                        // pack doc_no 
                        String __docNoWhere = _getDocNo_stock_check(true);
                        __query.Append("select distinct " + MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans_detail._item_code + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "\"",
                            _g.d.ic_trans_detail._item_name + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name + "\"",
                            _g.d.ic_trans_detail._unit_code + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + "\"",
                            "sum(" + _g.d.ic_trans_detail._qty + ") as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._check_stock_1 + "\""
                            )
                            + " from ic_trans_detail ");
                        __query.Append(" where " + _g.d.ic_trans_detail._doc_no + " in (" + __docNoWhere + ") and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString() + " and " + _g.d.ic_trans_detail._is_serial_number + " = 1 and " + _g.d.ic_trans_detail._last_status + "=0");
                        __query.Append(" group by item_code,item_name,unit_code");

                        __query_sub.Append("select " + MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans_serial_number._ic_code + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._ic_code + "\"",
                            _g.d.ic_trans_serial_number._serial_number + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + "\"",
                            _g.d.ic_trans_serial_number._doc_no + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no + "\"",
                            _g.d.ic_trans_serial_number._doc_date + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + "\"",
                            _g.d.ic_trans_serial_number._wh_code + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._wh_code + "\"",
                            _g.d.ic_trans_serial_number._shelf_code + " as \"" + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._shelf_code + "\""
                            ) + " from " + _g.d.ic_trans_serial_number._table);
                        __whereDetail.Append(_g.d.ic_trans_serial_number._doc_no + " in (" + __docNoWhere + ") and " + _g.d.ic_trans_serial_number._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString() + " and " + _g.d.ic_trans_serial_number._last_status + "=0");

                        __orderDetail = " order by " + _g.d.ic_trans_serial_number._ic_code + "," + _g.d.ic_trans_serial_number._serial_number;

                    }
                    break;
                case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:

                    string __ar_type = this._condition._screen._getDataStr(_g.d.resource_report._ar_type);

                    __query.Append(" select distinct " + MyLib._myGlobal._fieldAndComma(
                        _g.d.ic_trans_detail._cust_code + " as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + "\"",
                        "( select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + ") as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans._cust_name + "\"",
                        "( select " + _g.d.ar_type._name_1 + " from " + _g.d.ar_type._table + " where " + _g.d.ar_type._table + "." + _g.d.ar_type._code + "= (select " + _g.d.ar_customer._ar_type + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + ")) as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ar_customer._ar_type + "\""
                        ));

                    __query.Append(" from " + _g.d.ic_trans_detail._table);
                    __query.Append(" where " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " and " + _g.d.ic_trans_detail._doc_date + " between " + __begin_date + "  and " + __end_date);

                    if (__ar_type.Length > 0)
                    {
                        __query.Append(" and (select " + _g.d.ar_customer._ar_type + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + ") = \'" + __ar_type + "\' ");
                    }
                    __orderMaster = " order by cust_code ";


                    StringBuilder __queryDetailTemp1 = new StringBuilder();
                    __queryDetailTemp1.Append(" select ");
                    //__queryDetailTemp1.Append(" (select name_1 from ar_type where ar_type.code = (select ar_type from ar_customer where ar_customer.code = ic_trans_detail.cust_code)) as ar_type_name ");
                    //__queryDetailTemp1.Append(" ,cust_code, (select name_1 from ar_customer where ar_customer.code = ic_trans_detail.cust_code) as cust_name ");
                    __queryDetailTemp1.Append(" cust_code,item_code ");
                    __queryDetailTemp1.Append(" , (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code) as item_name ");
                    __queryDetailTemp1.Append(" , (select unit_standard from ic_inventory where ic_inventory.code = ic_trans_detail.item_code) as unit_code ");
                    __queryDetailTemp1.Append(" , sum((qty * ( stand_value/divide_value))) as " + _g.d.ic_resource._qty_sale);
                    __queryDetailTemp1.Append(" , sum(sum_amount_exclude_vat) as " + _g.d.ic_trans_detail._sum_amount_exclude_vat);
                    __queryDetailTemp1.Append(" ,sum(sum_of_cost) as " + _g.d.ic_trans_detail._sum_of_cost);
                    __queryDetailTemp1.Append(" , (sum(sum_amount_exclude_vat) - sum(sum_of_cost)) as " + _g.d.ic_trans_detail._profit);
                    __queryDetailTemp1.Append(" from ic_trans_detail ");
                    __queryDetailTemp1.Append(" where " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " and " + _g.d.ic_trans_detail._doc_date + " between " + __begin_date + "  and " + __end_date);
                    if (__ar_type.Length > 0)
                    {
                        __queryDetailTemp1.Append(" and (select " + _g.d.ar_customer._ar_type + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + ") = \'" + __ar_type + "\' ");
                    }

                    //__queryDetailTemp1.Append(" and doc_date between '2012-01-01' and '2012-09-30' ");
                    __queryDetailTemp1.Append(" group by item_code, cust_code ");

                    __query_sub.Append(" select " + MyLib._myGlobal._fieldAndComma(
                        _g.d.ic_trans_detail._cust_code + " as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + "\"",
                        _g.d.ic_trans_detail._item_code + " as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "\"",
                        _g.d.ic_trans_detail._item_name + " as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name + "\"",
                        _g.d.ic_trans_detail._unit_code + " as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + "\"",

                        _g.d.ic_resource._qty_sale + " as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_resource._qty_sale + "\"",
                        _g.d.ic_trans_detail._sum_amount_exclude_vat + " as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount_exclude_vat + "\"",
                        _g.d.ic_trans_detail._sum_of_cost + " as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_of_cost + "\"",
                        _g.d.ic_trans_detail._profit + " as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._profit + "\"",
                        " (case when " + _g.d.ic_trans_detail._sum_amount_exclude_vat + " = 0 then 0 else (" + _g.d.ic_trans_detail._profit + "*100 ) / " + _g.d.ic_trans_detail._sum_amount_exclude_vat + " end ) as " + "\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_resource._profit_lost_persent + "\""
                        ));
                    __query_sub.Append(" from (" + __queryDetailTemp1.ToString() + " ) as temp1 ");
                    __orderDetail = " order by cust_code ";
                    break;
            }

            __query.Append(((__where.Length > 0) ? " where " + __where : "") + __orderMaster);

            __query_sub.Append(((__whereDetail.Length > 0) ? " where " + __whereDetail : "") + __orderDetail);

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __queryHead = __query.ToString();
            string __queryDetail = __query_sub.ToString();

            try
            {
                this._dataTableProduct = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryHead).Tables[0];
                this._dataTableDoc = (_displayDetail) ? __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetail).Tables[0] : null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region Processs Stock Diff

        private string _whPack()
        {
            StringBuilder __whPack = new StringBuilder();
            for (int __row = 0; __row < this._condition._selectWarehouseAndLocation._whGrid._rowData.Count; __row++)
            {
                if (this._condition._selectWarehouseAndLocation._whGrid._cellGet(__row, this._condition._fieldCheck).ToString() == "1")
                {
                    string __whCode = this._condition._selectWarehouseAndLocation._whGrid._cellGet(__row, _g.d.ic_warehouse._code).ToString();
                    if (__whPack.Length > 0)
                    {
                        __whPack.Append(",");
                    }
                    __whPack.Append("\'" + __whCode + "\'");
                }
            }
            return __whPack.ToString();
        }


        string _getDocNo_stock_check(bool haveSingle)
        {

            StringBuilder __docPack = new StringBuilder();
            for (int __row = 0; __row < this._condition._grid._rowData.Count; __row++)
            {
                if (this._condition._grid._cellGet(__row, this._condition._fieldCheck).ToString() == "1")
                {
                    string __docNo = this._condition._grid._cellGet(__row, _g.d.ic_trans._doc_no).ToString();
                    if (__docPack.Length > 0)
                    {
                        __docPack.Append(",");
                    }
                    __docPack.Append(((haveSingle) ? "\'" : "") + __docNo + ((haveSingle) ? "\'" : ""));
                }
            }

            return __docPack.ToString();
        }

        //DataTable __detailTable;
        /// <summary>
        /// process แล้วเก็บรวมกันไว้ใน datatable
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="count"></param>
        /// <param name="docNo"></param>
        void _process_stock_diff(int mode, int count, string docNo)
        {
            //DataTable __rootTable;

            string __fieldBalance = "qty_balance";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();

            string __docNoWhere = (mode == 0) ? " in (" + docNo + ")" : "=\'" + docNo + "\'";
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select distinct " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + __docNoWhere));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select distinct " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + __docNoWhere));


            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select  (select " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._average_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._average_cost + ",(select " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_type + "," + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code, "sum(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ")) as " + _g.d.ic_trans_detail._qty) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + __docNoWhere + " group by " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code)));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time) + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + __docNoWhere));
            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(
            //    _g.d.ic_trans._doc_no + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "\"",
            //    _g.d.ic_trans._doc_date + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date + "\"",
            //    _g.d.ic_trans._doc_ref + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref + "\"",
            //    _g.d.ic_trans._doc_ref_date + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date + "\"",
            //    _g.d.ic_trans._total_amount + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount + "\"") +
            //    " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + __docNoWhere));
            __myquery.Append("</node>");

            string __debugQuery = __myquery.ToString();

            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

            if (__getData.Count > 0)
            {
                DataTable __source1 = ((DataSet)__getData[0]).Tables[0]; // distince itemcode
                DataTable __source2 = ((DataSet)__getData[1]).Tables[0]; // distinct wh shelf
                DataTable __source3 = ((DataSet)__getData[2]).Tables[0]; // รายละเอียดสินค้า
                DataTable __source4 = ((DataSet)__getData[3]).Tables[0]; // doc_date doc_time
                //DataTable __source5 = ((DataSet)__getData[4]).Tables[0]; // doc_no //for head report

                // master doc
                // addtoDatatableProduct
                //__rootTable = __source5;



                // detail doc หาผลต่างแต่ละรายการ

                // หาเวลาเริ่มต้นเอกสาร
                if (__source1.Rows.Count > 0)
                {
                    DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__source4.Rows[0][_g.d.ic_trans._doc_date].ToString());
                    DateTime __docDateCalc = __docDate.AddDays(-1);
                    string __docTime = __source4.Rows[0][_g.d.ic_trans._doc_time].ToString();
                    string __docTimeCalc = "";
                    if (mode == 0)
                    {
                        __docTimeCalc = "24:58";
                        __docTime = "24:59";
                    }
                    else
                    {
                        string[] __docTimeSplit = __docTime.Split(':');
                        int __hour = (int)MyLib._myGlobal._decimalPhase(__docTimeSplit[0].ToString());
                        int __minute = (int)MyLib._myGlobal._decimalPhase(__docTimeSplit[1].ToString());
                        if (--__minute < 0)
                        {
                            __minute = 59;
                            __hour--;
                        }
                        __docTimeCalc = string.Format("{0:00}:{1:00}", __hour, __minute);
                    }

                    // หาผลต่าง ยอดคงเหลือ และการตรวจนับ
                    StringBuilder __itemCode = new StringBuilder();
                    StringBuilder __whCode = new StringBuilder();
                    StringBuilder __locationCode = new StringBuilder();

                    for (int __loop = 0; __loop < __source1.Rows.Count; __loop++)
                    {
                        if (__loop != 0)
                        {
                            __itemCode.Append(",");
                        }
                        __itemCode.Append("\'" + __source1.Rows[__loop][_g.d.ic_trans_detail._item_code].ToString() + "\'");
                    }
                    for (int __loop = 0; __loop < __source2.Rows.Count; __loop++)
                    {
                        if (__loop != 0)
                        {
                            __whCode.Append(",");
                            __locationCode.Append(",");
                        }
                        __whCode.Append("\'" + __source2.Rows[__loop][_g.d.ic_trans_detail._wh_code].ToString() + "\'");
                        __locationCode.Append("\'" + __source2.Rows[__loop][_g.d.ic_trans_detail._shelf_code].ToString() + "\'");
                    }
                    // คำนวณยอดคงเหลือแยกตามที่เก็บ เฉพาะสินค้าในเอกสาร
                    //String __queryStockBalance = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + ",sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end) * (" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) as " + __fieldBalance + " from " + _g.d.ic_trans_detail._table + " where " + _g._icInfoFlag._allFlag + " and " + _g.d.ic_trans_detail._item_code + " in (" + __itemCode.ToString() + ") and " + _g.d.ic_trans_detail._wh_code + " in (" + __whCode.ToString() + ") and " + _g.d.ic_trans_detail._shelf_code + " in (" + __locationCode.ToString() + ") and (" + _g.d.ic_trans_detail._doc_date_calc + "<=\'" + MyLib._myGlobal._convertDateToQuery(__docDateCalc) + "\' or (" + _g.d.ic_trans_detail._doc_date_calc + "=\'" + MyLib._myGlobal._convertDateToQuery(__docDateCalc) + "\' and " + _g.d.ic_trans_detail._doc_time_calc + "<=\'" + __docTimeCalc + "\')) group by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + " order by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code;  q ผิด โต๊ แก้ไขตรง  (" + _g.d.ic_trans_detail._doc_date_calc + "=\'" + MyLib._myGlobal._convertDateToQuery(__docDateCalc) + "\' and " + _g.d.ic_trans_detail._doc_time_calc + "<=\'" + __docTimeCalc + "\'))  ต้องเป็นวันที่ เช็ค stock
                    String __queryStockBalance = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + ",sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end) * (" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) as " + __fieldBalance + " from " + _g.d.ic_trans_detail._table + " where " + _g._icInfoFlag._allFlagQty + " and " + _g.d.ic_trans_detail._last_status + " =0 and " + _g.d.ic_trans_detail._item_code + " in (" + __itemCode.ToString() + ") and " + _g.d.ic_trans_detail._wh_code + " in (" + __whCode.ToString() + ") and " + _g.d.ic_trans_detail._shelf_code + " in (" + __locationCode.ToString() + ") and (" + _g.d.ic_trans_detail._doc_date_calc + "<=\'" + MyLib._myGlobal._convertDateToQuery(__docDateCalc) + "\' or (" + _g.d.ic_trans_detail._doc_date_calc + "=\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\' and " + _g.d.ic_trans_detail._doc_time_calc + "<=\'" + __docTimeCalc + "\')) group by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + " order by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code;

                    __myquery = new StringBuilder();
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStockBalance));
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory._code, _g.d.ic_inventory._unit_cost, _g.d.ic_inventory._name_1) + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " in (" + __itemCode.ToString() + ") order by " + _g.d.ic_inventory._code));
                    __myquery.Append("</node>");
                    string __queryStr = __myquery.ToString();
                    ArrayList __getData2 = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
                    DataTable __stockBalance = ((DataSet)__getData2[0]).Tables[0]; // ยอดคงเหลือ 
                    DataTable __item = ((DataSet)__getData2[1]).Tables[0]; // รายละเอียดสินค้า
                    //
                    List<_compareBalance> __dataList = new List<_compareBalance>();

                    for (int __loop = 0; __loop < __source3.Rows.Count; __loop++)
                    {
                        _compareBalance __data = new _compareBalance();
                        __data._itemCode = __source3.Rows[__loop][_g.d.ic_trans_detail._item_code].ToString();
                        __data._whCode = __source3.Rows[__loop][_g.d.ic_trans_detail._wh_code].ToString();
                        __data._locationCode = __source3.Rows[__loop][_g.d.ic_trans_detail._shelf_code].ToString();
                        __data._qtyNew = MyLib._myGlobal._decimalPhase(__source3.Rows[__loop][_g.d.ic_trans_detail._qty].ToString());
                        __data._average_cost = MyLib._myGlobal._decimalPhase(__source3.Rows[__loop][_g.d.ic_trans_detail._average_cost].ToString());
                        // toe item_type
                        __data._item_type = (int)MyLib._myGlobal._decimalPhase(__source3.Rows[__loop][_g.d.ic_trans_detail._item_type].ToString());
                        if (__stockBalance.Rows.Count > 0)
                        {
                            DataRow[] __find = __stockBalance.Select(_g.d.ic_trans_detail._item_code + "=\'" + __data._itemCode + "\' and " + _g.d.ic_trans_detail._wh_code + "=\'" + __data._whCode + "\' and " + _g.d.ic_trans_detail._shelf_code + "=\'" + __data._locationCode + "\'");
                            if (__find.Length > 0)
                            {
                                __data._qtyOld = MyLib._myGlobal._decimalPhase(__find[0][__fieldBalance].ToString());
                                __data._qtyDiff = __data._qtyNew - __data._qtyOld;
                            }
                            else
                            {
                                // กรณีไม่พบยอดคงเหลือ แต่มียอดตรวจนับ
                                __data._qtyOld = 0M;
                                __data._qtyDiff = __data._qtyNew;
                            }
                        }
                        else
                        {
                            __data._qtyOld = 0M;
                            __data._qtyDiff = __data._qtyNew - __data._qtyOld;
                        }

                        __dataList.Add(__data);
                    }

                    // insert detail datatable
                    //this._dataTableDoc.Rows.Add
                    if (this._dataTableProduct == null)
                    {
                        this._dataTableProduct = new DataTable("Result");
                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, typeof(String));
                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, typeof(String));
                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, typeof(String));
                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, typeof(String));
                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, typeof(String));
                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._total_qty, typeof(Decimal));
                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._check_stock_1, typeof(Decimal));
                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._check_stock_2, typeof(Decimal));

                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, typeof(Decimal));
                        this._dataTableProduct.Columns.Add(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, typeof(Decimal));
                        // this._dataTableDoc.Columns.Add("\"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark + "\"", typeof(Decimal));
                        // this._dataTableDoc.Columns.Add(_g.d.ap_ar_resource._amount, typeof(Decimal));
                        // this._dataTableDoc.Columns.Add(_g.d.ap_ar_resource._ar_balance, typeof(Decimal));
                        // toe
                        // this._dataTableDoc.Columns.Add(_g.d.ap_ar_resource._ref_doc_no, typeof(String));
                        // this._dataTableDoc.Columns.Add(_g.d.ap_ar_resource._ref_doc_date, typeof(String));
                    }

                    // for ใส่โลด
                    string __docRefNo = (mode == 0) ? "" : docNo;
                    for (int __loop = 0; __loop < __dataList.Count; __loop++)
                    {
                        DataRow[] __itemSelect = __item.Select(_g.d.ic_inventory._code + "=\'" + __dataList[__loop]._itemCode + "\'");
                        string __unitCost = "";
                        string __itemName = "";
                        if (__itemSelect.Length > 0)
                        {
                            __unitCost = __itemSelect[0][_g.d.ic_inventory._unit_cost].ToString();
                            __itemName = __itemSelect[0][_g.d.ic_inventory._name_1].ToString();
                        }

                        //"insert into " + _g.d.ic_trans_detail._table + "values (" + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", "\'" + __docTime + "\'", "\'" + __newDocNo + "\'", "\'" + __docRefNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", "\'" + __docTime + "\'", __transFlag, "3", "0", "\'" + __dataList[__loop]._itemCode + "\'", "\'" + __unitCost + "\'", "\'" + MyLib._myGlobal._convertStrToQuery(__itemName) + "\'", __loop.ToString(), __dataList[__loop]._qtyDiff.ToString(), "1", "1", "1", "\'" + __dataList[__loop]._whCode + "\'", "\'" + __dataList[__loop]._locationCode + "\'", "1", __dataList[__loop]._item_type.ToString()) + ")")); // toe เพิ่ม item_type = 0 
                        // insert to datatable
                        this._dataTableProduct.Rows.Add(
                            __dataList[__loop]._itemCode,
                            __itemName,
                            __dataList[__loop]._whCode,
                            __dataList[__loop]._locationCode,
                            __unitCost,
                            __dataList[__loop]._qtyOld,
                            __dataList[__loop]._qtyNew,
                            __dataList[__loop]._qtyDiff,
                            __dataList[__loop]._average_cost,
                            __dataList[__loop]._average_amount);


                        //__itemList.Add(__dataList[__loop]._itemCode);
                    }

                }
            }
            else
            {
                MessageBox.Show("ผิดพลาด คำสั่ง Query");
            }

        }

        /// <summary>
        /// มีการประมวลผล แบบพิเศษ ต้องทำแยก
        /// </summary>
        private void _query_stock_count_diff()
        {
            // แบบพิเศษ
            this._dataTableProduct = null;

            // get doc select 
            string __docNoWher = "";
            if (_g.g._companyProfile._count_stock_sum)
            {
                // แบบรวมทุกบิล กำหนดเวลาให้เป็น 24:00
                __docNoWher = _getDocNo_stock_check(true);
                _process_stock_diff(0, 0, __docNoWher);
            }
            else
            {
                for (int __row = 0; __row < this._condition._grid._rowData.Count; __row++)
                {
                    if (this._condition._grid._cellGet(__row, this._condition._fieldCheck).ToString() == "1")
                    {
                        string __docNo = this._condition._grid._cellGet(__row, _g.d.ic_trans._doc_no).ToString();
                        _process_stock_diff(1, __row, __docNo);
                        //this._process(1, __row, __docNo);
                        // ค้างไว้ก่อน
                    }
                }
            }

        }

        #endregion

        void _report__init()
        {

            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.Serial_number:
                case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                case SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ:
                case SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:

                case SMLERPReportTool._reportEnum.Item_Giveaway: // ของแถมซื้อ
                case SMLERPReportTool._reportEnum.Item_by_serial :
                    this._displayDetail = true;
                    break;

            }

            //this._displayDetail = this._con_so._condition_so_search3._getDataStr(_g.d.resource_report._display_detail).ToString().Equals("1") ? true : false;

            this._report._level = this._reportInitRoot(null, true, true);
            if (this._displayDetail == true)
            {
                SMLReport._generateLevelClass __level2 = this._reportInitDetail(this._report._level, true, 2, true);
            }
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDetailColumn(__columnList, spaceFirstColumnWidth);
            return this._report._addLevel(this._levelNameDetail, levelParent, __columnList, sumTotal, autoWidth);
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = (this._displayDetail) ? FontStyle.Bold : FontStyle.Regular;

            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.Item_by_serial: // รายงานสินค้าแบบมี serial
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 45, SMLReport._report._cellType.String, 0, __fontStyle));
                    break;
                case SMLERPReportTool._reportEnum.Item_Giveaway:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._permium_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._name_1, null, 45, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._important, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._date_begin, null, 8, SMLReport._report._cellType.DateTime, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._date_end, null, 8, SMLReport._report._cellType.DateTime, 0));

                    break;
                case SMLERPReportTool._reportEnum.Item_status:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 45, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_54, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty_first, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_12, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_60, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_58, null, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_48, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_66, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._amount_sale_debit, _g.d.ic_trans._table + "." + _g.d.ic_trans._so_addition_debt, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_44, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_56, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_16, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag_68, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._amount_sale_debit, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._so_addition_debt, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._balance_end, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));

                        // toe ซ่อน
                        // columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + ".item_type" , null, 0, SMLReport._report._cellType.String, 0, __fontStyle));

                    }
                    break;
                case SMLERPReportTool._reportEnum.Serial_number:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_resource._item_name, _g.d.ic_resource._table + "." + _g.d.ic_resource._item_name, 30, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._status, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.resource_report._date_import, _g.d.resource_report._table + "." + _g.d.resource_report._date_import, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_resource._ic_cost, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_cost, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    }
                    break;
                case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, null, 45, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 26, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._total_qty, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._check_stock_1, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._check_stock_2, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));

                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));


                    break;
                case SMLERPReportTool._reportEnum.Stock_no_count_no_balance:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    break;
                case SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._check_stock_1, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
                    break;
                case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_code, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans._cust_name, _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name, 40, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ar_customer._ar_type, _g.d.resource_report._table + "." + _g.d.resource_report._ar_type, 40, SMLReport._report._cellType.String, 0, __fontStyle));
                    break;

            }

            /*
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sale_name, null, 17, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, null, 17, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_before_vat, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._total_tax_at_pay, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._unit_code, false), null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._qty, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass("", "", 6, SMLReport._report._cellType.String, 0, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._discount_amount, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            //columnList.Add(new SMLReport._generateColumnListClass(this._fieldName(_g.d.ic_trans_detail._sum_amount, false), null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
            */
        }

        private void _reportInitDetailColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.Item_by_serial:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number, null, 40, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._wh_code, null, 30, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._shelf_code, null, 30, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._status, null, 30, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._void_date, null, 30, SMLReport._report._cellType.DateTime, 0));
                    break;
                case SMLERPReportTool._reportEnum.Item_Giveaway:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._ic_code, null, 14, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._ic_name, null, 32, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._unit_code, null, 6, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._qty, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular, false, false));

                    break;
                case SMLERPReportTool._reportEnum.Serial_number:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no, null, 10, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag, null, 10, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._wh_code, null, 12, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._shelf_code, null, 12, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_resource._ar_detail, _g.d.ic_resource._table + "." + _g.d.ic_resource._ar_detail, 20, SMLReport._report._cellType.String, 0));
                    break;
                case SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number, null, 40, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._wh_code, null, 30, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_serial._table + "." + _g.d.ic_serial._shelf_code, null, 30, SMLReport._report._cellType.String, 0));
                    break;
                case SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number, null, 12, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no, null, 10, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._wh_code, null, 12, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._shelf_code, null, 12, SMLReport._report._cellType.String, 0));
                    break;
                case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                    columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 14, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 32, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 6, SMLReport._report._cellType.String, 0));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_resource._qty_sale, _g.d.ic_resource._table + "." + _g.d.ic_resource._qty_sale, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular, false, false));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount_exclude_vat, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_of_cost, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._profit, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_resource._profit_lost_persent, _g.d.ic_resource._table + "." + _g.d.ic_resource._profit_lost_persent, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular, false, false));

                    break;

            }
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 8, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 15, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 6, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount_amount, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, null, 4, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, null, 4, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 6, SMLReport._report._cellType.String, 0));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount_exclude_vat, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _showCondition()
        {
            if (this._condition == null)
            {
                //_condition = new SMLERPReportTool._conditionScreen(_icType, "");
                this._condition = new SMLERPReportTool._conditionScreen(this._mode, this._screenName);

                this._condition._extra._tableName = _g.d.ic_inventory._table;
                this._condition._extra._searchTextWord.Visible = false;
                this._condition._extra._orderByComboBox.Visible = false;
                this._condition._extra._orderByComboBox.Dispose();


                // new ArrayList ใหม่  เพราะ ป้องกันการ  add width column  เพิ่ม

                this._condition.Size = new Size(600, 600);
            }

            this._condition.ShowDialog();
            if (this._condition._processClick)
            {

                //this._check_submit = this._condition._processClick;
                // new ArrayList ใหม่  เพราะ ป้องกันการ  add width column  เพิ่ม
                //_width.Clear(); _width_2.Clear(); _width_3.Clear();
                //_column.Clear(); _column_2.Clear(); _column_3.Clear();
                //this._config();
                string __beginDate = this._condition._screen._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._condition._screen._getDataStr(_g.d.resource_report._to_date);

                if (this._mode == SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ)
                {
                    this._report._conditionText = MyLib._myGlobal._resource("ตามเอกสาร") + " : " + _getDocNo_stock_check(false);
                }
                else
                {
                    switch (this._mode)
                    {
                        case SMLERPReportTool._reportEnum.Serial_number:
                            this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                            break;
                        //case SMLERPReportTool._reportEnum.Diff_from_count :
                        //    this._report._conditionText = MyLib._myGlobal._resource("ตามเอกสาร") + " : " + _getDocNo_stock_check();
                        //    break;
                        case SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                            {
                                string __ar_type = this._condition._screen._getDataStr(_g.d.resource_report._ar_type);
                                string __condigion = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                                //this._report._conditionText = MyLib._myGlobal._resource("ตามเอกสาร") + " : " + _getDocNo_stock_check(false);
                                if (__ar_type.Length > 0)
                                {
                                    __condigion += MyLib._myGlobal._resource("ประเภทลูกหนี้") + " : " + ((MyLib._myTextBox)this._condition._screen._getControl(_g.d.resource_report._ar_type))._textLast;
                                }
                                this._report._conditionText = __condigion;
                            }
                            break;
                    }
                    this._report._conditionText = _g.g._conditionGrid(this._condition._grid, this._report._conditionText);
                }
                this._report._build();
            }

        }
    }

    public class _compareBalance
    {
        public string _itemCode = "";
        public string _whCode = "";
        public string _locationCode = "";
        public decimal _qtyOld = 0M;
        public decimal _qtyNew = 0M;
        public decimal _qtyDiff = 0M;
        public int _item_type = 0;
        public string doc_ref = "";
        public string _doc_ref_date = "";
        public decimal _average_cost = 0M;
        public decimal _average_amount
        {
            get
            {
                if (_average_cost == 0)
                    return 0;

                return (_qtyDiff * _average_cost);
            }
        }
    }

}
