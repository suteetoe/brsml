using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL
{
    public partial class _journalPassUndo : UserControl
    {
        private SMLERPGL._display._selectAccountAndPeriod _conditionScreen = new _display._selectAccountAndPeriod();
        private MyLib._myGrid _dataGrid = new MyLib._myGrid();
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private string _check = "Check";
        private int _columnNumberDocNo = 0;
        private int _columnNumberCheck = 0;

        public _journalPassUndo()
        {
            InitializeComponent();
            this.Load += new EventHandler(_journalPass_Load);
        }

        void _journalPass_Load(object sender, EventArgs e)
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._dataGrid._table_name = _g.d.gl_journal._table;
            this._dataGrid._isEdit = false;
            this._dataGrid._addColumn(_g.d.gl_journal._book_code, 1, 10, 5);
            this._dataGrid._addColumn(_g.d.gl_journal._doc_date, 4, 10, 10);
            this._dataGrid._addColumn(_g.d.gl_journal._doc_no, 1, 10, 15);
            this._dataGrid._addColumn(_g.d.gl_journal._description, 1, 10, 50);
            this._dataGrid._addColumn(_g.d.gl_journal._amount, 3, 1, 10, true, false, true, false, __formatNumber);
            this._dataGrid._addColumn(this._check, 11, 10, 10);
            this._columnNumberCheck = this._dataGrid._findColumnByName(this._check);
            this._columnNumberDocNo = this._dataGrid._findColumnByName(_g.d.gl_journal._doc_no);
            //
            this._conditionScreen.ScreenType = _display._selectAccountAndPeriodType.ผ่านรายการ_ยกเลิก;
            this._conditionScreen.Dock = DockStyle.Fill;
            this._dataGrid.Dock = DockStyle.Fill;
            this._panelCondition.Controls.Add(this._conditionScreen);
            this._panelDetail.Controls.Add(this._dataGrid);
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            string __dateBegin = this._conditionScreen._getDataStrQuery(_g.d.gl_resource._date_begin);
            string __dateEnd = this._conditionScreen._getDataStrQuery(_g.d.gl_resource._date_end);
            string __docNoBegin = this._conditionScreen._getDataStr(_g.d.gl_resource._doc_begin);
            string __docNoEnd = this._conditionScreen._getDataStr(_g.d.gl_resource._doc_end);
            string __docNoWare = "";
            if (__docNoBegin.Length != 0 || __docNoEnd.Length != 0)
            {
                if (__docNoBegin.Length != 0 && __docNoEnd.Length == 0)
                {
                    // ป้อนเฉพาะเอกสารเริ่มต้น
                    __docNoWare = " and doc_no=\'" + __docNoBegin + "\'";
                }
                else
                    if (__docNoBegin.Length == 0 && __docNoEnd.Length != 0)
                    {
                        // ป้อนเฉพาะเอกสารสุดท้าย
                        __docNoWare = " and doc_no=\'" + __docNoEnd + "\'";
                    }
                    else
                        if (__docNoBegin.Length != 0 && __docNoEnd.Length != 0)
                        {
                            // ป้อนเอกสารเริ่มต้นและสิ้นสุด
                            __docNoWare = " and doc_no between \'" + __docNoBegin + "\' and \'" + __docNoEnd + "\'";
                        }
            }
            this._dataGrid._clear();
            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.gl_journal._doc_date, _g.d.gl_journal._doc_no, _g.d.gl_journal._book_code, _g.d.gl_journal._description, _g.d.gl_journal._is_pass, " case when " + _g.d.gl_journal._debit + "=0 then " + _g.d.gl_journal._credit + " else " + _g.d.gl_journal._debit + " end as " + _g.d.gl_journal._amount) + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._is_pass+"=1 and "+_g.d.gl_journal._doc_date + " between " + __dateBegin + " and " + __dateEnd + __docNoWare + " order by " + MyLib._myGlobal._fieldAndComma(_g.d.gl_journal._doc_date, _g.d.gl_journal._doc_no)));
            __query.Append("</node>");
            ArrayList __getData = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
            DataTable __journal = ((DataSet)__getData[0]).Tables[0];
            if (__journal.Rows.Count == 0)
            {
                MessageBox.Show("Data not found", "Error");
            }
            else
            {
                this._dataGrid._loadFromDataTable(__journal);
            }
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
            {
                this._dataGrid._cellUpdate(__row, this._check, 1, true);
            }
            this._dataGrid.Invalidate();
        }

        private void _removeAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
            {
                this._dataGrid._cellUpdate(__row, this._check, 0, false);
            }
            this._dataGrid.Invalidate();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _passButton_Click(object sender, EventArgs e)
        {
            DialogResult __result = MessageBox.Show("ต้องการยกเลิกผ่านรายการจริงหรือไม่", "Pass", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (__result == DialogResult.Yes)
            {
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
                {
                    int __value = (int)MyLib._myGlobal._decimalPhase(this._dataGrid._cellGet(__row, this._check).ToString());
                    if (__value == 1)
                    {
                        string __docNo = this._dataGrid._cellGet(__row, this._columnNumberDocNo).ToString();
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal._table + " set " + _g.d.gl_journal._is_pass + "=0 where " + _g.d.gl_journal._doc_no + "=\'" + __docNo + "\'"));
                    }
                }
                string __isPassQuery = "(select " + _g.d.gl_journal._is_pass + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_no + ")";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal_detail._is_pass + "=" + __isPassQuery + " where " +_g.d.gl_journal_detail._is_pass +" is null or "+ _g.d.gl_journal_detail._is_pass + "<>" + __isPassQuery));
                __query.Append("</node>");
                string __queryResult = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__queryResult.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                }
                else
                {
                    MessageBox.Show(__queryResult, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
