using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._chart
{
    public partial class _changeAccountCode : Form
    {
        public _changeAccountCode()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _buttonStart_Click(object sender, EventArgs e)
        {
            this._changeAccountCodeScreen1._saveLastControl();
            string __getOldCode = this._changeAccountCodeScreen1._getDataStr(_g.d.gl_resource._account_code);
            string __getNewCode = this._changeAccountCodeScreen1._getDataStr(_g.d.gl_resource._change_account_code_to);
            if (__getOldCode.ToLower().Equals(__getNewCode.ToLower()) || __getOldCode.Length == 0 || __getNewCode.Length == 0)
            {
                string __message = "รหัสไม่สมบูรณ์ หรือไม่ถูกต้อง";
                MessageBox.Show(__message);
            }
            else
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_chart_of_account._code + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + __getOldCode.ToUpper() + "\'");
                if (__getData.Tables[0].Rows.Count == 0)
                {
                    string __message = "ไม่พบผังบัญชี";
                    MessageBox.Show(__message);
                }
                else
                {
                    DataSet __getNewData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_chart_of_account._code + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + __getNewCode.ToUpper() + "\'");
                    if (__getNewData.Tables[0].Rows.Count != 0)
                    {
                        string __message = "รหัสที่ต้องการเปลี่ยน ซ้ำกับรหัสเดิม";
                        MessageBox.Show(__message);
                    }
                    else
                    {
                        string __message = "ต้องการเปลี่ยนรหัสจริงหรือไม่";
                        DialogResult __result = MessageBox.Show(__message, "Warning", MessageBoxButtons.YesNo);
                        if (__result == DialogResult.Yes)
                        {
                            StringBuilder __query = new StringBuilder();
                            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            // ข้อมูลหลัก
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account._table + " set " + _g.d.gl_chart_of_account._code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account_side_list._table + " set " + _g.d.gl_chart_of_account_side_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account_side_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account_depart_list._table + " set " + _g.d.gl_chart_of_account_depart_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account_depart_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account_job_list._table + " set " + _g.d.gl_chart_of_account_job_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account_job_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account_allocate_list._table + " set " + _g.d.gl_chart_of_account_allocate_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account_allocate_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account_project_list._table + " set " + _g.d.gl_chart_of_account_project_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account_project_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            // รายวัน
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal_detail._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_side_list._table + " set " + _g.d.gl_journal_side_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_side_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_depart_list._table + " set " + _g.d.gl_journal_depart_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_depart_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_allocate_list._table + " set " + _g.d.gl_journal_allocate_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_allocate_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_project_list._table + " set " + _g.d.gl_journal_project_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_project_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_job_list._table + " set " + _g.d.gl_journal_job_list._account_code + "=\'" + __getNewCode + "\' where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_job_list._account_code) + "=\'" + __getOldCode.ToUpper() + "\'"));
                            __query.Append("</node>");
                            string __queryResult = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                            if (__queryResult.Length == 0)
                            {
                                string __success = "เปลี่ยนรหัสสมบูรณ์";
                                MessageBox.Show(__success);
                            }
                            else
                            {
                                MessageBox.Show(__queryResult, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
    }

    public partial class _changeAccountCodeScreen : MyLib._myScreen
    {
        public _changeAccountCodeScreen()
        {
            this.AutoSize = true;
            this._table_name = _g.d.gl_resource._table;
            this._maxColumn = 1;
            this._addTextBox(0, 0, 1, 0, _g.d.gl_resource._account_code, 1, 10, 1, true, false, true);
            this._addTextBox(1, 0, 1, 0, _g.d.gl_resource._change_account_code_to, 1, 10, 0, true, false, true);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_changeAccountCodeScreen__textBoxSearch);
        }

        _g._searchChartOfAccountDialog _searchChartOfAccount;
        void _changeAccountCodeScreen__textBoxSearch(object sender)
        {
            _searchChartOfAccount = new _g._searchChartOfAccountDialog();
            _searchChartOfAccount.Text = ((MyLib._myTextBox)sender)._labelName;
            _searchChartOfAccount._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchChartOfAccount__searchEnterKeyPress);
            _searchChartOfAccount._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchChartOfAccount.ShowDialog();
        }

        void _search(MyLib._myGrid grid, int row)
        {
            this._setDataStr(_g.d.gl_resource._account_code, (string)grid._cellGet(row, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code));
            _searchChartOfAccount.Close();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            _search((MyLib._myGrid)sender, e._row);
        }

        void _searchChartOfAccount__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _search((MyLib._myGrid)sender, row);
        }
    }
}