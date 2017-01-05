using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;
using System.Json;
using System.Globalization;

namespace BRInterface.saletools
{
    public partial class import_request_transfer : UserControl
    {
        string _url = "http://61.91.199.124/STSMLService/SMLService/STWSSML.asmx";
        string _action = "http://61.91.199.124/STSMLService/SMLService/STWSSML.asmx?op=Export_STGoodsTransfer";
        DataTable _dataDetail;
        DataTable _data;

        string _agentCode = "";

        public import_request_transfer()
        {
            InitializeComponent();

            // get agent code
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort("select * from " + _g.d.erp_company_profile._table);

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                this._agentCode = __result.Tables[0].Rows[0][_g.d.erp_company_profile._sap_code].ToString();
            }

            this._docGrid._table_name = _g.d.ic_trans._table;
            this._docGrid._addColumn("select", 11, 10, 10);
            this._docGrid._isEdit = false;
            this._docGrid._addColumn(_g.d.ic_trans._doc_no, 1, 25, 20);
            this._docGrid._addColumn(_g.d.ic_trans._doc_date, 4, 0, 25);
            this._docGrid._addColumn(_g.d.ic_trans._sale_code, 1, 25, 20);
            this._docGrid._addColumn(_g.d.ic_trans._doc_type, 1, 25, 20);
            this._docGrid.WidthByPersent = true;
            this._docGrid._calcPersentWidthToScatter();
            this._docGrid._mouseClick += _docGrid__mouseClick;

            this._detailGrid._table_name = _g.d.ic_trans_detail._table;
            this._detailGrid._isEdit = false;
            this._detailGrid._addColumn(_g.d.ic_trans_detail._item_code, 1, 25, 10);
            this._detailGrid._addColumn(_g.d.ic_trans_detail._qty, 1, 25, 10);
            this._detailGrid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 25, 10);
            this._detailGrid._addColumn(_g.d.ic_trans_detail._wh_code, 1, 25, 10);
            this._detailGrid._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 25, 10);

            this._detailGrid.WidthByPersent = true;
            this._detailGrid._calcPersentWidthToScatter();




            _getData();
        }

        private void _docGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // load data to detail
            if (e._row > -1)
            {
                string __docNo = this._docGrid._cellGet(e._row, _g.d.ic_trans._doc_no).ToString();
                DataRow[] __getRow = this._dataDetail.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");
                this._detailGrid._loadFromDataTable(this._dataDetail, __getRow);

            }
        }

        void _getData()
        {
            try
            {
                /*
                XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
                HttpWebRequest webRequest = CreateWebRequest(_url, "Export_STGoodsTransfer");
                InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                // do something usefull here like update your UI.
                asyncResult.AsyncWaitHandle.WaitOne();

                // get the response from the completed web request.
                string soapResult;
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                    Console.Write(soapResult);
                }*/

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_action);
                request.Method = "POST";
                request.ContentType = "text/xml;charset=UTF-8";
                //request.Headers.Add("SOAPAction", "\"" + _action + "\"");

                string data = CreateSoapEnvelope();
                byte[] byteData = Encoding.UTF8.GetBytes(data.ToString());      // Create a byte array of the data we want to send
                request.ContentLength = byteData.Length;                        // Set the content length in the request headers

                // Write data to request
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                string result = "";
                XmlDocument xmlResult = new XmlDocument();
                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        result = reader.ReadToEnd();
                        reader.Close();
                    }
                    xmlResult.LoadXml(result);
                    xmlResult.Normalize();

                    XmlNamespaceManager manager = new XmlNamespaceManager(xmlResult.NameTable);

                    manager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    manager.AddNamespace("hbr", "http://tempuri.org/");

                    XmlNodeList xnList = xmlResult.SelectNodes("//hbr:Export_STGoodsTransferResult", manager);

                    if (xnList.Count > 0)
                    {
                        string __xml = xnList[0].InnerText; //.Replace("\r", "").Replace("\n", ""); ;

                        JsonValue __json = JsonValue.Parse(__xml);

                        //DataTable __getDataTable = _jsonConvert.JsonStringToDataTable(__array);

                        _dataDetail = new DataTable();
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._doc_date, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._doc_no, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._line_number, typeof(int));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._item_code, typeof(String));
                        _dataDetail.Columns.Add("POSTINGDATE", typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._sale_code, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans._doc_type, typeof(String));
                        _dataDetail.Columns.Add("RETBATCH", typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._qty, typeof(Decimal));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._unit_code, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._wh_code, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._shelf_code, typeof(String));
                        _dataDetail.Columns.Add("ENTRYDATE", typeof(String));
                        _dataDetail.Columns.Add(_g.d.sync_table_list._sync_count, typeof(int));

                        foreach (JsonValue value in __json)
                        {
                            decimal __qty = MyLib._myGlobal._decimalPhase(value["QTY"]);
                            int __lineNumber = MyLib._myGlobal._intPhase(value["LINENUMBER"].ToString());
                            int __totalRecord = MyLib._myGlobal._intPhase(value["TOTALRECORDS"].ToString());
                            string __dateCheck = value["DOCDATE"].ToString().Replace("\"", string.Empty);

                            string __docDate = null;
                            string __docDateStr = value["DOCDATE"];

                            if (__docDateStr.ToString().Length > 0)
                                __docDate = DateTime.ParseExact(__docDateStr, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                            DateTime __entryDate = new DateTime(1900, 01, 01);

                            //(value["ENTRYDATE"].Equals("00000000") ? null :  DateTime.ParseExact(value["ENTRYDATE"], "yyyyMMdd", CultureInfo.InvariantCulture);
                            string __postingDate = null;
                            string __postingDateStr = value["POSTINGDATE"];
                            if (__postingDateStr.Length > 0)
                                __postingDate = DateTime.ParseExact(__postingDateStr, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                            _dataDetail.Rows.Add(
                                __docDate,
                                value["DOCNUMBER"].ToString().Replace("\"", string.Empty),
                                __lineNumber,
                                value["MATERIAL"].ToString().Replace("\"", string.Empty),
                                __postingDate,
                                value["EMPLOYEE"].ToString().Replace("\"", string.Empty),
                                value["TYPE"].ToString().Replace("\"", string.Empty),
                                value["RETBATCH"].ToString().Replace("\"", string.Empty),
                                __qty,
                                value["UNIT"].ToString().Replace("\"", string.Empty),
                                value["PLANT"].ToString().Replace("\"", string.Empty),
                                value["STORAGE"].ToString().Replace("\"", string.Empty),
                                __entryDate,
                                __totalRecord
                                );
                        }

                        int __rowCount = _dataDetail.Rows.Count;
                        this._data = MyLib._dataTableExtension._selectDistinct(_dataDetail, _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._sale_code + "," + _g.d.ic_trans._doc_type);

                    }


                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                this._docGrid._loadFromDataTable(this._data);

                // check import ไปแล้ว ไม่ต้องแสดง
                StringBuilder __docListTemp = new StringBuilder();

                for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
                {
                    if (__docListTemp.Length > 0)
                    {
                        __docListTemp.Append(",");
                    }

                    string __docNo = this._docGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString();
                    __docListTemp.Append("\'" + __docNo + "\'");
                }

                if (__docListTemp.Length > 0)
                {
                    string __query = "select doc_no from ic_trans where trans_flag = 124 and doc_no in (" + __docListTemp.ToString() + ")";
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataSet __result = __myFrameWork._queryShort(__query);
                    if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                    {
                        for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                        {
                            string __findDocNo = __result.Tables[0].Rows[__row][_g.d.ic_trans._doc_no].ToString();
                            int __foundRow = this._docGrid._findData(this._docGrid._findColumnByName(_g.d.ic_trans._doc_no), __findDocNo);
                            this._docGrid._rowData.RemoveAt(__foundRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.Accept = "text/xml";
            return webRequest;
        }

        private string CreateSoapEnvelope()
        {
            XmlDocument soapEnvelop = new XmlDocument();

            string __xmlData = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">" +
                "<soap12:Body>" +
                "<Export_STGoodsTransfer xmlns=\"http://tempuri.org/\">" +
                "<AgentCode>" + this._agentCode + "</AgentCode>" +
                "</Export_STGoodsTransfer>" +
                "</soap12:Body>" +
                "</soap12:Envelope>";


            // soapEnvelop.LoadXml(__xmlData);
            return __xmlData;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _reloadButton_Click(object sender, EventArgs e)
        {
            this._getData();
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการนำเข้าข้อมูลที่ได้เลือกไว้หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string __docTime = DateTime.Now.ToString("HH:MM");

                for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
                {
                    if (this._docGrid._cellGet(__row, 0).ToString().Equals("1"))
                    {
                        StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                        string __docNo = this._docGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString();
                        string __docType = this._docGrid._cellGet(__row, _g.d.ic_trans._doc_type).ToString();
                        string __doCDate = MyLib._myGlobal._convertDateToQuery((DateTime)this._docGrid._cellGet(__row, _g.d.ic_trans._doc_date));

                        DataRow[] __docRow = this._data.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");
                        DataRow[] __detailRow = this._dataDetail.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");

                        // do import data
                        string __fieldTransList = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._doc_no, _g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._sale_code);


                        if (__docType.ToLower().Equals("r"))
                        {
                            __fieldTransList += "," + _g.d.ic_trans._wh_from + "," + _g.d.ic_trans._location_from;
                        }
                        else
                        {
                            __fieldTransList += "," + _g.d.ic_trans._wh_to + "," + _g.d.ic_trans._location_to;
                        }
                        //MyLib._myGlobal._convertDateToQuery((DateTime)__docRow[0][_g.d.ic_trans._doc_date])
                        // ic_trans import
                        string __insertTrans = "insert into " + _g.d.ic_trans._table + "(" + __fieldTransList + ",send_type) values (124, 3, \'" + __docNo + "\', \'" + __doCDate + "\', \'" + __docTime + "\', \'" + __detailRow[0][_g.d.ic_trans._sale_code].ToString() + "\', \'" + __detailRow[0][_g.d.ic_trans_detail._wh_code].ToString() + "\', \'" + __detailRow[0][_g.d.ic_trans_detail._shelf_code].ToString() + "\',99) ";


                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertTrans));

                        // ic_trans_detail import
                        string __fieldDetailList = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._trans_flag, _g.d.ic_trans_detail._trans_type, _g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_time,
                            _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._qty);

                        if (__docType.ToLower().Equals("r"))
                        {
                            __fieldDetailList += "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code;
                        }
                        else
                        {
                            __fieldDetailList += "," + _g.d.ic_trans_detail._wh_code_2 + "," + _g.d.ic_trans_detail._shelf_code_2;
                        }

                        string __dataTransList = "124, 3, \'" + __docNo + "\', \'" + __doCDate + "\', \'" + __docTime + "\',";
                        for (int __rowDetail = 0; __rowDetail < __detailRow.Length; __rowDetail++)
                        {
                            string __itemcode = __detailRow[__rowDetail][_g.d.ic_trans_detail._item_code].ToString();
                            string __unitCode = __detailRow[__rowDetail][_g.d.ic_trans_detail._unit_code].ToString();
                            decimal __qty = MyLib._myGlobal._decimalPhase(__detailRow[__rowDetail][_g.d.ic_trans_detail._qty].ToString());
                            string __whCode = __detailRow[__rowDetail][_g.d.ic_trans_detail._wh_code].ToString();
                            string __shelfCode = __detailRow[__rowDetail][_g.d.ic_trans_detail._shelf_code].ToString();

                            string __insertDetail = "insert into " + _g.d.ic_trans_detail._table +
                                "(" + __fieldDetailList + ") values " +
                                "(" + __dataTransList +
                                "\'" + __itemcode + "\', \'" + __unitCode + "\', \'" + __qty.ToString() + "\', \'" + __whCode + "\', \'" + __shelfCode + "\') ";

                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertDetail));

                        }

                        // update ค่าอื่น ๆ
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set item_name = (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) where doc_no = \'" + __docNo + "\' and coalesce(item_name, '') <> (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) "));

                        __queryList.Append("</node>");

                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());

                    }
                }

                MessageBox.Show("Success");
                this._getData();
            }
        }
    }
}
