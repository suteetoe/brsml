namespace MyLib._databaseManage._linkDatabase
{
    partial class _linkDatabase
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Database certificate", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Database List", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_linkDatabase));
            this._splitMain = new System.Windows.Forms.SplitContainer();
            this._gridDatabaseLink = new MyLib._myGrid();
            this._listViewDatabase = new MyLib._listViewXP();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._buttonExit = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this._byIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._splitMain.Panel1.SuspendLayout();
            this._splitMain.Panel2.SuspendLayout();
            this._splitMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitMain
            // 
            this._splitMain.BackColor = System.Drawing.SystemColors.Window;
            this._splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitMain.Location = new System.Drawing.Point(0, 25);
            this._splitMain.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._splitMain.Name = "_splitMain";
            this._splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitMain.Panel1
            // 
            this._splitMain.Panel1.Controls.Add(this._gridDatabaseLink);
            // 
            // _splitMain.Panel2
            // 
            this._splitMain.Panel2.Controls.Add(this._listViewDatabase);
            this._splitMain.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this._splitMain.Size = new System.Drawing.Size(645, 519);
            this._splitMain.SplitterDistance = 248;
            this._splitMain.SplitterWidth = 1;
            this._splitMain.TabIndex = 1;
            this._splitMain.Text = "splitContainer1";
            // 
            // _gridDatabaseLink
            // 
            this._gridDatabaseLink._extraWordShow = true;
            this._gridDatabaseLink._selectRow = -1;
            this._gridDatabaseLink.BackColor = System.Drawing.SystemColors.Window;
            this._gridDatabaseLink.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridDatabaseLink.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridDatabaseLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridDatabaseLink.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridDatabaseLink.Location = new System.Drawing.Point(0, 0);
            this._gridDatabaseLink.Margin = new System.Windows.Forms.Padding(2, 10, 2, 10);
            this._gridDatabaseLink.Name = "_gridDatabaseLink";
            this._gridDatabaseLink.Size = new System.Drawing.Size(643, 246);
            this._gridDatabaseLink.TabIndex = 0;
            this._gridDatabaseLink.TabStop = false;
            // 
            // _listViewDatabase
            // 
            this._listViewDatabase.BackColor = System.Drawing.Color.White;
            this._listViewDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Database certificate";
            listViewGroup1.Name = "databaseCertificate";
            listViewGroup1.Tag = "databaseCertificate";
            listViewGroup2.Header = "Database List";
            listViewGroup2.Name = "databaseList";
            listViewGroup2.Tag = "databaseList";
            this._listViewDatabase.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this._listViewDatabase.LargeImageList = this.imageList1;
            this._listViewDatabase.Location = new System.Drawing.Point(2, 2);
            this._listViewDatabase.Margin = new System.Windows.Forms.Padding(0);
            this._listViewDatabase.MultiSelect = false;
            this._listViewDatabase.Name = "_listViewDatabase";
            this._listViewDatabase.ShowItemToolTips = true;
            this._listViewDatabase.Size = new System.Drawing.Size(639, 264);
            this._listViewDatabase.TabIndex = 2;
            this._listViewDatabase.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "data.png");
            this.imageList1.Images.SetKeyName(1, "data_certificate.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._buttonExit,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(645, 25);
            this.toolStrip1.TabIndex = 4;
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
            this._buttonExit.Image = global::MyLib.Resource16x16.error1;
            this._buttonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonExit.Name = "_buttonExit";
            this._buttonExit.Padding = new System.Windows.Forms.Padding(1);
            this._buttonExit.ResourceName = "screen_close";
            this._buttonExit.Size = new System.Drawing.Size(75, 22);
            this._buttonExit.Text = "ปิดหน้าจอ";
            this._buttonExit.Click += new System.EventHandler(this._buttonExit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._byIconToolStripMenuItem,
            this.byListToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::MyLib.Resource16x16.replace2;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(77, 22);
            this.toolStripDropDownButton1.Text = "View by";
            // 
            // _byIconToolStripMenuItem
            // 
            this._byIconToolStripMenuItem.Image = global::MyLib.Resource16x16.photo_portrait;
            this._byIconToolStripMenuItem.Name = "_byIconToolStripMenuItem";
            this._byIconToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this._byIconToolStripMenuItem.Text = "by Icon";
            this._byIconToolStripMenuItem.Click += new System.EventHandler(this._byIconToolStripMenuItem_Click);
            // 
            // byListToolStripMenuItem
            // 
            this.byListToolStripMenuItem.Image = global::MyLib.Resource16x16.preferences;
            this.byListToolStripMenuItem.Name = "byListToolStripMenuItem";
            this.byListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.byListToolStripMenuItem.Text = "by List";
            this.byListToolStripMenuItem.Click += new System.EventHandler(this.byListToolStripMenuItem_Click);
            // 
            // _linkDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(645, 544);
            this.Controls.Add(this._splitMain);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(313, 304);
            this.Name = "_linkDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_linkDatabase";
            this.Load += new System.EventHandler(this._linkDatabase_Load);
            this._splitMain.Panel1.ResumeLayout(false);
            this._splitMain.Panel2.ResumeLayout(false);
            this._splitMain.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitMain;
        private MyLib._listViewXP _listViewDatabase;
        private _myGrid _gridDatabaseLink;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem _byIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ToolStripMyButton _buttonSave;
        private ToolStripMyButton _buttonExit;
    }
}