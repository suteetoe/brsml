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
    public class _screenReportCondition : MyLib._myScreen
    {
        _g._searchChartOfAccountDialog _searchChartOfAccount = new _g._searchChartOfAccountDialog();
        private _screenJournalConditionType _screenTypeResult = _screenJournalConditionType.none;
        public bool _screenCreated = false;
        private string _searchName = "";
        private TextBox _searchTextBox;

        public _screenReportCondition()
        {
            _createScreen();
            _searchChartOfAccount._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchChartOfAccount._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchChartOfAccount__searchEnterKeyPress);
        }

        public _screenJournalConditionType _screenType
        {
            get
            {
                return _screenTypeResult;
            }
            set
            {
                _screenTypeResult = value;
                this.SuspendLayout();
                this.Controls.Clear();
                this._createScreen();
                this.ResumeLayout(false);
                this.PerformLayout();
            }
        }

        public void _createScreen()
        {
            int __line = 0;
            this.AutoSize = true;
            this._table_name = _g.d.gl_resource._table;
            if (_screenType != _screenJournalConditionType.none)
            {
                if (_screenCreated == false)
                {
                    _screenCreated = true;
                    this._maxColumn = 1;
                    if (_screenType == _screenJournalConditionType.Sheet)
                    {
                        this._addTextBox(__line++, 0, 0, 0, _g.d.gl_resource._account_code_begin, 1, 0, 1, true, false);
                        this._addTextBox(__line++, 0, 0, 0, _g.d.gl_resource._account_code_end, 1, 0, 1, true, false);
                        this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenJournalCondition__textBoxSearch);
                    }
                    if (_screenType == _screenJournalConditionType.Sheet || _screenType == _screenJournalConditionType.SheetSum || _screenType == _screenJournalConditionType.Journal || _screenType == _screenJournalConditionType.JournalSum || _screenType == _screenJournalConditionType.TrialBalance || _screenType == _screenJournalConditionType.WorkSheet || _screenType == _screenJournalConditionType.WorkSheetByBranch || _screenType == _screenJournalConditionType.WorkSheetByBranchDepart || _screenType == _screenJournalConditionType.SinghaReportProfitAndLost || _screenType == _screenJournalConditionType.SinghaReportBalance)
                    {
                        this._addDateBox(__line++, 0, 1, 0, _g.d.gl_resource._date_begin, 1, true, true);
                        this._addDateBox(__line++, 0, 1, 0, _g.d.gl_resource._date_end, 1, true, true);
                    }
                    if (_screenType == _screenJournalConditionType.Journal || _screenType == _screenJournalConditionType.JournalSum)
                    {
                        this._addTextBox(__line++, 0, _g.d.gl_resource._doc_begin, 0);
                        this._addTextBox(__line++, 0, _g.d.gl_resource._doc_end, 0);
                    }
                    if (_screenType == _screenJournalConditionType.Sheet || _screenType == _screenJournalConditionType.SheetSum || _screenType == _screenJournalConditionType.TrialBalance || _screenType == _screenJournalConditionType.WorkSheet || _screenType == _screenJournalConditionType.WorkSheetByBranch || _screenType == _screenJournalConditionType.WorkSheetByBranchDepart)
                    {
                        this._addCheckBox(__line++, 0, _g.d.gl_resource._select_all_account, true, false);
                    }
                    if (_screenType == _screenJournalConditionType.Sheet || _screenType == _screenJournalConditionType.Journal || _screenType == _screenJournalConditionType.JournalSum)
                    {
                        this._addCheckBox(__line++, 0, _g.d.gl_resource._total_end_date, true, false, true);
                    }
                    if (_screenType == _screenJournalConditionType.SheetSum || _screenType == _screenJournalConditionType.TrialBalance || _screenType == _screenJournalConditionType.WorkSheet || _screenType == _screenJournalConditionType.WorkSheetByBranch || _screenType == _screenJournalConditionType.WorkSheetByBranchDepart)
                    {
                        this._addCheckBox(__line++, 0, _g.d.gl_resource._select_account_balance, true, false, true);
                        this._addCheckBox(__line++, 0, _g.d.gl_resource._select_account_adj, true, false, true);
                    }
                    if (_screenType == _screenJournalConditionType.WorkSheetByBranch || _screenType == _screenJournalConditionType.WorkSheetByBranchDepart || _screenType == _screenJournalConditionType.SinghaReportProfitAndLost)
                    {
                        this._addCheckBox(__line++, 0, _g.d.gl_resource._period, true, false, false);
                    }
                    if (_screenType == _screenJournalConditionType.Journal || _screenType == _screenJournalConditionType.JournalSum)
                    {
                        MyLib._myGroupBox __group = this._addGroupBox(__line++, 0, 2, 1, 1, _g.d.gl_resource._sort_by, false);
                        this._addRadioButtonOnGroupBox(0, 0, __group, _g.d.gl_resource._sort_by_date, 1, true);
                        this._addRadioButtonOnGroupBox(1, 0, __group, _g.d.gl_resource._sort_by_doc_no, 2, false);
                    }
                    if (_screenType == _screenJournalConditionType.TrialBalance)
                    {
                        this._addCheckBox(__line++, 0, _g.d.gl_resource._total_end_date, true, false, false);
                    }
                    //
                    this._refresh();
                    this._focusFirst();
                }
            }
        }

        void _screenJournalCondition__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.Equals(_g.d.gl_resource._account_code_begin) || name.Equals(_g.d.gl_resource._account_code_end))
            {
                _searchName = name;
                _searchTextBox = ((MyLib._myTextBox)sender).textBox;
                _searchChartOfAccount.Text = label_name;
                _searchChartOfAccount.ShowDialog();
            }
        }

        void _searchChartOfAccount__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchTextBox.Text = (string)_searchChartOfAccount._dataList._gridData._cellGet(row, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code);
            _searchChartOfAccount.Close();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            _searchTextBox.Text = (string)_searchChartOfAccount._dataList._gridData._cellGet(e._row, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code);
            _searchChartOfAccount.Close();
        }
    }

    public enum _screenJournalConditionType
    {
        none,
        Journal,
        JournalSum,
        Sheet,
        SheetSum,
        TrialBalance,
        WorkSheet,
        WorkSheetByBranch,
        WorkSheetByBranchDepart,
        SinghaReportProfitAndLost,
        SinghaReportBalance
    }
}
