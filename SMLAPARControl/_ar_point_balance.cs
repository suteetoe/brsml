using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAPARControl
{
    public partial class _ar_point_balance : UserControl
    {
        MyLib._searchDataFull _searchItem = null;
        TextBox _searchTextBox;

        string _doc_format_code = "POB";
        public _ar_point_balance()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat(_g.g._search_screen_ar_point_balance, MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.ic_trans._doc_no, 1);
            _myManageData1._dataList._extraWhereEvent += new MyLib.ExtraWhereEventHandler(_dataList__extraWhereEvent);
            _myManageData1._manageButton = this.toolStrip1;

            //_myManageData1._manageBackgroundPanel = this._myPanel;
            //_myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            //_myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            //_myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            this._ar_point_balance_screen1._saveKeyDown += new MyLib.SaveKeyDownHandler(_ar_point_balance_screen1__saveKeyDown);        

            //this._posMachineScreen1._checkKeyDown += new MyLib.CheckKeyDownHandler(_posMachineScreen1__checkKeyDown);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 500;

            this._ar_point_balance_screen1._textBoxSearch += new MyLib.TextBoxSearchHandler(_ar_point_balance_screen__textBoxSearch);
            this._ar_point_balance_screen1._textBoxChanged += new MyLib.TextBoxChangedHandler(_ar_point_balance_screen__textBoxChanged);

            this.Resize += new EventHandler(_ar_point_balance_Resize);

        }

        void _myManageData1__clearData()
        {
            this._ar_point_balance_screen1._clear();
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
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

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString);
                _ar_point_balance_screen1._loadData(__getData.Tables[0]);

                if (forEdit)
                {
                    _ar_point_balance_screen1._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        void _ar_point_balance_screen1__saveKeyDown(object sender)
        {
            _save_data();
        }

        string _dataList__extraWhereEvent()
        {
            string __result = _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString();

            return __result;
        }

        void _ar_point_balance_Resize(object sender, EventArgs e)
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
            string __emptyField = this._ar_point_balance_screen1._checkEmtryField();

            if (__emptyField.Length > 0)
            {
                MessageBox.Show("กรุณาป้อนข้อมูล \n" + __emptyField.ToString(), "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string _docNo = this._ar_point_balance_screen1._getDataStr(_g.d.ic_trans._doc_no);

            StringBuilder _query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            _query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_trans where doc_no = \'" + _docNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString() + " "));

            ArrayList __fieldList = this._ar_point_balance_screen1._createQueryForDatabase();
            string _queryPoint = "insert into " + _g.d.ic_trans._table + "(" + __fieldList[0].ToString() + "," + _g.d.ic_trans._trans_flag + "," + _g.d.ic_trans._last_status + ") values(" + __fieldList[1].ToString() + ", " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString()  + ",0)";
            _query.Append(MyLib._myUtil._convertTextToXmlForQuery(_queryPoint));
            _query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, _query.ToString());

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
                _ar_point_balance_screen1._clear();
                _ar_point_balance_screen1._focusFirst();
                this._myManageData1._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {

        }

        string _docFormatCode = "";

        void _ar_point_balance_screen__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (name.Equals(_g.d.ic_trans._cust_code))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            else if (name.Equals(_g.d.ic_trans._doc_no))
            {
                string __docNo = this._ar_point_balance_screen1._getDataStr(_g.d.ic_trans._doc_no);

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._doc_running +" from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    this._docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __startRunningNumber = __getFormat.Rows[0][_g.d.erp_doc_format._doc_running].ToString();

                    string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docNo, this._ar_point_balance_screen1._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา, _g.g._transControlTypeEnum.ว่าง, _g.d.ic_trans._table, __startRunningNumber);
                    this._ar_point_balance_screen1._setDataStr(_g.d.ic_trans._doc_no, __newDoc, "", true);
                    this._ar_point_balance_screen1._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                }

                if (this._docFormatCode.Trim().Length == 0)
                {
                    string __firstString = MyLib._myGlobal._firstString(__docNo);
                    if (__firstString.Length > 0)
                    {
                        this._docFormatCode = __firstString;
                        this._ar_point_balance_screen1._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
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

        public void _search(Boolean warning)
        {
            string __searchValue = _ar_point_balance_screen1._getDataStr(_searchName);

            string __query = "";
            string __searchName = "";

            if (_searchName.Equals(_g.d.ic_trans._cust_code))
            {
                __searchName = _searchName;
                __query = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + __searchValue.ToUpper() + "\'";
            }

            if (!__query.Equals("") && !__searchName.Equals(""))
            {
                try
                {
                    _searchAndWarning(__searchName, __query, warning);
                }
                catch
                {
                }
            }

        }

        bool _searchAndWarning(string fieldName, string query, Boolean warning)
        {
            bool __result = false;
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = _ar_point_balance_screen1._getDataStr(fieldName);
                _ar_point_balance_screen1._setDataStr(fieldName, getDataStr, getData, true);
            }

            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && _ar_point_balance_screen1._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + _ar_point_balance_screen1._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

        string _searchName = "";
        void _ar_point_balance_screen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            _searchName = __getControl._name;
            string label_name = __getControl._labelName;
            string _searchWhere = "";

            if (_searchName.Equals(_g.d.ic_trans._cust_code))
            {
                // start search pos id dialog
                this._searchItem = new MyLib._searchDataFull();
                this._searchItem.Text = MyLib._myGlobal._resource("ค้นหาลูกค้า");
                this._searchItem._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, false);
            }
            else if (_searchName.Equals(_g.d.ic_trans._doc_no))
            {
                this._searchItem = new MyLib._searchDataFull();
                _searchTextBox = __getControl.textBox;
                this._searchItem._dataList._loadViewFormat(_g.g._screen_erp_doc_format, MyLib._myGlobal._userSearchScreenGroup, false);
                _searchWhere = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'" + this._doc_format_code.ToString() + "\'";
            }

            if (this._searchItem._name.Length == 0)
            {
                this._searchItem._name = _searchName;
                // start search and bind event
                this._searchItem._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);

                this._searchItem._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                this._searchItem._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);

                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._searchItem, false, true, _searchWhere);

            }
        }

        void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent("", row);
        }

        void _searchByParent(string name, int row)
        {
            int __columnNumber = 0;

            if (_searchName.Equals(_g.d.ic_trans._cust_code))
            {
                __columnNumber = this._searchItem._dataList._gridData._findColumnByName(_g.d.ar_customer._table + "." + _g.d.ar_customer._code);
            }

            string __result = (string)this._searchItem._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._searchItem.Visible = false;
                this._ar_point_balance_screen1._setDataStr(this._searchName, __result, "", false);
                this._search(true);
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;

            this._searchByParent(__getParent2._name, e._row);
            SendKeys.Send("{ENTER}");
        }

    }

    public class _ar_point_balance_screen : MyLib._myScreen
    {
        public _ar_point_balance_screen()
        {
            this._table_name = _g.d.ic_trans._table;
            this._maxColumn = 2;
            int __row = 0;

            this._addDateBox(__row++, 0, 1, 1, _g.d.ic_trans._doc_date, 1, true);
            this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
            this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.ic_trans._sum_point, 1, 2, true);
            //this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);

            this._addTextBox(__row++, 0, 3, 0, _g.d.ic_trans._remark, 2, 0);


        }

    }
}
