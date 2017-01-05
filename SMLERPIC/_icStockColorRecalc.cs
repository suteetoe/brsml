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
    public partial class _icStockColorRecalc : UserControl
    {
        public _icStockColorRecalc()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            ArrayList __itemForProcess = new ArrayList();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            // ดึงใบรับสำเร็จรูป เฉพาะ สีผสม item_type=6 
            string __getDocQuery = "select roworder," + _g.d.ic_trans_detail._sum_of_cost + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._qty + " from " + _g.d.ic_trans_detail._table +
                " where " + _g.d.ic_trans._doc_date + ">=" + this._conditionScreen._getDataStrQuery(_g.d.ic_resource._date_begin) +
                " and " + _g.d.ic_trans_detail._doc_date + "<=" + this._conditionScreen._getDataStrQuery(_g.d.ic_resource._date_end) + " and " + _g.d.ic_trans_detail._trans_flag + "=60" +
                " and " + _g.d.ic_trans_detail._item_code + " in (select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._item_type + "=6)";
            DataTable __getDocTable = __myFrameWork._queryShort(__getDocQuery).Tables[0];
            for (int __docLoop = 0; __docLoop < __getDocTable.Rows.Count; __docLoop++)
            {
                string __roworder = __getDocTable.Rows[__docLoop]["roworder"].ToString();
                string __docNoLoop = __getDocTable.Rows[__docLoop][_g.d.ic_trans_detail._doc_no].ToString();
                string __itemCodeLoop = __getDocTable.Rows[__docLoop][_g.d.ic_trans_detail._item_code].ToString();
                decimal __sumOfCostLoop = MyLib._myGlobal._decimalPhase(__getDocTable.Rows[__docLoop][_g.d.ic_trans_detail._sum_of_cost].ToString());
                decimal __qty = MyLib._myGlobal._decimalPhase(__getDocTable.Rows[__docLoop][_g.d.ic_trans_detail._qty].ToString());
                Boolean __foundItemForProcess = false;
                for (int __loop = 0; __loop < __itemForProcess.Count; __loop++)
                {
                    if (__itemForProcess[__loop].ToString().Equals(__itemCodeLoop))
                    {
                        __foundItemForProcess = true;
                        break;
                    }
                }
                if (__foundItemForProcess == false)
                {
                    __itemForProcess.Add(__itemCodeLoop);
                }
                // ดึงบิลใบเบิกที่สร้างไว้
                string __queryCost = "select coalesce(sum(" + _g.d.ic_trans_detail._sum_of_cost + "),0) as " + _g.d.ic_trans_detail._sum_of_cost + " from " + _g.d.ic_trans_detail._table +
                    " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNoLoop + "\' and " + _g.d.ic_trans_detail._item_code_main + "=\'" + __itemCodeLoop + "\'";
                DataTable __getCostTable = __myFrameWork._queryShort(__queryCost).Tables[0];
                decimal __sumOfCost = 0.0M;
                if (__getCostTable.Rows.Count > 0)
                {
                    __sumOfCost = MyLib._myGlobal._decimalPhase(__getCostTable.Rows[0][_g.d.ic_trans_detail._sum_of_cost].ToString());
                }
                if (__sumOfCost != __sumOfCostLoop)
                {
                    decimal __price = (__qty == 0) ? 0 : (__sumOfCost / __qty);
                    string __queryUpdate = "update " + _g.d.ic_trans_detail._table + " set " +
                        _g.d.ic_trans_detail._sum_of_cost + "=" + __sumOfCost.ToString() + "," +
                        _g.d.ic_trans_detail._sum_amount + "=" + __sumOfCost.ToString() + "," +
                        _g.d.ic_trans_detail._sum_amount_exclude_vat + "=" + __sumOfCost.ToString() + "," +
                        _g.d.ic_trans_detail._price + "=" + __price.ToString() +
                        " where roworder=" + __roworder.ToString();
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __queryUpdate);
                }
                // รวมยอด
                string __querySumUpdate = "update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._total_amount + "=coalesce((select sum(" + _g.d.ic_trans_detail._sum_of_cost + ") from " + _g.d.ic_trans_detail._table + " where " +
                    _g.d.ic_trans_detail._trans_flag + "=60 and " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNoLoop + "\'),0) where " + _g.d.ic_trans._doc_no + "=\'" + __docNoLoop + "\' and " + _g.d.ic_trans._trans_flag + "=60";
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __querySumUpdate);
            }
            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
            string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(__itemForProcess), "*");
            //
            MessageBox.Show("Success.");
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    public class _icStockColorRecalcConditionScreen : MyLib._myScreen
    {
        public _icStockColorRecalcConditionScreen()
        {
            this._table_name = _g.d.ic_resource._table;
            this._maxColumn = 2;
            this._addDateBox(0, 0, 0, 0, _g.d.ic_resource._date_begin, 1, true);
            this._addDateBox(0, 1, 0, 0, _g.d.ic_resource._date_end, 1, true);
            //
            DateTime __today = DateTime.Now;
            DateTime __dateBegin = __today.AddDays(-10);
            this._setDataDate(_g.d.ic_resource._date_begin, new DateTime(__dateBegin.Year, __dateBegin.Month, __dateBegin.Day));
            this._setDataDate(_g.d.ic_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
        }
    }
}
