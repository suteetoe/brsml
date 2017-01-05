using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace SMLERPGL._display
{
    public class _selectAccountAndPeriod : MyLib._myScreen
    {
        _g._searchChartOfAccountDialog _searchChartOfAccount = new _g._searchChartOfAccountDialog();
        string _searchName = "";
        TextBox _searchTextBox;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        _selectAccountAndPeriodType _screenType;
        SMLERPControl._selectCode _selectCodeSide;
        SMLERPControl._selectCode _selectCodeDepartment;
        SMLERPControl._selectCode _selectCodeAllocate;
        SMLERPControl._selectCode _selectCodeJob;
        SMLERPControl._selectCode _selectCodeProject;

        MyLib._searchDataFull _searchFull = new MyLib._searchDataFull();

        public _selectAccountAndPeriod()
        {
            _createScreen();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_selectAccountAndPeriod__textBoxSearch);
            _searchChartOfAccount._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchChartOfAccount._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchChartOfAccount__searchEnterKeyPress);

            _searchFull._name = _g.g._search_screen_account_period;
            _searchFull._dataList._loadViewFormat(_searchFull._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchFull._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchFull._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchChartOfAccount__searchEnterKeyPress);

        }

        void _createScreen()
        {
            this.AutoSize = true;
            this._table_name = _g.d.gl_resource._table;
            switch (_screenType)
            {
                case _selectAccountAndPeriodType.ผ่านรายการ_ยกเลิก:
                    {
                        this._addTextBox(0, 0, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.gl_resource._date_begin, 1, true, false);
                        this._addDateBox(0, 2, 1, 0, _g.d.gl_resource._date_end, 1, true, false);
                        this._addTextBox(1, 0, 1, 0, _g.d.gl_resource._book_select, 1, 10, 1, true, false, true);
                        this._addTextBox(1, 1, 1, 0, _g.d.gl_resource._doc_begin, 1, 10, 0, true, false, true);
                        this._addTextBox(1, 2, 1, 0, _g.d.gl_resource._doc_end, 1, 10, 0, true, false, true);
                    }
                    break;
                case _selectAccountAndPeriodType.ผ่านรายการ:
                    {
                        this._addTextBox(0, 0, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.gl_resource._date_begin, 1, true, false);
                        this._addDateBox(0, 2, 1, 0, _g.d.gl_resource._date_end, 1, true, false);
                        this._addTextBox(1, 0, 1, 0, _g.d.gl_resource._book_select, 1, 10, 1, true, false, true);
                        this._addTextBox(1, 1, 1, 0, _g.d.gl_resource._doc_begin, 1, 10, 0, true, false, true);
                        this._addTextBox(1, 2, 1, 0, _g.d.gl_resource._doc_end, 1, 10, 0, true, false, true);
                    }
                    break;
                case _selectAccountAndPeriodType.แยกประเภท:
                    {
                        this._maxColumn = 3;
                        this._addTextBox(0, 0, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.gl_resource._date_begin, 1, true, false);
                        this._addDateBox(0, 2, 1, 0, _g.d.gl_resource._date_end, 1, true, false);
                        this._addTextBox(1, 0, 1, 0, _g.d.gl_resource._account_code, 1, 10, 1, true, false, false);
                        this._addCheckBox(1, 1, _g.d.gl_resource._is_pass, false, true,true);
                    }
                    break;
                case _selectAccountAndPeriodType.แยกประเภท_สรุป:
                    {
                        this._maxColumn = 3;
                        this._addCheckBox(0, 0, _g.d.gl_resource._all_data_check, false, true);
                        this._addCheckBox(0, 1, _g.d.gl_resource._debit_credit, false, true,true);
                        this._addCheckBox(0, 2, _g.d.gl_resource._is_pass, false, true, true);
                        this._addTextBox(1, 0, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                        this._addDateBox(1, 1, 1, 0, _g.d.gl_resource._date_begin, 2, true, false);
                        this._addDateBox(1, 2, 1, 0, _g.d.gl_resource._date_end, 2, true, false);
                    }
                    break;
                case _selectAccountAndPeriodType.งบทดลอง:
                    {
                        this._maxColumn = 4;
                        this._addCheckBox(0, 0, _g.d.gl_resource._all_data_check, false, true);
                        this._addCheckBox(0, 1, _g.d.gl_resource._debit_credit, false, true);
                        this._addCheckBox(0, 2, _g.d.gl_resource._select_account_adj, false, true,true);
                        this._addCheckBox(0, 3, _g.d.gl_resource._is_pass, false, true, true);
                        this._addTextBox(1, 0, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                        this._addDateBox(1, 1, 1, 0, _g.d.gl_resource._date_begin, 2, true, false);
                        this._addDateBox(1, 2, 1, 0, _g.d.gl_resource._date_end, 2, true, false);
                    }
                    break;
                case _selectAccountAndPeriodType.กระดาษทำการ:
                    {
                        this._maxColumn = 4;
                        this._addCheckBox(0, 0, _g.d.gl_resource._all_data_check, false, true);
                        this._addCheckBox(0, 1, _g.d.gl_resource._debit_credit, false, true,true);
                        this._addCheckBox(0, 2, _g.d.gl_resource._select_account_adj, false, true, true);
                        this._addCheckBox(0, 3, _g.d.gl_resource._is_pass, false, true, true);
                        this._addTextBox(1, 0, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                        this._addDateBox(1, 1, 1, 0, _g.d.gl_resource._date_begin, 2, true, false);
                        this._addDateBox(1, 2, 1, 0, _g.d.gl_resource._date_end, 2, true, false);
                    }
                    break;
                case _selectAccountAndPeriodType.งบทดลอง_มิติ:
                    {
                        _selectCodeSide = new SMLERPControl._selectCode(_g.d.erp_side_list._table);
                        _selectCodeDepartment = new SMLERPControl._selectCode(_g.d.erp_department_list._table);
                        _selectCodeAllocate = new SMLERPControl._selectCode(_g.d.erp_allocate_list._table);
                        _selectCodeJob = new SMLERPControl._selectCode(_g.d.erp_job_list._table);
                        _selectCodeProject = new SMLERPControl._selectCode(_g.d.erp_project_list._table);
                        //
                        this._maxColumn = 3;
                        this._addCheckBox(0, 0, _g.d.gl_resource._all_data_check, false, true);
                        this._addCheckBox(0, 1, _g.d.gl_resource._debit_credit, false, true,true);
                        this._addCheckBox(0, 2, _g.d.gl_resource._is_pass, false, true, true);
                        this._addTextBox(1, 0, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                        this._addDateBox(1, 1, 1, 0, _g.d.gl_resource._date_begin, 1, true, false);
                        this._addDateBox(1, 2, 1, 0, _g.d.gl_resource._date_end, 1, true, false);
                        MyLib._myGroupBox __groupBox = this._addGroupBox(2, 0, 1, 5, 5, _g.d.gl_resource._check, false);
                        this._addRadioButtonOnGroupBox(0, 0, __groupBox, _g.d.gl_resource._side, _g.d.gl_resource._side, true);
                        this._addRadioButtonOnGroupBox(0, 1, __groupBox, _g.d.gl_resource._department, _g.d.gl_resource._department, false);
                        this._addRadioButtonOnGroupBox(0, 2, __groupBox, _g.d.gl_resource._allocate, _g.d.gl_resource._allocate, false);
                        this._addRadioButtonOnGroupBox(0, 3, __groupBox, _g.d.gl_resource._project, _g.d.gl_resource._project, false);
                        this._addRadioButtonOnGroupBox(0, 4, __groupBox, _g.d.gl_resource._job, _g.d.gl_resource._job, false);
                        MyLib._myButton __selectCode = this._addButton(4, 0, 5, _g.d.gl_resource._select_code);
                        __selectCode.Click += new EventHandler(__selectCode_Click);
                    }
                    break;
            }
            try
            {
                int __period = _g.g._accountPeriodFind(MyLib._myGlobal._workingDate) - 1;
                this._setDataDate(_g.d.gl_resource._date_begin, _g.g._accountPeriodDateBegin[__period]);
                this._setDataDate(_g.d.gl_resource._date_end, _g.g._accountPeriodDateEnd[__period]);
            }
            catch
            {
            }
            this.Invalidate();
        }

        void __selectCode_Click(object sender, EventArgs e)
        {
            string __getValue = this._getDataStr(_g.d.gl_resource._check);
            if (__getValue.Equals(_g.d.gl_resource._side))
            {
                _selectCodeSide.ShowDialog();
            }
            else
                if (__getValue.Equals(_g.d.gl_resource._department))
                {
                    _selectCodeDepartment.ShowDialog();
                }
                else
                    if (__getValue.Equals(_g.d.gl_resource._allocate))
                    {
                        _selectCodeAllocate.ShowDialog();
                    }
                    else
                        if (__getValue.Equals(_g.d.gl_resource._project))
                        {
                            _selectCodeProject.ShowDialog();
                        }
                        else
                            if (__getValue.Equals(_g.d.gl_resource._job))
                            {
                                _selectCodeJob.ShowDialog();
                            }
        }

        public _selectAccountAndPeriodType ScreenType
        {
            get
            {
                return _screenType;
            }
            set
            {
                _screenType = value;
                this.SuspendLayout();
                this.Controls.Clear();
                _createScreen();
                this.ResumeLayout(false);
                this.PerformLayout();
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, e._row);
        }

        void _searchChartOfAccount__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        /// <summary>
        /// กดปุ่ม Enter ในหน้าจอค้นหา
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="row"></param>
        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        /// <summary>
        /// กด Mouse ตอนค้นหา หรือ Enter ตอนค้นหา
        /// </summary>
        /// <param name="name"></param>
        /// <param name="row"></param>
        void _searchAll(string name, int row)
        {
            if (name.Equals(_g.g._search_screen_gl_chart_of_account))
            {
                string __result = (string)_searchChartOfAccount._dataList._gridData._cellGet(row, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code);
                if (__result.Length > 0)
                {
                    _searchChartOfAccount.Close();
                    this._setDataStr(_searchName, __result, "", true);
                    _search(true);
                }
            }
            else if (name.Equals("screen_account_period"))
            {
                string __result = (string)_searchFull._dataList._gridData._cellGet(row, _g.d.erp_account_period._table + "." + _g.d.erp_account_period._period_number);
                if (__result.Length > 0)
                {
                    _searchFull.Close();
                    DateTime _startDate = MyLib._myGlobal._convertDateFromQuery(_searchFull._dataList._gridData._cellGet(row, _g.d.erp_account_period._table + "." + _g.d.erp_account_period._date_start).ToString());
                    DateTime _endDate = MyLib._myGlobal._convertDateFromQuery(_searchFull._dataList._gridData._cellGet(row, _g.d.erp_account_period._table + "." + _g.d.erp_account_period._date_end).ToString()); 

                    this._setDataStr(_searchName, __result, "", true);
                    this._setDataDate(_g.d.gl_resource._date_begin, _startDate);
                    this._setDataDate(_g.d.gl_resource._date_end, _endDate);

                    _search(true);
                }
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._code + "=\'" + this._getDataStr(_g.d.gl_resource._account_code) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.gl_resource._account_code, (DataSet)_getData[0], warning);
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, getDataStr, getData, true);
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && this._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        //_searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

        void _selectAccountAndPeriod__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.Equals(_g.d.gl_resource._account_code))
            {
                _searchName = name;
                _searchTextBox = ((MyLib._myTextBox)sender).textBox;
                _searchChartOfAccount.Text = label_name;
                _searchChartOfAccount.ShowDialog();
            }
            else if (name.Equals(_g.d.gl_resource._period))
            {
                _searchName = name;
                _searchFull.Text = label_name;
                _searchFull.ShowDialog();
            }
        }
    }

    public enum _selectAccountAndPeriodType
    {
        แยกประเภท,
        แยกประเภท_สรุป,
        งบทดลอง,
        กระดาษทำการ,
        งบทดลอง_มิติ,
        ผ่านรายการ,
        ผ่านรายการ_ยกเลิก
    }
}
