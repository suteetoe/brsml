namespace SMLERPASSET
{
    partial class _as_maintain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_as_maintain));
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel2 = new MyLib._myPanel();
            this._myPanel1 = new MyLib._myPanel();
            this._screenTop = new MyLib._myScreen();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._clearDataAfterSaveButton = new MyLib.ToolStripMyButton();
            this._autoRunningButton = new MyLib.ToolStripMyButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._maintenanceGrid1 = new SMLERPASSET._controls._maintenanceGrid();
            this._myManageData1._form2.SuspendLayout();
            this._myManageData1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageData1
            // 
            this._myManageData1.BackColor = System.Drawing.Color.LightCyan;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            // 
            // _myManageData1.Panel1
            // 
            this._myManageData1._form1.BackColor = System.Drawing.Color.LightCyan;
            // 
            // _myManageData1.Panel2
            // 
            this._myManageData1._form2.BackColor = System.Drawing.Color.LightCyan;
            this._myManageData1._form2.Controls.Add(this._myPanel2);
            this._myManageData1._form2.Controls.Add(this._myToolBar);
            this._myManageData1.Size = new System.Drawing.Size(529, 447);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;
            // 
            // _myPanel2
            // 
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._myPanel1);
            this._myPanel2.Controls.Add(this._screenTop);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 25);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel2.Size = new System.Drawing.Size(449, 420);
            this._myPanel2.TabIndex = 5;
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._maintenanceGrid1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(5, 45);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this._myPanel1.Size = new System.Drawing.Size(439, 370);
            this._myPanel1.TabIndex = 5;
            // 
            // _screenTop
            // 
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(5, 5);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(439, 40);
            this._screenTop.TabIndex = 0;
            // 
            // _myToolBar
            // 
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._clearDataAfterSaveButton,
            this._autoRunningButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(449, 25);
            this._myToolBar.TabIndex = 13;
            this._myToolBar.Text = "toolStrip2";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPASSET.Properties.Resources.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.Size = new System.Drawing.Size(87, 22);
            this._buttonSave.Text = "บันทึก (F12)";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _clearDataAfterSaveButton
            // 
            this._clearDataAfterSaveButton.Image = ((System.Drawing.Image)(resources.GetObject("_clearDataAfterSaveButton.Image")));
            this._clearDataAfterSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearDataAfterSaveButton.Name = "_clearDataAfterSaveButton";
            this._clearDataAfterSaveButton.ResourceName = "ล้างหน้าจอทุกครั้งหลังจากบันทึก";
            this._clearDataAfterSaveButton.Padding = new System.Windows.Forms.Padding(1);
            this._clearDataAfterSaveButton.Size = new System.Drawing.Size(176, 22);
            this._clearDataAfterSaveButton.Text = "ล้างหน้าจอทุกครั้งหลังจากบันทึก";
            this._clearDataAfterSaveButton.Click += new System.EventHandler(this._clearDataAfterSaveButton_Click);
            // 
            // _autoRunningButton
            // 
            this._autoRunningButton.Image = ((System.Drawing.Image)(resources.GetObject("_autoRunningButton.Image")));
            this._autoRunningButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._autoRunningButton.Name = "_autoRunningButton";
            this._autoRunningButton.ResourceName = "เลขที่เอกสารอัตโนมัติ";
            this._autoRunningButton.Padding = new System.Windows.Forms.Padding(1);
            this._autoRunningButton.Size = new System.Drawing.Size(133, 22);
            this._autoRunningButton.Text = "เลขที่เอกสารอัตโนมัติ ";
            this._autoRunningButton.Click += new System.EventHandler(this._autoRunningButton_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "check.png");
            this.imageList1.Images.SetKeyName(1, "forbidden.png");
            // 
            // _maintenanceGrid1
            // 
            this._maintenanceGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._maintenanceGrid1.Location = new System.Drawing.Point(0, 5);
            this._maintenanceGrid1.Name = "_maintenanceGrid1";
            this._maintenanceGrid1.Size = new System.Drawing.Size(439, 365);
            this._maintenanceGrid1.TabIndex = 0;
            // 
            // _as_maintain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_as_maintain";
            this.Size = new System.Drawing.Size(529, 447);
            this._myManageData1._form2.ResumeLayout(false);
            this._myManageData1._form2.PerformLayout();
            this._myManageData1.ResumeLayout(false);
            this._myPanel2.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageData1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _buttonSave;
        private MyLib._myPanel _myPanel2;
        private MyLib._myScreen _screenTop;
        private MyLib._myPanel _myPanel1;
        private SMLERPASSET._controls._maintenanceGrid _maintenanceGrid1;
        private MyLib.ToolStripMyButton _clearDataAfterSaveButton;
        private MyLib.ToolStripMyButton _autoRunningButton;
        private System.Windows.Forms.ImageList imageList1;


    }
}
