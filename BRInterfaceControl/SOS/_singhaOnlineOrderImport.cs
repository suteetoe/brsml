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
using System.Collections;

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
                this._agentCode = __result.Tables[0].Rows[0][_g.d.erp_company_profile._agent_code].ToString();
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
                var __json = __n.DownloadString(__url + "/SOSOrder/" + "?agentcode=" + this._agentCode);

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
        _g.d.ic_trans._total_except_vat, _g.d.ic_trans._total_amount, _g.d.ic_trans._ref_doc_type);

                    string __fieldDetailList = MyLib._myGlobal._fieldAndComma(
        _g.d.ic_trans_detail._trans_flag, _g.d.ic_trans_detail._trans_type,
        _g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_time,

        _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._qty,
        _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code,

        _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._price_exclude_vat, _g.d.ic_trans_detail._sum_amount,
        _g.d.ic_trans_detail._discount_amount, _g.d.ic_trans_detail._discount, _g.d.ic_trans_detail._total_vat_value,
        _g.d.ic_trans_detail._tax_type, _g.d.ic_trans_detail._vat_type, _g.d.ic_trans_detail._item_name);

                    for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
                    {
                        if (this._docGrid._cellGet(__row, 0).ToString().Equals("1"))
                        {
                            StringBuilder __rejectMessage = new StringBuilder();
                            List<string> __itemList = new List<string>();
                            List<string> __productUnit = new List<string>();
                            Boolean __savePass = false;
                            WebClient __n = new WebClient();

                            StringBuilder __myQuery = new StringBuilder();
                            string __docNo = "";
                            string __fileName = "";
                            try
                            {
                                __fileName = this._docGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString().Replace("\"", string.Empty);
                                var __jsonStr = __n.DownloadString(__url + "/SOSOrder/order/" + __fileName + "?agentcode=" + this._agentCode);
                                JsonValue __json = JsonValue.Parse(__jsonStr);
                                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                                // do save order ic_trans

                                // check null value
                                if (__json["doc_date"] == null)
                                {
                                    __rejectMessage.AppendLine("doc_date is null");
                                }

                                if (__json["doc_time"] == null)
                                {
                                    __rejectMessage.AppendLine("doc_time is null");
                                }

                                __docNo = __json["doc_no"].ToString().Replace("\"", string.Empty);
                                string __docDate = (__json["doc_date"] != null) ? DateTime.ParseExact(__json["doc_date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") : "";
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

                                string __sendDate = (__json["Delivery_date"] != null) ? DateTime.ParseExact(__json["Delivery_date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") : __docDate;
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

                                string __insertTrans = "insert into " + _g.d.ic_trans._table + "(" + __fieldTransList + ") " +
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
            ", \'SOS\') ";

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertTrans));

                                // shipment
                                string __shipmentField = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_shipment._doc_no, _g.d.ic_trans_shipment._doc_date, _g.d.ic_trans_shipment._trans_flag
                                    , _g.d.ic_trans_shipment._transport_name, _g.d.ic_trans_shipment._transport_address
                                    , _g.d.ic_trans_shipment._latitude, _g.d.ic_trans_shipment._longitude);
                                string __shipmentValue = MyLib._myGlobal._fieldAndComma("\'" + __docNo + "\'", "\'" + __docDate + "\'", this._transFlag.ToString()
                                    , "\'" + JsonLib._utils._getJsonValue(__json["ContractName"]) + "\'", "\'" + __json["ShipToAddress"].ToString().Replace("\"", string.Empty) + "\'"
                                    , JsonLib._utils._getJsonValueForQuery(__json["CustLat"]), JsonLib._utils._getJsonValueForQuery(__json["CustLng"]));

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_shipment._table + "(" + __shipmentField + ") values(" + __shipmentValue + ")"));

                                string orderdate = __docDate + " " + __docTime; // DateTime.ParseExact( + " " + __json["doc_time"], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm");

                                // ic_trans_additional
                                string __addtionalField = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_additional._doc_no, _g.d.ic_trans_additional._trans_flag
                                    , _g.d.ic_trans_additional._deliveryday, _g.d.ic_trans_additional._ssfquantity, _g.d.ic_trans_additional._cust_id
                                    , _g.d.ic_trans_additional._custcode, _g.d.ic_trans_additional._web_order_id, _g.d.ic_trans_additional._district1_name
                                    , _g.d.ic_trans_additional._district2_name, _g.d.ic_trans_additional._province_name, _g.d.ic_trans_additional._custlat
                                    , _g.d.ic_trans_additional._custlng, _g.d.ic_trans_additional._custbusinesstel//, _g.d.ic_trans_additional._custemail
                                    , _g.d.ic_trans_additional._orderdetail, _g.d.ic_trans_additional._ordertype//, _g.d.ic_trans_additional._custmobile
                                    , _g.d.ic_trans_additional._firstorder, _g.d.ic_trans_additional._orderpaymenttype, _g.d.ic_trans_additional._deliveryaddressid
                                    , _g.d.ic_trans_additional._orderdeliverydate, _g.d.ic_trans_additional._custrefcode, _g.d.ic_trans_additional._paymentdate
                                    , _g.d.ic_trans_additional._orderdate);
                                string __addtionalValue = MyLib._myGlobal._fieldAndComma("\'" + __docNo + "\'", this._transFlag.ToString()
                                    , JsonLib._utils._getJsonValueForQuery(__json["DeliveryDay"]), JsonLib._utils._getJsonValueForQuery(__json["SSFQuantity"]), JsonLib._utils._getJsonValueForQuery(__json["Cust_ID"])
                                    , JsonLib._utils._getJsonValueForQuery(__json["CustCode"]), JsonLib._utils._getJsonValueForQuery(__json["web_order_id"]), JsonLib._utils._getJsonValueForQuery(__json["District1_Name"])
                                    , JsonLib._utils._getJsonValueForQuery(__json["District2_Name"]), JsonLib._utils._getJsonValueForQuery(__json["Province_Name"]), JsonLib._utils._getJsonValueForQuery(__json["CustLat"])
                                    , JsonLib._utils._getJsonValueForQuery(__json["CustLng"]), JsonLib._utils._getJsonValueForQuery(__json["CustBusinessTel"]) //, JsonLib._utils._getJsonValueForQuery(__json["_custemail"])
                                    , JsonLib._utils._getJsonValueForQuery(__json["OrderDetail"]), JsonLib._utils._getJsonValueForQuery(__json["OrderType"]) //, JsonLib._utils._getJsonValueForQuery(__json["ShipToAddress"])
                                    , JsonLib._utils._getJsonValueForQuery(__json["FirstOrder"]), JsonLib._utils._getJsonValueForQuery(__json["OrderPaymentType"]), JsonLib._utils._getJsonValueForQuery(__json["DeliveryAddressID"])
                                    , JsonLib._utils._getJsonValueForQuery(__json["Delivery_date"]), JsonLib._utils._getJsonValueForQuery(__json["cust_code"]), JsonLib._utils._getJsonValueForQuery(__json["PayMentDate"])
                                    , "\'" + orderdate + "\'");

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_additional._table + "(" + __addtionalField + ") values(" + __addtionalValue + ")"));


                                // ic_trans_detail
                                JsonValue __items = __json["details"];
                                string __dataTransList = _transFlag.ToString() + ", " + _transType.ToString() + ", \'" + __docNo + "\', \'" + __docDate + "\', \'" + __docTime + "\',";

                                for (int __rowDetail = 0; __rowDetail < __items.Count; __rowDetail++)
                                {
                                    // check null data
                                    StringBuilder __NullDetail = new StringBuilder();
                                    if (__items[__rowDetail]["item_code"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " item_code is null");
                                    }
                                    if (__items[__rowDetail]["unit_code"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " unit_code is null");
                                    }
                                    if (__items[__rowDetail]["wh_code"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " wh_code is null");
                                    }
                                    if (__items[__rowDetail]["shelf_code"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " shelf_code is null");
                                    }
                                    if (__items[__rowDetail]["line_number"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " line_number is null");
                                    }
                                    if (__items[__rowDetail]["qty"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " qty is null");
                                    }
                                    if (__items[__rowDetail]["price"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " price is null");
                                    }
                                    if (__items[__rowDetail]["price_exclude_vat"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " price_exclude_vat is null");
                                    }
                                    if (__items[__rowDetail]["sum_amount"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " sum_amount is null");
                                    }
                                    if (__items[__rowDetail]["discount_amount"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " discount_amount is null");
                                    }
                                    if (__items[__rowDetail]["vat_amount"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " vat_amount is null");
                                    }
                                    if (__items[__rowDetail]["tax_type"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " tax_type is null");
                                    }
                                    if (__items[__rowDetail]["vat_type"] == null)
                                    {
                                        __NullDetail.AppendLine(" row : " + __rowDetail + " vat_type is null");
                                    }


                                    if (__NullDetail.Length > 0)
                                    {
                                        __rejectMessage.AppendLine(__NullDetail.ToString());
                                    }
                                    else
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

                                        string __itemName = "";

                                        if (__isPremium == 1)
                                        {
                                            __itemName = JsonLib._utils._getJsonValue(__items[__rowDetail]["promotion"]);
                                        }
                                        else
                                        {
                                            __itemName = (__items[__rowDetail]["item_name"] == null) ? "" : __items[__rowDetail]["item_name"].ToString().Replace("\"", string.Empty);
                                        }

                                        string __insertDetail = "insert into " + _g.d.ic_trans_detail._table +
                                            "(" + __fieldDetailList + ") values " +
                    "(" + __dataTransList +
                    "\'" + __itemCode + "\', \'" + __unitCode + "\', \'" + __qty.ToString() + "\', \'" + __whCode + "\', \'" + __shelfCode + "\'," +
                    "\'" + __price.ToString() + "\', \'" + __priceExcludeVat.ToString() + "\', \'" + __sumAmount.ToString() + "\'," +
                    "\'" + __discount_amount.ToString() + "\', \'" + __discount_amount.ToString() + "\', \'" + __total_vat_value.ToString() + "\', " +
                    "\'" + __tax_type.ToString() + "\', \'" + __vat_type.ToString() + "\', \'" + __itemName + "\') ";

                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__insertDetail));

                                        if (__itemList.IndexOf(__itemCode) == -1)
                                        {
                                            __itemList.Add(__itemCode);
                                        }

                                        __productUnit.Add("(" + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' and " + _g.d.ic_unit_use._code + "=\'" + __unitCode + "\' ) ");
                                    }
                                }

                                StringBuilder __extraWhereGetDefaultWhShelf = new StringBuilder();
                                if (_g.g._companyProfile._perm_wh_shelf)
                                {
                                    // _g.d.erp_user_group_wh_shelf._screen_code
                                    __extraWhereGetDefaultWhShelf.Append(_g._icInfoFlag._icWhShelfUserPermissionWhereQuery(_g.g._transControlTypeEnum.ขาย_สั่งขาย));
                                }

                                // update ชื่อสินค้า
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set " +
                                    //" item_name = (case when is_permium = 1 then item_name else (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) end) " +
                                    "" + _g.d.ic_trans_detail._stand_value + " = (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                    ", " + _g.d.ic_trans_detail._divide_value + " = (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                    //", " + _g.d.ic_trans_detail._item_ty + " = (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                    ", wh_code = coalesce((select wh_code from ic_wh_shelf where ic_wh_shelf.ic_code =ic_trans_detail.item_code " + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + "  order by wh_code limit 1),'') " +
                                    ", shelf_code = coalesce((select shelf_code from ic_wh_shelf where ic_wh_shelf.ic_code = ic_trans_detail.item_code " + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + "  order by wh_code limit 1),'')  " +
                                    " where doc_no = \'" + __docNo + "\' and (" +
                                    " coalesce(item_name, '') <> (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) or " +
                                    " " + _g.d.ic_trans_detail._stand_value + " <> (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) or " +
                                    " " + _g.d.ic_trans_detail._divide_value + " <> (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) or  " +

                                    " wh_code <> coalesce((select wh_code from ic_wh_shelf where ic_wh_shelf.ic_code =ic_trans_detail.item_code " + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + "  order by wh_code limit 1),'') or " +
                                    " shelf_code <> coalesce((select shelf_code from ic_wh_shelf where ic_wh_shelf.ic_code = ic_trans_detail.item_code " + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + "  order by wh_code limit 1),'')   " +

                                    " )"));

                                // update ชื่อสินค้า
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set " +
                                " item_name = (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) " +
                                " WHERE  doc_no = \'" + __docNo + "\' AND length(item_name) = 0 "
                                ));

                                // update คลังที่เก็บ
                                /*
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans_detail set " +
                                    //" item_name = (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) " +
                                    //", " + _g.d.ic_trans_detail._stand_value + " = (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                    //", " + _g.d.ic_trans_detail._divide_value + " = (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                    //", " + _g.d.ic_trans_detail._item_ty + " = (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) " +
                                    ", wh_code = coalesce((select wh_code from ic_wh_shelf where ic_wh_shelf.ic_code =ic_trans_detail.item_code " + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + "  order by wh_code limit 1),'') " +
                                    ", shelf_code = coalesce((select shelf_code from ic_wh_shelf where ic_wh_shelf.ic_code = ic_trans_detail.item_code " + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + "  order by wh_code limit 1),'')  " +
                                    " where doc_no = \'" + __docNo + "\' and (" +
                                    //" coalesce(item_name, '') <> (select name_1 from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) or " +
                                    //" " + _g.d.ic_trans_detail._stand_value + " <> (select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) or " +
                                    //" " + _g.d.ic_trans_detail._divide_value + " <> (select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where ic_unit_use.code = ic_trans_detail.unit_code and ic_unit_use.ic_code = ic_trans_detail.item_code ) or  " +

                                    " wh_code <> coalesce((select wh_code from ic_wh_shelf where ic_wh_shelf.ic_code =ic_trans_detail.item_code " + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + "  order by wh_code limit 1),'') or " +
                                    " shelf_code <> coalesce((select shelf_code from ic_wh_shelf where ic_wh_shelf.ic_code = ic_trans_detail.item_code " + ((__extraWhereGetDefaultWhShelf.Length > 0) ? " and " + __extraWhereGetDefaultWhShelf.ToString() : "") + "  order by wh_code limit 1),'')   " +

                                    " )"));
                                    */

                                __myQuery.Append("</node>");

                            }
                            catch (Exception ex)
                            {
                                __rejectMessage.AppendLine("Error : " + ex.ToString());
                            }

                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();


                            // ตรวจข้อมูลก่อนทำการบันทึก
                            StringBuilder __checkQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");


                            // pack สินค้า
                            __checkQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" select code, name_1 from ic_inventory where code in (\'" + String.Join("\',\'", __itemList.ToArray()) + "\') "));

                            // pack หน่วยนับ
                            __checkQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" select ic_code, code, stand_value, divide_value from ic_unit_use where (" + String.Join(" or ", __productUnit.ToArray()) + " ) "));

                            __checkQuery.Append("</node>");

                            ArrayList __checkResutlt = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __checkQuery.ToString());
                            if (__checkResutlt.Count > 0)
                            {
                                // check สินค้า
                                DataTable __icDataTable = ((DataSet)__checkResutlt[0]).Tables[0];
                                for (int __rowCheck = 0; __rowCheck < __itemList.Count; __rowCheck++)
                                {
                                    if (__icDataTable.Rows.Count == 0)
                                    {
                                        __rejectMessage.AppendLine("Not Found item_code : " + __itemList[__rowCheck]);
                                    }
                                    else
                                    {
                                        DataRow[] __getItem = __icDataTable.Select("code = \'" + __itemList[__rowCheck] + "\'");
                                        if (__getItem.Length == 0)
                                        {
                                            __rejectMessage.AppendLine("Not Found item_code : " + __itemList[__rowCheck]);
                                        }
                                    }
                                }

                                // check Unit 
                                DataTable __unitDataTable = ((DataSet)__checkResutlt[1]).Tables[0];
                                for (int __rowCheck = 0; __rowCheck < __productUnit.Count; __rowCheck++)
                                {
                                    if (__unitDataTable.Rows.Count == 0)
                                    {
                                        __rejectMessage.AppendLine("Not Found Unit : " + __productUnit[__rowCheck].Replace("(ic_code=", "item_code:").Replace(" and code=", ",unit_code:").Replace("\' )", "\'"));
                                    }
                                    else
                                    {
                                        DataRow[] __getItem = __unitDataTable.Select(__productUnit[__rowCheck]);
                                        if (__getItem.Length == 0)
                                        {
                                            __rejectMessage.AppendLine("Not Found Unit : " + __productUnit[__rowCheck].Replace("(ic_code=", "item_code:").Replace(" and code=", ",unit_code:").Replace("\' )", "\'"));
                                        }
                                    }
                                }

                            }

                            if (__rejectMessage.Length == 0)
                            {
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

                                    string __moveResult = __n.DownloadString(__url + "/SOSOrder/move/" + __fileName + "?agentcode=" + this._agentCode);

                                    if (__moveResult.Length > 0)
                                    {
                                        MessageBox.Show(__moveResult);
                                    }

                                    // update check
                                }
                            }
                            else
                            {
                                // reject create
                                StringBuilder __logMessage = new StringBuilder();
                                __logMessage.AppendLine("Order Number : " + __docNo);

                                string __rejectResult = __n.DownloadString(__url + "/SOSOrder/reject/" + __fileName + "?agentcode=" + this._agentCode + "&message=" + __logMessage.ToString() + __rejectMessage.ToString());
                                MessageBox.Show(__logMessage.ToString() + __rejectMessage.ToString(), "Import Order Failed : " + __docNo);

                                if (__rejectResult.Length > 0)
                                {
                                    MessageBox.Show(__rejectResult);
                                }
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
