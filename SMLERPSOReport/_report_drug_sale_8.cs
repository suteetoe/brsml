using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;
using System.Collections;

namespace SMLERPSOReport
{
    public partial class _report_drug_sale_8 : UserControl
    {
        private MyLib._myForm _conditionForm = new MyLib._myForm();
        private MyLib._myForm _resultForm = new MyLib._myForm();
        private MyLib._myForm _bookSelectForm = new MyLib._myForm();
        private MyLib._myScreen _condition = new MyLib._myScreen();
        private SMLReport._generate _report = new SMLReport._generate("", true);
        private MyLib._myGrid _bookSelectGrid = new MyLib._myGrid();
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private List<string> _bookCodeList;
        private DataTable _dataTableBalance;
        private DataTable _dataTableData;
        private string _dateBegin;
        private string _dateEnd;
        private StringBuilder _lotNumberSelect;
        string _levelNameMain = "cust";
        string _levelNameDoc = "doc";
        string _itemCode = "";

        string _columnNo = "row_number";
        int _columnNumberCount = 0;

        //string _columnDrugName = "name_1";
        public _report_drug_sale_8()
        {
            InitializeComponent();

            // เงื่อนไข
            this._condition._table_name = _g.d.ic_resource._table;
            this._condition._maxColumn = 1;
            this._condition._addTextBox(0, 0, 1, 1, _g.d.ic_resource._item_code, 1, 0, 1, true, false);
            this._condition._textBoxSearch += _condition__textBoxSearch;
            this._condition._textBoxChanged += _condition__textBoxChanged;

            // รายละเอัยด LOT
            this._bookSelectGrid._table_name = _g.d.ic_resource._table;
            this._bookSelectGrid._isEdit = false;
            this._bookSelectGrid._addColumn("Select", 11, 1, 5);
            this._bookSelectGrid._addColumn(_g.d.ic_resource._lot_number, 1, 20, 10);
            this._bookSelectGrid._addColumn(_g.d.ic_resource._date_expire, 4, 10, 8, false, false);

            this._bookSelectGrid._calcPersentWidthToScatter();
            //
            //this._bookSelectGrid._loadFromDataTable(this._myFrameWork._queryShort("select 1 as select,* from " + _g.d.erp_pass_book._table + " order by " + _g.d.erp_pass_book._code).Tables[0]);
            //
            this._report._viewControl._buttonCondition.Visible = false;
            this._report._query += new SMLReport._generate.QueryEventHandler(_viewReport__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_viewReport__init);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_viewReport__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report._renderHeader += _report__renderHeader;
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
            this._bookSelectForm.Text = "หมายเลข LOT";
            this._bookSelectGrid.Dock = DockStyle.Fill;
            this._bookSelectForm._colorBackground = false;
            this._bookSelectForm.Controls.Add(this._bookSelectGrid);
            //
            this._resultForm.Text = "รายงานการขายยาตามที่สำนักงานคณะกรรมการอาหารและยากำหนด";
            this._report.Dock = DockStyle.Fill;
            this._resultForm._colorBackground = false;
            this._resultForm.Controls.Add(this._report);
            //
            DockableFormInfo __formCondition = this._dock.Add(this._conditionForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formCondition.ShowCloseButton = false;
            __formCondition.ShowContextMenuButton = false;
            this._dock.DockForm(__formCondition, DockStyle.Left, zDockMode.Inner);
            //
            DockableFormInfo __formBookSelect = this._dock.Add(this._bookSelectForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formBookSelect.ShowCloseButton = false;
            __formBookSelect.ShowContextMenuButton = false;
            this._dock.DockForm(__formBookSelect, __formCondition, DockStyle.Bottom, zDockMode.Inner);
            //
            DockableFormInfo __formResultGridl = this._dock.Add(this._resultForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formResultGridl.ShowCloseButton = false;
            __formResultGridl.ShowContextMenuButton = false;
            this._dock.DockForm(__formResultGridl, DockStyle.Fill, zDockMode.Inner);
        }

        void _condition__textBoxSearch(object sender)
        {
            // open popup search
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            MyLib._searchDataFull __searchControl = null;

            if (name == _g.d.ic_resource._item_code)
            {

                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                __searchControl = new MyLib._searchDataFull();
                __searchControl._name = _g.g._search_screen_ic_inventory;
                __searchControl._dataList._loadViewFormat(__searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchControl._dataList._gridData._mouseClick += (o1, s1) =>
                {
                    string __arCode = __searchControl._dataList._gridData._cellGet(__searchControl._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                    this._condition._setDataStr(_g.d.ic_resource._item_code, __arCode);
                    __searchControl.Close();

                };

                __searchControl._searchEnterKeyPress += (s1, e1) =>
                {
                    string __arCode = __searchControl._dataList._gridData._cellGet(__searchControl._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                    this._condition._setDataStr(_g.d.ic_resource._item_code, __arCode);
                    __searchControl.Close();
                };


                __searchControl.StartPosition = FormStartPosition.CenterScreen;
                __searchControl.ShowDialog();
            }
        }

        void _report__renderHeader(SMLReport._generate source)
        {
            //string __reportName = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._report_name);

            SMLReport._report._objectListType __headerObject = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
            int __column0 = source._viewControl._addColumn(__headerObject, 100);
            //
            source._viewControl._addCell(__headerObject, __column0, true, 0, -1, SMLReport._report._cellType.String, "รายงานการขายยาตามที่สำนักงานคณะกรรมการอาหารและยากำหนด", SMLReport._report._cellAlign.Center, source._viewControl._fontHeader1);
            //
            source._viewControl._addCell(__headerObject, __column0, true, 1, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdBusinessName, SMLReport._report._cellAlign.Center, source._viewControl._fontHeader2);
            source._viewControl._addCell(__headerObject, __column0, true, 2, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdAddress.Replace("\n", string.Empty), SMLReport._report._cellAlign.Center, source._viewControl._fontHeader2);
            //
            SMLReport._report._objectListType __headerObject2 = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
            int __column1 = source._viewControl._addColumn(__headerObject2, 100);
            //
            source._viewControl._addCell(__headerObject2, __column1, true, 2, -1, SMLReport._report._cellType.String, "แบบ ข.ย.8", SMLReport._report._cellAlign.Right, source._viewControl._fontHeader2);
            //
        }

        void _condition__textBoxChanged(object sender, string name)
        {
            this._bookSelectGrid._clear();

            // update lot grid
            this._itemCode = this._condition._getDataStr(_g.d.ic_resource._item_code);
            if (this._itemCode.Length > 0)
            {
                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                this._bookSelectGrid._loadFromDataTable(__process._stkStockInfoAndBalanceByLot(_g.g._productCostType.ปรกติ, null, this._itemCode, this._itemCode, "", false, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT));


            }
        }

        void _viewReport__init()
        {
            //this._report._resourceTable = _g.d.ic_resource._table;

            this._report._level = this._reportInitMain(null, false, true);
            SMLReport._generateLevelClass __level2 = this._reportInitDoc(this._report._level, false, 0, true);
        }

        SMLReport._generateLevelClass _reportInitMain(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();

            __columnList.Add(new SMLReport._generateColumnListClass(_columnNo, "ลำดับที่", 3, SMLReport._report._cellType.Number, 0, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._ic_name, "ชื่อยา", 18, SMLReport._report._cellType.String, 0, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._lot_number, "เลขที่หรืออักษร", 10, SMLReport._report._cellType.String, 0, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._mfd_date, "วัน เดือน ปี", 6, SMLReport._report._cellType.DateTime, 0, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._qty_in, "จำนวน/ปริมาณ", 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._doc_date, "วัน เดือน ปี", 6, SMLReport._report._cellType.DateTime, 0, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._qty_out, "จำนวน/ปริมาณ", 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._ar_name, "ชื่อผู้ซื้อ", 19, SMLReport._report._cellType.String, 0, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._address, "ที่อยู่", 32, SMLReport._report._cellType.String, 0, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            __columnList.Add(new SMLReport._generateColumnListClass("", "ลายมือชื่อผู้มี", 7, SMLReport._report._cellType.String, 0, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center }); // ลายมือชื่อ
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._remark, _g.d.ic_trans._table + "." + _g.d.ic_trans._remark, 5, SMLReport._report._cellType.String, 0, FontStyle.Bold) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });

            return this._report._addLevel(this._levelNameMain, levelParent, __columnList, sumTotal, autoWidth);
        }

        SMLReport._generateLevelClass _reportInitDoc(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDocColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitColumnValue(__columnList);
            return this._report._addLevel(this._levelNameDoc, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDocColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            //columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            /*
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
             * */
            columnList.Add(new SMLReport._generateColumnListClass(_columnNo, "expire_date_temp.cust_name", 3, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._item_name, "expire_date_temp.cust_name", 18, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._lot_number, "ของครั้งที่ผลิต", 10, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._mfd_date, "ที่ผลิต", 6, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._qty_in, "ที่ผลิต", 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._doc_date, "ที่ขาย", 6, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._qty_out, "ที่ขาย", 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._cust_name, "expire_date_temp.cust_name", 19, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer._address, "expire_date_temp.cust_name", 32, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });
            columnList.Add(new SMLReport._generateColumnListClass("", "หน้าที่ปฏิบัติงาน", 7, SMLReport._report._cellType.String, 0) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center }); // ลายมือชื่อ
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._remark, "expire_date_temp.cust_name", 5, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.All, _headerBorderStyle = SMLReport._report._columnBorder.LeftRight, _headeralign = SMLReport._report._cellAlign.Center });


        }

        DataRow[] _viewReport__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (level._levelName.Equals(this._levelNameMain))
            {
                /*DataTable __data = new DataTable();
                __data.Columns.Add(_g.d.cb_resource._book_no, typeof(string));
                for (int __loop = 0; __loop < this._bookCodeList.Count; __loop++)
                {
                    __data.Rows.Add(this._bookCodeList[__loop]);
                }*/
                return _dataTableBalance.Select();
            }
            else
                if (level._levelName.Equals(this._levelNameDoc))
                {
                    _columnNumberCount = 0;

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
                    __where.Append(" lot_number = \'" + source["lot_number"].ToString() + "\'");
                    if (this._dataTableData.Rows.Count == 0)
                        return null;

                    return this._dataTableData.Select(__where.ToString());
                }
            return null;
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (columnNumber == sender._findColumnName(_g.d.ar_customer._address))
            {
                sender._columnList[columnNumber]._dataStr = sender._columnList[columnNumber]._dataStr.Replace("\n", string.Empty);
            }
        }

        void _viewReport__query()
        {
            StringBuilder __myquery = new StringBuilder();

            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");

            this._dateBegin = MyLib._myGlobal._sqlDateFunction(MyLib._myGlobal._convertDateToQuery((MyLib._myGlobal._convertDate(this._condition._getDataStr(_g.d.cb_resource._date_from)))));
            this._dateEnd = MyLib._myGlobal._sqlDateFunction(MyLib._myGlobal._convertDateToQuery((MyLib._myGlobal._convertDate(this._condition._getDataStr(_g.d.cb_resource._date_to)))));

            this._lotNumberSelect = new StringBuilder();
            for (int __row = 0; __row < this._bookSelectGrid._rowData.Count; __row++)
            {
                if ((int)MyLib._myGlobal._decimalPhase(this._bookSelectGrid._cellGet(__row, 0).ToString()) == 1)
                {
                    string __value = this._bookSelectGrid._cellGet(__row, _g.d.ic_resource._lot_number).ToString().Trim().ToUpper();
                    if (__value.Length > 0)
                    {
                        if (this._lotNumberSelect.Length > 0)
                        {
                            this._lotNumberSelect.Append(",");
                        }
                        this._lotNumberSelect.Append("\'" + __value + "\'");
                    }
                }
            }

            //  head
            StringBuilder __queryLot1 = new StringBuilder();
            __queryLot1.Append("select 0 as " + _columnNo + " ,ic_code,ic_name,ic_unit_code||'~'||(select name_1 from ic_unit where ic_unit.code=ic_unit_code) as ic_unit_code,lot_number,qty_in*(select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=temp2.ic_code and ic_unit_use.code=temp2.ic_unit_code) as qty_in, (select  mfd_date from ic_trans_detail_lot where ic_trans_detail_lot.lot_number = temp2.lot_number and ic_trans_detail_lot.item_code=temp2.ic_code and calc_flag = 1 order by doc_date, doc_time  offset 0 limit 1  ) as mfd_date, null as doc_date, 0 as qty_out, '' as ar_name, '' as address, '' as remark  ");
            __queryLot1.Append(" from ( ");
            __queryLot1.Append(" select ic_code,ic_name,ic_unit_code,lot_number,sum(qty_in) as qty_in ");
            __queryLot1.Append(" from ( ");
            __queryLot1.Append(" select item_code as ic_code,coalesce((select name_1 from ic_inventory where ic_inventory.code=ic_trans_detail_lot.item_code),'') as ic_name,coalesce((select unit_cost from ic_inventory where ic_inventory.code=ic_trans_detail_lot.item_code),'') as ic_unit_code ,lot_number as lot_number ,case when calc_flag=1 and trans_flag <> 70 then qty else 0 end as qty_in ");
            __queryLot1.Append(" from ic_trans_detail_lot ");
            __queryLot1.Append(" where item_code = \'" + this._itemCode + "\' ");
            if (this._lotNumberSelect.Length > 0)
            {
                __queryLot1.Append(" and " + _g.d.ic_trans_detail_lot._lot_number + " in (" + this._lotNumberSelect.ToString() + ") ");
            }
            __queryLot1.Append(" ) as temp1  ");
            __queryLot1.Append(" group by ic_code,ic_name,ic_unit_code,lot_number ");
            __queryLot1.Append(" ) as temp2 order by ic_code,lot_number");

            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryLot1.ToString()));


            // detail
            StringBuilder __queryDetail = new StringBuilder();
            __queryDetail.Append(" select 0 as " + _columnNo + ", item_code, (select name_1 from ic_inventory where ic_inventory.code = temp1.item_code)  as item_name, lot_number, 0 as qty_in, null as mfd_date, doc_date, qty as qty_out ");
            __queryDetail.Append(" , (select name_1 from ar_customer where ar_customer.code = temp1.cust_code) as cust_name ");
            __queryDetail.Append(" , (select address from ar_customer where ar_customer.code = temp1.cust_code) as address ");
            __queryDetail.Append(" , doc_no, (sale_code  || '~' || (select name_1 from erp_user where erp_user.code = sale_code )) as sale_code ");
            __queryDetail.Append(" from (  ");
            __queryDetail.Append(" select (select cust_code from ic_trans where ic_trans.doc_no = ic_trans_detail_lot.doc_no and ic_trans.trans_flag = ic_trans.trans_flag) as cust_code, item_code, lot_number, doc_date, doc_no, trans_flag , qty ");
            __queryDetail.Append(" , (select sale_code from ic_trans where ic_trans.doc_no = ic_trans_detail_lot.doc_no and ic_trans.trans_flag = ic_trans.trans_flag) as sale_code ");
            __queryDetail.Append(" from ic_trans_detail_lot  ");
            __queryDetail.Append(" where trans_flag = 44  and item_code  = \'" + this._itemCode + "\' ");
            if (this._lotNumberSelect.Length > 0)
            {
                __queryDetail.Append(" and " + _g.d.ic_trans_detail_lot._lot_number + " in (" + this._lotNumberSelect.ToString() + ") ");
            }

            __queryDetail.Append(" ) as temp1  ");
            __queryDetail.Append(" order by item_code, lot_number, doc_date, doc_no ");

            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail.ToString()));


            __myquery.Append("</node>");

            ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._bookCodeList = new List<string>();
            this._dataTableBalance = null;
            this._dataTableData = null;
            if (_getData.Count > 0)
            {
                this._dataTableBalance = ((DataSet)_getData[0]).Tables[0];
                this._dataTableData = ((DataSet)_getData[1]).Tables[0];
                DataTable __book1 = MyLib._dataTableExtension._selectDistinct(this._dataTableBalance, _g.d.ic_resource._lot_number);
                DataTable __book2 = MyLib._dataTableExtension._selectDistinct(this._dataTableData, _g.d.ic_resource._lot_number);
                if (__book1 != null)
                {
                    for (int __loop = 0; __loop < __book1.Rows.Count; __loop++)
                    {
                        this._bookCodeList.Add(__book1.Rows[__loop]["lot_number"].ToString());
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
                int __rowNumber = 0;
                // คำนวณยอดคงเหลือ
                for (int __loop1 = 0; __loop1 < this._bookCodeList.Count; __loop1++)
                {
                    __rowNumber = 0;
                    string __bookCode = this._bookCodeList[__loop1];
                    decimal __balance = 0M;
                    if (__book1 != null && this._dataTableBalance.Rows.Count > 0)
                    {
                        DataRow[] __select = this._dataTableBalance.Select(" lot_number =\'" + __bookCode + "\'");
                        if (__select.Length > 0)
                        {
                            __balance = MyLib._myGlobal._decimalPhase(__select[0][_g.d.ic_resource._qty_in].ToString());
                        }
                    }
                    for (int __loop2 = 0; __loop2 < this._dataTableData.Rows.Count; __loop2++)
                    {
                        if (this._dataTableData.Rows[__loop2][_g.d.ic_resource._lot_number].ToString().Equals(__bookCode))
                        {
                            __balance = __balance + (MyLib._myGlobal._decimalPhase(this._dataTableData.Rows[__loop2][_g.d.ic_resource._qty_in].ToString()) - MyLib._myGlobal._decimalPhase(this._dataTableData.Rows[__loop2][_g.d.ic_resource._qty_out].ToString()));
                            this._dataTableData.Rows[__loop2][_g.d.ic_resource._qty_in] = __balance;
                            this._dataTableData.Rows[__loop2][_columnNo] = ++__rowNumber;
                        }
                    }
                }
            }
        }

    }
}
