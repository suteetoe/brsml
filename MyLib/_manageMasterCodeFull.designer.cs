namespace MyLib
{
	partial class _manageMasterCodeFull
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this._manageDataScreen = new MyLib._myManageData();
            this._myPanel = new MyLib._myPanel();
            this._webBrowser = new System.Windows.Forms.WebBrowser();
            //this._panel1 = new MyLib._myPanel();
            this._extraPanel = new System.Windows.Forms.Panel();
            this._inputScreen = new MyLib._managerMasterScreenClass();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._labelTitle = new MyLib._myShadowLabel(this.components);
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._manageDataScreen._form2.SuspendLayout();
            this._manageDataScreen.SuspendLayout();
            this._myPanel.SuspendLayout();
            //this._panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _manageDataScreen
            // 
            this._manageDataScreen._mainMenuCode = "";
            this._manageDataScreen._mainMenuId = "";
            this._manageDataScreen.BackColor = System.Drawing.Color.WhiteSmoke;
            this._manageDataScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._manageDataScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._manageDataScreen.Location = new System.Drawing.Point(0, 0);
            this._manageDataScreen.Name = "_manageDataScreen";
            // 
            // _manageDataScreen.Panel1
            // 
            this._manageDataScreen._form1.BackColor = System.Drawing.Color.LightCyan;
            // 
            // _manageDataScreen.Panel2
            // 
            this._manageDataScreen._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._manageDataScreen._form2.Controls.Add(this._myPanel);
            this._manageDataScreen._form2.Controls.Add(this.flowLayoutPanel1);
            this._manageDataScreen._form2.Controls.Add(this._toolBar);
            this._manageDataScreen.Size = new System.Drawing.Size(898, 699);
            this._manageDataScreen.TabIndex = 1;
            this._manageDataScreen.TabStop = false;
            // 
            // _myPanel
            // 
            this._myPanel._switchTabAuto = false;
            this._myPanel.AutoSize = true;
            this._myPanel.BeginColor = System.Drawing.Color.White;
            this._myPanel.Controls.Add(this._webBrowser);
            //this._myPanel.Controls.Add(this._panel1);
            this._myPanel.Controls.Add(this._extraPanel);
            this._myPanel.Controls.Add(this._inputScreen);

            this._myPanel.CornerPicture = null;
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel.EndColor = System.Drawing.Color.WhiteSmoke;
            this._myPanel.Location = new System.Drawing.Point(0, 52);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this._myPanel.Size = new System.Drawing.Size(684, 645);
            this._myPanel.TabIndex = 15;
            // 
            // _webBrowser
            // 
            this._webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._webBrowser.Location = new System.Drawing.Point(5, 4);
            this._webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowser.Name = "_webBrowser";
            this._webBrowser.Size = new System.Drawing.Size(674, 637);
            this._webBrowser.TabIndex = 15;
            // 
            // _panel1
            // 
            //this._panel1.AutoSize = true;
            //this._panel1.BackColor = System.Drawing.Color.Transparent;
            //this._panel1.Controls.Add(this._extraPanel);
            //this._panel1.Controls.Add(this._inputScreen);
            //this._panel1.Dock = System.Windows.Forms.DockStyle.Top;
            //this._panel1.Location = new System.Drawing.Point(5, 4);
            //this._panel1.Name = "_panel1";
            //this._panel1.Size = new System.Drawing.Size(674, 0);
            //this._panel1.TabIndex = 17;
            // 
            // _extraPanel
            // 
            this._extraPanel.AutoSize = true;
            this._extraPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._extraPanel.Location = new System.Drawing.Point(0, 0);
            this._extraPanel.Name = "_extraPanel";
            this._extraPanel.Size = new System.Drawing.Size(674, 0);
            this._extraPanel.TabIndex = 16;
            // 
            // _inputScreen
            // 
            this._inputScreen._isChange = false;
            this._inputScreen.AutoSize = true;
            this._inputScreen.BackColor = System.Drawing.Color.Transparent;
            this._inputScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._inputScreen.Location = new System.Drawing.Point(0, 0);
            this._inputScreen.Name = "_inputScreen";
            this._inputScreen.Size = new System.Drawing.Size(674, 0);
            this._inputScreen.TabIndex = 14;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.flowLayoutPanel1.Controls.Add(this._labelTitle);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(684, 27);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // _labelTitle
            // 
            this._labelTitle.Angle = 0F;
            this._labelTitle.AutoSize = true;
            this._labelTitle.DrawGradient = false;
            this._labelTitle.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this._labelTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._labelTitle.ForeColor = System.Drawing.Color.Turquoise;
            this._labelTitle.Location = new System.Drawing.Point(580, 4);
            this._labelTitle.Name = "_labelTitle";
            this._labelTitle.ShadowColor = System.Drawing.Color.Black;
            this._labelTitle.Size = new System.Drawing.Size(91, 19);
            this._labelTitle.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this._labelTitle.TabIndex = 15;
            this._labelTitle.Text = "LabelTitle";
            this._labelTitle.XOffset = 1F;
            this._labelTitle.YOffset = 1F;
            // 
            // _toolBar
            // 
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this.toolStripSeparator1,
            this._buttonClose});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(684, 25);
            this._toolBar.TabIndex = 13;
            this._toolBar.Text = "toolStrip2";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::MyLib.Properties.Resources.filesave;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "บันทึก (F12)";
            this._buttonSave.Size = new System.Drawing.Size(87, 22);
            this._buttonSave.Text = "บันทึก (F12)";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::MyLib.Properties.Resources.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Size = new System.Drawing.Size(75, 22);
            this._buttonClose.Text = "ปิดหน้าจอ";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _manageMasterCodeFull
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._manageDataScreen);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_manageMasterCodeFull";
            this.Size = new System.Drawing.Size(898, 699);
            this._manageDataScreen._form2.ResumeLayout(false);
            this._manageDataScreen._form2.PerformLayout();
            this._manageDataScreen.ResumeLayout(false);
            this._myPanel.ResumeLayout(false);
            this._myPanel.PerformLayout();
            //this._panel1.ResumeLayout(false);
            //this._panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private MyLib._myPanel _myPanel;
		private MyLib.ToolStripMyButton _buttonSave;
        public _managerMasterScreenClass _inputScreen;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public _myShadowLabel _labelTitle;
        public _myManageData _manageDataScreen;
        public System.Windows.Forms.ToolStrip _toolBar;
        public System.Windows.Forms.WebBrowser _webBrowser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ToolStripMyButton _buttonClose;
        public System.Windows.Forms.Panel _extraPanel;
        //public MyLib._myPanel _panel1;
	}
}
