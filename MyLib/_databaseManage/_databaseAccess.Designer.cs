namespace MyLib._databaseManage
{
    partial class _databaseAccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_databaseAccess));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Database List", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Member on group", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("User List", System.Windows.Forms.HorizontalAlignment.Left);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._splitMain = new System.Windows.Forms.SplitContainer();
            this._listViewDatabase = new MyLib._listViewXP();
            this._splitSub = new System.Windows.Forms.SplitContainer();
            this._listViewUserMember = new MyLib._listViewXP();
            this._listViewUser = new MyLib._listViewXP();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._buttonExit = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._myGroupBox1 = new MyLib._myGroupBox();
            this._viewByList = new System.Windows.Forms.RadioButton();
            this._viewByIcon = new System.Windows.Forms.RadioButton();
            this._splitMain.Panel1.SuspendLayout();
            this._splitMain.Panel2.SuspendLayout();
            this._splitMain.SuspendLayout();
            this._splitSub.Panel1.SuspendLayout();
            this._splitSub.Panel2.SuspendLayout();
            this._splitSub.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "users_family.png");
            this.imageList1.Images.SetKeyName(1, "user1.png");
            this.imageList1.Images.SetKeyName(2, "user1_preferences.png");
            this.imageList1.Images.SetKeyName(3, "user3.png");
            this.imageList1.Images.SetKeyName(4, "user_headphones.png");
            this.imageList1.Images.SetKeyName(5, "data_certificate.png");
            // 
            // _splitMain
            // 
            this._splitMain.BackColor = System.Drawing.SystemColors.Window;
            this._splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitMain.Location = new System.Drawing.Point(100, 25);
            this._splitMain.Margin = new System.Windows.Forms.Padding(2);
            this._splitMain.Name = "_splitMain";
            this._splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitMain.Panel1
            // 
            this._splitMain.Panel1.Controls.Add(this._listViewDatabase);
            // 
            // _splitMain.Panel2
            // 
            this._splitMain.Panel2.Controls.Add(this._splitSub);
            this._splitMain.Size = new System.Drawing.Size(718, 527);
            this._splitMain.SplitterDistance = 185;
            this._splitMain.SplitterWidth = 1;
            this._splitMain.TabIndex = 7;
            this._splitMain.Text = "splitContainer1";
            // 
            // _listViewDatabase
            // 
            this._listViewDatabase.BackColor = System.Drawing.Color.White;
            this._listViewDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listViewDatabase.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            listViewGroup1.Header = "Database List";
            listViewGroup1.Name = "myDatabaseList";
            listViewGroup1.Tag = "myDatabaseList";
            this._listViewDatabase.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this._listViewDatabase.LargeImageList = this.imageList1;
            this._listViewDatabase.Location = new System.Drawing.Point(0, 0);
            this._listViewDatabase.Margin = new System.Windows.Forms.Padding(0);
            this._listViewDatabase.MultiSelect = false;
            this._listViewDatabase.Name = "_listViewDatabase";
            this._listViewDatabase.ShowItemToolTips = true;
            this._listViewDatabase.Size = new System.Drawing.Size(716, 183);
            this._listViewDatabase.TabIndex = 1;
            this._listViewDatabase.UseCompatibleStateImageBehavior = false;
            // 
            // _splitSub
            // 
            this._splitSub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitSub.Location = new System.Drawing.Point(0, 0);
            this._splitSub.Margin = new System.Windows.Forms.Padding(2);
            this._splitSub.Name = "_splitSub";
            // 
            // _splitSub.Panel1
            // 
            this._splitSub.Panel1.Controls.Add(this._listViewUserMember);
            // 
            // _splitSub.Panel2
            // 
            this._splitSub.Panel2.Controls.Add(this._listViewUser);
            this._splitSub.Size = new System.Drawing.Size(718, 341);
            this._splitSub.SplitterDistance = 360;
            this._splitSub.SplitterWidth = 1;
            this._splitSub.TabIndex = 0;
            this._splitSub.Text = "splitContainer2";
            // 
            // _listViewUserMember
            // 
            this._listViewUserMember.AllowDrop = true;
            this._listViewUserMember.BackColor = System.Drawing.Color.White;
            this._listViewUserMember.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup2.Header = "Member on group";
            listViewGroup2.Name = "myMember";
            listViewGroup2.Tag = "myMember";
            this._listViewUserMember.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
            this._listViewUserMember.LargeImageList = this.imageList1;
            this._listViewUserMember.Location = new System.Drawing.Point(0, 0);
            this._listViewUserMember.Margin = new System.Windows.Forms.Padding(0);
            this._listViewUserMember.Name = "_listViewUserMember";
            this._listViewUserMember.ShowItemToolTips = true;
            this._listViewUserMember.Size = new System.Drawing.Size(358, 339);
            this._listViewUserMember.TabIndex = 2;
            this._listViewUserMember.UseCompatibleStateImageBehavior = false;
            // 
            // _listViewUser
            // 
            this._listViewUser.AllowDrop = true;
            this._listViewUser.BackColor = System.Drawing.Color.White;
            this._listViewUser.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup3.Header = "User List";
            listViewGroup3.Name = "myUserList";
            listViewGroup3.Tag = "myUserList";
            this._listViewUser.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3});
            this._listViewUser.LargeImageList = this.imageList1;
            this._listViewUser.Location = new System.Drawing.Point(0, 0);
            this._listViewUser.Margin = new System.Windows.Forms.Padding(0);
            this._listViewUser.Name = "_listViewUser";
            this._listViewUser.ShowItemToolTips = true;
            this._listViewUser.Size = new System.Drawing.Size(355, 339);
            this._listViewUser.TabIndex = 3;
            this._listViewUser.UseCompatibleStateImageBehavior = false;
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
            this.toolStrip1.Size = new System.Drawing.Size(818, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::MyLib.Resource16x16.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "save_and_close";
            this._buttonSave.Size = new System.Drawing.Size(123, 22);
            this._buttonSave.Text = "บันทึกและปิดหน้าจอ";
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
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myGroupBox1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(100, 527);
            this._myPanel1.TabIndex = 9;
            // 
            // _myGroupBox1
            // 
            this._myGroupBox1.Controls.Add(this._viewByList);
            this._myGroupBox1.Controls.Add(this._viewByIcon);
            this._myGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myGroupBox1.Location = new System.Drawing.Point(5, 5);
            this._myGroupBox1.Name = "_myGroupBox1";
            this._myGroupBox1.ResourceName = "view_by";
            this._myGroupBox1.Size = new System.Drawing.Size(90, 70);
            this._myGroupBox1.TabIndex = 0;
            this._myGroupBox1.TabStop = false;
            this._myGroupBox1.Text = "แสดงโดย";
            // 
            // _viewByList
            // 
            this._viewByList.AutoSize = true;
            this._viewByList.Location = new System.Drawing.Point(9, 40);
            this._viewByList.Margin = new System.Windows.Forms.Padding(2);
            this._viewByList.Name = "_viewByList";
            this._viewByList.Size = new System.Drawing.Size(41, 17);
            this._viewByList.TabIndex = 1;
            this._viewByList.Text = "List";
            // 
            // _viewByIcon
            // 
            this._viewByIcon.AutoSize = true;
            this._viewByIcon.Checked = true;
            this._viewByIcon.Location = new System.Drawing.Point(9, 19);
            this._viewByIcon.Margin = new System.Windows.Forms.Padding(2);
            this._viewByIcon.Name = "_viewByIcon";
            this._viewByIcon.Size = new System.Drawing.Size(46, 17);
            this._viewByIcon.TabIndex = 0;
            this._viewByIcon.TabStop = true;
            this._viewByIcon.Text = "Icon";
            // 
            // _databaseAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 552);
            this.Controls.Add(this._splitMain);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_databaseAccess";
            this.Text = "Database Access";
            this.Load += new System.EventHandler(this._databaseAccess_Load);
            this._splitMain.Panel1.ResumeLayout(false);
            this._splitMain.Panel2.ResumeLayout(false);
            this._splitMain.ResumeLayout(false);
            this._splitSub.Panel1.ResumeLayout(false);
            this._splitSub.Panel2.ResumeLayout(false);
            this._splitSub.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myGroupBox1.ResumeLayout(false);
            this._myGroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton _viewByList;
        private System.Windows.Forms.RadioButton _viewByIcon;
        private ToolStripMyButton _buttonSave;
        private _myGroupBox _myGroupBox1;
        private ToolStripMyButton _buttonExit;
        private _listViewXP _listViewUser;
        private System.Windows.Forms.ImageList imageList1;
        private _listViewXP _listViewDatabase;
        private System.Windows.Forms.SplitContainer _splitMain;
        private System.Windows.Forms.SplitContainer _splitSub;
        private _listViewXP _listViewUserMember;
        private _myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}