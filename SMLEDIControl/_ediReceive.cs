using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Globalization;
using System.Collections;
using System.Json;
using MyLib;

namespace SMLEDIControl
{
    public partial class _ediReceive : UserControl
    {
        string _agentCode = "";
        string __url = "http://bs.brteasy.com:8080/SinghaEDI_TEST";
        int _transFlag = 36;
        int _transType = 2;
        MyLib._searchDataFull _search_data_full_pointer;
        string _searchName = "";
        string _old_filed_name = "";
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        TextBox _searchTextBox;
        WebClient __n = new WebClient();
        List<string> __itemHead = new List<string>();
        ArrayList _searchScreenMasterList = new ArrayList();
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        public string _screen_code = "PU";

        public _ediReceive()
        {
            InitializeComponent();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort("select * from " + _g.d.erp_company_profile._table);

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                this._agentCode = __result.Tables[0].Rows[0][_g.d.erp_company_profile._agent_code].ToString();
            }

            this._docGrid._table_name = _g.d.ic_trans._table;
            this._docGrid._addColumn("select", 11, 10, 10);
            this._docGrid._isEdit = false;
            this._docGrid._addColumn(_g.d.ic_trans._doc_no, 1, 90, 90);
            this._docGrid.WidthByPersent = true;
            this._docGrid._calcPersentWidthToScatter();
            this._docGrid._mouseClick += _docGrid__mouseClick;

    

            this._icTransScreenTopControl1._table_name = _g.d.ic_trans._table;
            this._icTransScreenTopControl1._maxColumn = 5;
            this._icTransScreenTopControl1._addTextBox(1, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, false);
            this._icTransScreenTopControl1._addTextBox(1, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, false);
      

            this._icTransScreenTopControl1._textBoxSearch += new MyLib.TextBoxSearchHandler(_ictransScreenTopControl__textBoxSearch);

            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Hide();

            this.Load += _singhaOnlineOrderImport_Load;
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                int __usedStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status);
                int __docSuccessColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_success);
                int __lastStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);
                if (__usedStatusColumn != -1)
                {
                    // มีการนำไปใช้แล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.LightSeaGreen;
                    }
                }
                if (__docSuccessColumn != -1)
                {
                    // มีการอ้างอิงครบแล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __docSuccessColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.SlateBlue;
                    }
                }
                if (__lastStatusColumn != -1)
                {
                    // เอกสารมีการยกเลิก
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __lastStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.Red;
                    }
                }
            }
            catch
            {
            }
            return (__result);
        }

        private void _singhaOnlineOrderImport_Load(object sender, EventArgs e)
        {
            this._getData();
        }

        private void _docGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this.textShowData.Clear();
       
            if (e._row > -1)
            {
                string __docNo = this._docGrid._cellGet(e._row, _g.d.ic_trans._doc_no).ToString();
                string __fileName = "";
                __fileName = this._docGrid._cellGet(e._row, _g.d.ic_trans._doc_no).ToString().Replace("\"", string.Empty);
                var __jsonStr = __n.DownloadString(__url + "/EDI/order/" + __fileName + "?agentcode=1000806&encoding=UTF-8");
                this.textShowData.Text = __jsonStr.ToString();
            }
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

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchAll(string screenName, int row)
        {
            int __columnNumber = 0;
            string __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._search_data_full_pointer.Visible = false;
                this._icTransScreenTopControl1._setDataStr(this._searchName, __result, "", false);
                // this._search(true);
            }
        }

        bool _searchAndWarning(string fieldName, DataSet __dataResult, Boolean warning)
        {
            bool __result = false;
            string __getData = "";
            string __getDataStr = this._icTransScreenTopControl1._getDataStr(fieldName);
            string __getDataStr1 = this._icTransScreenTopControl1._getDataStr(fieldName);
            this._icTransScreenTopControl1._setDataStr(fieldName, __getDataStr, __getData, true);
            if (__dataResult.Tables[0].Rows.Count > 0)
            {
                __getData = __dataResult.Tables[0].Rows[0][0].ToString();
            }
            this._icTransScreenTopControl1._setDataStr(fieldName, __getDataStr, __getData, true);
            if (this._searchTextBox != null)
            {

                // toe เพิ่ม 20130311
                // 20160810 toe เอาออก && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ 
                if (this._searchName.CompareTo(fieldName) == 0 && (fieldName.Equals(_g.d.ic_trans._cust_code) ||
                    fieldName.Equals(_g.d.ic_trans._wh_from) ||
                    fieldName.Equals(_g.d.ic_trans._location_from) ||
                    fieldName.Equals(_g.d.ic_trans._wh_to) ||
                    fieldName.Equals(_g.d.ic_trans._location_to)) == true && __dataResult.Tables[0].Rows.Count == 0 && warning == true)
                {
                    MessageBox.Show("ไม่พบข้อมูล : " + ((MyLib._myTextBox)this._searchTextBox.Parent)._labelName);
                    this._icTransScreenTopControl1._setDataStr(fieldName, "", "", true);
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

        void _ictransScreenTopControl__textBoxSearch(object sender)
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
                    __searchObject._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                }

                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                if (__getControl.textBox.Text == "")
                {
                    this._search_data_full_pointer._dataList._searchText.TextBox.Text = "";
                }
                //
            }
            //   MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._label._field_name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            _searchScreenMasterList.Clear();
            try
            {
                {
                    string __extraWhere = "";
                    _searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "", this._screen_code);

                    if (this._searchName.Equals(_g.d.ic_trans._wh_from))
                    {
                        _searchScreenMasterList.Clear();
                        _searchScreenMasterList.Add(_g.g._search_master_ic_warehouse);
                        _searchScreenMasterList.Add(_g.d.ic_warehouse._table);
                    }
                    else
                          if (this._searchName.Equals(_g.d.ic_trans._location_from))
                    {
                        _searchScreenMasterList.Clear();
                        _searchScreenMasterList.Add(_g.g._search_master_ic_shelf);
                        _searchScreenMasterList.Add(_g.d.ic_shelf._table);
                        _searchScreenMasterList.Add(_g.d.ic_shelf._whcode + "=\'" + this._icTransScreenTopControl1._getDataStr(_g.d.ic_trans._wh_from) + "\'");
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
                        __extraWhere = (_searchScreenMasterList.Count == 3) ? _searchScreenMasterList[2].ToString() : __extraWhere;


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
                    // }

                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + " : " + __ex.StackTrace.ToString());
            }
        }

        void _getData()
        {
            try
            {
                this._docGrid._clear();
                // get data from restful server
                WebClient __n = new WebClient();

                //var __json = __n.DownloadString(__url + "/EDI/" + "?agentcode=" + this._agentCode);
                var __json = __n.DownloadString(__url + "/EDI/" + "?agentcode=1000806");
                JsonValue __jsonObject = JsonValue.Parse(__json);
                //JsonArray __jsonObject = new JsonArray(__json);
                // do other


                if (__jsonObject.Count > 0)
                {
                    for (int __row = 0; __row < __jsonObject.Count; __row++)
                    {
                        JsonValue __object = (JsonValue)__jsonObject[__row];
                        if (__object.ToString().Equals("\"success\"") == false && __object.ToString().Equals("\"reject\"") == false)
                        {
                            int __rowAdd = this._docGrid._addRow();
                            this._docGrid._cellUpdate(__rowAdd, _g.d.ic_trans._doc_no, __object.ToString().Replace("\"", string.Empty), true);
                            this._docGrid._cellUpdate(__rowAdd, 0, 1, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void _reloadButton_Click(object sender, EventArgs e)
        {
            this._getData();
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            this._process();
        }

        void _process()
        {
            try
            {
                if (MessageBox.Show("ต้องการนำเข้าข้อมูลที่ได้เลือกไว้หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
                    {
                        transdatasap __transdataedi = new transdatasap();
                        transdatadetailsap __transdatadetailsap = new transdatadetailsap();
                        if (this._docGrid._cellGet(__row, 0).ToString().Equals("1"))
                        {
                            //StringBuilder __rejectMessage = new StringBuilder();
                            List<string> __itemList = new List<string>();
                            //List<string> __productUnit = new List<string>();

                            string __fileName = "";
                            try
                            {
                                __fileName = this._docGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString().Replace("\"", string.Empty);
                                var __jsonStr = __n.DownloadString(__url + "/EDI/order/" + __fileName + "?agentcode=1000806");
                                //JsonValue __json = JsonValue.Parse(__jsonStr);
                                
                                MessageBox.Show(__jsonStr.ToString(), "นำเข้าข้อมูลเรียบร้อยแล้ว พบข้อผิดพลาดที่รายการดังต่อไปนี้", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                string __doc = __jsonStr.ToString();
                                string[] line = __doc.Split('\n');
                  
                                foreach (string __text_in_line in line)
                                {
                                   
                                    string _gethead = __text_in_line.Substring(0, 6);
                                    //เช็คหัวแถว
                                    if (_gethead.Equals("HDRINF"))
                                    {
                                         __transdataedi = transdatasap.ParseEDIText(__text_in_line);
                                        __itemList.Add(__transdataedi._queryInsertEdi());
                                    }
                                    else {
                                         __transdatadetailsap = transdatadetailsap.ParseEDIText(__text_in_line);
                                        __itemList.Add(__transdatadetailsap._queryInsertEdiDetail());
                                    }
                                    
                                }
                                Console.WriteLine(__itemList.Count);
                                string line123456 = string.Join("----", __itemList.ToArray());
                                Console.WriteLine(line123456);
                     
                                // __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "นำเข้าข้อมูลเรียบร้อยแล้ว พบข้อผิดพลาดที่รายการดังต่อไปนี้", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //__rejectMessage.AppendLine("Error : " + ex.ToString());
                            }
                            String __queryInsert = MyLib._myGlobal._xmlHeader + "<node>" + __transdataedi._queryInsertEdi() + "</node>";
                            //MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                            //string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryInsert);

                            //if (__result.Length == 0)
                            //{
                            //    // MessageBox.Show("เสร็จ");
                            //}
                            //else
                            //{
                               
                            //    //MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //}

                        }
                    } // end loop
                    MessageBox.Show("Success");
                    this._getData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
            {
                this._docGrid._cellUpdate(__row, 0, 1, true);
            }
            this._docGrid.Invalidate();
        }

        private void _selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
            {
                this._docGrid._cellUpdate(__row, 0, 0, true);
            }
            this._docGrid.Invalidate();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}