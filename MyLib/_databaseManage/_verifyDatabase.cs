using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace MyLib._databaseManage
{
    public partial class _verifyDatabase : MyLib._myForm
    {
        private int _totalDatabase = 0;
        private int _databaseCount = 0;
        private int _errorCount = 0;
        private string _progressText = "";
        private string _progressDatabaseText = "";
        private string _databaseName = "";
        private string _fixedDatabaseName = "";
        public int _progressBarValue = 0;
        private StringBuilder _resultText = new StringBuilder();
        private Thread _working;
        private Boolean _workingSuccess = false;
        private ArrayList _databaseList = new ArrayList();
        public bool _success = false;

        public _verifyDatabase()
        {
            InitializeComponent();
            this._start("");
        }

        public _verifyDatabase(string databaseName)
        {
            InitializeComponent();
            this._start(databaseName);
        }

        /// <summary>
        /// สำหรับ verify Database ของ SML Bill Free
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="mainDatabaseNotCheck"></param>
        public _verifyDatabase(string databaseName, bool mainDatabaseNotCheck)
        {
            InitializeComponent();
            this._startVerifyDatabase(databaseName);
        }

        void _startVerifyDatabase(string databaseName)
        {
            this._fixedDatabaseName = databaseName;
            _splitMain.Location = new Point(_splitMain.Location.X, 5);
            _listViewDatabase.View = View.List;
            _listViewDatabase.CheckBoxes = true;
            // ดึงฐานข้อมูลทั้งหมด
            _myFrameWork myFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._masterDatabaseType);
            string __where = "";
            if (databaseName.Trim().Length > 0)
            {
                __where = " where " + MyLib._myGlobal._addUpper("database_name") + "=\'" + databaseName.ToUpper() + "\' and upper(user_code) = \'" + MyLib._myGlobal._userCode + "\' ";
            }
            string __query = "select database_name as " + MyLib._d.sml_database_list._data_group + ", database_name as " + MyLib._d.sml_database_list._data_code + ", database_name as " + MyLib._d.sml_database_list._data_name + ", database_name as " + MyLib._d.sml_database_list._data_database_name + " from sml_bill_user " + __where + " order by database_name";
            DataSet result = myFrameWork._query(MyLib._myGlobal._masterRegisterDatabaseName, __query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    ListViewItem item = new ListViewItem();
                    // item.Name = getRows[row].ItemArray[2].ToString();
                    item.Name = getRows[row].ItemArray[3].ToString();//somruk
                    //item.Text = getRows[row].ItemArray[3].ToString() + " (Group:[" + getRows[row].ItemArray[0].ToString() + "],Code:[" + getRows[row].ItemArray[1].ToString() + "],Name:[" + getRows[row].ItemArray[2].ToString() + "]";
                    item.Text = getRows[row].ItemArray[3].ToString() + " (Group:[" + getRows[row].ItemArray[0].ToString() + "],Code:[" + getRows[row].ItemArray[1].ToString() + "],Name:[" + getRows[row].ItemArray[3].ToString() + "]";//somruk
                    item.ImageIndex = 0;
                    if (item.Name.ToUpper().Equals(databaseName.ToUpper()))
                    {
                        item.Checked = true;
                    }
                    _listViewDatabase.Items.Add(item);
                } // for
            }
            _listViewDatabase._setExStyles();
            _listViewDatabase.Invalidate();
            this.Disposed += new EventHandler(_verifyDatabase_Disposed);
            if (databaseName.Trim().Length > 0)
            {
                this._totalDatabase = 1;
                this._startVerify();
            }
        }

        void _start(string databaseName)
        {
            this._fixedDatabaseName = databaseName;
            _splitMain.Location = new Point(_splitMain.Location.X, 5);
            _listViewDatabase.View = View.List;
            _listViewDatabase.CheckBoxes = true;
            // ดึงฐานข้อมูลทั้งหมด
            _myFrameWork myFrameWork = new _myFrameWork();
            string __where = "";
            if (databaseName.Trim().Length > 0)
            {
                __where = " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_database_name) + "=\'" + databaseName.ToUpper() + "\'";
            }
            string __query = "select " + MyLib._d.sml_database_list._data_group + "," + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + "," + MyLib._d.sml_database_list._data_database_name + " from " + MyLib._d.sml_database_list._table + __where + " order by " + MyLib._d.sml_database_list._data_group;
            DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, __query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    ListViewItem item = new ListViewItem();
                    // item.Name = getRows[row].ItemArray[2].ToString();
                    item.Name = getRows[row].ItemArray[3].ToString();//somruk
                    //item.Text = getRows[row].ItemArray[3].ToString() + " (Group:[" + getRows[row].ItemArray[0].ToString() + "],Code:[" + getRows[row].ItemArray[1].ToString() + "],Name:[" + getRows[row].ItemArray[2].ToString() + "]";
                    item.Text = getRows[row].ItemArray[3].ToString() + " (Group:[" + getRows[row].ItemArray[0].ToString() + "],Code:[" + getRows[row].ItemArray[1].ToString() + "],Name:[" + getRows[row].ItemArray[3].ToString() + "]";//somruk
                    item.ImageIndex = 0;
                    if (item.Name.ToUpper().Equals(databaseName.ToUpper()))
                    {
                        item.Checked = true;
                    }
                    _listViewDatabase.Items.Add(item);
                } // for
            }
            _listViewDatabase._setExStyles();
            _listViewDatabase.Invalidate();
            this.Disposed += new EventHandler(_verifyDatabase_Disposed);
            if (databaseName.Trim().Length > 0)
            {
                this._totalDatabase = 1;
                this._startVerify();
            }
        }

        void _verifyDatabase_Disposed(object sender, EventArgs e)
        {
            try
            {
                this._working.Abort();
            }
            catch
            {
            }
        }


        private void _verifyDatabase_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void _viewByIcon_CheckedChanged(object sender, EventArgs e)
        {
            _listViewDatabase.View = View.LargeIcon;
        }

        private void _viewByList_CheckedChanged(object sender, EventArgs e)
        {
            _listViewDatabase.View = View.List;
        }

        private void _buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (int loop = 0; loop < _listViewDatabase.Items.Count; loop++)
            {
                _listViewDatabase.Items[loop].Checked = true;
            } // for
        }

        private void _startThread()
        {
            this._resultText = new StringBuilder();
            _myFrameWork __myFrameWork = new _myFrameWork();
            // start
            ArrayList __getTableList = __myFrameWork._getAllTable(0, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseStructFileName);
            this._databaseCount = 0;
            this._progressBarValue = 0;
            this._errorCount = 0;
            for (int __databaseLoop = 0; __databaseLoop < this._databaseList.Count; __databaseLoop++)
            {
                this._databaseName = this._databaseList[__databaseLoop].ToString();
                this._databaseCount++;
                this._progressDatabaseText = "Verify Database (" + this._databaseCount + "/" + this._totalDatabase + ") : [" + this._databaseName + "]";
                this._progressText = "..";
                this._resultText.Append("Database [" + this._databaseName + "] start\n");
                //
                int __maxTable = __getTableList.Count;
                __myFrameWork._verifyDatabaseScript("before", MyLib._myGlobal._databaseConfig, "SML", this._databaseName, MyLib._myGlobal._databaseVerifyXmlFileName);
                for (int __loop = 0; __loop < __maxTable; __loop++)
                {
                    string __tableName = __getTableList[__loop].ToString();
                    //if (tableName.ToLower().Equals("ar_customer"))
                    {
                        this._progressText = "Verify Table (" + (__loop + 1) + "/" + (__maxTable) + " : [" + this._databaseName + "." + __tableName + "]";
                        this._resultText.Append("\tTable [" + __tableName + "]\n");
                        this._progressBarValue = (((__loop + 1) * 100) / (__maxTable));
                        //
                        string verifyResult = __myFrameWork._verifyDatabase(MyLib._myGlobal._databaseConfig, "SML", this._databaseName, __tableName, MyLib._myGlobal._databaseStructFileName);
                        if (verifyResult.Length > 0)
                        {
                            this._resultText.Append("Error : [" + this._progressTextTable.Text + "]\n" + verifyResult + "\n");
                            this._errorCount++;
                        }
                    }
                    // add roworder to primary_key (กันไว้ กรณีลืมสร้าง)
                    // ย้ายไป web service__myFrameWork._queryInsertOrUpdate(this._databaseName, "ALTER TABLE " + tableName + " ADD PRIMARY KEY (roworder)");
                }// for
                __myFrameWork._verifyDatabaseScript("after", MyLib._myGlobal._databaseConfig, "SML", this._databaseName, MyLib._myGlobal._databaseVerifyXmlFileName);
            }
            this._workingSuccess = true;
        }

        void _startVerify()
        {
            this._toolStrip1.Enabled = false;
            _progressBarDatabase.Value = 0;
            _progressBarTable.Value = 0;
            _resultTextBox.Text = "";
            //
            this._databaseList = new ArrayList();
            for (int databaseLoop = 0; databaseLoop < _listViewDatabase.Items.Count; databaseLoop++)
            {
                if (_listViewDatabase.Items[databaseLoop].Checked)
                {
                    string __databaseName = _listViewDatabase.Items[databaseLoop].Name;
                    this._databaseList.Add(__databaseName);
                }
            }
            this._workingSuccess = false;
            this._working = new Thread(this._startThread);
            this._working.Start();
            this._timerForProgress.Start();
        }

        private void _buttonStart_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            __myFrameWork._sendXmlFile(MyLib._myGlobal._databaseStructFileName);
            __myFrameWork._sendXmlFile(MyLib._myGlobal._databaseVerifyXmlFileName);
            __myFrameWork._sendXmlFile(MyLib._myGlobal._dataViewTemplateXmlFileName);

            this._totalDatabase = 0;
            for (int loop = 0; loop < _listViewDatabase.Items.Count; loop++)
            {
                if (_listViewDatabase.Items[loop].Checked)
                {
                    this._totalDatabase++;
                }
            } // for

            if (this._totalDatabase == 0)
            {
                // ยังไม่ได้เลือกข้อมูลที่ต้องการ Verify กรุณาเลือกก่อน
                MessageBox.Show(MyLib._myGlobal._resource("warning30"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                // --  
                string message = string.Format(MyLib._myGlobal._resource("warning31"), this._totalDatabase);
                // ท่านได้เลือกจำนวน : {0} ฐานข้อมูล\n\nต้องการทำการ Verify หรือไม่
                DialogResult result = MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.OK)
                {
                    this._startVerify();
                }
            }
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _timerForProgress_Tick(object sender, EventArgs e)
        {
            this._progressTextDatabase.Text = "Verify Database (" + this._databaseCount + "/" + this._totalDatabase + ") : [" + this._databaseName + "]";
            this._progressTextDatabase.Invalidate();
            this._progressBarDatabase.Value = ((this._databaseCount * 100) / (this._totalDatabase));
            this._progressBarDatabase.Invalidate();
            //
            this._progressTextTable.Text = this._progressText;
            this._progressTextTable.Invalidate();
            this._progressBarTable.Value = this._progressBarValue;
            this._progressBarTable.Invalidate();
            //
            this._resultTextBox.Text = this._resultText.ToString();
            this._resultTextBox.Invalidate();
            if (this._workingSuccess)
            {
                this._timerForProgress.Stop();
                // --  
                string __message = string.Format(MyLib._myGlobal._resource("warning32"), this._totalDatabase, this._errorCount);
                // ทำการ Verify database จำนวน : {0} ฐานข้อมูล\nมีรายการผิดพลาดจำนวน : {1} รายการ
                MessageBox.Show(__message, MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this._toolStrip1.Enabled = true;

                // update last verify
                {
                    string __xmlStructFileName = ("sml_struct_" + MyLib._myGlobal._providerCode + ".xml").ToUpper();
                    MyLib._myFrameWork __myFrameWork = new _myFrameWork();

                    string __dateTimeStr = MyLib._myGlobal._xmlUpdate.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                    __myFrameWork._saveXmlFile(__xmlStructFileName, __dateTimeStr);

                    string __fileName = MyLib._myGlobal._databaseStructFileName;
                    DateTime __lastDateTimeUpdate = __myFrameWork._getFileLastUpdate(__fileName);

                    //string __dateTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                    string __query = "update " + MyLib._d.sml_database_list._table + " set " + MyLib._d.sml_database_list._last_database_xml_update + "=\'" + __lastDateTimeUpdate + "\' where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_database_name) + "=\'" + this._databaseName + "\'";
                    string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
                    if (__result.Length > 0)
                    {
                        MessageBox.Show(__result);
                    }
                }

                if (this._fixedDatabaseName.Trim().Length > 0)
                {
                    this._success = true;
                    this.Dispose();
                }
            }
        }

        private void _sendXMLButton_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            __myFrameWork._sendXmlFile(MyLib._myGlobal._databaseStructFileName);
            __myFrameWork._sendXmlFile(MyLib._myGlobal._databaseVerifyXmlFileName);
            __myFrameWork._sendXmlFile(MyLib._myGlobal._dataViewTemplateXmlFileName);
        }
    }
}