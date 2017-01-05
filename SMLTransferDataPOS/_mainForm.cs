using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace SMLTransferDataPOS
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public ArrayList _threadList = new ArrayList();
        Thread _valcummwork = null;

        public _mainForm()
        {
            InitializeComponent();

            if (MyLib._myGlobal._OEMVersion == "EARTH")
            {
                this._exceptVersionTag = "&earth&";
            }

            if (MyLib._myGlobal._programName.Equals("SML CM POS Data Manage"))
            {
                this._exceptVersionTag = "&cm&";

            }


            this.Resize += new EventHandler(_mainForm_Resize);
            this.Load += new EventHandler(_mainForm_Load);
            this._mainMenu = this._menuTransfer;
            //this._menuImageList = this._menuImage;
            this._programName = (MyLib._myGlobal._programName.Length == 0) ? "SML Data Transfer" : MyLib._myGlobal._programName;
            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Version : {0}  Build : {1:s}", __myVersion, __myTime);
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();
            this.Disposed += new EventHandler(_mainForm_Disposed);
            //
            // โต๋ ย้ายไปใน _templateMainForm.cs
            this._manageTableForAutoUnlock();

        }

        public override void _activeMenu(string mainMenuId, string menuName, string tag)
        {
            try
            {
                if (MyLib._myGlobal._maxUserCurrent > MyLib._myGlobal._maxUser)
                {
                    MessageBox.Show("Limit user please wait.");
                    MyLib._myGlobal._registerProcess();

                    // โต๋ เพิ่่ม กรณี เข้า server ลูกค้าแล้ว user เต็ม
                    if (SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") == -1)
                    {
                        return;
                    }
                }
                // ตรวจสอบสิทธิ์
                MyLib._mainMenuClass __listmenu = new MyLib._mainMenuClass();
                __listmenu = MyLib._myGlobal._listMenuAll;
                bool __ischeckMainmenu = (mainMenuId.Equals(menuName)) ? true : false;
                string _mainMenuCode = "";
                // start
                if (__ischeckMainmenu == false)
                {
                    _datacenter_load();

                    string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
                    _mainMenuCode = menuName;
                    MyLib._PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(mainMenuId, _mainMenuCode);
                    MyLib._myGlobal._mainMenuIdPassTrue = mainMenuId;
                    MyLib._myGlobal._mainMenuCodePassTrue = _mainMenuCode;
                    _g.g._companyProfileLoad();
                    //
                    switch (menuName)
                    {
                        case "menu_server_config": // กำหนดรายละเอียดสาขา
                            //_createAndSelectTab(menuName, menuName, __screenName, new _branchSyncControl());
                            _mainServerSetup __serverSetup = new _mainServerSetup();
                            __serverSetup.ShowDialog();
                            break;
                        case "menu_sale_transection": // กำหนดตารางข้อมูล
                            if (MyLib._myGlobal._OEMVersion.Equals("EARTH"))
                            {
                                _createAndSelectTab(menuName, menuName, __screenName, new _earthSaleTransferUserControl());

                            }
                            else if (MyLib._myGlobal._programName.Equals("SML CM POS Data Manage"))
                            {
                                _createAndSelectTab(menuName, menuName, __screenName, new _siriPosDataTransfer());

                            }
                            else
                                _createAndSelectTab(menuName, menuName, __screenName, new _posSaleControl());
                            break;
                        case "menu_ic_transfer":
                            _createAndSelectTab(menuName, menuName, __screenName, new _icTransfer());
                            break;

                        case "menu_ic_wd_transfer":
                            _createAndSelectTab(menuName, menuName, __screenName, new _earthTransferTransectionControl(56));

                            break;

                        case "menu_ic_adjust_transfer":
                            _createAndSelectTab(menuName, menuName, __screenName, new _earthTransferTransectionControl(66));

                            break;
                        case "menu_ic_transfer_branch":
                            _createAndSelectTab(menuName, menuName, __screenName, new _earthTransferTransectionControl(72));

                            break;

                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _datacenter_load()
        {
            // load 
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.erp_data_center._table));
            __query.Append("</node>");


            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Count > 0)
            {
                DataTable __datacenterResult = ((DataSet)__result[0]).Tables[0];

                if (__datacenterResult.Rows.Count > 0)
                {
                    _global._datacenter_server = __datacenterResult.Rows[0][_g.d.erp_data_center._datacenter_server].ToString();
                    _global._datacenter_provider = __datacenterResult.Rows[0][_g.d.erp_data_center._datacenter_provider_name].ToString();
                    _global._datacenter_database_name = __datacenterResult.Rows[0][_g.d.erp_data_center._datacenter_database_name].ToString();

                    int __databaseType = MyLib._myGlobal._intPhase(__datacenterResult.Rows[0][_g.d.erp_data_center._datacenter_database_name].ToString());
                    switch (__databaseType)
                    {
                        case 0:
                            _global._datacenter_database_type = MyLib._myGlobal._databaseType.PostgreSql;
                            break;
                        case 1:
                            _global._datacenter_database_type = MyLib._myGlobal._databaseType.MySql;
                            break;
                        case 2:
                            _global._datacenter_database_type = MyLib._myGlobal._databaseType.MicrosoftSQL2000;
                            break;
                        case 3:
                            _global._datacenter_database_type = MyLib._myGlobal._databaseType.MicrosoftSQL2005;
                            break;
                        case 4:
                            _global._datacenter_database_type = MyLib._myGlobal._databaseType.Oracle;
                            break;
                        case 5:
                            _global._datacenter_database_type = MyLib._myGlobal._databaseType.Firebird;
                            break;
                    }

                }
            }
        }

        void _mainForm_Disposed(object sender, EventArgs e)
        {
            try
            {
                for (int __loop = 0; __loop < this._threadList.Count; __loop++)
                {
                    try
                    {
                        Thread __getThread = (Thread)this._threadList[__loop];
                        if (__getThread != null && __getThread.IsAlive)
                        {
                            __getThread.Abort();
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
            try
            {
                this._valcummwork.Abort();
            }
            catch
            {
            }
        }

        void _mainForm_Load(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
            this._loadMyMenu();
        }

        void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
        }
    }
}
