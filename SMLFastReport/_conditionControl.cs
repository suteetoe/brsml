using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLFastReport
{
    public class _conditionControl : MyLib._myScreen
    {
        private _xmlClass _xmlSource;
        public List<_conditionValueCass> _field;
        private List<_searchSpecClass> _searchList = new List<_searchSpecClass>();

        public delegate void _beforeBuildConditionScreen();
        public event _beforeBuildConditionScreen _beforeBuild;

        public string _getDefaultValueTextBox(string defaultValues)
        {
            if (defaultValues.IndexOf("#") != -1)
            {
                if (defaultValues.IndexOf("#year#") != -1)
                {
                    string __year = DateTime.Now.ToString("yyyy", new System.Globalization.CultureInfo("th-TH"));
                    defaultValues = defaultValues.Replace("#year#", __year);
                }
                if (defaultValues.IndexOf("#month#") != -1)
                {
                    string __year = DateTime.Now.ToString("MM", new System.Globalization.CultureInfo("th-TH"));
                    defaultValues = defaultValues.Replace("#month#", __year);
                }
            }
            return defaultValues;
        }

        public void _build(_xmlClass xmlSource)
        {
            if (this._beforeBuild != null)
            {
                this._beforeBuild();
            }

            try
            {
                this._searchList.Clear();
                List<_conditionValueCass> __fieldValueOld = new List<_conditionValueCass>();
                this._buildFieldValue();
                if (this._field != null)
                {
                    for (int __loop = 0; __loop < this._field.Count; __loop++)
                    {
                        __fieldValueOld.Add(this._field[__loop]);
                    }
                }
                //
                this._xmlSource = xmlSource;
                try
                {
                    
                    this.Controls.Clear();
                }
                catch
                {
                }
                this._maxColumn = 1;
                // หาจำนวน column สูงสุด
                for (int __loop = 0; __loop < xmlSource._conditionList.Count; __loop++)
                {
                    if (xmlSource._conditionList[__loop]._column + 1 > this._maxColumn)
                    {
                        this._maxColumn = xmlSource._conditionList[__loop]._column + 1;
                    }
                }
                this._table_name = "";
                this._field = new List<_conditionValueCass>();
                // build screen
                for (int __loop = 0; __loop < xmlSource._conditionList.Count; __loop++)
                {
                    _conditionDetailClass __fieldCondition = xmlSource._conditionList[__loop];
                    Boolean __displayLabel = (__fieldCondition._code.Length > 0) ? true : false;
                    _conditionValueCass __newField = new _conditionValueCass();
                    int __columnSpan = (__fieldCondition._span == 0) ? 1 : __fieldCondition._span;
                    switch (__fieldCondition._type)
                    {
                        case "Text":
                            this._addTextBox(__fieldCondition._row, __fieldCondition._column, 1, 1, __fieldCondition._code, __columnSpan, 10, (__fieldCondition._command.Trim().Length > 0) ? 1 : 0, __displayLabel, false, true, false, true, __fieldCondition._code, __fieldCondition._command);
                            __newField._queryName = __fieldCondition._name;
                            __newField._name = __fieldCondition._code;
                            __newField._fieldType = __fieldCondition._type;
                            __newField._command = __fieldCondition._command;
                            __newField._value = "";
                            this._field.Add(__newField);
                            if (__fieldCondition._defaultValue.Length > 0)
                            {
                                this._setDataStr(__fieldCondition._code, this._getDefaultValueTextBox(__fieldCondition._defaultValue));
                            }
                            break;
                        case "Number":
                            this._addNumberBox(__fieldCondition._row, __fieldCondition._column, 1, 1, __fieldCondition._code, 1, 2, true, "", true, __fieldCondition._code, __fieldCondition._command);
                            __newField._queryName = __fieldCondition._name;
                            __newField._name = __fieldCondition._code;
                            __newField._fieldType = __fieldCondition._type;
                            __newField._command = __fieldCondition._command;
                            __newField._value = 0M;
                            this._field.Add(__newField);
                            //__newCondition._name = __field._fieldName;
                            /*if (this._editMode == false && __defaultValue.Length == 2)
                            {
                                this._setDataNumber(__field._fieldName + this._addFieldForm, MyLib._myGlobal._decimalPhase(__defaultValue[0].ToString()));
                                this._setDataNumber(__field._fieldName + this._addFieldTo, MyLib._myGlobal._decimalPhase(__defaultValue[1].ToString()));
                            }*/
                            break;
                        case "Date":
                            this._addDateBox(__fieldCondition._row, __fieldCondition._column, 1, 1, __fieldCondition._code, 1, __displayLabel, true, true, __fieldCondition._code);
                            __newField._queryName = __fieldCondition._name;
                            __newField._name = __fieldCondition._code;
                            __newField._fieldType = __fieldCondition._type;
                            __newField._command = __fieldCondition._command;
                            __newField._value = DateTime.Now;
                            this._setDataDate(__fieldCondition._code, __newField._value);
                            this._field.Add(__newField);
                            if (__fieldCondition._defaultValue.Length > 0)
                            {
                                switch (__fieldCondition._defaultValue.ToLower())
                                {
                                    case "month_begin": __newField._value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); break;
                                    case "month_end": __newField._value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1); break;
                                }
                                this._setDataDate(__fieldCondition._code, __newField._value);
                            }
                            break;
                        case "DropDown":
                            __newField._queryName = __fieldCondition._name;
                            __newField._name = __fieldCondition._code;
                            __newField._fieldType = __fieldCondition._type;
                            __newField._command = __fieldCondition._command;
                            __newField._value = DateTime.Now;
                            this._field.Add(__newField);
                            try
                            {
                                string[] __command = __fieldCondition._command.Split(',');
                                List<string> __resource = new List<string>();
                                for (int __row = 0; __row < __command.Length; __row++)
                                {
                                    __resource.Add(MyLib._myResource._findResource(__command[__row], false)._str);
                                }
                                this._addComboBox(__fieldCondition._row, __fieldCondition._column, __fieldCondition._code, true, __resource.ToArray(), false);
                            }
                            catch
                            {
                            }
                            break;
                    }
                }
                // set value
                for (int __loop = 0; __loop < __fieldValueOld.Count; __loop++)
                {
                    switch (__fieldValueOld[__loop]._fieldType)
                    {
                        case "Text":
                            this._setDataStr(__fieldValueOld[__loop]._name, __fieldValueOld[__loop]._value.ToString());
                            break;
                        case "Number":
                            this._setDataNumber(__fieldValueOld[__loop]._name, MyLib._myGlobal._decimalPhase(__fieldValueOld[__loop]._value.ToString()));
                            break;
                        case "Date":
                            this._setDataDate(__fieldValueOld[__loop]._name, __fieldValueOld[__loop]._value);
                            break;
                        case "DropDown":

                            break;
                    }
                }
                //
                this._textBoxSearch -= new MyLib.TextBoxSearchHandler(_conditionControl__textBoxSearch);
                this._textBoxSearch += new MyLib.TextBoxSearchHandler(_conditionControl__textBoxSearch);
                this.Invalidate();
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _conditionControl__textBoxSearch(object sender)
        {
            try
            {
                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                Boolean __foundOldSearch = false;
                for (int __loop = 0; __loop < this._searchList.Count; __loop++)
                {
                    if (this._searchList[__loop]._searName.Equals(__getControl._name))
                    {
                        this._searchList[__loop].__search.ShowDialog();
                        __foundOldSearch = true;
                        break;
                    }
                }
                if (__foundOldSearch == false)
                {
                    string __searchFormat = __getControl._searchFormat.Trim();
                    if (__searchFormat.Length > 0)
                    {
                        if (__searchFormat[0] == '!')
                        {
                            string __selectFieldName = "";

                            string __formatName = __searchFormat.Remove(0, 1);
                            MyLib._searchDataFull __search = new MyLib._searchDataFull();
                            if (__formatName[0] == '*')
                            {
                                __formatName = __formatName.Remove(0, 1);
                                __search.WindowState = FormWindowState.Maximized;
                            }
                            if (__formatName.IndexOf(":") != -1)
                            {
                                string __getExtraWhere = __formatName.Substring(__formatName.IndexOf(":") + 1);
                                __search._dataList._extraWhere = __getExtraWhere;

                                __formatName = __formatName.Replace(__getExtraWhere, "");
                                __formatName = __formatName.Replace(":", "");

                            }

                            if (__formatName.IndexOf("#") != -1)
                            {
                                if (__formatName.Length - 1 > __formatName.IndexOf("#"))
                                {
                                    // ตัดเอา ชื่อ field ออกมา
                                    __selectFieldName = __formatName.Substring(__formatName.IndexOf("#") + 1);

                                    __formatName = __formatName.Replace(__selectFieldName, "");
                                }

                                __formatName = __formatName.Replace("#", "");
                                __search._dataList._multiSelect = true;
                            }
                            __search._name = __getControl._name;
                            __search._dataList._loadViewFormat(__formatName, MyLib._myGlobal._userSearchScreenGroup, false);

                            if (__selectFieldName.Length > 0)
                            {
                                __search._dataList._multiSelectColumnName = __selectFieldName;
                            }
                            __search._dataList._gridData._mouseClick += (s1, e1) =>
                            {
                                __getControl.textBox.Text = e1._text;
                                __search.Close();
                            };
                            __search._dataList._selectSuccessButton.Click += (s1, e1) =>
                            {
                                __getControl.textBox.Text = __search._dataList._selectList();
                                __search.Close();
                            };
                            __search._dataList._refreshData();
                            __search._dataList._loadViewData(0);
                            MyLib._myGlobal._startSearchBox(__getControl, __getControl._labelName, __search, true);
                            this._searchList.Add(new _searchSpecClass(__search._name, __search));
                        }
                        else
                            if (__searchFormat.IndexOf('[') != -1)
                        {
                            string __formatName = __searchFormat;
                            MyLib._searchDataFull __search = new MyLib._searchDataFull();
                            __search._dataList._gridData._width_by_persent = true;
                            __search._dataList._lockRecord = false;
                            if (__formatName[0] == '*')
                            {
                                __formatName = __formatName.Remove(0, 1);
                                __search.WindowState = FormWindowState.Maximized;
                            }
                            if (__formatName.IndexOf("#") != -1)
                            {
                                __formatName = __formatName.Replace("#", "");
                                __search._dataList._multiSelect = true;
                                __search.WindowState = FormWindowState.Maximized;
                            }
                            // [erp_user,code:text:20,name_1:text:20]
                            __formatName = __formatName.Replace("[", "").Replace("]", "");
                            string[] __list = __formatName.Split(',');
                            __search._dataList._tableName = __list[0].ToString();
                            for (int __loop = 1; __loop < __list.Length; __loop++)
                            {
                                string[] __field = __list[__loop].Split(':');
                                int __width = (int)MyLib._myGlobal._decimalPhase(__field[2].ToString());
                                if (__field[1].ToLower().Equals("text"))
                                {
                                    __search._dataList._gridData._addColumn(__field[0].ToString(), 1, 10, __width);
                                }
                            }
                            __search._dataList._gridData._addColumn("is_lock_record", 2, 0, 0, false, true, true, false);
                            __search._dataList._gridData._mouseClick += (s1, e1) =>
                            {
                                __getControl.textBox.Text = e1._text;
                                __search.Close();
                            };
                            __search._dataList._selectSuccessButton.Click += (s1, e1) =>
                            {
                                __getControl.textBox.Text = __search._dataList._selectList();
                                __search.Close();
                            };
                            __search._dataList._refreshData();
                            __search._dataList._loadViewData(0);
                            MyLib._myGlobal._startSearchBox(__getControl, __getControl._labelName, __search, true);
                            this._searchList.Add(new _searchSpecClass(__search._name, __search));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        string _genCodeList(string fieldName, string value)
        {
            return MyLib._myUtil._genCodeList(fieldName, value);
            /*try
            {
                string[] __split = value.Split(',');
                StringBuilder __between = new StringBuilder();
                StringBuilder __tempText = new StringBuilder();
                Boolean __haveIn = false;
                string __orText = "";
                for (int __loop = 0; __loop < __split.Length; __loop++)
                {
                    string __str = __split[__loop];
                    if (__str.IndexOf(':') != -1)
                    {
                        if (__between.Length > 0)
                        {
                            __between.Append(" or ");
                        }
                        string[] __split2 = __str.Split(':');
                        __between.Append(fieldName + " between \'" + __split2[0].ToString() + "\' and \'" + __split2[1].ToString() + "\'");
                    }
                    else
                    {
                        if (__str.Length > 0)
                        {
                            if (__tempText.Length > 0)
                            {
                                __tempText.Append(",");
                            }
                            __tempText.Append("\'" + __str + "\'");
                            __haveIn = true;
                        }
                    }
                }
                if (__haveIn)
                {
                    if (__between.Length > 0)
                    {
                        __orText = " or ";
                    }
                    __tempText = new StringBuilder(__orText + fieldName + " in (" + __tempText.ToString() + ") ");
                }
                return __between.ToString() + __tempText.ToString();
            }
            catch
            {
                return "";
            }*/
        }

        /// <summary>
        /// เอาเงื่อนไขจากจอ เข้า list
        /// </summary>
        public void _buildFieldValue()
        {
            if (this._field != null)
            {
                for (int __loop = 0; __loop < this._field.Count; __loop++)
                {
                    // ค้นหาตามชื่อ field
                    string __defaultValue = "";
                    string __columnName = "";
                    for (int __row = 0; __row < _xmlSource._conditionList.Count; __row++)
                    {
                        if (_xmlSource._conditionList[__row]._name.Equals(this._field[__loop]._queryName))
                        {
                            __defaultValue = _xmlSource._conditionList[__row]._defaultValue;
                            __columnName = _xmlSource._conditionList[__row]._columnName;
                            break;
                        }
                    }
                    switch (this._field[__loop]._fieldType)
                    {
                        case "Text":
                            {
                                string __value = this._getDataStr(this._field[__loop]._name);
                                switch (this._field[__loop]._command.ToLower())
                                {
                                    case "#year#":
                                        __value = ((int)MyLib._myGlobal._decimalPhase(__value) - MyLib._myGlobal._year_add).ToString();
                                        break;
                                }
                                this._field[__loop]._value = __value;
                                this._field[__loop]._valueWhere = (__columnName != null && __columnName.Length > 0) ? " where (" + __columnName + "=\'" + __value + "\') " : "";
                                this._field[__loop]._valueAnd = (__columnName != null && __columnName.Length > 0) ? " and ( " + __columnName + "=\'" + __value + "\' )" : "";
                                this._field[__loop]._valueOr = (__columnName != null && __columnName.Length > 0) ? " or ( " + __columnName + "=\'" + __value + "\' )" : "";

                                this._field[__loop]._valueAndcheck = (__columnName != null && __columnName.Length > 0 && __value.Length > 0) ? " and ( " + __columnName + "=\'" + __value + "\' )" : "";
                                this._field[__loop]._valueOrCheck = (__columnName != null && __columnName.Length > 0 && __value.Length > 0) ? " or ( " + __columnName + "=\'" + __value + "\' )" : "";

                                if (this._field[__loop]._command.IndexOf('#') != -1)
                                {
                                    string __codeList = this._genCodeList(__columnName, __value);
                                    if (__codeList.Length > 0)
                                    {
                                        this._field[__loop]._valueWhere = " where (" + __codeList + ")";
                                        this._field[__loop]._valueAnd = " and (" + __codeList + ")";
                                        this._field[__loop]._valueAndcheck = " and (" + __codeList + ")";
                                        this._field[__loop]._valueOr = " or (" + __codeList + ")";
                                        this._field[__loop]._valueOrCheck = " or (" + __codeList + ")";
                                    }
                                    else
                                    {
                                        this._field[__loop]._valueWhere = "";
                                        this._field[__loop]._valueAnd = "";
                                        this._field[__loop]._valueOr = "";
                                    }
                                }
                            }
                            break;
                        case "Number":
                            {
                                string __value = this._getDataNumber(this._field[__loop]._name).ToString();
                                this._field[__loop]._value = __value;
                                this._field[__loop]._valueWhere = "";
                                this._field[__loop]._valueAnd = "";
                                this._field[__loop]._valueOr = "";
                            }
                            break;
                        case "Date":
                            {
                                DateTime __value = MyLib._myGlobal._convertDate(this._getDataStr(this._field[__loop]._name));
                                this._field[__loop]._value = __value;
                                this._field[__loop]._valueWhere = "";
                                this._field[__loop]._valueAnd = "";
                                this._field[__loop]._valueOr = "";
                            }
                            break;
                        case "DropDown":
                            {

                                int __dropDownValue = (int)MyLib._myGlobal._decimalPhase(this._getDataStr(this._field[__loop]._name));
                                string __value = __defaultValue.Split(',')[__dropDownValue];
                                this._field[__loop]._value = __value;

                                this._field[__loop]._valueWhere = (__columnName != null && __columnName.Length > 0) ? " where (" + __columnName + "=\'" + __value + "\') " : "";
                                this._field[__loop]._valueAnd = (__columnName != null && __columnName.Length > 0) ? " and ( " + __columnName + "=\'" + __value + "\' )" : "";
                                this._field[__loop]._valueOr = (__columnName != null && __columnName.Length > 0) ? " or ( " + __columnName + "=\'" + __value + "\' )" : "";

                                this._field[__loop]._valueAndcheck = (__columnName != null && __columnName.Length > 0 && __value.Length > 0) ? " and ( " + __columnName + "=\'" + __value + "\' )" : "";
                                this._field[__loop]._valueOrCheck = (__columnName != null && __columnName.Length > 0 && __value.Length > 0) ? " or ( " + __columnName + "=\'" + __value + "\' )" : "";

                            }
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// ข้อมูลการเลือก
    /// </summary>
    public class _conditionValueCass
    {
        public string _queryName;
        public string _name;
        public object _value;
        public string _valueWhere = "";
        public string _valueAnd = "";
        public string _valueOr = "";
        public string _fieldType;
        public string _command;

        public string _valueAndcheck = "";
        public string _valueOrCheck = "";
    }

    public class _searchSpecClass
    {
        public string _searName;
        public MyLib._searchDataFull __search;

        public _searchSpecClass(string name, MyLib._searchDataFull searchControl)
        {
            this._searName = name;
            this.__search = searchControl;
        }
    }
}
