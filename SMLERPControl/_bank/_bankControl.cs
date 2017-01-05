using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl._bank
{
    public partial class _bankControl : UserControl
    {
        private _bankControlTypeEnum __bankControlTypeTemp;
        private _TransTypeEnum __TransTypeTemp;

        public _TransTypeEnum TransType
        {
            set
            {
                this.__TransTypeTemp = value;
                this.Invalidate();
            }
            get
            {
                return this.__TransTypeTemp;
            }
        }
        public _bankControlTypeEnum BankControlType
        {
            set
            {
                this.__bankControlTypeTemp = value;
                this._bankScreenTop.BankControlType = value;
                this._bankDetailGrid.BankControlType = value;
                this._bankScreenBottom.BankControlType = value;
                this._EvenScreenSearch();
                this.Invalidate();
            }
            get
            {
                return this.__bankControlTypeTemp;
            }
        }

        private string _QureyExtra;
        private string _QureyDeleteExtra;
        bool _LoadFromScreen = false;
        string _chkSearchName = "";
        string _oldDocNo = "";
        DateTime _oldDate = new DateTime(1000, 1, 1);
        int _getColumnDocDate = 0;
        int _getColumnDocNo = 0;
        //int _getColumnBookCode = 0;
        int _getColumnchqNumber = 0;
        MyLib._searchDataFull searchPassBook = new MyLib._searchDataFull();//ค้นหาเลขสมุดธนาคาร
        MyLib._searchDataFull searchChqRef = new MyLib._searchDataFull();//อ้างอิงเช็คเลขที่
        MyLib._searchDataFull searchOwnerName = new MyLib._searchDataFull();//ชื่อเจ้าของเช็ค
        MyLib._searchDataFull searchBank = new MyLib._searchDataFull();//ธนาคาร
        MyLib._searchDataFull searchPersonCode = new MyLib._searchDataFull();//ผู้รับ/จ่ายเช็ค
        MyLib._searchDataFull searchApproveCode = new MyLib._searchDataFull();//ผู้อนุมัติเช็ค
        MyLib._searchDataFull _searchDocGroup = new MyLib._searchDataFull();//ค้นหากลุ่มเอกสาร      
        MyLib._searchDataFull _searchBookCode = new MyLib._searchDataFull();//ค้นหาสมุดรายวัน
        MyLib._searchDataFull _searchDocRef = new MyLib._searchDataFull();//ค้นหาเอกสารอ้างอิง
        MyLib._searchDataFull _searchCustCode = new MyLib._searchDataFull();//ค้นหาลูกหนี้เจ้าหนี้
        string _searchName = "";
        TextBox _searchTextBox;
        int _transFlagChqRef = 0;
        public _bankControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //_myManageData        
            _myManageBank._dataList._lockRecord = true;
            //_myManageBank._displayMode = 0;
            //_myManageBank._selectDisplayMode(this._myManageBank._displayMode);
            _myManageBank._manageButton = this._myToolbar;
            //_myManageBank._manageBackgroundPanel = this._myPanel1;
            _myManageBank._autoSize = true;
            _myManageBank._autoSizeHeight = 400;
            //_myManageBank._dataList._refreshData();
            _myManageBank.Invalidate();
            //ค้นหา screen Top                         
            this._bankScreenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_bankScreenTop__textBoxChanged);
            this._bankScreenTop._checkKeyDown += new MyLib.CheckKeyDownHandler(_bankScreenTop__checkKeyDown);
            this._bankScreenTop._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_bankScreenTop__checkKeyDownReturn);
            this._bankScreenTop._textBoxSearch += new MyLib.TextBoxSearchHandler(_bankScreenTop__textBoxSearch);
            //screen bottom
            this._bankScreenBottom._checkKeyDown += new MyLib.CheckKeyDownHandler(_bankScreenBottom__checkKeyDown);
            this._bankScreenBottom._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_bankScreenBottom__checkKeyDownReturn);
            //ManageData
            this._myManageBank._dataList._referFieldAdd(_g.d.cb_trans._doc_no, 1);
            this._myManageBank._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageBank__loadDataToScreen);
            this._myManageBank._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageBank._newDataClick += new MyLib.NewDataEvent(_myManageBank__newDataClick);
            this._myManageBank._discardData += new MyLib.DiscardDataEvent(_myManageBank__discardData);
            this._myManageBank._clearData += new MyLib.ClearDataEvent(_myManageBank__clearData);
            //Grid
            this._bankDetailGrid._clear();
            this._bankDetailGrid._keyDown += new MyLib.KeyDownEventHandler(_bankDetailGrid__keyDown);
            this.Load += new EventHandler(_bankControl_Load);
            this._bankDetailGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_bankDetailGrid__alterCellUpdate);
            this._bankDetailGrid._queryForUpdateWhere += new MyLib.QueryForUpdateWhereEventHandler(_bankDetailGrid__queryForUpdateWhere);
            this._bankDetailGrid._queryForUpdateCheck += new MyLib.QueryForUpdateCheckEventHandler(_bankDetailGrid__queryForUpdateCheck);
            this._bankDetailGrid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_bankDetailGrid__queryForInsertCheck);
            this._bankDetailGrid._queryForRowRemoveCheck += new MyLib.QueryForRowRemoveCheckEventHandler(_bankDetailGrid__queryForRowRemoveCheck);
            this._bankDetailGrid._queryForInsertPerRow += new MyLib.QueryForInsertPerRowEventHandler(_bankDetailGrid__queryForInsertPerRow);
            this._bankDetailGrid._afterAddRow += new MyLib.AfterAddRowEventHandler(_bankDetailGrid__afterAddRow);
            this._bankDetailGrid._mouseClickClip += new MyLib.ClipMouseClickHandler(_bankDetailGrid__mouseClickClip);

            //ค้นหาในกริด
            this._bankDetailGrid._clickSearchButton += new MyLib.SearchEventHandler(_bankDetailGrid__clickSearchButton);
            this._bankDetailGrid._moveNextColumn += new MyLib.MoveNextColumnEventHandler(_bankDetailGrid__moveNextColumn);
            this._bankDetailGrid._focusCell += new MyLib.FocusCellEventHandler(_bankDetailGrid__focusCell);


            //กลุ่มเอกสาร
            _searchDocGroup._name = _g.g._search_screen_erp_doc_group;
            _searchDocGroup._dataList._loadViewFormat(_searchDocGroup._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchDocGroup._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchDocGroup._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchDocGroup._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_searchDocGroup__searchEnterKeyPress);
            _searchDocGroup._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDocGroup__searchEnterKeyPress);
            //สมุดรายวัน
            _searchBookCode._name = _g.g._search_screen_gl_journal_book;
            _searchBookCode._dataList._loadViewFormat(_searchBookCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchBookCode._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchBookCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchBookCode._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_searchBookCode__searchEnterKeyPress);
            _searchBookCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchBookCode__searchEnterKeyPress);
            //เอกสารอ้างอิง
            _searchDocRef._name = _g.g._screen_ap_ar_trans;
            _searchDocRef._dataList._loadViewFormat(_searchDocRef._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchDocRef._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchDocRef._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchDocRef._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_searchDocRef__searchEnterKeyPress);
            _searchDocRef._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDocRef__searchEnterKeyPress);
            //ลูกหนี้เจ้าหนี้
            _searchCustCode._name = _g.g._search_screen_ar;
            _searchCustCode._dataList._loadViewFormat(_searchCustCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchCustCode._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchCustCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchCustCode._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_searchCustCode__searchEnterKeyPress);
            _searchCustCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchCustCode__searchEnterKeyPress);
        }


        /// <summary>
        /// ฟังชั่น สร้าง หมุด ใน Grid
        /// </summary>
        /// <param name="number"></param>

        _cbDetailExtra __detailExtra;

        void _bankDetailGrid__mouseClickClip(object sender, MyLib.GridCellEventArgs e)
        {
            if (_LoadFromScreen)
            {
                try
                {
                    string __getChqNumberForQurey = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString();
                    MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                    StringBuilder __myquery = new StringBuilder();
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(" select * from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + " = '" + __getChqNumberForQurey + "'"));
                    __myquery.Append("</node>");
                    ArrayList _getData = myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                    this.__detailExtra._cbDetailExtraTopScreen._loadData(((DataSet)_getData[0]).Tables[0]);
                    this.__detailExtra._cbDetailExtraTopScreen.Invalidate();
                }
                catch
                {
                }
            }
            else
            {
                if (e._columnName.Equals(_g.d.cb_trans_detail._description))
                {
                    MyLib._myGrid __grid = (MyLib._myGrid)sender;
                    string __chqNumber = __grid._cellGet(e._row, _g.d.cb_trans_detail._trans_number).ToString();
                    if (__chqNumber.Length > 0)
                    {
                        __detailExtra = new _cbDetailExtra(__chqNumber);
                        _cbDetailExtraObject __getObject = (_cbDetailExtraObject)__grid._cellGet(e._row, _g.d.cb_trans_detail._description);
                        __detailExtra._cbDetailExtraTopScreen._clear();

                        __detailExtra._buttonConfirm.Click += new EventHandler(_buttonConfirm_Click);

                        if (__getObject._chq_number.Count != 0)
                        {
                            __detailExtra._cbDetailExtraTopScreen._setDataDate(_g.d.cb_chq_list._chq_due_date, MyLib._myGlobal._convertDate(__getObject._chq_due_date[0].ToString()));
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._currency_code, __getObject._currency_code[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._chq_description, __getObject._chq_description[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._ap_ar_code, __getObject._ap_ar_code[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._book_code, __getObject._book_code[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._pass_book_code, __getObject._pass_book_code[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._bank_code, __getObject._bank_code[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._bank_branch, __getObject._bank_branch[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataDate(_g.d.cb_chq_list._chq_get_date, MyLib._myGlobal._convertDate(__getObject._chq_get_date[0].ToString()));
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._person_code, __getObject._person_code[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._side_code, __getObject._side_code[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._department_code, __getObject._department_code[0].ToString());
                            __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._remark, __getObject._remark[0].ToString());

                        }
                        __detailExtra._cbDetailExtraTopScreen._setDataStr(_g.d.cb_chq_list._chq_number, __chqNumber);
                        decimal __getAmount = (decimal)this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._amount);
                        if (__getAmount == 0)
                        {
                            __getAmount = 0.0M;
                        }
                        __detailExtra._cbDetailExtraTopScreen._setDataNumber(_g.d.gl_journal_detail._amount, __getAmount);
                        __detailExtra._cbDetailExtraTopScreen.Invalidate();
                        __detailExtra.ShowDialog();
                    }
                }
            }
        }

        void _buttonConfirm_Click(object sender, EventArgs e)
        {
            _cbDetailExtraObject __getObject = (_cbDetailExtraObject)this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._description);
            __getObject._chq_number.Clear();
            __getObject._chq_due_date.Clear();
            __getObject._amount.Clear();
            __getObject._currency_code.Clear();
            __getObject._chq_description.Clear();
            __getObject._ap_ar_code.Clear();
            __getObject._book_code.Clear();
            __getObject._pass_book_code.Clear();
            __getObject._bank_code.Clear();
            __getObject._bank_branch.Clear();
            __getObject._chq_get_date.Clear();
            __getObject._person_code.Clear();
            __getObject._side_code.Clear();
            __getObject._department_code.Clear();
            __getObject._remark.Clear();


            __detailExtra._start();

            __getObject._chq_number.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._chq_number));
            __getObject._chq_due_date.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._chq_due_date));
            __getObject._amount.Add(__detailExtra._cbDetailExtraTopScreen._getDataNumber(_g.d.cb_chq_list._amount));
            __getObject._currency_code.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._currency_code));
            __getObject._chq_description.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._chq_description));
            __getObject._ap_ar_code.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._ap_ar_code));
            __getObject._book_code.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._book_code));
            __getObject._pass_book_code.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._pass_book_code));
            __getObject._bank_code.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._bank_code));
            __getObject._bank_branch.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._bank_branch));
            __getObject._chq_get_date.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._chq_get_date));
            __getObject._person_code.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._person_code));
            __getObject._side_code.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._side_code));
            __getObject._department_code.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._department_code));
            __getObject._remark.Add(__detailExtra._cbDetailExtraTopScreen._getDataStr(_g.d.cb_chq_list._remark));

            __detailExtra.Close();
        }

        void _bankDetailGrid__afterAddRow(object sender, int row)
        {
            // เพิ่ม Object เพื่อรองรับการทำงานต่อไป
            MyLib._myGrid __grid = (MyLib._myGrid)sender;
            __grid._cellUpdate(row, _g.d.cb_trans_detail._description, new _cbDetailExtraObject(), false);
        }

        /// <summary>
        /// ฟังชั่น สร้าง Even ของ Grid และ Screen 
        /// </summary>
        /// <param name="number"></param> 
        void _EvenScreenSearch()
        {
            //popup screen top
            //วันที่
            MyLib._myTextBox __getDocDateControl = (MyLib._myTextBox)this._bankScreenTop._getControl(_g.d.cb_trans._doc_date);
            //กลุ่มเอกสาร
            MyLib._myTextBox __getDocGroupControl = (MyLib._myTextBox)this._bankScreenTop._getControl(_g.d.cb_trans._doc_group);
            //สมุดรายวัน
            MyLib._myTextBox __getBookCodeControl = (MyLib._myTextBox)this._bankScreenTop._getControl(_g.d.cb_trans._book_code);
            //เอกสารอ้างอิง
            MyLib._myTextBox __getDocRefControl = (MyLib._myTextBox)this._bankScreenTop._getControl(_g.d.cb_trans._doc_ref);
            //ลูกหนี้เจ้าหนี้
            //MyLib._myTextBox __getCustCodeControl = (MyLib._myTextBox)this._bankScreenTop._getControl(_g.d.cb_trans._cust_code);

            //วันที่                    
            if (__getDocDateControl != null)
            {
                //__getDocDateControl.textBox.Leave -= new EventHandler(docdate_textBox_Leave);
                __getDocDateControl.textBox.Leave += new EventHandler(docdate_textBox_Leave);
            }
            //กลุ่มเอกสาร                    
            if (__getDocGroupControl != null)
            {
                //__getDocGroupControl.textBox.Enter -= new EventHandler(textBox_Enter);
                //__getDocGroupControl.textBox.Leave -= new EventHandler(textBox_Leave);                
                __getDocGroupControl.textBox.Enter += new EventHandler(textBox_Enter);
                __getDocGroupControl.textBox.Leave += new EventHandler(textBox_Leave);

            }
            //สมุดรายวัน                    
            if (__getBookCodeControl != null)
            {
                //__getBookCodeControl.textBox.Enter -= new EventHandler(textBox_Enter);
                //__getBookCodeControl.textBox.Leave -= new EventHandler(textBox_Leave);                
                __getBookCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
                __getBookCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
            }
            //เอกสารอ้างอิง                    
            if (__getDocRefControl != null)
            {
                //__getDocRefControl.textBox.Enter -= new EventHandler(textBox_Enter);
                //__getDocRefControl.textBox.Leave -= new EventHandler(textBox_Leave);                
                __getDocRefControl.textBox.Enter += new EventHandler(textBox_Enter);
                __getDocRefControl.textBox.Leave += new EventHandler(textBox_Leave);
            }
            //ลูกหนี้เจ้าหนี้                    
            //if (__getCustCodeControl != null)
            //{
            //    __getCustCodeControl.textBox.Enter -= new EventHandler(textBox_Enter);
            //    __getCustCodeControl.textBox.Leave -= new EventHandler(textBox_Leave);
            //    __getCustCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
            //    __getCustCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
            //}
        }
        /// <summary>
        /// ฟังชั่น การค้น ของ Grid และ Screen 
        /// </summary>
        /// <param name="number"></param> 
        void _bankDetailGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.CompareTo(_g.d.cb_trans_detail._pass_book_code) == 0)
            {
                searchPassBookStart();
                MyLib._myGlobal._startSearchBox(this._bankDetailGrid._inputTextBox, e._columnName, searchPassBook);
            }
            else if (e._columnName.CompareTo(_g.d.cb_trans_detail._chq_ref) == 0)
            {
                searchChqRefStart();
                MyLib._myGlobal._startSearchBox(this._bankDetailGrid._inputTextBox, e._columnName, searchChqRef, false);

            }
            //else if (e._columnName.CompareTo(_g.d.cb_trans_detail._owner_name) == 0)
            //{
            //    searchOwnerNameStart();
            //    MyLib._myGlobal._startSearchBox(this._bankDetailGrid._inputTextBox, e._columnName, searchOwnerName);
            //}
            else if (e._columnName.CompareTo(_g.d.cb_trans_detail._bank_code) == 0)
            {
                searchBankStart();
                MyLib._myGlobal._startSearchBox(this._bankDetailGrid._inputTextBox, e._columnName, searchBank);
            }
            //else if (e._columnName.CompareTo(_g.d.cb_trans_detail._person_code) == 0)
            //{
            //    searchPersonCodeStart();
            //    MyLib._myGlobal._startSearchBox(this._bankDetailGrid._inputTextBox, e._columnName, searchPersonCode);
            //}
            else if (e._columnName.CompareTo(_g.d.cb_trans_detail._approve_code) == 0)
            {
                searchApproveCodeStart();
                MyLib._myGlobal._startSearchBox(this._bankDetailGrid._inputTextBox, e._columnName, searchApproveCode);
            }
        }

        public void _bankScreenTop__textBoxSearch(object sender)
        {

            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            //เมื่อ คลิกปุ่มค้นหาจะมี popup ขึ้นมาให้เลือกรหัส   
            if (name.Equals(_g.d.cb_trans._doc_no))
            {
                this._bankScreenTop._getControl(_g.d.cb_trans._doc_date).Focus();
                this._bankScreenTop._getControl(_g.d.cb_trans._doc_no).Focus();
                string __getNewCode = _autoRunning();
                if (__getNewCode.Length > 0)
                {
                    this._bankScreenTop._setDataStr(_g.d.cb_trans._doc_no, __getNewCode);
                }
            }
            if (name.CompareTo(_g.d.cb_trans._doc_group) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocGroup, false);
            }
            if (name.CompareTo(_g.d.cb_trans._book_code) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchBookCode, false);
            }
            if (name.CompareTo(_g.d.cb_trans._doc_ref) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocRef, false);
            }
            //if (name.CompareTo(_g.d.cb_trans._cust_code) == 0)
            //{
            //    MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
            //    _searchName = name;
            //    _searchTextBox = getControl.textBox;
            //    MyLib._myGlobal._startSearchBox(getControl, label_name, _searchCustCode, false);
            //}
        }

        void _bankScreenTop__textBoxChanged(object sender, string name)
        {
            //string label_name = ((MyLib._myTextBox)sender)._labelName;
            //MyLib._myTextBox getControl = (MyLib._myTextBox)sender;            
            MyLib._myTextBox getControl = (MyLib._myTextBox)_bankScreenTop._getControl(name);
            string label_name = ((MyLib._myTextBox)getControl)._labelName;
            string _chkTaxt = this._bankScreenTop._getDataStr(_g.d.cb_trans._doc_no);
            if (!_chkSearchName.Equals(_chkTaxt))
            {
                _chkSearchName = _chkTaxt;
                if (name.Equals(_g.d.cb_trans._doc_no))
                {
                    string __getNewCode = _autoRunning();
                    if (__getNewCode.Length > 0)
                    {
                        this._bankScreenTop._setDataStr(_g.d.cb_trans._doc_no, __getNewCode);
                    }
                }
            }
            if (name.Equals(_g.d.cb_trans._doc_group))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocGroup, false);
            }
            if (name.Equals(_g.d.cb_trans._book_code))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchBookCode, false);
            }
            if (name.Equals(_g.d.cb_trans._doc_ref))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocRef, false);
            }
            //if (name.Equals(_g.d.cb_trans._cust_code))
            //{
            //    _searchTextBox = (TextBox)sender;
            //    _searchName = name;
            //    MyLib._myGlobal._startSearchBox(getControl, label_name, _searchCustCode, false);
            //}            
        }

        private void searchApproveCodeStart()
        {
            searchApproveCode = new MyLib._searchDataFull();
            searchApproveCode._name = _g.g._search_screen_erp_user;
            searchApproveCode._dataList._loadViewFormat(searchApproveCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
            searchApproveCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            searchApproveCode._dataList._refreshData();
            searchApproveCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(searchApproveCode__searchEnterKeyPress);
            searchApproveCode._dataList._loadViewData(0);
        }

        private void searchPersonCodeStart()
        {
            searchPersonCode = new MyLib._searchDataFull();
            searchPersonCode._name = _g.g._search_screen_erp_user;
            searchPersonCode._dataList._loadViewFormat(searchPersonCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
            searchPersonCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            searchPersonCode._dataList._refreshData();
            searchPersonCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(searchPersonCode__searchEnterKeyPress);
            searchPersonCode._dataList._loadViewData(0);
        }

        private void searchBankStart()
        {
            searchBank = new MyLib._searchDataFull();
            searchBank._name = _g.g._search_screen_bank;
            searchBank._dataList._loadViewFormat(searchBank._name, MyLib._myGlobal._userSearchScreenGroup, false);
            searchBank._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            searchBank._dataList._refreshData();
            searchBank._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(searchBank__searchEnterKeyPress);
            searchBank._dataList._loadViewData(0);
        }

        private void searchOwnerNameStart()
        {
            searchOwnerName = new MyLib._searchDataFull();
            searchOwnerName._name = _g.g._search_screen_ar;
            searchOwnerName._dataList._loadViewFormat(searchOwnerName._name, MyLib._myGlobal._userSearchScreenGroup, false);
            searchOwnerName._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            searchOwnerName._dataList._refreshData();
            searchOwnerName._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(searchOwnerName__searchEnterKeyPress);
            searchOwnerName._dataList._loadViewData(0);
        }

        private void searchChqRefStart()
        {


            if (_transFlagChqRef == 1 || _transFlagChqRef == 2)
            {
                searchChqRef = new MyLib._searchDataFull();
                searchChqRef._name = _g.g._search_screen_cb_chq_ref_in_list;
                searchChqRef._dataList._loadViewFormat(searchChqRef._name, MyLib._myGlobal._userSearchScreenGroup, false);
                searchChqRef._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                searchChqRef._dataList._refreshData();
                searchChqRef._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(searchChqRef__searchEnterKeyPress);
                searchChqRef._dataList._loadViewData(0);
            }
            else
            {
                searchChqRef = new MyLib._searchDataFull();
                searchChqRef._name = _g.g._search_screen_cb_chq_ref;
                searchChqRef._dataList._loadViewFormat(searchChqRef._name, MyLib._myGlobal._userSearchScreenGroup, false);
                searchChqRef._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                searchChqRef._dataList._refreshData();
                searchChqRef._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(searchChqRef__searchEnterKeyPress);
                searchChqRef._dataList._loadViewData(0);
            }



        }

        private void searchPassBookStart()
        {
            searchPassBook = new MyLib._searchDataFull();
            //searchPassBook.WindowState = FormWindowState.Maximized;
            searchPassBook._name = _g.g._search_screen_สมุดเงินฝาก;
            searchPassBook._dataList._loadViewFormat(searchPassBook._name, MyLib._myGlobal._userSearchScreenGroup, false);
            searchPassBook._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            searchPassBook._dataList._refreshData();
            searchPassBook._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(searchPassBook__searchEnterKeyPress);
            searchPassBook._dataList._loadViewData(0);

        }

        void searchApproveCode__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchGridByParent(sender, row);
        }
        void searchPersonCode__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchGridByParent(sender, row);
        }
        void searchBank__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchGridByParent(sender, row);
        }
        void searchOwnerName__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchGridByParent(sender, row);
        }
        void searchChqRef__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchGridByParent(sender, row);
        }
        void searchPassBook__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchGridByParent(sender, row);
        }
        void _searchCustCode__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchDocRef__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchBookCode__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchDocGroup__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            if (__getParent1._gridData._rowData.Count > 0)
            {
                _searchAll(__getParent2._name, row);
                SendKeys.Send("{TAB}");
            }
        }
        private void _searchGridByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{ENTER}");
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            _searchDocGroup.Visible = false;
            _searchBookCode.Visible = false;
            _searchDocRef.Visible = false;
            _searchCustCode.Visible = false;

        }

        void docdate_textBox_Leave(object sender, EventArgs e)
        {
            MyLib._myDateBox __getDateBox = (MyLib._myDateBox)((Control)sender).Parent;
            if (__getDateBox.textBox.Text.Length == 0)
            {
                this._bankScreenTop._setDataDate(_g.d.cb_trans._doc_date, MyLib._myGlobal._workingDate);
                __getDateBox._checkDate(true, true);
                this._bankScreenTop._isChange = false;
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _textBoxStartSearch(__getControl);
            __getControl.textBox.Focus();


        }

        void _textBoxStartSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.Equals(_g.d.cb_trans._doc_group))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocGroup, false);
            }
            if (name.Equals(_g.d.cb_trans._book_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchBookCode, false);
            }
            if (name.CompareTo(_g.d.cb_trans._doc_ref) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocRef, false);
            }
            //if (name.CompareTo(_g.d.cb_trans._cust_code) == 0)
            //{
            //    MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
            //    _searchName = name;
            //    _searchTextBox = getControl.textBox;
            //    MyLib._myGlobal._startSearchBox(getControl, label_name, _searchCustCode, false);
            //}
        }


        void _searchAll(string name, int row)
        {
            if (name.CompareTo(_g.g._search_screen_สมุดเงินฝาก) == 0)
            {
                string result = (string)searchPassBook._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    searchPassBook.Close();
                    this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code, result, true);
                    string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString();
                    string __query = "select " + _g.d.erp_pass_book._name_1 + "," + _g.d.erp_pass_book._bank_code + "," + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                    this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code, __query);
                }
            }
            else if (name.CompareTo(_g.g._search_screen_cb_chq_ref) == 0)
            {
                string result = (string)searchChqRef._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    searchChqRef.Close();
                    this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref, result, true);
                    string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref).ToString();
                    string __query = "select " + _g.d.cb_trans_detail._trans_number + " from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._trans_number + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\' and " + _g.d.cb_trans_detail._trans_flag + " = " + _transFlagChqRef;
                    this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref, __query);
                }
            }
            else if (name.CompareTo(_g.g._search_screen_cb_chq_ref_in_list) == 0)
            {
                string result = (string)searchChqRef._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    searchChqRef.Close();
                    this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref, result, true);
                    string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref).ToString();
                    string __query = "select " + _g.d.cb_chq_list._chq_number + " from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\' and " + _g.d.cb_chq_list._chq_type + " = " + _transFlagChqRef;
                    this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref, __query);
                }
            }
            else if (name.CompareTo(_g.g._search_screen_ar) == 0)
            {
                if (searchOwnerName.Visible)
                {
                    //string result = (string)searchOwnerName._dataList._gridData._cellGet(row, 0);
                    //if (result.Length > 0)
                    //{
                    //    searchOwnerName.Close();
                    //    this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._owner_name, result, true);
                    //    string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._owner_name).ToString();
                    //    string __query = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                    //    this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._owner_name, __query);
                    //}
                }
                else
                {
                    //string result = (string)this._searchCustCode._dataList._gridData._cellGet(row, 0);
                    //if (result.Length > 0)
                    //{
                    //    this._searchCustCode.Close();
                    //    this._bankScreenTop._setDataStr(_searchName, result);
                    //   // _search(false);
                    //    //ลูกหนี้
                    //    string __query = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._cust_code) + "\'";
                    //    this._searchAndWarning(_g.d.cb_trans._cust_code, __query);
                    //}            
                }
            }
            else if (name.CompareTo(_g.g._search_screen_bank) == 0)
            {
                string result = (string)searchBank._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    searchBank.Close();
                    this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._bank_code, result, true);
                    string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._bank_code).ToString();
                    string __query = "select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                    this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._bank_code, __query);
                }
            }
            else if (name.CompareTo(_g.g._search_screen_erp_user) == 0)
            {
                if (searchPersonCode.Visible)
                {
                    //string result = (string)searchPersonCode._dataList._gridData._cellGet(row, 0);
                    //if (result.Length > 0)
                    //{
                    //    searchPersonCode.Close();
                    //    this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._person_code, result, true);
                    //    string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._person_code).ToString();
                    //    string __query = "select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                    //    this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._person_code, __query);
                    //}
                }
                else
                {
                    string result = (string)searchApproveCode._dataList._gridData._cellGet(row, 0);
                    if (result.Length > 0)
                    {
                        searchApproveCode.Close();
                        this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._approve_code, result, true);
                        string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._approve_code).ToString();
                        string __query = "select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                        this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._approve_code, __query);
                    }
                }
            }
            else if (name.CompareTo(_g.g._search_screen_erp_doc_group) == 0)
            {
                string result = (string)_searchDocGroup._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchDocGroup.Close();
                    this._bankScreenTop._setDataStr(_searchName, result);
                    //_search(false);
                    //กลุ่มเอกสาร
                    string __query = "select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where " + _g.d.erp_doc_group._code + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._doc_group) + "\'";
                    this._searchAndWarning(_g.d.cb_trans._doc_group, __query);
                }
            }
            else if (name.CompareTo(_g.g._search_screen_gl_journal_book) == 0)
            {
                string result = (string)_searchBookCode._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {

                    _searchBookCode.Close();
                    this._bankScreenTop._setDataStr(_searchName, result);
                    //_search(false);
                    string __query = "select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + _g.d.gl_journal_book._code + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._book_code) + "\'";
                    this._searchAndWarning(_g.d.cb_trans._book_code, __query);
                }
            }
            else if (name.CompareTo(_g.g._screen_ap_ar_trans) == 0)
            {
                string result = (string)this._searchDocRef._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {

                    this._searchDocRef.Close();
                    this._bankScreenTop._setDataStr(_searchName, result);
                    //_search(false);
                    //เอกสารอ้างอิง
                    string __query = "select " + _g.d.ap_ar_trans._doc_no + " from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._doc_no + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._doc_ref) + "\'";
                    this._searchAndWarning(_g.d.cb_trans._doc_ref, __query);
                }
            }
            else if (name.CompareTo(_g.g._search_screen_ap) == 0)
            {
                string result = (string)this._searchCustCode._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {

                    //this._searchCustCode.Close();
                    //this._bankScreenTop._setDataStr(_searchName, result);
                    ////_search(false);
                    ////เจ้าหนี้
                    //string __query = "select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._cust_code) + "\'";
                    //this._searchAndWarning(_g.d.cb_trans._cust_code, __query);                    
                }
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_สมุดเงินฝาก) == 0)
            {
                searchPassBook.Close();
                this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code, e._text, true);
                string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString();
                string __query = "select " + _g.d.erp_pass_book._name_1 + "," + _g.d.erp_pass_book._bank_code + "," + _g.d.erp_pass_book._bank_branch + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code, __query);
            }
            else if (name.CompareTo(_g.g._search_screen_cb_chq_ref) == 0)
            {
                searchChqRef.Close();
                this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref, e._text, true);
                string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref).ToString();
                string __query = "select " + _g.d.cb_trans_detail._trans_number + " from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._trans_number + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\' and " + _g.d.cb_trans_detail._trans_flag + " = " + _transFlagChqRef;
                this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref, __query);

            }
            else if (name.CompareTo(_g.g._search_screen_cb_chq_ref_in_list) == 0)
            {
                searchChqRef.Close();
                this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref, e._text, true);
                string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref).ToString();
                string __query = "select " + _g.d.cb_chq_list._chq_number + " from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\' and " + _g.d.cb_chq_list._chq_type + " = " + _transFlagChqRef;
                this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._chq_ref, __query);

            }
            else if (name.CompareTo(_g.g._search_screen_ar) == 0)
            {
                if (searchOwnerName.Visible)
                {
                    //searchOwnerName.Close();
                    //this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._owner_name, e._text, true);
                    //string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._owner_name).ToString();
                    //string __query = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                    //this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._owner_name, __query);

                }
                else
                {
                    //this._searchCustCode.Close();
                    //this._bankScreenTop._setDataStr(_searchName, e._text);
                    //// _search(false);
                    ////ลูกหนี้
                    //string __query = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._cust_code) + "\'";
                    //this._searchAndWarning(_g.d.cb_trans._cust_code, __query);                    
                }
            }
            else if (name.CompareTo(_g.g._search_screen_bank) == 0)
            {

                searchBank.Close();
                this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._bank_code, e._text, true);
                string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._bank_code).ToString();
                string __query = "select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._bank_code, __query);

            }
            else if (name.CompareTo(_g.g._search_screen_erp_user) == 0)
            {
                if (searchPersonCode.Visible)
                {
                    //searchPersonCode.Close();
                    //this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._person_code, e._text, true);
                    //string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._person_code).ToString();
                    //string __query = "select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                    //this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._person_code, __query);

                }
                else
                {
                    searchApproveCode.Close();
                    this._bankDetailGrid._cellUpdate(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._approve_code, e._text, true);
                    string getCode = this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._approve_code).ToString();
                    string __query = "select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + MyLib._myUtil._convertTextToXml(getCode) + "\'";
                    this._searchAndWarning(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._approve_code, __query);

                }
            }
            else if (name.CompareTo(_g.g._search_screen_erp_doc_group) == 0)
            {
                _searchDocGroup.Close();
                this._bankScreenTop._setDataStr(_searchName, e._text);
                //_search(false);
                //กลุ่มเอกสาร
                string __query = "select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where " + _g.d.erp_doc_group._code + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._doc_group) + "\'";
                this._searchAndWarning(_g.d.cb_trans._doc_group, __query);
            }
            else if (name.CompareTo(_g.g._search_screen_gl_journal_book) == 0)
            {
                _searchBookCode.Close();
                this._bankScreenTop._setDataStr(_searchName, e._text);
                //_search(false);
                string __query = "select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + _g.d.gl_journal_book._code + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._book_code) + "\'";
                this._searchAndWarning(_g.d.cb_trans._book_code, __query);
            }
            else if (name.CompareTo(_g.g._screen_ap_ar_trans) == 0)
            {
                this._searchDocRef.Close();
                this._bankScreenTop._setDataStr(_searchName, e._text);
                //_search(false);
                //เอกสารอ้างอิง
                string __query = "select " + _g.d.ap_ar_trans._doc_no + " from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._doc_no + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._doc_ref) + "\'";
                this._searchAndWarning(_g.d.cb_trans._doc_ref, __query);
            }
            else if (name.CompareTo(_g.g._search_screen_ap) == 0)
            {
                //this._searchCustCode.Close();
                //this._bankScreenTop._setDataStr(_searchName, e._text);
                ////_search(false);
                ////เจ้าหนี้
                //string __query = "select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + this._bankScreenTop._getDataStr(_g.d.cb_trans._cust_code) + "\'";
                //this._searchAndWarning(_g.d.cb_trans._cust_code, __query);               
            }
        }



        bool _searchAndWarning(string fieldName, string query)
        {

            bool __result = false;
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, query);

            if (__dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = __dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = this._bankScreenTop._getDataStr(fieldName);
                this._bankScreenTop._setDataStr(fieldName, getDataStr, getData, true);

            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && this._bankScreenTop._getDataStr(fieldName) != "")
                {
                    if (__dataResult.Tables[0].Rows.Count == 0)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        //_searchTextBox.Focus();
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._bankScreenTop._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

        void _searchAndWarning(int row, string fieldName, string query)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    if (fieldName.CompareTo(_g.d.cb_trans_detail._pass_book_code) == 0)
                    {
                        string getData = dataResult.Tables[0].Rows[0][0].ToString();
                        string getDataStr = this._bankDetailGrid._cellGet(row, fieldName).ToString();
                        string getBankCode = dataResult.Tables[0].Rows[0][1].ToString();
                        string getBranchBank = dataResult.Tables[0].Rows[0][2].ToString();
                        //string getNameBook = dataResult.Tables[0].Rows[0][3].ToString();
                        this._bankDetailGrid._cellUpdate(row, fieldName, (getData + "/" + getDataStr), false);
                        this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._bank_code, getBankCode, false);
                        //this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._bank_branch, getBranchBank, false);
                        //this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._bank_description, getNameBook, false);
                    }
                    else
                    {
                        string getData = dataResult.Tables[0].Rows[0][0].ToString();
                        string getDataStr = this._bankDetailGrid._cellGet(row, fieldName).ToString();
                        this._bankDetailGrid._cellUpdate(row, fieldName, (getData + "/" + getDataStr), false);
                    }

                }
                else
                {
                    if (this._bankDetailGrid._cellGet(row, fieldName).ToString().Length != 0)
                    {
                        if (dataResult.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + fieldName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    if (fieldName.CompareTo(_g.d.cb_trans_detail._pass_book_code) == 0)
                    {
                        this._bankDetailGrid._cellUpdate(row, fieldName, "", false);
                        this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._pass_book_code, "", false);
                        this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._bank_code, "", false);
                        //this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._bank_branch, "", false);
                        //this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._bank_description, "", false);
                    }
                    else
                    {
                        this._bankDetailGrid._cellUpdate(row, fieldName, "", false);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Even การทำงาน ของ Grid และ Screen ทั่วไปไม่เกี่ยวกับค้นหา
        /// </summary>
        /// <param name="number"></param>     
        Boolean _bankDetailGrid__keyDown(object sender, int row, int column, Keys keyCode)
        {
            if (row == 0 && (keyCode == Keys.PageUp || keyCode == Keys.Up))
            {
                this._bankScreenTop.Focus();
                return true;
            }
            if (row == 0 && (keyCode == Keys.PageDown || keyCode == Keys.Down))
            {
                this._bankScreenBottom.Focus();
                return true;
            }
            return false;
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
                            this._bankScreenTop.Focus();
                            return true;
                        }
                }
            }
            if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }
            if (keyData == Keys.F12)
            {
                _saveData();
                return true;
            }
            if (keyData == Keys.F2)
            {
                if (this._searchDocGroup.Visible)
                {
                    //ค้นหากลุ่มเอกสาร 
                    this._searchDocGroup.Focus();
                    this._searchDocGroup._firstFocus();
                }
                if (this._searchBookCode.Visible)
                {
                    //ค้นหาสมุดรายวัน
                    this._searchBookCode.Focus();
                    this._searchBookCode._firstFocus();
                }
                if (this._searchDocRef.Visible)
                {
                    //ค้นหาเอกสารอ้างอิง
                    this._searchDocRef.Focus();
                    this._searchDocRef._firstFocus();
                }
                if (this._searchCustCode.Visible)
                {
                    //ค้นหาลูกหนี้เจ้าหนี้
                    this._searchCustCode.Focus();
                    this._searchCustCode._firstFocus();
                }
                if (this.searchPassBook.Visible)
                {
                    //ค้นหาเลขสมุดธนาคาร
                    this.searchPassBook.Focus();
                    this.searchPassBook._firstFocus();
                }
                if (this.searchChqRef.Visible)
                {
                    //อ้างอิงเช็คเลขที่
                    this.searchChqRef.Focus();
                    this.searchChqRef._firstFocus();
                }
                if (this.searchOwnerName.Visible)
                {
                    //ชื่อเจ้าของเช็ค
                    this.searchOwnerName.Focus();
                    this.searchOwnerName._firstFocus();
                }
                if (this.searchBank.Visible)
                {
                    //ธนาคาร
                    this.searchBank.Focus();
                    this.searchBank._firstFocus();
                }
                if (this.searchPersonCode.Visible)
                {
                    //ผู้รับ/จ่ายเช็ค
                    this.searchPersonCode.Focus();
                    this.searchPersonCode._firstFocus();
                }
                if (this.searchApproveCode.Visible)
                {
                    //ผู้อนุมัติเช็ค
                    this.searchApproveCode.Focus();
                    this.searchApproveCode._firstFocus();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }



        void _bankDetailGrid__focusCell(object sender, int row, int column, string columnName)
        {
            if (columnName.Equals(_g.d.cb_trans_detail._pass_book_code))
            {
                string _amount = (string)this._bankDetailGrid._cellGet(row, columnName);
                this._bankDetailGrid._cellUpdate(row, columnName, _amount, false);
            }
            if (columnName.Equals(_g.d.cb_trans_detail._amount))
            {
                decimal _amount = (decimal)this._bankDetailGrid._cellGet(row, columnName);
                this._bankDetailGrid._cellUpdate(row, columnName, _amount, false);
            }
            if (columnName.Equals(_g.d.cb_trans_detail._fee_amount))
            {
                decimal _fee_amount = (decimal)this._bankDetailGrid._cellGet(row, columnName);
                this._bankDetailGrid._cellUpdate(row, columnName, _fee_amount, false);
            }
            if (columnName.Equals(_g.d.cb_trans_detail._other_amount))
            {
                decimal _other_amount = (decimal)this._bankDetailGrid._cellGet(row, columnName);
                this._bankDetailGrid._cellUpdate(row, columnName, _other_amount, false);
            }
            if (columnName.Equals(_g.d.cb_trans_detail._tax_at_pay))
            {
                decimal _tax_at_pay = (decimal)this._bankDetailGrid._cellGet(row, columnName);
                this._bankDetailGrid._cellUpdate(row, columnName, _tax_at_pay, false);
            }
            if (columnName.Equals(_g.d.cb_trans_detail._sum_amount))
            {
                decimal _net_amount = (decimal)this._bankDetailGrid._cellGet(row, columnName);
                this._bankDetailGrid._cellUpdate(row, columnName, _net_amount, false);
            }
        }

        MyLib._myGridMoveColumnType _bankDetailGrid__moveNextColumn(MyLib._myGrid sender, int newRow, int newColumn)
        {
            MyLib._myGridMoveColumnType __result = new MyLib._myGridMoveColumnType();
            switch (BankControlType)
            {
                case _bankControlTypeEnum.ฝากเงิน:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.cash_payin_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.cash_withdraw:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.cash_withdraw_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.received:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.received_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.payment:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.payment_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.interest_received:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.interest_received_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.interest_payment:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.interest_payment_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.cash_transfer_received:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.cash_transfer_received_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.cash_transfer_payment:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.cash_transfer_payment_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._pass_book_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._pass_book_code);
                    }
                    break;
                case _bankControlTypeEnum.in_payin:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.in_payin_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.in_return:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.in_return_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.in_honor:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.in_honor_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.in_change:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.in_change_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.in_new:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.in_new_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.out_return:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.out_return_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.out_honor:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.out_honor_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.out_change:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.out_change_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.credit_money:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._credit_card_no).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._credit_card_no);
                    }
                    break;
                case _bankControlTypeEnum.credit_cancel:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._credit_card_no).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._credit_card_no);
                    }
                    break;
                case _bankControlTypeEnum.petty_receive:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._petty_cash_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._petty_cash_code);
                    }
                    break;
                case _bankControlTypeEnum.petty_receive_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._petty_cash_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._petty_cash_code);
                    }
                    break;
                case _bankControlTypeEnum.petty_request:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._petty_cash_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._petty_cash_code);
                    }
                    break;
                case _bankControlTypeEnum.petty_request_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._petty_cash_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._petty_cash_code);
                    }
                    break;
                case _bankControlTypeEnum.petty_return:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._petty_cash_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._petty_cash_code);
                    }
                    break;
                case _bankControlTypeEnum.petty_return_cancle:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._petty_cash_code).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._petty_cash_code);
                    }
                    break;
                case _bankControlTypeEnum.เช็ครับ:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;
                case _bankControlTypeEnum.out_payment:
                    if (this._bankDetailGrid._cellGet(this._bankDetailGrid._selectRow, _g.d.cb_trans_detail._trans_number).ToString().Length == 0)
                    {
                        newColumn = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._trans_number);
                    }
                    break;

            }
            __result._newColumn = newColumn;
            __result._newRow = newRow;
            return __result;
        }

        MyLib.QueryForInsertPerRowType _bankDetailGrid__queryForInsertPerRow(MyLib._myGrid sender, int row)
        {
            MyLib.QueryForInsertPerRowType result = new MyLib.QueryForInsertPerRowType();
            result._field = "line_number";
            result._data = row.ToString();
            return (result);
        }

        bool _bankDetailGrid__queryForRowRemoveCheck(MyLib._myGrid sender, int row)
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

        bool _bankDetailGrid__queryForInsertCheck(MyLib._myGrid sender, int row)
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

        bool _bankDetailGrid__queryForUpdateCheck(MyLib._myGrid sender, int row)
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

        string _bankDetailGrid__queryForUpdateWhere(MyLib._myGrid sender, int row)
        {
            int __getInt = (int)sender._cellGet(row, sender._rowNumberName);
            return (sender._rowNumberName + "=" + __getInt.ToString());
        }

        void _bankDetailGrid__alterCellUpdate(object sender, int row, int column)
        {
            //ผลรวมเอาไปลง Screen ล่าง จะบวกเพิ่มเรื่อยๆ
            int field_amount = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._amount);
            int field_fee_amount = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._fee_amount);
            int field_other_amount = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._other_amount);
            int field_tax_at_pay = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._tax_at_pay);
            this._bankDetailGrid._calcTotal(false);
            decimal get_total_amount = ((MyLib._myGrid._columnType)this._bankDetailGrid._columnList[field_amount])._total;
            decimal get_total_fee = (field_fee_amount == -1) ? 0M : ((MyLib._myGrid._columnType)this._bankDetailGrid._columnList[field_fee_amount])._total;
            decimal get_total_other_amount = (field_other_amount == -1) ? 0M : ((MyLib._myGrid._columnType)this._bankDetailGrid._columnList[field_other_amount])._total;
            decimal get_total_tax_at_pay = (field_tax_at_pay == -1) ? 0M : ((MyLib._myGrid._columnType)this._bankDetailGrid._columnList[field_tax_at_pay])._total;
            this._bankScreenBottom._setDataNumber(_g.d.cb_trans._total_amount, get_total_amount);
            this._bankScreenBottom._setDataNumber(_g.d.cb_trans._total_fee_amount, get_total_fee);
            this._bankScreenBottom._setDataNumber(_g.d.cb_trans._total_other_amount, get_total_other_amount);
            this._bankScreenBottom._setDataNumber(_g.d.cb_trans._total_tax_at_pay, get_total_tax_at_pay);
            //ไปฟั่งชั่นรวมใน Screen ล่าง
            _calcTotalGrid();
            //ผลรวมเอาไปใส่ กริด แต่ ล่ะ row
            decimal amount = MyLib._myGlobal._decimalPhase(this._bankDetailGrid._cellGet(row, field_amount).ToString());
            decimal fee_amount = (field_fee_amount == -1) ? 0M : MyLib._myGlobal._decimalPhase(this._bankDetailGrid._cellGet(row, field_fee_amount).ToString());
            decimal other_amount = (field_other_amount == -1) ? 0M : MyLib._myGlobal._decimalPhase(this._bankDetailGrid._cellGet(row, field_other_amount).ToString());
            decimal tax_at_pay = (field_tax_at_pay == -1) ? 0M : MyLib._myGlobal._decimalPhase(this._bankDetailGrid._cellGet(row, field_tax_at_pay).ToString());
            //เอามาคำนวน
            decimal sum_amount = amount - (fee_amount + other_amount + tax_at_pay);
            //จับใส่ กริด
            this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._sum_amount, sum_amount, false);

            //ค้นหาใน กริดรายละเอียด
            //string name = this._bankDetailGrid._findColumnByName._selectColumn.ToString();
            //MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            //MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            //string name = getParent2._name;            
            //if (name.Equals(_g.d.cb_trans_detail._pass_book_code))
            //{
            //    _searchAll(name, row);
            //}
            //else if (name.Equals(_g.d.cb_trans_detail._chq_ref))
            //{
            //    _searchAll(name, row);

            //}
            //else if (name.Equals(_g.d.cb_trans_detail._bank_code))
            //{
            //    _searchAll(name, row);
            //}
            //else if (name.Equals(_g.d.cb_trans_detail._approve_code))
            //{
            //    _searchAll(name, row);
            //}
        }
        bool _bankScreenTop__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (this._bankScreenTop._lastControl.GetType() == typeof(MyLib._myTextBox))
            {
                if ((keyData == Keys.Tab || keyData == Keys.Enter) && ((MyLib._myTextBox)this._bankScreenTop._lastControl)._name.Equals(_g.d.cb_trans._remark))
                {
                    _gotoItemGrid();
                    return false;
                }
            }
            return true;
        }


        Boolean _bankScreenTop__checkKeyDown(object sender, Keys keyData)
        {
            if (keyData == Keys.PageDown || keyData == Keys.Down)
            {
                _gotoItemGrid();
            }
            return true;
        }

        bool _bankScreenBottom__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (this._bankScreenBottom._lastControl.GetType() == typeof(MyLib._myTextBox))
            {
                if ((keyData == Keys.Tab || keyData == Keys.Enter) && ((MyLib._myTextBox)this._bankScreenBottom._lastControl)._name.Equals(_g.d.cb_trans._total_net_amount))
                {
                    this._bankScreenTop.Focus();
                    return false;
                }
            }
            return true;
        }

        bool _bankScreenBottom__checkKeyDown(object sender, Keys keyData)
        {
            if (keyData == Keys.PageDown || keyData == Keys.Down)
            {
                this._bankScreenTop.Focus();
            }
            return true;
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        /// <summary>
        /// เพื่อตรวจสอบว่าบันทึกรายละเอียดด้านบนครบหรือไม่
        /// </summary>
        /// <returns></returns>
        Boolean _gotoItemGrid()
        {
            this._bankDetailGrid.Focus();
            this._bankDetailGrid._removeLastControl();
            if (this._bankDetailGrid._selectRow == -1)
            {
                this._bankDetailGrid._selectRow = 0;
            }
            this._bankDetailGrid._selectColumn = 0;
            this._bankDetailGrid._inputCell(this._bankDetailGrid._selectRow, this._bankDetailGrid._selectColumn);
            return true;
        }

        /// <summary>
        /// ฟังชั่น ทั่วไป
        /// </summary>
        /// <param name="number">autoRuning=เลขที่เอกสารอัตโนมัต,CalcTotalGrid=คำนวนผลรวมใส่ Screen Bottom</param>        
        string _autoRunning()
        {
            return MyLib._myGlobal._getAutoRun(_g.d.cb_trans._table, _g.d.cb_trans._doc_no, _g.d.cb_trans._trans_flag, _bankGlobal._bankType(BankControlType).ToString(), this._bankScreenTop._getDataStr(_g.d.cb_trans._doc_no).ToUpper());
        }

        private void _calcTotalGrid()
        {
            decimal get_field_amount = this._bankScreenBottom._getDataNumber(_g.d.cb_trans._total_amount);
            decimal get_field_fee_amount = this._bankScreenBottom._getDataNumber(_g.d.cb_trans._total_fee_amount);
            decimal get_field_other_amount = this._bankScreenBottom._getDataNumber(_g.d.cb_trans._total_other_amount);
            decimal get_field_tax_at_pay = this._bankScreenBottom._getDataNumber(_g.d.cb_trans._total_tax_at_pay);
            decimal set_field_net_amount = get_field_amount - (get_field_fee_amount + get_field_other_amount + get_field_tax_at_pay);
            this._bankScreenBottom._setDataNumber(_g.d.cb_trans._total_net_amount, set_field_net_amount);
            this._bankScreenBottom._refresh();
        }

        void _get_column_number()
        {
            _getColumnDocDate = this._myManageBank._dataList._gridData._findColumnByName(_g.d.cb_trans._table + "." + _g.d.cb_trans._doc_date);
            _getColumnDocNo = this._myManageBank._dataList._gridData._findColumnByName(_g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no);
            //_getColumnBookCode = this._myManageBank._dataList._gridData._findColumnByName(_g.d.cb_trans._table + "." + _g.d.cb_trans._book_code);            
        }
        /// <summary>
        /// Even การทำงาน ของ MyManageData
        /// </summary>
        /// <param name="number"></param>  
        void _myManageBank__clearData()
        {
            this._bankScreenTop._clear();
            this._bankScreenBottom._clear();
            this._bankDetailGrid._clear();
            this._bankScreenTop._isChange = false;
            this._bankScreenBottom._isChange = false;
        }

        bool _myManageBank__discardData()
        {
            bool result = true;
            if (this._bankScreenTop._isChange || this._bankScreenBottom._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._bankScreenTop._isChange = false;
                    this._bankScreenBottom._isChange = false;
                }
            }
            return (result);
        }

        void _myManageBank__newDataClick()
        {
            Control codeControl = this._bankScreenTop._getControl(_g.d.cb_trans._doc_no);
            codeControl.Enabled = true;
            //for (int row = 0; row < this._bankDetailGrid._rowData.Count; row++)
            //{
            //    this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._amount, 0.0M, false);
            //    this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._fee_amount, 0.0M, false);
            //    this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._other_amount, 0.0M, false);
            //    this._bankDetailGrid._cellUpdate(row, _g.d.cb_trans_detail._sum_amount, 0.0M, false);
            //}
            this._bankDetailGrid._clear();
            this._bankDetailGrid.Invalidate();
            this._bankScreenTop._focusFirst();
        }
        /// <summary>
        /// Even การทำงาน Delete,Load,Save
        /// </summary>
        /// <param name="number"></param>
        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            _get_column_number();
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int loop = 0; loop < selectRowOrder.Count; loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[loop];

                DateTime __getDate = MyLib._myGlobal._convertDate(this._myManageBank._dataList._gridData._cellGet(getData.row, _getColumnDocDate).ToString());
                string __getDocNo = this._myManageBank._dataList._gridData._cellGet(getData.row, _getColumnDocNo).ToString();

                string __getchqNumber = "";
                string __qureychqNumber = "";
                switch (this.BankControlType)
                {
                    case _bankControlTypeEnum.เช็ครับ:
                        try
                        {
                            __qureychqNumber = "select " + _g.d.cb_trans_detail._trans_number + " from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + " = '" + __getDocNo + "' and " + _g.d.cb_trans_detail._trans_flag + " = " + _bankGlobal._bankType(BankControlType).ToString();
                            DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, __qureychqNumber);
                            if (dataResult.Tables[0].Rows.Count > 0)
                            {
                                for (int chqloop = 0; chqloop < dataResult.Tables[0].Rows.Count; chqloop++)
                                {
                                    __getchqNumber = dataResult.Tables[0].Rows[chqloop][0].ToString();
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + " = '" + __getchqNumber + "' and " + _g.d.cb_chq_list._chq_type + " = 1"));
                                }
                            }
                        }
                        catch
                        {
                        }
                        break;
                    case _bankControlTypeEnum.out_payment:
                        try
                        {
                            __qureychqNumber = "select " + _g.d.cb_trans_detail._trans_number + " from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + " = '" + __getDocNo + "' and " + _g.d.cb_trans_detail._trans_flag + " = " + _bankGlobal._bankType(BankControlType).ToString();
                            DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, __qureychqNumber);
                            if (dataResult.Tables[0].Rows.Count > 0)
                            {
                                for (int chqloop = 0; chqloop < dataResult.Tables[0].Rows.Count; chqloop++)
                                {
                                    __getchqNumber = dataResult.Tables[0].Rows[chqloop][0].ToString();
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + " = '" + __getchqNumber + "' and " + _g.d.cb_chq_list._chq_type + " = 2"));
                                }
                            }
                        }
                        catch
                        {
                        }
                        break;
                }
                __myQuery.Append(string.Format("<query>delete from {0} {1}</query>", this._bankScreenTop._table_name, getData.whereString + " and " + _g.d.cb_trans._trans_flag + " = " + _bankGlobal._bankType(BankControlType).ToString()));
                string myFormat = MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + _g.d.cb_trans_detail._doc_date + "=\'" + MyLib._myGlobal._convertDateToQuery(__getDate) + "\' and  " + _g.d.cb_trans_detail._doc_no + "=\'" + __getDocNo + "\'" + " and " + _g.d.cb_trans_detail._trans_flag + " = " + _bankGlobal._bankType(BankControlType).ToString());
                __myQuery.Append(string.Format(myFormat, _g.d.cb_trans_detail._table));
            }
            __myQuery.Append("</node>");
            //MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(0, null);
                this._myManageBank._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool _myManageBank__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                _get_column_number();
                _oldDate = (DateTime)__rowDataArray[_getColumnDocDate];
                _oldDocNo = __rowDataArray[_getColumnDocNo].ToString();
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_trans._table + whereString + " and " + _g.d.cb_trans._trans_flag + " = " + _bankGlobal._bankType(BankControlType).ToString()));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_date + "=\'" + MyLib._myGlobal._convertDateToQuery(_oldDate) + "\' and " + _g.d.cb_trans_detail._doc_no + "=\'" + _oldDocNo + "\'" + " and " + _g.d.cb_trans_detail._trans_flag + " = " + _bankGlobal._bankType(BankControlType).ToString()));
                __myquery.Append("</node>");
                ArrayList _getData = myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._bankScreenTop._loadData(((DataSet)_getData[0]).Tables[0]);
                this._bankScreenBottom._loadData(((DataSet)_getData[0]).Tables[0]);
                this._bankDetailGrid._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);
                _calcTotalGrid();
                //_search(true);
                if (forEdit)
                {
                    this._bankScreenTop._focusFirst();
                }
                // ให้คำนวณยอดรวม             
                this._bankScreenTop.Invalidate();
                this._bankScreenBottom.Invalidate();
                this._bankDetailGrid.Invalidate();
                _LoadFromScreen = true;
            }
            catch (Exception)
            {
            }
            return (false);
        }

        private void _saveData()
        {
            // กำหนด Focus ใหม่ ไม่งั้นข้อมูลจะไม่สมบูรณ์
            this._bankDetailGrid.Focus();
            string _getUserCode = MyLib._myGlobal._userCode;
            DateTime _getSaveDate = MyLib._myGlobal._workingDate;
            //เช็ค Taxt ว่าห้ามว่าง ทั้ง 2 screen
            string __getEmtry = this._bankScreenTop._checkEmtryField() + this._bankScreenBottom._checkEmtryField();
            if (__getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, __getEmtry);
            }
            else
            {
                //จับข้อมูลเข้า ArrayList ก่อน
                ArrayList __getDataTop = this._bankScreenTop._createQueryForDatabase();
                ArrayList __getDataBottom = this._bankScreenBottom._createQueryForDatabase();
                //เตรียมแพค xml
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                //เตรียม ข้อมูล และชื่อ ฟิว ต่างๆ จาก grid ที่ไม่ได้ สร้างไว้ใน grid
                string __fieldList = _g.d.cb_trans._doc_date + "," + _g.d.cb_trans._doc_no + "," + _g.d.cb_trans._trans_flag + "," + _g.d.cb_trans._trans_type + ",";
                string __dataList = this._bankScreenTop._getDataStrQuery(_g.d.cb_trans._doc_date) + ","
                    + this._bankScreenTop._getDataStrQuery(_g.d.cb_trans._doc_no) + ","
                    + _bankGlobal._bankType(BankControlType).ToString() + ","
                    + _TransTypeGlobal._TransType(TransType).ToString() + ",";
                //เอาผลรวม เตรียมเข้า grid
                int __getColumnNumberAmount = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._amount);
                int __getColumnNumberFee = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._fee_amount);
                int __getColumnNumberNetAmount = this._bankDetailGrid._findColumnByName(_g.d.cb_trans_detail._sum_amount);
                int __getColumnTransFlag = _bankGlobal._bankType(BankControlType);
                int __getColumnTransType = _TransTypeGlobal._TransType(TransType);

                //ไว้บันทึกในหน้าจอ
                string __extStr = _g.d.cb_trans._total_amount + "=" + ((MyLib._myGrid._columnType)this._bankDetailGrid._columnList[__getColumnNumberAmount])._total.ToString() +
                        "," + _g.d.cb_trans._total_fee_amount + "=" + ((MyLib._myGrid._columnType)this._bankDetailGrid._columnList[__getColumnNumberFee])._total.ToString() +
                        "," + _g.d.cb_trans._total_other_amount + "=" + this._bankScreenBottom._getDataNumber(_g.d.cb_trans._total_other_amount) +
                        "," + _g.d.cb_trans._total_net_amount + "=" + ((MyLib._myGrid._columnType)this._bankDetailGrid._columnList[__getColumnNumberFee])._total.ToString() +
                        "," + _g.d.cb_trans._trans_flag + " = " + __getColumnTransFlag +
                        "," + _g.d.cb_trans._trans_type + " = " + __getColumnTransType;
                //ไว้เช็คตอนอัพเดทในกริด
                string __fieldUpdate = _g.d.cb_trans._doc_date + "=" + this._bankScreenTop._getDataStrQuery(_g.d.cb_trans._doc_date) + ","
                    + _g.d.cb_trans._doc_no + "=" + this._bankScreenTop._getDataStrQuery(_g.d.cb_trans._doc_no) + ","
                    + _g.d.cb_trans._doc_group + "=" + this._bankScreenTop._getDataStrQuery(_g.d.cb_trans._doc_group) + ","
                    + _g.d.cb_trans._trans_flag + "=" + __getColumnTransFlag + ","
                    + _g.d.cb_trans._trans_type + "=" + __getColumnTransType;
                //ชื่อ ฟิวที่ ใช้ บันทึก เทเบิล chq list
                ArrayList __getDataExrea = this.__detailExtra._cbDetailExtraTopScreen._createQueryForDatabase();


                if (this._myManageBank._mode == 1)
                {
                    //บันทึกข้อมูลใหม่                   
                    switch (this.BankControlType)
                    {
                        case _bankControlTypeEnum.เช็ครับ:
                            for (int __row = 0; __row < this._bankDetailGrid._rowData.Count; __row++)
                            {
                                SMLERPControl._bank._cbDetailExtraObject __getExtraData = (SMLERPControl._bank._cbDetailExtraObject)this._bankDetailGrid._cellGet(__row, _g.d.cb_trans_detail._description);
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_chq_list._table + " (" + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_due_date + "," + _g.d.cb_chq_list._amount + "," + _g.d.cb_chq_list._currency_code +
                                "," + _g.d.cb_chq_list._chq_description + "," + _g.d.cb_chq_list._ap_ar_code + "," + _g.d.cb_chq_list._book_code + "," + _g.d.cb_chq_list._pass_book_code +
                                "," + _g.d.cb_chq_list._bank_code + "," + _g.d.cb_chq_list._bank_branch + "," + _g.d.cb_chq_list._chq_get_date + "," + _g.d.cb_chq_list._person_code +
                                "," + _g.d.cb_chq_list._side_code + "," + _g.d.cb_chq_list._department_code + "," + _g.d.cb_chq_list._remark + "," + _g.d.cb_chq_list._chq_type +
                                ") values ('" + __getExtraData._chq_number[__row].ToString() + "','" + MyLib._myGlobal._convertDateToQuery(DateTime.Parse(__getExtraData._chq_due_date[__row].ToString(), MyLib._myGlobal._cultureInfo())) +
                                "'," + __getExtraData._amount[__row].ToString() + ",'" + __getExtraData._currency_code[__row].ToString() +
                                "','" + __getExtraData._chq_description[__row].ToString() + "','" + __getExtraData._ap_ar_code[__row].ToString() +
                                "','" + __getExtraData._book_code[__row].ToString() + "','" + __getExtraData._pass_book_code[__row].ToString() +
                                "','" + __getExtraData._bank_code[__row].ToString() + "','" + __getExtraData._bank_branch[__row].ToString() +
                                "','" + MyLib._myGlobal._convertDateToQuery(DateTime.Parse(__getExtraData._chq_get_date[__row].ToString(), MyLib._myGlobal._cultureInfo())) + "','" + __getExtraData._person_code[__row].ToString() +
                                "','" + __getExtraData._side_code[__row].ToString() + "','" + __getExtraData._department_code[__row].ToString() +
                                "','" + __getExtraData._remark[__row].ToString() + "',1" +
                                ")"));
                            }
                            break;
                        case _bankControlTypeEnum.out_payment:
                            for (int __row = 0; __row < this._bankDetailGrid._rowData.Count; __row++)
                            {
                                SMLERPControl._bank._cbDetailExtraObject __getExtraData = (SMLERPControl._bank._cbDetailExtraObject)this._bankDetailGrid._cellGet(__row, _g.d.cb_trans_detail._description);
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_chq_list._table + " (" + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_due_date + "," + _g.d.cb_chq_list._amount + "," + _g.d.cb_chq_list._currency_code +
                                "," + _g.d.cb_chq_list._chq_description + "," + _g.d.cb_chq_list._ap_ar_code + "," + _g.d.cb_chq_list._book_code + "," + _g.d.cb_chq_list._pass_book_code +
                                "," + _g.d.cb_chq_list._bank_code + "," + _g.d.cb_chq_list._bank_branch + "," + _g.d.cb_chq_list._chq_get_date + "," + _g.d.cb_chq_list._person_code +
                                "," + _g.d.cb_chq_list._side_code + "," + _g.d.cb_chq_list._department_code + "," + _g.d.cb_chq_list._remark + "," + _g.d.cb_chq_list._chq_type +
                                ") values ('" + __getExtraData._chq_number[__row].ToString() + "','" + MyLib._myGlobal._convertDateToQuery(DateTime.Parse(__getExtraData._chq_due_date[__row].ToString(), MyLib._myGlobal._cultureInfo())) +
                                "'," + __getExtraData._amount[__row].ToString() + ",'" + __getExtraData._currency_code[__row].ToString() +
                                "','" + __getExtraData._chq_description[__row].ToString() + "','" + __getExtraData._ap_ar_code[__row].ToString() +
                                "','" + __getExtraData._book_code[__row].ToString() + "','" + __getExtraData._pass_book_code[__row].ToString() +
                                "','" + __getExtraData._bank_code[__row].ToString() + "','" + __getExtraData._bank_branch[__row].ToString() +
                                "','" + MyLib._myGlobal._convertDateToQuery(DateTime.Parse(__getExtraData._chq_get_date[__row].ToString(), MyLib._myGlobal._cultureInfo())) + "','" + __getExtraData._person_code[__row].ToString() +
                                "','" + __getExtraData._side_code[__row].ToString() + "','" + __getExtraData._department_code[__row].ToString() +
                                "','" + __getExtraData._remark[__row].ToString() + "',2" +
                                ")"));
                            }
                            break;
                    }
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" insert into " + _g.d.cb_trans._table +
                        " (" + __getDataTop[0].ToString() + "," + __getDataBottom[0] + "," + _g.d.cb_trans._trans_flag + "," + _g.d.cb_trans._trans_type +
                        ") values (" + __getDataTop[1].ToString() + "," + __getDataBottom[1].ToString() + "," + _bankGlobal._bankType(BankControlType).ToString() + "," + _TransTypeGlobal._TransType(TransType).ToString() + ")"));
                    __myQuery.Append(this._bankDetailGrid._createQueryForInsert(_g.d.cb_trans_detail._table, __fieldList, __dataList));
                }
                else
                {
                    //แก้ไขข้อมูล                    
                    switch (this.BankControlType)
                    {
                        case _bankControlTypeEnum.เช็ครับ:
                            for (int __row = 0; __row < this._bankDetailGrid._rowData.Count; __row++)
                            {
                                SMLERPControl._bank._cbDetailExtraObject __getExtraData = (SMLERPControl._bank._cbDetailExtraObject)this._bankDetailGrid._cellGet(__row, _g.d.cb_trans_detail._description);

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + " ='" + __getExtraData._chq_number[__row].ToString() + "' and " + _g.d.cb_chq_list._chq_type + " = 1"));
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_chq_list._table + " (" + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_due_date + "," + _g.d.cb_chq_list._amount + "," + _g.d.cb_chq_list._currency_code +
                                "," + _g.d.cb_chq_list._chq_description + "," + _g.d.cb_chq_list._ap_ar_code + "," + _g.d.cb_chq_list._book_code + "," + _g.d.cb_chq_list._pass_book_code +
                                "," + _g.d.cb_chq_list._bank_code + "," + _g.d.cb_chq_list._bank_branch + "," + _g.d.cb_chq_list._chq_get_date + "," + _g.d.cb_chq_list._person_code +
                                "," + _g.d.cb_chq_list._side_code + "," + _g.d.cb_chq_list._department_code + "," + _g.d.cb_chq_list._remark + "," + _g.d.cb_chq_list._chq_type +
                                ") values ('" + __getExtraData._chq_number[__row].ToString() + "','" + MyLib._myGlobal._convertDateToQuery(DateTime.Parse(__getExtraData._chq_due_date[__row].ToString(), MyLib._myGlobal._cultureInfo())) +
                                "'," + __getExtraData._amount[__row].ToString() + ",'" + __getExtraData._currency_code[__row].ToString() +
                                "','" + __getExtraData._chq_description[__row].ToString() + "','" + __getExtraData._ap_ar_code[__row].ToString() +
                                "','" + __getExtraData._book_code[__row].ToString() + "','" + __getExtraData._pass_book_code[__row].ToString() +
                                "','" + __getExtraData._bank_code[__row].ToString() + "','" + __getExtraData._bank_branch[__row].ToString() +
                                "','" + MyLib._myGlobal._convertDateToQuery(DateTime.Parse(__getExtraData._chq_get_date[__row].ToString(), MyLib._myGlobal._cultureInfo())) + "','" + __getExtraData._person_code[__row].ToString() +
                                "','" + __getExtraData._side_code[__row].ToString() + "','" + __getExtraData._department_code[__row].ToString() +
                                "','" + __getExtraData._remark[__row].ToString() + "',1" +
                                ")"));
                            }
                            break;
                        case _bankControlTypeEnum.out_payment:
                            for (int __row = 0; __row < this._bankDetailGrid._rowData.Count; __row++)
                            {
                                SMLERPControl._bank._cbDetailExtraObject __getExtraData = (SMLERPControl._bank._cbDetailExtraObject)this._bankDetailGrid._cellGet(__row, _g.d.cb_trans_detail._description);

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.cb_chq_list._table + " where " + _g.d.cb_chq_list._chq_number + " ='" + __getExtraData._chq_number[__row].ToString() + "' and " + _g.d.cb_chq_list._chq_type + " = 2"));
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" insert into " + _g.d.cb_chq_list._table + " (" + _g.d.cb_chq_list._chq_number + "," + _g.d.cb_chq_list._chq_due_date + "," + _g.d.cb_chq_list._amount + "," + _g.d.cb_chq_list._currency_code +
                                "," + _g.d.cb_chq_list._chq_description + "," + _g.d.cb_chq_list._ap_ar_code + "," + _g.d.cb_chq_list._book_code + "," + _g.d.cb_chq_list._pass_book_code +
                                "," + _g.d.cb_chq_list._bank_code + "," + _g.d.cb_chq_list._bank_branch + "," + _g.d.cb_chq_list._chq_get_date + "," + _g.d.cb_chq_list._person_code +
                                "," + _g.d.cb_chq_list._side_code + "," + _g.d.cb_chq_list._department_code + "," + _g.d.cb_chq_list._remark + "," + _g.d.cb_chq_list._chq_type +
                                ") values ('" + __getExtraData._chq_number[__row].ToString() + "','" + MyLib._myGlobal._convertDateToQuery(DateTime.Parse(__getExtraData._chq_due_date[__row].ToString(), MyLib._myGlobal._cultureInfo())) +
                                "'," + __getExtraData._amount[__row].ToString() + ",'" + __getExtraData._currency_code[__row].ToString() +
                                "','" + __getExtraData._chq_description[__row].ToString() + "','" + __getExtraData._ap_ar_code[__row].ToString() +
                                "','" + __getExtraData._book_code[__row].ToString() + "','" + __getExtraData._pass_book_code[__row].ToString() +
                                "','" + __getExtraData._bank_code[__row].ToString() + "','" + __getExtraData._bank_branch[__row].ToString() +
                                "','" + MyLib._myGlobal._convertDateToQuery(DateTime.Parse(__getExtraData._chq_get_date[__row].ToString(), MyLib._myGlobal._cultureInfo())) + "','" + __getExtraData._person_code[__row].ToString() +
                                "','" + __getExtraData._side_code[__row].ToString() + "','" + __getExtraData._department_code[__row].ToString() +
                                "','" + __getExtraData._remark[__row].ToString() + "',2" +
                                ")"));
                            }
                            break;
                    }
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.cb_trans._table + " set " + __extStr + "," + __getDataTop[2].ToString() + this._myManageBank._dataList._whereString + " and " + _g.d.cb_trans._trans_flag + " = " + _TransTypeGlobal._TransType(TransType).ToString()));
                    __myQuery.Append(this._bankDetailGrid._createQueryRowRemove(_g.d.cb_trans_detail._table));
                    // อย่าลืม Event _queryForUpdateWhere ไม่งั้นมันไม่ทำงานนะ                   
                    __myQuery.Append(this._bankDetailGrid._createQueryForUpdate(_g.d.cb_trans_detail._table, __fieldUpdate));
                    // ต่อท้ายด้วย Insert บรรทัดใหม่
                    __myQuery.Append(this._bankDetailGrid._createQueryForInsert(_g.d.cb_trans_detail._table, __fieldList, __dataList, true));
                }
                __myQuery.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    this._bankScreenTop._isChange = false;
                    this._bankScreenBottom._isChange = false;
                    this._bankScreenTop._clear();
                    this._bankScreenBottom._clear();
                    this._bankDetailGrid._clear();
                    this._bankScreenTop._focusFirst();
                    _autoRunning();
                    if (this._myManageBank._mode == 1)
                    {
                        this._myManageBank._afterInsertData();
                        string __getOldNumber = this._bankScreenTop._getDataStr(_g.d.cb_trans._doc_no);

                    }
                    else
                    {
                        this._myManageBank._afterUpdateData();
                    }
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Even การทำงาน From Load
        /// </summary>
        /// <param name="number">ทำงานครั้งแรกที่เปิดหน้าจอ</param>
        void _bankControl_Load(object sender, EventArgs e)
        {
            switch (BankControlType)
            {
                case _bankControlTypeEnum.ฝากเงิน:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString() + " and " + _g.d.cb_trans._trans_type;
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_ธนาคาร_ฝากเงิน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.cash_payin_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_ธนาคาร_ฝากเงิน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.cash_withdraw:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_cash_withdraw, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.cash_withdraw_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_cash_withdraw, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.received:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_bank_income, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.received_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_bank_income, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.payment:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_bank_expanse, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.payment_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_bank_expanse, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.interest_received:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_interest_received, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.interest_received_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_interest_received, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.interest_payment:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_interest_payment, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.interest_payment_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_interest_payment, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_received:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_cash_transfer, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_received_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_cash_transfer, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_payment:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_cash_transfer, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_payment_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_cash_transfer, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_payin:
                    _transFlagChqRef = 1;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_payin_cancle:
                    _transFlagChqRef = 17;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_return:
                    _transFlagChqRef = 17;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_return_cancle:
                    _transFlagChqRef = 19;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_honor:
                    _transFlagChqRef = 17;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_honor_cancle:
                    _transFlagChqRef = 21;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_change:
                    _transFlagChqRef = 17;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_change_cancle:
                    _transFlagChqRef = 23;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_new:
                    _transFlagChqRef = 17;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_new_cancle:
                    _transFlagChqRef = 25;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.out_return:
                    _transFlagChqRef = 2;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.out_return_cancle:
                    _transFlagChqRef = 27;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.out_honor:
                    _transFlagChqRef = 27;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.out_honor_cancle:
                    _transFlagChqRef = 29;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.out_change:
                    _transFlagChqRef = 27;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.out_change_cancle:
                    _transFlagChqRef = 31;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.credit_money:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.credit_cancel:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_receive:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_receive_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_request:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_request_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_return:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_return_cancle:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.เช็ครับ:
                    //_transFlagChqRef = 0;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.out_payment:
                    //_transFlagChqRef = 0;
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                //ส่วนเหลือ
                case _bankControlTypeEnum.in_discount:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.in_cancel:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_request_approve:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_request_approve_cancel:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_clear:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.out_cancel:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_cb_chq_trans, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _bankControlTypeEnum.petty_payment:
                    _myManageBank._dataList._extraWhere = _g.d.cb_trans._trans_flag + "=" + _bankGlobal._bankType(BankControlType).ToString();
                    _myManageBank._dataList._loadViewFormat(_g.g._search_screen_เงินสดย่อย_รายวัน, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                //ส่วนเหลือ
            }

            //this._myManageBank._dataList._refreshData();
            this._myManageBank._dataListOpen = true;
            this._myManageBank._calcArea();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            if (_myManageBank._dataList._loadViewDataSuccess == false)
            {
                this._myManageBank._dataList._loadViewData(0);
            }
        }

    }

    public class _bankDetailGridControl : MyLib._myGrid
    {
        private _bankControlTypeEnum _bankControlTypeTemp;

        public _bankControlTypeEnum BankControlType
        {
            set
            {
                this._bankControlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._bankControlTypeTemp;
            }
        }

        public _bankDetailGridControl()
        {
            this._build();
        }

        int __buildCount = 0;
        void _build()
        {
            if (++__buildCount > 1)
            {
                return;
            }
            this._columnList.Clear();
            this._table_name = _g.d.cb_trans_detail._table;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            switch (BankControlType)
            {
                case _bankControlTypeEnum.ฝากเงิน:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.cash_payin_cancle:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);                    
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.cash_withdraw:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch,1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.cash_withdraw_cancle:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch,1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.received:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);                    
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.received_cancle:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);                    
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.payment:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.payment_cancle:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.interest_received:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.interest_received_cancle:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.interest_payment:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.interest_payment_cancle:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_received:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_received_cancle:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_payment:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_payment_cancle:
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 16, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 14, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_description, 1, 255, 20, false, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 25, 14, true, false, true, true, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 25, 14, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 25, 14, false, false, true, false, __formatNumber);
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_payin:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_payin_cancle:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_return:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_return_cancle:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_honor:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_honor_cancle:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_change:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_change_cancle:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_new:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_new_cancle:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.out_return:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.out_return_cancle:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.out_honor:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.out_honor_cancle:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.out_change:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.out_change_cancle:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 12, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.credit_money:
                    this._addColumn(_g.d.cb_trans_detail._credit_card_no, 1, 20, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._credit_card_expire, 4, 15, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 12, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 12, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม                    
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.credit_cancel:
                    this._addColumn(_g.d.cb_trans_detail._credit_card_no, 1, 20, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._credit_card_expire, 4, 15, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 25, 12, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 25, 12, false, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 25, 10, false, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 12, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 12, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 12, true, false, true, true, __formatNumber);//ยอดเงินรวม                    
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_receive:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 11, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 11, true, false, true, true);
                    //this._addColumn(_g.d.cb_trans_detail._person_code, 1, 50, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 11, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 11, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 11, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 12, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_receive_cancle:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 11, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 11, true, false, true, true);
                    //this._addColumn(_g.d.cb_trans_detail._person_code, 1, 50, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 11, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 11, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 11, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 12, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_request:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 11, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 11, true, false, true, true);
                    //this._addColumn(_g.d.cb_trans_detail._person_code, 1, 50, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 11, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 11, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 11, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 12, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_request_cancle:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 11, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 11, true, false, true, true);
                    //this._addColumn(_g.d.cb_trans_detail._person_code, 1, 50, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 11, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 11, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 11, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 12, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_return:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 11, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 11, true, false, true, true);
                    //this._addColumn(_g.d.cb_trans_detail._person_code, 1, 50, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 11, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 11, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 11, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 12, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_return_cancle:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 11, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 11, true, false, true, true);
                    //this._addColumn(_g.d.cb_trans_detail._person_code, 1, 50, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 11, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 11, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 11, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 12, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.เช็ครับ:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค                    
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 12, true, false, true, true);//สาขาธนาคาร                    
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 11, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._description, 12, 0, 5, true, false, false);//รายละเอียดเป็น หมุด                   
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.out_payment:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 16, true, false, true, false);//เลขที่เช็ค                    
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 12, true, false, true, true);//ธนาคาร
                    this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 12, true, false, true, true);//สาขาธนาคาร                    
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 11, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 11, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 11, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._description, 12, 0, 5, true, false, false);//รายละเอียดเป็น หมุด                    
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;


                //ส่วนเหลือ
                case _bankControlTypeEnum.in_discount:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 7, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 7, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 7, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 7, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 7, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 7, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 7, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 7, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.in_cancel:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 7, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 7, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 7, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 7, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 7, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 7, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 7, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 7, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_request_approve:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 9, true, false, true, true);
                    //this._addColumn(_g.d.cb_trans_detail._person_code, 1, 50, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 9, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 9, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_request_approve_cancel:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 9, true, false, true, true);
                    //this._addColumn(_g.d.cb_trans_detail._person_code, 1, 50, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 9, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 9, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_clear:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 9, true, false, true, true);
                    //this._addColumn(_g.d.cb_trans_detail._person_code, 1, 50, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 9, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 9, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.petty_payment:
                    this._addColumn(_g.d.cb_trans_detail._petty_cash_code, 1, 20, 9, true, false, true, false);
                    this._addColumn(_g.d.cb_trans_detail._approve_date, 4, 15, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 9, true, false, true, true);
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 9, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 9, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 9, true, false, true, true, __formatNumber);//ยอดเงินรวม  
                    this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ                    
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                case _bankControlTypeEnum.out_cancel:
                    this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 20, 7, true, false, true, false);//เลขที่เช็ค
                    this._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 7, true, false, true, true);//อ้างอิงเช็คเลขที่
                    //this._addColumn(_g.d.cb_trans_detail._owner_name, 1, 50, 7, true, false, true, false);//ชื่อเจ้าของเช็ค
                    this._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 7, true, false, true, true);//ธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 7, true, false, true, false);//สาขาธนาคาร
                    //this._addColumn(_g.d.cb_trans_detail._chq_get_date, 4, 15, 7, true, false, true, true);//วันที่รับเช็ค                                        
                    //this._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 15, 7, true, false, true, true);//วันที่หมดอายุ
                    this._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 7, true, false, true, true, __formatNumber);//จำนวนเงิน
                    this._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 10, 7, true, false, true, true, __formatNumber);//ค่าธรรมเนียม
                    this._addColumn(_g.d.cb_trans_detail._other_amount, 3, 10, 7, true, false, true, true, __formatNumber);//ค่าใช้จ่ายอื่นๆ
                    this._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 10, 7, true, false, true, true, __formatNumber);//ภาษีหัก ณ ที่จ่าย
                    this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 10, 7, true, false, true, true, __formatNumber);//ยอดเงินรวม
                    //this._addColumn(_g.d.cb_trans_detail._chq_description, 1, 200, 16, true, false, true, false);
                    //this._addColumn(_g.d.cb_trans_detail._remark, 1, 200, 10, true, false, true, false);//หมายเหตุ
                    this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
                    break;
                //ส่วนเหลือ
            }
            this._calcPersentWidthToScatter();
            this.Invalidate();
        }
    }

    public class _bankScreenTopControl : MyLib._myScreen
    {
        private _bankControlTypeEnum __bankControlTypeTemp;

        public _bankControlTypeEnum BankControlType
        {
            set
            {
                this.__bankControlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this.__bankControlTypeTemp;
            }
        }

        public _bankScreenTopControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            switch (this.BankControlType)
            {
                case _bankControlTypeEnum.ฝากเงิน:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.cash_payin_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.cash_withdraw:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.cash_withdraw_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.received:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.received_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.payment:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.payment_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.interest_received:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.interest_received_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.interest_payment:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.interest_payment_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_received:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_received_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_payment:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_payment_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);//auto run
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);//search 
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);//search
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_trans._cust_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(2, 1, 1, 0, _g.d.cb_trans._book_code, 1, 1, 1, true, false, true);//search
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._description, 2, 1, 0, true, false, true);
                    this._addTextBox(6, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_payin:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_payin_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_return:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_return_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_honor:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_honor_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_change:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_change_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_new:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_new_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.out_return:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.out_return_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.out_honor:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.out_honor_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.out_change:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.out_change_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.credit_money:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.credit_cancel:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_receive:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);                    
                    this._addTextBox(2, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_receive_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);                    
                    this._addTextBox(2, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_request:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);
                    this._addTextBox(2, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_request_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);
                    this._addTextBox(2, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_return:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);
                    this._addTextBox(2, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_return_cancle:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);
                    this._addTextBox(2, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.เช็ครับ:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.out_payment:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                //ส่วนเหลือ
                case _bankControlTypeEnum.in_discount:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.in_cancel:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.out_cancel:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 0, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._book_code, 1, 0, 1, true, false, true);
                    this._addTextBox(2, 0, 2, 1, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_payment:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_request_approve:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_request_approve_cancel:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 1, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
                case _bankControlTypeEnum.petty_clear:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.cb_trans._doc_date, 1, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_trans._doc_no, 1, 1, 3, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_trans._doc_ref, 1, 1, 0, true, false, true);
                    //this._addComboBox(2, 0, _g.d.cb_trans._money_type, true, _g.g._moneyType, false);
                    this._addTextBox(3, 0, 3, 0, _g.d.cb_trans._remark, 2, 1, 0, true, false, true);
                    break;
            }
            this._setDataDate(_g.d.cb_trans._doc_date, MyLib._myGlobal._workingDate);
            this.Invalidate();
            this.ResumeLayout();

        }

    }
    public class _bankScreenBottomControl : MyLib._myScreen
    {
        private _bankControlTypeEnum __bankControlTypeTemp;

        public _bankControlTypeEnum BankControlType
        {
            set
            {
                this.__bankControlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this.__bankControlTypeTemp;
            }
        }

        public _bankScreenBottomControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            switch (BankControlType)
            {
                case _bankControlTypeEnum.ฝากเงิน:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.cash_payin_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.cash_withdraw:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.cash_withdraw_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.received:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.received_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.payment:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.payment_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.interest_received:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.interest_received_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.interest_payment:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.interest_payment_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_received:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_received_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_payment:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.cash_transfer_payment_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_payin:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_payin_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_return:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_return_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_honor:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_honor_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_change:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_change_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_new:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_new_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.out_return:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.out_return_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.out_honor:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.out_honor_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.out_change:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.out_change_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.credit_money:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.credit_cancel:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_receive:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_receive_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_request:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_request_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_return:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_return_cancle:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.เช็ครับ:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.out_payment:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                //ส่วนเหลือ
                case _bankControlTypeEnum.in_discount:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.in_cancel:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_request_approve:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_request_approve_cancel:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_clear:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.petty_payment:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                case _bankControlTypeEnum.out_cancel:
                    this._maxColumn = 3;
                    this._table_name = _g.d.cb_trans._table;
                    this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._total_fee_amount, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_other_amount, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true);
                    break;
                //ส่วนเหลือ
            }
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public static class _TransTypeGlobal
    {
        public static int _TransType(_TransTypeEnum TransType)
        {
            switch (TransType)
            {
                case _TransTypeEnum.Cash: return 1;
                case _TransTypeEnum.Checks: return 2;
                case _TransTypeEnum.PettyCash: return 3;
                case _TransTypeEnum.CreditCard: return 4;
            }
            return 0;
        }
    }
    public enum _TransTypeEnum
    {
        /// <summary>
        /// 1.Cash : เงินสด
        /// </summary>
        Cash,
        /// <summary>
        /// 2.Checks : เช็ค
        /// </summary>
        Checks,
        /// <summary>
        /// 3.Petty Cash : เงินสดย่อย
        /// </summary>
        PettyCash,
        /// <summary>
        /// 4.Credit Card : เครดิต
        /// </summary>
        CreditCard
    }

    public static class _bankGlobal
    {
        public static int _bankType(_bankControlTypeEnum BankControlType)
        {
            switch (BankControlType)
            {
                case _bankControlTypeEnum.ฝากเงิน: return 1;
                case _bankControlTypeEnum.cash_payin_cancle: return 2;
                case _bankControlTypeEnum.cash_withdraw: return 3;
                case _bankControlTypeEnum.cash_withdraw_cancle: return 4;
                case _bankControlTypeEnum.received: return 5;
                case _bankControlTypeEnum.received_cancle: return 6;
                case _bankControlTypeEnum.payment: return 7;
                case _bankControlTypeEnum.payment_cancle: return 8;
                case _bankControlTypeEnum.interest_received: return 9;
                case _bankControlTypeEnum.interest_received_cancle: return 10;
                case _bankControlTypeEnum.interest_payment: return 11;
                case _bankControlTypeEnum.interest_payment_cancle: return 12;
                case _bankControlTypeEnum.cash_transfer_received: return 13;
                case _bankControlTypeEnum.cash_transfer_received_cancle: return 14;
                case _bankControlTypeEnum.cash_transfer_payment: return 15;
                case _bankControlTypeEnum.cash_transfer_payment_cancle: return 16;
                case _bankControlTypeEnum.in_payin: return 17;
                case _bankControlTypeEnum.in_payin_cancle: return 18;
                case _bankControlTypeEnum.in_return: return 19;
                case _bankControlTypeEnum.in_return_cancle: return 20;
                case _bankControlTypeEnum.in_honor: return 21;
                case _bankControlTypeEnum.in_honor_cancle: return 22;
                case _bankControlTypeEnum.in_change: return 23;
                case _bankControlTypeEnum.in_change_cancle: return 24;
                case _bankControlTypeEnum.in_new: return 25;
                case _bankControlTypeEnum.in_new_cancle: return 26;
                case _bankControlTypeEnum.out_return: return 27;
                case _bankControlTypeEnum.out_return_cancle: return 28;
                case _bankControlTypeEnum.out_honor: return 29;
                case _bankControlTypeEnum.out_honor_cancle: return 30;
                case _bankControlTypeEnum.out_change: return 31;
                case _bankControlTypeEnum.out_change_cancle: return 32;
                case _bankControlTypeEnum.credit_money: return 33;
                case _bankControlTypeEnum.credit_cancel: return 34;
                case _bankControlTypeEnum.petty_receive: return 35;
                case _bankControlTypeEnum.petty_receive_cancle: return 36;
                case _bankControlTypeEnum.petty_request: return 37;
                case _bankControlTypeEnum.petty_request_cancle: return 38;
                case _bankControlTypeEnum.petty_return: return 39;
                case _bankControlTypeEnum.petty_return_cancle: return 40;
                case _bankControlTypeEnum.เช็ครับ: return 41;
                case _bankControlTypeEnum.out_payment: return 42;
                //ส่วนเหลือ
                case _bankControlTypeEnum.in_discount: return 0;
                case _bankControlTypeEnum.in_cancel: return 0;
                case _bankControlTypeEnum.petty_payment: return 0;
                case _bankControlTypeEnum.out_cancel: return 0;
                case _bankControlTypeEnum.petty_request_approve: return 0;
                case _bankControlTypeEnum.petty_request_approve_cancel: return 0;
                case _bankControlTypeEnum.petty_clear: return 0;

            }
            return 0;
        }
    }

    public enum _bankControlTypeEnum
    {
        /// <summary>
        /// 1.cash payin : บันทึกฝากเงิน
        /// </summary>
        ฝากเงิน,
        /// <summary>
        /// 2.cash payin cancle : ยกเลิกบันทึกฝากเงิน
        /// </summary>
        cash_payin_cancle,
        /// <summary>
        /// 3.cash withdraw : บันทึกถอนเงิน
        /// </summary>
        cash_withdraw,
        /// <summary>
        /// 4.cash withdraw cancle : ยกเลิกบันทึกถอนเงิน
        /// </summary>
        cash_withdraw_cancle,
        /// <summary>        
        /// 5.received : รายได้จากธนาคาร
        /// </summary>
        received,
        /// <summary>        
        /// 6.received cancle : ยกเลิกรายได้จากธนาคาร
        /// </summary>
        received_cancle,
        /// <summary>
        /// 7.payment : ค่าใช้จ่ายจากธนาคาร
        /// </summary>
        payment,
        /// <summary>
        /// 8.payment cancle : ยกเลิกค่าใช้จ่ายจากธนาคาร
        /// </summary>
        payment_cancle,
        /// <summary>
        /// 9.interest received : ดอกเบี้ยรับ
        /// </summary>
        interest_received,
        /// <summary>
        /// 10.interest received cancle : ยกเลิกดอกเบี้ยรับ
        /// </summary>
        interest_received_cancle,
        /// <summary>
        /// 11.interest payment : ดอกเบี้ยจ่าย
        /// </summary>
        interest_payment,
        /// <summary>
        /// 12.interest payment cancle : ยกเลิกดอกเบี้ยจ่าย
        /// </summary>
        interest_payment_cancle,
        /// <summary>
        /// 13.cash transfer received : บันทึกโอนเงินเข้า
        /// </summary>
        cash_transfer_received,
        /// <summary>
        /// 14.cash transfer received cancle : ยกเลิกบันทึกโอนเงินเข้า
        /// </summary>
        cash_transfer_received_cancle,
        /// <summary>
        /// 15.cash transfer payment : บันทึกโอนเงินออก
        /// </summary>
        cash_transfer_payment,
        /// <summary>
        /// 16.cash transfer payment cancle : ยกเลิกบันทึกโอนเงินออก
        /// </summary>
        cash_transfer_payment_cancle,
        /// <summary>
        /// 17.in payin : บันทึกนำฝากเช็ครับ
        /// </summary>
        in_payin,
        /// <summary>
        /// 18.in payin cancle : บันทึกยกเลิกนำฝากเช็ครับ
        /// </summary>
        in_payin_cancle,
        /// <summary>
        /// 19.in return : บันทึกเช็ครับคืน
        /// </summary>
        in_return,
        /// <summary>
        /// 20.in return cancle : บันทึกยกเลิกเช็ครับคืน
        /// </summary>
        in_return_cancle,
        /// <summary>
        /// 21.in honor : บันทึกเช็ครับผ่าน
        /// </summary>
        in_honor,
        /// <summary>
        /// 22.in honor cancle : บันทึกยกเลิกเช็ครับผ่าน
        /// </summary>
        in_honor_cancle,
        /// <summary>
        /// 23.in change : บันทึกเปลี่ยนเช็คนำฝาก
        /// </summary>
        in_change,
        /// <summary>
        /// 24.in change cancle : บันทึกยกเลิกเปลี่ยนเช็คนำฝาก
        /// </summary>
        in_change_cancle,
        /// <summary>
        /// 25.in new : บันทึกเช็คนำเข้าใหม่
        /// </summary>
        in_new,
        /// <summary>
        /// 26.in new cancle : บันทึกยกเลิกเช็คนำเข้าใหม่
        /// </summary>
        in_new_cancle,
        /// <summary>
        /// 27.out return : บันทึกเช็คจ่ายคืน
        /// </summary>
        out_return,
        /// <summary>
        /// 28.out return cancle : บันทึกยกเลิกเช็คจ่ายคืน
        /// </summary>
        out_return_cancle,
        /// <summary>
        /// 29.out honor : บันทึกเช็คจ่ายผ่าน 
        /// </summary>        
        out_honor,
        /// <summary>
        /// 30.out honor cancle : บันทึกยกเลิกเช็คจ่ายผ่าน 
        /// </summary>        
        out_honor_cancle,
        /// <summary>
        /// 31.out change : บันทึกเปลี่ยนเช็คจ่าย
        /// </summary>
        out_change,
        /// <summary>
        /// 32.out change cancle : บันทึกยกเลิกเปลี่ยนเช็คจ่าย
        /// </summary>
        out_change_cancle,
        /// <summary>
        /// 33.credit money : บันทึกรายการขึ้นเงินบัตรเครดิต
        /// </summary>
        credit_money,
        /// <summary>
        /// 34.credit card cancel : บันทึกยกเลิกรายการบัตรเครดิต
        /// </summary>
        credit_cancel,
        /// <summary>
        /// 35.petty cash receive : บันทึกรับเงินสดย่อย
        /// </summary>
        petty_receive,
        /// <summary>
        /// 36.petty cash receive cancle : บันทึกยกเลิกรับเงินสดย่อย
        /// </summary>
        petty_receive_cancle,
        /// <summary>
        /// 37.petty cash request : บันทึกขอเบิกเงินสดย่อย
        /// </summary>
        petty_request,
        /// <summary>
        /// 38.petty cash request cancel : ยกเลิกขอเบิกเงินสดย่อย
        /// </summary>
        petty_request_cancle,
        /// <summary>
        /// 39.petty cash return : บันทึกรับคืนเงินสดย่อย
        /// </summary>
        petty_return,
        /// <summary>
        /// 40.petty cash return cancel : ยกเลิกรับคืนเงินสดย่อย
        /// </summary>
        petty_return_cancle,
        /// <summary>
        /// 41.chq in receive : บันทึกเช็ครับ
        /// </summary>
        เช็ครับ,
        /// <summary>
        /// 42.chq out payment : บันทึกเช็คจ่าย
        /// </summary>
        out_payment,
        /// <summary>
        /// 000000.in_discount : บันทึกขายลดเช็ค
        /// </summary>
        in_discount,
        /// <summary>
        /// 000000.in_cancel : บันทึกยกเลิกเช็ครับ
        /// </summary>
        in_cancel,
        /// <summary>
        /// 000000.out_cancel : บันทึกยกเลิกเช็คจ่าย
        /// </summary>
        out_cancel,
        /// <summary>
        /// 000000.petty_cash_payment : บันทึกจ่ายเงินสดย่อย
        /// </summary>
        petty_payment,
        /// <summary>
        /// 000000.petty_cash_request_approve : อนุมัติใบขอเบิกเงินสดย่อย
        /// </summary>
        petty_request_approve,
        /// <summary>
        /// 000000.petty_cash_request_approve_cancel : ยกเลิกอนุมัติขอเบิกเงินสดย่อย
        /// </summary>
        petty_request_approve_cancel,
        /// <summary>
        /// 000000.petty_clear : ปิดเงินสดย่อย/จ่ายเงินล่วงหน้า
        /// </summary>
        petty_clear

    }

    public class _cbDetailExtraObject
    {
        public ArrayList _chq_number = new ArrayList();
        public ArrayList _chq_due_date = new ArrayList();
        public ArrayList _amount = new ArrayList();
        public ArrayList _currency_code = new ArrayList();
        public ArrayList _chq_description = new ArrayList();
        public ArrayList _ap_ar_code = new ArrayList();
        public ArrayList _book_code = new ArrayList();
        public ArrayList _pass_book_code = new ArrayList();
        public ArrayList _bank_code = new ArrayList();
        public ArrayList _bank_branch = new ArrayList();
        public ArrayList _chq_get_date = new ArrayList();
        public ArrayList _person_code = new ArrayList();
        public ArrayList _side_code = new ArrayList();
        public ArrayList _department_code = new ArrayList();
        public ArrayList _remark = new ArrayList();
    }

    public class _cbDetailExtraDetailClass
    {
        public string _chq_number;
        public string _chq_due_date;
        public decimal _amount;
        public string _currency_code;
        public string _chq_description;
        public string _ap_ar_code;
        public string _book_code;
        public string _pass_book_code;
        public string _bank_code;
        public string _bank_branch;
        public string _chq_get_date;
        public string _person_code;
        public string _side_code;
        public string _department_code;
        public string _remark;
    }
}
