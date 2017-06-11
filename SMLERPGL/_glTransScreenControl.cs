using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPGL
{
    public partial class _glTransScreenControl : UserControl
    {
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        string _searchName = "";
        TextBox _searchTextBox;
        string _old_filed_name = "";

        string _screenCode = "";

        public _glTransScreenControl()
        {
            InitializeComponent();
            this._gridDetail._isEdit = false;
        }

        void _build()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            int __row = 0;
            if (this._transControlType != _g.g._transControlTypeEnum.ว่าง)
            {

                this._screenTop.AutoSize = true;
                this._screenTop._maxColumn = 2;
                this._screenTop._table_name = _g.d.gl_trans._table;

                this._screenTop._addTextBox(__row, 0, 1, 0, _g.d.gl_trans._account_period, 1, 10, 1, true, false, false, true, false, _g.d.gl_trans._account_period);
                this._screenTop._addTextBox(__row++, 1, 1, 0, _g.d.gl_trans._account_year, 1, 10, 0, true, false, false);

                this._screenTop._addDateBox(__row++, 0, 1, 0, _g.d.gl_trans._doc_date, 1, true);
                //this._screenTop._addTextBox(__row++, 1, 1, 0, _g.d.gl_trans._doc_time, 1, 10, 1, true, false, true);

                this._screenTop._addTextBox(__row, 0, 1, 0, _g.d.gl_trans._doc_no, 1, 10, 1, true, false, false);
                this._screenTop._addTextBox(__row++, 1, 1, 0, _g.d.gl_trans._doc_format_code, 1, 10, 0, true, false, true);

                /*if (this._transControlType == _g.g._transControlTypeEnum.บัญชี_ประมวลผลสิ้นงวด)
                {
                    this._screenTop._addTextBox(__row, 0, 1, 0, _g.d.gl_trans._account_period, 1, 10, 1, true, false, false, true, false, _g.d.gl_trans._account_period);
                    this._screenTop._addTextBox(__row++, 1, 1, 0, _g.d.gl_trans._account_year, 1, 10, 1, true, false, false);

                }
                else
                {
                    this._screenTop._addTextBox(__row, 0, 1, 0, _g.d.gl_trans._account_period, 1, 10, 1, true, false, false, true, false, _g.d.gl_trans._account_period);
                    this._screenTop._addTextBox(__row++, 1, 1, 0, _g.d.gl_trans._account_year, 1, 10, 1, true, false, false);
                }*/


                this._screenTop._addDateBox(__row, 0, 1, 0, _g.d.gl_trans._from_date, 1, true);
                this._screenTop._addDateBox(__row++, 1, 1, 0, _g.d.gl_trans._to_date, 1, true);

                this._screenTop._addTextBox(__row++, 0, 1, 0, _g.d.gl_trans._book_code, 1, 10, 1, true, false, false);
                this._screenTop._addTextBox(__row++, 0, 1, 0, _g.d.gl_trans._close_to_account, 1, 10, 1, true, false, false);
                this._screenTop._addTextBox(__row++, 0, 2, _g.d.gl_trans._remark, 2, 255);



                __row++;

                this._gridDetail._width_by_persent = true;
                this._gridDetail._table_name = _g.d.gl_trans_detail._table;
                this._gridDetail._total_show = true;
                this._gridDetail._addColumn(_g.d.gl_trans_detail._account_code, 1, 25, 15, true, false, true, true);
                this._gridDetail._addColumn(_g.d.gl_trans_detail._account_name, 1, 100, 25, true, false);
                //if (_g.g._companyProfile._branchStatus == 1)
                //{
                //    this._gridDetail._addColumn(this._columnBranchCodeTemp, 1, 25, 15, true, false, false, true, "", "", "", _g.d.gl_journal_detail._branch_code);
                //    this._gridDetail._addColumn(_g.d.gl_journal_detail._branch_code, 1, 25, 15, true, true, true, false);
                //}
                this._gridDetail._addColumn(_g.d.gl_trans_detail._debit, 3, 0, 15, true, false, true, false, __formatNumber);
                this._gridDetail._addColumn(_g.d.gl_trans_detail._credit, 3, 0, 15, true, false, true, false, __formatNumber);
                //

                this._gridDetail._calcPersentWidthToScatter();

                this._screenTop._textBoxSearch += _screenTop__textBoxSearch;
                this._screenTop._textBoxChanged += _screenTop__textBoxChanged;

                this._screenTop._enabedControl(_g.d.as_trans._doc_format_code, false);

                switch (this._transControlTypeTemp)
                {
                    case _g.g._transControlTypeEnum.บัญชี_ประมวลผลสิ้นงวด:
                        _screenCode = "GLPP";
                        this._screenTop._enabedControl(_g.d.gl_trans._account_year, false);
                        break;
                    case _g.g._transControlTypeEnum.บัญชี_ประมวลผลสิ้นปี:
                        this._screenTop._enabedControl(_g.d.gl_trans._account_period, false);
                        _screenCode = "GLPY";
                        break;
                }

                this._screenTop._enabedControl(_g.d.gl_trans._from_date, false);
                this._screenTop._enabedControl(_g.d.gl_trans._to_date, false);
                this._screenTop._enabedControl(_g.d.gl_trans._doc_date, false);

            }
        }

        private void _screenTop__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.as_trans._doc_no))
            {
                // new running
                string _docFormatCode = "";
                string __docNo = this._screenTop._getDataStr(_g.d.as_trans._doc_no);

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._doc_running + "," + _g.d.erp_doc_format._gl_book + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    _docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __startRunningNumber = __getFormat.Rows[0][_g.d.erp_doc_format._doc_running].ToString();

                    //string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docNo, this._screenTop._getDataStr(_g.d.as_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.สินทรัพย์_โอนค่าเสื่อม, _g.g._transControlTypeEnum.ว่าง, _g.d.as_trans._table, __startRunningNumber);
                    string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ว่าง, _docFormatCode, this._screenTop._getDataStr(_g.d.as_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง, _g.d.as_trans._table, __startRunningNumber, _g.d.gl_trans._doc_no, "");
                    this._screenTop._setDataStr(_g.d.gl_trans._doc_no, __newDoc, "", true);
                    this._screenTop._setDataStr(_g.d.gl_trans._doc_format_code, _docFormatCode, "", true);
                    this._screenTop._setDataStr(_g.d.gl_trans._book_code, __getFormat.Rows[0][_g.d.erp_doc_format._gl_book].ToString(), "", true);
                }

                if (_docFormatCode.Trim().Length == 0)
                {
                    string __firstString = MyLib._myGlobal._firstString(__docNo);
                    if (__firstString.Length > 0)
                    {
                        _docFormatCode = __firstString;
                        this._screenTop._setDataStr(_g.d.as_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }
            }
            else if (name.Equals(_g.d.gl_journal._account_period))
            {
                // ดึงปี ดึงจากวันที่เริ่มต้น-สิ้นสุด งวด
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getPeriod = __myFrameWork._queryShort("select " + _g.d.erp_account_period._date_start + "," + _g.d.erp_account_period._date_end + "," + _g.d.erp_account_period._period_year + " from " + _g.d.erp_account_period._table + " where " + _g.d.erp_account_period._period_number + "=\'" + this._screenTop._getDataStr(_g.d.gl_trans._account_period) + "\'").Tables[0];
                if (__getPeriod.Rows.Count > 0)
                {
                    this._screenTop._setDataStr(_g.d.gl_trans._account_year, __getPeriod.Rows[0][_g.d.erp_account_period._period_year].ToString(), "", true);
                    this._screenTop._setDataDate(_g.d.gl_trans._from_date, MyLib._myGlobal._convertDateFromQuery(__getPeriod.Rows[0][_g.d.erp_account_period._date_start].ToString()));
                    this._screenTop._setDataDate(_g.d.gl_trans._to_date, MyLib._myGlobal._convertDateFromQuery(__getPeriod.Rows[0][_g.d.erp_account_period._date_end].ToString()));
                    this._screenTop._setDataDate(_g.d.gl_trans._doc_date, MyLib._myGlobal._convertDateFromQuery(__getPeriod.Rows[0][_g.d.erp_account_period._date_end].ToString()));
                }
            }
            else if (name.Equals(_g.d.gl_journal._account_year))
            {
                // ดึงงวดสุดท้าย ของปี ดึงจากวันที่เริ่มต้น-สิ้นสุด ปี
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getPeriod = __myFrameWork._queryShort("select min(" + _g.d.erp_account_period._date_start + ") as " + _g.d.erp_account_period._date_start + ",max(" + _g.d.erp_account_period._date_end + ") as " + _g.d.erp_account_period._date_end + ",max(" + _g.d.erp_account_period._period_number + ") as " + _g.d.erp_account_period._period_number + " from " + _g.d.erp_account_period._table + " where " + _g.d.erp_account_period._period_year + "=\'" + this._screenTop._getDataStr(_g.d.gl_trans._account_year) + "\'").Tables[0];
                if (__getPeriod.Rows.Count > 0)
                {

                    this._screenTop._setDataStr(_g.d.gl_trans._account_period, __getPeriod.Rows[0][_g.d.erp_account_period._period_number].ToString(), "", true);
                    this._screenTop._setDataDate(_g.d.gl_trans._from_date, MyLib._myGlobal._convertDateFromQuery(__getPeriod.Rows[0][_g.d.erp_account_period._date_start].ToString()));
                    this._screenTop._setDataDate(_g.d.gl_trans._to_date, MyLib._myGlobal._convertDateFromQuery(__getPeriod.Rows[0][_g.d.erp_account_period._date_end].ToString()));
                    this._screenTop._setDataDate(_g.d.gl_trans._doc_date, MyLib._myGlobal._convertDateFromQuery(__getPeriod.Rows[0][_g.d.erp_account_period._date_end].ToString()));
                }
            }
        }

        string _search_screen_neme(string _name)
        {
            if (_name.Equals(_g.d.gl_trans._close_to_account)) return _g.g._search_screen_gl_chart_of_account;
            if (_name.Equals(_g.d.gl_trans._doc_no)) return _g.g._screen_erp_doc_format;

            if (_name.Equals(_g.d.gl_trans._book_code)) return _g.g._search_screen_gl_journal_book;
            if (_name.Equals(_g.d.gl_trans._account_period) || _name.Equals(_g.d.gl_trans._account_year)) return _g.g._search_screen_account_period;
            return "";
        }

        private void _screenTop__textBoxSearch(object sender)
        {
            /*
            this._screenTop._saveLastControl();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            string label_name = __getControl._labelName;
            string _searchWhere = "";
            _searchName = __getControl._name;
            if (_searchName.Equals(_g.d.as_trans._doc_no))
            {
                // search doc no
                if (_searchItem == null)
                {
                    this._searchItem = new MyLib._searchDataFull();
                    _searchTextBox = __getControl.textBox;
                    this._searchItem._dataList._loadViewFormat(_g.g._screen_erp_doc_format, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchItem._name = _searchName;
                    // start search and bind event
                    this._searchItem._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);

                    this._searchItem._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                    this._searchItem._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                }
                _searchWhere = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'" + this._screenCode + "\'";
            }

            //if (this._searchItem._name.Length == 0)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._searchItem, false, true, _searchWhere);

            }*/
            this._screenTop._saveLastControl();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchName = __getControl._name.ToLower();
            string label_name = __getControl._labelName;
            string _search_text_new = _search_screen_neme(this._searchName);

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
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible == true)
                {
                    this._search_data_full_pointer.Visible = false;
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                //
            }
            if (!this._search_data_full_pointer._name.Equals(_search_text_new.ToLower()))
            {
                if (this._search_data_full_pointer._name.Length == 0)
                {
                    this._search_data_full_pointer._showMode = 0;
                    this._search_data_full_pointer._name = _search_text_new;
                    this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                    this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    //
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                }

                if (_searchName.Equals(_g.d.as_trans._doc_no))
                {
                    this._search_data_full_pointer._dataList._extraWhere = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'" + this._screenCode + "\'";

                }
            }

            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, this._search_data_full_pointer._dataList._extraWhere);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false);
            }

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

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
        }

        void _searchAll(string name, int row)
        {
            string _search_text_new = name;
            string __result = "";
            if (name.Length > 0)
            {
                __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);

                if (_searchName.Equals(_g.d.gl_journal._account_year))
                {
                    //__result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, _g.d.erp_account_period._table + "." + _g.d.erp_account_period._period_year);
                }

                if (__result.Length != 0)
                {
                    this._search_data_full_pointer.Visible = false;
                    //this._setDataStr(_searchName, __result, "", true);
                    this._screenTop._setDataStr(_searchName, __result);
                    this._search(true);
                }
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where code=\'" + this._screenTop._getDataStr(_g.d.gl_trans._close_to_account).ToUpper() + "\'"));

                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where code=\'" + this._screenTop._getDataStr(_g.d.gl_trans._book_code).ToUpper() + "\'"));

                /*__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset_type._name_1 + " from " + _g.d.as_asset_type._table + " where code=\'" + this._getDataStr(_g.d.as_trans._from_as_type).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset_type._name_1 + " from " + _g.d.as_asset_type._table + " where code=\'" + this._getDataStr(_g.d.as_trans._to_as_type).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where code=\'" + this._getDataStr(_g.d.as_trans._from_as_department).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where code=\'" + this._getDataStr(_g.d.as_trans._to_as_department).ToUpper() + "\'"));
                */
                __myquery.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

                _searchAndWarning(_g.d.gl_trans._close_to_account, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.gl_trans._book_code, (DataSet)_getData[1], warning);
                /*_searchAndWarning(_g.d.as_trans._from_as_type, (DataSet)_getData[2], warning);
                _searchAndWarning(_g.d.as_trans._to_as_type, (DataSet)_getData[3], warning);
                _searchAndWarning(_g.d.as_trans._from_as_department, (DataSet)_getData[4], warning);
                _searchAndWarning(_g.d.as_trans._to_as_department, (DataSet)_getData[5], warning);*/

            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._screenTop._getDataStr(fieldName);
                this._screenTop._setDataStr(fieldName, __getDataStr, __getData, true); // jead เพิ่มให้
            }
            else
            {
                if (this._searchTextBox != null)
                {
                    if (this._searchName.CompareTo(fieldName) == 0 && this._screenTop._getDataStr(fieldName) != "")
                    {
                        if (dataResult.Tables[0].Rows.Count == 0 && warning)
                        {
                            MyLib._myTextBox __getTextBox = (MyLib._myTextBox)(MyLib._myTextBox)this._searchTextBox.Parent;
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getTextBox._labelName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            __getTextBox._textFirst = "";
                            __getTextBox._textSecond = "";
                            __getTextBox._textLast = "";
                            this._screenTop._setDataStr(fieldName, "", "", true);
                            __getTextBox.Focus();
                            __getTextBox.textBox.Focus();
                            __result = true;
                        }
                    }
                }
            }
            return __result;
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
            SendKeys.Send("{ENTER}");
        }

        private _g.g._transControlTypeEnum _transControlTypeTemp;
        public _g.g._transControlTypeEnum _transControlType
        {
            get
            {
                return _transControlTypeTemp;
            }
            set
            {
                _transControlTypeTemp = value;
                this._build();
            }
        }

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            // start process

            // check ข้อมูลก่อนประมวลผล
           
        }
    }
}
