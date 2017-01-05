using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL
{
    public partial class GenDataForm : Form
    {
        public GenDataForm()
        {
            InitializeComponent();
        }

        private void _buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime __date = this._dateTimePicker.Value;
                Random __random = new Random();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __chart = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 +
                    " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._status + "=0");
                int __count = 0;
                for (int __dayCount = 0; __dayCount < MyLib._myGlobal._intPhase(this._textBoxDays.Text); __dayCount++)
                {
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    for (int __loop = 1; __loop <= MyLib._myGlobal._intPhase(this._textBoxPerDay.Text); __loop++)
                    {
                        MyLib._myGlobal._guid = "SMLX";
                        double __amountDebit1 = (__random.Next(100000) + 1) + ((double)__random.Next(99) / 10.0);
                        double __amountDebit2 = (__random.Next(100000) + 1) + ((double)__random.Next(99) / 10.0);
                        double __amount = __amountDebit1 + __amountDebit2;
                        // Head
                        string __docDate = String.Format("{0}-{1}-{2}", __date.Year, __date.Month, __date.Day);
                        string __docNo = String.Format("{0}-{1}-1", __docDate, __loop);
                        string __desc = String.Format("รายการสร้างอัตโนมัติ {0} เพื่อทดสอบความเร็วในการทำงาน", __docNo);
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format("insert into {0} ({1},{2},{3},{4},{5},{6},{7}) values (\'{8}\',\'{9}\',\'{10}\',\'{11}\',{12},{13},{14})",
                            _g.d.gl_journal._table,
                            _g.d.gl_journal._doc_date, _g.d.gl_journal._book_code, _g.d.gl_journal._doc_no, _g.d.gl_journal._description, _g.d.gl_journal._debit, _g.d.gl_journal._credit, _g.d.gl_journal._period_number,
                            __docDate, "03", __docNo, __desc, __amount, __amount, __date.Month)));
                        // Details
                        int __addr1 = __random.Next(__chart.Tables[0].Rows.Count);
                        int __addr2 = __random.Next(__chart.Tables[0].Rows.Count);
                        int __addr3 = __random.Next(__chart.Tables[0].Rows.Count);
                        //
                        string __accountCode1 = __chart.Tables[0].Rows[__addr1].ItemArray[0].ToString();
                        string __accountName1 = __chart.Tables[0].Rows[__addr1].ItemArray[1].ToString();
                        string __accountCode2 = __chart.Tables[0].Rows[__addr2].ItemArray[0].ToString();
                        string __accountName2 = __chart.Tables[0].Rows[__addr2].ItemArray[1].ToString();
                        string __accountCode3 = __chart.Tables[0].Rows[__addr3].ItemArray[0].ToString();
                        string __accountName3 = __chart.Tables[0].Rows[__addr3].ItemArray[1].ToString();
                        //
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format("insert into {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10}) values (\'{11}\',\'{12}\',\'{13}\',\'{14}\',\'{15}\',{16},{17},{18},{19},0)",
                            _g.d.gl_journal_detail._table,
                            _g.d.gl_journal_detail._doc_date, _g.d.gl_journal_detail._book_code, _g.d.gl_journal_detail._doc_no, _g.d.gl_journal_detail._account_code, _g.d.gl_journal_detail._account_name, _g.d.gl_journal_detail._credit, _g.d.gl_journal_detail._debit, _g.d.gl_journal_detail._period_number, _g.d.gl_journal_detail._debit_or_credit, _g.d.gl_journal_detail._journal_type,
                            __docDate, "03", __docNo, __accountCode1, __accountName1, 0, __amountDebit1, __date.Month, 0)));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format("insert into {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10}) values (\'{11}\',\'{12}\',\'{13}\',\'{14}\',\'{15}\',{16},{17},{18},{19},0)",
                            _g.d.gl_journal_detail._table,
                            _g.d.gl_journal_detail._doc_date, _g.d.gl_journal_detail._book_code, _g.d.gl_journal_detail._doc_no, _g.d.gl_journal_detail._account_code, _g.d.gl_journal_detail._account_name, _g.d.gl_journal_detail._credit, _g.d.gl_journal_detail._debit, _g.d.gl_journal_detail._period_number, _g.d.gl_journal_detail._debit_or_credit, _g.d.gl_journal_detail._journal_type,
                            __docDate, "03", __docNo, __accountCode2, __accountName2, 0, __amountDebit2, __date.Month, 0)));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format("insert into {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10}) values (\'{11}\',\'{12}\',\'{13}\',\'{14}\',\'{15}\',{16},{17},{18},{19},0)",
                            _g.d.gl_journal_detail._table,
                            _g.d.gl_journal_detail._doc_date, _g.d.gl_journal_detail._book_code, _g.d.gl_journal_detail._doc_no, _g.d.gl_journal_detail._account_code, _g.d.gl_journal_detail._account_name, _g.d.gl_journal_detail._credit, _g.d.gl_journal_detail._debit, _g.d.gl_journal_detail._period_number, _g.d.gl_journal_detail._debit_or_credit, _g.d.gl_journal_detail._journal_type,
                            __docDate, "03", __docNo, __accountCode3, __accountName3, __amount, 0, __date.Month, 1)));
                        this._textBoxDate.Text = __date.ToShortDateString();
                        this._textBoxDate.Refresh();
                        this._textBoxCount.Text = __docNo;
                        this._textBoxCount.Refresh();
                        if (++__count == 201)
                        {
                            __count = 0;
                            __myQuery.Append("</node>");
                            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                            if (__result.Length > 0)
                            {
                                MessageBox.Show(__result);
                            }
                            //
                            __myQuery = new StringBuilder();
                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        }
                    }
                    __myQuery.Append("</node>");
                    if (__count > 0)
                    {
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length > 0)
                        {
                            MessageBox.Show(__result);
                        }
                    }
                    __date = __date.AddDays(1);
                    
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }
}
