using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Json;
using System.Globalization;
using System.Net;
using System.IO;

namespace BRInterface.saletools
{
    public partial class _import_so_invoice : UserControl
    {
        string _url = "http://61.91.199.124/STSMLService/SMLService/STWSSML.asmx";
        string _action = "http://61.91.199.124/STSMLService/SMLService/STWSSML.asmx?op=Export_STSalesInvoice";
        DataTable _dataDetail;
        DataTable _data;
        DataTable _payDetail;

        string _agentCode = "";
        int _transFlag = 0;
        int _transType = 0;

        /// <summary>
        /// Mode, 1=Invoice, 2 = Sale Order, 3 = มัดจำขวดลัง 
        /// </summary>
        int _mode = 0;

        public _import_so_invoice(int mode)
        {
            InitializeComponent();

            this._mode = mode;


            switch (this._mode)
            {
                case 1:
                    _transFlag = 44;
                    _transType = 2;
                    break;
                case 2:
                    _transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย);
                    _transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_สั่งขาย);
                    break;
            }

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

                    XmlNodeList xnList = xmlResult.SelectNodes("//hbr:Export_STSalesInvoiceResult", manager);

                    if (xnList.Count > 0)
                    {
                        string __xml = xnList[0].InnerText; //.Replace("\r", "").Replace("\n", ""); ;

                        JsonValue __json = JsonValue.Parse(__xml);

                        // shipto_party

                        _data = new DataTable();
                        _data.Columns.Add(_g.d.ic_trans._doc_no, typeof(String));
                        _data.Columns.Add(_g.d.ic_trans._doc_date, typeof(String));
                        _data.Columns.Add(_g.d.ic_trans._cust_code, typeof(String));
                        _data.Columns.Add(_g.d.ic_trans._sale_code, typeof(String));
                        _data.Columns.Add(_g.d.ic_trans._inquiry_type, typeof(int));
                        _data.Columns.Add(_g.d.ic_trans._vat_type, typeof(int));
                        _data.Columns.Add(_g.d.ic_trans._send_date, typeof(String));
                        _data.Columns.Add(_g.d.ic_trans_shipment._transport_address, typeof(String));
                        _data.Columns.Add(_g.d.ic_trans._total_value, typeof(Decimal));
                        _data.Columns.Add(_g.d.ic_trans._total_after_discount, typeof(Decimal));
                        _data.Columns.Add(_g.d.ic_trans._total_after_vat, typeof(Decimal));
                        _data.Columns.Add(_g.d.ic_trans._total_amount, typeof(Decimal));
                        _data.Columns.Add(_g.d.ic_trans._total_before_vat, typeof(Decimal));
                        _data.Columns.Add(_g.d.ic_trans._total_discount, typeof(Decimal));
                        _data.Columns.Add(_g.d.ic_trans._total_except_vat, typeof(Decimal));
                        _data.Columns.Add(_g.d.ic_trans._total_vat_value, typeof(Decimal));
                        _data.Columns.Add(_g.d.ic_trans._vat_rate, typeof(Decimal));
                        _data.Columns.Add(_g.d.cb_trans._cash_amount, typeof(Decimal));
                        _data.Columns.Add(_g.d.cb_trans._chq_amount, typeof(Decimal));
                        _data.Columns.Add(_g.d.cb_trans._card_amount, typeof(Decimal));
                        _data.Columns.Add(_g.d.ic_trans._last_status, typeof(int));


                        _dataDetail = new DataTable();
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._doc_date, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._doc_no, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._line_number, typeof(int));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._item_code, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._is_permium, typeof(int));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._qty, typeof(Decimal));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._unit_code, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._wh_code, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._shelf_code, typeof(String));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._price, typeof(Decimal));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._price_exclude_vat, typeof(Decimal));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._sum_amount, typeof(Decimal));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._discount_amount, typeof(Decimal));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._total_vat_value, typeof(Decimal));
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._tax_type, typeof(int)); //  "ประเภทภาษี 0 = มีภาษี, 1 = ยกเว้น"
                        _dataDetail.Columns.Add(_g.d.ic_trans_detail._vat_type, typeof(int)); //  "ประเภทภาษี 0 = มีภาษี, 1 = ยกเว้น"

                        _payDetail = new DataTable();
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._doc_date, typeof(String));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._doc_no, typeof(String));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._doc_type, typeof(int));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._credit_card_type, typeof(String));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._trans_number, typeof(String));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._amount, typeof(Decimal));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._approve_code, typeof(String));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._chq_due_date, typeof(String));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._bank_code, typeof(String));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._charge, typeof(Decimal));
                        _payDetail.Columns.Add(_g.d.cb_trans_detail._sum_amount, typeof(Decimal));

                        for (int __row = 0; __row < __json.Count; __row++)
                        {
                            int __source = MyLib._myGlobal._intPhase(__json[__row]["source"].ToString().Replace("\"", string.Empty));
                            if (__source == this._mode)
                            {
                                string __docNo = __json[__row]["doc_no"].ToString().Replace("\"", string.Empty);
                                string __docDate = DateTime.ParseExact(__json[__row]["doc_date"], "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                                string __custCode = __json[__row]["cust_code"].ToString().Replace("\"", string.Empty);
                                string __saleCode = __json[__row]["sale_code"].ToString().Replace("\"", string.Empty);

                                int __saleType = MyLib._myGlobal._intPhase(__json[__row]["sale_type"].ToString().Replace("\"", string.Empty));
                                int __vatType = MyLib._myGlobal._intPhase(__json[__row]["tax_type"].ToString().Replace("\"", string.Empty));

                                string __sendDate = DateTime.ParseExact(__json[__row]["delivery_date"], "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                                string __soldTo = __json[__row]["soldto_party"].ToString().Replace("\"", string.Empty);

                                Decimal __totalValue = MyLib._myGlobal._decimalPhase(__json[__row]["total_value"].ToString().Replace("\"", string.Empty));
                                Decimal __totalAfterDiscount = MyLib._myGlobal._decimalPhase(__json[__row]["total_after_discount"].ToString().Replace("\"", string.Empty));
                                Decimal __totalAfterVat = MyLib._myGlobal._decimalPhase(__json[__row]["total_after_vat"].ToString().Replace("\"", string.Empty));
                                Decimal __totalAmount = MyLib._myGlobal._decimalPhase(__json[__row]["total_amount"].ToString().Replace("\"", string.Empty));
                                Decimal __totalBeforeVat = MyLib._myGlobal._decimalPhase(__json[__row]["total_before_vat"].ToString().Replace("\"", string.Empty));
                                Decimal __totalDiscount = MyLib._myGlobal._decimalPhase(__json[__row]["total_discount"].ToString().Replace("\"", string.Empty));
                                Decimal __totalExceptVat = MyLib._myGlobal._decimalPhase(__json[__row]["total_except_vat"].ToString().Replace("\"", string.Empty));
                                Decimal __totalVatValue = MyLib._myGlobal._decimalPhase(__json[__row]["total_vat_value"].ToString().Replace("\"", string.Empty));
                                Decimal __vatRate = MyLib._myGlobal._decimalPhase(__json[__row]["vat_rate"].ToString().Replace("\"", string.Empty));
                                Decimal __cashAmount = MyLib._myGlobal._decimalPhase(__json[__row]["cash_amount"].ToString().Replace("\"", string.Empty));
                                Decimal __chqAmount = MyLib._myGlobal._decimalPhase(__json[__row]["chq_amount"].ToString().Replace("\"", string.Empty));
                                Decimal __cardAmount = MyLib._myGlobal._decimalPhase(__json[__row]["credit_amount"].ToString().Replace("\"", string.Empty));
                                int __lastStatus = 0;

                                _data.Rows.Add(
                                    __docNo,
                                    __docDate,
                                    __custCode,
                                    __saleCode,
                                    __saleType,
                                    __vatType,
                                    __sendDate,
                                    __soldTo,
                                    __totalValue,
                                    __totalAfterDiscount,
                                    __totalAfterVat,
                                    __totalAmount,
                                    __totalBeforeVat,
                                    __totalDiscount,
                                    __totalExceptVat,
                                    __totalVatValue,
                                    __vatRate,
                                    __cashAmount,
                                    __chqAmount,
                                    __cardAmount,
                                    __lastStatus
                                    );

                                // detail
                                JsonValue __items = __json[__row]["details"];

                                for (int __rowDetail = 0; __rowDetail < __items.Count; __rowDetail++)
                                {
                                    string __itemCode = __items[__rowDetail]["item_code"].ToString().Replace("\"", string.Empty);
                                    string __unitCode = __items[__rowDetail]["unit_code"].ToString().Replace("\"", string.Empty);
                                    string __whCode = __items[__rowDetail]["wh_code"].ToString().Replace("\"", string.Empty);
                                    string __shelfCode = __items[__rowDetail]["shelf_code"].ToString().Replace("\"", string.Empty);
                                    int __isPremium = MyLib._myGlobal._intPhase(__items[__rowDetail]["is_permium"].ToString().Replace("\"", string.Empty));
                                    int __lineNumber = MyLib._myGlobal._intPhase(__items[__rowDetail]["line_number"].ToString().Replace("\"", string.Empty));

                                    Decimal __qty = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["qty"].ToString().Replace("\"", string.Empty));
                                    Decimal __price = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["price"].ToString().Replace("\"", string.Empty));
                                    Decimal __priceExcludeVat = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["price_exclude_vat"].ToString().Replace("\"", string.Empty));
                                    Decimal __sumAmount = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["sum_amount"].ToString().Replace("\"", string.Empty));

                                    Decimal __discount_amount = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["discount_amount"].ToString().Replace("\"", string.Empty));
                                    Decimal __total_vat_value = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["vat_amount"].ToString().Replace("\"", string.Empty));

                                    int __tax_type = MyLib._myGlobal._intPhase(__items[__rowDetail]["tax_type"].ToString().Replace("\"", string.Empty));
                                    int __vat_type = MyLib._myGlobal._intPhase(__items[__rowDetail]["vat_type"].ToString().Replace("\"", string.Empty));

                                    _dataDetail.Rows.Add(
                                        __docDate,
                                        __docNo,
                                        __lineNumber,
                                        __itemCode,
                                        __isPremium,
                                        __qty,
                                        __unitCode,
                                        __whCode,
                                        __shelfCode,
                                        __price,
                                        __priceExcludeVat,
                                        __sumAmount,
                                        __discount_amount,
                                        __total_vat_value,
                                        __tax_type,
                                        __vat_type
                                        );


                                }

                                JsonValue __pays = __json[__row]["paydetails"];
                                for (int __payDetail = 0; __payDetail < __pays.Count; __payDetail++)
                                {
                                    int __docType = MyLib._myGlobal._intPhase(__pays[__payDetail]["pay_type"].ToString().Replace("\"", string.Empty));
                                    string __creditCardType = __pays[__payDetail]["credit_card_type"].ToString().Replace("\"", string.Empty);
                                    string __transNumber = __pays[__payDetail]["pay_trans_number"].ToString().Replace("\"", string.Empty);
                                    Decimal __amount = MyLib._myGlobal._decimalPhase(__pays[__payDetail]["pay_amount"].ToString().Replace("\"", string.Empty));
                                    string __approveCode = __pays[__payDetail]["no_approved"].ToString().Replace("\"", string.Empty);

                                    string __chqDueDateStr = __pays[__payDetail]["chq_due_date"].ToString().Replace("\"", string.Empty);
                                    string __chqDueDate = ((__chqDueDateStr.Length > 0) ? DateTime.ParseExact(__chqDueDateStr, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") : "");

                                    string __bankCode = __pays[__payDetail]["bank_code"].ToString().Replace("\"", string.Empty);
                                    Decimal __charge = MyLib._myGlobal._decimalPhase(__pays[__payDetail]["charge"].ToString().Replace("\"", string.Empty));
                                    Decimal __sumAmount = MyLib._myGlobal._decimalPhase(__pays[__payDetail]["sum_amount"].ToString().Replace("\"", string.Empty));

                                    _payDetail.Rows.Add(
                                        __docDate,
                                        __docNo,
                                        __docType,
                                        __creditCardType,
                                        __transNumber,
                                        __amount,
                                        __approveCode,
                                        __chqDueDate,
                                        __bankCode,
                                        __charge,
                                        __sumAmount
                                        );
                                }
                            }
                            // paydetail
                        }


                        /*

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

                            var __docDate = DateTime.ParseExact(value["DOCDATE"], "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                            DateTime __entryDate = new DateTime(1900, 01, 01);

                            //(value["ENTRYDATE"].Equals("00000000") ? null :  DateTime.ParseExact(value["ENTRYDATE"], "yyyyMMdd", CultureInfo.InvariantCulture);
                            DateTime __postingDate = DateTime.ParseExact(value["POSTINGDATE"], "yyyyMMdd", CultureInfo.InvariantCulture);

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
                        */
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
                    string __query = "select doc_no from ic_trans where trans_flag = " + this._transFlag.ToString() + " and doc_no in (" + __docListTemp.ToString() + ")";
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

        private string CreateSoapEnvelope()
        {
            XmlDocument soapEnvelop = new XmlDocument();

            string __xmlData = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">" +
                "<soap12:Body>" +
                "<Export_STSalesInvoice xmlns=\"http://tempuri.org/\">" +
                "<AgentCode>" + this._agentCode + "</AgentCode>" +
                "</Export_STSalesInvoice>" +
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
            _process();
        }

        void _process()
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
                        DateTime __docDateTime = (DateTime)this._docGrid._cellGet(__row, _g.d.ic_trans._doc_date);

                        string __docDate = MyLib._myGlobal._convertDateToQuery((DateTime)this._docGrid._cellGet(__row, _g.d.ic_trans._doc_date));

                        DataRow[] __docRow = this._data.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");
                        DataRow[] __detailRow = this._dataDetail.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");
                        DataRow[] __payDetailRow = this._payDetail.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");

                        // do import data

                        #region ic_trans

                        string __fieldTransList = MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type,
                            _g.d.ic_trans._doc_no, _g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time,
                            _g.d.ic_trans._tax_doc_no, _g.d.ic_trans._tax_doc_date,
                            _g.d.ic_trans._sale_code, _g.d.ic_trans._cust_code,
                            _g.d.ic_trans._inquiry_type, _g.d.ic_trans._vat_type,

                            _g.d.ic_trans._vat_rate, _g.d.ic_trans._total_value,
                            _g.d.ic_trans._send_date, _g.d.ic_trans._discount_word, _g.d.ic_trans._total_discount,
                            _g.d.ic_trans._total_before_vat,
                            _g.d.ic_trans._total_vat_value, _g.d.ic_trans._total_after_vat,
                            _g.d.ic_trans._total_except_vat, _g.d.ic_trans._total_amount);

                        decimal __totalValue = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.ic_trans._total_value].ToString());
                        decimal __totalAfterDiscount = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.ic_trans._total_after_discount].ToString());
                        decimal __totalAfterVat = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.ic_trans._total_after_vat].ToString());
                        decimal __totalAmount = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.ic_trans._total_amount].ToString());
                        decimal __totalBeforeVat = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.ic_trans._total_before_vat].ToString());
                        decimal __totalDiscount = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.ic_trans._total_discount].ToString());
                        decimal __totalExceptVat = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.ic_trans._total_except_vat].ToString());
                        decimal __totalVatValue = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.ic_trans._total_vat_value].ToString());
                        decimal __vatRate = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.ic_trans._vat_rate].ToString());

                        string __sendDate = __docRow[0][_g.d.ic_trans._send_date].ToString();
                        //MyLib._myGlobal._convertDateToQuery((DateTime)__docRow[0][_g.d.ic_trans._doc_date])

                        // ic_trans import
                        string __insertTrans = "insert into " + _g.d.ic_trans._table + "(" + __fieldTransList + ") " +
                            " values (" +
                            _transFlag.ToString() + ", " + _transType.ToString() + ", \'" + __docNo + "\', \'" + __docDate + "\', \'" + __docTime + "\'," +
                            "\'" + __docNo + "\', \'" + __docDate + "\'," +
                            "\'" + __docRow[0][_g.d.ic_trans._sale_code].ToString() + "\', \'" + __docRow[0][_g.d.ic_trans._cust_code].ToString() + "\'," +
                            "\'" + (__docRow[0][_g.d.ic_trans._inquiry_type].ToString().Equals("1") ? "0" : "1") + "\',\'" + __docRow[0][_g.d.ic_trans._vat_type].ToString() + "\'," +

                            "\'" + __vatRate.ToString() + "\', \'" + __totalValue.ToString() + "\'," +
                            "" + ((__sendDate.Length > 0) ? "\'" + __sendDate + "\'" : "null") + ", \'" + __totalDiscount.ToString() + "\', \'" + __totalDiscount.ToString() + "\'," +
                            "\'" + __totalBeforeVat.ToString() + "\', " +
                            "\'" + __totalVatValue.ToString() + "\', \'" + __totalAfterVat.ToString() + "\', " +
                            "\'" + __totalExceptVat.ToString() + "\', \'" + __totalAmount.ToString() + "\'" +
                            ") ";

                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertTrans));

                        #endregion

                        #region ic_trans_detail import

                        string __fieldDetailList = MyLib._myGlobal._fieldAndComma(
                            _g.d.ic_trans_detail._trans_flag, _g.d.ic_trans_detail._trans_type,
                            _g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_time,

                            _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._qty,
                            _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code,

                            _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._price_exclude_vat, _g.d.ic_trans_detail._sum_amount,
                            _g.d.ic_trans_detail._discount_amount, _g.d.ic_trans_detail._discount, _g.d.ic_trans_detail._total_vat_value,
                            _g.d.ic_trans_detail._tax_type, _g.d.ic_trans_detail._vat_type);


                        string __dataTransList = _transFlag.ToString() + ", " + _transType.ToString() + ", \'" + __docNo + "\', \'" + __docDate + "\', \'" + __docTime + "\',";

                        for (int __rowDetail = 0; __rowDetail < __detailRow.Length; __rowDetail++)
                        {
                            string __itemcode = __detailRow[__rowDetail][_g.d.ic_trans_detail._item_code].ToString();
                            string __unitCode = __detailRow[__rowDetail][_g.d.ic_trans_detail._unit_code].ToString();
                            decimal __qty = MyLib._myGlobal._decimalPhase(__detailRow[__rowDetail][_g.d.ic_trans_detail._qty].ToString());
                            string __whCode = __detailRow[__rowDetail][_g.d.ic_trans_detail._wh_code].ToString();
                            string __shelfCode = __detailRow[__rowDetail][_g.d.ic_trans_detail._shelf_code].ToString();
                            decimal __price = MyLib._myGlobal._decimalPhase(__detailRow[__rowDetail][_g.d.ic_trans_detail._price].ToString());
                            decimal __priceExcludeVat = MyLib._myGlobal._decimalPhase(__detailRow[__rowDetail][_g.d.ic_trans_detail._price_exclude_vat].ToString());
                            decimal __sumAmount = MyLib._myGlobal._decimalPhase(__detailRow[__rowDetail][_g.d.ic_trans_detail._sum_amount].ToString());

                            decimal __discountAmount = MyLib._myGlobal._decimalPhase(__detailRow[__rowDetail][_g.d.ic_trans_detail._discount_amount].ToString());
                            decimal __vatValue = MyLib._myGlobal._decimalPhase(__detailRow[__rowDetail][_g.d.ic_trans_detail._total_vat_value].ToString());

                            int __tax_type = MyLib._myGlobal._intPhase(__detailRow[__rowDetail][_g.d.ic_trans_detail._tax_type].ToString());
                            int __vat_type = MyLib._myGlobal._intPhase(__detailRow[__rowDetail][_g.d.ic_trans_detail._vat_type].ToString());


                            string __insertDetail = "insert into " + _g.d.ic_trans_detail._table +
                                "(" + __fieldDetailList + ") values " +
                                "(" + __dataTransList +
                                "\'" + __itemcode + "\', \'" + __unitCode + "\', \'" + __qty.ToString() + "\', \'" + __whCode + "\', \'" + __shelfCode + "\'," +
                                "\'" + __price.ToString() + "\', \'" + __priceExcludeVat.ToString() + "\', \'" + __sumAmount.ToString() + "\'," +
                                "\'" + __discountAmount.ToString() + "\', \'" + __discountAmount.ToString() + "\', \'" + __vatValue.ToString() + "\', " +
                                "\'" + __tax_type.ToString() + "\', \'" + __vat_type.ToString() + "\') ";

                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertDetail));

                        }

                        #endregion


                        if (this._mode == 1)
                        {
                            decimal __sumCharge = 0M;
                            #region cb_trans_detail

                            string __fieldCbTransDetail = MyLib._myGlobal._fieldAndComma(
                                _g.d.cb_trans_detail._trans_flag, _g.d.cb_trans_detail._trans_type,
                                _g.d.cb_trans_detail._doc_no, _g.d.cb_trans_detail._doc_date,
                                _g.d.cb_trans_detail._doc_type,

                                _g.d.cb_trans_detail._trans_number,
                                _g.d.cb_trans_detail._no_approved, _g.d.cb_trans_detail._credit_card_type, _g.d.cb_trans_detail._charge,

                                _g.d.cb_trans_detail._bank_code, _g.d.cb_trans_detail._chq_due_date,
                                _g.d.cb_trans_detail._amount, _g.d.cb_trans_detail._sum_amount
                                );

                            string __valueCbTransDetail = _transFlag.ToString() + ", " + _transType.ToString() + ", \'" + __docNo + "\', \'" + __docDate + "\', ";

                            for (int __pay = 0; __pay < __payDetailRow.Length; __pay++)
                            {
                                int __docType = (int)__payDetailRow[__pay][_g.d.cb_trans_detail._doc_type];
                                string __transNumber = __payDetailRow[__pay][_g.d.cb_trans_detail._trans_number].ToString();
                                string __approveCode = __payDetailRow[__pay][_g.d.cb_trans_detail._approve_code].ToString();
                                string __creditCardType = __payDetailRow[__pay][_g.d.cb_trans_detail._credit_card_type].ToString();
                                decimal __charge = MyLib._myGlobal._decimalPhase(__payDetailRow[__pay][_g.d.cb_trans_detail._charge].ToString());
                                string __bankCode = __payDetailRow[__pay][_g.d.cb_trans_detail._bank_code].ToString();
                                string __chqDueDate = __payDetailRow[__pay][_g.d.cb_trans_detail._chq_due_date].ToString();

                                decimal __sumAmount = MyLib._myGlobal._decimalPhase(__payDetailRow[__pay][_g.d.cb_trans_detail._sum_amount].ToString());
                                decimal __amount = (__docType == 2) ? __sumAmount : __sumAmount - __charge;

                                __sumCharge += __charge;

                                string __insertDetail = "insert into " + _g.d.cb_trans_detail._table +
                                    "(" + __fieldCbTransDetail + ") values " +
                                    "(" + __valueCbTransDetail +

                                    __docType + "," +
                                    "\'" + __transNumber + "\'," +
                                    "\'" + __approveCode + "\', \'" + __creditCardType + "\', \'" + __charge.ToString() + "\', " +
                                    "\'" + __bankCode + "\', " + ((__chqDueDate.Length > 0) ? "\'" + __chqDueDate + "\'" : "null") + "," +
                                    "\'" + __amount + "\', \'" + __sumAmount + "\' )";

                                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertDetail));
                            }

                            #endregion

                            #region cb_trans

                            string __fieldCbTrans = MyLib._myGlobal._fieldAndComma(
                                _g.d.cb_trans._trans_flag, _g.d.cb_trans._trans_type,
                                _g.d.cb_trans._doc_no, _g.d.cb_trans._doc_date,
                                _g.d.cb_trans._total_amount, _g.d.cb_trans._cash_amount,
                                _g.d.cb_trans._total_credit_charge, _g.d.cb_trans._chq_amount, _g.d.cb_trans._card_amount,
                                _g.d.cb_trans._total_net_amount, _g.d.cb_trans._total_amount_pay
                                );

                            decimal __cashAmount = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.cb_trans._cash_amount].ToString());
                            decimal __chqAmount = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.cb_trans._chq_amount].ToString());
                            decimal __cardAmount = MyLib._myGlobal._decimalPhase(__docRow[0][_g.d.cb_trans._card_amount].ToString());

                            string __valueCbTrans = _transFlag.ToString() + ", " + _transType.ToString() + ", \'" + __docNo + "\', \'" + __docDate + "\'," +
                                "\'" + __totalAmount.ToString() + "\', \'" + __cashAmount.ToString() + "\'," +
                                "\'" + __sumCharge.ToString() + "\', \'" + __chqAmount.ToString() + "\', \'" + __cardAmount.ToString() + "\'," +
                                "\'" + (__totalAmount + __sumCharge).ToString() + "\', \'" + (__cashAmount + __chqAmount + __cardAmount).ToString() + "\'";

                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_trans._table + "(" + __fieldCbTrans + ") values (" + __valueCbTrans + ")"));

                            #endregion

                            #region Vat Sale
                            string __vatSaleField = MyLib._myGlobal._fieldAndComma(
                                _g.d.gl_journal_vat_sale._trans_flag, _g.d.gl_journal_vat_sale._trans_type,
                                _g.d.gl_journal_vat_sale._doc_no, _g.d.gl_journal_vat_sale._doc_date,

                                _g.d.gl_journal_vat_sale._vat_number, _g.d.gl_journal_vat_sale._vat_date,
                                _g.d.gl_journal_vat_sale._vat_effective_period, _g.d.gl_journal_vat_sale._vat_effective_year,
                                _g.d.gl_journal_vat_sale._base_caltax_amount, _g.d.gl_journal_vat_sale._tax_rate, _g.d.gl_journal_vat_sale._amount, _g.d.gl_journal_vat_sale._except_tax_amount
                                );

                            string __vatSaleValue = _transFlag.ToString() + ", " + _transType.ToString() + ", \'" + __docNo + "\', \'" + __docDate + "\'," +
                                "\'" + __docNo + "\', \'" + __docDate + "\'," +
                                "\'" + __docDateTime.Month.ToString() + "\', \'" + (__docDateTime.Year + 543).ToString() + "\'," +
                                "\'" + __totalBeforeVat.ToString() + "\', \'" + __vatRate.ToString() + "\', \'" + __totalVatValue.ToString() + "\', \'" + __totalExceptVat.ToString() + "\'";

                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_vat_sale._table + "(" + __vatSaleField + ") values (" + __vatSaleValue + ")"));

                            #endregion

                        }

                        // update ชื่อสินค้า
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set " +
                            " item_name = (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) " +
                            ", " + _g.d.ic_trans_detail._stand_value + " = (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                            ", " + _g.d.ic_trans_detail._divide_value + " = (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                            //", " + _g.d.ic_trans_detail._item_ty + " = (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                            " where doc_no = \'" + __docNo + "\' and (" +
                            " coalesce(item_name, '') <> (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) or " +
                            " " + _g.d.ic_trans_detail._stand_value + " <> (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) or " +
                            " " + _g.d.ic_trans_detail._divide_value + " <> (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                            " )"));

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
