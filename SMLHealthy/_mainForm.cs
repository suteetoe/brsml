using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using MyLib;

namespace SMLHealthy
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public ArrayList _threadList = new ArrayList();
        Thread _valcummwork = null;

        public _mainForm()
        {
            InitializeComponent();
            this.Resize += new EventHandler(_mainForm_Resize);
            this.Load += new EventHandler(_mainForm_Load);
            this._mainMenu = this._mainMenuSMLHealthy;
            this._menuImageList = this._imageList16x16;
            this._programName = (MyLib._myGlobal._programName.Length == 0) ? "SML ERP" : MyLib._myGlobal._programName;
            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Version : {0}  Build : {1:s}", __myVersion, __myTime);
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();
            this.Disposed += new EventHandler(_mainForm_Disposed);
            //
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_branch_list._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.gl_chart_of_account._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.gl_journal._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_inventory._table);
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
          
        }

        private void _vacuum()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                if (__myFrameWork._databaseSelectType == _myGlobal._databaseType.PostgreSql)
                {
                    Thread.Sleep(1000 * 60);
                    string __query = "VACUUM FULL ANALYZE";
                    //__myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);
                    //__myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
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
        }

        void _mainForm_Load(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
            //MyLib._myUtil._showTaskBarNotifier(this, "", "Welcome to SMLERP");
            // Process
            for (int __loop = 0; __loop < 3; __loop++)
            {
                Thread __process = new Thread(new ThreadStart(_process));
                __process.Name = "SML Thread " + __loop.ToString();
                __process.IsBackground = true;
                __process.Start();
                this._threadList.Add(__process);
            }
            //
            //this._valcummwork = new Thread(new ThreadStart(_vacuum));
            //this._valcummwork.Start();
            this._loadMyMenu();
        }

        /// <summary>
        /// ประมวลผลทุกหนึ่งวินาที
        /// </summary>
        private void _process()
        {
            //while (true)
            //{
            //    try
            //    {
            //        SMLProcess._sendProcess._procesNow();
            //    }
            //    catch
            //    {
            //    }
            //    Thread.Sleep(10000);
            //}
        }


        void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
        }

        void _selectMenuFlow(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPControl._flow._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }
        void _selectMenuConfig(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPConfig._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }
        void _selectMenuHealthyConfig(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLHealthyConfig._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }
      
        void _selectMenuARHealthy(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLHealthyControl._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }
        public override void _activeMenu(string mainMenuId, string menuName, string tag)
        {
            if (MyLib._myGlobal._maxUserCurrent > MyLib._myGlobal._maxUser)
            {
                MessageBox.Show("Limit user please wait.");
                MyLib._myGlobal._registerProcess();
                return;
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
                if (__permission._isRead)
                {
                    if (tag.IndexOf("&ar_healthy&") != -1)
                    {
                        _selectMenuARHealthy(menuName, __screenName);
                    }
                    else
                        if (tag.IndexOf("&healthyconfig&") != -1)
                        {
                            _selectMenuHealthyConfig(menuName, __screenName);

                        }
                        else
                            if (tag.IndexOf("&healthy&") != -1)
                            {
                                // SMLFastReport._designer __fastReport = new SMLFastReport._designer();
                                // __fastReport._load(menuName);
                                // _createAndSelectTab(menuName, menuName, menuName, __fastReport);
                            }
                            else
                            {
                                bool __selectTabFound = _selectTab(menuName);
                                if (__selectTabFound == false)
                                {
                                    Control __getControl = (new _g._selectMenu())._getObject(menuName, __screenName, _programName);
                                    if (__getControl != null)
                                    {
                                        _createAndSelectTab(menuName, menuName, __screenName, __getControl);
                                    }
                                }
                            }
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถเข้าเมนูนี้ได้") + __screenName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
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
