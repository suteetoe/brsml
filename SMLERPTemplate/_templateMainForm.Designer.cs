namespace SMLERPTemplate
{
    partial class _templateMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_templateMainForm));
            this._contextMenuBoardStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._newsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._timerFindWebservice = new System.Windows.Forms.Timer(this.components);
            this._menuBar = new System.Windows.Forms.MenuStrip();
            this._timerStatus = new System.Windows.Forms.Timer(this.components);
            this._imageList16x16 = new System.Windows.Forms.ImageList(this.components);
            this._tabControl = new MyLib._myTabControl();
            this.Home = new System.Windows.Forms.TabPage();
            this._menuPanel = new System.Windows.Forms.Panel();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._statusUserList = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusComputerInformation = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusUserLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusDatabaseName = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusCompressWebservice = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusWebserviceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._status = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this._changeDate = new MyLib.ToolStripMyButton();
            this._showLeftMenu = new System.Windows.Forms.ToolStripButton();
            this._misButton = new System.Windows.Forms.ToolStripButton();
            this._screenSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._registerInfoLabel = new System.Windows.Forms.ToolStripLabel();
            this._syncDataButton = new System.Windows.Forms.ToolStripButton();
            this._contextMenuBoardStrip.SuspendLayout();
            this._tabControl.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contextMenuBoardStrip
            // 
            this._contextMenuBoardStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newsToolStripMenuItem});
            this._contextMenuBoardStrip.Name = "_contextMenuBoardStrip";
            this._contextMenuBoardStrip.Size = new System.Drawing.Size(104, 26);
            // 
            // _newsToolStripMenuItem
            // 
            this._newsToolStripMenuItem.Name = "_newsToolStripMenuItem";
            this._newsToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this._newsToolStripMenuItem.Text = "News";
            this._newsToolStripMenuItem.Click += new System.EventHandler(this._newsToolStripMenuItem_Click);
            // 
            // _timerFindWebservice
            // 
            this._timerFindWebservice.Enabled = true;
            this._timerFindWebservice.Interval = 60000;
            this._timerFindWebservice.Tick += new System.EventHandler(this._timerFindWebservice_Tick);
            // 
            // _menuBar
            // 
            this._menuBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._menuBar.GripMargin = new System.Windows.Forms.Padding(0);
            this._menuBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this._menuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this._menuBar.Location = new System.Drawing.Point(324, 0);
            this._menuBar.Name = "_menuBar";
            this._menuBar.Padding = new System.Windows.Forms.Padding(0);
            this._menuBar.Size = new System.Drawing.Size(603, 0);
            this._menuBar.TabIndex = 9;
            this._menuBar.Text = "Menu Bar";
            // 
            // _timerStatus
            // 
            this._timerStatus.Enabled = true;
            this._timerStatus.Interval = 1000;
            this._timerStatus.Tick += new System.EventHandler(this._timerStatus_Tick);
            // 
            // _imageList16x16
            // 
            this._imageList16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList16x16.ImageStream")));
            this._imageList16x16.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList16x16.Images.SetKeyName(0, "");
            this._imageList16x16.Images.SetKeyName(1, "");
            this._imageList16x16.Images.SetKeyName(2, "media_play_green.png");
            this._imageList16x16.Images.SetKeyName(3, "media_stop_red.png");
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this.Home);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Font = new System.Drawing.Font("Tahoma", 9F);
            this._tabControl.ItemSize = new System.Drawing.Size(0, 20);
            this._tabControl.Location = new System.Drawing.Point(324, 25);
            this._tabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._tabControl.Multiline = true;
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(603, 517);
            this._tabControl.TabIndex = 6;
            this._tabControl.TableName = "";
            this._tabControl.TabStop = false;
            // 
            // Home
            // 
            this.Home.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(104)))), ((int)(((byte)(147)))));
            this.Home.Location = new System.Drawing.Point(4, 24);
            this.Home.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Home.Name = "Home";
            this.Home.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Home.Size = new System.Drawing.Size(595, 489);
            this.Home.TabIndex = 0;
            this.Home.Text = "Home";
            // 
            // _menuPanel
            // 
            this._menuPanel.BackColor = System.Drawing.Color.White;
            this._menuPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._menuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this._menuPanel.Location = new System.Drawing.Point(0, 0);
            this._menuPanel.Name = "_menuPanel";
            this._menuPanel.Padding = new System.Windows.Forms.Padding(4);
            this._menuPanel.Size = new System.Drawing.Size(324, 568);
            this._menuPanel.TabIndex = 0;
            // 
            // _statusStrip
            // 
            this._statusStrip.BackgroundImage = global::SMLERPTemplate.Properties.Resources.bg2;
            this._statusStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusUserList,
            this._statusComputerInformation,
            this._statusProcess,
            this._statusUserLabel,
            this._statusDatabaseName,
            this._statusCompressWebservice,
            this._statusWebserviceLabel,
            this._status});
            this._statusStrip.Location = new System.Drawing.Point(324, 542);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Padding = new System.Windows.Forms.Padding(1, 4, 16, 1);
            this._statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._statusStrip.Size = new System.Drawing.Size(603, 26);
            this._statusStrip.TabIndex = 7;
            this._statusStrip.Text = "statusStrip1";
            // 
            // _statusUserList
            // 
            this._statusUserList.BackColor = System.Drawing.Color.Transparent;
            this._statusUserList.ForeColor = System.Drawing.Color.White;
            this._statusUserList.Image = global::SMLERPTemplate.Properties.Resources.users2;
            this._statusUserList.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._statusUserList.Name = "_statusUserList";
            this._statusUserList.Size = new System.Drawing.Size(69, 16);
            this._statusUserList.Text = "User List";
            this._statusUserList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._statusUserList.ToolTipText = "Display user list";
            this._statusUserList.Click += new System.EventHandler(this._statusUserList_Click);
            // 
            // _statusComputerInformation
            // 
            this._statusComputerInformation.BackColor = System.Drawing.Color.Transparent;
            this._statusComputerInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._statusComputerInformation.ForeColor = System.Drawing.Color.White;
            this._statusComputerInformation.Image = global::SMLERPTemplate.Properties.Resources.key1;
            this._statusComputerInformation.Name = "_statusComputerInformation";
            this._statusComputerInformation.Size = new System.Drawing.Size(16, 16);
            this._statusComputerInformation.Text = "Computer infomation";
            this._statusComputerInformation.ToolTipText = "Computer information";
            // 
            // _statusProcess
            // 
            this._statusProcess.BackColor = System.Drawing.Color.Transparent;
            this._statusProcess.ForeColor = System.Drawing.Color.White;
            this._statusProcess.Image = global::SMLERPTemplate.Properties.Resources.oszillograph;
            this._statusProcess.Name = "_statusProcess";
            this._statusProcess.Size = new System.Drawing.Size(64, 16);
            this._statusProcess.Text = "Process";
            // 
            // _statusUserLabel
            // 
            this._statusUserLabel.BackColor = System.Drawing.Color.Transparent;
            this._statusUserLabel.ForeColor = System.Drawing.Color.White;
            this._statusUserLabel.Image = global::SMLERPTemplate.Properties.Resources.user1;
            this._statusUserLabel.Name = "_statusUserLabel";
            this._statusUserLabel.Size = new System.Drawing.Size(112, 16);
            this._statusUserLabel.Text = "User information";
            this._statusUserLabel.ToolTipText = "User information";
            // 
            // _statusDatabaseName
            // 
            this._statusDatabaseName.BackColor = System.Drawing.Color.Transparent;
            this._statusDatabaseName.ForeColor = System.Drawing.Color.White;
            this._statusDatabaseName.Image = global::SMLERPTemplate.Properties.Resources.data_ok;
            this._statusDatabaseName.Name = "_statusDatabaseName";
            this._statusDatabaseName.Size = new System.Drawing.Size(107, 16);
            this._statusDatabaseName.Text = "Database name";
            this._statusDatabaseName.ToolTipText = "Database name";
            // 
            // _statusCompressWebservice
            // 
            this._statusCompressWebservice.BackColor = System.Drawing.Color.Transparent;
            this._statusCompressWebservice.ForeColor = System.Drawing.Color.White;
            this._statusCompressWebservice.Name = "_statusCompressWebservice";
            this._statusCompressWebservice.Size = new System.Drawing.Size(59, 16);
            this._statusCompressWebservice.Text = "Compress";
            this._statusCompressWebservice.ToolTipText = "Database name";
            // 
            // _statusWebserviceLabel
            // 
            this._statusWebserviceLabel.BackColor = System.Drawing.Color.Transparent;
            this._statusWebserviceLabel.ForeColor = System.Drawing.Color.White;
            this._statusWebserviceLabel.Image = global::SMLERPTemplate.Properties.Resources.server_client;
            this._statusWebserviceLabel.Name = "_statusWebserviceLabel";
            this._statusWebserviceLabel.Size = new System.Drawing.Size(172, 16);
            this._statusWebserviceLabel.Text = "Webservice connect status";
            this._statusWebserviceLabel.ToolTipText = "Webservice status";
            // 
            // _status
            // 
            this._status.BackColor = System.Drawing.Color.Transparent;
            this._status.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._status.ForeColor = System.Drawing.Color.White;
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(11, 14);
            this._status.Text = ".";
            // 
            // _toolBar
            // 
            this._toolBar.BackgroundImage = global::SMLERPTemplate.Properties.Resources.bt03;
            this._toolBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._toolBar.GripMargin = new System.Windows.Forms.Padding(0);
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._changeDate,
            this._showLeftMenu,
            this._misButton,
            this._screenSize,
            this.toolStripSeparator1,
            this._registerInfoLabel,
            this._syncDataButton});
            this._toolBar.Location = new System.Drawing.Point(324, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(603, 25);
            this._toolBar.TabIndex = 8;
            this._toolBar.Text = "Tool Bar";
            // 
            // _changeDate
            // 
            this._changeDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._changeDate.Image = global::SMLERPTemplate.Properties.Resources.clock;
            this._changeDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._changeDate.Name = "_changeDate";
            this._changeDate.Padding = new System.Windows.Forms.Padding(1);
            this._changeDate.ResourceName = "";
            this._changeDate.Size = new System.Drawing.Size(23, 22);
            this._changeDate.Click += new System.EventHandler(this._changeDate_Click);
            // 
            // _showLeftMenu
            // 
            this._showLeftMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._showLeftMenu.Image = global::SMLERPTemplate.Properties.Resources.window_sidebar1;
            this._showLeftMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._showLeftMenu.Name = "_showLeftMenu";
            this._showLeftMenu.Size = new System.Drawing.Size(23, 22);
            this._showLeftMenu.Text = "Show Left Menu";
            this._showLeftMenu.Click += new System.EventHandler(this._showLeftMenu_Click);
            // 
            // _misButton
            // 
            this._misButton.Image = global::SMLERPTemplate.Properties.Resources.chart;
            this._misButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._misButton.Name = "_misButton";
            this._misButton.Size = new System.Drawing.Size(47, 22);
            this._misButton.Text = "MIS";
            this._misButton.Click += new System.EventHandler(this._misButton_Click);
            // 
            // _screenSize
            // 
            this._screenSize.Image = ((System.Drawing.Image)(resources.GetObject("_screenSize.Image")));
            this._screenSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._screenSize.Name = "_screenSize";
            this._screenSize.Size = new System.Drawing.Size(83, 22);
            this._screenSize.Text = "1024*768";
            this._screenSize.Click += new System.EventHandler(this._screenSize_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _registerInfoLabel
            // 
            this._registerInfoLabel.Name = "_registerInfoLabel";
            this._registerInfoLabel.Size = new System.Drawing.Size(77, 22);
            this._registerInfoLabel.Text = "Register Info";
            // 
            // _syncDataButton
            // 
            this._syncDataButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._syncDataButton.Image = global::SMLERPTemplate.Properties.Resources.replace2;
            this._syncDataButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._syncDataButton.Name = "_syncDataButton";
            this._syncDataButton.Size = new System.Drawing.Size(23, 22);
            this._syncDataButton.Text = "SML Activesync is on";
            // 
            // _templateMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(927, 568);
            this.Controls.Add(this._tabControl);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this._toolBar);
            this.Controls.Add(this._menuBar);
            this.Controls.Add(this._menuPanel);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_templateMainForm";
            this.Text = "_templateMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._contextMenuBoardStrip.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip _contextMenuBoardStrip;
        private System.Windows.Forms.ToolStripMenuItem _newsToolStripMenuItem;
        private System.Windows.Forms.Timer _timerFindWebservice;
        protected System.Windows.Forms.MenuStrip _menuBar;
        private System.Windows.Forms.ToolStripStatusLabel _statusUserLabel;
        private System.Windows.Forms.ToolStripStatusLabel _statusProcess;
        private System.Windows.Forms.ToolStripStatusLabel _statusCompressWebservice;
        private System.Windows.Forms.ToolStripStatusLabel _statusDatabaseName;
        protected System.Windows.Forms.StatusStrip _statusStrip;
        protected System.Windows.Forms.ToolStripStatusLabel _statusUserList;
        private System.Windows.Forms.ToolStripStatusLabel _statusComputerInformation;
        private System.Windows.Forms.ToolStripStatusLabel _statusWebserviceLabel;
        public System.Windows.Forms.ToolStripStatusLabel _status;
        protected System.Windows.Forms.ToolStrip _toolBar;
        private System.Windows.Forms.ToolStripButton _showLeftMenu;
        protected System.Windows.Forms.Timer _timerStatus;
        public System.Windows.Forms.Panel _menuPanel;
        public System.Windows.Forms.TabPage Home;
        public MyLib.ToolStripMyButton _changeDate;
        public System.Windows.Forms.ImageList _imageList16x16;
        private System.Windows.Forms.ToolStripButton _screenSize;
        protected System.Windows.Forms.ToolStripButton _misButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripLabel _registerInfoLabel;
        public MyLib._myTabControl _tabControl;
        protected System.Windows.Forms.ToolStripButton _syncDataButton;
    }
}