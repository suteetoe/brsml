using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SMLERPIC
{
    public partial class _icAddBarcodeFastControl : UserControl
    {
        private string _wareHouseCode = "";
        private string _locationCode = "";
        private int _unitType = 0;
        private SMLERPControl._selectWarehouseAndLocationForm _selectWarehouseAndLocation = new SMLERPControl._selectWarehouseAndLocationForm(false);
        private MyLib._searchDataFull _searchItem = null;
        Boolean _foundWarehouseAndLocation = false;

        public _icAddBarcodeFastControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._icUpdateControl._closeButton.Visible = false;
            this._icUpdateControl._saveKey = false;
            //this._icUpdateControl._toolbar.Visible = false;

            this._mainTab.TableName = _g.d.ic_resource._table;
            this._mainTab._getResource();

            this._unitGrid._isEdit = false;
            this._gridWarehouseLocation._isEdit = false;
            this._barcodeTextBox.KeyPress += new KeyPressEventHandler(_barcodeTextBox_KeyPress);
            this.Load += (s1, e1) =>
            {
                this._barcodeTextBox.Select();
            };
            this._barCodeGrid.GetUnitCode += new SMLInventoryControl.GetUnitCodeEventHandler(_barCodeGrid_GetUnitCode);
            this._barCodeGrid.GetUnitType += new SMLInventoryControl.GetUnitTypeEventHandler(_barCodeGrid_GetUnitType);
            this._barCodeGrid.GetItemCode += new SMLInventoryControl.GetItemCodeEventHandler(_barCodeGrid_GetItemCode);
            this._barCodeGrid.GetItemDesc += new SMLInventoryControl.GetItemDescEventHandler(_barCodeGrid_GetItemDesc);
            this._barCodeGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_barCodeGrid__queryForInsertCheck);
            //
            this._switchWarehouseShelfButton.Click += (s1, e1) =>
            {
                this._selectWarehouseAndLocation.ShowDialog();
            };
            this._selectWarehouseAndLocation._closeButton.Click += (s1, e1) =>
            {
                this._selectWarehouseAndLocation.Close();
                this._wareHouseCode = "";
                this._locationCode = "";
                //
                for (int __row = 0; __row < this._selectWarehouseAndLocation._whGrid._rowData.Count; __row++)
                {
                    if (this._selectWarehouseAndLocation._whGrid._cellGet(__row, 0).ToString().Equals("1"))
                    {
                        this._wareHouseCode = this._selectWarehouseAndLocation._whGrid._cellGet(__row, _g.d.ic_warehouse._code).ToString();
                        break;
                    }
                }
                //
                for (int __row = 0; __row < this._selectWarehouseAndLocation._locationGrid._rowData.Count; __row++)
                {
                    if (this._selectWarehouseAndLocation._locationGrid._cellGet(__row, this._selectWarehouseAndLocation._fieldCheck).ToString().Equals("1"))
                    {
                        this._locationCode = this._selectWarehouseAndLocation._locationGrid._cellGet(__row, _g.d.ic_shelf._code).ToString();
                        break;
                    }
                }
                this._warehouseLocationLabel.Text = "";
                if (this._wareHouseCode.Length > 0 && this._locationCode.Length > 0)
                {
                    this._warehouseLocationLabel.Text = this._wareHouseCode + "/" + this._locationCode;
                }
            };
            //
            // this._selectWarehouseAndLocation.ShowDialog(); // toe เอาออกให้เลือก คลังเอา

            this.Disposed += new EventHandler(_icAddBarcodeFastControl_Disposed);

            this._icUpdateControl._saveSuccess += _icUpdateControl__saveSuccess;
        }

        void _icUpdateControl__saveSuccess(string itemCode, string barcode)
        {
            this._loadData(itemCode);
        }

        void _icAddBarcodeFastControl_Disposed(object sender, EventArgs e)
        {
            _g._utils __utils = new _g._utils();
            __utils._updateInventoryMaster("");
            Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
            __thread.Start();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._barcodeTextBox.Text = this._searchItem._dataList._gridData._cellGet(e._row, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
            this._searchItem.Close();
            this._search();
        }

        void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._barcodeTextBox.Text = this._searchItem._dataList._gridData._cellGet(row, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
            this._searchItem.Close();
            this._search();
        }

        bool _barCodeGrid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            string __barcode = this._barCodeGrid._cellGet(row, _g.d.ic_inventory_barcode._barcode).ToString().Trim();
            return (__barcode.Length == 0) ? false : true;
        }

        string _barCodeGrid_GetUnitCode(object sender)
        {
            return this._itemUnitLabel.Text;
        }

        string _barCodeGrid_GetItemDesc(object sender)
        {
            return this._itemNameLabel.Text;
        }

        string _barCodeGrid_GetItemCode(object sender)
        {
            return this._itemCodeLabel.Text;
        }

        int _barCodeGrid_GetUnitType(object sender)
        {
            return this._unitType;
        }

        void _search()
        {
            string __barcode = this._barcodeTextBox.Text;
            this._barcodeLabel.Text = __barcode;
            this._barcodeTextBox.Text = "";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + __barcode + "\'"));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_barcode._ic_code + " from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._barcode) + "=\'" + __barcode + "\'"));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataTable __itemTable = ((DataSet)__getData[0]).Tables[0];
            DataTable __barcodeTable = ((DataSet)__getData[1]).Tables[0];
            if (__itemTable.Rows.Count > 0)
            {
                this._loadData(__barcode);
            }
            else
            {
                if (__barcodeTable.Rows.Count > 0)
                {
                    string __itemCode = __barcodeTable.Rows[0][0].ToString();
                    this._loadData(__itemCode);
                }
                else
                {
                    bool __found = false;

                    if (MyLib._myGlobal._OEMVersion.Equals("ais") || ( _g.g._companyProfile._sync_wbservice_url.Trim().Length > 0 && _g.g._companyProfile._sync_product))
                    {

                        SMLProcess._syncClass __sync = new SMLProcess._syncClass();
                        __found = __sync._findProduct(__barcode);

                        if (__found == true)
                        {
                            this._loadData(__sync._foundItemCode);
                        }
                    }
                    else
                    {
                        __found = false;
                    }

                    if (__found == false)
                    {
                        this._mainTab.SelectedIndex = 1;
                        this.panel3.BackColor = Color.Red;
                        this._clear();
                        this._icUpdateControl._icMainScreen._setDataStr(_g.d.ic_inventory._code, __barcode);

                        this._icUpdateControl._icBarcodeGrid._addRow();
                        this._icUpdateControl._icBarcodeGrid._cellUpdate(0, _g.d.ic_inventory_barcode._barcode, __barcode, true);
                    }
                }
            }
        }

        void _clear()
        {
            this._itemCodeLabel.Text = "";
            this._itemNameLabel.Text = "";
            this._itemUnitLabel.Text = "";
            this._shelfLabel.Text = "";
            this._barCodeGrid._clear();
            this._gridWarehouseLocation._clear();
            this._unitGrid._clear();
            this._foundWarehouseAndLocation = false;
            this._icUpdateControl._clear();
        }

        void _barcodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this._search();
            }
        }

        void _loadData(string itemCode)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + itemCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + "=\'" + itemCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + ") as " + _g.d.ic_unit_use._name_1 + " from " + _g.d.ic_unit_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit_use._ic_code) + "=\'" + itemCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._wh_code + ") as " + _g.d.ic_wh_shelf._wh_name + ", (select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._shelf_code + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._wh_code + " ) as " + _g.d.ic_wh_shelf._shelf_name + " from " + _g.d.ic_wh_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_wh_shelf._ic_code) + "=\'" + itemCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_wh_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_wh_shelf._ic_code) + "=\'" + itemCode + "\' and " + _g.d.ic_wh_shelf._wh_code + "=\'" + this._wareHouseCode + "\' and " + _g.d.ic_wh_shelf._shelf_code + "=\'" + this._locationCode + "\'"));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                DataTable __itemTable = ((DataSet)__getData[0]).Tables[0];
                this._barCodeGrid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this._unitGrid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                this._gridWarehouseLocation._loadFromDataTable(((DataSet)__getData[3]).Tables[0]);
                DataTable __warehouseLocationTable = ((DataSet)__getData[4]).Tables[0];
                if (this._autoFocusCheckBox.Checked)
                {
                    this._barCodeGrid.Select();
                    this._barCodeGrid._gotoCell(this._barCodeGrid._rowData.Count, 0);
                }
                else
                {
                    this._barcodeTextBox.SelectAll();
                }
                //
                if (__itemTable.Rows.Count > 0)
                {
                    this._itemCodeLabel.Text = __itemTable.Rows[0][_g.d.ic_inventory._code].ToString();
                    this._itemNameLabel.Text = __itemTable.Rows[0][_g.d.ic_inventory._name_1].ToString();
                    this._itemUnitLabel.Text = __itemTable.Rows[0][_g.d.ic_inventory._unit_standard].ToString();
                    this.panel3.BackColor = Color.Black;
                }
                else
                {
                    this.panel3.BackColor = Color.Red;
                }
                if (__warehouseLocationTable.Rows.Count > 0)
                {
                    this._shelfLabel.Text = __warehouseLocationTable.Rows[0][_g.d.ic_wh_shelf._shelf_list].ToString().Trim().ToUpper();
                    this._foundWarehouseAndLocation = true;
                }
                else
                {
                    this._shelfLabel.Text = "";
                }
            }
            catch
            {
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    if (this._searchItem == null)
                    {
                        this._searchItem = new MyLib._searchDataFull();
                        this._searchItem.Text = "Product";
                        this._searchItem._name = _g.g._search_screen_ic_inventory_barcode;
                        this._searchItem._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                        this._searchItem._dataList._loadViewFormat(_g.g._search_screen_ic_inventory_barcode, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                        this._searchItem.WindowState = FormWindowState.Maximized;
                    }
                    this._searchItem.ShowDialog();
                    return true;
                case Keys.F12:
                    this._saveData();
                    this._barcodeTextBox.Select();
                    return true;
                case Keys.F10:
                    this._icUpdateControl._saveData();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _saveData()
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                if (this._itemCodeLabel.Text.Length > 0)
                {
                    Boolean __check = true;
                    if (this._shelfTextBox.Text.Trim().Length > 0)
                    {
                        if (this._wareHouseCode.Length == 0 || this._locationCode.Length == 0)
                        {
                            __check = false;
                        }
                    }
                    if (__check)
                    {
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        string __itemCode = this._itemCodeLabel.Text;
                        string __fieldList = _g.d.ic_inventory_detail._ic_code + " , ";
                        string __dataList = "\'" + __itemCode + "\' , ";
                        this._barCodeGrid._updateRowIsChangeAll(true);
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + "=\'" + __itemCode + "\'"));
                        __myQuery.Append(this._barCodeGrid._createQueryForInsert(_g.d.ic_inventory_barcode._table, __fieldList, __dataList));
                        if (this._shelfTextBox.Text.Trim().Length > 0 && this._wareHouseCode.Length > 0)
                        {
                            // เพิ่ม shelf auto
                            this._shelfTextBox.Text = this._shelfTextBox.Text.Trim().ToUpper();
                            string[] __split = this._shelfLabel.Text.Trim().ToUpper().Split(',');
                            Boolean __foundShelf = false;
                            StringBuilder __shelfList = new StringBuilder(this._shelfLabel.Text);
                            for (int __loop = 0; __loop < __split.Length; __loop++)
                            {
                                if (this._shelfTextBox.Text.Equals(__split[__loop].ToString()))
                                {
                                    __foundShelf = true;
                                    break;
                                }
                            }
                            if (__foundShelf == false)
                            {
                                if (__shelfList.Length > 0)
                                {
                                    __shelfList.Append(",");
                                }
                                __shelfList.Append(this._shelfTextBox.Text);
                            }
                            if (this._foundWarehouseAndLocation)
                            {
                                // กรณีมี Warehouse เดิม update
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_wh_shelf._table + " set " + _g.d.ic_wh_shelf._shelf_list + "=\'" + __shelfList.ToString() + "\' where " + MyLib._myGlobal._addUpper(_g.d.ic_wh_shelf._ic_code) + "=\'" + this._itemCodeLabel.Text + "\' and " + _g.d.ic_wh_shelf._wh_code + "=\'" + this._wareHouseCode + "\' and " + _g.d.ic_wh_shelf._shelf_code + "=\'" + this._locationCode + "\'"));
                            }
                            else
                            {
                                // ถุ้าไม่มีก็ Insert
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_wh_shelf._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.ic_wh_shelf._ic_code, _g.d.ic_wh_shelf._wh_code, _g.d.ic_wh_shelf._shelf_code, _g.d.ic_wh_shelf._remark, _g.d.ic_wh_shelf._shelf_list, _g.d.ic_wh_shelf._status, _g.d.ic_wh_shelf._min_point, _g.d.ic_wh_shelf._max_point) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + this._itemCodeLabel.Text + "\'", "\'" + this._wareHouseCode + "\'", "\'" + this._locationCode + "\'", "\'\'", "\'" + __shelfList.ToString() + "\'", "1", "0", "0" + ")")));
                                // เพิ่ม
                            }
                        }
                        __myQuery.Append("</node>");
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length == 0)
                        {
                            this._clear();
                            this.panel3.BackColor = Color.Green;
                            // Update ชื่อหน่วยนับ
                            _g._utils __utils = new _g._utils();
                            __utils._updateInventoryMaster(this._itemCodeLabel.Text);
                            Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
                            __thread.Start();
                        }
                        else
                        {
                            MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ข้อมูลไม่สมบูรณ์");
                    }
                }
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._saveData();
            this._barcodeTextBox.Select();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
