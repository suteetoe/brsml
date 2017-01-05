namespace MyLib._databaseManage
{
    partial class _verifyDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_verifyDatabase));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._timerForProgress = new System.Windows.Forms.Timer(this.components);
            this._splitMain = new System.Windows.Forms.SplitContainer();
            this._listViewDatabase = new MyLib._listViewXP();
            this._resultTextBox = new System.Windows.Forms.RichTextBox();
            this.Result = new System.Windows.Forms.Label();
            this._progressBarTable = new System.Windows.Forms.ProgressBar();
            this._progressTextTable = new System.Windows.Forms.Label();
            this._progressBarDatabase = new System.Windows.Forms.ProgressBar();
            this._progressTextDatabase = new System.Windows.Forms.Label();
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSelectAll = new MyLib.ToolStripMyButton();
            this._buttonStart = new MyLib.ToolStripMyButton();
            this._buttonExit = new MyLib.ToolStripMyButton();
            this._sendXMLButton = new MyLib.ToolStripMyButton();
            this._myGroupBox1 = new MyLib._myGroupBox();
            this._viewByList = new System.Windows.Forms.RadioButton();
            this._viewByIcon = new System.Windows.Forms.RadioButton();
            this._splitMain.Panel1.SuspendLayout();
            this._splitMain.Panel2.SuspendLayout();
            this._splitMain.SuspendLayout();
            this._toolStrip1.SuspendLayout();
            this._myGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "data.png");
            this.imageList1.Images.SetKeyName(1, "data_certificate.png");
            // 
            // _timerForProgress
            // 
            this._timerForProgress.Tick += new System.EventHandler(this._timerForProgress_Tick);
            // 
            // _splitMain
            // 
            this._splitMain.BackColor = System.Drawing.SystemColors.Window;
            this._splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitMain.Location = new System.Drawing.Point(0, 25);
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
            this._splitMain.Panel2.Controls.Add(this._progressBarTable);
            this._splitMain.Panel2.Controls.Add(this._progressTextTable);
            this._splitMain.Panel2.Controls.Add(this._progressBarDatabase);
            this._splitMain.Panel2.Controls.Add(this._progressTextDatabase);
            this._splitMain.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this._splitMain.Size = new System.Drawing.Size(696, 538);
            this._splitMain.SplitterDistance = 189;
            this._splitMain.SplitterWidth = 1;
            this._splitMain.TabIndex = 4;
            this._splitMain.Text = "splitContainer1";
            // 
            // _listViewDatabase
            // 
            this._listViewDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listViewDatabase.LargeImageList = this.imageList1;
            this._listViewDatabase.Location = new System.Drawing.Point(0, 0);
            this._listViewDatabase.Margin = new System.Windows.Forms.Padding(0);
            this._listViewDatabase.Name = "_listViewDatabase";
            this._listViewDatabase.Size = new System.Drawing.Size(694, 187);
            this._listViewDatabase.TabIndex = 0;
            this._listViewDatabase.UseCompatibleStateImageBehavior = false;
            // 
            // _resultTextBox
            // 
            this._resultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultTextBox.Location = new System.Drawing.Point(2, 86);
            this._resultTextBox.Name = "_resultTextBox";
            this._resultTextBox.Size = new System.Drawing.Size(690, 258);
            this._resultTextBox.TabIndex = 4;
            this._resultTextBox.Text = "";
            // 
            // Result
            // 
            this.Result.AutoSize = true;
            this.Result.Dock = System.Windows.Forms.DockStyle.Top;
            this.Result.Location = new System.Drawing.Point(2, 72);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(40, 14);
            this.Result.TabIndex = 5;
            this.Result.Text = "Result";
            // 
            // _progressBarTable
            // 
            this._progressBarTable.Dock = System.Windows.Forms.DockStyle.Top;
            this._progressBarTable.Location = new System.Drawing.Point(2, 51);
            this._progressBarTable.Name = "_progressBarTable";
            this._progressBarTable.Size = new System.Drawing.Size(690, 21);
            this._progressBarTable.TabIndex = 3;
            // 
            // _progressTextTable
            // 
            this._progressTextTable.AutoSize = true;
            this._progressTextTable.Dock = System.Windows.Forms.DockStyle.Top;
            this._progressTextTable.Location = new System.Drawing.Point(2, 37);
            this._progressTextTable.Name = "_progressTextTable";
            this._progressTextTable.Size = new System.Drawing.Size(72, 14);
            this._progressTextTable.TabIndex = 2;
            this._progressTextTable.Text = "Verify Table";
            // 
            // _progressBarDatabase
            // 
            this._progressBarDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this._progressBarDatabase.Location = new System.Drawing.Point(2, 16);
            this._progressBarDatabase.Name = "_progressBarDatabase";
            this._progressBarDatabase.Size = new System.Drawing.Size(690, 21);
            this._progressBarDatabase.TabIndex = 1;
            // 
            // _progressTextDatabase
            // 
            this._progressTextDatabase.AutoSize = true;
            this._progressTextDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this._progressTextDatabase.Location = new System.Drawing.Point(2, 2);
            this._progressTextDatabase.Name = "_progressTextDatabase";
            this._progressTextDatabase.Size = new System.Drawing.Size(92, 14);
            this._progressTextDatabase.TabIndex = 0;
            this._progressTextDatabase.Text = "Verify Database";
            // 
            // _toolStrip1
            // 
            this._toolStrip1.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this._toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSelectAll,
            this._buttonStart,
            this._buttonExit,
            this._sendXMLButton});
            this._toolStrip1.Location = new System.Drawing.Point(0, 0);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(696, 25);
            this._toolStrip1.TabIndex = 17;
            this._toolStrip1.Text = "toolStrip1";
            // 
            // _buttonSelectAll
            // 
            this._buttonSelectAll.Image = global::MyLib.Resource16x16.preferences;
            this._buttonSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectAll.Name = "_buttonSelectAll";
            this._buttonSelectAll.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSelectAll.ResourceName = "select_all";
            this._buttonSelectAll.Size = new System.Drawing.Size(76, 22);
            this._buttonSelectAll.Text = "select_all";
            this._buttonSelectAll.Click += new System.EventHandler(this._buttonSelectAll_Click);
            // 
            // _buttonStart
            // 
            this._buttonStart.Image = global::MyLib.Resource16x16.flash;
            this._buttonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonStart.Name = "_buttonStart";
            this._buttonStart.Padding = new System.Windows.Forms.Padding(1);
            this._buttonStart.ResourceName = "start_verify";
            this._buttonStart.Size = new System.Drawing.Size(86, 22);
            this._buttonStart.Text = "start_verify";
            this._buttonStart.Click += new System.EventHandler(this._buttonStart_Click);
            // 
            // _buttonExit
            // 
            this._buttonExit.Image = global::MyLib.Resource16x16.error;
            this._buttonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonExit.Name = "_buttonExit";
            this._buttonExit.Padding = new System.Windows.Forms.Padding(1);
            this._buttonExit.ResourceName = "screen_close";
            this._buttonExit.Size = new System.Drawing.Size(95, 22);
            this._buttonExit.Text = "screen_close";
            this._buttonExit.Click += new System.EventHandler(this._buttonExit_Click);
            // 
            // _sendXMLButton
            // 
            this._sendXMLButton.Image = global::MyLib.Properties.Resources.document_into;
            this._sendXMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._sendXMLButton.Name = "_sendXMLButton";
            this._sendXMLButton.Padding = new System.Windows.Forms.Padding(1);
            this._sendXMLButton.ResourceName = "Send XML";
            this._sendXMLButton.Size = new System.Drawing.Size(82, 22);
            this._sendXMLButton.Text = "Send XML";
            this._sendXMLButton.Visible = false;
            this._sendXMLButton.Click += new System.EventHandler(this._sendXMLButton_Click);
            // 
            // _myGroupBox1
            // 
            this._myGroupBox1.Controls.Add(this._viewByList);
            this._myGroupBox1.Controls.Add(this._viewByIcon);
            this._myGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myGroupBox1.Location = new System.Drawing.Point(2, 2);
            this._myGroupBox1.Name = "_myGroupBox1";
            this._myGroupBox1.ResourceName = "view_by";
            this._myGroupBox1.Size = new System.Drawing.Size(112, 100);
            this._myGroupBox1.TabIndex = 0;
            this._myGroupBox1.TabStop = false;
            this._myGroupBox1.Text = "แสดงโดย";
            // 
            // _viewByList
            // 
            this._viewByList.AutoSize = true;
            this._viewByList.Checked = true;
            this._viewByList.Location = new System.Drawing.Point(6, 39);
            this._viewByList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._viewByList.Name = "_viewByList";
            this._viewByList.Size = new System.Drawing.Size(41, 17);
            this._viewByList.TabIndex = 1;
            this._viewByList.TabStop = true;
            this._viewByList.Text = "List";
            this._viewByList.CheckedChanged += new System.EventHandler(this._viewByList_CheckedChanged);
            // 
            // _viewByIcon
            // 
            this._viewByIcon.AutoSize = true;
            this._viewByIcon.Location = new System.Drawing.Point(6, 18);
            this._viewByIcon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._viewByIcon.Name = "_viewByIcon";
            this._viewByIcon.Size = new System.Drawing.Size(46, 17);
            this._viewByIcon.TabIndex = 0;
            this._viewByIcon.Text = "Icon";
            this._viewByIcon.CheckedChanged += new System.EventHandler(this._viewByIcon_CheckedChanged);
            // 
            // _verifyDatabase
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(696, 563);
            this.Controls.Add(this._splitMain);
            this.Controls.Add(this._toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_verifyDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verify Database";
            this.Load += new System.EventHandler(this._verifyDatabase_Load);
            this._splitMain.Panel1.ResumeLayout(false);
            this._splitMain.Panel2.ResumeLayout(false);
            this._splitMain.Panel2.PerformLayout();
            this._splitMain.ResumeLayout(false);
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this._myGroupBox1.ResumeLayout(false);
            this._myGroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitMain;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label _progressTextDatabase;
        private System.Windows.Forms.ProgressBar _progressBarDatabase;
        private System.Windows.Forms.Label _progressTextTable;
        private System.Windows.Forms.ProgressBar _progressBarTable;
        private System.Windows.Forms.RichTextBox _resultTextBox;
        private System.Windows.Forms.Label Result;
        private System.Windows.Forms.RadioButton _viewByIcon;
        private System.Windows.Forms.RadioButton _viewByList;
		private MyLib._listViewXP _listViewDatabase;
        private System.Windows.Forms.ToolStrip _toolStrip1;
        private _myGroupBox _myGroupBox1;
        private ToolStripMyButton _buttonSelectAll;
        private ToolStripMyButton _buttonStart;
        private ToolStripMyButton _buttonExit;
        private System.Windows.Forms.Timer _timerForProgress;
        private ToolStripMyButton _sendXMLButton;
    }
}