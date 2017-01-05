using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SMLReport._formReport
{
    public partial class _queryDetailControl : UserControl
    {
        private Form _designForm;
        private SMLReport._design._queryDesigner _design;
        private ArrayList _conditionHistory = new ArrayList();
        private ArrayList _condition = new ArrayList();
        private string _conditionColumnName = "FieldName";
        private string _colditionColumnValue = "Value";
        private string _fieldColumnName = "FieldName";
        private string _fieldColumnResoure = "Resource";

        public int _getConditionCount
        {
            get
            {
                return _condition.Count;
            }
        }
        public _queryDetailControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._compareGrid._table_name = "";
            this._compareGrid._addColumn(this._conditionColumnName, 1, 100, 50);
            this._compareGrid._addColumn(this._colditionColumnValue, 1, 100, 50);
            //
            this._fieldGrid._table_name = "";
            this._fieldGrid._addColumn(this._fieldColumnName, 1, 100, 50);
            this._fieldGrid._addColumn(this._fieldColumnResoure, 1, 100, 50);
            //
            this._queryTextBox.TextChanged += new EventHandler(_queryTextBox_TextChanged);
        }

        void _queryTextBox_TextChanged(object sender, EventArgs e)
        {
            this._condition = new ArrayList();
            string[] __getCondition = Regex.Split(this._queryTextBox.Text.Trim(), @"(#)|(#)");
            int __loop = 0;
            bool __foundShare = false;
            while (__loop < __getCondition.Length)
            {
                if (__foundShare == true)
                {
                    _conditionClass __data = new _conditionClass();
                    __data._fieldName = __getCondition[__loop];
                    __data._value = "";
                    this._condition.Add(__data);
                    __loop++;
                    __foundShare = false;
                }
                else
                {
                    if (__getCondition[__loop].Equals("#"))
                    {
                        __foundShare = true;
                    }
                }
                __loop++;
            }
            // เก็บค่าเก่าก่อน
            this._conditionHistory.Clear();
            for (int __find1 = 0; __find1 < this._compareGrid._rowData.Count; __find1++)
            {
                _conditionClass __data = new _conditionClass();
                __data._fieldName = this._compareGrid._cellGet(__find1, this._conditionColumnName).ToString();
                __data._value = this._compareGrid._cellGet(__find1, this._colditionColumnValue).ToString();
                this._conditionHistory.Add(__data);
            }
            this._compareGrid._clear();
            for (int __loop2 = 0; __loop2 < this._condition.Count; __loop2++)
            {
                int __row = this._compareGrid._addRow();
                this._compareGrid._cellUpdate(__row, this._conditionColumnName, ((_conditionClass)this._condition[__loop2])._fieldName, false);
                this._compareGrid._cellUpdate(__row, this._colditionColumnValue, ((_conditionClass)this._condition[__loop2])._value, false);
            }
            // get History
            for (int __find1 = 0; __find1 < this._compareGrid._rowData.Count; __find1++)
            {
                string __getFieldName = this._compareGrid._cellGet(__find1, this._conditionColumnName).ToString();
                for (int __find2 = 0; __find2 < this._conditionHistory.Count; __find2++)
                {
                    if (__getFieldName.Equals(((_conditionClass)this._conditionHistory[__find2])._fieldName))
                    {
                        this._compareGrid._cellUpdate(__find1, this._colditionColumnValue, ((_conditionClass)this._conditionHistory[__find2])._value, false);
                        break;
                    }
                }
            }
            this._compareGrid.Invalidate();
        }

        private void _queryBuilderButton_Click(object sender, EventArgs e)
        {
            this._design = new _design._queryDesigner();
            this._design._saveButton.Click += new EventHandler(_saveButton_Click);
            this._designForm = new Form();
            this._design.Dock = DockStyle.Fill;
            this._designForm.Controls.Add(this._design);
            this._designForm.WindowState = FormWindowState.Maximized;
            this._designForm.ShowDialog();
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            this._queryTextBox.Text = this._design._queryTextBox.Text;
            this._designForm.Dispose();
        }

        struct _conditionClass
        {
            public string _fieldName;
            public string _value;
        }

        string _processQuery(string source, ArrayList condition)
        {
            string __result = source;
            for (int __loop = 0; __loop < condition.Count; __loop++)
            {
                _conditionClass __replace = (_conditionClass)condition[__loop];
                __result = __result.Replace("#" + __replace._fieldName + "#", __replace._value);
            }
            return __result;
        }

        ArrayList _packCondition()
        {
            ArrayList __result = new ArrayList();
            for (int __loop = 0; __loop < this._compareGrid._rowData.Count; __loop++)
            {
                _conditionClass __data = new _conditionClass();
                __data._fieldName = this._compareGrid._cellGet(__loop, this._conditionColumnName).ToString();
                __data._value = this._compareGrid._cellGet(__loop, this._colditionColumnValue).ToString();
                __result.Add(__data);
            }
            return __result;
        }

        public void _flush()
        {
            if (this._queryTextBox.Text.Length > 0)
            {
                this._getField();
            }
            else
            {
                this._fieldGrid._rowData.Clear();
            }
        }

        private void _fieldNameButton_Click(object sender, EventArgs e)
        {
            this._getField();
        }

        private void _getField()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string[] __result = __myFrameWork._queryColumnName(MyLib._myGlobal._databaseName, _processQuery(this._queryTextBox.Text, _packCondition())).Split(',');
                this._fieldGrid._clear();
                for (int __row = 0; __row < __result.Length; __row++)
                {
                    int __addr = this._fieldGrid._addRow();
                    this._fieldGrid._cellUpdate(__addr, this._fieldColumnName, __result[__row].ToString(), false);
                    string __resourceName = __result[__row].ToString();
                    if (__resourceName.IndexOf('.') == -1 && this._resourceTextBox.Text.Trim().Length > 0)
                    {
                        __resourceName = this._resourceTextBox.Text.Trim() + "." + __resourceName;
                    }
                    MyLib._myResourceType __findResource = MyLib._myResource._findResource(__resourceName, __resourceName, false);
                    this._fieldGrid._cellUpdate(__addr, this._fieldColumnResoure, __findResource._str, false);
                }
                this.Cursor = Cursors.Default;
                this._fieldGrid.Invalidate();
            }
            catch (Exception ex)
            {
                // Debugger.Break();
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _executeButton_Click(object sender, EventArgs e)
        {
            SMLReport._formReport._queryExecuteForm __result = new _formReport._queryExecuteForm();
            __result.__query = _processQuery(this._queryTextBox.Text, _packCondition());
            __result.ShowDialog();
        }

        /// <summary>
        /// รายวัน
        /// </summary>
        /// <param name="__mode"></param>
        /// <returns></returns>
        private string _getICTransDailyQuery(string __mode)
        {

            string __tableName = "";
            string __wheretrans = "";
            string __query = "";

            StringBuilder __ap_ar_query = new StringBuilder();

            //  addition เพิ่มเติม
            switch (__mode)
            {
                case "supplier":
                    string _supplier_name = ", (select " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ap_supplier._name_1 + "";
                    string _supplier_address = ",(select " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._address + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ap_supplier._address;
                    string _supplier_telephone = ", (select " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._telephone + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ap_supplier._telephone + "";
                    string _supplier_fax = ", (select " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._fax + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ap_supplier._fax + "";

                    __ap_ar_query.Append(_supplier_name + _supplier_address + _supplier_telephone + _supplier_fax);
                    break;
                case "customer":
                    string _customer_name = ", (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ar_customer._name_1 + "";
                    string _customer_address = ",(select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._address + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ar_customer._address;
                    string _customer_telephone = ", (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._telephone + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ar_customer._telephone + "";
                    string _customer_fax = ", (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ar_customer._fax + "";

                    __ap_ar_query.Append(_customer_name + _customer_address + _customer_telephone + _customer_fax);
                    break;
                case "buy":
                    string _buy_supplier_name = ", (select " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ap_supplier._name_1 + "";
                    string _buy_supplier_address = ",(select " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._address + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ap_supplier._address;
                    string _buy_supplier_telephone = ", (select " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._telephone + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ap_supplier._telephone + "";
                    string _buy_supplier_fax = ", (select " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._fax + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ap_supplier._fax + "";

                    __ap_ar_query.Append(_buy_supplier_name + _buy_supplier_address + _buy_supplier_telephone + _buy_supplier_fax);
                    break;
                case "sale":
                    string _sale_customer_name = ", (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ar_customer._name_1 + "";
                    string _sale_customer_address = ",(select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._address + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ar_customer._address;
                    string _sale_customer_telephone = ", (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._telephone + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ar_customer._telephone + "";
                    string _sale_customer_fax = ", (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as " + _g.d.ar_customer._fax + "";

                    __ap_ar_query.Append(_sale_customer_name + _sale_customer_address + _sale_customer_telephone + _sale_customer_fax);
                    break;
            }

            // field get

            switch (__mode)
            {
                case "buy":
                case "sale":
                case "inventory":
                    {
                        __tableName = _g.d.ic_trans._table;
                        __wheretrans = " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = '#doc_no#' and " + _g.d.ic_trans._table + ". " + _g.d.ic_trans._trans_flag + " = #trans_flag# ";

                        string __trans_field = MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._contactor,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code,
                            "(select name_1 from erp_user where code=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as sale_name", 
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._tax_doc_no,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._tax_doc_date,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._credit_day,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._credit_date,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._send_day,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._send_date,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._discount_word,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._vat_rate,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._total_value,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._total_after_vat,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._total_except_vat,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._total_before_vat,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._wh_from,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._location_from,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._wh_to,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._location_to,
                            _g.d.ic_trans._table + "." + _g.d.ic_trans._remark
                        );

                        // ประกอบ 
                        __query = "select " + __trans_field + __ap_ar_query.ToString() + " from " + __tableName + __wheretrans;
                    }
                    break;
                case "supplier":
                case "customer":
                    __tableName = _g.d.ap_ar_trans._table;
                    __wheretrans = " where " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no + " = '#doc_no#' and " + _g.d.ap_ar_trans._table + ". " + _g.d.ap_ar_trans._trans_flag + " = #trans_flag# ";

                    string __ap_ar_trans_field = MyLib._myGlobal._fieldAndComma(
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_time,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_ref,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_ref_date,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._tax_doc_no,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._tax_doc_date,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._sale_code,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._credit_day,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._due_date,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._vat_rate,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_value,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_discount,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_vat_value,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_after_vat,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_before_vat,
                            _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._remark
                        );

                    __query = "select " + _g.d.ap_ar_trans._table + ".* " + __ap_ar_query.ToString() + " from " + __tableName + __wheretrans;
                    break;
            }


            this._queryTextBox.Text = __query;


            return __query;
        }

        /// <summary>
        /// รายละเอียดรายวัน
        /// </summary>
        /// <param name="__mode"></param>
        /// <returns></returns>
        private string _getICTransDetailDailyQuery(string __mode)
        {

            string __tableName = "";
            string __wheretrans = "";
            string __query = "";

            StringBuilder __query_append = new StringBuilder();

            //  addition เพิ่มเติม
            switch (__mode.ToLower())
            {
                case "supplier":
                case "customer":
                    //string _customer_name = ", (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ar_customer._name_1 + "";
                    //string _customer_address = ",(select ( coalesce(" + _g.d.ar_customer._table + "." + _g.d.ar_customer._address + ", '') || case when length(coalesce((select " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._name_1 + " from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + "), '')) > 0 then ' ต.' ||  (select " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._name_1 + " from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + ") else '' end || case when length (coalesce((select " + _g.d.erp_amper._table + "." + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + "), '')) > 0 then ' อ.' || (select " + _g.d.erp_amper._table + "." + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + ") else '' end || 	case when length (coalesce((select " + _g.d.erp_province._table + "." + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + "), '')) > 0 then ' จ.' || (select " + _g.d.erp_province._table + "." + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") else '' end || ' ' ||  coalesce(" + _g.d.ar_customer._table + "." + _g.d.ar_customer._zip_code + ", '')) from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ar_customer._address;
                    //string _customer_telephone = ", (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._telephone + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ar_customer._telephone + "";
                    //string _customer_fax = ", (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ar_customer._fax + "";

                    //__query_append.Append(_customer_name + _customer_address + _customer_telephone + _customer_fax);
                    break;
                case "buy":
                case "sale":
                case "inventory":
                    string _unit_name = ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as unit_name";
                    string _actual_averate_cost = "";
                    string _barcode_price_1 = "";

                    __query_append.Append(_unit_name + _actual_averate_cost + _barcode_price_1);
                    break;
            }

            // field get

            switch (__mode)
            {
                case "buy":
                case "sale":
                case "inventory":
                    {
                        __tableName = _g.d.ic_trans_detail._table;
                        __wheretrans = " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " = '#doc_no#' and " + _g.d.ic_trans_detail._table + ". " + _g.d.ic_trans_detail._trans_flag + " = #trans_flag# ";

                        string __trans_field = MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code,
                            _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name,
                            _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code,
                            _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_ref,
                            _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty,
                            _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount,
                            _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount,
                            _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code,
                            _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code,
                            "(select ic_warehouse.name_1 from ic_warehouse where ic_warehouse.code = ic_trans_detail.wh_code ) as wh_name",
                            "(select ic_shelf.name_1 from ic_shelf where ic_shelf.code = ic_trans_detail.shelf_code and ic_shelf.whcode= ic_trans_detail.wh_code) as shelf_name"
                        );

                        // ประกอบ 
                        __query = "select " + __trans_field + __query_append.ToString() + " from " + __tableName + __wheretrans;
                    }
                    break;
                case "supplier":
                case "customer":

                    // select line_number,billing_no,billing_date,due_date,sum_pay_money from ap_ar_trans_detail
                    __tableName = _g.d.ap_ar_trans_detail._table;
                    __wheretrans = " where " + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._doc_no + " = '#doc_no#' and " + _g.d.ap_ar_trans_detail._table + ". " + _g.d.ap_ar_trans_detail._trans_flag + " = #trans_flag# ";

                    string __ap_ar_trans_field = MyLib._myGlobal._fieldAndComma(
                            _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._line_number,
                            _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_no,
                            _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_date,
                            _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._due_date,
                            _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_pay_money

                        );

                    __query = "select " + __ap_ar_trans_field.ToString() + __query_append.ToString() + " from " + __tableName + __wheretrans;
                    break;
            }


            this._queryTextBox.Text = __query;


            return __query;
        }

        private void _queryCompanyLabel_Click(object sender, EventArgs e)
        {
            string __query = "select " + _g.d.erp_company_profile._table + ".* from " + _g.d.erp_company_profile._table;
            this._queryTextBox.Text = __query;
        }

        private void _queryMasterBuyLabel_Click(object sender, EventArgs e)
        {
            string __query = _getICTransDailyQuery("buy");
            this._queryTextBox.Text = __query;
        }

        private void _queryDetailBuyLabel_Click(object sender, EventArgs e)
        {
            this._queryTextBox.Text = _getICTransDetailDailyQuery("buy");
        }

        private void _queryMasterSaleButton_Click(object sender, EventArgs e)
        {
            this._queryTextBox.Text = _getICTransDailyQuery("sale");
        }

        private void _queryDetailSaleButton_Click(object sender, EventArgs e)
        {
            this._queryTextBox.Text = _getICTransDetailDailyQuery("sale");
        }

        private void _supplierDailyQueryButton_Click(object sender, EventArgs e)
        {

        }

        private void _supplierDailyDetailQueryButton_Click(object sender, EventArgs e)
        {

        }

        private void _customerDailyQueryButton_Click(object sender, EventArgs e)
        {

        }

        private void _customerDailyDetailQueryButton_Click(object sender, EventArgs e)
        {

        }

        private void _inventoryDailyQueryButton_Click(object sender, EventArgs e)
        {
            this._queryTextBox.Text = this._getICTransDailyQuery("inventory");
        }

        private void _inventoryDailyDetailQueryButton_Click(object sender, EventArgs e)
        {
            this._queryTextBox.Text = _getICTransDetailDailyQuery("inventory");
        }

        private string _cb_trans_QueryString()
        {
            return @"";
        }

        private string _cb_trans_detail_QueryString()
        {
            return @"SELECT   
	coalesce((select amount from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag# and doc_type = 1 ), 0) as transfer_amount,
	coalesce((select name_1 from erp_bank where code = (select bank_code from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag#  and doc_type = 1 )), '') as transfer_bankname,
	coalesce((select name_1 from erp_bank_branch  where (code = (select bank_branch from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag#  and doc_type = 1 )) and  (bank_code = (select bank_code from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag#  and doc_type = 1 )) ), '') as transfer_bankbranch,
	coalesce((select amount from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag# and doc_type = 3), 0) as credit_amount,
 	coalesce((select credit_card_type from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag# and doc_type = 3), '') as credit_card_type,
 	coalesce((select trans_number from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag# and doc_type = 3), '') as credit_approve_number,
	coalesce((select amount from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag# and doc_type = 2 limit 1 ), 0) as chq_amount1,
 	coalesce((select trans_number from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag# and doc_type = 2 limit 1), '') as chq_number1,
	coalesce((select name_1 from erp_bank where code = (select bank_code from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag#  and doc_type = 2 limit 1 )), '') as chq_bankname1,
	coalesce((select name_1 from erp_bank_branch  where (code = (select bank_branch from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag#  and doc_type = 2 limit 1 )) and  (bank_code = (select bank_code from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag#  and doc_type = 2 limit 1 )) ), '') as chq_bankbranch1,
	coalesce((select amount from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag# and doc_type = 2 limit 1 offset 1 ), 0) as chq_amount2,
 	coalesce((select trans_number from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag# and doc_type = 2 limit 1 offset 1), '') as chq_number2,
	coalesce((select name_1 from erp_bank where code = (select bank_code from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag#  and doc_type = 2 limit 1offset 1 )), '') as chq_bankname2,
	coalesce((select name_1 from erp_bank_branch  where (code = (select bank_branch from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag#  and doc_type = 2 limit 1 offset 1 )) and  (bank_code = (select bank_code from cb_trans_detail where doc_no = '#doc_no#' and trans_flag = #trans_flag#  and doc_type = 2 limit 1 offset 1 )) ), '') as chq_bankbranch2

";
        }
    }
}
