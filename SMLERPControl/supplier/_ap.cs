using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

/*
* @author MooAe 
* @copyright 2009
* @mail naiay@msn.com
*/

namespace SMLERPControl.supplier
{
    public partial class _ap : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _ap()
        {
            this.SuspendLayout();
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._myManageData1._displayMode = 0;
            _myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_ap_supplier", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.ap_supplier._code, 1);
            _myManageData1._manageButton = this._myToolbar;
            //_myManageData1._manageBackgroundPanel = this._myPanel1;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screen_ap_main1__saveKeyDown);
            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            //_myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;
            this.Disposed += new EventHandler(_ap_Disposed);
            this._myToolbar.Renderer = new Renderers.WindowsVistaRenderer();
            this.ResumeLayout();

            _g._utils __utils = new _g._utils();
            __utils._updateInventoryMaster("");
            Thread __thread = new Thread(new ThreadStart(__utils._updateSupplierMasterFunction));
            __thread.Start();
        }

        void _ap_Disposed(object sender, EventArgs e)
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
                this._screenTop._setDataStr(_g.d.ap_supplier._code, MyLib._myGlobal._getAutoRun(_g.d.ap_supplier._table, _g.d.ap_supplier._code), "", true);
                return true;
            }
            if (keyData == Keys.F12 && this._myToolbar.Enabled == true)
            {
                _save_data();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _screen_ap_main1__saveKeyDown(object sender)
        {
            _save_data();
        }

        private void _save_data()
        {
            if (this._myToolbar.Enabled == true)
            {
                if (MyLib._myGlobal._checkChangeMaster())
                {
                    string getEmtry = this._screenTop._checkEmtryField();
                    if (getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, getEmtry);
                    }
                    else
                    {
                        String __chkNewCode = this._screenTop._getDataStr(_g.d.ap_supplier._code);
                        if (MyLib._myGlobal._chkAutoRunBeforSave(_myManageData1._mode, __chkNewCode, _g.d.ap_supplier._table, _g.d.ap_supplier._code))
                        {
                            if (MyLib._myGlobal._isCheckRuningBeforSave)
                            {
                                this._screenTop._setDataStr(_g.d.ap_supplier._code, MyLib._myGlobal._getAutoRun(_g.d.ap_supplier._table, _g.d.ap_supplier._code));
                            }
                            ArrayList __getData = this._screenTop._createQueryForDatabase();

                            String __ap_code = this._screenTop._getDataStr(_g.d.ap_supplier._code);


                            StringBuilder __myQuery = new StringBuilder();
                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            if (_myManageData1._mode == 1)
                            {
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));

                                string __detailField = _g.d.ap_supplier_detail._ap_code + "," + _g.d.ap_supplier_detail._tax_id + "," + _g.d.ap_supplier_detail._card_id + "," + _g.d.ap_supplier_detail._branch_type + "," + _g.d.ap_supplier_detail._branch_code;
                                string __detailValue = "\'" + __ap_code + "\', \'" + this._screenTop._getDataStr(_g.d.ap_supplier._tax_id) + "\', \'" + this._screenTop._getDataStr(_g.d.ap_supplier._card_id) + "\', \'" + this._screenTop._getDataStr(_g.d.ap_supplier._branch_type) + "\', \'" + this._screenTop._getDataStr(_g.d.ap_supplier._branch_code) + "\' ";

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ap_supplier_detail._table + " (" + __detailField + ") values (" + __detailValue + ")"));

                            }
                            else
                            {
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + __getData[2].ToString() + _myManageData1._dataList._whereString));

                                string __updateDetail = _g.d.ap_supplier_detail._tax_id + "=\'" + this._screenTop._getDataStr(_g.d.ar_customer._tax_id) + "\'," +
                                    _g.d.ap_supplier_detail._card_id + "=\'" + this._screenTop._getDataStr(_g.d.ar_customer._card_id) + "\'," +
                                    _g.d.ap_supplier_detail._branch_type + "=\'" + this._screenTop._getDataStr(_g.d.ar_customer._branch_type) + "\'," +
                                    _g.d.ap_supplier_detail._branch_code + "=\'" + this._screenTop._getDataStr(_g.d.ar_customer._branch_code) + "\' ";

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ap_supplier_detail._table + " set " + __updateDetail + " where " + _g.d.ap_supplier_detail._ap_code + "=\'" + __ap_code + "\' "));


                            }
                            //
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
                                this._screenTop._focusFirst();
                            }
                            else
                            {
                                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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
                int __columnArCodeIndex = this._myManageData1._dataList._gridData._findColumnByName(_g.d.ap_supplier._table + "." + _g.d.ap_supplier._code);

                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                {
                    MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                    string __ap_code = this._myManageData1._dataList._gridData._cellGet(getData.row, __columnArCodeIndex).ToString();


                    Boolean __pass = true;
                    {
                        string __quey = "select (select count(*) from ic_trans where ic_trans.cust_code = ap_supplier.code ) as count_ic_trans, (select count(*) from ap_ar_trans where ap_ar_trans.cust_code = ap_supplier.code ) as count_ap_ar_trans from " + _g.d.ap_supplier._table + " where " + MyLib._myGlobal._addUpper(_g.d.ap_supplier._code) + "=\'" + MyLib._myUtil._convertTextToXml(__ap_code).ToUpper() + "\'";
                        DataTable __getCount = _myFrameWork._queryShort(__quey).Tables[0];
                        int __countICTrans = (int)MyLib._myGlobal._decimalPhase(__getCount.Rows[0][0].ToString());
                        int __countApArTrans = (int)MyLib._myGlobal._decimalPhase(__getCount.Rows[0][1].ToString());
                        if (__countICTrans > 0 || __countApArTrans > 0)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("เจ้าหนี้มีการใช้ไปแล้ว") + " " + MyLib._myGlobal._resource("ห้ามลบ") + " : " + __ap_code);
                            __pass = false;
                        }
                    }

                    if (__pass)
                    {
                        __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString));
                        __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _g.d.ap_supplier_detail._table, " where " + MyLib._myGlobal._addUpper(_g.d.ap_supplier_detail._ap_code) + "=\'" + __ap_code.ToUpper() + "\'"));
                    }
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
            Control codeControl = this._screenTop._getControl(_g.d.ap_supplier._code);
            codeControl.Enabled = true;
            this._screenTop._setDataStr(_g.d.ap_supplier._code, MyLib._myGlobal._getAutoRun(_g.d.ap_supplier._table, _g.d.ap_supplier._code), "", true);
            if (this._screenTop._getControl(_g.d.ap_supplier._name_1).GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox getText = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ap_supplier._name_1);
                getText.textBox.Focus();
                getText.textBox.SelectAll();
            }
        }

        void _myManageData1__newDataClick()
        {
            this._screenTop._clear();
            Control codeControl = this._screenTop._getControl(_g.d.ap_supplier._code);
            codeControl.Enabled = true;
            this._screenTop._setDataStr(_g.d.ap_supplier._code, MyLib._myGlobal._getAutoRun(_g.d.ap_supplier._table, _g.d.ap_supplier._code), "", true);
            if (this._screenTop._getControl(_g.d.ap_supplier._name_1).GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox getText = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ap_supplier._name_1);
                getText.textBox.Focus();
                getText.textBox.SelectAll();
            }
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

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                DataSet getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * " +
                    ", (select " + _g.d.ap_supplier_detail._tax_id + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + ") as " + _g.d.ap_supplier._tax_id +
                    ", (select " + _g.d.ap_supplier_detail._card_id + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + ") as " + _g.d.ap_supplier._card_id +
                    ", (select " + _g.d.ap_supplier_detail._branch_type + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + ") as " + _g.d.ap_supplier._branch_type +
                    ", (select " + _g.d.ap_supplier_detail._branch_code + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + ") as " + _g.d.ap_supplier._branch_code +

                    "  from " + _myManageData1._dataList._tableName + whereString);
                this._screenTop._loadData(getData.Tables[0]);
                Control codeControl = this._screenTop._getControl(_g.d.ap_supplier._code);
                codeControl.Enabled = false;
                this._screenTop._search(false);
                this._screenTop._isChange = false;

                if (getData.Tables.Count > 0 && getData.Tables[0].Rows.Count > 0)
                {
                    this._screenTop._setDataStr(_g.d.ap_supplier._tax_id, getData.Tables[0].Rows[0][_g.d.ap_supplier._tax_id].ToString());
                    this._screenTop._setDataStr(_g.d.ap_supplier._card_id, getData.Tables[0].Rows[0][_g.d.ap_supplier._card_id].ToString());
                    this._screenTop._setDataStr(_g.d.ap_supplier._branch_code, getData.Tables[0].Rows[0][_g.d.ap_supplier._branch_code].ToString());

                    this._screenTop._setComboBox(_g.d.ap_supplier._branch_type, MyLib._myGlobal._intPhase(getData.Tables[0].Rows[0][_g.d.ap_supplier._tax_id].ToString()));
                }

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            if (_myManageData1._dataList._loadViewDataSuccess == false)
            {
                this._myManageData1._dataList._loadViewData(0);
            }
        }
    }
}
