using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public class _stkProfitConditionScreen : MyLib._myScreen
    {
        MyLib._searchDataFull _searchItem = new MyLib._searchDataFull();
        _infoStkProfitEnum _mode;

        public _stkProfitConditionScreen(_infoStkProfitEnum mode, string dateBegin, string dateEnd)
        {
            this._mode = mode;
            this._table_name = _g.d.ic_resource._table;
            this._maxColumn = 2;
            int __row = 0;
            switch (this._mode)
            {
                case _infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    // ตามสินค้า
                    this._addTextBox(__row, 0, 0, 0, _g.d.ic_resource._ic_code_begin, 1, 25, 1, true, false, false);
                    this._addTextBox(__row++, 1, 0, 0, _g.d.ic_resource._ic_code_end, 1, 25, 1, true, false, false);
                    break;
                case _infoStkProfitEnum.กำไรขั้นต้น_เอกสาร:
                    // ตามเอกสาร ไม่ให้เลือกเอกสาร
                    break;
                case  _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                    // ตามลูกค้า
                    this._addTextBox(__row, 0, 0, 0, _g.d.ic_resource._ar_code_begin, 1, 25, 1, true, false, false);
                    this._addTextBox(__row++, 1, 0, 0, _g.d.ic_resource._ar_code_end, 1, 25, 1, true, false, false);
                    break;
            }
            this._addDateBox(__row, 0, 0, 0, _g.d.ic_resource._date_begin, 1, true);
            this._addDateBox(__row, 1, 0, 0, _g.d.ic_resource._date_end, 1, true);
            //
            DateTime __today = DateTime.Now;
            this._setDataDate(_g.d.ic_resource._date_begin, (dateBegin.Length != 0) ? MyLib._myGlobal._convertDate(dateBegin):  new DateTime(__today.Year, __today.Month, 1));
            this._setDataDate(_g.d.ic_resource._date_end, (dateEnd.Length != 0) ? MyLib._myGlobal._convertDate(dateEnd) : new DateTime(__today.Year, __today.Month, __today.Day));
            //
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_stkMovementConditionScreen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_stkMovementConditionScreen__textBoxChanged);
            //
            switch (this._mode)
            {
                case _infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    // ตามสินค้า
                    this._searchItem._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);
                    break;
                case _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                    // ตามลูกค้า
                    this._searchItem._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, false);
                    break;
            }
            this._searchItem._dataList._refreshData();
            this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
        }

        void _stkMovementConditionScreen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_resource._ic_code_begin))
            {
                string __itemCode = this._getDataStr(_g.d.ic_resource._ic_code_begin);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getItem = __myFrameWork._queryShort("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'").Tables[0];
                string __itemName = "";
                if (__getItem.Rows.Count > 0)
                {
                    __itemName = __getItem.Rows[0][_g.d.ic_inventory._name_1].ToString();
                }
                this._setDataStr(_g.d.ic_resource._ic_code_begin, __itemCode, __itemName, true);
            }
            if (name.Equals(_g.d.ic_resource._ic_code_end))
            {
                string __itemCode = this._getDataStr(_g.d.ic_resource._ic_code_end);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getItem = __myFrameWork._queryShort("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'").Tables[0];
                string __itemName = "";
                if (__getItem.Rows.Count > 0)
                {
                    __itemName = __getItem.Rows[0][_g.d.ic_inventory._name_1].ToString();
                }
                this._setDataStr(_g.d.ic_resource._ic_code_end, __itemCode, __itemName, true);
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchItem.Close();
            switch (this._mode)
            {
                case _infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    {
                        string __itemCode = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                        if (this._searchItem._name.Equals(_g.d.ic_resource._ic_code_begin))
                        {
                            this._setDataStr(_g.d.ic_resource._ic_code_begin, __itemCode);
                        }
                        else if (this._searchItem._name.Equals(_g.d.ic_resource._ic_code_end))
                        {
                            this._setDataStr(_g.d.ic_resource._ic_code_end, __itemCode);
                        }
                    }
                    break;
                case _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                    {
                        string __arCode = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                        if (this._searchItem._name.Equals(_g.d.ic_resource._ar_code_begin))
                        {
                            this._setDataStr(_g.d.ic_resource._ar_code_begin, __arCode);
                        }
                        else if (this._searchItem._name.Equals(_g.d.ic_resource._ar_code_begin))
                        {
                            this._setDataStr(_g.d.ic_resource._ar_code_begin, __arCode);
                        }
                    }
                    break;
                // ตามลูกค้า
            }
            //
            SendKeys.Send("{TAB}");
        }

        void _stkMovementConditionScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)sender;
            this._searchItem._name = __textBox._name;
            this._searchItem.Text = __textBox._labelName;
            this._searchItem.StartPosition = FormStartPosition.CenterScreen;
            this._searchItem.ShowDialog();
        }
    }
}
