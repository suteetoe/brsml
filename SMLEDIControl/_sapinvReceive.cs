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
using Newtonsoft.Json;
using System.Threading;

namespace SMLEDIControl
{

    public partial class _sapinvReceive : UserControl
    {
        string _agentCode = "";
        string __url = "http://bs.brteasy.com:8080/SinghaEDI";
        int _transFlag = 36;
        int _transType = 2;
        JsonValue _dataRowObject = null;
        string _old_filed_name = "";
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        MyLib._searchDataFull _search_data_full_pointer;
        int _search_data_full_buffer_addr = -1;
        string _searchName = "";

        TextBox _searchTextBox;
        ArrayList _searchScreenMasterList = new ArrayList();
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();
        public string _screen_code = "PU";
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _sapinvReceive()
        {
            InitializeComponent();
            this._docGrid._table_name = _g.d.ic_trans._table;
            this._docGrid._addColumn("select", 11, 10, 10);
            this._docGrid._isEdit = false;
            this._docGrid._addColumn("วันที่ใบกำกับภาษี", 1, 90, 30);
            this._docGrid._addColumn("ใบกำกับภาษี", 1, 90, 30);
            this._docGrid._addColumn("ใบแจ้งหนี้", 1, 90, 30);
            this._docGrid._addColumn(_g.d.ic_trans._doc_no, 1, 90, 30, false, true);
            //this._docGrid._addColumn("data", 12, 0, 0, false, true);
            this._docGrid.WidthByPersent = true;
            this._docGrid._calcPersentWidthToScatter();
            this._docGrid._mouseClick += _docGrid__mouseClick;

            this._icTransScreenTopControl1._textBoxSearch += new MyLib.TextBoxSearchHandler(_ictransScreenTopControl__textBoxSearch);


            this._icTransScreenTopControl1._table_name = _g.d.ic_trans._table;
            this._icTransScreenTopControl1._maxColumn = 5;
            this._icTransScreenTopControl1._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 1, true, false, false);
            this._icTransScreenTopControl1._addTextBox(1, 1, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 0, true, false, false);
            this._icTransScreenTopControl1._addTextBox(1, 2, 1, 0, _g.d.ic_trans._location_from, 1, 1, 0, true, false, false);
            this._icTransScreenTopControl1._setDataStr(_g.d.ic_trans._wh_from, _g.g._companyProfile._warehouse_on_the_way.ToString(), "", true);
            this._icTransScreenTopControl1._setDataStr(_g.d.ic_trans._location_from, _g.g._companyProfile._shelf_on_the_way.ToString(), "", true);
            this._icTransScreenTopControl1._getControl(_g.d.ic_trans._wh_from).Enabled = false;
            this._icTransScreenTopControl1._getControl(_g.d.ic_trans._location_from).Enabled = false;
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

        //public void _search(Boolean warning)
        //{
        //    StringBuilder __myquery = new StringBuilder();
        //    string __queryStr = __myquery.ToString();
        //    int __tableCount = 0;
        //    int __tableCountWareHouse = __tableCount;
        //    int __tableCountLocation = __tableCount;
        //    ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
        //    _searchAndWarning(_g.d.ic_trans._doc_group, (DataSet)_getData[0], warning);
        //    _searchAndWarning(_g.d.ic_trans._cust_code, (DataSet)_getData[1], warning);
        //    _searchAndWarning(_g.d.ic_trans._wh_from, (DataSet)_getData[__tableCountWareHouse], warning);
        //    _searchAndWarning(_g.d.ic_trans._location_from, (DataSet)_getData[__tableCountLocation], warning);
        //    //this._checkCreditDay(__custCode, (DataSet)_getData[1]);
        //}

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

        private void _singhaOnlineOrderImport_Load(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort("select * from " + _g.d.erp_company_profile._table);

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                this._agentCode = __result.Tables[0].Rows[0][_g.d.erp_company_profile._agent_code].ToString();

            }
            if (_agentCode != "")
            {
                Thread __threadProcess = new Thread(new ThreadStart(_getData));
                __threadProcess.Name = "SML Thread getData";
                __threadProcess.IsBackground = true;
                __threadProcess.Start();
                //this._getData();
            }
            else
            {
                MessageBox.Show("ไม่พบ Agent Code ที่ใช้ในการดึงข้อมูล กรุณากำหนด Agent Code ในเมนู 1.1.1 ", "Error  Agent Code is missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _docGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // load data to detail
            if (e._row > -1)
            {
                string __docNo = this._docGrid._cellGet(e._row, _g.d.ic_trans._doc_no).ToString();
                //DataRow[] __getRow = this._dataDetail.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");
                //this._detailGrid._loadFromDataTable(this._dataDetail, __getRow);
            }
        }

        void _getData()
        {
            try
            {

                this._docGrid._clear();
                WebClient __n = new WebClient();
                string _data = "{\"P_agentcode\":\"" + _agentCode + "\"}";
                MyLib._restClient __rest = new _restClient("http://ws-dev.boonrawd.co.th/MasterPaymentAgent/api/SMLHeaderBill", HttpVerb.POST, _data);
                __rest._setContentType("application/json");
                __rest._addHeaderRequest(string.Format("APIKey: {0}", "api_sml"));
                __rest._addHeaderRequest(string.Format("APISecret: {0}", "@p1_$m1@p1_$m1"));
                string __result = __rest.MakeRequest("");
                JsonValue __resultObject;
                if (__result.Length > 0)
                {

                    string __where_in = "";
                    JsonValue __jsonObject = JsonValue.Parse(__result);
                    if (__jsonObject.Count > 0)
                    {
                        __resultObject = JsonValue.Parse(__jsonObject["Result"].ToString());
                        for (int __row1 = 0; __row1 < __resultObject.Count; __row1++)
                        {
                            string __Agentcode = __resultObject[__row1]["Agentcode"].ToString().Replace("\"", string.Empty);
                            string __BILLINGDOCNO = __resultObject[__row1]["BILLINGDOCNO"].ToString().Replace("\"", string.Empty);

                            string __check_doc_no = __Agentcode.Substring(3) + "-" + __BILLINGDOCNO;
                            if (__row1 == __resultObject.Count - 1)
                            {
                                __where_in += "'" + __check_doc_no + "'";
                            }
                            else
                            {
                                __where_in += "'" + __check_doc_no + "',";
                            }
                        }

                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string _query = "select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " in (" + __where_in + ")";
                        DataSet __resultcheck = __myFrameWork._queryShort(_query);

                        for (int __row1 = 0; __row1 < __resultObject.Count; __row1++)
                        {
                            string __Agentcode = __resultObject[__row1]["Agentcode"].ToString().Replace("\"", string.Empty);
                            string __BILLINGDOCNO = __resultObject[__row1]["BILLINGDOCNO"].ToString().Replace("\"", string.Empty);
                            string __tax_doc_no = "";
                            string __tax_doc_date = "";
                            if (__resultObject[__row1]["tax_doc_no"] != null)
                            {
                                __tax_doc_no = __resultObject[__row1]["tax_doc_no"].ToString().Replace("\"", string.Empty);
                            }
                            if (__resultObject[__row1]["tax_doc_date"] != null)
                            {
                                __tax_doc_date = __resultObject[__row1]["tax_doc_date"].ToString().Replace("\"", string.Empty);
                            }

                            string __check_detail = __Agentcode.Substring(3) + "-" + __BILLINGDOCNO;
                            Boolean __check = true;
                            if (__resultcheck.Tables.Count > 0)
                            {
                                for (int __i = 0; __i < __resultcheck.Tables[0].Rows.Count; __i++)
                                {
                                    Console.WriteLine("Hello, " + __resultcheck.Tables[0].Rows[__i][_g.d.ic_trans._doc_no].ToString());
                                    if (__resultcheck.Tables[0].Rows[__i][_g.d.ic_trans._doc_no].ToString().Equals(__check_detail))
                                    {
                                        __check = false;
                                    }
                                }
                            }
                            if (__check)
                            {
                                int __rowAdd = this._docGrid._addRow();
                                this._docGrid._cellUpdate(__rowAdd, _g.d.ic_trans._doc_no, __BILLINGDOCNO, true);
                                this._docGrid._cellUpdate(__rowAdd, "วันที่ใบกำกับภาษี", __tax_doc_date, true);
                                this._docGrid._cellUpdate(__rowAdd, "ใบกำกับภาษี", __tax_doc_no, true);
                                this._docGrid._cellUpdate(__rowAdd, "ใบแจ้งหนี้", __BILLINGDOCNO, true);
                                this._docGrid._cellUpdate(__rowAdd, 0, 1, true);
                            }
                        }
                        this._docGrid.Invalidate();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Agent Code :" + _agentCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void _reloadButton_Click(object sender, EventArgs e)
        {
            this._getData();
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            //this._process();
            Thread __threadProcess = new Thread(new ThreadStart(_process));
            __threadProcess.Name = "SML Thread";
            __threadProcess.IsBackground = true;
            __threadProcess.Start();
        }

        void _process()
        {
            try
            {
                if (MessageBox.Show("ต้องการนำเข้าข้อมูลที่ได้เลือกไว้หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    StringBuilder __log = new StringBuilder();


                    for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
                    {
                        if (this._docGrid._cellGet(__row, 0).ToString().Equals("1"))
                        {
                            try
                            {
                                string __BILLINGDOCNO = (string)this._docGrid._cellGet(__row, 4);
                                string _datadetail = "{\"P_billingdocno\":\"" + __BILLINGDOCNO + "\",\"P_agentcode\":\"" + _agentCode + "\"}";
                                //string _datadetail = "{\"P_billingdocno\":\"6002301035\",\"P_agentcode\":\"" + _agentCode + "\"}";

                                //rest 3 
                                MyLib._restClient __restdetail = new _restClient("http://ws-dev.boonrawd.co.th/MasterPaymentAgent/api/SMLBill", HttpVerb.POST, _datadetail);
                                __restdetail._setContentType("application/json");
                                __restdetail._addHeaderRequest(string.Format("APIKey: {0}", "api_sml"));
                                __restdetail._addHeaderRequest(string.Format("APISecret: {0}", "@p1_$m1@p1_$m1"));
                                string __resultdetail = __restdetail.MakeRequest("");
                                JsonValue __jsonDetailObject2 = JsonValue.Parse(__resultdetail);
                                JsonValue __jsonDetailObject22 = JsonValue.Parse(__jsonDetailObject2["Result"][0].ToString());
                                string __check_detail = __jsonDetailObject22["Agentcode"].ToString().Substring(4).Replace("\"", string.Empty) + "-" + __jsonDetailObject22["BILLINGDOCNO"].ToString().Replace("\"", string.Empty);

                                try
                                {
                                    transdatasap __transdatasap = transdatasap.Parse(__jsonDetailObject2["Result"][0].ToString());
                                    __transdatasap.details = new List<transdatadetailsap>();
                                    if (__jsonDetailObject22["details"].Count > 0)
                                    {

                                        for (int i = 0; i < __jsonDetailObject22["details"].Count; i++)
                                        {
                                            transdatadetailsap __transdatadetailsap = transdatadetailsap.Parse(__jsonDetailObject22["details"][i].ToString());
                                            __transdatadetailsap.wh_code = this._icTransScreenTopControl1._getDataStr(_g.d.ic_trans._wh_from).ToString();
                                            __transdatadetailsap.shelf_code = this._icTransScreenTopControl1._getDataStr(_g.d.ic_trans._location_from).ToString();
                                            //  = __transdatadetailsap.BAT_DATE
                                            //DateTime bd = Convert.ToDateTime(__transdatadetailsap.BAT_DATE);
                                            if (__transdatadetailsap.BAT_DATE != null)
                                            {
                                                string date = __transdatadetailsap.BAT_DATE.ToString().Substring(0, 4) + "-" + __transdatadetailsap.BAT_DATE.ToString().Substring(4, 2) + "-" + __transdatadetailsap.BAT_DATE.ToString().Substring(6);
                                                DateTime __convertDate = DateTime.ParseExact(date, "yyyy-MM-dd", null);
                                                __transdatadetailsap.BAT_DATE = MyLib._myGlobal._convertDateToQuery(__convertDate);
                                                __transdatadetailsap.date_expire = MyLib._myGlobal._convertDateToQuery(__convertDate.AddDays(90));
                                            }
                                            __transdatadetailsap.item_code = __transdatadetailsap.MATERIALCODE;
                                            __transdatadetailsap.sum_amount_exclude_vat = MyLib._myGlobal._decimalPhase(__transdatadetailsap.qty) * MyLib._myGlobal._decimalPhase(__transdatadetailsap.price);
                                            __transdatadetailsap.total_vat_value = __transdatadetailsap.sum_amount_exclude_vat * 7 / 100;
                                            __transdatasap.details.Add(__transdatadetailsap);

                                        }
                                    }
                                    __transdatasap.doc_format_code = this._icTransScreenTopControl1._getDataStr(_g.d.ic_trans._doc_format_code).ToString();
                                    __transdatasap.wh_from = this._icTransScreenTopControl1._getDataStr(_g.d.ic_trans._wh_from).ToString();
                                    __transdatasap.location_from = this._icTransScreenTopControl1._getDataStr(_g.d.ic_trans._location_from).ToString();
                                    __transdatasap.inquiry_type = 0;
                                    __transdatasap.vat_type = 0;
                                    __transdatasap.check();


                                    //     __lastResulValue = __transdatasap._getJson();

                                    //string _data = this._docGrid._cellGet(__row, 2).ToString();
                                    //   transdatasap dataTrans = (transdatasap)this._docGrid._cellGet(__row, "data");
                                    //transdatasap dataTrans = (transdatasap)__transdatasap._getJson();
                                    // MessageBox.Show(dataTrans._getJson());
                                    String __queryInsert = MyLib._myGlobal._xmlHeader + "<node>" + __transdatasap._queryInsert() + "</node>";
                                    MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryInsert);

                                    if (__result.Length == 0)
                                    {
                                        // MessageBox.Show("เสร็จ");
                                    }
                                    else
                                    {
                                        __log.Append(__result);
                                        //MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    __log.Append(ex);
                                    __log.Append(__resultdetail);
                                }
                            }
                            catch (Exception ex)
                            {
                                __log.Append(ex);
                            }
                        }
                    } // end loop
                    if (__log.Length > 0)
                    {
                        MessageBox.Show(__log.ToString(), "พบข้อผิดพลาดที่รายการดังต่อไปนี้", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("นำเข้าข้อมูลสำเร็จแล้ว", "นำเข้าข้อมูลเรียบร้อยแล้ว", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
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

        private void _myTextBox1_Load(object sender, EventArgs e)
        {

        }
    }

}

