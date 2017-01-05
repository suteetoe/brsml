using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _search_chq_form : Form
    {
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);
        private _g.g._transControlTypeEnum _mode;

        public _search_chq_form(_g.g._transControlTypeEnum mode)
        {
            InitializeComponent();

            this._mode = mode;

            string __ap_ar_name = "";
            switch (this._mode)
            {
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                    __ap_ar_name = _g.d.cb_chq_list._ar_name;
                    break;
                default:
                    __ap_ar_name = _g.d.cb_chq_list._ar_name;
                    break;
            }

            this._resultGrid._isEdit = false;
            this._resultGrid._table_name = _g.d.cb_chq_list._table;
            this._resultGrid._addColumn("Select", 11, 10, 5);
            this._resultGrid._addColumn(_g.d.cb_chq_list._chq_number, 1, 10, 15);
            this._resultGrid._addColumn(_g.d.cb_chq_list._chq_get_date, 4, 10, 15);
            this._resultGrid._addColumn(_g.d.cb_chq_list._doc_ref, 1, 10, 15);
            this._resultGrid._addColumn(_g.d.cb_chq_list._chq_due_date, 4, 10, 10);

            this._resultGrid._addColumn(_g.d.cb_chq_list._ap_ar_code, 1, 10, 35, false, false, true, false, "", _g.d.cb_chq_list._ap_ar_code, "", __ap_ar_name);
            this._resultGrid._addColumn(_g.d.cb_chq_list._amount, 3, 10, 20, false, false, false, false, _formatNumberAmount);

            this._resultGrid._addColumn(_g.d.cb_chq_list._doc_line_number, 1, 10, 0, false, true);
            //
            //
            //this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._amount, Color.AliceBlue);
            //this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._due_day, Color.AliceBlue);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();

        }

        string _extraWhere = "";

        public void _process(string extraWhere)
        {
            _extraWhere = extraWhere;
            _loadData();
        }

        void _loadData()
        {
            // key search

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

            // ประกอบ Query

            string __ap_ar_name = _g.d.cb_chq_list._ap_ar_code;
            string __chqType = "";

            switch (this._mode)
            {
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก:
                    __ap_ar_name = _g.d.cb_chq_list._ap_ar_code + " || \'~\' || (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._ap_ar_code + ") ";
                    __chqType = "3";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                    __ap_ar_name = _g.d.cb_chq_list._ap_ar_code + " || \'~\' || (select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._ap_ar_code + ") ";
                    __chqType = "2";

                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก:
                    __ap_ar_name = _g.d.cb_chq_list._ap_ar_code + " || \'~\' || (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._ap_ar_code + ") ";
                    __chqType = "1";

                    break;
                default:
                    __ap_ar_name = _g.d.cb_chq_list._ap_ar_code + " || \'~\' || (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._ap_ar_code + ") ";
                    __chqType = "1";

                    break;
            }

            // for extrawhere           

            StringBuilder __query = new StringBuilder("select ");
            __query.Append(MyLib._myGlobal._fieldAndComma(
                _g.d.cb_chq_list._chq_number,
                _g.d.cb_chq_list._chq_get_date,
                _g.d.cb_chq_list._doc_ref,
                _g.d.cb_chq_list._chq_due_date,
                __ap_ar_name + " as " + _g.d.cb_chq_list._ap_ar_code,
                ((this._mode == _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน) ? _g.d.cb_chq_list._sum_amount + " as " : "" )  + _g.d.cb_chq_list._amount,
                _g.d.cb_chq_list._doc_line_number
                ));

            __query.Append(" from " + _g.d.cb_chq_list._table);

            __query.Append(" where " + _g.d.cb_chq_list._chq_type + "=" + __chqType);
            if (_extraWhere.Length > 0)
            {
                __query.Append(_extraWhere);
            }

            if (__where.Length > 0)
            {
                __query.Append(" and  (" + __where.ToString() + ")");
            }

            __query.Append(" order by " + _g.d.cb_chq_list._chq_due_date);

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort(__query.ToString());

            if (__result.Tables.Count > 0)
                this._resultGrid._loadFromDataTable(__result.Tables[0]);


        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._resultGrid._rowData.Count; __row++)
            {
                this._resultGrid._cellUpdate(__row, "Select", 1, false);
            }
            this._resultGrid.Invalidate();
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
    }


}
