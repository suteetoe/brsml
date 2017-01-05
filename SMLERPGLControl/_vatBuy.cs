using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace SMLERPGLControl
{
    public partial class _vatBuy : UserControl
    {
        public delegate _vatRequestData VatRequestDataEventHandler();
        public event VatRequestDataEventHandler _vatRequest;
        //
        public DateTime _defaultDate = MyLib._myGlobal._workingDate;
        MyLib._searchDataFull _searchVatGroup;
        ArrayList _fieldVatBuyTypeName = new ArrayList();
        ArrayList _fieldBranchTypeName = new ArrayList();
        _searchVatForm _selectVat;
        string _lastCustCode = "";

        public event _getCustCodeEventHandler _getCustCode;
        public delegate string _getCustCodeEventHandler();

        public _vatBuy()
        {
            InitializeComponent();
            string formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this.SuspendLayout();
            this._vatGrid.SuspendLayout();
            this._vatGrid._table_name = _g.d.gl_journal_vat_buy._table;

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger)
            {
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ap_name, 1, 255, 10, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._tax_no, 1, 25, 10, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._branch_type, 10, 10, 10, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._branch_code, 1, 25, 10, true);

                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ref_doc_date, 4, 25, 8, false, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ref_doc_no, 1, 25, 8, false, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ref_vat_date, 4, 25, 8, false, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ref_vat_no, 1, 25, 8, false, true);

                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_date, 4, 0, 8);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_doc_no, 1, 25, 8, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_new_number, 1, 5, 8, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_effective_period, 2, 0, 5, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_effective_year, 2, 0, 5, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_description, 1, 255, 6, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_group, 1, 10, 5, true, false, true, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_base_amount, 3, 0, 6, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_rate, 3, 0, 5, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_amount, 3, 0, 6, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_total_amount, 3, 0, 6, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_except_amount_1, 3, 0, 6, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_average, 3, 0, 6, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_type, 10, 10, 6, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._is_add, 11, 10, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._manual_add, 2, 0, 5, false, true, true);

            }
            else
            {


                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ref_doc_date, 4, 25, 8, false, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ref_doc_no, 1, 25, 8, false, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ref_vat_date, 4, 25, 8, false, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ref_vat_no, 1, 25, 8, false, true);

                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_date, 4, 0, 8);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_doc_no, 1, 25, 8, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_new_number, 1, 5, 8, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_effective_period, 2, 0, 5, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_effective_year, 2, 0, 5, true, false, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_description, 1, 255, 8, true, false);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_group, 1, 10, 5, true, false, true, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_base_amount, 3, 0, 8, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_rate, 3, 0, 5, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_amount, 3, 0, 8, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_total_amount, 3, 0, 8, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_except_amount_1, 3, 0, 8, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_average, 3, 0, 8, true, false, true, false, formatNumber);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._vat_type, 10, 10, 8, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._is_add, 11, 10, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._ap_name, 1, 255, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._tax_no, 1, 25, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._branch_type, 10, 10, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._branch_code, 1, 25, 4, true);
                this._vatGrid._addColumn(_g.d.gl_journal_vat_buy._manual_add, 2, 0, 5, false, true, true);

            }
            //
            this._vatGrid._moveNextColumn += new MyLib.MoveNextColumnEventHandler(_vatGrid__moveNextColumn);
            this._vatGrid._afterAddRow += new MyLib.AfterAddRowEventHandler(_vatGrid__afterAddRow);
            this._vatGrid._clickSearchButton += new MyLib.SearchEventHandler(_vatGrid__clickSearchButton);
            this._vatGrid._lostFocusCell += new MyLib.LostFocusCellEventHandler(_vatGrid__lostFocusCell);
            this._vatGrid._focusCell += new MyLib.FocusCellEventHandler(_vatGrid__focusCell);
            this._vatGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_vatGrid__alterCellUpdate);
            this._vatGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_vatGrid__cellComboBoxItem);
            this._vatGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_vatGrid__cellComboBoxGet);
            this._vatGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_vatGrid__beforeDisplayRow);
            //
            // toe fill ap_detail
            //this._vatGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_vatGrid__queryForInsertCheck);

            for (int __loop = 0; __loop < _g.g._vatBuyType.Length; __loop++)
            {
                string __fieldName = this._vatGrid._table_name + "." + _g.g._vatBuyType[__loop];
                _fieldVatBuyTypeName.Add(MyLib._myResource._findResource(__fieldName, __fieldName)._str);
            }

            for (int __loop = 0; __loop < _g.g._ap_ar_branch_type.Length; __loop++)
            {
                string __fieldName = this._vatGrid._table_name + "." + _g.g._ap_ar_branch_type[__loop];
                _fieldBranchTypeName.Add(MyLib._myResource._findResource(__fieldName, __fieldName)._str);
            }

            this._vatGrid._calcPersentWidthToScatter();
            this._vatGrid.ResumeLayout(false);
            this._vatGrid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            this._searchVatBuyButton.Click += _searchVatBuyButton_Click;
        }

        public void _clear()
        {
            this._manualVatCheckbox.Checked = false;
        }

        //bool _vatGrid__queryForInsertCheck(MyLib._myGrid sender, int row)
        //{
        //    // check ap_name, tax_id, branch_type, branch_code

        //    return true;
        //}

        /// <summary>
        /// check ap detail
        /// </summary>
        /// <param name="_mode">0 add, 1 edit</param>
        public void _checkApDetail(int mode)
        {
            for (int __row = 0; __row < this._vatGrid._rowData.Count; __row++)
            {
                int __isManualFill = MyLib._myGlobal._intPhase(this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._manual_add).ToString());

                if (mode == 2)
                {
                    if (__isManualFill == 0)
                    {
                        string __ap_name = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._ap_name).ToString();
                        string __ap_type = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._branch_type).ToString();
                        string __branch_code = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._branch_code).ToString();
                        string __tax_no = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._tax_no).ToString();
                        if (__ap_name.Equals("") && __ap_type.Equals("0") && __branch_code.Equals("") && __tax_no.Equals(""))
                        {
                            // auto ap_detail
                            // fill empty value from ap_detail
                            string __custCode = this._getCustCode();
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            DataTable __data = __myFrameWork._queryShort("select (select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + ") as " + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier_detail._tax_id + "," + _g.d.ap_supplier_detail._branch_type + "," + _g.d.ap_supplier_detail._branch_code + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._ap_code + "=\'" + __custCode + "\'").Tables[0];
                            if (__data.Rows.Count > 0)
                            {
                                this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._ap_name, __data.Rows[0][_g.d.ap_supplier._name_1].ToString(), true);
                                this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._branch_type, MyLib._myGlobal._intPhase(__data.Rows[0][_g.d.ap_supplier_detail._branch_type].ToString()), true);
                                this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._branch_code, __data.Rows[0][_g.d.ap_supplier_detail._branch_code].ToString(), true);
                                this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._tax_no, __data.Rows[0][_g.d.ap_supplier_detail._tax_id].ToString(), true);
                            }
                        }
                        else
                        {
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._manual_add, 1, true);
                        }
                    }
                }
                else
                {

                    string __ap_name = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._ap_name).ToString();
                    string __ap_type = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._branch_type).ToString();
                    string __branch_code = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._branch_code).ToString();
                    string __tax_no = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._tax_no).ToString();

                    // กรณีเพิ่ม check ap_name, tax_id, branch_type, branch_code
                    if (__ap_name.Equals("") && __ap_type.Equals("0") && __branch_code.Equals("") && __tax_no.Equals(""))
                    {
                        // fill empty value from ap_detail
                        string __custCode = this._getCustCode();
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        DataTable __data = __myFrameWork._queryShort("select (select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + ") as " + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier_detail._tax_id + "," + _g.d.ap_supplier_detail._branch_type + "," + _g.d.ap_supplier_detail._branch_code + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._ap_code + "=\'" + __custCode + "\'").Tables[0];
                        if (__data.Rows.Count > 0)
                        {
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._ap_name, __data.Rows[0][_g.d.ap_supplier._name_1].ToString(), true);
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._branch_type, MyLib._myGlobal._intPhase(__data.Rows[0][_g.d.ap_supplier_detail._branch_type].ToString()), true);
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._branch_code, __data.Rows[0][_g.d.ap_supplier_detail._branch_code].ToString(), true);
                            this._vatGrid._cellUpdate(__row, _g.d.gl_journal_vat_buy._tax_no, __data.Rows[0][_g.d.ap_supplier_detail._tax_id].ToString(), true);
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

        void _searchVatBuyButton_Click(object sender, EventArgs e)
        {
            Boolean __reload = false;

            if (this._selectVat == null)
            {
                this._selectVat = new _searchVatForm();
                this._selectVat._processButton.Click += _selectVatProcessButton_Click;
                __reload = true;
            }

            string __getCustCode = this._getCustCode();

            if (__getCustCode.Length > 0)
            {
                if (__reload || this._lastCustCode.Equals(__getCustCode) == false)
                {
                    this._lastCustCode = __getCustCode;
                    this._selectVat._process(this._lastCustCode);
                }

                // ลบรายการที่เลือกไปแล้ว
                for (int __row = 0; __row < this._vatGrid._rowData.Count; __row++)
                {
                    string __docNo = this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._ref_vat_no).ToString();
                    int __addr = this._selectVat._resultGrid._findData(this._selectVat._resultGrid._findColumnByName(_g.d.gl_journal_vat_buy._vat_doc_no), __docNo);
                    if (__addr != -1)
                    {
                        this._selectVat._resultGrid._rowData.RemoveAt(__addr);
                    }
                }

                this._selectVat.ShowDialog();
            }
        }

        void _selectVatProcessButton_Click(object sender, EventArgs e)
        {
            this._selectVat.Close();

            try
            {
                // ลบบรรทัดที่ว่าง
                //int __rowDelete = 0;
                //while (__rowDelete < this._vatGrid._rowData.Count)
                //{
                //    if (this._vatGrid._cellGet(__rowDelete, _g.d.gl_journal_vat_buy._vat_doc_no).ToString().Trim().Length == 0)
                //    {
                //        this._vatGrid._rowData.RemoveAt(__rowDelete);
                //    }
                //    else
                //    {
                //        __rowDelete++;
                //    }
                //}

                for (int __row = 0; __row < this._selectVat._resultGrid._rowData.Count; __row++)
                {

                    if ((int)this._selectVat._resultGrid._cellGet(__row, _g.d.ap_ar_resource._select) == 1)
                    {
                        int __rowAddr = this._vatGrid._addRow();

                        this._vatGrid._cellUpdate(__rowAddr, _g.d.gl_journal_vat_buy._ref_doc_no, this._selectVat._resultGrid._cellGet(__row, _g.d.gl_journal_vat_buy._doc_no).ToString(), true);
                        this._vatGrid._cellUpdate(__rowAddr, _g.d.gl_journal_vat_buy._ref_vat_no, this._selectVat._resultGrid._cellGet(__row, _g.d.gl_journal_vat_buy._vat_doc_no).ToString(), true);

                        this._vatGrid._cellUpdate(__rowAddr, _g.d.gl_journal_vat_buy._ref_doc_date, MyLib._myGlobal._convertDateFromQuery(this._selectVat._resultGrid._cellGet(__row, _g.d.gl_journal_vat_buy._doc_date).ToString()), true);
                        this._vatGrid._cellUpdate(__rowAddr, _g.d.gl_journal_vat_buy._ref_vat_date, MyLib._myGlobal._convertDateFromQuery(this._selectVat._resultGrid._cellGet(__row, _g.d.gl_journal_vat_buy._vat_date).ToString()), true);

                        // ยอดภาษี
                        this._vatGrid._cellUpdate(__rowAddr, _g.d.gl_journal_vat_buy._vat_base_amount, MyLib._myGlobal._decimalPhase(this._selectVat._resultGrid._cellGet(__row, _g.d.gl_journal_vat_buy._vat_base_amount).ToString()), true);
                        this._vatGrid._cellUpdate(__rowAddr, _g.d.gl_journal_vat_buy._vat_rate, MyLib._myGlobal._decimalPhase(this._selectVat._resultGrid._cellGet(__row, _g.d.gl_journal_vat_buy._vat_rate).ToString()), true);
                        this._vatGrid._cellUpdate(__rowAddr, _g.d.gl_journal_vat_buy._vat_total_amount, MyLib._myGlobal._decimalPhase(this._selectVat._resultGrid._cellGet(__row, _g.d.gl_journal_vat_buy._vat_total_amount).ToString()), true);
                        this._vatGrid._cellUpdate(__rowAddr, _g.d.gl_journal_vat_buy._vat_amount, MyLib._myGlobal._decimalPhase(this._selectVat._resultGrid._cellGet(__row, _g.d.gl_journal_vat_buy._vat_amount).ToString()), true);

                    }
                }
                this._vatGrid.Validate();
                this._checkHideGridVatRefColumn();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void _checkHideGridVatRefColumn()
        {
            bool __found = false;

            for (int __row = 0; __row < this._vatGrid._rowData.Count; __row++)
            {
                if (this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._ref_vat_no).ToString().Length > 0)
                {
                    __found = true;
                    break;
                }
            }

            int __vatRefNoColumnNumber = this._vatGrid._findColumnByName(_g.d.gl_journal_vat_buy._ref_vat_no);
            int __vatRefDateColumnNumber = this._vatGrid._findColumnByName(_g.d.gl_journal_vat_buy._ref_vat_date);


            MyLib._myGrid._columnType __vatRefNoColumn = (MyLib._myGrid._columnType)this._vatGrid._columnList[__vatRefNoColumnNumber];
            __vatRefNoColumn._isHide = !__found;
            __vatRefNoColumn._widthPercent = (__found) ? 10 : 0;
            this._vatGrid._columnList[__vatRefNoColumnNumber] = (MyLib._myGrid._columnType)__vatRefNoColumn;

            MyLib._myGrid._columnType __vatRefDateColumn = (MyLib._myGrid._columnType)this._vatGrid._columnList[__vatRefDateColumnNumber];
            __vatRefDateColumn._isHide = !__found;
            __vatRefDateColumn._widthPercent = (__found) ? 10 : 0;
            this._vatGrid._columnList[__vatRefDateColumnNumber] = (MyLib._myGrid._columnType)__vatRefDateColumn;

            this._vatGrid._calcPersentWidthToScatter();
            this._vatGrid._recalcColumnWidth(true);
            this._vatGrid.Invalidate();

        }

        MyLib.BeforeDisplayRowReturn _vatGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            // ตรวจยอดภาษี ถ้าไม่ตรงเปลี่ยนเป็นสีแดง
            decimal __getBaseAmount = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_base_amount);
            decimal __getVatRate = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_rate);
            decimal __getVatAmount = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_amount);
            decimal __getVatExceptAmount = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_except_amount_1);
            if (__getVatAmount != MyLib._myGlobal._round((__getBaseAmount * __getVatRate / 100.0M), 2))
            {
                senderRow.newColor = Color.Red;
            }
            return (senderRow);
        }

        string _vatGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            string __result = "";
            if (columnName.Equals(_g.d.gl_journal_vat_buy._vat_type))
            {
                __result = _fieldVatBuyTypeName[select].ToString();
            }
            else if (columnName.Equals(_g.d.gl_journal_vat_buy._branch_type))
            {
                __result = _fieldBranchTypeName[select].ToString();
            }
            return __result;
        }

        object[] _vatGrid__cellComboBoxItem(object sender, int row, int column)
        {

            if (column == this._vatGrid._findColumnByName(_g.d.gl_journal_vat_buy._vat_type))
            {
                return (object[])_fieldVatBuyTypeName.ToArray();
            }
            else if (column == this._vatGrid._findColumnByName(_g.d.gl_journal_vat_buy._branch_type))
            {
                return (object[])_fieldBranchTypeName.ToArray();
            }

            return null;
        }

        /// <summary>
        /// ลบบรรทัดที่ไม่ได้ป้อนเลขที่ใบกำกับ
        /// </summary>
        public void _deleteRowIfBlank()
        {
            int __row = 0;
            while (__row < this._vatGrid._rowData.Count)
            {
                if (this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._vat_doc_no).ToString().Trim().Length == 0)
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
            if (this._vatGrid._rowData.Count == 0)
            {
                this._vatGrid._addRow();
            }

            if (this._manualVatCheckbox.Checked == false)
            {
                this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_buy._vat_date, source._vatDate, true);
                this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_buy._vat_doc_no, source._vatDocNo, false);
                this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_buy._vat_rate, source._vatRate, false);
                // นับดูว่าใช้ไปกี่บรรทัด
                int __count = 0;
                for (int __row = 0; __row < this._vatGrid._rowData.Count; __row++)
                {
                    if (this._vatGrid._cellGet(__row, _g.d.gl_journal_vat_buy._vat_doc_no).ToString().Trim().Length > 0)
                    {
                        __count++;
                    }
                }
                if (__count == 1)
                {
                    // กรณีมีบรรทัดเดียว ให้ดึงค่าต่างๆ ลงมาใส่โดยอัตโนมัติ
                    this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_buy._vat_base_amount, source._vatBaseAmount, false);
                    this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_buy._vat_amount, source._vatAmount, false);
                    this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_buy._vat_total_amount, source._vatTotalAmount, false);
                    this._vatGrid._cellUpdate(0, _g.d.gl_journal_vat_buy._vat_except_amount_1, source._vatExceptAmount, false);
                }
            }
            this._vatGrid.Invalidate();
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
            this._vatGrid._cellUpdate(row, "row_number", row, false);
            this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._line_number, row, false);
            string __columnName = ((MyLib._myGrid._columnType)__getGrid._columnList[column])._originalName;
            //
            if (__columnName.Equals(_g.d.gl_journal_vat_buy._vat_group))
            {
                _g.g._glGetTaxGroupType getTaxGroup = _g.g._searchTaxGroup(__getGrid._cellGet(__getGrid._selectRow, column).ToString());
                if (getTaxGroup._name.Length == 0)
                {
                    this._vatGrid._cellUpdate(row, this._vatGrid._selectColumn, "", false);
                }
                this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_rate, getTaxGroup._tax_rate, false);
            }
            decimal __getVatRate = (decimal)MyLib._myGlobal._decimalPhase(this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_rate).ToString());
            decimal __getBaseAmount = (decimal)MyLib._myGlobal._decimalPhase(this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_base_amount).ToString());
            decimal __getVatAmount = (decimal)MyLib._myGlobal._decimalPhase(this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_amount).ToString());
            decimal __getVatTotalAmount = (decimal)MyLib._myGlobal._decimalPhase(this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_total_amount).ToString());
            decimal __getVatExceptAmount = (decimal)MyLib._myGlobal._decimalPhase(this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_except_amount_1).ToString());
            decimal __getVatAverage = (decimal)MyLib._myGlobal._decimalPhase(this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_average).ToString());
            if (this._autoCalcVatCheckBox.Checked)
            {
                if (__columnName.Equals(_g.d.gl_journal_vat_buy._vat_date))
                {
                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_effective_period, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_date)).Month, false);
                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_effective_year, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_date)).Year + MyLib._myGlobal._year_add, false);
                }
                else
                    if (__columnName.Equals(_g.d.gl_journal_vat_buy._vat_rate))
                    {
                        __getVatAmount = __getBaseAmount + MyLib._myGlobal._round(__getBaseAmount * (__getVatRate / 100.0M), 2);
                        __getVatTotalAmount = __getBaseAmount + __getVatAmount;
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_amount, __getVatAmount, false);
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_total_amount, __getVatTotalAmount, false);
                        if (__getVatRate < 0 || __getVatRate > 15)
                        {
                            MessageBox.Show((MyLib._myGlobal._language == 0) ? "อัตราภาษีไม่เหมาะสม" : "tax Rate error");
                        }
                    }
                    else
                        if (__columnName.Equals(_g.d.gl_journal_vat_buy._vat_base_amount))
                        {
                            __getVatAmount = MyLib._myGlobal._round(__getBaseAmount * (__getVatRate / 100.0M), 2);
                            __getVatTotalAmount = __getBaseAmount + __getVatAmount;
                            this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_amount, __getVatAmount, false);
                            this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_total_amount, __getVatTotalAmount, false);
                        }
                        else
                            if (__columnName.Equals(_g.d.gl_journal_vat_buy._vat_amount))
                            {
                                __getVatTotalAmount = __getBaseAmount + __getVatAmount;
                                this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_total_amount, __getVatTotalAmount, false);
                            }
                            else
                                if (__columnName.Equals(_g.d.gl_journal_vat_buy._vat_total_amount))
                                {
                                    __getVatAmount = MyLib._myGlobal._round((__getVatTotalAmount * __getVatRate) / (100.0M + __getVatRate), 2);
                                    __getBaseAmount = __getVatTotalAmount - __getVatAmount;
                                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_amount, __getVatAmount, false);
                                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_base_amount, __getBaseAmount, false);
                                }
                                else
                                    if (__columnName.Equals(_g.d.gl_journal_vat_buy._vat_except_amount_1))
                                    {
                                        if (__getVatExceptAmount > __getVatAmount)
                                        {
                                            MessageBox.Show((MyLib._myGlobal._language == 0) ? "ยอดไม่เหมาะสม" : "tax Rate error");
                                        }
                                    }
            }
        }

        void _vatGrid__focusCell(object sender, int row, int column, string columnName)
        {
            if (columnName.Equals(_g.d.gl_journal_vat_buy._vat_group))
            {
                //_searchVatGroupPopUp(columnName, false);
            }
        }

        void _vatGrid__lostFocusCell(object sender, int row, int column, string columnName)
        {
            if (_searchVatGroup != null)
            {
                _searchVatGroup.Visible = false;
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
            _vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_group, __result, true);
        }

        void _vatGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.CompareTo(_g.d.gl_journal_vat_buy._vat_group) == 0)
            {
                _searchVatGroupPopUp(e._columnName, true);
                _searchVatGroup._firstFocus();
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            _searchVatGroup.Close();
            this._vatGrid._inputTextBox.textBox.Text = e._text;
            _vatGrid._cellUpdate(_vatGrid.SelectRow, _g.d.gl_journal_vat_buy._vat_group, e._text, true);
        }

        void _vatGrid__afterAddRow(object sender, int row)
        {
            _autoInput(row, _g.d.gl_journal_vat_buy._vat_date);
            _autoInput(row, _g.d.gl_journal_vat_buy._vat_doc_no);
            _autoInput(row, _g.d.gl_journal_vat_buy._vat_new_number);
            _autoInput(row, _g.d.gl_journal_vat_buy._vat_group);
            _autoInput(row, _g.d.gl_journal_vat_buy._vat_description);
        }

        void _autoInput(int row, string columnName)
        {
            DateTime __getDate = (DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_date);
            string __getDocNo = this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_new_number).ToString();
            string __getVatGroup = this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_group).ToString();
            if (_autoInputCheckBox.Checked)
            {
                if (columnName.CompareTo(_g.d.gl_journal_vat_buy._vat_date) == 0)
                {
                    if (__getDate.Year == 1000)
                    {
                        if (row == 0)
                        {
                            this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_date, _defaultDate, false);
                        }
                        else
                        {
                            DateTime __getDatePrev = (DateTime)this._vatGrid._cellGet(row - 1, _g.d.gl_journal_vat_buy._vat_date);
                            this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_date, __getDatePrev, false);
                        }
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_effective_period, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_date)).Month, false);
                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_effective_year, ((DateTime)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_date)).Year + MyLib._myGlobal._year_add, false);
                    }
                }
                else
                    if (columnName.CompareTo(_g.d.gl_journal_vat_buy._vat_doc_no) == 0)
                    {
                        if (__getDocNo.Length == 0)
                        {
                            if (row != 0)
                            {
                                string getString = this._vatGrid._cellGet(row - 1, _g.d.gl_journal_vat_buy._vat_doc_no).ToString();
                                if (this._autoInputCheckBox.Checked)
                                {
                                    getString = MyLib._myGlobal._autoRunningNumberStyleRightToLeft(getString);
                                }
                                this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_doc_no, getString, false);
                            }
                        }
                    }
                    else
                        if (columnName.CompareTo(_g.d.gl_journal_vat_buy._vat_new_number) == 0)
                        {
                            if (__getDocNo.Length == 0)
                            {
                                if (row != 0)
                                {
                                    string getString = this._vatGrid._cellGet(row - 1, _g.d.gl_journal_vat_buy._vat_new_number).ToString();
                                    if (this._autoInputCheckBox.Checked)
                                    {
                                        getString = MyLib._myGlobal._autoRunningNumberStyleRightToLeft(getString);
                                    }
                                    this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_new_number, getString, false);
                                }
                            }
                        }
                        else
                            if (columnName.CompareTo(_g.d.gl_journal_vat_buy._vat_group) == 0)
                            {
                                if (__getVatGroup.Length == 0)
                                {
                                    if (row != 0)
                                    {
                                        string getString = this._vatGrid._cellGet(row - 1, _g.d.gl_journal_vat_buy._vat_group).ToString();
                                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_group, getString, false);
                                    }
                                    //
                                    decimal getVatRate = (decimal)this._vatGrid._cellGet(row, _g.d.gl_journal_vat_buy._vat_rate);
                                    if (getVatRate == 0)
                                    {
                                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_rate, _g.g._default_vat_rate, false);
                                    }
                                }
                            }
                            else
                                if (columnName.CompareTo(_g.d.gl_journal_vat_buy._vat_description) == 0)
                                {
                                    if (row != 0)
                                    {
                                        string getString = this._vatGrid._cellGet(row - 1, _g.d.gl_journal_vat_buy._vat_description).ToString();
                                        this._vatGrid._cellUpdate(row, _g.d.gl_journal_vat_buy._vat_description, getString, false);
                                    }
                                    //
                                }
            }
        }

        MyLib._myGridMoveColumnType _vatGrid__moveNextColumn(MyLib._myGrid sender, int newRow, int newColumn)
        {
            MyLib._myGridMoveColumnType __result = new MyLib._myGridMoveColumnType();
            if (((DateTime)_vatGrid._cellGet(sender._selectRow, _g.d.gl_journal_vat_buy._vat_date)).Year == 1000)
            {
                newColumn = _vatGrid._findColumnByName(_g.d.gl_journal_vat_buy._vat_date);
            }
            __result._newColumn = newColumn;
            __result._newRow = newRow;
            return (__result);
        }

        private void _receiveVatGrid_Load(object sender, EventArgs e)
        {

        }
    }
}
