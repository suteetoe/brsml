using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGLControl
{
    public partial class _glDetailExtra : Form
    {
        private string _accountCode;

        public _glDetailExtra(string accountCode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._accountCode = accountCode;
        }

        private void _glDetailExtra_Load(object sender, EventArgs e)
        {
            _start();
            //
            this._glDetailExtraAllocateGridData._accountCode = this._accountCode;
            this._glDetailExtraDepartmentGridData._accountCode = this._accountCode;
            this._glDetailExtraJobGridData._accountCode = this._accountCode;
            this._glDetailExtraProjectGridData._accountCode = this._accountCode;
            this._glDetailExtraSideGridData._accountCode = this._accountCode;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this._glDetailExtraAllocateGridData._isChange ||
                this._glDetailExtraDepartmentGridData._isChange ||
                this._glDetailExtraJobGridData._isChange ||
                this._glDetailExtraProjectGridData._isChange ||
                this._glDetailExtraSideGridData._isChange)
            {
                DialogResult __result = MessageBox.Show(MyLib._myGlobal._resource("มีการแก้ไขข้อมูลบางส่วน ต้องการยกเลิกจริงหรือไม่"), "Warning", MessageBoxButtons.YesNo);
                if (__result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            base.OnClosing(e);
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void _start()
        {
            this._glDetailExtraAllocateGridData.Focus();
            this._glDetailExtraDepartmentGridData.Focus();
            this._glDetailExtraJobGridData.Focus();
            this._glDetailExtraProjectGridData.Focus();
            this._glDetailExtraSideGridData.Focus();
            //
            this._glDetailExtraAllocateGridData._isChange = false;
            this._glDetailExtraDepartmentGridData._isChange = false;
            this._glDetailExtraJobGridData._isChange = false;
            this._glDetailExtraProjectGridData._isChange = false;
            this._glDetailExtraSideGridData._isChange = false;
            //
        }
    }

    public partial class _glDetailExtraTopScreen : MyLib._myScreen
    {
        public _glDetailExtraTopScreen()
        {
            this._maxColumn = 1;
            this.SuspendLayout();
            this._table_name = _g.d.gl_journal_detail._table;
            this._addTextBox(0, 0, 1, 0, _g.d.gl_journal_detail._account_code, 1, 25, 0, true, false, false);
            this._addTextBox(1, 0, 1, 0, _g.d.gl_journal_detail._account_name, 1, 25, 0, true, false, false);
            this._addNumberBox(2, 0, 1, 0, _g.d.gl_journal_detail._amount, 1, 2, true, MyLib._myGlobal._getFormatNumber("m02"));
            this.ResumeLayout();
        }
    }

    public partial class _glDetailExtraGrid : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        public string _accountCode;
        public string _fixed_table_name;
        public string _fixed_code;
        public string _fixed_name;
        public string _fixed_allocate_persent;
        public string _fixed_allocate_amount;
        public string _fixed_detail_list_table;
        public string _fixed_detail_list_code;
        public string _fixed_detail_list_account_code;
        public string _fixed_list_table;
        public string _fixed_list_code;
        public string _fixed_list_name;
        public string _fixed_list_account_code;
        public decimal _total_amount = 0;

        public _glDetailExtraGrid()
        {
            this._total_show = true;
        }

        public void _createGrid()
        {
            this._table_name = this._fixed_table_name;
            this._addColumn(this._fixed_code, 1, 10, 20, true, false, false, true);
            this._addColumn(this._fixed_name, 1, 10, 40, false, false, false, false);
            this._addColumn(this._fixed_allocate_persent, 3, 0, 15, true, false, true, false, this._formatNumber);
            this._addColumn(this._fixed_allocate_amount, 3, 0, 25, true, false, true, false, this._formatNumber);
            //
            this._clickSearchButton += new MyLib.SearchEventHandler(_glDetailExtraGrid__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_glDetailExtraGrid__alterCellUpdate);
        }

        void _glDetailExtraGrid__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._findColumnByName(this._fixed_code))
            {
                string __getCode = this._cellGet(row, 0).ToString();
                bool __found = false;
                for (int __row = 0; __row < this._rowData.Count; __row++)
                {
                    string __getCodeCompare = this._cellGet(__row, 0).ToString().ToLower();
                    if (__row != row && __getCodeCompare.Length > 0 && __getCodeCompare.Equals(__getCode.ToLower()))
                    {
                        __found = true;
                        MessageBox.Show(MyLib._myGlobal._resource("รายการนี้ใช้ไปแล้ว"));
                        this._cellUpdate(row, 0, "", false);
                        break;
                    }
                }
                if (__found == false)
                {
                    string __query = "select " + this._fixed_detail_list_code + " from " + this._fixed_detail_list_table + " where " + this._fixed_detail_list_account_code + "=\'" + this._accountCode + "\' and " + this._fixed_detail_list_code + "=\'" + __getCode + "\'";
                    DataSet __getData1 = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    try
                    {
                        string __getNameTest = __getData1.Tables[0].Rows[0].ItemArray[0].ToString();
                        //
                        __query = "select " + this._fixed_list_name + " from " + this._fixed_list_table + " where " + this._fixed_list_code + "=\'" + __getCode + "\'";
                        DataSet __getData2 = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                        bool __fail = false;
                        try
                        {
                            string __getName = __getData2.Tables[0].Rows[0].ItemArray[0].ToString();
                            this._cellUpdate(row, 1, __getName, false);
                            if (__getName.Length == 0)
                            {
                                __fail = true;
                            }
                        }
                        catch
                        {
                            __fail = true;
                        }
                        if (__fail)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบรายการที่ต้องการ"));
                            this._cellUpdate(row, 0, "", false);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("ไม่พบรายการที่ต้องการ");
                        this._cellUpdate(row, 0, "", false);
                    }
                }
            }
            else
                if (column == this._findColumnByName(this._fixed_allocate_persent))
                {
                    // Persent to amount
                    decimal __calc = this._total_amount * (((decimal)this._cellGet(this._selectRow, this._fixed_allocate_persent)) / 100M);
                    this._cellUpdate(this._selectRow, this._fixed_allocate_amount, __calc, false);
                }
                else
                    if (column == this._findColumnByName(this._fixed_allocate_amount))
                    {
                        // amount to persent
                        decimal __calc = (this._total_amount == 0) ? 0 : (((decimal)this._cellGet(this._selectRow, this._fixed_allocate_amount)) * 100M) / this._total_amount;
                        this._cellUpdate(this._selectRow, this._fixed_allocate_persent, __calc, false);
                    }
        }

        void _glDetailExtraGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._column == 0)
            {
                _glDetailExtraSelectCode __selecode = new _glDetailExtraSelectCode();
                __selecode._loadDataSet += new LoadDataEventHandler(__selecode__loadDataSet);
                __selecode._confirm += new ConfirmEventHandler(__selecode__confirm);
                __selecode.ShowDialog();
            }
        }

        void __selecode__confirm(MyLib._myGrid dataGrid)
        {
            for (int __row = 0; __row < dataGrid._rowData.Count; __row++)
            {
                if (dataGrid._cellGet(__row, 0).ToString().Equals("1"))
                {
                    string __getCode = dataGrid._cellGet(__row, 1).ToString();
                    bool __found = false;
                    for (int __find = 0; __find < this._rowData.Count; __find++)
                    {
                        if (__getCode.Equals(this._cellGet(__find, 0).ToString()))
                        {
                            __found = true;
                            break;
                        }
                    }
                    if (__found == false)
                    {
                        int __addr = -1;
                        for (int __findBlankRow = 0; __findBlankRow < this._rowData.Count; __findBlankRow++)
                        {
                            if (this._cellGet(__findBlankRow, 0).ToString().Length == 0)
                            {
                                __addr = __findBlankRow;
                                this._cellUpdate(__addr, 0, __getCode, true);
                                break;
                            }
                        }
                        if (__addr == -1)
                        {
                            __addr = this._addRow();
                            this._cellUpdate(__addr, 0, __getCode, true);
                        }
                    }
                }
            }
        }

        DataSet __selecode__loadDataSet()
        {
            StringBuilder __query = new StringBuilder();
            StringBuilder __not = new StringBuilder();
            for (int __find = 0; __find < this._rowData.Count; __find++)
            {
                string __getCode = this._cellGet(__find, 0).ToString();
                if (__getCode.Length > 0)
                {
                    if (__not.Length == 0)
                    {
                        __not.Append(" and " + this._fixed_detail_list_code + " not in (");
                    }
                    else
                    {
                        __not.Append(",");
                    }
                    __not.Append("\'" + __getCode + "\'");
                }
            }
            if (__not.Length != 0)
            {
                __not.Append(")");
            }
            __query.Append(String.Concat("select ", this._fixed_detail_list_code, " as ", _g.d.gl_resource._code, ","));
            __query.Append(String.Concat("(select ", this._fixed_list_name, " from ", this._fixed_list_table, " where ", this._fixed_list_code, "=", this._fixed_detail_list_table, ".", this._fixed_detail_list_code, ") as ", _g.d.gl_resource._name_1, ","));
            __query.Append(String.Concat("(select ", this._fixed_list_name, " from ", this._fixed_list_table, " where ", this._fixed_list_code, "=", this._fixed_detail_list_table, ".", this._fixed_detail_list_code, ") as ", _g.d.gl_resource._name_2, " "));
            __query.Append(String.Concat("from ", this._fixed_detail_list_table, " where ", this._fixed_detail_list_account_code, "=\'", this._accountCode, "\'", __not.ToString()));
            return _myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
        }

        public void _addData(int row, _glDetailExtraDetailClass data)
        {
            this._cellUpdate(row, 0, data._code, false);
            this._cellUpdate(row, 1, data._name, false);
            this._cellUpdate(row, 2, data._persent, false);
            this._cellUpdate(row, 3, data._amount, false);
        }

        public _glDetailExtraDetailClass _getData(int row)
        {
            _glDetailExtraDetailClass __result = new _glDetailExtraDetailClass();
            __result._code = (string)this._cellGet(row, this._fixed_code);
            __result._name = (string)this._cellGet(row, this._fixed_name);
            __result._persent = (decimal)this._cellGet(row, this._fixed_allocate_persent);
            __result._amount = (decimal)this._cellGet(row, this._fixed_allocate_amount);
            return __result;
        }
    }

    public partial class _glDetailExtraSideGrid : _glDetailExtraGrid
    {
        public _glDetailExtraSideGrid()
        {
            this._fixed_table_name = _g.d.gl_journal_side_list._table;
            this._fixed_code = _g.d.gl_journal_side_list._side_code;
            this._fixed_name = _g.d.gl_journal_side_list._side_name;
            this._fixed_allocate_persent = _g.d.gl_journal_side_list._allocate_persent;
            this._fixed_allocate_amount = _g.d.gl_journal_side_list._allocate_amount;
            this._fixed_detail_list_table = _g.d.gl_chart_of_account_side_list._table;
            this._fixed_detail_list_code = _g.d.gl_chart_of_account_side_list._side_code;
            this._fixed_detail_list_account_code = _g.d.gl_chart_of_account_side_list._account_code;
            this._fixed_list_table = _g.d.erp_side_list._table;
            this._fixed_list_code = _g.d.erp_side_list._code;
            this._fixed_list_name = _g.d.erp_side_list._name_1;
            //
            this._createGrid();
        }
    }

    public partial class _glDetailExtraDepartmentGrid : _glDetailExtraGrid
    {
        public _glDetailExtraDepartmentGrid()
        {
            this._fixed_table_name = _g.d.gl_journal_depart_list._table;
            this._fixed_code = _g.d.gl_journal_depart_list._department_code;
            this._fixed_name = _g.d.gl_journal_depart_list._department_name;
            this._fixed_allocate_persent = _g.d.gl_journal_depart_list._allocate_persent;
            this._fixed_allocate_amount = _g.d.gl_journal_depart_list._allocate_amount;
            this._fixed_detail_list_table = _g.d.gl_chart_of_account_depart_list._table;
            this._fixed_detail_list_code = _g.d.gl_chart_of_account_depart_list._department_code;
            this._fixed_detail_list_account_code = _g.d.gl_chart_of_account_depart_list._account_code;
            this._fixed_list_table = _g.d.erp_department_list._table;
            this._fixed_list_code = _g.d.erp_department_list._code;
            this._fixed_list_name = _g.d.erp_department_list._name_1;
            //
            this._createGrid();
        }
    }

    public partial class _glDetailExtraProjectGrid : _glDetailExtraGrid
    {
        public _glDetailExtraProjectGrid()
        {
            this._fixed_table_name = _g.d.gl_journal_project_list._table;
            this._fixed_code = _g.d.gl_journal_project_list._project_code;
            this._fixed_name = _g.d.gl_journal_project_list._project_name;
            this._fixed_allocate_persent = _g.d.gl_journal_project_list._allocate_persent;
            this._fixed_allocate_amount = _g.d.gl_journal_project_list._allocate_amount;
            this._fixed_detail_list_table = _g.d.gl_chart_of_account_project_list._table;
            this._fixed_detail_list_code = _g.d.gl_chart_of_account_project_list._project_code;
            this._fixed_detail_list_account_code = _g.d.gl_chart_of_account_project_list._account_code;
            this._fixed_list_table = _g.d.erp_project_list._table;
            this._fixed_list_code = _g.d.erp_project_list._code;
            this._fixed_list_name = _g.d.erp_project_list._name_1;
            //
            this._createGrid();
        }
    }

    public partial class _glDetailExtraAllocateGrid : _glDetailExtraGrid
    {
        public _glDetailExtraAllocateGrid()
        {
            this._fixed_table_name = _g.d.gl_journal_allocate_list._table;
            this._fixed_code = _g.d.gl_journal_allocate_list._allocate_code;
            this._fixed_name = _g.d.gl_journal_allocate_list._allocate_name;
            this._fixed_allocate_persent = _g.d.gl_journal_allocate_list._allocate_persent;
            this._fixed_allocate_amount = _g.d.gl_journal_allocate_list._allocate_amount;
            this._fixed_detail_list_table = _g.d.gl_chart_of_account_allocate_list._table;
            this._fixed_detail_list_code = _g.d.gl_chart_of_account_allocate_list._allocate_code;
            this._fixed_detail_list_account_code = _g.d.gl_chart_of_account_allocate_list._account_code;
            this._fixed_list_table = _g.d.erp_allocate_list._table;
            this._fixed_list_code = _g.d.erp_allocate_list._code;
            this._fixed_list_name = _g.d.erp_allocate_list._name_1;
            //
            this._createGrid();
        }
    }

    public partial class _glDetailExtraJobGrid : _glDetailExtraGrid
    {
        public _glDetailExtraJobGrid()
        {
            this._fixed_table_name = _g.d.gl_journal_job_list._table;
            this._fixed_code = _g.d.gl_journal_job_list._job_code;
            this._fixed_name = _g.d.gl_journal_job_list._job_name;
            this._fixed_allocate_persent = _g.d.gl_journal_job_list._allocate_persent;
            this._fixed_allocate_amount = _g.d.gl_journal_job_list._allocate_amount;
            this._fixed_detail_list_table = _g.d.gl_chart_of_account_job_list._table;
            this._fixed_detail_list_code = _g.d.gl_chart_of_account_job_list._job_code;
            this._fixed_detail_list_account_code = _g.d.gl_chart_of_account_job_list._account_code;
            this._fixed_list_table = _g.d.erp_job_list._table;
            this._fixed_list_code = _g.d.erp_job_list._code;
            this._fixed_list_name = _g.d.erp_job_list._name_1;
            //
            this._createGrid();
        }
    }

}