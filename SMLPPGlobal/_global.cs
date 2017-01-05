using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace SMLPPGlobal
{
    public static class g
    {
        public enum _ppControlTypeEnum
        {
            ว่าง,
            PickPack,
            SaleShipment,
            Shipment,
            Shipment_Confirm
        }

        public static int _transFlagPP(_ppControlTypeEnum icTransControlType)
        {
            switch (icTransControlType)
            {
                case _ppControlTypeEnum.SaleShipment: return 44;
                case _ppControlTypeEnum.Shipment: return 1901;
                case _ppControlTypeEnum.Shipment_Confirm: return 1903;
            }

            return -1;
        }

        public static string _getAutoRun(_ppControlTypeEnum transType, string docNo, string docDate, string format, int transFlag, string tableName, string startRunningDoc)
        {
            string __getFormat = format;
            string __result = __getFormat;
            if (__getFormat.Length > 0)
            {
                DateTime __date = MyLib._myGlobal._convertDate(docDate);
                CultureInfo __dateUSZone = new CultureInfo("en-US");
                CultureInfo __dateTHZone = new CultureInfo("th-TH");
                __getFormat = __getFormat.Replace("DD", __date.ToString("dd", __dateUSZone));
                __getFormat = __getFormat.Replace("MM", __date.ToString("MM", __dateUSZone));
                __getFormat = __getFormat.Replace("YYYY", __date.ToString("yyyy", __dateUSZone));
                __getFormat = __getFormat.Replace("YY", __date.ToString("yy", __dateUSZone));
                __getFormat = __getFormat.Replace("วว", __date.ToString("dd", __dateTHZone));
                __getFormat = __getFormat.Replace("ดด", __date.ToString("MM", __dateTHZone));
                __getFormat = __getFormat.Replace("ปปปป", __date.ToString("yyyy", __dateTHZone));
                __getFormat = __getFormat.Replace("ปป", __date.ToString("yy", __dateTHZone));
                __getFormat = __getFormat.Replace("@", docNo);
                int __numberBegin = __getFormat.IndexOf("#");
                if (__numberBegin != -1)
                {
                    StringBuilder __getFormatNumber = new StringBuilder();
                    int __numberEnd = __numberBegin;
                    while (__numberEnd < __getFormat.Length && __getFormat[__numberEnd] == '#')
                    {
                        __getFormatNumber.Append("#");
                        __numberEnd++;
                    }
                    __getFormat = __getFormat.Remove(__numberBegin, __numberEnd - __numberBegin);
                    string __getDocQuery = __getFormat + "z";
                    string __qw = "";
                    switch (transType)
                    {
                        case _ppControlTypeEnum.Shipment:
                            {
                                __qw = " " + _g.d.pp_shipment._trans_flag + " = " + _transFlagPP(transType).ToString() + " and " + _g.d.pp_shipment._doc_no + " <= '" + __getDocQuery + "'";
                                __result = MyLib._myGlobal._getAutoRun(tableName, _g.d.pp_shipment._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString(), startRunningDoc).ToString();
                            }
                            break;

                    }
                }
            }
            return __result;
        }

    }
}
