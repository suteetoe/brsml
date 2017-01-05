using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace SMLERPControl
{
    public partial class _importFromImageForm : Form
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        Thread _processThread;

        public _importFromImageForm()
        {
            InitializeComponent();
            //
            this._resultGrid._isEdit = false;
            this._resultGrid._table_name = _g.d.erp_doc_format._table;
            this._resultGrid._addColumn(_g.d.erp_doc_format._code, 1, 20, 10);
            this._resultGrid._addColumn(_g.d.erp_doc_format._screen_code, 1, 20, 10);
            this._resultGrid._addColumn(_g.d.erp_doc_format._name_1, 1, 20, 20);
            this._resultGrid._addColumn(_g.d.erp_doc_format._scan_folder, 1, 60, 40);
            this._resultGrid._addColumn(_g.d.erp_doc_format._scan_doc_count, 2, 60, 10);
            this._resultGrid._addColumn(_g.d.erp_doc_format._scan_doc_success, 2, 60, 10);
            this._resultGrid._calcPersentWidthToScatter();
            //
            _scandir();
        }

        void _scandir()
        {
            MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
            ArrayList __scandisk = new ArrayList();
            __scandisk = _getinfo._scrandive();
            string[] _dataDive = Environment.GetLogicalDrives();
            for (int loop = 0; loop < _dataDive.Length; loop++)
            {
                __scandisk.Add(_getinfo.GetVolumeSerial((_dataDive[loop].Replace(":\\", ""))).Trim());
            }
            if (__scandisk.Count > 0)
            {
                StringBuilder __query = new StringBuilder();
                StringBuilder __queryIn = new StringBuilder();
                __query.Append("select * from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._scan_computer_id + " in (");
                for (int __loop = 0; __loop < __scandisk.Count; __loop++)
                {
                    if (__scandisk[__loop].ToString().Length > 0)
                    {
                        if (__queryIn.Length > 0)
                        {
                            __queryIn.Append(",");
                        }
                        __queryIn.Append("\'" + __scandisk[__loop].ToString() + "\'");
                    }
                }
                __query.Append(__queryIn.ToString() + ")");
                DataTable __data = this._myFrameWork._queryShort(__query.ToString()).Tables[0];
                this._resultGrid._loadFromDataTable(__data);
                for (int __row = 0; __row < this._resultGrid._rowData.Count; __row++)
                {
                    DirectoryInfo __dir = new DirectoryInfo(this._resultGrid._cellGet(__row, _g.d.erp_doc_format._scan_folder).ToString());
                    var files = Array.FindAll(__dir.GetFiles(), x => !x.Name.Equals("Thumbs.db"));
                    this._resultGrid._cellUpdate(__row, _g.d.erp_doc_format._scan_doc_count, files.Length, false);
                }
            }
            else
            {
                MessageBox.Show("Doc not found");
            }
        }

        MemoryStream _getFileStream(string fileName)
        {
            FileStream oImg;
            BinaryReader oBinaryReader;
            byte[] oImgByteArray;
            MemoryStream ms;

            oImg = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            oBinaryReader = new BinaryReader(oImg);
            oImgByteArray = oBinaryReader.ReadBytes((int)oImg.Length);
            ms = new MemoryStream(oImgByteArray);
            oBinaryReader.Close();
            oImg.Close();
            return ms;
        }

        private void _processStripButton_Click(object sender, EventArgs e)
        {
            //_process();
            _scandir();
            _processThread = new Thread(new ThreadStart(_process));
            _processThread.IsBackground = true;
            _processThread.Start();
        }

        void _process()
        {
            for (int __doc = 0; __doc < this._resultGrid._rowData.Count; __doc++)
            {
                string __folder = this._resultGrid._cellGet(__doc, _g.d.erp_doc_format._scan_folder).ToString();
                DirectoryInfo __dir = new DirectoryInfo(__folder);
                FileInfo[] __file = Array.FindAll(__dir.GetFiles(), x => !x.Name.Equals("Thumbs.db"));
                for (int __loop = 0; __loop < __file.Length; __loop++)
                {
                    if (__file[__loop].Name == "Thumbs.db")
                    {
                        continue;
                    }
                    string __fileName = __file[__loop].FullName.ToString();

                    string __screenCode = this._resultGrid._cellGet(__doc, _g.d.erp_doc_format._screen_code).ToString();
                    string __docNo = "0-" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 23).ToUpper();
                    string __docDate = "2000-1-1";

                    // อัดรูปเข้า Database โดยใช้ ws
                    MemoryStream __stream = _getFileStream(__fileName);
                    //Image img = Image.FromStream(__stream);
                    this._pictureBox.Image = Image.FromStream(new MemoryStream(__stream.ToArray()));
                    // this._pictureBox.Refresh(); invoke

                    string _insquery = string.Format("insert into " + _g.d.sml_doc_images._table + "(" + _g.d.sml_doc_images._image_id + "," + _g.d.sml_doc_images._guid_code + "," + _g.d.sml_doc_images._image_file + ") VALUES('{0}','{1}',?)", __docNo, System.Guid.NewGuid().ToString());
                    string __resultImageSave = _myFrameWork._queryByteData(MyLib._myGlobal._databaseName, _insquery, new object[] { __stream.ToArray() });


                    _g.g._transControlTypeEnum __transFlag = _g.g._transFlagGlobal._transFlagByScreenCode(__screenCode);
                    StringBuilder __trans = new StringBuilder();
                    __trans.Append("insert into " + _g.d.ic_trans._table + " (");
                    __trans.Append(_g.d.ic_trans._doc_no + ",");
                    __trans.Append(_g.d.ic_trans._doc_date + ",");
                    __trans.Append(_g.d.ic_trans._doc_time + ",");
                    __trans.Append(_g.d.ic_trans._cust_code + ",");
                    __trans.Append(_g.d.ic_trans._doc_format_code + ",");
                    __trans.Append(_g.d.ic_trans._trans_type + ",");
                    __trans.Append(_g.d.ic_trans._trans_flag + ")");
                    __trans.Append(" values (");
                    __trans.Append("\'" + __docNo + "\',");
                    __trans.Append("\'" + __docDate + "\',");
                    __trans.Append("\'08:00\',");
                    __trans.Append("\'SCAN\',");
                    __trans.Append("\'" + this._resultGrid._cellGet(__doc, _g.d.erp_doc_format._code).ToString() + "\',");
                    __trans.Append("\'" + _g.g._transTypeGlobal._transType(__transFlag).ToString() + "\',");
                    __trans.Append("\'" + _g.g._transFlagGlobal._transFlag(__transFlag).ToString() + "\')");
                    string __result = _myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __trans.ToString());
                    if (__result.Length == 0)
                    {
                        // remove file name
                        try
                        {
                            File.Delete(__file[__loop].FullName);
                        }
                        catch
                        {
                        }
                        this._resultGrid._cellUpdate(__doc, _g.d.erp_doc_format._scan_doc_success, __loop + 1, true);
                    }
                    else
                    {
                        MessageBox.Show(__result);
                        break;
                    }
                }
            }

            _scandir();
            _pictureBox.Image = null;
            //_pictureBox.Refresh();
        }

        private void _stopProcessStripButton_Click(object sender, EventArgs e)
        {
            if (_processThread != null && _processThread.IsAlive)
            {
                _processThread.Abort();
            }
        }

        private void _refreshStripButton_Click(object sender, EventArgs e)
        {

            if (_processThread == null || _processThread.IsAlive == false)
            {
                _scandir();
            }
        }
    }
}
