using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _tableSearchLevelControl : UserControl
    {
        public delegate void MenuTableClick(object sender, EventArgs e);

        private float _pictureSize = 100f;
        //private float _zoomScale = 1f;
        public event MenuTableClick _menuTableClick;

        public _tableSearchLevelControl()
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
        }

        void _addControl(FlowLayoutPanel flowPanel, DataTable data, int level, string where, string whereItem)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
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
                _tableSearchLevelMenuControl __menu = new _tableSearchLevelMenuControl(data.Rows[__row][0].ToString(), data.Rows[__row][0].ToString(), "", 0, level);
                __menu.BaseColor = __buttonColor;
                __menu.Click += (s1, e1) =>
                {
                    if (level <= 4)
                    {
                        _tableSearchLevelMenuControl _button = (_tableSearchLevelMenuControl)s1;
                        this._loadMenu(level + 1, where, _button._tableNumber, whereItem);
                    }
                };
                flowPanel.Controls.Add(__menu);
            }
            // ดึงโต๊ะ
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                string __query2 = "select " + _g.d.table_master._number + "," + _g.d.table_master._name_1 + "," + _g.d.table_master._trans_guid + ",coalesce(" + _g.d.table_master._status + ",0)" + ", " + _g.d.table_master._table_barcode + " from " + _g.d.table_master._table + whereItem + " order by " + _g.d.table_master._priority + "," + _g.d.table_master._number;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query2));
                /*string __xquery = "select " + _g.d.images._guid_code + "," + _g.d.images._image_id + " from " + _g.d.images._table + " where " + _g.d.images._image_id + " in (select " + _g.d.table_master._ic_code + " from " + _g.d.table_master._table + whereItem + ")";
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__xquery));*/
                __myquery.Append("</node>");
                string __debug_query = __myquery.ToString();
                ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                //
                DataTable __data2 = ((DataSet)__data[0]).Tables[0];
                //
                this._flowLayoutItem.Controls.Clear();
                for (int __row2 = 0; __row2 < __data2.Rows.Count; __row2++)
                {
                    string __tableNumber = __data2.Rows[__row2][0].ToString();
                    _tableSearchLevelMenuControl __menu2 = new _tableSearchLevelMenuControl(__tableNumber, __data2.Rows[__row2][1].ToString(), __data2.Rows[__row2][2].ToString(), (int)MyLib._myGlobal._decimalPhase(__data2.Rows[__row2][3].ToString()), 0);
                    __menu2._barcode = __data2.Rows[__row2][_g.d.table_master._table_barcode].ToString();
                    __menu2.myImageAlign = ContentAlignment.TopCenter;
                    __menu2.myTextAlign = ContentAlignment.BottomCenter;
                    __menu2.TextAlign = ContentAlignment.BottomCenter;
                    __menu2.Size = new System.Drawing.Size((int)(this._pictureSize * MyLib._myGlobal._searchTableZoomLevel), (int)(this._pictureSize * MyLib._myGlobal._searchTableZoomLevel));
                    // ดึงรูป
                    /*SMLERPControl._getImageData __getImage = new SMLERPControl._getImageData(__itemCode);
                    __getImage._guidList = ((DataSet)__data[1]).Tables[0];
                    __getImage._onLoadImageComplete += (s1, e1) =>
                    {
                        if (e1 != null)
                        {
                            __menu2.mText = "";
                            __menu2.myImage = e1;
                            __menu2.ImageSize = new System.Drawing.Size((int)((this._pictureSize * this._zoomScale) - 10f), (int)((this._pictureSize * this._zoomScale) - 50f));
                            __menu2.Invalidate();
                        }
                    };
                    __getImage._process();*/
                    //
                    __menu2.Click += (s2, e2) =>
                    {
                        if (_menuTableClick != null)
                        {
                            _menuTableClick(s2, e2);
                        }
                    };
                    this._flowLayoutItem.Controls.Add(__menu2);
                }
            }
            catch
            {
            }
        }

        void _loadMenu(int level, string where, string selectValue, string whereItem)
        {
            string __where = "";
            string __whereItem = "";
            FlowLayoutPanel __flowPanel = null;
            string __fieldName = "";
            string __lastFieldName = "";

            switch (level)
            {
                case 1:
                    __flowPanel = this._flowLayout1;
                    __fieldName = _g.d.table_master._level_1;
                    this._flowLayout2.Controls.Clear();
                    this._flowLayout3.Controls.Clear();
                    this._flowLayout4.Controls.Clear();
                    break;
                case 2: __flowPanel = this._flowLayout2;
                    __lastFieldName = _g.d.table_master._level_1;
                    __fieldName = _g.d.table_master._level_2;
                    this._flowLayout3.Controls.Clear();
                    this._flowLayout4.Controls.Clear();
                    break;
                case 3: __flowPanel = this._flowLayout3;
                    __lastFieldName = _g.d.table_master._level_2;
                    __fieldName = _g.d.table_master._level_3;
                    this._flowLayout4.Controls.Clear();
                    break;
                case 4: __flowPanel = this._flowLayout4;
                    __lastFieldName = _g.d.table_master._level_3;
                    __fieldName = _g.d.table_master._level_4;
                    break;
            }
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __where = where + ((where.Trim().Length == 0) ? " where " : " and ") + __fieldName + "<>\'\' and " + __fieldName + " is not null" + ((selectValue.Length > 0) ? " and " + __lastFieldName + "=\'" + selectValue + "\'" : "");
                __whereItem = whereItem + ((selectValue.Trim().Length == 0) ? "" : ((whereItem.Trim().Length == 0) ? " where " : " and ") + __lastFieldName + "=\'" + selectValue + "\'");
                //string __query = "select distinct " + __fieldName + "," + _g.d.table_master._priority + " from " + _g.d.table_master._table + __where + " order by " + _g.d.table_master._priority;
                string __query = "select distinct " + __fieldName + " from ( select " + __fieldName + "," + _g.d.table_master._priority + " from " + _g.d.table_master._table + __where + " order by " + _g.d.table_master._priority + ") as temp1";
                DataTable __data = __myFrameWork._queryShort(__query).Tables[0];
                this._addControl(__flowPanel, __data, level, __where, __whereItem);
            }
            catch
            {
            }
        }

        public string __where { get; set; }

        void _redrawItem()
        {
            foreach (_tableSearchLevelMenuControl __control in this._flowLayoutItem.Controls)
            {
                __control.Size = new Size((int)(this._pictureSize * MyLib._myGlobal._searchTableZoomLevel), (int)(this._pictureSize * MyLib._myGlobal._searchTableZoomLevel));
                __control.ImageSize = new System.Drawing.Size((int)((this._pictureSize * MyLib._myGlobal._searchTableZoomLevel) - 10f), (int)((this._pictureSize * MyLib._myGlobal._searchTableZoomLevel) - 50f));
            };
        }

        private void _zoomInButton_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._searchTableZoomLevel = MyLib._myGlobal._searchTableZoomLevel + (MyLib._myGlobal._searchTableZoomLevel * 0.1f);
            this._redrawItem();
        }

        private void _zoomOutButton_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._searchTableZoomLevel = MyLib._myGlobal._searchTableZoomLevel - (MyLib._myGlobal._searchTableZoomLevel * 0.1f);
            this._redrawItem();
        }
    }
}
