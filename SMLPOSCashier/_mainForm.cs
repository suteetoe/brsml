using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyLib;
using System.Threading;
using System.Collections;
using SMLPosClient;

namespace SMLPOSCashier
{
    /// <summary>Master POS Controller</summary>
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public ArrayList _threadList = new ArrayList();

        public _mainForm()
        {
            InitializeComponent();
            this._programName = "SML POS Client";
            this._mainMenu = this._mainMenuPosClient;
            this._menuImageList = this._menuImage;

            this._programName = (MyLib._myGlobal._programName.Length == 0) ? "SML ERP" : MyLib._myGlobal._programName;
            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Version : {0}  Build : {1:s}", __myVersion, __myTime);
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();

            this.Disposed += new EventHandler(_mainForm_Disposed);
            //
            //this._closeAllControl();
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            //MyLib._myGlobal._mainSize = this.Size;
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
        }


        void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
        }

        private void _mainForm_Load(object sender, EventArgs e)
        {
            // select 

            //_posClientForm _posClient = new _posClientForm();
            //_posClient.Dock = DockStyle.Fill;
            //this.Controls.Add(_posClient);
            //MyLib._myUtil._showTaskBarNotifier(this, "", "Welcome to SMLERP");
            MyLib._myGlobal._mainSize = this.Size;
            //MyLib._myUtil._showTaskBarNotifier(this, "", "Welcome to SMLERP");

            // Process
            for (int __loop = 0; __loop < 5; __loop++)
            {
                Thread __process = new Thread(new ThreadStart(_process));
                __process.Name = "SML Thread " + __loop.ToString();
                __process.IsBackground = true;
                __process.Start();
                this._threadList.Add(__process);
            }

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

        private void button3_Click(object sender, EventArgs e)
        {
            _posPromotionForm __xx = new _posPromotionForm();
            __xx.ShowDialog();
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
                    if (menuName.Equals("menu_config_pos_screen"))
                    {
                        //_createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._designer._clientDesign());
                        MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, __screenName, new _configPOSScreen());
                    }
                    else
                        if (menuName.Equals("menu_display_pos_screen"))
                        {
                            _posLoginForm __login = new _posLoginForm();
                            __login.ShowDialog();
                            if (__login._isPassed)
                            {
                                _createAndSelectTab(menuName, menuName, __screenName, new _posClientForm(__login._userCode,__login._passwordTextBox.Text));
                            }
                        }
                        else
                            if (menuName.Equals("menu_save_send_money"))
                            {
                                _manageMasterCodeFull __screenFull = new MyLib._manageMasterCodeFull();
                                __screenFull._labelTitle.Text = __screenName;
                                __screenFull._dataTableName = _g.d.POSCashierSettle._table;
                                __screenFull._addColumn(_g.d.POSCashierSettle._DocNo, 10, 100);
                                __screenFull._inputScreen._addDateBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._DocDate, 1, true);
                                //__screenFull._addColumn(_g.d.POSCashierSettle._DocDate, 100, 100);
                                __screenFull._addColumn(_g.d.POSCashierSettle._CashierCode, 100, 100);
                                __screenFull._addColumn(_g.d.POSCashierSettle._MACHINECODE, 100, 100);
                                __screenFull._addColumn(_g.d.POSCashierSettle._POS_ID, 100, 100);
                                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._CashAmount, 1, 2, true);
                                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._CreditCardAmount, 1, 2, true);
                                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._CoupongAmount, 1, 2, true);
                                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._ChqAmount, 1, 2, true);
                                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._TransferAmount, 1, 2, true);
                                __screenFull._finish();

                                _createAndSelectTab(menuName, menuName, __screenName, __screenFull);

                            }
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถเข้าเมนูนี้ได้") + __screenName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }

        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            /*
            switch (keyData)
            {
                case Keys.F10 :
                    this.Close();
                    break;

                    // check full screen mode

            }
             * */
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
