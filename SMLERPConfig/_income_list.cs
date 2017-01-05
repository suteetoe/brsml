using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPConfig
{
    public partial class _income_list : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _income_list()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_erp_income_list", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.erp_income_list._code, 1);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._manageBackgroundPanel = this._myPanel;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._incomelistScreen1._saveKeyDown += new MyLib.SaveKeyDownHandler(_incomelistScreen1__saveKeyDown);
            this._incomelistScreen1._checkKeyDown += new MyLib.CheckKeyDownHandler(_incomelistScreen1__checkKeyDown);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 500;

            this.Disposed += new EventHandler(_side_Disposed);
            this.Resize += new EventHandler(_side_Resize);
        }

        Boolean _incomelistScreen1__checkKeyDown(object sender, Keys keyData)
        {
            if (_myToolBar.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
                this._incomelistScreen1._isChange = false;
            }
						return true;
        }

        void _side_Resize(object sender, EventArgs e)
        {
            if (_myManageData1._dataList._loadViewDataSuccess == false)
            {
                _myManageData1._dataListOpen = true;
                _myManageData1._calcArea();
                _myManageData1._dataList._loadViewData(0);
            }
        }

        void _save_data()
        {
            _incomelistScreen1._saveLastControl();
            string getEmtry = _incomelistScreen1._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {
                ArrayList __getData = _incomelistScreen1._createQueryForDatabase();
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
                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    if (_myManageData1._mode == 1)
                    {
                        _myManageData1._afterInsertData();
                    }
                    else
                    {
                        _myManageData1._afterUpdateData();
                    }
                    _incomelistScreen1._clear();
                    _incomelistScreen1._focusFirst();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void _side_Disposed(object sender, EventArgs e)
        {

        }

        void _myManageData1__newDataClick()
        {
            Control codeControl = _incomelistScreen1._getControl(_g.d.erp_income_list._code);
            codeControl.Enabled = true;
            _incomelistScreen1._focusFirst();
        }

        void _incomelistScreen1__saveKeyDown(object sender)
        {
            _save_data();
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
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

        void _myManageData1__clearData()
        {
            _incomelistScreen1._clear();
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (_incomelistScreen1._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    _incomelistScreen1._isChange = false;
                }
            }
            return (result);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                DataSet getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString);
                _incomelistScreen1._loadData(getData.Tables[0]);
                Control codeControl = _incomelistScreen1._getControl(_g.d.erp_income_list._code);
                codeControl.Enabled = false;
                if (forEdit)
                {
                    _incomelistScreen1._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            this._save_data();
        }
    }

    public partial class _incomelistScreen : MyLib._myScreen
    {
        public _incomelistScreen()
        {
            this._maxColumn = 1;
            this.SuspendLayout();
            this._table_name = _g.d.erp_income_list._table;
            this._addTextBox(0, 0, 1, 0, _g.d.erp_income_list._code, 1, 25, 0, true, false, false);
            this._addTextBox(1, 0, 1, 0, _g.d.erp_income_list._name_1, 1, 25, 0, true, false, false);
            this._addTextBox(2, 0, 1, 0, _g.d.erp_income_list._name_2, 1, 25, 0, true, false, true);
            this._addTextBox(3, 0, 1, 0, _g.d.erp_income_list._gl_account_code, 1, 25, 1, true, false, true);
            this.ResumeLayout();
        }
    }
}
