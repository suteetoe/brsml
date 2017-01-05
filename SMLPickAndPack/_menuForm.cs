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

namespace SMLPickAndPack
{
    public partial class _menuForm : SMLERPTemplate._templateMainForm
    {
        public ArrayList _threadList = new ArrayList();
        Thread _valcummwork = null;
        //Thread _getPrinterWork = null;

        public _menuForm()
        {
            InitializeComponent();
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
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_wms_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_income_list._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_expenses_list._table);

            /* ----------
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_purchase_request._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_so_deposit_money._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_so_credit_note._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_so_inquiry._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_so_invoice._table);
            //cash & bank
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_bank_income._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_bank_income_detail._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_cash_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_chq_list._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_chq_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_chq_trans_detail._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_credit_card._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_credit_card_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_credit_card_trans_detail._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_other_payment._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_petty_cash._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_petty_cash_trans._table);
            */
            /*ArrayList __main = _createMenuBarMain(this._mainMenuERP.Nodes);
            ArrayList __compare = _createMenuBarMain(this._compare.Nodes);
            StringBuilder __str = new StringBuilder();
            for (int __loop1 = 0; __loop1 < __compare.Count; __loop1++)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __main.Count; __loop2++)
                {
                    if (__compare[__loop1].ToString().Equals(__main[__loop2].ToString()))
                    {
                        __found = true;
                    }
                }
                if (__found == false)
                {
                    __str.Append(__compare[__loop1].ToString() + ",");
                }
            }
            string __xx = __str.ToString();
            string __cc = __xx;*/
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

        //private void _getPrinter()
        //{
        //    try
        //    {
        //        ManagementObjectSearcher __printerList = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
        //        foreach (ManagementObject __getPrinter in __printerList.Get())
        //        {
        //            string __printerName = __getPrinter["Name"].ToString();
        //            bool __isDefault = (__getPrinter["Default"].ToString().ToLower().Equals("true")) ? true : false;

        //            MyLib._myGlobal._printerList.Add(new _printerListClass() { _printerName = __printerName, _isDefault = __isDefault });
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

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

            // toe move to SMLERPTemplate
            //this._getPrinterWork = new Thread(_getPrinter);
            //this._getPrinterWork.Start();

            this._loadMyMenu();
        }

        /// <summary>
        /// ประมวลผลทุกหนึ่งวินาที
        /// </summary>
        private void _process()
        {
            while (true)
            {
                try
                {
                    SMLProcess._sendProcess._procesNow();
                }
                catch
                {
                }
                Thread.Sleep(10000);
            }
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

        void _selectMenuAR(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPAR._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuICInfo(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPICInfo._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuIC(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPIC._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuReportIC(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPICReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuPickAndPack(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = null;
                switch (menuName.ToLower())
                {
                    case "menu_setup_pp_machine":
                        __getControl = (Control)new _setupMachineUserControl();
                        break;
                    case "menu_pick_active":
                        __getControl = (Control)new _pick._mainControl();
                        break;
                    case "menu_pick_plan":
                        __getControl = (Control)new _plan._planControl();
                        break;
                }
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
                    if (tag.IndexOf("&fastreport&") != -1)
                    {
                        SMLFastReport._designer __fastReport = new SMLFastReport._designer();
                        __fastReport._load(menuName);
                        _createAndSelectTab(menuName, menuName, menuName, __fastReport);
                    }
                    else
                        if (menuName.Equals("fast_report_designer"))
                        {
                            _createAndSelectTab(menuName, menuName, menuName, new SMLFastReport._designer());
                        }
                        else
                            if (menuName.Equals("menu_form_edit"))
                            {
                                _createAndSelectTab(menuName, menuName, menuName, new SMLReport._formReport._formDesigner());
                            }
                            else
                                if (menuName.Equals("menu_import_formdesign"))
                                {
                                    _createAndSelectTab(menuName, menuName, menuName, new SMLReport._ImportFormDesign());
                                }
                                else if (menuName.Equals("menu_import_report"))
                                {
                                    _createAndSelectTab(menuName, menuName, menuName, new SMLFastReport._importFastReport());
                                }
                                else
                                    if (tag.IndexOf("&pp&") != -1)
                                    {
                                        _selectMenuPickAndPack(menuName, __screenName);
                                    }
                                    else
                                        if (tag.IndexOf("&flow&") != -1)
                                        {
                                            _selectMenuFlow(menuName, __screenName);
                                        }
                                        else
                                            if (tag.IndexOf("&config&") != -1)
                                            {
                                                _selectMenuConfig(menuName, __screenName);
                                            }
                                            else
                                                if (tag.IndexOf("&ar&") != -1)
                                                {
                                                    _selectMenuAR(menuName, __screenName);
                                                }
                                                else
                                                    if (tag.IndexOf("&ic&") != -1)
                                                    {
                                                        _selectMenuIC(menuName, __screenName);
                                                    }
                                                    else
                                                        if (tag.IndexOf("&report&") != -1)
                                                        {
                                                            _selectMenuReport(menuName, __screenName);
                                                        }
                                                        else if (tag.IndexOf("&reportic&") != -1)
                                                        {
                                                            _selectMenuReportIC(menuName, __screenName);
                                                        }
                                                        else
                                                            if (tag.IndexOf("&icinfo&") != -1)
                                                            {
                                                                _selectMenuICInfo(menuName, __screenName);
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
