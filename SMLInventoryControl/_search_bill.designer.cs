namespace SMLInventoryControl
{
    partial class _search_bill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_search_bill));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._myPanel1 = new MyLib._myPanel();
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new MyLib.ToolStripMyLabel();
            this._mytextboxsearch = new System.Windows.Forms.ToolStripTextBox();
            this._retrivenow = new MyLib.ToolStripMyButton();
            this._mySelectAll = new MyLib.ToolStripMyButton();
            this._myButton_ok = new MyLib.ToolStripMyButton();
            this._myButton_close = new MyLib.ToolStripMyButton();
            this._myPanel2 = new MyLib._myPanel();
            this._myGrid1 = new MyLib._myGrid();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._myPanel1.SuspendLayout();
            this._toolBar.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "check.png");
            this.imageList1.Images.SetKeyName(1, "forbidden.png");
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._toolBar);
            this._myPanel1.Controls.Add(this._myPanel2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(662, 409);
            this._myPanel1.TabIndex = 0;
            // 
            // _toolBar
            // 
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this._mytextboxsearch,
            this._retrivenow,
            this._mySelectAll,
            this._myButton_ok,
            this._myButton_close});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(662, 25);
            this._toolBar.TabIndex = 1;
            this._toolBar.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoToolTip = true;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripLabel1.ResourceName = "ค้นหา";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "ค้นหา";
            // 
            // _mytextboxsearch
            // 
            this._mytextboxsearch.Name = "_mytextboxsearch";
            this._mytextboxsearch.Size = new System.Drawing.Size(200, 25);
            this._mytextboxsearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            this._mytextboxsearch.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // _retrivenow
            // 
            this._retrivenow.Image = ((System.Drawing.Image)(resources.GetObject("_retrivenow.Image")));
            this._retrivenow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._retrivenow.Name = "_retrivenow";
            this._retrivenow.Padding = new System.Windows.Forms.Padding(1);
            this._retrivenow.ResourceName = "แสดงทันที";
            this._retrivenow.Size = new System.Drawing.Size(77, 22);
            this._retrivenow.Text = "แสดงทันที";
            this._retrivenow.Click += new System.EventHandler(this._autoRunningNumberButton_Click);
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
            this._myButton_ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._myButton_ok.Name = "_myButton_ok";
            this._myButton_ok.Padding = new System.Windows.Forms.Padding(1);
            this._myButton_ok.ResourceName = "ตกลง";
            this._myButton_ok.Size = new System.Drawing.Size(38, 22);
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
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._myGrid1);
            this._myPanel2.Controls.Add(this._statusStrip);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this._myPanel2.Size = new System.Drawing.Size(662, 409);
            this._myPanel2.TabIndex = 2;
            // 
            // _myGrid1
            // 
            this._myGrid1._extraWordShow = true;
            this._myGrid1._selectRow = -1;
            this._myGrid1.AllowDrop = true;
            this._myGrid1.AutoSize = true;
            this._myGrid1.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._myGrid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._myGrid1.DisplayRowNumber = false;
            this._myGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid1.Location = new System.Drawing.Point(0, 25);
            this._myGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid1.Name = "_myGrid1";
            this._myGrid1.Padding = new System.Windows.Forms.Padding(5);
            this._myGrid1.ShowTotal = true;
            this._myGrid1.Size = new System.Drawing.Size(662, 362);
            this._myGrid1.TabIndex = 0;
            this._myGrid1.TabStop = false;
            // 
            // _statusStrip
            // 
            this._statusStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._statusStrip.Location = new System.Drawing.Point(0, 387);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Padding = new System.Windows.Forms.Padding(1, 4, 16, 1);
            this._statusStrip.Size = new System.Drawing.Size(662, 22);
            this._statusStrip.TabIndex = 4;
            this._statusStrip.Text = "statusStrip1";
            // 
            // _Search_bill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 409);
            this.Controls.Add(this._myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_Search_bill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_Search_bill";
            this.Load += new System.EventHandler(this._search_bill_Load);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myGrid _myGrid1;
        private System.Windows.Forms.ToolStrip _toolBar;
        private MyLib._myPanel _myPanel2;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripTextBox _mytextboxsearch;
        private MyLib.ToolStripMyButton _myButton_ok;
        private MyLib.ToolStripMyButton _myButton_close;
        private MyLib.ToolStripMyButton _retrivenow;
        private System.Windows.Forms.ImageList imageList1;
        private MyLib.ToolStripMyLabel toolStripLabel1;
        private MyLib.ToolStripMyButton _mySelectAll;
    }
}