using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSChecker
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public _mainForm()
        {
            InitializeComponent();

            this.Resize += _mainForm_Resize;
            this.Disposed += _mainForm_Disposed;

            this._mainMenu = this._checkerMenu;
            this._menuImageList = this.imageList1;

            this._programName = (MyLib._myGlobal._programName.Length == 0) ? "SML ERP" : MyLib._myGlobal._programName;
            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Version : {0}  Build : {1:s}", __myVersion, __myTime);
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();


            this.Load += _mainForm_Load;
        }

        public override void _activeMenu(string mainMenuId, string menuName, string tag)
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
            if (menuName.Equals("check_vat"))
            {
                _createAndSelectTab(menuName, menuName, menuName, new _checkVatControl());
            }


        }

        void _mainForm_Load(object sender, EventArgs e)
        {
            
        }

        void _mainForm_Disposed(object sender, EventArgs e)
        {
            
        }

        void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
        }
    }
}
