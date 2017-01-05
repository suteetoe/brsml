using System;
using System.Collections.Generic;
using System.Text;

namespace MyLib._databaseManage
{
    [Serializable]
    public class _loginHistoryClass
    {
        public List<_loginHistoryDetailClass> __details = new List<_loginHistoryDetailClass>();
    }
    public class _loginHistoryDetailClass
    {
        public string __name;
        public List<string> __url = new List<string>();
        public string __provider;
        public string __group;
        public string __user;
        public string __proxyUrl;
        public int __proxyUsed;
        public string __proxyUser;
        public string __proxyPassword;
        public int __languageNumber;
    }
}
