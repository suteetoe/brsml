using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace SINGHAReport
{
    public partial class _conditionForm : Form
    {
        _singhaReportEnum _mode;
        public SMLERPControl._selectWarehouseAndLocationForm _selectWarehouseAndLocation = null;

        public _conditionForm(_singhaReportEnum mode, string screenName)
        {
            InitializeComponent();

            this._mode = mode;
            if (MyLib._myGlobal._isDesignMode == false)
            {
                // this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._label.Text = screenName;
            this.Text = screenName;

            this._screen._init(this._mode);

            // screen

            // grid
            switch (this._mode)
            {
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                    {
                        this._gridCondition._table_name = _g.d.erp_branch_list._table;
                        this._gridCondition._isEdit = false;
                        this._gridCondition._addColumn("Check", 11, 1, 20, false);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._code, 1, 1, 30, true);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._name_1, 1, 1, 50, true);
                        if (MyLib._myGlobal._isDesignMode == false)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_branch_list._table);
                            this._gridCondition._loadFromDataTable(__getData.Tables[0]);
                            for (int __loop = 0; __loop < this._gridCondition._rowData.Count; __loop++)
                            {
                                this._gridCondition._cellUpdate(__loop, "check", 1, false);
                            }
                        }

                        this._selectAllToolstrip.Click += new System.EventHandler(this._selectAllToolstrip_Click);
                        this._selectNoneToolstrip.Click += new System.EventHandler(this._selectNoneToolstrip_Click);
                        this._useBranchCheckbox.CheckedChanged += _useBranchCheckbox_CheckedChanged;
                        this._selectBranchPanel.Enabled = false;
                    }
                    break;
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเงินโอน:
                    {
                        this._gridCondition._table_name = _g.d.erp_branch_list._table;
                        this._gridCondition._isEdit = false;
                        this._gridCondition._addColumn("Check", 11, 1, 20, false);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._code, 1, 1, 30, true);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._name_1, 1, 1, 50, true);
                        if (MyLib._myGlobal._isDesignMode == false)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_branch_list._table);
                            this._gridCondition._loadFromDataTable(__getData.Tables[0]);
                            for (int __loop = 0; __loop < this._gridCondition._rowData.Count; __loop++)
                            {
                                this._gridCondition._cellUpdate(__loop, "check", 1, false);
                            }
                        }

                        this._selectAllToolstrip.Click += new System.EventHandler(this._selectAllToolstrip_Click);
                        this._selectNoneToolstrip.Click += new System.EventHandler(this._selectNoneToolstrip_Click);
                        this._useBranchCheckbox.CheckedChanged += _useBranchCheckbox_CheckedChanged;
                        this._selectBranchPanel.Enabled = false;

                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3:
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53:
                    {
                        this._gridCondition._table_name = _g.d.erp_branch_list._table;
                        this._gridCondition._isEdit = false;
                        this._gridCondition._addColumn("Check", 11, 1, 20, false);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._code, 1, 1, 30, true);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._name_1, 1, 1, 50, true);
                        if (MyLib._myGlobal._isDesignMode == false)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_branch_list._table);
                            this._gridCondition._loadFromDataTable(__getData.Tables[0]);
                            for (int __loop = 0; __loop < this._gridCondition._rowData.Count; __loop++)
                            {
                                this._gridCondition._cellUpdate(__loop, "check", 1, false);
                            }
                        }

                        this._selectAllToolstrip.Click += new System.EventHandler(this._selectAllToolstrip_Click);
                        this._selectNoneToolstrip.Click += new System.EventHandler(this._selectNoneToolstrip_Click);
                        this._useBranchCheckbox.CheckedChanged += _useBranchCheckbox_CheckedChanged;
                        this._selectBranchPanel.Enabled = false;

                        // current month
                        IFormatProvider __culture = new CultureInfo("th-TH");

                        this._screen._setComboBox(_g.d.resource_report_vat._vat_month, DateTime.Now.Month - 1);
                        this._screen._setDataStr(_g.d.resource_report_vat._vat_year, DateTime.Now.ToString("yyyy", __culture));


                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                case _singhaReportEnum.GL_รายงานภาษีขาย:
                    {
                        this._gridCondition._table_name = _g.d.erp_branch_list._table;
                        this._gridCondition._isEdit = false;
                        this._gridCondition._addColumn("Check", 11, 1, 20, false);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._code, 1, 1, 30, true);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._name_1, 1, 1, 50, true);
                        if (MyLib._myGlobal._isDesignMode == false)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_branch_list._table);
                            this._gridCondition._loadFromDataTable(__getData.Tables[0]);
                            for (int __loop = 0; __loop < this._gridCondition._rowData.Count; __loop++)
                            {
                                this._gridCondition._cellUpdate(__loop, "check", 1, false);
                            }
                        }

                        this._selectAllToolstrip.Click += new System.EventHandler(this._selectAllToolstrip_Click);
                        this._selectNoneToolstrip.Click += new System.EventHandler(this._selectNoneToolstrip_Click);
                        this._useBranchCheckbox.CheckedChanged += _useBranchCheckbox_CheckedChanged;
                        this._selectBranchPanel.Enabled = false;

                        // current month
                        IFormatProvider __culture = new CultureInfo("th-TH");

                        this._screen._setComboBox(_g.d.resource_report_vat._vat_month, DateTime.Now.Month - 1);
                        this._screen._setDataStr(_g.d.resource_report_vat._vat_year, DateTime.Now.ToString("yyyy", __culture));
                    }
                    break;

                case _singhaReportEnum.การ์ดเจ้าหนี้:
                    //this._gridCondition._table_name = _g.d.resource_report._table;

                    this._gridCondition._table_name = _g.d.erp_branch_list._table;
                    this._gridCondition._isEdit = false;
                    this._gridCondition._addColumn("Check", 11, 1, 20, false);
                    this._gridCondition._addColumn(_g.d.gl_journal_book._code, 1, 1, 30, true);
                    this._gridCondition._addColumn(_g.d.gl_journal_book._name_1, 1, 1, 50, true);
                    if (MyLib._myGlobal._isDesignMode == false)
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_branch_list._table);
                        this._gridCondition._loadFromDataTable(__getData.Tables[0]);
                        for (int __loop = 0; __loop < this._gridCondition._rowData.Count; __loop++)
                        {
                            this._gridCondition._cellUpdate(__loop, "check", 1, false);
                        }
                    }

                    this._selectAllToolstrip.Click += new System.EventHandler(this._selectAllToolstrip_Click);
                    this._selectNoneToolstrip.Click += new System.EventHandler(this._selectNoneToolstrip_Click);
                    this._useBranchCheckbox.CheckedChanged += _useBranchCheckbox_CheckedChanged;
                    this._selectBranchPanel.Enabled = false;

                    // ap list


                    //this._gridCondition._addColumn(_g.d.resource_report._table + "." + _g.d.resource_report._from_ap, 1, 1, 50, true, false, false, true);
                    //this._gridCondition._addColumn(_g.d.resource_report._table + "." + _g.d.resource_report._to_ap, 1, 1, 50, true, false, false, true);
                    //this._gridCondition._clickSearchButton += _gridCondition__clickSearchButton;

                    break;
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                case _singhaReportEnum.ลูกหนี้คงค้าง:
                    {
                        this._gridCondition._table_name = _g.d.erp_branch_list._table;
                        this._gridCondition._isEdit = false;
                        this._gridCondition._addColumn("Check", 11, 1, 20, false);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._code, 1, 1, 30, true);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._name_1, 1, 1, 50, true);
                        if (MyLib._myGlobal._isDesignMode == false)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_branch_list._table);
                            this._gridCondition._loadFromDataTable(__getData.Tables[0]);
                            for (int __loop = 0; __loop < this._gridCondition._rowData.Count; __loop++)
                            {
                                this._gridCondition._cellUpdate(__loop, "check", 1, false);
                            }
                        }

                        this._selectAllToolstrip.Click += new System.EventHandler(this._selectAllToolstrip_Click);
                        this._selectNoneToolstrip.Click += new System.EventHandler(this._selectNoneToolstrip_Click);
                        this._useBranchCheckbox.Text = "แสดงเฉพาะสาขา";
                        this._useBranchCheckbox.CheckedChanged += _useBranchCheckbox_CheckedChanged;
                        this._selectBranchPanel.Enabled = false;
                    }
                    break;
                case _singhaReportEnum.GL_ข้อมูลรายวัน:
                    {

                        this._gridCondition._table_name = _g.d.erp_branch_list._table;
                        this._gridCondition._isEdit = false;
                        this._gridCondition._addColumn("Check", 11, 1, 20, false);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._code, 1, 1, 30, true);
                        this._gridCondition._addColumn(_g.d.gl_journal_book._name_1, 1, 1, 50, true);
                        if (MyLib._myGlobal._isDesignMode == false)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_branch_list._table);
                            this._gridCondition._loadFromDataTable(__getData.Tables[0]);
                            for (int __loop = 0; __loop < this._gridCondition._rowData.Count; __loop++)
                            {
                                this._gridCondition._cellUpdate(__loop, "check", 1, false);
                            }
                        }

                        this._selectAllToolstrip.Click += new System.EventHandler(this._selectAllToolstrip_Click);
                        this._selectNoneToolstrip.Click += new System.EventHandler(this._selectNoneToolstrip_Click);
                        this._useBranchCheckbox.CheckedChanged += _useBranchCheckbox_CheckedChanged;
                        this._selectBranchPanel.Enabled = false;

                    }
                    break;
                case _singhaReportEnum.รายงานการตัดเช็ค:
                case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                    {
                        this._grouper1.Visible = false;
                    }
                    break;

                case _singhaReportEnum.การ์ดสินค้า:
                    {
                        this._grouper1.Visible = false;
                        this._extendPanel.Visible = true;

                        this._selectWarehouseButton.Click += (s1, e1) =>
                        {
                            this._selectWarehouseAndLocation.ShowDialog();
                        };

                        this._selectWarehouseAndLocation = new SMLERPControl._selectWarehouseAndLocationForm(true);
                        this._selectWarehouseAndLocation._closeButton.Click += (s1, e1) =>
                        {
                            this._selectWarehouseAndLocation.Close();
                        };
                    }
                    break;
                case _singhaReportEnum.BankStatement:
                    {
                        this._useBranchPanel.Visible = false;

                        this._gridCondition._table_name = _g.d.erp_pass_book._table;
                        this._gridCondition._isEdit = false;
                        this._gridCondition._addColumn("Select", 11, 1, 5);
                        this._gridCondition._addColumn(_g.d.erp_pass_book._code, 1, 10, 10);
                        this._gridCondition._addColumn(_g.d.erp_pass_book._book_number, 1, 20, 20);
                        this._gridCondition._addColumn(_g.d.erp_pass_book._name_1, 1, 30, 30);
                        this._gridCondition._addColumn(_g.d.erp_pass_book._bank_code, 1, 20, 15);
                        this._gridCondition._addColumn(_g.d.erp_pass_book._bank_branch, 1, 20, 20);
                        this._gridCondition._addColumn("bank_name", 1, 20, 20, false, true);

                        this._gridCondition._calcPersentWidthToScatter();
                        //
                        if (MyLib._myGlobal._isDesignMode == false)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            this._gridCondition._loadFromDataTable(__myFrameWork._queryShort("select 1 as select," + MyLib._myGlobal._fieldAndComma(_g.d.erp_pass_book._code, _g.d.erp_pass_book._book_number, _g.d.erp_pass_book._name_1, _g.d.erp_pass_book._bank_code , "(select name_1 from erp_bank where erp_bank.code = erp_pass_book.bank_code) as bank_name", _g.d.erp_pass_book._bank_branch) +" from " + _g.d.erp_pass_book._table + " order by " + _g.d.erp_pass_book._code).Tables[0]);
                        }
                        this._selectAllToolstrip.Click += new System.EventHandler(this._selectAllToolstrip_Click);
                        this._selectNoneToolstrip.Click += new System.EventHandler(this._selectNoneToolstrip_Click);

                        this._myTabControl1.FixedName = true;
                        this.branch.Text = "เลือกสมุดบัญชี";
                    }
                    break;
                case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                    {
                        this._grouper1.Visible = false;
                        this._extendPanel.Visible = false;
                    }
                    break;
                case _singhaReportEnum.รายงานค่าใช้จ่ายอื่น:
                    {
                    }
                    break;
                case _singhaReportEnum.รายงานยอดขายรายเดือนตามกลุ่มสินค้า_เทียบปี:
                    {
                        this._grouper1.Visible = false;
                    }
                    break;
            }

            this.ResumeLayout(false);

        }

        MyLib._searchDataFull _search_data_full;

        private void _gridCondition__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            switch (this._mode)
            {
                case _singhaReportEnum.การ์ดเจ้าหนี้:
                    _search_data_full = new MyLib._searchDataFull();
                    this._search_data_full._name = _g.g._search_screen_ap;
                    this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._search_data_full._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full._dataList._refreshData();
                    this._search_data_full._dataList._loadViewData(0);
                    string __searchBoxLabel = MyLib._myResource._findResource(_g.d.resource_report._table + "." + _g.d.resource_report._search_ap, _g.d.resource_report._table + "." + _g.d.resource_report._search_ap)._str;
                    MyLib._myGlobal._startSearchBox(this._gridCondition._inputTextBox, __searchBoxLabel, this._search_data_full, true);

                    break;
            }

        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._search_data_full.Close();
            this._gridCondition._cellUpdate(this._gridCondition._selectRow, this._gridCondition._selectColumn, e._text, false);
        }

        private void _useBranchCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this._selectBranchPanel.Enabled = this._useBranchCheckbox.Checked;
        }

        private void _selectAllToolstrip_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._gridCondition._rowData.Count; __row++)
            {
                this._gridCondition._cellUpdate(__row, 0, 1, true);
            }
            this._gridCondition.Invalidate();
        }

        private void _selectNoneToolstrip_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._gridCondition._rowData.Count; __row++)
            {
                this._gridCondition._cellUpdate(__row, 0, 0, true);
            }
            this._gridCondition.Invalidate();
        }
    }
}
