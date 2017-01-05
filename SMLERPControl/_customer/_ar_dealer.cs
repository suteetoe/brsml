using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl._customer
{
    public partial class _ar_dealer : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public _ar_dealer()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_ar_customer", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.ar_customer._code, 1);
            _myManageData1._manageButton = this._myToolbar;
            if (_g.g._companyProfile._customer_by_branch && _g.g._companyProfile._change_branch_code == false)
            {
                _myManageData1._dataList._extraWhere = _g.d.ar_customer._ar_branch_code + "=\'" + _g.g._companyProfile._branch_code + "\' ";
            }
            //_myManageData1._manageBackgroundPanel = this._myPanel1;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screen_ar1__saveKeyDown);
            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            _myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;
            this.Disposed += new EventHandler(_ar_Disposed);
            this._myToolbar.Renderer = new Renderers.WindowsVistaRenderer();
            this._screenTop.Enabled = false;
            this._screen_ar_main2._focusFirst();
        }

        void _ar_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if ((keyData & Keys.Control) == Keys.Control)
            {
                switch (keyCode)
                {
                    case Keys.Home:
                        {
                            this._screenTop._focusFirst();
                            return true;
                        }
                }
            }
            if (keyData == Keys.F2)
            {
                this._screen_ar_main2._setDataStr(_g.d.ar_dealer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_dealer._table, _g.d.ar_dealer._code), "", true);
                return true;
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
            if (this._myToolbar.Enabled == true)
            {
                if (MyLib._myGlobal._checkChangeMaster())
                {
                    string getEmtry = this._screen_ar_main2._checkEmtryField();
                    if (getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, getEmtry);
                    }
                    else
                    {
                        ArrayList __getData = this._screen_ar_main2._createQueryForDatabase();
                        StringBuilder __myQuery = new StringBuilder();
                        string __dataList_1 = this._screenTop._getDataStrQuery(_g.d.ar_customer._code) + ",";
                        string __dataListUpdate = "  where " + _g.d.ar_dealer._ar_code + " = " + this._screenTop._getDataStrQuery(_g.d.ar_dealer._code);
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ar_dealer._table + " " + __dataListUpdate));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_dealer._table + " (ar_code," + __getData[0].ToString() + ") values (" + __dataList_1 + "" + __getData[1].ToString() + ")"));
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
                            this._screen_ar_main2._clear();
                            this._screen_ar_main2._focusFirst();
                        }
                        else
                        {
                            MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถบันทึกข้อมูลได้"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ar_dealer._table), _myManageData1._dataList._tableName, getData.whereString));
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
            Control codeControl = this._screenTop._getControl(_g.d.ar_dealer._code);
            codeControl.Enabled = true;
            this._screenTop._setDataStr(_g.d.ar_dealer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_dealer._table, _g.d.ar_dealer._code), "", true);
            this._screen_ar_main2._clear();
            /*if (this._screenTop._getControl(_g.d.ar_dealer._name_1).GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox getText = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ar_dealer._name_1);
                getText.textBox.Focus();
                getText.textBox.SelectAll();
            }*/
        }

        void _myManageData1__newDataClick()
        {
            this._screenTop._clear();
            Control codeControl = this._screenTop._getControl(_g.d.ar_dealer._code);
            codeControl.Enabled = true;
            this._screenTop._setDataStr(_g.d.ar_dealer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_dealer._table, _g.d.ar_dealer._code), "", true);
            this._screen_ar_main2._clear();
            /*if (this._screenTop._getControl(_g.d.ar_dealer._name_1).GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox getText = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ar_dealer._name_1);
                getText.textBox.Focus();
                getText.textBox.SelectAll();
            }*/
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
            return _myManageData1._dataList._gridData._findColumnByName(_g.d.ar_customer._table + "." + _g.d.ar_customer._code);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                this._screen_ar_main2._clear();
                ArrayList __rowDataArray = (ArrayList)rowData;
                string _oldDocNo = __rowDataArray[_get_column_number()].ToString();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _myManageData1._dataList._tableName + whereString));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ar_dealer._table + " where " + _g.d.ar_dealer._ar_code + " = '" + _oldDocNo + "'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._screenTop._loadData(((DataSet)_getData[0]).Tables[0]);
                this._screen_ar_main2._loadData(((DataSet)_getData[1]).Tables[0]);
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