using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace SMLPosClient
{
    public static class _androidCustomerScreen
    {
        private static SMLPOSControl._posScreenConfig _posConfig;
        private static List<string> __valueSend = new List<string>();
        private static Boolean _isOpen = false;
        public static Thread _thread = null;

        public static void _setConfig(SMLPOSControl._posScreenConfig config)
        {
            _posConfig = config;
            if (_isOpen == false)
            {
                _isOpen = true;
                _thread = new Thread(new ThreadStart(_sendThread));
                _thread.Start();
            }
        }

        public static void _stop()
        {
            try
            {
                _thread.Abort();
            }
            catch
            {
            }
        }

        public static string _add(_g.g._androidDisplayEnum command, string value)
        {
            switch (command)
            {
                case _g.g._androidDisplayEnum.ItemName:
                    return String.Concat("<name>", value, "</name>");
                case _g.g._androidDisplayEnum.Unit:
                    return String.Concat("<unit>", value, "</unit>");
                case _g.g._androidDisplayEnum.Qty:
                    return String.Concat("<qty>", value, "</qty>");
                case _g.g._androidDisplayEnum.Price:
                    return String.Concat("<price>", value, "</price>");
                case _g.g._androidDisplayEnum.Total:
                    return String.Concat("<total>", value, "</total>");
                case _g.g._androidDisplayEnum.Amount:
                    return String.Concat("<amount>", value, "</amount>");
                case _g.g._androidDisplayEnum.AmountWord:
                    return String.Concat("<amountWord>", value, "</amountWord>");
            }
            return "";
        }

        public static void _send(string value)
        {
            __valueSend.Add(value);
            if (__valueSend.Count > 20)
            {
                __valueSend.RemoveAt(0);
            }

        }

        public static void _sendThread()
        {
            while (true)
            {
                try
                {
                    if (__valueSend.Count > 0)
                    {
                        IPEndPoint __ip = new IPEndPoint(IPAddress.Any, 18001);
                        Socket __socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        __socket.Bind(__ip);
                        __socket.Listen(10);
                        Socket __client = __socket.Accept();
                        Encoding __myUTF8 = Encoding.UTF8;
                        byte[] __dataBytes = new byte[400];
                        __dataBytes = __myUTF8.GetBytes(__valueSend[0]);
                        int __sentdata = __client.Send(__dataBytes, __dataBytes.Length, SocketFlags.None);
                        __valueSend.RemoveAt(0);
                        __client.Close();
                        __socket.Close();
                    }
                }
                catch (Exception __ex)
                {
                    Console.WriteLine(__ex.Message.ToString());
                }
            }
        }

        public static void _singleCommand(_g.g._androidDisplayEnum command, string value)
        {
            if (_posConfig._android_customer_display)
            {
                StringBuilder __str = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                __str.Append("<node>");
                switch (command)
                {
                    case _g.g._androidDisplayEnum.Clear:
                        {
                            __str.Append(String.Concat("<url>", MyLib._myGlobal._webServiceServer, "</url>"));
                            __str.Append(String.Concat("<provider>", MyLib._myGlobal._providerCode, "</provider>"));
                            __str.Append(_add(_g.g._androidDisplayEnum.ItemName, ""));
                            __str.Append(_add(_g.g._androidDisplayEnum.Unit, ""));
                            __str.Append(_add(_g.g._androidDisplayEnum.Qty, ""));
                            __str.Append(_add(_g.g._androidDisplayEnum.Price, ""));
                            __str.Append(_add(_g.g._androidDisplayEnum.Total, ""));
                            __str.Append(_add(_g.g._androidDisplayEnum.Amount, ""));
                            __str.Append(_add(_g.g._androidDisplayEnum.AmountWord, ""));
                        };
                        break;
                    default:
                        __str.Append(_add(command, value));
                        break;
                }
                __str.Append("</node>");
                _send(__str.ToString());
            }
        }
    }
}
