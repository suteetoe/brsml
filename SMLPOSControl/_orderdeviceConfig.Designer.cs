namespace SMLPOSControl
{
    partial class _orderdeviceConfig
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
            this._flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._myPanel1 = new MyLib._myPanel();
            this._orderDeviceConfigScreen1 = new SMLPOSControl._orderDeviceConfigScreen();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._buttonClose = new MyLib.VistaButton();
            this._buttonSaveConfig = new MyLib.VistaButton();
            this._myTabControl1 = new MyLib._myTabControl();
            this.tab_order_device_config = new System.Windows.Forms.TabPage();
            this.tab_pos_slip_font_config = new System.Windows.Forms.TabPage();
            this._myPanel2 = new MyLib._myPanel();
            this._orderFontSlipScreen1 = new SMLPOSControl._orderFontSlipScreen();
            this._flowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myTabControl1.SuspendLayout();
            this.tab_order_device_config.SuspendLayout();
            this.tab_pos_slip_font_config.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _flowLayoutPanel1
            // 
            this._flowLayoutPanel1.AutoSize = true;
            this._flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._flowLayoutPanel1.Controls.Add(this._myShadowLabel1);
            this._flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._flowLayoutPanel1.Name = "_flowLayoutPanel1";
            this._flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._flowLayoutPanel1.Size = new System.Drawing.Size(447, 43);
            this._flowLayoutPanel1.TabIndex = 9;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myShadowLabel1.DrawGradient = false;
            this._myShadowLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(38, 5);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(406, 33);
            this._myShadowLabel1.StartColor = System.Drawing.Color.White;
            this._myShadowLabel1.TabIndex = 7;
            this._myShadowLabel1.Text = "กำหนดคุณสมบัติเครื่องสั่งอาหาร";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._orderDeviceConfigScreen1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(3, 3);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(433, 376);
            this._myPanel1.TabIndex = 12;
            // 
            // _orderDeviceConfigScreen1
            // 
            this._orderDeviceConfigScreen1._isChange = false;
            this._orderDeviceConfigScreen1.BackColor = System.Drawing.Color.Transparent;
            this._orderDeviceConfigScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderDeviceConfigScreen1.Location = new System.Drawing.Point(0, 0);
            this._orderDeviceConfigScreen1.Name = "_orderDeviceConfigScreen1";
            this._orderDeviceConfigScreen1.Size = new System.Drawing.Size(433, 376);
            this._orderDeviceConfigScreen1.TabIndex = 10;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonClose);
            this._myFlowLayoutPanel1.Controls.Add(this._buttonSaveConfig);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 451);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(447, 36);
            this._myFlowLayoutPanel1.TabIndex = 8;
            // 
            // _buttonClose
            // 
            this._buttonClose._drawNewMethod = false;
            this._buttonClose.BackColor = System.Drawing.Color.Transparent;
            this._buttonClose.ButtonText = "Close";
            this._buttonClose.Location = new System.Drawing.Point(374, 3);
            this._buttonClose.myImage = global::SMLPOSControl.Properties.Resources.error1;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(70, 25);
            this._buttonClose.TabIndex = 2;
            this._buttonClose.Text = "vistaButton1";
            this._buttonClose.UseVisualStyleBackColor = false;
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _buttonSaveConfig
            // 
            this._buttonSaveConfig._drawNewMethod = false;
            this._buttonSaveConfig.BackColor = System.Drawing.Color.Transparent;
            this._buttonSaveConfig.ButtonText = "Save";
            this._buttonSaveConfig.Location = new System.Drawing.Point(298, 3);
            this._buttonSaveConfig.myImage = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._buttonSaveConfig.Name = "_buttonSaveConfig";
            this._buttonSaveConfig.Size = new System.Drawing.Size(70, 25);
            this._buttonSaveConfig.TabIndex = 3;
            this._buttonSaveConfig.Text = "vistaButton2";
            this._buttonSaveConfig.UseVisualStyleBackColor = false;
            this._buttonSaveConfig.Click += new System.EventHandler(this._buttonSaveConfig_Click);
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.tab_order_device_config);
            this._myTabControl1.Controls.Add(this.tab_pos_slip_font_config);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Location = new System.Drawing.Point(0, 43);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.Size = new System.Drawing.Size(447, 408);
            this._myTabControl1.TabIndex = 11;
            this._myTabControl1.TableName = "order_device_id";
            // 
            // tab_order_device_config
            // 
            this.tab_order_device_config.Controls.Add(this._myPanel1);
            this.tab_order_device_config.Location = new System.Drawing.Point(4, 22);
            this.tab_order_device_config.Name = "tab_order_device_config";
            this.tab_order_device_config.Padding = new System.Windows.Forms.Padding(3);
            this.tab_order_device_config.Size = new System.Drawing.Size(439, 382);
            this.tab_order_device_config.TabIndex = 0;
            this.tab_order_device_config.Text = "tab_order_device_config";
            this.tab_order_device_config.UseVisualStyleBackColor = true;
            // 
            // tab_pos_slip_font_config
            // 
            this.tab_pos_slip_font_config.Controls.Add(this._myPanel2);
            this.tab_pos_slip_font_config.Location = new System.Drawing.Point(4, 22);
            this.tab_pos_slip_font_config.Name = "tab_pos_slip_font_config";
            this.tab_pos_slip_font_config.Padding = new System.Windows.Forms.Padding(3);
            this.tab_pos_slip_font_config.Size = new System.Drawing.Size(439, 382);
            this.tab_pos_slip_font_config.TabIndex = 1;
            this.tab_pos_slip_font_config.Text = "tab_pos_slip_font_config";
            this.tab_pos_slip_font_config.UseVisualStyleBackColor = true;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._orderFontSlipScreen1);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(3, 3);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.ShowLineBackground = true;
            this._myPanel2.Size = new System.Drawing.Size(433, 376);
            this._myPanel2.TabIndex = 0;
            // 
            // _orderFontSlipScreen1
            // 
            this._orderFontSlipScreen1._isChange = false;
            this._orderFontSlipScreen1.BackColor = System.Drawing.Color.Transparent;
            this._orderFontSlipScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderFontSlipScreen1.Location = new System.Drawing.Point(0, 0);
            this._orderFontSlipScreen1.Name = "_orderFontSlipScreen1";
            this._orderFontSlipScreen1.Size = new System.Drawing.Size(433, 376);
            this._orderFontSlipScreen1.TabIndex = 0;
            // 
            // _orderdeviceConfig
            // 
            this._colorBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._colorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(447, 487);
            this.Controls.Add(this._myTabControl1);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Controls.Add(this._flowLayoutPanel1);
            this.Name = "_orderdeviceConfig";
            this.ResourceName = "กำหนดคุณสมบัติเครื่องสั่งอาหาร";
            this.Text = "กำหนดคุณสมบัติเครื่องสั่งอาหาร";
            this._flowLayoutPanel1.ResumeLayout(false);
            this._flowLayoutPanel1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myTabControl1.ResumeLayout(false);
            this.tab_order_device_config.ResumeLayout(false);
            this.tab_order_device_config.PerformLayout();
            this.tab_pos_slip_font_config.ResumeLayout(false);
            this._myPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel1;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _buttonClose;
        private MyLib.VistaButton _buttonSaveConfig;
        private _orderDeviceConfigScreen _orderDeviceConfigScreen1;
        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage tab_order_device_config;
        private System.Windows.Forms.TabPage tab_pos_slip_font_config;
        private MyLib._myPanel _myPanel2;
        private _orderFontSlipScreen _orderFontSlipScreen1;


    }
}