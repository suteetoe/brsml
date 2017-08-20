using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPARControl
{
    public partial class _selectBillForm : Form
    {
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);
        private _g.g._transControlTypeEnum _mode;

        private string _sortColumn = _g.d.ap_ar_resource._due_date;
        string _getOrderBy = "";
        public _selectBillForm(_g.g._transControlTypeEnum mode)
        {
            InitializeComponent();
            //
            this._mode = mode;
            this._resultGrid._isEdit = false;
            this._resultGrid._table_name = _g.d.ap_ar_resource._table;
            this._resultGrid._addColumn(_g.d.ap_ar_resource._select, 11, 10, 5);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_no, 1, 10, 15);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_date, 4, 10, 15);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._ref_doc_no, 1, 10, 15);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._ref_doc_date, 4, 10, 15);

            this._resultGrid._addColumn(_g.d.ap_ar_resource._tax_doc_no, 1, 10, 15);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._tax_doc_date, 4, 10, 15);

            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_type, 1, 10, 15, false);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._due_date, 4, 10, 15);
            //
            this._resultGrid._addColumn(_g.d.ap_ar_resource._amount, 3, 10, 15, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._ar_balance, 3, 10, 15, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._due_day, 3, 10, 10, false, false, false, false, _g.g._getFormatNumberStr(0, 0));

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                this._resultGrid._addColumn(_g.d.ap_ar_resource._remark, 1, 10, 15);
            }
            //
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._amount, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._due_day, Color.AliceBlue);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();

            // sort
            this._resultGrid._mouseClick += _resultGrid__mouseClick;
        }

        private void _resultGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {

            if (e._row == -1 && e._column != -1)
            {
                // start sort datatable

                MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType) this._resultGrid._columnList[e._column];

                string __newOrder = __getColumnType._originalName;
                if (__newOrder.CompareTo(this._getOrderBy) == 0)
                {
                    this._getOrderBy = __newOrder + " desc";
                }
                else
                {
                    this._getOrderBy = __newOrder;
                }

                this._sortColumn = this._getOrderBy;
                this._loadData();
            }
        }

        public void _process(string custCode, DateTime processDate)
        {
            this._custCode = custCode;
            this._processDate = processDate;

            _loadData();
        }

        string _custCode = "";
        DateTime _processDate;

        void _loadData()
        {
            // ประกอบ where

            string __searchTextTrim = this._searchTextbox.textBox.Text.Trim();
            string[] __searchTextSplit = __searchTextTrim.Split(' ');

            // ประกอบ where
            StringBuilder __where = new StringBuilder();
            if (__searchTextSplit.Length > 1)
            {
                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                for (int __loop = 0; __loop < _resultGrid._columnList.Count; __loop++)
                {
                    bool __whereFirst = false;
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)_resultGrid._columnList[__loop];
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
                                            if (this._searchTextbox.textBox.Text[0] == '+')
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
                for (int __loop = 0; __loop < _resultGrid._columnList.Count; __loop++)
                {
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)_resultGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณีการค้นหาตัวเดียว
                            if (this._searchTextbox.textBox.Text.Length > 0)
                            {
                                try
                                {
                                    string __getValue = this._searchTextbox.textBox.Text;
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
                                                if (this._searchTextbox.textBox.Text[0] == '+')
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

            if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only == true && ( _g.g._companyProfile._change_branch_code == false))
            {
                if (__where.Length > 0)
                    __where.Append(" and ");

                __where.Append(" ( " + _g.d.ap_ar_resource._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\') ");

            }

            if ((this._mode == _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล || this._mode == _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล) && _g.g._companyProfile._filter_pay_bill == true)
            {
                if (__where.Length > 0)
                    __where.Append(" and ");
                if (this._mode == _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล)
                    __where.Append(" ( not exists (select doc_no from ap_ar_trans_detail where ap_ar_trans_detail.billing_no = temp3.doc_no and ap_ar_trans_detail.bill_type = temp3.doc_type and ap_ar_trans_detail.last_status = 0 and ap_ar_trans_detail.trans_flag = 235) ) ");
                else
                    __where.Append(" ( not exists (select doc_no from ap_ar_trans_detail where ap_ar_trans_detail.billing_no = temp3.doc_no and ap_ar_trans_detail.bill_type = temp3.doc_type and ap_ar_trans_detail.last_status = 0 and ap_ar_trans_detail.trans_flag = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล) + ") ) ");

            }

            if (this._showZeroDocCheckbox.Checked == true)
            {
                if (__where.Length > 0)
                    __where.Append(" and ");

                __where.Append(" (true) or ( amount=0 and (select count(doc_no) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no = temp3.doc_no ) = 0 ) ");
            }

            SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
            this._resultGrid._clear();
            DataTable __data = __process._arBalanceDoc(this._mode, 0, this._custCode, this._custCode, "", "", this._processDate, _sortColumn, __where.ToString(), false);
            if (__data != null)
            {
                this._resultGrid._loadFromDataTable(__data);
            }
        }

        string _extraWhere2 = "";
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
            __form._screen._table_name = this._resultGrid._table_name;
            __form._screen._maxColumn = 2;
            int __row = 0;
            for (int __loop = 0; __loop < this._resultGrid._columnList.Count; __loop++)
            {
                MyLib._myGrid._columnType __column = (MyLib._myGrid._columnType)this._resultGrid._columnList[__loop];
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


        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._resultGrid._rowData.Count; __row++)
            {
                this._resultGrid._cellUpdate(__row, _g.d.ap_ar_resource._select, 1, false);
            }
            this._resultGrid.Invalidate();
        }

        private void _filterButtn_Click(object sender, EventArgs e)
        {
            this._filterScreen();
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
                    if (_oldText.CompareTo(this._searchTextbox.textBox.Text) != 0)
                    {
                        _oldText = this._searchTextbox.textBox.Text;
                        this._loadData();
                    }
                }
            }

        }

        private void _showZeroDocCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this._loadData();
        }
    }
}
