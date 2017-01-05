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
    public partial class _creditMasterControl : UserControl
    {
        private _creditMasterControlTypeEnum __creditMasterControlTypeTemp;

        public _creditMasterControlTypeEnum creditMasterControlType
        {
            set
            {
                this.__creditMasterControlTypeTemp = value;
                this._creditMasterScreenTop.creditMasterControlType = value;
                _EvenScreenSearch();
                this.Invalidate();
            }
            get
            {
                return this.__creditMasterControlTypeTemp;
            }
        }

        MyLib._searchDataFull _searchDocGroup = new MyLib._searchDataFull();//ค้นหากลุ่มเอกสาร
        MyLib._searchDataFull _searchCustCode = new MyLib._searchDataFull();//ค้นหาลูกหนี้เจ้าหนี้
        MyLib._searchDataFull _searchBookCode = new MyLib._searchDataFull();//ค้นหาสมุดรายวัน
        MyLib._searchDataFull _searchPassBook = new MyLib._searchDataFull();//ค้นหาเลขสมุดธนาคาร
        MyLib._searchDataFull _searchCurrency = new MyLib._searchDataFull();//ค้นหารหัสสกุลเงิน
        MyLib._searchDataFull _searchPersonCode = new MyLib._searchDataFull();//ผู้รับ/จ่ายเช็ค
        string _searchName = "";
        TextBox _searchTextBox;
        public _creditMasterControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //_myManageData        
            this._myManageCreditMaster._dataList._lockRecord = true;
            this._myManageCreditMaster._displayMode = 0;
            this._myManageCreditMaster._selectDisplayMode(this._myManageCreditMaster._displayMode);
            this._myManageCreditMaster._manageButton = this._myToolbar;
            this._myManageCreditMaster._manageBackgroundPanel = this._myPanel1;

            this._myManageCreditMaster._autoSize = true;
            this._myManageCreditMaster._autoSizeHeight = 400;           
            this._myManageCreditMaster.Invalidate();

            //even
            this.Load += new EventHandler(_creditMasterControl_Load);
            //ManageData
            // ตัวนี้เอาไว้อ้างเพื่อเป็นตัวเชื่อม (relations) เพื่อใช้ในการแก้ไข จะได้ Update ถูก Record
            this._myManageCreditMaster._dataList._referFieldAdd(_g.d.cb_credit_card._credit_card_no, 1);
            this._myManageCreditMaster._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageCreditMaster__loadDataToScreen);
            this._myManageCreditMaster._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageCreditMaster._newDataClick += new MyLib.NewDataEvent(_myManageCreditMaster__newDataClick);
            this._myManageCreditMaster._discardData += new MyLib.DiscardDataEvent(_myManageCreditMaster__discardData);
            this._myManageCreditMaster._clearData += new MyLib.ClearDataEvent(_myManageCreditMaster__clearData);
            //ปิดหมดทุกอย่าง
            this._myManageCreditMaster._dataList._buttonDelete.Enabled = false;
            this._myManageCreditMaster._dataList._buttonNew.Enabled = false;
            this._myManageCreditMaster._dataList._buttonNewFromTemp.Enabled = false;
            this._myManageCreditMaster._dataList._buttonSelectAll.Enabled = false;            
            this._saveButton.Enabled = false;
            
            //ค้นหา screen Top                         
            this._creditMasterScreenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_creditMasterScreenTop__textBoxChanged);            
            this._creditMasterScreenTop._textBoxSearch += new MyLib.TextBoxSearchHandler(_creditMasterScreenTop__textBoxSearch);
            //popup screen top            
            //กลุ่มเอกสาร
            _searchDocGroup._name = _g.g._search_screen_erp_doc_group;
            _searchDocGroup._dataList._loadViewFormat(_searchDocGroup._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchDocGroup._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchDocGroup._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDocGroup__searchEnterKeyPress);
            //สมุดรายวัน
            _searchBookCode._name = _g.g._search_screen_gl_journal_book;
            _searchBookCode._dataList._loadViewFormat(_searchBookCode._name, MyLib._myGlobal._userSearchScreenGroup, false);            
            _searchBookCode._dataList._gridData._mouseClick +=new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchBookCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchBookCode__searchEnterKeyPress);
            //ลูกหนี้เจ้าหนี้
            _searchCustCode._name = _g.g._search_screen_ar;
            _searchCustCode._dataList._loadViewFormat(_searchCustCode._name, MyLib._myGlobal._userSearchScreenGroup, false);            
            _searchCustCode._dataList._gridData._mouseClick +=new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchCustCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchCustCode__searchEnterKeyPress);
            //สมุดธนาคาร
            _searchPassBook._name = _g.g._search_screen_สมุดเงินฝาก;
            _searchPassBook._dataList._loadViewFormat(_searchPassBook._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchPassBook._dataList._gridData._mouseClick +=new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchPassBook._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchPassBook__searchEnterKeyPress);
            //สกุลเงิน
            _searchCurrency._name = _g.g._search_screen_erp_currency;
            _searchCurrency._dataList._loadViewFormat(_searchCurrency._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchCurrency._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchCurrency._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchCurrency__searchEnterKeyPress);
            //ผู้รับ/จ่ายเช็ค
            _searchPersonCode._name = _g.g._search_screen_erp_user;
            _searchPersonCode._dataList._loadViewFormat(_searchPersonCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchPersonCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchPersonCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchPersonCode__searchEnterKeyPress);
            
        }
        /// <summary>
        /// ฟังชั่น สร้าง Even ของ Grid และ Screen 
        /// </summary>
        /// <param name="number"></param> 
        void _EvenScreenSearch()
        {
            //กลุ่มเอกสาร
            //MyLib._myTextBox __getDocGroupControl = (MyLib._myTextBox)this._creditMasterScreenTop._getControl(_g.d.cb_credit_card._doc_group);
            //สมุดรายวัน
            MyLib._myTextBox __getBookCodeControl = (MyLib._myTextBox)this._creditMasterScreenTop._getControl(_g.d.cb_credit_card._book_code);
            //ลูกหนี้เจ้าหนี้
            MyLib._myTextBox __getCustCodeControl = (MyLib._myTextBox)this._creditMasterScreenTop._getControl(_g.d.cb_credit_card._ar_code);
            //ค้นหาเลขสมุดธนาคาร
            MyLib._myTextBox __getPassBookControl = (MyLib._myTextBox)this._creditMasterScreenTop._getControl(_g.d.cb_credit_card._pass_book_code);
            //ค้นหารหัสสกุลเงิน
            MyLib._myTextBox __getCurrencyControl = (MyLib._myTextBox)this._creditMasterScreenTop._getControl(_g.d.cb_credit_card._currency_code);
            //ผู้รับ/จ่ายเช็ค
            MyLib._myTextBox __getPersonCodeControl = (MyLib._myTextBox)this._creditMasterScreenTop._getControl(_g.d.cb_credit_card._person_code);
            //กลุ่มเอกสาร                    
            //if (__getDocGroupControl != null)
            //{
            //    __getDocGroupControl.textBox.Enter -= new EventHandler(textBox_Enter);
            //    __getDocGroupControl.textBox.Leave -= new EventHandler(textBox_Leave);
            //    __getDocGroupControl.textBox.Enter += new EventHandler(textBox_Enter);
            //    __getDocGroupControl.textBox.Leave += new EventHandler(textBox_Leave);
            //}
            //สมุดรายวัน                    
            if (__getBookCodeControl != null)
            {
                //__getBookCodeControl.textBox.Enter -= new EventHandler(textBox_Enter);
                //__getBookCodeControl.textBox.Leave -= new EventHandler(textBox_Leave);
                __getBookCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
                __getBookCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
            }
            //ลูกหนี้เจ้าหนี้                    
            if (__getCustCodeControl != null)
            {
                //__getCustCodeControl.textBox.Enter -= new EventHandler(textBox_Enter);
                //__getCustCodeControl.textBox.Leave -= new EventHandler(textBox_Leave);
                __getCustCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
                __getCustCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
            }
            //สมุดธนาคาร
            if (__getPassBookControl != null)
            {
                __getPassBookControl.textBox.Enter += new EventHandler(textBox_Enter);
                __getPassBookControl.textBox.Leave += new EventHandler(textBox_Leave);
            }
            //สกุลเงิน
            if (__getCurrencyControl != null)
            {
                __getCurrencyControl.textBox.Enter += new EventHandler(textBox_Enter);
                __getCurrencyControl.textBox.Leave += new EventHandler(textBox_Leave);
            }
            //ผู้รับ/จ่ายเช็ค
            if (__getPersonCodeControl != null)
            {
                __getPersonCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
                __getPersonCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
            }
        }
        /// <summary>
        /// ฟังชั่น การค้น ของ Grid และ Screen 
        /// </summary>
        /// <param name="number"></param> 
        void textBox_Leave(object sender, EventArgs e)
        {
            _searchDocGroup.Visible = false;
            _searchBookCode.Visible = false;            
            _searchCustCode.Visible = false;
            _searchPassBook.Visible = false;
            _searchCurrency.Visible = false;
            _searchPersonCode.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _textBoxStartSearch(__getControl);
            __getControl.textBox.Focus();
        }

        void _searchCustCode__searchEnterKeyPress(MyLib._myGrid sender, int row)
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

        void _searchPersonCode__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchCurrency__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchPassBook__searchEnterKeyPress(MyLib._myGrid sender, int row)
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

        void _creditMasterScreenTop__textBoxChanged(object sender, string name)
        {
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
            //if (name.Equals(_g.d.cb_credit_card._doc_group))
            //{
            //    _searchTextBox = (TextBox)sender;
            //    _searchName = name;
            //    MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocGroup, false);
            //}
            if (name.Equals(_g.d.cb_credit_card._book_code))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchBookCode, false);
            }            
            if (name.Equals(_g.d.cb_credit_card._ar_code))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchCustCode, false);
            }
            if (name.Equals(_g.d.cb_credit_card._pass_book_code))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchPassBook, false);
            }
            if (name.Equals(_g.d.cb_credit_card._currency_code))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                MyLib._myGlobal._startSearchBox(getControl, label_name,_searchCurrency, false);
            }
            if (name.Equals(_g.d.cb_credit_card._person_code))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchPersonCode, false);
            }
        }
        
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;                        
            //if (name.CompareTo(_g.g._search_screen_erp_doc_group) == 0)
            //{
            //    _searchDocGroup.Close();
            //    this._creditMasterScreenTop._setDataStr(_searchName, e._text);
            //    //_search(false);
            //    //กลุ่มเอกสาร
            //    string __query = "select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where " + _g.d.erp_doc_group._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._doc_group) + "\'";
            //    this._searchAndWarning(_g.d.cb_credit_card._doc_group, __query);
            //}
            if (name.CompareTo(_g.g._search_screen_gl_journal_book) == 0)
            {
                _searchBookCode.Close();
                this._creditMasterScreenTop._setDataStr(_searchName, e._text);
                //_search(false);
                //สมุดรายวัน
                string __query = "select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + _g.d.gl_journal_book._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._book_code) + "\'";
                this._searchAndWarning(_g.d.cb_credit_card._book_code, __query);
            }
            else if (name.CompareTo(_g.g._search_screen_ar) == 0)
            {
                this._searchCustCode.Close();
                this._creditMasterScreenTop._setDataStr(_searchName, e._text);
                // _search(false);
                //ลูกหนี้
                string __query = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._ar_code) + "\'";
                this._searchAndWarning(_g.d.cb_credit_card._ar_code, __query);
            }
            else if (name.CompareTo(_g.g._search_screen_ar) == 0)
            {
                this._searchPassBook.Close();
                this._creditMasterScreenTop._setDataStr(_searchName, e._text);
                // _search(false);
                //สมุดธนาคาร
                string __query = "select " + _g.d.erp_pass_book._name_1 + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._pass_book_code) + "\'";
                this._searchAndWarning(_g.d.cb_credit_card._pass_book_code, __query);
            }
            else if (name.CompareTo(_g.g._search_screen_erp_currency) == 0)
            {
                this._searchCurrency.Close();
                this._creditMasterScreenTop._setDataStr(_searchName, e._text);
                // _search(false);
                //สกุลเงิน
                string __query = "select " + _g.d.erp_currency._name_1 + " from " + _g.d.erp_currency._table + " where " + _g.d.erp_currency._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._currency_code) + "\'";
                this._searchAndWarning(_g.d.cb_credit_card._currency_code, __query);
            }
            else if (name.CompareTo(_g.g._search_screen_erp_user) == 0)
            {
                this._searchPersonCode.Close();
                this._creditMasterScreenTop._setDataStr(_searchName, e._text);
                // _search(false);
                //ผู้รับ/จ่ายเช็ค
                string __query = "select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._person_code) + "\'";
                this._searchAndWarning(_g.d.cb_credit_card._person_code, __query);
            }            
        }
        void _creditMasterScreenTop__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            //เมื่อ คลิกปุ่มค้นหาจะมี popup ขึ้นมาให้เลือกรหัส   
            //if (name.Equals(_g.d.cb_credit_card._doc_no))
            //{
            //    string __getNewCode = _autoRunning();
            //    if (__getNewCode.Length > 0)
            //    {
            //        this._creditMasterScreenTop._setDataStr(_g.d.cb_credit_card._doc_no, __getNewCode);
            //    }
            //}            
            //if (name.CompareTo(_g.d.cb_credit_card._doc_group) == 0)
            //{
            //    MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
            //    _searchName = name;
            //    _searchTextBox = getControl.textBox;
            //    MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocGroup, false);
            //}
            if (name.CompareTo(_g.d.cb_credit_card._book_code) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchBookCode, false);
            }            
            if (name.CompareTo(_g.d.cb_credit_card._ar_code) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchCustCode, false);
            }
            if (name.Equals(_g.d.cb_credit_card._pass_book_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;                
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchPassBook, false);
            }
            if (name.Equals(_g.d.cb_credit_card._currency_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchCurrency, false);
            }
            if (name.Equals(_g.d.cb_credit_card._person_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchPersonCode, false);
            }
        }

        void _textBoxStartSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            //if (name.Equals(_g.d.cb_credit_card._doc_group))
            //{
            //    MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
            //    _searchName = name;
            //    _searchTextBox = getControl.textBox;
            //    MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocGroup, false);
            //}
            if (name.Equals(_g.d.cb_credit_card._book_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchBookCode, false);
            }            
            if (name.CompareTo(_g.d.cb_credit_card._ar_code) == 0)
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchCustCode, false);
            }
            if (name.Equals(_g.d.cb_credit_card._pass_book_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchPassBook, false);
            }
            if (name.Equals(_g.d.cb_credit_card._currency_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchCurrency, false);
            }
            if (name.Equals(_g.d.cb_credit_card._person_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchPersonCode, false);
            }
        }

        void _searchAll(string name, int row)
        {            
            //if (name.CompareTo(_g.g._search_screen_erp_doc_group) == 0)
            //{
            //    string result = (string)_searchDocGroup._dataList._gridData._cellGet(row, 0);
            //    if (result.Length > 0)
            //    {
            //        _searchDocGroup.Close();
            //        this._creditMasterScreenTop._setDataStr(_searchName, result);
            //        //_search(false);
            //        //กลุ่มเอกสาร
            //        string __query = "select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where " + _g.d.erp_doc_group._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._doc_group) + "\'";
            //        this._searchAndWarning(_g.d.cb_credit_card._doc_group, __query);
            //    }
            //}
             if (name.CompareTo(_g.g._search_screen_gl_journal_book) == 0)
            {
                string result = (string)_searchBookCode._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {

                    _searchBookCode.Close();
                    this._creditMasterScreenTop._setDataStr(_searchName, result);
                    //_search(false);
                    string __query = "select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + _g.d.gl_journal_book._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._book_code) + "\'";
                    this._searchAndWarning(_g.d.cb_credit_card._book_code, __query);
                }
            }            
            else if (name.CompareTo(_g.g._search_screen_ar) == 0)
            {
                string result = (string)this._searchCustCode._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    this._searchCustCode.Close();
                    this._creditMasterScreenTop._setDataStr(_searchName, result);
                    // _search(false);
                    //ลูกหนี้
                    string __query = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._ar_code) + "\'";
                    this._searchAndWarning(_g.d.cb_credit_card._ar_code, __query);
                }
            }
            else if (name.CompareTo(_g.g._search_screen_สมุดเงินฝาก) == 0)
            {
                string result = (string)this._searchPassBook._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    this._searchPassBook.Close();
                    this._creditMasterScreenTop._setDataStr(_searchName, result);
                    // _search(false);
                    //สมุดธนาคาร
                    string __query = "select " + _g.d.erp_pass_book._name_1 + " from " + _g.d.erp_pass_book._table + " where " + _g.d.erp_pass_book._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._pass_book_code) + "\'";
                    this._searchAndWarning(_g.d.cb_credit_card._pass_book_code, __query);
                }
            }
            else if (name.CompareTo(_g.g._search_screen_erp_currency) == 0)
            {
                string result = (string)this._searchCurrency._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    this._searchCurrency.Close();
                    this._creditMasterScreenTop._setDataStr(_searchName, result);
                    // _search(false);
                    //สกุลเงิน
                    string __query = "select " + _g.d.erp_currency._name_1 + " from " + _g.d.erp_currency._table + " where " + _g.d.erp_currency._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._currency_code) + "\'";
                    this._searchAndWarning(_g.d.cb_credit_card._currency_code, __query);
                }
            }
            else if (name.CompareTo(_g.g._search_screen_erp_user) == 0)
            {
                string result = (string)this._searchPersonCode._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    this._searchPersonCode.Close();
                    this._creditMasterScreenTop._setDataStr(_searchName, result);
                    // _search(false);
                    //ผู้รับจ่ายเช็ค
                    string __query = "select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._creditMasterScreenTop._getDataStr(_g.d.cb_credit_card._person_code) + "\'";
                    this._searchAndWarning(_g.d.cb_credit_card._person_code, __query);
                }
            }
        }       

        bool _searchAndWarning(string fieldName, string query)
        {

            bool __result = false;
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, query);

            if (dataResult.Tables[0].Rows.Count > 0)
            {
                if (fieldName.CompareTo(_g.d.cb_credit_card._ar_code) == 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][0].ToString();
                    string getDataStr = this._creditMasterScreenTop._getDataStr(fieldName);
                    this._creditMasterScreenTop._setDataStr(fieldName, getDataStr);
                    this._creditMasterScreenTop._setDataStr(_g.d.cb_credit_card._ar_name, getData);
                }
                else
                {
                    string getData = dataResult.Tables[0].Rows[0][0].ToString();
                    string getDataStr = this._creditMasterScreenTop._getDataStr(fieldName);
                    this._creditMasterScreenTop._setDataStr(fieldName, getDataStr, getData, true);
                }

            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && this._creditMasterScreenTop._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        //_searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._creditMasterScreenTop._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }
        /// <summary>
        /// Even การทำงาน ของ Grid และ Screen ทั่วไปไม่เกี่ยวกับค้นหา
        /// </summary>
        /// <param name="number"></param> 
        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            //if ((keyData & Keys.Control) == Keys.Control)
            //{
            //    switch (keyCode)
            //    {
            //        case Keys.Home:
            //            {
            //                this._creditMasterScreenTop.Focus();
            //                return true;
            //            }
            //    }
            //}
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
                if (this._searchCustCode.Visible)
                {

                    this._searchCustCode.Focus();
                    this._searchCustCode._firstFocus();
                }
                if (this._searchBookCode.Visible)
                {
                    
                    this._searchBookCode.Focus();
                    this._searchBookCode._firstFocus();
                }
                if (this._searchPassBook.Visible)
                {
                    
                    this._searchPassBook.Focus();
                    this._searchPassBook._firstFocus();
                }
                if (this._searchCurrency.Visible)
                {
                    
                    this._searchCurrency.Focus();
                    this._searchCurrency._firstFocus();
                }
                if (this._searchPersonCode.Visible)
                {
                   
                    this._searchPersonCode.Focus();
                    this._searchPersonCode._firstFocus();
                }                
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// ฟังชั่น ทั่วไป
        /// </summary>
        /// <param name="number">autoRuning=เลขที่เอกสารอัตโนมัต,CalcTotalGrid=คำนวนผลรวมใส่ Screen Bottom</param>        
        //string _autoRunning()
        //{
        //    return MyLib._myGlobal._getAutoRun(_g.d.cb_credit_card._table, _g.d.cb_credit_card._doc_no);
        //}

        /// <summary>
        /// Even การทำงาน ของ MyManageData
        /// </summary>
        /// <param name="number"></param> 
        void _myManageCreditMaster__clearData()
        {
            this._creditMasterScreenTop._clear();            
            this._creditMasterScreenTop._isChange = false;            
        }

        bool _myManageCreditMaster__discardData()
        {
            bool result = true;
            if (this._creditMasterScreenTop._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._creditMasterScreenTop._isChange = false;                    
                }
            }
            return (result);
        }

        void _myManageCreditMaster__newDataClick()
        {
            Control codeControl = this._creditMasterScreenTop._getControl(_g.d.cb_credit_card._credit_card_no);
            codeControl.Enabled = true;            
            this._creditMasterScreenTop._focusFirst();
        }
        /// <summary>
        /// Even การทำงาน Delete,Load,Save
        /// </summary>
        /// <param name="number"></param>
        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), this._myManageCreditMaster._dataList._tableName, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                this._myManageCreditMaster._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool _myManageCreditMaster__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                //string _queryLoadToScreen = "select * from " + _g.d.cb_credit_card._table + whereString +" and "+ _g.d.cb_credit_card._trans_type + "=" + _creditMasterGlobal._creditMasterType(creditMasterControlType).ToString();
                //DataSet getData = myFrameWork._query(MyLib._myGlobal._databaseName, _queryLoadToScreen);
                //this._creditMasterScreenTop._loadData(getData.Tables[0]);
                //Control codeControl = this._creditMasterScreenTop._getControl(_g.d.cb_credit_card._credit_card_no);
                //codeControl.Enabled = false;
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_credit_card._table + whereString + " and " + _g.d.cb_credit_card._trans_type + "=" + _creditMasterGlobal._creditMasterType(creditMasterControlType).ToString()));
                __myquery.Append("</node>");
                ArrayList _getData = myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._creditMasterScreenTop._loadData(((DataSet)_getData[0]).Tables[0]);
                if (forEdit)
                {
                    this._creditMasterScreenTop._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        private void _saveData()
        {
            switch (creditMasterControlType)
            {
                case _creditMasterControlTypeEnum.credit_master_receive:
                    string getEmtry = this._creditMasterScreenTop._checkEmtryField();
                    if (getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, getEmtry);
                    }
                    else
                    {
                        //จับข้อมูลเข้า ArrayList ก่อน
                        ArrayList getData = this._creditMasterScreenTop._createQueryForDatabase();
                        //เตรียมแพค xml
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        if (this._myManageCreditMaster._mode == 1)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" insert into " + _g.d.cb_credit_card._table + " (" + getData[0].ToString() + ") values (" + getData[1].ToString() + ")"));
                        }
                        else
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + this._myManageCreditMaster._dataList._tableName + " set " + getData[2].ToString() + this._myManageCreditMaster._dataList._whereString));
                        }
                        //                
                        __myQuery.Append("</node>");
                        MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                        string result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (result.Length == 0)
                        {
                            MyLib._myGlobal._displayWarning(1, null);
                            this._creditMasterScreenTop._isChange = false;
                            if (this._myManageCreditMaster._mode == 1)
                            {
                                this._myManageCreditMaster._afterInsertData();
                            }
                            else
                            {
                                this._myManageCreditMaster._afterUpdateData();
                            }
                            this._creditMasterScreenTop._clear();
                            this._creditMasterScreenTop._focusFirst();
                            //_autoRunning();

                        }
                        else
                        {
                            MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
            }
            
        }
        /// <summary>
        /// Even การทำงาน From Load
        /// </summary>
        /// <param name="number">ทำงานครั้งแรกที่เปิดหน้าจอ</param>
        void _creditMasterControl_Load(object sender, EventArgs e)
        {
            switch (creditMasterControlType)
            {
                case _creditMasterControlTypeEnum.credit_master_receive:
                    this._myManageCreditMaster._dataList._extraWhere = _g.d.cb_credit_card._trans_type + "=" +_creditMasterGlobal._creditMasterType(creditMasterControlType).ToString();
                    this._myManageCreditMaster._dataList._loadViewFormat(_g.g._search_screen_บัตรเครดิต, MyLib._myGlobal._userSearchScreenGroup, true);                    
                    break;
                case _creditMasterControlTypeEnum.credit_master_payment:
                    this._myManageCreditMaster._dataList._extraWhere = _g.d.cb_credit_card._trans_type + "=" + _creditMasterGlobal._creditMasterType(creditMasterControlType).ToString();
                    this._myManageCreditMaster._dataList._loadViewFormat(_g.g._search_screen_บัตรเครดิต, MyLib._myGlobal._userSearchScreenGroup, true);   
                    break;
            }
            this._myManageCreditMaster._dataList._refreshData();
            this._myManageCreditMaster._dataListOpen = true;
            this._myManageCreditMaster._calcArea();
        }
    }
    
    public class _creditMasterScreenTopControl : MyLib._myScreen
    {
        private _creditMasterControlTypeEnum __creditMasterControlTypeTemp;

        public _creditMasterControlTypeEnum creditMasterControlType
        {
            set
            {
                this.__creditMasterControlTypeTemp = value;
                //this.__build();
                this.Invalidate();
            }
            get
            {
                return this.__creditMasterControlTypeTemp;
            }
        }

        public _creditMasterScreenTopControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            switch (creditMasterControlType)
            {
                case _creditMasterControlTypeEnum.credit_master_receive:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_credit_card._table;
                    //this._addTextBox(0, 0, 1, 0, _g.d.cb_credit_card._credit_card_no, 1, 1, 0, true, false, true);            //เลขที่บัตรเครดิต
                    //this._addTextBox(0, 1, 1, 0, _g.d.cb_credit_card._credit_card_code, 1, 1, 0, true, false, true);          //รหัสท้ายบัตร
                    //this._addComboBox(1, 0, _g.d.cb_credit_card._credit_card_type, true, _g.g._credit_card_type, false);      //ประเภทบัตรเครดิต
                    //this._addNumberBox(1, 1, 1, 0, _g.d.cb_credit_card._amount, 1, 2, true);                            //วงเงินเครดิต                    
                    //this._addTextBox(2, 0, 1, 0, _g.d.cb_credit_card._owner_name, 1, 255, 0, true, false, true, true);        //ชื่อเจ้าของบัตร
                    //this._addDateBox(3, 0, 1, 0, _g.d.cb_credit_card._credit_card_expire, 1, true, true);                     //วันที่บัตรหมดอายุ
                    //this._addDateBox(3, 1, 1, 0, _g.d.cb_credit_card._credit_due_date, 1, true, true);                        //วันที่ครบกำหนด
                    //this._addTextBox(4, 0, 1, 0, _g.d.cb_credit_card._book_code, 1, 1, 1, true, false, true);                 //สมุดรายวัน 
                    //this._addTextBox(4, 1, 1, 0, _g.d.cb_credit_card._pass_book_code, 1, 1, 1, true, false, true);           //สมุดเงินฝากธนาคาร
                    //this._addTextBox(5, 0, 1, 0, _g.d.cb_credit_card._bank_code, 1, 1, 0, true, false, true,false);                 //รหัสธนาคาร
                    //this._addTextBox(5, 1, 1, 0, _g.d.cb_credit_card._bank_branch, 1, 1, 0, true, false, true,false);               //สาขาของธนาคาร
                    //this._addTextBox(6, 0, 1, 0, _g.d.cb_credit_card._currency_code, 1, 1, 1, true, false, true);             //รหัสกุลเงิน
                    //this._addNumberBox(6, 1, 1, 0, _g.d.cb_credit_card._amount, 1, 2, true);                                  //จำนวนเงิน  
                    //this._addTextBox(7, 0, 2, 0, _g.d.cb_credit_card._bank_description, 2, 100, 0, true, false, true);       //คำอธิบาย
                    //this._addTextBox(6, 0, 1, 0, _g.d.cb_credit_card._person_code, 1, 1, 1, true, false, true);              //ผู้รับบัตร
                    //this._addDateBox(6, 1, 1, 0, _g.d.cb_credit_card._credit_get_date, 1, true, true);                       //วันที่รับบัตร
                    //this._addTextBox(7, 0, 1, 0, _g.d.cb_credit_card._ar_code, 1, 1, 1, true, false, true);                   //รหัสลูกค้า
                    //this._addTextBox(7, 1, 1, 0, _g.d.cb_credit_card._ar_name, 1, 255, 0, false, false, true, true);          //ชื่อลูกค้า
                    //this._addTextBox(8, 0, 2, 0, _g.d.cb_credit_card._remark, 2, 100, 0, true, false, true);                 //หมายเหตุ

                    //this._addTextBox(0, 0, 1, 0, _g.d.cb_credit_card._credit_card_no, 1, 1, 0, true, false, true);            //เลขที่บัตรเครดิต
                    //this._addTextBox(0, 1, 1, 0, _g.d.cb_credit_card._credit_card_code, 1, 1, 0, true, false, true);          //รหัสท้ายบัตร
                    //this._addComboBox(1, 0, _g.d.cb_credit_card._credit_card_type, true, _g.g._credit_card_type, false);      //ประเภทบัตรเครดิต
                    //this._addNumberBox(1, 1, 1, 0, _g.d.cb_credit_card._amount, 1, 2, true);                            //จำนวนเงิน    
                    //this._addDateBox(2, 0, 1, 0, _g.d.cb_credit_card._credit_get_date, 1, true, true);                       //วันที่รับบัตร
                    //this._addDateBox(2, 1, 1, 0, _g.d.cb_credit_card._credit_card_expire, 1, true, true);                     //วันที่บัตรหมดอายุ
                    //this._addTextBox(3, 0, 1, 0, _g.d.cb_credit_card._bank_code, 1, 1, 0, true, false, true);                 //รหัสธนาคาร
                    //this._addTextBox(3, 1, 1, 0, _g.d.cb_credit_card._bank_branch, 1, 1, 0, true, false, true);               //สาขาของธนาคาร

                    this._addTextBox(0, 0, 1, 0, _g.d.cb_credit_card._credit_card_no, 1, 1, 0, true, false, true);            //เลขที่บัตรเครดิต
                    this._addTextBox(0, 1, 1, 0, _g.d.cb_credit_card._credit_card_type, 1, 1, 0, true, false, true);           //ประเภทบัตรเครดิต
                    this._addDateBox(1, 0, 1, 0, _g.d.cb_credit_card._date_cut, 1, true, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.cb_credit_card._no_approved, 1, 1, 0, true, false, true); 
                    this._addNumberBox(2, 0, 1, 0, _g.d.cb_credit_card._amount, 1, 2, true);                            //จำนวนเงิน
                    this._addNumberBox(2, 1, 1, 0, _g.d.cb_credit_card._charge, 1, 2, true);                            //ยอด Charge
                    this._addNumberBox(3, 1, 1, 0, _g.d.cb_credit_card._sum_amount, 1, 2, true);                           //จำนวนเงินรวม

                    
                    

                    this._getControl(_g.d.cb_credit_card._credit_card_no).Enabled = false;                    
                    this._getControl(_g.d.cb_credit_card._credit_card_type).Enabled = false;
                    this._getControl(_g.d.cb_credit_card._amount).Enabled = false;
                    this._getControl(_g.d.cb_credit_card._charge).Enabled = false;
                    this._getControl(_g.d.cb_credit_card._sum_amount).Enabled = false;
                    this._getControl(_g.d.cb_credit_card._no_approved).Enabled = false;
                    this._getControl(_g.d.cb_credit_card._date_cut).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._credit_card_code).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._credit_card_expire).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._bank_code).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._bank_branch).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._credit_get_date).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._credit_money).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._owner_name).Enabled = false;                    
                    //this._getControl(_g.d.cb_credit_card._credit_due_date).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._book_code).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._pass_book_code).Enabled = false;                    
                    //this._getControl(_g.d.cb_credit_card._person_code).Enabled = false;                    
                    //this._getControl(_g.d.cb_credit_card._ar_code).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._ar_name).Enabled = false;
                    //this._getControl(_g.d.cb_credit_card._remark).Enabled = false;
                    break;
                case _creditMasterControlTypeEnum.credit_master_payment:
                    this._maxColumn = 2;
                     this._table_name = _g.d.cb_credit_card._table;
                     //this._addTextBox(0, 0, 1, 0, _g.d.cb_credit_card._credit_card_no, 1, 1, 0, true, false, true);            //เลขที่บัตรเครดิต
                     //this._addTextBox(0, 1, 1, 0, _g.d.cb_credit_card._credit_card_code, 1, 1, 0, true, false, true);          //รหัสท้ายบัตร
                     //this._addComboBox(1, 0, _g.d.cb_credit_card._credit_card_type, true, _g.g._credit_card_type, false);      //ประเภทบัตรเครดิต
                     //this._addNumberBox(1, 1, 1, 0, _g.d.cb_credit_card._amount, 1, 2, true);                            //วงเงินเครดิต                    
                     //this._addTextBox(2, 0, 1, 0, _g.d.cb_credit_card._owner_name, 1, 255, 0, true, false, true, true);        //ชื่อเจ้าของบัตร
                     //this._addDateBox(3, 0, 1, 0, _g.d.cb_credit_card._credit_card_expire, 1, true, true);                     //วันที่บัตรหมดอายุ
                     //this._addDateBox(3, 1, 1, 0, _g.d.cb_credit_card._credit_due_date, 1, true, true);                        //วันที่ครบกำหนด
                     //this._addTextBox(4, 0, 1, 0, _g.d.cb_credit_card._book_code, 1, 1, 1, true, false, true);                 //สมุดรายวัน 
                     //this._addTextBox(4, 1, 1, 0, _g.d.cb_credit_card._pass_book_code, 1, 1, 1, true, false, true);           //สมุดเงินฝากธนาคาร
                     //this._addTextBox(5, 0, 1, 0, _g.d.cb_credit_card._bank_code, 1, 1, 0, true, false, true,false);                 //รหัสธนาคาร
                     //this._addTextBox(5, 1, 1, 0, _g.d.cb_credit_card._bank_branch, 1, 1, 0, true, false, true,false);               //สาขาของธนาคาร
                     //this._addTextBox(6, 0, 1, 0, _g.d.cb_credit_card._currency_code, 1, 1, 1, true, false, true);             //รหัสกุลเงิน
                     //this._addNumberBox(6, 1, 1, 0, _g.d.cb_credit_card._amount, 1, 2, true);                                  //จำนวนเงิน  
                     //this._addTextBox(7, 0, 2, 0, _g.d.cb_credit_card._bank_description, 2, 100, 0, true, false, true);       //คำอธิบาย
                     //this._addTextBox(6, 0, 1, 0, _g.d.cb_credit_card._person_code, 1, 1, 1, true, false, true);              //ผู้รับบัตร
                     //this._addDateBox(6, 1, 1, 0, _g.d.cb_credit_card._credit_get_date, 1, true, true);                       //วันที่รับบัตร
                     //this._addTextBox(7, 0, 1, 0, _g.d.cb_credit_card._ar_code, 1, 1, 1, true, false, true);                   //รหัสลูกค้า
                     //this._addTextBox(7, 1, 1, 0, _g.d.cb_credit_card._ar_name, 1, 255, 0, false, false, true, true);          //ชื่อลูกค้า
                     //this._addTextBox(8, 0, 2, 0, _g.d.cb_credit_card._remark, 2, 100, 0, true, false, true);                 //หมายเหตุ

                     //this._addTextBox(0, 0, 1, 0, _g.d.cb_credit_card._credit_card_no, 1, 1, 0, true, false, true);            //เลขที่บัตรเครดิต
                     //this._addTextBox(0, 1, 1, 0, _g.d.cb_credit_card._credit_card_code, 1, 1, 0, true, false, true);          //รหัสท้ายบัตร
                     //this._addComboBox(1, 0, _g.d.cb_credit_card._credit_card_type, true, _g.g._credit_card_type, false);      //ประเภทบัตรเครดิต
                     //this._addNumberBox(1, 1, 1, 0, _g.d.cb_credit_card._amount, 1, 2, true);                            //จำนวนเงิน    
                     //this._addDateBox(2, 0, 1, 0, _g.d.cb_credit_card._credit_get_date, 1, true, true);                       //วันที่รับบัตร
                     //this._addDateBox(2, 1, 1, 0, _g.d.cb_credit_card._credit_card_expire, 1, true, true);                     //วันที่บัตรหมดอายุ
                     //this._addTextBox(3, 0, 1, 0, _g.d.cb_credit_card._bank_code, 1, 1, 0, true, false, true);                 //รหัสธนาคาร
                     //this._addTextBox(3, 1, 1, 0, _g.d.cb_credit_card._bank_branch, 1, 1, 0, true, false, true);               //สาขาของธนาคาร

                     this._addTextBox(0, 0, 1, 0, _g.d.cb_credit_card._credit_card_no, 1, 1, 0, true, false, true);            //เลขที่บัตรเครดิต
                     this._addTextBox(0, 1, 1, 0, _g.d.cb_credit_card._credit_card_type, 1, 1, 0, true, false, true);           //ประเภทบัตรเครดิต
                     this._addDateBox(1, 0, 1, 0, _g.d.cb_credit_card._date_cut, 1, true, true);
                     this._addTextBox(1, 1, 1, 0, _g.d.cb_credit_card._no_approved, 1, 1, 0, true, false, true);
                     this._addNumberBox(2, 0, 1, 0, _g.d.cb_credit_card._amount, 1, 2, true);                            //จำนวนเงิน
                     this._addNumberBox(2, 1, 1, 0, _g.d.cb_credit_card._charge, 1, 2, true);                            //ยอด Charge
                     this._addNumberBox(3, 1, 1, 0, _g.d.cb_credit_card._sum_amount, 1, 2, true);                           //จำนวนเงินรวม




                     this._getControl(_g.d.cb_credit_card._credit_card_no).Enabled = false;
                     this._getControl(_g.d.cb_credit_card._credit_card_type).Enabled = false;
                     this._getControl(_g.d.cb_credit_card._amount).Enabled = false;
                     this._getControl(_g.d.cb_credit_card._charge).Enabled = false;
                     this._getControl(_g.d.cb_credit_card._sum_amount).Enabled = false;
                     this._getControl(_g.d.cb_credit_card._no_approved).Enabled = false;
                     this._getControl(_g.d.cb_credit_card._date_cut).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._credit_card_code).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._credit_card_expire).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._bank_code).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._bank_branch).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._credit_get_date).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._credit_money).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._owner_name).Enabled = false;                    
                     //this._getControl(_g.d.cb_credit_card._credit_due_date).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._book_code).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._pass_book_code).Enabled = false;                    
                     //this._getControl(_g.d.cb_credit_card._person_code).Enabled = false;                    
                     //this._getControl(_g.d.cb_credit_card._ar_code).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._ar_name).Enabled = false;
                     //this._getControl(_g.d.cb_credit_card._remark).Enabled = false;
                    break;
            }
            
            
            this.Invalidate();
            this.ResumeLayout();
        }
    }
    public static class _creditMasterGlobal
    {
        public static int _creditMasterType(_creditMasterControlTypeEnum creditMasterControlType)
        {
            switch (creditMasterControlType)
            {
                case _creditMasterControlTypeEnum.credit_master_receive: return 1;
                case _creditMasterControlTypeEnum.credit_master_payment: return 2;
            }
            return 0;
        }
    }

    public enum _creditMasterControlTypeEnum
    {
        /// <summary>
        /// 1.credit_master_receive : รายละเอียดรับบัตรเครดิต
        /// </summary>
        credit_master_receive,
        /// <summary>
        /// 1.credit_master_payment : รายละเอียดจ่ายบัตรเครดิต
        /// </summary>
        credit_master_payment
    }
}
