using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace _viewCapture
{
    public partial class _viewScreen : UserControl
    {
        public _viewScreen()
        {
            InitializeComponent();
            //
        }

        Boolean _findControl(string computerName)
        {
            foreach (Control __control in this._flowLayoutPanel.Controls)
            {
                if (__control.GetType() == typeof(_viewScreenThumbnail))
                {
                    _viewScreenThumbnail __compare = (_viewScreenThumbnail)__control;
                    if (__compare._computerName.Equals(computerName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            {
                string __query = "insert into " + MyLib._d.sml_screen_realtime._table + " (" + MyLib._d.sml_screen_realtime._computer_name + ") (select distinct " + MyLib._d.sml_guid._computer_name + " from " + MyLib._d.sml_guid._table + " where " + MyLib._d.sml_guid._computer_name + " not in (select " + MyLib._d.sml_screen_realtime._computer_name + " from " + MyLib._d.sml_screen_realtime._table + "))";
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
            }
            {
                string __query = "delete from " + MyLib._d.sml_screen_realtime._table + " where " + MyLib._d.sml_screen_realtime._computer_name + " not in (select distinct " + MyLib._d.sml_guid._computer_name + " from " + MyLib._d.sml_guid._table + ")";
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
            }
            {
                string __dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                string __query = "update " + MyLib._d.sml_screen_realtime._table + " set " + MyLib._d.sml_screen_realtime._request_time + "=\'" + __dateTime + "\'";
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
            }
            {
                string __query = "select " + MyLib._d.sml_screen_realtime._computer_name + " from " + MyLib._d.sml_screen_realtime._table + " order by " + MyLib._d.sml_screen_realtime._computer_name;
                DataTable __screenList = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
                // ลบ Control ทิ้ง กรณีไม่มีชื่อคอมพิวเตอร์แล้ว
                foreach (Control __control in this._flowLayoutPanel.Controls)
                {
                    if (__control.GetType() == typeof(_viewScreenThumbnail))
                    {
                        _viewScreenThumbnail __compare = (_viewScreenThumbnail)__control;
                        Boolean __found = false;
                        for (int __row = 0; __row < __screenList.Rows.Count; __row++)
                        {
                            string __computerName = __screenList.Rows[__row][MyLib._d.sml_screen_realtime._computer_name].ToString();
                            if (__compare._computerName.Equals(__computerName))
                            {
                                __found = true;
                                break;
                            }
                        }
                        if (__found == false)
                        {
                            __control.Dispose();
                        }
                    }
                }
                // เพิ่มคอมพิวเตอร์เข้าไปใหม่
                for (int __row = 0; __row < __screenList.Rows.Count; __row++)
                {
                    string __computerName = __screenList.Rows[__row][MyLib._d.sml_screen_realtime._computer_name].ToString();
                    if (this._findControl(__computerName) == false)
                    {
                        _viewScreenThumbnail __newControl = new _viewScreenThumbnail(__computerName);
                        this._flowLayoutPanel.Controls.Add(__newControl);
                    }
                }
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
