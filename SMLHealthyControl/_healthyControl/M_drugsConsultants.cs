using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLHealthyControl._healthyControl
{
    public partial class M_drugsConsultants : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public M_drugsConsultants()
        {
            InitializeComponent();
            this.SuspendLayout();
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat(_g.g._search_screen_healthy_drugsconsultants, MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.m_drugsconsultants._ar_code, 1);
            _myManageData1._dataList._referFieldAdd(_g.d.m_drugsconsultants._date,2);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._healthy_drugsConsultants._saveKeyDown += new MyLib.SaveKeyDownHandler(_healthy_drugsConsultants__saveKeyDown);
            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            //   _myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;
            this._save.Click += new EventHandler(_save_Click);
            this._close.Click += new EventHandler(_close_Click);
            this.ResumeLayout();
            this._healthy_drugsConsultants._isChange = false;
            //this._myManageData1.Panel2.Controls.Add(this._myPanel1);
        }

        void _healthy_drugsConsultants__saveKeyDown(object sender)
        {
            _save_data();
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
                            this._healthy_drugsConsultants._focusFirst();
                            return true;
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
        void _save_Click(object sender, EventArgs e)
        {
            _save_data();
        }
        private void _save_data()
        {
            string getEmtry = this._healthy_drugsConsultants._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {

                ArrayList __getData = this._healthy_drugsConsultants._createQueryForDatabase();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                string __dataList = this._healthy_drugsConsultants._getDataStrQuery(_g.d.m_drugsconsultants._ar_code) + "," + this._healthy_drugsConsultants._getDataStrQuery(_g.d.m_drugsconsultants._date) + ",";
                string __feildList = _g.d.m_drugsconsultants_details._ar_code + "," + _g.d.m_drugsconsultants_details._date+ ",";
                string __dataListUpdate = "  where " + _g.d.m_drugsconsultants_details._ar_code + " = " + this._healthy_drugsConsultants._getDataStrQuery(_g.d.m_drugsconsultants._ar_code) + " and " + _g.d.m_drugsconsultants._date + " = " + this._healthy_drugsConsultants._getDataStrQuery(_g.d.m_drugsconsultants._date);
                this.__grid_drugsConsultants._updateRowIsChangeAll(true);
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.m_drugsconsultants_details._table + " " + __dataListUpdate));
                __myQuery.Append(this.__grid_drugsConsultants._createQueryForInsert(_g.d.m_drugsconsultants_details._table, __feildList, __dataList));
                if (_myManageData1._mode == 1)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                }
                else
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + __getData[2].ToString() + _myManageData1._dataList._whereString));
                }
                
                __myQuery.Append("</node>");
                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    this._healthy_drugsConsultants._isChange = false;
                    if (_myManageData1._mode == 1)
                    {
                        _myManageData1._afterInsertData();
                    }
                    else
                    {
                        _myManageData1._afterUpdateData();
                    }
                    this._healthy_drugsConsultants._clear();
                    this.__grid_drugsConsultants._clear();
                    this._healthy_drugsConsultants._focusFirst();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        void _close_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
            this._healthy_drugsConsultants._clear();
            this.__grid_drugsConsultants._clear();
            Control codeControl = this._healthy_drugsConsultants._getControl(_g.d.m_drugsconsultants._ar_code);
            codeControl.Enabled = true;
            ///  this._healthy_consultation._setDataStr(_g.d.M_PatientProfile., MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code), "", true);
            //  if (this._healthy_consultation._getControl(_g.d.M_PatientProfile._name_1).GetType() == typeof(MyLib._myTextBox))
            //  {
            //      MyLib._myTextBox getText = (MyLib._myTextBox)this._healthy_consultation._getControl(_g.d.M_PatientProfile._name_1);
            //      getText.textBox.Focus();
            //      getText.textBox.SelectAll();
            //  }
        }

        void _myManageData1__newDataClick()
        {
            this._healthy_drugsConsultants._clear();
            this.__grid_drugsConsultants._clear();
            Control codeControl = this._healthy_drugsConsultants._getControl(_g.d.m_drugsconsultants._ar_code);
            codeControl.Enabled = true;
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (this._healthy_drugsConsultants._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._healthy_drugsConsultants._isChange = false;
                }
            }
            return (result);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                this.__grid_drugsConsultants._clear();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _myManageData1._dataList._tableName + whereString));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.m_drugsconsultants_details._table+ whereString.Replace("m_drugsconsultants","m_drugsconsultants_details")));               
                __myQuery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myQuery.ToString());
                this._healthy_drugsConsultants._loadData(((DataSet)_getData[0]).Tables[0]);
                string  __teststaffId =((DataSet)_getData[0]).Tables[0].Rows[0][_g.d.m_drugsconsultants._staff_id].ToString();
                _healthy_drugsConsultants._search(false);
                this.__grid_drugsConsultants._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);
                Control codeControl = this._healthy_drugsConsultants._getControl(_g.d.m_drugsconsultants._ar_code);
                codeControl.Enabled = false;
                for (int __row = 0; __row < this.__grid_drugsConsultants._rowData.Count; __row++) {
                    //__grid_drugsConsultants._searchAndWarning(__row);
                    __grid_drugsConsultants._gridFindItem(__row);
                }
                __grid_drugsConsultants.Invalidate();
                this._healthy_drugsConsultants._isChange = false;
                if (forEdit)
                {
                    this._healthy_drugsConsultants._focusFirst();
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
