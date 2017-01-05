using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPIC
{
    public partial class _icSpecificSearch : UserControl
    {
        public _icSpecificSearch()
        {
            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this.SuspendLayout();
            this._myManageMain._autoSize = true;
            this._myManageMain._displayMode = 0;
            //this._myManageMain._readOnly = true;
            this._myManageMain._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            this._myManageMain._selectDisplayMode(this._myManageMain._displayMode);
            this._myManageMain._dataList._loadViewFormat("screen_ic_specific_search_word", MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageMain._dataList._referFieldAdd(_g.d.ic_specific_search_word._keyword, 1);
            this._myManageMain._manageButton = this._myToolBar;
            this._myManageMain._manageBackgroundPanel = this._myPanel1;
            this._myManageMain._autoSizeHeight = 450;
            this._myManageMain._dataListOpen = true;
            this._myManageMain._loadDataToScreen += _myManageMain__loadDataToScreen;
            this._myManageMain._closeScreen += _myManageMain__closeScreen;
            this._myManageMain._clearData += _myManageMain__clearData;
            this._myManageMain._dataList._deleteData += _dataList__deleteData;
            //this._myManageMain._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            //this._myManageMain._dataList._loadViewData(0);
            this.ResumeLayout(false);
            this._saveButton.Click += _saveButton_Click;

        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._saveData();
        }

        private void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {

        }

        private void _myManageMain__clearData()
        {
            this._screenTop._screenICSpecialSearchWord._clear();
            this._screenTop._gridPiston._clear();
            //this._screenTop._gridPistonRim._clear();
            //this._screenTop._gridOtherPart._clear();
        }

        private void _myManageMain__closeScreen()
        {
            this.Dispose();
        }

        private bool _myManageMain__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                string __keyWord = this._myManageMain._dataList._gridData._cellGet(this._myManageMain._dataList._gridData._selectRow, this._myManageMain._dataList._gridData._findColumnByName(_g.d.ic_specific_search_word._table + "." + _g.d.ic_specific_search_word._keyword)).ToString();

                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_specific_search_word._keyword + "," + _g.d.ic_specific_search_word._engine_valve + "," + _g.d.ic_specific_search_word._displacement + "," + _g.d.ic_specific_search_word._market_names + "," + _g.d.ic_specific_search_word._remark + " from " + _g.d.ic_specific_search_word._table + " where " + _g.d.ic_specific_search_word._keyword + " = \'" + __keyWord + "\'"));

                string __getPartQuery = "select " + _g.d.ic_specific_search._ic_code  + "," + _g.d.ic_specific_search._sort_order + ", (select name_1 from ic_inventory where ic_inventory.code = ic_specific_search.ic_code) as " + _g.d.ic_specific_search._ic_name + ", (select name_1 from ic_group where ic_group.code = (select " + _g.d.ic_inventory._group_main + " from ic_inventory where ic_inventory.code = ic_specific_search.ic_code)) as " + _g.d.ic_specific_search._group_code + "  from " + _g.d.ic_specific_search._table + " where " + _g.d.ic_specific_search._keyword + " = \'" + __keyWord + "\' order by line_number" ;
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__getPartQuery, "1")));
                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__getPartQuery, "2")));
                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__getPartQuery, "0")));

                __query.Append("</node>");

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Count > 0)
                {
                    this._screenTop._screenICSpecialSearchWord._loadData(((DataSet)__result[0]).Tables[0]);
                    this._screenTop._gridPiston._loadFromDataTable(((DataSet)__result[1]).Tables[0]);
                    //this._screenTop._gridPistonRim._loadFromDataTable(((DataSet)__result[2]).Tables[0]);
                    //this._screenTop._gridOtherPart._loadFromDataTable(((DataSet)__result[3]).Tables[0]);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        void _saveData()
        {
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            string __keyWord = this._screenTop._screenICSpecialSearchWord._getDataStr(_g.d.ic_specific_search_word._keyword);
            if (this._myManageMain._mode == 2)
            {
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_specific_search_word._table + " where " + _g.d.ic_specific_search_word._keyword + " = \'" + __keyWord + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_specific_search._table + " where " + _g.d.ic_specific_search._keyword + " = \'" + __keyWord + "\'"));

            }
            ArrayList __screenTop = this._screenTop._screenICSpecialSearchWord._createQueryForDatabase();

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_specific_search_word._table + "(" + __screenTop[0] + ") values (" + __screenTop[1] + ")"));

            this._screenTop._gridPiston._updateRowIsChangeAll(true);
            //this._screenTop._gridPistonRim._updateRowIsChangeAll(true);
            //this._screenTop._gridOtherPart._updateRowIsChangeAll(true);
            __query.Append(this._screenTop._gridPiston._createQueryForInsert(_g.d.ic_specific_search._table, _g.d.ic_specific_search._keyword + "," + _g.d.ic_specific_search._group_type + ",", "\'" + __keyWord + "\',1,"));
            //__query.Append(this._screenTop._gridPistonRim._createQueryForInsert(_g.d.ic_specific_search._table, _g.d.ic_specific_search._keyword + "," + _g.d.ic_specific_search._group_type + ",", "\'" + __keyWord + "\',2,"));
            //__query.Append(this._screenTop._gridOtherPart._createQueryForInsert(_g.d.ic_specific_search._table, _g.d.ic_specific_search._keyword + "," + _g.d.ic_specific_search._group_type + ",", "\'" + __keyWord + "\',0,"));
            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
            if (__result.Length > 0)
            {
                MessageBox.Show("Error : " + __result, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MyLib._myGlobal._displayWarning(1, "");
                this._myManageMain._dataList._refreshData();
                _myManageMain__clearData();
            }
        }
    }




}
