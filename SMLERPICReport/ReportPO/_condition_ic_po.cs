﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport.ReportPO
{
    public partial class _condition_ic_po : MyLib._myForm
    {
        public _condition_ic_po(string __page)
        {
            InitializeComponent();
            /* private MyLib._myPanel _myPanel1;
                    private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
                    private MyLib.VistaButton _bt_exit;
                    private MyLib.VistaButton _bt_process;
                    private MyLib._grouper _grouper1;
                    private MyLib._grouper _grouper2;
                    public SMLReport._whereUserControl _whereControl;
                    public _condition_ic_grid _condition_ic_grid;
                    public _condition_ic_screen _condition_ic_screen1;*/
        }
    }

    public partial class _condition_ic_po_scren : MyLib._myScreen
    {
        public _condition_ic_po_scren()
        {
            this._table_name = _g.d.resource_report._table;
        }

        public void _init(string __page)
        {

            this._table_name = _g.d.resource_report._table;
            if (SMLERPReportTool._reportEnum.ขาย_รายงานใบเสนอซื้อสินค้า.Equals(__page))
            {
                this._maxColumn = 2;                                
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_department, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_department, 1, 1, 1, true, false, true);
            }
        }
    }

    public partial class _codition_ic_po_grid : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        public _codition_ic_po_grid()
        {
            this._clickSearchButton += new MyLib.SearchEventHandler(_codition_ic_po_grid__clickSearchButton);
        }

        void _codition_ic_po_grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string _searchName = this._cellGet(this._selectRow, this._selectColumn).ToString();
            string _search_text_new = _g.g._search_screen_ic_inventory;
            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                this._search_data_full = new MyLib._searchDataFull();
                this._search_data_full._name = _search_text_new;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._refreshData();
                this._search_data_full._dataList._loadViewData(0);
            }
            MyLib._myGlobal._startSearchBox(this._inputTextBox, "ค้นหารหัสสินค้า", this._search_data_full, true);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_ic_inventory) == 0)
            {
                this._search_data_full.Close();
                this._cellUpdate(this._selectRow, this._selectColumn, e._text, false);
            }
        }

        public void _setFromToColumn(string __from_column_name, string __to_column_name)
        {
            this._clear();
            this._addColumn(_g.d.resource_report._table + "." + __from_column_name, 1, 1, 50, true, false, false, true);
            this._addColumn(_g.d.resource_report._table + "." + __to_column_name, 1, 1, 50, true, false, false, true);
            this._width_by_persent = true;
            this.AllowDrop = true;         
            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
        }

        public DataTable _getCondition()
        {
            if (this._rowCount(0) == 0) return null;
            DataTable __dataTable = new DataTable("FromTo");
            __dataTable.Columns.Add("from");
            __dataTable.Columns.Add("to");
            for (int __row = 0; __row < this._rowCount(0); __row++)
            {
                DataRow __dataRow = __dataTable.NewRow();
                __dataRow[0] = this._cellGet(__row, 0).ToString();
                __dataRow[1] = this._cellGet(__row, 1).ToString();
                __dataTable.Rows.Add(__dataRow);
            }
            return __dataTable;
        }
    }
}
