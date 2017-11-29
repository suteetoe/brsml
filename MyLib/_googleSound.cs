using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace MyLib
{
    public static class _googleSound
    {
        public static List<String> _soundList = new List<string>();
       // private static WMPLib.WindowsMediaPlayer _wplayer;
        private static string _path = @"c:\smlsoundtemp";
        private static double _rateDefalt = 1.75f;

        public static void _play(string text)
        {
            _play(text, 0);
        }

        public static void _play(string text,double rate)
        {
            // _soundList.Clear();
            if (rate == 0)
            {
                rate = _rateDefalt;
            }
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            text = text.Replace(",", " ").Replace("-", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            if (text.Trim().Length > 0)
            {
                string[] __split = text.Trim().Split(' ');
                for (int __loop = 0; __loop < __split.Length; __loop++)
                {
                    _soundList.Add(__split[__loop]);
                }
            }
            if (_soundList.Count > 0)
            {
                string __soundFileName = _path + "\\" + _soundList[0].ToString() + ".mp3";
                string __url = "http://translate.google.com/translate_tts?ie=UTF-8&tl=th&q=" + _soundList[0].ToString().Replace(" ", "%20");
                if (!File.Exists(__soundFileName))
                {
                    using (Stream __ms = new MemoryStream())
                    {
                        using (Stream __stream = WebRequest.Create(__url).GetResponse().GetResponseStream())
                        {
                            byte[] __buffer = new byte[32768];
                            int __read;
                            while ((__read = __stream.Read(__buffer, 0, __buffer.Length)) > 0)
                            {
                                __ms.Write(__buffer, 0, __read);
                            }
                        }
                        __ms.Position = 0;
                        FileStream __file = new FileStream(__soundFileName, FileMode.Create, System.IO.FileAccess.Write);
                        byte[] __bytes = new byte[__ms.Length];
                        __ms.Read(__bytes, 0, (int)__ms.Length);
                        __file.Write(__bytes, 0, __bytes.Length);
                        __file.Close();
                        __ms.Close();
                    }
                }
             //   _wplayer = new WMPLib.WindowsMediaPlayer();
              // _wplayer.settings.rate = rate;
             //  _wplayer.PlayStateChange += wplayer_PlayStateChange;
             //   _wplayer.URL = __soundFileName;
             //   _wplayer.controls.play();
            }
        }

        private static void wplayer_PlayStateChange(int NewState)
        {
         //   if (_wplayer.playState == WMPLib.WMPPlayState.wmppsReady)
         //   {
         //       _play("",0);
         //   }
         //   if (_wplayer.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
        //    {
        //        if (_soundList.Count > 0)
        //        {
        //            _soundList.RemoveAt(0);
         //       }
         //       _play("",0);
         //   }
        }
    }
}
