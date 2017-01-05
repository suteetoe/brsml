namespace SMLERPGL._chart
{
	partial class _chartOfAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_chartOfAccount));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel = new MyLib._myPanel();
            this._screenTop = new SMLERPGL._chart._chartOfAccountScreen();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._buttonSelectSide = new MyLib.ToolStripMyButton();
            this._buttonSelectDepartment = new MyLib.ToolStripMyButton();
            this._buttonSelectAllocate = new MyLib.ToolStripMyButton();
            this._buttonSelectJob = new MyLib.ToolStripMyButton();
            this._buttonSelectProject = new MyLib.ToolStripMyButton();
            this._myManageData1._form2.SuspendLayout();
            this._myManageData1.SuspendLayout();
            this._myPanel.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            // 
            // _myManageData1.Panel1
            // 
            this._myManageData1._form1.BackColor = System.Drawing.Color.LightCyan;
            this._myManageData1._form1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            // 
            // _myManageData1.Panel2
            // 
            this._myManageData1._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1._form2.Controls.Add(this._myPanel);
            this._myManageData1._form2.Controls.Add(this._myToolBar);
            this._myManageData1.Size = new System.Drawing.Size(947, 578);
            this._myManageData1.TabIndex = 1;
            this._myManageData1.TabStop = false;
            this._myManageData1.Text = "_myManageData1";
            // 
            // _myPanel
            // 
            this._myPanel._switchTabAuto = false;
            this._myPanel.BeginColor = System.Drawing.Color.White;
            this._myPanel.Controls.Add(this._screenTop);
            this._myPanel.CornerPicture = null;
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel.EndColor = System.Drawing.Color.WhiteSmoke;
            this._myPanel.Location = new System.Drawing.Point(0, 25);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Padding = new System.Windows.Forms.Padding(4);
            this._myPanel.Size = new System.Drawing.Size(720, 551);
            this._myPanel.TabIndex = 13;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(4, 4);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(712, 508);
            this._screenTop.TabIndex = 0;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackColor = System.Drawing.SystemColors.Control;
            this._myToolBar.BackgroundImage = global::SMLERPGL.Resource16x16.bt03;
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._buttonSelectSide,
            this._buttonSelectDepartment,
            this._buttonSelectAllocate,
            this._buttonSelectJob,
            this._buttonSelectProject});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(720, 25);
            this._myToolBar.TabIndex = 12;
            this._myToolBar.Text = "ToolStrip";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPGL.Resource16x16.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "บันทึก (F12)";
            this._buttonSave.Size = new System.Drawing.Size(87, 22);
            this._buttonSave.Text = "บันทึก (F12)";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonSelectSide
            // 
            this._buttonSelectSide.Image = global::SMLERPGL.Resource16x16.preferences;
            this._buttonSelectSide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectSide.Name = "_buttonSelectSide";
            this._buttonSelectSide.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSelectSide.ResourceName = "เลือกหน่วยงาน";
            this._buttonSelectSide.Size = new System.Drawing.Size(99, 22);
            this._buttonSelectSide.Text = "เลือกหน่วยงาน";
            this._buttonSelectSide.Click += new System.EventHandler(this._buttonSelectSide_Click);
            // 
            // _buttonSelectDepartment
            // 
            this._buttonSelectDepartment.Image = global::SMLERPGL.Resource16x16.preferences;
            this._buttonSelectDepartment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectDepartment.Name = "_buttonSelectDepartment";
            this._buttonSelectDepartment.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSelectDepartment.ResourceName = "เลือกแผนก";
            this._buttonSelectDepartment.Size = new System.Drawing.Size(83, 22);
            this._buttonSelectDepartment.Text = "เลือกแผนก";
            this._buttonSelectDepartment.Click += new System.EventHandler(this._buttonSelectDepartment_Click);
            // 
            // _buttonSelectAllocate
            // 
            this._buttonSelectAllocate.Image = ((System.Drawing.Image)(resources.GetObject("_buttonSelectAllocate.Image")));
            this._buttonSelectAllocate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectAllocate.Name = "_buttonSelectAllocate";
            this._buttonSelectAllocate.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSelectAllocate.ResourceName = "เลือกการจัดสรร";
            this._buttonSelectAllocate.Size = new System.Drawing.Size(102, 22);
            this._buttonSelectAllocate.Text = "เลือกการจัดสรร";
            this._buttonSelectAllocate.Click += new System.EventHandler(this._buttonSelectAllocate_Click);
            // 
            // _buttonSelectJob
            // 
            this._buttonSelectJob.Image = ((System.Drawing.Image)(resources.GetObject("_buttonSelectJob.Image")));
            this._buttonSelectJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectJob.Name = "_buttonSelectJob";
            this._buttonSelectJob.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSelectJob.ResourceName = "เลือกงาน";
            this._buttonSelectJob.Size = new System.Drawing.Size(71, 22);
            this._buttonSelectJob.Text = "เลือกงาน";
            this._buttonSelectJob.Click += new System.EventHandler(this._buttonSelectJob_Click);
            // 
            // _buttonSelectProject
            // 
            this._buttonSelectProject.Image = ((System.Drawing.Image)(resources.GetObject("_buttonSelectProject.Image")));
            this._buttonSelectProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectProject.Name = "_buttonSelectProject";
            this._buttonSelectProject.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSelectProject.ResourceName = "เลือกโครงการ";
            this._buttonSelectProject.Size = new System.Drawing.Size(94, 22);
            this._buttonSelectProject.Text = "เลือกโครงการ";
            this._buttonSelectProject.Click += new System.EventHandler(this._buttonSelectProject_Click);
            // 
            // _chartOfAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this._myManageData1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_chartOfAccount";
            this.Size = new System.Drawing.Size(947, 578);
            this._myManageData1._form2.ResumeLayout(false);
            this._myManageData1._form2.PerformLayout();
            this._myManageData1.ResumeLayout(false);
            this._myPanel.ResumeLayout(false);
            this._myPanel.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ImageList imageList1;
        private MyLib._myManageData _myManageData1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _buttonSave;
        private _chartOfAccountScreen _screenTop;
        private MyLib.ToolStripMyButton _buttonSelectSide;
        private MyLib.ToolStripMyButton _buttonSelectDepartment;
        private MyLib.ToolStripMyButton _buttonSelectAllocate;
        private MyLib.ToolStripMyButton _buttonSelectJob;
        private MyLib.ToolStripMyButton _buttonSelectProject;
        private MyLib._myPanel _myPanel;
	}
}
