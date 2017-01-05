using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace SMLInventoryControl
{
    public class _icTransScreenMoreControl : MyLib._myScreen
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private ArrayList __searchScreenMasterList = new ArrayList();
        private SMLERPGlobal._searchProperties __searchScreenProperties = new SMLERPGlobal._searchProperties();
        public string _screen_code = "";
        private _g.g._transControlTypeEnum _icTransControlTypeTemp;
        private int _buildCount = 0;
        private TextBox _searchTextBox;
        private string _searchName = "";
        private ArrayList _search_data_full_buffer = new ArrayList();
        private ArrayList _search_data_full_buffer_name = new ArrayList();
        private int _search_data_full_buffer_addr = -1;
        private MyLib._searchDataFull _search_data_full_pointer;
        private string _old_filed_name = "";

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                if (MyLib._myGlobal._isDesignMode == false)
                {
                    this._icTransControlTypeTemp = value;
                    this._build();
                    this.Invalidate();
                }
            }
            get
            {
                return this._icTransControlTypeTemp;
            }
        }

        public void _newData()
        {
            this._setDataStr(_g.d.ic_trans._branch_code, MyLib._myGlobal._branchCode, "", true);
            this._setDataStr(_g.d.ic_trans._cashier_code, MyLib._myGlobal._userCode, "", true);
        }

        void _build()
        {
            if (this._icTransControlType == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            this._buildCount++;
            if (this._buildCount > 1)
            {
                MessageBox.Show("TransScreenMore : มีการสร้างจอสองครั้ง");
            }
            int __row = 0;
            this._reset();
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this._table_name = _g.d.ic_trans._table;
            this._maxColumn = 2;
            ///
            if (_g.g._companyProfile._branchStatus == 1)
            {
                this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._branch_code, 1, 1, 1, true, false, false);
                if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                {
                    this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._branch_code_to, 1, 1, 1, true, false, false);
                }
                if (_g.g._companyProfile._change_branch_code == false)
                {
                    this._enabedControl(_g.d.ic_trans._branch_code, false);
                    this._enabedControl(_g.d.ic_trans._branch_code_to, false);
                }
                __row++;
            }
            //
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    {
                        this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
                    }
                    break;
            }

            int __column = 0;
            if (_g.g._companyProfile._use_doc_group == 1)
            {
                this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
            }

            if (_g.g._companyProfile._use_unit == 1)
            {
                this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true, true);
                if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                {
                    this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._side_code_to, 1, 1, 1, true, false, true);
                }
            }

            if (__column > 1)
            {
                __row++;
                __column = 0;
            }

            if (_g.g._companyProfile._use_department == 1)
            {
                this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, ((_g.g._companyProfile._warning_department == true) ? false : true), true);
                if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                {
                    this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._department_code_to, 1, 1, 1, true, false, true);
                }

            }
            if (__column > 1)
            {
                __row++;
                __column = 0;
            }

            if (_g.g._companyProfile._use_allocate == 1)
            {
                this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false);
                if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                {
                    this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._allocate_code_to, 1, 1, 1, true, false, true);
                }

            }
            if (__column > 1)
            {
                __row++;
                __column = 0;
            }

            if (_g.g._companyProfile._use_project == 1)
            {
                this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false);
                if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                {
                    this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._project_code_to, 1, 1, 1, true, false, true);
                }

            }
            if (__column > 1)
            {
                __row++;
                __column = 0;
            }

            if (_g.g._companyProfile._use_job == 1)
            {
                this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false);
                if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
                {
                    this._addTextBox(__row, __column++, 1, 0, _g.d.ic_trans._job_code_to, 1, 1, 1, true, false, true);
                }

            }
            if (__column > 1)
            {
                __row++;
                __column = 0;
            }
            if (__column != 0)
            {
                __row++;
            }
            // โต๋ ย้ายไป screen buttom
            /*
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSAccountPro ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS)
            {
                this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._exchange_rate, 1, 2, true, __formatNumber);
            }*/

            //
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cashier_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._user_approve, 1, 1, 1, true, false, true);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._transport_code, 1, 1, 1, true, false, true);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._extra_word, 1, 1, 0, true, false, true, true, true, _g.d.ic_trans._purchase_name);
                    this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                    __row += 2;
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    if (_g.g._companyProfile._sr_ss_credit_check)
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._approve_code, 1, 1, 1, true, false, true);
                        this._enabedControl(_g.d.ic_trans._approve_code, false);

                    }
                    break;
            }
            this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark_2, 2, 1, 0, true, false, true);
            __row += 2;
            this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark_3, 2, 1, 0, true, false, true);
            __row += 2;

            if (this._icTransControlType != _g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก && this._icTransControlType != _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก && this._icTransControlType != _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก)
            {
                this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark_4, 2, 1, 0, true, false, true);

                __row += 2;
                this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark_5, 2, 1, 0, true, false, true);
            }


            //
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icTransScreenMoreControl__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icTransScreenMoreControl__textBoxChanged);
            //
            foreach (Control __getControlAll in this.Controls)
            {
                if (__getControlAll.GetType() == typeof(MyLib._myTextBox))
                {
                    MyLib._myTextBox __getControlTextBox = (MyLib._myTextBox)__getControlAll;
                    if (__getControlTextBox._iconNumber == 1)
                    {
                        // กำหนดให้การค้นหาแบบจอเล็กแสดง
                        __getControlTextBox.textBox.Leave += new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.Enter += new EventHandler(textBox_Enter);
                        __getControlTextBox.textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                }
            }
            this._setDataStr(_g.d.ic_trans._branch_code, MyLib._myGlobal._branchCode, "", true);
            this._setDataStr(_g.d.ic_trans._branch_code_to, MyLib._myGlobal._branchCode, "", true);

            this._setDataStr(_g.d.ic_trans._cashier_code, MyLib._myGlobal._userCode);
            if (_g.g._companyProfile._change_branch_code == false)
            {
                this._enabedControl(_g.d.ic_trans._branch_code, false);
                this._enabedControl(_g.d.ic_trans._branch_code_to, false);
            }

            this._enabedControl(_g.d.ic_trans._cashier_code, false);
            this._enabedControl(_g.d.ic_trans._user_approve, false);
        }

        void _icTransScreenMoreControl__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            if (!this._old_filed_name.Equals(__getControl._name.ToLower()))
            {
                Boolean __found = false;
                this._old_filed_name = __getControl._name.ToLower();
                // jead
                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (__getControl._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(__getControl._name);
                    MyLib._searchDataFull __searchObject = new MyLib._searchDataFull();
                    if (__getControl._iconNumber == 4)
                    {
                        __searchObject.WindowState = FormWindowState.Maximized;
                    }
                    this._search_data_full_buffer.Add(__searchObject);
                }

                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                //
            }
            //   MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._label._field_name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            __searchScreenMasterList.Clear();
            try
            {
                {
                    string __extraWhere = "";
                    __searchScreenMasterList = __searchScreenProperties._setColumnSearch(this._searchName, 0, "", this._screen_code);
                    if (__searchScreenMasterList.Count > 0)
                    {
                        if (!this._search_data_full_pointer._name.Equals(__searchScreenMasterList[0].ToString().ToLower()))
                        {
                            if (this._search_data_full_pointer._name.Length == 0)
                            {
                                this._search_data_full_pointer._name = __searchScreenMasterList[0].ToString();
                                this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                                // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                                this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                                this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                                //
                                this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                                this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                            }
                        }
                        __extraWhere = (__searchScreenMasterList.Count == 3) ? __searchScreenMasterList[2].ToString() : "";
                        MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __extraWhere);
                        if (__getControl._iconNumber == 1)
                        {
                            __getControl.Focus();
                            __getControl.textBox.Focus();
                        }
                        else
                        {
                            this._search_data_full_pointer.Focus();
                            this._search_data_full_pointer._firstFocus();
                        }
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + " : " + __ex.StackTrace.ToString());
            }
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
            SendKeys.Send("{ENTER}");
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _searchAll(string screenName, int row)
        {
            int __columnNumber = 0;
            string __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._search_data_full_pointer.Visible = false;
                this._setDataStr(this._searchName, __result, "", false);
                this._search(true);
            }
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_data_full_pointer != null)
                {
                    this._search_data_full_pointer.Visible = true;
                    this._search_data_full_pointer.Focus();
                    this._search_data_full_pointer._firstFocus();
                }
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            Boolean __found = false;
            int __addr = 0;
            for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
            {
                if (__getControl._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                {
                    __addr = __loop;
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {
                __addr = this._search_data_full_buffer_name.Add(__getControl._name);
                this._search_data_full_buffer.Add((MyLib._searchDataFull)new MyLib._searchDataFull());
            }
            this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
            this._search_data_full_buffer_addr = __addr;
            _icTransScreenMoreControl__textBoxSearch(__getControl);
            __getControl.textBox.Focus();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }

        void _icTransScreenMoreControl__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            if (name.Equals(_g.d.ic_trans._transport_code) ||
                name.Equals(_g.d.ic_trans._branch_code) ||
                name.Equals(_g.d.ic_trans._department_code) ||
                name.Equals(_g.d.ic_trans._project_code) ||
                name.Equals(_g.d.ic_trans._allocate_code) ||
                name.Equals(_g.d.ic_trans._side_code) ||
                name.Equals(_g.d.ic_trans._job_code))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = __getControl._label._field_name;
                this._search(true);
            }
        }

        bool _searchAndWarning(string fieldName, DataSet __dataResult, Boolean warning)
        {
            bool __result = false;
            string __getData = "";
            string __getDataStr = this._getDataStr(fieldName);
            string __getDataStr1 = this._getDataStr(fieldName);
            this._setDataStr(fieldName, __getDataStr, __getData, true);
            if (__dataResult.Tables[0].Rows.Count > 0)
            {
                __getData = __dataResult.Tables[0].Rows[0][0].ToString();
            }
            this._setDataStr(fieldName, __getDataStr, __getData, true);
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (__dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        //MessageBox.Show(MyLib._myGlobal._resource("เอกสารเลขที่ หรือ ยอดตัดจ่าย ห้ามว่าง"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        MessageBox.Show("ไม่พบข้อมูล : " + ((MyLib._myTextBox)this._searchTextBox.Parent)._labelName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }

        public void _search(bool warning)
        {
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            //
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.transport_type._name_1 + " from " + _g.d.transport_type._table + " where " + _g.d.transport_type._code + "=\'" + this._getDataStr(_g.d.ic_trans._transport_code) + "\'"));
            //
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._department_code) + "\'"));
            //
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_branch_list._name_1 + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._branch_code) + "\'"));
            //
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_currency._name_1 + " from " + _g.d.erp_currency._table + " where " + _g.d.erp_currency._code + "=\'" + this._getDataStr(_g.d.ic_trans._currency_code) + "\'"));
            //
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_project_list._name_1 + " from " + _g.d.erp_project_list._table + " where " + _g.d.erp_project_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._project_code) + "\'"));
            //
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_allocate_list._name_1 + " from " + _g.d.erp_allocate_list._table + " where " + _g.d.erp_allocate_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._allocate_code) + "\'"));
            //
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_side_list._name_1 + " from " + _g.d.erp_side_list._table + " where " + _g.d.erp_side_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._side_code) + "\'"));
            //
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_job_list._name_1 + " from " + _g.d.erp_job_list._table + " where " + _g.d.erp_job_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._job_code) + "\'"));
            if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._department_code_to) + "\'"));
                //
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_branch_list._name_1 + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._branch_code_to) + "\'"));
                //
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_project_list._name_1 + " from " + _g.d.erp_project_list._table + " where " + _g.d.erp_project_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._project_code_to) + "\'"));
                //
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_allocate_list._name_1 + " from " + _g.d.erp_allocate_list._table + " where " + _g.d.erp_allocate_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._allocate_code_to) + "\'"));
                //
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_side_list._name_1 + " from " + _g.d.erp_side_list._table + " where " + _g.d.erp_side_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._side_code_to) + "\'"));
                //
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_job_list._name_1 + " from " + _g.d.erp_job_list._table + " where " + _g.d.erp_job_list._code + "=\'" + this._getDataStr(_g.d.ic_trans._job_code_to) + "\'"));

            }

            __myquery.Append("</node>");
            ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            _searchAndWarning(_g.d.ic_trans._transport_code, (DataSet)__getData[0], warning);
            _searchAndWarning(_g.d.ic_trans._department_code, (DataSet)__getData[1], warning);
            _searchAndWarning(_g.d.ic_trans._branch_code, (DataSet)__getData[2], warning);
            _searchAndWarning(_g.d.ic_trans._currency_code, (DataSet)__getData[3], warning);
            _searchAndWarning(_g.d.ic_trans._project_code, (DataSet)__getData[4], warning);
            _searchAndWarning(_g.d.ic_trans._allocate_code, (DataSet)__getData[5], warning);
            _searchAndWarning(_g.d.ic_trans._side_code, (DataSet)__getData[6], warning);
            _searchAndWarning(_g.d.ic_trans._job_code, (DataSet)__getData[7], warning);
            if (this._icTransControlType == _g.g._transControlTypeEnum.สินค้า_โอนออก)
            {
                _searchAndWarning(_g.d.ic_trans._department_code_to, (DataSet)__getData[8], warning);
                _searchAndWarning(_g.d.ic_trans._branch_code_to, (DataSet)__getData[9], warning);
                _searchAndWarning(_g.d.ic_trans._project_code_to, (DataSet)__getData[10], warning);
                _searchAndWarning(_g.d.ic_trans._allocate_code_to, (DataSet)__getData[11], warning);
                _searchAndWarning(_g.d.ic_trans._side_code_to, (DataSet)__getData[12], warning);
                _searchAndWarning(_g.d.ic_trans._job_code_to, (DataSet)__getData[13], warning);

            }
        }
    }
}
