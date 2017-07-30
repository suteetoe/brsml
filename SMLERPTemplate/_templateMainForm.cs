using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Xml;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Globalization;
using MyLib;
using Crom.Controls.Docking;
using System.Management;
using System.Xml.Serialization;

namespace SMLERPTemplate
{
    public partial class _templateMainForm : Form
    {
        string _imageDatabaseName = "thai7";
        public string _programName = "";
        public string _versionInfo = "";
        public MyLib._myTreeView _mainMenu;
        MyLib._webServiceShowStatus webServiceShowStatus;
        MyLib._computerStatus computerShowStatus;
        SMLERPControl._myMenu _myMenuScreen;
        ArrayList _board = new ArrayList();
        public ImageList _menuImageList;
        bool _displayConnectFail = false;
        string _userConnected = "";
        string _processInfoLabel = "";
        Thread _myFindWebserviceThread = null;
        Thread _processStatusBarThread = null;
        Thread _processCaptureScreenThread = null;
        Thread _dataSyncThread = null;
        Thread _pointCenterSyncThread = null;
        Thread _internetSyncThread = null;
        private DockContainer _dock = new DockContainer();
        public _myOutLookForm _outLook = null;
        List<_myMenuClass> _myMenu = new List<_myMenuClass>();
        List<_myMenuGroupClass> _myMenuGroup = new List<_myMenuGroupClass>();
        Thread _getPrinterWork = null;
        Thread _getFastReportWork = null;
        Thread _getMasterFormDesignThread = null;
        Thread _checkTransectionThread = null;

        Panel _homeMenu = new Panel();

        Thread _getReportServer = null;

        public Boolean _getResourceMenu = true;

        //
        public _templateMainForm()
        {
            InitializeComponent();
            // toe ย้ายไป เพื่อจะได้ override เอา
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._menuBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _constructMethod();
            try
            {
                string __smlSoftPath = @"C:\smlsoft\";
                bool __isDirCreate = MyLib._myUtil._dirExists(__smlSoftPath);

                if (__isDirCreate == false)
                {
                    System.IO.Directory.CreateDirectory(__smlSoftPath); // create folders
                }
            }
            catch
            {
            }
        }

        public virtual void _constructMethod()
        {
            this.Load += new System.EventHandler(this._templateMainForm_Load);

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._syncDataButton.Visible = false;
                _memoryCleaner __memoryCleaner = new _memoryCleaner();
                __memoryCleaner.Start();
                //somruk                  
                Form __myRefer = this; //somruk
                if (__myRefer.GetType().ToString().Equals("ANBridgeCenterAdmin._mainScreen") || __myRefer.GetType().ToString().Equals("SMLOffTakeSalesAdmin._mainScreen") || __myRefer.GetType().ToString().Equals("SMLHealthy._mainForm"))
                {
                    this._misButton.Visible = false;
                }
                //
                if (SystemInformation.ComputerName.ToLower().IndexOf("jead") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("fon") != -1)
                {
                    this._screenSize.Dispose();
                }
                // start
                this._myMenuScreen = new SMLERPControl._myMenu();
                ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
                this._menuPanel.Visible = false;
                this.MinimumSize = new Size(600, 600);
                this.DoubleBuffered = true;
                /*this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.UseTextForAccessibility, true);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.SetStyle(ControlStyles.CacheText, true);*/
                //

                /* ไม่เข้าใจ ทำไมต้อง load อาจจะกำหนดสิทธิ์
                 MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                try
                {
                    MyLib._myResource._resource = __myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                }
                catch
                {
                }*/
                //
                _statusCompressWebservice.Image = _imageList16x16.Images[(MyLib._myGlobal._compressWebservice == true) ? 0 : 1];
                //
                _statusWebserviceLabel.MouseHover += new EventHandler(_statusWebserviceLabel_MouseHover);
                _statusWebserviceLabel.MouseLeave += new EventHandler(_statusWebserviceLabel_MouseLeave);
                //
                _statusUserList.MouseHover += new EventHandler(_statusUserList_MouseHover);
                _statusUserList.MouseLeave += new EventHandler(_statusUserList_MouseLeave);
                //
                _statusComputerInformation.MouseHover += new EventHandler(_statusComputerInformation_MouseHover);
                _statusComputerInformation.MouseLeave += new EventHandler(_statusComputerInformation_MouseLeave);
                //
                this._tabControl.DoubleClick += new EventHandler(_tabControl_DoubleClick);
                //
                this._outLook = new _myOutLookForm();
                this._outLook._editGroupButton.Click += new EventHandler(_editGroupButton_Click);
                this._outLook._deleteMenuButton.Click += new EventHandler(_deleteMenuButton_Click);
                this._outLook._listView.DoubleClick += new EventHandler(_listView_DoubleClick);
                this._outLook._addFastReportToMenuButton.Click += new EventHandler(_addMenuButton_Click);

                // toe get printer name thread
                this._getPrinterWork = new Thread(_getPrinter);
                this._getPrinterWork.IsBackground = true;
                this._getPrinterWork.Start();

                // โต๋ย้าย ไปที่อื่น เพื่อเช็คว่าเป็นเครื่อง process หรือเปล่า จะได้ลดภาระการทำงานลง
                /*if (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLBIllFree)
                {
                    // toe check fast report
                    bool __isProcessDevice = false;
                    if (_g.g._companyProfile._process_serial_device.Length > 0)
                    {
                        MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                        string[] _dataDive = Environment.GetLogicalDrives();


                        for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                        {
                            string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();
                            if (__getDeviceCode.Length > 0 && _g.g._companyProfile._process_serial_device.ToLower().IndexOf(__getDeviceCode) != -1)
                            {
                                __isProcessDevice = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        __isProcessDevice = true;
                    }

                    if (__isProcessDevice && MyLib._myGlobal._databaseName.Length > 0)
                    {
                        this._doImportReportWord();
                        // get master form
                        this._doImportMasterFormDesign();
                    }
                }*/

                this.Disposed += new EventHandler(_templateMainForm_Disposed);
                // jead MyLib._myGlobal._registerProcess();
                this._homeMenu.Dock = DockStyle.Fill;
                this._homeMenu.BackColor = Color.Transparent;
                //this.Home.Controls.Add(this._homeMenu); toe ย้ายไปใส่ใน dock

                if (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLColorStore && MyLib._myGlobal._programName.Equals("DTS Client Download") == false && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLIMS && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.IMSPOS)
                {
                    Form __formMenu = new Form();
                    __formMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(104)))), ((int)(((byte)(147)))));
                    __formMenu.Controls.Add(this._homeMenu);

                    DockableFormInfo __formFill = this._dock.Add(__formMenu, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                    this._dock.DockForm(__formFill, DockStyle.Fill, zDockMode.Inner);
                    __formFill.ShowCloseButton = false;
                    __formFill.ShowContextMenuButton = false;
                }
            }
        }

        protected void _manageTableForAutoUnlock()
        {
            MyLib._myGlobal._tableForAutoUnlock.Clear();
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_branch_list._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.gl_chart_of_account._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.gl_journal._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_inventory._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_inventory_barcode._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ap_supplier._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ar_customer._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ar_customer._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_asset._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_asset_maintenance._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_asset_sale._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_pass_book._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_province._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_amper._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_tambon._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_doc_format._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_user_group._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ap_ar_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_income_list._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_expenses_list._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_view_table_custom._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.kitchen_master._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.table_master._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.POS_ID._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_purchase_permium._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.coupon_list._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ap_ar_transport_label._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_warehouse._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_shelf._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_promotion_formula._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_user._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_bank._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_bank_branch._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.edi_external._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_wms_trans._table);

            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_specific_search_word._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_trans._table);

        }

        public Boolean _addHomeShortcut(int panelIndex)
        {
            _selectMenuShortcutForm _selectForm = new _selectMenuShortcutForm();

            foreach (TreeNode node in this._mainMenu.Nodes)
            {
                _selectForm._menuTree.Nodes.Add((TreeNode)node.Clone());
            }
            _selectForm.StartPosition = FormStartPosition.CenterScreen;
            _selectForm.ShowDialog();
            if (_selectForm.DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                // save to database
                MyLib._myFrameWork __ws = new _myFrameWork();
                string _query = "insert into " + _g.d.homeshortcut._table + "(" + _g.d.homeshortcut._menu_code + "," + _g.d.homeshortcut._menu_name + "," + _g.d.homeshortcut._menu_tag + "," + _g.d.homeshortcut._menu_group + ") values (\'" + _selectForm._selectMenuCode + "\',\'" + _selectForm._selectMenuName + "\',\'" + _selectForm._selectMenuTag + "\'," + panelIndex + ") ";
                string __result = __ws._queryInsertOrUpdate(MyLib._myGlobal._databaseName, _query);
                if (__result.Length == 0)
                    return true;
                else
                    MessageBox.Show(__result);
            }

            return false;
        }

        public void _doImportReportWord()
        {
            if (MyLib._myGlobal._programName.Equals("SML CM") == false)
            {
                this._getFastReportWork = new Thread(_getFastReport);
                this._getFastReportWork.IsBackground = true;
                this._getFastReportWork.Start();
            }
        }

        void _getFastReport()
        {
            string _reportFileName = "smlfastreport.xml";

            try
            {
                MyLib._myFrameWork __ws = new _myFrameWork();
                string _query = "select count(" + _g.d.sml_fastreport._menuid + ") as countreport from " + _g.d.sml_fastreport._table;
                DataSet __ds = __ws._query(MyLib._myGlobal._databaseName, _query);

                StreamReader __source = MyLib._getStream._getDataStream(_reportFileName);

                XmlDocument _reportXML = new XmlDocument();
                _reportXML.Load(__source);

                if (MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0]["countreport"].ToString()) == 0)
                {
                    XmlNodeList __xmlList = _reportXML.SelectNodes("/node/report");

                    foreach (XmlNode __node in __xmlList)
                    {

                        string __reportId = __node.Attributes["id"].Value.ToString();
                        string __reportName = __node.Attributes["name"].Value.ToString();
                        string __reportTimeUpdate = __node.Attributes["timeupdate"].Value.ToString();
                        string __reportContent = __node["content"].InnerText;

                        byte[] __reportByte = Convert.FromBase64String(__reportContent);
                        string __reportStr = MyLib._compress._deCompressString(__reportByte);

                        //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Get Report " + __reportName + " Value", false);

                        SMLFastReport._xmlClass __xmlClass = (SMLFastReport._xmlClass)MyLib._myGlobal.FromXml(__reportStr, typeof(SMLFastReport._xmlClass));

                        XmlSerializer __xs = new XmlSerializer(typeof(SMLFastReport._xmlClass));
                        MemoryStream __memoryStream = new MemoryStream();
                        __xs.Serialize(__memoryStream, __xmlClass);

                        string _queryIns = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + "," + _g.d.sml_fastreport._reportdata + ") VALUES('{0}','{1}', '{2}',?)", __reportId, __reportName, __reportTimeUpdate);

                        byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

                        string __result = __ws._queryByteData(MyLib._myGlobal._databaseName, _queryIns, new object[] { __memoryStreamCompress });

                        if (__result.Length == 0)
                        {
                            Console.WriteLine("Import XML Report : " + __reportId + " Success \\n");
                        }
                        else
                        {
                            Console.WriteLine("Error XML Report : " + __reportId + " " + __result + " \\n");
                        }
                        //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Insert Report " + __reportName + " Result : " + __result, false);
                    }
                }
                else
                {
                    //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Compare Import Mode", false);

                    // get report list to compare
                    string __query = "select " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._timeupdate + " from " + _g.d.sml_fastreport._table;
                    DataTable __reportResult = __ws._queryShort(__query).Tables[0];

                    // compare mode
                    XmlNodeList __xmlList = _reportXML.SelectNodes("/node/report");
                    foreach (XmlNode __node in __xmlList)
                    {
                        string __reportId = __node.Attributes["id"].Value.ToString();
                        string __reportName = __node.Attributes["name"].Value.ToString();
                        string __reportTimeUpdate = __node.Attributes["timeupdate"].Value.ToString();
                        DateTime __xmlTimeUpdate = DateTime.Parse(__reportTimeUpdate, MyLib._myGlobal._cultureInfo());

                        //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Get Report " + __reportName + " Value", false);

                        //string _compare = "select " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._timeupdate + " from " + _g.d.sml_fastreport._table + " where " + _g.d.sml_fastreport._menuid + "=\'" + __reportId + "\' ";
                        //DataTable __compareResult = __ws._queryShort(_compare).Tables[0];
                        DataRow[] __select = __reportResult.Select(_g.d.sml_fastreport._menuid + "=\'" + __reportId + "\'");
                        bool _importReport = false;

                        if (__select.Length == 0)
                        {
                            _importReport = true;
                        }
                        else
                        {
                            DateTime _reportTimeUpdate = DateTime.Parse(__select[0][_g.d.sml_fastreport._timeupdate].ToString(), MyLib._myGlobal._cultureInfo());

                            if (__xmlTimeUpdate > _reportTimeUpdate)
                            {
                                _importReport = true;
                            }
                        }

                        if (_importReport)
                        {
                            __ws._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.sml_fastreport._table + " where " + MyLib._myGlobal._addUpper(_g.d.sml_fastreport._menuid) + "=\'" + __reportId + "\' ");

                            string __reportContent = __node["content"].InnerText;

                            byte[] __reportByte = Convert.FromBase64String(__reportContent);
                            string __reportStr = MyLib._compress._deCompressString(__reportByte);
                            SMLFastReport._xmlClass __xmlClass = (SMLFastReport._xmlClass)MyLib._myGlobal.FromXml(__reportStr, typeof(SMLFastReport._xmlClass));

                            XmlSerializer __xs = new XmlSerializer(typeof(SMLFastReport._xmlClass));
                            MemoryStream __memoryStream = new MemoryStream();
                            __xs.Serialize(__memoryStream, __xmlClass);

                            string _queryIns = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + "," + _g.d.sml_fastreport._reportdata + ") VALUES('{0}','{1}', '{2}',?)", __reportId, __reportName, __reportTimeUpdate);

                            byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

                            string __result = __ws._queryByteData(MyLib._myGlobal._databaseName, _queryIns, new object[] { __memoryStreamCompress });

                            //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Insert Report " + __reportName + " Result : " + __result, false);
                            if (__result.Length == 0)
                            {
                                Console.WriteLine("Import XML Report : " + __reportId + " Success \\n");
                            }
                            else
                            {
                                Console.WriteLine("Error XML Report : " + __reportId + " " + __result + " \\n");
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Exception In Import Fast Report" + ex.ToString(), false);
            }

            //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : End Process Import Report ", false);

        }

        //XmlDocument _formdesignXML;
        public void _doImportMasterFormDesign()
        {
            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOS ||
                MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.IMSPOS ||
                MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOSMED ||
                MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOSLite ||
                MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong)
            {
                this._getMasterFormDesignThread = new Thread(_getMasterFormDesign);
                this._getMasterFormDesignThread.IsBackground = true;
                this._getMasterFormDesignThread.Start();
            }
        }

        void _getMasterFormDesign()
        {
            string _reportFileName = "smlformdesign.xml";
            try
            {
                // import form design
                MyLib._myFrameWork __ws = new _myFrameWork();
                string _query = "select count(" + _g.d.formdesign._formcode + ") as countform from " + _g.d.formdesign._table + " where  " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + " IN (\'INV1\', \'INV2\', \'INV3\')";
                DataSet __ds = __ws._query(MyLib._myGlobal._databaseName, _query);


                XmlDocument _formdesignXML = new XmlDocument();
                StreamReader __source = MyLib._getStream._getDataStream(_reportFileName);
                //    string __getXml = __source.ReadToEnd();
                _formdesignXML.Load(__source);
                __source.Close();

                if (MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0]["countform"].ToString()) == 0)
                {
                    //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posinitformdesignlog.txt", DateTime.Now.ToString() + " : NOT FOUND inv1 inv2 inv3", true);

                    XmlNodeList __xmlList = _formdesignXML.SelectNodes("/node/formdesign");
                    foreach (XmlNode __node in __xmlList)
                    {
                        string __reportId = __node.Attributes["formcode"].Value.ToString();
                        string __reportName = __node.Attributes["formname"].Value.ToString();
                        string __reportContent = __node["content"].InnerText;

                        byte[] __reportByte = Convert.FromBase64String(__reportContent);
                        string __reportStr = MyLib._compress._deCompressString(__reportByte);
                        SMLReport._formReport.SMLFormDesignXml __xmlClass = (SMLReport._formReport.SMLFormDesignXml)MyLib._myGlobal.FromXml(__reportStr, typeof(SMLReport._formReport.SMLFormDesignXml));

                        XmlSerializer __xs = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                        MemoryStream __memoryStream = new MemoryStream();
                        __xs.Serialize(__memoryStream, __xmlClass);

                        string _queryIns = string.Format("insert into " + _g.d.formdesign._table + "(" + _g.d.formdesign._formcode + "," + _g.d.formdesign._formname + ", " + _g.d.formdesign._timeupdate + "," + _g.d.formdesign._formguid_code + "," + _g.d.formdesign._formdesigntext + ") VALUES('{0}','{1}', '', '{2}',?)", __reportId, __reportName, MyLib._myGlobal._convertDateToQuery(DateTime.Now));

                        byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

                        if (MyLib._myGlobal._allowProcessReportServer == true)
                        {
                            string __result = __ws._queryByteData(MyLib._myGlobal._databaseName, _queryIns, new object[] { __memoryStreamCompress });
                        }
                    }
                }
                else
                {
                    //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posinitformdesignlog.txt", DateTime.Now.ToString() + " : found inv1 inv2 inv3 and NO IMPORT", true);
                }

            }
            catch
            {
                //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posinitformdesignlog.txt", DateTime.Now.ToString() + " : Exception to access system.io from thread", true);
            }
        }

        void _addMenuButton_Click(object sender, EventArgs e)
        {

            try
            {
                // toe เช็คสิทธิ์
                // ตรวจสอบสิทธิ์

                MyLib._mainMenuClass __listmenu = new MyLib._mainMenuClass();
                __listmenu = MyLib._myGlobal._listMenuAll;
                //bool __ischeckMainmenu = (mainMenuId.Equals(menuName)) ? true : false;
                //string _mainMenuCode = "";
                // start
                //if (__ischeckMainmenu == false)
                {
                    //string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
                    // _mainMenuCode = menuName;
                    string mainMenuId = "menu_setup";
                    string _mainMenuCode = "fast_report_designer";
                    _PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(mainMenuId, _mainMenuCode);
                    MyLib._myGlobal._mainMenuIdPassTrue = mainMenuId;
                    MyLib._myGlobal._mainMenuCodePassTrue = _mainMenuCode;
                    _g.g._companyProfileLoad();
                    //
                    if (__permission._isRead || MyLib._myGlobal._userCode.ToLower().Equals("superadmin"))
                    {
                        _menuSelectReportForm __form = new _menuSelectReportForm();
                        __form._grid._gridData._mouseClick += (s2, e2) =>
                        {
                            string __code = ((MyLib._myGrid)s2)._cellGet(e2._row, 0).ToString();
                            string __name = ((MyLib._myGrid)s2)._cellGet(e2._row, 1).ToString();
                            //
                            string __groupID = "";
                            if (this._myMenuGroup.Count > 0)
                            {
                                _seletMenuGroupForm __select = new _seletMenuGroupForm();
                                for (int __loop = 0; __loop < this._myMenuGroup.Count; __loop++)
                                {
                                    __select._listBox.Items.Add(this._myMenuGroup[__loop]._name);
                                }
                                __select._listBox.SelectedIndexChanged += (s1, e1) =>
                                {
                                    try
                                    {
                                        __groupID = __select._listBox.SelectedItem.ToString();
                                    }
                                    catch
                                    {
                                    }
                                    __select.Dispose();
                                };
                                __select.ShowDialog();
                            }
                            _myMenuClass _newMenu = new _myMenuClass();
                            //string __temp = MyLib._myResource._findResource(__code, __name)._str;
                            _newMenu._menuID = __code;
                            _newMenu._groupID = __groupID;
                            _newMenu._menuName = __name;
                            _newMenu._tag = "&fastreport&";
                            this._myMenu.Add(_newMenu);
                            this._buildMyMenu(true);
                            __form.Close();
                        };
                        __form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถเข้าเมนูนี้ได้"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _listView_DoubleClick(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < this._outLook._listView.SelectedItems.Count; __loop++)
            {
                if (this._outLook._listView.SelectedItems[__loop].Selected)
                {
                    this._activeMenu("sml_soft_home_screen_menu_fast_report", ((_myListView)this._outLook._listView.SelectedItems[__loop])._menuID, (((_myListView)this._outLook._listView.SelectedItems[__loop])._tag));
                    break;
                }
            }
        }

        private void _getPrinter()
        {
            try
            {
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
                    string __printerName = printer; // __getPrinter["Name"].ToString();
                    bool __isDefault = false; //(__getPrinter["Default"].ToString().ToLower().Equals("true")) ? true : false;
                    settings.PrinterName = printer;
                    if (settings.IsDefaultPrinter)
                        __isDefault = true;

                    _printerListClass __listPrinter = new _printerListClass() { _printerName = __printerName, _isDefault = __isDefault };
                    if (MyLib._myGlobal._printerList.Find(__info => __info._printerName == __printerName) == null)
                    {
                        MyLib._myGlobal._printerList.Add(__listPrinter);
                    }
                }
            }
            catch
            {

                try
                {
                    ManagementObjectSearcher __printerList = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
                    foreach (ManagementObject __getPrinter in __printerList.Get())
                    {
                        string __printerName = __getPrinter["Name"].ToString();
                        bool __isDefault = (__getPrinter["Default"].ToString().ToLower().Equals("true")) ? true : false;
                        _printerListClass __listPrinter = new _printerListClass() { _printerName = __printerName, _isDefault = __isDefault };
                        if (MyLib._myGlobal._printerList.Find(__info => __info._printerName == __printerName) == null)
                        {
                            MyLib._myGlobal._printerList.Add(__listPrinter);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        void _deleteMenuButton_Click(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < this._outLook._listView.SelectedItems.Count; __loop++)
            {
                if (this._outLook._listView.SelectedItems[__loop].Selected)
                {
                    for (int __loop2 = 0; __loop2 < this._myMenu.Count; __loop2++)
                    {
                        if (((_myListView)this._outLook._listView.SelectedItems[__loop])._menuID.ToUpper().Equals(this._myMenu[__loop2]._menuID.ToUpper()))
                        {
                            this._myMenu.RemoveAt(__loop2);
                            break;
                        }
                    }
                }
            }
            this._buildMyMenu(true);
        }

        void _editGroupButton_Click(object sender, EventArgs e)
        {
            _editGroupMenuForm __form = new _editGroupMenuForm();
            StringBuilder __str = new StringBuilder();
            for (int __loop = 0; __loop < this._myMenuGroup.Count; __loop++)
            {
                __str.Append(this._myMenuGroup[__loop]._name + "\r\n");
            }
            __form._textBox.Text = __str.ToString();
            __form._saveButton.Click += (s1, e1) =>
            {
                this._myMenuGroup.Clear();
                for (int __loop = 0; __loop < __form._textBox.Lines.Length; __loop++)
                {
                    if (__form._textBox.Lines[__loop].ToString().Trim().Length > 0)
                    {
                        _myMenuGroupClass __new = new _myMenuGroupClass();
                        __new._name = __form._textBox.Lines[__loop].ToString().Trim().ToUpper();
                        this._myMenuGroup.Add(__new);
                    }
                }
                __form.Dispose();
                this._buildMyMenu(true);
            };
            __form.ShowDialog();
        }

        void _templateMainForm_Disposed(object sender, EventArgs e)
        {
            try
            {
                if (this._processCaptureScreenThread != null)
                {
                    this._processCaptureScreenThread.Abort();
                }
            }
            catch
            {
            }

            try
            {
                if (this._getPrinterWork != null)
                {
                    this._getPrinterWork.Abort();
                }
            }
            catch
            {
            }

            try
            {
                if (this._getFastReportWork != null)
                {
                    this._getFastReportWork.Abort();
                }
            }
            catch
            {
            }

            try
            {
                if (this._getReportServer != null)
                {
                    this._getReportServer.Abort();
                }
            }
            catch
            {
            }

        }

        void _tabControl_DoubleClick(object sender, EventArgs e)
        {
            _myTabControl __myTab = (_myTabControl)sender;

            _cloneForm __newForm = new _cloneForm();
            __newForm.WindowState = FormWindowState.Normal;
            __newForm.Size = new Size((int)(this.Width * 0.8), (int)(this.Height * 0.8));
            __newForm.StartPosition = FormStartPosition.CenterScreen;
            __newForm.Text = __myTab.TabPages[__myTab.SelectedIndex].Text + " (F11=Switch Full Screen ON/OFF)";
            if (__myTab.TabPages[__myTab.SelectedIndex].Controls.Count != 0)
            {
                ((Control)__myTab.TabPages[__myTab.SelectedIndex].Controls[0]).Disposed -= new EventHandler(_screen_Disposed);
                __newForm.Controls.Add(__myTab.TabPages[__myTab.SelectedIndex].Controls[0]);
                __myTab.TabPages[__myTab.SelectedIndex].Dispose();
                __newForm.Show();
            }
        }

        WebBrowser _home()
        {
            WebBrowser __result = new WebBrowser();
            __result.Dock = DockStyle.Fill;
            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccount ||
                //MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLColorStore ||
                MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLServiceCenter ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                string __config = MyLib._myGlobal._databaseConfig;
                //__config = __config.Replace(".", MyLib._myGlobal._providerCode + ".");
                MyLib._myWebserviceType __ws = (MyLib._myWebserviceType)MyLib._myGlobal._webServiceServerList[0];
                string __url = "http://www.smlsoft.com/smlreport?config=" + __config + "&database=" + MyLib._myGlobal._databaseName + "&ws=http://" + __ws._webServiceName;
                __result.Navigate(__url);
            }
            return __result;
        }

        public void _closeAllControl()
        {
            foreach (Control __control in this.Controls)
            {
                __control.Visible = false;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (MyLib._myGlobal._displayWarning(8, "") == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __myFrameWork._logoutProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._guid);
            }
            base.OnClosing(e);
        }

        void _buttonRemoveAll_Click(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < _board.Count; __loop++)
            {
                ((Control)_board[__loop]).Dispose();
            }
            _board.Clear();
        }

        void _buttonAddBoard_Click(object sender, EventArgs e)
        {
            this._contextMenuBoardStrip.Show(Control.MousePosition);
        }

        void _statusComputerInformation_MouseLeave(object sender, EventArgs e)
        {
            if (this.computerShowStatus != null)
            {
                computerShowStatus.Dispose();
            }
            this.Cursor = Cursors.Default;
        }

        void _statusComputerInformation_MouseHover(object sender, EventArgs e)
        {
            computerShowStatus = new MyLib._computerStatus();
            computerShowStatus.DesktopLocation = new Point(100, (_statusStrip.Location.Y - computerShowStatus.Height) + _statusStrip.Height);
            computerShowStatus.StartPosition = FormStartPosition.Manual;
            computerShowStatus.Show();
            this.Cursor = Cursors.Hand;
        }

        void _statusUserList_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        void _statusUserList_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        void _statusWebserviceLabel_MouseLeave(object sender, EventArgs e)
        {
            if (webServiceShowStatus != null)
            {
                webServiceShowStatus.Dispose();
            }
            this.Cursor = Cursors.Default;
        }

        void _statusWebserviceLabel_MouseHover(object sender, EventArgs e)
        {
            webServiceShowStatus = new MyLib._webServiceShowStatus();
            webServiceShowStatus.DesktopLocation = new Point(100, (_statusStrip.Location.Y - webServiceShowStatus.Height) + _statusStrip.Height);
            webServiceShowStatus.StartPosition = FormStartPosition.Manual;
            webServiceShowStatus.Show();
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// เลือก Tab
        /// </summary>
        /// <param name="tabID"></param>
        /// <returns></returns>
        public bool _selectTab(string tabID)
        {
            int __getTabNumber = this._tabControl.TabPages.IndexOfKey(tabID);
            return ((__getTabNumber == -1) ? false : true);
        }

        /// <summary>
        /// แสดง Tab
        /// </summary>
        /// <param name="screen"></param>
        private void _displayTab(Control screen)
        {
            this._tabControl.SelectedTab.Controls.Add(screen);
            screen.Dock = DockStyle.Fill;
            screen.Disposed += new EventHandler(_screen_Disposed);
            screen.Focus();
        }

        void _screen_Disposed(object sender, EventArgs e)
        {
            try
            {
                int __thisTab = this._tabControl.SelectedIndex;
                this._tabControl.SelectedTab.Dispose();
                if (__thisTab > 0)
                {
                    this._tabControl.SelectTab(--__thisTab);
                }
                sender = null;
                GC.Collect();
            }
            catch
            {
            }
        }

        /// <summary>
        /// สร้าง Tab ใหม่
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="tabName"></param>
        string _createTab(string originalID, string tabID, string tabName)
        {
            string __guid = _g.g._logMenu(1, "", originalID);
            MyLib._myTabPage __myTabPage = new MyLib._myTabPage();
            __myTabPage.Name = tabID + Guid.NewGuid().ToString();
            __myTabPage.__screenGuid = __guid;
            __myTabPage.__menuCode = originalID;
            __myTabPage.Text = tabName;
            __myTabPage.Tag = originalID;
            __myTabPage.UseVisualStyleBackColor = false;
            __myTabPage.Disposed += new EventHandler(myTabPage_Disposed);
            this._tabControl.TabPages.Add(__myTabPage);
            return __myTabPage.Name;
        }

        void myTabPage_Disposed(object sender, EventArgs e)
        {
            _g.g._logMenu(2, ((MyLib._myTabPage)sender).__screenGuid, "");
        }

        /// <summary>
        /// สร้าง Tab ใหม่ และเลือก Tab ที่สร้างใหม่
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="tabName"></param>
        /// <param name="screen"></param>
        public void _createAndSelectTab(string originalID, string tabID, string tabName, Control screen)
        {
            string __tabID = _createTab(originalID, tabID, tabName);
            this._tabControl.SelectTab(__tabID);
            //TabPage __tab = this._tabControl.SelectedTab;
            //
            _displayTab(screen);
            screen.Focus();
            _g.g._tabControl = this._tabControl;
        }

        private void _statusUserList_Click(object sender, EventArgs e)
        {
            string __tabId = "menu_user_list";
            if (_selectTab(__tabId) == false)
            {
                _createAndSelectTab(__tabId, __tabId, "User List", new MyLib._userList());
            }
        }

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public static extern bool SetSystemTime([In] ref structSystemTime st);

        public struct structSystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        private void _timerStatus_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                _timerStatus.Stop();
                if (MyLib._myGlobal._webServiceServerList.Count > 0)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(MyLib._myGlobal._webServiceServerList, MyLib._myGlobal._webServiceServer);
                    int __imageID = (__myFrameWork._statusWebserviceServer(MyLib._myGlobal._webServiceServer) == true) ? 2 : 3;
                    this._statusWebserviceLabel.Image = _imageList16x16.Images[__imageID];
                    this._statusWebserviceLabel.Text = MyLib._myGlobal._webServiceServer;
                    _statusCompressWebservice.Image = _imageList16x16.Images[(MyLib._myGlobal._compressWebservice == true) ? 0 : 1];
                    _statusCompressWebservice.Text = "Compress " + ((MyLib._myGlobal._compressWebservice == true) ? "ON" : "OFF");
                    //
                    Process __process = Process.GetCurrentProcess();
                    string __format = "{0,0:#,###.00}";
                    double __workingMemory = (double)__process.WorkingSet64;
                    this._statusProcess.Text = string.Format(__format, (double)((__workingMemory / 1024.0) / 1000.0)) + "MB";
                    _showStatus();
                    //
                    __myFrameWork = null;
                    __process.Dispose();
                    __process = null;
                    this._syncDataButton.Visible = MyLib._myGlobal._syncDataActive;
                }
                _timerStatus.Start();
            }
        }

        void _scanWebservice()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                try
                {
                    if (_g.g._companyProfile._disable_sync_time == false)
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        __myFrameWork._webserviceServerReConnect(false);
                        MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(MyLib._myGlobal._webServiceServerList, MyLib._myGlobal._webServiceServer);
                        _processStatusBar();
                        _showStatus();
                        __myFrameWork = null;
                        __myFrameWork = new _myFrameWork();
                        // ดึงวันที่จาก server database
                        CultureInfo __dateZone = new CultureInfo("en-US");
                        string __query = "select now()";
                        DataTable __getData = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
                        DateTime __getDateTime = DateTime.Parse(__getData.Rows[0][0].ToString().Split('.')[0].ToString(), __dateZone).ToUniversalTime();
                        structSystemTime __st = new structSystemTime();
                        __st.wYear = (ushort)__getDateTime.Year;
                        __st.wMonth = (ushort)__getDateTime.Month;
                        __st.wDay = (ushort)__getDateTime.Day;
                        __st.wHour = (ushort)__getDateTime.Hour;
                        __st.wMinute = (ushort)__getDateTime.Minute;
                        __st.wSecond = (ushort)__getDateTime.Second;
                        SetSystemTime(ref __st);
                        __dateZone = null;
                    }
                }
                catch
                {
                }
            }
        }

        void _processStatusBarWork()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                try
                {
                    MyLib._myWebservice __ws = new MyLib._myWebservice(MyLib._myGlobal._webServiceServer);
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    bool __getGuidConnect = __ws._sendGuid(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._guid, MyLib._myGlobal._databaseName, __myFrameWork._packTableFromUnlock());
                    if (__getGuidConnect == false)
                    {
                        if (_displayConnectFail == false)
                        {
                            _displayConnectFail = true;
                        }
                    }
                    else
                    {
                        _displayConnectFail = false;
                    }
                    __ws.Dispose();
                    __ws = null;
                    __myFrameWork = null;
                }
                catch
                {
                    if (_displayConnectFail == false)
                    {
                        _displayConnectFail = true;
                    }
                }
                // get User List
                try
                {

                    // get branch
                    string __branch_status = "";
                    try
                    {
                        if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._branch_code.Length > 0)
                        {
                            __branch_status = string.Format(" {0}({1})", _g.g._companyProfile._branch_code, _g.g._companyProfile._branch_name);
                        }
                    }
                    catch
                    {
                    }

                    MyLib._myGlobal._registerProcess();
                    this._processInfoLabel = MyLib._myGlobal._productCode + " : " + MyLib._myGlobal._productDesc;
                    if (MyLib._myGlobal._productCallCenterID.Length > 0)
                    {
                        this._processInfoLabel = this._processInfoLabel + " : Call Center Code=" + MyLib._myGlobal._productCallCenterID + __branch_status;
                    }
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataSet __userList = __myFrameWork._query(MyLib._myGlobal._mainDatabase, "select count(*) as xcount from sml_guid");
                    MyLib._myGlobal._maxUserCurrent = (int)MyLib._myGlobal._decimalPhase(__userList.Tables[0].Rows[0].ItemArray[0].ToString());
                    this._userConnected = MyLib._myGlobal._maxUserCurrent.ToString() + "/" + MyLib._myGlobal._maxUser.ToString();
                    __userList.Dispose();
                    __userList = null;
                    __myFrameWork = null;
                }
                catch (Exception __ex)
                {
                    String g = __ex.Message.ToString();
                }
            }
        }

        private void _captureScreenNow(string roworder)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                Bitmap __captureScreen = MyLib.CaptureScreen.GetDesktopImage();
                MemoryStream __ms = new MemoryStream();
                __captureScreen.Save(__ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Image __captureScreen2 = __captureScreen.GetThumbnailImage(200, 100, null, IntPtr.Zero);
                MemoryStream __msMini = new MemoryStream();
                __captureScreen2.Save(__msMini, System.Drawing.Imaging.ImageFormat.Jpeg);
                IFormatProvider __culture = new CultureInfo("en-US");
                string __dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", __culture);
                string __query = "update " + _d.sml_screen_capture._table + " set " + _d.sml_screen_capture._capture_time + "=\'" + __dateTime + "\'," + _d.sml_screen_capture._computer_name + "=\'" + MyLib._myGlobal._computerName + "\'," + _d.sml_screen_capture._user_code + "=\'" + MyLib._myGlobal._userCode + "\'," + _d.sml_screen_capture._database_code + "=\'" + MyLib._myGlobal._databaseName + "\'," + _d.sml_screen_capture._capture_screen + "=decode(\'" + Convert.ToBase64String(__ms.ToArray()) + "\',\'base64\')," + _d.sml_screen_capture._capture_screen_thumbnail + "=decode(\'" + Convert.ToBase64String(__msMini.ToArray()) + "\',\'base64\')," + _d.sml_screen_capture._request + "=2 where roworder=" + roworder;
                string __result = __myFrameWork._queryInsertOrUpdate(_myGlobal._mainDatabase, __query);
                if (__result.Length > 0)
                {
                    MessageBox.Show(__result);
                }
                __myFrameWork = null;
            }
            catch
            {
            }
        }

        // ส่ง SMS
        private void _pointCenterSync()
        {
            // slip 3 นาทีก่อน ค่อยทำ
            Thread.Sleep((1000 * 60) * 3);

            Boolean __isProcessDevice = false;
            if (_g.g._companyProfile._process_serial_device.Length > 0)
            {
                MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                string[] _dataDive = Environment.GetLogicalDrives();

                for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                {
                    string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();
                    if (__getDeviceCode.Length > 0 && _g.g._companyProfile._process_serial_device.ToLower().IndexOf(__getDeviceCode) != -1)
                    {
                        __isProcessDevice = true;
                        break;
                    }
                }
            }

            // create trigger            
            if (__isProcessDevice)
            {
                try
                {
                    MyLib._myFrameWork __ws = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                    string __query = "select * from  sml_shop_list where product_code= \'" + MyLib._myGlobal._productCode + "\' and database_name = \'" + MyLib._myGlobal._databaseName + "\' ";
                    DataSet __result = __ws._query(MyLib._myGlobal._masterPointCenterSyncName, __query);
                    if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                        string __tableSyncName = _g.d.point_center_sync._table;//  "point_center_sync";
                        StringBuilder __script = new StringBuilder();
                        string __findField = "SELECT count(*) FROM information_schema.columns WHERE table_schema='public' AND table_name='" + __tableSyncName + "' and column_name='date_time'";
                        int __countField = 0;
                        DataTable __resultFind = __myFrameWork._queryShort(__findField).Tables[0];
                        if (__resultFind.Rows.Count > 0)
                        {
                            __countField = (int)Int32.Parse(__resultFind.Rows[0][0].ToString());
                        }
                        if (__countField == 0)
                        {
                            __script.Append("ALTER TABLE " + __tableSyncName + " ADD COLUMN date_time timestamp NOT NULL DEFAULT now();");
                            string __result_add = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                        }
                        string[] __command = { "insert", "update", "delete" };
                        for (int __loop = 0; __loop < 3; __loop++)
                        {
                            // trans_flag=44
                            __script = new StringBuilder();
                            string __triggerName = "point_center_sync_" + __command[__loop] + "()";
                            __script.Append("CREATE FUNCTION " + __triggerName + " \r\n");
                            __script.Append("RETURNS trigger AS \r\n");
                            __script.Append("$BODY$ \r\n");
                            __script.Append("BEGIN \r\n");
                            switch (__loop)
                            {
                                case 0:
                                    __script.Append("IF NEW.trans_flag=44 and NEW.point_telephone <> '' THEN ");
                                    __script.Append("insert into " + __tableSyncName + " (doc_no,amount,command) values (NEW.doc_no,NEW.total_amount,2); \r\n");
                                    __script.Append("END IF;");
                                    break;
                                case 1:
                                    __script.Append("IF NEW.trans_flag=44 THEN ");
                                    __script.Append("insert into " + __tableSyncName + " (doc_no,amount,command) values (OLD.doc_no,OLD.total_amount,1); \r\n");
                                    __script.Append("IF NEW.point_telephone <> '' and NEW.last_status=0 THEN ");
                                    __script.Append("insert into " + __tableSyncName + " (doc_no,amount,command) values (NEW.doc_no,NEW.total_amount,2); \r\n");
                                    __script.Append("END IF;");
                                    __script.Append("END IF;");
                                    break;
                                case 2:
                                    __script.Append("IF OLD.trans_flag=44 THEN ");
                                    __script.Append("insert into " + __tableSyncName + " (doc_no,amount,command) values (OLD.doc_no,OLD.total_amount,1); \r\n");
                                    __script.Append("END IF;");
                                    break;
                            }
                            __script.Append("RETURN NEW; \r\n");
                            __script.Append("END; \r\n");
                            __script.Append("$BODY$ \r\n");
                            __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                            __script.Append("COST 100; \r\n");
                            __script.Append("ALTER FUNCTION " + __triggerName + " OWNER TO postgres; \r\n");
                            string __result_insert = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                            // trigger
                            __script = new StringBuilder();
                            __script.Append("CREATE TRIGGER " + __tableSyncName + "_trigger" + __command[__loop] + " \r\n");
                            __script.Append("AFTER " + __command[__loop] + " \r\n");
                            __script.Append("ON " + _g.d.ic_trans._table + " \r\n");
                            __script.Append("FOR EACH ROW \r\n");
                            __script.Append("EXECUTE PROCEDURE " + __triggerName + "; \r\n");
                            string __result_trigger = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                        }
                    }
                }
                catch
                {

                }
            }

            while (true)
            {
                try
                {


                    if (__isProcessDevice)
                    {
                        MyLib._myFrameWork __ws = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);

                        if (__ws._testConnect() == false)
                        {
                            return;
                        }

                        string __query = "select * from  sml_shop_list where product_code= \'" + MyLib._myGlobal._productCode + "\' and database_name = \'" + MyLib._myGlobal._databaseName + "\' ";
                        DataSet __result = __ws._query(MyLib._myGlobal._masterPointCenterSyncName, __query);
                        if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                        {
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            /*
                            string __tableSyncName = _g.d.point_center_sync._table;//  "point_center_sync";
                            StringBuilder __script = new StringBuilder();
                            string __findField = "SELECT count(*) FROM information_schema.columns WHERE table_schema='public' AND table_name='" + __tableSyncName + "' and column_name='date_time'";
                            int __countField = 0;
                            DataTable __resultFind = __myFrameWork._queryShort(__findField).Tables[0];
                            if (__resultFind.Rows.Count > 0)
                            {
                                __countField = (int)Int32.Parse(__resultFind.Rows[0][0].ToString());
                            }
                            if (__countField == 0)
                            {
                                __script.Append("ALTER TABLE " + __tableSyncName + " ADD COLUMN date_time timestamp NOT NULL DEFAULT now();");
                                string __result_add = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                            }
                            string[] __command = { "insert", "update", "delete" };
                            for (int __loop = 0; __loop < 3; __loop++)
                            {
                                // trans_flag=44
                                __script = new StringBuilder();
                                string __triggerName = "point_center_sync_" + __command[__loop] + "()";
                                __script.Append("CREATE FUNCTION " + __triggerName + " \r\n");
                                __script.Append("RETURNS trigger AS \r\n");
                                __script.Append("$BODY$ \r\n");
                                __script.Append("BEGIN \r\n");
                                switch (__loop)
                                {
                                    case 0:
                                        __script.Append("IF NEW.trans_flag=44 and NEW.point_telephone <> '' THEN ");
                                        __script.Append("insert into " + __tableSyncName + " (doc_no,amount,command) values (NEW.doc_no,NEW.total_amount,2); \r\n");
                                        __script.Append("END IF;");
                                        break;
                                    case 1:
                                        __script.Append("IF NEW.trans_flag=44 THEN ");
                                        __script.Append("insert into " + __tableSyncName + " (doc_no,amount,command) values (OLD.doc_no,OLD.total_amount,1); \r\n");
                                        __script.Append("IF NEW.point_telephone <> '' and NEW.last_status=0 THEN ");
                                        __script.Append("insert into " + __tableSyncName + " (doc_no,amount,command) values (NEW.doc_no,NEW.total_amount,2); \r\n");
                                        __script.Append("END IF;");
                                        __script.Append("END IF;");
                                        break;
                                    case 2:
                                        __script.Append("IF OLD.trans_flag=44 THEN ");
                                        __script.Append("insert into " + __tableSyncName + " (doc_no,amount,command) values (OLD.doc_no,OLD.total_amount,1); \r\n");
                                        __script.Append("END IF;");
                                        break;
                                }
                                __script.Append("RETURN NEW; \r\n");
                                __script.Append("END; \r\n");
                                __script.Append("$BODY$ \r\n");
                                __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                __script.Append("COST 100; \r\n");
                                __script.Append("ALTER FUNCTION " + __triggerName + " OWNER TO postgres; \r\n");
                                string __result_insert = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                // trigger
                                __script = new StringBuilder();
                                __script.Append("CREATE TRIGGER " + __tableSyncName + "_trigger" + __command[__loop] + " \r\n");
                                __script.Append("AFTER " + __command[__loop] + " \r\n");
                                __script.Append("ON " + _g.d.ic_trans._table + " \r\n");
                                __script.Append("FOR EACH ROW \r\n");
                                __script.Append("EXECUTE PROCEDURE " + __triggerName + "; \r\n");
                                string __result_trigger = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                            }

                            */
                            // เริ่มส่งข้อมูล
                            while (true)
                            {
                                string __query2 = "select roworder," + _g.d.point_center_sync._command + "," + _g.d.point_center_sync._doc_no + "," + _g.d.point_center_sync._amount + ",(select doc_date||' '||doc_time||':00' from ic_trans where ic_trans.doc_no=" + _g.d.point_center_sync._table + "." + _g.d.point_center_sync._doc_no + ") as doc_date_time ,(select point_telephone from ic_trans where ic_trans.doc_no=" + _g.d.point_center_sync._table + "." + _g.d.point_center_sync._doc_no + ") as point_telephone, (select sum_point_2 from ic_trans where ic_trans.doc_no=" + _g.d.point_center_sync._table + "." + _g.d.point_center_sync._doc_no + ") as point_amount from " + _g.d.point_center_sync._table + " order by date_time limit 1";
                                DataTable __getData = __myFrameWork._queryShort(__query2).Tables[0];
                                if (__getData.Rows.Count > 0)
                                {
                                    int __roworder = (int)Int32.Parse(__getData.Rows[0][0].ToString());
                                    int __mode = (int)Int32.Parse(__getData.Rows[0][1].ToString());
                                    string __docNo = __getData.Rows[0][2].ToString();
                                    decimal __amount = MyLib._myGlobal._decimalPhase(__getData.Rows[0][3].ToString());
                                    string __docDateTime = __getData.Rows[0][4].ToString();
                                    string __telephone = __getData.Rows[0][5].ToString().Trim();
                                    decimal __pointAmount = MyLib._myGlobal._decimalPhase(__getData.Rows[0][6].ToString());
                                    switch (__mode)
                                    {
                                        case 1:
                                            {
                                                string __query3 = "delete from trans where product_code=\'" + MyLib._myGlobal._productCode + "\' and database_name=\'" + MyLib._myGlobal._databaseName + "\' and doc_no=\'" + __docNo + "\'";
                                                string __resultQuery = __ws._queryInsertOrUpdate(MyLib._myGlobal._masterPointCenterSyncName, __query3);
                                                if (__resultQuery.Length == 0)
                                                {
                                                    string __result1 = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.point_center_sync._table + " where roworder=" + __roworder.ToString());
                                                }
                                            }
                                            break;
                                        case 2:
                                            {
                                                if (__telephone.Length > 0)
                                                {
                                                    string __query3 = "insert into trans (product_code,database_name,doc_no,amount,doc_date_time,telephone,point_amount) values (\'" + MyLib._myGlobal._productCode + "\',\'" + MyLib._myGlobal._databaseName + "\',\'" + __docNo + "\'," + __amount.ToString() + ",\'" + __docDateTime + "\',\'" + __telephone + "\'," + __pointAmount.ToString() + ")";
                                                    string __resultQuery = __ws._queryInsertOrUpdate(MyLib._myGlobal._masterPointCenterSyncName, __query3);
                                                    if (__resultQuery.Length == 0)
                                                    {
                                                        string __result1 = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.point_center_sync._table + " where roworder=" + __roworder.ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    string __result1 = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.point_center_sync._table + " where roworder=" + __roworder.ToString());
                                                }
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            // ส่ง sms
                            DataTable __getDataTelephone = __myFrameWork._queryShort("select distinct " + _g.d.point_center_sms._telephone_number + " from " + _g.d.point_center_sms._table).Tables[0];
                            for (int __loop = 0; __loop < __getDataTelephone.Rows.Count; __loop++)
                            {
                                string __phoneNumber = __getDataTelephone.Rows[0][0].ToString();
                                string __query3 = "insert into send_sms (product_code,database_name,telephone_number) values (\'" + MyLib._myGlobal._productCode + "\',\'" + MyLib._myGlobal._databaseName + "\',\'" + __phoneNumber + "\')";
                                string __resultQuery = __ws._queryInsertOrUpdate(MyLib._myGlobal._masterPointCenterSyncName, __query3);
                                if (__resultQuery.Length == 0)
                                {
                                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.point_center_sms._table + " where " + _g.d.point_center_sms._telephone_number + "=\'" + __phoneNumber.ToString() + "\'");
                                }
                            }
                        }
                    }
                    Thread.Sleep(10000);
                }
                catch
                {
                }
            }
        }

        // สั่งสินค้าผ่าน Internet
        private void _internetSync()
        {
            MyLib._myFrameWork __ws = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            while (true)
            {
                try
                {
                    Boolean __isProcessDevice = false;
                    if (_g.g._companyProfile._internetSync)
                    {
                        if (_g.g._companyProfile._process_serial_device.Length > 0)
                        {
                            MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                            string[] _dataDive = Environment.GetLogicalDrives();

                            for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                            {
                                string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();
                                if (__getDeviceCode.Length > 0 && _g.g._companyProfile._process_serial_device.ToLower().IndexOf(__getDeviceCode) != -1)
                                {
                                    __isProcessDevice = true;
                                    break;
                                }
                            }
                        }

                        if (__isProcessDevice)
                        {
                            string __query = "select * from sml_shop_list where product_code= \'" + MyLib._myGlobal._productCode + "\' and database_name = \'" + MyLib._myGlobal._databaseName + "\' ";
                            DataSet __result = __ws._query(MyLib._myGlobal._internetSyncName, __query);
                            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                            {
                                string __tableSyncName = _g.d.internet_center_sync._table; //  "internet_center_sync";
                                StringBuilder __script = new StringBuilder();
                                string __findField = "SELECT count(*) FROM information_schema.columns WHERE table_schema='public' AND table_name='" + __tableSyncName + "' and column_name='date_time'";
                                int __countField = 0;
                                DataTable __resultFind = __myFrameWork._queryShort(__findField).Tables[0];
                                if (__resultFind.Rows.Count > 0)
                                {
                                    __countField = (int)Int32.Parse(__resultFind.Rows[0][0].ToString());
                                }
                                if (__countField == 0)
                                {
                                    __script.Append("ALTER TABLE " + __tableSyncName + " ADD COLUMN date_time timestamp NOT NULL DEFAULT now();");
                                    string __result_add = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                }
                                string[] __databaseCommand = { "delete", "update", "insert" };
                                string[] __tableList = { _g.d.ic_unit._table, _g.d.ic_inventory._table, _g.d.ic_inventory_barcode._table, _g.d.ic_inventory_level._table, _g.d.images._table, _g.d.ic_inventory_bundle._table };
                                string[] __fieldKeyList = { _g.d.ic_unit._code, _g.d.ic_inventory._code, _g.d.ic_inventory_barcode._barcode, _g.d.ic_inventory_level._barcode, _g.d.images._image_id, _g.d.ic_inventory_bundle._barcode };
                                // table ic_unit (code=0)
                                // เก็บประวัติ การเพิ่ม,แก้ไข,ลบ เพื่อ Sync
                                for (int __tableLoop = 0; __tableLoop < __tableList.Length; __tableLoop++)
                                {
                                    string __tableTrigger = __tableList[__tableLoop];
                                    Boolean __updateAll = false;
                                    for (int __loopCommand = 0; __loopCommand < 3; __loopCommand++)
                                    {
                                        __script = new StringBuilder();
                                        string __triggerNameStr = "internet_center_sync_" + __tableTrigger + "_" + __databaseCommand[__loopCommand];
                                        string __triggerName = __triggerNameStr + "()";
                                        string __queryFind = "select count(*) as xcountfrom from pg_proc where proname=\'" + __triggerNameStr + "\'";
                                        DataTable __resultFind2 = __myFrameWork._queryShort(__queryFind).Tables[0];
                                        int __countFind = 0;
                                        if (__resultFind2.Rows.Count > 0)
                                        {
                                            __countFind = (int)Int32.Parse(__resultFind2.Rows[0][0].ToString());
                                        }
                                        if (__countFind == 0)
                                        {
                                            __script.Append("CREATE FUNCTION " + __triggerName + " \r\n");
                                            __script.Append("RETURNS trigger AS \r\n");
                                            __script.Append("$BODY$ \r\n");
                                            __script.Append("BEGIN \r\n");
                                            switch (__loopCommand)
                                            {
                                                case 0:
                                                    __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",0,OLD." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                    break;
                                                case 1:
                                                    switch (__tableLoop)
                                                    {
                                                        case 0: // ic_unit
                                                        case 1: // ic_inventory
                                                            {
                                                                __script.Append("if (OLD.CODE <> NEW.CODE or OLD.NAME_1 <> NEW.NAME_1 or OLD.NAME_2 <> NEW.NAME_2 or OLD.NAME_ENG_1 <> NEW.NAME_ENG_1 or OLD.NAME_ENG_2 <> NEW.NAME_ENG_2) THEN \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",1,OLD." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",2,NEW." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("END IF; \r\n");
                                                            }
                                                            break;
                                                        case 2: // ic_inventory_barcode
                                                            {
                                                                __script.Append("if (OLD.BARCODE <> NEW.BARCODE or OLD.IC_CODE <> NEW.IC_CODE or OLD.DESCRIPTION <> NEW.DESCRIPTION or OLD.UNIT_CODE <> NEW.UNIT_CODE or OLD.PRICE <> NEW.PRICE or OLD.PRICE_MEMBER <> NEW.PRICE_MEMBER) THEN \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",1,OLD." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",2,NEW." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("END IF; \r\n");
                                                            }
                                                            break;
                                                        case 3: // ic_inventory_level
                                                            {
                                                                __script.Append("if (OLD.BARCODE <> NEW.BARCODE or OLD.IC_CODE <> NEW.IC_CODE or OLD.UNIT_CODE <> NEW.UNIT_CODE or OLD.LEVEL_1 <> NEW.LEVEL_1 or OLD.LEVEL_2 <> NEW.LEVEL_2 or OLD.LEVEL_3 <> NEW.LEVEL_3 or OLD.LEVEL_4 <> NEW.LEVEL_4 or OLD.price <> NEW.price or OLD.suggest_remark <> NEW.suggest_remark) THEN \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",1,OLD." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",2,NEW." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("END IF; \r\n");
                                                            }
                                                            break;
                                                        case 4: // images
                                                            {
                                                                __script.Append("if (OLD.IMAGE_ID <> NEW.IMAGE_ID or OLD.image_file <> NEW.image_file or OLD.image_order <> NEW.image_order) THEN \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",1,OLD." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",2,NEW." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("END IF; \r\n");
                                                            }
                                                            break;
                                                        case 5: // inventory_bundle
                                                            {
                                                                __script.Append("if (OLD.BARCODE <> NEW.BARCODE or OLD.IC_CODE <> NEW.IC_CODE or OLD.ic_code_bundle <> NEW.ic_code_bundle or OLD.UNIT_CODE <> NEW.UNIT_CODE or OLD.qty <> NEW.qty or OLD.line_number <> NEW.line_number) THEN \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",1,OLD." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",2,NEW." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                                __script.Append("END IF; \r\n");
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case 2:
                                                    __script.Append("insert into " + __tableSyncName + " (table_number,command_code,code) values (" + __tableLoop.ToString() + ",3,NEW." + __fieldKeyList[__tableLoop] + "); \r\n");
                                                    break;
                                            }
                                            __script.Append("RETURN NEW; \r\n");
                                            __script.Append("END; \r\n");
                                            __script.Append("$BODY$ \r\n");
                                            __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                            __script.Append("COST 100; \r\n");
                                            __script.Append("ALTER FUNCTION " + __triggerName + " OWNER TO postgres; \r\n");
                                            string __result_insert = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                            if (__result_insert.Length == 0)
                                            {
                                                // trigger
                                                __script = new StringBuilder();
                                                __script.Append("CREATE TRIGGER " + __tableTrigger + "_trigger" + __databaseCommand[__loopCommand] + " \r\n");
                                                __script.Append("AFTER " + __databaseCommand[__loopCommand] + " \r\n");
                                                __script.Append("ON " + __tableTrigger + " \r\n");
                                                __script.Append("FOR EACH ROW \r\n");
                                                __script.Append("EXECUTE PROCEDURE " + __triggerName + "; \r\n");
                                                string __result_trigger = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                if (__result_trigger.Length == 0)
                                                {
                                                    __updateAll = true;
                                                }
                                                else
                                                {
                                                    MessageBox.Show(__result_trigger);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show(__result_insert);
                                            }
                                        }
                                    }
                                    if (__updateAll)
                                    {
                                        switch (__tableLoop)
                                        {
                                            case 0: // ic_unit
                                            case 1: // ic_inventory
                                                // insert into internet_center_sync (table_number,command_code,code) (select 0,3,code from ic_unit)
                                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + __tableSyncName + " (table_number,command_code,code)  (select " + __tableLoop.ToString() + ",3," + __fieldKeyList[__tableLoop] + " from " + __tableTrigger + ")");
                                                break;
                                            case 2: // ic_inventory_barcode
                                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + __tableSyncName + " (table_number,command_code,code)  (select " + __tableLoop.ToString() + ",3," + __fieldKeyList[__tableLoop] + " from " + __tableTrigger + ")");
                                                break;
                                            case 3: // ic_inventory_level
                                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + __tableSyncName + " (table_number,command_code,code)  (select " + __tableLoop.ToString() + ",3," + __fieldKeyList[__tableLoop] + " from " + __tableTrigger + ")");
                                                break;
                                            case 4: // images
                                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + __tableSyncName + " (table_number,command_code,code)  (select " + __tableLoop.ToString() + ",3," + __fieldKeyList[__tableLoop] + " from " + __tableTrigger + ")");
                                                break;
                                            case 5: // inventory_budle
                                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + __tableSyncName + " (table_number,command_code,code)  (select " + __tableLoop.ToString() + ",3," + __fieldKeyList[__tableLoop] + " from " + __tableTrigger + ")");
                                                break;
                                        }
                                    }
                                }
                                // รับรูปภาพ จาก Server
                                {
                                    try
                                    {
                                        string __queryServer = "select image_id,image_order,guid_code from images_upload where product_code= \'" + MyLib._myGlobal._productCode + "\' and database_code = \'" + MyLib._myGlobal._databaseName + "\' ";
                                        DataSet __resultServer = __ws._query(MyLib._myGlobal._internetSyncName, __queryServer);
                                        if (__resultServer.Tables.Count > 0 && __resultServer.Tables[0].Rows.Count > 0)
                                        {
                                            DataTable __resultTable = __resultServer.Tables[0];
                                            for (int __row = 0; __row < __resultServer.Tables[0].Rows.Count; __row++)
                                            {
                                                string __code = __resultTable.Rows[__row][0].ToString();
                                                string __imageOrder = __resultTable.Rows[__row][1].ToString();
                                                string __guid = __resultTable.Rows[__row][2].ToString();

                                                string __queryDelete = "delete from " + _g.d.images._table + " where " + _g.d.images._image_id + "=\'" + __code + "\' and " + _g.d.images._image_order + "=" + __imageOrder;
                                                string __resultDelete = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __queryDelete);

                                                int __pictureWidth = 800;
                                                string __qureyImage = "select " + _g.d.images._image_file + " from images_upload where guid_code=\'" + __guid + "\'";
                                                byte[] __databyte = new byte[1024];
                                                __databyte = __ws._ImageByte(this._imageDatabaseName, __qureyImage);
                                                MemoryStream __ms = new MemoryStream(__databyte);
                                                ArrayList __xlist = new ArrayList();
                                                //Image __returnImage = Image.FromStream(__ms);
                                                using (var __original = Image.FromStream(__ms))
                                                using (Image __resized = _g.g._resizeImage(__original, __pictureWidth, (int)((__pictureWidth) * 3 / 4)))
                                                {
                                                    //__resized.Save("c:\\test\\" + __code+__loop.ToString() + ".jpg");
                                                    string __xxSystem = System.Guid.NewGuid().ToString();
                                                    // bool _xcheckedImage = false;
                                                    if (__resized != null)
                                                    {
                                                        _g.g.BMPXMLSerialization __bmpx = new _g.g.BMPXMLSerialization(new Bitmap(__resized));
                                                        byte[] __getPictureData = __bmpx.BMPBytes;
                                                        _g.g._typeImage = new MyLib.SMLJAVAWS.imageType();
                                                        _g.g._typeImage._databyteImage = __getPictureData;
                                                        _g.g._typeImage._code = __xxSystem;
                                                        __xlist.Add(_g.g._typeImage);
                                                        //if (_xcheckedImage) getpic.Image = null;
                                                        _g.g._listType = ((MyLib.SMLJAVAWS.imageType[])__xlist.ToArray(typeof(MyLib.SMLJAVAWS.imageType)));
                                                    }
                                                }
                                                string[] __feildList = { "image_id", "image_file", "guid_code" };//insert
                                                string xwhere = "image_id";//update
                                                string xTable = "images";
                                                string insertorupdate = "0";
                                                string _guid_code = __guid;
                                                string xswheredata = __guid;
                                                string __x_result = __myFrameWork._SaveImageList(_guid_code, MyLib._myGlobal._databaseName, _g.g._listType, insertorupdate, __feildList, xTable, xwhere, xswheredata);
                                                // update กลับ
                                                string __query5 = "update images set image_order=" + __imageOrder + "," + _g.d.images._image_id + "=\'" + __code + "\' where image_id=\'" + __guid + "\'";
                                                string __resultQuery2 = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query5);
                                                // ลบข้อมูลบน server
                                                __queryDelete = "delete from images_upload where " + _g.d.images._image_id + "=\'" + __code + "\' and " + _g.d.images._image_order + "=" + __imageOrder;
                                                __resultDelete = __ws._queryInsertOrUpdate(this._imageDatabaseName, __queryDelete);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }

                                // sync edit from web 
                                {
                                    // step 
                                    // 1. get from manage_inventory where is_change = 1
                                    // 2. check code if exists then update else insert
                                    // 3. update value
                                    while (true)
                                    {
                                        string __getChangeInventoryQuery = "select roworder, code, name_th, name_en, name_lao from manage_inventory where is_change = 1 and product_code = \'" + MyLib._myGlobal._productCode + "\' and database_code = \'" + MyLib._myGlobal._databaseName + "\' order by date_time limit 1 ";
                                        DataSet __getChangeDataSet = __ws._query(MyLib._myGlobal._internetSyncName, __getChangeInventoryQuery);
                                        if (__getChangeDataSet.Tables.Count > 0)
                                        {
                                            DataTable __getChangeDataTable = __getChangeDataSet.Tables[0];

                                            if (__getChangeDataTable.Rows.Count > 0)
                                            {
                                                int __roworder = (int)Int32.Parse(__getChangeDataTable.Rows[0][0].ToString());
                                                string __code = __getChangeDataTable.Rows[0][1].ToString();
                                                string __name_th = __getChangeDataTable.Rows[0][2].ToString();
                                                string __name_en = __getChangeDataTable.Rows[0][3].ToString();
                                                string __name_lao = __getChangeDataTable.Rows[0][4].ToString();

                                                // check exists ic_inventory.code
                                                bool __isExists = false;

                                                string __checkInventoryQuery = "select code, name_1 from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __code.ToUpper() + "\'";
                                                DataTable __checkInventoryDataTable = __myFrameWork._queryShort(__checkInventoryQuery).Tables[0];
                                                if (__checkInventoryDataTable.Rows.Count > 0)
                                                {
                                                    __isExists = true;
                                                }

                                                // process insert or update
                                                StringBuilder __insertOrUpdateQuery = new StringBuilder();
                                                __insertOrUpdateQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                                if (__isExists == false)
                                                {
                                                    // insert
                                                    __insertOrUpdateQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory._table + " (" + _g.d.ic_inventory._code + ", " + _g.d.ic_inventory._name_1 + ", " + _g.d.ic_inventory._name_eng_1 + ") values(\'" + __code.ToUpper() + "\', \'" + __name_th + "\', \'" + __name_en + "\') "));
                                                }
                                                else
                                                {
                                                    // update
                                                    __insertOrUpdateQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + _g.d.ic_inventory._name_1 + " = \'" + __name_th + "\', " + _g.d.ic_inventory._name_eng_1 + " = \'" + __name_en + "\' where " + _g.d.ic_inventory._code + " = \'" + __code.ToUpper() + "\' "));
                                                }

                                                __insertOrUpdateQuery.Append("</node>");

                                                // process query
                                                string __processInventoryResult = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __insertOrUpdateQuery.ToString());

                                                // if process success 
                                                // update manage_inventory set is_change = 0 where roworder = __roworder
                                                if (__processInventoryResult.Length == 0)
                                                {
                                                    string __resultUpdateIsChange = __ws._queryInsertOrUpdate(MyLib._myGlobal._internetSyncName, "update manage_inventory set is_change = 0 where roworder = " + __roworder);
                                                }

                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }


                                // เริ่มส่งข้อมูล
                                while (true)
                                {
                                    try
                                    {
                                        string __query2 = "select roworder," + _g.d.internet_center_sync._table_number + "," + _g.d.internet_center_sync._command_code + "," + _g.d.internet_center_sync._code + " from " + _g.d.internet_center_sync._table + " order by " + "date_time," + _g.d.internet_center_sync._command_code + " limit 1";
                                        DataSet __getDataSet = __myFrameWork._queryShort(__query2);
                                        if (__getDataSet.Tables.Count > 0)
                                        {
                                            DataTable __getData = __getDataSet.Tables[0];
                                            if (__getData.Rows.Count > 0)
                                            {
                                                int __roworder = (int)Int32.Parse(__getData.Rows[0][0].ToString());
                                                int __tableNumber = (int)Int32.Parse(__getData.Rows[0][1].ToString());
                                                int __command = (int)Int32.Parse(__getData.Rows[0][2].ToString());
                                                string __code = __getData.Rows[0][3].ToString();
                                                StringBuilder __myQuery = new StringBuilder();
                                                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                                switch (__tableNumber)
                                                {
                                                    case 0: // ic_unit
                                                        {
                                                            // delete first
                                                            string __query3 = "delete from inventory_unit where product_code=\'" + MyLib._myGlobal._productCode + "\' and database_code=\'" + MyLib._myGlobal._databaseName + "\' and code=\'" + __code + "\'";
                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                            switch (__command)
                                                            {
                                                                case 2:
                                                                case 3:
                                                                    {
                                                                        string __query4 = "select " + _g.d.ic_unit._name_1 + "," + _g.d.ic_unit._name_2 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + "= \'" + __code + "\'";
                                                                        DataTable __getData4 = __myFrameWork._queryShort(__query4).Tables[0];
                                                                        if (__getData4.Rows.Count > 0)
                                                                        {
                                                                            string __name_1 = __getData4.Rows[0][0].ToString();
                                                                            string __name_2 = __getData4.Rows[0][1].ToString();
                                                                            // insert
                                                                            __query3 = "insert into inventory_unit (product_code,database_code,code,name_1,name_2) values (\'" + MyLib._myGlobal._productCode + "\',\'" + MyLib._myGlobal._databaseName + "\',\'" + __code + "\',\'" + __name_1 + "\',\'" + __name_2 + "\')";
                                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                                        }
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                        break;
                                                    case 1: // ic_inventory
                                                        {
                                                            // delete first
                                                            string __query3 = "delete from inventory where product_code=\'" + MyLib._myGlobal._productCode + "\' and database_code=\'" + MyLib._myGlobal._databaseName + "\' and code=\'" + __code + "\'";
                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                            switch (__command)
                                                            {
                                                                case 2:
                                                                case 3:
                                                                    {
                                                                        string __query4 = "select " + _g.d.ic_inventory._name_1 + "," + _g.d.ic_inventory._name_2 + "," + _g.d.ic_inventory._drink_type + "," + _g.d.ic_inventory._name_eng_1 + "," + _g.d.ic_inventory._name_eng_2 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "= \'" + __code + "\'";
                                                                        DataTable __getData4 = __myFrameWork._queryShort(__query4).Tables[0];
                                                                        if (__getData4.Rows.Count > 0)
                                                                        {
                                                                            string __name_1 = __getData4.Rows[0][0].ToString();
                                                                            string __name_2 = __getData4.Rows[0][1].ToString();
                                                                            string __drink_type = __getData4.Rows[0][2].ToString();
                                                                            string __name_eng_1 = __getData4.Rows[0][3].ToString();
                                                                            string __name_eng_2 = __getData4.Rows[0][4].ToString();
                                                                            // insert (update)
                                                                            __query3 = "insert into inventory (product_code,database_code,code,name_1,name_2,drink_type,name_eng_1,name_eng_2) values (\'" + MyLib._myGlobal._productCode + "\',\'" + MyLib._myGlobal._databaseName + "\',\'" + __code + "\',\'" + MyLib._myGlobal._convertStrToQuery(__name_1) + "\',\'" + MyLib._myGlobal._convertStrToQuery(__name_2) + "\'," + __drink_type + ",\'" + MyLib._myGlobal._convertStrToQuery(__name_eng_1) + "\',\'" + MyLib._myGlobal._convertStrToQuery(__name_eng_2) + "\')";
                                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                                        }
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                        break;
                                                    case 2: // ic_inventory_barcode
                                                        {
                                                            // delete first
                                                            string __query3 = "delete from inventory_barcode where product_code=\'" + MyLib._myGlobal._productCode + "\' and database_code=\'" + MyLib._myGlobal._databaseName + "\' and barcode=\'" + __code + "\'";
                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                            switch (__command)
                                                            {
                                                                case 2:
                                                                case 3:
                                                                    {
                                                                        string __query4 = "select " + _g.d.ic_inventory_barcode._ic_code + "," + _g.d.ic_inventory_barcode._unit_code + "," + _g.d.ic_inventory_barcode._price + "," + _g.d.ic_inventory_barcode._price_member + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "= \'" + __code + "\'";
                                                                        DataTable __getData4 = __myFrameWork._queryShort(__query4).Tables[0];
                                                                        if (__getData4.Rows.Count > 0)
                                                                        {
                                                                            string __itemCode = __getData4.Rows[0][0].ToString();
                                                                            string __unitCode = __getData4.Rows[0][1].ToString();
                                                                            decimal __price_1 = MyLib._myGlobal._decimalPhase(__getData4.Rows[0][2].ToString());
                                                                            decimal __price_2 = MyLib._myGlobal._decimalPhase(__getData4.Rows[0][3].ToString());
                                                                            // insert (update)
                                                                            __query3 = "insert into inventory_barcode (product_code,database_code,barcode,item_code,unit_code,price_1,price_2) values (\'" + MyLib._myGlobal._productCode + "\',\'" + MyLib._myGlobal._databaseName + "\',\'" + __code + "\',\'" + __itemCode + "\',\'" + __unitCode + "\'," + __price_1.ToString() + "," + __price_2.ToString() + ")";
                                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                                        }
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                        break;
                                                    case 3: // ic_inventory_level
                                                        {
                                                            // delete first
                                                            string __query3 = "delete from inventory_level where product_code=\'" + MyLib._myGlobal._productCode + "\' and database_code=\'" + MyLib._myGlobal._databaseName + "\' and barcode=\'" + __code + "\'";
                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                            switch (__command)
                                                            {
                                                                case 2:
                                                                case 3:
                                                                    {
                                                                        string __query4 = "select " + _g.d.ic_inventory_level._ic_code + "," + _g.d.ic_inventory_level._unit_code + "," + _g.d.ic_inventory_level._price + "," + _g.d.ic_inventory_level._level_1 + "," + _g.d.ic_inventory_level._level_2 + "," + _g.d.ic_inventory_level._level_3 + "," + _g.d.ic_inventory_level._level_4 + "," + _g.d.ic_inventory_level._suggest_remark + " from " + _g.d.ic_inventory_level._table + " where " + _g.d.ic_inventory_level._barcode + "= \'" + __code + "\'";
                                                                        DataTable __getData4 = __myFrameWork._queryShort(__query4).Tables[0];
                                                                        if (__getData4.Rows.Count > 0)
                                                                        {
                                                                            int __count = 0;
                                                                            string __itemCode = __getData4.Rows[0][__count++].ToString();
                                                                            string __unitCode = __getData4.Rows[0][__count++].ToString();
                                                                            decimal __price = MyLib._myGlobal._decimalPhase(__getData4.Rows[0][__count++].ToString());
                                                                            string __level_1 = __getData4.Rows[0][__count++].ToString();
                                                                            string __level_2 = __getData4.Rows[0][__count++].ToString();
                                                                            string __level_3 = __getData4.Rows[0][__count++].ToString();
                                                                            string __level_4 = __getData4.Rows[0][__count++].ToString();
                                                                            string __suggest_remark = __getData4.Rows[0][__count++].ToString();
                                                                            // insert (update)
                                                                            __query3 = "insert into inventory_level (product_code,database_code,barcode,ic_code,unit_code,price,level_1,level_2,level_3,level_4,suggest_remark) values (\'" + MyLib._myGlobal._productCode + "\',\'" + MyLib._myGlobal._databaseName + "\',\'" + __code + "\',\'" + __itemCode + "\',\'" + __unitCode + "\'," + __price.ToString() + ",\'" + __level_1.ToString() + "\',\'" + __level_2.ToString() + "\',\'" + __level_3.ToString() + "\',\'" + __level_4.ToString() + "\',\'" + __suggest_remark.ToString() + "\')";
                                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                                        }
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                        break;
                                                    case 4: // images
                                                        {
                                                            // delete first
                                                            string __guid = Guid.NewGuid().ToString("N");
                                                            string __query4 = "select " + _g.d.images._guid_code + "," + _g.d.images._image_order + " from " + _g.d.images._table + " where " + _g.d.images._image_id + "=\'" + __code + "\' order by " + _g.d.images._image_order;
                                                            DataTable __getData4 = __myFrameWork._queryShort(__query4).Tables[0];
                                                            for (int __orderLoop = 0; __orderLoop < __getData4.Rows.Count; __orderLoop++)
                                                            {
                                                                string __imageGuid = __getData4.Rows[__orderLoop][0].ToString();
                                                                string __imageOrder = __getData4.Rows[__orderLoop][1].ToString();
                                                                string __query3 = "delete from images where product_code=\'" + MyLib._myGlobal._productCode + "\' and database_code=\'" + MyLib._myGlobal._databaseName + "\' and image_id=\'" + __code + "\' and image_order=" + __imageOrder;
                                                                string __resultQuery1 = __ws._queryInsertOrUpdate(this._imageDatabaseName, __query3);
                                                                switch (__command)
                                                                {
                                                                    case 2:
                                                                    case 3:
                                                                        {
                                                                            int __pictureWidth = 800;
                                                                            string __qurey = "select " + _g.d.images._image_file + " from " + _g.d.images._table + " where " + _g.d.images._image_id + "=\'" + __code + "\' and " + _g.d.images._image_order + "=" + __imageOrder.ToString();
                                                                            byte[] __databyte = new byte[1024];
                                                                            __databyte = __myFrameWork._ImageByte(MyLib._myGlobal._databaseName, __qurey);
                                                                            MemoryStream __ms = new MemoryStream(__databyte);
                                                                            ArrayList __xlist = new ArrayList();
                                                                            //Image __returnImage = Image.FromStream(__ms);
                                                                            using (var __original = Image.FromStream(__ms))
                                                                            using (Image __resized = _g.g._resizeImage(__original, __pictureWidth, (int)((__pictureWidth) * 3 / 4)))
                                                                            {
                                                                                //__resized.Save("c:\\test\\" + __code+__loop.ToString() + ".jpg");
                                                                                string __xxSystem = System.Guid.NewGuid().ToString();
                                                                                // bool _xcheckedImage = false;
                                                                                if (__resized != null)
                                                                                {
                                                                                    _g.g.BMPXMLSerialization __bmpx = new _g.g.BMPXMLSerialization(new Bitmap(__resized));
                                                                                    byte[] __getPictureData = __bmpx.BMPBytes;
                                                                                    _g.g._typeImage = new MyLib.SMLJAVAWS.imageType();
                                                                                    _g.g._typeImage._databyteImage = __getPictureData;
                                                                                    _g.g._typeImage._code = __xxSystem;
                                                                                    __xlist.Add(_g.g._typeImage);
                                                                                    //if (_xcheckedImage) getpic.Image = null;
                                                                                    _g.g._listType = ((MyLib.SMLJAVAWS.imageType[])__xlist.ToArray(typeof(MyLib.SMLJAVAWS.imageType)));
                                                                                }
                                                                            }
                                                                            string[] __feildList = { "image_id", "image_file", "guid_code" };//insert
                                                                            string xwhere = "image_id";//update
                                                                            string xTable = "images";
                                                                            string insertorupdate = "0";
                                                                            string _guid_code = __guid;
                                                                            string xswheredata = __guid;
                                                                            string __x_result = __ws._SaveImageList(_guid_code, this._imageDatabaseName, _g.g._listType, insertorupdate, __feildList, xTable, xwhere, xswheredata);
                                                                            // update กลับ
                                                                            string __query5 = "update images set image_order=" + __imageOrder + ",product_code=\'" + MyLib._myGlobal._productCode + "\',database_code=\'" + MyLib._myGlobal._databaseName + "\',image_id=\'" + __code + "\' where image_id=\'" + __guid + "\'";
                                                                            string __resultQuery2 = __ws._queryInsertOrUpdate(this._imageDatabaseName, __query5);
                                                                        }
                                                                        break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case 5: // ic_inventory_bundle
                                                        {
                                                            // delete first
                                                            string __query3 = "delete from inventory_bundle where product_code=\'" + MyLib._myGlobal._productCode + "\' and database_code=\'" + MyLib._myGlobal._databaseName + "\' and barcode=\'" + __code + "\'";
                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                            switch (__command)
                                                            {
                                                                case 2:
                                                                case 3:
                                                                    {
                                                                        string __query4 = "select " + _g.d.ic_inventory_bundle._ic_code + "," + _g.d.ic_inventory_bundle._barcode + "," + _g.d.ic_inventory_bundle._unit_code + "," + _g.d.ic_inventory_bundle._line_number + "," + _g.d.ic_inventory_bundle._qty + "," + _g.d.ic_inventory_bundle._ic_code_bundle + " from " + _g.d.ic_inventory_bundle._table + " where " + _g.d.ic_inventory_bundle._barcode + "= \'" + __code + "\'";
                                                                        DataTable __getData4 = __myFrameWork._queryShort(__query4).Tables[0];
                                                                        if (__getData4.Rows.Count > 0)
                                                                        {
                                                                            string __itemCode = __getData4.Rows[0][0].ToString();
                                                                            string __barcode = __getData4.Rows[0][1].ToString();
                                                                            string __unitCode = __getData4.Rows[0][2].ToString();
                                                                            int __lineNumber = (int)MyLib._myGlobal._decimalPhase(__getData4.Rows[0][3].ToString());
                                                                            decimal __qty = MyLib._myGlobal._decimalPhase(__getData4.Rows[0][4].ToString());
                                                                            string __codeBundle = __getData4.Rows[0][5].ToString();
                                                                            // insert (update)
                                                                            __query3 = "insert into inventory_bundle (product_code,database_code,ic_code,line_number,ic_code_bundle,unit_code,qty,barcode) values (\'" + MyLib._myGlobal._productCode + "\',\'" + MyLib._myGlobal._databaseName + "\',\'" + __itemCode + "\'," + __lineNumber.ToString() + ",\'" + __codeBundle + "\',\'" + __unitCode + "\'," + __qty.ToString() + ",\'" + __code + "\')";
                                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query3));
                                                                        }
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                        break;
                                                }
                                                __myQuery.Append("</node>");
                                                string __resultQuery = __ws._queryList(MyLib._myGlobal._internetSyncName, __myQuery.ToString());
                                                if (__resultQuery.Length == 0)
                                                {
                                                    string __result1 = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.internet_center_sync._table + " where roworder=" + __roworder.ToString());
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(10000);
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message);
                }
            }
        }

        private void _captureScreen()
        {
            int __count = 0;
            while (true)
            {
                try
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    {
                        DataTable __capture = __myFrameWork._query(_myGlobal._mainDatabase, "select roworder," + _d.sml_screen_capture._request + " from " + _d.sml_screen_capture._table + " where " + _d.sml_screen_capture._guid_code + "=\'" + MyLib._myGlobal._guid + "\' and " + _d.sml_screen_capture._request + "=1").Tables[0];
                        if (__capture.Rows.Count > 0)
                        {
                            string __roworder = __capture.Rows[0]["roworder"].ToString();
                            this._captureScreenNow(__roworder);
                        }
                    }
                    {
                        if (++__count == 5)
                        {
                            string __query = "select roworder from " + _d.sml_screen_realtime._table + " where " + _d.sml_screen_realtime._computer_name + "=\'" + MyLib._myGlobal._computerName + "\' and " + _d.sml_screen_realtime._request_time + " is not null and now()-" + _d.sml_screen_realtime._request_time + "<\'00:00:30\'";
                            DataTable __capture = __myFrameWork._query(_myGlobal._mainDatabase, __query).Tables[0];
                            if (__capture.Rows.Count > 0)
                            {
                                string __rowOrder = __capture.Rows[0]["roworder"].ToString();
                                Bitmap __captureScreen = MyLib.CaptureScreen.GetDesktopImage();
                                Image __captureScreen2 = __captureScreen.GetThumbnailImage(200, 100, null, IntPtr.Zero);
                                MemoryStream __msMini = new MemoryStream();
                                __captureScreen2.Save(__msMini, System.Drawing.Imaging.ImageFormat.Jpeg);
                                IFormatProvider __culture = new CultureInfo("en-US");
                                string __dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", __culture);
                                string __queryUpdate = "update " + _d.sml_screen_realtime._table + " set " + _d.sml_screen_realtime._screen_thumbnail + "=decode(\'" + Convert.ToBase64String(__msMini.ToArray()) + "\',\'base64\')," + _d.sml_screen_realtime._update_time + "=\'" + __dateTime + "\' where roworder=" + __rowOrder;
                                __myFrameWork._queryInsertOrUpdate(_myGlobal._mainDatabase, __queryUpdate);
                            }
                            __count = 0;
                        }
                    }
                    Thread.Sleep(10000);
                }
                catch
                {
                }
            }
        }

        protected void _processStatusBar()
        {
            try
            {
                if (this._processStatusBarThread != null)
                {
                    this._processStatusBarThread.Abort();
                }
            }
            catch
            {
            }
            this._processStatusBarThread = new Thread(new ThreadStart(this._processStatusBarWork));
            this._processStatusBarThread.IsBackground = true;
            this._processStatusBarThread.Start();
        }

        protected void _showStatus()
        {
            this._statusUserLabel.Text = MyLib._myGlobal._userName + " (" + MyLib._myGlobal._userCode + ")";
            this._statusDatabaseName.Text = MyLib._myGlobal._databaseName;
            this._statusWebserviceLabel.Text = (MyLib._myGlobal._serviceConnected) ? MyLib._myGlobal._webServiceServer : "OFF LINE";
            if (MyLib._myGlobal._serviceConnected == false)
            {
                this._statusWebserviceLabel.Image = _imageList16x16.Images[3];
                this._statusStrip.Invalidate();
            };
            // this._statusComputerInformation.Text = MyLib._myGlobal._computerName;
            this._statusUserList.Text = this._userConnected;
            this._registerInfoLabel.Text = this._processInfoLabel;
            this._statusStrip.Invalidate();
        }

        private void _timerFindWebservice_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                try
                {
                    if (this._myFindWebserviceThread != null)
                    {
                        this._myFindWebserviceThread.Abort();
                    }
                }
                catch
                {
                }
                this._timerFindWebservice.Stop();
                this._myFindWebserviceThread = new Thread(new ThreadStart(this._scanWebservice));
                this._myFindWebserviceThread.IsBackground = true;
                this._myFindWebserviceThread.Start();
                this._timerFindWebservice.Start();
            }
        }

        public void mainMenuChangeResource(string beginNumber, int runNumber, TreeNode getNode)
        {
            getNode.Text = String.Format("{0}{1}.{2}", beginNumber, (runNumber + 1), ((this._getResourceMenu) ? MyLib._myResource._findResource(getNode.Name, getNode.Text)._str : getNode.Text));
            beginNumber = String.Format("{0}{1}.", beginNumber, (runNumber + 1));
            //__menuXml.Append(getNode.Text+"\n");
            for (int __loop = 0; __loop < getNode.Nodes.Count; __loop++)
            {
                getNode.ForeColor = Color.Blue;
                mainMenuChangeResource(beginNumber, __loop, getNode.Nodes[__loop]);
            }
        }

        void _startup()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                try
                {
                    // ตรวจสอบค่าเริ่มต้นที่จำเป็นสำหรับระบบ

                    // slow point
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    if (MyLib._myGlobal._isUserTest)
                    {
                        MyLib._myGlobal._writeLogFile(@"C:\smlsoft\smllog.txt", "start up system " + DateTime.Now.ToString(), true);
                    }

                    string result = __myFrameWork._systemStartup(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, "SML", MyLib._myGlobal._tableNameView, MyLib._myGlobal._tableNameViewColumn, MyLib._myGlobal._dataViewXmlFileName, MyLib._myGlobal._dataViewTemplateXmlFileName);

                    if (MyLib._myGlobal._isUserTest)
                    {
                        MyLib._myGlobal._writeLogFile(@"C:\smlsoft\smllog.txt", "end system start up" + DateTime.Now.ToString(), false);
                    }

                    if (result.Length != 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("มีข้อผิดพลาดในการตรวจสอบระบบ") + "\n" + result, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //
                    // Load Resource
                    try
                    {
                        string __xmlFile = "smllanguage.xml";
                        _myWebservice __ws = new _myWebservice(MyLib._myGlobal._webServiceServer);
                        StreamReader __source = MyLib._getStream._getDataStream(__xmlFile);
                        //string __getXml = __source.ReadToEnd();
                        //MyLib._myGlobal._resourceFromXml.
                        DataSet __ds = new DataSet();
                        //__ds.ReadXml(__getXml);
                        __ds.ReadXml(__source);
                        DataTable __dt = __ds.Tables[1];
                        string __thaiLang = "";
                        string __englishLang = "";
                        string __chineseLang = "";
                        string __malayLang = "";
                        string __indiaLang = "";
                        string __laoLang = "";
                        foreach (DataRow __row in __dt.Rows)
                        {
                            string __fieldName = __row.ItemArray[0].ToString();
                            string __value = __row.ItemArray[1].ToString();
                            if (__fieldName.Equals("thai_lang")) __thaiLang = (__value == null) ? "" : __value;
                            else
                                if (__fieldName.Equals("english_lang")) __englishLang = (__value == null) ? "" : __value;
                            else
                                    if (__fieldName.Equals("chinese_lang")) __chineseLang = (__value == null) ? "" : __value;
                            else
                                        if (__fieldName.Equals("malay_lang")) __malayLang = (__value == null) ? "" : __value;
                            else
                                            if (__fieldName.Equals("india_lang")) __indiaLang = (__value == null) ? "" : __value;
                            else
                                                if (__fieldName.Equals("lao_lang"))
                            {
                                __laoLang = (__value == null) ? "" : __value;
                                // จบ Row ให้ add เข้า array
                                MyLib._myGlobal._resourceFromXml.Add(new _languageClass("", __thaiLang, __englishLang, __malayLang, __chineseLang, __indiaLang, __laoLang));
                            }
                        }
                        MyLib._myGlobal._resourceFromXml.Sort(delegate (_languageClass __resource1, _languageClass __resource2) { return __resource1._thai.CompareTo(__resource2._thai); });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    if (MyLib._myGlobal._isUserTest)
                    {
                        MyLib._myGlobal._writeLogFile(@"C:\smlsoft\smllog.txt", "end system start up" + DateTime.Now.ToString(), false);
                    }

                    //ถ้าเป็น Payroll ไม่เช็คค่าเริ่มต้น
                    if (this._programName.Equals("SML Payroll") || this._programName.Equals("DTS Client Download"))
                    {
                    }
                    else
                    {
                        _g.g._companyProfileLoad();
                    }
                    __myFrameWork = null;
                }
                catch
                {
                }
            }
        }

        void menu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MyLib._myToolStripMenuItem __getControl = (MyLib._myToolStripMenuItem)sender;
                DialogResult __getResult = MessageBox.Show(this._menuBar, "[" + __getControl.ToString() + "] : " + MyLib._myGlobal._resource("ต้องการเพิ่ม Menu นี้เข้าไปไว้ใน Menu ส่วนตัวหรือไม่"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (__getResult == DialogResult.Yes)
                {
                    string __groupID = "";
                    if (this._myMenuGroup.Count > 0)
                    {
                        _seletMenuGroupForm __select = new _seletMenuGroupForm();
                        for (int __loop = 0; __loop < this._myMenuGroup.Count; __loop++)
                        {
                            __select._listBox.Items.Add(this._myMenuGroup[__loop]._name);
                        }
                        __select._listBox.SelectedIndexChanged += (s1, e1) =>
                        {
                            try
                            {
                                __groupID = __select._listBox.SelectedItem.ToString();
                            }
                            catch
                            {
                            }
                            __select.Dispose();
                        };
                        __select.ShowDialog();
                    }
                    _myMenuClass _newMenu = new _myMenuClass();
                    _newMenu._menuID = __getControl.Name;
                    _newMenu._groupID = __groupID;
                    _newMenu._tag = __getControl.Tag.ToString();
                    this._myMenu.Add(_newMenu);
                    this._buildMyMenu(true);
                }
            }
        }

        public void _buildMyMenu(Boolean updateDatabase)
        {
            //
            this._outLook._listView.Groups.Clear();
            this._outLook._listView.Groups.Add(new ListViewGroup("Default", "Default"));
            for (int __loop = 0; __loop < this._myMenuGroup.Count; __loop++)
            {
                ListViewGroup __group = new ListViewGroup();
                __group.Name = this._myMenuGroup[__loop]._name;
                __group.Header = this._myMenuGroup[__loop]._name;
                this._outLook._listView.Groups.Add(__group);
            }
            //
            this._outLook._listView.Clear();
            for (int __loop = 0; __loop < this._myMenu.Count; __loop++)
            {
                ListViewGroup __group = this._outLook._listView.Groups[0];
                for (int __find = 0; __find < this._outLook._listView.Groups.Count; __find++)
                {
                    if (this._myMenu[__loop]._groupID.ToUpper().Equals(this._outLook._listView.Groups[__find].Name.ToUpper()))
                    {
                        __group = this._outLook._listView.Groups[__find];
                        break;
                    }
                }
                _myListView __newView = new _myListView();
                __newView._menuID = this._myMenu[__loop]._menuID;
                __newView.Group = __group;
                __newView._tag = this._myMenu[__loop]._tag;
                __newView.Text = MyLib._myResource._findResource(__newView._menuID, this._myMenu[__loop]._menuName, false)._str;
                __newView.ImageIndex = 0;
                this._outLook._listView.Items.Add(__newView);
            }
            if (updateDatabase)
            {
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                StringBuilder __queryUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from mymenugroup where user_code=\'" + MyLib._myGlobal._userCode + "\'"));
                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from mymenulist where user_code=\'" + MyLib._myGlobal._userCode + "\'"));
                for (int __loop = 0; __loop < this._myMenuGroup.Count; __loop++)
                {
                    __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into mymenugroup (user_code,group_code) values (\'" + MyLib._myGlobal._userCode + "\',\'" + this._myMenuGroup[__loop]._name + "\')"));
                }
                for (int __loop = 0; __loop < this._myMenu.Count; __loop++)
                {
                    __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into mymenulist (user_code,group_code,menu_code,menu_name,menu_tag) values (\'" + MyLib._myGlobal._userCode + "\',\'" + this._myMenu[__loop]._groupID + "\',\'" + this._myMenu[__loop]._menuID + "\',\'" + this._myMenu[__loop]._menuName + "\',\'" + this._myMenu[__loop]._tag + "\')"));
                }
                __queryUpdate.Append("</node>");
                __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryUpdate.ToString());
            }
        }

        public void _loadMyMenu()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                StringBuilder __queryUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from mymenugroup where user_code=\'" + MyLib._myGlobal._userCode + "\'"));
                __queryUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from mymenulist where user_code=\'" + MyLib._myGlobal._userCode + "\'"));
                __queryUpdate.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryUpdate.ToString());
                //
                this._myMenuGroup.Clear();
                DataTable __dt1 = ((DataSet)__getData[0]).Tables[0];
                DataTable __dt2 = ((DataSet)__getData[1]).Tables[0];
                for (int __row = 0; __row < __dt1.Rows.Count; __row++)
                {
                    _myMenuGroupClass __new = new _myMenuGroupClass();
                    __new._name = __dt1.Rows[__row]["group_code"].ToString();
                    this._myMenuGroup.Add(__new);
                }
                //
                this._myMenu.Clear();
                for (int __row = 0; __row < __dt2.Rows.Count; __row++)
                {
                    _myMenuClass __new = new _myMenuClass();
                    __new._groupID = __dt2.Rows[__row]["group_code"].ToString();
                    __new._menuID = __dt2.Rows[__row]["menu_code"].ToString();
                    __new._menuName = __dt2.Rows[__row]["menu_name"].ToString();
                    __new._tag = __dt2.Rows[__row]["menu_tag"].ToString();
                    this._myMenu.Add(__new);
                }
                this._buildMyMenu(false);
            }
            catch
            {
            }
        }

        public virtual void _activeMenu(string mainMenuId, string menuName, string tag)
        {
        }

        void menu_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            MyLib._myToolStripMenuItem __getMenu = (MyLib._myToolStripMenuItem)sender;
            string __menuTag = (__getMenu.Tag == null) ? "" : __getMenu.Tag.ToString();
            if (__getMenu._next == null)
            {
                // หาหัว Menu
                string _mainMeniId = "";
                if (__getMenu != null)
                {
                    MyLib._myToolStripMenuItem __findMainId = __getMenu;
                    do
                    {
                        _mainMeniId = __findMainId._menuName;
                        __findMainId = __findMainId._parent;
                    } while (__findMainId != null);
                }
                _activeMenu(_mainMeniId, __getMenu._menuName.ToLower(), __menuTag);
            }
        }

        private void _activeMenuTree(TreeNode myNode)
        {
            if (myNode.Parent != null && myNode.GetNodeCount(true) == 0)
            {
                _activeMenu(myNode.Parent.Name, myNode.Name.ToLower(), (myNode.Tag == null) ? "" : myNode.Tag.ToString());
            }
        }

        protected void _mainMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _activeMenuTree(e.Node);
        }

        /*StringBuilder _menuNotFound= new StringBuilder();
        StringBuilder _menuName = new StringBuilder();
        StringBuilder _menuID = new StringBuilder ();
        string _menuCompare = ",menu_setup,menu_setup_erp,menu_company_profile,menu_option,menu_setup_branch,menu_setup_department_list,menu_setup_staff,menu_setup_user_group,menu_setup_staff_pic,menu_setup_doc_group,menu_setup_doc_format,menu_setup_transport,menu_setup_ar_logistic_area,menu_setup_province_auto,menu_setup_province,menu_setup_ampher,menu_setup_tambon,menu_setup_ic,menu_setup_ic_group_main,menu_setup_ic_group_sub,menu_setup_ic_brand,menu_setup_ic_model,menu_setup_ic_color,menu_setup_ic_size,menu_setup_ic_pattern,menu_setup_ic_grade,menu_setup_ic_category,menu_setup_ic_design,menu_setup_ic_dimension_name,menu_setup_ic_dimension,menu_income_list,menu_setup_ic_class,menu_setup_ic_unit,menu_setup_ic_wh,menu_setup_ic_shelf,menu_setup_ic_account_group,menu_setup_ic_import_duty_point,menu_setup_ic_issue_type,menu_setup_stk_adjust,menu_setup_ic_close_reason,ic_auto_add_warehouse_and_location,Node1111,menu_setup_other_income,menu_setup_ar_sale_area,Node5,menu_expenses_list,menu_setup_ap,menu_setup_ap_type,menu_setup_ap_group_main,menu_setup_ap_group_sub,menu_setup_ap_dimension,menu_setup_ar,menu_setup_ar_type,menu_setup_ar_group_main,menu_setup_ar_group_sub,menu_setup_ar_dimension,menu_credit,menu_setup_ar_credit_approve,menu_setup_ar_pay_bill_reason,menu_setup_ar_keep_chq_reason,menu_setup_ar_keep_money,menu_setup_doc_approve,menu_cash_bank,menu_setup_bank,menu_setup_bank_branch,menu_book_bank_type,menu_book_bank_trans_type,menu_income_list,menu_expenses_list,menu_setup_credit_type,menu_petty_cash,menu_setup_asset,menu_asset_setup_type,menu_asset_setup_location,menu_asset_setup_maintain,menu_asset_setup_maintain_unit,menu_asset_setup_retire,menu_setup_gl,menu_account_period,menu_setup_gl_journal_book,menu_setup_gl_account_type,menu_setup_gl_account_group,menu_setup_gl_group_for_cash_flow,menu_tax_group,menu_tax_type,balance_sheet_design,menu_form_edit,fast_report_designer,menu_tools,menu_database,menu_view_manager,menu_verify_database,menu_shink_database,Node6,Node7,menu_change_password,Node9,Node10,menu_database_struct,menu_query,menu_change_code,menu_change_code_item,menu_view_capture_screen_realtime,menu_import_formdesign,menu_audit,menu_view_capture_screen,menu_audit_login,menu_audit_menu,menu_audit_trans,menu_import_data_master,menu_import_report,menu_ic,menu_ic_item_menu,menu_ic_item_detail,menu_ic_item_detail_barcode,menu_ic_item_detail_2,menu_ic_item_detail_2_barcode,menu_ic_item_barcode,menu_ic_item_picture,menu_ic_item_serial,menu_ic_item_set,menu_ic_report_cancel_1,menu_ic_detail,menu_ic_report_item_set_formula,menu_ic_report_item_barcode,menu_ic_by_license,menu_ic_purchase_price,menu_ic_purchase_premium,menu_ic_item_saleprice,menu_ic_item_saleprice_2,menu_ic_report_cancel_2,menu_ic_report_item_purchase_price,menu_ic_report_item_giveaway,menu_ic_report_item_sale_price,menu_ic_report_item_sale_price_normal,menu_ic_stk_balance,menu_ic_finish_receive,menu_ic_issue,menu_ic_return_receive,menu_ic_report_4,menu_ic_finish_receive_cancel,menu_ic_issue_cancel,menu_ic_return_receive_cancel,menu_ic_report_cancel_goods_pack,menu_ic_report_cancel_raw_material,menu_ic_report_cancel_goods_raw_meterial,menu_ic_report_goods_balance,menu_import_item_ready_by_date,menu_ic_report_item_withdraw,menu_ic_report_item_return,menu_ic_transfer_wh_out,menu_ic_stk_count,menu_ic_stk_adjust_auto,menu_ic_stk_adjust,menu_ic_report_5,menu_ic_transfer_wh_out_cancel,menu_ic_stk_adjust_over_cancel,menu_ic_stk_adjust_lost_cancel,menu_ic_report_stk_cancel,menu_ic_report_stk_cancel_over,menu_ic_report_stk_cancel_lost,menu_ic_report_item_transfer,menu_ic_report_item_count,menu_ic_report_item_adjust_increase,menu_ic_report_item_adjust_decrease,menu_ic_info,menu_ic_info_stk_balance,menu_ic_info_stk_balance_warehouse,menu_ic_info_stk_balance_shelf,menu_ic_info_stk_balance_lot,menu_ic_info_stk_movement,menu_ic_info_stk_movement_sum,menu_ic_info_stk_movement_sum_by_amount,menu_ic_info_stk_reorder,menu_ic_info_purchase_history,menu_ic_info_sale_history,menu_ic_info_product_no_movement,menu_ic_info_stk_profit,menu_ic_info_stk_profit_by_doc,menu_ic_info_stk_profit_by_ar,IC_INVENTORY_NO_SALE,menu_report_ic,menu_item_status,menu_account_special_item,menu_diff_from_count,menu_print_document_for_count_by_item,menu_implement_item,menu_span_import_item,menu_lot_item,menu_item_and_staple,menu_print_document_for_count_by_warehouse,menu_ic_report_absolute_1,menu_ic_serial_number,menu_result_item_export,menu_result_item_import,menu_item_balance,menu_item_balance_hightest,menu_item_balance_now_only_serial,menu_item_non_transfer,menu_item_transfer,menu_result_transfer_item,menu_item_transfer_standard,menu_ic_report_info_stk_profit_root,menu_ic_report_info_stk_profit_by_doc_and_discount,menu_ic_report_info_stk_profit,menu_ic_report_info_stk_profit_ic_doc,menu_ic_report_info_stk_profit_ic_cust,menu_ic_report_info_stk_profit_ic_cust_doc,menu_ic_report_info_stk_profit_doc,menu_ic_report_info_stk_profit_doc_ic,menu_ic_report_info_stk_profit_cust,menu_ic_report_info_stk_profit_cust_ic,menu_ic_report_info_stk_profit_cust_ic_doc,menu_ic_report_info_stk_profit_cust_doc,menu_ic_report_info_stk_profit_cust_doc_ic,menu_ic_recalc,menu_po,menu_purchase_requisition,menu_purchase_requisition_approval,menu_purchase_request_report,menu_cancel_purchase_requisition,menu_po_report_record_requisition_purchase,menu_po_report_requisition_purchase1,menu_po_report_requisition_cancel,menu_po_report_requisition_purchase,menu_po_purchase_order,menu_po_purchase_order_approve,menu_po_purchase_order_report,menu_po_purchase_order_cancel,menu_po_report_purchase_order,menu_po_report_purchase_order_approve,menu_po_report_purchase_order_cancel,menu_po_report_purchase_order_status,menu_po_hold_receive_by_doc,menu_po_deposit_payment_1,menu_po_deposit_return_1,menu_po_deposit_payment_1_report,menu_po_deposit_payment_1_cancel,menu_po_deposit_return_1_cancel,menu_po_report_deposit_payment,menu_po_report_deposit_payment_return,menu_po_report_deposit_payment_cancel,menu_po_report_deposit_payment_return_cancel,menu_po_report_deposit_payment_remain,menu_po_report_cut_deposit_payment,menu_po_deposit_payment_2,menu_po_deposit_return_2,menu_po_deposit_payment_2_report,menu_po_deposit_payment_2_cancel,menu_po_deposit_return_2_cancel,menu_po_report_advance_payment,menu_po_report_advance_payment_return,menu_po_report_advance_payment_cancel,menu_po_report_advance_payment_return_cancel,menu_po_report_deposit_payment_2_remain,menu_po_report_cut_deposit_payment_2,menu_po_purchase_billing,menu_po_credit_note,menu_po_addition_debt,menu_po_purchase_report,menu_po_purchase_billing_cancel,menu_po_credit_note_cancel,menu_po_addition_debt_cancel,menu_po_report_purchase,menu_po_report_credit_note,menu_po_report_add_goods,menu_po_report_purchase_cancel,menu_po_credit_note_cancel_report,menu_po_report_add_goods_cancel,menu_po_report_absolute,menu_po_recive_by_item,menu_po_report_purchase_total,menu_po_report_purchase_sum_by_tax,menu_po_report_debt_from_purchase,menu_po_report_purchase_order_sum,menu_po_report_add_goods_sum,menu_po_report_return_sum,menu_po_report_purchase_order_due,menu_po_report_analysist,menu_po_report_rank_purchase_total,menu_po_report_purchase_analyze,menu_po_report_compare_purchase_monthly,menu_so,menu_so_quotation_order,menu_so_quotation_approve,menu_so_quotation_report,menu_so_quotation_cancel,menu_so_report_quotation,menu_so_report_quotation_approve,menu_so_report_quotation_cancel,menu_so_report_quotation_stat,menu_so_inquiry_order,menu_so_inquiry_order_approve,menu_so_inquiry_order_report,menu_so_inquiry_order_cancel,menu_so_report_inquiry,menu_so_report_inquiry_approve,menu_so_report_inquiry_cancel,menu_so_report_inquiry_stat,menu_so_sale_order,menu_so_sale_order_approve,menu_so_sale_order_report,menu_so_sale_order_cancel,menu_so_report_sale_order,menu_so_report_sale_order_approve,menu_so_report_sale_order_cancel,menu_so_report_sale_order_stat,menu_so_deposit_receive_1,menu_so_deposit_return_1,menu_so_deposit_report,menu_so_deposit_receive_1_cancel,menu_so_deposit_return_1_cancel,menu_so_report_deposit_receive,menu_so_report_deposit_return,menu_so_report_deposit_payment_cancel,menu_so_report_deposit_payment_return_cancel,menu_so_report_deposit_payment_remain,menu_so_report_cut_deposit_payment,menu_so_deposit_receive_2,menu_so_deposit_return_2,menu_so_deposit_2_report,menu_so_deposit_receive_2_cancel,menu_so_deposit_return_2_cancel,menu_so_report_deposit_receive_2,menu_so_report_deposit_return_2,menu_so_report_deposit_payment_cancel_2,menu_so_report_deposit_payment_return_cancel_2,menu_so_report_deposit_payment_remain_2,menu_so_report_cut_deposit_payment_2,menu_so_invoice,menu_so_credit_note,menu_so_invoice_add,menu_so_report,menu_so_invoice_cancel,menu_so_credit_note_cancel,menu_so_invoice_add_cancel,menu_so_report_invoice,menu_so_sale_service_by_item,menu_so_report_credit_note,menu_so_report_add_goods,menu_so_report_invoice_cancel,menu_so_report_credit_note_cancel,menu_so_report_add_goods_cancel,menu_so_report_absolute,menu_sell_by_sale,menu_shipments_by_date,menu_sale_history_product,menu_so_report_analysist,menu_analysis_sell_ex_by_product,menu_analysis_sell_sum_by_docno,menu_margin_reacceptance,menu_product_rank,menu_shipments_compare_month,menu_compare_sale_with_price,menu_product_sale_loss,menu_ap,menu_ap_main,menu_ap_detail,menu_ap_detail_2,menu_ap_reference,menu_ap_picture,menu_ap_report_detail_ap,menu_ap_debt_balance,menu_ap_cn_balance,menu_ap_increase_debt,menu_ap_balance_report,menu_ap_report_early_year_balance_setup,menu_ap_report_early_year_balance_decrease,menu_ap_report_early_year_balance_increase,menu_ap_debt_other,menu_ap_cn_debt_other,menu_ap_increase_debt_other,menu_ap_other,menu_ap_cancel_debt_balance_other,menu_ap_cancel_cn_debt_other,menu_ap_cancel_increase_debt_other,menu_documents_early_year_other,menu_increase_debt_other,menu_reduction_dept_other,menu_cancel_documents_early_year_other,menu_cancel_increase_debt_other,menu_cancel_reduction_dept_other,menu_ap_pay_bill,menu_ap_debt_billing,menu_ap_report_cancel,menu_ap_cancel_pay_bill,menu_ap_cancel_debt_billing,menu_ap_report_cancel_pay_bill,menu_ap_report_cancel_debt_billing,menu_ap_report_billing,menu_billing_value_by_invoice,menu_billing_outstanding,menu_ap_report_payment,menu_payment_detail,menu_ap_report_absolute,menu_invoice_arrears_by_date,menu_invoice_due_by_date,menu_invoice_arrears_due,menu_invoice_overdue_by_date,menu_payment_by_invoice,menu_payment_by_date,menu_payment_by_department,menu_ap_analytics,menu_ap_report_payable_movement,menu_ap_report_payable_aging,menu_ap_report_payable_aging_by_doc,menu_ap_report_payable_status,menu_ap_report_payable_not_movement,menu_ap_detail_debt_by_ap,menu_ar,menu_ar_main,menu_ar_detail,menu_ar_detail_2,menu_ar_reference,menu_ar_picture,menu_ar_credit_group,menu_ar_report_detail_ar,menu_ar_debt_balance,menu_ar_increase_debt,menu_ar_cn_balance,menu_ar_balance_report,menu_ar_report_early_year_balance_setup,menu_ar_report_early_year_balance_increase,menu_ar_report_early_year_balance_decrease,menu_ar_debt_other,menu_ar_increase_debt_other,menu_ar_cn_debt_other,menu_ar_other,menu_ar_cancel_debt_balance_other,menu_ar_cancel_cn_debt_other,menu_ar_cancel_increase_debt_other,menu_ar_documents_early_year_other,menu_report_ar_increase_debt_other,menu_ar_reduction_dept_other,menu_ar_cancel_documents_early_year_other,menu_ar_cancel_increase_debt_other,menu_ar_cancel_reduction_dept_other,menu_ar_pay_bill,menu_ar_debt_billing,menu_ar_cancel,menu_ar_cancel_pay_bill,menu_ar_cancel_debt_billing,menu_ar_report_cancel_pay_billing,menu_ar_report_cancel_invoice,menu_ar_report_billing,menu_ar_report_billing_and_detail,menu_ar_report_pay_debt_trans,menu_ar_report_receipt,menu_ar_report_receipt_and_detail,menu_ar_report_absolute,menu_ar_report_invoice_remain_pay,menu_ar_report_check_balance,menu_ar_analytics,menu_ar_report_movement,menu_ar_report_period_ar_debt_remain,menu_ar_report_period_ar_debt_remain_by_doc,menu_ar_report_status_ar,menu_cash_and_bank,menu_cash_income_other,menu_cash_income_other_credit,menu_cash_income_other_debit,menu_cash_income_report,menu_cash_income_other_cancel,menu_cash_income_other_credit_cancel,menu_cash_income_other_debit_cancel,menu_cash_income_other_report,menu_cash_income_other_credit_report,menu_cash_income_other_debit_report,menu_cash_income_other_cancel_report,menu_cash_income_other_cancel_report,menu_cash_income_other_cancel_report,menu_cash_expense_other,menu_cash_expense_other_credit,menu_cash_expense_other_debit,menu_cash_expense_report,menu_cash_expense_other_cancel,menu_cash_expense_other_credit_cancel,menu_cash_expense_other_debit_cancel,menu_cash_expense_other_report,menu_cash_expense_other_credit_report,menu_cash_expense_other_debit_report,menu_cash_expense_other_cancel_report,menu_cash_expense_other_credit_cancel_report,menu_cash_expense_other_debit_cancel_report,menu_petty_cash_m,menu_cb_petty_cash_receive,menu_cb_petty_return,menu_money_report_cancel,menu_cb_petty_cash_request_cancel,menu_cb_petty_return_cancel,menu_report_cb_petty_cash_request,menu_report_cb_petty_cash_return,menu_report_cb_cancel_pettycash_request,menu_report_cb_cancel_pettycash_return,menu_pettycash_total,_menu_sub_cash_movements,menu_report_cb_pettycash_request_budget,menu_report_cb_request_testpay,menu_cb_bank,menu_bank_withdraw_payin,menu_book_bank,menu_cb_cash_payin,menu_cb_cash_withdraw,menu_report_cancel_withdraw_payin,menu_cb_cash_payin_cancle,menu_cb_cash_withdraw_cancle,menu_report_cancel_withdraw,menu_report_cancel_payin,menu_cb_report_withdraw,_menu_cash_withdrawals,menu_cb_report_transfer_bank,menu_cb_report_bank_income,menu_cb_report_bank_expanse,menu_cb_chq_in,menu_cb_chq_in_receive_master,menu_cb_chq_in_payin,menu_cb_chq_in_pass,menu_cb_chq_in_return,menu_cb_chq_in_cancel,menu_cb_chq_renew,menu_cb_chq_report_cancel,menu_cb_chq_in_payin_cancle,menu_cb_chq_in_honor_cancle,menu_cb_chq_in_return_cancle,menu_cb_chq_cancel,menu_cb_chq_in_change_cancle,menu_report_cb_chq_in_payin_cancel,menu_report_cb_chq_in_honor_cancle,menu_report_cb_chq_in_return_cancle,menu_report_cb_chq_cancel,menu_report_cb_chq_in_change_cancle,menu_cb_report_chq_in_receive,menu_report_chq_card_detail_cheque_by_date_import,menu_report_chq_card_disposit_cheque_detail,menu_cb_chq_out,menu_cb_chq_out_payment_master,menu_cb_chq_out_pass,menu_cb_chq_out_cancel,menu_cb_chq_out_renew,menu_cb_chq_out_report_cancel,menu_cb_chq_out_honor_cancle,menu_cb_chq_out_cancel_hornor_cancel,menu_cb_chq_out_change_cancle,menu_report_cb_chq_out_honor_cancel,menu_report_cb_chq_out_cancel_hornor_cancel,menu_report_cb_chq_out_change_cancle,menu_cb_report_chq_out_payment,menu_report_chq_card_enddate_pay_cheque,menu_report_chq_card_cancel_cheque_detail,menu_cb_credit_card,menu_cb_credit_card_receive,menu_cb_credit_card_pass,menu_cb_credit_card_cancel,menu_cb_report_credit_card_receive,menu_report_cb_credit_card_cancel,menu_report_cb_credit_card_receive,menu_report_chq_card_enddate_card,menu_report_chq_card_cancel_card_detail,menu_cb_report_absolute,_menu_cash_movements,_menu_cash_deposit,_menu_received_cash_sub_item,_menu_summary_payment,_menu_book_bank_balance,_menu_bank_statement,_menu_statement_advance,menu_asset,menu_as_asset_detail,menu_asset_lists,menu_asset_picture,menu_asset_maintenance,menu_asset_sale,menu_show_asset_byday,menu_show_asset_bymonth,menu_show_asset_byyear,menu_asset_transfers,menu_asset_reports,menu_asset_report_list,menu_asset_report_depreciate,menu_asset_report_maintain,menu_asset_report_sale,menu_general_ledger_main,menu_chart_of_account_main,menu_chart_of_account,menu_chart_of_account_fast,menu_chart_of_account_flow,menu_report_chart_of_account,menu_gl_main,menu_gl_journal,menu_gl_journal_fast,menu_gl_picture,menu_gl_process,menu_gl_check_data,menu_gl_journal_pass,menu_gl_journal_pass_cancel,menu_gl_show_sheet,menu_gl_show_sheet_sum,menu_gl_show_trial_balance,menu_gl_analysis_trial_balance,menu_gl_show_work_sheet,menu_gl_report_journal,menu_gl_report_journal_sum,menu_gl_report_sheet,menu_gl_report_sheet_sum,menu_gl_report_trial_balance,menu_gl_report_balance_sheet,menu_gl_report_balance,menu_gl_vat,menu_report_vat_sale,menu_report_vat_buy,menu_gl_vat_report_sum,menu_gl_vat_report_sale_inter,menu_gl_vat_report_buy_inter,menu_gl_vat_report_inter_sum,menu_gl_wht,menu_gl_wht_report_in_53,menu_gl_wht_report_in_3,menu_gl_wht_report_sum,menu_gl_wht_report_sum,gl_menu_allocate,menu_gl_allocate_all,menu_gl_report_other,menu_gl_report_slip,menu_gl_end_of_period,menu_gl_end_of_year,";
        */
        protected ToolStripItem _createMenuBar(MyLib._myToolStripMenuItem parent, TreeNode nodes, Font font)
        {
            MyLib._myToolStripMenuItem __menu = new MyLib._myToolStripMenuItem();
            __menu.Font = font;

            if (MyLib._myGlobal._isDesignMode == false)
            {
                __menu._parent = parent;
                __menu._menuName = nodes.Name;
                __menu.Name = nodes.Name;
                __menu.Text = nodes.Text;
                __menu.Tag = nodes.Tag;
                __menu.Click += new EventHandler(menu_Click);
                /* _menuName.Append(nodes.Name+",");
                 _menuID.Append((nodes.Tag == null) ? "" : nodes.Tag.ToString()).Append(",");
                 if (_menuCompare.IndexOf(","+nodes.Name+",") == -1)
                 {
                     _menuNotFound.Append(nodes.Text + ",");
                 }*/
                if (_menuImageList != null && nodes.ImageKey.Length > 0)
                {
                    __menu.Image = _menuImageList.Images[nodes.ImageKey];
                }
                if (nodes.FirstNode != null)
                {
                    TreeNode __nextMenu = nodes.FirstNode;

                    do
                    {
                        if (__nextMenu.Tag == null || (__nextMenu.Tag != null && __nextMenu.Tag.ToString().IndexOf("&hide&") != -1) == false)
                        {
                            ToolStripItem __newMenu = _createMenuBar(__menu, __nextMenu, font);
                            __menu._next = __newMenu;
                            __menu.DropDownItems.Add(__newMenu);
                            if (__nextMenu.Tag != null)
                            {
                                if (__nextMenu.Tag.ToString().IndexOf("&line&") != -1)
                                {
                                    ToolStripSeparator __line = new ToolStripSeparator();
                                    __line.Font = font;
                                    __menu.DropDownItems.Add(__line);
                                }
                                // form
                                if (__nextMenu.Tag.ToString().IndexOf("&form&") != -1)
                                {
                                    _g.g._menuFormListClass __data = new _g.g._menuFormListClass();
                                    __data._screenCode = __nextMenu.Name;
                                    __data._screenName = __nextMenu.Text;
                                    _g.g._menuFormList.Add(__data);
                                }
                            }
                        }
                        __nextMenu = __nextMenu.NextNode;
                    } while (__nextMenu != null);
                }
                if (__menu.DropDownItems.Count == 0)
                {
                    __menu.MouseDown += new MouseEventHandler(menu_MouseDown);
                }

            }
            return (__menu);
        }

        protected void _mainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _activeMenuTree(_mainMenu.SelectedNode);
            }
        }

        protected virtual void _checkTransectionProcess()
        {

        }

        //StringBuilder __menuXml = new StringBuilder();
        private void _templateMainForm_Load(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                MyLib._myGlobal._statusLabel = this._status;
                MyLib._myGlobal._statusStrip = this._statusStrip;
                try
                {
                    if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLBIllFree)
                    {
                        this.Text = _programName + " : " + MyLib._myGlobal._userCode + " " + this._versionInfo;
                        _showStatus();
                        _processStatusBar();
                        this._status.Text = "Connect web service " + MyLib._myGlobal._webServiceServer;
                        this.Invalidate();
                        this._status.Text = "";
                        this._mainMenu.__name = "MAIN";
                        this._mainMenu.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                        //__menuXml = new StringBuilder();
                        for (int loop = 0; loop < this._mainMenu.Nodes.Count; loop++)
                        {
                            mainMenuChangeResource("", loop, this._mainMenu.Nodes[loop]);
                        } // for
                        _startup();
                        // Create Menu Bar
                        this._menuBar.Items.Clear();
                        for (int loop = 0; loop < this._mainMenu.Nodes.Count; loop++)
                        {
                            this._menuBar.Items.Add(_createMenuBar(null, this._mainMenu.Nodes[loop], this._menuBar.Font));
                        }

                        this._mainMenu.NodeMouseClick += new TreeNodeMouseClickEventHandler(_mainMenu_NodeMouseClick);
                        this._mainMenu.KeyDown += new KeyEventHandler(_mainMenu_KeyDown);

                        this._dock.Dock = DockStyle.Fill;

                        // toe ยกเว้น dts ไม่ต้องโหลด home
                        this.Home.Controls.Add(this._dock);

                        DockableFormInfo __formLeft = this._dock.Add(this._outLook, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                        this._dock.DockForm(__formLeft, DockStyle.Left, zDockMode.Inner);
                        __formLeft.ShowCloseButton = false;
                        __formLeft.ShowContextMenuButton = false;
                    }
                    else
                    {
                        this._processCaptureScreenThread = new Thread(new ThreadStart(this._captureScreen));
                        this._processCaptureScreenThread.IsBackground = true;
                        this._processCaptureScreenThread.Start();
                        if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong ||
                            MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro)
                        {
                            // jead แต้มสะสมรวม 
                            this._pointCenterSyncThread = new Thread(new ThreadStart(this._pointCenterSync));
                            this._pointCenterSyncThread.IsBackground = true;
                            this._pointCenterSyncThread.Start();
                        }

                        // ขายสินค้าผ่าน Internet
                        this._internetSyncThread = new Thread(new ThreadStart(this._internetSync));
                        this._internetSyncThread.IsBackground = true;
                        this._internetSyncThread.Start();

                        // toe disable function for dts client download
                        if (this._programName.Equals("DTS Client Download") == false)
                        {
                            this._dataSyncThread = new Thread(new ThreadStart(SMLDataCenterSync._run._start));
                            this._dataSyncThread.IsBackground = true;
                            this._dataSyncThread.Start();
                        }

                        // toe thread transection check
                        this._checkTransectionThread = new Thread(new ThreadStart(_checkTransectionProcess));
                        this._checkTransectionThread.IsBackground = true;
                        this._checkTransectionThread.Start();

                        this.Text = MyLib._myGlobal._databaseName + ((MyLib._myGlobal._fixBranchName.Length > 0) ? " สาขา " + MyLib._myGlobal._fixBranchName : "") + " : " + _programName + " " + this._versionInfo;

                        _showStatus();
                        _processStatusBar();
                        // ดึง Resource ทั้งหมด จาก WebService
                        this._status.Text = "Connect web service " + MyLib._myGlobal._webServiceServer;
                        this.Invalidate();
                        this._status.Text = "";
                        this._mainMenu.__name = "MAIN";
                        this._mainMenu.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                        //__menuXml = new StringBuilder();
                        for (int loop = 0; loop < this._mainMenu.Nodes.Count; loop++)
                        {
                            mainMenuChangeResource("", loop, this._mainMenu.Nodes[loop]);
                        } // for
                        _startup();

                        // Create Menu Bar
                        this._menuBar.Items.Clear();
                        for (int loop = 0; loop < this._mainMenu.Nodes.Count; loop++)
                        {
                            _myToolStripMenuItem __menuBar = (_myToolStripMenuItem)_createMenuBar(null, this._mainMenu.Nodes[loop], this._menuBar.Font);

                            if (_g.g._companyProfile._show_menu_by_permission == true)
                            {
                                if (__menuBar.DropDown.Items.Count > 0)
                                    this._menuBar.Items.Add(__menuBar);
                            }
                            else
                                this._menuBar.Items.Add(__menuBar);
                        }
                        //
                        /*    string _a1 = _menuName.ToString();
                            string _a2 = _menuID.ToString();
                            string _a3 = _menuNotFound.ToString();*/
                        this._mainMenu.NodeMouseClick += new TreeNodeMouseClickEventHandler(_mainMenu_NodeMouseClick);
                        this._mainMenu.KeyDown += new KeyEventHandler(_mainMenu_KeyDown);

                        this._dock.Dock = DockStyle.Fill;

                        // toe ยกเว้น dts ไม่ต้องโหลด home
                        this.Home.Controls.Add(this._dock);

                        DockableFormInfo __formLeft = this._dock.Add(this._outLook, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                        this._dock.DockForm(__formLeft, DockStyle.Left, zDockMode.Inner);
                        __formLeft.ShowCloseButton = false;
                        __formLeft.ShowContextMenuButton = false;

                        // ย้ายมาจาก constructor
                        if (MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLBIllFree)
                        {
                            // check branch from serial hdd
                            StringBuilder __serialCheck = new StringBuilder();
                            if (_g.g._companyProfile._branchStatus == 1)
                            {
                                try
                                {
                                    MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                                    string[] _dataDive = Environment.GetLogicalDrives();

                                    for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                                    {
                                        string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();

                                        if (__getDeviceCode.Length > 0)
                                        {
                                            if (__serialCheck.Length > 0)
                                            {
                                                __serialCheck.Append(" or ");
                                            }
                                            __serialCheck.Append(" (upper(serial_list) like \'%" + __getDeviceCode.ToUpper() + "%\') ");
                                        }
                                    }
                                }
                                catch
                                {
                                }

                                if (__serialCheck.Length > 0)
                                {
                                    string __queryCheckBranchCode = " select code, name_1 from erp_branch_list where serial_list != '' and ( " + __serialCheck.ToString() + ") ";

                                    MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                                    DataSet __branchList = __myFrameWork._queryShort(__queryCheckBranchCode);

                                    string __getBranchCode = "";
                                    string __getBranchName = "";

                                    if (__branchList.Tables.Count > 0 && __branchList.Tables[0].Rows.Count > 0)
                                    {
                                        __getBranchCode = __branchList.Tables[0].Rows[0][_g.d.erp_branch_list._code].ToString();
                                        __getBranchName = __branchList.Tables[0].Rows[0][_g.d.erp_branch_list._name_1].ToString();

                                    }

                                    if (__getBranchCode != "")
                                    {
                                        _g.g._companyProfile._branch_code = __getBranchCode;
                                        _g.g._companyProfile._branch_name = __getBranchName;
                                        MyLib._myGlobal._branchCodeFormSerialDrive = __getBranchCode;
                                    }
                                }

                            }

                            // toe check fast report
                            bool __isProcessDevice = false;
                            if (_g.g._companyProfile._process_serial_device.Length > 0)
                            {
                                MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                                string[] _dataDive = Environment.GetLogicalDrives();


                                for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                                {
                                    string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();
                                    if (__getDeviceCode.Length > 0 && _g.g._companyProfile._process_serial_device.ToLower().IndexOf(__getDeviceCode) != -1)
                                    {
                                        __isProcessDevice = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                __isProcessDevice = true;
                            }

                            if (__isProcessDevice && MyLib._myGlobal._databaseName.Length > 0)
                            {
                                this._doImportReportWord();
                                // get master form
                                this._doImportMasterFormDesign();

                                // toe thread import report
                                if (MyLib._myGlobal._programName.Equals("SML CM") == false)
                                {
                                    this._getReportServer = new Thread(new ThreadStart(SMLFastReport._processImportReport._doImport));
                                    this._getReportServer.IsBackground = true;
                                    this._getReportServer.Start();
                                }

                            }



                        }
                        else
                        {
                            this._doImportReportWord();
                            // toe thread import report
                            this._doImportMasterFormDesign();

                            this._getReportServer = new Thread(new ThreadStart(SMLFastReport._processImportReport._doImport));
                            this._getReportServer.IsBackground = true;
                            this._getReportServer.Start();
                        }
                    }
                    //this._dock.SetWidth(__formLeft, _myGlobal._mainForm.Width / 2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("มีข้อผิดพลาดในการตรวจสอบระบบ") + "\n" + ex.Message.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this._menuPanel.Visible = false;
                this.Invalidate();
                this._createHomeMenuPanel(this._mainMenu.Nodes, 0);
            }

            //if (_g.g._companyProfile._warning_reorder_point)
            //{
            //    SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
            //    __process._checkICPurchasePointAlert();
            //}
        }

        private void _createHomeMenuPanel(TreeNodeCollection items, int level)
        {
            List<Control> __controlForDispose = new List<Control>();
            foreach (Control __control in this._homeMenu.Controls)
            {
                if (__control.GetType() == typeof(_myFlowLayoutPanel))
                {
                    _myFlowLayoutPanel __item = (_myFlowLayoutPanel)__control;
                    if (__item._level >= level)
                    {
                        __controlForDispose.Add(__item);
                    }
                }
            }
            for (int __loop = 0; __loop < __controlForDispose.Count; __loop++)
            {
                __controlForDispose[__loop].Dispose();
            }
            //
            Color __backColor = Color.Cyan;
            switch (level)
            {
                case 1: __backColor = Color.Red; break;
                case 2: __backColor = Color.Magenta; break;
                case 3: __backColor = Color.Blue; break;
                case 4: __backColor = Color.Pink; break;
                case 5: __backColor = Color.Green; break;
            }
            _myFlowLayoutPanel __flowLayout = new _myFlowLayoutPanel();
            __flowLayout._level = level;
            __flowLayout.Dock = DockStyle.Top;
            __flowLayout.AutoSize = true;
            string __imageFolder = @"./_images/";
            for (int __loop = 0; __loop < items.Count; __loop++)
            {
                TreeNode __item = items[__loop];
                MyLib.VistaButton __button = new VistaButton();
                __button.Size = new System.Drawing.Size(150, 80);
                __button.ButtonText = __item.Text;
                __button._drawNewMethod = true;
                __button.Name = __item.Name;
                __button.myTextAlign = ContentAlignment.BottomCenter;
                __button.ImageSize = new System.Drawing.Size(32, 32);
                __button.myImageAlign = ContentAlignment.TopCenter;
                __button._next = __item.Nodes;
                __button._haveNodes = (__item.Nodes.Count == 0) ? false : true;
                __button.Tag = __item.Tag;
                try
                {
                    __button.myImage = (__button._haveNodes) ? Bitmap.FromFile(__imageFolder + "folder.png") : Bitmap.FromFile(__imageFolder + "window_add.png");
                }
                catch
                {
                }

                try
                {
                    int __index = __item.Tag.ToString().IndexOf('*');
                    if (__index != -1)
                    {
                        string __tag = __item.Tag.ToString();
                        __index++;
                        StringBuilder __pictureName = new StringBuilder();
                        while (__index < __tag.Length && __tag[__index] != '*')
                        {
                            __pictureName.Append(__tag[__index]);
                            __index++;
                        }
                        __button.myImage = Bitmap.FromFile(__imageFolder + __pictureName.ToString() + ".png");
                    }
                }
                catch
                {
                }
                __button.BaseColor = __backColor;
                __button._level = level;
                __button.Click += (s1, e1) =>
                {
                    MyLib.VistaButton __sender = (MyLib.VistaButton)s1;
                    if (__sender._next.Count > 0)
                    {
                        this._createHomeMenuPanel(__sender._next, __sender._level + 1);
                    }
                    else
                    {
                        // Menu
                        //MessageBox.Show("adadsadadad");
                        this._activeMenu("list_menu", (__sender.Name != null) ? __sender.Name : "", (__sender.Tag != null) ? __sender.Tag.ToString() : "");


                    }
                };
                __flowLayout.Controls.Add(__button);
            }
            this._homeMenu.Controls.Add(__flowLayout);
            __flowLayout.BringToFront();
        }

        private void _newsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void _showLeftMenu_Click(object sender, EventArgs e)
        {
            this._menuPanel.Visible = (this._menuPanel.Visible == true) ? false : true;
            this.Invalidate();
        }

        private void _changeDate_Click(object sender, EventArgs e)
        {
            MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, "Date", new MyLib._setDate());
        }

        private void _buttonAddBoard_Click_1(object sender, EventArgs e)
        {

        }

        public void _mainMenuAcess(string beginNumber, int runNumber, TreeNode getNode)
        {
            getNode.Text = String.Format("{0}{1}.{2}", beginNumber, (runNumber + 1), MyLib._myResource._findResource(getNode.Name, getNode.Text)._str);
            beginNumber = String.Format("{0}{1}.", beginNumber, (runNumber + 1));
            for (int loop = 0; loop < getNode.Nodes.Count; loop++)
            {
                _mainMenuAcess(beginNumber, loop, getNode.Nodes[loop]);
                __MenuSubList = new MyLib._submenuListClass();
                __MenuSubList._submeid = (string)getNode.Nodes[loop].Name;
                __MenuSubList._submenuname1 = (string)getNode.Nodes[loop].Text;
                __MenuSubList._isRead = false;
                __MenuSubList._isAdd = false;
                __MenuSubList._isEdit = false;
                __MenuSubList._isDelete = false;
                __MenuList._munsubList.Add(__MenuSubList);
            }
        }

        protected string _exceptVersionTag = "";
        void _menuRemoveByVersion(TreeNodeCollection getNode)
        {
            string __tag = "";
            string __oemTag = "";
            switch (MyLib._myGlobal._isVersionEnum)
            {
                case _myGlobal._versionType.SMLAccount:
                    __tag = "&2&";
                    break;
                case _myGlobal._versionType.SMLAccountProfessional:
                    __tag = "&3&";
                    break;
                case _myGlobal._versionType.SMLPOS:
                case _myGlobal._versionType.IMSPOS:
                    __tag = "&6&";
                    break;
                case _myGlobal._versionType.SMLPOSLite:
                    __tag = "&7&";
                    break;
                case _myGlobal._versionType.SMLTomYumGoong:
                    __tag = "&8&";
                    break;
                case _myGlobal._versionType.SMLPOSMED:
                    __tag = "&9&";
                    break;
                case _myGlobal._versionType.SMLPOSStarter:
                    __tag = "&10&";
                    break;
                case _myGlobal._versionType.SMLTomYumGoongPro:
                    __tag = "&11&";
                    break;
                case _myGlobal._versionType.SMLAccountPOS:
                    __tag = "&12&";
                    break;
                case _myGlobal._versionType.SMLAccountPOSProfessional:
                    __tag = "&13&";
                    break;
            }

            // for oem
            if (MyLib._myGlobal._OEMVersion.Equals("imex") || MyLib._myGlobal._OEMVersion.Equals("ims"))
            {
                __oemTag = "&imex&";
            }
            else if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                __oemTag = "&singha&";
            }

            if (__tag.Length > 0 || MyLib._myGlobal._menuAll)
            {
                int __addr = 0;
                while (__addr < getNode.Count)
                {
                    string __menuName = getNode[__addr].Text;

                    if (MyLib._myGlobal._menuAll == false && getNode[__addr].Tag != null && getNode[__addr].Tag.ToString().Length > 0 && getNode[__addr].Tag.ToString().IndexOf(__tag) == -1 && getNode[__addr].Tag.ToString().Equals("&hide&") == false)
                    {
                        if (__oemTag.Length == 0 || getNode[__addr].Tag.ToString().ToLower().IndexOf(__oemTag) == -1 || (_exceptVersionTag.Length == 0 && getNode[__addr].Tag.ToString().ToLower().IndexOf(_exceptVersionTag) == -1))
                            getNode.RemoveAt(__addr);
                        else
                            __addr++;
                    }
                    else
                    {

                        if (getNode[__addr].Nodes.Count > 0)
                        {
                            _menuRemoveByVersion(getNode[__addr].Nodes);
                        }
                        __addr++;
                    }
                }
            }
        }

        //somruk
        MyLib._menuListClass __MenuList;
        MyLib._submenuListClass __MenuSubList;
        MyLib._mainMenuClass __MainMenu;
        public _mainMenuClass __getMenuListAll()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._menuRemoveByVersion(_mainMenu.Nodes);
                __MainMenu = new _mainMenuClass();
                for (int loop = 0; loop < _mainMenu.Nodes.Count; loop++)
                {
                    __MenuList = new _menuListClass();
                    __MenuList._menuMainname = _mainMenu.Nodes[loop].Name;
                    _mainMenuAcess("", loop, _mainMenu.Nodes[loop]);
                    __MainMenu._MainMenuList.Add(__MenuList);

                }
                //MemoryStream __stream = new MemoryStream();
                //XmlSerializer s = new XmlSerializer(typeof(_MainMenuClass));
                //s.Serialize(__stream, __MainMenu);
                //TextWriter w = new StreamWriter(@"c:\test\Menu.xml");
                //s.Serialize(w, __MainMenu);
                //w.Close();
            }
            return __MainMenu;
        }

        private void _selectLanguage_Click(object sender, EventArgs e)
        {

        }

        private void _screenSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1024, 768);
        }

        private void _misButton_Click(object sender, EventArgs e)
        {
            _createAndSelectTab("MIS", "MIS", "MIS", this._home());
        }
    }

    public class _myListView : ListViewItem
    {
        public string _menuID = "";
        public string _tag = "";
    }

    public class _myMenuGroupClass
    {
        public string _name = "";
    }

    public class _myMenuClass
    {
        public string _groupID = "";
        public string _menuID = "";
        public string _menuName = "";
        public string _tag = "";
    }

    public class _memoryCleaner
    {
        private const int PERIOD_IN_MS = 5000;

        private static int Counter_;

        private Thread thread_;
        private AutoResetEvent event_ = new AutoResetEvent(false);

        public _memoryCleaner()
        {
        }

        public void Start()
        {
            Stop();
            thread_ = new Thread(new ThreadStart(run));
            thread_.Name = string.Format("MemoryCleaner#{0}", Interlocked.Increment(ref Counter_));
            thread_.IsBackground = true; // this makes thread to be stopped when Main thread is over
            event_.Reset();
            thread_.Start();
        }

        public void Stop()
        {
            if (thread_ != null)
            {
                event_.Set();
                thread_.Join();
                thread_ = null;
            }
        }

        private void run()
        {
            while (!event_.WaitOne(PERIOD_IN_MS, false))
            {
                GC.SuppressFinalize(this);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }

    public class _cloneForm : Form
    {
        public _cloneForm()
        {
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F11)
            {
                if (this.TopMost == true)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                    this.TopMost = false;
                    return true;
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.TopMost = true;
                    return true;
                }
            }
            else if (keyData == (Keys.Control | Keys.F11)) // toe ทำ สำหรับ POS
            {
                if (this.TopMost == true)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                    this.TopMost = false;
                    return true;
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.TopMost = true;
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}