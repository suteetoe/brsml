using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace MyLib
{
    public static class _getStream
    {
        public static event _getStreamEventHandler _getStreamEvent;
        //
        public static StreamReader _getDataStream(string xmlFileName)
        {
            //Assembly __thisAssembly = Assembly.GetExecutingAssembly();
            //Stream __xmlstream = __thisAssembly.GetManifestResourceStream("MyLib." + xmlFileName);
            StreamReader __result = null;
            if (_getStreamEvent != null)
            {
                __result = new StreamReader(_getStreamEvent(xmlFileName), Encoding.GetEncoding("utf-8"));
            }
            return __result;
        }
    }

    public delegate Stream _getStreamEventHandler(string xmlFileName);
}
