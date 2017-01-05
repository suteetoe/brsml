using System;
using System.Collections.Generic;
using System.Json;
using System.Net;
using System.Text;

namespace SMLInventoryControl
{
    public class _findPriceCenter
    {

        public _icTransItemGridControl._priceStruct _findPrice(string branch, string itemCode, string barcode, string unitCode, decimal qty, string custCode, string memberCode, Boolean approve, string getUserApprove, string docDate, _g.g._vatTypeEnum vatType, decimal vatRate, int sale_type, int transport_type)
        {
            _icTransItemGridControl._priceStruct __result = new _icTransItemGridControl._priceStruct();

            WebClient __n = new WebClient();
            // var __json = __n.DownloadString("http://smldata2.smldatacenter.com:8088/SMLJavaRESTService/service/pricelist/barcodeprice?provider=datatest&dbname=smlpricecenter&branch=X01");

            string __urlServer = _g.g._companyProfile._price_list_server;
            string[] __urlServerSplit = __urlServer.Split('?');
            string __serverParameter = __urlServerSplit[1];

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

            string __url = __urlServerSplit[0] + "/SMLJavaRESTService/service/pricelist/price" + __restParameter.ToString() + ((__serverParameter.Length > 0) ? "&" + __serverParameter : "") + "&branch=" + branch;

            var __json = __n.DownloadString(__url);

            JsonValue __jsonObject = JsonValue.Parse(__json);
            // do other

            if (__jsonObject.Count > 0)
            {
                decimal __price = MyLib._myGlobal._decimalPhase(__jsonObject["price"].ToString().Replace("\"", string.Empty));

                if (__price == 0)
                {
                    return __result;
                }
                string __priceGuid = __jsonObject["price_guid"].ToString().Replace("\"", string.Empty);
                string __priceInfo = __jsonObject["price_info"].ToString().Replace("\"", string.Empty);
                __result._foundPrice = true;
                __result._type = 99;
                __result._mode = 99;
                __result._price = __price;
                __result._price_guid = __priceGuid;
                __result._price_info = __priceInfo;
            }

            return __result;
        }
    }
}
