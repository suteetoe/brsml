using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._utils
{
    public partial class _selectSerialNumberForm : Form
    {
        public string _selectStr = "";
        private string _itemCode;
        private string _docRefNo;
        private _g.g._transControlTypeEnum _transControlType;

        public _selectSerialNumberForm(_g.g._transControlTypeEnum transControlType, string itemCode, string docRefNo)
        {
            InitializeComponent();
            this._itemCode = itemCode;
            this._docRefNo = docRefNo.Trim();
            this._transControlType = transControlType;
            this._dataList._fullMode = false;
            this._dataList._editMode = false;
            this._dataList._lockRecord = false; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            this._dataList._loadViewFormat(_g.g._search_screen_ic_serial, MyLib._myGlobal._userSearchScreenGroup, false);
            this._dataList._referFieldAdd(_g.d.ic_serial._serial_number, 1);
            this._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            this._extraWhere();
            this._allCheckBox.CheckedChanged += new EventHandler(_allCheckBox_CheckedChanged);
        }

        void _allCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this._extraWhere();
        }

        private void _extraWhere()
        {
            this._dataList._extraWhere = _g.d.ic_serial._ic_code + "=\'" + this._itemCode + "\'";
            if (this._docRefNo.Length > 0)
            {
                string __transFlagRef = "";
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                        __transFlagRef = __transFlagRef = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString();
                        this._dataList._extraWhere = this._dataList._extraWhere + " and " + _g.d.ic_serial._serial_number + " in (select " + _g.d.ic_trans_serial_number._serial_number + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no + "=\'" + this._docRefNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + " in (" + __transFlagRef + "))";
                        break;
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        __transFlagRef = __transFlagRef = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString();
                        this._dataList._extraWhere = this._dataList._extraWhere + " and " + _g.d.ic_serial._serial_number + " in (select " + _g.d.ic_trans_serial_number._serial_number + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no + "=\'" + this._docRefNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + " in (" + __transFlagRef + "))";
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        __transFlagRef = __transFlagRef = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString();
                        this._dataList._extraWhere = this._dataList._extraWhere + " and " + _g.d.ic_serial._serial_number + " in (select " + _g.d.ic_trans_serial_number._serial_number + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no + "=\'" + this._docRefNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + " in (" + __transFlagRef + "))";
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                        __transFlagRef = __transFlagRef = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString();
                        this._dataList._extraWhere = this._dataList._extraWhere + " and " + _g.d.ic_serial._serial_number + " in (select " + _g.d.ic_trans_serial_number._serial_number + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no + "=\'" + this._docRefNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + " in (" + __transFlagRef + "))";
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                        __transFlagRef = __transFlagRef = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ).ToString();
                        this._dataList._extraWhere = this._dataList._extraWhere + " and " + _g.d.ic_serial._serial_number + " in (select " + _g.d.ic_trans_serial_number._serial_number + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no + "=\'" + this._docRefNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + " in (" + __transFlagRef + "))";
                        break;
                }
                //this._dataList._extraWhere = this._dataList._extraWhere + " and " + _g.d.ic_serial._serial_number + " in (select " + _g.d.ic_trans_serial_number._serial_number + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_no + "=\'" + this._docRefNo + "\' and " + _g.d.ic_trans_serial_number._trans_flag + " in (" + __transFlagRef + "))";
            }
            if (this._allCheckBox.Checked == false)
            {
                switch (this._transControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                        // เฉพาะที่อยู่ในสต๊อก
                        this._dataList._extraWhere = this._dataList._extraWhere + " and " + _g.d.ic_serial._status + "=0";
                        break;
                }
            }
            this._dataList._refreshData();
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData, Graphics e)
        {
            int __status = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, _g.d.ic_serial._table + "." + _g.d.ic_serial._status).ToString());
            if (__status == 1)
            {
                senderRow.newColor = Color.Blue;
            }
            return senderRow;
        }
    }
}
