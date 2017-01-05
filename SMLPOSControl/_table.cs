using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _table : UserControl
    {
        private _tableScreenControl _screen = new _tableScreenControl();
        private string _oldCode = "";

        public _table()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._screen._toolStrip.EnabledChanged += new EventHandler(_toolStrip_EnabledChanged);
            //
            this._screen.Dock = DockStyle.Fill;
            this._screen.Enabled = false;
            this._myManageData1._form2.Controls.Add(this._screen);
            //
            this._myManageData1._displayMode = 0;
            this._myManageData1._dataList._lockRecord = true;
            this._myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            this._myManageData1._dataList._loadViewFormat(_g.g._search_screen_table, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            this._myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageData1._manageButton = this._screen._toolStrip;
            this._myManageData1._manageBackgroundPanel = this._screen._panel;
            this._myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            this._myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            this._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            this._myManageData1._dataList._referFieldAdd(_g.d.table_master._number, 1);
            this._myManageData1._checkEditData += new MyLib.CheckEditDataEvent(_myManageData1__checkEditData);
            this._myManageData1._calcArea();
            this._myManageData1._dataListOpen = true;
            this._myManageData1._autoSize = true;
            this._myManageData1._autoSizeHeight = 450;
            this._myManageData1.Invalidate();
            //
            this._screen._saveButton.Click += (s1, e1) =>
            {
                this._saveData();
            };
            this._screen._closeButton.Click += (s1, e1) =>
            {
                this.Dispose();
            };
        }

        void _toolStrip_EnabledChanged(object sender, EventArgs e)
        {
            this._screen.Enabled = true;
        }

        bool _myManageData1__checkEditData(int row, MyLib._myGrid sender)
        {
            return true;
        }


        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _clear()
        {
            this._screen._myScreen1._clear();
        }

        void _myManageData1__clearData()
        {
            this._clear();
            Control __codeControl = this._screen._myScreen1._getControl(_g.d.table_master._number);
            __codeControl.Enabled = true;
        }

        bool _myManageData1__discardData()
        {
            return true;
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            // check for delete
            StringBuilder __tableForCheckDelete = new StringBuilder();
            for (int loop = 0; loop < selectRowOrder.Count; loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[loop];
                int _getColumnTableNumber = this._myManageData1._dataList._gridData._findColumnByName(_g.d.table_master._table + "." + _g.d.table_master._number);
                string __getTableNumber = this._myManageData1._dataList._gridData._cellGet(getData.row, _getColumnTableNumber).ToString().ToUpper();

                if (__tableForCheckDelete.Length > 0)
                {
                    __tableForCheckDelete.Append(",");
                }
                __tableForCheckDelete.Append("\'" + __getTableNumber + "\'");
            }


            string __checkQuery = "select coalesce(" + _g.d.table_master._status + ", 0) as " + _g.d.table_master._status + ", " + _g.d.table_master._number + " from " + _g.d.table_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " in (" + __tableForCheckDelete + ") order by  " + _g.d.table_master._status + " desc ";
            DataTable __dataCheck = __myFrameWork._queryShort(__checkQuery).Tables[0];
            if (__dataCheck.Rows.Count > 0 && __dataCheck.Rows[0][_g.d.table_master._status].ToString().Equals("0") == false)
            {
                MessageBox.Show("โต๊ะ " +  __dataCheck.Rows[0][_g.d.table_master._number].ToString() + " มีการใช้งานอยู่");
                return;
            }



            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int loop = 0; loop < selectRowOrder.Count; loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[loop];
                int _getColumnTableNumber = this._myManageData1._dataList._gridData._findColumnByName(_g.d.table_master._table + "." + _g.d.table_master._number);
                string __getTableNumber = this._myManageData1._dataList._gridData._cellGet(getData.row, _getColumnTableNumber).ToString().ToUpper();
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.table_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + __getTableNumber + "\'"));
            }
            __myQuery.Append("</node>");

            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(0, null);
                _myManageData1._dataList._refreshData();
                this._clear();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                StringBuilder __myquery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                this._oldCode = __rowDataArray[1].ToString().ToUpper();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.table_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + "=\'" + this._oldCode + "\'"));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._screen._myScreen1._loadData(((DataSet)__getData[0]).Tables[0]);
                //
                Control __codeControl = this._screen._myScreen1._getControl(_g.d.table_master._number);
                __codeControl.Enabled = false;
                if (forEdit)
                {
                    this._screen._myScreen1._focusFirst();
                }

                this._screen._myScreen1.Invalidate();

                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }

        private void _saveData()
        {
            try
            {
                string __getEmtry = this._screen._myScreen1._checkEmtryField();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    string __tableCode = this._screen._myScreen1._getDataStr(_g.d.table_master._number).ToString();

                    string __checkQuery = "select " + _g.d.table_master._status + " from " + _g.d.table_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + "=\'" + __tableCode + "\'";
                    DataTable __dataCheck = __myFrameWork._queryShort(__checkQuery).Tables[0];
                    if (__dataCheck.Rows.Count > 0 && __dataCheck.Rows[0][0].ToString().Equals("0") == false)
                    {
                        MessageBox.Show("ไม่สามารถแก้ัไขได้ โต๊ะ ดังกล่าวมีการใช้งานอยู่");
                        return;
                    }


                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.table_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + "=\'" + __tableCode + "\'"));
                    ArrayList __getData = this._screen._myScreen1._createQueryForDatabase();
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_master._table + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                    __myQuery.Append("</node>");
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length == 0)
                    {
                        this._clear();
                        //
                        MyLib._myGlobal._displayWarning(1, null);
                        if (this._myManageData1._mode == 1)
                        {
                            this._myManageData1._afterInsertData();
                        }
                        else
                        {
                            this._myManageData1._afterUpdateData();
                        }
                    }
                    else
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
        }

        private void _closeButton_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _testButton_Click_1(object sender, EventArgs e)
        {
            Form __testForm = new Form();
            __testForm.WindowState = FormWindowState.Maximized;
            _tableSearchLevelControl __control = new _tableSearchLevelControl();
            __control.Dock = DockStyle.Fill;
            __testForm.Controls.Add(__control);
            __testForm.ShowDialog();
        }
    }
}
