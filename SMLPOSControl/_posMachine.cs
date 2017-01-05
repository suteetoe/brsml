using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _posMachine : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _posMachine()
        {
            this.SuspendLayout();
            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_pos_machine", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.POS_ID._MACHINECODE, 1);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._manageBackgroundPanel = this._myPanel;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._posMachineScreen1._saveKeyDown += new MyLib.SaveKeyDownHandler(_posMachineScreen1__saveKeyDown);
            this._posMachineScreen1._checkKeyDown += new MyLib.CheckKeyDownHandler(_posMachineScreen1__checkKeyDown);

            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 500;

            this.Disposed += new EventHandler(_side_Disposed);
            //this.Resize += new EventHandler(_side_Resize);
            this.ResumeLayout(false);
        }

        Boolean _posMachineScreen1__checkKeyDown(object sender, Keys keyData)
        {
            if (_myToolBar.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
                this._posMachineScreen1._isChange = false;
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
            _posMachineScreen1._saveLastControl();
            string __getEmtry = _posMachineScreen1._checkEmtryField();
            if (__getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, __getEmtry);
            }
            else
            {
                ArrayList __getData = _posMachineScreen1._createQueryForDatabase();
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
                    _posMachineScreen1._clear();
                    _posMachineScreen1._focusFirst();
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
            Control __codeControl = _posMachineScreen1._getControl(_g.d.POS_ID._MACHINECODE);
            __codeControl.Enabled = true;
            _posMachineScreen1._focusFirst();
        }

        void _posMachineScreen1__saveKeyDown(object sender)
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
            _posMachineScreen1._clear();
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData1__discardData()
        {
            bool __result = true;
            if (_posMachineScreen1._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    __result = false;
                }
                else
                {
                    _posMachineScreen1._isChange = false;
                }
            }
            return (__result);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString);
                _posMachineScreen1._loadData(__getData.Tables[0]);
                Control __codeControl = _posMachineScreen1._getControl(_g.d.POS_ID._MACHINECODE);
                __codeControl.Enabled = false;
                if (forEdit)
                {
                    _posMachineScreen1._focusFirst();
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

    public partial class _posMachineScreen : MyLib._myScreen
    {
        private MyLib._searchDataFull _searchWh = new MyLib._searchDataFull();
        private MyLib._searchDataFull _searchLocation = new MyLib._searchDataFull();
        private MyLib._searchDataFull _searchForm = new MyLib._searchDataFull();

        public _posMachineScreen()
        {
            int __row = 0;

            this._maxColumn = 3;
            this.SuspendLayout();
            this._table_name = _g.d.POS_ID._table;
            this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._MACHINECODE, 1, 25, 0, true, false, false);
            this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._POS_ID, 2, 25, 0, true, false, false);

            this._addTextBox(__row, 0, 1, 0, _g.d.POS_ID._doc_format_code, 1, 25, 1, true, false, true, true, true, _g.d.POS_ID._doc_format_code_inv);
            this._addTextBox(__row, 1, 1, 0, _g.d.POS_ID._doc_format, 1, 25, 0, true, false, true, true, true, _g.d.POS_ID._doc_format_template);
            this._addTextBox(__row++, 2, 1, 0, _g.d.POS_ID._doc_running_inv, 1, 25, 0, true, false, true);

            this._addTextBox(__row, 0, 1, 0, _g.d.POS_ID._doc_format_code_tax_inv, 1, 25, 1, true, false, true);
            this._addTextBox(__row, 1, 1, 0, _g.d.POS_ID._doc_format_vat_full, 1, 25, 0, true, false, true, true, true, _g.d.POS_ID._doc_format_template);
            this._addTextBox(__row++, 2, 1, 0, _g.d.POS_ID._doc_running_vat_full, 1, 25, 0, true, false, true);

            this._addTextBox(__row, 0, 1, 0, _g.d.POS_ID._doc_format_code_doc_cancel, 1, 25, 1, true, false, true);
            this._addTextBox(__row, 1, 1, 0, _g.d.POS_ID._doc_format_cancel, 1, 25, 0, true, false, true, true, true, _g.d.POS_ID._doc_format_template);
            this._addTextBox(__row++, 2, 1, 0, _g.d.POS_ID._doc_running_cancel, 1, 25, 0, true, false, true);

            this._addTextBox(__row, 0, 1, 0, _g.d.POS_ID._doc_format_code_fill_money, 1, 25, 1, true, false, true);
            this._addTextBox(__row, 1, 1, 0, _g.d.POS_ID._doc_format_fillmoney, 1, 25, 0, true, false, true, true, true, _g.d.POS_ID._doc_format_template);
            this._addTextBox(__row++, 2, 1, 0, _g.d.POS_ID._doc_running_fillmoney, 1, 25, 0, true, false, true);

            this._addTextBox(__row, 0, 1, 0, _g.d.POS_ID._doc_format_code_send_money, 1, 25, 1, true, false, true);
            this._addTextBox(__row, 1, 1, 0, _g.d.POS_ID._doc_format_cashierSettle, 1, 25, 0, true, false, true, true, true, _g.d.POS_ID._doc_format_template);
            this._addTextBox(__row++, 2, 1, 0, _g.d.POS_ID._doc_running_cashierSettle, 1, 25, 0, true, false, true);

            this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._pos_ic_wht, 3, 25, 1, true, false, true);
            this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._pos_ic_shelf, 3, 25, 1, true, false, true);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POS_ID._pos_config_number, 1, 0, true);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POS_ID._price_number, 1, 0, true);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POS_ID._slip_number, 1, 0, true);
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong)
            {
                this._addNumberBox(__row++, 0, 1, 0, _g.d.POS_ID._close_form_number, 1, 0, true);
            }

            if (_g.g._companyProfile._deposit_format_from_pos == true)
            {
                this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._doc_format_deposit, 2, 25, 1, true, false, true);
                this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._doc_format_deposit_return, 2, 25, 1, true, false, true);
                this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._doc_format_deposit_cancel, 2, 25, 1, true, false, true);
                this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._doc_format_deposit_return_cancel, 2, 25, 1, true, false, true);
            }
            this._addTextBox(__row, 0, 3, 0, _g.d.POS_ID._pos_header, 3, 25, 0, true, false, true);
            __row += 3;

            MyLib._myGroupBox __group1 = this._addGroupBox(__row++, 0, 1, 2, 3, _g.d.POS_ID._pos_type_invoice, true);
            this._addRadioButtonOnGroupBox(0, 0, __group1, _g.d.POS_ID._pos_semi_invoice, 0, true);
            this._addRadioButtonOnGroupBox(0, 1, __group1, _g.d.POS_ID._pos_full_invoice, 1, false);

            __row++;
            this._addCheckBox(__row++, 0, _g.d.POS_ID._pos_save_e_journal, false, true);

            this._addCheckBox(__row++, 0, _g.d.POS_ID._status, false, true);
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
            {
                this._addCheckBox(__row++, 0, _g.d.POS_ID._show_order_detail, false, true);
                this._addCheckBox(__row++, 0, _g.d.POS_ID._show_in_copy, false, true);
            }
            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLPOSLite)
            {
                this._addCheckBox(__row++, 0, _g.d.POS_ID._use_credit_sale, false, true);
            }

            this._addCheckBox(__row++, 0, _g.d.POS_ID._no_remark_sendmoney, false, true);
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                this._addTextBox(__row, 0, 1, 0, _g.d.POS_ID._doc_format_credit, 1, 25, 0, true, false, true);
                this._addTextBox(__row++, 1, 1, 0, _g.d.POS_ID._doc_running_credit, 1, 25, 0, true, false, true);
                this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._doc_format_code_credit, 2, 25, 1, true, false, true);

            }

            if (_g.g._companyProfile._branchStatus ==1)
            {
                this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._branch_code, 2, 25, 1, true, false, true);
            }

            if (_g.g._companyProfile._use_department == 1)
            {
                this._addTextBox(__row++, 0, 1, 0, _g.d.POS_ID._department_code, 2, 25, 1, true, false, true);
            }

            this.ResumeLayout();
            //
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_posMachineScreen__textBoxSearch);
            //
            this._searchWh.StartPosition = FormStartPosition.CenterScreen;
            this._searchWh.Text = MyLib._myGlobal._resource("ค้นหาคลังสินค้า");
            this._searchWh._dataList._loadViewFormat(_g.g._search_master_ic_warehouse, MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchWh._dataList._gridData._mouseClick += (s1, e1) =>
            {
                string __code = this._searchWh._dataList._gridData._cellGet(this._searchWh._dataList._gridData._selectRow, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();
                this._searchWh.Close();
                this._setDataStr(_g.d.POS_ID._pos_ic_wht, __code);
            };
            this._searchWh._searchEnterKeyPress += (s1, e1) =>
            {
                string __code = this._searchWh._dataList._gridData._cellGet(this._searchWh._dataList._gridData._selectRow, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();
                this._searchWh.Close();
                this._setDataStr(_g.d.POS_ID._pos_ic_wht, __code);
            };
            //
            this._searchLocation.StartPosition = FormStartPosition.CenterScreen;
            this._searchLocation.Text = MyLib._myGlobal._resource("ค้นหาที่เก็บสินค้า");
            this._searchLocation._dataList._loadViewFormat(_g.g._search_master_ic_shelf, MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchLocation._dataList._gridData._mouseClick += (s1, e1) =>
            {
                string __code = this._searchLocation._dataList._gridData._cellGet(this._searchLocation._dataList._gridData._selectRow, _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code).ToString();
                this._searchLocation.Close();
                this._setDataStr(_g.d.POS_ID._pos_ic_shelf, __code);
            };
            this._searchWh._searchEnterKeyPress += (s1, e1) =>
            {
                string __code = this._searchLocation._dataList._gridData._cellGet(this._searchLocation._dataList._gridData._selectRow, _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code).ToString();
                this._searchLocation.Close();
                this._setDataStr(_g.d.POS_ID._pos_ic_shelf, __code);
            };

            this._searchForm._dataList._loadViewFormat(_g.g._screen_erp_doc_format, MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchForm._dataList._gridData._mouseClick += (s1, e1) =>
            {
                string __code = this._searchForm._dataList._gridData._cellGet(this._searchForm._dataList._gridData._selectRow, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                this._searchForm.Close();
                this._setDataStr(_searchName, __code);
            };
            this._searchForm._searchEnterKeyPress += (s1, e1) =>
            {
                string __code = this._searchForm._dataList._gridData._cellGet(this._searchForm._dataList._gridData._selectRow, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                this._searchForm.Close();
                this._setDataStr(_searchName, __code);
            };

        }

        string _searchName = "";
        void _posMachineScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            string __searchName = __getControl._name.ToLower();
            if (__searchName.Equals(_g.d.POS_ID._pos_ic_wht))
            {
                this._searchWh.ShowDialog();
            }
            else
            {
                if (__searchName.Equals(_g.d.POS_ID._pos_ic_shelf))
                {
                    string __whCode = this._getDataStr(_g.d.POS_ID._pos_ic_wht);
                    this._searchLocation._dataList._extraWhere = _g.d.ic_shelf._whcode + "=\'" + __whCode + "\'";
                    this._searchLocation._dataList._refreshData();
                    this._searchLocation.ShowDialog();
                }
                else if (__searchName.Equals(_g.d.POS_ID._department_code))
                {
                    MyLib._searchDataFull __search = new MyLib._searchDataFull();
                    __search.Text = MyLib._myGlobal._resource("ค้นหาแผนก");
                    __search._dataList._loadViewFormat(_g.g._search_master_screen_erp_department_list, MyLib._myGlobal._userSearchScreenGroup, false);
                    __search.StartPosition = FormStartPosition.CenterScreen;
                    __search._dataList._gridData._mouseClick += (s2, e2) =>
                    {
                        object __itemCode = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code);
                        object __itemName = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.erp_department_list._table + "." + _g.d.erp_department_list._name_1);

                        this._setDataStr(_g.d.POS_ID._department_code, __itemCode.ToString(), __itemName.ToString(), true);
                        //this._cellUpdate(this._selectRow, _g.d.ic_specific_search._ic_name, __itemName.ToString(), true);
                        __search.Close();
                        SendKeys.Send("{TAB}");

                    };

                    __search._searchEnterKeyPress += (s2, e2) =>
                    {
                        object __itemCode = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code);
                        object __itemName = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.erp_department_list._table + "." + _g.d.erp_department_list._name_1);

                        this._setDataStr(_g.d.POS_ID._department_code, __itemCode.ToString(), __itemName.ToString(), true);
                        //this._cellUpdate(this._selectRow, _g.d.ic_specific_search._ic_name, __itemName.ToString(), true);
                        __search.Close();
                        SendKeys.Send("{TAB}");

                    };

                    __search.ShowDialog();
                }
                else if (__searchName.Equals(_g.d.POS_ID._doc_format_code) ||
                    __searchName.Equals(_g.d.POS_ID._doc_format_deposit) ||
                    __searchName.Equals(_g.d.POS_ID._doc_format_deposit_return) ||
                    __searchName.Equals(_g.d.POS_ID._doc_format_deposit_cancel) ||
                    __searchName.Equals(_g.d.POS_ID._doc_format_deposit_return_cancel) ||
                    __searchName.Equals(_g.d.POS_ID._doc_format_code_doc_cancel) ||
                    __searchName.Equals(_g.d.POS_ID._doc_format_code_tax_inv) ||
                    __searchName.Equals(_g.d.POS_ID._doc_format_code_fill_money) ||
                    __searchName.Equals(_g.d.POS_ID._doc_format_code_send_money))
                {
                    _searchName = __searchName;
                    string __screenCode = "";
                    if (__searchName.Equals(_g.d.POS_ID._doc_format_deposit))
                    {
                        __screenCode = "SRV";
                    }
                    else if (__searchName.Equals(_g.d.POS_ID._doc_format_deposit_return))
                    {
                        __screenCode = "SRT";
                    }
                    else if (__searchName.Equals(_g.d.POS_ID._doc_format_deposit_return_cancel))
                    {
                        __screenCode = "SCT";
                    }
                    else if (__searchName.Equals(_g.d.POS_ID._doc_format_deposit_cancel))
                    {
                        __screenCode = "SCR";
                    }
                    else if (__searchName.Equals(_g.d.POS_ID._doc_format_code))
                    {
                        __screenCode = "SIP";
                    }
                    else if (__searchName.Equals(_g.d.POS_ID._doc_format_code_doc_cancel))
                    {
                        __screenCode = "SIC";
                    }
                    else if (__searchName.Equals(_g.d.POS_ID._doc_format_code_tax_inv))
                    {
                        __screenCode = "SI";
                    }
                    else if (__searchName.Equals(_g.d.POS_ID._doc_format_code_fill_money))
                    {
                        __screenCode = "FLM";
                    }
                    else if (__searchName.Equals(_g.d.POS_ID._doc_format_code_send_money))
                    {
                        __screenCode = "SDM";
                    }
                    MyLib._myGlobal._startSearchBox(__getControl, "ค้นหารหัสเอกสาร", this._searchForm, false, true, MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'" + __screenCode + "\'");

                }
            }
        }
    }
}
