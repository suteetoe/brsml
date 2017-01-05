namespace MyLib._databaseManage
{
    partial class _resourceEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_resourceEdit));
            this.label1 = new MyLib._myLabel();
            this._searchGroupComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new MyLib._myLabel();
            this._searchTextBox = new System.Windows.Forms.TextBox();
            this._buttonLoad = new MyLib._myButton();
            this._gridResource = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._buttonExit = new MyLib.ToolStripMyButton();
            this._myGroupBox1 = new MyLib._myGroupBox();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._myPanel1 = new MyLib._myPanel();
            this.toolStrip1.SuspendLayout();
            this._myGroupBox1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.ResourceName = "database_group";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "กลุ่มข้อมูล";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _searchGroupComboBox
            // 
            this._searchGroupComboBox.FormattingEnabled = true;
            this._searchGroupComboBox.Location = new System.Drawing.Point(105, 14);
            this._searchGroupComboBox.Margin = new System.Windows.Forms.Padding(2);
            this._searchGroupComboBox.Name = "_searchGroupComboBox";
            this._searchGroupComboBox.Size = new System.Drawing.Size(106, 22);
            this._searchGroupComboBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.ResourceName = "text_for_search";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "ค้นหา";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _searchTextBox
            // 
            this._searchTextBox.Location = new System.Drawing.Point(105, 38);
            this._searchTextBox.Margin = new System.Windows.Forms.Padding(2);
            this._searchTextBox.Name = "_searchTextBox";
            this._searchTextBox.Size = new System.Drawing.Size(106, 22);
            this._searchTextBox.TabIndex = 0;
            // 
            // _buttonLoad
            // 
            this._buttonLoad.AutoSize = true;
            this._buttonLoad.BackColor = System.Drawing.Color.Transparent;
            this._buttonLoad.ButtonText = "ดึงข้อมูล";
            this._buttonLoad.Location = new System.Drawing.Point(133, 2);
            this._buttonLoad.Margin = new System.Windows.Forms.Padding(2);
            this._buttonLoad.myImage = global::MyLib.Resource16x16.replace2;
            this._buttonLoad.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonLoad.myUseVisualStyleBackColor = false;
            this._buttonLoad.Name = "_buttonLoad";
            this._buttonLoad.ResourceName = "load_data";
            this._buttonLoad.Size = new System.Drawing.Size(79, 24);
            this._buttonLoad.TabIndex = 1;
            this._buttonLoad.Text = "เรียกข้อมูล";
            this._buttonLoad.UseVisualStyleBackColor = false;
            this._buttonLoad.Click += new System.EventHandler(this._buttonLoad_Click);
            // 
            // _gridResource
            // 
            this._gridResource._extraWordShow = true;
            this._gridResource._selectRow = -1;
            this._gridResource.BackColor = System.Drawing.SystemColors.Window;
            this._gridResource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridResource.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridResource.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridResource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridResource.Location = new System.Drawing.Point(230, 25);
            this._gridResource.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._gridResource.Name = "_gridResource";
            this._gridResource.Size = new System.Drawing.Size(464, 368);
            this._gridResource.TabIndex = 14;
            this._gridResource.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._buttonExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(694, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::MyLib.Resource16x16.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "save_and_close";
            this._buttonSave.Size = new System.Drawing.Size(131, 22);
            this._buttonSave.Text = "บันทึกพร้อมปิดหน้าจอ";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonExit
            // 
            this._buttonExit.Image = global::MyLib.Resource16x16.error;
            this._buttonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonExit.Name = "_buttonExit";
            this._buttonExit.Padding = new System.Windows.Forms.Padding(1);
            this._buttonExit.ResourceName = "screen_close";
            this._buttonExit.Size = new System.Drawing.Size(75, 22);
            this._buttonExit.Text = "ปิดหน้าจอ";
            this._buttonExit.Click += new System.EventHandler(this._buttonExit_Click);
            // 
            // _myGroupBox1
            // 
            this._myGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this._myGroupBox1.Controls.Add(this._myFlowLayoutPanel1);
            this._myGroupBox1.Controls.Add(this._searchTextBox);
            this._myGroupBox1.Controls.Add(this.label2);
            this._myGroupBox1.Controls.Add(this._searchGroupComboBox);
            this._myGroupBox1.Controls.Add(this.label1);
            this._myGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myGroupBox1.Location = new System.Drawing.Point(5, 5);
            this._myGroupBox1.Name = "_myGroupBox1";
            this._myGroupBox1.ResourceName = "search";
            this._myGroupBox1.Size = new System.Drawing.Size(220, 97);
            this._myGroupBox1.TabIndex = 16;
            this._myGroupBox1.TabStop = false;
            this._myGroupBox1.Text = "ค้นหาข้อมูล";
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonLoad);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(3, 66);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(214, 28);
            this._myFlowLayoutPanel1.TabIndex = 5;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myGroupBox1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(230, 368);
            this._myPanel1.TabIndex = 17;
            // 
            // _resourceEdit
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(694, 393);
            this.Controls.Add(this._gridResource);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "_resourceEdit";
            this.Text = "_resourceEdit";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this._resourceEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._myGroupBox1.ResumeLayout(false);
            this._myGroupBox1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _searchTextBox;
        private System.Windows.Forms.ComboBox _searchGroupComboBox;
        private _myGrid _gridResource;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private _myGroupBox _myGroupBox1;
        private _myPanel _myPanel1;
        private ToolStripMyButton _buttonSave;
        private ToolStripMyButton _buttonExit;
        private _myLabel label1;
        private _myLabel label2;
        private _myButton _buttonLoad;
        private _myFlowLayoutPanel _myFlowLayoutPanel1;
    }
}