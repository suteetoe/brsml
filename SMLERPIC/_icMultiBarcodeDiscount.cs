using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icMultiBarcodeDiscount : UserControl
    {
        private _icMultiBarcodeDiscountControl _screenControl = new _icMultiBarcodeDiscountControl();
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _resetButton;

        private _icBarcodeDiscountGrid __discount1;
        private _icBarcodeDiscountGrid __discount2;
        private _icBarcodeDiscountGrid __discount3;
        private _icBarcodeDiscountGrid __discount4;
        private _icBarcodeDiscountGrid __discount5;
        private string _oldBarcode = "";


        public _icMultiBarcodeDiscount()
        {
            InitializeComponent();
            //
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            //
            this._saveButton = new MyLib.ToolStripMyButton();
            //
            this._resetButton = new MyLib.ToolStripMyButton();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            // 
            // _saveButton
            // 
            this._saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(23, 22);
            this._saveButton.Text = "บันทึก";
            //
            // _resetButton
            //
            this._resetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            this._resetButton.Image = global::SMLERPIC.Properties.Resources.lightbulb_on;
            this._resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._resetButton.Name = "_saveButton";
            this._resetButton.Padding = new System.Windows.Forms.Padding(1);
            this._resetButton.ResourceName = "เริ่มใหม่ทั้งหมด";
            this._resetButton.Size = new System.Drawing.Size(23, 22);
            this._resetButton.Text = "เริ่มใหม่ทั้งหมด";
            //
            this._myToolBar.Items.Add(this._saveButton);
            this._myToolBar.Items.Add(this._resetButton);
            //
            this._myManageMain._form2.Controls.Add(this._screenControl);
            this._screenControl.Dock = DockStyle.Fill;
            this._myManageMain._form2.Controls.Add(this._myToolBar);
            this._myToolBar.Dock = DockStyle.Top;
            //
            this._myManageMain._displayMode = 0;
            this._myManageMain._dataList._lockRecord = true;
            this._myManageMain._selectDisplayMode(this._myManageMain._displayMode);
            this._myManageMain._dataList._loadViewFormat(_g.g._search_screen_ic_barcode, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageMain._dataList._referFieldAdd(_g.d.ic_inventory_barcode_price._barcode, 1);
            //this._myManageMain._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageMain__loadDataToScreen);
            //this._myManageMain._manageButton = this._myToolBar;
            //this._myManageDetail._manageBackgroundPanel = this._myPanel1;
            this._myManageMain._newDataClick += new MyLib.NewDataEvent(_myManageMain__newDataClick);
            //this._myManageMain._discardData += new MyLib.DiscardDataEvent(_myManageMain__discardData);
            this._myManageMain._clearData += new MyLib.ClearDataEvent(_myManageMain__clearData);
            //this._myManageMain._closeScreen += new MyLib.CloseScreenEvent(_myManageMain__closeScreen);
            //this._myManageMain._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageMain._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._myManageMain._dataList._loadViewData(0);
            this._myManageMain._calcArea();
            this._myManageMain._dataListOpen = true;
            this._myManageMain._autoSize = true;
            this._myManageMain._autoSizeHeight = 450;
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._resetButton.Click += new EventHandler(_resetButton_Click);
            this._myManageMain.Invalidate();
            //
            //
            this.__discount1 = new _icBarcodeDiscountGrid(_barcodeDiscountGridType.ส่วนลดพิเศษทั่วไป);
            this.__discount2 = new _icBarcodeDiscountGrid(_barcodeDiscountGridType.ส่วนลดพิเศษสมาชิก);
            this.__discount3 = new _icBarcodeDiscountGrid(_barcodeDiscountGridType.ส่วนลดโปรโมชั่น);
            this.__discount4 = new _icBarcodeDiscountGrid(_barcodeDiscountGridType.ส่วนลดสมาชิก);
            this.__discount5 = new _icBarcodeDiscountGrid(_barcodeDiscountGridType.ส่วนลดพนักงาน);
            this._screenControl._tableLayoutPanel.Controls.Add(this.__discount1, 0, 0);
            this._screenControl._tableLayoutPanel.Controls.Add(this.__discount2, 1, 0);
            this._screenControl._tableLayoutPanel.Controls.Add(this.__discount3, 0, 1);
            this._screenControl._tableLayoutPanel.Controls.Add(this.__discount4, 1, 1);
            this._screenControl._tableLayoutPanel.Controls.Add(this.__discount5, 0, 2);
            this.__discount1.Dock = DockStyle.Fill;
            this.__discount2.Dock = DockStyle.Fill;
            this.__discount3.Dock = DockStyle.Fill;
            this.__discount4.Dock = DockStyle.Fill;
            this.__discount5.Dock = DockStyle.Fill;

        }

        void _resetButton_Click(object sender, EventArgs e)
        {
            this._clear();
        }

        void _myManageMain__clearData()
        {
            this._clear();
        }

        void _clear()
        {
            this._oldBarcode = "";
            this._screenControl._selectedGid._clear();
            this.__discount1._grid._clear();
            this.__discount2._grid._clear();
            this.__discount3._grid._clear();
            this.__discount4._grid._clear();
            this.__discount5._grid._clear();
        }

        void _myManageMain__newDataClick()
        {

        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __barCode = this._myManageMain._dataList._gridData._cellGet(e._row, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
            Boolean __found = false;
            for (int __row = 0; __row < this._screenControl._selectedGid._rowData.Count; __row++)
            {
                if (__barCode.Equals(this._screenControl._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._barcode).ToString()))
                {
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __data = __myFrameWork._queryShort("select *, (select " + _g.d.ic_unit._table + "." + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + " = " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " ) as " + _g.d.ic_inventory_barcode._unit_name + "  from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "=\'" + __barCode + "\'").Tables[0];
                if (__data.Rows.Count > 0)
                {
                    int __addr = this._screenControl._selectedGid._addRow();
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._barcode, __data.Rows[0][_g.d.ic_inventory_barcode._barcode].ToString(), false);
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._ic_code, __data.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString(), false);
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._description, __data.Rows[0][_g.d.ic_inventory_barcode._description].ToString(), false);
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._unit_code, __data.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString(), false);
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price].ToString()), false);
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_2, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_2].ToString()), false);
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_3, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_3].ToString()), false);
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_4, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_4].ToString()), false);
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._unit_name, __data.Rows[0][_g.d.ic_inventory_barcode._unit_name].ToString(), false);
                    this._screenControl._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._stamp_count, 0M, false);
                }
            }
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            // save multi discount barcode
            //if (this._oldBarcode.Length > 0)
            //{

            this.__discount1._grid._updateRowIsChangeAll(true);
            this.__discount2._grid._updateRowIsChangeAll(true);
            this.__discount3._grid._updateRowIsChangeAll(true);
            this.__discount4._grid._updateRowIsChangeAll(true);
            this.__discount5._grid._updateRowIsChangeAll(true);

            string __fieldList = _g.d.ic_inventory_barcode_price._barcode + "," + _g.d.ic_inventory_barcode_price._discount_type + ",";
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            //__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_barcode_price._table + " where " + _g.d.ic_inventory_barcode_price._barcode + "=\'" + this._oldBarcode + "\'"));

            // for itemgrid โลด
            for (int __i = 0; __i < this._screenControl._selectedGid._rowData.Count; __i++)
            {
                string __dataList = "\'" + this._screenControl._selectedGid._cellGet(__i, _g.d.ic_inventory_barcode._barcode).ToString() + "\',";

                __myQuery.Append(this.__discount1._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "1,", false));
                __myQuery.Append(this.__discount2._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "2,", false));
                __myQuery.Append(this.__discount3._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "3,", false));
                __myQuery.Append(this.__discount4._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "4,", false));
                __myQuery.Append(this.__discount5._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "5,", false));
            }

            __myQuery.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, null);
                this._clear();
                this._myManageMain._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //}
        }
    }
}
