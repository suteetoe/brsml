using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Json;
using System.Globalization;

namespace BRInterfaceControl.SOS
{
    public partial class _singhaOnlineOrderImport : UserControl
    {
        string _agentCode = "";
        string __url = "http://bs.brteasy.com:8080/SinghaSOInterface";
        int _transFlag = 36;
        int _transType = 2;

        public _singhaOnlineOrderImport()
        {
            InitializeComponent();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort("select * from " + _g.d.erp_company_profile._table);

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                this._agentCode = __result.Tables[0].Rows[0][_g.d.erp_company_profile._sap_code].ToString();
            }

            this._docGrid._table_name = _g.d.ic_trans._table;
            this._docGrid._addColumn("select", 11, 10, 10);
            this._docGrid._isEdit = false;
            this._docGrid._addColumn(_g.d.ic_trans._doc_no, 1, 90, 90);
            this._docGrid.WidthByPersent = true;
            this._docGrid._calcPersentWidthToScatter();
            this._docGrid._mouseClick += _docGrid__mouseClick;


            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Hide();

            this.Load += _singhaOnlineOrderImport_Load;
        }

        private void _singhaOnlineOrderImport_Load(object sender, EventArgs e)
        {
            this._getData();
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
                // var __json = __n.DownloadString("http://smldata2.smldatacenter.com:8088/SMLJavaRESTService/service/pricelist/barcodeprice?provider=datatest&dbname=smlpricecenter&branch=X01");

                /*
                StringBuilder __restParameter = new StringBuilder();
                __restParameter.Append("?item_code=" + itemCode);
                __restParameter.Append("&unit_code=" + unitCode);
                __restParameter.Append("&qty=" + qty);
                if (custCode.Length > 0)
                    __restParameter.Append("&cust_code=" + custCode);
                __restParameter.Append("&doc_date=" + docDate);
                __restParameter.Append("&sale_type=" + sale_type);

                if (vatType != _g.g._vatTypeEnum.ว่าง)
                {
                    string __vatTypeStr = "";
                    switch (vatType)
                    {
                        case _g.g._vatTypeEnum.ยกเว้นภาษี:
                            __vatTypeStr = "3";
                            break;
                        case _g.g._vatTypeEnum.ภาษีแยกนอก:
                            __vatTypeStr = "1";
                            break;
                        case _g.g._vatTypeEnum.ภาษีรวมใน:
                            __vatTypeStr = "2";
                            break;
                    }
                    __restParameter.Append("&vat_type=" + __vatTypeStr);
                }
                */
                var __json = __n.DownloadString(__url + "/SOSOrder/");

                JsonValue __jsonObject = JsonValue.Parse(__json);
                //JsonArray __jsonObject = new JsonArray(__json);
                // do other


                if (__jsonObject.Count > 0)
                {
                    for (int __row = 0; __row < __jsonObject.Count; __row++)
                    {
                        JsonValue __object = (JsonValue)__jsonObject[__row];
                        if (__object.ToString().Equals("\"success\"") == false)
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

                    string __fieldDetailList = MyLib._myGlobal._fieldAndComma(
        _g.d.ic_trans_detail._trans_flag, _g.d.ic_trans_detail._trans_type,
        _g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_time,

        _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._qty,
        _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code,

        _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._price_exclude_vat, _g.d.ic_trans_detail._sum_amount,
        _g.d.ic_trans_detail._discount_amount, _g.d.ic_trans_detail._discount, _g.d.ic_trans_detail._total_vat_value,
        _g.d.ic_trans_detail._tax_type, _g.d.ic_trans_detail._vat_type);




                    for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
                    {
                        if (this._docGrid._cellGet(__row, 0).ToString().Equals("1"))
                        {
                            string __fileName = this._docGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString().Replace("\"", string.Empty);

                            WebClient __n = new WebClient();
                            var __jsonStr = __n.DownloadString(__url + "/SOSOrder/order/" + __fileName);

                            JsonValue __json = JsonValue.Parse(__jsonStr);

                            Boolean __savePass = false;
                            StringBuilder __myQuery = new StringBuilder();
                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");



                            // do save order ic_trans

                            string __docNo = __json["doc_no"].ToString().Replace("\"", string.Empty);
                            string __docDate = DateTime.ParseExact(__json["doc_date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                            string __custCode = __json["cust_code"].ToString().Replace("\"", string.Empty);
                            string __saleCode = __json["sale_code"].ToString().Replace("\"", string.Empty);
                            string __docTime = DateTime.Now.ToString("HH:MM");

                            // for edit
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_trans where doc_no = \'" + __docNo + "\' and trans_flag = " + this._transFlag));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_trans_detail where doc_no = \'" + __docNo + "\' and trans_flag = " + this._transFlag));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from gl_journal_vat_sale where doc_no = \'" + __docNo + "\' and trans_flag = " + this._transFlag));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_shipment._table + " where doc_no = \'" + __docNo + "\' and trans_flag = " + this._transFlag));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_additional._table + " where doc_no = \'" + __docNo + "\' and trans_flag = " + this._transFlag));

                            int __saleType = MyLib._myGlobal._intPhase(__json["sale_type"].ToString().Replace("\"", string.Empty));
                            int __vatType = MyLib._myGlobal._intPhase(__json["vat_type"].ToString().Replace("\"", string.Empty));

                            string __sendDate = (__json["Delivery_date"] != null) ? DateTime.ParseExact(__json["Delivery_date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") : "1900-01-01";
                            string __soldTo = __json["ShipToAddress"].ToString().Replace("\"", string.Empty);

                            Decimal __totalValue = MyLib._myGlobal._decimalPhase(__json["total_value"].ToString().Replace("\"", string.Empty));
                            Decimal __totalAfterDiscount = 0M; // MyLib._myGlobal._decimalPhase(__json["total_after_discount"].ToString().Replace("\"", string.Empty));
                            Decimal __totalAfterVat = MyLib._myGlobal._decimalPhase(__json["total_after_vat"].ToString().Replace("\"", string.Empty));
                            Decimal __totalAmount = MyLib._myGlobal._decimalPhase(__json["total_amount"].ToString().Replace("\"", string.Empty));
                            Decimal __totalBeforeVat = MyLib._myGlobal._decimalPhase(__json["total_before_vat"].ToString().Replace("\"", string.Empty));
                            Decimal __totalDiscount = MyLib._myGlobal._decimalPhase(__json["total_discount"].ToString().Replace("\"", string.Empty));
                            Decimal __totalExceptVat = MyLib._myGlobal._decimalPhase(__json["total_except_vat"].ToString().Replace("\"", string.Empty));
                            Decimal __totalVatValue = MyLib._myGlobal._decimalPhase(__json["total_vat_value"].ToString().Replace("\"", string.Empty));
                            Decimal __vatRate = MyLib._myGlobal._decimalPhase(__json["vat_rate"].ToString().Replace("\"", string.Empty));

                            string __insertTrans = "insert into " + _g.d.ic_trans._table + "(" + __fieldTransList + ",send_type) " +
                                " values (" +
        _transFlag.ToString() + ", " + _transType.ToString() + ", \'" + __docNo + "\', \'" + __docDate + "\', \'" + __docTime + "\'," +
        "\'" + __docNo + "\', \'" + __docDate + "\'," +
        "\'" + __saleCode + "\', \'" + __custCode + "\'," +
        "\'" + (__saleType.Equals("1") ? "0" : "1") + "\',\'" + __vatType + "\'," +

        "\'" + __vatRate.ToString() + "\', \'" + __totalValue.ToString() + "\'," +
        "" + ((__sendDate.Length > 0) ? "\'" + __sendDate + "\'" : "null") + ", \'" + __totalDiscount.ToString() + "\', \'" + __totalDiscount.ToString() + "\'," +
        "\'" + __totalBeforeVat.ToString() + "\', " +
        "\'" + __totalVatValue.ToString() + "\', \'" + __totalAfterVat.ToString() + "\', " +
        "\'" + __totalExceptVat.ToString() + "\', \'" + __totalAmount.ToString() + "\'" +
        ", 299) ";

                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertTrans));

                            // shipment
                            string __shipmentField = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_shipment._doc_no, _g.d.ic_trans_shipment._doc_date, _g.d.ic_trans_shipment._trans_flag
                                ,_g.d.ic_trans_shipment._transport_name, _g.d.ic_trans_shipment._transport_address);
                            string __shipmentValue = MyLib._myGlobal._fieldAndComma("\'" + __docNo + "\'", "\'" + __docDate + "\'", this._transFlag.ToString()
                                ,"\'" + JsonLib._utils._getJsonValue(__json["ContractName"]) + "\'" , "\'" + __json["ShipToAddress"].ToString().Replace("\"", string.Empty) + "\'");

                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_shipment._table + "(" + __shipmentField + ") values(" + __shipmentValue + ")"));

                            // ic_trans_additional
                            string __addtionalField = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_additional._doc_no, _g.d.ic_trans_additional._trans_flag
                                , _g.d.ic_trans_additional._deliveryday, _g.d.ic_trans_additional._ssfquantity, _g.d.ic_trans_additional._cust_id
                                , _g.d.ic_trans_additional._custcode, _g.d.ic_trans_additional._web_order_id, _g.d.ic_trans_additional._district1_name
                                , _g.d.ic_trans_additional._district2_name, _g.d.ic_trans_additional._province_name, _g.d.ic_trans_additional._custlat
                                , _g.d.ic_trans_additional._custlng, _g.d.ic_trans_additional._custbusinesstel//, _g.d.ic_trans_additional._custemail
                                , _g.d.ic_trans_additional._orderdetail, _g.d.ic_trans_additional._ordertype//, _g.d.ic_trans_additional._custmobile
                                , _g.d.ic_trans_additional._firstorder, _g.d.ic_trans_additional._orderpaymenttype, _g.d.ic_trans_additional._deliveryaddressid
                                , _g.d.ic_trans_additional._orderdeliverydate, _g.d.ic_trans_additional._custrefcode, _g.d.ic_trans_additional._paymentdate);
                            string __addtionalValue = MyLib._myGlobal._fieldAndComma("\'" + __docNo + "\'", this._transFlag.ToString()
                                , JsonLib._utils._getJsonValueForQuery(__json["DeliveryDay"]), JsonLib._utils._getJsonValueForQuery(__json["SSFQuantity"]), JsonLib._utils._getJsonValueForQuery(__json["Cust_ID"])
                                , JsonLib._utils._getJsonValueForQuery(__json["CustCode"]), JsonLib._utils._getJsonValueForQuery(__json["web_order_id"]), JsonLib._utils._getJsonValueForQuery(__json["District1_Name"])
                                , JsonLib._utils._getJsonValueForQuery(__json["District2_Name"]), JsonLib._utils._getJsonValueForQuery(__json["Province_Name"]), JsonLib._utils._getJsonValueForQuery(__json["CustLat"])
                                , JsonLib._utils._getJsonValueForQuery(__json["CustLng"]), JsonLib._utils._getJsonValueForQuery(__json["CustBusinessTel"]) //, JsonLib._utils._getJsonValueForQuery(__json["_custemail"])
                                , JsonLib._utils._getJsonValueForQuery(__json["OrderDetail"]), JsonLib._utils._getJsonValueForQuery(__json["OrderType"]) //, JsonLib._utils._getJsonValueForQuery(__json["ShipToAddress"])
                                , JsonLib._utils._getJsonValueForQuery(__json["FirstOrder"]), JsonLib._utils._getJsonValueForQuery(__json["OrderPaymentType"]), JsonLib._utils._getJsonValueForQuery(__json["DeliveryAddressID"])
                                , JsonLib._utils._getJsonValueForQuery(__json["Delivery_date"]), JsonLib._utils._getJsonValueForQuery(__json["cust_code"]), JsonLib._utils._getJsonValueForQuery(__json["PayMentDate"])
                                );

                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_additional._table + "(" + __addtionalField + ") values(" + __addtionalValue + ")"));


                            // ic_trans_detail
                            JsonValue __items = __json["details"];
                            string __dataTransList = _transFlag.ToString() + ", " + _transType.ToString() + ", \'" + __docNo + "\', \'" + __docDate + "\', \'" + __docTime + "\',";

                            for (int __rowDetail = 0; __rowDetail < __items.Count; __rowDetail++)
                            {
                                string __itemCode = __items[__rowDetail]["item_code"].ToString().Replace("\"", string.Empty);
                                string __unitCode = __items[__rowDetail]["unit_code"].ToString().Replace("\"", string.Empty);
                                string __whCode = __items[__rowDetail]["wh_code"].ToString().Replace("\"", string.Empty);
                                string __shelfCode = __items[__rowDetail]["shelf_code"].ToString().Replace("\"", string.Empty);
                                int __isPremium = (__items[__rowDetail]["is_permium"] == null) ? 0 : MyLib._myGlobal._intPhase(__items[__rowDetail]["is_permium"].ToString().Replace("\"", string.Empty));
                                int __lineNumber = MyLib._myGlobal._intPhase(__items[__rowDetail]["line_number"].ToString().Replace("\"", string.Empty));

                                Decimal __qty = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["qty"].ToString().Replace("\"", string.Empty));
                                Decimal __price = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["price"].ToString().Replace("\"", string.Empty));
                                Decimal __priceExcludeVat = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["price_exclude_vat"].ToString().Replace("\"", string.Empty));
                                Decimal __sumAmount = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["sum_amount"].ToString().Replace("\"", string.Empty));

                                Decimal __discount_amount = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["discount_amount"].ToString().Replace("\"", string.Empty));
                                Decimal __total_vat_value = MyLib._myGlobal._decimalPhase(__items[__rowDetail]["vat_amount"].ToString().Replace("\"", string.Empty));

                                int __tax_type = MyLib._myGlobal._intPhase(__items[__rowDetail]["tax_type"].ToString().Replace("\"", string.Empty));
                                int __vat_type = MyLib._myGlobal._intPhase(__items[__rowDetail]["vat_type"].ToString().Replace("\"", string.Empty));

                                string __insertDetail = "insert into " + _g.d.ic_trans_detail._table +
                                    "(" + __fieldDetailList + ") values " +
            "(" + __dataTransList +
            "\'" + __itemCode + "\', \'" + __unitCode + "\', \'" + __qty.ToString() + "\', \'" + __whCode + "\', \'" + __shelfCode + "\'," +
            "\'" + __price.ToString() + "\', \'" + __priceExcludeVat.ToString() + "\', \'" + __sumAmount.ToString() + "\'," +
            "\'" + __discount_amount.ToString() + "\', \'" + __discount_amount.ToString() + "\', \'" + __total_vat_value.ToString() + "\', " +
            "\'" + __tax_type.ToString() + "\', \'" + __vat_type.ToString() + "\') ";

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertDetail));

                            }

                            // update ชื่อสินค้า
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set " +
                                " item_name = (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) " +
                                ", " + _g.d.ic_trans_detail._stand_value + " = (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                ", " + _g.d.ic_trans_detail._divide_value + " = (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                //", " + _g.d.ic_trans_detail._item_ty + " = (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                " where doc_no = \'" + __docNo + "\' and (" +
                                " coalesce(item_name, '') <> (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) or " +
                                " " + _g.d.ic_trans_detail._stand_value + " <> (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) or " +
                                " " + _g.d.ic_trans_detail._divide_value + " <> (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                " )"));



                            __myQuery.Append("</node>");

                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                            if (__result.Length > 0)
                            {
                                MessageBox.Show(__result, "error");
                                return;
                            }
                            else
                            {
                                __savePass = true;
                            }
                            if (__savePass)
                            {
                                // move to success
                                /*
                                string __moveResult = __n.DownloadString(__url + "/SOSOrder/move/" + __fileName);

                                if (__moveResult.Length > 0)
                                {
                                    MessageBox.Show(__moveResult);
                                }
                                */
                                // update check
                            }
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
    }
}
