using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icRecalc : UserControl
    {
        public _icRecalc()
        {
            InitializeComponent();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // ปรับปรุงอัตราส่วนหน่วยนับ
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_unit_use set stand_value=1 where stand_value=0 or stand_value is null");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_unit_use set divide_value=1 where divide_value=0 or divide_value is null");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set stand_value=1 where stand_value <> 1 and ic_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=0)");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set divide_value=1 where divide_value <> 1 and ic_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=0)");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set stand_value=(select stand_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code) " +
                        " where stand_value<>(select stand_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code) " +
                        " and ic_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=1)");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set divide_value=(select divide_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code) " +
                        " where divide_value<>(select divide_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code) " +
                        " and ic_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=1)");
                //
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set average_cost_1 = 0 where average_cost_1 is null ");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_wms_trans_detail set stand_value=(select stand_value from ic_unit_use where ic_unit_use.ic_code=ic_wms_trans_detail.item_code and ic_unit_use.code=ic_wms_trans_detail.unit_code) " +
                        " where stand_value<>(select stand_value from ic_unit_use where ic_unit_use.ic_code=ic_wms_trans_detail.item_code and ic_unit_use.code=ic_wms_trans_detail.unit_code) " +
                        " and ic_wms_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=1)");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_wms_trans_detail set divide_value=(select divide_value from ic_unit_use where ic_unit_use.ic_code=ic_wms_trans_detail.item_code and ic_unit_use.code=ic_wms_trans_detail.unit_code) " +
                        " where divide_value<>(select divide_value from ic_unit_use where ic_unit_use.ic_code=ic_wms_trans_detail.item_code and ic_unit_use.code=ic_wms_trans_detail.unit_code) " +
                        " and ic_wms_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=1)");

                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                string __query = "select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._item_type + "<>5 order by " + _g.d.ic_inventory._code;
                DataTable __item = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                ArrayList __itemList = new ArrayList();
                for (int __row = 0; __row < __item.Rows.Count; __row++)
                {
                    __itemList.Add(__item.Rows[__row][_g.d.ic_inventory._code].ToString());
                }
                

                if (__itemList.Count > 0)
                {
                    int __count = 0;
                    int __addr = 0;
                    ArrayList __itemListForCalc = new ArrayList();
                    while (__addr < __itemList.Count)
                    {
                        __itemListForCalc.Add(__itemList[__addr].ToString());
                        if (__count > 1000)
                        {
                            string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");
                            if (__resultStr.Length > 0)
                            {
                                MessageBox.Show(__resultStr);
                            }
                            __count = 0;
                            __itemListForCalc = new ArrayList();
                        }
                        __count++;
                        __addr++;
                        this._statusTextBox.Text = __addr.ToString() + "/" + __itemList.Count.ToString();
                        this._statusTextBox.Invalidate();
                        Application.DoEvents();
                    }
                    this._statusTextBox.Text = __itemList.Count.ToString() + "/" + __itemList.Count.ToString();
                    if (__itemListForCalc.Count > 0)
                    {
                        string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");
                        if (__resultStr.Length > 0)
                        {
                            MessageBox.Show(__resultStr);
                        }
                    }
                }
                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void vistaButton1_Click(object sender, EventArgs e)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._discount_amount + "=0 where " + _g.d.ic_trans_detail._discount_amount + " is null");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._price_exclude_vat + "=0 where " + _g.d.ic_trans_detail._price_exclude_vat + " is null");
                DataTable __getData = __myFrameWork._queryShort("select roworder," + "(select coalesce(" + _g.d.ic_trans._vat_type + ",0) from " + _g.d.ic_trans._table + " where " +
                    _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and " +
                    _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_date + " and " +
                    _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + ") as vat_type," +
                    _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._price_exclude_vat + ",coalesce(" + _g.d.ic_trans_detail._discount + ",\'\') as " + _g.d.ic_trans_detail._discount + "," +
                    _g.d.ic_trans_detail._total_vat_value + "," + _g.d.ic_trans_detail._discount_amount + "," + _g.d.ic_trans_detail._qty + " from " + _g.d.ic_trans_detail._table).Tables[0];
                for (int __row = 0; __row < __getData.Rows.Count; __row++)
                {
                    string __roworder = __getData.Rows[__row]["roworder"].ToString();
                    decimal __qty = MyLib._myGlobal._decimalPhase(__getData.Rows[__row][_g.d.ic_trans_detail._qty].ToString());
                    decimal __price = MyLib._myGlobal._decimalPhase(__getData.Rows[__row][_g.d.ic_trans_detail._price].ToString());
                    decimal __priceExcludeVat = MyLib._myGlobal._decimalPhase(__getData.Rows[__row][_g.d.ic_trans_detail._price_exclude_vat].ToString());
                    string __discountWord = __getData.Rows[__row][_g.d.ic_trans_detail._discount].ToString();
                    decimal __discountAmount = MyLib._myGlobal._decimalPhase(__getData.Rows[__row][_g.d.ic_trans_detail._discount_amount].ToString());
                    decimal __totalVatValue = MyLib._myGlobal._decimalPhase(__getData.Rows[__row][_g.d.ic_trans_detail._total_vat_value].ToString());
                    int __vatType = MyLib._myGlobal._intPhase(__getData.Rows[__row]["vat_type"].ToString());
                    Boolean __update = false;
                    if (__price != __priceExcludeVat)
                    {
                        __update = true;
                        __priceExcludeVat = (__vatType == 1) ? MyLib._myGlobal._round(__price * 100M / 107M, 3) : __price;
                    }
                    decimal __calcDiscount = MyLib._myGlobal._calcDiscountOnly(__price, __discountWord, MyLib._myGlobal._round(__qty * __price, _g.g._companyProfile._item_amount_decimal), _g.g._companyProfile._item_amount_decimal)._discountAmount;
                    if (__vatType == 1)
                    {
                        __calcDiscount = MyLib._myGlobal._round(__calcDiscount * 100M / 107M, 2);
                    }
                    decimal __totalVatValueCalc = ((__priceExcludeVat * __qty) - __calcDiscount) * (7M / 100M);
                    if (__totalVatValueCalc != __totalVatValue)
                    {
                        __totalVatValue = __totalVatValueCalc;
                        __update = true;
                    }
                    if (__discountAmount != __calcDiscount)
                    {
                        __discountAmount = __calcDiscount;
                        __update = true;
                    }
                    if (__update)
                    {
                        __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.ic_trans_detail._table + " set " +
                            _g.d.ic_trans_detail._total_vat_value + "=" + __totalVatValue.ToString() + "," +
                            _g.d.ic_trans_detail._price_exclude_vat + "=" + __priceExcludeVat.ToString() + "," +
                            _g.d.ic_trans_detail._discount_amount + "=" + __discountAmount.ToString() +
                            " where roworder=" + __roworder);
                    }
                }
                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _recheckFlowButton_Click(object sender, EventArgs e)
        {
            // recheck all doc flow

            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // ปรับปรุงอัตราส่วนหน่วยนับ

                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                //string __query = "select distinct trans_flag, count(*) as xcount from ic_trans where doc_no != '' group by trans_flag order by xcount ";

                string __query = "select doc_no from ic_trans where doc_no != '' order by doc_date";
                DataTable __item = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];

                int __rowCount = __item.Rows.Count;


                SMLProcess._docFlow __process = new SMLProcess._docFlow();
                ArrayList __itemList = new ArrayList();

                for (int __row = 0; __row < __item.Rows.Count; __row++)
                {
                    __itemList.Add(__item.Rows[__row][_g.d.ic_trans._doc_no].ToString());
                }

                if (__itemList.Count > 0)
                {
                    int __count = 0;
                    int __addr = 0;
                    ArrayList __itemListForCalc = new ArrayList();
                    while (__addr < __itemList.Count)
                    {

                        //string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");
                        __itemListForCalc.Add(__itemList[__addr].ToString());

                        if (__count > 1000)
                        {
                            //string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");

                            __process._processAll(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, "", _g.g._getItemRepack(__itemListForCalc));

                            /*if (__resultStr.Length > 0)
                            {
                                MessageBox.Show(__resultStr);
                            }*/
                            __count = 0;
                            __itemListForCalc = new ArrayList();
                        }
                        __count++;
                        __addr++;
                        this._statusTextBox.Text = __addr.ToString() + "/" + __itemList.Count.ToString();
                        this._statusTextBox.Invalidate();
                        Application.DoEvents();

                    }
                    if (__itemListForCalc.Count > 0)
                    {
                        __process._processAll(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, "", _g.g._getItemRepack(__itemListForCalc));
                    }
                }
                /*
                   ArrayList __itemList = new ArrayList();
                   for (int __row = 0; __row < __item.Rows.Count; __row++)
                   {
                       __itemList.Add(__item.Rows[__row][_g.d.ic_trans._doc_no].ToString());
                   }
                   if (__itemList.Count > 0)
                   {
                       int __count = 0;
                       int __addr = 0;
                       ArrayList __itemListForCalc = new ArrayList();
                       while (__addr < __itemList.Count)
                       {
                           __itemListForCalc.Add(__itemList[__addr].ToString());
                           if (__count > 1000)
                           {
                               string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");
                               if (__resultStr.Length > 0)
                               {
                                   MessageBox.Show(__resultStr);
                               }
                               __count = 0;
                               __itemListForCalc = new ArrayList();
                           }
                           __count++;
                           __addr++;
                           this._statusTextBox.Text = __addr.ToString() + "/" + __itemList.Count.ToString();
                           this._statusTextBox.Invalidate();
                           Application.DoEvents();
                       }
                       this._statusTextBox.Text = __itemList.Count.ToString() + "/" + __itemList.Count.ToString();
                       if (__itemListForCalc.Count > 0)
                       {
                           string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");
                           if (__resultStr.Length > 0)
                           {
                               MessageBox.Show(__resultStr);
                           }
                       }
                   }*/

                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _resetApArDocFlow_Click(object sender, EventArgs e)
        {
            // recheck ap ar flow

            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // ปรับปรุงอัตราส่วนหน่วยนับ

                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                //string __query = "select distinct trans_flag, count(*) as xcount from ic_trans where doc_no != '' group by trans_flag order by xcount ";

                string __query = "select doc_no from ap_ar_trans where doc_no != '' order by doc_date";
                DataTable __item = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];

                int __rowCount = __item.Rows.Count;


                SMLProcess._docFlow __process = new SMLProcess._docFlow();
                ArrayList __itemList = new ArrayList();

                for (int __row = 0; __row < __item.Rows.Count; __row++)
                {
                    __itemList.Add(__item.Rows[__row][_g.d.ic_trans._doc_no].ToString());
                }

                if (__itemList.Count > 0)
                {
                    int __count = 0;
                    int __addr = 0;
                    ArrayList __itemListForCalc = new ArrayList();
                    while (__addr < __itemList.Count)
                    {

                        //string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");
                        __itemListForCalc.Add(__itemList[__addr].ToString());

                        if (__count > 1000)
                        {
                            //string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");

                            __process._processAll(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, "", _g.g._getItemRepack(__itemListForCalc));

                            /*if (__resultStr.Length > 0)
                            {
                                MessageBox.Show(__resultStr);
                            }*/
                            __count = 0;
                            __itemListForCalc = new ArrayList();
                        }
                        __count++;
                        __addr++;
                        this._statusTextBox.Text = __addr.ToString() + "/" + __itemList.Count.ToString();
                        this._statusTextBox.Invalidate();
                        Application.DoEvents();

                    }
                    if (__itemListForCalc.Count > 0)
                    {
                        __process._processAll(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, "", _g.g._getItemRepack(__itemListForCalc));
                    }
                }


                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _testFlowButton_Click(object sender, EventArgs e)
        {
            try
            {
                string __queryGet = "select doc_no from ( " +
                    " select trans_flag , max(doc_no) as doc_no from ic_trans group by trans_flag " +
                    " union all " +
                    " select trans_flag, max(doc_no) as doc_no from ap_ar_trans group by trans_flag " +
                    ") as temp ";

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __item = __myFrameWork._query(MyLib._myGlobal._databaseName, __queryGet).Tables[0];

                int __rowCount = __item.Rows.Count;

                SMLProcess._docFlow __process = new SMLProcess._docFlow();
                ArrayList __itemList = new ArrayList();

                for (int __row = 0; __row < __item.Rows.Count; __row++)
                {
                    __itemList.Add(__item.Rows[__row][_g.d.ic_trans._doc_no].ToString());
                }

                if (__itemList.Count > 0)
                {
                    int __count = 0;
                    int __addr = 0;
                    ArrayList __itemListForCalc = new ArrayList();
                    while (__addr < __itemList.Count)
                    {
                        __itemListForCalc.Add(__itemList[__addr].ToString());

                        if (__count > 1000)
                        {
                            __process._processAll(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, "", _g.g._getItemRepack(__itemListForCalc));
                            __count = 0;
                            __itemListForCalc = new ArrayList();
                        }
                        __count++;
                        __addr++;
                        this._statusTextBox.Text = __addr.ToString() + "/" + __itemList.Count.ToString();
                        this._statusTextBox.Invalidate();
                        Application.DoEvents();
                    }
                    if (__itemListForCalc.Count > 0)
                    {
                        __process._processAll(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, "", _g.g._getItemRepack(__itemListForCalc));
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _recheckWmsFlowButton_Click(object sender, EventArgs e)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // ปรับปรุงอัตราส่วนหน่วยนับ

                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                //string __query = "select distinct trans_flag, count(*) as xcount from ic_trans where doc_no != '' group by trans_flag order by xcount ";

                string __query = "select doc_no from ic_wms_trans where doc_no != '' order by doc_date";
                DataTable __item = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];

                int __rowCount = __item.Rows.Count;


                SMLProcess._docFlow __process = new SMLProcess._docFlow();
                ArrayList __itemList = new ArrayList();

                for (int __row = 0; __row < __item.Rows.Count; __row++)
                {
                    __itemList.Add(__item.Rows[__row][_g.d.ic_trans._doc_no].ToString());
                }

                if (__itemList.Count > 0)
                {
                    int __count = 0;
                    int __addr = 0;
                    ArrayList __itemListForCalc = new ArrayList();
                    while (__addr < __itemList.Count)
                    {

                        //string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");
                        __itemListForCalc.Add(__itemList[__addr].ToString());

                        if (__count > 1000)
                        {
                            //string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemListForCalc), "*");

                            __process._processAll(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, "", _g.g._getItemRepack(__itemListForCalc));

                            /*if (__resultStr.Length > 0)
                            {
                                MessageBox.Show(__resultStr);
                            }*/
                            __count = 0;
                            __itemListForCalc = new ArrayList();
                        }
                        __count++;
                        __addr++;
                        this._statusTextBox.Text = __addr.ToString() + "/" + __itemList.Count.ToString();
                        this._statusTextBox.Invalidate();
                        Application.DoEvents();

                    }
                    if (__itemListForCalc.Count > 0)
                    {
                        __process._processAll(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, "", _g.g._getItemRepack(__itemListForCalc));
                    }
                }


                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }
}
