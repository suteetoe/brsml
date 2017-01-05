using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl._coupon
{
    public partial class _couponGenerate : UserControl
    {
        public _couponGenerateControl _coupon = new _couponGenerateControl();

        public _couponGenerate()
        {
            InitializeComponent();

            this._coupon.Dock = DockStyle.Fill;
            this._myManageData._form2.Controls.Add(this._coupon);

            _myManageData._dataList._buttonDelete.Enabled = false;
            _myManageData._dataList._lockRecord = false; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData._dataList._loadViewFormat("screen_coupon_list", MyLib._myGlobal._userSearchScreenGroup, true);
            int __columnIndex = this._myManageData._dataList._gridData._findColumnByName(_g.d.coupon_list._table + "." + _g.d.coupon_list._last_status);
            ((MyLib._myGrid._columnType)this._myManageData._dataList._gridData._columnList[__columnIndex])._isColumnFilter = false;


            _myManageData._dataList._referFieldAdd(_g.d.coupon_list._number, 1);
            _myManageData._manageButton = this._coupon._myToolBar;
            //_myManageData._manageBackgroundPanel = this._coupon._myPanel;
            _myManageData._closeScreen += _myManageData__closeScreen;
            _myManageData._newDataClick += _myManageData__newDataClick;
            _myManageData._clearData += _myManageData__clearData;
            //_myManageData._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);

            _coupon._saveButton.Click += _saveButton_Click;
            _coupon._closeButton.Click += _closeButton_Click;
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _saveData()
        {
            // packquery insert โลด
            string __getEmtry = this._coupon._screenTop._checkEmtryField();
            if (__getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, __getEmtry);
            }
            else
            {
                ArrayList __getDataField = this._coupon._screenTop._createQueryForDatabase();

                string __fieldList = __getDataField[0].ToString() + ",";
                string __valueList = __getDataField[1].ToString() + ",";

                this._coupon._couponGenGrid._updateRowIsChangeAll(true);
                StringBuilder __myQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(this._coupon._couponGenGrid._createQueryForInsert(_g.d.coupon_list._table, __fieldList, __valueList));
                __myQuery.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    // pack coupon and process balance
                    StringBuilder __couponNumberList = new StringBuilder();
                    for (int __i = 0; __i < this._coupon._couponGenGrid._rowData.Count; __i++)
                    {
                        if (__couponNumberList.Length > 0)
                            __couponNumberList.Append(",");

                        __couponNumberList.Append(this._coupon._couponGenGrid._cellGet(__i, _g.d.coupon_list._number).ToString());
                    }

                    SMLProcess._posProcess __processPos = new SMLProcess._posProcess();
                    __processPos._numberList = __couponNumberList.ToString();
                    __processPos._processCoupon();

                    _myManageData._afterInsertData();

                    MyLib._myGlobal._displayWarning(1, null);
                    this._coupon._screenTop._isChange = false;

                    this._clearScreen();
                    this._coupon._screenTop._focusFirst();
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

        void _clearScreen()
        {
            this._coupon._screenTop._clear();
            this._coupon._couponGenGrid._clear();
            this._coupon._amountTextbox.Text = "";
            this._coupon._formatTextbox.Text = "";

        }

        void _myManageData__clearData()
        {
            this._clearScreen();
        }

        void _myManageData__newDataClick()
        {
            this._clearScreen();
        }

        void _myManageData__closeScreen()
        {
            this.Dispose();
        }
    }
}
