using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPConfig
{
    public partial class _salePlanManage : UserControl
    {
        string _oldCode = "";

        public _salePlanManage()
        {

            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData._dataList._loadViewFormat("screen_sale_plan", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData._dataList._referFieldAdd(_g.d.erp_sale_plan._code, 1);
            _myManageData._manageButton = this._myToolbar;
            _myManageData._loadDataToScreen += _myManageData__loadDataToScreen;
            _myManageData._discardData += _myManageData__discardData;
            _myManageData._closeScreen += _myManageData__closeScreen;
            _myManageData._newDataClick += _myManageData__newDataClick;
            _myManageData._clearData += _myManageData__clearData;
            _myManageData._dataList._deleteData += _dataList__deleteData;

            //_myManageData._manageBackgroundPanel = this._myPanel;
            /*
            _myManageData._loadDataToScreen += _myManageData__loadDataToScreen;
           */

            _myManageData._dataListOpen = true;
            _myManageData._calcArea();
            _myManageData._autoSize = true;
            _myManageData._autoSizeHeight = 500;
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];

                string __getDocNo = this._myManageData._dataList._gridData._cellGet(getData.row, _g.d.erp_sale_plan._code).ToString();

                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData._dataList._tableName, getData.whereString));
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _g.d.erp_sale_plan_detail._table, " where " + _g.d.erp_sale_plan_detail._plan_code + "=\'" + __getDocNo + "\'"));
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
            this._oldCode = "";
            this._gridSale._clear();
            Control __codeControl = this._screenTop._getControl(_g.d.erp_sale_plan._code);
            __codeControl.Enabled = true;

        }

        void _myManageData__newDataClick()
        {
            this._screenTop._clear();
            this._oldCode = "";
        }

        void _myManageData__closeScreen()
        {
            this.Dispose();
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

        bool _myManageData__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            try
            {
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _myManageData._dataList._tableName + whereString));

                string __oldCode = ((ArrayList)rowData)[this._myManageData._dataList._gridData._findColumnByName(_g.d.erp_sale_plan._table + "." + _g.d.erp_sale_plan._code)].ToString();
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *" +
                    ", (select  " + _g.d.ar_sale_area._name_1 + " from " + _g.d.ar_sale_area._table + " where " + _g.d.ar_sale_area._table + "." + _g.d.ar_sale_area._code + "=" + _g.d.erp_sale_plan_detail._table + "." + _g.d.erp_sale_plan_detail._sale_area_code + " ) as " + _g.d.erp_sale_plan_detail._sale_area_name +
                    ", (select  " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.erp_sale_plan_detail._table + "." + _g.d.erp_sale_plan_detail._sale_code + " ) as " + _g.d.erp_sale_plan_detail._sale_name +
                    ", (select  " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.erp_sale_plan_detail._table + "." + _g.d.erp_sale_plan_detail._item_code + " ) as " + _g.d.erp_sale_plan_detail._item_name +
                    " from " + _g.d.erp_sale_plan_detail._table + " where " + _g.d.erp_sale_plan_detail._plan_code + "=\'" + __oldCode + "\'"));


                __query.Append("</node>");

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                DataSet __getData = ((DataSet)__result[0]);
                DataSet __getData2 = ((DataSet)__result[1]);
                _screenTop._loadData(__getData.Tables[0]);
                Control __codeControl = this._screenTop._getControl(_g.d.erp_sale_plan._code);
                this._gridSale._loadFromDataTable(__getData2.Tables[0]);
                this._oldCode = this._screenTop._getDataStr(_g.d.erp_sale_plan._code);
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
            return false;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._saveData();
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
                string __planCode = this._screenTop._getDataStr(_g.d.erp_sale_plan._code);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                ArrayList __getData = _screenTop._createQueryForDatabase();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                if (_myManageData._mode == 2)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _myManageData._dataList._tableName + " where " + _g.d.erp_sale_plan._code + "='" + this._oldCode + "' "));
                }

                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData._dataList._tableName + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                //

                // pack detail
                this._gridSale._updateRowIsChangeAll(true);
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_sale_plan_detail._table + " where " + _g.d.erp_sale_plan_detail._plan_code + "='" + this._oldCode + "'"));
                __myQuery.Append(this._gridSale._createQueryForInsert(_g.d.erp_sale_plan_detail._table, _g.d.erp_sale_plan_detail._plan_code + ",", "'" + __planCode + "',"));

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
                    this._gridSale._clear();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                this._saveData();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public class _sale_plan_screentop : MyLib._myScreen
    {
        public _sale_plan_screentop()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.erp_sale_plan._table;
            this._addTextBox(0, 0, 1, 0, _g.d.erp_sale_plan._code, 1, 25, 0, true, false, false);
            this._addTextBox(1, 0, _g.d.erp_sale_plan._name_1, 255);
            this._addDateBox(2, 0, 1, 1, _g.d.erp_sale_plan._begin_date, 20, true);
            this._addDateBox(2, 1, 1, 1, _g.d.erp_sale_plan._end_date, 20, true);
            this._addTextBox(3, 0, 3, 2, _g.d.erp_sale_plan._remark, 2, 25, 0, true, false, true);
            this._setUpper(_g.d.erp_sale_plan._code);
        }
    }

}
