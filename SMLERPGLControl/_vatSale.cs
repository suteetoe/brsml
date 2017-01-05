using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPGLControl
{
    public partial class _vatSale : UserControl
    {
        public delegate _vatRequestData VatRequestDataEventHandler();
        public event VatRequestDataEventHandler _vatRequest;
        //
        public string _defaultDate = "";
        public string _defaultDocNo = "";

        public event _getCustCodeEventHandler _getCustCode;
        public delegate string _getCustCodeEventHandler();

        MyLib._searchDataFull _searchVatGroup;
        ArrayList _fieldBranchTypeName = new ArrayList();
        /// <summary>
        /// toe ทำมารับสิงห์กรณี update จาก ที่อยู่จัดส่ง
        /// </summary>
        public bool _manualTaxID = false;

        public _vatSale()
        {
            InitializeComponent();
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._vatGrid._table_name = _g.d.gl_journal_vat_sale._table;

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
            {
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._ar_name, 1, 255, 10, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._tax_no, 1, 25, 10, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._branch_type, 10, 10, 10, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._branch_code, 1, 25, 10, true);

                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._vat_date, 4, 0, 10);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._vat_number, 1, 25, 15, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._vat_effective_period, 2, 0, 5, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._vat_effective_year, 2, 0, 5, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._description, 1, 255, 10, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._tax_group, 1, 10, 6, true, false, true, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._base_caltax_amount, 3, 0, 6, true, false, true, false, __formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._tax_rate, 3, 0, 6, true, false, true, false, __formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._amount, 3, 0, 6, true, false, true, false, __formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._except_tax_amount, 3, 0, 10, true, false, true, false, __formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._is_add, 11, 10, 6, true);

                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._manual_add, 2, 0, 5, false, true, true);

            }
            else
            {
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._vat_date, 4, 0, 10);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._vat_number, 1, 25, 15, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._vat_effective_period, 2, 0, 5, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._vat_effective_year, 2, 0, 5, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._description, 1, 255, 20, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._tax_group, 1, 10, 10, true, false, true, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._base_caltax_amount, 3, 0, 10, true, false, true, false, __formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._tax_rate, 3, 0, 10, true, false, true, false, __formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._amount, 3, 0, 10, true, false, true, false, __formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._except_tax_amount, 3, 0, 10, true, false, true, false, __formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._is_add, 11, 10, 6, true);

                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._ar_name, 1, 255, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._tax_no, 1, 25, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._branch_type, 10, 10, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._branch_code, 1, 25, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_sale._manual_add, 2, 0, 5, false, true, true);
            }
            for (int __loop = 0; __loop < _g.g._ap_ar_branch_type.Length; __loop++)
            {
                string __fieldName = this._vatGrid._table_name + "." + _g.g._ap_ar_branch_type[__loop];
                _fieldBranchTypeName.Add(MyLib._myResource._findResource(__fieldName, __fieldName)._str);
            }

            //
            this._vatGrid._clickSearchButton += new MyLib.SearchEventHandler(_vatGrid__clickSearchButton);
            this._vatGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_vatGrid__alterCellUpdate);
            this._vatGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_vatGrid__beforeDisplayRow);
            this._vatGrid._afterAddRow += new MyLib.AfterAddRowEventHandler(_vatGrid__afterAddRow);
            this._vatGrid._focusCell += new MyLib.FocusCellEventHandler(_vatGrid__focusCell);
            this._vatGrid._lostFocusCell += new MyLib.LostFocusCellEventHandler(_vatGrid__lostFocusCell);
            this._vatGrid._moveNextColumn += new MyLib.MoveNextColumnEventHandler(_vatGrid__moveNextColumn);

            this._vatGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_vatGrid__cellComboBoxItem);
            this._vatGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_vatGrid__cellComboBoxGet);

            //
            this._vatGrid._calcPersentWidthToScatter();
        }

        string _vatGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            string __result = "";
            if (columnName.Equals(_g.d.gl_journal_vat_sale._branch_type))
            {
                __result = _fieldBranchTypeName[select].ToString();
            }
            return __result;
        }

        object[] _vatGrid__cellComboBoxItem(object sender, int row, int column)
        {

            if (column == this._vatGrid._findColumnByName(_g.d.gl_journal_vat_sale._branch_type))
            {
                return (object[])_fieldBranchTypeName.ToArray();
            }

            return null;
        }


        /// <summary>
        /// check ar detail
        /// </summary>
        /// <param name="_mode">0 add, 1 edit</param>
        public void _checkArDetail(int mode)
        {
            for (int __row = 0; __row < this._vatGrid._rowData.Count; __row++)
            {
                int __isManualFill = MyLib._myGlobal._intPhase(this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._manual_add).ToString());

                if (mode == 2)
                {
                    if (__isManualFill == 0)
                    {
                        // auto ap_detail
                        // fill empty value from ap_detail
                        string __custCode = this._getCustCode();
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        DataTable __data = __myFrameWork._queryShort("select (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + ") as " + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer_detail._tax_id + "," + _g.d.ar_customer_detail._branch_type + "," + _g.d.ar_customer_detail._branch_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + "=\'" + __custCode + "\'").Tables[0];

                        if (__data.Rows.Count > 0)
                        {
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._ar_name, __data.Rows[0][_g.d.ar_customer._name_1].ToString(), true);
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._branch_type, MyLib._myGlobal._intPhase(__data.Rows[0][_g.d.ar_customer_detail._branch_type].ToString()), true);
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._branch_code, __data.Rows[0][_g.d.ar_customer_detail._branch_code].ToString(), true);
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._tax_no, __data.Rows[0][_g.d.ar_customer_detail._tax_id].ToString(), true);
                        }

                    }
                }
                else
                {

                    string __ap_name = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_sale._ar_name).ToString();
                    string __ap_type = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_sale._branch_type).ToString();
                    string __branch_code = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_sale._branch_code).ToString();
                    string __tax_no = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_sale._tax_no).ToString();

                    // กรณีเพิ่ม check ap_name, tax_id, branch_type, branch_code
                    if (__ap_name.Equals("") && __ap_type.Equals("0") && __branch_code.Equals("") && __tax_no.Equals(""))
                    {
                        // fill empty value from ap_detail
                        string __custCode = this._getCustCode();
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        DataTable __data = __myFrameWork._queryShort("select (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + ") as " + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer_detail._tax_id + "," + _g.d.ar_customer_detail._branch_type + "," + _g.d.ar_customer_detail._branch_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + "=\'" + __custCode + "\'").Tables[0];
                        if (__data.Rows.Count > 0)
                        {
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._ar_name, __data.Rows[0][_g.d.ar_customer._name_1].ToString(), true);
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._branch_type, MyLib._myGlobal._intPhase(__data.Rows[0][_g.d.ar_customer_detail._branch_type].ToString()), true);
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._branch_code, __data.Rows[0][_g.d.ar_customer_detail._branch_code].ToString(), true);
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_sale._tax_no, __data.Rows[0][_g.d.ar_customer_detail._tax_id].ToString(), true);
                        }
                    }
                    else
                    {
                        // manual add
                        this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._manual_add, 1, true);
                    }
                }
            }
        }


        /// <summary>
        /// ลบบรรทัดที่ไม่ได้ป้อนเลขที่ใบกำกับ
        /// </summary>
        public void _deleteRowIfBlank()
        {
            int __row = 0;
            while (__row < this._vatGrid._rowData.Count)
            {
                if (this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_sale._vat_number).ToString().Trim().Length == 0)
                {
                    this._vatGrid._rowData.RemoveAt(__row);
                }
                else
                {
                    __row++;
                }
            }
        }

        public void _updateFirstRow(_vatRequestData source)
        {
            if (source._vatDate.Year > 1900)
            {
                if (this._vatGrid._rowData.Count == 0)
                {
                    this._vatGrid._addRow();
                }
                this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._vat_date, source._vatDate, true);
                this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._vat_number, source._vatDocNo, false);
                this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._tax_rate, source._vatRate, false);
                // นับดูว่าใช้ไปกี่บรรทัด
                int __count = 0;
                for (int __row = 0; __row < this._vatGrid._rowData.Count; __row++)
                {
                    if (this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_sale._vat_number).ToString().Trim().Length > 0)
                    {
                        __count++;
                    }
                }
                if (__count == 1)
                {
                    // กรณีมีบรรทัดเดียว ให้ดึงค่าต่างๆ ลงมาใส่โดยอัตโนมัติ
                    this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._base_caltax_amount, source._vatBaseAmount, false);
                    this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._amount, source._vatAmount, false);
                    this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_sale._except_tax_amount, source._vatExceptAmount, false);
                }
            }
            else
            {
                this._vatGrid._clear();
            }
            this._vatGrid.Invalidate();
        }

        MyLib._myGridMoveColumnType _vatGrid__moveNextColumn(MyLib._myGrid sender, int newRow, int newColumn)
        {
            MyLib._myGridMoveColumnType __result = new MyLib._myGridMoveColumnType();
            if (((DateTime)_vatGrid._cellGet(sender._selectRow, _g.d.gl_journal_vat_sale._vat_date)).Year == 1000)
            {
                newColumn = _vatGrid._findColumnByName(_g.d.gl_journal_vat_sale._vat_date);
            }
            else
                if (this._vatGrid._selectColumn == this._vatGrid._findColumnByName(_g.d.gl_journal_vat_sale._vat_number) && _vatGrid._cellGet(sender._selectRow, _g.d.gl_journal_vat_sale._vat_number).ToString().Length == 0)
            {
                MessageBox.Show((MyLib._myGlobal._language == 0) ? "กรุณาบันทึกเลขที่ใบกำกับภาษีก่อน" : "Please enter tax number");
                newColumn = _vatGrid._findColumnByName(_g.d.gl_journal_vat_sale._vat_number);
            }
            __result._newColumn = newColumn;
            __result._newRow = newRow;
            return (__result);
        }

        void _vatGrid__lostFocusCell(object sender, int row, int column, string columnName)
        {
            if (_searchVatGroup != null)
            {
                _searchVatGroup.Visible = false;
            }
        }

        void _vatGrid__focusCell(object sender, int row, int column, string columnName)
        {
            if (columnName.Equals(_g.d.gl_journal_vat_sale._tax_group))
            {
                _searchVatGroupPopUp(columnName, false);
            }
        }

        void _searchVatGroupPopUp(string columnName, bool showSearchBox)
        {
            if (_searchVatGroup == null)
            {
                _searchVatGroup = new MyLib._searchDataFull();
                _searchVatGroup._name = _g.g._search_screen_gl_tax_group;
                _searchVatGroup._dataList._loadViewFormat(_searchVatGroup._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _searchVatGroup._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _searchVatGroup._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchVatGroup__searchEnterKeyPress);
                _searchVatGroup._dataList._refreshData();
            }
            _searchVatGroup._dataList._searchPanel.Visible = showSearchBox;
            _searchVatGroup.Text = columnName;
            MyLib._myGlobal._startSearchBox(this._vatGrid._inputTextBox, columnName, _searchVatGroup, false);
            if (showSearchBox)
            {
                _searchVatGroup._dataList._recalcPosition();
            }
            else
            {
                this._vatGrid._inputTextBox.textBox.Focus();
            }
        }

        void _searchVatGroup__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            string __result = (string)_searchVatGroup._dataList._gridData._cellGet(row, _g.d.gl_tax_group._table + "." + _g.d.gl_tax_group._code);
            _searchVatGroup.Close();
            this._vatGrid._inputTextBox.textBox.Text = __result;
            _vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._tax_group, __result, true);
        }

        void _vatGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.CompareTo(_g.d.gl_journal_vat_sale._tax_group) == 0)
            {
                _searchVatGroupPopUp(e._columnName, true);
                _searchVatGroup._firstFocus();
            }
        }

        void _vatGrid__alterCellUpdate(object sender, int row, int column)
        {
            MyLib._myGrid __getGrid = (MyLib._myGrid)sender;
            //
            if (this._vatRequest != null)
            {
                this._updateFirstRow(this._vatRequest());
            }
            //
            // if (__getGrid._rowData.Count > 0)
            {



                string __columnName = ((MyLib._myGrid._columnType)__getGrid._columnList[column])._originalName;

                decimal __getBaseAmount = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._base_caltax_amount);
                decimal __getVatRate = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._tax_rate);
                decimal __getVatAmount = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._amount);
                decimal __getVatExceptAmount = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._except_tax_amount);

                Boolean __calcVatAmountEvent = false;

                if (__columnName.Equals(_g.d.gl_journal_vat_buy._vat_date))
                {
                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_effective_period, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._vat_date)).Month, false);
                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_effective_year, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._vat_date)).Year + MyLib._myGlobal._year_add, false);
                }
                else
                    if (__columnName.CompareTo(_g.d.gl_journal_vat_sale._tax_group) == 0)
                {
                    _g.g._glGetTaxGroupType getTaxGroup = _g.g._searchTaxGroup(__getGrid._cellGet(__getGrid._selectRow, column).ToString());
                    if (getTaxGroup._name.Length == 0)
                    {
                        this._vatGrid._cellUpdate(row, this._vatGrid._selectColumn, "", false);
                    }
                    else
                    {
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._tax_rate, getTaxGroup._tax_rate, false);
                        __getVatRate = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._tax_rate);
                        __calcVatAmountEvent = true;
                    }
                }
                else
                        if (__columnName.CompareTo(_g.d.gl_journal_vat_sale._base_caltax_amount) == 0)
                {
                    __calcVatAmountEvent = true;
                }
                else
                            if (__columnName.CompareTo(_g.d.gl_journal_vat_sale._tax_rate) == 0)
                {
                    __calcVatAmountEvent = true;
                }

                if (__calcVatAmountEvent && _autoCalcVatCheckBox.Checked)
                {
                    if (__columnName.Equals(_g.d.gl_journal_vat_sale._vat_date))
                    {
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_effective_period, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._vat_date)).Month, false);
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_effective_year, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._vat_date)).Year + MyLib._myGlobal._year_add, false);
                    }
                    decimal calcVatAmount = (__getBaseAmount * __getVatRate) / 100.0M;
                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._amount, calcVatAmount, false);
                }
            }
        }

        void _autoInput(int row, string columnName)
        {
            if (_autoInputCheckBox.Checked)
            {
                if (columnName.CompareTo(_g.d.gl_journal_vat_sale._vat_date) == 0)
                {
                    if (row == 0)
                    {
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_date, _defaultDate, false);
                    }
                    else
                    {
                        DateTime __getDate = (DateTime)this._vatGrid._cellGet(row - 1, _g.d.gl_journal_vat_sale._vat_date);
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_date, __getDate, false);
                    }
                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_effective_period, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._vat_date)).Month, false);
                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_effective_year, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._vat_date)).Year + MyLib._myGlobal._year_add, false);
                }
                else
                    if (columnName.CompareTo(_g.d.gl_journal_vat_sale._vat_number) == 0)
                {
                    if (row == 0)
                    {
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_number, _defaultDocNo, false);
                    }
                    else
                    {
                        string getString = this._vatGrid._cellGet(row - 1, _g.d.gl_journal_vat_sale._vat_number).ToString();
                        if (_autoNumberCheckBox.Checked)
                        {
                            getString = MyLib._myGlobal._autoRunningNumberStyleRightToLeft(getString);
                        }
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._vat_number, getString, false);
                    }
                }
                else
                        if (columnName.CompareTo(_g.d.gl_journal_vat_sale._tax_group) == 0)
                {
                    string __getVatGroup = this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._tax_group).ToString();
                    if (__getVatGroup.Length == 0)
                    {
                        if (row != 0)
                        {
                            string getString = this._vatGrid._cellGet(row - 1, _g.d.gl_journal_vat_sale._tax_group).ToString();
                            this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._tax_group, getString, false);
                        }
                        //
                        decimal getVatRate = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._tax_rate);
                        if (getVatRate == 0)
                        {
                            this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._tax_rate, _g.g._default_vat_rate, false);
                        }
                    }
                }
                else
                            if (columnName.CompareTo(_g.d.gl_journal_vat_sale._description) == 0)
                {
                    if (row != 0)
                    {
                        string getString = this._vatGrid._cellGet(row - 1, _g.d.gl_journal_vat_sale._description).ToString();
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_sale._description, getString, false);
                    }
                }
            }
        }

        void _vatGrid__afterAddRow(object sender, int row)
        {
            _autoInput(row, _g.d.gl_journal_vat_sale._vat_date);
            _autoInput(row, _g.d.gl_journal_vat_sale._vat_number);
            _autoInput(row, _g.d.gl_journal_vat_sale._tax_group);
            _autoInput(row, _g.d.gl_journal_vat_sale._description);
        }

        MyLib.BeforeDisplayRowReturn _vatGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            // ตรวจยอดภาษี ถ้าไม่ตรงเปลี่ยนเป็นสีแดง
            decimal __getBaseAmount = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._base_caltax_amount);
            decimal __getVatRate = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._tax_rate);
            decimal __getVatAmount = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._amount);
            decimal __getVatExceptAmount = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_sale._except_tax_amount);
            if (__getVatAmount != MyLib._myGlobal._round((__getBaseAmount * __getVatRate / 100.0M), 2))
            {
                senderRow.newColor = Color.Red;
            }
            return (senderRow);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (_searchVatGroup != null)
                _searchVatGroup.Close();
            _vatGrid._cellUpdate(e._row, _g.d.gl_journal_vat_sale._tax_group, e._text, true);
        }

        private void _vatGrid_Load(object sender, EventArgs e)
        {

        }
    }
}
