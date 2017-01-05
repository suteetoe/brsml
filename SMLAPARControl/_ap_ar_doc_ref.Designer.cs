namespace SMLERPAPARControl
{
    partial class _ap_ar_doc_ref
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_ap_ar_doc_ref));
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new MyLib.ToolStripMyLabel();
            this._mytextboxsearch = new System.Windows.Forms.ToolStripTextBox();
            this._mySelectAll = new MyLib.ToolStripMyButton();
            this._myButton_ok = new MyLib.ToolStripMyButton();
            this._myButton_close = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._grouper1 = new MyLib._grouper();
            this._ap_ar_doc_ref_grid1 = new SMLERPAPARControl._ap_ar_doc_ref_grid();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._myToolbar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolbar
            // 
            this._myToolbar.BackgroundImage = global::SMLERPAPARControl.Properties.Resources.bt03;
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this._mytextboxsearch,
            this._mySelectAll,
            this._myButton_ok,
            this._myButton_close});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(996, 25);
            this._myToolbar.TabIndex = 2;
            this._myToolbar.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoToolTip = true;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripLabel1.ResourceName = "ค้นหา";
            this.toolStripLabel1.Size = new System.Drawing.Size(62, 22);
            this.toolStripLabel1.Text = "ค้นหาข้อมูล";
            // 
            // _mytextboxsearch
            // 
            this._mytextboxsearch.Name = "_mytextboxsearch";
            this._mytextboxsearch.Size = new System.Drawing.Size(200, 25);
            this._mytextboxsearch.TextChanged += new System.EventHandler(this._mytextboxsearch_TextChanged);
            // 
            // _mySelectAll
            // 
            this._mySelectAll.Image = ((System.Drawing.Image)(resources.GetObject("_mySelectAll.Image")));
            this._mySelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mySelectAll.Name = "_mySelectAll";
            this._mySelectAll.Padding = new System.Windows.Forms.Padding(1);
            this._mySelectAll.ResourceName = "เลือกทั้งหมด";
            this._mySelectAll.Size = new System.Drawing.Size(86, 22);
            this._mySelectAll.Text = "เลือกทั้งหมด";
            this._mySelectAll.Click += new System.EventHandler(this._mySelectAll_Click);
            // 
            // _myButton_ok
            // 
            this._myButton_ok.Image = ((System.Drawing.Image)(resources.GetObject("_myButton_ok.Image")));
            this._myButton_ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._myButton_ok.Name = "_myButton_ok";
            this._myButton_ok.Padding = new System.Windows.Forms.Padding(1);
            this._myButton_ok.ResourceName = "ตกลง";
            this._myButton_ok.Size = new System.Drawing.Size(54, 22);
            this._myButton_ok.Text = "ตกลง";
            this._myButton_ok.Click += new System.EventHandler(this._myButton_ok_Click);
            // 
            // _myButton_close
            // 
            this._myButton_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._myButton_close.Name = "_myButton_close";
            this._myButton_close.Padding = new System.Windows.Forms.Padding(1);
            this._myButton_close.ResourceName = "ปิดหน้าจอ";
            this._myButton_close.Size = new System.Drawing.Size(59, 22);
            this._myButton_close.Text = "ปิดหน้าจอ";
            this._myButton_close.Click += new System.EventHandler(this._myButton_close_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouper1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.SkyBlue;
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(996, 328);
            this._myPanel1.TabIndex = 10;
            // 
            // _grouper1
            // 
            this._grouper1.AutoSize = true;
            this._grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouper1.BackgroundGradientColor = System.Drawing.SystemColors.ControlLight;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper1.BorderColor = System.Drawing.Color.White;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._ap_ar_doc_ref_grid1);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "";
            this._grouper1.Location = new System.Drawing.Point(5, 5);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(5);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(986, 318);
            this._grouper1.TabIndex = 17;
            // 
            // _ap_ar_doc_ref_grid1
            // 
            this._ap_ar_doc_ref_grid1._extraWordShow = true;
            this._ap_ar_doc_ref_grid1._selectRow = -1;
            this._ap_ar_doc_ref_grid1.AllowDrop = true;
            this._ap_ar_doc_ref_grid1.AutoSize = true;
            this._ap_ar_doc_ref_grid1.BackColor = System.Drawing.SystemColors.Window;
            this._ap_ar_doc_ref_grid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._ap_ar_doc_ref_grid1.ColumnBackgroundAuto = false;
            this._ap_ar_doc_ref_grid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._ap_ar_doc_ref_grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ap_ar_doc_ref_grid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._ap_ar_doc_ref_grid1.IsEdit = false;
            this._ap_ar_doc_ref_grid1.Location = new System.Drawing.Point(5, 5);
            this._ap_ar_doc_ref_grid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ap_ar_doc_ref_grid1.Name = "_ap_ar_doc_ref_grid1";
            this._ap_ar_doc_ref_grid1.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._ap_ar_doc_ref_grid1.Size = new System.Drawing.Size(976, 308);
            this._ap_ar_doc_ref_grid1.TabIndex = 0;
            this._ap_ar_doc_ref_grid1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon_selected.png");
            this.imageList1.Images.SetKeyName(1, "icon_select.png");
            // 
            // _ap_ar_doc_ref
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 353);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myToolbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.ImeMode = System.Windows.Forms.ImeMode.HangulFull;
            this.Name = "_ap_ar_doc_ref";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._grouper1.ResumeLayout(false);
            this._grouper1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox _mytextboxsearch;
        private MyLib.ToolStripMyButton _mySelectAll;
        private MyLib.ToolStripMyButton _myButton_ok;
        private MyLib.ToolStripMyButton _myButton_close;
        private MyLib._myPanel _myPanel1;
        private MyLib._grouper _grouper1;
        private _ap_ar_doc_ref_grid _ap_ar_doc_ref_grid1;
        private System.Windows.Forms.ImageList imageList1;
    }
}