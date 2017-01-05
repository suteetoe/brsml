using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransWeightControl : Form
    {
        _g.g.ProjectAllowcateEnum _controlTypeEnumTemp;
        MyLib._searchDataFull _searchMaster;
        string _table_name = "";
        public string _columnItemCode = "";
        public string _ColumnItemName = "";
        string _screenName = "";

        public delegate Boolean SaveDataEventHandler(object sender);
        //
        public event SaveDataEventHandler _saveData;

        public _g.g.ProjectAllowcateEnum _controlTypeEnum
        {
            get
            {
                return _controlTypeEnumTemp;
            }
            set
            {
                _controlTypeEnumTemp = value;
                this._build();
            }
        }
        public _icTransWeightControl(_g.g.ProjectAllowcateEnum type, String code, decimal value)
        {
            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._controlTypeEnum = type;
            this._screen._setDataStr(_g.d.ic_trans_detail._item_code, code);
            this._screen._setDataNumber(_g.d.ic_trans._total_amount, value);
            this.Text = _screenName + " " + code + " : " + value;
        }

        public void _loadData(_icTransItemGridControl.icTransWeightStruct source)
        {
            for (int __row = 0; __row < source.__details.Count; __row++)
            {
                _icTransItemGridControl.icTransWeightDetailStruct __detail = source.__details[__row];
                int __addr = this._grid._addRow();
                this._grid._cellUpdate(__addr, this._columnItemCode, __detail._code, false);
                this._grid._cellUpdate(__addr, this._ColumnItemName, __detail._name, false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans_detail_department._ratio, __detail._ratio, false);
                this._grid._cellUpdate(__addr, _g.d.ic_trans_detail_department._amount, __detail._amount, false);
            }

        }

        void _build()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);
            this._searchMaster = new MyLib._searchDataFull();

            switch (this._controlTypeEnum)
            {
                case _g.g.ProjectAllowcateEnum.แผนก:
                    {
                        this._searchMaster._dataList._loadViewFormat(_g.g._search_screen_erp_department_list, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._table_name = _g.d.ic_trans_detail_department._table;
                        this._columnItemCode = _g.d.ic_trans_detail_department._department_code;
                        this._ColumnItemName = _g.d.ic_trans_detail_department._department_name;
                        _screenName = "การถัวเฉลี่ยแผนก";
                    }
                    break;
                case _g.g.ProjectAllowcateEnum.โครงการ:
                    {
                        this._searchMaster._dataList._loadViewFormat(_g.g._search_master_erp_project_list, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._table_name = _g.d.ic_trans_detail_project._table;
                        this._columnItemCode = _g.d.ic_trans_detail_project._project_code;
                        this._ColumnItemName = _g.d.ic_trans_detail_project._project_name;
                        _screenName = "การถัวเฉลี่ยโครงการ";
                    }
                    break;
                case _g.g.ProjectAllowcateEnum.การจัดสรร:
                    {
                        this._searchMaster._dataList._loadViewFormat(_g.g._search_master_erp_allocate_list, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._table_name = _g.d.ic_trans_detail_allocate._table;
                        this._columnItemCode = _g.d.ic_trans_detail_allocate._allocate_code;
                        this._ColumnItemName = _g.d.ic_trans_detail_allocate._allocate_name;
                        _screenName = "การถัวเฉลี่ยการจัดสรร";
                    }
                    break;
                case _g.g.ProjectAllowcateEnum.หน่วยงาน:
                    {
                        this._searchMaster._dataList._loadViewFormat(_g.g._search_screen_erp_side_list, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._table_name = _g.d.ic_trans_detail_site._table;
                        this._columnItemCode = _g.d.ic_trans_detail_site._site_code;
                        this._ColumnItemName = _g.d.ic_trans_detail_site._site_name;
                        _screenName = "การถัวเฉลี่ยฝ่าย";
                    }
                    break;
                case _g.g.ProjectAllowcateEnum.งาน:
                    {
                        this._searchMaster._dataList._loadViewFormat(_g.g._search_master_erp_job_list, MyLib._myGlobal._userSearchScreenGroup, false);
                        this._table_name = _g.d.ic_trans_detail_jobs._table;
                        this._columnItemCode = _g.d.ic_trans_detail_jobs._job_code;
                        this._ColumnItemName = _g.d.ic_trans_detail_jobs._job_name;
                        _screenName = "การถัวเฉลี่ยงาน";
                    }
                    break;
            }


            this._screen._maxColumn = 1;
            this._screen._table_name = _g.d.ic_trans._table;
            this._screen._addTextBox(0, 0, 1, 1, _g.d.ic_trans_detail._item_code, 1, 255, 1, true, false, false, true, false, _g.d.ic_trans._department_code);
            this._screen._addNumberBox(1, 0, 1, 1, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);

            // grid                        
            this._grid._table_name = this._table_name;
            this._grid._total_show = true;
            this._grid._addColumn(this._columnItemCode, 1, 25, 25, true, false, true, true);
            this._grid._addColumn(this._ColumnItemName, 1, 45, 45, false, false, false);
            this._grid._addColumn(_g.d.ic_trans_detail_department._ratio, 3, 15, 15, true, false, true, false, __formatNumberAmount);
            this._grid._addColumn(_g.d.ic_trans_detail_department._amount, 3, 15, 15, true, false, true, false, __formatNumberAmount);


            this._grid._calcPersentWidthToScatter();
            this._grid._clickSearchButton -= _grid__clickSearchButton;
            this._grid._clickSearchButton += _grid__clickSearchButton;
            this._grid._alterCellUpdate += _grid__alterCellUpdate;

            this._screen._enabedControl(_g.d.ic_trans_detail._item_code, false);
            this._screen._enabedControl(_g.d.ic_trans._total_amount, false);

            this._searchMaster._dataList._gridData._mouseClick -= _gridData__mouseClick;
            this._searchMaster._dataList._gridData._mouseClick += _gridData__mouseClick;
            this._searchMaster._searchEnterKeyPress -= _searchMaster__searchEnterKeyPress;
            this._searchMaster._searchEnterKeyPress += _searchMaster__searchEnterKeyPress;
        }

        void _grid__alterCellUpdate(object sender, int row, int column)
        {
            int __columnRatio = this._grid._findColumnByName(_g.d.ic_trans_detail_department._ratio);
            int __columnAmount = this._grid._findColumnByName(_g.d.ic_trans_detail_department._amount);

            if (column == __columnRatio)
            {
                decimal __ratio = (decimal)this._grid._cellGet(row, _g.d.ic_trans_detail_department._ratio);
                decimal __amount = (this._screen._getDataNumber(_g.d.ic_trans._total_amount) / 100) * __ratio;
                this._grid._cellUpdate(row, _g.d.ic_trans_detail_department._amount, __amount, false);
            }

            this._grid.Invalidate();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchByParent(sender, e._row);
        }

        void _searchMaster__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            string __resultCode = (string)this._searchMaster._dataList._gridData._cellGet(row, 0);
            string __resultName = (string)this._searchMaster._dataList._gridData._cellGet(row, 1);
            // check dup
            Boolean __found = false;
            for (int __row = 0; __row < this._grid._rowData.Count; __row++)
            {
                if (this._grid._cellGet(__row, 0).ToString() == __resultCode)
                {
                    __found = true;
                    break;
                }
            }

            if (__found == false)
            {
                this._grid._cellUpdate(this._grid._selectRow, this._columnItemCode, __resultCode, false);
                this._grid._cellUpdate(this._grid._selectRow, this._ColumnItemName, __resultName, false);
                //switch (this._controlTypeEnum)
                //{
                //    case _g.g.ProjectAllowcateEnum.แผนก:
                //        {
                //        }
                //        break;
                //}
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("มีการเลือก") + " " + __resultCode + ":" + __resultName + " " + MyLib._myGlobal._resource("ไปแล้ว"));
            }

            this._searchMaster.Close();
            SendKeys.Send("{TAB}");
        }


        void _grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            // start search
            this._searchMaster.StartPosition = FormStartPosition.CenterParent;
            this._searchMaster.ShowDialog();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            // check total
            this._grid._calcTotal(false);
            MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)this._grid._columnList[this._grid._findColumnByName(_g.d.ic_trans_detail_department._amount)];
            decimal __getValue = __getColumn._total;

            if (this._screen._getDataNumber(_g.d.ic_trans._total_amount) == __getValue)
            {
                if (_saveData != null)
                {
                    if (_saveData(this))
                    {
                        this.Dispose();
                    }
                }
            }
            else
            {
                MessageBox.Show("ยอดเฉลี่ยไม่สมบูรณ์");
            }
        }

    }
}
