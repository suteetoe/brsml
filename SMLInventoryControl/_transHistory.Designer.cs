namespace SMLInventoryControl
{
    partial class _transHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_transHistory));
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._resultGrid = new MyLib._myGrid();
            this._myPanel2 = new MyLib._myPanel();
            this._searchTextbox = new MyLib._myTextBox();
            this._myLabel1 = new MyLib._myLabel();
            this._condition = new SMLInventoryControl._saleHistoryConditionScreen();
            this._toolStrip.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip
            // 
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._closeButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(950, 25);
            this._toolStrip.TabIndex = 4;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _processButton
            // 
            this._processButton.Image = ((System.Drawing.Image)(resources.GetObject("_processButton.Image")));
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(67, 22);
            this._processButton.Text = "Process";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = ((System.Drawing.Image)(resources.GetObject("_closeButton.Image")));
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.IsEdit = false;
            this._resultGrid.Location = new System.Drawing.Point(0, 53);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(950, 552);
            this._resultGrid.TabIndex = 5;
            this._resultGrid.TabStop = false;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._searchTextbox);
            this._myPanel2.Controls.Add(this._myLabel1);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 25);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Size = new System.Drawing.Size(950, 28);
            this._myPanel2.TabIndex = 6;
            // 
            // _searchTextbox
            // 
            this._searchTextbox._column = 0;
            this._searchTextbox._defaultBackGround = System.Drawing.Color.White;
            this._searchTextbox._emtry = true;
            this._searchTextbox._enterToTab = false;
            this._searchTextbox._icon = false;
            this._searchTextbox._iconNumber = 1;
            this._searchTextbox._isChange = false;
            this._searchTextbox._isGetData = false;
            this._searchTextbox._isQuery = true;
            this._searchTextbox._isSearch = false;
            this._searchTextbox._isTime = false;
            this._searchTextbox._labelName = "";
            this._searchTextbox._maxColumn = 0;
            this._searchTextbox._name = null;
            this._searchTextbox._row = 0;
            this._searchTextbox._rowCount = 0;
            this._searchTextbox._textFirst = "";
            this._searchTextbox._textLast = "";
            this._searchTextbox._textSecond = "";
            this._searchTextbox._upperCase = false;
            this._searchTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchTextbox.AutoSize = true;
            this._searchTextbox.BackColor = System.Drawing.SystemColors.Window;
            this._searchTextbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._searchTextbox.ForeColor = System.Drawing.Color.Black;
            this._searchTextbox.IsUpperCase = false;
            this._searchTextbox.Location = new System.Drawing.Point(88, 3);
            this._searchTextbox.Margin = new System.Windows.Forms.Padding(0);
            this._searchTextbox.MaxLength = 0;
            this._searchTextbox.Name = "_searchTextbox";
            this._searchTextbox.ShowIcon = false;
            this._searchTextbox.Size = new System.Drawing.Size(854, 22);
            this._searchTextbox.TabIndex = 1;
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.BackColor = System.Drawing.Color.Transparent;
            this._myLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel1.Location = new System.Drawing.Point(5, 8);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "";
            this._myLabel1.Size = new System.Drawing.Size(82, 14);
            this._myLabel1.TabIndex = 0;
            this._myLabel1.Text = "ข้อความค้นหา";
            // 
            // _condition
            // 
            this._condition._isChange = false;
            this._condition.AutoSize = true;
            this._condition.BackColor = System.Drawing.Color.Transparent;
            this._condition.Dock = System.Windows.Forms.DockStyle.Top;
            this._condition.Location = new System.Drawing.Point(0, 0);
            this._condition.Name = "_condition";
            this._condition.Size = new System.Drawing.Size(950, 0);
            this._condition.TabIndex = 0;
            // 
            // _transHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._resultGrid);
            this.Controls.Add(this._myPanel2);
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._condition);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_transHistory";
            this.Size = new System.Drawing.Size(950, 605);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private MyLib._myGrid _resultGrid;
        public _saleHistoryConditionScreen _condition;
        public System.Windows.Forms.ToolStrip _toolStrip;
        private MyLib._myPanel _myPanel2;
        private MyLib._myTextBox _searchTextbox;
        private MyLib._myLabel _myLabel1;
    }
}
