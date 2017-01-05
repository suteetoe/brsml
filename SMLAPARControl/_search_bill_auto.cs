using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPARControl
{
    public partial class _search_bill_auto : Form
    {
        public _search_bill_auto()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        private string _result;

        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }

        private void _OkButton_Click(object sender, EventArgs e)
        {
            _result = "";
            bool _isChk = false;
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            MyLib._myCheckBox _getData_1 = (MyLib._myCheckBox)this._arScreenSearchPayBillAuto1._getControl(_g.d.ap_ar_resource._so_debt_balance);
            MyLib._myCheckBox _getData_2 = (MyLib._myCheckBox)this._arScreenSearchPayBillAuto1._getControl(_g.d.ap_ar_resource._so_cn_balance);
            MyLib._myCheckBox _getData_3 = (MyLib._myCheckBox)this._arScreenSearchPayBillAuto1._getControl(_g.d.ap_ar_resource._so_billing);
            MyLib._myCheckBox _getData_4 = (MyLib._myCheckBox)this._arScreenSearchPayBillAuto1._getControl(_g.d.ap_ar_resource._so_addition_debt);
            if ((_getData_1.Checked) || (_getData_2.Checked))
            {
                string _str = " ( select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " +
                    _g.d.ar_customer._code + " = " + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ap_ar_resource._ap_ar_name;
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select  "
                + _g.d.ap_ar_trans._cust_code + "," + _str + "," + _g.d.ap_ar_trans._doc_no + "," + _g.d.ap_ar_trans._doc_date + ","
                + _g.d.ap_ar_trans._due_date + "," + _g.d.ap_ar_trans._total_debt_balance + "," + _g.d.ap_ar_trans._total_pay_money+ ","
                + _g.d.ap_ar_trans._trans_flag + " from " + _g.d.ap_ar_trans._table + " " + _getArSearch(_getData_1.Checked, _getData_2.Checked)));
                _isChk = true;
            }
            if ((_getData_3.Checked) || (_getData_4.Checked))
            {
                string _str = " ( select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " +
                    _g.d.ar_customer._code + " = " + _g.d.ic_trans._cust_code + ") as " + _g.d.ap_ar_resource._ap_ar_name;
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select  "
                + _g.d.ic_trans._cust_code + "," + _str + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._doc_date + ","
                + _g.d.ic_trans._due_date + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._trans_type + " from "
                + _g.d.ic_trans._table + " " + _getItenSearch(_getData_3.Checked, _getData_4.Checked)));
                _isChk = true;
            }
            __myQuery.Append("</node>");
            if (_isChk)
            {
                _result = __myQuery.ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("กรุณาเลือกเงือนไขก่อน"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            
        }

        string _getArSearch(bool d1, bool d2)
        {
            string __result = "";
            string _s_customer = "";
            string _e_customer = "";
            string _s_doc_no = "";
            string _e_doc_no = "";
            DateTime _s_date;
            DateTime _e_date;

            _s_date = MyLib._myGlobal._convertDate(this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._from_doc_date));
            _e_date = MyLib._myGlobal._convertDate(this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._to_doc_date));
            if ((_s_date.Year > 1500) && (_e_date.Year > 1500))
            {
                __result = "(" + _g.d.ap_ar_trans._doc_date + "  between '" + MyLib._myGlobal._convertDateToQuery(_s_date) + "' and '" + MyLib._myGlobal._convertDateToQuery(_e_date) + "')";
            }
            else
            {
                if ((_s_date.Year > 1500)) __result = "(" + _g.d.ap_ar_trans._doc_date + "  = '" + MyLib._myGlobal._convertDateToQuery(_s_date) + "')";
                if ((_e_date.Year > 1500)) __result = "(" + _g.d.ap_ar_trans._doc_date + "  = '" + MyLib._myGlobal._convertDateToQuery(_e_date) + "')";
            }
            _s_customer = this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._from_customer);
            _e_customer = this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._to_customer);
            if (!_s_customer.Equals("") && (!_e_customer.Equals("")))
            {
                if (!__result.Equals("")) __result += " and ";
                __result += "(" + _g.d.ap_ar_trans._cust_code + " >= '" + _s_customer + "' and " + _g.d.ap_ar_trans._cust_code + " <= '" + _e_customer + "')";
            }
            else
            {
                if ((!_s_customer.Equals("")) || (!_e_customer.Equals(""))) if (!__result.Equals("")) __result += " and ";
                if (!_s_customer.Equals("")) __result += "(" + _g.d.ap_ar_trans._cust_code + " = '" + _s_customer + "')";
                if (!_e_customer.Equals("")) __result += "(" + _g.d.ap_ar_trans._cust_code + " = '" + _e_customer + "')";
            }
            _s_doc_no = this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._from_doc_no);
            _e_doc_no = this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._to_doc_no);
            if (!_s_doc_no.Equals("") && (!_e_doc_no.Equals("")))
            {
                if (!__result.Equals("")) __result += " and ";
                __result += "(" + _g.d.ap_ar_trans._doc_no + " >= '" + _s_doc_no + "' and " + _g.d.ap_ar_trans._doc_no + " <= '" + _e_doc_no + "')";
            }
            else
            {
                if ((!_s_doc_no.Equals("")) || (!_s_doc_no.Equals(""))) if (!__result.Equals("")) __result += " and ";
                if (!_s_doc_no.Equals("")) __result += "(" + _g.d.ap_ar_trans._doc_no + " = '" + _s_doc_no + "')";
                if (!_e_doc_no.Equals("")) __result += "(" + _g.d.ap_ar_trans._doc_no + " = '" + _e_doc_no + "')";
            }

            string _query = "";
            if (d1)
            {
                _query = " ((" + _g.d.ap_ar_trans._trans_flag + "  = 6) and (" + _g.d.ap_ar_trans._trans_type + " = 2))";
            }
            if (d2)
            {
                if (!_query.Equals("")) _query += " or ";
                _query += " ((" + _g.d.ap_ar_trans._trans_flag + "  = 7) and (" + _g.d.ap_ar_trans._trans_type + " = 2))";
            }
            if (_query.Length > 0)
            {
                if (!__result.Equals("")) __result += " and ";
                __result += _query;
            }
            if (__result.Length > 0) __result = " where " + __result;
            return __result;
        }

        string _getItenSearch(bool d3 ,bool d4)
        {
            string __result = "";
            string _s_customer = "";
            string _e_customer = "";
            string _s_doc_no = "";
            string _e_doc_no = "";
            DateTime _s_date;
            DateTime _e_date;

            _s_date = MyLib._myGlobal._convertDate(this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._from_doc_date));
            _e_date = MyLib._myGlobal._convertDate(this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._to_doc_date));
            if ((_s_date.Year > 1500) && (_e_date.Year > 1500))
            {
                __result = "(" + _g.d.ic_trans._doc_date + "  between '" + MyLib._myGlobal._convertDateToQuery(_s_date) + "' and '" + MyLib._myGlobal._convertDateToQuery(_e_date) + "')";
            }
            else
            {
                if ((_s_date.Year > 1500)) __result = "(" + _g.d.ic_trans._doc_date + "  = '" + MyLib._myGlobal._convertDateToQuery(_s_date) + "')";
                if ((_e_date.Year > 1500)) __result = "(" + _g.d.ic_trans._doc_date + "  = '" + MyLib._myGlobal._convertDateToQuery(_e_date) + "')";
            }
            _s_customer = this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._from_customer);
            _e_customer = this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._to_customer);
            if (!_s_customer.Equals("") && (!_e_customer.Equals("")))
            {
                if (!__result.Equals("")) __result += " and ";
                __result += "(" + _g.d.ic_trans._cust_code + " >= '" + _s_customer + "' and " + _g.d.ic_trans._cust_code + " <= '" + _e_customer + "')";
            }
            else
            {
                if ((!_s_customer.Equals("")) || (!_e_customer.Equals(""))) if (!__result.Equals("")) __result += " and ";
                if (!_s_customer.Equals("")) __result += "(" + _g.d.ic_trans._cust_code + " = '" + _s_customer + "')";
                if (!_e_customer.Equals("")) __result += "(" + _g.d.ic_trans._cust_code + " = '" + _e_customer + "')";
            }
            _s_doc_no = this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._from_doc_no);
            _e_doc_no = this._arScreenSearchPayBillAuto1._getDataStr(_g.d.ap_ar_resource._to_doc_no);
            if (!_s_doc_no.Equals("") && (!_e_doc_no.Equals("")))
            {
                if (!__result.Equals("")) __result += " and ";
                __result += "(" + _g.d.ic_trans._doc_no + " >= '" + _s_doc_no + "' and " + _g.d.ic_trans._doc_no + " <= '" + _e_doc_no + "')";
            }
            else
            {
                if ((!_s_doc_no.Equals("")) || (!_s_doc_no.Equals(""))) if (!__result.Equals("")) __result += " and ";
                if (!_s_doc_no.Equals("")) __result += "(" + _g.d.ic_trans._doc_no + " = '" + _s_doc_no + "')";
                if (!_e_doc_no.Equals("")) __result += "(" + _g.d.ic_trans._doc_no + " = '" + _e_doc_no + "')";
            }
            string _query = "";
            if (d3)
            {
                _query = " ((" + _g.d.ic_trans._trans_flag + "  = 14) and (" + _g.d.ic_trans._trans_type + " = 2))";
            }
            if (d4)
            {
                if (!_query.Equals("")) _query += " or ";
                _query += " ((" + _g.d.ic_trans._trans_flag + "  = 15) and (" + _g.d.ic_trans._trans_type + " = 2))";
            }
            if (_query.Length > 0)
            {
                if (!__result.Equals("")) __result += " and ";
                __result += _query;
            }
            if (__result.Length > 0) __result = " where " + __result;
            return __result;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
