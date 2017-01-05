using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _screenCaptureForm : Form
    {
        private string _guid;
        Thread _thread = null;
        string _message = "";
        Boolean _refreshEnable = false;

        public _screenCaptureForm(string guid)
        {
            this._guid = guid;
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this.Load += new EventHandler(_screenCaptureForm_Load);
            this.Disposed += new EventHandler(_screenCaptureForm_Disposed);
        }

        void _screenCaptureForm_Disposed(object sender, EventArgs e)
        {
            if (this._thread != null)
            {
                this._thread.Abort();
            }
        }

        void _screenCaptureForm_Load(object sender, EventArgs e)
        {
            this._requestCapture();
        }

        private void _process()
        {
            this._refreshEnable = false;
            try
            {
                this._message = "Request to Client. please wait....";
                //
                _myFrameWork __myFrameWork = new _myFrameWork();
                while (true)
                {
                    Thread.Sleep(1000);
                    string __query2 = "select " + _d.sml_screen_capture._request + ",encode(" + _d.sml_screen_capture._capture_screen + ",\'base64\') as " + _d.sml_screen_capture._capture_screen + " from " + _d.sml_screen_capture._table + " where " + _d.sml_screen_capture._guid_code + "=\'" + this._guid + "\' and " + _d.sml_screen_capture._request + "=2 order by roworder desc limit 1";
                    DataTable __dt = __myFrameWork._query(_myGlobal._mainDatabase, __query2).Tables[0];
                    if (__dt.Rows.Count > 0)
                    {
                        MemoryStream __bgms = new MemoryStream((byte[])Convert.FromBase64String(__dt.Rows[0][_d.sml_screen_capture._capture_screen].ToString()));
                        Image __image = Image.FromStream(__bgms);
                        this._pictureBox.Image = __image;
                        break;
                    }
                }
                this._message = "";
            }
            catch (Exception __ex)
            {
                this._message = "Fail : " + __ex.Message.ToString();
            }
            this._refreshEnable = true;
        }

        private void _requestCapture()
        {
            if (this._thread != null)
            {
                this._thread.Abort();
                this._thread = null;
            }
            _thread = new Thread(this._process);
            _thread.Start();
            //

            _myFrameWork __myFrameWork = new _myFrameWork();
            StringBuilder __queryUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _d.sml_screen_capture._table + " (" + _myGlobal._fieldAndComma(_d.sml_screen_capture._guid_code, _d.sml_screen_capture._request) + ") values (" + _myGlobal._fieldAndComma("\'" + this._guid + "\'", "1") + ")"));
            __queryUpdate.Append("</node>");
            __myFrameWork._queryList(MyLib._myGlobal._mainDatabase, __queryUpdate.ToString());
        }

        private void _refreshButton_Click(object sender, EventArgs e)
        {
            this._requestCapture();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            this._statusLabel.Text = this._message;
            this._refreshButton.Enabled = this._refreshEnable;
        }
    }
}
