using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace SMLTransfer
{
    public partial class _mainForm1 : Form
    {
        public class _docType
        {
            public _docType(string name, string transFlag)
            {
                this._name = name;
                this._transFlag = transFlag;
            }

            public string _name = "";
            public string _transFlag = "";
        }

        string __sourceGUID = "";
        string __targetGUID = "";

        public _mainForm1()
        {
            InitializeComponent();

            string __computerName = SystemInformation.ComputerName.ToLower();
            //

            if (__computerName.IndexOf("toe-pc") != -1)
            {
                if (MessageBox.Show("Debug", "DEBUG", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    this._sourceWebServiceTextbox.Text = "sml1.thaiddns.com:8080";
                    this._sourceProviderTextbox.Text = "SINGHA";
                    this._sourceGroupTextbox.Text = "SML";
                    this._sourceUserCodeTextbox.Text = "superadmin";
                    this._sourcePasswordTextbox.Text = "superadmin";
                    this._sourceDatabaseNameTextbox.Text = "SINGHA";

                    this._desWebServiceTextbox.Text = "localhost:8080";
                    this._desProviderTextbox.Text = "DEBUG";
                    this._desGroupTextbox.Text = "SML";
                    this._desUserCodeTextbox.Text = "superadmin";
                    this._desPasswordTextbox.Text = "superadmin";
                    this._desDatabaseNameTextbox.Text = "IMPORT";
                }
            }

            List<_docType> __docType = new List<_docType>();
            __docType.Add(new _docType("ซื้อสินค้า", "12"));
            __docType.Add(new _docType("เพิ่มหนี้/ราคาผิด", "14"));
            __docType.Add(new _docType("ส่งคืนสินค้า/ราคาผิด", "16"));
            __docType.Add(new _docType("จ่ายเงินมัดจำ", "11"));
            __docType.Add(new _docType("รับคืนเงินมัดจำ", "25"));

            __docType.Add(new _docType("รับสินค้า", "310"));
            __docType.Add(new _docType("ส่งคืนสินค้า/ราคาผิด (ทะยอยรับ)", "311"));
            __docType.Add(new _docType("ตั้งหนี้จากการรับสินค้า", "315"));
            __docType.Add(new _docType("เพิ่มหนี้/ราคาผิด (ทะยอยรับ)", "316"));
            __docType.Add(new _docType("ลดหนี้จากใบตั้งหนี้", "317"));

            __docType.Add(new _docType("ขายสินค้าและบริการ", "44"));
            __docType.Add(new _docType("เพิ่มสินค้า/เพิ่มหนี้", "46"));
            __docType.Add(new _docType("รับคืนสินค้า/ลดหนี้", "48"));
            __docType.Add(new _docType("รับเงินมัดจำ", "110"));
            __docType.Add(new _docType("คืนเงินมัดจำ", "112"));

            __docType.Add(new _docType("รายได้อื่น ๆ", "250"));
            __docType.Add(new _docType("ลดหนี้รายได้อื่น ๆ", "252"));
            __docType.Add(new _docType("เพิ่มหนี้รายได้อื่น ๆ", "254"));

            __docType.Add(new _docType("ค่าใช้จ่ายอื่น ๆ", "260"));
            __docType.Add(new _docType("ลดหนี้ค่าใช้ัจ่ายอื่น ๆ", "262"));
            __docType.Add(new _docType("เพิ่มหนี้ค่าใช้จ่ายอื่น ๆ", "264"));


            this._gridDocType._getResource = false;
            this._gridDocType._isEdit = false;
            this._gridDocType._addColumn("Check", 11, 10, 10);
            this._gridDocType._addColumn("Detail", 1, 10, 90);
            this._gridDocType._addColumn("Flag", 1, 10, 90, false, true);
            this._gridDocType._calcPersentWidthToScatter();


            for (int __row = 0; __row < __docType.Count; __row++)
            {
                int __addRow = this._gridDocType._addRow();

                _docType __doc = __docType[__row];

                this._gridDocType._cellUpdate(__addRow, 1, __doc._name, true);
                this._gridDocType._cellUpdate(__addRow, 2, __doc._transFlag, true);

            }

            _checkConnectedDatabase();

        }

        private void _removeAllButton_Click(object sender, EventArgs e)
        {
            //foreach (Control __getControl in _transGroupbox.Controls)
            //{
            //    if (__getControl.GetType() == typeof(CheckBox))
            //    {
            //        CheckBox __getCheckbox = (CheckBox)__getControl;
            //        __getCheckbox.Checked = false;
            //    }
            //}

            for (int __row = 0; __row < this._gridDocType._rowData.Count; __row++)
            {
                this._gridDocType._cellUpdate(__row, 0, 0, true);
            }
            this._gridDocType.Invalidate();
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            //foreach (Control __getControl in _transGroupbox.Controls)
            //{
            //    if (__getControl.GetType() == typeof(CheckBox))
            //    {
            //        CheckBox __getCheckbox = (CheckBox)__getControl;
            //        __getCheckbox.Checked = true;
            //    }
            //}
            for (int __row = 0; __row < this._gridDocType._rowData.Count; __row++)
            {
                this._gridDocType._cellUpdate(__row, 0, 1, true);
            }
            this._gridDocType.Invalidate();
        }

        private void _sourceConnectButton_Click(object sender, EventArgs e)
        {
            // test Connect
            if (__sourceGUID.Length > 0)
            {
                // logout
                _disConnect(this._sourceWebServiceTextbox.Text, this._sourceProviderTextbox.Text, this._sourceGroupTextbox.Text, this._sourceUserCodeTextbox.Text, this._sourcePasswordTextbox.Text, this._sourceDatabaseNameTextbox.Text, __sourceGUID);
                this._sourceConnectdStatus.Text = "";
                this.__sourceGUID = "";
                this._sourceConnectButton.Text = "connect";

                // change status
                this._sourceWebServiceTextbox.Enabled =
                    this._sourceProviderTextbox.Enabled =
                    this._sourceGroupTextbox.Enabled =
                    this._sourceUserCodeTextbox.Enabled =
                    this._sourcePasswordTextbox.Enabled =
                    this._sourceDatabaseNameTextbox.Enabled = true;
            }
            else
            {
                string __guid = _connect(this._sourceWebServiceTextbox.Text, this._sourceProviderTextbox.Text, this._sourceGroupTextbox.Text, this._sourceUserCodeTextbox.Text, this._sourcePasswordTextbox.Text, this._sourceDatabaseNameTextbox.Text);
                if (__guid.Length > 0)
                {
                    this._sourceConnectdStatus.Text = "Connected";
                    this.__sourceGUID = __guid;

                    // change status
                    this._sourceWebServiceTextbox.Enabled =
                        this._sourceProviderTextbox.Enabled =
                        this._sourceGroupTextbox.Enabled =
                        this._sourceUserCodeTextbox.Enabled =
                        this._sourcePasswordTextbox.Enabled =
                        this._sourceDatabaseNameTextbox.Enabled = false;
                    this._sourceConnectButton.Text = "Disconnect";
                }
                else
                {
                    MessageBox.Show("Login Failed");
                    this._sourceConnectdStatus.Text = __errorConnectStr;
                }
            }
            _checkConnectedDatabase();
        }

        void _checkConnectedDatabase()
        {
            if (__sourceGUID.Length > 0 && __targetGUID.Length > 0)
            {

            }
            else
            {

            }
        }

        private void _desConnectButton_Click(object sender, EventArgs e)
        {

            string __host = "";
            string __provider = "";
            string __group = "";
            string __userCode = "";
            string __password = "";

            if (_connectSameSourceCheckbox.Checked == true)
            {
                __host = this._sourceWebServiceTextbox.Text;
                __provider = this._sourceProviderTextbox.Text;
                __group = this._sourceGroupTextbox.Text;
                __userCode = this._sourceUserCodeTextbox.Text;
                __password = this._sourcePasswordTextbox.Text;
            }
            else
            {
                __host = this._desWebServiceTextbox.Text;
                __provider = this._desProviderTextbox.Text;
                __group = this._desGroupTextbox.Text;
                __userCode = this._desUserCodeTextbox.Text;
                __password = this._desPasswordTextbox.Text;
            }

            if (__targetGUID.Length > 0)
            {
                // logout
                _disConnect(__host, __provider, __group, __userCode, __password, this._desDatabaseNameTextbox.Text, __targetGUID);

                this._desWebServiceTextbox.Enabled =
                        this._desProviderTextbox.Enabled =
                        this._desGroupTextbox.Enabled =
                        this._desUserCodeTextbox.Enabled =
                        this._desPasswordTextbox.Enabled =
                        this._desDatabaseNameTextbox.Enabled = true;

                this._desConnectButton.Text = "Connect";
                this._desConnectedStatus.Text = "";
                this.__targetGUID = "";


            }
            else
            {
                string __guid = _connect(__host, __provider, __group, __userCode, __password, this._desDatabaseNameTextbox.Text);
                if (__guid.Length > 0)
                {
                    this._desConnectedStatus.Text = "Connected";
                    this.__targetGUID = __guid;

                    // ปรับสถานะ
                    this._desWebServiceTextbox.Enabled =
                        this._desProviderTextbox.Enabled =
                        this._desGroupTextbox.Enabled =
                        this._desUserCodeTextbox.Enabled =
                        this._desPasswordTextbox.Enabled =
                        this._desDatabaseNameTextbox.Enabled = false;

                    this._desConnectButton.Text = "Disconnect";
                }
                else
                {
                    MessageBox.Show("Login Failed");
                    this._desConnectedStatus.Text = __errorConnectStr;
                }

            }
            // __myFrameWork._logoutProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._guid);

            _checkConnectedDatabase();
        }

        string __errorConnectStr = "";
        string _connect(string server, string provider, string group, string userCode, string password, string databaseName)
        {
            string __result = "";

            try
            {
                MyLib._myGlobal._webServiceServerList.Clear();

                MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                data._webServiceName = server;
                data._webServiceConnected = false;
                MyLib._myGlobal._webServiceServerList.Add(data);

                MyLib._myGlobal._providerCode = provider;
                MyLib._myGlobal._databaseName = databaseName;
                MyLib._myGlobal._nonPermission = true;
                MyLib._myGlobal._userCode = userCode;
                MyLib._myGlobal._password = password; //  "superadmin";

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                __result = __myFrameWork._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, MyLib._myGlobal._userCode, MyLib._myGlobal._password, MyLib._myGlobal._computerName, MyLib._myGlobal._databaseName);

                if (__result.Length > 0)
                {
                    DataSet __company = __myFrameWork._query(databaseName, "select " + _g.d.erp_company_profile._company_name_1 + " from " + _g.d.erp_company_profile._table);
                    if (__company.Tables.Count == 0)
                    {
                        __errorConnectStr = "Not Found Database";
                        __result = "";
                    }
                }
            }
            catch (Exception ex)
            {
                __errorConnectStr = ex.Message.ToString();
                __result = "";
            }

            return __result;
        }

        void _disConnect(string server, string provider, string group, string userCode, string password, string databaseName, string guid)
        {
            // __myFrameWork._logoutProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._guid);
            try
            {
                MyLib._myGlobal._webServiceServerList.Clear();

                MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                data._webServiceName = server;
                data._webServiceConnected = false;
                MyLib._myGlobal._webServiceServerList.Add(data);

                MyLib._myGlobal._providerCode = provider;
                MyLib._myGlobal._databaseName = databaseName;
                MyLib._myGlobal._nonPermission = true;
                MyLib._myGlobal._userCode = userCode;
                MyLib._myGlobal._password = password; //  "superadmin";

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();


                __myFrameWork._logoutProcess(MyLib._myGlobal._databaseConfig, guid);

            }
            catch (Exception ex)
            {
                __errorConnectStr = ex.Message.ToString();
            }
        }

        private void _connectSameSourceCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this._desWebServiceTextbox.Enabled =
                this._desProviderTextbox.Enabled =
                this._desGroupTextbox.Enabled =
                this._desUserCodeTextbox.Enabled =
                this._desPasswordTextbox.Enabled = (_connectSameSourceCheckbox.Checked) ? false : true;
        }

        private void _startButton_Click(object sender, EventArgs e)
        {
            this._process();
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {

        }

        void _process()
        {
            string __fromDate = MyLib._myGlobal._convertDateToQuery(this._fromDateDatebox._dateTime);
            string __toDate = MyLib._myGlobal._convertDateToQuery(this._toDateDatebox._dateTime);

            string __fromDocNo = this._fromDocNoTextbox.Text;
            string __toDocNo = this._toDocNoTextbox.Text;

            string __whereDocDate = " {0} between '" + __fromDate + "' and '" + __toDate + "' ";
            string __whereDocNo = "";
            if (__fromDocNo.Length > 0 && __toDocNo.Length > 0)
            {
                __whereDocNo = " {0} between '" + __fromDocNo + "' and '" + __toDocNo + "' ";
            }

            //foreach (Control __getControl in _transGroupbox.Controls)
            for (int __row = 0; __row < this._gridDocType._rowData.Count; __row++)
            {
                if (this._gridDocType._cellGet(__row, 0).ToString().Equals("1"))
                {
                    //CheckBox __getCheckbox = (CheckBox)__getControl;
                    //if (__getCheckbox.Checked == true && __getCheckbox.Tag != null)

                    string __getTransFlag = this._gridDocType._cellGet(__row, 2).ToString();
                    {
                        bool __pass = true;
                        //_processCopyTrans(__getCheckbox.Tag.ToString(), __fromDate, __toDate, __fromDocNo, __toDocNo);

                        // ic_trans
                        if (__pass == true)
                        {
                            __pass = _processCopyTrans(_g.d.ic_trans._table, " " + _g.d.ic_trans._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.ic_trans._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.ic_trans._doc_no) : ""));
                        }

                        // ic_trans_detail
                        if (__pass == true)
                        {
                            __pass = _processCopyTrans(_g.d.ic_trans_detail._table, " " + _g.d.ic_trans_detail._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.ic_trans_detail._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.ic_trans_detail._doc_no) : ""));
                        }
                        // cb_trans
                        if (__pass == true)
                        {
                            __pass = _processCopyTrans(_g.d.cb_trans._table, " " + _g.d.cb_trans._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.cb_trans._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.cb_trans._doc_no) : ""));
                        }
                        // cb_trans_detail
                        if (__pass == true)
                        {
                            __pass = _processCopyTrans(_g.d.cb_trans_detail._table, " " + _g.d.cb_trans_detail._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.cb_trans_detail._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.cb_trans_detail._doc_no) : ""));
                        }
                        // ap_ar_trans
                        if (__pass == true)
                        {
                            __pass = _processCopyTrans(_g.d.ap_ar_trans._table, " " + _g.d.ap_ar_trans._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.ap_ar_trans._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.ap_ar_trans._doc_no) : ""));
                        }
                        // ap_ar_trans_detail
                        if (__pass == true)
                        {
                            __pass = _processCopyTrans(_g.d.ap_ar_trans_detail._table, " " + _g.d.ap_ar_trans_detail._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.ap_ar_trans_detail._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.ap_ar_trans_detail._doc_no) : ""));
                        }
                        // gl_gournal_vat_sale
                        if (__pass == true)
                        {
                            __pass = _processCopyTrans(_g.d.gl_journal_vat_buy._table, " " + _g.d.gl_journal_vat_buy._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.gl_journal_vat_buy._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.gl_journal_vat_buy._doc_no) : ""));
                        }
                        // gl_gournal_vat_buy
                        if (__pass == true)
                        {
                            __pass = _processCopyTrans(_g.d.gl_journal_vat_sale._table, " " + _g.d.gl_journal_vat_sale._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.gl_journal_vat_sale._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.gl_journal_vat_sale._doc_no) : ""));
                        }
                        // gl_wht_list
                        if (__pass == true)
                        {
                            __pass = _processCopyTrans(_g.d.gl_wht_list._table, " " + _g.d.gl_wht_list._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.gl_wht_list._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.gl_wht_list._doc_no) : ""));
                        }
                        // gl_wht_list_detail
                        if (__pass == true)
                        {
                            _processCopyTrans(_g.d.gl_wht_list_detail._table, " " + _g.d.gl_wht_list._trans_flag + " = " + __getTransFlag + " and " + string.Format(__whereDocDate, _g.d.gl_wht_list._doc_date) + ((__whereDocNo.Length > 0) ? " and " + string.Format(__whereDocNo, _g.d.gl_wht_list._doc_no) : ""));
                        }
                    }
                }
            }

            MessageBox.Show("Success");
        }

        public struct _syncFieldListStruct
        {
            public string _fieldNameForSelect;
            public string _fieldNameForInsertUpdate;
            public string _fieldType;
            public string _fieldDefaultValue;
        }

        List<_syncFieldListStruct> _getFieldFromXml(string getXml)
        {

            List<_syncFieldListStruct> __fieldList = new List<_syncFieldListStruct>();
            XmlDocument __xDoc = new XmlDocument();
            __xDoc.LoadXml(getXml);
            __xDoc.DocumentElement.Normalize();
            XmlElement __xRoot = __xDoc.DocumentElement;
            XmlNodeList __xReader = __xRoot.GetElementsByTagName("detail");
            for (int __detail = 0; __detail < __xReader.Count; __detail++)
            {
                XmlNode __xFirstNode = __xReader.Item(__detail);
                if (__xFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement __xTable = (XmlElement)__xFirstNode;
                    string __type = __xTable.GetAttribute("type").ToLower();
                    if (__type.Equals("varchar") ||
                        __type.Equals("date") ||
                        __type.Equals("int2") ||
                        __type.Equals("int4") ||
                        __type.Equals("int8") ||
                        __type.Equals("char") ||
                        __type.Equals("uuid") ||
                        __type.Equals("bool") ||
                        __type.Equals("numeric") ||
                        __type.Equals("decimal") ||
                        __type.Equals("bytea") ||
                        __type.Equals("image") ||
                        __type.Equals("float8") ||
                        __type.Equals("timestamp") ||

                        // toe for sql server
                        __type.Equals("int") ||
                        __type.Equals("float") ||
                        __type.Equals("money") ||
                        __type.Equals("smalldatetime") ||
                        __type.Equals("datetime") ||
                        __type.Equals("smallint") ||
                        __type.Equals("uniqueidentifier")
                        )
                    {
                        string __fieldName = __xTable.GetAttribute("column_name").ToLower();

                        _syncFieldListStruct __new = new _syncFieldListStruct();
                        __new._fieldNameForInsertUpdate = __fieldName;
                        if (__type.Equals("bytea") || __type.Equals("image"))
                        {
                            __new._fieldNameForSelect = "encode(" + __fieldName + ",\'base64\') as " + __fieldName;
                        }
                        else
                        {
                            __new._fieldNameForSelect = __fieldName;
                        }
                        __new._fieldType = __type;
                        __fieldList.Add(__new);
                    }
                    else
                    {
                    }

                }
            }
            return __fieldList;

        }

        bool _processCopyTrans(string tableName, string extraWhere)
        {
            MyLib._myFrameWork __datacenterFrameWork = new MyLib._myFrameWork(this._sourceWebServiceTextbox.Text, "SMLConfig" + this._sourceProviderTextbox.Text.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
            __datacenterFrameWork._databaseSelectType = __datacenterFrameWork._setDataBaseCode();
            List<_syncFieldListStruct> __fieldListServer = _getFieldFromXml(__datacenterFrameWork._getSchemaTable(this._sourceDatabaseNameTextbox.Text, tableName));

            string __host = "";
            string __provider = "";
            string __group = "";
            string __userCode = "";
            string __password = "";

            if (_connectSameSourceCheckbox.Checked == true)
            {
                __host = this._sourceWebServiceTextbox.Text;
                __provider = this._sourceProviderTextbox.Text;
                __group = this._sourceGroupTextbox.Text;
                __userCode = this._sourceUserCodeTextbox.Text;
                __password = this._sourcePasswordTextbox.Text;
            }
            else
            {
                __host = this._desWebServiceTextbox.Text;
                __provider = this._desProviderTextbox.Text;
                __group = this._desGroupTextbox.Text;
                __userCode = this._desUserCodeTextbox.Text;
                __password = this._desPasswordTextbox.Text;
            }

            MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork(__host, "SMLConfig" + __provider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
            __clientFrameWork._databaseSelectType = __clientFrameWork._setDataBaseCode();
            List<_syncFieldListStruct> __fieldListClient = _getFieldFromXml(__clientFrameWork._getSchemaTable(this._desDatabaseNameTextbox.Text, tableName));


            // ลบ Field ที่ไม่ตรงกัน ระหว่าง server,client
            // หาจาก field server ถ้าไม่มีใน field client ลบ field server ออก
            int __loop1 = 0;
            while (__loop1 < __fieldListServer.Count)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __fieldListClient.Count && __found == false; __loop2++)
                {
                    if (__fieldListServer[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListClient[__loop2]._fieldNameForInsertUpdate))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __fieldListServer.RemoveAt(__loop1);
                }
                else
                {
                    __loop1++;
                }
            }
            // หาใน field client ถ้าไม่มีใน field server ให้ลบ field client ออก
            __loop1 = 0;
            while (__loop1 < __fieldListClient.Count)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __fieldListServer.Count && __found == false; __loop2++)
                {
                    if (__fieldListClient[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListServer[__loop2]._fieldNameForInsertUpdate))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __fieldListClient.RemoveAt(__loop1);
                }
                else
                {
                    __loop1++;
                }
            }

            // ประกอบ field
            StringBuilder __fieldForInsert = new StringBuilder();
            StringBuilder __fieldForSelect = new StringBuilder();

            StringBuilder __fieldForServerSelect = new StringBuilder(); // ใส่ default value มาแล้ว

            // toe เพิ่ม default value ต้อง แยก select ระหว่าง client และ center
            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
            {
                if (__fieldForSelect.Length > 0)
                {
                    __fieldForSelect.Append(",");
                    __fieldForInsert.Append(",");
                    __fieldForServerSelect.Append(",");
                }
                __fieldForSelect.Append(__fieldListClient[__loop]._fieldNameForSelect.ToString());
                __fieldForInsert.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate.ToString());
                __fieldForServerSelect.Append(__fieldListClient[__loop]._fieldNameForSelect.ToString());

            }

            // 
            DataTable __getDataFromCenter = null;
            try
            {
                __getDataFromCenter = __datacenterFrameWork._query(this._sourceDatabaseNameTextbox.Text, "select " + __fieldForServerSelect.ToString() + " from " + tableName + " where " + extraWhere + " ").Tables[0];
            }
            catch
            {
            }

            if (__getDataFromCenter != null && __fieldForInsert.Length > 0)
            {

                // เปรียบเทียบข้อมูล ถ้าพบข้อมูลเก่าก็ update หรือ ถ้าไม่พบก็ insert
                StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __rowData = 0; __rowData < __getDataFromCenter.Rows.Count; __rowData++)
                {
                    StringBuilder __query = new StringBuilder();

                    {
                        // insert
                        __query.Append("insert into " + tableName + " (" + __fieldForInsert + ") values (");
                        for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                        {
                            if (__loop != 0)
                            {
                                __query.Append(",");
                            }
                            switch (__fieldListClient[__loop]._fieldType.ToLower())
                            {
                                case "bytea":
                                case "image":
                                    {
                                        string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                        __query.Append("decode(\'" + __value + "\',\'base64\')");
                                    }
                                    break;
                                case "uniqueidentifier":
                                case "uuid":
                                case "varchar":
                                    {
                                        //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                        string __value = (__fieldListClient[__loop]._fieldDefaultValue != null && __fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                        __query.Append("\'" + __value + "\'");
                                    }
                                    break;
                                case "date":
                                case "datetime":
                                case "smalldatetime":
                                case "timestamp":
                                    {
                                        //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                        string __value = (__fieldListClient[__loop]._fieldDefaultValue != null && __fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                        __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                    }
                                    break;
                                default: // ตัวเลข
                                    {
                                        //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                        string __value = (__fieldListClient[__loop]._fieldDefaultValue != null && __fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                        __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                    }
                                    break;
                            }
                        }
                        __query.Append(")");
                    }
                    __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                }
                __queryList.Append("</node>");

                string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                if (__result.Length == 0)
                {
                    // update สถานะ send_success
                }
                else
                {
                    this._messageTextbox.Text = __result;
                    return false;
                }
            }

            /*XmlDocument __xDoc = new XmlDocument();
            __xDoc.LoadXml(__sourceTableSchema);
            __xDoc.DocumentElement.Normalize();
            XmlElement __xRoot = __xDoc.DocumentElement;
            XmlNodeList __xReader = __xRoot.GetElementsByTagName("detail");
            for (int __detail = 0; __detail < __xReader.Count; __detail++)
            {
                XmlNode __xFirstNode = __xReader.Item(__detail);
                if (__xFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement __xTable = (XmlElement)__xFirstNode;
                    if (__tableName.ToLower().Equals(__xTable.GetAttribute("table_name").ToLower()))
                    {
                        string __type = __xTable.GetAttribute("type").ToLower();
                        if (__type.Equals("varchar") ||
                            __type.Equals("date") ||
                            __type.Equals("datetime") ||
                            __type.Equals("timestamp") ||
                            __type.Equals("smalldatetime") ||
                            __type.Equals("int") ||
                            __type.Equals("int2") ||
                            __type.Equals("int4") ||
                            __type.Equals("int8") ||
                            __type.Equals("char") ||
                            __type.Equals("uniqueidentifier") ||
                            __type.Equals("uuid") ||
                            __type.Equals("bool") ||
                            __type.Equals("numeric") ||
                            __type.Equals("decimal") ||
                            __type.Equals("bytea") ||
                            __type.Equals("image") ||
                            __type.Equals("float8"))
                        {
                            string __fieldName = __xTable.GetAttribute("column_name").ToLower();
                            if (__fieldName.Equals("send_success") == false && __fieldName.Equals("is_sync_in") == false)
                            {
                                _syncFieldListStruct __new = new _syncFieldListStruct();
                                __new._fieldNameForInsertUpdate = __fieldName;
                                if (__type.Equals("bytea") || __type.Equals("image"))
                                {
                                    __new._fieldNameForSelect = "encode(" + __fieldName + ",\'base64\') as " + __fieldName;
                                    // Field รูปหรือเปล่า ถ้ามี ก็ให้ลดขนาดการรับส่ง
                                    __limitReceiveRecord = 10;
                                    __limitSendRecord = 10;
                                }
                                else
                                {
                                    __new._fieldNameForSelect = __fieldName;
                                }
                                __new._fieldType = __type;
                                __fieldList.Add(__new);
                            }
                        }
                    }
                }
            }*/

            /*

            // start query

            int __queryIndex = 0;
            int __tableTransQueryIndex = 0;

            string __docDateWhere = " {0} between \'" + fromDate + "\' and \'" + toDate + "\' ";
            string __docNoWhere = (fromDocNo.Length > 0 && toDocNo.Length > 0) ? " {0} between \'" + fromDocNo + "\' and \'" + toDocNo + "\' " : "";

            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            string __queryTrans = "select " + MyLib._myGlobal._fieldAndComma(
                _g.d.ic_trans._doc_no, 
                _g.d.ic_trans._doc_date
                ) + " from " + _g.d.ic_trans._table + " where " + string.Format(__docDateWhere, _g.d.ic_trans._doc_no) + ((__docNoWhere.Length > 0) ? " and " + string.Format(__docNoWhere, _g.d.ic_trans._doc_no) : "");

            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTrans));
            __tableTransQueryIndex = __queryIndex;
            __queryIndex++;

            __queryList.Append("<node>");

            
            */



            /*

            {
                switch (this._transferSelect)
                {
                    case 0: // SML -> SML
                        __targetTableName = __sourceTableName;
                        break;
                    case 1: // BC Account -> SML
                        __targetTableName = this._scriptFindTargetTableName(__sourceTableName);
                        break;
                }
                //
                if (this._scriptTargetFieldNameDuplicate(__sourceTableName, __targetTableName) == false)
                {
                    string __queryGetTargetColumnName = "";
                    string __queryGetColumnName = "";
                    string __queryGetIndex = "";
                    switch (this._providerSourceSelect)
                    {
                        case 0: // PostgresSQL
                        case 2: // Microsoft SQL
                            __queryGetTargetColumnName = "SELECT * FROM information_schema.columns where upper(table_name)=\'" + __targetTableName.ToUpper() + "\'";
                            __queryGetColumnName = "SELECT * FROM information_schema.columns where upper(table_name)=\'" + __sourceTableName.ToUpper() + "\'";
                            __queryGetIndex = "SELECT * FROM information_schema.constraint_column_usage where upper(table_name)=\'" + __sourceTableName.ToUpper() + "\'";
                            break;
                    }
                    // ดึง field name เพื่อเปรียบเทียบ
                    DataTable __targetField = __myFrameWork._query(this._targetDatabaseName, __queryGetTargetColumnName).Tables[0];
                    //
                    DataTable __tableList = null;
                    DataTable __keyList = null;
                    //
                    switch (this._providerSourceSelect)
                    {
                        case 0: // PostgresSQL
                            __tableList = this._queryToDataTable(__postgresConn, __queryGetColumnName);
                            __keyList = this._queryToDataTable(__postgresConn, __queryGetIndex);
                            break;
                        case 2: // Microsoft SQL
                            __tableList = this._queryToDataTable(__microsoftSqlConn, __queryGetColumnName);
                            __keyList = this._queryToDataTable(__microsoftSqlConn, __queryGetIndex);
                            break;
                    }
                    //
                    for (int __fieldRow = 0; __fieldRow < __tableList.Rows.Count; __fieldRow++)
                    {
                        if (__query.Length > 0)
                        {
                            __query.Append(",\n");
                        }
                        DataRow __dataRow = __tableList.Rows[__fieldRow];
                        string __columnName = __dataRow["column_name"].ToString();
                        // ค้นหาว่า field ตรงกันหรือเปล่า
                        Boolean __fieldTargetFound = false;
                        for (int __findField = 0; __findField < __targetField.Rows.Count; __findField++)
                        {
                            string __fieldName = "";
                            switch (this._transferSelect)
                            {
                                case 0: // SML -> SML
                                    __fieldName = __targetField.Rows[__findField]["column_name"].ToString().ToUpper().Trim();
                                    break;
                                case 1: // BC Account -> SML
                                    __fieldName = this._scriptFindSourceField(__sourceTableName, __columnName);
                                    if (__fieldName.Length > 0)
                                    {
                                        try
                                        {
                                            __fieldName = __targetField.Rows[__findField][__fieldName].ToString().ToUpper().Trim();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    break;
                            }
                            if (__columnName.Trim().ToUpper().Equals(__fieldName))
                            {
                                __fieldTargetFound = true;
                                break;
                            }
                        }
                        if (__fieldTargetFound)
                        {
                            if (this._ignoreFieldTextBox.Text.ToLower().IndexOf("[" + __columnName.ToLower() + "]") == -1)
                            {
                                string __columnDefault = __dataRow["column_default"].ToString();
                                Boolean __isNull = __dataRow["is_nullable"].ToString().ToUpper().Equals("YES");
                                string __dataType = __dataRow["data_type"].ToString();
                                Boolean __stringCase = false;
                                Boolean __byteACase = false;
                                int __charMaxLength = _convertToInt(__dataRow["character_maximum_length"].ToString());
                                int __numericPrecision = _convertToInt(__dataRow["numeric_precision"].ToString());
                                int __numericPrecisionRadix = _convertToInt(__dataRow["numeric_precision_radix"].ToString());
                                //
                                __query.Append(__columnName + " ");
                                if (__columnDefault.IndexOf("nextval") != -1)
                                {
                                    __query.Append(" serial ");
                                }
                                else
                                {
                                    if (__dataType.Equals("integer"))
                                    {
                                        __query.Append(__dataType);
                                    }
                                    else
                                    {
                                        if (__dataType.Equals("bytea") || __dataType.Equals("image"))
                                        {
                                            __query.Append(__dataType);
                                            if (__charMaxLength > 0)
                                            {
                                                __query.Append("(" + __charMaxLength.ToString() + ")");
                                            }
                                            __byteACase = true;
                                        }
                                        else
                                            if (__dataType.Equals("character varying") || __dataType.Equals("varchar") || __dataType.Equals("character") || __dataType.Equals("char"))
                                            {
                                                __query.Append(__dataType);
                                                if (__charMaxLength > 0)
                                                {
                                                    __query.Append("(" + __charMaxLength.ToString() + ")");
                                                }
                                                __stringCase = true;
                                            }
                                            else
                                                if (__dataType.Equals("date") || __dataType.Equals("datetime") || __dataType.Equals("smalldatetime"))
                                                {
                                                    __query.Append(__dataType);
                                                    __stringCase = true;
                                                }
                                                else
                                                    if (__dataType.Equals("timestamp without time zone"))
                                                    {
                                                        __query.Append(__dataType);
                                                        __stringCase = true;
                                                    }
                                                    else
                                                        if (__dataType.Equals("double precision"))
                                                        {
                                                            __query.Append(__dataType);
                                                        }
                                                        else
                                                            if (__dataType.Equals("smallint"))
                                                            {
                                                                __query.Append(__dataType);
                                                            }
                                                            else
                                                            {
                                                                __query.Append(__dataType);
                                                            }
                                    }
                                    __fieldColumnName.Add(__columnName);
                                    __fieldDataStringType.Add(__stringCase);
                                    __fieldDataByteAType.Add(__byteACase);
                                    __fieldDataType.Add(__dataType);
                                    __fieldDataNull.Add(__isNull);
                                }
                                //
                                if (__isNull == false)
                                {
                                    __query.Append(" NOT NULL ");
                                }
                            }
                        }
                    }
                    if (__keyList.Rows.Count > 0)
                    {
                        __query.Append(",\n");
                        StringBuilder __indexField = new StringBuilder();
                        for (int __loop = 0; __loop < __keyList.Rows.Count; __loop++)
                        {
                            if (__indexField.Length > 0)
                            {
                                __indexField.Append(",");
                            }
                            __indexField.Append(__keyList.Rows[__loop]["column_name"].ToString());
                        }
                        __query.Append("CONSTRAINT " + __keyList.Rows[0]["constraint_name"].ToString() + " PRIMARY KEY (" + __indexField.ToString() + ")\n");
                    }
                    __query.Append(")\n");
                    if (this._createRadioButton.Checked)
                    {
                        // Create
                        __query.Append("CREATE TABLE " + __targetTableName + " (\n");
                        string __result = __myFrameWork._queryInsertOrUpdate(this._targetDatabaseName, __query.ToString().Replace("\n", " "));
                        if (__result.Length > 0)
                        {
                            this._resultText.Append("Create : " + __targetTableName + " fail.\r\n");
                            __transferData = false;
                        }
                    }
                    else
                    {
                        if (this._truncateRadioButton.Checked)
                        {
                            // Truncate
                            string __result = __myFrameWork._queryInsertOrUpdate(this._targetDatabaseName, "truncate table " + __targetTableName);
                            if (__result.Length > 0)
                            {
                                this._resultText.Append("Truncate : " + __targetTableName + " fail.\r\n");
                                __transferData = false;
                            }
                        }
                    }
                    this._resultGrid._cellUpdate(__row, _fieldTransferSuccess, (__transferData) ? "Transfer" : "Skip", false);
                    this._resultGrid._cellUpdate(__row, this._fieldCheck, 0, false);
                    this._resultGrid.Invalidate();
                    if (__transferData)
                    {
                        // Insert
                        ArrayList __fieldSourceName = new ArrayList();
                        ArrayList __fieldTargetName = new ArrayList();
                        StringBuilder __dataSourceField = new StringBuilder();
                        StringBuilder __dataTargetField = new StringBuilder();
                        switch (this._transferSelect)
                        {
                            case 0: // SML -> SML
                                {
                                    for (int __fieldScan = 0; __fieldScan < __fieldColumnName.Count; __fieldScan++)
                                    {
                                        if (__dataSourceField.Length != 0)
                                        {
                                            __dataSourceField.Append(",");
                                        }
                                        __dataSourceField.Append(__fieldColumnName[__fieldScan].ToString());
                                        //
                                        if (__dataTargetField.Length != 0)
                                        {
                                            __dataTargetField.Append(",");
                                        }
                                        __dataTargetField.Append(__fieldColumnName[__fieldScan].ToString());
                                        //
                                        __fieldSourceName.Add(__fieldColumnName[__fieldScan].ToString());
                                        __fieldTargetName.Add(__fieldColumnName[__fieldScan].ToString());
                                    }
                                }
                                break;
                            case 1: // BC Account -> SML
                                {
                                    int __addr = this._scriptFindSourceTableNameAddr(__sourceTableName) + 1;
                                    while (__addr < this._scriptArray.Count)
                                    {
                                        if (this._scriptArray[__addr][0] == '*' || this._scriptArray[__addr][0] == '!' || this._scriptArray[__addr][0] == '-')
                                        {
                                            break;
                                        }
                                        string[] __split = this._scriptArray[__addr].Split(',');
                                        if (__dataSourceField.Length != 0)
                                        {
                                            __dataSourceField.Append(",");
                                        }
                                        __dataSourceField.Append(__split[0].Trim().ToString());
                                        //
                                        if (__dataTargetField.Length != 0)
                                        {
                                            __dataTargetField.Append(",");
                                        }
                                        __dataTargetField.Append(__split[1].Trim().ToString());
                                        //
                                        __fieldSourceName.Add(__split[0].Trim().ToString());
                                        __fieldTargetName.Add(__split[1].Trim().ToString());
                                        __addr++;
                                    }
                                }
                                break;
                        }
                        if (__dataSourceField.Length > 0)
                        {
                            string __sqlCommand = "select " + __dataSourceField.ToString() + " from " + __sourceTableName + ((this._extraWhereTextbox.Text.Trim().Length > 0) ? " where " + this._extraWhereTextbox.Text : "");
                            StringBuilder __queryInsert = new StringBuilder();
                            int __count = 0;
                            int __countRecord = 0;
                            Npgsql.NpgsqlCommand __postgresCommandRead = null;
                            Npgsql.NpgsqlDataReader __postgresDataReader = null;
                            SqlCommand __microsoftSqlCommandRead = null;
                            SqlDataReader __microsoftSqlDataReader = null;
                            switch (this._providerSourceSelect)
                            {
                                case 0: // PostgresSQL
                                    __postgresCommandRead = new Npgsql.NpgsqlCommand(__sqlCommand, __postgresConn);
                                    __postgresDataReader = __postgresCommandRead.ExecuteReader();
                                    break;
                                case 2: // Microsoft SQL
                                    __microsoftSqlCommandRead = new SqlCommand(__sqlCommand, __microsoftSqlConn);
                                    __microsoftSqlDataReader = __microsoftSqlCommandRead.ExecuteReader();
                                    break;
                            }
                            while (true)
                            {
                                Boolean __read = false;
                                switch (this._providerSourceSelect)
                                {
                                    case 0: // PostgresSQL
                                        __read = __postgresDataReader.Read();
                                        break;
                                    case 2: // Microsoft SQL
                                        __read = __microsoftSqlDataReader.Read();
                                        break;
                                }
                                if (__read == false)
                                {
                                    break;
                                }
                                __countRecord++;
                                this._resultGrid._cellUpdate(__row, _fieldTransferSuccess, (__countRecord == 0) ? "0" : __countRecord.ToString(), false);
                                if (__queryInsert.Length == 0)
                                {
                                    __queryInsert.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                }
                                StringBuilder __data = new StringBuilder();
                                for (int __fieldScan = 0; __fieldScan < __fieldSourceName.Count; __fieldScan++)
                                {
                                    if (__data.Length != 0)
                                    {
                                        __data.Append(",");
                                    }
                                    int __findTypeAddr = __fieldScan;
                                    for (int __loop = 0; __loop < __fieldColumnName.Count; __loop++)
                                    {
                                        if (__fieldColumnName[__loop].ToString().Trim().ToUpper().Equals(__fieldSourceName[__fieldScan].ToString().ToUpper().Trim()))
                                        {
                                            __findTypeAddr = __loop;
                                            break;
                                        }
                                    }
                                    string __getData = "";
                                    byte[] __getByte = null;
                                    switch (this._providerSourceSelect)
                                    {
                                        case 0: // PostgresSQL
                                            if ((Boolean)__fieldDataByteAType[__findTypeAddr])
                                            {
                                                try
                                                {
                                                    __getByte = (byte[])__postgresDataReader.GetValue(__fieldScan);
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            else
                                            {
                                                __getData = __postgresDataReader[__fieldScan].ToString().Replace("\'", "\'\'");
                                            }
                                            break;
                                        case 2: // Microsoft SQL
                                            if ((Boolean)__fieldDataByteAType[__findTypeAddr])
                                            {
                                                __getByte = (byte[])__microsoftSqlDataReader.GetValue(__fieldScan);
                                            }
                                            else
                                            {
                                                __getData = __microsoftSqlDataReader[__fieldScan].ToString().Replace("\'", "\'\'");
                                            }
                                            break;
                                    }
                                    if ((Boolean)__fieldDataNull[__findTypeAddr] && (Boolean)__fieldDataStringType[__findTypeAddr] && __getData.ToString().Trim().Length == 0)
                                    {
                                        // กรณีวันที่
                                        if (__fieldDataType[__findTypeAddr].ToString().Equals("date") || __fieldDataType[__findTypeAddr].ToString().Equals("datetime") || __fieldDataType[__findTypeAddr].ToString().Equals("timestamp without time zone") || __fieldDataType[__findTypeAddr].ToString().Equals("smalldatetime"))
                                        {
                                            __data.Append("null");
                                        }
                                        else
                                        {
                                            __data.Append("\'\'");
                                        }
                                    }
                                    else
                                    {
                                        if ((Boolean)__fieldDataStringType[__findTypeAddr])
                                        {
                                            __data.Append("\'");
                                        }
                                        // กรณีวันที่
                                        if (__fieldDataType[__findTypeAddr].ToString().Equals("date") || __fieldDataType[__findTypeAddr].ToString().Equals("datetime") || __fieldDataType[__findTypeAddr].ToString().Equals("timestamp without time zone") || __fieldDataType[__findTypeAddr].ToString().Equals("smalldatetime"))
                                        {
                                            switch (this._providerSourceSelect)
                                            {
                                                case 0: // PostgresSQL

                                                    __getData = ((DateTime)__postgresDataReader[__fieldScan]).ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                                                    //.ToString().Replace("\'", "\'\'");

                                                    break;
                                                case 2: // Microsoft SQL

                                                    __getData = ((DateTime)__microsoftSqlDataReader[__fieldScan]).ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));

                                                    break;
                                            }

                                            __getData = MyLib._myGlobal._convertDateToQuery(this._convertDateFromQuery(__getData));
                                        }
                                        else
                                        {
                                            // กรณีเป็นตัวเลข ให้เป็น 0
                                            if ((Boolean)__fieldDataStringType[__findTypeAddr] == false && __getData.Length == 0)
                                            {
                                                __getData = "0";
                                            }
                                        }
                                        if ((Boolean)__fieldDataByteAType[__findTypeAddr])
                                        {
                                            if (__getByte != null)
                                            {
                                                __getData = "decode('" + Convert.ToBase64String(__getByte) + "','base64')";
                                            }
                                        }
                                        __data.Append(__getData);
                                        if ((Boolean)__fieldDataStringType[__findTypeAddr])
                                        {
                                            __data.Append("\'");
                                        }
                                    }
                                }
                                __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + __targetTableName + " (" + __dataTargetField.ToString() + ") values (" + __data.ToString() + ")"));
                                if (++__count == 6000 || __queryInsert.Length > 100000)
                                {
                                    __queryInsert.Append("</node>");
                                    string __resultInsert = "";
                                    try
                                    {
                                        __resultInsert = __myFrameWork._queryList(this._targetDatabaseName, MyLib._myGlobal._deleteAscError(__queryInsert.ToString()));
                                    }
                                    catch (Exception __ex)
                                    {
                                        MessageBox.Show(__ex.Message.ToString() + __queryInsert.ToString());
                                    }
                                    if (__resultInsert.Length > 0)
                                    {
                                        this._resultText.Append(__resultInsert + "\r\n");
                                        break;
                                    }
                                    __queryInsert = new StringBuilder();
                                    __count = 0;
                                }
                            }
                            if (__queryInsert.Length > 0)
                            {
                                __queryInsert.Append("</node>");
                                string __resultInsert = "";
                                try
                                {
                                    __resultInsert = __myFrameWork._queryList(this._targetDatabaseName, MyLib._myGlobal._deleteAscError(__queryInsert.ToString()));
                                }
                                catch (Exception __ex)
                                {
                                    MessageBox.Show(__ex.Message.ToString() + __queryInsert.ToString());
                                }
                                if (__resultInsert.Length > 0)
                                {
                                    this._resultText.Append(__resultInsert + "\r\n");
                                    break;
                                }
                            }
                            switch (this._providerSourceSelect)
                            {
                                case 0: // PostgresSQL
                                    __postgresDataReader.Close();
                                    __postgresCommandRead.Dispose();
                                    break;
                                case 2: // Microsoft SQL
                                    __microsoftSqlDataReader.Close();
                                    __microsoftSqlCommandRead.Dispose();
                                    break;
                            }
                        }
                    }
                }
            }
            */
            // end
            return true;
        }

        private void _selectNoneMaster_Click(object sender, EventArgs e)
        {
            foreach (Control __getControl in _groupboxMaster.Controls)
            {
                if (__getControl.GetType() == typeof(CheckBox))
                {
                    CheckBox __getCheckbox = (CheckBox)__getControl;
                    __getCheckbox.Checked = false;
                }
            }

        }

        private void _selectAllMaster_Click(object sender, EventArgs e)
        {
            foreach (Control __getControl in _groupboxMaster.Controls)
            {
                if (__getControl.GetType() == typeof(CheckBox))
                {
                    CheckBox __getCheckbox = (CheckBox)__getControl;
                    __getCheckbox.Checked = true;
                }
            }

        }

        Thread _processMasterThread;
        bool _onProcessDataResult = false;

        public bool _onProcess
        {
            get
            {
                return _onProcessDataResult;
            }
            set
            {
                _onProcessDataResult = value;
                this._groupboxMaster.Enabled = !value;

            }
        }

        int _maxProcessTable = 0;
        int _currentProcessTable = 0;
        int _totalProcessRecord = 0;
        int _currentProcessRecord = 0;

        private void _startTransferMasterButton_Click(object sender, EventArgs e)
        {
            this._onProcess = true;
            _maxProcessTable = 0;
            _currentProcessTable = 0;

            //this._processMaster();
            _processMasterThread = new Thread(new ThreadStart(_processMaster));
            _processMasterThread.IsBackground = true;
            _processMasterThread.Start();

            timer1.Start();
        }


        void _processMaster()
        {

            if (this._icCheckbox.Checked == true)
            {
                _maxProcessTable = 5;
                _currentProcessTable = 0;
                _totalProcessRecord = 0;
                _currentProcessRecord = 0;
                _currentProcessTable++;
                _copyTable(_g.d.ic_unit._table, _g.d.ic_unit._code);

                _currentProcessTable++;
                _currentProcessRecord = 0;
                _copyTable(_g.d.ic_inventory._table, _g.d.ic_inventory._code);

                _currentProcessTable++;
                _currentProcessRecord = 0;
                _copyTable(_g.d.ic_inventory_detail._table, _g.d.ic_inventory_detail._ic_code);

                _currentProcessTable++;
                _currentProcessRecord = 0;
                _copyTable(_g.d.ic_inventory_barcode._table, _g.d.ic_inventory_barcode._ic_code + "," + _g.d.ic_inventory_barcode._barcode);

                _currentProcessTable++;
                _currentProcessRecord = 0;
                _copyTable(_g.d.ic_unit_use._table, _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._code);
                //_copyTable(_g.d.ic_._table, _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._code);
                //_copyTable(_g.d.ic_unit_use._table, _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._code);
                //_copyTable(_g.d.ic_unit_use._table, _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._code);

                this._icCheckbox.Checked = false;
            }

            if (this._apCheckbox.Checked == true)
            {
                _maxProcessTable = 2;
                _currentProcessTable = 0;
                _totalProcessRecord = 0;
                _currentProcessRecord = 0;
                _currentProcessTable++;
                _copyTable(_g.d.ap_supplier._table, _g.d.ap_supplier._code);

                _currentProcessTable++;
                _copyTable(_g.d.ap_supplier_detail._table, _g.d.ap_supplier_detail._ap_code);
                this._apCheckbox.Checked = false;
            }

            if (this._arCheckbox.Checked == true)
            {
                _maxProcessTable = 2;
                _currentProcessTable = 0;
                _totalProcessRecord = 0;
                _currentProcessRecord = 0;
                _currentProcessTable++;
                _copyTable(_g.d.ar_customer._table, _g.d.ar_customer._code);

                _currentProcessTable++;
                _copyTable(_g.d.ar_customer_detail._table, _g.d.ar_customer_detail._ar_code);
                this._arCheckbox.Checked = false;
            }

            this._onProcess = false;
            MessageBox.Show("Process Success");
        }

        #region Master Transfer
        /// <summary>
        /// สร้างตาราง ฝั่ง client (temp)
        /// </summary>
        /// <param name="tableName"></param>
        void _copyTable(string tableName, string fieldCompare)
        {
            MyLib._myFrameWork __datacenterFrameWork = new MyLib._myFrameWork(this._sourceWebServiceTextbox.Text, "SMLConfig" + this._sourceProviderTextbox.Text.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
            __datacenterFrameWork._databaseSelectType = __datacenterFrameWork._setDataBaseCode();
            List<_syncFieldListStruct> __fieldListServer = _getFieldFromXml(__datacenterFrameWork._getSchemaTable(this._sourceDatabaseNameTextbox.Text, tableName));

            string __host = "";
            string __provider = "";
            string __group = "";
            string __userCode = "";
            string __password = "";

            if (_connectSameSourceCheckbox.Checked == true)
            {
                __host = this._sourceWebServiceTextbox.Text;
                __provider = this._sourceProviderTextbox.Text;
                __group = this._sourceGroupTextbox.Text;
                __userCode = this._sourceUserCodeTextbox.Text;
                __password = this._sourcePasswordTextbox.Text;
            }
            else
            {
                __host = this._desWebServiceTextbox.Text;
                __provider = this._desProviderTextbox.Text;
                __group = this._desGroupTextbox.Text;
                __userCode = this._desUserCodeTextbox.Text;
                __password = this._desPasswordTextbox.Text;
            }

            MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork(__host, "SMLConfig" + __provider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
            __clientFrameWork._databaseSelectType = __clientFrameWork._setDataBaseCode();
            List<_syncFieldListStruct> __fieldListClient = _getFieldFromXml(__clientFrameWork._getSchemaTable(this._desDatabaseNameTextbox.Text, tableName));

            // ลบ Field ที่ไม่ตรงกัน ระหว่าง server,client
            // หาจาก field server ถ้าไม่มีใน field client ลบ field server ออก
            int __loop1 = 0;
            while (__loop1 < __fieldListServer.Count)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __fieldListClient.Count && __found == false; __loop2++)
                {
                    if (__fieldListServer[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListClient[__loop2]._fieldNameForInsertUpdate))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __fieldListServer.RemoveAt(__loop1);
                }
                else
                {
                    __loop1++;
                }
            }

            // หาใน field client ถ้าไม่มีใน field server ให้ลบ field client ออก
            __loop1 = 0;
            while (__loop1 < __fieldListClient.Count)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __fieldListServer.Count && __found == false; __loop2++)
                {
                    if (__fieldListClient[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListServer[__loop2]._fieldNameForInsertUpdate))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __fieldListClient.RemoveAt(__loop1);
                }
                else
                {
                    __loop1++;
                }
            }

            // ประกอบ field
            StringBuilder __fieldForInsert = new StringBuilder();
            StringBuilder __fieldForSelect = new StringBuilder();
            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
            {

                // if (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("is_lock_record") == false && __fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("roworder") == false && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("last_movement_date") == false) && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("guid_code") == false))
                {
                    if (__fieldForSelect.Length > 0)
                    {
                        __fieldForInsert.Append(",");
                        __fieldForSelect.Append(",");
                    }

                    __fieldForInsert.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate.ToString());
                    __fieldForSelect.Append(__fieldListClient[__loop]._fieldNameForSelect.ToString());

                    //if (__compareQuery.Length > 0)
                    //{
                    //    __compareQuery.Append(" and ");
                    //}

                    //__compareQuery.Append(tableName + "." + __fieldListClient[__loop]._fieldNameForSelect + "=" + targetTableName + "." + __fieldListClient[__loop]._fieldNameForSelect);
                }
            }


            // ดึงรายการมาตรวจ



            string __queryGetFieldCompare = "select " + fieldCompare + " from " + tableName + " order by " + fieldCompare;
            DataTable __getCompareFieldDataTable = __datacenterFrameWork._query(this._sourceDatabaseNameTextbox.Text, __queryGetFieldCompare).Tables[0];

            this._totalProcessRecord = __getCompareFieldDataTable.Rows.Count;

            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            int __queryCount = 0;

            foreach (DataRow row in __getCompareFieldDataTable.Rows)
            {
                this._currentProcessRecord++;
                // check client
                StringBuilder __where = new StringBuilder();

                string[] __fieldCompareSplit = fieldCompare.Split(',');

                foreach (string field in __fieldCompareSplit)
                {
                    if (__where.Length > 0)
                    {
                        __where.Append(" and ");
                    }

                    __where.Append(field + "=\'" + row[field].ToString() + "\'");
                }

                // compare ทีละ record
                string __compareQuery = "select " + __fieldForSelect + " from " + tableName + " where " + __where;
                using (DataTable __compareDataTable = __clientFrameWork._query(this._desDatabaseNameTextbox.Text, __compareQuery).Tables[0])
                {
                    bool __found = false;
                    bool __isUpdate = false;

                    if (__compareDataTable.Rows.Count > 0)
                    {
                        __found = true;
                    }


                    using (DataTable __sourceDataTable = __datacenterFrameWork._query(this._sourceDatabaseNameTextbox.Text, __compareQuery).Tables[0])
                    {
                        #region Start Compare

                        if (__found == true)
                        {
                            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                            {

                                if (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("is_lock_record") == false && __fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("roworder") == false && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("last_movement_date") == false) && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("guid_code") == false))
                                {
                                    switch (__fieldListClient[__loop]._fieldType.ToLower())
                                    {
                                        case "bytea":
                                        case "image":
                                            {
                                                string __valueClient = __compareDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                                string __valueSource = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();

                                                if (__valueClient != __valueSource)
                                                {
                                                    __isUpdate = true;
                                                }
                                            }
                                            break;
                                        case "uniqueidentifier":
                                        case "uuid":
                                        case "varchar":
                                            {
                                                //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                string __valueClient = __compareDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'"); ;
                                                string __valueSource = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'"); ;

                                                if (__valueClient != __valueSource)
                                                {
                                                    __isUpdate = true;
                                                }
                                            }
                                            break;
                                        case "date":
                                        case "datetime":
                                        case "smalldatetime":
                                        case "timestamp":
                                            {
                                                //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                string __valueClient = __compareDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __valueClient = (__valueClient.Length == 0) ? "null" : "\'" + __valueClient + "\'";

                                                string __valueSource = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __valueSource = (__valueSource.Length == 0) ? "null" : "\'" + __valueSource + "\'";

                                                if (__valueClient != __valueSource)
                                                {
                                                    __isUpdate = true;
                                                }

                                            }
                                            break;
                                        default: // ตัวเลข
                                            {
                                                //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                string __valueClient = __compareDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __valueClient = (__valueClient.Length == 0) ? "0" : "\'" + __valueClient + "\'";

                                                string __valueSource = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __valueSource = (__valueSource.Length == 0) ? "0" : "\'" + __valueSource + "\'";


                                                if (__valueClient != __valueSource)
                                                {
                                                    __isUpdate = true;
                                                }
                                            }
                                            break;
                                    }
                                }

                            }
                        }
                        #endregion


                        StringBuilder __query = new StringBuilder();
                        if (__found == false)
                        {
                            // insert
                            __query.Append("insert into " + tableName + " (" + __fieldForInsert + ") values (");
                            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                            {
                                if (__loop != 0)
                                {
                                    __query.Append(",");
                                }

                                if (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("is_lock_record") == false && __fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("roworder") == false && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("last_movement_date") == false) && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("guid_code") == false))
                                {
                                    switch (__fieldListClient[__loop]._fieldType.ToLower())
                                    {
                                        case "bytea":
                                        case "image":
                                            {
                                                string __value = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                                __query.Append("decode(\'" + __value + "\',\'base64\')");
                                            }
                                            break;
                                        case "uniqueidentifier":
                                        case "uuid":
                                        case "varchar":
                                            {
                                                //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                string __value = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __query.Append("\'" + __value + "\'");
                                            }
                                            break;
                                        case "date":
                                        case "datetime":
                                        case "smalldatetime":
                                        case "timestamp":
                                            {
                                                //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                string __value = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                            }
                                            break;
                                        default: // ตัวเลข
                                            {
                                                //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                string __value = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (__fieldListClient[__loop]._fieldNameForSelect.ToLower())
                                    {
                                        case "last_movement_date":
                                            __query.Append("null");
                                            break;
                                        case "guid_code":
                                            __query.Append("\'\'");
                                            break;
                                        default:
                                            __query.Append("0");
                                            break;
                                    }
                                }
                            }
                            __query.Append(")");
                        }
                        else if (__isUpdate == true)
                        {
                            // update
                            __query.Append("update " + tableName + " set ");
                            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                            {
                                if (__loop != 0)
                                {
                                    __query.Append(",");
                                }
                                __query.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate + "=");

                                if (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("is_lock_record") == false && __fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("roworder") == false && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("last_movement_date") == false) && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("guid_code") == false))
                                {

                                    switch (__fieldListClient[__loop]._fieldType.ToLower())
                                    {
                                        case "bytea":
                                        case "image":
                                            {
                                                string __value = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                                __query.Append("decode(\'" + __value + "\',\'base64\')");
                                            }
                                            break;
                                        case "uniqueidentifier":
                                        case "uuid":
                                        case "varchar":
                                            {
                                                string __value = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __query.Append("\'" + __value + "\'");
                                            }
                                            break;
                                        case "date":
                                        case "datetime":
                                        case "smalldatetime":
                                        case "timestamp":
                                            {
                                                string __value = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                            }
                                            break;
                                        default: // ตัวเลข
                                            {
                                                string __value = __sourceDataTable.Rows[0][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (__fieldListClient[__loop]._fieldNameForSelect.ToLower())
                                    {
                                        case "last_movement_date":
                                            __query.Append("null");
                                            break;
                                        case "guid_code":
                                            __query.Append("\'\'");
                                            break;
                                        default:
                                            __query.Append("0");
                                            break;
                                    }
                                }
                            }
                            //__query.Append(" where " + __whereInTable.ToString());
                            __query.Append(" where " + __where);
                        }
                        //string __compareResult = __clientFrameWork._queryInsertOrUpdate(this._desDatabaseNameTextbox.Text, __query.ToString());
                        if (__query.Length > 0)
                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                        __queryCount++;

                    }

                    //__queryList.Append("</node>");

                }
                // 
                if (__queryCount == 1000)
                {
                    __queryList.Append("</node>");
                    string __result = __clientFrameWork._queryList(this._desDatabaseNameTextbox.Text, __queryList.ToString());
                    __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    __queryCount = 0;
                }
            }

            __queryList.Append("</node>");
            string __compareResult = __clientFrameWork._queryList(this._desDatabaseNameTextbox.Text, __queryList.ToString());
            __queryCount = 0;


            // delete
            /*
            if (__getXmlClient.IndexOf("column_name") == -1)
            {
                // ถ้าไม่มีโครงสร้าง ฝั่ง client สร้างทันที

                // ดึงโครงสร้าง ฝั่ง server 
                if (_xDoc == null)
                {
                    MyLib._myFrameWork __datacenterFrameWork = new MyLib._myFrameWork(_global._datacenter_server, _global._datacenter_configFileName, _global._datacenter_database_type);
                    DataSet __myDataSet = __datacenterFrameWork._dataStruct(MyLib._myGlobal._databaseStructFileName);

                    _xDoc = new XmlDocument();
                    _xDoc.LoadXml(__myDataSet.GetXml());
                    _xDoc.DocumentElement.Normalize();

                }

                // filter
                XmlNode __tableSelect = _getTableStruct(tableName);

                // gen script สร้างฐานข้อมูล
                string __createTableQuery = _createTableScript(__tableSelect, __tempTableName);
                string __result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __createTableQuery);
                if (__result.Length == 0)
                {
                    this.Invoke(_sendMessageResult, new object[] { "create table " + __tempTableName + " success " });

                }
                else
                {
                    this.Invoke(_sendMessageResult, new object[] { __result });
                    _errorCount++;
                }


                _checkIndex(__tableSelect, __tempTableName);
            }

            // กระหน่ำ
            _transFerTable(tableName, __tempTableName);

            // compare และโอน จาก temp ไป table จริง
            _compareTable(__tempTableName, tableName);
            */
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this._onProcess == false)
            {
                timer1.Stop();
            }

            // show label
            this._processMasterProgressBar.Maximum = this._maxProcessTable;
            this._processMasterProgressBar.Value = this._currentProcessTable;

            this._processMasterStatusLabel.Text = this._currentProcessRecord + "/" + this._totalProcessRecord;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (__sourceGUID.Length > 0 && __targetGUID.Length > 0)
            {

            }
            else
            {
                if (this.tabControl1.SelectedIndex != 0)
                {
                    MessageBox.Show("ยังไม่ได้เชื่อมต่อฐานข้อมูล");
                    this.tabControl1.SelectedIndex = 0;
                }
            }
        }
    }
}
