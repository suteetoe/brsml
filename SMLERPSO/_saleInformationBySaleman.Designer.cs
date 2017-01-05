namespace SMLERPSO
{
    partial class _saleInformationBySaleman
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
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._docGrid = new MyLib._myGrid();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._salemanGrid = new MyLib._myGrid();
            this._myPanel3 = new MyLib._myPanel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this._myLabel3 = new MyLib._myLabel();
            this._searchSaleManTextbox = new System.Windows.Forms.TextBox();
            this._icGrid = new MyLib._myGrid();
            this._myPanel1 = new MyLib._myPanel();
            this._searchAuto = new System.Windows.Forms.CheckBox();
            this._myLabel1 = new MyLib._myLabel();
            this._searchICTextbox = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this._myLabel2 = new MyLib._myLabel();
            this._saleGrid = new MyLib._myGrid();
            this._arGrid = new MyLib._myGrid();
            this._myPanel2 = new MyLib._myPanel();
            this._searchArTextbox = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this._myPanel3.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _timer
            // 
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _docGrid
            // 
            this._docGrid._extraWordShow = true;
            this._docGrid._selectRow = -1;
            this._docGrid.BackColor = System.Drawing.SystemColors.Window;
            this._docGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._docGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._docGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._docGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._docGrid.Location = new System.Drawing.Point(0, 0);
            this._docGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._docGrid.Name = "_docGrid";
            this._docGrid.Size = new System.Drawing.Size(530, 564);
            this._docGrid.TabIndex = 9;
            this._docGrid.TabStop = false;
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
            this.splitContainer3.Panel1.Controls.Add(this._salemanGrid);
            this.splitContainer3.Panel1.Controls.Add(this._myPanel3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._icGrid);
            this.splitContainer3.Panel2.Controls.Add(this._myPanel1);
            this.splitContainer3.Size = new System.Drawing.Size(547, 376);
            this.splitContainer3.SplitterDistance = 188;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
            // 
            // _salemanGrid
            // 
            this._salemanGrid._extraWordShow = true;
            this._salemanGrid._selectRow = -1;
            this._salemanGrid.BackColor = System.Drawing.SystemColors.Window;
            this._salemanGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._salemanGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._salemanGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._salemanGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._salemanGrid.Location = new System.Drawing.Point(0, 29);
            this._salemanGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._salemanGrid.Name = "_salemanGrid";
            this._salemanGrid.Size = new System.Drawing.Size(543, 155);
            this._salemanGrid.TabIndex = 10;
            this._salemanGrid.TabStop = false;
            // 
            // _myPanel3
            // 
            this._myPanel3._switchTabAuto = false;
            this._myPanel3.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel3.Controls.Add(this.checkBox2);
            this._myPanel3.Controls.Add(this._myLabel3);
            this._myPanel3.Controls.Add(this._searchSaleManTextbox);
            this._myPanel3.CornerPicture = null;
            this._myPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel3.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel3.Location = new System.Drawing.Point(0, 0);
            this._myPanel3.Name = "_myPanel3";
            this._myPanel3.Size = new System.Drawing.Size(543, 29);
            this._myPanel3.TabIndex = 11;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(500, 6);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 17);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Auto";
            this.checkBox2.UseVisualStyleBackColor = false;
            // 
            // _myLabel3
            // 
            this._myLabel3.AutoSize = true;
            this._myLabel3.BackColor = System.Drawing.Color.Transparent;
            this._myLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel3.Location = new System.Drawing.Point(3, 7);
            this._myLabel3.Name = "_myLabel3";
            this._myLabel3.ResourceName = "ข้อความค้นหา";
            this._myLabel3.Size = new System.Drawing.Size(82, 14);
            this._myLabel3.TabIndex = 1;
            this._myLabel3.Text = "ข้อความค้นหา";
            // 
            // _searchSaleManTextbox
            // 
            this._searchSaleManTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchSaleManTextbox.BackColor = System.Drawing.SystemColors.Window;
            this._searchSaleManTextbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._searchSaleManTextbox.ForeColor = System.Drawing.Color.Black;
            this._searchSaleManTextbox.Location = new System.Drawing.Point(88, 4);
            this._searchSaleManTextbox.Margin = new System.Windows.Forms.Padding(0);
            this._searchSaleManTextbox.MaxLength = 0;
            this._searchSaleManTextbox.Name = "_searchSaleManTextbox";
            this._searchSaleManTextbox.Size = new System.Drawing.Size(405, 22);
            this._searchSaleManTextbox.TabIndex = 0;
            // 
            // _icGrid
            // 
            this._icGrid._extraWordShow = true;
            this._icGrid._selectRow = -1;
            this._icGrid.BackColor = System.Drawing.SystemColors.Window;
            this._icGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icGrid.Location = new System.Drawing.Point(0, 29);
            this._icGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icGrid.Name = "_icGrid";
            this._icGrid.Size = new System.Drawing.Size(543, 154);
            this._icGrid.TabIndex = 6;
            this._icGrid.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._searchAuto);
            this._myPanel1.Controls.Add(this._myLabel1);
            this._myPanel1.Controls.Add(this._searchICTextbox);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(543, 29);
            this._myPanel1.TabIndex = 7;
            // 
            // _searchAuto
            // 
            this._searchAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._searchAuto.AutoSize = true;
            this._searchAuto.BackColor = System.Drawing.Color.Transparent;
            this._searchAuto.Checked = true;
            this._searchAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this._searchAuto.Location = new System.Drawing.Point(500, 6);
            this._searchAuto.Name = "_searchAuto";
            this._searchAuto.Size = new System.Drawing.Size(48, 17);
            this._searchAuto.TabIndex = 2;
            this._searchAuto.Text = "Auto";
            this._searchAuto.UseVisualStyleBackColor = false;
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
            this._myLabel1.TabIndex = 1;
            this._myLabel1.Text = "ข้อความค้นหา";
            // 
            // _searchICTextbox
            // 
            this._searchICTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchICTextbox.BackColor = System.Drawing.SystemColors.Window;
            this._searchICTextbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._searchICTextbox.ForeColor = System.Drawing.Color.Black;
            this._searchICTextbox.Location = new System.Drawing.Point(88, 4);
            this._searchICTextbox.Margin = new System.Windows.Forms.Padding(0);
            this._searchICTextbox.MaxLength = 0;
            this._searchICTextbox.Name = "_searchICTextbox";
            this._searchICTextbox.Size = new System.Drawing.Size(405, 22);
            this._searchICTextbox.TabIndex = 0;
            this._searchICTextbox.TextChanged += new System.EventHandler(this._searchICTextbox_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(492, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Auto";
            this.checkBox1.UseVisualStyleBackColor = false;
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
            this._myLabel2.TabIndex = 1;
            this._myLabel2.Text = "ข้อความค้นหา";
            // 
            // _saleGrid
            // 
            this._saleGrid._extraWordShow = true;
            this._saleGrid._selectRow = -1;
            this._saleGrid.BackColor = System.Drawing.SystemColors.Window;
            this._saleGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._saleGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._saleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._saleGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._saleGrid.Location = new System.Drawing.Point(0, 0);
            this._saleGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._saleGrid.Name = "_saleGrid";
            this._saleGrid.Size = new System.Drawing.Size(1078, 208);
            this._saleGrid.TabIndex = 9;
            this._saleGrid.TabStop = false;
            // 
            // _arGrid
            // 
            this._arGrid._extraWordShow = true;
            this._arGrid._selectRow = -1;
            this._arGrid.BackColor = System.Drawing.SystemColors.Window;
            this._arGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._arGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._arGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._arGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._arGrid.Location = new System.Drawing.Point(0, 29);
            this._arGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._arGrid.Name = "_arGrid";
            this._arGrid.Size = new System.Drawing.Size(543, 158);
            this._arGrid.TabIndex = 8;
            this._arGrid.TabStop = false;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this.checkBox1);
            this._myPanel2.Controls.Add(this._myLabel2);
            this._myPanel2.Controls.Add(this._searchArTextbox);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Size = new System.Drawing.Size(543, 29);
            this._myPanel2.TabIndex = 9;
            // 
            // _searchArTextbox
            // 
            this._searchArTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchArTextbox.BackColor = System.Drawing.SystemColors.Window;
            this._searchArTextbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._searchArTextbox.ForeColor = System.Drawing.Color.Black;
            this._searchArTextbox.Location = new System.Drawing.Point(88, 4);
            this._searchArTextbox.Margin = new System.Windows.Forms.Padding(0);
            this._searchArTextbox.MaxLength = 0;
            this._searchArTextbox.Name = "_searchArTextbox";
            this._searchArTextbox.Size = new System.Drawing.Size(397, 22);
            this._searchArTextbox.TabIndex = 0;
            this._searchArTextbox.TextChanged += new System.EventHandler(this._searchArTextbox_TextChanged);
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
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this._arGrid);
            this.splitContainer4.Panel2.Controls.Add(this._myPanel2);
            this.splitContainer4.Size = new System.Drawing.Size(547, 568);
            this.splitContainer4.SplitterDistance = 376;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 0;
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
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._docGrid);
            this.splitContainer2.Size = new System.Drawing.Size(1082, 568);
            this.splitContainer2.SplitterDistance = 547;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._saleGrid);
            this.splitContainer1.Size = new System.Drawing.Size(1082, 781);
            this.splitContainer1.SplitterDistance = 568;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            // 
            // _saleInformationBySaleman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "_saleInformationBySaleman";
            this.Size = new System.Drawing.Size(1082, 781);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this._myPanel3.ResumeLayout(false);
            this._myPanel3.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer _timer;
        private MyLib._myGrid _docGrid;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.CheckBox checkBox1;
        private MyLib._myLabel _myLabel2;
        private MyLib._myGrid _saleGrid;
        private System.Windows.Forms.TextBox _searchICTextbox;
        private MyLib._myGrid _arGrid;
        private System.Windows.Forms.CheckBox _searchAuto;
        private MyLib._myPanel _myPanel2;
        private System.Windows.Forms.TextBox _searchArTextbox;
        private MyLib._myGrid _icGrid;
        private MyLib._myPanel _myPanel1;
        private MyLib._myLabel _myLabel1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _salemanGrid;
        private MyLib._myPanel _myPanel3;
        private System.Windows.Forms.CheckBox checkBox2;
        private MyLib._myLabel _myLabel3;
        private System.Windows.Forms.TextBox _searchSaleManTextbox;
    }
}
