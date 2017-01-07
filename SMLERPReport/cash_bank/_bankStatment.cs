using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;

namespace SMLERPReport.cash_bank
{
    public partial class _bankStatment : UserControl
    {
        private MyLib._myForm _conditionForm = new MyLib._myForm();
        private MyLib._myForm _resultForm = new MyLib._myForm();
        private MyLib._myForm _bookSelectForm = new MyLib._myForm();
        private MyLib._myScreen _condition = new MyLib._myScreen();
        private SMLReport._generate _report = new SMLReport._generate("", false);
        private MyLib._myGrid _bookSelectGrid = new MyLib._myGrid();
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private List<string> _bookCodeList;
        private DataTable _dataTableBalance;
        private DataTable _dataTableData;
        private string _dateBegin;
        private string _dateEnd;
        private StringBuilder _bookCode;
        string _levelNameMain = "cust";
        string _levelNameDoc = "doc";

        public _bankStatment()
        {
            InitializeComponent();
            // เงื่อนไข
            this._condition._table_name = _g.d.cb_resource._table;
            this._condition._maxColumn = 1;
            this._condition._addDateBox(0, 0, 1, 1, _g.d.cb_resource._date_from, 1, true);
            this._condition._addDateBox(1, 0, 1, 1, _g.d.cb_resource._date_to, 1, true);
            DateTime __today = DateTime.Now;
            this._condition._setDataDate(_g.d.cb_resource._date_from, new DateTime(__today.Year, __today.Month, 1).AddMonths(-12));
            this._condition._setDataDate(_g.d.cb_resource._date_to, new DateTime(__today.Year, __today.Month, __today.Day));
            // สมุดธนาคาร
            this._bookSelectGrid._table_name = _g.d.erp_pass_book._table;
            this._bookSelectGrid._isEdit = false;
            this._bookSelectGrid._addColumn("Select", 11, 1, 5);
            this._bookSelectGrid._addColumn(_g.d.erp_pass_book._code, 1, 10, 10);
            this._bookSelectGrid._addColumn(_g.d.erp_pass_book._book_number, 1, 20, 20);
            this._bookSelectGrid._addColumn(_g.d.erp_pass_book._name_1, 1, 30, 30);
            this._bookSelectGrid._addColumn(_g.d.erp_pass_book._bank_code, 1, 20, 15);
            this._bookSelectGrid._addColumn(_g.d.erp_pass_book._bank_branch, 1, 20, 20);
            this._bookSelectGrid._addColumn("bank_name", 1, 20, 20, false, true);

            this._bookSelectGrid._calcPersentWidthToScatter();
            //
            this._bookSelectGrid._loadFromDataTable(this._myFrameWork._queryShort("select 1 as select, (select name_1 from erp_bank where erp_bank.code = erp_pass_book.bank_code) as bank_name,* from " + _g.d.erp_pass_book._table + " order by " + _g.d.erp_pass_book._code).Tables[0]);
            //
            this._report._viewControl._buttonCondition.Visible = false;
            this._report._query += new SMLReport._generate.QueryEventHandler(_viewReport__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_viewReport__init);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_viewReport__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report.Disposed += (s1, e1) =>
            {
                this.Dispose();
            };
            //
            this._conditionForm.Text = "เงื่อนไข";
            this._condition.Dock = DockStyle.Fill;
            this._conditionForm._colorBackground = false;
            this._conditionForm.Controls.Add(this._condition);
            //
            this._bookSelectForm.Text = "สมุดธนาคาร";
            this._bookSelectGrid.Dock = DockStyle.Fill;
            this._bookSelectForm._colorBackground = false;
            this._bookSelectForm.Controls.Add(this._bookSelectGrid);
            //
            this._resultForm.Text = "รายการเคลื่อนไหว";
            this._report.Dock = DockStyle.Fill;
            this._resultForm._colorBackground = false;
            this._resultForm.Controls.Add(this._report);
            //
            DockableFormInfo __formCondition = this._dock.Add(this._conditionForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formCondition.ShowCloseButton = false;
            __formCondition.ShowContextMenuButton = false;
            this._dock.DockForm(__formCondition, DockStyle.Top, zDockMode.Inner);
            //
            DockableFormInfo __formBookSelect = this._dock.Add(this._bookSelectForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formBookSelect.ShowCloseButton = false;
            __formBookSelect.ShowContextMenuButton = false;
            this._dock.DockForm(__formBookSelect, __formCondition, DockStyle.Right, zDockMode.Inner);
            //
            DockableFormInfo __formResultGridl = this._dock.Add(this._resultForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formResultGridl.ShowCloseButton = false;
            __formResultGridl.ShowContextMenuButton = false;
            this._dock.DockForm(__formResultGridl, DockStyle.Fill, zDockMode.Inner);
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (columnNumber == sender._findColumnName(_g.d.cb_resource._doc_type))
            {
                sender._columnList[columnNumber]._dataStr = _g.g._transFlagGlobal._transName((int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr));
            }
        }

        DataRow[] _viewReport__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (level._levelName.Equals(this._levelNameMain))
            {
                DataTable __data = new DataTable();
                __data.Columns.Add(_g.d.cb_resource._book_no, typeof(string));
                __data.Columns.Add(_g.d.cb_resource._book_name, typeof(string));
                __data.Columns.Add(_g.d.cb_resource._book_number, typeof(string));
                __data.Columns.Add(_g.d.cb_resource._bank_code, typeof(string));

                for (int __loop = 0; __loop < this._bookCodeList.Count; __loop++)
                {
                    string bookCode = this._bookCodeList[__loop];

                    int __passBookCodeColumn = this._bookSelectGrid._findColumnByName(_g.d.erp_pass_book._code);
                    int __findRow = this._bookSelectGrid._findData(__passBookCodeColumn, this._bookCodeList[__loop]);

                    string __bookName = this._bookSelectGrid._cellGet(__findRow, _g.d.erp_pass_book._name_1).ToString();
                    string __bookNumber = this._bookSelectGrid._cellGet(__findRow, _g.d.erp_pass_book._book_number).ToString();
                    string __bookCode = this._bookSelectGrid._cellGet(__findRow, _g.d.erp_pass_book._bank_code).ToString() + "~" + this._bookSelectGrid._cellGet(__findRow, "bank_name").ToString();

                    __data.Rows.Add(bookCode, __bookName, __bookNumber, __bookCode);
                }
                return __data.Select();
            }
            else
                if (level._levelName.Equals(this._levelNameDoc))
            {
                StringBuilder __where = new StringBuilder();
                /*for (int __loop = 0; __loop < levelParent._columnList.Count; __loop++)
                {
                    if (levelParent._columnList[__loop]._fieldName.Length > 0)
                    {
                        if (__where.Length > 0)
                        {
                            __where.Append(" and ");
                        }
                        __where.Append(levelParent._columnList[__loop]._fieldName + "=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                    }
                }*/
                if (levelParent._columnList[0]._fieldName.Length > 0)
                {
                    if (__where.Length > 0)
                    {
                        __where.Append(" and ");
                    }
                    __where.Append(levelParent._columnList[0]._fieldName + "=\'" + source[levelParent._columnList[0]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                }
                if (this._dataTableData.Rows.Count == 0)
                    return null;

                return this._dataTableData.Select(__where.ToString());
            }
            return null;
        }

        SMLReport._generateLevelClass _reportInitMain(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._book_no, _g.d.cb_resource._book_no, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._book_name, _g.d.cb_resource._book_name, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._bank_code, _g.d.cb_resource._bank_code, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._book_number, _g.d.cb_resource._book_number, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));

            return this._report._addLevel(this._levelNameMain, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDocColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            int __columnWidth = 8;
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._doc_time, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._doc_type, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._remark1, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._remark, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._amount_in, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._amount_out, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            SMLReport._generateColumnListClass __columnAmountBalance = new SMLReport._generateColumnListClass(_g.d.cb_resource._amount_balance, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular);
            __columnAmountBalance._isTotal = false;
            columnList.Add(__columnAmountBalance);
        }

        SMLReport._generateLevelClass _reportInitDoc(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDocColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitColumnValue(__columnList);
            return this._report._addLevel(this._levelNameDoc, levelParent, __columnList, sumTotal, autoWidth);
        }

        void _viewReport__init()
        {
            this._report._resourceTable = _g.d.cb_resource._table;
            this._report._level = this._reportInitMain(null, false, false);
            SMLReport._generateLevelClass __level2 = this._reportInitDoc(this._report._level, true, 2, true);
        }

        void _viewReport__query()
        {
            StringBuilder __myquery = new StringBuilder();
            StringBuilder __incomeFlagCBTransDetail = new StringBuilder();
            StringBuilder __outcomeFlagCBTransDetail = new StringBuilder();
            StringBuilder __incomeFlagICTransDetail = new StringBuilder();
            StringBuilder __outcomeFlagICTransDetail = new StringBuilder();
            StringBuilder __arFlag = new StringBuilder();
            // ช่องเงินโอน (รับ)
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค) + ",");
            __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด));
            // ช่องเงินโอน (จ่าย)
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) + ",");
            __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ));
            // เงินสดธนาคาร (รับ)
            __incomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน) + ",");
            __incomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน) + ",");
            __incomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน) + ",");
            __incomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
            // เงินสดธนาคาร (จ่าย)
            __outcomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน) + ",");
            __outcomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน) + ",");
            __outcomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน));
            // ประเภทลูกหนี้
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน) + ",");
            __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้));

            //
            String __allFlagCBTransDetail = __incomeFlagCBTransDetail.ToString() + "," + __outcomeFlagCBTransDetail.ToString();
            String __alFlagICTransDetail = __incomeFlagICTransDetail.ToString() + "," + __outcomeFlagICTransDetail.ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร);
            //
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            this._dateBegin = MyLib._myGlobal._sqlDateFunction(MyLib._myGlobal._convertDateToQuery((MyLib._myGlobal._convertDate(this._condition._getDataStr(_g.d.cb_resource._date_from)))));
            this._dateEnd = MyLib._myGlobal._sqlDateFunction(MyLib._myGlobal._convertDateToQuery((MyLib._myGlobal._convertDate(this._condition._getDataStr(_g.d.cb_resource._date_to)))));
            this._bookCode = new StringBuilder();
            for (int __row = 0; __row < this._bookSelectGrid._rowData.Count; __row++)
            {
                if ((int)MyLib._myGlobal._decimalPhase(this._bookSelectGrid._cellGet(__row, 0).ToString()) == 1)
                {
                    string __value = this._bookSelectGrid._cellGet(__row, _g.d.erp_pass_book._code).ToString().Trim().ToUpper();
                    if (__value.Length > 0)
                    {
                        if (this._bookCode.Length > 0)
                        {
                            this._bookCode.Append(",");
                        }
                        this._bookCode.Append("\'" + __value + "\'");
                    }
                }
            }
            //
            string __queryTrans1 = "select 1 as doc_sort, " + MyLib._myGlobal._fieldAndComma("case when " + _g.d.cb_trans_detail._pass_book_code + " is null or " + _g.d.cb_trans_detail._pass_book_code + "=\'\' then " + _g.d.cb_trans_detail._trans_number + " else " + _g.d.cb_trans_detail._pass_book_code + " end as " + _g.d.cb_resource._book_no, " case when " + _g.d.cb_trans_detail._doc_type + "=1 then " + _g.d.cb_trans_detail._chq_due_date + " else " + _g.d.cb_trans_detail._doc_date + " end as " + _g.d.cb_resource._doc_date, _g.d.cb_trans_detail._doc_time + " as " + _g.d.cb_resource._doc_time, _g.d.cb_trans_detail._doc_no + " as " + _g.d.cb_resource._doc_no, _g.d.cb_trans_detail._remark + " as " + _g.d.cb_resource._remark, "case when " +
                _g.d.cb_trans_detail._trans_flag + " in (" + __incomeFlagCBTransDetail.ToString() + ") then " + _g.d.cb_trans_detail._amount + " else 0 end as " + _g.d.cb_resource._amount_in, "case when " +
                _g.d.cb_trans_detail._trans_flag + " in (" + __outcomeFlagCBTransDetail.ToString() + ") then " + _g.d.cb_trans_detail._amount + " else 0 end as " + _g.d.cb_resource._amount_out, "0 as " + _g.d.cb_resource._amount_balance, _g.d.cb_trans_detail._trans_flag + " as " + _g.d.cb_resource._doc_type, "{1}") + " from " + _g.d.cb_trans_detail._table + " where case when " + _g.d.cb_trans_detail._pass_book_code + " is null or " + _g.d.cb_trans_detail._pass_book_code + "=\'\' then " + _g.d.cb_trans_detail._trans_number + " else " + _g.d.cb_trans_detail._pass_book_code + " end in (" + this._bookCode.ToString() + ") and " + _g.d.cb_trans_detail._trans_flag + " in (" + __allFlagCBTransDetail + ") " +
                " and ( case when  " + _g.d.cb_trans_detail._doc_type + "=1 then " + _g.d.cb_trans_detail._chq_due_date + " {0} else " + _g.d.cb_trans_detail._doc_date + " {0} end) " +
                " and coalesce(" + _g.d.cb_trans_detail._last_status + ", 0)=0 " +
                " and " + _g.d.cb_trans_detail._doc_type + "=1 "; // " and " + _g.d.cb_trans_detail._doc_type + "<>2 ";

            string __queryTrans2 = "select 1 as doc_sort, " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code + " as " + _g.d.cb_resource._book_no, _g.d.ic_trans_detail._doc_date + " as " + _g.d.cb_resource._doc_date, _g.d.ic_trans_detail._doc_time + " as " + _g.d.cb_resource._doc_time, _g.d.ic_trans_detail._doc_no + " as " + _g.d.cb_resource._doc_no, _g.d.cb_resource._remark + " as " + _g.d.cb_trans_detail._remark, "case when " +
_g.d.ic_trans_detail._trans_flag + " in (" + __incomeFlagICTransDetail.ToString() + ") then " + _g.d.ic_trans_detail._sum_amount + " else (case when " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร) + " then " + _g.d.ic_trans_detail._transfer_amount + " else  0  end) end as " + _g.d.cb_resource._amount_in, "case when " +
_g.d.ic_trans_detail._trans_flag + " in (" + __outcomeFlagICTransDetail.ToString() + ") then " + _g.d.ic_trans_detail._sum_amount + " else (case when " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร) + " then " + _g.d.ic_trans_detail._transfer_amount + " else  0  end) end as " + _g.d.cb_resource._amount_out, "0 as " + _g.d.cb_resource._amount_balance, _g.d.ic_trans_detail._trans_flag + " as " + _g.d.cb_resource._doc_type, "\'\' as " + _g.d.cb_resource._remark1) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._item_code + " in (" + this._bookCode.ToString() + ") and " + _g.d.ic_trans_detail._trans_flag + " in (" + __alFlagICTransDetail + ") and (" + _g.d.ic_trans_detail._doc_date + " {0}) and coalesce(" + _g.d.ic_trans_detail._last_status + ", 0)=0";

            // toe ค่าธรรมเนียม
            string __queryFeeAmount = "select 2 as doc_sort, " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code + " as " + _g.d.cb_resource._book_no, _g.d.ic_trans_detail._doc_date + " as " + _g.d.cb_resource._doc_date, _g.d.ic_trans_detail._doc_time + " as " + _g.d.cb_resource._doc_time, _g.d.ic_trans_detail._doc_no + " as " + _g.d.cb_resource._doc_no, _g.d.cb_resource._remark + " as " + _g.d.cb_trans_detail._remark
                , " 0 as " + _g.d.cb_resource._amount_in
                , "case when " + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร) + ") then " + _g.d.ic_trans_detail._fee_amount + " else 0 end as " + _g.d.cb_resource._amount_out
                , "0 as " + _g.d.cb_resource._amount_balance, _g.d.ic_trans_detail._trans_flag + " as " + _g.d.cb_resource._doc_type, "\'\' as " + _g.d.cb_resource._remark1) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._item_code + " in (" + this._bookCode.ToString() + ") and " + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร) + ") and (" + _g.d.ic_trans_detail._fee_amount + " > 0 ) and (" + _g.d.ic_trans_detail._doc_date + " {0}) and coalesce(" + _g.d.ic_trans_detail._last_status + ", 0)=0";

            // ยอดยกมา
            string __queryBalance = "select " + _g.d.cb_resource._book_no + ",sum(" + _g.d.cb_resource._amount_in + "-" + _g.d.cb_resource._amount_out + ") as " + _g.d.cb_resource._amount_balance + " from (" + String.Format(__queryTrans1 + " union all " + __queryTrans2 + " union all " + __queryFeeAmount, " < " + this._dateBegin, "\'\' as " + _g.d.cb_resource._remark1) + ") as temp1 group by " + _g.d.cb_resource._book_no;
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryBalance));

            // รายวัน
            string __apArCode = "(select ap_ar_code from cb_trans where cb_trans.doc_no=" + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no + " and " + " cb_trans.trans_flag=" + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_flag + ")";
            string __queryTrans = String.Format(__queryTrans1 + " union all " + __queryTrans2 + " union all " + __queryFeeAmount, " between " + this._dateBegin + " and " + this._dateEnd, "case when " + _g.d.cb_trans_detail._trans_flag + " in (" + __arFlag.ToString() + ") then (select name_1 from ar_customer where ar_customer.code=" + __apArCode + ") else  (select name_1 from ap_supplier where ap_supplier.code=" + __apArCode + ") end as " + _g.d.cb_resource._remark1) + " order by " + MyLib._myGlobal._fieldAndComma(_g.d.cb_resource._book_no, _g.d.cb_resource._doc_date, _g.d.cb_resource._doc_time) + ", doc_sort";
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTrans));
            __myquery.Append("</node>");

            ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._bookCodeList = new List<string>();
            this._dataTableBalance = null;
            this._dataTableData = null;
            if (_getData.Count > 0)
            {
                this._dataTableBalance = ((DataSet)_getData[0]).Tables[0];
                this._dataTableData = ((DataSet)_getData[1]).Tables[0];
                DataTable __book1 = MyLib._dataTableExtension._selectDistinct(this._dataTableBalance, _g.d.cb_resource._book_no);
                DataTable __book2 = MyLib._dataTableExtension._selectDistinct(this._dataTableData, _g.d.cb_resource._book_no);
                if (__book1 != null)
                {
                    for (int __loop = 0; __loop < __book1.Rows.Count; __loop++)
                    {
                        this._bookCodeList.Add(__book1.Rows[__loop][0].ToString());
                    }
                }
                if (__book2 != null)
                {
                    for (int __loop1 = 0; __loop1 < __book2.Rows.Count; __loop1++)
                    {
                        Boolean __found = false;
                        for (int __loop2 = 0; __loop2 < this._bookCodeList.Count; __loop2++)
                        {
                            if (this._bookCodeList[__loop2].Equals(__book2.Rows[__loop1][0].ToString()))
                            {
                                __found = true;
                                break;
                            }
                        }
                        if (__found == false)
                        {
                            this._bookCodeList.Add(__book2.Rows[__loop1][0].ToString());
                        }
                    }
                }
                // คำนวณยอดคงเหลือ
                for (int __loop1 = 0; __loop1 < this._bookCodeList.Count; __loop1++)
                {
                    string __bookCode = this._bookCodeList[__loop1];
                    decimal __balance = 0M;
                    if (__book1 != null && this._dataTableBalance.Rows.Count > 0)
                    {
                        DataRow[] __select = this._dataTableBalance.Select(_g.d.cb_resource._book_no + "=\'" + __bookCode + "\'");
                        if (__select.Length > 0)
                        {
                            __balance = MyLib._myGlobal._decimalPhase(__select[0][_g.d.cb_resource._amount_balance].ToString());
                        }
                    }
                    for (int __loop2 = 0; __loop2 < this._dataTableData.Rows.Count; __loop2++)
                    {
                        if (this._dataTableData.Rows[__loop2][_g.d.cb_resource._book_no].ToString().Equals(__bookCode))
                        {
                            __balance = __balance + (MyLib._myGlobal._decimalPhase(this._dataTableData.Rows[__loop2][_g.d.cb_resource._amount_in].ToString()) - MyLib._myGlobal._decimalPhase(this._dataTableData.Rows[__loop2][_g.d.cb_resource._amount_out].ToString()));
                            this._dataTableData.Rows[__loop2][_g.d.cb_resource._amount_balance] = __balance;
                        }
                    }
                }
            }
        }
    }
}
