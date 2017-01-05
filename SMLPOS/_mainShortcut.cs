using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace SMLPOS
{

    public partial class _mainShortcut : UserControl
    {
        public _mainShortcut()
        {
            //this.ControlAdded += new ControlEventHandler(_mainShortcut_ControlAdded);

            InitializeComponent();

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSStarter)
            {
                this._reportGroupbox.Visible = false;
            }

            this.DoubleBuffered = true;

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            //this.BackgroundImage = global::SMLPOS.Properties.Resources.Abstract_Blue_backgrounds_3;
            //this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            // bind item event
            _bindClickItem(this._beginFlowPanel);
            _bindClickItem(this._soFlowPanel);
            _bindClickItem(this._icFlowPanel);
            _bindClickItem(this._arFlowPanel);

            if (MyLib._myGlobal._programName.Equals("mPay POS Online") || MyLib._myGlobal._programName.Equals("mPay POS Lite Online"))
            {
                this._labelProgramName.Text = "Saby Pay POS";
            }
            else
                this._labelProgramName.Text = MyLib._myGlobal._programName;

            this._checkboxShowWelcomeScreen.CheckedChanged += new EventHandler(_checkboxShowWelcomeScreen_CheckedChanged);

            this._initCheckBox();
        }

        void _initCheckBox()
        {
            try
            {
                if (File.Exists(MyLib._myGlobal._posConfigOpenWelcomeScreen))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(MyLib._myGlobal._posConfigOpenWelcomeScreen);
                    xDoc.DocumentElement.Normalize();

                    XmlElement __xRoot = xDoc.DocumentElement;

                    XmlNodeList __xReader = __xRoot.GetElementsByTagName("openDisplay");
                    if (__xReader.Count > 0)
                    {
                        XmlNode __xFirstNode = __xReader.Item(0);
                        if (__xFirstNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement __xTable = (XmlElement)__xFirstNode;
                            if (((int)MyLib._myGlobal._decimalPhase(__xTable.InnerText)) == 1)
                            {
                                _checkboxShowWelcomeScreen.Checked = true;
                            }
                        }
                    }
                }

            }
            catch
            {
            }
        }

        void _checkboxShowWelcomeScreen_CheckedChanged(object sender, EventArgs e)
        {
            //write config to sml config
            StringBuilder __xmlStr = new StringBuilder(String.Concat(MyLib._myGlobal._xmlHeader, "<node>"));
            __xmlStr.Append(String.Concat("<openDisplay>", ((this._checkboxShowWelcomeScreen.Checked) ? "1" : "0"), "</openDisplay>"));
            __xmlStr.Append(String.Concat("</node>"));

            StreamWriter __sr = File.CreateText(MyLib._myGlobal._posConfigOpenWelcomeScreen);
            __sr.WriteLine(__xmlStr.ToString());
            __sr.Close();
        }

        void _myFlowLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control.GetType() == typeof(MyLib._myImageButton))
            {
                MyLib._myImageButton __button = (MyLib._myImageButton)e.Control;
                __button.Click += new EventHandler(__buttonMenu_Click);
            }
        }

        void __buttonMenu_Click(object sender, System.EventArgs e)
        {
            MyLib._myImageButton __button = (MyLib._myImageButton)sender;

            if (this._menuButtonClick != null)
            {
                this._menuButtonClick(__button.ButtonText, ((__button.Name != null && __button.Name.Trim().Length > 0) ? __button.Name : ""), ((__button.Tag != null && __button.Tag.ToString().Length > 0) ? __button.Tag.ToString() : ""));
            }
        }

        void _bindClickItem(Control __obj)
        {
            for (int __i = 0; __i < __obj.Controls.Count; __i++)
            {
                // check tag versoin ด้วย
                if (__obj.Controls[__i].GetType() == typeof(MyLib._myImageButton))
                {

                    MyLib._myImageButton __button = (MyLib._myImageButton)__obj.Controls[__i];
                    _buttonRemoveByVersion(__button);
                    __button.Click += new EventHandler(__buttonMenu_Click);
                }
            }
        }

        void _buttonRemoveByVersion(MyLib._myImageButton button)
        {
            string __tag = "";
            switch (MyLib._myGlobal._isVersionEnum)
            {
                case MyLib._myGlobal._versionType.SMLAccount:
                    __tag = "&2&";
                    break;
                case MyLib._myGlobal._versionType.SMLAccountProfessional:
                    __tag = "&3&";
                    break;
                case MyLib._myGlobal._versionType.SMLPOS:
                    __tag = "&6&";
                    break;
                case MyLib._myGlobal._versionType.SMLPOSLite:
                    __tag = "&7&";
                    break;
                case MyLib._myGlobal._versionType.SMLTomYumGoong:
                    __tag = "&8&";
                    break;
                case MyLib._myGlobal._versionType.SMLPOSStarter:
                    __tag = "&10&";
                    break;
                case MyLib._myGlobal._versionType.SMLAccountPOSProfessional:
                    __tag = "&13&";
                    break;
            }

            if (button.Tag.ToString().Length > 0)
            {
                if (button.Tag.ToString().IndexOf(__tag) == -1)
                {
                    button.Visible = false;
                }
            }
        }

        public event _menuButtonclick _menuButtonClick;

        private void _flowMenu_MouseMove(object sender, MouseEventArgs e)
        {
            MyLib._myFlowLayoutPanel __obj = (MyLib._myFlowLayoutPanel)sender;
            __obj.Focus();
        }
    }

    public delegate void _menuButtonclick(string menuId, string menuText, string menuTag);
}
