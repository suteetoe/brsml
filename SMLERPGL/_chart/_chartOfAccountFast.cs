using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SMLERPGL._chart
{
    public partial class _chartOfAccountFast : UserControl
    {
        ArrayList _fieldAccountTypeName = new ArrayList();
        ArrayList _fieldAccountBalanceMode = new ArrayList();
        ArrayList _fieldAccountBalanceSheetEffect = new ArrayList();
        ArrayList _fieldAccountLevel = new ArrayList();
        MyLib._myFrameWork _frameWork = new MyLib._myFrameWork();

        public _chartOfAccountFast()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._accountGrid._addRowEnabled = false;
            this._accountGrid._table_name = _g.d.gl_chart_of_account._table;
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._code, 1, 0, 10, false, false, true);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._name_1, 1, 0, 14);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._name_2, 1, 0, 10);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._status, 11, 0, 5);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._account_level, 10, 0, 8);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._account_type, 10, 0, 8);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._balance_mode, 10, 0, 8);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._balance_sheet_status, 10, 0, 8);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._department_status, 11, 0, 5);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._job_status, 11, 0, 5);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._allocate_status, 11, 0, 5);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._side_status, 11, 0, 5);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._work_in_process_status, 11, 0, 5);
            this._accountGrid._addColumn(_g.d.gl_chart_of_account._active_status, 11, 0, 5);
            this._accountGrid._calcPersentWidthToScatter();
            this._accountGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_accountGrid__cellComboBoxItem);
            this._accountGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_accountGrid__cellComboBoxGet);
            this._accountGrid._queryForUpdateCheck += new MyLib.QueryForUpdateCheckEventHandler(_accountGrid__queryForUpdateCheck);
            this._accountGrid._queryForUpdateWhere += new MyLib.QueryForUpdateWhereEventHandler(_accountGrid__queryForUpdateWhere);
            this._accountGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_accountGrid__beforeDisplayRow);
            //
            for (int __loop = 0; __loop < _g.g._accountLevel.Length; __loop++)
            {
                string __fieldName = this._accountGrid._table_name + "." + _g.g._accountLevel[__loop];
                _fieldAccountLevel.Add(MyLib._myResource._findResource(__fieldName, __fieldName)._str);
            }            
            //
            for (int __loop = 0; __loop < _g.g._accountType.Length; __loop++)
            {
                string __fieldName = this._accountGrid._table_name + "." + _g.g._accountType[__loop];
                _fieldAccountTypeName.Add(MyLib._myResource._findResource(__fieldName, __fieldName)._str);
            }
            _fieldAccountBalanceMode.Add(MyLib._myResource._findResource(this._accountGrid._table_name + "." + _g.g._accountBalanceMode[0], this._accountGrid._table_name + "." + _g.g._accountBalanceMode[0])._str);
            _fieldAccountBalanceMode.Add(MyLib._myResource._findResource(this._accountGrid._table_name + "." + _g.g._accountBalanceMode[1], this._accountGrid._table_name + "." + _g.g._accountBalanceMode[1])._str);
            for (int __loop = 0; __loop < _g.g._accountBalanceSheetEffect.Length; __loop++)
            {
                string __fieldName = this._accountGrid._table_name + "." + _g.g._accountBalanceSheetEffect[__loop];
                _fieldAccountBalanceSheetEffect.Add(MyLib._myResource._findResource(__fieldName, __fieldName)._str);
            }
            //
            this.Load += new EventHandler(_chartOfAccountFast_Load);
            this.Disposed += new EventHandler(_chartOfAccountFast_Disposed);
        }

        MyLib.BeforeDisplayRowReturn _accountGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            return (_g.g._chartOfAccountBeforeDisplay(sender, columnNumber, columnName, senderRow, columnType, rowData));
        }

        void _chartOfAccountFast_Disposed(object sender, EventArgs e)
        {
            _frameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.gl_chart_of_account._table + " set guid_code=null");
            _chartOfAccountProcessFlow __process = new _chartOfAccountProcessFlow();
            __process.ShowDialog();
        }

        string _accountGrid__queryForUpdateWhere(MyLib._myGrid sender, int row)
        {
            return MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=" + "\'" + sender._cellGet(row, _g.d.gl_chart_of_account._code.ToUpper()) + "\'";
        }

        bool _accountGrid__queryForUpdateCheck(MyLib._myGrid sender, int row)
        {
            bool __result = false;
            if ((bool)sender._cellGet(row, sender._columnList.Count))
            {
                __result = true;
            }
            return __result;
        }

        void _chartOfAccountFast_Load(object sender, EventArgs e)
        {
            _frameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.gl_chart_of_account._table + " set guid_code=\'" + MyLib._myGlobal._guid.ToUpper() + "\'");
            StringBuilder __query = new StringBuilder();
            __query.Append("select ");
            for (int __loop = 0; __loop < this._accountGrid._columnList.Count; __loop++)
            {
                MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)this._accountGrid._columnList[__loop];
                if (__loop != 0)
                {
                    __query.Append(",");
                }
                __query.Append(__getColumn._originalName);
            }
            __query.Append(" from " + _g.d.gl_chart_of_account._table + " order by code");
            DataSet __getData = _frameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
            this._accountGrid._loadFromDataTable(__getData.Tables[0]);
        }

        string _accountGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            string __result = "";
            if (columnName.Equals(_g.d.gl_chart_of_account._account_level))
            {
                __result = _fieldAccountLevel[select].ToString();
            }
            else
                if (columnName.Equals(_g.d.gl_chart_of_account._account_type))
                {
                    __result = _fieldAccountTypeName[select].ToString();
                }
                else
                    if (columnName.Equals(_g.d.gl_chart_of_account._balance_mode))
                    {
                        __result = _fieldAccountBalanceMode[select].ToString();
                    }
                    else
                        if (columnName.Equals(_g.d.gl_chart_of_account._balance_sheet_status))
                        {
                            __result = _fieldAccountBalanceSheetEffect[select].ToString();
                        }
            return __result;
        }

        object[] _accountGrid__cellComboBoxItem(object sender, int row, int column)
        {
            string __getColumnName = ((MyLib._myGrid._columnType)this._accountGrid._columnList[column])._originalName;
            if (__getColumnName.Equals(_g.d.gl_chart_of_account._account_level))
            {
                return (object[])_fieldAccountLevel.ToArray();
            }
            else
                if (__getColumnName.Equals(_g.d.gl_chart_of_account._account_type))
                {
                    return (object[])_fieldAccountTypeName.ToArray();
                }
                else
                    if (__getColumnName.Equals(_g.d.gl_chart_of_account._balance_mode))
                    {
                        return (object[])_fieldAccountBalanceMode.ToArray();
                    }
                    else
                        if (__getColumnName.Equals(_g.d.gl_chart_of_account._balance_sheet_status))
                        {
                            return (object[])_fieldAccountBalanceSheetEffect.ToArray();
                        }
            return (null);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                if (keyData == Keys.F12)
                {
                    _saveData();
                    return true;
                }
                if (keyData == Keys.Escape)
                {
                    _closeScreen();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _closeScreen()
        {
            bool __closeOk = false;
            _accountGrid.Focus();
            if (_accountGrid._isChange)
            {
                if (MessageBox.Show(MyLib._myGlobal._resource("มีการแก้ไขข้อมูลบางส่วนแล้ว ท่านต้องการปิดหน้าจอจริงหรือไม่"), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    __closeOk = true;
                }
            }
            else
            {
                __closeOk = true;
            }
            if (__closeOk)
            {
                this.Dispose();
            }
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            _closeScreen();
        }

        void _saveData()
        {
            this._accountGrid.Focus();
            string __query = _accountGrid._createQueryForUpdate(_g.d.gl_chart_of_account._table, "");
            if (__query.Length > 0)
            {
                __query = "<node>" + __query + "</node>";
                string __result = _frameWork._queryList(MyLib._myGlobal._databaseName, __query);
                if (__result.Length == 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ปรับปรุงข้อมูลสำเร็จ"));
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ปรับปรุงข้อมูลไม่สำเร็จ")+" : " + __result);
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ไม่มีรายการแก้ไข"));
                this.Dispose();
            }
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            _saveData();
        }
    }
}
