using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLPOSControl._food
{
    public partial class _chefListControl : UserControl
    {
        string _oldCode = "";

        public _chefListControl()
        {
            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData._dataList._loadViewFormat("screen_chef_master", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData._dataList._referFieldAdd(_g.d.chef_master._chef_code, 1);
            _myManageData._manageButton = this._myToolbar;
            //_myManageData._manageBackgroundPanel = this._myPanel;
            _myManageData._loadDataToScreen += _myManageData__loadDataToScreen;
            _myManageData._discardData += _myManageData__discardData;
            _myManageData._closeScreen += _myManageData__closeScreen;
            _myManageData._newDataClick += _myManageData__newDataClick;
            _myManageData._clearData += _myManageData__clearData;
            _myManageData._dataList._deleteData += _dataList__deleteData;

            _myManageData._dataListOpen = true;
            _myManageData._calcArea();
            _myManageData._autoSize = true;
            _myManageData._autoSizeHeight = 500;


            // screentop
            this._screenTop._table_name = _g.d.chef_master._table;
            this._screenTop._addTextBox(0, 0, 1, 1, _g.d.chef_master._chef_code, 1, 20, 0, true, false, false);
            this._screenTop._addTextBox(1, 0, _g.d.chef_master._chef_name, 100);
            this._screenTop._addCheckBox(2, 0, _g.d.chef_master._status, false, true);
            this._screenTop._setUpper(_g.d.chef_master._chef_code);

            // grid
            this._itemGrid._table_name = _g.d.chef_master_item._table;
            this._itemGrid._isEdit = false;
            this._itemGrid._addColumn("check", 11, 10, 10, false);
            this._itemGrid._addColumn(_g.d.chef_master_item._item_code, 1, 25, 20);
            this._itemGrid._addColumn(_g.d.chef_master_item._item_name, 1, 75, 70, false);
            this._itemGrid._queryForInsertCheck += _itemGrid__queryForInsertCheck;

        }

        bool _itemGrid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (this._itemGrid._cellGet(row, 0).ToString().Equals("1"))
                return true;
            return false;
        }

        bool _myManageData__discardData()
        {
            bool __result = true;
            if (this._screenTop._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    __result = false;
                }
                else
                {
                    this._screenTop._isChange = false;
                }
            }
            return (__result);
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData._dataList._tableName, getData.whereString));
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _g.d.chef_master_item._table, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            string result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myManageData._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _myManageData__clearData()
        {
            //throw new NotImplementedException();
            this._loadItem();
            this._oldCode = "";
            Control __codeControl = this._screenTop._getControl(_g.d.chef_master._chef_code);
            __codeControl.Enabled = true;

        }

        void _myManageData__newDataClick()
        {
            //throw new NotImplementedException();
            this._screenTop._clear();
            this._oldCode = "";
        }

        void _myManageData__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            try
            {
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _myManageData._dataList._tableName + whereString));

                string __chef_code = ((ArrayList)rowData)[1].ToString();
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select (case when (coalesce((select " + _g.d.chef_master_item._item_code + " from " + _g.d.chef_master_item._table + " where " + _g.d.chef_master_item._table + "." + _g.d.chef_master_item._item_code + " = " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.chef_master_item._table + "." + _g.d.chef_master_item._chef_code + " = '" + __chef_code + "' ), '')= '') then 0 else 1 end) as check, " + _g.d.ic_inventory._code + " as " + _g.d.chef_master_item._item_code + ", " + _g.d.ic_inventory._name_1 + " as " + _g.d.chef_master_item._item_name + " from " + _g.d.ic_inventory._table));


                __query.Append("</node>");

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                DataSet __getData1 = ((DataSet)__result[0]);
                DataSet __getData2 = ((DataSet)__result[1]);
                _screenTop._loadData(__getData1.Tables[0]);
                Control __codeControl = this._screenTop._getControl(_g.d.chef_master._chef_code);
                this._itemGrid._loadFromDataTable(__getData2.Tables[0]);
                this._oldCode = this._screenTop._getDataStr(_g.d.chef_master._chef_code);
                __codeControl.Enabled = false;
                if (forEdit)
                {
                    _screenTop._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
            //throw new NotImplementedException();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        void _saveData()
        {
            this._screenTop._saveLastControl();
            string __getEmtry = _screenTop._checkEmtryField();
            if (__getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, __getEmtry);
            }
            else
            {
                string __chef_code = this._screenTop._getDataStr(_g.d.chef_master._chef_code);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                ArrayList __getData = _screenTop._createQueryForDatabase();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                if (_myManageData._mode == 2)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _myManageData._dataList._tableName + " where " + _g.d.chef_master._chef_code + "='" + this._oldCode + "' "));
                }

                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData._dataList._tableName + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                //

                // pack detail
                this._itemGrid._updateRowIsChangeAll(true);
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.chef_master_item._table + " where " + _g.d.chef_master_item._chef_code + "='" + this._oldCode + "'"));
                __myQuery.Append(this._itemGrid._createQueryForInsert(_g.d.chef_master_item._table, _g.d.chef_master_item._chef_code + ",", "'" + __chef_code + "',"));

                __myQuery.Append("</node>");
                string result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    if (_myManageData._mode == 1)
                    {
                        _myManageData._afterInsertData();
                    }
                    else
                    {
                        _myManageData._afterUpdateData();
                    }
                    this._screenTop._clear();
                    this._screenTop._focusFirst();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void _loadItem()
        {
            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._code + " as " + _g.d.chef_master_item._item_code + ", " + _g.d.ic_inventory._name_1 + " as " + _g.d.chef_master_item._item_name + " from " + _g.d.ic_inventory._table));
            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
            if (__result.Count > 0 && ((DataSet)__result[0]).Tables.Count > 0)
            {
                DataTable __table = ((DataSet)__result[0]).Tables[0];
                this._itemGrid._loadFromDataTable(__table);
            }
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this._itemGrid._rowData.Count; __i++)
            {
                this._itemGrid._cellUpdate(__i, 0, 1, false);
            }
        }

        private void _disselectButton_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this._itemGrid._rowData.Count; __i++)
            {
                this._itemGrid._cellUpdate(__i, 0, 0, false);
            }

        }

    }
}
