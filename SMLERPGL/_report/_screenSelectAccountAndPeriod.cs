using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPGL._report
{
    public class _screenSelectAccountAndPeriod : MyLib._myScreen
    {
        _g._searchChartOfAccountDialog _searchChartOfAccount = new _g._searchChartOfAccountDialog();
        string _searchName = "";
        TextBox _searchTextBox;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        _selectAccountAndPeriodType _screenType = _selectAccountAndPeriodType.glList;

        public _screenSelectAccountAndPeriod()
        {
            this.AutoSize = true;
            this._table_name = _g.d.gl_resource._table;
            _createScreen();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_selectAccountAndPeriod__textBoxSearch);
            _searchChartOfAccount._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchChartOfAccount._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchChartOfAccount__searchEnterKeyPress);
        }

        void _createScreen()
        {
            if (_screenType == _selectAccountAndPeriodType.glList)
            {
                this._maxColumn = 4;
                this._addTextBox(0, 0, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                this._addDateBox(0, 1, 1, 0, _g.d.gl_resource._date_begin, 1, true, false);
                this._addDateBox(0, 2, 1, 0, _g.d.gl_resource._date_end, 1, true, false);
                this._addTextBox(0, 3, 1, 0, _g.d.gl_resource._account_code, 1, 10, 1, true, false, false);
            }
            else
                if (_screenType == _selectAccountAndPeriodType.glListSum)
                {
                    this._maxColumn = 7;
                    this._addCheckBox(0, 0, _g.d.gl_resource._all_data_check, false, true);
                    this._addCheckBox(0, 1, _g.d.gl_resource._debit_credit, false, true);
                    this._addTextBox(0, 2, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                    this._addDateBox(0, 3, 1, 0, _g.d.gl_resource._date_begin, 2, true, false);
                    this._addDateBox(0, 5, 1, 0, _g.d.gl_resource._date_end, 2, true, false);
                    this._setCheckBox(_g.d.gl_resource._debit_credit, "1");
                }
                else
                    if (_screenType == _selectAccountAndPeriodType.glTrialBalance)
                    {
                        this._maxColumn = 7;
                        this._addCheckBox(0, 0, _g.d.gl_resource._all_data_check, false, true);
                        this._addCheckBox(0, 1, _g.d.gl_resource._debit_credit, false, true);
                        this._addTextBox(0, 2, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                        this._addDateBox(0, 3, 1, 0, _g.d.gl_resource._date_begin, 2, true, false);
                        this._addDateBox(0, 5, 1, 0, _g.d.gl_resource._date_end, 2, true, false);
                        this._setCheckBox(_g.d.gl_resource._debit_credit, "1");
                    }
                    else
                        if (_screenType == _selectAccountAndPeriodType.glWorkSheet)
                        {
                            this._maxColumn = 7;
                            this._addCheckBox(0, 0, _g.d.gl_resource._all_data_check, false, true);
                            this._addCheckBox(0, 1, _g.d.gl_resource._debit_credit, false, true);
                            this._addTextBox(0, 2, 1, 0, _g.d.gl_resource._period, 1, 10, 1, true, false, true);
                            this._addDateBox(0, 3, 1, 0, _g.d.gl_resource._date_begin, 2, true, false);
                            this._addDateBox(0, 5, 1, 0, _g.d.gl_resource._date_end, 2, true, false);
                            this._setCheckBox(_g.d.gl_resource._debit_credit, "1");
                        }
            this.Invalidate();
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
        }
    }

    public enum _selectAccountAndPeriodType
    {
        glList,
        glListSum,
        glTrialBalance,
        glWorkSheet
    }
}
