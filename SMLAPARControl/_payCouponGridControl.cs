using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAPARControl
{
    public class _payCouponGridControl : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        string _searchName = "";
        string _search_data_full_name = "";
        //string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
        //combo box
        //object[] _cerdit_card_type = new object[] {_g.d.cb_credit_card._credit_type_visa, _g.d.cb_credit_card._credit_type_master };
        //ArrayList _credit_card_type_arraylist = new ArrayList();
        public delegate decimal _totalAmountEventargs();
        public event _totalAmountEventargs _getTotalAmount;

        public _payCouponGridControl()
        {
            this._table_name = _g.d.cb_trans_detail._table;
            this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 30, true, false, true, false, "", "", "", _g.d.cb_trans_detail._coupon_number);
            this._addColumn(_g.d.cb_trans_detail._balance_amount, 3, 1, 20, false, false, true, false, __formatNumber);
            this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 20, true, false, true, false, __formatNumber);
            this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 50, true, false, true);
            this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);

            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;

            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_payCouponGridControl__alterCellUpdate);

            this._totalCheck += (senser, row, column) =>
            {
                bool __result = true;
                int __columnNumber = this._findColumnByName(_g.d.cb_trans_detail._trans_number);
                if (this._cellGet(row, __columnNumber).ToString().Trim().Length == 0)
                {
                    __result = false;
                }
                return __result;
            };
            this._calcPersentWidthToScatter();
            this.Invalidate();
        }

        void _payCouponGridControl__alterCellUpdate(object sender, int row, int column)
        {
            int __columnCouponNumber = this._findColumnByName(_g.d.cb_trans_detail._trans_number);
            int __columnCouponAmount = this._findColumnByName(_g.d.cb_trans_detail._amount);
            if (column == __columnCouponNumber)
            {
                string __number = this._cellGet(row, __columnCouponNumber).ToString();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.coupon_list._amount + "," + _g.d.coupon_list._balance_amount + ", " + _g.d.coupon_list._date_expire + ", " + _g.d.coupon_list._last_status + "," + _g.d.coupon_list._coupon_type + "," + _g.d.coupon_list._single_use + ", case when coalesce((select doc_no from cb_trans_detail where trans_number= coupon_list.number and doc_type = 9  and last_status = 0 limit 1 ), '')='' then 0 else 1 end as used_status  from " + _g.d.coupon_list._table + " where " + _g.d.coupon_list._number + "=\'" + __number + "\'"));
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select sum(" + _g.d.cb_trans_detail._amount + ") as " + _g.d.cb_trans_detail._amount + " from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_type + "=9 and " + _g.d.cb_trans_detail._trans_number + "=\'" + __number + "\' and " + _g.d.cb_trans_detail._last_status + " =0"));
                __myquery.Append("</node>");
                string __debug_query = __myquery.ToString();
                ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                Boolean __found = false;
                decimal __amount = 0M;
                decimal __balance = 0M;
                if (__data.Count > 0)
                {
                    DataTable __getData = ((DataSet)__data[0]).Tables[0];
                    if (__getData.Rows.Count > 0)
                    {

                        DateTime __expireDate = MyLib._myGlobal._convertDateFromQuery(__getData.Rows[0][_g.d.coupon_list._date_expire].ToString());
                        DateTime __currentDate = DateTime.Today;
                        Boolean __is_single_use = __getData.Rows[0][_g.d.coupon_list._single_use].ToString().Equals("1") ? true : false;
                        int __used_status = MyLib._myGlobal._intPhase(__getData.Rows[0]["used_status"].ToString());

                        if (__currentDate > __expireDate)
                        {
                            // check expire date
                            __found = true;
                            MessageBox.Show("คูปองหมดอายุ ไปตั้งแต่ วันที่ " + __expireDate.ToLongDateString(), "คูปองหมดอายุ");
                            this._cellUpdate(row, __columnCouponNumber, "", false);
                        }
                        else if (MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.coupon_list._last_status].ToString()) == 1)
                        {
                            __found = true;
                            // check cancel 
                            MessageBox.Show("คูปอง " + __number + " ถูกยกเลิกใช้งาน", "คูปองยกเลิกใช้งาน");
                            this._cellUpdate(row, __columnCouponNumber, "", false);
                        }
                        else if (__is_single_use && __used_status == 1) 
                        {
                            MessageBox.Show("คูปอง " + __number + " ใช้ังานไปแล้ว", "คูปองใช้งานแล้ว");
                            this._cellUpdate(row, __columnCouponNumber, "", false);
                            __found = true;
                        }
                        else
                        {
                            __found = true;

                            if (__getData.Rows[0][_g.d.coupon_list._coupon_type].ToString().Equals("1"))
                            {
                                decimal __totalAmount = 0M;
                                if (_getTotalAmount != null)
                                {
                                    __totalAmount = _getTotalAmount();
                                }
                                // ลดตาม %
                                decimal __discountPercent = MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.coupon_list._amount].ToString());
                                decimal __afterDiscount = MyLib._myGlobal._calcAfterDiscount(__discountPercent.ToString() + "%", __totalAmount, _g.g._companyProfile._item_price_decimal);

                                decimal __discountAmount = (__totalAmount) - __afterDiscount;
                                __amount = __discountAmount;
                                __balance = __amount;
                            }
                            else
                            {
                                __amount = MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.coupon_list._amount].ToString());
                                __balance = MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.coupon_list._balance_amount].ToString());
                            }
                        }
                    }
                }
                if (__found == false)
                {
                    this._cellUpdate(row, __columnCouponNumber, "", false);
                    MessageBox.Show("ไม่พบคูปอง" + " : " + __number);
                }
                this._cellUpdate(row, this._findColumnByName(_g.d.cb_trans_detail._balance_amount), __balance, false);
                //this._cellUpdate(row, this._findColumnByName(_g.d.cb_trans_detail._amount), __amount, false);
                this._cellUpdate(row, this._findColumnByName(_g.d.cb_trans_detail._amount), __balance, false);
            }
            else if (column == __columnCouponAmount)
            {
                decimal __amount = (decimal)this._cellGet(row, __columnCouponAmount);
                decimal __balanceAmount = (decimal)this._cellGet(row, this._findColumnByName(_g.d.cb_trans_detail._balance_amount));

                if (__amount <= 0 || __amount > __balanceAmount)
                {
                    MessageBox.Show("ป้อนจำนวนเงินไม่สมบูรณ์ ", "ตรวจสอบ");
                    this._cellUpdate(row, __columnCouponAmount, 0M, false);
                }
            }

        }
    }
}
