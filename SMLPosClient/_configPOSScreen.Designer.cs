namespace SMLPosClient
{
    partial class _configPOSScreen
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
            this._myPanel1 = new MyLib._myPanel();
            this._tabPosConfig = new MyLib._myTabControl();
            this.tab_posClientConfig = new System.Windows.Forms.TabPage();
            this._posClientGeneralConfig = new SMLPosClient._posClientGeneralConfig();
            this.tab_pos_display = new System.Windows.Forms.TabPage();
            this._screenMoniter = new SMLPosClient._screen_pos_display();
            this.tab_posSlipFontConfig = new System.Windows.Forms.TabPage();
            this._screen_pos_configslipFont = new SMLPosClient._screen_pos_configslipFont();
            this.tab_pos_sound = new System.Windows.Forms.TabPage();
            this._screen_pos_sound_config = new SMLPosClient._screen_pos_sound_config();
            this.tab_customerDisplay = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._buttonClose = new MyLib.VistaButton();
            this._buttonSaveConfig = new MyLib.VistaButton();
            this._configCustomerDisplayControl1 = new SMLPosClient._configCustomerDisplayControl();
            this._myPanel1.SuspendLayout();
            this._tabPosConfig.SuspendLayout();
            this.tab_posClientConfig.SuspendLayout();
            this.tab_pos_display.SuspendLayout();
            this.tab_posSlipFontConfig.SuspendLayout();
            this.tab_pos_sound.SuspendLayout();
            this.tab_customerDisplay.SuspendLayout();
            this.panel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._tabPosConfig);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(647, 562);
            this._myPanel1.TabIndex = 0;
            // 
            // _tabPosConfig
            // 
            this._tabPosConfig.Controls.Add(this.tab_posClientConfig);
            this._tabPosConfig.Controls.Add(this.tab_pos_display);
            this._tabPosConfig.Controls.Add(this.tab_posSlipFontConfig);
            this._tabPosConfig.Controls.Add(this.tab_pos_sound);
            this._tabPosConfig.Controls.Add(this.tab_customerDisplay);
            this._tabPosConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabPosConfig.Font = new System.Drawing.Font("Tahoma", 9F);
            this._tabPosConfig.Location = new System.Drawing.Point(0, 43);
            this._tabPosConfig.Multiline = true;
            this._tabPosConfig.Name = "_tabPosConfig";
            this._tabPosConfig.SelectedIndex = 0;
            this._tabPosConfig.Size = new System.Drawing.Size(647, 480);
            this._tabPosConfig.TabIndex = 3;
            this._tabPosConfig.TableName = "";
            // 
            // tab_posClientConfig
            // 
            this.tab_posClientConfig.Controls.Add(this._posClientGeneralConfig);
            this.tab_posClientConfig.Location = new System.Drawing.Point(4, 23);
            this.tab_posClientConfig.Name = "tab_posClientConfig";
            this.tab_posClientConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tab_posClientConfig.Size = new System.Drawing.Size(639, 453);
            this.tab_posClientConfig.TabIndex = 0;
            this.tab_posClientConfig.Text = "1.tab_posClientConfig";
            this.tab_posClientConfig.UseVisualStyleBackColor = true;
            // 
            // _posClientGeneralConfig
            // 
            this._posClientGeneralConfig._isChange = false;
            this._posClientGeneralConfig.BackColor = System.Drawing.Color.Transparent;
            this._posClientGeneralConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this._posClientGeneralConfig.Font = new System.Drawing.Font("Tahoma", 9F);
            this._posClientGeneralConfig.Location = new System.Drawing.Point(3, 3);
            this._posClientGeneralConfig.Name = "_posClientGeneralConfig";
            this._posClientGeneralConfig.Size = new System.Drawing.Size(633, 447);
            this._posClientGeneralConfig.TabIndex = 0;
            // 
            // tab_pos_display
            // 
            this.tab_pos_display.Controls.Add(this._screenMoniter);
            this.tab_pos_display.Location = new System.Drawing.Point(4, 23);
            this.tab_pos_display.Name = "tab_pos_display";
            this.tab_pos_display.Padding = new System.Windows.Forms.Padding(3);
            this.tab_pos_display.Size = new System.Drawing.Size(639, 453);
            this.tab_pos_display.TabIndex = 1;
            this.tab_pos_display.Text = "2.tab_pos_display";
            this.tab_pos_display.UseVisualStyleBackColor = true;
            // 
            // _screenMoniter
            // 
            this._screenMoniter._isChange = false;
            this._screenMoniter.BackColor = System.Drawing.Color.Transparent;
            this._screenMoniter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screenMoniter.Font = new System.Drawing.Font("Tahoma", 9F);
            this._screenMoniter.Location = new System.Drawing.Point(3, 3);
            this._screenMoniter.Name = "_screenMoniter";
            this._screenMoniter.Size = new System.Drawing.Size(633, 447);
            this._screenMoniter.TabIndex = 2;
            // 
            // tab_posSlipFontConfig
            // 
            this.tab_posSlipFontConfig.Controls.Add(this._screen_pos_configslipFont);
            this.tab_posSlipFontConfig.Location = new System.Drawing.Point(4, 23);
            this.tab_posSlipFontConfig.Name = "tab_posSlipFontConfig";
            this.tab_posSlipFontConfig.Size = new System.Drawing.Size(639, 453);
            this.tab_posSlipFontConfig.TabIndex = 2;
            this.tab_posSlipFontConfig.Text = "3.tab_posSlipFontConfig";
            this.tab_posSlipFontConfig.UseVisualStyleBackColor = true;
            // 
            // _screen_pos_configslipFont
            // 
            this._screen_pos_configslipFont._isChange = false;
            this._screen_pos_configslipFont.BackColor = System.Drawing.Color.Transparent;
            this._screen_pos_configslipFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen_pos_configslipFont.Font = new System.Drawing.Font("Tahoma", 9F);
            this._screen_pos_configslipFont.Location = new System.Drawing.Point(0, 0);
            this._screen_pos_configslipFont.Name = "_screen_pos_configslipFont";
            this._screen_pos_configslipFont.Size = new System.Drawing.Size(639, 453);
            this._screen_pos_configslipFont.TabIndex = 0;
            // 
            // tab_pos_sound
            // 
            this.tab_pos_sound.Controls.Add(this._screen_pos_sound_config);
            this.tab_pos_sound.Location = new System.Drawing.Point(4, 23);
            this.tab_pos_sound.Name = "tab_pos_sound";
            this.tab_pos_sound.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.tab_pos_sound.Size = new System.Drawing.Size(639, 453);
            this.tab_pos_sound.TabIndex = 3;
            this.tab_pos_sound.Text = "4.tab_pos_sound";
            this.tab_pos_sound.UseVisualStyleBackColor = true;
            // 
            // _screen_pos_sound_config
            // 
            this._screen_pos_sound_config._isChange = false;
            this._screen_pos_sound_config.BackColor = System.Drawing.Color.Transparent;
            this._screen_pos_sound_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen_pos_sound_config.Location = new System.Drawing.Point(50, 0);
            this._screen_pos_sound_config.Name = "_screen_pos_sound_config";
            this._screen_pos_sound_config.Size = new System.Drawing.Size(589, 453);
            this._screen_pos_sound_config.TabIndex = 0;
            // 
            // tab_customerDisplay
            // 
            this.tab_customerDisplay.Controls.Add(this.panel1);
            this.tab_customerDisplay.Location = new System.Drawing.Point(4, 23);
            this.tab_customerDisplay.Name = "tab_customerDisplay";
            this.tab_customerDisplay.Size = new System.Drawing.Size(639, 453);
            this.tab_customerDisplay.TabIndex = 4;
            this.tab_customerDisplay.Text = "5.tab_customerDisplay";
            this.tab_customerDisplay.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._configCustomerDisplayControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 453);
            this.panel1.TabIndex = 0;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._myShadowLabel1);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(647, 43);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.EndColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(494, 5);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(150, 29);
            this._myShadowLabel1.StartColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.TabIndex = 0;
            this._myShadowLabel1.Text = "POS Screen";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._buttonClose);
            this._myFlowLayoutPanel2.Controls.Add(this._buttonSaveConfig);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 523);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(647, 39);
            this._myFlowLayoutPanel2.TabIndex = 1;
            // 
            // _buttonClose
            // 
            this._buttonClose._drawNewMethod = false;
            this._buttonClose.BackColor = System.Drawing.Color.Transparent;
            this._buttonClose.ButtonText = "Close";
            this._buttonClose.Location = new System.Drawing.Point(574, 8);
            this._buttonClose.myImage = global::SMLPosClient.Properties.Resources.delete2;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(70, 25);
            this._buttonClose.TabIndex = 0;
            this._buttonClose.Text = "vistaButton1";
            this._buttonClose.UseVisualStyleBackColor = false;
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _buttonSaveConfig
            // 
            this._buttonSaveConfig._drawNewMethod = false;
            this._buttonSaveConfig.BackColor = System.Drawing.Color.Transparent;
            this._buttonSaveConfig.ButtonText = "Save";
            this._buttonSaveConfig.Location = new System.Drawing.Point(498, 8);
            this._buttonSaveConfig.myImage = global::SMLPosClient.Properties.Resources.disk_blue;
            this._buttonSaveConfig.Name = "_buttonSaveConfig";
            this._buttonSaveConfig.Size = new System.Drawing.Size(70, 25);
            this._buttonSaveConfig.TabIndex = 1;
            this._buttonSaveConfig.Text = "vistaButton2";
            this._buttonSaveConfig.UseVisualStyleBackColor = false;
            this._buttonSaveConfig.Click += new System.EventHandler(this._buttonSaveConfig_Click);
            // 
            // _configCustomerDisplayControl1
            // 
            this._configCustomerDisplayControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._configCustomerDisplayControl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._configCustomerDisplayControl1.Location = new System.Drawing.Point(0, 0);
            this._configCustomerDisplayControl1.Name = "_configCustomerDisplayControl1";
            this._configCustomerDisplayControl1.Size = new System.Drawing.Size(639, 453);
            this._configCustomerDisplayControl1.TabIndex = 0;
            // 
            // _configPOSScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 562);
            this.Controls.Add(this._myPanel1);
            this.Name = "_configPOSScreen";
            this.Text = "_configPOSScreen";
            this._myPanel1.ResumeLayout(false);
            this._tabPosConfig.ResumeLayout(false);
            this.tab_posClientConfig.ResumeLayout(false);
            this.tab_pos_display.ResumeLayout(false);
            this.tab_posSlipFontConfig.ResumeLayout(false);
            this.tab_pos_sound.ResumeLayout(false);
            this.tab_customerDisplay.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib.VistaButton _buttonClose;
        private MyLib.VistaButton _buttonSaveConfig;
        private _screen_pos_display _screenMoniter;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib._myTabControl _tabPosConfig;
        private System.Windows.Forms.TabPage tab_posClientConfig;
        private System.Windows.Forms.TabPage tab_pos_display;
        private _posClientGeneralConfig _posClientGeneralConfig;
        private System.Windows.Forms.TabPage tab_posSlipFontConfig;
        private _screen_pos_configslipFont _screen_pos_configslipFont;
        private System.Windows.Forms.TabPage tab_pos_sound;
        private _screen_pos_sound_config _screen_pos_sound_config;
        private System.Windows.Forms.TabPage tab_customerDisplay;
        private System.Windows.Forms.Panel panel1;
        private _configCustomerDisplayControl _configCustomerDisplayControl1;
    }
}