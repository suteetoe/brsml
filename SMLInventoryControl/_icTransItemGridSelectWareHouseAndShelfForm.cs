using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransItemGridSelectWareHouseAndShelfForm : Form
    {
        public string _itemCode = "";
        public string _lastCode = "";
        /// <summary>ประเภทการแสดงคลังและที่เก็บ </summary>
        public string _screenType = "";
        public _g.g._transControlTypeEnum _controlType = _g.g._transControlTypeEnum.ว่าง;

        public event _selectWareHouseAndShelfEventHandler _selectWareHouseAndShelf;
        public string _extraWhere = "";

        public _icTransItemGridSelectWareHouseAndShelfForm()
        {
            InitializeComponent();
            this.Shown += new EventHandler(_icTransItemGridSelectWareHouseAndShelfForm_Shown);
            this._wareHouseAndLocation._mouseClick += new MyLib.MouseClickHandler(_wareHouseAndShelf__mouseClick);
        }

        void _wareHouseAndShelf__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            _return(e._row);
        }

        void _return(int row)
        {
            string __wareHouseCode = this._wareHouseAndLocation._cellGet(row, _g.d.ic_wh_shelf._wh_code).ToString();
            string __shelfCode = this._wareHouseAndLocation._cellGet(row, _g.d.ic_wh_shelf._shelf_code).ToString();
            if (this._selectWareHouseAndShelf != null && __wareHouseCode.Length > 0 && __shelfCode.Length > 0)
            {
                this.Close();
                this._selectWareHouseAndShelf(1, __wareHouseCode, __shelfCode);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                this._selectWareHouseAndShelf(0, "", "");
                return true;
            }
            if (keyData == Keys.Enter)
            {
                _return(this._wareHouseAndLocation._selectRow);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _icTransItemGridSelectWareHouseAndShelfForm_Shown(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __extraWhere = new StringBuilder();

            // toe เพิ่ม filter warehouse และ shelf ตามกลุ่มพนักงาน
            if (_g.g._companyProfile._perm_wh_shelf)
            {
                // _g.d.erp_user_group_wh_shelf._screen_code


                // ย้ายไป icInfoFlag
                __extraWhere.Append(_g._icInfoFlag._icWhShelfUserPermissionWhereQuery(this._controlType));
                /*
                string __screen_type = "";
                switch (this._controlType)
                {
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "PU" + "\' ";
                        break;
                    case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        {
                            if (MyLib._myGlobal._programName.Equals("SML CM"))
                            {
                                __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "SI" + "\' ";
                            }
                        }
                        break;
                    case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "SI" + "\' ";
                        break;
                    case _g.g._transControlTypeEnum.สินค้า_โอนเข้า:
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    case _g.g._transControlTypeEnum.สินค้า_โอนเข้า_ยกเลิก:
                    case _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก:
                        __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "IM" + "\' ";
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                        __screen_type = " and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'" + "ST" + "\' ";
                        break;
                }

                if (__screen_type.Length > 0)
                {
                    // __queryWareHouseAndShelf = "select * from (" + __queryWareHouseAndShelf + ") as temp1 where " +
                    __extraWhere.Append(
                    _g.d.ic_wh_shelf._wh_code + " in (select " + _g.d.erp_user_group_wh_shelf._wh_code + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + " in (select " + _g.d.erp_user_group_detail._group_code + " from " + _g.d.erp_user_group_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "') " + __screen_type + ") and " +
                    _g.d.ic_wh_shelf._shelf_code + " in (select " + _g.d.erp_user_group_wh_shelf._shelf_code + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + " in (select " + _g.d.erp_user_group_detail._group_code + " from " + _g.d.erp_user_group_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_user_group_detail._user_code) + " = '" + MyLib._myGlobal._userCode.ToUpper() + "') " + __screen_type + ") "

                    );
                }*/


                if (this._extraWhere.Length > 0)
                {
                    if (__extraWhere.Length > 0)
                        __extraWhere.Append(" and ");

                    __extraWhere.Append(this._extraWhere);
                }
            }

            string __queryWareHouseAndShelf = this._wareHouseAndLocation._createQueryForLoad(this._itemCode, __extraWhere.ToString());


            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __queryWareHouseAndShelf);
            this._wareHouseAndLocation._loadFromDataTable(__result.Tables[0]);
            this._wareHouseAndLocation._selectColumn = 0;
            this._wareHouseAndLocation._selectRow = 0;
            this._wareHouseAndLocation._message = "เลือกคลังและพื้นที่เก็บโดยใช้ Mouse หรือ Keyboard โดยเลื่อนขึ้นลง แล้วเลือกโดยกด <b>Enter</b> หรือ ยกเลิกโดยกด <b>Esc</b>";
            this._wareHouseAndLocation.Invalidate();
            this._wareHouseAndLocation.Focus();
            // Find
            this._wareHouseAndLocation._findAndGotoRow(this._wareHouseAndLocation._temp1, this._lastCode);
        }

        public delegate void _selectWareHouseAndShelfEventHandler(int mode, string wareHouseCode, string shelfCode);
    }
}
