using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Web.Services;
using System.Drawing.Imaging;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Threading;
using WIALib;
using System.Runtime.InteropServices;
using DShowNET;
using DShowNET.Device;


namespace SMLERPControl
{
    public partial class _getPicture : UserControl
    {
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ArrayList x_list_x = new ArrayList();
        private PictureBox _mypic = new PictureBox();
        public int _displaypic;
        MemoryStream ms;
        public bool _picenabled = false;
        protected int lastX = 0;
        protected int lastY = 0;
        protected string lastFilename = String.Empty;
        protected bool validData;
        private int totalpic = 1;
        private bool __isWebcam = false;
        private bool __isScanner = false;
        public string _Tablename = "";
        public bool _checkClick = true;
        public int _pictureCount = 0;
        _WebCam __cam;

        Boolean __isScannerWIADriver = false;

        public bool _isScanner
        {
            get { return __isScanner; }
            set { __isScanner = value; }
        }

        public bool _isWebcam
        {
            get { return __isWebcam; }
            set { __isWebcam = value; }
        }
        public _getPicture()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            //  this.webCamCapture1.ImageCaptured += new WebCamCapture.WebCamEventHandler(webCamCapture1_ImageCaptured);
            this._pictureZoom.Invalidated += new InvalidateEventHandler(_pictureZoom_Invalidated);
            this._pictureZoom.DoubleClick += new EventHandler(_pictureZoom_DoubleClick);
            //  __isWebcam=  __ischeckWebcamIswork();
            //_isWebcam = this.webCamCapture1._checkDeviceWebCam();
            // __checkWebcamDevice();
            // _isWebcam = true;
            __isScanner = _checkScanner();
            __isScannerWIADriver = _checkScannerWIA();


        }
        void __checkWebcamDevice()
        {
            ArrayList capDevices;
            if (!DsUtils.IsCorrectDirectXVersion())
            {
                this.__isWebcam = false;
                return;
            }

            if (!DsDev.GetDevicesOfCat(FilterCategory.VideoInputDevice, out capDevices))
            {
                this.__isWebcam = false;
                return;
            }

            DsDevice dev = null;
            if (capDevices.Count == 1)
            {
                dev = capDevices[0] as DsDevice;
                this.__isWebcam = true;
            }
            else
            {

            }
        }
        void _pictureZoom_DoubleClick(object sender, EventArgs e)
        {
            if (this._pictureZoom.Image != null)
            {
                _getPictureFullScreen __newScreen = new _getPictureFullScreen();
                __newScreen._pictureBox.Image = this._pictureZoom.Image;
                __newScreen._pictureBox.Size = new Size(this._pictureZoom.Image.Width, _pictureZoom.Image.Height);
                __newScreen.TopMost = true;
                __newScreen.ShowDialog();
            }
        }

        void _pictureZoom_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (this._pictureZoom.Image != null)
            {
                if (this._pictureZoom.SizeMode == PictureBoxSizeMode.StretchImage)
                {
                    this._pictureZoom.Size = new Size((int)(this._pictureZoom.Image.Width * this.calRatio) - 10, (int)(_pictureZoom.Image.Height * this.calRatio) - 10);
                }
                else
                {
                    this._pictureZoom.Size = new Size(this._pictureZoom.Image.Width, _pictureZoom.Image.Height);
                }
            }
        }

        void webCamCapture1_ImageCaptured(object source, WebcamEventArgs e)
        {

            this._mypic.Image = e.WebCamImage;
        }
        [Category("_SML")]
        [Description("จำนวนที่ต้องการสร้าง PictureBox จำนวน สูงสุด 8 รูป")]
        [DefaultValue(1)]
        public int _DisplayPictureAmount
        {
            get
            {
                return _displaypic;
            }
            set
            {
                this._displaypic = value;
                _createControlpic(_mypic, this._displaypic);
            }

        }
        void _createControlpic(PictureBox _pic, int amount)
        {
            int xwidth = 0;
            this.totalpic = amount;
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.AutoSize = true;
            for (int x = 0; x < amount; x++)
            {

                _pic.Location = new System.Drawing.Point(xwidth, this.toolStrip1.Location.Y + this.toolStrip1.Height + 10);
                _pic.Name = x.ToString();
                //_pic.Size = new System.Drawing.Size(110, 108);
                _pic.Size = new System.Drawing.Size(50, 50);
                _pic.BackColor = System.Drawing.Color.Transparent;
                _pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                _pic.ContextMenuStrip = contextMenuStrip1;
                _pic.TabIndex = 1;
                _pic.TabStop = false;
                _pic.SizeMode = PictureBoxSizeMode.Zoom;
                _pic.DragEnter += new DragEventHandler(_pic_DragEnter);
                _pic.DragDrop += new DragEventHandler(_pic_DragDrop);
                _pic.MouseEnter += new EventHandler(_pic_MouseEnter);
                _pic.Click += new EventHandler(_pic_Click);
                _pic.AllowDrop = true;
                _pic.BringToFront();

                this.flowLayoutPanel1.Controls.Add(_pic);
                xwidth += _pic.Width + 10;
                _pic = new System.Windows.Forms.PictureBox();
            }
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, this.toolStrip1.Location.Y + this.toolStrip1.Height + 10);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(681, 112);
            this.Controls.Add(flowLayoutPanel1);
        }

        void _pic_Click(object sender, EventArgs e)
        {
            if (this._checkClick)
            {
                if (this._picenabled == false && ((PictureBox)(sender)).Image != null)
                {

                    MyLib._myGlobal._displayWarning(4, null);


                }
                else
                {


                }
            }
        }

        void _showPicture(PictureBox mybox)
        {
            if (mybox.Image != null)
            {
                this._txtSize.Text = _getSizeBmp((Bitmap)(mybox.Image)) + " KB";
                this.txtHeight.Text = mybox.Image.Size.Height.ToString();
                this.txtWidth.Text = mybox.Image.Size.Width.ToString();
                _pictureZoom.Image = mybox.Image;
                _pictureZoom.Invalidate();
                this.txtHeight.Text = mybox.Image.Size.Height.ToString();
                this.txtWidth.Text = mybox.Image.Size.Width.ToString();
            }
            else
            {

                this.txtHeight.Text = "";
                this.txtWidth.Text = "";
                this._txtSize.Text = "";
            }
        }

        void _pic_MouseEnter(object sender, EventArgs e)
        {
            _showPicture((PictureBox)sender);
        }

        private string _getSizeBmp(Bitmap _image)
        {

            string __xresult = "";
            _g.g.BMPXMLSerialization bmpx = new _g.g.BMPXMLSerialization(new Bitmap(_image));
            float _float = Convert.ToSingle(bmpx.BMPBytes.Length);
            double __doublebyte = (double)((_float) / 1024);
            string getFormat = MyLib._myGlobal._getFormatNumber("m01");
            object data = Double.Parse(String.Format(getFormat, (double)__doublebyte));
            __xresult = data.ToString();
            return __xresult;
        }

        void _pic_DragEnter(object sender, DragEventArgs e)
        {
            Debug.WriteLine("OnDragEnter");
            string filename;
            validData = GetFilename(out filename, e);
            if (validData)
            {
                if (lastFilename != filename)
                {

                    lastFilename = filename;

                }
                else
                {

                }
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        void _pic_DragDrop(object sender, DragEventArgs e)
        {
            string xname = ((Control)(sender)).Name;
            if (validData)
            {
                foreach (Control getControl in flowLayoutPanel1.Controls)
                {
                    PictureBox getpic = new PictureBox();
                    getpic = (PictureBox)getControl;
                    if (getpic.Name == xname)
                    {
                        getpic.Image = Image.FromStream(__getMemoryStream(lastFilename));
                        break;
                    }
                }
            }
        }

        protected bool GetFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = String.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {

                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp") || (ext == ".gif"))
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }

        private string scandirectory(string _guid_formdatabase)
        {
            string x_return = "";
            DirectoryInfo _dir = new DirectoryInfo(strPathname);
            FileInfo[] rgFiles = _dir.GetFiles(_guid_formdatabase + ".jpg");
            for (int x = 0; x < rgFiles.Length; x++)
            {
                string filename = rgFiles[x].Name.Substring(0, rgFiles[x].Name.Length - 4);
                if (_guid_formdatabase == filename)
                {
                    x_return = rgFiles[x].Name;
                }
                else
                {
                }

            }
            return x_return;
        }

        private void scandirectory_delete()
        {

            DirectoryInfo _dir = new DirectoryInfo(strPathname);
            FileInfo[] rgFiles = _dir.GetFiles("*.jpg");

            for (int x = 0; x < rgFiles.Length; x++)
            {
                string filename = rgFiles[x].Name;
                for (int j = 0; j < x_list_x.Count; j++)
                {
                    string del = x_list_x[j].ToString();
                    if (del.CompareTo(filename) == 0)
                    {

                        rgFiles[x].Delete();
                    }
                }
            }
        }
        string _getCode = "";
        void _imgebytefromdatabaseThread()
        {
            try
            {
                string __tabble = this._Tablename.Length == 0 ? "images" : this._Tablename;
                byte[] __databyte = new byte[1024];
                string __xquery = "select guid_code from " + __tabble + " where image_id = \'" + this._getCode + "\' order by image_order";
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, __xquery);
                DataTable __dt = __ds.Tables[0];

                x_list_x.Clear();
                string xcompare = "";
                this._pictureCount = __dt.Rows.Count;

                for (int __xloop = 0; __xloop < __dt.Rows.Count; __xloop++)
                {
                    string __guid_code = __dt.Rows[__xloop][0].ToString();
                    string __scran = scandirectory(__guid_code);
                    if (__scran.Length != 0)
                        xcompare = __scran.Substring(0, __scran.Length - 4);

                    if (xcompare.Equals(__guid_code))
                    {
                        if (xcompare.CompareTo("noimage") != 0)
                        {
                            _g.g.BMPXMLSerialization bmpx = new _g.g.BMPXMLSerialization(new Bitmap(Image.FromFile(strPathname + "\\" + __scran)));
                            __databyte = bmpx.BMPBytes;
                        }
                    }
                    else
                    {
                        if (xcompare.Equals(__guid_code) == false)
                        {
                            string __qurey = "select Image_file from " + __tabble + " where guid_code ='" + __guid_code + "'";
                            __databyte = __myFrameWork._ImageByte(MyLib._myGlobal._databaseName, __qurey);
                            FileStream oOutput = File.Create(this.strPathname + @"\" + __guid_code + ".jpg", __databyte.Length);
                            oOutput.Write(__databyte, 0, __databyte.Length);
                            oOutput.Close();
                            oOutput.Dispose();
                        }
                    }
                    try
                    {
                        PictureBox getpic = (PictureBox)flowLayoutPanel1.Controls[__xloop];
                        if (getpic.Name.Equals(__xloop.ToString()))
                        {
                            if (__guid_code.Equals("noimage"))
                            {
                                getpic.Image = null;
                            }
                            else
                            {
                                getpic.Image = Image.FromStream(new MemoryStream(__databyte));
                                getpic.Tag = __guid_code;

                                if (__xloop == 0)
                                {
                                    this._showPicture(getpic);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                }
                __dt.Dispose();
                __ds.Dispose();
            }
            catch
            {
            }
        }
        Thread __getThread;
        private void _imgebytefromdatabase(string _code)
        {
            this._getCode = _code;
            /*try
            {
                if (__getThread != null)
                {
                    __getThread.Abort();
                }
            }
            catch
            {
            }
            __getThread = new Thread(new ThreadStart(_imgebytefromdatabaseThread));
            __getThread.Start();*/
            // ใช้ Thread แล้วมัน Hang ว่างๆ ค่อยหาใหม่เด้อ จืด
            _imgebytefromdatabaseThread();
        }

        private string strPathname = @"c:\smltemp";
        private void _createFlodertem()
        {
            try
            {
                if (!Directory.Exists(strPathname))
                {
                    Directory.CreateDirectory(strPathname);
                }
            }
            catch
            {
            }
        }

        MemoryStream __getMemoryStream(string namefile)
        {
            FileStream oImg;
            BinaryReader oBinaryReader;
            byte[] oImgByteArray;

            oImg = new FileStream(namefile, FileMode.Open, FileAccess.Read);
            oBinaryReader = new BinaryReader(oImg);
            oImgByteArray = oBinaryReader.ReadBytes((int)oImg.Length);
            ms = new MemoryStream(oImgByteArray);
            oBinaryReader.Close();
            oImg.Close();
            return ms;
        }

        private void cccToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem mybox = (ToolStripMenuItem)(sender);

            foreach (Control getControl in flowLayoutPanel1.Controls)
            {
                if (getControl.GetType() == typeof(PictureBox))
                {
                    PictureBox getpic = new PictureBox();
                    getpic = (PictureBox)getControl;


                    if (contextMenuStrip1.SourceControl == getpic)
                    {

                        getpic.Image = null;
                    }


                }
            }
            _pictureZoom.Image = null;
        }

        private IDataObject tempObj;

        public void _loadImage(string _code)
        {
            ListType = null;
            _createFlodertem();
            _imgebytefromdatabase(_code);
        }

        private MyLib.SMLJAVAWS.imageType[] ListType;
        private MyLib.SMLJAVAWS.imageType TypeImage;
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip mySender = (ContextMenuStrip)(sender);
            PictureBox getpic = new PictureBox();
            if (mySender.SourceControl.GetType() == typeof(PictureBox))
            {
                getpic = (PictureBox)mySender.SourceControl;
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[0].Visible = this._picenabled;
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[1].Visible = this._picenabled;
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[2].Visible = this._picenabled;
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[3].Visible = this._picenabled;
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[4].Visible = this._picenabled;
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[4].Enabled = (__isScanner || __isScannerWIADriver) ? true : false;
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[3].Enabled = true;
                //   contextMenuStrip1.SourceControl.ContextMenuStrip.Items[4].Enabled = this.__isWebcam;
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[0].Text = MyLib._myGlobal._resource("ลบ");
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[2].Text = MyLib._myGlobal._resource("เลือกรูป");
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[3].Text = MyLib._myGlobal._resource("กล้อง");
                //    contextMenuStrip1.SourceControl.ContextMenuStrip.Items[4].Text = "จับภาพ";
                contextMenuStrip1.SourceControl.ContextMenuStrip.Items[4].Text = MyLib._myGlobal._resource("สแกนภาพ");
                if (getpic.Image == null)
                {

                    contextMenuStrip1.SourceControl.ContextMenuStrip.Items[0].Enabled = false;
                }
                else
                {
                    contextMenuStrip1.SourceControl.ContextMenuStrip.Items[0].Enabled = true;
                }
            }

        }

        public string _updateImage(string _guid_code)
        {
            string __tabble = this._Tablename.Length == 0 ? "images" : this._Tablename;
            _createFlodertem();
            string x_result = "";
            ArrayList xlist = new ArrayList();
            byte[] getData;
            string xxSystem = "";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            for (int __xloop = 0; __xloop < flowLayoutPanel1.Controls.Count; __xloop++)
            {
                PictureBox getpic = new PictureBox();
                getpic = (PictureBox)flowLayoutPanel1.Controls[__xloop];
                xxSystem = System.Guid.NewGuid().ToString();

                // check duplicate guid
               

                bool _xcheckedImage = false;
                if (getpic.Image != null)
                {
                    if (getpic.Tag == null)
                    {
                        while (true)
                        {
                            string __checkGuidDuplicateQuery = "select guid_code from " + __tabble + " where guid_code ='" + xxSystem + "'";
                            DataTable __resultCheck = __myFrameWork._queryShort(__checkGuidDuplicateQuery).Tables[0];
                            if (__resultCheck.Rows.Count == 0)
                            {
                                break;
                            }
                            xxSystem = System.Guid.NewGuid().ToString();
                        }

                    }

                    _g.g.BMPXMLSerialization bmpx = new _g.g.BMPXMLSerialization(new Bitmap(getpic.Image));
                    getData = bmpx.BMPBytes;
                    TypeImage = new MyLib.SMLJAVAWS.imageType();
                    TypeImage._databyteImage = getData;
                    TypeImage._code = (getpic.Tag != null && getpic.Tag.ToString() != "") ? getpic.Tag.ToString() : xxSystem;

                    xlist.Add(TypeImage);
                    if (_xcheckedImage) getpic.Image = null;
                    ListType = ((MyLib.SMLJAVAWS.imageType[])xlist.ToArray(typeof(MyLib.SMLJAVAWS.imageType)));
                }
                else if (getpic.Tag != null && getpic.Tag.ToString() != "")
                {
                    // delete from guid
                    string __qurey = "delete from " + __tabble + " where guid_code ='" + getpic.Tag.ToString() + "'";
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __qurey);
                }
            }
            string[] xfeild = { "image_id", "image_file", "guid_code" };//insert
            string xwhere = "image_id";//update
            string xTable = (this._Tablename.Length == 0) ? "images" : this._Tablename;//update
            string insertorupdate = "0";
            string xswheredata = _guid_code;

            //MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            if (ListType != null)
                x_result = __myFrameWork._SaveImageList(_guid_code, MyLib._myGlobal._databaseName, ListType, insertorupdate, xfeild, xTable, xwhere, xswheredata);
            // }
            return x_result;
        }
        public void _setEnable(bool _xenable)
        {
            this.toolStrip1.Enabled = _xenable;
            // this.flowLayoutPanel1.Enabled = _xenable;
            this._pictureZoom.Image = null;
            this._picenabled = _xenable;
        }
        public void _clearpic()
        {
            this._pictureCount = 0;
            foreach (Control getControl in flowLayoutPanel1.Controls)
            {
                if (getControl.GetType() == typeof(PictureBox))
                {
                    PictureBox getpic = new PictureBox();
                    getpic = (PictureBox)getControl;
                    getpic.Image = null;
                    getpic.Tag = null;
                }
            }
            this._pictureZoom.Image = null;
            this.txtHeight.Text = "";
            this.txtWidth.Text = "";
            this._txtSize.Text = "";
        }
        float calRatio
        {
            get
            {
                float __ratioWidth = 1.0f;
                float __ratioHeight = 1.0f;
                if (this._pictureZoom.Image != null)
                {
                    if (this._pictureZoom.Image.Width > this.panel1.Width)
                    {
                        __ratioWidth = (float)this.panel1.Width / (float)this._pictureZoom.Image.Width;
                    }
                    if (this._pictureZoom.Image.Height > this.panel1.Height)
                    {
                        __ratioHeight = (float)this.panel1.Height / (float)this._pictureZoom.Image.Height;
                    }
                    float __result = (__ratioHeight < __ratioWidth) ? __ratioHeight : __ratioWidth;
                    return __result;
                }
                return 1.0f;
            }
        }
        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            if (this._pictureZoom.Image != null)
            {
                float __calcRatio = this.calRatio;
                this._pictureZoom.Size = new Size((int)(this._pictureZoom.Image.Width * __calcRatio) - 10, (int)(_pictureZoom.Image.Height * __calcRatio) - 10);
            }
        }


        private void toolStripMyButton1_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string[] filename;
            openFileDialog1.Multiselect = true;
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.bmp;*.jpg;*.jpeg;*.gif;*.png";
            openFileDialog1.FilterIndex = this.totalpic;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileNames;
                for (int x_name = 0; x_name < filename.Length; x_name++)
                {
                    foreach (Control getControl in flowLayoutPanel1.Controls)
                    {
                        if (getControl.GetType() == typeof(PictureBox))
                        {
                            PictureBox getpic = new PictureBox();
                            getpic = (PictureBox)getControl;
                            if (getpic.Name == x_name.ToString())
                            {
                                if ((myStream = openFileDialog1.OpenFile()) != null)
                                {
                                    getpic.Image = Image.FromStream(__getMemoryStream(filename[x_name]));
                                    __getMemoryStream(filename[x_name]).Close();
                                    myStream.Close();
                                }

                            };
                        }
                    }
                }
            }

        }

        private void toolStripMyButton3_Click(object sender, EventArgs e)
        {
            PictureBox getpic = new PictureBox();
            tempObj = Clipboard.GetDataObject();
            Array data = ((IDataObject)tempObj).GetData("FileDrop") as Array;
            if (data != null)
            {
                int __length = 0;
                if (data.Length > this._displaypic)
                {
                    __length = this._displaypic;
                }
                else
                {
                    __length = data.Length;
                }
                for (int __x = 0; __x < __length; __x++)
                {
                    string filename = ((string[])data)[__x];
                    string ext = Path.GetExtension(filename).ToLower();
                    if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp") || (ext == ".gif"))
                    {
                        getpic = (PictureBox)flowLayoutPanel1.Controls[__x];
                        getpic.Image = Image.FromStream(__getMemoryStream(filename));
                        __getMemoryStream(filename).Close();

                    }
                }
            }
        }

        private void pathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox getpic = new PictureBox();
            if (Clipboard.ContainsImage())
            {
                getpic = (PictureBox)contextMenuStrip1.SourceControl;
                getpic.Image = Clipboard.GetImage();

            }
            else
            {
                tempObj = Clipboard.GetDataObject();
                Array data = ((IDataObject)tempObj).GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        string filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp") || (ext == ".gif"))
                        {
                            getpic = (PictureBox)contextMenuStrip1.SourceControl;
                            getpic.Image = Image.FromStream(__getMemoryStream(filename));
                            __getMemoryStream(filename).Close();
                            // ret = true;
                        }
                        else
                        {
                            MessageBox.Show("invalided fomat");
                        }
                    }
                }
            }
            // Clipboard.Clear();
        }

        private void DisposeImage(PictureBox pictureBox)
        {
            // disable "Save As" menu entry
            Image oldImg = pictureBox.Image;
            pictureBox.Image = null;					// empty picture box
            if (oldImg != null)
                oldImg.Dispose();						// dispose old image (free memory, unlock file)

            if (imageFileName != null)
            {				// try to delete the temporary image file
                try
                {
                    File.Delete(imageFileName);
                }
                catch (Exception)
                { }
            }
        }


        /// <summary> temporary image file. </summary>
        private string imageFileName;

        private void browsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string[] filename;
            openFileDialog1.Multiselect = false;
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.bmp;*.jpg;*.jpeg;*.gif;*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileNames;
                PictureBox getpic = new PictureBox();
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {

                    getpic = (PictureBox)contextMenuStrip1.SourceControl;
                    getpic.Image = Image.FromStream(__getMemoryStream(filename[0]));
                    __getMemoryStream(filename[0]).Close();
                    myStream.Close();

                }
            }
        }
        private PictureBox __PictureBox = new PictureBox();
        private void webCamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control __getControl in flowLayoutPanel1.Controls)
            {
                if (__getControl.GetType() == typeof(PictureBox))
                {
                    PictureBox _getpic = new PictureBox();
                    _getpic = (PictureBox)__getControl;
                    if (contextMenuStrip1.SourceControl == _getpic)
                    {
                        this._mypic = _getpic;
                        __cam = new _WebCam();
                        __cam.ImageCaptured += new _WebCam.WebCamEventHandler(__frmcamara_ImageCaptured);
                        __cam.ShowDialog();
                        __cam.Dispose();
                        /*this.webCamCapture1.CaptureHeight = 200;
                        this.webCamCapture1.CaptureWidth = 200;
                        this.webCamCapture1.TimeToCapture_milliseconds = 20;
                         start the video capture. let the control handle the
                         frame numbers.
                         getpic = __PictureBox;
                         this._mypic = __PictureBox;
                          this.webCamCapture1.Start(0);*/
                    }
                }
            }
        }

        void __frmcamara_ImageCaptured(object source, WebcamEventArgs e)
        {
            this._mypic.Image = e.WebCamImage;
        }

        private void scannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control getControl in flowLayoutPanel1.Controls)
            {
                if (getControl.GetType() == typeof(PictureBox))
                {
                    PictureBox getpic = new PictureBox();
                    getpic = (PictureBox)getControl;
                    if (contextMenuStrip1.SourceControl == getpic)
                    {
                        if (__isScannerWIADriver)
                        {
                            // WIA Method
                            try
                            {
                                const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
                                WIA.CommonDialogClass wiaDiag = new WIA.CommonDialogClass();

                                WIA.ImageFile wiaImage = null;

                                wiaImage = wiaDiag.ShowAcquireImage(
                                        WIA.WiaDeviceType.UnspecifiedDeviceType,
                                        WIA.WiaImageIntent.GrayscaleIntent,
                                        WIA.WiaImageBias.MaximizeQuality,
                                        wiaFormatJPEG, true, true, false);

                                if (wiaImage != null)
                                {
                                    WIA.Vector vector = wiaImage.FileData;

                                    //Image i = Image.FromStream(new MemoryStream((byte[])vector.get_BinaryData()));
                                    //i.Save("");

                                    getpic.Image = Image.FromStream(new MemoryStream((byte[])vector.get_BinaryData()));
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            WiaClass wiaManager = null;		// WIA manager COM object
                            CollectionClass wiaDevs = null;		// WIA devices collection COM object
                            ItemClass wiaRoot = null;		// WIA root device COM object
                            CollectionClass wiaPics = null;		// WIA collection COM object
                            ItemClass wiaItem = null;		// WIA image COM object

                            try
                            {
                                wiaManager = new WiaClass();		// create COM instance of WIA manager

                                wiaDevs = wiaManager.Devices as CollectionClass;			// call Wia.Devices to get all devices
                                if ((wiaDevs == null) || (wiaDevs.Count == 0))
                                {
                                    MessageBox.Show(this, "No WIA devices found!", "WIA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    //Application.Exit();
                                    return;
                                }

                                object selectUsingUI = System.Reflection.Missing.Value;			// = Nothing
                                wiaRoot = (ItemClass)wiaManager.Create(ref selectUsingUI);	// let user select device
                                if (wiaRoot == null)											// nothing to do
                                    return;

                                // this call shows the common WIA dialog to let the user select a picture:
                                wiaPics = wiaRoot.GetItemsFromUI(WiaFlag.SingleImage, WiaIntent.ImageTypeColor) as CollectionClass;
                                if (wiaPics == null)
                                    return;

                                bool takeFirst = true;						// this sample uses only one single picture
                                foreach (object wiaObj in wiaPics)			// enumerate all the pictures the user selected
                                {
                                    if (takeFirst)
                                    {
                                        DisposeImage(getpic);						// remove previous picture
                                        wiaItem = (ItemClass)Marshal.CreateWrapperOfType(wiaObj, typeof(ItemClass));
                                        imageFileName = Path.GetTempFileName();				// create temporary file for image
                                        Cursor.Current = Cursors.WaitCursor;				// could take some time
                                        this.Refresh();
                                        wiaItem.Transfer(imageFileName, false);			// transfer picture to our temporary file
                                        getpic.Image = Image.FromFile(imageFileName);	// create Image instance from file					// enable "Save as" menu entry
                                        takeFirst = false;									// first and only one done.
                                    }
                                    Marshal.ReleaseComObject(wiaObj);					// release enumerated COM object
                                }
                            }
                            catch (Exception ee)
                            {
                                MessageBox.Show(this, "Acquire from WIA Imaging failed\r\n" + ee.Message, "WIA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                // Application.Exit();
                            }
                            finally
                            {
                                if (wiaItem != null)
                                    Marshal.ReleaseComObject(wiaItem);		// release WIA image COM object
                                if (wiaPics != null)
                                    Marshal.ReleaseComObject(wiaPics);		// release WIA collection COM object
                                if (wiaRoot != null)
                                    Marshal.ReleaseComObject(wiaRoot);		// release WIA root device COM object
                                if (wiaDevs != null)
                                    Marshal.ReleaseComObject(wiaDevs);		// release WIA devices collection COM object
                                if (wiaManager != null)
                                    Marshal.ReleaseComObject(wiaManager);		// release WIA manager COM object
                                Cursor.Current = Cursors.Default;				// restore cursor
                            }
                        }
                    }
                }
            }
        }

        bool _checkScannerWIA()
        {
            bool __result = false;
            try
            {
                WIA.DeviceManager deviceManager = new WIA.DeviceManager();
                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
                {
                    //Add the device to the list if it is a scanner
                    if (deviceManager.DeviceInfos[i].Type == WIA.WiaDeviceType.ScannerDeviceType)
                    {
                        __result = true;
                        break;
                    }

                    //Devices.Items.Add(new Scanner(deviceManager.DeviceInfos[i]));
                }
            }
            catch
            {
            }
            return __result;
        }

        bool _checkScanner()
        {
            bool __result = true;
            WiaClass wiaManager = null;		// WIA manager COM object
            CollectionClass wiaDevs = null;		// WIA devices collection COM object
            ItemClass wiaRoot = null;		// WIA root device COM object
            CollectionClass wiaPics = null;		// WIA collection COM object
            ItemClass wiaItem = null;		// WIA image COM object           

            try
            {

                wiaManager = new WiaClass();		// create COM instance of WIA manager
            }
            catch (Exception ex)
            {
                __result = false;

            }
            this.__isScanner = __result;
            return __result;

        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox getpic = new PictureBox();
            if (Clipboard.ContainsImage())
            {
                getpic = (PictureBox)contextMenuStrip1.SourceControl;
                getpic.Image = Clipboard.GetImage();

            }
            else
            {
                tempObj = Clipboard.GetDataObject();
                Array data = ((IDataObject)tempObj).GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        string filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp") || (ext == ".gif"))
                        {
                            getpic = (PictureBox)contextMenuStrip1.SourceControl;
                            getpic.Image = Image.FromStream(__getMemoryStream(filename));
                            __getMemoryStream(filename).Close();
                            // ret = true;
                        }
                        else
                        {
                            MessageBox.Show("invalided fomat");
                        }
                    }
                }
            }
        }

        private void _buttonPictureMode_Click(object sender, EventArgs e)
        {
            if (this._pictureZoom.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                this._pictureZoom.SizeMode = PictureBoxSizeMode.Normal;
            }
            else
            {
                this._pictureZoom.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }

    public class _getImageData
    {
        public event _loadImageCompleteErgs _onLoadImageComplete;
        public event _loadImageNotFoundErgs _onLoadImageNotFound;

        public string _imageCode = "";
        private string _imageGUID = "";
        private string _Tablename = "images";
        Thread _processThread = null;
        Image _image = null;
        private string _strPathname = @"c:\smltemp";
        public DataTable _guidList = null;
        public int _imageCount = 0;

        public _getImageData(string code)
        {
            _imageCode = code;
        }

        public _getImageData(string code, string tableName)
        {
            _imageCode = code;
            _Tablename = tableName;
        }

        public void _process()
        {
            _processThread = new Thread(_getImageProcess);
            _processThread.Start();
        }

        public void _getImageProcess()
        {
            try
            {
                int __addr = -1;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // download image
                string __tabble = this._Tablename.Length == 0 ? "images" : this._Tablename;
                byte[] __databyte = new byte[1024];
                DataTable __dt = this._guidList;
                if (__dt == null)
                {
                    string __xquery = "select guid_code from " + __tabble + " where image_id = \'" + this._imageCode + "\'";
                    DataSet __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, __xquery);
                    __dt = __ds.Tables[0];
                    __addr = 0;
                    this._imageCount = __dt.Rows.Count;
                }
                else
                {
                    for (int __loop = 0; __loop < __dt.Rows.Count; __loop++)
                    {
                        if (__dt.Rows[__loop][1].ToString().Equals(this._imageCode))
                        {
                            __addr = __loop;
                            break;
                        }
                    }
                }
                //x_list_x.Clear();
                string __xcompare = "";
                if (__addr != -1)
                {
                    // กรณีไม่มีรูป
                    if (__dt.Rows.Count == 0)
                    {
                        if (_onLoadImageNotFound != null)
                        {
                            _onLoadImageNotFound(this);
                        }
                    }

                    string __guid_code = __dt.Rows[__addr][0].ToString();
                    string __scran = _scandirectory(__guid_code);
                    if (__scran.Length != 0)
                    {
                        __xcompare = __scran.Substring(0, __scran.Length - 4);
                    }
                    if (__xcompare.Equals(__guid_code))
                    {
                        if (__xcompare.CompareTo("noimage") != 0)
                        {
                            _g.g.BMPXMLSerialization __bmpx = new _g.g.BMPXMLSerialization(new Bitmap(Image.FromFile(_strPathname + "\\" + __scran)));
                            __databyte = __bmpx.BMPBytes;
                        }
                    }
                    else
                    {
                        if (__xcompare.Equals(__guid_code) == false)
                        {
                            string __qurey = "select Image_file from " + __tabble + " where guid_code ='" + __guid_code + "'";
                            __databyte = __myFrameWork._ImageByte(MyLib._myGlobal._databaseName, __qurey);
                            FileStream oOutput = File.Create(this._strPathname + @"\" + __guid_code + ".jpg", __databyte.Length);
                            oOutput.Write(__databyte, 0, __databyte.Length);
                            oOutput.Close();
                            oOutput.Dispose();
                        }
                    }
                    try
                    {

                        if (__guid_code.Equals("noimage"))
                        {
                            _image = null;
                        }
                        else
                        {
                            _image = Image.FromStream(new MemoryStream(__databyte));

                        }

                    }
                    catch
                    {
                    }

                }

                //complete

                if (_onLoadImageComplete != null)
                {
                    _onLoadImageComplete(this, _image);
                }

            }
            catch
            {
            }
        }

        private string _scandirectory(string guid_formdatabase)
        {
            string __x_return = "";
            DirectoryInfo __dir = new DirectoryInfo(_strPathname);
            FileInfo[] __rgFiles = __dir.GetFiles(guid_formdatabase + ".jpg");
            for (int _x = 0; _x < __rgFiles.Length; _x++)
            {
                string _filename = __rgFiles[_x].Name.Substring(0, __rgFiles[_x].Name.Length - 4);
                if (guid_formdatabase == _filename)
                {
                    __x_return = __rgFiles[_x].Name;
                }
                else
                {
                }

            }
            return __x_return;
        }

        public Image _getImageNow()
        {
            _getImageProcess();
            return this._image;
        }
    }

    public delegate void _loadImageCompleteErgs(object sender, Image __imageBytea);
    public delegate void _loadImageNotFoundErgs(object sender);
}