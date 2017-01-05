using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPSO
{
    public partial class _saleInformation : UserControl
    {
        DataTable _getDataAR;
        public _saleInformation()
        {
            InitializeComponent();

            _build();

            this.Load += _saleInformation_Load;
        }

        void _saleInformation_Load(object sender, EventArgs e)
        {
            // load data
            if (this.DesignMode == false)
            {
                this._loadData();
            }
        }

        void _loadData()
        {
            string __searchTextTrim = this._searchTextbox.Text.Trim();
            string[] __searchTextSplit = __searchTextTrim.Split(' ');

            // ประกอบ where
            StringBuilder __where = new StringBuilder();
            if (__searchTextSplit.Length > 1)
            {
                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                for (int __loop = 0; __loop < this._arGrid._columnList.Count; __loop++)
                {
                    bool __whereFirst = false;
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._arGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                            bool __first2 = false;
                            for (int __searchIndex = 0; __searchIndex < __searchTextSplit.Length; __searchIndex++)
                            {
                                if (__searchTextSplit[__searchIndex].Length > 0)
                                {
                                    string __getValue = __searchTextSplit[__searchIndex].ToUpper();
                                    string __newDateValue = __getValue;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            //
                                            decimal __newValue = 0M;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = MyLib._myGlobal._decimalPhase(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == false)
                                                        {
                                                            if (__where.Length > 0)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __where.Append("(");
                                                            __whereFirst = true;
                                                        }
                                                        if (__first2)
                                                        {
                                                            __where.Append(" and ");
                                                        }
                                                        __first2 = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == false)
                                                    {
                                                        if (__where.Length > 0)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __where.Append("(");
                                                        __whereFirst = true;
                                                    }
                                                    if (__first2)
                                                    {
                                                        __where.Append(" and ");
                                                    }
                                                    __first2 = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            if (__whereFirst == false)
                                            {
                                                if (__where.Length > 0)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __where.Append("(");
                                                __whereFirst = true;
                                            }
                                            if (__first2)
                                            {
                                                __where.Append(" and ");
                                            }
                                            __first2 = true;
                                            //
                                            //if (this._addQuotWhere)
                                            //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                            //else
                                            __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                            if (this._searchTextbox.Text[0] == '+')
                                            {
                                                __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                            }
                                            else
                                            {
                                                __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                            }
                                            break;
                                    }
                                }
                            }
                            if (__whereFirst)
                            {
                                __where.Append(")");
                            }
                        }
                    }
                } // for
            }
            else
            {
                bool __whereFirst = false;
                for (int __loop = 0; __loop < this._arGrid._columnList.Count; __loop++)
                {
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._arGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณีการค้นหาตัวเดียว
                            if (this._searchTextbox.Text.Length > 0)
                            {
                                try
                                {
                                    string __getValue = this._searchTextbox.Text;
                                    string __newDateValue = __getValue;
                                    Boolean __valueExtra = false;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                        __valueExtra = true;
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            double __newValue = 0;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = Double.Parse(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == true)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __whereFirst = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == true)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __whereFirst = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            //
                                            if (__valueExtra == false)
                                            {
                                                if (__whereFirst == true)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __whereFirst = true;
                                                //
                                                //if (this._addQuotWhere)
                                                //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                //else
                                                __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                                if (this._searchTextbox.Text[0] == '+')
                                                {
                                                    __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                }
                                                else
                                                {
                                                    __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                }
                                            }
                                            break;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                } // for
            }
            if (__where.Length > 0)
            {
                __where = new StringBuilder("(" + __where.ToString() + ")");
            }

            if (__where.Length > 0)
            {
                if (this._extraWhere2.Length > 0)
                {
                    __where.Append(" and (" + this._extraWhere2 + ")");
                }
            }
            else
            {
                if (this._extraWhere2.Length > 0)
                {
                    __where.Append("(" + this._extraWhere2 + ")");
                }
            }

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, name_1 from ar_customer " + ((__where.Length > 0) ? " where " + __where : "") + " order by code"));


            __query.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            this._getDataAR = ((DataSet)__result[0]).Tables[0];

            this._arGrid._loadFromDataTable(this._getDataAR);

        }

        string _extraWhere2 = "";


        void _build()
        {

            string __amountFormatStr = _g.g._getFormatNumberStr(3);
            string __qtyFormatStr = _g.g._getFormatNumberStr(1);
            this._icSearchTextbox.TextChanged += _icSearchTextbox_TextChanged;
            // grid customer
            this._arGrid._table_name = _g.d.ar_customer._table;
            this._arGrid._isEdit = false;
            this._arGrid._addColumn(_g.d.ar_customer._code, 1, 30, 30);
            this._arGrid._addColumn(_g.d.ar_customer._name_1, 1, 70, 70);
            this._arGrid._calcPersentWidthToScatter();
            this._arGrid._mouseClick += _arGrid__mouseClick;

            this._gridIC._table_name = _g.d.ic_trans_detail._table;
            this._gridIC._isEdit = false;
            this._gridIC._addColumn(_g.d.ic_trans_detail._item_code, 1, 30, 30);
            this._gridIC._addColumn(_g.d.ic_trans_detail._item_name, 1, 70, 70);
            this._gridIC._calcPersentWidthToScatter();
            this._gridIC._mouseClick += _gridIC__mouseClick;

            /*this._gridICTrans._table_name = _g.d.ic_trans_detail._table;
            this._gridICTrans._isEdit = false;
            this._gridICTrans._width_by_persent = true;
            this._gridICTrans._addColumn(_g.d.ic_trans_detail._doc_date, 4, 20, 20);
            this._gridICTrans._addColumn(_g.d.ic_trans_detail._doc_no, 1, 20, 20);
            this._gridICTrans._addColumn(_g.d.ic_trans_detail._qty, 3, 15, 15, false, false, true, false, __qtyFormatStr);
            this._gridICTrans._addColumn(_g.d.ic_trans_detail._price, 3, 15, 15, false, false, true, false, __amountFormatStr);
            this._gridICTrans._addColumn(_g.d.ic_trans_detail._unit_code, 1, 15, 15);
            this._gridICTrans._addColumn(_g.d.ic_trans_detail._discount, 1, 15, 15);
            this._gridICTrans._addColumn(_g.d.ic_trans_detail._sale_code, 1, 20, 20);
            this._gridICTrans._calcPersentWidthToScatter()*/
            this._gridICTrans._table_name = _g.d.ic_resource._table;
            this._gridICTrans._isEdit = false;
            this._gridICTrans._width_by_persent = true;
            this._gridICTrans._addColumn(_g.d.ic_resource._doc_date, 4, 20, 20);
            this._gridICTrans._addColumn(_g.d.ic_resource._doc_no, 1, 20, 20);
            this._gridICTrans._addColumn(_g.d.ic_resource._qty, 3, 15, 15, false, false, true, false, __qtyFormatStr);
            this._gridICTrans._addColumn(_g.d.ic_resource._price, 3, 15, 15, false, false, true, false, __amountFormatStr);
            this._gridICTrans._addColumn(_g.d.ic_resource._unit_code, 1, 15, 15);
            this._gridICTrans._addColumn(_g.d.ic_resource._discount, 1, 15, 15);
            this._gridICTrans._addColumn(_g.d.ic_resource._sale_detail, 1, 20, 20);
            this._gridICTrans._addColumn(_g.d.ic_resource._trans_discount, 1, 15, 15);
            this._gridICTrans._addColumn(_g.d.ic_trans._trans_flag, 2, 15, 15, false, true);
            this._gridICTrans._calcPersentWidthToScatter();
            this._gridICTrans._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridICTrans__beforeDisplayRow);


            this._gridSaleMonth._table_name = _g.d.resource_report._table;
            this._gridSaleMonth._isEdit = false;
            this._gridSaleMonth._width_by_persent = true;
            this._gridSaleMonth._addColumn(_g.d.resource_report._year, 1, 10, 10);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_jan, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_feb, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_mar, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_apr, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_may, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_jun, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_jul, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_aug, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_sep, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_oct, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_nov, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._month_dec, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._addColumn(_g.d.resource_report._total, 3, 10, 10, false, false, true, false, __amountFormatStr);
            this._gridSaleMonth._calcPersentWidthToScatter();
            //this._gridSaleMonth.Invalidate();

            MyLib._myNumberBox __overdueTextbox = (MyLib._myNumberBox)this.ar_doc_chq_status_screen1._getControl(_g.d.resource_report._overdue);
            __overdueTextbox._customSearchClick += (s1, e1) =>
            {
                string __selectCustCode = this.ar_doc_chq_status_screen1._getDataStr(_g.d.resource_report._ar_code);

                if (__selectCustCode.Length > 0)
                {
                    SMLERPAPARControl._ap_ar_billForm __billDetail = new SMLERPAPARControl._ap_ar_billForm(_g.g._transTypeEnum.ลูกหนี้);
                    __billDetail.Text = "รายการบิลค้างชำระ เกินกำหนดชำระ";
                    __billDetail._loadData(__selectCustCode, 2);
                    __billDetail.ShowDialog(MyLib._myGlobal._mainForm);
                }
            };

            MyLib._myNumberBox __ondueTextbox = (MyLib._myNumberBox)this.ar_doc_chq_status_screen1._getControl(_g.d.resource_report._ondue);
            __ondueTextbox._customSearchClick += (s1, e1) =>
            {
                string __selectCustCode = this.ar_doc_chq_status_screen1._getDataStr(_g.d.resource_report._ar_code);

                if (__selectCustCode.Length > 0)
                {
                    SMLERPAPARControl._ap_ar_billForm __billDetail = new SMLERPAPARControl._ap_ar_billForm(_g.g._transTypeEnum.ลูกหนี้);
                    __billDetail.Text = "รายการบิลยังไม่ครบกำหนดชำระ";
                    __billDetail._loadData(__selectCustCode, 1);
                    __billDetail.ShowDialog(MyLib._myGlobal._mainForm);

                }
            };

        }

        MyLib.BeforeDisplayRowReturn _gridICTrans__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;

            int __transFlagColumn = sender._findColumnByName(_g.d.ic_trans._trans_flag);
            if (__transFlagColumn != -1)
            {
                // มีการนำไปใช้แล้ว
                int __flag = MyLib._myGlobal._intPhase(sender._cellGet(row, __transFlagColumn).ToString());
                if (__flag == 48)
                {
                    __result.newColor = Color.Red;
                }

                if (__flag == 46)
                {
                    __result.newColor = Color.Green;
                }
            }


            return (__result);

        }

        void _icSearchTextbox_TextChanged(object sender, EventArgs e)
        {
            if (_tableIC != null)
            {
                StringBuilder __where = new StringBuilder();
                // split value
                string __searchTextTrim = this._icSearchTextbox.Text.Trim();
                string[] __searchTextSplit = __searchTextTrim.Split(' ');
                if (__searchTextSplit.Length > 1)
                {
                    // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                    for (int __loop = 0; __loop < this._gridIC._columnList.Count; __loop++)
                    {
                        bool __whereFirst = false;
                        MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._gridIC._columnList[__loop];
                        if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                        {
                            if (__getColumnType._isHide == false)
                            {
                                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                                bool __first2 = false;
                                for (int __searchIndex = 0; __searchIndex < __searchTextSplit.Length; __searchIndex++)
                                {
                                    if (__searchTextSplit[__searchIndex].Length > 0)
                                    {
                                        string __getValue = __searchTextSplit[__searchIndex].ToUpper();
                                        string __newDateValue = __getValue;
                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                        {
                                            __newDateValue = __getValue.ToString().Remove(0, 1);
                                        }
                                        switch (__getColumnType._type)
                                        {
                                            case 2: // Number
                                            case 3:
                                            case 5:
                                                //
                                                decimal __newValue = 0M;
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newValue = MyLib._myGlobal._decimalPhase(__newDateValue);
                                                        //
                                                        if (__newValue != 0)
                                                        {
                                                            if (__whereFirst == false)
                                                            {
                                                                if (__where.Length > 0)
                                                                {
                                                                    __where.Append(" or ");
                                                                }
                                                                __where.Append("(");
                                                                __whereFirst = true;
                                                            }
                                                            if (__first2)
                                                            {
                                                                __where.Append(" and ");
                                                            }
                                                            __first2 = true;
                                                            //
                                                            //if (this._addQuotWhere)
                                                            //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                            //else
                                                            __where.Append(string.Concat(__getColumnType._query));
                                                            //
                                                            if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                                __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                            else
                                                                __getValue = String.Concat("=", __newValue.ToString());
                                                            __where.Append(__getValue);
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 4: // Date
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                        //
                                                        if (__whereFirst == false)
                                                        {
                                                            if (__where.Length > 0)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __where.Append("(");
                                                            __whereFirst = true;
                                                        }
                                                        if (__first2)
                                                        {
                                                            __where.Append(" and ");
                                                        }
                                                        __first2 = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                        {
                                                            __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                            __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                        else
                                                        {
                                                            __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 11:
                                                {

                                                }
                                                break;
                                            default:// String
                                                if (__whereFirst == false)
                                                {
                                                    if (__where.Length > 0)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __where.Append("(");
                                                    __whereFirst = true;
                                                }
                                                if (__first2)
                                                {
                                                    __where.Append(" and ");
                                                }
                                                __first2 = true;
                                                //
                                                //if (this._addQuotWhere)
                                                //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                //else
                                                __where.Append(string.Concat("" + __getColumnType._query + ""));
                                                if (this._icSearchTextbox.Text[0] == '+')
                                                {
                                                    __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                }
                                                else
                                                {
                                                    __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                }
                                                break;
                                        }
                                    }
                                }
                                if (__whereFirst)
                                {
                                    __where.Append(")");
                                }
                            }
                        }
                    } // for
                }
                else
                {
                    bool __whereFirst = false;
                    for (int __loop = 0; __loop < this._gridIC._columnList.Count; __loop++)
                    {
                        MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._gridIC._columnList[__loop];
                        if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                        {
                            if (__getColumnType._isHide == false)
                            {
                                // กรณีการค้นหาตัวเดียว
                                if (this._icSearchTextbox.Text.Length > 0)
                                {
                                    try
                                    {
                                        string __getValue = this._icSearchTextbox.Text;
                                        string __newDateValue = __getValue;
                                        Boolean __valueExtra = false;
                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                        {
                                            __newDateValue = __getValue.ToString().Remove(0, 1);
                                            __valueExtra = true;
                                        }
                                        switch (__getColumnType._type)
                                        {
                                            case 2: // Number
                                            case 3:
                                            case 5:
                                                double __newValue = 0;
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newValue = Double.Parse(__newDateValue);
                                                        //
                                                        if (__newValue != 0)
                                                        {
                                                            if (__whereFirst == true)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __whereFirst = true;
                                                            //
                                                            //if (this._addQuotWhere)
                                                            //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                            //else
                                                            __where.Append(string.Concat(__getColumnType._query));
                                                            //
                                                            if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                                __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                            else
                                                                __getValue = String.Concat("=", __newValue.ToString());
                                                            __where.Append(__getValue);
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 4: // Date
                                                try
                                                {
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                        //
                                                        if (__whereFirst == true)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __whereFirst = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                        {
                                                            __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                            __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                        else
                                                        {
                                                            __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                break;
                                            case 11:
                                                {

                                                }
                                                break;
                                            default:// String
                                                //
                                                if (__valueExtra == false)
                                                {
                                                    if (__whereFirst == true)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __whereFirst = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat("" + __getColumnType._query + ""));
                                                    if (this._icSearchTextbox.Text[0] == '+')
                                                    {
                                                        __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    } // for
                }


                DataRow[] __selectData = _tableIC.Select(__where.ToString());
                DataTable __getTmp = _tableIC.Clone();
                foreach (DataRow row in __selectData)
                {
                    __getTmp.Rows.Add(row.ItemArray);
                }
                this._gridIC._loadFromDataTable(__getTmp);
            }
        }

        void _gridIC__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __getARCode = this.ar_doc_chq_status_screen1._getDataStr(_g.d.resource_report._ar_code);  //this._arGrid._cellGet(this._arGrid._selectRow, _g.d.ar_customer._code).ToString();
            string __getICCode = this._gridIC._cellGet(e._row, _g.d.ic_trans_detail._item_code).ToString();

            // load sale information
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(
                "select doc_no, doc_date, sum(qty) as qty, unit_code, price, discount, sum(sum_amount) as sum_amount, (sale_code || '~' || (select name_1 from erp_user where erp_user.code = temp1.sale_code ) ) as sale_detail, (select discount_word from ic_trans where ic_trans.doc_no= temp1.doc_no and ic_trans.trans_flag = temp1.trans_flag) as trans_discount, trans_flag " +

                " from ( " +
                " select doc_no, doc_date, doc_date_calc, doc_time_calc, sale_code, trans_flag, item_code, unit_code, coalesce(discount, '0') as discount, price, case when trans_flag = 48 then -1*qty else qty end as qty, case when trans_flag = 48 then -1*sum_amount else sum_amount end as sum_amount  from ic_trans_detail where cust_code = '" + __getARCode + "' and trans_flag in (44, 46, 48) and item_code = \'" + __getICCode + "\' and last_status = 0  " +
                ") as temp1 " +
                " group by doc_no, doc_date, doc_date_calc, doc_time_calc, sale_code, trans_flag, item_code, unit_code, discount, price order by doc_date_calc desc, doc_time_calc desc "
                ));


            __query.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            DataTable __getDataICTrans = ((DataSet)__result[0]).Tables[0];

            this._gridICTrans._loadFromDataTable(__getDataICTrans);
        }

        DataTable _tableIC;
        void _arGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            try
            {
                string __getARCode = this._arGrid._cellGet(e._row, _g.d.ar_customer._code).ToString();

                // load sale information
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select distinct item_code, (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) as item_name from ic_trans_detail where cust_code = \'" + __getARCode + "\' and trans_flag in (44, 46, 48) order by item_code "));

                // sale year
                string __queryGetSaleAmount = " select cust_code, doc_no, trans_flag, ( case when trans_flag in (48,103) then -1*total_amount else total_amount end ) as amount , extract(month from doc_date ) as month_sum, extract(year from doc_date ) as year_sum from ic_trans where trans_flag in (44,46,48,99,101,103) ";
                string __queryGetSaleExtractMonth = "select cust_code,(case when month_sum =1 then amount else 0 end) as month_1,(case when month_sum =2 then amount else 0 end) as month_2,(case when month_sum =3 then amount else 0 end) as month_3,(case when month_sum =4 then amount else 0 end) as month_4,(case when month_sum =5 then amount else 0 end) as month_5,(case when month_sum =6 then amount else 0 end) as month_6,(case when month_sum =7 then amount else 0 end) as month_7,(case when month_sum =8 then amount else 0 end) as month_8,(case when month_sum =9 then amount else 0 end) as month_9,(case when month_sum =10 then amount else 0 end) as month_10,(case when month_sum =11 then amount else 0 end) as month_11,(case when month_sum =12 then amount else 0 end) as month_12 , year_sum from ("
                    + __queryGetSaleAmount + ") as temp1 ";

                string __queryGetSumMonth = " select cust_code , sum(month_1) as month_1, sum(month_2) as month_2, sum(month_3) as month_3, sum(month_4) as month_4, sum(month_5) as month_5, sum(month_6) as month_6, sum(month_7) as month_7, sum(month_8) as month_8, sum(month_9) as month_9, sum(month_10) as month_10, sum(month_11) as month_11, sum(month_12) as month_12, year_sum from ( "
                    + __queryGetSaleExtractMonth + " ) as temp2 group by cust_code, year_sum order by cust_code ";

                string __getSaleYer = " select cust_code "
                    + ", month_1 as " + _g.d.resource_report._month_jan
                    + ", month_2 as " + _g.d.resource_report._month_feb
                    + ", month_3 as " + _g.d.resource_report._month_mar
                    + ", month_4 as " + _g.d.resource_report._month_apr
                    + ", month_5 as " + _g.d.resource_report._month_may
                    + ", month_6 as " + _g.d.resource_report._month_jun
                    + ", month_7 as " + _g.d.resource_report._month_jul
                    + ", month_8 as " + _g.d.resource_report._month_aug
                    + ", month_9 as " + _g.d.resource_report._month_sep
                    + ", month_10 as " + _g.d.resource_report._month_oct
                    + ", month_11 as " + _g.d.resource_report._month_nov
                    + ", month_12 as " + _g.d.resource_report._month_dec
                    + ", (month_1 + month_2 + month_3 + month_4 + month_5 + month_6 + month_7 + month_8 + month_9 + month_10 + month_11 + month_12 ) " + _g.d.resource_report._total
                    + ", year_sum as " + _g.d.resource_report._year + " from ( " + __queryGetSumMonth + " ) as temp3 where cust_code = \'" + __getARCode + "\' order by year ";

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__getSaleYer));

                // ar detail
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ar_customer._code, _g.d.ar_customer._name_1, _g.d.ar_customer._address, _g.d.ar_customer._telephone, _g.d.ar_customer._remark) + " from ar_customer where ar_customer.code = \'" + __getARCode + "\'"));

                // ar status
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select ar_code" +
                    ", sum(case when(due_date < now()) then balance_amount else 0 end ) as over_due " +
                    ", sum(case when(due_date < now()) then 1 else 0 end ) as overdue_count" +
                    ",sum(case when(due_date >= now()) then balance_amount else 0 end ) as indue " +
                    ",sum(case when(due_date >= now()) then 1 else 0 end ) as indue_count " +
                    ",sum(balance_amount) as ar_balance from sml_ar_balance_doc(null, null, \'" + __getARCode + "\') as temp1 group by ar_code "));

                // chq
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select ap_ar_code" +
                    ", sum(case when status = 0 then amount else 0 end) as onhand_amount" +
                    ", sum(case when status = 0 then 1 else 0 end) as onhand_count" +
                    ", sum(case when status = 3 then amount else 0 end) as return_amount" +
                    ", sum(case when status = 3 then 1 else 0 end) as return_count from cb_chq_list where chq_type = 1 and ap_ar_code = \'" + __getARCode + "\' group by ap_ar_code "));

                __query.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                _tableIC = ((DataSet)__result[0]).Tables[0];
                DataTable __getDataSale = ((DataSet)__result[1]).Tables[0];
                DataTable __getARTable = ((DataSet)__result[2]).Tables[0];
                DataTable __getARDebt = ((DataSet)__result[3]).Tables[0];
                DataTable __getChqBalance = ((DataSet)__result[4]).Tables[0];

                this._gridIC._loadFromDataTable(_tableIC);
                this._gridSaleMonth._loadFromDataTable(__getDataSale);

                StringBuilder __arDetaiLStr = new StringBuilder();

                __arDetaiLStr.Append("ที่อยู่" + " : " + __getARTable.Rows[0][_g.d.ar_customer._address].ToString() + "\r\n");
                __arDetaiLStr.Append("โทรศัพท์" + " : " + __getARTable.Rows[0][_g.d.ar_customer._telephone].ToString() + "\r\n");
                if (__getARTable.Rows[0][_g.d.ar_customer._remark].ToString().Length > 0)
                {
                    __arDetaiLStr.Append("หมายเหตุ" + " : " + __getARTable.Rows[0][_g.d.ar_customer._remark].ToString() + "\r\n");

                }

                this.ar_doc_chq_status_screen1._setDataStr(_g.d.resource_report._ar_code, __getARTable.Rows[0][_g.d.ar_customer._code].ToString());
                this.ar_doc_chq_status_screen1._setDataStr(_g.d.resource_report._ar_name, __getARTable.Rows[0][_g.d.ar_customer._name_1].ToString());

                this._arDetailTextbox.Text = __arDetaiLStr.ToString();

                decimal __overDueAmount = 0M;
                decimal __overDueCount = 0M;
                decimal __onDueAmount = 0M;
                decimal __onDueCount = 0M;
                decimal __totalDue = 0M;
                decimal __totalDueCount = 0M;

                decimal __chqOnHandAmount = 0M;
                decimal __onHandCount = 0M;
                decimal __chqReturnAmount = 0M;
                decimal __returnCount = 0M;
                decimal __totalChqAmount = 0M;
                decimal __totalChqCount = 0M;

                if (__getARDebt.Rows.Count > 0)
                {
                    __overDueAmount = MyLib._myGlobal._decimalPhase(__getARDebt.Rows[0]["over_due"].ToString());
                    __overDueCount = MyLib._myGlobal._decimalPhase(__getARDebt.Rows[0]["overdue_count"].ToString());
                    __onDueAmount = MyLib._myGlobal._decimalPhase(__getARDebt.Rows[0]["indue"].ToString());
                    __onDueCount = MyLib._myGlobal._decimalPhase(__getARDebt.Rows[0]["indue_count"].ToString());
                    __totalDue = __overDueAmount + __onDueAmount;
                    __totalDueCount = __overDueCount + __onDueCount;

                }

                if (__getChqBalance.Rows.Count > 0)
                {
                    __chqOnHandAmount = MyLib._myGlobal._decimalPhase(__getChqBalance.Rows[0]["onhand_amount"].ToString());
                    __onHandCount = MyLib._myGlobal._decimalPhase(__getChqBalance.Rows[0]["onhand_count"].ToString());
                    __chqReturnAmount = MyLib._myGlobal._decimalPhase(__getChqBalance.Rows[0]["return_amount"].ToString());
                    __returnCount = MyLib._myGlobal._decimalPhase(__getChqBalance.Rows[0]["return_count"].ToString());
                    __totalChqAmount = __chqOnHandAmount + __chqReturnAmount;
                    __totalChqCount = __onHandCount + __returnCount;
                }


                decimal __totalAmount = __totalDue + __totalChqAmount;
                decimal __totalCount = __totalDueCount + __totalChqCount;

                this.ar_doc_chq_status_screen1._setDataNumber(_g.d.resource_report._overdue, __overDueAmount);
                this.ar_doc_chq_status_screen1._setDataNumber(_g.d.resource_report._ondue, __onDueAmount);
                this.ar_doc_chq_status_screen1._setDataNumber(_g.d.resource_report._total_debt_balance, __totalDue);

                this.ar_doc_chq_status_screen1._setDataNumber("overdue_count", __overDueCount);
                this.ar_doc_chq_status_screen1._setDataNumber("ondue_count", __onDueCount);
                this.ar_doc_chq_status_screen1._setDataNumber("total_debt_count", __totalDueCount);


                this.ar_doc_chq_status_screen1._setDataNumber(_g.d.resource_report._cheque_in_hand, __chqOnHandAmount);
                this.ar_doc_chq_status_screen1._setDataNumber(_g.d.resource_report._cheque_return, __chqReturnAmount);
                this.ar_doc_chq_status_screen1._setDataNumber(_g.d.resource_report._total_item, __totalChqAmount);
                this.ar_doc_chq_status_screen1._setDataNumber(_g.d.resource_report._debt_value, __totalAmount);

                this.ar_doc_chq_status_screen1._setDataNumber("inhand_count", __onHandCount);
                this.ar_doc_chq_status_screen1._setDataNumber("return_count", __returnCount);
                this.ar_doc_chq_status_screen1._setDataNumber("total_chq_count", __totalChqCount);
                this.ar_doc_chq_status_screen1._setDataNumber("total_all_count", __totalCount);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                this._timer.Stop();
                this._timer.Start();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        string _oldText = "";

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (_searchAuto.Checked)
                {
                    if (_oldText.CompareTo(this._searchTextbox.Text) != 0)
                    {
                        _oldText = this._searchTextbox.Text;
                        this._loadData();
                    }
                }
            }
        }

        string _from_str = "_form";
        string _to_str = "_to";

        void _filterScreen()
        {
            List<string> __field = new List<string>();
            List<string> __fieldForm = new List<string>();
            List<string> __fieldTo = new List<string>();
            List<int> __fieldType = new List<int>();
            //
            MyLib._filterForm __form = new MyLib._filterForm();
            __form.TopMost = true;
            __form._screen._table_name = this._arGrid._table_name;
            __form._screen._maxColumn = 2;
            int __row = 0;
            for (int __loop = 0; __loop < this._arGrid._columnList.Count; __loop++)
            {
                MyLib._myGrid._columnType __column = (MyLib._myGrid._columnType)this._arGrid._columnList[__loop];
                if (__column._isHide == false && __column._isQuery == true)
                {
                    Boolean __created = false;
                    switch (__column._type)
                    {
                        case 1: // String
                            __form._screen._addTextBox(__row, 0, 0, 0, __column._originalName + this._from_str, 1, 1, 0, true, false, false, false, true, __column._originalName);
                            __form._screen._addTextBox(__row, 1, 0, 0, __column._originalName + this._to_str, 1, 1, 0, true, false, false, false, true, __column._originalName);
                            __created = true;
                            break;
                        case 3:
                        case 5: // Decimal
                            __form._screen._addNumberBox(__row, 0, 0, 0, __column._originalName + this._from_str, 1, 2, true, "", false, __column._originalName);
                            __form._screen._addNumberBox(__row, 1, 0, 0, __column._originalName + this._to_str, 1, 2, true, "", false, __column._originalName);
                            __created = true;
                            break;
                        case 4: // Date
                            __form._screen._addDateBox(__row, 0, 0, 0, __column._originalName + this._from_str, 1, true, true, false, __column._originalName);
                            __form._screen._addDateBox(__row, 1, 0, 0, __column._originalName + this._to_str, 1, true, true, false, __column._originalName);
                            __created = true;
                            break;
                    }
                    if (__created)
                    {
                        __row++;
                        __field.Add(__column._originalName);
                        __fieldForm.Add(__column._originalName + this._from_str);
                        __fieldTo.Add(__column._originalName + this._to_str);
                        __fieldType.Add(__column._type);
                    }
                }
            }
            __form._clearButton.Click += (s1, e1) =>
            {
                __form._screen._clear();
                this._extraWhere2 = "";
                this._loadData();
            };
            __form._closeButton.Click += (s1, e1) =>
            {
                __form.Dispose();
            };
            __form._filterButton.Click += (s1, e1) =>
            {
                StringBuilder __extraQuery = new StringBuilder();
                for (int __loop = 0; __loop < __fieldType.Count; __loop++)
                {
                    string __queryWhere = "";
                    switch (__fieldType[__loop])
                    {
                        case 1:
                            {
                                // String
                                string __formValue = __form._screen._getDataStr(__fieldForm[__loop]).Trim();
                                string __toValue = __form._screen._getDataStr(__fieldTo[__loop]).Trim();
                                if (__formValue.Length > 0 && __toValue.Length > 0)
                                {
                                    __queryWhere = __field[__loop] + " between \'" + __formValue + "\' and \'" + __toValue + "\'";
                                }
                                else
                                {
                                    if (__formValue.Length > 0)
                                    {
                                        __queryWhere = __field[__loop] + " >= \'" + __formValue + "\'";
                                    }
                                    else
                                    {
                                        if (__toValue.Length > 0)
                                        {
                                            __queryWhere = __field[__loop] + " <= \'" + __toValue + "\'";
                                        }
                                    }
                                }
                            }
                            break;
                        case 3:
                        case 5:
                            {
                                // Decimal
                                decimal __formValue = __form._screen._getDataNumber(__fieldForm[__loop]);
                                decimal __toValue = __form._screen._getDataNumber(__fieldTo[__loop]);
                                if (__formValue != 0 && __toValue != 0)
                                {
                                    __queryWhere = __field[__loop] + " between " + __formValue + " and " + __toValue;
                                }
                                else
                                {
                                    if (__formValue != 0)
                                    {
                                        __queryWhere = __field[__loop] + " >= " + __formValue;
                                    }
                                    else
                                    {
                                        if (__toValue != 0)
                                        {
                                            __queryWhere = __field[__loop] + " <= " + __toValue;
                                        }
                                    }
                                }
                            }
                            break;
                        case 4:
                            {
                                // Date
                                string __formValue = __form._screen._getDataStr(__fieldForm[__loop]).Trim();
                                string __toValue = __form._screen._getDataStr(__fieldTo[__loop]).Trim();
                                string __formValueQuery = __form._screen._getDataStrQuery(__fieldForm[__loop]).Trim();
                                string __toValueQuery = __form._screen._getDataStrQuery(__fieldTo[__loop]).Trim();
                                if (__formValue.Length > 0 && __toValue.Length > 0)
                                {
                                    __queryWhere = __field[__loop] + " between " + __formValueQuery + " and " + __toValueQuery;
                                }
                                else
                                {
                                    if (__formValue.Length > 0)
                                    {
                                        __queryWhere = __field[__loop] + " >= " + __formValueQuery;
                                    }
                                    else
                                    {
                                        if (__toValue.Length > 0)
                                        {
                                            __queryWhere = __field[__loop] + " <= " + __toValueQuery;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                    if (__queryWhere.Length > 0)
                    {
                        if (__extraQuery.Length > 0)
                        {
                            __extraQuery.Append(" and ");
                        }
                        __extraQuery.Append("(" + __queryWhere + ")");
                    }
                }
                this._extraWhere2 = __extraQuery.ToString();
                this._loadData();
            };
            __form.Show();
        }


        private void _filterButton_Click(object sender, EventArgs e)
        {
            this._filterScreen();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    public class ar_doc_chq_status_screen : MyLib._myScreen
    {
        public ar_doc_chq_status_screen()
        {
            this._table_name = _g.d.resource_report._table;

            int __row = 0;
            this._maxColumn = 2;
            //__row++;
            this._addTextBox(__row++, 0, _g.d.resource_report._ar_code, 20);
            this._addTextBox(__row++, 0, _g.d.resource_report._ar_name, 50);

            this._addNumberBox(__row, 0, 1, 1, _g.d.resource_report._overdue, 1, 2, true);
            this._addNumberBox(__row++, 1, 1, 1, "overdue_count", 1, 2, true, "", true, _g.d.resource_report._sum_item_list);

            this._addNumberBox(__row, 0, 1, 1, _g.d.resource_report._ondue, 1, 2, true);
            this._addNumberBox(__row++, 1, 1, 1, "ondue_count", 1, 2, true, "", true, _g.d.resource_report._sum_item_list);

            this._addNumberBox(__row, 0, 1, 1, _g.d.resource_report._total_debt_balance, 1, 2, true, "", true, _g.d.resource_report._total);
            this._addNumberBox(__row++, 1, 1, 1, "total_debt_count", 1, 2, true, "", true, _g.d.resource_report._sum_item_list);

            this._addNumberBox(__row, 0, 1, 1, _g.d.resource_report._cheque_in_hand, 1, 2, true);
            this._addNumberBox(__row++, 1, 1, 1, "inhand_count", 1, 2, true, "", true, _g.d.resource_report._sum_item_list);

            this._addNumberBox(__row, 0, 1, 1, _g.d.resource_report._cheque_return, 1, 2, true);
            this._addNumberBox(__row++, 1, 1, 1, "return_count", 1, 2, true, "", true, _g.d.resource_report._sum_item_list);

            this._addNumberBox(__row, 0, 1, 1, _g.d.resource_report._total_item, 1, 2, true, "", true, _g.d.resource_report._total);
            this._addNumberBox(__row++, 1, 1, 1, "total_chq_count", 1, 2, true, "", true, _g.d.resource_report._sum_item_list);

            this._addNumberBox(__row, 0, 1, 1, _g.d.resource_report._debt_value, 1, 2, true);
            this._addNumberBox(__row++, 1, 1, 1, "total_all_count", 1, 2, true, "", true, _g.d.resource_report._sum_item_list);

            this._enabedControl(_g.d.resource_report._ar_code, false);
            this._enabedControl(_g.d.resource_report._ar_name, false);

            /*this._enabedControl(_g.d.resource_report._overdue, false);
            this._enabedControl(_g.d.resource_report._ondue, false);*/

            this._enabedControl(_g.d.resource_report._total_debt_balance, false);
            this._enabedControl(_g.d.resource_report._cheque_in_hand, false);
            this._enabedControl(_g.d.resource_report._cheque_return, false);
            this._enabedControl(_g.d.resource_report._total_item, false);
            this._enabedControl(_g.d.resource_report._debt_value, false);

            this._enabedControl("overdue_count", false);
            this._enabedControl("ondue_count", false);
            this._enabedControl("total_debt_count", false);
            this._enabedControl("inhand_count", false);
            this._enabedControl("return_count", false);
            this._enabedControl("total_chq_count", false);
            this._enabedControl("total_all_count", false);

            MyLib._myNumberBox __overdueTextbox = (MyLib._myNumberBox)this._getControl(_g.d.resource_report._overdue);
            __overdueTextbox.textBox.Enabled = false;
            __overdueTextbox._iconSearch.Image = Properties.Resources.view1;

            MyLib._myNumberBox __ondueTextbox = (MyLib._myNumberBox)this._getControl(_g.d.resource_report._ondue);
            __ondueTextbox.textBox.Enabled = false;
            __ondueTextbox._iconSearch.Image = Properties.Resources.view1;
            __overdueTextbox._customSearchClick += (o1, s1) =>
            {

            };

        }
    }
}
