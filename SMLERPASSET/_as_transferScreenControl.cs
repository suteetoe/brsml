using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPASSET
{
    public partial class _as_transferScreenControl : UserControl
    {
        MyLib._searchDataFull _searchItem;
        TextBox _searchTextBox;
        string _searchName = "";

        public _as_transferScreenControl()
        {
            InitializeComponent();


            int __row = 0;
            this._screenTop.AutoSize = true;
            this._screenTop._maxColumn = 2;
            this._screenTop._table_name = _g.d.as_trans._table;
            this._screenTop._addDateBox(__row, 0, 1, 0, _g.d.as_trans._doc_date, 1, true);
            this._screenTop._addTextBox(__row++, 1, 1, 0, _g.d.as_trans._doc_time, 1, 10, 1, true, false, true);

            this._screenTop._addTextBox(__row, 0, 1, 0, _g.d.as_trans._doc_no, 1, 10, 1, true, false, false);
            this._screenTop._addTextBox(__row++, 1, 1, 0, _g.d.as_trans._doc_format_code, 1, 10, 0, true, false, true);
            //this._screenTop._addTextBox(__row++, 0, 1, 0, "สมุดรายวัน", 1, 10, 1, true, false, false);
            this._screenTop._addTextBox(__row++, 0, 2, _g.d.as_trans._remark, 2, 255);

            __row++;
            // Condition
            //this._screenTop._addLabel(4, 0, "", "เงื่อนไข");
            // Grid
            this._gridAssetDetail._width_by_persent = true;
            this._gridAssetDetail._table_name = _g.d.as_trans_detail._table;
            this._gridAssetDetail._total_show = true;
            this._gridAssetDetail._addColumn(_g.d.as_trans_detail._item_code, 1, 10, 20, true, false, true, true);
            this._gridAssetDetail._addColumn(_g.d.as_trans_detail._item_name, 1, 20, 20, false, false);
            this._gridAssetDetail._addColumn(_g.d.as_trans_detail._remark, 1, 40, 40, true, false);
            this._gridAssetDetail._addColumn(_g.d.as_trans_detail._sum_amount, 3, 0, 30, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            //this._gridAssetDetail._addColumn(_g.d.as_trans_detail._item_code, 3, 0, 20, true, false, false, false, MyLib._myGlobal._getFormatNumber("m02"));
            // ซ่อน
            this._gridAssetDetail._addColumn(_g.d.as_asset._depreciation_account_code, 1, 20, 20, false, true, false);
            this._gridAssetDetail._addColumn(_g.d.as_asset._depreciation_sum_account_code, 1, 20, 20, false, true, false);


            this._gridAssetDetail._calcPersentWidthToScatter();

            this._screenTop._textBoxSearch += _screenTop__textBoxSearch;
            this._screenTop._textBoxChanged += _screenTop__textBoxChanged;

            this._screenTop._enabedControl(_g.d.as_trans._doc_format_code, false);
        }

        public void _clear()
        {
            this._screenTop._clear();
            this._gridAssetDetail._clear();
            this._asConditionScreen1._clear();
        }

        private void _screenTop__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.as_trans._doc_no))
            {
                // new running
                string _docFormatCode = "";
                string __docNo = this._screenTop._getDataStr(_g.d.as_trans._doc_no);

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._doc_running + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    _docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __startRunningNumber = __getFormat.Rows[0][_g.d.erp_doc_format._doc_running].ToString();

                    //string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docNo, this._screenTop._getDataStr(_g.d.as_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.สินทรัพย์_โอนค่าเสื่อม, _g.g._transControlTypeEnum.ว่าง, _g.d.as_trans._table, __startRunningNumber);
                    string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ว่าง, _docFormatCode, this._screenTop._getDataStr(_g.d.as_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง, _g.d.as_trans._table, __startRunningNumber, _g.d.as_trans._doc_no, "");
                    this._screenTop._setDataStr(_g.d.as_trans._doc_no, __newDoc, "", true);
                    this._screenTop._setDataStr(_g.d.as_trans._doc_format_code, _docFormatCode, "", true);
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
        }

        private void _screenTop__textBoxSearch(object sender)
        {
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
            }

            //if (this._searchItem._name.Length == 0)
            _searchWhere = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'AST\'";
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._searchItem, false, true, _searchWhere);

            }

        }

        void _searchByParent(string name, int row)
        {
            int __columnNumber = 0;
            string __result = (string)this._searchItem._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._searchItem.Visible = false;
                this._screenTop._setDataStr(this._searchName, __result);
                SendKeys.Send("{ENTER}");
            }
        }


        void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent("", row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;

            this._searchByParent(__getParent2._name, e._row);
            //SendKeys.Send("{ENTER}");
        }
    }

    public class _asConditionScreen : MyLib._myScreen
    {
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        string _searchName = "";
        TextBox _searchTextBox;
        string _old_filed_name = "";

        public _asConditionScreen()
        {
            int __row = 0;
            this._table_name = _g.d.as_trans._table;
            this._addTextBox(__row, 0, 1, 0, _g.d.as_trans._from_as_code, 1, 10, 1, true, false, false);
            this._addTextBox(__row++, 1, 1, 0, _g.d.as_trans._to_as_code, 1, 10, 1, true, false, false);
            this._addTextBox(__row, 0, 1, 0, _g.d.as_trans._from_as_type, 1, 10, 1, true, false, false);
            this._addTextBox(__row++, 1, 1, 0, _g.d.as_trans._to_as_type, 1, 10, 1, true, false, false);
            this._addTextBox(__row, 0, 1, 0, _g.d.as_trans._from_as_department, 1, 10, 1, true, false, false);
            this._addTextBox(__row++, 1, 1, 0, _g.d.as_trans._to_as_department, 1, 10, 1, true, false, false);
            this._addNumberBox(__row, 0, 0, 0, _g.d.as_trans._from_as_period, 1, 0, true);
            this._addNumberBox(__row++, 1, 0, 0, _g.d.as_trans._to_as_period, 1, 0, true);

            this._addDateBox(__row, 0, 1, 0, _g.d.as_trans._date_begin, 1, true, false);
            this._addDateBox(__row++, 1, 1, 0, _g.d.as_trans._date_end, 1, true, false);

            this._addNumberBox(__row++, 0, 0, 0, _g.d.as_trans._by_year, 1, 0, true);
            this._addCheckBox(__row++, 0, _g.d.as_trans._department_split, false, true);

            this._textBoxSearch += _screenTop__textBoxSearch;
            this._textBoxChanged += _screenTop__textBoxChanged;

        }

        private void _screenTop__textBoxChanged(object sender, string name)
        {
            this._search(true);
        }

        private void _screenTop__textBoxSearch(object sender)
        {
            this._saveLastControl();
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
            }

            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, this._search_data_full_pointer._dataList._extraWhere);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false);
            }
            // jead : ทำให้ข้อมูลเรียกใหม่ทุกครั้ง ไม่ต้องมี เพราะ _startSearchBox มันดึงให้แล้ว this._search_data_full_pointer._dataList._refreshData(); เอาออกสองรอบแล้วเด้อ
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

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
            //SendKeys.Send("{ENTER}");
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

                if (__result.Length != 0)
                {
                    this._search_data_full_pointer.Visible = false;
                    //this._setDataStr(_searchName, __result, "", true);
                    this._setDataStr(_searchName, __result);
                    this._search(true);
                }
            }
        }

        string _search_screen_neme(string _name)
        {
            if (_name.Equals(_g.d.as_trans._from_as_code) || _name.Equals(_g.d.as_trans._to_as_code)) return _g.g._search_screen_as;
            if (_name.Equals(_g.d.as_trans._from_as_type) || _name.Equals(_g.d.as_trans._to_as_type)) return _g.g._search_master_as_asset_type;
            if (_name.Equals(_g.d.as_trans._from_as_department) || _name.Equals(_g.d.as_trans._to_as_department)) return _g.g._search_screen_erp_department_list;
            return "";
        }

        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset._name_1 + " from " + _g.d.as_asset._table + " where code=\'" + this._getDataStr(_g.d.as_trans._from_as_code).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset._name_1 + " from " + _g.d.as_asset._table + " where code=\'" + this._getDataStr(_g.d.as_trans._to_as_code).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset_type._name_1 + " from " + _g.d.as_asset_type._table + " where code=\'" + this._getDataStr(_g.d.as_trans._from_as_type).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset_type._name_1 + " from " + _g.d.as_asset_type._table + " where code=\'" + this._getDataStr(_g.d.as_trans._to_as_type).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where code=\'" + this._getDataStr(_g.d.as_trans._from_as_department).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where code=\'" + this._getDataStr(_g.d.as_trans._to_as_department).ToUpper() + "\'"));

                __myquery.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

                _searchAndWarning(_g.d.as_trans._from_as_code, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.as_trans._to_as_code, (DataSet)_getData[1], warning);
                _searchAndWarning(_g.d.as_trans._from_as_type, (DataSet)_getData[2], warning);
                _searchAndWarning(_g.d.as_trans._to_as_type, (DataSet)_getData[3], warning);
                _searchAndWarning(_g.d.as_trans._from_as_department, (DataSet)_getData[4], warning);
                _searchAndWarning(_g.d.as_trans._to_as_department, (DataSet)_getData[5], warning);

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
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true); // jead เพิ่มให้
            }
            else
            {
                if (this._searchTextBox != null)
                {
                    if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                    {
                        if (dataResult.Tables[0].Rows.Count == 0 && warning)
                        {
                            MyLib._myTextBox __getTextBox = (MyLib._myTextBox)(MyLib._myTextBox)this._searchTextBox.Parent;
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getTextBox._labelName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            __getTextBox._textFirst = "";
                            __getTextBox._textSecond = "";
                            __getTextBox._textLast = "";
                            this._setDataStr(fieldName, "", "", true);
                            __getTextBox.Focus();
                            __getTextBox.textBox.Focus();
                            __result = true;
                        }
                    }
                }
            }
            return __result;
        }

    }
}
