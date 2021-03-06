﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

/*
* @author MooAe 
* @copyright 2009
* @mail naiay@msn.com
*/

namespace SMLERPControl._customer
{
    public partial class _ar_detail_healthy : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._myTextBox _searchTextBox;
        string _searchName = "";

        public _ar_detail_healthy()
        {
            this.SuspendLayout();
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            //
            // toe 
            MyLib._myTextBox __apCode = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ar_customer._code);
            __apCode._displayLabel = false;
            __apCode.Visible = false;

            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            //_myManageData1._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._loadViewFormat(_g.g._search_screen_healthy_member_profile, MyLib._myGlobal._userSearchScreenGroup, true);

            _myManageData1._dataList._referFieldAdd(_g.d.ar_customer._code, 1);
            _myManageData1._manageButton = this._myToolbar;
            //_myManageData1._manageBackgroundPanel = this._myPanel1;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            //this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screen_ar1__saveKeyDown);
            this._screen_ar_ArDealer._saveKeyDown += new MyLib.SaveKeyDownHandler(_screen_ar_ArDealer__saveKeyDown);
            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            _myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;
            this.Disposed += new EventHandler(_ar_Disposed);
            this._myToolbar.Renderer = new Renderers.WindowsVistaRenderer();
            this._myTabControl1.SelectedIndexChanged += new EventHandler(_myTabControl1_SelectedIndexChanged);
            _screen_ar_ArDealer._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screen_ar_ArDealer__checkKeyDownReturn);
            this._screen_ar_ArDealerHealthy._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screen_ar_ArDealerHealthy__checkKeyDownReturn);
            this._screen_ar_ArDealer._textBoxChanged += new MyLib.TextBoxChangedHandler(_screen_ar_ArDealer__textBoxChanged);
            this.ResumeLayout();

            this._screenTop.Enabled = false;

            // ถ้ามาจาก SML POS ให้ซ่อนแถบ healthy
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS)
            {
                this._myTabControl1.TabPages.RemoveAt(3);
                this._myTabControl1.TabPages.RemoveAt(2);
                this._myTabControl1.TabPages.RemoveAt(1);
            }

        }

        void _screen_ar_ArDealer__textBoxChanged(object sender, string name)
        {
            // get ar detial to screen top
            _searchTextBox = (MyLib._myTextBox)((Control)sender).Parent;
            _searchName = name;
            string __query = "";
            if (_searchName.Equals(_g.d.ar_dealer._ar_code))
            {
                string __arCode = _screen_ar_ArDealer._getDataStr(_searchName);
                __query = "select * from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + __arCode.ToUpper() + "\'";

            }

            if (__query.Length > 0 && _searchName.Length > 0)
            {
                _searchAndWarning(_searchName, __query, true);
            }


        }

        bool _searchAndWarning(string fieldName, string query, Boolean warning)
        {
            bool __result = false;
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                //string getData = dataResult.Tables[0].Rows[0][0].ToString();
                //string getDataStr = _mainPOSConfigScreen1._getDataStr(fieldName);
                //_mainPOSConfigScreen1._setDataStr(fieldName, getDataStr, getData, true);
                this._screenTop._loadData(dataResult.Tables[0]);
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && _screen_ar_ArDealer._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + _screen_ar_ArDealer._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;

        }


        void _screen_ar_ArDealer__saveKeyDown(object sender)
        {
            this._save_data();
        }

        bool _screen_ar_ArDealerHealthy__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.ar_dealer._mobile_phone))
                        {
                            this._myTabControl1.SelectedIndex = 1;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        bool _screen_ar_ArDealer__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myDateBox))
                    {
                        MyLib._myDateBox __getTextBox = (MyLib._myDateBox)sender;
                        if (__getTextBox._name.Equals(_g.d.ar_dealer._expire_date))
                        {
                            this._myTabControl2.SelectedIndex = 1;
                            this._myTabControl1.SelectedIndex = 0;

                            this._screen_ar_ArDealerHealthy._focusFirst();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        void _myTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._myTabControl1.SelectedIndex == 0)
            {
                this._screen_ar_ArDealerHealthy._focusFirst();
            }
            else if (this._myTabControl1.SelectedIndex == 1)
            {
                this.grid_M_congenital_disease._gotoCell(0, 0);
            }
            else if (this._myTabControl1.SelectedIndex == 2)
            {
                this._grid_M_allergic._gotoCell(0, 0);
            }

        }

        void _ar_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if ((keyData & Keys.Control) == Keys.Control && (keyCode == Keys.Home))
            {
                this._screen_ar_ArDealer._focusFirst();
                return true;
            }
            else
            {
                if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D1))
                {
                    this._myTabControl1.SelectedIndex = 0;
                    return true;
                }
                else
                {
                    if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D2))
                    {
                        this._myTabControl1.SelectedIndex = 1;
                        return true;
                    }
                    else
                    {
                        if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D3))
                        {
                            this._myTabControl1.SelectedIndex = 2;
                            return true;
                        }
                        else
                        {
                        }
                    }
                }
            }

            if (keyData == Keys.F12)
            {
                _save_data();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _screen_ar1__saveKeyDown(object sender)
        {
            _save_data();
        }

        private void _save_data()
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                string getEmtry = this._screenTop._checkEmtryField();
                string _getScreenYourHealthyEmpty = this._screen_yourhealthy._checkEmtryField();
                bool _passValidHealthy = true;

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED && _getScreenYourHealthyEmpty.Length > 0)
                {
                    if (_getScreenYourHealthyEmpty.Length > 0)
                    {
                        _passValidHealthy = false;
                    }
                }

                if (getEmtry.Length > 0 || _passValidHealthy == false)
                {

                    MyLib._myGlobal._displayWarning(2, getEmtry + ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED) ? _getScreenYourHealthyEmpty : ""));
                }
                else
                {
                    ArrayList __getData = this._screen_ar_ArDealer._createQueryForDatabase();
                    ArrayList __getDataHeathy = this._screen_ar_ArDealerHealthy._createQueryForDatabase();
                    ArrayList __getDataYourHealthy = this._screen_yourhealthy._createQueryForDatabase();

                    StringBuilder __myQuery = new StringBuilder();
                    string __member_id = this._screen_ar_ArDealer._getDataStr(_g.d.ar_dealer._code);
                    string __dataList_1 = this._screenTop._getDataStrQuery(_g.d.ar_customer._code) + ",";
                    string __ar_code = this._screen_ar_ArDealer._getDataStr(_g.d.ar_dealer._ar_code);
                    string __dataList_2 = __dataList_1 + this._screen_ar_ArDealer._getDataStrQuery(_g.d.ar_dealer._code) + ",";
                    string __dataListUpdate = "  where " + _g.d.ar_dealer._ar_code + " = " + this._screenTop._getDataStrQuery(_g.d.ar_dealer._code);
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ar_dealer._table + " " + __dataListUpdate + " and " + _g.d.ar_dealer._code + " = \'" + this._screen_ar_ArDealer._getDataStr(_g.d.ar_dealer._code) + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_dealer._table + " (" + __getData[0].ToString() + "," + __getDataHeathy[0].ToString() + ") values (" + __getData[1].ToString() + "," + __getDataHeathy[1].ToString() + ")"));
                    this._grid_M_allergic._updateRowIsChangeAll(true);
                    this.grid_M_congenital_disease._updateRowIsChangeAll(true);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED)
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.m_allergic._table + " " + __dataListUpdate));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.m_congenital_disease._table + " " + __dataListUpdate));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.m_yourhealthy._table + " " + __dataListUpdate + " AND " + _g.d.m_yourhealthy._member_id + " = \'" + this._screen_ar_ArDealer._getDataStr(_g.d.ar_dealer._code) + "\'"));
                        __myQuery.Append(this._grid_M_allergic._createQueryForInsert(_g.d.m_allergic._table, _g.d.m_allergic._ar_code + "," + _g.d.m_allergic._member_id + ",", __dataList_2));
                        __myQuery.Append(this.grid_M_congenital_disease._createQueryForInsert(_g.d.m_congenital_disease._table, _g.d.m_allergic._ar_code + "," + _g.d.m_allergic._member_id + ",", __dataList_2));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.m_yourhealthy._table + "(" + _g.d.m_yourhealthy._ar_code + "," + _g.d.m_yourhealthy._member_id + "," + __getDataYourHealthy[0].ToString() + ")" + " values (\'" + __ar_code + "\', \'" + __member_id + "\'," + __getDataYourHealthy[1].ToString() + ")"));
                    }

                    __myQuery.Append("</node>");

                    string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, null);
                        this._screenTop._isChange = false;
                        if (_myManageData1._mode == 1)
                        {
                            _myManageData1._afterInsertData();
                        }
                        else
                        {
                            _myManageData1._afterUpdateData();
                        }
                        this._screenTop._clear();
                        this._screen_ar_ArDealer._clear();
                        this._screen_ar_ArDealerHealthy._clear();
                        this._grid_M_allergic._clear();
                        this.grid_M_congenital_disease._clear();
                        this._screen_ar_ArDealer._focusFirst();
                    }
                    else
                    {
                        MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                {
                    MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                    __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString));
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
        }

        void _myManageData1__clearData()
        {
            this._screenTop._clear();
            this._screen_ar_ArDealerHealthy._clear();
            this._grid_M_allergic._clear();
            this.grid_M_congenital_disease._clear();
            Control codeControl = this._screenTop._getControl(_g.d.ar_dealer._code);
            codeControl.Enabled = true;
            // this._screenTop._setDataStr(_g.d.ar_dealer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_dealer._table, _g.d.ar_dealer._code), "", true);
            this._screen_ar_ArDealer._clear();
        }

        void _myManageData1__newDataClick()
        {
            this._screenTop._clear();
            this._grid_M_allergic._clear();
            this.grid_M_congenital_disease._clear();
            this._screenTop._clear();
            this._screen_ar_ArDealerHealthy._clear();
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED)
            {
                this._screen_yourhealthy._clear();
            }
            Control codeControl = this._screenTop._getControl(_g.d.ar_dealer._code);
            codeControl.Enabled = true;
            // this._screenTop._setDataStr(_g.d.ar_dealer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_dealer._table, _g.d.ar_dealer._code), "", true);
            this._screen_ar_ArDealer._clear();
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (this._screenTop._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._screenTop._isChange = false;
                }
            }
            return (result);
        }

        private int _get_column_number()
        {
            return _myManageData1._dataList._gridData._findColumnByName(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._code);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {

                this._screen_ar_ArDealer._clear();
                this._screen_ar_ArDealerHealthy._clear();
                ArrayList __rowDataArray = (ArrayList)rowData;
                string _oldDocNo = __rowDataArray[_get_column_number()].ToString();
                string _ar_code = __rowDataArray[_myManageData1._dataList._gridData._findColumnByName(_g.d.ar_dealer._table + "." + _g.d.ar_dealer._ar_code)].ToString();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + " = \'" + _ar_code + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ar_dealer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_dealer._code) + " = \'" + _oldDocNo.ToUpper() + "\' AND " + _g.d.ar_dealer._ar_code + " = \'" + _ar_code + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.m_congenital_disease._table + " where " + _g.d.m_congenital_disease._ar_code + " = '" + _ar_code + "'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.m_allergic._table + " where " + _g.d.m_allergic._ar_code + " = '" + _ar_code + "'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.m_yourhealthy._table + " where " + MyLib._myGlobal._addUpper(_g.d.m_yourhealthy._member_id) + " = \'" + _oldDocNo + "\' AND " + _g.d.m_yourhealthy._ar_code + " = \'" + _ar_code + "\' "));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._screenTop._loadData(((DataSet)_getData[0]).Tables[0]);
                this._screen_ar_ArDealer._loadData(((DataSet)_getData[1]).Tables[0]);
                this._screen_ar_ArDealerHealthy._loadData(((DataSet)_getData[1]).Tables[0]);
                this.grid_M_congenital_disease._loadFromDataTable(((DataSet)_getData[2]).Tables[0]);
                for (int __row = 0; __row < grid_M_congenital_disease._rowData.Count; __row++)
                {
                    this.grid_M_congenital_disease._searchAndWarning(__row);
                }

                this._grid_M_allergic._loadFromDataTable(((DataSet)_getData[3]).Tables[0]);
                for (int __row = 0; __row < _grid_M_allergic._rowData.Count; __row++)
                {
                    this._grid_M_allergic._searchAndWarning(__row);
                }

                this._screen_yourhealthy._loadData(((DataSet)_getData[4]).Tables[0]);

                _screen_ar_ArDealer._search(false);
                this._screenTop._search(false);
                _screen_ar_ArDealerHealthy._search(false);
                this._screenTop._isChange = false;
                if (forEdit)
                {
                    this._screenTop._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._save_data();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
