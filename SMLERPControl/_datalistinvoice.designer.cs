namespace SMLERPControl
{
    partial class _datalistinvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_datalistinvoice));
            this._myPanel1 = new MyLib._myPanel();
            this._dataList = new MyLib._myGrid();
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new MyLib.ToolStripMyLabel();
            this._mytextboxsearch = new System.Windows.Forms.ToolStripTextBox();
            this._infoLabel = new MyLib.ToolStripMyLabel();
            this._retrivenow = new MyLib.ToolStripMyButton();
            this._mySelectAll = new MyLib.ToolStripMyButton();
            this._myButton_ok = new MyLib.ToolStripMyButton();
            this._myButton_close = new MyLib.ToolStripMyButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._myPanel1.SuspendLayout();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._dataList);
            this._myPanel1.Controls.Add(this._toolBar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(625, 439);
            this._myPanel1.TabIndex = 1;
            // 
            // _dataList
            // 
            this._dataList.BackColor = System.Drawing.SystemColors.Window;
            this._dataList.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._dataList.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._dataList.DisplayRowNumber = false;
            this._dataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataList.Location = new System.Drawing.Point(0, 25);
            this._dataList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dataList.Name = "_dataList";
            this._dataList.Size = new System.Drawing.Size(625, 414);
            this._dataList.TabIndex = 0;
            this._dataList.TabStop = false;
            // 
            // _toolBar
            // 
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this._mytextboxsearch,
            this._infoLabel,
            this._retrivenow,
            this._mySelectAll,
            this._myButton_ok,
            this._myButton_close});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(625, 25);
            this._toolBar.TabIndex = 8;
            this._toolBar.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoToolTip = true;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.ResourceName = "ค้นหา";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel1.Text = "ค้นหา :";
            // 
            // _mytextboxsearch
            // 
            this._mytextboxsearch.Name = "_mytextboxsearch";
            this._mytextboxsearch.Size = new System.Drawing.Size(200, 25);
            // 
            // _infoLabel
            // 
            this._infoLabel.Name = "_infoLabel";
            this._infoLabel.Padding = new System.Windows.Forms.Padding(1);
            this._infoLabel.Size = new System.Drawing.Size(2, 22);
            // 
            // _retrivenow
            // 
            this._retrivenow.Image = ((System.Drawing.Image)(resources.GetObject("_retrivenow.Image")));
            this._retrivenow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._retrivenow.Name = "_retrivenow";
            this._retrivenow.ResourceName = "แสดงทันที";
            this._retrivenow.Padding = new System.Windows.Forms.Padding(1);
            this._retrivenow.Size = new System.Drawing.Size(87, 22);
            this._retrivenow.Text = "Retrive now";
            this._retrivenow.Click += new System.EventHandler(this._retrivenow_Click);
            // 
            // _mySelectAll
            // 
            this._mySelectAll.Image = ((System.Drawing.Image)(resources.GetObject("_mySelectAll.Image")));
            this._mySelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mySelectAll.Name = "_mySelectAll";
            this._mySelectAll.ResourceName = "เลือกทั้งหมด";
            this._mySelectAll.Padding = new System.Windows.Forms.Padding(1);
            this._mySelectAll.Size = new System.Drawing.Size(88, 22);
            this._mySelectAll.Text = "เลือกทั้งหมด";
            this._mySelectAll.Click += new System.EventHandler(this._mySelectAll_Click);
            // 
            // _myButton_ok
            // 
            this._myButton_ok.Image = global::SMLERPControl.Properties.Resources.AsagiOk;
            this._myButton_ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._myButton_ok.Name = "_myButton_ok";
            this._myButton_ok.ResourceName = "บันทึกข้อมูล (F12)";
            this._myButton_ok.Padding = new System.Windows.Forms.Padding(1);
            this._myButton_ok.Size = new System.Drawing.Size(55, 22);
            this._myButton_ok.Text = "ตกลง";
            this._myButton_ok.Click += new System.EventHandler(this._myButton_ok_Click);
            // 
            // _myButton_close
            // 
            this._myButton_close.Image = global::SMLERPControl.Properties.Resources.delete2;
            this._myButton_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._myButton_close.Name = "_myButton_close";
            this._myButton_close.ResourceName = "ปิดหน้าจอ";
            this._myButton_close.Padding = new System.Windows.Forms.Padding(1);
            this._myButton_close.Size = new System.Drawing.Size(43, 22);
            this._myButton_close.Text = "ปิด";
            this._myButton_close.Click += new System.EventHandler(this._myButton_close_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "check.png");
            this.imageList1.Images.SetKeyName(1, "forbidden.png");
            // 
            // _statusStrip
            // 
            this._statusStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._statusStrip.Location = new System.Drawing.Point(0, 417);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Padding = new System.Windows.Forms.Padding(1, 4, 16, 1);
            this._statusStrip.Size = new System.Drawing.Size(625, 22);
            this._statusStrip.TabIndex = 10;
            this._statusStrip.Text = "statusStrip1";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // _datalistinvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this._myPanel1);
            this.Name = "_datalistinvoice";
            this.Size = new System.Drawing.Size(625, 439);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip _toolBar;
        private MyLib.ToolStripMyLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox _mytextboxsearch;
        private MyLib.ToolStripMyLabel _infoLabel;
        private MyLib.ToolStripMyButton _retrivenow;
        private MyLib.ToolStripMyButton _mySelectAll;
        private MyLib.ToolStripMyButton _myButton_ok;
        private MyLib.ToolStripMyButton _myButton_close;
        public MyLib._myGrid _dataList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.Timer timer1;
    }
}
