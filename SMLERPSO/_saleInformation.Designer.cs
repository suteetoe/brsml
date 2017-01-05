namespace SMLERPSO
{
    partial class _saleInformation
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this._arGrid = new MyLib._myGrid();
            this._myPanel1 = new MyLib._myPanel();
            this._searchAuto = new System.Windows.Forms.CheckBox();
            this._filterButton = new System.Windows.Forms.Button();
            this._searchTextbox = new System.Windows.Forms.TextBox();
            this._myLabel1 = new MyLib._myLabel();
            this._arDetailTextbox = new System.Windows.Forms.TextBox();
            this._gridIC = new MyLib._myGrid();
            this._myPanel2 = new MyLib._myPanel();
            this._icSearchTextbox = new System.Windows.Forms.TextBox();
            this._myLabel2 = new MyLib._myLabel();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.ar_doc_chq_status_screen1 = new SMLERPSO.ar_doc_chq_status_screen();
            this._gridICTrans = new MyLib._myGrid();
            this._gridSaleMonth = new MyLib._myGrid();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSaleInformation = new System.Windows.Forms.TabPage();
            this.tabSaleInformationByProduct = new System.Windows.Forms.TabPage();
            this._saleInformationByProduct1 = new SMLERPSO._saleInformationByProduct();
            this.tabSaleInformationBySale = new System.Windows.Forms.TabPage();
            this._saleInformationBySaleman1 = new SMLERPSO._saleInformationBySaleman();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabSaleInformation.SuspendLayout();
            this.tabSaleInformationByProduct.SuspendLayout();
            this.tabSaleInformationBySale.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1116, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPSO.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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
            this.splitContainer1.Panel2.Controls.Add(this._gridSaleMonth);
            this.splitContainer1.Size = new System.Drawing.Size(1100, 859);
            this.splitContainer1.SplitterDistance = 679;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(1100, 679);
            this.splitContainer2.SplitterDistance = 455;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._gridIC);
            this.splitContainer3.Panel2.Controls.Add(this._myPanel2);
            this.splitContainer3.Size = new System.Drawing.Size(455, 679);
            this.splitContainer3.SplitterDistance = 392;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer5
            // 
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this._arGrid);
            this.splitContainer5.Panel1.Controls.Add(this._myPanel1);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this._arDetailTextbox);
            this.splitContainer5.Size = new System.Drawing.Size(455, 392);
            this.splitContainer5.SplitterDistance = 203;
            this.splitContainer5.SplitterWidth = 1;
            this.splitContainer5.TabIndex = 0;
            // 
            // _arGrid
            // 
            this._arGrid._extraWordShow = true;
            this._arGrid._selectRow = -1;
            this._arGrid.BackColor = System.Drawing.SystemColors.Window;
            this._arGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._arGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._arGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._arGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._arGrid.Location = new System.Drawing.Point(0, 30);
            this._arGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._arGrid.Name = "_arGrid";
            this._arGrid.Size = new System.Drawing.Size(451, 169);
            this._arGrid.TabIndex = 0;
            this._arGrid.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._searchAuto);
            this._myPanel1.Controls.Add(this._filterButton);
            this._myPanel1.Controls.Add(this._searchTextbox);
            this._myPanel1.Controls.Add(this._myLabel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(451, 30);
            this._myPanel1.TabIndex = 1;
            // 
            // _searchAuto
            // 
            this._searchAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._searchAuto.AutoSize = true;
            this._searchAuto.BackColor = System.Drawing.Color.Transparent;
            this._searchAuto.Checked = true;
            this._searchAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this._searchAuto.Location = new System.Drawing.Point(367, 6);
            this._searchAuto.Name = "_searchAuto";
            this._searchAuto.Size = new System.Drawing.Size(53, 18);
            this._searchAuto.TabIndex = 3;
            this._searchAuto.Text = "Auto";
            this._searchAuto.UseVisualStyleBackColor = false;
            // 
            // _filterButton
            // 
            this._filterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._filterButton.Image = global::SMLERPSO.Properties.Resources.zoom_in;
            this._filterButton.Location = new System.Drawing.Point(422, 3);
            this._filterButton.Name = "_filterButton";
            this._filterButton.Size = new System.Drawing.Size(26, 23);
            this._filterButton.TabIndex = 2;
            this._filterButton.UseVisualStyleBackColor = true;
            this._filterButton.Click += new System.EventHandler(this._filterButton_Click);
            // 
            // _searchTextbox
            // 
            this._searchTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchTextbox.Location = new System.Drawing.Point(91, 4);
            this._searchTextbox.Name = "_searchTextbox";
            this._searchTextbox.Size = new System.Drawing.Size(271, 22);
            this._searchTextbox.TabIndex = 1;
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.BackColor = System.Drawing.Color.Transparent;
            this._myLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel1.Location = new System.Drawing.Point(3, 7);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "ข้อความค้นหา";
            this._myLabel1.Size = new System.Drawing.Size(82, 14);
            this._myLabel1.TabIndex = 0;
            this._myLabel1.Text = "ข้อความค้นหา";
            // 
            // _arDetailTextbox
            // 
            this._arDetailTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._arDetailTextbox.Location = new System.Drawing.Point(0, 0);
            this._arDetailTextbox.Multiline = true;
            this._arDetailTextbox.Name = "_arDetailTextbox";
            this._arDetailTextbox.Size = new System.Drawing.Size(451, 184);
            this._arDetailTextbox.TabIndex = 0;
            // 
            // _gridIC
            // 
            this._gridIC._extraWordShow = true;
            this._gridIC._selectRow = -1;
            this._gridIC.BackColor = System.Drawing.SystemColors.Window;
            this._gridIC.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridIC.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridIC.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridIC.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridIC.Location = new System.Drawing.Point(0, 30);
            this._gridIC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridIC.Name = "_gridIC";
            this._gridIC.Size = new System.Drawing.Size(451, 252);
            this._gridIC.TabIndex = 0;
            this._gridIC.TabStop = false;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._icSearchTextbox);
            this._myPanel2.Controls.Add(this._myLabel2);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Size = new System.Drawing.Size(451, 30);
            this._myPanel2.TabIndex = 2;
            // 
            // _icSearchTextbox
            // 
            this._icSearchTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._icSearchTextbox.Location = new System.Drawing.Point(91, 4);
            this._icSearchTextbox.Name = "_icSearchTextbox";
            this._icSearchTextbox.Size = new System.Drawing.Size(357, 22);
            this._icSearchTextbox.TabIndex = 1;
            // 
            // _myLabel2
            // 
            this._myLabel2.AutoSize = true;
            this._myLabel2.BackColor = System.Drawing.Color.Transparent;
            this._myLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel2.Location = new System.Drawing.Point(3, 7);
            this._myLabel2.Name = "_myLabel2";
            this._myLabel2.ResourceName = "ข้อความค้นหา";
            this._myLabel2.Size = new System.Drawing.Size(82, 14);
            this._myLabel2.TabIndex = 0;
            this._myLabel2.Text = "ข้อความค้นหา";
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.ar_doc_chq_status_screen1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this._gridICTrans);
            this.splitContainer4.Size = new System.Drawing.Size(644, 679);
            this.splitContainer4.SplitterDistance = 392;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 0;
            // 
            // ar_doc_chq_status_screen1
            // 
            this.ar_doc_chq_status_screen1._isChange = false;
            this.ar_doc_chq_status_screen1.BackColor = System.Drawing.Color.Transparent;
            this.ar_doc_chq_status_screen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ar_doc_chq_status_screen1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ar_doc_chq_status_screen1.Location = new System.Drawing.Point(0, 0);
            this.ar_doc_chq_status_screen1.Name = "ar_doc_chq_status_screen1";
            this.ar_doc_chq_status_screen1.Size = new System.Drawing.Size(640, 388);
            this.ar_doc_chq_status_screen1.TabIndex = 0;
            // 
            // _gridICTrans
            // 
            this._gridICTrans._extraWordShow = true;
            this._gridICTrans._selectRow = -1;
            this._gridICTrans.BackColor = System.Drawing.SystemColors.Window;
            this._gridICTrans.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridICTrans.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridICTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridICTrans.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridICTrans.Location = new System.Drawing.Point(0, 0);
            this._gridICTrans.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridICTrans.Name = "_gridICTrans";
            this._gridICTrans.Size = new System.Drawing.Size(640, 282);
            this._gridICTrans.TabIndex = 0;
            this._gridICTrans.TabStop = false;
            // 
            // _gridSaleMonth
            // 
            this._gridSaleMonth._extraWordShow = true;
            this._gridSaleMonth._selectRow = -1;
            this._gridSaleMonth.BackColor = System.Drawing.SystemColors.Window;
            this._gridSaleMonth.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridSaleMonth.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridSaleMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridSaleMonth.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridSaleMonth.Location = new System.Drawing.Point(0, 0);
            this._gridSaleMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridSaleMonth.Name = "_gridSaleMonth";
            this._gridSaleMonth.Size = new System.Drawing.Size(1096, 175);
            this._gridSaleMonth.TabIndex = 0;
            this._gridSaleMonth.TabStop = false;
            // 
            // _timer
            // 
            this._timer.Interval = 500;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSaleInformation);
            this.tabControl1.Controls.Add(this.tabSaleInformationByProduct);
            this.tabControl1.Controls.Add(this.tabSaleInformationBySale);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1116, 894);
            this.tabControl1.TabIndex = 1;
            // 
            // tabSaleInformation
            // 
            this.tabSaleInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabSaleInformation.Controls.Add(this.splitContainer1);
            this.tabSaleInformation.Location = new System.Drawing.Point(4, 23);
            this.tabSaleInformation.Name = "tabSaleInformation";
            this.tabSaleInformation.Padding = new System.Windows.Forms.Padding(3);
            this.tabSaleInformation.Size = new System.Drawing.Size(1108, 867);
            this.tabSaleInformation.TabIndex = 0;
            this.tabSaleInformation.Text = "Sale Information";
            this.tabSaleInformation.UseVisualStyleBackColor = true;
            // 
            // tabSaleInformationByProduct
            // 
            this.tabSaleInformationByProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabSaleInformationByProduct.Controls.Add(this._saleInformationByProduct1);
            this.tabSaleInformationByProduct.Location = new System.Drawing.Point(4, 23);
            this.tabSaleInformationByProduct.Name = "tabSaleInformationByProduct";
            this.tabSaleInformationByProduct.Padding = new System.Windows.Forms.Padding(3);
            this.tabSaleInformationByProduct.Size = new System.Drawing.Size(1108, 867);
            this.tabSaleInformationByProduct.TabIndex = 1;
            this.tabSaleInformationByProduct.Text = "By Product";
            this.tabSaleInformationByProduct.UseVisualStyleBackColor = true;
            // 
            // _saleInformationByProduct1
            // 
            this._saleInformationByProduct1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._saleInformationByProduct1.Location = new System.Drawing.Point(3, 3);
            this._saleInformationByProduct1.Name = "_saleInformationByProduct1";
            this._saleInformationByProduct1.Size = new System.Drawing.Size(1100, 859);
            this._saleInformationByProduct1.TabIndex = 1;
            // 
            // tabSaleInformationBySale
            // 
            this.tabSaleInformationBySale.Controls.Add(this._saleInformationBySaleman1);
            this.tabSaleInformationBySale.Location = new System.Drawing.Point(4, 23);
            this.tabSaleInformationBySale.Name = "tabSaleInformationBySale";
            this.tabSaleInformationBySale.Size = new System.Drawing.Size(1108, 867);
            this.tabSaleInformationBySale.TabIndex = 2;
            this.tabSaleInformationBySale.Text = "By Saleman";
            this.tabSaleInformationBySale.UseVisualStyleBackColor = true;
            // 
            // _saleInformationBySaleman1
            // 
            this._saleInformationBySaleman1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._saleInformationBySaleman1.Location = new System.Drawing.Point(0, 0);
            this._saleInformationBySaleman1.Name = "_saleInformationBySaleman1";
            this._saleInformationBySaleman1.Size = new System.Drawing.Size(1108, 867);
            this._saleInformationBySaleman1.TabIndex = 0;
            // 
            // _saleInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_saleInformation";
            this.Size = new System.Drawing.Size(1116, 919);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            this.splitContainer5.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabSaleInformation.ResumeLayout(false);
            this.tabSaleInformationByProduct.ResumeLayout(false);
            this.tabSaleInformationBySale.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private MyLib._myGrid _arGrid;
        private MyLib._myGrid _gridIC;
        private MyLib._myGrid _gridICTrans;
        private MyLib._myGrid _gridSaleMonth;
        private System.Windows.Forms.TextBox _arDetailTextbox;
        private ar_doc_chq_status_screen ar_doc_chq_status_screen1;
        private MyLib._myPanel _myPanel1;
        private MyLib._myLabel _myLabel1;
        private System.Windows.Forms.TextBox _searchTextbox;
        private System.Windows.Forms.Button _filterButton;
        private System.Windows.Forms.CheckBox _searchAuto;
        private System.Windows.Forms.Timer _timer;
        private MyLib._myPanel _myPanel2;
        private System.Windows.Forms.TextBox _icSearchTextbox;
        private MyLib._myLabel _myLabel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSaleInformation;
        private System.Windows.Forms.TabPage tabSaleInformationByProduct;
        private _saleInformationByProduct _saleInformationByProduct1;
        private System.Windows.Forms.TabPage tabSaleInformationBySale;
        private _saleInformationBySaleman _saleInformationBySaleman1;
    }
}
