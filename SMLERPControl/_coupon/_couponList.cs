using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._coupon
{
    public partial class _couponList : UserControl
    {
        public _couponListControl _coupon = new _couponListControl();

        public _couponList()
        {
            InitializeComponent();
            //
            this._coupon.Dock = DockStyle.Fill;
            this._myManageData1._form2.Controls.Add(this._coupon);
            //
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_coupon_list", MyLib._myGlobal._userSearchScreenGroup, true);

            int __columnIndex = this._myManageData1._dataList._gridData._findColumnByName(_g.d.coupon_list._table + "." + _g.d.coupon_list._last_status);
            ((MyLib._myGrid._columnType)this._myManageData1._dataList._gridData._columnList[__columnIndex])._isColumnFilter = false;

            _myManageData1._dataList._referFieldAdd(_g.d.coupon_list._number, 1);
            _myManageData1._manageButton = this._coupon._toolStrip;
            _myManageData1._manageBackgroundPanel = this._coupon._myPanel;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._coupon._saveButton.Click += new EventHandler(_saveButton_Click);
            this._coupon._closeButton.Click += new EventHandler(_closeButton_Click);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 500;
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _saveData()
        {
            string __getEmtry = this._coupon._screen._checkEmtryField();
            if (__getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, __getEmtry);
            }
            else
            {
                ArrayList __getData = this._coupon._screen._createQueryForDatabase();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                if (_myManageData1._mode == 1)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                }
                else
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + __getData[2].ToString() + _myManageData1._dataList._whereString));
                }
                //
                __myQuery.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    string __number = this._coupon._screen._getDataStr(_g.d.coupon_list._number).ToString().Trim();
                    SMLProcess._posProcess __processPos = new SMLProcess._posProcess();
                    __processPos._processCoupon(__number);
                    //
                    MyLib._myGlobal._displayWarning(1, null);
                    this._coupon._screen._isChange = false;
                    if (_myManageData1._mode == 1)
                    {
                        _myManageData1._afterInsertData();
                    }
                    else
                    {
                        _myManageData1._afterUpdateData();
                    }
                    this._coupon._screen._clear();
                    this._coupon._screen._focusFirst();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            this._saveData();
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, __getData.whereString));
            } // for
            __myQuery.Append("</node>");
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myManageData1._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _myManageData1__clearData()
        {
            this._coupon._screen._clear();
            Control __codeControl = this._coupon._screen._getControl(_g.d.coupon_list._number);
            __codeControl.Enabled = true;
        }

        void _myManageData1__newDataClick()
        {
            this._coupon._screen._clear();
            this._coupon._movementGrid._clear();
            Control __codeControl = this._coupon._screen._getControl(_g.d.coupon_list._number);
            __codeControl.Enabled = true;
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData1__discardData()
        {
            bool __result = true;
            if (this._coupon._screen._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    __result = false;
                }
                else
                {
                    this._coupon._screen._isChange = false;
                }
            }
            return (__result);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                //DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + MyLib._myGlobal._fieldAndComma(_g.d.coupon_list._number, _g.d.coupon_list._amount, _g.d.coupon_list._date, _g.d.coupon_list._date_expire, _g.d.coupon_list._coupon_type, _g.d.coupon_list._remark, "case when"  + as _g.d.coupon_list._balance_amount) + " from " + _myManageData1._dataList._tableName + whereString);
                DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString);
                this._coupon._screen._loadData(__getData.Tables[0]);
                //
                string __number = this._coupon._screen._getDataStr(_g.d.coupon_list._number).Trim();
                DataSet __getDataMovement = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._trans_number + "=\'" + __number + "\' order by " + _g.d.cb_trans_detail._doc_date + "," + _g.d.cb_trans_detail._doc_time);
                this._coupon._movementGrid._loadFromDataTable(__getDataMovement.Tables[0]);
                decimal __balance = MyLib._myGlobal._decimalPhase(this._coupon._screen._getDataStr(_g.d.coupon_list._amount).Trim());
                int __type = MyLib._myGlobal._intPhase(this._coupon._screen._getDataStr(_g.d.coupon_list._coupon_type).Trim());



                for (int __row = 0; __row < this._coupon._movementGrid._rowData.Count; __row++)
                {
                    int __lastStatus = (int)MyLib._myGlobal._decimalPhase(this._coupon._movementGrid._cellGet(__row, _g.d.cb_trans_detail._last_status).ToString());
                    if (__lastStatus == 0)
                    {
                        decimal __amount = MyLib._myGlobal._decimalPhase(this._coupon._movementGrid._cellGet(__row, _g.d.cb_trans_detail._amount).ToString());
                        __balance -= __amount;
                    }
                    else
                    {
                        string __desc = this._coupon._movementGrid._cellGet(__row, _g.d.cb_trans_detail._description).ToString();
                        this._coupon._movementGrid._cellUpdate(__row, _g.d.cb_trans_detail._description, "*" + __desc, false);
                    }
                    if (__type == 1)
                    {
                        this._coupon._movementGrid._cellUpdate(__row, _g.d.coupon_list._balance_amount, 0M, false);
                    }
                    else
                    {
                        this._coupon._movementGrid._cellUpdate(__row, _g.d.coupon_list._balance_amount, __balance, false);
                    }
                }
                this._coupon.Invalidate();

                //
                Control __codeControl = this._coupon._screen._getControl(_g.d.coupon_list._number);
                __codeControl.Enabled = false;
                this._coupon._screen._isChange = false;
                if (forEdit)
                {
                    this._coupon._screen._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }
    }
}
