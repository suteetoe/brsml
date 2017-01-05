using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SMLServiceCenter
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public void _disableMenu(TreeNodeCollection source)
        {
            int __index = 0;
            while (__index < source.Count)
            {
                TreeNode nodes = source[__index];
                if (nodes.Tag != null && nodes.Tag.ToString().ToLower().IndexOf("&disable&") != -1)
                {                                    
                    nodes.Remove();                  
                }
                else
                {
                    if (nodes.Nodes != null)
                    {
                        this._disableMenu(nodes.Nodes);
                    }
                    __index++;
                }
            }
        }
        public _mainForm()
        {
            InitializeComponent();
            this._disableMenu(this._mainMenuServiceCenter.Nodes);
            this.Resize += new EventHandler(_mainForm_Resize);
            this.Load += new EventHandler(_mainForm_Load);
           
            this._programName = (MyLib._myGlobal._programName.Length == 0) ? "SML ERP" : MyLib._myGlobal._programName;
            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Version : {0}  Build : {1:s}", __myVersion, __myTime);
            this._mainMenu = this._mainMenuServiceCenter;
            this._menuImageList = this._menuImage;
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();
            this.Disposed += new EventHandler(_mainForm_Disposed);
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
            
        }

        void _mainForm_Disposed(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
        }

        void _mainForm_Load(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
            //MyLib._myUtil._showTaskBarNotifier(this, "", "Welcome to SMLERP");
            // Process
            this.__process = new Thread(_process);
            this.__process.Start();
        }
        Thread __process;
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

        public override void _activeMenu(string mainMenuId, string menuName, string tag)
        {
            // start
            string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
            if (menuName.Equals("menu_form_edit"))
            {
                _g.g._companyProfileLoad();
                _createAndSelectTab(menuName, menuName, menuName, new SMLReport._formReport._formDesigner());
            }
            else
                if (tag.IndexOf("&flow&") != -1)
                {
                    _g.g._companyProfileLoad();
                    _selectMenuFlow(menuName, __screenName);
                }
                else
                    if (tag.IndexOf("&config&") != -1)
                    {
                        _g.g._companyProfileLoad();
                        _selectMenuConfig(menuName, __screenName);
                    }
                    else
                        if (tag.IndexOf("&gl&") != -1)
                        {
                            _g.g._companyProfileLoad();
                            _selectMenuGl(menuName, __screenName);
                        }
                        else
                            if (tag.IndexOf("&so&") != -1)
                            {
                                _g.g._companyProfileLoad();
                                _selectMenuSo(menuName, __screenName);
                            }
                            else
                                if (tag.IndexOf("&po&") != -1)
                                {
                                    _g.g._companyProfileLoad();
                                    _selectMenuPo(menuName, __screenName);
                                }
                                else
                                    if (tag.IndexOf("&ar&") != -1)
                                    {
                                        _g.g._companyProfileLoad();
                                        _selectMenuAR(menuName, __screenName);
                                    }
                                    else
                                        if (tag.IndexOf("&poreport&") != -1)
                                        {
                                            _g.g._companyProfileLoad();
                                            _selectMenuPOReport(menuName, __screenName);
                                        }
                                        else
                                            if (tag.IndexOf("&ap&") != -1)
                                            {
                                                _g.g._companyProfileLoad();
                                                _selectMenuAP(menuName, __screenName);
                                            }
                                            else
                                                if (tag.IndexOf("&cashbank&") != -1)
                                                {
                                                    _g.g._companyProfileLoad();
                                                    _selectMenuCashBank(menuName, __screenName);
                                                }
                                                else
                                                    if (tag.IndexOf("&ic&") != -1)
                                                    {
                                                        _g.g._companyProfileLoad();
                                                        _selectMenuIC(menuName, __screenName);
                                                    }
                                                    else
                                                        if (tag.IndexOf("&apreport&") != -1)
                                                        {
                                                            _g.g._companyProfileLoad();
                                                            _selectMenuAPReport(menuName, __screenName);
                                                        }
                                                        else
                                                            if (tag.IndexOf("&arreport&") != -1)
                                                            {
                                                                _g.g._companyProfileLoad();
                                                                _selectMenuARReport(menuName, __screenName);
                                                            }
                                                            else
                                                                if (tag.IndexOf("&report&") != -1)
                                                                {
                                                                    _g.g._companyProfileLoad();
                                                                    _selectMenuReport(menuName, __screenName);
                                                                }
                                                                else if (tag.IndexOf("&reportic&") != -1)
                                                                {
                                                                    _g.g._companyProfileLoad();
                                                                    _selectMenuReportIC(menuName, __screenName);
                                                                }
                                                                else
                                                                    if (tag.IndexOf("&icinfo&") != -1)
                                                                    {
                                                                        _g.g._companyProfileLoad();
                                                                        _selectMenuICInfo(menuName, __screenName);
                                                                    }
                                                                    else
                                                                        if (tag.IndexOf("&soreport&") != -1)
                                                                        {
                                                                            _g.g._companyProfileLoad();
                                                                            _selectMenuSOReport(menuName, __screenName);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (tag.IndexOf("&as&") != -1)
                                                                            {
                                                                                _g.g._companyProfileLoad();
                                                                                _selectMenuAsSet(menuName, __screenName);
                                                                            }
                                                                            else
                                                                            {
                                                                                bool __selectTabFound = _selectTab(menuName);
                                                                                if (__selectTabFound == false)
                                                                                {
                                                                                   // Control __getControl = _g._selectMenu._getObject(menuName, __screenName, _programName);
                                                                                    Control __getControl = (new _g._selectMenu())._getObject(menuName, __screenName, _programName);
                                                                                    if (__getControl != null)
                                                                                    {
                                                                                        _g.g._companyProfileLoad();
                                                                                        _createAndSelectTab(menuName, menuName, __screenName, __getControl);
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
        }

    }
}
