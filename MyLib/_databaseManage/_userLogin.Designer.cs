namespace MyLib._databaseManage
{
    partial class _userLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_userLogin));
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._selectLanguage = new System.Windows.Forms.ToolStripDropDownButton();
            this._selectEnglishLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectThaiLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectMalayLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectChineseLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectTaiwanLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._selectIndonesianLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this._emergencyButton = new MyLib.ToolStripMyButton();
            this._proxyButton = new System.Windows.Forms.ToolStripButton();
            this._webServiceConfigButton = new MyLib.ToolStripMyButton();
            this._keysToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._helpButton = new System.Windows.Forms.ToolStripButton();
            this._myPanel1 = new MyLib._myPanel();
            this._loginButton = new MyLib.VistaButton();
            this._screenTop = new MyLib._databaseManage._userLoginScreen();
            this.panel1 = new System.Windows.Forms.Panel();
            this._fancyLabel1 = new MyLib._fancyLabel(this.components);
            this._toolStrip.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip
            // 
            this._toolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectLanguage,
            this._emergencyButton,
            this._proxyButton,
            this._webServiceConfigButton,
            this._keysToolStripButton,
            this.toolStripSeparator1,
            this._helpButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(490, 25);
            this._toolStrip.TabIndex = 39;
            // 
            // _selectLanguage
            // 
            this._selectLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectEnglishLanguage,
            this._selectThaiLanguage,
            this._selectMalayLanguage,
            this._selectChineseLanguage,
            this._selectTaiwanLanguage,
            this._selectIndonesianLanguage});
            this._selectLanguage.Image = global::MyLib.Properties.Resources.earth;
            this._selectLanguage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectLanguage.Name = "_selectLanguage";
            this._selectLanguage.Size = new System.Drawing.Size(29, 22);
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
            // _emergencyButton
            // 
            this._emergencyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._emergencyButton.Image = global::MyLib.Properties.Resources.flashlight;
            this._emergencyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._emergencyButton.Name = "_emergencyButton";
            this._emergencyButton.Padding = new System.Windows.Forms.Padding(1);
            this._emergencyButton.ResourceName = "";
            this._emergencyButton.Size = new System.Drawing.Size(23, 22);
            this._emergencyButton.Text = "โหมดฉุกเฉิน";
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
            // _webServiceConfigButton
            // 
            this._webServiceConfigButton.Image = global::MyLib.Properties.Resources.gear_connection;
            this._webServiceConfigButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._webServiceConfigButton.Name = "_webServiceConfigButton";
            this._webServiceConfigButton.Padding = new System.Windows.Forms.Padding(1);
            this._webServiceConfigButton.ResourceName = "";
            this._webServiceConfigButton.Size = new System.Drawing.Size(89, 22);
            this._webServiceConfigButton.Text = "Perference";
            this._webServiceConfigButton.Click += new System.EventHandler(this._webServiceConfigButton_Click);
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
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.White;
            this._myPanel1.Controls.Add(this._loginButton);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.Controls.Add(this.panel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(490, 165);
            this._myPanel1.TabIndex = 41;
            // 
            // _loginButton
            // 
            this._loginButton._drawNewMethod = false;
            this._loginButton.BackColor = System.Drawing.Color.Transparent;
            this._loginButton.ButtonColor = System.Drawing.Color.DeepSkyBlue;
            this._loginButton.ButtonText = "เข้าสู่ระบบ";
            this._loginButton.Location = new System.Drawing.Point(386, 129);
            this._loginButton.myImage = global::MyLib.Properties.Resources._lock;
            this._loginButton.Name = "_loginButton";
            this._loginButton.Size = new System.Drawing.Size(93, 26);
            this._loginButton.TabIndex = 2;
            this._loginButton.UseVisualStyleBackColor = false;
            this._loginButton.Click += new System.EventHandler(this._loginButton_Click);
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(10, 79);
            this._screenTop.Margin = new System.Windows.Forms.Padding(0);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(470, 54);
            this._screenTop.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._fancyLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 79);
            this.panel1.TabIndex = 1;
            // 
            // _fancyLabel1
            // 
            this._fancyLabel1._blurAmount = 8;
            this._fancyLabel1._BlurColor = System.Drawing.Color.LimeGreen;
            this._fancyLabel1.BackColor = System.Drawing.Color.Transparent;
            this._fancyLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fancyLabel1.Font = new System.Drawing.Font("Times New Roman", 38.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._fancyLabel1.ForeColor = System.Drawing.Color.RoyalBlue;
            this._fancyLabel1.Location = new System.Drawing.Point(0, 0);
            this._fancyLabel1.Name = "_fancyLabel1";
            this._fancyLabel1.Size = new System.Drawing.Size(470, 79);
            this._fancyLabel1.TabIndex = 0;
            this._fancyLabel1.Text = "SML POS Starter";
            this._fancyLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _userLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 190);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_userLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStrip;
        protected System.Windows.Forms.ToolStripDropDownButton _selectLanguage; 
        private System.Windows.Forms.ToolStripMenuItem _selectEnglishLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectThaiLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectMalayLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectChineseLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectTaiwanLanguage;
        private System.Windows.Forms.ToolStripMenuItem _selectIndonesianLanguage;
        protected System.Windows.Forms.ToolStripButton _proxyButton;
        protected System.Windows.Forms.ToolStripButton _keysToolStripButton;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripButton _helpButton;
        private System.Windows.Forms.Panel panel1;
        protected VistaButton _loginButton;
        private ToolStripMyButton _webServiceConfigButton;
        public _fancyLabel _fancyLabel1;
        public _userLoginScreen _screenTop;
        public ToolStripMyButton _emergencyButton;
        protected _myPanel _myPanel1;
    }
}