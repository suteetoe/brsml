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
    public partial class _icBarcodeDiscount : UserControl
    {
        private _icBarcodeDiscountControl _screenControl = new _icBarcodeDiscountControl();
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private _icBarcodeDiscountGrid __discount1;
        private _icBarcodeDiscountGrid __discount2;
        private _icBarcodeDiscountGrid __discount3;
        private _icBarcodeDiscountGrid __discount4;
        private _icBarcodeDiscountGrid __discount5;
        private string _oldBarcode = "";

        public _icBarcodeDiscount()
        {
            InitializeComponent();
            //
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            //
            this._saveButton = new MyLib.ToolStripMyButton();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (this._myToolBar != null)
                {
                    this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                }
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
            this._myToolBar.Items.Add(this._saveButton);
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
            this._myManageMain._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageMain__loadDataToScreen);
            this._myManageMain._manageButton = this._myToolBar;
            //this._myManageDetail._manageBackgroundPanel = this._myPanel1;
            this._myManageMain._newDataClick += new MyLib.NewDataEvent(_myManageMain__newDataClick);
            this._myManageMain._discardData += new MyLib.DiscardDataEvent(_myManageMain__discardData);
            this._myManageMain._clearData += new MyLib.ClearDataEvent(_myManageMain__clearData);
            this._myManageMain._closeScreen += new MyLib.CloseScreenEvent(_myManageMain__closeScreen);
            this._myManageMain._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageMain._dataList._loadViewData(0);
            this._myManageMain._calcArea();
            this._myManageMain._dataListOpen = true;
            this._myManageMain._autoSize = true;
            this._myManageMain._autoSizeHeight = 450;
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._myManageMain.Invalidate();
            //
            this._myToolBar.EnabledChanged += new EventHandler(_myToolBar_EnabledChanged);
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

        void _myToolBar_EnabledChanged(object sender, EventArgs e)
        {

        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            if (this._oldBarcode.Length > 0)
            {
                this.__discount1._grid._updateRowIsChangeAll(true);
                this.__discount2._grid._updateRowIsChangeAll(true);
                this.__discount3._grid._updateRowIsChangeAll(true);
                this.__discount4._grid._updateRowIsChangeAll(true);
                string __fieldList = _g.d.ic_inventory_barcode_price._barcode + "," + _g.d.ic_inventory_barcode_price._discount_type + ",";
                string __dataList = "\'" + this._oldBarcode + "\',";
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_barcode_price._table + " where " + _g.d.ic_inventory_barcode_price._barcode + "=\'" + this._oldBarcode + "\'"));
                __myQuery.Append(this.__discount1._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "1,", false));
                __myQuery.Append(this.__discount2._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "2,", false));
                __myQuery.Append(this.__discount3._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "3,", false));
                __myQuery.Append(this.__discount4._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "4,", false));
                __myQuery.Append(this.__discount5._grid._createQueryForInsert(_g.d.ic_inventory_barcode_price._table, __fieldList, __dataList + "5,", false));
                __myQuery.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    this._myManageMain._afterUpdateData();
                    this._clear();
                    this._myManageMain._dataList._refreshData();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {

        }

        void _myManageMain__closeScreen()
        {
            this.Dispose();
        }

        void _myManageMain__clearData()
        {
            this._clear();
        }

        bool _myManageMain__discardData()
        {
            return true;
        }

        void _myManageMain__newDataClick()
        {

        }

        void _clear()
        {
            this._oldBarcode = "";
            this._screenControl._icBarcodeDiscountScreen._clear();
            this.__discount1._grid._clear();
            this.__discount2._grid._clear();
            this.__discount3._grid._clear();
            this.__discount4._grid._clear();
            this.__discount5._grid._clear();
        }

        bool _myManageMain__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                this._clear();
                StringBuilder __myquery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                int _getColumnCode = this._myManageMain._dataList._gridData._findColumnByName(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode);
                this._oldBarcode = __rowDataArray[_getColumnCode].ToString();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_barcode._table + whereString));
                string __queryDiscount = "select * from " + _g.d.ic_inventory_barcode_price._table + " where " + _g.d.ic_inventory_barcode_price._barcode + "=\'" + this._oldBarcode + "\' and " + _g.d.ic_inventory_barcode_price._discount_type + "={0} order by " + _g.d.ic_inventory_barcode_price._priority;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(__queryDiscount, "1")));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(__queryDiscount, "2")));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(__queryDiscount, "3")));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(__queryDiscount, "4")));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(__queryDiscount, "5")));
                __myquery.Append("</node>");

                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._screenControl._icBarcodeDiscountScreen._loadData(((DataSet)__getData[0]).Tables[0]);
                this.__discount1._grid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this.__discount2._grid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                this.__discount3._grid._loadFromDataTable(((DataSet)__getData[3]).Tables[0]);
                this.__discount4._grid._loadFromDataTable(((DataSet)__getData[4]).Tables[0]);
                this.__discount5._grid._loadFromDataTable(((DataSet)__getData[5]).Tables[0]);
                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }
    }

    public class _icBarcodeDiscountScreenControl : MyLib._myScreen
    {
        public _icBarcodeDiscountScreenControl()
        {
            this._table_name = _g.d.ic_inventory_barcode._table;
            this._maxColumn = 2;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_inventory_barcode._barcode, 1, 1, 0, true, false, false);
            this._addTextBox(0, 1, 1, 0, _g.d.ic_inventory_barcode._ic_code, 2, 1, 0, true, false, false);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_inventory_barcode._description, 2, 1, 0, true, false, false);
            this._addTextBox(1, 1, 1, 0, _g.d.ic_inventory_barcode._unit_code, 2, 1, 0, true, false, false);
            this._addNumberBox(2, 0, 1, 0, _g.d.ic_inventory_barcode._price, 1, 0, true);
        }
    }
}
