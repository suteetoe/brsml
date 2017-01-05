using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLPosClient
{
    public partial class _posSaveMoneyReceive : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _posSaveMoneyReceive()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this.SuspendLayout();
            _myManageData1._dataList._buttonNew.Visible = false;
            _myManageData1._dataList._buttonNewFromTemp.Visible = false;

            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat(_g.g._search_screen_pos_save_send_money, MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._extraWhereEvent += new MyLib.ExtraWhereEventHandler(_dataList__extraWhereEvent);
            _myManageData1._dataList._referFieldAdd(_g.d.POSCashierSettle._DocNo, 1);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myScreen._saveKeyDown += new MyLib.SaveKeyDownHandler(_myScreen__saveKeyDown);
            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            //_myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;
            this._save.Click += new EventHandler(_save_Click);
            this._close.Click += new EventHandler(_close_Click);
            this.ResumeLayout();


        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString + " AND " + _g.d.POSCashierSettle._trans_type + "=1"));
            } // for
            __myQuery.Append("</node>");
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myManageData1._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void _myManageData1__clearData()
        {
            this._myScreen._clear();
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (this._myScreen._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._myScreen._isChange = false;
                    this._myScreen._clear();
                }
            }
            return (result);

        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                DataSet getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString + " AND " + _g.d.POSCashierSettle._trans_type + "=1");
                this._myScreen._loadData(getData.Tables[0]);
                Control codeControl = this._myScreen._getControl(_g.d.m_information._ic_code);
                codeControl.Enabled = false;
                //this._pos_save_send_money_screen1._search(false);
                this._myScreen._isChange = false;
                if (forEdit)
                {
                    this._myScreen._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);

        }

        string _dataList__extraWhereEvent()
        {
            return _g.d.POSCashierSettle._trans_type + "=1";
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _save_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        void _myScreen__saveKeyDown(object sender)
        {
            _save_data();
        }

        void _save_data()
        {
            string getEmtry = this._myScreen._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {

                ArrayList __getData = this._myScreen._createQueryForDatabase();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                if (_myManageData1._mode == 1)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData[0].ToString() + ", " + _g.d.POSCashierSettle._trans_type + ") values (" + __getData[1].ToString() + ", 1)"));
                }
                else
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + __getData[2].ToString() + _myManageData1._dataList._whereString + " AND " + _g.d.POSCashierSettle._trans_type + "=1"));
                }
                //
                __myQuery.Append("</node>");
                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    this._myScreen._isChange = false;
                    if (_myManageData1._mode == 1)
                    {
                        _myManageData1._afterInsertData();
                    }
                    else
                    {
                        _myManageData1._afterUpdateData();
                    }
                    this._myScreen._clear();
                    this._myScreen._focusFirst();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }

    public class _pos_save_money_receive_screen : MyLib._myScreen
    {
        public _pos_save_money_receive_screen()
        {
            this._table_name = _g.d.POSCashierSettle._table;
            this._maxColumn = 1;
            int __row = 0;
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._MACHINECODE, 0);
            this._addDateBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._DocDate, 1, true);
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._doc_time, 0);
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._DocNo, 0);
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._CashierCode, 0);
            this._addNumberBox(__row++, 0, 0, 0, _g.d.POSCashierSettle._CashAmount, 1, 2, true);
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._manager_code, 0);
            this._addTextBox(__row++, 0, 2, 0, _g.d.POSCashierSettle._remark, 2, 1, 0, true, false, true);

            this._enabedControl(_g.d.POSCashierSettle._MACHINECODE, false);
            this._enabedControl(_g.d.POSCashierSettle._CashierCode, false);
            this._enabedControl(_g.d.POSCashierSettle._DocDate, false);
            this._enabedControl(_g.d.POSCashierSettle._doc_time, false);
            this._enabedControl(_g.d.POSCashierSettle._DocNo, false);

        }
    }
}
