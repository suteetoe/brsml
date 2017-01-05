using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Data;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace _viewCapture
{
    public partial class _viewScreenThumbnail : UserControl
    {
        public string _computerName = "";

        public _viewScreenThumbnail(string computerName)
        {
            InitializeComponent();
            //
            this._computerName = computerName;
            this._infoLabel.Text = computerName;
            //
            this._load();
        }

        void _load()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            switch (__myFrameWork._databaseSelectType)
            {
                case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                    {
                        string __query = "select convert(varchar(max)," + MyLib._d.sml_screen_realtime._screen_thumbnail + ",\'varbinary(max)\') as " + MyLib._d.sml_screen_realtime._screen_thumbnail + "," + MyLib._d.sml_screen_realtime._update_time + "," + MyLib._d.sml_screen_realtime._computer_name + " from " + MyLib._d.sml_screen_realtime._table + " where " + MyLib._d.sml_screen_realtime._computer_name + "=\'" + this._computerName + "\'";
                        DataTable __screenList = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
                        if (__screenList.Rows.Count > 0)
                        {
                            try
                            {
                                MemoryStream __bgms = new MemoryStream((byte[])Convert.FromBase64String(__screenList.Rows[0][MyLib._d.sml_screen_realtime._screen_thumbnail].ToString()));
                                this._pictureBox.Image = Image.FromStream(__bgms);
                                this._pictureBox.Invalidate();
                                IFormatProvider __culture = new CultureInfo("en-US");
                                this._infoLabel.Text = __screenList.Rows[0][MyLib._d.sml_screen_realtime._computer_name].ToString() + " : " + MyLib._myGlobal._convertDate(__screenList.Rows[0][MyLib._d.sml_screen_realtime._update_time].ToString()).ToString("HH:mm:ss", __culture);
                            }
                            catch
                            {
                            }
                        }
                    }
                    break;
                case MyLib._myGlobal._databaseType.PostgreSql:
                    {
                        string __query = "select encode(" + MyLib._d.sml_screen_realtime._screen_thumbnail + ",\'base64\') as " + MyLib._d.sml_screen_realtime._screen_thumbnail + "," + MyLib._d.sml_screen_realtime._update_time + "," + MyLib._d.sml_screen_realtime._computer_name + " from " + MyLib._d.sml_screen_realtime._table + " where " + MyLib._d.sml_screen_realtime._computer_name + "=\'" + this._computerName + "\'";
                        DataTable __screenList = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
                        if (__screenList.Rows.Count > 0)
                        {
                            try
                            {
                                MemoryStream __bgms = new MemoryStream((byte[])Convert.FromBase64String(__screenList.Rows[0][MyLib._d.sml_screen_realtime._screen_thumbnail].ToString()));
                                this._pictureBox.Image = Image.FromStream(__bgms);
                                this._pictureBox.Invalidate();
                                IFormatProvider __culture = new CultureInfo("en-US");
                                this._infoLabel.Text = __screenList.Rows[0][MyLib._d.sml_screen_realtime._computer_name].ToString() + " : " + MyLib._myGlobal._convertDate(__screenList.Rows[0][MyLib._d.sml_screen_realtime._update_time].ToString()).ToString("HH:mm:ss", __culture);
                            }
                            catch
                            {
                            }
                        }
                    }
                    break;
            }
        }

        private void _pictureBox_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + MyLib._d.sml_guid._guid_code + " from " + MyLib._d.sml_guid._table + " where " + MyLib._d.sml_guid._computer_name + "=\'" + this._computerName + "\'";
            DataTable __data = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
            if (__data.Rows.Count > 0)
            {
                string __guid = __data.Rows[0][MyLib._d.sml_guid._guid_code].ToString();
                MyLib._screenCaptureForm __show = new MyLib._screenCaptureForm(__guid);
                __show.ShowDialog();
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            this._load();
        }
    }
}
