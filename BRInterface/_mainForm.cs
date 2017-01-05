using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BRInterface
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public _mainForm()
        {
            InitializeComponent();

            this.Resize += _mainForm_Resize;
            this.Load += _mainForm_Load;
            this.Disposed += _mainForm_Disposed;

            this._mainMenu = this._mainMenuInterface;
            this._programName = (MyLib._myGlobal._programName.Length == 0) ? "Br Interface" : MyLib._myGlobal._programName;
            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Version : {0}  Build : {1:s}", __myVersion, __myTime);
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();

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
                    string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
                    _mainMenuCode = menuName;
                    MyLib._PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(mainMenuId, _mainMenuCode);
                    MyLib._myGlobal._mainMenuIdPassTrue = mainMenuId;
                    MyLib._myGlobal._mainMenuCodePassTrue = _mainMenuCode;
                    _g.g._companyProfileLoad();

                    switch (menuName)
                    {
                        case "menu_import_request_transfer_import":
                            _createAndSelectTab(menuName, menuName, __screenName, new saletools.import_request_transfer());
                            break;
                        case "menu_improt_sale":
                            _createAndSelectTab(menuName, menuName, __screenName, new saletools._import_so_invoice(1));
                            break;
                        case "menu_import_sale_order":
                            _createAndSelectTab(menuName, menuName, __screenName, new saletools._import_so_invoice(2));
                            break;
                    }
                    //
                    /*
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
                    */
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _mainForm_Disposed(object sender, EventArgs e)
        {
        }

        private void _mainForm_Load(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
            this._loadMyMenu();
        }

        private void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
        }
    }
}
