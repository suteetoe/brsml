using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;

namespace SMLERPIC
{
    public partial class _icImportPicture : UserControl
    {
        ConformBoxInvoke __conformBox;

        public _icImportPicture()
        {
            InitializeComponent();
            __conformBox = new ConformBoxInvoke(_startConfirmBox);
        }

        private void _buttonBrowseFolders_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog __open = new FolderBrowserDialog();

            if (__open.ShowDialog() == DialogResult.OK)
            {
                // list file to list view
                _foldersLocationTextbox.Text = __open.SelectedPath;

                string[] filters = { "*.jpg", "*.png", "*.gif", "*.bmp" };
                ArrayList images = new ArrayList();

                foreach (string filter in filters)
                {
                    images.AddRange(Directory.GetFiles(_foldersLocationTextbox.Text, filter));
                }

                for (int __i = 0; __i < images.Count; __i++)
                {
                    _viewFileDirectory.Items.Add(new ListViewItem((string)images[__i]));
                }
            }

        }

        int _processIndex = 0;
        int _processMax = 0;
        string _processText = "";
        List<_processValue> __processStack = null;


        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            _processIndex = 0;
            __lastResultConfirm = MyLib.MemoryBoxResult.Cancel;
            _processMax = _viewFileDirectory.Items.Count;
            __processStack = new List<_processValue>();

            for (int __i = 0; __i < _viewFileDirectory.Items.Count; __i++)
            {
                ListViewItem __item = _viewFileDirectory.Items[__i];

                //if (__i % 3 == 0)
                //{
                //    __item.StateImageIndex = 1;
                //}
                //else
                //{
                //    __item.StateImageIndex = 0;
                //}
                __processStack.Add(new _processValue() { _filePath = __item.Text });
            }

            this._progressbar.Visible = true;
            this._progressbar.Maximum = this._processMax;
            this.Enabled = false;
            Thread __new = new Thread(_process);
            __new.Start();
            this.timer1.Start();

        }

        private MyLib.MemoryBoxResult _startConfirmBox(string code)
        {
            if (__lastResultConfirm == MyLib.MemoryBoxResult.YesToAll || __lastResultConfirm == MyLib.MemoryBoxResult.NoToAll)
                return __lastResultConfirm;

            MyLib.MemoryBoxResult __result = MyLib.MemoryBoxResult.Cancel;
            __result = MyLib._myMessageBox.ShowForm("ยืนยัน เขียนทับ", "ยืนยัน");
            return __result;

        }

        MyLib.MemoryBoxResult __lastResultConfirm = MyLib.MemoryBoxResult.Cancel;
        void _process()
        {
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();

            for (_processIndex = 0; _processIndex < __processStack.Count; _processIndex++)
            {
                string __fileName = __processStack[_processIndex]._filePath;
                __processStack[_processIndex]._success = _successStatus.InProcess;

                this._processText = "Process : " + __fileName;
                // check รหัสสินค้าก่อน มีอยู่หรือเปล่า
                string __icCode = Path.GetFileNameWithoutExtension(__fileName);

                string __checkIC = "select count(" + _g.d.ic_inventory._code + ") as count_ic from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + __icCode.ToUpper() + "\'";

                DataSet __dsCheckIc = __fw._query(MyLib._myGlobal._databaseName, __checkIC);
                if (MyLib._myGlobal._decimalPhase(__dsCheckIc.Tables[0].Rows[0]["count_ic"].ToString()) > 0)
                {
                    __processStack[_processIndex]._success = _successStatus.InProcess;

                    string _query = "select image_id from images where image_id = '" + __icCode + "' ";
                    DataSet __ds = __fw._query(MyLib._myGlobal._databaseName, _query);

                    Boolean _overwrite = true;
                    if (__ds.Tables[0].Rows.Count > 0)
                    {
                        // save to database
                        //if (MyLib._myMessageBox.Show("ยืนยัน เขียนทับ", "ยืนยัน") == MyLib.MemoryBoxResult. {
                        __lastResultConfirm = (MyLib.MemoryBoxResult)this.Invoke(__conformBox, new object[] { __icCode });

                        //if (__lastResultConfirm != MyLib.MemoryBoxResult.NoToAll && __lastResultConfirm != MyLib.MemoryBoxResult.YesToAll)
                        //{
                        //    //MyLib._myMessageBox __messagebox = new MyLib._myMessageBox();
                        //    //__messagebox.ShowDialog(MyLib._myGlobal._mainForm);

                        //    //Form __form = new Form();
                        //    //__form.ShowDialog(MyLib._myGlobal._mainForm);
                        //    //__lastResultConfirm = __messagebox.ShowMemoryDialog(MyLib._myGlobal._mainForm,"ยืนยัน เขียนทับ", "ยืนยัน");

                        //    //__lastResultConfirm = MyLib._myMessageBox.ShowForm("ยืนยัน เขียนทับ", "ยืนยัน");
                        //}

                        if (__lastResultConfirm == MyLib.MemoryBoxResult.Yes)
                        {
                            _overwrite = true;
                        }

                        if (__lastResultConfirm == MyLib.MemoryBoxResult.Cancel)
                        {
                            this._processText = "Cancel";
                            return;
                        }

                        if (__lastResultConfirm == MyLib.MemoryBoxResult.No || __lastResultConfirm == MyLib.MemoryBoxResult.NoToAll)
                        {
                            _overwrite = false;
                            __processStack[_processIndex]._success = _successStatus.Skip;
                        }
                    }

                    if (_overwrite)
                    {
                        // overwrite
                        __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.images._table + " where " + _g.d.images._image_id + "='" + __icCode + "'");

                        // read file 
                        MemoryStream __memoryStream = __getMemoryStream(__fileName);
                        string _insquery = string.Format("insert into " + _g.d.images._table + "(" + _g.d.images._image_id + "," + _g.d.images._guid_code + "," + _g.d.images._image_file + ") VALUES('{0}','{1}',?)", __icCode, System.Guid.NewGuid().ToString());

                        string __result = __fw._queryByteData(MyLib._myGlobal._databaseName, _insquery, new object[] { __memoryStream.ToArray() });
                        if (__result == "")
                        {
                            __processStack[_processIndex]._success = _successStatus.Complete;
                        }
                        else
                        {
                            __processStack[_processIndex]._success = _successStatus.Error;
                        }
                    }
                    if (__processStack[_processIndex]._success == _successStatus.InProcess)
                    {
                        __processStack[_processIndex]._success = _successStatus.Default;
                    }
                }
                else
                {
                    __processStack[_processIndex]._success = _successStatus.NotFound;
                }
                // save to database
            }
            this._processText = "Success";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // update listview value update process in index
            for (int __i = 0; __i < _processIndex; __i++)
            {
                switch (__processStack[__i]._success)
                {
                    case _successStatus.Complete:
                        _viewFileDirectory.Items[__i].StateImageIndex = 0;
                        break;
                    case _successStatus.Error:
                        _viewFileDirectory.Items[__i].StateImageIndex = 1;
                        break;
                    case _successStatus.InProcess:
                        _viewFileDirectory.Items[__i].StateImageIndex = 2;
                        break;
                    case _successStatus.Skip:
                        _viewFileDirectory.Items[__i].StateImageIndex = 3;
                        break;
                    case _successStatus.NotFound:
                        _viewFileDirectory.Items[__i].StateImageIndex = 4;
                        break;
                    default:
                        _viewFileDirectory.Items[__i].StateImageIndex = -1;
                        break;
                }
            }

            this._progressbar.Value = this._processIndex;
            this._progressbar.Invalidate();
            this._labelResult.Text = this._processText;
            this._labelResult.Invalidate();
            if (this._processText.Equals("Success") || this._processText.Equals("Cancel"))
            {
                this.timer1.Stop();
                if (this._processText.Equals("Success"))
                    MessageBox.Show(this._processText);
                this._processText = "";
                this._progressbar.Visible = false;
                this.Enabled = true;
            }

        }

        MemoryStream __getMemoryStream(string namefile)
        {
            FileStream oImg;
            BinaryReader oBinaryReader;
            byte[] oImgByteArray;
            MemoryStream ms;

            oImg = new FileStream(namefile, FileMode.Open, FileAccess.Read);
            oBinaryReader = new BinaryReader(oImg);
            oImgByteArray = oBinaryReader.ReadBytes((int)oImg.Length);
            ms = new MemoryStream(oImgByteArray);
            oBinaryReader.Close();
            oImg.Close();
            return ms;
        }

        public class _processValue
        {
            public string _fileName = "";
            public string _filePath = "";
            public _successStatus _success = _successStatus.Default;
        }

        public enum _successStatus
        {
            Complete,
            Skip,
            Error,
            NotFound,
            InProcess,
            Default
        }

        private void _close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private delegate MyLib.MemoryBoxResult ConformBoxInvoke(string code);
    }
}
