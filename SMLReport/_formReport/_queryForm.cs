using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLReport._formReport
{
    public partial class _queryForm : Form
    {
        private ArrayList __queryWindow = new ArrayList();
        public _queryDetailControl _a;
        public _queryDetailControl _b;
        public _queryDetailControl _c;
        public _queryDetailControl _d;
        public _queryDetailControl _e;
        public _queryDetailControl _f;
        public _queryDetailControl _g;
        public _queryDetailControl _h;
        public _queryDetailControl _i;

        public _queryForm()
        {
            InitializeComponent();
            __queryWindow.Add(this._a);
            __queryWindow.Add(this._b);
            __queryWindow.Add(this._c);
            __queryWindow.Add(this._d);
            __queryWindow.Add(this._e);
            __queryWindow.Add(this._f);
            __queryWindow.Add(this._g);
            __queryWindow.Add(this._h);
            __queryWindow.Add(this._i);

            this.FormClosed += new FormClosedEventHandler(_queryForm_FormClosed);

        }

        void _queryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this._afterCloseQueryForm != null)
            {
                this._afterCloseQueryForm(this);
            }
        }

        public string _getQuery(string _rule)
        {
            switch (_rule.ToLower())
            {
                case "a": return this._a._queryTextBox.Text;
                case "b": return this._b._queryTextBox.Text;
                case "c": return this._c._queryTextBox.Text;
                case "d": return this._d._queryTextBox.Text;
                case "e": return this._e._queryTextBox.Text;
                case "f": return this._f._queryTextBox.Text;
                case "g": return this._g._queryTextBox.Text;
                case "h": return this._h._queryTextBox.Text;
                case "i": return this._i._queryTextBox.Text;
            }
            return "";
        }

        public void _Flush()
        {
            try
            {
                _a._flush();
                _b._flush();
                _c._flush();
                _d._flush();
                _e._flush();
                _f._flush();
                _g._flush();
                _h._flush();
                _i._flush();
            }
            catch
            {
            }
        }

        public FormQuerys _getQueryFormDesign()
        {
            FormQuerys __formquery = new FormQuerys();
            for (int __i = 0; __i < __queryWindow.Count; __i++)
            {
                __formquery.QueryLists.Add(_getFieldList((_queryDetailControl)__queryWindow[__i]));
            }

            return __formquery;

        }

        public void _loadQueryFromXML(FormQuerys __queryList)
        {
            for (int __i = 0; __i < __queryWindow.Count; __i++)
            {
                _queryDetailControl __queryScreen = (_queryDetailControl)__queryWindow[__i];

                if (((query)__queryList.QueryLists[__i])._queryString != null)
                {
                    __queryScreen._queryTextBox.Text = ((query)__queryList.QueryLists[__i])._queryString;
                    __queryScreen._resourceTextBox.Text = (((query)__queryList.QueryLists[__i])._talbeResource != null) ? ((query)__queryList.QueryLists[__i])._talbeResource.Trim() : "";

                    query __query = (query)__queryList.QueryLists[__i];

                    for (int n = 0; n < __query._fieldList.Count; n++)
                    {
                        MyLib._myGrid __gridField = (MyLib._myGrid)__queryScreen._fieldGrid;
                        int rowid = __gridField._addRow();
                        __gridField._cellUpdate(rowid, 0, ((queryField)__query._fieldList[n]).FieldName, false);
                        __gridField._cellUpdate(rowid, 1, ((queryField)__query._fieldList[n]).Resource, false);
                    }

                    for (int n = 0; n < __query._coditionList.Count; n++)
                    {
                        MyLib._myGrid __gridField = (MyLib._myGrid)__queryScreen._compareGrid;
                        //int rowid = __gridField._addRow();
                        //__gridField._cellUpdate(rowid, 0, ((fieldCondition)__query._coditionList[n]).FieldName, false);
                        __gridField._cellUpdate(n, 1, ((fieldCondition)__query._coditionList[n]).value, false);
                    }
                }

            }

        }

        private query _getFieldList(_queryDetailControl __queryScreen)
        {
            query __formQuery = new query();
            ArrayList __fieldList = new ArrayList();
            ArrayList __conditionList = new ArrayList();

            __formQuery._queryString = __queryScreen._queryTextBox.Text.Trim();
            __formQuery._talbeResource = __queryScreen._resourceTextBox.Text.Trim();

            if (__queryScreen._queryTextBox.Text != "")
            {


                MyLib._myGrid __gridQueryList = (MyLib._myGrid)__queryScreen._fieldGrid;
                for (int __i = 0; __i < __gridQueryList._rowData.Count; __i++)
                {
                    queryField __field = new queryField();
                    __field.FieldName = __gridQueryList._cellGet(__i, 0).ToString();
                    __field.Resource = __gridQueryList._cellGet(__i, 1).ToString();
                    if (__field.FieldName.Trim().Length > 0)
                    {
                        __fieldList.Add(__field);
                    }
                }

                __formQuery._fieldList = __fieldList;

                MyLib._myGrid __gridConditionList = (MyLib._myGrid)__queryScreen._compareGrid;
                for (int __i = 0; __i < __gridConditionList._rowData.Count; __i++)
                {
                    fieldCondition __condition = new fieldCondition();

                    __condition.FieldName = __gridConditionList._cellGet(__i, 0).ToString();
                    __condition.value = __gridConditionList._cellGet(__i, 1).ToString();
                    if (__condition.FieldName.Trim().Length > 0)
                    {
                        __conditionList.Add(__condition);

                    }
                }
                __formQuery._coditionList = __conditionList;
            }

            __formQuery._fieldList = __fieldList;
            __formQuery._coditionList = __conditionList;


            return __formQuery;
        }

        public List<string> _getConditionList()
        {
            List<string> __allCondition = new List<string>();
            List<fieldCondition> __fieldList = new List<fieldCondition>();

            foreach (_queryDetailControl __control in __queryWindow)
            {
                MyLib._myGrid __gridConditionList = (MyLib._myGrid)__control._compareGrid;
                for (int __i = 0; __i < __gridConditionList._rowData.Count; __i++)
                {
                    fieldCondition __condition = new fieldCondition();

                    __condition.FieldName = __gridConditionList._cellGet(__i, 0).ToString();
                    __condition.value = __gridConditionList._cellGet(__i, 1).ToString();
                    if (__condition.FieldName.Trim().Length > 0)
                    {
                        __fieldList.Add(__condition);
                    }
                }
            }

            int __index = 0;
            for (int __i = 0; __i < __fieldList.Count; __i++)
            {
                __index = 0;
                foreach (string __data in __allCondition)
                {
                    if (((string)__fieldList[__i].FieldName).Equals(__data))
                    {
                        __index = 1;
                    }
                }

                if (__index == 0)
                {
                    __allCondition.Add(__fieldList[__i].FieldName);
                }
            }

            return __allCondition;
        }

        public event AfterCloseQueryForm _afterCloseQueryForm;

    }

    public delegate void AfterCloseQueryForm(object sender);
}
