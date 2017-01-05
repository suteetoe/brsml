using System;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Collections;

namespace SMLProcess
{
    public class _smlFrameWork : _g.SMLJavaWebService.SMLFrameWorkService
    {
        private string __webServiceFunctionName = "/SMLFrameWork";
        public _smlFrameWork()
        {
            this.Url = string.Concat("http://", MyLib._myGlobal._webServiceServer, "/", MyLib._myGlobal._webServiceName, this.__webServiceFunctionName);
            if (MyLib._myGlobal._proxyUsed == 1)
            {
                WebProxy __proxy = new WebProxy(MyLib._myGlobal._proxyUrl, true);
                __proxy.Credentials = new NetworkCredential(MyLib._myGlobal._proxyUser, MyLib._myGlobal._proxyPassword);
                this.Proxy = __proxy;
            }
            _checkCompress(MyLib._myGlobal._webServiceServer);
        }

        public _smlFrameWork(string webServiceName)
        {
            this.Url = string.Concat("http://", webServiceName, "/", MyLib._myGlobal._webServiceName, this.__webServiceFunctionName);
            MyLib._myGlobal._webServiceServer = webServiceName;
            if (MyLib._myGlobal._proxyUsed == 1)
            {
                WebProxy __proxy = new WebProxy(MyLib._myGlobal._proxyUrl, true);
                __proxy.Credentials = new NetworkCredential(MyLib._myGlobal._proxyUser, MyLib._myGlobal._proxyPassword);
                this.Proxy = __proxy;
            }
            _checkCompress(MyLib._myGlobal._webServiceServer);
        }

        private void _checkCompress(string webServiceName)
        {
            MyLib._myGlobal._compressWebservice = (webServiceName.IndexOf(".com") != -1 || webServiceName.IndexOf(".net") != -1 || webServiceName.IndexOf("203.") != -1 || webServiceName.IndexOf("202.") != -1 || webServiceName.IndexOf(".info") != -1) ? true : false;
            //MyLib._myGlobal._compressWebservice = true; // ทดสอบการย่อ จะได้ดูว่ามี Bug หรือเปล่า
        }
    }

    public class _smlProcess
    {
        public String _glReFormatChartOfAccount()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __rs = __myFrameWork._queryShort("select code,account_level,main_code,status from gl_chart_of_account order by code").Tables[0];
            ArrayList __mainAccount = new ArrayList();
            for (int __loop = 0; __loop < 100; __loop++)
            {
                __mainAccount.Add("");
            }
            StringBuilder __myQuery = new StringBuilder((MyLib._myGlobal._xmlHeader + "<node>"));
            for (int __row = 0; __row < __rs.Rows.Count; __row++)
            {
                String __accountCode = __rs.Rows[__row][0].ToString();
                int __accountLevel = (int)MyLib._myGlobal._decimalPhase(__rs.Rows[__row][1].ToString());
                String __mainCode = __rs.Rows[__row][2].ToString();
                int __status = (int)MyLib._myGlobal._decimalPhase(__rs.Rows[__row][3].ToString());
                __mainAccount[__accountLevel] = __accountCode;
                string __mainCodeUpdate = (__accountLevel == 0) ? "" : __mainAccount[__accountLevel - 1].ToString();
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update gl_chart_of_account set main_code=\'" + __mainCodeUpdate + "\' where upper(code)=\'" + __accountCode.ToUpper() + "\'"));
            }
            __myQuery.Append("</node>");
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            //_smlFrameWork __frameWork = new _smlFrameWork();
            //return __frameWork._glReFormatChartOfAccount(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName);
            return "";
        }

        public DataTable _report_ar_status(string __whereString, string __orderByString)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            while (__myFrameWork._tryCount < 10)
            {
                __myFrameWork._tryCount++;
                try
                {
                    /* ----------------------
                    string __getTableString = "";
                    _smlFrameWork __ws = new _smlFrameWork();
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __getTableString = __ws._report_ar_status(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, __whereString, __orderByString);
                    }
                    else
                    {
                        byte[] __getByte = __ws._report_ar_status_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, __whereString, __orderByString);
                        __getTableString = MyLib._compress._deCompressString(__getByte);
                    }
                    DataSet __ds = __myFrameWork._convertStringToDataSet(__getTableString);
                    __ws.Dispose();
                    return __ds.Tables[0];
                     */
                }
                catch (TimeoutException)
                {
                    __myFrameWork._tryConnect();
                }
                catch (WebException)
                {
                    __myFrameWork._tryConnect();
                }
            }
            return null;
        }
    }
}
