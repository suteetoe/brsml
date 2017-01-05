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
    public partial class _icBarcodeDiscountGrid : UserControl
    {
        public _barcodeDiscountGridType _priceType;
        public string _itemCode = "";
        int __width1 = 12;
        SMLERPControl._ic._icTransItemGridSelectUnitForm _icTransItemGridSelectUnit = new SMLERPControl._ic._icTransItemGridSelectUnitForm();

        public _icBarcodeDiscountGrid(_barcodeDiscountGridType priceType)
        {
            InitializeComponent();

            this._priceType = priceType;
            this._createGrid();
        }

        private void _createGrid()
        {
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);

            this._grid._clear();
            this._grid._columnListTop.Clear();
            this._grid._columnList.Clear();
            this._grid._columnTopActive = true;
            this._grid._table_name = _g.d.ic_inventory_barcode_price._table;
            switch (this._priceType)
            {
                case _barcodeDiscountGridType.ส่วนลดพิเศษทั่วไป:
                    this._grid._addColumnTop(_g.d.ic_inventory_barcode_price._price_name_1, 0, 1);
                    break;
                case _barcodeDiscountGridType.ส่วนลดพิเศษสมาชิก:
                    this._grid._addColumnTop(_g.d.ic_inventory_barcode_price._price_name_2, 0, 1);
                    break;
                case _barcodeDiscountGridType.ส่วนลดโปรโมชั่น:
                    this._grid._addColumnTop(_g.d.ic_inventory_barcode_price._price_name_3, 0, 1);
                    break;
                case _barcodeDiscountGridType.ส่วนลดสมาชิก:
                    this._grid._addColumnTop(_g.d.ic_inventory_barcode_price._price_name_4, 0, 1);
                    break;
                case _barcodeDiscountGridType.ส่วนลดพนักงาน:
                    this._grid._addColumnTop(_g.d.ic_inventory_barcode_price._price_name_5, 0, 1);
                    break;
            }
            this._grid._addColumn(_g.d.ic_inventory_barcode_price._priority, 3, 0, 5, true, false, true, false, __formatNumberPrice);
            this._grid._addColumn(_g.d.ic_inventory_barcode_price._discount_word, 1, 0, 10, true, false);
            this._grid._addColumnTop(_g.d.ic_inventory_barcode_price._qty_condition, 2, 3);
            this._grid._addColumn(_g.d.ic_inventory_barcode_price._qty_begin, 3, 0, this.__width1, true, false, true, false, __formatNumberPrice);
            this._grid._addColumn(_g.d.ic_inventory_barcode_price._qty_end, 3, 0, this.__width1, true, false, true, false, __formatNumberPrice);
            this._grid._setColumnBackground(_g.d.ic_inventory_barcode_price._qty_begin, Color.LavenderBlush);
            this._grid._setColumnBackground(_g.d.ic_inventory_barcode_price._qty_end, Color.LavenderBlush);
            //
            this._grid._addColumnTop(_g.d.ic_inventory_barcode_price._date_condition, 4, 5);
            this._grid._addColumn(_g.d.ic_inventory_barcode_price._date_begin, 4, 0, this.__width1, true, false);
            this._grid._addColumn(_g.d.ic_inventory_barcode_price._date_end, 4, 0, this.__width1, true, false);

            this._grid._addColumn(_g.d.ic_inventory_barcode_price._lock_day, 12, 0, this.__width1, true, false);

            this._grid._setColumnBackground(_g.d.ic_inventory_barcode_price._date_begin, Color.AliceBlue);
            this._grid._setColumnBackground(_g.d.ic_inventory_barcode_price._lock_day, Color.AliceBlue);

            // pos filter
            this._grid._addColumn(_g.d.ic_inventory_barcode_price._lock_discount, 10, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_barcode_price._lock_code_list, 1, 0, 10, true, false);
            //
            this._grid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_grid__cellComboBoxItem);
            this._grid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_grid__cellComboBoxGet);


            //
            this._grid._addColumn(_g.d.ic_inventory_barcode_price._status, 11, 0, 5, true, false);
            //
            this._addEvent();
            //
            this._grid._addColumn("roworder", 1, 0, 0, false, true, false);
            this._grid._calcPersentWidthToScatter();
            //
        }

        MyLib.BeforeDisplayRowReturn _grid__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            if (row < sender._rowData.Count && columnName.Equals(_g.d.ic_inventory_barcode_price._table + "." + _g.d.ic_inventory_barcode_price._lock_day))
            {
                object __daySelectObj = sender._cellGet(row, sender._findColumnByName(_g.d.ic_inventory_barcode_price._lock_day));
                ((ArrayList)senderRow.newData)[columnNumber] = "ทุกวัน";
                if (__daySelectObj != null)
                {
                    string __getDaySelect = __daySelectObj.ToString();
                    StringBuilder __dayValue = new StringBuilder();

                    string[] __daySplit = __getDaySelect.Split(',');
                    foreach (string __day in __daySplit)
                    {
                        if (__dayValue.Length > 0)
                        {
                            __dayValue.Append(",");
                        }

                        switch (__day)
                        {
                            case "0": __dayValue.Append("อาทิตย์"); break;
                            case "1": __dayValue.Append("จันทร์"); break;
                            case "2": __dayValue.Append("อังคาร"); break;
                            case "3": __dayValue.Append("พุธ"); break;
                            case "4": __dayValue.Append("พฤหัสบดี"); break;
                            case "5": __dayValue.Append("ศุกร์"); break;
                            case "6": __dayValue.Append("เสาร์"); break;
                        }
                    }

                    ((ArrayList)senderRow.newData)[columnNumber] = (__dayValue.Length > 0) ? __dayValue.ToString() : "ทุกวัน";

                }

                /*if (__select == 1)
                {
                    switch (__mode)
                    {
                        case 1: __result.newColor = Color.Red;
                            ((ArrayList)senderRow.newData)[columnNumber] = "ส่งออกจาก Data Center";
                            break;
                        case 2: __result.newColor = Color.Blue;
                            ((ArrayList)senderRow.newData)[columnNumber] = "รับข้อมูลเข้า Data Center";
                            break;
                        case 3: __result.newColor = Color.Green;
                            ((ArrayList)senderRow.newData)[columnNumber] = "แลกเปลี่ยนข้อมูลกับ Data Center";
                            break;
                    }
                }*/
            }
            return __result;
        }

        string[] _sale_type = { "ทุกเครื่องในระบบ", "เฉพาะรหัสเครื่อง POS" };
        string _grid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            return (_sale_type[(select == -1) ? 0 : select].ToString());
        }

        object[] _grid__cellComboBoxItem(object sender, int row, int column)
        {
            return _sale_type;
        }

        public void _addEvent()
        {
            this._grid._afterAddRow += new MyLib.AfterAddRowEventHandler(_grid__afterAddRow);
            this._grid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_grid__alterCellUpdate);
            this._grid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_grid__queryForInsertCheck);
            this._grid._beforeDisplayRendering += _grid__beforeDisplayRendering;
            this._grid._mouseClickClip += _grid__mouseClickClip;
            this._grid._beforeLoadDataObjectColumn += _grid__beforeLoadDataObjectColumn;
        }

        object _grid__beforeLoadDataObjectColumn(object sender, int row, int column)
        {
            return sender.ToString();
        }


        public void _removeEvent()
        {
            this._grid._afterAddRow -= new MyLib.AfterAddRowEventHandler(_grid__afterAddRow);
            this._grid._alterCellUpdate -= new MyLib.AfterCellUpdateEventHandler(_grid__alterCellUpdate);
            this._grid._queryForInsertCheck -= new MyLib.QueryForInsertCheckEventHandler(_grid__queryForInsertCheck);
            this._grid._beforeDisplayRendering -= _grid__beforeDisplayRendering;
            this._grid._mouseClickClip -= _grid__mouseClickClip;

        }

        bool _grid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return (this._grid._cellGet(row, _g.d.ic_inventory_barcode_price._discount_word).ToString().Length == 0) ? false : true;
        }

        void _grid__alterCellUpdate(object sender, int row, int column)
        {
        }

        void _grid__mouseClickClip(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.Equals(_g.d.ic_inventory_barcode_price._lock_day))
            {
                object __getOldDayObject = this._grid._cellGet(e._row, _g.d.ic_inventory_barcode_price._lock_day);
                string __getOldDahy = (__getOldDayObject != null) ? __getOldDayObject.ToString() : "";
                _icBarcodeDiscountSelectDayForm __daySelectForm = new _icBarcodeDiscountSelectDayForm();

                if (__getOldDahy.Length > 0)
                {
                    __daySelectForm._sunCheckbox.Checked =
                        __daySelectForm._monCheckbox.Checked =
                        __daySelectForm._tueCheckbox.Checked =
                        __daySelectForm._webCheckbox.Checked =
                        __daySelectForm._thuCheckbox.Checked =
                        __daySelectForm._friCheckbox.Checked =
                        __daySelectForm._satCheckbox.Checked = false;


                    string[] __daySplit = __getOldDahy.Split(',');
                    foreach (string day in __daySplit)
                    {
                        switch (day)
                        {
                            case "0": __daySelectForm._sunCheckbox.Checked = true; break;
                            case "1": __daySelectForm._monCheckbox.Checked = true; break;
                            case "2": __daySelectForm._tueCheckbox.Checked = true; break;
                            case "3": __daySelectForm._webCheckbox.Checked = true; break;
                            case "4": __daySelectForm._thuCheckbox.Checked = true; break;
                            case "5": __daySelectForm._friCheckbox.Checked = true; break;
                            case "6": __daySelectForm._satCheckbox.Checked = true; break;

                        }
                    }
                }

                __daySelectForm.StartPosition = FormStartPosition.CenterScreen;
                __daySelectForm.ShowDialog();
                if (__daySelectForm.DialogResult == DialogResult.OK)
                {
                    if (__daySelectForm._allDayCheckbox.Checked == true)
                    {
                        this._grid._cellUpdate(e._row, _g.d.ic_inventory_barcode_price._lock_day, "", true);
                    }
                    else
                    {
                        // บางวัน
                        StringBuilder __selectDayData = new StringBuilder();

                        if (__daySelectForm._sunCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("0");
                        }

                        if (__daySelectForm._monCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("1");
                        }

                        if (__daySelectForm._tueCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("2");
                        }

                        if (__daySelectForm._webCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("3");
                        }

                        if (__daySelectForm._thuCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("4");
                        }

                        if (__daySelectForm._friCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("5");
                        }

                        if (__daySelectForm._satCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("6");
                        }

                        this._grid._cellUpdate(e._row, _g.d.ic_inventory_barcode_price._lock_day, __selectDayData.ToString(), true);

                    }
                }
            }
        }


        void _grid__afterAddRow(object sender, int row)
        {
            int __year = DateTime.Now.Year;
            this._grid._cellUpdate(row, _g.d.ic_inventory_barcode_price._priority, (decimal)(row + 1), false);
            this._grid._cellUpdate(row, _g.d.ic_inventory_barcode_price._qty_begin, (decimal)1, false);
            this._grid._cellUpdate(row, _g.d.ic_inventory_barcode_price._qty_end, (decimal)9999999, false);
            this._grid._cellUpdate(row, _g.d.ic_inventory_barcode_price._date_begin, new DateTime(__year, 1, 1), false);
            this._grid._cellUpdate(row, _g.d.ic_inventory_barcode_price._date_end, new DateTime(__year + 5, 12, 31), false);
            this._grid._cellUpdate(row, _g.d.ic_inventory_barcode_price._status, 1, false);
            this._grid._cellUpdate(row, _g.d.ic_inventory_barcode_price._lock_day, "", false);
        }
    }

    public enum _barcodeDiscountGridType
    {
        ส่วนลดพิเศษทั่วไป,
        ส่วนลดพิเศษสมาชิก,
        ส่วนลดโปรโมชั่น,
        ส่วนลดสมาชิก,
        ส่วนลดพนักงาน
    }
}
