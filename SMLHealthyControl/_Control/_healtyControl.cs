using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLHealthyControl._Control
{
    public partial class _healtyControl : UserControl
    {
        public _healtyControl()
        {
            InitializeComponent();
        }
    }
    public class _healthy_yourhealthy : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        _searchProperties _searchScreenProperties = new _searchProperties();
        string _old_filed_name = "";
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        MyLib._searchDataFull _search_data_full_pointer;
        int _search_data_full_buffer_addr = -1;
        public _healthy_yourhealthy()
        {
            this._build();

        }
        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.m_yourhealthy._table;
            this._addTextBox(0, 0, 0, 0, _g.d.m_yourhealthy._ar_code, 1, 0, 1, true, false, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.m_yourhealthy._member_id, 2, 100, 0, true, false, true, true);
            this._addDateBox(2, 0, 0, 0, _g.d.m_yourhealthy._measure_date, 1, true, false);
            this._addNumberBox(3, 0, 0, 0, _g.d.m_yourhealthy._weight, 1, 1, true);
            this._addNumberBox(4, 0, 0, 0, _g.d.m_yourhealthy._high, 1, 1, true);
            this._addNumberBox(5, 0, 0, 0, _g.d.m_yourhealthy._pressure, 1, 1, true);
            this._addNumberBox(6, 0, 0, 0, _g.d.m_yourhealthy._body_fat, 1, 1, true);
            this._addNumberBox(7, 0, 0, 0, _g.d.m_yourhealthy._ldl, 1, 1, true);
            this._addNumberBox(8, 0, 0, 0, _g.d.m_yourhealthy._hdl, 1, 1, true);
            this._addNumberBox(9, 0, 0, 0, _g.d.m_yourhealthy._triglyceride, 1, 1, true);
            this._addNumberBox(10, 0, 0, 0, _g.d.m_yourhealthy._cholesterol, 1, 1, true);
            this._addNumberBox(11, 0, 0, 0, _g.d.m_yourhealthy._kkos, 1, 1, true);
            this._addNumberBox(12, 0, 0, 0, _g.d.m_yourhealthy._blood_Oxygen, 1, 1, true);
            this._addNumberBox(13, 0, 0, 0, _g.d.m_yourhealthy._bmi, 1, 1, true);
            this._addNumberBox(14, 0, 0, 0, _g.d.m_yourhealthy._blood_sugar, 1, 1, true);
            this._addNumberBox(15, 0, 0, 0, _g.d.m_yourhealthy._bun, 1, 1, true);
            this._addNumberBox(16, 0, 0, 0, _g.d.m_yourhealthy._creatinine, 1, 1, true);
            this._addTextBox(17, 0, 2, 0, _g.d.m_yourhealthy._remark, 1, 0, 0, true, false, true, true);

            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_healthy_yourhealthy__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(healthy_yourhealthy__textBoxChanged);
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
            Control codeControl = this._getControl(_g.d.m_yourhealthy._member_id);
            codeControl.Enabled = false;
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
            _healthy_yourhealthy__textBoxSearch(__getControl);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }
        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.m_yourhealthy._ar_code)) return _g.g._search_screen_ar;
            return "";
        }
        string _search_screen_name_extra_where(string _name)
        {
            // if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }
        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }
        void healthy_yourhealthy__textBoxChanged(object sender, string name)
        {

            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                //__textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }
        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                if (fieldName.Equals(_g.d.m_allergic._member_id))
                {
                    this._setDataStr(fieldName, __getData, "", false);
                }
                else
                {
                    this._setDataStr(fieldName, __getDataStr, __getData, true);
                }

            }
            else
            {
                this._setDataStr(fieldName, "", "", true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        string __message = "";
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr(_g.d.m_yourhealthy._ar_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dealer._code + " from " + _g.d.ar_dealer._table + " where " + _g.d.ar_dealer._ar_code + "=\'" + this._getDataStr(_g.d.m_yourhealthy._ar_code) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.m_yourhealthy._ar_code, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.m_yourhealthy._member_id, (DataSet)_getData[1], warning) == false) { }

            }
            catch
            {
            }
        }
        void _healthy_yourhealthy__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _search_screen_name(this._searchName);
            string __where_query = _search_screen_name_extra_where(this._searchName);
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
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                }
            }

            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
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

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }
        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
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
                    this._setDataStr(_searchName, __result);
                }
            }
            this._search(true);
            SendKeys.Send("{TAB}");
        }
    }
    public class _healthy_consultation : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        _searchProperties _searchScreenProperties = new _searchProperties();
        string _old_filed_name = "";
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        MyLib._searchDataFull _search_data_full_pointer;
        int _search_data_full_buffer_addr = -1;
        public _healthy_consultation()
        {
            this._build();

        }
        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.m_consultation._table;
            this._addTextBox(0, 0, 0, 0, _g.d.m_consultation._ic_code, 1, 0, 1, true, false, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.m_consultation._member_id, 1, 1, 0, true, false, true, true);
            this._addTextBox(2, 0, 0, 0, _g.d.m_consultation._usercreator, 1, 1, 1, true, false, true, true);
            this._addDateBox(3, 0, 0, 0, _g.d.m_consultation._record_date, 1, true, false);
            this._addTextBox(4, 0, 0, 0, _g.d.m_consultation._doctor, 1, 1, 1, true, false, true, true);
            this._addTextBox(5, 0, 2, 0, _g.d.m_consultation._consultation_detail, 1, 0, 0, true, false, true, true);
            this._addCheckBox(7, 0, _g.d.m_consultation._status, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_healthy_consultation__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(healthy_consultation__textBoxChanged);
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

            Control codeControl = this._getControl(_g.d.m_consultation._usercreator);
            Control codeControl_member_id = this._getControl(_g.d.m_consultation._member_id);
            codeControl_member_id.Enabled = false;
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
            _healthy_consultation__textBoxSearch(__getControl);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }
        void healthy_consultation__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                //__textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }

            //if (name.Equals(_g.d.m_consultation._ic_code))
            //{
            //    this._searchTextBox = (TextBox)sender;
            //    this._searchName = name;
            //    this._search(true);
            //}
        }
        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                if (fieldName.Equals(_g.d.m_consultation._member_id))
                {
                    this._setDataStr(fieldName, __getData, "", false);
                }
                else
                {
                    this._setDataStr(fieldName, __getDataStr, __getData, true);
                }

            }
            else
            {
                this._setDataStr(fieldName, "", "", true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        string __message = "";
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr(_g.d.m_consultation._ic_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dealer._code + " from " + _g.d.ar_dealer._table + " where " + _g.d.ar_dealer._ar_code + "=\'" + this._getDataStr(_g.d.m_consultation._ic_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where upper(" + _g.d.erp_user._code + ")=\'" + this._getDataStr(_g.d.m_consultation._usercreator) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where upper(" + _g.d.erp_user._code + ")=\'" + this._getDataStr(_g.d.m_consultation._doctor) + "\'"));
             


                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.m_consultation._ic_code, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.m_consultation._member_id, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.m_consultation._usercreator, (DataSet)_getData[2], warning) == false) { }
                if (_searchAndWarning(_g.d.m_consultation._doctor, (DataSet)_getData[3], warning) == false) { }

            }
            catch
            {
            }
        }
        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.m_consultation._ic_code)) return _g.g._search_screen_ar;
            if (_name.Equals(_g.d.m_consultation._doctor)) return _g.g._search_screen_erp_user;
            if (_name.Equals(_g.d.m_consultation._usercreator)) return _g.g._search_screen_erp_user;
            return "";
        }
        string _search_screen_name_extra_where(string _name)
        {
            // if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }
        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }
        void _healthy_consultation__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _search_screen_name(this._searchName);
            string __where_query = _search_screen_name_extra_where(this._searchName);
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
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                }
            }

            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
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

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }
        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
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
                    this._setDataStr(_searchName, __result);
                }
            }
            this._search(true);
            SendKeys.Send("{TAB}");
        }


    }
    public class _healthy_congenital_disease : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        _searchProperties _searchScreenProperties = new _searchProperties();
        string _old_filed_name = "";
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        MyLib._searchDataFull _search_data_full_pointer;
        int _search_data_full_buffer_addr = -1;
        public _healthy_congenital_disease()
        {
            this._build();

        }
        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.m_congenital_disease._table;
            this._addTextBox(0, 0, 0, 0, _g.d.m_congenital_disease._ar_code, 1, 0, 1, true, false, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.m_congenital_disease._member_id, 1, 0, 0, true, false, false, true);
            this._addTextBox(2, 0, 0, 0, _g.d.m_congenital_disease._disease, 1, 0, 1, true, false, false, true);
            this._addTextBox(3, 0, 0, 0, _g.d.m_congenital_disease._symptom, 1, 0, 0, true, false, true, true);
            this._addTextBox(4, 0, 2, 0, _g.d.m_congenital_disease._remark, 1, 0, 0, true, false, true, true);
            this._addCheckBox(6, 0, _g.d.m_congenital_disease._status, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_healthy_congenital_disease__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(healthy_congenital_disease__textBoxChanged);
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
            Control codeControl_member_id = this._getControl(_g.d.m_congenital_disease._member_id);
            codeControl_member_id.Enabled = false;
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
            _healthy_congenital_disease__textBoxSearch(__getControl);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }
        void healthy_congenital_disease__textBoxChanged(object sender, string name)
        {

            //if (name.Equals(_g.d.m_congenital_disease._ar_code) || name.Equals(_g.d.m_congenital_disease._disease))
            //{
            //    this._searchTextBox = (TextBox)sender;
            //    this._searchName = name;
            //    this._search(true);
            //}
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                __textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }
        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                if (fieldName.Equals(_g.d.m_congenital_disease._member_id))
                {
                    this._setDataStr(fieldName, __getData, "", false);
                }
                else
                {
                    this._setDataStr(fieldName, __getDataStr, __getData, true);
                }

            }
            else
            {
                this._setDataStr(fieldName, "", "", true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        string __message = "";
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr(_g.d.m_congenital_disease._ar_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_disease._name_1 + " from " + _g.d.m_disease._table + " where " + _g.d.m_disease._code + "=\'" + this._getDataStr(_g.d.m_congenital_disease._disease) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dealer._code + " from " + _g.d.ar_dealer._table + " where " + _g.d.ar_dealer._ar_code + "=\'" + this._getDataStr(_g.d.m_congenital_disease._ar_code) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.m_congenital_disease._ar_code, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.m_congenital_disease._disease, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.m_congenital_disease._member_id, (DataSet)_getData[2], warning) == false) { }

            }
            catch
            {
            }
        }
        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }
        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.m_congenital_disease._ar_code)) return _g.g._search_screen_ar;
            if (_name.Equals(_g.d.m_congenital_disease._disease)) return _g.g._search_screen_healthy_disease;
            return "";
        }
        string _search_screen_name_extra_where(string _name)
        {
            // if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }
        void _healthy_congenital_disease__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _search_screen_name(this._searchName);
            string __where_query = _search_screen_name_extra_where(this._searchName);
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
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                }
            }
            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
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

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }
        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
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
                    this._setDataStr(_searchName, __result);
                }
            }
            this._search(true);
            SendKeys.Send("{TAB}");
        }

    }
    public class _healthy_drugsConsultants : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        _searchProperties _searchScreenProperties = new _searchProperties();
        string _old_filed_name = "";
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        MyLib._searchDataFull _search_data_full_pointer;
        int _search_data_full_buffer_addr = -1;
        public _healthy_drugsConsultants()
        {
            this._build();

        }
        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.m_drugsconsultants._table;
            this._addTextBox(0, 0, 0, 0, _g.d.m_drugsconsultants._ar_code, 1, 0, 1, true, false, false, true);
            this._addTextBox(1, 0, 1, 0, _g.d.m_drugsconsultants._member_id, 1, 0, 0, true, false, true, true);
            this._addDateBox(2, 0, 0, 0, _g.d.m_drugsconsultants._date, 1, true, false);
            this._addTextBox(3, 0, 0, 0, _g.d.m_drugsconsultants._staff_id, 1, 0, 1, true, false, true, true);
            this._addTextBox(4, 0, 0, 0, _g.d.m_drugsconsultants._branch_code, 1, 0, 1, true, false, true, true);
          //  this._addTextBox(5, 0, 3, 0, _g.d.m_drugsconsultants._remark, 1, 0, 0, true, false, true, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_healthy_m_drugsconsultants__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(healthy_m_drugsconsultants__textBoxChanged);
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
            Control codeControl_member_id = this._getControl(_g.d.m_drugsconsultants._member_id);
            codeControl_member_id.Enabled = false;
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
            _healthy_m_drugsconsultants__textBoxSearch(__getControl);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }
        void healthy_m_drugsconsultants__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
               // __textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }

            //if (name.Equals(_g.d.m_drugsconsultants._ar_code) ||
            //    name.Equals(_g.d.m_drugsconsultants._staff_id) ||
            //    name.Equals(_g.d.m_drugsconsultants._branch_code)
            //    )
            //{
            //    this._searchTextBox = (TextBox)sender;
            //    this._searchName = name;
            //    this._search(true);
            //}
        }
        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                if (fieldName.Equals(_g.d.m_allergic._member_id))
                {
                    this._setDataStr(fieldName, __getData, "", false);
                }
                else
                {
                    this._setDataStr(fieldName, __getDataStr, __getData, true);
                }

            }
            else
            {
                this._setDataStr(fieldName, "", "", true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        string __message = "";
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.m_drugsconsultants._ar_code)) return _g.g._search_screen_ar;
            if (_name.Equals(_g.d.m_drugsconsultants._staff_id)) return _g.g._search_screen_erp_user;
            if (_name.Equals(_g.d.m_drugsconsultants._branch_code)) return _g.g._search_master_erp_branch_list;

            return "";
        }
        string _search_screen_name_extra_where(string _name)
        {
            // if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr(_g.d.m_drugsconsultants._ar_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._getDataStr(_g.d.m_drugsconsultants._staff_id) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_branch_list._name_1 + " from " + _g.d.erp_branch_list._table + " where upper(" + _g.d.erp_branch_list._code + ")=\'" + this._getDataStr(_g.d.m_drugsconsultants._branch_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dealer._code + " from " + _g.d.ar_dealer._table + " where " + _g.d.ar_dealer._ar_code + "=\'" + this._getDataStr(_g.d.m_drugsconsultants._ar_code) + "\'"));
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_warning._name_1 + " from " + _g.d.m_warning._table + " where " + _g.d.m_warning._icode + "=\'" + this._getDataStr(_g.d.m_drugsconsultants._warning) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.m_drugsconsultants._ar_code, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.m_drugsconsultants._staff_id, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.m_drugsconsultants._branch_code, (DataSet)_getData[2], warning) == false) { }
                if (_searchAndWarning(_g.d.m_drugsconsultants._member_id, (DataSet)_getData[3], warning) == false) { }
            }
            catch
            {
            }
        }

        void _healthy_m_drugsconsultants__textBoxSearch(object sender)
        {
            this._saveLastControl();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _search_screen_name(this._searchName);
            string __where_query = _search_screen_name_extra_where(this._searchName);
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
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                }
            }
            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
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

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
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
                    this._setDataStr(_searchName, __result);
                }
            }
           this._search(true);
            if (!_searchName.Equals(_g.d.m_drugsconsultants._branch_code)) SendKeys.Send("{TAB}");
           // 
        }

    }
    public class _healthy_allergic : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        _searchProperties _searchScreenProperties = new _searchProperties();
        string _old_filed_name = "";
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        MyLib._searchDataFull _search_data_full_pointer;
        int _search_data_full_buffer_addr = -1;
        public _healthy_allergic()
        {
            this._build();

        }
        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.m_allergic._table;
            this._addTextBox(0, 0, 0, 0, _g.d.m_allergic._ar_code, 1, 0, 1, true, false, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.m_allergic._member_id, 1, 0, 0, true, false, true, true);
            this._addTextBox(2, 0, 0, 0, _g.d.m_allergic._ic_group_main, 1, 0, 1, true, false, false, true);
            this._addTextBox(3, 0, 0, 0, _g.d.m_allergic._symptom, 1, 0, 0, true, false, true, true);
            this._addTextBox(4, 0, 2, 0, _g.d.m_allergic._remark, 1, 0, 0, true, false, true, true);
            this._addCheckBox(6, 0, _g.d.m_allergic._status, true, false, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(__healthy_allergic__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_healthy_allergic__textBoxChanged);
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
            Control codeControl_member_id = this._getControl(_g.d.m_allergic._member_id);
            codeControl_member_id.Enabled = false;
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
            __healthy_allergic__textBoxSearch(__getControl);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }
        void _healthy_allergic__textBoxChanged(object sender, string name)
        {

            //if (name.Equals(_g.d.m_allergic._ar_code) || name.Equals(_g.d.m_allergic._ic_group_main))
            //{
            //    this._searchTextBox = (TextBox)sender;
            //    this._searchName = name;
            //    this._search(true);
            //}
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                //__textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }
        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                if (fieldName.Equals(_g.d.m_allergic._member_id))
                {
                    this._setDataStr(fieldName, __getData, "", false);
                }
                else
                {
                    this._setDataStr(fieldName, __getDataStr, __getData, true);
                }

            }
            else
            {
                this._setDataStr(fieldName, "", "", true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        string __message = "";
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + this._getDataStr(_g.d.m_allergic._ar_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_group._name_1 + " from " + _g.d.ic_group._table + " where " + _g.d.ic_group._code + "=\'" + this._getDataStr(_g.d.m_allergic._ic_group_main) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_dealer._code + " from " + _g.d.ar_dealer._table + " where " + _g.d.ar_dealer._ar_code + "=\'" + this._getDataStr(_g.d.m_allergic._ar_code) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.m_allergic._ar_code, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.m_allergic._ic_group_main, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.m_allergic._member_id, (DataSet)_getData[2], warning) == false) { }

            }
            catch
            {
            }
        }
        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.m_allergic._ar_code)) return _g.g._search_screen_ar;
            if (_name.Equals(_g.d.m_allergic._ic_group_main)) return _g.g._search_master_ic_group;
            return "";
        }
        string _search_screen_name_extra_where(string _name)
        {
            // if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }
        void __healthy_allergic__textBoxSearch(object sender)
        {

            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _search_screen_name(this._searchName);
            string __where_query = _search_screen_name_extra_where(this._searchName);
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
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                }
            }
            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
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

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
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
                    this._setDataStr(_searchName, __result);
                }
            }
            this._search(true);
            SendKeys.Send("{TAB}");
        }

    }
    public class _healthy_information : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        _searchProperties _searchScreenProperties = new _searchProperties();
        string _old_filed_name = "";
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        MyLib._searchDataFull _search_data_full_pointer;
        int _search_data_full_buffer_addr = -1;
        public _healthy_information()
        {
            this._build();

        }
        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this.AutoSize = true;
            this._maxColumn = 1;
            this._table_name = _g.d.m_information._table;
            this._addTextBox(0, 0, 0, 0, _g.d.m_information._ic_code, 1, 0, 1, true, false, false, true);
            this._addTextBox(1, 0, 0, 0, _g.d.m_information._common_name, 1, 0, 0, true, false, true, true);
            this._addTextBox(2, 0, 0, 0, _g.d.m_information._mim_group, 1, 0,1, true, false, true, true);
            this._addTextBox(3, 0, 0, 0, _g.d.m_information._properties, 1, 0, 1, true, false, true, true);
            this._addTextBox(4, 0, 0, 0, _g.d.m_information._dozen, 1, 0, 1, true, false, true, true);
            this._addTextBox(5, 0, 0, 0, _g.d.m_information._symptom, 1, 0, 0, true, false, true, true, true);
            this._addTextBox(6, 0, 0, 0, _g.d.m_information._warning, 1, 0, 1, true, false, true, true);
            this._addTextBox(7, 0, 0, 0, _g.d.m_information._times, 1, 0, 1, true, false, true, true);
            //this._addComboBox(7, 0, _g.d.m_information._times, true, new string[] { _g.d.m_information._1, _g.d.m_information._2, _g.d.m_information._3 }, false);
            this._addTextBox(8, 0, 0, 0, _g.d.m_information._frequency, 1, 0, 1, true, false, true, true);
            this._addComboBox(9, 0, _g.d.m_information._keep_methods, true, new string[] { _g.d.m_information._9, _g.d.m_information._10, }, false);


            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_healthy_information__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(healthy_information__textBoxChanged);
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
            _healthy_information__textBoxSearch(__getControl);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }
        void healthy_information__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                //__textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }
        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            else
            {
                this._setDataStr(fieldName, "", "", true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        string __message = "";
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_dozen._name_1 + " from " + _g.d.m_dozen._table + " where " + _g.d.m_dozen._icode + "=\'" + this._getDataStr(_g.d.m_information._dozen) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._getDataStr(_g.d.m_information._ic_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_frequency._name_1 + " from " + _g.d.m_frequency._table + " where " + _g.d.m_frequency._code + "=\'" + this._getDataStr(_g.d.m_information._frequency) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_properties._name_1 + " from " + _g.d.m_properties._table + " where " + _g.d.m_properties._code + "=\'" + this._getDataStr(_g.d.m_information._properties) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_times._name_1 + " from " + _g.d.m_times._table + " where " + _g.d.m_times._code + "=\'" + this._getDataStr(_g.d.m_information._times) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_warning._name_1 + " from " + _g.d.m_warning._table + " where " + _g.d.m_warning._icode + "=\'" + this._getDataStr(_g.d.m_information._warning) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_mim_group._name_1 + " from " + _g.d.m_mim_group._table + " where " + _g.d.m_mim_group._code + "=\'" + this._getDataStr(_g.d.m_information._mim_group) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (_searchAndWarning(_g.d.m_information._dozen, (DataSet)_getData[0], warning) == false) { }
                if (_searchAndWarning(_g.d.m_information._ic_code, (DataSet)_getData[1], warning) == false) { }
                if (_searchAndWarning(_g.d.m_information._frequency, (DataSet)_getData[2], warning) == false) { }
                if (_searchAndWarning(_g.d.m_information._properties, (DataSet)_getData[3], warning) == false) { }
                if (_searchAndWarning(_g.d.m_information._times, (DataSet)_getData[4], warning) == false) { }
                if (_searchAndWarning(_g.d.m_information._warning, (DataSet)_getData[5], warning) == false) { }
                if (_searchAndWarning(_g.d.m_information._mim_group, (DataSet)_getData[6], warning) == false) { }

            }
            catch
            {
            }
        }
        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.m_information._mim_group)) return _g.g._search_screen_healthy_m_mim_group;
            if (_name.Equals(_g.d.m_information._dozen)) return _g.g._search_master_ic_group;
            if (_name.Equals(_g.d.m_information._ic_code)) return _g.g._search_screen_ic_inventory;
            if (_name.Equals(_g.d.m_information._frequency)) return _g.g._search_screen_healthy_frequency;
            if (_name.Equals(_g.d.m_information._properties)) return _g.g._search_screen_healthy_properties;
            if (_name.Equals(_g.d.m_information._times)) return _g.g._search_screen_healthy_times;
            if (_name.Equals(_g.d.m_information._warning)) return _g.g._search_screen_healthy_warning;
            return "";
        }
        string _search_screen_name_extra_where(string _name)
        {
            // if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }
        void _healthy_information__textBoxSearch(object sender)
        {           
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _search_screen_name(this._searchName);
            string __where_query = _search_screen_name_extra_where(this._searchName);
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
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                }
            }
            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
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


        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }
        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
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
                    this._setDataStr(_searchName, __result);
                }
            }
            this._search(true);
            SendKeys.Send("{TAB}");
        }
    }
}

