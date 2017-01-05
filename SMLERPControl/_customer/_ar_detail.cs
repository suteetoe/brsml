using System;
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
    public partial class _ar_detail : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _ar_detail()
        {
            this.SuspendLayout();
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, true);
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
            //_myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            //_myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screen_ar1__saveKeyDown);
            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            //_myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;

            // toe 
            this._myManageData1._dataList._buttonNew.Visible =
                this._myManageData1._dataList._buttonNewFromTemp.Visible =
                this._myManageData1._dataList._buttonSelectAll.Visible =
                this._myManageData1._dataList._buttonDelete.Visible = false;



            this.Disposed += new EventHandler(_ar_Disposed);
            this._myToolbar.Renderer = new Renderers.WindowsVistaRenderer();
            this._myTabControl1.SelectedIndexChanged += new EventHandler(_myTabControl1_SelectedIndexChanged);
            this._screenTop._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screenTop__checkKeyDownReturn);
            this._screen_ar_detail_2._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screen_ar_detail_2__checkKeyDownReturn);
            this._screen_ar_detail_3._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screen_ar_detail_3__checkKeyDownReturn);
            this._screen_ar_detail_5._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screen_ar_detail_5__checkKeyDownReturn);
            this.ResumeLayout(false);
            //
            this._screenTop.Enabled = false;
        }

        bool _screen_ar_detail_5__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.ar_customer_detail._group_sub_4))
                        {
                            this._myTabControl1.SelectedIndex = 3;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        bool _screen_ar_detail_3__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.ar_customer_detail._keep_money_person))
                        {
                            this._screen_ar_detail_4._focusFirst();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        bool _screen_ar_detail_2__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.ar_customer_detail._credit_day))
                        {
                            this._screen_ar_detail_3._focusFirst();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        bool _screenTop__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.ar_customer._website))
                        {
                            this._screen_ar_detail_1._focusFirst();
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
                this._screen_ar_detail_1._focusFirst();
            }
            else if (this._myTabControl1.SelectedIndex == 1)
            {
                this._screen_ar_detail_3._focusFirst();
            }
            else if (this._myTabControl1.SelectedIndex == 2)
            {
                this._screen_ar_detail_4._focusFirst();
            }
            else if (this._myTabControl1.SelectedIndex == 3)
            {
                //this._screen_ar_contact_grid1._cellGet(0, 0);
            }
            else if (this._myTabControl1.SelectedIndex == 4)
            {
                //this._screen_ar_item_grid1._cellGet(0, 0);
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
                this._screenTop._focusFirst();
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
                            if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D4))
                            {
                                this._myTabControl1.SelectedIndex = 3;
                                return true;
                            }
                            else
                            {
                                if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D5))
                                {
                                    this._myTabControl1.SelectedIndex = 4;
                                    return true;
                                }
                                else
                                {
                                    if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D6))
                                    {
                                        this._myTabControl1.SelectedIndex = 5;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (keyData == Keys.F2)
            {
                this._screenTop._setDataStr(_g.d.ar_customer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code), "", true);
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
                    string getEmtry = this._screenTop._checkEmtryField();
                    if (getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, getEmtry);
                    }
                    else
                    {
                        String __chkNewCode = this._screenTop._getDataStr(_g.d.ar_customer._code);
                        if (MyLib._myGlobal._chkAutoRunBeforSave(_myManageData1._mode, __chkNewCode, _g.d.ar_customer._table, _g.d.ar_customer._code))
                        {
                            if (MyLib._myGlobal._isCheckRuningBeforSave)
                            {
                                this._screenTop._setDataStr(_g.d.ar_customer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code));
                            }
                            ArrayList __getData1_1 = this._screenTop._createQueryForDatabase();
                            ArrayList __getData1 = this._screen_ar_detail_1._createQueryForDatabase();
                            ArrayList __getData2 = this._screen_ar_detail_2._createQueryForDatabase();
                            ArrayList __getData3 = this._screen_ar_detail_3._createQueryForDatabase();
                            ArrayList __getData4 = this._screen_ar_detail_4._createQueryForDatabase();
                            ArrayList __getData5 = this._screen_ar_detail_5._createQueryForDatabase();

                            string __dataList = this._screenTop._getDataStrQuery(_g.d.ar_customer._code) + ",";
                            string __fieldList_1 = _g.d.ar_customer._code + ",";

                            StringBuilder __myQuery = new StringBuilder();

                            string __dataList_1 = this._screenTop._getDataStrQuery(_g.d.ar_customer._code) + ",";
                            string __dataListUpdate = "  where " + _g.d.ar_customer_detail._ar_code + " = " + this._screenTop._getDataStrQuery(_g.d.ar_customer._code);

                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ar_contactor._table + " " + __dataListUpdate));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ar_item_by_customer._table + " " + __dataListUpdate));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ar_customer_detail._table + " " + __dataListUpdate));

                            if (_myManageData1._mode == 1)
                            {
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData1_1[0].ToString() + ") values (" + __getData1_1[1].ToString() + ")"));
                            }
                            else
                            {
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + __getData1_1[2].ToString() + _myManageData1._dataList._whereString));
                            }


                            string __extraField = "";
                            string __extraValue = "";
                            //if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            //{
                            //    MyLib._myComboBox __reasonCombo = (MyLib._myComboBox)this._screen_ar_detail_2._getControl(_g.d.ar_customer_detail._close_reason);

                            //    __extraField = "," + _g.d.ar_customer_detail._close_reason;
                            //    __extraValue = ",\'" + __reasonCombo.Text + "\'";
                            //}
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_customer_detail._table + " (ar_code," + __getData1[0].ToString() + "," + __getData2[0].ToString() + "," + __getData3[0].ToString() + "," + __getData4[0].ToString() + "," + __getData5[0].ToString() + __extraField + ") values (" + __dataList_1 + "" + __getData1[1].ToString() + "," + __getData2[1].ToString() + "," + __getData3[1].ToString() + "," + __getData4[1].ToString() + "," + __getData5[1].ToString() + __extraValue + ")"));


                            string __result_images = this._getPicture1._updateImage("CONTRACT" + this._screenTop._getDataStr(_g.d.ar_customer._code));

                            this._screen_ar_contact_grid1._updateRowIsChangeAll(true);
                            this._screen_ar_item_grid1._updateRowIsChangeAll(true);
                            this._screen_ar_contact_grid1._updateRowIsChangeAll(true);
                            this._screen_ar_item_grid1._updateRowIsChangeAll(true);
                            __myQuery.Append(this._screen_ar_contact_grid1._createQueryForInsert(_g.d.ar_contactor._table, "ar_code,", __dataList));
                            __myQuery.Append(this._screen_ar_item_grid1._createQueryForInsert(_g.d.ar_item_by_customer._table, "ar_code,", __dataList));
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
                                this._myManageData1._newData(true);
                                this._screenTop._isChange = false;
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
            this._screen_ar_detail_1._clear();
            this._screen_ar_detail_2._clear();
            this._screen_ar_detail_3._clear();
            this._screen_ar_detail_4._clear();
            this._screen_ar_detail_5._clear();
            this._screen_ar_item_grid1._clear();
            this._screen_ar_contact_grid1._clear();
            Control codeControl = this._screenTop._getControl(_g.d.ar_customer._code);
            codeControl.Enabled = true;
            this._screenTop._setDataStr(_g.d.ar_customer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code), "", true);
            if (this._screenTop._getControl(_g.d.ar_customer._name_1).GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox getText = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ar_customer._name_1);
                getText.textBox.Focus();
                getText.textBox.SelectAll();
            }
        }

        void _myManageData1__newDataClick()
        {
            this._screenTop._clear();
            this._screen_ar_detail_1._clear();
            this._screen_ar_detail_2._clear();
            this._screen_ar_detail_3._clear();
            this._screen_ar_detail_4._clear();
            this._screen_ar_detail_5._clear();
            this._screen_ar_item_grid1._clear();
            this._screen_ar_contact_grid1._clear();
            this._getPicture1._clearpic();
            Control codeControl = this._screenTop._getControl(_g.d.ar_customer._code);
            codeControl.Enabled = true;
            this._screenTop._setDataStr(_g.d.ar_customer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code), "", true);
            if (this._screenTop._getControl(_g.d.ar_customer._name_1).GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox getText = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ar_customer._name_1);
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

        private int _get_column_number()
        {
            return _myManageData1._dataList._gridData._findColumnByName(_g.d.ar_customer._table + "." + _g.d.ar_customer._code);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                this._screen_ar_detail_1._clear();
                this._screen_ar_detail_2._clear();
                this._screen_ar_detail_3._clear();
                this._screen_ar_detail_4._clear();
                this._screen_ar_detail_5._clear();
                this._screen_ar_contact_grid1._clear();
                this._screen_ar_item_grid1._clear();

                ArrayList __rowDataArray = (ArrayList)rowData;
                string _oldDocNo = __rowDataArray[_get_column_number()].ToString();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _myManageData1._dataList._tableName + whereString));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + " = '" + _oldDocNo + "'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ar_contactor._table + " where " + _g.d.ar_contactor._ar_code + " = '" + _oldDocNo + "'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ar_item_by_customer._table + " where " + _g.d.ar_item_by_customer._ar_code + " = '" + _oldDocNo + "'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._screenTop._loadData(((DataSet)_getData[0]).Tables[0]);
                this._screen_ar_detail_1._loadData(((DataSet)_getData[1]).Tables[0]);
                this._screen_ar_detail_2._loadData(((DataSet)_getData[1]).Tables[0]);
                this._screen_ar_detail_3._loadData(((DataSet)_getData[1]).Tables[0]);
                this._screen_ar_detail_4._loadData(((DataSet)_getData[1]).Tables[0]);
                this._screen_ar_detail_5._loadData(((DataSet)_getData[1]).Tables[0]);
                this._screen_ar_contact_grid1._loadFromDataTable(((DataSet)_getData[2]).Tables[0]);
                this._screen_ar_item_grid1._loadFromDataTable(((DataSet)_getData[3]).Tables[0]);
                this._screenTop._search(true);
                this._screen_ar_detail_1._search(false);
                this._screen_ar_detail_2._search(false);
                this._screen_ar_detail_3._search(false);
                this._screen_ar_detail_4._search(false);
                this._screen_ar_detail_5._search(false);
                this._screenTop._isChange = false;
                _getPicture1._clearpic();
                _getPicture1._loadImage("CONTRACT" + this._screenTop._getDataStr(_g.d.ar_customer._code));

                //if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                //{
                //    string __getReason = ((DataSet)_getData[1]).Tables[0].Rows[0][_g.d.ar_customer_detail._close_reason].ToString();

                //    MyLib._myComboBox __reasonCombo = (MyLib._myComboBox)this._screen_ar_detail_2._getControl(_g.d.ar_customer_detail._close_reason);
                //    __reasonCombo.Text = __getReason;
                //}

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
