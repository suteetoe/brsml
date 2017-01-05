using System;
using System.Collections.Generic;
using System.Text;

namespace SMLBalanceTransfer
{
    public class _global
    {
        public static Boolean _autoLogin = false;

        public static int _sourceProvider = -1;
        public static string _soruceHost = "";
        public static string _sourcePort = "";
        public static string _sourceUser = "";
        public static string _sourcePass = "";
        public static string _sourceDatabase = "";

        public static int _targetProvider = -1;
        public static string _targetHost = "";
        public static string _targetPort = "";
        public static string _targetUser = "";
        public static string _targetPass = "";
        public static string _targetDatabase = "";

        public static int _item_price_decimal = 2;
        public static int _item_qty_decimal = 2;
        public static int _item_amount_decimal = 2;
        /// <summary>
        /// result 0=postgres, 1=mssql, 2=mysql,3=oracle,4=firebird
        /// </summary>
        /// <param name="databaseType"></param>
        /// <param name="limitValue"></param>
        /// <returns></returns>
        public static System.Collections.ArrayList _getLimitQueryString(MyLib._myGlobal._databaseType databaseType, int limitValue)
        {
            System.Collections.ArrayList __result = new System.Collections.ArrayList();

            //PostgreSql,
            //MySql,
            //MicrosoftSQL2000,
            //MicrosoftSQL2005,
            //Oracle,
            //Firebird

            // 0
            if (databaseType == MyLib._myGlobal._databaseType.PostgreSql)
                __result.Add(" limit " + limitValue.ToString());
            else
                __result.Add("");

            // 1 sql server
            if (databaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2000 || databaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2005)
                __result.Add(" top " + limitValue.ToString());
            else
                __result.Add("");

            // 2 mysql
            if (databaseType == MyLib._myGlobal._databaseType.MySql)
                __result.Add(" limit " + limitValue.ToString());
            else
                __result.Add("");


            // 3 Oracle
            __result.Add("");

            // 4 firebird
            __result.Add("");


            return __result;
        }
    }


}
