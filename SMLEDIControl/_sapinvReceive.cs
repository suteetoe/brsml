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
            this._docGrid._addColumn(_g.d.ic_trans._doc_no, 1, 90, 90);
            this._docGrid._addColumn("data", 12, 0, 0, false, true);
            this._docGrid.WidthByPersent = true;
            this._docGrid._calcPersentWidthToScatter();
            this._docGrid._mouseClick += _docGrid__mouseClick;

            this._icTransScreenTopControl1._textBoxSearch += new MyLib.TextBoxSearchHandler(_ictransScreenTopControl__textBoxSearch);
           

            this._icTransScreenTopControl1._table_name = _g.d.ic_trans._table;
            this._icTransScreenTopControl1._maxColumn = 5;
            this._icTransScreenTopControl1._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 1, true, false, false);
            this._icTransScreenTopControl1._addTextBox(1, 1, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, true);
            this._icTransScreenTopControl1._addTextBox(1, 2, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, true);

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

                    //if (_searchScreenMasterList.Count > 0 && _searchScreenMasterList[0].ToString().Equals(_g.g._screen_erp_doc_format) && ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite) && _g.g._companyProfile._deposit_format_from_pos == true &&
                    //(this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก)))
                    //{
                    //    try
                    //    {
                    //        // อ่าน config เครื่อง POS
                    //        string __localPath = string.Format(@"c:\\smlsoft\\smlPOSScreenConfig-{0}-{1}-{2}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__").Replace("/", string.Empty), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName);
                    //        XmlDocument xDoc = new XmlDocument();
                    //        try
                    //        {
                    //            xDoc.Load(__localPath);
                    //        }
                    //        catch
                    //        {
                    //        }

                    //        xDoc.DocumentElement.Normalize();
                    //        XmlElement __xRoot = xDoc.DocumentElement;

                    //        string __posId = __xRoot.GetElementsByTagName("_posid").Item(0).InnerText;
                    //        this._setDataStr(this._searchName, __posId);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show("Cannot Load Pos Config", "ผิดพลาด");
                    //    }

                    //}
                    //else
                    //{
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
                // get data from restful server
                WebClient __n = new WebClient();
                string _data = "{\"P_agentcode\":\"" + _agentCode + "\"}";
                //string _data = "{\"P_agentcode\":\"0001014678\"}";
                MyLib._restClient __rest = new _restClient("http://ws-dev.boonrawd.co.th/MasterPaymentAgent/api/SMLHeaderBill", HttpVerb.POST, _data);
                __rest._setContentType("application/json");
                __rest._addHeaderRequest(string.Format("APIKey: {0}", "api_sml"));
                __rest._addHeaderRequest(string.Format("APISecret: {0}", "@p1_$m1@p1_$m1"));
                string __result = __rest.MakeRequest("");
                if (__result.Length > 0)
                {
                    JsonValue __jsonObject = JsonValue.Parse(__result);
                    if (__jsonObject.Count > 0)
                    {
                        JsonValue __resultObject = JsonValue.Parse(__jsonObject["Result"].ToString());
                        JsonValue __lastResulValue = null;
                        for (int __row1 = 0; __row1 < __resultObject.Count; __row1++)
                        {
                            transdatasap __transdatasap = transdatasap.Parse(__resultObject[__row1].ToString());

                            string __BILLINGDOCNO = __resultObject[__row1]["BILLINGDOCNO"].ToString().Replace("\"", string.Empty);


                            //ดึง detail 6002351316
                            string _datadetail = "{\"P_billingdocno\":\"" + __BILLINGDOCNO + "\",\"P_agentcode\":\"" + _agentCode + "\"}";
                           // string _datadetail = "{\"P_billingdocno\":\"6002351316\",\"P_agentcode\":\"" + _agentCode + "\"}";
                            MyLib._restClient __restdetail = new _restClient("http://ws-dev.boonrawd.co.th/MasterPaymentAgent/api/SMLDetailMat", HttpVerb.POST, _datadetail);
                            __restdetail._setContentType("application/json");
                            __restdetail._addHeaderRequest(string.Format("APIKey: {0}", "api_sml"));
                            __restdetail._addHeaderRequest(string.Format("APISecret: {0}", "@p1_$m1@p1_$m1"));
                            string __resultdetail = __restdetail.MakeRequest("");
                            JsonValue __jsonDetailObject = JsonValue.Parse(__resultdetail);
                            JsonArray __resultarray = new JsonArray();
                            JsonObject detail = new JsonObject();
                            detail.Add("detail", __jsonDetailObject["Result"]);
                            string jsondetail = detail.ToString();

                            __transdatasap.doc_format_code = this._icTransScreenTopControl1._getDataStr(_g.d.ic_trans._doc_no).ToString();
                            __transdatasap.wh_from = this._icTransScreenTopControl1._getDataStr(_g.d.ic_trans._wh_from).ToString();
                            __transdatasap.location_from = this._icTransScreenTopControl1._getDataStr(_g.d.ic_trans._location_from).ToString();

                            __transdatasap.details = new List<transdatadetailsap>();
                            if (__jsonDetailObject["Result"].Count > 0)
                            {

                                for (int __row = 0; __row1 < __jsonDetailObject["Result"].Count; __row1++)
                                {
                                    transdatadetailsap __transdatadetailsap = transdatadetailsap.Parse(__jsonDetailObject["Result"][__row1].ToString());

                                    __transdatasap.details.Add(__transdatadetailsap);
                                }
                            }
                            __transdatasap.check();

                            //  JsonArray __resultdetailObject = JsonArray.Parse(__jsonDetailObject["Result"].ToString());

                            //transdatadetail __transdatadetail = transdatadetail.Parse(__resultdetailObject[0].ToString());

                            __lastResulValue = __transdatasap._getJson();
                            // __lastResulValue = __transdata._getJson(__resultObject[__row1].ToString(), __transdatadetail.ToString());
                            // string __BILLINGDOCNO = __lastResulValue["BILLINGDOCNO"].ToString().Replace("\"", string.Empty);


                            int __rowAdd = this._docGrid._addRow();
                            this._docGrid._cellUpdate(__rowAdd, _g.d.ic_trans._doc_no, __BILLINGDOCNO, true);
                            this._docGrid._cellUpdate(__rowAdd, 0, 1, true);
                            this._docGrid._cellUpdate(__rowAdd, "data", __lastResulValue, true);
                        }
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
                        if (this._docGrid._cellGet(__row, 0).ToString().Equals("1"))
                        {
                            try
                            {
                                string _data = this._docGrid._cellGet(__row, 2).ToString();
                                MessageBox.Show(_data);
                                //WebClient __n = new WebClient();
                                //MyLib._restClient __rest = new _restClient("http://ws-dev.boonrawd.co.th/MasterPaymentAgent/api/SMLHeaderBill", HttpVerb.POST, _data);
                                //__rest._setContentType("application/json");
                                //__rest._addHeaderRequest(string.Format("APIKey: {0}", "api_sml"));
                                //__rest._addHeaderRequest(string.Format("APISecret: {0}", "@p1_$m1@p1_$m1"));
                                //string __result = __rest.MakeRequest("");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    } // end loop

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

    class transdatasap
    {
        public string Agentcode { get; set; }
        public string BILLINGDOCNO { get; set; }
        public string doc_no { get; set; }
        public string doc_format_code { get; set; }
        public string doc_date { get; set; }
        public string doc_time { get; set; }
        public string ap_code { get; set; }
        public string tax_doc_date { get; set; }
        public string tax_doc_no { get; set; }
        public string vat_rate { get; set; }
        public string total_value { get; set; }
        public string total_discount { get; set; }
        public string total_before_vat { get; set; }
        public string total_vat_value { get; set; }
        public string total_except_vat { get; set; }
        public string total_after_vat { get; set; }
        public string total_amount { get; set; }
        public List<transdatadetailsap> details { get; set; }
        public string wh_from { get; set; }
        public string location_from { get; set; }
        public string credit_day { get; set; }
        public string credit_date { get; set; }
        public string remark { get; set; }
        public string discount_word { get; set; }
        public string PAYMENTTERM_CAL { get; set; }
        public string Bat_number { get; set; }
        public string Bat_date { get; set; }
        public string cust_code { get; set; }


        public transdatasap()
        {

        }
        public string _getJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            //JsonValue __resultObject = JsonValue.Parse(json);
            return json;

        }
        public void check()
        {
            this.doc_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true));
            this.doc_time = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2");
            this.doc_no = this.Agentcode.Substring(3) + "-" + this.BILLINGDOCNO;
            this.cust_code = this.ap_code;
            this.doc_format_code = this.doc_format_code;
            //this.vat_rate = MyLib._myGlobal._decimalPhase(this.vat_rate.ToString().Replace("%", string.Empty));
            this.vat_rate =this.vat_rate.Replace("%", string.Empty);
        }

        public static transdatasap Parse(string json)
        {

            if (json != null)
            {
                transdatasap _transdata = JsonConvert.DeserializeObject<transdatasap>(json);
                return _transdata;
            }
            return null;
        }
    }

    class transdatadetailsap
    {
        public string BILLINGDOCNO { get; set; }
        public string Agentcode { get; set; }
        public string MATERIALCODE { get; set; }
        public string line_number { get; set; }
        public string is_permium { get; set; }
        public string SALESUNIT { get; set; }
        public string wh_code { get; set; }
        public string shelf_code { get; set; }
        public string qty { get; set; }
        public string price { get; set; }
        public string price_exclude_vat { get; set; }
        public string discount_amount { get; set; }
        public string sum_amount { get; set; }
        public string vat_amount { get; set; }
        public string tax_type { get; set; }
        public string vat_type { get; set; }

        public transdatadetailsap()
        {

        }

        public JsonValue _getJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            JsonValue __resultObject = JsonValue.Parse(json);
            return __resultObject;
        }
        public static transdatadetailsap Parse(string json)
        {
            if (json != null)
            {
                transdatadetailsap _transdatadetail = JsonConvert.DeserializeObject<transdatadetailsap>(json);
                return _transdatadetail;
            }
            return null;
        }
    }
}
