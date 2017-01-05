using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;

namespace DTSClientDownload
{

    public class _gridItemServer : MyLib._myGrid
    {
        public _gridItemServer()
        {
            this._addColumn("Check", 11, 0, 10);
            this._addColumn("code", 1, 25, 20, false, false, true, false, "", "", "", "รหัสสินค้า");
            this._addColumn("barcode", 1, 25, 20, false, false, true, false, "", "", "", "รหัสสินค้า");
            this._addColumn("name_1", 1, 25, 30, false, false, true, false, "", "", "", "ชื่อสินค้า");
            this._addColumn("unit_code", 1, 25, 20, false, false, true, false, "", "", "", "หน่วยนับ");
        }
    }

    public class _gridItemClient : MyLib._myGrid // MyLib._myDataList
    {
        public _gridItemClient()
        {
            this._isEdit = false;
            this._importFromTextFileFastToolStripMenuItem.Visible = false;
            this._importFromTextFileToolStripMenuItem.Visible = false;

            this._addColumn("Check", 11, 0, 10);
            this._addColumn("code", 1, 25, 15, true, false, true, false, "", "", "", "รหัสสินค้า");
            this._addColumn("barcode", 1, 25, 15, true, false, true, false, "", "", "", "บาร์โค๊ต");
            this._addColumn("name1", 1, 25, 30, true, false, true, false, "", "", "", "ชื่อสินค้า");
            this._addColumn("defstkunitcode", 1, 25, 20, true, false, true, false, "", "", "", "หน่วยนับ");
            this._addColumn("unittype", 1, 25, 10, true, false, true, false, "", "", "", "-");

            // field ซ่อน
            this._addColumn("item_detail", 12, 0, 0, false, true);
            this._addColumn("item_unit", 12, 0, 0, false, true);
            this._addColumn("item_stkpacking", 12, 0, 0, false, true);

            this._addColumn("is_dup_itemcode", 1, 25, 10, false, true, false);
            this._addColumn("is_dup_name", 1, 25, 10, false, true, false);
            this._addColumn("is_dup_barcode", 1, 25, 10, false, true, false);


            this._addColumn(_g.DataClient.dts_bcitem._groupcode, 1, 25, 0, true, true, true);
            this._addColumn(_g.DataClient.dts_bcitem._typecode, 1, 25, 0, true, true, true);
            this._addColumn(_g.DataClient.dts_bcitem._categorycode, 1, 25, 0, true, true, true);
            this._addColumn(_g.DataClient.dts_bcitem._formatcode, 1, 25, 0, true, true, true);
            //this._button.Visible = false;
            //this._buttonHelp.Visible = false;
            //this._buttonClose.Visible = false;
            //this._buttonNew.Visible = false;
            //this._buttonDelete.Visible = false;
            //this._buttonNewFromTemp.Visible = false;

            ////this._gridData.IsEdit = true;

            //this._gridData._addColumn("Check", 11, 0, 10);
            //this._gridData._addColumn("code", 1, 25, 20, true, false, true, false, "", "", "", "รหัสสินค้า");
            //this._gridData._addColumn("barcode", 1, 25, 20, true, false, true, false, "", "", "", "รหัสสินค้า");
            //this._gridData._addColumn("name_1", 1, 25, 30, true, false, true, false, "", "", "", "ชื่อสินค้า");
            //this._gridData._addColumn("unit_code", 1, 25, 20, true, false, true, false, "", "", "", "หน่วยนับ");

            //this._gridData._width_by_persent = true;
            //this.Invalidate();

            this._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridItemClient__beforeDisplayRow);
        }

        MyLib.BeforeDisplayRowReturn _gridItemClient__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            int __columnDupCode = this._findColumnByName("is_dup_itemcode");
            int __columnDupName = this._findColumnByName("is_dup_name");
            int __columnDupBarcode = this._findColumnByName("is_dup_barcode");

            if (__columnDupCode != 1 && __columnDupName != -1 && __columnDupBarcode != -1)
            {
                string __dupCdoe = this._cellGet(row, __columnDupCode).ToString();
                string __dupName = this._cellGet(row, __columnDupName).ToString();
                string __dupBarcode = this._cellGet(row, __columnDupBarcode).ToString();

                if (__dupCdoe.Equals("1") || __dupName.Equals("1") || __dupBarcode.Equals("1"))
                {
                    senderRow.newColor = Color.Red;
                }
                //switch (__type)
                //{
                //    case 2:
                //        senderRow.newColor = Color.Magenta;
                //        break;
                //    case 3:
                //        senderRow.newColor = Color.MediumVioletRed;
                //        break;
                //    case 6:
                //        senderRow.newColor = Color.DarkOrange;
                //        break;
                //    case 7:
                //        senderRow.newColor = Color.Blue;
                //        break;
                //    default:
                //        senderRow.newColor = Color.Black;
                //        break;
                //}
                //{
                //    senderRow.newColor = Color.YellowGreen;
                //}
            }

            return (senderRow);
        }
    }

    public enum _screenDownloadEnum
    {
        None,
        PO,
        SO
    }

    public class _gridTransDetailImport : MyLib._myGrid
    {
        private _screenDownloadEnum _type = _screenDownloadEnum.None;
        public _screenDownloadEnum _screen_type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
                _build();
            }
        }

        public _gridTransDetailImport()
        {
            this.AddRow = false;
            this._importFromTextFileFastToolStripMenuItem.Visible = false;
            this._importFromTextFileToolStripMenuItem.Visible = false;
        }

        object[] _warehouseList;
        object[] _locationList;

        private void _build()
        {
            if (DesignMode == false)
            {
                _getWareHouseList();
                /*
                switch (this._type)
                {
                    case _screenDownloadEnum.PO:
                 * */
                this._columnList.Clear();
                this._addColumn("item_code", 1, 25, 20, false, false, true, false, "", "", "", "รหัสสินค้า");
                this._addColumn("item_name", 1, 25, 30, false, false, true, false, "", "", "", "ชื่อสินค้า");
                this._addColumn("wh_code", 10, 25, 20, true, false, true, false, "", "", "", "คลังสินค้า");
                this._addColumn("lc_code", 10, 25, 20, true, false, true, false, "", "", "", "ที่เก็บ");

                this._addColumn("wh_name", 1, 25, 20, false, true, true, false, "", "", "", "ชื่อคลังสินค้า");
                this._addColumn("location_name", 1, 25, 20, false, true, true, false, "", "", "", "ชื่อที่เก็บ");

                this._addColumn("unit_code", 1, 25, 15, false, false, true, false, "", "", "", "หน่วยนับ");
                this._addColumn("qty", 3, 25, 20, false, false, true, false, "#,###.00", "", "", "จำนวน");
                this._addColumn("price", 3, 25, 20, false, false, true, false, "#,###.00", "", "", ((this._screen_type == _screenDownloadEnum.PO) ? "ราคาซื้อ" : "ราคา/หน่วย"));
                this._addColumn("amount", 3, 25, 20, false, false, true, false, "#,###.00", "", "", "จำนวนเงิน");

                this._addColumn("netamount", 3, 25, 20, false, true, true);
                this._addColumn("taxrate", 3, 25, 20, false, true, true);
                this._addColumn("rate1", 3, 25, 20, false, true, true);
                this._addColumn("rate2", 3, 25, 20, false, true, true);

                this._addColumn("detailclass", 12, 25, 20, false, true, true);

                //this._addColumn("trans_detail", 12, 0, 0, true, true, false, false, "", "", "");
                //this._cellComboBoxItem -= new MyLib.CellComboBoxItemEventHandler(_gridTransDetailImport__cellComboBoxItem);
                //this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_gridTransDetailImport__cellComboBoxItem);

                //this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_gridTransDetailImport__cellComboBoxGet);

                this._calcPersentWidthToScatter();
                /*
                        break;
                    case _screenDownloadEnum.SO:
                        this._addColumn("item_code", 1, 25, 20, true, false, true, false, "", "", "", "รหัสสินค้า");
                        this._addColumn("item_name", 1, 25, 30, true, false, true, false, "", "", "", "ชื่อสินค้า");
                        this._addColumn("wh_code", 10, 25, 20, true, false, true, false, "", "", "", "คลังสินค้า");
                        this._addColumn("lc_code", 10, 25, 20, true, false, true, false, "", "", "", "ที่เก็บ");
                        this._addColumn("unit_code", 1, 25, 15, true, false, true, false, "", "", "", "หน่วยนับ");
                        this._addColumn("qty", 3, 25, 20, true, false, true, false, "", "", "", "จำนวน");
                        this._addColumn("price", 3, 25, 20, true, false, true, false, "", "", "", "ราคา/หน่วย");
                        this._addColumn("amount", 3, 25, 20, true, false, true, false, "", "", "", "จำนวนเงิน");

                        //this._addColumn("trans_detail", 12, 0, 0, true, true, false, false, "", "", "");

                        this._calcPersentWidthToScatter();
                        break;
                }*/
            }
        }

        string _gridTransDetailImport__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            string __result = "";

            if (columnName == "wh_code")
            {
                if (this._cellGet(row, columnName).ToString() == "")
                    __result = "";
                __result = (_warehouseList != null && _warehouseList.Length > select) ? _warehouseList[select].ToString() : "";
                this._cellUpdate(row, "wh_name", __result, true);
                _getLocationList(row);
            }
            else if (columnName == "lc_code")
            {
                if (this._cellGet(row, columnName).ToString() == "")
                    __result = "";
                __result = (_locationList != null && _locationList.Length > select) ? _locationList[select].ToString() : "";

                this._cellUpdate(row, "location_name", __result, true);

            }
            return __result;
        }

        void _getWareHouseList()
        {
            _clientFrameWork __fw = new _clientFrameWork();
            DataSet __result = __fw._query("select code + '~' + name from  BCWarehouse");
            List<object> __warehouseList = new List<object>();
            //__warehouseList.Add("");
            if (__result.Tables.Count > 0)
            {
                for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
                {
                    __warehouseList.Add(__result.Tables[0].Rows[__i][0].ToString());
                }
            }

            _warehouseList = __warehouseList.ToArray();
        }

        void _getLocationList(int row)
        {
            string __query = "select code + '~' + name from  bcshelf where WHCode=\'" + _global._getValue(this._cellGet(row, "wh_name").ToString()) + "\'";

            _clientFrameWork __fw = new _clientFrameWork();
            DataSet __result = __fw._query(__query);
            List<string> __shelfList = new List<string>();

            for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
            {
                __shelfList.Add(__result.Tables[0].Rows[__i][0].ToString());
            }

            _locationList = __shelfList.ToArray();
        }

        object[] _gridTransDetailImport__cellComboBoxItem(object sender, int row, int column)
        {

            object[] __obj = null;

            if (row < this._rowData.Count)
            {
                int __columnWareHouse = this._findColumnByName("wh_code");
                int __columnShelf = this._findColumnByName("lc_code");

                if (column == __columnWareHouse)
                {
                    return _warehouseList;
                }
                else if (column == __columnShelf)
                {

                    return _locationList;
                }
            }

            return __obj;
        }
    }

    public class _gridTransImport : MyLib._myGrid // MyLib._myDataList
    {
        private _screenDownloadEnum _type = _screenDownloadEnum.None;
        public _screenDownloadEnum _screen_type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
                _build();
            }
        }

        public _gridTransImport()
        {
            this._importFromTextFileFastToolStripMenuItem.Visible = false;
            this._importFromTextFileToolStripMenuItem.Visible = false;

            this._isEdit = false;
            //this._button.Visible = false;
            //this._buttonHelp.Visible = false;
            //this._buttonClose.Visible = false;
            //this._buttonNew.Visible = false;
            //this._buttonDelete.Visible = false;
            //this._buttonNewFromTemp.Visible = false;

            //this._gridData.IsEdit = true;

            //this._gridData._addColumn("Check", 11, 0, 10);
            //this._gridData._addColumn("doc_no", 1, 25, 20, true, false, true, false, "", "", "", "เลขที่เอกสาร");
            //this._gridData._addColumn("doc_date", 1, 25, 20, true, false, true, false, "", "", "", "วันที่เอกสาร");
            //this._gridData._addColumn("cust_code", 1, 25, 30, true, false, true, false, "", "", "", "ชื่อตัวแทน");
            //this._gridData._addColumn("doc_time", 1, 25, 20, true, false, true, false, "", "", "", "เวลา");

            //this._gridData._width_by_persent = true;
            //this.Invalidate();
        }

        public void _build()
        {
            if (DesignMode == false)
            {
                switch (this._type)
                {
                    case _screenDownloadEnum.PO:
                        this._addColumn("Check", 11, 0, 10);
                        this._addColumn("docno", 1, 25, 25, true, false, true, false, "", "", "", "เลขที่เอกสาร");
                        this._addColumn("docdate", 4, 25, 25, true, false, true, false, "", "", "", "วันที่เอกสาร");
                        this._addColumn("mydescription", 1, 25, 40, true, false, true, false, "", "", "", "กลุ่มสินค้า");

                        this._addColumn("trans_detail", 12, 0, 0, true, true, false, false, "", "", "");
                        this._addColumn("transitem", 12, 0, 0, true, true, false, false, "", "", "");

                        break;
                    case _screenDownloadEnum.SO:
                        this._addColumn("Check", 11, 0, 10);
                        this._addColumn("docno", 1, 25, 20, true, false, true, false, "", "", "", "เลขที่เอกสาร");
                        this._addColumn("docdate", 1, 25, 20, true, false, true, false, "", "", "", "วันที่เอกสาร");
                        this._addColumn("ownreceive", 1, 25, 30, true, false, true, false, "", "", "", "ผู้รับสินค้า");
                        this._addColumn("mydescription", 1, 25, 20, true, false, true, false, "", "", "", "หมายเหตุ");

                        this._addColumn("trans_detail", 12, 0, 0, true, true, false, false, "", "", "");
                        this._addColumn("transitem", 12, 0, 0, true, true, false, false, "", "", "");

                        break;
                }
            }
        }
    }

    /// <summary>
    /// จอเลือกหน่วยนับ
    /// </summary>
    public class _screenicUnit : MyLib._myScreen
    {
        public string _itemCode = "";
        public string _itemBarcode = "";

        public _screenicUnit()
        {
            // ดึงหน่วนนับจาก all unit code

            this._maxColumn = 2;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_inventory._unit_standard, 1, 255, 0, true, false, true, true, true, "หน่วยนับมาตรฐาน");
            this._addComboBox(0, 1, _g.d.ic_inventory._unit_type, true, new string[] { "หน่วยนับเดียว", "หลายหน่วยนับ" }, true, "ประเภทหน่วยนับ");

            this._addComboBox(1, 0, _g.d.ic_inventory_detail._start_sale_unit, true, new string[] { "" }, false, "หน่วยนับขาย");
            //this._addTextBox(1, 0, 1, 0, _g.d.ic_inventory_detail._start_sale_unit, 1, 255, 1, true, false, true, true, true, "หน่วยนับขาย");
            this._addTextBox(1, 1, 1, 0, _g.d.ic_inventory._unit_standard_stand_value, 1, 255, 0, true, false, true, true, true, "อัตราส่วน");

            this._addComboBox(2, 0, _g.d.ic_inventory_detail._start_purchase_unit, true, new string[] { "" }, false, "หน่วยนับซื้อ");
            //this._addTextBox(2, 0, 1, 0, _g.d.ic_inventory_detail._start_purchase_unit, 1, 255, 1, true, false, true, true, true, "หน่วยนับซื้อ");
            this._addTextBox(2, 1, 1, 0, _g.d.ic_inventory._unit_standard_divide_value, 1, 255, 0, true, false, true, true, true, "อัตราส่วน");

            this._enabedControl(_g.d.ic_inventory._unit_standard_stand_value, false);
            this._enabedControl(_g.d.ic_inventory._unit_standard, false);
            this._enabedControl(_g.d.ic_inventory._unit_standard_divide_value, false);

            this._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(_screenicUnit__comboBoxSelectIndexChanged);
        }

        void _screenicUnit__comboBoxSelectIndexChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory._unit_type))
            {
                if (this._getDataStr(_g.d.ic_inventory._unit_type).Equals("1"))
                {
                    this._enabedControl(_g.d.ic_inventory_detail._start_purchase_unit, true);
                    this._enabedControl(_g.d.ic_inventory_detail._start_sale_unit, true);
                }
                else
                {
                    this._enabedControl(_g.d.ic_inventory_detail._start_purchase_unit, false);
                    this._enabedControl(_g.d.ic_inventory_detail._start_sale_unit, false);
                }
            }
        }

    }

    /// <summary>
    /// จอเลือกคลังและที่เก็บสินค้า
    /// </summary>
    public class _screenIcWarehouseLocation : MyLib._myScreen
    {
        public string _itemCode = "";
        public string _itemBarcode = "";

        public _screenIcWarehouseLocation()
        {
            // get warehouse
            _clientFrameWork __fw = new _clientFrameWork();
            DataSet __result = __fw._query("select code + '~' + name from  BCWarehouse");
            List<string> __warehouseList = new List<string>();
            //__warehouseList.Add("");
            if (__result.Tables.Count > 0)
            {
                for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
                {
                    __warehouseList.Add(__result.Tables[0].Rows[__i][0].ToString());
                }
            }

            this._addComboBox(0, 0, _g.d.ic_inventory_detail._start_purchase_wh, true, __warehouseList.ToArray(), false, "คลังเริ่มต้นซื้อ");
            this._addComboBox(0, 1, _g.d.ic_inventory_detail._start_purchase_shelf, true, new string[] { "" }, false, "ที่เก็บเริ่มต้นซื้อ");

            this._addComboBox(1, 0, _g.d.ic_inventory_detail._start_sale_wh, true, __warehouseList.ToArray(), false, "คลังเริ่มต้นขาย");
            this._addComboBox(1, 1, _g.d.ic_inventory_detail._start_sale_shelf, true, new string[] { "" }, false, "ทีเก็บเริ่มต้นขาย");

            this._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(_screenIcWarehouseLocation__comboBoxSelectIndexChanged);
        }

        void _screenIcWarehouseLocation__comboBoxSelectIndexChanged(object sender, string name)
        {
            string __query = "";
            if (name.Equals(_g.d.ic_inventory_detail._start_purchase_wh) || name.Equals(_g.d.ic_inventory_detail._start_sale_wh))
            {
                __query = "select code + '~' + name from  bcshelf where WHCode=\'" + this._getDataComboStr(name, false).Split('~')[0] + "\'";

                _clientFrameWork __fw = new _clientFrameWork();
                DataSet __result = __fw._query(__query);
                List<string> __warehouseList = new List<string>();

                MyLib._myComboBox __combobox;
                if (name.Equals(_g.d.ic_inventory_detail._start_purchase_wh))
                {
                    __combobox = (MyLib._myComboBox)this._getControl(_g.d.ic_inventory_detail._start_purchase_shelf);
                }
                else
                {
                    __combobox = (MyLib._myComboBox)this._getControl(_g.d.ic_inventory_detail._start_sale_shelf);
                }

                if (__result.Tables.Count > 0 && __combobox != null)
                {
                    __combobox.Items.Clear();

                    if (__result.Tables[0].Rows.Count > 0)
                    {

                        for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
                        {
                            __combobox.Items.Add(__result.Tables[0].Rows[__i][0].ToString());
                        }
                    }
                }
            }

        }
    }

    public class _itemRelateStruct
    {
        public List<_itemRelateDetailStruct> _details = new List<_itemRelateDetailStruct>();
    }

    public class _itemRelateDetailStruct
    {
        public string _itemCode = "";
        public string _itemBarcode = "";
        public string _unit_Standard = "";
        public int _unit_type = 0;
        public string _sale_unit_code = "";
        public string _purchase_unit_codce = "";
        public float _sale_rate = 1;
        public float _purchase_rate = 1;
        public string _purchase_wh_code = "";
        public string _purchase_lc_code = "";
        public string _sale_wh_code = "";
        public string _sale_lc_code = "";
    }

    /// <summary>
    /// list หน่วยนับที่ใช้กับสินค้า
    /// </summary>
    public class _itemUnitCodeList
    {
        public List<_itemUnitCode> _details = new List<_itemUnitCode>();
    }

    /// <summary>
    /// หน่วยนับที่ใช้กับสินค้า
    /// </summary>
    public class _itemUnitCode
    {
        public string _unitCode = "";
        public int _isStkUnit = 0;
        public decimal _rate = 0M;
    }

    /// <summary>
    /// List Stock Packing
    /// </summary>
    public class _stkPackingList
    {
        public List<_stkPacking> _details = new List<_stkPacking>();
    }

    /// <summary>
    /// รายละเอียด Stock Packing
    /// </summary>
    public class _stkPacking
    {
        public string _unitCode = "";
        public decimal _rate1 = 1;
        public decimal _rate2 = 1;
    }

    public class _trans
    {
        public string _docno = "";
        public DateTime _docdate = new DateTime();
        public int _taxtype = 0;
        public int _taxRate = 0;
        public string _aparcode = "";
        public string _agent_code = "";
        public decimal _sumofitemamount = 0M;
        public decimal _afterdiscount
        {
            get
            {
                return _sumofitemamount;
            }
        }
        public decimal _beforetaxamount
        {
            get
            {
                return _totalamount - _taxamount;
            }
        }

        public decimal _taxamount = 0M;

        public decimal _beforetaxamountGet
        {
            get
            {
                decimal __result = 0M;

                if (_taxtype == 0)
                    __result = _sumofitemamount;

                if (_taxtype == 1)
                {
                    if (_taxRate > 0)
                    {
                        __result = MyLib._myGlobal._round((_sumofitemamount * 100.0M) / (100.0M + _taxRate), 2);
                    }
                    else
                    {
                        __result = _sumofitemamount;
                    }
                }

                return __result;

            }
        }

        /// <summary>
        /// มูลค่าภาษีเกิดจากการคำณวน
        /// </summary>
        public decimal _taxAmountGet
        {
            get
            {
                decimal __result = 0M;

                if (_taxtype == 0)
                {
                    if (_taxRate > 0)
                    {
                        __result = MyLib._myGlobal._round((_sumofitemamount * (_taxRate / 100.0M)), 2);
                    }
                    else
                    {
                        __result = _sumofitemamount;
                    }
                }
                else if (_taxtype == 1)
                {
                    if (_taxRate > 0)
                    {
                        __result = _sumofitemamount - MyLib._myGlobal._round(((_sumofitemamount * 100.0M) / (100.0M + _taxRate)), 2);
                    }
                    else
                    {
                        __result = _sumofitemamount;
                    }
                }

                return __result;
            }
        }

        /// <summary>
        /// ยอดรวมจากการคำณวน
        /// </summary>
        public decimal _totalAmountGet
        {
            get
            {
                return (_sumofitemamount + _taxAmountGet);

            }
        }

        /// <summary>
        /// มูลค่าสุทธิ จากการคำณวน
        /// </summary>
        public decimal _netamountGet
        {
            get
            {
                return (_sumofitemamount + _taxAmountGet);
            }
        }


        public decimal _totalamount = 0M;
        public decimal _excepttaxamount
        {
            get
            {
                return 0M;
            }
        }

        public decimal _zerotaxamount
        {
            get
            {
                return 0M;
            }
        }

        public decimal _netamount = 0M;
        public int _leadtime = 0;
        public DateTime _leaddate
        {
            get
            {
                return _docdate.AddDays(_leadtime);
            }
        }

        public int _creditday = 0;
        public DateTime _duedate
        {
            get
            {
                return _docdate.AddDays(this._creditday);
            }
        }

        public int _deliveryDay = 0;
        public DateTime _deliveryDate
        {
            get
            {
                return _docdate.AddDays(this._deliveryDay);
            }
        }

        public int _isConditionSend = 0;

        public int _billType = 0;
        public string _ownreceive = "";
        public string _myDescription = "";

    }

    public class _trans_detail_list
    {
        public List<_trans_detail> _detail = new List<_trans_detail>();
    }

    public class _trans_detail
    {
        public string _docNo = "";
        public string _itemCode = "";
        public string _itemName = "";
        public string _ap_ar_code = "";
        public string _agent_code = "";
        public string _unitCode = "";
        public decimal _qty = 0M;
        public decimal _price = 0M;
        public decimal _amount = 0M;
        public decimal _netamount = 0M;
        public decimal _taxrate = 0M;
        public decimal _rate1 = 0M;
        public decimal _rate2 = 0M;

        public string _wh_code = "";
        public string _shelf_code = "";

        public string groupcode = "";
        public string typecode = "";
        public string categorycode = "";
        public string formatcode = "";

        public int _billtype = 0;
        public DateTime _OriginalDate = new DateTime();

        public string _DefSaleWHCode = "";
        public string _DefSaleShelf = "";
        public string _DefBuyWHCode = "";
        public string _DefBuyShelf = "";

    }

}
