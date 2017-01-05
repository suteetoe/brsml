using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SMLProcess
{
    public class _asProcess
    {
        IFormatProvider __cultureEN = new CultureInfo("en-US");

        public _asProcess()
        {
        }

        public String _asViewDepreciateBalance(DateTime dateBegin, DateTime dateEnd, int processBy, int processMode)
        {
            return _asViewDepreciateBalance(dateBegin, dateEnd, processBy, processMode, "", "");
        }

        public String _asViewDepreciateBalance(DateTime dateBegin, DateTime dateEnd, int processBy, int processMode, string forReport, string whereReport)
        {
            // สตริงสำหรับ รีเทิร์น
            StringBuilder __result = new StringBuilder();
            __result.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __result.Append("<ResultSet>");
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // ดึงข้อมูลทั้งหมด
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + "ad." + _g.d.as_asset_detail._as_code + ",(select a." + _g.d.as_asset._name_1 + " from " + _g.d.as_asset._table + " a where ad." + _g.d.as_asset_detail._as_code + "=a." + _g.d.as_asset._code + ") as as_name," + forReport + "ad." + _g.d.as_asset_detail._as_buy_price + "," + "ad." + _g.d.as_asset_detail._as_dead_value + "," + "ad." + _g.d.as_asset_detail._as_age + "," + "ad." + _g.d.as_asset_detail._start_calc_date + "," + "ad." + _g.d.as_asset_detail._stop_calc_date + "," + "ad." + _g.d.as_asset_detail._calc_type + "," + "ad." + _g.d.as_asset_detail._as_calc_value + " from " + _g.d.as_asset_detail._table + " ad" + whereReport + " order by ad." + _g.d.as_asset_detail._as_code));
            // ดึงข้อมูลการขายสินทรัพย์
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset_sale._table + "." + _g.d.as_asset_sale._sale_date + "," + _g.d.as_asset_sale_detail._table + "." + _g.d.as_asset_sale_detail._as_code + " from " + _g.d.as_asset_sale._table + " inner join " + _g.d.as_asset_sale_detail._table + " on " + _g.d.as_asset_sale._table + "." + _g.d.as_asset_sale._doc_no + "=" + _g.d.as_asset_sale_detail._table + "." + _g.d.as_asset_sale_detail._doc_no));
            __myquery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataSet __getAssets = null;
            DataSet __getAssetSale = null; 
            // มีข้อมูลจากการคิวรี่
            if (_getData.Count > 0)
            {
                __getAssets = (DataSet)_getData[0];
                __getAssetSale = (DataSet)_getData[1];
                // จำนวนวันเริ่มต้นถึงสิ้นสุด
                DateTime __dateSelectColumnBegin = dateBegin;
                DateTime __dateSelectColumnEnd = dateEnd;
                TimeSpan __timeSpanTotal = __dateSelectColumnEnd.Subtract(__dateSelectColumnBegin);
                double __diffDateTotal = __timeSpanTotal.TotalDays;
                int __diffColumnTotal = (int)__diffDateTotal;
                // Begin And End
                string __strDateColumnBegin = MyLib._myGlobal._convertDateToQuery(__dateSelectColumnBegin);
                DateTime __dateStrColumnBegin = Convert.ToDateTime(__strDateColumnBegin);
                string __strDateColumnEnd = MyLib._myGlobal._convertDateToQuery(__dateSelectColumnEnd);
                DateTime __dateStrColumnEnd = Convert.ToDateTime(__strDateColumnEnd);
                // Index Column Asset
                int __columnCode = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset_detail._as_code);
                int __columnName = __getAssets.Tables[0].Columns.IndexOf("as_name");
                int __columnBuyPrice = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset_detail._as_buy_price);
                int __columnDeadValue = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset_detail._as_dead_value);
                int __columnBeginDate = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset_detail._start_calc_date);
                int __columnEndDate = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset_detail._stop_calc_date);
                int __columnCalcType = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset_detail._calc_type);
                int __columnAge = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset_detail._as_age);
                int __columnCalcValue = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset_detail._as_calc_value);
                // For Report  
                int __columnReport = 0;
                if (forReport.Length > 0)
                {
                    __columnReport = __getAssets.Tables[0].Columns.IndexOf("orderby_code");
                }
                // Index Column Asset Sale
                int __columnSaleDate = __getAssetSale.Tables[0].Columns.IndexOf(_g.d.as_asset_sale._sale_date);
                int __columnAsCode = __getAssetSale.Tables[0].Columns.IndexOf(_g.d.as_asset_sale_detail._as_code);
                // ยอดยกมา มูลค่ายกไป
                float __totalCome = 0;
                float __totalValueGo = 0;
                // Is Sale
                DateTime __dateEndSale = dateEnd;
                // วนลูปจำนวนสินทรัพย์
                for (int __row = 0; __row < __getAssets.Tables[0].Rows.Count; __row++)
                {
                    string __haveStartCalc = __getAssets.Tables[0].Rows[__row].ItemArray[__columnBeginDate].ToString();
                    string __haveStopCalc = __getAssets.Tables[0].Rows[__row].ItemArray[__columnEndDate].ToString();
                    float __haveAge = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnAge].ToString());
                    float __haveCalcValue = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnCalcValue].ToString());
                    string __as_name_process = __getAssets.Tables[0].Rows[__row].ItemArray[__columnCode].ToString();
                    // มีฟิล์ดที่ต้องการคำนวณครบ
                    if (__haveStartCalc.Length > 0 && __haveStopCalc.Length > 0 && __haveAge > 0 && __haveCalcValue > 0)
                    {
                        // Is Sale
                        string __yearSearchSale = "";
                        string __monthSearchSale = "";
                        string __daySearchSale = "";
                        // เปรียบเทียบเวลาเริ่มและเวลาสิ้นสุด
                        DateTime __dateColumnBegin = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnBeginDate].ToString());
                        DateTime __dateColumnEnd = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnEndDate].ToString());
                        DateTime __dateColumnBetweenBegin = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnBeginDate].ToString());
                        DateTime __dateColumnBetweenEnd = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnEndDate].ToString());
                        DateTime __dateColumnBetweenBeginAndEnd = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnBeginDate].ToString());
                        int __dateCompareBegin = DateTime.Compare(__dateStrColumnBegin, __dateColumnBetweenBegin);
                        int __dateCompareEnd = DateTime.Compare(__dateStrColumnEnd, __dateColumnBetweenEnd);
                        int __dateCompareBeginAndEnd = DateTime.Compare(__dateStrColumnEnd, __dateColumnBetweenBeginAndEnd);
                        // ดูว่าเป็นวันระหว่างปีหรือป่าว
                        string __yearColumnBeginSplit = __getAssets.Tables[0].Rows[__row].ItemArray[__columnBeginDate].ToString();
                        string[] __splitDateBegin = __yearColumnBeginSplit.Substring(0, 10).Split('-');
                        string __subYearDateBegin = __splitDateBegin[0];
                        string __subMonthDateBegin = __splitDateBegin[1];
                        string __subDayDateBegin = __splitDateBegin[2];
                        string __yearColumnEndSplit = __getAssets.Tables[0].Rows[__row].ItemArray[__columnEndDate].ToString();
                        string[] __splitDateEnd = __yearColumnEndSplit.Substring(0, 10).Split('-');
                        string __subYearDateEnd = __splitDateEnd[0];
                        string __subMonthDateEnd = __splitDateEnd[1];
                        string __subDayDateEnd = __splitDateEnd[2];
                        // ดูว่าขายไปหรือป่าว
                        for (int __rowSale = 0; __rowSale < __getAssetSale.Tables[0].Rows.Count; __rowSale++)
                        {
                            DateTime __dateColumnSaleCompare = Convert.ToDateTime(__getAssetSale.Tables[0].Rows[__rowSale].ItemArray[__columnSaleDate].ToString());
                            DateTime __dateColumnSale = MyLib._myGlobal._convertDateFromQuery(__getAssetSale.Tables[0].Rows[__rowSale].ItemArray[__columnSaleDate].ToString());
                            if (DateTime.Compare(__dateColumnBegin, __dateColumnSaleCompare) < 0 && DateTime.Compare(__dateColumnEnd, __dateColumnSaleCompare) > 0)
                            {
                                string __asCode = __getAssets.Tables[0].Rows[__row].ItemArray[__columnCode].ToString();
                                string __saleAsCode = __getAssetSale.Tables[0].Rows[__rowSale].ItemArray[__columnAsCode].ToString();
                                if (__asCode.Equals(__saleAsCode) == true)
                                {
                                    __dateEndSale = __dateColumnSale;
                                    __yearSearchSale = __dateEndSale.ToString("yyyy", __cultureEN);
                                    __monthSearchSale = __dateEndSale.ToString("yyyy-MM", __cultureEN);
                                    __daySearchSale = __dateEndSale.ToString("yyyy-MM-dd", __cultureEN);
                                }
                            }
                        }
                        // For Report
                        string __forReport = "";
                        if (forReport.Length > 0)
                        {
                            __forReport = __getAssets.Tables[0].Rows[__row].ItemArray[__columnReport].ToString();
                        }
                        // processBy 1=Day,2=Month,3=Year
                        #region Day Process
                        if (processBy == 1)
                        {
                            // คำนวณใหม่ทั้งหมด
                            DateTime __yearColumnBeginGlobal = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnBeginDate].ToString());
                            DateTime __yearColumnEndGlobal = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnEndDate].ToString());
                            TimeSpan __timeSpan = __dateColumnEnd.Subtract(__dateColumnBegin);
                            double __diffDate = __timeSpan.TotalDays;
                            int __diffTotal = (int)__diffDate;
                            if (__diffTotal >= 0)
                            {
                                float __buyPrice = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnBuyPrice].ToString());
                                float __deadValue = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnDeadValue].ToString());
                                int __calcType = (int)MyLib._myGlobal._intPhase(__getAssets.Tables[0].Rows[__row].ItemArray[__columnCalcType].ToString());
                                float __age = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnAge].ToString());
                                float __CalcValue = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnCalcValue].ToString());
                                float __calcPerDay = 0;
                                float __dataForProcess = 0;
                                float __calcZeroStartPerDay = 0;
                                float __depreciateStartPercent = 0;
                                float __beforeStartSquareRoot = (float)__deadValue / (float)__CalcValue;
                                float __yearStartForPower = (float)1 / (__age);
                                float __depreciateStartPercentPerYear = (float)1 - (float)Math.Pow(__beforeStartSquareRoot, __yearStartForPower);
                                float __dataForProcessBefore = 0;
                                float __dataForProcessAfter = 0;
                                float __depreciateStartPercentPerDayBefore = 0;
                                float __depreciateStartPercentPerDayAfter = 0;
                                // จำนวนวันในปีเริ่มต้นและจำนวนวันตั้งแต่วันแรกจนถึงวันที่เริ่มต้น
                                int __yearOfEnd = MyLib._myGlobal._intPhase(__subYearDateBegin);// -543; toe เอาออก เช็ค culture ให้ดี
                                DateTime __endOfYearBefore = new DateTime(__yearOfEnd, 12, 31);
                                DateTime __beginOfYearBefore = new DateTime(__yearOfEnd, 01, 01);
                                TimeSpan __timeSpanDateInYearBefore = __endOfYearBefore.Subtract(__beginOfYearBefore);
                                int __diffDateInYearBefore = (int)__timeSpanDateInYearBefore.TotalDays + 1;
                                TimeSpan __timeSpanBetweenBeforeDate = __endOfYearBefore.Subtract(__yearColumnBeginGlobal);
                                int __diffDateBefore = (int)__timeSpanBetweenBeforeDate.TotalDays;
                                // จำนวนวันในปีสิ้นสุดและจำนวนวันตั้งแต่วันที่สิ้นสุดจนถึงวันสิ้นปี
                                int __yearOfBegin = MyLib._myGlobal._intPhase(__subYearDateEnd);// -543; toe เอาออก เช็ค culture ให้ดี
                                DateTime __beginOfYearAfter = new DateTime(__yearOfBegin, 01, 01);
                                DateTime __endOfYearAfter = new DateTime(__yearOfBegin, 12, 31);
                                TimeSpan __timeSpanDateInYearAfter = __endOfYearAfter.Subtract(__beginOfYearAfter);
                                int __diffDateInYearAfter = (int)__timeSpanDateInYearAfter.TotalDays + 1;
                                TimeSpan __timeSpanBetweenAfterDate = __yearColumnEndGlobal.Subtract(__beginOfYearAfter);
                                int __diffDateAfter = (int)__timeSpanBetweenAfterDate.TotalDays;
                                // __calcType 0=เส้นตรง, 1=ยอดลดลง
                                // processMode 0=ตามหลักกรมสรรพากร, 1=ตามหลักการบัญชี, 2=ตามจำนวนวันจริง
                                if (__calcType == 0)
                                {
                                    if (processMode == 0)
                                    {
                                        __dataForProcess = (float)__age * 365;
                                    }
                                    else if (processMode == 1)
                                    {
                                        __dataForProcess = (float)__age * 360;
                                    }
                                    else if (processMode == 2)
                                    {
                                        __dataForProcess = (float)__diffTotal;
                                    }
                                    __calcZeroStartPerDay = (__CalcValue - __deadValue) / __dataForProcess;
                                }
                                else if (__calcType == 1)
                                {
                                    float __calcPerYearBefore = 0;
                                    float __calcPerYearAfter = 0;
                                    float __beforeSquareRoot = (float)__deadValue / __CalcValue;
                                    float __dayForPower = 0;
                                    if (processMode == 0)
                                    {
                                        __dataForProcess = (float)__age * 365;
                                        __dataForProcessBefore = (float)__diffDateBefore / 365;
                                        __calcPerYearBefore = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessBefore;
                                        __depreciateStartPercentPerDayBefore = (float)__calcPerYearBefore / 365;
                                        __dataForProcessAfter = (float)__diffDateAfter / 365;
                                        __calcPerYearAfter = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessAfter;
                                        __depreciateStartPercentPerDayAfter = (float)__calcPerYearAfter / 365;
                                    }
                                    else if (processMode == 1)
                                    {
                                        __dataForProcess = (float)__age * 360;
                                        __dataForProcessBefore = (float)__diffDateBefore / 360;
                                        __calcPerYearBefore = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessBefore;
                                        __depreciateStartPercentPerDayBefore = (float)__calcPerYearBefore / 360;
                                        __dataForProcessAfter = (float)__diffDateAfter / 360;
                                        __calcPerYearAfter = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessAfter;
                                        __depreciateStartPercentPerDayAfter = (float)__calcPerYearAfter / 360;
                                    }
                                    else if (processMode == 2)
                                    {
                                        __dataForProcess = (float)__diffTotal;
                                        __dataForProcessBefore = (float)__diffDateBefore / __diffDateInYearBefore;
                                        __calcPerYearBefore = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessBefore;
                                        __depreciateStartPercentPerDayBefore = (float)__calcPerYearBefore / __diffDateInYearBefore;
                                        __dataForProcessAfter = (float)__diffDateAfter / __diffDateInYearAfter;
                                        __calcPerYearAfter = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessAfter;
                                        __depreciateStartPercentPerDayAfter = (float)__calcPerYearAfter / __diffDateInYearAfter;
                                    }
                                    __dayForPower = (float)1 / __dataForProcess;
                                    __depreciateStartPercent = (float)1 - (float)Math.Pow(__beforeSquareRoot, __dayForPower);
                                }
                                float __balance = __CalcValue;
                                float __comeValue = 0;
                                float __netCome = 0;
                                float __netDepreciate = 0;
                                float __netValueGo = 0;
                                _assetListType[] __assetListType = new _assetListType[__diffTotal + 1];
                                for (int __date = 0; __date <= __diffTotal; __date++)
                                {
                                    // เริ่มต้นมากกว่า วันที่ 1 เดือน 1 สิ้นสุด วันที่ 31 เดือน 12
                                    if ((__subDayDateBegin.Equals("01") == false || __subMonthDateBegin.Equals("01") == false) && (__subDayDateEnd.Equals("31") == true && __subMonthDateEnd.Equals("12") == true))
                                    {
                                        if (__date <= __diffDateBefore)
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / __diffDateInYearBefore;
                                                }
                                                __calcPerDay = __calcZeroStartPerDay * __dataForProcess;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                __calcPerDay = __depreciateStartPercentPerDayBefore;
                                            }
                                        }
                                        else
                                        {
                                            if (__calcType == 0)
                                            {
                                                __calcPerDay = __calcZeroStartPerDay;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (__balance < 1)
                                                {
                                                    __calcPerDay = __comeValue - __deadValue;
                                                }
                                                else
                                                {
                                                    __calcPerDay = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                    }
                                    // สิ้นสุดน้อยกว่า วันที่ 31 เดือน 12 เริ่มต้น วันที่ 1 เดือน 1
                                    if ((__subDayDateEnd.Equals("31") == false || __subMonthDateEnd.Equals("12") == false) && (__subDayDateBegin.Equals("01") == true && __subMonthDateBegin.Equals("01") == true))
                                    {
                                        if (__date >= __diffTotal - __diffDateAfter)
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / __diffDateInYearAfter;
                                                }
                                                __calcPerDay = __calcZeroStartPerDay * __dataForProcess;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                __calcPerDay = __depreciateStartPercentPerDayAfter;
                                            }
                                        }
                                        else
                                        {
                                            if (__calcType == 0)
                                            {
                                                __calcPerDay = __calcZeroStartPerDay;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (__balance < 1)
                                                {
                                                    __calcPerDay = __comeValue - __deadValue;
                                                }
                                                else
                                                {
                                                    __calcPerDay = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                    }
                                    // เริ่มต้นมากกว่า วันที่ 1 เดือน 1 สิ้นสุดน้อยกว่า วันที่ 31 เดือน 12
                                    if ((__subDayDateBegin.Equals("01") == false || __subMonthDateBegin.Equals("01") == false) && (__subDayDateEnd.Equals("31") == false || __subMonthDateEnd.Equals("12") == false))
                                    {
                                        if (__date <= __diffDateBefore)
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / __diffDateInYearBefore;
                                                }
                                                __calcPerDay = __calcZeroStartPerDay * __dataForProcess;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                __calcPerDay = __depreciateStartPercentPerDayBefore;
                                            }
                                        }
                                        else if (__date > __diffDateBefore && __date < __diffTotal - __diffDateAfter)
                                        {
                                            if (__calcType == 0)
                                            {
                                                __calcPerDay = __calcZeroStartPerDay;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (__balance < 1)
                                                {
                                                    __calcPerDay = __comeValue - __deadValue;
                                                }
                                                else
                                                {
                                                    __calcPerDay = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / __diffDateInYearAfter;
                                                }
                                                __calcPerDay = __calcZeroStartPerDay * __dataForProcess;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                __calcPerDay = __depreciateStartPercentPerDayAfter;
                                            }
                                        }
                                    }
                                    // เริ่มต้น วันที่ 1 เดือน 1 สิ้นสุด วันที่ 31 เดือน 12
                                    if (__subDayDateBegin.Equals("01") == true && __subMonthDateBegin.Equals("01") == true && __subDayDateEnd.Equals("31") == true && __subMonthDateEnd.Equals("12") == true)
                                    {
                                        if (__calcType == 0)
                                        {
                                            __calcPerDay = __calcZeroStartPerDay;
                                        }
                                        else if (__calcType == 1)
                                        {
                                            if (__balance < 1)
                                            {
                                                __calcPerDay = __comeValue - __deadValue;
                                            }
                                            else
                                            {
                                                __calcPerDay = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                            }
                                        }
                                    }
                                    //*********************
                                    __netDepreciate += __calcPerDay;
                                    if (__date == 0)
                                    {
                                        __netCome = __CalcValue;
                                        __netValueGo = __CalcValue - __netDepreciate;
                                        __netDepreciate = 0;
                                    }
                                    else
                                    {
                                        __netCome = __netValueGo;
                                        __netValueGo = __netCome - __netDepreciate;
                                        __netDepreciate = 0;
                                    }
                                    string __codeType = __getAssets.Tables[0].Rows[__row].ItemArray[__columnCode].ToString();
                                    string __nameType = __getAssets.Tables[0].Rows[__row].ItemArray[__columnName].ToString();
                                    DateTime __dateColumnForSave = __dateColumnBegin;
                                    string __dateName = __dateColumnForSave.ToString("yyyy-MM-dd");
                                    __assetListType[__date] = new _assetListType(__codeType, __nameType, __dateName, __calcPerDay, __netCome, __netValueGo);
                                    TimeSpan __timeSpanAdd = new TimeSpan(1, 0, 0, 0);
                                    __dateColumnBegin = __dateColumnBegin.Add(__timeSpanAdd);
                                    __balance -= __calcPerDay;
                                    __comeValue += __calcPerDay;
                                }
                                // Sort
                                Array.Sort(__assetListType, new _dateCompareSort());
                                // Binary Search
                                string __dateBeginName = __dateSelectColumnBegin.ToString("yyyy-MM-dd", __cultureEN);
                                string __dateEndName = __dateSelectColumnEnd.ToString("yyyy-MM-dd", __cultureEN);
                                int __beginFound = Array.BinarySearch(__assetListType, __dateBeginName, new _dateCompareSearch());
                                int __endFound = Array.BinarySearch(__assetListType, __dateEndName, new _dateCompareSearch());
                                int __endSaleFound = Array.BinarySearch(__assetListType, __daySearchSale, new _dateCompareSearch());
                                // ประกอบร่าง ตามจำนวนวันที่จะโชว์ในกริด
                                if (__beginFound >= 0 && __endFound >= 0)
                                {
                                    __result.Append("<Row>");
                                    __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Code) + "</as_code>");
                                    __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Name) + "</as_name>");
                                    __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                    __totalCome = __assetListType[__beginFound].Come;
                                    __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                    int dayCount = 0;
                                    float sumResult = 0;
                                    for (int f = __beginFound; f <= __endFound; f++)
                                    {
                                        if (__endSaleFound > 0 && __endSaleFound < __endFound)
                                        {
                                            if (f > __endSaleFound)
                                            {
                                                __result.Append("<c" + dayCount + ">0</c" + dayCount + ">");
                                                dayCount++;
                                            }
                                            else
                                            {
                                                __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                                sumResult += __assetListType[f].Result;
                                                dayCount++;
                                            }
                                        }
                                        else
                                        {
                                            __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                            sumResult += __assetListType[f].Result;
                                            dayCount++;
                                        }
                                    }
                                    __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                    if (__endSaleFound > 0)
                                    {
                                        if (__endSaleFound >= __endFound)
                                        {
                                            __totalValueGo = __assetListType[__endFound].ValueGo;
                                        }
                                        else
                                        {
                                            __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                        }
                                    }
                                    else
                                    {
                                        __totalValueGo = __assetListType[__endFound].ValueGo;
                                    }
                                    __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                    if (forReport.Length > 0)
                                    {
                                        __result.Append("<as_report>" + __forReport + "</as_report>");
                                    }
                                    __result.Append("</Row>");
                                }
                                else
                                {
                                    // วันเริ่มต้นที่เลือก น้อยกว่าเท่ากับ วันเริ่มต้นคำนวณ และ วันสิ้นสุดที่เลือก มากกว่าเท่ากับ วันที่สิ้นสุดคำนวณ
                                    if (__dateCompareBegin <= 0 && __dateCompareEnd >= 0 && __dateCompareBeginAndEnd >= 0)
                                    {
                                        string __dateBetweenBeginName = __dateColumnBetweenBegin.ToString("yyyy-MM-dd");
                                        string __dateBetweenEndName = __dateColumnBetweenEnd.ToString("yyyy-MM-dd");
                                        int __beginBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenBeginName, new _dateCompareSearch());
                                        int __endBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenEndName, new _dateCompareSearch());
                                        TimeSpan __timeSpanBetween = __dateColumnBetweenBegin.Subtract(__dateStrColumnBegin);
                                        double __diffDateBetween = __timeSpanBetween.TotalDays;
                                        int __diffBetween = (int)__diffDateBetween;
                                        int __diffDataBetween = __endBetweenFound - __beginBetweenFound;
                                        if (__diffDataBetween == 0)
                                        {
                                            __diffDataBetween = 1;
                                        }
                                        else
                                        {
                                            __diffDataBetween = __diffDataBetween + 1;
                                        }
                                        TimeSpan __timeSpanBetweenEnd = __dateStrColumnEnd.Subtract(__dateColumnBetweenEnd);
                                        double __diffDateBetweenEnd = __timeSpanBetweenEnd.TotalDays;
                                        int __diffBetweenEnd = (int)__diffDateBetweenEnd;
                                        __result.Append("<Row>");
                                        __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Code) + "</as_code>");
                                        __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Name) + "</as_name>");
                                        __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                        __totalCome = __assetListType[__beginBetweenFound].Come;
                                        __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                        int dayCount = __beginBetweenFound;
                                        float sumResult = 0;
                                        for (int fs = 0; fs < __diffBetween; fs++)
                                        {
                                            __result.Append("<c" + fs + ">0</c" + fs + ">");
                                        }
                                        for (int f = __diffBetween; f < __diffBetween + __diffDataBetween; f++)
                                        {
                                            if (__endSaleFound > 0)
                                            {
                                                if (f > __endSaleFound + __diffBetween)
                                                {
                                                    __result.Append("<c" + f + ">0</c" + f + ">");
                                                }
                                                else
                                                {
                                                    __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                    sumResult += __assetListType[dayCount].Result;
                                                    dayCount++;
                                                }
                                            }
                                            else
                                            {
                                                __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                sumResult += __assetListType[dayCount].Result;
                                                dayCount++;
                                            }
                                        }
                                        for (int fe = __diffBetween + __diffDataBetween; fe <= __diffBetween + __diffDataBetween + __diffBetweenEnd; fe++)
                                        {
                                            __result.Append("<c" + fe + ">0</c" + fe + ">");
                                        }
                                        __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                        if (__endSaleFound > 0)
                                        {
                                            if (__endSaleFound >= __endBetweenFound)
                                            {
                                                __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                            }
                                            else
                                            {
                                                __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                            }
                                        }
                                        else
                                        {
                                            __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                        }
                                        __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                        if (forReport.Length > 0)
                                        {
                                            __result.Append("<as_report>" + __forReport + "</as_report>");
                                        }
                                        __result.Append("</Row>");
                                    }
                                    else if (__dateCompareBegin <= 0 && __dateCompareEnd <= 0 && __dateCompareBeginAndEnd >= 0)
                                    {
                                        string __dateBetweenBeginName = __dateColumnBetweenBegin.ToString("yyyy-MM-dd");
                                        int __beginBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenBeginName, new _dateCompareSearch());
                                        TimeSpan __timeSpanBetween = __dateColumnBetweenBegin.Subtract(__dateStrColumnBegin);
                                        double __diffDateBetween = __timeSpanBetween.TotalDays;
                                        int __diffBetween = (int)__diffDateBetween;
                                        TimeSpan __timeSpanBetweenEnd = __dateStrColumnEnd.Subtract(__dateColumnBetweenBegin);
                                        double __diffDateBetweenEnd = __timeSpanBetweenEnd.TotalDays;
                                        int __diffBetweenEnd = (int)__diffDateBetweenEnd;
                                        __result.Append("<Row>");
                                        __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Code) + "</as_code>");
                                        __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Name) + "</as_name>");
                                        __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                        __totalCome = __assetListType[__beginBetweenFound].Come;
                                        __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                        int dayCount = __beginBetweenFound;
                                        float sumResult = 0;
                                        for (int fs = 0; fs < __diffBetween; fs++)
                                        {
                                            __result.Append("<c" + fs + ">0</c" + fs + ">");
                                        }
                                        for (int f = __diffBetween; f <= __diffBetween + __diffBetweenEnd + __beginBetweenFound; f++)
                                        {
                                            if (__endSaleFound > 0)
                                            {
                                                if (f > __endSaleFound + __diffBetween)
                                                {
                                                    __result.Append("<c" + f + ">0</c" + f + ">");
                                                }
                                                else
                                                {
                                                    __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                    sumResult += __assetListType[dayCount].Result;
                                                    dayCount++;
                                                }
                                            }
                                            else
                                            {
                                                __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                sumResult += __assetListType[dayCount].Result;
                                                dayCount++;
                                            }
                                        }
                                        __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                        if (__endSaleFound > 0)
                                        {
                                            if (__endSaleFound >= __beginBetweenFound + __diffBetweenEnd)
                                            {
                                                __totalValueGo = __assetListType[__beginBetweenFound + __diffBetweenEnd].ValueGo;
                                            }
                                            else
                                            {
                                                __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                            }
                                        }
                                        else
                                        {
                                            __totalValueGo = __assetListType[__beginBetweenFound + __diffBetweenEnd].ValueGo;
                                        }
                                        __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                        if (forReport.Length > 0)
                                        {
                                            __result.Append("<as_report>" + __forReport + "</as_report>");
                                        }
                                        __result.Append("</Row>");
                                    }
                                    else
                                    {
                                        if (__beginFound > 0)
                                        {
                                            string __dateBetweenEndName = __dateColumnBetweenEnd.ToString("yyyy-MM-dd");
                                            int __endBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenEndName, new _dateCompareSearch());
                                            TimeSpan __timeSpanBetweenEnd = __dateStrColumnEnd.Subtract(__dateColumnBetweenEnd);
                                            double __diffDateBetweenEnd = __timeSpanBetweenEnd.TotalDays;
                                            int __diffBetweenEnd = (int)__diffDateBetweenEnd;
                                            __result.Append("<Row>");
                                            __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Code) + "</as_code>");
                                            __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Name) + "</as_name>");
                                            __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                            __totalCome = __assetListType[__beginFound].Come;
                                            __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                            int dayCount = 0;
                                            float sumResult = 0;
                                            for (int f = __beginFound; f <= __endBetweenFound; f++)
                                            {
                                                if (__endSaleFound > 0)
                                                {
                                                    if (f > __endSaleFound)
                                                    {
                                                        __result.Append("<c" + dayCount + ">0</c" + dayCount + ">");
                                                        dayCount++;
                                                    }
                                                    else
                                                    {
                                                        __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                                        sumResult += __assetListType[f].Result;
                                                        dayCount++;
                                                    }
                                                }
                                                else
                                                {
                                                    __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                                    sumResult += __assetListType[f].Result;
                                                    dayCount++;
                                                }
                                            }
                                            for (int fe = 0; fe < __diffBetweenEnd; fe++)
                                            {
                                                __result.Append("<c" + dayCount + ">0</c" + dayCount + ">");
                                                dayCount++;
                                            }
                                            __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                            if (__endSaleFound > 0)
                                            {
                                                if (__endSaleFound >= __endBetweenFound)
                                                {
                                                    __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                                }
                                                else
                                                {
                                                    __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                                }
                                            }
                                            else
                                            {
                                                __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                            }
                                            __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                            if (forReport.Length > 0)
                                            {
                                                __result.Append("<as_report>" + __forReport + "</as_report>");
                                            }
                                            __result.Append("</Row>");
                                        }
                                    }
                                }
                            }// Close More Than 0
                        }
                        #endregion
                        #region Process By Month
                        else if (processBy == 2)
                        {
                            // คำนวณใหม่ทั้งหมด
                            DateTime __monthColumnBeginGlobal = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnBeginDate].ToString());
                            DateTime __monthColumnEndGlobal = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnEndDate].ToString());
                            TimeSpan __timeSpanMonth = __dateColumnEnd.Subtract(__dateColumnBegin);
                            double __diffDateInMonth = __timeSpanMonth.TotalDays;
                            int __diffTotalInMonth = (int)__diffDateInMonth;
                            //*********************
                            int __monthsApart = 12 * (__monthColumnEndGlobal.Year - __monthColumnBeginGlobal.Year) + __monthColumnEndGlobal.Month - __monthColumnBeginGlobal.Month;
                            if (__monthsApart >= 0)
                            {
                                float __buyPrice = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnBuyPrice].ToString());
                                float __deadValue = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnDeadValue].ToString());
                                float __age = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnAge].ToString());
                                int __calcType = (int)MyLib._myGlobal._intPhase(__getAssets.Tables[0].Rows[__row].ItemArray[__columnCalcType].ToString());
                                float __CalcValue = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnCalcValue].ToString());
                                float __calcPerMonth = 0;
                                float __calcZeroStartPerMonth = (float)(__CalcValue - __deadValue) / (__age * 12);
                                float __beforeStartSquareRoot = (float)__deadValue / (float)__CalcValue;
                                float __monthStartForPower = (float)1 / (__age * 12);
                                float __depreciateStartPercent = (float)1 - (float)Math.Pow(__beforeStartSquareRoot, __monthStartForPower);
                                float __yearStartForPower = (float)1 / (__age);
                                float __depreciateStartPercentPerYear = (float)1 - (float)Math.Pow(__beforeStartSquareRoot, __yearStartForPower);
                                float __dataForProcess = 0;
                                float __dataForProcessBefore = 0;
                                float __dataForProcessAfter = 0;
                                float __calcZeroStartPerDay = 0;
                                float __depreciateStartPercentPerDayBefore = 0;
                                float __depreciateStartPercentPerDayAfter = 0;
                                float __calcPerDay = 0;
                                int __daysInMonth = 0;
                                int __monthPlusBefore = MyLib._myGlobal._intPhase(__subMonthDateBegin);
                                int __monthPlusAfter = MyLib._myGlobal._intPhase(__subMonthDateBegin);
                                int __monthMinusBefore = MyLib._myGlobal._intPhase(__subMonthDateEnd);
                                int __monthMinusAfter = MyLib._myGlobal._intPhase(__subMonthDateEnd);
                                int __monthMinusAfterFirst = 1;
                                float __balance = __CalcValue;
                                float __comeValue = 0;
                                float __netCome = 0;
                                float __netDepreciate = 0;
                                float __netValueGo = 0;
                                // จำนวนวันในปีเริ่มต้นและจำนวนวันตั้งแต่วันแรกจนถึงวันที่เริ่มต้น
                                int __yearOfEnd = MyLib._myGlobal._intPhase(__subYearDateBegin); // toe เอาออก -543;
                                DateTime __endOfYearBefore = new DateTime(__yearOfEnd, 12, 31);
                                DateTime __beginOfYearBefore = new DateTime(__yearOfEnd, 01, 01);
                                TimeSpan __timeSpanDateInYearBefore = __endOfYearBefore.Subtract(__beginOfYearBefore);
                                int __diffDateInYearBefore = (int)__timeSpanDateInYearBefore.TotalDays + 1;
                                TimeSpan __timeSpanBetweenBeforeDate = __endOfYearBefore.Subtract(__monthColumnBeginGlobal);
                                int __diffDateBefore = (int)__timeSpanBetweenBeforeDate.TotalDays;
                                // จำนวนวันในปีสิ้นสุดและจำนวนวันตั้งแต่วันที่สิ้นสุดจนถึงวันสิ้นปี
                                int __yearOfBegin = MyLib._myGlobal._intPhase(__subYearDateEnd); // toe เอกออก -543;
                                DateTime __beginOfYearAfter = new DateTime(__yearOfBegin, 01, 01);
                                DateTime __endOfYearAfter = new DateTime(__yearOfBegin, 12, 31);
                                TimeSpan __timeSpanDateInYearAfter = __endOfYearAfter.Subtract(__beginOfYearAfter);
                                int __diffDateInYearAfter = (int)__timeSpanDateInYearAfter.TotalDays + 1;
                                TimeSpan __timeSpanBetweenAfterDate = __monthColumnEndGlobal.Subtract(__beginOfYearAfter);
                                int __diffDateAfter = (int)__timeSpanBetweenAfterDate.TotalDays;
                                // __calcType 0=เส้นตรง, 1=ยอดลดลง
                                // processMode 0=ตามหลักกรมสรรพากร, 1=ตามหลักการบัญชี, 2=ตามจำนวนวันจริง
                                if (__calcType == 0)
                                {
                                    if (processMode == 0)
                                    {
                                        __dataForProcess = (float)__age * 365;
                                    }
                                    else if (processMode == 1)
                                    {
                                        __dataForProcess = (float)__age * 360;
                                    }
                                    else if (processMode == 2)
                                    {
                                        __dataForProcess = (float)__diffTotalInMonth;
                                    }
                                    __calcZeroStartPerDay = (__CalcValue - __deadValue) / __dataForProcess;
                                }
                                else if (__calcType == 1)
                                {
                                    float __calcPerYearBefore = 0;
                                    float __calcPerYearAfter = 0;
                                    if (processMode == 0)
                                    {
                                        __dataForProcessBefore = (float)__diffDateBefore / 365;
                                        __calcPerYearBefore = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessBefore;
                                        __depreciateStartPercentPerDayBefore = (float)__calcPerYearBefore / 365;
                                        __dataForProcessAfter = (float)__diffDateAfter / 365;
                                        __calcPerYearAfter = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessAfter;
                                        __depreciateStartPercentPerDayAfter = (float)__calcPerYearAfter / 365;
                                    }
                                    else if (processMode == 1)
                                    {
                                        __dataForProcessBefore = (float)__diffDateBefore / 360;
                                        __calcPerYearBefore = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessBefore;
                                        __depreciateStartPercentPerDayBefore = (float)__calcPerYearBefore / 360;
                                        __dataForProcessAfter = (float)__diffDateAfter / 360;
                                        __calcPerYearAfter = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessAfter;
                                        __depreciateStartPercentPerDayAfter = (float)__calcPerYearAfter / 360;
                                    }
                                    else if (processMode == 2)
                                    {
                                        __dataForProcessBefore = (float)__diffDateBefore / __diffDateInYearBefore;
                                        __calcPerYearBefore = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessBefore;
                                        __depreciateStartPercentPerDayBefore = (float)__calcPerYearBefore / __diffDateInYearBefore;
                                        __dataForProcessAfter = (float)__diffDateAfter / __diffDateInYearAfter;
                                        __calcPerYearAfter = __CalcValue * __depreciateStartPercentPerYear * __dataForProcessAfter;
                                        __depreciateStartPercentPerDayAfter = (float)__calcPerYearAfter / __diffDateInYearAfter;
                                    }
                                }
                                _assetListType[] __assetListType = new _assetListType[__monthsApart + 1];
                                Calendar cal = CultureInfo.CurrentCulture.DateTimeFormat.Calendar;
                                for (int __month = 0; __month <= __monthsApart; __month++)
                                {
                                    // ไม่ได้เริ่มต้น วันที่ 1/1 แต่สิ้นสุดวันที่ 31/12
                                    if ((__subDayDateBegin.Equals("01") == false || __subMonthDateBegin.Equals("01") == false) && (__subDayDateEnd.Equals("31") == true && __subMonthDateEnd.Equals("12") == true))
                                    {
                                        if (__month + MyLib._myGlobal._intPhase(__subMonthDateBegin) <= 12)
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / __diffDateInYearBefore;
                                                }
                                                // ถ้าเป็นระหว่างปี ให้เอา จำนวนวันในเดือนนั้น คูณ ค่าเสื่อมที่คิดเป็นรายวัน
                                                if (__monthPlusBefore < 13)
                                                {
                                                    __daysInMonth = DateTime.DaysInMonth(MyLib._myGlobal._intPhase(__subYearDateBegin), __monthPlusBefore);
                                                    __calcPerDay = __calcZeroStartPerDay * __dataForProcess;
                                                    if (__subDayDateBegin.Equals("01") == false && __month == 0)
                                                    {
                                                        __calcPerMonth = (float)(__daysInMonth - MyLib._myGlobal._intPhase(__subDayDateBegin)) * __calcPerDay;
                                                    }
                                                    else
                                                    {
                                                        __calcPerMonth = (float)__daysInMonth * __calcPerDay;
                                                    }
                                                    __monthPlusBefore++;
                                                }
                                                else
                                                {
                                                    __calcPerMonth = __calcZeroStartPerMonth;
                                                }
                                            }
                                            else if (__calcType == 1)
                                            {
                                                // ถ้าเป็นระหว่างปี ให้เอา จำนวนวันในเดือนนั้น คูณ ค่าเสื่อมที่คิดเป็นรายวัน
                                                if (__monthPlusBefore < 13)
                                                {
                                                    __daysInMonth = DateTime.DaysInMonth(MyLib._myGlobal._intPhase(__subYearDateBegin), __monthPlusBefore);
                                                    __calcPerDay = __depreciateStartPercentPerDayBefore;
                                                    if (__subDayDateBegin.Equals("01") == false && __month == 0)
                                                    {
                                                        __calcPerMonth = (float)(__daysInMonth - MyLib._myGlobal._intPhase(__subDayDateBegin)) * __calcPerDay;
                                                    }
                                                    else
                                                    {
                                                        __calcPerMonth = (float)__daysInMonth * __calcPerDay;
                                                    }
                                                    __monthPlusBefore++;
                                                }
                                                else
                                                {
                                                    __calcPerMonth = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (__calcType == 0)
                                            {
                                                __calcPerMonth = __calcZeroStartPerMonth;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (__balance < 1)
                                                {
                                                    __calcPerMonth = __comeValue - __deadValue;
                                                }
                                                else
                                                {
                                                    __calcPerMonth = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                    }
                                    // เริ่มต้น วันที่ 1/1 และ ไม่ได้สิ้นสุด ในวันที่ 31/12
                                    if ((__subDayDateEnd.Equals("31") == false || __subMonthDateEnd.Equals("12") == false) && (__subDayDateBegin.Equals("01") == true && __subMonthDateBegin.Equals("01") == true))
                                    {
                                        if (__month > (__monthsApart - MyLib._myGlobal._intPhase(__subMonthDateEnd)))
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / __diffDateInYearAfter;
                                                }
                                                // ถ้าเป็นระหว่างปี ให้เอา จำนวนวันในเดือนนั้น คูณ ค่าเสื่อมที่คิดเป็นรายวัน
                                                if (__monthMinusAfter > 0)
                                                {
                                                    __daysInMonth = DateTime.DaysInMonth(MyLib._myGlobal._intPhase(__subYearDateEnd), __monthMinusAfterFirst);
                                                    __calcPerDay = __calcZeroStartPerDay * __dataForProcess;
                                                    if (__subDayDateEnd.Equals("01") == false && __month == __monthsApart)
                                                    {
                                                        __calcPerMonth = (float)MyLib._myGlobal._intPhase(__subDayDateEnd) * __calcPerDay;
                                                    }
                                                    else
                                                    {
                                                        __calcPerMonth = (float)__daysInMonth * __calcPerDay;
                                                    }
                                                    __monthMinusAfter--;
                                                    __monthMinusAfterFirst++;
                                                }
                                                else
                                                {
                                                    __calcPerMonth = __calcZeroStartPerMonth;
                                                }
                                            }
                                            else if (__calcType == 1)
                                            {
                                                // ถ้าเป็นระหว่างปี ให้เอา จำนวนวันในเดือนนั้น คูณ ค่าเสื่อมที่คิดเป็นรายวัน
                                                if (__monthMinusAfter > 0)
                                                {
                                                    __daysInMonth = DateTime.DaysInMonth(MyLib._myGlobal._intPhase(__subYearDateEnd), __monthMinusAfterFirst);
                                                    __calcPerDay = __depreciateStartPercentPerDayAfter;
                                                    if (__subDayDateEnd.Equals("01") == false && __month == __monthsApart)
                                                    {
                                                        __calcPerMonth = (float)MyLib._myGlobal._intPhase(__subDayDateEnd) * __calcPerDay;
                                                    }
                                                    else
                                                    {
                                                        __calcPerMonth = (float)__daysInMonth * __calcPerDay;
                                                    }
                                                    __monthMinusAfter--;
                                                    __monthMinusAfterFirst++;
                                                }
                                                else
                                                {
                                                    __calcPerMonth = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (__calcType == 0)
                                            {
                                                __calcPerMonth = __calcZeroStartPerMonth;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (__balance < 1)
                                                {
                                                    __calcPerMonth = __comeValue - __deadValue;
                                                }
                                                else
                                                {
                                                    __calcPerMonth = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                    }
                                    // ไม่ได้เริ่มต้น 1/1 และไม่ได้สิ้นสุด 31/12
                                    if ((__subDayDateBegin.Equals("01") == false || __subMonthDateBegin.Equals("01") == false) && (__subDayDateEnd.Equals("31") == false || __subMonthDateEnd.Equals("12") == false))
                                    {
                                        if (__month + MyLib._myGlobal._intPhase(__subMonthDateBegin) <= 12)
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / __diffDateInYearBefore;
                                                }
                                                // ถ้าเป็นระหว่างปี ให้เอา จำนวนวันในเดือนนั้น คูณ ค่าเสื่อมที่คิดเป็นรายวัน
                                                if (__monthPlusBefore < 13)
                                                {
                                                    __daysInMonth = DateTime.DaysInMonth(MyLib._myGlobal._intPhase(__subYearDateBegin), __monthPlusBefore);
                                                    __calcPerDay = __calcZeroStartPerDay;// toe __calcPerDay = __calcZeroStartPerDay * __dataForProcess;
                                                    if (__subDayDateBegin.Equals("01") == false && __month == 0)
                                                    {
                                                        __calcPerMonth = (float)((__daysInMonth - MyLib._myGlobal._intPhase(__subDayDateBegin)) + 1) * __calcPerDay;
                                                    }
                                                    else
                                                    {
                                                        __calcPerMonth = (float)__daysInMonth * __calcPerDay;
                                                    }
                                                    __monthPlusBefore++;
                                                }
                                            }
                                            else if (__calcType == 1)
                                            {
                                                // ถ้าเป็นระหว่างปี ให้เอา จำนวนวันในเดือนนั้น คูณ ค่าเสื่อมที่คิดเป็นรายวัน
                                                if (__monthPlusBefore < 13)
                                                {
                                                    __daysInMonth = DateTime.DaysInMonth(MyLib._myGlobal._intPhase(__subYearDateBegin), __monthPlusBefore);
                                                    __calcPerDay = __depreciateStartPercentPerDayBefore;
                                                    if (__subDayDateBegin.Equals("01") == false && __month == 0)
                                                    {
                                                        __calcPerMonth = (float)(__daysInMonth - MyLib._myGlobal._intPhase(__subDayDateBegin)) * __calcPerDay;
                                                    }
                                                    else
                                                    {
                                                        __calcPerMonth = (float)__daysInMonth * __calcPerDay;
                                                    }
                                                    __monthPlusBefore++;
                                                }
                                            }
                                        }
                                        else if (__month + MyLib._myGlobal._intPhase(__subMonthDateBegin) > 12 && __month <= (__monthsApart - MyLib._myGlobal._intPhase(__subMonthDateEnd)))
                                        {
                                            if (__calcType == 0)
                                            {
                                                __calcPerMonth = __calcZeroStartPerMonth;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (__balance < 1)
                                                {
                                                    __calcPerMonth = __comeValue - __deadValue;
                                                }
                                                else
                                                {
                                                    __calcPerMonth = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / __diffDateInYearAfter;
                                                }
                                                // ถ้าเป็นระหว่างปี ให้เอา จำนวนวันในเดือนนั้น คูณ ค่าเสื่อมที่คิดเป็นรายวัน
                                                if (__monthMinusAfter > 0)
                                                {
                                                    __daysInMonth = DateTime.DaysInMonth(MyLib._myGlobal._intPhase(__subYearDateEnd), __monthMinusAfterFirst);
                                                    __calcPerDay = __calcZeroStartPerDay;// toe__calcPerDay = __calcZeroStartPerDay * __dataForProcess;
                                                    if (__subDayDateEnd.Equals("01") == false && __month == __monthsApart)
                                                    {
                                                        __calcPerMonth = (float)MyLib._myGlobal._intPhase(__subDayDateEnd) * __calcPerDay;
                                                    }
                                                    else
                                                    {
                                                        __calcPerMonth = (float)__daysInMonth * __calcPerDay;
                                                    }
                                                    __monthMinusAfter--;
                                                    __monthMinusAfterFirst++;
                                                }
                                                else
                                                {
                                                    __calcPerMonth = __calcZeroStartPerMonth;
                                                }
                                            }
                                            else if (__calcType == 1)
                                            {
                                                // ถ้าเป็นระหว่างปี ให้เอา จำนวนวันในเดือนนั้น คูณ ค่าเสื่อมที่คิดเป็นรายวัน
                                                if (__monthMinusAfter > 0)
                                                {
                                                    __daysInMonth = DateTime.DaysInMonth(MyLib._myGlobal._intPhase(__subYearDateEnd), __monthMinusAfterFirst);
                                                    __calcPerDay = __depreciateStartPercentPerDayAfter;
                                                    if (__subDayDateEnd.Equals("01") == false && __month == __monthsApart)
                                                    {
                                                        __calcPerMonth = (float)MyLib._myGlobal._intPhase(__subDayDateEnd) * __calcPerDay;
                                                    }
                                                    else
                                                    {
                                                        __calcPerMonth = (float)__daysInMonth * __calcPerDay;
                                                    }
                                                    __monthMinusAfter--;
                                                    __monthMinusAfterFirst++;
                                                }
                                                else
                                                {
                                                    __calcPerMonth = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                    }
                                    // เริ่มต้น วันที่ 1/1 สิ้นสุด วันที่ 31/12
                                    if (__subDayDateBegin.Equals("01") == true && __subMonthDateBegin.Equals("01") == true && __subDayDateEnd.Equals("31") == true && __subMonthDateEnd.Equals("12") == true)
                                    {
                                        if (__calcType == 0)
                                        {
                                            __calcPerMonth = __calcZeroStartPerMonth;
                                        }
                                        else if (__calcType == 1)
                                        {
                                            if (__balance < 1)
                                            {
                                                __calcPerMonth = __comeValue - __deadValue;
                                            }
                                            else
                                            {
                                                __calcPerMonth = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                            }
                                        }
                                    }
                                    //*********************
                                    __netDepreciate += __calcPerMonth;
                                    if (__month == 0)
                                    {
                                        __netCome = __CalcValue;
                                        __netValueGo = __CalcValue - __netDepreciate;
                                        __netDepreciate = 0;
                                    }
                                    else
                                    {
                                        __netCome = __netValueGo;
                                        __netValueGo = __netCome - __netDepreciate;
                                        __netDepreciate = 0;
                                    }
                                    string __codeType = __getAssets.Tables[0].Rows[__row].ItemArray[__columnCode].ToString();
                                    string __nameType = __getAssets.Tables[0].Rows[__row].ItemArray[__columnName].ToString();
                                    DateTime __monthColumnForSave = __dateColumnBegin;
                                    string __monthName = __monthColumnForSave.ToString("yyyy-MM");
                                    __assetListType[__month] = new _assetListType(__codeType, __nameType, __monthName, __calcPerMonth, __netCome, __netValueGo);
                                    __dateColumnBegin = cal.AddMonths(__dateColumnBegin, 1);
                                    __balance -= __calcPerMonth;
                                    __comeValue += __calcPerMonth;
                                }
                                // Sort
                                Array.Sort(__assetListType, new _dateCompareSort());
                                // Binary Search
                                string __monthBeginName = __dateSelectColumnBegin.ToString("yyyy-MM", __cultureEN);
                                string __monthEndName = __dateSelectColumnEnd.ToString("yyyy-MM", __cultureEN);
                                int __beginFound = Array.BinarySearch(__assetListType, __monthBeginName, new _dateCompareSearch());
                                int __endFound = Array.BinarySearch(__assetListType, __monthEndName, new _dateCompareSearch());
                                int __endSaleFound = Array.BinarySearch(__assetListType, __monthSearchSale, new _dateCompareSearch());
                                // ประกอบร่าง ตามจำนวนเดือนที่จะโชว์ในกริด
                                if (__beginFound >= 0 && __endFound >= 0)
                                {
                                    __result.Append("<Row>");
                                    __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Code) + "</as_code>");
                                    __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Name) + "</as_name>");
                                    __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                    __totalCome = __assetListType[__beginFound].Come;
                                    __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                    int dayCount = 0;
                                    float sumResult = 0;
                                    for (int f = __beginFound; f <= __endFound; f++)
                                    {
                                        if (__endSaleFound > 0)
                                        {
                                            if (f > __endSaleFound)
                                            {
                                                __result.Append("<c" + dayCount + ">0</c" + dayCount + ">");
                                                dayCount++;
                                            }
                                            else
                                            {
                                                __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                                sumResult += __assetListType[f].Result;
                                                dayCount++;
                                            }
                                        }
                                        else
                                        {
                                            __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                            sumResult += __assetListType[f].Result;
                                            dayCount++;
                                        }
                                    }
                                    __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                    if (__endSaleFound > 0)
                                    {
                                        if (__endSaleFound >= __endFound)
                                        {
                                            __totalValueGo = __assetListType[__endFound].ValueGo;
                                        }
                                        else
                                        {
                                            __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                        }
                                    }
                                    else
                                    {
                                        __totalValueGo = __assetListType[__endFound].ValueGo;
                                    }
                                    __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                    if (forReport.Length > 0)
                                    {
                                        __result.Append("<as_report>" + __forReport + "</as_report>");
                                    }
                                    __result.Append("</Row>");
                                }
                                else
                                {
                                    // เดือนเริ่มต้นที่เลือก น้อยกว่าเท่ากับ เดือนเริ่มต้นคำนวณ และ เดือนสิ้นสุดที่เลือก มากกว่าเท่ากับ เดือนที่สิ้นสุดคำนวณ
                                    if (__dateCompareBegin <= 0 && __dateCompareEnd >= 0 && __dateCompareBeginAndEnd >= 0)
                                    {
                                        string __dateBetweenBeginName = __dateColumnBetweenBegin.ToString("yyyy-MM");
                                        string __dateBetweenEndName = __dateColumnBetweenEnd.ToString("yyyy-MM");
                                        int __beginBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenBeginName, new _dateCompareSearch());
                                        int __endBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenEndName, new _dateCompareSearch());
                                        int __diffBetween = 12 * (__dateColumnBetweenBegin.Year - __dateStrColumnBegin.Year) + __dateColumnBetweenBegin.Month - __dateStrColumnBegin.Month;
                                        int __diffDataBetween = __endBetweenFound - __beginBetweenFound;
                                        if (__diffDataBetween == 0)
                                        {
                                            __diffDataBetween = 1;
                                        }
                                        else
                                        {
                                            __diffDataBetween = __diffDataBetween + 1;
                                        }
                                        int __diffBetweenEnd = 12 * (__dateStrColumnEnd.Year - __dateColumnBetweenBegin.Year) + __dateStrColumnEnd.Month - __dateColumnBetweenBegin.Month;
                                        __result.Append("<Row>");
                                        __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Code) + "</as_code>");
                                        __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Name) + "</as_name>");
                                        __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                        __totalCome = __assetListType[__beginBetweenFound].Come;
                                        __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                        int dayCount = __beginBetweenFound;
                                        float sumResult = 0;
                                        for (int fs = 0; fs < __diffBetween; fs++)
                                        {
                                            __result.Append("<c" + fs + ">0</c" + fs + ">");
                                        }
                                        for (int f = __diffBetween; f < __diffBetween + __diffDataBetween; f++)
                                        {
                                            if (__endSaleFound > 0)
                                            {
                                                if (f > __endSaleFound + __diffBetween)
                                                {
                                                    __result.Append("<c" + f + ">0</c" + f + ">");
                                                }
                                                else
                                                {
                                                    __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                    sumResult += __assetListType[dayCount].Result;
                                                    dayCount++;
                                                }
                                            }
                                            else
                                            {
                                                __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                sumResult += __assetListType[dayCount].Result;
                                                dayCount++;
                                            }
                                        }
                                        for (int fe = __diffBetween + __diffDataBetween; fe <= __diffBetween + __diffDataBetween + __diffBetweenEnd; fe++)
                                        {
                                            __result.Append("<c" + fe + ">0</c" + fe + ">");
                                        }
                                        __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                        if (__endSaleFound > 0)
                                        {
                                            if (__endSaleFound >= __endBetweenFound)
                                            {
                                                __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                            }
                                            else
                                            {
                                                __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                            }
                                        }
                                        else
                                        {
                                            __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                        }
                                        __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                        if (forReport.Length > 0)
                                        {
                                            __result.Append("<as_report>" + __forReport + "</as_report>");
                                        }
                                        __result.Append("</Row>");
                                    }
                                    else if (__dateCompareBegin <= 0 && __dateCompareEnd <= 0 && __dateCompareBeginAndEnd >= 0)
                                    {
                                        string __dateBetweenBeginName = __dateColumnBetweenBegin.ToString("yyyy-MM");
                                        int __beginBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenBeginName, new _dateCompareSearch());
                                        int __diffBetween = 12 * (__dateColumnBetweenBegin.Year - __dateStrColumnBegin.Year) + __dateColumnBetweenBegin.Month - __dateStrColumnBegin.Month;
                                        int __diffBetweenEnd = 12 * (__dateStrColumnEnd.Year - __dateColumnBetweenBegin.Year) + __dateStrColumnEnd.Month - __dateColumnBetweenBegin.Month;
                                        __result.Append("<Row>");
                                        __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Code) + "</as_code>");
                                        __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Name) + "</as_name>");
                                        __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                        __totalCome = __assetListType[__beginBetweenFound].Come;
                                        __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                        int dayCount = __beginBetweenFound;
                                        float sumResult = 0;
                                        for (int fs = 0; fs < __diffBetween; fs++)
                                        {
                                            __result.Append("<c" + fs + ">0</c" + fs + ">");
                                        }
                                        for (int f = __diffBetween; f <= __diffBetween + __diffBetweenEnd + __beginBetweenFound; f++)
                                        {
                                            if (__endSaleFound > 0)
                                            {
                                                if (f > __endSaleFound + __diffBetween)
                                                {
                                                    __result.Append("<c" + f + ">0</c" + f + ">");
                                                }
                                                else
                                                {
                                                    __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                    sumResult += __assetListType[dayCount].Result;
                                                    dayCount++;
                                                }
                                            }
                                            else
                                            {
                                                __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                sumResult += __assetListType[dayCount].Result;
                                                dayCount++;
                                            }
                                        }
                                        __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                        if (__endSaleFound > 0)
                                        {
                                            if (__endSaleFound >= __beginBetweenFound + __diffBetweenEnd)
                                            {
                                                __totalValueGo = __assetListType[__beginBetweenFound + __diffBetweenEnd].ValueGo;
                                            }
                                            else
                                            {
                                                __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                            }
                                        }
                                        else
                                        {
                                            __totalValueGo = __assetListType[__beginBetweenFound + __diffBetweenEnd].ValueGo;
                                        }
                                        __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                        if (forReport.Length > 0)
                                        {
                                            __result.Append("<as_report>" + __forReport + "</as_report>");
                                        }
                                        __result.Append("</Row>");
                                    }
                                    else
                                    {
                                        if (__beginFound > 0)
                                        {
                                            string __dateBetweenEndName = __dateColumnBetweenEnd.ToString("yyyy-MM");
                                            int __endBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenEndName, new _dateCompareSearch());
                                            int __diffBetweenEnd = 12 * (__dateStrColumnEnd.Year - __dateColumnBetweenBegin.Year) + __dateStrColumnEnd.Month - __dateColumnBetweenBegin.Month;
                                            __result.Append("<Row>");
                                            __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Code) + "</as_code>");
                                            __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Name) + "</as_name>");
                                            __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                            __totalCome = __assetListType[__beginFound].Come;
                                            __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                            int dayCount = 0;
                                            float sumResult = 0;
                                            for (int f = __beginFound; f <= __endBetweenFound; f++)
                                            {
                                                if (__endSaleFound > 0)
                                                {
                                                    if (f > __endSaleFound)
                                                    {
                                                        __result.Append("<c" + dayCount + ">0</c" + dayCount + ">");
                                                        dayCount++;
                                                    }
                                                    else
                                                    {
                                                        __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                                        sumResult += __assetListType[f].Result;
                                                        dayCount++;
                                                    }
                                                }
                                                else
                                                {
                                                    __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                                    sumResult += __assetListType[f].Result;
                                                    dayCount++;
                                                }
                                            }
                                            for (int fe = __endBetweenFound; fe < __diffBetweenEnd; fe++)
                                            {
                                                __result.Append("<c" + dayCount + ">0</c" + dayCount + ">");
                                                dayCount++;
                                            }
                                            __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                            if (__endSaleFound > 0)
                                            {
                                                if (__endSaleFound >= __endBetweenFound)
                                                {
                                                    __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                                }
                                                else
                                                {
                                                    __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                                }
                                            }
                                            else
                                            {
                                                __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                            }
                                            __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                            if (forReport.Length > 0)
                                            {
                                                __result.Append("<as_report>" + __forReport + "</as_report>");
                                            }
                                            __result.Append("</Row>");
                                        }
                                    }
                                }
                            } // Close More Than 0
                        }
                        #endregion
                        #region Process By Year
                        else if (processBy == 3)
                        {
                            // คำนวณใหม่ทั้งหมด
                            DateTime __yearColumnBeginGlobal = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnBeginDate].ToString());
                            DateTime __yearColumnEndGlobal = Convert.ToDateTime(__getAssets.Tables[0].Rows[__row].ItemArray[__columnEndDate].ToString());
                            //*****************
                            int __yearApart = __yearColumnEndGlobal.Year - __yearColumnBeginGlobal.Year;
                            if (__yearApart >= 0)
                            {
                                float __buyPrice = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnBuyPrice].ToString());
                                float __deadValue = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnDeadValue].ToString());
                                float __age = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnAge].ToString());
                                int __calcType = (int)MyLib._myGlobal._intPhase(__getAssets.Tables[0].Rows[__row].ItemArray[__columnCalcType].ToString());
                                float __CalcValue = (float)float.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnCalcValue].ToString());
                                float __calcPerYear = 0;
                                float __calcZeroStartPerYear = (float)(__CalcValue - __deadValue) / __age;
                                float __beforeStartSquareRoot = (float)__deadValue / (float)__CalcValue;
                                float __yearStartForPower = (float)1 / (__age);
                                float __depreciateStartPercent = (float)1 - (float)Math.Pow(__beforeStartSquareRoot, __yearStartForPower);
                                float __dataForProcess = 0;
                                float __balance = __CalcValue;
                                float __comeValue = 0;
                                float __netCome = 0;
                                float __netDepreciate = 0;
                                float __netValueGo = 0;
                                // จำนวนวันในปีเริ่มต้นและจำนวนวันตั้งแต่วันแรกจนถึงวันที่เริ่มต้น
                                int __yearOfEnd = MyLib._myGlobal._intPhase(__subYearDateBegin); // toe เอาออก -543;
                                DateTime __endOfYearBefore = new DateTime(__yearOfEnd, 12, 31);
                                DateTime __beginOfYearBefore = new DateTime(__yearOfEnd, 01, 01);
                                TimeSpan __timeSpanDateInYearBefore = __endOfYearBefore.Subtract(__beginOfYearBefore);
                                int __diffDateInYearBefore = (int)__timeSpanDateInYearBefore.TotalDays + 1;
                                TimeSpan __timeSpanBetweenBeforeDate = __endOfYearBefore.Subtract(__yearColumnBeginGlobal);
                                int __diffDateBefore = (int)__timeSpanBetweenBeforeDate.TotalDays;
                                // จำนวนวันในปีสิ้นสุดและจำนวนวันตั้งแต่วันที่สิ้นสุดจนถึงวันสิ้นปี
                                int __yearOfBegin = MyLib._myGlobal._intPhase(__subYearDateEnd); // toe เอาออก -543;
                                DateTime __beginOfYearAfter = new DateTime(__yearOfBegin, 01, 01);
                                DateTime __endOfYearAfter = new DateTime(__yearOfBegin, 12, 31);
                                TimeSpan __timeSpanDateInYearAfter = __endOfYearAfter.Subtract(__beginOfYearAfter);
                                int __diffDateInYearAfter = (int)__timeSpanDateInYearAfter.TotalDays + 1;
                                TimeSpan __timeSpanBetweenAfterDate = __yearColumnEndGlobal.Subtract(__beginOfYearAfter);
                                int __diffDateAfter = (int)__timeSpanBetweenAfterDate.TotalDays;                                
                                //**************
                                _assetListType[] __assetListType = new _assetListType[__yearApart + 1];
                                Calendar calYear = CultureInfo.CurrentCulture.DateTimeFormat.Calendar;
                                // __calcType 0=เส้นตรง, 1=ยอดลดลง
                                // processMode 0=ตามหลักกรมสรรพากร, 1=ตามหลักการบัญชี, 2=ตามจำนวนวันจริง
                                for (int __year = 0; __year <= __yearApart; __year++)
                                {
                                    // เริ่มต้นมากกว่า วันที่ 1 เดือน 1 สิ้นสุด วันที่ 31 เดือน 12
                                    if ((__subDayDateBegin.Equals("01") == false || __subMonthDateBegin.Equals("01") == false) && (__subDayDateEnd.Equals("31") == true && __subMonthDateEnd.Equals("12") == true))
                                    {
                                        if (__year == 0)
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / __diffDateInYearBefore;
                                                }
                                                __calcPerYear = __calcZeroStartPerYear * __dataForProcess;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / __diffDateInYearBefore;
                                                }
                                                __calcPerYear = __CalcValue * __depreciateStartPercent * __dataForProcess;
                                            }
                                        }
                                        else
                                        {
                                            if (__calcType == 0)
                                            {
                                                __calcPerYear = __calcZeroStartPerYear;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (__balance < 1)
                                                {
                                                    __calcPerYear = __comeValue - __deadValue;
                                                }
                                                else
                                                {
                                                    __calcPerYear = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                    }
                                    // สิ้นสุดน้อยกว่า วันที่ 31 เดือน 12 เริ่มต้น วันที่ 1 เดือน 1
                                    if ((__subDayDateEnd.Equals("31") == false || __subMonthDateEnd.Equals("12") == false) && (__subDayDateBegin.Equals("01") == true && __subMonthDateBegin.Equals("01") == true))
                                    {
                                        if (__year == __yearApart)
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / __diffDateInYearAfter;
                                                }
                                                __calcPerYear = __calcZeroStartPerYear * __dataForProcess;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / __diffDateInYearAfter;
                                                }
                                                __calcPerYear = __calcPerYear * __dataForProcess;
                                            }
                                        }
                                        else
                                        {
                                            if (__calcType == 0)
                                            {
                                                __calcPerYear = __calcZeroStartPerYear;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (__balance < 1)
                                                {
                                                    __calcPerYear = __comeValue - __deadValue;
                                                }
                                                else
                                                {
                                                    __calcPerYear = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                    } 
                                    // เริ่มต้นมากกว่า วันที่ 1 เดือน 1 สิ้นสุดน้อยกว่า วันที่ 31 เดือน 12
                                    if ((__subDayDateBegin.Equals("01") == false || __subMonthDateBegin.Equals("01") == false) && (__subDayDateEnd.Equals("31") == false || __subMonthDateEnd.Equals("12") == false))
                                    {
                                        if (__year == 0)
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / __diffDateInYearBefore;
                                                }
                                                __calcPerYear = __calcZeroStartPerYear * __dataForProcess;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateBefore / __diffDateInYearBefore;
                                                }
                                                __calcPerYear = __CalcValue * __depreciateStartPercent * __dataForProcess;
                                            }
                                        }
                                        else if (__year > 0 && __year < __yearApart)
                                        {
                                            if (__calcType == 0)
                                            {
                                                __calcPerYear = __calcZeroStartPerYear;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (__balance < 1)
                                                {
                                                    __calcPerYear = __comeValue - __deadValue;
                                                }
                                                else
                                                {
                                                    __calcPerYear = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (__calcType == 0)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / __diffDateInYearAfter;
                                                }
                                                __calcPerYear = __calcZeroStartPerYear * __dataForProcess;
                                            }
                                            else if (__calcType == 1)
                                            {
                                                if (processMode == 0)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 365;
                                                }
                                                else if (processMode == 1)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / 360;
                                                }
                                                else if (processMode == 2)
                                                {
                                                    __dataForProcess = (float)__diffDateAfter / __diffDateInYearAfter;
                                                }
                                                __calcPerYear = __calcPerYear * __dataForProcess;
                                            }
                                        }
                                    }
                                    // เริ่มต้น วันที่ 1 เดือน 1 สิ้นสุด วันที่ 31 เดือน 12
                                    if (__subDayDateBegin.Equals("01") == true && __subMonthDateBegin.Equals("01") == true && __subDayDateEnd.Equals("31") == true && __subMonthDateEnd.Equals("12") == true)
                                    {
                                        if (__calcType == 0)
                                        {
                                            __calcPerYear = __calcZeroStartPerYear;
                                        }
                                        else if (__calcType == 1)
                                        {
                                            if (__balance < 1)
                                            {
                                                __calcPerYear = __comeValue - __deadValue;
                                            }
                                            else
                                            {
                                                __calcPerYear = (__CalcValue - __comeValue) * __depreciateStartPercent;
                                            }
                                        }
                                    }
                                    //*********************
                                    __netDepreciate += __calcPerYear;
                                    if (__year == 0)
                                    {
                                        __netCome = __CalcValue;
                                        __netValueGo = __CalcValue - __netDepreciate;
                                        __netDepreciate = 0;
                                    }
                                    else
                                    {
                                        __netCome = __netValueGo;
                                        __netValueGo = __netCome - __netDepreciate;
                                        __netDepreciate = 0;
                                    }
                                    string __codeType = __getAssets.Tables[0].Rows[__row].ItemArray[__columnCode].ToString();
                                    string __nameType = __getAssets.Tables[0].Rows[__row].ItemArray[__columnName].ToString();
                                    DateTime __yearColumnForSave = __dateColumnBegin;
                                    string __yearName = __yearColumnForSave.ToString("yyyy");
                                    __assetListType[__year] = new _assetListType(__codeType, __nameType, __yearName, __calcPerYear, __netCome, __netValueGo);
                                    __dateColumnBegin = calYear.AddYears(__dateColumnBegin, 1);
                                    __balance -= __calcPerYear;
                                    __comeValue += __calcPerYear;
                                }
                                // Sort
                                Array.Sort(__assetListType, new _dateCompareSort());
                                // Binary Search
                                string __yearBeginName = __dateSelectColumnBegin.ToString("yyyy", __cultureEN);
                                string __yearEndName = __dateSelectColumnEnd.ToString("yyyy", __cultureEN);
                                int __beginFound = Array.BinarySearch(__assetListType, __yearBeginName, new _dateCompareSearch());
                                int __endFound = Array.BinarySearch(__assetListType, __yearEndName, new _dateCompareSearch());
                                int __endSaleFound = Array.BinarySearch(__assetListType, __yearSearchSale, new _dateCompareSearch());
                                // ประกอบร่าง ตามจำนวนปีที่จะโชว์ในกริด
                                if (__beginFound >= 0 && __endFound >= 0)
                                {
                                    __result.Append("<Row>");
                                    __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Code) + "</as_code>");
                                    __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Name) + "</as_name>");
                                    __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                    __totalCome = __assetListType[__beginFound].Come;
                                    __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                    int dayCount = 0;
                                    float sumResult = 0;
                                    for (int f = __beginFound; f <= __endFound; f++)
                                    {
                                        if (__endSaleFound > 0)
                                        {
                                            if (f > __endSaleFound)
                                            {
                                                __result.Append("<c" + dayCount + ">0</c" + dayCount + ">");
                                                dayCount++;
                                            }
                                            else
                                            {
                                                __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                                sumResult += __assetListType[f].Result;
                                                dayCount++;
                                            }
                                        }
                                        else
                                        {
                                            __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                            sumResult += __assetListType[f].Result;
                                            dayCount++;
                                        }
                                    }
                                    __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                    if (__endSaleFound > 0)
                                    {
                                        if (__endSaleFound >= __endFound)
                                        {
                                            __totalValueGo = __assetListType[__endFound].ValueGo;
                                        }
                                        else
                                        {
                                            __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                        }
                                    }
                                    else
                                    {
                                        __totalValueGo = __assetListType[__endFound].ValueGo;
                                    }
                                    __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                    if (forReport.Length > 0)
                                    {
                                        __result.Append("<as_report>" + __forReport + "</as_report>");
                                    }
                                    __result.Append("</Row>");
                                }
                                else
                                {
                                    // ปีเริ่มต้นที่เลือก น้อยกว่าเท่ากับ ปีเริ่มต้นคำนวณ และ ปีสิ้นสุดที่เลือก มากกว่าเท่ากับ ปีที่สิ้นสุดคำนวณ
                                    if (__dateCompareBegin <= 0 && __dateCompareEnd >= 0 && __dateCompareBeginAndEnd >= 0)
                                    {
                                        string __dateBetweenBeginName = __dateColumnBetweenBegin.ToString("yyyy");
                                        string __dateBetweenEndName = __dateColumnBetweenEnd.ToString("yyyy");
                                        int __beginBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenBeginName, new _dateCompareSearch());
                                        int __endBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenEndName, new _dateCompareSearch());
                                        int __diffBetween = __dateColumnBetweenBegin.Year - __dateStrColumnBegin.Year;
                                        int __diffDataBetween = __endBetweenFound - __beginBetweenFound;
                                        if (__diffDataBetween == 0)
                                        {
                                            __diffDataBetween = 1;
                                        }
                                        else
                                        {
                                            __diffDataBetween = __diffDataBetween + 1;
                                        }
                                        int __diffBetweenEnd = __dateStrColumnEnd.Year - __dateColumnBetweenBegin.Year;
                                        __result.Append("<Row>");
                                        __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Code) + "</as_code>");
                                        __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Name) + "</as_name>");
                                        __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                        __totalCome = __assetListType[__beginBetweenFound].Come;
                                        __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                        int dayCount = __beginBetweenFound;
                                        float sumResult = 0;
                                        for (int fs = 0; fs < __diffBetween; fs++)
                                        {
                                            __result.Append("<c" + fs + ">0</c" + fs + ">");
                                        }
                                        for (int f = __diffBetween; f < __diffBetween + __diffDataBetween; f++)
                                        {
                                            if (__endSaleFound > 0)
                                            {
                                                if (f > __endSaleFound + __diffBetween)
                                                {
                                                    __result.Append("<c" + f + ">0</c" + f + ">");
                                                }
                                                else
                                                {
                                                    __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                    sumResult += __assetListType[dayCount].Result;
                                                    dayCount++;
                                                }
                                            }
                                            else
                                            {
                                                __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                sumResult += __assetListType[dayCount].Result;
                                                dayCount++;
                                            }
                                        }
                                        for (int fe = __diffBetween + __diffDataBetween; fe <= __diffBetween + __diffDataBetween + __diffBetweenEnd; fe++)
                                        {
                                            __result.Append("<c" + fe + ">0</c" + fe + ">");
                                        }
                                        __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                        if (__endSaleFound > 0)
                                        {
                                            if (__endSaleFound >= __endBetweenFound)
                                            {
                                                __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                            }
                                            else
                                            {
                                                __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                            }
                                        }
                                        else
                                        {
                                            __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                        }
                                        __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                        if (forReport.Length > 0)
                                        {
                                            __result.Append("<as_report>" + __forReport + "</as_report>");
                                        }
                                        __result.Append("</Row>");
                                    }
                                    else if (__dateCompareBegin <= 0 && __dateCompareEnd <= 0 && __dateCompareBeginAndEnd >= 0)
                                    {
                                        string __dateBetweenBeginName = __dateColumnBetweenBegin.ToString("yyyy");
                                        int __beginBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenBeginName, new _dateCompareSearch());
                                        int __diffBetween = __dateColumnBetweenBegin.Year - __dateStrColumnBegin.Year;
                                        int __diffBetweenEnd = __dateStrColumnEnd.Year - __dateColumnBetweenBegin.Year;
                                        __result.Append("<Row>");
                                        __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Code) + "</as_code>");
                                        __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginBetweenFound].Name) + "</as_name>");
                                        __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                        __totalCome = __assetListType[__beginBetweenFound].Come;
                                        __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                        int dayCount = __beginBetweenFound;
                                        float sumResult = 0;
                                        for (int fs = 0; fs < __diffBetween; fs++)
                                        {
                                            __result.Append("<c" + fs + ">0</c" + fs + ">");
                                        }
                                        for (int f = __diffBetween; f <= __diffBetween + __diffBetweenEnd + __beginBetweenFound; f++)
                                        {
                                            if (__endSaleFound > 0)
                                            {
                                                if (f > __endSaleFound + __diffBetween)
                                                {
                                                    __result.Append("<c" + f + ">0</c" + f + ">");
                                                }
                                                else
                                                {
                                                    __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                    sumResult += __assetListType[dayCount].Result;
                                                    dayCount++;
                                                }
                                            }
                                            else
                                            {
                                                __result.Append("<c" + f + ">" + __assetListType[dayCount].Result.ToString() + "</c" + f + ">");
                                                sumResult += __assetListType[dayCount].Result;
                                                dayCount++;
                                            }
                                        }
                                        __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                        if (__endSaleFound > 0)
                                        {
                                            if (__endSaleFound >= __beginBetweenFound + __diffBetweenEnd)
                                            {
                                                __totalValueGo = __assetListType[__beginBetweenFound + __diffBetweenEnd].ValueGo;
                                            }
                                            else
                                            {
                                                __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                            }
                                        }
                                        else
                                        {
                                            __totalValueGo = __assetListType[__beginBetweenFound + __diffBetweenEnd].ValueGo;
                                        }
                                        __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                        if (forReport.Length > 0)
                                        {
                                            __result.Append("<as_report>" + __forReport + "</as_report>");
                                        }
                                        __result.Append("</Row>");
                                    }
                                    else
                                    {
                                        if (__beginFound > 0)
                                        {
                                            string __dateBetweenEndName = __dateColumnBetweenEnd.ToString("yyyy");
                                            int __endBetweenFound = Array.BinarySearch(__assetListType, __dateBetweenEndName, new _dateCompareSearch());
                                            int __diffBetweenEnd = __dateStrColumnEnd.Year - __dateColumnBetweenBegin.Year;
                                            __result.Append("<Row>");
                                            __result.Append("<as_code>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Code) + "</as_code>");
                                            __result.Append("<as_name>" + MyLib._myUtil._convertTextToXml(__assetListType[__beginFound].Name) + "</as_name>");
                                            __result.Append("<as_buyprice>" + __buyPrice.ToString() + "</as_buyprice>");
                                            __totalCome = __assetListType[__beginFound].Come;
                                            __result.Append("<as_come>" + __totalCome.ToString() + "</as_come>");
                                            int dayCount = 0;
                                            float sumResult = 0;
                                            for (int f = __beginFound; f <= __endBetweenFound; f++)
                                            {
                                                if (__endSaleFound > 0)
                                                {
                                                    if (f > __endSaleFound)
                                                    {
                                                        __result.Append("<c" + dayCount + ">0</c" + dayCount + ">");
                                                        dayCount++;
                                                    }
                                                    else
                                                    {
                                                        __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                                        sumResult += __assetListType[f].Result;
                                                        dayCount++;
                                                    }
                                                }
                                                else
                                                {
                                                    __result.Append("<c" + dayCount + ">" + __assetListType[f].Result.ToString() + "</c" + dayCount + ">");
                                                    sumResult += __assetListType[f].Result;
                                                    dayCount++;
                                                }
                                            }
                                            for (int fe = __endBetweenFound; fe < __diffBetweenEnd; fe++)
                                            {
                                                __result.Append("<c" + dayCount + ">0</c" + dayCount + ">");
                                                dayCount++;
                                            }
                                            __result.Append("<as_result>" + sumResult.ToString() + "</as_result>");
                                            if (__endSaleFound > 0)
                                            {
                                                if (__endSaleFound >= __endBetweenFound)
                                                {
                                                    __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                                }
                                                else
                                                {
                                                    __totalValueGo = __assetListType[__endSaleFound].ValueGo;
                                                }
                                            }
                                            else
                                            {
                                                __totalValueGo = __assetListType[__endBetweenFound].ValueGo;
                                            }
                                            __result.Append("<as_valuego>" + __totalValueGo.ToString() + "</as_valuego>");
                                            if (forReport.Length > 0)
                                            {
                                                __result.Append("<as_report>" + __forReport + "</as_report>");
                                            }
                                            __result.Append("</Row>");
                                        }
                                    }
                                }
                            }// Close More Than 0
                        }// Close If Process By
                        #endregion

                    }// Close Have Date For Calculate          
                } // Close For Loop Asset
            }// มีข้อมูลจากการคิวรี่
            __result.Append("</ResultSet>");
            __result.Append("</node>");
            return __result.ToString();
        } // End _asViewDepreciateBalance
    } // End Class _asProcess

    public class _assetListType
    {
        private string _code;
        private string _name;
        private string _date;
        private float _result;
        private float _come;
        private float _valuego;

        public _assetListType() 
        {
            _code = "";
            _name = "";
            _date = "";
            _result = 0;
            _come = 0;
            _valuego = 0;
        }
        public _assetListType(string code, string name, string date, float result, float come, float valuego)
        {
            _code = code;
            _name = name;
            _date = date;
            _result = result;
            _come = come;
            _valuego = valuego;
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public float Result
        {
            get { return _result; }
            set { _result = value; }
        }
        public float Come
        {
            get { return _come; }
            set { _come = value; }
        }
        public float ValueGo
        {
            get { return _valuego; }
            set { _valuego = value; }
        }
    }
    // Sort
    public class _dateCompareSort : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            int _sortVal = String.Compare((x as _assetListType).Date, (y as _assetListType).Date, true);
            return _sortVal;
        }
    }
    // Search
    public class _dateCompareSearch : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            int _searchVal = (x as _assetListType).Date.CompareTo((string)y);
            return _searchVal;
        }
    }
    //*****
}
