using System;
using System.Data;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MyLib
{
    public class _myWebservice : SMLJAVAWS.DotNetFrameWorkService
    {
        private string _webServiceFunctionName = "/DotNetFrameWork";
        public Boolean _compress = false;

        public _myWebservice()
        {
            this._init(MyLib._myGlobal._webServiceServer);
        }

        public _myWebservice(string webServiceName)
        {
            this._init(webServiceName);
        }

        void _init(string webServiceName)
        {
            try
            {
                if (webServiceName != null)
                {
                    //this.Proxy = _myGlobal._webProxy();
                    this.Url = string.Concat("http://", webServiceName, "/", MyLib._myGlobal._webServiceName, this._webServiceFunctionName);
                    this._compress = _checkCompress(webServiceName);
                    this.Timeout = 500000;
                    if (MyLib._myGlobal._proxyUsed == 1)
                    {
                        WebProxy __proxy = new WebProxy(MyLib._myGlobal._proxyUrl, true);
                        __proxy.Credentials = new NetworkCredential(MyLib._myGlobal._proxyUser, MyLib._myGlobal._proxyPassword);
                        this.Proxy = __proxy;
                    }
                }
            }
            catch
            {
            }
        }

        private Boolean _checkCompress(string webServiceName)
        {
            webServiceName = webServiceName.ToLower();
            Boolean __result =
                (webServiceName.IndexOf("192.") != -1 ||
                webServiceName.IndexOf("localhost") != -1 ||
                webServiceName.IndexOf("127.") != -1) ? false : true;
            //_myGlobal._compressWebservice = true; // ทดสอบการย่อ จะได้ดูว่ามี Bug หรือเปล่า
            _myGlobal._compressWebservice = __result;
            return __result;
        }
    }
}
