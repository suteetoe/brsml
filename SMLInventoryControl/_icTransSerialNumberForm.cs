using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransSerialNumberForm : Form
    {
        public delegate Boolean CheckSerialNumberEventHandler(int gridRow, string serialNumber);
        public delegate decimal PriceEventHandler();
        public delegate string WareHouseEventHandler();
        public delegate string LocationEventHandler();
        //
        public event CheckSerialNumberEventHandler _checkSerialNumber;
        public event PriceEventHandler _price;
        public event WareHouseEventHandler _getWarehouse;
        public event LocationEventHandler _getLocation;
        //
        private int _maxRow = -1;
        private _g.g._transControlTypeEnum _transControlType;
        private _utils._selectSerialNumberForm _searchSerialNumber;
        private string _itemCode;
        private string _itemName;
        private int _gridRow;
        private string _docNo;
        private string _docNoOld;
        private string _refDocNo;

        public _icTransSerialNumberForm(string docNo, string docNoOld, string refDocNo, string itemCode, string itemName, int gridRow, _g.g._transControlTypeEnum transControlType, int maxRow)
        {
            InitializeComponent();
            //
            this._maxRow = maxRow;
            this._transControlType = transControlType;
            this._itemCode = itemCode;
            this._itemName = itemName;
            this._gridRow = gridRow;
            this._docNo = docNo;
            this._docNoOld = docNoOld;
            this._refDocNo = refDocNo;
            //
            Boolean __isEditSerialNumber = true;
            Boolean __isEditVoidDate = true;
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    __isEditSerialNumber = false;
                    __isEditVoidDate = true;
                    break;
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    __isEditSerialNumber = false;
                    __isEditVoidDate = false;
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    // กรณี รับคืนไม่อ้างบิล ให้บันทึกหมายเลขเครื่องเหมือนตอนซื้อ
                    __isEditVoidDate = false;
                    if (refDocNo.Length > 0)
                    {
                        __isEditSerialNumber = false;
                    }
                    break;
                case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                    //__isEditVoidDate = false;
                    break;
                case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                    __isEditVoidDate = false;
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                    //__isEditVoidDate = false;
                    break;
                case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย:
                    //__isEditVoidDate = false;
                    break;
                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                    //__isEditVoidDate = false;
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                    //__isEditVoidDate = false;
                    break;
                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                    //__isEditVoidDate = false;
                    break;
            }

            // toe isedit show gen wizard
            if (__isEditSerialNumber == false)
            {
                this._genPanel.Visible = false;
            }

            this._grid._table_name = _g.d.ic_trans_serial_number._table;
            this._grid._addColumn(_g.d.ic_trans_serial_number._serial_number, 1, 100, 30, __isEditSerialNumber, false, true, false);
            this._grid._addColumn(_g.d.ic_trans_serial_number._price, 3, 1, 20, _g.g._companyProfile._column_price_enable, false, true, true, _g.g._getFormatNumberStr(2));
            this._grid._addColumn(_g.d.ic_trans_serial_number._void_date, 4, 100, 20, __isEditVoidDate, false);
            this._grid._addColumn(_g.d.ic_trans_serial_number._description, 1, 100, 30);
            this._grid._calcPersentWidthToScatter();
            //
            this._grid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_grid__alterCellUpdate);
            // 
            this._serialNumberTextBox.KeyUp += new KeyEventHandler(_serialNumberTextBox_KeyUp);
            this.Load += new EventHandler(_icTransSerialNumberForm_Load);

            if (__isEditSerialNumber)
            {
                // check start running format
                string __query = "select " + _g.d.ic_inventory._serial_no_format + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._itemCode + "\'";
                MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                DataTable __result = __myFramework._queryShort(__query).Tables[0];
                if (__result.Rows.Count > 0)
                {
                    if (__result.Rows[0][0].ToString().Length > 0)
                    {
                        // set start running
                        string __getFormat = __result.Rows[0][0].ToString();
                        string __newSerialNumber = __getFormat;

                        DateTime __date = DateTime.Now;
                        CultureInfo __dateUSZone = new CultureInfo("en-US");
                        CultureInfo __dateTHZone = new CultureInfo("th-TH");
                        __getFormat = __getFormat.Replace("DD", __date.ToString("dd", __dateUSZone));
                        __getFormat = __getFormat.Replace("MM", __date.ToString("MM", __dateUSZone));
                        __getFormat = __getFormat.Replace("YYYY", __date.ToString("yyyy", __dateUSZone));
                        __getFormat = __getFormat.Replace("YY", __date.ToString("yy", __dateUSZone));
                        __getFormat = __getFormat.Replace("วว", __date.ToString("dd", __dateTHZone));
                        __getFormat = __getFormat.Replace("ดด", __date.ToString("MM", __dateTHZone));
                        __getFormat = __getFormat.Replace("ปปปป", __date.ToString("yyyy", __dateTHZone));
                        __getFormat = __getFormat.Replace("ปป", __date.ToString("yy", __dateTHZone));
                        __getFormat = __getFormat.Replace("@", this._itemCode);
                        int __numberBegin = __getFormat.IndexOf("#");
                        if (__numberBegin != -1)
                        {
                            StringBuilder __getFormatNumber = new StringBuilder();
                            int __numberEnd = __numberBegin;
                            while (__numberEnd < __getFormat.Length && __getFormat[__numberEnd] == '#')
                            {
                                __getFormatNumber.Append("#");
                                __numberEnd++;
                            }
                            __getFormat = __getFormat.Remove(__numberBegin, __numberEnd - __numberBegin);
                            string __getDocQuery = __getFormat + "z";

                            string __qw = " " + _g.d.ic_trans_serial_number._serial_number + " <= '" + __getDocQuery + "' and " + _g.d.ic_trans_serial_number._ic_code + " = \'" + this._itemCode + "\'";
                            __newSerialNumber = MyLib._myGlobal._getAutoRun(_g.d.ic_trans_serial_number._table, _g.d.ic_trans_serial_number._serial_number, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();

                            if (__newSerialNumber.Length > 0)
                            {
                                this._serialStartTextbox.Text = __newSerialNumber;
                            }
                        }


                    }
                }
            }
        }

        public void _loadData(_icTransItemGridControl._serialNumberStruct source)
        {
            for (int __row = 0; __row < source.__details.Count; __row++)
            {
                _icTransItemGridControl._serialNumberDetailStruct __detail = source.__details[__row];
                int __addr = this._grid._addRow();
                this._grid._cellUpdate(__addr, _g.d.ic_trans_serial_number._serial_number, __detail._serialNumber, false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans_serial_number._void_date, __detail._voidDate, false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans_serial_number._price, __detail._price, false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans_serial_number._description, __detail._description, false);
            }
        }

        void _icTransSerialNumberForm_Load(object sender, EventArgs e)
        {
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    this.Text = MyLib._myGlobal._resource("บันทึกจ่าย Serial Number");
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    this.Text = MyLib._myGlobal._resource("บันทึกรับ Serial Number");
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    this.Text = MyLib._myGlobal._resource("บันทึกรับ Serial Number");
                    break;
            }
            this._serialNumberTextBox.Focus();
        }

        Boolean _checkSerialNumberDupGrid(int row, string serialNumber)
        {
            if (serialNumber.Trim().Length > 0)
            {
                for (int __row = 0; __row < this._grid._rowData.Count; __row++)
                {
                    if (row != __row)
                    {
                        string __serialNumber = this._grid._cellGet(__row, _g.d.ic_trans_serial_number._serial_number).ToString().Trim().ToUpper();
                        if (serialNumber.Equals(__serialNumber))
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("บันทึกหมายเลขเครื่องซ้ำ"));
                            this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._serial_number, "", false);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        void _grid__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._grid._findColumnByName(_g.d.ic_trans_serial_number._serial_number))
            {
                string __serialNumber = this._grid._cellGet(row, _g.d.ic_trans_serial_number._serial_number).ToString().Trim().ToUpper();
                if (__serialNumber.Length > 0 && this._checkSerialNumberDupGrid(row, __serialNumber) == false)
                {
                    if (this._checkSerialNumber != null)
                    {
                        if (this._checkSerialNumber(this._gridRow, __serialNumber) == false)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            switch (this._transControlType)
                            {
                                #region กรณีรับเข้า
                                case _g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา:
                                case _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า:
                                case _g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ:
                                case _g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย: // น่าจะอยู่ด้านออก
                                case _g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า:
                                case _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า:
                                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                                case _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา:
                                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                                case _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก:
                                case _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป:
                                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                                    {
                                        bool __check = true;
                                        if ((this._transControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) && this._refDocNo.Length > 0)
                                        {
                                            __check = false;
                                        }

                                        if (__check)
                                        {
                                            SMLProcess._icProcess __icProcess = new SMLProcess._icProcess();

                                            // ตรวจว่ามีหมายเลขเครื่องเดิมหรือไม่ (กรณีรับเข้า ไม่อ้างบิล)
                                            string __query = "select count(*) as xcount from " + _g.d.ic_trans_serial_number._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_serial_number._serial_number) + "=\'" + __serialNumber + "\' and " + MyLib._myGlobal._addUpper(_g.d.ic_trans_serial_number._doc_no) + " not in (\'" + this._docNo + "\',\'" + this._docNoOld + "\') and " + _g.d.ic_trans_serial_number._last_status + "=0 and " + _g.d.ic_trans_serial_number._trans_flag + " in (" + __icProcess._stockBalanceAllFlag.Replace("[", "").Replace("]", "") + ")";
                                            DataSet __getCount = __myFrameWork._queryShort(__query);
                                            if (__getCount.Tables.Count > 0)
                                            {
                                                DataTable __find = __getCount.Tables[0];
                                                int __count = (int)MyLib._myGlobal._decimalPhase(__find.Rows[0][0].ToString());
                                                if (__count > 0)
                                                {
                                                    if (_g.g._companyProfile._use_serial_no_duplicate == true)
                                                    {
                                                        if (MessageBox.Show("หมายเลขเครื่องมีอยู่ในระบบแล้ว ต้องการใช้หมายเลขเครื่องนี้ดำเนินการต่อหรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                                        {
                                                            this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._serial_number, "", false);
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show(MyLib._myGlobal._resource("หมายเลขเครื่องมีอยู่ในระบบแล้ว") + " : " + __serialNumber);
                                                        this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._serial_number, "", false);
                                                        return;
                                                    }
                                                }

                                                //else
                                                //{
                                                if (row > 0 && column == this._grid._findColumnByName(_g.d.ic_trans_serial_number._serial_number))
                                                {
                                                    try
                                                    {
                                                        DateTime __getDate = (DateTime)this._grid._cellGet(row - 1, _g.d.ic_trans_serial_number._void_date);
                                                        DateTime __getDateOld = (DateTime)this._grid._cellGet(row, _g.d.ic_trans_serial_number._void_date);
                                                        if (__getDateOld.Year < 2000)
                                                        {
                                                            this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._void_date, __getDate, false);
                                                        }
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                                //}
                                            }
                                        }
                                        break;
                                    }
                                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                    {
                                        // กรณีอ้างอิงบิล ต้องเช็ค serial ว่าตรงกันหรือเปล่า
                                        if (this._refDocNo.Length > 0)
                                        {
                                            string __query = "select count(*) as xcount from " + _g.d.ic_trans_serial_number._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_serial_number._serial_number) + "=\'" + __serialNumber + "\' and " + MyLib._myGlobal._addUpper(_g.d.ic_trans_serial_number._doc_no) + " =\'" + this._refDocNo + "\' and " + _g.d.ic_trans_serial_number._last_status + "=0";
                                            DataSet __getCount = __myFrameWork._queryShort(__query);
                                            if (__getCount.Tables.Count > 0)
                                            {
                                                DataTable __find = __getCount.Tables[0];
                                                int __count = (int)MyLib._myGlobal._decimalPhase(__find.Rows[0][0].ToString());
                                                if (__count == 0)
                                                {
                                                    MessageBox.Show(MyLib._myGlobal._resource("หมายเลขเครื่องไม่ตรงกับเอกสารการขาย") + " : " + __serialNumber);
                                                    this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._serial_number, "", false);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                #endregion
                                #region Toe ขาออก เช็ค serial ตามคลัง
                                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                    {
                                        //
                                        string __getWarehouse = (this._getWarehouse != null) ? this._getWarehouse() : "";
                                        string __getLocation = (this._getLocation != null) ? this._getLocation() : "";

                                        if (__getWarehouse != "" && __getLocation != "")
                                        {

                                            // ตรวจว่ามี คลังและทีเก็บตรงหรือไม่
                                            string __query = "select " + _g.d.ic_serial._wh_code + "," + _g.d.ic_serial._shelf_code + "," + _g.d.ic_serial._ic_code + " from " + _g.d.ic_serial._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_serial._serial_number) + "=\'" + __serialNumber + "\' ";
                                            DataSet __getCount = __myFrameWork._queryShort(__query);
                                            if (__getCount.Tables.Count > 0 && __getCount.Tables[0].Rows.Count > 0)
                                            {
                                                string __itemCode = __getCount.Tables[0].Rows[0][_g.d.ic_serial._ic_code].ToString();

                                                string __serialNumberWarehouse = __getCount.Tables[0].Rows[0][_g.d.ic_serial._wh_code].ToString();
                                                string __serialNumberLocation = __getCount.Tables[0].Rows[0][_g.d.ic_serial._shelf_code].ToString();

                                                if (this._itemCode.Equals(__itemCode) == false)
                                                {
                                                    // เตือน ไม่ตรงคลังและที่เก็บ
                                                    MessageBox.Show(MyLib._myGlobal._resource("สินค้า Serial ") + " : " + __serialNumber + " " + MyLib._myGlobal._resource("ไม่ตรงกับเอกสารต้นทาง"));
                                                    this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._serial_number, "", false);
                                                    return;
                                                }

                                                if (__getWarehouse.Equals(__serialNumberWarehouse) == false || __getLocation.Equals(__serialNumberLocation) == false)
                                                {
                                                    // เตือน ไม่ตรงคลังและที่เก็บ
                                                    MessageBox.Show(MyLib._myGlobal._resource("คลังและที่เก็บสินค้า Serial ") + " : " + __serialNumber + " " + MyLib._myGlobal._resource("ไม่ตรงกับเอกสารต้นทาง"));
                                                    this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._serial_number, "", false);
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ Serial ") + " : " + __serialNumber);
                                                this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._serial_number, "", false);

                                            }
                                        }
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        else
                        {
                            this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._serial_number, "", false);
                        }
                    }
                    decimal __price = MyLib._myGlobal._decimalPhase(this._grid._cellGet(row, _g.d.ic_trans_serial_number._price).ToString());
                    if (__price == 0)
                    {
                        this._grid._cellUpdate(row, _g.d.ic_trans_serial_number._price, this._price(), false);
                    }
                }
            }
        }

        void _serialNumberTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this._serialNumberTextBox.Text.Trim().Length > 0)
                {
                    int __addr = this._grid._addRow();
                    this._grid._cellUpdate(__addr, _g.d.ic_trans_serial_number._serial_number, this._serialNumberTextBox.Text.Trim(), true);
                    this._grid._gotoCell(__addr, 0);
                    if (this._autoRunCheckBox.Checked)
                    {
                        this._serialNumberTextBox.Text = MyLib._myGlobal._autoRunningNumberStyleRightToLeft(this._serialNumberTextBox.Text);
                    }
                    else
                    {
                        this._serialNumberTextBox.Text = "";
                    }
                    this._serialNumberTextBox.Focus();
                }
            }
        }

        void _checkAll()
        {
            // pack query check ทีเดียว
            StringBuilder __serialCheck = new StringBuilder();
            for (int __i = 0; __i < this._grid._rowData.Count; __i++)
            {
                //this._grid__alterCellUpdate(null, __i, this._grid._findColumnByName(_g.d.ic_trans_serial_number._serial_number));
                if (__serialCheck.Length > 0)
                {
                    __serialCheck.Append(",");
                }

                string __serialGridGet = this._grid._cellGet(__i, _g.d.ic_trans_serial_number._serial_number).ToString().ToUpper();
                if (this._checkSerialNumber(this._gridRow, __serialGridGet) == false)
                {
                    __serialCheck.Append("\'" + __serialGridGet + "\'");
                }
                else
                {
                    this._grid._cellUpdate(__i, _g.d.ic_trans_serial_number._serial_number, "", false);
                }
            }

            if (__serialCheck.Length > 0)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select " + _g.d.ic_trans_serial_number._serial_number + " from " + _g.d.ic_trans_serial_number._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_serial_number._serial_number) + " in (" + __serialCheck.ToString() + ") and " + MyLib._myGlobal._addUpper(_g.d.ic_trans_serial_number._doc_no) + " not in (\'" + this._docNo + "\',\'" + this._docNoOld + "\') and " + _g.d.ic_trans_serial_number._last_status + "=0";
                DataSet __getCount = __myFrameWork._queryShort(__query);
                if (__getCount.Tables.Count > 0 && __getCount.Tables[0].Rows.Count > 0)
                {
                    DataTable __tableDupSerial = __getCount.Tables[0];
                    for (int __row = 0; __row < __tableDupSerial.Rows.Count; __row++)
                    {
                        string __serial = __tableDupSerial.Rows[__row][_g.d.ic_trans_serial_number._serial_number].ToString();

                        if (_g.g._companyProfile._use_serial_no_duplicate == true)
                        {
                            if (MessageBox.Show("หมายเลขเครื่อง " + __serial + " มีอยู่ในระบบแล้ว ต้องการใช้หมายเลขเครื่องนี้ดำเนินการต่อหรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                            {
                                int __gridRowIndex = this._grid._findData(this._grid._findColumnByName(_g.d.ic_trans_serial_number._serial_number), __serial);
                                if (__gridRowIndex != -1)
                                    this._grid._cellUpdate(__gridRowIndex, _g.d.ic_trans_serial_number._serial_number, "", false);
                            }
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("หมายเลขเครื่องมีอยู่ในระบบแล้ว") + " : " + __serial);
                            int __gridRowIndex = this._grid._findData(this._grid._findColumnByName(_g.d.ic_trans_serial_number._serial_number), __serial);
                            if (__gridRowIndex != -1)
                                this._grid._cellUpdate(__gridRowIndex, _g.d.ic_trans_serial_number._serial_number, "", false);
                        }

                    }
                }

            }
        }

        private void _genSerialButton_Click(object sender, EventArgs e)
        {
            // gen 
            int __count = MyLib._myGlobal._intPhase(this._serialQtyTextbox.Text);
            DateTime __voidDate = _serialVoidDate._dateTime;
            string __detail = this._serialDetailTextbox.Text;
            decimal __price = this._price();

            if (__count > 0 && this._serialStartTextbox.Text != "")
            {
                string __couponStartNumber = this._serialStartTextbox.Text;

                for (int __i = 0; __i < __count; __i++)
                {
                    int __rowIndex = this._grid._addRow();
                    this._grid._cellUpdate(__rowIndex, _g.d.ic_trans_serial_number._serial_number, __couponStartNumber, false);
                    this._grid._cellUpdate(__rowIndex, _g.d.ic_trans_serial_number._void_date, __voidDate, false);
                    this._grid._cellUpdate(__rowIndex, _g.d.ic_trans_serial_number._description, __detail, false);
                    this._grid._cellUpdate(__rowIndex, _g.d.ic_trans_serial_number._price, __price, false);

                    string __nextCouponNumber = MyLib._myGlobal._autoRunningNumberStyleRightToLeft(__couponStartNumber);
                    __couponStartNumber = __nextCouponNumber;

                }

                this._checkAll();
            }

        }
    }
}
