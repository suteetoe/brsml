using MyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    class _processBase
    {
        private string _tableName = "";
        private string _sendUrl = "";
      
        public string tableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                this._tableName = value;
            }
        }

        public string sendUrl
        {
            get
            {
                return this._sendUrl;
            }
            set
            {
                this._sendUrl = value;
            }
        }

        public void _startProcess()
        {

            _setdata(_getDataSml(_tableName));
            _sendData();
        }

        protected virtual void _setdata(ArrayList data) {

        }

        public ArrayList _getDataSml(string _tableName)
        {
          
            //StringBuilder __myquery = new StringBuilder();
            //__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from "+ _tableName + " limit 10"));
            //__myquery.Append("</node>");
            //ArrayList __getData = __myFrameWork._queryListGetData(_myGlobal._databaseName, __myquery.ToString());
            //DataSet __data = (DataSet)__getData[0];

            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select code,name_1 from " + this.tableName + " limit 10"));
            __myquery.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(Program.serverURL, "SMLConfig" + Program.provider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
            ArrayList __getData = __myFrameWork._queryListGetData(Program.dbName, __myquery.ToString());
            DataSet getData = (DataSet)__getData[0];

            for (int __row = 0; __row < getData.Tables[0].Rows.Count; __row++)
            {
                Console.WriteLine(getData.Tables[0].Rows[__row]["code"].ToString());
                
            }
            return __getData;
        }

        public static void _sendData()
        {
 

        }

    }


}
