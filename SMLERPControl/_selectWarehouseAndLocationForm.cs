using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _selectWarehouseAndLocationForm : Form
    {
        public string _fieldCheck = "Check";
        private string _whCodeActive = "";
        private Boolean _multiSelect = false;
        private int _mode = 0;

        /// <summary>
        /// mode 0 = all, 1 wh only
        /// </summary>
        /// <param name="multiSelect"></param>
        /// <param name="mode"></param>
        public _selectWarehouseAndLocationForm(Boolean multiSelect, int mode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._multiSelect = multiSelect;
            this._mode = mode;
            this._build();
        }

        public _selectWarehouseAndLocationForm(Boolean multiSelect)
        {
            InitializeComponent();
            //
            this._multiSelect = multiSelect;
            this._build();

        }

        void _build()
        {
            this._whGrid._table_name = _g.d.ic_warehouse._table;
            this._whGrid._addColumn(this._fieldCheck, 11, 10, 10);
            this._whGrid._addColumn(_g.d.ic_warehouse._code, 1, 20, 20);
            this._whGrid._addColumn(_g.d.ic_warehouse._name_1, 1, 30, 30);
            this._whGrid._addColumn(_g.d.ic_warehouse._address, 1, 40, 40);
            this._whGrid._calcPersentWidthToScatter();
            this._whGrid._isEdit = false;
            this._whGrid.Invalidate();
            //
            this._locationGrid._table_name = _g.d.ic_shelf._table;
            this._locationGrid._addColumn(this._fieldCheck, 11, 10, 10);
            this._locationGrid._addColumn(_g.d.ic_shelf._whcode, 1, 20, 30);
            this._locationGrid._addColumn(_g.d.ic_shelf._code, 1, 20, 30);
            this._locationGrid._addColumn(_g.d.ic_shelf._name_1, 1, 30, 60);
            this._locationGrid._isEdit = false;
            this._locationGrid._calcPersentWidthToScatter();
            this._locationGrid.Invalidate();
            //
            this._loadData();
            //
            if (this._multiSelect == false)
            {
                this._whDeSelectAllButton.Visible = false;
                this._whSelectAllButton.Visible = false;
                this._locationSelectAllButton.Visible = false;
                this._locationDeSelectAllButton.Visible = false;
                //
                this._whGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_whGrid__alterCellUpdate);
                this._locationGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_locationGrid__alterCellUpdate);
            }
            else
            {
                this._whSelectAllButton.Click += (s1, e1) => { this._whSelectChangeAll(true); };
                this._whDeSelectAllButton.Click += (s1, e1) => { this._whSelectChangeAll(false); };
                this._locationSelectAllButton.Click += (s1, e1) => { this._locationSelectChangeAll(true); };
                this._locationDeSelectAllButton.Click += (s1, e1) => { this._locationSelectChangeAll(false); };
            }

            if (this._mode == 1)
            {
                this.splitContainer1.Panel2Collapsed = true;
                this.splitContainer1.Panel2.Hide();
            }
        }

        void _locationGrid__alterCellUpdate(object sender, int row, int column)
        {
            if (this._locationGrid._cellGet(row, 0).ToString().Equals("1"))
            {
                for (int __loop = 0; __loop < this._locationGrid._rowData.Count; __loop++)
                {
                    if (__loop != row)
                    {
                        this._locationGrid._cellUpdate(__loop, 0, 0, false);
                    }
                }
                this._locationGrid.Invalidate();
            }
        }

        void _whGrid__alterCellUpdate(object sender, int row, int column)
        {
            if (this._whGrid._cellGet(row, 0).ToString().Equals("1"))
            {
                for (int __loop = 0; __loop < this._whGrid._rowData.Count; __loop++)
                {
                    if (__loop != row)
                    {
                        this._whGrid._cellUpdate(__loop, 0, 0, false);
                    }
                }
                this._whGrid.Invalidate();
            }
        }

        /// <summary>
        /// เอาไปใช้ใน Query
        /// </summary>
        /// <returns></returns>
        public string _wareHouseSelected()
        {
            StringBuilder __result = new StringBuilder();
            for (int __row = 0; __row < this._whGrid._rowData.Count; __row++)
            {
                if (this._whGrid._cellGet(__row, this._fieldCheck).ToString().Equals("1"))
                {
                    if (__result.Length > 0)
                    {
                        __result.Append(",");
                    }
                    __result.Append("\'" + this._whGrid._cellGet(__row, _g.d.ic_warehouse._code).ToString() + "\'");
                }
            }
            return __result.ToString();
        }

        /// <summary>
        /// เอาไปใช้ใน Query
        /// </summary>
        /// <returns></returns>
        public string _locationSelected()
        {
            StringBuilder __result = new StringBuilder();
            for (int __row = 0; __row < this._locationGrid._rowData.Count; __row++)
            {
                if (this._locationGrid._cellGet(__row, this._fieldCheck).ToString().Equals("1"))
                {
                    if (__result.Length > 0)
                    {
                        __result.Append(",");
                    }
                    __result.Append("\'" + this._locationGrid._cellGet(__row, _g.d.ic_shelf._code).ToString() + "\'");
                }
            }
            return __result.ToString();
        }

        /// <summary>
        /// เอาไปใช้ใน Query
        /// </summary>
        /// <returns></returns>
        public string _wareHouseLocationSelected(string warehouseFieldName, string locationFieldName)
        {
            StringBuilder __result = new StringBuilder();
            for (int __row = 0; __row < this._locationGrid._rowData.Count; __row++)
            {
                if (this._locationGrid._cellGet(__row, this._fieldCheck).ToString().Equals("1"))
                {
                    if (__result.Length > 0)
                    {
                        __result.Append(" or ");
                    }
                    __result.Append("(" + warehouseFieldName + "=\'" + this._locationGrid._cellGet(__row, _g.d.ic_shelf._whcode).ToString() + "\' and " + locationFieldName + "=\'" + this._locationGrid._cellGet(__row, _g.d.ic_shelf._code).ToString() + "\')");
                }
            }
            return __result.ToString();
        }

        /// <summary>
        /// เอาไปพิมพ์หัวรายงาน
        /// </summary>
        /// <returns></returns>
        public string _header()
        {
            StringBuilder __warehouse = new StringBuilder();
            for (int __row = 0; __row < this._whGrid._rowData.Count; __row++)
            {
                if (this._whGrid._cellGet(__row, this._fieldCheck).ToString().Equals("1"))
                {
                    if (__warehouse.Length > 0)
                    {
                        __warehouse.Append(",");
                    }
                    else
                    {
                        __warehouse.Append("คลัง" + " : ");
                    }
                    __warehouse.Append(this._whGrid._cellGet(__row, _g.d.ic_warehouse._code).ToString());
                }
            }
            // ที่เก็บ
            StringBuilder __location = new StringBuilder();
            for (int __row = 0; __row < this._locationGrid._rowData.Count; __row++)
            {
                if (this._locationGrid._cellGet(__row, this._fieldCheck).ToString().Equals("1"))
                {
                    if (__location.Length > 0)
                    {
                        __location.Append(",");
                    }
                    else
                    {
                        __location.Append("พื้นที่เก็บ" + " : ");
                    }
                    __location.Append(this._locationGrid._cellGet(__row, _g.d.ic_shelf._code).ToString());
                }
            }
            string __result = __warehouse.ToString() + ((__warehouse.Length > 0) ? " " : "") + __location.ToString();
            return __result;
        }


        private void _whSelectChangeAll(Boolean mode)
        {
            for (int __row = 0; __row < this._whGrid._rowData.Count; __row++)
            {
                this._whGrid._cellUpdate(__row, this._fieldCheck, (mode) ? 1 : 0, false);
            }
            this._whGrid.Invalidate();
        }

        private void _locationSelectChangeAll(Boolean mode)
        {
            for (int __row = 0; __row < this._locationGrid._rowData.Count; __row++)
            {
                this._locationGrid._cellUpdate(__row, this._fieldCheck, (mode) ? 1 : 0, false);
            }
            this._locationGrid.Invalidate();
        }

        public void _loadData()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            this._whGrid._loadFromDataTable(__myFrameWork._queryShort("select " + _g.d.ic_warehouse._code + "," + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " order by " + _g.d.ic_warehouse._code).Tables[0]);
            this._whGrid.Invalidate();
            //
            this._locationGrid._loadFromDataTable(__myFrameWork._queryShort("select distinct " + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code + "," + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " order by " + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code).Tables[0]);
            this._locationGrid.Invalidate();
        }
    }
}
