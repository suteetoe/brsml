using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace SMLERPConfig._accountPeriod
{
    public partial class _accountPeriod : MyLib._myForm
    {
        ArrayList _grid = new ArrayList();
        public _accountPeriod()
        {
            InitializeComponent();
        }

        private void _accountPeriod_Load(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __dataDetail = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_account_period._table);
            _myGrid1._table_name = _g.d.erp_account_period._table;
            _myGrid1._addColumn(_g.d.erp_account_period._period_number, 2, 0, 10, false, false, true);
            _myGrid1._addColumn(_g.d.erp_account_period._date_start, 4, 0, 25);
            _myGrid1._addColumn(_g.d.erp_account_period._date_end, 4, 0, 25);
            _myGrid1._addColumn(_g.d.erp_account_period._period_year, 2, 0, 20);
            _myGrid1._addColumn(_g.d.erp_account_period._status, 10, 0, 20);
            _myGrid1.Dock = DockStyle.Fill;
            _myGrid1._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(temp__cellComboBoxItem);
            _myGrid1._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(temp__cellComboBoxGet);
            _myGrid1._afterAddRow += new MyLib.AfterAddRowEventHandler(temp__afterAddRow);
            // load
            _myGrid1._loadFromDataTable(__dataDetail.Tables[0], __dataDetail.Tables[0].Select());
        }

        void temp__afterAddRow(object sender, int row)
        {
            _myGrid1._cellUpdate(row, 0, row + 1, false);
        }

        Object[] termStatus = new Object[] { "Open", "Close" };
        string temp__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            return (termStatus[(select == -1) ? 0 : select].ToString());
        }

        object[] temp__cellComboBoxItem(object sender, int row, int column)
        {
            return (termStatus);
        }

        void getDialog__callBack(int selectYear)
        {
        }

        private void _buttonAuto_Click(object sender, EventArgs e)
        {
            _accountPeriodSetTermAuto getDialog = new _accountPeriodSetTermAuto();
            getDialog._callBack += new _accountPeriodSetTermAuto.CallBackHandler(getDialog__callBack);
            getDialog.ShowDialog(this);
        }

        void getDialog__callBack(DateTime dateBegin, int numOfPeriod)
        {
            _myGrid1._clear();
            int __month = dateBegin.Month;
            int __year = dateBegin.Year;
            for (int __loop = 0; __loop < numOfPeriod; __loop++)
            {
                _myGrid1._addRow();
                _myGrid1._cellUpdate(__loop, 1, new DateTime(__year, __month, 1), true);
                DateTime __dateEnd = new DateTime(1000, 1, 1);
                int __day = 31;
                bool __loopCheck = true;
                do
                {
                    try
                    {
                        __dateEnd = new DateTime(__year, __month, __day);
                        __loopCheck = false;
                    }
                    catch (Exception)
                    {
                        __day--;
                    }
                } while (__loopCheck);
                _myGrid1._cellUpdate(__loop, 2, __dateEnd, true);
                _myGrid1._cellUpdate(__loop, 3, 0, true);
                _myGrid1._cellUpdate(__loop, _g.d.erp_account_period._period_year, (new DateTime(__year, __month, 1)).Year + MyLib._myGlobal._year_add, true);
                if (++__month == 13)
                {
                    __month = 1;
                    __year++;
                }
            }
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            _myGrid1._updateRowIsChangeAll(true);
            string __myQuery = MyLib._myGlobal._xmlHeader + "<node>";
            __myQuery += "<query>delete from " + _g.d.erp_account_period._table + "</query>";
            __myQuery += _myGrid1._createQueryForInsert(_g.d.erp_account_period._table, "", "");
            __myQuery += "</node>";
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery);
            if (result.Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้"), MyLib._myGlobal._resource("สำเร็จ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show(result,MyLib._myGlobal._resource("ล้มเหลว") , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}