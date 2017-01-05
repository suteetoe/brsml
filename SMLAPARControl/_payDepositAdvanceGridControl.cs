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
    public class _payDepositAdvanceGridControl : MyLib._myGrid
    {
        public delegate void _refreshDataEvent(_payDepositAdvanceGridControl sender);
        public delegate string _getCustCodeEvent();
        public delegate DateTime _getProcessDateEvent();
        public event _refreshDataEvent _refreshData;
        public event _getCustCodeEvent _getCustCode;
        public event _getProcessDateEvent _getProcessDate;
        //
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
        public _g.g._depositAdvanceEnum _mode;
        private _selectDepositAdvanceForm _depositAdvance;

        public _payDepositAdvanceGridControl()
        {
        }

        public void _build(_g.g._depositAdvanceEnum mode)
        {
            this._mode = mode;
            //
            this._depositAdvance = new _selectDepositAdvanceForm(mode);
            this._depositAdvance._processButton.Click += new EventHandler(_processButton_Click);
            //
            string __docNoResourceName = "";
            string __amountResourceName = "";
            string __payResourceName = "";
            switch (mode)
            {
                case _g.g._depositAdvanceEnum.รับเงินล่วงหน้า:
                    __docNoResourceName = _g.d.cb_trans_detail._deposit_no;
                    __amountResourceName = _g.d.cb_trans_detail._deposit_amount;
                    __payResourceName = _g.d.cb_trans_detail._deposit_pay;
                    break;
                case _g.g._depositAdvanceEnum.รับเงินมัดจำ:
                    __docNoResourceName = _g.d.cb_trans_detail._advance_no;
                    __amountResourceName = _g.d.cb_trans_detail._advance_amount;
                    __payResourceName = _g.d.cb_trans_detail._advance_pay;
                    break;
                case _g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า:
                    __docNoResourceName = _g.d.cb_trans_detail._deposit_no;
                    __amountResourceName = _g.d.cb_trans_detail._deposit_amount;
                    __payResourceName = _g.d.cb_trans_detail._deposit_pay;
                    break;
                case _g.g._depositAdvanceEnum.จ่ายเงินมัดจำ:
                    __docNoResourceName = _g.d.cb_trans_detail._advance_no;
                    __amountResourceName = _g.d.cb_trans_detail._advance_amount;
                    __payResourceName = _g.d.cb_trans_detail._advance_pay;
                    break;
            }
            this._table_name = _g.d.cb_trans_detail._table;
            this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 20, true, false, true, true, "", "", "", __docNoResourceName);
            this._addColumn(_g.d.cb_trans_detail._doc_date_ref, 4, 1, 15, false, false, true);
            this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 15, false, false, true, false, __formatNumber, "", "", __amountResourceName);
            this._addColumn(_g.d.cb_trans_detail._balance_amount, 3, 1, 15, false, false, true, false, __formatNumber, "", "", _g.d.cb_trans_detail._balance_amount);
            this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 15, true, false, true, false, __formatNumber, "", "", __payResourceName);
            this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 20, true, false, true);
            this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = true;
            this._total_show = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;

            this._clickSearchButton += new MyLib.SearchEventHandler(_pay_credit__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_pay_credit__alterCellUpdate);
            this._afterRemoveRow += _payDepositAdvanceGridControl__afterRemoveRow;
            this.Invalidate();
        }

        private void _payDepositAdvanceGridControl__afterRemoveRow(object sender)
        {
            if (this._refreshData != null)
            {
                this._refreshData(this);
            }
        }

        void _processButton_Click(object sender, EventArgs e)
        {
            this._depositAdvance.Close();
            // เพิ่มบรรทัดใหม่
            for (int __row = 0; __row < this._depositAdvance._resultGrid._rowData.Count; __row++)
            {
                // ลบบรรทัดที่ว่าง
                int __rowDelete = 0;
                while (__rowDelete < this._rowData.Count)
                {
                    if (this._cellGet(__rowDelete, _g.d.cb_trans_detail._trans_number).ToString().Trim().Length == 0)
                    {
                        this._rowData.RemoveAt(__rowDelete);
                    }
                    else
                    {
                        __rowDelete++;
                    }
                }
                if ((int)this._depositAdvance._resultGrid._cellGet(__row, _g.d.ap_ar_resource._select) == 1)
                {
                    int __rowAddr = this._addRow();
                    this._cellUpdate(__rowAddr, _g.d.cb_trans_detail._trans_number, this._depositAdvance._resultGrid._cellGet(__row, _g.d.ap_ar_resource._doc_no).ToString(), true);
                }
            }
            this._gotoCell(this._rowData.Count, this._findColumnByName(_g.d.cb_trans_detail._trans_number));
        }

        void _pay_credit__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._findColumnByName(_g.d.cb_trans_detail._trans_number))
            {
                SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
                string __docNo = this._cellGet(row, column).ToString();
                DataTable __getData = __process._depositBalanceDoc(this._mode, "", 0, this._getCustCode(), this._getCustCode(), __docNo, __docNo, this._getProcessDate(), "");
                if (__getData.Rows.Count > 0)
                {
                    DataRow __dataRow = __getData.Rows[0];
                    this._cellUpdate(row, _g.d.cb_trans_detail._doc_date_ref, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._doc_date].ToString()), false);
                    this._cellUpdate(row, _g.d.cb_trans_detail._sum_amount, __dataRow[_g.d.ap_ar_resource._amount], false);
                    this._cellUpdate(row, _g.d.cb_trans_detail._balance_amount, __dataRow[_g.d.ap_ar_resource._balance_amount], false);
                    this._cellUpdate(row, _g.d.cb_trans_detail._amount, __dataRow[_g.d.ap_ar_resource._balance_amount], false);
                    this.Invalidate();
                }
            }
            if (column == this._findColumnByName(_g.d.cb_trans_detail._amount))
            {
                decimal __balanceAmount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._balance_amount).ToString());
                decimal __amount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._amount).ToString());
                if (__amount > __balanceAmount)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ยอดตัดจ่ายมากกว่ายอดคงเหลือ"), MyLib._myGlobal._resource("ผิดพลาด"));
                    this._cellUpdate(row, _g.d.cb_trans_detail._amount, __balanceAmount, false);
                }
            }
            if (this._refreshData != null)
            {
                this._refreshData(this);
            }
        }

        void _pay_credit__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.Equals(_g.d.cb_trans_detail._trans_number))
            {
                string __getCustCode = this._getCustCode();
                if (__getCustCode.Length  == 0) {
                    MessageBox.Show(MyLib._myGlobal._resource("กรุณาเลือกลูกค้า"));
                    return;
                }
                this._depositAdvance._process(__getCustCode, this._getProcessDate());
                this._depositAdvance.ShowDialog();
            }
        }
    }
}
