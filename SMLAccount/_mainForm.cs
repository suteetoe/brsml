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
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace SMLAccount
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public ArrayList _threadList = new ArrayList();
        Thread _valcummwork = null;
        Thread _dlSoundThread = null;
        Thread _sendApproveCreditThread = null;
        //Thread _getPrinterWork = null;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                if (SystemInformation.ComputerName.ToLower().IndexOf("jead") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("fon") != -1)
                {
                    Control __getControl = SMLERPGL._selectMenu._getObject("menu_gl_report_design", "ออกแบบงบการเงิน");
                    if (__getControl != null)
                    {
                        _createAndSelectTab("menu_gl_report_design", "menu_gl_report_design", "ออกแบบงบการเงิน", __getControl);
                        MyLib._myGlobal._mainForm.WindowState = FormWindowState.Normal;
                        MyLib._myGlobal._mainForm.Location = new Point(1800, 200);
                        MyLib._myGlobal._mainForm.Size = new Size(1366, 768);
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _openAccountPeriod()
        {
            MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, "", new SMLERPConfig._accountPeriod._accountPeriod());
        }

        private void _checkPeriod()
        {
            if (_g.g._accountPeriodDateBegin.Count == 0)
            {
                DialogResult messageResult = MessageBox.Show(MyLib._myGlobal._mainForm, "ยังไม่ได้กำหนดงวดบัญชี\nต้องการกำหนดหรือไม่ (แนะนำให้กำหนดก่อน)", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (messageResult == DialogResult.Yes)
                {
                    _openAccountPeriod();
                    _g.g._accountPeriodGet();
                }
            }

            // toe fix query กำหนดปีบัญชีอัตโนมัติ 
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update erp_account_period set period_year = ( extract(year from date_start) + 543 ) where period_year is null ");
        }

        private void _sendRequestArCredit()
        {
            if (_g.g._companyProfile._request_ar_credit)
            {
                _sendApproveCreditThread = new Thread(new ThreadStart(SMLERPControl._arInfoProcess._sendRequestARCredit));
                this._sendApproveCreditThread.IsBackground = true;
                this._sendApproveCreditThread.Start();

                //SMLERPControl._arInfoProcess __process = new SMLERPControl._arInfoProcess._sendRequestARCredit();
                //__process._sendRequestARCredit();
            }
        }

        protected override void _checkTransectionProcess()
        {
            // start process
            SMLERPICInfo._icTransectionCheck._startProcess();

            // toe update doc_no_guid = '' เมื่อผ่านไปแล้ว 10 วัน

            MyLib._myFrameWork __fw = new _myFrameWork();
            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountPOSProfessional)
            {
                string _updateDocGuid = "update ic_trans set doc_no_guid = '' where doc_date < \'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now.AddDays(-7)) + "\' and doc_no_guid <> '' and doc_no_guid is not null";
                __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, _updateDocGuid);

            }

            __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.barcode_import_list._table + " where " + _g.d.barcode_import_list._doc_date + " < \'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now.AddDays(-60)) + "\' ");
            __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.barcode_import_list_detail._table + " where " + _g.d.barcode_import_list_detail._doc_date + " < \'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now.AddDays(-60)) + "\' ");


        }

        protected override string _getResourceMenuID(string code, string defaultName)
        {
            switch (code)
            {
                case "menu_setup_ic_group_main": return MyLib._myGlobal._resource("ประเภทของธุรกิจ");
                case "menu_setup_ic_group_sub": return MyLib._myGlobal._resource("ประเภทหลักของผลิตภัณฑ์");
                case "menu_setup_ic_brand": return MyLib._myGlobal._resource("กลุ่มผลิตภัณฑ์");
                case "menu_setup_ic_model": return MyLib._myGlobal._resource("Fermentation");
                case "menu_setup_ic_pattern": return MyLib._myGlobal._resource("ตราสินค้า");
                case "menu_setup_ic_grade": return MyLib._myGlobal._resource("กลุ่มสินค้าย่อย/รสชาติ");
                case "menu_setup_ic_design": return MyLib._myGlobal._resource("หน่วยนับ");
                default:
                    return base._getResourceMenuID(code, defaultName);
            }
        }
        public _mainForm()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional && MyLib._myGlobal._OEMVersion.Length == 0)
            {
                this.Icon = new Icon(GetType(), "professional.ico");
            };

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccount && MyLib._myGlobal._programName.Equals("Sea And Hill Account"))
            {
                this.Icon = new Icon(GetType(), "SeaAndHillAccount_2.ico");
            }

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountPOS && MyLib._myGlobal._programName.Equals("Sea And Hill Account POS"))
            {
                this.Icon = new Icon(GetType(), "seaandhill_accountpos.ico");
            }


            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountProfessional && MyLib._myGlobal._OEMVersion.Equals("imex"))
            {
                this.Icon = new Icon(GetType(), "Arrow.ico");
            }

            // singha hide menu check permission can read
            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                _hideMenuCannotAccess();

                // change resource
                /*
                MyLib._myResource._updateResource("menu_setup_ic_group_main", MyLib._myGlobal._resource("ประเภทของธุรกิจ"));
                MyLib._myResource._updateResource("menu_setup_ic_group_sub", MyLib._myGlobal._resource("ประเภทหลักของผลิตภัณฑ์"));
                MyLib._myResource._updateResource("menu_setup_ic_brand", MyLib._myGlobal._resource("กลุ่มผลิตภัณฑ์"));
                MyLib._myResource._updateResource("menu_setup_ic_model", MyLib._myGlobal._resource("Fermentation"));
                MyLib._myResource._updateResource("menu_setup_ic_pattern", MyLib._myGlobal._resource("ตราสินค้า"));
                MyLib._myResource._updateResource("menu_setup_ic_grade", MyLib._myGlobal._resource("กลุ่มสินค้าย่อย/รสชาติ"));
                MyLib._myResource._updateResource("menu_setup_ic_design", MyLib._myGlobal._resource("หน่วยนับ"));
                */

                /*
                MyLib._myResource._updateResource("ic_inventory.item_brand", MyLib._myGlobal._resource("กลุ่มผลิตภัณฑ์"));
                MyLib._myResource._updateResource("ic_inventory.item_design", MyLib._myGlobal._resource("หน่วยนับ"));
                MyLib._myResource._updateResource("ic_inventory.group_main", MyLib._myGlobal._resource("ประเภทของธุรกิจ"));
                MyLib._myResource._updateResource("ic_inventory.group_sub", MyLib._myGlobal._resource("ประเภทหลักของผลิตภัณฑ์"));
                MyLib._myResource._updateResource("ic_inventory.item_model", MyLib._myGlobal._resource("Fermentation"));
                MyLib._myResource._updateResource("ic_inventory.item_pattern", MyLib._myGlobal._resource("ตราสินค้า"));
                MyLib._myResource._updateResource("ic_inventory.item_grade", MyLib._myGlobal._resource("กลุ่มสินค้าย่อย/รสชาติ"));

                MyLib._myResource._updateResource("erp_branch_list.sale_hub_approve", MyLib._myGlobal._resource("ผู้อนุมัติ SINGHA ARM"));
                MyLib._myResource._updateResource("erp_credit_approve_level.sale_hub_auth", MyLib._myGlobal._resource("SINGHA ARM User"));
                MyLib._myResource._updateResource("erp_option.sale_hub_approve", MyLib._myGlobal._resource("ผู้อนุมัติ SINGHA ARM"));
                MyLib._myResource._updateResource("erp_option.salehub_approve", MyLib._myGlobal._resource("ผ่านระบบ SINGHA ARM"));
                MyLib._myResource._updateResource("erp_option.sms_and_salehub_approve", MyLib._myGlobal._resource("ผ่านระบบ SMS และ SINGHA ARM"));
                MyLib._myResource._updateResource("erp_user.sale_hub_user", MyLib._myGlobal._resource("Sale Hub Users"));
                */

                // change index
                MyLib._myFrameWork __myFremeWork = new _myFrameWork();

                // select function old index if not match drop and create new index
                if (MyLib._myGlobal._databaseName.Length > 0)
                {
                    DataTable __result = __myFremeWork._queryShort("SELECT indexdef FROM pg_indexes WHERE indexname = \'pk_ap_code_tax_no\' ").Tables[0];
                    if (__result.Rows.Count > 0)
                    {
                        string __newIndex = "CREATE UNIQUE INDEX pk_ap_code_tax_no ON gl_journal_vat_buy USING btree (ap_code, vat_doc_no, vat_calc, vat_effective_year)";
                        string __indexMatch = __result.Rows[0][0].ToString();
                        if (__indexMatch.Equals(__newIndex) == false)
                        {
                            __myFremeWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "DROP INDEX pk_ap_code_tax_no");
                            __myFremeWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __newIndex);
                        }
                    }
                }
            }

            this.Resize += new EventHandler(_mainForm_Resize);
            this.Load += new EventHandler(_mainForm_Load);
            this._mainMenu = this._mainMenuERP;
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._mainMenuERP.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
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




            //MyLib._myGlobal._tableForAutoUnlock.Clear();
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_branch_list._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.gl_chart_of_account._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.gl_journal._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_inventory._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_inventory_barcode._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ap_supplier._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ar_customer._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ar_customer._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_asset._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_asset_maintenance._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_asset_sale._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_pass_book._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_province._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_amper._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_tambon._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_doc_format._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_user_group._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ap_ar_trans._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_trans._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_income_list._table);
            //MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_expenses_list._table);

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

            try
            {
                string __smlSoftPath = @"C:\smlsoft\";
                bool __isDirCreate = MyLib._myUtil._dirExists(__smlSoftPath);

                if (__isDirCreate == false)
                {
                    System.IO.Directory.CreateDirectory(__smlSoftPath); // create folders
                }

                if (MyLib._myUtil._dirExists(@"c:\smltemp\") == false)
                {
                    System.IO.Directory.CreateDirectory(@"c:\smltemp\");
                }

            }
            catch
            {
            }
        }

        _mainMenuClass _userPermissonMenu;
        _mainMenuClass _groupPermissonMenu;

        void _hideMenuCannotAccess()
        {
            if (MyLib._myGlobal._userCode.ToLower().Equals("superadmin") || MyLib._myGlobal._nonPermission)
            {
                return;
            }

            if (MyLib._myGlobal._mainDatabase.Equals("smlerpmain") == false)
            {
                _g.g._companyProfileLoad();
                _userPermissonMenu = MyLib._myGlobal._getUserAccessMenuPermissionAll();
                _groupPermissonMenu = MyLib._myGlobal._getGroupAccessMenuPermissionAll();

                if (_g.g._companyProfile._show_menu_by_permission)
                {
                    _hideNode(this._mainMenuERP.Nodes);

                    //foreach (TreeNode node in this._mainMenuERP.Nodes)
                    //{
                    //    if (node.Nodes.Count == 0)
                    //    {
                    //        node.Tag = (node.Tag != null) ? node.Tag + "&hide&" : "&hide&";
                    //    }
                    //}
                }
            }
        }

        void _hideNode(TreeNodeCollection getNode)
        {
            foreach (TreeNode node in getNode)
            {
                string __menuName = node.Name;

                bool __isRead = false;

                // check group 
                if (_groupPermissonMenu != null)
                {
                    for (int __loop = 0; __loop < _groupPermissonMenu._MainMenuList.Count; __loop++)
                    {
                        MyLib._menuListClass __sub = (MyLib._menuListClass)_groupPermissonMenu._MainMenuList[__loop];
                        string __mainmenu = __sub._menuMainname;
                        for (int __groupLoop = 0; __groupLoop < __sub._munsubList.Count; __groupLoop++)
                        {
                            string __menuCode = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._submeid.ToString();
                            if (node.Name.ToLower().Equals(__menuCode.ToLower()))
                            {
                                __isRead = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isRead;

                            }
                        }
                    }
                }

                if (_userPermissonMenu != null && __isRead == false)
                {
                    for (int __loop = 0; __loop < _userPermissonMenu._MainMenuList.Count; __loop++)
                    {
                        MyLib._menuListClass __sub = (MyLib._menuListClass)_userPermissonMenu._MainMenuList[__loop];
                        string __mainmenu = __sub._menuMainname;
                        for (int __groupLoop = 0; __groupLoop < __sub._munsubList.Count; __groupLoop++)
                        {
                            string __menuCode = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._submeid.ToString();
                            //if (MyLib._myGlobal._mainMenuCode.ToLower().Equals(__menuCode.ToLower()))
                            if (node.Name.ToLower().Equals(__menuCode.ToLower()))
                            {
                                __isRead = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isRead;
                            }
                        }
                    }
                }

                if (__isRead == false)
                {
                    node.Tag = (node.Tag != null) ? node.Tag + "&hide&" : "&hide&";
                }
                _hideNode(node.Nodes);
            }
        }

        /*protected override Boolean _checkIsHideMenu(TreeNode node)
        {
            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                bool __isRead = false;

                // check group 
                if (_groupPermissonMenu != null)
                {
                    for (int __loop = 0; __loop < _groupPermissonMenu._MainMenuList.Count; __loop++)
                    {
                        MyLib._menuListClass __sub = (MyLib._menuListClass)_groupPermissonMenu._MainMenuList[__loop];
                        string __mainmenu = __sub._menuMainname;
                        for (int __groupLoop = 0; __groupLoop < __sub._munsubList.Count; __groupLoop++)
                        {
                            string __menuCode = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._submeid.ToString();
                            if (node.Name.ToLower().Equals(__menuCode.ToLower()))
                            {
                                __isRead = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isRead;

                            }
                        }
                    }
                }

                if (_userPermissonMenu != null && __isRead == false)
                {
                    for (int __loop = 0; __loop < _userPermissonMenu._MainMenuList.Count; __loop++)
                    {
                        MyLib._menuListClass __sub = (MyLib._menuListClass)_userPermissonMenu._MainMenuList[__loop];
                        string __mainmenu = __sub._menuMainname;
                        for (int __groupLoop = 0; __groupLoop < __sub._munsubList.Count; __groupLoop++)
                        {
                            string __menuCode = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._submeid.ToString();
                            //if (MyLib._myGlobal._mainMenuCode.ToLower().Equals(__menuCode.ToLower()))
                            if (node.Name.ToLower().Equals(__menuCode.ToLower()))
                            {
                                __isRead = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isRead;
                            }
                        }
                    }
                }

                return (__isRead == false) ? true : false;

            }
            return false;
        }*/

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
            try
            {
                _dlSoundThread.Abort();
            }
            catch
            {
            }

            try
            {
                if (_sendApproveCreditThread != null)
                    _sendApproveCreditThread.Abort();
            }
            catch
            {

            }

        }

        void _mainForm_Load(object sender, EventArgs e)
        {

            MyLib._myResource._updateResource("ic_inventory.item_brand", MyLib._myGlobal._resource("กลุ่มผลิตภัณฑ์"));
            MyLib._myResource._updateResource("ic_inventory.item_design", MyLib._myGlobal._resource("หน่วยนับ"));
            MyLib._myResource._updateResource("ic_inventory.group_main", MyLib._myGlobal._resource("ประเภทของธุรกิจ"));
            MyLib._myResource._updateResource("ic_inventory.group_sub", MyLib._myGlobal._resource("ประเภทหลักของผลิตภัณฑ์"));
            MyLib._myResource._updateResource("ic_inventory.item_model", MyLib._myGlobal._resource("Fermentation"));
            MyLib._myResource._updateResource("ic_inventory.item_pattern", MyLib._myGlobal._resource("ตราสินค้า"));
            MyLib._myResource._updateResource("ic_inventory.item_grade", MyLib._myGlobal._resource("กลุ่มสินค้าย่อย/รสชาติ"));

            MyLib._myResource._updateResource("erp_branch_list.sale_hub_approve", MyLib._myGlobal._resource("ผู้อนุมัติ SINGHA ARM"));
            MyLib._myResource._updateResource("erp_credit_approve_level.sale_hub_auth", MyLib._myGlobal._resource("SINGHA ARM User"));
            MyLib._myResource._updateResource("erp_option.sale_hub_approve", MyLib._myGlobal._resource("ผู้อนุมัติ SINGHA ARM"));
            MyLib._myResource._updateResource("erp_option.salehub_approve", MyLib._myGlobal._resource("ผ่านระบบ SINGHA ARM"));
            MyLib._myResource._updateResource("erp_option.sms_and_salehub_approve", MyLib._myGlobal._resource("ผ่านระบบ SMS และ SINGHA ARM"));
            MyLib._myResource._updateResource("erp_user.sale_hub_user", MyLib._myGlobal._resource("Sale Hub Users"));



            MyLib._myGlobal._mainSize = this.Size;
            //MyLib._myUtil._showTaskBarNotifier(this, "", "Welcome to SMLERP");
            // check is process
            bool _isProcessDevice = false;
            if (_g.g._companyProfile._process_serial_device.Length > 0)
            {
                MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                string[] _dataDive = Environment.GetLogicalDrives();

                for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                {
                    string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();
                    if (__getDeviceCode.Length > 0 && _g.g._companyProfile._process_serial_device.ToLower().IndexOf(__getDeviceCode) != -1)
                    {
                        _isProcessDevice = true;
                        break;
                    }
                }
            }
            else
            {
                _isProcessDevice = true;
            }

            //MyLib._myUtil._showTaskBarNotifier(this, "", "Welcome to SMLERP");
            // Process
            if (_isProcessDevice == true)
            {
                for (int __loop = 0; __loop < 3; __loop++)
                {
                    Thread __process = new Thread(new ThreadStart(_process));
                    __process.Name = "SML Thread " + __loop.ToString();
                    __process.IsBackground = true;
                    __process.Start();
                    this._threadList.Add(__process);

                    if (MyLib._myGlobal._OEMVersion == "imex")
                    {
                        break;
                    }
                }
            }

            // 
            //this._valcummwork = new Thread(new ThreadStart(_vacuum));
            //this._valcummwork.Start();

            // toe move to SMLERPTemplate
            //this._getPrinterWork = new Thread(_getPrinter);
            //this._getPrinterWork.Start();

            this._loadMyMenu();

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountPOSProfessional)
            {
                // check sound
                string __smlSourndPath = @"C:\\smlsoft\\";
                if (MyLib._myUtil._dirExists(__smlSourndPath) == false || System.IO.File.Exists(__smlSourndPath + "beep-24.wav") == false)
                {
                    //System.IO.Directory.CreateDirectory(__smlSourndPath);
                    SMLPOSControl._posSoundDownload __dw = new SMLPOSControl._posSoundDownload();

                    _dlSoundThread = new Thread(__dw._downloadSound);
                    _dlSoundThread.Start();
                }

                SMLPOSControl._global._checkInitPosDesign();
                SMLPOSControl._global._checkInitConfig();
                this.openWelcomeScreen();

            }

            if (_g.g._companyProfile._warning_reorder_point)
            {
                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                __process._checkICPurchasePointAlert();
            }

            if (_g.g._companyProfile._warning_product_expire)
            {
                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                __process._checkICExpireAlert();

            }

            if (_g.g._companyProfile._warning_bill_overdue)
            {
                SMLERPControl._arInfoProcess __process = new SMLERPControl._arInfoProcess();
                __process._checkArOverDueAlert();

            }

            // ไม่ work multithread น่าจะมีปัญหา เลยย้ายไปทำตอนขออนุมัติ
            //if (MyLib._myGlobal._isVersionAccount)
            //{
            //    this._sendRequestArCredit();
            //}
            _sendMessageProcess();

            if (_g.g._companyProfile._lock_bill_auto && _g.g._companyProfile._lock_bill_auto_interval > 0)
            {
                string __queryLockBill = string.Format("update ic_trans set is_lock_record = 1 where trans_flag = 44 and last_status = 0 and doc_date <= (date(now()) - interval '{0} day') and coalesce(is_lock_record, 0) = 0 ", _g.g._companyProfile._lock_bill_auto_interval);

                MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __queryLockBill);
                if (__result.Length > 0)
                {
                    System.Console.WriteLine(__result);
                }
            }

        }

        public void _sendMessageProcess()
        {
            SMLERPMailMessage._sendData __sendData = new SMLERPMailMessage._sendData();
            _sendApproveCreditThread = new Thread(new ThreadStart(__sendData._sendMessageData));
            this._sendApproveCreditThread.IsBackground = true;
            this._sendApproveCreditThread.Start();

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

        void _selectMenuAudit(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPAudit._selectMenu._getObject(menuName, screenName);
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

        void _selectMenuGl(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPGL._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuSo(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPSO._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuPo(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPPO._selectMenu._getObject(menuName, screenName);
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

        void _selectMenuAsSet(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPASSET._selectMenu._getObject(menuName);
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

        private void _selectMenuAPReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPAPReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        private void _selectMenuARReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPARReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        private void _selectMenuSOReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPSOReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuAP(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPAP._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuCashBank(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPCASHBANK._selectMenu._getObject(menuName);
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

        private void _selectMenuPOReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPPOReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuPOS(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLPOSControl._selectMenu._getObject(menuName, screenName);
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

        void _selectMenuTransportLabel(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLTransportLabel._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuCBReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPCASHBANKReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuPOSClient(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLPosClient._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuEDI(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLEDIControl._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuPP(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLPPShipment._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuMap(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPGL._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }


        // toe
        public void openWelcomeScreen()
        {

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountPOSProfessional) // Account POS เท่านั้น
            {
                Boolean __openWelcomeScreen = true;

                try
                {
                    if (File.Exists(MyLib._myGlobal._posConfigOpenWelcomeScreen))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(MyLib._myGlobal._posConfigOpenWelcomeScreen);
                        xDoc.DocumentElement.Normalize();

                        XmlElement __xRoot = xDoc.DocumentElement;

                        XmlNodeList __xReader = __xRoot.GetElementsByTagName("openDisplay");
                        if (__xReader.Count > 0)
                        {
                            XmlNode __xFirstNode = __xReader.Item(0);
                            if (__xFirstNode.NodeType == XmlNodeType.Element)
                            {
                                XmlElement __xTable = (XmlElement)__xFirstNode;
                                if (((int)MyLib._myGlobal._decimalPhase(__xTable.InnerText)) == 1)
                                {
                                    __openWelcomeScreen = false;
                                }
                            }
                        }
                    }

                }
                catch
                {
                }

                if (__openWelcomeScreen)
                {
                    // main shortcut
                    _mainShortcut __main = new _mainShortcut();
                    __main._addShortcut += __main__addShortcut;
                    __main._menuButtonClick += (menuId, menuText, menuTag) =>
                    {
                        _activeMenu("menu_shortcut", menuText, menuTag);
                    };
                    _createAndSelectTab("Main", "Main", "menu_shortcut", __main);
                }
            }
        }

        void __main__addShortcut(object sender, int panelIndex)
        {
            if (this._addHomeShortcut(panelIndex))
            {
                // refresh panel
                _mainShortcut __mainShortcut = (_mainShortcut)sender;
                __mainShortcut._refreshCustomShortCut();
            }
        }

        void _selectMenuSINGHAReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SINGHAReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuSINGHASyncMaster(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLSINGHAControl._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        public override void _activeMenu(string mainMenuId, string menuName, string tag)
        {
            try
            {
                Boolean __pass = false;
                if (SystemInformation.ComputerName.ToLower().IndexOf("jead") != -1)
                {
                    __pass = true;
                }
                if (__pass == false)
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
                }
                // ตรวจสอบสิทธิ์
                MyLib._mainMenuClass __listmenu = new MyLib._mainMenuClass();
                __listmenu = MyLib._myGlobal._listMenuAll;
                bool __ischeckMainmenu = (mainMenuId.Equals(menuName)) ? true : false;
                string _mainMenuCode = "";
                // start
                if (__ischeckMainmenu == false)
                {
                    _g.g._accountPeriodGet();
                    this._checkPeriod();
                    string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
                    _mainMenuCode = menuName;
                    _PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(mainMenuId, _mainMenuCode);
                    MyLib._myGlobal._mainMenuIdPassTrue = mainMenuId;
                    MyLib._myGlobal._mainMenuCodePassTrue = _mainMenuCode;
                    _g.g._companyProfileLoad();
                    //
                    if (__permission._isRead || MyLib._myGlobal._userCode.ToLower().Equals("superadmin"))
                    {
                        if (tag.IndexOf("&fastreportother&") != -1)
                        {
                            //SMLFastReport._designer __fastReport = new SMLFastReport._designer();
                            //__fastReport._loadReportOhter(menuName);
                            Control __getControl = SMLFastReport._selectMenu._getObject(menuName, menuName);
                            if (__getControl != null)
                            {
                                _createAndSelectTab(menuName, menuName, menuName, __getControl);
                            }
                        }
                        else
                            if (tag.IndexOf("&fastreport&") != -1)
                        {
                            SMLFastReport._designer __fastReport = new SMLFastReport._designer();
                            __fastReport._load(menuName);
                            _createAndSelectTab(menuName, menuName, menuName, __fastReport);
                        }
                        else
                                if (menuName.Equals("data_backup"))
                        {
                            MyLib._databaseManage._dataBackupForm __form = new MyLib._databaseManage._dataBackupForm();
                            __form.ShowDialog();
                        }
                        else
                                    if (menuName.Equals("menu_import_from_image"))
                        {
                            SMLERPControl._importFromImageForm __import = new SMLERPControl._importFromImageForm();
                            __import.ShowDialog();
                        }
                        else
                                        if (menuName.Equals("data_restore"))
                        {
                            MyLib._databaseManage._dataRestoreForm __form = new MyLib._databaseManage._dataRestoreForm();
                            __form.ShowDialog();

                        }
                        else
                                            if (menuName.Equals("menu_mis_calendar_trans"))
                        {
                            _createAndSelectTab(menuName, menuName, menuName, new SMLMIS.calendar_trans());
                        }
                        else
                                                if (menuName.Equals("fast_report_designer"))
                        {
                            _createAndSelectTab(menuName, menuName, menuName, new SMLFastReport._designer());
                        }
                        else
                                                    if (menuName.Equals("menu_setup_staff_pic"))
                        {
                            _createAndSelectTab(menuName, menuName, menuName, new SMLERPControl.erp_user._erp_userDetailpicture());
                        }
                        else if (menuName.Equals("menu_form_edit"))
                        {
                            _createAndSelectTab(menuName, menuName, menuName, new SMLReport._formReport._formDesigner());
                        }
                        else if (menuName.Equals("menu_import_formdesign"))
                        {
                            _createAndSelectTab(menuName, menuName, menuName, new SMLReport._ImportFormDesign());
                        }
                        else if (menuName.Equals("menu_import_report"))
                        {
                            _createAndSelectTab(menuName, menuName, menuName, new SMLFastReport._importFastReport());
                        }
                        else if (menuName.Equals("menu_import_report_branch"))
                        {
                            _createAndSelectTab(menuName, menuName, menuName, new SMLFastReport._importFastReport() { mode = 1 });
                        }
                        else if (menuName.Equals("client_design"))
                        {
                            _createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._designer._clientDesign());
                        }
                        else
                                if (menuName.Equals("menu_pos_point_rate"))
                        {
                            SMLPOSControl._posPointPeriodForm __form = new SMLPOSControl._posPointPeriodForm();
                            __form.ShowDialog();
                        }
                        else
                                    if (tag.IndexOf("&transportlabel&") != -1)
                        {
                            //_createAndSelectTab(menuName, menuName, menuName, new SMLTransportLabel._transport_label(0));
                            _selectMenuTransportLabel(menuName, __screenName);

                        }
                        else
                                        if (menuName.Equals("menu_welcome_page"))
                        {
                            _mainShortcut __main = new _mainShortcut();
                            __main._addShortcut += __main__addShortcut;
                            __main._menuButtonClick += (menuId, menuText, menuTag) =>
                            {
                                _activeMenu("menu_shortcut", menuText, menuTag);
                            };
                            _createAndSelectTab("Main", "Main", "menu_shortcut", __main);

                        }
                        else if (tag.IndexOf("&posclient&") != -1)
                        {
                            //_createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._designer._clientDesign());
                            //MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, __screenName, new SMLPosClient._configPOSScreen());
                            _selectMenuPOSClient(menuName, __screenName);
                        }
                        else
                                            if (tag.IndexOf("&edi&") != -1)
                        {
                            _selectMenuEDI(menuName, __screenName);

                        }
                        else
                                                if (tag.IndexOf("&pos&") != -1)
                        {
                            _selectMenuPOS(menuName, __screenName);
                        }
                        else
                                                    if (tag.IndexOf("&audit&") != -1)
                        {
                            _selectMenuAudit(menuName, __screenName);
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
                                                                if (tag.IndexOf("&gl&") != -1)
                        {
                            _selectMenuGl(menuName, __screenName);
                        }
                        else
                                                                    if (tag.IndexOf("&so&") != -1)
                        {
                            _selectMenuSo(menuName, __screenName);
                        }
                        else
                                                                        if (tag.IndexOf("&po&") != -1)
                        {
                            _selectMenuPo(menuName, __screenName);
                        }
                        else
                                                                            if (tag.IndexOf("&ar&") != -1)
                        {
                            _selectMenuAR(menuName, __screenName);
                        }
                        else
                                                                                if (tag.IndexOf("&ar_healthy&") != -1)
                        {
                            _selectMenuAR(menuName, __screenName);
                        }
                        else
                                                                                    if (tag.IndexOf("&poreport&") != -1)
                        {
                            _selectMenuPOReport(menuName, __screenName);
                        }
                        else
                                                                                        if (tag.IndexOf("&ap&") != -1)
                        {
                            _selectMenuAP(menuName, __screenName);
                        }
                        else
                                                                                            if (tag.IndexOf("&cashbank&") != -1)
                        {
                            _selectMenuCashBank(menuName, __screenName);
                        }
                        else
                                                                                                if (tag.IndexOf("&ic&") != -1)
                        {
                            _selectMenuIC(menuName, __screenName);
                        }
                        else
                                                                                                    if (tag.IndexOf("&apreport&") != -1)
                        {
                            _selectMenuAPReport(menuName, __screenName);
                        }
                        else
                                                                                                        if (tag.IndexOf("&arreport&") != -1)
                        {
                            _selectMenuARReport(menuName, __screenName);
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
                        else if (tag.IndexOf("&icinfo&") != -1)
                        {
                            _selectMenuICInfo(menuName, __screenName);
                        }
                        else
                                if (tag.IndexOf("&soreport&") != -1)
                        {
                            _selectMenuSOReport(menuName, __screenName);
                        }
                        else
                                if (tag.IndexOf("&pp&") != -1)
                        {
                            _selectMenuPP(menuName, __screenName);
                        }

                        else
                        {
                            if (tag.IndexOf("&as&") != -1)
                            {
                                _selectMenuAsSet(menuName, __screenName);
                            }
                            else
                            {
                                if (tag.IndexOf("&pos&") != -1)
                                {
                                    _selectMenuPOS(menuName, __screenName);
                                }
                                else
                                    if (tag.IndexOf("&cbreport&") != -1)
                                {
                                    _selectMenuCBReport(menuName, __screenName);
                                }

                                else if (tag.IndexOf("&singhareport&") != -1)
                                {
                                    _selectMenuSINGHAReport(menuName, __screenName);
                                }
                                else if (tag.IndexOf("&singhasyncmaster&") != -1)
                                {
                                    _selectMenuSINGHASyncMaster(menuName, __screenName);
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

                        }
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถเข้าเมนูนี้ได้") + __screenName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (OutOfMemoryException OutofMemoryEX)
            {
                Debugger.Break();
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

    }
}
