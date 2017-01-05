namespace SMLERPGLControl
{
    partial class _searchVatForm
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
            this._resultGrid = new MyLib._myGrid();
            this._myPanel1 = new MyLib._myPanel();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._closeButton = new MyLib._myButton();
            this._processButton = new MyLib._myButton();
            this._selectAllButton = new MyLib._myButton();
            this._myPanel2 = new MyLib._myPanel();
            this._filterButtn = new System.Windows.Forms.Button();
            this._searchAuto = new System.Windows.Forms.CheckBox();
            this._searchTextbox = new MyLib._myTextBox();
            this._myLabel1 = new MyLib._myLabel();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.SuspendLayout();
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
            this._resultGrid.Location = new System.Drawing.Point(0, 28);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(825, 304);
            this._resultGrid.TabIndex = 4;
            this._resultGrid.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 332);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(825, 32);
            this._myPanel1.TabIndex = 5;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._closeButton);
            this._myFlowLayoutPanel2.Controls.Add(this._processButton);
            this._myFlowLayoutPanel2.Controls.Add(this._selectAllButton);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(825, 32);
            this._myFlowLayoutPanel2.TabIndex = 35;
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "ปิดหน้าจอ";
            this._closeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._closeButton.Location = new System.Drawing.Point(736, 3);
            this._closeButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._closeButton.myImage = global::SMLERPGLControl.Properties.Resources.error;
            this._closeButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._closeButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.myUseVisualStyleBackColor = false;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Size = new System.Drawing.Size(87, 24);
            this._closeButton.TabIndex = 29;
            this._closeButton.Text = "ปิดหน้าจอ";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _processButton
            // 
            this._processButton._drawNewMethod = false;
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "ประมวลผล";
            this._processButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._processButton.Location = new System.Drawing.Point(641, 3);
            this._processButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._processButton.myImage = global::SMLERPGLControl.Properties.Resources.flash;
            this._processButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._processButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._processButton.myUseVisualStyleBackColor = false;
            this._processButton.Name = "_processButton";
            this._processButton.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._processButton.ResourceName = "ประมวลผล";
            this._processButton.Size = new System.Drawing.Size(91, 24);
            this._processButton.TabIndex = 30;
            this._processButton.Text = "ประมวลผล";
            this._processButton.UseVisualStyleBackColor = false;
            // 
            // _selectAllButton
            // 
            this._selectAllButton._drawNewMethod = false;
            this._selectAllButton.AutoSize = true;
            this._selectAllButton.BackColor = System.Drawing.Color.Transparent;
            this._selectAllButton.ButtonText = "เลือกทั้งหมด";
            this._selectAllButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._selectAllButton.Location = new System.Drawing.Point(537, 3);
            this._selectAllButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._selectAllButton.myImage = global::SMLERPGLControl.Properties.Resources.check;
            this._selectAllButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._selectAllButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._selectAllButton.myUseVisualStyleBackColor = false;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._selectAllButton.ResourceName = "เลือกทั้งหมด";
            this._selectAllButton.Size = new System.Drawing.Size(100, 24);
            this._selectAllButton.TabIndex = 31;
            this._selectAllButton.Text = "เลือกทั้งหมด";
            this._selectAllButton.UseVisualStyleBackColor = false;
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._filterButtn);
            this._myPanel2.Controls.Add(this._searchAuto);
            this._myPanel2.Controls.Add(this._searchTextbox);
            this._myPanel2.Controls.Add(this._myLabel1);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Size = new System.Drawing.Size(825, 28);
            this._myPanel2.TabIndex = 6;
            // 
            // _filterButtn
            // 
            this._filterButtn.Image = global::SMLERPGLControl.Properties.Resources.zoom_in;
            this._filterButtn.Location = new System.Drawing.Point(792, 2);
            this._filterButtn.Name = "_filterButtn";
            this._filterButtn.Size = new System.Drawing.Size(25, 25);
            this._filterButtn.TabIndex = 3;
            this._filterButtn.UseVisualStyleBackColor = true;
            // 
            // _searchAuto
            // 
            this._searchAuto.AutoSize = true;
            this._searchAuto.BackColor = System.Drawing.Color.Transparent;
            this._searchAuto.Checked = true;
            this._searchAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this._searchAuto.Location = new System.Drawing.Point(736, 6);
            this._searchAuto.Name = "_searchAuto";
            this._searchAuto.Size = new System.Drawing.Size(52, 18);
            this._searchAuto.TabIndex = 2;
            this._searchAuto.Text = "Auto";
            this._searchAuto.UseVisualStyleBackColor = false;
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
            this._searchTextbox.BackColor = System.Drawing.SystemColors.Window;
            this._searchTextbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._searchTextbox.ForeColor = System.Drawing.Color.Black;
            this._searchTextbox.IsUpperCase = false;
            this._searchTextbox.Location = new System.Drawing.Point(90, 3);
            this._searchTextbox.Margin = new System.Windows.Forms.Padding(0);
            this._searchTextbox.MaxLength = 0;
            this._searchTextbox.Name = "_searchTextbox";
            this._searchTextbox.ShowIcon = false;
            this._searchTextbox.Size = new System.Drawing.Size(642, 22);
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
            // _timer
            // 
            this._timer.Interval = 500;
            // 
            // _searchVatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 364);
            this.Controls.Add(this._resultGrid);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_searchVatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_searchVatForm";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MyLib._myGrid _resultGrid;
        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib._myButton _closeButton;
        public MyLib._myButton _processButton;
        private MyLib._myButton _selectAllButton;
        private MyLib._myPanel _myPanel2;
        private System.Windows.Forms.Button _filterButtn;
        private System.Windows.Forms.CheckBox _searchAuto;
        private MyLib._myTextBox _searchTextbox;
        private MyLib._myLabel _myLabel1;
        private System.Windows.Forms.Timer _timer;
    }
}