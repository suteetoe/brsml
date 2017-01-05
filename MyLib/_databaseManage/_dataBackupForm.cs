using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace MyLib._databaseManage
{
    public partial class _dataBackupForm : Form
    {
        private delegate void onErrorBackupDataInvoke(string errorMessage);

        onErrorBackupDataInvoke onError;


        private Boolean _workingSuccess = false;
        private Thread _working;
        private string _progressTextStr = "";
        private string _progressRecordTextStr = "";
        private int _progressBarTableValue = 0;
        private int _progressBarRecordValue = 0;
        private XmlDocument _xDoc = new XmlDocument();

        public _dataBackupForm()
        {
            InitializeComponent();
            this._fileNameTextBox.textBox.Text = MyLib._myGlobal._databaseName + "-" + String.Format("{0:yy-MM-dd-HH-mm}" + ".gz", DateTime.Now);
            this._folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.FormClosed += new FormClosedEventHandler(_dataBackupForm_FormClosed);
            this.onError = new onErrorBackupDataInvoke(_onErrorBackup);
        }

        void _onErrorBackup(string message)
        {
            MessageBox.Show(message);
        }

        void _dataBackupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this._working.Abort();
            }
            catch
            {
            }
        }

        private void _buttonSelect_Click(object sender, EventArgs e)
        {
            DialogResult __result = _folderBrowserDialog.ShowDialog();
            if (__result == DialogResult.OK)
            {
                string[] __files = Directory.GetFiles(_folderBrowserDialog.SelectedPath);
                this._folderTextBox.TextBox.Text = _folderBrowserDialog.SelectedPath;
            }
        }

        private void _startThread()
        {
            try
            {

                this._progressBarTableValue = 0;
                this._progressBarRecordValue = 0;
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                //
                DataTable __dt = new DataTable();
                __dt.Columns.Add("tablename", typeof(string));
                __dt.Columns.Add("fieldname", typeof(string));
                __dt.Columns.Add("name thai", typeof(string));
                __dt.Columns.Add("name english", typeof(string));
                __dt.Columns.Add("type", typeof(string));
                __dt.Columns.Add("length", typeof(string));
                __dt.Columns.Add("indentity", typeof(string));
                __dt.Columns.Add("allow_null", typeof(string));
                __dt.DefaultView.Sort = "tablename";
                //
                DataSet __myDataSet = __myFrameWork._dataStruct(MyLib._myGlobal._databaseStructFileName);
                _xDoc.LoadXml(__myDataSet.GetXml());
                _xDoc.DocumentElement.Normalize();
                XmlElement __xRoot = _xDoc.DocumentElement;
                XmlNodeList __xReader = __xRoot.GetElementsByTagName("table");
                for (int __table = 0; __table < __xReader.Count; __table++)
                {
                    XmlNode __xFirstNode = __xReader.Item(__table);
                    if (__xFirstNode.NodeType == XmlNodeType.Element)
                    {
                        XmlElement __xTable = (XmlElement)__xFirstNode;
                        // get field
                        string __tableName = __xTable.GetAttribute("name");
                        XmlNodeList __xField = __xTable.GetElementsByTagName("field");
                        for (int __field = 0; __field < __xField.Count; __field++)
                        {
                            XmlNode __xReadNode = __xField.Item(__field);
                            if (__xReadNode != null)
                            {
                                if (__xReadNode.NodeType == XmlNodeType.Element)
                                {
                                    XmlElement __xGetField = (XmlElement)__xReadNode;
                                    string __resourceOnly = __xGetField.GetAttribute("resource_only");
                                    if (__resourceOnly.Equals("false"))
                                    {
                                        __dt.Rows.Add(__tableName, __xGetField.GetAttribute("name"), __xGetField.GetAttribute("thai"), __xGetField.GetAttribute("eng"), __xGetField.GetAttribute("type"), __xGetField.GetAttribute("length"), __xGetField.GetAttribute("indentity"), __xGetField.GetAttribute("allow_null"));
                                    }
                                }
                            }
                        } // for
                    }
                }
                //
                ArrayList __getTableList = __myFrameWork._getAllTable(0, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseStructFileName);
                FileStream __f2 = new FileStream(this._folderTextBox.TextBox.Text + "/" + this._fileNameTextBox.textBox.Text, FileMode.Create);
                GZipStream __gz = new GZipStream(__f2, CompressionMode.Compress, false);
                for (int __loop = 0; __loop < __getTableList.Count; __loop++)
                {
                    string __tableName = __getTableList[__loop].ToString();
                    this._progressTextStr = "Backup Table " + (__loop + 1) + "/" + (__getTableList.Count) + " : [" + MyLib._myGlobal._databaseName + "." + __tableName + "]";
                    this._progressBarTableValue = (((__loop + 1) * 100) / (__getTableList.Count));
                    this._progressRecordTextStr = "";
                    this._progressBarRecordValue = 0;

                    bool __foundBytea = false;

                    if (__tableName.Equals("formdesign") || 2 == 2)
                    {
                        DataTable __countDataTable = __myFrameWork._queryShort("select count(*) as xcount from " + __tableName).Tables[0];
                        int __totalRecord = (int)MyLib._myGlobal._decimalPhase(__countDataTable.Rows[0][0].ToString());
                        int __offset = 0;
                        int __count = 0;
                        int __limit = 1000;
                        DataRow[] __fieldList = __dt.Select("tablename=\'" + __tableName + "\'");
                        StringBuilder __querySelectField = new StringBuilder();
                        for (int __field = 0; __field < __fieldList.Length; __field++)
                        {
                            if (__field != 0)
                            {
                                __querySelectField.Append(",");
                            }
                            string __fieldName = __fieldList[__field]["fieldname"].ToString();
                            string __fieldType = __fieldList[__field]["type"].ToString();
                            if (__fieldType.Equals("image"))
                            {
                                __querySelectField.Append("encode(" + __fieldName + ",'base64') as " + __fieldName);
                                __foundBytea = true;
                            }
                            else
                            {
                                __querySelectField.Append(__fieldName);
                            }
                        }

                        if (__foundBytea == true)
                        {
                            __limit = 5;
                        }

                        while (__count < __totalRecord)
                        {
                            DataTable __data = __myFrameWork._queryShort("select " + __querySelectField.ToString() + " from " + __tableName + "  order by roworder offset " + __offset.ToString() + " limit " + __limit.ToString()).Tables[0];
                            __offset += __limit;
                            __count += __limit;
                            StringBuilder __query = new StringBuilder();
                            for (int __record = 0; __record < __data.Rows.Count; __record++)
                            {
                                StringBuilder __queryField = new StringBuilder();
                                StringBuilder __queryData = new StringBuilder();
                                for (int __field = 0; __field < __fieldList.Length; __field++)
                                {
                                    if (__field != 0)
                                    {
                                        __queryField.Append(",");
                                        __queryData.Append(",");
                                    }
                                    string __fieldName = __fieldList[__field]["fieldname"].ToString();
                                    __queryField.Append(__fieldName);
                                    string __fieldType = __fieldList[__field]["type"].ToString();
                                    if (__fieldType.Equals("image"))
                                    {
                                        __queryData.Append("decode(\'" + __data.Rows[__record][__fieldName].ToString() + "\','base64')");
                                    }
                                    else if (__fieldType.Equals("smalldatetime") || __fieldType.Equals("datetime"))
                                    {
                                        string __getDateValue = __data.Rows[__record][__fieldName].ToString();
                                        __queryData.Append((__getDateValue.Length > 0) ? "\'" + __getDateValue + "\'" : "null");
                                    }
                                    else
                                    {
                                        if (__fieldType.Equals("int") || __fieldType.Equals("smallint") || __fieldType.Equals("tinyint") || __fieldType.Equals("float"))
                                        {
                                            string __getNumberValue = __data.Rows[__record][__fieldName].ToString();
                                            __queryData.Append((__getNumberValue.Length > 0) ? __getNumberValue : "0");
                                        }
                                        else
                                        {
                                            __queryData.Append("\'" + __data.Rows[__record][__fieldName].ToString().Replace("'", "''") + "\'");
                                        }
                                    }
                                }
                                __query.Append("insert into " + __tableName + " (" + __queryField.ToString() + ") values (" + __queryData.ToString() + ");\r\n");
                            }
                            byte[] __array = Encoding.UTF8.GetBytes(__query.ToString());
                            __gz.Write(__array, 0, __array.Length);
                            int __countProgress = __count;
                            if (__countProgress > __totalRecord)
                            {
                                __countProgress = __totalRecord;
                            }
                            this._progressRecordTextStr = __countProgress.ToString() + "/" + __totalRecord.ToString();
                            this._progressBarRecordValue = (((__countProgress) * 100) / (__totalRecord));
                        }
                    }
                }
                __gz.Close();
                __f2.Close();
                this._workingSuccess = true;
            }
            catch (Exception ex)
            {
                this.Invoke(onError, new object[] { ex.ToString() });
            }
        }

        private void _buttonStart_Click(object sender, EventArgs e)
        {
            this._buttonStart.Enabled = false;
            this._buttonStop.Enabled = true;
            this._buttonStart.Invalidate();
            this._workingSuccess = false;
            this._working = new Thread(this._startThread);
            this._working.Start();
            this._timerForProgress.Start();
        }

        private void _timerForProgress_Tick(object sender, EventArgs e)
        {
            this._progressTextTable.Text = this._progressTextStr;
            this._progressTextTable.Invalidate();
            this._progressBar.Value = this._progressBarTableValue;
            this._progressBar.Invalidate();
            //
            this._progressRecordText.Text = this._progressRecordTextStr;
            this._progressRecordText.Invalidate();
            this._progressBarRecord.Value = this._progressBarRecordValue;
            this._progressBarRecord.Invalidate();
            //
            if (this._workingSuccess)
            {
                this._timerForProgress.Stop();
                MessageBox.Show("Success", MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this._buttonStop.Enabled = false;
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update erp_company_profile set last_backup=now()");
            }
        }

        private void _buttonStop_Click(object sender, EventArgs e)
        {
            this._buttonStart.Enabled = true;
            this._buttonStop.Enabled = false;
            this._timerForProgress.Stop();
            try
            {
                this._working.Abort();
            }
            catch
            {
            }
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _optionButton_Click(object sender, EventArgs e)
        {
            _databaseDumpQueryForm _dumpForm = new _databaseDumpQueryForm();

            _dumpForm.ShowDialog();
                
        }
    }
}
