using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl.erp_user
{
    public partial class _erp_userDetailpicture : UserControl
    {
        string _oldCode = "";
       
        public _erp_userDetailpicture()
        {
            InitializeComponent();
            
            this._myManageDetail._displayMode = 0;
            this._myManageDetail._dataList._lockRecord = true;
            this._myManageDetail._selectDisplayMode(this._myManageDetail._displayMode);
            this._myManageDetail._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
            //this._myManageDetail._dataList._loadViewFormat("screen_ic_inventory", MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDetail._dataList._loadViewFormat("screen_erp_user", MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDetail._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            // this._myManageDetail._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageDetail._manageButton = this._myToolBar;
            this._myManageDetail._manageBackgroundPanel = this._myPanel1;
            this._myManageDetail._newDataClick += new MyLib.NewDataEvent(_myManageDetail__newDataClick);
            this._myManageDetail._discardData += new MyLib.DiscardDataEvent(_myManageDetail__discardData);
            this._myManageDetail._clearData += new MyLib.ClearDataEvent(_myManageDetail__clearData);
            this._myManageDetail._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageDetail._dataList._referFieldAdd(_g.d.erp_user._code, 1);
            this._myManageDetail._dataList._loadViewData(0);
            this._myManageDetail._checkEditData += new MyLib.CheckEditDataEvent(_myManageDetail__checkEditData);
            this._myManageDetail._calcArea();
            this._myManageDetail._dataListOpen = true;
            this._myManageDetail._autoSize = true;
            this._myManageDetail._autoSizeHeight = 450;
            _getPicture1._Tablename = _g.d.images_erp_user._table;
            _getPicture1._setEnable(false);

            this._myManageDetail.Invalidate();           
        }

        bool _myManageDetail__checkEditData(int row, MyLib._myGrid sender)
        {
            int __itemCodeColumnNumber = sender._findColumnByName(_g.d.erp_user._table + "." + _g.d.erp_user._code);
            string __Code = sender._cellGet(row, __itemCodeColumnNumber).ToString();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort("select is_lock_record from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + __Code + "\'").Tables[0];
            int is_lock_record = MyLib._myGlobal._intPhase(__getData.Rows[0][0].ToString());
            return (is_lock_record == 0) ? true : false;
        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }

        void _myManageDetail__clearData()
        {
            this._screenerp_user1._clear();
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _myManageDetail__newDataClick()
        {
            this._screenerp_user1._clear();
            this._myManageDetail._dataList._refreshData();
        }

        string _dataList__columnFieldNameReplace(string source)
        {
            return source.Replace("sml_value_branch_code", MyLib._myGlobal._branchCode);
        }

        bool _myManageDetail__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;
            try
            {
                string __query = "select * from " + this._myManageDetail._dataList._tableName + whereString;              
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();               
                DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                this._screenerp_user1._loadData(__getData.Tables[0]);
                this._screenerp_user1.Invalidate();
                this._screenerp_user1._focusFirst();              
                string _codepic_ = __getData.Tables[0].Rows [0][_g.d.erp_user._code].ToString();
                this._getPicture1._loadImage(_codepic_);

                if (_myToolBar.Enabled == false)
                {
                    this._getPicture1._setEnable(_myToolBar.Enabled);
                }
                else
                {
                    this._getPicture1._setEnable(_myToolBar.Enabled);
                }
                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }
        void _save_data()
        {
            // กำหนด Focus ใหม่ ไม่งั้นข้อมูลจะไม่สมบูรณ์
            string __result = "";
            if (_myManageDetail._manageButton.Enabled)
            {
                string __getEmtry = this._screenerp_user1._checkEmtryField();
                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    string _codepic = this._screenerp_user1._getDataStr(_g.d.ic_inventory._code);
                    string _codepic_ = _codepic.Replace("/", "").Trim();
                    if (_myManageDetail._mode == 1)
                    {

                    }
                    else
                    {
                        __result = this._getPicture1._updateImage(_codepic_);
                    }

                    if (__result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, null);
                        _screenerp_user1._isChange = false;
                        if (_myManageDetail._mode == 1)
                        {
                            _myManageDetail._afterInsertData();
                        }
                        else
                        {
                            _myManageDetail._afterUpdateData();
                        }
                        _screenerp_user1._clear();
                        _getPicture1._clearpic();
                        _getPicture1._setEnable(false);


                    }
                    else
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
      
        private void _saveButton_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        private void _close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
