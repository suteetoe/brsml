namespace MyLib._databaseManage
{
    partial class _createDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_createDatabase));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("User Group", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("User", System.Windows.Forms.HorizontalAlignment.Left);
            this._splitSub = new System.Windows.Forms.SplitContainer();
            this._myPanel2 = new MyLib._myPanel();
            this._databaseGroupView = new MyLib._listViewXP();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._selectDatabaseGroupLabel = new MyLib._myLabel();
            this._myPanel3 = new MyLib._myPanel();
            this._resultListView = new MyLib._listViewXP();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._createButton = new MyLib._myButton();
            this._detailScreen = new MyLib._myScreen();
            this._progressText = new MyLib._myLabel();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._splitMain = new System.Windows.Forms.SplitContainer();
            this._myPanel1 = new MyLib._myPanel();
            this._userAndGroupLlistView = new MyLib._listViewXP();
            this.userAndGroupLabel = new MyLib._myLabel();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._selectAllButton = new MyLib._myButton();
            this._removeSelectButton = new MyLib._myButton();
            this._splitSub.Panel1.SuspendLayout();
            this._splitSub.Panel2.SuspendLayout();
            this._splitSub.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this._myPanel3.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this._splitMain.Panel1.SuspendLayout();
            this._splitMain.Panel2.SuspendLayout();
            this._splitMain.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitSub
            // 
            this._splitSub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitSub.Location = new System.Drawing.Point(0, 0);
            this._splitSub.Name = "_splitSub";
            this._splitSub.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitSub.Panel1
            // 
            this._splitSub.Panel1.Controls.Add(this._myPanel2);
            this._splitSub.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // _splitSub.Panel2
            // 
            this._splitSub.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._splitSub.Panel2.Controls.Add(this._myPanel3);
            this._splitSub.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this._splitSub_Panel2_Paint);
            this._splitSub.Size = new System.Drawing.Size(507, 509);
            this._splitSub.SplitterDistance = 185;
            this._splitSub.SplitterWidth = 1;
            this._splitSub.TabIndex = 0;
            this._splitSub.Text = "splitContainer1";
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._databaseGroupView);
            this._myPanel2.Controls.Add(this._selectDatabaseGroupLabel);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel2.Size = new System.Drawing.Size(505, 183);
            this._myPanel2.TabIndex = 5;
            // 
            // _databaseGroupView
            // 
            this._databaseGroupView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._databaseGroupView.LargeImageList = this.imageList1;
            this._databaseGroupView.Location = new System.Drawing.Point(5, 29);
            this._databaseGroupView.Margin = new System.Windows.Forms.Padding(0);
            this._databaseGroupView.MultiSelect = false;
            this._databaseGroupView.Name = "_databaseGroupView";
            this._databaseGroupView.ShowGroups = false;
            this._databaseGroupView.Size = new System.Drawing.Size(495, 149);
            this._databaseGroupView.TabIndex = 0;
            this._databaseGroupView.UseCompatibleStateImageBehavior = false;
            this._databaseGroupView.SelectedIndexChanged += new System.EventHandler(this._databaseGroup_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "data_copy.ico");
            // 
            // _selectDatabaseGroupLabel
            // 
            this._selectDatabaseGroupLabel.AutoSize = true;
            this._selectDatabaseGroupLabel.BackColor = System.Drawing.Color.Transparent;
            this._selectDatabaseGroupLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._selectDatabaseGroupLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._selectDatabaseGroupLabel.Location = new System.Drawing.Point(5, 5);
            this._selectDatabaseGroupLabel.Margin = new System.Windows.Forms.Padding(0);
            this._selectDatabaseGroupLabel.Name = "_selectDatabaseGroupLabel";
            this._selectDatabaseGroupLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._selectDatabaseGroupLabel.ResourceName = "create_database_2";
            this._selectDatabaseGroupLabel.Size = new System.Drawing.Size(112, 24);
            this._selectDatabaseGroupLabel.TabIndex = 4;
            this._selectDatabaseGroupLabel.Text = "create_database_2";
            // 
            // _myPanel3
            // 
            this._myPanel3._switchTabAuto = false;
            this._myPanel3.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel3.Controls.Add(this._resultListView);
            this._myPanel3.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel3.Controls.Add(this._detailScreen);
            this._myPanel3.Controls.Add(this._progressText);
            this._myPanel3.Controls.Add(this._progressBar);
            this._myPanel3.CornerPicture = null;
            this._myPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel3.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this._myPanel3.Location = new System.Drawing.Point(0, 0);
            this._myPanel3.Name = "_myPanel3";
            this._myPanel3.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel3.Size = new System.Drawing.Size(505, 321);
            this._myPanel3.TabIndex = 6;
            // 
            // _resultListView
            // 
            this._resultListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._resultListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultListView.Location = new System.Drawing.Point(5, 92);
            this._resultListView.Margin = new System.Windows.Forms.Padding(0);
            this._resultListView.MultiSelect = false;
            this._resultListView.Name = "_resultListView";
            this._resultListView.ShowGroups = false;
            this._resultListView.Size = new System.Drawing.Size(495, 177);
            this._resultListView.TabIndex = 2;
            this._resultListView.UseCompatibleStateImageBehavior = false;
            this._resultListView.View = System.Windows.Forms.View.List;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._createButton);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(5, 58);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(495, 34);
            this._myFlowLayoutPanel2.TabIndex = 5;
            // 
            // _createButton
            // 
            this._createButton._drawNewMethod = false;
            this._createButton.AutoSize = true;
            this._createButton.BackColor = System.Drawing.Color.Transparent;
            this._createButton.ButtonText = "create_database_process";
            this._createButton.Location = new System.Drawing.Point(321, 5);
            this._createButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._createButton.myImage = global::MyLib.Properties.Resources.filesave;
            this._createButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._createButton.myUseVisualStyleBackColor = false;
            this._createButton.Name = "_createButton";
            this._createButton.ResourceName = "create_database_process";
            this._createButton.Size = new System.Drawing.Size(173, 24);
            this._createButton.TabIndex = 1;
            this._createButton.Text = "create_database_process";
            this._createButton.UseVisualStyleBackColor = false;
            this._createButton.Click += new System.EventHandler(this._createButton_Click);
            // 
            // _detailScreen
            // 
            this._detailScreen._isChange = false;
            this._detailScreen.BackColor = System.Drawing.Color.Transparent;
            this._detailScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._detailScreen.Location = new System.Drawing.Point(5, 5);
            this._detailScreen.Name = "_detailScreen";
            this._detailScreen.Size = new System.Drawing.Size(495, 53);
            this._detailScreen.TabIndex = 0;
            // 
            // _progressText
            // 
            this._progressText.AutoSize = true;
            this._progressText.BackColor = System.Drawing.Color.Transparent;
            this._progressText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._progressText.Location = new System.Drawing.Point(5, 269);
            this._progressText.Name = "_progressText";
            this._progressText.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._progressText.ResourceName = "process";
            this._progressText.Size = new System.Drawing.Size(48, 24);
            this._progressText.TabIndex = 4;
            this._progressText.Text = "process";
            // 
            // _progressBar
            // 
            this._progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._progressBar.Location = new System.Drawing.Point(5, 293);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(495, 23);
            this._progressBar.TabIndex = 3;
            // 
            // _splitMain
            // 
            this._splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitMain.Location = new System.Drawing.Point(0, 0);
            this._splitMain.Name = "_splitMain";
            // 
            // _splitMain.Panel1
            // 
            this._splitMain.Panel1.Controls.Add(this._myPanel1);
            this._splitMain.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this._splitMain_Panel1_Paint);
            // 
            // _splitMain.Panel2
            // 
            this._splitMain.Panel2.Controls.Add(this._splitSub);
            this._splitMain.Size = new System.Drawing.Size(821, 509);
            this._splitMain.SplitterDistance = 313;
            this._splitMain.SplitterWidth = 1;
            this._splitMain.TabIndex = 1;
            this._splitMain.Text = "splitContainer2";
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._userAndGroupLlistView);
            this._myPanel1.Controls.Add(this.userAndGroupLabel);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(311, 507);
            this._myPanel1.TabIndex = 5;
            // 
            // _userAndGroupLlistView
            // 
            this._userAndGroupLlistView.CheckBoxes = true;
            this._userAndGroupLlistView.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "User Group";
            listViewGroup1.Name = "user_group";
            listViewGroup1.Tag = "user_group";
            listViewGroup2.Header = "User";
            listViewGroup2.Name = "user";
            listViewGroup2.Tag = "user";
            this._userAndGroupLlistView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this._userAndGroupLlistView.Location = new System.Drawing.Point(5, 29);
            this._userAndGroupLlistView.Name = "_userAndGroupLlistView";
            this._userAndGroupLlistView.Size = new System.Drawing.Size(301, 439);
            this._userAndGroupLlistView.TabIndex = 0;
            this._userAndGroupLlistView.UseCompatibleStateImageBehavior = false;
            // 
            // userAndGroupLabel
            // 
            this.userAndGroupLabel.AutoSize = true;
            this.userAndGroupLabel.BackColor = System.Drawing.Color.Transparent;
            this.userAndGroupLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.userAndGroupLabel.ForeColor = System.Drawing.Color.Black;
            this.userAndGroupLabel.Location = new System.Drawing.Point(5, 5);
            this.userAndGroupLabel.Name = "userAndGroupLabel";
            this.userAndGroupLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.userAndGroupLabel.ResourceName = "create_database_1";
            this.userAndGroupLabel.Size = new System.Drawing.Size(112, 24);
            this.userAndGroupLabel.TabIndex = 3;
            this.userAndGroupLabel.Text = "create_database_1";
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._selectAllButton);
            this._myFlowLayoutPanel1.Controls.Add(this._removeSelectButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(5, 468);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(301, 34);
            this._myFlowLayoutPanel1.TabIndex = 4;
            // 
            // _selectAllButton
            // 
            this._selectAllButton._drawNewMethod = false;
            this._selectAllButton.AutoSize = true;
            this._selectAllButton.BackColor = System.Drawing.Color.Transparent;
            this._selectAllButton.ButtonText = "select_all";
            this._selectAllButton.Location = new System.Drawing.Point(214, 5);
            this._selectAllButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._selectAllButton.myImage = global::MyLib.Resource16x16.add;
            this._selectAllButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._selectAllButton.myUseVisualStyleBackColor = false;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.ResourceName = "select_all";
            this._selectAllButton.Size = new System.Drawing.Size(86, 24);
            this._selectAllButton.TabIndex = 1;
            this._selectAllButton.Text = "select_all";
            this._selectAllButton.UseVisualStyleBackColor = false;
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _removeSelectButton
            // 
            this._removeSelectButton._drawNewMethod = false;
            this._removeSelectButton.AutoSize = true;
            this._removeSelectButton.BackColor = System.Drawing.Color.Transparent;
            this._removeSelectButton.ButtonText = "deselect_all";
            this._removeSelectButton.Location = new System.Drawing.Point(113, 5);
            this._removeSelectButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._removeSelectButton.myImage = global::MyLib.Resource16x16.delete;
            this._removeSelectButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._removeSelectButton.myUseVisualStyleBackColor = false;
            this._removeSelectButton.Name = "_removeSelectButton";
            this._removeSelectButton.ResourceName = "deselect_all";
            this._removeSelectButton.Size = new System.Drawing.Size(99, 24);
            this._removeSelectButton.TabIndex = 2;
            this._removeSelectButton.Text = "deselect_all";
            this._removeSelectButton.UseVisualStyleBackColor = false;
            this._removeSelectButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // _createDatabase
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(821, 509);
            this.Controls.Add(this._splitMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "_createDatabase";
            this.ResourceName = "warning72";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this._createDatabase_Load);
            this._splitSub.Panel1.ResumeLayout(false);
            this._splitSub.Panel2.ResumeLayout(false);
            this._splitSub.ResumeLayout(false);
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this._myPanel3.ResumeLayout(false);
            this._myPanel3.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this._splitMain.Panel1.ResumeLayout(false);
            this._splitMain.Panel2.ResumeLayout(false);
            this._splitMain.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.SplitContainer _splitSub;
        private System.Windows.Forms.SplitContainer _splitMain;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.ImageList imageList1;
        private _myScreen _detailScreen;
		private MyLib._listViewXP _databaseGroupView;
		private MyLib._listViewXP _resultListView;
		private MyLib._listViewXP _userAndGroupLlistView;
		private _myFlowLayoutPanel _myFlowLayoutPanel2;
		private _myFlowLayoutPanel _myFlowLayoutPanel1;
        private _myLabel _progressText;
        private _myLabel userAndGroupLabel;
        private _myLabel _selectDatabaseGroupLabel;
        private _myButton _createButton;
        private _myButton _removeSelectButton;
        private _myButton _selectAllButton;
        private _myPanel _myPanel1;
        private _myPanel _myPanel2;
        private _myPanel _myPanel3;
    }
}