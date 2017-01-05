namespace MyLib._databaseManage
{
    partial class _userAndGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_userAndGroup));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Group List", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("User List", System.Windows.Forms.HorizontalAlignment.Left);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._listView = new MyLib._listViewXP();
            this._myPanel1 = new MyLib._myPanel();
            this._myGroupBox2 = new MyLib._myGroupBox();
            this._viewByList = new System.Windows.Forms.RadioButton();
            this._viewByIcon = new System.Windows.Forms.RadioButton();
            this._myGroupBox1 = new MyLib._myGroupBox();
            this._deviceTextBox = new System.Windows.Forms.TextBox();
            this._myLabel1 = new MyLib._myLabel();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._updateButton = new MyLib._myButton();
            this.label1 = new MyLib._myLabel();
            this._code = new System.Windows.Forms.TextBox();
            this._userPassword = new System.Windows.Forms.TextBox();
            this._name = new System.Windows.Forms.TextBox();
            this.label4 = new MyLib._myLabel();
            this.label2 = new MyLib._myLabel();
            this.label3 = new MyLib._myLabel();
            this._levelComboBox = new System.Windows.Forms.ComboBox();
            this._userRadio = new MyLib._myRadioButton();
            this._active = new MyLib._myCheckBox();
            this._groupRadio = new MyLib._myRadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonNewGroup = new MyLib.ToolStripMyButton();
            this._buttonNewUser = new MyLib.ToolStripMyButton();
            this._buttonDelete = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonExit = new MyLib.ToolStripMyButton();
            this._myPanel1.SuspendLayout();
            this._myGroupBox2.SuspendLayout();
            this._myGroupBox1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "users_family.png");
            this.imageList1.Images.SetKeyName(1, "user3.png");
            this.imageList1.Images.SetKeyName(2, "user1.png");
            // 
            // _listView
            // 
            this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Group List";
            listViewGroup1.Name = "myGroupList";
            listViewGroup1.Tag = "myGroupList";
            listViewGroup2.Header = "User List";
            listViewGroup2.Name = "myUserList";
            listViewGroup2.Tag = "myUserList";
            this._listView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this._listView.LargeImageList = this.imageList1;
            this._listView.Location = new System.Drawing.Point(283, 25);
            this._listView.Name = "_listView";
            this._listView.Size = new System.Drawing.Size(509, 528);
            this._listView.TabIndex = 4;
            this._listView.UseCompatibleStateImageBehavior = false;
            this._listView.SelectedIndexChanged += new System.EventHandler(this._listView_SelectedIndexChanged_1);
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
            this._myPanel1.Padding = new System.Windows.Forms.Padding(2);
            this._myPanel1.ShowBackground = false;
            this._myPanel1.Size = new System.Drawing.Size(283, 528);
            this._myPanel1.TabIndex = 6;
            // 
            // _myGroupBox2
            // 
            this._myGroupBox2.Controls.Add(this._viewByList);
            this._myGroupBox2.Controls.Add(this._viewByIcon);
            this._myGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myGroupBox2.Location = new System.Drawing.Point(2, 238);
            this._myGroupBox2.Name = "_myGroupBox2";
            this._myGroupBox2.ResourceName = "view_by";
            this._myGroupBox2.Size = new System.Drawing.Size(279, 73);
            this._myGroupBox2.TabIndex = 1;
            this._myGroupBox2.TabStop = false;
            this._myGroupBox2.Text = "รูปแบบการแสดง";
            // 
            // _viewByList
            // 
            this._viewByList.AutoSize = true;
            this._viewByList.Location = new System.Drawing.Point(6, 43);
            this._viewByList.Name = "_viewByList";
            this._viewByList.Size = new System.Drawing.Size(41, 17);
            this._viewByList.TabIndex = 1;
            this._viewByList.Text = "List";
            this._viewByList.CheckedChanged += new System.EventHandler(this._viewByList_CheckedChanged);
            // 
            // _viewByIcon
            // 
            this._viewByIcon.AutoSize = true;
            this._viewByIcon.Checked = true;
            this._viewByIcon.Location = new System.Drawing.Point(6, 20);
            this._viewByIcon.Name = "_viewByIcon";
            this._viewByIcon.Size = new System.Drawing.Size(46, 17);
            this._viewByIcon.TabIndex = 0;
            this._viewByIcon.TabStop = true;
            this._viewByIcon.Text = "Icon";
            this._viewByIcon.CheckedChanged += new System.EventHandler(this._viewByIcon_CheckedChanged);
            // 
            // _myGroupBox1
            // 
            this._myGroupBox1.Controls.Add(this._deviceTextBox);
            this._myGroupBox1.Controls.Add(this._myLabel1);
            this._myGroupBox1.Controls.Add(this._myFlowLayoutPanel1);
            this._myGroupBox1.Controls.Add(this.label1);
            this._myGroupBox1.Controls.Add(this._code);
            this._myGroupBox1.Controls.Add(this._userPassword);
            this._myGroupBox1.Controls.Add(this._name);
            this._myGroupBox1.Controls.Add(this.label4);
            this._myGroupBox1.Controls.Add(this.label2);
            this._myGroupBox1.Controls.Add(this.label3);
            this._myGroupBox1.Controls.Add(this._levelComboBox);
            this._myGroupBox1.Controls.Add(this._userRadio);
            this._myGroupBox1.Controls.Add(this._active);
            this._myGroupBox1.Controls.Add(this._groupRadio);
            this._myGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myGroupBox1.Location = new System.Drawing.Point(2, 2);
            this._myGroupBox1.Name = "_myGroupBox1";
            this._myGroupBox1.ResourceName = "detail";
            this._myGroupBox1.Size = new System.Drawing.Size(279, 236);
            this._myGroupBox1.TabIndex = 0;
            this._myGroupBox1.TabStop = false;
            this._myGroupBox1.Text = "รายละเอียด";
            // 
            // _deviceTextBox
            // 
            this._deviceTextBox.Location = new System.Drawing.Point(75, 143);
            this._deviceTextBox.Name = "_deviceTextBox";
            this._deviceTextBox.Size = new System.Drawing.Size(198, 22);
            this._deviceTextBox.TabIndex = 14;
            // 
            // _myLabel1
            // 
            this._myLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._myLabel1.Location = new System.Drawing.Point(7, 146);
            this._myLabel1.Margin = new System.Windows.Forms.Padding(0);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "device";
            this._myLabel1.Size = new System.Drawing.Size(65, 13);
            this._myLabel1.TabIndex = 13;
            this._myLabel1.Text = "Device";
            this._myLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._updateButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(3, 209);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(273, 24);
            this._myFlowLayoutPanel1.TabIndex = 12;
            // 
            // _updateButton
            // 
            this._updateButton.AutoSize = true;
            this._updateButton.BackColor = System.Drawing.Color.Transparent;
            this._updateButton.ButtonText = "ปรับปรุง";
            this._updateButton.Location = new System.Drawing.Point(196, 0);
            this._updateButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._updateButton.myImage = global::MyLib.Resource16x16.nav_up_blue;
            this._updateButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._updateButton.myUseVisualStyleBackColor = false;
            this._updateButton.Name = "_updateButton";
            this._updateButton.ResourceName = "update";
            this._updateButton.Size = new System.Drawing.Size(76, 24);
            this._updateButton.TabIndex = 6;
            this._updateButton.Text = "ปรับปรุง";
            this._updateButton.UseVisualStyleBackColor = false;
            this._updateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.ResourceName = "code";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "รหัส";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _code
            // 
            this._code.Location = new System.Drawing.Point(75, 14);
            this._code.Name = "_code";
            this._code.Size = new System.Drawing.Size(198, 22);
            this._code.TabIndex = 1;
            // 
            // _userPassword
            // 
            this._userPassword.Location = new System.Drawing.Point(75, 66);
            this._userPassword.Name = "_userPassword";
            this._userPassword.PasswordChar = '*';
            this._userPassword.Size = new System.Drawing.Size(198, 22);
            this._userPassword.TabIndex = 3;
            // 
            // _name
            // 
            this._name.Location = new System.Drawing.Point(75, 40);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(198, 22);
            this._name.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.Location = new System.Drawing.Point(7, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.ResourceName = "password";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "รหัสผ่าน";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.Location = new System.Drawing.Point(7, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.ResourceName = "name";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "ชื่อ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.Location = new System.Drawing.Point(7, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.ResourceName = "level";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "ระดับ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _levelComboBox
            // 
            this._levelComboBox.FormattingEnabled = true;
            this._levelComboBox.Items.AddRange(new object[] {
            "User",
            "Super User",
            "Admin",
            "Super Admin"});
            this._levelComboBox.Location = new System.Drawing.Point(75, 116);
            this._levelComboBox.Name = "_levelComboBox";
            this._levelComboBox.Size = new System.Drawing.Size(198, 22);
            this._levelComboBox.TabIndex = 5;
            // 
            // _userRadio
            // 
            this._userRadio.AutoSize = true;
            this._userRadio.Location = new System.Drawing.Point(29, 179);
            this._userRadio.Name = "_userRadio";
            this._userRadio.ResourceName = "is_user";
            this._userRadio.Size = new System.Drawing.Size(77, 18);
            this._userRadio.TabIndex = 6;
            this._userRadio.Text = "ให้เป็นผู้ใช้";
            // 
            // _active
            // 
            this._active.AutoSize = true;
            this._active.Location = new System.Drawing.Point(75, 92);
            this._active.Name = "_active";
            this._active.ResourceName = "login_active";
            this._active.Size = new System.Drawing.Size(95, 18);
            this._active.TabIndex = 4;
            this._active.Text = "เข้าใช้ระบบได้";
            // 
            // _groupRadio
            // 
            this._groupRadio.AutoSize = true;
            this._groupRadio.Location = new System.Drawing.Point(152, 179);
            this._groupRadio.Name = "_groupRadio";
            this._groupRadio.ResourceName = "is_group";
            this._groupRadio.Size = new System.Drawing.Size(78, 18);
            this._groupRadio.TabIndex = 7;
            this._groupRadio.Text = "ให้เป็นกลุ่ม";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this.toolStripSeparator2,
            this._buttonNewGroup,
            this._buttonNewUser,
            this._buttonDelete,
            this.toolStripSeparator1,
            this._buttonExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 5;
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
            this._buttonNewGroup.Image = global::MyLib.Properties.Resources.user1_new;
            this._buttonNewGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonNewGroup.Name = "_buttonNewGroup";
            this._buttonNewGroup.Padding = new System.Windows.Forms.Padding(1);
            this._buttonNewGroup.ResourceName = "new_group";
            this._buttonNewGroup.Size = new System.Drawing.Size(89, 22);
            this._buttonNewGroup.Text = "เพิ่มกลุ่มใหม่";
            this._buttonNewGroup.Click += new System.EventHandler(this._buttonNewGroup_Click);
            // 
            // _buttonNewUser
            // 
            this._buttonNewUser.Image = global::MyLib.Properties.Resources.user1_new;
            this._buttonNewUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonNewUser.Name = "_buttonNewUser";
            this._buttonNewUser.Padding = new System.Windows.Forms.Padding(1);
            this._buttonNewUser.ResourceName = "new_user";
            this._buttonNewUser.Size = new System.Drawing.Size(88, 22);
            this._buttonNewUser.Text = "เพิ่มรหัสผู้ใช้";
            this._buttonNewUser.Click += new System.EventHandler(this._buttonNewUser_Click);
            // 
            // _buttonDelete
            // 
            this._buttonDelete.Image = global::MyLib.Properties.Resources.user1_delete;
            this._buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonDelete.Name = "_buttonDelete";
            this._buttonDelete.Padding = new System.Windows.Forms.Padding(1);
            this._buttonDelete.ResourceName = "delete_list";
            this._buttonDelete.Size = new System.Drawing.Size(109, 22);
            this._buttonDelete.Text = "ลบรายการที่เลือก";
            this._buttonDelete.Click += new System.EventHandler(this._buttonDelete_Click);
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
            // _userAndGroup
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(792, 553);
            this.Controls.Add(this._listView);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "_userAndGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User and Group";
            this.Load += new System.EventHandler(this._userAndGroup_Load);
            this._myPanel1.ResumeLayout(false);
            this._myGroupBox2.ResumeLayout(false);
            this._myGroupBox2.PerformLayout();
            this._myGroupBox1.ResumeLayout(false);
            this._myGroupBox1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.RadioButton _viewByIcon;
        private System.Windows.Forms.RadioButton _viewByList;
        private System.Windows.Forms.TextBox _code;
        private System.Windows.Forms.TextBox _name;
        private System.Windows.Forms.ComboBox _levelComboBox;
        private System.Windows.Forms.TextBox _userPassword;
		private MyLib._listViewXP _listView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private _myPanel _myPanel1;
        private _myGroupBox _myGroupBox2;
        private _myGroupBox _myGroupBox1;
        private ToolStripMyButton _buttonNewGroup;
        private ToolStripMyButton _buttonNewUser;
        private ToolStripMyButton _buttonDelete;
        private ToolStripMyButton _buttonSave;
        private ToolStripMyButton _buttonExit;
        private _myLabel label1;
        private _myLabel label2;
        private _myLabel label3;
        private _myLabel label4;
        private _myCheckBox _active;
        private _myRadioButton _userRadio;
        private _myRadioButton _groupRadio;
        private _myButton _updateButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private _myFlowLayoutPanel _myFlowLayoutPanel1;
        private System.Windows.Forms.TextBox _deviceTextBox;
        private _myLabel _myLabel1;
    }
}