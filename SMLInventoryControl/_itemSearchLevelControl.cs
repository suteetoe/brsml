using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _itemSearchLevelControl : UserControl
    {
        private float _pictureSize = 100f;
        private float _groupSize = 50f;
        //private float _zoomScale = 1f;
        public event MenuItemClick _menuItemClick;
        public decimal _qty = 0M;

        private bool _showItemPrice = true;

        public int _barcode_price_level = 0;
        private bool _productBasketResult = false;
        private MyLib._myFrameWork _frameWork = new MyLib._myFrameWork();

        public bool _productBasket
        {
            get
            {
                return _productBasketResult;
            }
            set
            {
                _productBasketResult = value;
                this.splitContainer1.Panel2Collapsed = (_productBasketResult == false) ? true : false;
            }
        }

        public string _barcode = "";

        public _itemSearchLevelControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._flowLayout1.AutoSize = true;
            this._flowLayout2.AutoSize = true;
            this._flowLayout3.AutoSize = true;
            this._flowLayout4.AutoSize = true;
            this.DoubleBuffered = true;
            this._loadMenu(1, "", "", "");

            // addgrid
            this._selectGrid._table_name = _g.d.ic_trans_detail._table;
            this._selectGrid._addColumn(_g.d.ic_trans_detail._item_code, 1, 0, 10, true, true);
            this._selectGrid._addColumn(_g.d.ic_trans_detail._item_name, 1, 0, 80, false, false);
            this._selectGrid._addColumn(_g.d.ic_trans_detail._barcode, 1, 0, 10, true, true);
            this._selectGrid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 0, 10, true, true);
            this._selectGrid._addColumn(_g.d.ic_trans_detail._qty, 3, 0, 20, false, false, true, false, _g.g._getFormatNumberStr(2));
            this._selectGrid._addColumn(_g.d.ic_trans_detail._price, 3, 0, 10, true, true, true, false, _g.g._getFormatNumberStr(2));
            this._selectGrid._addColumn(_g.d.ic_trans_detail._remark, 1, 0, 10, true, true);

            this.splitContainer1.Panel2Collapsed = true;

            this.Load += new EventHandler(_itemSearchLevelControl_Load);
        }

        public _itemSearchLevelControl(bool priceHidden)
        {

            InitializeComponent();

            this._showItemPrice = priceHidden;
            this._flowLayout1.AutoSize = true;
            this._flowLayout2.AutoSize = true;
            this._flowLayout3.AutoSize = true;
            this._flowLayout4.AutoSize = true;
            this.DoubleBuffered = true;
            this._loadMenu(1, "", "", "");
            this.Load += new EventHandler(_itemSearchLevelControl_Load);
        }

        void _itemSearchLevelControl_Load(object sender, EventArgs e)
        {
            this._qtyTextBox.Text = "1";
        }

        void _addControl(FlowLayoutPanel flowPanel, DataTable data, int level, string where, string whereItem)
        {
            if (flowPanel != null)
                flowPanel.Controls.Clear();
            Color __buttonColor = Color.Cyan;
            switch (level)
            {
                case 1: __buttonColor = Color.Cyan; break;
                case 2: __buttonColor = Color.LightYellow; break;
                case 3: __buttonColor = Color.OrangeRed; break;
                case 4: __buttonColor = Color.AliceBlue; break;
            }
            for (int __row = 0; __row < data.Rows.Count; __row++)
            {
                _itemSearchLevelMenuControl __menu = new _itemSearchLevelMenuControl(data.Rows[__row][1].ToString(), data.Rows[__row][1].ToString(), "", "", 0M, level, "");
                __menu.BaseColor = __buttonColor;
                __menu.Click += (s1, e1) =>
                {
                    if (level <= 4)
                    {
                        _itemSearchLevelMenuControl _button = (_itemSearchLevelMenuControl)s1;
                        this._loadMenu(level + 1, where, _button._itemCode, whereItem);
                    }
                };
                if (flowPanel != null)
                    flowPanel.Controls.Add(__menu);
            }
            // ดึงสินค้า
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                string __query2 = "select " + _g.d.ic_inventory_level._ic_code +

                    ",(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._ic_code + ") as " + _g.d.ic_inventory._name_1 +
                    "," + _g.d.ic_inventory_level._barcode + "," + _g.d.ic_inventory_level._unit_code + "," + _g.d.ic_inventory_level._price + ", " + _g.d.ic_inventory_level._suggest_remark +
                    ", (select " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._price + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._barcode + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._ic_code + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._unit_code + ") as barcode_price_1 " +
                    ", (select " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._price_2 + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._barcode + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._ic_code + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._unit_code + ") as barcode_price_2 " +
                    ", (select " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._price_3 + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._barcode + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._ic_code + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._unit_code + ") as barcode_price_3 " +
                    ", (select " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._price_4 + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._barcode + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._ic_code + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " = " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._unit_code + ") as barcode_price_4 " +
                    ", (select " + _g.d.ic_inventory._barcode_checker_print + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._ic_code + ") as " + _g.d.ic_inventory._barcode_checker_print +
                    ",(case when level_4~E'^\\\\d+$' then cast(level_4 as integer) else 9999 end) as item_sort " +
                    ",(select " + _g.d.ic_inventory._name_eng_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._ic_code + ") as " + _g.d.ic_inventory._name_eng_1 +
                    ",(select " + _g.d.ic_unit._name_2 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._unit_code + ") as unit_eng " +
                    " from " + _g.d.ic_inventory_level._table + whereItem; // +" order by , " + _g.d.ic_inventory_level._ic_code;

                string __query3 = " select * from ( " + __query2 + ") as yy where coalesce((select is_hold_sale from ic_inventory_detail where ic_inventory_detail.ic_code = yy.ic_code), 0)=0 order by item_sort, " + _g.d.ic_inventory_level._ic_code;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                string __xquery = "select " + _g.d.images._guid_code + "," + _g.d.images._image_id + " from " + _g.d.images._table + " where " + _g.d.images._image_id + " in (select " + _g.d.ic_inventory_level._ic_code + " from " + _g.d.ic_inventory_level._table + whereItem + ")";
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__xquery));

                string __barcodequery = "select " + _g.d.images._guid_code + "," + _g.d.images._image_id + " from " + _g.d.images._table + " where " + _g.d.images._image_id + " in (select " + _g.d.ic_inventory_level._barcode + " from " + _g.d.ic_inventory_level._table + whereItem + ")";
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__barcodequery));
                __myquery.Append("</node>");

                string __debug_query = __myquery.ToString();
                ArrayList __data = this._frameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                //
                DataTable __data2 = ((DataSet)__data[0]).Tables[0];
                //
                this._flowLayoutItem.SuspendLayout();
                this._flowLayoutItem.Controls.Clear();
                for (int __row2 = 0; __row2 < __data2.Rows.Count; __row2++)
                {
                    if (__row2 > 3000)
                        break;

                    string __itemCode = __data2.Rows[__row2][_g.d.ic_inventory_level._ic_code].ToString();

                    decimal _itemPrice = 0M;// MyLib._myGlobal._decimalPhase(__data2.Rows[__row2][4].ToString());
                    //if (_itemPrice == -1)
                    //{
                    switch (_barcode_price_level)
                    {
                        case 4:
                            _itemPrice = MyLib._myGlobal._decimalPhase(__data2.Rows[__row2]["barcode_price_4"].ToString());
                            break;
                        case 3:
                            _itemPrice = MyLib._myGlobal._decimalPhase(__data2.Rows[__row2]["barcode_price_3"].ToString());
                            break;
                        case 2:
                            _itemPrice = MyLib._myGlobal._decimalPhase(__data2.Rows[__row2]["barcode_price_2"].ToString());
                            break;
                        default:
                            _itemPrice = MyLib._myGlobal._decimalPhase(__data2.Rows[__row2]["barcode_price_1"].ToString());
                            break;

                    }
                    //}
                    // price process 
                    // price -1 use barcode_price
                    string __barcode = __data2.Rows[__row2][2].ToString();

                    _itemSearchLevelMenuControl __menu2 = new _itemSearchLevelMenuControl(__itemCode,
                        __data2.Rows[__row2][_g.d.ic_inventory._name_1].ToString()
                        , __data2.Rows[__row2][_g.d.ic_inventory_level._barcode].ToString(), __data2.Rows[__row2][_g.d.ic_inventory_level._unit_code].ToString(), (this._showItemPrice) ? _itemPrice : 0M, 0, __data2.Rows[__row2][_g.d.ic_inventory_level._suggest_remark].ToString());
                    __menu2.myImageAlign = ContentAlignment.TopCenter;
                    __menu2.myTextAlign = ContentAlignment.BottomCenter;
                    __menu2.TextAlign = ContentAlignment.BottomCenter;
                    __menu2.Size = new System.Drawing.Size((int)(this._pictureSize * MyLib._myGlobal._searchItemZoomLevel), (int)(this._pictureSize * MyLib._myGlobal._searchItemZoomLevel));
                    __menu2._print_checker = __data2.Rows[__row2][_g.d.ic_inventory._barcode_checker_print].ToString().Equals("1") ? true : false;
                    __menu2._unitNameEng = __data2.Rows[__row2]["unit_eng"].ToString();
                    __menu2._nameEng = __data2.Rows[__row2][_g.d.ic_inventory._name_eng_1].ToString();
                    // ดึงรูป
                    SMLERPControl._getImageData __getImage = new SMLERPControl._getImageData(__itemCode);

                    if (_g.g._companyProfile._barcode_picture)
                    {
                        __getImage._imageCode = __barcode;
                        __getImage._guidList = ((DataSet)__data[2]).Tables[0];
                    }
                    else
                    {
                        __getImage._guidList = ((DataSet)__data[1]).Tables[0];
                    }
                    __getImage._onLoadImageComplete += (s1, e1) =>
                    {
                        if (e1 != null)
                        {
                            __menu2.mText = "";
                            __menu2.myImage = e1;
                            __menu2.ImageSize = new System.Drawing.Size((int)((this._pictureSize * MyLib._myGlobal._searchItemZoomLevel) - 10f), (int)((this._pictureSize * MyLib._myGlobal._searchItemZoomLevel) - 50f));
                            __menu2.Invalidate();
                        }
                    };
                    __getImage._process();
                    //
                    __menu2.Click += (s2, e2) =>
                    {
                        // update row
                        if (this._productBasket)
                        {
                            _updateGridRow(s2);
                        }
                        else
                        {
                            if (_menuItemClick != null)
                            {
                                this._qty = MyLib._myGlobal._decimalPhase(this._qtyTextBox.Text.Trim());
                                this._qtyTextBox.Text = "1";
                                _menuItemClick(s2, e2);
                            }
                        }
                        // เลือกสินค้า
                        // this._loadMenu(level + 1, where);
                    };
                    this._flowLayoutItem.Controls.Add(__menu2);
                }
                this._flowLayoutItem.ResumeLayout(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void _updateGridRow(object sender)
        {
            _itemSearchLevelMenuControl __menuItem = (_itemSearchLevelMenuControl)sender;

            this._barcode = __menuItem._barcode;

            this._qty = MyLib._myGlobal._decimalPhase(this._qtyTextBox.Text.Trim());
            this._qtyTextBox.Text = "1";
            bool __found = false;
            // for grid
            for (int __i = 0; __i < this._selectGrid._rowData.Count; __i++)
            {
                if (this._selectGrid._cellGet(__i, _g.d.ic_trans_detail._item_code).ToString().Equals(__menuItem._itemCode) && this._selectGrid._cellGet(__i, _g.d.ic_trans_detail._unit_code).ToString().Equals(__menuItem._unitCode))
                {
                    __found = true;
                    decimal __qty = MyLib._myGlobal._decimalPhase(this._selectGrid._cellGet(__i, _g.d.ic_trans_detail._qty).ToString()) + this._qty;

                    this._selectGrid._cellUpdate(__i, _g.d.ic_trans_detail._qty, __qty, true);
                    this._selectGrid.Invalidate();
                    break;
                }
            }

            if (__found == false)
            {
                int __newRow = this._selectGrid._addRow();
                this._selectGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._item_code, __menuItem._itemCode, false);
                this._selectGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._item_name, __menuItem._itemName, false);
                this._selectGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._barcode, __menuItem._barcode, false);
                this._selectGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._unit_code, __menuItem._unitCode, false);
                this._selectGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._remark, __menuItem._suggest_remark, false);
                this._selectGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._price, __menuItem._price, true);
                this._selectGrid._cellUpdate(__newRow, _g.d.ic_trans_detail._qty, this._qty, true);
            }
        }


        void _loadMenu(int level, string where, string selectValue, string whereItem)
        {
            string __where = "";
            string __whereItem = "";
            FlowLayoutPanel __flowPanel = null;
            string __fieldName = "";
            string __lastFieldName = "";
            string __extraSort = "";

            switch (level)
            {
                case 1:
                    __flowPanel = this._flowLayout1;
                    __fieldName = _g.d.ic_inventory_level._level_1;
                    __extraSort = " case when " + __fieldName + " ~ '^[0-9]' then to_number((case when strpos(" + __fieldName + ", '.') > 0 then substr(" + __fieldName + ",0, strpos(" + __fieldName + ", '.')) else '0' end ), 'FMS999999.99') else 0 end, ";

                    this._flowLayout2.Controls.Clear();
                    this._flowLayout3.Controls.Clear();
                    this._flowLayout4.Controls.Clear();
                    break;
                case 2:
                    __flowPanel = this._flowLayout2;
                    __lastFieldName = _g.d.ic_inventory_level._level_1;
                    __fieldName = _g.d.ic_inventory_level._level_2;
                    __extraSort = " case when " + __fieldName + " ~ '^[0-9]' then to_number((case when strpos(" + __fieldName + ", '.') > 0 then substr(" + __fieldName + ",0, strpos(" + __fieldName + ", '.')) else '0' end ), 'FMS999999.99') else 0 end, ";
                    this._flowLayout3.Controls.Clear();
                    this._flowLayout4.Controls.Clear();
                    break;
                case 3:
                    __flowPanel = this._flowLayout3;
                    __lastFieldName = _g.d.ic_inventory_level._level_2;
                    __fieldName = _g.d.ic_inventory_level._level_3;
                    __extraSort = " case when " + __fieldName + " ~ '^[0-9]' then to_number((case when strpos(" + __fieldName + ", '.') > 0 then substr(" + __fieldName + ",0, strpos(" + __fieldName + ", '.')) else '0' end ), 'FMS999999.99') else 0 end, ";
                    this._flowLayout4.Controls.Clear();
                    break;
                case 4:
                    __flowPanel = this._flowLayout4;
                    __lastFieldName = _g.d.ic_inventory_level._level_3;
                    __fieldName = _g.d.ic_inventory_level._level_4;
                    __extraSort = " case when " + __fieldName + " ~ '^[0-9]' then to_number((case when strpos(" + __fieldName + ", '.') > 0 then substr(" + __fieldName + ",0, strpos(" + __fieldName + ", '.')) else '0' end ), 'FMS999999.99') else 0 end, ";
                    break;
                case 5:
                    __flowPanel = null;
                    __lastFieldName = _g.d.ic_inventory_level._level_4;
                    __fieldName = _g.d.ic_inventory_level._level_4;
                    __extraSort = " case when " + __fieldName + " ~ '^[0-9]' then to_number((case when strpos(" + __fieldName + ", '.') > 0 then substr(" + __fieldName + ",0, strpos(" + __fieldName + ", '.')) else '0' end ), 'FMS999999.99') else 0 end, ";
                    break;
            }
            try
            {
                __where = where + ((where.Trim().Length == 0) ? " where " : " and ") + __fieldName + "<>\'\' and " + __fieldName + " is not null" + ((selectValue.Length > 0) ? " and " + __lastFieldName + "=\'" + selectValue + "\'" : "");
                __whereItem = whereItem + ((selectValue.Trim().Length == 0) ? "" : ((whereItem.Trim().Length == 0) ? " where " : " and ") + __lastFieldName + "=\'" + selectValue + "\'");
                string __query = " select distinct " + __extraSort + __fieldName + " from " + _g.d.ic_inventory_level._table + __where + " order by " + __extraSort + __fieldName;
                DataTable __data = this._frameWork._queryShort(__query).Tables[0];
                this._addControl(__flowPanel, __data, level, __where, __whereItem);
            }
            catch
            {
            }
        }

        public string __where { get; set; }

        void _redrawItem()
        {
            foreach (_itemSearchLevelMenuControl __control in this._flowLayoutItem.Controls)
            {
                __control.Size = new Size((int)(this._pictureSize * MyLib._myGlobal._searchItemZoomLevel), (int)(this._pictureSize * MyLib._myGlobal._searchItemZoomLevel));
                __control.ImageSize = new System.Drawing.Size((int)((this._pictureSize * MyLib._myGlobal._searchItemZoomLevel) - 10f), (int)((this._pictureSize * MyLib._myGlobal._searchItemZoomLevel) - 50f));
            };
        }

        private void _zoomInButton_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._searchItemZoomLevel = MyLib._myGlobal._searchItemZoomLevel + (MyLib._myGlobal._searchItemZoomLevel * 0.1f);
            this._redrawItem();
        }

        private void _zoomOutButton_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._searchItemZoomLevel = MyLib._myGlobal._searchItemZoomLevel - (MyLib._myGlobal._searchItemZoomLevel * 0.1f);
            this._redrawItem();
        }

        private void _qtyAddButton_Click(object sender, EventArgs e)
        {
            this._qtyTextBox.Text = (((int)MyLib._myGlobal._decimalPhase(this._qtyTextBox.Text)) + 1).ToString();
        }

        private void _qtyReduceButton_Click(object sender, EventArgs e)
        {
            int __qty = (int)MyLib._myGlobal._decimalPhase(this._qtyTextBox.Text);
            if (__qty > 1)
            {
                this._qtyTextBox.Text = (__qty - 1).ToString();
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (_menuItemClick != null)
            {
                _menuItemClick(this, e);
                this._selectGrid._clear();

            }
        }

        private void _renewItemButton_Click(object sender, EventArgs e)
        {
            this._selectGrid._clear();
        }

        private void _deleteLastItemButton_Click(object sender, EventArgs e)
        {
            // remove last barcode select
            if (this._barcode.Equals(""))
            {
                return;
            }

            for (int __row = 0; __row < this._selectGrid._rowData.Count; __row++)
            {
                string __barcode = this._selectGrid._cellGet(__row, _g.d.ic_trans_detail._barcode).ToString();
                if (__barcode.Equals(this._barcode))
                {
                    this._selectGrid._rowData.RemoveAt(__row);
                    this._selectGrid.Invalidate();
                    this._barcode = "";
                    break;
                }
            }
        }
    }

    public delegate void MenuItemClick(object sender, EventArgs e);
}
