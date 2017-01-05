using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Data;
using MyLib;
using System.Drawing;

namespace SMLERPCASHBANKReport._condition
{
    public partial class _condition_screen : MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        //////////MyLib._searchDataFull _searchDataFull = new MyLib._searchDataFull();
        //////////string _searchName = "";
        //////////TextBox _searchTextBox;
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        private string _oldCustCode = "";
        string _searchName = "";
        TextBox _searchTextBox;
        string _old_filed_name = "";
        string _old_cust_code = "";
        public string __where_query = "";
        public string __checkPage = "";

        public _condition_screen()
        {
            //this._table_name = _g.d.resource_report._table;
            //this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenCondition__textBoxSearch);
            //this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_screen__textBoxChanged);            
        }

        public void _init(string __page)
        {
            this.SuspendLayout();
            this._reset();
            this._table_name = _g.d.resource_report._table;
            this._maxColumn = 2;
            this.__checkPage = __page;
            if (__page.Equals(_enum_screen_report_cb._cb_creditcard.ToString()) || __page.Equals(_enum_screen_report_cb._cb_creditcardcancel.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_credit_number, 1, 1, 1, true, false, true);
                this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_credit_number, 1, 1, 1, true, false, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);
                
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_craditcardgetmoney.ToString()) || __page.Equals(_enum_screen_report_cb._cb_craditcardgetmoneycancel.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_credit_number, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_credit_number, 1, 1, 1, true, false, true);

                this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashopen.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_petty_cash_code, 1, 1, 1, true, false, true);
                this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_petty_cash_code, 1, 1, 1, true, false, true);
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashreceive.ToString()) || __page.Equals(_enum_screen_report_cb._cb_subcashreceivecancel.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_subcashreturn.ToString()) || __page.Equals(_enum_screen_report_cb._cb_subcashreturncancel.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_subcashwithdraw.ToString()) || __page.Equals(_enum_screen_report_cb._cb_subcashwithdrawcancel.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_subcashmovements.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);

                this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqincancel.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqinchange.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqinchangecancel.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqindeposit.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqindepositcancel.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqinpass.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqinpasscancel.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqinreceive.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqinrenew.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqinrenewcancel.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqinreturn.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqinreturncancel.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqinstatus.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqoutcancel.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqoutchange.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqoutchangecancel.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqoutpass.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqoutpasscancel.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqoutpayment.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqoutreturn.ToString())
                || __page.Equals(_enum_screen_report_cb._cb_chqoutreturncancel.ToString()) || __page.Equals(_enum_screen_report_cb._cb_chqoutstatus.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_check_number, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_check_number, 1, 1, 1, true, false, true);

                this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(_enum_screen_report_cb._chq_receive.ToString()) || __page.Equals(_enum_screen_report_cb._chq_payment.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_check_number, 1, 1, 1, true, false, true);
                this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_check_number, 1, 1, 1, true, false, true);
            }
            else if (__page.Equals(_enum_screen_report_cb._credit_receive.ToString()) || __page.Equals(_enum_screen_report_cb._credit_payment.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_credit_number, 1, 1, 1, true, false, true);
                this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_credit_number, 1, 1, 1, true, false, true);
            }
            
            // jead : เอา iconNumber=1 มาเพิ่ม Event
            foreach (Control __getControlAll in this.Controls)
            {
                if (__getControlAll.GetType() == typeof(MyLib._myTextBox))
                {
                    MyLib._myTextBox __getControlTextBox = (MyLib._myTextBox)__getControlAll;                      
                    if (__getControlTextBox._iconNumber == 1)
                    {
                        // กำหนดให้การค้นหาแบบจอเล็กแสดง
                        __getControlTextBox.textBox.Leave += new EventHandler(textBox_Leave);
                        //__getControlTextBox.textBox.Enter += new EventHandler(textBox_Enter);
                        __getControlTextBox.textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                    else if (__getControlTextBox._iconNumber == 4)
                    {
                        __getControlTextBox.textBox.Leave -= new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.Leave += new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.KeyDown -= new KeyEventHandler(textBox_KeyDown);
                        __getControlTextBox.textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                }               
            }
            
            this.Padding = new Padding(3);
            this._textBoxSearch -= new MyLib.TextBoxSearchHandler(_screenCondition__textBoxSearch);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenCondition__textBoxSearch);
            this._textBoxChanged -= new MyLib.TextBoxChangedHandler(_screenCondition__textBoxChanged);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenCondition__textBoxChanged);            

            this.Dock = DockStyle.Top;
            this.AutoSize = true;

            this.Invalidate();
            this.ResumeLayout();            
            
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible)
                {
                    this._search_data_full_pointer.Focus();
                    this._search_data_full_pointer._firstFocus();
                }
            }
        }

        void _screenCondition__textBoxChanged(object sender, string name)
        {
            // jead
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;

            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                __textBox._textFirst = __textBox._textFirst.ToUpper();
                ////////if (__textBox._name.Equals(_g.d.ic_trans._cust_code))
                ////////{
                ////////    this._oldCustCode = __textBox._textFirst;
                ////////}                      
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        public void _deleteSearchList(MyLib._myTextBox source)
        {
            Boolean __found = false;
            int __addr = 0;
            for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
            {
                if (source._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                {
                    __addr = __loop;
                    __found = true;
                    break;
                }
            }
            if (__found)
            {
                this._search_data_full_buffer_name.RemoveAt(__addr);
                this._search_data_full_buffer.RemoveAt(__addr);
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            // jead
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
            //                       
        }
        

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _screenCondition__textBoxSearch(__getControl);
        }
        void _screenCondition__textBoxSearch(object sender)
        {

            Debug.Print("_screenCondition__textBoxSearch  : ");
            this._saveLastControl();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchName = __getControl._name.ToLower();
            string label_name = __getControl._labelName;
            string _search_text_new = _search_screen_neme(this._searchName);
            

            if (!this._old_filed_name.Equals(__getControl._name.ToLower()))
            {
                Debug.Print("-- _screenCondition__textBoxSearch  : ");

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
            //ค้นหารหัสลูกหนี้
            //ค้นหาหน้าจอ Top               

            if (!this._search_data_full_pointer._name.Equals(_search_text_new.ToLower()))
            {
                // แก้แบบนี้เด้อ
                if (this._search_data_full_pointer._name.Length == 0)
                {
                    Debug.Print("-+- _screenCondition__textBoxSearch  : ");

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
            //ตั้งค่า where ให้ popup
            if (__checkPage.Equals(_enum_screen_report_cb._chq_receive.ToString()))
            {
                this.__where_query = " chq_type = 1 ";
            }
            else if (__checkPage.Equals(_enum_screen_report_cb._chq_payment.ToString()))
            {
                this.__where_query = " chq_type = 2 ";
            }
            else if (__checkPage.Equals(_enum_screen_report_cb._credit_receive.ToString()))
            {
                this.__where_query = " trans_type = 1 ";
            }
            else if (__checkPage.Equals(_enum_screen_report_cb._credit_payment.ToString()))
            {
                this.__where_query = " trans_type = 2 ";
            } 
            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม            
            if (this._search_data_full_pointer._show == false)
            {
                //MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, this.__where_query);
                _startSearchBox(__getControl, label_name, this._search_data_full_pointer, true, true, this.__where_query);

            }
            else
            {
                //MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false);
                _startSearchBox(__getControl, label_name, this._search_data_full_pointer, true);
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

        public static void _startSearchBox(_myTextBox getControl, string label, _searchDataFull screen, bool showDialog)
        {
            _startSearchBox(getControl, label, screen, true, false, "");
        }

        public static void _startSearchBox(_myTextBox getControl, string label, _searchDataFull screen, bool showDialog, bool reloadData, string whereQuery)
        {
            screen._show = true;
            screen.Text = label;
            screen.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            screen.Size = new Size(450, 320);
            int __newLocationX = (0 - screen.Width) + getControl._iconSearch.Width;
            int __newLocationY = getControl._iconSearch.Height;
            Point __newPoint = getControl._iconSearch.PointToScreen(new Point(__newLocationX, __newLocationY));
            screen.DesktopLocation = __newPoint;
            if (screen.DesktopLocation.Y + screen.Height + 50 > Screen.PrimaryScreen.Bounds.Height)
            {
                __newLocationY -= (screen.Height + getControl.Height);
                __newPoint = getControl._iconSearch.PointToScreen(new Point(__newLocationX, __newLocationY));
                screen.DesktopLocation = __newPoint;
            }
            if (__newPoint.X < 5)
            {
                __newPoint.X = 5;
                screen.DesktopLocation = __newPoint;
            }
            screen.StartPosition = FormStartPosition.Manual;
            if (screen._dataList._loadViewDataSuccess == false || reloadData)
            {
                screen._dataList._recalcPosition();
                screen._dataList._loadViewData(0, whereQuery);
            }
            if (showDialog)
            {
                screen._showMode = 0;
                screen.Opacity = 100;
                screen.ShowDialog();
            }
            else
            {
                screen._showMode = 1;
                screen.Opacity = 80;                            
                if (screen.Visible == false)
                {
                    screen.Show(_myGlobal._mainForm);
                }
                
            }
            //if (screen._dataList._loadViewDataSuccess == false || reloadData)
            //{
            //    screen._dataList._recalcPosition();
            //    screen._dataList._loadViewData(0, whereQuery);
            //}
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
                //if (name.Equals(_g.g._screen_ap_trans) ||
                //    name.Equals(_g.g._screen_ar_trans))
                //{
                //    __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 1);
                //}
                //else
                //{
                    __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                //}
                if (__result.Length != 0)
                {
                    this._search_data_full_pointer.Visible = false;
                    this._setDataStr(_searchName, __result);
                    this._search(true);
                }
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                //StringBuilder __myquery = new StringBuilder();
                //__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._doc_group).ToUpper() + "\'"));
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_side_list._name_1 + " from " + _g.d.erp_side_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._side_code).ToUpper() + "\'"));
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._department_code).ToUpper() + "\'"));
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_project_list._name_1 + " from " + _g.d.erp_project_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._project_code).ToUpper() + "\'"));
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_allocate_list._name_1 + " from " + _g.d.erp_allocate_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._allocate_code).ToUpper() + "\'"));
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._sale_code).ToUpper() + "\'"));
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_job_list._name_1 + " from " + _g.d.erp_job_list._table + " where code=\'" + this._getDataStr(_g.d.ic_trans._job_code).ToUpper() + "\'"));
                
                //__myquery.Append("</node>");
                //ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                //_searchAndWarning(_g.d.ic_trans._doc_group, (DataSet)_getData[0], warning);
                //_searchAndWarning(_g.d.ic_trans._side_code, (DataSet)_getData[1], warning);
                //_searchAndWarning(_g.d.ic_trans._department_code, (DataSet)_getData[2], warning);
                //_searchAndWarning(_g.d.ic_trans._project_code, (DataSet)_getData[3], warning);
                //_searchAndWarning(_g.d.ic_trans._allocate_code, (DataSet)_getData[4], warning);
                //_searchAndWarning(_g.d.ic_trans._sale_code, (DataSet)_getData[5], warning);
                //_searchAndWarning(_g.d.ic_trans._job_code, (DataSet)_getData[6], warning);
                //_searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[7], warning);                
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
                //if (fieldName.Equals(_g.d.ic_trans._cust_code))
                //{
                //    Debug.Print("searchAndWarning  : ");
                //    if (!this._old_cust_code.Equals(__getDataStr))
                //    {
                //        this._old_cust_code = __getDataStr;
                //    }
                //}
                //else if (fieldName.Equals(_g.d.ic_trans._verb_no))
                //{
                //    this._loadData(((DataSet)dataResult).Tables[0]);
                //}
                //else
                //{
                    this._setDataStr(fieldName, __getDataStr, __getData, true);
                //}
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
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        string _search_screen_neme(string _name)
        {            
            if (_name.Equals(_g.d.resource_report._from_check_number)) return _g.g._search_screen_cb_เช็ครับ;
            if (_name.Equals(_g.d.resource_report._to_check_number)) return _g.g._search_screen_cb_เช็ครับ;
            if (_name.Equals(_g.d.resource_report._from_credit_number)) return _g.g._search_screen_บัตรเครดิต;
            if (_name.Equals(_g.d.resource_report._to_credit_number)) return _g.g._search_screen_บัตรเครดิต;
            //if (_name.Equals(_g.d.ic_trans._doc_group)) return _g.g._search_screen_erp_doc_group;
            //if (_name.Equals(_g.d.ic_trans._cust_code)) return _geteSarchViewName();
            //if (_name.Equals(_g.d.ic_trans._project_code)) return _g.g._search_master_erp_project_list;
            //if (_name.Equals(_g.d.ic_trans._allocate_code)) return _g.g._search_master_erp_allocate_list;
            //if (_name.Equals(_g.d.ic_trans._job_code)) return _g.g._search_master_erp_job_list;
            //if (_name.Equals(_g.d.ic_trans._side_code)) return _g.g._search_screen_erp_side_list;
            //if (_name.Equals(_g.d.ic_trans._department_code)) return _g.g._search_screen_erp_department_list;
            //if (_name.Equals(_g.d.ic_trans._sale_area_code)) return _g.g._search_master_ar_area_code;
            //if (_name.Equals(_g.d.ic_trans._sale_code)) return _g.g._search_screen_erp_user;
            //if (_name.Equals(_g.d.ic_trans._verb_no)) return _g.g._screen_po_so_deposit;
            return "";
        }

        //private string _geteSarchViewName()
        //{
        //    if (_PoSoDepositGlobalType._PoSoDepositType(this.__PoSoDepositControlTypeTemp).Equals(1))
        //    {
        //        return _g.g._search_screen_ap;
        //    }
        //    else
        //    {
        //        return _g.g._search_screen_ar;
        //    }
        //}

        
    }
    

    public enum _enum_screen_report_cb
    {
        /// <summary>
        /// รายงานสถานะเช็ครับ
        /// </summary>
        _cb_chqinstatus,
        /// <summary>
        /// รายงานเช็ครับ
        /// </summary>
        _cb_chqinreceive,
        /// <summary>
        /// รายงานนำฝากเช็ครับ
        /// </summary>
        _cb_chqindeposit,
        /// <summary>
        /// รายงานเช็ครับผ่าน
        /// </summary>
        _cb_chqinpass,
        /// <summary>
        /// รายงานเช็ครับคืน
        /// </summary>
        _cb_chqinreturn,
        /// <summary>
        /// รายงานเปลี่ยนเช็คนำฝาก
        /// </summary>
        _cb_chqinchange,
        /// <summary>
        /// รายงานนำเช็คเข้าใหม่
        /// </summary>
        _cb_chqinrenew,
        /// <summary>
        /// รายงานสถานะเช็คจ่าย
        /// </summary>
        _cb_chqoutstatus,
        /// <summary>
        /// รายงานเช็คจ่าย
        /// </summary>
        _cb_chqoutpayment,
        /// <summary>
        /// รายงานเช็คจ่ายผ่าน
        /// </summary>
        _cb_chqoutpass,
        /// <summary>
        /// รายงานเช็คจ่ายคืน
        /// </summary>
        _cb_chqoutreturn,
        /// <summary>
        /// รายงานเปลี่ยนเช็คจ่าย
        /// </summary>
        _cb_chqoutchange,
        /// <summary>
        /// รายงานยกเลิกเช็ครับ
        /// </summary>
        _cb_chqincancel,
        /// <summary>
        /// รายงานยกเลิกนำฝากเช็ครับ
        /// </summary>
        _cb_chqindepositcancel,
        /// <summary>
        /// รายงานยกเลิกเช็ครับผ่าน
        /// </summary>
        _cb_chqinpasscancel,
        /// <summary>
        /// รายงานยกเลิกเช็ครับคืน
        /// </summary>
        _cb_chqinreturncancel,
        /// <summary>
        /// รายงานยกเลิกเปลี่ยนเช็คนำฝาก
        /// </summary>
        _cb_chqinchangecancel,
        /// <summary>
        /// รายงานยกเลิกนำเช็คเข้าใหม่
        /// </summary>
        _cb_chqinrenewcancel,
        /// <summary>
        /// รายงานยกเลิกเช็คจ่าย
        /// </summary>
        _cb_chqoutcancel,
        /// <summary>
        /// รายงานยกเลิกเช็คจ่ายผ่าน
        /// </summary>
        _cb_chqoutpasscancel,
        /// <summary>
        /// รายงานยกเลิกเช็คจ่ายคืน
        /// </summary>
        _cb_chqoutreturncancel,
        /// <summary>
        /// รายงานยกเลิกเปลี่ยนเช็คจ่าย
        /// </summary>
        _cb_chqoutchangecancel,
        /// <summary>
        /// รายงานรายการบัตรเครดิต
        /// </summary>
        _cb_creditcard,
        /// <summary>
        /// รายงานยกเลิกรายการบัตรเครดิต
        /// </summary>
        _cb_creditcardcancel,
        /// <summary>
        /// รายงานขึ้นเงินบัตรเครดิต
        /// </summary>
        _cb_craditcardgetmoney,
        /// <summary>
        /// รายงานยกเลิกขึ้นเงินบัตรเครดิต
        /// </summary>
        _cb_craditcardgetmoneycancel,
        /// <summary>
        /// รายงานกำหนดวงเงินสดย่อย
        /// </summary>
        _cb_subcashopen,
        /// <summary>
        /// รายงานรับเงินสดย่อย
        /// </summary>
        _cb_subcashreceive,
        /// <summary>
        /// รายงานยกเลิกรับเงินสดย่อย
        /// </summary>
        _cb_subcashreceivecancel,
        /// <summary>
        /// รายงานขอเบิกเงินสดย่อย
        /// </summary>
        _cb_subcashwithdraw,
        /// <summary>
        /// รายงานยกเลิกขอเบิกเงินสดย่อย
        /// </summary>
        _cb_subcashwithdrawcancel,
        /// <summary>
        /// รายงานรับคือเงินสดย่อย
        /// </summary>
        _cb_subcashreturn,
        /// <summary>
        /// รายงานยกเลิกรับคือเงินสดย่อย
        /// </summary>
        _cb_subcashreturncancel,
        /// <summary>
        /// รายงานเคลื่อนไหวเงินสดย่อย
        /// </summary>
        _cb_subcashmovements,
        /// <summary>
        /// รายงานทะเบียนเช็ครับ
        /// </summary>
        _chq_receive,
         /// <summary>
        /// รายงานทะเบียนเช็คจ่าย
        /// </summary>
        _chq_payment,
         /// <summary>
        /// รายงานรายการรับบัตรเครดิต
        /// </summary>
        _credit_receive,
        /// <summary>
        /// รายงานรายการจ่ายบัตรเครดิต
        /// </summary>
        _credit_payment

    }
}
