using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace SMLPosClient
{
    public partial class _selectScreenDemo : Form
    {
        string _savePosScreenConfigFileName = "smlPOSScreenConfig";

        Screen[] _displayDevice = null;
        string _moniterStr = "moniter";
        DataTable _posData = null;

        public _selectScreenDemo()
        {
            InitializeComponent();
            this._screenMoniter._maxColumn = 1;
            this.Load += new EventHandler(_selectScreenDemo_Load);

            _displayDevice = Screen.AllScreens;
            int __i;
            int __row = 0;
            for (__i = 0; __i < _displayDevice.Length; __i++)
            {
                Screen __src = _displayDevice[__i];
                string __devieName = string.Format("Moniter {0} ({1}x{2})", __i, __src.Bounds.Width, __src.Bounds.Height);
                this._screenMoniter._addComboBox(__row++, 0, _moniterStr + __i.ToString(), true, new string[] { "" }, false, __devieName);
            }

            this._screenMoniter.Invalidate();
        }

        void _selectScreenDemo_Load(object sender, EventArgs e)
        {
            _loadPosScreenList();
            _loadConfig();
        }

        private void _loadPosScreenList()
        {
            string __query = "select " + _g.d.sml_posdesign._screen_code + " , " + _g.d.sml_posdesign._screen_name + " from " + _g.d.sml_posdesign._table;
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();
            DataSet __ds = __fw._query(MyLib._myGlobal._databaseName, __query);
            List<string> __str = new List<string>();
            __str.Add("None");
            //Dictionary<string, string> __posList = new Dictionary<string, string>();
            if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
            {
                _posData = __ds.Tables[0];
                for (int __row = 0; __row < __ds.Tables[0].Rows.Count; __row++)
                {
                    __str.Add(__ds.Tables[0].Rows[__row][_g.d.sml_posdesign._screen_name].ToString());
                }
            }

            for (int __i = 0; __i < _displayDevice.Length; __i++)
            {
                Control __obj = this._screenMoniter._getControl(_moniterStr + __i);
                if (__obj != null && __obj.GetType() == typeof(MyLib._myComboBox))
                {
                    MyLib._myComboBox __comboBox = (MyLib._myComboBox)__obj;
                    __comboBox.Items.Clear();
                    __comboBox.Items.AddRange(__str.ToArray());
                    __comboBox.MaxDropDownItems = __str.Count;
                }
            }

        }


        private void _buttonSaveConfig_Click(object sender, EventArgs e)
        {
            // save config to xml
                        string __configFileName = _savePosScreenConfigFileName + MyLib._myGlobal._databaseName + ".xml";
            string __path = Path.GetTempPath() + "\\" + __configFileName.ToLower();

            //string __localpath = @"c:\\smlsoft" + "\\" + __configFileName.ToLower();
            string __localpath = string.Format(@"c:\\smlsoft\\{3}-{0}-{1}-{2}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__"), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, _savePosScreenConfigFileName);

            SMLPOSControl._posScreenConfig __config = null;

            try
            {
                // ลองอ่านจาก location ใหม่ (c:\smlsoft\)

                TextReader readFile = new StreamReader(__localpath.ToLower());
                XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                __config = (SMLPOSControl._posScreenConfig)__xsLoad.Deserialize(readFile);
                readFile.Close();
            }
            catch
            {
                // ถ้าไม่ได้ให้ไปอ่านที่เก่า
                try
                {
                    TextReader readFile = new StreamReader(__path.ToLower());
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                    __config = (SMLPOSControl._posScreenConfig)__xsLoad.Deserialize(readFile);
                    readFile.Close();
                }
                catch (Exception ex)
                {
                }
            }
            if (__config != null)
            {
                __config._screenConfig.Clear();

                for (int __i = 0; __i < this._displayDevice.Length; __i++)
                {
                    Control __obj = this._screenMoniter._getControl(_moniterStr + __i);
                    if (__obj != null && __obj.GetType() == typeof(MyLib._myComboBox))
                    {
                        MyLib._myComboBox __comboBox = (MyLib._myComboBox)__obj;
                        Screen __src = _displayDevice[__i];

                        string _screenDeviceName = Regex.Split(__src.DeviceName, "\0")[0].ToString();

                        SMLPOSControl._screenConfig __device = new SMLPOSControl._screenConfig() { _moniter = __i.ToString(), _deviceName = MyLib._myUtil._convertTextToXml(_screenDeviceName) };
                        __device._isMasterScreen = _checkIsPrimaryScreen(__src.DeviceName);
                        __device._screen_code = (__comboBox.SelectedIndex <= 0) ? "None" : _posData.Rows[__comboBox.SelectedIndex - 1][_g.d.sml_posdesign._screen_code].ToString();
                        __config._screenConfig.Add(__device);
                    }
                }

                XmlSerializer __colXs = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                TextWriter __memoryStream = new StreamWriter(__localpath.ToLower(), false, Encoding.UTF8);
                __colXs.Serialize(__memoryStream, __config);
                __memoryStream.Close();

            }

            this.Close();
        }

        private bool _checkIsPrimaryScreen(string __deviceName)
        {
            foreach (Screen __scr in _displayDevice)
            {
                if (__scr.DeviceName.Equals(__deviceName))
                {
                    return __scr.Primary;
                }
            }
            return false;
        }

        public void _loadConfig()
        {
            string __configFileName = _savePosScreenConfigFileName + MyLib._myGlobal._databaseName + ".xml";
            string __path = Path.GetTempPath() + "\\" + __configFileName.ToLower();

            //string __localpath = @"c:\\smlsoft" + "\\" + __configFileName.ToLower();
            string __localpath = string.Format(@"c:\\smlsoft\\{3}-{0}-{1}-{2}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__"), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, _savePosScreenConfigFileName);

            SMLPOSControl._posScreenConfig __config = null;

            try
            {
                // ลองอ่านจาก location ใหม่ (c:\smlsoft\)

                TextReader readFile = new StreamReader(__localpath.ToLower());
                XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                __config = (SMLPOSControl._posScreenConfig)__xsLoad.Deserialize(readFile);
                readFile.Close();
            }
            catch
            {
                // ถ้าไม่ได้ให้ไปอ่านที่เก่า
                try
                {
                    TextReader readFile = new StreamReader(__path.ToLower());
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                    __config = (SMLPOSControl._posScreenConfig)__xsLoad.Deserialize(readFile);
                    readFile.Close();
                }
                catch (Exception ex)
                {
                }
            }
            if (__config != null)
            {

                //Control __obj = 
                for (int __i = 0; __i < __config._screenConfig.Count; __i++)
                {
                    Control __obj = this._screenMoniter._getControl(_moniterStr + __i);
                    if (__obj != null && __obj.GetType() == typeof(MyLib._myComboBox))
                    {
                        MyLib._myComboBox __combobox = (MyLib._myComboBox)__obj;
                        int __selectedIndex = 0;
                        if (((SMLPOSControl._screenConfig)__config._screenConfig[__i])._screen_code.Equals("None"))
                        {

                        }
                        else
                        {
                            for (int __row = 0; __row < this._posData.Rows.Count; __row++)
                            {
                                if (this._posData.Rows[__row][_g.d.sml_posdesign._screen_code].ToString().Equals(((SMLPOSControl._screenConfig)__config._screenConfig[__i])._screen_code))
                                {
                                    __selectedIndex = __row + 1;
                                    break;
                                }
                            }
                        }

                        __combobox.SelectedIndex = __selectedIndex;
                    }
                }

                // config payform
                //Control __radio = this._screenMoniter._getControl(_resourcePayForm);
                //if (__radio != null && __radio.GetType() == typeof(MyLib._myComboBox))
                //{
                //    MyLib._myComboBox __comboBox = (MyLib._myComboBox)__radio;

                //    int __selectIndex = 0;
                //    for (int __row = 0; __row < this._posData.Rows.Count; __row++)
                //    {
                //        if (this._posData.Rows[__row][_g.d.sml_posdesign._screen_code].ToString().Equals(__config._payform))
                //        {
                //            __selectIndex = __row + 1;
                //            break;
                //        }
                //    }
                //    __comboBox.SelectedIndex = __selectIndex;
                //}

            }
        }

    }
}
