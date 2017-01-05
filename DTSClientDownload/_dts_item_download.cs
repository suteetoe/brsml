using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DTSClientDownload
{
    public partial class _dts_item_download : UserControl
    {
        public int _clientItemSelect = -1;
        Boolean _checkChangeIndex = false;
        DataTable _shelfDataTable;

        public _dts_item_download()
        {
            InitializeComponent();

            _loadWhLocation();

            this.Load += new EventHandler(_dts_item_download_Load);
            this._gridItemClient1._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_gridItemClient1__alterCellUpdate);
            this._gridItemClient2._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_gridItemClient2__alterCellUpdate);
            this._gridItemClient2._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_gridItemClient2__afterSelectRow);
            this._gridItemClient2._mouseClick += new MyLib.MouseClickHandler(_gridItemClient2__mouseClick);
            this._gridItemClient1._mouseClick += new MyLib.MouseClickHandler(_gridItemClient1__mouseClick);
            this._screenicUnit1._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(__comboBoxSelectIndexChanged);
            this._screenIcWarehouseLocation1._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(__comboBoxSelectIndexChanged);
            this._checkEnableSaveButton();

            this._importButton.Enabled = false;
            this._exportButton.Enabled = false;
            
        }

        void _gridItemClient2__alterCellUpdate(object sender, int row, int column)
        {
            _checkExportButton();
        }

        void _checkExportButton()
        {
            Boolean __pass = false;
            for (int __i = 0; __i < this._gridItemClient2._rowData.Count; __i++)
            {
                if (this._gridItemClient2._cellGet(__i, 0).ToString().Equals("1"))
                {
                    __pass = true;
                    break;
                }
            }

            this._exportButton.Enabled = __pass;
        }

        void _gridItemClient1__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // show detail duplicate
            this._labelItemResult.Text = "";

            // get value
            string __dupCdoe = this._gridItemClient1._cellGet(e._row, "is_dup_itemcode").ToString();
            string __dupName = this._gridItemClient1._cellGet(e._row, "is_dup_name").ToString();
            string __dupBarcode = this._gridItemClient1._cellGet(e._row, "is_dup_barcode").ToString();

            string __itemCode = this._gridItemClient1._cellGet(e._row, _g.DataClient.dts_bcitem._code).ToString();
            string __itemName = this._gridItemClient1._cellGet(e._row, _g.DataClient.dts_bcitem._name1).ToString();
            string __itemBarcode = this._gridItemClient1._cellGet(e._row, _g.DataClient.dts_bcitem._barcode).ToString();

            if (__dupCdoe.Equals("1") || __dupName.Equals("1"))
            {
                // get old code

                // ไป qeuy มา 
                _clientFrameWork __clientFrameWork = new _clientFrameWork();
                DataSet __result = __clientFrameWork._query("select code, name1 from bcitem where bcitem.code = '" + __itemCode + "' or bcitem.name1 = '" + __itemName + "' ");
                if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                {
                    this._labelItemResult.Text = "พบข้อมูลการซ้ำสินค้า รหัส " + __result.Tables[0].Rows[0]["code"].ToString() + " ชื่อ " + __result.Tables[0].Rows[0]["name1"].ToString() + "";
                }
            }
            else if (__dupBarcode.Equals("1"))
            {
                // get barcode
                _clientFrameWork __clientFrameWork = new _clientFrameWork();
                DataSet __result = __clientFrameWork._query("select itemcode, barcodename from bcitem where bcitem.code = '" + __itemCode + "' or bcitem.name1 = '" + __itemName + "' ");
                if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                {

                    this._labelItemResult.Text = "พบข้อมูลการซ้ำของบาร์โค๊ด รหัส " + __result.Tables[0].Rows[0]["itemcode"].ToString() + " บาร์โค๊ด " + __result.Tables[0].Rows[0]["barcodename"].ToString();
                }
            }
        }

        void _showAllItemCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            this._loadData();
        }

        void _checkEnableSaveButton()
        {
            if (this._gridItemClient2._rowData.Count > 0)
            {
                this._saveButton.Enabled = true;
            }
            else
            {
                this._saveButton.Enabled = false;
            }
        }

        void _disSelectServerToolStripButton_Click(object sender, System.EventArgs e)
        {
            for (int __i = 0; __i < this._gridItemClient1._rowData.Count; __i++)
            {
                this._gridItemClient1._cellUpdate(__i, 0, 0, true);
            }
            this._gridItemClient1.Refresh();
        }

        string _getTempShelf(string __whCode)
        {
            try
            {
                DataRow[] __rowSelect = _shelfDataTable.Select("whcode=" + _global._getValue(__whCode));
                if (__rowSelect.Length > 0)
                {
                    return __rowSelect[0]["code"].ToString() + "~" + __rowSelect[0]["name"].ToString();
                }
                else
                {
                    return _shelfDataTable.Rows[0]["code"].ToString() + "~" + _shelfDataTable.Rows[0]["name"].ToString();
                }
            }
            catch
            {
            }
            return _shelfDataTable.Rows[0]["code"].ToString() + "~" + _shelfDataTable.Rows[0]["name"].ToString();
        }

        string _getTempWH()
        {
            return _shelfDataTable.Rows[0]["whcode"].ToString() + "~" + _shelfDataTable.Rows[0]["whname"].ToString();
        }

        void _selectedAllClientToolStripButton_Click(object sender, System.EventArgs e)
        {
            //string __message = "ท่านตกลงให้โปรแกรมบันทึกข้อมูลสินค้าทั้งหมดจริงหรือไม่? \n- เป็นสินค้าหน่วยนับเดียวทั้งหมด \n- มีรหัสคลัง/ที่เก็บเริ่มต้นเดียวกันทั้งหมด";
            string __message = "ต้องการเลือกข้อมูลทั้งหมดหรือไม่";
            if (MessageBox.Show(__message, DTSClientDownload._global._champMessageCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            //string __purchase_wh_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_purchase_wh, false); // "";
            //string __purchase_lc_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_purchase_shelf, false); // "";
            //string __sale_wh_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_sale_wh, false); // "";
            //string __sale_lc_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_sale_shelf, false);// "";

            for (int __i = 0; __i < this._gridItemClient2._rowData.Count; __i++)
            {
                this._gridItemClient2._cellUpdate(__i, 0, 1, true);

                // update all wh shelf
                //_itemRelateDetailStruct __detail = (_itemRelateDetailStruct)this._gridItemClient2._cellGet(__i, "item_detail");

                //__detail._purchase_wh_code = (__purchase_wh_code != "") ? __purchase_wh_code : _getTempWH();
                //__detail._purchase_lc_code = (__purchase_lc_code != "") ? __purchase_lc_code : _getTempShelf(__purchase_wh_code);
                //__detail._sale_wh_code = (__sale_wh_code != "") ? __sale_wh_code : _getTempWH();
                //__detail._sale_lc_code = (__sale_lc_code != "") ? __sale_lc_code : _getTempShelf(__sale_wh_code);


            }
            this._gridItemClient2.Refresh();
        }

        void _disSelectClientToolStripButton_Click(object sender, System.EventArgs e)
        {
            for (int __i = 0; __i < this._gridItemClient2._rowData.Count; __i++)
            {
                this._gridItemClient2._cellUpdate(__i, 0, 0, true);
            }
            this._gridItemClient2.Refresh();
        }

        void _selectAllToolstripButton_Click(object sender, System.EventArgs e)
        {
            _selectAllItem();
        }

        void _selectAllItem()
        {
            for (int __i = 0; __i < this._gridItemClient1._rowData.Count; __i++)
            {
                this._gridItemClient1._cellUpdate(__i, 0, 1, true);
            }
            this._gridItemClient1.Refresh();
        }

        // กด <-
        void _exportButton_Click(object sender, System.EventArgs e)
        {
            //for (int __i = 0; __i < this._gridItemClient1._rowData.Count; __i++)
            //{
            //    this._gridItemClient1._cellUpdate(__i, 0, 0, true);
            //}
            //this._gridItemClient1.Refresh();

            for (int __i = this._gridItemClient2._rowData.Count - 1; __i >= 0; __i--)
            {
                if (this._gridItemClient2._cellGet(__i, 0).ToString().Equals("1"))
                {
                    this._gridItemClient2._rowData.RemoveAt(__i);
                }
            }
            this._gridItemClient2.Refresh();
            this._screenicUnit1._clear();
            this._screenIcWarehouseLocation1._clear();
            this._checkEnableSaveButton();
            _checkExportButton();

        }

        // กด ->
        void _importButton_Click(object sender, System.EventArgs e)
        {
            //_selectAllItem();
            for (int __i = 0; __i < this._gridItemClient1._rowData.Count; __i++)
            {
                if (this._gridItemClient1._cellGet(__i, 0).ToString().Equals("1"))
                {
                    _addSelectItem(__i);
                }
                //else
                //{
                //    _removeSelectItem(__i);
                //}
            }

            // หลังจาก insert เสร็จก็ปล่อยรายการออกไป
            //for (int __i = 0; __i < this._gridItemClient1._rowData.Count; __i++)
            //{
            //    this._gridItemClient1._cellUpdate(__i, 0, 0, false);
            //}
            //this._gridItemClient1.Refresh();
            this._checkEnableSaveButton();
        }

        void _loadWhLocation()
        {
            _clientFrameWork __myFrameWork = new _clientFrameWork();

            List<string> __query = new List<string>();
            __query.Add("select code,name from bcwarehouse");
            __query.Add("select whcode, (select bcwarehouse.name from bcwarehouse where bcwarehouse.code = bcshelf.whcode) as whname, code, name from bcshelf");

            ArrayList __result = __myFrameWork._getDataList(__query);
            if (__result.Count > 0)
            {
                _shelfDataTable = ((DataSet)__result[1]).Tables[0];
            }
        }

        void __comboBoxSelectIndexChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_inventory._unit_type) ||
                name.Equals(_g.d.ic_inventory_detail._start_sale_unit) ||
                name.Equals(_g.d.ic_inventory_detail._start_purchase_unit))
            //||
            //name.Equals(_g.d.ic_inventory_detail._start_purchase_wh) ||
            //name.Equals(_g.d.ic_inventory_detail._start_purchase_shelf) ||
            //name.Equals(_g.d.ic_inventory_detail._start_sale_wh) ||
            //name.Equals(_g.d.ic_inventory_detail._start_sale_shelf))
            {
                // save struct กลับไป
                if (_checkChangeIndex)
                {
                    int __selectRow = this._gridItemClient2._selectRow;

                    if (name.Equals(_g.d.ic_inventory_detail._start_sale_unit) ||
                        name.Equals(_g.d.ic_inventory_detail._start_purchase_unit))
                    {
                        // get rate
                        _itemUnitCodeList __unitList = (_itemUnitCodeList)this._gridItemClient2._cellGet(__selectRow, "item_unit");
                        decimal __rate = 0M;
                        for (int __i = 0; __i < __unitList._details.Count; __i++)
                        {
                            string __unit_code = this._screenicUnit1._getDataComboStr(name, false);
                            if (__unitList._details[__i]._unitCode.Equals(__unit_code))
                            {
                                __rate = __unitList._details[__i]._rate;
                                break;
                            }
                        }

                        if (name.Equals(_g.d.ic_inventory_detail._start_sale_unit))
                        {
                            this._screenicUnit1._setDataStr(_g.d.ic_inventory._unit_standard_stand_value, __rate.ToString());
                        }
                        else
                        {
                            this._screenicUnit1._setDataStr(_g.d.ic_inventory._unit_standard_divide_value, __rate.ToString());
                        }
                    }

                    _itemRelateDetailStruct __detail = (_itemRelateDetailStruct)this._gridItemClient2._cellGet(__selectRow, "item_detail");
                    __detail._unit_type = (this._screenicUnit1._getDataStr(_g.d.ic_inventory._unit_type).Equals("0")) ? 0 : 1;
                    __detail._sale_unit_code = this._screenicUnit1._getDataComboStr(_g.d.ic_inventory_detail._start_sale_unit, false);
                    __detail._purchase_unit_codce = this._screenicUnit1._getDataComboStr(_g.d.ic_inventory_detail._start_purchase_unit, false);

                    //__detail._purchase_wh_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_purchase_wh, false);
                    //__detail._purchase_lc_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_purchase_shelf, false);
                    //__detail._sale_wh_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_sale_wh, false);
                    //__detail._sale_lc_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_sale_shelf, false);
                }
            }

        }

        void _gridItemClient2__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row < this._gridItemClient2._rowData.Count)
            {
                _loadItemDetail(e._row);
            }
        }

        void _gridItemClient2__afterSelectRow(object sender, int row)
        {
            // load struct to detail form
            // check enable export

            _loadItemDetail(row);
        }

        void _loadItemDetail(int row)
        {
            _itemRelateDetailStruct __detail = (_itemRelateDetailStruct)this._gridItemClient2._cellGet(row, "item_detail");
            if (__detail == null)
            {
                __detail = new _itemRelateDetailStruct();
                // get default ไปด้วยเลย
            }

            _loadAllUnitUse(row);
            _loadDetail(__detail);

        }

        void _loadAllUnitUse(int row)
        {
            // เอา class มา gen เป็น unit and rate

            _itemUnitCodeList __unitList = (_itemUnitCodeList)this._gridItemClient2._cellGet(row, "item_unit");
            ((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_purchase_unit)).Items.Clear();
            //((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_purchase_unit)).height

            ((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_sale_unit)).Items.Clear();
            //((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_sale_unit)).DropDownHeight = 19;
            if (__unitList != null)
            {
                for (int __i = 0; __i < __unitList._details.Count; __i++)
                {
                    ((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_purchase_unit)).Items.Add(__unitList._details[__i]._unitCode);
                    ((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_sale_unit)).Items.Add(__unitList._details[__i]._unitCode);

                }
            }
        }

        void _loadDetail(_itemRelateDetailStruct detail)
        {
            this._checkChangeIndex = false;

            this._screenicUnit1._itemCode = detail._itemCode;
            this._screenicUnit1._itemBarcode = detail._itemBarcode;

            this._screenIcWarehouseLocation1._itemCode = detail._itemCode;
            this._screenIcWarehouseLocation1._itemBarcode = detail._itemBarcode;

            this._screenicUnit1._setDataStr(_g.d.ic_inventory._unit_standard, detail._unit_Standard);
            this._screenicUnit1._setComboBox(_g.d.ic_inventory._unit_type, detail._unit_type);

            if (detail._sale_unit_code == "")
                this._screenicUnit1._clearComcoBox(_g.d.ic_inventory_detail._start_sale_unit, false);
            else
                this._screenicUnit1._setComboBox(_g.d.ic_inventory_detail._start_sale_unit, detail._sale_unit_code);
            this._screenicUnit1._setDataStr(_g.d.ic_inventory._unit_standard_stand_value, detail._sale_rate.ToString());

            if (detail._purchase_unit_codce == "")
                this._screenicUnit1._clearComcoBox(_g.d.ic_inventory_detail._start_purchase_unit, false);
            else
                this._screenicUnit1._setComboBox(_g.d.ic_inventory_detail._start_purchase_unit, detail._purchase_unit_codce);
            this._screenicUnit1._setDataStr(_g.d.ic_inventory._unit_standard_divide_value, detail._purchase_rate.ToString());

            if (detail._purchase_wh_code == "")
                this._screenIcWarehouseLocation1._clearComcoBox(_g.d.ic_inventory_detail._start_purchase_wh, false);
            else
                this._screenIcWarehouseLocation1._setComboBox(_g.d.ic_inventory_detail._start_purchase_wh, detail._purchase_wh_code);

            if (detail._purchase_lc_code == "")
                this._screenIcWarehouseLocation1._clearComcoBox(_g.d.ic_inventory_detail._start_purchase_shelf, false);
            else
                this._screenIcWarehouseLocation1._setComboBox(_g.d.ic_inventory_detail._start_purchase_shelf, detail._purchase_lc_code);

            if (detail._sale_wh_code == "")
                this._screenIcWarehouseLocation1._clearComcoBox(_g.d.ic_inventory_detail._start_sale_wh, false);
            else
                this._screenIcWarehouseLocation1._setComboBox(_g.d.ic_inventory_detail._start_sale_wh, detail._sale_wh_code);

            if (detail._sale_lc_code == "")
                this._screenIcWarehouseLocation1._clearComcoBox(_g.d.ic_inventory_detail._start_sale_shelf, false);
            else
                this._screenIcWarehouseLocation1._setComboBox(_g.d.ic_inventory_detail._start_sale_shelf, detail._sale_lc_code);

            if (detail._unit_type.Equals(1))
            {
                this._screenicUnit1._enabedControl(_g.d.ic_inventory_detail._start_purchase_unit, true);
                this._screenicUnit1._enabedControl(_g.d.ic_inventory_detail._start_sale_unit, true);
            }
            else
            {
                this._screenicUnit1._enabedControl(_g.d.ic_inventory_detail._start_purchase_unit, false);
                this._screenicUnit1._enabedControl(_g.d.ic_inventory_detail._start_sale_unit, false);
            }
            this._checkChangeIndex = true;
        }

        void _gridItemClient1__alterCellUpdate(object sender, int row, int column)
        {
            //List<int> __selectIndex = new List<int>();

            //for (int __i = 0; __i < this._gridItemClient1._rowData.Count; __i++)
            //{
            //    if (this._gridItemClient1._cellGet(__i, 0).ToString().Equals("1"))
            //    {
            //        _addSelectItem(__i);
            //    }
            //    else
            //    {
            //        _removeSelectItem(__i);
            //    }

            //}
            // check add key
            _checkImportButton();
        }

        void _checkImportButton()
        {
            bool __canAdd = false;
            for (int __i = 0; __i < this._gridItemClient1._rowData.Count; __i++)
            {
                if (this._gridItemClient1._cellGet(__i, 0).ToString().Equals("1"))
                {
                    //__canAdd = true;
                    string __dupCdoe = this._gridItemClient1._cellGet(__i, "is_dup_itemcode").ToString();
                    string __dupName = this._gridItemClient1._cellGet(__i, "is_dup_name").ToString();
                    string __dupBarcode = this._gridItemClient1._cellGet(__i, "is_dup_barcode").ToString();

                    if (__dupCdoe.Equals("1") == false && __dupName.Equals("1") == false && __dupBarcode.Equals("1") == false)
                    {
                        __canAdd = true;
                        break;
                    }

                }
            }

            for (int __i = 0; __i < this._gridItemClient1._rowData.Count; __i++)
            {
                if (this._gridItemClient1._cellGet(__i, 0).ToString().Equals("1"))
                {
                    //__canAdd = true;
                    string __dupCdoe = this._gridItemClient1._cellGet(__i, "is_dup_itemcode").ToString();
                    string __dupName = this._gridItemClient1._cellGet(__i, "is_dup_name").ToString();
                    string __dupBarcode = this._gridItemClient1._cellGet(__i, "is_dup_barcode").ToString();

                    if (__dupCdoe.Equals("1") || __dupName.Equals("1") || __dupBarcode.Equals("1"))
                    {
                        __canAdd = false;
                        break;
                    }

                }
            }

            this._importButton.Enabled = __canAdd;
            //}
        }

        void _addSelectItem(int serverRowSelect)
        {
            // check exists
            Boolean __found = false;

            string __itemCode = this._gridItemClient1._cellGet(serverRowSelect, "code").ToString();
            string __itemName = this._gridItemClient1._cellGet(serverRowSelect, "name1").ToString();
            string __itemBarcode = this._gridItemClient1._cellGet(serverRowSelect, "barcode").ToString();
            string __itemStkUnitDef = this._gridItemClient1._cellGet(serverRowSelect, "defstkunitcode").ToString();
            int __itemUnitType = 0;

            for (int __i = 0; __i < this._gridItemClient2._rowData.Count; __i++)
            {
                string __iccode = this._gridItemClient2._cellGet(__i, "code").ToString();
                string __icBarcode = this._gridItemClient2._cellGet(__i, "barcode").ToString();

                if (__itemCode.Equals(__iccode) && __itemBarcode.Equals(__icBarcode))
                {
                    __found = true;
                    break;
                }
            }

            if (__found == false)
            {
                // เพิ่มรายการลงตาราง

                int addr = this._gridItemClient2._addRow();
                this._gridItemClient2._cellUpdate(addr, 0, 0, false);
                this._gridItemClient2._cellUpdate(addr, "code", __itemCode, false);
                this._gridItemClient2._cellUpdate(addr, "name1", __itemName, false);
                this._gridItemClient2._cellUpdate(addr, "barcode", __itemBarcode, false);
                this._gridItemClient2._cellUpdate(addr, "defstkunitcode", __itemStkUnitDef, false);
                this._gridItemClient2._cellUpdate(addr, "unittype", __itemUnitType, false);

                // เพิ่มมา
                this._gridItemClient2._cellUpdate(addr, _g.DataClient.dts_bcitem._groupcode, this._gridItemClient1._cellGet(serverRowSelect, _g.DataClient.dts_bcitem._groupcode).ToString(), false);
                this._gridItemClient2._cellUpdate(addr, _g.DataClient.dts_bcitem._typecode, this._gridItemClient1._cellGet(serverRowSelect, _g.DataClient.dts_bcitem._typecode).ToString(), false);
                this._gridItemClient2._cellUpdate(addr, _g.DataClient.dts_bcitem._categorycode, this._gridItemClient1._cellGet(serverRowSelect, _g.DataClient.dts_bcitem._categorycode).ToString(), false);
                this._gridItemClient2._cellUpdate(addr, _g.DataClient.dts_bcitem._formatcode, this._gridItemClient1._cellGet(serverRowSelect, _g.DataClient.dts_bcitem._formatcode).ToString(), false);

                // รายละเอียดเพิ่มเติม
                _itemRelateDetailStruct __relate = new _itemRelateDetailStruct();
                __relate._itemCode = __itemCode;
                __relate._itemBarcode = __itemBarcode;
                __relate._unit_Standard = __itemStkUnitDef;
                //if (_shelfDataTable != null && _shelfDataTable.Rows.Count > 0)
                //{
                //    __relate._purchase_wh_code = _shelfDataTable.Rows[0]["whcode"].ToString() + "~" + _shelfDataTable.Rows[0]["whname"].ToString();
                //    __relate._purchase_lc_code = _shelfDataTable.Rows[0]["code"].ToString() + "~" + _shelfDataTable.Rows[0]["name"].ToString();
                //    __relate._sale_wh_code = _shelfDataTable.Rows[0]["whcode"].ToString() + "~" + _shelfDataTable.Rows[0]["whname"].ToString();
                //    __relate._sale_lc_code = _shelfDataTable.Rows[0]["code"].ToString() + "~" + _shelfDataTable.Rows[0]["name"].ToString();

                //}
                this._gridItemClient2._cellUpdate(addr, "item_detail", __relate, false);

                List<string> __query = new List<string>();
                __query.Add("select unitcode, rate, isstkunit from dts_allunitcode where itemcode = '" + __itemCode + "'");
                __query.Add("select unitcode, rate1, rate2 from dts_bcstkpacking where itemcode = '" + __itemCode + "'");

                _clientFrameWork __clientFrameWork = new _clientFrameWork();
                ArrayList __result = __clientFrameWork._getDataList(__query);
                if (__result.Count > 0)
                {
                    DataTable __allunitCodeTable = ((DataSet)__result[0]).Tables[0];
                    DataTable __stkPackingTable = ((DataSet)__result[1]).Tables[0];

                    _itemUnitCodeList __unitList = new _itemUnitCodeList();
                    for (int __i = 0; __i < __allunitCodeTable.Rows.Count; __i++)
                    {
                        _itemUnitCode __unitUse = new _itemUnitCode();
                        __unitUse._unitCode = __allunitCodeTable.Rows[__i]["unitcode"].ToString();
                        __unitUse._isStkUnit = __allunitCodeTable.Rows[__i]["isstkunit"].ToString().Equals("1") ? 1 : 0;
                        __unitUse._rate = MyLib._myGlobal._decimalPhase(__allunitCodeTable.Rows[__i]["rate"].ToString());

                        __unitList._details.Add(__unitUse);
                    }
                    this._gridItemClient2._cellUpdate(addr, "item_unit", __unitList, false);

                    _stkPackingList __stkPackingList = new _stkPackingList();
                    for (int __i = 0; __i < __stkPackingTable.Rows.Count; __i++)
                    {
                        _stkPacking __stkPacking = new _stkPacking();
                        __stkPacking._unitCode = __stkPackingTable.Rows[__i]["unitcode"].ToString();
                        __stkPacking._rate1 = MyLib._myGlobal._decimalPhase(__stkPackingTable.Rows[__i]["rate1"].ToString());
                        __stkPacking._rate2 = MyLib._myGlobal._decimalPhase(__stkPackingTable.Rows[__i]["rate2"].ToString());

                        __stkPackingList._details.Add(__stkPacking);
                    }
                    this._gridItemClient2._cellUpdate(addr, "item_stkpacking", __stkPackingList, false);

                }
                /*
                _clientFrameWork __clientFrameWork = new _clientFrameWork();
                ArrayList __result = __clientFrameWork._getDataList(__query);
                if (__result.Count > 0)
                {
                    DataTable __allunitCodeTable = ((DataSet)__result[0]).Tables[0];
                    DataTable __stkPackingTable = ((DataSet)__result[1]).Tables[0];

                    //((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_purchase_unit)).Items.Clear();
                    //((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_sale_unit)).Items.Clear();
                    for (int __i = 0; __i < __allunitCodeTable.Rows.Count; __i++)
                    {
                        //((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_purchase_unit)).Items.Add(__allunitCodeTable.Rows[__i]["unitcode"].ToString());
                        //((MyLib._myComboBox)this._screenicUnit1._getControl(_g.d.ic_inventory_detail._start_sale_unit)).Items.Add(__allunitCodeTable.Rows[__i]["unitcode"].ToString());

                    }
                }
                 * */


                //_loadAllUnitUse(__itemCode);
            }

        }

        void _removeSelectItem(int serverRowSelect)
        {
            // get line 
            int __line = -1;

            string __itemCode = this._gridItemClient1._cellGet(serverRowSelect, "code").ToString();
            string __itemBarcode = this._gridItemClient1._cellGet(serverRowSelect, "barcode").ToString();

            for (int __i = 0; __i < this._gridItemClient2._rowData.Count; __i++)
            {
                string __iccode = this._gridItemClient2._cellGet(__i, "code").ToString();
                string __icBarcode = this._gridItemClient2._cellGet(__i, "barcode").ToString();

                if (__itemCode.Equals(__iccode) && __itemBarcode.Equals(__icBarcode))
                {
                    __line = __i;
                    break;
                }
            }


            // remove at line 
            if (__line != -1)
            {
                this._gridItemClient2._rowData.RemoveAt(__line);
                this._gridItemClient2.Refresh();
            }
        }

        void _dts_item_download_Load(object sender, EventArgs e)
        {
            _loadData();
        }

        void _loadData()
        {
            // load ข้อมูลสินค้า
            _clientFrameWork __myFramework = new _clientFrameWork();

            //string __query = "select * from dts_bcitem where code not in (select code from bcitem) ";

            string __dupKeyQuery = ",case when ((select COUNT(code) from bcitem where bcitem.Code = dts_bcitem.code) > 0) then 1 else 0  end  as is_dup_itemcode, case when ((select COUNT(code) from bcitem where bcitem.Name1 = dts_bcitem.Name1) > 0) then 1 else 0  end as is_dup_name, case when ((select COUNT(barcode) from BCBarCodeMaster where BCBarCodeMaster.Barcode = dts_bcitem.barcode) > 0) then 1 else 0  end as is_dup_barcode ";

            string __query = "select * " + __dupKeyQuery + " from dts_bcitem  where code not in (select code from bcitem) ";
            if (this._showAllItemCheckBox.Checked)
            {
                __query = " select * " + __dupKeyQuery + " from dts_bcitem  ";
            }

            DataSet __result = __myFramework._query(__query);

            if (__result.Tables.Count > 0)
            {
                this._gridItemClient1._loadFromDataTable(__result.Tables[0]);
            }

            _gridItemClient2._clear();

        }

        private void _refreshButton_Click(object sender, EventArgs e)
        {
            _loadData();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        void _saveData()
        {
            string __message = "คุณต้องการบันทึกข้อมูลหรือไม่";
            if (this._gridItemClient2._rowData.Count > 1)
            {
                __message = "ท่านตกลงให้โปรแกรมบันทึกข้อมูลสินค้าทั้งหมดจริงหรือไม่? \n- เป็นสินค้าหน่วยนับเดียวทั้งหมด \n- มีรหัสคลัง/ที่เก็บเริ่มต้นเดียวกันทั้งหมด";
            }

            if (MessageBox.Show(__message, DTSClientDownload._global._champMessageCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            _clientFrameWork __myFrameWork = new _clientFrameWork();
            List<string> __query;
            string __downloadGuid = Guid.NewGuid().ToString();
            int __downloadFlag = 0;

            // check 
            StringBuilder __itemDuplicate = new StringBuilder();
            for (int __i = 0; __i < this._gridItemClient2._rowData.Count; __i++)
            {
                if (this._gridItemClient2._cellGet(__i, 0).ToString().Equals("1"))
                {
                    DataSet __itemResult = __myFrameWork._query("select code, name1 from bcitem where code = \'" + this._gridItemClient2._cellGet(__i, "code").ToString() + "\'");
                    if (__itemResult.Tables.Count > 0 && __itemResult.Tables[0].Rows.Count > 0)
                    {
                        __itemDuplicate.AppendLine(__itemResult.Tables[0].Rows[0]["code"].ToString() + ", " + __itemResult.Tables[0].Rows[0]["name1"].ToString());
                    }
                }
            }


            if (__itemDuplicate.Length > 0)
            {
                MessageBox.Show("รหัสสินค้ามีอยู่แล้วในระบบ \n" + __itemDuplicate.ToString(), "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // after check

            string __purchase_wh_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_purchase_wh, false); // "";
            string __purchase_lc_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_purchase_shelf, false); // "";
            string __sale_wh_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_sale_wh, false); // "";
            string __sale_lc_code = this._screenIcWarehouseLocation1._getDataComboStr(_g.d.ic_inventory_detail._start_sale_shelf, false);// "";

            __purchase_wh_code = (__purchase_wh_code != "") ? __purchase_wh_code : _getTempWH();
            __purchase_lc_code = (__purchase_lc_code != "") ? __purchase_lc_code : _getTempShelf(__purchase_wh_code);
            __sale_wh_code = (__sale_wh_code != "") ? __sale_wh_code : _getTempWH();
            __sale_lc_code = (__sale_lc_code != "") ? __sale_lc_code : _getTempShelf(__sale_wh_code);


            string __extraDTSField = ",creatorcode,createdatetime";
            string __extraDTSValue = ",'dts',getdate()";


            // insert log start download
            __myFrameWork._excute("insert into dts_download(guid_download, agentcode, download_flag,download_date, download_start) values(\'" + __downloadGuid + "\',\'" + MyLib._myGlobal._userCode + "\'," + __downloadFlag + ", \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");

            // pack query insert
            for (int __i = 0; __i < this._gridItemClient2._rowData.Count; __i++)
            {
                if (this._gridItemClient2._cellGet(__i, 0).ToString().Equals("1"))
                {
                    __query = new List<string>();
                    string __itemCode = this._gridItemClient2._cellGet(__i, "code").ToString();

                    __myFrameWork._excute("insert into dts_download_detail(guid_download, agentcode, download_flag,download_date, download_start,ref_no) values(\'" + __downloadGuid + "\',\'" + MyLib._myGlobal._userCode + "\'," + __downloadFlag + ", \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', \'" + __itemCode + "\')");


                    // จัดการหน่วยนับก่อน                    
                    _itemUnitCodeList __unitCodeList = (_itemUnitCodeList)this._gridItemClient2._cellGet(__i, "item_unit");
                    _stkPackingList __stkPackingList = (_stkPackingList)this._gridItemClient2._cellGet(__i, "item_stkpacking");
                    _itemRelateDetailStruct __itemDetail = (_itemRelateDetailStruct)this._gridItemClient2._cellGet(__i, "item_detail");

                    for (int __indexUnit = 0; __indexUnit < __unitCodeList._details.Count; __indexUnit++)
                    {
                        if (_isExitUnitCode(__unitCodeList._details[__indexUnit]._unitCode) == false)
                        {
                            __query.Add("insert into BCItemUnit (Code, Name) values (\'" + __unitCodeList._details[__indexUnit]._unitCode + "\', \'" + __unitCodeList._details[__indexUnit]._unitCode + "\')");
                        }
                    }

                    // คลังและที่เก็บ
                    for (int __index = 0; __index < this._shelfDataTable.Rows.Count; __index++)
                    {
                        __query.Add("insert into BCItemWarehouse(itemcode,whcode,shelfcode) values(\'" + __itemCode + "\', \'" + this._shelfDataTable.Rows[__index]["whcode"].ToString() + "\', \'" + this._shelfDataTable.Rows[__index]["code"].ToString() + "\') ");
                    }

                    // bcitem
                    string __bcItemInsertField = MyLib._myGlobal._fieldAndComma(
                        "Code",
                        "name1",
                        "defstkunitcode",
                        "unittype",
                        "defsaleunitcode",
                        "defbuyunitcode",
                        "defsalewhcode",
                        "defsaleshelf",
                        "defbuywhcode",
                        "defbuyshelf",
                        _g.DataClient.dts_bcitem._groupcode,
                        _g.DataClient.dts_bcitem._typecode,
                        _g.DataClient.dts_bcitem._categorycode,
                        _g.DataClient.dts_bcitem._formatcode);

                    //GroupCode,
                    //TypeCode,
                    //CategoryCode,
                    //FormatCode,

                    string __unitType = __itemDetail._unit_type.Equals(0) ? "0" : "1"; // MyLib._myGlobal._decimalPhase(this._gridItemClient2._cellGet(__i, "unittype").ToString()).Equals(0M) ? "0" : "1";
                    string __defaultUnitCode = this._gridItemClient2._cellGet(__i, "defstkunitcode").ToString();
                    string __bcItemInsertValue = MyLib._myGlobal._fieldAndComma(
                        "\'" + __itemCode + "\'",
                        "\'" + this._gridItemClient2._cellGet(__i, "name1").ToString() + "\'",
                        "\'" + __defaultUnitCode + "\'",
                        __unitType,
                        (__unitType == "0") ? "\'" + __defaultUnitCode + "\'" : "\'" + __itemDetail._sale_unit_code + "\'",
                        (__unitType == "0") ? "\'" + __defaultUnitCode + "\'" : "\'" + __itemDetail._purchase_unit_codce + "\'",
                        "\'" + _global._getValue(__purchase_wh_code) + "\'", //"\'" + ((__itemDetail._sale_wh_code.Length > 0) ? _global._getValue(__itemDetail._sale_wh_code) : _shelfDataTable.Rows[0]["whcode"].ToString()) + "\'",
                        "\'" + _global._getValue(__purchase_lc_code) + "\'", //"\'" + ((__itemDetail._sale_lc_code.Length > 0) ? _global._getValue(__itemDetail._sale_lc_code) : _shelfDataTable.Rows[0]["code"].ToString()) + "\'",
                        "\'" + _global._getValue(__sale_wh_code) + "\'", //"\'" + ((__itemDetail._purchase_wh_code.Length > 0) ? _global._getValue(__itemDetail._purchase_wh_code) : _shelfDataTable.Rows[0]["whcode"].ToString()) + "\'",
                        "\'" + _global._getValue(__sale_lc_code) + "\'",  //"\'" + ((__itemDetail._purchase_lc_code.Length > 0) ? _global._getValue(__itemDetail._purchase_lc_code) : _shelfDataTable.Rows[0]["code"].ToString()) + "\'",
                        "\'" + this._gridItemClient2._cellGet(__i, _g.DataClient.dts_bcitem._groupcode).ToString() + "\'",
                        "\'" + this._gridItemClient2._cellGet(__i, _g.DataClient.dts_bcitem._typecode).ToString() + "\'",
                        "\'" + this._gridItemClient2._cellGet(__i, _g.DataClient.dts_bcitem._categorycode).ToString() + "\'",
                        "\'" + this._gridItemClient2._cellGet(__i, _g.DataClient.dts_bcitem._formatcode).ToString() + "\'");

                    string __bcItemInsert = "INSERT INTO BCITEM (" + __bcItemInsertField + __extraDTSField + ") VALUES(" + __bcItemInsertValue + __extraDTSValue + ")";
                    __query.Add(__bcItemInsert);

                    // barcode master 
                    string __bcbarcodemasterInsert = "INSERT INTO bcbarcodemaster(itemcode,barcode,barcodename,unitcode,activestatus,remark) values(\'" + __itemCode + "\',\'" + this._gridItemClient2._cellGet(__i, "barcode").ToString() + "\',\'" + this._gridItemClient2._cellGet(__i, "name1").ToString() + "\',\'" + __defaultUnitCode + "\',1, NULL)";
                    __query.Add(__bcbarcodemasterInsert);

                    // stkPacking
                    if (__unitType.Equals("1"))
                    {
                        for (int __indexPacking = 0; __indexPacking < __stkPackingList._details.Count; __indexPacking++)
                        {
                            __query.Add("INSERT INTO BCStkPacking (ItemCode,UnitCode,Rate1,Rate2,RATE,Running,Runner) VALUES (\'" + __itemCode + "\','" + __stkPackingList._details[__indexPacking]._unitCode + "'," + __stkPackingList._details[__indexPacking]._rate1 + "," + __stkPackingList._details[__indexPacking]._rate2 + "," + __stkPackingList._details[__indexPacking]._rate1 / __stkPackingList._details[__indexPacking]._rate2 + "," + (__indexPacking + 1) + "," + (__indexPacking + 1) + ") ");
                        }
                    }

                    // save เข้า champ
                    string __result = __myFrameWork._queryList(__query);
                    if (__result.Length == 0)
                    {
                        __myFrameWork._excute("update dts_download_detail set download_success = \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', download_status = 1 where guid_download = \'" + __downloadGuid + "\' and agentcode=\'" + MyLib._myGlobal._userCode + "\' and download_flag = " + __downloadFlag + " and ref_no = \'" + __itemCode + "\' ");
                    }
                    else
                    {
                        __myFrameWork._excute("update dts_download_detail set download_success = \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', download_status = 2 where guid_download = \'" + __downloadGuid + "\' and agentcode=\'" + MyLib._myGlobal._userCode + "\' and download_flag = " + __downloadFlag + " and ref_no = \'" + __itemCode + "\' ");

                        MessageBox.Show(__result, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            __myFrameWork._excute("update dts_download set download_success = \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', download_status = 1 where guid_download = \'" + __downloadGuid + "\' and agentcode=\'" + MyLib._myGlobal._userCode + "\' and download_flag = " + __downloadFlag + " ");

            MessageBox.Show("ดาวน์โหลดข้อมูลสมบูรณ์ !!", DTSClientDownload._global._champMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            // refresh data ใหม่

            // ตัดรายการที่ได้เลือกออกไป
            // แล้ว refresh data ใหม่
            this._gridItemClient2._clear();
            this._loadData();
            _checkEnableSaveButton();
        }

        private Boolean _isExitUnitCode(string unitCode)
        {
            Boolean __result = false;

            string __query = "select code,name from BCItemUnit where upper(code) =\'" + unitCode.ToUpper() + "\'";

            _clientFrameWork __myFrameWork = new _clientFrameWork();
            DataSet __dataResult = __myFrameWork._query(__query);

            if (__dataResult.Tables.Count > 0 && __dataResult.Tables[0].Rows.Count > 0)
            {
                if (__dataResult.Tables[0].Rows[0]["code"].ToString().ToUpper().Equals(unitCode.ToUpper()))
                {
                    __result = true;
                }
            }

            return __result;
        }
    }

}
