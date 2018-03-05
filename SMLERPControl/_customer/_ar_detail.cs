using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

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
        decimal _creditAmount = 0M;
        int _creditStatus = 0;

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

            Control _arm_approve_date_Control = this._screen_customer._getControl(_g.d.ar_customer_detail._arm_approve_date);
            Control _arm_ar_shoptype1_code = this._screen_customer._getControl(_g.d.ar_customer_detail._ar_shoptype1_code);
            Control _arm_ar_shoptype2_code = this._screen_customer._getControl(_g.d.ar_customer_detail._ar_shoptype2_code);
            Control _arm_ar_shoptype3_code = this._screen_customer._getControl(_g.d.ar_customer_detail._ar_shoptype3_code);
            Control _arm_ar_shoptype4_code = this._screen_customer._getControl(_g.d.ar_customer_detail._ar_shoptype4_code);
            Control _arm_ar_shoptype5_code = this._screen_customer._getControl(_g.d.ar_customer_detail._ar_shoptype5_code);
            Control _arm_sub_ar_shoptype5_code = this._screen_customer._getControl(_g.d.ar_customer_detail._sub_ar_shoptype5_code);

            _arm_approve_date_Control.Enabled = false;
            _arm_ar_shoptype1_code.Enabled = false;
            _arm_ar_shoptype2_code.Enabled = false;
            _arm_ar_shoptype3_code.Enabled = false;
            _arm_ar_shoptype4_code.Enabled = false;
            _arm_ar_shoptype5_code.Enabled = false;
            _arm_sub_ar_shoptype5_code.Enabled = false;
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
                if (this._myManageData1._isEdit == false)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning55"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
                            ArrayList __getData6 = this._screen_customer._createQueryForDatabase();

                            string __dataList = this._screenTop._getDataStrQuery(_g.d.ar_customer._code) + ",";
                            string __fieldList_1 = _g.d.ar_customer._code + ",";

                            StringBuilder __myQuery = new StringBuilder();

                            string __dataList_1 = this._screenTop._getDataStrQuery(_g.d.ar_customer._code) + ",";
                            string __dataListUpdate = "  where " + _g.d.ar_customer_detail._ar_code + " = " + this._screenTop._getDataStrQuery(_g.d.ar_customer._code);

                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ar_contactor._table + " " + __dataListUpdate));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ar_item_by_customer._table + " " + __dataListUpdate));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ar_customer_detail._table + " " + __dataListUpdate));

                            // ไม่จำเป็นต้องแก้ไข tab แรก
                            //if (_myManageData1._mode == 1)
                            //{
                            //    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData1_1[0].ToString() + ") values (" + __getData1_1[1].ToString() + ")"));
                            //}
                            //else
                            //{
                            //    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + __getData1_1[2].ToString() + _myManageData1._dataList._whereString));
                            //}


                            string __extraField = "";
                            string __extraValue = "";
                            //if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            //{
                            //    MyLib._myComboBox __reasonCombo = (MyLib._myComboBox)this._screen_ar_detail_2._getControl(_g.d.ar_customer_detail._close_reason);

                            //    __extraField = "," + _g.d.ar_customer_detail._close_reason;
                            //    __extraValue = ",\'" + __reasonCombo.Text + "\'";
                            //}
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_customer_detail._table + " (ar_code," + __getData1[0].ToString() + "," + __getData2[0].ToString() + "," + __getData3[0].ToString() + "," + __getData4[0].ToString() + "," + __getData5[0].ToString() + "," + __getData6[0].ToString() + __extraField + ") values (" + __dataList_1 + "" + __getData1[1].ToString() + "," + __getData2[1].ToString() + "," + __getData3[1].ToString() + "," + __getData4[1].ToString() + "," + __getData5[1].ToString() + "," + __getData6[1].ToString() + __extraValue + ")"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ar_customer set " +  _g.d.ar_customer._last_update_time + " = now() where " + _g.d.ar_customer._code + "= " + this._screenTop._getDataStrQuery(_g.d.ar_customer._code) + " "));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ar_customer set " + _g.d.ar_customer._arm_tier + " = "+ this._screen_customer._getDataStrQuery(_g.d.ar_customer_detail._arm_tier) +" where " + _g.d.ar_customer._code + "= " + this._screenTop._getDataStrQuery(_g.d.ar_customer._code) + " "));


                            string __result_images = this._getPicture1._updateImage("CONTRACT" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                            string __result_images2 = this._getPicture2._updateImage("CONTRACT#6#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                            string __result_images3 = this._getPicture3._updateImage("CONTRACT#7#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                            string __result_images4 = this._getPicture4._updateImage("CONTRACT#8#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                            string __result_images5 = this._getPicture5._updateImage("CONTRACT#9#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                            string __result_images6 = this._getPicture6._updateImage("CONTRACT#10#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                            string __result_images7 = this._getPicture7._updateImage("CONTRACT#11#" + this._screenTop._getDataStr(_g.d.ar_customer._code));

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

                                if (_g.g._companyProfile._arm_send_ar_change && this._myManageData1._mode == 2)
                                {
                                    StringBuilder __message = new StringBuilder();
                                    string __sendTo = _g.g._companyProfile._arm_send_ar_change_to;

                                    // get granch
                                    //__message.Append("แก้ไขลูกค้า " + this._screenTop._getDataStr(_g.d.ar_customer._name_1) + "(" + this._screenTop._getDataStr(_g.d.ar_customer._code) + ") โดย " + MyLib._myGlobal._userName + "(" + MyLib._myGlobal._userCode + ") ");

                                    string __editMessage = "";

                                    if (MyLib._myGlobal._intPhase(this._screen_ar_detail_2._getDataStr(_g.d.ar_customer_detail._credit_status)) != this._creditStatus)
                                    {
                                        __editMessage = "สถานะเครดิต " + ((MyLib._myGlobal._intPhase(this._screen_ar_detail_2._getDataStr(_g.d.ar_customer_detail._credit_status)) == 0) ? "เปิด" : (MyLib._myGlobal._intPhase(this._screen_ar_detail_2._getDataStr(_g.d.ar_customer_detail._credit_status)) == 2) ? "ปิดชั่วคราว" : "ปิด");
                                    }

                                    if (this._screen_ar_detail_2._getDataNumber(_g.d.ar_customer_detail._credit_money) != this._creditAmount)
                                    {
                                        if (__editMessage.Length > 0)
                                        {
                                            __editMessage += " ";
                                        }
                                        __editMessage += "วงเงินเครดิต " + this._creditAmount.ToString("#,###,##0") + " แก้ไขเป็น " + this._screen_ar_detail_2._getDataNumber(_g.d.ar_customer_detail._credit_money).ToString("#,###,##0");
                                    }

                                    __message.Append(string.Format("แก้ไขข้อมูลลูกหนี้ {0} {1} {2} User : {3} {4}",
                                        this._screenTop._getDataStr(_g.d.ar_customer._code)
                                        , this._screenTop._getDataStr(_g.d.ar_customer._name_1)
                                        , __editMessage
                                        , MyLib._myGlobal._userCode
                                        , DateTime.Now.ToString("yyyyMMddHHmmss", new CultureInfo("en-US"))));

                                    DataTable __sendCancelBranch = _myFrameWork._queryShort("select " + _g.d.erp_branch_list._arm_send_ar_change_to + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + " = \'" + MyLib._myGlobal._branchCode + "\' ").Tables[0];
                                    if (__sendCancelBranch.Rows.Count > 0 && __sendCancelBranch.Rows[0][0].ToString().Length > 0)
                                    {
                                        __sendTo = __sendCancelBranch.Rows[0][0].ToString();
                                    }

                                    SMLERPMailMessage._sendMessage._sendMessageSaleHub(__sendTo, __message.ToString(), "");
                                }

                                this._screenTop._isChange = false;
                                if (_myManageData1._mode == 1)
                                {
                                    _myManageData1._afterInsertData();
                                }
                                else
                                {
                                    _myManageData1._afterUpdateData();
                                }
                                //this._myManageData1._newData(true);
                                _myManageData1__clearData();
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
            this._creditAmount = 0M;
            this._creditStatus = 0;

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
            //this._screenTop._setDataStr(_g.d.ar_customer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code), "", true);
            if (this._screenTop._getControl(_g.d.ar_customer._name_1).GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox getText = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ar_customer._name_1);
                getText.textBox.Focus();
                getText.textBox.SelectAll();
            }
        }

        void _myManageData1__newDataClick()
        {
            this._creditAmount = 0M;
            this._creditStatus = 0;

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
                this._screen_customer._clear();
                this._screen_ar_contact_grid1._clear();
                this._screen_ar_item_grid1._clear();

                ArrayList __rowDataArray = (ArrayList)rowData;
                string _oldDocNo = __rowDataArray[_get_column_number()].ToString();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _myManageData1._dataList._tableName + whereString));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * , (select " + _g.d.ar_customer._arm_tier + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + ") as " + _g.d.ar_customer_detail._arm_tier + ", (select " + _g.d.ar_customer._arm_approve_date + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + ") as " + _g.d.ar_customer_detail._arm_approve_date + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + " = '" + _oldDocNo + "'"));
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
                this._screen_customer._loadData(((DataSet)_getData[1]).Tables[0]);

                DataSet getData = (DataSet)_getData[1];
                if (getData.Tables.Count > 0 && getData.Tables[0].Rows.Count > 0)
                {
                    this._screen_customer._setDataDate(_g.d.ar_customer_detail._arm_approve_date, MyLib._myGlobal._convertDateFromQuery(getData.Tables[0].Rows[0][_g.d.ar_customer_detail._arm_approve_date].ToString()));
                }
                this._screen_ar_contact_grid1._loadFromDataTable(((DataSet)_getData[2]).Tables[0]);
                this._screen_ar_item_grid1._loadFromDataTable(((DataSet)_getData[3]).Tables[0]);
                this._screenTop._search(true);
                this._screen_ar_detail_1._search(false);
                this._screen_ar_detail_2._search(false);
                this._screen_ar_detail_3._search(false);
                this._screen_ar_detail_4._search(false);
                this._screen_ar_detail_5._search(false);
                this._screen_customer._search(true);
                this._screenTop._isChange = false;
                _getPicture1._clearpic();
                _getPicture1._loadImage("CONTRACT" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                //เนสเพิ่มรูปภาพ อีก 6 ประเภท
                _getPicture2._clearpic();
                _getPicture2._loadImage("CONTRACT#6#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                _getPicture3._clearpic();
                _getPicture3._loadImage("CONTRACT#7#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                _getPicture4._clearpic();
                _getPicture4._loadImage("CONTRACT#8#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                _getPicture5._clearpic();
                _getPicture5._loadImage("CONTRACT#9#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                _getPicture6._clearpic();
                _getPicture6._loadImage("CONTRACT#10#" + this._screenTop._getDataStr(_g.d.ar_customer._code));
                _getPicture7._clearpic();
                _getPicture7._loadImage("CONTRACT#11#" + this._screenTop._getDataStr(_g.d.ar_customer._code));

                //if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                //{
                //    string __getReason = ((DataSet)_getData[1]).Tables[0].Rows[0][_g.d.ar_customer_detail._close_reason].ToString();

                //    MyLib._myComboBox __reasonCombo = (MyLib._myComboBox)this._screen_ar_detail_2._getControl(_g.d.ar_customer_detail._close_reason);
                //    __reasonCombo.Text = __getReason;
                //}

                this._creditAmount = this._screen_ar_detail_2._getDataNumber(_g.d.ar_customer_detail._credit_money);
                this._creditStatus = MyLib._myGlobal._intPhase(this._screen_ar_detail_2._getDataStr(_g.d.ar_customer_detail._credit_status));



                if (forEdit)
                {
                    this._screenTop._focusFirst();
                    this._getPicture1._setEnable(true);
                    this._getPicture2._setEnable(true);
                    this._getPicture3._setEnable(true);
                    this._getPicture4._setEnable(true);
                    this._getPicture5._setEnable(true);
                    this._getPicture6._setEnable(true);
                    this._getPicture7._setEnable(true);
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
