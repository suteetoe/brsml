using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _dataRestoreForm : Form
    {
        int _progressBarRecordValue = 0;
        string _progressText = "";
        private Thread _working;
        Boolean _workingSuccess = false;

        public _dataRestoreForm()
        {
            InitializeComponent();
            this._buttonStop.Enabled = false;
        }

        private void _buttonSelect_Click(object sender, EventArgs e)
        {
            //FileStream __f2 = new FileStream(this._folderTextBox.TextBox.Text + "/" + this._fileNameTextBox.textBox.Text, FileMode.Create);
            OpenFileDialog __openFile = new OpenFileDialog(); // { DefaultExt = "gz", Filter = "gz Files (*.gz)", FilterIndex = 0 };
            __openFile.DefaultExt = "gz";
            __openFile.Filter = "SML Backup File |*.gz;";
            __openFile.FilterIndex = 0;
            if (__openFile.ShowDialog() == DialogResult.OK)
            {
                this._fileNameTextBox.textBox.Text = __openFile.FileName;
            }
        }

        private void _buttonStart_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            StringBuilder __queryCheck = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __queryCheck.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(*) as xcount from ic_inventory"));
            __queryCheck.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(*) as xcount from ic_trans"));
            __queryCheck.Append("</node>");

            ArrayList __checkResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryCheck.ToString());
            DataTable __icTable = ((DataSet)__checkResult[0]).Tables[0];
            DataTable __transTable = ((DataSet)__checkResult[1]).Tables[0];

            // check empty database
            if (MyLib._myGlobal._intPhase(__icTable.Rows[0][0].ToString()) == 0 && MyLib._myGlobal._intPhase(__transTable.Rows[0][0].ToString()) == 0)
            {

                DialogResult __result = MessageBox.Show("ยืนยันการนำข้อมูลที่สำรองไว้ กลับมาใช้งาน", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

                if (__result == System.Windows.Forms.DialogResult.Yes)
                {
                    //();
                    MyLib._myGlobal._allowProcessReportServer = false;

                    this._buttonStart.Enabled = false;
                    this._buttonStop.Enabled = true;
                    this._buttonStart.Invalidate();
                    this._workingSuccess = false;
                    this._working = new Thread(this._startThread);
                    this._working.Start();
                    this._timerForProcess.Start();
                }
            }
            else
            {
                MessageBox.Show("ฐานข้อมูลปัจจุบันมีข้อมูลอยู่แล้ว");
            }
        }



        public void _startThread()
        {
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();

            // ไม่ผ่าน ram เต็ม
            /*
            this._progressBarRecordValue = 0;

            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            StringBuilder uncompressed = new StringBuilder();

            using (FileStream __f2 = new FileStream(this._fileNameTextBox.TextBox.Text, FileMode.Open))
            {
                using (GZipStream __gz = new GZipStream(__f2, CompressionMode.Decompress))
                {
                    //byte[] buffer = new byte[4096];
                    //int readBytes;
                    //while ((readBytes = __gz.Read(buffer, 0, buffer.Length)) != 0)
                    //{
                    //    uncompressed.Append(Encoding.UTF8.GetString(buffer));
                    //}
                    using (StreamReader __reader = new StreamReader(__gz))
                    {
                        uncompressed.Append(__reader.ReadToEnd());
                    }
                }
            }

            string[] __line = uncompressed.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            // pack query insert 
            if (__line.Length > 0)
            {
                // fix error
                __myFrameWork._queryShort("truncate erp_view_table");
                __myFrameWork._queryShort("truncate erp_view_column");
                __myFrameWork._queryShort("truncate fromdesign");
                __myFrameWork._queryShort("truncate sml_fastreport");
                __myFrameWork._queryShort("truncate sml_posdesign");
            }

            StringBuilder __queryList = new StringBuilder();
            int __count = 0;

            for (int __row = 0; __row < __line.Length; __row++)
            {
                if (__queryList.Length == 0)
                {
                    __queryList.Append(MyLib._myGlobal._xmlHeader + "<node>");
                }

                string __getLine = __line[__row];

                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__getLine));

                this._progressBarRecordValue = ((__row + 1) * 100) / __line.Length;
                this._progressText = __row.ToString() + "/" + __line.Length.ToString();

                if (++__count == 6000 || __queryList.Length > 100000)
                {
                    __queryList.Append("</node>");
                    string __resultInsert = "";
                    try
                    {
                        //__resultInsert = __myFrameWork._queryList(MyLib._myGlobal._databaseName, MyLib._myGlobal._deleteAscError(__queryList.ToString()));
                        __resultInsert = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message.ToString() + __queryList.ToString());
                    }
                    if (__resultInsert.Length > 0)
                    {
                        //this._resultText.Append(__resultInsert + "\r\n");
                        MessageBox.Show(__resultInsert);
                        this._workingSuccess = true;
                        break;
                    }
                    __queryList = new StringBuilder();
                    __count = 0;
                }
            }

            if (__queryList.Length > 0 && this._workingSuccess == false)
            {
                __queryList.Append("</node>");

                //string __degubQuery = __queryList.ToString();
                //string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, MyLib._myGlobal._deleteAscError(__queryList.ToString()));
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                if (__result.Length == 0)
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show(__result);
                }
            }

            this._workingSuccess = true;
            */

            string __temppath = System.IO.Path.GetTempPath();
            string __fileName = Guid.NewGuid().ToString();
            string newFileName = __temppath + __fileName;

            using (FileStream __f2 = new FileStream(this._fileNameTextBox.TextBox.Text, FileMode.Open))
            {
                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream __gz = new GZipStream(__f2, CompressionMode.Decompress))
                    {
                        byte[] buffer = new byte[1024];
                        int nRead;
                        while ((nRead = __gz.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            decompressedFileStream.Write(buffer, 0, nRead);
                        }
                    }
                }
            }

            //var lines = System.IO.File.ReadAllLines(newFileName);
            //var __rowCount = lines.Length;

            var __rowCount = 0;
            using (TextReader reader = new StreamReader(newFileName, Encoding.UTF8))
            {
                while (reader.ReadLine() != null)
                {
                    __rowCount++;
                }
            }

            try
            {
                using (TextReader __tempFile = new StreamReader(newFileName, Encoding.UTF8))
                {
                    // fix error
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate erp_view_table");
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate erp_view_column");
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate formdesign");
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate sml_fastreport");
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate sml_posdesign");

                    StringBuilder __lineStr = new StringBuilder();
                    StringBuilder __queryList = new StringBuilder();
                    int __count = 0;
                    int __row = 0;

                    string __readLine;
                    while ((__readLine = __tempFile.ReadLine()) != null)
                    {

                        if (__readLine.EndsWith(");"))
                        {
                            __lineStr.Append(__readLine);

                            // process here
                            if (__queryList.Length == 0)
                            {
                                __queryList.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            }

                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__lineStr.ToString()));

                            if (++__count == 6000 || __queryList.Length > 100000)
                            {
                                __queryList.Append("</node>");
                                string __resultInsert = "";
                                try
                                {
                                    //__resultInsert = __myFrameWork._queryList(MyLib._myGlobal._databaseName, MyLib._myGlobal._deleteAscError(__queryList.ToString()));
                                    __resultInsert = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                                }
                                catch (Exception __ex)
                                {
                                    MessageBox.Show(__ex.Message.ToString() + __queryList.ToString());
                                }
                                if (__resultInsert.Length > 0)
                                {
                                    //this._resultText.Append(__resultInsert + "\r\n");
                                    MessageBox.Show(__resultInsert);
                                    this._workingSuccess = true;
                                    break;
                                }
                                __queryList = new StringBuilder();
                                __count = 0;
                            }

                            __lineStr = new StringBuilder();
                        }
                        else
                        {
                            __lineStr.Append(__readLine);
                        }

                        this._progressBarRecordValue = ((++__row) * 100) / __rowCount;
                        this._progressText = __row.ToString() + "/" + __rowCount;


                    }

                    if (__queryList.Length > 0 && this._workingSuccess == false)
                    {
                        __queryList.Append("</node>");

                        //string __degubQuery = __queryList.ToString();
                        //string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, MyLib._myGlobal._deleteAscError(__queryList.ToString()));
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                        if (__result.Length == 0)
                        {
                            MessageBox.Show("Success");
                        }
                        else
                        {
                            MessageBox.Show(__result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            // remove temp
            try
            {
                System.IO.File.Delete(newFileName);
            }
            catch
            {
            }
            this._workingSuccess = true;

        }

        private void _timerForProcess_Tick(object sender, EventArgs e)
        {
            this._progressBar.Value = this._progressBarRecordValue;
            this._progressBar.Invalidate();

            this._progressTextTable.Text = this._progressText;
            this._progressTextTable.Invalidate();

            if (this._workingSuccess == true)
            {
                this._progressBar.Value = 100;
                this._timerForProcess.Stop();
                this._buttonStop.Enabled = false;
                this._buttonStart.Enabled = true;

                MyLib._myGlobal._allowProcessReportServer = true;


            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}
