namespace MyLib._databaseManage
{
    public partial class _selectDatabase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_selectDatabase));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("System Manage", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("User Manage (Provider)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Database Manage (Provider)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Tools", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("User and Group", 1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Group Manage", 2);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Resource Edit", 4);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Change Password", 3);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Group Permisions", 17);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("User Permissions", 16);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Database Group", 5);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Create new Database", 6);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Link Database", 7);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Database Access", 15);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Verify Database", 8);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("Database Information", 14);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("Transfer Database", 19);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("System Manage", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("Provider Config", 0);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("Webservice Setup", 0);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("Change Password", 3);
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("Register", 24);
            this._imageIconList = new System.Windows.Forms.ImageList(this.components);
            this._imageTabIconList = new System.Windows.Forms.ImageList(this.components);
            this._panel1 = new System.Windows.Forms.Panel();
            this._gridWebservice = new MyLib._myGrid();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._selectLanguage = new System.Windows.Forms.ToolStripDropDownButton();
            this._selectEnglishLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectThaiLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectMalayLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectChineseLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectTaiwanLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectIndonesianLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectLaoLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._emergencyButton = new MyLib.ToolStripMyButton();
            this._proxyButton = new System.Windows.Forms.ToolStripButton();
            this._keysToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._settingToolStripButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._helpButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._history = new System.Windows.Forms.ToolStripDropDownButton();
            this._logoPanel = new System.Windows.Forms.Panel();
            this._logoPicture = new System.Windows.Forms.PictureBox();
            this._loginTab = new MyLib._myTabControl();
            this._login = new System.Windows.Forms.TabPage();
            this._gridDatabaseList = new MyLib._myGrid();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._buttonLogin = new MyLib._myButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.@__profileLabel = new System.Windows.Forms.Label();
            this._profileNameTextBox = new System.Windows.Forms.TextBox();
            this._screenTop = new MyLib._myScreen();
            this._admin = new System.Windows.Forms.TabPage();
            this._listViewAdminMenu = new MyLib._listViewXP();
            this._columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._myFlowLayoutPanel3 = new MyLib._myFlowLayoutPanel();
            this._buttonLoginAdmin = new MyLib._myButton();
            this._screenTopAdmin = new MyLib._myScreen();
            this._server = new System.Windows.Forms.TabPage();
            this._listViewServerMenu = new MyLib._listViewXP();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._panel1.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this._logoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._logoPicture)).BeginInit();
            this._loginTab.SuspendLayout();
            this._login.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this._admin.SuspendLayout();
            this._myFlowLayoutPanel3.SuspendLayout();
            this._server.SuspendLayout();
            this.SuspendLayout();
            // 
            // _imageIconList
            // 
            this._imageIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageIconList.ImageStream")));
            this._imageIconList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageIconList.Images.SetKeyName(0, "");
            this._imageIconList.Images.SetKeyName(1, "");
            this._imageIconList.Images.SetKeyName(2, "");
            this._imageIconList.Images.SetKeyName(3, "");
            this._imageIconList.Images.SetKeyName(4, "");
            this._imageIconList.Images.SetKeyName(5, "");
            this._imageIconList.Images.SetKeyName(6, "");
            this._imageIconList.Images.SetKeyName(7, "");
            this._imageIconList.Images.SetKeyName(8, "");
            this._imageIconList.Images.SetKeyName(9, "");
            this._imageIconList.Images.SetKeyName(10, "");
            this._imageIconList.Images.SetKeyName(11, "");
            this._imageIconList.Images.SetKeyName(12, "");
            this._imageIconList.Images.SetKeyName(13, "");
            this._imageIconList.Images.SetKeyName(14, "");
            this._imageIconList.Images.SetKeyName(15, "data_lock.png");
            this._imageIconList.Images.SetKeyName(16, "user.png");
            this._imageIconList.Images.SetKeyName(17, "users.png");
            this._imageIconList.Images.SetKeyName(18, "document_into.png");
            this._imageIconList.Images.SetKeyName(19, "data_replace.png");
            this._imageIconList.Images.SetKeyName(20, "flash1.png");
            this._imageIconList.Images.SetKeyName(21, "data_view.png");
            this._imageIconList.Images.SetKeyName(22, "table_view.png");
            this._imageIconList.Images.SetKeyName(23, "preferences.png");
            this._imageIconList.Images.SetKeyName(24, "1261.png");
            // 
            // _imageTabIconList
            // 
            this._imageTabIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageTabIconList.ImageStream")));
            this._imageTabIconList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageTabIconList.Images.SetKeyName(0, "key1.png");
            this._imageTabIconList.Images.SetKeyName(1, "user1.png");
            this._imageTabIconList.Images.SetKeyName(2, "environment.png");
            // 
            // _panel1
            // 
            this._panel1.AutoSize = true;
            this._panel1.Controls.Add(this._gridWebservice);
            this._panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._panel1.Location = new System.Drawing.Point(0, 132);
            this._panel1.Name = "_panel1";
            this._panel1.Size = new System.Drawing.Size(610, 94);
            this._panel1.TabIndex = 39;
            // 
            // _gridWebservice
            // 
            this._gridWebservice._extraWordShow = true;
            this._gridWebservice._selectRow = -1;
            this._gridWebservice.BackColor = System.Drawing.SystemColors.Window;
            this._gridWebservice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridWebservice.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridWebservice.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridWebservice.Dock = System.Windows.Forms.DockStyle.Top;
            this._gridWebservice.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridWebservice.Location = new System.Drawing.Point(0, 0);
            this._gridWebservice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridWebservice.Name = "_gridWebservice";
            this._gridWebservice.Size = new System.Drawing.Size(610, 94);
            this._gridWebservice.TabIndex = 5;
            this._gridWebservice.TabStop = false;
            // 
            // _toolStrip
            // 
            this._toolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectLanguage,
            this._emergencyButton,
            this._proxyButton,
            this._keysToolStripButton,
            this._settingToolStripButton,
            this.toolStripSeparator1,
            this._helpButton,
            this.toolStripSeparator2,
            this._history});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(610, 25);
            this._toolStrip.TabIndex = 38;
            // 
            // _selectLanguage
            // 
            this._selectLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectEnglishLanguage,
            this._selectThaiLanguage,
            this._selectMalayLanguage,
            this._selectChineseLanguage,
            this._selectTaiwanLanguage,
            this._selectIndonesianLanguage,
            this._selectLaoLanguage});
            this._selectLanguage.Image = global::MyLib.Properties.Resources.earth;
            this._selectLanguage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectLanguage.Name = "_selectLanguage";
            this._selectLanguage.Size = new System.Drawing.Size(29, 22);
            this._selectLanguage.Click += new System.EventHandler(this._selectLanguage_Click);
            // 
            // _selectEnglishLanguage
            // 
            this._selectEnglishLanguage.Name = "_selectEnglishLanguage";
            this._selectEnglishLanguage.Size = new System.Drawing.Size(200, 22);
            this._selectEnglishLanguage.Text = "English (USA)";
            this._selectEnglishLanguage.Click += new System.EventHandler(this._selectEnglishLanguage_Click);
            // 
            // _selectThaiLanguage
            // 
            this._selectThaiLanguage.Name = "_selectThaiLanguage";
            this._selectThaiLanguage.Size = new System.Drawing.Size(200, 22);
            this._selectThaiLanguage.Text = "Thai";
            this._selectThaiLanguage.Click += new System.EventHandler(this._selectThaiLanguage_Click);
            // 
            // _selectMalayLanguage
            // 
            this._selectMalayLanguage.Name = "_selectMalayLanguage";
            this._selectMalayLanguage.Size = new System.Drawing.Size(200, 22);
            this._selectMalayLanguage.Text = "Malay (Malaysia)";
            this._selectMalayLanguage.Click += new System.EventHandler(this._selectMalayLanguage_Click);
            // 
            // _selectChineseLanguage
            // 
            this._selectChineseLanguage.Name = "_selectChineseLanguage";
            this._selectChineseLanguage.Size = new System.Drawing.Size(200, 22);
            this._selectChineseLanguage.Text = "Chinese (P.R. of China)";
            this._selectChineseLanguage.Click += new System.EventHandler(this._selectChineseLanguage_Click);
            // 
            // _selectTaiwanLanguage
            // 
            this._selectTaiwanLanguage.Name = "_selectTaiwanLanguage";
            this._selectTaiwanLanguage.Size = new System.Drawing.Size(200, 22);
            this._selectTaiwanLanguage.Text = "Chinese (Taiwan)";
            this._selectTaiwanLanguage.Click += new System.EventHandler(this._selectTaiwanLanguage_Click);
            // 
            // _selectIndonesianLanguage
            // 
            this._selectIndonesianLanguage.Name = "_selectIndonesianLanguage";
            this._selectIndonesianLanguage.Size = new System.Drawing.Size(200, 22);
            this._selectIndonesianLanguage.Text = "Indonesian";
            this._selectIndonesianLanguage.Click += new System.EventHandler(this._selectIndonesianLanguage_Click);
            // 
            // _selectLaoLanguage
            // 
            this._selectLaoLanguage.Name = "_selectLaoLanguage";
            this._selectLaoLanguage.Size = new System.Drawing.Size(200, 22);
            this._selectLaoLanguage.Text = "Lao";
            this._selectLaoLanguage.Click += new System.EventHandler(this._selectLaoLanguage_Click);
            // 
            // _emergencyButton
            // 
            this._emergencyButton.Image = global::MyLib.Properties.Resources.flashlight;
            this._emergencyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._emergencyButton.Name = "_emergencyButton";
            this._emergencyButton.Padding = new System.Windows.Forms.Padding(1);
            this._emergencyButton.ResourceName = "";
            this._emergencyButton.Size = new System.Drawing.Size(103, 22);
            this._emergencyButton.Text = "Get IP Server";
            this._emergencyButton.Click += new System.EventHandler(this._emergencyButton_Click);
            // 
            // _proxyButton
            // 
            this._proxyButton.Image = global::MyLib.Properties.Resources.server_lock;
            this._proxyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._proxyButton.Name = "_proxyButton";
            this._proxyButton.Size = new System.Drawing.Size(57, 22);
            this._proxyButton.Text = "Proxy";
            this._proxyButton.Click += new System.EventHandler(this._proxyButton_Click);
            // 
            // _keysToolStripButton
            // 
            this._keysToolStripButton.Image = global::MyLib.Properties.Resources.keys;
            this._keysToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._keysToolStripButton.Name = "_keysToolStripButton";
            this._keysToolStripButton.Size = new System.Drawing.Size(52, 22);
            this._keysToolStripButton.Text = "Keys";
            this._keysToolStripButton.Click += new System.EventHandler(this._keysToolStripButton_Click);
            // 
            // _settingToolStripButton
            // 
            this._settingToolStripButton.Image = global::MyLib.Properties.Resources.gear;
            this._settingToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._settingToolStripButton.Name = "_settingToolStripButton";
            this._settingToolStripButton.Padding = new System.Windows.Forms.Padding(1);
            this._settingToolStripButton.ResourceName = "";
            this._settingToolStripButton.Size = new System.Drawing.Size(63, 22);
            this._settingToolStripButton.Text = "Config";
            this._settingToolStripButton.Click += new System.EventHandler(this._settingToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _helpButton
            // 
            this._helpButton.Image = global::MyLib.Properties.Resources.help2;
            this._helpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._helpButton.Name = "_helpButton";
            this._helpButton.Size = new System.Drawing.Size(51, 22);
            this._helpButton.Text = "Help";
            this._helpButton.Click += new System.EventHandler(this._helpButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _history
            // 
            this._history.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._history.Image = global::MyLib.Properties.Resources.filesave;
            this._history.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._history.Name = "_history";
            this._history.Size = new System.Drawing.Size(29, 22);
            this._history.Text = "toolStripDropDownButton1";
            this._history.ToolTipText = "History";
            // 
            // _logoPanel
            // 
            this._logoPanel.BackColor = System.Drawing.Color.White;
            this._logoPanel.Controls.Add(this._logoPicture);
            this._logoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._logoPanel.Location = new System.Drawing.Point(0, 25);
            this._logoPanel.Name = "_logoPanel";
            this._logoPanel.Size = new System.Drawing.Size(610, 107);
            this._logoPanel.TabIndex = 6;
            // 
            // _logoPicture
            // 
            this._logoPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logoPicture.Image = global::MyLib.Properties.Resources.sml_account_logo;
            this._logoPicture.Location = new System.Drawing.Point(0, 0);
            this._logoPicture.Name = "_logoPicture";
            this._logoPicture.Size = new System.Drawing.Size(610, 107);
            this._logoPicture.TabIndex = 0;
            this._logoPicture.TabStop = false;
            // 
            // _loginTab
            // 
            this._loginTab.Controls.Add(this._login);
            this._loginTab.Controls.Add(this._admin);
            this._loginTab.Controls.Add(this._server);
            this._loginTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._loginTab.FixedName = true;
            this._loginTab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._loginTab.ImageList = this._imageTabIconList;
            this._loginTab.Location = new System.Drawing.Point(0, 226);
            this._loginTab.Margin = new System.Windows.Forms.Padding(2);
            this._loginTab.Multiline = true;
            this._loginTab.Name = "_loginTab";
            this._loginTab.SelectedIndex = 0;
            this._loginTab.Size = new System.Drawing.Size(610, 337);
            this._loginTab.TabIndex = 36;
            this._loginTab.TableName = "";
            // 
            // _login
            // 
            this._login.BackColor = System.Drawing.Color.Azure;
            this._login.Controls.Add(this._gridDatabaseList);
            this._login.Controls.Add(this._myFlowLayoutPanel2);
            this._login.Controls.Add(this._screenTop);
            this._login.ImageKey = "key1.png";
            this._login.Location = new System.Drawing.Point(4, 23);
            this._login.Margin = new System.Windows.Forms.Padding(2);
            this._login.Name = "_login";
            this._login.Padding = new System.Windows.Forms.Padding(2);
            this._login.Size = new System.Drawing.Size(602, 310);
            this._login.TabIndex = 0;
            this._login.Text = "Login";
            this._login.UseVisualStyleBackColor = true;
            this._login.Click += new System.EventHandler(this.login_Click);
            // 
            // _gridDatabaseList
            // 
            this._gridDatabaseList._extraWordShow = true;
            this._gridDatabaseList._selectRow = -1;
            this._gridDatabaseList.BackColor = System.Drawing.SystemColors.Window;
            this._gridDatabaseList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridDatabaseList.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridDatabaseList.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridDatabaseList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridDatabaseList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridDatabaseList.Location = new System.Drawing.Point(2, 34);
            this._gridDatabaseList.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._gridDatabaseList.Name = "_gridDatabaseList";
            this._gridDatabaseList.Size = new System.Drawing.Size(598, 274);
            this._gridDatabaseList.TabIndex = 28;
            this._gridDatabaseList.TabStop = false;
            this._gridDatabaseList.Load += new System.EventHandler(this._gridDatabaseList_Load);
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._buttonLogin);
            this._myFlowLayoutPanel2.Controls.Add(this.flowLayoutPanel1);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(598, 32);
            this._myFlowLayoutPanel2.TabIndex = 34;
            // 
            // _buttonLogin
            // 
            this._buttonLogin._drawNewMethod = false;
            this._buttonLogin.AutoSize = true;
            this._buttonLogin.BackColor = System.Drawing.Color.Transparent;
            this._buttonLogin.ButtonText = "login";
            this._buttonLogin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._buttonLogin.Location = new System.Drawing.Point(534, 3);
            this._buttonLogin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._buttonLogin.myImage = global::MyLib.Resource16x16.key1;
            this._buttonLogin.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonLogin.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonLogin.myUseVisualStyleBackColor = false;
            this._buttonLogin.Name = "_buttonLogin";
            this._buttonLogin.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._buttonLogin.ResourceName = "login";
            this._buttonLogin.Size = new System.Drawing.Size(62, 24);
            this._buttonLogin.TabIndex = 29;
            this._buttonLogin.Text = "login";
            this._buttonLogin.UseVisualStyleBackColor = false;
            this._buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.@__profileLabel);
            this.flowLayoutPanel1.Controls.Add(this._profileNameTextBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(332, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 32);
            this.flowLayoutPanel1.TabIndex = 32;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // __profileLabel
            // 
            this.@__profileLabel.AutoSize = true;
            this.@__profileLabel.Location = new System.Drawing.Point(3, 8);
            this.@__profileLabel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.@__profileLabel.Name = "__profileLabel";
            this.@__profileLabel.Size = new System.Drawing.Size(48, 14);
            this.@__profileLabel.TabIndex = 31;
            this.@__profileLabel.Text = "Profile :";
            // 
            // _profileNameTextBox
            // 
            this._profileNameTextBox.Location = new System.Drawing.Point(57, 3);
            this._profileNameTextBox.Name = "_profileNameTextBox";
            this._profileNameTextBox.Size = new System.Drawing.Size(134, 22);
            this._profileNameTextBox.TabIndex = 30;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(2, 2);
            this._screenTop.Margin = new System.Windows.Forms.Padding(2);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(598, 0);
            this._screenTop.TabIndex = 33;
            // 
            // _admin
            // 
            this._admin.BackColor = System.Drawing.Color.Azure;
            this._admin.Controls.Add(this._listViewAdminMenu);
            this._admin.Controls.Add(this._myFlowLayoutPanel3);
            this._admin.Controls.Add(this._screenTopAdmin);
            this._admin.ImageKey = "user1.png";
            this._admin.Location = new System.Drawing.Point(4, 23);
            this._admin.Margin = new System.Windows.Forms.Padding(2);
            this._admin.Name = "_admin";
            this._admin.Padding = new System.Windows.Forms.Padding(2);
            this._admin.Size = new System.Drawing.Size(602, 310);
            this._admin.TabIndex = 1;
            this._admin.Text = "Admin";
            this._admin.UseVisualStyleBackColor = true;
            // 
            // _listViewAdminMenu
            // 
            this._listViewAdminMenu.BackColor = System.Drawing.Color.MintCream;
            this._listViewAdminMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._listViewAdminMenu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnHeader1});
            this._listViewAdminMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listViewAdminMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._listViewAdminMenu.ForeColor = System.Drawing.Color.Navy;
            listViewGroup1.Header = "System Manage";
            listViewGroup1.Name = "system_manage";
            listViewGroup2.Header = "User Manage (Provider)";
            listViewGroup2.Name = "user_manage";
            listViewGroup3.Header = "Database Manage (Provider)";
            listViewGroup3.Name = "database_manage";
            listViewGroup4.Header = "Tools";
            listViewGroup4.Name = "Tools";
            this._listViewAdminMenu.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            listViewItem1.Group = listViewGroup2;
            listViewItem1.StateImageIndex = 0;
            listViewItem1.Tag = "user_and_group";
            listViewItem2.Group = listViewGroup2;
            listViewItem2.Tag = "group_manage";
            listViewItem3.Group = listViewGroup2;
            listViewItem3.Tag = "resource_edit";
            listViewItem4.Group = listViewGroup2;
            listViewItem4.Tag = "admin_change_password";
            listViewItem5.Group = listViewGroup2;
            listViewItem5.Tag = "group_permissions";
            listViewItem6.Group = listViewGroup2;
            listViewItem6.Tag = "user_permissions";
            listViewItem7.Group = listViewGroup3;
            listViewItem7.Tag = "database_group";
            listViewItem8.Group = listViewGroup3;
            listViewItem8.Tag = "create_new_database";
            listViewItem9.Group = listViewGroup3;
            listViewItem9.StateImageIndex = 0;
            listViewItem9.Tag = "link_database";
            listViewItem10.Group = listViewGroup3;
            listViewItem10.Tag = "database_access";
            listViewItem11.Group = listViewGroup3;
            listViewItem11.Tag = "verify_database";
            listViewItem12.Checked = true;
            listViewItem12.Group = listViewGroup3;
            listViewItem12.StateImageIndex = 9;
            listViewItem12.Tag = "database_information";
            listViewItem13.Group = listViewGroup3;
            listViewItem13.Tag = "transfer_database";
            this._listViewAdminMenu.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13});
            this._listViewAdminMenu.LargeImageList = this._imageIconList;
            this._listViewAdminMenu.Location = new System.Drawing.Point(2, 32);
            this._listViewAdminMenu.Margin = new System.Windows.Forms.Padding(0);
            this._listViewAdminMenu.MultiSelect = false;
            this._listViewAdminMenu.Name = "_listViewAdminMenu";
            this._listViewAdminMenu.ShowItemToolTips = true;
            this._listViewAdminMenu.Size = new System.Drawing.Size(598, 276);
            this._listViewAdminMenu.TabIndex = 2;
            this._listViewAdminMenu.UseCompatibleStateImageBehavior = false;
            this._listViewAdminMenu.SelectedIndexChanged += new System.EventHandler(this.listViewAdminMenu_SelectedIndexChanged);
            // 
            // _myFlowLayoutPanel3
            // 
            this._myFlowLayoutPanel3.AutoSize = true;
            this._myFlowLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel3.Controls.Add(this._buttonLoginAdmin);
            this._myFlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel3.Location = new System.Drawing.Point(2, 2);
            this._myFlowLayoutPanel3.Name = "_myFlowLayoutPanel3";
            this._myFlowLayoutPanel3.Size = new System.Drawing.Size(598, 30);
            this._myFlowLayoutPanel3.TabIndex = 4;
            // 
            // _buttonLoginAdmin
            // 
            this._buttonLoginAdmin._drawNewMethod = false;
            this._buttonLoginAdmin.AutoSize = true;
            this._buttonLoginAdmin.BackColor = System.Drawing.Color.Transparent;
            this._buttonLoginAdmin.ButtonText = "login";
            this._buttonLoginAdmin.Location = new System.Drawing.Point(534, 2);
            this._buttonLoginAdmin.Margin = new System.Windows.Forms.Padding(2);
            this._buttonLoginAdmin.myImage = global::MyLib.Resource16x16.key1;
            this._buttonLoginAdmin.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonLoginAdmin.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonLoginAdmin.myUseVisualStyleBackColor = false;
            this._buttonLoginAdmin.Name = "_buttonLoginAdmin";
            this._buttonLoginAdmin.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._buttonLoginAdmin.ResourceName = "login";
            this._buttonLoginAdmin.Size = new System.Drawing.Size(62, 26);
            this._buttonLoginAdmin.TabIndex = 1;
            this._buttonLoginAdmin.Text = "login";
            this._buttonLoginAdmin.UseVisualStyleBackColor = false;
            this._buttonLoginAdmin.Click += new System.EventHandler(this.buttonLoginAdmin_Click_1);
            // 
            // _screenTopAdmin
            // 
            this._screenTopAdmin._isChange = false;
            this._screenTopAdmin.AutoSize = true;
            this._screenTopAdmin.BackColor = System.Drawing.Color.Transparent;
            this._screenTopAdmin.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTopAdmin.Location = new System.Drawing.Point(2, 2);
            this._screenTopAdmin.Margin = new System.Windows.Forms.Padding(2);
            this._screenTopAdmin.Name = "_screenTopAdmin";
            this._screenTopAdmin.Size = new System.Drawing.Size(598, 0);
            this._screenTopAdmin.TabIndex = 0;
            // 
            // _server
            // 
            this._server.Controls.Add(this._listViewServerMenu);
            this._server.ImageKey = "environment.png";
            this._server.Location = new System.Drawing.Point(4, 23);
            this._server.Name = "_server";
            this._server.Size = new System.Drawing.Size(602, 310);
            this._server.TabIndex = 2;
            this._server.Text = "Server Setup";
            this._server.UseVisualStyleBackColor = true;
            // 
            // _listViewServerMenu
            // 
            this._listViewServerMenu.BackColor = System.Drawing.Color.MintCream;
            this._listViewServerMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._listViewServerMenu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this._listViewServerMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listViewServerMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._listViewServerMenu.ForeColor = System.Drawing.Color.Navy;
            listViewGroup5.Header = "System Manage";
            listViewGroup5.Name = "system_manage";
            this._listViewServerMenu.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup5});
            listViewItem14.Group = listViewGroup5;
            listViewItem14.Tag = "provider_config";
            listViewItem15.Group = listViewGroup5;
            listViewItem15.Tag = "webservice_setup";
            listViewItem16.Group = listViewGroup5;
            listViewItem16.Tag = "server_change_password";
            listViewItem17.Group = listViewGroup5;
            listViewItem17.Tag = "register";
            this._listViewServerMenu.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17});
            this._listViewServerMenu.LargeImageList = this._imageIconList;
            this._listViewServerMenu.Location = new System.Drawing.Point(0, 0);
            this._listViewServerMenu.Margin = new System.Windows.Forms.Padding(0);
            this._listViewServerMenu.MultiSelect = false;
            this._listViewServerMenu.Name = "_listViewServerMenu";
            this._listViewServerMenu.ShowItemToolTips = true;
            this._listViewServerMenu.Size = new System.Drawing.Size(602, 310);
            this._listViewServerMenu.TabIndex = 3;
            this._listViewServerMenu.UseCompatibleStateImageBehavior = false;
            this._listViewServerMenu.SelectedIndexChanged += new System.EventHandler(this._listViewXP1_SelectedIndexChanged);
            // 
            // _selectDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(610, 563);
            this.Controls.Add(this._loginTab);
            this.Controls.Add(this._panel1);
            this.Controls.Add(this._logoPanel);
            this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_selectDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this._selectDatabase_Load);
            this._panel1.ResumeLayout(false);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._logoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._logoPicture)).EndInit();
            this._loginTab.ResumeLayout(false);
            this._login.ResumeLayout(false);
            this._login.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this._admin.ResumeLayout(false);
            this._admin.PerformLayout();
            this._myFlowLayoutPanel3.ResumeLayout(false);
            this._myFlowLayoutPanel3.PerformLayout();
            this._server.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList _imageIconList;
		private System.Windows.Forms.TabPage _login;
		private _myGrid _gridDatabaseList;
		private _myFlowLayoutPanel _myFlowLayoutPanel2;
		private _myButton _buttonLogin;
		private _myScreen _screenTop;
		private System.Windows.Forms.TabPage _admin;
		private _listViewXP _listViewAdminMenu;
		private System.Windows.Forms.ColumnHeader _columnHeader1;
		private _myFlowLayoutPanel _myFlowLayoutPanel3;
		private _myButton _buttonLoginAdmin;
        private _myScreen _screenTopAdmin;
        private System.Windows.Forms.ImageList _imageTabIconList;
        private _myGrid _gridWebservice;
        private System.Windows.Forms.Panel _panel1;
        private System.Windows.Forms.TabPage _server;
        private _listViewXP _listViewServerMenu;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton _selectLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectEnglishLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectThaiLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectMalayLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectChineseLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectTaiwanLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectIndonesianLanguage;
        private System.Windows.Forms.ToolStripButton _proxyButton;
        private System.Windows.Forms.ToolStripButton _keysToolStripButton;
        public _myTabControl _loginTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _helpButton;
        public System.Windows.Forms.Panel _logoPanel;
        private System.Windows.Forms.PictureBox _logoPicture;
        private ToolStripMyButton _emergencyButton;
        private System.Windows.Forms.ToolStripMenuItem _selectLaoLanguage;
        private ToolStripMyButton _settingToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton _history;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label __profileLabel;
        private System.Windows.Forms.TextBox _profileNameTextBox;
    }
}
