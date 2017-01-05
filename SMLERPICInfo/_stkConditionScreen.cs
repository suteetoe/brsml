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

namespace SMLERPICInfo
{
    public class _stkConditionScreen : MyLib._myScreen
    {
        private SMLERPControl._searchItemForm _searchItem;
        private _stkConditionControlEnum stkConditionControlTypeTemp;
        private SMLInventoryControl._utils._selectWareHouseForm _selectWareHouse;
        private SMLInventoryControl._utils._selectLocaionForm _selectLocation;

        public _stkConditionScreen()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._searchItem = new SMLERPControl._searchItemForm(false);
                this._selectWareHouse = new SMLInventoryControl._utils._selectWareHouseForm();
                this._selectLocation = new SMLInventoryControl._utils._selectLocaionForm();
                this._selectWareHouse._saveButton.Click += new EventHandler(__selectWareHouse_saveButton_Click);
                this._selectLocation._saveButton.Click += new EventHandler(_selectLocation_saveButton_Click);
                this._f12 = false;
            }
        }

        void _selectLocation_saveButton_Click(object sender, EventArgs e)
        {
            this._setDataStr(_g.d.ic_resource._location, this._selectLocation._selectStr);
        }

        void __selectWareHouse_saveButton_Click(object sender, EventArgs e)
        {
            this._setDataStr(_g.d.ic_resource._warehouse, this._selectWareHouse._selectStr);
        }

        public _stkConditionControlEnum _stkConditionType
        {
            set
            {
                this.stkConditionControlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this.stkConditionControlTypeTemp;
            }
        }

        void _build()
        {
            if (this._stkConditionType == _stkConditionControlEnum.ว่าง)
            {
                return;
            }
            //
            DateTime __today = DateTime.Now;
            this._table_name = _g.d.ic_resource._table;
            this._maxColumn = 2;

            switch (this._stkConditionType)
            {
                case _stkConditionControlEnum.สินค้าคงเหลือตามLOT:
                    this._addTextBox(0, 0, 0, 0, _g.d.ic_resource._ic_code_begin, 1, 25, 1, true, false, false);
                    this._addTextBox(0, 1, 0, 0, _g.d.ic_resource._ic_code_end, 1, 25, 1, true, false, false);
                    this._addDateBox(1, 0, 0, 0, _g.d.ic_resource._date_begin, 1, true);
                    this._addDateBox(1, 1, 0, 0, _g.d.ic_resource._date_end, 1, true);
                    this._addCheckBox(2, 0, _g.d.ic_resource._balance_only, false, true, true);
                    //
                    this._setDataDate(_g.d.ic_resource._date_begin, new DateTime(__today.Year, __today.Month, 1));
                    this._setDataDate(_g.d.ic_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
                    break;
                case _stkConditionControlEnum.สินค้าคงเหลือตามที่เก็บ:
                case _stkConditionControlEnum.สินค้าคงเหลือตามคลัง:
                case _stkConditionControlEnum.สินค้าคงเหลือ:
                    this._addTextBox(0, 0, 0, 0, _g.d.ic_resource._ic_code_begin, 1, 25, 1, true, false, false);
                    this._addTextBox(0, 1, 0, 0, _g.d.ic_resource._ic_code_end, 1, 25, 1, true, false, false);
                    this._addDateBox(1, 0, 0, 0, _g.d.ic_resource._date_begin, 1, true);
                    this._addDateBox(1, 1, 0, 0, _g.d.ic_resource._date_end, 1, true);
                    this._addTextBox(2, 0, 0, 0, _g.d.ic_resource._item_name, 1, 25, 0, true, false, false);
                    //
                    this._setDataDate(_g.d.ic_resource._date_begin, new DateTime(__today.Year, __today.Month, 1));
                    this._setDataDate(_g.d.ic_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
                    break;
                case _stkConditionControlEnum.สินค้าไม่เคลื่อนไหว:
                    string __formatNumber = MyLib._myGlobal._getFormatNumber("m00");
                    this._addTextBox(0, 0, 0, 0, _g.d.ic_resource._ic_code_begin, 1, 25, 1, true, false, false);
                    this._addTextBox(0, 1, 0, 0, _g.d.ic_resource._ic_code_end, 1, 25, 1, true, false, false);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ic_resource._count_day_from, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ic_resource._count_day_to, 1, 2, true, __formatNumber);
                    this._addDateBox(2, 0, 0, 0, _g.d.ic_resource._date_end, 1, true);
                    //
                    this._setDataNumber(_g.d.ic_resource._count_day_from, 30);
                    this._setDataNumber(_g.d.ic_resource._count_day_to, 5000);
                    this._setDataDate(_g.d.ic_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
                    break;
                case _stkConditionControlEnum.สรุปเคลื่อนไหวสินค้าตามปริมาณ:
                    this._addTextBox(0, 0, 0, 0, _g.d.ic_resource._ic_code_begin, 1, 25, 1, true, false, false);
                    this._addTextBox(0, 1, 0, 0, _g.d.ic_resource._ic_code_end, 1, 25, 1, true, false, false);
                    this._addDateBox(1, 0, 0, 0, _g.d.ic_resource._date_begin, 1, true);
                    this._addDateBox(1, 1, 0, 0, _g.d.ic_resource._date_end, 1, true);
                    this._addTextBox(2, 0, 0, 0, _g.d.ic_resource._ic_group, 1, 25, 1, true, false, true);
                    //
                    this._setDataDate(_g.d.ic_resource._date_begin, new DateTime(__today.Year, __today.Month, 1));
                    this._setDataDate(_g.d.ic_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
                    break;
            }
            //
            this._textBoxSearch -= new MyLib.TextBoxSearchHandler(_stkConditionControl__textBoxSearch);
            this._textBoxChanged -= new MyLib.TextBoxChangedHandler(_stkConditionControl__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_stkConditionControl__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_stkConditionControl__textBoxChanged);
            //
            this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchItem.Close();
            string __itemCode = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            //
            if (this._searchItem._name.Equals(_g.d.ic_resource._ic_code_begin))
                this._setDataStr(_g.d.ic_resource._ic_code_begin, __itemCode);
            else
                this._setDataStr(_g.d.ic_resource._ic_code_end, __itemCode);
            //
            SendKeys.Send("{TAB}");
        }

        void _stkConditionControl__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_resource._ic_code_begin) || name.Equals(_g.d.ic_resource._ic_code_end))
            {
                try
                {
                    string __itemCode = this._getDataStr(name);
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getItem = __myFrameWork._queryShort("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'").Tables[0];
                    string __itemName = "";
                    if (__getItem.Rows.Count > 0)
                    {
                        __itemName = __getItem.Rows[0][_g.d.ic_inventory._name_1].ToString();
                    }
                    this._setDataStr(name, __itemCode, __itemName, true);
                }
                catch
                {
                }
            }
        }

        void _stkConditionControl__textBoxSearch(object sender)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)sender;
            if (__textBox._name.Equals(_g.d.ic_resource._ic_code_begin) || __textBox._name.Equals(_g.d.ic_resource._ic_code_end))
            {
                this._searchItem._name = __textBox._name;
                this._searchItem.Text = __textBox._labelName;
                this._searchItem.StartPosition = FormStartPosition.CenterScreen;
                this._searchItem.ShowDialog();
            }
            if (__textBox._name.Equals(_g.d.ic_resource._ic_group))
            {
                // search group
                MyLib._searchDataFull __searchGroup = new MyLib._searchDataFull();
                __searchGroup._dataList._loadViewFormat(_g.g._search_master_ic_group, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchGroup._dataList._multiSelect = true;                
                __searchGroup._dataList._selectSuccessButton.Click += (s1, e1) =>
                {
                    __textBox.textBox.Text = __searchGroup._dataList._selectList();
                    __searchGroup.Close();
                };
                __searchGroup.StartPosition = FormStartPosition.CenterScreen;
                __searchGroup.ShowDialog();
                //MyLib._myGlobal._startSearchBox(__textBox, __textBox._labelName, __searchGroup, true);
            }
            if (__textBox._name.Equals(_g.d.ic_resource._warehouse))
            {
                this._selectWareHouse.ShowDialog();
            }
            if (__textBox._name.Equals(_g.d.ic_resource._location))
            {
                string __wareHouse = this._getDataStr(_g.d.ic_resource._warehouse);

                this._selectLocation._location._load(__wareHouse);
                this._selectLocation.ShowDialog();
            }
        }
    }

    public enum _stkConditionControlEnum
    {
        ว่าง,
        สินค้าคงเหลือ,
        สินค้าคงเหลือตามคลัง,
        สินค้าคงเหลือตามที่เก็บ,
        สินค้าคงเหลือตามLOT,
        สินค้าไม่เคลื่อนไหว,
        สรุปเคลื่อนไหวสินค้าตามปริมาณ
    }
}
