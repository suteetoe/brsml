using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSChecker
{
    public partial class _checkVatControl : UserControl
    {
        string _totalVatField = "total_vat";
        string _totalNoVatField = "total_no_vat";
        string _total_except_vat_sum = "total_except_vat_sum";
        string _new_total_before_vat = "new_total_before_vat";
        string _new_total_after_vat = "new_total_after_vat";
        string _newdiscountValueField = "total_new_discount";
        public _checkVatControl()
        {
            InitializeComponent();


            this._gridVatProblem._width_by_persent = true;
            this._gridVatProblem._table_name = _g.d.ic_trans._table;
            this._gridVatProblem._addColumn("check", 11, 10, 5);
            this._gridVatProblem._addColumn(_g.d.ic_trans._doc_no, 1, 10, 15);
            this._gridVatProblem._addColumn(_g.d.ic_trans._doc_date, 4, 10, 10);
            this._gridVatProblem._addColumn(_g.d.ic_trans._cust_code, 1, 10, 20);

            this._gridVatProblem._addColumn(_g.d.ic_trans._total_value, 3, 10, 10, false, false, true, false, "#,###,###.00");

            this._gridVatProblem._addColumn(_totalVatField, 3, 10, 10, false, false, true, false, "#,###,###.00");
            this._gridVatProblem._addColumn(_totalNoVatField, 3, 10, 10, false, false, true, false, "#,###,###.00");
            this._gridVatProblem._addColumn(_g.d.ic_trans._total_discount, 3, 10, 10, false, false, true, false, "#,###,###.00");

            this._gridVatProblem._addColumn(_g.d.ic_trans._total_before_vat, 3, 10, 10, false, false, true, false, "#,###,###.00");
            this._gridVatProblem._addColumn(_g.d.ic_trans._total_after_vat, 3, 10, 10, false, false, true, false, "#,###,###.00");
            this._gridVatProblem._addColumn(_g.d.ic_trans._total_except_vat, 3, 10, 10, false, false, true, false, "#,###,###.00");
            this._gridVatProblem._addColumn(_g.d.ic_trans._total_vat_value, 3, 10, 10, false, false, true, false, "#,###,###.00");

            this._gridVatProblem._addColumn(_newdiscountValueField, 3, 10, 10, false, false, true, false, "#,###,###.00");

            this._gridVatProblem._addColumn(_g.d.ic_trans._ref_new_amount, 3, 10, 10, false, false, true, false, "#,###,###.00");
            this._gridVatProblem._addColumn(_new_total_before_vat, 3, 10, 10, false, false, true, false, "#,###,###.00");

            // ซ่อน
            this._gridVatProblem._addColumn(_g.d.ic_trans._vat_type, 2, 10, 10, false, true, true, false);
            this._gridVatProblem._addColumn(_g.d.ic_trans._advance_amount, 3, 10, 10, false, true, true, false);
            this._gridVatProblem._addColumn(_g.d.ic_trans._vat_rate, 3, 10, 10, false, true, true, false);
            this._gridVatProblem._addColumn(_new_total_after_vat, 3, 10, 10, false, true, true, false, "#,###,###.00");

            this._gridVatProblem._calcPersentWidthToScatter();

            this._gridVatProblem._isEdit = false;

            // set date
            DateTime firstDayOfTheMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfTheMonth = firstDayOfTheMonth.AddMonths(1).AddDays(-1);

            this._screen_check_vat1._setDataDate(_g.d.resource_report._from_date, firstDayOfTheMonth);
            this._screen_check_vat1._setDataDate(_g.d.resource_report._to_date, lastDayOfTheMonth);
        }

        private void _checkButton_Click(object sender, EventArgs e)
        {
            bool __selectAll = this._screen_check_vat1._getDataStr(_g.d.resource_report._all).Equals("1") ? true : false;

            string __query = "select cust_code, doc_no, doc_date" +
                ", vat_rate, vat_type, total_value, advance_amount, total_discount, total_vat_value, total_before_vat, total_after_vat, total_amount, total_except_vat " +
                ", coalesce((select sum(sum_amount) from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag and ic_trans_detail.tax_type = 0 and item_type not in (3, 5) ), 0) as " + _totalVatField + // total_vat
                ", coalesce((select sum(sum_amount) from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag and ic_trans_detail.tax_type = 1 and item_type not in (3, 5) ), 0) as " + _totalNoVatField + // total_no_vat
                ", (select sum(sum_amount_exclude_vat) from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag ) as " + _total_except_vat_sum +
                " from ic_trans where trans_flag = 44 and is_pos = 1 " + ((__selectAll == true) ? "" : " and total_discount > 0 ") + "  and doc_date between " + this._screen_check_vat1._getDataStrQuery(_g.d.resource_report._from_date) + " and " + this._screen_check_vat1._getDataStrQuery(_g.d.resource_report._to_date) + " " +
                " order by doc_date, doc_no ";
            // ยังไม่ต้องเช็ค " and total_before_vat <> (select sum(sum_amount_exclude_vat) from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag ) ";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort(__query);

            this._gridVatProblem._loadFromDataTable(__result.Tables[0]);

            // check vat

            for (int __i = 0; __i < this._gridVatProblem._rowData.Count; __i++)
            {
                decimal __transTotalValue = MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _g.d.ic_trans._total_value).ToString());

                decimal __totalValueVat = MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _totalVatField).ToString());
                decimal __totalValueNoVat = MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _totalNoVatField).ToString());
                decimal __totalAdvance = MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _g.d.ic_trans._advance_amount).ToString());

                decimal __totalValue = __totalValueVat + __totalValueNoVat;
                decimal __totalDiscount = MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _g.d.ic_trans._total_discount).ToString()); // __totalValue - MyLib._myGlobal._calcAfterDiscount(this._getDataStr(_g.d.ic_trans._discount_word), __totalValue, _g.g._companyProfile._item_amount_decimal);

                if (__transTotalValue != (__totalValueVat + __totalValueNoVat))
                {
                    decimal __newDiscount = __totalDiscount - (__transTotalValue - (__totalValueVat + __totalValueNoVat));

                    this._gridVatProblem._cellUpdate(__i, _newdiscountValueField, __newDiscount, true);
                    __totalDiscount = __newDiscount;
                }




                decimal __vatRate = MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _g.d.ic_trans._vat_rate).ToString());  //this._getDataNumber(_g.d.ic_trans._vat_rate);
                decimal __beforeVat = 0;
                decimal __vatValue = 0;
                decimal __afterVat = 0;
                decimal __totalAmount = 0;

                int __vat_type = (int)MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _g.d.ic_trans._vat_type).ToString());
                switch (__vat_type)
                {
                    case 0: // _g.g._vatTypeEnum.ภาษีแยกนอก:
                        {
                            __beforeVat = (__totalValueVat - __totalDiscount) - __totalAdvance;
                            __vatValue = MyLib._myGlobal._round(__beforeVat * (__vatRate / 100.0M), _g.g._companyProfile._item_amount_decimal);
                            __afterVat = __beforeVat + __vatValue;
                            __totalAmount = __totalValueNoVat + __afterVat;
                        }
                        break;
                    case 1: // _g.g._vatTypeEnum.ภาษีรวมใน:
                        {
                            // toe
                            if (_g.g._companyProfile._discount_type == 1)
                            {
                                // ลดกอน vat
                                __totalAmount = (__totalValue - __totalDiscount) - __totalAdvance;
                                __beforeVat = MyLib._myGlobal._round(((__totalValueVat - (__totalDiscount + __totalAdvance)) * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                __vatValue = MyLib._myGlobal._round((__totalValueVat - (__totalDiscount + __totalAdvance)) - __beforeVat, _g.g._companyProfile._item_amount_decimal);
                                __afterVat = __beforeVat + __vatValue;
                            }
                            else
                            {
                                /// แบบเดิม
                                __totalAmount = (__totalValue - __totalDiscount) - __totalAdvance;
                                //__beforeVat = MyLib._myGlobal._round(((__totalValueVat) * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                __beforeVat = MyLib._myGlobal._round(((__totalValueVat - __totalAdvance) * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
                                __vatValue = MyLib._myGlobal._round((__totalValueVat - __totalAdvance) - __beforeVat, _g.g._companyProfile._item_amount_decimal);
                                __afterVat = __beforeVat + __vatValue;
                            }
                        }
                        break;
                    case 2: // _g.g._vatTypeEnum.ยกเว้นภาษี:
                        __vatValue = 0;
                        __totalAmount = (__totalValue - __totalDiscount) - __totalAdvance;
                        break;
                }
                //
                //this._setDataNumber(_g.d.ic_trans._total_value, __totalValueVat + __totalValueNoVat);
                //this._setDataNumber(_g.d.ic_trans._total_discount, __totalDiscount);
                //this._setDataNumber(_g.d.ic_trans._total_before_vat, __beforeVat);
                //this._setDataNumber(_g.d.ic_trans._total_vat_value, __vatValue);
                //this._setDataNumber(_g.d.ic_trans._total_after_vat, __afterVat);
                //this._setDataNumber(_g.d.ic_trans._total_except_vat, __totalValueNoVat);
                //this._setDataNumber(_g.d.ic_trans._total_amount, __totalAmount);

                // check vat
                decimal __old_vat = MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _g.d.ic_trans._total_vat_value).ToString());
                if (__old_vat != __vatValue)
                {
                    this._gridVatProblem._cellUpdate(__i, "check", 1, true);

                    this._gridVatProblem._cellUpdate(__i, _g.d.ic_trans._ref_new_amount, __vatValue, true);
                    this._gridVatProblem._cellUpdate(__i, _new_total_before_vat, __beforeVat, true);
                    this._gridVatProblem._cellUpdate(__i, _new_total_after_vat, __afterVat, true);

                }

                if (__transTotalValue != (__totalValueVat + __totalValueNoVat))
                {
                    this._gridVatProblem._cellUpdate(__i, "check", 1, true);

                }
            }
        }

        private void _updateVatButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ยืนยันการ เปลี่ยนแปลงข้อมูล", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //bool __selectAll = this._screen_check_vat1._getDataStr(_g.d.resource_report._all).Equals("1") ? true : false;

                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");

                // write query update 
                for (int __i = 0; __i < this._gridVatProblem._rowData.Count; __i++)
                {
                    if (this._gridVatProblem._cellGet(__i, "check").ToString().Equals("1"))
                    {
                        string __total_before_vat_data = this._gridVatProblem._cellGet(__i, _new_total_before_vat).ToString();
                        string __new_vat_value = this._gridVatProblem._cellGet(__i, _g.d.ic_trans._ref_new_amount).ToString();
                        string __new_total_after_vat = this._gridVatProblem._cellGet(__i, _new_total_after_vat).ToString();

                        string __doc_no = this._gridVatProblem._cellGet(__i, _g.d.ic_trans._doc_no).ToString();
                        string __doc_date = MyLib._myGlobal._convertDateToQuery((DateTime)this._gridVatProblem._cellGet(__i, _g.d.ic_trans._doc_date));

                        string __total_new_discount = this._gridVatProblem._cellGet(__i, _newdiscountValueField).ToString();

                        decimal __totalValueVat = MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _totalVatField).ToString());
                        decimal __totalValueNoVat = MyLib._myGlobal._decimalPhase(this._gridVatProblem._cellGet(__i, _totalNoVatField).ToString());

                        string __updateStr = "";

                        if (MyLib._myGlobal._decimalPhase(__new_vat_value) > 0M)
                        {
                            __updateStr = _g.d.ic_trans._total_before_vat + "=\'" + __total_before_vat_data + "\', " + _g.d.ic_trans._total_vat_value + "=\'" + __new_vat_value + "\', " + _g.d.ic_trans._total_after_vat + "=\'" + __new_total_after_vat + "\' ";
                        }

                        if (MyLib._myGlobal._decimalPhase(__total_new_discount) > 0M)
                        {
                            __updateStr += ((__updateStr.Length> 0) ? ", " : "") + _g.d.ic_trans._total_value + "=\'" + (__totalValueVat + __totalValueNoVat) + "\', " + _g.d.ic_trans._total_discount + "=\'" + __total_new_discount + "\', " + _g.d.ic_trans._discount_word + "=\'" + __total_new_discount + "\' ";
                        }

                        //if (MyLib._myGlobal._decimalPhase(__new_vat_value) > 0)
                        //{
                        // ic_trans

                        if (__updateStr.Length > 0)
                        {

                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set " + __updateStr + " where doc_no = \'" + __doc_no + "\' and doc_date = \'" + __doc_date + "\' and trans_flag = 44 and is_pos = 1 "));

                            // gl_journal_vat
                            //string __vatFieldList = _g.d.gl_journal_vat_sale._book_code + "," + _g.d.gl_journal_vat_sale._vat_calc + "," + _g.d.gl_journal_vat_sale._trans_type + "," + _g.d.gl_journal_vat_sale._trans_flag + "," + _g.d.gl_journal_vat_sale._doc_date + "," + _g.d.gl_journal_vat_sale._doc_no + "," + _g.d.gl_journal_vat_sale._ar_code + ",";
                            //string __vatDataList = "\'\'," + _g.g._transTypeGlobal._vatSaleType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ",2," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + ",\'" + __docDate + "\',\'" + __docNo + "\',\'" + _custCode + "\',";
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_vat_sale._table + " set " + _g.d.gl_journal_vat_sale._amount + "=" + __new_vat_value + "," + _g.d.gl_journal_vat_sale._base_caltax_amount + "=" + __total_before_vat_data + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + __doc_no + "\' and " + _g.d.gl_journal_vat_sale._doc_date + "=\'" + __doc_date + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=44"));
                            //}
                        }
                    }

                }
                __query.Append("</node>");

                string __debug_query = __query.ToString();

                MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();

                string __result = __myFramework._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Length == 0)
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show(__result);
                }

            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    public class _screen_check_vat : MyLib._myScreen
    {
        public _screen_check_vat()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.resource_report._table;
            this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
            this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
            this._addCheckBox(1, 0, _g.d.resource_report._all, true, false);
        }
    }
}
