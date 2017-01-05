using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGLControl
{
    public partial class _journalScreen_pic : MyLib._myScreen
    {
        
        string _searchName = "";
        TextBox _searchTextBox;
        public _journalScreen_pic()
        {
            InitializeComponent();
            this.AutoSize = true;
            this._maxColumn = 2;
            this._table_name = _g.d.gl_journal._table;
            this._addDateBox(0, 0, 1, 0, _g.d.gl_journal._doc_date, 1, true, false);
            this._addTextBox(0, 1, 1, 0, _g.d.gl_journal._doc_no, 1, 10, 0, true, false, false);
            this._addDateBox(1, 0, 1, 0, _g.d.gl_journal._ref_date, 1, true);
            this._addTextBox(1, 1, 1, 0, _g.d.gl_journal._ref_no, 1, 10, 0, true, false);
            this._addTextBox(2, 0, 1, 0, _g.d.gl_journal._book_code, 1, 10, 1, true, false, false);
            this._addTextBox(3, 0, 2, 0, _g.d.gl_journal._description, 2, 255, 0, true, false, true);
          //  this._afterClear += new MyLib.AfterClearHandler(_journalScreen__afterClear);
            this._clear();
            this.Invalidate();
        }
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาสมุดบัญชี
                string query = "select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + _g.d.gl_journal_book._code + "=\'" + MyLib._myUtil._convertTextToXml(this._getDataStr(_g.d.gl_journal._book_code)) + "\'";
                _searchAndWarning(_g.d.gl_journal._book_code, query, warning);
            }
            catch
            {
            }
        }
        bool _searchAndWarning(string fieldName, string query, Boolean warning)
        {
            bool __result = false;
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
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
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ")+" : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }
}
