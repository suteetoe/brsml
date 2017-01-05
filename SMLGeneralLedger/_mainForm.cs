using MyLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLGeneralLedger
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
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



        public _mainForm()
        {
            InitializeComponent();

            this.Resize += _mainForm_Resize;
            this.Load += _mainForm_Load;
            this.Disposed += _mainForm_Disposed;

            this._mainMenu = this._myMainMenu;

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myMainMenu.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._programName = (MyLib._myGlobal._programName.Length == 0) ? "SML ERP" : MyLib._myGlobal._programName;
            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Version : {0}  Build : {1:s}", __myVersion, __myTime);
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();

            this._manageTableForAutoUnlock();

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

        void _mainForm_Disposed(object sender, EventArgs e)
        {

        }

        void _mainForm_Load(object sender, EventArgs e)
        {

        }

        void _mainForm_Resize(object sender, EventArgs e)
        {

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
                        if (menuName.Equals("data_backup"))
                        {
                            MyLib._databaseManage._dataBackupForm __form = new MyLib._databaseManage._dataBackupForm();
                            __form.ShowDialog();
                        }
                        else
                            if (menuName.Equals("menu_form_edit"))
                            {
                                _createAndSelectTab(menuName, menuName, menuName, new SMLReport._formReport._formDesigner());
                            }
                            else
                                if (menuName.Equals("data_restore"))
                                {
                                    MyLib._databaseManage._dataRestoreForm __form = new MyLib._databaseManage._dataRestoreForm();
                                    __form.ShowDialog();

                                }
                                else
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
                                        if (tag.IndexOf("&audit&") != -1)
                                        {
                                            _selectMenuAudit(menuName, __screenName);
                                        }
                                        else
                                            if (tag.IndexOf("&icinfo&") != -1)
                                            {
                                                _selectMenuICInfo(menuName, __screenName);
                                            }
                                            else
                                                if (tag.IndexOf("&gl&") != -1)
                                                {
                                                    _selectMenuGl(menuName, __screenName);
                                                }
                                                else if (tag.IndexOf("&config&") != -1)
                                                {
                                                    _selectMenuConfig(menuName, __screenName);
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
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
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



    }
}
