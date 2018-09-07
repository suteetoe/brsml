using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace SMLERPGLControl
{
    public partial class _glDetail : UserControl
    {
        private _g._searchChartOfAccountDialog _searchChartOfAccount = new _g._searchChartOfAccountDialog();
        private _g._searchChartOfAccount _chartOfAccountScreen = new _g._searchChartOfAccount();
        MyLib._searchDataFull _searchBranch = new MyLib._searchDataFull();

        private _glDetailExtra __detailExtra;
        string _columnBranchCodeTemp = "barnch_code_temp";
        

        public _glDetail()
        {
            InitializeComponent();
            //
            //this.difference_debit_credit._resource_name = _g.d.gl_journal_detail._table + "." + this.difference_debit_credit.Name;
            //this.difference_debit_credit._getResourceName(_g.d.gl_journal_detail._table);
            //
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._glDetailGrid._table_name = _g.d.gl_journal_detail._table;
            this._glDetailGrid._rowNumberWork = true;
            this._glDetailGrid.AllowDrop = true;
            this._glDetailGrid._addColumn(_g.d.gl_journal_detail._account_code, 1, 25, 15, true, false, true, true);
            this._glDetailGrid._addColumn(_g.d.gl_journal_detail._account_name, 1, 100, 25, true, false);
            this._glDetailGrid._addColumn(_g.d.gl_journal_detail._description, 1, 255, 25, true, false);
            if (_g.g._companyProfile._branchStatus == 1)
            {
                this._glDetailGrid._addColumn(this._columnBranchCodeTemp, 1, 25, 15, true, false, false, true, "", "", "", _g.d.gl_journal_detail._branch_code);
                this._glDetailGrid._addColumn(_g.d.gl_journal_detail._branch_code, 1, 25, 15, true, true, true, false);
            }
            this._glDetailGrid._addColumn(_g.d.gl_journal_detail._debit, 3, 0, 15, true, false, true, false, __formatNumber);
            this._glDetailGrid._addColumn(_g.d.gl_journal_detail._credit, 3, 0, 15, true, false, true, false, __formatNumber);
            this._glDetailGrid._addColumn(_g.d.gl_journal_detail._dimension, 12, 0, 5, true, false, false);
            this._glDetailGrid._addColumn(_g.d.gl_journal_detail._line_number, 2, 0, 15, false, true, false);
            this._glDetailGrid._addColumn(this._glDetailGrid._rowNumberName, 2, 0, 15, false, true, true);
            //
            this._glDetailGrid._clickSearchButton += new MyLib.SearchEventHandler(_glDetailGrid__clickSearchButton);
            this._glDetailGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_glDetailGrid__alterCellUpdate);
            this._glDetailGrid._totalCheck += new MyLib.TotalCheckEventHandler(_glDetailGrid__totalCheck);
            this._glDetailGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_glDetailGrid__queryForInsertCheck);
            this._glDetailGrid._queryForInsertPerRow += new MyLib.QueryForInsertPerRowEventHandler(_glDetailGrid__queryForInsertPerRow);
            this._glDetailGrid._moveNextColumn += new MyLib.MoveNextColumnEventHandler(_glDetailGrid__moveNextColumn);
            this._glDetailGrid._mouseClickClip += new MyLib.ClipMouseClickHandler(_glDetailGrid__mouseClickClip);
            this._glDetailGrid._afterAddRow += new MyLib.AfterAddRowEventHandler(_glDetailGrid__afterAddRow);
            this._glDetailGrid.DragEnter += new DragEventHandler(_glDetailGrid_DragEnter);
            this._glDetailGrid.DragDrop += new DragEventHandler(_glDetailGrid_DragDrop);
            //
            this._glDetailGrid.Invalidated += new InvalidateEventHandler(_glDetailGrid_Invalidated);
            this._glDetailGrid._calcPersentWidthToScatter();
            //
            _searchChartOfAccount._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchChartOfAccount._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(searchChartOfAccount__searchEnterKeyPress);


            _searchBranch._name = _g.g._search_master_erp_branch_list;
            _searchBranch._dataList._loadViewFormat(_searchBranch._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchBranch._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchBranch._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(searchChartOfAccount__searchEnterKeyPress);

        }

        //MyLib.BeforeDisplayRowReturn _glDetailGrid__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        //{
        //    if (columnName.Equals(this._glDetailGrid._table_name + "." + this._columnBranchCodeTemp))
        //    {
        //        int __branchNameColumn = this._glDetailGrid._findColumnByName(this._columnBranchNameTemp);
        //        string __branchName = this._glDetailGrid._cellGet(row, __branchNameColumn).ToString();
        //        if (__branchName.Length > 0) ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __branchName);

        //    }
        //    return senderRow;
        //}

        public string _deleteGlExtraList(string docNo)
        {
            string __queryBeginCommand = "<query>";
            string __queryEndCommand = "</query>";
            string __queryDelete = "delete from ";
            string __queryWhere = " where ";

            StringBuilder __result = new StringBuilder();
            __result.Append(string.Concat(__queryBeginCommand, __queryDelete, _g.d.gl_journal_side_list._table, __queryWhere, _g.d.gl_journal_side_list._doc_no, "=\'", docNo, "\'", __queryEndCommand));
            __result.Append(string.Concat(__queryBeginCommand, __queryDelete, _g.d.gl_journal_job_list._table, __queryWhere, _g.d.gl_journal_job_list._doc_no, "=\'", docNo, "\'", __queryEndCommand));
            __result.Append(string.Concat(__queryBeginCommand, __queryDelete, _g.d.gl_journal_project_list._table, __queryWhere, _g.d.gl_journal_project_list._doc_no, "=\'", docNo, "\'", __queryEndCommand));
            __result.Append(string.Concat(__queryBeginCommand, __queryDelete, _g.d.gl_journal_depart_list._table, __queryWhere, _g.d.gl_journal_depart_list._doc_no, "=\'", docNo, "\'", __queryEndCommand));
            __result.Append(string.Concat(__queryBeginCommand, __queryDelete, _g.d.gl_journal_allocate_list._table, __queryWhere, _g.d.gl_journal_allocate_list._doc_no, "=\'", docNo, "\'", __queryEndCommand));
            return __result.ToString();
        }


        public string _saveGlExtraListQuery(MyLib._myGrid detailGrid, string __fieldList, string __dataList)
        {
            StringBuilder __myQuery = new StringBuilder();
            for (int __row = 0; __row < detailGrid._rowData.Count; __row++)
            {
                string __accountCode = (string)detailGrid._cellGet(__row, 0);
                SMLERPGLControl._glDetailExtraObject __getExtraData = (SMLERPGLControl._glDetailExtraObject)detailGrid._cellGet(__row, _g.d.gl_journal_detail._dimension);
                int __line_number = __row;
                // Side List
                for (int __loop = 0; __loop < __getExtraData._sideList.Count; __loop++)
                {
                    int __line_number_detail = __loop + 1;
                    SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._sideList[__loop];
                    if (__getDetailData._code.Length > 0)
                    {
                        __myQuery.Append(string.Concat("<query>insert into ", _g.d.gl_journal_side_list._table, " (", __fieldList, _g.d.gl_journal_side_list._side_code, ",", _g.d.gl_journal_side_list._side_name, ",",
                            _g.d.gl_journal_side_list._allocate_persent, ",", _g.d.gl_journal_side_list._allocate_amount, ",", _g.d.gl_journal_side_list._line_number, ",", _g.d.gl_journal_side_list._line_number_detail, ",",
                            _g.d.gl_journal_side_list._account_code, ") values (", __dataList, "\'", __getDetailData._code, "\',\'", __getDetailData._name, "\',", __getDetailData._persent.ToString(), ",", __getDetailData._amount.ToString(), ",",
                            __line_number.ToString(), ",", __line_number_detail.ToString(), ",\'", __accountCode, "\')</query>"));
                    }
                }
                // Department List
                for (int __loop = 0; __loop < __getExtraData._departmentList.Count; __loop++)
                {
                    int __line_number_detail = __loop + 1;
                    SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._departmentList[__loop];
                    if (__getDetailData._code.Length > 0)
                    {
                        __myQuery.Append(string.Concat("<query>insert into ", _g.d.gl_journal_depart_list._table, " (", __fieldList, _g.d.gl_journal_depart_list._department_code, ",", _g.d.gl_journal_depart_list._department_name, ",",
                            _g.d.gl_journal_depart_list._allocate_persent, ",", _g.d.gl_journal_depart_list._allocate_amount, ",", _g.d.gl_journal_depart_list._line_number, ",", _g.d.gl_journal_depart_list._line_number_detail, ",",
                            _g.d.gl_journal_depart_list._account_code, ") values (", __dataList, "\'", __getDetailData._code, "\',\'", __getDetailData._name, "\',", __getDetailData._persent.ToString(), ",", __getDetailData._amount.ToString(), ",",
                            __line_number.ToString(), ",", __line_number_detail.ToString(), ",\'", __accountCode, "\')</query>"));
                    }
                }
                // Project List
                for (int __loop = 0; __loop < __getExtraData._projectList.Count; __loop++)
                {
                    int __line_number_detail = __loop + 1;
                    SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._projectList[__loop];
                    if (__getDetailData._code.Length > 0)
                    {
                        __myQuery.Append(string.Concat("<query>insert into ", _g.d.gl_journal_project_list._table, " (", __fieldList, _g.d.gl_journal_project_list._project_code, ",", _g.d.gl_journal_project_list._project_name, ",",
                            _g.d.gl_journal_project_list._allocate_persent, ",", _g.d.gl_journal_project_list._allocate_amount, ",", _g.d.gl_journal_project_list._line_number, ",", _g.d.gl_journal_project_list._line_number_detail, ",",
                            _g.d.gl_journal_project_list._account_code, ") values (", __dataList, "\'", __getDetailData._code, "\',\'", __getDetailData._name, "\',", __getDetailData._persent.ToString(), ",", __getDetailData._amount.ToString(), ",",
                            __line_number.ToString(), ",", __line_number_detail.ToString(), ",\'", __accountCode, "\')</query>"));
                    }
                }
                // Allocate List
                for (int __loop = 0; __loop < __getExtraData._allocateList.Count; __loop++)
                {
                    int __line_number_detail = __loop + 1;
                    SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._allocateList[__loop];
                    if (__getDetailData._code.Length > 0)
                    {
                        __myQuery.Append(string.Concat("<query>insert into ", _g.d.gl_journal_allocate_list._table, " (", __fieldList, _g.d.gl_journal_allocate_list._allocate_code, ",", _g.d.gl_journal_allocate_list._allocate_name, ",",
                            _g.d.gl_journal_allocate_list._allocate_persent, ",", _g.d.gl_journal_allocate_list._allocate_amount, ",", _g.d.gl_journal_allocate_list._line_number, ",", _g.d.gl_journal_allocate_list._line_number_detail, ",",
                            _g.d.gl_journal_allocate_list._account_code, ") values (", __dataList, "\'", __getDetailData._code, "\',\'", __getDetailData._name, "\',", __getDetailData._persent.ToString(), ",", __getDetailData._amount.ToString(), ",",
                            __line_number.ToString(), ",", __line_number_detail.ToString(), ",\'", __accountCode, "\')</query>"));
                    }
                }
                // Job List
                for (int __loop = 0; __loop < __getExtraData._jobList.Count; __loop++)
                {
                    int __line_number_detail = __loop + 1;
                    SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._jobList[__loop];
                    if (__getDetailData._code.Length > 0)
                    {
                        __myQuery.Append(string.Concat("<query>insert into ", _g.d.gl_journal_job_list._table, " (", __fieldList, _g.d.gl_journal_job_list._job_code, ",", _g.d.gl_journal_job_list._job_name, ",",
                            _g.d.gl_journal_job_list._allocate_persent, ",", _g.d.gl_journal_job_list._allocate_amount, ",", _g.d.gl_journal_job_list._line_number, ",", _g.d.gl_journal_job_list._line_number_detail, ",",
                            _g.d.gl_journal_job_list._account_code, ") values (", __dataList, "\'", __getDetailData._code, "\',\'", __getDetailData._name, "\',", __getDetailData._persent.ToString(), ",", __getDetailData._amount.ToString(), ",",
                            __line_number.ToString(), ",", __line_number_detail.ToString(), ",\'", __accountCode, "\')</query>"));
                    }
                }
            }
            return __myQuery.ToString();
        }

        public string _loadDataQuery(string docNo)
        {
            return _loadDataQuery(docNo, "");
        }

        public string _loadDataQuery(string docNo, string extraWhere)
        {
            StringBuilder __myquery = new StringBuilder();
            // 1
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Concat("select * from ", _g.d.gl_journal._table, " where ", _g.d.gl_journal._doc_no, "=\'", docNo, "\'" + extraWhere)));
            // 2
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Concat("select *" +
                ", (" + _g.d.gl_journal_detail._branch_code + " || '" + "~" + "' || (select name_1 from erp_branch_list where erp_branch_list.code = gl_journal_detail.branch_code )) as " + this._columnBranchCodeTemp + " from ",
                _g.d.gl_journal_detail._table,
                " where ", _g.d.gl_journal._doc_no, "=\'", docNo, "\'" + extraWhere,
                " order by ", _g.d.gl_journal_detail._line_number + "," + _g.d.gl_journal_detail._debit_or_credit)));

            // 3 Side List
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Concat("select ", _g.d.gl_journal_side_list._side_code, ",", _g.d.gl_journal_side_list._side_name, ",", _g.d.gl_journal_side_list._allocate_persent, ",", _g.d.gl_journal_side_list._allocate_amount, ",", _g.d.gl_journal_side_list._line_number + ",", _g.d.gl_journal_side_list._line_number_detail,
                " from ", _g.d.gl_journal_side_list._table, " where ", _g.d.gl_journal_side_list._doc_no, "=\'", docNo, "\' " + extraWhere, " order by ", _g.d.gl_journal_side_list._line_number, ",", _g.d.gl_journal_side_list._line_number_detail)));
            // 4 Department List
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Concat("select ", _g.d.gl_journal_depart_list._department_code, ",", _g.d.gl_journal_depart_list._department_name, ",", _g.d.gl_journal_depart_list._allocate_persent, ",", _g.d.gl_journal_depart_list._allocate_amount, ",", _g.d.gl_journal_depart_list._line_number + ",", _g.d.gl_journal_depart_list._line_number_detail, " from ", _g.d.gl_journal_depart_list._table, " where ", _g.d.gl_journal_depart_list._doc_no, "=\'", docNo, "\' " + extraWhere, " order by ", _g.d.gl_journal_depart_list._line_number, ",", _g.d.gl_journal_depart_list._line_number_detail)));
            // 5 Project List
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Concat("select ", _g.d.gl_journal_project_list._project_code, ",", _g.d.gl_journal_project_list._project_name, ",", _g.d.gl_journal_project_list._allocate_persent, ",", _g.d.gl_journal_project_list._allocate_amount, ",", _g.d.gl_journal_project_list._line_number + ",", _g.d.gl_journal_project_list._line_number_detail, " from ", _g.d.gl_journal_project_list._table, " where ", _g.d.gl_journal_project_list._doc_no, "=\'", docNo, "\' " + extraWhere, " order by ", _g.d.gl_journal_project_list._line_number, ",", _g.d.gl_journal_project_list._line_number_detail)));
            // 6 Allocate List
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Concat("select ", _g.d.gl_journal_allocate_list._allocate_code, ",", _g.d.gl_journal_allocate_list._allocate_name, ",", _g.d.gl_journal_allocate_list._allocate_persent, ",", _g.d.gl_journal_allocate_list._allocate_amount, ",", _g.d.gl_journal_allocate_list._line_number + ",", _g.d.gl_journal_allocate_list._line_number_detail, " from ", _g.d.gl_journal_allocate_list._table, " where ", _g.d.gl_journal_allocate_list._doc_no, "=\'", docNo, "\' " + extraWhere, " order by ", _g.d.gl_journal_allocate_list._line_number, ",", _g.d.gl_journal_allocate_list._line_number_detail)));
            // 7 Job List
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Concat("select ", _g.d.gl_journal_job_list._job_code, ",", _g.d.gl_journal_job_list._job_name, ",", _g.d.gl_journal_job_list._allocate_persent, ",", _g.d.gl_journal_job_list._allocate_amount, ",", _g.d.gl_journal_job_list._line_number + ",", _g.d.gl_journal_job_list._line_number_detail, " from ", _g.d.gl_journal_job_list._table, " where ", _g.d.gl_journal_job_list._doc_no, "=\'", docNo, "\' " + extraWhere, " order by ", _g.d.gl_journal_job_list._line_number, ",", _g.d.gl_journal_job_list._line_number_detail)));
            return __myquery.ToString();
        }

        public void _loadDataExtra(MyLib._myGrid grid, ArrayList dataSet, int startTable)
        {
            // ดึง xxx list เข้าย่อยอีกที
            //
            for (int __row = 0; __row < grid._rowData.Count; __row++)
            {
                int __line_compare = (int)MyLib._myGlobal._decimalPhase(grid._cellGet(__row, _g.d.ic_trans_detail._line_number).ToString());
                SMLERPGLControl._glDetailExtraObject __getExtraData = new SMLERPGLControl._glDetailExtraObject();
                // 0=Side,1=Department,2=Project,3=Allocate,4=Job
                for (int __type = 0; __type < 5; __type++)
                {
                    string __line_number = "";
                    switch (__type)
                    {
                        case 0: __line_number = _g.d.gl_journal_side_list._line_number; break;
                        case 1: __line_number = _g.d.gl_journal_depart_list._line_number; break;
                        case 2: __line_number = _g.d.gl_journal_project_list._line_number; break;
                        case 3: __line_number = _g.d.gl_journal_allocate_list._line_number; break;
                        case 4: __line_number = _g.d.gl_journal_job_list._line_number; break;
                    }
                    try
                    {
                        DataSet __dataSet = (DataSet)dataSet[startTable + __type];
                        DataTable __tables = __dataSet.Tables[0];
                        if (__tables.Rows.Count > 0)
                        {
                            DataRow[] __getList = __tables.Select(__line_number + "=" + __line_compare.ToString());
                            for (int __loop = 0; __loop < __getList.Length; __loop++)
                            {
                                SMLERPGLControl._glDetailExtraDetailClass __getData = new SMLERPGLControl._glDetailExtraDetailClass();
                                __getData._code = __getList[__loop].ItemArray[0].ToString();
                                __getData._name = __getList[__loop].ItemArray[1].ToString();
                                __getData._persent = MyLib._myGlobal._decimalPhase(__getList[__loop].ItemArray[2].ToString());
                                __getData._amount = MyLib._myGlobal._decimalPhase(__getList[__loop].ItemArray[3].ToString());
                                switch (__type)
                                {
                                    case 0: __getExtraData._sideList.Add(__getData); ; break;
                                    case 1: __getExtraData._departmentList.Add(__getData); ; break;
                                    case 2: __getExtraData._projectList.Add(__getData); ; break;
                                    case 3: __getExtraData._allocateList.Add(__getData); ; break;
                                    case 4: __getExtraData._jobList.Add(__getData); ; break;
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (MyLib._myGlobal._isUserTest)
                            System.Diagnostics.Debugger.Break();
                    }
                }
                grid._cellUpdate(__row, _g.d.gl_journal_detail._dimension, __getExtraData, false);
            }
        }

        public void _loadData(SMLERPGLControl._journalScreen screenTop, bool forEdit, string docNo)
        {
            this._loadData(screenTop, forEdit, docNo, 0);
        }

        public void _loadData(SMLERPGLControl._journalScreen screenTop, bool forEdit, string docNo, int transFlag)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(_loadDataQuery(docNo, (transFlag > 0) ? " AND trans_flag = " + transFlag + " " : ""));
            __myquery.Append("</node>");
            ArrayList __getDataMain = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            screenTop._loadData(((DataSet)__getDataMain[0]).Tables[0]);
            _glDetailGrid._loadFromDataTable(((DataSet)__getDataMain[1]).Tables[0]);
            this._loadDataExtra(this._glDetailGrid, __getDataMain, 2);
            //
            screenTop._search(false);
            if (forEdit)
            {
                screenTop._focusFirst();
            }
            // ให้คำนวณยอดรวม
            _glDetailGrid._calcTotal(false);
            _glDetailGrid.Invalidate();
            //
        }

        private void _glDetailGrid__afterAddRow(object sender, int row)
        {
            // เพิ่ม Object เพื่อรองรับการทำงานต่อไป
            MyLib._myGrid __grid = (MyLib._myGrid)sender;
            __grid._cellUpdate(row, _g.d.gl_journal_detail._dimension, new _glDetailExtraObject(), false);

        }

        private void _glDetailGrid__mouseClickClip(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.Equals(_g.d.gl_journal_detail._dimension))
            {
                MyLib._myGrid __grid = (MyLib._myGrid)sender;
                string __accountCode = __grid._cellGet(e._row, _g.d.gl_journal_detail._account_code).ToString();
                if (__accountCode.Length > 0)
                {
                    __detailExtra = new _glDetailExtra(__accountCode);
                    _glDetailExtraObject __getObject = (_glDetailExtraObject)__grid._cellGet(e._row, _g.d.gl_journal_detail._dimension);
                    __detailExtra._glDetailExtraTopScreen._clear();
                    __detailExtra._glDetailExtraSideGridData._clear();
                    __detailExtra._glDetailExtraAllocateGridData._clear();
                    __detailExtra._glDetailExtraDepartmentGridData._clear();
                    __detailExtra._glDetailExtraJobGridData._clear();
                    __detailExtra._glDetailExtraProjectGridData._clear();
                    for (int __row = 0; __row < __getObject._sideList.Count; __row++)
                    {
                        int __addr = __detailExtra._glDetailExtraSideGridData._addRow();
                        __detailExtra._glDetailExtraSideGridData._addData(__addr, (_glDetailExtraDetailClass)__getObject._sideList[__row]);
                    }
                    for (int __row = 0; __row < __getObject._allocateList.Count; __row++)
                    {
                        int __addr = __detailExtra._glDetailExtraAllocateGridData._addRow();
                        __detailExtra._glDetailExtraAllocateGridData._addData(__addr, (_glDetailExtraDetailClass)__getObject._allocateList[__row]);
                    }
                    for (int __row = 0; __row < __getObject._departmentList.Count; __row++)
                    {
                        int __addr = __detailExtra._glDetailExtraDepartmentGridData._addRow();
                        __detailExtra._glDetailExtraDepartmentGridData._addData(__addr, (_glDetailExtraDetailClass)__getObject._departmentList[__row]);
                    }
                    for (int __row = 0; __row < __getObject._jobList.Count; __row++)
                    {
                        int __addr = __detailExtra._glDetailExtraJobGridData._addRow();
                        __detailExtra._glDetailExtraJobGridData._addData(__addr, (_glDetailExtraDetailClass)__getObject._jobList[__row]);
                    }
                    for (int __row = 0; __row < __getObject._projectList.Count; __row++)
                    {
                        int __addr = __detailExtra._glDetailExtraProjectGridData._addRow();
                        __detailExtra._glDetailExtraProjectGridData._addData(__addr, (_glDetailExtraDetailClass)__getObject._projectList[__row]);
                    }
                    __detailExtra._buttonConfirm.Click += new EventHandler(_buttonConfirm_Click);
                    //
                    __detailExtra._glDetailExtraTopScreen._setDataStr(_g.d.gl_journal_detail._account_code, (string)this._glDetailGrid._cellGet(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._account_code));
                    __detailExtra._glDetailExtraTopScreen._setDataStr(_g.d.gl_journal_detail._account_name, (string)this._glDetailGrid._cellGet(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._account_name));
                    decimal __getAmount = (decimal)this._glDetailGrid._cellGet(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._debit);
                    if (__getAmount == 0)
                    {
                        __getAmount = (decimal)this._glDetailGrid._cellGet(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._credit);
                    }
                    __detailExtra._glDetailExtraTopScreen._setDataNumber(_g.d.gl_journal_detail._amount, __getAmount);
                    __detailExtra._glDetailExtraTopScreen.Invalidate();
                    __detailExtra._glDetailExtraSideGridData._total_amount = __getAmount;
                    __detailExtra._glDetailExtraAllocateGridData._total_amount = __getAmount;
                    __detailExtra._glDetailExtraDepartmentGridData._total_amount = __getAmount;
                    __detailExtra._glDetailExtraJobGridData._total_amount = __getAmount;
                    __detailExtra._glDetailExtraProjectGridData._total_amount = __getAmount;
                    //
                    __detailExtra.ShowDialog();
                }
            }
        }

        private void _buttonConfirm_Click(object sender, EventArgs e)
        {
            _glDetailExtraObject __getObject = (_glDetailExtraObject)this._glDetailGrid._cellGet(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._dimension);
            __getObject._sideList.Clear();
            __getObject._departmentList.Clear();
            __getObject._jobList.Clear();
            __getObject._projectList.Clear();
            __getObject._allocateList.Clear();
            __detailExtra._start();
            //
            for (int __row = 0; __row < __detailExtra._glDetailExtraSideGridData._rowData.Count; __row++)
            {
                __getObject._sideList.Add(__detailExtra._glDetailExtraSideGridData._getData(__row));
            }
            for (int __row = 0; __row < __detailExtra._glDetailExtraAllocateGridData._rowData.Count; __row++)
            {
                __getObject._allocateList.Add(__detailExtra._glDetailExtraAllocateGridData._getData(__row));
            }
            for (int __row = 0; __row < __detailExtra._glDetailExtraDepartmentGridData._rowData.Count; __row++)
            {
                __getObject._departmentList.Add(__detailExtra._glDetailExtraDepartmentGridData._getData(__row));
            }
            for (int __row = 0; __row < __detailExtra._glDetailExtraJobGridData._rowData.Count; __row++)
            {
                __getObject._jobList.Add(__detailExtra._glDetailExtraJobGridData._getData(__row));
            }
            for (int __row = 0; __row < __detailExtra._glDetailExtraProjectGridData._rowData.Count; __row++)
            {
                __getObject._projectList.Add(__detailExtra._glDetailExtraProjectGridData._getData(__row));
            }
            __detailExtra.Close();
        }

        private void _glDetailGrid_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(MyLib._myGrid)))
                {
                    MyLib._myGrid __sender = (MyLib._myGrid)e.Data.GetData(typeof(MyLib._myGrid));
                    for (int __loop = 0; __loop < __sender._rowData.Count; __loop++)
                    {
                        object __data = __sender._cellGet(__loop, 0);
                        if (__data != null && __data.GetType() == typeof(int) && (int)__data == 1)
                        {
                            int __newRow = this._glDetailGrid._addRow();
                            this._glDetailGrid._cellUpdate(__newRow, 0, __sender._cellGet(__loop, 1).ToString(), true);
                            __sender._cellUpdate(__loop, 0, 0, false);
                        }
                    }
                }
            }
            catch
            {
                if (MyLib._myGlobal._isUserTest)
                    System.Diagnostics.Debugger.Break();
            }
        }

        private void _glDetailGrid_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private MyLib._myGridMoveColumnType _glDetailGrid__moveNextColumn(MyLib._myGrid sender, int newRow, int newColumn)
        {
            MyLib._myGridMoveColumnType __result = new MyLib._myGridMoveColumnType();
            if (_autoTabCheckBox.Checked)
            {
                if (newColumn == 1)
                {
                    newColumn = 3;
                }
            }
            if (_glDetailGrid._cellGet(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._account_code).ToString().Length == 0)
            {
                newColumn = _glDetailGrid._findColumnByName(_g.d.gl_journal_detail._account_code);
            }
            __result._newColumn = newColumn;
            __result._newRow = newRow;
            return __result;
        }

        private void searchChartOfAccount__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            if (_searchName.Equals(_g.d.gl_journal_detail._branch_code))
            {
                _searchBranch.Close();
                _glDetailGrid._cellUpdate(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._branch_code, sender._cellGet(sender._selectRow, _g.d.erp_branch_list._table + "." + _g.d.erp_branch_list._code).ToString(), true);
                SendKeys.Send("{TAB}");

            }
            else
            {
                _searchChartOfAccount.Close();
                _glDetailGrid._cellUpdate(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._account_code, sender._cellGet(sender._selectRow, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code).ToString(), true);
                SendKeys.Send("{TAB}");
            }
        }

        private void _updateExtra(int row)
        {
            decimal __get_amount = (decimal)this._glDetailGrid._cellGet(row, _g.d.gl_journal_detail._debit);
            if (__get_amount == 0)
            {
                __get_amount = (decimal)this._glDetailGrid._cellGet(row, _g.d.gl_journal_detail._credit);
            }
            _glDetailExtraObject __getObject = (_glDetailExtraObject)this._glDetailGrid._cellGet(row, _g.d.gl_journal_detail._dimension);
            if (__getObject != null)
            {
                for (int __loop = 0; __loop < __getObject._sideList.Count; __loop++)
                {
                    _glDetailExtraDetailClass __getData = (_glDetailExtraDetailClass)__getObject._sideList[__loop];
                    __getData._amount = (__get_amount * __getData._persent) / 100.0M;
                    __getObject._sideList[__loop] = (_glDetailExtraDetailClass)__getData;
                }
                for (int __loop = 0; __loop < __getObject._departmentList.Count; __loop++)
                {
                    _glDetailExtraDetailClass __getData = (_glDetailExtraDetailClass)__getObject._departmentList[__loop];
                    __getData._amount = (__get_amount * __getData._persent) / 100.0M;
                    __getObject._departmentList[__loop] = (_glDetailExtraDetailClass)__getData;
                }
                for (int __loop = 0; __loop < __getObject._allocateList.Count; __loop++)
                {
                    _glDetailExtraDetailClass __getData = (_glDetailExtraDetailClass)__getObject._allocateList[__loop];
                    __getData._amount = (__get_amount * __getData._persent) / 100.0M;
                    __getObject._allocateList[__loop] = (_glDetailExtraDetailClass)__getData;
                }
                for (int __loop = 0; __loop < __getObject._projectList.Count; __loop++)
                {
                    _glDetailExtraDetailClass __getData = (_glDetailExtraDetailClass)__getObject._projectList[__loop];
                    __getData._amount = (__get_amount * __getData._persent) / 100.0M;
                    __getObject._projectList[__loop] = (_glDetailExtraDetailClass)__getData;
                }
                for (int __loop = 0; __loop < __getObject._jobList.Count; __loop++)
                {
                    _glDetailExtraDetailClass __getData = (_glDetailExtraDetailClass)__getObject._jobList[__loop];
                    __getData._amount = (__get_amount * __getData._persent) / 100.0M;
                    __getObject._jobList[__loop] = (_glDetailExtraDetailClass)__getData;
                }
                this._glDetailGrid._cellUpdate(row, _g.d.gl_journal_detail._dimension, __getObject, false);
            }
        }

        void _glDetailGrid__alterCellUpdate(object sender, int row, int column)
        {
            if (column == 0)
            {
                _searchAccount(row);
            }
            else if (column == this._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._branch_code))
            {
                _searchBranchList(row);
                SendKeys.Send("{TAB}");

            }

            decimal get_credit = 0m;
            try
            {
                get_credit = (decimal)_glDetailGrid._cellGet(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._credit);
            }
            catch
            {
                //System.Diagnostics.Debugger.Break();
            }
            decimal get_debit = 0m;
            try
            {
                get_debit = (decimal)_glDetailGrid._cellGet(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._debit);
            }
            catch
            {
                //System.Diagnostics.Debugger.Break();
            }
            if (this._glDetailGrid._selectColumn == this._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._credit))
            {
                _clearAmount();
                _glDetailGrid._cellUpdate(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._credit, get_credit, false);
                _updateExtra(this._glDetailGrid._selectRow);
            }
            else
                if (this._glDetailGrid._selectColumn == this._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._debit))
            {
                _clearAmount();
                _glDetailGrid._cellUpdate(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._debit, get_debit, false);
                _updateExtra(this._glDetailGrid._selectRow);
            }
            this.Invalidate();
        }

        private void _glDetailGrid_Invalidated(object sender, InvalidateEventArgs e)
        {
            int __field_credit_column_number = this._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._credit);
            int __field_debit_column_number = this._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._debit);
            _glDetailGrid._calcTotal(false);
            decimal __get_total_credit = ((MyLib._myGrid._columnType)this._glDetailGrid._columnList[__field_credit_column_number])._total;
            decimal __get_total_debit = ((MyLib._myGrid._columnType)this._glDetailGrid._columnList[__field_debit_column_number])._total;
            _total_amount._point = 2;
            _total_amount._double = __get_total_debit - __get_total_credit;
            _total_amount._refresh();
        }

        private void _glDetailGrid_Load(object sender, EventArgs e)
        {
        }

        private MyLib.QueryForInsertPerRowType _glDetailGrid__queryForInsertPerRow(object sender, int row)
        {
            MyLib.QueryForInsertPerRowType __result = new MyLib.QueryForInsertPerRowType();
            __result._field = "line_number";
            __result._data = row.ToString();
            return (__result);
        }

        private bool _glDetailGrid__queryForInsertCheck(object sender, int row)
        {
            return ((((string)_glDetailGrid._cellGet(row, _g.d.gl_journal_detail._account_code)).Length == 0) ? false : true);
        }

        private bool _glDetailGrid__totalCheck(object sender, int row, int column)
        {
            bool _result = true;
            if (((string)this._glDetailGrid._cellGet(row, 0)).ToString().Length == 0)
            {
                _result = false;
            }
            return (_result);
        }

        private void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (_searchName == this._columnBranchCodeTemp)
            {
                _searchBranch.Close();
                _glDetailGrid._cellUpdate(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._branch_code, e._text, true);

            }
            else
            {
                _searchChartOfAccount.Close();
                _glDetailGrid._cellUpdate(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._account_code, e._text, true);
            }
        }

        private void _clearAmount()
        {
            if (this._glDetailGrid._selectRow != -1)
            {
                _glDetailGrid._cellUpdate(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._debit, 0.0M, false);
                _glDetailGrid._cellUpdate(this._glDetailGrid._selectRow, _g.d.gl_journal_detail._credit, 0.0M, false);
            }
        }

        void _searchBranchList(int row)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __getAccountCode = this._glDetailGrid._cellGet(row, _g.d.gl_journal_detail._branch_code).ToString();
            string __query = "select " + _g.d.erp_branch_list._name_1 + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + "=\'" + MyLib._myUtil._convertTextToXml(__getAccountCode) + "\'";
            try
            {
                DataSet __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__dataResult.Tables[0].Rows.Count > 0)
                {

                    string getData = __dataResult.Tables[0].Rows[0][0].ToString();
                    _glDetailGrid._cellUpdate(row, this._columnBranchCodeTemp, string.Concat(__getAccountCode, "~", getData), true);
                }
                else
                {
                    if (__getAccountCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบรายการ ") + " : " + __getAccountCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    _glDetailGrid._cellUpdate(row, _g.d.gl_journal_detail._branch_code, "", false);
                }
            }
            catch
            {
                if (MyLib._myGlobal._isUserTest)
                    System.Diagnostics.Debugger.Break();
            }

        }

        private void _searchAccount(int row)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __getAccountCode = this._glDetailGrid._cellGet(row, _g.d.gl_journal_detail._account_code).ToString();
            string __query = "select " + _g.d.gl_chart_of_account._name_1 + "," + _g.d.gl_chart_of_account._status + "," + _g.d.gl_chart_of_account._balance_mode + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._code + "=\'" + MyLib._myUtil._convertTextToXml(__getAccountCode) + "\'";
            try
            {
                DataSet __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__dataResult.Tables[0].Rows.Count > 0)
                {
                    string __getStatus = __dataResult.Tables[0].Rows[0][1].ToString();
                    if (__getStatus.Length == 0 || __getStatus.Equals("0"))
                    {
                        string getData = __dataResult.Tables[0].Rows[0][0].ToString();
                        _glDetailGrid._cellUpdate(row, _g.d.gl_journal_detail._account_name, getData, false);
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("บัญชีนี้ไม่สามารถบันทึกรายการได้"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        _glDetailGrid._cellUpdate(row, _g.d.gl_journal_detail._account_code, "", false);
                        _clearAmount();
                    }
                }
                else
                {
                    if (__getAccountCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ ผังบัญชี") + " : " + __getAccountCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    _glDetailGrid._cellUpdate(row, _g.d.gl_journal_detail._account_code, "", false);
                    _clearAmount();
                }
            }
            catch
            {
                if (MyLib._myGlobal._isUserTest)
                    System.Diagnostics.Debugger.Break();
            }
        }

        string _searchName = "";

        private void _glDetailGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.CompareTo(_g.d.gl_journal_detail._account_code) == 0)
            {
                _searchChartOfAccount.Text = e._columnName;
                _searchName = e._columnName;
                _searchChartOfAccount.ShowDialog();
            }
            else if (e._columnName.CompareTo(this._columnBranchCodeTemp) == 0)
            {
                _searchBranch.Text = e._columnName;
                _searchName = e._columnName;
                _searchBranch.StartPosition = FormStartPosition.CenterScreen;
                _searchBranch.ShowDialog();

            }
        }

        public void _chartOfAccountScreenShow()
        {
            if (_chartOfAccountScreen.Visible == false)
            {
                _chartOfAccountScreen.Show(this);
            }
        }
    }

    public class _glDetailExtraObject
    {
        public ArrayList _sideList = new ArrayList();
        public ArrayList _departmentList = new ArrayList();
        public ArrayList _projectList = new ArrayList();
        public ArrayList _allocateList = new ArrayList();
        public ArrayList _jobList = new ArrayList();
    }

    public class _glDetailExtraDetailClass
    {
        public string _code;
        public string _name;
        public decimal _persent;
        public decimal _amount;
    }
}
