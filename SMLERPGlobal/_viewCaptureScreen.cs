using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace _viewCapture
{
    public partial class _viewCaptureScreen : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        DataTable _screenList = null;
        int _pictureMax = 0;
        int _pictureAddr = 0;
        ImageList _imageList;

        public _viewCaptureScreen()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            DataTable __getUser = this._myFrameWork._query(MyLib._myGlobal._mainDatabase, "select " + MyLib._d.sml_user_list._user_code + "," + MyLib._d.sml_user_list._user_name + " from " + MyLib._d.sml_user_list._table + " order by " + MyLib._d.sml_user_list._user_code).Tables[0];
            for (int __row = 0; __row < __getUser.Rows.Count; __row++)
            {
                this._userListComboBox.Items.Add(__getUser.Rows[__row][MyLib._d.sml_user_list._user_code].ToString() + "," + __getUser.Rows[__row][MyLib._d.sml_user_list._user_name].ToString());
            }
            //
            this._userListComboBox.SelectedIndexChanged += new EventHandler(_userListComboBox_SelectedIndexChanged);
            this._imageDrawListBox.SelectedIndexChanged += new EventHandler(_imageDrawListBox_SelectedIndexChanged);
        }

        void _imageDrawListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._loadPicture(this._imageDrawListBox.SelectedIndex);
        }

        void _loadThumbnail(object parameter)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string[] __getParameter = ((string)parameter).Split(',');
                int __row = Int32.Parse(__getParameter[1].ToString());
                switch (__myFrameWork._databaseSelectType)
                {
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                        {
                            string __query = "select " + MyLib._d.sml_screen_capture._capture_screen_thumbnail  +" from " + MyLib._d.sml_screen_capture._table + " where roworder=" + __getParameter[0].ToString();
                            DataTable __dt = this._myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
                            byte[] _myImage = (byte[])__dt.Rows[0][MyLib._d.sml_screen_capture._capture_screen_thumbnail];
                        } 
                        break;
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        {
                            string __query = "select " + "encode(" + MyLib._d.sml_screen_capture._capture_screen_thumbnail + ",\'base64\') as " + MyLib._d.sml_screen_capture._capture_screen_thumbnail + " from " + MyLib._d.sml_screen_capture._table + " where roworder=" + __getParameter[0].ToString();
                            DataTable __dt = this._myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
                            MemoryStream __bgms = new MemoryStream((byte[])Convert.FromBase64String(__dt.Rows[0][MyLib._d.sml_screen_capture._capture_screen_thumbnail].ToString()));
                            this._imageList.Images[__row] = Image.FromStream(__bgms);
                            this._imageDrawListBox.Invalidate();
                        }
                        break;
                }
            }
            catch
            {
            }
        }

        void _userListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._userListComboBox.SelectedIndexChanged -= new EventHandler(_userListComboBox_SelectedIndexChanged);
            this._imageDrawListBox.SelectedIndexChanged -= new EventHandler(_imageDrawListBox_SelectedIndexChanged);
            //
            this._screenList = null;
            string[] __userCode = this._userListComboBox.SelectedItem.ToString().Split(',');
            if (__userCode.Length > 0)
            {
                string __query = "select " + MyLib._myGlobal._fieldAndComma("roworder", MyLib._d.sml_screen_capture._capture_time, MyLib._d.sml_screen_capture._computer_name, MyLib._d.sml_screen_capture._database_code) + " from " + MyLib._d.sml_screen_capture._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_screen_capture._user_code) + "=\'" + __userCode[0].ToString().ToUpper() + "\' order by " + MyLib._d.sml_screen_capture._capture_time;
                this._screenList = this._myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
                this._pictureAddr = this._pictureMax = this._screenList.Rows.Count - 1;
                this._imageDrawListBox.Items.Clear();
                this._imageDrawListBox.ItemHeight = 120;
                this._imageList = new ImageList();
                this._imageList.ImageSize = new Size(200, 100);
                this._imageList.ColorDepth = ColorDepth.Depth32Bit;
                this._imageDrawListBox.ImageList = this._imageList;
                for (int __row = 0; __row < this._screenList.Rows.Count; __row++)
                {
                    string __rowOrder = this._screenList.Rows[__row]["roworder"].ToString();
                    this._imageDrawListBox.Items.Add(new _listBoxItem(__rowOrder, this._screenList.Rows[__row][MyLib._d.sml_screen_capture._capture_time].ToString(), __row));
                    Bitmap __bitmap = new Bitmap(10, 10);
                    this._imageList.Images.Add(__bitmap);
                }
                this._imageDrawListBox.SelectedIndex = this._imageDrawListBox.Items.Count - 1;
                this._imageDrawListBox.SelectedIndex = -1;
                this._imageDrawListBox.Invalidate();
                for (int __row = this._screenList.Rows.Count - 1; __row >= 0; __row--)
                {
                    _listBoxItem __item = (_listBoxItem)this._imageDrawListBox.Items[__row];
                    Thread __loadByThread = new Thread(new ParameterizedThreadStart(_loadThumbnail));
                    __loadByThread.IsBackground = false;
                    __loadByThread.Name = "Load" + __item.ID.ToString();
                    __loadByThread.Start(__item.ID.ToString() + "," + __item.ImageIndex.ToString());
                }
            }
            //
            this._userListComboBox.SelectedIndexChanged += new EventHandler(_userListComboBox_SelectedIndexChanged);
            this._imageDrawListBox.SelectedIndexChanged += new EventHandler(_imageDrawListBox_SelectedIndexChanged);
        }

        void _loadPicture(int addr)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                this._pictureAddr = addr;
                this._totalLabel.Text = (this._pictureAddr + 1).ToString() + "/" + (this._pictureMax + 1).ToString();
                string __rowOrder = this._screenList.Rows[addr]["roworder"].ToString();
                string __query = "select encode(" + MyLib._d.sml_screen_capture._capture_screen + ",\'base64\') as " + MyLib._d.sml_screen_capture._capture_screen + " from " + MyLib._d.sml_screen_capture._table + " where roworder=" + __rowOrder;
                DataTable __dt = this._myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
                if (__dt.Rows.Count > 0)
                {
                    this._infoLabel.Text = String.Format("Date Time : {0},Computer Name : {1},Database Code : {2}", this._screenList.Rows[addr][MyLib._d.sml_screen_capture._capture_time].ToString(), this._screenList.Rows[addr][MyLib._d.sml_screen_capture._computer_name].ToString(), this._screenList.Rows[addr][MyLib._d.sml_screen_capture._database_code].ToString());
                    MemoryStream __bgms = new MemoryStream((byte[])Convert.FromBase64String(__dt.Rows[0][MyLib._d.sml_screen_capture._capture_screen].ToString()));
                    Image __image = Image.FromStream(__bgms);
                    this._pictureBox.Image = __image;
                }
            }
            catch
            {
            }
            this._prevButton.Enabled = (this._pictureAddr > 0) ? true : false;
            this._nextButton.Enabled = (this._pictureAddr < this._pictureMax) ? true : false;
            Cursor = Cursors.Arrow;
        }

        private void _prevButton_Click(object sender, EventArgs e)
        {
            this._loadPicture(this._pictureAddr - 1);
        }

        private void _nextButton_Click(object sender, EventArgs e)
        {
            this._loadPicture(this._pictureAddr + 1);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
