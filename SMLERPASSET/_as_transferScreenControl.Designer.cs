namespace SMLERPASSET
{
    partial class _as_transferScreenControl
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
            this._myPanel1 = new MyLib._myPanel();
            this._tab = new MyLib._myTabControl();
            this.tab_detail = new System.Windows.Forms.TabPage();
            this._gridAssetDetail = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonProcess = new MyLib.ToolStripMyButton();
            this._buttonReset = new MyLib.ToolStripMyButton();
            this._myGroupBox1 = new MyLib._myGroupBox();
            this._screenTop = new MyLib._myScreen();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._asConditionScreen1 = new SMLERPASSET._asConditionScreen();
            this._myPanel1.SuspendLayout();
            this._tab.SuspendLayout();
            this.tab_detail.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._myGroupBox1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._tab);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.Controls.Add(this._myToolBar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(1027, 1320);
            this._myPanel1.TabIndex = 19;
            // 
            // _tab
            // 
            this._tab.Controls.Add(this.tab_detail);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._tab.Location = new System.Drawing.Point(0, 25);
            this._tab.Multiline = true;
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(1027, 1295);
            this._tab.TabIndex = 19;
            this._tab.TableName = "ic_trans";
            // 
            // tab_detail
            // 
            this.tab_detail.Controls.Add(this._gridAssetDetail);
            this.tab_detail.Controls.Add(this.toolStrip1);
            this.tab_detail.Controls.Add(this._myGroupBox1);
            this.tab_detail.Location = new System.Drawing.Point(4, 23);
            this.tab_detail.Margin = new System.Windows.Forms.Padding(0);
            this.tab_detail.Name = "tab_detail";
            this.tab_detail.Padding = new System.Windows.Forms.Padding(3);
            this.tab_detail.Size = new System.Drawing.Size(1019, 1268);
            this.tab_detail.TabIndex = 0;
            this.tab_detail.Text = "tab_detail";
            this.tab_detail.UseVisualStyleBackColor = true;
            // 
            // _gridAssetDetail
            // 
            this._gridAssetDetail._extraWordShow = true;
            this._gridAssetDetail._selectRow = -1;
            this._gridAssetDetail.BackColor = System.Drawing.SystemColors.Window;
            this._gridAssetDetail.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._gridAssetDetail.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._gridAssetDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridAssetDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridAssetDetail.Location = new System.Drawing.Point(3, 217);
            this._gridAssetDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridAssetDetail.Name = "_gridAssetDetail";
            this._gridAssetDetail.Size = new System.Drawing.Size(1013, 1048);
            this._gridAssetDetail.TabIndex = 18;
            this._gridAssetDetail.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonProcess,
            this._buttonReset});
            this.toolStrip1.Location = new System.Drawing.Point(3, 192);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1013, 25);
            this.toolStrip1.TabIndex = 19;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonProcess
            // 
            this._buttonProcess.Image = global::SMLERPASSET.Properties.Resources.flash;
            this._buttonProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.Padding = new System.Windows.Forms.Padding(1);
            this._buttonProcess.ResourceName = "ประมวลผล";
            this._buttonProcess.Size = new System.Drawing.Size(76, 22);
            this._buttonProcess.Text = "ประมวลผล";
            // 
            // _buttonReset
            // 
            this._buttonReset.Image = global::SMLERPASSET.Properties.Resources.replace2;
            this._buttonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonReset.Name = "_buttonReset";
            this._buttonReset.Padding = new System.Windows.Forms.Padding(1);
            this._buttonReset.ResourceName = "Reset";
            this._buttonReset.Size = new System.Drawing.Size(57, 22);
            this._buttonReset.Text = "Reset";
            // 
            // _myGroupBox1
            // 
            this._myGroupBox1.AutoSize = true;
            this._myGroupBox1.Controls.Add(this._asConditionScreen1);
            this._myGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myGroupBox1.Location = new System.Drawing.Point(3, 3);
            this._myGroupBox1.Name = "_myGroupBox1";
            this._myGroupBox1.ResourceName = "เงื่อนไข";
            this._myGroupBox1.Size = new System.Drawing.Size(1013, 189);
            this._myGroupBox1.TabIndex = 21;
            this._myGroupBox1.TabStop = false;
            this._myGroupBox1.Text = "เงื่อนไข";
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(0, 25);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(1027, 0);
            this._screenTop.TabIndex = 16;
            // 
            // _myToolBar
            // 
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(1027, 25);
            this._myToolBar.TabIndex = 17;
            this._myToolBar.Text = "toolStrip2";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPASSET.Properties.Resources.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Size = new System.Drawing.Size(112, 22);
            this._buttonSave.Text = "บันทึกข้อมูล (F12)";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPASSET.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            // 
            // _asConditionScreen1
            // 
            this._asConditionScreen1._isChange = false;
            this._asConditionScreen1.BackColor = System.Drawing.Color.Transparent;
            this._asConditionScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._asConditionScreen1.Location = new System.Drawing.Point(3, 18);
            this._asConditionScreen1.Name = "_asConditionScreen1";
            this._asConditionScreen1.Size = new System.Drawing.Size(1007, 168);
            this._asConditionScreen1.TabIndex = 22;
            // 
            // _as_transferScreenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_as_transferScreenControl";
            this.Size = new System.Drawing.Size(1027, 1320);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._tab.ResumeLayout(false);
            this.tab_detail.ResumeLayout(false);
            this.tab_detail.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._myGroupBox1.ResumeLayout(false);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myGrid _gridAssetDetail;
        public MyLib._myScreen _screenTop;
        public System.Windows.Forms.ToolStrip _myToolBar;
        public MyLib.ToolStripMyButton _buttonSave;
        public MyLib._myPanel _myPanel1;
        public MyLib._myTabControl _tab;
        public System.Windows.Forms.TabPage tab_detail;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib.ToolStripMyButton _buttonProcess;
        public MyLib.ToolStripMyButton _buttonReset;
        private MyLib._myGroupBox _myGroupBox1;
        public _asConditionScreen _asConditionScreen1;
    }
}
