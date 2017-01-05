namespace MyLib._databaseManage
{
    partial class _groupDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_groupDatabase));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Database Group List", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Admin for Group", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Admin List", System.Windows.Forms.HorizontalAlignment.Left);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._splitMain = new System.Windows.Forms.SplitContainer();
            this._listViewGroup = new MyLib._listViewXP();
            this._database_group_list = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._splitSub = new System.Windows.Forms.SplitContainer();
            this._listViewAdminOwnerGroup = new MyLib._listViewXP();
            this._listViewAdmin = new MyLib._listViewXP();
            this.label1 = new MyLib._myLabel();
            this._detailCode = new System.Windows.Forms.TextBox();
            this._detailName = new System.Windows.Forms.TextBox();
            this.label2 = new MyLib._myLabel();
            this._detailUpdateButton = new MyLib._myButton();
            this._viewByIcon = new System.Windows.Forms.RadioButton();
            this._viewByList = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonNewGroup = new MyLib.ToolStripMyButton();
            this._buttonGroup = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonExit = new MyLib.ToolStripMyButton();
            this._myGroupBox1 = new MyLib._myGroupBox();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._myGroupBox2 = new MyLib._myGroupBox();
            this._myPanel1 = new MyLib._myPanel();
            this._splitMain.Panel1.SuspendLayout();
            this._splitMain.Panel2.SuspendLayout();
            this._splitMain.SuspendLayout();
            this._splitSub.Panel1.SuspendLayout();
            this._splitSub.Panel2.SuspendLayout();
            this._splitSub.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._myGroupBox1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myGroupBox2.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "data_copy.png");
            this.imageList1.Images.SetKeyName(1, "user1.png");
            this.imageList1.Images.SetKeyName(2, "user1_preferences.png");
            // 
            // _splitMain
            // 
            this._splitMain.BackColor = System.Drawing.SystemColors.Window;
            this._splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitMain.Location = new System.Drawing.Point(191, 25);
            this._splitMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._splitMain.Name = "_splitMain";
            this._splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitMain.Panel1
            // 
            this._splitMain.Panel1.Controls.Add(this._listViewGroup);
            // 
            // _splitMain.Panel2
            // 
            this._splitMain.Panel2.Controls.Add(this._splitSub);
            this._splitMain.Size = new System.Drawing.Size(522, 487);
            this._splitMain.SplitterDistance = 173;
            this._splitMain.SplitterWidth = 1;
            this._splitMain.TabIndex = 5;
            this._splitMain.Text = "splitContainer1";
            // 
            // _listViewGroup
            // 
            this._listViewGroup.BackColor = System.Drawing.Color.White;
            this._listViewGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._database_group_list});
            this._listViewGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Database Group List";
            listViewGroup1.Name = "myDatabaseGroupList";
            listViewGroup1.Tag = "myDatabaseGroupList";
            this._listViewGroup.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this._listViewGroup.LargeImageList = this.imageList1;
            this._listViewGroup.Location = new System.Drawing.Point(0, 0);
            this._listViewGroup.Margin = new System.Windows.Forms.Padding(0);
            this._listViewGroup.MultiSelect = false;
            this._listViewGroup.Name = "_listViewGroup";
            this._listViewGroup.ShowItemToolTips = true;
            this._listViewGroup.Size = new System.Drawing.Size(520, 171);
            this._listViewGroup.TabIndex = 1;
            this._listViewGroup.UseCompatibleStateImageBehavior = false;
            this._listViewGroup.SelectedIndexChanged += new System.EventHandler(this._listViewGroup_SelectedIndexChanged);
            // 
            // _database_group_list
            // 
            this._database_group_list.Text = "Database Group List";
            // 
            // _splitSub
            // 
            this._splitSub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitSub.Location = new System.Drawing.Point(0, 0);
            this._splitSub.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._splitSub.Name = "_splitSub";
            // 
            // _splitSub.Panel1
            // 
            this._splitSub.Panel1.Controls.Add(this._listViewAdminOwnerGroup);
            // 
            // _splitSub.Panel2
            // 
            this._splitSub.Panel2.Controls.Add(this._listViewAdmin);
            this._splitSub.Size = new System.Drawing.Size(522, 313);
            this._splitSub.SplitterDistance = 265;
            this._splitSub.SplitterWidth = 1;
            this._splitSub.TabIndex = 0;
            this._splitSub.Text = "splitContainer2";
            // 
            // _listViewAdminOwnerGroup
            // 
            this._listViewAdminOwnerGroup.AllowDrop = true;
            this._listViewAdminOwnerGroup.BackColor = System.Drawing.Color.White;
            this._listViewAdminOwnerGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup2.Header = "Admin for Group";
            listViewGroup2.Name = "myAdminForGroup";
            listViewGroup2.Tag = "myAdminForGroup";
            this._listViewAdminOwnerGroup.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
            this._listViewAdminOwnerGroup.LargeImageList = this.imageList1;
            this._listViewAdminOwnerGroup.Location = new System.Drawing.Point(0, 0);
            this._listViewAdminOwnerGroup.Margin = new System.Windows.Forms.Padding(0);
            this._listViewAdminOwnerGroup.Name = "_listViewAdminOwnerGroup";
            this._listViewAdminOwnerGroup.ShowItemToolTips = true;
            this._listViewAdminOwnerGroup.Size = new System.Drawing.Size(263, 311);
            this._listViewAdminOwnerGroup.TabIndex = 2;
            this._listViewAdminOwnerGroup.UseCompatibleStateImageBehavior = false;
            // 
            // _listViewAdmin
            // 
            this._listViewAdmin.AllowDrop = true;
            this._listViewAdmin.BackColor = System.Drawing.Color.White;
            this._listViewAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup3.Header = "Admin List";
            listViewGroup3.Name = "myAdminList";
            listViewGroup3.Tag = "myAdminList";
            this._listViewAdmin.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3});
            this._listViewAdmin.LargeImageList = this.imageList1;
            this._listViewAdmin.Location = new System.Drawing.Point(0, 0);
            this._listViewAdmin.Margin = new System.Windows.Forms.Padding(0);
            this._listViewAdmin.Name = "_listViewAdmin";
            this._listViewAdmin.ShowItemToolTips = true;
            this._listViewAdmin.Size = new System.Drawing.Size(254, 311);
            this._listViewAdmin.TabIndex = 3;
            this._listViewAdmin.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.ResourceName = "code";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "รหัส";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _detailCode
            // 
            this._detailCode.Location = new System.Drawing.Point(61, 14);
            this._detailCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._detailCode.Name = "_detailCode";
            this._detailCode.Size = new System.Drawing.Size(116, 22);
            this._detailCode.TabIndex = 1;
            // 
            // _detailName
            // 
            this._detailName.Location = new System.Drawing.Point(61, 41);
            this._detailName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._detailName.Name = "_detailName";
            this._detailName.Size = new System.Drawing.Size(116, 22);
            this._detailName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 44);
            this.label2.Name = "label2";
            this.label2.ResourceName = "name";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "ชื่อ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _detailUpdateButton
            // 
            this._detailUpdateButton.AutoSize = true;
            this._detailUpdateButton.BackColor = System.Drawing.Color.Transparent;
            this._detailUpdateButton.ButtonText = "ปรับปรุง";
            this._detailUpdateButton.Location = new System.Drawing.Point(102, 4);
            this._detailUpdateButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._detailUpdateButton.myImage = global::MyLib.Resource16x16.check2;
            this._detailUpdateButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._detailUpdateButton.myUseVisualStyleBackColor = false;
            this._detailUpdateButton.Name = "_detailUpdateButton";
            this._detailUpdateButton.ResourceName = "update";
            this._detailUpdateButton.Size = new System.Drawing.Size(76, 24);
            this._detailUpdateButton.TabIndex = 5;
            this._detailUpdateButton.Text = "Add";
            this._detailUpdateButton.UseVisualStyleBackColor = false;
            this._detailUpdateButton.Click += new System.EventHandler(this._detailUpdateButton_Click);
            // 
            // _viewByIcon
            // 
            this._viewByIcon.AutoSize = true;
            this._viewByIcon.Checked = true;
            this._viewByIcon.Location = new System.Drawing.Point(10, 21);
            this._viewByIcon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._viewByIcon.Name = "_viewByIcon";
            this._viewByIcon.Size = new System.Drawing.Size(46, 17);
            this._viewByIcon.TabIndex = 0;
            this._viewByIcon.TabStop = true;
            this._viewByIcon.Text = "Icon";
            this._viewByIcon.CheckedChanged += new System.EventHandler(this._viewByIcon_CheckedChanged);
            // 
            // _viewByList
            // 
            this._viewByList.AutoSize = true;
            this._viewByList.Location = new System.Drawing.Point(10, 41);
            this._viewByList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._viewByList.Name = "_viewByList";
            this._viewByList.Size = new System.Drawing.Size(41, 17);
            this._viewByList.TabIndex = 1;
            this._viewByList.Text = "List";
            this._viewByList.CheckedChanged += new System.EventHandler(this._viewByList_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this.toolStripSeparator2,
            this._buttonNewGroup,
            this._buttonGroup,
            this.toolStripSeparator1,
            this._buttonExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(713, 25);
            this.toolStrip1.TabIndex = 9;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonNewGroup
            // 
            this._buttonNewGroup.Image = global::MyLib.Properties.Resources.folder_new;
            this._buttonNewGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonNewGroup.Name = "_buttonNewGroup";
            this._buttonNewGroup.Padding = new System.Windows.Forms.Padding(1);
            this._buttonNewGroup.ResourceName = "new_database_group";
            this._buttonNewGroup.Size = new System.Drawing.Size(136, 22);
            this._buttonNewGroup.Text = "สร้างกลุ่มฐานข้อมูลใหม่";
            this._buttonNewGroup.Click += new System.EventHandler(this._buttonNewGroup_Click);
            // 
            // _buttonGroup
            // 
            this._buttonGroup.Image = global::MyLib.Properties.Resources.folder_delete;
            this._buttonGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonGroup.Name = "_buttonGroup";
            this._buttonGroup.Padding = new System.Windows.Forms.Padding(1);
            this._buttonGroup.ResourceName = "delete_database_group";
            this._buttonGroup.Size = new System.Drawing.Size(107, 22);
            this._buttonGroup.Text = "ลบกลุ่มฐานข้อมูล";
            this._buttonGroup.Click += new System.EventHandler(this._buttonGroup_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            this._myGroupBox1.Controls.Add(this._detailName);
            this._myGroupBox1.Controls.Add(this._detailCode);
            this._myGroupBox1.Controls.Add(this.label2);
            this._myGroupBox1.Controls.Add(this.label1);
            this._myGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myGroupBox1.Location = new System.Drawing.Point(5, 5);
            this._myGroupBox1.Name = "_myGroupBox1";
            this._myGroupBox1.Padding = new System.Windows.Forms.Padding(0);
            this._myGroupBox1.ResourceName = "detail";
            this._myGroupBox1.Size = new System.Drawing.Size(181, 104);
            this._myGroupBox1.TabIndex = 10;
            this._myGroupBox1.TabStop = false;
            this._myGroupBox1.Text = "รายละเอียด";
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._detailUpdateButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 72);
            this._myFlowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(181, 32);
            this._myFlowLayoutPanel1.TabIndex = 6;
            // 
            // _myGroupBox2
            // 
            this._myGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this._myGroupBox2.Controls.Add(this._viewByList);
            this._myGroupBox2.Controls.Add(this._viewByIcon);
            this._myGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myGroupBox2.Location = new System.Drawing.Point(5, 109);
            this._myGroupBox2.Name = "_myGroupBox2";
            this._myGroupBox2.ResourceName = "view_by";
            this._myGroupBox2.Size = new System.Drawing.Size(181, 72);
            this._myGroupBox2.TabIndex = 11;
            this._myGroupBox2.TabStop = false;
            this._myGroupBox2.Text = "รูปแบบการแสดง";
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myGroupBox2);
            this._myPanel1.Controls.Add(this._myGroupBox1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(191, 487);
            this._myPanel1.TabIndex = 12;
            // 
            // _groupDatabase
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(713, 512);
            this.Controls.Add(this._splitMain);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(430, 467);
            this.Name = "_groupDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_groupDatabase";
            this.Load += new System.EventHandler(this._groupDatabase_Load);
            this._splitMain.Panel1.ResumeLayout(false);
            this._splitMain.Panel2.ResumeLayout(false);
            this._splitMain.ResumeLayout(false);
            this._splitSub.Panel1.ResumeLayout(false);
            this._splitSub.Panel2.ResumeLayout(false);
            this._splitSub.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._myGroupBox1.ResumeLayout(false);
            this._myGroupBox1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._myGroupBox2.ResumeLayout(false);
            this._myGroupBox2.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer _splitMain;
		private System.Windows.Forms.SplitContainer _splitSub;
		private MyLib._listViewXP _listViewGroup;
		private System.Windows.Forms.ColumnHeader _database_group_list;
		private MyLib._listViewXP _listViewAdminOwnerGroup;
        private MyLib._listViewXP _listViewAdmin;
		private System.Windows.Forms.TextBox _detailCode;
        private System.Windows.Forms.TextBox _detailName;
        private _myButton _detailUpdateButton;
		private System.Windows.Forms.RadioButton _viewByIcon;
		private System.Windows.Forms.RadioButton _viewByList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private _myGroupBox _myGroupBox1;
        private _myGroupBox _myGroupBox2;
        private _myPanel _myPanel1;
        private ToolStripMyButton _buttonNewGroup;
        private ToolStripMyButton _buttonGroup;
        private ToolStripMyButton _buttonSave;
        private ToolStripMyButton _buttonExit;
        private _myLabel label1;
        private _myLabel label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private _myFlowLayoutPanel _myFlowLayoutPanel1;
    }
}