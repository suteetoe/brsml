namespace MyLib._databaseManage
{
	partial class _viewManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_viewManage));
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._resetButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._menuList = new MyLib._myTreeView();
            this._myFlowLayoutPanel3 = new MyLib._myFlowLayoutPanel();
            this._workPanel = new MyLib._myPanel();
            this._flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ButtonExit = new MyLib._myButton();
            this._viewManagerTab = new MyLib._myTabControl();
            this._find = new System.Windows.Forms.TabPage();
            this._myPanel2 = new MyLib._myPanel();
            this._group = new MyLib._myTabControl();
            this.group1 = new System.Windows.Forms.TabPage();
            this.group2 = new System.Windows.Forms.TabPage();
            this.group3 = new System.Windows.Forms.TabPage();
            this.group4 = new System.Windows.Forms.TabPage();
            this.group5 = new System.Windows.Forms.TabPage();
            this.group6 = new System.Windows.Forms.TabPage();
            this.group7 = new System.Windows.Forms.TabPage();
            this.group8 = new System.Windows.Forms.TabPage();
            this.group9 = new System.Windows.Forms.TabPage();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this.ButtonTest = new MyLib._myButton();
            this.ButtonLoadTemplate = new MyLib._myButton();
            this._myScreen1 = new MyLib._myScreen();
            this._form = new System.Windows.Forms.TabPage();
            this._document = new System.Windows.Forms.TabPage();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this.ButtonSave = new MyLib._myButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._toolStrip1.SuspendLayout();
            this._viewManagerTab.SuspendLayout();
            this._find.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this._group.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "window");
            this._imageList.Images.SetKeyName(1, "window_edit");
            this._imageList.Images.SetKeyName(2, "folder");
            // 
            // _splitContainer
            // 
            this._splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(5, 30);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._menuList);
            this._splitContainer.Panel1.Controls.Add(this._myFlowLayoutPanel3);
            this._splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this.panel1);
            this._splitContainer.Panel2.Controls.Add(this._flowLayoutPanel);
            this._splitContainer.Size = new System.Drawing.Size(763, 357);
            this._splitContainer.SplitterDistance = 197;
            this._splitContainer.TabIndex = 0;
            this._splitContainer.Text = "splitContainer1";
            // 
            // _toolStrip1
            // 
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._resetButton,
            this._closeButton});
            this._toolStrip1.Location = new System.Drawing.Point(5, 5);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(763, 25);
            this._toolStrip1.TabIndex = 0;
            this._toolStrip1.Text = "toolStrip1";
            // 
            // _resetButton
            // 
            this._resetButton.Image = ((System.Drawing.Image)(resources.GetObject("_resetButton.Image")));
            this._resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._resetButton.Name = "_resetButton";
            this._resetButton.Size = new System.Drawing.Size(110, 22);
            this._resetButton.Text = "Reset to Default";
            this._resetButton.Click += new System.EventHandler(this._resetButton_Click);
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
            // _menuList
            // 
            this._menuList.BackColor = System.Drawing.Color.LightYellow;
            this._menuList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._menuList.ImageIndex = 0;
            this._menuList.ImageList = this._imageList;
            this._menuList.Location = new System.Drawing.Point(5, 5);
            this._menuList.Name = "_menuList";
            this._menuList.SelectedImageIndex = 0;
            this._menuList.ShowNodeToolTips = true;
            this._menuList.Size = new System.Drawing.Size(185, 345);
            this._menuList.TabIndex = 0;
            this._menuList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._myTreeView1_AfterSelect);
            // 
            // _myFlowLayoutPanel3
            // 
            this._myFlowLayoutPanel3.AutoSize = true;
            this._myFlowLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel3.Location = new System.Drawing.Point(5, 350);
            this._myFlowLayoutPanel3.Name = "_myFlowLayoutPanel3";
            this._myFlowLayoutPanel3.Size = new System.Drawing.Size(185, 0);
            this._myFlowLayoutPanel3.TabIndex = 1;
            // 
            // _workPanel
            // 
            this._workPanel._switchTabAuto = false;
            this._workPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._workPanel.CornerPicture = null;
            this._workPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._workPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._workPanel.Location = new System.Drawing.Point(0, 0);
            this._workPanel.Name = "_workPanel";
            this._workPanel.Padding = new System.Windows.Forms.Padding(5);
            this._workPanel.Size = new System.Drawing.Size(560, 343);
            this._workPanel.TabIndex = 2;
            // 
            // _flowLayoutPanel
            // 
            this._flowLayoutPanel.AutoSize = true;
            this._flowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._flowLayoutPanel.Location = new System.Drawing.Point(0, 343);
            this._flowLayoutPanel.Name = "_flowLayoutPanel";
            this._flowLayoutPanel.Padding = new System.Windows.Forms.Padding(5);
            this._flowLayoutPanel.Size = new System.Drawing.Size(560, 12);
            this._flowLayoutPanel.TabIndex = 0;
            // 
            // ButtonExit
            // 
            this.ButtonExit.AutoSize = true;
            this.ButtonExit.BackColor = System.Drawing.Color.Transparent;
            this.ButtonExit.ButtonText = "ปิด";
            this.ButtonExit.Location = new System.Drawing.Point(100, 0);
            this.ButtonExit.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.ButtonExit.myImage = global::MyLib.Resource16x16.error;
            this.ButtonExit.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonExit.myUseVisualStyleBackColor = false;
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ButtonExit.ResourceName = "screen_close";
            this.ButtonExit.Size = new System.Drawing.Size(51, 24);
            this.ButtonExit.TabIndex = 0;
            this.ButtonExit.Text = "ปิด";
            this.ButtonExit.UseVisualStyleBackColor = false;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // _viewManagerTab
            // 
            this._viewManagerTab.Controls.Add(this._find);
            this._viewManagerTab.Controls.Add(this._form);
            this._viewManagerTab.Controls.Add(this._document);
            this._viewManagerTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._viewManagerTab.FixedName = true;
            this._viewManagerTab.Location = new System.Drawing.Point(5, 5);
            this._viewManagerTab.Multiline = true;
            this._viewManagerTab.Name = "_viewManagerTab";
            this._viewManagerTab.SelectedIndex = 0;
            this._viewManagerTab.ShowTabNumber = true;
            this._viewManagerTab.Size = new System.Drawing.Size(587, 344);
            this._viewManagerTab.TabIndex = 5;
            this._viewManagerTab.TableName = "view_manager";
            // 
            // _find
            // 
            this._find.Controls.Add(this._myPanel2);
            this._find.Location = new System.Drawing.Point(4, 22);
            this._find.Name = "_find";
            this._find.Size = new System.Drawing.Size(579, 318);
            this._find.TabIndex = 0;
            this._find.Text = "1.Search Data";
            this._find.UseVisualStyleBackColor = true;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._group);
            this._myPanel2.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel2.Controls.Add(this._myScreen1);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel2.Size = new System.Drawing.Size(579, 318);
            this._myPanel2.TabIndex = 0;
            // 
            // _group
            // 
            this._group.Controls.Add(this.group1);
            this._group.Controls.Add(this.group2);
            this._group.Controls.Add(this.group3);
            this._group.Controls.Add(this.group4);
            this._group.Controls.Add(this.group5);
            this._group.Controls.Add(this.group6);
            this._group.Controls.Add(this.group7);
            this._group.Controls.Add(this.group8);
            this._group.Controls.Add(this.group9);
            this._group.Dock = System.Windows.Forms.DockStyle.Fill;
            this._group.FixedName = true;
            this._group.Location = new System.Drawing.Point(5, 83);
            this._group.Multiline = true;
            this._group.Name = "_group";
            this._group.SelectedIndex = 0;
            this._group.ShowTabNumber = true;
            this._group.Size = new System.Drawing.Size(569, 230);
            this._group.TabIndex = 4;
            this._group.TableName = "view_manager_find";
            // 
            // group1
            // 
            this.group1.Location = new System.Drawing.Point(4, 22);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(561, 204);
            this.group1.TabIndex = 9;
            this.group1.Text = "Group 1";
            this.group1.UseVisualStyleBackColor = true;
            // 
            // group2
            // 
            this.group2.Location = new System.Drawing.Point(4, 22);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(561, 204);
            this.group2.TabIndex = 10;
            this.group2.Text = "Group 2";
            this.group2.UseVisualStyleBackColor = true;
            // 
            // group3
            // 
            this.group3.Location = new System.Drawing.Point(4, 22);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(561, 204);
            this.group3.TabIndex = 2;
            this.group3.Text = "Group 3";
            this.group3.UseVisualStyleBackColor = true;
            // 
            // group4
            // 
            this.group4.Location = new System.Drawing.Point(4, 22);
            this.group4.Name = "group4";
            this.group4.Size = new System.Drawing.Size(561, 204);
            this.group4.TabIndex = 3;
            this.group4.Text = "Group 4";
            this.group4.UseVisualStyleBackColor = true;
            // 
            // group5
            // 
            this.group5.Location = new System.Drawing.Point(4, 22);
            this.group5.Name = "group5";
            this.group5.Size = new System.Drawing.Size(561, 204);
            this.group5.TabIndex = 4;
            this.group5.Text = "Group 5";
            this.group5.UseVisualStyleBackColor = true;
            // 
            // group6
            // 
            this.group6.Location = new System.Drawing.Point(4, 22);
            this.group6.Name = "group6";
            this.group6.Size = new System.Drawing.Size(561, 204);
            this.group6.TabIndex = 5;
            this.group6.Text = "Group 6";
            this.group6.UseVisualStyleBackColor = true;
            // 
            // group7
            // 
            this.group7.Location = new System.Drawing.Point(4, 22);
            this.group7.Name = "group7";
            this.group7.Size = new System.Drawing.Size(561, 204);
            this.group7.TabIndex = 6;
            this.group7.Text = "Group 7";
            this.group7.UseVisualStyleBackColor = true;
            // 
            // group8
            // 
            this.group8.Location = new System.Drawing.Point(4, 22);
            this.group8.Name = "group8";
            this.group8.Size = new System.Drawing.Size(561, 204);
            this.group8.TabIndex = 7;
            this.group8.Text = "Group 8";
            this.group8.UseVisualStyleBackColor = true;
            // 
            // group9
            // 
            this.group9.Location = new System.Drawing.Point(4, 22);
            this.group9.Name = "group9";
            this.group9.Size = new System.Drawing.Size(561, 204);
            this.group9.TabIndex = 8;
            this.group9.Text = "Group 9";
            this.group9.UseVisualStyleBackColor = true;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this.ButtonTest);
            this._myFlowLayoutPanel2.Controls.Add(this.ButtonLoadTemplate);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(5, 56);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(569, 27);
            this._myFlowLayoutPanel2.TabIndex = 3;
            // 
            // ButtonTest
            // 
            this.ButtonTest.AutoSize = true;
            this.ButtonTest.BackColor = System.Drawing.Color.Transparent;
            this.ButtonTest.ButtonText = "test";
            this.ButtonTest.Location = new System.Drawing.Point(515, 0);
            this.ButtonTest.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.ButtonTest.myImage = global::MyLib.Resource16x16.view;
            this.ButtonTest.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonTest.myUseVisualStyleBackColor = false;
            this.ButtonTest.Name = "ButtonTest";
            this.ButtonTest.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ButtonTest.ResourceName = "test";
            this.ButtonTest.Size = new System.Drawing.Size(53, 27);
            this.ButtonTest.TabIndex = 0;
            this.ButtonTest.Text = "test";
            this.ButtonTest.UseVisualStyleBackColor = false;
            this.ButtonTest.Click += new System.EventHandler(this.ButtonTest_Click);
            // 
            // ButtonLoadTemplate
            // 
            this.ButtonLoadTemplate.AutoSize = true;
            this.ButtonLoadTemplate.BackColor = System.Drawing.Color.Transparent;
            this.ButtonLoadTemplate.ButtonText = "default_load";
            this.ButtonLoadTemplate.Location = new System.Drawing.Point(416, 0);
            this.ButtonLoadTemplate.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.ButtonLoadTemplate.myImage = global::MyLib.Resource16x16.recycle;
            this.ButtonLoadTemplate.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonLoadTemplate.myUseVisualStyleBackColor = false;
            this.ButtonLoadTemplate.Name = "ButtonLoadTemplate";
            this.ButtonLoadTemplate.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ButtonLoadTemplate.ResourceName = "default_load";
            this.ButtonLoadTemplate.Size = new System.Drawing.Size(97, 27);
            this.ButtonLoadTemplate.TabIndex = 1;
            this.ButtonLoadTemplate.Text = "default_load";
            this.ButtonLoadTemplate.UseVisualStyleBackColor = false;
            this.ButtonLoadTemplate.Click += new System.EventHandler(this.ButtonLoadTemplate_Click);
            // 
            // _myScreen1
            // 
            this._myScreen1._isChange = false;
            this._myScreen1.BackColor = System.Drawing.Color.Transparent;
            this._myScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myScreen1.Location = new System.Drawing.Point(5, 5);
            this._myScreen1.Name = "_myScreen1";
            this._myScreen1.Size = new System.Drawing.Size(569, 51);
            this._myScreen1.TabIndex = 1;
            this._myScreen1.Paint += new System.Windows.Forms.PaintEventHandler(this._myScreen1_Paint);
            // 
            // _form
            // 
            this._form.Location = new System.Drawing.Point(4, 22);
            this._form.Name = "_form";
            this._form.Padding = new System.Windows.Forms.Padding(3);
            this._form.Size = new System.Drawing.Size(579, 318);
            this._form.TabIndex = 1;
            this._form.Text = "2.Form";
            this._form.UseVisualStyleBackColor = true;
            // 
            // _document
            // 
            this._document.Location = new System.Drawing.Point(4, 22);
            this._document.Name = "_document";
            this._document.Size = new System.Drawing.Size(579, 318);
            this._document.TabIndex = 2;
            this._document.Text = "3.Document";
            this._document.UseVisualStyleBackColor = true;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this.ButtonSave);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(5, 349);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(587, 28);
            this._myFlowLayoutPanel1.TabIndex = 0;
            // 
            // ButtonSave
            // 
            this.ButtonSave.AutoSize = true;
            this.ButtonSave.BackColor = System.Drawing.Color.Transparent;
            this.ButtonSave.ButtonText = "บันทึก";
            this.ButtonSave.Location = new System.Drawing.Point(519, 0);
            this.ButtonSave.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.ButtonSave.myImage = global::MyLib.Resource16x16.disk_blue;
            this.ButtonSave.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonSave.myUseVisualStyleBackColor = false;
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ButtonSave.ResourceName = "save";
            this.ButtonSave.Size = new System.Drawing.Size(67, 27);
            this.ButtonSave.TabIndex = 1;
            this.ButtonSave.Text = "บันทึก";
            this.ButtonSave.UseVisualStyleBackColor = false;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._workPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 343);
            this.panel1.TabIndex = 0;
            // 
            // _viewManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._splitContainer);
            this.Controls.Add(this._toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_viewManage";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(773, 392);
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel1.PerformLayout();
            this._splitContainer.Panel2.ResumeLayout(false);
            this._splitContainer.Panel2.PerformLayout();
            this._splitContainer.ResumeLayout(false);
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this._viewManagerTab.ResumeLayout(false);
            this._find.ResumeLayout(false);
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this._group.ResumeLayout(false);
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer _splitContainer;
		private _myTreeView _menuList;
		private _myFlowLayoutPanel _myFlowLayoutPanel3;
		private _myButton ButtonExit;
		private _myPanel _workPanel;
		private _myTabControl _viewManagerTab;
		private System.Windows.Forms.TabPage _find;
		private _myPanel _myPanel2;
		private _myTabControl _group;
		private System.Windows.Forms.TabPage group1;
		private System.Windows.Forms.TabPage group2;
		private System.Windows.Forms.TabPage group3;
		private System.Windows.Forms.TabPage group4;
		private System.Windows.Forms.TabPage group5;
		private System.Windows.Forms.TabPage group6;
		private System.Windows.Forms.TabPage group7;
		private System.Windows.Forms.TabPage group8;
		private System.Windows.Forms.TabPage group9;
		private _myFlowLayoutPanel _myFlowLayoutPanel2;
		private _myButton ButtonTest;
		private _myButton ButtonLoadTemplate;
		private _myScreen _myScreen1;
		private System.Windows.Forms.TabPage _form;
		private System.Windows.Forms.TabPage _document;
		private _myFlowLayoutPanel _myFlowLayoutPanel1;
		private _myButton ButtonSave;
        public System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.ToolStripButton _resetButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        public System.Windows.Forms.ToolStrip _toolStrip1;
        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel;
        private System.Windows.Forms.Panel panel1;
	}
}
