namespace MyLib._databaseManage
{
    partial class _shrinkDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_shrinkDatabase));
            this._progressTextDatabase = new MyLib._myLabel();
            this._progressBarDatabase = new System.Windows.Forms.ProgressBar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._listViewDatabase = new MyLib._listViewXP();
            this._splitMain = new System.Windows.Forms.SplitContainer();
            this._resultTextBox = new System.Windows.Forms.RichTextBox();
            this.Result = new MyLib._myLabel();
            this._viewByIcon = new System.Windows.Forms.RadioButton();
            this._viewByList = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSelectAll = new MyLib.ToolStripMyButton();
            this._buttonStart = new MyLib.ToolStripMyButton();
            this._buttonExit = new MyLib.ToolStripMyButton();
            this._myGroupBox1 = new MyLib._myGroupBox();
            this._myPanel1 = new MyLib._myPanel();
            this._splitMain.Panel1.SuspendLayout();
            this._splitMain.Panel2.SuspendLayout();
            this._splitMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._myGroupBox1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _progressTextDatabase
            // 
            this._progressTextDatabase.AutoSize = true;
            this._progressTextDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this._progressTextDatabase.Location = new System.Drawing.Point(2, 2);
            this._progressTextDatabase.Name = "_progressTextDatabase";
            this._progressTextDatabase.ResourceName = "database_name";
            this._progressTextDatabase.Size = new System.Drawing.Size(70, 14);
            this._progressTextDatabase.TabIndex = 0;
            this._progressTextDatabase.Text = "ชื่อฐานข้อมูล";
            // 
            // _progressBarDatabase
            // 
            this._progressBarDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this._progressBarDatabase.Location = new System.Drawing.Point(2, 16);
            this._progressBarDatabase.Name = "_progressBarDatabase";
            this._progressBarDatabase.Size = new System.Drawing.Size(680, 21);
            this._progressBarDatabase.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "data.png");
            this.imageList1.Images.SetKeyName(1, "data_certificate.png");
            // 
            // _listViewDatabase
            // 
            this._listViewDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listViewDatabase.LargeImageList = this.imageList1;
            this._listViewDatabase.Location = new System.Drawing.Point(0, 0);
            this._listViewDatabase.Margin = new System.Windows.Forms.Padding(0);
            this._listViewDatabase.Name = "_listViewDatabase";
            this._listViewDatabase.Size = new System.Drawing.Size(684, 252);
            this._listViewDatabase.TabIndex = 0;
            this._listViewDatabase.UseCompatibleStateImageBehavior = false;
            // 
            // _splitMain
            // 
            this._splitMain.BackColor = System.Drawing.SystemColors.Window;
            this._splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitMain.Location = new System.Drawing.Point(108, 25);
            this._splitMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._splitMain.Name = "_splitMain";
            this._splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitMain.Panel1
            // 
            this._splitMain.Panel1.Controls.Add(this._listViewDatabase);
            // 
            // _splitMain.Panel2
            // 
            this._splitMain.Panel2.Controls.Add(this._resultTextBox);
            this._splitMain.Panel2.Controls.Add(this.Result);
            this._splitMain.Panel2.Controls.Add(this._progressBarDatabase);
            this._splitMain.Panel2.Controls.Add(this._progressTextDatabase);
            this._splitMain.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this._splitMain.Size = new System.Drawing.Size(686, 529);
            this._splitMain.SplitterDistance = 254;
            this._splitMain.SplitterWidth = 1;
            this._splitMain.TabIndex = 7;
            this._splitMain.Text = "splitContainer1";
            // 
            // _resultTextBox
            // 
            this._resultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultTextBox.Location = new System.Drawing.Point(2, 51);
            this._resultTextBox.Name = "_resultTextBox";
            this._resultTextBox.Size = new System.Drawing.Size(680, 219);
            this._resultTextBox.TabIndex = 4;
            this._resultTextBox.Text = "";
            // 
            // Result
            // 
            this.Result.AutoSize = true;
            this.Result.Dock = System.Windows.Forms.DockStyle.Top;
            this.Result.Location = new System.Drawing.Point(2, 37);
            this.Result.Name = "Result";
            this.Result.ResourceName = "result";
            this.Result.Size = new System.Drawing.Size(73, 14);
            this.Result.TabIndex = 5;
            this.Result.Text = "ผลการทำงาน";
            // 
            // _viewByIcon
            // 
            this._viewByIcon.AutoSize = true;
            this._viewByIcon.Location = new System.Drawing.Point(6, 16);
            this._viewByIcon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._viewByIcon.Name = "_viewByIcon";
            this._viewByIcon.Size = new System.Drawing.Size(46, 17);
            this._viewByIcon.TabIndex = 0;
            this._viewByIcon.Text = "Icon";
            this._viewByIcon.CheckedChanged += new System.EventHandler(this._viewByIcon_CheckedChanged);
            // 
            // _viewByList
            // 
            this._viewByList.AutoSize = true;
            this._viewByList.Checked = true;
            this._viewByList.Location = new System.Drawing.Point(6, 36);
            this._viewByList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._viewByList.Name = "_viewByList";
            this._viewByList.Size = new System.Drawing.Size(41, 17);
            this._viewByList.TabIndex = 1;
            this._viewByList.TabStop = true;
            this._viewByList.Text = "List";
            this._viewByList.CheckedChanged += new System.EventHandler(this._viewByList_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSelectAll,
            this._buttonStart,
            this._buttonExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(794, 25);
            this.toolStrip1.TabIndex = 16;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonSelectAll
            // 
            this._buttonSelectAll.Image = global::MyLib.Resource16x16.preferences;
            this._buttonSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectAll.Name = "_buttonSelectAll";
            this._buttonSelectAll.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSelectAll.ResourceName = "select_all";
            this._buttonSelectAll.Size = new System.Drawing.Size(86, 22);
            this._buttonSelectAll.Text = "เลือกทั้งหมด";
            this._buttonSelectAll.Click += new System.EventHandler(this._buttonSelectAll_Click);
            // 
            // _buttonStart
            // 
            this._buttonStart.Image = global::MyLib.Resource16x16.flash;
            this._buttonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonStart.Name = "_buttonStart";
            this._buttonStart.Padding = new System.Windows.Forms.Padding(1);
            this._buttonStart.ResourceName = "start_shink";
            this._buttonStart.Size = new System.Drawing.Size(124, 22);
            this._buttonStart.Text = "เริ่มกระชับฐานข้อมูล";
            this._buttonStart.Click += new System.EventHandler(this._buttonStart_Click);
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
            this._myGroupBox1.Controls.Add(this._viewByList);
            this._myGroupBox1.Controls.Add(this._viewByIcon);
            this._myGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myGroupBox1.Location = new System.Drawing.Point(2, 2);
            this._myGroupBox1.Name = "_myGroupBox1";
            this._myGroupBox1.ResourceName = "view_by";
            this._myGroupBox1.Size = new System.Drawing.Size(104, 66);
            this._myGroupBox1.TabIndex = 17;
            this._myGroupBox1.TabStop = false;
            this._myGroupBox1.Text = "รูปแบบการแสดง";
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
            this._myPanel1.Padding = new System.Windows.Forms.Padding(2);
            this._myPanel1.ShowBackground = false;
            this._myPanel1.Size = new System.Drawing.Size(108, 529);
            this._myPanel1.TabIndex = 18;
            // 
            // _shrinkDatabase
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(794, 554);
            this.Controls.Add(this._splitMain);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_shrinkDatabase";
            this.Text = "_shrinkDatabase";
            this.Load += new System.EventHandler(this._shrinkDatabase_Load);
            this._splitMain.Panel1.ResumeLayout(false);
            this._splitMain.Panel2.ResumeLayout(false);
            this._splitMain.Panel2.PerformLayout();
            this._splitMain.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._myGroupBox1.ResumeLayout(false);
            this._myGroupBox1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar _progressBarDatabase;
        private System.Windows.Forms.ImageList imageList1;
        private MyLib._listViewXP _listViewDatabase;
        private System.Windows.Forms.SplitContainer _splitMain;
        private System.Windows.Forms.RichTextBox _resultTextBox;
        private System.Windows.Forms.RadioButton _viewByIcon;
        private System.Windows.Forms.RadioButton _viewByList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private _myGroupBox _myGroupBox1;
        private _myPanel _myPanel1;
        private ToolStripMyButton _buttonSelectAll;
        private ToolStripMyButton _buttonStart;
        private ToolStripMyButton _buttonExit;
        private _myLabel _progressTextDatabase;
        private _myLabel Result;
    }
}