using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DTSClientDownload
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        Thread _dataSyncDTSServer = null;



        public _mainForm()
        {
        }

        public override void _constructMethod()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._menuBar.Visible = false;
                this._toolBar.Visible = false;

                //this._myMenuScreen = new SMLERPControl._myMenu();
                this._timerStatus.Enabled = false;
                //ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
                this._menuPanel.Visible = false;
                this.MinimumSize = new Size(600, 600);
                this.Font = MyLib._myGlobal._myFont;
                this.DoubleBuffered = true;

                this._registerInfoLabel.Visible = false;
                this._statusUserList.Visible = false;
                this._syncDataButton.Visible = false;
                SMLERPTemplate._memoryCleaner __memoryCleaner = new SMLERPTemplate._memoryCleaner();
                __memoryCleaner.Start();
                this._misButton.Visible = false;

                this._getResourceMenu = false;
                this._tabControl.FixedName = true;

                this._programName = MyLib._myGlobal._programName;
                InitializeComponent();

                this.Load += new EventHandler(_mainForm_Load);
                this.Resize += new EventHandler(_mainForm_Resize);
                this.Disposed += new EventHandler(_mainForm_Disposed);

                this._mainMenu = this._mainDTSMenu;
                this._tabControl.TabPages.Clear();

                this._statusTimerProcess.Start();
            }
        }

        void _mainForm_Disposed(object sender, EventArgs e)
        {

        }

        void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
        }

        void _mainForm_Load(object sender, EventArgs e)
        {
            MyLib._myGlobal._statusLabel = this._status;
            MyLib._myGlobal._statusStrip = this._statusStrip;

            this._dataSyncDTSServer = new Thread(new ThreadStart(_dtsSync._startSync));
            this._dataSyncDTSServer.IsBackground = true;
            this._dataSyncDTSServer.Start();

            this.Text = MyLib._myGlobal._databaseName + " : " + _programName + " " + this._versionInfo;
            this._showStatus();
            //this._processStatusBar();
            this._status.Text = _global._champDatabaseName;

            _mainMenu.__name = "MAIN";
            //__menuXml = new StringBuilder();
            for (int loop = 0; loop < _mainMenu.Nodes.Count; loop++)
            {
                mainMenuChangeResource("", loop, _mainMenu.Nodes[loop]);
            } // for

            //_startup();
            // Create Menu Bar
            this._menuBar.Items.Clear();
            for (int loop = 0; loop < this._mainMenu.Nodes.Count; loop++)
            {
                this._menuBar.Items.Add(_createMenuBar(null, this._mainMenu.Nodes[loop], this._menuBar.Font));
            }
            //

            _mainMenu.NodeMouseClick += new TreeNodeMouseClickEventHandler(_mainMenu_NodeMouseClick);
            _mainMenu.KeyDown += new KeyEventHandler(_mainMenu_KeyDown);

            //this._dock.Dock = DockStyle.Fill;

            //// toe ยกเว้น dts ไม่ต้องโหลด home
            //this.Home.Controls.Add(this._dock);

            //DockableFormInfo __formLeft = this._dock.Add(this._outLook, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            //this._dock.DockForm(__formLeft, DockStyle.Left, zDockMode.Inner);
            //__formLeft.ShowCloseButton = false;
            //__formLeft.ShowContextMenuButton = false;

            // create  tab รอเลย
            _createAndSelectTab("menu_item_download", "menu_item_download", "ข้อมูลสินค้า SCG", new _dts_item_download());
            _createAndSelectTab("menu_po_download", "menu_po_download", "นำเข้าใบสั่งซื้อสินค้า eOrdering", new _dts_po_download());
            _createAndSelectTab("menu_so_download", "menu_so_download", "นำเข้าใบสั่งขาย/สั่งจอง", new _so_Download());
            _createAndSelectTab("dts_download_log", "dts_download_log", "ประวัติการรับข้อมูล", new _download_log());

            this._tabControl.SelectTab(0);
        }

        public override void _activeMenu(string mainMenuId, string menuName, string tag)
        {
            //string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
            string __screenName = "";
            if (menuName.Equals("menu_item_download"))
            {
                __screenName = "ข้อมูลสินค้า SCG";
                _createAndSelectTab(menuName, menuName, __screenName, new _dts_item_download());
            }
            else
                if (menuName.Equals("menu_po_download"))
                {
                    __screenName = "นำเข้าใบสั่งซื้อสินค้า eOrdering";
                    _createAndSelectTab(menuName, menuName, __screenName, new _dts_po_download());
                }
                else if (menuName.Equals("menu_so_download"))
                {
                    __screenName = "นำเข้าใบสั่งขาย/สั่งจอง";
                    _createAndSelectTab(menuName, menuName, __screenName, new _so_Download());
                }
                else if (menuName.Equals("dts_download_log"))
                {
                    __screenName = "ประวัติการรับข้อมูล";
                    _createAndSelectTab(menuName, menuName, __screenName, new _download_log());
                }
        }

        private void _statusTimerProcess_Tick(object sender, EventArgs e)
        {
            this._syncDataButton.Visible = MyLib._myGlobal._syncDataActive;
        }
    }
}
