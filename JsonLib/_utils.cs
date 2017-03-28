using System;
using System.Collections.Generic;
using System.Json;
using System.Text;

namespace JsonLib
{
    public class _utils
    {
        public static string _getJsonValue(JsonValue jsonObject)
        {
            string __result = "";
            if (jsonObject == null)
                return null;

            __result = jsonObject.ToString().Replace("\"", string.Empty);

            return __result;
        }

        public static string _getJsonValueForQuery(JsonValue jsonObject)
        {

            string __result = "";

            if (jsonObject == null)
                return  "null";

            __result = "\'" + _getJsonValue(jsonObject) + "\'";

            return __result;
        }
    }
}
