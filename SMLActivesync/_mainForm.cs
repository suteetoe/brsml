using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using MyLib;
using System.Text.RegularExpressions;
using Crom.Controls.Docking;
using System.Management;

namespace SMLActivesync
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public ArrayList _threadList = new ArrayList();
        Thread _valcummwork = null;
        //Thread _getPrinterWork = null;

        public _mainForm()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional)
            {
                this.Icon = new Icon(GetType(), "professional.ico");
            };
            this.Resize += new EventHandler(_mainForm_Resize);
            this.Load += new EventHandler(_mainForm_Load);
            this._mainMenu = this._mainMenuERP;
            this._menuImageList = this._menuImage;
            this._programName = (MyLib._myGlobal._programName.Length == 0) ? "SML ERP" : MyLib._myGlobal._programName;
            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Version : {0}  Build : {1:s}", __myVersion, __myTime);
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();
            this.Disposed += new EventHandler(_mainForm_Disposed);
            //
            // โต๋ ย้ายไปใน _templateMainForm.cs
            this._manageTableForAutoUnlock();

        }

        private void _vacuum()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                if (__myFrameWork._databaseSelectType == _myGlobal._databaseType.PostgreSql)
                {
                    Thread.Sleep((1000 * 60) * 60);
                    string __query = "VACUUM FULL ANALYZE";
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
                }
            }
            catch
            {
            }
        }

        private ArrayList _createMenuBarMain(TreeNodeCollection nodes)
        {
            ArrayList __result = new ArrayList();
            for (int __loop = 0; __loop < nodes.Count; __loop++)
            {
                __result.AddRange(_createMenuBar(nodes[__loop]));
            }
            return (__result);
        }
        private ArrayList _createMenuBar(TreeNode nodes)
        {
            ArrayList __result = new ArrayList();
            if (nodes.FirstNode != null)
            {
                TreeNode __nextMenu = nodes.FirstNode;
                do
                {
                    __result.Add(__nextMenu.Name + ":" + __nextMenu.Text + ":" + __nextMenu.Tag);
                    ArrayList __get = _createMenuBar(__nextMenu);
                    __result.AddRange(__get);
                    __nextMenu = __nextMenu.NextNode;
                } while (__nextMenu != null);
            }
            return (__result);
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

            //try
            //{
            //    this._getPrinterWork.Abort();
            //}
            //catch
            //{
            //}
        }

        void _mainForm_Load(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
            //MyLib._myUtil._showTaskBarNotifier(this, "", "Welcome to SMLERP");
            // Process
            //this._valcummwork = new Thread(new ThreadStart(_vacuum));
            //this._valcummwork.Start();

            // toe move to SMLERPTemplate
            //this._getPrinterWork = new Thread(_getPrinter);
            //this._getPrinterWork.Start();

            this._loadMyMenu();
        }


        void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
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
                    string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
                    _mainMenuCode = menuName;
                    _PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(mainMenuId, _mainMenuCode);
                    MyLib._myGlobal._mainMenuIdPassTrue = mainMenuId;
                    MyLib._myGlobal._mainMenuCodePassTrue = _mainMenuCode;
                    _g.g._companyProfileLoad();
                    //
                    switch (menuName)
                    {
                        case "menu_datacenter_branch": // กำหนดรายละเอียดสาขา
                            _createAndSelectTab(menuName, menuName, __screenName, new _branchSyncControl());
                            break;
                        case "menu_datacenter_table": // กำหนดตารางข้อมูล
                            _createAndSelectTab(menuName, menuName, __screenName, new _tableSyncControl());
                            break;
                        case "menu_datacenter_send_control": // ส่งข้อมูล
                            _createAndSelectTab(menuName, menuName, __screenName, new _reSyncSendControl());
                            break;
                        case "menu_datacenter_monitor": // สถานะการส่งข้อมูล
                            _createAndSelectTab(menuName, menuName, __screenName, new _syncStatusControl());
                            break;
                        case "menu_datacenter_receive_control" :
                            _createAndSelectTab(menuName, menuName, __screenName, new _reSyncReciveControl());
                            break;
                        case "menu_change_customer_code_master" :
                            _createAndSelectTab(menuName, menuName, __screenName, new _changeMasterCode(_changeMasterType.ลูกหนี้));
                            break;
                        case "menu_change_item_code_master" :
                            _createAndSelectTab(menuName, menuName, __screenName, new _changeMasterCode(_changeMasterType.สินค้า));
                            break;
                        case "menu_change_supplier_code_master" :
                            _createAndSelectTab(menuName, menuName, __screenName, new _changeMasterCode(_changeMasterType.เจ้าหนี้));
                            break;
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _mainMenuERP_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void _outLook_Load(object sender, EventArgs e)
        {

        }
    }
}
