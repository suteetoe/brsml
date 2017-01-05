namespace DTSClientDownload
{
    partial class _download_log
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
            this.label1 = new System.Windows.Forms.Label();
            this._searchTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._downloadDate = new MyLib._myDateBox();
            this._searchButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._gridDownloadItem = new MyLib._myGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this._searchItem = new System.Windows.Forms.CheckBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._gridDownloadPO = new MyLib._myGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this._searchPO = new System.Windows.Forms.CheckBox();
            this._gridDownloadSO = new MyLib._myGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this._searchSO = new System.Windows.Forms.CheckBox();
            this._footPanel = new System.Windows.Forms.Panel();
            this._exportButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this._footPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "ข้อความค้นหา : ";
            // 
            // _searchTextbox
            // 
            this._searchTextbox.Location = new System.Drawing.Point(100, 13);
            this._searchTextbox.Name = "_searchTextbox";
            this._searchTextbox.Size = new System.Drawing.Size(233, 22);
            this._searchTextbox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "วันที่รับข้อมูล";
            // 
            // _downloadDate
            // 
            this._downloadDate._column = 0;
            this._downloadDate._dateTime = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._downloadDate._dateTimeOld = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._downloadDate._defaultBackGround = System.Drawing.Color.White;
            this._downloadDate._emtry = true;
            this._downloadDate._enterToTab = false;
            this._downloadDate._icon = true;
            this._downloadDate._iconNumber = 1;
            this._downloadDate._isChange = false;
            this._downloadDate._isQuery = true;
            this._downloadDate._isSearch = false;
            this._downloadDate._isTime = false;
            this._downloadDate._labelName = "";
            this._downloadDate._lostFocust = true;
            this._downloadDate._maxColumn = 0;
            this._downloadDate._name = null;
            this._downloadDate._row = 0;
            this._downloadDate._rowCount = 0;
            this._downloadDate._textFirst = "";
            this._downloadDate._textLast = "";
            this._downloadDate._textSecond = "";
            this._downloadDate._upperCase = false;
            this._downloadDate._warning = true;
            this._downloadDate.BackColor = System.Drawing.Color.Transparent;
            this._downloadDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._downloadDate.ForeColor = System.Drawing.Color.Black;
            this._downloadDate.IsUpperCase = false;
            this._downloadDate.Location = new System.Drawing.Point(431, 13);
            this._downloadDate.Margin = new System.Windows.Forms.Padding(0);
            this._downloadDate.MaxLength = 0;
            this._downloadDate.Name = "_downloadDate";
            this._downloadDate.ShowIcon = true;
            this._downloadDate.Size = new System.Drawing.Size(193, 22);
            this._downloadDate.TabIndex = 3;
            // 
            // _searchButton
            // 
            this._searchButton.Location = new System.Drawing.Point(662, 12);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(89, 23);
            this._searchButton.TabIndex = 4;
            this._searchButton.Text = "ค้นหา";
            this._searchButton.UseVisualStyleBackColor = true;
            this._searchButton.Click += new System.EventHandler(this._searchButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._searchButton);
            this.panel1.Controls.Add(this._downloadDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this._searchTextbox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1143, 47);
            this.panel1.TabIndex = 5;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 47);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._gridDownloadItem);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1143, 699);
            this.splitContainer1.SplitterDistance = 488;
            this.splitContainer1.TabIndex = 6;
            // 
            // _gridDownloadItem
            // 
            this._gridDownloadItem._extraWordShow = true;
            this._gridDownloadItem._selectRow = -1;
            this._gridDownloadItem.BackColor = System.Drawing.SystemColors.Window;
            this._gridDownloadItem.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridDownloadItem.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridDownloadItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridDownloadItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridDownloadItem.Location = new System.Drawing.Point(0, 40);
            this._gridDownloadItem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridDownloadItem.Name = "_gridDownloadItem";
            this._gridDownloadItem.Size = new System.Drawing.Size(488, 659);
            this._gridDownloadItem.TabIndex = 1;
            this._gridDownloadItem.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._searchItem);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(488, 40);
            this.panel2.TabIndex = 0;
            // 
            // _searchItem
            // 
            this._searchItem.AutoSize = true;
            this._searchItem.Location = new System.Drawing.Point(20, 14);
            this._searchItem.Name = "_searchItem";
            this._searchItem.Size = new System.Drawing.Size(127, 18);
            this._searchItem.TabIndex = 0;
            this._searchItem.Text = "ข้อมูลสินค้าจาก SCG";
            this._searchItem.UseVisualStyleBackColor = true;
            this._searchItem.CheckedChanged += new System.EventHandler(this._searchItem_CheckedChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._gridDownloadPO);
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._gridDownloadSO);
            this.splitContainer2.Panel2.Controls.Add(this.panel4);
            this.splitContainer2.Size = new System.Drawing.Size(651, 699);
            this.splitContainer2.SplitterDistance = 344;
            this.splitContainer2.TabIndex = 0;
            // 
            // _gridDownloadPO
            // 
            this._gridDownloadPO._extraWordShow = true;
            this._gridDownloadPO._selectRow = -1;
            this._gridDownloadPO.BackColor = System.Drawing.SystemColors.Window;
            this._gridDownloadPO.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridDownloadPO.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridDownloadPO.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridDownloadPO.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridDownloadPO.Location = new System.Drawing.Point(0, 40);
            this._gridDownloadPO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridDownloadPO.Name = "_gridDownloadPO";
            this._gridDownloadPO.Size = new System.Drawing.Size(344, 659);
            this._gridDownloadPO.TabIndex = 2;
            this._gridDownloadPO.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this._searchPO);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(344, 40);
            this.panel3.TabIndex = 1;
            // 
            // _searchPO
            // 
            this._searchPO.AutoSize = true;
            this._searchPO.Location = new System.Drawing.Point(23, 14);
            this._searchPO.Name = "_searchPO";
            this._searchPO.Size = new System.Drawing.Size(140, 18);
            this._searchPO.TabIndex = 1;
            this._searchPO.Text = "ใบสั่งซื้อสินค้าจาก SCG";
            this._searchPO.UseVisualStyleBackColor = true;
            this._searchPO.CheckedChanged += new System.EventHandler(this._searchItem_CheckedChanged);
            // 
            // _gridDownloadSO
            // 
            this._gridDownloadSO._extraWordShow = true;
            this._gridDownloadSO._selectRow = -1;
            this._gridDownloadSO.BackColor = System.Drawing.SystemColors.Window;
            this._gridDownloadSO.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridDownloadSO.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridDownloadSO.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridDownloadSO.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridDownloadSO.Location = new System.Drawing.Point(0, 40);
            this._gridDownloadSO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridDownloadSO.Name = "_gridDownloadSO";
            this._gridDownloadSO.Size = new System.Drawing.Size(303, 659);
            this._gridDownloadSO.TabIndex = 2;
            this._gridDownloadSO.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this._searchSO);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(303, 40);
            this.panel4.TabIndex = 1;
            // 
            // _searchSO
            // 
            this._searchSO.AutoSize = true;
            this._searchSO.Location = new System.Drawing.Point(25, 14);
            this._searchSO.Name = "_searchSO";
            this._searchSO.Size = new System.Drawing.Size(183, 18);
            this._searchSO.TabIndex = 2;
            this._searchSO.Text = "ใบสั่งขาย/สั่งจองสินค้าจาก SCG";
            this._searchSO.UseVisualStyleBackColor = true;
            this._searchSO.CheckedChanged += new System.EventHandler(this._searchItem_CheckedChanged);
            // 
            // _footPanel
            // 
            this._footPanel.Controls.Add(this._exportButton);
            this._footPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footPanel.Location = new System.Drawing.Point(0, 746);
            this._footPanel.Name = "_footPanel";
            this._footPanel.Size = new System.Drawing.Size(1143, 36);
            this._footPanel.TabIndex = 3;
            // 
            // _exportButton
            // 
            this._exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._exportButton.Location = new System.Drawing.Point(1040, 7);
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new System.Drawing.Size(100, 23);
            this._exportButton.TabIndex = 0;
            this._exportButton.Text = "Export... ";
            this._exportButton.UseVisualStyleBackColor = true;
            this._exportButton.Click += new System.EventHandler(this._exportButton_Click);
            // 
            // _download_log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._footPanel);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_download_log";
            this.Size = new System.Drawing.Size(1143, 782);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this._footPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _searchTextbox;
        private System.Windows.Forms.Label label2;
        private MyLib._myDateBox _downloadDate;
        private System.Windows.Forms.Button _searchButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox _searchItem;
        private System.Windows.Forms.CheckBox _searchPO;
        private System.Windows.Forms.CheckBox _searchSO;
        private MyLib._myGrid _gridDownloadItem;
        private MyLib._myGrid _gridDownloadPO;
        private MyLib._myGrid _gridDownloadSO;
        private System.Windows.Forms.Panel _footPanel;
        private System.Windows.Forms.Button _exportButton;

    }
}
