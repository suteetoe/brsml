using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPPControl
{
    public class _shipmentScreenTop : MyLib._myScreen
    {
        string _old_filed_name = "";
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        ArrayList _searchScreenMasterList = new ArrayList();
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;

        public string _screen_code = "";
        public string _docFormatCode = "";


        private SMLPPGlobal.g._ppControlTypeEnum _transControlType = SMLPPGlobal.g._ppControlTypeEnum.ว่าง;

        public SMLPPGlobal.g._ppControlTypeEnum transControlType
        {
            get
            {
                return this._transControlType;
            }
            set
            {
                this._transControlType = value;
                this.build();
            }
        }

        void build()
        {
            if (this.transControlType == SMLPPGlobal.g._ppControlTypeEnum.ว่าง)
            {
                return;
            }

            this.SuspendLayout();

            this._textBoxSearch += _shipmentScreenTop__textBoxSearch;
            this._textBoxChanged += _shipmentScreenTop__textBoxChanged;

            int __row = 0;
            this._table_name = _g.d.pp_shipment._table;
            this._maxColumn = 2;

            {
                if (this.transControlType != SMLPPGlobal.g._ppControlTypeEnum.ว่าง)
                {
                    switch (this.transControlType)
                    {
                        case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                            {
                                this._addDateBox(__row, 0, 1, 1, _g.d.pp_shipment._doc_date, 1, true);
                                this._addTextBox(__row++, 1, _g.d.pp_shipment._doc_time, 25);
                                this._addTextBox(__row, 0, 1, 1, _g.d.pp_shipment._doc_no, 1, 25, 1, true, false);
                                this._addTextBox(__row++, 1, _g.d.pp_shipment._doc_format_code, 255);
                                //if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") == false)
                                {
                                    this._addTextBox(__row++, 0, 1, 1, _g.d.pp_shipment._cust_code, 2, 25, 1, true, false);
                                }
                                this._addTextBox(__row, 0, _g.d.pp_shipment._doc_ref, 255);
                                this._addDateBox(__row++, 1, 1, 1, _g.d.pp_shipment._doc_ref_date, 1, true);
                                this._addTextBox(__row++, 0, 1, 1, _g.d.pp_shipment._transport_type, 1, 25, 1, true, false);
                                this._addTextBox(__row, 0, 3, 2, _g.d.pp_shipment._remark, 2, 100);
                            }
                            break;
                        case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:
                            {
                                this._table_name = _g.d.ic_trans._table;

                                this._maxColumn = 4;
                                this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                                this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
                                this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                                this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);

                                this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
                                this._addTextBox(__row++, 2, 1, 0, _g.d.ic_trans._contactor, 2, 1, 1, true, false, true);

                                this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                                this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 3, true, false, true);
                                this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                                this._addDateBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);

                                this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._saleType, true, _g.d.ic_trans._sale_type);
                                this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
                                this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                                this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);

                                this._enabedControl(_g.d.ic_trans._doc_date, false);
                                this._enabedControl(_g.d.ic_trans._doc_time, false);
                                this._enabedControl(_g.d.ic_trans._doc_no, false);
                                this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                                this._enabedControl(_g.d.ic_trans._cust_code, false);
                                this._enabedControl(_g.d.ic_trans._contactor, false);
                                this._enabedControl(_g.d.ic_trans._tax_doc_date, false);
                                this._enabedControl(_g.d.ic_trans._tax_doc_no, false);
                                this._enabedControl(_g.d.ic_trans._doc_ref, false);
                                this._enabedControl(_g.d.ic_trans._doc_ref_date, false);
                                this._enabedControl(_g.d.ic_trans._inquiry_type, false);
                                this._enabedControl(_g.d.ic_trans._vat_type, false);
                                this._enabedControl(_g.d.ic_trans._sale_code, false);
                                this._enabedControl(_g.d.ic_trans._sale_group, false);

                            }
                            break;
                    }

                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);

                }


            }

            this.Invalidate();
            this.ResumeLayout();
        }

        public void _newData()
        {
            this._setDataDate(_g.d.pp_shipment._doc_date, DateTime.Now);
            this._setDataStr(_g.d.ic_trans._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"));
        }

        void _shipmentScreenTop__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((TextBox)sender).Parent;


            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;

            if (name.Equals(_g.d.ic_trans._doc_no))
            {
                // ต้องป้อนตัวหน้าก่อน เพื่อจะหารูปแบบเอกสาร
                string __docNo = this._getDataStr(_g.d.ic_trans._doc_no);
                string __docType = __docNo;


                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._tax_format + "," + _g.d.erp_doc_format._doc_running + "," + _g.d.erp_doc_format._vat_type + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    this._docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __docFormatCodeTemp = _docFormatCode;

                    string __startRunningNumber = __getFormat.Rows[0][_g.d.erp_doc_format._doc_running].ToString();
                    string __newDoc = SMLPPGlobal.g._getAutoRun(this.transControlType, __docFormatCodeTemp, this._getDataStr(_g.d.pp_shipment._doc_date).ToString(), __format, SMLPPGlobal.g._transFlagPP(this.transControlType), _g.d.pp_shipment._table, __startRunningNumber);

                    //   _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docFormatCodeTemp, this._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง, _g.d.pp_shipment._table, __startRunningNumber);

                    // __result = MyLib._myGlobal._getAutoRun(tableName, _g.d.ic_trans._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString(), startRunningDoc).ToString();

                    string __taxFormat = __getFormat.Rows[0][_g.d.erp_doc_format._tax_format].ToString();

                    // toe แทรก tax doc no ไปก่อน
                    /*
                    if (_docFormatCode.Length > 0 && __taxFormat.Length > 0 && this._getDataStr(_g.d.ic_trans._tax_doc_date).ToString().Length > 0)
                    {
                        if (__taxFormat.Trim().Length > 0)
                        {
                            // new tax auto run

                            string __newTaxDoc = _g.g._getAutoRun(_g.g._autoRunType.ใบกำกับภาษี, _docFormatCode, this._getDataStr(_g.d.ic_trans._doc_date).ToString(), __taxFormat, this.transControlType, _g.g._transControlTypeEnum.ว่าง, this._icTransTable);
                            this._setDataStr(_g.d.ic_trans._tax_doc_no, __newTaxDoc);
                        }
                    }*/

                    this._setDataStr(_g.d.pp_shipment._doc_no, __newDoc, "", true);
                    this._setDataStr(_g.d.pp_shipment._doc_format_code, _docFormatCode, "", true);

                }
                if (this._docFormatCode.Trim().Length == 0)
                {
                    string __firstString = MyLib._myGlobal._firstString(__docNo);
                    if (__firstString.Length > 0)
                    {
                        this._docFormatCode = __firstString;
                        this._setDataStr(_g.d.pp_shipment._doc_format_code, _docFormatCode, "", true);
                    }
                }
                /*if (this._docFormatCode.Equals(MyLib._myGlobal._firstString(__docNo)) == false)
                {
                    DialogResult __message = MessageBox.Show("ประเภทเอกสารไม่สัมพันธ์กับเลขที่เอกสาร ต้องการเปลี่ยนตามเลขที่เอกสารเลยหรือไม่", "Doc Number", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    if (__message == DialogResult.Yes)
                    {
                        this._docFormatCode = MyLib._myGlobal._firstString(__docNo);
                        this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }*/
            }
        }

        void _shipmentScreenTop__textBoxSearch(object sender)
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
                    __searchObject._dataList._gridData._beforeDisplayRow += _gridData__beforeDisplayRow;
                }

                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                if (__getControl.textBox.Text == "")
                {
                    this._search_data_full_pointer._dataList._searchText.TextBox.Text = "";
                }

            }

            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._label._field_name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            _searchScreenMasterList.Clear();
            string __extraWhere = "";

            try
            {
                _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "", this._screen_code);

                if (this._searchName.Equals(_g.d.pp_shipment._cust_code))
                {
                    _searchScreenMasterList.Clear();
                    _searchScreenMasterList.Add(_g.g._search_screen_ar);
                    _searchScreenMasterList.Add(_g.d.ar_customer._table);

                }
                else if (this._searchName.Equals(_g.d.pp_shipment._transport_type))
                {
                    _searchScreenMasterList.Clear();
                    _searchScreenMasterList.Add(_g.g._search_screen_erp_transport);
                    _searchScreenMasterList.Add(_g.d.transport_type._table);
                }

                if (_searchScreenMasterList.Count > 0)
                {
                    if (this._search_data_full_pointer._name.Equals(_searchScreenMasterList[0].ToString().ToLower()) == false)
                    {
                        if (this._search_data_full_pointer._name.Length == 0)
                        {
                            this._search_data_full_pointer._name = _searchScreenMasterList[0].ToString();
                            this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                            this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                            this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            //
                            this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                            this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                        }
                    }
                    __extraWhere = (_searchScreenMasterList.Count == 3) ? _searchScreenMasterList[2].ToString() : "";
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
            catch (Exception ex)
            {

            }


        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
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


        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;

            return __result;
        }

        void _searchAll(string screenName, int row)
        {
            int __columnNumber = 0;

            /*
            switch (this.transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                    if (screenName.Equals(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ)))
                    {
                        __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                    }
                    break;

            }
            */

            string __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._search_data_full_pointer.Visible = false;
                this._setDataStr(this._searchName, __result, "", false);
                this._search(true);
            }

        }

        public void _search(Boolean warning)
        {
            if (MyLib._myGlobal._isDesignMode)
            {
                return;
            }

            string __custCode = this._getDataStr(_g.d.pp_shipment._cust_code);

            string __querySearchArName = "select " + _g.d.ar_customer._name_1 +
                " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + __custCode.ToUpper() + "\'";

            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader);
            __myquery.Append("<node>");

            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySearchArName));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.transport_type._name_1 + " from " + _g.d.transport_type._table + " where " + MyLib._myGlobal._addUpper(_g.d.transport_type._code) + "=\'" + this._getDataStr(_g.d.pp_shipment._transport_type).ToUpper() + "\'"));

            __myquery.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            _searchAndWarning(_g.d.pp_shipment._cust_code, (DataSet)_getData[0], warning);
            _searchAndWarning(_g.d.pp_shipment._transport_type, (DataSet)_getData[1], warning);

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

                // toe เพิ่ม 20130311
                if (this._searchName.CompareTo(fieldName) == 0 && fieldName.Equals(_g.d.ic_trans._cust_code) == true && __dataResult.Tables[0].Rows.Count == 0 && warning == true)
                {
                    MessageBox.Show("ไม่พบข้อมูล : " + ((MyLib._myTextBox)this._searchTextBox.Parent)._labelName);
                    this._setDataStr(fieldName, "", "", true);
                }

                // jead เอาไว้แก้ทีหลัง
                /*if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (__dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("เอกสารเลขที่ หรือ ยอดตัดจ่าย ห้ามว่าง"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }*/
            }
            return __result;
        }

    }
}
