namespace SMLERPIC
{
	partial class _icMainControl
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
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._myTabControlMain = new MyLib._myTabControl();
            this.tab_unit = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._icmainGridUnit = new SMLERPControl._ic._icmainGridUnitControl();
            this._infoWebBrowser = new System.Windows.Forms.WebBrowser();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._icmainGridBarCode = new SMLInventoryControl._icmainGridBarCodeControl();
            this._icmainGridBranch = new SMLInventoryControl._icmainGridWarehouseLocationControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._getWareHouseAndLocationButton = new System.Windows.Forms.ToolStripButton();
            this._clearButton = new System.Windows.Forms.ToolStripButton();
            this.tab_account = new System.Windows.Forms.TabPage();
            this._icmainScreenAccount = new SMLInventoryControl._icmainScreenAccountControl();
            this.tab_opposite_unit = new System.Windows.Forms.TabPage();
            this._icmainGridUnitOpposi = new SMLInventoryControl._icmainGridUnitOpposiControl();
            this.tab_other = new System.Windows.Forms.TabPage();
            this._icmainScreenMoreControl = new SMLERPControl._icmainScreenMoreControl();
            this.tab_standard_cost = new System.Windows.Forms.TabPage();
            this._icStandardCost = new SMLInventoryControl._icStandardCostControl();
            this._myPanel1 = new MyLib._myPanel();
            this._icmainScreenTop = new SMLERPControl._icmainScreenTopControl();
            this._myToolBar.SuspendLayout();
            this._myTabControlMain.SuspendLayout();
            this.tab_unit.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tab_account.SuspendLayout();
            this.tab_opposite_unit.SuspendLayout();
            this.tab_other.SuspendLayout();
            this.tab_standard_cost.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLERPIC.Properties.Resources.bt03;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(936, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(112, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            // 
            // _myTabControlMain
            // 
            this._myTabControlMain.Controls.Add(this.tab_unit);
            this._myTabControlMain.Controls.Add(this.tab_account);
            this._myTabControlMain.Controls.Add(this.tab_opposite_unit);
            this._myTabControlMain.Controls.Add(this.tab_other);
            this._myTabControlMain.Controls.Add(this.tab_standard_cost);
            this._myTabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControlMain.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControlMain.Location = new System.Drawing.Point(0, 182);
            this._myTabControlMain.Multiline = true;
            this._myTabControlMain.Name = "_myTabControlMain";
            this._myTabControlMain.SelectedIndex = 0;
            this._myTabControlMain.ShowTabNumber = true;
            this._myTabControlMain.Size = new System.Drawing.Size(1026, 454);
            this._myTabControlMain.TabIndex = 1;
            this._myTabControlMain.TableName = "";
            // 
            // tab_unit
            // 
            this.tab_unit.Controls.Add(this.splitContainer1);
            this.tab_unit.Location = new System.Drawing.Point(4, 23);
            this.tab_unit.Name = "tab_unit";
            this.tab_unit.Padding = new System.Windows.Forms.Padding(3);
            this.tab_unit.Size = new System.Drawing.Size(1018, 427);
            this.tab_unit.TabIndex = 0;
            this.tab_unit.Text = "1.tab_unit";
            this.tab_unit.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1012, 421);
            this.splitContainer1.SplitterDistance = 143;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._icmainGridUnit);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._infoWebBrowser);
            this.splitContainer2.Size = new System.Drawing.Size(1012, 143);
            this.splitContainer2.SplitterDistance = 857;
            this.splitContainer2.TabIndex = 1;
            // 
            // _icmainGridUnit
            // 
            this._icmainGridUnit._extraWordShow = true;
            this._icmainGridUnit._selectRow = -1;
            this._icmainGridUnit.BackColor = System.Drawing.SystemColors.Window;
            this._icmainGridUnit.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icmainGridUnit.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icmainGridUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainGridUnit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icmainGridUnit.Location = new System.Drawing.Point(0, 0);
            this._icmainGridUnit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icmainGridUnit.Name = "_icmainGridUnit";
            this._icmainGridUnit.Size = new System.Drawing.Size(855, 141);
            this._icmainGridUnit.TabIndex = 0;
            this._icmainGridUnit.TabStop = false;
            // 
            // _infoWebBrowser
            // 
            this._infoWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._infoWebBrowser.Location = new System.Drawing.Point(0, 0);
            this._infoWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._infoWebBrowser.Name = "_infoWebBrowser";
            this._infoWebBrowser.ScrollBarsEnabled = false;
            this._infoWebBrowser.Size = new System.Drawing.Size(149, 141);
            this._infoWebBrowser.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this._icmainGridBarCode);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._icmainGridBranch);
            this.splitContainer3.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer3.Size = new System.Drawing.Size(1012, 274);
            this.splitContainer3.SplitterDistance = 126;
            this.splitContainer3.TabIndex = 0;
            // 
            // _icmainGridBarCode
            // 
            this._icmainGridBarCode._extraWordShow = true;
            this._icmainGridBarCode._selectRow = -1;
            this._icmainGridBarCode.AllowDrop = true;
            this._icmainGridBarCode.BackColor = System.Drawing.SystemColors.Window;
            this._icmainGridBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._icmainGridBarCode.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icmainGridBarCode.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icmainGridBarCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainGridBarCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icmainGridBarCode.Location = new System.Drawing.Point(0, 0);
            this._icmainGridBarCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icmainGridBarCode.Name = "_icmainGridBarCode";
            this._icmainGridBarCode.Padding = new System.Windows.Forms.Padding(5);
            this._icmainGridBarCode.Size = new System.Drawing.Size(1010, 124);
            this._icmainGridBarCode.TabIndex = 2;
            this._icmainGridBarCode.TabStop = false;
            // 
            // _icmainGridBranch
            // 
            this._icmainGridBranch._extraWordShow = true;
            this._icmainGridBranch._selectRow = -1;
            this._icmainGridBranch.BackColor = System.Drawing.SystemColors.Window;
            this._icmainGridBranch.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icmainGridBranch.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icmainGridBranch.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainGridBranch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icmainGridBranch.Location = new System.Drawing.Point(0, 25);
            this._icmainGridBranch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icmainGridBranch.Name = "_icmainGridBranch";
            this._icmainGridBranch.Size = new System.Drawing.Size(1010, 117);
            this._icmainGridBranch.TabIndex = 4;
            this._icmainGridBranch.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPIC.Properties.Resources.bt03;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._getWareHouseAndLocationButton,
            this._clearButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1010, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _getWareHouseAndLocationButton
            // 
            this._getWareHouseAndLocationButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._getWareHouseAndLocationButton.Image = global::SMLERPIC.Properties.Resources.replace2;
            this._getWareHouseAndLocationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._getWareHouseAndLocationButton.Name = "_getWareHouseAndLocationButton";
            this._getWareHouseAndLocationButton.Size = new System.Drawing.Size(161, 22);
            this._getWareHouseAndLocationButton.Text = "All WareHouse and Shelf";
            this._getWareHouseAndLocationButton.Click += new System.EventHandler(this._getWareHouseAndLocationButton_Click);
            // 
            // _clearButton
            // 
            this._clearButton.Image = global::SMLERPIC.Properties.Resources.delete;
            this._clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(53, 22);
            this._clearButton.Text = "Clear";
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // tab_account
            // 
            this.tab_account.Controls.Add(this._icmainScreenAccount);
            this.tab_account.Location = new System.Drawing.Point(4, 23);
            this.tab_account.Name = "tab_account";
            this.tab_account.Padding = new System.Windows.Forms.Padding(3);
            this.tab_account.Size = new System.Drawing.Size(1018, 427);
            this.tab_account.TabIndex = 3;
            this.tab_account.Text = "2.tab_account";
            this.tab_account.UseVisualStyleBackColor = true;
            // 
            // _icmainScreenAccount
            // 
            this._icmainScreenAccount._isChange = false;
            this._icmainScreenAccount.AutoSize = true;
            this._icmainScreenAccount.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainScreenAccount.Font = new System.Drawing.Font("Tahoma", 9F);
            this._icmainScreenAccount.Location = new System.Drawing.Point(3, 3);
            this._icmainScreenAccount.Name = "_icmainScreenAccount";
            this._icmainScreenAccount.Padding = new System.Windows.Forms.Padding(3);
            this._icmainScreenAccount.Size = new System.Drawing.Size(1012, 421);
            this._icmainScreenAccount.TabIndex = 0;
            // 
            // tab_opposite_unit
            // 
            this.tab_opposite_unit.Controls.Add(this._icmainGridUnitOpposi);
            this.tab_opposite_unit.Location = new System.Drawing.Point(4, 23);
            this.tab_opposite_unit.Name = "tab_opposite_unit";
            this.tab_opposite_unit.Padding = new System.Windows.Forms.Padding(3);
            this.tab_opposite_unit.Size = new System.Drawing.Size(1018, 427);
            this.tab_opposite_unit.TabIndex = 1;
            this.tab_opposite_unit.Text = "3.tab_opposite_unit";
            this.tab_opposite_unit.UseVisualStyleBackColor = true;
            // 
            // _icmainGridUnitOpposi
            // 
            this._icmainGridUnitOpposi._extraWordShow = true;
            this._icmainGridUnitOpposi._selectRow = -1;
            this._icmainGridUnitOpposi.BackColor = System.Drawing.SystemColors.Window;
            this._icmainGridUnitOpposi.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icmainGridUnitOpposi.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icmainGridUnitOpposi.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainGridUnitOpposi.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icmainGridUnitOpposi.Location = new System.Drawing.Point(3, 3);
            this._icmainGridUnitOpposi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icmainGridUnitOpposi.Name = "_icmainGridUnitOpposi";
            this._icmainGridUnitOpposi.Size = new System.Drawing.Size(1012, 421);
            this._icmainGridUnitOpposi.TabIndex = 1;
            this._icmainGridUnitOpposi.TabStop = false;
            // 
            // tab_other
            // 
            this.tab_other.Controls.Add(this._icmainScreenMoreControl);
            this.tab_other.Location = new System.Drawing.Point(4, 23);
            this.tab_other.Name = "tab_other";
            this.tab_other.Size = new System.Drawing.Size(1018, 427);
            this.tab_other.TabIndex = 4;
            this.tab_other.Text = "4.tab_other";
            this.tab_other.UseVisualStyleBackColor = true;
            // 
            // _icmainScreenMoreControl
            // 
            this._icmainScreenMoreControl._isChange = false;
            this._icmainScreenMoreControl.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenMoreControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainScreenMoreControl.Location = new System.Drawing.Point(0, 0);
            this._icmainScreenMoreControl.Name = "_icmainScreenMoreControl";
            this._icmainScreenMoreControl.Size = new System.Drawing.Size(1018, 427);
            this._icmainScreenMoreControl.TabIndex = 0;
            // 
            // tab_standard_cost
            // 
            this.tab_standard_cost.Controls.Add(this._icStandardCost);
            this.tab_standard_cost.Location = new System.Drawing.Point(4, 23);
            this.tab_standard_cost.Name = "tab_standard_cost";
            this.tab_standard_cost.Size = new System.Drawing.Size(1018, 427);
            this.tab_standard_cost.TabIndex = 5;
            this.tab_standard_cost.Text = "5.tab_standard_cost";
            this.tab_standard_cost.UseVisualStyleBackColor = true;
            // 
            // _icStandardCost
            // 
            this._icStandardCost._extraWordShow = true;
            this._icStandardCost._selectRow = -1;
            this._icStandardCost.BackColor = System.Drawing.SystemColors.Window;
            this._icStandardCost.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icStandardCost.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icStandardCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icStandardCost.Font = new System.Drawing.Font("Tahoma", 9F);
            this._icStandardCost.Location = new System.Drawing.Point(0, 0);
            this._icStandardCost.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icStandardCost.Name = "_icStandardCost";
            this._icStandardCost.Size = new System.Drawing.Size(1018, 427);
            this._icStandardCost.TabIndex = 0;
            this._icStandardCost.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.Size = new System.Drawing.Size(936, 705);
            this._myPanel1.TabIndex = 1;
            // 
            // _icmainScreenTop
            // 
            this._icmainScreenTop._isChange = false;
            this._icmainScreenTop.AutoSize = true;
            this._icmainScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenTop.Location = new System.Drawing.Point(0, 0);
            this._icmainScreenTop.Name = "_icmainScreenTop";
            this._icmainScreenTop.Size = new System.Drawing.Size(1026, 182);
            this._icmainScreenTop.TabIndex = 0;
            // 
            // _icMainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._myTabControlMain);
            this.Controls.Add(this._icmainScreenTop);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icMainControl";
            this.Size = new System.Drawing.Size(1026, 636);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myTabControlMain.ResumeLayout(false);
            this.tab_unit.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tab_account.ResumeLayout(false);
            this.tab_account.PerformLayout();
            this.tab_opposite_unit.ResumeLayout(false);
            this.tab_other.ResumeLayout(false);
            this.tab_standard_cost.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib._myPanel _myPanel1;
        private MyLib._myTabControl _myTabControlMain;
        private System.Windows.Forms.TabPage tab_unit;
        public SMLERPControl._ic._icmainGridUnitControl _icmainGridUnit;
        private System.Windows.Forms.TabPage tab_opposite_unit;
        public SMLInventoryControl._icmainGridUnitOpposiControl _icmainGridUnitOpposi;
        private System.Windows.Forms.TabPage tab_account;
        public SMLInventoryControl._icmainScreenAccountControl _icmainScreenAccount;
        public SMLERPControl._icmainScreenTopControl _icmainScreenTop;
        private MyLib.ToolStripMyButton _saveButton;
        private System.Windows.Forms.WebBrowser _infoWebBrowser;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        public SMLInventoryControl._icmainGridWarehouseLocationControl _icmainGridBranch;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _getWareHouseAndLocationButton;
        public SMLInventoryControl._icmainGridBarCodeControl _icmainGridBarCode;
        private System.Windows.Forms.ToolStripButton _clearButton;
        private System.Windows.Forms.TabPage tab_other;
        public SMLERPControl._icmainScreenMoreControl _icmainScreenMoreControl;
        private System.Windows.Forms.TabPage tab_standard_cost;
        public SMLInventoryControl._icStandardCostControl _icStandardCost;
        public System.Windows.Forms.SplitContainer splitContainer1;
    }
}
