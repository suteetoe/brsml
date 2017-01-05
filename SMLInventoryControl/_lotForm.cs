using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _lotForm : Form
    {
        private _icTransItemGridControl _grid;
        private int _row = 0;
        private string __formatNumberQty = _g.g._getFormatNumberStr(1);
        bool __onLoad = false;
        _g.g._transControlTypeEnum _mode;

        public _lotForm(_g.g._transControlTypeEnum mode)
        {
            InitializeComponent();

            if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
            {
                // renew location expire 
                this.label3.Location = new System.Drawing.Point(7, 35);
                this.label2.Location = new System.Drawing.Point(7, 63);

                this._expireDate.Location = new System.Drawing.Point(112, 56);
                this._mfdDate.Location = new System.Drawing.Point(112, 31);

                this._mfdDate.TabIndex = 3;

            }

            this._mode = mode;
            if (_g.g._transCalcTypeGlobal._transStockCalcType(this._mode) == 1 && this._mode != _g.g._transControlTypeEnum.ขาย_สั่งขาย)
            {
                // ขาเข้า disabled grid
                this._resultGrid.Enabled = false;
            }
            else
            {
                // ขาออก disabled textbox
                this._myPanel2.Enabled = false;
            }
            //
            this._resultGrid._table_name = _g.d.ic_resource._table;
            if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
            {
                this._resultGrid._addColumn("wh_code", 1, 10, 8, false, false, false, false, "", "", "", _g.d.ic_resource._warehouse);
                this._resultGrid._addColumn("shelf_code", 1, 10, 8, false, false, false, false, "", "", "", _g.d.ic_resource._location);

            }
            this._resultGrid._addColumn(_g.d.ic_resource._lot_number, 1, 20, 10);
            this._resultGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);


            // hidden
            this._resultGrid._addColumn(_g.d.ic_resource._date_expire, 4, 10, 8, false, true, false);
            this._resultGrid._addColumn(_g.d.ic_trans_detail_lot._mfd_date, 4, 10, 8, false, true, false);
            this._resultGrid._addColumn(_g.d.ic_trans_detail_lot._mfn_name, 1, 10, 8, false, true, false);


            this._resultGrid._calcPersentWidthToScatter();
            this._resultGrid._mouseClick += new MyLib.MouseClickHandler(_resultGrid__mouseClick);
            //
            this._expireDate._afterSelectCalendar += new MyLib.AfterSelectCalendarHandler(_expireDate__afterSelectCalendar);
            this._lotNumber.TextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            this._expireDate.TextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            this._mfdDate.textBox.TextChanged += TextBox_TextChanged;
            this._mfnTextbox.textBox.TextChanged += TextBox_TextChanged;
        }

        void _resultGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._lotNumber.TextBox.Text = this._resultGrid._cellGet(e._row, _g.d.ic_resource._lot_number).ToString();

            this._mfnTextbox.textBox.Text = this._resultGrid._cellGet(e._row, _g.d.ic_trans_detail_lot._mfn_name).ToString();
            if (this._resultGrid._cellGet(e._row, _g.d.ic_resource._date_expire) != null && this._resultGrid._cellGet(e._row, _g.d.ic_resource._date_expire).ToString().Length > 0)
            {
                this._expireDate._dateTime = MyLib._myGlobal._convertDateFromQuery(this._resultGrid._cellGet(e._row, _g.d.ic_resource._date_expire).ToString());
                this._expireDate._dateTimeOld = this._expireDate._dateTime;
                this._expireDate._refresh();
            }

            if (this._resultGrid._cellGet(e._row, _g.d.ic_trans_detail_lot._mfd_date) != null && this._resultGrid._cellGet(e._row, _g.d.ic_trans_detail_lot._mfd_date).ToString().Length > 0)
            {
                this._mfdDate._dateTime = MyLib._myGlobal._convertDateFromQuery(this._resultGrid._cellGet(e._row, _g.d.ic_trans_detail_lot._mfd_date).ToString());
                this._mfdDate._dateTimeOld = this._mfdDate._dateTime;
                this._mfdDate._refresh();
            }


            // update other
        }

        void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (__onLoad == false)
            {
                this._save();
            }
        }

        void _expireDate__afterSelectCalendar(DateTime e)
        {
            this._save();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                return false;
            }
            this._save();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _save()
        {
            if (this._row > -1)
            {
                try
                {
                    this._grid._cellUpdate(this._row, _g.d.ic_trans_detail._lot_number_1, this._lotNumber.textBox.Text, false);
                    if (this._expireDate._checkDate(false, false))
                    {
                        this._grid._cellUpdate(this._row, _g.d.ic_trans_detail._date_expire, this._expireDate._dateTime, false);
                    }
                    if (this._mfdDate._checkDate(false, false))
                    {
                        this._grid._cellUpdate(this._row, _g.d.ic_trans_detail._mfd_date, this._mfdDate._dateTime, false);
                    }
                    this._grid._cellUpdate(this._row, _g.d.ic_trans_detail._mfn_name, this._mfnTextbox.textBox.Text, false);
                    this._grid.Invalidate();
                }
                catch
                {
                }
            }
        }

        public void _load(_icTransItemGridControl grid, int row, DateTime date)
        {
            this._grid = grid;
            this._row = row;
            this.__onLoad = true;
            if (this._row > -1)
            {
                string __itemCode = this._grid._cellGet(this._row, _g.d.ic_trans_detail._item_code).ToString().Trim();
                if (__itemCode.Length > 0)
                {
                    string __itemName = this._grid._cellGet(this._row, _g.d.ic_trans_detail._item_name).ToString();
                    int __costType = (int)this._grid._cellGet(this._row, grid._columnCostType);
                    if (__costType == 2 || __costType == 3)
                    {
                        this.Enabled = true;
                        // Lot กำหนดเอง,หมดอายุ
                        this._lotNumber.Enabled = !(__costType == 3);
                        this._expireDate.Enabled = true; // (__costType == 3);
                        this._text.Text = "Line : " + this._grid._selectRow + " Code : " + __itemCode + " Name : " + __itemName;
                        this._lotNumber._textFirst = this._grid._cellGet(this._row, _g.d.ic_trans_detail._lot_number_1).ToString();

                        try
                        {
                            string __date = this._grid._cellGet(this._row, _g.d.ic_trans_detail._date_expire).ToString();
                            IFormatProvider __cultureEng = new CultureInfo("en-US");
                            DateTime __getDate = Convert.ToDateTime(__date, __cultureEng);
                            IFormatProvider __cultureThai = MyLib._myGlobal._cultureInfo();
                            this._expireDate.textBox.Text = this._expireDate._textFirst = this._expireDate._textLast = __getDate.ToString("d/M/yyyy", __cultureThai);
                            this._expireDate._textSecond = "";
                            this._expireDate._checkDate(true, true);


                        }
                        catch
                        {
                            this._expireDate.textBox.Text =
                            this._expireDate._textFirst =
                            this._expireDate._textSecond =
                            this._expireDate._textLast = "";
                            this._expireDate._dateTime = new DateTime(1000, 1, 1);
                            this._expireDate._dateTimeOld = this._expireDate._dateTime;
                            this._expireDate._refresh();
                        }

                        //if ((MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims"))
                        //{
                        this._mfnTextbox._textFirst = this._grid._cellGet(this._row, _g.d.ic_trans_detail._mfn_name).ToString();
                        try
                        {
                            string __getMFDDate = this._grid._cellGet(this._row, _g.d.ic_trans_detail._mfd_date).ToString();
                            DateTime __getDateFromGrid = (DateTime)this._grid._cellGet(this._row, _g.d.ic_trans_detail._mfd_date);
                            DateTime __defaultDate = new DateTime(1000, 1, 1);
                            if ((__getDateFromGrid == __defaultDate) == false)
                            {
                                IFormatProvider __cultureEng = new CultureInfo("en-US");
                                DateTime __getDate = Convert.ToDateTime(__getMFDDate, __cultureEng);
                                IFormatProvider __cultureThai = MyLib._myGlobal._cultureInfo();
                                this._mfdDate.textBox.Text = this._mfdDate._textFirst = this._mfdDate._textLast = __getDate.ToString("d/M/yyyy", __cultureThai);
                                this._expireDate._textSecond = "";
                                this._expireDate._checkDate(true, true);
                            }
                            else
                            {
                                this._mfdDate.textBox.Text =
                                    this._mfdDate._textFirst =
                                    this._mfdDate._textSecond =
                                    this._mfdDate._textLast = "";
                                this._mfdDate._dateTime = new DateTime(1000, 1, 1);
                                this._mfdDate._dateTimeOld = this._expireDate._dateTime;
                                this._mfdDate._refresh();
                            }
                        }
                        catch
                        {
                            this._mfdDate.textBox.Text =
                                this._mfdDate._textFirst =
                                this._mfdDate._textSecond =
                                this._mfdDate._textLast = "";
                            this._mfdDate._dateTime = new DateTime(1000, 1, 1);
                            this._mfdDate._dateTimeOld = this._expireDate._dateTime;
                            this._mfdDate._refresh();
                        }
                        //}

                        this._grid.Invalidate();
                    }
                    else
                    {
                        this.Enabled = false;
                    }
                    SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                    if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
                    {
                        string __wh_code = this._grid._cellGet(this._row, _g.d.ic_trans_detail._wh_code).ToString();
                        string __shelf_code = this._grid._cellGet(this._row, _g.d.ic_trans_detail._shelf_code).ToString();
                        string __wh_shelf = _g.d.ic_trans_detail_lot._wh_code + " = \'" + __wh_code + "\' and " + _g.d.ic_trans_detail_lot._shelf_code + "=\'" + __shelf_code + "\'";

                        // this._resultGrid._loadFromDataTable(__process._stkStockInfoAndBalanceByLotLocation(_g.g._productCostType.ปรกติ, null, __itemCode, __itemCode, "", true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT_ที่เก็บ_เรียงตามเอกสาร_IMEX, __wh_shelf));
                        DataTable __lotBalance = __process._stkLotInfoAndBalance(null, __itemCode, __itemCode, "\'" + __wh_code + "\'", "\'" + __shelf_code + "\'", true);
                        this._resultGrid._loadFromDataTable(__lotBalance);


                    }
                    else
                        this._resultGrid._loadFromDataTable(__process._stkStockInfoAndBalanceByLot(_g.g._productCostType.ปรกติ, null, __itemCode, __itemCode, "", true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT));
                }
                else
                {
                    this._text.Text = "";
                    this._lotNumber._textFirst = "";
                    this._resultGrid._clear();
                    this._mfnTextbox._textFirst = "";
                    this._mfdDate.textBox.Text =
                                    this._mfdDate._textFirst =
                                    this._mfdDate._textSecond =
                                    this._mfdDate._textLast = "";
                    this._mfdDate._dateTime = new DateTime(1000, 1, 1);
                    this._mfdDate._dateTimeOld = this._expireDate._dateTime;
                    this._mfdDate._refresh();

                }
            }
            else
            {
                this._text.Text = "";
                this._lotNumber._textFirst = "";
                this._resultGrid._clear();
                this._mfnTextbox._textFirst = "";
                this._mfdDate.textBox.Text =
                                this._mfdDate._textFirst =
                                this._mfdDate._textSecond =
                                this._mfdDate._textLast = "";
                this._mfdDate._dateTime = new DateTime(1000, 1, 1);
                this._mfdDate._dateTimeOld = this._expireDate._dateTime;
                this._mfdDate._refresh();
            }
            this._resultGrid.Invalidate();
            this.Invalidate();
            this.__onLoad = false;
        }
    }
}
