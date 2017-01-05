using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace SMLERPARAPInfo
{
    public class _conditionScreen : MyLib._myScreen
    {
        MyLib._searchDataFull _searchDialog = new MyLib._searchDataFull();
        string _custCodeFullFieldName = "";
        string _custCodeFieldName = "";
        string _custBeginFieldName = "";
        string _custEndFieldName = "";

        public _conditionScreen(_apArConditionEnum mode)
        {
            if (mode == _apArConditionEnum.ว่าง)
            {
                return;
            }
            //
            this.Dock = DockStyle.Top;
            this.AutoSize = true;
            DateTime __today = DateTime.Now;
            this._table_name = _g.d.ap_ar_resource._table;
            this._maxColumn = 2;
            string __searchFormatName = "";

            switch (_apAr._apArCheck(mode))
            {
                case _apArEnum.ลูกหนี้:
                    __searchFormatName = _g.g._search_screen_ar;
                    this._custCodeFullFieldName = _g.d.ar_customer._table + "." + _g.d.ar_customer._code;
                    this._custCodeFieldName = _g.d.ar_customer._code;
                    this._custBeginFieldName = _g.d.ap_ar_resource._ar_code_begin;
                    this._custEndFieldName = _g.d.ap_ar_resource._ar_code_end;
                    break;
                case _apArEnum.เจ้าหนี้:
                    this._custCodeFullFieldName = _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code;
                    __searchFormatName = _g.g._search_screen_ap;
                    this._custCodeFieldName = _g.d.ap_supplier._code;
                    this._custBeginFieldName = _g.d.ap_ar_resource._ap_code_begin;
                    this._custEndFieldName = _g.d.ap_ar_resource._ap_code_end;
                    break;
            }
            switch (mode)
            {
                case _apArConditionEnum.ลูกหนี้_สถานะลูกหนี้:
                case _apArConditionEnum.เจ้าหนี้_สถานะเจ้าหนี้:
                    this._addTextBox(0, 0, 0, 0, this._custBeginFieldName, 1, 25, 1, true, false, false);
                    this._addTextBox(0, 1, 0, 0, this._custEndFieldName, 1, 25, 1, true, false, false);
                    this._addDateBox(1, 0, 0, 0, _g.d.ap_ar_resource._date_begin, 1, true);
                    this._addDateBox(1, 1, 0, 0, _g.d.ap_ar_resource._date_end, 1, true);
                    //
                    this._setDataDate(_g.d.ap_ar_resource._date_begin, new DateTime(__today.Year, __today.Month, 1));
                    this._setDataDate(_g.d.ap_ar_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
                    break;
                case _apArConditionEnum.เจ้าหนี้_เคลื่อนไหว:
                case _apArConditionEnum.ลูกหนี้_เคลื่อนไหว:
                    this._addTextBox(0, 0, 0, 0, this._custBeginFieldName, 1, 25, 1, true, false, false);
                    this._addDateBox(1, 0, 0, 0, _g.d.ap_ar_resource._date_begin, 1, true);
                    this._addDateBox(1, 1, 0, 0, _g.d.ap_ar_resource._date_end, 1, true);
                    //
                    this._setDataDate(_g.d.ap_ar_resource._date_begin, new DateTime(__today.Year, __today.Month, 1));
                    this._setDataDate(_g.d.ap_ar_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
                    break;
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยก_เอกสาร:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร:
                case _apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้:
                case _apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้_แยก_เอกสาร:
                case _apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้_แยกลูกหนี้_แยกเอกสาร:
                    string __formatNumber = MyLib._myGlobal._getFormatNumber("m00");
                    this._addTextBox(0, 0, 0, 0, this._custBeginFieldName, 1, 25, 1, true, false, false);
                    if (mode == _apArConditionEnum.ลูกหนี้_อายุลูกหนี้ || mode == _apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้)
                    {
                        this._addTextBox(0, 1, 0, 0, this._custEndFieldName, 1, 25, 1, true, false, false);
                    }
                    this._addDateBox(1, 0, 0, 0, _g.d.ap_ar_resource._date_end, 1, true);
                    this._addNumberBox(2, 0, 1, 0, _g.d.ap_ar_resource._term_1_begin, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 1, 1, 0, _g.d.ap_ar_resource._term_1_end, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 0, 1, 0, _g.d.ap_ar_resource._term_2_begin, 1, 2, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ap_ar_resource._term_2_end, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 0, 1, 0, _g.d.ap_ar_resource._term_3_begin, 1, 2, true, __formatNumber);
                    this._addNumberBox(4, 1, 1, 0, _g.d.ap_ar_resource._term_3_end, 1, 2, true, __formatNumber);
                    this._addNumberBox(5, 0, 1, 0, _g.d.ap_ar_resource._term_4_begin, 1, 2, true, __formatNumber);
                    this._addNumberBox(5, 1, 1, 0, _g.d.ap_ar_resource._term_4_end, 1, 2, true, __formatNumber);
                    MyLib._myGroupBox __dueDate = this._addGroupBox(6, 0, 1, 2, 2, _g.d.ap_ar_resource._due_date_select, true);
                    this._addRadioButtonOnGroupBox(0, 0, __dueDate, _g.d.ap_ar_resource._by_due_date, 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, __dueDate, _g.d.ap_ar_resource._by_doc_date, 1, false);
                    //
                    this._textBoxChanged += new MyLib.TextBoxChangedHandler(_arConditionScreen__textBoxChanged);
                    //
                    this._setDataDate(_g.d.ap_ar_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
                    this._setDataNumber(_g.d.ap_ar_resource._term_1_begin, 1.0M);
                    this._setDataNumber(_g.d.ap_ar_resource._term_1_end, 30.0M);
                    this._setDataNumber(_g.d.ap_ar_resource._term_2_begin, 31.0M);
                    this._setDataNumber(_g.d.ap_ar_resource._term_2_end, 60.0M);
                    this._setDataNumber(_g.d.ap_ar_resource._term_3_begin, 61.0M);
                    this._setDataNumber(_g.d.ap_ar_resource._term_3_end, 90.0M);
                    this._setDataNumber(_g.d.ap_ar_resource._term_4_begin, 91.0M);
                    this._setDataNumber(_g.d.ap_ar_resource._term_4_end, 120.0M);
                    this._getControl(_g.d.ap_ar_resource._term_2_begin).Enabled = false;
                    this._getControl(_g.d.ap_ar_resource._term_3_begin).Enabled = false;
                    this._getControl(_g.d.ap_ar_resource._term_4_begin).Enabled = false;
                    break;
            }
            //
            this._textBoxSearch -= new MyLib.TextBoxSearchHandler(_arConditionControl__textBoxSearch);
            this._textBoxChanged -= new MyLib.TextBoxChangedHandler(_arConditionControl__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_arConditionControl__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_arConditionControl__textBoxChanged);
            //
            this._searchDialog._dataList._loadViewFormat(__searchFormatName, MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchDialog._dataList._refreshData();
            this._searchDialog._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this.Invalidate();
        }

        void _arConditionScreen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ap_ar_resource._term_1_end))
            {
                int __term_1_end = (int)this._getDataNumber(_g.d.ap_ar_resource._term_1_end);
                this._setDataNumber(_g.d.ap_ar_resource._term_2_begin, __term_1_end + 1);
            }
            else
                if (name.Equals(_g.d.ap_ar_resource._term_2_end))
                {
                    int __term_2_end = (int)this._getDataNumber(_g.d.ap_ar_resource._term_2_end);
                    this._setDataNumber(_g.d.ap_ar_resource._term_3_begin, __term_2_end + 1);
                }
                else
                    if (name.Equals(_g.d.ap_ar_resource._term_3_end))
                    {
                        int __term_3_end = (int)this._getDataNumber(_g.d.ap_ar_resource._term_3_end);
                        this._setDataNumber(_g.d.ap_ar_resource._term_4_begin, __term_3_end + 1);
                    }
                    else
                        if (name.Equals(_g.d.ap_ar_resource._term_4_end))
                        {
                            int __term_4_end = (int)this._getDataNumber(_g.d.ap_ar_resource._term_4_end);
                            this._setDataNumber(_g.d.ap_ar_resource._term_5_end, __term_4_end);
                        }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchDialog.Close();
            string __custCode = ((MyLib._myGrid)sender)._cellGet(e._row, this._custCodeFullFieldName).ToString();
            //
            if (this._searchDialog._name.Equals(_g.d.ap_ar_resource._ar_code_begin)) this._setDataStr(_g.d.ap_ar_resource._ar_code_begin, __custCode);
            else
                if (this._searchDialog._name.Equals(_g.d.ap_ar_resource._ar_code_end)) this._setDataStr(_g.d.ap_ar_resource._ar_code_end, __custCode);
                else
                    if (this._searchDialog._name.Equals(_g.d.ap_ar_resource._ap_code_begin)) this._setDataStr(_g.d.ap_ar_resource._ap_code_begin, __custCode);
                    else
                        if (this._searchDialog._name.Equals(_g.d.ap_ar_resource._ap_code_end)) this._setDataStr(_g.d.ap_ar_resource._ap_code_end, __custCode);
            //
            SendKeys.Send("{TAB}");
        }

        void _arConditionControl__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ap_ar_resource._ar_code_begin) || name.Equals(_g.d.ap_ar_resource._ar_code_end))
            {
                string __arCode = this._getDataStr(name);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getItem = __myFrameWork._queryShort("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + __arCode + "\'").Tables[0];
                string __arName = "";
                if (__getItem.Rows.Count > 0)
                {
                    __arName = __getItem.Rows[0][_g.d.ar_customer._name_1].ToString();
                }
                this._setDataStr(name, __arCode, __arName, true);
            }
            else
                if (name.Equals(_g.d.ap_ar_resource._ap_code_begin) || name.Equals(_g.d.ap_ar_resource._ap_code_end))
                {
                    string __apCode = this._getDataStr(name);
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getItem = __myFrameWork._queryShort("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + __apCode + "\'").Tables[0];
                    string __arName = "";
                    if (__getItem.Rows.Count > 0)
                    {
                        __arName = __getItem.Rows[0][_g.d.ar_customer._name_1].ToString();
                    }
                    this._setDataStr(name, __apCode, __arName, true);
                }
        }

        void _arConditionControl__textBoxSearch(object sender)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)sender;
            this._searchDialog._name = __textBox._name;
            this._searchDialog.Text = __textBox._labelName;
            this._searchDialog.StartPosition = FormStartPosition.CenterScreen;
            this._searchDialog.ShowDialog();
        }
    }

    public static class _apAr
    {
        public static _apArEnum _apArCheck(_apArConditionEnum mode)
        {
            switch (mode)
            {
                case _apArConditionEnum.ลูกหนี้_เคลื่อนไหว:
                case _apArConditionEnum.ลูกหนี้_สถานะลูกหนี้:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยก_เอกสาร:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร:
                case _apArConditionEnum.ลูกหนี้_ยอดคงเหลือตามเอกสาร:
                    return _apArEnum.ลูกหนี้;
                default:
                    return _apArEnum.เจ้าหนี้;
            }
        }
    }

    public enum _apArEnum
    {
        ว่าง,
        ลูกหนี้,
        เจ้าหนี้
    }

    public enum _apArConditionEnum
    {
        ว่าง,
        ลูกหนี้_เคลื่อนไหว,
        ลูกหนี้_อายุลูกหนี้,
        ลูกหนี้_อายุลูกหนี้_แยก_เอกสาร,
        ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร,
        ลูกหนี้_สถานะลูกหนี้,
        ลูกหนี้_ยอดคงเหลือตามเอกสาร,
        เจ้าหนี้_เคลื่อนไหว,
        เจ้าหนี้_อายุเจ้าหนี้,
        เจ้าหนี้_อายุเจ้าหนี้_แยก_เอกสาร,
        เจ้าหนี้_อายุเจ้าหนี้_แยกลูกหนี้_แยกเอกสาร,
        เจ้าหนี้_สถานะเจ้าหนี้,
        เจ้าหนี้_ยอดคงเหลือตามเอกสาร,
        //
        debtor,
        early_debt_setup,
        early_debt_setup_cancel,
        early_debt_increase,
        early_debt_increase_cancel,
        early_debt_decrease,
        early_debt_decrease_cancel,
        other_debt_setup,
        other_debt_setup_cancel,
        other_debt_increase,
        other_debt_increase_cancel,
        other_debt_decrease,
        other_debt_decrease_cancel,
        billing_automatic,
        billing,
        billing_cancel,
        receipt_temp,
        receipt,
        receipt_cancel,
        debt_cut_off,
        absolute_period_debt_remain,
        absolute_check_balance,
        absolute_invoice_remain_pay,
        receipt_daily
    }
}
