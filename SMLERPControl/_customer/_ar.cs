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

namespace SMLERPControl._customer
{
    public partial class _ar : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        _screen_ar_main _ar_detail_screen;
        string _oldCode = "";

        public _ar()
        {
            this.SuspendLayout();
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_ar_customer", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.ar_customer._code, 1);
            _myManageData1._manageButton = this._myToolbar;
            //_myManageData1._manageBackgroundPanel = this._myPanel1;

            if (_g.g._companyProfile._customer_by_branch && _g.g._companyProfile._change_branch_code == false)
            {
                _myManageData1._dataList._extraWhere = _g.d.ar_customer._ar_branch_code + "=\'" + _g.g._companyProfile._branch_code + "\' ";
            }

            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screen_ar1__saveKeyDown);

            // toe
            this._gridDealer._queryForUpdateWhere += new MyLib.QueryForUpdateWhereEventHandler(_gridDealer__queryForUpdateWhere);
            this._gridDealer._queryForUpdateCheck += new MyLib.QueryForUpdateCheckEventHandler(_gridDealer__queryForUpdateCheck);
            this._gridDealer._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_gridDealer__queryForInsertCheck);
            this._gridDealer._queryForRowRemoveCheck += new MyLib.QueryForRowRemoveCheckEventHandler(_gridDealer__queryForRowRemoveCheck);

            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            //_myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;
            this.Disposed += new EventHandler(_ar_Disposed);
            this._myToolbar.Renderer = new Renderers.WindowsVistaRenderer();
            this.ResumeLayout(false);

            _g._utils __utils = new _g._utils();
            __utils._updateInventoryMaster("");
            Thread __thread = new Thread(new ThreadStart(__utils._updateCustomerMasterFunction));
            __thread.Start();

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
            {
                this._myTab1.Dispose();
                this._myPanel1.Controls.Clear();


                _ar_detail_screen = new _screen_ar_main();
                _ar_detail_screen._controlName = _controlTypeEnum.ArDetail;
                _ar_detail_screen.Dock = DockStyle.Top;
                this._myPanel1.Controls.Add(_ar_detail_screen);
                this._myPanel1.Controls.Add(this._screenTop);
                //this._myPanel1.Controls.Add()
            }

            if (MyLib._myGlobal._isUserLockDocument == true)
            {
                this._myManageData1._dataList._isLockDoc = true;
                this._myManageData1._dataList._buttonUnlockDoc.Visible = true;
                this._myManageData1._dataList._buttonLockDoc.Visible = true;
                this._myManageData1._dataList._separatorLockDoc.Visible = true;
            }

        }

        bool _gridDealer__queryForRowRemoveCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return true;
            }
            // เป็นค่าว่างหรือไม่ (ให้ลบออก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return true;
            }
            return false;
        }

        bool _gridDealer__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            return true;

        }

        bool _gridDealer__queryForUpdateCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            // ดูว่าเป็น row_number เป็น 0 หรือไม่ ถ้าเป็น 0 คือเป็นรายการใหม่ (ห้ามใช้คำสั่ง Update เดี๋ยวมันจะไป Insert ให้ต่อไป)
            if (((int)sender._cellGet(row, sender._rowNumberName)) == 0)
            {
                return false;
            }
            return true;
        }

        string _gridDealer__queryForUpdateWhere(MyLib._myGrid sender, int row)
        {
            int __getInt = (int)sender._cellGet(row, sender._rowNumberName);
            return (sender._rowNumberName + "=" + __getInt.ToString());
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
                this._screenTop._setDataStr(_g.d.ar_customer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code), "", true);
                return true;
            }
            if (keyData == Keys.F12 && this._myToolbar.Enabled == true)
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
                if (this._myManageData1._mode == 1 && this._myManageData1._isAdd == false)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning19"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool __pass = true;

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
                            if (MyLib._myGlobal._isCheckRuningBeforSave && _myManageData1._mode == 1)
                            {
                                this._screenTop._setDataStr(_g.d.ar_customer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code));
                            }

                            // check duplicate ar
                            if (_g.g._companyProfile._check_ar_duplicate_name && this._myManageData1._mode == 1)
                            {
                                string __checkArNameQuery = "select count(*) as xcount from ar_customer where name_1 = \'" + this._screenTop._getDataStr(_g.d.ar_customer._name_1).Trim() + "\' ";
                                DataTable __checkNameTable = _myFrameWork._queryShort(__checkArNameQuery).Tables[0];
                                if (__checkNameTable.Rows.Count > 0 && MyLib._myGlobal._intPhase(__checkNameTable.Rows[0][0].ToString()) > 0)
                                {
                                    MessageBox.Show("ชื่อลูกค้า : " + this._screenTop._getDataStr(_g.d.ar_customer._name_1).Trim() + " ซ้ำ ");
                                    __pass = false;
                                }
                            }

                            if (__pass)
                            {
                                if (this._myManageData1._mode == 2)
                                {
                                    // ถ้าเป็นการแก้ไข หาก code ไม่ตรงให้เอาของเก่า
                                    string __getCode = this._screenTop._getDataStr(_g.d.ar_customer._code);
                                    if (__getCode != this._oldCode)
                                    {
                                        this._screenTop._setDataStr(_g.d.ar_customer._code, this._oldCode, "", true);
                                    }

                                }

                                ArrayList __getData = this._screenTop._createQueryForDatabase();
                                String _cust_code = this._screenTop._getDataStr(_g.d.ar_customer._code);
                                StringBuilder __myQuery = new StringBuilder();

                                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                                string __saleShiftField = "";
                                string __saleShiftValue = "";
                                string __saleShiftUpdate = "";
                                if (_g.g._companyProfile._use_sale_shift == true)
                                {
                                    __saleShiftField = "," + _g.d.ar_customer._sale_shift_id;
                                    __saleShiftValue = ",\'" + _g.g._companyProfile._sale_shift_id + "\'";
                                    __saleShiftUpdate = "," + _g.d.ar_customer._sale_shift_id + "=\'" + _g.g._companyProfile._sale_shift_id + "\' ";
                                }

                                if (_myManageData1._mode == 1)
                                {
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_customer._table + " (" + __getData[0].ToString() + __saleShiftField + ") values (" + __getData[1].ToString() + __saleShiftValue + ")"));

                                    string __detailField = _g.d.ar_customer_detail._ar_code + "," + _g.d.ar_customer_detail._tax_id + "," + _g.d.ar_customer_detail._card_id + "," + _g.d.ar_customer_detail._branch_type + "," + _g.d.ar_customer_detail._branch_code;
                                    string __detailValue = "\'" + _cust_code + "\', \'" + this._screenTop._getDataStr(_g.d.ar_customer._tax_id) + "\', \'" + this._screenTop._getDataStr(_g.d.ar_customer._card_id) + "\', \'" + this._screenTop._getDataStr(_g.d.ar_customer._branch_type) + "\', \'" + this._screenTop._getDataStr(_g.d.ar_customer._branch_code) + "\' ";

                                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                                    {
                                        __detailField = _g.d.ar_customer_detail._ar_code;// + ","; + _g.d.ar_customer_detail._tax_id + "," + _g.d.ar_customer_detail._card_id + "," + _g.d.ar_customer_detail._branch_type + "," + _g.d.ar_customer_detail._branch_code;
                                        __detailValue = "\'" + _cust_code + "\'"; // +  ", \'" + this._screenTop._getDataStr(_g.d.ar_customer._tax_id) + "\', \'" + this._screenTop._getDataStr(_g.d.ar_customer._card_id) + "\', \'" + this._screenTop._getDataStr(_g.d.ar_customer._branch_type) + "\', \'" + this._screenTop._getDataStr(_g.d.ar_customer._branch_code) + "\' ";


                                        ArrayList __getDataCustomerDetail = _ar_detail_screen._createQueryForDatabase();
                                        __detailField += "," + __getDataCustomerDetail[0].ToString();
                                        __detailValue += "," + __getDataCustomerDetail[1].ToString();
                                    }

                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_customer_detail._table + " (" + __detailField + ") values (" + __detailValue + ")"));
                                    // toe add save dealer
                                    String __q1 = this._gridDealer._createQueryForInsert(_g.d.ar_dealer._table, _g.d.ar_dealer._ar_code + ",", "'" + _cust_code + "',");
                                    __myQuery.Append(__q1);
                                }
                                else
                                {
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ar_customer._table + " set " + __getData[2].ToString() + __saleShiftUpdate + _myManageData1._dataList._whereString));

                                    string __updateDetail = _g.d.ar_customer_detail._tax_id + "=\'" + this._screenTop._getDataStr(_g.d.ar_customer._tax_id) + "\'," +
                                        _g.d.ar_customer_detail._card_id + "=\'" + this._screenTop._getDataStr(_g.d.ar_customer._card_id) + "\'," +
                                        _g.d.ar_customer_detail._branch_type + "=\'" + this._screenTop._getDataStr(_g.d.ar_customer._branch_type) + "\'," +
                                        _g.d.ar_customer_detail._branch_code + "=\'" + this._screenTop._getDataStr(_g.d.ar_customer._branch_code) + "\' ";



                                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                                    {
                                        ArrayList __getDataCustomerDetail = _ar_detail_screen._createQueryForDatabase();
                                        __updateDetail += "," + __getDataCustomerDetail[2].ToString();
                                    }

                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ar_customer_detail._table + " set " + __updateDetail + " where " + _g.d.ar_customer_detail._ar_code + "=\'" + _cust_code + "\' "));

                                    //__myQuery.Append(this._saleGrid1._myGrid1._createQueryRowRemove(_g.d.as_asset_sale_detail._table));
                                    //// อย่าลืม Event _queryForUpdateWhere ไม่งั้นมันไม่ทำงานนะ
                                    //string __fieldUpdate = _g.d.as_asset_sale_detail._doc_no + "=\'" + _oldDocNo + "\'";
                                    //__myQuery.Append(this._saleGrid1._myGrid1._createQueryForUpdate(_g.d.as_asset_sale_detail._table, __fieldUpdate));
                                    //// ต่อท้ายด้วย Insert บรรทัดใหม่
                                    //__myQuery.Append(this._saleGrid1._myGrid1._createQueryForInsert(_g.d.as_asset_sale_detail._table, fieldList, dataList, true));

                                    __myQuery.Append(this._gridDealer._createQueryRowRemove(_g.d.ar_dealer._table));
                                    // อย่าลืม Event _queryForUpdateWhere ไม่งั้นมันไม่ทำงานนะ
                                    string __fieldUpdate = _g.d.ar_dealer._ar_code + "=\'" + _cust_code + "\'";
                                    __myQuery.Append(this._gridDealer._createQueryForUpdate(_g.d.ar_dealer._table, __fieldUpdate));

                                    // ต่อท้ายด้วย Insert บรรทัดใหม่
                                    __myQuery.Append(this._gridDealer._createQueryForInsert(_g.d.ar_dealer._table, _g.d.ar_dealer._ar_code + ",", "'" + _cust_code + "',", true));

                                    //string __q2 = this._gridDealer._createQueryForUpdate(_g.d.ar_dealer._table, MyLib._myGlobal._fieldAndComma(_g.d.ar_dealer._regist_date, _g.d.ar_dealer._expire_date));
                                    //__myQuery.Append(__q2);
                                }
                                //




                                __myQuery.Append("</node>");
                                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                if (result.Length == 0)
                                {
                                    MyLib._myGlobal._displayWarning(1, null);

                                    if (_g.g._companyProfile._arm_send_ar_change && this._myManageData1._mode == 2)
                                    {
                                        StringBuilder __message = new StringBuilder();
                                        string __sendTo = _g.g._companyProfile._arm_send_cn_to;

                                        // get granch
                                        __message.Append("แก้ไขลูกค้า " + this._screenTop._getDataStr(_g.d.ar_customer._name_1) + "(" + this._screenTop._getDataStr(_g.d.ar_customer._code) + ") โดย " + MyLib._myGlobal._userName + "(" + MyLib._myGlobal._userCode + ") ");

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

                int __columnArCodeIndex = this._myManageData1._dataList._gridData._findColumnByName(_g.d.ar_customer._table + "." + _g.d.ar_customer._code);

                for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                {
                    MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];

                    string __ar_code = this._myManageData1._dataList._gridData._cellGet(getData.row, __columnArCodeIndex).ToString();

                    Boolean __pass = true;
                    {
                        string __quey = "select (select count(*) from ic_trans where ic_trans.cust_code = ar_customer.code ) as count_ic_trans, (select count(*) from ap_ar_trans where ap_ar_trans.cust_code = ar_customer.code ) as count_ap_ar_trans from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + MyLib._myUtil._convertTextToXml(__ar_code).ToUpper() + "\'";
                        DataTable __getCount = _myFrameWork._queryShort(__quey).Tables[0];
                        int __countICTrans = (int)MyLib._myGlobal._decimalPhase(__getCount.Rows[0][0].ToString());
                        int __countApArTrans = (int)MyLib._myGlobal._decimalPhase(__getCount.Rows[0][1].ToString());
                        if (__countICTrans > 0 || __countApArTrans > 0)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ลูกหนี้มีการใช้ไปแล้ว") + " " + MyLib._myGlobal._resource("ห้ามลบ") + " : " + __ar_code);
                            __pass = false;
                        }
                    }

                    if (__pass)
                    {
                        __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _g.d.ar_customer._table, getData.whereString));
                        __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _g.d.ar_customer_detail._table, " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer_detail._ar_code) + "=\'" + __ar_code.ToUpper() + "\'"));
                        __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _g.d.ar_dealer._table, " where " + MyLib._myGlobal._addUpper(_g.d.ar_dealer._ar_code) + "=\'" + __ar_code.ToUpper() + "\'"));
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
            this._oldCode = "";
            Control codeControl = this._screenTop._getControl(_g.d.ar_customer._code);
            codeControl.Enabled = true;
            this._screenTop._setDataStr(_g.d.ar_customer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code), "", true);
            if (this._screenTop._getControl(_g.d.ar_customer._name_1).GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox getText = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ar_customer._name_1);
                getText.textBox.Focus();
                getText.textBox.SelectAll();
            }
            if (_g.g._companyProfile._use_sale_shift == true)
            {
                this._screenTop._setDataStr(_g.d.ar_customer._remark, "Create : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US")));
            }
            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                this._screenTop._setDataStr(_g.d.ar_customer._ar_branch_code, _g.g._companyProfile._branch_code);
            }
        }

        void _myManageData1__newDataClick()
        {
            this._screenTop._clear();
            this._oldCode = "";
            this._gridDealer._clear();
            Control codeControl = this._screenTop._getControl(_g.d.ar_customer._code);
            codeControl.Enabled = true;
            this._screenTop._setDataStr(_g.d.ar_customer._code, MyLib._myGlobal._getAutoRun(_g.d.ar_customer._table, _g.d.ar_customer._code), "", true);
            if (this._screenTop._getControl(_g.d.ar_customer._name_1).GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox getText = (MyLib._myTextBox)this._screenTop._getControl(_g.d.ar_customer._name_1);
                getText.textBox.Focus();
                getText.textBox.SelectAll();
            }
            if (_g.g._companyProfile._use_sale_shift == true)
            {
                this._screenTop._setDataStr(_g.d.ar_customer._remark, "Create : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US")));
            }

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                this._screenTop._setDataStr(_g.d.ar_customer._ar_branch_code, _g.g._companyProfile._branch_code);
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
            this._screenTop._clear();
            this._gridDealer._clear();

            try
            {
                StringBuilder __myQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * " +
                    ", (select " + _g.d.ar_customer_detail._tax_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") as " + _g.d.ar_customer._tax_id +
                    ", (select " + _g.d.ar_customer_detail._card_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") as " + _g.d.ar_customer._card_id +
                    ", (select " + _g.d.ar_customer_detail._branch_type + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") as " + _g.d.ar_customer._branch_type +
                    ", (select " + _g.d.ar_customer_detail._branch_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") as " + _g.d.ar_customer._branch_code +
                    " from " + _g.d.ar_customer._table + whereString));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ar_dealer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_dealer._ar_code) + "=\'" + ((ArrayList)rowData)[1].ToString().ToUpper() + "\'"));
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ar_customer_detail._table + " where ar_code=\'" + ((ArrayList)rowData)[1].ToString().ToUpper() + "\'"));
                }

                __myQuery.Append("</node>");

                ArrayList getDataList = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myQuery.ToString());

                DataSet getData = (DataSet)getDataList[0];
                DataSet dealerData = (DataSet)getDataList[1];

                this._screenTop._loadData(getData.Tables[0]);
                this._oldCode = this._screenTop._getDataStr(_g.d.ar_customer._code);
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                {

                    DataSet __detailData = (DataSet)getDataList[2];
                    this._ar_detail_screen._loadData(__detailData.Tables[0]);
                }

                if (getData.Tables.Count > 0 && getData.Tables[0].Rows.Count > 0)
                {
                    this._screenTop._setDataStr(_g.d.ar_customer._tax_id, getData.Tables[0].Rows[0][_g.d.ar_customer._tax_id].ToString());
                    this._screenTop._setDataStr(_g.d.ar_customer._card_id, getData.Tables[0].Rows[0][_g.d.ar_customer._card_id].ToString());
                    this._screenTop._setDataStr(_g.d.ar_customer._branch_code, getData.Tables[0].Rows[0][_g.d.ar_customer._branch_code].ToString());

                    this._screenTop._setComboBox(_g.d.ar_customer._branch_type, MyLib._myGlobal._intPhase(getData.Tables[0].Rows[0][_g.d.ar_customer._tax_id].ToString()));
                }

                this._gridDealer._loadFromDataTable(dealerData.Tables[0]);
                Control codeControl = this._screenTop._getControl(_g.d.ar_customer._code);
                codeControl.Enabled = false;
                this._screenTop._search(true);
                this._screenTop._isChange = false;
                if (forEdit)
                {
                    this._screenTop._focusFirst();
                }

                //this._oldCode = ((ArrayList)rowData)[1].ToString().ToUpper();
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

    public class _arDealerGrid : MyLib._myGrid
    {

        public _arDealerGrid()
        {
            this._rowNumberWork = true;
            this._table_name = _g.d.ar_dealer._table;
            this._addColumn(_g.d.ar_dealer._code, 1, 1, 40, true, false, true);
            this._addColumn(_g.d.ar_dealer._regist_date, 4, 1, 30, true, false, true);
            this._addColumn(_g.d.ar_dealer._expire_date, 4, 1, 30, true, false, true);
            this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
            //this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_pay_credit__cellComboBoxGet);
            //this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_pay_credit__cellComboBoxItem);

            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;

            this._calcPersentWidthToScatter();
            this.Invalidate();
        }
    }

}
