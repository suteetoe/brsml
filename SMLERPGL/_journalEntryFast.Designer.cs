namespace SMLERPGL
{
    partial class _journalEntryFast
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_journalEntryFast));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._dataGrid = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._createDataGridButton = new MyLib.ToolStripMyButton();
            this._processButton = new MyLib.ToolStripMyButton();
            this._autoDateButton = new MyLib.ToolStripMyButton();
            this._autoRunningNumberButton = new MyLib.ToolStripMyButton();
            this._autoCalcButton = new MyLib.ToolStripMyButton();
            this._glTemplateDetail = new SMLERPGLControl._glDetail();
            this._myTabControl1 = new MyLib._myTabControl();
            this.tab_template = new System.Windows.Forms.TabPage();
            this._screenTop = new SMLERPGLControl._journalScreen();
            this._recurring1 = new SMLERPGLControl._recurring();
            this.tab_journal = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this._myTabControl1.SuspendLayout();
            this.tab_template.SuspendLayout();
            this.tab_journal.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(790, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLERPGL.Resource16x16.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Size = new System.Drawing.Size(74, 22);
            this._buttonClose.Text = "ปิดหน้าจอ";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _dataGrid
            // 
            this._dataGrid._extraWordShow = true;
            this._dataGrid._selectRow = -1;
            this._dataGrid.BackColor = System.Drawing.SystemColors.Window;
            this._dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._dataGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._dataGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataGrid.Location = new System.Drawing.Point(3, 28);
            this._dataGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dataGrid.Name = "_dataGrid";
            this._dataGrid.Size = new System.Drawing.Size(776, 522);
            this._dataGrid.TabIndex = 3;
            this._dataGrid.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._createDataGridButton,
            this._processButton,
            this._autoDateButton,
            this._autoRunningNumberButton,
            this._autoCalcButton});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(776, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _createDataGridButton
            // 
            this._createDataGridButton.Image = global::SMLERPGL.Resource16x16.flash;
            this._createDataGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._createDataGridButton.Name = "_createDataGridButton";
            this._createDataGridButton.Padding = new System.Windows.Forms.Padding(1);
            this._createDataGridButton.ResourceName = "สร้างตารางเพื่อบันทึกข้อมูล";
            this._createDataGridButton.Size = new System.Drawing.Size(148, 22);
            this._createDataGridButton.Text = "สร้างตารางเพื่อบันทึกข้อมูล";
            this._createDataGridButton.Click += new System.EventHandler(this._buttonCreateDataGrid_Click);
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLERPGL.Resource16x16.disk_blue;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Padding = new System.Windows.Forms.Padding(1);
            this._processButton.ResourceName = "ประมวลผล (สร้างรายวัน)";
            this._processButton.Size = new System.Drawing.Size(136, 22);
            this._processButton.Text = "ประมวลผล (สร้างรายวัน)";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _autoDateButton
            // 
            this._autoDateButton.Image = ((System.Drawing.Image)(resources.GetObject("_autoDateButton.Image")));
            this._autoDateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._autoDateButton.Name = "_autoDateButton";
            this._autoDateButton.Padding = new System.Windows.Forms.Padding(1);
            this._autoDateButton.ResourceName = "วันที่อัตโนมัติ";
            this._autoDateButton.Size = new System.Drawing.Size(85, 22);
            this._autoDateButton.Text = "วันที่อัตโนมัติ";
            this._autoDateButton.Click += new System.EventHandler(this._autoDateButton_Click);
            // 
            // _autoRunningNumberButton
            // 
            this._autoRunningNumberButton.Image = ((System.Drawing.Image)(resources.GetObject("_autoRunningNumberButton.Image")));
            this._autoRunningNumberButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._autoRunningNumberButton.Name = "_autoRunningNumberButton";
            this._autoRunningNumberButton.Padding = new System.Windows.Forms.Padding(1);
            this._autoRunningNumberButton.ResourceName = "เลขที่เอกสารอัตโนมัติ";
            this._autoRunningNumberButton.Size = new System.Drawing.Size(121, 22);
            this._autoRunningNumberButton.Text = "เลขที่เอกสารอัตโนมัติ";
            this._autoRunningNumberButton.Click += new System.EventHandler(this._autoRunningNumberButton_Click);
            // 
            // _autoCalcButton
            // 
            this._autoCalcButton.Image = ((System.Drawing.Image)(resources.GetObject("_autoCalcButton.Image")));
            this._autoCalcButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._autoCalcButton.Name = "_autoCalcButton";
            this._autoCalcButton.Padding = new System.Windows.Forms.Padding(1);
            this._autoCalcButton.ResourceName = "คำนวณภาษีมูลค่าเพิ่มทันที";
            this._autoCalcButton.Size = new System.Drawing.Size(147, 22);
            this._autoCalcButton.Text = "คำนวณภาษีมูลค่าเพิ่มทันที";
            this._autoCalcButton.Click += new System.EventHandler(this._autoCalcButton_Click);
            // 
            // _glTemplateDetail
            // 
            this._glTemplateDetail.BackColor = System.Drawing.Color.Transparent;
            this._glTemplateDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._glTemplateDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glTemplateDetail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glTemplateDetail.Location = new System.Drawing.Point(3, 208);
            this._glTemplateDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glTemplateDetail.Name = "_glTemplateDetail";
            this._glTemplateDetail.Size = new System.Drawing.Size(776, 342);
            this._glTemplateDetail.TabIndex = 1;
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.tab_template);
            this._myTabControl1.Controls.Add(this.tab_journal);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(0, 25);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.ShowTabNumber = true;
            this._myTabControl1.Size = new System.Drawing.Size(790, 580);
            this._myTabControl1.TabIndex = 5;
            this._myTabControl1.TableName = "gl_resource";
            // 
            // tab_template
            // 
            this.tab_template.BackColor = System.Drawing.Color.MediumTurquoise;
            this.tab_template.Controls.Add(this._glTemplateDetail);
            this.tab_template.Controls.Add(this._screenTop);
            this.tab_template.Controls.Add(this._recurring1);
            this.tab_template.Location = new System.Drawing.Point(4, 23);
            this.tab_template.Name = "tab_template";
            this.tab_template.Padding = new System.Windows.Forms.Padding(3);
            this.tab_template.Size = new System.Drawing.Size(782, 553);
            this.tab_template.TabIndex = 0;
            this.tab_template.Text = "1.tab_template";
            this.tab_template.UseVisualStyleBackColor = true;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(3, 28);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(776, 180);
            this._screenTop.TabIndex = 0;
            // 
            // _recurring1
            // 
            this._recurring1.AutoSize = true;
            this._recurring1.Dock = System.Windows.Forms.DockStyle.Top;
            this._recurring1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._recurring1.Location = new System.Drawing.Point(3, 3);
            this._recurring1.Name = "_recurring1";
            this._recurring1.Size = new System.Drawing.Size(776, 25);
            this._recurring1.TabIndex = 2;
            // 
            // tab_journal
            // 
            this.tab_journal.BackColor = System.Drawing.Color.Turquoise;
            this.tab_journal.Controls.Add(this._dataGrid);
            this.tab_journal.Controls.Add(this.toolStrip2);
            this.tab_journal.Location = new System.Drawing.Point(4, 23);
            this.tab_journal.Name = "tab_journal";
            this.tab_journal.Padding = new System.Windows.Forms.Padding(3);
            this.tab_journal.Size = new System.Drawing.Size(782, 553);
            this.tab_journal.TabIndex = 1;
            this.tab_journal.Text = "2.tab_journal";
            this.tab_journal.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "check.png");
            this.imageList1.Images.SetKeyName(1, "forbidden.png");
            // 
            // _journalEntryFast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.Controls.Add(this._myTabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_journalEntryFast";
            this.Size = new System.Drawing.Size(790, 605);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this._myTabControl1.ResumeLayout(false);
            this.tab_template.ResumeLayout(false);
            this.tab_template.PerformLayout();
            this.tab_journal.ResumeLayout(false);
            this.tab_journal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SMLERPGLControl._glDetail _glTemplateDetail;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib._myGrid _dataGrid;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private MyLib.ToolStripMyButton _autoDateButton;
        private MyLib.ToolStripMyButton _autoRunningNumberButton;
        private MyLib.ToolStripMyButton _autoCalcButton;
        private MyLib.ToolStripMyButton _createDataGridButton;
        private MyLib.ToolStripMyButton _buttonClose;
        private MyLib.ToolStripMyButton _processButton;
        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage tab_template;
        private SMLERPGLControl._journalScreen _screenTop;
        private System.Windows.Forms.TabPage tab_journal;
        private SMLERPGLControl._recurring _recurring1;
        private System.Windows.Forms.ImageList imageList1;
    }
}
