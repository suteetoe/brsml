namespace SMLERPGL
{
    partial class _journalEntryAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_journalEntryAccount));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._tab = new MyLib._myTabControl();
            this._tab_detail = new System.Windows.Forms.TabPage();
            this._glDetail1 = new SMLERPGLControl._glDetail();
            this._screenTop = new SMLERPGLControl._journalScreen();
            this._recurring1 = new SMLERPGLControl._recurring();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._buttonPrint = new MyLib.ToolStripMyButton();
            this._clearDataAfterSaveButton = new MyLib.ToolStripMyButton();
            this._autoRunningButton = new MyLib.ToolStripMyButton();
            this._buttonChartOfAccountPopUp = new MyLib.ToolStripMyButton();
            this._myPanel1.SuspendLayout();
            this._tab.SuspendLayout();
            this._tab_detail.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "check.png");
            this.imageList1.Images.SetKeyName(1, "forbidden.png");
            // 
            // _myManageData1
            // 
            this._myManageData1.BackColor = System.Drawing.Color.Transparent;
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
            this._myManageData1._form2.BackColor = System.Drawing.SystemColors.Control;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            this._myManageData1._form2.Controls.Add(this._recurring1);
            this._myManageData1._form2.Controls.Add(this._myToolBar);
            this._myManageData1.Size = new System.Drawing.Size(909, 556);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._tab);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 50);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(4);
            this._myPanel1.ShowBackground = false;
            this._myPanel1.Size = new System.Drawing.Size(824, 485);
            this._myPanel1.TabIndex = 17;
            // 
            // _tab
            // 
            this._tab.Controls.Add(this._tab_detail);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._tab.ImageList = this.imageList1;
            this._tab.Location = new System.Drawing.Point(4, 184);
            this._tab.Multiline = true;
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(816, 297);
            this._tab.TabIndex = 7;
            this._tab.TableName = "ic_trans";
            // 
            // _tab_detail
            // 
            this._tab_detail.Controls.Add(this._glDetail1);
            this._tab_detail.Location = new System.Drawing.Point(4, 23);
            this._tab_detail.Name = "_tab_detail";
            this._tab_detail.Size = new System.Drawing.Size(808, 270);
            this._tab_detail.TabIndex = 0;
            this._tab_detail.Text = "tab_detail";
            this._tab_detail.UseVisualStyleBackColor = true;
            // 
            // _glDetail1
            // 
            this._glDetail1.AllowDrop = true;
            this._glDetail1.BackColor = System.Drawing.Color.Transparent;
            this._glDetail1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._glDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glDetail1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glDetail1.Location = new System.Drawing.Point(0, 0);
            this._glDetail1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glDetail1.Name = "_glDetail1";
            this._glDetail1.Size = new System.Drawing.Size(808, 270);
            this._glDetail1.TabIndex = 16;
            this._glDetail1.TabStop = false;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(4, 4);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(816, 180);
            this._screenTop.TabIndex = 18;
            // 
            // _recurring1
            // 
            this._recurring1.AutoSize = true;
            this._recurring1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._recurring1.BackgroundImage = global::SMLERPGL.Resource16x16.bt03;
            this._recurring1.Dock = System.Windows.Forms.DockStyle.Top;
            this._recurring1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._recurring1.Location = new System.Drawing.Point(0, 25);
            this._recurring1.Name = "_recurring1";
            this._recurring1.Size = new System.Drawing.Size(824, 25);
            this._recurring1.TabIndex = 19;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myToolBar.BackgroundImage = global::SMLERPGL.Resource16x16.bt03;
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._buttonPrint,
            this._clearDataAfterSaveButton,
            this._autoRunningButton,
            this._buttonChartOfAccountPopUp});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(824, 25);
            this._myToolBar.TabIndex = 13;
            this._myToolBar.Text = "toolStrip2";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPGL.Resource16x16.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "บันทึก (F12)";
            this._buttonSave.Size = new System.Drawing.Size(86, 22);
            this._buttonSave.Text = "บันทึก (F12)";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonPrint
            // 
            this._buttonPrint.Image = global::SMLERPGL.Resource16x16.printer;
            this._buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPrint.Name = "_buttonPrint";
            this._buttonPrint.Padding = new System.Windows.Forms.Padding(1);
            this._buttonPrint.ResourceName = "พิมพ์";
            this._buttonPrint.Size = new System.Drawing.Size(52, 22);
            this._buttonPrint.Text = "พิมพ์";
            this._buttonPrint.Click += new System.EventHandler(this._buttonPrint_Click);
            // 
            // _clearDataAfterSaveButton
            // 
            this._clearDataAfterSaveButton.Image = ((System.Drawing.Image)(resources.GetObject("_clearDataAfterSaveButton.Image")));
            this._clearDataAfterSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearDataAfterSaveButton.Name = "_clearDataAfterSaveButton";
            this._clearDataAfterSaveButton.Padding = new System.Windows.Forms.Padding(1);
            this._clearDataAfterSaveButton.ResourceName = "ล้างหน้าจอทุกครั้งหลังจากบันทึก";
            this._clearDataAfterSaveButton.Size = new System.Drawing.Size(171, 22);
            this._clearDataAfterSaveButton.Text = "ล้างหน้าจอทุกครั้งหลังจากบันทึก";
            this._clearDataAfterSaveButton.Click += new System.EventHandler(this._clearDataAfterSaveButton_Click);
            // 
            // _autoRunningButton
            // 
            this._autoRunningButton.Image = ((System.Drawing.Image)(resources.GetObject("_autoRunningButton.Image")));
            this._autoRunningButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._autoRunningButton.Name = "_autoRunningButton";
            this._autoRunningButton.Padding = new System.Windows.Forms.Padding(1);
            this._autoRunningButton.ResourceName = "เลขที่เอกสารอัตโนมัติ";
            this._autoRunningButton.Size = new System.Drawing.Size(121, 22);
            this._autoRunningButton.Text = "เลขที่เอกสารอัตโนมัติ";
            this._autoRunningButton.Click += new System.EventHandler(this._autoRunningButton_Click);
            // 
            // _buttonChartOfAccountPopUp
            // 
            this._buttonChartOfAccountPopUp.Image = global::SMLERPGL.Resource16x16.branch_element;
            this._buttonChartOfAccountPopUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonChartOfAccountPopUp.Name = "_buttonChartOfAccountPopUp";
            this._buttonChartOfAccountPopUp.Padding = new System.Windows.Forms.Padding(1);
            this._buttonChartOfAccountPopUp.ResourceName = "รายละเอียดผังบัญชีทั้งหมด (F9)";
            this._buttonChartOfAccountPopUp.Size = new System.Drawing.Size(169, 22);
            this._buttonChartOfAccountPopUp.Text = "รายละเอียดผังบัญชีทั้งหมด (F9)";
            this._buttonChartOfAccountPopUp.Click += new System.EventHandler(this._buttonChartOfAccountPopUp_Click);
            // 
            // _journalEntryAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._myManageData1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_journalEntryAccount";
            this.Size = new System.Drawing.Size(909, 556);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._tab.ResumeLayout(false);
            this._tab_detail.ResumeLayout(false);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageData1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _buttonSave;
        private MyLib.ToolStripMyButton _buttonPrint;
        private MyLib._myPanel _myPanel1;
        private SMLERPGLControl._journalScreen _screenTop;
        private SMLERPGLControl._glDetail _glDetail1;
        private MyLib.ToolStripMyButton _clearDataAfterSaveButton;
        private MyLib.ToolStripMyButton _autoRunningButton;
        private SMLERPGLControl._recurring _recurring1;
        private MyLib.ToolStripMyButton _buttonChartOfAccountPopUp;
        private System.Windows.Forms.ImageList imageList1;
        private MyLib._myTabControl _tab;
        private System.Windows.Forms.TabPage _tab_detail;


    }
}
